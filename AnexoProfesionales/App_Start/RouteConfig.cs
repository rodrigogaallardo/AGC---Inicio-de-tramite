using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace AnexoProfesionales
{
    public static class RouteConfig
    {
        public static string VISOR_ENCOMIENDA = "Tramites/Habilitacion/Visualizar/";
        public static string IMPRIMIR_ENCOMIENDA = "Tramites/Habilitacion/Imprimir/";

        public static string INICIAR_ENCOMIENDA = "Tramites/Habilitacion/Iniciar";

        public static string VISOR_ENCOMIENDA_TITULAR = "Tramites/Habilitacion/VisualizarTitulares/";
        public static string EDITAR_ENCOMIENDA_UBICACION = "Tramites/Habilitacion/EditarUbicacion/";
        public static string EDITAR_ENCOMIENDA_RUBROS = "Tramites/Habilitacion/EditarRubros/";
        public static string EDITAR_ENCOMIENDA_RUBROSCN = "Tramites/Habilitacion/EditarRubrosCN/";
        public static string EDITAR_ENCOMIENDA_TITULAR = "Tramites/Habilitacion/EditarTitulares/";
        public static string EDITAR_ENCOMIENDA_DATOSLOCAL = "Tramites/Habilitacion/EditarDatosLocal/";
        public static string EDITAR_ENCOMIENDA_CERTIFICADOSOBRECARGA = "Tramites/Habilitacion/EditarCertificadoSobrecarga/";
        public static string EDITAR_ENCOMIENDA_CARGAPLANO = "Tramites/Habilitacion/EditarCargarPlano/";
        public static string EDITAR_ENCOMIENDA_CONFORMACIONLOCAL = "Tramites/Habilitacion/EditarConformacionLocal/";

        public static string AGREGAR_ENCOMIENDA_UBICACION = "Tramites/Habilitacion/AgregarUbicacion/";
        public static string AGREGAR_ENCOMIENDA_RUBROS = "Tramites/Habilitacion/AgregarRubros/";
        public static string AGREGAR_ENCOMIENDA_TITULAR = "Tramites/Habilitacion/AgregarTitulares/";
        public static string AGREGAR_ENCOMIENDA_DATOSLOCAL = "Tramites/Habilitacion/AgregarDatosLocal/";
        public static string AGREGAR_ENCOMIENDA_CERTIFICADOSOBRECARGA = "Tramites/Habilitacion/AgregarCertificadoSobrecarga/";
        public static string AGREGAR_ENCOMIENDA_CARGAPLANO = "Tramites/Habilitacion/AgregarCargarPlano/";
        public static string AGREGAR_ENCOMIENDA_CONFORMACIONLOCAL = "Tramites/Habilitacion/AgregarConformacionLocal/";
        public static string BANDEJA_DE_ENTRADA = "Tramites/Bandeja";

        public const string DESCARGA_FILE = "DescargarFile/";
        public const string DESCARGA_PLANO = "DescargarPlano/";

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.EnableFriendlyUrls();

            routes.MapPageRoute("VisorTramite_", VISOR_ENCOMIENDA + "{id_encomienda}", "~/Tramites/Habilitacion/VisorTramite.aspx");
            routes.MapPageRoute("ImprimirEncomienda_", IMPRIMIR_ENCOMIENDA + "{id}", "~/Tramites/Habilitacion/ImprimirEncomienda.aspx");

            routes.MapPageRoute("IniciarEncomienda_", INICIAR_ENCOMIENDA, "~/Tramites/Habilitacion/InicioTramite.aspx");

            routes.MapPageRoute("Bandeja_", BANDEJA_DE_ENTRADA, "~/Tramites/BandejaDeEntrada.aspx");

            routes.MapPageRoute("VisorTitulares_", VISOR_ENCOMIENDA_TITULAR + "{id_encomienda}", "~/Tramites/Habilitacion/VisorTitulares.aspx");
            routes.MapPageRoute("Ubicacion_", EDITAR_ENCOMIENDA_UBICACION+"{id_encomienda}", "~/Tramites/Habilitacion/Ubicacion.aspx");
            routes.MapPageRoute("Rubros_", EDITAR_ENCOMIENDA_RUBROS + "{id_encomienda}", "~/Tramites/Habilitacion/Rubros.aspx");
            routes.MapPageRoute("RubrosCN_", EDITAR_ENCOMIENDA_RUBROSCN + "{id_encomienda}", "~/Tramites/Habilitacion/RubrosCN.aspx");
            routes.MapPageRoute("Titulares_", EDITAR_ENCOMIENDA_TITULAR + "{id_encomienda}", "~/Tramites/Habilitacion/Titulares.aspx");
            routes.MapPageRoute("DatosLocal_", EDITAR_ENCOMIENDA_DATOSLOCAL + "{id_encomienda}", "~/Tramites/Habilitacion/DatosLocal.aspx");
            routes.MapPageRoute("CertificadoSobrecarga_", EDITAR_ENCOMIENDA_CERTIFICADOSOBRECARGA + "{id_encomienda}", "~/Tramites/Habilitacion/CertificadoSobrecarga.aspx");
            routes.MapPageRoute("CargarPlano_", EDITAR_ENCOMIENDA_CARGAPLANO + "{id_encomienda}", "~/Tramites/Habilitacion/CargarPlano.aspx");
            routes.MapPageRoute("ConformacionLocal_", EDITAR_ENCOMIENDA_CONFORMACIONLOCAL + "{id_encomienda}", "~/Tramites/Habilitacion/ConformacionLocal.aspx");

            routes.MapPageRoute("UbicacionA", AGREGAR_ENCOMIENDA_UBICACION + "{id_encomienda}", "~/Tramites/Habilitacion/Ubicacion.aspx");
            routes.MapPageRoute("RubrosA", AGREGAR_ENCOMIENDA_RUBROS + "{id_encomienda}", "~/Tramites/Habilitacion/Rubros.aspx");
            routes.MapPageRoute("TitularesA", AGREGAR_ENCOMIENDA_TITULAR + "{id_encomienda}", "~/Tramites/Habilitacion/Titulares.aspx");
            routes.MapPageRoute("DatosLocalA", AGREGAR_ENCOMIENDA_DATOSLOCAL + "{id_encomienda}", "~/Tramites/Habilitacion/DatosLocal.aspx");
            routes.MapPageRoute("CertificadoSobrecargaA", AGREGAR_ENCOMIENDA_CERTIFICADOSOBRECARGA + "{id_encomienda}", "~/Tramites/Habilitacion/CertificadoSobrecarga.aspx");
            routes.MapPageRoute("CargarPlanoA", AGREGAR_ENCOMIENDA_CARGAPLANO + "{id_encomienda}", "~/Tramites/Habilitacion/CargarPlano.aspx");
            routes.MapPageRoute("ConformacionLocalA", AGREGAR_ENCOMIENDA_CONFORMACIONLOCAL + "{id_encomienda}", "~/Tramites/Habilitacion/ConformacionLocal.aspx");

            routes.MapPageRoute("DescargaFile_", DESCARGA_FILE + "{id_file}", "~/Reportes/GetPDF.aspx");
            routes.MapPageRoute("DescargaPlano_", DESCARGA_PLANO + "{id}", "~/Reportes/DescargaPlanos.aspx");
        }
    }
}
