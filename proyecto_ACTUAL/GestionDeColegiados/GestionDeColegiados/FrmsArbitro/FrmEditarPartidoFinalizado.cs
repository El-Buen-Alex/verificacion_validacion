using System;
using System.Windows.Forms;

using Control.AdmEncuentros;
using Control.AdmEncuentrosGenerados;

namespace GestionDeColegiados.FrmsArbitro {
  public partial class FrmEditarPartidoFinalizado : Form {
    private AdmEncuentrosDefinidos _admEncuentrosDefinidos = AdmEncuentrosDefinidos.GetAdmGenerarEncuentrosDefinidos();
    private AdmEncuentroFinalizado _admEncuentroFinalizado = AdmEncuentroFinalizado.GetAdmEncuentrosFinalizados();
    public FrmEditarPartidoFinalizado() {
      InitializeComponent();
      bool lleno = _admEncuentrosDefinidos.LlenarCmbEncuentrosDefinidosFinalizados(cmbEncuentros);
      CambiarDisponibilidadControladoresUi(false);

    }
    private void CambiarDisponibilidadControladoresUi(bool estado) {
      lblEquipoLocal.Enabled = estado;
      lblEquipoVisitante.Enabled = estado;
      txtGolesLocal.Enabled = estado;
      txtGolesVisitante.Enabled = estado;
      lblPuntosLocal1.Enabled = estado;
      lblPuntosVisitante.Enabled = estado;
      lblPuntosLocalResultado.Visible = estado;
      lblPuntosVisitanteResultado.Visible = estado;
    }

    private void RefrezcarComponentes() {
      int index = cmbEncuentros.SelectedIndex;
      _admEncuentrosDefinidos.LlenarMatchDefinidosFinalizados(index, lblEquipoLocal, lblEquipoVisitante);
      _admEncuentroFinalizado.LlenarInformacionPartido(index, txtGolesLocal, txtGolesVisitante, lblPuntosLocalResultado, lblPuntosVisitanteResultado);
    }

    private void CmbEncuentros_SelectedIndexChanged(object sender, EventArgs e) {
      RefrezcarComponentes();
      CambiarDisponibilidadControladoresUi(true);
      btnGuardarCambios.Enabled = false;
    }

    private void Actualizado(bool respuesta) {
      btnGuardarCambios.Enabled = !respuesta;
    }
    private void Actualizar() {
      string golesLocal = txtGolesLocal.Text;
      string golesVisitante = txtGolesVisitante.Text;
      if(String.IsNullOrEmpty(golesLocal) || String.IsNullOrEmpty(golesVisitante)) {

        MessageBox.Show("Ingrese la cantidad de Goles realizado por los equipos correctamente");
      } else {
        int index = cmbEncuentros.SelectedIndex;
        bool actualizo = _admEncuentroFinalizado.UpdateEncuentroFinalizado(index, golesLocal, golesVisitante);
        Actualizado(actualizo);
      }

    }
    private void ActualizarPuntos() {
      string golLocal = txtGolesLocal.Text;
      string golVisitante = txtGolesVisitante.Text;
      _admEncuentroFinalizado.CalcularPuntos(lblPuntosLocalResultado, golLocal, golVisitante);
      _admEncuentroFinalizado.CalcularPuntos(lblPuntosVisitanteResultado, golVisitante, golLocal);

    }

    private void TxtGoles_TextChanged(object sender, EventArgs e) {
      btnGuardarCambios.Enabled = true;
      ActualizarPuntos();
    }

    private void BtnGuardarCambios_Click(object sender, EventArgs e) {
      Actualizar();
    }
  }
}
