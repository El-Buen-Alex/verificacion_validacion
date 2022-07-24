using System;

namespace Model.Equipo {
  /// <summary>
  /// Clase Equipo
  /// </summary>
  public class Equipo {/// <summary>
                       /// Atributos privados de la clase Equipo
                       /// </summary>
    private int _idEquipo;
    private String _nombreEquipo;
    private int _numeroJugadores;
    private String _nombreDirectoTecnico;
    private String _presidenteEquipo;
    /// <summary>
    /// COnstructor por defecto
    /// </summary>
    public Equipo() {
    }
    /// <summary>
    /// Constructor sin el id del equipo
    /// </summary>
    /// <param name="nombreEquipo">Nombre del equipo</param>
    /// <param name="numeroJugadores">Numero de jugadores</param>
    /// <param name="nombreDirectoTecnico">Nombre del director Técnico del equipo</param>
    /// <param name="presidenteEquipo">Presidente del equipo</param>
    public Equipo(string nombreEquipo, int numeroJugadores, string nombreDirectoTecnico, string presidenteEquipo) {
      this._nombreEquipo = nombreEquipo;
      this._numeroJugadores = numeroJugadores;
      this._nombreDirectoTecnico = nombreDirectoTecnico;
      this._presidenteEquipo = presidenteEquipo;
    }
    /// <summary>
    /// Constructor parametrizado
    /// </summary>
    /// <param name="idEquipo">Identificador del equipo</param>
    /// <param name="nombreEquipo">Nombre del equipo</param>
    /// <param name="numeroJugadores">Numero de jugadores</param>
    /// <param name="nombreDirectoTecnico">Nombre del director Técnico del equipo</param>
    /// <param name="presidenteEquipo">Presidente del equipo</param>
    public Equipo(int idEquipo, string nombreEquipo, int numeroJugadores, string nombreDirectoTecnico, string presidenteEquipo) {
      this._idEquipo = idEquipo;
      this._nombreEquipo = nombreEquipo;
      this._numeroJugadores = numeroJugadores;
      this._nombreDirectoTecnico = nombreDirectoTecnico;
      this._presidenteEquipo = presidenteEquipo;
    }
    /// <summary>
    /// Métodos getter y setter de los atributos de clase
    /// </summary>
    public int IdEquipo { get => _idEquipo; set => _idEquipo = value; }
    public string NombreEquipo { get => _nombreEquipo; set => _nombreEquipo = value; }
    public int NumeroJugadores { get => _numeroJugadores; set => _numeroJugadores = value; }
    public string NombreDirectoTecnico { get => _nombreDirectoTecnico; set => _nombreDirectoTecnico = value; }
    public string PresidenteEquipo { get => _presidenteEquipo; set => _presidenteEquipo = value; }

    public override string ToString() { return base.ToString(); }
  }
}