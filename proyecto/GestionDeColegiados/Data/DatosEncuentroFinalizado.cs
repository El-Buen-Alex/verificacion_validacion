using System;
using System.Collections.Generic;
using System.Data;

using Model.Partido;

using MySql.Data.MySqlClient;

using Sistema;

namespace Data {
  public class DatosEncuentroFinalizado {
    private MySqlConnection _conexion = null;
    private MySqlTransaction _trans = null;

    public bool AddEncuentroFinalizado(EncuentroFinalizado encuentroFinalizado) {
      bool guardado = false;
      _conexion = ConexionBD.getConexion();
      _conexion.Open();
      _trans = _conexion.BeginTransaction();
      try {
        MySqlCommand cmd = new MySqlCommand("guardarPartidoFinalizado", _conexion, _trans);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@_idEquipo", encuentroFinalizado.IdEquipo);
        cmd.Parameters.AddWithValue("@_idDefinido", encuentroFinalizado.IdEncuentroDefinido);
        cmd.Parameters.AddWithValue("@_golesFavor", encuentroFinalizado.GolesFavor);
        cmd.Parameters.AddWithValue("@_golesContra", encuentroFinalizado.GolesContra);
        cmd.Parameters.AddWithValue("@_golesDiferencia", encuentroFinalizado.GolesDiferencia);
        cmd.Parameters.AddWithValue("@_puntos", encuentroFinalizado.Puntos);
        cmd.Parameters.AddWithValue("@_copa", encuentroFinalizado.Copa);

        cmd.ExecuteNonQuery();

        _trans.Commit();

        guardado = true;
      } catch(MySqlException ex) {
        _trans.Rollback();
        throw new Exception(ex.ToString());
      } finally {
        _conexion.Close();
      }

      return guardado;
    }

    public bool FinalizarCompetencia(string anio, string estado) {
      bool respuesta = false;
      _conexion = ConexionBD.getConexion();
      _conexion.Open();
      _trans = _conexion.BeginTransaction();
      try {
        MySqlCommand cmd = new MySqlCommand("finalizarCompetencia", _conexion, _trans);

        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("_copa", anio);
        cmd.Parameters.AddWithValue("_estado", estado[0]);
        cmd.ExecuteNonQuery();

        _trans.Commit();

        respuesta = true;

      } catch(MySqlException ex) {
        _trans.Rollback();

        throw new Exception(ex.ToString());
      } finally {
        _conexion.Close();
      }
      return respuesta;
    }

    public List<EncuentroFinalizado> GetEncuentrosFinalizados(string anio) {
      List<EncuentroFinalizado> posiciones = new List<EncuentroFinalizado>();
      EncuentroFinalizado encuentroFinalizado = null;
      _conexion = ConexionBD.getConexion();
      _conexion.Open();
      try {
        MySqlCommand cmd = new MySqlCommand("obtenerCompetencia", _conexion);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@_anio", anio);
        MySqlDataReader reader = cmd.ExecuteReader();
        while(reader.Read()) {
          encuentroFinalizado = new EncuentroFinalizado();
          encuentroFinalizado.Id = Convert.ToInt32(reader["id_partidoFinalizado"].ToString());
          encuentroFinalizado.IdEquipo = Convert.ToInt32(reader["idEquipo"].ToString());
          encuentroFinalizado.IdEncuentroDefinido = Convert.ToInt32(reader["idDefinido"].ToString());
          encuentroFinalizado.GolesFavor = Convert.ToInt32(reader["golesAFavor"].ToString());
          encuentroFinalizado.GolesContra = Convert.ToInt32(reader["golesEnContra"].ToString());
          encuentroFinalizado.GolesDiferencia = Convert.ToInt32(reader["golesDeDiferencia"].ToString());
          encuentroFinalizado.Puntos = Convert.ToInt32(reader["puntosTotales"].ToString());
          posiciones.Add(encuentroFinalizado);
        }

      } catch(MySqlException ex) {
        throw new Exception(ex.ToString());
      } finally {
        _conexion.Close();
      }
      return posiciones;
    }

    public EncuentroFinalizado GetEncuentroFinalizadoByIDefinidoEquipo(int idDefinido, int idEquipoLocal) {
      EncuentroFinalizado encuentroFinalizado = null;
      _conexion = ConexionBD.getConexion();
      _conexion.Open();
      try {
        MySqlCommand cmd = new MySqlCommand("obtenerEncuentroFinalizadoById", _conexion);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("_idDefinido", idDefinido);
        cmd.Parameters.AddWithValue("_idEquipo", idEquipoLocal);
        MySqlDataReader reader = cmd.ExecuteReader();
        if(reader.Read()) {
          encuentroFinalizado = new EncuentroFinalizado();
          encuentroFinalizado.Id = Convert.ToInt32(reader["id_partidoFinalizado"].ToString());
          encuentroFinalizado.IdEquipo = Convert.ToInt32(reader["idEquipo"].ToString());
          encuentroFinalizado.IdEncuentroDefinido = Convert.ToInt32(reader["idDefinido"].ToString());
          encuentroFinalizado.GolesFavor = Convert.ToInt32(reader["golesFavor"].ToString());
          encuentroFinalizado.GolesContra = Convert.ToInt32(reader["golesContra"].ToString());
          encuentroFinalizado.GolesDiferencia = Convert.ToInt32(reader["golesDiferencia"].ToString());
          encuentroFinalizado.Puntos = Convert.ToInt32(reader["puntos"].ToString());
        }
      } catch(MySqlException ex) {
        throw new Exception(ex.ToString());
      } finally {
        _conexion.Close();
      }
      return encuentroFinalizado;
    }

    public bool UpdateEncuentroFinalizado(EncuentroFinalizado encuentroResultado) {
      bool respuesta = false;
      _conexion = ConexionBD.getConexion();
      _conexion.Open();
      _trans = _conexion.BeginTransaction();
      try {
        MySqlCommand comando = new MySqlCommand("actulizarEncuentroFinalizado", _conexion, _trans);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue("@_idDefinido", encuentroResultado.IdEncuentroDefinido);
        comando.Parameters.AddWithValue("@_idEquipo", encuentroResultado.IdEquipo);
        comando.Parameters.AddWithValue("@_golFavor", encuentroResultado.GolesFavor);
        comando.Parameters.AddWithValue("@_golContra", encuentroResultado.GolesContra);
        comando.Parameters.AddWithValue("@_golDiferencia", encuentroResultado.GolesDiferencia);
        comando.Parameters.AddWithValue("@_puntos", encuentroResultado.Puntos);
        comando.ExecuteNonQuery();
        _trans.Commit();
        respuesta = true;
      } catch(MySqlException ex) {
        _trans.Rollback();
        Console.WriteLine(ex.Message);
      } finally {
        _conexion.Close();
      }

      return respuesta;
    }

    public int GetCantidadEncuentrosFinalizados(string anio) {
      int cantidad = 0;
      _conexion = ConexionBD.getConexion();
      _conexion.Open();
      try {
        MySqlCommand comando = new MySqlCommand("cantidadEncuentrosFinalizados", _conexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue("@_copa", anio);
        MySqlDataReader reader = comando.ExecuteReader();
        if(reader.Read()) {

          cantidad = Convert.ToInt32(reader["cantidadEncuentros"].ToString());
        }

      } catch(Exception ex) {
        Console.WriteLine(ex.Message);
        cantidad = -1;
      } finally {
        _conexion.Close();
      }
      return cantidad;

    }
  }
}
