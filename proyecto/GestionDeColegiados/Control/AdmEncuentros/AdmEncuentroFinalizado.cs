using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Control.AdmEncuentrosGenerados;
using Control.AdmEquipos;

using Data;

using Model;
using Model.Equipo;
using Model.Partido;

namespace Control.AdmEncuentros {
  public class AdmEncuentroFinalizado {

    private static AdmEncuentroFinalizado _admEncuentroFinalizado = null;
    private List<EncuentroFinalizado> _encuentrosFinalizados;
    private AdmEncuentrosDefinidos _admEncuentrosDefinidos = AdmEncuentrosDefinidos.GetAdmGenerarEncuentrosDefinidos();
    private AdmGenerarEncuentros _admEncuentrosGenerados = AdmGenerarEncuentros.GetAdmadmGenerarEncuentros();
    private AdmEquipo _admEquipos = AdmEquipo.GetEquipo();
    private ValidacionGUI _validaciones = new ValidacionGUI();
    private DatosEncuentroFinalizado _datosEncuentroFinalizado = new DatosEncuentroFinalizado();

    private AdmEncuentroFinalizado() {
      _encuentrosFinalizados = new List<EncuentroFinalizado>();
    }

    public static AdmEncuentroFinalizado GetAdmEncuentrosFinalizados() {
      if(_admEncuentroFinalizado == null) {

        _admEncuentroFinalizado = new AdmEncuentroFinalizado();
      }
      return _admEncuentroFinalizado;
    }
    /// <summary>
    /// Usado para finalizar una competencia
    /// </summary>
    /// <returns> Un booleano que indica si la acción se completo con exito o no</returns>
    public bool FinalizarCompetencia() {
      bool respuesta = false;

      //respuesta = DarBajaCompetencia();
      string anio = DateTime.Now.Year.ToString();
      respuesta = _datosEncuentroFinalizado.FinalizarCompetencia(anio, "F");
      return respuesta;
    }
    /// <summary>
    /// Usado para finalizar la competencia
    /// </summary>
    /// <returns>Un booleano que indica si la acción se completo con exito o no</returns>
    public bool ReinicarCompetencia() {
      bool respuesta = false;
      string anio = DateTime.Now.Year.ToString();
      int cantidad = _datosEncuentroFinalizado.GetCantidadEncuentrosFinalizados(anio);
      if(cantidad > 0) {

        respuesta = _datosEncuentroFinalizado.FinalizarCompetencia(anio, "R");
      } else if(cantidad == 0) {

        respuesta = true;
      } else if(cantidad == -1) {

        respuesta = false;
      }
      return respuesta;
    }
    /// <summary>
    /// Metodo usado para dar baja a la competencia
    /// </summary>
    /// <remarks>Se necesita existir encuentros finalizados para poder realizarse</remarks>
    /// <returns>Un booleano que indica si la acción se completo con exito o no</returns>
    public bool DarBajaCompetencia() {
      bool respuesta = false;
      string anio = DateTime.Now.Year.ToString();
      int cantidad = _datosEncuentroFinalizado.GetCantidadEncuentrosFinalizados(anio);
      if(cantidad > 0) {

        respuesta = _datosEncuentroFinalizado.FinalizarCompetencia(anio, "N");
      } else if(cantidad == 0) {

        respuesta = true;
      } else if(cantidad == -1) {

        respuesta = false;
      }
      return respuesta;
    }
    /// <summary>
    /// Se encarga de llenar la tabla de posiciones de los resultados de encuentros finalizados
    /// </summary>
    /// <param name="dgvCompeticion">DataGridView encargado de mostrar una simulada tabla de posiciones</param>
    /// <returns>un booleano en respuesta de si se logró llenar con exito la tabla</returns>
    public bool LlenarDgv(DataGridView dgvCompeticion) {
      bool respuesta = false;
      string anio = "" + DateTime.Now.Year;
      _encuentrosFinalizados = _datosEncuentroFinalizado.GetEncuentrosFinalizados(anio);
      if(_encuentrosFinalizados.Count > 0) {

        int posicion = 1;
        foreach(EncuentroFinalizado encuentro in _encuentrosFinalizados) {
          string nombreEquipo = _admEquipos.ObtenerEquipoPorId(encuentro.IdEquipo).NombreEquipo;
          dgvCompeticion.Rows.Add(posicion, nombreEquipo, encuentro.GolesFavor, encuentro.GolesContra, encuentro.GolesDiferencia, encuentro.Puntos);
          posicion++;
        }
        respuesta = true;
      }
      return respuesta;
    }
    /// <summary>
    /// Encargado de calcular puntos de un equipo B
    /// </summary>
    /// <param name="puntos">Son los puntos que obtuvo el equipo A</param>
    /// <returns> devuelve los puntos del equipo B en relacion al equipo A</returns>
    private int CalcularPuntosVisitante(int puntos) {
      int puntosVisitante = 0;
      if(puntos == 0) {

        puntosVisitante = 3;
      } else if(puntos == 1) {
        puntosVisitante = 1;
      } else if(puntos == 3) {
        puntosVisitante = 0;
      }
      return puntosVisitante;
    }
    /// <summary>
    /// llena la información de encuentro finalizado seleccionado de un combobox
    /// </summary>
    /// <param name="index">La posicion en el combobox del cual se escogio el encuentro</param>
    /// <param name="txtGolesLocal">Conttrolador de UI encargado de pintar los Goles locales de un encuentro</param>
    /// <param name="txtGolesVisitante">Conttrolador de UI encargado de pintar los Golesde visitante de un encuentro</param>
    /// <param name="lblPuntosLocalResultado">Cantidad de puntos para el equipo local, acorde al resultado de goles</param>
    /// <param name="lblPuntosVisitanteResultado">Cantidad de puntos para el equipo visitante, acorde al resultado de goles</param>
    public void LlenarInformacionPartido(int index, TextBox txtGolesLocal, TextBox txtGolesVisitante, Label lblPuntosLocalResultado, Label lblPuntosVisitanteResultado) {
      EncuentroDefinido encuentroDefinido = _admEncuentrosDefinidos.GetEncuentroDefinidoByIndex(index);
      EncuentroGenerado encuentroGenerado = _admEncuentrosGenerados.ObtenerEncuentroPorID(encuentroDefinido.IdEncuentroGeneradoPendiente);
      EncuentroFinalizado encuentroFinalizado = _datosEncuentroFinalizado.GetEncuentroFinalizadoByIDefinidoEquipo(encuentroDefinido.Id, encuentroGenerado.IdEquipoLocal);
      txtGolesLocal.Text = encuentroFinalizado.GolesFavor.ToString();
      txtGolesVisitante.Text = encuentroFinalizado.GolesContra.ToString();
      int puntosLocal = encuentroFinalizado.Puntos;
      lblPuntosLocalResultado.Text = puntosLocal.ToString();
      lblPuntosVisitanteResultado.Text = CalcularPuntosVisitante(puntosLocal).ToString();
    }
    /// <summary>
    /// Crea un encuentro finalizado
    /// </summary>
    /// <param name="idEquipo">Id del equipo ganador del encuentro</param>
    /// <param name="idDefinido">Id del encuentro definido</param>
    /// <param name="golesFavor">Goles a favor del equipo</param>
    /// <param name="golesContra">Goles en contra del equipo</param>
    /// <returns>Regresa un Encuentro finalizado, acorde a los datos recibidos</returns>
    private EncuentroFinalizado GetEncuentro(int idEquipo, int idDefinido, string golesFavor, string golesContra) {

      Equipo equipo = _admEquipos.ObtenerEquipoPorId(idEquipo);

      int golAFavor = _validaciones.AInt(golesFavor);
      int golEnContra = _validaciones.AInt(golesContra);
      return new EncuentroFinalizado(equipo.IdEquipo, golAFavor, golEnContra, idDefinido);

    }
    /// <summary>
    /// Metdodo utilizado para actualizar un encuentro Finalizado
    /// </summary>
    /// <param name="index">la posicion del encuentro finalizado respecto a una lista</param>
    /// <param name="golesLocal">cantidad de goles de local que se han actualizado</param>
    /// <param name="golesVisitante">cantidad de goles de visitante que se registran</param>
    /// <returns>regresa un booleano que hace referencia a si la acción se logró con exito o no</returns>
    public bool UpdateEncuentroFinalizado(int index, string golesLocal, string golesVisitante) {
      bool respuesta = false;

      EncuentroDefinido encuentroDefinido = _admEncuentrosDefinidos.ListaEncuentrosDefinidos[index];
      EncuentroGenerado encuentroGenerado = _admEncuentrosGenerados.ObtenerEncuentroPorID(encuentroDefinido.IdEncuentroGeneradoPendiente);


      EncuentroFinalizado encuentroResultadoLocal = GetEncuentro(encuentroGenerado.IdEquipoLocal, encuentroDefinido.Id, golesLocal, golesVisitante);
      EncuentroFinalizado encuentroResultadoVisitante = GetEncuentro(encuentroGenerado.IdEquipoVisitante, encuentroDefinido.Id, golesVisitante, golesLocal);

      respuesta = _datosEncuentroFinalizado.UpdateEncuentroFinalizado(encuentroResultadoLocal);
      if(respuesta) {

        respuesta = _datosEncuentroFinalizado.UpdateEncuentroFinalizado(encuentroResultadoVisitante);
      }
      return respuesta;
    }

