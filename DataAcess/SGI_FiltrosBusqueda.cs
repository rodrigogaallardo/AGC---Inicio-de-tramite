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
    
    public partial class SGI_FiltrosBusqueda
    {
        public System.Guid Id_Busqueda { get; set; }
        public string Filtros { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.Guid> CreateUser { get; set; }
        public string botonAccion { get; set; }
    
        public virtual aspnet_Users aspnet_Users { get; set; }
    }
}
