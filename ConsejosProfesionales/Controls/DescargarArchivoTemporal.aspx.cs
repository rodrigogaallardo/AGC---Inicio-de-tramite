using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsejosProfesionales.Controls
{
    public partial class DescargarArchivoTemporal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DescargarArchivo();
            }
        }

        private void DescargarArchivo()
        {
            string filename = (Request.QueryString["fname"] != null ? Request.QueryString["fname"].ToString() : "");
            filename = HttpUtility.UrlDecode(filename);
            string filepath = "C:\\Temporal\\" + filename;
            

            if (filename.Length > 0)
            {

                Response.Clear();
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", filename));

                if (filename.EndsWith(".xls"))
                    Response.ContentType = "application/vnd.ms-excel";
                if (filename.EndsWith(".xlsx"))
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                Response.Charset = "utf-8";

                Response.WriteFile(filepath);

                Response.End();
            }
        }
    }
}