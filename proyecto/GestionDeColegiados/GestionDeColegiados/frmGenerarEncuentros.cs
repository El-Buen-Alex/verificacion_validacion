using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Control.AdmEncuentrosGenerados;
using Control.AdmEquipos;

namespace GestionDeColegiados {
  public partial class frmGenerarEncuentros : Form {
    AdmEquipo _admEquipo = AdmEquipo.GetEquipo();
    AdmGenerarEncuentros _admGenerarEncuentros = AdmGenerarEncuentros.GetAdmadmGenerarEncuentros();
    private List<Label> _listaContenedoresLocal = new List<Label>();
    private List<Label> _listaContenedoresVisitante = new List<Label>();
    private List<PictureBox> _listaPictureBox = new List<PictureBox>();

    public List<Label> ListaContenedoresLocal { get => _listaContenedoresLocal; set => _listaContenedoresLocal = value; }
    public List<Label> ListaContenedoresVisitante { get => _listaContenedoresVisitante; set => _listaContenedoresVisitante = value; }


    /// <summary>
    /// Constructor del form de Generar encuentro
    /// </summary>
    /// <remarks>Si el parametro ingresado es falso, se puede generar, caso contrario se muestran los encuentros</remarks>
    /// <param name="estado">Define si los encuentros ya han sido generados o aún no</param>
    public frmGenerarEncuentros(bool estado) {
      InitializeComponent();
      InciarContenedores();
      AgregarPbaLista();
      CambiarAccesibilidadPictureBox(_listaPictureBox, false);
      CambiarAccesibilidad(_listaContenedoresLocal, false);
      CambiarAccesibilidad(_listaContenedoresVisitante, false);
      if(estado) {

        lblOrden.Visible = false;
        btnGenerarEncuentros.Visible = false;
        btnGuardarEncuentros.Visible = false;
        lblTitulo.Text = "ENCUENTROS PENDIENTES" +
            "\r\n DE FECHA Y COLEGIADOS";
        int limiteInferiorPb = 0, limiteSuperiorPb = 0;
        for(int x = 0; x < _admGenerarEncuentros.ObtnerNumeroEncuentrosGeneradosPendientes(); x++) {

          _admGenerarEncuentros.LlenarTuplas(_listaContenedoresLocal[x], _listaContenedoresVisitante[x], x);
          _listaContenedoresLocal[x].Visible = true;
          _listaContenedoresVisitante[x].Visible = true;
          limiteSuperiorPb = limiteInferiorPb + 3;
          ActivarPictureBox(limiteInferiorPb, limiteSuperiorPb);
          limiteInferiorPb += 3;
        }
      } else {
        btnGuardarEncuentros.Enabled = false;
      }

    }
    private void ActivarPictureBox(int LimiteInferior, int LimiteSuperior) {
      /*por cada encuentro pendiente se deben mostrar 3 picturebox de la lista 
      se añade el limite inferior desde donde se va a habilitar hasta doonde se va a 
      habilitar dichos picturebox*/
      for(int i = LimiteInferior; i < LimiteSuperior; i++) {
        _listaPictureBox[i].Visible = true;
      }
    }
    private void CambiarAccesibilidadPictureBox(List<PictureBox> listaContenedores, bool estado) {
      /*establecemos que todos los picturebox se mostraran en un estado
      que se pasará por parametro*/
      foreach(PictureBox pictureBox in listaContenedores) {
        pictureBox.Visible = estado;
      }

    }
    private void CambiarAccesibilidad(List<Label> listaContenedores, bool estado) {
      /*establecemos que todos los labels se mostraran en un estado
     que se pasará por parametro*/
      foreach(Label contenedor in listaContenedores) {
        contenedor.Visible = estado;
      }

    }
    private void AgregarPbaLista() {
      //añadimos los pciturebox a la lista
      _listaPictureBox.Add(pictureBox1);
      _listaPictureBox.Add(pictureBox2);
      _listaPictureBox.Add(pictureBox3);
      _listaPictureBox.Add(pictureBox4);
      _listaPictureBox.Add(pictureBox5);
      _listaPictureBox.Add(pictureBox6);
      _listaPictureBox.Add(pictureBox7);
      _listaPictureBox.Add(pictureBox8);
      _listaPictureBox.Add(pictureBox9);
      _listaPictureBox.Add(pictureBox10);
      _listaPictureBox.Add(pictureBox11);
      _listaPictureBox.Add(pictureBox12);
      _listaPictureBox.Add(pictureBox13);
      _listaPictureBox.Add(pictureBox14);
      _listaPictureBox.Add(pictureBox15);

    }
    private void InciarContenedores() {
      //añadimos los labels a la lista
      _listaContenedoresLocal.Add(lblEquipo1);
      _listaContenedoresLocal.Add(lblEquipo2);
      _listaContenedoresLocal.Add(lblEquipo3);
      _listaContenedoresLocal.Add(lblEquipo4);
      _listaContenedoresLocal.Add(lblEquipo5);
      _listaContenedoresVisitante.Add(lblEquipo6);
      _listaContenedoresVisitante.Add(lblEquipo7);
      _listaContenedoresVisitante.Add(lblEquipo8);
      _listaContenedoresVisitante.Add(lblEquipo9);
      _listaContenedoresVisitante.Add(lblEquipo10);
    }
    private void GenerarEncuentros_Click(object sender, EventArgs e) {
      //mostramos los label y picturebox necesarios para generar encuentros
      CambiarAccesibilidad(_listaContenedoresLocal, true);
      CambiarAccesibilidad(_listaContenedoresVisitante, true);
      CambiarAccesibilidadPictureBox(_listaPictureBox, true);
      bool genero = _admGenerarEncuentros.GenerarEncuentrosAleatorios(_listaContenedoresLocal, _listaContenedoresVisitante);
      //una vez generado los encuentros se activa la opcion de Guardar encuentros
      if(genero) {

        btnGuardarEncuentros.Enabled = true;
      } else {
        MessageBox.Show("No se logró generar Encuentros");
      }
    }

    private void GuardarDatos_Click(object sender, EventArgs e) {
      //se guarda los encuentos y retorna un mensaje respuesta a la acción de Guardar
      string guardo = _admGenerarEncuentros.GuardarEncuentrosAleatorios();
      MessageBox.Show(guardo);
      if(guardo[0] == 'S') {

        btnGenerarEncuentros.Enabled = false;
        btnGuardarEncuentros.Enabled = false;
        btnGenerarEncuentros.Visible = false;
        btnGuardarEncuentros.Visible = false;
      }
    }

  }
}
