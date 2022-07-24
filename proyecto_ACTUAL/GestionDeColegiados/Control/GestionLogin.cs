using System;

using Model;

namespace Control {
  public class GestionLogin {
    private ConexionUsuarioBD _gestionUsuario = new ConexionUsuarioBD();
    //metodo necesario para gestionar el intento de acceder a la aplicación
    public string ControlLogin(string usuario, string password) {
      //creamos una cadena que ayudará a dar respuesta del proceso
      string respuesta = "";
      try {
        Administrador nuevoUsuario = ObtenerUsuario(usuario, password);
        if(nuevoUsuario == null) {

          respuesta = "ERROR: ";
          throw new UsuarioNoRegistradoException(usuario);
        } else {
          respuesta = nuevoUsuario.Rol;
        }
      } catch(Exception ex) {
        Console.WriteLine(ex.Message);
        respuesta = "ERROR: " + ex.Message;
      }
      return respuesta;
    }

    public bool CambiarPass(string newPass, int idUser) {
      bool cambioPass;
      string cambio = _gestionUsuario.CambiarPassword(newPass, idUser);
      if(cambio.StartsWith("EXITO")) {

        cambioPass = true;
      } else {
        cambioPass = false;
      }

      return cambioPass;
    }

    private Administrador ObtenerUsuario(string username, string password) {
      ConexionUsuarioBD gestionUsuario = new ConexionUsuarioBD();
      Administrador usuario = null;
      //creamos una cadena que ayudará a dar respuesta del proceso
      string respuesta = "";
      try {
        usuario = gestionUsuario.ExisteUsuario(username.Trim(), password);

        if(usuario == null) {

          throw new UsuarioNoRegistradoException(username);
        }
      } catch(Exception ex) {
        Console.WriteLine(ex.Message);
        respuesta = ex.Message;
      }
      return usuario;
    }
    public int ObtenerId(string usuario, string password) {
      Administrador user = ObtenerUsuario(usuario, password);

      return user.Id;
    }

    public bool ValidarUltimoAcceso(string usuario, string password) {
      bool respuesta = true;
      Administrador admin = ObtenerUsuario(usuario, password);
      if(String.IsNullOrEmpty(admin.PrimerAcceso)) {

        respuesta = false;
      }
      return respuesta;
    }
  }
}