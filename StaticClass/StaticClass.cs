using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Globalization;
using iTextSharp.text.pdf;

namespace StaticClass
{
    public class clsError
    {
        public int? codigo { get; set; }
        public string error { get; set; }
    }
    public class ValidationException : Exception
    {
        public ValidationException()
        {
        }

        public ValidationException(string message)
            : base(message)
        {
        }

        public ValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }


    public static class Instructivos_tipos
    {
        public const string DGHyP_Consulta_Padron = "DGHyP_Consulta_Padron";
        public const string DGHyP_Habilitaciones = "DGHyP_Habilitaciones";
        public const string DGHyP_Transferencias = "DGHyP_Transferencias";
        public const string DGHyP_Anexo = "DGHyP_Anexo_Tecnico";
        public const string DGHyP_Ampliaciones = "DGHyP_Ampliaciones";
        public const string DGHyP_RedistribucionesUso = "DGHyP_RedistribucionesUso";
        public const string DGHyP_HabilitacionECI = "DGHyP_HabilitacionECI";
        public const string DGHyP_PermisoMC = "DGHyP_PermisoMC";
        public static string Mail_Pruebas
        {
            get
            {
                string ret = "";
                string value = System.Configuration.ConfigurationManager.AppSettings["Mail.Pruebas"];
                if (!string.IsNullOrEmpty(value))
                {
                    ret = value.ToString();
                }
                return ret;
            }
        }
    }

    public static class Errors
    {
        public const string ENCOMIENDA_PROFESIONAL_INEXISTENTE_USUARIO = "El usuario no se identifica como un profesional perteneciente a un consejo. Informe de esta situación al consejo que usted pertenezca";
        public const string ENCOMIENDA_PROFESIONAL_INHIBIDO = "Usuario Inhibido. Debe comunicarse con su Consejo Profesional para modificar su situación";
        public const string ENCOMIENDA_PLANOS_EXISTENTE = "El plano que está queriendo ingresar ya se encuentra en la lista.";
        public const string FILE_NO_TRANSFERIDO = "El archivo no fue transferido al servidor";
        public const string ENCOMIENDA_SALON_VENTA_SIN_ESP = "Debe especificar la superficie del Salón de Ventas.";
        public const string ENCOMIENDA_SIN_PLANOS_HABILITACION = "No se cargó el Plano de Habilitación.";
        public const string ENCOMIENDA_SIN_PLANOS_REDISTRIBUCION_USO = "No se cargó el Plano de Redistribución de Uso.";
        public const string ENCOMIENDA_SIN_PLANOS_AMPLIACION = "No se cargó el Plano de Ampliación.";
        public const string ENCOMIENDA_SIN_PLANCHETA = "No se cargó la Plancheta.";
        public const string ENCOMIENDA_SIN_PLANOS_CONTRA_INCENDIO = "Se debe cargar el Plano Contra Incendios.";
        public const string ENCOMIENDA_SIN_CERTIFICADO_PRO_TEATRO = "Debe ingresar el Certificado Pro-Teatro.";
        public const string ENCOMIENDA_REDISTRIBUION_USO_NO_SSP = "El tramite que corresponde a la Redistribución de uso debe contener al menos un rubro que sea 'con planos','inspección previa' o 'habilitación previa'.";
        public const string ENCOMIENDA_CANT_OPERARIOS = "La cantidad de operarios debe ser mayor a cero.";
        public const string ENCOMIENDA_SUM_SUP_RUBROS = "La sumatoria de las superficies asignadas a los rubros no puede ser menor a la superficie a habilitar.";
        public const string ENCOMIENDA_SUP_RUBROS = "Al menos uno de los Rubros debe ser igual a la superficie a habilitar.";
        public const string ENCOMIENDA_RUBROS_INVALIDOS = "Hay uno o más rubros que no son válidos, verifique el error en la grilla de rubros.<br />En las columnas de Zona y Superficie.";
        public const string ENCOMIENDA_SUPERFICIE_RUBRO = "La superficie del rubro {0} es mayor a la superficie a habilitar.";
        public const string ENCOMIENDA_SOLICITUD_INGRESAR_TITULARES = "Debe ingresar el/los titular/es para poder continuar con el trámite.";
        public const string ENCOMIENDA_SOLICITUD_DATOS_INVALIDOS = "Los datos proporcionados no coinciden con ningúna Solicitud.";
        public const string ENCOMIENDA_CAMBIOS = "El estado del Anexo Profesional no admite cambios en los datos.";
        public const string ENCOMIENDA_TRAMITE_BLOQUEADO = "El trámite se encuentra bloqueado";
        public const string ENCOMIENDA_SOLICITUD_ESTADO_ERRONEO = "El estado de la solicitud no permite iniciar un nuevo Anexo Técnico.";
        public const string ENCOMIENDA_SOLICITUD_ENCOMIENDA_EN_CURSO = "Ya exite un anexo en curso para esta solicitud. El mismo es el {0} .";

        public const string ENCOMIENDA_NO_DATOS_LOCAL = "No se han cargado los datos del local.";
        public const string ENCOMIENDA_RUBRO = "El Rubro no ha sido encontrado en la base de datos. No es posible agregarlo a la encomienda";
        public const string ENCOMIENDA_DATOS_LOCAL_OPERARIOS = "La cantidad de operarios debe ser mayor a cero.";
        public const string ENCOMIENDA_DATOS_LOCAL_SUPERFICIE_RUBRO = "La superficie del rubro no puede ser mayor a la superficie a habilitar";
        public const string ENCOMIENDA_DATOS_LOCAL_SUPERFICIE_RUBRO_AMP = "La superficie del rubro no puede ser mayor a la superficie a ampliar";
        public const string ENCOMIENDA_DATOS_LOCAL_SUPERFICIE = "La sumatoria de las superficies asignadas a los rubros no puede ser menor a la superficie a habilitar.";
        public const string ENCOMIENDA_RUBRO_EXISTENTE = "El Rubro ya se encuentra en la encomienda. Si necesita modificar la superficie, eliminelo e ingreselo nuevamente con la nueva superficie";
        public const string ENCOMIENDA_ESTADO_PERFIL_INVALIDO = "Cambio de estado invalido. Su perfil no permite realizar este cambio de estado.";
        public const string ENCOMIENDA_NO_CATEGORIZADO_AMBIENTALMENTE = "El Rubro no ha sido Categorizado según el Impacto Ambiental para la superficie indicada. No es posible agregarlo al Anexo Técnico";

        public const string ENCOMIENDA_SUMATORIA_DEPOSITO_MAYOR_TOTAL = "La sumatoria de todos los sectores declarados como deposito NO puede superar el 60% de la superficie total a habilitar.";
        public const string ENCOMIENDA_SECTORES_CARGA_DESCARGA_MAYOR_30MTS = "Los sectores con destino \"Playa de cargar/descarga\" la superficie del mismo debe ser mayor o igual a 30 m2.";
        public const string ENCOMIENDA_EXIGIR_SOBREGARGA = "Cualquier planta declarada distinta de PB (planta baja) debe exigir una declaración del profesional";
        public const string ENCOMIENDA_INGRESAR_RUBROS = "Debe ingresar los rubros/actividades para poder continuar con el trámite.";
        public const string ENCOMIENDA_DECLARAR_ARTICULO_521 = "Debe declarar que cumple con el artículo 5.2.1 inc “c” o “d” del cpu.";
        public const string ENCOMIENDA_CARGAR_DESTINO_CARGADESCARGA = "Es necesario cargar destino carga descarga.";
        public const string ENCOMIENDA_SUP_CARGADESCARGA_MENOR_SUP_TOTAL = "La superficie total para carga/descarga debe ser mayor o igual a {0} m2.";
        public const string ENCOMIENDA_SUP_CARGADESCARGA_PORCENTAJE = "La superficie total para carga/descarga debe ser de al menos un {0} % de la superficie total a habilitar.";
        public const string ENCOMIENDA_DEBE_EXISTIR_1_BANO = "Debe existir 1 sector con destino baño.";
        public const string ENCOMIENDA_DEBE_EXISTIR_2_BANO = "Debe existir al menos 2 sectores con destino baño.";
        public const string ENCOMIENDA_DEBE_EXISTIR_2_BANO_SALUBRIDAD = "Debe existir al menos 2 sectores con destino baño y haber seleccionado que posee servicio de salubridad especial.";
        public const string ENCOMIENDA_PLANTAS_CONSECUTIVAS = "Es necesario adjuntar plano de incendio.";
        public const string ENCOMIENDA_ADJUNTAR_DOCUMENTOS_LEY_962 = "Para continuar, deberá ajuntar una DDJJ firmada por titular y profesional <br>(consignando la preexistencia del local a la vigencia de la Ley Nº 962, y que no se hayan realizado obras de ampliación en el inmueble) <br>y una copia autenticada de algún antecedente que acredite la preexistencia del local y su destino comercial.";
        public const string ENCOMIENDA_ADJUNTAR_DOCUMENTOS_REQUERIDO_RUBRO_ZONA = "Es necesario adjuntar el documento {0} para el rubro {1}.";
        public const string ENCOMIENDA_RUBROS_NO_VIGENTES = "El/los rubro/s con codigo {0} no estan vigentes, debera eliminarlos para continuar.";
        public const string ENCOMIENDA_FALTA_SOBRECARGA = "Debe completar los datos de la sobrecarga.";
        public const string ENCOMIENDA_CONFORMACION_LOCAL_DISTINTA_SUPERFICIE = "La superficie total de la conformación del local es distinta a la superficie total a habilitar.";
        public const string ENCOMIENDA_PLANTAS_SELECCIONADAS = "Los Anexos Técnicos no podrán ser confirmados sin tener seleccionada al menos una planta";


