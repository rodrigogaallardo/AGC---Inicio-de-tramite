using BusinesLayer.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsejosProfesionales.Reportes
{
    public partial class ImprimirEncomiendaExt : System.Web.UI.Page
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
            string valor = (Request.QueryString["param"] == null) ? "" : Request.QueryString["param"].ToString();
            int id_certificado = 0;
            if (valor != null)
            {
                int.TryParse(valor, out id_certificado);
            }
            CertificadosBL certificadoBL = new CertificadosBL();
 
            var ds = certificadoBL.Single(id_certificado);
            if (ds == null)
            {
                throw new Exception("No se pudo encontarar el certificado "+id_certificado);
            }
            try
            {
                string nombArch = "Encomienda-" + id_certificado.ToString() + ".pdf";
                System.IO.MemoryStream msPdf = new System.IO.MemoryStream();
                msPdf = new System.IO.MemoryStream(ds.Certificado);

                //mostrar archivo
                Response.Clear();
                Response.Buffer = true;//false;
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "inline;filename=" + nombArch);
                Response.AddHeader("Content-Length", msPdf.Length.ToString());
                Response.BinaryWrite(msPdf.ToArray());
                Response.Flush();
                Response.Close();
                //Response.End();
            }
            catch (Exception)
            {
                throw new Exception("Se produjo un error al cargar el certificado "+id_certificado);
            }
        }


        private void EnviarError(string mensaje)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "text/HTML";
            Response.AddHeader("Content-Disposition", "inline;filename=error.html");
            Response.Write(mensaje);
            Response.Flush();
            //Response.End();
        }
    }
}