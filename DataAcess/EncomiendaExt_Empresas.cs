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
    
    public partial class EncomiendaExt_Empresas
    {
        public int id_encomiendaext_empresa { get; set; }
        public int id_encomienda { get; set; }
        public string TipoEmpresa { get; set; }
        public string RazonSocial { get; set; }
        public string Cuit { get; set; }
        public string Calle { get; set; }
        public Nullable<int> NroPuerta { get; set; }
        public string Piso { get; set; }
        public string Depto { get; set; }
        public string Localidad { get; set; }
        public string CodigoPostal { get; set; }
    
        public virtual EncomiendaExt EncomiendaExt { get; set; }
    }
}
