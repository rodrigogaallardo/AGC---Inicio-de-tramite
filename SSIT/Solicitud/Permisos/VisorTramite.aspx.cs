using BusinesLayer.Implementation;
using DataTransferObject;
using ExternalService;
using ExternalService.ws_interface_AGC;
using SSIT.App_Components;
using SSIT.Common;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SSIT.Solicitud.Permisos.Controls.Tramite_CAA;

namespace SSIT.Solicitud.Permisos
{
    public partial class VisorTramite : SecurePage
    {
        SSITSolicitudesBL blSol = new SSITSolicitudesBL();
        PermisosBL blPermisos = new PermisosBL();
        ParametrosBL blParam = new ParametrosBL();

        private int id_solicitud
        {
            get
            {
                int ret = 0;
                int.TryParse(Convert.ToString(Page.RouteData.Values["id_solicitud"]), out ret);
                return ret;
            }
            set
            {
                hid_id_solicitud.Value = value.ToString();
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager sm = ScriptManager.GetCurrent(this);
            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(updCargarDatos, updCargarDatos.GetType(), "init_Js_updCargarDatos", "init_Js_updCargarDatos();", true);
            }

        }

        private void ComprobarSolicitud(SSITSolicitudesDTO solicitud)
        {
            if (Page.RouteData.Values["id_solicitud"] != null)
            {

                if (solicitud != null)
                {
                    /*Falta el userID y hacer overload de getuserid con el tipo de tramite*/
                    Guid userid_solicitud = Functions.GetUserid();
                    this.id_solicitud = solicitud.IdSolicitud;

                    if (userid_solicitud != solicitud.CreateUser)
                        Response.Redirect("~/Errores/Error3002.aspx");
                }
                else
                    Response.Redirect("~/Errores/Error3004.aspx");
            }
            else
                Response.Redirect("~/Errores/Error3001.aspx");

        }

        protected void btnCargarDatostramite_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dt = DateTime.Now;
                var sol = blSol.Single(id_solicitud);
                ComprobarSolicitud(sol);

                Guid userid = Functions.GetUserid();
                string msgError = blSol.ActualizarEstado(id_solicitud, userid);

                sol = blSol.Single(id_solicitud);
                CargarDatos(sol);

                ScriptManager.RegisterStartupScript(updCargarDatos, updCargarDatos.GetType(), "finalizarCarga", "finalizarCarga();", true);
                if (sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.ANU)
                    lblAlertasSolicitud.Text = "Esta solicitud se encuentra anulada.";

