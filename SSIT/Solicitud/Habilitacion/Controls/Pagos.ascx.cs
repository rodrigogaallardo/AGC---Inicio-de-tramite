using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Linq;
using System.Threading.Tasks;

namespace SSIT.Solicitud.Habilitacion.Controls
{
    public class ErrorEventsArgs : EventArgs
    {
        public string error { get; set; }
    }

    public partial class Pagos : System.Web.UI.UserControl
    {
        #region "Propiedades"

        public int tipo_tramite
        {
            get
            {
                int ret = 0;
                int.TryParse(hid_tipo_tramite.Value, out ret);
                return ret;
            }
            set
            {
                hid_tipo_tramite.Value = value.ToString();
            }

        }
        public int id_solicitud
        {
            get
            {
                int ret = 0;
                int.TryParse(hid_id_solicitud.Value, out ret);
                return ret;
            }
            set
            {
                hid_id_solicitud.Value = value.ToString();
            }

        }
        private int id_pago
        {
            get
            {
                int ret = 0;
                int.TryParse(hid_id_pago.Value, out ret);
                return ret;
            }
            set
            {
                hid_id_pago.Value = value.ToString();
            }

        }
        private int id_estado_pago
        {
            get
            {
                int ret = 0;
                int.TryParse(hid_estado_pago.Value, out ret);
                return ret;
            }
            set
            {
                hid_estado_pago.Value = value.ToString();
            }

        }
        public bool HabilitarGeneracionManual
        {
            get
            {
                bool ret = false;
                bool.TryParse(hid_habilitar_generacion_manual.Value, out ret);
                return ret;
            }
            set
            {
                hid_habilitar_generacion_manual.Value = value.ToString();
            }

        }
        #endregion

        #region "Eventos"

        public delegate void EventHandlerError(object sender, ErrorEventsArgs e);
        public event EventHandlerError ErrorClick;

        protected virtual void OnErrorClick(EventArgs e)
        {
            if (ErrorClick != null)
            {

                ErrorEventsArgs args = new ErrorEventsArgs();
                args.error = hid_error_message.Value;

                ErrorClick(this, args);
            }
        }
        #endregion

