using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticClass
{
    public class MailWelcome
    {
        public string NombreApellido { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Renglon1 { get; set; }
        public string Renglon2 { get; set; }
        public string Renglon3 { get; set; }
        public string Urlactivacion { get; set; }
    }

    public class MailPassRecovery
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Renglon1 { get; set; }
        public string Renglon2 { get; set; }
        public string Renglon3 { get; set; }
        public string UrlLogin { get; set; }
    }

    public class MailUsuario
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Renglon1 { get; set; }
        public string Renglon2 { get; set; }
        public string Renglon3 { get; set; }
        public string UrlLogin { get; set; }
    }

    public class MailSolicitudNuevaPuerta
    {
        public string Username { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public int? Seccion { get; set; }
        public string Manzana { get; set; }
        public string Parcela { get; set; }
        public int? NroPartidaMatriz { get; set; }
        public string Calle { get; set; }
        public int NroPuerta { get; set; }
        public string urlFoto { get; set; }
        public string UrlMapa { get; set; }
    }
    public class MailAnulacionAnexo
    {
        public string Renglon1 { get; set; }
        public string Renglon2 { get; set; }
        public string Renglon3 { get; set; }
        public string Profesional { get; set; }
        public string IdEncomienda { get; set; }
    }
    public class MailSolicitudNueva
    {
        public string id_solicitud { get; set; }
        public string codigo_seguridad { get; set; }
    }
}
