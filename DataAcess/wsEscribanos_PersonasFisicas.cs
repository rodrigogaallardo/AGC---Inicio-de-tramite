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
    
    public partial class wsEscribanos_PersonasFisicas
    {
        public wsEscribanos_PersonasFisicas()
        {
            this.wsEscribanos_PersonasFisicas_Representantes = new HashSet<wsEscribanos_PersonasFisicas_Representantes>();
        }
    
        public int id_wsPersonasFisicas { get; set; }
        public int id_actanotarial { get; set; }
        public int id_personafisica { get; set; }
        public Nullable<System.DateTime> fecha_ultimo_pago_IIBB { get; set; }
        public Nullable<decimal> porcentaje_titularidad { get; set; }
        public Nullable<int> exento_IB { get; set; }
        public Nullable<int> reciente_insc_IB { get; set; }
        public Nullable<int> acredita_insc_IB { get; set; }
        public Nullable<int> acredita_pago_IB { get; set; }
        public Nullable<int> acredita_pago_aportes_IB { get; set; }
    
        public virtual ICollection<wsEscribanos_PersonasFisicas_Representantes> wsEscribanos_PersonasFisicas_Representantes { get; set; }
        public virtual wsEscribanos_ActaNotarial wsEscribanos_ActaNotarial { get; set; }
    }
}
