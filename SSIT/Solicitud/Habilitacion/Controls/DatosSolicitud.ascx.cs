using BusinesLayer.Implementation;
using DataTransferObject;
using ExternalService;
using StaticClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace SSIT.Solicitud.Habilitacion.Controls
{
    public partial class DatosSolicitud : System.Web.UI.UserControl
    {
        private int id_solicitud
        {
            get
            {
                int ret = 0;
                int.TryParse(Page.RouteData.Values["id_solicitud"].ToString(), out ret);
                return ret;
            }
        }
     

        private static bool _Editable;
        [Browsable(true),
        Category("Appearance"),
        DefaultValue(true),
        Description("Devuelve/Establece si es posible eliminar las ubicaciones.")]
        public bool Editable
        {
            get
            {
                if (ViewState["DatosSolicitud.ascx._Editable"] != null)
                    Editable = Convert.ToBoolean(ViewState["DatosSolicitud.ascx._Editable"]);

                return _Editable;
            }
            set
            {
                // Se debe setear la propiedad editable antes de ejecutar la carga de datos
                ViewState["DatosSolicitud.ascx._Editable"] = value;
                _Editable = value;
            }
        }

        private static bool _EditableTitulares;
        [Browsable(true),
        Category("Appearance"),
        DefaultValue(true),
        Description("Devuelve/Establece sies posible eliminar los titulares.")]
        public bool EditableTitulares
        {
            get
            {
                if (ViewState["DatosSolicitud.ascx._Editable"] != null)
                    EditableTitulares = Convert.ToBoolean(ViewState["DatosSolicitud.ascx._EditableTitulares"]);

                return _EditableTitulares;
            }
            set
            {
                // Se debe setear la propiedad editable antes de ejecutar la carga de datos
                ViewState["DatosSolicitud.ascx._EditableTitulares"] = value;
                _EditableTitulares = value;
            }
        }

        private static bool _EditableExpRel;
        [Browsable(true),
        Category("Appearance"),
        DefaultValue(true),
        Description("Devuelve/Establece si es posible editar el expediente relacionado.")]
        public bool EditableExpRel
        {
            get
            {
                if (ViewState["DatosSolicitud.ascx._EditableExpRel"] != null)
                    EditableExpRel = Convert.ToBoolean(ViewState["DatosSolicitud.ascx._EditableExpRel"]);

                return _EditableExpRel;
            }
            set
            {
                // Se debe setear la propiedad editable antes de ejecutar la carga de datos
                ViewState["DatosSolicitud.ascx._EditableExpRel"] = value;
                _EditableExpRel = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ScriptManager.RegisterStartupScript(udpcontacto, udpcontacto.GetType(), "init_JS_updDatosContacto", "init_JS_updDatosContacto();", true);
            }
             ScriptManager.RegisterStartupScript(udpcontacto, udpcontacto.GetType(), "init_JS_updDatosContacto", "init_JS_updDatosContacto();", true);
            
        }
        public void CargarDatos(SSITSolicitudesDTO ssitDTO)
        {
            int nroSolReferencia = 0;
            int.TryParse(ConfigurationManager.AppSettings["NroSolicitudReferencia"], out nroSolReferencia);
            

            #region titulares
            visTitulares.CargarDatos(id_solicitud);

            if(ssitDTO.IdTipoTramite == (int)Constantes.TipoTramite.AMPLIACION)
                btnModificarTitulares.PostBackUrl = string.Format("~/" + RouteConfig.EDITAR_TITULAR_SOLICITUD_AMPLIACION + "{0}", id_solicitud);
            else if (ssitDTO.IdTipoTramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                btnModificarTitulares.PostBackUrl = string.Format("~/" + RouteConfig.EDITAR_TITULAR_SOLICITUD_REDISTRIBUCION_USO + "{0}", id_solicitud);
            else
                btnModificarTitulares.PostBackUrl = string.Format("~/" + RouteConfig.EDITAR_TITULAR_SOLICITUD + "{0}", id_solicitud);


            btnModificarTitulares.Visible = _EditableTitulares;
            #endregion
            #region Ubicacion
            visUbicaciones.Editable = false;
            visUbicaciones.CargarDatos(ssitDTO);

            if (ssitDTO.IdTipoTramite == (int)Constantes.TipoTramite.AMPLIACION)
                btnModificarUbicacion.PostBackUrl = string.Format("~/" + RouteConfig.EDITAR_UBICACION_SOLICITUD_AMPLIACION + "{0}", id_solicitud);
            else if (ssitDTO.IdTipoTramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                btnModificarUbicacion.PostBackUrl = string.Format("~/" + RouteConfig.EDITAR_UBICACION_SOLICITUD_REDISTRIBUCION_USO + "{0}", id_solicitud);
            else
                btnModificarUbicacion.PostBackUrl = string.Format("~/" + RouteConfig.EDITAR_UBICACION_SOLICITUD + "{0}", id_solicitud);

            btnModificarUbicacion.Visible = _Editable;
            #endregion
            #region Datos de contacto

            pnlDatosContacto.Enabled = _EditableTitulares;

            if (_EditableTitulares)
            {
                pnlBotonesGuardarRubroEstadio.Style["display"] = "block";
                pnlBotonesGuardar.Style["display"] = "block";
            }
            else
            {
                pnlBotonesGuardarRubroEstadio.Style["display"] = "none";
                pnlBotonesGuardar.Style["display"] = "none";
            }

            if (!string.IsNullOrEmpty(ssitDTO.CodArea != null ? ssitDTO.CodArea.Trim() : "")
                || !string.IsNullOrEmpty(ssitDTO.Prefijo != null ? ssitDTO.Prefijo.Trim() : "")
                || !string.IsNullOrEmpty(ssitDTO.Sufijo != null ? ssitDTO.Sufijo.Trim() : ""))
            {
                pnlDatosAnteriores.Style["display"] = "none";
                txtCodArea.Text = ssitDTO.CodArea;
                txtPrefijo.Text = ssitDTO.Prefijo;
                txtSufijo.Text = ssitDTO.Sufijo;
            }
            else if (!string.IsNullOrEmpty(ssitDTO.Telefono != null ? ssitDTO.Telefono.Trim() : ""))
            {
                pnlDatosAnteriores.Style["display"] = "block";
                lblTlfAnterior.Text = ssitDTO.Telefono;
            }
            else
                pnlDatosAnteriores.Style["display"] = "none";

            SSITSolicitudesBL ssitBL = new SSITSolicitudesBL();
            //if (ssitBL.isEscuela(id_solicitud))
            //{
            //    box_ExpRel.Style["display"] = "block";
            //    txtNumeroExpSade.Enabled = _EditableExpRel;
            //    txtNumeroExpSade.Text = ssitDTO.NroExpedienteSadeRelacionado;

            //    if (_EditableExpRel)
            //    {
            //        pnlBotonesGuardarNumero.Style["display"] = "block";
            //    }
            //    else
            //    {
            //        pnlBotonesGuardarNumero.Style["display"] = "none";
            //    }
            //}
            //else
            //    box_ExpRel.Style["display"] = "none";
            #endregion

            #region Rubro Estadio
            if (ssitDTO.IdSolicitud < nroSolReferencia ||
                (ssitDTO.IdTipoTramite == (int)Constantes.TipoTramite.PERMISO && ssitDTO.IdTipoExpediente == (int)Constantes.TipoDeExpediente.MusicaCanto))
            {

                pnlRubroEstadio.Enabled = _EditableTitulares;
                txtNroExpedienteCAA.Text = ssitDTO.NroExpedienteCAA;
                chkExencionPago.Checked = ssitDTO.ExencionPago;

                EncomiendaBL encBL = new EncomiendaBL();
                var encDTO = encBL.GetByFKIdSolicitud(id_solicitud);

                var encAprobadaDTO = encDTO.Where(x => x.IdEstado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo)
                                                    .OrderByDescending(o => o.IdEncomienda)
                                                    .FirstOrDefault();
                if (encAprobadaDTO != null)
                {
                    RubrosBL rubBL = new RubrosBL();
                    var rubDTO = rubBL.GetByListCodigo(encAprobadaDTO.EncomiendaRubrosDTO.Select(s => s.CodigoRubro).ToList());
                    if (rubDTO.Where(x => x.EsEstadio).Any())
                        box_RubroEstadio.Style["display"] = "block";
                    else
                        box_RubroEstadio.Style["display"] = "none";
                }
                else
                    box_RubroEstadio.Style["display"] = "none";
            }
            #endregion
        }

        protected void btnGuardarContacto_Click(object sender, EventArgs e)
        {
            try
            {
                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;

                SSITSolicitudesBL ssitBL = new SSITSolicitudesBL();
                SSITSolicitudesDTO ssitDTO = ssitBL.Single(id_solicitud);

                string CodArea = txtCodArea.Text.Trim();
                string Prefijo = txtPrefijo.Text.Trim();
                string Sufijo = txtSufijo.Text.Trim();

                ssitDTO.CodArea = CodArea;
                ssitDTO.Prefijo = Prefijo;
                ssitDTO.Sufijo = Sufijo;
                ssitDTO.LastUpdateDate = DateTime.Now;
                ssitDTO.LastUpdateUser = userid;
                ssitBL.Update(ssitDTO);
                udpcontacto.Update();
            }
            catch (Exception ex)
            {
                lblError.Text = Funciones.GetErrorMessage(ex);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showfrmError_DatosSolicitud", "showfrmError_DatosSolicitud();", true);
            }

        }

        protected void lnkGuardarRubroEstadio_Click(object sender, EventArgs e)
        {
            try
            {
                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;

                SSITSolicitudesBL ssitBL = new SSITSolicitudesBL();
                SSITSolicitudesDTO ssitDTO = ssitBL.Single(id_solicitud);

                string NroExpedienteCAA = txtNroExpedienteCAA.Text.Trim();
                bool ExencionPago = chkExencionPago.Checked;

                ssitDTO.NroExpedienteCAA = NroExpedienteCAA;
                ssitDTO.ExencionPago = ExencionPago;
                ssitDTO.LastUpdateDate = DateTime.Now;
                ssitDTO.LastUpdateUser = userid;
                ssitBL.Update(ssitDTO);
                udpcontacto.Update();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showfrm_DatosGuardados", "showfrmDatosGuardados();", true);
            }
            catch (Exception ex)
            {
                lblError.Text = Funciones.GetErrorMessage(ex);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showfrmError_DatosSolicitud", "showfrmError_DatosSolicitud();", true);
            }

        }
        //Se comenta por mantis 0139620: JADHE 53891 - SSIT - quitar exigencia expediente relacionado SADE
        //protected void btnGuardarNumeroExpSade_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ParametrosBL blParam = new ParametrosBL();
        //        ws_ExpedienteElectronico serviceEE = new ws_ExpedienteElectronico();
        //        serviceEE.Url = blParam.GetParametroChar("SGI.Url.Service.ExpedienteElectronico");
        //        string username_servicio_EE= blParam.GetParametroChar("SGI.UserName.Service.ExpedienteElectronico");
        //        string pass_servicio_EE = blParam.GetParametroChar("SGI.Pwd.Service.ExpedienteElectronico");

        //        if(!serviceEE.ExisteExpedienteEnSADE(username_servicio_EE, pass_servicio_EE, txtNumeroExpSade.Text.Trim()))
        //            throw new Exception(Errors.SSIT_NUMERO_EXPEDIENTE_SADE_INEXISTENTE);

        //        Guid userid = (Guid)Membership.GetUser().ProviderUserKey;

        //        SSITSolicitudesBL ssitBL = new SSITSolicitudesBL();
        //        SSITSolicitudesDTO ssitDTO = ssitBL.Single(id_solicitud);


        //        //ssitDTO.NroExpedienteSadeRelacionado = txtNumeroExpSade.Text;
        //        ssitDTO.LastUpdateDate = DateTime.Now;
        //        ssitDTO.LastUpdateUser = userid;
        //        ssitBL.Update(ssitDTO);
        //        udpcontacto.Update();
        //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showfrmDatosGuardados", "showfrmDatosGuardados();", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        lblError.Text = Funciones.GetErrorMessage(ex);
        //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showfrmError_DatosSolicitud", "showfrmError_DatosSolicitud();", true);
        //    }

        //}
    }
}