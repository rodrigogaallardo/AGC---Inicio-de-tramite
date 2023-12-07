using BusinesLayer.Implementation;
using DataTransferObject;
using ExternalService.ws_interface_AGC;
using SSIT.Common;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Solicitud.Habilitacion.Controls
{

    //public class ErrorEventsArgs : EventArgs
    //{
    //    public string error { get; set; }
    //}

    public partial class PagosSolicitud : System.Web.UI.UserControl
    {
        #region "Eventos"

        public delegate void EventHandlerError(object sender, SSIT.Solicitud.Habilitacion.Controls.ErrorEventsArgs e);
        public event EventHandlerError PSErrorClick;

        protected virtual void OnPSErrorClick(SSIT.Solicitud.Habilitacion.Controls.ErrorEventsArgs e)
        {
            if (PSErrorClick != null)
            {

                SSIT.Solicitud.Habilitacion.Controls.ErrorEventsArgs args = new SSIT.Solicitud.Habilitacion.Controls.ErrorEventsArgs();
                args.error = e.error;

                PSErrorClick(this, args);
            }
        }
        #endregion

        public bool Enabled
        {
            get
            {
                return (ViewState["_Enabled"] != null ? Convert.ToBoolean(ViewState["_Enabled"]) : true);
            }
            set
            {
                ViewState["_Enabled"] = value;
                //if (btnGenerarCAA.Visible)
                //    btnGenerarCAA.Visible = value;
            }
        }

        public async Task Cargar_Datos(SSITSolicitudesDTO sol, DtoCAA CAA_Actual, IEnumerable<EncomiendaDTO> lstEncomiendas)
        {
            //Condiciones para Habilitacion de boton de pago //

            //1. Debe estar en datos confirmados
            //2. Debe tener al menos un AT en estado aprobado por el consejo
            //3. No debe tener diferencias entre la solicitud y el at
            //4. Debe tener al menos un CAA aprobado o eximido.

            vis_Pagos_AGC.tipo_tramite = (int)Constantes.PagosTipoTramite.HAB;
            vis_Pagos_APRA.tipo_tramite = (int)Constantes.PagosTipoTramite.CAA;

            vis_Pagos_AGC.HabilitarGeneracionManual = false;

            bool boolEstado = sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF
                || sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.PENPAG
                || sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO;

            bool boolATCurso = !lstEncomiendas.Where(x => x.IdEstado == (int)Constantes.Encomienda_Estados.Incompleta
                    || x.IdEstado == (int)Constantes.Encomienda_Estados.Completa
                    || x.IdEstado == (int)Constantes.Encomienda_Estados.Confirmada
                    || x.IdEstado == (int)Constantes.Encomienda_Estados.Ingresada_al_consejo).Any();

            bool boolATApro = lstEncomiendas.Where(x => (x.IdEstado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo 
                || x.IdEstado == (int)Constantes.Encomienda_Estados.Vencida)
                && x.tipoAnexo == Constantes.TipoAnexo_A).Any();

            var listEncAprob = lstEncomiendas
                      .Where(x => x.IdEstado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo
                               || x.IdEstado == (int)Constantes.Encomienda_Estados.Vencida)
                      .ToList();

            var Ultima_enc_A = listEncAprob.Where(x => x.tipoAnexo == Constantes.TipoAnexo_A)
                                            .OrderByDescending(o => o.IdEncomienda)
                                            .FirstOrDefault();

            if (Ultima_enc_A != null)
            {
                if (Ultima_enc_A.IdTipoTramite == (int)Constantes.TipoTramite.AMPLIACION)
                {
                    vis_Pagos_AGC.tipo_tramite = (int)Constantes.PagosTipoTramite.AMP;
                }
            }

            bool boolTieneCAA = CAA_Actual != null;

            SSITSolicitudesBL blSol = new SSITSolicitudesBL();
            bool boolEximidoCAA = blSol.EximidoCAA(sol.IdSolicitud);

            bool boolTieneCAAoEstaEximidoCAA = boolTieneCAA || boolEximidoCAA;

            if (CAA_Actual != null)
            {
                if (CAA_Actual.id_tipotramite == (int)Constantes.TiposDeTramiteCAA.CAA_ESP)
                {
                    vis_Pagos_APRA.HabilitarGeneracionManual = false;
                } 
            }

            DateTime dt = DateTime.Now;
            bool boolCompare = blSol.CompareWithEncomienda(sol.IdSolicitud);
            System.Diagnostics.Debug.Write("CompareWithEncomienda " + (DateTime.Now - dt).Milliseconds.ToString() + Environment.NewLine);

            if (sol.IdTipoTramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
            {
                vis_Pagos_AGC.HabilitarGeneracionManual = boolEstado && boolATCurso && boolATApro && boolCompare;
            }
            else
            {
                vis_Pagos_AGC.HabilitarGeneracionManual = boolEstado && boolATCurso && boolATApro && boolCompare && boolTieneCAAoEstaEximidoCAA;
            }
            
            vis_Pagos_AGC.id_solicitud = sol.IdSolicitud;
            await vis_Pagos_AGC.CargarPagos((Constantes.PagosTipoTramite)vis_Pagos_AGC.tipo_tramite, sol, lstEncomiendas);
            
            vis_Pagos_APRA.id_solicitud = sol.IdSolicitud;
            await vis_Pagos_APRA.CargarPagos(Constantes.PagosTipoTramite.CAA, sol, lstEncomiendas);
        }

        public Pagos getVis_Pagos_AGC()
        {
            return vis_Pagos_AGC;
        }
        public Pagos getVis_Pagos_APRA()
        {
            return vis_Pagos_APRA;
        }

        public void labelProteatro(bool chequeado)
        {
            if (chequeado)
            {
                ScriptManager.RegisterClientScriptBlock(udpLabelProteatro, udpLabelProteatro.GetType(), "mostrarLabelProTeatro", "mostrarLabelProTeatro();", true);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(udpLabelProteatro, udpLabelProteatro.GetType(), "ocultarLabelProTeatro", "ocultarLabelProTeatro();", true);

            }
            udpLabelProteatro.Update();
        }
        public void OcultarPnlHabViejas()
        {
            pnlAGC.Visible = false;
        }
        public async Task RecargarPagos(Constantes.PagosTipoTramite TipoTramite, int IdSolicitud)
        {
            SSITSolicitudesBL solicitudesBL = new SSITSolicitudesBL();
            if (TipoTramite == Constantes.PagosTipoTramite.CAA)
            {

                await vis_Pagos_APRA.CargarPagos(TipoTramite, solicitudesBL.Single(IdSolicitud), null);
            }
            if (TipoTramite == Constantes.PagosTipoTramite.HAB || TipoTramite == Constantes.PagosTipoTramite.AMP)
            {
                await vis_Pagos_AGC.CargarPagos(TipoTramite, solicitudesBL.Single(IdSolicitud), null);
            }
        }

        public async Task ProcesarBoletaUnicaAGC(SSITSolicitudesDTO sol)
        {
            Guid userId = Functions.GetUserid();
            EncomiendaBL encomiendaBL = new EncomiendaBL();
            IEnumerable<EncomiendaDTO> lstEncomiendas = encomiendaBL.GetByFKIdSolicitud(sol.IdSolicitud);
            var enc = lstEncomiendas.Where(x => x.IdEstado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo)
                .OrderByDescending(x => x.IdEncomienda).FirstOrDefault();

            Constantes.PagosTipoTramite tipo_tramite = (Constantes.PagosTipoTramite)enc.IdTipoTramite;

            //Cuando la solicitud está pendiente de Pago y no se genenró nunca una boleta, entonces se debe generar la BUI de AGC
            if (sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.PENPAG && await getVis_Pagos_AGC().HabilitarGeneracionPago(tipo_tramite, sol.IdSolicitud, lstEncomiendas)
                    && await getVis_Pagos_AGC().GetPagosCount(tipo_tramite, sol.IdSolicitud) == 0)
            {
                getVis_Pagos_AGC().GenerarBoletaUnica(tipo_tramite, sol.IdSolicitud);
                await getVis_Pagos_AGC().CargarPagos(tipo_tramite, sol, null);
            }

        }


        protected void PagosError_Click(object sender, SSIT.Solicitud.Habilitacion.Controls.ErrorEventsArgs e)
        {
            OnPSErrorClick(e);
        }
    }
}