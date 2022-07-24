using System;
using System.Windows.Forms;

using Control.AdmEquipos;

namespace GestionDeColegiados {
  public partial class FrmVerTodos : Form {
    AdmEquipo _equipo = AdmEquipo.GetEquipo();
    public FrmVerTodos() {
      InitializeComponent();
      btnEditar.Visible = false;
      btnEliminar.Visible = false;
    }

    private void BtnEditar_Click(object sender, EventArgs e) {
      DataGridViewRow filaSeleccionada = tablaDatos.CurrentRow;
      string id = filaSeleccionada.Cells[1].Value.ToString();
      if(id.CompareTo("") != 0) {
        FrmEditarEquipo editar = new FrmEditarEquipo(id);
        editar.ShowDialog();
      }

    }

    private void Buscar_Click(object sender, EventArgs e) {
      String nombre = nomEquipo.Text;
      if(nombre.CompareTo("") != 0) {
        _equipo.LlenaTabla(tablaDatos, nombre);
        if(tablaDatos.Rows.Count > 0) {
          btnEditar.Visible = true;
          btnEliminar.Visible = true;
        } else {
          btnEditar.Visible = false;
          btnEliminar.Visible = false;
          MessageBox.Show("No existen resultados");
        }
      } else {
        MessageBox.Show("Debe ingresar algún parámetro para buscar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void BtnEliminar_Click(object sender, EventArgs e) {
      DataGridViewRow filaSeleccionada = tablaDatos.CurrentRow;
      string id = filaSeleccionada.Cells[1].Value.ToString();
      DialogResult resultado;
      resultado = MessageBox.Show("¡Está seguro de eliminar un equipo!\nSi acepta tendrá que agregar uno nuevo", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
      if(id.CompareTo("") != 0 && resultado == DialogResult.Yes) {
        _equipo.EliminarRegistro(id);
      }
    }
  }
}