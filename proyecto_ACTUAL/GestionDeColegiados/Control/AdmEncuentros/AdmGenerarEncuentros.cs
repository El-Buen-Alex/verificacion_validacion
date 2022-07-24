using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using Control.AdmEquipos;

using Data;

using Model;
using Model.Equipo;

namespace Control.AdmEncuentrosGenerados {
  public class AdmGenerarEncuentros {
    private List<EncuentroGenerado> _listaEncuentrosGenerados;
    private List<EncuentroGenerado> _listaEncuentrosGeneradosPendientes;
    private EncuentroGenerado _encuentroAuxiliar = null;
    private static AdmGenerarEncuentros _admGenerarEncuentros = null;
    private AdmEquipo _admEquipo = AdmEquipo.GetEquipo();
    private List<Equipo> _listaEquipos;
    private DatosEnuenctrosGenerados _datosEncuentrosGenerados = new DatosEnuenctrosGenerados();
    private List<int> _idsEquiposLocales = new List<int>();
    private List<int> _idsEquiposVisitantes = new List<int>();
    private List<int> _numerosAleatoriosLocal = new List<int>();
    private List<int> _numerosAleatoriosVisitante = new List<int>();
    public List<EncuentroGenerado> ListaEncuentrosGenerados { get => _listaEncuentrosGenerados; set => _listaEncuentrosGenerados = value; }
    public List<EncuentroGenerado> ListaEncuentrosGeneradosPendientes { get => _listaEncuentrosGeneradosPendientes; set => _listaEncuentrosGeneradosPendientes = value; }

    /*metodo necesario para llenar tuplas(encuentro entre dos equipos)
     * con la información necesaria de cada encuentro
     */
    public void LlenarTuplas(Label lblEquipoLocal, Label lblEquipoVisitante, int posicion) {
      _listaEncuentrosGeneradosPendientes = _datosEncuentrosGenerados.ObtenerEncuentrosPendientes();
      int idEquipoLocal = _listaEncuentrosGeneradosPendientes[posicion].IdEquipoLocal;
      int idEquipoVisitante = _listaEncuentrosGeneradosPendientes[posicion].IdEquipoVisitante;
      Equipo equipoLocal = _admEquipo.ObtenerEquipoPorId(idEquipoLocal);
      Equipo equipoVisitante = _admEquipo.ObtenerEquipoPorId(idEquipoVisitante);
      lblEquipoLocal.Text = equipoLocal.NombreEquipo;
      lblEquipoVisitante.Text = equipoVisitante.NombreEquipo;
    }

    internal bool DarBajaEncuentrosGenerados() {
      throw new NotImplementedException();
    }

    /*Metodo para pedirle a la clase admEncuentrosGenerados que nos devuelva
* un encuentro generado a través del id del mismo
*/
    public EncuentroGenerado ObtenerEncuentroPorID(int idEncuentroGeneradoPendiente) {
      return _datosEncuentrosGenerados.ObtenerEncuentroPendiente(idEncuentroGeneradoPendiente);
    }
    /*Metodo para pedirle a la clase admEncuentrosGenerados que 
     * cambie el estado de un encuentro generado pendiente a por jugar
    */
    public bool CambiarEstadoEncuentro(int idEncuentroGeneradoPendiente) {
      return _datosEncuentrosGenerados.CambiarEstadoEncuentro(idEncuentroGeneradoPendiente);
    }

    public bool DarBajaEncuentros() {
      bool respuesta = _datosEncuentrosGenerados.CambiarEstadoEncuentros("N");
      return respuesta;
    }