        public const string SSIT_ENCOMIENDA_DATOS_INVALIDOS = "Los datos proporcionados no coinciden con ningún trámite de Encomienda Digital.";
        public const string SSIT_ENCOMIENDA_ESTADO_INVALIDO = "La Anexo Profesinonal NO se encuentra aprobada por el consejo. Su estado actual es ({0}), no es posible iniciar el trámite, para iniciarlo la misma debe encontrarse aprobada.";
        public const string SSIT_SOLICITUD_ENCOMIENDA_EXISTENTE = "Ya existe una soliciud iniciada con este número de encomienda, por favor continúe con la misma. La solicitud es la Nº ";
        public const string SSIT_SOLICITUD_ENCOMIENDA_ORDEN_ERRONEO = "Cargue las encomiendas en orden de creación. Primero la original y luego sus rectificatorias";
        public const string SSIT_SOLICITUD_RELACIONADA_ESTADO_INVALIDO = "El estado de la solicitud de la encomienda relacionada no es valido.";
        public const string SSIT_SOLICITUD_ENCOMIENDA_SIN_PLANOS_HABILITACION = "El Plano de Habilitación es de caracter obligatorio, por favor suba el mismo.";
        public const string SSIT_SOLICITUD_UBICACIONES_CLAUSURAS = "Se pone en conocimiento que el domicilio declarado por usted presenta irregularidades. Por favor acerquese a nuestras oficinas ubicadas en TTE. GRAL. JUAN DOMINGO PERON 2941.";
        public const string SSIT_SOLICITUD_UBICACIONES_INHIBIDAS = "Se pone en conocimiento que el domicilio declarado por usted esta inhibido. Por favor acerquese a nuestras oficinas ubicadas en TTE. GRAL. JUAN DOMINGO PERON 2941.";
        public const string SSIT_SOLICITUD_ANEXO_NOTARIAL_INEXISTENTE = "El Anexo Técnico Nº {0} deberá estar acompañado de un Anexo Notarial Aprobado para poder presentar la solicitud.";
        public const string SSIT_SOLICITUD_ANEXO_NOTARIAL_ANULADO = "El Anexo Notarial que posee está Anulado, comuniquese con el Escribano.";
        public const string SSIT_SOLICITUD_ANEXO_EN_CURSO = "Existen Anexos Tecnicos relacionados a la Solicitud en curso";
        public const string SSIT_SOLICITUD_ANEXO_NOTARIAL_SIN_ARCHIVO = "Se han enviado los datos del acta notarial pero no se ha enviado el archivo pdf correspondiente a la misma.";
        public const string SSIT_SOLICITUD_ANEXO_TECNICO_INEXISTENTE = "Todas las Solicitudes deberán estar acompañadas de un Anexo Técnico Aprobado.";
        
        public const string SSIT_SOLICITUD_CAA_INEXISTENTE = "La solicitud deberá estar acompañada de un Certificado de Aptitud Ambiental(CAA) Aprobado y relacionado al ultimo anexo técnico tipo A o posteriores.";
        public const string SSIT_SOLICITUD_TITULARES_UBICACIONES_DIFERENTES = "Los titulares y/o Ubicaciones de la Solicitud son diferentes al anexo aprobado.";
        public const string SSIT_SOLICITUD_PAGO_CAA = "Todas las Solicitudes deberán estar acompañadas del Pago de Certificado de Aptitud Ambiental.";
        public const string SSIT_SOLICITUD_PAGO_CAA_ESP = "Requiere el pago del CAA  favor diríjase a la Mesa de Entradas de APRA, solicitando previamente turno correspondiente a Mesa de Entrada -  Entrega de documentación y vista de expediente  en   www.buenosaires.gob.ar/turnos, para regularizar su situación.";
        public const string SSIT_SOLICITUD_PAGO = "Todas las Solicitudes deberán estar acompañadas del Pago.";
        public const string SSIT_SOLICITUD_NORMATIVA_ANEXO_SIN_DOCUMENTO = "Adjunte copia de la normativa declarada en la sección 'Aplicar Normativa' del anexo que autoriza el ejercicio de la actividad para esa zona y rubro/s ingresado/s";
        public const string SSIT_SOLICITUD_OBSERVACIONES_SIN_PROCESAR = "Debe procesar todas las observaciones.";
        public const string SSIT_SOLICITUD_SIN_TAREA_ABIERTA = "No se ha encontrado tarea abierta.";
        public const string SSIT_SOLICITUD_ESTADO_INVALIDO_PRESENTAR = "La solicitud no se encuentra en un estado valido para presentar.";
        public const string SSIT_SOLICITUD_INGRESAR_TITULARES = "Debe ingresar el/los titular/es para poder continuar con el trámite.";
        public const string SSIT_SOLICITUD_ESCUELA_SIN_PLANO_NUEVO = "Debe adjuntar un nuevo plano.";
        public const string SSIT_NUMERO_EXPEDIENTE_SADE_INEXISTENTE = "No existe dicho expediente en SADE.";
        public const string SSIT_SOLICITUD_ESCUELA_SIN_NUMERO_EXPEDIENTE_RELACIONADO_SADE = "Debe ingresar el Número de expediente SADE relacionado.";
        public const string SSIT_SOLICITUD_NO_ADMITE_CAMBIOS = "El estado de la Solicitud no admite cambios en los datos.";
        public const string SSIT_SOLICITUD_UBICACION_PROTEGIDA = "La ubicación declarada es una ubicación protegida. Deberá adjuntar la disposición de DGIUR";
        public const string SSIT_SOLICITUD_NO_DATOS_LOCAL = "No se han cargado los datos del local.";
        public const string SSIT_SOLICITUD_RUBRO_EXISTENTE = "El Rubro ya se encuentra en la solicitud. Si necesita modificar la superficie, eliminelo e ingreselo nuevamente con la nueva superficie";
        public const string SSIT_SOLICITUD_RUBRO = "El Rubro no ha sido encontrado en la base de datos. No es posible agregarlo a la solicitud";
        public const string SSIT_SOLICITUD_INGRESAR_RUBROS = "Debe ingresar los rubros/actividades para poder continuar con el trámite.";
        public const string SSIT_SOLICITUD_INGRESAR_DOC_RUBROS = "Debe adjuntar la siguiente documentacion obligatoria: {0}";

        public const string SSIT_TRANSFERENCIAS_SIN_ACTA_NOTARIAL = "Debe ingresar el Acta Notarial.";
        public const string SSIT_TRANSFERENCIAS_SIN_EDICTOS = "Debe ingresar los edictos.";
        public const string SSIT_TRANSFERENCIAS_SIN_TITULARES = "No se ingreso los titulares.";
        public const string SSIT_TRANSFERENCIAS_SIN_TAREAS_REVISION = "No hay una tarea de revisión de pago para esta solicitud.";
        public const string SSIT_TRANSFERENCIAS_CPADRON_SIN_COINCIDENCIAS = "Los datos proporcionados no coinciden con ningún trámite de Consulta al Padrón.";
        public const string SSIT_TRANSFERENCIAS_TAREA_NO_CREADA = "No se pudo crear la tarea de la solicitud";
        public const string SSIT_CPADRON_NO_SE_PUEDE_CREAR = "No se pudo crear la tarea de la solicitud";
        public const string SSIT_TRANSFERENCIAS_NO_ADMITE_CAMBIOS = "El estado de la Transferencia no admite cambios en los datos.";
        public const string SSIT_TRANSFERENCIAS_PAGO = "Todos las Solicitudes deberán estar acompañadas del Pago.";
        public const string SSIT_TRANSFERENCIAS_SIN_DOCUMENTOS = "Debe ingresar los documentos requeridos según su tipo de transmisión.";
        public const string SSIT_TRANSFERENCIAS_SIN_UBICACION = "Debe ingresar los datos de la ubicación.";

