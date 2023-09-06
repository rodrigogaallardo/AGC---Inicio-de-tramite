using BusinesLayer.Implementation;
using DataTransferObject;
using ExternalService;
using ExternalService.ws_interface_AGC;
using SSIT.App_Components;
using SSIT.Common;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using static StaticClass.Constantes;
using Org.BouncyCastle.Utilities;
using DataAcess;
using ExternalService.Class.Express;
using System.Threading.Tasks;

namespace SSIT
{
    public partial class VisorTramite : SecurePage
    {


        private int id_solicitud
        {
            get
            {
                int ret = 0;
                int.TryParse(Convert.ToString(Page.RouteData.Values["id_solicitud"]), out ret);
                return ret;
            }
            set
            {
                hid_id_solicitud.Value = value.ToString();
            }

        }
        private int id_estado
        {
            get
            {
                int ret = 0;
                int.TryParse(hid_id_estado.Value, out ret);
                return ret;
            }
            set
            {
                hid_id_estado.Value = value.ToString();
            }

        }
        private bool solicitudNueva
        {
            get
            {
                int ret = 0;
                int.TryParse(hid_id_solicitud.Value, out ret);
                return ret > Constantes.SOLICITUDES_NUEVAS_MAYORES_A;
            }
        }

        private string mensajeAlertaPagos = "";

        protected void Page_Load(object sender, EventArgs e)
        {
           

            ScriptManager sm = ScriptManager.GetCurrent(this);
            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(updCargarDatos, updCargarDatos.GetType(), "init_Js_updCargarDatos", 
                    "init_Js_updCargarDatos();", true);
            }
            int nroSolReferencia = 0;
            int.TryParse(ConfigurationManager.AppSettings["NroSolicitudReferencia"], out nroSolReferencia);
            if (id_solicitud <= nroSolReferencia)
                divbtnImprimirSolicitud.Visible = false;

            string tieneOblea = visDocumentos.tieneOblea(id_solicitud);
            divbtnOblea.Visible = false;

            //si boleta cero esta activa, oculto panel de pagos
            SSITSolicitudesBL _blSol = new SSITSolicitudesBL();
            var _sol = _blSol.Single(id_solicitud);
            if (BoletaCeroActiva())
            {
                liBui.Visible = false;
                visPagosSolicitud.Visible = false;
            }

            if (tieneOblea != "")
            {
                btnOblea.NavigateUrl = tieneOblea;
                divbtnOblea.Visible = true;
                if (id_solicitud <= nroSolReferencia)
                    divbtnImprimirSolicitud.Visible = false;
            }
            #region ASOSA ASYNC
            //GetBUIsCAA(200001);
            //List<GetCAAsByEncomiendasResponse> l2 = await GetBUIsCAA(200001);
            #endregion
        }

