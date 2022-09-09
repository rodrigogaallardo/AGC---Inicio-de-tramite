using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinesLayer.Implementation;
 
namespace ConsejosProfesionales.Reportes
{
    public partial class ImprimirEncomienda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ImprimeEncomienda();
                }
                catch (Exception ex)
                {
                    EnviarError(ex.Message);
                }
            }
        }

        private void ImprimeEncomienda()
        {
            try
            {
                string str_id_encomienda = (Request.QueryString["id_encomienda"] == null) ? "" : Request.QueryString["id_encomienda"].ToString();
                int id_encomienda = 0;
                int.TryParse(str_id_encomienda, out id_encomienda);

                string arch = "Encomienda-" + id_encomienda.ToString() + ".pdf";

                EncomiendaDocumentosAdjuntosBL encomiendaDocAdjuntos = new EncomiendaDocumentosAdjuntosBL();
 
                var ds = encomiendaDocAdjuntos.GetByFKIdEncomiendaTipoSis(id_encomienda, "ENCOMIENDA_DIGITAL");


                if (ds == null)
                    throw new Exception(string.Format("No se encontró el archivo de la encomienda -> id_encomienda = {0}.", id_encomienda));

                ExternalService.ExternalServiceFiles files = new ExternalService.ExternalServiceFiles();
                
                string extension = string.Empty;
                byte[] a = files.downloadFile(ds.FirstOrDefault().id_file, out extension) ;
                
                Response.Clear();
                Response.Buffer = true;//false;
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "inline;filename=" + arch );
                Response.AddHeader("Content-Length", a.Length.ToString());
                Response.BinaryWrite(a);
                Response.Flush();
                Response.End();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void EnviarError(string mensaje)
        {
            Response.Clear();
            Response.Write(mensaje);
            Response.Flush();
            Response.End();
        }
    }
}