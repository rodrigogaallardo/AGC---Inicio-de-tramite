namespace DataTransferObject
{
    public partial class PersonaTadDTO
    {
        public long Id { get; set; }

        public string Cuit { get; set; }

        public string Email { get; set; }

        public string RazonSocial { get; set; }

        public string Telefono { get; set; }

        public object[] Titulares { get; set; }

        public DomicilioConstituidoDTO DomicilioConstituido { get; set; }

        public string TipoDocumentoIdentidad { get; set; }

        public string DocumentoIdentidad { get; set; }

        public string Apellido1 { get; set; }

        public string Apellido2 { get; set; }

        public string Apellido3 { get; set; }

        public string Sexo { get; set; }

        public string Nombre2 { get; set; }

        public string Nombre1 { get; set; }

        public string Nombre3 { get; set; }

        public string NombreApellido { get; set; }
    }

    public partial class DomicilioConstituidoDTO
    {
        public string CodPostal { get; set; }

        public long Id { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string Altura { get; set; }

        public string Piso { get; set; }
    }
}
