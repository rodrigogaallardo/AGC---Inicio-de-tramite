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
    
    public partial class Emails_Origenes
    {
        public int id_origen { get; set; }
        public string descripcion { get; set; }
        public string cfg_mail_from { get; set; }
        public int id_email_template { get; set; }
        public string cfg_smtp { get; set; }
        public Nullable<int> cfg_smpt_puerto { get; set; }
    }
}
