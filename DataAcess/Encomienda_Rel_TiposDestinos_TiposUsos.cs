//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAcess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Encomienda_Rel_TiposDestinos_TiposUsos
    {
        public int id_rel_tipodestino_tipouso { get; set; }
        public int id_tipo_destino { get; set; }
        public int id_tipo_uso { get; set; }
        public decimal valor_min_req { get; set; }
        public bool requiere_detalle { get; set; }
        public string texto_fijo_detalle { get; set; }
    
        public virtual Encomienda_Tipos_Destinos Encomienda_Tipos_Destinos { get; set; }
        public virtual Encomienda_Tipos_Usos Encomienda_Tipos_Usos { get; set; }
    }
}
