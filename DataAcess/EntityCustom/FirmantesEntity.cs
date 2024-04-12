using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.EntityCustom
{
    public class FirmantesEntity
    {
        public string TipoPersona { get; set; }
        public int IdFirmante { get; set; }
        public string Titular { get; set; }
        public string ApellidoNombres { get; set; }
        public string DescTipoDocPersonal { get; set; }
        public string NroDocumento { get; set; }
        public string NomTipoCaracter { get; set; }
        public string CargoFirmante { get; set; }
        public string Email { get; set; }
        public string Cuit { get; set; }
    }

    public class FirmantesPJEntity
    {
        public int IdPersonaJuridica { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string TipoDoc { get; set; }
        public string NroDoc { get; set; }
        public string NomTipoCaracter { get; set; }
        public int IdTipoDocPersonal { get; set; }
        public int IdTipoCaracter { get; set; }
        public string CargoFirmantePj { get; set; }
        public string Email { get; set; }
        public bool? FirmanteMismaPersona { get; set; }
        public string Cuit { get; set; }
    }

}
