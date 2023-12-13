using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace SSIT.Solicitud.Transferencia.Controls
{
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
        public async Task<Constantes.BUI_EstadoPago> GetEstadoPago(Constantes.PagosTipoTramite tipo_tramite, int id_solicitud)
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
        public async Task<bool> HabilitarGeneracionPago(Constantes.PagosTipoTramite tipo_tramite, int IdSolicitud, List<EncomiendaDTO> lstEncomiendas)
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
        public async Task CargarPagos(Constantes.PagosTipoTramite tipo_tramite, int id_solicitud)
        {
            bool habilitarGeneracionPago = await HabilitarGeneracionPago(tipo_tramite, id_solicitud, null);

            pnlGenerarBoletaUnica.Visible = HabilitarGeneracionManual && habilitarGeneracionPago;

            PagosBoletasBL pagosBoletaBL = new PagosBoletasBL();
            var lstPagos = await pagosBoletaBL.CargarPagos(tipo_tramite, id_solicitud, null);

            grdPagosGeneradosBUI.DataSource = lstPagos;
            grdPagosGeneradosBUI.DataBind();

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

                string estado = ConsultarEstadoPago((Constantes.PagosTipoTramite)this.tipo_tramite, rowItem.id_pago);

                HyperLink lnkImprimirBoletaUnica = (HyperLink)e.Row.FindControl("lnkImprimirBoletaUnica");
                lnkImprimirBoletaUnica.NavigateUrl = string.Format("~/" + RouteConfig.IMPRIMIR_BOLETA + "{0}", rowItem.id_pago);

                if (estado != Constantes.BUI_EstadoPago.Pagado.ToString() && estado != Constantes.BUI_EstadoPago.SinPagar.ToString())
                    lnkImprimirBoletaUnica.Visible = false;

                lblDescripicionEstadoPago.Text = estado;
                SetEstadoPago(estado, rowItem.id_pago);
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
        public async Task GenerarBoletaUnica(Constantes.PagosTipoTramite tipo_tramite, int id_solicitud)
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
                if (this.tipo_tramite == (int)Constantes.PagosTipoTramite.HAB)
                {
                    Task.Run(async () =>
                    {
                        await GenerarBoletaUnica(Constantes.PagosTipoTramite.HAB, this.id_solicitud);
                    }).Wait();
                    Task.Run(async () =>
                    {
                        await CargarPagos(Constantes.PagosTipoTramite.HAB, this.id_solicitud);
                    }).Wait();
                    
                }
                if (this.tipo_tramite == (int)Constantes.PagosTipoTramite.CAA)
                {
                    Task.Run(async () =>
                    {
                        await GenerarBoletaUnica(Constantes.PagosTipoTramite.CAA, this.id_solicitud);
                    }).Wait();
                    Task.Run(async () =>
                    {
                        await CargarPagos(Constantes.PagosTipoTramite.CAA, this.id_solicitud);
                    }).Wait();
                }
                if (this.tipo_tramite == (int)Constantes.PagosTipoTramite.TR)
                {
                    Task.Run(async () =>
                    {
                        await GenerarBoletaUnica(Constantes.PagosTipoTramite.TR, this.id_solicitud);
                    }).Wait();
                    
                    Task.Run(async () =>
                    {
                        await CargarPagos(Constantes.PagosTipoTramite.TR, this.id_solicitud);
                    }).Wait();
                }

            }
            catch (Exception ex)
            {
                LogError.Write(ex);
            }
        }
    }
}