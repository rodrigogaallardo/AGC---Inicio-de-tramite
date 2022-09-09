using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Solicitud
{
    public partial class Tramite : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id_solicitud = 0;
                int.TryParse(Convert.ToString(Page.RouteData.Values["id_solicitud"]), out id_solicitud);
                int id_Tad = 0;
                int.TryParse(Convert.ToString(Page.RouteData.Values["id_tad"]), out id_Tad);

                SSITSolicitudesBL sSITSolicitudesBL = new SSITSolicitudesBL();
                ConsultaPadronSolicitudesBL consultaPadronSolicitudesBL = new ConsultaPadronSolicitudesBL();
                TransferenciasSolicitudesBL transferenciasSolicitudesBL = new TransferenciasSolicitudesBL();
                ConsultaPadronSolicitudesDTO cp = null;
                SSITSolicitudesDTO sol = null;
                TransferenciasSolicitudesDTO tr = null;
                try
                {
                    sol = sSITSolicitudesBL.Single(id_solicitud);
                }
                catch (Exception) { }
                try
                {
                    cp = consultaPadronSolicitudesBL.Single(id_solicitud);
                }
                catch (Exception) { }
                try
                {
                    tr = transferenciasSolicitudesBL.Single(id_solicitud);
                }
                catch (Exception) { }

                if (sol != null && sol.idTAD != null && sol.idTAD.Value == id_Tad)
                {
                    if (sol.IdTipoTramite == (int)Constantes.TipoDeTramite.Habilitacion)
                        Response.Redirect(string.Format("~/" + RouteConfig.VISOR_SOLICITUD + "{0}", sol.IdSolicitud));
                    else if (sol.IdTipoTramite == (int)Constantes.TipoDeTramite.Ampliacion)
                        Response.Redirect(string.Format("~/" + RouteConfig.VISOR_SOLICITUD_AMPLIACION + "{0}", sol.IdSolicitud));
                    else if (sol.IdTipoTramite == (int)Constantes.TipoDeTramite.RedistribucionDeUso)
                        Response.Redirect(string.Format("~/" + RouteConfig.VISOR_SOLICITUD_REDISTRIBUCION_USO + "{0}", sol.IdSolicitud));
                }
                else if (cp != null && cp.idTAD != null && cp.idTAD.Value == id_Tad)
                {
                    Response.Redirect(string.Format("~/" + RouteConfig.VISOR_CPADRON + "{0}", cp.IdConsultaPadron));
                }
                else if (tr != null && tr.idTAD != null && tr.idTAD.Value == id_Tad)
                {
                    Response.Redirect(string.Format("~/" + RouteConfig.VISOR_TRANSFERENCIAS + "{0}", tr.IdSolicitud));
                }
                else
                    Response.Redirect("~/Errores/Error3004.aspx");
            }
        }
    }
}