        public string GetEstadoPago()
        {
            return hid_estado_pago.Value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipo_tramite"></param>
        /// <param name="id_solicitud"></param>
        /// <returns></returns>
        public async Task <Constantes.BUI_EstadoPago> GetEstadoPago(Constantes.PagosTipoTramite tipo_tramite, int id_solicitud)
        {
            PagosBoletasBL pagosBoletaBL = new PagosBoletasBL();
            var ret = await pagosBoletaBL.GetEstadoPago(tipo_tramite, id_solicitud);
            hid_estado_pago.Value = ret.ToString();
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetIdPago()
        {
            int ret = 0;
            int.TryParse(hid_id_pago.Value, out ret);
            return ret;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipo_tramite"></param>
        /// <param name="id_solicitud"></param>
        /// <returns></returns>
        public async Task<int> GetPagosCount(Constantes.PagosTipoTramite tipo_tramite, int id_solicitud)
        {
            PagosBoletasBL pagosBoletaBL = new PagosBoletasBL();
            return await pagosBoletaBL.GetPagosCount(tipo_tramite, id_solicitud);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipo_tramite"></param>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public async Task<bool> HabilitarGeneracionPago(Constantes.PagosTipoTramite tipo_tramite, int IdSolicitud, IEnumerable<EncomiendaDTO> lstEncomiendas)
        {
            PagosBoletasBL pagosBoletaBL = new PagosBoletasBL();
            return await pagosBoletaBL.HabilitarGeneracionPago(tipo_tramite, IdSolicitud, lstEncomiendas);
        }

        public async Task<bool> HabilitarGeneracionPagoApra(int id_solicitud)
        {
            if (await GetPagosCount(Constantes.PagosTipoTramite.CAA, id_solicitud) == 0)
                return true;
            string estado_pago = GetEstadoPago();
            if (string.IsNullOrEmpty(estado_pago) ||
                estado_pago == Constantes.BUI_EstadoPago.Vencido.ToString() ||
                estado_pago == Constantes.BUI_EstadoPago.Anulada.ToString() ||
                estado_pago == Constantes.BUI_EstadoPago.Cancelada.ToString())
                return true;
            else
                return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="valor"></param>
        /// <param name="id_pago"></param>
        public void SetEstadoPago(string valor, int id_pago)
        {
            string estado_actual = hid_estado_pago.Value;
            if (string.IsNullOrEmpty(estado_actual))
            {
                hid_estado_pago.Value = valor;
                hid_id_pago.Value = id_pago.ToString();
            }
            else
            {
                if (estado_actual != valor && estado_actual != Constantes.BUI_EstadoPago.Pagado.ToString())
                {
                    //me pasan un valor diferente al º y el actual no es pagado.
                    //porque el estado pago no se cambia porque tiene mas prioridad que los otros

                    if (valor != Constantes.BUI_EstadoPago.Vencido.ToString())
                    {
                        hid_estado_pago.Value = valor;
                        hid_id_pago.Value = id_pago.ToString();
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipo_tramite"></param>
        /// <param name="id_solicitud"></param>
        public async Task CargarPagos(Constantes.PagosTipoTramite tipo_tramite, SSITSolicitudesDTO solicitud, IEnumerable<EncomiendaDTO> lstEncomiendas)
        {
            EncomiendaDTO encDTO = null;
            if (lstEncomiendas == null)
            {
                EncomiendaBL encBL = new EncomiendaBL();

                encDTO = encBL.GetByFKIdSolicitud(solicitud.IdSolicitud).Where(x => x.IdEstado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo)
                                                            .OrderByDescending(o => o.IdEncomienda)
                                                            .FirstOrDefault();
            }
            else
            {
                encDTO = lstEncomiendas.Where(x => x.IdEstado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo)
                                                            .OrderByDescending(o => o.IdEncomienda)
                                                            .FirstOrDefault();
            }

            PagosBoletasBL pagosBoletaBL = new PagosBoletasBL();
            var lstPagos = await pagosBoletaBL.CargarPagos(tipo_tramite, solicitud.IdSolicitud, lstEncomiendas);

            grdPagosGeneradosBUI.DataSource = lstPagos;
            grdPagosGeneradosBUI.DataBind();

            if (tipo_tramite == Constantes.PagosTipoTramite.CAA)
            {
                pnlGenerarBoletaUnica.Visible = HabilitarGeneracionManual && await HabilitarGeneracionPago(tipo_tramite, solicitud.IdSolicitud, 
                                                                                                        lstEncomiendas);
            }
            else
            {
                //optimizado para CA luego se pasa los demas
                int[] estados_consultados = new int[] { (int)Constantes.BUI_EstadoPago.Pagado, (int)Constantes.BUI_EstadoPago.SinPagar };
                bool hasMatch = estados_consultados.Any(x => lstPagos.Any(y => y.id_estado_pago == x));
                pnlGenerarBoletaUnica.Visible = HabilitarGeneracionManual && !hasMatch;

                ////0144418: JADHE 56576 - SSIT - No puede generar BUI de pago
                //if (solicitud.IdTipoTramite == (int)Constantes.TipoTramite.HABILITACION)
                //    if (solicitud.IdTipoExpediente == (int)Constantes.TipoDeExpediente.Especial)
                //        if (solicitud.IdSubTipoExpediente == (int)Constantes.SubtipoDeExpediente.HabilitacionPrevia)
                //            if (solicitud.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF)
                //                pnlGenerarBoletaUnica.Visible = true;

            }

            if (tipo_tramite == Constantes.PagosTipoTramite.HAB || tipo_tramite == Constantes.PagosTipoTramite.AMP)
            {                
                if (encDTO != null)
                {
                    #region 0139531: JADHE 53779 - SSIT - REQ - Eximir pago BUI Centro Culturales por Sociedad civil
                    /*******************************************************************************************
                                // 0139531: JADHE 53779 - SSIT - REQ - Eximir pago BUI Centro Culturales por Sociedad civil
                                // PENDIENTE DE CONFIRMACION POR FALTA DE TIPIFICADO
                                bool esSocCivil = false;
                                SSITSolicitudesTitularesPersonasJuridicasBL persJuridicas = new SSITSolicitudesTitularesPersonasJuridicasBL();
                                var solicPersJuridicas = persJuridicas.GetByFKIdSolicitud(solicitud.IdSolicitud);
                                foreach (var pj in solicPersJuridicas)
                                {
                                    if (pj.IdTipoSociedad == (int)Constantes.TipoSociedad.Sociedad_Civil)
                                    {
                                        esSocCivil = true;
                                        break;
                                    }
                                }

                                SSITDocumentosAdjuntosBL ssitDocBL = new SSITDocumentosAdjuntosBL();
                                var listDocSsit = ssitDocBL.GetByFKIdSolicitud(solicitud.IdSolicitud);

                                int ExcepcionRubro = (int)Constantes.TieneRubroConExencionPago.SinExencion;

                                var paramBL = new ParametrosBL();
                                int cotaSolic = 0;
                                if (int.TryParse(paramBL.GetParametroChar("NroSolicitudReferencia"), out cotaSolic) && solicitud.IdSolicitud > cotaSolic)
                                {
                                    RubrosCNBL rubCNBL = new RubrosCNBL();
                                    var lstRubros = rubCNBL.GetByListCodigo(encDTO.EncomiendaRubrosCNDTO.Select(s => s.CodigoRubro).ToList());

                                    bool tieneRubroProTeatro = lstRubros.Where(x => x.Codigo == Constantes.RubrosCN.Teatro_Independiente).Any();
                                    if (tieneRubroProTeatro)
                                        ExcepcionRubro = (int)Constantes.TieneRubroConExencionPago.ProTeatro;

                                    bool tieneRubroCCultural = lstRubros.Where(x => x.Codigo == Constantes.RubrosCN.Centro_Cultural_A ||
                                        x.Codigo == Constantes.RubrosCN.Centro_Cultural_B ||
                                        x.Codigo == Constantes.RubrosCN.Centro_Cultural_C).Any();
                                    if (tieneRubroCCultural) // && esSocCivil (Pendiente de confirmacion)
                                        ExcepcionRubro = (int)Constantes.TieneRubroConExencionPago.CentroCultural;
                                }
                                else
                                {
                                    RubrosBL rubBL = new RubrosBL();
                                    var lstRubros = rubBL.GetByListCodigo(encDTO.EncomiendaRubrosDTO.Select(s => s.CodigoRubro).ToList());

                                    bool tieneRubroProTeatro = lstRubros.Where(x => x.EsProTeatro).Any();
                                    if (tieneRubroProTeatro)
                                        ExcepcionRubro = (int)Constantes.TieneRubroConExencionPago.ProTeatro;

                                    bool tieneRubroEstadio = lstRubros.Where(x => x.EsEstadio).Any();
                                    if (tieneRubroEstadio)
                                        ExcepcionRubro = (int)Constantes.TieneRubroConExencionPago.Estadio;

                                    bool tieneRubroCCultural = lstRubros.Where(x => x.EsCentroCultural).Any();
                                    if (tieneRubroCCultural) // && esSocCivil (Pendiente de confirmacion)
                                        ExcepcionRubro = (int)Constantes.TieneRubroConExencionPago.CentroCultural;
                                }

                                //Valido que sea una ECI
                                if (encDTO.esECI)
                                {
                                    ExcepcionRubro = (int)Constantes.TieneRubroConExencionPago.EsECI; ;
                                }

                                switch (ExcepcionRubro)
                                {
                                    case (int)Constantes.TieneRubroConExencionPago.ProTeatro:
                                        bool tieneDocProTeatro = listDocSsit.Where(x => x.id_tdocreq == (int)Constantes.TipoDocumentoRequerido.CertificadoProTeatro).Any();
                                        if (tieneDocProTeatro)
                                            pnlGenerarBoletaUnica.Visible = false;
                                        break;
                                    case (int)Constantes.TieneRubroConExencionPago.Estadio:
                                        bool tieneChkEstadio = solicitud.ExencionPago;
                                        if (tieneChkEstadio)
                                            pnlGenerarBoletaUnica.Visible = false;
                                        break;
                                    case (int)Constantes.TieneRubroConExencionPago.CentroCultural:
                                        bool tieneDocCCultural = listDocSsit.Where(x => x.id_tdocreq == (int)Constantes.TipoDocumentoRequerido.ConstanciaInicioTramiteIGJoINAES).Any();
                                        if (tieneDocCCultural)
                                            pnlGenerarBoletaUnica.Visible = false;
                                        break;
                                    case (int)Constantes.TieneRubroConExencionPago.EsECI:
                                        //Valido que solo tenga ese rubro para que sea una excepcion de pago
                                        if (encDTO.EncomiendaRubrosCNDTO.Count == 1)
                                            pnlGenerarBoletaUnica.Visible = false;
                                        break;
                                    default:
                                        break;
                                }

                                ***********************************************************************************************/
                    #endregion

                    RubrosBL rubBL = new RubrosBL();
                    SSITDocumentosAdjuntosBL ssitDocBL = new SSITDocumentosAdjuntosBL();

                    var lstRubros = rubBL.GetByListCodigo(encDTO.EncomiendaRubrosDTO.Select(s => s.CodigoRubro).ToList());
                    var listDocSsit = ssitDocBL.GetByFKIdSolicitud(solicitud.IdSolicitud);

                    int ExcepcionRubro = (int)Constantes.TieneRubroConExencionPago.SinExencion;

                    bool tieneRubroProTeatro = lstRubros.Where(x => x.EsProTeatro).Any();
                    if (tieneRubroProTeatro)
                        ExcepcionRubro = (int)Constantes.TieneRubroConExencionPago.ProTeatro;

                    bool tieneRubroEstadio = lstRubros.Where(x => x.EsEstadio).Any();
                    if (tieneRubroEstadio)
                        ExcepcionRubro = (int)Constantes.TieneRubroConExencionPago.Estadio;

                    bool tieneRubroCCultural = lstRubros.Where(x => x.EsCentroCultural).Any();
                    if (tieneRubroCCultural)
                        ExcepcionRubro = (int)Constantes.TieneRubroConExencionPago.CentroCultural;

                    //Valido que sea una ECI
                    bool esEci = (encDTO != null && encDTO.EsECI && solicitud != null && solicitud.EsECI);
                    if (esEci)
                    {
                        ExcepcionRubro = (int)Constantes.TieneRubroConExencionPago.EsECI; ;
                    }

                    switch (ExcepcionRubro)
                    {
                        case (int)Constantes.TieneRubroConExencionPago.ProTeatro:
                            bool tieneDocProTeatro = listDocSsit.Where(x => x.id_tdocreq == (int)Constantes.TipoDocumentoRequerido.CertificadoProTeatro).Any();
                            if (tieneDocProTeatro)
                                pnlGenerarBoletaUnica.Visible = false;
                            break;
                        case (int)Constantes.TieneRubroConExencionPago.Estadio:
                            bool tieneChkEstadio = solicitud.ExencionPago;
                            if (tieneChkEstadio)
                                pnlGenerarBoletaUnica.Visible = false;
                            break;
                        case (int)Constantes.TieneRubroConExencionPago.CentroCultural:
                            bool tieneDocCCultural = listDocSsit.Where(x => x.id_tdocreq == (int)Constantes.TipoDocumentoRequerido.ConstanciaInicioTramiteIGJoINAES).Any();
                            if (tieneDocCCultural)
                                pnlGenerarBoletaUnica.Visible = false;
                            break;
                        case (int)Constantes.TieneRubroConExencionPago.EsECI:
                            //Valido que solo tenga ese rubro para que sea una excepcion de pago
                            if (encDTO.EncomiendaRubrosCNDTO.Count == 1 || encDTO.IdTipoTramite == (int)Constantes.TipoTramite.HabilitacionECIAdecuacion)
                                pnlGenerarBoletaUnica.Visible = false;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void grdPagosGeneradosBUI_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                clsItemGrillaPagos rowItem = (clsItemGrillaPagos)e.Row.DataItem;
                Label lblDescripicionEstadoPago = (Label)e.Row.FindControl("lblDescripicionEstadoPago");

                HyperLink lnkImprimirBoletaUnica = (HyperLink)e.Row.FindControl("lnkImprimirBoletaUnica");
                lnkImprimirBoletaUnica.NavigateUrl = string.Format("~/" + RouteConfig.IMPRIMIR_BOLETA + "{0}", rowItem.id_pago);

                if (rowItem.id_estado_pago != (int)Constantes.BUI_EstadoPago.Pagado && rowItem.id_estado_pago != (int)Constantes.BUI_EstadoPago.SinPagar)
                    lnkImprimirBoletaUnica.Visible = false;

                lblDescripicionEstadoPago.Text = rowItem.desc_estado_pago;
                SetEstadoPago(rowItem.desc_estado_pago, rowItem.id_pago);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipo_tramite"></param>
        /// <param name="id_pago"></param>
        /// <returns></returns>
        public string ConsultarEstadoPago(Constantes.PagosTipoTramite tipo_tramite, int id_pago)
        {
            PagosBoletasBL pagosBl = new PagosBoletasBL();
            string strEstadoPago = pagosBl.ConsultarEstadoPago(tipo_tramite, id_pago);
            return strEstadoPago;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipo_tramite"></param>
        /// <param name="id_solicitud"></param>
        public async void GenerarBoletaUnica(Constantes.PagosTipoTramite tipo_tramite, int id_solicitud)
        {
            PagosBoletasBL pagosBl = new PagosBoletasBL();
            await pagosBl.GenerarBoletaUnica(tipo_tramite, id_solicitud);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkGenerarBoletaUnica_Click(object sender, EventArgs e)
        {
            try
            {
                SSITSolicitudesBL solicitudesBL = new SSITSolicitudesBL();
                if (this.tipo_tramite == (int)Constantes.PagosTipoTramite.HAB ||
                    this.tipo_tramite == (int)Constantes.PagosTipoTramite.AMP)
                {
                    GenerarBoletaUnica((Constantes.PagosTipoTramite)tipo_tramite, this.id_solicitud);
                    
                    Task.Run(async () =>
                    {
                        await CargarPagos((Constantes.PagosTipoTramite)tipo_tramite, solicitudesBL.Single(id_solicitud), null);
                    }).Wait();
                }
                if (this.tipo_tramite == (int)Constantes.PagosTipoTramite.CAA)
                {
                    GenerarBoletaUnica(Constantes.PagosTipoTramite.CAA, this.id_solicitud);
                    
                    Task.Run(async () =>
                    {
                        await CargarPagos(Constantes.PagosTipoTramite.CAA, solicitudesBL.Single(id_solicitud), null);
                    }).Wait();
                }
                if (this.tipo_tramite == (int)Constantes.PagosTipoTramite.TR)
                {
                    GenerarBoletaUnica(Constantes.PagosTipoTramite.TR, this.id_solicitud);
                    
                    Task.Run(async () =>
                    {
                        await CargarPagos(Constantes.PagosTipoTramite.TR, solicitudesBL.Single(id_solicitud), null);
                    }).Wait();
                }

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                hid_error_message.Value = ex.Message;
                OnErrorClick(e);
            }
        }
    }
}