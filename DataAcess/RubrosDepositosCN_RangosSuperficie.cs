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
    
    public partial class RubrosDepositosCN_RangosSuperficie
    {
        public int idRepositoRangoSup { get; set; }
        public int IdDeposito { get; set; }
        public int id_tipocircuito { get; set; }
        public string LetraAnexo { get; set; }
        public decimal DesdeM2 { get; set; }
        public decimal HastaM2 { get; set; }
    
        public virtual RubrosDepositosCN RubrosDepositosCN { get; set; }
    }
}
