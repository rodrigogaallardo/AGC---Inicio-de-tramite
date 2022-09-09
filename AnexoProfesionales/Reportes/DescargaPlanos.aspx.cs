using AnexoProfesionales.App_Components;
using BusinesLayer.Implementation;
using ExternalService;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AnexoProfesionales.Reportes
{
    public partial class DescargaPlanos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string param_id = "";
                int id = 0;

                try
                {
                    param_id = Page.RouteData.Values["id"].ToString();
                    byte[] bidfile = Convert.FromBase64String(HttpUtility.UrlDecode(param_id));
                    id = int.Parse(System.Text.Encoding.ASCII.GetString(bidfile));


                }
                catch (Exception)
                {
                    LogError.Write(new Exception("Error al convertir el id, parametro enviado: " + param_id));
                }

                DescargarPlano(id);
            }

        }


        private void DescargarPlano(int id)
        {
            try
            {
                EncomiendaPlanosBL blPlanos = new EncomiendaPlanosBL();
                var plano = blPlanos.Single(id);

                string arch = "plano-enc" + plano.id_encomienda.ToString() + "-" + plano.id_encomienda_plano.ToString() + "-" + plano.nombre_archivo;

                ExternalServiceFiles ser = new ExternalServiceFiles();
                string fileExtension;
                byte[] documento = ser.downloadFile(plano.id_file, out fileExtension);
                if (documento == null)
                    throw new Exception("No se encontro el id_file en la base de archivos.");

                System.IO.MemoryStream msPdf = new System.IO.MemoryStream();
                msPdf = new System.IO.MemoryStream(documento);

                //mostrar archivo
                Response.Clear();
                Response.Buffer = true;//false;
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + arch);
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