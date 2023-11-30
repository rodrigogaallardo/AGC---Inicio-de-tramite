using BusinesLayer.Implementation;
using DataTransferObject;
using ExternalService;
using SSIT.App_Components;
using SSIT.Common;
using SSIT.Solicitud.Controls;
using SSIT.Solicitud.Transferencia.Controls;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static StaticClass.Constantes;

namespace SSIT
{
    public partial class VisorTransmision : BasePage
    {
        public int IdSolicitud
        {
            get
            {
                return Convert.ToInt32(Page.RouteData.Values["id_solicitud"]);
            }
        }

        TransferenciasSolicitudesBL _transferenciaBL = null;
        private TransferenciasSolicitudesBL TransferenciaBL
        {
            get
            {
                if (_transferenciaBL == null)
                    _transferenciaBL = new TransferenciasSolicitudesBL();

                return _transferenciaBL;
            }
        }


        string mensajeAlertaObservacion;
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager sm = ScriptManager.GetCurrent(this);

            if (sm.IsInAsyncPostBack)
            {
            }

            btnModificarTitulares.PostBackUrl = string.Format("~/" + RouteConfig.EDITAR_TRANSFERENCIAS_TITULAR + "{0}", IdSolicitud);
            btnModificarTitularesAnt.PostBackUrl = string.Format("~/" + RouteConfig.EDITAR_TRANSFERENCIAS_TITULAR + "{0}", IdSolicitud);
        }

