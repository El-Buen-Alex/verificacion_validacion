using System;
using System.Collections.Generic;
using System.Data;

using Model.Equipo;

using MySql.Data.MySqlClient;

using Sistema;
/*clase que conecta directamente con los procedimientos que están ejecutados en la base de datos*/
namespace Data {
  public class DatosEquipos {
    private MySqlConnection _conexion = null;
    private MySqlTransaction _trans = null;

    /// <summary>
    /// Método que permite obtener el nombre y el id de un equipos por identificador
    /// </summary>
    /// <param name="id">id del equipo que se quiere obtener la información</param>
    /// <returns>Devuelve un arreglo de equipos con la información necesaria</returns>
    public Equipo ObtenerEquipoPorId(int id) {
      Equipo equipo = null;
      _conexion = ConexionBD.GetConexion();
      _conexion.Open();
      try {
        MySqlCommand cmd = new MySqlCommand("obtenerEquipo", _conexion);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@_equipoID", id);
        MySqlDataReader reader = cmd.ExecuteReader();
        if(reader.Read()) {

          equipo = new Equipo();
          equipo.NombreEquipo = reader["nombre"].ToString();
          equipo.IdEquipo = Convert.ToInt32(reader["idequipo"].ToString());
        }
      } catch(MySqlException ex) {
        Console.WriteLine(ex.Message);
      }
      _conexion.Close();
      return equipo;
    }

    /// <summary>
    /// Método usado para extraer los datos completos de un equipo
    /// </summary>
    /// <returns>Devuelve una lista con los datos recuperdos desde la bases de datos</returns>
    public List<Equipo> ConsultarEquiposTabla() {
      List<Equipo> listaEquipo = new List<Equipo>();
      Equipo nombreEquipo = null;
      _conexion = ConexionBD.GetConexion();
      _conexion.Open();
      try {
        MySqlCommand comando = new MySqlCommand("obtenerDatosEquipos", _conexion);
        comando.CommandType = CommandType.StoredProcedure;
        MySqlDataReader reader = comando.ExecuteReader();
        while(reader.Read()) {
          nombreEquipo = new Equipo();
          nombreEquipo.NombreEquipo = reader["nombre"].ToString();
          nombreEquipo.IdEquipo = Convert.ToInt32(reader["idequipo"].ToString());
          nombreEquipo.NumeroJugadores = Convert.ToInt32(reader["numero_jugadores"].ToString());
          nombreEquipo.NombreDirectoTecnico = reader["nombre_director_tecnico"].ToString();
          nombreEquipo.PresidenteEquipo = reader["presidente_equipo"].ToString();
          listaEquipo.Add(nombreEquipo);
        }
      } catch(MySqlException ex) {
        listaEquipo = null;
        throw new Exception(ex.ToString());
      }
      _conexion.Close();
      return listaEquipo;
    }

    /// <summary>
    /// Método que permite eliminar un equipo, en este caso cambiar el estado de un equipo
    /// </summary>
    /// <param name="id">El parámetro es usado para actualizar el estado del equipo</param>
    /// <returns>Se regresa una variable entera que funciona como bandera en el caso de ser éxitoso el cambio</returns>
    public int EliminarEquipo(string id) {
      int identificador = 0;
      _conexion = ConexionBD.GetConexion();
      _conexion.Open();
      _trans = _conexion.BeginTransaction();
      try {
        MySqlCommand cmd = new MySqlCommand("eliminarEquipo", _conexion, _trans);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@_idEquipo", id);
        cmd.ExecuteNonQuery();
        identificador = 1;
        _trans.Commit();
      } catch(MySqlException ex) {
        _trans.Rollback();
        throw new Exception(ex.ToString());
      }
      _conexion.Close();
      return identificador;
    }


