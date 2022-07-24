using System;
using System.Windows.Forms;

using Control;
using Control.AdmColegiados;
using Control.AdmEncuentrosGenerados;
using Control.AdmEstadios;

namespace GestionDeColegiados {
  public partial class FrmCambiarEstadioPartido : Form {
    AdmEncuentrosDefinidos _admEncuentrosDefinidos = AdmEncuentrosDefinidos.GetAdmGenerarEncuentrosDefinidos();
    AdmEstadio _admEstadio = AdmEstadio.GetAdmEstadio();
    AdmColegiado _admColegiado = AdmColegiado.GetAdmCol();
    private bool _muestraInfo;
    private ValidacionGUI _validacionGUI = new ValidacionGUI();
    public FrmCambiarEstadioPartido() {
      InitializeComponent();
      //se setean los controladores graficos
      _admEncuentrosDefinidos.LlenarPartidosCmb(cmbEncuentros);
      CambiarDisponibilidadControladoresUi(false);
    }

    private void EnviarDatosGuardar() {
      int indexEncuentro, indexEstadio, indexColegiados;
      if(cmbEncuentros.Items.ToString() != "" && cmbEstadios.Items.ToString() != "") {
        //se recupera la posicion del encuentro al que se desea cambiar el estadio
        //se selecciona el nuevo estadio al que se desea asignar al encuentro
        indexEncuentro = cmbEncuentros.SelectedIndex;
        indexEstadio = cmbEstadios.SelectedIndex;
        indexColegiados = cmbGrupoColegiado.SelectedIndex;
        DateTime fecha = dtpFechaEncuentro.Value;
        DateTime hora = dtpHora.Value;

        //se intenta cambiar el estadio al encuentro
        bool actualizo = _admEncuentrosDefinidos.ActualizarEncuentroDefinido(indexEncuentro, indexEstadio, indexColegiados, fecha, hora);
        if(actualizo) {
          // si no ocurre problemas al cambiar, se refrezcan los componentes y 
          //se muestra que el cambio se realizó con exito
          MessageBox.Show("El cambio se realizo con exito");
          CambiarDisponibilidadControladoresUi(false);
          CambiarAccesibilidadBotonGuardar(false);
        } else {
          MessageBox.Show("No se logró modificar el registro.");
        }
      }
    }

    private void BtnGuardarCambios_Click(object sender, EventArgs e) {
      EnviarDatosGuardar();
    }

    private void CmbEncuentros_SelectedIndexChanged(object sender, EventArgs e) {
      /*cuando el usuario seleccione un encuentro
       el contenido del lblEstadioActual se seteara con el
      nombre del estadio actual del encuentro que se ha seleccionado*/
      int indexEncuentroDefinidoSeleccionado = cmbEncuentros.SelectedIndex;
      _muestraInfo = _admEncuentrosDefinidos.LlenarInformacíonPartidoCompleta(indexEncuentroDefinidoSeleccionado, lblEquipoLocal, lblEquipoVisitante, cmbEstadios, dtpFechaEncuentro, dtpHora, cmbGrupoColegiado, txtColegiados);
      if(_muestraInfo) {

        CambiarDisponibilidadControladoresUi(true);
        ValidarFecha();
      }
    }
    private void CambiarDisponibilidadControladoresUi(bool estado) {
      lblEquipoLocal.Enabled = estado;
      lblEquipoVisitante.Enabled = estado;
      dtpFechaEncuentro.Enabled = estado;
      dtpHora.Enabled = estado;
      cmbGrupoColegiado.Enabled = estado;
      cmbEstadios.Enabled = estado;
    }
    private void CambiarAccesibilidadBotonGuardar(bool estado) {
      btnGuardarCambios.Enabled = estado;
    }

    private void Cmb_SelectedIndexChanged(object sender, EventArgs e) {
      //si se selecciona un nuevo estadio, habilitará la opcion de guardar
      btnGuardarCambios.Enabled = true;
    }
    private void CmbColegiados_SelectedIndexChanged(object sender, EventArgs e) {
      //si se selecciona un nuevo estadio, habilitará la opcion de guardar
      int indexColegiados = cmbGrupoColegiado.SelectedIndex;
      string s = _admColegiado.ObtenerNombreDeColegiadosIndex(indexColegiados);
      txtColegiados.Text = s;
      btnGuardarCambios.Enabled = true;
    }

    private void ValidarFecha() {
      bool fecha = _validacionGUI.ValidarFecha(dtpFechaEncuentro.Value);
      if(!fecha) {

        CambiarDisponibilidadControladoresUi(false);
        CambiarAccesibilidadBotonGuardar(false);
        dtpFechaEncuentro.Enabled = true;
        lblFechaMenor.Visible = true;
      } else {
        lblFechaMenor.Visible = false;
        CambiarDisponibilidadControladoresUi(_muestraInfo);
        CambiarAccesibilidadBotonGuardar(_muestraInfo);
      }
    }

    private void DtpFechaEncuentro_ValueChanged(object sender, EventArgs e) {
      ValidarFecha();

    }
    private void DtpHoraEncuentro_ValueChanged(object sender, EventArgs e) {
      CambiarAccesibilidadBotonGuardar(_muestraInfo);
    }
  }
}