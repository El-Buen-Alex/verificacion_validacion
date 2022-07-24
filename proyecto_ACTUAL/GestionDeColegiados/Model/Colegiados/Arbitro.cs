namespace Model {
  /// <summary>
  /// Clase Abstracta Arbitro.
  /// </summary>
  public abstract class Arbitro {
    private int _idArbitro;
    private string _cedula;
    private string _nombre;
    private string _apellidos;
    private string _domicilio;
    private string _email;
    private string _telefono;

    /// <summary>
    /// Constructor por defecto.
    /// </summary>
    public Arbitro() {
    }

    /// <summary>
    /// Constructor parametrizado.
    /// </summary>
    /// <param name="idArbitro">ID del arbitro.</param>
    /// <param name="cedula">Cedula del arbito.</param>
    /// <param name="nombre">Nombre del arbito.</param>
    /// <param name="apellidos">Apellido del arbito.</param>
    /// <param name="domicilio">Domicilio del arbito.</param>
    /// <param name="email">Email del arbito.</param>
    /// <param name="telefono">Telefono del arbito.</param>
    public Arbitro(int idArbitro, string cedula, string nombre, string apellidos,
        string domicilio, string email, string telefono) {
      this._cedula = cedula;
      this._nombre = nombre;
      this._apellidos = apellidos;
      this._domicilio = domicilio;
      this._email = email;
      this._telefono = telefono;
    }

    /// <summary>
    /// Métodos Getter y Setter de los atributos de arbitro.
    /// </summary>
    public int IdArbitro { get => _idArbitro; set => _idArbitro = value; }
    public string Cedula { get => _cedula; set => _cedula = value; }
    public string Nombre { get => _nombre; set => _nombre = value; }
    public string Apellidos { get => _apellidos; set => _apellidos = value; }
    public string Domicilio { get => _domicilio; set => _domicilio = value; }
    public string Email { get => _email; set => _email = value; }
    public string Telefono { get => _telefono; set => _telefono = value; }

    /// <summary>
    /// Método que “convierte” el objeto a mostrar en texto.
    /// </summary>
    /// <returns>Devuelve una cadena de texto.</returns>
    public override string ToString() {
      return base.ToString();
    }
  }
}