    /// <summary>
    /// Método que permite editar un equipo
    /// </summary>
    /// <param name="equipo">objeto con los datos necesarios de un equipo</param>
    /// <returns>Se regresa una variable entera que funciona como bandera en el caso de ser éxitoso el cambio</returns>
    public int EditarEquipo(Equipo equipo) {
      int id = 0;
      _conexion = ConexionBD.GetConexion();
      _conexion.Open();
      _trans = _conexion.BeginTransaction();
      try {
        MySqlCommand cmd = new MySqlCommand("editarEquipo", _conexion, _trans);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@_idEquipo", equipo.IdEquipo);
        cmd.Parameters.AddWithValue("@_nombre", equipo.NombreEquipo);
        cmd.Parameters.AddWithValue("@_numero_jugadores", equipo.NumeroJugadores);
        cmd.Parameters.AddWithValue("@_nombre_director_tecnico", equipo.NombreDirectoTecnico);
        cmd.Parameters.AddWithValue("@_presidente_equipo", equipo.PresidenteEquipo);
        cmd.ExecuteNonQuery();
        id = 1;
        _trans.Commit();
      } catch(MySqlException ex) {
        _trans.Rollback();
        throw new Exception(ex.ToString());
      }
      _conexion.Close();
      return id;
    }

    /// <summary>
    /// Método que permite insertar un equipo a la vez en la base de datos
    /// </summary>
    /// <param name="equipo">Objeto que contiene la información necesaria para almacenar un equipo</param>
    /// <returns>Regresa el id del equipo que se ingresó</returns>
    public int InsertarEquipo(Equipo equipo) {
      int id = 0;
      _conexion = ConexionBD.GetConexion();
      _conexion.Open();
      _trans = _conexion.BeginTransaction();
      try {
        MySqlCommand cmd = new MySqlCommand("guardarEquipo", _conexion, _trans);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@_nombre", equipo.NombreEquipo);
        cmd.Parameters.AddWithValue("@_numero_jugadores", equipo.NumeroJugadores);
        cmd.Parameters.AddWithValue("@_nombre_director_tecnico", equipo.NombreDirectoTecnico);
        cmd.Parameters.AddWithValue("@_presidente_equipo", equipo.PresidenteEquipo);
        cmd.ExecuteNonQuery();
        cmd = new MySqlCommand("obtenerId", _conexion);
        cmd.CommandType = CommandType.StoredProcedure;
        id = Convert.ToInt32(cmd.ExecuteScalar());
        _trans.Commit();
      } catch(MySqlException ex) {
        _trans.Rollback();
        throw new Exception(ex.ToString());
      }
      _conexion.Close();
      return id;
    }

    /// <summary>
    /// Método para consultar el nombre de equipo y el id
    /// </summary>
    /// <returns>Devuelve una lista de todos los equipos registrados en la base de datos</returns>
    public List<Equipo> ConsultarEquipos() {
      List<Equipo> listaEquipo = new List<Equipo>();
      Equipo nombreEquipo = null;
      _conexion = ConexionBD.GetConexion();
      _conexion.Open();
      try {
        MySqlCommand comando = new MySqlCommand("obtenerNombreEquipo", _conexion);
        comando.CommandType = CommandType.StoredProcedure;
        MySqlDataReader reader = comando.ExecuteReader();
        while(reader.Read()) {
          nombreEquipo = new Equipo();
          nombreEquipo.NombreEquipo = reader["nombre"].ToString();
          nombreEquipo.IdEquipo = Convert.ToInt32(reader["idequipo"].ToString());
          listaEquipo.Add(nombreEquipo);
        }
      } catch(MySqlException ex) {
        listaEquipo = null;
        throw new Exception(ex.ToString());
      }
      _conexion.Close();
      return listaEquipo;
    }
    /*Método el cual se comunica con el prcedimiento que nos devolverá la cantidad de equipos registrados en la bd*/
    /// <summary>
    /// Método para saber cuantos equipos han sido registrados en la base de datos
    /// </summary>
    /// <returns>Devuelve una variable entera con la información de la cantidad de equipos registrados </returns>
    public int ObtenerCantidadEquipoRegistrados() {
      int cantidad = 0;
      _conexion = ConexionBD.GetConexion();
      _conexion.Open();
      try {
        MySqlCommand cmd = new MySqlCommand("cantidadEquipos", _conexion);
        cmd.CommandType = CommandType.StoredProcedure;
        MySqlDataReader reader = cmd.ExecuteReader();
        if(reader.Read()) {

          cantidad = Convert.ToInt32(reader["cantidadEquipos"].ToString());
        }
      } catch(MySqlException ex) {
        Console.WriteLine("Error al obtener la cantidad de equipos registrados" + ex.Message);
      }
      _conexion.Close();
      return cantidad;
    }

  }
}