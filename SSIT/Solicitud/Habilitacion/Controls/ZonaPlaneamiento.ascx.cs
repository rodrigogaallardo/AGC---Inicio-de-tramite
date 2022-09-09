using BusinesLayer.Implementation;
using DataTransferObject;
using SSIT.App_Components;
using SSIT.Common;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSIT.Solicitud.Habilitacion.Controls;

namespace SSIT.Solicitud.Habilitacion.Controls
{
    public partial class ZonaPlaneamiento : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(updZonaPlaneamiento, updZonaPlaneamiento.GetType(), "hideModalAPH", "hideModalAPH();",true); 
        }
        public void mostrarPlaneamiento (int idUbicacion)
            {
                UbicacionesBL ubicacionesBL = new UbicacionesBL();
                bool hayLabel = false;
                var item = ubicacionesBL.Single(idUbicacion);

                if ((item.ZonasPlaneamiento.CodZonaPla.Contains("I")) && !(!item.ZonasPlaneamiento.CodZonaPla.Contains("Rb")))
                    {
                            lblZonaSeleccionadaI.Style["Display"] = "block";
                            hayLabel = true;
                    }

                if ((item.ZonasPlaneamiento.CodZonaPla.Contains("P")) && (!item.ZonasPlaneamiento.CodZonaPla.Contains("ADP")) && (!(item.ZonasPlaneamiento.CodZonaPla.Contains("APH"))))
                    {
                        lblZonaSeleccionadaP.Style["Display"] = "block";
                            hayLabel = true;
                    }

                if (item.ZonasPlaneamiento.CodZonaPla.Contains("U"))
                    {
                        lblZonaSeleccionadaU.Style["Display"] = "block";
                            hayLabel = true;
                    }

                if (item.ZonasPlaneamiento.CodZonaPla.Contains("AR"))
                    {
                        lblZonaSeleccionadaAR.Style["Display"] = "block";
                            hayLabel = true;
                    }

                if (item.ZonasPlaneamiento.CodZonaPla.Contains("RU"))
                    {
                        lblZonaSeleccionadaRU.Style["Display"] = "block";
                            hayLabel = true;
                    }

                if (item.ZonasPlaneamiento.CodZonaPla.Contains("UF"))
                    {
                        lblZonaSeleccionadaUF.Style["Display"] = "block";
                            hayLabel = true;
                    }

                if (item.ZonasPlaneamiento.CodZonaPla.Contains("UP"))
                    {
                        lblZonaSeleccionadaUP.Style["Display"] = "block";
                            hayLabel = true;  
                    }

                if (item.ZonasPlaneamiento.CodZonaPla.Contains("ARE"))
                    {
                        lblZonaSeleccionadaARE.Style["Display"] = "block";
                            hayLabel = true;
                    }

                if (item.ZonasPlaneamiento.CodZonaPla.Contains("ADP"))
                    {
                        lblZonaSeleccionadaADP.Style["Display"] = "block";
                            hayLabel = true;
                    }
                if (item.ZonasPlaneamiento.CodZonaPla.Contains("APH"))
                {
                    lblZonaSeleccionadaAPH.Style["Display"] = "block";
                    hayLabel = true;
                }
                if (hayLabel)
                {
                    pnlZonaPlaneamiento.Style["Display"] = "inline block";
                }
                else
                {
                    pnlZonaPlaneamiento.Style["Display"] = "none";
                }

           }
      
    }
       
}