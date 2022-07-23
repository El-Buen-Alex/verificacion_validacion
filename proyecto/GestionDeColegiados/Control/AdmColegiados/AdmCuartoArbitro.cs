using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Data;

using Model.Colegiados;

namespace Control.AdmColegiados {
  /// <summary>
  /// Clase para la gestión de Cuarto Árbitro.
  /// </summary>
  /// <remarks>
  /// Crea las listas, instancias y validaciones para obtener los datos de Cuarto Árbitro.
  /// </remarks>
  public class AdmCuartoArbitro : IAdm {
    List<CuartoArbitro> _listaCuartoArbitro = new List<CuartoArbitro>();
    CuartoArbitro _cuartoArbitro = null;
    ValidacionGUI _v = new ValidacionGUI();
    DatosColegiados _datos = new DatosColegiados();

    private static AdmCuartoArbitro _admCA = null;

    public List<CuartoArbitro> ListaCuartoArbitro { get => _listaCuartoArbitro; set => _listaCuartoArbitro = value; }

    /// <summary>
    /// Paso para el uso de Singleton.
    /// </summary>
    /// <remarks>
    /// Creando atributo privado de la clase AdmCuartoArbitro.
    /// </remarks>
    private AdmCuartoArbitro() {
      _listaCuartoArbitro = new List<CuartoArbitro>();
    }

    /// <summary>
    /// Paso para el uso de Singleton.
    /// </summary>
    /// <remarks>
    /// Creando atributo estático de la clase Cuarto Árbitro.
    /// </remarks>
    /// <returns>Devuelve una instancia de AdmCuartoArbitro.</returns>
    public static AdmCuartoArbitro GetAdmCA() {
      if(_admCA == null)
        _admCA = new AdmCuartoArbitro();
      return _admCA;
    }

    /// <summary>
    /// Método guardar de la interface IAdm.
    /// </summary>
    /// <param name="txtcedula">Cedula recogida.</param>
    /// <param name="txtnombre">Nombre recogido.</param>
    /// <param name="txtapellido">Apellido recogido.</param>
    /// <param name="txtdomicilio">Domicilio recogido.</param>
    /// <param name="txtemail">Email recogido.</param>
    /// <param name="txttelefono">Telefono recogido.</param>
    /// <returns>Devuelve el último id registrado como entero.</returns>
    public int Guardar(TextBox txtcedula, TextBox txtnombre, TextBox txtapellido,
        TextBox txtdomicilio, TextBox txtemail, TextBox txttelefono) {
      string cedula = txtcedula.Text,
          nombre = txtnombre.Text,
          apellidos = txtapellido.Text,
          domicilio = txtdomicilio.Text,
          email = txtemail.Text,
          telefono = txttelefono.Text;
      int id = 0;

      _cuartoArbitro = new CuartoArbitro(0, cedula, nombre, apellidos, domicilio, email, telefono);

      if(_cuartoArbitro != null) {

        _listaCuartoArbitro.Add(_cuartoArbitro);    //Añadir a la lista
        id = GuardarCuartoArbitroBD(_cuartoArbitro); //Guardar BD
      }
      return id;
    }

    /// <summary>
    /// Guardar datos de Cuarto Árbitro en la BD.
    /// </summary>
    /// <param name="cuartoArbitro">Objeto Cuarto Árbitro.</param>
    /// <returns>Devuelve el último id registrado como entero.</returns>
    private int GuardarCuartoArbitroBD(CuartoArbitro cuartoArbitro) {
      int id = 0;
      string mensaje = "";
      try {
        id = _datos.InsertarCuartoArbitro(cuartoArbitro);
      } catch(FalloBDException ex) {
        mensaje = ex.Message;
        MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }
      return id;
    }

    /// <summary>
    /// Método obtenerDatos de la interface IAdm.
    /// </summary>
    /// <remarks>
    /// Llena <paramref name="dgvListarColegiados"/> con los datos del <paramref name="id"/> buscado.
    /// </remarks>
    /// <param name="id">ID de un Cuarto Árbitro.</param>
    /// <param name="dgvListarColegiados">DataGridView que va a ser llenado con datos.</param>
    public void ObtenerDatos(int id, DataGridView dgvListarColegiados) {
      _listaCuartoArbitro = _datos.ConsultarCuartoArbitro(id);
      foreach(CuartoArbitro datosCA in _listaCuartoArbitro) {
        dgvListarColegiados.Rows.Add("Cuarto Árbitro", datosCA.Cedula, datosCA.Nombre, datosCA.Apellidos, datosCA.Domicilio, datosCA.Email, datosCA.Telefono);
      }
    }

    /// <summary>
    /// Instancia de la clase Cuarto Árbitro.
    /// </summary>
    CuartoArbitro _cA;

    /// <summary>
    /// Método recogerDatosEditar de la interface IAdm.
    /// </summary>
    /// <remarks>
    /// Recoge los datos que son seleccionados para editar por el usuario.
    /// </remarks>
    /// <param name="filaSeleccionada">DataGridViewRow que contiene los datos seleccionado por el usuario.</param>
    public void RecogerDatosEditar(DataGridViewRow filaSeleccionada) {
      foreach(CuartoArbitro cuartoArb in _listaCuartoArbitro) {
        if(cuartoArb.Cedula == filaSeleccionada.Cells[1].Value.ToString() &&
            cuartoArb.Nombre == filaSeleccionada.Cells[2].Value.ToString() &&
            cuartoArb.Apellidos == filaSeleccionada.Cells[3].Value.ToString() &&
            cuartoArb.Domicilio == filaSeleccionada.Cells[4].Value.ToString() &&
            cuartoArb.Email == filaSeleccionada.Cells[5].Value.ToString() &&
            cuartoArb.Telefono == filaSeleccionada.Cells[6].Value.ToString()) {
          _cA = cuartoArb;
        }
      }
    }

