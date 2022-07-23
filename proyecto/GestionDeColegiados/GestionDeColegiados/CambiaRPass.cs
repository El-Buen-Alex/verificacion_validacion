using System;
using System.Windows.Forms;

using Control;

namespace GestionDeColegiados {
  public partial class CambiarPass : Form {
    private int _idUser;
    private GestionLogin _gestionLogin = new GestionLogin();
    public CambiarPass(int idUsuraio) {
      _idUser = idUsuraio;
      InitializeComponent();

    }

    private void PictureBox5_Click(object sender, EventArgs e) {
      this.WindowState = FormWindowState.Minimized;
    }

    private void PictureBox6_Click(object sender, EventArgs e) {
      Application.Exit();
    }

    private void BtnChangePass_Click(object sender, EventArgs e) {
      string newPass = txtPass.Text.Trim();
      string cambiar = _gestionLogin.CambiarPass(newPass, this._idUser);
      if(cambiar.StartsWith("EXITO")) {

        this.Close();
        btnIniciarSesion iniciar = new btnIniciarSesion();
        iniciar.Show();

      } else {
        MessageBox.Show(cambiar, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    }
    private void HabilitarButton() {
      string newPass = txtPass.Text.Trim();
      string confPass = textRepeatPass.Text.Trim();

      if(!string.IsNullOrEmpty(newPass) && !string.IsNullOrEmpty(confPass)) {

        if(newPass == confPass) {

          ControlarVisibilidad(true, false);
        } else {
          ControlarVisibilidad(false, true);
        }
      } else {
        btnChangePass.Enabled = false;
      }

    }

    private void ControlarVisibilidad(bool estadoBtn, bool estadoLbl) {
      btnChangePass.Enabled = estadoBtn;
      lblAviso.Visible = estadoLbl;
    }

    private void TextRepeatPass_TextChanged(object sender, EventArgs e) {
      HabilitarButton();

    }

  }
}
