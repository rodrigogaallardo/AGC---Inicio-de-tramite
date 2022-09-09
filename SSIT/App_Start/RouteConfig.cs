using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace SSIT
{
    public static class RouteConfig
    {
        public static string HOME = "";
        public static string ACCOUNT20de10 = "NOTAD";
        public static string BANDEJA_DE_ENTRADA = "Solicitud/Bandeja";
        public static string BANDEJA_DE_ENTRADA2 = "Solicitud/Bandeja";//"Solicitud/Bandeja2";
        public static string BANDEJA_DE_NOTIFICACIONES = "Solicitud/BandejaNotificaciones";

        public const string INICIO_TRAMITE = "Solicitud/Habilitacion/Inicio/";
        public const string VISOR_SOLICITUD = "Solicitud/Habilitacion/Visualizar/";
        public const string VISOR_SOLICITUD2 = "Solicitud/Habilitacion/Visualizar2/";
        public const string EDITAR_TITULAR_SOLICITUD = "Solicitud/Habilitacion/EditarTitulares/";
        public const string AGREGAR_TITULAR_SOLICITUD = "Solicitud/Habilitacion/AgregarTitulares/";
        public const string EDITAR_UBICACION_SOLICITUD = "Solicitud/Habilitacion/EditarUbicacion/";
        public const string AGREGAR_UBICACION_SOLICITUD = "Solicitud/Habilitacion/AgregarUbicacion/";

        public const string AGREGAR_UBICACION_CPADRON = "Solicitud/ConsultaPadron/AgregarUbicacion/";
        public const string AGREGAR_DATOSLOCAL_CPADRON = "Solicitud/ConsultaPadron/AgregarDatosLocal/";
        public const string AGREGAR_RUBROS_CPADRON = "Solicitud/ConsultaPadron/AgregarRubros/";
        public const string AGREGAR_TITULARES_CPADRON = "Solicitud/ConsultaPadron/AgregarTitulares/";
        public const string AGREGAR_TITULARES_SOLICITUD_CPADRON = "Solicitud/ConsultaPadron/AgregarTitularesSolicitud/";
        public const string AGREGAR_DOCUMENTOS_CPADRON = "Solicitud/ConsultaPadron/AgregarDocumentos/";

        public const string EDITAR_UBICACION_CPADRON = "Solicitud/ConsultaPadron/EditarUbicacion/";
        public const string EDITAR_DATOSLOCAL_CPADRON = "Solicitud/ConsultaPadron/EditarDatosLocal/";
        public const string EDITAR_RUBROS_CPADRON = "Solicitud/ConsultaPadron/EditarRubros/";
        public const string EDITAR_TITULARES_CPADRON = "Solicitud/ConsultaPadron/EditarTitulares/";
        public const string EDITAR_TITULARES_SOLICITUD_CPADRON = "Solicitud/ConsultaPadron/EditarTitularesSolicitud/";
        public const string EDITAR_DOCUMENTOS_CPADRON = "Solicitud/ConsultaPadron/EditarDocumentos/";

        public const string VISOR_CPADRON = "Solicitud/ConsultaPadron/Visualizar/";
        public const string DATOS_CPADRON = "Solicitud/ConsultaPadron/Datos/";
        
        public const string VISOR_TRANSFERENCIAS = "Solicitud/Transferencias/Visualizar/";
        public const string VISOR_TRANSMISIONES = "Solicitud/Transmisiones/Visualizar/";
        public const string EDITAR_TRANSFERENCIAS_TITULAR = "Solicitud/Transferencia/EditarTitulares/";
        public const string INICIAR_TRANSFERENCIAS = "Solicitud/Transferencia/InicioTramite";
        public const string AGREGAR_TITULAR_TRANSFERENCIA = "Solicitud/Transferencia/AgregarTitulares/";
        public const string AGREGAR_TITULAR_TRANSMISION = "Solicitud/Transferencia/AgregarTitularesTransmision/";
        public const string AGREGAR_DATOSLOCAL_TRANSMISION = "Solicitud/Transferencia/AgregarDatosLocal/";
        public const string EDITAR_UBICACION_TRANSFERENCIA = "Solicitud/Transferencia/EditarUbicacion/";

        public const string INICIAR_TRANSICIONES = "Solicitud/Transferencia/InicioTransicion";
        public const string CARGA_PLANCHETA_TRANSMISION = "Solicitud/Transmision/CargarPlancheta/";

        public const string DESCARGA_FILE = "DescargarFile/";
        public const string DESCARGA_ACTA = "DescargarActa/";

        public const string IMPRIMIR_SOLICITUD = "ImprimirSolicitud/";
        public const string IMPRIMIR_SOLICITUD2 = "ImprimirSolicitud2/";
        public const string IMPRIMIR_TRANSFERENCIA = "ImprimirTransferencia/";
        public const string IMPRIMIR_BOLETA = "ImprimirBoleta/";
        public const string IMPRIMIR_CERTIFICADO = "ImprimirCertificado/";
        public const string DESCARGA_LISTADO_PROFESIONALES = "ListadoProfesionales";
        public const string DESCARGA_LISTADO_ESCRIBANOS = "ListadoEscribanos";
        public const string IMPRIMIR_TRANSMISION = "ImprimirTransmision/";

        public const string INICIO_TRAMITE_AMPLIACION = "Solicitud/Ampliacion/Inicio";
        public const string VISOR_SOLICITUD_AMPLIACION = "Solicitud/Ampliacion/Visualizar/";
        public const string EDITAR_TITULAR_SOLICITUD_AMPLIACION = "Solicitud/Ampliacion/EditarTitulares/";
        public const string AGREGAR_TITULAR_SOLICITUD_AMPLIACION = "Solicitud/Ampliacion/AgregarTitulares/";
        public const string EDITAR_UBICACION_SOLICITUD_AMPLIACION = "Solicitud/Ampliacion/EditarUbicacion/";
        public const string AGREGAR_UBICACION_SOLICITUD_AMPLIACION = "Solicitud/Ampliacion/AgregarUbicacion/";
        public const string CARGA_PLANCHETA_AMPLIACION = "Solicitud/Ampliacion/CargarPlancheta/";

        //ECI
        public const string SELECION_TRAMITE_ECI = "Solicitud/HabilitacionECI/SelecInicioTramite/";
        public const string INICIO_TRAMITE_ECI = "Solicitud/HabilitacionECI/Inicio/";
        public const string VISOR_SOLICITUD_ECI = "Solicitud/HabilitacionECI/Visualizar/";
        public const string EDITAR_TITULAR_SOLICITUD_ECI = "Solicitud/HabilitacionECI/EditarTitulares/";
        public const string AGREGAR_TITULAR_SOLICITUD_ECI = "Solicitud/HabilitacionECI/AgregarTitulares/";
        public const string EDITAR_UBICACION_SOLICITUD_ECI = "Solicitud/HabilitacionECI/EditarUbicacion/";
        public const string AGREGAR_UBICACION_SOLICITUD_ECI = "Solicitud/HabilitacionECI/AgregarUbicacion/";
        public const string CARGA_PLANCHETA_ECI = "Solicitud/HabilitacionECI/CargarPlancheta/";

        public const string INICIO_TRAMITE_REDISTRIBUCION_USO = "Solicitud/RedistribucionUso/Inicio";
        public const string VISOR_SOLICITUD_REDISTRIBUCION_USO = "Solicitud/RedistribucionUso/Visualizar/";
        public const string EDITAR_TITULAR_SOLICITUD_REDISTRIBUCION_USO = "Solicitud/RedistribucionUso/EditarTitulares/";
        public const string AGREGAR_TITULAR_SOLICITUD_REDISTRIBUCION_USO = "Solicitud/RedistribucionUso/AgregarTitulares/";
        public const string EDITAR_UBICACION_SOLICITUD_REDISTRIBUCION_USO = "Solicitud/RedistribucionUso/EditarUbicacion/";
        public const string AGREGAR_UBICACION_SOLICITUD_REDISTRIBUCION_USO = "Solicitud/RedistribucionUso/AgregarUbicacion/";
        public const string CARGA_PLANCHETA_REDISTRIBUCION_USO = "Solicitud/RedistribucionUso/CargarPlancheta/";

        public const string INICIO_TRAMITE_PERMISO_MC = "Solicitud/Permisos/Inicio";
        public const string VISOR_SOLICITUD_PERMISO_MC = "Solicitud/Permisos/Visualizar/";
        public const string EDITAR_TITULAR_SOLICITUD_PERMISO_MC = "Solicitud/Permisos/EditarTitulares/";
        public const string AGREGAR_TITULAR_SOLICITUD_PERMISO_MC = "Solicitud/Permisos/AgregarTitulares/";
        public const string EDITAR_UBICACION_SOLICITUD_PERMISO_MC = "Solicitud/Permisos/EditarUbicacion/";
        public const string AGREGAR_UBICACION_SOLICITUD_PERMISO_MC = "Solicitud/Permisos/AgregarUbicacion/";
        public const string AGREGAR_RUBROS_SOLICITUD_PERMISO_MC = "Solicitud/Permisos/AgregarRubros/";
        public const string EDITAR_RUBROS_SOLICITUD_PERMISO_MC = "Solicitud/Permisos/EditarRubros/";
        public const string AGREGAR_DATOSLOCAL_SOLICITUD_PERMISO_MC = "Solicitud/Permisos/AgregarDatosLocal/";
        public const string EDITAR_DATOSLOCAL_SOLICITUD_PERMISO_MC = "Solicitud/Permisos/EditarDatosLocal/";


        public const string CONVERSION_USUARIOS = "Solicitud/ConversionUsuarios";
        public const string ERRORES = "Error/";
        public const string MANTENIMIENTO = "TareaMantenimiento/";

        public const string VISOR_TRAMITE = "Solicitud/Habilitaciones/Visualizar/";
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.EnableFriendlyUrls();


            // Habilitaciones
            routes.MapPageRoute("Inicio_", INICIO_TRAMITE + "{id_solicitud}", "~/Solicitud/Habilitacion/InicioTramite.aspx");
            routes.MapPageRoute("VisorTramite_", VISOR_SOLICITUD + "{id_solicitud}", "~/Solicitud/VisorTramite.aspx");
            routes.MapPageRoute("VisorTramite_2", VISOR_SOLICITUD2 + "{id_solicitud}", "~/Solicitud/VisorTramite2.aspx");
            routes.MapPageRoute("EditarUbicacionTramite_", EDITAR_UBICACION_SOLICITUD + "{id_solicitud}", "~/Solicitud/Ubicacion.aspx");
            routes.MapPageRoute("EditarTitularTramite_", EDITAR_TITULAR_SOLICITUD + "{id_solicitud}", "~/Solicitud/Titulares.aspx");
            routes.MapPageRoute("AgregarUbicacionTramite_", AGREGAR_UBICACION_SOLICITUD + "{id_solicitud}", "~/Solicitud/Ubicacion.aspx");
            routes.MapPageRoute("AgregarTitularTramite_", AGREGAR_TITULAR_SOLICITUD + "{id_solicitud}", "~/Solicitud/Titulares.aspx");

            routes.MapPageRoute("CPadron_", DATOS_CPADRON + "{id_solicitud}", "~/Solicitud/Consulta_Padron/DatosSolicitud.aspx");
            routes.MapPageRoute("CPadron_Visor", VISOR_CPADRON + "{id_solicitud}", "~/Solicitud/Consulta_Padron/VisorTramite.aspx");
            routes.MapPageRoute("AgregarUbicacionCPadron_", AGREGAR_UBICACION_CPADRON + "{id_solicitud}", "~/Solicitud/Consulta_Padron/Ubicacion.aspx");
            routes.MapPageRoute("AgregarDatosLocalCPadron_", AGREGAR_DATOSLOCAL_CPADRON + "{id_solicitud}", "~/Solicitud/Consulta_Padron/DatosLocal.aspx");
            routes.MapPageRoute("AgregarRubrosCPadron_", AGREGAR_RUBROS_CPADRON + "{id_solicitud}", "~/Solicitud/Consulta_Padron/Rubros.aspx");
            routes.MapPageRoute("AgregarTitularesCPadron_", AGREGAR_TITULARES_CPADRON + "{id_solicitud}", "~/Solicitud/Consulta_Padron/Titulares.aspx");
            routes.MapPageRoute("AgregarTitularesSolicitudCPadron_", AGREGAR_TITULARES_SOLICITUD_CPADRON + "{id_solicitud}", "~/Solicitud/Consulta_Padron/TitularesSolicitud.aspx");
            routes.MapPageRoute("AgregarDocumentosSolicitudCPadron_", AGREGAR_DOCUMENTOS_CPADRON + "{id_solicitud}", "~/Solicitud/Consulta_Padron/CargarPlano.aspx");
            routes.MapPageRoute("EditarUbicacionCPadron_", EDITAR_UBICACION_CPADRON + "{id_solicitud}", "~/Solicitud/Consulta_Padron/Ubicacion.aspx");
            routes.MapPageRoute("EditarDatosLocalCPadron_", EDITAR_DATOSLOCAL_CPADRON + "{id_solicitud}", "~/Solicitud/Consulta_Padron/DatosLocal.aspx");
            routes.MapPageRoute("EditarRubrosCPadron_", EDITAR_RUBROS_CPADRON + "{id_solicitud}", "~/Solicitud/Consulta_Padron/Rubros.aspx");
            routes.MapPageRoute("EditarTitularesCPadron_", EDITAR_TITULARES_CPADRON + "{id_solicitud}", "~/Solicitud/Consulta_Padron/Titulares.aspx");
            routes.MapPageRoute("EditarTitularesSolicitudCPadron_", EDITAR_TITULARES_SOLICITUD_CPADRON + "{id_solicitud}", "~/Solicitud/Consulta_Padron/TitularesSolicitud.aspx");
            routes.MapPageRoute("EditarDocumentosSolicitudCPadron_", EDITAR_DOCUMENTOS_CPADRON + "{id_solicitud}", "~/Solicitud/Consulta_Padron/CargarPlano.aspx");

            // Transferencias
            routes.MapPageRoute("VisorTramite_Transferencias", VISOR_TRANSFERENCIAS + "{id_solicitud}", "~/Solicitud/Transferencia/VisorTramiteTransferencia.aspx");
            routes.MapPageRoute("EditarTitularTransferencia_", EDITAR_TRANSFERENCIAS_TITULAR + "{id_solicitud}", "~/Solicitud/Transferencia/Titulares.aspx");
            routes.MapPageRoute("InicarTransferencia", INICIAR_TRANSFERENCIAS, "~/Solicitud/Transferencia/InicioTramite.aspx");
            routes.MapPageRoute("AgregarTitularTransferencia_", AGREGAR_TITULAR_TRANSFERENCIA + "{id_solicitud}", "~/Solicitud/Transferencia/Titulares.aspx");
            routes.MapPageRoute("AgregarUbicacionTransferencia_", EDITAR_UBICACION_TRANSFERENCIA + "{id_solicitud}", "~/Solicitud/Transferencia/Ubicacion.aspx");
            
            //Transmisiones
            routes.MapPageRoute("InicarTransicion", INICIAR_TRANSICIONES, "~/Solicitud/Transferencia/InicioTransicion.aspx");
            routes.MapPageRoute("VisorTramite_Transmisiones", VISOR_TRANSMISIONES + "{id_solicitud}", "~/Solicitud/Transferencia/VisorTransmision.aspx");
            routes.MapPageRoute("AgregarTitularTransmision_", AGREGAR_TITULAR_TRANSMISION + "{id_solicitud}", "~/Solicitud/Transferencia/TitularesTransmision.aspx");
            routes.MapPageRoute("AgregarDatosTransferencia_", AGREGAR_DATOSLOCAL_TRANSMISION + "{id_solicitud}", "~/Solicitud/Transferencia/DatosLocal.aspx");
            routes.MapPageRoute("ImprimirSolicitudTransmision_", IMPRIMIR_TRANSMISION + "{id_solicitud}", "~/Reportes/Imprimir.aspx");
            routes.MapPageRoute("CARGA_PLANCHETA_TRANSMISION", CARGA_PLANCHETA_TRANSMISION + "{id_solicitud}", "~/Solicitud/Transferencia/CargaPlancheta.aspx");
            
            // Ampliaciones
            routes.MapPageRoute("INICIO_TRAMITE_AMPLIACION", INICIO_TRAMITE_AMPLIACION , "~/Solicitud/Ampliacion/InicioTramite.aspx");
            routes.MapPageRoute("VISOR_SOLICITUD_AMPLIACION", VISOR_SOLICITUD_AMPLIACION + "{id_solicitud}", "~/Solicitud/VisorTramite.aspx");
            routes.MapPageRoute("EDITAR_TITULAR_SOLICITUD_AMPLIACION", EDITAR_TITULAR_SOLICITUD_AMPLIACION + "{id_solicitud}", "~/Solicitud/Titulares.aspx");
            routes.MapPageRoute("AGREGAR_TITULAR_SOLICITUD_AMPLIACION", AGREGAR_TITULAR_SOLICITUD_AMPLIACION + "{id_solicitud}", "~/Solicitud/Titulares.aspx");
            routes.MapPageRoute("EDITAR_UBICACION_SOLICITUD_AMPLIACION", EDITAR_UBICACION_SOLICITUD_AMPLIACION + "{id_solicitud}", "~/Solicitud/Ubicacion.aspx");
            routes.MapPageRoute("AGREGAR_UBICACION_SOLICITUD_AMPLIACION", AGREGAR_UBICACION_SOLICITUD_AMPLIACION + "{id_solicitud}", "~/Solicitud/Ubicacion.aspx");
            routes.MapPageRoute("CARGA_PLANCHETA_AMPLIACION", CARGA_PLANCHETA_AMPLIACION + "{id_solicitud}", "~/Solicitud/Ampliacion/CargaPlancheta.aspx");

            //ECI
            routes.MapPageRoute("SelecInicioTramite_", SELECION_TRAMITE_ECI + "{id_solicitud}", "~/Solicitud/HabilitacionECI/SelecInicioTramite.aspx");
            routes.MapPageRoute("InicioECI_", INICIO_TRAMITE_ECI + "{id_solicitud}", "~/Solicitud/HabilitacionECI/InicioTramite.aspx");
            routes.MapPageRoute("VisorTramiteECI_", VISOR_SOLICITUD_ECI + "{id_solicitud}", "~/Solicitud/VisorTramite.aspx");
            routes.MapPageRoute("EditarUbicacionTramiteECI_", EDITAR_UBICACION_SOLICITUD_ECI + "{id_solicitud}", "~/Solicitud/Ubicacion.aspx");
            routes.MapPageRoute("EditarTitularTramiteECI_", EDITAR_TITULAR_SOLICITUD_ECI + "{id_solicitud}", "~/Solicitud/Titulares.aspx");
            routes.MapPageRoute("AgregarUbicacionTramiteECI_", AGREGAR_UBICACION_SOLICITUD_ECI + "{id_solicitud}", "~/Solicitud/Ubicacion.aspx");
            routes.MapPageRoute("AgregarTitularTramiteECI_", AGREGAR_TITULAR_SOLICITUD_ECI + "{id_solicitud}", "~/Solicitud/Titulares.aspx");
            routes.MapPageRoute("CARGA_PLANCHETA_ECI", CARGA_PLANCHETA_ECI + "{id_solicitud}", "~/Solicitud/HabilitacionECI/CargaPlancheta.aspx");

            // Redistribuciones de Uso
            routes.MapPageRoute("INICIO_TRAMITE_REDISTRIBUCION_USO", INICIO_TRAMITE_AMPLIACION, "~/Solicitud/RedistribucionUso/InicioTramite.aspx");
            routes.MapPageRoute("VISOR_SOLICITUD_REDISTRIBUCION_USO", VISOR_SOLICITUD_REDISTRIBUCION_USO + "{id_solicitud}", "~/Solicitud/VisorTramite.aspx");
            routes.MapPageRoute("EDITAR_TITULAR_SOLICITUD_REDISTRIBUCION_USO", EDITAR_TITULAR_SOLICITUD_REDISTRIBUCION_USO + "{id_solicitud}", "~/Solicitud/Titulares.aspx");
            routes.MapPageRoute("AGREGAR_TITULAR_SOLICITUD_REDISTRIBUCION_USO", AGREGAR_TITULAR_SOLICITUD_REDISTRIBUCION_USO + "{id_solicitud}", "~/Solicitud/Titulares.aspx");
            routes.MapPageRoute("EDITAR_UBICACION_SOLICITUD_REDISTRIBUCION_USO", EDITAR_UBICACION_SOLICITUD_REDISTRIBUCION_USO + "{id_solicitud}", "~/Solicitud/Ubicacion.aspx");
            routes.MapPageRoute("AGREGAR_UBICACION_SOLICITUD_REDISTRIBUCION_USO", AGREGAR_UBICACION_SOLICITUD_REDISTRIBUCION_USO + "{id_solicitud}", "~/Solicitud/Ubicacion.aspx");
            routes.MapPageRoute("CARGA_PLANCHETA_REDISTRIBUCION_USO", CARGA_PLANCHETA_REDISTRIBUCION_USO + "{id_solicitud}", "~/Solicitud/RedistribucionUso/CargaPlancheta.aspx");

            // Permisos M{usica y Canto
            routes.MapPageRoute("INICIO_TRAMITE_PERMISO_MC", INICIO_TRAMITE_PERMISO_MC, "~/Solicitud/Permisos/InicioTramiteMC.aspx");
            routes.MapPageRoute("VISOR_SOLICITUD_PERMISO_MC", VISOR_SOLICITUD_PERMISO_MC + "{id_solicitud}", "~/Solicitud/Permisos/VisorTramite.aspx");
            routes.MapPageRoute("EDITAR_TITULAR_SOLICITUD_PERMISO_MC", EDITAR_TITULAR_SOLICITUD_PERMISO_MC + "{id_solicitud}", "~/Solicitud/Titulares.aspx");
            routes.MapPageRoute("AGREGAR_TITULAR_SOLICITUD_PERMISO_MC", AGREGAR_TITULAR_SOLICITUD_PERMISO_MC + "{id_solicitud}", "~/Solicitud/Titulares.aspx");
            routes.MapPageRoute("EDITAR_UBICACION_SOLICITUD_PERMISO_MC", EDITAR_UBICACION_SOLICITUD_PERMISO_MC + "{id_solicitud}", "~/Solicitud/Ubicacion.aspx");
            routes.MapPageRoute("AGREGAR_UBICACION_SOLICITUD_PERMISO_MC", AGREGAR_UBICACION_SOLICITUD_PERMISO_MC + "{id_solicitud}", "~/Solicitud/Ubicacion.aspx");
            routes.MapPageRoute("EDITAR_RUBROS_SOLICITUD_PERMISO_MC", EDITAR_RUBROS_SOLICITUD_PERMISO_MC + "{id_solicitud}", "~/Solicitud/Rubros.aspx");
            routes.MapPageRoute("AGREGAR_RUBROS_SOLICITUD_PERMISO_MC", AGREGAR_RUBROS_SOLICITUD_PERMISO_MC + "{id_solicitud}", "~/Solicitud/Rubros.aspx");
            routes.MapPageRoute("EDITAR_DATOSLOCAL_SOLICITUD_PERMISO_MC", EDITAR_DATOSLOCAL_SOLICITUD_PERMISO_MC + "{id_solicitud}", "~/Solicitud/DatosLocal.aspx");
            routes.MapPageRoute("AGREGAR_DATOSLOCAL_SOLICITUD_PERMISO_MC", AGREGAR_DATOSLOCAL_SOLICITUD_PERMISO_MC + "{id_solicitud}", "~/Solicitud/DatosLocal.aspx");
            
            //Generales
            routes.MapPageRoute("Home", HOME, "~/Default.aspx");
            routes.MapPageRoute("20de10", ACCOUNT20de10, "~/Account/20de10.aspx");
            routes.MapPageRoute("GEN_Errores_Route", ERRORES + "{id}", "~/Errores/ErrorElmah.aspx");
            routes.MapPageRoute("Mantenimiento_", MANTENIMIENTO + "{cargo}", "~/Errores/mantenimiento.aspx");
            routes.MapPageRoute("Bandeja_", BANDEJA_DE_ENTRADA, "~/Solicitud/BandejaDeEntrada.aspx");
            routes.MapPageRoute("Bandeja_2", BANDEJA_DE_ENTRADA2, "~/Solicitud/BandejaDeEntrada2.aspx");
            routes.MapPageRoute("BandejaNotificaciones_", BANDEJA_DE_NOTIFICACIONES, "~/Solicitud/BandejaDeNotificaciones.aspx");
            routes.MapPageRoute("CARGA_CONVERSION_USUARIOS", CONVERSION_USUARIOS, "~/Solicitud/ConversionUsuarios.aspx");
            routes.MapPageRoute("VISOR_TRAMITE", VISOR_TRAMITE + "{id_solicitud}/{id_tad}", "~/Solicitud/Tramite.aspx");
            routes.MapPageRoute("DescargaFile_", DESCARGA_FILE + "{id_file}", "~/Reportes/GetPDF.aspx");
            routes.MapPageRoute("DescargaActa_", DESCARGA_ACTA + "{id_actanotarial}", "~/Reportes/ImprimirActaNotarial.aspx");
            routes.MapPageRoute("ImprimirSolicitud_", IMPRIMIR_SOLICITUD + "{id_solicitud}", "~/Reportes/ImprimirSolicitud.aspx");
            routes.MapPageRoute("ImprimirSolicitud_2", IMPRIMIR_SOLICITUD2 + "{id_solicitud}", "~/Reportes/ImprimirSolicitudNueva.aspx");
            routes.MapPageRoute("ImprimirSolicitudTransferencia_", IMPRIMIR_TRANSFERENCIA + "{id_solicitud}", "~/Reportes/Imprimir.aspx");
            routes.MapPageRoute("ImprimirBoleta_", IMPRIMIR_BOLETA + "{id_pago}", "~/Reportes/ImprimirBoletaUnica.aspx");
            routes.MapPageRoute("ImprimirCertificado_", IMPRIMIR_CERTIFICADO + "{id_certificado}", "~/Reportes/ImprimirCertificado.aspx");
            routes.MapPageRoute("ImprimirListaProfesionales_", DESCARGA_LISTADO_PROFESIONALES, "~/Reportes/ImprimirProfesionales.aspx");
            routes.MapPageRoute("ImprimirListaEscribanos_", DESCARGA_LISTADO_ESCRIBANOS, "~/Reportes/ImprimirEscribanos.aspx");


            routes.MapPageRoute("GetDoc_", "Mobile/GetDoc/{id_tipo}/{id}", "~/Mobile/ObtenerDoc.aspx");

        }
    }
}

