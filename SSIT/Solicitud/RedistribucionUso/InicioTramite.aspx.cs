using BusinesLayer.Implementation;
using DataTransferObject;
using ExternalService;
using SSIT.App_Components;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Solicitud.RedistribucionUso
{
    public partial class InicioTramite : SecurePage
    {

        RedistribucionUsoBL blSolRU = new RedistribucionUsoBL();
        SSITSolicitudesBL blSol = new SSITSolicitudesBL();

        protected void Page_Load(object sender, EventArgs e)
        {
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


            int? anio_expediente = null;
            int? nro_expediente = null;
            int? nro_partida_matriz = null;
            string cuit = txtCuit.Text.Trim();

            if (txtExpediente_Anio.Text.Trim().Length > 0 && txtExpediente_Nro.Text.Trim().Length > 0)
            {
                anio_expediente = int.Parse(txtExpediente_Anio.Text);
                nro_expediente = int.Parse(txtExpediente_Nro.Text);
            }
            if (txtNroPartidaMatriz.Text.Trim().Length > 0)
            {
                nro_partida_matriz = int.Parse(txtNroPartidaMatriz.Text);
            }

            var lstTramitesAprobados = blSolRU.GetSolicitudesAprobadas(anio_expediente, nro_expediente, nro_partida_matriz, cuit).OrderByDescending(o => o.IdSolicitud).ToList();
            SeleccionTramitesAprobados.LoadData(lstTramitesAprobados);


            if (SeleccionTramitesAprobados.Count > 0)
                btnConfirmar.Text = "Confirmar";
            else
                btnConfirmar.Text = "Continuar";

            this.EjecutarScript(updBotonesValidar, "showfrmTramitesEncontrados();");

        }

        private bool ValidarTramiteSeleccionado()
        {
            return (SeleccionTramitesAprobados.Count == 0 || SeleccionTramitesAprobados.GetTramiteSeleccionado() != null);
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            lblError.Text = "";

            try
            {
                if (ValidarTramiteSeleccionado())
                {
                    var solAprobada = SeleccionTramitesAprobados.GetTramiteSeleccionado();

                    if (solAprobada != null)
                    {
                        
                        if (solAprobada.IdTipoExpediente == (int)Constantes.TipoDeExpediente.Simple &&
                            solAprobada.IdSubTipoExpediente == (int) Constantes.SubtipoDeExpediente.SinPlanos)
                        {
                            lblError.Text = "La habilitación que da origen a la Redistribución de Uso, NO puede ser una habilitación simple sin planos.";
                        }
                        
                    }

                    if (lblError.Text.Length > 0)
                        this.EjecutarScript(updTramitesEncontrados, "showfrmError();");
                    else
                        this.EjecutarScript(updTramitesEncontrados, "showfrmConfirmarNuevaRedistribucionUso();");
                    
                }
                else
                {
                    lblError.Text = "Dele seleccionar un trámite para poder confirmar.";
                    this.EjecutarScript(updTramitesEncontrados, "showfrmError();");
                }

            }
            catch (Exception ex)
            {
                LogError.Write(ex);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updConfirmarNuevaRedistribucionUso, "showfrmError();");
            }
        }

        protected void btnNuevaRedistribucionUso_Click(object sender, EventArgs e)
        {
            SSITSolicitudesBL sSITSolicitudesBL = new SSITSolicitudesBL();
            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
            string url_destino = "";
            SSITSolicitudesDTO sol = new SSITSolicitudesDTO();
            int id_solicitud = 0;

            try
            {
                if (ValidarTramiteSeleccionado())
                {
                    var solAprobada = SeleccionTramitesAprobados.GetTramiteSeleccionado();

                    SSITSolicitudesOrigenDTO oSSITSolicitudesOrigenDTO = null;

                    if (solAprobada != null)
                    {
                        oSSITSolicitudesOrigenDTO = new SSITSolicitudesOrigenDTO();

                        if (solAprobada.IdTipoTramite == (int)Constantes.TipoTramite.TRANSFERENCIA)
                            oSSITSolicitudesOrigenDTO.id_transf_origen = solAprobada.IdSolicitud;
                        else
                            oSSITSolicitudesOrigenDTO.id_solicitud_origen = solAprobada.IdSolicitud;

                        oSSITSolicitudesOrigenDTO.CreateDate = DateTime.Now;
                    }


                    sol.CodigoSeguridad = Funciones.getGenerarCodigoSeguridadEncomiendas();
                    sol.IdEstado = (int)Constantes.TipoEstadoSolicitudEnum.INCOM;
                    
                    sol.IdTipoTramite = (int)Constantes.TipoTramite.REDISTRIBUCION_USO;
                    sol.IdTipoExpediente = (int)Constantes.TipoDeExpediente.NoDefinido;
                    sol.IdSubTipoExpediente = (int)Constantes.SubtipoDeExpediente.NoDefinido;
                    sol.CreateDate = DateTime.Now;
                    sol.CreateUser = userid;
                    sol.SSITSolicitudesOrigenDTO = oSSITSolicitudesOrigenDTO;
                    sol.Servidumbre_paso = false;

                    id_solicitud = blSolRU.Insert(sol);

                    if (oSSITSolicitudesOrigenDTO != null)
                        url_destino = "~/" + RouteConfig.VISOR_SOLICITUD_REDISTRIBUCION_USO + id_solicitud.ToString();
                    else
                        url_destino = "~/" + RouteConfig.CARGA_PLANCHETA_REDISTRIBUCION_USO + id_solicitud.ToString();

                    string cuit = Membership.GetUser().UserName;
                    ParametrosBL parametrosBL = new ParametrosBL();
                    string _urlESB = parametrosBL.GetParametroChar("Url.Service.ESB");
                    string trata = parametrosBL.GetParametroChar("Trata.RedistribucionDeUso");
                    bool tad = Convert.ToBoolean(parametrosBL.GetParametroChar("SSIT.NO.TAD"));

                    if (tad)
                    {
                        int idTAD = wsTAD.crearTramiteTAD(_urlESB, cuit, trata, null, Constantes.Sistema, id_solicitud);
                        sol = sSITSolicitudesBL.Single(id_solicitud);
                        sol.idTAD = idTAD;
                        sSITSolicitudesBL.Update(sol);
                    }
                }
                else
                {
                    lblError.Text = "Dele seleccionar un trámite para poder confirmar.";
                    this.EjecutarScript(updConfirmarNuevaRedistribucionUso, "showfrmError();");
                }
            }
            catch (Exception ex)
            {
                if (id_solicitud != 0)
                    sSITSolicitudesBL.Delete(sol);
                url_destino = "";
                LogError.Write(ex);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updConfirmarNuevaRedistribucionUso, "showfrmError();");
            }

            if (url_destino.Length > 0)
                Response.Redirect(url_destino);
        }

    }

}