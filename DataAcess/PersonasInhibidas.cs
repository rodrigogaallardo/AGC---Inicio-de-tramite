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
    
    public partial class PersonasInhibidas
    {
        public int id_personainhibida { get; set; }
        public Nullable<int> id_tipodoc_personal { get; set; }
        public Nullable<int> nrodoc_personainhibida { get; set; }
        public int nroorden_personainhibida { get; set; }
        public string cuit_personainhibida { get; set; }
        public string nomape_personainhibida { get; set; }
        public Nullable<System.DateTime> fecharegistro_personainhibida { get; set; }
        public Nullable<System.DateTime> fechavencimiento_personainhibida { get; set; }
        public string autos_personainhibida { get; set; }
        public string juzgado_personainhibida { get; set; }
        public string secretaria_personainhibida { get; set; }
        public int estado_personainhibida { get; set; }
        public Nullable<System.DateTime> fechabaja_personainhibida { get; set; }
        public Nullable<int> operador_personainhibida { get; set; }
        public string observaciones_personainhibida { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public string LastUpdateUser { get; set; }
        public string MotivoLevantamiento { get; set; }
        public Nullable<int> id_tipopersona { get; set; }
    
        public virtual TipoDocumentoPersonal TipoDocumentoPersonal { get; set; }
        public virtual TipoPersona TipoPersona { get; set; }
    }
}
