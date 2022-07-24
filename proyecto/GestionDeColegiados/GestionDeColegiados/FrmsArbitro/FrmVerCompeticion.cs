using System;
using System.Windows.Forms;

using Control.AdmEncuentros;
using Control.AdmEncuentrosGenerados;
using Control.AdmEstadios;

namespace GestionDeColegiados.FrmsArbitro {
  public partial class FrmVerCompeticion : Form {
    private AdmEncuentroFinalizado _admEncuentroFinalizado = AdmEncuentroFinalizado.GetAdmEncuentrosFinalizados();
    private AdmEncuentrosDefinidos _admEncuentroDefinido = AdmEncuentrosDefinidos.GetAdmGenerarEncuentrosDefinidos();
    private AdmGenerarEncuentros _admGenerarEncuentros = AdmGenerarEncuentros.GetAdmadmGenerarEncuentros();
    private AdmEstadio _admEstadio = AdmEstadio.GetAdmEstadio();
    public FrmVerCompeticion(bool puedeAdministrar) {
      InitializeComponent();
      CompetenciaLlenar();
      if(puedeAdministrar) {

        msAdmin.Visible = puedeAdministrar;
        GestionarAccesibilidadMsAdmin();
      }
    }
    private void GestionarAccesibilidadFinalizarCompetencia(int cantEncFin) {
      bool accesibilidad = false;
      if(cantEncFin % 5 == 0) {

        accesibilidad = true;
      }
      fINALIZARCOMPETENCIAToolStripMenuItem.Enabled = accesibilidad;
    }

    private void ExaminarDisponibilidadPorEncuentrosGenerados(int cantidadGenerado) {
      if(cantidadGenerado > 0) {

        dARBAJACOMPETENCIAToolStripMenuItem.Enabled = true;
      } else {
        rEINICIARRESULTADOSToolStripMenuItem.Enabled = false;
        rEINICIARTODALACOMPETENCIAToolStripMenuItem.Enabled = false;
        dARBAJACOMPETENCIAToolStripMenuItem.Enabled = false;
      }
    }
    private void ExaminarDisponibilidadPorEncuentrosDefinidos(int cantidadDefinido) {
      if(cantidadDefinido > 0) {

        rEINICIARTODALACOMPETENCIAToolStripMenuItem.Enabled = true;
        dARBAJACOMPETENCIAToolStripMenuItem.Enabled = true;

      } else {
        rEINICIARRESULTADOSToolStripMenuItem.Enabled = false;
        rEINICIARTODALACOMPETENCIAToolStripMenuItem.Enabled = false;
      }
    }
    private void ExaminarDisponibilidadPorEncuentrosFinalizados(int cantidadEncuentrosFinalizados) {
      if(cantidadEncuentrosFinalizados > 0) {

        dARBAJACOMPETENCIAToolStripMenuItem.Enabled = true;
        rEINICIARTODALACOMPETENCIAToolStripMenuItem.Enabled = true;
        rEINICIARRESULTADOSToolStripMenuItem.Enabled = true;
      } else {
        rEINICIARRESULTADOSToolStripMenuItem.Enabled = false;
      }
    }

    private void GestionarAccesibilidadMsAdmin() {
      int cantidadGenerado = _admGenerarEncuentros.ObtnerNumeroEncuentrosGeneradosPendientes();
      int cantidadDefinido = _admEncuentroDefinido.ObtenerCantidadEncuentrosDefinidos();
      int cantidadEncuentrosFinalizados = _admEncuentroFinalizado.GetCantidadEncuentrosFinalizados();
      ExaminarDisponibilidadPorEncuentrosGenerados(cantidadGenerado);
      ExaminarDisponibilidadPorEncuentrosDefinidos(cantidadDefinido);
      ExaminarDisponibilidadPorEncuentrosFinalizados(cantidadEncuentrosFinalizados);
      GestionarAccesibilidadFinalizarCompetencia(cantidadEncuentrosFinalizados);
    }

    private void CompetenciaLlenar() {
      bool existe = _admEncuentroFinalizado.LlenarDgv(dgvCompeticion);
      lblAdvertencia.Visible = !existe;
    }

