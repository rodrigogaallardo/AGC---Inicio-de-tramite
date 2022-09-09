using BusinesLayer.Implementation;
using SSIT.App_Components;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataTransferObject;
using SSIT.Common;

namespace SSIT.Solicitud
{
    public partial class DatosLocal : SecurePage
    {
        SSITSolicitudesBL solBL = new SSITSolicitudesBL();
        SSITSolicitudesUbicacionesBL solUbicBL = new SSITSolicitudesUbicacionesBL();
        UbicacionesBL ubicBL = new UbicacionesBL();
        SSITSolicitudesDatosLocalBL datosLocalBL = new SSITSolicitudesDatosLocalBL();
        CallesBL callesBL = new CallesBL();

        private int id_solicitud
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
        private int id_tipo_tramite
        {
            get
            {
                int ret = 0;
                int.TryParse(hid_id_tipo_tramite.Value, out ret);
                return ret;
            }
            set
            {
                hid_id_tipo_tramite.Value = value.ToString();
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager sm = ScriptManager.GetCurrent(this);

            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(updDatosLocal, updDatosLocal.GetType(), "init_JS_updDatosLocal", "init_JS_updDatosLocal();", true);
            }


            if (!IsPostBack)
            {
                hid_DecimalSeparator.Value = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                hid_return_url.Value = Request.Url.AbsoluteUri;
                ComprobarSolicitud();
            }

        }


        private void ComprobarSolicitud()
        {
            if (Page.RouteData.Values["id_solicitud"] != null)
            {
                this.id_solicitud = Convert.ToInt32(Page.RouteData.Values["id_solicitud"].ToString());

                var sol = solBL.Single(id_solicitud);
                if (sol != null)
                {
                    Guid userid_solicitud = (Guid)Membership.GetUser().ProviderUserKey;
                    this.id_tipo_tramite = sol.IdTipoTramite;

                    if (userid_solicitud != sol.CreateUser)
                        Server.Transfer("~/Errores/Error3002.aspx");
                    else
                    {
                        if (!(sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.INCOM ||
                                sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.COMP ||
                                sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF ||
                                sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO))
                        {
                            Server.Transfer("~/Errores/Error3003.aspx");
                        }
                    }
                }
                else
                    Server.Transfer("~/Errores/Error3004.aspx");

            }
            else
                Server.Transfer("~/Errores/Error3001.aspx");
        }

        protected void btnCargarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                CargarDatos(this.id_solicitud);
                CargarMapas(this.id_solicitud);
                this.EjecutarScript(updCargarDatos, "finalizarCarga();");

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updCargarDatos, "finalizarCarga();showfrmError();");
            }

        }

        private void CargarMapas(int IdSolicitud)
        {

            List<SSITSolicitudesUbicacionesDTO> lstUbic = solUbicBL.GetByFKIdSolicitud(IdSolicitud).ToList();

            foreach (var item in lstUbic)
            {
                var ubicacion = ubicBL.Single(item.IdUbicacion.Value);
                String dir = "";
                foreach (var p in item.SSITSolicitudesUbicacionesPuertasDTO)
                {
                    dir  = callesBL.GetNombre(p.CodigoCalle, p.NroPuerta) + " " + p.NroPuerta;
                    break;
                }
                
                
                imgMapa1.ImageUrl = Funciones.GetUrlMapa(ubicacion.Seccion.Value, ubicacion.Manzana, ubicacion.Parcela, dir);
                imgMapa2.ImageUrl = Funciones.GetUrlCroquis(ubicacion.Seccion.Value, ubicacion.Manzana, ubicacion.Parcela, dir);

            }

            updDatosLocal.Update();

        }

        private void CargarDatos(int IdSolicitud)
        {

            SSITSolicitudesDatosLocalDTO dl = datosLocalBL.Single(IdSolicitud);
            if (dl != null)
            {
                txtSuperficieCubierta.Text = dl.superficie_cubierta_dl.ToString();
                txtSuperficieDescubierta.Text = dl.superficie_descubierta_dl.ToString();

                txtSuperficieTotal.Text = Convert.ToString(dl.superficie_cubierta_dl + dl.superficie_descubierta_dl );
            }

        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {


            Guid userid = Functions.GetUserid();

            try
            {
                decimal superficie_cubierta = 0;
                decimal superficie_descubierta = 0;
                decimal.TryParse(txtSuperficieCubierta.Text, out superficie_cubierta);
                decimal.TryParse(txtSuperficieDescubierta.Text, out superficie_descubierta);

                SSITSolicitudesDatosLocalDTO datosLocal = datosLocalBL.Single(this.id_solicitud);

                if(datosLocal == null)
                {
                    datosLocal = new SSITSolicitudesDatosLocalDTO();
                    datosLocal.CreateUser = userid;
                    datosLocal.CreateDate = DateTime.Now;
                }
                else
                {
                    datosLocal.LastUpdateUser = userid;
                    datosLocal.LastUpdateDate = DateTime.Now;
                }

                datosLocal.IdSolicitud = this.id_solicitud;
                datosLocal.superficie_cubierta_dl = superficie_cubierta;
                datosLocal.superficie_descubierta_dl = superficie_descubierta;

                datosLocalBL.Update(datosLocal);

                string url = "";
                if (hid_return_url.Value.Contains("Editar"))
                {
                    if (this.id_tipo_tramite == (int)Constantes.TipoTramite.PERMISO)
                        url = string.Format("~/" + RouteConfig.VISOR_SOLICITUD_PERMISO_MC + "{0}", id_solicitud);
                }
                else
                {
                    url = string.Format("~/" + RouteConfig.AGREGAR_RUBROS_SOLICITUD_PERMISO_MC + "{0}", id_solicitud);
                }

                if(url.Length > 0)
                    Response.Redirect(url);
            }

            catch (Exception ex)
            {
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updBotonesGuardar, "showfrmError();");
            }
        }

    }
}