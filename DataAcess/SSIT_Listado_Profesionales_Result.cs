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
    
    public partial class SSIT_Listado_Profesionales_Result
    {
        public string Apellido { get; set; }
        public string nombre { get; set; }
        public Nullable<int> total { get; set; }
        public Nullable<int> Aprobadas { get; set; }
        public Nullable<decimal> porcentaje_aprob { get; set; }
        public Nullable<int> Rechazadas { get; set; }
        public Nullable<decimal> porcentaje_recha { get; set; }
        public Nullable<int> Vencidas { get; set; }
        public Nullable<decimal> porcentaje_venci { get; set; }
        public Nullable<int> idcircuito { get; set; }
        public int id_profesional { get; set; }
        public string Consejo { get; set; }
    }
}