    private void MostrarMensajeFinalizar(bool respuesta, string mensaje) {
      if(respuesta) {

        _admEstadio.PonerEstadiosDisponibles();
        GestionarAccesibilidadMsAdmin();
      } else {
        mensaje = "NO SE PUDO ELIMINAR";
      }
      MessageBox.Show(mensaje);
    }


    private void DAR_BAJA_RCOMPETENCIAToolStripMenuItem_Click(object sender, EventArgs e) {
      string pregunta = "¿Seguro que quieres DAR DE BAJA la competición?\n";
      string mensaje = "Esto Hará que se vuelva a generar y definir encuentros en una nueva copa.";
      string reaccion = " Pero no se podrá visualizar en futuras busquedas.";
      string mensajeExito = "Se dio de baja a la competencia con exito";
      DialogResult res = MessageBox.Show(pregunta + mensaje + reaccion, "CUIDADO", MessageBoxButtons.YesNo);
      if(res == DialogResult.Yes) {

        bool resultado = _admEncuentroFinalizado.DarBajaCompetencia();
        if(resultado) {

          resultado = _admEncuentroDefinido.DarBajaEncuentrosDefinidos();
          if(resultado) {

            resultado = _admGenerarEncuentros.DarBajaEncuentros();
          }
        }
        MostrarMensajeFinalizar(resultado, mensajeExito);
      }

    }

    private void FINALIZARCOMPETENCIAToolStripMenuItem_Click(object sender, EventArgs e) {
      string pregunta = "¿Seguro que quieres FINALIZAR la competición?\n";
      string mensaje = "Esto Hará que se vuelva a generar y definir encuentros en una nueva copa.";
      string reaccion = " Se podrá visualizar en futuras busquedas.";
      string mensajeExito = "Se Finalizó la competencia con exito";
      DialogResult res = MessageBox.Show(pregunta + mensaje + reaccion, "CUIDADO", MessageBoxButtons.YesNo);
      if(res == DialogResult.Yes) {

        bool resultado = _admEncuentroFinalizado.FinalizarCompetencia();
        MostrarMensajeFinalizar(resultado, mensajeExito);
      }
    }
    private void ReiniciarTodaCompetencia() {
      string pregunta = "¿Seguro que quieres REINICIAR la competición?\n";
      string mensaje = "Esto Hará que se elimine todo menos los encuentros que esten generados";
      string mensajeExito = "Se Reinició la competencia con exito";
      DialogResult res = MessageBox.Show(pregunta + mensaje, "Cuidado", MessageBoxButtons.YesNo);
      bool resultado = false;
      if(res == DialogResult.Yes) {

        int cantidadEF = _admEncuentroFinalizado.GetCantidadEncuentrosFinalizados();
        if(cantidadEF > 0) {

          resultado = _admEncuentroFinalizado.ReinicarCompetencia();
        }
        int cantidadED = _admEncuentroDefinido.ObtenerCantidadEncuentrosDefinidos();
        if(cantidadED > 0) {

          resultado = _admEncuentroDefinido.ReinicarCompetencia();
        }
        MostrarMensajeFinalizar(resultado, mensajeExito);
      }
    }
    private void REINICIARTODALACOMPETENCIAToolStripMenuItem_Click(object sender, EventArgs e) {
      ReiniciarTodaCompetencia();
    }

    private void REINICIARRESULTADOSToolStripMenuItem_Click(object sender, EventArgs e) {
      string pregunta = "¿Seguro que quieres REINICIAR LOS RESULTADOS de la competición?\n";
      string mensaje = "Esto Hará que se elimine todo menos los encuentros que esten GENERADOS y DEFINIDOS";
      string mensajeExito = "Se Reinicio los resultados de la competencia con exito";
      DialogResult res = MessageBox.Show(pregunta + mensaje, "ALERTA", MessageBoxButtons.YesNo);
      if(res == DialogResult.Yes) {

        bool resultado = _admEncuentroFinalizado.ReinicarCompetencia();
        MostrarMensajeFinalizar(resultado, mensajeExito);
      }
    }
  }
}
