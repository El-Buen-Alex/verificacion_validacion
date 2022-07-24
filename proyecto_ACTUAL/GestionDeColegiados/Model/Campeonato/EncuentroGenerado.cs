namespace Model {
  public class EncuentroGenerado {
    private int _id;
    private int _idEquipoLocal;
    private int _idEquipoVisitante;
    private string _estado;
    private string _asignacion;

    public EncuentroGenerado() {

    }
    public EncuentroGenerado(int idEquipoLocal, int idEquipoVisitante) {
      this._idEquipoLocal = idEquipoLocal;
      this._idEquipoVisitante = idEquipoVisitante;
    }
    public EncuentroGenerado(int idEquipoLocal, int idEquipoVisitante, string estado) {
      this._idEquipoLocal = idEquipoLocal;
      this._idEquipoVisitante = idEquipoVisitante;
      this._estado = estado;
    }
    public EncuentroGenerado(int id, int idEquipoLocal, int idEquipoVisitante, string estado, string asignacion) {
      this._id = id;
      this._idEquipoLocal = idEquipoLocal;
      this._idEquipoVisitante = idEquipoVisitante;
      this._estado = estado;
      this._asignacion = asignacion;
    }

    public int Id { get => _id; set => _id = value; }
    public int IdEquipoLocal { get => _idEquipoLocal; set => _idEquipoLocal = value; }
    public int IdEquipoVisitante { get => _idEquipoVisitante; set => _idEquipoVisitante = value; }
    public string Estado { get => _estado; set => _estado = value; }
    public string Asignacion { get => _asignacion; set => _asignacion = value; }
  }
}
