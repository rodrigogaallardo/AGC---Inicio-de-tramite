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
    
    public partial class Ubicaciones_CatalogoDistritos_Zonas
    {
        public Ubicaciones_CatalogoDistritos_Zonas()
        {
            this.Ubicaciones_CatalogoDistritos_Subzonas = new HashSet<Ubicaciones_CatalogoDistritos_Subzonas>();
            this.Ubicaciones_Distritos_Excepciones_Mixturas = new HashSet<Ubicaciones_Distritos_Excepciones_Mixturas>();
            this.Ubicaciones_Distritos_Excepciones_RubrosCN = new HashSet<Ubicaciones_Distritos_Excepciones_RubrosCN>();
        }
    
        public int IdZona { get; set; }
        public int IdDistrito { get; set; }
        public string CodigoZona { get; set; }
    
        public virtual Ubicaciones_CatalogoDistritos Ubicaciones_CatalogoDistritos { get; set; }
        public virtual ICollection<Ubicaciones_CatalogoDistritos_Subzonas> Ubicaciones_CatalogoDistritos_Subzonas { get; set; }
        public virtual ICollection<Ubicaciones_Distritos_Excepciones_Mixturas> Ubicaciones_Distritos_Excepciones_Mixturas { get; set; }
        public virtual ICollection<Ubicaciones_Distritos_Excepciones_RubrosCN> Ubicaciones_Distritos_Excepciones_RubrosCN { get; set; }
    }
}