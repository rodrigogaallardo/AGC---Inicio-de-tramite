using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinesLayer.Implementation;
using StaticClass;
using System.IO;
using Reporting;
using ExternalService;

namespace ConsejosProfesionales.Reportes
{
    public partial class ImprimirComprobante : SecurePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int tipo = (Request.QueryString["tipo"] == null) ? 0 : Convert.ToInt32(Request.QueryString["tipo"]);
                    int nro_tramite = (Request.QueryString["nro_tramite"] == null) ? 0 : Convert.ToInt32(Request.QueryString["nro_tramite"]);
                    Guid userid = Guid.Empty;
                    if (Request.QueryString["userid"] != null)
                        Guid.TryParse(Request.QueryString["userid"].ToString(), out userid);

                    if (tipo == 1)
                        ImprimirCertificadoEncomienda(nro_tramite, userid);
                    else
                        ImprimirCertificadoEncomiendaExterna(tipo, nro_tramite, userid);

                }
                catch (Exception ex)
                {
                    EnviarError(ex.Message);
                }
                Response.End();
            }
        }
        private void ImprimirCertificadoEncomiendaExterna(int tipo_tramite, int nroTramite, Guid userid)
        {

            var documento = CommonReport.GenerarCertificadoExtConsejo(tipo_tramite, nroTramite);
            if (documento == null)
                throw new Exception("No se pudo generar pdf certificado.");


            string arch = "certificado-consejo-" + nroTramite.ToString() + ".pdf";

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + arch);
            Response.AddHeader("Content-Length", documento.Length.ToString());

            Response.BinaryWrite(Funciones.StreamToArray(documento));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_encomienda"></param>
        /// <param name="userid"></param>
        private void ImprimirCertificadoEncomienda(int id_encomienda, Guid userid)
        {
            EncomiendaDocumentosAdjuntosBL documentos = new EncomiendaDocumentosAdjuntosBL();

            var adjunto = documentos.GetByFKIdEncomiendaTipoSis(id_encomienda, (int)Constantes.TiposDeDocumentosSistema.CERTIF_CONSEJO_HABILITACION).FirstOrDefault();
            
            string fileName = string.Empty;
            byte[] documento = null;
            string arch = "";
            if (adjunto != null)
            {
                ExternalServiceFiles externalServices = new ExternalServiceFiles();
                documento = externalServices.downloadFile(adjunto.id_file, out fileName);
                arch = fileName;
            }

            if (documento == null)
            {
                ExternalServiceReporting reportingService = new ExternalServiceReporting();
                var ReportingEntity = reportingService.GetPDFCertificadoConsejoEncomienda(id_encomienda, false);

                documento = ReportingEntity.Reporte;
                arch = ReportingEntity.FileName;
                if (documento == null)
                    throw new Exception("No se pudo generar pdf certificado.");
            
            }

                        
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + arch);
            Response.AddHeader("Content-Length", documento.Length.ToString());

            Response.BinaryWrite(documento);

        }

        private void EnviarError(string mensaje)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "text/HTML";
            Response.AddHeader("Content-Disposition", "inline;filename=error.html");
            Response.Write("Error: " + mensaje);
            Response.Flush();

        }
    }
}