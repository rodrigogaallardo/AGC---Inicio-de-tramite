using BusinesLayer.Implementation;
using ExternalService;
using Reporting;
using StaticClass;
using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;

namespace SSIT
{
    /// <summary>
    /// Descripción breve de WSssit
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WSssit : System.Web.Services.WebService
    {

        [WebMethod]
        public bool generarDocInicioTramite(string user, string pass, int id_solicitud)
        {

            if (user == null || user.Count() == 0)
                throw new Exception("El nombre de usuario no puede ser nulo.");

            MembershipUser usuario = Membership.GetUser(user);

            if (usuario == null)
                throw new Exception("El nombre de usuario no existe.");
            else if (usuario.GetPassword() != pass)
                throw new Exception("La Contraseña ingresada es incorrecta.");

            ExternalServiceReporting service = new ExternalServiceReporting();
            var reporte =  service.GetPDFOblea(id_solicitud, true);

            SSITSolicitudesBL blSol = new SSITSolicitudesBL();
            Guid userid = (Guid)usuario.ProviderUserKey;
            return blSol.SetOblea(id_solicitud, userid, reporte.Id_file, reporte.FileName);
        }

        [WebMethod]
        public bool enviarEncuesta(string user, string pass, int id_solicitud)
        {

            if (user == null || user.Count() == 0)
                throw new Exception("El nombre de usuario no puede ser nulo.");

            MembershipUser usuario = Membership.GetUser(user);

            if (usuario == null)
                throw new Exception("El nombre de usuario no existe.");
            else if (usuario.GetPassword() != pass)
                throw new Exception("La Contraseña ingresada es incorrecta.");

            ExternalServiceEncuesta service = new ExternalServiceEncuesta();

            SSITSolicitudesBL blSol = new SSITSolicitudesBL();
            var enc = blSol.getEncuesta(id_solicitud);
            return service.enviar(enc);
        }
    }
}