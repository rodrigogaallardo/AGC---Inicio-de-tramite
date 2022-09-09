using ExternalService;
using SSIT.App_Components;
using StaticClass;
using System;
using System.Web;
using System.Web.UI;
using BusinesLayer.Implementation;
using DataTransferObject.Engine;

namespace SSIT.Reportes
{
    public partial class GetActa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string param_id = "";
                int id_file = 0;

                //int id_file2 = Convert.ToInt32(HttpContext.Current.Request.QueryString["id_certificado"]);
                try
                {
                    param_id = HttpContext.Current.Request.QueryString["id_certificado"].ToString();
                    //byte[] bidfile = Convert.FromBase64String(HttpUtility.UrlDecode(param_id));
                    //id_file = int.Parse(System.Text.Encoding.ASCII.GetString(bidfile));
                    int.TryParse(param_id, out id_file);
                    

                }
                catch (Exception)
                {
                    LogError.Write(new Exception("Error al convertir el id_file, parametro enviado: " + param_id));
                }

                DescargarFile(id_file);
            }
        }
        private void DescargarFile(int id_certificado)
        {
            try
            {
                ExternalServiceFiles ser = new ExternalServiceFiles();
                byte[] documento = ser.downloadFileByidCertificado(id_certificado);
                if (documento == null)
                    throw new Exception("No se encontro el Certificado en la base de archivos.");

                System.IO.MemoryStream msPdf = new System.IO.MemoryStream();
                msPdf = new System.IO.MemoryStream(documento);

                //mostrar archivo
                Response.Clear();
                Response.Buffer = true;//false;
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + "CertificadoActaNotarial_" + id_certificado + ".pdf");
                Response.AddHeader("Content-Length", msPdf.Length.ToString());
                Response.BinaryWrite(msPdf.ToArray());
                Response.Flush();


            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error al recuperar el pdf: " + Environment.NewLine + ex.Message);
            }
        }

    }
}