using System;
using System.Windows.Forms;

using Control.AdmColegiados;
using Control.AdmEncuentrosGenerados;
using Control.AdmEstadios;

namespace GestionDeColegiados {
  public partial class FrmRegistrarPartido : Form {
    private AdmColegiado _admColegiado = AdmColegiado.GetAdmCol();
    private AdmGenerarEncuentros _admGenerarEncuentros = AdmGenerarEncuentros.GetAdmadmGenerarEncuentros();
    private AdmEncuentrosDefinidos _admGenerarEncuentroDefinido = AdmEncuentrosDefinidos.GetAdmGenerarEncuentrosDefinidos();
    private AdmEstadio _admEstadio = AdmEstadio.GetAdmEstadio();
    public FrmRegistrarPartido() {
      InitializeComponent();
      RefrezcarContenedores();

    }
    private void RefrezcarContenedores() {
      /*pasamos posicion 0, porque cada vez que se asigna un estadio y coleggiado a un 
       ecneuntroG generado pendiente, este cambia de estado("por jugar") y en la lista existe
      un encuentro menos por definir estadio y colegiado*/
      _admGenerarEncuentros.LlenarTuplas(lblEquipoLocal, lblEquipoVisitante, 0);
      _admColegiado.LlenarColegiadosCmb(cmbGrupoColegiado);
      _admEstadio.LlenarEstadiosCmb(cmbEstadio);
    }
    private void CambiarAccesibilidadControlesGraficos(bool accesibilidad) {
      dtpFechaEncuentro.Enabled = accesibilidad;
      cmbGrupoColegiado.Enabled = accesibilidad;
      btnRegistrar.Enabled = accesibilidad;
      dtpHora.Enabled = accesibilidad;
      cmbEstadio.Enabled = accesibilidad;
      btnSiguiente.Enabled = !accesibilidad;
    }
    private void ControladoresGUINoDisponibles() {
      dtpFechaEncuentro.Enabled = false;
      cmbGrupoColegiado.Enabled = false;
      dtpHora.Enabled = false;
      btnRegistrar.Enabled = false;
      btnSiguiente.Enabled = false;
      cmbEstadio.Enabled = false;
    }
    private void BtnRegistrar_Click(object sender, EventArgs e) {
      int grupoSeleccionado = cmbGrupoColegiado.SelectedIndex;
      DateTime fechaPartido = dtpFechaEncuentro.Value;
      DateTime horaPartido = dtpHora.Value;
      int estadioSeleccionado = cmbEstadio.SelectedIndex;
      bool guardo = _admGenerarEncuentroDefinido.GuardarEncuentroDefinido(grupoSeleccionado, fechaPartido, horaPartido, estadioSeleccionado, 0);
      if(guardo) {
        //si guarda se bloquea la capacidad de editar algun encuentro ya definido
        CambiarAccesibilidadControlesGraficos(false);
      }
      if(_admGenerarEncuentros.ObtnerNumeroEncuentrosGeneradosPendientes() == 0) {
        //Si ya no existen encuentros pedientes por definir colegiados, estado y fecha
        //se bloquea la capacidad de interactuar completamente con esta interfaz
        ControladoresGUINoDisponibles();
      }
    }

    private void BtnSiguiente_Click(object sender, EventArgs e) {
      //si da clic en siguiente, se resetean los componentes y 
      //se cambia la accesbilidad de la ventana grafica
      RefrezcarContenedores();
      CambiarAccesibilidadControlesGraficos(true);
    }
  }
}
