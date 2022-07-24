namespace Model.Colegiados {
  /// <summary>
  /// Clase <c>IntegrantesColegiados</c> para obtener nombre de los arbitros registrados.
  /// </summary>
  public class IntegrantesColegiados {
    private int _idGrupoColegiado;
    private string _nombrejuezCentral;
    private string _nombreasistente1;
    private string _nombreasistente2;
    private string _nombrecuartoArbitro;

    /// <summary>
    /// Métodos Getter y Setter de los atributos de <c>IntegrantesColegiados</c>.
    /// </summary>
    public string NombrejuezCentral { get => _nombrejuezCentral; set => _nombrejuezCentral = value; }
    public string Nombreasistente1 { get => _nombreasistente1; set => _nombreasistente1 = value; }
    public string Nombreasistente2 { get => _nombreasistente2; set => _nombreasistente2 = value; }
    public string NombrecuartoArbitro { get => _nombrecuartoArbitro; set => _nombrecuartoArbitro = value; }
    public int IdGrupoColegiado { get => _idGrupoColegiado; set => _idGrupoColegiado = value; }

    /// <summary>
    /// Método que “convierte” el objeto a mostrar en texto.
    /// </summary>
    /// <returns>Devuelve una cadena de texto.</returns>
    public override string ToString() {
      return base.ToString();
    }
  }
}