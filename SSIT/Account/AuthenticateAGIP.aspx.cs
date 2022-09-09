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