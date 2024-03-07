using SSIT.App_Components;
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
using SSIT.Common;
using Microsoft.Ajax.Utilities;
using SSIT.Account;

namespace SSIT
{
    public partial class Titulares : SecurePage
    {
        SSITSolicitudesBL solBL = new SSITSolicitudesBL();

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
                Titulo.CargarDatos(id_solicitud, Constantes.TIT_TITULARES);
            }

        }


        private int id_solicitud
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
        private int id_tipo_tramite
        {
            get
            {
                int ret = 0;
                int.TryParse(hid_id_tipo_tramite.Value, out ret);
                return ret;
            }
            set
            {
                hid_id_tipo_tramite.Value = value.ToString();
            }

        }
        private void ComprobarSolicitud()
        {
            if (Page.RouteData.Values["id_solicitud"] != null)
            {
                this.id_solicitud = Convert.ToInt32(Page.RouteData.Values["id_solicitud"].ToString());
                SSITSolicitudesBL solBL = new SSITSolicitudesBL();
                var sol = solBL.Single(id_solicitud);
                if (sol != null)
                {
                    Guid userid_solicitud = (Guid)Membership.GetUser().ProviderUserKey;
                    this.id_tipo_tramite = sol.IdTipoTramite;

                    if (userid_solicitud != sol.CreateUser)
                        Server.Transfer("~/Errores/Error3002.aspx");
                    else
                    {
                        if (!(sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.INCOM ||
                                sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.COMP ||
                                sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF ||
                                sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO))
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
                CargarDatosTitulares(id_solicitud);

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

        public void CargarDatosTitulares(int id_solicitud)
        {

            TitularesBL titularesBL = new TitularesBL();
            FirmantesBL firmantesBL = new FirmantesBL();
            var sol = solBL.Single(id_solicitud);

            var lstTitulares = titularesBL.GetTitularesSolicitud(id_solicitud).ToList();

            //if (lstTitulares.Count() == 0)
            //    throw new Exception("No existen titulares para la solicitud: " + Convert.ToString(id_solicitud));

            var lstFirmantes = firmantesBL.GetFirmantesSolicitud(id_solicitud).ToList();

            //if (lstFirmantes.Count() == 0)
            //    throw new Exception("No existen firmantes para la solicitud: " + Convert.ToString(id_solicitud));

            grdTitularesHab.DataSource = lstTitulares;
            grdTitularesHab.DataBind();

            grdTitularesTra.DataSource = lstFirmantes;
            grdTitularesTra.DataBind();

            if ((this.id_tipo_tramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                && sol.SSITSolicitudesOrigenDTO != null)
            {
                btnShowAgregarPF.Visible = false;
                btnShowAgregarPJ.Visible = false;

                foreach (GridViewRow row in grdTitularesHab.Rows)
                {
                    LinkButton btnEliminarTitular = (LinkButton)row.FindControl("btnEliminarTitular");
                    btnEliminarTitular.Visible = false;
                }
                updShowAgregarPersonas.Update();
            }

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
            //var lstTipoDocPersonalCompleto = tipoDocumentoPersonalBL.GetAll();

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
            int[] disponibilidadPJ = new int[] { 1, 2 };

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
            //ddlTipoCaracterLegalFirPF.Items.Insert(0, string.Empty);

            SSITSolicitudesFirmantesPersonasJuridicasBL solFirPJBL = new SSITSolicitudesFirmantesPersonasJuridicasBL();
            string[] lstCargosFirmantes = solFirPJBL.GetCargoFirmantesPersonasJuridicas();

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
                var lstLocalidades = localidadBL.GetByFKIdProvinciaExcluir(idProvincia, false).OrderBy(x => x.Depto);

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
                txtIngresosBrutosPF.Enabled = string.IsNullOrWhiteSpace(txtIngresosBrutosPF.Text); ;
            }
            upd_txtIngresosBrutosPF.Update();
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
            txtCuitFirPF.Text = "";
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
                    || id_tiposociedad == (int)Constantes.TipoSociedad.Sociedad_no_constituidas_regularmente))
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
                txtIngresosBrutosPJ.Enabled = string.IsNullOrWhiteSpace(txtIngresosBrutosPJ.Text); ;
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
            int id_personafisica = 0;
            int.TryParse(hid_id_titular_pf.Value, out id_personafisica);

            //Valida si existe una persona física ya ingresada con el mismo numero de CUIT.
            SSITSolicitudesTitularesPersonasFisicasBL ssitTitPFBL = new SSITSolicitudesTitularesPersonasFisicasBL();

            var ssitTitPFDTO = ssitTitPFBL.GetByIdSolicitudCuitIdPersonaFisica(this.id_solicitud, txtCuitPF.Text.Trim(), id_personafisica);

            bool existeTitular = ssitTitPFDTO.Any();

            if (existeTitular == true)
            {
                var titPF = ssitTitPFDTO.FirstOrDefault();
                string apellido = titPF.Apellido;
                string nombre = titPF.Nombres;

                lblError.Text = "Ya existe el titular (" + apellido + ", " + nombre + ")  con el mismo número de CUIT.";
                this.EjecutarScript(updBotonesAgregarPF, "showfrmError();");
                ret = false;
            }

            return ret;
        }

        private bool ValidarDatosPantallaPJ()
        {
            bool ret = true;

            int id_personajuridica = 0;
            int.TryParse(hid_id_titular_pj.Value, out id_personajuridica);

            SSITSolicitudesTitularesPersonasJuridicasBL solTitPJBL = new SSITSolicitudesTitularesPersonasJuridicasBL();

            //Valida si existe una persona física ya ingresada con el mismo numero de CUIT.
            var ssitTitPJDTO = solTitPJBL.GetByIdSolicitudCuitIdPersonaJuridica(this.id_solicitud, txtCuitPJ.Text.Trim(), id_personajuridica);
            bool existeTitular = ssitTitPJDTO.Any();

            if (existeTitular == true)
            {
                var titPJ = ssitTitPJDTO.FirstOrDefault();
                string RazonSocial = titPJ.RazonSocial;

                lblError.Text = "Ya existe el titular (" + RazonSocial + ")  con el mismo número de CUIT.";
                this.EjecutarScript(updBotonesAgregarPJ, "showfrmError();");
                ret = false;
            }

            return ret;

        }

        protected void btnAceptarTitPF_Click(object sender, EventArgs e)
        {
            bool valido = true;
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
                        SSITSolicitudesFirmantesPersonasFisicasBL solFirPFBL = new SSITSolicitudesFirmantesPersonasFisicasBL();
                        SSITSolicitudesTitularesPersonasFisicasBL solTitPFBL = new SSITSolicitudesTitularesPersonasFisicasBL();

                        SSITSolicitudesFirmantesPersonasFisicasDTO solFirPFDTO = new SSITSolicitudesFirmantesPersonasFisicasDTO();
                        SSITSolicitudesTitularesPersonasFisicasDTO solTitPFDTO = new SSITSolicitudesTitularesPersonasFisicasDTO();


                        if (id_personafisica > 0)
                        {
                            solFirPFDTO = solFirPFBL.GetByIdSolicitudIdPersonaFisica(id_solicitud, id_personafisica).FirstOrDefault();
                            solTitPFDTO = solTitPFBL.GetByIdSolicitudIdPersonaFisica(id_solicitud, id_personafisica).FirstOrDefault();

                            if (solFirPFDTO != null)
                                solFirPFBL.Delete(solFirPFDTO);
                            if (solTitPFDTO != null)
                                solTitPFBL.Delete(solTitPFDTO);
                        }

                        solTitPFDTO.IdSolicitud = id_solicitud;
                        solTitPFDTO.Apellido = Apellido;
                        solTitPFDTO.Nombres = Nombres;
                        solTitPFDTO.IdTipodocPersonal = id_tipodoc_personal;
                        solTitPFDTO.NroDocumento = Nro_Documento;
                        solTitPFDTO.Cuit = Cuit;
                        solTitPFDTO.IdTipoiibb = id_tipoiibb;
                        solTitPFDTO.IngresosBrutos = Ingresos_Brutos;
                        solTitPFDTO.Calle = Calle;
                        solTitPFDTO.NroPuerta = Nro_Puerta;
                        solTitPFDTO.Piso = Piso;
                        solTitPFDTO.Depto = Depto;
                        solTitPFDTO.Torre = Torre;
                        solTitPFDTO.IdLocalidad = id_Localidad;
                        solTitPFDTO.CodigoPostal = Codigo_Postal;
                        solTitPFDTO.Telefono = Telefono;
                        solTitPFDTO.TelefonoMovil = TelefonoMovil;

                        solTitPFDTO.Email = Email.ToLower();
                        solTitPFDTO.MismoFirmante = MismoFirmante;
                        solTitPFDTO.CreateUser = userid;
                        solTitPFDTO.CreateDate = DateTime.Now;

                        solTitPFDTO.DtoFirmantes = new SSITSolicitudesFirmantesPersonasFisicasDTO();
                        solTitPFDTO.DtoFirmantes.IdSolicitud = id_solicitud;

                        if (!MismoFirmante)
                        {
                            int.TryParse(ddlTipoDocumentoFirPF.SelectedValue, out id_tipodoc_personal);
                            int.TryParse(ddlTipoCaracterLegalFirPF.SelectedValue, out TipoCaracterLegalTitular);
                            solTitPFDTO.DtoFirmantes.Apellido = txtApellidoFirPF.Text.Trim();
                            solTitPFDTO.DtoFirmantes.Nombres = txtNombresFirPF.Text.Trim();
                            solTitPFDTO.DtoFirmantes.IdTipoDocPersonal = id_tipodoc_personal;
                            solTitPFDTO.DtoFirmantes.NroDocumento = txtNroDocumentoFirPF.Text.Trim();
                            solTitPFDTO.DtoFirmantes.IdTipoCaracter = TipoCaracterLegalTitular;
                            solTitPFDTO.DtoFirmantes.Cuit = txtCuitFirPF.Text.Trim();
                        }
                        else
                        {
                            solTitPFDTO.DtoFirmantes.Apellido = Apellido;
                            solTitPFDTO.DtoFirmantes.Nombres = Nombres;
                            solTitPFDTO.DtoFirmantes.IdTipoDocPersonal = id_tipodoc_personal;
                            solTitPFDTO.DtoFirmantes.NroDocumento = Nro_Documento;
                            solTitPFDTO.DtoFirmantes.IdTipoCaracter = TipoCaracterLegalTitular;
                            solTitPFDTO.DtoFirmantes.Cuit = Cuit;
                        }

                        var sol = solBL.Single(id_solicitud);
                        if (!MismoFirmante)
                        {

                            valido = ((sol.IdTipoTramite == (int)Constantes.TipoTramite.PERMISO && sol.IdTipoExpediente == (int)Constantes.TipoDeExpediente.MusicaCanto)
                                        || ValidarCuit(solTitPFDTO.Cuit.Trim(), solTitPFDTO.DtoFirmantes.Cuit.Trim(), updBotonesAgregarPF));
                        }

                        valido = ((sol.IdTipoTramite == (int)Constantes.TipoTramite.PERMISO && sol.IdTipoExpediente == (int)Constantes.TipoDeExpediente.MusicaCanto)
                                 || ValidarApoderamiento(solTitPFDTO.Cuit, updBotonesAgregarPF));

                        if (valido)
                        {
                            solTitPFBL.Insert(solTitPFDTO);
                            copiarTitulares();
                        }
                    }
                    catch
                    {
                        throw;
                    }

                    CargarDatosTitulares(this.id_solicitud);
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
                    SSITSolicitudesFirmantesPersonasFisicasBL solFirPFBL = new SSITSolicitudesFirmantesPersonasFisicasBL();
                    SSITSolicitudesTitularesPersonasFisicasBL solTitPFBL = new SSITSolicitudesTitularesPersonasFisicasBL();

                    SSITSolicitudesFirmantesPersonasFisicasDTO solFirPFDTO = solFirPFBL.GetByIdSolicitudIdPersonaFisica(this.id_solicitud, id_persona).FirstOrDefault();
                    SSITSolicitudesTitularesPersonasFisicasDTO solTitPFDTO = solTitPFBL.GetByIdSolicitudIdPersonaFisica(this.id_solicitud, id_persona).FirstOrDefault();

                    if (solFirPFDTO != null)
                        solFirPFBL.Delete(solFirPFDTO);

                    if (solTitPFDTO != null)
                        solTitPFBL.Delete(solTitPFDTO);
                }
                else
                {
                    TitularesBL titBL = new TitularesBL();
                    titBL.DeleteTitPJ(this.id_solicitud, id_persona);
                }
                copiarTitulares();
                CargarDatosTitulares(this.id_solicitud);
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
            SSITSolicitudesTitularesPersonasJuridicasBL solTitPJBL = new SSITSolicitudesTitularesPersonasJuridicasBL();

            LocalidadBL localidadBL = new LocalidadBL();
            FirmantesBL firmantesBL = new FirmantesBL();

            var pj = solTitPJBL.Single(id_personajuridica);
            var sol = solBL.Single(pj.IdSolicitud);

            if (pj != null)
            {
                hid_id_titular_pj.Value = id_personajuridica.ToString();

                ddlTipoSociedadPJ.SelectedValue = pj.IdTipoSociedad.ToString();
                txtRazonSocialPJ.Text = pj.RazonSocial;
                txtCuitPJ.Text = pj.CUIT;
                ddlTipoIngresosBrutosPJ.SelectedValue = pj.IdTipoiibb.ToString();
                txtIngresosBrutosPJ.Text = pj.NroIibb;
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
                    var lstTitularesSH = titularesBL.GetTitularesSHSolicitud(id_personajuridica).ToList();

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


                        var lstFirmantesSH = firmantesBL.GetFirmantesPJPFSolicitud(id_firmante_pj).ToList();

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
                            datarw[5] = firmanteSH.NomTipoCaracter;//
                            datarw[6] = firmanteSH.IdTipoDocPersonal;
                            datarw[7] = firmanteSH.IdTipoCaracter;
                            datarw[8] = firmanteSH.CargoFirmantePj;
                            datarw[9] = firmanteSH.Email;
                            datarw[10] = Guid.NewGuid();
                            datarw[11] = rowid_titular;
                            datarw[12] = firmanteSH.FirmanteMismaPersona;
                            datarw[13] = firmanteSH.Cuit;

                            dtFirmantesSH.Rows.Add(datarw);
                        }

                        grdFirmantesSH.DataSource = dtFirmantesSH;
                        grdFirmantesSH.DataBind();
                    }

                }
                else
                {

                    var lstFirmantesPJ = firmantesBL.GetFirmantesPJSolicitud(id_personajuridica).ToList();

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
            SSITSolicitudesTitularesPersonasFisicasBL solTitPFBL = new SSITSolicitudesTitularesPersonasFisicasBL();


            var pf = solTitPFBL.Single(id_personafisica);
            var sol = solBL.Single(pf.IdSolicitud);


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

                    SSITSolicitudesFirmantesPersonasFisicasBL solFirPFBL = new SSITSolicitudesFirmantesPersonasFisicasBL();
                    var firmante = solFirPFBL.GetByFKIdPersonaFisica(id_personafisica).FirstOrDefault();

                    if (firmante != null)
                    {
                        txtApellidoFirPF.Text = firmante.Apellido;
                        txtNombresFirPF.Text = firmante.Nombres;
                        txtCuitFirPF.Text = firmante.Cuit;
                        ddlTipoDocumentoFirPF.SelectedValue = firmante.IdTipoDocPersonal.ToString();
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

            if (this.id_tipo_tramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO || this.id_tipo_tramite == (int)Constantes.TipoTramite.AMPLIACION
                    || sol.SSITSolicitudesOrigenDTO != null)
                HabilitarSoloFirmantesABMPF(false);

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
            if (txtCuitPJ.Text.Trim() != "")
            {
                LimpiarControlesFirPJ();
                this.EjecutarScript(updbtnShowAgregarFirPJ, "showfrmAgregarFirmantePJ();");
            }
            else
            {
                lblError.Text = "Debe ingresar los datos del Titular";
                this.EjecutarScript(updbtnShowAgregarFirPJ, "showfrmError();");
            }
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
            txtCuitFirPJ.Text = "";
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

                var sol = solBL.Single(id_solicitud);

                if (Validation)
                {
                    dt.Rows.Add(txtApellidosFirPJ.Text.Trim(), txtNombresFirPJ.Text.Trim(), ddlTipoDocumentoFirPJ.SelectedItem.Text.Trim(), txtNroDocumentoFirPJ.Text.Trim(), txtCuitFirPJ.Text.Trim(),
                                ddlTipoCaracterLegalFirPJ.SelectedItem.Text, int.Parse(ddlTipoDocumentoFirPJ.SelectedValue), txtEmailFirPJ.Text.Trim(), int.Parse(ddlTipoCaracterLegalFirPJ.SelectedValue),
                                txtCargoFirPJ.Text.Trim(), dt.Rows.Count);

                    if (Validation)
                        Validation = ((sol.IdTipoTramite == (int)Constantes.TipoTramite.PERMISO && sol.IdTipoExpediente == (int)Constantes.TipoDeExpediente.MusicaCanto)
                                       || ValidarCuit(txtCuitPJ.Text.Trim(), txtCuitFirPJ.Text.Trim(), updgrdFirmantesPJ));
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


                    if (Validation)
                        Validation = ValidarCuit(txtCuitPJ.Text.Trim(), txtCuitFirPJ.Text.Trim(), updgrdFirmantesPJ);
                }


            }

            if (Validation)
            {
                this.EjecutarScript(updgrdFirmantesPJ, "hidefrmAgregarFirmantePJ();");
                grdFirmantesPJ.DataSource = dt;
                grdFirmantesPJ.DataBind();
            }
            updgrdFirmantesPJ.Update();
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
            txtCuitFirPJ.Text = dr["Cuit"].ToString();
            ddlTipoDocumentoFirPJ.SelectedValue = dr["IdTipoDocPersonal"].ToString();
            ddlTipoCaracterLegalFirPJ.SelectedValue = dr["id_tipocaracter"].ToString();

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

                        //SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasBL solTitPJPFBL = new SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasBL();
                        SSITSolicitudesFirmantesPersonasJuridicasBL solFirPJBL = new SSITSolicitudesFirmantesPersonasJuridicasBL();
                        SSITSolicitudesTitularesPersonasJuridicasBL solTitPJBL = new SSITSolicitudesTitularesPersonasJuridicasBL();

                        SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO solTitPJPFDTO = new SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO();
                        SSITSolicitudesFirmantesPersonasJuridicasDTO solFirPJDTO = new SSITSolicitudesFirmantesPersonasJuridicasDTO();
                        SSITSolicitudesTitularesPersonasJuridicasDTO solTitPJDTO = new SSITSolicitudesTitularesPersonasJuridicasDTO();

                        DataTable dtFirmantesSH = dtFirmantesSHCargados();
                        DataTable dtTitularesSH = dtTitularesSHCargados();
                        DataTable dtFirmantes = dtFirmantesCargados();

                        FirmantesSHDTO firmantesSHDTO;
                        TitularesSHDTO titularesSHDTO;
                        SSITSolicitudesFirmantesPersonasJuridicasDTO solFirPJInsertDTO;

                        solTitPJDTO = new SSITSolicitudesTitularesPersonasJuridicasDTO();

                        solTitPJDTO.IdPersonaJuridica = id_personajuridica;
                        solTitPJDTO.IdSolicitud = id_solicitud;
                        solTitPJDTO.IdTipoSociedad = id_tiposociedad;
                        solTitPJDTO.RazonSocial = Razon_Social;
                        solTitPJDTO.CUIT = Cuit;
                        solTitPJDTO.IdTipoiibb = id_tipoiibb;
                        solTitPJDTO.NroIibb = Ingresos_Brutos;
                        solTitPJDTO.Calle = Calle;
                        solTitPJDTO.NroPuerta = Nro_Puerta;
                        solTitPJDTO.Piso = Piso;
                        solTitPJDTO.Depto = Depto;
                        solTitPJDTO.Torre = Torre;
                        solTitPJDTO.IdLocalidad = id_Localidad;
                        solTitPJDTO.CodigoPostal = Codigo_Postal;
                        solTitPJDTO.Telefono = Telefono;
                        solTitPJDTO.Email = Email;
                        solTitPJDTO.CreateUser = userid;
                        solTitPJDTO.CreateDate = DateTime.Now;

                        List<FirmantesSHDTO> lstFirmantesSHDTO = new List<FirmantesSHDTO>();
                        List<TitularesSHDTO> lstTitularesSHDTO = new List<TitularesSHDTO>();
                        List<SSITSolicitudesFirmantesPersonasJuridicasDTO> lstSolFirPJDTO = new List<SSITSolicitudesFirmantesPersonasJuridicasDTO>();

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
                            firmantesSHDTO.Cuit = dr["Cuit"].ToString();
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
                            titularesSHDTO.Cuit = rowTitularSH["Cuit"].ToString();
                            lstTitularesSHDTO.Add(titularesSHDTO);
                        }

                        foreach (DataRow rowFirPerJur in dtFirmantes.Rows)
                        {
                            solFirPJInsertDTO = new SSITSolicitudesFirmantesPersonasJuridicasDTO();
                            solFirPJInsertDTO.IdSolicitud = id_solicitud;
                            solFirPJInsertDTO.IdPersonaJuridica = id_personajuridica;
                            solFirPJInsertDTO.Apellido = rowFirPerJur["Apellidos"].ToString();
                            solFirPJInsertDTO.Nombres = rowFirPerJur["Nombres"].ToString();
                            solFirPJInsertDTO.IdTipoDocPersonal = int.Parse(rowFirPerJur["IdTipoDocPersonal"].ToString());
                            solFirPJInsertDTO.NroDocumento = rowFirPerJur["NroDoc"].ToString();
                            solFirPJInsertDTO.Cuit = rowFirPerJur["Cuit"].ToString();
                            solFirPJInsertDTO.Email = rowFirPerJur["Email"].ToString();
                            solFirPJInsertDTO.IdTipoCaracter = int.Parse(rowFirPerJur["id_tipocaracter"].ToString());
                            solFirPJInsertDTO.CargoFirmantePj = rowFirPerJur["cargo_firmante_pj"].ToString();
                            lstSolFirPJDTO.Add(solFirPJInsertDTO);
                        }


                        solTitPJDTO.firmantesSH = lstFirmantesSHDTO;
                        solTitPJDTO.titularesSH = lstTitularesSHDTO;
                        solTitPJDTO.solFirDTO = lstSolFirPJDTO;

                        var sol = solBL.Single(id_solicitud);
                        if ((sol.IdTipoTramite == (int)Constantes.TipoTramite.PERMISO && sol.IdTipoExpediente == (int)Constantes.TipoDeExpediente.MusicaCanto)
                                || ValidarApoderamiento(solTitPJDTO.CUIT, updBotonesAgregarPJ))
                        {
                            solTitPJBL.Insert(solTitPJDTO);
                            copiarTitulares();
                        }

                    }
                    catch
                    {
                        throw;
                    }


                    CargarDatosTitulares(this.id_solicitud);
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
            string url = "";
            try
            {
                if (grdTitularesHab.Rows.Count == 0)
                    throw new Exception(Errors.SSIT_SOLICITUD_INGRESAR_TITULARES);

                if (hid_return_url.Value.Contains("Editar"))
                {
                    if (this.id_tipo_tramite == (int)Constantes.TipoTramite.AMPLIACION)
                        url = string.Format("~/" + RouteConfig.VISOR_SOLICITUD_AMPLIACION + "{0}", id_solicitud);
                    else if (this.id_tipo_tramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                        url = string.Format("~/" + RouteConfig.VISOR_SOLICITUD_REDISTRIBUCION_USO + "{0}", id_solicitud);
                    else if (this.id_tipo_tramite == (int)Constantes.TipoTramite.PERMISO)
                        url = string.Format("~/" + RouteConfig.VISOR_SOLICITUD_PERMISO_MC + "{0}", id_solicitud);
                    else
                        url = string.Format("~/" + RouteConfig.VISOR_SOLICITUD + "{0}", id_solicitud);

                }
                else
                {
                    if (this.id_tipo_tramite == (int)Constantes.TipoTramite.AMPLIACION)
                        url = string.Format("~/" + RouteConfig.AGREGAR_UBICACION_SOLICITUD_AMPLIACION + "{0}", id_solicitud);
                    else if (this.id_tipo_tramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                        url = string.Format("~/" + RouteConfig.AGREGAR_UBICACION_SOLICITUD_REDISTRIBUCION_USO + "{0}", id_solicitud);
                    else if (this.id_tipo_tramite == (int)Constantes.TipoTramite.PERMISO)
                        url = string.Format("~/" + RouteConfig.AGREGAR_UBICACION_SOLICITUD_PERMISO_MC + "{0}", id_solicitud);
                    else
                        url = string.Format("~/" + RouteConfig.AGREGAR_UBICACION_SOLICITUD + "{0}", id_solicitud);
                }

                Response.Redirect(url);

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
            string url = "";
            try
            {
                if (this.id_tipo_tramite == (int)Constantes.TipoTramite.AMPLIACION)
                    url = string.Format("~/" + RouteConfig.VISOR_SOLICITUD_AMPLIACION + "{0}", id_solicitud);
                else if (this.id_tipo_tramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                    url = string.Format("~/" + RouteConfig.VISOR_SOLICITUD_REDISTRIBUCION_USO + "{0}", id_solicitud);
                else
                    url = string.Format("~/" + RouteConfig.VISOR_SOLICITUD + "{0}", id_solicitud);

                Response.Redirect(url);
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
            txtCuitTitSH.Text = "";
            txtEmailTitSH.Text = "";

            txtApellidosFirSH.Text = "";
            txtNombresFirSH.Text = "";
            ddlTipoDocumentoFirSH.ClearSelection();
            txtNroDocumentoFirSH.Text = "";
            txtCuitFirSH.Text = "";
            txtEmailFirSH.Text = "";

            txtCargoFirSH.Text = "";
            pnlCargoFirmanteSH.Style["display"] = "none";
            ddlTipoCaracterLegalFirSH.ClearSelection();

            optOtraPersonaSH.Checked = false;
            optMismaPersonaSH.Checked = true;
            pnlFirSH.Style["display"] = "none";
            ValExiste_TipoNroDocTitSH.Style["display"] = "none";
        }

        protected void btnAgregarTitularSH_Click(object sender, EventArgs e)
        {
            if (txtCuitPJ.Text.Trim() != "")
            {
                LimpiarDatosABMTitularesSH();
                hid_rowindex_titSH.Value = "";
                updABMTitularesSH.Update();
                this.EjecutarScript(updBotonesAgregarTitularSH, "showfrmAgregarTitularesSH();");
            }
            else
            {
                lblError.Text = "Debe ingresar el cuit del Titular.";
                this.EjecutarScript(updBotonesAgregarTitularSH, "showfrmError();");
            }
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
            bool valido = true;

            DataTable dtTitularesSH = dtTitularesSHCargados();
            DataTable dtFirmantesSH = dtFirmantesSHCargados();

            var sol = solBL.Single(this.id_solicitud);

            string firmanteDe = txtApellidosTitSH.Text.Trim() + " " + txtNombresTitSH.Text.Trim();
            bool Validation = true;
            if (hid_rowindex_titSH.Value.Length == 0)
            {
                Guid rowid_titular = Guid.NewGuid();

                foreach (DataRow dr in dtTitularesSH.Rows)
                {
                    if (dr["TipoDoc"].ToString().Equals(ddlTipoDocumentoTitSH.SelectedItem.Text.Trim()) &&
                        dr["NroDoc"].ToString().Equals(txtNroDocumentoTitSH.Text.Trim()))
                    {
                        ValExiste_TipoNroDocTitSH.Style["display"] = "inline-block";
                        updABMTitularesSH.Update();
                        Validation = false;
                        break;
                    }
                }

                if (Validation)
                {

                    dtTitularesSH.Rows.Add(rowid_titular, txtApellidosTitSH.Text.Trim(), txtNombresTitSH.Text.Trim(), ddlTipoDocumentoTitSH.SelectedItem.Text.Trim(), txtNroDocumentoTitSH.Text.Trim(),
                               txtCuitTitSH.Text.Trim(), int.Parse(ddlTipoDocumentoTitSH.SelectedValue), txtEmailTitSH.Text.Trim());

                    if (optMismaPersonaSH.Checked)
                    {

                        dtFirmantesSH.Rows.Add(firmanteDe, txtApellidosTitSH.Text.Trim(), txtNombresTitSH.Text.Trim(), ddlTipoDocumentoTitSH.SelectedItem.Text.Trim(), txtNroDocumentoTitSH.Text.Trim(),
                                  "Titular", int.Parse(ddlTipoDocumentoTitSH.SelectedValue), 1, string.Empty, txtEmailTitSH.Text.Trim(), Guid.NewGuid(), rowid_titular, optMismaPersonaSH.Checked, txtCuitTitSH.Text.Trim());
                    }
                    else
                    {
                        dtFirmantesSH.Rows.Add(firmanteDe, txtApellidosFirSH.Text.Trim(), txtNombresFirSH.Text.Trim(), ddlTipoDocumentoFirSH.SelectedItem.Text.Trim(), txtNroDocumentoFirSH.Text.Trim(),
                                    ddlTipoCaracterLegalFirSH.SelectedItem.Text, int.Parse(ddlTipoDocumentoFirSH.SelectedValue),
                                    int.Parse(ddlTipoCaracterLegalFirSH.SelectedValue), txtCargoFirSH.Text.Trim(), txtEmailFirSH.Text.Trim(), Guid.NewGuid(), rowid_titular, optMismaPersonaSH.Checked, txtCuitFirSH.Text.Trim());
                        valido = ValidarCuit(txtCuitTitSH.Text.Trim(), txtCuitFirSH.Text.Trim(), updBotonesIngresarTitularesSH);
                    }
                    if (valido)
                        valido = ValidarCuit(txtCuitPJ.Text.Trim(), txtCuitTitSH.Text.Trim(), updBotonesIngresarTitularesSH);
                }

            }
            else
            {
                foreach (DataRow dr in dtTitularesSH.Rows)
                {
                    if (dr["TipoDoc"].ToString().Equals(ddlTipoDocumentoTitSH.SelectedItem.Text.Trim()) &&
                        dr["NroDoc"].ToString().Equals(txtNroDocumentoTitSH.Text.Trim()) &&
                        dr["rowindex"].ToString() != hid_rowindex_titSH.Value)
                    {
                        ValExiste_TipoNroDocTitSH.Style["display"] = "inline-block";
                        updABMTitularesSH.Update();
                        Validation = false;
                        break;
                    }
                }

                if (Validation)
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
                    dtTitularesSH.Rows[rowindex]["Cuit"] = txtCuitTitSH.Text.Trim();
                    dtTitularesSH.Rows[rowindex]["IdTipoDocPersonal"] = int.Parse(ddlTipoDocumentoTitSH.SelectedValue);
                    dtTitularesSH.Rows[rowindex]["email"] = txtEmailTitSH.Text.Trim();



                    if (drFirmantesSH != null && optOtraPersonaSH.Checked)
                    {
                        drFirmantesSH["Apellidos"] = txtApellidosFirSH.Text.Trim();
                        drFirmantesSH["Nombres"] = txtNombresFirSH.Text.Trim();
                        drFirmantesSH["NroDoc"] = txtNroDocumentoFirSH.Text.Trim();
                        drFirmantesSH["IdTipoDocPersonal"] = ddlTipoDocumentoFirSH.SelectedValue;
                        drFirmantesSH["Cuit"] = txtCuitFirSH.Text.Trim();
                        drFirmantesSH["email"] = txtEmailFirSH.Text.Trim();
                        drFirmantesSH["nom_tipocaracter"] = ddlTipoCaracterLegalFirSH.SelectedItem.Text;
                        drFirmantesSH["id_tipocaracter"] = ddlTipoCaracterLegalFirSH.SelectedValue;
                        drFirmantesSH["cargo_firmante"] = txtCargoFirSH.Text.Trim();
                        drFirmantesSH["misma_persona"] = false;

                        valido = ValidarCuit(txtCuitTitSH.Text.Trim(), txtCuitFirSH.Text.Trim(), updBotonesIngresarTitularesSH);

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
                        drFirmantesSH["Cuit"] = txtCuitTitSH.Text.Trim();
                        drFirmantesSH["cargo_firmante"] = string.Empty;
                        drFirmantesSH["misma_persona"] = true;
                    }

                    if (valido)
                        valido = ValidarCuit(txtCuitPJ.Text.Trim(), txtCuitTitSH.Text.Trim(), updBotonesIngresarTitularesSH);
                }
            }

            if (valido)
            {
                grdTitularesSH.DataSource = dtTitularesSH;
                grdTitularesSH.DataBind();

                grdFirmantesSH.DataSource = dtFirmantesSH;
                grdFirmantesSH.DataBind();
            }

            updgrillaTitularesSH.Update();

            if (Validation && valido)
                copiarTitulares();

            if (Validation && valido)
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
            dt.Columns.Add("Cuit", typeof(string));
            dt.Columns.Add("IdTipoDocPersonal", typeof(int));
            dt.Columns.Add("email", typeof(string));
            dt.Columns.Add("rowindex", typeof(int));

            foreach (GridViewRow row in grdTitularesSH.Rows)
            {
                DataRow datarw;
                datarw = dt.NewRow();

                HiddenField hid_id_tipodoc_grdTitularesSH = (HiddenField)grdTitularesSH.Rows[row.RowIndex].Cells[0].FindControl("hid_id_tipodoc_grdTitularesSH");
                HiddenField hid_rowid_grdTitularesSH = (HiddenField)grdTitularesSH.Rows[row.RowIndex].Cells[5].FindControl("hid_rowid_grdTitularesSH");

                datarw[0] = Guid.Parse(hid_rowid_grdTitularesSH.Value);
                datarw[1] = HttpUtility.HtmlDecode(row.Cells[0].Text);
                datarw[2] = HttpUtility.HtmlDecode(row.Cells[1].Text);
                datarw[3] = HttpUtility.HtmlDecode(row.Cells[2].Text);
                datarw[4] = HttpUtility.HtmlDecode(row.Cells[3].Text);
                datarw[5] = HttpUtility.HtmlDecode(row.Cells[4].Text);
                datarw[6] = int.Parse(hid_id_tipodoc_grdTitularesSH.Value);
                datarw[7] = HttpUtility.HtmlDecode(row.Cells[5].Text);
                datarw[8] = row.RowIndex;

                dt.Rows.Add(datarw);

            }

            return dt;
        }

        private DataTable dtFirmantesSHCargados()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FirmanteDe", typeof(string));//0
            dt.Columns.Add("Apellidos", typeof(string));//1
            dt.Columns.Add("Nombres", typeof(string));//2
            dt.Columns.Add("TipoDoc", typeof(string));//3
            dt.Columns.Add("NroDoc", typeof(string));//4            
            dt.Columns.Add("nom_tipocaracter", typeof(string));//5
            dt.Columns.Add("IdTipoDocPersonal", typeof(int));//6
            dt.Columns.Add("id_tipocaracter", typeof(int));//7
            dt.Columns.Add("cargo_firmante", typeof(string));//8
            dt.Columns.Add("email", typeof(string));//9
            dt.Columns.Add("rowid", typeof(Guid));//10
            dt.Columns.Add("rowid_titular", typeof(Guid));//11
            dt.Columns.Add("misma_persona", typeof(bool));//12
            dt.Columns.Add("Cuit", typeof(string));//13


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
                datarw[5] = HttpUtility.HtmlDecode(row.Cells[6].Text);
                datarw[6] = int.Parse(hid_id_tipodoc_grdFirmantesSH.Value);
                datarw[7] = int.Parse(hid_id_caracter_grdFirmantesSH.Value);
                datarw[8] = HttpUtility.HtmlDecode(row.Cells[7].Text).Trim();
                datarw[9] = HttpUtility.HtmlDecode(row.Cells[8].Text).Trim();
                datarw[10] = Guid.Parse(hid_rowid_grdFirmantesSH.Value);
                datarw[11] = Guid.Parse(hid_rowid_titularSH_grdFirmantesSH.Value);
                datarw[12] = Convert.ToBoolean(hid_misma_persona_grdFirmantesSH.Value);
                datarw[13] = HttpUtility.HtmlDecode(row.Cells[5].Text);
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
            ddlTipoDocumentoTitSH.SelectedValue = drTitularesSH["IdTipoDocPersonal"].ToString();
            txtEmailTitSH.Text = drTitularesSH["email"].ToString();
            txtCuitTitSH.Text = drTitularesSH["cuit"].ToString();

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
                    txtCuitFirSH.Text = drFirmantesSH["Cuit"].ToString();
                    ddlTipoDocumentoFirSH.SelectedValue = drFirmantesSH["IdTipoDocPersonal"].ToString();
                    txtEmailFirSH.Text = drFirmantesSH["email"].ToString();
                    ddlTipoCaracterLegalFirSH.SelectedValue = drFirmantesSH["id_tipocaracter"].ToString();
                    txtCargoFirSH.Text = drFirmantesSH["cargo_firmante"].ToString();
                    pnlFirSH.Style["display"] = "block";

                    if (txtCargoFirSH.Text.Length > 0)
                        pnlCargoFirmanteSH.Style["display"] = "block";
                    else
                        pnlCargoFirmanteSH.Style["display"] = "none";
                }

            }

            hid_rowindex_titSH.Value = row.RowIndex.ToString();
            updABMTitularesSH.Update();

            this.EjecutarScript(updgrillaTitularesSH, "showfrmAgregarTitularesSH();");
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

        protected void btnEliminarTitularSH_Click(object sender, EventArgs e)
        {

            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.Parent.Parent;

            this.EjecutarScript(updgrillaTitularesSH, "showfrmConfirmarEliminarFirSH(" + row.RowIndex.ToString() + ");");

        }

        protected void btnConfirmarEliminarFirSH_Click(object sender, EventArgs e)
        {

            int Rowindex = Convert.ToInt32(hid_rowindex_eliminarSH.Value);
            //int rowindex = int.Parse(hid_rowindex_eliminarSH.Value);

            //LinkButton btn = (LinkButton)sender;
            //GridViewRow rowwww = (GridViewRow)btn.Parent.Parent;

            DataTable dtTitularesSH = dtTitularesSHCargados();
            DataTable dtFirmantesSH = dtFirmantesSHCargados();

            Guid rowid_titular = (Guid)dtTitularesSH.Rows[Rowindex]["rowid"];

            dtTitularesSH.Rows.Remove(dtTitularesSH.Rows[Rowindex]);

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

            this.EjecutarScript(updConfirmarEliminarSH, "hidefrmConfirmarEliminarSH();");
            ScriptManager.RegisterStartupScript(updCargarDatos, updCargarDatos.GetType(), "finalizarCarga", "finalizarCarga();", true);
        }

        //copio los titulares a la encomienda cuando el estado de la solicitud esta en Datos Confirmados
        private void copiarTitulares()
        {
            SSITSolicitudesBL solBL = new SSITSolicitudesBL();
            var sol = solBL.Single(id_solicitud);
            if (sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF
                || sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO
                || sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.SUSPEN)
            {
                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                EncomiendaBL blEnc = new EncomiendaBL();
                var listEncomiendas = blEnc.GetByFKIdSolicitud(id_solicitud).Where(x => x.IdEstado == (int)Constantes.Encomienda_Estados.Incompleta
                            || x.IdEstado == (int)Constantes.Encomienda_Estados.Completa);
                if (listEncomiendas != null && listEncomiendas.Count() > 0)
                {
                    var encEnCurso = listEncomiendas.OrderByDescending(x => x.IdEncomienda).First();
                    EncomiendaTitularesPersonasFisicasBL blEncTPF = new EncomiendaTitularesPersonasFisicasBL();
                    EncomiendaFirmantesPersonasFisicasBL blEncFPF = new EncomiendaFirmantesPersonasFisicasBL();
                    EncomiendaTitularesPersonasJuridicasBL blEncTPJ = new EncomiendaTitularesPersonasJuridicasBL();
                    EncomiendaFirmantesPersonasJuridicasBL blEncFPJ = new EncomiendaFirmantesPersonasJuridicasBL();
                    EncomiendaTitularesPersonasJuridicasPersonasFisicasBL blEncTPJPF = new EncomiendaTitularesPersonasJuridicasPersonasFisicasBL();

                    #region borro lo existente
                    var firPF = blEncFPF.GetByFKIdEncomienda(encEnCurso.IdEncomienda);
                    foreach (var tit in firPF)
                        blEncFPF.Delete(tit);

                    var titPF = blEncTPF.GetByFKIdEncomienda(encEnCurso.IdEncomienda);
                    foreach (var tit in titPF)
                        blEncTPF.Delete(tit);

                    var titPJPF = blEncTPJPF.GetByFKIdEncomienda(encEnCurso.IdEncomienda);
                    foreach (var tit in titPJPF)
                        blEncTPJPF.Delete(tit);

                    var firPJ = blEncFPJ.GetByFKIdEncomienda(encEnCurso.IdEncomienda);
                    foreach (var tit in firPJ)
                        blEncFPJ.Delete(tit);

                    var titPJ = blEncTPJ.GetByFKIdEncomienda(encEnCurso.IdEncomienda);
                    foreach (var tit in titPJ)
                        blEncTPJ.Delete(tit);
                    #endregion

                    EncomiendaBL encBL = new EncomiendaBL();
                    encBL.copyTitularesFromEncomienda(id_solicitud, encEnCurso.IdEncomienda, userid);

                }
                //#region EnviarCambio
                //solo envian los cambios a GP cuando se confirmar el tramite
                //if (sol.idTAD != null)
                //{
                //    Functions.enviarParticipantes(sol, false);
                //}
                //#endregion

            }
        }

        private void HabilitarSoloFirmantesABMPF(bool enabled)
        {
            txtApellidosPF.Enabled = enabled;
            txtNombresPF.Enabled = enabled;
            ddlTipoDocumentoPF.Enabled = enabled;
            ddlTipoIngresosBrutosPF.Enabled = enabled;
            txtNroDocumentoPF.Enabled = enabled;
            txtCuitPF.Enabled = enabled;
            txtIngresosBrutosPF.Enabled = enabled;
            txtCallePF.Enabled = enabled;
            txtNroPuertaPF.Enabled = enabled;
            txtPisoPF.Enabled = enabled;
            txtDeptoPF.Enabled = enabled;
            txtTorrePF.Enabled = enabled;
            txtCPPF.Enabled = enabled;
            txtTelefonoPF.Enabled = enabled;
            txtTelefonoMovilPF.Enabled = enabled;
            ddlLocalidadPF.Enabled = enabled;
            ddlProvinciaPF.Enabled = enabled;
            txtEmailPF.Enabled = enabled;

        }

        private void HabilitarSoloFirmantesABMPJ(bool enabled)
        {
            txtRazonSocialPJ.Enabled = enabled;
            txtCuitPJ.Enabled = enabled;
            txtIngresosBrutosPJ.Enabled = enabled;
            txtCallePJ.Enabled = enabled;
            txtNroPuertaPJ.Enabled = enabled;
            txtPisoPJ.Enabled = enabled;
            txtDeptoPJ.Enabled = enabled;
            txtTorrePJ.Enabled = enabled;
            txtCPPJ.Enabled = enabled;
            txtTelefonoPJ.Enabled = enabled;
            txtCargoFirPJ.Enabled = enabled;
            txtEmailPJ.Enabled = enabled;
            ddlTipoSociedadPJ.Enabled = enabled;
            ddlTipoIngresosBrutosPJ.Enabled = enabled;
            ddlLocalidadPJ.Enabled = enabled;
            ddlProvinciaPJ.Enabled = enabled;

            btnAgregarTitularSH.Visible = enabled;
            txtApellidosTitSH.Enabled = enabled;
            txtNombresTitSH.Enabled = enabled;
            ddlTipoDocumentoTitSH.Enabled = enabled;
            txtNroDocumentoTitSH.Enabled = enabled;
            txtEmailTitSH.Enabled = enabled;

            foreach (GridViewRow row in grdTitularesSH.Rows)
            {
                LinkButton btnEliminarTitularSH = (LinkButton)row.FindControl("btnEliminarTitularSH");
                btnEliminarTitularSH.Visible = false;
            }

        }

        private bool ValidarCuit(string cuitTitular, string cuitFirmante, UpdatePanel updPanel)
        {
            bool resul = true;
            bool evaluar = true;
            try
            {
                ParametrosBL parametrosBL = new ParametrosBL();
                evaluar = Convert.ToBoolean(parametrosBL.GetParametroChar("SSIT.Evaluar.Titulares.AGIP"));
                if (evaluar)
                {
                    //VERSION VIEJA: USANDO SOAP DE AGIP
                    //var r = Functions.isCuitsRelacionados(cuitFirmante, true, cuitTitular, true, (Guid)Membership.GetUser().ProviderUserKey);
                    //if (r.statusCode == 306)
                    //{
                    //    lblError.Text = r.status + "- Debe volver a iniciar sesión.";
                    //    this.EjecutarScript(updPanel, "showfrmError();");
                    //    resul = false;
                    //}
                    //else if (!r.result.msg)
                    //{
                    //    string value = parametrosBL.GetParametroChar("AGIP.ERROR1.URL");
                    //    string url = " Para esto deberá gestionar el apoderamiento en AGIP contando con Clave Ciudad Nivel 2 según corresponda. Para mas informacion ver: <a href='" + value + "'>TAD</a>";
                    //    resul = false;
                    //    lblError.Text = string.Format("Los Cuits no se encuentran relacionados en el servicio de AGIP - {0}", url);
                    //    this.EjecutarScript(updPanel, "showfrmError();");
                    //}

                    //VERSION NUEVA: USANDO TOKEN JWT (PODERDANTES)
                    //AuthenticateAGIPProc authenticateAGIPProc = new AuthenticateAGIPProc();
                    //Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                    //string tokenJWT = authenticateAGIPProc.GetTokenTAD(userid);
                    //LogError.Write(new Exception($"token TAD, {tokenJWT} + userid + {userid}"));
                    //if (!tokenJWT.IsNullOrWhiteSpace() && !tokenJWT.Contains("expirado"))
                    //{
                    //    var r = Functions.isCuitsRelacionadosJWT(cuitFirmante, evaluar, cuitTitular, tokenJWT);
                    //    //revisar que error y status code ponerles
                    //    if (r.statusCode == 306)
                    //    {
                    //        lblError.Text = r.status + "- Debe volver a iniciar sesión.";
                    //        this.EjecutarScript(updPanel, "showfrmError();");
                    //        resul = false;
                    //    }
                    //    else if (!r.result.msg)
                    //    {
                    //        string value = parametrosBL.GetParametroChar("AGIP.ERROR1.URL");
                    //        string url = " Para esto deberá gestionar el apoderamiento en AGIP contando con Clave Ciudad Nivel 2 según corresponda. Para mas informacion ver: <a href='" + value + "'>TAD</a>";
                    //        resul = false;
                    //        lblError.Text = string.Format("Los Cuits no se encuentran relacionados en el servicio de AGIP - {0}", url);
                    //        this.EjecutarScript(updPanel, "showfrmError();");
                    //    }
                    //}
                    //else
                    //{
                    //    LogError.Write(new Exception("No tiene token de TAD, no estaba logeado en TAD/miba"));
                    //    lblError.Text = "- Debe volver a iniciar sesión en TAD/MIBA.";
                    //    this.EjecutarScript(updPanel, "showfrmError();");
                    //    resul = false;
                    //}

                    //VERSION NUEVA: USANDO SERVICIO REST DE "ISREPRESENTANTELEGAL" DE AGIP
                    var r = Functions.isCuitsRelacionadosRest(cuitFirmante, cuitTitular);
                    if (r.statusCode == 306)
                    {
                        lblError.Text = r.message + " - Debe volver a iniciar sesión.";
                        this.EjecutarScript(updPanel, "showfrmError();");
                        resul = false;
                    }
                    else if (r.success ?? false)
                    {
                        string value = parametrosBL.GetParametroChar("AGIP.ERROR1.URL");
                        string url = " Para esto deberá gestionar el apoderamiento en AGIP contando con Clave Ciudad Nivel 2 según corresponda. Para mas informacion ver: <a href='" + value + "'>TAD</a>";
                        resul = false;
                        lblError.Text = string.Format("Los Cuits no se encuentran relacionados en el servicio de AGIP - {0}", url);
                        this.EjecutarScript(updPanel, "showfrmError();");
                    }
                }
            }
            catch (Exception ex)
            {
                resul = false;
                lblError.Text = "Error en el servicio de verificación de cuits: " + ex.Message;
                LogError.Write(new Exception(lblError.Text));
                this.EjecutarScript(updPanel, "showfrmError();");
            }
            return resul;
        }

        private bool ValidarApoderamiento(string cuitTitular, UpdatePanel updPanel)
        {
            bool resul = true;
            bool evaluar = true;
            try
            {
                ParametrosBL parametrosBL = new ParametrosBL();
                UsuarioBL userBL = new UsuarioBL();
                Guid userId = (Guid)Membership.GetUser().ProviderUserKey;
                var apoderado = userBL.Single(userId);
                evaluar = Convert.ToBoolean(parametrosBL.GetParametroChar("SSIT.Evaluar.Titulares.TAD"));

                if (evaluar)
                {
                    if (apoderado != null && cuitTitular != apoderado.CUIT)
                    {
                        var r = Functions.validarApoderamiento(cuitTitular, apoderado.CUIT);
                        resul = r.relacion;
                        if (!r.relacion)
                        {
                            string value = string.Empty;
                            string url = string.Empty;
                            if (r.descripcion.Contains("no se encuentran relacionados"))
                            {
                                value = parametrosBL.GetParametroChar("TAD.ERROR2.URL"); ;
                                url = " Para esto deberá gestionar el apoderamiento en TAD. Para mas informacion ver: <a href='" + value + "'>TAD</a>";
                            }
                            else
                            {
                                value = parametrosBL.GetParametroChar("TAD.ERROR1.URL"); ;
                                url = " Para estar registrado en la plataforma, deberá contar con su Clave Ciudad Nivel 2 e ingresar al portal <a href='" + value + "'>TAD</a>";
                            }

                            lblError.Text = string.Format("{0} - {1}", r.descripcion, url);
                            this.EjecutarScript(updPanel, "showfrmError();");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resul = false;
                LogError.Write(new Exception("Error en el servicio de Apoderamiento: " + ex.Message));
                lblError.Text = "Error en el servicio de Apoderamiento: " + ex.Message;
                this.EjecutarScript(updPanel, "showfrmError();");
                // throw ex;
            }
            return resul;
        }

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

        protected void validarCuitOtroPJButton_Click (object sender, EventArgs e)
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

        private void AutocompleteFormDatosFirmante(PersonaTAD datos)
        {
            txtApellidoFirPF.Text = datos.Apellidos;
            txtNombresFirPF.Text = datos.Nombres;
            txtNroDocumentoFirPF.Text = datos.Documento;
        }

        private void AutoCompleteFormDatosFJ(PersonaTAD datos)
        {
            txtApellidosFirPJ.Text = datos.Apellidos;
            txtNombresFirPJ.Text = datos.Nombres;
            txtNroDocumentoFirPJ.Text = datos.Documento;
            txtEmailFirPJ.Text = datos.Email;
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

        protected void ClearFormDF()
        {
            txtApellidoFirPF.Text = string.Empty;
            txtNombresFirPF.Text = string.Empty;
            txtNroDocumentoFirPF.Text = string.Empty;
        }

        protected void ClearFormNuevoPJ()
        {
            txtApellidosFirPJ.Text = string.Empty;
            txtNombresFirPJ.Text = string.Empty;
            txtNroDocumentoFirPJ.Text = string.Empty;
            txtEmailFirPJ.Text = string.Empty;
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
        private void ValidFromDF()
        {
            txtApellidoFirPF.Enabled = string.IsNullOrWhiteSpace(txtApellidoFirPF.Text);
            txtNombresFirPF.Enabled = string.IsNullOrWhiteSpace(txtNombresFirPF.Text);
            txtNroDocumentoFirPF.Enabled = string.IsNullOrWhiteSpace(txtNroDocumentoFirPF.Text);
        }

        private void ValidFromNuevaFirmantePJ()
        {
            txtApellidosFirPJ.Enabled = string.IsNullOrWhiteSpace(txtApellidosFirPJ.Text);
            txtNombresFirPJ.Enabled = string.IsNullOrWhiteSpace(txtNombresFirPJ.Text);
            txtNroDocumentoFirPJ.Enabled = string.IsNullOrWhiteSpace(txtNroDocumentoFirPJ.Text);
            txtEmailFirPJ.Enabled = string.IsNullOrWhiteSpace(txtEmailFirPJ.Text);
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