        private   void ActualizarEstadoPenPagEnTramite(ref SSITSolicitudesDTO sol, IEnumerable<EncomiendaDTO> lstEncDTO)
        {
            int id_solicitud = sol.IdSolicitud;
            if (sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.PENPAG)
            {
                ParametrosBL blParam = new ParametrosBL();

                bool estadoPagoAGC = visPagosSolicitud.getVis_Pagos_AGC().GetEstadoPago(Constantes.PagosTipoTramite.HAB, id_solicitud) == Constantes.BUI_EstadoPago.Pagado;

                if (!estadoPagoAGC)
                {
                    var encDTO = lstEncDTO.Where(x => x.IdEstado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo)
                                            .OrderByDescending(o => o.IdEncomienda)
                                            .FirstOrDefault();
                    if (encDTO != null)
                    {
                        /*******************************************************************************************
                    // 0139531: JADHE 53779 - SSIT - REQ - Eximir pago BUI Centro Culturales por Sociedad civil
                    // PENDIENTE DE CONFIRMACION POR FALTA DE TIPIFICADO

                        SSITDocumentosAdjuntosBL ssitDocBL = new SSITDocumentosAdjuntosBL();
                        var listDocSsit = ssitDocBL.GetByFKIdSolicitud(sol.IdSolicitud);

                        int ExcepcionRubro = (int)Constantes.TieneRubroConExencionPago.SinExencion;

                        bool esSocCivil = false;
                        SSITSolicitudesTitularesPersonasJuridicasBL persJuridicas = new SSITSolicitudesTitularesPersonasJuridicasBL();
                        var solicPersJuridicas = persJuridicas.GetByFKIdSolicitud(encDTO.IdSolicitud);
                        foreach (var pj in solicPersJuridicas)
                        {
                            if (pj.IdTipoSociedad == (int)Constantes.TipoSociedad.Sociedad_Civil)
                            {
                                esSocCivil = true;
                                break;
                            }
                        }
                        var paramBL = new ParametrosBL();
                        int cotaSolic = 0;
                        if (int.TryParse(paramBL.GetParametroChar("NroSolicitudReferencia"), out cotaSolic) && sol.IdSolicitud > cotaSolic)
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
                                    estadoPagoAGC = true;
                                break;
                            case (int)Constantes.TieneRubroConExencionPago.Estadio:
                                bool tieneChkEstadio = sol.ExencionPago;
                                if (tieneChkEstadio)
                                    estadoPagoAGC = true;
                                break;
                            case (int)Constantes.TieneRubroConExencionPago.CentroCultural:
                                bool tieneDocCCultural = listDocSsit.Where(x => x.id_tdocreq == (int)Constantes.TipoDocumentoRequerido.ConstanciaInicioTramiteIGJoINAES).Any();
                                if (tieneDocCCultural)
                                    estadoPagoAGC = true;
                                break;
                            case (int)Constantes.TieneRubroConExencionPago.EsECI:
                                //Valido que solo tenga ese rubro para que sea una excepcion de pago
                                if (encDTO.EncomiendaRubrosCNDTO.Count == 1)
                                    estadoPagoAGC = true;
                                break;

                            default:
                                break;
                        }

                        ***********************************************************************************************/

                        RubrosBL rubBL = new RubrosBL();
                        SSITDocumentosAdjuntosBL ssitDocBL = new SSITDocumentosAdjuntosBL();

                        var lstRubros = rubBL.GetByListCodigo(encDTO.EncomiendaRubrosDTO.Select(s => s.CodigoRubro).ToList());
                        var listDocSsit = ssitDocBL.GetByFKIdSolicitud(sol.IdSolicitud);

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

                        //Valido que sea una ECI ya se a origen de la encomienda o de la solicitud
                        if (encDTO.EsECI && sol.EsECI)
                        {
                            ExcepcionRubro = (int)Constantes.TieneRubroConExencionPago.EsECI; ;
                        }

                        switch (ExcepcionRubro)
                        {
                            case (int)Constantes.TieneRubroConExencionPago.ProTeatro:
                                bool tieneDocProTeatro = listDocSsit.Where(x => x.id_tdocreq == (int)Constantes.TipoDocumentoRequerido.CertificadoProTeatro).Any();
                                if (tieneDocProTeatro)
                                    estadoPagoAGC = true;
                                break;
                            case (int)Constantes.TieneRubroConExencionPago.Estadio:
                                bool tieneChkEstadio = sol.ExencionPago;
                                if (tieneChkEstadio)
                                    estadoPagoAGC = true;
                                break;
                            case (int)Constantes.TieneRubroConExencionPago.CentroCultural:
                                bool tieneDocCCultural = listDocSsit.Where(x => x.id_tdocreq == (int)Constantes.TipoDocumentoRequerido.ConstanciaInicioTramiteIGJoINAES).Any();
                                if (tieneDocCCultural)
                                    estadoPagoAGC = true;
                                break;
                            case (int)Constantes.TieneRubroConExencionPago.EsECI:
                                //Valido que solo tenga ese rubro para que sea una excepcion de pago
                                if (encDTO.EncomiendaRubrosCNDTO.Count == 1 || encDTO.IdTipoTramite == (int)Constantes.TipoTramite.HabilitacionECIAdecuacion)
                                    estadoPagoAGC = true;
                                break;

                            default:
                                break;
                        }
                    }
                }

                bool estadoPagoCAA = false;

                if (lstEncDTO.Count() > 0)
                {
                    int[] lst_id_Encomiendas = lstEncDTO.Select(s => s.IdEncomienda).ToArray();
                    ws_Interface_AGC servicio = new ws_Interface_AGC();
                    ExternalService.ws_interface_AGC.wsResultado ws_resultado_CAA = new ExternalService.ws_interface_AGC.wsResultado();

                    servicio.Url = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC");
                    string username_servicio = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.User");
                    string password_servicio = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.Password");
                    DtoCAA[] l = servicio.Get_CAAs_by_Encomiendas(username_servicio, password_servicio, lst_id_Encomiendas.ToArray(), ref ws_resultado_CAA);



                    #region ASOSA ASYNC
                    GetBUIsCAA(200001);
                    //List<GetCAAsByEncomiendasResponse> l2 = await  GetCAAsByEncomiendas(lst_id_Encomiendas);
                    #endregion

                    var caaActual = l.Where(x => x.id_estado == (int)Constantes.CAA_EstadoSolicitud.Aprobado).OrderByDescending(o => o.id_caa).FirstOrDefault();

                    if (caaActual != null)
                        estadoPagoCAA = servicio.Get_BUIs_CAA(username_servicio, password_servicio, caaActual.id_caa, ref ws_resultado_CAA).Where(x => x.EstadoId == (int)Constantes.BUI_EstadoPago.Pagado).Any();
                }

                if (estadoPagoAGC && estadoPagoCAA)
                {
                    SSITSolicitudesBL ssitBL = new SSITSolicitudesBL();
                    //if (id_solicitud <= Constantes.SOLICITUDES_NUEVAS_MAYORES_A)
                    //    sol.IdEstado = (int)Constantes.TipoEstadoSolicitudEnum.ETRA;
                    //else
                    sol.IdEstado = (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF;

                    sol.LastUpdateDate = DateTime.Now;
                    sol.LastUpdateUser = (Guid)Membership.GetUser().ProviderUserKey;
                    ssitBL.Update(sol);
                    sol = ssitBL.Single(id_solicitud);
                }
                else
                    this.MostrarMensajeAlertas("Debera tener una boleta de AGC abonada y la boleta del ultimo Certificado de Aptitud Ambiental (CAA) abonada.");
            }
        }

        #region ASOSA ASYNC
        private async Task GetCAAsByEncomiendas(int[] lst_id_Encomiendas)
        {
            ExternalService.ApraSrvRest apraSrvRest = new ExternalService.ApraSrvRest();
            List<GetCAAsByEncomiendasResponse> l2 = await apraSrvRest.GetCAAsByEncomiendas(lst_id_Encomiendas.ToList());

        }
        private async Task GetBUIsCAA(int id_solicitud)
        {
            ExternalService.ApraSrvRest apraSrvRest = new ExternalService.ApraSrvRest();
            List<GetBUIsCAAResponse> l2 = await apraSrvRest.GetBUIsCAA(id_solicitud);
           
        }
        #endregion
        private void CargarDatos(SSITSolicitudesDTO sol)
        {
       
            EncomiendaBL blEnc = new EncomiendaBL();
            var lstEnc = blEnc.GetByFKIdSolicitud(id_solicitud);
            CargarDatos(sol, lstEnc);
        }
        protected void btnCargarDatostramite_Click(object sender, EventArgs e)
        {
            #region ASOSA ASYNC

            RegisterAsyncTask(new PageAsyncTask(() => GetBUIsCAA(200001)));
            #endregion
            try
            {
                this.id_solicitud = id_solicitud;
                DateTime dt = DateTime.Now;

                SSITSolicitudesBL blSol = new SSITSolicitudesBL();
                var sol = blSol.Single(id_solicitud);
                ComprobarSolicitud(sol);

                string msgError = blSol.ActualizarEstado(id_solicitud, (Guid)Membership.GetUser().ProviderUserKey);
                System.Diagnostics.Debug.Write("ActualizarEstadoCompleto" + (DateTime.Now - dt).Milliseconds.ToString() + Environment.NewLine);

                sol = blSol.Single(id_solicitud);

                dt = DateTime.Now;
                EncomiendaBL blEnc = new EncomiendaBL();
                System.Diagnostics.Debug.Write("instancia BL: " + (DateTime.Now - dt).Milliseconds.ToString() + Environment.NewLine);
                dt = DateTime.Now;
                var lstEnc = blEnc.GetByFKIdSolicitud(id_solicitud);
                System.Diagnostics.Debug.Write("encomiendas: " + (DateTime.Now - dt).Milliseconds.ToString() + Environment.NewLine);

                dt = DateTime.Now;
                ActualizarEstadoPenPagEnTramite(ref sol, lstEnc);
                System.Diagnostics.Debug.Write("ActualizarEstadoPenPagEnTramite" + (DateTime.Now - dt).Milliseconds.ToString() + Environment.NewLine);

                CargarDatos(sol, lstEnc);

                ScriptManager.RegisterStartupScript(updCargarDatos, updCargarDatos.GetType(), "finalizarCarga", "finalizarCarga();", true);
                if (sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.ANU)
                    lblAlertasSolicitud.Text = "Esta solicitud se encuentra anulada.";

                if (msgError != null || (lblAlertasSolicitud.Text != null && lblAlertasSolicitud.Text != ""))
                {
                    lblError.Text = msgError;
                    if (msgError != null && (lblAlertasSolicitud.Text != null && lblAlertasSolicitud.Text != ""))
                        this.MostrarMensajeAlertas(lblAlertasSolicitud.Text, msgError);
                    else if (msgError != null && msgError != "")
                        this.MostrarMensajeAlertas(msgError);
                    else if (lblAlertasSolicitud.Text != null && lblAlertasSolicitud.Text != "")
                        this.MostrarMensajeAlertas(lblAlertasSolicitud.Text);
                    updEstadoSolicitud.Update();
                }

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                divbtnConfirmarTramite.Visible = false; //ante cualquier error no permitir confirmar
                divbtnImprimirSolicitud.Visible = false;
                divbtnOblea.Visible = false;
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updCargarDatos, "finalizarCarga();showfrmError();");
            }
        }

