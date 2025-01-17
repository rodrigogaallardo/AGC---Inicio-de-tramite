﻿using BusinesLayer.Implementation;
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
using System.Net;
using iTextSharp.text;
using ExternalService;
using Microsoft.IdentityModel.Tokens;
using System.IO;

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
            sre = 18,
            sreCC = 19,
            sc = 17,
            cre = 16,
            DDJJ = 53 
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
            var lstArchivosCAA = new List<CAA_ArchivosDTO>();
            var solBl = new SSITSolicitudesBL();
            var sol = solBl.Single(id_solicitud);
            bool CondicionExpress = false;
            updTramiteCAA.Visible = true;

            //el Btn Ingresar A SIPSA y Buscar solo se muestran cuando la sol esta en estado datos Confirmados y Observado
            if (MostrarPanelesCAA.Contains(sol.IdEstado))
            {
                
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
            
            List<int> estados = new List<int>();
            estados.Add((int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo);
            estados.Add((int)Constantes.Encomienda_Estados.Vencida);

            this.id_solicitud = id_solicitud;
            //se cambia logica para q se compare el ultimo anexo tipo "A" y posteriores
            //mantis 149128: JADHE 57846 - SSIT - No permite generar una nueva BUI

            if (lst_encomiendas.Count() > 0)
            {
                this.id_encomienda = lst_encomiendas.FirstOrDefault().IdEncomienda;
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
                var encWrap = await GetCAAsByEncomiendas(lst_encomiendas.Select(x => x.IdEncomienda).ToList());
                _lstCaa = encWrap.ListCaa;
                #endregion
            }
            // Establece el CAA relacionado con la encomienda 
            // ----------------------------------------------

            if (_lstCaa != null && _lstCaa.Count > 0)
            {
                if (_lstCaa != null && _lstCaa.Count > 0 && _lstCaa.FirstOrDefault().formulario.id_encomienda_agc != 0)
                {
                    //this.CAA_Actual = List_CAA.Where(x => x.id_estado != (int)Constantes.CAA_EstadoSolicitud.Anulado).OrderByDescending(x => x.id_caa).FirstOrDefault();
                    _caaAct = _lstCaa.Where(caa => caa.id_estado != (int)Constantes.CAA_EstadoSolicitud.Anulado).OrderByDescending(caa => caa.id_solicitud).FirstOrDefault();

                }
                else
                {
                    btnGenerarCAA.Visible = true;
                }

                if (_caaAct != null)
                {
                    
                    if ((_caaAct.id_tipocertificado == (int)Constantes.CAA_TipoCertificado.SujetoaCategorizacion ||
                         _caaAct.id_tipocertificado == (int)Constantes.CAA_TipoCertificado.ConRelevanteEfecto) &&
                         _caaAct.id_estado != (int)Constantes.CAA_EstadoSolicitud.Aprobado)
                    {
                        //pnlInfoSIPSA.Visible = id_solicitud > Constantes.SOLICITUDES_NUEVAS_MAYORES_A;
                        //pnlInfo.Visible = false;
                        grdArchivosCAA.Visible = false;
                    }
                }

                EncomiendaDTO Encomienda = encBL.Single(id_encomienda);
                
                EncomiendaDocumentosAdjuntosBL encDocBL = new EncomiendaDocumentosAdjuntosBL();
                
                int id_tipodocsis = (int)Constantes.TiposDeDocumentosSistema.CERTIFICADO_CAA;
                
                //antes deberia recorrer la lista de encomiendas y despues este foreach
                //o quiza enta bien solamente tomar la ultima aprobada
                if(_lstCaa != null && _lstCaa.Count() > 0)
                {
                    List<int> listEncomiendasCAA = _lstCaa.Select(x => x.formulario.id_encomienda_agc).ToList();
                    List<EncomiendaDocumentosAdjuntosDTO> ListDocAdj = new List<EncomiendaDocumentosAdjuntosDTO>();
                    foreach (var enc in listEncomiendasCAA)
                    {
                        var listaencCAADocs =  encDocBL.GetByFKIdEncomiendaTipoSis(enc, id_tipodocsis).ToList();
                        ListDocAdj.AddRange(listaencCAADocs);
                    }
                     //var x = encDocBL.GetByFKIdEncomiendaTipoSis(id_encomienda, id_tipodocsis).ToList();
                    foreach (var docAdj in ListDocAdj)
                    {
                        var caaActual = _lstCaa.Where(x => x.formulario.id_encomienda_agc == docAdj.id_encomienda).FirstOrDefault();
                        CAA_ArchivosDTO item = new CAA_ArchivosDTO();
                        item.id_encomienda = docAdj.id_encomienda;
                        item.id_file = docAdj.id_file;
                        item.CreateDate = docAdj.CreateDate;
                        item.url = string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Functions.ConvertToBase64String(item.id_file));
                        item.id_caa = caaActual.id_solicitud;
                        item.id_solicitud = Encomienda.IdSolicitud;
                        item.mostrarDoc = true;// antes hacia esto caa.Documentos.Count() > 0;
                        item.nombre = docAdj.nombre_archivo;
                        item.id_tipocertificado = caaActual.id_tipocertificado;
                        item.estado_caa = caaActual.estado;
                        item.codigo_tipocertificado = caaActual.codigo_tipocertificado;
                        item.nombre_tipocertificado = caaActual.nombre_tipocertificado;
                        lstArchivosCAA.Add(item);
                    }
                    //para el backlog, si no tiene archivo en nuestra base lo agrego
                    var caaBacklog = listEncomiendasCAA.Where(y => !lstArchivosCAA.Select(x => x.id_encomienda).Contains(y)).ToList();
                    if (caaBacklog != null && caaBacklog.Count() > 0)
                    {
                        bool subioAlgo = await InsertarCAA_DocAdjuntos(caaBacklog);
                        //si como minimo subio algun archivo, recargo la pagina para poder verlos
                        if (subioAlgo)
                        {
                            DivBtnSIPSAExpress.Visible = false;
                            string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
                            Response.Redirect(url);
                        }
                        else
                        {
                            var caasSinCert = _lstCaa.Where(x => caaBacklog
                                                    .Contains(x.formulario.id_encomienda_agc)).ToList();
                            foreach (var caaSinCert in caasSinCert)
                            {
                                CAA_ArchivosDTO item = new CAA_ArchivosDTO();
                                item.id_encomienda = caaSinCert.formulario.id_encomienda_agc;
                                item.CreateDate = caaSinCert.createDate;
                                item.id_caa = caaSinCert.id_solicitud;
                                item.id_solicitud = Encomienda.IdSolicitud;
                                item.mostrarDoc = false;
                                item.id_tipocertificado = caaSinCert.id_tipocertificado;
                                item.estado_caa = caaSinCert.estado;
                                item.codigo_tipocertificado = caaSinCert.codigo_tipocertificado;
                                item.nombre_tipocertificado = caaSinCert.nombre_tipocertificado;
                                lstArchivosCAA.Add(item);
                            }
                        }
                    }
                    
                }


                grdArchivosCAA.DataSource = lstArchivosCAA;
                grdArchivosCAA.DataBind();
            }
            if (_lstCaa != null)
            {
                if(lst_encomiendas.Count() < _lstCaa.Count())
                {
                    pnlBuscarCAA.Visible = true;
                    if (CondicionExpress)
                    {
                        btnGenerarCAA.Visible = true;
                        DivBtnSIPSAExpress.Visible = true;
                    }
                }
                else
                {
                    btnGenerarCAA.Visible = false;
                    DivBtnSIPSAExpress.Visible = false;
                }
            }
            else
            {
                if (CondicionExpress)
                {
                    btnGenerarCAA.Visible = true;
                    DivBtnSIPSAExpress.Visible = true;
                }
                else
                {
                    pnlBuscarCAA.Visible = true;
                }
            }
            
            if (_lstCaa != null && _lstCaa.Count() <= 0)
            {
                grdArchivosCAA.DataSource = null;
                grdArchivosCAA.DataBind();
            }
            // only for test DivBtnSIPSAExpress.Visible = true;
        }

        protected async void btnBuscarCAA_Click(object sender, EventArgs e)
        {
            bool RecargarPagina = false;
            pnlErrorBuscarCAA.Visible = false;
            lblErrorBuscarCAA.Text = "";
            lstMensajesCAA.ClearSelection();

            int id_solicitud_caa = 0;
            string codigo_seguridad_CAA = txtCodSeguridadCAA.Text.Trim().ToUpper();
            int.TryParse(txtNroCAA.Text.Trim(), out id_solicitud_caa);

            WsEscribanosActaNotarialBL blActaNotarial = new WsEscribanosActaNotarialBL();
            EncomiendaBL blEnc = new EncomiendaBL();

            var lst_encomiendas = blEnc.GetByFKIdSolicitud(id_solicitud);

            var enc = lst_encomiendas
                    .Where(x => x.IdEstado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo)
                    .OrderByDescending(x => x.IdEncomienda)
                    .FirstOrDefault();

            ValidarCodigoSeguridadResponse resCodSeg = await ValidarCodigoSeguridad(id_solicitud_caa, codigo_seguridad_CAA);
            //esto se salteaba antes en esta rama, verificar en master
            if (!resCodSeg.EsValido)
            {
                lblErrorBuscarCAA.Text = resCodSeg.ErrorDesc;
                pnlErrorBuscarCAA.Visible = true;
            }
            else
            {
                GetCAAResponse caa = await GetCAA(id_solicitud_caa);
                var lstMensajes = blSol.CompareWithCAA(this.id_solicitud, caa);
                if (lstMensajes.Count == 0)
                {
                    AsociarAnexoTecnicoResponse anexoEstado = await AsociarAnexoTecnico(id_solicitud_caa, codigo_seguridad_CAA, id_encomienda);
                    if (anexoEstado.Asociado)
                    {
                        blActaNotarial.CopiarDesdeAPRA(enc.IdEncomienda, caa.id_solicitud);
                        RecargarPagina = true;
                    }
                    else
                    {
                        lblErrorBuscarCAA.Text = anexoEstado.ErrorDesc;
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

            if (RecargarPagina)
            {
                string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
                Response.Redirect(url);
            }
            btnBuscarCAA.Enabled = true;
        }

        public async Task<bool> InsertarCAA_DocAdjuntos(List<int> encomiendas)
        {

            int CAA_id = 0;
            bool subioFile = false;
            int cantidadSubido = 0;
            ExternalServiceFiles files_service = new ExternalServiceFiles();

            GetCAAsByEncomiendasWrapResponse caaWrap = null;
            caaWrap = await GetCAAsByEncomiendas(encomiendas);

            List<GetCAAsByEncomiendasResponse> lstRCAAenc = caaWrap.ListCaa;
            if (lstRCAAenc != null && lstRCAAenc.Count > 0)
            {
                foreach (var caa_act in lstRCAAenc)
                {
                    CAA_id = caa_act.id_solicitud;
                    if (CAA_id > 0)
                    {
                        GetCAAResponse caa = null;
                        caa = await GetCAA(CAA_id);
                        
                        subioFile = GetCAA_fileInfo(caa);
                        if (subioFile)
                            cantidadSubido++;
                    }

                }
            }
            if (cantidadSubido > 0)
                subioFile = true;
            return subioFile;    //Agrego los CAA con exito (perhaps)
        }

        private async Task<GetCAAsByEncomiendasWrapResponse> GetCAAsByEncomiendas(List<int> lst_id_Encomiendas)
        {
            ExternalService.ApraSrvRest apraSrvRest = new ExternalService.ApraSrvRest();
            GetCAAsByEncomiendasWrapResponse lstCaa = await apraSrvRest.GetCAAsByEncomiendas(lst_id_Encomiendas);
            return lstCaa;
        }
        private async Task<AsociarAnexoTecnicoResponse> AsociarAnexoTecnico(int id_caa, string codigo_caa, int id_encomienda)
        {
            ExternalService.ApraSrvRest apraSrvRest = new ExternalService.ApraSrvRest();
            AsociarAnexoTecnicoResponse resultado = await apraSrvRest.AsociarAnexoTecnico(id_caa, codigo_caa, id_encomienda);
            return resultado;
        }
        private async Task<ValidarCodigoSeguridadResponse> ValidarCodigoSeguridad(int id_caa, string codigo_caa)
        {
            ExternalService.ApraSrvRest apraSrvRest = new ExternalService.ApraSrvRest();
            ValidarCodigoSeguridadResponse resultado = await apraSrvRest.ValidarCodigoSeguridad(id_caa, codigo_caa);
            return resultado;
        }
        private async Task<GetBUIsCAAResponseWrap> GetBUIsCAA(int id_solicitud)
        {
            ExternalService.ApraSrvRest apraSrvRest = new ExternalService.ApraSrvRest();
            GetBUIsCAAResponseWrap lstBuis = await apraSrvRest.GetBUIsCAA(id_solicitud);
            return lstBuis;
        }
        private async Task<GenerarCAAAutoResponse> GenerarCAAAutomaticos(int IdEncomienda, string codSeguridad)
        {
            ExternalService.ApraSrvRest apraSrvRest = new ExternalService.ApraSrvRest();
            GenerarCAAAutoResponse response_caa = await apraSrvRest.GenerarCAAAutomatico(IdEncomienda, codSeguridad);
            return response_caa;
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
            try
            {
                if(response.certificado == null)
                {
                    Exception caaExp = new Exception(
                        $"La solicitud de CAA no tiene Certificado. id_solicitud_caa : {response.id_solicitud}," +
                        $"id_encomienda : {response.formulario.id_encomienda_agc}," +
                        $"id_estado_solicitud_caa : {response.id_estado},"
                        );
                    LogError.Write(caaExp);
                    lblErrorBuscarCAA.Text = $"La solicitud de CAA {response.id_solicitud} aún no tiene Certificado. No fue generado por APRA";
                    pnlErrorBuscarCAA.Visible = true;
                    return false;
                }
                string fileName = response.certificado.fileName;
                DateTime fechaCreacionCAA = response.fechaIngreso;
                string extension = response.certificado.extension;
                byte[] rawBytes = Convert.FromBase64String(response.certificado.rawBytes);
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
                        id_tipocertificado = (int)TipoCertificadoCAA.cre;
                        break;
                    case 5:
                        id_tipocertificado = (int)TipoCertificadoCAA.DDJJ;
                        break;
                    default:
                        id_tipocertificado = (int)TipoCertificadoCAA.DDJJ;
                        break;
                }

                EncomiendaBL encomiendaBL = new EncomiendaBL();
                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                subioFile = encomiendaBL.InsertarCAA_DocAdjuntos(response.formulario.id_encomienda_agc, userid, rawBytes, fileName, extension, id_tipocertificado, fechaCreacionCAA);
                //aca deberia volver a correr el load asi muestra el archivo
                return subioFile;
            }
            catch (Exception ex)
            {
                LogError.Write(ex);
                return subioFile;
                //throw (ex); esto podia generar el loop de logs
            }
            
        }

        protected async void linkBtnGenerarCAA_Click(object sender, EventArgs e)
        {
            EncomiendaDTO Encomienda = encBL.Single(id_encomienda);
            string codSeguridad = Encomienda.CodigoSeguridad;
            int CAA_id = 0;
            pnlErrorBuscarCAA.Visible = true;
            lblErrorBuscarCAA.Text = "";
            lstMensajesCAA.ClearSelection();
            List<string> lstErr = new List<string>();
            //Antes de generar un CAA reviso si la encomienda ya tiene un CAA
            List<int> encomiendas = new List<int>();
            encomiendas.Add(id_encomienda);
            GetCAAsByEncomiendasWrapResponse caaWrap = await GetCAAsByEncomiendas(encomiendas);
            List<GetCAAsByEncomiendasResponse> lstRCAAenc = caaWrap.ListCaa;
            if (lstRCAAenc != null && lstRCAAenc.Count > 0)
            {
                CAA_id = lstRCAAenc.FirstOrDefault().id_solicitud;
            }
            else
            {
                //Si la encomienda no tiene un CAA, entonces lo creo
                GenerarCAAAutoResponse rCaa = await GenerarCAAAutomaticos(id_encomienda, codSeguridad);

                if (rCaa != null)
                {
                    if(rCaa.ErrorCode == HttpStatusCode.OK.ToString())
                        CAA_id = rCaa.id_solicitud_caa;
                    else
                    {
                        lstErr.Add(rCaa.ErrorDesc);
                        lstMensajesCAA.DataSource = lstErr;
                    }
                }
                    
            }
            
            if (CAA_id > 0)
            {
                GetCAAResponse caa = await GetCAA(CAA_id);
                var fileInfo = GetCAA_fileInfo(caa);
                if (fileInfo)
                {
                    DivBtnSIPSAExpress.Visible = false;
                    string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
                    Response.Redirect(url);
                }
                
            }
            else
            {
                //mostrar CAA viejo
                updBuscarCAA.Visible = true;
                DivBtnSIPSA.Visible = true;
                DivBtnSIPSAExpress.Visible = false;

                //lstErr.Add(rCaa.ErrorDesc);
                //lstMensajesCAA.DataSource = lstErr;
                lstMensajesCAA.DataBind();
            }
            generandoCAAgif.Style["display"] = "none";
        }

    }



}