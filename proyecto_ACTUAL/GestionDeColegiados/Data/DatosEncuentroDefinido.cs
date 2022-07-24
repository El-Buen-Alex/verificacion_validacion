using System;
using System.Collections.Generic;
using System.Data;

using Model;

using MySql.Data.MySqlClient;

using Sistema;

namespace Data {
  public class DatosEncuentroDefinido {
    private MySqlConnection _conexion = null;
    private MySqlTransaction _transaccion = null;


    public int GuardarEncuentroDefinido(EncuentroDefinido encuentroDefinido) {
      int id = 0;
      _conexion = ConexionBD.GetConexion();
      _conexion.Open();
      _transaccion = _conexion.BeginTransaction();
      try {
        MySqlCommand comando = new MySqlCommand("guardarEncuentrosDefinidos", _conexion, _transaccion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue("_fecha", encuentroDefinido.FechaDeEncuentro.ToString("yyyy-MM-dd"));
        comando.Parameters.AddWithValue("_hora", encuentroDefinido.Hora.ToString("HH:mm"));
        comando.Parameters.AddWithValue("_idencuentro", encuentroDefinido.IdEncuentroGeneradoPendiente);
        comando.Parameters.AddWithValue("_idcolegiado", encuentroDefinido.IdColegiado);
        comando.Parameters.AddWithValue("_idestadio", encuentroDefinido.IdEstadio);
        comando.ExecuteNonQuery();
        comando = new MySqlCommand("obtenerId", _conexion);
        comando.CommandType = CommandType.StoredProcedure;
        id = Convert.ToInt32(comando.ExecuteScalar());
        if(id != 0) {

          _transaccion.Commit();
        }
      } catch(Exception ex) {
        _transaccion.Rollback();
        Console.WriteLine(ex.Message);
      }
      _conexion.Close();
      return id;
    }

    public List<EncuentroDefinido> ObtenerEncuentros() {
      List<EncuentroDefinido> lista = new List<EncuentroDefinido>();
      EncuentroDefinido encuentroDefinido = null;
      _conexion = ConexionBD.GetConexion();
      _conexion.Open();
      try {
        MySqlCommand comando = new MySqlCommand("mostrarEncuentroDefinidos", _conexion);
        comando.CommandType = CommandType.StoredProcedure;
        MySqlDataReader reader = comando.ExecuteReader();
        while(reader.Read()) {
          encuentroDefinido = new EncuentroDefinido();
          encuentroDefinido.Id = Convert.ToInt32(reader["idefinido"].ToString());
          encuentroDefinido.IdColegiado = Convert.ToInt32(reader["idcolegiado"].ToString());
          encuentroDefinido.IdEncuentroGeneradoPendiente = Convert.ToInt32(reader["idencuentro"].ToString());
          encuentroDefinido.IdEstadio = Convert.ToInt32(reader["idestadio"].ToString());
          encuentroDefinido.Hora = Convert.ToDateTime(reader["hora"].ToString());
          encuentroDefinido.FechaDeEncuentro = Convert.ToDateTime(reader["fecha"].ToString());
          lista.Add(encuentroDefinido);
        }

      } catch(Exception ex) {
        Console.WriteLine(ex.Message);
      }
      _conexion.Close();
      return lista;
    }

    public bool CambiarEstadoEnucentroDefinido(string estado) {
      bool respuesta = false;
      _conexion = ConexionBD.GetConexion();
      _conexion.Open();
      _transaccion = _conexion.BeginTransaction();
      try {
        MySqlCommand cmd = new MySqlCommand("CambiarEstadoEncuentrosDefinidos", _conexion, _transaccion);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("_estado", estado[0]);
        cmd.ExecuteNonQuery();
        _transaccion.Commit();
        respuesta = true;
      } catch(MySqlException ex) {
        _transaccion.Rollback();
        throw new Exception(ex.ToString());
      } finally {
        _conexion.Close();
      }
      return respuesta;
    }

    public List<EncuentroDefinido> GetEncuentrosDefinidosFinalizados(string anio) {
      List<EncuentroDefinido> lista = new List<EncuentroDefinido>();
      EncuentroDefinido encuentroDefinido = null;
      _conexion = ConexionBD.GetConexion();
      _conexion.Open();
      try {
        MySqlCommand comando = new MySqlCommand("encuentrosDefinidosEncuentrosFinalizados", _conexion, _transaccion);
        comando.Parameters.AddWithValue("_copa", anio);
        comando.CommandType = CommandType.StoredProcedure;
        MySqlDataReader reader = comando.ExecuteReader();
        while(reader.Read()) {
          encuentroDefinido = new EncuentroDefinido();
          encuentroDefinido.Id = Convert.ToInt32(reader["idefinido"].ToString());
          encuentroDefinido.IdColegiado = Convert.ToInt32(reader["idcolegiado"].ToString());
          encuentroDefinido.IdEncuentroGeneradoPendiente = Convert.ToInt32(reader["idencuentro"].ToString());
          encuentroDefinido.IdEstadio = Convert.ToInt32(reader["idestadio"].ToString());
          encuentroDefinido.Hora = Convert.ToDateTime(reader["hora"].ToString());
          encuentroDefinido.FechaDeEncuentro = Convert.ToDateTime(reader["fecha"].ToString());
          lista.Add(encuentroDefinido);
        }
      } catch(Exception ex) {
        Console.WriteLine(ex.Message);
      } finally {
        _conexion.Close();
      }
      return lista;
    }

    public int ObtenerCantidadEncuentrosDefinidos() {
      int cantidad = 0;
      _conexion = ConexionBD.GetConexion();
      _conexion.Open();
      try {
        MySqlCommand comando = new MySqlCommand("cantidadEncuentrosDefinidosActivos", _conexion);
        comando.CommandType = CommandType.StoredProcedure;
        MySqlDataReader reader = comando.ExecuteReader();
        if(reader.Read()) {

          cantidad = Convert.ToInt32(reader["cantidadEncuentrosActivos"].ToString());
        }
      } catch(Exception ex) {
        Console.WriteLine(ex.Message);
      } finally {
        _conexion.Close();
      }
      return cantidad;
    }

    public bool ActualizarEstadioDePartido(int idEncuentroPorActualizar, int idNuevoEstadioAsociado) {
      bool exito = false;
      _conexion = ConexionBD.GetConexion();
      _conexion.Open();

      try {
        MySqlCommand comando = new MySqlCommand("actulizarEstadioAsociado", _conexion, _transaccion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue("@_idencuentro", idEncuentroPorActualizar);
        comando.Parameters.AddWithValue("@_idEstadio", idNuevoEstadioAsociado);
        comando.ExecuteNonQuery();
        exito = true;
      } catch(MySqlException ex) {
        Console.WriteLine(ex.Message);
      }
      _conexion.Close();
      return exito;
    }

    public int ObtenerCantidadEncuentrosPorJugar() {
      int cantidad = 0;
      _conexion = ConexionBD.GetConexion();
      _conexion.Open();
      try {
        MySqlCommand comando = new MySqlCommand("cantidadEncuentrosPorJugar", _conexion);
        comando.CommandType = CommandType.StoredProcedure;
        MySqlDataReader reader = comando.ExecuteReader();
        if(reader.Read()) {

          cantidad = Convert.ToInt32(reader["cantidadEncuentros"].ToString());
        }
      } catch(Exception ex) {
        Console.WriteLine(ex.Message);
      }
      _conexion.Close();
      return cantidad;
    }

    public bool ActualizarFechaHoraDEPartido(int id, DateTime fecha, DateTime hora, int idColegiado) {
      bool exito = false;
      _conexion = ConexionBD.GetConexion();
      _conexion.Open();
      try {
        MySqlCommand comando = new MySqlCommand("actulizarFechaHoraColegiadoEncuentroDefinido", _conexion, _transaccion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue("@_idefinido", id);
        comando.Parameters.AddWithValue("@_fecha", fecha.ToString("yyyy-MM-dd"));
        comando.Parameters.AddWithValue("@_hora", hora.ToString("HH:mm"));
        comando.Parameters.AddWithValue("@_idColegiado", idColegiado);
        comando.ExecuteNonQuery();
        exito = true;
      } catch(MySqlException ex) {
        Console.WriteLine(ex.Message);
        exito = false;
      } finally {
        _conexion.Close();
      }
      return exito;
    }
  }
}