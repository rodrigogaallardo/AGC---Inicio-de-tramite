using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalService
{

    public enum BUTipoPersona
    {
        Fisica,
        Juridica
    }
    public enum BUTipodocumento
    {
        DNI,
        CUIT,
        LC,
        CI,
        LE,
        PAS
    }

    public class BUBoletaUnica
    {
        public int IdBoletaUnica { get; set; }
        public int IdPago { get; set; }
        public string CodBarras { get; set; }
        public long NroBoletaUnica { get; set; }
        public int Dependencia { get; set; }
        public BUDatosContribuyente Contribuyente { get; set; }
        public decimal MontoTotal { get; set; }
        public int EstadoId { get; set; }
        public string EstadoNombre { get; set; }
        public DateTime? FechaPago { get; set; }
        public DateTime? FechaAnulada { get; set; }
        public DateTime? FechaCancelada { get; set; }
        public string TrazaPago { get; set; }
        public string CodigoVerificador { get; set; }
        public string NroBUI { get; set; }
        public Guid? BUI_ID { get; set; }

        public string MedioDePagoRectificado { get; set; }
        public string NumeroComprobante { get; set; }
        public string NumeroVoucher { get; set; }
        public string CantidadCuotas { get; set; }
        public string CodigoAutorizacion { get; set; }
        public string LugarDePago { get; set; }
        public List<BUIEstados> Estados { get; set; }
        public DateTime? FechaPago2 { get; set; }

    }
    public class BUIEstados
    {
        public string Estado { get; set; }
        public DateTime? ActualizadoFecha { get; set; }
    }


        public class BUDatosContribuyente
    {
        public BUTipoPersona TipoPersona { get; set; }
        public string ApellidoyNombre { get; set; }
        public Nullable<BUTipodocumento> TipoDoc { get; set; }
        public string Documento { get; set; }
        public string Direccion { get; set; }
        public string Piso { get; set; }
        public string Departamento { get; set; }
        public string Localidad { get; set; }
        public string CodPost { get; set; }
        public string Email { get; set; }
        public string TipoDocumentoValue { get; set; }
        public string TipoPersonaValue { get; set; }
    }

    public class BUConcepto
    {
        public int idPagoConcepto { get; set; }
        public int IdPago { get; set; }
        public int CodConcepto1 { get; set; }
        public int CodConcepto2 { get; set; }
        public int CodConcepto3 { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Importe { get; set; }
        public decimal ValorDetalle { get; set; }
        public bool? AdmiteReglas { get; set; }
        public Guid ItemID { get; set; }
    }

    public class BUIDependencia
    {
        public Guid ID { get; set; }
        public string Nombre { get; set; }
        public List<string> Items { get; set; }
    }

    public class BUIDatosContribuyente
    {
        public string TipoPersona { get; set; }
        public BUITipoDeDocumento TipoDocumento { get; set; }
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public string Direccion { get; set; }
        public string Piso { get; set; }
        public string Departamento { get; set; }
        public string Localidad { get; set; }
        public string CodigoPostal { get; set; }
        public string Email { get; set; }
    }

    public class BUIConceptoConfig
    {
        public bool AdmiteReglas { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public List<BUIDetalleConcepto> Detalles { get; set; }
        public Guid ID { get; set; }
        public bool TieneCantidadFija { get; set; }
        public bool TieneValorFijo { get; set; }
        public decimal Valor { get; set; }
        public int Vigencia { get; set; }
    }

    public class BUIDetalleConcepto
    {
        public string Descripcion { get; set; }
        public Guid ID { get; set; }
        public Guid ItemID { get; set; }
        public string Nombre { get; set; }
        public decimal Valor { get; set; }

    }

    public class BUITipoDeDocumento
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Formato { get; set; }
        public Guid ID { get; set; }
        public string Regex { get; set; }
    }

    public class BUIConcepto
    {
        public Guid ID { get; set; }
        public Guid ItemID { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Importe { get; set; }
        public int Vigencia { get; set; }
        public decimal Total { get; set; }
        public List<BUIDetalleConcepto> Detalles { get; set; }
    }

    public class Detalles
    {
        public double Valor { get; set; }
    }

    public class RequestImporte
    {
        public Guid ItemID { get; set; }
        public List<Detalles> Detalles { get; set; }
    }

    public class BUICalculoImporte
    {
        public int CodigoConcepto1 { get; set; }
        public int CodigoConcepto2 { get; set; }
        public int CodigoConcepto3 { get; set; }
        public double? Valor { get; set; }
    }

    public class RequestBUI
    {
        public Guid DependenciaID { get; set; }
        public BUIDatosContribuyente Contribuyente { get; set; }
        public List<BUIConcepto> Conceptos { get; set; }
    }

    public class ResponseBUI
    {
        public Guid ID { get; set; }
        public string Numero { get; set; }
        public Guid DependenciaID { get; set; }
        public BUIDependencia Dependencia { get; set; }
        public Nullable<Guid> ContribuyenteID { get; set; }
        public BUIDatosContribuyente Contribuyente { get; set; }
        public string Codigo { get; set; }
        public string Fecha { get; set; }
        public List<BUIConcepto> Conceptos { get; set; }
        public Guid UsuarioID { get; set; }
        public string Usuario { get; set; }
        public int UsuarioLegajo { get; set; }
        public string UsuarioNombre { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public string QR { get; set; }
        public string Barcode { get; set; }
        public string Pagado { get; set; }
        public string Anulado { get; set; }
        public string Cancelado { get; set; }
        public string Traza { get; set; }
    }

    public class BUDatosBoleta
    {
        public BUDatosContribuyente datosConstribuyente { get; set; }
        public List<BUConcepto> listaConcepto { get; set; }
    }
}
