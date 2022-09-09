using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.EntityCustom
{
    public class TransferenciasPlantasEntity
    {         
        public int? IdTransferenciaTipoSector { get; set; }
        public int IdTipoSector { get; set; }
        public string Descripcion { get; set; }
        public bool Seleccionado { get; set; }
        public bool MuestraCampoAdicional { get; set; }
        public string Detalle { get; set; }
        public int TamanoCampoAdicional { get; set; }
        public bool? Ocultar { get; set; }
    }
}
