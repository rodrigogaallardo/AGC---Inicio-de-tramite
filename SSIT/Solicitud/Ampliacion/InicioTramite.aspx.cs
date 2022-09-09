using SSIT.App_Components;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinesLayer.Implementation;
using DataTransferObject;
using System.Web.Security;
using ExternalService;

namespace SSIT.Solicitud.Ampliacion
{
    public partial class InicioTramite : SecurePage
    {
        
        AmpliacionesBL blSol = new AmpliacionesBL();

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

            if(txtExpediente_Anio.Text.Trim().Length > 0 && txtExpediente_Nro.Text.Trim().Length > 0)
            {
                anio_expediente = int.Parse(txtExpediente_Anio.Text);
                nro_expediente = int.Parse(txtExpediente_Nro.Text);
            }
            if (txtNroPartidaMatriz.Text.Trim().Length > 0)
            {
                nro_partida_matriz = int.Parse(txtNroPartidaMatriz.Text);
            }

            var lstTramitesAprobados = blSol.GetSolicitudesAprobadas(anio_expediente, nro_expediente, nro_partida_matriz, cuit).OrderByDescending(o=> o.IdSolicitud).ToList();
            SeleccionTramitesAprobados.LoadData(lstTramitesAprobados);

            
            if(SeleccionTramitesAprobados.Count > 0)
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
            try
            {
                if (ValidarTramiteSeleccionado())
                {
                    this.EjecutarScript(updTramitesEncontrados, "showfrmConfirmarNuevaAmpliacion();");
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
                this.EjecutarScript(updConfirmarNuevaAmpliacion, "showfrmError();");
            }
        }

        protected void btnNuevaAmpliacion_Click(object sender, EventArgs e)
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
                    
                    sol.IdTipoTramite = (int)Constantes.TipoTramite.AMPLIACION;
                    sol.IdTipoExpediente = (int)Constantes.TipoDeExpediente.NoDefinido;
                    sol.IdSubTipoExpediente = (int)Constantes.SubtipoDeExpediente.NoDefinido;
                    sol.CreateDate = DateTime.Now;
                    sol.CreateUser = userid;
                    sol.SSITSolicitudesOrigenDTO = oSSITSolicitudesOrigenDTO;
                    
                    
                    id_solicitud = blSol.Insert(sol);


                    if (oSSITSolicitudesOrigenDTO != null)
                        url_destino = "~/" + RouteConfig.VISOR_SOLICITUD_AMPLIACION + id_solicitud.ToString();
                    else
                        url_destino = "~/" + RouteConfig.CARGA_PLANCHETA_AMPLIACION + id_solicitud.ToString();

                    string cuit = Membership.GetUser().UserName;
                    ParametrosBL parametrosBL = new ParametrosBL();
                    string _urlESB = parametrosBL.GetParametroChar("Url.Service.ESB");
                    string trata = parametrosBL.GetParametroChar("Trata.Ampliacion");
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
                    this.EjecutarScript(updConfirmarNuevaAmpliacion, "showfrmError();");
                }
            }
            catch (Exception ex)
            {
                if (id_solicitud != 0)
                    sSITSolicitudesBL.Delete(sol);
                url_destino = "";

                LogError.Write(ex);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updConfirmarNuevaAmpliacion, "showfrmError();");
            }

            if (url_destino.Length > 0)
                Response.Redirect(url_destino);
        }

    }
    
}