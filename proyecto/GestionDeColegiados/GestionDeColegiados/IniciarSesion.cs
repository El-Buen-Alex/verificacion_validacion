using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Control;

using GestionDeColegiados.FrmsArbitro;


namespace GestionDeColegiados {
  public partial class BtnIniciarSesion : Form {
    //dlly variables necesarios para poder mover de lugar la barra de titulo 
    [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
    private extern static void ReleaseCapture();
    [DllImport("user32.DLL", EntryPoint = "SendMessage")]
    private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

    /*variables necesarias para gestionar efectos visuales cuando el mouse se posiciona
        dentro de un controlador previamente determinado
    */
    private Color _colorDefaultClose;
    private Color _colorDefaultMin;


    private GestionLogin _gestionLogin = new GestionLogin();


    public BtnIniciarSesion() {
      InitializeComponent();
    }



    // metodo necesario para gestionar el movimiento de la ventana
    private void BarraTitulo_MouseDown(object sender, MouseEventArgs e) {
      ReleaseCapture();
      SendMessage(this.Handle, 0x112, 0xf012, 0);
    }


    private void PbCerrar_Click(object sender, EventArgs e) {
      Application.Exit();
    }

    private void PictureBox3_Click(object sender, EventArgs e) {
      this.WindowState = FormWindowState.Minimized;
    }


    //Eventos que generan un efecto visual en cuanto el mouse pasa por dicho controlador
    private void PbCerrar_MouseEnter(object sender, EventArgs e) {
      _colorDefaultClose = pbCerrar.BackColor;
      pbCerrar.BackColor = Color.FromArgb(202, 49, 32);
    }
    protected void PbCerrar_MouseLeave(object sender, EventArgs e) {
      pbCerrar.BackColor = _colorDefaultClose;
    }

    protected void PbMinimizar_MouseEnter(object sender, EventArgs e) {
      _colorDefaultMin = pbMinimizar.BackColor;
      pbMinimizar.BackColor = Color.FromArgb(52, 58, 64);
    }

    private void PbMinimizar_MouseLeave(object sender, EventArgs e) {
      pbMinimizar.BackColor = _colorDefaultMin;
    }

    //Evento que abre el formulario luego de un inicio de sesión exitoso
    private void Button1_Click(object sender, EventArgs e) {
      string usuario = txtUsuario.Text;
      string password = txtPassword.Text;

      if(string.IsNullOrEmpty(usuario) || (string.IsNullOrEmpty(password))) {
        ValidarCamposVaciosLogin();
      } else {
        //metodo que envia el usuario y contraseña ingrsado para validar si los datos fueron ingresados correctamente
        string mensaje = "";
        mensaje = _gestionLogin.ControlLogin(usuario, password);

        RedirigirUsuario(mensaje, usuario, password);
      }
    }
    private void BorrarCampos() {
      txtUsuario.Text = "";
      txtPassword.Text = "";

    }
    //metodo que valida si existen campos vacios en la GUI
    private void ValidarCamposVaciosLogin() {
      string usuario = txtUsuario.Text;
      string password = txtPassword.Text;

      if(string.IsNullOrEmpty(usuario)) {
        errorProvider1.SetError(txtUsuario, "Debe ingresar un usuario");
      }
      if(string.IsNullOrEmpty(password)) {
        errorProvider1.SetError(txtPassword, "Debe ingresar una contraseña");
      }
    }

    private void AccesoUser(string mensaje) {
      if(mensaje == "presidente") {
        this.Hide();
        MenuPrincipal pantallaPrincipal = new MenuPrincipal();
        pantallaPrincipal.Show();
      } else if(mensaje == "arbitro") {
        this.Hide();
        FrmMenuArbitro pantallaPrincipalArbitro = new FrmMenuArbitro();
        pantallaPrincipalArbitro.Show();
      }
    }

    /// <summary>
    /// Metodo encargado de controlar si se debe cambiar contraseña o no
    /// </summary>
    /// <param name="tuvoUltimoAcceso">ultimo acceso del usuario</param>
    /// <param name="mensaje">recibe el rol del usuario que intenta loggearse</param>
    /// <param name="usuario">User name dle usuario</param>
    /// <param name="password">pasword del usuario</param>
    private void ControlarUltimoAcceso(bool tuvoUltimoAcceso, string mensaje, string usuario, string password) {
      if(tuvoUltimoAcceso) {
        AccesoUser(mensaje);
      } else {
        int id = _gestionLogin.ObtenerId(usuario, password);
        this.Hide();
        CambiarPass cambiar = new CambiarPass(id);
        cambiar.ShowDialog();
      }
    }

    private void RedirigirUsuario(string mensaje, string usuario, string password) {

      if(mensaje.StartsWith("ERROR")) {
        MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        BorrarCampos();
      } else {

        bool ultimoAcceso = _gestionLogin.ValidarUltimoAcceso(usuario, password);
        ControlarUltimoAcceso(ultimoAcceso, mensaje, usuario, password);
      }
    }
  }
}