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
    
    public partial class Ubicaciones_Acciones
    {
        public Ubicaciones_Acciones()
        {
            this.Ubicaciones_Operaciones = new HashSet<Ubicaciones_Operaciones>();
        }
    
        public int id_accion { get; set; }
        public string Descripcion { get; set; }
    
        public virtual ICollection<Ubicaciones_Operaciones> Ubicaciones_Operaciones { get; set; }
    }
}
