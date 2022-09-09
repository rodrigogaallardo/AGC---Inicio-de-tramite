using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class SolicitudDTO
    {
        public int NroSolicitud { get; set; }
        public System.DateTime FechaSolicitud { get; set; }
        public System.Guid Usuario { get; set; }
        public Nullable<int> IdTipoActividad { get; set; }
        public Nullable<int> IdTipoSociedad { get; set; }
        public string ZonaDeclarada { get; set; }
        public Nullable<decimal> Superficie { get; set; }
        public Nullable<int> CantidadOperarios { get; set; }
        public Nullable<int> IdTipoNormativa { get; set; }
        public string NroNormativa { get; set; }
        public Nullable<int> IdEntidadNormativa { get; set; }
        public Nullable<int> AnioNormativa { get; set; }
        public Nullable<int> IdProfesional { get; set; }
        public Nullable<int> MatriculaEscribano { get; set; }
        public Nullable<System.DateTime> FechaIngreso { get; set; }
        public Nullable<System.Guid> UsuarioIngreso { get; set; }
        public string NroCarpeta { get; set; }
        public string NroExpediente { get; set; }
        public int IdEstado { get; set; }
        public string OtraPuerta { get; set; }
        public bool PresentaPlanos { get; set; }
        public Nullable<System.Guid> UpdateUser { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<System.Guid> UsuarioCambioEstado { get; set; }
        public Nullable<System.DateTime> FechaCambioEstado { get; set; }
        public int id_tipotramite { get; set; }
        public int id_tipoexpediente { get; set; }
        public int id_subtipoexpediente { get; set; }
        public Nullable<bool> Valid_deuda_agip { get; set; }
    
    }
}
