using System;

namespace DataTransferObject
{
    public class TransferenciasNotificacionesDTO
    {
        public int id_notificacion { get; set; }
        public int id_solicitud { get; set; }
        public int id_email { get; set; }
        public DateTime createDate { get; set; }
        public DateTime? fechaNotificacionSSIT { get; set; }
        public Nullable<int> Id_NotificacionMotivo { get; set; }
    }
}