        public const string SSIT_CPADRON_NONBRE_ARCHIVO = "El nombre del archivo no puede superar los 50 caracteres";
        public const string SSIT_CPADRON_NO_ADMITE_CAMBIOS = "El estado de la Consulta al Padron no admite cambios en los datos.";
        public const string SSIT_CPADRON_SUPERFICIE_RUBRO_MAYOR = "La superficie del rubro no puede ser mayor a la superficie a habilitar";
        public const string SSIT_CPADRON_TIENE_RUBRO = "El Rubro ya se encuentra en la consulta al Padron. Si necesita modificar la superficie, eliminelo e ingreselo nuevamente con la nueva superficie";
        public const string SSIT_CPADRON_RUBRO_NO_ENCONTRADO = "El Rubro no ha sido encontrado en la base de datos. No es posible agregarlo a la consulta al padron";
        public const string SSIT_CPADRON_RUBRO_NO_CATEGORIZADO_AMBIENTALMENTE = "El Rubro no ha sido Categorizado según el Impacto Ambiental para la superficie indicada. No es posible agregarlo a la solicitud";
        public const string SSIT_CPADRON_RUBRO_ZONA = "Para ingresar rubros en la consulta al padron es necesario haber seleccionado la Zona antes de ingresar un rubro.";
        public const string SSIT_SOLICITUD_NO_CAMBIOS = "El estado de la solicitud no admite cambios en los datos.";
        public const string SSIT_SOLICITUD_ECI_SIN_DOC_ANEXO = "La solicitud Nº {0} deberá estar acompañada de un Certificado de Espacio Cultural Independiente (CECI) para poder presentarla.";
        public const string SSIT_ENCOMIENDA_ECI_SIN_RUBRO_ANEXO = "La encomienda Nº {0} deberá estar acompañada del rubro {1} Espacio Cultural Independiente para poder tramitar la solicitud.";

        public const string UBICACION_INHIBIDA = "La ubicación que desea agregar se encuentra inhibida.";
        public const string UBICACION_SIN_ZONIFICACION = "La ubicación no posee zonificación, no es posible ingresarla.";
        public const string UBICACION_IGUAL = "No puede agregar 2 veces la misma ubicación, de ser necesario editar la existente.";

        public const string FILE_FORMATO_INCORRECTO = "El formato del archivo no es correcto.";
        public const string FILE_TAMANIO_INCORRECTO = "El tamaño máximo permitido para los documentos es de {0} MB";
        public const string FILE_TIPO_DOCUMENTO_SIN_FIRMA = "El Tipo de documento seleccionado requiere que el documento este firmado.";
        public const string FILE_TIPO_DOCUMENTO_CON_FIRMA = "El Tipo de documento seleccionado requiere que el documento no este firmado.";
        public const string FILE_DWF_SIN_VERSION = "No se ha podido leer la versión del plano. Por favor utilice una aplicación standard para exportarlo.";

        public const string SOLO_RUBRO_INDIVIDUAL = "El rubro {0} no puede compartir habilitación con otros rubros.";

        public const string FILE_DOCUMENTO_PROTEGIDO = "El documento esta protegido con contraseña o bien tiene un nivel de seguridad no permitido. Por favor verifique las propiedades del PDF.";

        public const string SSIT_TRANSFERENCIAS_SIN_ADJUNTOS = "Se deben adjuntar los documentos obligatorios, Edicto y Actuación Notarial";
        public const string SSIT_SOLICITUDES_CAMBIOS = "El estado de la solicitud no admite cambios en los datos.";
    }
    public class Engine
    {
        //public const decimal VersionCircuito1 = 1;
        public const decimal VersionCircuito2 = 2;
        public const decimal VersionCircuito3 = 3;
        public const decimal VersionCircuito4 = 4;

        public const string Sufijo_SolicitudHabilitacion = "06";
        public const string Sufijo_FinTramite = "29";
        public const string Sufijo_CorreccionSolicitud = "25";
        public const string Sufijo_AsginacionCalificador = "09";
        public const string Sufijo_GenerarExpediente = "22";
        public const string Sufijo_Calificar_1 = "10";
        public const string Sufijo_Calificar_2 = "01";
    }

    public class Constantes
    {
        public const string Sistema = "SSIT";
        public const string RubroNoContemplado = "888888";

        public const string BOLETA_UNICA = "Boleta única";
        public const int ID_BOLETA_UNICA = 1;
        public const string BUI_TRANSFERENCIA = "BUI_TRANSFERENCIA";
        public const string TIT_TITULARES = "Titulares y firmantes del tramite";
        public const string TIT_UBICACION = "Ubicación";

        public const string ENCOMIENDA_REG_ANT = "ENCOMIENDA_REG_ANT";
        public const string ENCOMIENDA_RNI_ANT = "ENCOMIENDA_RNI_ANT";
        public const string ESTADO_INCOMPLETO_DESCRIPCION = "Incompleto";

        public const string TipoPersonaFisica = "PF";
        public const string TipoPersonaFisica_Desc = "Persona Física";

        public const string TipoPersonaJuridica = "PJ";
        public const string TipoPersonaJuridica_Desc = "Persona Jurídica";

        public const string TipoAnexo_A = "A";
        public const string TipoAnexo_B = "B";

        public const string PathTemporal = "C:\\Temporal\\";

        public const string PLANCHETA = "plancheta";
        public const string OTROS = "otros";

        public const int SOLICITUDES_NUEVAS_MAYORES_A = 299999;

        public const string ContextInfoKey = "{3C2198FE-DAEE-11E0-B463-33B64824019B}";

        public static DateTime SolicitudFechaControlActaNotrial = new DateTime(2013, 04, 01);

        public static DateTime fechaImplementacionSSPAutomaticas = new DateTime(2017, 06, 07);

        public const string UserNameCookie = "__UserName";

        public const string EXTENSION_PDF = "pdf";
        public const string EXTENSION_DWF = "dwf";
        public const string EXTENSION_JPG = "jpg";

        public const string RUBRO_MUSICA_Y_CANTO = "1.5";


        public static class EncomiendaDirectorObra //los siguientes parametros fueron borrados de base a pedido
        {
            public const string LigueDeObra = "Ligue de Obra";
            public const string DesligueDeObra = "Desligue de Obra";
        }

        public static class TiposDeDocumentosPlancheta
        {
            public const string PlanchetaCPadron = "PlanchetaCPadron";
        }

        public enum EstadoEncomiendaExterna
        {
            PendienteIngreso = 2,
            Ingresado = 3,
            Aprobado = 4,
            RechaZado = 5,
            Anulado = 20
        }

        public enum SadeProcesos
        {
            RevFirma = 8
        }

        public enum TipoNormativa
        {
            DISP = 3
        }

        public enum EntidadNormativa
        {
            DGIUR = 5
        }

        public static class Parametros
        {
            public const string HostAutorizacion = "/Api/Authenticate";
            public const string HostAutorizacionUser = "WS-SGI";
            public const string HostAutorizacionPass = "123456";

            public const string HostFiles = "/api/files";
            public const string HostFilesDelete = "/api/DeleteFiles";
            public const string HostFilesAN = "/api/FilesAN";
            public const string HostMail = "/api/emails";
            public const string HostServicesPagos = "/ws.rest.pagos";
            public const string HostServicesFiles = "/ws.rest.files";
        }

        public static class TipoTramiteDescripcion
        {
            public const string Habilitacion = "Habilitación";
            public const string Transferencia = "Transferencia";
            public const string Ampliacion_Unificacion = "Ampliación / Unificación";
            public const string RedistribucionDeUso = "Redistribución de Uso";
            public const string RectificatoriaHabilitacion = "Rectificatoria de Habilitación";
            public const string HabilitacionECI = "Habilitación ECI";
            public const string AdecuacionECI = "Adecuación ECI";
        }
        public static class TipoExpedienteDescripcion
        {
            public const string NoDefinido = "Indeterminado";
            public const string Simple = "Simple";
            public const string Especial = "Especial";
        }

        public static class SubtipoDeExpedienteDescripcion
        {
            public const string InspeccionPrevia = " Inspección Previa";
            public const string HabilitacionPrevia = " Habilitación Previa";
            public const string ConPlanos = " (con planos)";
            public const string SinPlanos = " (sin planos)";
        }

