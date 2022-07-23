using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Data;

using Model.Colegiados;

namespace Control.AdmColegiados {
  /// <summary>
  /// Clase para la gestión de Juez Central.
  /// </summary>
  /// <remarks>
  /// Crea las listas, instancias y validaciones para obtener los datos de Juez Central.
  /// </remarks>
  public class AdmJuezCentral : IAdm {
    List<JuezCentral> _listaJuezCentral = new List<JuezCentral>();
    JuezCentral _juezCentral = null;
    ValidacionGUI _v = new ValidacionGUI();
    DatosColegiados _datos = new DatosColegiados();

    private static AdmJuezCentral _admJ = null;

    public List<JuezCentral> ListaJuezCentral { get => _listaJuezCentral; set => _listaJuezCentral = value; }

    /// <summary>
    /// Paso para el uso de Singleton.
    /// </summary>
    /// <remarks>
    /// Creando atributo privado de la clase AdmJuezCentral.
    /// </remarks>
    private AdmJuezCentral() {
      _listaJuezCentral = new List<JuezCentral>();
    }

    /// <summary>
    /// Paso para el uso de Singleton.
    /// </summary>
    /// <remarks>
    /// Creando atributo estático de la clase Juez Central.
    /// </remarks>
    /// <returns>Devuelve una instancia de AdmJuezCentral.</returns>
    public static AdmJuezCentral GetAdmJ() {
      if(_admJ == null)
        _admJ = new AdmJuezCentral();
      return _admJ;
    }

    /// <summary>
    /// Método Guardar de la interface IAdm.
    /// </summary>
    /// <param name="txtcedulaJC">Cedula recogida.</param>
    /// <param name="txtnombreJC">Nombre recogido.</param>
    /// <param name="txtapellidoJC">Apellido recogido.</param>
    /// <param name="txtdomicilioJC">Domicilio recogido.</param>
    /// <param name="txtemailJC">Email recogido.</param>
    /// <param name="txttelefonoJC">Telefono recogido.</param>
    /// <returns>Devuelve el último id registrado como entero.</returns>
    public int Guardar(TextBox txtcedulaJC, TextBox txtnombreJC,
                       TextBox txtapellidoJC, TextBox txtdomicilioJC,
                       TextBox txtemailJC, TextBox txttelefonoJC) {
      string cedula = txtcedulaJC.Text,
          nombre = txtnombreJC.Text,
          apellidos = txtapellidoJC.Text,
          domicilio = txtdomicilioJC.Text,
          email = txtemailJC.Text,
          telefono = txttelefonoJC.Text;
      int id = 0;

      _juezCentral = new JuezCentral(0, cedula, nombre, apellidos, domicilio, email, telefono);

      if(_juezCentral != null) {

        _listaJuezCentral.Add(_juezCentral);      //Añadir a la lista
        id = GuardarJuezCentralBD(_juezCentral); //Guardar BD
      }
      return id;
    }

    /// <summary>
    /// Guardar datos de Juez Central en la BD.
    /// </summary>
    /// <param name="juezCentral">Objeto Juez Central.</param>
    /// <returns>Devuelve el último id registrado como entero.</returns>
    private int GuardarJuezCentralBD(JuezCentral juezCentral) {
      int id = 0;
      string mensaje = "";
      try {
        id = _datos.InsertarJuezCentral(juezCentral);
      } catch(FalloBDException ex) {
        mensaje = ex.Message;
        MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }
      return id;
    }

    /// <summary>
    /// Método ObtenerDatos de la interface IAdm.
    /// </summary>
    /// <remarks>
    /// Llena <paramref name="dgvListarColegiados"/> con los datos del <paramref name="id"/> buscado.
    /// </remarks>
    /// <param name="id">ID de un Juez Central.</param>
    /// <param name="dgvListarColegiados">DataGridView que va a ser llenado con datos.</param>
    public void ObtenerDatos(int id, DataGridView dgvListarColegiados) {
      _listaJuezCentral = _datos.ConsultarJuezCentral(id);
      foreach(JuezCentral datosJC in _listaJuezCentral) {
        dgvListarColegiados.Rows.Add("Juez Central", datosJC.Cedula, datosJC.Nombre,
            datosJC.Apellidos, datosJC.Domicilio, datosJC.Email, datosJC.Telefono);
      }
    }

    /// <summary>
    /// Instancia de la clase Asistente.
    /// </summary>
    JuezCentral _jC;

