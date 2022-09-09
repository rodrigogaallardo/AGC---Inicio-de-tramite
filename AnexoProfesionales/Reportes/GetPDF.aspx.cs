using AnexoProfesionales.App_Components;
using ExternalService;
using StaticClass;
using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;

namespace AnexoProfesionales.Reportes
{
    public partial class GetPDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string param_id = "";
                int id_file = 0;

                try
                {
                    param_id = Page.RouteData.Values["id_file"].ToString();
                    byte[] bidfile = Convert.FromBase64String(HttpUtility.UrlDecode(param_id));
                    id_file = int.Parse(System.Text.Encoding.ASCII.GetString(bidfile));

                }
                catch (Exception)
                {
                    LogError.Write(new Exception("Error al convertir el id_file, parametro enviado: " + param_id));
                }

                DescargarFile(id_file);
            }
        }
        private void DescargarFile(int id_file)
        {
            try
            {
                ExternalServiceFiles ser = new ExternalServiceFiles();
                string fileExtension;
                byte[] documento = ser.downloadFile(id_file, out fileExtension);
                if (documento == null)
                    throw new Exception("No se encontro el id_file en la base de archivos.");

                StringBuilder sb = new StringBuilder();
                foreach (char c in fileExtension)
                {
                    if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                        sb.Append(c);
                }
                string nam = sb.ToString().Replace("-", "");

                string nombArch = "Documento-Adjunto-" + id_file.ToString() + Path.GetExtension(nam);
                System.IO.MemoryStream msPdf = new System.IO.MemoryStream();
                msPdf = new System.IO.MemoryStream(documento);

                //mostrar archivo
                Response.Clear();
                Response.Buffer = true;//false;
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + nombArch);
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