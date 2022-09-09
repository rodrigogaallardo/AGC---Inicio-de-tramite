using ExternalService;
using Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Reportes
{
    public partial class ImprimirEscribanos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Imprimir();
            }
        }

        private void Imprimir()
        {
            try
            {
                byte[] pdfEscribanos = new byte[0];

                ExternalServiceReporting reportingService = new ExternalServiceReporting();
                var ReportingEntity = reportingService.GetPDFEscribanos(false);

                pdfEscribanos = ReportingEntity.Reporte;
                string arch = ReportingEntity.FileName;
                if (pdfEscribanos != null && pdfEscribanos.Length > 0)
                {
                    Response.Clear();
                    Response.Buffer = true;//false;
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "inline;filename=" + arch);
                    Response.AddHeader("Content-Length", pdfEscribanos.Length.ToString());
                    Response.BinaryWrite(pdfEscribanos);
                    Response.Flush();
                }
                else
                {
                    Response.Clear();
                    Response.Write("No se ha podido recuperar el lisado de Escribanos.");
                }

            }
            catch
            {
                Server.Transfer("~/Errores/Error3005.aspx");
            }
        }
    }
}