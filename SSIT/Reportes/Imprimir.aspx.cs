using BusinesLayer.Implementation;
using DataTransferObject;
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

    

    public partial class Imprimir : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Imprimirsolicitud();
                }
                catch (Exception ex)
                {
                    EnviarError(ex.Message);
                }
                Response.End();

            }


        }

        private void Imprimirsolicitud()
        {
            String param_id = Page.RouteData.Values["id_solicitud"].ToString();
            byte[] bidfile = Convert.FromBase64String(HttpUtility.UrlDecode(param_id));
            int id_solicitud = int.Parse(System.Text.Encoding.ASCII.GetString(bidfile));


            ExternalServiceReporting reportingService = new ExternalServiceReporting();
            //var ReportingEntity = reportingService.GetPDFTransferencia(id_solicitud, false);
            var ReportingEntity = reportingService.GetPDFTransmision(id_solicitud, false);

            string arch = ReportingEntity.FileName;
            byte[] pdfSolicitud = new byte[0];

            pdfSolicitud = ReportingEntity.Reporte;

            Response.Clear();
            Response.Buffer = true;//false;
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + arch);

            Response.BinaryWrite(pdfSolicitud);

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