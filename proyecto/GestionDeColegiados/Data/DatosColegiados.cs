using System;
using System.Collections.Generic;
using System.Data;

using Model.Colegiados;

using MySql.Data.MySqlClient;

using Sistema;

namespace Data {
  /// <summary>
  /// Clase para gestionar los datos de colegiado en la BD.
  /// </summary>
  /// <remarks>
  /// Se usa para ejecutar los procedimientos almacenados y operaciones de la BD.
  /// </remarks>
  public class DatosColegiados {
    private MySqlConnection _conexion = null;
    private MySqlTransaction _trans = null;

    /// <summary>
    /// Método para guardar Juez Central.
    /// </summary>
    /// <param name="juezCentral">Objeto Juez Central.</param>
    /// <returns>Devuelve el último id insertado como entero.</returns>
    public int InsertarJuezCentral(JuezCentral juezCentral) {
      int id = 0;
      _conexion = ConexionBD.getConexion(); //Obtener conexión
      _conexion.Open();                     //Abrir conexión
      _trans = _conexion.BeginTransaction(); //Comenzar transaccion
      try {
        //Inicializa una nueva instancia de la clase MySqlCommand con el texto de la consulta, MySqlConnection y MySqlTransaction
        MySqlCommand cmd = new MySqlCommand("guardarJuezCentral", _conexion, _trans);

        //Comando para decirle que lo que ejecutar es un procedimiento
        cmd.CommandType = CommandType.StoredProcedure;

        //Añade los valores del procedimiento a los atributos de la clase JuezCentral
        cmd.Parameters.AddWithValue("@_cedula", juezCentral.Cedula);
        cmd.Parameters.AddWithValue("@_nombre", juezCentral.Nombre);
        cmd.Parameters.AddWithValue("@_apellido", juezCentral.Apellidos);
        cmd.Parameters.AddWithValue("@_domicilio", juezCentral.Domicilio);
        cmd.Parameters.AddWithValue("@_email", juezCentral.Email);
        cmd.Parameters.AddWithValue("@_telefono", juezCentral.Telefono);

        //Ejecuta el procedimiento
        cmd.ExecuteNonQuery();

        //Obtener ID de la ultima sentencia que se ejecutó
        cmd = new MySqlCommand("obtenerId", _conexion);
        cmd.CommandType = CommandType.StoredProcedure;

        //Convertir lo obtenido a entero
        id = Convert.ToInt32(cmd.ExecuteScalar());

        _trans.Commit();
      } catch(MySqlException ex) {
        _trans.Rollback();
        throw new FalloBDException(ex.Message);
      }
      _conexion.Close(); //Cerrar conexión
      return id;
    }

    /// <summary>
    /// Método para guardar Asistente 1.
    /// </summary>
    /// <param name="asistente1">Objeto Asistente 1.</param>
    /// <returns>Devuelve el último id insertado como entero.</returns>
    public int InsertarAsistente1(Asistente asistente1) {
      int id = 0;
      _conexion = ConexionBD.getConexion();    //Obtener conexión
      _conexion.Open();                        //Abrir conexión
      _trans = _conexion.BeginTransaction();    //Comenzar transaccion
      try {
        MySqlCommand cmd = new MySqlCommand("guardarAsistente1", _conexion, _trans);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@_cedula", asistente1.Cedula);
        cmd.Parameters.AddWithValue("@_nombre", asistente1.Nombre);
        cmd.Parameters.AddWithValue("@_apellido", asistente1.Apellidos);
        cmd.Parameters.AddWithValue("@_domicilio", asistente1.Domicilio);
        cmd.Parameters.AddWithValue("@_email", asistente1.Email);
        cmd.Parameters.AddWithValue("@_telefono", asistente1.Telefono);
        cmd.Parameters.AddWithValue("@_banda", asistente1.Banda);

        cmd.ExecuteNonQuery();

        cmd = new MySqlCommand("obtenerId", _conexion);
        cmd.CommandType = CommandType.StoredProcedure;
        id = Convert.ToInt32(cmd.ExecuteScalar());

        _trans.Commit();
      } catch(MySqlException ex) {
        _trans.Rollback();
        throw new FalloBDException(ex.Message);
      }
      _conexion.Close();   //Cerrar conexión
      return id;
    }

