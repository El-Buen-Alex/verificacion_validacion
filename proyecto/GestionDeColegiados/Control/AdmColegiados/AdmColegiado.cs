using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Data;

using Model;
using Model.Colegiados;

namespace Control.AdmColegiados {
  /// <summary>
  /// Clase para la gestión de Colegiados.
  /// </summary>
  /// <remarks>
  /// Crea las listas, instancias y validaciones para obtener los datos de Colegiados.
  /// </remarks>
  public class AdmColegiado {
    List<Colegiado> _listaColegiado = new List<Colegiado>();
    Colegiado _colegiado = null;
    ValidacionGUI _v = new ValidacionGUI();
    DatosColegiados _datos = new DatosColegiados();
    DatosEncuentroDefinido _datosEncuentroDefinido = null;
    private List<IntegrantesColegiados> _listaintegColeg;
    private static AdmColegiado _admCol = null;
    Contexto _contexto = null;
    DataGridViewRow _filaSeleccionada = null;

    public List<Colegiado> ListaColegiado { get => _listaColegiado; set => _listaColegiado = value; }
    public List<IntegrantesColegiados> ListaintegColeg { get => _listaintegColeg; set => _listaintegColeg = value; }

    /// <summary>
    /// Paso para el uso de Singleton.
    /// </summary>
    /// <remarks>
    /// Creando atributo privado de la clase AdmColegiado.
    /// </remarks>
    private AdmColegiado() {
      _listaColegiado = new List<Colegiado>();
    }

    /// <summary>
    /// Paso para el uso de Singleton.
    /// </summary>
    /// <remarks>
    /// Creando atributo estático de la clase AdmColegiado.
    /// </remarks>
    /// <returns>Devuelve una instancia de AdmColegiado.</returns>
    public static AdmColegiado GetAdmCol() {
      if(_admCol == null)
        _admCol = new AdmColegiado();
      return _admCol;
    }

    /// <summary>
    /// Llenar ComboBox con nombres de Juez Central.
    /// </summary>
    /// <param name="cmbGrupoColegiado"> ComboBox recogido.</param>
    public void LlenarColegiadosCmb(ComboBox cmbGrupoColegiado) {
      _listaintegColeg = new List<IntegrantesColegiados>();
      _listaintegColeg = _datos.ConsultarColegiado();
      cmbGrupoColegiado.DisplayMember = "NombrejuezCentral";
      cmbGrupoColegiado.DataSource = _listaintegColeg;

    }

    /// <summary>
    /// LLenar lista de integrantes de colegiados.
    /// </summary>
    public void LlenarListaColegiados() {
      _listaintegColeg = _datos.ConsultarColegiado();
    }

    /// <summary>
    /// Guardar id de todos los arbitros.
    /// </summary>
    /// <param name="idjuezcentral">ID del juez central.</param>
    /// <param name="idasistente1">ID del asistente 1.</param>
    /// <param name="idasistente2">ID del asistente 2.</param>
    /// <param name="idcuartoarbitro">ID del cuarto arbitro.</param>
    public void Guardar(int idjuezcentral, int idasistente1, int idasistente2, int idcuartoarbitro) {
      _colegiado = new Colegiado(0, idjuezcentral, idasistente1, idasistente2, idcuartoarbitro);

      if(_colegiado != null) {
        _listaColegiado.Add(_colegiado);
        GuardarColegiadoBD(_colegiado); //Guardar BD
      }
    }

