using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalService.Class
{
    public class TipoDocumento
    {
        public int id { get; set; }
        public string acronimoGedo { get; set; }
        public string acronimoTAD { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string formularioControlado { get; set; }
        public string tipoProduccion { get; set; }
        public string usuarioIniciador { get; set; }
        public string usuarioCreacion { get; set; }
        public long fechaAlta { get; set; }
        public string usuarioModificacion { get; set; }
        public long fechaModificacion { get; set; }
        public bool esEmbebido { get; set; }
        public bool firmaConToken { get; set; }
        public string ip { get; set; }
        public bool esFirmaConjunta { get; set; }
        public string documentoTipoFirma { get; set; }
        public string textoLibreLimite { get; set; }
        public bool textoLibreEnriquecido { get; set; }
        public bool embebidoOpcional { get; set; }
        public bool esFirmaCloud { get; set; }
    }

    public class NivelAcceso
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int nivelAcceso { get; set; }
        public string proveedor { get; set; }
        public string authorizationEndPoint { get; set; }
        public string endSessionEndPoint { get; set; }
        public string loginComponent { get; set; }
        public bool habilitarApoderamiento { get; set; }
    }

    public class TerminosYCondiciones
    {
        public int id { get; set; }
        public TipoDocumento tipoDocumento { get; set; }
        public string estado { get; set; }
        public long fechaAlta { get; set; }
        public string contenido { get; set; }
        public NivelAcceso nivelAcceso { get; set; }
    }

    public class SistemaConsumidor
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public long fechaCreacion { get; set; }
        public bool visible { get; set; }
    }

    public class Persona
    {
        public int id { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string razonSocial { get; set; }
        public string cuit { get; set; }
        public string tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string sexo { get; set; }
        public string codigoPais { get; set; }
        public string codigoTelefonoPais { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public int usuarioCreacion { get; set; }
        public long fechaAlta { get; set; }
        public string usuarioModificacion { get; set; }
        public long fechaModificacion { get; set; }
        public string tipoPersona { get; set; }
        public int valiRenaper { get; set; }
        public TerminosYCondiciones terminosYCondiciones { get; set; }
        public SistemaConsumidor sistemaConsumidor { get; set; }
        public string baId { get; set; }
        public bool habilitadaVista360 { get; set; }
    }
    public class Provincia
    {
        public int id { get; set; }
        public string nombre { get; set; }
    }
    public class Departamento
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public Provincia provincia { get; set; }
        public int orden { get; set; }
    }
    public class Localidad
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public Departamento departamento { get; set; }
        public Provincia provincia { get; set; }
    }

    public class Pais
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
    }
    public class PersonaLogin
    {
        public int id { get; set; }
        public Persona persona { get; set; }
        public string calle { get; set; }
        public string altura { get; set; }
        public string piso { get; set; }
        public string depto { get; set; }
        public string codigoPostal { get; set; }
        public string telefono { get; set; }
        public string observaciones { get; set; }
        public Provincia provincia { get; set; }
        public Localidad localidad { get; set; }
        public Departamento departamento { get; set; }
        public Pais pais { get; set; }
    }

    public class ApoderadoTad
    {
        public int id { get; set; }
        public Persona persona { get; set; }
        public string calle { get; set; }
        public string altura { get; set; }
        public string piso { get; set; }
        public string depto { get; set; }
        public string codigoPostal { get; set; }
        public string telefono { get; set; }
        public string observaciones { get; set; }
    }

    public class TokenJwtTad
    {
        public PersonaLogin personaLogin { get; set; }
        public ApoderadoTad apoderado { get; set; }
        public int idTad { get; set; }
        public int tipoTramite { get; set; }
        public IList<object> poderdantes { get; set; }
    }




}