    /// <summary>
    /// Método para guardar Asistente 2.
    /// </summary>
    /// <param name="asistente2">Objeto Asistente 2.</param>
    /// <returns>Devuelve el último id insertado como entero.</returns>
    public int InsertarAsistente2(Asistente asistente2) {
      int id = 0;
      _conexion = ConexionBD.getConexion();    //Obtener conexión
      _conexion.Open();                        //Abrir conexión
      _trans = _conexion.BeginTransaction();    //Comenzar transaccion
      try {
        MySqlCommand cmd = new MySqlCommand("guardarAsistente2", _conexion, _trans);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@_cedula", asistente2.Cedula);
        cmd.Parameters.AddWithValue("@_nombre", asistente2.Nombre);
        cmd.Parameters.AddWithValue("@_apellido", asistente2.Apellidos);
        cmd.Parameters.AddWithValue("@_domicilio", asistente2.Domicilio);
        cmd.Parameters.AddWithValue("@_email", asistente2.Email);
        cmd.Parameters.AddWithValue("@_telefono", asistente2.Telefono);
        cmd.Parameters.AddWithValue("@_banda", asistente2.Banda);

        cmd.ExecuteNonQuery();

        cmd = new MySqlCommand("obtenerId", _conexion);
        cmd.CommandType = CommandType.StoredProcedure;
        id = Convert.ToInt32(cmd.ExecuteScalar());

        _trans.Commit();
      } catch(MySqlException ex) {
        _trans.Rollback();
        throw new FalloBDException(ex.Message);
      }
      _conexion.Close();   //Cerrar conexión
      return id;
    }

    /// <summary>
    /// Método para guardar Cuarto Arbitro.
    /// </summary>
    /// <param name="cuartoArbitro">Objeto Cuarto Arbitro.</param>
    /// <returns>Devuelve el último id insertado como entero</returns>
    public int InsertarCuartoArbitro(CuartoArbitro cuartoArbitro) {
      int id = 0;
      _conexion = ConexionBD.getConexion();    //Obtener conexión
      _conexion.Open();                        //Abrir conexión
      _trans = _conexion.BeginTransaction();    //Comenzar transaccion
      try {
        MySqlCommand cmd = new MySqlCommand("guardarCuartoArbitro", _conexion, _trans);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@_cedula", cuartoArbitro.Cedula);
        cmd.Parameters.AddWithValue("@_nombre", cuartoArbitro.Nombre);
        cmd.Parameters.AddWithValue("@_apellido", cuartoArbitro.Apellidos);
        cmd.Parameters.AddWithValue("@_domicilio", cuartoArbitro.Domicilio);
        cmd.Parameters.AddWithValue("@_email", cuartoArbitro.Email);
        cmd.Parameters.AddWithValue("@_telefono", cuartoArbitro.Telefono);
        cmd.ExecuteNonQuery();
        cmd = new MySqlCommand("obtenerId", _conexion);
        cmd.CommandType = CommandType.StoredProcedure;
        id = Convert.ToInt32(cmd.ExecuteScalar());
        _trans.Commit();
      } catch(MySqlException ex) {
        _trans.Rollback();
        throw new FalloBDException(ex.Message);
      }
      _conexion.Close();   //Cerrar conexión
      return id;
    }

    /// <summary>
    /// Método para guardar Colegiado.
    /// </summary>
    /// <param name="colegiado">Objeto Colegiado.</param>
    /// <returns>Devuelve el último id insertado como entero</returns>
    public void InsertarColegiado(Colegiado colegiado) {
      _conexion = ConexionBD.getConexion();    //Obtener conexión
      _conexion.Open();                        //Abrir conexión
      _trans = _conexion.BeginTransaction();    //Comenzar transaccion
      try {
        MySqlCommand cmd = new MySqlCommand("guardarColegiado", _conexion, _trans);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@_idjuezcentral", colegiado.Idjuezcentral);
        cmd.Parameters.AddWithValue("@_idasistente1", colegiado.Idasistente1);
        cmd.Parameters.AddWithValue("@_idasistente2", colegiado.Idasistente2);
        cmd.Parameters.AddWithValue("@_idcuartoarbitro", colegiado.Idcuartoarbitro);

        cmd.ExecuteNonQuery();

        _trans.Commit();
      } catch(MySqlException ex) {
        _trans.Rollback();
        throw new FalloBDException(ex.Message);
      }
      _conexion.Close();//Cerrar conexión
    }

