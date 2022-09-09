using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using ExternalService;

namespace ConsejosProfesionales.Encomiendas
{
    public partial class ActualizarEncomiendasExt : SecurePage
    {
        /// <summary>
        /// 
        /// </summary>
        public int NroTramite {

            get {
                return Convert.ToInt32(this.RouteData.Values["id_encomienda"].ToString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int TipoCertificado {
            get {
                return Convert.ToInt32(this.RouteData.Values["tipo_tramite"].ToString());
            }
        }

        #region carga inicial
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
                btnNuevaBusqueda.PostBackUrl = "~/Encomiendas/SearchEncomiendasExt.aspx?tipo_certificado=" + TipoCertificado.ToString();

                EncomiendaBL encomiendaBL = new EncomiendaBL();
                var dsSol = encomiendaBL.GetEncomiendaExterna(NroTramite);
                if (dsSol != null)
                {
                    int nro_tramite = dsSol.nroTramite;
                    btnImprimirComprobante.NavigateUrl = string.Format("~/Reportes/ImprimirComprobante.aspx?tipo={0}&nro_tramite={1}&userid={2}", TipoCertificado.ToString(), nro_tramite, userid.ToString().ToLower());
                }

                ddlCambiarEstado.ClearSelection();
            }
        }

        private void CargarEstadosPosibles(int id_encomienda)
        {
            int id_estado_actual = 0;
            EncomiendaBL encomiendaBL = new EncomiendaBL();
            EncomiendaEstadosBL encomiendaEstadosBL = new EncomiendaEstadosBL();  
            var dsEncomienda = encomiendaBL.GetEncomiendaExterna(id_encomienda);

           if (dsEncomienda != null)
           {
               id_estado_actual = dsEncomienda.IdEstado;

               ddlCambiarEstado.DataSource = encomiendaEstadosBL.TraerEncomiendaExtEstadosSiguientes((Guid)Membership.GetUser().ProviderUserKey, id_estado_actual, 0).Distinct();
               ddlCambiarEstado.DataTextField = "NomEstadoConsejo";
                ddlCambiarEstado.DataValueField = "IdEstado";
                ddlCambiarEstado.DataBind();

                ListItem itm = new ListItem("(Seleccione el estado)", "-1");
                ddlCambiarEstado.Items.Insert(0, itm);
           }            
        }
        
        private void CargarDatosTramite(int id_encomienda)
        {
            CargarDatosEncomienda(id_encomienda);
        }

        private void CargarHistorialEstados(int id_encomienda)
        {
            EncomiendaBL encomiendaBL = new EncomiendaBL();
            var elements = encomiendaBL.Traer_EncomiendaExt_HistorialEstados(id_encomienda);
            grdHistorialEstados.DataSource = elements;
            grdHistorialEstados.DataBind();
        }

        #endregion

        private void CargarDatosEncomienda(int id_encomienda)
        {
            Guid userId;
            EncomiendaBL encomiendaBL = new EncomiendaBL();
            var ds = encomiendaBL.GetEncomiendaExterna(id_encomienda);
            int id_estado;
            int nroTramite = 0;
            int tipo_tramite = 0;
            bool Bloqueada = false;

            Bloqueada = ds.Bloqueada;
            DateTime FechaEncomienda = (DateTime)ds.FechaEncomienda;
            lblFechaEncomienda.Text = FechaEncomienda.ToShortDateString();
            lblEstado.Text = ds.Estado.NomEstado;
                
            id_estado = ds.IdEstado;
            nroTramite = ds.nroTramite;
            tipo_tramite = ds.IdTipoTramite;

            userId = ds.CreateUser;
            Guid userid_Encomienda = (Guid)ds.CreateUser;

            //Deshabilita o habilita los botones según su estado.

            if (id_estado == (int)Constantes.EstadoEncomiendaExterna.Ingresado || id_estado == (int)Constantes.EstadoEncomiendaExterna.Aprobado
                || id_estado == (int)Constantes.EstadoEncomiendaExterna.RechaZado)
                pnlExportarXML.Visible = true;
            else
                pnlExportarXML.Visible = false;

            if (id_estado == (int)Constantes.EstadoEncomiendaExterna.Aprobado)
                btnImprimirComprobante.Visible = true;
            else
                btnImprimirComprobante.Visible = false;

            if (Bloqueada)
            {
                btnImprimirComprobante.Visible = false;
                ddlCambiarEstado.Enabled = false;
                btnCambiarEstado.Visible = false;
            }

            if ((id_estado == (int)Constantes.EstadoEncomiendaExterna.RechaZado) && (TipoCertificado == (int)Constantes.TipoCertificado.EncomiendaLey257))
            {
                ddlCambiarEstado.Enabled = false;
                btnCambiarEstado.Enabled = false;
                btnCambiarEstado.CssClass = "btn btn-default";
            }

            lblTipoTramite.Text = Enum.GetName(typeof(Constantes.TipoCertificado), TipoCertificado);
            lblNroEncomienda.Text = nroTramite.ToString();

            var certificados = TraerCertificado(tipo_tramite, nroTramite);
            if (!certificados.Any())
            {
                repeater_certificados.Visible = false;
            }
            else
            {
                repeater_certificados.Visible = true;
                repeater_certificados.DataSource = certificados;
                repeater_certificados.DataBind();

                string url = string.Format(ResolveUrl("~/Reportes/ImprimirEncomiendaExt.aspx") + "?param={0}", certificados.FirstOrDefault().id_certificado);
                hid_url_reporte.Value = url;
            }
        }

        public IList<CertificadosDTO>  TraerCertificado(int tipoTramite, int nroTramite)
        {
            CertificadosBL certificadosBL = new CertificadosBL();

            return certificadosBL.GetByFKNroTipo(nroTramite, tipoTramite).ToList();
        }

        protected void btnCambiarEstado_Click(object sender, EventArgs e)
        {
            lblmpeInfo.Text = "";
            if (ddlCambiarEstado.SelectedIndex <= 0)
            {
                lblMensajeError.Text = "Debe seleccionar el estado por el cual se desea realizar el cambio.";
                mvBotonesAccion.ActiveViewIndex = 2;
                ScriptManager.RegisterClientScriptBlock(updBotonesAccion, updBotonesAccion.GetType(), "mostrarError", "moverFinal();", true);
                return;
            }

            try
            {
                int id_encomienda = int.Parse(hid_id_encomienda.Value);
                int id_estado = int.Parse(ddlCambiarEstado.SelectedValue);
                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                //jaraujo

                EncomiendaBL encomiendaBL = new EncomiendaBL();
                
                if (id_estado == (int)Constantes.EstadoEncomiendaExterna.Aprobado)
                {
                    ExternalServiceReporting reportingService = new ExternalServiceReporting();
                    ExternalServiceFiles esf = new ExternalServiceFiles();

                    byte[] pdfCertificado = new byte[0];
                    int id_fileCert = 0;
                    string fileName = "";
                    try
                    {
                        if (TipoCertificado == (int)(int)Constantes.TipoCertificado.EncomiendaLey257)
                        {
                            var ReportingEntityCertificado = reportingService.GetPDFCertificadoExtConsejoEncomienda(id_encomienda, true);
                            pdfCertificado = ReportingEntityCertificado.Reporte;
                            id_fileCert = pdfCertificado != null ? ReportingEntityCertificado.Id_file : 0;
                            fileName = ReportingEntityCertificado.FileName;
                        }
                        else if(TipoCertificado == (int)(int)Constantes.TipoCertificado.Formulario_inscripción_demoledores_excavadores)
                        {
                            var ReportingEntityCertificado = reportingService.GetPDFCertificadoExtConsejoEncomiendaDeEx(id_encomienda, true);
                            pdfCertificado = ReportingEntityCertificado.Reporte;
                            id_fileCert = pdfCertificado != null ? ReportingEntityCertificado.Id_file : 0;
                            fileName = ReportingEntityCertificado.FileName;
                        }
                        else
                        {
                            var ReportingEntityCertificado = reportingService.GetPDFCertificadoExtConsejoEncomienda(id_encomienda, true);
                            pdfCertificado = ReportingEntityCertificado.Reporte;
                            id_fileCert = pdfCertificado != null ? ReportingEntityCertificado.Id_file : 0;
                            fileName = ReportingEntityCertificado.FileName;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMensajeError.Text = $"Error Reporting, No se pudo generar pdf certificado. - GetPDFCertificadoConsejoEncomienda {ex.Message}";
                        this.EjecutarScript(updBotonesAccion, string.Format("mostrarPopup('{0}');", pnlMensajeError.ClientID));
                    }


                    if (pdfCertificado.Length == 0)
                        throw new Exception("No se pudo generar pdf certificado.");

                    if (pdfCertificado != null)
                    {

                        encomiendaBL.ActualizarEncomiendaEx_Estado(id_encomienda, id_estado, userid, id_fileCert);
                    }

                }
                else
                {
                    encomiendaBL.ActualizarEncomiendaEx_Estado(id_encomienda, id_estado, userid);
                }


                /////////////
                CargarDatosEncomienda(int.Parse(hid_id_encomienda.Value));
                CargarEstadosPosibles(int.Parse(hid_id_encomienda.Value));
                CargarHistorialEstados(int.Parse(hid_id_encomienda.Value));
            }
            catch (Exception ex)
            {
                lblMensajeError.Text = ex.Message;
                mvBotonesAccion.ActiveViewIndex = 2;
            }

            if (lblmpeInfo.Text.Length == 0)
            {
                mvBotonesAccion.ActiveViewIndex = 1;
                lblMensajeInfo.Text = "La encomienda fue actualizada exitosamente.";
            }

            ScriptManager.RegisterClientScriptBlock(updBotonesAccion, updBotonesAccion.GetType(), "mostrarError", "moverFinal();", true);

        }

        protected void btnMensajeVerIndex0_Click(object sender, EventArgs e)
        {
            mvBotonesAccion.ActiveViewIndex = 0;
            ScriptManager.RegisterClientScriptBlock(updBotonesAccion, updBotonesAccion.GetType(), "mostrarError", "moverFinal();", true);
        }

        protected void lnkCertificado_Command(object sender, CommandEventArgs e)
        {
            LinkButton lnkEliminar = (LinkButton)sender;
            int id_certificado = Convert.ToInt32(lnkEliminar.CommandArgument);
            string url = string.Format(ResolveUrl("~/Reportes/ImprimirEncomiendaExt.aspx") + "?param={0}", id_certificado);
            hid_url_reporte.Value = url;
        }
    }
}