using BusinesLayer.Implementation;
using DataAcess;
using DataTransferObject;
using ExternalService;
using ExternalService.Class.Express;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Tls;
using Reporting;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using static iTextSharp.text.pdf.qrcode.Version;

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

        [WebMethod]
        public bool InsertarCAA_DocAdjuntos(string user, string pass, int id_solicitud)
        {
            //uso las credenciales de APRA para este endpoint
            //ya se que no es lo ideal pero son las 1am
            //y estoy programando desde las 6am del dia de ayer
            int CAA_id = 0;
            string Usuario = ConfigurationManager.AppSettings["UsuarioApraAgc"];
            string Password = ConfigurationManager.AppSettings["PasswordApraAgc"];
            if (user.IsNullOrEmpty())
                throw new Exception("El nombre de usuario no puede ser nulo.");
            else if (Password != pass)
                throw new Exception("La Contraseña ingresada es incorrecta.");

            ExternalServiceFiles files_service = new ExternalServiceFiles();

            EncomiendaBL blEnc = new EncomiendaBL();
            var lstEnc = blEnc.GetByFKIdSolicitud(id_solicitud);
            List<int> encomiendas = lstEnc.Select(e => e.IdEncomienda).ToList();

            GetCAAsByEncomiendasWrapResponse caaWrap = null;
            Task.Run(async () =>
            {
                caaWrap = await GetCAAsByEncomiendas(encomiendas);
            }).Wait();
             
            List<GetCAAsByEncomiendasResponse> lstRCAAenc = caaWrap.ListCaa;
            if (lstRCAAenc != null && lstRCAAenc.Count > 0)
            {
                foreach (var caa_act in lstRCAAenc)
                {
                    CAA_id = caa_act.id_solicitud;
                    if (CAA_id > 0)
                    {
                        GetCAAResponse caa = null;
                        Task.Run(async () =>
                        {
                            caa = await GetCAA(CAA_id);
                        }).Wait();
                        var fileInfo = GetCAA_fileInfo(caa, caa_act.formulario.id_encomienda_agc);
                    }
                    
                }
            }
            else
                return false;   // no tiene CAA que agregar

            
            return true;    //Agrego los CAA con exito (perhaps)
        }

        private async Task<GetCAAsByEncomiendasWrapResponse> GetCAAsByEncomiendas(List<int> lst_id_Encomiendas)
        {
            ExternalService.ApraSrvRest apraSrvRest = new ExternalService.ApraSrvRest();
            GetCAAsByEncomiendasWrapResponse lstCaa = await apraSrvRest.GetCAAsByEncomiendas(lst_id_Encomiendas);
            return lstCaa;
        }

        private async Task<GetCAAResponse> GetCAA(int id_caa)
        {
            ExternalService.ApraSrvRest apraSrvRest = new ExternalService.ApraSrvRest();
            GetCAAResponse jsonCaa = await apraSrvRest.GetCaa(id_caa);
            return jsonCaa;
        }

        private byte[] GetCAA_fileBytes(Task<string> json)
        {
            byte[] file = null;
            return file;
        }

        private bool GetCAA_fileInfo(GetCAAResponse response, int id_encomienda)
        {
            bool subioFile = false;
            string fileName = response.certificado.fileName;
            string extension = response.certificado.extension;
            DateTime fechaCreacionCAA = response.fechaIngreso;
            byte[] rawBytes = Convert.FromBase64String(response.certificado.rawBytes);
            int id_tipocertificado = 1;

            switch (response.id_tipocertificado)
            {
                case 1:
                    id_tipocertificado = 18;
                    break;
                case 2:
                    id_tipocertificado = 19;
                    break;
                case 3:
                    id_tipocertificado = 17;
                    break;
                case 4:
                    id_tipocertificado = 16;
                    break;
                case 5:
                    id_tipocertificado = 53;
                    break;
                default:
                    id_tipocertificado = 53;
                    break;
            }

            EncomiendaBL encomiendaBL = new EncomiendaBL();
            Guid userid = (Guid)Membership.GetUser("ws-sgi").ProviderUserKey;
            subioFile = encomiendaBL.InsertarCAA_DocAdjuntos(id_encomienda, userid, rawBytes, fileName, extension, id_tipocertificado, fechaCreacionCAA);
            return subioFile;
        }

    }
}