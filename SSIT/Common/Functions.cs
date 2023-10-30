using BusinesLayer.Implementation;
using DataTransferObject;
using ExternalService;
using ExternalService.Class;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using static StaticClass.Constantes;

namespace SSIT.Common
{

    public class Functions
    {
        public const string DATOS_CONSULTA = "DATOS_CONSULTA";
        public static string GetUrlFoto(int seccion, string manzana, string parcela, int ancho, int alto)
        {
            string ret = "";
            string SMP = "";
            int tamaManzana = 3;
            int tamaParcela = 3;

            manzana = manzana.Trim();
            parcela = parcela.Trim();


            SMP += seccion.ToString().PadLeft(2, Convert.ToChar("0"));
            SMP += "-";

            if (manzana.Length > 0)
            {
                if (!Char.IsNumber(manzana, manzana.Length - 1))
                    tamaManzana = 4;
            }

            SMP += manzana.PadLeft(tamaManzana, Convert.ToChar("0"));
            SMP += "-";

            if (parcela.Length > 0)
            {
                if (!Char.IsNumber(parcela, parcela.Length - 1))
                    tamaParcela = 4;
            }

            SMP += parcela.PadLeft(tamaParcela, Convert.ToChar("0"));

            ret = string.Format("http://fotos.usig.buenosaires.gob.ar/getFoto?smp={0}&i=0&h={1}&w={2}", SMP, alto, ancho);

            return ret;

        }
        public static string GetUrlMapa(int seccion, string manzana, string parcela, string Direccion)
        {

            string ret = "";
            string SMP = "";
            int tamaManzana = 3;
            int tamaParcela = 3;
            manzana = manzana.Trim();
            parcela = parcela.Trim();
            Direccion = Direccion.Trim();

            SMP += seccion.ToString().PadLeft(2, Convert.ToChar("0"));
            SMP += "-";

            if (manzana.Length > 0)
            {
                if (!Char.IsNumber(manzana, manzana.Length - 1))
                    tamaManzana = 4;
            }

            SMP += manzana.PadLeft(tamaManzana, Convert.ToChar("0"));
            SMP += "-";

            if (parcela.Length > 0)
            {
                if (!Char.IsNumber(parcela, parcela.Length - 1))
                    tamaParcela = 4;
            }

            SMP += parcela.PadLeft(tamaParcela, Convert.ToChar("0"));

            ret = string.Format("http://servicios.usig.buenosaires.gob.ar/LocDir/mapa.phtml?dir={0}&desc={0}&w=400&h=300&punto=5&r=200&smp={1}",
                        Direccion, SMP);
            return ret;

        }
        public static string GetUrlCroquis(string seccion, string manzana, string parcela, string Direccion)
        {

            string ret = "";
            string SMP = "";
            int tamaManzana = 3;
            int tamaParcela = 3;
            seccion = seccion.Trim();
            manzana = manzana.Trim();
            parcela = parcela.Trim();
            Direccion = Direccion.Trim();

            SMP += seccion.PadLeft(2, Convert.ToChar("0"));
            SMP += "-";

            if (manzana.Length > 0)
            {
                if (!Char.IsNumber(manzana, manzana.Length - 1))
                    tamaManzana = 4;
            }

            SMP += manzana.PadLeft(tamaManzana, Convert.ToChar("0"));
            SMP += "-";

            if (parcela.Length > 0)
            {
                if (!Char.IsNumber(parcela, parcela.Length - 1))
                    tamaParcela = 4;
            }

            SMP += parcela.PadLeft(tamaParcela, Convert.ToChar("0"));

            ret = string.Format("http://servicios.usig.buenosaires.gob.ar/LocDir/mapa.phtml?dir={0}&w=400&h=300&punto=5&r=50&smp={1}",
                     Direccion, SMP);
            return ret;

        }

        public static string ImageNotFound(System.Web.UI.Page page)
        {

            return page.ResolveClientUrl("~/Content/img/app/ImageNotFound.png");
        }

        public static string ConvertToBase64String(int value)
        {
            byte[] str1Byte = Encoding.ASCII.GetBytes(Convert.ToString(value));
            string base64 = Convert.ToBase64String(str1Byte);
            return base64;
        }

