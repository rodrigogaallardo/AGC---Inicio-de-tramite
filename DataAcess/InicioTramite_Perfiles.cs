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
    
    public partial class InicioTramite_Perfiles
    {
        public InicioTramite_Perfiles()
        {
            this.InicioTramite_Menues = new HashSet<InicioTramite_Menues>();
            this.aspnet_Users = new HashSet<aspnet_Users>();
        }
    
        public int id_perfil { get; set; }
        public string nombre_perfil { get; set; }
        public string descripcion_perfil { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.Guid> CreateUser { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public Nullable<System.Guid> LastUpdateUser { get; set; }
    
        public virtual ICollection<InicioTramite_Menues> InicioTramite_Menues { get; set; }
        public virtual ICollection<aspnet_Users> aspnet_Users { get; set; }
        public virtual aspnet_Users aspnet_Users1 { get; set; }
        public virtual aspnet_Users aspnet_Users11 { get; set; }
    }
}
