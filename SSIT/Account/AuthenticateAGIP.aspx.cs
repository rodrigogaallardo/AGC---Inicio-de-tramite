using SSIT.App_Components;
using System;
using System.IO;
using System.Xml.Serialization;

namespace SSIT.Account
{
    #region "clases AGIP"


    [XmlRoot(ElementName = "datos")]
    public class Datos
    {
        [XmlElement(ElementName = "servicio")]
        public Servicio Servicio { get; set; }
        [XmlElement(ElementName = "autenticado")]
        public Autenticado Autenticado { get; set; }
        [XmlElement(ElementName = "representados")]
        public Representados Representados { get; set; }
    }



    [XmlRoot(ElementName = "servicio")]
    public class Servicio
    {
        [XmlAttribute(AttributeName = "nombre")]
        public string Nombre { get; set; }
        [XmlAttribute(AttributeName = "exp_time")]
        public string Exp_time { get; set; }
    }

    [XmlRoot(ElementName = "autenticado")]
    public class Autenticado
    {
        [XmlAttribute(AttributeName = "cuit")]
        public string Cuit { get; set; }
        [XmlAttribute(AttributeName = "nombre")]
        public string Nombre { get; set; }
        [XmlAttribute(AttributeName = "isib")]
        public string Isib { get; set; }
        [XmlAttribute(AttributeName = "cat")]
        public string Cat { get; set; }
        [XmlAttribute(AttributeName = "codcalle")]
        public string Codcalle { get; set; }
        [XmlAttribute(AttributeName = "calle")]
        public string Calle { get; set; }
        [XmlAttribute(AttributeName = "puerta")]
        public string Puerta { get; set; }
        [XmlAttribute(AttributeName = "piso")]
        public string Piso { get; set; }
        [XmlAttribute(AttributeName = "dpto")]
        public string Dpto { get; set; }
        [XmlAttribute(AttributeName = "codpostal")]
        public string Codpostal { get; set; }
        [XmlAttribute(AttributeName = "codlocalidad")]
        public string Codlocalidad { get; set; }
        [XmlAttribute(AttributeName = "localidad")]
        public string Localidad { get; set; }
        [XmlAttribute(AttributeName = "codprov")]
        public string Codprov { get; set; }
        [XmlAttribute(AttributeName = "provincia")]
        public string Provincia { get; set; }
        [XmlAttribute(AttributeName = "telefono")]
        public string Telefono { get; set; }
        [XmlAttribute(AttributeName = "email")]
        public string Email { get; set; }
        [XmlAttribute(AttributeName = "nivel")]
        public string Nivel { get; set; }
        [XmlAttribute(AttributeName = "tipoDocumento")]
        public string TipoDocumento { get; set; }
        [XmlAttribute(AttributeName = "documento")]
        public string Documento { get; set; }
    }

    [XmlRoot(ElementName = "representado")]
    public class Representado
    {
        [XmlAttribute(AttributeName = "cuit")]
        public string Cuit { get; set; }
        [XmlAttribute(AttributeName = "nombre")]
        public string Nombre { get; set; }
        [XmlAttribute(AttributeName = "isib")]
        public string Isib { get; set; }
        [XmlAttribute(AttributeName = "cat")]
        public string Cat { get; set; }
        [XmlAttribute(AttributeName = "codcalle")]
        public string Codcalle { get; set; }
        [XmlAttribute(AttributeName = "calle")]
        public string Calle { get; set; }
        [XmlAttribute(AttributeName = "puerta")]
        public string Puerta { get; set; }
        [XmlAttribute(AttributeName = "piso")]
        public string Piso { get; set; }
        [XmlAttribute(AttributeName = "dpto")]
        public string Dpto { get; set; }
        [XmlAttribute(AttributeName = "codpostal")]
        public string Codpostal { get; set; }
        [XmlAttribute(AttributeName = "codlocalidad")]
        public string Codlocalidad { get; set; }
        [XmlAttribute(AttributeName = "localidad")]
        public string Localidad { get; set; }
        [XmlAttribute(AttributeName = "codprov")]
        public string Codprov { get; set; }
        [XmlAttribute(AttributeName = "provincia")]
        public string Provincia { get; set; }
        [XmlAttribute(AttributeName = "telefono")]
        public string Telefono { get; set; }
        [XmlAttribute(AttributeName = "email")]
        public string Email { get; set; }
        [XmlAttribute(AttributeName = "tipoRepresentacion")]
        public string TipoRepresentacion { get; set; }
        [XmlAttribute(AttributeName = "tipoDocumento")]
        public string TipoDocumento { get; set; }
        [XmlAttribute(AttributeName = "documento")]
        public string Documento { get; set; }
        [XmlAttribute(AttributeName = "elegido")]
        public string Elegido { get; set; }
    }

