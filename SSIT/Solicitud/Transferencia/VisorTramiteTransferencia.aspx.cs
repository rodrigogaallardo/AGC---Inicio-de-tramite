using BusinesLayer.Implementation;
using DataTransferObject;
using ExternalService;
using SSIT.App_Components;
using SSIT.Common;
using SSIT.Solicitud.Controls;
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
    public partial class VisorTramiteTransferencia : BasePage
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

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager sm = ScriptManager.GetCurrent(this);

            if (sm.IsInAsyncPostBack)
            {
            }

            btnModificarTitulares.PostBackUrl = string.Format("~/" + RouteConfig.EDITAR_TRANSFERENCIAS_TITULAR + "{0}", IdSolicitud);

        }
        string mensajeAlertaObservacion;
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
                divbtnImprimirSolicitud.Visible = false;

                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updCargarDatos, updCargarDatos.GetType(), "finalizarCarga", "finalizarCarga();showfrmError();", true);
            }
        }

        private void CargarCombos()
        {

            TiposDeDocumentosRequeridosBL blTipoDoc = new TiposDeDocumentosRequeridosBL();

            List<TiposDeDocumentosRequeridosDTO> lstTiposDocumentos = blTipoDoc.GetVisibleSSITXTipoTramite((int)Constantes.TipoDeTramite.Transferencia).ToList();
            CargaDocumentos.CargarCombo(lstTiposDocumentos);
            updpnlAgregarDocumentos.Update();
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
        /// <summary>
        /// 
        /// </summary>
        private void CargarDatosTramite(TransferenciasSolicitudesDTO transferencia)
        {

            ConsultaPadronSolicitudesBL consulta = new ConsultaPadronSolicitudesBL();

            Guid userId = (Guid)Membership.GetUser().ProviderUserKey;

            var consultaPadron = consulta.Single(transferencia.IdConsultaPadron);

            divbtnAnularTramite.Visible = false;
            divbtnImprimirSolicitud.Visible = false;
            divbtnConfirmarTramite.Visible = false;

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

            //Solo se puede imprimir cuando se encuentra confirmado
            if (transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF)
            {
                divbtnImprimirSolicitud.Visible = true;
                btnImprimirSolicitud.Visible = true;
                btnImprimirSolicitud.NavigateUrl = string.Format("~/" + RouteConfig.IMPRIMIR_TRANSFERENCIA + "{0}", Functions.ConvertToBase64String(IdSolicitud));
            }

            //if (transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.COMP)
            //{
            //    divbtnConfirmarTramite.Visible = true;
            //}
            //else if (transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.VALESCR)
            //{
            //    if (!pnlAlertasSolicitud.Visible)
            //        divbtnConfirmarTramite.Visible = true;
            //}

            if (transferencia.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.ETRA &&
                transferencia.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.VALESCR &&
                transferencia.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.ANU)
            {
                divbtnAnularTramite.Visible = true;

            }
               

            if (transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.INCOM
                || transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.COMP
                || transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO
                || (transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.SUSPEN && isUltimaTareaCorreccion(transferencia.IdSolicitud)))
            {
                pnlModifTitulares.Visible = true;
                pnlAgregarDocumentos.Visible = true;
            }

            if (transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.VALESCR)
            {
                pnlAgregarDocumentos.Visible = true;
            }

            lblNroSolicitud.Text = transferencia.IdSolicitud.ToString();
            lblNroEncomienda.Text = transferencia.IdConsultaPadron.ToString();
            lblTipoTramite.Text = transferencia.TipoTramite.DescripcionTipoTramite;
            lblEstadoSolicitud.Text = transferencia.Estado.Descripcion;

            if (transferencia.NumeroExpedienteSade != null)
                lblNroExpediente.Text = transferencia.NumeroExpedienteSade;

            if ((transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.COMP) ||
                (transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.INCOM) ||
                (transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.VALESCR) ||
                (transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.ESCREAL) ||
                (transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO))
            {
                btnMostrarAgregadoDocumentos.Visible = true;
                divbtnConfirmarTramite.Visible = true;
            }
            else
            {
                btnMostrarAgregadoDocumentos.Visible = false;
            }

            #region Ubicacion
            visUbicaciones.Editable = false;
            visUbicaciones.CargarDatos(consultaPadron);
            #endregion

            #region Datos del Local

            DatosLocal.CargarDatos(consultaPadron);
            #endregion

            #region Rubros
            visRubros.OcultarZonaSup = true;
            visRubros.CargarDatos(consultaPadron);
            #endregion

            #region  Titulares de la transferencia
            //Titulares de la transferencia
            Titulares.CargarDatos(transferencia);
            #endregion

            #region  Titulares de la solicitud
            TitularesSolicitud.CargarDatos(consultaPadron);
            #endregion

            #region Titulares de la Habilitación
            TitularesHabilitacion.CargarDatos(consultaPadron);
            #endregion

            #region adjuntos
            CargarDocumentos(transferencia, consultaPadron);
            #endregion

            #region Observaciones
            CargarDatosObservaciones(transferencia);
            #endregion

            #region PAGOS
            if (BoletaCeroActiva())
            {
                pnlPagos.Visible = false;
                Pagos.Visible = false;
            }

            Pagos.id_solicitud = IdSolicitud;
            Pagos.tipo_tramite = (int)Constantes.PagosTipoTramite.TR;

            Constantes.BUI_EstadoPago[] arrEstadosPago = new Constantes.BUI_EstadoPago[] { Constantes.BUI_EstadoPago.Pagado, Constantes.BUI_EstadoPago.SinPagar };

            if (transferencia.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.PENPAG && !arrEstadosPago.Contains(Pagos.GetEstadoPago(Constantes.PagosTipoTramite.TR, transferencia.IdSolicitud)))
                Pagos.HabilitarGeneracionManual = true;

            Pagos.CargarPagos(Constantes.PagosTipoTramite.TR, IdSolicitud);
            #endregion


            updCargarDatos.Update();
            updEstadoSolicitud.Update();
            updpnlDocumentosAdjuntos.Update();
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

        #region observaciones
        private void CargarDatosObservaciones(TransferenciasSolicitudesDTO transferencia)
        {
            this.mensajeAlertaObservacion = "";

            gridObservaciones.DataSource = transferencia.Observaciones;
            gridObservaciones.DataBind();

            hid_mostrar_observacion.Value = (gridObservaciones.Rows.Count > 0) ? "true" : "false";

        }
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
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updEstadoSolicitud, updEstadoSolicitud.GetType(), "showfrmError", "showfrmError();", true);
            }
        }

        protected void btnConfirmarTramite_Click(object sender, EventArgs e)
        {
            try
            {
                var sol = TransferenciaBL.Single(IdSolicitud);
                if (sol.idTAD != null)
                {
                    enviarCambio(sol);
                    enviarParticipantes(sol);
                }
                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                TransferenciaBL.Confirmar(IdSolicitud, userid);


            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updEstadoSolicitud, updEstadoSolicitud.GetType(), "showfrmError", "showfrmError();", true);
            }
            Cargar();
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

            var titular = lstParticipantesSSIT.FirstOrDefault(x => x.idPerfil == idPerfilTit);

            var listTitularesComplementariosCuit = lstParticipantesSSIT
                                    .Where(x => x.cuit != titular.cuit)
                                    .Select(x => x.cuit)
                                    .ToList();
            //esto para arreglar el backlog de error22
            if (solicitante == null)
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
            }

            bool cambios = listParticipantesSSITCuit.Except(listParticipantesGPCuit).Any()
                || listParticipantesGPCuit.Except(listParticipantesSSITCuit).Any();

            if (cambios)
            {
                bool tieneSolicitante = false;
                //desvincular todos los participantes, menos el solicitante                 
                foreach (var item in lstParticipantesGP)
                {
                    if (item.idPerfil != (int)TipoParticipante.Solicitante)
                        wsGP.DesvincularParticipante(_urlESB, sol.idTAD.Value, solicitante.cuit, solicitante.idPerfil, Constantes.Sistema, item.cuit, item.idPerfil);
                    else
                        tieneSolicitante = true;
                }

                // alta solicitante/apoderado
                if (!tieneSolicitante)
                    wsGP.nuevoTramiteParticipante(_urlESB, trata, sol.idTAD.Value, sol.NumeroExpedienteSade,
                        usuDTO.CUIT, (int)TipoParticipante.Solicitante, true, Constantes.Sistema,
                        usuDTO.Nombre, usuDTO.Apellido, usuDTO.RazonSocial);

                // alta titular 
                wsGP.nuevoTramiteParticipante(_urlESB, trata, sol.idTAD.Value, sol.NumeroExpedienteSade,
                        titular.cuit, titular.idPerfil, titular.idPerfil == idPerfilSol, Constantes.Sistema, titular.Nombres, titular.Apellido, titular.RazonSocial);

                //alta titulares complementarios
                foreach (var item in lstParticipantesSSIT)
                {
                    if (listTitularesComplementariosCuit.Contains(item.cuit) && item.idPerfil != idPerfilSol)
                    {
                        wsGP.nuevoTramiteParticipante(_urlESB, trata, sol.idTAD.Value, sol.NumeroExpedienteSade,
                                item.cuit, (int)TipoParticipante.TitularComplementario, item.idPerfil == idPerfilSol, Constantes.Sistema, item.Nombres, item.Apellido, item.RazonSocial);
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

        #region pro teatro
        private void cargarProTeatro(int id_encomienda)
        {
            EncomiendaBL blEnc = new EncomiendaBL();
            var enc = blEnc.Single(id_encomienda);

            if (enc.ProTeatro)
                pnlDatosProTeatro.Style.Value = "display: block";
            else
                pnlDatosProTeatro.Style.Value = "display: none";


            EncomiendaDocumentosAdjuntosBL encDocAdjBL = new EncomiendaDocumentosAdjuntosBL();
            TiposDeDocumentosSistemaBL tipDocSis = new TiposDeDocumentosSistemaBL();
            EncomiendaRubrosBL blRubros = new EncomiendaRubrosBL();
            int id_tipodocsis = tipDocSis.GetByCodigo("CERTIFICADO_PRO_TEATRO").id_tipdocsis;
            grdProTeatro.DataSource = encDocAdjBL.GetByFKIdEncomiendaTipoSis(id_encomienda, id_tipodocsis).ToList();
            grdProTeatro.DataBind();

            uplProTeatro.Update();
            updPnlCertificados.Update();
            var rubros = blRubros.GetByFKIdEncomienda(id_encomienda);
            hid_mostrar_certificado.Value = "False";
            foreach (var rubro in rubros)
            {
                if (rubro.CodigoRubro == "800530")
                {
                    hid_mostrar_certificado.Value = "True";
                    break;
                }

            }
        }

        #endregion

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
        }

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