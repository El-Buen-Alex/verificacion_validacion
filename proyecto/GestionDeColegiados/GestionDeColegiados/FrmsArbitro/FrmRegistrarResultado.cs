using System;
using System.Windows.Forms;

using Control.AdmEncuentros;
using Control.AdmEncuentrosGenerados;

namespace GestionDeColegiados.FrmsArbitro {
  public partial class FrmRegistrarResultado : Form {
    private AdmEncuentrosDefinidos _admEncuentrosDefinidos = AdmEncuentrosDefinidos.GetAdmGenerarEncuentrosDefinidos();
    private AdmEncuentroFinalizado _admEncuentroFinalizado = AdmEncuentroFinalizado.GetAdmEncuentrosFinalizados();
    public FrmRegistrarResultado() {
      InitializeComponent();
      _admEncuentrosDefinidos.LlenarPartidosCmb(cmbEncuentros);
      CambiarDisponibilidadControladoresUi(false);
    }
    private void LimpiarControladoresUi() {
      txtGolesLocal.Text = "";
      txtGolesVisitante.Text = "";
      lblPuntosLocalResultado.Text = "";
      lblPuntosVisitanteResultado.Text = "";
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
      btnGuardarCambios.Enabled = estado;
    }

    private void RefrezcarComponentes() {
      int index = cmbEncuentros.SelectedIndex;
      _admEncuentrosDefinidos.LlenarMatch(index, lblEquipoLocal, lblEquipoVisitante);
    }
    private void CmbEncuentros_SelectedIndexChanged(object sender, EventArgs e) {
      RefrezcarComponentes();
      CambiarDisponibilidadControladoresUi(true);
      LimpiarControladoresUi();
    }

    private void Guardado(bool guardado) {
      if(guardado) {

        CambiarDisponibilidadControladoresUi(false);
        _admEncuentrosDefinidos.LlenarPartidosCmb(cmbEncuentros);

      }
    }

    private void EnviarDatosGuardar() {
      bool respuesta = false;
      string golesLocal = txtGolesLocal.Text;
      string golesVisitante = txtGolesVisitante.Text;
      if(String.IsNullOrEmpty(golesLocal) || String.IsNullOrEmpty(golesVisitante)) {

        MessageBox.Show("Ingrese la cantidad de Goles realizado por los equipos correctamente");
      } else {
        int index = cmbEncuentros.SelectedIndex;
        respuesta = _admEncuentroFinalizado.GuardarEncuentroFinalizado(index, golesLocal, golesVisitante);
        Guardado(respuesta);
      }
    }


    private void BtnGuardarCambios_Click(object sender, EventArgs e) {
      EnviarDatosGuardar();
    }

    private void ActualizarPuntos() {
      string golLocal = txtGolesLocal.Text;
      string golVisitante = txtGolesVisitante.Text;
      _admEncuentroFinalizado.CalcularPuntos(lblPuntosLocalResultado, golLocal, golVisitante);
      _admEncuentroFinalizado.CalcularPuntos(lblPuntosVisitanteResultado, golVisitante, golLocal);

    }

    private void TxtGolesLocal_TextChanged(object sender, EventArgs e) {
      ActualizarPuntos();
    }

    private void TxtGolesVisitante_TextChanged(object sender, EventArgs e) {
      ActualizarPuntos();
    }
  }
}
