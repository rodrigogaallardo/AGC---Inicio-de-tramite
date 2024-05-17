using BusinesLayer.Implementation;
using DataTransferObject;
using Org.BouncyCastle.Asn1.X509.Qualified;
using SSIT.App_Components;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Solicitud.Transferencia
{
    public partial class Titulares : SecurePage
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
            }

        }

        private int IdSolicitud
        {
            get
            {
                int ret = 0;
                int.TryParse(hid_id_solicitud.Value, out ret);
                return ret;
            }
            set
            {
                hid_id_solicitud.Value = value.ToString();
            }

        }

        private void ComprobarSolicitud()
        {
            if (Page.RouteData.Values["id_solicitud"] != null)
            {
                this.IdSolicitud = Convert.ToInt32(Page.RouteData.Values["id_solicitud"].ToString());
                TransferenciasSolicitudesBL transferenciasSolicitudesBL = new TransferenciasSolicitudesBL();
                var enc = transferenciasSolicitudesBL.Single(IdSolicitud);
                if (enc != null)
                {
                    /*Falta el userID y hacer overload de getuserid con el tipo de tramite*/
                    Guid userid_solicitud = (Guid)Membership.GetUser().ProviderUserKey;

                    if (userid_solicitud != enc.CreateUser)
                        Server.Transfer("~/Errores/Error3002.aspx");
                }
                else
                    Server.Transfer("~/Errores/Error3004.aspx");
            }
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
                CargarDatosTitulares(IdSolicitud);
                CargarDatosTitularesANT(IdSolicitud);

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

        public void CargarDatosTitulares(int IdSolicitud)
        {

            TitularesBL titularesBL = new TitularesBL();
            FirmantesBL firmantesBL = new FirmantesBL();

            var lstTitulares = titularesBL.GetTitularesTransferencias(IdSolicitud).ToList();

            var lstFirmantes = firmantesBL.GetFirmantesTransferencias(IdSolicitud).ToList();


            grdTitularesHab.DataSource = lstTitulares;
            grdTitularesHab.DataBind();

            grdTitularesTra.DataSource = lstFirmantes;
            grdTitularesTra.DataBind();

            updGrillaTitulares.Update();
            updGrillaFirmantes.Update();
        }

        public void CargarDatosTitularesANT(int IdSolicitud)
        {

            TitularesBL titularesBL = new TitularesBL();
            FirmantesBL firmantesBL = new FirmantesBL();
            TransferenciasSolicitudesBL transferenciasSolicitudesBL = new TransferenciasSolicitudesBL();
            var sol = transferenciasSolicitudesBL.Single(IdSolicitud);


            var lstTitulares = titularesBL.GetTitularesTransferenciasANT(IdSolicitud).ToList();

            var lstFirmantes = firmantesBL.GetFirmantesTransferenciasANT(IdSolicitud).ToList();

            grdTitularesHabANT.DataSource = lstTitulares;
            grdTitularesHabANT.DataBind();

            grdTitularesTraANT.DataSource = lstFirmantes;
            grdTitularesTraANT.DataBind();

            if (sol.idSolicitudRef != null)
            {
                btnShowAgregarPFANT.Visible = false;
                btnShowAgregarPJANT.Visible = false;
                updShowAgregarPersonasANT.Update();
            }

            updGrillaTitularesANT.Update();
            updGrillaFirmantesANT.Update();

        }

        protected void grdTitularesHabANT_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            TransferenciasSolicitudesBL transferenciasSolicitudesBL = new TransferenciasSolicitudesBL();
            var sol = transferenciasSolicitudesBL.Single(IdSolicitud);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (sol.idSolicitudRef != null)
                {
                    LinkButton lkbeditar = (LinkButton)e.Row.Cells[4].FindControl("btnEditarTitularAnt");
                    lkbeditar.Visible = false;
                    LinkButton lkbeliminar = (LinkButton)e.Row.Cells[4].FindControl("btnEliminarTitular");
                    lkbeliminar.Visible = false;
                }
            }
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
            var lstProvincias = provinciaBL.GetProvincias();

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
            // var lstTipoDocPersonal = tipoDocumentoPersonalBL.GetAll();
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
            int[] disponibilidadPF = new int[] { 1 };
            int[] disponibilidadPJ = new int[] { 2 };

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

            TransferenciasFirmantesPersonasJuridicasBL encomiendaFirmantesPersonasJuridicasBL = new TransferenciasFirmantesPersonasJuridicasBL();
            string[] lstCargosFirmantes = encomiendaFirmantesPersonasJuridicasBL.GetCargoFirmantesPersonasJuridicas();

            hid_CargosFirPJ.Value = string.Join(",", lstCargosFirmantes).ToUpper();
            hid_CargosFirSH.Value = string.Join(",", lstCargosFirmantes).ToUpper();

        }
        private void CargarTiposDeIngresosBrutos()
        {
            TiposDeIngresosBrutosBL tiposDeIngresosBrutosBL = new TiposDeIngresosBrutosBL();
            var lstTipoIngresosBrutos = tiposDeIngresosBrutosBL.GetIngresosBrutos();

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
            txtCPPF.Text = "";
            txtTelefonoPF.Text = "";
            txtTelefonoMovilPF.Text = "";
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

                if (tiposoc != null && (id_tiposociedad == (int)Constantes.TipoSociedad.Sociedad_Hecho ||
                    id_tiposociedad == (int)Constantes.TipoSociedad.Sociedad_no_constituidas_regularmente))
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
            upd_txtIngresosBrutosPJ.Update();
            ScriptManager.RegisterStartupScript(upd_ddlTipoIngresosBrutosPF, upd_ddlTipoIngresosBrutosPF.GetType(), " setFocus", "setFocus();", true);
        }

        protected void ddlProvinciaPJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarLocalidades(ddlProvinciaPJ, ddlLocalidadPJ);
        }

        private bool ValidarDatosPantallaPF()
        {
            bool ret = true;
            bool existeTitular = true;
            int id_personafisica = 0;
            int.TryParse(hid_id_titular_pf.Value, out id_personafisica);
            int TipoTitular = 0;
            int.TryParse(hid_tipo_titular.Value, out TipoTitular);

            if (TipoTitular == 0)
            {
                //Valida si existe una persona física ya ingresada con el mismo numero de CUIT.
                TransferenciasTitularesPersonasFisicasBL encTitPFBL = new TransferenciasTitularesPersonasFisicasBL();
                existeTitular = encTitPFBL.GetByIdTransferenciasCuitIdPersonaFisica(IdSolicitud, txtCuitPF.Text.Trim(), id_personafisica).Any();
            }
            else
            {
                TransferenciasTitularesSolicitudPersonasFisicasBL encTitPFBL = new TransferenciasTitularesSolicitudPersonasFisicasBL();
                existeTitular = encTitPFBL.GetByIdSolicitudCuitIdPersonaFisica(IdSolicitud, txtCuitPF.Text.Trim(), id_personafisica).Any();
            }
            if (existeTitular)
            {
                ValExiste_TitularPF.Style["display"] = "inline-block";
                ret = false;
            }

            return ret;
        }

        private bool ValidarDatosPantallaPJ()
        {
            bool ret = true;
            bool existeTitular = true;
            int id_personajuridica = 0;
            int.TryParse(hid_id_titular_pj.Value, out id_personajuridica);
            int TipoTitular = 0;
            int.TryParse(hid_tipo_titular.Value, out TipoTitular);
            if (TipoTitular == 0)
            {
                TransferenciasTitularesPersonasJuridicasBL encTitPJBL = new TransferenciasTitularesPersonasJuridicasBL();
                //Valida si existe una persona física ya ingresada con el mismo numero de CUIT.
                existeTitular = encTitPJBL.GetByIdTransferenciasCuitIdPersonaJuridica(IdSolicitud, txtCuitPJ.Text.Trim(), id_personajuridica).Any();
            }
            else
            {
                TransferenciasTitularesSolicitudPersonasJuridicasBL encTitPJBL = new TransferenciasTitularesSolicitudPersonasJuridicasBL();
                existeTitular = encTitPJBL.GetByIdSolicitudCuitIdPersonaJuridica(IdSolicitud, txtCuitPJ.Text.Trim(), id_personajuridica).Any();
            }
            if (existeTitular)
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
                    int id_Localidad = 0;
                    string Codigo_Postal = txtCPPF.Text.Trim();
                    string TelefonoArea = txtTelefonoPF.Text.Trim();
                    string TelefonoMovil = txtTelefonoMovilPF.Text.Trim();
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
                        int TipoTitular = 0;
                        int.TryParse(hid_tipo_titular.Value, out TipoTitular);
                        if (TipoTitular == 0)
                        {
                            TransferenciasTitularesPersonasFisicasBL encTitPFBL = new TransferenciasTitularesPersonasFisicasBL();
                            TransferenciasTitularesPersonasFisicasDTO encTitPerFisDTO = new TransferenciasTitularesPersonasFisicasDTO();

                            encTitPerFisDTO.IdPersonaFisica = id_personafisica;
                            encTitPerFisDTO.IdSolicitud = IdSolicitud;
                            encTitPerFisDTO.Apellido = Apellido;
                            encTitPerFisDTO.Nombres = Nombres;
                            encTitPerFisDTO.IdTipodocPersonal = id_tipodoc_personal;
                            encTitPerFisDTO.NumeroDocumento = Nro_Documento;
                            encTitPerFisDTO.Cuit = Cuit;
                            encTitPerFisDTO.IdTipoiibb = id_tipoiibb;
                            encTitPerFisDTO.IngresosBrutos = Ingresos_Brutos;
                            encTitPerFisDTO.Calle = Calle;
                            encTitPerFisDTO.NumeroPuerta = Nro_Puerta;
                            encTitPerFisDTO.Piso = Piso;
                            encTitPerFisDTO.Depto = Depto;
                            encTitPerFisDTO.IdLocalidad = id_Localidad;
                            encTitPerFisDTO.CodigoPostal = Codigo_Postal;
                            encTitPerFisDTO.Email = Email.ToLower();
                            encTitPerFisDTO.MismoFirmante = MismoFirmante;
                            encTitPerFisDTO.Telefono = TelefonoArea;
                            encTitPerFisDTO.Celular = TelefonoMovil;
                            encTitPerFisDTO.CreateUser = userid;
                            encTitPerFisDTO.CreateDate = DateTime.Now;

                            encTitPerFisDTO.DtoFirmantes = new TransferenciasFirmantesPersonasFisicasDTO();
                            encTitPerFisDTO.DtoFirmantes.IdSolicitud = IdSolicitud;

                            if (!MismoFirmante)
                            {
                                int.TryParse(ddlTipoDocumentoFirPF.SelectedValue, out id_tipodoc_personal);
                                int.TryParse(ddlTipoCaracterLegalFirPF.SelectedValue, out TipoCaracterLegalTitular);
                                encTitPerFisDTO.DtoFirmantes.Apellido = txtApellidoFirPF.Text.Trim();
                                encTitPerFisDTO.DtoFirmantes.Nombres = txtNombresFirPF.Text.Trim();
                                encTitPerFisDTO.DtoFirmantes.IdTipoDocumentoPersonal = id_tipodoc_personal;
                                encTitPerFisDTO.DtoFirmantes.NumeroDocumento = txtNroDocumentoFirPF.Text.Trim();
                                encTitPerFisDTO.DtoFirmantes.IdTipoCaracter = TipoCaracterLegalTitular;
                                encTitPerFisDTO.DtoFirmantes.Cuit = txtCuitFirPF.Text.Trim();
                            }
                            else
                            {
                                encTitPerFisDTO.DtoFirmantes.Apellido = Apellido;
                                encTitPerFisDTO.DtoFirmantes.Nombres = Nombres;
                                encTitPerFisDTO.DtoFirmantes.IdTipoDocumentoPersonal = id_tipodoc_personal;
                                encTitPerFisDTO.DtoFirmantes.NumeroDocumento = Nro_Documento;
                                encTitPerFisDTO.DtoFirmantes.IdTipoCaracter = TipoCaracterLegalTitular;
                                encTitPerFisDTO.DtoFirmantes.Cuit = Cuit;
                            }

                            encTitPFBL.Insert(encTitPerFisDTO);
                        }
                        else
                        {
                            TransferenciasTitularesSolicitudPersonasFisicasBL encTitPFBL = new TransferenciasTitularesSolicitudPersonasFisicasBL();
                            TransferenciasTitularesSolicitudPersonasFisicasDTO encTitPerFisDTO = new TransferenciasTitularesSolicitudPersonasFisicasDTO();

                            encTitPerFisDTO.IdPersonaFisica = id_personafisica;
                            encTitPerFisDTO.IdSolicitud = IdSolicitud;
                            encTitPerFisDTO.Apellido = Apellido;
                            encTitPerFisDTO.Nombres = Nombres;
                            encTitPerFisDTO.IdTipoDocumentoPersonal = id_tipodoc_personal;
                            encTitPerFisDTO.NumeroDocumento = Nro_Documento;
                            encTitPerFisDTO.CUIT = Cuit;
                            encTitPerFisDTO.IdTipoiibb = id_tipoiibb;
                            encTitPerFisDTO.IngresosBrutos = Ingresos_Brutos;
                            encTitPerFisDTO.Calle = Calle;
                            encTitPerFisDTO.NumeroPuerta = Nro_Puerta;
                            encTitPerFisDTO.Piso = Piso;
                            encTitPerFisDTO.Depto = Depto;
                            encTitPerFisDTO.IdLocalidad = id_Localidad;
                            encTitPerFisDTO.CodigoPostal = Codigo_Postal;
                            encTitPerFisDTO.Email = Email.ToLower();
                            encTitPerFisDTO.MismoFirmante = MismoFirmante;
                            encTitPerFisDTO.Telefono = TelefonoArea;
                            encTitPerFisDTO.TelefonoMovil = TelefonoMovil;
                            encTitPerFisDTO.CreateUser = userid;
                            encTitPerFisDTO.CreateDate = DateTime.Now;

                            encTitPerFisDTO.DtoFirmantes = new TransferenciasFirmantesSolicitudPersonasFisicasDTO();

                            encTitPerFisDTO.DtoFirmantes.id_solicitud = IdSolicitud;

                            if (!MismoFirmante)
                            {
                                int.TryParse(ddlTipoDocumentoFirPF.SelectedValue, out id_tipodoc_personal);
                                int.TryParse(ddlTipoCaracterLegalFirPF.SelectedValue, out TipoCaracterLegalTitular);
                                encTitPerFisDTO.DtoFirmantes.Apellido = txtApellidoFirPF.Text.Trim();
                                encTitPerFisDTO.DtoFirmantes.Nombres = txtNombresFirPF.Text.Trim();
                                encTitPerFisDTO.DtoFirmantes.id_tipodoc_personal = id_tipodoc_personal;
                                encTitPerFisDTO.DtoFirmantes.Nro_Documento = txtNroDocumentoFirPF.Text.Trim();
                                encTitPerFisDTO.DtoFirmantes.id_tipocaracter = TipoCaracterLegalTitular;
                            }
                            else
                            {
                                encTitPerFisDTO.DtoFirmantes.Apellido = Apellido;
                                encTitPerFisDTO.DtoFirmantes.Nombres = Nombres;
                                encTitPerFisDTO.DtoFirmantes.id_tipodoc_personal = id_tipodoc_personal;
                                encTitPerFisDTO.DtoFirmantes.Nro_Documento = Nro_Documento;
                                encTitPerFisDTO.DtoFirmantes.id_tipocaracter = TipoCaracterLegalTitular;
                            }
                            encTitPFBL.Insert(encTitPerFisDTO);
                        }
                    }
                    catch
                    {
                        throw;
                    }

                    CargarDatosTitulares(IdSolicitud);
                    CargarDatosTitularesANT(IdSolicitud);
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
                int TipoTitular = 0;
                int.TryParse(hid_tipo_titular.Value, out TipoTitular);

                if (TipoTitular == 0)
                {

                    if (TipoPersona.Equals("PF"))
                    {
                        TransferenciasFirmantesPersonasFisicasBL encFirPerFisBL = new TransferenciasFirmantesPersonasFisicasBL();
                        TransferenciasTitularesPersonasFisicasBL encTitPerFisBL = new TransferenciasTitularesPersonasFisicasBL();

                        TransferenciasFirmantesPersonasFisicasDTO encFirPerFisDTO = encFirPerFisBL.GetByFKIdSolicitudIdPersonaFisica(IdSolicitud, id_persona).FirstOrDefault();
                        TransferenciasTitularesPersonasFisicasDTO encTitPerFisDTO = encTitPerFisBL.GetByFKIdSolicitudIdPersonaFisica(IdSolicitud, id_persona).FirstOrDefault();

                        if (encFirPerFisDTO != null)
                            encFirPerFisBL.Delete(encFirPerFisDTO);

                        if (encTitPerFisDTO != null)
                            encTitPerFisBL.Delete(encTitPerFisDTO);
                    }
                    else
                    {
                        TransferenciasFirmantesPersonasJuridicasBL encFirPerJurBL = new TransferenciasFirmantesPersonasJuridicasBL();
                        TransferenciasTitularesPersonasJuridicasBL encTitPerJurBL = new TransferenciasTitularesPersonasJuridicasBL();
                        TransferenciasTitularesPersonasJuridicasPersonasFisicasBL encTitPerJurPerFisBL = new TransferenciasTitularesPersonasJuridicasPersonasFisicasBL();

                        List<TransferenciasFirmantesPersonasJuridicasDTO> encFirPerJurDTO = encFirPerJurBL.GetByFKIdSolicitudIdPersonaJuridica(IdSolicitud, id_persona).ToList();
                        List<TransferenciasTitularesPersonasJuridicasDTO> encTitPerJurDTO = encTitPerJurBL.GetByIdSolicitudIdPersonaJuridica(IdSolicitud, id_persona).ToList();
                        List<TransferenciasTitularesPersonasJuridicasPersonasFisicasDTO> encTitPerJurPerFisDTO = encTitPerJurPerFisBL.GetByIdSolicitudIdPersonaJuridica(IdSolicitud, id_persona).ToList();

                        if (encTitPerJurPerFisDTO.Count > 0)
                        {
                            foreach (var item in encTitPerJurPerFisDTO)
                                encTitPerJurPerFisBL.Delete(item);
                        }
                        if (encFirPerJurDTO.Count > 0)
                        {
                            foreach (var item in encFirPerJurDTO)
                                encFirPerJurBL.Delete(item);
                        }
                        if (encTitPerJurDTO.Count > 0)
                        {
                            foreach (var item in encTitPerJurDTO)
                                encTitPerJurBL.Delete(item);
                        }
                    }
                }
                else
                {
                    if (TipoPersona.Equals("PF"))
                    {
                        TransferenciasFirmantesSolicitudPersonasFisicasBL encFirPerFisBL = new TransferenciasFirmantesSolicitudPersonasFisicasBL();
                        TransferenciasTitularesSolicitudPersonasFisicasBL encTitPerFisBL = new TransferenciasTitularesSolicitudPersonasFisicasBL();

                        List<TransferenciasFirmantesSolicitudPersonasFisicasDTO> encFirPerFisDTO = encFirPerFisBL.GetByFKIdSolicitudIdPersonaFisica(IdSolicitud, id_persona).ToList();
                        List<TransferenciasTitularesSolicitudPersonasFisicasDTO> encTitPerFisDTO = encTitPerFisBL.GetByFKIdSolicitudIdPersonaFisica(IdSolicitud, id_persona).ToList();

                        if (encFirPerFisDTO.Count > 0)
                        {
                            foreach (var item in encFirPerFisDTO)
                                encFirPerFisBL.Delete(item);
                        }
                        if (encTitPerFisDTO.Count > 0)
                        {
                            foreach (var item in encTitPerFisDTO)
                                encTitPerFisBL.Delete(item);
                        }
                    }
                    else
                    {
                        TransferenciasFirmantesSolicitudPersonasJuridicasBL encFirPerJurBL = new TransferenciasFirmantesSolicitudPersonasJuridicasBL();
                        TransferenciasTitularesSolicitudPersonasJuridicasBL encTitPerJurBL = new TransferenciasTitularesSolicitudPersonasJuridicasBL();
                        TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasBL encTitPerJurPerFisBL = new TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasBL();

                        List<TransferenciasFirmantesSolicitudPersonasJuridicasDTO> encFirPerJurDTO = encFirPerJurBL.GetByFKIdSolicitudIdPersonaJuridica(IdSolicitud, id_persona).ToList();
                        List<TransferenciasTitularesSolicitudPersonasJuridicasDTO> encTitPerJurDTO = encTitPerJurBL.GetByIdSolicitudIdPersonaJuridica(IdSolicitud, id_persona).ToList();
                        List<TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasDTO> encTitPerJurPerFisDTO = encTitPerJurPerFisBL.GetByIdSolicitudIdPersonaJuridica(IdSolicitud, id_persona).ToList();

                        if (encTitPerJurPerFisDTO.Count > 0)
                        {
                            foreach (var item in encTitPerJurPerFisDTO)
                                encTitPerJurPerFisBL.Delete(item);
                        }
                        if (encFirPerJurDTO.Count > 0)
                        {
                            foreach (var item in encFirPerJurDTO)
                                encFirPerJurBL.Delete(item);
                        }
                        if (encTitPerJurDTO.Count > 0)
                        {
                            foreach (var item in encTitPerJurDTO)
                                encTitPerJurBL.Delete(item);
                        }
                    }
                }
                CargarDatosTitulares(IdSolicitud);
                CargarDatosTitularesANT(IdSolicitud);
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
            hid_tipo_titular.Value = "0"; //titulares NUEVOS
            LimpiarControlesABMPF();
            updAgregarPersonaFisica.Update();
            this.EjecutarScript(updShowAgregarPersonas, "showfrmAgregarPersonaFisica();");

        }

        protected void btnShowAgregarPFANT_Click(object sender, EventArgs e)
        {
            hid_id_titular_pf.Value = "0";
            hid_tipo_titular.Value = "1"; //titulares ANT
            LimpiarControlesABMPF();
            updAgregarPersonaFisica.Update();
            this.EjecutarScript(updShowAgregarPersonasANT, "showfrmAgregarPersonaFisica();");

        }

        protected void btnEditarTitular_Click(object sender, EventArgs e)
        {
            LinkButton btnEditarTitular = (LinkButton)sender;

            string TipoPersona = btnEditarTitular.CommandName;
            int id_persona = int.Parse(btnEditarTitular.CommandArgument);

            int TipoTitular = 0;
            int.TryParse(hid_tipo_titular.Value, out TipoTitular);

            if (TipoPersona.Equals("PF"))
            {
                LimpiarControlesABMPF();
                hid_id_titular_pf.Value = id_persona.ToString();
                if (TipoTitular == 0)
                {
                    CargarDatosTitularPF(id_persona);
                    updAgregarPersonaFisica.Update();
                    this.EjecutarScript(updGrillaTitulares, "showfrmAgregarPersonaFisica();");
                }
                else
                {
                    CargarDatosTitularPFANT(id_persona);
                    updAgregarPersonaFisica.Update();
                    this.EjecutarScript(updGrillaTitularesANT, "showfrmAgregarPersonaFisica();");
                }
            }

            if (TipoPersona.Equals("PJ"))
            {
                LimpiarControlesABMPJ();
                hid_id_titular_pj.Value = id_persona.ToString();
                if (TipoTitular == 0)
                {
                    CargarDatosTitularPJ(id_persona);
                    updAgregarPersonaJuridica.Update();
                    this.EjecutarScript(updGrillaTitulares, "showfrmAgregarPersonaJuridica();");
                }
                else
                {
                    CargarDatosTitularPJANT(id_persona);
                    updAgregarPersonaJuridica.Update();
                    this.EjecutarScript(updGrillaTitularesANT, "showfrmAgregarPersonaJuridica();");
                }
            }

        }

        private void CargarDatosTitularPJ(int id_personajuridica)
        {
            TransferenciasTitularesPersonasJuridicasBL encomiendaTitularesPersonasJuridicasBL = new TransferenciasTitularesPersonasJuridicasBL();
            LocalidadBL localidadBL = new LocalidadBL();
            FirmantesBL firmantesBL = new FirmantesBL();

            var pj = encomiendaTitularesPersonasJuridicasBL.Single(id_personajuridica);

            if (pj != null)
            {
                hid_id_titular_pj.Value = id_personajuridica.ToString();

                ddlTipoSociedadPJ.SelectedValue = pj.IdTipoSociedad.ToString();
                txtRazonSocialPJ.Text = pj.RazonSocial;
                txtCuitPJ.Text = pj.Cuit;
                ddlTipoIngresosBrutosPJ.SelectedValue = pj.IdTipoiibb.ToString();
                txtIngresosBrutosPJ.Text = pj.NumeroIibb;
                txtCallePJ.Text = pj.Calle;
                txtNroPuertaPJ.Text = pj.NumeroPuerta.ToString();
                txtPisoPJ.Text = pj.Piso;
                txtDeptoPJ.Text = pj.Depto;
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
                    var lstTitularesSH = titularesBL.GetTitularesSHTransferencias(id_personajuridica).ToList();

                    grdTitularesSH.DataSource = lstTitularesSH;
                    grdTitularesSH.DataBind();

                    DataTable dtFirmantesSH = dtFirmantesSHCargados();
                    dtFirmantesSH.Clear();
                    foreach (var titularsh in lstTitularesSH)
                    {
                        int id_firmante_pj = titularsh.IdFirmantePj.Value;
                        Guid rowid_titular = titularsh.RowId;
                        string FirmanteDe = titularsh.Apellidos + " " + titularsh.Nombres;


                        //var lstFirmantesSH = firmantesBL.GetFirmantesPJPF(id_firmante_pj).ToList();
                        var lstFirmantesSH = firmantesBL.GetTransfFirmantesPJPFSolicitudByIDSol(IdSolicitud).Where(x => x.IdPersonaJuridica == id_firmante_pj).ToList();
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
                    }
                    grdFirmantesSH.DataSource = dtFirmantesSH;
                    grdFirmantesSH.DataBind();
                }
                else
                {

                    var lstFirmantesPJ = firmantesBL.GetFirmantesTransferenciasPJ(id_personajuridica).ToList();

                    if (lstFirmantesPJ.Count() == 0)
                        throw new Exception("No existen firmantes con el ID: " + Convert.ToString(id_personajuridica));

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Apellidos", typeof(string));
                    dt.Columns.Add("Nombres", typeof(string));
                    dt.Columns.Add("TipoDoc", typeof(string));
                    dt.Columns.Add("NroDoc", typeof(string));
                    dt.Columns.Add("Cuit", typeof(string));
                    dt.Columns.Add("nom_tipocaracter", typeof(string));
                    dt.Columns.Add("IdTipoDocPersonal", typeof(int));
                    dt.Columns.Add("email", typeof(string));
                    dt.Columns.Add("id_tipocaracter", typeof(int));
                    dt.Columns.Add("cargo_firmante_pj", typeof(string));
                    dt.Columns.Add("rowindex", typeof(int));
                    int rowindex = 0;

                    foreach (var item in lstFirmantesPJ)
                    {
                        dt.Rows.Add(item.Apellidos, item.Nombres, item.TipoDoc, item.NroDoc, item.Cuit, item.NomTipoCaracter, item.IdTipoDocPersonal,
                                item.Email, item.IdTipoCaracter, item.CargoFirmantePj, rowindex);
                        rowindex++;
                    }

                    grdFirmantesPJ.DataSource = dt;
                    grdFirmantesPJ.DataBind();
                }

            }

        }

        private void CargarDatosTitularPJANT(int id_personajuridica)
        {
            //TransferenciasTitularesSolicitudPersonasJuridicasBL encomiendaTitularesPersonasJuridicasBL = new TransferenciasTitularesSolicitudPersonasJuridicasBL();
            LocalidadBL localidadBL = new LocalidadBL();
            FirmantesBL firmantesBL = new FirmantesBL();
            TransferenciasTitularesSolicitudPersonasJuridicasBL encTitPJBL = new TransferenciasTitularesSolicitudPersonasJuridicasBL();

            var pj = encTitPJBL.Single(id_personajuridica);

            if (pj != null)
            {
                hid_id_titular_pj.Value = id_personajuridica.ToString();

                ddlTipoSociedadPJ.SelectedValue = pj.IdTipoSociedad.ToString() != "0" ? pj.IdTipoSociedad.ToString() : "";
                txtRazonSocialPJ.Text = pj.RazonSocial;
                txtCuitPJ.Text = pj.CUIT;
                ddlTipoIngresosBrutosPJ.SelectedValue = pj.IdTipoiibb.ToString() != "0" ? pj.IdTipoiibb.ToString() : "";
                txtIngresosBrutosPJ.Text = pj.Numeroiibb;
                txtCallePJ.Text = pj.Calle;
                txtNroPuertaPJ.Text = pj.NroPuerta.ToString();
                txtPisoPJ.Text = pj.Piso;
                txtDeptoPJ.Text = pj.Depto;
                txtCPPJ.Text = pj.CodigoPostal;
                txtTelefonoPJ.Text = pj.Telefono;
                txtEmailPJ.Text = pj.Email;

                ddlTipoIngresosBrutosPJ_SelectedIndexChanged(ddlTipoIngresosBrutosPJ, new EventArgs());

                var localidad = localidadBL.Single(pj.IdLocalidad);
                if (localidad != null)
                {
                    ddlProvinciaPJ.SelectedValue = localidad.IdProvincia.ToString() != "0" ? localidad.IdProvincia.ToString() : "";
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
                    var lstTitularesSH = titularesBL.GetTitularesSHTransferenciasANT(id_personajuridica).ToList();

                    grdTitularesSH.DataSource = lstTitularesSH;
                    grdTitularesSH.DataBind();

                    DataTable dtFirmantesSH = dtFirmantesSHCargados();
                    dtFirmantesSH.Clear();
                    foreach (var titularsh in lstTitularesSH)
                    {
                        int id_firmante_pj = titularsh.IdFirmantePj.Value;
                        Guid rowid_titular = titularsh.RowId;
                        string FirmanteDe = titularsh.Apellidos + " " + titularsh.Nombres;


                        //var lstFirmantesSH = firmantesBL.GetFirmantesPJPF(id_firmante_pj).ToList();
                        var lstFirmantesSH = firmantesBL.GetFirmantesTransferenciasPJANT(id_firmante_pj).Where(x => x.NroDoc == titularsh.NroDoc).ToList();
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

                    var lstFirmantesPJ = firmantesBL.GetFirmantesTransferenciasPJANT(id_personajuridica).ToList();

                    if (lstFirmantesPJ.Count() == 0)
                        throw new Exception("No existen firmantes con el ID: " + Convert.ToString(id_personajuridica));

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Apellidos", typeof(string));
                    dt.Columns.Add("Nombres", typeof(string));
                    dt.Columns.Add("TipoDoc", typeof(string));
                    dt.Columns.Add("NroDoc", typeof(string));
                    dt.Columns.Add("Cuit", typeof(string));
                    dt.Columns.Add("nom_tipocaracter", typeof(string));
                    dt.Columns.Add("IdTipoDocPersonal", typeof(int));
                    dt.Columns.Add("email", typeof(string));
                    dt.Columns.Add("id_tipocaracter", typeof(int));
                    dt.Columns.Add("cargo_firmante_pj", typeof(string));
                    dt.Columns.Add("rowindex", typeof(int));
                    int rowindex = 0;

                    foreach (var item in lstFirmantesPJ)
                    {
                        dt.Rows.Add(item.Apellidos, item.Nombres, item.TipoDoc, item.NroDoc, item.Cuit, item.NomTipoCaracter, item.IdTipoDocPersonal,
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
            TransferenciasTitularesPersonasFisicasBL encTitPFBL = new TransferenciasTitularesPersonasFisicasBL();
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
                    txtCuitFirPF.Text = "";
                }
                else
                {

                    optOtraPersona.Checked = true;
                    optMismaPersona.Checked = false;

                    TransferenciasFirmantesPersonasFisicasBL encFirPFBL = new TransferenciasFirmantesPersonasFisicasBL();
                    var firmante = encFirPFBL.GetByFKIdPersonaFisica(id_personafisica).FirstOrDefault();

                    if (firmante != null)
                    {
                        txtApellidoFirPF.Text = firmante.Apellido;
                        txtNombresFirPF.Text = firmante.Nombres;
                        ddlTipoDocumentoFirPF.SelectedValue = firmante.IdTipoDocumentoPersonal.ToString();
                        txtNroDocumentoFirPF.Text = firmante.NumeroDocumento;
                        ddlTipoCaracterLegalFirPF.SelectedValue = firmante.IdTipoCaracter.ToString();
                        txtCuitFirPF.Text = firmante.Cuit;
                    }

                    pnlOtraPersona.Style["display"] = "block";

                }

                txtApellidosPF.Text = pf.Apellido;
                txtNombresPF.Text = pf.Nombres;
                txtNroDocumentoPF.Text = pf.NumeroDocumento.ToString();
                txtCuitPF.Text = pf.Cuit;
                txtIngresosBrutosPF.Text = pf.IngresosBrutos;
                txtCallePF.Text = pf.Calle;
                txtNroPuertaPF.Text = pf.NumeroPuerta.ToString();
                txtPisoPF.Text = pf.Piso;
                txtDeptoPF.Text = pf.Depto;
                txtCPPF.Text = pf.CodigoPostal;
                txtTelefonoPF.Text = pf.Telefono;
                //txtTelefonoPrefijoPF.Text = pf.TelefonoPrefijo;
                //txtTelefonoSufijoPF.Text = pf.TelefonoSufijo;
                txtTelefonoMovilPF.Text = pf.Celular;
                //txtSmsPF.Text = pf.Sms;
                txtEmailPF.Text = pf.Email;

                ddlTipoDocumentoPF.SelectedValue = pf.IdTipodocPersonal.ToString();
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

        private void CargarDatosTitularPFANT(int id_personafisica)
        {
            TransferenciasTitularesSolicitudPersonasFisicasBL encTitPFBL = new TransferenciasTitularesSolicitudPersonasFisicasBL();
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

                    TransferenciasFirmantesSolicitudPersonasFisicasBL encFirPFBL = new TransferenciasFirmantesSolicitudPersonasFisicasBL();
                    var firmante = encFirPFBL.GetByFKIdPersonaFisica(id_personafisica).FirstOrDefault();

                    if (firmante != null)
                    {
                        txtApellidoFirPF.Text = firmante.Apellido;
                        txtNombresFirPF.Text = firmante.Nombres;
                        ddlTipoDocumentoFirPF.SelectedValue = firmante.id_tipodoc_personal.ToString();
                        txtNroDocumentoFirPF.Text = firmante.Nro_Documento;
                        ddlTipoCaracterLegalFirPF.SelectedValue = firmante.id_tipocaracter.ToString();
                    }

                    pnlOtraPersona.Style["display"] = "block";

                }

                txtApellidosPF.Text = pf.Apellido;
                txtNombresPF.Text = pf.Nombres;
                txtNroDocumentoPF.Text = pf.NumeroDocumento.ToString();
                txtCuitPF.Text = pf.CUIT;
                txtIngresosBrutosPF.Text = pf.IngresosBrutos;
                txtCallePF.Text = pf.Calle;
                txtNroPuertaPF.Text = pf.NumeroPuerta.ToString();
                txtPisoPF.Text = pf.Piso;
                txtDeptoPF.Text = pf.Depto;
                txtCPPF.Text = pf.CodigoPostal;
                txtTelefonoPF.Text = pf.Telefono;
                //txtTelefonoPrefijoPF.Text = pf.TelefonoPrefijo;
                //txtTelefonoSufijoPF.Text = pf.TelefonoSufijo;
                txtTelefonoMovilPF.Text = pf.TelefonoMovil;
                //txtSmsPF.Text = pf.Sms;
                txtEmailPF.Text = pf.Email;

                ddlTipoDocumentoPF.SelectedValue = pf.IdTipoDocumentoPersonal.ToString();
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
            hid_tipo_titular.Value = "0"; //titulares NUEVO
            LimpiarControlesABMPJ();
            updAgregarPersonaJuridica.Update();
            this.EjecutarScript(updShowAgregarPersonas, "showfrmAgregarPersonaJuridica();");
            //ScriptManager.RegisterStartupScript(updShowAgregarPersonas, updShowAgregarPersonas.GetType(), " showfrmAgregarPersonaJuridica", "showfrmAgregarPersonaJuridica();", true);
        }

        protected void btnShowAgregarPJANT_Click(object sender, EventArgs e)
        {
            hid_id_titular_pj.Value = "0";
            hid_tipo_titular.Value = "1"; //titulares ANT
            LimpiarControlesABMPJ();
            updAgregarPersonaJuridica.Update();
            this.EjecutarScript(updShowAgregarPersonasANT, "showfrmAgregarPersonaJuridica();");

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
            dt.Columns.Add("Cuit", typeof(string));
            dt.Columns.Add("nom_tipocaracter", typeof(string));
            dt.Columns.Add("IdTipoDocPersonal", typeof(int));
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


                datarw[0] = HttpUtility.HtmlDecode(row.Cells[0].Text);
                datarw[1] = HttpUtility.HtmlDecode(row.Cells[1].Text);
                datarw[2] = HttpUtility.HtmlDecode(row.Cells[2].Text);
                datarw[3] = HttpUtility.HtmlDecode(row.Cells[3].Text);
                datarw[4] = HttpUtility.HtmlDecode(row.Cells[4].Text);
                datarw[5] = HttpUtility.HtmlDecode(row.Cells[6].Text);
                datarw[6] = int.Parse(hid_id_tipodoc_grdFirmantes.Value);
                datarw[7] = HttpUtility.HtmlDecode(row.Cells[5].Text);
                datarw[8] = int.Parse(hid_id_caracter_grdFirmantes.Value);
                datarw[9] = HttpUtility.HtmlDecode(row.Cells[7].Text);
                datarw[10] = row.RowIndex;

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
            try
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
                        dt.Rows.Add(txtApellidosFirPJ.Text.Trim(), txtNombresFirPJ.Text.Trim(), ddlTipoDocumentoFirPJ.SelectedItem.Text.Trim(), txtNroDocumentoFirPJ.Text.Trim(), txtCuitFirPJ.Text.Trim(),
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
                        dt.Rows[rowindex]["Cuit"] = txtCuitFirPJ.Text.Trim();
                        dt.Rows[rowindex]["nom_tipocaracter"] = ddlTipoCaracterLegalFirPJ.SelectedItem.Text;
                        dt.Rows[rowindex]["IdTipoDocPersonal"] = int.Parse(ddlTipoDocumentoFirPJ.SelectedValue);
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
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updgrdFirmantesPJ, "showfrmError();");
            }

        }

        protected void btnEditarFirPJ_Click(object sender, EventArgs e)
        {
            try
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
                ddlTipoDocumentoFirPJ.SelectedValue = dr["IdTipoDocPersonal"].ToString();
                ddlTipoCaracterLegalFirPJ.SelectedValue = dr["id_tipocaracter"].ToString();
                txtCuitFirPJ.Text = dr["Cuit"].ToString();
                txtCargoFirPJ.Text = dr["cargo_firmante_pj"].ToString();

                if (!(String.IsNullOrWhiteSpace(txtCargoFirPJ.Text)))
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
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updgrdFirmantesPJ, "showfrmError();");
            }
        }

        protected void btnEliminarFirmantePJ_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updConfirmarEliminarFirPJ, "showfrmError();");
            }
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
                        int TipoTitular = 0;
                        int.TryParse(hid_tipo_titular.Value, out TipoTitular);
                        if (TipoTitular == 0)
                        {
                            TransferenciasTitularesPersonasJuridicasDTO encTitPerJurDTO = new TransferenciasTitularesPersonasJuridicasDTO();
                            TransferenciasTitularesPersonasJuridicasBL encTitPJBL = new TransferenciasTitularesPersonasJuridicasBL();

                            DataTable dtFirmantesSH = dtFirmantesSHCargados();
                            DataTable dtTitularesSH = dtTitularesSHCargados();
                            DataTable dtFirmantes = dtFirmantesCargados();

                            FirmantesSHDTO firmantesSHDTO;
                            TitularesSHDTO titularesSHDTO;
                            TransferenciasFirmantesPersonasJuridicasDTO encFirPerJurInsertDTO;
                            encTitPerJurDTO = new TransferenciasTitularesPersonasJuridicasDTO();

                            encTitPerJurDTO.IdPersonaJuridica = id_personajuridica;
                            encTitPerJurDTO.IdSolicitud = IdSolicitud;
                            encTitPerJurDTO.IdTipoSociedad = id_tiposociedad;
                            encTitPerJurDTO.RazonSocial = Razon_Social;
                            encTitPerJurDTO.Cuit = Cuit;
                            encTitPerJurDTO.IdTipoiibb = id_tipoiibb;
                            encTitPerJurDTO.NumeroIibb = Ingresos_Brutos;
                            encTitPerJurDTO.Calle = Calle;
                            encTitPerJurDTO.NumeroPuerta = Nro_Puerta;
                            encTitPerJurDTO.Piso = Piso;
                            encTitPerJurDTO.Depto = Depto;
                            encTitPerJurDTO.IdLocalidad = id_Localidad;
                            encTitPerJurDTO.CodigoPostal = Codigo_Postal;
                            encTitPerJurDTO.Telefono = Telefono;
                            encTitPerJurDTO.Email = Email;
                            encTitPerJurDTO.CreateUser = userid;
                            encTitPerJurDTO.CreateDate = DateTime.Now;

                            List<FirmantesSHDTO> lstFirmantesSHDTO = new List<FirmantesSHDTO>();
                            List<TitularesSHDTO> lstTitularesSHDTO = new List<TitularesSHDTO>();
                            List<TransferenciasFirmantesPersonasJuridicasDTO> lstEncomiendaFirmantesPersonasJuridicasDTO = new List<TransferenciasFirmantesPersonasJuridicasDTO>();

                            foreach (DataRow dr in dtFirmantesSH.Rows)
                            {
                                firmantesSHDTO = new FirmantesSHDTO();
                                firmantesSHDTO.FirmanteDe = dr["FirmanteDe"].ToString();
                                firmantesSHDTO.Apellidos = dr["Apellidos"].ToString();
                                firmantesSHDTO.Nombres = dr["Nombres"].ToString();
                                firmantesSHDTO.TipoDoc = dr["TipoDoc"].ToString();
                                firmantesSHDTO.NroDoc = dr["NroDoc"].ToString();
                                firmantesSHDTO.nom_tipocaracter = dr["nom_tipocaracter"].ToString();
                                firmantesSHDTO.id_tipodoc_personal = Convert.ToInt32(dr["IdTipoDocPersonal"].ToString());
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
                                titularesSHDTO.IdTipoDocPersonal = Convert.ToInt32(rowTitularSH["IdTipoDocPersonal"].ToString());
                                titularesSHDTO.Email = rowTitularSH["email"].ToString();
                                lstTitularesSHDTO.Add(titularesSHDTO);
                            }

                            foreach (DataRow rowFirPerJur in dtFirmantes.Rows)
                            {
                                encFirPerJurInsertDTO = new TransferenciasFirmantesPersonasJuridicasDTO();
                                encFirPerJurInsertDTO.IdSolicitud = IdSolicitud;
                                encFirPerJurInsertDTO.IdPersonaJuridica = id_personajuridica;
                                encFirPerJurInsertDTO.Apellido = rowFirPerJur["Apellidos"].ToString();
                                encFirPerJurInsertDTO.Nombres = rowFirPerJur["Nombres"].ToString();
                                encFirPerJurInsertDTO.IdTipoDocumentoPersonal = int.Parse(rowFirPerJur["IdTipoDocPersonal"].ToString());
                                encFirPerJurInsertDTO.NumeroDocumento = rowFirPerJur["NroDoc"].ToString();
                                encFirPerJurInsertDTO.Email = rowFirPerJur["Email"].ToString();
                                encFirPerJurInsertDTO.IdTipoCaracter = int.Parse(rowFirPerJur["id_tipocaracter"].ToString());
                                encFirPerJurInsertDTO.CargoFirmantePersonaJuridica = rowFirPerJur["cargo_firmante_pj"].ToString();
                                encFirPerJurInsertDTO.Cuit = rowFirPerJur["Cuit"].ToString();

                                lstEncomiendaFirmantesPersonasJuridicasDTO.Add(encFirPerJurInsertDTO);
                            }


                            encTitPerJurDTO.firmantesSH = lstFirmantesSHDTO;
                            encTitPerJurDTO.titularesSH = lstTitularesSHDTO;
                            encTitPerJurDTO.encFirDTO = lstEncomiendaFirmantesPersonasJuridicasDTO;

                            encTitPJBL.Insert(encTitPerJurDTO);
                        }
                        else
                        {
                            TransferenciasTitularesSolicitudPersonasJuridicasDTO encTitPerJurDTO = new TransferenciasTitularesSolicitudPersonasJuridicasDTO();
                            TransferenciasTitularesSolicitudPersonasJuridicasBL encTitPJBL = new TransferenciasTitularesSolicitudPersonasJuridicasBL();

                            DataTable dtFirmantesSH = dtFirmantesSHCargados();
                            DataTable dtTitularesSH = dtTitularesSHCargados();
                            DataTable dtFirmantes = dtFirmantesCargados();

                            FirmantesSHDTO firmantesSHDTO;
                            TitularesSHDTO titularesSHDTO;
                            TransferenciasFirmantesSolicitudPersonasJuridicasDTO encFirPerJurInsertDTO;
                            encTitPerJurDTO = new TransferenciasTitularesSolicitudPersonasJuridicasDTO();

                            encTitPerJurDTO.IdPersonaJuridica = id_personajuridica;
                            encTitPerJurDTO.IdSolicitud = IdSolicitud;
                            encTitPerJurDTO.IdTipoSociedad = id_tiposociedad;
                            encTitPerJurDTO.RazonSocial = Razon_Social;
                            encTitPerJurDTO.CUIT = Cuit;
                            encTitPerJurDTO.IdTipoiibb = id_tipoiibb;
                            encTitPerJurDTO.Numeroiibb = Ingresos_Brutos;
                            encTitPerJurDTO.Calle = Calle;
                            encTitPerJurDTO.NroPuerta = Nro_Puerta;
                            encTitPerJurDTO.Piso = Piso;
                            encTitPerJurDTO.Depto = Depto;
                            encTitPerJurDTO.IdLocalidad = id_Localidad;
                            encTitPerJurDTO.CodigoPostal = Codigo_Postal;
                            encTitPerJurDTO.Telefono = Telefono;
                            encTitPerJurDTO.Email = Email;
                            encTitPerJurDTO.CreateUser = userid;
                            encTitPerJurDTO.CreateDate = DateTime.Now;

                            List<FirmantesSHDTO> lstFirmantesSHDTO = new List<FirmantesSHDTO>();
                            List<TitularesSHDTO> lstTitularesSHDTO = new List<TitularesSHDTO>();
                            List<TransferenciasFirmantesSolicitudPersonasJuridicasDTO> lstEncomiendaFirmantesPersonasJuridicasDTO = new List<TransferenciasFirmantesSolicitudPersonasJuridicasDTO>();

                            foreach (DataRow dr in dtFirmantesSH.Rows)
                            {
                                firmantesSHDTO = new FirmantesSHDTO();
                                firmantesSHDTO.FirmanteDe = dr["FirmanteDe"].ToString();
                                firmantesSHDTO.Apellidos = dr["Apellidos"].ToString();
                                firmantesSHDTO.Nombres = dr["Nombres"].ToString();
                                firmantesSHDTO.TipoDoc = dr["TipoDoc"].ToString();
                                firmantesSHDTO.NroDoc = dr["NroDoc"].ToString();
                                firmantesSHDTO.nom_tipocaracter = dr["nom_tipocaracter"].ToString();
                                firmantesSHDTO.id_tipodoc_personal = Convert.ToInt32(dr["IdTipoDocPersonal"].ToString());
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
                                titularesSHDTO.IdTipoDocPersonal = Convert.ToInt32(rowTitularSH["IdTipoDocPersonal"].ToString());
                                titularesSHDTO.Email = rowTitularSH["email"].ToString();
                                lstTitularesSHDTO.Add(titularesSHDTO);
                            }

                            foreach (DataRow rowFirPerJur in dtFirmantes.Rows)
                            {
                                encFirPerJurInsertDTO = new TransferenciasFirmantesSolicitudPersonasJuridicasDTO();
                                encFirPerJurInsertDTO.id_solicitud = IdSolicitud;
                                encFirPerJurInsertDTO.id_personajuridica = id_personajuridica;
                                encFirPerJurInsertDTO.Apellido = rowFirPerJur["Apellidos"].ToString();
                                encFirPerJurInsertDTO.Nombres = rowFirPerJur["Nombres"].ToString();
                                encFirPerJurInsertDTO.id_tipodoc_personal = int.Parse(rowFirPerJur["IdTipoDocPersonal"].ToString());
                                encFirPerJurInsertDTO.Nro_Documento = rowFirPerJur["NroDoc"].ToString();
                                encFirPerJurInsertDTO.Email = rowFirPerJur["Email"].ToString();
                                encFirPerJurInsertDTO.id_tipocaracter = int.Parse(rowFirPerJur["id_tipocaracter"].ToString());
                                encFirPerJurInsertDTO.cargo_firmante_pj = rowFirPerJur["cargo_firmante_pj"].ToString();
                                encFirPerJurInsertDTO.Cuit = rowFirPerJur["Cuit"].ToString();
                                lstEncomiendaFirmantesPersonasJuridicasDTO.Add(encFirPerJurInsertDTO);
                            }


                            encTitPerJurDTO.firmantesSH = lstFirmantesSHDTO;
                            encTitPerJurDTO.titularesSH = lstTitularesSHDTO;
                            encTitPerJurDTO.encFirDTO = lstEncomiendaFirmantesPersonasJuridicasDTO;

                            encTitPJBL.Insert(encTitPerJurDTO);
                        }
                    }
                    catch
                    {
                        throw;
                    }


                    CargarDatosTitulares(IdSolicitud);
                    CargarDatosTitularesANT(IdSolicitud);
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
                if (grdTitularesHab.Rows.Count == 0 || grdTitularesHabANT.Rows.Count == 0)
                    throw new Exception(Errors.SSIT_SOLICITUD_INGRESAR_TITULARES);

                Response.Redirect(string.Format("~/" + RouteConfig.VISOR_TRANSMISIONES + "{0}", IdSolicitud));

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
                //Response.Redirect(string.Format("~/" + RouteConfig.VISOR_TRANSFERENCIAS + "{0}", IdSolicitud));
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesGuardar, "showfrmError();");

            }
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
            try
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
                    dtTitularesSH.Rows[rowindex]["IdTipoDocPersonal"] = int.Parse(ddlTipoDocumentoTitSH.SelectedValue);
                    dtTitularesSH.Rows[rowindex]["email"] = txtEmailTitSH.Text.Trim();



                    if (drFirmantesSH != null && optOtraPersonaSH.Checked)
                    {
                        drFirmantesSH["Apellidos"] = txtApellidosFirSH.Text.Trim();
                        drFirmantesSH["Nombres"] = txtNombresFirSH.Text.Trim();
                        drFirmantesSH["NroDoc"] = txtNroDocumentoFirSH.Text.Trim();
                        drFirmantesSH["IdTipoDocPersonal"] = ddlTipoDocumentoFirSH.SelectedValue;
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
                        drFirmantesSH["IdTipoDocPersonal"] = ddlTipoDocumentoTitSH.SelectedValue;
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
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updBotonesIngresarTitularesSH, "showfrmError();");
            }
        }

        private DataTable dtTitularesSHCargados()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("rowid", typeof(Guid));
            dt.Columns.Add("Apellidos", typeof(string));
            dt.Columns.Add("Nombres", typeof(string));
            dt.Columns.Add("TipoDoc", typeof(string));
            dt.Columns.Add("NroDoc", typeof(string));
            dt.Columns.Add("IdTipoDocPersonal", typeof(int));
            dt.Columns.Add("email", typeof(string));

            foreach (GridViewRow row in grdTitularesSH.Rows)
            {
                DataRow datarw;
                datarw = dt.NewRow();

                HiddenField hid_id_tipodoc_grdTitularesSH = (HiddenField)grdTitularesSH.Rows[row.RowIndex].Cells[0].FindControl("hid_id_tipodoc_grdTitularesSH");
                HiddenField hid_rowid_grdTitularesSH = (HiddenField)grdTitularesSH.Rows[row.RowIndex].Cells[0].FindControl("hid_rowid_grdTitularesSH");

                datarw[0] = Guid.Parse(hid_rowid_grdTitularesSH.Value);
                datarw[1] = HttpUtility.HtmlDecode(row.Cells[0].Text);
                datarw[2] = HttpUtility.HtmlDecode(row.Cells[1].Text);
                datarw[3] = HttpUtility.HtmlDecode(row.Cells[2].Text);
                datarw[4] = HttpUtility.HtmlDecode(row.Cells[3].Text);
                datarw[5] = int.Parse(hid_id_tipodoc_grdTitularesSH.Value);
                datarw[6] = HttpUtility.HtmlDecode(row.Cells[4].Text);

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
            dt.Columns.Add("IdTipoDocPersonal", typeof(int));
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
            try
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
                ddlTipoDocumentoTitSH.SelectedValue = drTitularesSH["IdTipoDocPersonal"].ToString();
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
                        ddlTipoDocumentoFirSH.SelectedValue = drFirmantesSH["IdTipoDocPersonal"].ToString();
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
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updgrillaTitularesSH, "showfrmError();");
            }
        }

        protected void btnEliminarTitularSH_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updgrillaTitularesSH, "showfrmError();");
            }

        }

        //Funcionalidades para validad CUIT en transferencias

        //Validar persona fisica
        protected void validarCuitPfButton_Click(object sender, EventArgs e)
        {
            try
            {
                var titularesBL = new TitularesBL();
                var persona = titularesBL.GetPersonaTAD(txtCuitPF.Text);
                var datos = GeneratePersonaTAD(persona);
                AutocompleteFormPF(datos);
                ValidFromPF();
            }
            catch (Exception ex)
            {
                ClearFormPF();
                lblError.Text = ex.Message.Replace("\r\n", "<br>");
                this.EjecutarScript(updAgregarPersonaFisica, "showfrmError();");
            }
            updAgregarPersonaFisica.Update();
        }

        //Validar persona juridica
        protected void validarCuitPjButton_Click(object sender, EventArgs e)
        {
            try
            {
                var titularesBL = new TitularesBL();
                var persona = titularesBL.GetPersonaTAD(txtCuitPJ.Text);
                var datos = GeneratePersonaTAD(persona);
                AutocompleteFormPJ(datos);
                ValidFromPJ();
            }
            catch (Exception ex)
            {
                ClearFormPJ();
                lblError.Text = ex.Message.Replace("\r\n", "<br>");
                this.EjecutarScript(updAgregarPersonaJuridica, "showfrmError();");
            }
            updAgregarPersonaJuridica.Update();
        }

        //Validar cuit otro firmante pf
        protected void validarCuitOtroFirmante_Click(object sender, EventArgs e)
        {
            try
            {
                var titularesBL = new TitularesBL();
                var persona = titularesBL.GetPersonaTAD(txtCuitFirPF.Text);
                var datos = GeneratePersonaTAD(persona);
                AutocompleteFormDatosFirmante(datos);
                ValidFromDF();
            }
            catch (Exception ex)
            {
                ClearFormDF();
                lblError.Text = ex.Message.Replace("\r\n", "<br>");
                this.EjecutarScript(updAgregarPersonaFisica, "showfrmError();");
            }
            updFirmantePF.Update();
        }

        //Validar cuit otro firmante pj
        protected void validarCuitOtroPJButton_Click(object sender, EventArgs e)
        {
            try
            {
                var titularesBL = new TitularesBL();
                var persona = titularesBL.GetPersonaTAD(txtCuitFirPJ.Text);
                var datos = GeneratePersonaTAD(persona);
                AutoCompleteFormDatosFJ(datos);
                ValidFromNuevaFirmantePJ();
            }
            catch (Exception ex)
            {
                ClearFormNuevoPJ();
                lblError.Text = ex.Message.Replace("\r\n", "<br>");
                this.EjecutarScript(updAgregarPersonaJuridica, "showfrmError();");
            }
            updFirmantePJ.Update();
        }



        private void ValidFromNuevaFirmantePJ()
        {
            txtApellidosFirPJ.Enabled = string.IsNullOrWhiteSpace(txtApellidosFirPJ.Text);
            txtNombresFirPJ.Enabled = string.IsNullOrWhiteSpace(txtNombresFirPJ.Text);
            txtNroDocumentoFirPJ.Enabled = string.IsNullOrWhiteSpace(txtNroDocumentoFirPJ.Text);
            txtEmailFirPJ.Enabled = string.IsNullOrWhiteSpace(txtEmailFirPJ.Text);
        }

        private void ValidFromPF()
        {
            //txtApellidosPF.Enabled = string.IsNullOrWhiteSpace(txtApellidosPF.Text);
            //txtNombresPF.Enabled = string.IsNullOrWhiteSpace(txtNombresPF.Text);
            txtNroDocumentoPF.Enabled = string.IsNullOrWhiteSpace(txtNroDocumentoPF.Text);
            txtIngresosBrutosPF.Enabled = string.IsNullOrWhiteSpace(txtIngresosBrutosPF.Text) && ddlTipoIngresosBrutosPF.SelectedIndex > 0;
            txtCallePF.Enabled = string.IsNullOrWhiteSpace(txtCallePF.Text);
            txtNroPuertaPF.Enabled = string.IsNullOrWhiteSpace(txtNroPuertaPF.Text);
            txtCPPF.Enabled = string.IsNullOrWhiteSpace(txtCPPF.Text);
            txtTelefonoPF.Enabled = string.IsNullOrWhiteSpace(txtTelefonoPF.Text);
            txtEmailPF.Enabled = string.IsNullOrWhiteSpace(txtEmailPF.Text);

            ddlTipoDocumentoPF.Enabled = true;
            ddlTipoIngresosBrutosPF.Enabled = true;
        }

        private void ValidFromDF()
        {
            txtApellidoFirPF.Enabled = string.IsNullOrWhiteSpace(txtApellidoFirPF.Text);
            txtNombresFirPF.Enabled = string.IsNullOrWhiteSpace(txtNombresFirPF.Text);
            txtNroDocumentoFirPF.Enabled = string.IsNullOrWhiteSpace(txtNroDocumentoFirPF.Text);
        }

        private void ValidFromPJ()
        {
            //txtRazonSocialPJ.Enabled = string.IsNullOrWhiteSpace(txtRazonSocialPJ.Text);
            txtIngresosBrutosPJ.Enabled = string.IsNullOrWhiteSpace(txtIngresosBrutosPF.Text) && ddlTipoIngresosBrutosPJ.SelectedIndex > 0;
            txtCallePJ.Enabled = string.IsNullOrWhiteSpace(txtCallePJ.Text);
            txtNroPuertaPJ.Enabled = string.IsNullOrWhiteSpace(txtNroPuertaPJ.Text);
            txtCPPJ.Enabled = string.IsNullOrWhiteSpace(txtCPPJ.Text);
            txtTelefonoPJ.Enabled = string.IsNullOrWhiteSpace(txtTelefonoPJ.Text);
            txtEmailPJ.Enabled = string.IsNullOrWhiteSpace(txtEmailPJ.Text);

            ddlTipoIngresosBrutosPJ.Enabled = true;
        }



        private PersonaTAD GeneratePersonaTAD(PersonaTadDTO persona)
        {
            return new PersonaTAD
            {
                RazonSocial = persona.RazonSocial,
                Apellidos = $"{persona.Apellido1 ?? string.Empty} {persona.Apellido2 ?? string.Empty}".Trim(),
                Nombres = $"{persona.Nombre1 ?? string.Empty} {persona.Nombre2 ?? string.Empty} {persona.Nombre3 ?? string.Empty}".Trim(),
                Documento = persona.DocumentoIdentidad,
                CodigoPostal = persona.DomicilioConstituido?.CodPostal,
                Calle = persona.DomicilioConstituido?.Direccion,
                NroPuerta = persona.DomicilioConstituido?.Altura,
                Cuit = persona.Cuit,
                Telefono = persona.Telefono,
                Email = persona.Email,
            };
        }

        private void AutocompleteFormPF(PersonaTAD datos)
        {
            txtApellidosPF.Text = datos.Apellidos;
            txtNombresPF.Text = datos.Nombres;
            txtNroDocumentoPF.Text = datos.Documento;
            txtIngresosBrutosPF.Text = datos.IngresosBrutos;
            txtCallePF.Text = datos.Calle;
            txtNroPuertaPF.Text = datos.NroPuerta;
            txtCPPF.Text = datos.CodigoPostal;
            txtTelefonoPF.Text = datos.Telefono;
            txtEmailPF.Text = datos.Email;
        }

        private void AutocompleteFormPJ(PersonaTAD datos)
        {
            txtRazonSocialPJ.Text = datos.RazonSocial;
            txtIngresosBrutosPJ.Text = datos.IngresosBrutos;
            txtCallePJ.Text = datos.Calle;
            txtNroPuertaPJ.Text = datos.NroPuerta;
            txtCPPJ.Text = datos.CodigoPostal;
            txtTelefonoPJ.Text = datos.Telefono;
            txtEmailPJ.Text = datos.Email;
        }

        private void AutoCompleteFormDatosFJ(PersonaTAD datos)
        {
            txtApellidosFirPJ.Text = datos.Apellidos;
            txtNombresFirPJ.Text = datos.Nombres;
            txtNroDocumentoFirPJ.Text = datos.Documento;
            txtEmailFirPJ.Text = datos.Email;
        }

        private void AutocompleteFormDatosFirmante(PersonaTAD datos)
        {
            txtApellidoFirPF.Text = datos.Apellidos;
            txtNombresFirPF.Text = datos.Nombres;
            txtNroDocumentoFirPF.Text = datos.Documento;

        }

        private void ClearFormPF()
        {
            txtApellidosPF.Text = string.Empty;
            txtNombresPF.Text = string.Empty;
            txtNroDocumentoPF.Text = string.Empty;
            txtIngresosBrutosPF.Text = string.Empty;
            txtCallePF.Text = string.Empty;
            txtNroPuertaPF.Text = string.Empty;
            txtCPPF.Text = string.Empty;
            txtTelefonoPF.Text = string.Empty;
            txtEmailPF.Text = string.Empty;

            txtApellidosPF.Enabled = false;
            txtNombresPF.Enabled = false;
            txtNroDocumentoPF.Enabled = false;
            txtIngresosBrutosPF.Enabled = false;
            txtCallePF.Enabled = false;
            txtNroPuertaPF.Enabled = false;
            txtCPPF.Enabled = false;
            txtTelefonoPF.Enabled = false;
            txtEmailPF.Enabled = false;
            ddlTipoDocumentoPF.Enabled = false;
            ddlTipoIngresosBrutosPF.Enabled = false;
        }

        protected void ClearFormDF()
        {
            txtApellidoFirPF.Text = string.Empty;
            txtNombresFirPF.Text = string.Empty;
            txtNroDocumentoFirPF.Text = string.Empty;
        }

        private void ClearFormPJ()
        {
            txtRazonSocialPJ.Text = string.Empty;
            txtIngresosBrutosPJ.Text = string.Empty;
            txtCallePJ.Text = string.Empty;
            txtNroPuertaPJ.Text = string.Empty;
            txtCPPJ.Text = string.Empty;
            txtTelefonoPJ.Text = string.Empty;
            txtEmailPJ.Text = string.Empty;

            txtRazonSocialPJ.Enabled = false;
            txtIngresosBrutosPJ.Enabled = false;
            txtCallePJ.Enabled = false;
            txtNroPuertaPJ.Enabled = false;
            txtCPPJ.Enabled = false;
            txtTelefonoPJ.Enabled = false;
            txtEmailPJ.Enabled = false;
            ddlTipoIngresosBrutosPJ.Enabled = false;
        }

        protected void ClearFormNuevoPJ()
        {
            txtApellidosFirPJ.Text = string.Empty;
            txtNombresFirPJ.Text = string.Empty;
            txtNroDocumentoFirPJ.Text = string.Empty;
            txtEmailFirPJ.Text = string.Empty;
        }



        private class PersonaTAD
        {
            public string Apellidos { get; set; }
            public string Nombres { get; set; }
            public string TipoDocumento { get; set; }
            public string Documento { get; set; }
            public string RazonSocial { get; set; }
            public string Cuit { get; set; }
            public string TipoIngresosBrutos { get; set; }
            public string IngresosBrutos { get; set; }
            public string Calle { get; set; }
            public string NroPuerta { get; set; }
            public string CodigoPostal { get; set; }
            public string Telefono { get; set; }
            public string Email { get; set; }
        }
    }
}