        public static class Eng_Grupos_Circuitos
        {
            public const string SCPR = "SCP-R";
        }
        public static class ZonasPlaneamientos
        {
            public const string R1a = "R1a";
        }

        public static class RubrosCN
        {
            public const string Teatro_Independiente = "2.1.10";
            public const string Centro_Cultural_A = "2.1.2";
            public const string Centro_Cultural_B = "2.1.3";
            public const string Centro_Cultural_C = "2.1.4";
        }

        public enum ImpactoAmbiental
        {
            SinRelevanteEfecto = 1,
            SinRelevanteEfectoConCondiciones = 2,
            SujetoACategorización = 3,
            ConRelevanteEfecto = 4,
            NoPermitidoEnLaCiudad = 5
        }

        public enum TipoCertificado
        {
            EncomiendaHabilitacion = 1,
            APRA = 2,
            Acta_Notarial = 3,
            EncomiendaLey257 = 4,
            PlanchetaHabilitacion = 5,
            Disposicion = 6,
            BoletaUnica = 7,
            Consejo = 8,
            Formulario_inscripción_demoledores_excavadores = 10,
            //Agregados
            Ligue = 11,
            Desligue = 12
        }

        public enum Estado_Encomienda_Antenas
        {
            Incompleto = 0,
            Completo = 1,
            PendienteIngreso = 2,
            Ingresado = 3,
            Aprobado = 4,
            Rechazado = 5,
            Anulado = 20,
        }
        public enum SanitariosUbicacion
        {
            NoDefinido = 0,
            DentroLocal = 1,
            FueraLocal = 2
        }
        public enum TipoDestino
        {
            Archivo = 1,
            Baño = 2,
            Cocina = 3,
            Comedor = 4,
            CuadraElaboracion = 5,
            Deposito = 6,
            Guarda_ropas = 7,
            Hall = 8,
            Laboratorio = 9,
            Local = 10,
            ModuloEstacionamiento = 11,
            Oficina = 12,
            Paso = 13,
            PlayaCargaDescarga = 14,
            Sala = 15,
            SalaMaquinas = 16,
            Salon = 17,
            Taller = 18,
            Vestuario = 19,
            Otros = 20,
            BañoPcD = 21
        }

        public enum Localidad
        {
            CABA = 999
        }
        public enum Provincia
        {
            CABA = 2
        }

        public enum GrupoConsejos
        {
            SE = 0,
            CPAU = 1,
            CPA = 2,
            CPII = 3,
            COPIME = 4,
            CPIC = 5,
            CPIN = 6,
            COPITEC = 7,
            CPIQ = 8,
            CPIAyE = 9
        }
        public enum TieneRubroConExencionPago
        {
            SinExencion = 0,
            ProTeatro = 1,
            Estadio = 2,
            CentroCultural = 3,
            EsECI = 4
        }
        public enum TipoDocumentoRequerido
        {
            DeclaracionJuradaSinPlano = 1,
            DeclaracionJuradaConPlano = 2,
            Acta_Notarial = 20,
            ContestacionObservaciones = 28,
            Edicto = 51,
            ConstanciaInicioTramiteIGJoINAES = 95,
            CertificadoProTeatro = 96,
            Plancheta = 41,
            Habilitacion_Previa_PDF = 99,
            DDJJ_para_Ley_962 = 91,
            Oficio_Judicial = 102,
            Estatuto_Societario = 103,
            PublicacionEdicto = 104,
            Docuemento_Publico_Privado = 105,
            Habilitacion_Previa_JPG = 110,
            Disposicion_DGIUR = 21
        }

        public enum TipoDeTramite
        {
            Consulta = 0,
            Habilitacion = 1,
            Transferencia = 2,
            Ampliacion = 3,
            RedistribucionDeUso = 4,
            RectificatoriaHabilitacion = 5,
            ConsultaPadron = 6,
            Permisos = 7
        }
        public enum TipoDeExpediente
        {
            NoDefinido = 0,
            Simple = 1,
            Especial = 2,
            MusicaCanto = 3
        }
        public enum SubtipoDeExpediente
        {
            NoDefinido = 0,
            SinPlanos = 1,
            ConPlanos = 2,
            InspeccionPrevia = 3,
            HabilitacionPrevia = 4
        }
        public enum TiposDeUbicacion
        {
            ParcelaComun = 0,
            EstacionDeSubte = 1,
            EstacionDeTren = 2,
            EstacionamientoSubterráneo = 3,
            EstacionDeOmnibus = 4,
            GaleriaComercialSubterranea = 5,
            ObjetoTerritorial = 11
        }

        public enum TipoDocumentoPersonal
        {
            SE = 0,
            DNI = 1,
            LE = 2,
            LC = 3,
            CI = 4,
            PASAPORTE = 5
        }
        public enum Encomienda_Estados
        {
            Incompleta = 0,
            Completa = 1,
            Confirmada = 2,
            Ingresada_al_consejo = 3,
            Aprobada_por_el_consejo = 4,
            Rechazada_por_el_consejo = 5,
            Anulada = 20,
            Vencida = 24
        }
        public enum ConsultaPadronEstados
        {
            INCOM = 0,
            COMP = 1,
            PING = 2,
            VIS = 3,
            APRO = 4,
            OBS = 5,
            ANU = 20,
            OBSERVADO = 27,
        }
        public enum CodigoTareas
        {
            FinTarea = 429,
            SolicitudConsultaPadrón = 406,
            //SolicituddeTransferencias = 506,
            CalificarTrámite = 510,
            SolicituddeTransferencias = 706,
            CalificarTramiteTransmision = 710
        }
        public enum TareasResultados
        {
            SinEstablecer = 0,
            SolicitudConfirmada = 10,
            SolicitudAnulada = 11,
            //12	Solicitud Vencida
            //13	Pago Realizado
            //14	Asignación Realizada
            //16	Revisar Zonificación
            //17	Pedir Revisión técnica y legal
            //18	Pedir Inspección
            //19	Aprobado
            //20	Pedir Rectificación y/o Documentación Adicional
            //22	Devolver al Calificador
            //23	Estoy de Acuerdo con el Calificador
            //24	Devolver al Calificador
            //25	Estoy de Acuerdo con el Calificador
            //26	Devolver al Calificador
            //27	Estoy de Acuerdo con el Calificador
            //28	Devolver al Calificador
            //29	Estoy de Acuerdo con el Calificador
            //30	Asignar Calificador Técnico y Legal
            //31	Revision Técnica y Legal Realizada
            //32	Asignar Inspector
            //33	Inspección realizada
            //34	Zonificacion realizada
            //35	Pagos Verificados
            //36	Expediente Generado
            //37	Trámite Entregado
            //38	Enviado a P.V.H.
            //39	Enviar al Gestor
            //40	Boleta Generada
            //41	Disposición Firmada
            //43	Revisión Realizada
            //44	Rechazado
            //45	Carga Finalizada
            //46	Corrección de Datos
            //47	Finalizado
            EnviaraEscribano = 48,
            EscrituraRealizada = 49
            //50	Dictamen Aprobado
            //51	Tarea Completada
            //52	Devolver a Aprobados
            //53	Verificación Realizada
            //54	Vuelve AVH
            //55	Realizado
            //56	Requiere nueva revisión
            //57	Envío realizado
            //58	Enviar al Gerente
            //59	Enviar al Subgerente
            //60	Requiere rechazo
            //61	Ratifica calificación
            //62	No ratifica calificación
            //63	Check List Generado
            //64	Pedir nueva Inspección
            //65	Ratifica
            //66	No ratifica
            //67	Estoy de acuerdo con el profesional
            //68	Devolver al profesional
            //69	Devolver a Dictamen
            //70	Revisión DGHP
        }
        public enum TipoSociedad
        {
            Unipersonal = 1,
            Sociedad_Hecho = 2,
            Sociedad_Anonima = 3,
            S_R_L = 4,
            Asociacion_Civil = 5,
            Sociedad_comandita_acciones = 6,
            Sociedad_Extranjera = 7,
            Cooperativa = 8,
            Sociedad_Colectiva = 9,
            Entidad_Gremial = 10,
            Sociedad_Comandita_Simple = 11,
            Sociedad_Capital_e_Industria = 12,
            Sociedad_Responsabilidad_Limitada = 13,
            Sociedad_Estado = 14,
            Sociedad_Binacional = 15,
            Fundacion = 16,
            Entidad_Extranjera_sin_Fines_Lucro = 17,
            Simple_Asociacion = 18,
            Federacion = 19,
            Confederacion = 20,
            Camara = 21,
            Obra_Social = 22,
            Contrato_Colaboracion_Empresaria = 23,
            U_T_E = 24,
            Consorcio_Cooperacion = 25,
            Sociedad_Garantia_Reciproca = 26,
            Sociedad_Capitalizacion_y_Ahorro = 27,
            Mutual = 28,
            Asociacion_Gremial = 29,
            Sociedad_Civil = 30,
            Fideicomiso = 31,
            Sociedad_no_constituidas_regularmente = 32,
            Consorcio_Propietarios = 33
        }

