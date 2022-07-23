using System;
using System.Windows.Forms;

using Control;
using Control.AdmEquipos;

namespace GestionDeColegiados {
  public partial class FrmEditarEquipo : Form {
    private AdmEquipo _equipo = AdmEquipo.GetEquipo();
    private ValidacionGUI _valida = new ValidacionGUI();
    public FrmEditarEquipo(String id) {
      InitializeComponent();
      _equipo.LlenarCampos(idEquipo, nombre, numjugadores, director, presidente, id);
    }

    private void BtnCancelar_Click(object sender, EventArgs e) {
      DialogResult resultado;
      resultado = MessageBox.Show("¡Está seguro de cancelar!", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
      if(resultado == DialogResult.Yes) {

        Close();
      }
    }

    private void BtnActualizar_Click(object sender, EventArgs e) {
      String Nombre = nombre.Text.Trim(),
         numJugadores = numjugadores.Text,
         directorNombre = director.Text,
         presidenteNombre = presidente.Text,
         id = idEquipo.Text;

      bool hayVacios = _valida.validarVacios(Nombre, numJugadores, directorNombre, presidenteNombre);   //Valida campos vacios al recuperar la informacion presente en los TextBox
      if(hayVacios != true) {

        MessageBox.Show(Nombre + ", " + numJugadores + ", " + directorNombre + ", " + presidenteNombre);
        _equipo.ActualizarDatos(_valida.AInt(id), Nombre, _valida.AInt(numJugadores), directorNombre, presidenteNombre);       /*Se ejecuta el método que nos permitirá Guardar la información*/
      } else {
        MessageBox.Show("Hay ciertos campos vacios");
      }

    }

    private void BtnRegresar_Click(object sender, EventArgs e) {
      DialogResult resultado;


      bool hayVacios = _valida.validarVacios(nombre.Text.Trim(), numjugadores.Text, director.Text, presidente.Text);
      if(hayVacios != true) {

        resultado = MessageBox.Show("¡Está seguro de regresar al apartado anterior, \n Si no ha dado clic en el botón Actualizar se perderán sus datos!", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
        if(resultado == DialogResult.Yes) {

          Close();
        }
      } else {
        resultado = MessageBox.Show("¡Está seguro de regresar al apartado anterior, \n Hay ciertor campos vacíos!", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
        if(resultado == DialogResult.Yes) {

          Close();
        }
      }


    }
  }
}
