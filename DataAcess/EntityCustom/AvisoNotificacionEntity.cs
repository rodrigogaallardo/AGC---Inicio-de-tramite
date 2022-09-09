using System;

namespace DataAcess.EntityCustom
{
    public class AvisoNotificacionEntity
    {
        public int IdNotificacion { get; set; }
        public int IdTramite { get; set; }
        public string AsuntoMail { get; set; }
        public DateTime FechaAviso { get; set; }
        public string Domicilio { get; set; }
        public int IdMail { get; set; }
        public int? Id_NotificacionMotivo { get; set; }
        public string NotificacionMotivo { get; set; }
        public string Url { get; set; }
        public DateTime? FechaNotificacion { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
