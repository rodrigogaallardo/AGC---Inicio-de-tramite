using BusinesLayer.Implementation;
using SSIT.App_Components;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Reportes
{
    public partial class ImprimirActaNotarial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string param_id = string.Empty;
                string id_actanotarial = string.Empty;

                try
                {
                    param_id = Page.RouteData.Values["id_actanotarial"].ToString();
                    byte[] bidfile = Convert.FromBase64String(HttpUtility.UrlDecode(param_id));
                    id_actanotarial = Convert.ToString(System.Text.Encoding.ASCII.GetString(bidfile));

                }
                catch (Exception)
                {
                    LogError.Write(new Exception("Error al convertir el id_file, parametro enviado: " + param_id));
                }

                DescargarFile(id_actanotarial);
            }
        }
        private void DescargarFile(string id_actanotarial)
        {
            CertificadosBL blCert = new CertificadosBL();
            var cert = blCert.GetByFKNroTipo(id_actanotarial, (int)Constantes.TipoTramiteCertificados.Certificado_Acta_Notarial_Encomienda).FirstOrDefault();

            if (cert == null)
                throw new Exception("No se pudo encontarar pdf.");

            try
            {

                string nombArch = "actaNotarial-" + id_actanotarial.ToString() + ".pdf";
                System.IO.MemoryStream msPdf = new System.IO.MemoryStream();
                msPdf = new System.IO.MemoryStream(cert.Certificado);

                //mostrar archivo
                Response.Clear();
                Response.Buffer = true;//false;
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "inline;filename=" + nombArch);
                Response.AddHeader("Content-Length", msPdf.Length.ToString());
                Response.BinaryWrite(msPdf.ToArray());
                Response.Flush();
            }
            catch (Exception)
            {
                throw new Exception("Se produjo un error al enviar pdf.");
            }
        }

    }
}