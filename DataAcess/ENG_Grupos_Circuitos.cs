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
    
    public partial class ENG_Grupos_Circuitos
    {
        public ENG_Grupos_Circuitos()
        {
            this.ENG_Rel_Circuitos_TiposDeTramite = new HashSet<ENG_Rel_Circuitos_TiposDeTramite>();
            this.Rubros = new HashSet<Rubros>();
            this.ENG_Grupos_Circuitos_Tipo_Tramite = new HashSet<ENG_Grupos_Circuitos_Tipo_Tramite>();
            this.RubrosCN_Subrubros = new HashSet<RubrosCN_Subrubros>();
            this.ENG_Circuitos = new HashSet<ENG_Circuitos>();
            this.RubrosCN = new HashSet<RubrosCN>();
        }
    
        public int id_grupo_circuito { get; set; }
        public string cod_grupo_circuito { get; set; }
        public string nom_grupo_circuito { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<int> id_tipoexpediente { get; set; }
        public Nullable<int> id_subtipoexpediente { get; set; }
        public Nullable<int> prioridad { get; set; }
    
        public virtual ICollection<ENG_Rel_Circuitos_TiposDeTramite> ENG_Rel_Circuitos_TiposDeTramite { get; set; }
        public virtual ICollection<Rubros> Rubros { get; set; }
        public virtual ICollection<ENG_Grupos_Circuitos_Tipo_Tramite> ENG_Grupos_Circuitos_Tipo_Tramite { get; set; }
        public virtual ICollection<RubrosCN_Subrubros> RubrosCN_Subrubros { get; set; }
        public virtual SubtipoExpediente SubtipoExpediente { get; set; }
        public virtual TipoExpediente TipoExpediente { get; set; }
        public virtual ICollection<ENG_Circuitos> ENG_Circuitos { get; set; }
        public virtual ICollection<RubrosCN> RubrosCN { get; set; }
    }
}
