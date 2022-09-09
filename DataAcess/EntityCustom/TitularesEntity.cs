using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.EntityCustom
{
    public class TitularesEntity
    {
        public int IdPersona { get; set; }
        public string TipoPersona { get; set; }
        public string TipoPersonaDesc { get; set; }
        public string ApellidoNomRazon { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string RazonSocial { get; set; }
        public string CUIT { get; set; }
        public string Domicilio { get; set; }
        public string Piso { get; set; }
        public string Depto { get; set; }
        public string Email { get; set; }
        public string Codigo_Postal { get; set; }
        public string Localidad { get; set; }
        public int tipo_doc { get; set; }
        public string desc_tipo_doc { get; set; }
        public string nro_doc { get; set; }
    }

    public class TitularesSHEntity
    {
        public Guid RowId { get; set; }
        public int IdPersonaJuridica { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string TipoDoc { get; set; }
        public string NroDoc { get; set; }
        public int IdTipoDocPersonal { get; set; }
        public string Email { get; set; }
        public int IdFirmantePj { get; set; }
        public string Cuit { get; set; }
    }
}