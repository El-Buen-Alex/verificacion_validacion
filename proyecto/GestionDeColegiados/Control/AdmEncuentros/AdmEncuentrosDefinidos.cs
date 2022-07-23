using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Control.AdmColegiados;
using Control.AdmEquipos;
using Control.AdmEstadios;

using Data;

using Model;
using Model.Equipo;

namespace Control.AdmEncuentrosGenerados {
  public class AdmEncuentrosDefinidos {
    private static AdmEncuentrosDefinidos _admGenerarEncuentrosDefinidos = null;
    private List<EncuentroDefinido> _listaEncuentrosDefinidos;
    private List<EncuentroGenerado> _listaEncuentrosGenerados = new List<EncuentroGenerado>();
    private DatosEncuentroDefinido _datosEncuentroDefinido = new DatosEncuentroDefinido();
    private AdmGenerarEncuentros _admEncuentrosGenerados = AdmGenerarEncuentros.getAdmadmGenerarEncuentros();
    private AdmColegiado _admColegiados = AdmColegiado.getAdmCol();


    private AdmEquipo _admEquipos = AdmEquipo.getEquipo();
    private AdmEstadio _admEstadios = AdmEstadio.GetAdmEstadio();
    private AdmEstadio _admEstadio = AdmEstadio.GetAdmEstadio();

    private AdmEncuentrosDefinidos() {
      _listaEncuentrosDefinidos = new List<EncuentroDefinido>();
    }

    public List<EncuentroDefinido> ListaEncuentrosDefinidos { get => _listaEncuentrosDefinidos; set => _listaEncuentrosDefinidos = value; }

    public static AdmEncuentrosDefinidos GetAdmGenerarEncuentrosDefinidos() {
      if(_admGenerarEncuentrosDefinidos == null) {

        _admGenerarEncuentrosDefinidos = new AdmEncuentrosDefinidos();
      }

      return _admGenerarEncuentrosDefinidos;
    }



    private void LlenarCmbMatch(ComboBox cmbEncuentros) {
      EncuentroGenerado encuentroGenerado;
      Equipo local;
      Equipo visitante;
      Estadio estadio;
      for(int x = 0; x < _listaEncuentrosDefinidos.Count; x++) {
        encuentroGenerado = _admEncuentrosGenerados.ObtenerEncuentroPorID(_listaEncuentrosDefinidos[x].IdEncuentroGeneradoPendiente);
        local = _admEquipos.ObtenerEquipoPorId(encuentroGenerado.IdEquipoLocal);
        visitante = _admEquipos.ObtenerEquipoPorId(encuentroGenerado.IdEquipoVisitante);
        estadio = _admEstadios.ObtenerEstadioPorId(_listaEncuentrosDefinidos[x].IdEstadio);
        cmbEncuentros.Items.Add(x + 1 + ":" + local.NombreEquipo + " VS " + visitante.NombreEquipo + "- " + estadio.Nombre);
        _listaEncuentrosGenerados.Add(encuentroGenerado);
      }

    }

    public bool DarBajaEncuentrosDefinidos() {
      bool respuesta = false;
      string anio = DateTime.Now.Year.ToString();
      respuesta = _datosEncuentroDefinido.CambiarEstadoEnucentroDefinido("N");
      return respuesta;
    }

    public EncuentroDefinido GetEncuentroDefinidoByIndex(int index) {
      return this.ListaEncuentrosDefinidos[index];
    }

    public bool LlenarCmbEncuentrosDefinidosFinalizados(ComboBox cmbEncuentros) {
      bool resultado = false;
      string anio = DateTime.Now.Year.ToString();
      cmbEncuentros.Items.Clear();
      _listaEncuentrosDefinidos = _datosEncuentroDefinido.GetEncuentrosDefinidosFinalizados(anio);
      if(_listaEncuentrosDefinidos.Count > 0) {
        LlenarCmbMatch(cmbEncuentros);
        resultado = true;
      }

      return resultado;
    }

    public bool ReinicarCompetencia() {
      bool respuesta = _datosEncuentroDefinido.CambiarEstadoEnucentroDefinido("R");
      return respuesta;
    }

