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
    
    public partial class wsEscribanos_ActaNotarial
    {
        public wsEscribanos_ActaNotarial()
        {
            this.wsEscribanos_DerechoOcupacion = new HashSet<wsEscribanos_DerechoOcupacion>();
            this.wsEscribanos_PersonasFisicas = new HashSet<wsEscribanos_PersonasFisicas>();
            this.wsEscribanos_PersonasJuridicas = new HashSet<wsEscribanos_PersonasJuridicas>();
        }
    
        public int id_actanotarial { get; set; }
        public int id_encomienda { get; set; }
        public Nullable<int> id_tipo_escritura { get; set; }
        public int nro_matricula_escribano_acta { get; set; }
        public Nullable<int> id_actuacion_notarial_acta { get; set; }
        public Nullable<int> nro_escritura_acta { get; set; }
        public System.DateTime fecha_escritura_acta { get; set; }
        public string registro_acta { get; set; }
        public bool local_afectado_ley13512 { get; set; }
        public bool reglamento_admite_actividad_ley13512 { get; set; }
        public Nullable<System.DateTime> fecha_asamblea_ley13512 { get; set; }
        public Nullable<System.DateTime> fecha_reglamento_ley13512 { get; set; }
        public Nullable<int> nro_escritura_ley13512 { get; set; }
        public Nullable<int> nro_matricula_escribano_ley13512 { get; set; }
        public string registro_ley13512 { get; set; }
        public string jurisdiccion_ley13512 { get; set; }
        public Nullable<System.DateTime> fecha_inscrip_reglamento_ley13512 { get; set; }
        public Nullable<int> nro_matricula_regprop_ley13512 { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public bool anulada { get; set; }
        public Nullable<System.DateTime> LastUpdateDate { get; set; }
        public string LastUpdateUser { get; set; }
        public Nullable<int> id_file { get; set; }
        public Nullable<int> conformidad_copropietario { get; set; }
    
        public virtual Escribano Escribano { get; set; }
        public virtual ICollection<wsEscribanos_DerechoOcupacion> wsEscribanos_DerechoOcupacion { get; set; }
        public virtual ICollection<wsEscribanos_PersonasFisicas> wsEscribanos_PersonasFisicas { get; set; }
        public virtual ICollection<wsEscribanos_PersonasJuridicas> wsEscribanos_PersonasJuridicas { get; set; }
        public virtual Encomienda Encomienda { get; set; }
        public virtual wsEscribanos_Tipos_Escrituras wsEscribanos_Tipos_Escrituras { get; set; }
    }
}
