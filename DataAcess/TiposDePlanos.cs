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
    
    public partial class TiposDePlanos
    {
        public TiposDePlanos()
        {
            this.Encomienda_Planos = new HashSet<Encomienda_Planos>();
        }
    
        public int id_tipo_plano { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public Nullable<bool> requiere_detalle { get; set; }
        public string extension { get; set; }
        public Nullable<int> tamanio_max_mb { get; set; }
        public string acronimo_SADE { get; set; }
    
        public virtual ICollection<Encomienda_Planos> Encomienda_Planos { get; set; }
    }
}