        public enum TipoEstadoSolicitudEnum
        {
            INCOM = 0,
            COMP = 1,
            PING = 2,
            ING = 3,
            CAR = 9,
            ANU = 20,
            VENCIDA = 24,
            VISADO = 25,
            AUTORIZADO = 26,
            OBSERVADO = 27,
            ETRA = 28,
            APRO = 29,
            RECH = 30,
            VALPVH = 31,
            OBSPVH = 32,
            OBLEA = 33,
            REVOCADA = 34,
            PENPAG = 35,
            VALESCR = 36,
            ESCREAL = 37,
            SUSPEN = 38,
            DATOSCONF = 39,
            BAJA = 40,
            CADUCO = 41,
            REVCADUCO = 42,
            BAJAADM = 43,
            BAJAADMECI = 44
        }

        public enum TipoDocumentacionReq
        {
            DJ = 1,
            PP = 2,
            IP = 3,
            HP = 4
        }

        public enum TiposDePlanos
        {
            Habilitacion = 1,
            Contra_Incendio = 2,
            Ventilacion = 3,
            Otro = 4,
            PlanoMensura = 5,
            PlanoRedistribuciónDeUso = 6,
            HabilitacionAnterior = 7,
            Ampliacion = 8
        }

        public enum TipoTramiteCertificados
        {
            Encomienda_Digital = 1,
            Certificado_APRA = 2,
            Certificado_Acta_Notarial_Encomienda = 3,
            Encomienda_de_Ley_257 = 4,
            Plancheta_Habilitacion = 5,
            Disposicion = 6,
            Boleta_Unica = 7,
            Certificacion_del_consejo = 8,
            Certificado_Acta_Notarial_CAA = 9,
            Formulario_de_inscripcion_de_demoledores_y_excavadores = 10,
            Ligue_de_Obra = 11,
            Desligue_de_Obra = 12,
            Certif_consejo_habilitacion = 22
        }

        public enum TipoTransmision
        {
            Transmision_Transferencia = 1,
            Transmision_nominacion = 2,
            Transmision_oficio_judicial = 3
        }
        public enum CAA_TiposDeDocumentosSistema
        {
            CARATULA_CAA = 5,
            CERTIFICADO_CAA = 4,
            DISPOSICION_CAA = 6,
            DOC_ADJUNTO_CAA = 3,
            RECTIFICATORIA_CAA = 2,
            SOLICITUD_CAA = 1
        }

        public enum CAA_EstadoSolicitud
        {
            Incompleto = 0,
            Completo = 1,
            PendienteIngreso = 2,
            Ingresado = 3,
            Observado = 4,
            Aprobado = 5,
            Rechazado = 6,
            Anulado = 20
        }

        public enum ENG_Tareas
        {
            // tareas para tramites simples sin lanos
            SSP_Encomienda_Digital = 1,
            SSP_Certificacion_Encomienda = 2,
            SSP_Minuta_Acta_Notarial = 3,
            SSP_Certificado_Aptitud_Ambiental = 4,
            SSP_Solicitud_Habilitacion = 6,
            SSP_Revisión_Pagos_APRA = 8,
            SSP_Asignar_Calificador = 9,
            SSP_Calificar = 10,
            SSP_Revision_SubGerente = 11,
            SSP_Revision_Gerente = 12,
            SSP_Revision_Director = 13,
            SSP_Revision_DGHP = 14,
            SSP_Calificacion_Tecnica_Legal = 15,
            SSP_Revision_Tecnica_Legal = 16,
            SSP_Asignar_Inspector = 18,
            SSP_Resultado_Inspector = 19,
            SSP_Validar_Zonificacion = 20,
            SSP_Revision_Pagos = 21,
            SSP_Generar_Expediente = 22,
            SSP_Entregar_Tramite = 23,
            SSP_Enviar_PVH = 24,
            SSP_Correccion_Solicitud = 25,
            SSP_Generacion_Boleta = 26,
            SSP_Revision_Firma_Disposicion = 27,
            SSP_Aprobados = 28,
            SSP_Fin_Tramite = 134150,
            //Nuevo
            SSP_Solicitud_Habilitacion_Nuevo = 300,
            SSP_Asignar_Calificador_SubGerente_Nuevo = 301,
            SSP_Asignar_Calificador_Gerente_Nuevo = 302,
            SSP_Calificar_Nuevo = 303,
            SSP_Revision_SubGerente_Nuevo = 304,
            SSP_Revision_Gerente_Nuevo = 305,
            SSP_Revision_DGHP_Nuevo = 306,
            SSP_Generar_Expediente_Nuevo = 307,
            SSP_Revision_Firma_Disposicion_Nuevo = 308,
            SSP_Enviar_DGFC_Nuevo = 309,
            SSP_Fin_Tramite_Nuevo = 310,
            SSP_Correccion_Solicitud_Nuevo = 311,

            // tareas para tramites simples con planos
            SCP_Asignar_Calificador = 34,
            SCP_Calificar = 35,
            SCP_Revision_SubGerente = 36,
            SCP_Revision_Gerente = 37,
            SCP_Revision_Director = 38,
            SCP_Revision_DGHP = 39,
            SCP_Calificacion_Tecnica_Legal = 40,
            SCP_Revision_Tecnica_Legal = 41,
            SCP_Asignar_Inspector = 42,
            SCP_Resultado_Inspector = 43,
            SCP_Validar_Zonificacion = 44,
            SCP_Revision_Pagos = 45,
            SCP_Generar_Expediente = 46,
            SCP_Entregar_Tramite = 47,
            SCP_Enviar_PVH = 48,
            SCP_Correccion_Solicitud = 49,
            SCP_Generacion_Boleta = 50,
            SCP_Revision_Firma_Disposicion = 51,
            SCP_Aprobados = 72,
            SCP_Fin_Tramite = 53,
            //Nuevo
            SCP_Solicitud_Habilitacion_Nuevo = 400,
            SCP_Asignar_Calificador_SubGerente_Nuevo = 401,
            SCP_Asignar_Calificador_Gerente_Nuevo = 402,
            SCP_Calificar_Nuevo = 403,
            SCP_Revision_SubGerente_Nuevo = 404,
            SCP_Revision_Gerente_Nuevo = 405,
            SCP_Revision_DGHP_Nuevo = 406,
            SCP_Generar_Expediente_Nuevo = 407,
            SCP_Revision_Firma_Disposicion_Nuevo = 408,
            SCP_Enviar_DGFC_Nuevo = 409,
            SCP_Fin_Tramite_Nuevo = 410,
            SCP_Correccion_Solicitud_Nuevo = 411,

            // tareas para tramites especiales
            ESP_Asignar_Calificador = 101,
            ESP_Calificar_1 = 102,
            ESP_Verificacion_AVH = 103,
            ESP_Revision_SubGerente = 104,
            ESP_Revision_Gerente_1 = 105,
            ESP_Dictamen_Asignar_Profesional = 106,
            ESP_Dictamen_Revisar_Tramite = 107,
            ESP_Dictamen_Revision_SubGerente = 108,
            ESP_Dictamen_Revision_Gerente = 109,
            ESP_Dictamen_GEDO = 110,
            ESP_Generacion_Boleta = 111,
            ESP_Revision_Pagos = 112,
            ESP_Generar_Expediente = 113,
            ESP_Revision_DGHP = 114,
            ESP_Revision_Firma_Disposicion = 115,
            ESP_Aprobados = 116,
            ESP_Entregar_Tramite = 117,
            ESP_Rechazado_SADE = 118,
            ESP_Fin_Tramite = 119,
            ESP_Calificar_2 = 121,
            ESP_Revision_Gerente_2 = 122,
            ESP_Correccion_Solicitud = 120,
            //nuevo
            ESP_Asignar_Calificador_Nuevo = 501,
            ESP_Calificar_1_Nuevo = 502,
            ESP_Revision_SubGerente_1_Nuevo = 503,
            ESP_Revision_Gerente_1_Nuevo = 504,
            ESP_Generar_Ticket_Lisa_Nuevo = 505,
            ESP_Obtener_Ticket_Lisa_Nuevo = 506,
            ESP_Calificar_2_Nuevo = 507,
            ESP_Revision_SubGerente_2_Nuevo = 508,
            ESP_Revision_Gerente_2_Nuevo = 509,
            ESP_Dictamen_Asignar_Profesional_Nuevo = 510,
            ESP_Dictamen_Realizar_Nuevo = 511,
            ESP_Dictamen_Revision_Nuevo = 512,
            ESP_Revision_DGHP_Nuevo = 513,
            ESP_Generar_Expediente_Nuevo = 514,
            ESP_Revision_Firma_Disposicion_Nuevo = 515,
            ESP_Enviar_DGFC_Nuevo = 516,
            ESP_Fin_Tramite_Nuevo = 517,
            ESP_Verificacion_AVH_Nuevo = 519,
            ESP_Correccion_Solicitud_Nuevo = 518,

