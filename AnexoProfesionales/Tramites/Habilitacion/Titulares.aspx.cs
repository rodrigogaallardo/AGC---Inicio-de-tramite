using AnexoProfesionales.App_Components;
using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnexoProfesionales
{
    public partial class Titulares : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager sm = ScriptManager.GetCurrent(this);

            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(updGrillaTitulares, updGrillaTitulares.GetType(), "init_JS_updGrillaTitulares", "init_JS_updGrillaTitulares();", true);
                ScriptManager.RegisterStartupScript(updAgregarPersonaFisica, updAgregarPersonaFisica.GetType(), "init_JS_updAgregarPersonaFisica", "init_JS_updAgregarPersonaFisica();", true);
                ScriptManager.RegisterStartupScript(upd_ddlTipoIngresosBrutosPF, upd_ddlTipoIngresosBrutosPF.GetType(), "init_JS_upd_ddlTipoIngresosBrutosPF", "init_JS_upd_ddlTipoIngresosBrutosPF();", true);
                ScriptManager.RegisterStartupScript(upd_txtIngresosBrutosPF, upd_txtIngresosBrutosPF.GetType(), "init_JS_upd_txtIngresosBrutosPF", "init_JS_upd_txtIngresosBrutosPF();", true);
                ScriptManager.RegisterStartupScript(updLocalidadPF, updLocalidadPF.GetType(), "init_JS_updLocalidadPF", "init_JS_updLocalidadPF();", true);
                ScriptManager.RegisterStartupScript(updProvinciasPF, updProvinciasPF.GetType(), "init_JS_updProvinciasPF", "init_JS_updProvinciasPF();", true);
                ScriptManager.RegisterStartupScript(updFirmantePF, updFirmantePF.GetType(), "init_JS_updFirmantePF", "init_JS_updFirmantePF();", true);
                ScriptManager.RegisterStartupScript(updAgregarPersonaJuridica, updAgregarPersonaJuridica.GetType(), "init_JS_updAgregarPersonaJuridica", "init_JS_updAgregarPersonaJuridica();", true);
                ScriptManager.RegisterStartupScript(upd_ddlTipoIngresosBrutosPJ, upd_ddlTipoIngresosBrutosPJ.GetType(), "init_JS_upd_ddlTipoIngresosBrutosPJ", "init_JS_upd_ddlTipoIngresosBrutosPJ();", true);
                ScriptManager.RegisterStartupScript(upd_txtIngresosBrutosPJ, upd_txtIngresosBrutosPJ.GetType(), "init_JS_upd_txtIngresosBrutosPJ", "init_JS_upd_txtIngresosBrutosPJ();", true);
                ScriptManager.RegisterStartupScript(updLocalidadPJ, updLocalidadPJ.GetType(), "init_JS_updLocalidadPJ", "init_JS_updLocalidadPJ();", true);
                ScriptManager.RegisterStartupScript(updProvinciasPJ, updProvinciasPJ.GetType(), "init_JS_updProvinciasPJ", "init_JS_updProvinciasPJ();", true);
                ScriptManager.RegisterStartupScript(upd_ddlTipoCaracterLegalFirPJ, upd_ddlTipoCaracterLegalFirPJ.GetType(), "init_JS_upd_ddlTipoCaracterLegalFirPJ", "init_JS_upd_ddlTipoCaracterLegalFirPJ();", true);
                ScriptManager.RegisterStartupScript(updFirmantePJ, updFirmantePJ.GetType(), "init_JS_updFirmantePJ", "init_JS_updFirmantePJ();", true);
                ScriptManager.RegisterStartupScript(upd_ddlTipoSociedadPJ, upd_ddlTipoSociedadPJ.GetType(), "init_JS_upd_ddlTipoSociedadPJ", "init_JS_upd_ddlTipoSociedadPJ();", true);
                ScriptManager.RegisterStartupScript(upd_txtRazonSocialPJ, upd_txtRazonSocialPJ.GetType(), "init_JS_upd_txtRazonSocialPJ", "init_JS_upd_txtRazonSocialPJ();", true);
                ScriptManager.RegisterStartupScript(updABMTitularesSH, updABMTitularesSH.GetType(), "init_Js_updABMTitularesSH", "init_Js_updABMTitularesSH();", true);
                ScriptManager.RegisterStartupScript(upd_ddlTipoCaracterLegalFirSH, upd_ddlTipoCaracterLegalFirSH.GetType(), "init_Js_upd_ddlTipoCaracterLegalFirSH", "init_Js_upd_ddlTipoCaracterLegalFirSH();", true);
                ScriptManager.RegisterStartupScript(updgrillaTitularesSH, updgrillaTitularesSH.GetType(), "init_Js_updgrillaTitularesSH", "init_Js_updgrillaTitularesSH();", true);

            }


            if (!IsPostBack)
            {
                hid_return_url.Value = Request.Url.AbsoluteUri;
                ComprobarSolicitud();
                Titulo.CargarDatos(id_encomienda, "Titulares y Firmantes");
            }

        }


        //private int id_solicitud
        //{
        //    get
        //    {
        //        int ret = 0;
        //        int.TryParse(hid_id_solicitud.Value, out ret);
        //        return ret;
        //    }
        //    set
        //    {
        //        hid_id_solicitud.Value = value.ToString();
        //    }

        //}

        private int id_encomienda
        {
            get
            {
                int ret = 0;
                int.TryParse(hid_id_encomienda.Value, out ret);
                return ret;
            }
            set
            {
                hid_id_encomienda.Value = value.ToString();
            }

        }

        private void ComprobarSolicitud()
        {
            if (Page.RouteData.Values["id_encomienda"] != null)
            {
                this.id_encomienda = Convert.ToInt32(Page.RouteData.Values["id_encomienda"].ToString());
                EncomiendaBL encomiendaBL = new EncomiendaBL();
                var enc = encomiendaBL.Single(id_encomienda);
                if (enc != null)
                {
                    Guid userid_solicitud = (Guid)Membership.GetUser().ProviderUserKey;

                    if (userid_solicitud != enc.CreateUser)
                        Server.Transfer("~/Errores/Error3002.aspx");
                    else
                    {
                        if (!(enc.IdEstado == (int)Constantes.Encomienda_Estados.Incompleta ||
                                enc.IdEstado == (int)Constantes.Encomienda_Estados.Completa))
                        {
                            Server.Transfer("~/Errores/Error3003.aspx");
                        }
                    }
                }
                else
                    Server.Transfer("~/Errores/Error3004.aspx");

            }
            else
                Server.Transfer("~/Errores/Error3001.aspx");
        }

        protected void btnCargarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                CargarTiposDeSociedades();
                CargarTiposDeDocumentoPersonal();
                CargarTiposDeIngresosBrutos();
                CargarTiposDeCaracterLegal();
                CargarProvincias();
                CargarDatosTitulares(id_encomienda);

                updAgregarPersonaFisica.Update();
                updABMTitularesSH.Update();
                this.EjecutarScript(updCargarDatos, "finalizarCarga();");
                if (hid_return_url.Value.Contains("Editar"))
                {
                    btnVolver.Style["display"] = "inline";
                    updBotonesGuardar.Update();
                }
                
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updCargarDatos, "finalizarCarga();showfrmError();");
            }

        }

        public void CargarDatosTitulares(int id_encomienda)
        {

            TitularesBL titularesBL = new TitularesBL();
            FirmantesBL firmantesBL = new FirmantesBL();

            var lstTitulares = titularesBL.GetTitularesEncomienda(id_encomienda).ToList();

            //if (lstTitulares.Count() == 0)
            //    throw new Exception("No existen titulares para la solicitud: " + Convert.ToString(id_encomienda));

            var lstFirmantes = firmantesBL.GetFirmantesEncomienda(id_encomienda).ToList();

            //if (lstFirmantes.Count() == 0)
            //    throw new Exception("No existen firmantes para la encomienda: " + Convert.ToString(id_encomienda));

            grdTitularesHab.DataSource = lstTitulares;
            grdTitularesHab.DataBind();

            grdTitularesTra.DataSource = lstFirmantes;
            grdTitularesTra.DataBind();

            updGrillaTitulares.Update();
            updGrillaFirmantes.Update();
        }


        #region "Carga de datos iniciales (Combos)"

        private void CargarTiposDeSociedades()
        {
            TipoSociedadBL tipoSociedadBL = new TipoSociedadBL();

            var lstTiposdeSociedad = tipoSociedadBL.GetAll();
            ddlTipoSociedadPJ.DataSource = lstTiposdeSociedad;
            ddlTipoSociedadPJ.DataTextField = "Descripcion";
            ddlTipoSociedadPJ.DataValueField = "Id";
            ddlTipoSociedadPJ.DataBind();
            ddlTipoSociedadPJ.Items.Remove(ddlTipoSociedadPJ.Items.FindByValue("1"));   // Elimina la Sociedad Unipersonal
            ddlTipoSociedadPJ.Items.Insert(0, string.Empty);
        }


        private void CargarProvincias()
        {
            ProvinciaBL provinciaBL = new ProvinciaBL();
            var lstProvincias = provinciaBL.GetAll();

            ddlProvinciaPJ.DataValueField = "Id";
            ddlProvinciaPJ.DataTextField = "Nombre";
            ddlProvinciaPJ.DataSource = lstProvincias;
            ddlProvinciaPJ.DataBind();
            ddlProvinciaPJ.Items.Insert(0, string.Empty);

            ddlProvinciaPF.DataValueField = "Id";
            ddlProvinciaPF.DataTextField = "Nombre";
            ddlProvinciaPF.DataSource = lstProvincias;
            ddlProvinciaPF.DataBind();
            ddlProvinciaPF.Items.Insert(0, string.Empty);


        }

        private void CargarTiposDeDocumentoPersonal()
        {
            TipoDocumentoPersonalBL tipoDocumentoPersonalBL = new TipoDocumentoPersonalBL();
            //var lstTipoDocPersonal = tipoDocumentoPersonalBL.GetAll();

            var lstTipoDocPersonal = tipoDocumentoPersonalBL.GetDniPasaporte();
            ddlTipoDocumentoPF.DataSource = lstTipoDocPersonal;
            ddlTipoDocumentoPF.DataTextField = "Nombre";
            ddlTipoDocumentoPF.DataValueField = "TipoDocumentoPersonalId";
            ddlTipoDocumentoPF.DataBind();
            ddlTipoDocumentoPF.Items.Insert(0, string.Empty);

            ddlTipoDocumentoFirPJ.DataSource = lstTipoDocPersonal;
            ddlTipoDocumentoFirPJ.DataTextField = "Nombre";
            ddlTipoDocumentoFirPJ.DataValueField = "TipoDocumentoPersonalId";
            ddlTipoDocumentoFirPJ.DataBind();
            ddlTipoDocumentoFirPJ.Items.Insert(0, string.Empty);

            ddlTipoDocumentoFirPF.DataSource = lstTipoDocPersonal;
            ddlTipoDocumentoFirPF.DataTextField = "Nombre";
            ddlTipoDocumentoFirPF.DataValueField = "TipoDocumentoPersonalId";
            ddlTipoDocumentoFirPF.DataBind();
            ddlTipoDocumentoFirPF.Items.Insert(0, string.Empty);

            ddlTipoDocumentoTitSH.DataSource = lstTipoDocPersonal;
            ddlTipoDocumentoTitSH.DataTextField = "Nombre";
            ddlTipoDocumentoTitSH.DataValueField = "TipoDocumentoPersonalId";
            ddlTipoDocumentoTitSH.DataBind();
            ddlTipoDocumentoTitSH.Items.Insert(0, string.Empty);

            ddlTipoDocumentoFirSH.DataSource = lstTipoDocPersonal;
            ddlTipoDocumentoFirSH.DataTextField = "Nombre";
            ddlTipoDocumentoFirSH.DataValueField = "TipoDocumentoPersonalId";
            ddlTipoDocumentoFirSH.DataBind();
            ddlTipoDocumentoFirSH.Items.Insert(0, string.Empty);

        }

        private void CargarTiposDeCaracterLegal()
        {
            TiposDeCaracterLegalBL tiposDeCaracterLegalBL = new TiposDeCaracterLegalBL();
            int[] disponibilidadPF = new int[] { 0, 1 };
            int[] disponibilidadPJ = new int[] { 0, 2 };

            var lstTipoCaracterFirPJ = tiposDeCaracterLegalBL.GetByDisponibilidad(disponibilidadPJ);
            var lstTipoCaracterFirPF = tiposDeCaracterLegalBL.GetByDisponibilidad(disponibilidadPF);

            //firmante de la persona jurídica

            ddlTipoCaracterLegalFirPJ.DataSource = lstTipoCaracterFirPJ;
            ddlTipoCaracterLegalFirPJ.DataTextField = "NomTipoCaracter";
            ddlTipoCaracterLegalFirPJ.DataValueField = "IdTipoCaracter";
            ddlTipoCaracterLegalFirPJ.DataBind();
            ddlTipoCaracterLegalFirPJ.Items.Insert(0, string.Empty);

            //firmante de la sociedad de hecho
            ddlTipoCaracterLegalFirSH.DataSource = lstTipoCaracterFirPJ;
            ddlTipoCaracterLegalFirSH.DataTextField = "NomTipoCaracter";
            ddlTipoCaracterLegalFirSH.DataValueField = "IdTipoCaracter";
            ddlTipoCaracterLegalFirSH.DataBind();
            ddlTipoCaracterLegalFirSH.Items.Insert(0, string.Empty);

            //firmante de la persona física

            ddlTipoCaracterLegalFirPF.DataSource = lstTipoCaracterFirPF;
            ddlTipoCaracterLegalFirPF.DataTextField = "NomTipoCaracter";
            ddlTipoCaracterLegalFirPF.DataValueField = "IdTipoCaracter";
            ddlTipoCaracterLegalFirPF.DataBind();
            ddlTipoCaracterLegalFirPF.Items.Insert(0, string.Empty);

            EncomiendaFirmantesPersonasJuridicasBL encomiendaFirmantesPersonasJuridicasBL = new EncomiendaFirmantesPersonasJuridicasBL();
            string[] lstCargosFirmantes = encomiendaFirmantesPersonasJuridicasBL.GetCargoFirmantesPersonasJuridicas();

            hid_CargosFirPJ.Value = string.Join(",", lstCargosFirmantes).ToUpper();
            hid_CargosFirSH.Value = string.Join(",", lstCargosFirmantes).ToUpper();

        }
        private void CargarTiposDeIngresosBrutos()
        {
            TiposDeIngresosBrutosBL tiposDeIngresosBrutosBL = new TiposDeIngresosBrutosBL();
            var lstTipoIngresosBrutos = tiposDeIngresosBrutosBL.GetAll();

            ddlTipoIngresosBrutosPF.DataSource = lstTipoIngresosBrutos;
            ddlTipoIngresosBrutosPF.DataTextField = "NomTipoIb";
            ddlTipoIngresosBrutosPF.DataValueField = "IdTipoIb";
            ddlTipoIngresosBrutosPF.DataBind();
            ddlTipoIngresosBrutosPF.Items.Insert(0, string.Empty);

            ddlTipoIngresosBrutosPJ.DataSource = lstTipoIngresosBrutos;
            ddlTipoIngresosBrutosPJ.DataTextField = "NomTipoIb";
            ddlTipoIngresosBrutosPJ.DataValueField = "IdTipoIb";
            ddlTipoIngresosBrutosPJ.DataBind();
            ddlTipoIngresosBrutosPJ.Items.Insert(0, string.Empty);

        }

        private void CargarLocalidades(DropDownList ddlProvincias, DropDownList ddlLocalidades)
        {

            if (ddlProvincias.SelectedIndex > 0)
            {
                LocalidadBL localidadBL = new LocalidadBL();

                int idProvincia = Convert.ToInt32(ddlProvincias.SelectedValue);
                var lstLocalidades = localidadBL.GetByFKIdProvinciaExcluir(idProvincia, false);

                ddlLocalidades.DataValueField = "Id";
                ddlLocalidades.DataTextField = "Depto";
                ddlLocalidades.DataSource = lstLocalidades;
                ddlLocalidades.DataBind();
            }
            else
            {
                ddlLocalidades.Items.Clear();
            }
        }
        #endregion

        protected void ddlTipoIngresosBrutosPF_SelectedIndexChanged(object sender, EventArgs e)
        {

            string expresion = "";
            string formatoIIBB = "";
            hid_IngresosBrutosPF_formato.Value = "";
            hid_IngresosBrutosPF_formato.Value = "";

            if (ddlTipoIngresosBrutosPF.SelectedValue.Length > 0)
            {
                TiposDeIngresosBrutosBL tiposDeIngresosBrutosBL = new TiposDeIngresosBrutosBL();


                int id_tipoiibb = int.Parse(ddlTipoIngresosBrutosPF.SelectedValue);
                var lstTiposDeIIBB = tiposDeIngresosBrutosBL.GetByIdTipoIb(id_tipoiibb);

                foreach (var item in lstTiposDeIIBB)
                {
                    formatoIIBB = item.FormatoTipoIb;
                    string[] matrizFormato = formatoIIBB.Split(Convert.ToChar("-"));
                    foreach (string elemento in matrizFormato)
                    {
                        if (elemento.Length > 0)
                            expresion += "-\\d{" + elemento.Length + "}";
                    }
                    if (expresion.Length > 0)
                        expresion = expresion.Substring(1);

                    hid_IngresosBrutosPF_formato.Value = formatoIIBB;
                    hid_IngresosBrutosPF_expresion.Value = expresion;
                }

            }


            if (expresion.Length == 0)
            {
                txtIngresosBrutosPF.Text = "";
                txtIngresosBrutosPF.Enabled = false;
            }
            else
            {
                txtIngresosBrutosPF.Enabled = true;
            }
            upd_txtIngresosBrutosPJ.Update();
            ScriptManager.RegisterStartupScript(upd_ddlTipoIngresosBrutosPF, upd_ddlTipoIngresosBrutosPF.GetType(), " setFocus", "setFocus();", true);
        }

        private void LimpiarControlesABMPF()
        {

            txtApellidosPF.Text = "";
            txtNombresPF.Text = "";
            txtNroDocumentoPF.Text = "";
            txtCuitPF.Text = "";
            txtIngresosBrutosPF.Text = "";
            txtCallePF.Text = "";
            txtNroPuertaPF.Text = "";
            txtPisoPF.Text = "";
            txtDeptoPF.Text = "";
            txtTorrePF.Text = "";
            txtCPPF.Text = "";
            txtTelefonoPF.Text = "";
            txtTelefonoMovilPF.Text = "";
            txtSmsPF.Text = "";
            txtEmailPF.Text = "";
            txtApellidoFirPF.Text = "";
            txtNombresFirPF.Text = "";
            ddlTipoCaracterLegalFirPF.ClearSelection();
            ddlTipoDocumentoFirPF.ClearSelection();
            txtNroDocumentoFirPF.Text = "";
            optMismaPersona.Checked = true;
            optOtraPersona.Checked = false;
            pnlOtraPersona.Style["display"] = "none";
            ValExiste_TitularPF.Style["display"] = "none";
            ddlTipoDocumentoPF.ClearSelection();
            ddlTipoIngresosBrutosPF.ClearSelection();
            ddlProvinciaPF.ClearSelection();
            CargarLocalidades(ddlProvinciaPJ, ddlLocalidadPF);

        }

        protected void ddlProvinciaPF_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarLocalidades(ddlProvinciaPF, ddlLocalidadPF);
        }

        protected void ddlTipoSociedadPJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id_tiposociedad = 0;

            if (int.TryParse(ddlTipoSociedadPJ.SelectedValue, out id_tiposociedad))
            {

                TipoSociedadBL tipoSociedadBL = new TipoSociedadBL();

                var tiposoc = tipoSociedadBL.Single(id_tiposociedad);

                if (tiposoc != null && (id_tiposociedad == (int)Constantes.TipoSociedad.Sociedad_Hecho 
                    || id_tiposociedad ==(int)Constantes.TipoSociedad.Sociedad_no_constituidas_regularmente))
                {
                    lblRazonSocialPJ.Text = "Nombre de Fantasía:";
                    pnlAgregarTitularSH.Style["display"] = "block";
                    pnlFirmantesPJ.Style["display"] = "none";

                    grdTitularesSH.DataSource = dtTitularesSHCargados();
                    grdTitularesSH.DataBind();
                    grdFirmantesSH.DataSource = dtFirmantesSHCargados();
                    grdFirmantesSH.DataBind();
                }
                else
                {
                    lblRazonSocialPJ.Text = "Razon Social (*):";
                    pnlAgregarTitularSH.Style["display"] = "none";
                    pnlFirmantesPJ.Style["display"] = "block";
                }
                updAgregarPersonaJuridica.Update();
            }

        }

        protected void ddlTipoIngresosBrutosPJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            string expresion = "";
            string formatoIIBB = "";
            hid_IngresosBrutosPJ_formato.Value = "";
            hid_IngresosBrutosPJ_formato.Value = "";

            if (ddlTipoIngresosBrutosPJ.SelectedValue.Length > 0)
            {
                TiposDeIngresosBrutosBL tiposDeIngresosBrutosBL = new TiposDeIngresosBrutosBL();

                int id_tipoiibb = int.Parse(ddlTipoIngresosBrutosPJ.SelectedValue);
                var lstTiposDeIIBB = tiposDeIngresosBrutosBL.GetByIdTipoIb(id_tipoiibb);

                foreach (var item in lstTiposDeIIBB)
                {
                    formatoIIBB = item.FormatoTipoIb;
                    string[] matrizFormato = formatoIIBB.Split(Convert.ToChar("-"));
                    foreach (string elemento in matrizFormato)
                    {
                        if (elemento.Length > 0)
                            expresion += "-\\d{" + elemento.Length + "}";
                    }
                    if (expresion.Length > 0)
                        expresion = expresion.Substring(1);

                    hid_IngresosBrutosPJ_formato.Value = formatoIIBB;
                    hid_IngresosBrutosPJ_expresion.Value = expresion;
                }
            }
            if (expresion.Length == 0)
            {
                txtIngresosBrutosPJ.Text = "";
                txtIngresosBrutosPJ.Enabled = false;
            }
            else
            {
                txtIngresosBrutosPJ.Enabled = true;
            }
        }

        protected void ddlProvinciaPJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarLocalidades(ddlProvinciaPJ, ddlLocalidadPJ);
        }

        private bool ValidarDatosPantallaPF()
        {
            bool ret = true;
            int id_personafisica = 0;
            int.TryParse(hid_id_titular_pf.Value, out id_personafisica);

            //Valida si existe una persona física ya ingresada con el mismo numero de CUIT.
            EncomiendaTitularesPersonasFisicasBL encTitPFBL = new EncomiendaTitularesPersonasFisicasBL();

            bool existeTitular = encTitPFBL.GetByIdEncomiendaCuitIdPersonaFisica(this.id_encomienda, txtCuitPF.Text.Trim(), id_personafisica).Any();

            if (existeTitular == true)
            {
                ValExiste_TitularPF.Style["display"] = "inline-block";
                ret = false;
            }

            return ret;
        }

        private bool ValidarDatosPantallaPJ()
        {
            bool ret = true;

            int id_personajuridica = 0;
            int.TryParse(hid_id_titular_pj.Value, out id_personajuridica);

            EncomiendaTitularesPersonasJuridicasBL encTitPJBL = new EncomiendaTitularesPersonasJuridicasBL();

            //Valida si existe una persona física ya ingresada con el mismo numero de CUIT.
            bool existeTitular = encTitPJBL.GetByIdEncomiendaCuitIdPersonaJuridica(this.id_encomienda, txtCuitPJ.Text.Trim(), id_personajuridica).Any();
            if (existeTitular == true)
            {
                ValExiste_TitularPJ.Style["display"] = "inline-block";
                ret = false;
            }

            return ret;

        }

        protected void btnAceptarTitPF_Click(object sender, EventArgs e)
        {

            if (ValidarDatosPantallaPF())
            {
                try
                {

                    Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                    int id_personafisica = 0;
                    string Apellido = txtApellidosPF.Text.Trim();
                    string Nombres = txtNombresPF.Text.Trim();
                    int id_tipodoc_personal = 0;
                    string Nro_Documento = txtNroDocumentoPF.Text.Trim();
                    string Cuit = txtCuitPF.Text.Trim();
                    int id_tipoiibb = 0;
                    string Ingresos_Brutos = txtIngresosBrutosPF.Text.Trim();
                    string Calle = txtCallePF.Text.Trim();
                    int Nro_Puerta = 0;
                    string Piso = txtPisoPF.Text.Trim();
                    string Depto = txtDeptoPF.Text.Trim();
                    string Torre = txtTorrePF.Text.Trim();
                    int id_Localidad = 0;
                    string Codigo_Postal = txtCPPF.Text.Trim();
                    string Telefono = txtTelefonoPF.Text.Trim();
                    string TelefonoMovil = txtTelefonoMovilPF.Text.Trim();
                    string Sms = txtSmsPF.Text.Trim();
                    string Email = txtEmailPF.Text.Trim();
                    bool MismoFirmante = optMismaPersona.Checked;
                    int TipoCaracterLegalTitular = 1;

                    int.TryParse(ddlTipoDocumentoPF.SelectedValue, out id_tipodoc_personal);
                    int.TryParse(ddlTipoIngresosBrutosPF.SelectedValue, out id_tipoiibb);
                    int.TryParse(txtNroPuertaPF.Text.Trim(), out Nro_Puerta);
                    int.TryParse(ddlLocalidadPF.SelectedValue, out id_Localidad);
                    int.TryParse(hid_id_titular_pf.Value, out id_personafisica);

                    try
                    {
                        EncomiendaFirmantesPersonasFisicasBL encFirPFBL = new EncomiendaFirmantesPersonasFisicasBL();
                        EncomiendaTitularesPersonasFisicasBL encTitPFBL = new EncomiendaTitularesPersonasFisicasBL();

                        EncomiendaFirmantesPersonasFisicasDTO encFirPerFisDTO = new EncomiendaFirmantesPersonasFisicasDTO();
                        EncomiendaTitularesPersonasFisicasDTO encTitPerFisDTO = new EncomiendaTitularesPersonasFisicasDTO();

                        bool existeTitular = encTitPFBL.GetByIdEncomiendaCuitIdPersonaFisica(this.id_encomienda, txtCuitPF.Text.Trim(), id_personafisica).Any();
                        bool existeFirmante = encFirPFBL.GetByIdEncomiendaIdPersonaFisica(id_encomienda, id_personafisica).Any();

                        if (existeTitular)
                        {
                            encTitPerFisDTO = encTitPFBL.GetByIdEncomiendaIdPersonaFisica(id_encomienda, id_personafisica).FirstOrDefault();
                            encTitPFBL.Delete(encTitPerFisDTO);
                        }
                        if (existeFirmante)
                        {
                            encFirPerFisDTO = encFirPFBL.GetByIdEncomiendaIdPersonaFisica(id_encomienda, id_personafisica).FirstOrDefault();
                            encFirPFBL.Delete(encFirPerFisDTO);
                        }

                        encTitPerFisDTO.IdEncomienda = id_encomienda;
                        encTitPerFisDTO.Apellido = Apellido;
                        encTitPerFisDTO.Nombres = Nombres;
                        encTitPerFisDTO.IdTipoDocPersonal = id_tipodoc_personal;
                        encTitPerFisDTO.NroDocumento = Nro_Documento;
                        encTitPerFisDTO.Cuit = Cuit;
                        encTitPerFisDTO.IdTipoiibb = id_tipoiibb;
                        encTitPerFisDTO.IngresosBrutos = Ingresos_Brutos;
                        encTitPerFisDTO.Calle = Calle;
                        encTitPerFisDTO.NroPuerta = Nro_Puerta;
                        encTitPerFisDTO.Piso = Piso;
                        encTitPerFisDTO.Depto = Depto;
                        encTitPerFisDTO.Torre = Torre;
                        encTitPerFisDTO.IdLocalidad = id_Localidad;
                        encTitPerFisDTO.CodigoPostal = Codigo_Postal;
                        encTitPerFisDTO.Telefono = Telefono;
                        encTitPerFisDTO.TelefonoMovil = TelefonoMovil;
                        encTitPerFisDTO.Sms = Sms;
                        encTitPerFisDTO.Email = Email.ToLower();
                        encTitPerFisDTO.MismoFirmante = MismoFirmante;
                        encTitPerFisDTO.CreateUser = userid;
                        encTitPerFisDTO.CreateDate = DateTime.Now;

                        List<EncomiendaFirmantesPersonasFisicasDTO> lstFirmantesPFDTO = new List<EncomiendaFirmantesPersonasFisicasDTO>();
                        //encTitPerFisDTO.DtoFirmantes = new EncomiendaFirmantesPersonasFisicasDTO();
                        //encTitPerFisDTO.DtoFirmantes.IdEncomienda = id_encomienda;

                        EncomiendaFirmantesPersonasFisicasDTO encFIRPFDTO = new EncomiendaFirmantesPersonasFisicasDTO();
                        if (!MismoFirmante)
                        {
                            int.TryParse(ddlTipoDocumentoFirPF.SelectedValue, out id_tipodoc_personal);
                            int.TryParse(ddlTipoCaracterLegalFirPF.SelectedValue, out TipoCaracterLegalTitular);
                            encFIRPFDTO.Apellido = txtApellidoFirPF.Text.Trim();
                            encFIRPFDTO.Nombres = txtNombresFirPF.Text.Trim();
                            encFIRPFDTO.IdTipodocPersonal = id_tipodoc_personal;
                            encFIRPFDTO.NroDocumento = txtNroDocumentoFirPF.Text.Trim();
                            encFIRPFDTO.IdTipoCaracter = TipoCaracterLegalTitular;
                        }
                        else
                        {
                            encFIRPFDTO.Apellido = Apellido;
                            encFIRPFDTO.Nombres = Nombres;
                            encFIRPFDTO.IdTipodocPersonal = id_tipodoc_personal;
                            encFIRPFDTO.NroDocumento = Nro_Documento;
                            encFIRPFDTO.IdTipoCaracter = TipoCaracterLegalTitular;

                        }
                        lstFirmantesPFDTO.Add(encFIRPFDTO);
                        encTitPerFisDTO.EncomiendaFirmantesPersonasFisicasDTO = lstFirmantesPFDTO;

                        encTitPFBL.Insert(encTitPerFisDTO);

                    }
                    catch
                    {
                        throw;
                    }

                    CargarDatosTitulares(this.id_encomienda);
                    this.EjecutarScript(updBotonesAgregarPF, "hidefrmAgregarPersonaFisica();");
                }
                catch (Exception ex)
                {
                    LogError.Write(ex, ex.Message);
                    lblError.Text = Funciones.GetErrorMessage(ex);
                    this.EjecutarScript(updBotonesAgregarPF, "showfrmError();");
                }
            }

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {

            try
            {

                int id_persona = int.Parse(hid_id_persona_eliminar.Value);
                string TipoPersona = hid_tipopersona_eliminar.Value;


                if (TipoPersona.Equals("PF"))
                {
                    EncomiendaFirmantesPersonasFisicasBL encFirPerFisBL = new EncomiendaFirmantesPersonasFisicasBL();
                    EncomiendaTitularesPersonasFisicasBL encTitPerFisBL = new EncomiendaTitularesPersonasFisicasBL();

                    EncomiendaFirmantesPersonasFisicasDTO encFirPerFisDTO = encFirPerFisBL.GetByIdEncomiendaIdPersonaFisica(this.id_encomienda, id_persona).FirstOrDefault();
                    EncomiendaTitularesPersonasFisicasDTO encTitPerFisDTO = encTitPerFisBL.GetByIdEncomiendaIdPersonaFisica(this.id_encomienda, id_persona).FirstOrDefault();

                    if (encFirPerFisDTO != null)
                        encFirPerFisBL.Delete(encFirPerFisDTO);

                    if (encTitPerFisDTO != null)
                        encTitPerFisBL.Delete(encTitPerFisDTO);
                }
                else
                {
                    EncomiendaFirmantesPersonasJuridicasBL encFirPerJurBL = new EncomiendaFirmantesPersonasJuridicasBL();
                    EncomiendaTitularesPersonasJuridicasBL encTitPerJurBL = new EncomiendaTitularesPersonasJuridicasBL();
                    EncomiendaTitularesPersonasJuridicasPersonasFisicasBL encTitPerJurPerFisBL = new EncomiendaTitularesPersonasJuridicasPersonasFisicasBL();

                    EncomiendaFirmantesPersonasJuridicasDTO encFirPerJurDTO = encFirPerJurBL.GetByIdEncomiendaIdPersonaJuridica(this.id_encomienda, id_persona).FirstOrDefault();
                    EncomiendaTitularesPersonasJuridicasDTO encTitPerJurDTO = encTitPerJurBL.GetByIdEncomiendaIdPersonaJuridica(this.id_encomienda, id_persona).FirstOrDefault();
                    EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO encTitPerJurPerFisDTO = encTitPerJurPerFisBL.GetByIdEncomiendaIdPersonaJuridica(this.id_encomienda, id_persona).FirstOrDefault();

                    if (encTitPerJurPerFisDTO != null)
                        encTitPerJurPerFisBL.Delete(encTitPerJurPerFisDTO);

                    if (encFirPerJurDTO != null)
                        encFirPerJurBL.Delete(encFirPerJurDTO);

                    if (encTitPerJurDTO != null)
                        encTitPerJurBL.Delete(encTitPerJurDTO);
                }

                CargarDatosTitulares(this.id_encomienda);
                this.EjecutarScript(updConfirmarEliminar, "hideConfirmarEliminar();");

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updConfirmarEliminar, "showfrmError();");
            }
        }

        protected void btnShowAgregarPF_Click(object sender, EventArgs e)
        {
            hid_id_titular_pf.Value = "0";
            LimpiarControlesABMPF();
            updAgregarPersonaFisica.Update();
            this.EjecutarScript(updShowAgregarPersonas, "showfrmAgregarPersonaFisica();");

        }

        protected void btnEditarTitular_Click(object sender, EventArgs e)
        {
            LinkButton btnEditarTitular = (LinkButton)sender;

            string TipoPersona = btnEditarTitular.CommandName;
            int id_persona = int.Parse(btnEditarTitular.CommandArgument);

            if (TipoPersona.Equals("PF"))
            {
                LimpiarControlesABMPF();
                hid_id_titular_pf.Value = id_persona.ToString();
                CargarDatosTitularPF(id_persona);
                updAgregarPersonaFisica.Update();
                this.EjecutarScript(updGrillaTitulares, "showfrmAgregarPersonaFisica();");
            }

            if (TipoPersona.Equals("PJ"))
            {
                LimpiarControlesABMPJ();
                hid_id_titular_pj.Value = id_persona.ToString();
                CargarDatosTitularPJ(id_persona);
                updAgregarPersonaJuridica.Update();
                this.EjecutarScript(updGrillaTitulares, "showfrmAgregarPersonaJuridica();");
            }

        }

        private void CargarDatosTitularPJ(int id_personajuridica)
        {
            EncomiendaTitularesPersonasJuridicasBL encomiendaTitularesPersonasJuridicasBL = new EncomiendaTitularesPersonasJuridicasBL();
            LocalidadBL localidadBL = new LocalidadBL();
            FirmantesBL firmantesBL = new FirmantesBL();

            var pj = encomiendaTitularesPersonasJuridicasBL.Single(id_personajuridica);

            if (pj != null)
            {
                hid_id_titular_pj.Value = id_personajuridica.ToString();

                ddlTipoSociedadPJ.SelectedValue = pj.IdTipoSociedad.ToString();
                txtRazonSocialPJ.Text = pj.RazonSocial;
                txtCuitPJ.Text = pj.CUIT;
                ddlTipoIngresosBrutosPJ.SelectedValue = pj.IdTipoIb.ToString();
                txtIngresosBrutosPJ.Text = pj.NroIb;
                txtCallePJ.Text = pj.Calle;
                txtNroPuertaPJ.Text = pj.NroPuerta.ToString();
                txtPisoPJ.Text = pj.Piso;
                txtDeptoPJ.Text = pj.Depto;
                txtTorrePJ.Text = (pj.Torre ?? "").Trim();
                txtCPPJ.Text = pj.CodigoPostal;
                txtTelefonoPJ.Text = pj.Telefono;
                txtEmailPJ.Text = pj.Email;

                ddlTipoIngresosBrutosPJ_SelectedIndexChanged(ddlTipoIngresosBrutosPJ, new EventArgs());

                var localidad = localidadBL.Single(pj.IdLocalidad);
                if (localidad != null)
                {
                    ddlProvinciaPJ.SelectedValue = localidad.IdProvincia.ToString();
                    CargarLocalidades(ddlProvinciaPJ, ddlLocalidadPJ);
                    ddlLocalidadPJ.SelectedValue = pj.IdLocalidad.ToString();
                }
                if (pj.IdTipoSociedad == (int)Constantes.TipoSociedad.Sociedad_Hecho ||
                    pj.IdTipoSociedad == (int)Constantes.TipoSociedad.Sociedad_no_constituidas_regularmente)
                {
                    lblRazonSocialPJ.Text = "Nombre de Fantasía:";
                    pnlAgregarTitularSH.Style["display"] = "block";
                    pnlFirmantesPJ.Style["display"] = "none";

                    TitularesBL titularesBL = new TitularesBL();
                    var lstTitularesSH = titularesBL.GetTitularesEncomiendaSH(id_personajuridica).ToList();

                    //if (lstTitularesSH.Count() == 0)
                    //    throw new Exception("No existen titulares para la solicitud: " + Convert.ToString(id_personajuridica));

                    grdTitularesSH.DataSource = lstTitularesSH;
                    grdTitularesSH.DataBind();

                    DataTable dtFirmantesSH = dtFirmantesSHCargados();
                    dtFirmantesSH.Clear();
                    foreach (var titularsh in lstTitularesSH)
                    {
                        int id_firmante_pj = titularsh.IdFirmantePj.Value;
                        Guid rowid_titular = titularsh.RowId;
                        string FirmanteDe = titularsh.Apellidos + " " + titularsh.Nombres;


                        var lstFirmantesSH = firmantesBL.GetFirmantesPJPF(id_firmante_pj).ToList();

                        if (lstFirmantesSH.Count() == 0)
                            throw new Exception("No existen firmantes con el ID: " + Convert.ToString(id_firmante_pj));

                        foreach (var firmanteSH in lstFirmantesSH)
                        {

                            DataRow datarw;
                            datarw = dtFirmantesSH.NewRow();

                            datarw[0] = FirmanteDe;
                            datarw[1] = firmanteSH.Apellidos;
                            datarw[2] = firmanteSH.Nombres;
                            datarw[3] = firmanteSH.TipoDoc;
                            datarw[4] = firmanteSH.NroDoc;
                            datarw[5] = firmanteSH.NomTipoCaracter;
                            datarw[6] = firmanteSH.IdTipoDocPersonal;
                            datarw[7] = firmanteSH.IdTipoCaracter;
                            datarw[8] = firmanteSH.CargoFirmantePj;
                            datarw[9] = firmanteSH.Email;
                            datarw[10] = Guid.NewGuid();
                            datarw[11] = rowid_titular;
                            datarw[12] = firmanteSH.FirmanteMismaPersona;

                            dtFirmantesSH.Rows.Add(datarw);
                        }

                        grdFirmantesSH.DataSource = dtFirmantesSH;
                        grdFirmantesSH.DataBind();
                    }

                }
                else
                {

                    var lstFirmantesPJ = firmantesBL.GetFirmantesPJ(id_personajuridica).ToList();

                    if (lstFirmantesPJ.Count() == 0)
                        throw new Exception("No existen firmantes con el ID: " + Convert.ToString(id_personajuridica));

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Apellidos", typeof(string));
                    dt.Columns.Add("Nombres", typeof(string));
                    dt.Columns.Add("TipoDoc", typeof(string));
                    dt.Columns.Add("NroDoc", typeof(string));
                    dt.Columns.Add("nom_tipocaracter", typeof(string));
                    dt.Columns.Add("id_tipodoc_personal", typeof(int));
                    dt.Columns.Add("email", typeof(string));
                    dt.Columns.Add("id_tipocaracter", typeof(int));
                    dt.Columns.Add("cargo_firmante_pj", typeof(string));
                    dt.Columns.Add("rowindex", typeof(int));
                    int rowindex = 0;

                    foreach (var item in lstFirmantesPJ)
                    {
                        dt.Rows.Add(item.Apellidos, item.Nombres, item.TipoDoc, item.NroDoc, item.NomTipoCaracter, item.IdTipoDocPersonal,
                                item.Email, item.IdTipoCaracter, item.CargoFirmantePj, rowindex);
                        rowindex++;
                    }

                    grdFirmantesPJ.DataSource = dt;
                    grdFirmantesPJ.DataBind();
                }

            }

        }

        private void CargarDatosTitularPF(int id_personafisica)
        {
            EncomiendaTitularesPersonasFisicasBL encTitPFBL = new EncomiendaTitularesPersonasFisicasBL();
            var pf = encTitPFBL.Single(id_personafisica);

            if (pf != null)
            {

                if (pf.MismoFirmante)
                {
                    optMismaPersona.Checked = true;
                    optOtraPersona.Checked = false;

                    txtApellidoFirPF.Text = "";
                    txtNombresFirPF.Text = "";
                    ddlTipoDocumentoFirPF.ClearSelection();
                    txtNroDocumentoFirPF.Text = "";
                    ddlTipoCaracterLegalFirPF.ClearSelection();
                }
                else
                {

                    optOtraPersona.Checked = true;
                    optMismaPersona.Checked = false;

                    EncomiendaFirmantesPersonasFisicasBL encFirPFBL = new EncomiendaFirmantesPersonasFisicasBL();
                    var firmante = encFirPFBL.GetByFKIdPersonaFisica(id_personafisica).FirstOrDefault();

                    if (firmante != null)
                    {
                        txtApellidoFirPF.Text = firmante.Apellido;
                        txtNombresFirPF.Text = firmante.Nombres;
                        ddlTipoDocumentoFirPF.SelectedValue = firmante.IdTipodocPersonal.ToString();
                        txtNroDocumentoFirPF.Text = firmante.NroDocumento;
                        ddlTipoCaracterLegalFirPF.SelectedValue = firmante.IdTipoCaracter.ToString();
                    }

                    pnlOtraPersona.Style["display"] = "block";

                }

                txtApellidosPF.Text = pf.Apellido;
                txtNombresPF.Text = pf.Nombres;
                txtNroDocumentoPF.Text = pf.NroDocumento.ToString();
                txtCuitPF.Text = pf.Cuit;
                txtIngresosBrutosPF.Text = pf.IngresosBrutos;
                txtCallePF.Text = pf.Calle;
                txtNroPuertaPF.Text = pf.NroPuerta.ToString();
                txtPisoPF.Text = pf.Piso;
                txtDeptoPF.Text = pf.Depto;
                txtTorrePJ.Text = (pf.Torre ?? "").Trim();
                txtCPPF.Text = pf.CodigoPostal;
                txtTelefonoPF.Text = pf.Telefono;
                txtTelefonoMovilPF.Text = pf.TelefonoMovil;
                txtSmsPF.Text = pf.Sms;
                txtEmailPF.Text = pf.Email;

                ddlTipoDocumentoPF.SelectedValue = pf.IdTipoDocPersonal.ToString();
                ddlTipoIngresosBrutosPF.SelectedValue = pf.IdTipoiibb.ToString();
                ddlTipoIngresosBrutosPF_SelectedIndexChanged(ddlTipoIngresosBrutosPF, new EventArgs());

                LocalidadBL localidadBL = new LocalidadBL();

                var localidad = localidadBL.Single(pf.IdLocalidad);

                if (localidad != null)
                {
                    ddlProvinciaPF.SelectedValue = localidad.IdProvincia.ToString();
                    CargarLocalidades(ddlProvinciaPF, ddlLocalidadPF);
                    ddlLocalidadPF.SelectedValue = pf.IdLocalidad.ToString();
                }
            }

        }

        protected void btnShowAgregarPJ_Click(object sender, EventArgs e)
        {
            hid_id_titular_pj.Value = "0";
            LimpiarControlesABMPJ();
            updAgregarPersonaJuridica.Update();
            this.EjecutarScript(updShowAgregarPersonas, "showfrmAgregarPersonaJuridica();");
        }

        private void LimpiarControlesABMPJ()
        {
            txtRazonSocialPJ.Text = "";
            txtCuitPJ.Text = "";
            txtIngresosBrutosPJ.Text = "";
            txtCallePJ.Text = "";
            txtNroPuertaPJ.Text = "";
            txtPisoPJ.Text = "";
            txtDeptoPJ.Text = "";
            txtTorrePJ.Text = "";
            txtCPPJ.Text = "";
            txtTelefonoPJ.Text = "";
            txtCargoFirPJ.Text = "";
            txtEmailPJ.Text = "";
            ValExiste_TitularPJ.Style["display"] = "none";

            ddlTipoSociedadPJ.ClearSelection();
            ddlProvinciaPJ.ClearSelection();
            ddlTipoIngresosBrutosPJ.ClearSelection();
            CargarLocalidades(ddlProvinciaPJ, ddlLocalidadPJ);

            DataTable dt = dtFirmantesCargados();
            dt.Clear();
            grdFirmantesPJ.DataSource = dt;
            grdFirmantesPJ.DataBind();


            DataTable dtTitSH = dtTitularesSHCargados();
            DataTable dtFirSH = dtFirmantesSHCargados();
            dtTitSH.Clear();
            dtFirSH.Clear();
            grdTitularesSH.DataSource = dtTitSH;
            grdTitularesSH.DataBind();

            grdFirmantesSH.DataSource = dtFirSH;
            grdFirmantesSH.DataBind();

            lblRazonSocialPJ.Text = "Razon Social (*):";
            pnlAgregarTitularSH.Style["display"] = "none";
            pnlFirmantesPJ.Style["display"] = "block";

        }

        private DataTable dtFirmantesCargados()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Apellidos", typeof(string));
            dt.Columns.Add("Nombres", typeof(string));
            dt.Columns.Add("TipoDoc", typeof(string));
            dt.Columns.Add("NroDoc", typeof(string));
            dt.Columns.Add("nom_tipocaracter", typeof(string));
            dt.Columns.Add("id_tipodoc_personal", typeof(int));
            dt.Columns.Add("email", typeof(string));
            dt.Columns.Add("id_tipocaracter", typeof(int));
            dt.Columns.Add("cargo_firmante_pj", typeof(string));
            dt.Columns.Add("rowindex", typeof(int));


            foreach (GridViewRow row in grdFirmantesPJ.Rows)
            {
                DataRow datarw;
                datarw = dt.NewRow();

                HiddenField hid_id_tipodoc_grdFirmantes = (HiddenField)grdFirmantesPJ.Rows[row.RowIndex].Cells[0].FindControl("hid_id_tipodoc_grdFirmantes");
                HiddenField hid_id_caracter_grdFirmantes = (HiddenField)grdFirmantesPJ.Rows[row.RowIndex].Cells[0].FindControl("hid_id_caracter_grdFirmantes");


                datarw[0] = HttpUtility.HtmlDecode(row.Cells[1].Text);
                datarw[1] = HttpUtility.HtmlDecode(row.Cells[2].Text);
                datarw[2] = HttpUtility.HtmlDecode(row.Cells[3].Text);
                datarw[3] = HttpUtility.HtmlDecode(row.Cells[4].Text);
                datarw[4] = HttpUtility.HtmlDecode(row.Cells[6].Text);
                datarw[5] = int.Parse(hid_id_tipodoc_grdFirmantes.Value);
                datarw[6] = HttpUtility.HtmlDecode(row.Cells[5].Text);
                datarw[7] = int.Parse(hid_id_caracter_grdFirmantes.Value);
                datarw[8] = HttpUtility.HtmlDecode(row.Cells[7].Text).Trim();
                datarw[9] = row.RowIndex;

                dt.Rows.Add(datarw);

            }

            return dt;
        }

        protected void ddlTipoCaracterLegalFirPJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id_tipocaracter = int.Parse(ddlTipoCaracterLegalFirPJ.SelectedValue);
            TiposDeCaracterLegalBL tiposDeCaracterLegalBL = new TiposDeCaracterLegalBL();

            var tc = tiposDeCaracterLegalBL.Single(id_tipocaracter);
            Req_CargoFirPJ.Style["display"] = "none";

            if (tc != null && tc.MuestraCargoTipoCaracter)
            {
                rowCargoFirmantePJ.Style["display"] = "block";
            }
            else
            {
                txtCargoFirPJ.Text = "";
                hid_CargosFir_seleccionado.Value = "";
                rowCargoFirmantePJ.Style["display"] = "none";
            }

        }

        protected void btnShowAgregarFirPJ_Click(object sender, EventArgs e)
        {
            LimpiarControlesFirPJ();
            this.EjecutarScript(updbtnShowAgregarFirPJ, "showfrmAgregarFirmantePJ();");
        }

        private void LimpiarControlesFirPJ()
        {
            hid_CargosFir_seleccionado.Value = "";
            hid_rowindex_fir.Value = "";
            txtApellidosFirPJ.Text = "";
            txtNombresFirPJ.Text = "";
            ddlTipoDocumentoFirPJ.ClearSelection();
            txtNroDocumentoFirPJ.Text = "";
            txtEmailFirPJ.Text = "";
            ddlTipoCaracterLegalFirPJ.ClearSelection();
            txtCargoFirPJ.Text = "";
            rowCargoFirmantePJ.Style["display"] = "none";
            Req_CargoFirPJ.Style["display"] = "none";
            ValExiste_TipoNroDocFirPJ.Style["display"] = "none";
            updFirmantePJ.Update();
        }

        protected void btnAceptarFirPJ_Click(object sender, EventArgs e)
        {

            DataTable dt = dtFirmantesCargados();
            bool Validation = true;

            if (hid_rowindex_fir.Value.Length == 0)
            {

                //Agregar firmante (Persona Jurídica)
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["TipoDoc"].ToString().Equals(ddlTipoDocumentoFirPJ.SelectedItem.Text.Trim()) &&
                        dr["NroDoc"].ToString().Equals(txtNroDocumentoFirPJ.Text.Trim()))
                    {
                        ValExiste_TipoNroDocFirPJ.Style["display"] = "inline-block";
                        updFirmantePJ.Update();
                        Validation = false;
                    }
                }

                if (Validation)
                {
                    dt.Rows.Add(txtApellidosFirPJ.Text.Trim(), txtNombresFirPJ.Text.Trim(), ddlTipoDocumentoFirPJ.SelectedItem.Text.Trim(), txtNroDocumentoFirPJ.Text.Trim(),
                        ddlTipoCaracterLegalFirPJ.SelectedItem.Text, int.Parse(ddlTipoDocumentoFirPJ.SelectedValue), txtEmailFirPJ.Text.Trim(), int.Parse(ddlTipoCaracterLegalFirPJ.SelectedValue),
                        txtCargoFirPJ.Text.Trim(), dt.Rows.Count);

                }

            }
            else
            {

                //Editar firmante (Persona Jurídica)
                foreach (DataRow drVal in dt.Rows)
                {
                    if (drVal["TipoDoc"].ToString().Equals(ddlTipoDocumentoFirPJ.SelectedItem.Text.Trim()) &&
                        drVal["NroDoc"].ToString().Equals(txtNroDocumentoFirPJ.Text.Trim()) &&
                        drVal["rowindex"].ToString() != hid_rowindex_fir.Value)
                    {
                        ValExiste_TipoNroDocFirPJ.Style["display"] = "inline-block";
                        updFirmantePJ.Update();
                        Validation = false;
                    }
                }

                if (Validation)
                {
                    int rowindex = int.Parse(hid_rowindex_fir.Value);
                    DataRow dr = dt.Rows[rowindex];

                    dt.Rows[rowindex]["Apellidos"] = txtApellidosFirPJ.Text.Trim();
                    dt.Rows[rowindex]["Nombres"] = txtNombresFirPJ.Text.Trim();
                    dt.Rows[rowindex]["TipoDoc"] = ddlTipoDocumentoFirPJ.SelectedItem.Text;
                    dt.Rows[rowindex]["NroDoc"] = txtNroDocumentoFirPJ.Text.Trim();
                    dt.Rows[rowindex]["nom_tipocaracter"] = ddlTipoCaracterLegalFirPJ.SelectedItem.Text;
                    dt.Rows[rowindex]["id_tipodoc_personal"] = int.Parse(ddlTipoDocumentoFirPJ.SelectedValue);
                    dt.Rows[rowindex]["email"] = txtEmailFirPJ.Text.Trim();
                    dt.Rows[rowindex]["id_tipocaracter"] = int.Parse(ddlTipoCaracterLegalFirPJ.SelectedValue);
                    dt.Rows[rowindex]["cargo_firmante_pj"] = txtCargoFirPJ.Text.Trim();
                }


            }

            grdFirmantesPJ.DataSource = dt;
            grdFirmantesPJ.DataBind();
            updgrdFirmantesPJ.Update();

            if (Validation)
                this.EjecutarScript(updgrdFirmantesPJ, "hidefrmAgregarFirmantePJ();");

        }

        protected void btnEditarFirPJ_Click(object sender, EventArgs e)
        {

            LimpiarControlesFirPJ();

            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.Parent.Parent;
            DataTable dt = dtFirmantesCargados();

            DataRow dr = dt.Rows[row.RowIndex];

            txtApellidosFirPJ.Text = dr["Apellidos"].ToString();
            txtNombresFirPJ.Text = dr["Nombres"].ToString();
            txtNroDocumentoFirPJ.Text = dr["NroDoc"].ToString();
            txtEmailFirPJ.Text = dr["Email"].ToString();
            ddlTipoDocumentoFirPJ.SelectedValue = dr["id_tipodoc_personal"].ToString();
            ddlTipoCaracterLegalFirPJ.SelectedValue = dr["id_tipocaracter"].ToString();

            txtCargoFirPJ.Text = dr["cargo_firmante_pj"].ToString();

            if (txtCargoFirPJ.Text.Length > 0)
            {
                rowCargoFirmantePJ.Style["display"] = "block";
            }
            else
            {
                rowCargoFirmantePJ.Style["display"] = "none";
            }

            hid_rowindex_fir.Value = row.RowIndex.ToString();
            updFirmantePJ.Update();
            this.EjecutarScript(updgrdFirmantesPJ, "showfrmAgregarFirmantePJ();");

        }

        protected void btnEliminarFirmantePJ_Click(object sender, EventArgs e)
        {
            int rowindex = int.Parse(hid_rowindex_eliminar.Value);
            DataTable dt = dtFirmantesCargados();

            dt.Rows.Remove(dt.Rows[rowindex]);
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                dr["rowindex"] = i;
                i++;
            }

            grdFirmantesPJ.DataSource = dt;
            grdFirmantesPJ.DataBind();
            updgrdFirmantesPJ.Update();

            this.EjecutarScript(updConfirmarEliminarFirPJ, "hidefrmConfirmarEliminarFirPJ();");
        }

        protected void btnAceptarTitPJ_Click(object sender, EventArgs e)
        {
            if (ValidarDatosPantallaPJ())
            {
                try
                {

                    Guid userid = (Guid)Membership.GetUser().ProviderUserKey;

                    int id_personajuridica = 0;
                    int id_tiposociedad = 0;
                    string Razon_Social = txtRazonSocialPJ.Text.Trim();
                    string Cuit = txtCuitPJ.Text.Trim();
                    int id_tipoiibb = 0;
                    string Ingresos_Brutos = txtIngresosBrutosPJ.Text.Trim();
                    string Calle = txtCallePJ.Text.Trim();
                    int Nro_Puerta = 0;
                    string Piso = txtPisoPJ.Text.Trim();
                    string Depto = txtDeptoPJ.Text.Trim();
                    string Torre = txtTorrePJ.Text.Trim();
                    int id_Localidad = 0;
                    string Codigo_Postal = txtCPPJ.Text.Trim();
                    string Telefono = txtTelefonoPJ.Text.Trim();
                    string Email = txtEmailPJ.Text.Trim();

                    int.TryParse(ddlTipoSociedadPJ.SelectedValue, out id_tiposociedad);
                    int.TryParse(ddlTipoIngresosBrutosPJ.SelectedValue, out id_tipoiibb);
                    int.TryParse(txtNroPuertaPJ.Text.Trim(), out Nro_Puerta);
                    int.TryParse(ddlLocalidadPJ.SelectedValue, out id_Localidad);

                    int.TryParse(hid_id_titular_pj.Value, out id_personajuridica);



                    try
                    {

                        EncomiendaTitularesPersonasJuridicasPersonasFisicasBL encTitPJPFBL = new EncomiendaTitularesPersonasJuridicasPersonasFisicasBL();
                        EncomiendaFirmantesPersonasJuridicasBL encFirPJBL = new EncomiendaFirmantesPersonasJuridicasBL();
                        EncomiendaTitularesPersonasJuridicasBL encTitPJBL = new EncomiendaTitularesPersonasJuridicasBL();

                        EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO encTitPerJurPerFisDTO = new EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO();
                        EncomiendaFirmantesPersonasJuridicasDTO encFirPerJurDTO = new EncomiendaFirmantesPersonasJuridicasDTO();
                        EncomiendaTitularesPersonasJuridicasDTO encTitPerJurDTO = new EncomiendaTitularesPersonasJuridicasDTO();

                        bool existeTitPJPF = encTitPJPFBL.GetByIdEncomiendaIdPersonaJuridica(id_encomienda, id_personajuridica).Any();
                        bool existeFir = encFirPJBL.GetByIdEncomiendaIdPersonaJuridica(id_encomienda, id_personajuridica).Any();
                        bool existeTit = encTitPJBL.GetByIdEncomiendaIdPersonaJuridica(id_encomienda, id_personajuridica).Any();
                        
                        if (existeTitPJPF)
                            encTitPerJurPerFisDTO = encTitPJPFBL.GetByIdEncomiendaIdPersonaJuridica(id_encomienda, id_personajuridica).FirstOrDefault();

                        if (existeFir)
                            encFirPerJurDTO = encFirPJBL.GetByIdEncomiendaIdPersonaJuridica(id_encomienda, id_personajuridica).FirstOrDefault();

                        if (existeTit)
                            encTitPerJurDTO = encTitPJBL.GetByIdEncomiendaIdPersonaJuridica(id_encomienda, id_personajuridica).FirstOrDefault();

                       
                        if (id_personajuridica > 0)
                        {
                            if (encTitPerJurPerFisDTO != null)
                                encTitPJPFBL.Delete(encTitPerJurPerFisDTO);
                            
                            if (encFirPerJurDTO != null)
                                encFirPJBL.Delete(encFirPerJurDTO);

                            if (encTitPerJurDTO != null)
                                encTitPJBL.Delete(encTitPerJurDTO);
                        }

                        DataTable dtFirmantesSH = dtFirmantesSHCargados();
                        DataTable dtTitularesSH = dtTitularesSHCargados();
                        DataTable dtFirmantes = dtFirmantesCargados();

                        FirmantesSHDTO firmantesSHDTO;
                        TitularesSHDTO titularesSHDTO;
                        EncomiendaFirmantesPersonasJuridicasDTO encFirPerJurInsertDTO;
                        encTitPerJurDTO = new EncomiendaTitularesPersonasJuridicasDTO();

                        encTitPerJurDTO.IdPersonaJuridica = id_personajuridica;
                        encTitPerJurDTO.IdEncomienda = id_encomienda;
                        encTitPerJurDTO.IdTipoSociedad = id_tiposociedad;
                        encTitPerJurDTO.RazonSocial = Razon_Social;
                        encTitPerJurDTO.CUIT = Cuit;
                        encTitPerJurDTO.IdTipoIb = id_tipoiibb;
                        encTitPerJurDTO.NroIb = Ingresos_Brutos;
                        encTitPerJurDTO.Calle = Calle;
                        encTitPerJurDTO.NroPuerta = Nro_Puerta;
                        encTitPerJurDTO.Piso = Piso;
                        encTitPerJurDTO.Depto = Depto;
                        encTitPerJurDTO.Torre = Torre;
                        encTitPerJurDTO.IdLocalidad = id_Localidad;
                        encTitPerJurDTO.CodigoPostal = Codigo_Postal;
                        encTitPerJurDTO.Telefono = Telefono;
                        encTitPerJurDTO.Email = Email;
                        encTitPerJurDTO.CreateUser = userid;
                        encTitPerJurDTO.CreateDate = DateTime.Now;

                        List<FirmantesSHDTO> lstFirmantesSHDTO = new List<FirmantesSHDTO>();
                        List<TitularesSHDTO> lstTitularesSHDTO = new List<TitularesSHDTO>();
                        List<EncomiendaFirmantesPersonasJuridicasDTO> lstEncomiendaFirmantesPersonasJuridicasDTO = new List<EncomiendaFirmantesPersonasJuridicasDTO>();

                        foreach (DataRow dr in dtFirmantesSH.Rows)
                        {
                            firmantesSHDTO = new FirmantesSHDTO();
                            firmantesSHDTO.FirmanteDe = dr["FirmanteDe"].ToString();
                            firmantesSHDTO.Apellidos = dr["Apellidos"].ToString();
                            firmantesSHDTO.Nombres = dr["Nombres"].ToString();
                            firmantesSHDTO.TipoDoc = dr["TipoDoc"].ToString();
                            firmantesSHDTO.NroDoc = dr["NroDoc"].ToString();
                            firmantesSHDTO.nom_tipocaracter = dr["nom_tipocaracter"].ToString();
                            firmantesSHDTO.id_tipodoc_personal = Convert.ToInt32(dr["id_tipodoc_personal"].ToString());
                            firmantesSHDTO.id_tipocaracter = Convert.ToInt32(dr["id_tipocaracter"].ToString());
                            firmantesSHDTO.cargo_firmante = dr["cargo_firmante"].ToString();
                            firmantesSHDTO.email = dr["email"].ToString();
                            firmantesSHDTO.rowid = (Guid)dr["rowid"];
                            firmantesSHDTO.rowid_titular = (Guid)dr["rowid_titular"];
                            firmantesSHDTO.misma_persona = Convert.ToBoolean(dr["misma_persona"].ToString());
                            lstFirmantesSHDTO.Add(firmantesSHDTO);
                        }

                        foreach (DataRow rowTitularSH in dtTitularesSH.Rows)
                        {
                            titularesSHDTO = new TitularesSHDTO();
                            titularesSHDTO.RowId = Guid.Parse(rowTitularSH["rowid"].ToString());
                            titularesSHDTO.Apellidos = rowTitularSH["Apellidos"].ToString();
                            titularesSHDTO.Nombres = rowTitularSH["Nombres"].ToString();
                            titularesSHDTO.TipoDoc = rowTitularSH["TipoDoc"].ToString();
                            titularesSHDTO.NroDoc = rowTitularSH["NroDoc"].ToString();
                            titularesSHDTO.IdTipoDocPersonal = Convert.ToInt32(rowTitularSH["id_tipodoc_personal"].ToString());
                            titularesSHDTO.Email = rowTitularSH["email"].ToString();
                            lstTitularesSHDTO.Add(titularesSHDTO);
                        }

                        foreach (DataRow rowFirPerJur in dtFirmantes.Rows)
                        {
                            encFirPerJurInsertDTO = new EncomiendaFirmantesPersonasJuridicasDTO();
                            encFirPerJurInsertDTO.IdEncomienda = id_encomienda;
                            encFirPerJurInsertDTO.IdPersonaJuridica = id_personajuridica;
                            encFirPerJurInsertDTO.Apellido = rowFirPerJur["Apellidos"].ToString();
                            encFirPerJurInsertDTO.Nombres = rowFirPerJur["Nombres"].ToString();
                            encFirPerJurInsertDTO.IdTipoDocPersonal = int.Parse(rowFirPerJur["id_tipodoc_personal"].ToString());
                            encFirPerJurInsertDTO.NroDocumento = rowFirPerJur["NroDoc"].ToString();
                            encFirPerJurInsertDTO.Email = rowFirPerJur["Email"].ToString();
                            encFirPerJurInsertDTO.IdTipoCaracter = int.Parse(rowFirPerJur["id_tipocaracter"].ToString());
                            encFirPerJurInsertDTO.CargoFirmantePj = rowFirPerJur["cargo_firmante_pj"].ToString();
                            lstEncomiendaFirmantesPersonasJuridicasDTO.Add(encFirPerJurInsertDTO);
                        }


                        encTitPerJurDTO.firmantesSH = lstFirmantesSHDTO;
                        encTitPerJurDTO.titularesSH = lstTitularesSHDTO;
                        encTitPerJurDTO.EncomiendaFirmantesPersonasJuridicasDTO = lstEncomiendaFirmantesPersonasJuridicasDTO;

                        encTitPJBL.Insert(encTitPerJurDTO);
                    }
                    catch
                    {
                        throw;
                    }


                    CargarDatosTitulares(this.id_encomienda);
                    this.EjecutarScript(updBotonesAgregarPF, "hidefrmAgregarPersonaJuridica();$('#Req_FirmantesPJ').hide();");
                }
                catch (Exception ex)
                {
                    LogError.Write(ex, ex.Message);
                    lblError.Text = Funciones.GetErrorMessage(ex);
                    this.EjecutarScript(updBotonesAgregarPJ, "showfrmError();");
                }
            }
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdTitularesHab.Rows.Count == 0)
                    throw new Exception(Errors.ENCOMIENDA_SOLICITUD_INGRESAR_TITULARES);

                EncomiendaBL encomiendaBL = new EncomiendaBL();
                var sol = encomiendaBL.Single(this.id_encomienda);

                Response.Redirect(string.Format("~/" + RouteConfig.VISOR_ENCOMIENDA + "{0}", id_encomienda));

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesGuardar, "showfrmError();");

            }

        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(string.Format("~/" + RouteConfig.VISOR_ENCOMIENDA + "{0}", id_encomienda));
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesGuardar, "showfrmError();");

            }
        }
        private bool solicitarIngresoProfesional(int id_encomienda)//, int id_tipocertificado)
        {
            bool ctrolCargaProfesional = false;

            EncomiendaRubrosBL encomiendaRubrosBL = new EncomiendaRubrosBL();
            EncomiendaNormativasBL eEncomiendaNormativasBL = new EncomiendaNormativasBL();

            var norm = eEncomiendaNormativasBL.GetByFKIdEncomienda(this.id_encomienda).FirstOrDefault();

            if (id_encomienda == 0 && norm != null)
            {
                // Si es opcion 2 y tiene normativa debe ir al profesional
                ctrolCargaProfesional = true;
            }
            else if (ctrolCargaProfesional && id_encomienda == 0 && !encomiendaRubrosBL.Encomienda_ValidarCargaProfesional_porRubro(id_encomienda))
            {
                // por opcion 2, cuando tiene un rubro  VCOL (Vivienda Colectiva) y VIND (Vivienda Individual), 
                // no debe pedir profesional
                ctrolCargaProfesional = false;
            }

            return ctrolCargaProfesional;
        }

        private void LimpiarDatosABMTitularesSH()
        {
            txtApellidosTitSH.Text = "";
            txtNombresTitSH.Text = "";
            ddlTipoDocumentoTitSH.ClearSelection();
            txtNroDocumentoTitSH.Text = "";
            txtEmailTitSH.Text = "";

            txtApellidosFirSH.Text = "";
            txtNombresFirSH.Text = "";
            ddlTipoDocumentoFirSH.ClearSelection();
            txtNroDocumentoFirSH.Text = "";
            txtEmailFirSH.Text = "";

            txtCargoFirSH.Text = "";
            pnlCargoFirmanteSH.Style["display"] = "none";
            ddlTipoCaracterLegalFirSH.ClearSelection();

            optOtraPersonaSH.Checked = false;
            optMismaPersonaSH.Checked = true;
            pnlFirSH.Style["display"] = "none";
        }

        protected void btnAgregarTitularSH_Click(object sender, EventArgs e)
        {
            LimpiarDatosABMTitularesSH();
            hid_rowindex_titSH.Value = "";
            updABMTitularesSH.Update();
            this.EjecutarScript(updBotonesAgregarTitularSH, "showfrmAgregarTitularesSH();");

        }

        protected void ddlTipoCaracterLegalFirSH_SelectedIndexChanged(object sender, EventArgs e)
        {

            int id_tipocaracter = 0;

            int.TryParse(ddlTipoCaracterLegalFirSH.SelectedValue, out id_tipocaracter);

            if (id_tipocaracter > 0)
            {
                TiposDeCaracterLegalBL tiposDeCaracterLegalBL = new TiposDeCaracterLegalBL();

                bool muestracargo_tipocaracter = tiposDeCaracterLegalBL.Single(id_tipocaracter).MuestraCargoTipoCaracter;
                if (muestracargo_tipocaracter)
                    pnlCargoFirmanteSH.Style["display"] = "block";
                else
                {
                    pnlCargoFirmanteSH.Style["display"] = "none";
                    txtCargoFirSH.Text = "";
                }
            }
            else
            {
                pnlCargoFirmanteSH.Style["display"] = "none";
                txtCargoFirSH.Text = "";
            }

        }

        protected void btnAceptarTitSH_Click(object sender, EventArgs e)
        {

            DataTable dtTitularesSH = dtTitularesSHCargados();
            DataTable dtFirmantesSH = dtFirmantesSHCargados();
            string firmanteDe = txtApellidosTitSH.Text.Trim() + " " + txtNombresTitSH.Text.Trim();

            if (hid_rowindex_titSH.Value.Length == 0)
            {
                Guid rowid_titular = Guid.NewGuid();

                dtTitularesSH.Rows.Add(rowid_titular, txtApellidosTitSH.Text.Trim(), txtNombresTitSH.Text.Trim(), ddlTipoDocumentoTitSH.SelectedItem.Text.Trim(), txtNroDocumentoTitSH.Text.Trim(),
                            int.Parse(ddlTipoDocumentoTitSH.SelectedValue), txtEmailTitSH.Text.Trim());


                if (optMismaPersonaSH.Checked)
                {

                    dtFirmantesSH.Rows.Add(firmanteDe, txtApellidosTitSH.Text.Trim(), txtNombresTitSH.Text.Trim(), ddlTipoDocumentoTitSH.SelectedItem.Text.Trim(), txtNroDocumentoTitSH.Text.Trim(),
                                "Titular", int.Parse(ddlTipoDocumentoTitSH.SelectedValue), 1, string.Empty, txtEmailTitSH.Text.Trim(), Guid.NewGuid(), rowid_titular, optMismaPersonaSH.Checked);
                }
                else
                {
                    dtFirmantesSH.Rows.Add(firmanteDe, txtApellidosFirSH.Text.Trim(), txtNombresFirSH.Text.Trim(), ddlTipoDocumentoFirSH.SelectedItem.Text.Trim(), txtNroDocumentoFirSH.Text.Trim(),
                                ddlTipoCaracterLegalFirSH.SelectedItem.Text, int.Parse(ddlTipoDocumentoFirSH.SelectedValue),
                                int.Parse(ddlTipoCaracterLegalFirSH.SelectedValue), txtCargoFirSH.Text.Trim(), txtEmailFirSH.Text.Trim(), Guid.NewGuid(), rowid_titular, optMismaPersonaSH.Checked);
                }

            }
            else
            {
                int rowindex = int.Parse(hid_rowindex_titSH.Value);
                DataRow drTitularesSH = dtTitularesSH.Rows[rowindex];
                DataRow drFirmantesSH = null;

                foreach (DataRow rowFirmante in dtFirmantesSH.Rows)
                {
                    if ((Guid)rowFirmante["rowid_titular"] == (Guid)drTitularesSH["rowid"])
                    {
                        drFirmantesSH = rowFirmante;
                        break;
                    }

                }

                dtTitularesSH.Rows[rowindex]["Apellidos"] = txtApellidosTitSH.Text.Trim();
                dtTitularesSH.Rows[rowindex]["Nombres"] = txtNombresTitSH.Text.Trim();
                dtTitularesSH.Rows[rowindex]["TipoDoc"] = ddlTipoDocumentoTitSH.SelectedItem.Text;
                dtTitularesSH.Rows[rowindex]["NroDoc"] = txtNroDocumentoTitSH.Text.Trim();
                dtTitularesSH.Rows[rowindex]["id_tipodoc_personal"] = int.Parse(ddlTipoDocumentoTitSH.SelectedValue);
                dtTitularesSH.Rows[rowindex]["email"] = txtEmailTitSH.Text.Trim();



                if (drFirmantesSH != null && optOtraPersonaSH.Checked)
                {
                    drFirmantesSH["Apellidos"] = txtApellidosFirSH.Text.Trim();
                    drFirmantesSH["Nombres"] = txtNombresFirSH.Text.Trim();
                    drFirmantesSH["NroDoc"] = txtNroDocumentoFirSH.Text.Trim();
                    drFirmantesSH["id_tipodoc_personal"] = ddlTipoDocumentoFirSH.SelectedValue;
                    drFirmantesSH["email"] = txtEmailFirSH.Text.Trim();
                    drFirmantesSH["nom_tipocaracter"] = ddlTipoCaracterLegalFirSH.SelectedItem.Text;
                    drFirmantesSH["id_tipocaracter"] = ddlTipoCaracterLegalFirSH.SelectedValue;
                    drFirmantesSH["cargo_firmante"] = txtCargoFirSH.Text.Trim();
                    drFirmantesSH["misma_persona"] = false;
                }
                else
                {
                    drFirmantesSH["Apellidos"] = txtApellidosTitSH.Text.Trim();
                    drFirmantesSH["Nombres"] = txtNombresTitSH.Text.Trim();
                    drFirmantesSH["NroDoc"] = txtNroDocumentoTitSH.Text.Trim();
                    drFirmantesSH["id_tipodoc_personal"] = ddlTipoDocumentoTitSH.SelectedValue;
                    drFirmantesSH["email"] = txtEmailTitSH.Text.Trim();
                    drFirmantesSH["nom_tipocaracter"] = "Titular";
                    drFirmantesSH["id_tipocaracter"] = "1";
                    drFirmantesSH["cargo_firmante"] = string.Empty;
                    drFirmantesSH["misma_persona"] = true;
                }
            }

            grdTitularesSH.DataSource = dtTitularesSH;
            grdTitularesSH.DataBind();

            grdFirmantesSH.DataSource = dtFirmantesSH;
            grdFirmantesSH.DataBind();

            updgrillaTitularesSH.Update();
            this.EjecutarScript(updBotonesIngresarTitularesSH, "hidefrmAgregarTitularesSH();");
        }

        private DataTable dtTitularesSHCargados()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("rowid", typeof(Guid));
            dt.Columns.Add("Apellidos", typeof(string));
            dt.Columns.Add("Nombres", typeof(string));
            dt.Columns.Add("TipoDoc", typeof(string));
            dt.Columns.Add("NroDoc", typeof(string));
            dt.Columns.Add("id_tipodoc_personal", typeof(int));
            dt.Columns.Add("email", typeof(string));

            foreach (GridViewRow row in grdTitularesSH.Rows)
            {
                DataRow datarw;
                datarw = dt.NewRow();

                HiddenField hid_id_tipodoc_grdTitularesSH = (HiddenField)grdTitularesSH.Rows[row.RowIndex].Cells[0].FindControl("hid_id_tipodoc_grdTitularesSH");
                HiddenField hid_rowid_grdTitularesSH = (HiddenField)grdTitularesSH.Rows[row.RowIndex].Cells[6].FindControl("hid_rowid_grdTitularesSH");

                datarw[0] = Guid.Parse(hid_rowid_grdTitularesSH.Value);
                datarw[1] = HttpUtility.HtmlDecode(row.Cells[1].Text);
                datarw[2] = HttpUtility.HtmlDecode(row.Cells[2].Text);
                datarw[3] = HttpUtility.HtmlDecode(row.Cells[3].Text);
                datarw[4] = HttpUtility.HtmlDecode(row.Cells[4].Text);
                datarw[5] = int.Parse(hid_id_tipodoc_grdTitularesSH.Value);
                datarw[6] = HttpUtility.HtmlDecode(row.Cells[5].Text);

                dt.Rows.Add(datarw);

            }

            return dt;
        }

        private DataTable dtFirmantesSHCargados()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FirmanteDe", typeof(string));
            dt.Columns.Add("Apellidos", typeof(string));
            dt.Columns.Add("Nombres", typeof(string));
            dt.Columns.Add("TipoDoc", typeof(string));
            dt.Columns.Add("NroDoc", typeof(string));
            dt.Columns.Add("nom_tipocaracter", typeof(string));
            dt.Columns.Add("id_tipodoc_personal", typeof(int));
            dt.Columns.Add("id_tipocaracter", typeof(int));
            dt.Columns.Add("cargo_firmante", typeof(string));
            dt.Columns.Add("email", typeof(string));
            dt.Columns.Add("rowid", typeof(Guid));
            dt.Columns.Add("rowid_titular", typeof(Guid));
            dt.Columns.Add("misma_persona", typeof(bool));


            foreach (GridViewRow row in grdFirmantesSH.Rows)
            {
                DataRow datarw;
                datarw = dt.NewRow();

                HiddenField hid_id_tipodoc_grdFirmantesSH = (HiddenField)grdFirmantesSH.Rows[row.RowIndex].FindControl("hid_id_tipodoc_grdFirmantesSH");
                HiddenField hid_id_caracter_grdFirmantesSH = (HiddenField)grdFirmantesSH.Rows[row.RowIndex].FindControl("hid_id_caracter_grdFirmantesSH");
                HiddenField hid_rowid_grdFirmantesSH = (HiddenField)grdFirmantesSH.Rows[row.RowIndex].FindControl("hid_rowid_grdFirmantesSH");
                HiddenField hid_rowid_titularSH_grdFirmantesSH = (HiddenField)grdFirmantesSH.Rows[row.RowIndex].FindControl("hid_rowid_titularSH_grdFirmantesSH");
                HiddenField hid_misma_persona_grdFirmantesSH = (HiddenField)grdFirmantesSH.Rows[row.RowIndex].FindControl("hid_misma_persona_grdFirmantesSH");


                datarw[0] = HttpUtility.HtmlDecode(row.Cells[0].Text);
                datarw[1] = HttpUtility.HtmlDecode(row.Cells[1].Text);
                datarw[2] = HttpUtility.HtmlDecode(row.Cells[2].Text);
                datarw[3] = HttpUtility.HtmlDecode(row.Cells[3].Text);
                datarw[4] = HttpUtility.HtmlDecode(row.Cells[4].Text);
                datarw[5] = HttpUtility.HtmlDecode(row.Cells[5].Text);
                datarw[6] = int.Parse(hid_id_tipodoc_grdFirmantesSH.Value);
                datarw[7] = int.Parse(hid_id_caracter_grdFirmantesSH.Value);
                datarw[8] = HttpUtility.HtmlDecode(row.Cells[6].Text).Trim();
                datarw[9] = HttpUtility.HtmlDecode(row.Cells[7].Text).Trim();
                datarw[10] = Guid.Parse(hid_rowid_grdFirmantesSH.Value);
                datarw[11] = Guid.Parse(hid_rowid_titularSH_grdFirmantesSH.Value);
                datarw[12] = Convert.ToBoolean(hid_misma_persona_grdFirmantesSH.Value);
                dt.Rows.Add(datarw);

            }

            return dt;
        }

        protected void btnEditarTitularSH_Click(object sender, EventArgs e)
        {

            LimpiarDatosABMTitularesSH();

            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.Parent.Parent;
            DataTable dtTitularesSH = dtTitularesSHCargados();
            DataTable dtFirmantesSH = dtFirmantesSHCargados();

            DataRow drTitularesSH = dtTitularesSH.Rows[row.RowIndex];
            DataRow drFirmantesSH = null;

            foreach (DataRow rowFirmante in dtFirmantesSH.Rows)
            {
                if ((Guid)rowFirmante["rowid_titular"] == (Guid)drTitularesSH["rowid"])
                {
                    drFirmantesSH = rowFirmante;
                    break;
                }

            }

            txtApellidosTitSH.Text = drTitularesSH["Apellidos"].ToString();
            txtNombresTitSH.Text = drTitularesSH["Nombres"].ToString();
            txtNroDocumentoTitSH.Text = drTitularesSH["NroDoc"].ToString();
            ddlTipoDocumentoTitSH.SelectedValue = drTitularesSH["id_tipodoc_personal"].ToString();
            txtEmailTitSH.Text = drTitularesSH["email"].ToString();


            if (drFirmantesSH != null)
            {
                bool misma_persona = Convert.ToBoolean(drFirmantesSH["misma_persona"]);
                // Primero se deben limpir y luego setear los valores de checked
                optMismaPersonaSH.Checked = false;
                optOtraPersonaSH.Checked = false;
                optMismaPersonaSH.Checked = misma_persona;
                optOtraPersonaSH.Checked = !misma_persona;
                if (!misma_persona)
                {
                    txtApellidosFirSH.Text = drFirmantesSH["Apellidos"].ToString();
                    txtNombresFirSH.Text = drFirmantesSH["Nombres"].ToString();
                    txtNroDocumentoFirSH.Text = drFirmantesSH["NroDoc"].ToString();
                    ddlTipoDocumentoFirSH.SelectedValue = drFirmantesSH["id_tipodoc_personal"].ToString();
                    txtEmailFirSH.Text = drFirmantesSH["email"].ToString();
                    ddlTipoCaracterLegalFirSH.SelectedValue = drFirmantesSH["id_tipocaracter"].ToString();
                    txtCargoFirSH.Text = drFirmantesSH["cargo_firmante"].ToString();
                    pnlFirSH.Style["display"] = "block";

                }

            }

            hid_rowindex_titSH.Value = row.RowIndex.ToString();
            updABMTitularesSH.Update();

            this.EjecutarScript(updgrillaTitularesSH, "showfrmAgregarTitularesSH();");
        }

        protected void btnEliminarTitularSH_Click(object sender, EventArgs e)
        {

            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.Parent.Parent;

            DataTable dtTitularesSH = dtTitularesSHCargados();
            DataTable dtFirmantesSH = dtFirmantesSHCargados();

            Guid rowid_titular = (Guid)dtTitularesSH.Rows[row.RowIndex]["rowid"];

            dtTitularesSH.Rows.Remove(dtTitularesSH.Rows[row.RowIndex]);

            foreach (DataRow rowFirmante in dtFirmantesSH.Rows)
            {
                if ((Guid)rowFirmante["rowid_titular"] == rowid_titular)
                {
                    dtFirmantesSH.Rows.Remove(rowFirmante);
                    break;
                }
            }

            grdTitularesSH.DataSource = dtTitularesSH;
            grdTitularesSH.DataBind();

            grdFirmantesSH.DataSource = dtFirmantesSH;
            grdFirmantesSH.DataBind();

            updgrillaTitularesSH.Update();

        }
    }
}