using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class EncomiendaDatosLocalDTO
    {
        public int id_encomiendadatoslocal { get; set; }
        public int id_encomienda { get; set; }
        public decimal? superficie_cubierta_dl { get; set; }
        public decimal? superficie_descubierta_dl { get; set; }
        //public decimal? superficie_semicubierta_dl { get; set; }
        public decimal? dimesion_frente_dl { get; set; }
        public bool lugar_carga_descarga_dl { get; set; }
        public bool estacionamiento_dl { get; set; }
        public bool red_transito_pesado_dl { get; set; }
        public bool sobre_avenida_dl { get; set; }
        public string materiales_pisos_dl { get; set; }
        public string materiales_paredes_dl { get; set; }
        public string materiales_techos_dl { get; set; }
        public string materiales_revestimientos_dl { get; set; }
        public int? sanitarios_ubicacion_dl { get; set; }
        public decimal? sanitarios_distancia_dl { get; set; }
        public string croquis_ubicacion_dl { get; set; }
        public int? cantidad_sanitarios_dl { get; set; }
        public decimal? superficie_sanitarios_dl { get; set; }
        public decimal? frente_dl { get; set; }
        public decimal? fondo_dl { get; set; }
        public decimal? lateral_izquierdo_dl { get; set; }
        public decimal? lateral_derecho_dl { get; set; }
        public bool? sobrecarga_corresponde_dl { get; set; }
        public int? sobrecarga_tipo_observacion { get; set; }
        public int? sobrecarga_requisitos_opcion { get; set; }
        public string sobrecarga_art813_inciso { get; set; }
        public string sobrecarga_art813_item { get; set; }
        public int? cantidad_operarios_dl { get; set; }
        public DateTime? CreateDate { get; set; }
        public Guid? CreateUser { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public Guid LastUpdateUser { get; set; }
        public double? local_venta { get; set; }
        public bool? cumple_ley_962 { get; set; }
        public bool? salubridad_especial { get; set; }
        public bool? eximido_ley_962 { get; set; }
        public bool? ampliacion_superficie { get; set; }
        public decimal? superficie_cubierta_amp { get; set; }
        public decimal? superficie_descubierta_amp { get; set; }
        public bool? dj_certificado_sobrecarga { get; set; }
        public bool estacionamientoBicicleta_dl { get; set; }

        public virtual ICollection<EncomiendaCertificadoSobrecargaDTO> EncomiendaCertificadoSobrecargaDTO { get; set; }

    }
}
