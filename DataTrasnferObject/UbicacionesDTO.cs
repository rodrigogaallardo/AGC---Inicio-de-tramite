using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class UbicacionesDTO
    {
        public int IdUbicacion { get; set; }
        public int? IdSubtipoUbicacion { get; set; }
        public int? NroPartidaMatriz { get; set; }
        public int? Circunscripcion { get; set; }
        public int? Seccion { get; set; }
        public string Manzana { get; set; }
        public string Parcela { get; set; }
        public decimal? SuperficieTotal { get; set; }
        public decimal? Fondo { get; set; }
        public decimal? Frente { get; set; }
        public int IdZonaPlaneamiento { get; set; }
        public string Observaciones { get; set; }
        public string CoordenadaX { get; set; }
        public string CoordenadaY { get; set; }
        public DateTime VigenciaDesde { get; set; }
        public DateTime? VigenciaHasta { get; set; }
        public DateTime? CreateDate { get; set; }
        public Guid? CreateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Guid? UpdateUser { get; set; }
        public string InhibidaObservacion { get; set; }
        public bool BajaLogica { get; set; }
        public decimal? SuperficieCubierta { get; set; }
        public int? PisosBajoRasante { get; set; }
        public int? PisosSobreRasante { get; set; }
        public int? Unidades { get; set; }
        public int? Locales { get; set; }
        public int? CantPh { get; set; }
        public decimal? Vuc { get; set; }
        public int? IdComuna { get; set; }
        public int? IdBarrio { get; set; }
        public int? IdAreaHospitalaria { get; set; }
        public int? IdComisaria { get; set; }
        public int? IdRegionSanitaria { get; set; }
        public int? IdDistritoEscolar { get; set; }
        public DateTime? FechaUltimaActualizacionUsig { get; set; }
        public int? CantiActualizacionesUsig { get; set; }
        public string ResultadoActualizacionUsig { get; set; }
        public string TipoPersonaTitularAgip { get; set; }
        public string TitularAgip { get; set; }
        public DateTime? FechaAltaAgip { get; set; }
        public bool EsEntidadGubernamental { get; set; }
        public bool EsUbicacionProtegida { get; set; }

        public ZonasPlaneamientoDTO ZonasPlaneamiento { get; set; }
        public ICollection<UbicacionesZonasMixturasDTO> UbicacionesZonasMixturasDTO { get; set; }
        public ICollection<UbicacionesDistritosDTO> UbicacionesDistritosDTO { get; set; }
        public ItemDirectionDTO Direccion { get; set;  }

        public ICollection<UbicacionesPuertasDTO> UbicacionesPuertas { get; set; }
    }
}
