using System;
using System.Collections.Generic;
using System.Data;

using Model;

using MySql.Data.MySqlClient;

using Sistema;

namespace Data {
  public class DatosEstadios {
    private MySqlConnection _conexion = null;
    private MySqlTransaction _transaccion = null;

    public List<Estadio> ObtenerEstadiosDisponibles() {
      List<Estadio> listaEstadios = new List<Estadio>();
      Estadio estadio = null;
      _conexion = ConexionBD.GetConexion();
      _conexion.Open();
      try {
        MySqlCommand comando = new MySqlCommand("estadiosDiponibles", _conexion);
        comando.CommandType = CommandType.StoredProcedure;
        MySqlDataReader reader = comando.ExecuteReader();
        while(reader.Read()) {
          estadio = new Estadio();
          estadio.Id = Convert.ToInt32(reader["idestadio"].ToString());
          estadio.Nombre = reader["nombreEstadio"].ToString();
          estadio.Asignacion = reader["asignacion"].ToString();
          listaEstadios.Add(estadio);
        }
      } catch(Exception ex) {
        Console.WriteLine("Error en la obtencion de estadios disponibles: " + ex.Message);
      }
      _conexion.Close();
      return listaEstadios;
    }

    public Estadio ObtenerEstadioPorId(int idEstadio) {
      Estadio estadio = null;
      _conexion = ConexionBD.GetConexion();
      _conexion.Open();
      try {
        MySqlCommand comando = new MySqlCommand("obtenerEstadioPorId", _conexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue("@_idEstadio", idEstadio);
        MySqlDataReader reader = comando.ExecuteReader();
        if(reader.Read()) {

          estadio = new Estadio();
          estadio.Id = Convert.ToInt32(reader["idestadio"].ToString());
          estadio.Nombre = reader["nombreEstadio"].ToString();
          estadio.Asignacion = reader["asignacion"].ToString();
        }

      } catch(MySqlException ex) {
        Console.WriteLine("Error en la obtencion de estadios disponibles: " + ex.Message);
      }
      _conexion.Close();
      return estadio;
    }

    public bool PutEstadiosDisponibles() {
      bool cambio = false;
      _conexion = ConexionBD.GetConexion();
      _conexion.Open();
      _transaccion = _conexion.BeginTransaction();
      try {
        MySqlCommand comando = new MySqlCommand("PonerEstadiosDisponibles", _conexion, _transaccion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.ExecuteNonQuery();
        _transaccion.Commit();
        cambio = true;
      } catch(Exception ex) {
        _transaccion.Rollback();
        Console.WriteLine("Error en la obtencion de estadios disponibles: " + ex.Message);
      } finally {
        _conexion.Close();
      }
      return cambio;
    }

    public bool CambiarEstado(int idEsadio, string estado) {
      bool cambio = false;
      _conexion = ConexionBD.GetConexion();
      _conexion.Open();
      _transaccion = _conexion.BeginTransaction();
      try {
        MySqlCommand comando = new MySqlCommand("asigacionEstadoEstadio", _conexion, _transaccion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue("_asignacion", estado);
        comando.Parameters.AddWithValue("_idestadio", idEsadio);
        comando.ExecuteNonQuery();
        _transaccion.Commit();
        cambio = true;
      } catch(Exception ex) {
        _transaccion.Rollback();
        Console.WriteLine("Error en la obtencion de estadios disponibles: " + ex.Message);
      }
      _conexion.Close();
      return cambio;
    }
  }
}
