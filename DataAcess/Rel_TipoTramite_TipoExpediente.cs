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
    
    public partial class Rel_TipoTramite_TipoExpediente
    {
        public int id_rel_TipoTramite_TipoExpediente { get; set; }
        public int id_tipotramite { get; set; }
        public int id_tipoexpediente { get; set; }
    
        public virtual TipoExpediente TipoExpediente { get; set; }
        public virtual TipoTramite TipoTramite { get; set; }
    }
}
