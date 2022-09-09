using BusinesLayer.Implementation;
using DataTransferObject;
using ExternalService;
using Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Reportes
{
    public partial class ObtenerDoc : System.Web.UI.Page
    {
        private int id { get; set; } 
        private int id_tipo { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    try
                    {
                        string strID = (Request.QueryString["id_tipo"] == null) ? "" : Request.QueryString["id_tipo"].ToString();
                        if (string.IsNullOrEmpty(strID))
                        {
                            strID = Page.RouteData.Values["id_tipo"].ToString();
                            byte[] data = Convert.FromBase64String(strID);
                            strID = System.Text.Encoding.ASCII.GetString(data);
                        }
                        id_tipo = (strID == null) ? 0 : Convert.ToInt32(strID);

                        string sid = (Request.QueryString["id"] == null) ? "" : Request.QueryString["id"].ToString();
                        if (string.IsNullOrEmpty(sid))
                        {
                            sid = Page.RouteData.Values["id"].ToString();
                            byte[] data = Convert.FromBase64String(sid);
                            sid = System.Text.Encoding.ASCII.GetString(data);
                        }
                        Guid GuidId = new Guid(sid);
                        id = (sid == null) ? 0 : Guid2Int(GuidId);

                        DescargarFile();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
                catch (Exception ex)
                {
                    EnviarError(ex.Message);
                }
                Response.End();

            }

        }

        private int Guid2Int(Guid value)
        {
            byte[] b = value.ToByteArray();
            int bint = BitConverter.ToInt32(b, 0);
            return bint - 10240;
        }

        private void DescargarFile()
        {
            try
            {
                ExternalServiceFiles ser = new ExternalServiceFiles();
                //Busco el idFile del archivo
                int id_file = 0;
                byte[] Pdf = new byte[0];
                string FileName = string.Empty;

                switch(id_tipo) //Se usa por si hay que hacer busquedas en base al idRecibido
                {
                    case 1: //Se recibedirectamente el id_file, no se hace busqueda
                        id_file = id;
                        FileName = "";
                        break;
                }

                if (id_file > 0)
                {
                    Pdf = ser.downloadFile(id_file, out FileName);
                }

                if (Pdf != null && Pdf.Length > 0)
                {
                    Response.Clear();
                    Response.Buffer = true;//false;
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", "inline;filename=" + FileName);
                    Response.AddHeader("Content-Length", Pdf.Length.ToString());
                    Response.BinaryWrite(Pdf);
                    Response.Flush();
                }
                else
                {
                    Response.Clear();
                    Response.Write("No es posible encontrar el archivo");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un error al recuperar el archivo. \"" + ex.Message + "\"");
            }
        }

        private void EnviarError(string mensaje)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "text/HTML";
            Response.AddHeader("Content-Disposition", "inline;filename=error.html");
            Response.Write("Error: " + mensaje);
            Response.Flush();

        }
    }
}