        private string tieneOblea(int id_solicitud)
        {
            TransferenciasDocumentosAdjuntosBL blDoc = new TransferenciasDocumentosAdjuntosBL();
            var ingresados = blDoc.GetByFKIdSolicitud(id_solicitud);
            foreach (var doc in ingresados)
            {
                if (doc.IdTipoDocsis == (int)StaticClass.Constantes.TiposDeDocumentosSistema.OBLEA_SOLICITUD)
                {
                    string url = string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Functions.ConvertToBase64String(doc.IdFile));
                    return url;
                }
            }
            return "";
        }

        protected void btnCargarDatostramite_Click(object sender, EventArgs e)
        {
            try
            {
                Cargar();
                ScriptManager.RegisterStartupScript(updCargarDatos, updCargarDatos.GetType(), "finalizarCarga", "finalizarCarga();", true);
                updCargarDatos.Update();
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                divbtnConfirmarTramite.Visible = false; //ante cualquier error no permitir confirmar
                //divbtnImprimirSolicitud.Visible = false;

                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updCargarDatos, updCargarDatos.GetType(), "finalizarCarga", "finalizarCarga();", true);
                ScriptManager.RegisterStartupScript(updCargarDatos, updCargarDatos.GetType(), "showfrmError", "showfrmError();", true);
            }
        }
        protected void Cargar()
        {
            var transferencia = TransferenciaBL.Single(IdSolicitud);
            ComprobarSolicitud(transferencia);
            Guid userId = (Guid)Membership.GetUser().ProviderUserKey;
            CargarCombos();
            TransferenciaBL.ActualizarEstadoCompleto(transferencia, userId);
            CargarDatosTramite(transferencia);

        }

        private void ComprobarSolicitud(TransferenciasSolicitudesDTO transferencia)
        {

            if (transferencia != null)
            {
                /*Falta el userID y hacer overload de getuserid con el tipo de tramite*/
                Guid userid_solicitud = (Guid)Membership.GetUser().ProviderUserKey;

                if (userid_solicitud != transferencia.CreateUser)
                    Response.Redirect("~/Errores/Error3002.aspx");
            }
            else
                Response.Redirect("~/Errores/Error3004.aspx");

        }

        private void CargarCombos()
        {

            TiposDeDocumentosRequeridosBL blTipoDoc = new TiposDeDocumentosRequeridosBL();

            List<TiposDeDocumentosRequeridosDTO> lstTiposDocumentos = blTipoDoc.GetVisibleSSITXTipoTramite((int)Constantes.TipoDeTramite.Transferencia).ToList();
            CargaDocumentos.CargarCombo(lstTiposDocumentos);
            updpnlAgregarDocumentos.Update();
        }

        protected void grdPlanos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            TransferenciasSolicitudesBL transferenciasSolicitudesBL = new TransferenciasSolicitudesBL();
            var sol = transferenciasSolicitudesBL.Single(IdSolicitud);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (sol.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM &&
                    sol.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.COMP &&
                    sol.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF)
                {
                    LinkButton lkbeliminar = (LinkButton)e.Row.Cells[3].FindControl("lnkEliminar");
                    lkbeliminar.Visible = false;
                }
            }
        }
        private void MostrarMensajeAlertaFaltantes()
        {

            if (lblEstadoSolicitud.Text == "Completo")
            {
                lblTextoTramiteIncompleto.Text = "Usted completó satisfactoriamente el primer paso de ingreso de Titulares y Ubicación. Para poder continuar con su trámite, deberá contactar a un profesional técnico de la construcción matriculado en CABA. Cuando presione " + " " + '"' + "Confirmar" + '"' + ", se le otorgará un código de seguridad el cual deberá brindarle al profesional para que realice el anexo técnico.";
                pnlTramiteIncompleto.Visible = true;
            }
            else
            {
                pnlTramiteIncompleto.Visible = false;
            }
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
        private void CargarDatosTramite(TransferenciasSolicitudesDTO transferencia)
        {

            ConsultaPadronSolicitudesBL consulta = new ConsultaPadronSolicitudesBL();

            Guid userId = (Guid)Membership.GetUser().ProviderUserKey;

            var consultaPadron = consulta.Single(transferencia.IdConsultaPadron);

            divbtnAnularTramite.Visible = false;
            divbtnImprimirSolicitud.Visible = false;
            divbtnConfirmarTramite.Visible = false;
            divbtnPresentarTramite.Visible = false;
            pnlAgregarDocumentos.Visible = false;
            pnlModifTitulares.Visible = false;
            btnModificarUbicacion.Visible = false;
            string tieneOble = tieneOblea(transferencia.IdSolicitud);
            divbtnOblea.Visible = false;
            if (tieneOble != "")
            {
                btnOblea.NavigateUrl = tieneOble;
                divbtnOblea.Visible = true;
            }
            //Solo se puede imprimir cuando se encuentra confirmado
            if (transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO ||
                transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.ETRA)
            {
                divbtnImprimirSolicitud.Visible = true;
                btnImprimirSolicitud.Visible = true;
                btnImprimirSolicitud.NavigateUrl = string.Format("~/" + RouteConfig.IMPRIMIR_TRANSMISION + "{0}", Functions.ConvertToBase64String(IdSolicitud));
            }

            if (transferencia.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.ETRA &&
                transferencia.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.VALESCR &&
                transferencia.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.ANU)
            {
                divbtnAnularTramite.Visible = true;

            }
            if (transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF ||
                transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO ||
                (transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.SUSPEN && isUltimaTareaCorreccion(transferencia.IdSolicitud)))
            {
                divbtnPresentarTramite.Visible = true;
                pnlAgregarDocumentos.Visible = true;
            }
            if (transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.INCOM
                || transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.COMP
                || transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO
                || transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF
                || (transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.SUSPEN && isUltimaTareaCorreccion(transferencia.IdSolicitud)))
            {
                pnlModifTitulares.Visible = true;
                pnlAgregarDocumentos.Visible = true;
                visUbicaciones.Editable = false;
                // se puede modificar la ubicacion incluso si tiene una solicitud de referencia
                btnModificarUbicacion.Visible = true;
                btnModificarUbicacion.PostBackUrl = string.Format("~/" + RouteConfig.EDITAR_UBICACION_TRANSFERENCIA + "{0}", IdSolicitud);

                /* 
                if (transferencia.idSolicitudRef == null)
                {
                    pnlModifTitularesAnt.Visible = true;
                    btnModificarUbicacion.Visible = true;
                    btnModificarUbicacion.PostBackUrl = string.Format("~/" + RouteConfig.EDITAR_UBICACION_TRANSFERENCIA + "{0}", IdSolicitud);
                }
                else
                {
                    //Para los casos de herencia de datos por transmisión ampliación o RDU es necesario que en caso de existencia de baja de ubicación la misma pueda ser editada.
                    //https://mantis.grupomost.com/view.php?id=166260
                    TransferenciasSolicitudesBL ssitBL = new TransferenciasSolicitudesBL();
                    //SSITSolicitudesUbicacionesDTO ssitUbic = ssitBL.Single(Convert.ToInt32(transferencia.idSolicitudRef)).SSITSolicitudesUbicacionesDTO.FirstOrDefault();
                    TransferenciasSolicitudesDTO transferenciaSolicitudesDTO = ssitBL.Single(Convert.ToInt32(transferencia.IdSolicitud));
                    UbicacionesBL ub = new UbicacionesBL();
                    bool bajaUbicacion = transferenciaSolicitudesDTO.Ubicaciones.Count > 0? ub.Single(Convert.ToInt32(transferenciaSolicitudesDTO.Ubicaciones.FirstOrDefault().IdUbicacion)).BajaLogica : false;

                     if (bajaUbicacion)
                    {
                        btnModificarUbicacion.Visible = true;
                        btnModificarUbicacion.PostBackUrl = string.Format("~/" + RouteConfig.EDITAR_UBICACION_TRANSFERENCIA);
                        btnModificarUbicacion.PostBackUrl = string.Format("~/" + RouteConfig.EDITAR_UBICACION_TRANSFERENCIA + "{0}", IdSolicitud);
                    }
                }
                */
            }

            lblNroSolicitud.Text = transferencia.IdSolicitud.ToString();
            //lblNroEncomienda.Text = transferencia.IdConsultaPadron.ToString();
            lblTipoTramite.Text = transferencia.TipoTramite.DescripcionTipoTramite;
            lblEstadoSolicitud.Text = transferencia.Estado.Descripcion;
            lblTipoTransmision.Text = transferencia.TipoTransmision.nom_tipotransmision;

            if (transferencia.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && transferencia.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
            {
                lblCodigoDeSeguridad.Text = transferencia.CodigoSeguridad;
            }

            if (transferencia.NumeroExpedienteSade != null)
                lblNroExpediente.Text = transferencia.NumeroExpedienteSade;

            if ((transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.COMP) ||
                (transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.INCOM) ||
                (transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF) ||
                (transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.VALESCR) ||
                (transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.ESCREAL) ||
                (transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO))
            {
                btnMostrarAgregadoDocumentos.Visible = true;
                if (transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.COMP)
                    divbtnConfirmarTramite.Visible = true;
            }
            else
            {
                btnMostrarAgregadoDocumentos.Visible = false;
            }
            updEstadoSolicitud.Update();
            #region Ubicacion            
            visUbicaciones.CargarDatos(transferencia);
            #endregion

            #region Rubros
            //visRubros.OcultarZonaSup = true;
            //visRubros.CargarDatos(consultaPadron);
            #endregion

            #region  Titulares de la transferencia
            //Titulares de la transferencia
            Titulares.CargarDatos(transferencia);
            #endregion

            #region  Titulares de la solicitud
            TitularesAnteriores.CargarDatos(transferencia);
            #endregion
            #region Anexo Tecnico
            EncomiendaBL blEnc = new EncomiendaBL();
            var lstEnc = blEnc.GetByFKIdSolicitudTransf(transferencia.IdSolicitud);
            visAnexoTecnico.CargarDatos(lstEnc);
            #endregion
            #region Observaciones
            visPresentacion.CargarDatos(transferencia);
            #endregion

            #region adjuntos
            CargarDocumentos(transferencia, consultaPadron);
            #endregion

            #region PAGOS
            if (BoletaCeroActiva())
            {
                pnlPagos.Visible = false;
                Pagos.Visible = false;
                updBoxPagos.Visible = false;

                liBui.Visible = false;
            }
            else
            {

                Pagos.id_solicitud = IdSolicitud;
                Pagos.tipo_tramite = (int)Constantes.PagosTipoTramite.TR;
                updBoxPagos.Visible = false;
                if ((transferencia.idTipoTransmision == (int)Constantes.TipoTransmision.Transmision_Transferencia) || (transferencia.idTipoTransmision == (int)Constantes.TipoTransmision.Transmision_nominacion))
                {
                    Constantes.BUI_EstadoPago[] arrEstadosPago = new Constantes.BUI_EstadoPago[] { Constantes.BUI_EstadoPago.Pagado/*, Constantes.BUI_EstadoPago.SinPagar*/ };

                    try
                    {
                        validarDocumentos(transferencia);

                        TransferenciaBL.validarEncomienda(IdSolicitud);
                        if (!arrEstadosPago.Contains(Pagos.GetEstadoPago(Constantes.PagosTipoTramite.TR, transferencia.IdSolicitud)))
                            Pagos.HabilitarGeneracionManual = true;
                    }
                    catch (Exception ex)
                    {
                        LogError.Write(ex);
                        MostrarMensajeAlertas(ex.Message);
                    }
                    Pagos.CargarPagos(Constantes.PagosTipoTramite.TR, IdSolicitud);

                    updBoxPagos.Visible = true;
                }
            }

            #endregion

            var trDocAdjDTO = transferencia.Documentos.Any(p => p.IdTipoDocsis == (int)Constantes.TiposDeDocumentosSistema.MANIFIESTO_TRANSMISION);

            //if (transferencia.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF &&
            //    transferencia.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM &&
            //    transferencia.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.COMP &&
            //    transferencia.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.ANU &&
            //    transferencia.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.RECH &&
            //    !trDocAdjDTO)
            //{
            //    RegenerarSolicitud(IdSolicitud);
            //}

            try
            {
                TransferenciaBL.ValidarSolicitud(transferencia);
                pnlAlertasSolicitud.Style["display"] = "none";
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                MostrarMensajeAlertas(ex.Message);
            }
            MostrarMensajeAlertaFaltantes();
            updCargarDatos.Update();
            updEstadoSolicitud.Update();
            updpnlDocumentosAdjuntos.Update();
            updpnlAgregarDocumentos.Update();
        }

        private bool isUltimaTareaCorreccion(int idSolicitud)
        {
            EngineBL blEng = new EngineBL();
            SGITramitesTareasDTO tramite = blEng.GetUltimaTareaTransferencia(idSolicitud);
            if (tramite == null)
                return false;
            if (tramite.IdTarea == (int)Constantes.ENG_Tareas.TR_Correccion_Solicitud)
                return true;
            return false;
        }


        private void validarDocumentos(TransferenciasSolicitudesDTO transferencia)
        {
            //valido documentos
            TransferenciasDocumentosAdjuntosBL trDocAdj = new TransferenciasDocumentosAdjuntosBL();
            List<int> lstDocAdj = trDocAdj.GetByFKIdSolicitud(transferencia.IdSolicitud).Select(x => x.IdTipoDocumentoRequerido).ToList();

            if (transferencia.idTipoTransmision == (int)Constantes.TipoTransmision.Transmision_oficio_judicial &&
                (lstDocAdj == null || !lstDocAdj.Contains(((int)Constantes.TipoDocumentoRequerido.Oficio_Judicial))))
                throw new Exception("Deberá ingresar el Oficio Judicial para continuar con el trámite");
            else if (transferencia.idTipoTransmision == (int)Constantes.TipoTransmision.Transmision_nominacion &&
                (lstDocAdj == null || !lstDocAdj.Contains(((int)Constantes.TipoDocumentoRequerido.Estatuto_Societario))))
                throw new Exception("Deberá ingresar el Estatuto Societario para continuar con el trámite");
            else if (transferencia.idTipoTransmision == (int)Constantes.TipoTransmision.Transmision_Transferencia &&
                (lstDocAdj == null ||
                ((!lstDocAdj.Contains(((int)Constantes.TipoDocumentoRequerido.PublicacionEdicto)) && !lstDocAdj.Contains(((int)Constantes.TipoDocumentoRequerido.Edicto))) ||
                (!lstDocAdj.Contains(((int)Constantes.TipoDocumentoRequerido.Docuemento_Publico_Privado))))))
                throw new Exception("Deberá ingresar la publicación de edictos y el documento público/privado para continuar con el trámite");
            else if ((transferencia.idSolicitudRef == null) &&
                !(lstDocAdj.Contains((int)Constantes.TipoDocumentoRequerido.Habilitacion_Previa_PDF) || lstDocAdj.Contains(((int)Constantes.TipoDocumentoRequerido.Habilitacion_Previa_JPG))))
            {
                throw new Exception("Deberá ingresar la Constancia de Habilitación Previa (Plancheta) para continuar con el trámite");
            }
        }
        private void CargarDocumentos(TransferenciasSolicitudesDTO transferencia, ConsultaPadronSolicitudesDTO consultaPadron)
        {
            var documentos = transferencia.Documentos;
            var doc = consultaPadron.DocumentosAdjuntos.FirstOrDefault(x => x.IdTipoDocumentoSistema == (int)Constantes.TiposDeDocumentosSistema.INFORMES_CPADRON);
            if (doc != null)
            {
                var docDTO = new TransferenciasDocumentosAdjuntosDTO()
                {
                    Id = 0,
                    CreateDate = doc.CreateDate,
                    NombreArchivo = doc.NombreArchivo,
                    IdFile = doc.IdFile,
                    IdSolicitud = transferencia.IdSolicitud,
                    TipoDocumentoRequerido = new TiposDeDocumentosRequeridosDTO
                    {
                        nombre_tdocreq = doc.TiposDeDocumentosSistema.nombre_tipodocsis
                    }
                };
                documentos.Add(docDTO);
            }
            grdPlanos.DataSource = documentos;

            grdPlanos.DataBind();
            pnlDocumentosAdjuntos.Visible = grdPlanos.DataSource != null;

            updpnlDocumentosAdjuntos.Update();
        }

        
        protected void btnPresentarTramite_Click(object sender, EventArgs e)
        {
            try
            {
                var transferencia = TransferenciaBL.Single(IdSolicitud);
                int id_estado_ant = transferencia.IdEstado;
                byte[] oblea = null;
                string emailUsuario = "";

                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                MembershipUser usuario = Membership.GetUser(userid);

                //valido documentos
                validarDocumentos(transferencia);

                //valido pago
                if (BoletaCeroActiva() == false)
                {
                    if ((transferencia.idTipoTransmision == (int)Constantes.TipoTransmision.Transmision_Transferencia) || (transferencia.idTipoTransmision == (int)Constantes.TipoTransmision.Transmision_nominacion))
                    {
                        PagosBoletasBL pagosBoletaBL = new PagosBoletasBL();
                        var lstPagos = pagosBoletaBL.CargarPagos(Constantes.PagosTipoTramite.TR, transferencia.IdSolicitud, null);
                        if (lstPagos == null)
                            throw new Exception(StaticClass.Errors.SSIT_TRANSFERENCIAS_PAGO);

                        //0148737: JADHE 57779 - SSIT - Error al presentar
                        DateTime fecha = new DateTime(2020, 01, 01);
                        if (transferencia.CreateDate > fecha)
                        {
                            if (!lstPagos.Any(p => p.id_estado_pago == (int)Constantes.BUI_EstadoPago.Pagado))
                            {
                                throw new Exception(StaticClass.Errors.SSIT_TRANSFERENCIAS_PAGO);
                            }
                        }

                        //0145298: JADHE 57098 - SGI - TRM 2019 piden BUI
                        TransferenciasSolicitudesBL blTransferencia = new TransferenciasSolicitudesBL();
                        TransferenciasSolicitudesDTO tranf = blTransferencia.Single(transferencia.IdSolicitud);
                        if (tranf.TipoTransmision.id_tipoTransmision == (int)Constantes.TipoTransmision.Transmision_Transferencia)
                        {
                            var ret = pagosBoletaBL.GetEstadoPago(Constantes.PagosTipoTramite.TR, transferencia.IdSolicitud);
                            if (ret != Constantes.BUI_EstadoPago.Pagado)
                                throw new Exception(StaticClass.Errors.SSIT_TRANSFERENCIAS_PAGO);
                        }
                    }
                }
                    //valido AT
                TransferenciaBL.validarEncomienda(IdSolicitud);
                RegenerarSolicitud(IdSolicitud);
                if (id_estado_ant == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF || id_estado_ant == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO)
                {
                    ExternalServiceReporting reportingService = new ExternalServiceReporting();
                    var ReportingEntity = reportingService.GetPDFObleaTransmision(IdSolicitud, true);
                    oblea = ReportingEntity.Reporte;
                    emailUsuario = usuario.Email;
                    TransferenciaBL.SetOblea(IdSolicitud, userid, ReportingEntity.Id_file, ReportingEntity.FileName);
                }

                if (transferencia.idTAD != null)
                {
                    enviarCambio(transferencia);
                    enviarParticipantes(transferencia);
                }
                TransferenciaBL.Confirmar(IdSolicitud, userid);

                #region envio mail y notificacion
                string html = new MailMessages().MailDisponibilzarQR(IdSolicitud);
                string Direccion = TransferenciaBL.GetDireccionesTransf(new List<int> { IdSolicitud }).First().direccion;
                string asunto = $"Solicitud de trámite N°: {IdSolicitud} - {Direccion}";

                var emails = new List<string>
                {
                    usuario.Email
                };
                emails.AddRange(transferencia.TitularesPersonasFisicas?.Select(t => t.Email));
                emails.AddRange(transferencia.TitularesPersonasJuridicas?.Select(t => t.Email));
                emails.AddRange(transferencia.FirmantesPersonasFisicas?.Select(f => f.Email));
                emails.AddRange(transferencia.FirmantesPersonasJuridicas?.Select(f => f.Email));
                emails.Add(new EncomiendaBL().GetProfesionalByTransf(IdSolicitud)?.Email);

                var idEmails = new List<int>();

                var emailService = new EmailServiceBL();
                foreach (var email in emails)
                {
                    EmailEntity emailEntity = new EmailEntity
                    {
                        Email = email,
                        Html = html,
                        Asunto = asunto,
                        IdEstado = (int)TiposDeEstadosEmail.PendienteDeEnvio,
                        IdTipoEmail = (int)TiposDeMail.Generico,
                        IdOrigen = (int)MailOrigenes.SSIT,
                        CantIntentos = 3,
                        CantMaxIntentos = 3,
                        FechaAlta = DateTime.Now,
                        Prioridad = 1
                    };

                    idEmails.Add(emailService.SendMail(emailEntity));
                }

                var notifBL = new TransferenciasNotificacionesBL();
                foreach (var idEmail in idEmails)
                {
                    notifBL.InsertNotificacionByIdSolicitud(IdSolicitud, idEmail, (int)MotivosNotificaciones.InicioHabilitación);
                }
                #endregion

                Cargar();
                ScriptManager.RegisterStartupScript(udpConfirmarSolcitud, udpConfirmarSolcitud.GetType(), "init_Js_updCargarDatos", "init_Js_updCargarDatos();", true);
            }
            catch (Exception ex)
            {
                LogError.Write(ex);
                lblTextoTramiteIncompleto.Text = Funciones.GetErrorMessage(ex);
                pnlTramiteIncompleto.Visible = true;

            }
        }

        protected void btnEliminarDocumentoAdjunto_Click(object sender, EventArgs e)
        {
            try
            {
                int id_docadjunto;
                int.TryParse(hid_id_docadjunto.Value, out id_docadjunto);
                TransferenciasDocumentosAdjuntosBL bldoc = new TransferenciasDocumentosAdjuntosBL();
                TransferenciasDocumentosAdjuntosDTO dto = new TransferenciasDocumentosAdjuntosDTO();

                dto.Id = id_docadjunto;
                bldoc.Delete(dto);

                var transferencia = TransferenciaBL.Single(IdSolicitud);
                ConsultaPadronSolicitudesBL consulta = new ConsultaPadronSolicitudesBL();

                var consultaPadron = consulta.Single(transferencia.IdConsultaPadron);

                CargarDocumentos(transferencia, consultaPadron);

                ScriptManager.RegisterStartupScript(updpnlDocumentosAdjuntos, updpnlDocumentosAdjuntos.GetType(), "hideEliminarDocumentoAdjunto", "hidefrmEliminarDocumentoAdjunto();", true);
                Cargar();
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updEstadoSolicitud, updEstadoSolicitud.GetType(), "showfrmError", "showfrmError();", true);
            }
            ScriptManager.RegisterStartupScript(udpConfirmarSolcitud, udpConfirmarSolcitud.GetType(), "init_Js_updCargarDatos", "init_Js_updCargarDatos();", true);
        }

        protected void btnConfirmarTramite_Click(object sender, EventArgs e)
        {
            try
            {
                var solTrDto = TransferenciaBL.Single(IdSolicitud);

                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                nroSolicitudModal.Text = solTrDto.IdSolicitud.ToString();
                lblCodSeguridad.Text = solTrDto.CodigoSeguridad.ToString();
                MostrarMensajeAlertaFaltantes();
                TransferenciaBL.ConfirmarSol(IdSolicitud, userid);

                #region envio mail y notificacion
                string direccion = TransferenciaBL.GetDireccionesTransf(new List<int> { IdSolicitud }).First().direccion;
                string html = new MailMessages().MailSolicitudNueva(solTrDto.IdSolicitud, solTrDto.CodigoSeguridad, solTrDto.TipoTramite.DescripcionTipoTramite);
                string asunto = $"Solicitud de trámite N°: {solTrDto.IdSolicitud} - {direccion}";

                var emails = new List<string>
                {
                    Membership.GetUser(userid).Email
                };
                emails.AddRange(solTrDto.TitularesPersonasFisicas?.Select(t => t.Email));
                emails.AddRange(solTrDto.TitularesPersonasJuridicas?.Select(t => t.Email));
                emails.AddRange(solTrDto.FirmantesPersonasFisicas?.Select(f => f.Email));
                emails.AddRange(solTrDto.FirmantesPersonasJuridicas?.Select(f => f.Email));
                emails.Add(new EncomiendaBL().GetProfesionalByTransf(IdSolicitud)?.Email);

                var idEmails = new List<int>();

                var emailService = new EmailServiceBL();
                foreach (var email in emails.Where(em => em != null).Distinct())
                {
                    EmailEntity emailEntity = new EmailEntity
                    {
                        Email = email,
                        Html = html,
                        Asunto = asunto,
                        IdEstado = (int)TiposDeEstadosEmail.PendienteDeEnvio,
                        IdTipoEmail = (int)TiposDeMail.Generico,
                        IdOrigen = (int)MailOrigenes.SSIT,
                        CantIntentos = 3,
                        CantMaxIntentos = 3,
                        FechaAlta = DateTime.Now,
                        Prioridad = 1
                    };

                    idEmails.Add(emailService.SendMail(emailEntity));
                }

                var notifBL = new TransferenciasNotificacionesBL();
                foreach (var idEmail in idEmails)
                {
                    notifBL.InsertNotificacionByIdSolicitud(IdSolicitud, idEmail, (int)MotivosNotificaciones.InicioHabilitación);
                }
                #endregion

                if (solTrDto.idTAD != null)
                {
                    enviarCambio(solTrDto);
                    enviarParticipantes(solTrDto);
                }

                ScriptManager.RegisterStartupScript(udpConfirmarSolcitud, udpConfirmarSolcitud.GetType(), "showModalConfirmarSolicitud", "showModalConfirmarSolicitud();", true);
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updEstadoSolicitud, updEstadoSolicitud.GetType(), "showfrmError", "showfrmError();", true);
            }
            finally
            {
                Cargar();
                updCargarDatos.Update();
                updEstadoSolicitud.Update();
                updpnlDocumentosAdjuntos.Update();
                updpnlAgregarDocumentos.Update();
                ScriptManager.RegisterStartupScript(udpConfirmarSolcitud, udpConfirmarSolcitud.GetType(), "init_Js_updCargarDatos", "init_Js_updCargarDatos();", true);
            }
        }

        private void enviarCambio(TransferenciasSolicitudesDTO sol)
        {
            ParametrosBL parametrosBL = new ParametrosBL();

            string _urlESB = parametrosBL.GetParametroChar("Url.Service.ESB");
            string trata = parametrosBL.GetParametroChar("Trata.Transferencias");

            string dir = "";

            string _noESB = parametrosBL.GetParametroChar("SSIT.NO.ESB");
            bool.TryParse(_noESB, out bool noESB);

            List<int> lisSol = new List<int>();
            lisSol.Add(IdSolicitud);
            foreach (var item in TransferenciaBL.GetDireccionesTransf(lisSol).ToList())
                dir += item.direccion + " / ";
            if (!noESB)
            {
                try
                {
                    wsTAD.actualizarTramite(_urlESB, sol.idTAD.Value, sol.IdSolicitud, sol.NumeroExpedienteSade, trata, dir);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        private void enviarParticipantes(TransferenciasSolicitudesDTO sol)
        {
            MembershipUser usuario = Membership.GetUser();
            ParametrosBL parametrosBL = new ParametrosBL();
            string _urlESB = parametrosBL.GetParametroChar("Url.Service.ESB");
            string trata = parametrosBL.GetParametroChar("Trata.Transferencias");

            var list = wsGP.perfilesPorTrata(_urlESB, trata);
            int idPerfilTit = 0;
            int idPerfilSol = 0;
            int idPerfilTitComplementario = 0;
            foreach (var p in list)
            {
                if (p.nombrePerfil == "TITULAR")
                    idPerfilTit = p.idPerfil;
                else if (p.nombrePerfil == "SOLICITANTE")
                    idPerfilSol = p.idPerfil;
                else if (p.nombrePerfil == "TITULAR COMPLEMENTARIO")
                    idPerfilTitComplementario = p.idPerfil;
            }
            TitularesBL titularesBL = new TitularesBL();

            var lstTitulares = titularesBL.GetTitularesTransferencias(sol.IdSolicitud).ToList();
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

            var listParticipantesSSITCuit = lstParticipantesSSIT.Select(x => x.cuit).Distinct().OrderByDescending(x => x);

            var listParticipantesGPCuit = lstParticipantesGP.Select(x => x.cuit).Distinct().OrderByDescending(x => x);

            var solicitante = lstParticipantesSSIT.FirstOrDefault(x => x.idPerfil == idPerfilSol);

            var solicitanteGP = lstParticipantesGP.FirstOrDefault(x => x.idPerfil == idPerfilSol);

            var titular = lstParticipantesSSIT.FirstOrDefault(x => x.idPerfil == idPerfilTit);

            var listTitularesComplementariosCuit = lstParticipantesSSIT
                                    .Where(x => x.cuit != titular.cuit)
                                    .Select(x => x.cuit)
                                    .ToList();
            //esto para arreglar el backlog de error22
            if (solicitanteGP == null)
            {
                Exception ex22 = new Exception(
                    $"Debe tener solicitante para poder tramitar, titular {titular}," +
                    $"Solicitud : {sol.IdSolicitud}, " +
                    $"idTad : {sol.idTAD}, " +
                    $"usuarioSSIT : {usuDTO.UserName}"
                    );
                LogError.Write(ex22);
                wsGP.nuevoTramiteParticipante(_urlESB, trata, sol.idTAD.Value, sol.NumeroExpedienteSade,
                usuDTO.CUIT, (int)TipoParticipante.Solicitante, true, Constantes.Sistema,
                usuDTO.Nombre, usuDTO.Apellido, usuDTO.RazonSocial);
                lstParticipantesGP = wsGP.GetParticipantesxTramite(_urlESB, sol.idTAD.Value).ToList();
                listParticipantesGPCuit = lstParticipantesGP.Select(x => x.cuit).Distinct().OrderByDescending(x => x);
            }
            bool cambios = listParticipantesSSITCuit.Except(listParticipantesGPCuit).Any()
                || listParticipantesGPCuit.Except(listParticipantesSSITCuit).Any();

            if (cambios)
            {
                bool tieneSolicitante = false;
                // baja
                foreach (var item in lstParticipantesGP)
                {
                    //desvincular todos los  participantes, menos el solicitante
                    if (item.idPerfil != (int)TipoParticipante.Solicitante)
                        wsGP.DesvincularParticipante(_urlESB, sol.idTAD.Value, solicitante.cuit, solicitante.idPerfil, Constantes.Sistema, item.cuit, item.idPerfil);
                    else
                        tieneSolicitante = true;
                }

                // alta solicitante/apoderado
                if (!tieneSolicitante)
                    wsGP.nuevoTramiteParticipante(_urlESB, trata, sol.idTAD.Value, sol.NumeroExpedienteSade,
                    solicitante.cuit, solicitante.idPerfil, solicitante.idPerfil == idPerfilSol, Constantes.Sistema,
                    solicitante.Nombres, solicitante.Apellido, solicitante.RazonSocial);

                // alta titular 
                wsGP.nuevoTramiteParticipante(_urlESB, trata, sol.idTAD.Value, sol.NumeroExpedienteSade,
                        titular.cuit, titular.idPerfil, titular.idPerfil == idPerfilSol, Constantes.Sistema,
                        titular.Nombres, titular.Apellido, titular.RazonSocial);

                //alta titulares complementarios
                foreach (var item in lstParticipantesSSIT)
                {
                    if (listTitularesComplementariosCuit.Contains(item.cuit) && item.idPerfil != idPerfilSol)
                    {
                        wsGP.nuevoTramiteParticipante(_urlESB, trata, sol.idTAD.Value, sol.NumeroExpedienteSade,
                                item.cuit, (int)TipoParticipante.TitularComplementario, item.idPerfil == idPerfilSol,
                                Constantes.Sistema, item.Nombres, item.Apellido, item.RazonSocial);
                    }
                }
            }
        }

        protected void btnAnularTramite_Click(object sender, EventArgs e)
        {
            try
            {
                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                TransferenciaBL.Anular(IdSolicitud, userid);
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updEstadoSolicitud, updEstadoSolicitud.GetType(), "showfrmError", "showfrmError();", true);

            }
            Cargar();
            updEstadoSolicitud.Update();
            ScriptManager.RegisterStartupScript(updEstadoSolicitud, updEstadoSolicitud.GetType(), "hidefrmConfirmarAnulacion", "hidefrmConfirmarAnulacion();", true);
        }

        protected void OnErrorCargaDocumentoClick(object sender, CargaDocumentoErrorEventArgs e)
        {
            lblError.Text = e.Description;
            ScriptManager.RegisterClientScriptBlock(pnlDatosDocumento, pnlDatosDocumento.GetType(), "mostrarError", "showfrmError(); ", true);
        }
        protected void CargaDocumentos_SubirDocumentoClick(object sender, ucCargaDocumentosEventsArgs e)
        {
            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
            try
            {
                TransferenciasDocumentosAdjuntosBL docBL = new TransferenciasDocumentosAdjuntosBL();
                TransferenciasDocumentosAdjuntosDTO dto = new TransferenciasDocumentosAdjuntosDTO();
                dto.IdSolicitud = IdSolicitud;
                dto.IdTipoDocumentoRequerido = e.id_tdocreq;
                dto.NombreArchivo = e.nombre_archivo;
                dto.CreateUser = userid;
                dto.Documento = e.Documento;
                docBL.Insert(dto);
                Cargar();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                ScriptManager.RegisterClientScriptBlock(pnlDatosDocumento, pnlDatosDocumento.GetType(), "mostrarError", "showfrmError(); ", true);
            }
            ScriptManager.RegisterStartupScript(udpConfirmarSolcitud, udpConfirmarSolcitud.GetType(), "init_Js_updCargarDatos", "init_Js_updCargarDatos();", true);
        }
        public void RegenerarSolicitud(int id_solicitud)
        {
            ExternalService.Class.ReportingEntity ReportingEntity = new ExternalService.Class.ReportingEntity();
            ExternalServiceFiles esf = new ExternalServiceFiles();
            TransferenciasDocumentosAdjuntosBL trDocAdjBL = new TransferenciasDocumentosAdjuntosBL();
            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
            byte[] pdfSolicitud = new byte[0];
            string arch = "ManifiestoTransmision-" + id_solicitud.ToString() + ".pdf";

            int id_tipodocsis = 0;
            id_tipodocsis = (int)Constantes.TiposDeDocumentosSistema.MANIFIESTO_TRANSMISION;

            var DocAdjDTO = trDocAdjBL.GetByFKIdSolicitud(id_solicitud).Where(x => x.IdTipoDocsis == (int)Constantes.TiposDeDocumentosSistema.MANIFIESTO_TRANSMISION).FirstOrDefault();

            ExternalServiceReporting reportingService = new ExternalServiceReporting();

            ReportingEntity = reportingService.GetPDFTransmision(id_solicitud, true);

            pdfSolicitud = ReportingEntity.Reporte;
            int id_file = ReportingEntity.Id_file;

            if (DocAdjDTO != null)
            {
                if (id_file != DocAdjDTO.IdFile)
                    esf.deleteFile(DocAdjDTO.IdFile);
                DocAdjDTO.IdFile = id_file;
                DocAdjDTO.NombreArchivo = arch;
                DocAdjDTO.UpdateDate = DateTime.Now;
                DocAdjDTO.UpdateUser = userid;
                trDocAdjBL.Update(DocAdjDTO);
            }
            else
            {
                DocAdjDTO = new TransferenciasDocumentosAdjuntosDTO();
                DocAdjDTO.IdSolicitud = id_solicitud;
                DocAdjDTO.IdTipoDocumentoRequerido = 0;
                DocAdjDTO.TipoDocumentoRequeridoDetalle = "";
                DocAdjDTO.GeneradoxSistema = true;
                DocAdjDTO.CreateDate = DateTime.Now;
                DocAdjDTO.CreateUser = userid;
                DocAdjDTO.NombreArchivo = arch;
                DocAdjDTO.IdFile = id_file;
                DocAdjDTO.IdTipoDocsis = id_tipodocsis;

                trDocAdjBL.Insert(DocAdjDTO, true);
            }
        }
        #region observaciones
        private class Observacion : IEqualityComparer<Observacion>
        {
            public DateTime CreateDate { get; set; }
            public string userApeNom { get; set; }
            public int id_ObsGrupo { get; set; }

            public bool Equals(Observacion source, Observacion dest)
            {
                return (source.id_ObsGrupo == dest.id_ObsGrupo);
            }
            public int GetHashCode(Observacion obj)
            {
                return obj.id_ObsGrupo.GetHashCode();
            }
        }
        //private void CargarDatosObservaciones(TransferenciasSolicitudesDTO transferencia)
        //{
        //    //this.mensajeAlertaObservacion = "";

        //    //gridObservaciones.DataSource = transferencia.Observaciones;
        //    //gridObservaciones.DataBind();

        //    //hid_mostrar_observacion.Value = (gridObservaciones.Rows.Count > 0) ? "true" : "false";

        //    //Observaciones nuevas



        //}
        protected void btnConfirmarObservacion_Command(object sender, CommandEventArgs e)
        {
            try
            {

                int id_solobs = Convert.ToInt32(e.CommandArgument);

                TransferenciasSolicitudesObservacionesBL blObs = new TransferenciasSolicitudesObservacionesBL();
                var obs = blObs.Single(id_solobs);
                obs.Leido = true;
                blObs.Update(obs);

                Cargar();
                ScriptManager.RegisterClientScriptBlock(updCargarDatos, updCargarDatos.GetType(), "mostrarPanel", "finalizarCargaTab();", true);

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updEstadoSolicitud, updEstadoSolicitud.GetType(), "showfrmError", "showfrmError();", true);
            }

        }
        protected void gridObservaciones_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TransferenciasSolicitudesObservacionesDTO row = (TransferenciasSolicitudesObservacionesDTO)e.Row.DataItem;

                LinkButton lnkModal = (LinkButton)e.Row.FindControl("lnkModal");
                Panel pnlObservacionModal = (Panel)e.Row.FindControl("pnlObservacionModal");
                Button btnConfirmarObservacion = (Button)e.Row.FindControl("btnConfirmarObservacion");
                lnkModal.Attributes["data-target"] = "#" + pnlObservacionModal.ClientID;


                if (row.Leido.HasValue)
                {
                    if (!row.Leido.Value)
                        this.mensajeAlertaObservacion = "Falta leer observaciones";
                    else
                        btnConfirmarObservacion.Style["display"] = "none";
                }

            }

        }
        #endregion

        private bool BoletaCeroActiva()
        {

            string boletaCero_FechaDesde = System.Configuration.ConfigurationManager.AppSettings["boletaCero_FechaDesde"];

            DateTime boletaCeroDate = DateTime.ParseExact(boletaCero_FechaDesde,
                                                            "yyyyMMdd",
                                                            System.Globalization.CultureInfo.InvariantCulture);

            if (DateTime.Now > boletaCeroDate)
                return true;

            return false;
        }
    }
}