    [XmlRoot(ElementName = "representados")]
    public class Representados
    {
        [XmlElement(ElementName = "representado")]
        public Representado Representado { get; set; }
    }




    #endregion

    #region MiBa 
    public class DatosMiBA
    {
        public Personalogin personaLogin { get; set; }
        public object idTad { get; set; }
        public Apoderados apoderados { get; set; }
        public object tipoTramite { get; set; }
        public object[] poderdantes { get; set; }
    }

    public class Personalogin
    {
        public int id { get; set; }
        public Persona persona { get; set; }
        public string calle { get; set; }
        public string altura { get; set; }
        public object piso { get; set; }
        public object depto { get; set; }
        public string codigoPostal { get; set; }
        public object telefono { get; set; }
        public object observaciones { get; set; }
    }

    public class Persona
    {
        public int id { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public object razonSocial { get; set; }
        public string cuit { get; set; }
        public string tipoDocumento { get; set; }
        public string numeroDocumento { get; set; }
        public string sexo { get; set; }
        public string codigoPais { get; set; }
        public string codigoTelefonoPais { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public int? usuarioCreacion { get; set; }
        public long? fechaAlta { get; set; }
        public object usuarioModificacion { get; set; }
        public long fechaModificacion { get; set; }
        public string tipoPersona { get; set; }
        public int valiRenaper { get; set; }
        public Terminosycondiciones terminosYCondiciones { get; set; }
        public object sistemaConsumidor { get; set; }
        public object baId { get; set; }
        public object habilitadaVista360 { get; set; }
    }

    public class Terminosycondiciones
    {
        public int id { get; set; }
        public Tipodocumento tipoDocumento { get; set; }
        public string estado { get; set; }
        public long fechaAlta { get; set; }
        public string contenido { get; set; }
        public Nivelacceso nivelAcceso { get; set; }
    }

    public class Tipodocumento
    {
        public int id { get; set; }
        public string acronimoGedo { get; set; }
        public string acronimoTAD { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public object formularioControlado { get; set; }
        public object tipoProduccion { get; set; }
        public string usuarioIniciador { get; set; }
        public string usuarioCreacion { get; set; }
        public long fechaAlta { get; set; }
        public object usuarioModificacion { get; set; }
        public long fechaModificacion { get; set; }
        public bool esEmbebido { get; set; }
        public bool firmaConToken { get; set; }
        public object ip { get; set; }
        public bool esFirmaConjunta { get; set; }
        public object documentoTipoFirma { get; set; }
        public object textoLibreLimite { get; set; }
        public bool textoLibreEnriquecido { get; set; }
        public object embebidoOpcional { get; set; }
        public bool esFirmaCloud { get; set; }
    }

    public class Nivelacceso
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

    public class Apoderados
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





    #endregion

    public partial class AuthenticateAGIP : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string value = System.Configuration.ConfigurationManager.AppSettings["mantenimiento"];
                if (value != null && Convert.ToBoolean(value))
                {
                    int cargo = 0;
                    int.TryParse(Convert.ToString(Page.RouteData.Values["cargo"]), out cargo);
                    if (cargo == 0)
                        Response.Redirect(string.Format("~/" + RouteConfig.MANTENIMIENTO + "{0}", 1));
                }

                AuthenticateAGIPProc auth = new AuthenticateAGIPProc();
                auth.ReadData();
            }
        }


    }
}