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
    
    public partial class Ley257_Estados
    {
        public Ley257_Estados()
        {
            this.Ley257_Solicitudes = new HashSet<Ley257_Solicitudes>();
        }
    
        public int id_estado { get; set; }
        public string cod_estado { get; set; }
        public string nom_estado { get; set; }
        public System.DateTime CreateDate { get; set; }
    
        public virtual ICollection<Ley257_Solicitudes> Ley257_Solicitudes { get; set; }
    }
}
