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
    
    public partial class SSIT_Solicitudes_bk
    {
        public int id_solicitud { get; set; }
        public int id_encomienda { get; set; }
        public int id_tipotramite { get; set; }
        public int id_tipoexpediente { get; set; }
        public int id_subtipoexpediente { get; set; }
        public Nullable<int> MatriculaEscribano { get; set; }
        public string NroExpediente { get; set; }
        public int id_estado { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.Guid CreateUser { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public Nullable<System.Guid> LastUpdateUser { get; set; }
        public string NroExpedienteSade { get; set; }
        public string telefono { get; set; }
        public Nullable<System.DateTime> FechaLibrado { get; set; }
    }
}