            // tareas para tramites transferencia
            TR_Asignar_Calificador = 61,
            TR_Calificar = 62,
            TR_Revision_SubGerente = 63,
            TR_Revision_Gerente_1 = 64,
            TR_Revision_Gerente_2 = 86,
            TR_Dictamen_Asignar_Profesional = 65,
            TR_Dictamen_Revisar_Tramite = 80,
            TR_Dictamen_Revision_SubGerente = 81,
            TR_Dictamen_Revision_Gerente = 82,
            TR_Dictamen_GEDO = 83,
            TR_Generacion_Boleta = 84,
            TR_Revision_Pagos = 85,
            TR_Generar_Expediente = 66,
            TR_Revision_DGHP = 67,
            TR_Revision_Firma_Disposicion = 68,
            TR_Aprobados = 73,
            TR_Entregar_Tramite = 69,
            TR_Fin_Tramite = 70,
            TR_Correccion_Solicitud = 60,

            // tareas para tramites consulta al padron 
            CP_Solicitud = 54,
            CP_Carga_Informacion = 55,
            CP_Revision_SubGerente = 74,
            CP_Generar_Expediente = 56,
            CP_Fin_Tramite = 57,
            CP_Correccion_Solicitud = 134151,

            // tareas para tramites esparcimiento
            ESPAR_Asignar_Calificador = 201,
            ESPAR_Calificar_1 = 202,
            ESPAR_Verificacion_AVH = 203,
            ESPAR_Calificar_2 = 204,
            ESPAR_Revision_SubGerente = 205,
            ESPAR_Revision_Gerente_1 = 206,
            ESPAR_Dictamen_Asignar_Profesional = 207,
            ESPAR_Dictamen_Revisar_Tramite = 208,
            ESPAR_Dictamen_Revision_SubGerente = 209,
            ESPAR_Dictamen_Revision_Gerente = 210,
            ESPAR_Dictamen_GEDO = 211,
            ESPAR_Revision_Gerente_2 = 212,
            ESPAR_Generacion_Boleta = 213,
            ESPAR_Revision_Pagos = 214,
            ESPAR_Generar_Expediente = 215,
            ESPAR_Revision_DGHP = 216,
            ESPAR_Revision_Firma_Disposicion = 217,
            ESPAR_Aprobados = 218,
            ESPAR_Entregar_Tramite = 219,
            ESPAR_Rechazado_SADE = 220,
            ESPAR_Fin_Tramite = 221,
            ESPA_Correccion_Solicitud = 222,
            //nuevo
            ESPAR_Asignar_Calificador_Nuevo = 601,
            ESPAR_Calificar_1_Nuevo = 602,
            ESPAR_Revision_SubGerente_1_Nuevo = 603,
            ESPAR_Revision_Gerente_1_Nuevo = 604,
            ESPAR_Generar_Ticket_Lisa_Nuevo = 605,
            ESPAR_Obtener_Ticket_Lisa_Nuevo = 606,
            ESPAR_Calificar_2_Nuevo = 607,
            ESPAR_Revision_SubGerente_2_Nuevo = 608,
            ESPAR_Revision_Gerente_2_Nuevo = 609,
            ESPAR_Dictamen_Asignar_Profesional_Nuevo = 610,
            ESPAR_Dictamen_Realizar_Nuevo = 611,
            ESPAR_Dictamen_Revision_Nuevo = 612,
            ESPAR_Revision_DGHP_Nuevo = 613,
            ESPAR_Generar_Expediente_Nuevo = 614,
            ESPAR_Revision_Firma_Disposicion_Nuevo = 615,
            ESPAR_Enviar_DGFC_Nuevo = 616,
            ESPAR_Fin_Tramite_Nuevo = 617,
            ESPAR_Verificacion_AVH_Nuevo = 619,
            ESPA_Correccion_Solicitud_Nuevo = 618,

            //Escuela
            SCP5_Generar_Expediente = 801,
            SCP5_Asignar_Calificador = 802,
            SCP5_Calificar_1 = 803,
            SCP5_Revision_SubGerente_1 = 804,
            SCP5_Revision_Gerente_1 = 805,
            SCP5_Verificacion_AVH = 806,
            SCP5_Calificar_2 = 807,
            SCP5_Revision_SubGerente_2 = 808,
            SCP5_Revision_Gerente_2 = 809,
            SCP5_Dictamen_Asignar_Profesional = 810,
            SCP5_Dictamen_Realizar = 811,
            SCP5_Dictamen_Revision = 812,
            SCP5_Revision_DGHP = 813,
            SCP5_Revision_Firma_Disposicion = 814,
            SCP5_Entregar_Tramite = 815,
            SCP5_Enviar_DGFC = 816,
            SCP5_Correccion_Solicitud = 817,
            SCP5_Fin_Tramite = 818,
            SCP5_Visado = 819,
            SCP5_Informar_Dpcimento_SADE = 820,


            //Escuela
            ESCU_HP_Generar_Expediente = 901,
            ESCU_HP_Asignar_Calificador = 902,
            ESCU_HP_Calificar_1 = 903,
            ESCU_HP_Revision_SubGerente_1 = 904,
            ESCU_HP_Revision_Gerente_1 = 905,
            ESCU_HP_Verificacion_AVH = 906,
            ESCU_HP_Calificar_2 = 907,
            ESCU_HP_Revision_SubGerente_2 = 908,
            ESCU_HP_Revision_Gerente_2 = 909,
            ESCU_HP_Dictamen_Asignar_Profesional = 910,
            ESCU_HP_Dictamen_Realizar = 911,
            ESCU_HP_Dictamen_Revision = 912,
            ESCU_HP_Revision_DGHP = 913,
            ESCU_HP_Revision_Firma_Disposicion = 914,
            ESCU_HP_Entregar_Tramite = 915,
            ESCU_HP_Enviar_DGFC = 916,
            ESCU_HP_Correccion = 917,
            ESCU_HP_Fin_Tramite = 918,
            ESCU_HP_Visado = 919,
            ESCU_HP_Informar_Dpcimento_SADE = 920,

            SCP3_Correccion_Solicitud = 1011,
            SCP4_Correccion_Solicitud = 1111
        }

        public static string TAREA_FORMULARIO_CALIFICAR = "Calificar.aspx";

        public enum GruposDeTramite {
            HAB = 1,
            CP = 2,
            TR = 3
        }

        public enum ENG_Resultados
        {
            SolicitudConfirmada = 10,
            SolicitudAnulada = 11,
            Realizado = 55
        }

        public enum ENG_Circuitos
        {
            SSP2 = 11,
            SCP2 = 12,
            SSP3 = 15,
            SSP3_AMP = 35,
            SSP2_AMP = 31,
            SCP2_AMP = 32,
            ESCU_IP = 16,
            ESCU_HP = 17,
            CPADRON = 4,
            TRANSF = 5,
            SCP4 = 19,
            SCP3 = 18,
            ESPAR2 = 14,
            ESPAR2_AMP = 34,
            ESPAR2_REDISTRIBUCION_USO = 54,
            SCP2_REDISTRIBUCION_USO = 52,
            SSP3_AGIL = 80,
            TRANSF_NUEVO = 7,
        }

        public enum ENG_Grupo_Circuitos
        {
            SSPA = 2
        }