    /// <summary>
    /// Metodo encargado de guardar un encuentro finalizado
    /// </summary>
    /// <param name="index">la posicion del encuentro finalizado respecto a una lista</param>
    /// <param name="golesLocal">cantidad de goles de local que se han actualizado</param>
    /// <param name="golesVisitante">cantidad de goles de visitante que se registran</param>
    /// <returns>regresa un booleano que hace referencia a si la acción se logró con exito o no</returns>
    public bool GuardarEncuentroFinalizado(int index, string golesLocal, string golesVisitante) {
      bool guardado = true;

      EncuentroDefinido encuentroDefinido = _admEncuentrosDefinidos.ListaEncuentrosDefinidos[index];
      EncuentroGenerado encuentroGenerado = _admEncuentrosGenerados.ObtenerEncuentroPorID(encuentroDefinido.IdEncuentroGeneradoPendiente);

      Equipo equipoLocal = _admEquipos.ObtenerEquipoPorId(encuentroGenerado.IdEquipoLocal);
      Equipo equipoVisitante = _admEquipos.ObtenerEquipoPorId(encuentroGenerado.IdEquipoVisitante);

      int golLocal = _validaciones.AInt(golesLocal);
      int golVisitante = _validaciones.AInt(golesVisitante);

      EncuentroFinalizado encuentroResultadoLocal = new EncuentroFinalizado(equipoLocal.IdEquipo, golLocal, golVisitante, encuentroDefinido.Id);
      EncuentroFinalizado encuentroResultadoVisitante = new EncuentroFinalizado(equipoVisitante.IdEquipo, golVisitante, golLocal, encuentroDefinido.Id);

      bool guardo = _datosEncuentroFinalizado.AddEncuentroFinalizado(encuentroResultadoLocal);
      if(guardo) {

        guardado = _datosEncuentroFinalizado.AddEncuentroFinalizado(encuentroResultadoVisitante);
      }

      return guardado;
    }

    public void CalcularPuntos(Label lblPuntos, string golFavor, string golContra) {
      if(!String.IsNullOrEmpty(golFavor) && !String.IsNullOrEmpty(golContra)) {

        int gFavor = _validaciones.AInt(golFavor);
        int gContra = _validaciones.AInt(golContra);
        lblPuntos.Text = "" + Puntos(gFavor, gContra);
      }
    }
    private int Puntos(int golFavor, int golContra) {
      int respuesta = 0;
      if(golContra == golFavor) {

        respuesta = 1;
      } else if(golFavor > golContra) {

        respuesta = 3;
      } else if(golFavor < golContra) {

        respuesta = 0;
      }
      return respuesta;
    }

    public int GetCantidadEncuentrosFinalizados() {
      string anio = DateTime.Now.Year.ToString();
      int cantidad = _datosEncuentroFinalizado.GetCantidadEncuentrosFinalizados(anio);
      if(cantidad < 0) { }
      return cantidad;
    }
  }
}