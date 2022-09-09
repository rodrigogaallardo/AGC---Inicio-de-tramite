using System;

namespace DataTransferObject
{
    public class TiposDeDocumentosRequeridosDTO
    {
        public int id_tdocreq { get; set; }
        public string nombre_tdocreq { get; set; }
        public string observaciones_tdocreq { get; set; }
        public bool baja_tdocreq { get; set; }        
        public DateTime CreateDate { get; set; }
        public Guid CreateUser { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public Guid? LastUpdateUser { get; set; }
        public bool RequiereDetalle { get; set; }
        public bool visible_en_SSIT { get; set; }
        public bool visible_en_SGI { get; set; }
        public int? tamanio_maximo_mb { get; set; }
        public string formato_archivo { get; set; }
        public string acronimo_SADE { get; set; }
        public bool visible_en_Obs { get; set; }
        public int? id_tipdocsis { get; set; }
        public bool verificar_firma_digital { get; set; }
        public string Descripcion_compuesta
        {
            get { return nombre_tdocreq + "|" + observaciones_tdocreq; }
        }
    }
}
