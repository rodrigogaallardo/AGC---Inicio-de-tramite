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
    
    public partial class Rubros_Historial_Cambios_Estados
    {
        public int id_rubhistcamest { get; set; }
        public int id_rubhistcam { get; set; }
        public int id_estado_ant { get; set; }
        public int id_estado_nuevo { get; set; }
        public System.DateTime fecha_modificacion { get; set; }
        public System.Guid usuario_modificacion { get; set; }
    
        public virtual Rubros_Historial_Cambios Rubros_Historial_Cambios { get; set; }
    }
}