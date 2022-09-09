using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.EntityCustom
{
    public class SobrecargasEntity
    {
        public int id_sobrecarga_detalle1 { get; set; }
        public int id_tipo_destino { get; set; }
        public string desc_tipo_destino { get; set; }
        public int id_tipo_uso { get; set; }
        public string desc_tipo_uso { get; set; }
        public decimal valor { get; set; }
        public string detalle { get; set; }
        public int id_planta { get; set; }
        public string desc_planta { get; set; }
        public string losa_sobre { get; set; }
        public int id_tipo_uso_1 { get; set; }
        public string desc_tipo_uso_1 { get; set; }
        public decimal valor_1 { get; set; }
        public int id_tipo_uso_2 { get; set; }
        public string desc_tipo_uso_2 { get; set; }
        public decimal valor_2 { get; set; }
        public string texto_carga_uso { get; set; }
        public string texto_uso_1 { get; set; }
        public string texto_uso_2 { get; set; }
    }
}
