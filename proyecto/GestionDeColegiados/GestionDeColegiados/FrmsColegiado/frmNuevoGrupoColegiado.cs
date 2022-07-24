using System;
using System.Windows.Forms;

using Control;
using Control.AdmColegiados;

namespace GestionDeColegiados {
  /// <summary>
  /// Formulario para agregar Áribtros.
  /// </summary>
  public partial class frmNuevoGrupoColegiado : Form {
    ValidacionGUI _validacionGUI = new ValidacionGUI();
    Contexto _contexto = null;
    AdmColegiado _admColegiado = AdmColegiado.GetAdmCol();

    /// <summary>
    /// Constructor del formulario.
    /// </summary>
    public frmNuevoGrupoColegiado() {
      InitializeComponent();
    }

    /// <summary>
    /// Evento para validar que solo ingrese números.
    /// </summary>
    /// <param name="sender">Objeto.</param>
    /// <param name="e">Evento.</param>
    private void ValidarNumeros_KeyPress(object sender, KeyPressEventArgs e) {
      if(!char.IsNumber(e.KeyChar) && (e.KeyChar != Convert.ToChar(Keys.Back))) {

        e.Handled = true;
        return;
      }
    }

    /// <summary>
    /// Evento para validar que solo ingrese letras.
    /// </summary>
    /// <param name="sender">Objeto.</param>
    /// <param name="e">Evento.</param>
    private void ValidarLetras_KeyPress(object sender, KeyPressEventArgs e) {
      if(!char.IsLetter(e.KeyChar) && (e.KeyChar != Convert.ToChar(Keys.Back)) &&
          (e.KeyChar != Convert.ToChar(Keys.Space))) {

        e.Handled = true;
        return;
      }
    }

    /// <summary>
    /// Envento click para el primer botón siguiente.
    /// </summary>
    /// <remarks>
    /// Valida si los campos están, si hay cedulas repetidas y oculta TexBox de ingreso.
    /// </remarks>
    /// <param name="sender">Objeto.</param>
    /// <param name="e">Evento.</param>
    private void Btnsiguiente1_Click(object sender, EventArgs e) {
      bool vacio = _validacionGUI.ValidarVacios(txtcedulaJC, txtnombreJC, txtapellidoJC, txtdomicilioJC, txtemailJC, txttelefonoJC);
      bool cedulaRepetida = _admColegiado.ValidarCedula(txtcedulaJC);
      bool txtcedulaRepetida = (txtcedulaJC.Text == txtcedulaAs1.Text) || (txtcedulaJC.Text == txtcedulaAs2.Text) || (txtcedulaJC.Text == txtcedulaCA.Text);
      if(vacio != true) {

        if(cedulaRepetida != true && txtcedulaRepetida != true) {

          CamposJuezCentral(false);
          CamposAsistente1(true);
        } else {
          MensajeCedulaRepetida();
        }
      } else {
        CamposIncompletos();
      }
    }

    /// <summary>
    /// Envento click para el segundo botón siguiente.
    /// </summary>
    /// <remarks>
    /// Valida si los campos están, si hay cedulas repetidas y oculta TexBox de ingreso.
    /// </remarks>
    /// <param name="sender">Objeto.</param>
    /// <param name="e">Evento.</param>
    private void Btnsiguiente2_Click(object sender, EventArgs e) {
      bool vacio = _validacionGUI.ValidarVacios(txtcedulaAs1, txtnombreAs1, txtapellidoAs1, txtdomicilioAs1, txtemailAs1, txttelefonoAs1);
      bool cedulaRepetida = _admColegiado.ValidarCedula(txtcedulaAs1);
      bool txtcedulaRepetida = (txtcedulaAs1.Text == txtcedulaJC.Text) || (txtcedulaAs1.Text == txtcedulaAs2.Text) || (txtcedulaAs1.Text == txtcedulaCA.Text);
      if(vacio != true) {

        if(cedulaRepetida != true && txtcedulaRepetida != true) {

          CamposAsistente1(false);
          CamposAsistente2(true);
        } else {
          MensajeCedulaRepetida();
        }
      } else {
        CamposIncompletos();
      }
    }

