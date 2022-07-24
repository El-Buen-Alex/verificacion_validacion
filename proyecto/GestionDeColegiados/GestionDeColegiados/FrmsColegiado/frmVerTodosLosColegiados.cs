using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Control.AdmColegiados;

using GestionDeColegiados.FrmsColegiado;

namespace GestionDeColegiados {
  /// <summary>
  /// Formulario para ver, editar y eliminar (Áribtros y colegiado).
  /// </summary>
  public partial class frmVerTodosLosColegiados : Form {
    /// <summary>
    /// DLL y variables necesarias para poder mover el formulario.
    /// </summary>
    [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
    private static extern void ReleaseCapture();
    [DllImport("user32.DLL", EntryPoint = "SendMessage")]
    private static extern void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

    private Color _colorDefaultClose;
    AdmColegiado _admColegiado = AdmColegiado.GetAdmCol();

    /// <summary>
    /// Constructor del formulario.
    /// </summary>
    public frmVerTodosLosColegiados() {
      InitializeComponent();
      _admColegiado.LlenarComboIdColegiado(cmbIdArbitro);
    }

    /// <summary>
    /// Método que controla el evento de arrastrar pantalla.
    /// </summary>
    /// <param name="sender">Objeto.</param>
    /// <param name="e">Evento.</param>
    private void PanelBarraTitulo_MouseDown(object sender, MouseEventArgs e) {
      ReleaseCapture();
      SendMessage(this.Handle, 0x112, 0xf012, 0);
    }

    /// <summary>
    /// Evento para cerrar pantalla.
    /// </summary>
    /// <param name="sender">Objeto.</param>
    /// <param name="e">Evento.</param>
    private void PbCerrar_Click(object sender, System.EventArgs e) {
      Close();
    }

    /// <summary>
    /// Eventos que generan un efecto visual en cuanto el mouse pasa por dicho controlador.
    /// </summary>
    /// <param name="sender">Objeto.</param>
    /// <param name="e">Evento.</param>
    private void PbCerrar_MouseEnter(object sender, System.EventArgs e) {
      _colorDefaultClose = pbCerrar.BackColor;
      pbCerrar.BackColor = Color.FromArgb(202, 49, 32);
    }

    /// <summary>
    /// Eventos que generan un efecto visual en cuanto el mouse sale por dicho controlador.
    /// </summary>
    /// <param name="sender">Objeto.</param>
    /// <param name="e">Evento.</param>
    private void PbCerrar_MouseLeave(object sender, System.EventArgs e) {
      pbCerrar.BackColor = _colorDefaultClose;
    }

    /// <summary>
    /// Evento click para el botón buscar.
    /// </summary>
    /// <param name="sender">Objeto.</param>
    /// <param name="e">Evento.</param>
    private void BtnBuscar_Click(object sender, System.EventArgs e) {
      if(cmbIdArbitro.Text.CompareTo("") != 0) {

        _admColegiado.LlenarDatosGrivColegiado(dgvListarColegiados, cmbIdArbitro);
        btnEditar.Enabled = true;
        btnEliminarArbitro.Enabled = true;
        btnEliminarColegiado.Enabled = true;
      } else {
        MessageBox.Show("Debe seleccionar algún grupo para buscar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    /// <summary>
    /// Evento click para el botón editar.
    /// </summary>
    /// <param name="sender">Objeto.</param>
    /// <param name="e">Evento.</param>>
    private void BtnEditar_Click(object sender, System.EventArgs e) {
      DataGridViewRow filaSeleccionada = dgvListarColegiados.CurrentRow;
      string arbitro = filaSeleccionada.Cells[0].Value.ToString();
      FrmEditarArbitro frmEditar = new FrmEditarArbitro(arbitro, cmbIdArbitro.Text);
      _admColegiado.RecogerDatosEditar(dgvListarColegiados);
      frmEditar.ShowDialog();
      _admColegiado.LlenarDatosGrivColegiado(dgvListarColegiados, cmbIdArbitro);
    }

    /// <summary>
    /// Evento click para el botón eliminar árbitro.
    /// </summary>
    /// <param name="sender">Objeto.</param>
    /// <param name="e">Evento.</param>>
    private void BtnEliminarArbitro_Click(object sender, System.EventArgs e) {
      DialogResult resultado;
      resultado = MessageBox.Show("¡Está seguro de eliminar un árbitro!\nSi acepta tendrá que agregar uno nuevo", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
      if(resultado == DialogResult.Yes) {

        DataGridViewRow row = dgvListarColegiados.CurrentRow;
        string arbitro = row.Cells[0].Value.ToString();
        FrmElimAgregarArbitro frmAgregar = new FrmElimAgregarArbitro(arbitro, cmbIdArbitro.Text);
        _admColegiado.RecogerDatosEliminar(dgvListarColegiados);
        frmAgregar.ShowDialog();
        _admColegiado.LlenarDatosGrivColegiado(dgvListarColegiados, cmbIdArbitro);
      }
    }

    /// <summary>
    /// Evento click para el botón eliminar colegiado.
    /// </summary>
    /// <param name="sender">Objeto.</param>
    /// <param name="e">Evento.</param>>
    private void BtnEliminarColegiado_Click(object sender, System.EventArgs e) {
      bool eliminado = false;
      DialogResult resultado;
      resultado = MessageBox.Show("¡Está seguro de eliminar el " + cmbIdArbitro.Text + " de colegiados!", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
      if(resultado == DialogResult.Yes) {

        eliminado = _admColegiado.EliminarColegiado(cmbIdArbitro.Text);
        if(eliminado == true) {

          _admColegiado.LlenarComboIdColegiado(cmbIdArbitro);
          dgvListarColegiados.Rows.Clear();
        }
      }
    }
  }
}