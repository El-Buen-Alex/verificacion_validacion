namespace Model.Colegiados {
  /// <summary>
  /// Clase Colegiado que va a registrar los id de los arbitros.
  /// </summary>
  public class Colegiado {
    private int _idcolegiado;
    private int _idjuezcentral;
    private int _idasistente1;
    private int _idasistente2;
    private int _idcuartoarbitro;

    /// <summary>
    /// Constructor por defecto.
    /// </summary>
    public Colegiado() {
    }

    /// <summary>
    /// Constructor parametrizado.
    /// </summary>
    /// <param name="idcolegiado">ID de colegiado.</param>
    /// <param name="idjuezcentral">ID de Juez Central.</param>
    /// <param name="idasistente1">ID de Asistente 1.</param>
    /// <param name="idasistente2">ID de Asistente 2.</param>
    /// <param name="idcuartoarbitro">ID de Cuarto Arbitro.</param>
    public Colegiado(int idcolegiado, int idjuezcentral, int idasistente1, int idasistente2, int idcuartoarbitro) {
      this._idcolegiado = idcolegiado;
      this._idjuezcentral = idjuezcentral;
      this._idasistente1 = idasistente1;
      this._idasistente2 = idasistente2;
      this._idcuartoarbitro = idcuartoarbitro;
    }

    /// <summary>
    /// Métodos Getter y Setter de los atributos de colegiado.
    /// </summary>
    public int Idcolegiado { get => _idcolegiado; set => _idcolegiado = value; }
    public int Idjuezcentral { get => _idjuezcentral; set => _idjuezcentral = value; }
    public int Idasistente1 { get => _idasistente1; set => _idasistente1 = value; }
    public int Idasistente2 { get => _idasistente2; set => _idasistente2 = value; }
    public int Idcuartoarbitro { get => _idcuartoarbitro; set => _idcuartoarbitro = value; }

    /// <summary>
    /// Método que “convierte” el objeto a mostrar en texto.
    /// </summary>
    /// <returns>Devuelve una cadena de texto.</returns>
    public override string ToString() {
      return base.ToString();
    }
  }
}