    /// <summary>
    /// Evento click para el botón regresar.
    /// </summary>
    /// <remarks>
    /// Aparece junto al segundo botón siguiente.
    /// </remarks>
    /// <param name="sender">Objeto.</param>
    /// <param name="e">Evento.</param>
    private void BtnRegresar2_Click(object sender, EventArgs e) {
      CamposAsistente1(false);
      CamposJuezCentral(true);
    }

    /// <summary>
    /// Envento click para el tercer botón siguiente.
    /// </summary>
    /// <remarks>
    /// Valida si los campos están, si hay cedulas repetidas y oculta TexBox de ingreso.
    /// </remarks>
    /// <param name="sender">Objeto.</param>
    /// <param name="e">Evento.</param>
    private void Btnsiguiente3_Click(object sender, EventArgs e) {
      bool vacio = _validacionGUI.ValidarVacios(txtcedulaAs2, txtnombreAs2, txtapellidoAs2, txtdomicilioAs2, txtemailAs2, txttelefonoAs2);
      bool cedulaRepetida = _admColegiado.ValidarCedula(txtcedulaAs2);
      bool txtcedulaRepetida = (txtcedulaAs2.Text == txtcedulaJC.Text) || (txtcedulaAs2.Text == txtcedulaAs1.Text) || (txtcedulaAs2.Text == txtcedulaCA.Text);
      if(vacio != true) {

        if(cedulaRepetida != true && txtcedulaRepetida != true) {

          CamposAsistente2(false);
          CamposCuartoArbitro(true);
        } else {
          MensajeCedulaRepetida();
        }
      } else {
        CamposIncompletos();
      }
    }

    /// <summary>
    /// Evento click para el botón regresar.
    /// </summary>
    /// <remarks>
    /// Aparece junto al tercer botón siguiente.
    /// </remarks>
    /// <param name="sender">Objeto.</param>
    /// <param name="e">Evento.</param>
    private void BtnRegresar3_Click(object sender, EventArgs e) {
      CamposAsistente2(false);
      CamposAsistente1(true);
    }

    /// <summary>
    /// Envento click para el botón registrar.
    /// </summary>
    /// <remarks>
    /// Valida si los campos están, si hay cedulas repetidas, oculta TexBox de ingreso y registra Colegiado.
    /// </remarks>
    /// <param name="sender">Objeto.</param>
    /// <param name="e">Evento.</param>
    private void BtnRegistrar_Click(object sender, EventArgs e) {
      bool vacio = _validacionGUI.ValidarVacios(txtcedulaCA, txtnombreCA, txtapellidoCA, txtdomicilioCA, txtemailCA, txttelefonoCA);
      bool cedulaRepetida = _admColegiado.ValidarCedula(txtcedulaCA);
      bool txtcedulaRepetida = (txtcedulaCA.Text == txtcedulaJC.Text) || (txtcedulaCA.Text == txtcedulaAs1.Text) || (txtcedulaCA.Text == txtcedulaAs2.Text);
      if(vacio != true) {

        if(cedulaRepetida != true && txtcedulaRepetida != true) {

          RegistrarColegiado();
          LimpiarCamposJuezCentral();
          LimpiarCamposAsistente1();
          LimpiarCamposAsistente2();
          LimpiarCamposArbitroCentral();
          CamposCuartoArbitro(false);
          CamposJuezCentral(true);
        } else {
          MensajeCedulaRepetida();
        }
      } else {
        CamposIncompletos();
      }
    }

    /// <summary>
    /// Evento click para el botón regresar.
    /// </summary>
    /// <remarks>
    /// Aparece junto al botón Regresar.
    /// </remarks>
    /// <param name="sender">Objeto.</param>
    /// <param name="e">Evento.</param>
    private void BtnRegresar4_Click(object sender, EventArgs e) {
      CamposCuartoArbitro(false);
      CamposAsistente2(true);
    }