    //constructor privado para ejecutar singleton
    private AdmGenerarEncuentros() {
      _listaEncuentrosGenerados = new List<EncuentroGenerado>();
    }
    //metodo necesario para ejecutar singleton
    public static AdmGenerarEncuentros GetAdmadmGenerarEncuentros() {
      if(_admGenerarEncuentros == null) {

        _admGenerarEncuentros = new AdmGenerarEncuentros();
      }
      return _admGenerarEncuentros;
    }
    /*Metodo necesario para pñoder generar encuentros aleatorios 
     * llenando dos listas de contenedores con nombres, uno de equipo local y otro de 
     * equipos visiatnte
     */
    public bool GenerarEncuentrosAleatorios(List<Label> listaContenedoresLocal, List<Label> listaContenedoresVisitante) {
      bool respuesta = false;
      _numerosAleatoriosLocal = GenerListaAleatoria(1, 6);
      _numerosAleatoriosVisitante = GenerListaAleatoria(6, 11);
      _listaEquipos = _admEquipo.ExtraerEquipos();
      //aqui se generan encuentros aleatorios y los ids se guardan acorde a su ubicacion
      //en su lista respectiva:idsequiposLocal idsEquipoVisitante
      try {
        bool generoNombreEquipoLocales = LlenarNombreEquipos(listaContenedoresLocal, _numerosAleatoriosLocal, _listaEquipos, _idsEquiposLocales);
        bool generoNombreEquipoVisitantes = LlenarNombreEquipos(listaContenedoresVisitante, _numerosAleatoriosVisitante, _listaEquipos, _idsEquiposVisitantes);
        if(!generoNombreEquipoVisitantes || !generoNombreEquipoLocales) {

          throw new GenerarEncuentrosException("Error en generarEncuentrosAleatorios");
        } else {
          respuesta = true;
        }
      } catch(Exception ex) {
        Console.WriteLine(ex.Message);
      }
      return respuesta;
    }
    public void LlenarListaEncuentrosGeneradosPendientes() {
      this._listaEncuentrosGeneradosPendientes = _datosEncuentrosGenerados.ObtenerEncuentrosPendientes();
    }
    /*Metodo donde se gestiona el llenar los nombres del encuentro de dos equipos
     * en sus contenedores respectivos
     */
    private bool LlenarNombreEquipos(List<Label> contenedores, List<int> listaAleatoria, List<Equipo> equipos, List<int> idsEquipos) {
      bool genero = false;
      int x = 0;
      foreach(int posicionAleatoria in listaAleatoria) {
        contenedores[x].Text = equipos[posicionAleatoria - 1].NombreEquipo;
        idsEquipos.Add(equipos[posicionAleatoria - 1].IdEquipo);
        x++;
        genero = true;
      }
      return genero;
    }

    public int ObtnerNumeroEncuentrosGeneradosPendientes() {
      int numeroEncuentros = 0;
      try {
        numeroEncuentros = _datosEncuentrosGenerados.ObetnerNumeroEncuentrosPendientes();
        if(numeroEncuentros == -1) {

          throw new GenerarEncuentrosException("Error en 'obtnerNumeroEncuentrosGeneradosPendientes()'");
        }
      } catch(Exception ex) {
        Console.WriteLine(ex.Message);
      }
      return numeroEncuentros;
    }
    /*Funcion necesaria para generar numeros aletorios en un rango definido y agregarlos
    * a una lista
    */
    private List<int> GenerListaAleatoria(int limiteInferior, int limiteSuperior) {
      List<int> listaAleatoria = new List<int>();
      int numeroGenerado = 0;
      while(listaAleatoria.Count() < 5) {
        numeroGenerado = new Random().Next(limiteInferior, limiteSuperior);
        if(!listaAleatoria.Contains(numeroGenerado)) {

          listaAleatoria.Add(numeroGenerado);
        }
      }
      return listaAleatoria;
    }
    /*Metodo para generar la lista de encuentros generados mediante la lista 
     de numeros aleatorios que se han generado previamente
    *asigna un equipo local dependiendo del id alteorio en idEquiposLocal
    *contra un equipo visitante dependiendo del id alteroio la lista idsEQuipoVisiatnte
    */
    private List<EncuentroGenerado> GenerarListaEncuentros() {
      List<EncuentroGenerado> lista = new List<EncuentroGenerado>();
      for(int x = 0; x < 5; x++) {
        _encuentroAuxiliar = new EncuentroGenerado(_idsEquiposLocales[x], _idsEquiposVisitantes[x]);
        lista.Add(_encuentroAuxiliar);
      }

      return lista;
    }
    /*metodo necesario para guardar encuentros aleaorias a través
     de una lista de encuentrosGenerados previamente instanciada
    con ayuda de las listas: idsEquipolocal y idsEquipovisitante que
    fueron creadas previamente*/
    public string GuardarEncuentrosAleatorios() {
      _listaEncuentrosGenerados = GenerarListaEncuentros();
      bool guardo = false;
      string mensaje = "";
      try {
        guardo = _datosEncuentrosGenerados.GuardarEncuentrosGenerados(_listaEncuentrosGenerados);
        if(guardo) {

          mensaje = "Se ha guardado con exito los encuentros generados";
        } else {
          mensaje = "No se logro guardar con exito. Intente nuevamente.";
          throw new GenerarEncuentrosException("Error en guardarEncuentrosAleatorios-AdmGenerarEncuentros");
        }
      } catch(Exception ex) {
        Console.WriteLine(ex.Message);
      }
      return mensaje;
    }
  }
}