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
    
    public partial class CAA_Rel_CAA_DocAdjuntos
    {
        public int id_caadocadjunto { get; set; }
        public int id_encomienda { get; set; }
        public int id_docadjunto { get; set; }
        public Nullable<int> id_solicitud { get; set; }
    
        public virtual Encomienda Encomienda { get; set; }
    }
}
