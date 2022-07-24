using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Control.AdmEncuentros;
using Control.AdmEncuentrosGenerados;

namespace GestionDeColegiados.FrmsArbitro {
  public partial class FrmMenuArbitro : Form {
    private AdmEncuentrosDefinidos _admEncuentrosDefinidos = AdmEncuentrosDefinidos.GetAdmGenerarEncuentrosDefinidos();
    private AdmEncuentroFinalizado _admEncuentroFinalizado = AdmEncuentroFinalizado.GetAdmEncuentrosFinalizados();
    //dll y variables necesarios para poder mover de lugar la barra de titulo 
    [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
    private extern static void ReleaseCapture();
    [DllImport("user32.DLL", EntryPoint = "SendMessage")]
    private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

    private Color _colorDefaultClose;
    private Color _colorDefaultMin;
    public FrmMenuArbitro() {
      InitializeComponent();
    }
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
    private void PanelBarraTitulo_MouseDown(object sender, MouseEventArgs e) {
      ReleaseCapture();
      SendMessage(this.Handle, 0x112, 0xf012, 0);
    }


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

    private void BtnGestionColegiado1_MouseEnter(object sender, EventArgs e) {
      flpGestionPartidoFinalizado.Visible = true;
    }

    private void BtnRegistrarPartido_Click(object sender, EventArgs e) {
      int cantEncuentrosDefinidos = _admEncuentrosDefinidos.ObtenerNumeroPartidosPorJugar();
      if(cantEncuentrosDefinidos == 0) {
        MessageBox.Show("No existen encuentros definidos por registrar");
      } else {
        AbrirFormEnPanel(new FrmRegistrarResultado());
      }

    }
    private void ExistenRegistrosArbir(object formhija) {
      int cantEncuentrosFinalizados = _admEncuentroFinalizado.GetCantidadEncuentrosFinalizados();
      if(cantEncuentrosFinalizados > 0) {
        AbrirFormEnPanel(formhija);
      }
    }
    private void BtnActualizarPartidoFinalizado_Click(object sender, EventArgs e) {
      ExistenRegistrosArbir(new FrmEditarPartidoFinalizado());
    }

    private void BtnVerTodosPartidos_Click(object sender, EventArgs e) {

      AbrirFormEnPanel(new FrmVerCompeticion(false));

    }

    private void Button2_Click(object sender, EventArgs e) {
      this.Close();
      BtnIniciarSesion frm = new BtnIniciarSesion();
      frm.Show();
    }
  }
}