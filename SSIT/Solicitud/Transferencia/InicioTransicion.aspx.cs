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

namespace SSIT.Solicitud.Transferencia
{
    public partial class InicioTransicion : SecurePage
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

        private void CargarTiposDeTransmisiones()
        {
            TiposDeTransmisionBL tipoTransmisionBL = new TiposDeTransmisionBL();

            var lstTiposdeTransmision = tipoTransmisionBL.GetAll();
            ddlTipoTransmision.DataSource = lstTiposdeTransmision;
            ddlTipoTransmision.DataTextField = "nom_tipotransmision";
            ddlTipoTransmision.DataValueField = "id_tipoTransmision";
            ddlTipoTransmision.DataBind();
            ddlTipoTransmision.Items.Insert(0, string.Empty);
        }

        private bool ValidarTramiteSeleccionado()
        {
            return (SeleccionTramitesAprobados.Count == 0 || SeleccionTramitesAprobados.GetTramiteSeleccionado() != null);
        }

        private ConsultaPadronSolicitudesDTO CrearCP(SolicitudesAprobadasDTO soli, Guid userid)
        {
            ConsultaPadronSolicitudesBL bl = new ConsultaPadronSolicitudesBL();
            ConsultaPadronSolicitudesDTO dto = new ConsultaPadronSolicitudesDTO();

            dto.IdTipoTramite = (int)Constantes.TipoDeTramite.ConsultaPadron;
            dto.IdEstado = (int)Constantes.TipoEstadoSolicitudEnum.INCOM;
            dto.CreateUser = Guid.Parse("A153211F-CCF4-4E86-BB90-C8D030974DD9"); 
            dto.CreateDate = DateTime.Now;
            dto.CodigoSeguridad = Funciones.getGenerarCodigoSeguridadEncomiendas();
            bl.Insert(dto);
            //bl.copiarDatos(soli.IdSolicitud, dto.IdConsultaPadron, userid);
            try
            {
                string cuit = Membership.GetUser().UserName;
                ParametrosBL parametrosBL = new ParametrosBL();
                string _urlESB = parametrosBL.GetParametroChar("Url.Service.ESB");
                string trata = parametrosBL.GetParametroChar("Trata.Consulta.Padron");
                bool tad = Convert.ToBoolean(parametrosBL.GetParametroChar("SSIT.NO.TAD"));

               /* if (tad)
                {
                    int idTAD = wsTAD.crearTramiteTAD(_urlESB, cuit, trata, null, Constantes.Sistema, dto.IdConsultaPadron);
                    dto.idTAD = idTAD;
                    bl.Update(dto);
                }      */          
            }
            catch (Exception ex)
            {
                if (dto.IdConsultaPadron != 0)
                {
                    var sol = bl.Single(dto.IdConsultaPadron);
                    bl.Delete(sol);
                }
                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updmpeInfo, updmpeInfo.GetType(), "showfrmError", "showfrmError();", true);
            }
            return dto;
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

            var lstTramitesAprobados = blSol.GetSolicitudesAprobadas(anio_expediente, nro_expediente, nro_partida_matriz, cuit).OrderByDescending(o => o.IdSolicitud).ToList();
            SeleccionTramitesAprobados.LoadData(lstTramitesAprobados);


            if (SeleccionTramitesAprobados.Count > 0)
                btnConfirmar.Text = "Confirmar";
            else
                btnConfirmar.Text = "Continuar";

            CargarTiposDeTransmisiones();
            this.EjecutarScript(updBotonesValidar, "showfrmTramitesEncontrados();");

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

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            
            try
            {
                if (ddlTipoTransmision.SelectedValue.Length == 0)
                {
                    lblError.Text = "Debe seleccionar un tipo de Transmisión para poder continuar";
                    this.EjecutarScript(updTramitesEncontrados, "showfrmError();");
                }
                else
                { 
                    if (ValidarTramiteSeleccionado())
                    {
                        this.EjecutarScript(updTramitesEncontrados, "showfrmConfirmarNuevaTransmision();");
                    }
                    else
                    {
                        lblError.Text = "Dele seleccionar un trámite para poder confirmar.";
                        this.EjecutarScript(updTramitesEncontrados, "showfrmError();");
                    }
                }
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updCargarDatos, "finalizarCarga();showfrmError();");
            }
        }

        protected void btnNuevaTransmision_Click(object sender, EventArgs e)
        {
            TransferenciasSolicitudesBL bl = new TransferenciasSolicitudesBL();
            ConsultaPadronSolicitudesBL cpBL = new ConsultaPadronSolicitudesBL();

            string url_destino = "";
            int id_solicitud = 0;
            TransferenciasSolicitudesDTO sol = null;
            try
            {
                if (ValidarTramiteSeleccionado())
                {
                    var solAprobada = SeleccionTramitesAprobados.GetTramiteSeleccionado();                    
                    Guid userid = (Guid)Membership.GetUser().ProviderUserKey;

                    var cPadron = CrearCP(solAprobada, userid);

                    if (cPadron != null)
                    {
                        id_solicitud = bl.CrearTransicion(cPadron.IdConsultaPadron, cPadron.CodigoSeguridad, userid, ddlTipoTransmision.SelectedIndex);
                        sol = bl.Single(id_solicitud);
                        if (solAprobada != null)
                        {
                            cpBL.copiarDatos(solAprobada.IdSolicitud, cPadron.IdConsultaPadron, cPadron.CreateUser, solAprobada.IdTipoTramite);
                            bl.copiarDatos(solAprobada.IdSolicitud, id_solicitud, userid, solAprobada.IdTipoTramite);
                            sol.idSolicitudRef = solAprobada.IdSolicitud;
                            url_destino = "~/" + RouteConfig.AGREGAR_TITULAR_TRANSFERENCIA + id_solicitud.ToString();
                        }
                        else                        
                            url_destino = "~/" + RouteConfig.CARGA_PLANCHETA_TRANSMISION + id_solicitud.ToString();

                        string cuit = Membership.GetUser().UserName;
                        ParametrosBL parametrosBL = new ParametrosBL();
                        string _urlESB = parametrosBL.GetParametroChar("Url.Service.ESB");
                        string trata = parametrosBL.GetParametroChar("Trata.Transferencias");
                        bool tad = Convert.ToBoolean(parametrosBL.GetParametroChar("SSIT.NO.TAD"));

                        if (tad)
                        {
                            int idTAD = wsTAD.crearTramiteTAD(_urlESB, cuit, trata, null, Constantes.Sistema, id_solicitud);

                            sol.idTAD = idTAD;
                        }
                        bl.Update(sol);
                    }
                    else
                    {
                        lblError.Text = "No se pudo crear la Consulta al padron.";
                        this.EjecutarScript(updConfirmarNuevaTransmision, "showfrmError();");
                    }
                }
                else
                {
                    lblError.Text = "Dele seleccionar un trámite para poder confirmar.";
                    this.EjecutarScript(updConfirmarNuevaTransmision, "showfrmError();");
                }
            }
            catch (Exception ex)
            {
                if (sol != null)
                    bl.Delete(sol);
                id_solicitud = 0;
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updmpeInfo, "showfrmError();");

            }
            if (url_destino.Length > 0)
                Response.Redirect(url_destino);
        }
    }
}