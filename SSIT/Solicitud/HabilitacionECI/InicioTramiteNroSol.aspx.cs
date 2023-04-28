using BusinesLayer.Implementation;
using DataTransferObject;
using ExternalService;
using ExternalService.ws_interface_AGC;
using SSIT.App_Components;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using static StaticClass.Constantes;

namespace SSIT.Solicitud.HabilitacionECI
{
    public partial class InicioTramiteNroSol : SecurePage
    {

        public string HabilitacionECI { get; set; }

        public int Id_Solicitud
        {
            get
            {
                return Convert.ToInt32(hid_id_solicitud.Value);
            }
            set
            {
                hid_id_solicitud.Value = value.ToString();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            HabilitacionECI = TipoTramiteDescripcion.HabilitacionECI;
            ScriptManager sm = ScriptManager.GetCurrent(this);

            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(updDatos, updDatos.GetType(), "init_Js_updDatos", "init_Js_updDatos();", true);
            }


            if (!IsPostBack)
            {
                hid_return_url.Value = Request.Url.AbsoluteUri;
            }

        }

        protected void btnCargarDatos_Click(object sender, EventArgs e)
        {
            try
            {

                this.EjecutarScript(updCargarDatos, "finalizarCarga();");

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updCargarDatos, "finalizarCarga();showfrmError();");
            }

        }