    /// <summary>
    /// Guardar datos de colegiado en la BD.
    /// </summary>
    /// <param name="colegiado">Objeto colegiado.</param>
    private void GuardarColegiadoBD(Colegiado colegiado) {
      string mensaje = "";
      try {
        _datos.InsertarColegiado(colegiado);
        MessageBox.Show("Se ha guardado correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
      } catch(FalloBDException ex) {
        mensaje = ex.Message;
        MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        MessageBox.Show("No se ha guardado el colegiado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    }

    /// <summary>
    /// Listar datos con los nombres de los colegiados.
    /// </summary>
    /// <param name="cmbIdArbitro">ComboBox seleccionado de colegiado.</param>
    public void LlenarComboIdColegiado(ComboBox cmbIdArbitro) {
      cmbIdArbitro.DataSource = null;
      cmbIdArbitro.Items.Clear();

      List<int> listaIdArbitro = new List<int>();
      listaIdArbitro = _datos.ConsultarIdColegiado();
      foreach(int datosId in listaIdArbitro) {
        cmbIdArbitro.Items.Add("Grupo " + datosId);
      }
    }

    /// <summary>
    /// Llenar el DataGridView con los datos de los árbitros.
    /// </summary>
    /// <remarks>
    /// Se llena el DataGridView de acuerdo a un contexto de árbitros determinado.
    /// </remarks>
    /// <param name="dgvListarColegiados">DataGridView que se va a llenar con los datos de los árbitros.</param>
    /// <param name="cmbIdArbitro">Combobox o grupo de colegiado seleccionado.</param>
    public void LlenarDatosGrivColegiado(DataGridView dgvListarColegiados, ComboBox cmbIdArbitro) {
      dgvListarColegiados.Rows.Clear();

      string colegiadoSeleccionado = cmbIdArbitro.Text;
      char delimitador = ' ';
      string[] cadena = colegiadoSeleccionado.Split(delimitador);
      int id = Convert.ToInt32(cadena[1]);

      _contexto = new Contexto(AdmJuezCentral.GetAdmJ());
      _contexto.Datos(id, dgvListarColegiados);

      _contexto = new Contexto(AdmAsistente1.GetAdmA1());
      _contexto.Datos(id, dgvListarColegiados);

      _contexto = new Contexto(AdmAsistente2.getAdmA2());
      _contexto.Datos(id, dgvListarColegiados);

      _contexto = new Contexto(AdmCuartoArbitro.getAdmCA());
      _contexto.Datos(id, dgvListarColegiados);
    }

    /// <summary>
    /// Obtener nombres indexados de colegiados.
    /// </summary>
    /// <param name="indexColegiados">Numero index de colegiado.</param>
    /// <returns>Devuelve el id del colegiado consultado como entero.</returns>
    public string ObtenerNombreDeColegiadosIndex(int indexColegiados) {
      int id = _listaintegColeg[indexColegiados].IdGrupoColegiado;
      return ObtenerNombreDeColegiados(id);
    }

    /// <summary>
    /// Obtener nombres de colegiado consultado.
    /// </summary>
    /// <param name="idColegiado">ID del colegiado que se va a buscar.</param>
    /// <returns>Delvuelve el nombre del colegiado consultado como string.</returns>
    public string ObtenerNombreDeColegiados(int idColegiado) {
      string nombres = "0";
      nombres = _datos.ObtenerNombreDeColegiados(idColegiado);
      return nombres;
    }

    /// <summary>
    /// Obtener Cantidad de Colegiados.
    /// </summary>
    /// <returns>Devuelve la cantidad de colegiados registrados.</returns>
    public int ObtenerCantidadColegiado() {
      _listaintegColeg = _datos.ConsultarColegiado();
      return _listaintegColeg.Count;
    }

    /// <summary>
    /// Consulta para validar si el arbitro ya está registrado.
    /// </summary>
    /// <param name="txtcedula">Cedula que se va a validar.</param>
    /// <returns>Devuelve true si la cedula está repetida o false si es una nueva.</returns>
    public bool ValidarCedula(TextBox txtcedula) {
      string cedula = txtcedula.Text;
      bool repetido = false;
      _listaintegColeg = _datos.ConsultarCedulaColegiado();
      foreach(IntegrantesColegiados integ in _listaintegColeg) {
        if(integ.NombrejuezCentral == cedula || integ.Nombreasistente1 == cedula ||
            integ.Nombreasistente2 == cedula || integ.NombrecuartoArbitro == cedula) {
          repetido = true;
          break;
        }
      }
      return repetido;
    }

    /// <summary>
    /// Llenar ComboBox con los nombres de arbitros registrados como colegiado.
    /// </summary>
    /// <param name="cmbGrupoColegiado"></param>
    /// <param name="idColegiados"></param>
    public void LlenarColegiadosCmb(ComboBox cmbGrupoColegiado, int idColegiados) {
      LlenarColegiadosCmb(cmbGrupoColegiado);
      int i = 0;
      foreach(IntegrantesColegiados item in _listaintegColeg) {
        if(item.IdGrupoColegiado == idColegiados) {
          cmbGrupoColegiado.SelectedIndex = i;
        }
        i++;
      }
    }

    /// <summary>
    /// Método estático para obtener la implementación de un contexto.
    /// </summary>
    /// <remarks>
    /// La implementación se obtiene de acuerdo a una fila seleccionada de un DataGridView.
    /// </remarks>
    /// <param name="filaSeleccionada">Fila seleccionada del DataGridview</param>
    /// <returns>Devuelve la implementación de un contexto de árbitro.</returns>
    private static Contexto EscogerArbitro(DataGridViewRow filaSeleccionada) {
      Contexto contextoArbitro = null;
      string arbitro = filaSeleccionada.Cells[0].Value.ToString();
      if(arbitro == "Juez Central") {
        contextoArbitro = new Contexto(AdmJuezCentral.GetAdmJ());
      }
      if(arbitro == "Asistente 1") {
        contextoArbitro = new Contexto(AdmAsistente1.GetAdmA1());
      }
      if(arbitro == "Asistente 2") {
        contextoArbitro = new Contexto(AdmAsistente2.getAdmA2());
      }
      if(arbitro == "Cuarto Árbitro") {
        contextoArbitro = new Contexto(AdmCuartoArbitro.getAdmCA());
      }
      return contextoArbitro;
    }

    /// <summary>
    /// Método para recoger los datos del colegiado seleccionado.
    /// </summary>
    /// <param name="dgvListarColegiados">Datos obtenidos del DataGridView de colegiados.</param>
    public void RecogerDatosEditar(DataGridView dgvListarColegiados) {
      _filaSeleccionada = dgvListarColegiados.CurrentRow;
      _contexto = EscogerArbitro(_filaSeleccionada);
      _contexto.RecogerDatosEditar(_filaSeleccionada);
    }

    /// <summary>
    /// Llenar FrmEditar con datos.
    /// </summary>
    /// <param name="txtCedula">Cedula.</param>
    /// <param name="txtNombre">Nombre.</param>
    /// <param name="txtApellido">Apellido.</param>
    /// <param name="txtDomicilio">Domicilio.</param>
    /// <param name="txtEmail">Email.</param>
    /// <param name="txtTelefono">Telefono.</param>
    public void LlenarDatosFormEditar(TextBox txtCedula, TextBox txtNombre, TextBox txtApellido, TextBox txtDomicilio, TextBox txtEmail, TextBox txtTelefono) {
      _contexto = EscogerArbitro(_filaSeleccionada);
      _contexto.LlenarDatosFormEditar(txtCedula, txtNombre, txtApellido, txtDomicilio, txtEmail, txtTelefono);
    }

    /// <summary>
    /// Método para obtener el id del algun arbitro seleccionado.
    /// </summary>
    /// <param name="idColegiado">ID del grupo del colegiado al que pertenece el arbitro.</param>
    /// <param name="filaSeleccionada">Datos del arbitro seleccionado en el DataGridView.</param>
    /// <returns>Devuelve el id del arbitro seleccionado como entero.</returns>
    private int ObtenerIDArbitro(int idColegiado, DataGridViewRow filaSeleccionada) {
      int idArbitro = 0;
      _listaColegiado = _datos.ObtenerTodosIdColegiado(idColegiado);
      string arbitro = filaSeleccionada.Cells[0].Value.ToString();
      if(arbitro == "Juez Central") {
        foreach(Colegiado col in _listaColegiado) {
          idArbitro = col.Idjuezcentral;
        }
      }
      if(arbitro == "Asistente 1") {
        foreach(Colegiado col in _listaColegiado) {
          idArbitro = col.Idasistente1;
        }
      }
      if(arbitro == "Asistente 2") {
        foreach(Colegiado col in _listaColegiado) {
          idArbitro = col.Idasistente2;
        }
      }
      if(arbitro == "Cuarto Árbitro") {
        foreach(Colegiado col in _listaColegiado) {
          idArbitro = col.Idcuartoarbitro;
        }
      }
      return idArbitro;
    }

    /// <summary>
    /// Método para editar el árbitro seleccionado.
    /// </summary>
    /// <param name="lblID">ID del colegiado al que pertenece el árbitro.</param>
    /// <param name="cedula">Cedula.</param>
    /// <param name="nombre">Nombre.</param>
    /// <param name="apellido">Apellido.</param>
    /// <param name="domicilio">Domicilio.</param>
    /// <param name="email">Email.</param>
    /// <param name="telefono">Teléfono.</param>
    public void EditarArbitro(string lblID, string cedula, string nombre, string apellido, string domicilio, string email, string telefono) {
      char delimitador = ' ';
      string[] cadena = lblID.Split(delimitador);
      int idColegiado = Convert.ToInt32(cadena[1]);
      int idArbitro = ObtenerIDArbitro(idColegiado, _filaSeleccionada);
      _contexto = EscogerArbitro(_filaSeleccionada);
      _contexto.EditarArbitro(idArbitro, cedula, nombre, apellido, domicilio, email, telefono);
    }

    /// <summary>
    /// Recoger datos para eliminar el árbitro seleccionado.
    /// </summary>
    /// <param name="dgvListarColegiados">Datos del DataGridView.</param>
    public void RecogerDatosEliminar(DataGridView dgvListarColegiados) {
      _filaSeleccionada = dgvListarColegiados.CurrentRow;
    }

    /// <summary>
    /// Método eliminar árbitro 
    /// </summary>
    /// <param name="lblID">ID del colegiado al que pertenece el árbitro.</param>
    /// <param name="cedula">Cedula.</param>
    /// <param name="nombre">Nombre.</param>
    /// <param name="apellido">Apellido.</param>
    /// <param name="domicilio">Domicilio.</param>
    /// <param name="email">Email.</param>
    /// <param name="telefono">Teléfono.</param>
    public void EliminarArbitro(string lblID, string cedula, string nombre, string apellido, string domicilio, string email, string telefono) {
      char delimitador = ' ';
      string[] cadena = lblID.Split(delimitador);
      int idColegiado = Convert.ToInt32(cadena[1]);
      int idArbitro = ObtenerIDArbitro(idColegiado, _filaSeleccionada);
      int idNuevo = 0;
      _contexto = EscogerArbitro(_filaSeleccionada);
      idNuevo = _contexto.EliminarArbitro(idArbitro, cedula, nombre, apellido, domicilio, email, telefono);
      if(idNuevo != 0) {
        string arbitro = _filaSeleccionada.Cells[0].Value.ToString();
        ActualizarColegiadoBD(idColegiado, idNuevo, arbitro);
      }
    }

    /// <summary>
    /// Actualizar el id del nuevo colegiado agregado después de eliminar.
    /// </summary>
    /// <param name="idColegiado">ID del colegiado.</param>
    /// <param name="idNuevo">El nuevo ID generado después de insertar.</param>
    /// <param name="arbitro">Tipo de árbitro.</param>
    private void ActualizarColegiadoBD(int idColegiado, int idNuevo, string arbitro) {
      string mensaje = "";
      try {
        _datos.ActualizarColegiadoBD(idColegiado, idNuevo, arbitro);
        MessageBox.Show("Sus datos fueron agregados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
      } catch(FalloBDException ex) {
        mensaje = ex.Message;
        MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }
    }

    /// <summary>
    /// Método para eliminar colegiado completo.
    /// </summary>
    /// <param name="colegiadoSeleccionado">Obtener el colegiado seleccionado.</param>
    /// <returns>Devuelve true si fue eliminado o false si ocurrió algún error.</returns>
    public bool EliminarColegiado(string colegiadoSeleccionado) {
      bool eliminado = false;
      char delimitador = ' ';
      string[] cadena = colegiadoSeleccionado.Split(delimitador);
      int idColegiado = Convert.ToInt32(cadena[1]);
      bool arbitroAsignado = ValidarArbitroAsignado(idColegiado);
      if(arbitroAsignado != true) {
        EliminarColegiadoBD(idColegiado);
        eliminado = true;
      } else {
        MessageBox.Show("El " + colegiadoSeleccionado + " no se puede eliminar porque\nya se encuentra asignado en un encuentro!!!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      return eliminado;
    }

    /// <summary>
    /// Validar si el árbitro ya está asignado.
    /// </summary>
    /// <param name="idColegiado">ID del colegiado al cual pertenece el árbitro.</param>
    /// <returns>Devueleve true si el árbitro fue asignado o false si no está asignado.</returns>
    private bool ValidarArbitroAsignado(int idColegiado) {
      List<EncuentroDefinido> listaEncuentro = new List<EncuentroDefinido>();
      _datosEncuentroDefinido = new DatosEncuentroDefinido();
      listaEncuentro = _datosEncuentroDefinido.ObtenerEncuentros();
      foreach(EncuentroDefinido encuentroDefinido in listaEncuentro) {
        if(encuentroDefinido.IdColegiado == idColegiado) {
          return true;
        }
      }
      return false;
    }

    /// <summary>
    /// Eliminar "lógico" en la BD de colegiado.
    /// </summary>
    /// <param name="idColegiado">ID del colegiado a eliminar.</param>
    private void EliminarColegiadoBD(int idColegiado) {
      string mensaje = "";
      try {
        _datos.EliminarColegiado(idColegiado);
        MessageBox.Show("Se eliminó el colegiado correctamente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
      } catch(FalloBDException ex) {
        mensaje = ex.Message;
        MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }
    }
  }
}