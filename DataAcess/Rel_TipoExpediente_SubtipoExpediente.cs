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
    
    public partial class Rel_TipoExpediente_SubtipoExpediente
    {
        public int id_tipo_subtipo { get; set; }
        public Nullable<int> id_tipoexpediente { get; set; }
        public Nullable<int> id_subtipoexpediente { get; set; }
    
        public virtual SubtipoExpediente SubtipoExpediente { get; set; }
        public virtual TipoExpediente TipoExpediente { get; set; }
    }
}
