namespace Model {
  public class Administrador {
    private int _id;
    private string _password, _nombre, _rol, _primerAcceso;

    public int Id { get => _id; set => _id = value; }
    public string Password { get => _password; set => _password = value; }
    public string Nombre { get => _nombre; set => _nombre = value; }
    public string Password1 { get => _password; set => _password = value; }
    public string Nombre1 { get => _nombre; set => _nombre = value; }
    public string Rol { get => _rol; set => _rol = value; }
    public string PrimerAcceso { get => _primerAcceso; set => _primerAcceso = value; }
  }
}
