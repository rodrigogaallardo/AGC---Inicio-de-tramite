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
    
    public partial class DGFYCO_Ubicaciones_DatosAdicionales
    {
        public int id_dgubicacion_datosadicionales { get; set; }
        public int id_dgubicacion { get; set; }
        public bool edificio_publico { get; set; }
        public Nullable<int> id_asdestino_uso { get; set; }
        public Nullable<int> cantidad_pisos { get; set; }
        public bool propiedad_horizontal { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string mail { get; set; }
        public Nullable<int> nro_documento { get; set; }
        public Nullable<System.DateTime> fecha_designacion { get; set; }
        public Nullable<int> id_asmetodos_designacion { get; set; }
        public string cuit { get; set; }
        public string nombre_organismo { get; set; }
        public string apellido_organismo { get; set; }
        public string mail_organismo { get; set; }
        public Nullable<int> nro_documento_organismo { get; set; }
        public string cargo_organismo { get; set; }
        public string organismo { get; set; }
        public string dependencia { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.Guid> CreateUser { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public Nullable<System.Guid> LastUpdateUser { get; set; }
        public Nullable<bool> persona_fisica { get; set; }
        public string razon_social { get; set; }
    
        public virtual aspnet_Users aspnet_Users { get; set; }
        public virtual aspnet_Users aspnet_Users1 { get; set; }
        public virtual DGFYCO_Ubicaciones DGFYCO_Ubicaciones { get; set; }
    }
}