    /// <summary>
    /// Método llenarDatosFormEditar de la interface IAdm.
    /// </summary>
    /// <remarks>
    /// Llena los TexBox de Editar con los datos del Cuarto Árbitro seleccionado.
    /// </remarks>
    /// <param name="txtCedula">Cedula.</param>
    /// <param name="txtNombre">Nombre.</param>
    /// <param name="txtApellido">Apellido.</param>
    /// <param name="txtDomicilio">Domicilio.</param>
    /// <param name="txtEmail">Email.</param>
    /// <param name="txtTelefono">Telefono.</param>
    public void LlenarDatosFormEditar(TextBox txtCedula, TextBox txtNombre, TextBox txtApellido, TextBox txtDomicilio, TextBox txtEmail, TextBox txtTelefono) {
      try {
        txtCedula.Text = _cA.Cedula.ToString();
        txtNombre.Text = _cA.Nombre.ToString();
        txtApellido.Text = _cA.Apellidos.ToString();
        txtDomicilio.Text = _cA.Domicilio.ToString();
        txtEmail.Text = _cA.Email.ToString();
        txtTelefono.Text = _cA.Telefono.ToString();
      } catch(FormatException ex) {
        Console.WriteLine(ex.Message);
      }
    }

    /// <summary>
    /// Método editarArbitro de la interface IAdm.
    /// </summary>
    /// <param name="idArbitro">ID recogido.</param>
    /// <param name="cedula">Cedula recogida.</param>
    /// <param name="nombre">Nombre recogido.</param>
    /// <param name="apellido">Apellido recogido.</param>
    /// <param name="domicilio">Domicilio recogido.</param>
    /// <param name="email">Email recogido.</param>
    /// <param name="telefono">Telefono recogido.</param>
    public void EditarArbitro(int idArbitro, string cedula, string nombre, string apellido,
        string domicilio, string email, string telefono) {
      _cuartoArbitro = new CuartoArbitro();
      _cuartoArbitro.IdArbitro = idArbitro;
      _cuartoArbitro.Cedula = cedula;
      _cuartoArbitro.Nombre = nombre;
      _cuartoArbitro.Apellidos = apellido;
      _cuartoArbitro.Domicilio = domicilio;
      _cuartoArbitro.Email = email;
      _cuartoArbitro.Telefono = telefono;

      if(_cuartoArbitro != null) {
        EditarCuartoArbitroBD(_cuartoArbitro);
      }
    }

    /// <summary>
    /// Modificar datos de Cuarto Árbitro en la BD.
    /// </summary>
    /// <param name="cuartoArbitro">Objeto Cuarto Árbitro.</param>
    private void EditarCuartoArbitroBD(CuartoArbitro cuartoArbitro) {
      string mensaje = "";
      try {
        _datos.EditarCuartoArbitro(cuartoArbitro);
        MessageBox.Show("Sus datos fueron actualizados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
      } catch(FalloBDException ex) {
        mensaje = ex.Message;
        MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }
    }

    /// <summary>
    /// Método eliminarArbitro de la interface IAdm
    /// </summary>
    /// <param name="idArbitro">ID recogido.</param>
    /// <param name="cedula">Cedula recogida.</param>
    /// <param name="nombre">Nombre recogido.</param>
    /// <param name="apellido">Apellido recogido.</param>
    /// <param name="domicilio">Domicilio recogido.</param>
    /// <param name="email">Email recogido.</param>
    /// <param name="telefono">Telefono recogido.</param>
    /// <returns>Devuelve el último id registrado como entero.</returns>
    public int EliminarArbitro(int idArbitro, string cedula, string nombre, string apellido,
        string domicilio, string email, string telefono) {
      _cuartoArbitro = new CuartoArbitro();
      _cuartoArbitro.IdArbitro = idArbitro;
      _cuartoArbitro.Cedula = cedula;
      _cuartoArbitro.Nombre = nombre;
      _cuartoArbitro.Apellidos = apellido;
      _cuartoArbitro.Domicilio = domicilio;
      _cuartoArbitro.Email = email;
      _cuartoArbitro.Telefono = telefono;
      int idNuevo = 0;

      if(_cuartoArbitro != null) {
        EliminarCuartoArbitroBD(idArbitro);
        idNuevo = GuardarCuartoArbitroBD(_cuartoArbitro);
      }
      return idNuevo;
    }

    /// <summary>
    /// Eliminar "lógico" en la BD
    /// </summary>
    /// <param name="idArbitro">ID recogido.</param>
    private void EliminarCuartoArbitroBD(int idArbitro) {
      string mensaje = "";
      try {
        _datos.EliminarCuartoArbitroBD(idArbitro);
      } catch(FalloBDException ex) {
        mensaje = ex.Message;
        MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }
    }
  }
}
