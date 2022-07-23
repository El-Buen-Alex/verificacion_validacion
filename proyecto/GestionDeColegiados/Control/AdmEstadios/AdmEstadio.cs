using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Data;

using Model;

namespace Control.AdmEstadios {
  public class AdmEstadio {
    private static AdmEstadio _admEstadio = null;
    private List<Estadio> _listaEstadiosDisponibles;
    private DatosEstadios _datosEstadios = new DatosEstadios();

    private AdmEstadio() {
      _listaEstadiosDisponibles = new List<Estadio>();
    }

    public List<Estadio> ListaEstadiosDisponibles { get => _listaEstadiosDisponibles; set => _listaEstadiosDisponibles = value; }

    public static AdmEstadio GetAdmEstadio() {
      if(_admEstadio == null) {

        _admEstadio = new AdmEstadio();
      }
      return _admEstadio;
    }
    public void RefrezcarListaEstadiosDisponibles() {
      _listaEstadiosDisponibles = _datosEstadios.ObtenerEstadiosDisponibles();
    }
    public void LlenarEstadiosCmb(ComboBox cmbEstadio) {
      cmbEstadio.DataSource = null;
      _listaEstadiosDisponibles = _datosEstadios.ObtenerEstadiosDisponibles();
      cmbEstadio.DisplayMember = "nombre";
      cmbEstadio.DataSource = _listaEstadiosDisponibles;
    }
    public void LlenarEstadiosCmb(ComboBox cmbEstadio, Estadio estadio) {
      cmbEstadio.DataSource = null;
      _listaEstadiosDisponibles = _datosEstadios.ObtenerEstadiosDisponibles();
      _listaEstadiosDisponibles.Insert(0, estadio);
      cmbEstadio.DisplayMember = "nombre";
      cmbEstadio.DataSource = _listaEstadiosDisponibles;
    }
    public Estadio ObtenerEstadioPorId(int idEstadio) {
      return _datosEstadios.ObtenerEstadioPorId(idEstadio);
    }
    public bool CambiarEstadoEstadio(int idEsadio, string estado) {
      bool cambio = false;
      cambio = _datosEstadios.CambiarEstado(idEsadio, estado);
      if(!cambio) {

        throw new ErrorActualizarEstadioException("Error en CambiarEstadoEstadio-AdmEstadio");
      }

      return cambio;
    }

    public void PonerEstadiosDisponibles() {
      bool respuesta = _datosEstadios.PutEstadiosDisponibles();
    }

    public void SeleccionarEstadio(ComboBox cmbEstadios, Estadio estadio) {
      LlenarEstadiosCmb(cmbEstadios, estadio);
      cmbEstadios.SelectedIndex = 0;
    }

    internal Estadio ObtenerEstadioPorIndex(int indexEncuentroSeleccionado) {
      throw new NotImplementedException();
    }
  }
}
