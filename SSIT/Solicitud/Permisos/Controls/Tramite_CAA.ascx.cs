using BusinesLayer.Implementation;
using DataAcess;
using DataTransferObject;
using ExternalService.Class.Express;
using ExternalService.ws_interface_AGC;
using SSIT.Common;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Solicitud.Permisos.Controls
{
    public partial class Tramite_CAA : System.Web.UI.UserControl
    {

        public partial class DtoRACGenerado
        {
            public int id_solicitud_caa { get; set; }
            public int id_caa { get; set; }
            public int id_rac { get; set; }
            public int id_form_rac { get; set; }
        }
        

        public class CAAErrorEventArgs : EventArgs
        {
            public int Code { get; set; }
            public string Description { get; set; }
        }

        public delegate void EventHandlerError(object sender, CAAErrorEventArgs e);
        public event EventHandlerError Error;

        private DtoCAA _CAA_Actual = null;

        #region "Propiedades"

        public int id_solicitud
        {
            get
            {
                return (ViewState["_id_solicitud"] != null ? (int)ViewState["_id_solicitud"] : 0);
            }
            set
            {
                ViewState["_id_solicitud"] = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return (ViewState["_Enabled"] != null ? Convert.ToBoolean(ViewState["_Enabled"]) : true);
            }
            set
            {
                ViewState["_Enabled"] = value;
            }
        }

        #endregion

        private SSITSolicitudesBL blSol = new SSITSolicitudesBL();
        
        public void Cargar_Datos(SSITSolicitudesDTO solcitud)
        {
            
            this.id_solicitud = solcitud.IdSolicitud;
            txtNroCAARelacionado.Text = "";
            txtNroRACRelacionado.Text = "";

            if (solcitud.SSITPermisosDatosAdicionalesDTO != null)
            {
                txtNroCAARelacionado.Text = solcitud.SSITPermisosDatosAdicionalesDTO.id_solicitud_caa.ToString();
                txtNroRACRelacionado.Text = solcitud.SSITPermisosDatosAdicionalesDTO.id_rac.ToString();
            }

            if(solcitud.IdEstado == (int) Constantes.TipoEstadoSolicitudEnum.COMP)
            {
                pnlBuscarCAA.Visible = true;
                pnlDatosRAC.Visible = false;
            }
            else
            {
                pnlBuscarCAA.Visible = false;
                pnlDatosRAC.Visible = true;
            }

        }
        

        public bool ValidarDatosPermiso(int IdSolicitud)
        {
            bool ret = false;
            CAAErrorEventArgs args = new CAAErrorEventArgs();

            SSITSolicitudesDTO sol = blSol.Single(IdSolicitud);

            if (txtNroCAA.Text.Trim().Length == 0 || txtCodSeguridadCAA.Text.Trim().Length == 0)
            {
                if (sol.IdTipoTramite != (int)Constantes.TipoDeTramite.Permisos || sol.IdTipoExpediente != (int)Constantes.TipoDeExpediente.MusicaCanto)
                {
                    args.Code = 5000;
                    args.Description = "Debe ingresar el Nro de CAA y el código de seguridad.";
                }
            }

            if (string.IsNullOrWhiteSpace(args.Description) && sol.IdEstado != (int) Constantes.TipoEstadoSolicitudEnum.COMP)
            {
                args.Code = 5000;
                args.Description = "La solicitud debe encontrarse en estado Completo, para poder evaluar el CAA." + Environment.NewLine;
            }

            if (string.IsNullOrWhiteSpace(args.Description) && !sol.SSITSolicitudesRubrosCNDTO.Any(x=> x.CodigoRubro == Constantes.RUBRO_MUSICA_Y_CANTO))
            {
                args.Code = 5000;
                args.Description = "La solicitud de Permiso debe contener la actividad 1.5 Alimentación en general y gastronomía." + Environment.NewLine;
            }

            if (!string.IsNullOrWhiteSpace(args.Description))
            {
                if (Error != null)
                {
                    Error(this, args);
                }
            }
            else
            {
                if (sol.IdTipoTramite == (int)Constantes.TipoDeTramite.Permisos && sol.IdTipoExpediente == (int)Constantes.TipoDeExpediente.MusicaCanto && (txtNroCAA.Text.Trim().Length == 0 || txtCodSeguridadCAA.Text.Trim().Length == 0))
                {
                    ret = false;
                }
                else
                {
                    ret = true;
                }
            }

            return ret;

        }
        //No hay un REST equivalente
        public DtoRACGenerado GenerarRAC(int id_solicitud_agc)
        {

            DtoRAC resultService = null;
            DtoRACGenerado result = null;
            CAAErrorEventArgs args = new CAAErrorEventArgs();
            args.Code = 5000;
            List<string> lstMensajes = new List<string>();
            System.Web.Security.MembershipUser usu = System.Web.Security.Membership.GetUser();
            string codigo_seguridad_CAA = txtCodSeguridadCAA.Text.Trim().ToUpper();
            int id_solicitud_caa = 0;
            int.TryParse(txtNroCAA.Text.Trim(), out id_solicitud_caa);

            if (ValidarDatosPermiso(id_solicitud_agc))
            {
                ws_Interface_AGC servicio = new ws_Interface_AGC();
                wsResultado ws_resultado_CAA = new wsResultado();
                wsResultado ws_resultado_RAC = new wsResultado();
                ParametrosBL blParam = new ParametrosBL();
                WsEscribanosActaNotarialBL blActaNotarial = new WsEscribanosActaNotarialBL();
                EncomiendaBL blEnc = new EncomiendaBL();

                servicio.Url = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC");
                string username_servicio = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.User");
                string password_servicio = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.Password");


                //Valida el codigo de seguridad de la solicitud de CAA
                //if (!servicio.ValidarCodigoSeguridad(username_servicio, password_servicio, id_solicitud_caa, codigo_seguridad_CAA, ref ws_resultado_CAA))
                ValidarCodigoSeguridadResponse resCodSeg = new ValidarCodigoSeguridadResponse();
                Task.Run(async () =>
                {
                    resCodSeg = await ValidarCodigoSeguridad(id_solicitud_caa, codigo_seguridad_CAA);
                }).Wait();
                if (resCodSeg == null)  //esto indicaria un error de comunicación con APRA, no creamos el permiso de musica y canto si esto sucede
                {
                    Exception exception = new Exception("Hubo un error al intentar validar el codigo de seguridad del caa");
                    LogError.Write(exception + $" id_solicitud_agc:{id_solicitud_agc}  id_solicitud_caa:{id_solicitud_caa} codigo_seguridad_CAA:{codigo_seguridad_CAA}");
                    throw exception;
                }
                if (!resCodSeg.EsValido)
                {
                    args.Description = ws_resultado_CAA.ErrorDescription;
                    if (Error != null)
                        Error(this, args);
                }
                else
                {
                    //Obtiene los datos de la solicitud de CAA.
                    //DtoCAA[] arrSolCAA = servicio.Get_CAAs(username_servicio, password_servicio, new int[] { id_solicitud_caa }, ref ws_resultado_CAA);
                    var lst_encomiendas = blEnc.GetByFKIdSolicitud(id_solicitud_agc);
                    List<GetCAAsByEncomiendasResponse> lstDocCAA = null;
                    Task.Run(async () =>
                    {
                        var listCaaW = await GetCAAsByEncomiendas(lst_encomiendas.Select(enc => enc.IdEncomienda).ToList());
                        lstDocCAA = listCaaW.ListCaa;
                    }).Wait();

                    if (lstDocCAA != null && lstDocCAA.Count() > 0)
                    {
                        GetCAAsByEncomiendasResponse solCAA = lstDocCAA.Where(x => x.id_estado == (int)Constantes.CAA_EstadoSolicitud.Aprobado).OrderByDescending(x => x.id_solicitud).FirstOrDefault();
                        GetCAAResponse caa = new GetCAAResponse();
                        if(solCAA != null)
                        {
                            Task.Run(async () =>
                            {
                                caa = await GetCAA(solCAA.id_solicitud);
                            }).Wait();
                            if (caa != null)
                                lstMensajes = blSol.CompareWithCAA(id_solicitud_agc, caa);
                        }
                        else
                        {
                            lstMensajes.Add("No se encontro ninguna encomienda Aprobada con CAA Aprobado.");
                        }
                        

                        if (lstMensajes.Count == 0)
                        {
                            resultService = servicio.Generar_RAC_MusicaCanto(username_servicio, password_servicio, id_solicitud_caa,
                                                    codigo_seguridad_CAA, id_solicitud_agc, usu.UserName, ref ws_resultado_RAC);

                            if (resultService == null)
                            {
                                args.Description = ws_resultado_RAC.ErrorDescription;

                                if (Error != null)
                                    Error(this, args);

                            }
                            else
                            {
                                txtNroCAARelacionado.Text = solCAA.id_solicitud.ToString();
                                txtNroRACRelacionado.Text = resultService.id_rac.ToString();
                                pnlBuscarCAA.Visible = false;
                                pnlDatosRAC.Visible = true;
                                result = new DtoRACGenerado();
                                result.id_caa = resultService.id_caa;
                                result.id_solicitud_caa = solCAA.id_solicitud;
                                result.id_rac = resultService.id_rac;
                                result.id_form_rac = resultService.id_form_rac;
                            }

                        }
                        else
                        {

                            if (lstMensajes.Count() > 0)
                            {
                                args.Description = "No es posible vincular la solicitud de CAA, se encontraron los siguientes inconvenientes:<br/><ul>";
                                foreach (var item in lstMensajes)
                                {
                                    args.Description += "<li>" + item + "</li>";
                                }
                                args.Description += "</ul>";

                                if (Error != null)
                                    Error(this, args);

                            }

                        }

                    }
                    else
                    {
                        args.Description = ws_resultado_CAA.ErrorDescription;
                        if (Error != null)
                            Error(this, args);

                    }
                }

                servicio.Dispose();

            }

            return result;
        }

        private async Task<ValidarCodigoSeguridadResponse> ValidarCodigoSeguridad(int id_caa, string codigo_caa)
        {
            ExternalService.ApraSrvRest apraSrvRest = new ExternalService.ApraSrvRest();
            ValidarCodigoSeguridadResponse resultado = await apraSrvRest.ValidarCodigoSeguridad(id_caa, codigo_caa);
            return resultado;
        }
        private async Task<GetCAAResponse> GetCAA(int id_caa)
        {
            ExternalService.ApraSrvRest apraSrvRest = new ExternalService.ApraSrvRest();
            GetCAAResponse jsonCaa = await apraSrvRest.GetCaa(id_caa);
            return jsonCaa;
        }
        private async Task<GetCAAsByEncomiendasWrapResponse> GetCAAsByEncomiendas(List<int> lst_id_Encomiendas)
        {
            ExternalService.ApraSrvRest apraSrvRest = new ExternalService.ApraSrvRest();
            GetCAAsByEncomiendasWrapResponse lstCaa = await apraSrvRest.GetCAAsByEncomiendas(lst_id_Encomiendas);
            return lstCaa;
        }

    }
}