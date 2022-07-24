using System;

namespace Control {
  [Serializable]
  public class GenerarEncuentrosException : Exception {
    public GenerarEncuentrosException(string mensajeError) : base(mensajeError) { }
  }
}