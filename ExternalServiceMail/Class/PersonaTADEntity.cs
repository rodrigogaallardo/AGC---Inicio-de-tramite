using Newtonsoft.Json;

namespace ExternalService.Class
{
    public partial class PersonaTadEntity
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("cuit")]
        public string Cuit { get; set; }

        [JsonProperty("apoderados")]
        public Apoderado[] Apoderados { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("razonSocial")]
        public string RazonSocial { get; set; }

        [JsonProperty("terminosCondiciones")]
        public TerminosCondiciones TerminosCondiciones { get; set; }

        [JsonProperty("telefono")]
        public string Telefono { get; set; }

        [JsonProperty("titulares")]
        public object[] Titulares { get; set; }

        [JsonProperty("domicilioConstituido")]
        public DomicilioConstituido DomicilioConstituido { get; set; }

        [JsonProperty("tipoDocumentoIdentidad")]
        public string TipoDocumentoIdentidad { get; set; }

        [JsonProperty("documentoIdentidad")]
        public string DocumentoIdentidad { get; set; }

        [JsonProperty("apellido1")]
        public string Apellido1 { get; set; }

        [JsonProperty("apellido2")]
        public string Apellido2 { get; set; }

        [JsonProperty("apellido3")]
        public string Apellido3 { get; set; }

        [JsonProperty("sexo")]
        public string Sexo { get; set; }

        [JsonProperty("nombre2")]
        public string Nombre2 { get; set; }

        [JsonProperty("nombre1")]
        public string Nombre1 { get; set; }

        [JsonProperty("nombre3")]
        public string Nombre3 { get; set; }

        [JsonProperty("nombreApellido")]
        public string NombreApellido { get; set; }
    }

    public partial class Apoderado
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("habilitacionesAGC")]
        public bool HabilitacionesAgc { get; set; }

        [JsonProperty("estado")]
        public bool Estado { get; set; }

        [JsonProperty("tipoRepresentacion")]
        public string TipoRepresentacion { get; set; }

        [JsonProperty("fechaVencimiento")]
        public string FechaVencimiento { get; set; }

        [JsonProperty("tieneVista360")]
        public bool TieneVista360 { get; set; }

        [JsonProperty("idApoderado")]
        public long IdApoderado { get; set; }

        [JsonProperty("idTitular")]
        public long IdTitular { get; set; }

        [JsonProperty("fechaAlta")]
        public string FechaAlta { get; set; }

        [JsonProperty("usuarioCreacion", NullValueHandling = NullValueHandling.Ignore)]
        public string UsuarioCreacion { get; set; }

        [JsonProperty("usuarioModificacion", NullValueHandling = NullValueHandling.Ignore)]
        public string UsuarioModificacion { get; set; }

        [JsonProperty("fechaModificacion", NullValueHandling = NullValueHandling.Ignore)]
        public string FechaModificacion { get; set; }
    }

    public partial class DomicilioConstituido
    {
        [JsonProperty("codPostal")]
        public string CodPostal { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("direccion")]
        public string Direccion { get; set; }

        [JsonProperty("telefono")]
        public string Telefono { get; set; }

        [JsonProperty("altura")]
        public string Altura { get; set; }

        [JsonProperty("piso")]
        public string Piso { get; set; }
    }

    public partial class TerminosCondiciones
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("estado")]
        public string Estado { get; set; }

        [JsonProperty("codigoContenido")]
        public string CodigoContenido { get; set; }
    }
}
