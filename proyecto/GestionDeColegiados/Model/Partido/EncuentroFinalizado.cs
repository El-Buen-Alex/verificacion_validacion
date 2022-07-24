using System;

namespace Model.Partido {
  public class EncuentroFinalizado {
    private int _id;
    private int _golesFavor;
    private int _golesContra;
    private int _golesDiferencia;
    private int _puntos;
    private int _idEquipo;
    private string _copa;
    private string _estado;
    private int _idEncuentroDefinido;

    public EncuentroFinalizado() { }

    private int CalcularPuntos(int golFavor, int golContra) {
      int puntos = 0;
      if(golFavor > golContra) {
        puntos = 3;
      } else if(golFavor < golContra) {
        puntos = 0;
      } else if(golFavor == golContra) {
        puntos = 1;
      }
      return puntos;
    }
    public EncuentroFinalizado(int idEquipo, int golFavor, int golContra, int idEncuentroDefinido) {
      this._idEquipo = idEquipo;
      this.GolesFavor = golFavor;
      this.GolesContra = golContra;
      this._golesDiferencia = this.GolesFavor - this.GolesContra;
      this._puntos = CalcularPuntos(golFavor, golContra);
      this._idEncuentroDefinido = idEncuentroDefinido;
      this._copa = "" + DateTime.Now.Year;
    }

    public EncuentroFinalizado(int golesFavor, int golesContra,
       int golesDiferencia, int puntos, int idEquipo, string copa, string estado, int idEncuentroDefinido) {
      this._id = 0;
      this._golesFavor = golesFavor;
      this._golesContra = golesContra;
      this._golesDiferencia = golesDiferencia;
      this._puntos = puntos;
      this._idEquipo = idEquipo;
      this._copa = copa;
      this._estado = estado;
      this._golesDiferencia = this.GolesFavor - this.GolesContra;
      this._idEncuentroDefinido = idEncuentroDefinido;
    }

    public EncuentroFinalizado(int id, int golesFavor, int golesContra,
        int golesDiferencia, int puntos, int idEquipo, string copa, string estado, int idEncuentroDefinido) {
      this._id = id;
      this._golesFavor = golesFavor;
      this._golesContra = golesContra;
      this._golesDiferencia = golesDiferencia;
      this._puntos = puntos;
      this._idEquipo = idEquipo;
      this._copa = copa;
      this._estado = estado;
      this._golesDiferencia = this.GolesFavor - this.GolesContra;
      this._idEncuentroDefinido = idEncuentroDefinido;
      this._puntos = CalcularPuntos(this._golesFavor, this.GolesContra);
    }

    public int Id { get => _id; set => _id = value; }

    public int GolesFavor { get => _golesFavor; set => _golesFavor = value; }

    public int GolesContra { get => _golesContra; set => _golesContra = value; }

    public int GolesDiferencia { get => _golesDiferencia; set => _golesDiferencia = value; }

    public int Puntos { get => _puntos; set => _puntos = value; }

    public int IdEquipo { get => _idEquipo; set => _idEquipo = value; }

    public string Copa { get => _copa; set => _copa = value; }

    public string Estado { get => _estado; set => _estado = value; }
    public int IdEncuentroDefinido { get => _idEncuentroDefinido; set => _idEncuentroDefinido = value; }
  }
}