    /*metodo usado para llenar encuentros en un combobox
se recupera los encuentros definidos, el nombre de equipo local y visitante
ademas del estadio*/
    public void LlenarPartidosCmb(ComboBox cmbEncuentros) {
      cmbEncuentros.Items.Clear();
      _listaEncuentrosDefinidos = _datosEncuentroDefinido.ObtenerEncuentros();
      LlenarCmbMatch(cmbEncuentros);
    }

    public int ObtenerCantidadEncuentrosDefinidos() {
      return _datosEncuentroDefinido.ObtenerCantidadEncuentrosDefinidos();
    }

    /*metodo para pedirle a AdmEstadio que nos devuelva el nombre de un estadio a través del id*/
    public string ObtenerNombreEstadioDelPartido(int indexEncuentroDefinidoSeleccionado) {
      Estadio estadio = _admEstadio.ObtenerEstadioPorId(_listaEncuentrosDefinidos[indexEncuentroDefinidoSeleccionado].IdEstadio);
      return estadio.Nombre;
    }
    /*metodo para pedirle a EncuentroGenerado que nos devuelva el nombre de un Encuentro Generado a través del id*/
    private EncuentroGenerado ObtenerEncuentroGenerado(int id) {
      return _admEncuentrosGenerados.ObtenerEncuentroPorID(id);
    }



    public void LlenarMatchDefinidosFinalizados(int index, 
                                                Label lblEquipoLocal, 
                                                Label lblEquipoVisitante) {
      string anio = DateTime.Now.Year.ToString();
      _listaEncuentrosDefinidos = _datosEncuentroDefinido.GetEncuentrosDefinidosFinalizados(anio);
      LlenarMatchEnParLabel(index, lblEquipoLocal, lblEquipoVisitante);
    }
    private void LlenarMatchEnParLabel(int indexEncuentroSeleccionado, 
                                       Label lblEquipoLocal, Label lblEquipoVisitante) {
      EncuentroDefinido encuentroDefinido = _listaEncuentrosDefinidos[indexEncuentroSeleccionado];
      LlenarDatosPartido(indexEncuentroSeleccionado, lblEquipoLocal, lblEquipoVisitante, encuentroDefinido);
    }

    /*meotdo usado para llenar la informacion completa de un encuentro 
     *obteniendo el nombre de los equipos 
     *estadio
     *fecha
     *y grupo de colegiados
     */
    public void LlenarMatch(int indexEncuentroSeleccionado, 
                            Label lblEquipoLocal, 
                            Label lblEquipoVisitante) {
      _listaEncuentrosDefinidos = _datosEncuentroDefinido.ObtenerEncuentros();
      EncuentroDefinido encuentroDefinido = _listaEncuentrosDefinidos[indexEncuentroSeleccionado];
      LlenarDatosPartido(indexEncuentroSeleccionado, lblEquipoLocal, lblEquipoVisitante, encuentroDefinido);
    }
    private void LlenarDatosPartido(int indexEncuentroSeleccionado, 
                                    Label lblEquipoLocal, 
                                    Label lblEquipoVisitante, 
                                    EncuentroDefinido encuentroDefinido) {
      EncuentroGenerado encuentroGenerado = ObtenerEncuentroGenerado(encuentroDefinido.IdEncuentroGeneradoPendiente);
      Equipo local;
      Equipo visitante;
      local = _admEquipos.ObtenerEquipoPorId(encuentroGenerado.IdEquipoLocal);
      visitante = _admEquipos.ObtenerEquipoPorId(encuentroGenerado.IdEquipoVisitante);
      lblEquipoLocal.Text = local.NombreEquipo;
      lblEquipoVisitante.Text = visitante.NombreEquipo;
    }

    private void LlenarFechaHoraPartido(Label lblFecha, 
                                        EncuentroDefinido encuentroDefinido) {
      lblFecha.Text = "FECHA: " + encuentroDefinido.FechaDeEncuentro.ToShortDateString() + " HORA: " + encuentroDefinido.Hora.ToShortTimeString();
    }