        public enum TiposDeDocumentosSistema
        {
            PLANCHETA_CPADRON = 1,
            ENCOMIENDA_DIGITAL = 2,
            INFORMES_CPADRON = 3,
            CERTIFICADO_CAA = 4,
            DOC_ADJUNTO_CPADRON = 5,
            CARATULA_CPADRON = 6,
            PLANCHETA_TRANSFERENCIA = 7,
            DOC_ADJUNTO_TRANSFERENCIA = 8,
            CARATULA_TRANSFERENCIA = 9,
            ACTUACION_NOTARIAL = 10,
            DISPOSICION_CPADRON = 11,
            DISPOSICION_TRANSFERENCIA = 12,
            OBLEA_SOLICITUD = 13,
            CARATULA_HABILITACION = 14,
            DISPOSICION_HABILITACION = 15,
            BUI_TRANSFERENCIA = 16,
            DOC_ADJUNTO_SSIT = 18,
            DOC_ADJUNTO_ENCOMIENDA = 19,
            SOLICITUD_HABILITACION = 20,
            PLANCHETA_HABILITACION = 21,
            CERTIF_CONSEJO_HABILITACION = 22,
            SOLICITUD_CPADRON = 23,
            CERTIFICADO_HABILITACION = 24,
            PRESENTACION_A_AGREGAR = 25,
            DECLARACION_RESPONSABLE = 26,
            MANIFIESTO_TRANSMISION = 27,
            PERMISO_MC = 28
        }

        public enum CAA_TipoCertificado
        {
            SinRelevanteEfecto = 1,
            SinRelevanteEfectoconCondiciones = 2,
            SujetoaCategorizacion = 3,
            ConRelevanteEfecto = 4
        }

        public enum TiposDeTramiteCAA
        {
            CAA = 1,
            CAA_ESP = 3
        }

        // Esta clase es para el control de pagos, 
        // para determinar si son los de habilitaciones o los  del CAA
        public enum PagosTipoTramite
        {
            HAB = 1,
            CAA = 2,
            TR = 3,
            AMP = 4
        }
        public enum TipoTransaccionTarea
        {
            Delete = 1,
            Update = 2
        }
        public enum AzraelTipoTramite
        {
            SSIT = 1,
            CPadron = 2,
            Encomienda = 3,
            Transferencia = 4,
            CAA = 5,
            SGI_HAB = 6
        }
        public enum TipoTramite
        {
            HABILITACION = 1,
            TRANSFERENCIA = 2,
            AMPLIACION = 3,
            HabilitacionECIAdecuacion = 3,
            HabilitacionECIHabilitacion = 1,
            REDISTRIBUCION_USO = 4,
            CONSULTA_PADRON = 6,
            PERMISO = 7,
            LIGUE = 11,
            DESLIGUE = 12
        }

        public enum BUI_EstadoPago
        {
            SinPagar = 0,
            Pagado = 1,
            Vencido = 2,
            Cancelada = 3,
            Anulada = 4
        }
        public enum TipoSector
        {
            Ninguno = 1,
            Azotea = 2,
            Entrepiso = 3,
            PlantaAlta = 4,
            Sótano = 6,
            Subsuelo = 7,
            PB = 8,
            Entresuelo = 9,
            Piso = 10,
            Otro = 11
        }

        public enum MotivosNotificaciones
        {
            InicioHabilitación = 1,
            Observado = 2,
            Rechazado = 3,
            Aprobado = 4,
            AvisoCaducidadPróximoCaducar = 5,
            avisoCaducidad = 6,
            BajaDeSolicitud = 7,
            QRDisponible = 8,
            AnexoTecnicoAnulado = 9,
            SolicitudConfirmada = 10
        }

        public struct ENG_Tipos_Tareas
        {
            public const string Calificar = "10";
            public const string Calificar2 = "01";
            public const string Correccion_Solicitud = "25";
            public const string Enviar_a_DGFyC = "35";
            public const string Revision_Gerente = "12";
            public const string Revision_Gerente2 = "02";
            public const string Revision_SubGerente = "11";
            public const string Revision_SubGerente2 = "03";
            public const string Asignacion_Calificador = "09";
            public const string Asignacion_Calificador2 = "08";
            public const string Dictamen_Asignacon = "40";
            public const string Dictamen_Realizar = "41";
            public const string Dictamen_Revision = "43";
            public const string Entregar_Tramite = "23";
            public const string Fin_Tramite = "29";
            public const string Generar_Expediente = "22";
            public const string Revision_DGHyP = "14";
            public const string Revision_DGHyP2 = "15";
            public const string Revision_Firma_Disposicion = "27";
            public const string Revision_Firma_Disposicion2 = "32";
            public const string Solicitud_Habilitacion = "06";
            public const string Verificacion_AVH = "31";
            public const string Visado = "47";
        }

        public enum TipoParticipante
        {
            Titular = 1,
            Solicitante = 4,
            TitularComplementario = 23
        }
    }

    public static class Funciones
    {
        /// <summary>
        /// Arma una lista de String para mostrar los mensajes de errores en un solo grupo.
        /// </summary>
        /// <param name="ListaErrores"></param>
        /// <returns></returns>
        public static string MensajeError(List<string> ListaErrores)
        {
            string errorMsg = "<ul><li>";

            foreach (var item in ListaErrores.Distinct())
                errorMsg += item + "</li><li>";

            errorMsg = errorMsg.Substring(0, errorMsg.LastIndexOf("<li>")) + "</ul>";

            return errorMsg;
        }

