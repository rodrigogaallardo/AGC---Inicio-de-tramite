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
    
    public partial class CPadron_Solicitudes_AvisoCaducidad
    {
        public int id_aviso { get; set; }
        public int id_cpadron { get; set; }
        public int id_email { get; set; }
        public System.DateTime CreateDate { get; set; }
    
        public virtual CPadron_Solicitudes CPadron_Solicitudes { get; set; }
        public virtual Emails Emails { get; set; }
    }
}
