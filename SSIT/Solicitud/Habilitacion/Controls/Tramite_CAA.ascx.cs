using BusinesLayer.Implementation;
using DataTransferObject;
using ExternalService.ws_interface_AGC;
using SSIT.App_Components;
using SSIT.Common;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;
//using ExternalService;
using ExternalService.Class.Express;
using System.Threading.Tasks;
using System.Web.Security;

namespace SSIT.Solicitud.Habilitacion.Controls
{
    public partial class Tramite_CAA : System.Web.UI.UserControl
    {
        public delegate void EventHandlerRecargarPago(object sender, EventArgs e);
        public event EventHandlerRecargarPago RecargarPago;

        public class CAAErrorEventArgs : EventArgs
        {
            public int Code { get; set; }
            public string Description { get; set; }
        }

        public delegate void EventHandlerError(object sender, CAAErrorEventArgs e);
        public event EventHandlerError Error;

        private List<DtoCAA> _List_CAA = new List<DtoCAA>();
        private List<GetCAAsByEncomiendasResponse> _lstCaa = new List<GetCAAsByEncomiendasResponse>();
        private DtoCAA _CAA_Actual = null;
        private GetCAAsByEncomiendasResponse _caaAct = null;

        public class ucRecargarPagosEventsArgs : EventArgs
        {
            public int id_solicitud { get; set; }
            public Constantes.PagosTipoTramite tipo_tramite { get; set; }
        }

        private enum TipoCertificadoCAA
        {
            sre = 16,
            sreCC = 17,
            sc = 18,
            cre = 19,
            DDJJ = 120   //TODO: Insertar nueva fila en la base de datos
        }

        public delegate void EventHandlerRecargarPagos(object sender, ucRecargarPagosEventsArgs e);
        public event EventHandlerRecargarPagos EventRecargarPagos;

        #region "Propiedades"

        public int id_encomienda
        {
            get
            {
                return (ViewState["_id_encomienda"] != null ? (int)ViewState["_id_encomienda"] : 0);
            }
            set
            {
                ViewState["_id_encomienda"] = value;
            }
        }
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

        public List<DtoCAA> List_CAA
        {
            get
            {
                return _List_CAA;
            }
            set
            {
                _List_CAA = value;
            }
        }

        public List<GetCAAsByEncomiendasResponse> LstCaa
        {
            get
            {
                return _lstCaa;
            }
            set
            {
                _lstCaa = value;
            }
        }

        public DtoCAA CAA_Actual
        {
            get
            {
                return _CAA_Actual;
            }
            set
            {
                _CAA_Actual = value;
            }
        }

        public GetCAAsByEncomiendasResponse CaaAct

        {
            get
            {
                return _caaAct;
            }
            set
            {
                _caaAct = value;
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
                if (btnGenerarCAA.Visible)
                {
                    btnGenerarCAA.Visible = value;
                }

            }
        }

        #endregion

        public class CAA_ArchivosDTO
        {
            public int id_solicitud { get; set; }
            public int id_caa { get; set; }
            public int id_file { get; set; }
            public int id_encomienda { get; set; }
            public DateTime CreateDate { get; set; }
            public string nombre { get; set; }
            public int id_tipocertificado { get; set; }
            public string codigo_tipocertificado { get; set; }
            public string url { get; set; }
            public int id_estado_caa { get; set; }
            public string estado_caa { get; set; }
            public string nombre_tipocertificado { get; set; }
            public bool mostrarDoc { get; set; }
        }

