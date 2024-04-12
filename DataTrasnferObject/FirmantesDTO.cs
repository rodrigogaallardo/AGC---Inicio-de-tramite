using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class FirmantesDTO
    {
        public string TipoPersona { get; set; }
        public int id_firmante { get; set; }
        public string Titular { get; set; }
        public string ApellidoNombres { get; set; }
        public string DescTipoDocPersonal { get; set; }
        public string Nro_Documento { get; set; }
        public string nom_tipocaracter { get; set; }
        public string cargo_firmante_pj { get; set; }
        public string Email { get; set; }
        public string Cuit { get; set; }
    }

    public class FirmantesPJDTO
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
    public class FirmantesSHDTO
    {
        public string FirmanteDe { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string TipoDoc { get; set; }
        public string NroDoc { get; set; }
        public string nom_tipocaracter { get; set; }
        public int id_tipodoc_personal { get; set; }
        public int id_tipocaracter { get; set; }
        public string cargo_firmante { get; set; }
        public string email { get; set; }
        public Guid rowid { get; set; }
        public Guid rowid_titular { get; set; }
        public bool? misma_persona { get; set; }
        public string Cuit { get; set; }
    }

}