    /// <summary>
    /// Llena la informacion de un encuentro definido, para visualizar
    /// </summary>
    /// <param name="indexEncuentroSeleccionado">Ubicacion del encuentro definido acorde a una lista</param>
    /// <param name="lblEquipoLocal">Controlador ui encargado de pintar el nombre del equipo local</param>
    /// <param name="lblEquipoVisitante">Controlador ui encargado de pintar el nombre del equipo visitante</param>
    /// <param name="lblEstadio"> Controlador ui encargado de pintar el nombre del estadio</param>
    /// <param name="lblFecha">Controlador ui encargado de pintar la fecha asignada</param>
    /// <param name="lblColegiado">Controlador ui encargado de pintar el colegiado encargado del partido</param>
    public void LlenarInformacíonPartidoCompleta(
      int indexEncuentroSeleccionado, Label lblEquipoLocal, Label lblEquipoVisitante, 
      Label lblEstadio, Label lblFecha, Label lblColegiado) {
      _listaEncuentrosDefinidos = _datosEncuentroDefinido.ObtenerEncuentros();
      EncuentroDefinido encuentroDefinido = _listaEncuentrosDefinidos[indexEncuentroSeleccionado];
      LlenarDatosPartido(
        indexEncuentroSeleccionado, lblEquipoLocal, 
        lblEquipoVisitante, encuentroDefinido);
      LlenarFechaHoraPartido(lblFecha, encuentroDefinido);
      lblEstadio.Text = ObtenerNombreEstadioDelPartido(indexEncuentroSeleccionado);
      lblColegiado.Text = _admColegiados.ObtenerNombreDeColegiados(encuentroDefinido.IdColegiado);
    }
    public int ObtenerNumeroPartidosPorJugar() {
      return _datosEncuentroDefinido.ObtenerCantidadEncuentrosPorJugar();
    }

    /// <summary>
    /// Llena informacion de un encuentro definido para poder actualizarlo
    /// </summary>
    /// <param name="indexEncuentroSeleccionado">Ubicacion del encuentro definido acorde a una lista</param>
    /// <param name="lblEquipoLocal">Controlador ui encargado de pintar el nombre del equipo local</param>
    /// <param name="lblEquipoVisitante">Controlador ui encargado de pintar el nombre del equipo visitante</param>
    /// <param name="cmbEstadios">Controlador ui encargado de llenar los estadios disponibles y seleccionar el correspodiente al encuentro</param>
    /// <param name="dtpFechaEncuentro">Controlador ui encargado de llenar la fecha del encuentro</param>
    /// <param name="dtpHora">Controlador ui encargado de llenar la hora del encuentro</param>
    /// <param name="cmbGrupoColegiado">Controlador ui encargado de llenar los colegiados y seleccionar el correspodiente al encuentro</param>
    /// <param name="txtColegiado">Se llena la informacion del grupo de colegiados</param>
    /// <returns></returns>
    public bool LlenarInformacíonPartidoCompleta(
      int indexEncuentroSeleccionado, Label lblEquipoLocal, 
      Label lblEquipoVisitante, ComboBox cmbEstadios, 
      DateTimePicker dtpFechaEncuentro, DateTimePicker dtpHora, 
      ComboBox cmbGrupoColegiado, TextBox txtColegiado) {

      bool respuesta = false;
      try {
        _listaEncuentrosDefinidos = _datosEncuentroDefinido.ObtenerEncuentros();
        EncuentroDefinido encuentroDefinido = _listaEncuentrosDefinidos[indexEncuentroSeleccionado];
        EncuentroGenerado encuentroGenerado = ObtenerEncuentroGenerado(encuentroDefinido.IdEncuentroGeneradoPendiente);
        LlenarDatosPartido(indexEncuentroSeleccionado, lblEquipoLocal, lblEquipoVisitante, encuentroDefinido);
        Estadio estadio = _admEstadio.ObtenerEstadioPorId(_listaEncuentrosDefinidos[indexEncuentroSeleccionado].IdEstadio);
        _admEstadio.SeleccionarEstadio(cmbEstadios, estadio);
        string colegiados = _admColegiados.ObtenerNombreDeColegiados(encuentroDefinido.IdColegiado);
        txtColegiado.Text = colegiados;
        _admColegiados.LlenarColegiadosCmb(cmbGrupoColegiado, encuentroDefinido.IdColegiado);
        dtpFechaEncuentro.Value = encuentroDefinido.FechaDeEncuentro;
        dtpHora.Value = encuentroDefinido.Hora;
        respuesta = true;
      } catch(Exception ex) {
        Console.WriteLine(ex.Message);
      }

      return respuesta;
    }
    public bool ActualizarEncuentroDefinido(
      int indexEncuentro, int indexEstadio, int indexColegiados, 
      DateTime fecha, DateTime hora) {
      bool actualizo = false;
      EncuentroDefinido auxiliar = _listaEncuentrosDefinidos[indexEncuentro];
      int idNuevoEstadioAsociado = _admEstadio.ListaEstadiosDisponibles[indexEstadio].Id;
      int nuevoColegiadoAsociado = _admColegiados.ListaintegColeg[indexColegiados].IdGrupoColegiado;
      actualizo = _datosEncuentroDefinido.ActualizarFechaHoraDEPartido(auxiliar.Id, fecha, hora, nuevoColegiadoAsociado);
      if(actualizo) {

        if(auxiliar.IdEstadio != idNuevoEstadioAsociado) {

          actualizo = ActualizarEstadio(auxiliar, idNuevoEstadioAsociado);
        }
      }

      return actualizo;
    }