        private SSITSolicitudesBL blSol = new SSITSolicitudesBL();
        private EncomiendaBL encBL = new EncomiendaBL();
        public async Task Cargar_Datos(int id_solicitud)
        {
            pnlErrorBuscarCAA.Visible = false;
            lblErrorBuscarCAA.Text = "";

            EncomiendaBL blEnc = new EncomiendaBL();
            var lst_encomiendas = blEnc.GetByFKIdSolicitud(id_solicitud);

            await Cargar_Datos(lst_encomiendas, id_solicitud);
        }
        public async Task Cargar_Datos(IEnumerable<EncomiendaDTO> lst_encomiendas, int id_solicitud)
        {
            int nroSolReferencia = 0;
            int.TryParse(ConfigurationManager.AppSettings["NroSolicitudReferencia"], out nroSolReferencia);

            var MostrarPanelesCAA = new List<int>();
            MostrarPanelesCAA.Add((int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF);
            MostrarPanelesCAA.Add((int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO);

            var solBl = new SSITSolicitudesBL();
            var sol = solBl.Single(id_solicitud);

            updTramiteCAA.Visible = true;

            //el Btn Ingresar A SIPSA y Buscar solo se muestran cuando la sol esta en estado datos Confirmados y Observado
            if (MostrarPanelesCAA.Contains(sol.IdEstado))
            {
                bool CondicionExpress = false;
                foreach (var _encomiendas in lst_encomiendas)
                {
                    foreach (var rubrosCN in _encomiendas.EncomiendaRubrosCNDTO)
                    {
                        if (rubrosCN.RubrosDTO.CondicionExpress == true)
                        {
                            CondicionExpress = true;
                        }
                        else
                        {
                            CondicionExpress = false;
                            goto Found;
                        }
                    }
                }

            Found:
                if (CondicionExpress == true)
                {
                    DivBtnSIPSAExpress.Visible = true;
                }
                else
                {
                    updBuscarCAA.Visible = true;
                    DivBtnSIPSA.Visible = true;
                }
            }


            grdArchivosCAA.Visible = true;
            id_encomienda = 0;
            List<int> estados = new List<int>();
            estados.Add((int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo);
            estados.Add((int)Constantes.Encomienda_Estados.Vencida);

            this.id_solicitud = id_solicitud;

            //se cambia logica para q se compare el ultimo anexo tipo "A" y posteriores
            //mantis 149128: JADHE 57846 - SSIT - No permite generar una nueva BUI

            if (lst_encomiendas.Count() > 0)
            {
                var UltimaA = lst_encomiendas.Where(x => estados.Contains(x.IdEstado) && x.tipoAnexo == Constantes.TipoAnexo_A).OrderByDescending(x => x.IdEncomienda).FirstOrDefault();
                if (UltimaA != null)
                {
                    var listaEncomiendas = lst_encomiendas.Where(x => x.IdEncomienda >= UltimaA.IdEncomienda).ToList();
                    // Habilita el ingreso de CAA por comparación
                    if (listaEncomiendas != null)
                    {
                        id_encomienda = listaEncomiendas.Last().IdEncomienda;
                        int idEstado = listaEncomiendas.Last().EncomiendaSSITSolicitudesDTO.Select(x => x.SSITSolicitudesDTO.IdEstado).FirstOrDefault();
                        if (idEstado == (int)Constantes.TipoEstadoSolicitudEnum.INCOM
                            || idEstado == (int)Constantes.TipoEstadoSolicitudEnum.COMP
                            || idEstado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF
                            || idEstado == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO)
                        {
                            pnlBuscarCAA.Visible = true;
                        }
                    } 
                }
                // Llena los CAAs de acuerdo a las encomiendas vinculadas a la solicitud.
                // ---------------------------------------------------------------------
                #region Luis CAA rest
                _lstCaa = await GetCAAsByEncomiendas(lst_encomiendas.Select(x => x.IdEncomienda).ToArray());

                #endregion
            }
            // Establece el CAA relacionado con la encomienda 
            // ----------------------------------------------

            if (_lstCaa != null && _lstCaa.Count > 0 && _lstCaa.FirstOrDefault().formulario.id_encomienda_agc != 0)
            {

                //this.CAA_Actual = List_CAA.Where(x => x.id_estado != (int)Constantes.CAA_EstadoSolicitud.Anulado).OrderByDescending(x => x.id_caa).FirstOrDefault();
                _caaAct = _lstCaa.Where(caa => caa.id_estado != (int)Constantes.CAA_EstadoSolicitud.Anulado).OrderByDescending(caa => caa.id_solicitud).FirstOrDefault();

            }

            if (_caaAct != null)
            {
                btnGenerarCAA.Visible = false;
                if ((_caaAct.id_tipocertificado == (int)Constantes.CAA_TipoCertificado.SujetoaCategorizacion ||
                     _caaAct.id_tipocertificado == (int)Constantes.CAA_TipoCertificado.ConRelevanteEfecto) &&
                     _caaAct.id_estado          != (int)Constantes.CAA_EstadoSolicitud.Aprobado)
                {
                    //pnlInfoSIPSA.Visible = id_solicitud > Constantes.SOLICITUDES_NUEVAS_MAYORES_A;
                    //pnlInfo.Visible = false;
                    grdArchivosCAA.Visible = false;
                }
            }
            if (lst_encomiendas.Count() > 0)
            {
                var lstArchivosCAA = new List<CAA_ArchivosDTO>();
                EncomiendaDTO Encomienda = encBL.Single(id_encomienda);
                EncomiendaDocumentosAdjuntosBL encDocBL = new EncomiendaDocumentosAdjuntosBL();
                
                int id_tipodocsis = (int)Constantes.TiposDeDocumentosSistema.CERTIFICADO_CAA;
                var ListDocAdj = encDocBL.GetByFKIdEncomiendaTipoSis(id_encomienda, id_tipodocsis).ToList();
                //antes deberia recorrer la lista de encomiendas y despues este foreach
                //o quiza enta bien solamente tomar la ultima aprobada
                foreach(var docAdj in ListDocAdj)
                {
                    CAA_ArchivosDTO item = new CAA_ArchivosDTO();
                    item.id_encomienda = _caaAct.formulario.id_encomienda_agc; //revisar si es el mismo
                    item.id_encomienda = docAdj.id_encomienda;
                    item.id_file = docAdj.id_file;
                    item.CreateDate = docAdj.CreateDate;
                    item.url = string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Functions.ConvertToBase64String(item.id_file));
                    //_caaAct = _lstCaa.Where(caa => caa.formulario.id_encomienda_agc).OrderByDescending(caa => caa.id_solicitud).FirstOrDefault();
                    // por ahora uso _caa_Act

                    item.id_caa = _caaAct.id_solicitud;
                    item.id_solicitud = Encomienda.IdSolicitud;
                    item.mostrarDoc = true;// antes hacia esto caa.Documentos.Count() > 0;
                    item.nombre = docAdj.nombre_archivo;
                    item.id_tipocertificado = _caaAct.id_tipocertificado;
                    item.estado_caa = _caaAct.estado;
                    item.codigo_tipocertificado = _caaAct.codigo_tipocertificado;
                    item.nombre_tipocertificado = _caaAct.nombre_tipocertificado;
                    lstArchivosCAA.Add(item);
                }
                grdArchivosCAA.DataSource = lstArchivosCAA;
                grdArchivosCAA.DataBind();
            }
            else
            {
                grdArchivosCAA.DataSource = null;
                grdArchivosCAA.DataBind();
            }

        }

        //pasar a rest
        protected void btnBuscarCAA_Click(object sender, EventArgs e)
        {
            bool RecargarPagina = false;
            pnlErrorBuscarCAA.Visible = false;
            lblErrorBuscarCAA.Text = "";
            lstMensajesCAA.ClearSelection();

            int id_solicitud_caa = 0;
            string codigo_seguridad_CAA = txtCodSeguridadCAA.Text.Trim().ToUpper();
            int.TryParse(txtNroCAA.Text.Trim(), out id_solicitud_caa);

            ws_Interface_AGC servicio = new ws_Interface_AGC();
            wsResultado ws_resultado_CAA = new wsResultado();
            ParametrosBL blParam = new ParametrosBL();
            WsEscribanosActaNotarialBL blActaNotarial = new WsEscribanosActaNotarialBL();
            EncomiendaBL blEnc = new EncomiendaBL();

            servicio.Url = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC");
            string username_servicio = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.User");
            string password_servicio = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.Password");


            var lst_encomiendas = blEnc.GetByFKIdSolicitud(id_solicitud);

            var enc = lst_encomiendas.Where(x => x.IdEstado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo).OrderByDescending(x => x.IdEncomienda).FirstOrDefault();


            //Salto la validacion, si esta en desa
            //if (!Functions.isDesarrollo())
            //{
                //Valida el codigo de seguridad de la solicitud de CAA
                if (!servicio.ValidarCodigoSeguridad(username_servicio, password_servicio, id_solicitud_caa, codigo_seguridad_CAA, ref ws_resultado_CAA))
                {
                    lblErrorBuscarCAA.Text = ws_resultado_CAA.ErrorDescription;
                    pnlErrorBuscarCAA.Visible = true;
                }
                else
                {
                    //Obtiene los datos de la solicitud de CAA.
                    DtoCAA[] arrSolCAA = servicio.Get_CAAs(username_servicio, password_servicio, new int[] { id_solicitud_caa }, ref ws_resultado_CAA);
                    if (arrSolCAA.Length > 0)
                    {
                        var solCAA = arrSolCAA[0];
                        var lstMensajes = blSol.CompareWithCAA(this.id_solicitud, solCAA);

                        if (lstMensajes.Count == 0)
                        {
                            if (servicio.AsociarAnexoTecnico(username_servicio, password_servicio, id_solicitud_caa, codigo_seguridad_CAA, enc.IdEncomienda, ref ws_resultado_CAA))
                            {
                                blActaNotarial.CopiarDesdeAPRA(enc.IdEncomienda, solCAA.id_caa);
                                RecargarPagina = true;
                                //Cargar_Datos(id_solicitud);
                                //txtNroCAA.Text = "";
                                //txtCodSeguridadCAA.Text = "";
                            }
                            else
                            {
                                lblErrorBuscarCAA.Text = ws_resultado_CAA.ErrorDescription;
                                pnlErrorBuscarCAA.Visible = true;
                            }

                        }
                        else
                        {
                            lblErrorBuscarCAA.Text = "No es posible vincular la solicitud de CAA, se encontraron los siguientes inconvenientes:";
                            lstMensajesCAA.DataSource = lstMensajes;
                            lstMensajesCAA.DataBind();
                            pnlErrorBuscarCAA.Visible = true;
                        }

                    }
                    else
                    {
                        lblErrorBuscarCAA.Text = ws_resultado_CAA.ErrorDescription;
                        pnlErrorBuscarCAA.Visible = true;
                    }
                }

                servicio.Dispose();
            //}
            if (RecargarPagina)
            {
                string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
                Response.Redirect(url);
            }
            btnBuscarCAA.Enabled = true;
        }


        private async Task<List<GetCAAsByEncomiendasResponse>> GetCAAsByEncomiendas(int[] lst_id_Encomiendas)
        {
            ExternalService.ApraSrvRest apraSrvRest = new ExternalService.ApraSrvRest();
            List<GetCAAsByEncomiendasResponse> lstCaa = await apraSrvRest.GetCAAsByEncomiendas(lst_id_Encomiendas.ToList());
            return lstCaa;
        }
        private async Task<List<GetBUIsCAAResponse>> GetBUIsCAA(int id_solicitud)
        {
            ExternalService.ApraSrvRest apraSrvRest = new ExternalService.ApraSrvRest();
            List<GetBUIsCAAResponse> lstBuis = await apraSrvRest.GetBUIsCAA(id_solicitud);
            return lstBuis;
        }
        private async Task<int> GenerarCAAAutomaticos(int IdEncomienda, string codSeguridad)
        {
            ExternalService.ApraSrvRest apraSrvRest = new ExternalService.ApraSrvRest();
            int id_caa = await apraSrvRest.GenerarCAAAutomatico(IdEncomienda, codSeguridad);
            return id_caa;
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

        private bool GetCAA_fileInfo(GetCAAResponse response)
        {
            bool subioFile = false;
            ExternalService.ExternalServiceFiles files_service = new ExternalService.ExternalServiceFiles();
            string fileName = response.certificado.fileName;
            string contentType = response.certificado.contentType;
            string extension = response.certificado.extension;
            byte[] rawBytes = Convert.FromBase64String(response.certificado.rawBytes);
            int size = response.certificado.size;
            int id_tipocertificado = 1;

            switch (response.id_tipocertificado)
            {
                case 1:
                    id_tipocertificado = (int)TipoCertificadoCAA.sre;   
                    break;
                case 2:
                    id_tipocertificado = (int)TipoCertificadoCAA.sreCC;
                    break;
                case 3:
                    id_tipocertificado = (int)TipoCertificadoCAA.sc;
                    break;
                case 4:
                    id_tipocertificado = (int)TipoCertificadoCAA.sre;
                    break;
                case 5:
                    id_tipocertificado = (int)TipoCertificadoCAA.DDJJ;
                    break;
                default:
                    break;
            }

            EncomiendaBL encomiendaBL = new EncomiendaBL();
            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
            subioFile = encomiendaBL.InsertarCAA_DocAdjuntos(id_encomienda, userid, rawBytes, fileName, extension, id_tipocertificado);  
            //aca deberia volver a correr el load asi muestra el archivo
            return subioFile;
        }

        protected async void linkBtnGenerarCAA_Click(object sender, EventArgs e)
        {
            EncomiendaDTO Encomienda = encBL.Single(id_encomienda);
            string codSeguridad = Encomienda.CodigoSeguridad;
                 
            int CAA_id = GenerarCAAAutomaticos(id_encomienda, codSeguridad).Result;
            if(CAA_id != 0)
            {
                GetCAAResponse caa = await GetCAA(CAA_id);
                var fileInfo = GetCAA_fileInfo(caa); // esto no necesita retornar nada
            }
            else
            {
                //mostrar CAA viejo
            }

        }

    }


}