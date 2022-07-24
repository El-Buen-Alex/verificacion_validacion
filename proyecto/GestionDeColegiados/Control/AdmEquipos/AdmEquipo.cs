using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Data;

using Model.Equipo;

namespace Control.AdmEquipos {
  public class AdmEquipo {
    private Equipo _equipo = null;
    private List<Equipo> _listaEquipo = new List<Equipo>();
    private static AdmEquipo _admEquipo = null;
    private DatosEquipos _datos = new DatosEquipos();

    /// <summary>
    /// Paso para el uso de Singleton
    /// </summary>
    private AdmEquipo() {

    }


    public void LlenarCampos(TextBox idEquipo, TextBox nombre, TextBox numjugadores, TextBox director, TextBox presidente, string id) {
      _listaEquipo = _datos.ConsultarEquiposTabla();
      foreach(Equipo equipo in _listaEquipo) {
        if(equipo.IdEquipo.Equals(Convert.ToInt32(id))) {

          idEquipo.Text += equipo.IdEquipo;
          nombre.Text = equipo.NombreEquipo;
          numjugadores.Text += equipo.NumeroJugadores;
          director.Text = equipo.NombreDirectoTecnico;
          presidente.Text = equipo.PresidenteEquipo;
        }
      }
    }

    public static AdmEquipo GetEquipo() {
      if(_admEquipo == null) {

        _admEquipo = new AdmEquipo();
      }
      return _admEquipo;
    }

    /// <summary>
    /// Método que consulta la cantidad de equipos que están presentes en la lista donde se agregan los equipos para llevar un control de registro en ella
    /// </summary>
    /// <returns>Devuelve la cantidad de equipos</returns>
    public int CantidadEquiposRegistrados() {
      ExtraerEquipos();
      return _listaEquipo.Count;
    }

    public void EliminarRegistro(string id) {
      int identificador = 0;
      if(id.CompareTo("") != 0) {

        identificador = _datos.EliminarEquipo(id);
        if(identificador != 0) {

          MessageBox.Show("Registro Eliminado con Éxito", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        } else {
          MessageBox.Show("Error al Eliminar los datos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }

    }

    public void ActualizarDatos(int id, string nombre, int numjugadores, string directorNombre, string presidenteNombre) {
      _equipo = new Equipo(id, nombre, numjugadores, directorNombre, presidenteNombre);
      ActualizarRegistroEquipo(_equipo);
    }

    private void ActualizarRegistroEquipo(Equipo equipo) {
      int id = 0;
      if(equipo != null) {

        id = _datos.EditarEquipo(equipo);
        if(id != 0) {

          MessageBox.Show("Actualización de datos exitosa", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        } else {
          MessageBox.Show("Error al actualizar los datos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
    }



    /// <summary>
    /// Método encargado de llenar un el datagridview que es usado para editar o eliminar un equipo previamente ya registrado
    /// </summary>
    /// <param name="tablaDatos"></param>
    /// <param name="nombre"></param>
    public void LlenaTabla(DataGridView tablaDatos, string nombre) {
      int i = 1;
      tablaDatos.Rows.Clear();
      _listaEquipo = _datos.ConsultarEquiposTabla();
      foreach(Equipo equipo in _listaEquipo) {
        if(equipo.NombreEquipo.ToLower().Contains(nombre.ToLower())) {

          tablaDatos.Rows.Add(i++, equipo.IdEquipo, equipo.NombreEquipo, equipo.NumeroJugadores, equipo.NombreDirectoTecnico, equipo.PresidenteEquipo);
        }
      }
    }

    /// <summary>
    /// Método que hace uso del constructor de la clase equipo, agrega a la lista y hace el llamado al método que conecta a la base de datos para facilitar el registros de nuevos equipos
    /// </summary>
    /// <param name="nombre"></param>
    /// <param name="numJugadores"></param>
    /// <param name="directorNombre"></param>
    /// <param name="presidenteNombre"></param>
    public void GuardarDatos(string nombre, int numJugadores, string directorNombre, string presidenteNombre) {
      _equipo = new Equipo(0, nombre, numJugadores, directorNombre, presidenteNombre);
      if(_equipo != null) {

        _listaEquipo.Add(_equipo);
        RegistrarEquipo(_equipo);
      }
    }

    /// <summary>
    /// Método encargado de llenar los labels con informacion para ser presentada al usuario donde sea invocado
    /// </summary>
    /// <param name="listaContenedores"></param>
    public void LlenarEquipos(List<Label> listaContenedores) {
      ExtraerEquipos();
      for(int x = 0; x < _listaEquipo.Count; x++) {
        listaContenedores[x].Text = _listaEquipo[x].NombreEquipo;
      }
    }

    /// <summary>
    /// método encargado de funcionar como puente entre los métodos de control con el método de data para obtener la cantidad de equipos registrados en la base de datos
    /// </summary>
    /// <returns></returns>
    public int ObtenerCantidadEquipo() {
      return _datos.ObtenerCantidadEquipoRegistrados();
    }

    /// <summary>
    /// Método encargado de funcionar como puente entre los métodos de control con el método de data para solicitar el id en la base de datos
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Retorna el equipo mediante su ID</returns>
    public Equipo ObtenerEquipoPorId(int id) {
      return _datos.ObtenerEquipoPorId(id);
    }

    /// <summary>
    /// Método encargado de funcionar como puente entre los métodos de control con el método de data para el registro en la base de datos
    /// </summary>
    /// <param name="equipo"></param>
    private void RegistrarEquipo(Equipo equipo) {
      int id = 0;
      id = _datos.InsertarEquipo(equipo);
      if(id == 0) {

        MessageBox.Show("Error al registrar el equipo");
      } else {
        MessageBox.Show("Registro exitoso!");
      }
    }

    /// <summary>
    /// Solicita los campos de la base de datos a otro metodo que hace la funcionalidad de extraerlos de  la BD
    /// </summary>
    /// <returns>Retorna algun equipo</returns>
    public List<Equipo> ExtraerEquipos() {
      _listaEquipo = _datos.ConsultarEquipos();
      return _listaEquipo;

    }

    public void LlenarEquiposCmb(ComboBox cmbEquipos, List<Equipo> equiposAux) {
      if(equiposAux != null && equiposAux.Count > 0) {

        cmbEquipos.DataSource = null;
        cmbEquipos.DisplayMember = "nombreEquipo";
        cmbEquipos.DataSource = equiposAux;
        cmbEquipos.SelectedIndex = -1;
      }
    }
    private int SeleccionarEquipoCmb(string equipo, List<Equipo> equiposAux) {
      int index = 0, iterador = 0;
      foreach(Equipo e in equiposAux) {
        if(e.NombreEquipo == equipo) {

          index = iterador;
        }
        iterador++;
      }
      return index;
    }

    public void ObserverCmbEquipos(ComboBox cmbEquipos, string equipo, string equipoSelect) {
      List<Equipo> equiposAux = ExtraerEquipos();
      int index = 0, iterador = 0;
      foreach(Equipo e in equiposAux) {
        if(e.NombreEquipo == equipo) {

          index = iterador;
        }
        iterador++;
      }
      equiposAux.RemoveAt(index);
      LlenarEquiposCmb(cmbEquipos, equiposAux);
      cmbEquipos.SelectedIndex = SeleccionarEquipoCmb(equipoSelect, equiposAux);
    }


  }
}
