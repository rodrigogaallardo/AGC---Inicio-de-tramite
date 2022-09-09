using BusinesLayer.Implementation;
using DataTransferObject;
using ExternalService;
using Reporting;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsejosProfesionales.Encomiendas
{
    public partial class ActualizarTramite : SecurePage
    {
        /// <summary>
        /// 
        /// </summary>
        public int NroTramite
        {
            get
            {
                return Convert.ToInt32(this.RouteData.Values["id_encomienda"].ToString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int TipoCertificado
        {
            get
            {
                return Convert.ToInt32(this.RouteData.Values["tipo_tramite"].ToString());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Membership.GetUser() == null)
                    Response.Redirect("~/Default.aspx");

                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                hid_id_encomienda.Value = NroTramite.ToString();
                CargarDatosTramite(NroTramite);
                CargarHistorialEstados(NroTramite);
                CargarEstadosPosibles(NroTramite);
                lnkExportarEncomiendaXML.NavigateUrl = "~/Reportes/ExportEncomiendaXML.aspx?id=" + NroTramite.ToString() + "&tipo_certificado=" + TipoCertificado.ToString();
                btnImprimirComprobante.NavigateUrl = string.Format("~/Reportes/ImprimirComprobante.aspx?tipo={0}&nro_tramite={1}&userid={2}", 1, NroTramite, userid.ToString().ToLower());

                string url = ResolveUrl(string.Format("~/Reportes/ImprimirEncomienda.aspx?id_encomienda={0}", NroTramite));
                hid_url_reporte.Value = url;
                ddlCambiarEstado.ClearSelection();
            }
        }

        private void CargarEstadosPosibles(int id_encomienda)
        {
            EncomiendaEstadosBL estadoBL = new EncomiendaEstadosBL();
            EncomiendaBL encomiendaBL = new EncomiendaBL();
            var dsEncomienda = encomiendaBL.Single(id_encomienda);

            List<EncomiendaEstadosDTO> lista = estadoBL.TraerEncomiendaEstadosSiguientes((Guid)Membership.GetUser().ProviderUserKey, dsEncomienda.IdEstado).ToList();
            if (lista.Any(x => x.IdEstado == (int)Constantes.Encomienda_Estados.Completa))
                lista.Find(x => x.IdEstado == (int)Constantes.Encomienda_Estados.Completa).NomEstado = "Devolver al contribuyente";

            ddlCambiarEstado.DataSource = lista; // estadoBL.TraerEncomiendaEstadosSiguientes((Guid)Membership.GetUser().ProviderUserKey, dsEncomienda.IdEstado);
            ddlCambiarEstado.DataTextField = "NomEstado";
            ddlCambiarEstado.DataValueField = "IdEstado";
            ddlCambiarEstado.DataBind();
            ListItem itm = new ListItem("(Seleccione el estado)", "-1");
            ddlCambiarEstado.Items.Insert(0, itm);
            //ddlCambiarEstado.Items.FindByValue("").Text = "Devolver al contribuyente";
        }

        private void CargarDatosTramite(int id_encomienda)
        {
            CargarDatosEncomienda(id_encomienda);
        }

        private void CargarDatosEncomienda(int id_encomienda)
        {
            EncomiendaBL encomiendaBL = new EncomiendaBL();
            var dsEncomienda = encomiendaBL.Single(id_encomienda);

            DateTime FechaEncomienda = dsEncomienda.FechaEncomienda;

            lblFechaEncomienda.Text = FechaEncomienda.ToShortDateString();
            lblEstado.Text = dsEncomienda.Estado.NomEstado;

            lblUsuarioCreacion.Text = Membership.GetUser(dsEncomienda.CreateUser).UserName;

            //Deshabilita o habilita los botones según su estado.
            int id_estado = dsEncomienda.IdEstado;
            if (id_estado == (int)Constantes.Encomienda_Estados.Ingresada_al_consejo || id_estado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo
                || id_estado == (int)Constantes.Encomienda_Estados.Rechazada_por_el_consejo)
                pnlExportarXML.Visible = true;
            else
                pnlExportarXML.Visible = false;

            if (id_estado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo)
                btnImprimirComprobante.Visible = true;
            else
                btnImprimirComprobante.Visible = false;

            lblTipoTramite.Text = dsEncomienda.TipoTramite.DescripcionTipoTramite + " " + dsEncomienda.TipoExpedienteDescripcion;
            if (dsEncomienda.EsECI)
                lblTipoTramite.Text = (dsEncomienda.IdTipoTramite == (int)StaticClass.Constantes.TipoTramite.HabilitacionECIHabilitacion ? StaticClass.Constantes.TipoTramiteDescripcion.HabilitacionECI : StaticClass.Constantes.TipoTramiteDescripcion.AdecuacionECI);

            lblNroEncomienda.Text = NroTramite.ToString();
        }


        private void CargarHistorialEstados(int id_encomienda)
        {
            EncomiendaBL encomiendaBL = new EncomiendaBL();

            grdHistorialEstados.DataSource = encomiendaBL.GetHistorial(id_encomienda);
            grdHistorialEstados.DataBind();
        }

        protected void btnCambiarEstado_Click(object sender, EventArgs e)
        {
            lblmpeInfo.Text = "";
            lblMensajeError.Text = "";

            if (ddlCambiarEstado.SelectedIndex <= 0)
            {
                lblMensajeError.Text = "Debe seleccionar el estado por el cual se desea realizar el cambio.";
                ScriptManager.RegisterClientScriptBlock(updBotonesAccion, updBotonesAccion.GetType(), "mostrarError", string.Format("mostrarPopup('{0}');", pnlMensajeError.ClientID), true);
            }
            else
            {
                try
                {
                    int id_encomienda = int.Parse(hid_id_encomienda.Value);
                    int id_estado = int.Parse(ddlCambiarEstado.SelectedValue);
                    Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                    EncomiendaBL encBL = new EncomiendaBL();

                    if (id_estado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo)
                    {
                        ExternalServiceReporting reportingService = new ExternalServiceReporting();
                        EncomiendaDocumentosAdjuntosBL encDocBL = new EncomiendaDocumentosAdjuntosBL();
                        ExternalServiceFiles esf = new ExternalServiceFiles();

                        byte[] pdfCertificado = new byte[0];
                        int id_fileCert = 0;
                        string fileName = "";
                        try
                        {
                            var ReportingEntityCertificado = reportingService.GetPDFCertificadoConsejoEncomienda(id_encomienda, true);
                            pdfCertificado = ReportingEntityCertificado.Reporte;
                            id_fileCert = pdfCertificado != null ? ReportingEntityCertificado.Id_file : 0;
                            fileName = ReportingEntityCertificado.FileName;
                        }
                        catch (Exception ex)
                        {
                            lblMensajeError.Text = $"Error Reporting, No se pudo generar pdf certificado. - GetPDFCertificadoConsejoEncomienda {ex.Message}";
                            this.EjecutarScript(updBotonesAccion, string.Format("mostrarPopup('{0}');", pnlMensajeError.ClientID));
                            //var ReportingEntityCertificado = reportingService.GetPDFCertificadoConsejoEncomienda(id_encomienda, true);
                            //pdfCertificado = ReportingEntityCertificado.Reporte;
                            //id_fileCert = pdfCertificado != null ? ReportingEntityCertificado.Id_file : 0;
                            //fileName = ReportingEntityCertificado.FileName;
                        }


                        if (pdfCertificado.Length == 0)
                            throw new Exception("No se pudo generar pdf certificado.");

                        if (pdfCertificado != null)
                        {
                            EncomiendaDocumentosAdjuntosDTO objectDto = new EncomiendaDocumentosAdjuntosDTO();
                            objectDto.CreateDate = DateTime.Now;
                            objectDto.CreateUser = userid;
                            objectDto.generadoxSistema = true;
                            objectDto.id_encomienda = id_encomienda;
                            objectDto.id_file = id_fileCert;
                            objectDto.id_tdocreq = 0;
                            objectDto.id_tipodocsis = (int)Constantes.TiposDeDocumentosSistema.CERTIF_CONSEJO_HABILITACION;
                            objectDto.nombre_archivo = fileName;
                            encDocBL.Insert(objectDto);

                            encBL.ActualizarEstado(id_encomienda, id_estado, userid, objectDto);
                            SendEncomiendaAprobado(id_encomienda);
                        }

                    }
                    else
                    {
                        encBL.ActualizarEstado(id_encomienda, id_estado, userid);
                    }


                    CargarDatosEncomienda(int.Parse(hid_id_encomienda.Value));
                    CargarEstadosPosibles(int.Parse(hid_id_encomienda.Value));
                    CargarHistorialEstados(int.Parse(hid_id_encomienda.Value));

                }
                catch (Exception ex)
                {
                    lblMensajeError.Text = ex.Message;
                    this.EjecutarScript(updBotonesAccion, string.Format("mostrarPopup('{0}');", pnlMensajeError.ClientID));
                }


                if (string.IsNullOrEmpty(lblMensajeError.Text))
                {
                    lblSuccess.Text = "El Anexo fue actualizado exitosamente.";
                    this.EjecutarScript(updBotonesAccion, string.Format("mostrarPopup('{0}');", pnlSuccess.ClientID));
                }
            }
        }

        private void SendEncomiendaAprobado(int IdEncomienda)
        {
            EncomiendaBL encomienda = new EncomiendaBL();
            var encomiendaDTO = encomienda.Single(IdEncomienda);
            MailMessages mailer = new MailMessages();

            string htmlBody;

            htmlBody = mailer.GetRecoveryEncomiendaAprobado();
            EmailServiceBL serviceMail = new EmailServiceBL();

            List<string> Emails = new List<string>();

            foreach (var titPJ in encomiendaDTO.EncomiendaTitularesPersonasJuridicasDTO.Where(x => !string.IsNullOrWhiteSpace(x.Email)))
                if (!string.IsNullOrWhiteSpace(titPJ.Email))
                    Emails.Add(titPJ.Email);


            foreach (var titPF in encomiendaDTO.EncomiendaTitularesPersonasFisicasDTO.Where(x => !string.IsNullOrWhiteSpace(x.Email)))
                if (!string.IsNullOrWhiteSpace(titPF.Email))
                    Emails.Add(titPF.Email);

            foreach (var titPJ in encomiendaDTO.EncomiendaTitularesPersonasJuridicasDTO.Where(x => !string.IsNullOrWhiteSpace(x.Email)))
                foreach (var firmante in titPJ.EncomiendaFirmantesPersonasJuridicasDTO)
                    if (!string.IsNullOrWhiteSpace(firmante.Email))
                        Emails.Add(firmante.Email);

            if (Emails.Any())
            {
                EmailServicePOST Datosmail = new EmailServicePOST();
                Datosmail.Email = string.Join(";", Emails);
                Datosmail.Html = htmlBody;
                Datosmail.Asunto = "Solicitud de habilitación N°: " + encomiendaDTO.IdSolicitud + " - " + encomiendaDTO.Direccion.direccion; //EncomiendaSSITSolicitudesDTO.Select(x => x.id_solicitud).FirstOrDefault()
                Datosmail.IdTipoEmail = (int)ExternalService.TiposDeMail.WebSGI_AprobacionDG;
                Datosmail.Prioridad = 1;

                serviceMail.SendMail(Datosmail);
            }
        }


    }
}