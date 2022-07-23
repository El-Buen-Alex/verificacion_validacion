using System;
using System.Windows.Forms;

using Control;

namespace GestionDeColegiados {
  public partial class CambiarPass : Form {
    private int idUser;
    private GestionLogin gestionLogin = new GestionLogin();
    public CambiarPass(int idUsuraio) {
      idUser = idUsuraio;
      InitializeComponent();

    }

    private void _pictureBox5_Click(object sender, EventArgs e) {
      this.WindowState = FormWindowState.Minimized;
    }

    private void _pictureBox6_Click(object sender, EventArgs e) {
      Application.Exit();
    }

    private void _btnChangePass_Click(object sender, EventArgs e) {
      string newPass = txtPass.Text.Trim();
      string cambiar = gestionLogin.CambiarPass(newPass, this.idUser);
      if(cambiar.StartsWith("EXITO")) {
        this.Close();
        btnIniciarSesion iniciar = new btnIniciarSesion();
        iniciar.Show();

      } else {
        MessageBox.Show(cambiar, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    }
    private void _habilitarButton() {
      string newPass = txtPass.Text.Trim();
      string confPass = textRepeatPass.Text.Trim();

      if(!string.IsNullOrEmpty(newPass) && !string.IsNullOrEmpty(confPass)) {
        if(newPass == confPass) {
          _controlarVisibilidad(true, false);
        } else {
          _controlarVisibilidad(false, true);
        }
      } else {
        btnChangePass.Enabled = false;
      }

    }

    private void _controlarVisibilidad(bool estadoBtn, bool estadoLbl) {
      btnChangePass.Enabled = estadoBtn;
      lblAviso.Visible = estadoLbl;
    }

    private void _textRepeatPass_TextChanged(object sender, EventArgs e) {
      _habilitarButton();

    }

  }
}
