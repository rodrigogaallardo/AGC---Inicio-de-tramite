using System;
using System.Web.UI;
using System.Configuration;
using System.Web.Security;
using System.Data;
using BusinesLayer.Implementation;
using System.Collections.Generic;
using System.Linq;
using SSIT.App_Components;
using StaticClass;
using DataTransferObject;
using ExternalService;

namespace SSIT.Solicitud.Transferencia
{
    public partial class InicioTramite : BasePage
    {
   
        protected override void OnUnload(EventArgs e)
        {            
            base.OnUnload(e);
        }

        protected void lnkContinuar_Click(object sender, EventArgs e)
        {
            TransferenciasSolicitudesBL bl = new TransferenciasSolicitudesBL();

            lblError.Text = "";
            
            int id_cpadron = 0;
            int id_solicitud = 0;
            TransferenciasSolicitudesDTO sol = null;
            if (Page.IsValid)
            {
                int.TryParse(txtNroEncomienda.Text, out id_cpadron);
                
                try
                {
                    Guid userid = (Guid)Membership.GetUser().ProviderUserKey;

                    id_solicitud = bl.CrearTransferencia(id_cpadron, txtCodigoSeguridad.Text.ToUpper(), userid);
                    sol = bl.Single(id_solicitud);
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
                catch (Exception ex)
                {
                    if (sol != null)
                        bl.Delete(sol);
                    id_solicitud = 0;
                    LogError.Write(ex, ex.Message);
                    lblError.Text = ex.Message;
                    this.EjecutarScript(updmpeInfo, "showfrmError();");

                }
                string pagina_destino = "";
                if (id_solicitud > 0)
                {
                    pagina_destino = "~/" + RouteConfig.AGREGAR_TITULAR_TRANSFERENCIA + "/" + id_solicitud;
                    Response.Redirect(pagina_destino);
                }
            }

        }
    }
}