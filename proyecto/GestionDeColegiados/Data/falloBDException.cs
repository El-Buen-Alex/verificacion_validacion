using System;

namespace Data {
  /// <summary>
  /// Excepcion para verificar fallos en la BD.
  /// </summary>
  [Serializable]
  public class FalloBDException : Exception {

    /// <summary>
    /// Constructores predeterminados.
    /// </summary>
    public string Arbitro { get; }

    public FalloBDException() { }

    public FalloBDException(string message) : base("Algo ha salido mal, revise su base de datos") { }

    public FalloBDException(string message, Exception inner) : base(message, inner) { }

    public FalloBDException(string message, string arbitro) : this(message) {
      Arbitro = arbitro;
    }
  }
}
