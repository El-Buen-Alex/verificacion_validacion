namespace Model {
  public class Estadio {
    private int _id;
    private string _nombre;
    private string _asignacion;

    public Estadio() {

    }
    public Estadio(int id, string nombre, string estado) {
      this._id = id;
      this._nombre = nombre;
      this._asignacion = estado;
    }

    public int Id { get => _id; set => _id = value; }
    public string Nombre { get => _nombre; set => _nombre = value; }
    public string Asignacion { get => _asignacion; set => _asignacion = value; }
  }
}
