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
    
    public partial class Rel_Encomienda_Rectificatoria
    {
        public int id_relencrec { get; set; }
        public int id_encomienda_anterior { get; set; }
        public int id_solicitud_anterior { get; set; }
        public int id_encomienda_nueva { get; set; }
    
        public virtual Encomienda Encomienda { get; set; }
        public virtual Encomienda Encomienda1 { get; set; }
    }
}
