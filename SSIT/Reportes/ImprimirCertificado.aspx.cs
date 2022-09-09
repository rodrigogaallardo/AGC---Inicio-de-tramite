using BusinesLayer.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Reportes
{
    public partial class ImprimirCertificado : System.Web.UI.Page
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
                if (Page.RouteData.Values["id_certificado"] != null)
                {

                    int id_certificado = int.Parse(Page.RouteData.Values["id_certificado"].ToString());

                    CertificadosBL certBL = new CertificadosBL();

                    var certDTO  = certBL.Single(id_certificado);

                    byte[] PdfCertificado = null;

                    if (certDTO != null)
                        PdfCertificado = certDTO.Certificado;

                    if (PdfCertificado != null && PdfCertificado.Length > 0)
                    {
                        Response.Clear();
                        Response.Buffer = true;//false;
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("Content-Disposition", "inline;filename=" + id_certificado.ToString() + ".pdf");
                        Response.AddHeader("Content-Length", PdfCertificado.Length.ToString());
                        Response.BinaryWrite(PdfCertificado);
                        Response.Flush();
                    }
                    else
                    {
                        Response.Clear();
                        Response.Write("No se ha podido recuperar el pdf del certificado. ");
                    }
                }
                else
                {
                    Response.Clear();

                }
            }
            catch
            {
                Server.Transfer("~/Errores/Error3005.aspx");
            }
        }
    }
}