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
    
    public partial class wsPagos
    {
        public wsPagos()
        {
            this.wsPagos_BoletaUnica = new HashSet<wsPagos_BoletaUnica>();
            this.SGI_Solicitudes_Pagos = new HashSet<SGI_Solicitudes_Pagos>();
            this.SSIT_Solicitudes_Pagos = new HashSet<SSIT_Solicitudes_Pagos>();
            this.Ley257_Solicitudes_Pagos = new HashSet<Ley257_Solicitudes_Pagos>();
            this.wsPagos_Conceptos = new HashSet<wsPagos_Conceptos>();
            this.wsPagos_BatchLog = new HashSet<wsPagos_BatchLog>();
            this.wsPagos_PagoElectronico = new HashSet<wsPagos_PagoElectronico>();
            this.wsPagos_PagoElectronico1 = new HashSet<wsPagos_PagoElectronico>();
        }
    
        public int id_pago { get; set; }
        public string TipoPersona { get; set; }
        public string TipoDoc { get; set; }
        public string ApellidoyNombre { get; set; }
        public string Documento { get; set; }
        public string Direccion { get; set; }
        public string Piso { get; set; }
        public string Depto { get; set; }
        public string Localidad { get; set; }
        public string CodPost { get; set; }
        public string Email { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
    
        public virtual ICollection<wsPagos_BoletaUnica> wsPagos_BoletaUnica { get; set; }
        public virtual ICollection<SGI_Solicitudes_Pagos> SGI_Solicitudes_Pagos { get; set; }
        public virtual ICollection<SSIT_Solicitudes_Pagos> SSIT_Solicitudes_Pagos { get; set; }
        public virtual ICollection<Ley257_Solicitudes_Pagos> Ley257_Solicitudes_Pagos { get; set; }
        public virtual ICollection<wsPagos_Conceptos> wsPagos_Conceptos { get; set; }
        public virtual ICollection<wsPagos_BatchLog> wsPagos_BatchLog { get; set; }
        public virtual ICollection<wsPagos_PagoElectronico> wsPagos_PagoElectronico { get; set; }
        public virtual ICollection<wsPagos_PagoElectronico> wsPagos_PagoElectronico1 { get; set; }
    }
}