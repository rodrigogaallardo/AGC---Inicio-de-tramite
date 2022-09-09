using System;
using System.Collections.Generic;

namespace DataTransferObject
{
	public class ConsultaPadronDatosLocalDTO
	{
		public int IdConsultaPadronDatosLocal { get; set; }
		public int IdConsultaPadron { get; set; }
		public decimal? SuperficieCubiertaDl { get; set; }
		public decimal? SuperficieDescubiertaDl { get; set; }
		public decimal? DimesionFrenteDl { get; set; }
		public bool LugarCargaDescargaDl { get; set; }
		public bool EstacionamientoDl { get; set; }
		public bool RedTransitoPesadoDl { get; set; }
		public bool SobreAvenidaDl { get; set; }
		public string MaterialesPisosDl { get; set; }
		public string MaterialesParedesDl { get; set; }
		public string MaterialesTechosDl { get; set; }
		public string MaterialesRevestimientosDl { get; set; }
		public int SanitariosUbicacionDl { get; set; }
		public decimal? SanitariosDistanciaDl { get; set; }
		public string CroquisUbicacionDl { get; set; }
		public int? CantidadSanitariosDl { get; set; }
		public decimal? SuperficieSanitariosDl { get; set; }
		public decimal? FrenteDl { get; set; }
		public decimal? FondoDl { get; set; }
		public decimal? LateralIzquierdoDl { get; set; }
		public decimal? LateralDerechoDl { get; set; }
		public int? CantidadOperariosDl { get; set; }
		public DateTime CreateDate { get; set; }
		public Guid CreateUser { get; set; }
		public DateTime? LastUpdateDate { get; set; }
		public Guid? LastUpdateUser { get; set; }
        public decimal? Local_venta { get; set; }
        public bool Dj_certificado_sobrecarga { get; set; }
    }				
}


