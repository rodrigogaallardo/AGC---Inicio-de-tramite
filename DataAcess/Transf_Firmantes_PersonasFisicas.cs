//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAcess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transf_Firmantes_PersonasFisicas
    {
        public int id_firmante_pf { get; set; }
        public int id_solicitud { get; set; }
        public int id_personafisica { get; set; }
        public string Apellido { get; set; }
        public string Nombres { get; set; }
        public int id_tipodoc_personal { get; set; }
        public string Nro_Documento { get; set; }
        public int id_tipocaracter { get; set; }
        public string Email { get; set; }
    
        public virtual TiposDeCaracterLegal TiposDeCaracterLegal { get; set; }
        public virtual Transf_Titulares_PersonasFisicas Transf_Titulares_PersonasFisicas { get; set; }
        public virtual TipoDocumentoPersonal TipoDocumentoPersonal { get; set; }
        public virtual Transf_Solicitudes Transf_Solicitudes { get; set; }
    }
}