        public static byte[] GetMD5(byte[] content_file)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                return md5.ComputeHash(content_file);
            }

        }

        public static string GetParametroChar(string CodParam)
        {
            string ret = "";
            ParametrosBL blParam = new ParametrosBL();
            ret = blParam.GetParametroChar(CodParam);
            return ret;
        }

        public static Guid GetUserid()
        {
            Guid userid = Guid.Empty;
            MembershipUser usu = Membership.GetUser();
            if (usu != null)
                userid = (Guid)Membership.GetUser().ProviderUserKey;

            return userid;
        }

        public static string GetErrorMessage(Exception ex)
        {
            string ret = ex.Message;


            if (ex.InnerException != null)
                ret = ex.InnerException.Message;

            return ret;
        }

        public static string GetAppSetting(string name)
        {
            string value = System.Configuration.ConfigurationManager.AppSettings[name];
            return value;
        }

        public static void enviarCambio(SSITSolicitudesDTO sol)
        {
            ParametrosBL parametrosBL = new ParametrosBL();
            SSITSolicitudesBL blSol = new SSITSolicitudesBL();

            string _noESB = parametrosBL.GetParametroChar("SSIT.NO.ESB");
            bool.TryParse(_noESB, out bool noESB);
            if (!noESB)
            {
                string _urlESB = parametrosBL.GetParametroChar("Url.Service.ESB");
                // Producción
                //_urlESB = @"https://servicios.gcba.gob.ar/api/v1";

                string trata = parametrosBL.GetParametroChar("Trata.Habilitacion");
                if (!sol.EsECI)
                {
                    if (sol.IdTipoTramite == (int)Constantes.TipoTramite.AMPLIACION)
                        trata = parametrosBL.GetParametroChar("Trata.Ampliacion");
                    else if (sol.IdTipoTramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                        trata = parametrosBL.GetParametroChar("Trata.RedistribucionDeUso");
                }

                string dir = "";

                List<int> lisSol = new List<int>();
                lisSol.Add(sol.IdSolicitud);
                foreach (var item in blSol.GetDireccionesSSIT(lisSol).ToList())
                    dir += item.direccion + " / ";
                try
                {
                    wsTAD.actualizarTramite(_urlESB, sol.idTAD.Value, sol.IdSolicitud, sol.NroExpedienteSade, trata, dir);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error en el mensaje de resouesta del servicio TAD de actualizarTramite: " + ex.Message);
                }
            }
        }
        public static void enviarParticipantes(SSITSolicitudesDTO sol)
        {
            MembershipUser usuario = Membership.GetUser();
            ParametrosBL parametrosBL = new ParametrosBL();
            string _urlESB = parametrosBL.GetParametroChar("Url.Service.ESB");
            string trata = parametrosBL.GetParametroChar("Trata.Habilitacion");
            if (!sol.EsECI)
            {
                if (sol.IdTipoTramite == (int)Constantes.TipoTramite.AMPLIACION)
                    trata = parametrosBL.GetParametroChar("Trata.Ampliacion");
                else if (sol.IdTipoTramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                    trata = parametrosBL.GetParametroChar("Trata.RedistribucionDeUso");
            }

            var list = wsGP.perfilesPorTrata(_urlESB, trata);
            int idPerfilTit = 0;
            int idPerfilSol = 0;
            int idPerfilTitComplementario = 0;
            foreach (var p in list)
            {
                if (p.nombrePerfil == "TITULAR")
                    idPerfilTit = p.idPerfil;
                else if (p.nombrePerfil == "SOLICITANTE")
                    idPerfilSol = p.idPerfil;
                else if (p.nombrePerfil == "TITULAR COMPLEMENTARIO")
                    idPerfilTitComplementario = p.idPerfil;
            }

            TitularesBL titularesBL = new TitularesBL();

            var lstTitulares = titularesBL.GetTitularesSolicitud(sol.IdSolicitud).ToList();
            UsuarioBL usuBL = new UsuarioBL();

            var lstUsu = new List<UsuarioDTO>();
            var usuDTO = usuBL.Single((Guid)usuario.ProviderUserKey);
            lstUsu.Add(usuDTO);

            var lstParticipantesSSIT = (from t in lstTitulares
                                        select new
                                        {
                                            cuit = t.CUIT,
                                            idPerfil = idPerfilTit,
                                            t.RazonSocial,
                                            t.Apellido,
                                            Nombres = t.Nombre
                                        }).Union(
                                        from u in lstUsu
                                        select new
                                        {
                                            cuit = u.UserName,
                                            idPerfil = idPerfilSol,
                                            u.RazonSocial,
                                            u.Apellido,
                                            Nombres = u.Nombre
                                        }).ToList();

            var lstParticipantesGP = wsGP.GetParticipantesxTramite(_urlESB, sol.idTAD.Value).ToList();

            var listParticipantesSSITCuit = lstParticipantesSSIT.Select(x => x.cuit).Distinct().OrderByDescending(x => x);

            var listParticipantesGPCuit = lstParticipantesGP.Select(x => x.cuit).Distinct().OrderByDescending(x => x);

            var solicitante = lstParticipantesSSIT.FirstOrDefault(x => x.idPerfil == idPerfilSol);

            var titular = lstParticipantesSSIT.FirstOrDefault(x => x.idPerfil == idPerfilTit);

            var listTitularesComplementariosCuit = lstParticipantesSSIT
                                    .Where(x => x.cuit != titular.cuit)
                                    .Select(x => x.cuit)
                                    .ToList();
            //esto para arreglar el backlog de error22
            if (solicitante == null)
            {
                Exception ex22 = new Exception(
                    $"Debe tener solicitante para poder tramitar, titular {titular}," +
                    $"Solicitud : {sol.IdSolicitud}, " +
                    $"idTad : {sol.idTAD}, " +
                    $"usuarioSSIT : {usuDTO.UserName}" 
                    );
                LogError.Write(ex22);
                wsGP.nuevoTramiteParticipante(_urlESB, trata, sol.idTAD.Value, sol.NroExpedienteSade,
                usuDTO.CUIT, (int)TipoParticipante.Solicitante, true, Constantes.Sistema,
                usuDTO.Nombre, usuDTO.Apellido, usuDTO.RazonSocial);
            }
            var cambios = listParticipantesSSITCuit.Except(listParticipantesGPCuit);
            if (cambios.Any())
            {
                // baja
                foreach (var item in lstParticipantesGP)
                {
                    //desvincular todos los  participantes, menos el solicitante
                    if(item.idPerfil != (int)TipoParticipante.Solicitante)
                    {
                        wsGP.DesvincularParticipante(_urlESB, sol.idTAD.Value, solicitante.cuit, solicitante.idPerfil, Constantes.Sistema, item.cuit, item.idPerfil);
                    }
                }

                // alta solicitante/apoderado
                wsGP.nuevoTramiteParticipante(_urlESB, trata, sol.idTAD.Value, sol.NroExpedienteSade,
                solicitante.cuit, solicitante.idPerfil, solicitante.idPerfil == idPerfilSol, Constantes.Sistema,
                solicitante.Nombres, solicitante.Apellido, solicitante.RazonSocial);

                // alta titular 
                wsGP.nuevoTramiteParticipante(_urlESB, trata, sol.idTAD.Value, sol.NroExpedienteSade,
                        titular.cuit, titular.idPerfil, titular.idPerfil == idPerfilSol, Constantes.Sistema, titular.Nombres, titular.Apellido, titular.RazonSocial);

                //alta titulares complementarios
                foreach (var item in lstParticipantesSSIT)
                {
                    if (listTitularesComplementariosCuit.Contains(item.cuit) && item.idPerfil != idPerfilSol)
                    {
                        wsGP.nuevoTramiteParticipante(_urlESB, trata, sol.idTAD.Value, sol.NroExpedienteSade,
                                item.cuit, (int)TipoParticipante.TitularComplementario, item.idPerfil == idPerfilSol, Constantes.Sistema, item.Nombres, item.Apellido, item.RazonSocial);
                    }
                }
            }
        }
        public static void enviarParticipantes(SSITSolicitudesNuevasDTO sol)
        {
            MembershipUser usuario = Membership.GetUser();
            ParametrosBL parametrosBL = new ParametrosBL();
            string _urlESB = parametrosBL.GetParametroChar("Url.Service.ESB");
            string trata = parametrosBL.GetParametroChar("Trata.Habilitacion");
            if (sol.IdTipoTramite == (int)Constantes.TipoTramite.AMPLIACION)
                trata = parametrosBL.GetParametroChar("Trata.Ampliacion");
            else if (sol.IdTipoTramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                trata = parametrosBL.GetParametroChar("Trata.RedistribucionDeUso");

            var list = wsGP.perfilesPorTrata(_urlESB, trata);
            int idPerfilTit = 0;
            int idPerfilSol = 0;
            int idPerfilTitComplementario = 0;
            foreach (var p in list)
            {
                if (p.nombrePerfil == "TITULAR")
                    idPerfilTit = p.idPerfil;
                else if (p.nombrePerfil == "SOLICITANTE")
                    idPerfilSol = p.idPerfil;
                else if (p.nombrePerfil == "TITULAR COMPLEMENTARIO")
                    idPerfilTitComplementario = p.idPerfil;
            }

            TitularesBL titularesBL = new TitularesBL();

            var lstTitulares = titularesBL.GetTitularesSolicitud(sol.IdSolicitud).ToList();
            UsuarioBL usuBL = new UsuarioBL();

            var lstUsu = new List<UsuarioDTO>();
            var usuDTO = usuBL.Single((Guid)usuario.ProviderUserKey);
            lstUsu.Add(usuDTO);

            var lstParticipantesSSIT = (from t in lstTitulares
                                        select new
                                        {
                                            cuit = t.CUIT,
                                            idPerfil = idPerfilTit,
                                            t.RazonSocial,
                                            t.Apellido,
                                            Nombres = t.Nombre
                                        }).Union(
                                        from u in lstUsu
                                        select new
                                        {
                                            cuit = u.UserName,
                                            idPerfil = idPerfilSol,
                                            u.RazonSocial,
                                            u.Apellido,
                                            Nombres = u.Nombre
                                        }).ToList();

            var lstParticipantesGP = wsGP.GetParticipantesxTramite(_urlESB, sol.idTAD.Value).Where(x => x.vigenciaParticipante == true).ToList();

            var listParticipantesSSITCuit = lstParticipantesSSIT.Select(x => x.cuit).Distinct();

            var listParticipantesGPCuit = lstParticipantesGP.Select(x => x.cuit).Distinct();

            var solicitante = lstParticipantesSSIT.FirstOrDefault(x => x.idPerfil == idPerfilSol);

            var titular = lstParticipantesSSIT.FirstOrDefault(x => x.idPerfil == idPerfilTit);

            var listTitularesComplementariosCuit = lstParticipantesSSIT
                                    .Where(x => x.cuit != titular.cuit)
                                    .Select(x => x.cuit)
                                    .ToList();

            var cambios = listParticipantesSSITCuit.Except(listParticipantesGPCuit);
            if (cambios.Any())
            {
                // baja
                foreach (var item in lstParticipantesGP)
                {
                    //desvincular todos los  participantes                    
                    wsGP.DesvincularParticipante(_urlESB, sol.idTAD.Value, solicitante.cuit, solicitante.idPerfil, Constantes.Sistema, item.cuit, item.idPerfil);
                }

                // alta solicitante/apoderado
                wsGP.nuevoTramiteParticipante(_urlESB, trata, sol.idTAD.Value, "",
                solicitante.cuit, solicitante.idPerfil, solicitante.idPerfil == idPerfilSol, Constantes.Sistema,
                solicitante.Nombres, solicitante.Apellido, solicitante.RazonSocial);

                // alta titular 
                wsGP.nuevoTramiteParticipante(_urlESB, trata, sol.idTAD.Value, "",
                        titular.cuit, titular.idPerfil, titular.idPerfil == idPerfilSol, Constantes.Sistema, titular.Nombres, titular.Apellido, titular.RazonSocial);

                //alta titulares complementarios
                foreach (var item in lstParticipantesSSIT)
                {
                    if (listTitularesComplementariosCuit.Contains(item.cuit) && item.idPerfil != idPerfilSol)
                    {
                        wsGP.nuevoTramiteParticipante(_urlESB, trata, sol.idTAD.Value, "",
                                item.cuit, (int)TipoParticipante.TitularComplementario, item.idPerfil == idPerfilSol, Constantes.Sistema, item.Nombres, item.Apellido, item.RazonSocial);
                    }
                }
            }
        }
        public static void enviarCambio(SSITSolicitudesNuevasDTO sol)
        {
            ParametrosBL parametrosBL = new ParametrosBL();
            SSITSolicitudesNuevasBL blSol = new SSITSolicitudesNuevasBL();

            string _urlESB = parametrosBL.GetParametroChar("Url.Service.ESB");
            string trata = parametrosBL.GetParametroChar("Trata.Habilitacion");
            if (sol.IdTipoTramite == (int)Constantes.TipoTramite.AMPLIACION)
                trata = parametrosBL.GetParametroChar("Trata.Ampliacion");
            else if (sol.IdTipoTramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                trata = parametrosBL.GetParametroChar("Trata.RedistribucionDeUso");
            string dir = "";

            string _noESB = parametrosBL.GetParametroChar("SSIT.NO.ESB");
            bool.TryParse(_noESB, out bool noESB);

            List<int> lisSol = new List<int>();
            lisSol.Add(sol.IdSolicitud);
            dir = sol.Nombre_calle + " " + sol.Altura_calle + " " + sol.Piso + " U.F " + sol.UnidadFuncional;
            if (!noESB)
            {

                try
                {
                    wsTAD.actualizarTramite(_urlESB, sol.idTAD.Value, sol.IdSolicitud, "", trata, dir);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error en el mensaje de resouesta del servicio TAD de enviarCambio: " + ex.Message); ;
                }
            }

        }
        public static CuitsRelacionadosPOST isCuitsRelacionados(string cuitAValidar, bool cuitAValidarSpecified, string cuitRepresentado, bool cuitRepresentadoSpecified, Guid user)
        {
            UsuarioBL usuario = new UsuarioBL();
            ExternalServiceAGIP servicio = new ExternalServiceAGIP();
            ExternalService.Class.CuitsRelacionadosDTO cuitsDto = new ExternalService.Class.CuitsRelacionadosDTO();
            CuitsRelacionadosPOST resul;
            try
            {
                var datosUsu = usuario.Single(user);

                if (!String.IsNullOrEmpty(cuitRepresentado))
                {
                    long cuitValidar = Convert.ToInt64(cuitAValidar);
                    long cuitRepre = Convert.ToInt64(cuitRepresentado);

                    cuitsDto.cuitAValidar = cuitValidar;
                    cuitsDto.cuitAValidarSpecified = cuitAValidarSpecified;
                    cuitsDto.cuitRepresentado = cuitRepre;
                    cuitsDto.cuitRepresentadoSpecified = cuitRepresentadoSpecified;
                    cuitsDto.token = datosUsu.token;
                    cuitsDto.sign = datosUsu.sign;
                    cuitsDto.servicioNombre = "";

                    resul = servicio.CuitsRelacionados(cuitsDto);
                }
                else
                    throw new Exception("Debe ingresar los datos del Titular");

                return resul;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ApoderamientoEntity validarApoderamiento(string cuitTitular, string cuitApoderado)
        {

            ExternalServiceApoderamiento servicio = new ExternalServiceApoderamiento();
            ApoderamientoEntity resul;
            try
            {

                if (!String.IsNullOrEmpty(cuitTitular))
                {
                    resul = servicio.ValidarApoderamientoonados(cuitTitular, cuitApoderado);
                    //if (!resul.relacion)
                    //{
                    //    throw new Exception(resul.descripcion);
                    //}
                }
                else
                    throw new Exception("Debe ingresar los datos del Titular");

                return resul;
            }
            catch (Exception ex)
            {
                LogError.Write(ex);
                throw ex;
            }
        }


        public static bool isDesarrollo()
        {
            string value = System.Configuration.ConfigurationManager.AppSettings["isDesarrollo"];
            bool desarrollo = false;
            bool.TryParse(value, out desarrollo);
            return desarrollo;
        }


    }
}
