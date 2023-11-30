using BusinesLayer.Implementation;
using DataTransferObject;
using ExternalService;
using SSIT.App_Components;
using SSIT.Solicitud.Consulta_Padron.Controls;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static StaticClass.Constantes;

namespace SSIT.Solicitud.Consulta_Padron
{
    public partial class VisorTramite : BasePage
    {

        public int IdSolicitud
        {
            get
            {
                return Convert.ToInt32(Page.RouteData.Values["id_solicitud"]);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                btnBandeja.PostBackUrl = "~/" + RouteConfig.BANDEJA_DE_ENTRADA;
            }
        }

        protected void btnCargarDatostramite_Click(object sender, EventArgs e)
        {
            try
            {
                Cargar();
                ScriptManager.RegisterStartupScript(updCargarDatos, updCargarDatos.GetType(), "finalizarCarga", "finalizarCarga();", true);
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                divbtnConfirmarTramite.Visible = false; //ante cualquier error no permitir confirmar                

                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updCargarDatos, updCargarDatos.GetType(), "finalizarCarga", "finalizarCarga();showfrmError();", true);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Cargar()
        {
            ConsultaPadronSolicitudesBL blEnc = new ConsultaPadronSolicitudesBL();
            ConsultaPadronSolicitudesDTO consultaPadronDTO = blEnc.Single(IdSolicitud);
            ComprobarSolicitud(consultaPadronDTO);
            ActualizaEstadoCompleto(ref consultaPadronDTO);
            CargarDatosTramite(consultaPadronDTO);
        }
        private void ActualizaEstadoCompleto(ref ConsultaPadronSolicitudesDTO consultaPadronDTO)
        {
            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
            ConsultaPadronSolicitudesBL consultaBL = new ConsultaPadronSolicitudesBL();

            lblAlertasSolicitud.Text = consultaBL.ActualizarEstadoConsultaPadron(ref consultaPadronDTO, userid);
            pnlAlertasSolicitud.Visible = !string.IsNullOrEmpty(lblAlertasSolicitud.Text);
        }

        private void ComprobarSolicitud(ConsultaPadronSolicitudesDTO consultaPadronDTO)
        {
            if (consultaPadronDTO != null)
            {
                /*Falta el userID y hacer overload de getuserid con el tipo de tramite*/
                Guid userid_solicitud = (Guid)Membership.GetUser().ProviderUserKey;

                if (userid_solicitud != consultaPadronDTO.CreateUser)
                    Response.Redirect("~/Errores/Error3002.aspx");
            }
            else
                Response.Redirect("~/Errores/Error3004.aspx");
        }
        /// <summary>
        /// 
        /// </summary>
        private void CargarDatosTramite()
        {
            ConsultaPadronSolicitudesBL blEnc = new ConsultaPadronSolicitudesBL();
            ConsultaPadronSolicitudesDTO consultaPadronDTO = blEnc.Single(IdSolicitud);
            CargarDatosTramite(consultaPadronDTO);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="consultaPadronDTO"></param>
        private void CargarDatosTramite(ConsultaPadronSolicitudesDTO consultaPadronDTO)
        {
            //Carga estado solicitud
            lblNroEncomienda.Text = IdSolicitud.ToString();
            lblTipoTramite.Text = consultaPadronDTO.TipoTramite.DescripcionTipoTramite;
            lblEstadoSolicitud.Text = consultaPadronDTO.Estado.NomEstadoUsuario;

            if (!String.IsNullOrWhiteSpace(consultaPadronDTO.NroExpedienteAnterior))
                lblNroExpediente.Text = consultaPadronDTO.NroExpedienteAnterior;

            if (String.IsNullOrWhiteSpace(consultaPadronDTO.NroExpedienteAnterior) || String.IsNullOrWhiteSpace(consultaPadronDTO.Observaciones))
            {
                visDatosSolicitud.Visible = true;
                visDatosSolicitud.btnGuardarVisor.Style["display"] = "block";
                visDatosSolicitud.botonesDeVisor();
            }
            else
            {
                visDatosSolicitud.Visible = false;
            }

            #region Ubicacion
            visUbicaciones.Editable = false;
            visUbicaciones.CargarDatos(consultaPadronDTO);
            btnModificarUbicacion.PostBackUrl = string.Format("~/" + RouteConfig.EDITAR_UBICACION_CPADRON + "{0}", IdSolicitud);
            #endregion

            #region DatosLocal
            visDatoslocal.CargarDatos(consultaPadronDTO);
            btnModificarDatosLocal.PostBackUrl = string.Format("~/" + RouteConfig.EDITAR_DATOSLOCAL_CPADRON + "{0}", IdSolicitud);
            #endregion

            #region Datos de la Solicitud
            visDatosSolicitud.CargarDatos(consultaPadronDTO);
            #endregion

            #region Rubros
            visRubros.CargarDatos(consultaPadronDTO);
            btnModificarRubros.PostBackUrl = string.Format("~/" + RouteConfig.EDITAR_RUBROS_CPADRON + "{0}", IdSolicitud);
            #endregion

            #region Observaciones
            CargarObservaciones(consultaPadronDTO);
            #endregion

            #region Titulares Habilitacion
            visTitularesHab.CargarDatos(consultaPadronDTO);
            btnModificarTitulares.PostBackUrl = string.Format("~/" + RouteConfig.EDITAR_TITULARES_CPADRON + "{0}", IdSolicitud);
            #endregion

            #region Titulares de la solicitud
            TitularesSol.CargarDatos(consultaPadronDTO);
            btnModifTitularesSolicitud.PostBackUrl = string.Format("~/" + RouteConfig.EDITAR_TITULARES_SOLICITUD_CPADRON + "{0}", IdSolicitud);
            #endregion

            #region Documentos
            CargarDocumentos(consultaPadronDTO);
            btnModifDocumentos.PostBackUrl = string.Format("~/" + RouteConfig.EDITAR_DOCUMENTOS_CPADRON + "{0}", IdSolicitud);
            #endregion

            #region Validaciones
            divbtnConfirmarTramite.Visible = false;
            divbtnAnularTramite.Visible = false;

            btnModificarDatosLocal.Visible = false;
            btnModificarTitulares.Visible = false;
            btnModificarRubros.Visible = false;
            btnModificarUbicacion.Visible = false;
            btnModifTitularesSolicitud.Visible = false;
            btnModifDocumentos.Visible = false;

            if (consultaPadronDTO.IdEstado == (int)Constantes.Encomienda_Estados.Completa ||
                consultaPadronDTO.IdEstado == (int)Constantes.Encomienda_Estados.Incompleta)
            {

                if (consultaPadronDTO.IdEstado == (int)Constantes.Encomienda_Estados.Completa)
                    divbtnConfirmarTramite.Visible = true;

                divbtnAnularTramite.Visible = true;

                btnModificarDatosLocal.Visible = true;
                btnModificarTitulares.Visible = true;
                btnModificarRubros.Visible = true;
                btnModificarUbicacion.Visible = true;
                btnModifTitularesSolicitud.Visible = true;
                btnModifDocumentos.Visible = true;
            }
            #endregion

            if (consultaPadronDTO.IdEstado == (int)Constantes.Encomienda_Estados.Anulada)
            {
                lblAlertasSolicitud.Text = "El tramite se encuentra Anulado.";
                pnlAlertasSolicitud.Visible = true;
            }

            updCargarDatos.Update();
            updEstadoSolicitud.Update();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="consultaPadron"></param>
        private void CargarObservaciones(ConsultaPadronSolicitudesDTO consultaPadron)
        {
            gridObservaciones.DataSource = consultaPadron.ObservacionesTareas;
            gridObservaciones.DataBind();
            if (gridObservaciones.Rows.Count == 0)
                box_observacion.Style["display"] = "none";

            hid_mostrar_observaciones.Value = (gridObservaciones.Rows.Count > 0).ToString();
        }

        protected void visDatosSolicitud_EventActualizarEstadoVisor(object sender, ucDatosSolicitud.ucActualizarEstadoVisorEventsArgs e)
        {
            ConsultaPadronSolicitudesBL consultaPadronBL = new ConsultaPadronSolicitudesBL();
            var consultaPadron = consultaPadronBL.Single(IdSolicitud);
            ActualizaEstadoCompleto(ref consultaPadron);
            lblNroExpediente.Text = e.NroExpediente;
            updEstadoSolicitud.Update();
            updCargarDatos.Update();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="consultaPadron"></param>
        private void CargarDocumentos(ConsultaPadronSolicitudesDTO consultaPadron)
        {
            repeater_certificados.DataSource = consultaPadron.DocumentosAdjuntosConInformeFinTramite;
            repeater_certificados.DataBind();
        }

        public void SubirCPadronPDF(int id_cpadron)
        {
            ExternalServiceFiles esf = new ExternalServiceFiles();
            ConsultaPadronDocumentosAdjuntosBL cpadronDocAdjBL = new ConsultaPadronDocumentosAdjuntosBL();
            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
            byte[] pdfCPadron = new byte[0];

            ExternalServiceReporting reportingService = new ExternalServiceReporting();
            var ReportingEntity = reportingService.GetPDFCPadron(id_cpadron, true);

            string arch = ReportingEntity.FileName;
            pdfCPadron = ReportingEntity.Reporte;
            int id_file = ReportingEntity.Id_file;

            ConsultaPadronDocumentosAdjuntosDTO DocAdjDTO = new ConsultaPadronDocumentosAdjuntosDTO();
            DocAdjDTO.IdConsultaPadron = id_cpadron;
            DocAdjDTO.TipodocumentoRequeridoDetalle = Constantes.TiposDeDocumentosPlancheta.PlanchetaCPadron;
            DocAdjDTO.GeneradoxSistema = true;
            DocAdjDTO.CreateDate = DateTime.Now;
            DocAdjDTO.CreateUser = userid;
            DocAdjDTO.NombreArchivo = arch;
            DocAdjDTO.IdFile = id_file;
            DocAdjDTO.IdTipoDocumentoSistema = (int)Constantes.TiposDeDocumentosSistema.SOLICITUD_CPADRON;

            cpadronDocAdjBL.Insert(DocAdjDTO);
        }

        protected void btnConfirmarTramite_Click(object sender, EventArgs e)
        {
            try
            {
                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                ConsultaPadronSolicitudesBL consultaBL = new ConsultaPadronSolicitudesBL();

                var sol = consultaBL.Single(IdSolicitud);
                if (sol.idTAD != null)
                {
                    enviarCambio(sol);
                    enviarParticipantes(sol);
                }
                consultaBL.ConfirmarTramite(IdSolicitud, userid);

                //Subimos la CPadron a files wiii
                SubirCPadronPDF(IdSolicitud);
                CargarDatosTramite();
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updEstadoSolicitud, updEstadoSolicitud.GetType(), "showfrmError", "showfrmError();", true);
            }
        }

        private void enviarCambio(ConsultaPadronSolicitudesDTO sol)
        {
            ConsultaPadronSolicitudesBL consultaBL = new ConsultaPadronSolicitudesBL();
            ParametrosBL parametrosBL = new ParametrosBL();

            string _urlESB = parametrosBL.GetParametroChar("Url.Service.ESB");
            string trata = parametrosBL.GetParametroChar("Trata.Consulta.Padron");
            string dir = "";

            string _noESB = parametrosBL.GetParametroChar("SSIT.NO.ESB");
            bool.TryParse(_noESB, out bool noESB);

            List<int> lisSol = new List<int>();
            lisSol.Add(sol.IdConsultaPadron);
            foreach (var item in consultaBL.GetDireccionesCpadron(lisSol).ToList())
                dir += item.direccion + " / ";
            if (!noESB)
            {
                try
                {
                    wsTAD.actualizarTramite(_urlESB, sol.idTAD.Value, sol.IdConsultaPadron, sol.NroExpedienteSade, trata, dir);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        private void enviarParticipantes(ConsultaPadronSolicitudesDTO sol)
        {
            MembershipUser usuario = Membership.GetUser();
            ParametrosBL parametrosBL = new ParametrosBL();
            string _urlESB = parametrosBL.GetParametroChar("Url.Service.ESB");
            string trata = parametrosBL.GetParametroChar("Trata.Consulta.Padron");

            var list = wsGP.perfilesPorTrata(_urlESB, trata);
            int idPerfilTit = 0;
            int idPerfilSol = 0;
            foreach (var p in list)
            {
                if (p.nombrePerfil == "TITULAR")
                    idPerfilTit = p.idPerfil;
                else if (p.nombrePerfil == "SOLICITANTE")
                    idPerfilSol = p.idPerfil;
            }
            TitularesBL titularesBL = new TitularesBL();

            var lstTitulares = titularesBL.GetTitularesConsultaPadron(sol.IdConsultaPadron).ToList();
            UsuarioBL usuBL = new UsuarioBL();

            var lstUsu = new List<UsuarioDTO>();
            var usuDTO = usuBL.Single((Guid)usuario.ProviderUserKey);
            lstUsu.Add(usuDTO);

            var lstParticipantesSSIT = (from t in lstTitulares
                                        select new
                                        {
                                            cuit = t.CUIT,
                                            idPerfil = idPerfilTit,
                                            t.RazonSocial,
                                            t.Apellido,
                                            Nombres = t.Nombre
                                        }).Union(
                                        from u in lstUsu
                                        select new
                                        {
                                            cuit = u.UserName,
                                            idPerfil = idPerfilSol,
                                            u.RazonSocial,
                                            u.Apellido,
                                            Nombres = u.Nombre
                                        }).ToList();

            var lstParticipantesGP = wsGP.GetParticipantesxTramite(_urlESB, sol.idTAD.Value).Where(x => x.vigenciaParticipante == true).ToList();

            // Da de alta los que no están en GP
            foreach (var item in lstParticipantesSSIT)
            {
                if (!lstParticipantesGP.Exists(x => x.cuit == item.cuit && x.idPerfil == item.idPerfil))
                    wsGP.nuevoTramiteParticipante(_urlESB, trata, sol.idTAD.Value, sol.NroExpedienteSade,
                        item.cuit, item.idPerfil, item.idPerfil == idPerfilSol, Constantes.Sistema, item.Nombres, item.Apellido, item.RazonSocial);
            }

            var solicitante = lstParticipantesSSIT.FirstOrDefault(x => x.idPerfil == idPerfilSol);

            var solicitanteGP = lstParticipantesGP.FirstOrDefault(x => x.idPerfil == idPerfilSol);

            //esto para arreglar el backlog de error22
            if (solicitanteGP == null)
            {
                Exception ex22 = new Exception(
                    $"Debe tener solicitante para poder tramitar, titular {lstTitulares.FirstOrDefault()}," +
                    $"Solicitud : {sol.IdConsultaPadron}, " +
                    $"idTad : {sol.idTAD}, " +
                    $"usuarioSSIT : {usuDTO.UserName}"
                    );
                LogError.Write(ex22);
                wsGP.nuevoTramiteParticipante(_urlESB, trata, sol.idTAD.Value, sol.NroExpedienteSade,
                usuDTO.CUIT, (int)TipoParticipante.Solicitante, true, Constantes.Sistema,
                usuDTO.Nombre, usuDTO.Apellido, usuDTO.RazonSocial);
                lstParticipantesGP = wsGP.GetParticipantesxTramite(_urlESB, sol.idTAD.Value).ToList();
            }

            // Da de baja los que no están SIPSA
            foreach (var item in lstParticipantesGP)
            {
                if(item.idPerfil != idPerfilSol)
                    if (!lstParticipantesSSIT.Exists(x => x.cuit == item.cuit && x.idPerfil == item.idPerfil))
                        wsGP.DesvincularParticipante(_urlESB, sol.idTAD.Value, solicitante.cuit, solicitante.idPerfil, Constantes.Sistema, item.cuit, item.idPerfil);
            }
        }
        protected void btnAnularTramite_Click(object sender, EventArgs e)
        {
            try
            {
                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                ConsultaPadronSolicitudesBL consultaBL = new ConsultaPadronSolicitudesBL();
                consultaBL.AnularTramite(IdSolicitud, userid);
                ScriptManager.RegisterStartupScript(updEstadoSolicitud, updEstadoSolicitud.GetType(), "hidefrmConfirmarAnulacion", "hidefrmConfirmarAnulacion();", true);
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updEstadoSolicitud, updEstadoSolicitud.GetType(), "showfrmError", "showfrmError();", true);

            }
            CargarDatosTramite();
        }

        protected void gridObservaciones_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ConsultaPadronSolicitudesObservacionesDTO row = (ConsultaPadronSolicitudesObservacionesDTO)e.Row.DataItem;

                LinkButton lnkModal = (LinkButton)e.Row.FindControl("lnkModal");
                Panel pnlObservacionModal = (Panel)e.Row.FindControl("pnlObservacionModal");
                lnkModal.Attributes["data-target"] = "#" + pnlObservacionModal.ClientID;

                //si algun registros tiene false, el total termina con false

                if (!row.Leido.Value)
                {
                    lblAlertasSolicitud.Text = "Falta leer observaciones";
                    pnlAlertasSolicitud.Visible = true;
                }
            }
        }
        protected void btnConfirmarObservacion_Command(object sender, CommandEventArgs e)
        {
            try
            {
                int id_cpadron_observacion = Convert.ToInt32(e.CommandArgument);

                ConsultaPadronSolicitudesObservacionesBL observaciones = new ConsultaPadronSolicitudesObservacionesBL();
                observaciones.Leer(id_cpadron_observacion);

                Cargar();

                ScriptManager.RegisterStartupScript(updCargarDatos, updCargarDatos.GetType(), "finalizarCarga", "finalizarCarga();", true);
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updEstadoSolicitud, updEstadoSolicitud.GetType(), "showfrmError", "showfrmError();", true);
            }
        }
    }
}