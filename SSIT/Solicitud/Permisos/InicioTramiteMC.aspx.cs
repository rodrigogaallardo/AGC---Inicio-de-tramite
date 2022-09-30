using BusinesLayer.Implementation;
using DataTransferObject;
using SSIT.App_Components;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Solicitud.Permisos
{
    public partial class InicioTramiteMC : SecurePage
    {

        AmpliacionesBL blSolAmp = new AmpliacionesBL();
        SSITSolicitudesBL blSol = new SSITSolicitudesBL();
        EncomiendaBL blEnc = new EncomiendaBL();

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
                ddlExpediente_Sector.Items.Clear();
                ddlExpediente_Sector.Items.Add("MGEYA");
                ddlExpediente_Sector.Items.Add("GCABA");
                ddlExpediente_Sector.SelectedIndex = 0;

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

            var lstTramitesAprobados = blSolAmp.GetSolicitudesAprobadas(anio_expediente, nro_expediente, nro_partida_matriz, cuit).OrderByDescending(o => o.IdSolicitud).ToList();
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
            try
            {
                if (ValidarTramiteSeleccionado())
                {
                    this.EjecutarScript(updTramitesEncontrados, "showfrmConfirmarNuevoPermiso();");
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
                this.EjecutarScript(updConfirmarNuevoPermiso, "showfrmError();");
            }
        }

        protected void btnNuevoPermiso_Click(object sender, EventArgs e)
        {
            SSITSolicitudesBL sSITSolicitudesBL = new SSITSolicitudesBL();
            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
            string url_destino = "";
            SSITSolicitudesDTO sol = new SSITSolicitudesDTO();
            SSITSolicitudesDTO solAnterior = new SSITSolicitudesDTO();

            int id_solicitud = 0;
            try
            {
                if (ValidarTramiteSeleccionado())
                {
                    SolicitudesAprobadasDTO solAprobada = SeleccionTramitesAprobados.GetTramiteSeleccionado();
                    SSITSolicitudesOrigenDTO oSSITSolicitudesOrigenDTO = null;


                    if (solAprobada != null)
                    {
                        // Obtiene todos los datos de la solicitud anterior
                        solAnterior = blSol.Single(solAprobada.IdSolicitud);
                        EncomiendaDTO encDTO;
                        if (solAnterior != null)
                            encDTO = blEnc.GetUltimaEncomiendaAprobada(solAprobada.IdSolicitud);
                        else
                            encDTO = null;

                        oSSITSolicitudesOrigenDTO = new DataTransferObject.SSITSolicitudesOrigenDTO();

                        if (solAprobada.IdTipoTramite == (int)Constantes.TipoTramite.TRANSFERENCIA)
                            oSSITSolicitudesOrigenDTO.id_transf_origen = solAprobada.IdSolicitud;
                        else
                            oSSITSolicitudesOrigenDTO.id_solicitud_origen = solAprobada.IdSolicitud;

                        oSSITSolicitudesOrigenDTO.CreateDate = DateTime.Now;
                        sol.Servidumbre_paso = solAprobada.Servidumbre_paso;

                        sol.SSITSolicitudesRubrosCNDTO = new List<SSITSolicitudesRubrosCNDTO>();

                        if (encDTO != null)
                        {
                            foreach (var rubro in encDTO.EncomiendaRubrosCNDTO)
                            {
                                sol.SSITSolicitudesRubrosCNDTO.Add(new SSITSolicitudesRubrosCNDTO
                                {
                                    CodigoRubro = rubro.CodigoRubro,
                                    CreateDate = DateTime.Now,
                                    CreateUser = userid,
                                    DescripcionRubro = rubro.DescripcionRubro,
                                    EsAnterior = rubro.EsAnterior,
                                    idImpactoAmbiental = rubro.idImpactoAmbiental,
                                    IdRubro = rubro.IdRubro,
                                    IdTipoActividad = rubro.IdTipoActividad,
                                    IdTipoExpediente = rubro.IdTipoExpediente,
                                    SuperficieHabilitar = rubro.SuperficieHabilitar,
                                    RestriccionSup = rubro.RestriccionSup,
                                    RestriccionZona = rubro.RestriccionZona
                                });
                            }

                            foreach (var dl in encDTO.EncomiendaDatosLocalDTO)
                            {
                                sol.SSITSolicitudesDatosLocalDTO = new SSITSolicitudesDatosLocalDTO
                                {
                                    CreateDate = DateTime.Now,
                                    CreateUser = userid,
                                    superficie_cubierta_dl = (dl.superficie_cubierta_dl.HasValue ? dl.superficie_cubierta_dl.Value : 0),
                                    superficie_descubierta_dl = (dl.superficie_descubierta_dl.HasValue ? dl.superficie_descubierta_dl.Value : 0)
                                };
                            }
                        }
                    }
                    else
                        sol.Servidumbre_paso = false;


                    sol.CodigoSeguridad = Funciones.getGenerarCodigoSeguridadEncomiendas();
                    sol.IdEstado = (int)Constantes.TipoEstadoSolicitudEnum.INCOM;

                    sol.IdTipoTramite = (int)Constantes.TipoTramite.PERMISO;
                    sol.IdTipoExpediente = (int)Constantes.TipoDeExpediente.MusicaCanto;
                    sol.IdSubTipoExpediente = (int)Constantes.SubtipoDeExpediente.NoDefinido;
                    sol.CreateDate = DateTime.Now;
                    sol.CreateUser = userid;
                    sol.SSITSolicitudesOrigenDTO = oSSITSolicitudesOrigenDTO;

                    id_solicitud = blSolAmp.Insert(sol);
                    

                    if (oSSITSolicitudesOrigenDTO != null)
                        url_destino = "~/" + RouteConfig.VISOR_SOLICITUD_PERMISO_MC + id_solicitud.ToString();
                    else
                        url_destino = "~/" + RouteConfig.AGREGAR_TITULAR_SOLICITUD_PERMISO_MC +  id_solicitud.ToString();
                    
                }
                else
                {
                    lblError.Text = "Dele seleccionar un trámite para poder confirmar.";
                    this.EjecutarScript(updConfirmarNuevoPermiso, "showfrmError();");
                }
            }
            catch (Exception ex)
            {
                if (id_solicitud != 0)
                    sSITSolicitudesBL.Delete(sol);

                url_destino = "";

                LogError.Write(ex);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updConfirmarNuevoPermiso, "showfrmError();");
            }

            if (url_destino.Length > 0)
                Response.Redirect(url_destino);
        }

    }

}