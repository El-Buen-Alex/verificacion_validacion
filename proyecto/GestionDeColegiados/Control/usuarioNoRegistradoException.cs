using System;

namespace Control {
  [Serializable]
  public class UsuarioNoRegistradoException : Exception {
    public UsuarioNoRegistradoException(string usuario) : base("El usuario y/o contraseña son incorrectos") { }
    public UsuarioNoRegistradoException(string mensaje, Exception inner) : base(mensaje, inner) { }
  }
}