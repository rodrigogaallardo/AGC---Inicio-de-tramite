//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAcess
{
    using System;
    using System.Collections.Generic;
    
    public partial class wsEscribanos_InstrumentoPublico
    {
        public int id_inspub { get; set; }
        public int id_derecho_ocupacion { get; set; }
        public Nullable<System.DateTime> fecha_escritura_inspub { get; set; }
        public Nullable<int> nro_escritura_inspub { get; set; }
        public int nro_matricula_escribano_inspub { get; set; }
        public string registro_inspub { get; set; }
        public string jurisdiccion_inspub { get; set; }
        public string matricula_registro_prop_inspub { get; set; }
    
        public virtual wsEscribanos_DerechoOcupacion wsEscribanos_DerechoOcupacion { get; set; }
    }
}
