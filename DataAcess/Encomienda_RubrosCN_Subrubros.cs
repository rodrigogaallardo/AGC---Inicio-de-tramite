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
    
    public partial class Encomienda_RubrosCN_Subrubros
    {
        public int Id_EncRubCNSubrubros { get; set; }
        public int Id_EncRubro { get; set; }
        public int Id_rubrosubrubro { get; set; }
    
        public virtual Encomienda_RubrosCN Encomienda_RubrosCN { get; set; }
        public virtual RubrosCN_Subrubros RubrosCN_Subrubros { get; set; }
    }
}
