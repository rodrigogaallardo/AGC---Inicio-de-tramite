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
    
    public partial class Usuario
    {
        public System.Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string Calle { get; set; }
        public Nullable<int> NroPuerta { get; set; }
        public string Piso { get; set; }
        public string Depto { get; set; }
        public string CodPostal { get; set; }
        public Nullable<int> IdLocalidad { get; set; }
        public Nullable<int> IdProvincia { get; set; }
        public string Movil { get; set; }
        public string TelefonoArea { get; set; }
        public string TelefonoPrefijo { get; set; }
        public string TelefonoSufijo { get; set; }
        public string Sms { get; set; }
        public Nullable<int> UserDni { get; set; }
        public string UserDetalleCaracter { get; set; }
        public int TipoPersona { get; set; }
        public string RazonSocial { get; set; }
        public Nullable<long> CUIT { get; set; }
        public string Telefono { get; set; }
        public bool AceptarTerminos { get; set; }
        public string Token { get; set; }
        public string SignU { get; set; }
    
        public virtual aspnet_Users aspnet_Users { get; set; }
    }
}
