using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinesLayer.Implementation;
using StaticClass;
using System.IO;
using Reporting;

namespace ConsejosProfesionales.Reportes
{
    public partial class ImprimirComprobanteAnt : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                try
                {

                    int id_encomienda = (Request.QueryString["id_encomienda"] == null) ? 0 : Convert.ToInt32(Request.QueryString["id_encomienda"]);
                    ImprimirCertificadoEncomiendaAnt(id_encomienda);

                }
                catch (Exception ex)
                {
                    EnviarError(ex.Message);
                }

                Response.End();

            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_encomienda"></param>
        private void ImprimirCertificadoEncomiendaAnt(int id_encomienda)
        {

            Stream documento = CommonReport.ImprimirCertificadoEncomiendaAnt(id_encomienda);

            if (documento == null)
                throw new Exception("No se pudo generar pdf certificado.");


            string arch = "certificado-antenas-" + id_encomienda.ToString() + ".pdf";

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + arch);
            Response.AddHeader("Content-Length", documento.Length.ToString());

            Response.BinaryWrite(Funciones.StreamToArray(documento));

            documento.Dispose();

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