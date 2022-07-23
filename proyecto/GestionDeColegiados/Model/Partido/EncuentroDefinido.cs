using System;

namespace Model {
  public class EncuentroDefinido {
    private int _id;
    private int _idEncuentroGeneradoPendiente;
    private int _idColegiado;
    private DateTime _fechaDeEncuentro;
    private DateTime _hora;
    private string _estado;
    private int _idEstadio;

    public EncuentroDefinido() {

    }
    public EncuentroDefinido(int idEncuentroGeneradoPendiente, int idColegiado, DateTime fechaDeEncuentro, DateTime hora, int idEStadio) {
      this._idEncuentroGeneradoPendiente = idEncuentroGeneradoPendiente;
      this._idColegiado = idColegiado;
      this._fechaDeEncuentro = fechaDeEncuentro;
      this._hora = hora;
      this._idEstadio = idEStadio;
    }
    public EncuentroDefinido(int idEncuentroGeneradoPendiente, int idColegiado, DateTime fechaDeEncuentro, string estado, DateTime hora, int idEStadio) {
      this._idEncuentroGeneradoPendiente = idEncuentroGeneradoPendiente;
      this._idColegiado = idColegiado;
      this._fechaDeEncuentro = fechaDeEncuentro;
      this._estado = estado;
      this._hora = hora;
      this._idEstadio = idEStadio;
    }

    public EncuentroDefinido(int id, int idEncuentroGeneradoPendiente, int idColegiado, DateTime fechaDeEncuentro, string estado, DateTime hora, int idEStadio) {
      this._id = id;
      this._idEncuentroGeneradoPendiente = idEncuentroGeneradoPendiente;
      this._idColegiado = idColegiado;
      this._fechaDeEncuentro = fechaDeEncuentro;
      this._estado = estado;
      this._hora = hora;
      this._idEstadio = idEStadio;
    }

    public int Id { get => _id; set => _id = value; }
    public int IdEncuentroGeneradoPendiente { get => _idEncuentroGeneradoPendiente; set => _idEncuentroGeneradoPendiente = value; }
    public int IdColegiado { get => _idColegiado; set => _idColegiado = value; }
    public DateTime FechaDeEncuentro { get => _fechaDeEncuentro; set => _fechaDeEncuentro = value; }
    public string Estado { get => _estado; set => _estado = value; }
    public DateTime Hora { get => _hora; set => _hora = value; }
    public int IdEstadio { get => _idEstadio; set => _idEstadio = value; }
  }
}
