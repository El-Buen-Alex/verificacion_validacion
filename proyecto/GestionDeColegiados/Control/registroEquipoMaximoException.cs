using System;

namespace Control {
  [Serializable]
  public class RegistroEquipoMaximoException : Exception {
    public string NombreEquipo { get; }
    public RegistroEquipoMaximoException(string mensaje) : base(mensaje) {

    }
    public RegistroEquipoMaximoException(string mensaje, Exception inner)
    : base(mensaje, inner) { }

    public RegistroEquipoMaximoException(string mensaje, string nombreEquipo)
        : this(mensaje) {
      this.NombreEquipo = nombreEquipo;
    }
  }
}