    /*Metodo que actualiza el estadio de un encuentro definido
     */
    public bool ActualizarEstadio(EncuentroDefinido encuentroDefinido, int idNuevoEstadioAsociado) {
      bool actualizo = false;
      _listaEncuentrosDefinidos = _datosEncuentroDefinido.ObtenerEncuentros();
      int idEncuentroPorActualizar = encuentroDefinido.Id;
      int idAntiguoEStadio = encuentroDefinido.IdEstadio;
      try {
        actualizo = _datosEncuentroDefinido.ActualizarEstadioDePartido(idEncuentroPorActualizar, idNuevoEstadioAsociado);
        if(actualizo) {

          _admEstadio.CambiarEstadoEstadio(idNuevoEstadioAsociado, "OCUPADO");
          _admEstadio.CambiarEstadoEstadio(idAntiguoEStadio, "DISPONIBLE");
        } else {
          throw new ErrorActualizarEstadioException("En ActualizarEstadio-AdmEncuentrosDefinidos: ");
        }

      } catch(Exception ex) {
        Console.WriteLine(ex.Message);
      }

      return actualizo;
    }
    /*metodo que guarda un encuentro deifnido y a su vez cambia el estado
     de un encuentro generado
     y el estadio al que fue asignado*/
    public bool GuardarEncuentroDefinido(int grupoColegiado, DateTime fechaPartido, DateTime horaPartido, int estadioSeleccionado, int posicion) {
      bool guardo = false;
      _admEncuentrosGenerados.llenarListaEncuentrosGeneradosPendientes();
      _admColegiados.llenarListaColegiados();
      _admEstadio.RefrezcarListaEstadiosDisponibles();
      int idEncuentroGeneradoPendiente = _admEncuentrosGenerados.ListaEncuentrosGeneradosPendientes[posicion].Id;
      int idColegiado = _admColegiados.ListaintegColeg[grupoColegiado].IdGrupoColegiado;
      int idEsadio = _admEstadio.ListaEstadiosDisponibles[estadioSeleccionado].Id;
      EncuentroDefinido encuentroDefinido = new EncuentroDefinido(idEncuentroGeneradoPendiente, idColegiado, fechaPartido, horaPartido, idEsadio);
      int id = _datosEncuentroDefinido.GuardarEncuentroDefinido(encuentroDefinido);
      if(id != 0) {
        bool cambiarEstado = _admEncuentrosGenerados.CambiarEstadoEncuentro(idEncuentroGeneradoPendiente);
        if(cambiarEstado) {

          try {
            bool cambioEstadio = _admEstadio.CambiarEstadoEstadio(idEsadio, "OCUPADO");
            if(cambiarEstado) {

              guardo = true;
            }
          } catch(Exception ex) {
            guardo = false;
            Console.WriteLine(ex.Message);
          }

        }

      }

      return guardo;
    }
  }
}
