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
    
    public partial class Ubicaciones_Puertas
    {
        public int id_ubic_puerta { get; set; }
        public int id_ubicacion { get; set; }
        public string tipo_puerta { get; set; }
        public int codigo_calle { get; set; }
        public int NroPuerta_ubic { get; set; }
    
        public virtual Ubicaciones Ubicaciones { get; set; }
    }
}
