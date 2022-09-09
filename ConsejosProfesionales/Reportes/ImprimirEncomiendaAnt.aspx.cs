using BusinesLayer.Implementation;
using ExternalService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsejosProfesionales.Reportes
{
    public partial class ImprimirEncomiendaAnt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ImprimirEncomienda();
                }
                catch (Exception ex)
                {
                    EnviarError(ex.Message);
                }

                Response.End();
            }

        }

        private void ImprimirEncomienda()
        {
            string str_id_encomienda = (Request.QueryString["id_encomienda"] == null) ? "" : Request.QueryString["id_encomienda"].ToString();

            if (str_id_encomienda.Length > 0)
            {

                int id_encomienda = int.Parse(str_id_encomienda);

                EncomiendaBL encomiendaBL = new EncomiendaBL();
                int IdFile = encomiendaBL.GetEncomiendaAntenasDocumentos(id_encomienda);

                ExternalServiceFiles services = new ExternalServiceFiles();
                string extension = string.Empty;

                byte[] pdf = services.downloadFile(IdFile, out  extension); 

                try
                {
                    string nombArch = "Encomienda-antenas-" + id_encomienda.ToString() + "." + extension;

                    //mostrar archivo
                    Response.Clear();
                    Response.Buffer = true;//false;
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "inline;filename=" + nombArch);
                    Response.AddHeader("Content-Length", pdf.Length.ToString());
                    Response.BinaryWrite(pdf);
                    Response.Flush();
                    Response.Close();

                }
                catch
                {
                    EnviarError("Se produjo un error al recuperar el pdf.");
                }
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