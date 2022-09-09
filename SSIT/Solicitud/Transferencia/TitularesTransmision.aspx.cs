﻿using BusinesLayer.Implementation;
using DataTransferObject;
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
    public partial class TitularesTransmision : BasePage
    {
        public int IdSolicitud
        {
            get
            {
                TransferenciasSolicitudesBL bl = new TransferenciasSolicitudesBL();
                return bl.Single(IdTransferencia).IdConsultaPadron;
            }
        }

        public int IdTransferencia
        {
            get
            {
                return Convert.ToInt32(Page.RouteData.Values["id_solicitud"]);
            }
        }

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
                ScriptManager.RegisterStartupScript(updAgregarPersonaJuridica, updAgregarPersonaJuridica.GetType(), "init_JS_updAgregarPersonaJuridica", "init_JS_updAgregarPersonaJuridica();", true);
                ScriptManager.RegisterStartupScript(upd_ddlTipoIngresosBrutosPJ, upd_ddlTipoIngresosBrutosPJ.GetType(), "init_JS_upd_ddlTipoIngresosBrutosPJ", "init_JS_upd_ddlTipoIngresosBrutosPJ();", true);
                ScriptManager.RegisterStartupScript(upd_txtIngresosBrutosPJ, upd_txtIngresosBrutosPJ.GetType(), "init_JS_upd_txtIngresosBrutosPJ", "init_JS_upd_txtIngresosBrutosPJ();", true);
                ScriptManager.RegisterStartupScript(updLocalidadPJ, updLocalidadPJ.GetType(), "init_JS_updLocalidadPJ", "init_JS_updLocalidadPJ();", true);
                ScriptManager.RegisterStartupScript(updProvinciasPJ, updProvinciasPJ.GetType(), "init_JS_updProvinciasPJ", "init_JS_updProvinciasPJ();", true);
                ScriptManager.RegisterStartupScript(upd_ddlTipoSociedadPJ, upd_ddlTipoSociedadPJ.GetType(), "init_JS_upd_ddlTipoSociedadPJ", "init_JS_upd_ddlTipoSociedadPJ();", true);
                ScriptManager.RegisterStartupScript(upd_txtRazonSocialPJ, upd_txtRazonSocialPJ.GetType(), "init_JS_upd_txtRazonSocialPJ", "init_JS_upd_txtRazonSocialPJ();", true);
                ScriptManager.RegisterStartupScript(updABMTitularesSH, updABMTitularesSH.GetType(), "init_Js_updABMTitularesSH", "init_Js_updABMTitularesSH();", true);
                ScriptManager.RegisterStartupScript(updgrillaTitularesSH, updgrillaTitularesSH.GetType(), "init_Js_updgrillaTitularesSH", "init_Js_updgrillaTitularesSH();", true);

            }

            if (!IsPostBack)
            {
                hid_return_url.Value = Request.Url.AbsoluteUri;
                //ComprobarSolicitud();
            }
        }

        protected void grdTitularesHabANT_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            bool editar = updShowAgregarPersonasANT.Visible;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {                
                LinkButton btnEliminar = (LinkButton)e.Row.FindControl("btnEliminarTitularANT");
                LinkButton btnEditar = (LinkButton)e.Row.FindControl("btnEditarTitularANT");

                btnEliminar.Visible = editar;
                btnEditar.Visible = editar;
            }
        }

        protected void btnCargarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                CargarTiposDeSociedades();
                CargarTiposDeDocumentoPersonal();
                CargarTiposDeIngresosBrutos();
                CargarProvincias();
                CargarDatosTitulares(IdSolicitud);
                CargarDatosTitularesCP(IdSolicitud);


                updAgregarPersonaFisica.Update();
                updABMTitularesSH.Update();
                this.EjecutarScript(updCargarDatos, "finalizarCarga();");
                if (hid_return_url.Value.Contains("Editar"))
                {
                    //btnVolver.Style["display"] = "inline";
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

        public void CargarDatosTitularesCP(int IdSolicitud)
        {
            TitularesBL titularesBL = new TitularesBL();
            var lstTitulares = titularesBL.GetTitularesConsultaPadron(IdSolicitud);

            if(lstTitulares != null)
            {
                updShowAgregarPersonasANT.Visible = false;
            }

            grdTitularesHabANT.DataSource = lstTitulares;
            grdTitularesHabANT.DataBind();

            updGrillaTitularesANT.Update();
        }
        public void CargarDatosTitulares(int IdSolicitud)
        {
            TitularesBL titularesBL = new TitularesBL();
            var lstTitulares = titularesBL.GetTitularesConsultaPadronSolicitud(IdSolicitud);

            grdTitularesHab.DataSource = lstTitulares;
            grdTitularesHab.DataBind();

            updGrillaTitulares.Update();
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
            var lstProvincias = provinciaBL.GetProvincias().OrderBy(p => p.Nombre);

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

            ddlTipoDocumentoTitSH.DataSource = lstTipoDocPersonal;
            ddlTipoDocumentoTitSH.DataTextField = "Nombre";
            ddlTipoDocumentoTitSH.DataValueField = "TipoDocumentoPersonalId";
            ddlTipoDocumentoTitSH.DataBind();
            ddlTipoDocumentoTitSH.Items.Insert(0, string.Empty);
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
            txtSmsPF.Text = "";
            txtEmailPF.Text = "";
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


                    grdTitularesSH.DataSource = dtTitularesSHCargados();
                    grdTitularesSH.DataBind();

                }
                else
                {
                    lblRazonSocialPJ.Text = "Razon Social (*):";
                    pnlAgregarTitularSH.Style["display"] = "none";
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
            bool existeTitular;
            int id_personafisica = 0;
            int.TryParse(hid_id_titular_pf.Value, out id_personafisica);
            int TipoTitular = 0;
            int.TryParse(hid_tipo_titular.Value, out TipoTitular);

            if (TipoTitular == 0)
            {
                //Valida si existe una persona física ya ingresada con el mismo numero de CUIT.
                ConsultaPadronTitularesSolicitudPersonasFisicasBL consultaPadronTitularesSolicitudPersonasFisicasBL = new ConsultaPadronTitularesSolicitudPersonasFisicasBL();

                existeTitular = consultaPadronTitularesSolicitudPersonasFisicasBL.GetByIdConsultaPadronCuitIdPersonaFisica(IdSolicitud, txtCuitPF.Text.Trim(), id_personafisica).Any();
            }
            else
            {
                ConsultaPadronTitularesPersonasFisicasBL consultaPadronTitularesSolicitudPersonasFisicasBL = new ConsultaPadronTitularesPersonasFisicasBL();

                existeTitular = consultaPadronTitularesSolicitudPersonasFisicasBL.GetByIdConsultaPadronCuitIdPersonaFisica(IdSolicitud, txtCuitPF.Text.Trim(), id_personafisica).Any();
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
            bool existeTitular = false;
            int id_personajuridica = 0;
            int.TryParse(hid_id_titular_pj.Value, out id_personajuridica);
            int TipoTitular = 0;
            int.TryParse(hid_tipo_titular.Value, out TipoTitular);
            if (TipoTitular == 0)
            {
                ConsultaPadronTitularesSolicitudPersonasJuridicasBL consultaPadronTitularesSolicitudPersonasJuridicasBL = new ConsultaPadronTitularesSolicitudPersonasJuridicasBL();
                //Valida si existe una persona física ya ingresada con el mismo numero de CUIT.
                existeTitular = consultaPadronTitularesSolicitudPersonasJuridicasBL.GetByIdConsultaPadronCuitIdPersonaJuridica(this.IdSolicitud, txtCuitPJ.Text.Trim(), id_personajuridica).Any();
            }
            else
            {
                ConsultaPadronTitularesPersonasJuridicasBL consultaPadronTitularesSolicitudPersonasJuridicasBL = new ConsultaPadronTitularesPersonasJuridicasBL();
                //Valida si existe una persona física ya ingresada con el mismo numero de CUIT.
                existeTitular = consultaPadronTitularesSolicitudPersonasJuridicasBL.GetByIdConsultaPadronCuitIdPersonaJuridica(this.IdSolicitud, txtCuitPJ.Text.Trim(), id_personajuridica).Any();
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
                    string Torre = txtTorrePF.Text.Trim();
                    int id_Localidad = 0;
                    string Codigo_Postal = txtCPPF.Text.Trim();
                    string Telefono = txtTelefonoPF.Text.Trim();
                    string TelefonoMovil = txtTelefonoMovilPF.Text.Trim();
                    string Sms = txtSmsPF.Text.Trim();
                    string Email = txtEmailPF.Text.Trim();

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
                            ConsultaPadronTitularesSolicitudPersonasFisicasBL consultaPadronTitularesSolicitudPersonasFisicasBL = new ConsultaPadronTitularesSolicitudPersonasFisicasBL();
                            ConsultaPadronTitularesSolicitudPersonasFisicasDTO encTitPerFisDTO = new ConsultaPadronTitularesSolicitudPersonasFisicasDTO();

                            encTitPerFisDTO.IdPersonaFisica = id_personafisica;
                            encTitPerFisDTO.IdConsultaPadron = IdSolicitud;
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
                            encTitPerFisDTO.Torre = Torre;
                            encTitPerFisDTO.IdLocalidad = id_Localidad;
                            encTitPerFisDTO.CodigoPostal = Codigo_Postal;
                            encTitPerFisDTO.Telefono = Telefono;
                            encTitPerFisDTO.TelefonoMovil = TelefonoMovil;
                            encTitPerFisDTO.Sms = Sms;
                            encTitPerFisDTO.Email = Email.ToLower();
                            encTitPerFisDTO.CreateUser = userid;
                            encTitPerFisDTO.CreateDate = DateTime.Now;

                            consultaPadronTitularesSolicitudPersonasFisicasBL.Insert(encTitPerFisDTO);
                        }
                        else
                        {
                            ConsultaPadronTitularesPersonasFisicasBL consultaPadronTitularesSolicitudPersonasFisicasBL = new ConsultaPadronTitularesPersonasFisicasBL();
                            ConsultaPadronTitularesPersonasFisicasDTO encTitPerFisDTO = new ConsultaPadronTitularesPersonasFisicasDTO();

                            encTitPerFisDTO.IdPersonaFisica = id_personafisica;
                            encTitPerFisDTO.IdConsultaPadron = IdSolicitud;
                            encTitPerFisDTO.Apellido = Apellido;
                            encTitPerFisDTO.Nombres = Nombres;
                            encTitPerFisDTO.IdTipoDocumentoPersonal = id_tipodoc_personal;
                            encTitPerFisDTO.NumeroDocumento = Nro_Documento;
                            encTitPerFisDTO.Cuit = Cuit;
                            encTitPerFisDTO.IdTipoiibb = id_tipoiibb;
                            encTitPerFisDTO.IngresosBrutos = Ingresos_Brutos;
                            encTitPerFisDTO.Calle = Calle;
                            encTitPerFisDTO.NumeroPuerta = Nro_Puerta;
                            encTitPerFisDTO.Piso = Piso;
                            encTitPerFisDTO.Depto = Depto;
                            encTitPerFisDTO.Torre = Torre;
                            encTitPerFisDTO.IdLocalidad = id_Localidad;
                            encTitPerFisDTO.CodigoPostal = Codigo_Postal;
                            encTitPerFisDTO.Telefono = Telefono;
                            encTitPerFisDTO.TelefonoMovil = TelefonoMovil;
                            encTitPerFisDTO.Sms = Sms;
                            encTitPerFisDTO.Email = Email.ToLower();
                            encTitPerFisDTO.CreateUser = userid;
                            encTitPerFisDTO.CreateDate = DateTime.Now;

                            consultaPadronTitularesSolicitudPersonasFisicasBL.Insert(encTitPerFisDTO);
                        }
                    }
                    catch
                    {
                        throw;
                    }

                    CargarDatosTitulares(this.IdSolicitud);
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
                int id_persona = 0;
                string TipoPersona = String.Empty;

                int TipoTitular = 0;
                int.TryParse(hid_tipo_titular.Value, out TipoTitular);

                if(TipoTitular == 0)
                {
                    id_persona = int.Parse(hid_id_persona_eliminar.Value);
                    TipoPersona = hid_tipopersona_eliminar.Value;
                    if (TipoPersona.Equals("PF"))
                    {
                        ConsultaPadronTitularesSolicitudPersonasFisicasBL encFirPerFisBL = new ConsultaPadronTitularesSolicitudPersonasFisicasBL();

                        encFirPerFisBL.Delete(
                            new ConsultaPadronTitularesSolicitudPersonasFisicasDTO
                            {
                                IdPersonaFisica = id_persona,
                                IdConsultaPadron = IdSolicitud
                            });
                    }
                    else
                    {
                        ConsultaPadronTitularesSolicitudPersonasJuridicasBL encTitPerJurBL = new ConsultaPadronTitularesSolicitudPersonasJuridicasBL();

                        encTitPerJurBL.Delete(new ConsultaPadronTitularesSolicitudPersonasJuridicasDTO()
                        {
                            IdPersonaJuridica = id_persona,
                            IdConsultaPadron = IdSolicitud
                        });
                    }
                    CargarDatosTitulares(this.IdSolicitud);
                }
                else
                {
                    id_persona = int.Parse(hid_id_personaANT_eliminar.Value);
                    TipoPersona = hid_tipopersonaANT_eliminar.Value;
                    if (TipoPersona.Equals("PF"))
                    {
                        ConsultaPadronTitularesPersonasFisicasBL encFirPerFisBL = new ConsultaPadronTitularesPersonasFisicasBL();

                        encFirPerFisBL.Delete(
                            new ConsultaPadronTitularesPersonasFisicasDTO
                            {
                                IdPersonaFisica = id_persona,
                                IdConsultaPadron = IdSolicitud
                            });
                    }
                    else
                    {
                        ConsultaPadronTitularesPersonasJuridicasBL encTitPerJurBL = new ConsultaPadronTitularesPersonasJuridicasBL();

                        encTitPerJurBL.Delete(new ConsultaPadronTitularesPersonasJuridicasDTO()
                        {
                            IdPersonaJuridica = id_persona,
                            IdConsultaPadron = IdSolicitud
                        });
                    }

                    CargarDatosTitularesCP(this.IdSolicitud);
                }


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
            ConsultaPadronTitularesSolicitudPersonasJuridicasBL consultaPadronTitularesSolicitudPersonasJuridicasBL = new ConsultaPadronTitularesSolicitudPersonasJuridicasBL();
            LocalidadBL localidadBL = new LocalidadBL();

            var pj = consultaPadronTitularesSolicitudPersonasJuridicasBL.Single(id_personajuridica);

            if (pj != null)
            {
                hid_id_titular_pj.Value = id_personajuridica.ToString();

                ddlTipoSociedadPJ.SelectedValue = pj.IdTipoSociedad.ToString();
                txtRazonSocialPJ.Text = pj.RazonSocial;
                txtCuitPJ.Text = pj.CUIT;
                ddlTipoIngresosBrutosPJ.SelectedValue = pj.IdTipoiibb.ToString();
                txtIngresosBrutosPJ.Text = pj.Numeroiibb;
                txtCallePJ.Text = pj.Calle;
                txtNroPuertaPJ.Text = pj.NumeroPuerta.ToString();
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

                    TitularesBL titularesBL = new TitularesBL();
                    var lstTitularesSH = titularesBL.GetTitularesSolicitudSHConsultaPadron(id_personajuridica);

                    grdTitularesSH.DataSource = lstTitularesSH;
                    grdTitularesSH.DataBind();

                }
            }
        }

        private void CargarDatosTitularPJANT(int id_personajuridica)
        {
            ConsultaPadronTitularesPersonasJuridicasBL consultaPadronTitularesPersonasJuridicasBL = new ConsultaPadronTitularesPersonasJuridicasBL();
            LocalidadBL localidadBL = new LocalidadBL();

            var pj = consultaPadronTitularesPersonasJuridicasBL.Single(id_personajuridica);

            if (pj != null)
            {
                hid_id_titular_pj.Value = id_personajuridica.ToString();

                ddlTipoSociedadPJ.SelectedValue = pj.IdTipoSociedad.ToString();
                txtRazonSocialPJ.Text = pj.RazonSocial;
                txtCuitPJ.Text = pj.CUIT;
                ddlTipoIngresosBrutosPJ.SelectedValue = pj.IdTipoiibb.ToString();
                txtIngresosBrutosPJ.Text = pj.NumeroroIibb;
                txtCallePJ.Text = pj.Calle;
                txtNroPuertaPJ.Text = pj.NumeroPuerta.ToString();
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

                    TitularesBL titularesBL = new TitularesBL();
                    var lstTitularesSH = titularesBL.GetTitularesSolicitudSHConsultaPadron(id_personajuridica);

                    grdTitularesSH.DataSource = lstTitularesSH;
                    grdTitularesSH.DataBind();

                }
            }
        }

        private void CargarDatosTitularPF(int id_personafisica)
        {
            ConsultaPadronTitularesSolicitudPersonasFisicasBL consultaPadronTitularesPersonasFisicasBL = new ConsultaPadronTitularesSolicitudPersonasFisicasBL();
            var pf = consultaPadronTitularesPersonasFisicasBL.Single(id_personafisica);

            if (pf != null)
            {
                txtApellidosPF.Text = pf.Apellido;
                txtNombresPF.Text = pf.Nombres;
                txtNroDocumentoPF.Text = pf.NumeroDocumento.ToString();
                txtCuitPF.Text = pf.CUIT;
                txtIngresosBrutosPF.Text = pf.IngresosBrutos;
                txtCallePF.Text = pf.Calle;
                txtNroPuertaPF.Text = pf.NumeroPuerta.ToString();
                txtPisoPF.Text = pf.Piso;
                txtDeptoPF.Text = pf.Depto;
                txtTorrePJ.Text = (pf.Torre ?? "").Trim();
                txtCPPF.Text = pf.CodigoPostal;
                txtTelefonoPF.Text = pf.Telefono;
                txtTelefonoMovilPF.Text = pf.TelefonoMovil;
                txtSmsPF.Text = pf.Sms;
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

        private void CargarDatosTitularPFANT(int id_personafisica)
        {
            ConsultaPadronTitularesPersonasFisicasBL consultaPadronTitularesPersonasFisicasBL = new ConsultaPadronTitularesPersonasFisicasBL();
            var pf = consultaPadronTitularesPersonasFisicasBL.Single(id_personafisica);

            if (pf != null)
            {
                txtApellidosPF.Text = pf.Apellido;
                txtNombresPF.Text = pf.Nombres;
                txtNroDocumentoPF.Text = pf.NumeroDocumento.ToString();
                txtCuitPF.Text = pf.Cuit;
                txtIngresosBrutosPF.Text = pf.IngresosBrutos;
                txtCallePF.Text = pf.Calle;
                txtNroPuertaPF.Text = pf.NumeroPuerta.ToString();
                txtPisoPF.Text = pf.Piso;
                txtDeptoPF.Text = pf.Depto;
                txtTorrePJ.Text = (pf.Torre ?? "").Trim();
                txtCPPF.Text = pf.CodigoPostal;
                txtTelefonoPF.Text = pf.Telefono;
                txtTelefonoMovilPF.Text = pf.TelefonoMovil;
                txtSmsPF.Text = pf.Sms;
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
            txtTorrePJ.Text = "";
            txtCPPJ.Text = "";
            txtTelefonoPJ.Text = "";
            txtEmailPJ.Text = "";
            ValExiste_TitularPJ.Style["display"] = "none";

            ddlTipoSociedadPJ.ClearSelection();
            ddlProvinciaPJ.ClearSelection();
            ddlTipoIngresosBrutosPJ.ClearSelection();
            CargarLocalidades(ddlProvinciaPJ, ddlLocalidadPJ);

            DataTable dtTitSH = dtTitularesSHCargados();

            dtTitSH.Clear();

            grdTitularesSH.DataSource = dtTitSH;
            grdTitularesSH.DataBind();

            lblRazonSocialPJ.Text = "Razon Social:";
            pnlAgregarTitularSH.Style["display"] = "none";

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
                        int TipoTitular = 0;
                        int.TryParse(hid_tipo_titular.Value, out TipoTitular);
                        if (TipoTitular == 0)
                        {
                            ConsultaPadronTitularesSolicitudPersonasJuridicasBL consultaPadronTitularesPersonasJuridicasBL = new ConsultaPadronTitularesSolicitudPersonasJuridicasBL();
                            ConsultaPadronTitularesSolicitudPersonasJuridicasPersonasFisicasDTO encTitPerJurPerFisDTO = new ConsultaPadronTitularesSolicitudPersonasJuridicasPersonasFisicasDTO();
                            ConsultaPadronTitularesSolicitudPersonasJuridicasDTO encTitPerJurDTO = new ConsultaPadronTitularesSolicitudPersonasJuridicasDTO();

                            if (id_personajuridica > 0)
                            {
                                encTitPerJurDTO = new ConsultaPadronTitularesSolicitudPersonasJuridicasDTO()
                                {
                                    IdPersonaJuridica = id_personajuridica,
                                    IdConsultaPadron = IdSolicitud
                                };
                                consultaPadronTitularesPersonasJuridicasBL.Delete(encTitPerJurDTO);
                            }

                            DataTable dtTitularesSH = dtTitularesSHCargados();
                            TitularesSHDTO titularesSHDTO;

                            encTitPerJurDTO = new ConsultaPadronTitularesSolicitudPersonasJuridicasDTO();

                            encTitPerJurDTO.IdPersonaJuridica = id_personajuridica;
                            encTitPerJurDTO.IdConsultaPadron = IdSolicitud;
                            encTitPerJurDTO.IdTipoSociedad = id_tiposociedad;
                            encTitPerJurDTO.RazonSocial = Razon_Social;
                            encTitPerJurDTO.CUIT = Cuit;
                            encTitPerJurDTO.IdTipoiibb = id_tipoiibb;
                            encTitPerJurDTO.Numeroiibb = Ingresos_Brutos;
                            encTitPerJurDTO.Calle = Calle;
                            encTitPerJurDTO.NumeroPuerta = Nro_Puerta;
                            encTitPerJurDTO.Piso = Piso;
                            encTitPerJurDTO.Depto = Depto;
                            encTitPerJurDTO.Torre = Torre;
                            encTitPerJurDTO.IdLocalidad = id_Localidad;
                            encTitPerJurDTO.CodigoPostal = Codigo_Postal;
                            encTitPerJurDTO.Telefono = Telefono;
                            encTitPerJurDTO.Email = Email;
                            encTitPerJurDTO.CreateUser = userid;
                            encTitPerJurDTO.CreateDate = DateTime.Now;

                            List<TitularesSHDTO> lstTitularesSHDTO = new List<TitularesSHDTO>();

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

                            encTitPerJurDTO.titularesSH = lstTitularesSHDTO;
                            consultaPadronTitularesPersonasJuridicasBL.Insert(encTitPerJurDTO);
                        }
                        else
                        {
                            ConsultaPadronTitularesPersonasJuridicasBL consultaPadronTitularesPersonasJuridicasBL = new ConsultaPadronTitularesPersonasJuridicasBL();
                            ConsultaPadronTitularesPersonasJuridicasPersonasFisicasDTO encTitPerJurPerFisDTO = new ConsultaPadronTitularesPersonasJuridicasPersonasFisicasDTO();
                            ConsultaPadronTitularesPersonasJuridicasDTO encTitPerJurDTO = new ConsultaPadronTitularesPersonasJuridicasDTO();

                            if (id_personajuridica > 0)
                            {
                                encTitPerJurDTO = new ConsultaPadronTitularesPersonasJuridicasDTO()
                                {
                                    IdPersonaJuridica = id_personajuridica,
                                    IdConsultaPadron = IdSolicitud
                                };
                                consultaPadronTitularesPersonasJuridicasBL.Delete(encTitPerJurDTO);
                            }

                            DataTable dtTitularesSH = dtTitularesSHCargados();
                            TitularesSHDTO titularesSHDTO;

                            encTitPerJurDTO = new ConsultaPadronTitularesPersonasJuridicasDTO();

                            encTitPerJurDTO.IdPersonaJuridica = id_personajuridica;
                            encTitPerJurDTO.IdConsultaPadron = IdSolicitud;
                            encTitPerJurDTO.IdTipoSociedad = id_tiposociedad;
                            encTitPerJurDTO.RazonSocial = Razon_Social;
                            encTitPerJurDTO.CUIT = Cuit;
                            encTitPerJurDTO.IdTipoiibb = id_tipoiibb;
                            encTitPerJurDTO.NumeroroIibb = Ingresos_Brutos;
                            encTitPerJurDTO.Calle = Calle;
                            encTitPerJurDTO.NumeroPuerta = Nro_Puerta;
                            encTitPerJurDTO.Piso = Piso;
                            encTitPerJurDTO.Depto = Depto;
                            encTitPerJurDTO.Torre = Torre;
                            encTitPerJurDTO.IdLocalidad = id_Localidad;
                            encTitPerJurDTO.CodigoPostal = Codigo_Postal;
                            encTitPerJurDTO.Telefono = Telefono;
                            encTitPerJurDTO.Email = Email;
                            encTitPerJurDTO.CreateUser = userid;
                            encTitPerJurDTO.CreateDate = DateTime.Now;

                            List<TitularesSHDTO> lstTitularesSHDTO = new List<TitularesSHDTO>();

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

                            encTitPerJurDTO.titularesSH = lstTitularesSHDTO;
                            consultaPadronTitularesPersonasJuridicasBL.Insert(encTitPerJurDTO);
                        }
                    }
                    catch
                    {
                        throw;
                    }


                    CargarDatosTitulares(this.IdSolicitud);
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
            if (grdTitularesHab.Rows.Count == 0 || grdTitularesHabANT.Rows.Count == 0)
            {
                lblError.Text = "No se pudo crear la Consulta al padron.";
                this.EjecutarScript(updBotonesGuardar, "showfrmError();");
            }
            else
            {
                copiarTitularesTransferencia();
                Response.Redirect(string.Format("~/" + RouteConfig.VISOR_TRANSMISIONES + "{0}", IdTransferencia));
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            try
            {
                //Response.Redirect(string.Format("~/" + RouteConfig.VISOR_CPADRON + "{0}", IdSolicitud));
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updBotonesGuardar, "showfrmError();");

            }
        }

        private void copiarTitularesTransferencia()
        {
            try
            {
                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                TransferenciasSolicitudesBL transf = new TransferenciasSolicitudesBL();
                ConsultaPadronTitularesSolicitudPersonasFisicasBL cppfBL = new ConsultaPadronTitularesSolicitudPersonasFisicasBL();
                ConsultaPadronTitularesSolicitudPersonasJuridicasBL cppjBL= new ConsultaPadronTitularesSolicitudPersonasJuridicasBL();
                TransferenciasTitularesPersonasFisicasBL tpfbl = new TransferenciasTitularesPersonasFisicasBL();
                TransferenciasTitularesPersonasJuridicasBL tpjbl = new TransferenciasTitularesPersonasJuridicasBL();

                var transferencia = transf.Single(IdSolicitud);

                var pf = cppfBL.GetByFKIdConsultaPadron(transferencia.IdConsultaPadron);
                foreach (var item in pf)
                {
                    TransferenciasTitularesPersonasFisicasDTO tpfDTO = new TransferenciasTitularesPersonasFisicasDTO();
                    tpfDTO.Apellido = item.Apellido;
                    tpfDTO.Calle = item.Calle;
                    tpfDTO.Celular = item.TelefonoMovil;
                    tpfDTO.CodigoPostal = item.CodigoPostal;
                    tpfDTO.Cuit = item.CUIT;
                    tpfDTO.Depto = item.Depto;
                    tpfDTO.Email = item.Email;
                    tpfDTO.IdLocalidad = item.IdLocalidad;
                    tpfDTO.IdPersonaFisica = item.IdPersonaFisica;
                    tpfDTO.IdSolicitud = IdSolicitud;
                    tpfDTO.Nombres = item.Nombres;
                    tpfDTO.IdTipodocPersonal = item.IdTipoDocumentoPersonal;
                    tpfDTO.NumeroDocumento = item.NumeroDocumento;
                    tpfDTO.IdTipoiibb = item.IdTipoiibb;
                    tpfDTO.IngresosBrutos = item.IngresosBrutos;
                    tpfDTO.NumeroPuerta = item.NumeroPuerta;
                    tpfDTO.Piso = item.Piso;
                    tpfDTO.Torre = item.Torre;
                    tpfDTO.Telefono = item.Telefono;
                    tpfDTO.CreateUser = userid;
                    tpfDTO.CreateDate = DateTime.Now;

                    tpfbl.Insert(tpfDTO);
                }

                var pj = cppjBL.GetByFKIdConsultaPadron(transferencia.IdConsultaPadron);
                foreach (var item in pj)
                {
                    TransferenciasTitularesPersonasJuridicasDTO tpfDTO = new TransferenciasTitularesPersonasJuridicasDTO();
                    TitularesSHDTO titularesSHDTO;

                    tpfDTO.IdPersonaJuridica = item.IdPersonaJuridica;
                    tpfDTO.IdSolicitud = IdSolicitud;
                    tpfDTO.IdTipoSociedad = item.IdTipoSociedad;
                    tpfDTO.RazonSocial = item.RazonSocial;
                    tpfDTO.Cuit = item.CUIT;
                    tpfDTO.IdTipoiibb = item.IdTipoiibb;
                    tpfDTO.NumeroIibb = item.Numeroiibb;
                    tpfDTO.Calle = item.Calle;
                    tpfDTO.NumeroPuerta = item.NumeroPuerta;
                    tpfDTO.Piso = item.Piso;
                    tpfDTO.Depto = item.Depto;
                    tpfDTO.Torre = item.Torre;
                    tpfDTO.IdLocalidad = item.IdLocalidad;
                    tpfDTO.CodigoPostal = item.CodigoPostal;
                    tpfDTO.Telefono = item.Telefono;
                    tpfDTO.Email = item.Email;
                    tpfDTO.CreateUser = userid;
                    tpfDTO.CreateDate = DateTime.Now;

                    List<TitularesSHDTO> lstTitularesSHDTO = new List<TitularesSHDTO>();

                    foreach (var rowTitularSH in item.titularesSH)
                    {
                        titularesSHDTO = new TitularesSHDTO();
                        titularesSHDTO.RowId = rowTitularSH.RowId;
                        titularesSHDTO.Apellidos = rowTitularSH.Apellidos;
                        titularesSHDTO.Nombres = rowTitularSH.Nombres;
                        titularesSHDTO.TipoDoc = rowTitularSH.TipoDoc;
                        titularesSHDTO.NroDoc = rowTitularSH.NroDoc;
                        titularesSHDTO.IdTipoDocPersonal = rowTitularSH.IdTipoDocPersonal;
                        titularesSHDTO.Email = rowTitularSH.Email;
                        lstTitularesSHDTO.Add(titularesSHDTO); ;
                    }

                    tpfDTO.titularesSH = lstTitularesSHDTO;
                    tpjbl.Insert(tpfDTO);
                }
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

        }

        protected void btnAgregarTitularSH_Click(object sender, EventArgs e)
        {
            LimpiarDatosABMTitularesSH();
            hid_rowindex_titSH.Value = "";
            updABMTitularesSH.Update();
            this.EjecutarScript(updBotonesAgregarTitularSH, "showfrmAgregarTitularesSH();");

        }

        protected void btnAceptarTitSH_Click(object sender, EventArgs e)
        {

            DataTable dtTitularesSH = dtTitularesSHCargados();
            string firmanteDe = txtApellidosTitSH.Text.Trim() + " " + txtNombresTitSH.Text.Trim();

            if (hid_rowindex_titSH.Value.Length == 0)
            {
                Guid rowid_titular = Guid.NewGuid();

                dtTitularesSH.Rows.Add(rowid_titular, txtApellidosTitSH.Text.Trim(), txtNombresTitSH.Text.Trim(), ddlTipoDocumentoTitSH.SelectedItem.Text.Trim(), txtNroDocumentoTitSH.Text.Trim(),
                            int.Parse(ddlTipoDocumentoTitSH.SelectedValue), txtEmailTitSH.Text.Trim());


            }
            else
            {
                int rowindex = int.Parse(hid_rowindex_titSH.Value);
                DataRow drTitularesSH = dtTitularesSH.Rows[rowindex];

                dtTitularesSH.Rows[rowindex]["Apellidos"] = txtApellidosTitSH.Text.Trim();
                dtTitularesSH.Rows[rowindex]["Nombres"] = txtNombresTitSH.Text.Trim();
                dtTitularesSH.Rows[rowindex]["TipoDoc"] = ddlTipoDocumentoTitSH.SelectedItem.Text;
                dtTitularesSH.Rows[rowindex]["NroDoc"] = txtNroDocumentoTitSH.Text.Trim();
                dtTitularesSH.Rows[rowindex]["IdTipoDocPersonal"] = int.Parse(ddlTipoDocumentoTitSH.SelectedValue);
                dtTitularesSH.Rows[rowindex]["email"] = txtEmailTitSH.Text.Trim();

            }

            grdTitularesSH.DataSource = dtTitularesSH;
            grdTitularesSH.DataBind();

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

        protected void btnEditarTitularSH_Click(object sender, EventArgs e)
        {
            LimpiarDatosABMTitularesSH();

            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.Parent.Parent;
            DataTable dtTitularesSH = dtTitularesSHCargados();

            DataRow drTitularesSH = dtTitularesSH.Rows[row.RowIndex];

            txtApellidosTitSH.Text = drTitularesSH["Apellidos"].ToString();
            txtNombresTitSH.Text = drTitularesSH["Nombres"].ToString();
            txtNroDocumentoTitSH.Text = drTitularesSH["NroDoc"].ToString();
            ddlTipoDocumentoTitSH.SelectedValue = drTitularesSH["IdTipoDocPersonal"].ToString();
            txtEmailTitSH.Text = drTitularesSH["email"].ToString();

            hid_rowindex_titSH.Value = row.RowIndex.ToString();
            updABMTitularesSH.Update();

            this.EjecutarScript(updgrillaTitularesSH, "showfrmAgregarTitularesSH();");
        }

        protected void btnEliminarTitularSH_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.Parent.Parent;

            DataTable dtTitularesSH = dtTitularesSHCargados();

            Guid rowid_titular = (Guid)dtTitularesSH.Rows[row.RowIndex]["rowid"];

            dtTitularesSH.Rows.Remove(dtTitularesSH.Rows[row.RowIndex]);

            grdTitularesSH.DataSource = dtTitularesSH;
            grdTitularesSH.DataBind();
            updgrillaTitularesSH.Update();
        }
    }
}