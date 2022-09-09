using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExternalService;
using BusinesLayer.Implementation;

namespace SSIT.Reportes
{
    public partial class ImprimirBoletaUnica : System.Web.UI.Page
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
                if (Page.RouteData.Values["id_pago"] != null)
                {

                    int id_pago = int.Parse(Page.RouteData.Values["id_pago"].ToString());

                    ExternalServicePagos servicePagos = new ExternalServicePagos();

                    byte[] PdfBoletaUnica = servicePagos.GetPDFBoletaUnica(id_pago);

                    if (PdfBoletaUnica != null && PdfBoletaUnica.Length > 0)
                    {
                        Response.Clear();
                        Response.Buffer = true;//false;
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("Content-Disposition", "inline;filename=" + id_pago.ToString() + ".pdf");
                        Response.AddHeader("Content-Length", PdfBoletaUnica.Length.ToString());
                        Response.BinaryWrite(PdfBoletaUnica);
                        Response.Flush();
                    }
                    else
                    {
                        Response.Clear();
                        Response.Write("El servicio de pagos no ha podido recuperar el pdf de la boleta. ");
                    }
                }
                else
                {
                    Response.Clear();
                    Response.Write("No se ha podido generar la boleta. url: " + Request.RawUrl);

                }
            }
            catch
            {
                Server.Transfer("~/Errores/Error3006.aspx");
            }
        }
    }
}