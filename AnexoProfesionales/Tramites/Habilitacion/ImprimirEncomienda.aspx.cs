using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AnexoProfesionales.App_Components;
using Reporting;
using StaticClass;
using BusinesLayer.Implementation;
using ExternalService;

namespace AnexoProfesionales.Tramites.Habilitacion
{
    public partial class ImprimirEncomienda : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Imprimir();
                }
                catch (Exception ex)
                {
                    LogError.Write(ex);
                    Response.Write("No es posible imprimir la encomienda.");
                }

            }
        }

        private void Imprimir()
        {
            if (Page.RouteData.Values["id"] != null)
            {
                ExternalServiceFiles esFiles = new ExternalServiceFiles();
                EncomiendaBL encBL = new EncomiendaBL();
                EncomiendaDocumentosAdjuntosBL encDocAdjBL = new EncomiendaDocumentosAdjuntosBL();
                TiposDeDocumentosSistemaBL tipDocSis = new TiposDeDocumentosSistemaBL();

                int id_tipodocsis;
                byte[] Pdf = new byte[0];
                int id_file = 0;
                int id_encomienda;

                id_tipodocsis = tipDocSis.GetByCodigo("ENCOMIENDA_DIGITAL").id_tipdocsis;
                id_encomienda = Funciones.ConvertFromBase64StringToInt32(Page.RouteData.Values["id"].ToString());
                id_file = encDocAdjBL.GetByFKIdEncomiendaTipoSis(id_encomienda, id_tipodocsis).Select(s => s.id_file).FirstOrDefault();


                if (id_file > 0)
                {
                    string fileExtension;
                    //Usando el servicio para traer de la base de datos
                    Pdf = esFiles.downloadFile(id_file, out fileExtension);

                    //Regenerando el reporte
                    //Pdf = CommonReport.GetPDFEncomienda(id_encomienda);
                }

                if (Pdf != null && Pdf.Length > 0 )
                {

                    Response.Clear();
                    Response.Buffer = true;//false;
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "inline;filename=Encomienda-" + id_encomienda.ToString() + ".pdf");
                    Response.AddHeader("Content-Length", Pdf.Length.ToString());
                    Response.BinaryWrite(Pdf);
                    Response.Flush();

                }
                else
                {
                    Response.Clear();
                    Response.Write("No es posible imprimir la Encomienda" + id_encomienda.ToString());
                }
            }
            else
            {
                Server.Transfer("~/Errores/Error3001.aspx");
            }

        }


    }
}