using DataAcess.EntityCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class RubrosDepositosCNDTO 
    {
        public int IdDeposito { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int IdCategoriaDeposito { get; set; }
        public string GradoMolestia { get; set; }
        public string ZonaMixtura1 { get; set; }
        public string ZonaMixtura2 { get; set; }
        public string ZonaMixtura3 { get; set; }
        public string ZonaMixtura4 { get; set; }
        public string ObservacionesCategorizacion { get; set; }
        public DateTime? VigenciaDesde { get; set; }
        public DateTime? VigenciaHasta { get; set; }
        public virtual ICollection<RubrosDepositosCN_RangosSuperficieDTO> RubrosDepositosCN_RangosSuperficie { get; set; }
    }

    public class clsItemGrillaSeleccionarDepositos
    {
        public string Categoria { get; set; }
        public int IdDeposito { get; set; }
        public int IdCategoriaDeposito { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string GradoMolestia { get; set; }
        public string CondicionZonaMixtura1 { get; set; }
        public string CondicionZonaMixtura2 { get; set; }
        public string CondicionZonaMixtura3 { get; set; }
        public string CondicionZonaMixtura4 { get; set; }
        public string ZonasMixtura { get; set; }
        public string CondicionZonaMixtura { get; set; }
        public string Icono { get; set; }
        public bool Resultado { get; set; }
        public bool TieneNormativa { get; set; }
        public string ObservacionesCategorizacion { get; set; }
        //public string CodigoTipoCertificado { get; set; }
        public string mensaje { get; set; }
        public ICollection<itemResultadoEvaluacionCondiciones> Resultadoscondiciones { get; set; }
    }

}
