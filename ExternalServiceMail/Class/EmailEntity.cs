using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalService
{
    public class EmailEntity
    {
        public int IdEmail { get; set; }
        public Guid? Guid { get; set; }
        public int IdOrigen { get; set; }
        public int IdTipoEmail { get; set; }
        public int IdEstado { get; set; }
        public int? CantIntentos { get; set; }
        public int? CantMaxIntentos { get; set; }
        public int? Prioridad { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public string Email { get; set; }
        public string Cc { get; set; }
        public string Cco { get; set; }
        public string Asunto { get; set; }
        public string Html { get; set; }
        public DateTime? FechaLectura { get; set; }
    }

    public class EmailServicePOST
    {
        public int? Prioridad { get; set; }
        public string Email { get; set; }
        public string Cc { get; set; }
        public string Cco { get; set; }
        public string Asunto { get; set; }
        public int? IdTipoEmail { get; set; }
        public string Html { get; set; }
    }

    public enum TiposDeMail
    {
        RechazoEmpresaConservadoraAscensor = 1,
        RechazoECA_Reiteracion = 2,
        Elevadores_PendientesRechazoOAceptacion = 3,
        Elevadores_ConObleaDisponible = 4,
        WebSGI_AvisoCaratula = 5,
        WebSGI_CorreccionSolicitud = 6,
        WebSGI_Rechazo = 7,
        WebSGI_AprobacionDG = 8,
        WebConsorcio_EnvioAvisoUnicaVez = 9,
        WebSIPSA_AvisoCertificadoExentoTesoreria = 10,
        Generico = 11,
        Recordatorio = 12,
        CreacionUsuario = 13,
        RecuperoContrasena = 14,
        AnulacionAnexoTecnico = 15
    }

    public enum MailOrigenes
    {
        WebConsorcio_RechazoEmpresaConservadoraAscensor = 1,
        WebConsorcio_RechazoECAReiteracion = 2,
        WebECA_ElevadoresPendientesRechazoOAceptacion = 3,
        WebECA_ElevadoresConobleaDisponible = 4,
        WebSGI_AvisoCaratula = 5,
        WebSGI_CorreccionSolicitud = 6,
        WebSGI_Rechazo = 7,
        WebSGI_AprobacionDG = 8,
        WebConsorcio_EnvioAvisoUnicaVez = 9,
        WebSIPSA_AvisoCertificadoExentoTesoreria = 10,
        Empresas_InstalacionesFijasContraIncendio = 11,
        AGC = 12,
        WebSGI_ConfirmacionSolicitud = 13,
        SAGI_SistemaInternodaAPRA = 14,
        SSIT = 15,
        DGFYCO_Instalaciones = 17
    }

    public enum TiposDeEstadosEmail
    {
        PendienteDeEnvio = 1,
        Enviado = 2,
        NoEnviadoSuperaIntentos = 3,
        EnvioCancelado = 4
    }
}