        public static string FechaString()
        {
            string fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return "[" + fecha + "]: ";
        }
        public static byte[] ConvertImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
        }
        public static byte[] StreamToArray(Stream input)
        {
            byte[] buffer = new byte[20 * 1024 * 1024]; // 20 mb
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        public static byte[] ImageToByte(System.Drawing.Image pImagen)
        {
            byte[] mImage;

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            pImagen.Save(ms, pImagen.RawFormat);
            mImage = ms.GetBuffer();
            ms.Close();

            return mImage;

        }

        public static string getGenerarCodigoSeguridadEncomiendas()
        {
            Random rnd = new Random();
            return Math.Round(rnd.NextDouble() * 9, 0, MidpointRounding.AwayFromZero).ToString() +
                                                 Convert.ToChar(Convert.ToInt32(Math.Round(rnd.NextDouble() * 25 + 65, 0, MidpointRounding.AwayFromZero))) +
                                                 Math.Round(rnd.NextDouble() * 9, 0, MidpointRounding.AwayFromZero).ToString() +
                                                 Convert.ToChar(Convert.ToInt32(Math.Round(rnd.NextDouble() * 25 + 65, 0, MidpointRounding.AwayFromZero)));
        }



        public static string GetErrorMessage(Exception ex)
        {
            string ret = ex.Message;
            Exception lex = ex;
            while (lex.InnerException != null)
            {
                lex = lex.InnerException;
            }

            if (lex != null)
                ret = lex.Message;

            return ret;
        }

        public static string ConvertToBase64String(int value)
        {
            byte[] str1Byte = Encoding.ASCII.GetBytes(Convert.ToString(value));
            string base64 = Convert.ToBase64String(str1Byte);
            return base64;
        }

        public static int ConvertFromBase64StringToInt32(string value)
        {
            byte[] bValue = Convert.FromBase64String(value);
            string res = Encoding.ASCII.GetString(bValue);
            int ret = int.Parse(res);

            return ret;
        }

        public static string IPtoDomain(string url)
        {
            string ret = url.Replace("10.20.72.31", "www.dghpsh.agcontrol.gob.ar");
            ret = ret.Replace("10.20.72.23", "instalaciones.agcontrol.gob.ar");
            ret = ret.Replace("azufral.agc", "instalaciones.agcontrol.gob.ar");

            return ret;
        }

        public static bool validarEmail(string email)
        {
            bool esValido = false;
            //string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            string expresion = "^[_a-zA-Z0-9-]+(\\.[_a-zA-Z0-9-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*(\\.[a-z]{2,3})";

            if (System.Text.RegularExpressions.Regex.IsMatch(email, expresion))
            {
                if (System.Text.RegularExpressions.Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    esValido = true;
                }
            }

            return esValido;
        }

        public static string SoloDigitos(string cadena)
        {
            string cadenaSalida = "";

            foreach (char item in cadena.ToCharArray())
            {
                if (Char.IsDigit(item))
                {
                    cadenaSalida = cadenaSalida + item;
                }
            }

            return cadenaSalida;
        }

        public static string GetUrlMapa(int seccion, string manzana, string parcela, string Direccion)
        {

            string ret = "";
            string SMP = "";
            int tamaManzana = 3;
            int tamaParcela = 3;
            manzana = manzana.Trim();
            parcela = parcela.Trim();
            Direccion = Direccion.Trim();

            SMP += seccion.ToString().PadLeft(2, Convert.ToChar("0"));
            SMP += "-";

            if (manzana.Length > 0)
            {
                if (!Char.IsNumber(manzana, manzana.Length - 1))
                    tamaManzana = 4;
            }

            SMP += manzana.PadLeft(tamaManzana, Convert.ToChar("0"));
            SMP += "-";

            if (parcela.Length > 0)
            {
                if (!Char.IsNumber(parcela, parcela.Length - 1))
                    tamaParcela = 4;
            }

            SMP += parcela.PadLeft(tamaParcela, Convert.ToChar("0"));

            ret = string.Format("http://servicios.usig.buenosaires.gob.ar/LocDir/mapa.phtml?dir={0}&desc={0}&w=400&h=300&punto=5&r=200&smp={1}",
                        Direccion, SMP);
            return ret;

        }

        public static string GetUrlMapa(int seccion, string manzana, string parcela, double Coordenada_X, double Coordenada_Y)
        {

            string ret = "";
            string SMP = "";
            int tamaManzana = 3;
            int tamaParcela = 3;
            manzana = manzana.Trim();
            parcela = parcela.Trim();

            SMP += seccion.ToString().PadLeft(2, Convert.ToChar("0"));
            SMP += "-";

            if (manzana.Length > 0)
            {
                if (!Char.IsNumber(manzana, manzana.Length - 1))
                    tamaManzana = 4;
            }

            SMP += manzana.PadLeft(tamaManzana, Convert.ToChar("0"));
            SMP += "-";

            if (parcela.Length > 0)
            {
                if (!Char.IsNumber(parcela, parcela.Length - 1))
                    tamaParcela = 4;
            }

            SMP += parcela.PadLeft(tamaParcela, Convert.ToChar("0"));

            ret = string.Format("http://servicios.usig.buenosaires.gob.ar/LocDir/mapa.phtml?x={0}&y={1}&w=400&h=300&punto=5&r=200&smp={1}", Coordenada_X.ToString(new CultureInfo("en-US")), Coordenada_Y.ToString(new CultureInfo("en-US")), SMP);

            return ret;

        }
        public static string GetUrlCroquis(int seccion, string manzana, string parcela, double Coordenada_X, double Coordenada_Y)
        {

            string ret = "";
            string SMP = "";
            int tamaManzana = 3;
            int tamaParcela = 3;
            manzana = manzana.Trim();
            parcela = parcela.Trim();

            SMP += seccion.ToString().PadLeft(2, Convert.ToChar("0"));
            SMP += "-";

            if (manzana.Length > 0)
            {
                if (!Char.IsNumber(manzana, manzana.Length - 1))
                    tamaManzana = 4;
            }

            SMP += manzana.PadLeft(tamaManzana, Convert.ToChar("0"));
            SMP += "-";

            if (parcela.Length > 0)
            {
                if (!Char.IsNumber(parcela, parcela.Length - 1))
                    tamaParcela = 4;
            }

            SMP += parcela.PadLeft(tamaParcela, Convert.ToChar("0"));

            ret = string.Format("http://servicios.usig.buenosaires.gob.ar/LocDir/mapa.phtml?x={0}&y={1}&w=400&h=300&punto=5&r=50&smp={2}", Coordenada_X.ToString(new CultureInfo("en-US")), Coordenada_Y.ToString(new CultureInfo("en-US")), SMP);
            return ret;

        }
        public static string GetUrlCroquis(int seccion, string manzana, string parcela, string Direccion)
        {

            string ret = "";
            string SMP = "";
            int tamaManzana = 3;
            int tamaParcela = 3;
            manzana = manzana.Trim();
            parcela = parcela.Trim();
            Direccion = Direccion.Trim();

            SMP += seccion.ToString().PadLeft(2, Convert.ToChar("0"));
            SMP += "-";

            if (manzana.Length > 0)
            {
                if (!Char.IsNumber(manzana, manzana.Length - 1))
                    tamaManzana = 4;
            }

            SMP += manzana.PadLeft(tamaManzana, Convert.ToChar("0"));
            SMP += "-";

            if (parcela.Length > 0)
            {
                if (!Char.IsNumber(parcela, parcela.Length - 1))
                    tamaParcela = 4;
            }

            SMP += parcela.PadLeft(tamaParcela, Convert.ToChar("0"));

            ret = string.Format("http://servicios.usig.buenosaires.gob.ar/LocDir/mapa.phtml?dir={0}&w=400&h=300&punto=5&r=50&smp={1}",
                     Direccion, SMP);
            return ret;

        }

        public static string GetUrlFoto(int seccion, string manzana, string parcela, int ancho, int alto)
        {
            string ret = "";
            string SMP = "";
            int tamaManzana = 3;
            int tamaParcela = 3;

            manzana = manzana.Trim();
            parcela = parcela.Trim();


            SMP += seccion.ToString().PadLeft(2, Convert.ToChar("0"));
            SMP += "-";

            if (manzana.Length > 0)
            {
                if (!Char.IsNumber(manzana, manzana.Length - 1))
                    tamaManzana = 4;
            }

            SMP += manzana.PadLeft(tamaManzana, Convert.ToChar("0"));
            SMP += "-";

            if (parcela.Length > 0)
            {
                if (!Char.IsNumber(parcela, parcela.Length - 1))
                    tamaParcela = 4;
            }

            SMP += parcela.PadLeft(tamaParcela, Convert.ToChar("0"));

            ret = string.Format("http://fotos.usig.buenosaires.gob.ar/getFoto?smp={0}&i=0&h={1}&w={2}", SMP, alto, ancho);

            return ret;

        }

        public static bool isFirmadoPdf(byte[] archivo)
        {
            bool ret = false;
            PdfReader reader = new PdfReader(archivo);
            AcroFields af = reader.AcroFields;
            ret = af.GetSignatureNames().Count > 0;
            reader.Dispose();
            return ret;
        }

        public static bool is_Ampliaciones_Implementado()
        {
            bool ret = false;
            string value = System.Configuration.ConfigurationManager.AppSettings["Ampliaciones_Implementado"];
            if (!string.IsNullOrEmpty(value))
            {
                ret = Convert.ToBoolean(value);
            }

            return ret;
        }

        public static bool is_ECI_Implementado()
        {
            bool ret = false;
            string value = System.Configuration.ConfigurationManager.AppSettings["ECI_Implementado"];
            if (!string.IsNullOrEmpty(value))
            {
                ret = Convert.ToBoolean(value);
            }

            return ret;
        }

        public static bool is_MC_Implementado()
        {
            bool ret = false;
            string value = System.Configuration.ConfigurationManager.AppSettings["MC_Implementado"];
            if (!string.IsNullOrEmpty(value))
            {
                ret = Convert.ToBoolean(value);
            }

            return ret;
        }
        public static bool is_RedistribucionUso_Implementado()
        {
            bool ret = false;
            string value = System.Configuration.ConfigurationManager.AppSettings["RedistribucionUso_Implementado"];
            if (!string.IsNullOrEmpty(value))
            {
                ret = Convert.ToBoolean(value);
            }

            return ret;
        }

        public static bool isDesarrollo()
        {
            string value = System.Configuration.ConfigurationManager.AppSettings["isDesarrollo"];
            bool desarrollo = false;
            bool.TryParse(value, out desarrollo);
            return desarrollo;
        }

        public static bool isLogs()
        {
            string value = System.Configuration.ConfigurationManager.AppSettings["isLogs"];
            bool desarrollo = false;
            bool.TryParse(value, out desarrollo);
            return desarrollo;
        }

        public static bool IsDigitsOnly(string text)
        {
            foreach (char c in text)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }


    }

    public static class Extensions
    {
        /// <summary>
        /// Gets the 12:00:00 instance of a DateTime
        /// </summary>
        public static DateTime AbsoluteStart(this DateTime dateTime)
        {
            return dateTime.Date;
        }

        /// <summary>
        /// Gets the 11:59:59 instance of a DateTime
        /// </summary>
        public static DateTime AbsoluteEnd(this DateTime dateTime)
        {
            return AbsoluteStart(dateTime).AddDays(1).AddTicks(-1);
        }

        /// <summary>
        /// Gets the First day of the month at 12:00:00 of a DateTime
        /// </summary>
        public static DateTime FirstDayOfTheMonth(this DateTime dateTime)
        {
            return AbsoluteStart(new DateTime(dateTime.Year, dateTime.Month, 1));
        }

        /// <summary>
        /// Gets the Last day of the month at 11:59:59 of a DateTime
        /// </summary>
        public static DateTime LastDayOfTheMonth(this DateTime dateTime)
        {
            return AbsoluteEnd(FirstDayOfTheMonth(dateTime).AddMonths(1).AddDays(-1));
        }
    }
}
