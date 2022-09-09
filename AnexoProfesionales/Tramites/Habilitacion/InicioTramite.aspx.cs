using AnexoProfesionales.App_Components;
using BusinesLayer.Implementation;
using StaticClass;
using System;
using System.Web.Security;
using System.Web.UI;

namespace AnexoProfesionales.Tramites.Habilitacion
{
    public partial class InicioTramite : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager sm = ScriptManager.GetCurrent(this);

            if (sm.IsInAsyncPostBack)
            {
                //ScriptManager.RegisterStartupScript(updpnlAgregarDocumentos, updpnlAgregarDocumentos.GetType(), "init_Js_updpnlAgregarDocumentos", "init_Js_updpnlAgregarDocumentos();", true);
            }
  
        }

        protected void btnCargarDatostramite_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(updCargarDatos, updCargarDatos.GetType(), "finalizarCarga", "finalizarCarga();", true);

            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updCargarDatos, updCargarDatos.GetType(), "finalizarCarga", "finalizarCarga();showfrmError();", true);
            }

        }

        protected void lnkContinuar_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblInfor.Text = "";
            int id_encomienda = 0;
            int id_solicitud = 0;
            string url_destino = "";
            try
            {
                int.TryParse(txtNroSolicitud.Text, out id_solicitud);
                EncomiendaBL blEnc = new EncomiendaBL();
                id_encomienda = blEnc.CrearEncomienda(id_solicitud, txtCodigoSeguridad.Text.Trim().ToUpper(), (Guid)Membership.GetUser().ProviderUserKey);

                var enc_nva = blEnc.Single(id_encomienda);
                
                if (enc_nva.EncomiendaUbicacionesDTO == null)
                    url_destino = string.Format("~/" + RouteConfig.AGREGAR_ENCOMIENDA_UBICACION + "{0}", id_encomienda);
                else
                    url_destino = string.Format("~/" + RouteConfig.VISOR_ENCOMIENDA + "{0}", id_encomienda);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                this.EjecutarScript(updContinuar, "showfrmError();");
            }

            if (lblError.Text.Trim().Length == 0)
                Response.Redirect(url_destino);

        }
    }
}