                if (msgError != null || (lblAlertasSolicitud.Text != null && lblAlertasSolicitud.Text != ""))
                {
                    lblError.Text = msgError;
                    if (msgError != null && (lblAlertasSolicitud.Text != null && lblAlertasSolicitud.Text != ""))
                        this.MostrarMensajeAlertas(lblAlertasSolicitud.Text, msgError);
                    else if (msgError != null && msgError != "")
                        this.MostrarMensajeAlertas(msgError);
                    else if (lblAlertasSolicitud.Text != null && lblAlertasSolicitud.Text != "")
                        this.MostrarMensajeAlertas(lblAlertasSolicitud.Text);
                    updEstadoSolicitud.Update();
                }

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                divbtnConfirmarTramite.Visible = false; //ante cualquier error no permitir confirmar
                divbtnImprimirSolicitud.Visible = false;
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updCargarDatos, "finalizarCarga();showfrmError();");
            }
        }

        private void CargarDatos(SSITSolicitudesDTO ssitDTO)
        {

            int id_estado = ssitDTO.IdEstado;

            CargarCabecera(ssitDTO);
            MostrarMensajeAlertaFaltantes();

            bool editable = id_estado == (int)Constantes.TipoEstadoSolicitudEnum.COMP || id_estado == (int)Constantes.TipoEstadoSolicitudEnum.INCOM;

            #region DatosSolicitud
            visDatosSolicitud.Editable = editable;
            visDatosSolicitud.EditableTitulares = editable;

            // Si es una ampliacio/ Redist Uso y proviene de una solicitud SGI no se permite editar
            if (ssitDTO.SSITSolicitudesOrigenDTO != null)
            {
                visDatosSolicitud.Editable = false;
            }
            visDatosSolicitud.EditableExpRel = editable || id_estado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF;

            visDatosSolicitud.CargarDatos(ssitDTO);
            #endregion

            #region Datos Local

            btnModificarDatosLocal.PostBackUrl = string.Format("~/{0}{1}", RouteConfig.EDITAR_DATOSLOCAL_SOLICITUD_PERMISO_MC, ssitDTO.IdSolicitud);
            btnModificarDatosLocal.Visible = editable;
            visDatosLocal.CargarDatos(ssitDTO);

            #endregion

            #region Rubros

            btnModificarRubros.PostBackUrl = string.Format("~/{0}{1}", RouteConfig.EDITAR_RUBROS_SOLICITUD_PERMISO_MC, ssitDTO.IdSolicitud);
            btnModificarRubros.Visible = editable;
            visRubros.CargarDatos(ssitDTO);

            #endregion

            #region DatosCAAyRAC
            visTramite_CAA.Enabled = editable;
            visTramite_CAA.Cargar_Datos(ssitDTO);
            #endregion

            #region Presentacion
            visPresentacion.CargarDatos(ssitDTO);
            #endregion

            #region Solicitud


            if (ssitDTO.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.APRO)
            {
                RegenerarSolicitud(id_solicitud);
                divbtnImprimirSolicitud.Visible = true;
            }
            #endregion

            updCargarDatos.Update();

        }

        private void CargarCabecera(SSITSolicitudesDTO sol)
        {

            int id_estado = sol.IdEstado;
            lblNroSolicitud.Text = sol.IdSolicitud.ToString();
            lblTipoTramite.Text = sol.TipoTramiteDescripcion + " " + sol.TipoExpedienteDescripcion;

            EngineBL engBL = new EngineBL();
            string descripcionCircuito = engBL.GetDescripcionCircuito(id_solicitud);

            if (descripcionCircuito != null)
                lblTipoTramite.Text += " - " + descripcionCircuito;

            lblEstadoSolicitud.Text = sol.TipoEstadoSolicitudDTO.Descripcion;
            if (sol.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && sol.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
            {
                lblCodigoDeSeguridad.Text = sol.CodigoSeguridad;
                divCodigoSeguridad.Style["display"] = "block";
            }

            if (sol.SSITSolicitudesOrigenDTO != null)
                lblHabAnterior.Text = sol.SSITSolicitudesOrigenDTO.id_solicitud_origen.ToString();

            updEstadoSolicitud.Update();

            #region botones
            btnBandeja.PostBackUrl = "~/" + RouteConfig.BANDEJA_DE_ENTRADA;
            divbtnConfirmarTramite.Visible = false;
            if (id_estado == (int)Constantes.TipoEstadoSolicitudEnum.COMP)
                divbtnConfirmarTramite.Visible = true;
            divbtnPresentarTramite.Visible = false;
            if (id_estado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF ||
                id_estado == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO ||
                (id_estado == (int)Constantes.TipoEstadoSolicitudEnum.SUSPEN))
                divbtnPresentarTramite.Visible = true;
            divbtnAnularTramite.Visible = false;
            if (id_estado == (int)Constantes.TipoEstadoSolicitudEnum.INCOM ||
                id_estado == (int)Constantes.TipoEstadoSolicitudEnum.COMP ||
                id_estado == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO ||
                id_estado == (int)Constantes.TipoEstadoSolicitudEnum.SUSPEN ||
                id_estado == (int)Constantes.TipoEstadoSolicitudEnum.ETRA ||
                id_estado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF)
                divbtnAnularTramite.Visible = true;

            divbtnImprimirSolicitud.Visible = false;

            #endregion

            updEstadoSolicitud.Update();
        }

        private void MostrarMensajeAlertas(params string[] mensajes)
        {
            string alerta = "";

            for (int iii = 0; iii < mensajes.Length; iii++)
            {
                if (!string.IsNullOrEmpty(mensajes[iii]))
                {
                    alerta = string.IsNullOrEmpty(alerta) ? mensajes[iii] : alerta + "<br>" + mensajes[iii];
                }
            }

            lblAlertasSolicitud.Text = System.Web.HttpUtility.HtmlEncode(alerta);
            pnlAlertasSolicitud.Visible = (alerta.Length > 0);
        }

        protected void btnConfirmarTramite_Click(object sender, EventArgs e)
        {
            Guid userid = Functions.GetUserid();
            try
            {
                SSITSolicitudesDTO solic = blSol.Single(this.id_solicitud);
                var dtoRAC = visTramite_CAA.GenerarRAC(this.id_solicitud);
                //var dtoRAC = new DtoRACGenerado();
                //if (solic.IdTipoTramite == (int)Constantes.TipoDeTramite.Permisos && solic.IdTipoExpediente == (int)Constantes.TipoDeExpediente.MusicaCanto)
                //{
                //    dtoRAC = null;
                //}
                //else
                //{
                //    dtoRAC = visTramite_CAA.GenerarRAC(this.id_solicitud);
                //}

                if (dtoRAC != null)
                {
                    SSITSolicitudesDTO solDTO = blSol.Single(this.id_solicitud);
                    solDTO.SSITPermisosDatosAdicionalesDTO = new SSITPermisosDatosAdicionalesDTO
                    {
                        IdSolicitud = this.id_solicitud,
                        id_caa = dtoRAC.id_caa,
                        id_solicitud_caa = dtoRAC.id_solicitud_caa,
                        id_rac = dtoRAC.id_rac,
                        id_form_rac = dtoRAC.id_form_rac,
                        CreateDate = DateTime.Now,
                        CreateUser = userid,
                    };

                    solDTO.IdEstado = (int)Constantes.TipoEstadoSolicitudEnum.ETRA;
                    blPermisos.Update(solDTO);

                    solDTO = blSol.Single(this.id_solicitud);
                    CargarDatos(solDTO);
                }
                else
                {
                    SSITSolicitudesDTO solDTO = blSol.Single(this.id_solicitud);
                    
                    solDTO.IdEstado = (int)Constantes.TipoEstadoSolicitudEnum.ETRA;
                    blPermisos.Update(solDTO);

                    solDTO = blSol.Single(this.id_solicitud);
                    CargarDatos(solDTO);
                }
                RegenerarSolicitud(id_solicitud);
                divbtnImprimirSolicitud.Visible = true;
            }

            catch (Exception ex)
            {
                MostrarMensajeAlertas(Functions.GetErrorMessage(ex));
            }
            this.EjecutarScript(udpConfirmarSolcitud1, "mostrarShortcuts();");

        }
        

        protected void btnAnularTramite_Click(object sender, EventArgs e)
        {
            try
            {
                SSITSolicitudesBL blSol = new SSITSolicitudesBL();
                var sol = blSol.Single(id_solicitud);
                Guid userid = Functions.GetUserid();
                if (blSol.anularSolicitud(id_solicitud, userid))
                {
                    sol = blSol.Single(id_solicitud);
                    CargarCabecera(sol);
                    btnCargarDatostramite_Click(sender, e);
                    ScriptManager.RegisterStartupScript(updEstadoSolicitud, updEstadoSolicitud.GetType(), "hidefrmConfirmarAnulacion2", "hidefrmConfirmarAnulacion2();", true);

                }
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updEstadoSolicitud, updEstadoSolicitud.GetType(), "showfrmError", "showfrmError();", true);
            }
        }

        protected void btnPresentarTramite_Click(object sender, EventArgs e)
        {

            /*
            try
            {
                pnlTramiteIncompleto.Visible = false;
                lblTextoTramiteIncompleto.Text = "";
                SSITSolicitudesBL blSol = new SSITSolicitudesBL();
                EncomiendaRubrosBL encRubrosBL = new EncomiendaRubrosBL();
                EncomiendaBL encBL = new EncomiendaBL();
                var sol = blSol.Single(id_solicitud);

                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                MembershipUser usuario = Membership.GetUser(userid);
                int id_estado_ant = sol.IdEstado;
                byte[] oblea = null;
                string emailUsuario = "";


                int id_encomienda = sol.EncomiendaSSITSolicitudesDTO.Select(x => x.EncomiendaDTO)
                                        .Where(x => x.IdEstado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo)
                                        .OrderByDescending(o => o.IdEncomienda)
                                        .Select(s => s.IdEncomienda)
                                        .FirstOrDefault();

                var lstEncRubros = encRubrosBL.GetRubrosTdoReqByIdEncomienda(id_encomienda);

                if (lstEncRubros.Any() && sol.IdTipoTramite != (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                {
                    grdDocumentosFaltantes.DataSource = lstEncRubros;
                    grdDocumentosFaltantes.DataBind();
                    updDocumentosFaltantes.Update();
                    ScriptManager.RegisterStartupScript(updDocumentosFaltantes, updDocumentosFaltantes.GetType(), "showfrmDocumentosFaltantes", "showfrmDocumentosFaltantes();", true);
                    return;
                }

                if (id_estado_ant == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF || id_estado_ant == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO)
                {
                    ExternalServiceReporting reportingService = new ExternalServiceReporting();
                    var ReportingEntity = reportingService.GetPDFOblea(id_solicitud, false);
                    oblea = ReportingEntity.Reporte;
                    emailUsuario = usuario.Email;
                    if (oblea.Length <= 0)
                    {
                        throw new Exception("No se pudo generar la oblea, intentelo nuevamente.");
                    }
                }
                if (blSol.presentarSolicitud(id_solicitud, userid, oblea, emailUsuario))
                {
                    sol = blSol.Single(id_solicitud);
                    RegenerarSolicitud(id_solicitud);
                    ActualizaTipoSubtipoExpSSIT(id_solicitud);
                    CargarCabecera(sol);
                    visDocumentos.CargarDatos(id_solicitud);

                    updEstadoSolicitud.Update();
                    visDatosSolicitud.CargarDatos(sol);
                    EncomiendaBL encomiendaBL = new EncomiendaBL();
                    var encomiendas = encomiendaBL.GetByFKIdSolicitud(id_solicitud);
                    visPresentacion.CargarDatos(sol, encomiendas);
                }

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                pnlTramiteIncompleto.Visible = true;
                lblTextoTramiteIncompleto.Text = Funciones.GetErrorMessage(ex);
                pnlTramiteIncompleto.Visible = true;

            }

            */
        }



        protected void visTramite_CAA_Error(object sender, SSIT.Solicitud.Permisos.Controls.Tramite_CAA.CAAErrorEventArgs e)
        {
            lblAlertasSolicitud.Text = e.Description;
            pnlAlertasSolicitud.Visible = true;

        }

        private void MostrarMensajeAlertaFaltantes()
        {

            if (lblEstadoSolicitud.Text == "Completo")
            {
                lblTextoTramiteIncompleto.Text = "Usted completó satisfactoriamente el primer paso de ingreso de Titulares, Ubicación, datos del local y Rubros." + Environment.NewLine +
                                                 "Para poder continuar con su trámite, deberá indicar el CAA con el cúal realizó la habilitación.";
                pnlTramiteIncompleto.Visible = true;
            }
            else
            {
                pnlTramiteIncompleto.Visible = false;
            }
        }

        public void RegenerarSolicitud(int id_solicitud)
        {
            ExternalService.Class.ReportingEntity ReportingEntity = new ExternalService.Class.ReportingEntity();
            ExternalServiceFiles esf = new ExternalServiceFiles();
            SSITDocumentosAdjuntosBL ssitDocAdjBL = new SSITDocumentosAdjuntosBL();
            Guid userid = Functions.GetUserid();
            byte[] pdfSolicitud = new byte[0];
            string arch = "PermisoMC-" + id_solicitud.ToString() + ".pdf";


            int id_tipodocsis = 0;
            id_tipodocsis = (int)Constantes.TiposDeDocumentosSistema.PERMISO_MC;

            var DocAdjDTO = ssitDocAdjBL.GetByFKIdSolicitudTipoDocSis(id_solicitud, id_tipodocsis).FirstOrDefault();

            if (DocAdjDTO == null)
            {

                ExternalServiceReporting reportingService = new ExternalServiceReporting();

                ReportingEntity = reportingService.GetPDFPermisoMC(id_solicitud, true);

                pdfSolicitud = ReportingEntity.Reporte;
                int id_file = ReportingEntity.Id_file;


                DocAdjDTO = new SSITDocumentosAdjuntosDTO();
                DocAdjDTO.id_solicitud = id_solicitud;
                DocAdjDTO.id_tdocreq = 0;
                DocAdjDTO.tdocreq_detalle = "";
                DocAdjDTO.generadoxSistema = true;
                DocAdjDTO.CreateDate = DateTime.Now;
                DocAdjDTO.CreateUser = userid;
                DocAdjDTO.nombre_archivo = arch;
                DocAdjDTO.id_file = id_file;
                DocAdjDTO.id_tipodocsis = id_tipodocsis;

                ssitDocAdjBL.Insert(DocAdjDTO, true);


                var sol = blSol.Single(id_solicitud);

                // referencia el archivo de reporte en APRA
                if (sol.SSITPermisosDatosAdicionalesDTO != null)
                {
                    ws_Interface_AGC servicio = new ws_Interface_AGC();
                    ExternalService.ws_interface_AGC.wsResultado ws_resultado = new ExternalService.ws_interface_AGC.wsResultado();


                    servicio.Url = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC");
                    string username_servicio = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.User");
                    string password_servicio = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.Password");

                    if (!servicio.ActualizarFileRACMusicaCanto(username_servicio, password_servicio, sol.SSITPermisosDatosAdicionalesDTO.id_rac, id_file, ref ws_resultado))
                    {
                        throw new Exception(string.Format("Error al intentar escribir el archivo en SIPSA: {0} - {1}", ws_resultado.ErrorCode, ws_resultado.ErrorDescription));
                    }
                }

            }

            btnImprimirSolicitud.NavigateUrl = string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Functions.ConvertToBase64String(DocAdjDTO.id_file));
        }

    }
}
