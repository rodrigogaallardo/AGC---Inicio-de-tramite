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
        private DtoCAA _CAA_Actual = null;

        public class ucRecargarPagosEventsArgs : EventArgs
        {
            public int id_solicitud { get; set; }
            public Constantes.PagosTipoTramite tipo_tramite { get; set; }
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
                //{
                //    btnGenerarCAA.Visible = value;
                //}

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
        public void Cargar_Datos(int id_solicitud)
        {
            pnlErrorBuscarCAA.Visible = false;
            lblErrorBuscarCAA.Text = "";

            EncomiendaBL blEnc = new EncomiendaBL();
            var lst_encomiendas = blEnc.GetByFKIdSolicitud(id_solicitud);

            Cargar_Datos(lst_encomiendas, id_solicitud);
        }
        public void Cargar_Datos(IEnumerable<EncomiendaDTO> lst_encomiendas, int id_solicitud)
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
                ws_Interface_AGC servicio = new ws_Interface_AGC();
                wsResultado ws_resultado_CAA = new wsResultado();

                ParametrosBL blParam = new ParametrosBL();
                servicio.Url = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC");
                string username_servicio = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.User");
                string password_servicio = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.Password");
                DtoCAA[] l = servicio.Get_CAAs_by_Encomiendas(username_servicio, password_servicio, lst_encomiendas.Select(x => x.IdEncomienda).ToList().ToArray(), ref ws_resultado_CAA);
                List_CAA = l.ToList();
            }
            // Establece el CAA relacionado con la encomienda 
            // ----------------------------------------------

            if (this.List_CAA != null && this.List_CAA.Count > 0 && this.List_CAA.FirstOrDefault().id_encomienda != 0)
            {

                //if (id_encomienda == 0)
                //    id_encomienda = this.List_CAA.FirstOrDefault().id_encomienda;
                this.CAA_Actual = List_CAA.Where(x => x.id_estado != (int)Constantes.CAA_EstadoSolicitud.Anulado).OrderByDescending(x => x.id_caa).FirstOrDefault();
            }

            if (this.CAA_Actual != null)
            { 
                if ((this.CAA_Actual.id_tipocertificado == (int)Constantes.CAA_TipoCertificado.SujetoaCategorizacion ||
                        this.CAA_Actual.id_tipocertificado == (int)Constantes.CAA_TipoCertificado.ConRelevanteEfecto) && this.CAA_Actual.id_estado != (int)Constantes.CAA_EstadoSolicitud.Aprobado)
                {
                    //pnlInfoSIPSA.Visible = id_solicitud > Constantes.SOLICITUDES_NUEVAS_MAYORES_A;
                    //pnlInfo.Visible = false;
                    grdArchivosCAA.Visible = false;
                }
            }
            if (lst_encomiendas.Count() > 0)
            {
                // cargar archivos CAA
                // -------------------
                var lstArchivosCAA = new List<CAA_ArchivosDTO>();

                foreach (var caa in List_CAA)
                {
                    CAA_ArchivosDTO item = new CAA_ArchivosDTO();
                    item.id_solicitud = caa.id_solicitud;
                    item.id_caa = caa.id_caa;
                    item.id_encomienda = caa.id_encomienda;
                    if (caa.id_estado == (int)Constantes.CAA_EstadoSolicitud.Aprobado)
                    {
                        if (caa.Documentos.Count() == 0)
                        {
                            // regeneración del PDF del CAA por servicio SOAP de SIPSA
                            ws_Interface_AGC servicio = new ws_Interface_AGC();
                            wsResultado ws_resultado_CAA = new wsResultado();

                            ParametrosBL blParam = new ParametrosBL();
                            servicio.Url = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC");
                            string username_servicio = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.User");
                            string password_servicio = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.Password");
                            servicio.RegenerarPDFCertificadoCAA(username_servicio, password_servicio, caa.id_solicitud, true, ref ws_resultado_CAA);
                        }
                        item.id_file = caa.Documentos[0].id_file;
                        item.url = string.Format("~/" + RouteConfig.DESCARGA_FILE + "{0}", Functions.ConvertToBase64String(item.id_file));
                        item.mostrarDoc = caa.Documentos.Count() > 0;
                    }

                    item.CreateDate = caa.CreateDate;
                    item.nombre = caa.desccorta_tipotramite;// "CAA";
                    item.id_tipocertificado = caa.id_tipocertificado;
                    item.codigo_tipocertificado = caa.cod_tipocertificado;
                    item.nombre_tipocertificado = caa.nombre_tipocertificado;
                    item.estado_caa = caa.nom_estado;
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
    }


}