using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace ConsejosProfesionales
{
    public static class RouteConfig
    {

        public static string ACTUALIZA_ENCOMIENDA_EXTERNA = "Encomiendas/ActualizarEncomiendasExterna/";
        public static string ACTUALIZA_ENCOMIENDA_ANTENA = "Encomiendas/ActualizarEncomiendasAntena/";
        public static string ACTUALIZA_ENCOMIENDA = "Encomiendas/ActualizarEncomiendas/";
        public static string DETALLE_ENCOMIENDA_OBRA = "Encomiendas/DetalleEncomiendaObra1/";

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.EnableFriendlyUrls();

            routes.MapPageRoute("ActualizarEncomiendasAntena", ACTUALIZA_ENCOMIENDA_ANTENA + "{id_encomienda}/{tipo_tramite}", "~/Encomiendas/ActualizarEncomiendasAnt.aspx");
            routes.MapPageRoute("ActualizarEncomiendasExterna", ACTUALIZA_ENCOMIENDA_EXTERNA + "{id_encomienda}/{tipo_tramite}", "~/Encomiendas/ActualizarEncomiendasExt.aspx");
            routes.MapPageRoute("ActualizarEncomiendas", ACTUALIZA_ENCOMIENDA + "{id_encomienda}/{tipo_tramite}", "~/Encomiendas/ActualizarTramite.aspx");
            routes.MapPageRoute("Detalle_Encomiendas_Obra", DETALLE_ENCOMIENDA_OBRA + "{id_encomienda}/{tipo_tramite}", "~/Encomiendas/DetalleEncomiendaObra.aspx");
            
        }
    }
}
