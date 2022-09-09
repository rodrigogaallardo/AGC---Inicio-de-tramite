using System;
using System.Collections.Generic;

namespace DataTransferObject
{
    public class SSITSolicitudesNuevasDTO
    {
        public int IdSolicitud { get; set; }
        public string CodigoSeguridad { get; set; }
        public int IdTipoTramite { get; set; }
        public int IdEstado { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreateUser { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public Guid? LastUpdateUser { get; set; }
        public string Nombre_RazonSocial { get; set; }
        public string Cuit { get; set; }
        public string Nombre_Profesional { get; set; }
        public string Matricula { get; set; }
        public int NroPartidaMatriz { get; set; }
        public int NroPartidaHorizontal { get; set; }
        public string Nombre_calle { get; set; }
        public int Altura_calle { get; set; }

        public string Piso { get; set; }
        public string UnidadFuncional { get; set; }
        public string Descripcion_Actividad { get; set; }
        public string CodZonaHab { get; set; }
        public decimal Superficie { get; set; }

        public string TipoTramiteDescripcion { get; set; }




        public Nullable<int> idTAD { get; set; }


        public virtual TipoEstadoSolicitudDTO TipoEstadoSolicitudDTO { get; set; }
        public virtual ICollection<RelRubrosSolicitudesNuevasDTO> RelRubrosSolicitudesNuevasDTO { get; set; }

    }
}


