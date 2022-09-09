﻿using BusinesLayer.Implementation;
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
    public partial class ImprimirSolicitudNueva : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    Imprimirsolicitud();
                }
                catch (Exception ex)
                {
                    EnviarError(ex.Message);
                }
                Response.End();

            }


        }

        private void Imprimirsolicitud()
        {
            int id_solicitud = 0;
            if (Page.RouteData.Values["id_solicitud"] != null)
            {
                String param_id = Page.RouteData.Values["id_solicitud"].ToString();
                byte[] bidfile = Convert.FromBase64String(HttpUtility.UrlDecode(param_id));
                id_solicitud = int.Parse(System.Text.Encoding.ASCII.GetString(bidfile));
            }
            else
            {
                id_solicitud = Convert.ToInt32(Request.QueryString["id"]);
            }
            byte[] pdfSolicitud = new byte[0];

            ExternalServiceReporting reportingService = new ExternalServiceReporting();
            var ReportingEntity = reportingService.GetPDFSolicitudNueva(id_solicitud, false);

            string arch = ReportingEntity.FileName;

            pdfSolicitud = ReportingEntity.Reporte;
            
            Response.Clear();
            Response.Buffer = true;//false;
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + arch);

            Response.BinaryWrite(pdfSolicitud);

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