        protected void btnValidar_Click(object sender, EventArgs e)
        {
            SSITSolicitudesBL blSol = new SSITSolicitudesBL();
            EngineBL eng = new EngineBL();

            int Solicitud_Nro = Convert.ToInt32(txtSolicitud_Nro.Text.Trim());
            string CodigoSeg = txtCodigoSeg.Text.Trim();

            var Solicitud = blSol.Single(Solicitud_Nro);
            int IdTareaGenerarExpediente = eng.getTareaGenerarExpediente(Solicitud_Nro);
            bool PasoGenerarExpediente = blSol.PasoGenerarExpediente(Solicitud_Nro, IdTareaGenerarExpediente);

            try
            {
                //Valido que sea una solicitud anterior al CUR
                if (blSol.isRubroCur(Solicitud.IdSolicitud))
                {
                    Id_Solicitud = 0;
                    lblError.Text = " Su solicitud no se encuentra en  condiciones de realizar la adecuacion ECI, por esta vía. Para mayor información, por favor acercarse a la Mesa de Ayuda de la AGC.";
                    this.EjecutarScript(updBotonesValidar, "showfrmError();");
                    return;
                }
                //valido el codigo de seguridad, el estado de la solicitud y que exista la tarea de Generacion de expediente
                if (Solicitud.CodigoSeguridad == CodigoSeg)
                {
                    if (
                        Solicitud.IdEstado == (int)StaticClass.Constantes.TipoEstadoSolicitudEnum.ETRA ||
                        Solicitud.IdEstado == (int)StaticClass.Constantes.TipoEstadoSolicitudEnum.OBSERVADO)
                    {
                        if (!PasoGenerarExpediente)
                        {
                            Id_Solicitud = Solicitud.IdSolicitud;
                            this.EjecutarScript(updBotonesValidar, "showfrmConfirmarNuevaAmpliacion();");
                        }
                        else
                        {
                            Id_Solicitud = 0;
                            lblError.Text = "Su solicitud cuenta con un expediente generado, deberá dirigirse hasta la Agencia Gubernamental de Control para dar continuidad a su trámite";
                            this.EjecutarScript(updBotonesValidar, "showfrmError();");
                        }
                    }
                    else
                    {
                        Id_Solicitud = 0;
                        lblError.Text = "El estado de la solicitud debe ser \"En trámite\" u \"Observado\", deberá dirigirse hasta la Agencia Gubernamental de Control para dar continuidad a su trámite";
                        this.EjecutarScript(updBotonesValidar, "showfrmError();");
                    }

                }
                else
                {
                    Id_Solicitud = 0;
                    lblError.Text = "El código de seguridad no corresponde a la solicitud";
                    this.EjecutarScript(updBotonesValidar, "showfrmError();");
                }
            }
            catch (Exception ex)
            {
                LogError.Write(ex);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updBotonesValidar, "showfrmError();");
            }

        }

        protected void btnNuevaAmpliacion_Click(object sender, EventArgs e)
        {
            SSITSolicitudesBL sSITSolicitudesBL = new SSITSolicitudesBL();
            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
            string url_destino = "";
            AmpliacionesBL blSol = new AmpliacionesBL();
            SSITSolicitudesDTO sol = new SSITSolicitudesDTO();
            int id_solicitudNew = 0;
            try
            {
                if (Id_Solicitud > 0)
                {
                    var solAprobada = sSITSolicitudesBL.Single(Id_Solicitud);

                    SSITSolicitudesOrigenDTO oSSITSolicitudesOrigenDTO = null;

                    if (solAprobada != null)
                    {
                        oSSITSolicitudesOrigenDTO = new DataTransferObject.SSITSolicitudesOrigenDTO();

                        if (solAprobada.IdTipoTramite == (int)Constantes.TipoTramite.TRANSFERENCIA)
                            oSSITSolicitudesOrigenDTO.id_transf_origen = solAprobada.IdSolicitud;
                        else
                            oSSITSolicitudesOrigenDTO.id_solicitud_origen = solAprobada.IdSolicitud;

                        oSSITSolicitudesOrigenDTO.CreateDate = DateTime.Now;
                        sol.Servidumbre_paso = solAprobada.Servidumbre_paso;
                    }
                    else
                        sol.Servidumbre_paso = false;



                    sol.CodigoSeguridad = Funciones.getGenerarCodigoSeguridadEncomiendas();
                    sol.IdEstado = (int)Constantes.TipoEstadoSolicitudEnum.INCOM;

                    sol.IdTipoTramite = (int)Constantes.TipoTramite.HabilitacionECIAdecuacion;
                    sol.IdTipoExpediente = (int)Constantes.TipoDeExpediente.NoDefinido;
                    sol.IdSubTipoExpediente = (int)Constantes.SubtipoDeExpediente.NoDefinido;
                    sol.CreateDate = DateTime.Now;
                    sol.CreateUser = userid;
                    sol.SSITSolicitudesOrigenDTO = oSSITSolicitudesOrigenDTO;
                    sol.EsECI = true;

                    id_solicitudNew = blSol.Insert(sol);

                    if (oSSITSolicitudesOrigenDTO != null)
                    {
                        //Copio la documentacion de la solicitud anterior
                        SSITDocumentosAdjuntosBL solDocBL = new SSITDocumentosAdjuntosBL();
                        SSITDocumentosAdjuntosDTO solDocDTO;
                        var SolOrigen = sSITSolicitudesBL.Single((int)oSSITSolicitudesOrigenDTO.id_solicitud_origen);

                        foreach (var itemDoc in SolOrigen.SSITDocumentosAdjuntosDTO)
                        {
                            //doc.id_tdocreq == 0
                            if (!itemDoc.generadoxSistema && itemDoc.id_tdocreq > 0)
                            {
                                solDocDTO = new SSITDocumentosAdjuntosDTO();
                                solDocDTO.id_solicitud = id_solicitudNew;
                                solDocDTO.id_tipodocsis = itemDoc.id_tipodocsis;
                                solDocDTO.id_tdocreq = 0;
                                solDocDTO.generadoxSistema = true;
                                solDocDTO.CreateDate = itemDoc.CreateDate;
                                solDocDTO.CreateUser = itemDoc.CreateUser;
                                solDocDTO.nombre_archivo = itemDoc.nombre_archivo;
                                solDocDTO.id_file = itemDoc.id_file;
                                solDocDTO.tdocreq_detalle = itemDoc.tdocreq_detalle;
                                solDocBL.Insert(solDocDTO, true);
                            }
                        }
                        //Copio el CAA
                        List<DtoCAA> CAS = CopiarCAADesdeSolicitudAnterior((int)oSSITSolicitudesOrigenDTO.id_solicitud_origen);
                        foreach (var itemDoc in CAS)
                        {
                            solDocDTO = new SSITDocumentosAdjuntosDTO();
                            solDocDTO.id_solicitud = id_solicitudNew;
                            solDocDTO.id_tipodocsis = itemDoc.Documentos[0].id_tipodocsis;
                            solDocDTO.id_tdocreq = 0;
                            solDocDTO.generadoxSistema = true;
                            solDocDTO.CreateDate = itemDoc.CreateDate;
                            solDocDTO.CreateUser = userid;
                            solDocDTO.nombre_archivo = "CAA" + itemDoc.id_encomienda.ToString() + "." + itemDoc.Documentos[0].formato_archivo;
                            solDocDTO.id_file = itemDoc.Documentos[0].id_file;
                            solDocDTO.tdocreq_detalle = "";
                            solDocBL.Insert(solDocDTO, true);
                        }
                    }

                    if (oSSITSolicitudesOrigenDTO != null)
                        url_destino = "~/" + RouteConfig.VISOR_SOLICITUD_ECI + id_solicitudNew.ToString();
                    else
                        url_destino = "~/" + RouteConfig.CARGA_PLANCHETA_ECI + id_solicitudNew.ToString();

                    string cuit = Membership.GetUser().UserName;
                    ParametrosBL parametrosBL = new ParametrosBL();
                    string _urlESB = parametrosBL.GetParametroChar("Url.Service.ESB");
                    string trata = parametrosBL.GetParametroChar("Trata.Habilitacion");
                    bool tad = Convert.ToBoolean(parametrosBL.GetParametroChar("SSIT.NO.TAD"));

                    if (!tad)
                    {
                        int idTAD = 0;
                        idTAD = wsTAD.crearTramiteTAD(_urlESB, cuit, trata, null, Constantes.Sistema, id_solicitudNew);
                        sol = sSITSolicitudesBL.Single(id_solicitudNew);
                        sol.idTAD = idTAD;
                        sSITSolicitudesBL.Update(sol);
                    }
                }
            }
            catch (Exception ex)
            {

                lblError.Text = ex.Message + " " + Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updConfirmarNuevaAmpliacion, "showfrmError();");
                LogError.Write(ex);

                if (id_solicitudNew != 0)
                    sSITSolicitudesBL.Delete(sol);
                url_destino = "";
            }

            if (url_destino.Length > 0)
                Response.Redirect(url_destino);
        }

        /// <summary>
        /// Copiars the caa desde solicitud anterior.
        /// </summary>
        /// <param name="id_Solicitud_Origen">The identifier solicitud origen.</param>
        /// <returns></returns>
        private List<DtoCAA> CopiarCAADesdeSolicitudAnterior(int id_Solicitud_Origen)
        {
            //Busco las encomiendas
            EncomiendaBL blEnc = new EncomiendaBL();
            var lstEnc = blEnc.GetByFKIdSolicitud(id_Solicitud_Origen);


            // Llena los CAAs de acuerdo a las encomiendas vinculadas a la solicitud.
            // ---------------------------------------------------------------------
            ws_Interface_AGC servicio = new ws_Interface_AGC();
            ExternalService.ws_interface_AGC.wsResultado ws_resultado_CAA = new ExternalService.ws_interface_AGC.wsResultado();

            ParametrosBL blParam = new ParametrosBL();
            servicio.Url = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC");
            string username_servicio = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.User");
            string password_servicio = blParam.GetParametroChar("SIPSA.Url.Webservice.ws_Interface_AGC.Password");
            DtoCAA[] l = servicio.Get_CAAs_by_Encomiendas(username_servicio, password_servicio, lstEnc.Select(x => x.IdEncomienda).ToList().ToArray(), ref ws_resultado_CAA);

            List<DtoCAA> List_CAA = l.ToList();
            return List_CAA;
        }

    }

}