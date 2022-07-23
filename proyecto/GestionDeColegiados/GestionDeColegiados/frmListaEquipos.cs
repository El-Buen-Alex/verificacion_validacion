using System.Collections.Generic;
using System.Windows.Forms;

using Control.AdmEquipos;

namespace GestionDeColegiados {
  public partial class FrmListaEquipos : Form {
    private List<Label> _listaContenedores = new List<Label>();
    private AdmEquipo _admEquipo = AdmEquipo.GetEquipo();
    public FrmListaEquipos() {
      InitializeComponent();
      /*se llenan los labels a una lista 
       * para poder gestionar el conjunto de ellos, al mostrar
       * los equipo
      */
      _listaContenedores.Add(lblEquipo1);
      _listaContenedores.Add(lblEquipo2);
      _listaContenedores.Add(lblEquipo3);
      _listaContenedores.Add(lblEquipo4);
      _listaContenedores.Add(lblEquipo5);
      _listaContenedores.Add(lblEquipo6);
      _listaContenedores.Add(lblEquipo7);
      _listaContenedores.Add(lblEquipo8);
      _listaContenedores.Add(lblEquipo9);
      _listaContenedores.Add(lblEquipo10);
      //llenamos los equipos en los labls
      _admEquipo.LlenarEquipos(_listaContenedores);
    }
  }
}