        private void ComprobarSolicitud(SSITSolicitudesDTO solicitud)
        {
            if (Page.RouteData.Values["id_solicitud"] != null)
            {
                if (solicitud != null)
                {
                    /*Falta el userID y hacer overload de getuserid con el tipo de tramite*/
                    Guid userid_solicitud = (Guid)Membership.GetUser().ProviderUserKey;

                    if (userid_solicitud != solicitud.CreateUser)
                        Response.Redirect("~/Errores/Error3002.aspx");
                }
                else
                    Response.Redirect("~/Errores/Error3004.aspx");
            }
            else
                Response.Redirect("~/Errores/Error3001.aspx");

        }

        private void RecargarPago(object sender, EventArgs e)
        {
            EncomiendaBL encomiendaBL = new EncomiendaBL();
            SSITSolicitudesBL blSol = new SSITSolicitudesBL();
            var sol = blSol.Single(id_solicitud);
            bool editable = id_estado == (int)Constantes.TipoEstadoSolicitudEnum.COMP || id_estado == (int)Constantes.TipoEstadoSolicitudEnum.INCOM;
            CargarPagos(editable, sol, encomiendaBL.GetByFKIdSolicitud(sol.IdSolicitud));

        }
        private void CargarPagos(bool editable, SSITSolicitudesDTO sol, IEnumerable<EncomiendaDTO> lstEncomiendas)
        {
            try
            {
                visPagosSolicitud.Enabled = editable;
                visPagosSolicitud.Cargar_Datos(sol, visTramite_CAA.CAA_Actual, lstEncomiendas);
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                this.mensajeAlertaPagos = Functions.GetErrorMessage(ex);
            }
            /*if (!solicitudNueva)
            {
                SGISolicitudesPagosBL pagos = new SGISolicitudesPagosBL();
                var pagosSol = pagos.GetHab(id_solicitud);
                if (pagosSol.Count() == 0)
                {
                    /*if (visPagosSolicitud.getVis_Pagos_AGC().grdPagosGeneradosBUI.Rows.Count <= 0 || visPagosSolicitud.getVis_Pagos_APRA().grdPagosGeneradosBUI.Rows.Count <= 0)
                        visPagosSolicitud.pnlSolictudAnterior2017.Style["Display"] = "block";* /

                    visPagosSolicitud.vis_Pagos_APRA.id_solicitud = sol.IdSolicitud;
                    visPagosSolicitud.Enabled = false;

                    if (visPagosSolicitud.getVis_Pagos_AGC().grdPagosGeneradosBUI.Rows.Count <= 0)
                        visPagosSolicitud.OcultarPnlHabViejas();
                }
            }*/

        }
        private void CargarDatos(SSITSolicitudesDTO ssitDTO, IEnumerable<EncomiendaDTO> lstEnc)
        {
            CargarCabecera(ssitDTO);
            MostrarMensajeAlertaFaltantes();

            bool editable = id_estado == (int)Constantes.TipoEstadoSolicitudEnum.COMP ||
                            id_estado == (int)Constantes.TipoEstadoSolicitudEnum.INCOM ||
                            id_estado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF;
            List<int> estadosAT = new List<int>();
            estadosAT.Add((int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo);
            estadosAT.Add((int)Constantes.Encomienda_Estados.Anulada);

            #region DatosSolicitud
            visDatosSolicitud.Editable = editable;
            visDatosSolicitud.EditableTitulares = editable
                || id_estado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF
                || id_estado == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO
                || id_estado == (int)Constantes.TipoEstadoSolicitudEnum.SUSPEN;

            /*
            // Si es una Ampliación / Redistribución de Uso y proviene de una solicitud SGI no se permite editar.
            if (ssitDTO.SSITSolicitudesOrigenDTO != null)
            {
                //Para los casos de herencia de datos por transmisión ampliación o RDU es necesario que en caso de existencia de baja de ubicación la misma pueda ser editada.
                //https://mantis.grupomost.com/view.php?id=166260

                bool bajaUbicacion = false;
                UbicacionesBL ub = new UbicacionesBL();
                SSITSolicitudesUbicacionBaseDTO ssitSolicitudUbicacionesBaseDTO = new SSITSolicitudesManagerBL().GetUbicacionBySolicitud(ssitDTO.SSITSolicitudesOrigenDTO);
                bajaUbicacion = ub.Single(Convert.ToInt32(ssitSolicitudUbicacionesBaseDTO.IdUbicacion)).BajaLogica;
                bajaUbicacion = visDatosSolicitud.Editable;
                
            }
            */
            visDatosSolicitud.EditableExpRel = editable || id_estado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF;

            DateTime dt = DateTime.Now;
            visDatosSolicitud.CargarDatos(ssitDTO);
            System.Diagnostics.Debug.Write("datos solicitud" + (DateTime.Now - dt).Milliseconds.ToString() + Environment.NewLine);
            #endregion

            #region Anexo Tecnico
            dt = DateTime.Now;
            visAnexoTecnico.CargarDatos(lstEnc);
            System.Diagnostics.Debug.Write("tecnico " + (DateTime.Now - dt).Milliseconds.ToString() + Environment.NewLine);
            #endregion

            #region Anexo Notarial
            dt = DateTime.Now;
            visAnexoNotarial.CargarDatos(lstEnc, id_solicitud);
            System.Diagnostics.Debug.Write("notarial " + (DateTime.Now - dt).Milliseconds.ToString() + Environment.NewLine);
            #endregion

            #region DatosCAA
            dt = DateTime.Now;
            visTramite_CAA.Enabled = editable;
            visTramite_CAA.Cargar_Datos(lstEnc, id_solicitud);
            System.Diagnostics.Debug.Write("caa " + (DateTime.Now - dt).Milliseconds.ToString() + Environment.NewLine);
            #endregion

            #region Pagos
            dt = DateTime.Now;
            CargarPagos(editable, ssitDTO, lstEnc);
            System.Diagnostics.Debug.Write("pagos " + (DateTime.Now - dt).Milliseconds.ToString() + Environment.NewLine);
            #endregion

            #region Documentos
            dt = DateTime.Now;
            visDocumentos.CargarDatos(ssitDTO, lstEnc);
            System.Diagnostics.Debug.Write("documentos " + (DateTime.Now - dt).Milliseconds.ToString() + Environment.NewLine);
            #endregion

            #region Presentacion
            dt = DateTime.Now;
            visPresentacion.CargarDatos(ssitDTO, lstEnc);
            System.Diagnostics.Debug.Write("presentacion " + (DateTime.Now - dt).Milliseconds.ToString() + Environment.NewLine);
            #endregion

            #region Solicitud6
            List<int> lstEstadosRegenerarSol = new List<int>();

            // Si se trata de un nuevo CUR, ocultamos el boton de Anexo Notarial
            int nroSolBaseCUR = 0;
            ParametrosBL param = new ParametrosBL();
            string valChar = param.GetParametroChar("NroSolicitudReferencia");
            if (int.TryParse(valChar, out nroSolBaseCUR) && id_solicitud > nroSolBaseCUR)
                liAnexoNotarial.Style["display"] = "none";

            int id_tipodocsis = 0;
            if (id_solicitud > nroSolBaseCUR)
                id_tipodocsis = (int)Constantes.TiposDeDocumentosSistema.DECLARACION_RESPONSABLE;
            else
                id_tipodocsis = (int)Constantes.TiposDeDocumentosSistema.SOLICITUD_HABILITACION;

            lstEstadosRegenerarSol.Add((int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF);
            lstEstadosRegenerarSol.Add((int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO);

            //136181: JADHE 51523 - SSIT AT - REQ - DDRR Se actualice DDRR segun AT aprobado
            //var ssitDocAdjDTO = ssitDTO.SSITDocumentosAdjuntosDTO.Any(p => p.id_tipodocsis == id_tipodocsis);

            List<int> listEstadosRecalcular = new List<int>();

            listEstadosRecalcular.Add((int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF);
            listEstadosRecalcular.Add((int)Constantes.TipoEstadoSolicitudEnum.ETRA);

            if (listEstadosRecalcular.Contains(ssitDTO.IdEstado))
            {
                ActualizaTipoSubtipoExpSSIT(id_solicitud);
            }

            if (lstEstadosRegenerarSol.Contains(ssitDTO.IdEstado))// && !ssitDocAdjDTO)
            {
                RegenerarSolicitud(id_solicitud);
            }
            #endregion

            if (ssitDTO.IdTipoTramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
            {
                liAnexoNotarial.Style["display"] = "none";
                liApra.Style["display"] = "none";

                DateTime fechaPago = new DateTime(2020, 1, 1);
                if (ssitDTO.CreateDate <= fechaPago)
                {
                    liBui.Style["display"] = "none";
                }
            }

            updCargarDatos.Update();
        }

        private void CargarCabecera(SSITSolicitudesDTO sol)
        {
            int nroSolReferencia = 0;
            int.TryParse(ConfigurationManager.AppSettings["NroSolicitudReferencia"], out nroSolReferencia);

            id_estado = sol.IdEstado;
            lblNroSolicitud.Text = sol.IdSolicitud.ToString();

            //Valido si es una ECI...
            if (!sol.EsECI)
            {
                lblTipoTramite.Text = sol.TipoTramiteDescripcion + " " + sol.TipoExpedienteDescripcion;
            }
            else
            {
                switch (sol.IdTipoTramite)
                {
                    case (int)StaticClass.Constantes.TipoTramite.HabilitacionECIAdecuacion:
                        lblTipoTramite.Text = TipoTramiteDescripcion.AdecuacionECI + " - " + sol.TipoExpedienteDescripcion;
                        break;
                    case (int)StaticClass.Constantes.TipoTramite.HabilitacionECIHabilitacion:
                        lblTipoTramite.Text = TipoTramiteDescripcion.HabilitacionECI + " - " + sol.TipoExpedienteDescripcion;
                        break;

                }
            }
            EngineBL engBL = new EngineBL();
            string descripcionCircuito = engBL.GetDescripcionCircuito(id_solicitud);

            if (descripcionCircuito != null)
                lblTipoTramite.Text += " - " + descripcionCircuito;

            lblEstadoSolicitud.Text = sol.TipoEstadoSolicitudDTO.Descripcion;
            if (sol.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && sol.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
            {
                lblCodigoDeSeguridad.Text = sol.CodigoSeguridad;
                divCodigoSeguridad.Style["display"] = "block";
            }

            if (sol.NroExpedienteSade != null)
                lblNroExpediente.Text = sol.NroExpedienteSade;


            if (sol.SSITSolicitudesOrigenDTO != null)
            {
                lblHabAnterior.Text = sol.SSITSolicitudesOrigenDTO.id_solicitud_origen != null ? sol.SSITSolicitudesOrigenDTO.id_solicitud_origen.ToString() : sol.SSITSolicitudesOrigenDTO.id_transf_origen.ToString();
            }
            else if (sol.EsECI && sol.IdTipoTramite == (int)Constantes.TipoTramite.HabilitacionECIAdecuacion && sol.NroExpedienteSadeRelacionado != null)
            {
                lblHabAnterior.Text = sol.NroExpedienteSadeRelacionado;
            }

            updEstadoSolicitud.Update();

            #region botones
            btnBandeja.PostBackUrl = "~/" + RouteConfig.BANDEJA_DE_ENTRADA;
            divbtnConfirmarTramite.Visible = false;
            if (id_estado == (int)Constantes.TipoEstadoSolicitudEnum.COMP)
                divbtnConfirmarTramite.Visible = true;
            divbtnPresentarTramite.Visible = false;
            if (id_estado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF ||
                id_estado == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO ||
                (id_estado == (int)Constantes.TipoEstadoSolicitudEnum.SUSPEN && isUltimaTareaCorreccion(sol.IdSolicitud)))
                divbtnPresentarTramite.Visible = true;
            divbtnAnularTramite.Visible = false;
            if (id_estado == (int)Constantes.TipoEstadoSolicitudEnum.INCOM ||
                id_estado == (int)Constantes.TipoEstadoSolicitudEnum.COMP ||
                id_estado == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO ||
                id_estado == (int)Constantes.TipoEstadoSolicitudEnum.SUSPEN ||
                id_estado == (int)Constantes.TipoEstadoSolicitudEnum.ETRA ||
                id_estado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF)
                divbtnAnularTramite.Visible = true;

            divbtnImprimirSolicitud.Visible = false;

            //if (id_solicitud > nroSolReferencia &&
            //   (id_estado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM &&
            //    id_estado != (int)Constantes.TipoEstadoSolicitudEnum.COMP &&
            //    id_estado != (int)Constantes.TipoEstadoSolicitudEnum.ANU))
            //{
            //    btnImprimirSolicitud.NavigateUrl = string.Format("~/" + RouteConfig.IMPRIMIR_SOLICITUD + "{0}", Functions.ConvertToBase64String(id_solicitud));
            //    divbtnImprimirSolicitud.Visible = true;
            //}
            //else 

            if (id_solicitud > nroSolReferencia &&
                id_estado == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO ||
                id_estado == (int)Constantes.TipoEstadoSolicitudEnum.ETRA)
            {
                btnImprimirSolicitud.NavigateUrl = string.Format("~/" + RouteConfig.IMPRIMIR_SOLICITUD + "{0}", Functions.ConvertToBase64String(id_solicitud));
                divbtnImprimirSolicitud.Visible = true;
            }

            string tieneOblea = visDocumentos.tieneOblea(id_solicitud);
            divbtnOblea.Visible = false;
            if (tieneOblea != "")
            {
                btnOblea.NavigateUrl = tieneOblea;
                divbtnOblea.Visible = true;
                if (id_solicitud <= nroSolReferencia)
                    divbtnImprimirSolicitud.Visible = false;

            }
            #endregion

            updEstadoSolicitud.Update();
        }

        private bool isUltimaTareaCorreccion(int idSolicitud)
        {
            EngineBL blEng = new EngineBL();
            SGITramitesTareasDTO tramite = blEng.GetUltimaTareaHabilitacionAbierta(idSolicitud);
            if (tramite == null)
                return false;
            if (tramite.IdTarea == (int)Constantes.ENG_Tareas.SSP_Correccion_Solicitud
                || tramite.IdTarea == (int)Constantes.ENG_Tareas.SSP_Correccion_Solicitud_Nuevo
                || tramite.IdTarea == (int)Constantes.ENG_Tareas.SCP_Correccion_Solicitud
                || tramite.IdTarea == (int)Constantes.ENG_Tareas.SCP_Correccion_Solicitud_Nuevo
                || tramite.IdTarea == (int)Constantes.ENG_Tareas.ESP_Correccion_Solicitud
                || tramite.IdTarea == (int)Constantes.ENG_Tareas.ESP_Correccion_Solicitud_Nuevo
                || tramite.IdTarea == (int)Constantes.ENG_Tareas.ESPA_Correccion_Solicitud
                || tramite.IdTarea == (int)Constantes.ENG_Tareas.ESPA_Correccion_Solicitud_Nuevo)
                return true;
            return false;
        }

        private void MostrarMensajeAlertas(params string[] mensajes)
        {
            string alerta = "";

            for (int iii = 0; iii < mensajes.Length; iii++)
            {
                if (!string.IsNullOrEmpty(mensajes[iii]))
                {
                    alerta = string.IsNullOrEmpty(alerta) ? mensajes[iii] : alerta + "<br>" + mensajes[iii];
                }
            }

            lblAlertasSolicitud.Text = System.Web.HttpUtility.HtmlEncode(alerta);
            pnlAlertasSolicitud.Visible = (alerta.Length > 0);
        }

        protected void btnConfirmarTramite_Click(object sender, EventArgs e)
        {
            try
            {
                SSITSolicitudesBL blSol = new SSITSolicitudesBL();
                var sol = blSol.Single(id_solicitud);

                SSITSolicitudesUbicacionesBL solUbicBL = new SSITSolicitudesUbicacionesBL();
                var solUbicDTO = solUbicBL.GetByFKIdSolicitud(id_solicitud);
                if (solUbicDTO.Count() > 1 && !sol.Servidumbre_paso)
                    throw new ValidationException("La opción <b>Servidumbre de paso</b> debe estar tildada, sí desea continuar");

                // Si es una ampliación/Redist Uso que no proviene un AT anterior y no tiene al menos un documento de tipo "Habilitacion Previa" no es posible confirmar.
                if ((sol.IdTipoTramite == (int)Constantes.TipoTramite.AMPLIACION || sol.IdTipoTramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO) && sol.SSITSolicitudesOrigenDTO == null
                    && sol.SSITDocumentosAdjuntosDTO.Count(x => x.id_tipodocsis == (int)Constantes.TiposDeDocumentosSistema.CERTIFICADO_HABILITACION) == 0)
                    throw new ValidationException("Para poder confirmar el trámite debe adjuntar el tipo de documento: 'Habilitación previa'.");

                EncomiendaBL encomiendaBL = new EncomiendaBL();
                var enc = encomiendaBL.GetByFKIdSolicitud(id_solicitud).FirstOrDefault();

                if ((enc != null) && (enc.IdEstado == (int)Constantes.Encomienda_Estados.Incompleta))
                {
                    throw new ValidationException("Para poder confirmar el trámite la encomienda no puede estar incompleta");
                }

                MembershipUser usuario = Membership.GetUser();
                if (sol.idTAD != null)
                {
                    Functions.enviarParticipantes(sol);
                    Functions.enviarCambio(sol);
                }

                if (blSol.confirmarSolicitud(id_solicitud, (Guid)usuario.ProviderUserKey))
                {
                    sol = blSol.Single(id_solicitud);

                    var encomiendas = encomiendaBL.GetByFKIdSolicitud(id_solicitud);
                    lblCodigoDeSeguridad.Text = sol.CodigoSeguridad.ToString();
                    CargarCabecera(sol);
                    visPresentacion.CargarDatos(sol, encomiendas);
                    if (id_solicitud > Constantes.SOLICITUDES_NUEVAS_MAYORES_A)
                    {
                        var notifBL = new SSITSolicitudesNotificacionesBL();

                        string direccion = blSol.GetDireccionesSSIT(new List<int> { id_solicitud }).First().direccion;
                        int idMotivoNotificacion = (int)MotivosNotificaciones.SolicitudConfirmada;
                        string motivo = notifBL.getMotivoById(idMotivoNotificacion);
                        string asunto = $"Sol: {id_solicitud} - {motivo} - {direccion}";
                        string html = new MailMessages().MailSolicitudNueva(sol.IdSolicitud, sol.CodigoSeguridad, sol.TipoTramiteDescripcion);

                        var emails = new List<string>
                        {
                            usuario.Email
                        };
                        emails.AddRange(sol.TitularesPersonasFisicas?.Select(t => t.Email));
                        emails.AddRange(sol.TitularesPersonasJuridicas?.Select(t => t.Email));
                        emails.AddRange(sol.FirmantesPersonasFisicas?.Select(f => f.Email));
                        emails.AddRange(sol.FirmantesPersonasJuridicas?.Select(f => f.Email));
                        emails.Add(new EncomiendaBL().GetProfesionalBySolicitud(sol.IdSolicitud)?.Email);

                        var mailService = new EmailServiceBL();
                        var idEmails = new List<int>();
                        foreach (var email in emails.Where(em => em != null).Distinct())
                        {
                            var emailEntity = new EmailEntity
                            {
                                Email = email,
                                Html = html,
                                Asunto = asunto,
                                IdEstado = (int)TiposDeEstadosEmail.PendienteDeEnvio,
                                IdTipoEmail = (int)TiposDeMail.Generico,
                                IdOrigen = (int)MailOrigenes.SSIT,
                                CantIntentos = 3,
                                CantMaxIntentos = 3,
                                FechaAlta = DateTime.Now,
                                Prioridad = 1
                            };
                            idEmails.Add(mailService.SendMail(emailEntity));
                        }

                        foreach (var idEmail in idEmails)
                        {
                            notifBL.InsertNotificacionByIdSolicitud(sol, idEmail, idMotivoNotificacion);
                        }
                    }
                }
                nroSolicitudModal.Text = sol.IdSolicitud.ToString();
                lblCodSeguridad.Text = sol.CodigoSeguridad.ToString();

                MostrarMensajeAlertaFaltantes();
                if (sol.IdTipoTramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                    lblConfirmarModal.Text = "Conserve estos datos, ya que le serán requeridos para completar el Anexo Técnico de su trámite. También podrá descargarlos desde el botón \"Descargar declaración responsable\" sí lo desea.";
                ScriptManager.RegisterStartupScript(udpConfirmarSolcitud, udpConfirmarSolcitud.GetType(), "showModalConfirmarSolicitud", "showModalConfirmarSolicitud();", true);
            }
            catch (ValidationException ex)
            {
                lblError.Text = ex.Message;
                this.EjecutarScript(udpError, "showfrmError();");
            }
            catch (Exception ex)
            {
                LogError.Write(ex);
                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(udpError, udpError.GetType(), "showfrmError", "showfrmError();", true);
            }
            finally
            {
                updCargarDatos.Update();
            }
        }

        protected void btnAnularTramite_Click(object sender, EventArgs e)
        {
            try
            {
                SSITSolicitudesBL blSol = new SSITSolicitudesBL();
                var sol = blSol.Single(id_solicitud);
                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                if (blSol.anularSolicitud(id_solicitud, userid))
                {
                    sol = blSol.Single(id_solicitud);
                    CargarCabecera(sol);
                    btnCargarDatostramite_Click(sender, e);
                    ScriptManager.RegisterStartupScript(updEstadoSolicitud, updEstadoSolicitud.GetType(), "hidefrmConfirmarAnulacion2", "hidefrmConfirmarAnulacion2();", true);

                }
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updEstadoSolicitud, updEstadoSolicitud.GetType(), "showfrmError", "showfrmError();", true);
            }
        }

        protected void btnPresentarTramite_Click(object sender, EventArgs e)
        {
            try
            {
                pnlTramiteIncompleto.Visible = false;
                lblTextoTramiteIncompleto.Text = "";
                SSITSolicitudesBL blSol = new SSITSolicitudesBL();
                EncomiendaRubrosBL encRubrosBL = new EncomiendaRubrosBL();
                EncomiendaBL encBL = new EncomiendaBL();
                var sol = blSol.Single(id_solicitud);

                //********** DARIO BOLETA 0 - 06/04/2023 **********
                //si boleta cero esta activa, marco la solicitud como excenta de pago
                    if (BoletaCeroActiva())
                        sol.ExencionPago = true;
                //*************************************************

                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                MembershipUser usuario = Membership.GetUser(userid);
                int id_estado_ant = sol.IdEstado;
                byte[] oblea = null;
                string emailUsuario = "";


                int id_encomienda = sol.EncomiendaSSITSolicitudesDTO.Select(x => x.EncomiendaDTO)
                                        .Where(x => x.IdEstado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo)
                                        .OrderByDescending(o => o.IdEncomienda)
                                        .Select(s => s.IdEncomienda)
                                        .FirstOrDefault();

                var lstEncRubros = encRubrosBL.GetRubrosTdoReqByIdEncomienda(id_encomienda);

                if (lstEncRubros.Any() && sol.IdTipoTramite != (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                {
                    grdDocumentosFaltantes.DataSource = lstEncRubros;
                    grdDocumentosFaltantes.DataBind();
                    updDocumentosFaltantes.Update();
                    ScriptManager.RegisterStartupScript(updDocumentosFaltantes, updDocumentosFaltantes.GetType(), "showfrmDocumentosFaltantes", "showfrmDocumentosFaltantes();", true);
                    return;
                }

                if (id_estado_ant == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF || 
                    id_estado_ant == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO)
                {
                    ExternalServiceReporting reportingService = new ExternalServiceReporting();
                    var ReportingEntity = reportingService.GetPDFOblea(id_solicitud, false);
                    oblea = ReportingEntity.Reporte;
                    emailUsuario = usuario.Email;
                    if (oblea.Length <= 0)
                    {
                        throw new Exception("No se pudo generar la oblea, intentelo nuevamente.");
                    }
                }

                LogError.Write("Antes de RegenerarSolicitud");
                RegenerarSolicitud(id_solicitud);
                LogError.Write("Antes de ValidacionSolicitudes");
                if (blSol.ValidacionSolicitudes(id_solicitud))
                {
                    LogError.Write("Antes de presentarSolicitud");
                    if (blSol.presentarSolicitud(id_solicitud, userid, oblea, emailUsuario))
                    {
                        if (sol.EsECI)
                        {
                            LogError.Write("Es ECI");
                            EngineBL eng = new EngineBL();
                            int Solicitud_Nro = sol.SSITSolicitudesOrigenDTO != null ? (int)sol.SSITSolicitudesOrigenDTO.id_solicitud_origen : 0;
                            if (Solicitud_Nro > 0)
                            {
                                var Solicitud = blSol.Single(Solicitud_Nro);

                                int IdTareaGenerarExpediente = eng.getTareaGenerarExpediente(Solicitud_Nro);
                                bool PasoGenerarExpediente = blSol.PasoGenerarExpediente(Solicitud_Nro, IdTareaGenerarExpediente);

                                //valido que no exista la tarea de Generacion de expediente
                                if (!PasoGenerarExpediente)
                                {
                                    //Dar baja la solicitud anterior
                                    try
                                    {
                                        LogError.Write("DarBajaPorECI");
                                        blSol.DarBajaPorECI(Solicitud, (Guid)usuario.ProviderUserKey, "Baja automatica por ECI");
                                    }
                                    catch (Exception ex)
                                    {
                                        throw new Exception("No se pudo dar de baja la solicitud de origen " + Solicitud_Nro.ToString() + " Error:" + Funciones.GetErrorMessage(ex));
                                    }
                                }
                            }
                        }
                        LogError.Write("EnviarParticipantes");
                        if (sol.idTAD != null)
                        {
                            Functions.enviarParticipantes(sol);
                            Functions.enviarCambio(sol);
                        }

                        LogError.Write("ActualizaTipoSubtipoExpSSIT");
                        ActualizaTipoSubtipoExpSSIT(id_solicitud);

                        CargarCabecera(sol);
                        visDocumentos.CargarDatos(id_solicitud);
                        visDatosSolicitud.CargarDatos(sol);
                        EncomiendaBL encomiendaBL = new EncomiendaBL();
                        var encomiendas = encomiendaBL.GetByFKIdSolicitud(id_solicitud);
                        visPresentacion.CargarDatos(sol, encomiendas);
                        divbtnImprimirSolicitud.Visible = true;
                        divbtnPresentarTramite.Visible = false;
                        updEstadoSolicitud.Update();
                    }
                    else
                    {
                        throw new Exception("No se pudo finalizar la presentacion intente nuevamente");
                    }
                }
                else
                {
                    throw new Exception("No se pudo validar la solicitud");
                }
            }
            catch (Exception ex)
            {
                lblTextoTramiteIncompleto.Text = Funciones.GetErrorMessage(ex);
                pnlTramiteIncompleto.Visible = true;
                lblError.Text = Funciones.GetErrorMessage(ex);
                updEstadoSolicitud.Update();
                ScriptManager.RegisterStartupScript(updEstadoSolicitud, updEstadoSolicitud.GetType(), "showfrmError", "showfrmError();", true);
                LogError.Write(ex, ex.Message);

            }
        }

        private void ActualizaTipoSubtipoExpSSIT(int idSolicitud)
        {
            SSITSolicitudesBL ssitBL = new SSITSolicitudesBL();
            ssitBL.ActualizaTipoSubtipoExp(idSolicitud);
        }
        public void RegenerarSolicitud(int id_solicitud)
        {
            ExternalService.Class.ReportingEntity ReportingEntity = new ExternalService.Class.ReportingEntity();
            ExternalServiceFiles esf = new ExternalServiceFiles();
            SSITDocumentosAdjuntosBL ssitDocAdjBL = new SSITDocumentosAdjuntosBL();
            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
            byte[] pdfSolicitud = new byte[0];
            string arch = "Solicitud-" + id_solicitud.ToString() + ".pdf";

            int nroSolReferencia = 0;
            int.TryParse(ConfigurationManager.AppSettings["NroSolicitudReferencia"], out nroSolReferencia);

            int id_tipodocsis = 0;
            if (id_solicitud > nroSolReferencia)
                id_tipodocsis = (int)Constantes.TiposDeDocumentosSistema.DECLARACION_RESPONSABLE;
            else
                id_tipodocsis = (int)Constantes.TiposDeDocumentosSistema.SOLICITUD_HABILITACION;

            var DocAdjDTO = ssitDocAdjBL.GetByFKIdSolicitudTipoDocSis(id_solicitud, id_tipodocsis).FirstOrDefault();

            ExternalServiceReporting reportingService = new ExternalServiceReporting();

            if (id_solicitud > nroSolReferencia)
                ReportingEntity = reportingService.GetPDFSolicitudActEconomica(id_solicitud, true);
            else
                ReportingEntity = reportingService.GetPDFSolicitud(id_solicitud, true);

            pdfSolicitud = ReportingEntity.Reporte;
            int id_file = ReportingEntity.Id_file;

            if (DocAdjDTO != null)
            {
                if (id_file != DocAdjDTO.id_file)
                    esf.deleteFile(DocAdjDTO.id_file);
                DocAdjDTO.id_file = id_file;
                DocAdjDTO.nombre_archivo = arch;
                DocAdjDTO.UpdateDate = DateTime.Now;
                DocAdjDTO.UpdateUser = userid;
                DocAdjDTO.fechaPresentado = DateTime.Now;
                ssitDocAdjBL.Update(DocAdjDTO);
            }
            else
            {
                DocAdjDTO = new SSITDocumentosAdjuntosDTO();
                DocAdjDTO.id_solicitud = id_solicitud;
                DocAdjDTO.id_tdocreq = 0;
                DocAdjDTO.tdocreq_detalle = "";
                DocAdjDTO.generadoxSistema = true;
                DocAdjDTO.CreateDate = DateTime.Now;
                DocAdjDTO.CreateUser = userid;
                DocAdjDTO.nombre_archivo = arch;
                DocAdjDTO.id_file = id_file;
                DocAdjDTO.id_tipodocsis = id_tipodocsis;

                ssitDocAdjBL.Insert(DocAdjDTO, true);
            }
        }

        protected void visTramite_CAA_Error(object sender, SSIT.Solicitud.Habilitacion.Controls.Tramite_CAA.CAAErrorEventArgs e)
        {
            lblAlertasSolicitud.Text = e.Description;
            pnlAlertasSolicitud.Visible = true;

        }

        private void MostrarMensajeAlertaFaltantes()
        {

            if (lblEstadoSolicitud.Text == "Completo")
            {
                lblTextoTramiteIncompleto.Text = "Usted completó satisfactoriamente el primer paso de ingreso de Titulares y Ubicación. Para poder continuar con su trámite, deberá contactar a un profesional técnico de la construcción matriculado en CABA. Cuando presione " + " " + '"' + "Confirmar" + '"' + ", se le otorgará un código de seguridad el cual deberá brindarle al profesional para que realice el anexo técnico.";
                pnlTramiteIncompleto.Visible = true;
            }
            else
            {
                pnlTramiteIncompleto.Visible = false;
            }
        }

        protected void visDocumentos_EventRecargarPagos(object sender, SSIT.Solicitud.Habilitacion.Controls.Documentos.ucRecargarPagosEventsArgs e)
        {
            visPagosSolicitud.RecargarPagos(e.tipo_tramite, e.id_solicitud);
        }

        protected void visTramite_CAA_EventRecargarPagos(object sender, SSIT.Solicitud.Habilitacion.Controls.Tramite_CAA.ucRecargarPagosEventsArgs e)
        {
            visPagosSolicitud.RecargarPagos(e.tipo_tramite, e.id_solicitud);

        }

        protected void PagosError_Click(object sender, SSIT.Solicitud.Habilitacion.Controls.ErrorEventsArgs e)
        {

            lblAlertasSolicitud.Text = e.error;
            pnlAlertasSolicitud.Visible = true;

            lblError.Text = e.error;
            ScriptManager.RegisterStartupScript(updAlertas, updAlertas.GetType(), "showfrmError", "showfrmError();", true);
        }

        private bool BoletaCeroActiva()
        {
            
            string boletaCero_FechaDesde = System.Configuration.ConfigurationManager.AppSettings["boletaCero_FechaDesde"];

            DateTime boletaCeroDate = DateTime.ParseExact(boletaCero_FechaDesde,
                                                            "yyyyMMdd",
                                                            System.Globalization.CultureInfo.InvariantCulture);

            if (DateTime.Now > boletaCeroDate)
                return true;

            return false;
        }
    }
}