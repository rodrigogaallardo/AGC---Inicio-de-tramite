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
    
    public partial class Envio_Mail_Proceso
    {
        public int id_proceso { get; set; }
        public string descripcion { get; set; }
        public string cfg_mail_from { get; set; }
        public string cfg_smtp { get; set; }
        public Nullable<int> cfg_smpt_puerto { get; set; }
        public string cfg_html { get; set; }
    }
}
