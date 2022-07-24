using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Control.AdmColegiados;
using Control.AdmEncuentros;
using Control.AdmEncuentrosGenerados;
using Control.AdmEquipos;

using GestionDeColegiados.FrmsArbitro;

namespace GestionDeColegiados {
  public partial class MenuPrincipal : Form {

    //dll y variables necesarios para poder mover de lugar la barra de titulo 
    [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
    private static extern void ReleaseCapture();
    [DllImport("user32.DLL", EntryPoint = "SendMessage")]
    private static extern void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

    private Color _colorDefaultClose;
    private Color _colorDefaultMin;
    private AdmEncuentroFinalizado _admEncuentroFinalizado = AdmEncuentroFinalizado.GetAdmEncuentrosFinalizados();
    private AdmGenerarEncuentros _admGenerarEncuentros = AdmGenerarEncuentros.GetAdmadmGenerarEncuentros();
    private AdmEquipo _admEquipo = AdmEquipo.GetEquipo();
    private AdmEncuentrosDefinidos _admEncuentrosDefinidos = AdmEncuentrosDefinidos.GetAdmGenerarEncuentrosDefinidos();
    private AdmColegiado _admColegiado = AdmColegiado.GetAdmCol();

    public MenuPrincipal() {
      InitializeComponent();
    }

    private void BtnGestionColegiados_MouseEnter(object sender, EventArgs e) {
      flpOpcionGestionColegiado.Visible = true;
    }

    private void BtnGestionEquipos2_MouseEnter(object sender, EventArgs e) {
      flpOpcionGestionEquipo.Visible = true;
    }

    private void BtnGestionEncuentros2_MouseEnter(object sender, EventArgs e) {
      flpOpcionGestionEncuentros.Visible = true;
    }

    /// <summary>
    /// Metodo encargado de pintar cada ventana en un panel
    /// </summary>
    /// <param name="formhija">Form que será pintado en un panel</param>
    private void AbrirFormEnPanel(object formhija) {
      /*preguntamos si existe minimo un formulario ya abierto
      si es así entonces lo cerramos*/
      if(this.pnlPanelContenedor.Controls.Count > 0)
        this.pnlPanelContenedor.Controls.RemoveAt(0);
      //finalmente abrimos el frm que se desea mostrar en el panel principal
      Form formAMostrar = formhija as Form;
      formAMostrar.TopLevel = false;
      formAMostrar.Dock = DockStyle.Fill;
      this.pnlPanelContenedor.Controls.Add(formAMostrar);
      this.pnlPanelContenedor.Tag = formAMostrar;
      formAMostrar.Show();

    }

    private void BtnNuevoGrupoColegiados_Click(object sender, EventArgs e) {
      AbrirFormEnPanel(new frmNuevoGrupoColegiado());
    }

    private void BtnVerTodosColegiados_Click(object sender, EventArgs e) {
      if(_admColegiado.ObtenerCantidadColegiado() == 0) {
        MessageBox.Show("No se han registrado colegiados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        AbrirFormEnPanel(new frmNuevoGrupoColegiado());
      } else {
        frmVerTodosLosColegiados verColegiados = new frmVerTodosLosColegiados();
        verColegiados.ShowDialog();
      }
    }

    private void BtnAnadirEquipo_Click(object sender, EventArgs e) {
      if(_admEquipo.ObtenerCantidadEquipo() < 10) {
        AbrirFormEnPanel(new FrmNuevoEquipo());
      } else {
        AbrirFormEnPanel(new FrmListaEquipos());
      }
    }

    private void ExaminarAccesibilidadGenerarEncuentrosPorCantidadEquipo() {
      if(_admEquipo.ObtenerCantidadEquipo() == 10) {
        ExaminarAccesibilidadGenerarEncuentros();
      } else {
        string faltaEquipo = "Para generar encuentros debe existir 10 equipos registrados" +
            "\n\rExisten: " + _admEquipo.ObtenerCantidadEquipo() + " Equipos registrados." +
            " Por favor ingrese: " + (10 - _admEquipo.ObtenerCantidadEquipo()) + " más";
        MessageBox.Show(faltaEquipo, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        AbrirFormEnPanel(new FrmNuevoEquipo());
      }
    }

    private void ExaminarAccesibilidadGenerarEncuentros() {
      if(_admGenerarEncuentros.ObtnerNumeroEncuentrosGeneradosPendientes() == 0) {
        AbrirFormEnPanel(new frmGenerarEncuentros(false));
      } else {
        MessageBox.Show("Ya se han generado y registrados los encuentros");
        AbrirFormEnPanel(new frmGenerarEncuentros(true));
      }
    }
    private void BtnGenerarEncuentros_Click(object sender, EventArgs e) {
      int cantEncuentrosDefinidos = _admEncuentrosDefinidos.ObtenerNumeroPartidosPorJugar();
      if(cantEncuentrosDefinidos == 0) {
        ExaminarAccesibilidadGenerarEncuentrosPorCantidadEquipo();
      } else {
        MessageBox.Show("Existen: " + cantEncuentrosDefinidos + " Por jugar, no se puede Generar Encuentros");
        AbrirFormEnPanel(new frmTodosLosEncuentrosDefinidos());
      }

    }

    private void BtnAsignarColegiados_Click(object sender, EventArgs e) {
      int numeroEncuentros = _admGenerarEncuentros.ObtnerNumeroEncuentrosGeneradosPendientes();
      if(numeroEncuentros == 0) {
        MessageBox.Show("No hay encuentros disponibles para asignar fecha y colegiados ");
      } else {
        AbrirFormEnPanel(new FrmRegistrarPartido());
      }

    }

    private void BtnCambiarEstadio_Click(object sender, EventArgs e) {
      if(_admEncuentrosDefinidos.ObtenerNumeroPartidosPorJugar() == 0) {
        MessageBox.Show("No hay Partidos por definir. Por favor, genere encuentros primero");
      } else {
        AbrirFormEnPanel(new FrmCambiarEstadioPartido());
      }
    }

    //metodo que controla el evento de arrastrar pantalla
    private void PanelBarraTitulo_MouseDown(object sender, MouseEventArgs e) {
      ReleaseCapture();
      SendMessage(this.Handle, 0x112, 0xf012, 0);
    }

    //evento para minimizar pantalla
    private void PbMinimizar_Click_1(object sender, EventArgs e) {
      this.WindowState = FormWindowState.Minimized;
    }
    //evento para cerrar pantalla
    private void PbCerrar_Click(object sender, EventArgs e) {
      Application.Exit();
    }


    //Eventos que generan un efecto visual en cuanto el mouse pasa por dicho controlador
    private void PbCerrar_MouseEnter(object sender, EventArgs e) {
      _colorDefaultClose = pbCerrar.BackColor;
      pbCerrar.BackColor = Color.FromArgb(202, 49, 32);
    }
    protected void PbCerrar_MouseLeave(object sender, EventArgs e) {
      pbCerrar.BackColor = _colorDefaultClose;
    }

    protected void PbMinimizar_MouseEnter(object sender, EventArgs e) {
      _colorDefaultMin = pbMinimizar.BackColor;
      pbMinimizar.BackColor = Color.FromArgb(52, 58, 64);
    }

    private void PbMinimizar_MouseLeave(object sender, EventArgs e) {
      pbMinimizar.BackColor = _colorDefaultMin;
    }

    private void BtnVerEncuentrosDefinidos_Click(object sender, EventArgs e) {
      if(_admEncuentrosDefinidos.ObtenerNumeroPartidosPorJugar() == 0) {
        MessageBox.Show("No hay Partidos por definir. Por favor, genere encuentros primero");
      } else {
        AbrirFormEnPanel(new frmTodosLosEncuentrosDefinidos());
      }
    }

    private void BtnVerTodosPartidos_Click(object sender, EventArgs e) {
      AbrirFormEnPanel(new FrmVerCompeticion(true));
    }

    private void Button1_MouseEnter(object sender, EventArgs e) {
      flpVerCompetencia.Visible = true;
    }

    private void Button2_Click(object sender, EventArgs e) {
      this.Close();
      BtnIniciarSesion frm = new BtnIniciarSesion();
      frm.Show();
    }
    public void AbrirFormNuevoEquipo() {
      if(_admEquipo.ObtenerCantidadEquipo() < 10) {
        AbrirFormEnPanel(new FrmNuevoEquipo());
      } else {
        AbrirFormEnPanel(new FrmListaEquipos());
      }
    }
    private void Button5_Click(object sender, EventArgs e) {
      AbrirFormNuevoEquipo();
    }

    private void ExaminarAccesibilidadEditarEquipoPorEncuentrosGenerados() {
      if(_admGenerarEncuentros.ObtnerNumeroEncuentrosGeneradosPendientes() == 0 && _admEncuentroFinalizado.GetCantidadEncuentrosFinalizados() == 0 && _admEncuentrosDefinidos.ObtenerCantidadEncuentrosDefinidos() == 0) {
        AbrirFormEnPanel(new FrmVerTodos());
      } else {
        MessageBox.Show("Existen encuentros generados o definidos. No se pueden eliminar o editar equipos.");
      }
    }
    private void Button3_Click(object sender, EventArgs e) {
      if(_admEquipo.ObtenerCantidadEquipo() > 0) {
        ExaminarAccesibilidadEditarEquipoPorEncuentrosGenerados();
      } else {
        MessageBox.Show("Ingrese primero algunos equipos");
        AbrirFormEnPanel(new FrmNuevoEquipo());
      }
    }
  }
}