    /// <summary>
    /// Evento click para el botón cancelar.
    /// </summary>
    /// <remarks>
    /// Limpia todos los campos y vuelve al regitro del primer árbitro.
    /// </remarks>
    /// <param name="sender">Objeto.</param>
    /// <param name="e">Evento.</param>
    private void BtnCancelar_Click(object sender, EventArgs e) {
      DialogResult resultado;
      resultado = MessageBox.Show("¿Está seguro que desea cancelar?\nSi lo hace, los datos ingresados se borrarán.", "Cancelar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
      if(resultado == DialogResult.Yes) {

        LimpiarCamposJuezCentral();
        LimpiarCamposAsistente1();
        LimpiarCamposAsistente2();
        LimpiarCamposArbitroCentral();
        CamposAsistente1(false);
        CamposAsistente2(false);
        CamposCuartoArbitro(false);
        CamposJuezCentral(true);
      }
    }

    /// <summary>
    /// Método para registrar el grupo de colegiados.
    /// </summary>
    /// <remarks>
    /// Recoge todos los id de los árbitros y los guarda.
    /// </remarks>
    private void RegistrarColegiado() {
      int idJuezCentral = ObtenerIdJuezCentral(),
          idAsistente1 = ObtenerIdAsistente1(),
          idAsistente2 = ObtenerIdAsistente2(),
          idCuartoArbitro = ObtenerIdCuartoArbitro();
      bool vacio = _validacionGUI.ValidarNum(idJuezCentral, idAsistente1, idAsistente2, idCuartoArbitro);
      if(vacio != true) {

        _admColegiado.Guardar(idJuezCentral, idAsistente1, idAsistente2, idCuartoArbitro);
      } else {
        MessageBox.Show("No se pudo agregar colegiados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    }

    /// <summary>
    /// Muestra mensaje de campos vacios.
    /// </summary>
    private void CamposIncompletos() {
      MessageBox.Show("Hay ciertos campos vacios", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    /// <summary>
    /// Muestra mensaje si existe algún árbitro registrado.
    /// </summary>
    private void MensajeCedulaRepetida() {
      MessageBox.Show("El árbitro que ingresó ya se encuentra registrado!!\nIngrese uno nuevo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    /// <summary>
    /// Envia los datos para guardar Juez Central.
    /// </summary>
    /// <remarks>
    /// Usa la clase contexto para asignarle el árbitro.
    /// </remarks>
    /// <returns>Devuelve el id del último Juez Central ingresado como entero.</returns>
    private int ObtenerIdJuezCentral() {
      int id = 0;
      _contexto = new Contexto(AdmJuezCentral.GetAdmJ());
      id = _contexto.ObtenerDatos(txtcedulaJC, txtnombreJC, txtapellidoJC, txtdomicilioJC, txtemailJC, txttelefonoJC);
      return id;
    }

    /// <summary>
    /// Envia los datos para guardar Asistente 1.
    /// </summary>
    /// <remarks>
    /// Usa la clase contexto para asignarle el árbitro.
    /// </remarks>
    /// <returns>Devuelve el id del último Asistente 1 ingresado como entero.</returns>
    private int ObtenerIdAsistente1() {
      int id = 0;
      _contexto = new Contexto(AdmAsistente1.GetAdmA1());
      id = _contexto.ObtenerDatos(txtcedulaAs1, txtnombreAs1, txtapellidoAs1, txtdomicilioAs1, txtemailAs1, txttelefonoAs1);
      return id;
    }

    /// <summary>
    /// Envia los datos para guardar Asistente 2.
    /// </summary>
    /// <remarks>
    /// Usa la clase contexto para asignarle el árbitro.
    /// </remarks>
    /// <returns>Devuelve el id del último Asistente 2 ingresado como entero.</returns>
    private int ObtenerIdAsistente2() {
      int id = 0;
      _contexto = new Contexto(AdmAsistente2.GetAdmA2());
      id = _contexto.ObtenerDatos(txtcedulaAs2, txtnombreAs2, txtapellidoAs2, txtdomicilioAs2, txtemailAs2, txttelefonoAs2);
      return id;
    }

    /// <summary>
    /// Envia los datos para guardar Cuarto Árbitro.
    /// </summary>
    /// <remarks>
    /// Usa la clase contexto para asignarle el árbitro.
    /// </remarks>
    /// <returns>Devuelve el id del último Cuarto Árbitro ingresado como entero.</returns>
    private int ObtenerIdCuartoArbitro() {
      int id = 0;
      _contexto = new Contexto(AdmCuartoArbitro.GetAdmCA());
      id = _contexto.ObtenerDatos(txtcedulaCA, txtnombreCA, txtapellidoCA, txtdomicilioCA, txtemailCA, txttelefonoCA);
      return id;
    }

    /// <summary>
    /// Método para cambiar la propiedad de "Visible" para Juez Central.
    /// </summary>
    /// <param name="valor">Parámetro con valor booleano.</param>
    private void CamposJuezCentral(bool valor) {
      labJuezCentral.Visible = valor;
      txtcedulaJC.Visible = valor;
      txtnombreJC.Visible = valor;
      txtapellidoJC.Visible = valor;
      txtdomicilioJC.Visible = valor;
      txtemailJC.Visible = valor;
      txttelefonoJC.Visible = valor;
      btnsiguiente1.Visible = valor;
    }

    /// <summary>
    /// Método para cambiar la propiedad de "Visible" para Asistente 1.
    /// </summary>
    /// <param name="valor">Parámetro con valor booleano.</param>
    private void CamposAsistente1(bool valor) {
      labAsist1.Visible = valor;
      txtcedulaAs1.Visible = valor;
      txtnombreAs1.Visible = valor;
      txtapellidoAs1.Visible = valor;
      txtdomicilioAs1.Visible = valor;
      txtemailAs1.Visible = valor;
      txttelefonoAs1.Visible = valor;
      btnRegresar2.Visible = valor;
      btnsiguiente2.Visible = valor;
    }

    /// <summary>
    /// Método para cambiar la propiedad de "Visible" para Asistente 2.
    /// </summary>
    /// <param name="valor">Parámetro con valor booleano.</param>
    private void CamposAsistente2(bool valor) {
      labAsist2.Visible = valor;
      txtcedulaAs2.Visible = valor;
      txtnombreAs2.Visible = valor;
      txtapellidoAs2.Visible = valor;
      txtdomicilioAs2.Visible = valor;
      txtemailAs2.Visible = valor;
      txttelefonoAs2.Visible = valor;
      btnRegresar3.Visible = valor;
      btnsiguiente3.Visible = valor;
    }

    /// <summary>
    /// Método para cambiar la propiedad de "Visible" para Cuarto Árbitro.
    /// </summary>
    /// <param name="valor">Parámetro con valor booleano.</param>
    private void CamposCuartoArbitro(bool valor) {
      labCuartoArb.Visible = valor;
      txtcedulaCA.Visible = valor;
      txtnombreCA.Visible = valor;
      txtapellidoCA.Visible = valor;
      txtdomicilioCA.Visible = valor;
      txtemailCA.Visible = valor;
      txttelefonoCA.Visible = valor;
      btnRegresar4.Visible = valor;
      btnRegistrar.Visible = valor;
    }

    /// <summary>
    /// Método para limpiar los campos de Juez central.
    /// </summary>
    private void LimpiarCamposJuezCentral() {
      txtcedulaJC.Text = "";
      txtnombreJC.Text = "";
      txtapellidoJC.Text = "";
      txtdomicilioJC.Text = "";
      txtemailJC.Text = "";
      txttelefonoJC.Text = "";
    }

    /// <summary>
    /// Método para limpiar los campos de Asistente 1.
    /// </summary>
    private void LimpiarCamposAsistente1() {
      txtcedulaAs1.Text = "";
      txtnombreAs1.Text = "";
      txtapellidoAs1.Text = "";
      txtdomicilioAs1.Text = "";
      txtemailAs1.Text = "";
      txttelefonoAs1.Text = "";
    }

    /// <summary>
    /// Método para limpiar los campos de Asistente 2.
    /// </summary>
    private void LimpiarCamposAsistente2() {
      txtcedulaAs2.Text = "";
      txtnombreAs2.Text = "";
      txtapellidoAs2.Text = "";
      txtdomicilioAs2.Text = "";
      txtemailAs2.Text = "";
      txttelefonoAs2.Text = "";
    }

    /// <summary>
    /// Método para limpiar los campos de Cuarto Árbitro.
    /// </summary>
    private void LimpiarCamposArbitroCentral() {
      txtcedulaCA.Text = "";
      txtnombreCA.Text = "";
      txtapellidoCA.Text = "";
      txtdomicilioCA.Text = "";
      txtemailCA.Text = "";
      txttelefonoCA.Text = "";
    }
  }
}