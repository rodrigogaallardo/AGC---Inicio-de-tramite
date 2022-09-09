using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaticClass;

namespace DataTransferObject
{
    public class EncomiendaConformacionLocalDTO
    {
        public int id_encomiendaconflocal { get; set; }
        public int id_encomienda { get; set; }
        public int id_destino { get; set; }
        public decimal? largo_conflocal { get; set; }
        public decimal? ancho_conflocal { get; set; }
        public decimal? alto_conflocal { get; set; }
        public string Paredes_conflocal { get; set; }
        public string Techos_conflocal { get; set; }
        public string Pisos_conflocal { get; set; }
        public string Frisos_conflocal { get; set; }
        public string Observaciones_conflocal { get; set; }
        public string Detalle_conflocal { get; set; }
        public int? id_encomiendatiposector { get; set; }
        public int? id_ventilacion { get; set; }
        public int? id_iluminacion { get; set; }
        public decimal? superficie_conflocal { get; set; }
        public int id_tiposuperficie { get; set; }
        public DateTime? CreateDate { get; set; }
        public Guid? CreateUser { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public Guid LastUpdateUser { get; set; }

        public virtual EncomiendaPlantasDTO EncomiendaPlantasDTO { get; set; }
        public virtual TipoSuperficieDTO TipoSuperficieDTO { get; set; }
        public virtual TipoVentilacionDTO TipoVentilacionDTO { get; set; }
        public virtual TipoDestinoDTO TipoDestinoDTO { get; set; }
        public virtual TipoIluminacionDTO TipoIluminacionDTO { get; set; }
        
        public string desc_planta {
            get{

                return (EncomiendaPlantasDTO.TipoSectorDTO.Id == (int)Constantes.TipoSector.Otro ? EncomiendaPlantasDTO.detalle_encomiendatiposector : EncomiendaPlantasDTO.TipoSectorDTO.Nombre);
            }
        } 

    }
}
