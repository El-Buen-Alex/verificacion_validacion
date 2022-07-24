using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Control;
using Control.AdmColegiados;

namespace GestionDeColegiados.FrmsColegiado {
  /// <summary>
  /// Formulario para eliminar y agregar Áribtros.
  /// </summary>
  public partial class FrmElimAgregarArbitro : Form {
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
    public FrmElimAgregarArbitro(string arbitro, string idColegiado) {
      InitializeComponent();
      lblAgregar.Text += arbitro;
      lblID.Text = idColegiado;
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
    /// Evento click para el botón agregar.
    /// </summary>
    /// <param name="sender">Objeto.</param>
    /// <param name="e">Evento.</param>
    private void BtnAgregar_Click(object sender, EventArgs e) {
      bool vacio = _validacionGUI.ValidarVacios(txtCedula, txtNombre, txtApellido, txtDomicilio, txtEmail, txtTelefono);
      bool cedulaRepetida = _admColegiado.ValidarCedula(txtCedula);
      if(vacio == true) {
        MessageBox.Show("Hay ciertos campos vacios", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      if(cedulaRepetida == true) {
        MessageBox.Show("El árbitro que ingresó ya se encuentra registrado!!\nIngrese uno nuevo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      if(vacio != true && cedulaRepetida != true) {
        string cedula = txtCedula.Text,
            nombre = txtNombre.Text,
            apellido = txtApellido.Text,
            domicilio = txtDomicilio.Text,
            email = txtEmail.Text,
            telefono = txtTelefono.Text;
        _admColegiado.EliminarArbitro(lblID.Text, cedula, nombre, apellido, domicilio, email, telefono);
        Close();
      }
    }

    /// <summary>
    /// Evento click para el botón cancelar.
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
  }
}