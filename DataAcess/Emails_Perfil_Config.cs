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
    
    public partial class Emails_Perfil_Config
    {
        public Emails_Perfil_Config()
        {
            this.Emails_Perfil_Relacion_Config = new HashSet<Emails_Perfil_Relacion_Config>();
        }
    
        public int id_profile { get; set; }
        public string profile_name { get; set; }
        public bool is_default { get; set; }
        public string display_name { get; set; }
        public string email_address { get; set; }
        public string smtp { get; set; }
        public int port { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    
        public virtual ICollection<Emails_Perfil_Relacion_Config> Emails_Perfil_Relacion_Config { get; set; }
    }
}
