using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Control;
using Control.AdmColegiados;

namespace GestionDeColegiados.FrmsColegiado {
  /// <summary>
  /// Formulario para editar Áribtros.
  /// </summary>
  public partial class FrmEditarArbitro : Form {
    /// <summary>
    /// DLL y variables necesarias para poder mover el formulario.
    /// </summary>
    [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
    private static extern void ReleaseCapture();
    [DllImport("user32.DLL", EntryPoint = "SendMessage")]
    private static extern void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

    private Color _colorDefaultClose;
    ValidacionGUI _validacionGUI = new ValidacionGUI();
    AdmColegiado _admColegiado = AdmColegiado.GetAdmCol();

    /// <summary>
    /// Constructor del formulario.
    /// </summary>
    /// <param name="arbitro">Tipo de árbitro.</param>
    /// <param name="idColegiado">ID del colegiado.</param>
    public FrmEditarArbitro(string arbitro, string idColegiado) {
      InitializeComponent();
      lblEditar.Text += arbitro;
      lblID.Text = idColegiado;
    }

    /// <summary>
    /// Evento Load del formulario.
    /// </summary>
    /// <param name="sender">Objeto.</param>
    /// <param name="e">Evento.</param>
    private void FrmEditarArbitro_Load(object sender, EventArgs e) {
      _admColegiado.LlenarDatosFormEditar(txtCedula, txtNombre, txtApellido, txtDomicilio, txtEmail, txtTelefono);
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
    private void PbCerrar_Click(object sender, EventArgs e) {
      Close();
    }

    /// <summary>
    /// Eventos que generan un efecto visual en cuanto el mouse pasa por dicho controlador.
    /// </summary>
    /// <param name="sender">Objeto.</param>
    /// <param name="e">Evento.</param>
    private void PbCerrar_MouseEnter(object sender, EventArgs e) {
      _colorDefaultClose = pbCerrar.BackColor;
      pbCerrar.BackColor = Color.FromArgb(202, 49, 32);
    }

    /// <summary>
    /// Eventos que generan un efecto visual en cuanto el mouse sale por dicho controlador.
    /// </summary>
    /// <param name="sender">Objeto.</param>
    /// <param name="e">Evento.</param>
    private void PbCerrar_MouseLeave(object sender, EventArgs e) {
      pbCerrar.BackColor = _colorDefaultClose;
    }

    /// <summary>
    /// Evento para validar que solo ingrese números.
    /// </summary>
    /// <param name="sender">Objeto.</param>
    /// <param name="e">Evento.</param>
    private void ValidarNumeros_KeyPress(object sender, KeyPressEventArgs e) {
      if(!char.IsNumber(e.KeyChar) && (e.KeyChar != Convert.ToChar(Keys.Back))) {

        e.Handled = true;
        return;
      }
    }

    /// <summary>
    /// Evento para validar que solo ingrese letras.
    /// </summary>
    /// <param name="sender">Objeto.</param>
    /// <param name="e">Evento.</param>
    private void ValidarLetras_KeyPress(object sender, KeyPressEventArgs e) {
      if(!char.IsLetter(e.KeyChar) && (e.KeyChar != Convert.ToChar(Keys.Back)) &&
          (e.KeyChar != Convert.ToChar(Keys.Space))) {

        e.Handled = true;
        return;
      }
    }

    /// <summary>
    /// Evento para cambiar el estado del CheckBox
    /// </summary>
    /// <param name="sender">Objeto.</param>
    /// <param name="e">Evento.</param>
    private void ChbxHabilitar_CheckedChanged(object sender, EventArgs e) {
      if(chbxHabilitar.Checked == true) {

        txtCedula.Enabled = true;
      } else {
        txtCedula.Enabled = false;
      }
    }

    /// <summary>
    /// Evento click para el botón actualizar.
    /// </summary>
    /// <param name="sender">Objeto.</param>
    /// <param name="e">Evento.</param>
    private void BtnActualizar_Click(object sender, EventArgs e) {
      bool vacio = _validacionGUI.ValidarVacios(txtCedula, txtNombre, txtApellido, txtDomicilio, txtEmail, txtTelefono);
      bool cedulaRepetida = _admColegiado.ValidarCedula(txtCedula);
      if(vacio == true) {

        MessageBox.Show("Hay ciertos campos vacios", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      if(txtCedula.Enabled == false) {

        Actualiza();
      } else {
        if(cedulaRepetida == true) {

          MessageBox.Show("El árbitro que ingresó ya se encuentra registrado!!\nIngrese uno nuevo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        if(vacio != true && cedulaRepetida != true) {

          Actualiza();
        }
      }
    }

    /// <summary>
    /// Evento click para el botón Cancelar.
    /// </summary>
    /// <param name="sender">Objeto.</param>
    /// <param name="e">Evento.</param>
    private void BtnCancelar_Click(object sender, EventArgs e) {
      DialogResult resultado;
      resultado = MessageBox.Show("¡Está seguro de cancelar!", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
      if(resultado == DialogResult.Yes) {
        Close();
      }
    }

    /// <summary>
    /// Método para actualizar y enviar los datos ingresados del formulario.
    /// </summary>
    private void Actualiza() {
      DialogResult resultado;
      resultado = MessageBox.Show("¡Está seguro de actualizar!", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
      if(resultado == DialogResult.Yes) {
        string cedula = txtCedula.Text,
            nombre = txtNombre.Text,
            apellido = txtApellido.Text,
            domicilio = txtDomicilio.Text,
            email = txtEmail.Text,
            telefono = txtTelefono.Text;
        _admColegiado.EditarArbitro(lblID.Text, cedula, nombre, apellido, domicilio, email, telefono);
        Close();
      }
    }
  }
}