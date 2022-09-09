using ExternalService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;

namespace SSIT
{
    /// <summary>
    /// Descripción breve de Download
    /// </summary>
    public class Download : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (Membership.GetUser() == null)
            {
                context.Response.Clear();
                context.Response.Write("Debe estar logueado para estar en esta seccion");
            }
            else
            {
                if (context.Request.QueryString["Id"] != null)
                {
                    ExternalServiceFiles esFiles = new ExternalServiceFiles();

                    byte[] Pdf = new byte[0];
                    int id_file = Convert.ToInt32(context.Request.QueryString["Id"]);
                    string FileName = string.Empty;
                    if (id_file > 0)
                    {
                        try
                        {
                            Pdf = esFiles.downloadFile(id_file, out FileName);
                        }
                        catch { }
                    }

                    if (Pdf != null && Pdf.Length > 0)
                    {
                        //string nombArch = "Documento-Adjunto-" + id_file.ToString() + FileName;

                        context.Response.Clear();
                        context.Response.Buffer = true;//false;
                        context.Response.ContentType = "application/octet-stream";
                        context.Response.AddHeader("Content-Disposition", "inline;filename=" + FileName);
                        context.Response.AddHeader("Content-Length", Pdf.Length.ToString());
                        context.Response.BinaryWrite(Pdf);
                        context.Response.Flush();

                    }
                    else
                    {
                        context.Response.Clear();
                        context.Response.Write("No es posible encontrar el archivo");
                    }
                }
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}