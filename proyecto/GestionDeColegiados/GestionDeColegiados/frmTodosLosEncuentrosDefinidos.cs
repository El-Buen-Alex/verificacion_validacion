using System;
using System.Windows.Forms;

using Control.AdmEncuentrosGenerados;

namespace GestionDeColegiados {
  public partial class FrmTodosLosEncuentrosDefinidos : Form {
    private AdmEncuentrosDefinidos _admEncuentrosDefinidos = AdmEncuentrosDefinidos.GetAdmGenerarEncuentrosDefinidos();
    public FrmTodosLosEncuentrosDefinidos() {
      InitializeComponent();
      //llenamos el combobox con los encuentros definidos disponibles
      _admEncuentrosDefinidos.LlenarPartidosCmb(cmbEncuentros);
      //deshabilitamos la capacidad de acceder a controladores graficos
      CambiarAccesibilidadControladoresGUI(false);
    }

    private void CambiarAccesibilidadControladoresGUI(bool estado) {
      pbTupla.Visible = estado;
      lblColegiados.Visible = estado;
      lblEquipoLocal.Visible = estado;
      lblEquipoVisitante.Visible = estado;
      lblEstadio.Visible = estado;
      lblFecha.Visible = estado;
      lblTituloColegiados.Visible = estado;
    }
    private void CmbEncuentros_SelectedIndexChanged(object sender, EventArgs e) {
      //cuando se seleccione un encuentro se recogera la posicion de la opcion combobox
      int indexEncuentroSeleccionado = cmbEncuentros.SelectedIndex;
      //se llena la información de los encuentros
      _admEncuentrosDefinidos.LlenarInformacíonPartidoCompleta(indexEncuentroSeleccionado, lblEquipoLocal, lblEquipoVisitante, lblEstadio, lblFecha, lblColegiados);
      //se permite la vista de los controladores graficos
      CambiarAccesibilidadControladoresGUI(true);
    }
    private void MostrarMensajeFinalizar(bool respuesta) {

    }
    private void DarBajaEncuentroDefToolStripMenuItem_Click(object sender, EventArgs e) {
      DialogResult res = MessageBox.Show("¿Seguro que quieres terminar la competición?", "Cuidado", MessageBoxButtons.YesNo);
      if(res == DialogResult.Yes) {

        bool resultado = _admEncuentrosDefinidos.DarBajaEncuentrosDefinidos();
        MostrarMensajeFinalizar(resultado);
      }
    }
  }
}