    /// <summary>
    /// Método para obtener los id de colegiados.
    /// </summary>
    /// <returns>Devuelve una lista de entero con los id.</returns>
    public List<int> ConsultarIdColegiado() {
      List<int> listaIdColegiado = new List<int>(); //Crear lista
      MySqlDataReader reader = null;          //tabla virtual
      _conexion = ConexionBD.getConexion();    //Obtener conexión
      _conexion.Open();                        //Abrir conexión
      try {
        MySqlCommand comando = new MySqlCommand("obtenerIdColegiado", _conexion);
        comando.CommandType = CommandType.StoredProcedure;
        reader = comando.ExecuteReader();

        //Condición para leer y agregar los arbitros a la lista
        while(reader.Read()) {
          int id = Convert.ToInt32(reader["idcolegiado"].ToString());
          listaIdColegiado.Add(id);
        }
      } catch(MySqlException ex) {
        listaIdColegiado = null;
        throw new FalloBDException(ex.Message);
      }
      _conexion.Close();   //Cerrar conexión
      return listaIdColegiado;
    }

    /// <summary>
    /// Método para obtener los datos de Juez Central.
    /// </summary>
    /// <param name="id">ID de Juez Central.</param>
    /// <returns>Devuelve una lista con objetos Juez Central.</returns>
    public List<JuezCentral> ConsultarJuezCentral(int id) {
      List<JuezCentral> listaArbitro = new List<JuezCentral>(); //Crear lista
      JuezCentral arbitro = null;
      MySqlDataReader reader = null;          //tabla virtual
      _conexion = ConexionBD.getConexion();    //Obtener conexión
      _conexion.Open();                        //Abrir conexión
      try {
        MySqlCommand comando = new MySqlCommand("obtenerJuezCentral", _conexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue("@_idColegiado", id);
        reader = comando.ExecuteReader();

        //Condición para leer y agregar los arbitros a la lista
        while(reader.Read()) {
          arbitro = new JuezCentral();
          arbitro.Cedula = reader["cedula"].ToString();
          arbitro.Nombre = reader["nombre"].ToString();
          arbitro.Apellidos = reader["apellido"].ToString();
          arbitro.Domicilio = reader["domicilio"].ToString();
          arbitro.Email = reader["email"].ToString();
          arbitro.Telefono = reader["telefono"].ToString();
          listaArbitro.Add(arbitro);
        }
      } catch(MySqlException ex) {
        listaArbitro = null;
        throw new FalloBDException(ex.Message);
      }
      _conexion.Close();   //Cerrar conexión
      return listaArbitro;
    }

    /// <summary>
    /// Método para obtener los datos de Asistente.
    /// </summary>
    /// <param name="id">ID de asistente.</param>
    /// <param name="procedimiento">Nombre del procedimierto a ejecutar.</param>
    /// <returns>Devuelve una lista con objetos Asistente.</returns>
    public List<Asistente> ConsultarAsistente(int id, string procedimiento) {
      List<Asistente> listaAsistente = new List<Asistente>();
      Asistente asistente = null;
      MySqlDataReader reader = null;          //tabla virtual
      _conexion = ConexionBD.getConexion();    //Obtener conexión
      _conexion.Open();                        //Abrir conexión
      try {
        MySqlCommand comando = new MySqlCommand(procedimiento, _conexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue("@_idColegiado", id);
        reader = comando.ExecuteReader();

        //Condición para leer y agregar los arbitros a la lista
        while(reader.Read()) {
          asistente = new Asistente();
          asistente.Cedula = reader["cedula"].ToString();
          asistente.Nombre = reader["nombre"].ToString();
          asistente.Apellidos = reader["apellido"].ToString();
          asistente.Domicilio = reader["domicilio"].ToString();
          asistente.Email = reader["email"].ToString();
          asistente.Telefono = reader["telefono"].ToString();
          listaAsistente.Add(asistente);
        }
      } catch(MySqlException ex) {
        listaAsistente = null;
        throw new FalloBDException(ex.Message);
      }
      _conexion.Close();   //Cerrar conexión
      return listaAsistente;
    }

    /// <summary>
    /// Método para obtener los datos de Asistente 1.
    /// </summary>
    /// <param name="id">ID de asistente 1.</param>
    /// <returns>Devuelve una lista con objetos Asistente 1.</returns>
    public List<Asistente> ConsultarAsistente1(int id) {
      List<Asistente> listaAsistente1 = new List<Asistente>();
      listaAsistente1 = ConsultarAsistente(id, "obtenerAsistente1");
      return listaAsistente1;
    }

    /// <summary>
    /// Método para obtener los datos de Asistente 2.
    /// </summary>
    /// <param name="id">ID de asistente 2.</param>
    /// <returns>Devuelve una lista con objetos Asistente 2.</returns>
    public List<Asistente> ConsultarAsistente2(int id) {
      List<Asistente> listaAsistente1 = new List<Asistente>();
      listaAsistente1 = ConsultarAsistente(id, "obtenerAsistente2");
      return listaAsistente1;
    }

    /// <summary>
    /// Método para obtener los datos de Cuarto Arbitro.
    /// </summary>
    /// <param name="id">ID de Cuarto Arbitro.</param>
    /// <returns>Devuelve una lista con objetos Cuarto Arbitro.</returns>
    public List<CuartoArbitro> ConsultarCuartoArbitro(int id) {
      List<CuartoArbitro> listaCA = new List<CuartoArbitro>(); //Crear lista
      CuartoArbitro arbitro = null;
      MySqlDataReader reader = null;          //tabla virtual
      _conexion = ConexionBD.getConexion();    //Obtener conexión
      _conexion.Open();                        //Abrir conexión
      try {
        MySqlCommand comando = new MySqlCommand("obtenerCuartoArbitro", _conexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue("@_idColegiado", id);
        reader = comando.ExecuteReader();

        //Condición para leer y agregar los arbitros a la lista
        while(reader.Read()) {
          arbitro = new CuartoArbitro();
          arbitro.Cedula = reader["cedula"].ToString();
          arbitro.Nombre = reader["nombre"].ToString();
          arbitro.Apellidos = reader["apellido"].ToString();
          arbitro.Domicilio = reader["domicilio"].ToString();
          arbitro.Email = reader["email"].ToString();
          arbitro.Telefono = reader["telefono"].ToString();
          listaCA.Add(arbitro);
        }
      } catch(MySqlException ex) {
        listaCA = null;
        throw new FalloBDException(ex.Message);
      }
      _conexion.Close();   //Cerrar conexión
      return listaCA;
    }

    /// <summary>
    /// Método para obtener nombre los arbitros.
    /// </summary>
    /// <returns>Devuelve una lista con objetos IntegrantesColegiados.</returns>
    public List<IntegrantesColegiados> ConsultarColegiado() {
      List<IntegrantesColegiados> listaColegiado = new List<IntegrantesColegiados>(); //Crear lista
      IntegrantesColegiados integrantesColeg = null;      //Instanciar clase IntegrantesColegiados
      MySqlDataReader reader = null;          //tabla virtual
      _conexion = ConexionBD.getConexion();    //Obtener conexión
      _conexion.Open();                        //Abrir conexión
      try {
        MySqlCommand comando = new MySqlCommand("obtenerColegiado", _conexion);

        comando.CommandType = CommandType.StoredProcedure;
        reader = comando.ExecuteReader();

        //Condición para leer y agregar los arbitros a la lista
        while(reader.Read()) {
          integrantesColeg = new IntegrantesColegiados();
          integrantesColeg.IdGrupoColegiado = Convert.ToInt32(reader["idGrupoColegiado"].ToString());
          integrantesColeg.NombrejuezCentral = reader["nombreJC"].ToString();
          integrantesColeg.Nombreasistente1 = reader["nombreAs1"].ToString();
          integrantesColeg.Nombreasistente2 = reader["nombreAs2"].ToString();
          integrantesColeg.NombrecuartoArbitro = reader["nombreCA"].ToString();
          listaColegiado.Add(integrantesColeg);
        }
      } catch(MySqlException ex) {
        listaColegiado = null;
        throw new FalloBDException(ex.Message);
      }
      _conexion.Close();   //Cerrar conexión
      return listaColegiado;
    }

    /// <summary>
    /// Método para consultar las cedula de los arbitros registrados.
    /// </summary>
    /// <returns>Devuelve una lista con objetos IntegrantesColegiados.</returns>
    public List<IntegrantesColegiados> ConsultarCedulaColegiado() {
      List<IntegrantesColegiados> listaColegiado = new List<IntegrantesColegiados>();
      IntegrantesColegiados integrantesColeg = null;
      MySqlDataReader reader = null;          //tabla virtual
      _conexion = ConexionBD.getConexion();    //Obtener conexión
      _conexion.Open();                        //Abrir conexión
      try {
        MySqlCommand comando = new MySqlCommand("obtenerCedulaColegiado", _conexion);

        comando.CommandType = CommandType.StoredProcedure;
        reader = comando.ExecuteReader();

        //Condición para leer y agregar los arbitros a la lista
        while(reader.Read()) {
          integrantesColeg = new IntegrantesColegiados();
          integrantesColeg.IdGrupoColegiado = Convert.ToInt32(reader["idGrupoColegiado"].ToString());
          integrantesColeg.NombrejuezCentral = reader["cedulaJC"].ToString();
          integrantesColeg.Nombreasistente1 = reader["cedulaAs1"].ToString();
          integrantesColeg.Nombreasistente2 = reader["cedulaAs2"].ToString();
          integrantesColeg.NombrecuartoArbitro = reader["cedulaCA"].ToString();
          listaColegiado.Add(integrantesColeg);
        }
      } catch(MySqlException ex) {
        listaColegiado = null;
        throw new FalloBDException(ex.Message);
      }
      _conexion.Close();   //Cerrar conexión
      return listaColegiado;
    }

    /// <summary>
    /// Método para obtener el nombre de los arbitros de acuerdo a un id.
    /// </summary>
    /// <param name="idColegiado">ID colegiado.</param>
    /// <returns>Devuelve un string con los nombres.</returns>
    public string ObtenerNombreDeColegiados(int idColegiado) {
      string nombres = "";
      MySqlDataReader reader = null;          //tabla virtual
      _conexion = ConexionBD.getConexion();    //Obtener conexión
      _conexion.Open();                        //Abrir conexión
      try {
        MySqlCommand comando = new MySqlCommand("obtenerUnColegiado", _conexion);

        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue("@_idColegiado", idColegiado);
        reader = comando.ExecuteReader();
        if(reader.Read()) {
          nombres = "Juez Central: " + reader["nombreJC"].ToString() + "\r\n";
          nombres += "Asistente 1: " + reader["nombreAs1"].ToString() + "\r\n";
          nombres += "Asistente 2: " + reader["nombreAs2"].ToString() + "\r\n";
          nombres += "Cuarto Arbitro: " + reader["nombreCA"].ToString();
        }
      } catch(MySqlException ex) {
        Console.WriteLine(ex.Message);
      }
      _conexion.Close();   //Cerrar conexión
      return nombres;
    }

    /// <summary>
    /// Método para obtener todos los id de los arbitros de acuerdo a un parametro.
    /// </summary>
    /// <param name="idColegiado">ID colegiado.</param>
    /// <returns>Devuelve una lista con objetos Colegiado.</returns>
    public List<Colegiado> ObtenerTodosIdColegiado(int idColegiado) {
      List<Colegiado> listaIDColegiado = new List<Colegiado>();
      Colegiado colegiado = null;
      MySqlDataReader reader = null;          //tabla virtual
      _conexion = ConexionBD.getConexion();    //Obtener conexión
      _conexion.Open();                        //Abrir conexión
      try {
        MySqlCommand comando = new MySqlCommand("obtenerTodosIDColegiado", _conexion);
        comando.CommandType = CommandType.StoredProcedure;
        comando.Parameters.AddWithValue("@_idColegiado", idColegiado);
        reader = comando.ExecuteReader();

        //Condición para leer y agregar los arbitros a la lista
        while(reader.Read()) {
          colegiado = new Colegiado();
          colegiado.Idcolegiado = Convert.ToInt32(reader["idColegiado"].ToString());
          colegiado.Idjuezcentral = Convert.ToInt32(reader["idJuezCentral"].ToString());
          colegiado.Idasistente1 = Convert.ToInt32(reader["idAsistente1"].ToString());
          colegiado.Idasistente2 = Convert.ToInt32(reader["idAsistente2"].ToString());
          colegiado.Idcuartoarbitro = Convert.ToInt32(reader["idCuartoArbitro"].ToString());
          listaIDColegiado.Add(colegiado);
        }
      } catch(MySqlException ex) {
        listaIDColegiado = null;
        throw new FalloBDException(ex.Message);
      }
      _conexion.Close();   //Cerrar conexión
      return listaIDColegiado;
    }

    /// <summary>
    /// Método para editar una Juez Central.
    /// </summary>
    /// <param name="juezCentral">ID de Juez Central.</param>
    public void EditarJuezCentralBD(JuezCentral juezCentral) {
      _conexion = ConexionBD.getConexion();    //Obtener conexión
      _conexion.Open();                        //Abrir conexión
      _trans = _conexion.BeginTransaction();    //Comenzar transaccion
      try {
        MySqlCommand comando = new MySqlCommand("editarJuezCentral", _conexion, _trans);
        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.AddWithValue("@_idJuezCentral", juezCentral.IdArbitro);
        comando.Parameters.AddWithValue("@_cedula", juezCentral.Cedula);
        comando.Parameters.AddWithValue("@_nombre", juezCentral.Nombre);
        comando.Parameters.AddWithValue("@_apellido", juezCentral.Apellidos);
        comando.Parameters.AddWithValue("@_domicilio", juezCentral.Domicilio);
        comando.Parameters.AddWithValue("@_email", juezCentral.Email);
        comando.Parameters.AddWithValue("@_telefono", juezCentral.Telefono);

        comando.ExecuteNonQuery();
        _trans.Commit();
      } catch(MySqlException ex) {
        _trans.Rollback();
        throw new FalloBDException(ex.Message);
      }
      _conexion.Close();   //Cerrar conexión
    }

    /// <summary>
    /// Método para editar un Asistente.
    /// </summary>
    /// <param name="asistente">Objeto Asistente.</param>
    /// <param name="procedimiento">Nombre del procedimiento.</param>
    public void EditarAsistenteBD(Asistente asistente, string procedimiento) {
      _conexion = ConexionBD.getConexion();    //Obtener conexión
      _conexion.Open();                        //Abrir conexión
      _trans = _conexion.BeginTransaction();    //Comenzar transaccion
      try {
        MySqlCommand comando = new MySqlCommand(procedimiento, _conexion, _trans);
        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.AddWithValue("@_idAsistente", asistente.IdArbitro);
        comando.Parameters.AddWithValue("@_cedula", asistente.Cedula);
        comando.Parameters.AddWithValue("@_nombre", asistente.Nombre);
        comando.Parameters.AddWithValue("@_apellido", asistente.Apellidos);
        comando.Parameters.AddWithValue("@_domicilio", asistente.Domicilio);
        comando.Parameters.AddWithValue("@_email", asistente.Email);
        comando.Parameters.AddWithValue("@_telefono", asistente.Telefono);

        comando.ExecuteNonQuery();
        _trans.Commit();
      } catch(MySqlException ex) {
        _trans.Rollback();
        throw new FalloBDException(ex.Message);
      }
      _conexion.Close();   //Cerrar conexión
    }

    /// <summary>
    /// Método para editar un Asistente 1.
    /// </summary>
    /// <param name="asistente1">Objeto Asistente 1.</param>
    public void EditarAsistente1BD(Asistente asistente1) {
      string procedimiento = "editarAsistente1";
      EditarAsistenteBD(asistente1, procedimiento);
    }

    /// <summary>
    /// Método para editar un Asistente 2.
    /// </summary>
    /// <param name="asistente2">Objeto Asistente 2.</param>
    public void EditarAsistente2BD(Asistente asistente2) {
      string procedimiento = "editarAsistente2";
      EditarAsistenteBD(asistente2, procedimiento);
    }

    /// <summary>
    /// Método para editar un Cuarto Arbitro.
    /// </summary>
    /// <param name="cuartoArbitro">Objeto Cuarto Arbitro-</param>
    public void EditarCuartoArbitro(CuartoArbitro cuartoArbitro) {
      _conexion = ConexionBD.getConexion();    //Obtener conexión
      _conexion.Open();                        //Abrir conexión
      _trans = _conexion.BeginTransaction();    //Comenzar transaccion
      try {
        MySqlCommand comando = new MySqlCommand("editarCuartoArbitro", _conexion, _trans);
        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.AddWithValue("@_idCuartoArbitro", cuartoArbitro.IdArbitro);
        comando.Parameters.AddWithValue("@_cedula", cuartoArbitro.Cedula);
        comando.Parameters.AddWithValue("@_nombre", cuartoArbitro.Nombre);
        comando.Parameters.AddWithValue("@_apellido", cuartoArbitro.Apellidos);
        comando.Parameters.AddWithValue("@_domicilio", cuartoArbitro.Domicilio);
        comando.Parameters.AddWithValue("@_email", cuartoArbitro.Email);
        comando.Parameters.AddWithValue("@_telefono", cuartoArbitro.Telefono);

        comando.ExecuteNonQuery();
        _trans.Commit();
      } catch(MySqlException ex) {
        _trans.Rollback();
        throw new FalloBDException(ex.Message);
      }
      _conexion.Close();   //Cerrar conexión
    }

    /// <summary>
    /// Eliminar un arbitro "lógico".
    /// </summary>
    /// <param name="idArbitro">ID arbitro.</param>
    /// <param name="procedimiento">Nombre del procedimiento.</param>
    public void EliminarArbitro(int idArbitro, string procedimiento) {
      _conexion = ConexionBD.getConexion();    //Obtener conexión
      _conexion.Open();                        //Abrir conexión
      _trans = _conexion.BeginTransaction();    //Comenzar transaccion
      try {
        MySqlCommand comando = new MySqlCommand(procedimiento, _conexion, _trans);
        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.AddWithValue("@_idArbitro", idArbitro);

        comando.ExecuteNonQuery();
        _trans.Commit();
      } catch(MySqlException ex) {
        _trans.Rollback();
        throw new FalloBDException(ex.Message);
      }
      _conexion.Close();   //Cerrar conexión
    }

    /// <summary>
    /// Método para eliminar un Juez Central.
    /// </summary>
    /// <param name="idArbitro">ID Juez Central.</param>
    public void EliminarJuezCentralBD(int idArbitro) {
      string procedimiento = "eliminarJuezCentral";
      EliminarArbitro(idArbitro, procedimiento);
    }

    /// <summary>
    /// Método para eliminar un Asistente 1.
    /// </summary>
    /// <param name="idArbitro">ID Asistente 1.</param>
    public void EliminarAsistente1BD(int idArbitro) {
      string procedimiento = "eliminarAsistente1";
      EliminarArbitro(idArbitro, procedimiento);
    }

    /// <summary>
    /// Método para eliminar un Asistente 2.
    /// </summary>
    /// <param name="idArbitro">ID Asistente 2.</param>
    public void EliminarAsistente2BD(int idArbitro) {
      string procedimiento = "eliminarAsistente2";
      EliminarArbitro(idArbitro, procedimiento);
    }

    /// <summary>
    /// Método para eliminar un Cuarto Arbitro.
    /// </summary>
    /// <param name="idArbitro">ID Cuarto Arbitro.</param>
    public void EliminarCuartoArbitroBD(int idArbitro) {
      string procedimiento = "eliminarCuartoArbitro";
      EliminarArbitro(idArbitro, procedimiento);
    }

    /// <summary>
    /// Método para eliminar un Colegiado.
    /// </summary>
    /// <param name="idArbitro">ID Colegiado.</param>
    public void EliminarColegiado(int idColegiado) {
      string procedimiento = "eliminarColegiado";
      EliminarArbitro(idColegiado, procedimiento);
    }

    /// <summary>
    /// Método para editar un arbitro de un colegiado.
    /// </summary>
    /// <param name="idColegiado">ID Colegiado.</param>
    /// <param name="idNuevo">ID del nuevo arbitro.</param>
    /// <param name="arbitro">Tipo de árbitro.</param>
    public void ActualizarColegiadoBD(int idColegiado, int idNuevo, string arbitro) {
      _conexion = ConexionBD.getConexion();    //Obtener conexión
      _conexion.Open();                        //Abrir conexión
      _trans = _conexion.BeginTransaction();    //Comenzar transaccion
      try {
        MySqlCommand comando = new MySqlCommand("actualizarColegiado", _conexion, _trans);
        comando.CommandType = CommandType.StoredProcedure;

        comando.Parameters.AddWithValue("@_idColegiado", idColegiado);
        comando.Parameters.AddWithValue("@_idNuevo", idNuevo);
        comando.Parameters.AddWithValue("@_arbitro", arbitro);

        comando.ExecuteNonQuery();
        _trans.Commit();
      } catch(MySqlException ex) {
        _trans.Rollback();
        throw new FalloBDException(ex.Message);
      }
      _conexion.Close();   //Cerrar conexión
    }
  }
}