    /// <summary>
    /// Método RecogerDatosEditar de la interface IAdm.
    /// </summary>
    /// <remarks>
    /// Recoge los datos que son seleccionados para editar por el usuario.
    /// </remarks>
    /// <param name="filaSeleccionada">DataGridViewRow que contiene los datos seleccionado por el usuario.</param>
    public void RecogerDatosEditar(DataGridViewRow filaSeleccionada) {
      foreach(JuezCentral juezCentral in _listaJuezCentral) {
        if(juezCentral.Cedula == filaSeleccionada.Cells[1].Value.ToString() &&
            juezCentral.Nombre == filaSeleccionada.Cells[2].Value.ToString() &&
            juezCentral.Apellidos == filaSeleccionada.Cells[3].Value.ToString() &&
            juezCentral.Domicilio == filaSeleccionada.Cells[4].Value.ToString() &&
            juezCentral.Email == filaSeleccionada.Cells[5].Value.ToString() &&
            juezCentral.Telefono == filaSeleccionada.Cells[6].Value.ToString()) {

          _jC = juezCentral;
        }
      }
    }

    /// <summary>
    /// Método LlenarDatosFormEditar de la interface IAdm.
    /// </summary>
    /// <remarks>
    /// Llena los TexBox de Editar con los datos del Juez Central seleccionado.
    /// </remarks>
    /// <param name="txtCedula">Cedula.</param>
    /// <param name="txtNombre">Nombre.</param>
    /// <param name="txtApellido">Apellido.</param>
    /// <param name="txtDomicilio">Domicilio.</param>
    /// <param name="txtEmail">Email.</param>
    /// <param name="txtTelefono">Telefono.</param>
    public void LlenarDatosFormEditar(TextBox txtCedula, TextBox txtNombre,
                                      TextBox txtApellido,TextBox txtDomicilio, 
                                      TextBox txtEmail, TextBox txtTelefono) {
      try {
        txtCedula.Text = _jC.Cedula.ToString();
        txtNombre.Text = _jC.Nombre.ToString();
        txtApellido.Text = _jC.Apellidos.ToString();
        txtDomicilio.Text = _jC.Domicilio.ToString();
        txtEmail.Text = _jC.Email.ToString();
        txtTelefono.Text = _jC.Telefono.ToString();
      } catch(FormatException ex) {
        Console.WriteLine(ex.Message);
      }
    }

    /// <summary>
    /// Método EditarArbitro de la interface IAdm.
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
      _juezCentral = new JuezCentral();
      _juezCentral.IdArbitro = idArbitro;
      _juezCentral.Cedula = cedula;
      _juezCentral.Nombre = nombre;
      _juezCentral.Apellidos = apellido;
      _juezCentral.Domicilio = domicilio;
      _juezCentral.Email = email;
      _juezCentral.Telefono = telefono;

      if(_juezCentral != null) {

        EditarJuezCentralBD(_juezCentral);  //BD
      }
    }

    /// <summary>
    /// Modificar datos de Asistente1 en la BD.
    /// </summary>
    /// <param name="juezCentral">Objeto Juez Central.</param>
    private void EditarJuezCentralBD(JuezCentral juezCentral) {
      string mensaje = "";
      try {
        _datos.EditarJuezCentralBD(juezCentral);
        MessageBox.Show("Sus datos fueron actualizados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
      } catch(FalloBDException ex) {
        mensaje = ex.Message;
        MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }
    }

    /// <summary>
    /// Método EliminarArbitro de la interface IAdm.
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
      _juezCentral = new JuezCentral();
      _juezCentral.IdArbitro = idArbitro;
      _juezCentral.Cedula = cedula;
      _juezCentral.Nombre = nombre;
      _juezCentral.Apellidos = apellido;
      _juezCentral.Domicilio = domicilio;
      _juezCentral.Email = email;
      _juezCentral.Telefono = telefono;
      int idNuevo = 0;

      if(_juezCentral != null) {
        EliminarJuezCentralBD(idArbitro);
        idNuevo = GuardarJuezCentralBD(_juezCentral);
      }
      return idNuevo;
    }

    /// <summary>
    /// Eliminar "lógico" en la BD.
    /// </summary>
    /// <param name="idArbitro">ID recogido.</param>
    private void EliminarJuezCentralBD(int idArbitro) {
      string mensaje = "";
      try {
        _datos.EliminarJuezCentralBD(idArbitro);
      } catch(FalloBDException ex) {
        mensaje = ex.Message;
        MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }
    }
  }
}