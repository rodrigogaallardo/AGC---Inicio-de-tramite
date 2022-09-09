using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsejosProfesionales.Encomiendas
{
    public partial class ActualizarEncomiendasAnt : SecurePage
    {
        /// <summary>
        /// 
        /// </summary>
        public int NroTramite
        {

            get
            {
                return Convert.ToInt32(this.RouteData.Values["id_encomienda"].ToString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int TipoCertificado
        {
            get
            {
                return Convert.ToInt32(this.RouteData.Values["tipo_tramite"].ToString());
            }
        }
        #region carga inicial
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Membership.GetUser() == null)
                    Response.Redirect("~/Default.aspx");

                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                hid_id_encomienda.Value = NroTramite.ToString();
                CargarDatosTramite(NroTramite);
                CargarHistorialEstados(NroTramite);
                CargarEstadosPosibles(NroTramite);
                EncomiendaBL encomiendaBL = new EncomiendaBL();
                var dsSol = encomiendaBL.GetEncomiendaAntenas(NroTramite);
                    
                btnImprimirComprobante.NavigateUrl = string.Format("~/Reportes/ImprimirComprobanteAnt.aspx?id_encomienda={0}", dsSol.IdEncomienda);

                ddlCambiarEstado.ClearSelection();
            }

        }

        private void CargarEstadosPosibles(int id_encomienda)
        {
            EncomiendaBL encomiendaBL = new EncomiendaBL();
            EncomiendaEstadosBL estadosBL = new EncomiendaEstadosBL(); 
            var dsEncomienda = encomiendaBL.GetEncomiendaAntenas(id_encomienda);


            ddlCambiarEstado.DataSource = estadosBL.TraerEncomiendaExtEstadosSiguientes((Guid)Membership.GetUser().ProviderUserKey, dsEncomienda.IdEstado, 0);
            ddlCambiarEstado.DataTextField = "NomEstado";
            ddlCambiarEstado.DataValueField = "IdEstado";
            ddlCambiarEstado.DataBind();

            ListItem itm = new ListItem("(Seleccione el estado)", "-1");
            ddlCambiarEstado.Items.Insert(0, itm);

        }

        private void CargarDatosTramite(int id_encomienda)
        {
            CargarDatosEncomienda(id_encomienda);
        }

        private void CargarHistorialEstados(int id_encomienda)
        {
            EncomiendaBL encomiendaBL = new EncomiendaBL();
                                        
            grdHistorialEstados.DataSource =  encomiendaBL.GetHistorial(id_encomienda);
            grdHistorialEstados.DataBind();
        }

        #endregion

        private void CargarDatosEncomienda(int id_encomienda)
        {
            EncomiendaBL encomiendaBL = new EncomiendaBL();

            EncomiendaDTO ds = encomiendaBL.GetEncomiendaAntenas(id_encomienda);

            DateTime FechaEncomienda = ds.CreateDate;
            lblFechaEncomienda.Text = FechaEncomienda.ToShortDateString();
            lblEstado.Text = ds.Estado.NomEstado;
            Guid userid_Encomienda = ds.CreateUser;
            //Deshabilita o habilita los botones según su estado.

            if (ds.IdEstado == (int)Constantes.Estado_Encomienda_Antenas.Aprobado)
                btnImprimirComprobante.Visible = true;
            else
                btnImprimirComprobante.Visible = false;

            lblTipoTramite.Text = ds.TipoTramite.DescripcionTipoTramite;
            lblNroEncomienda.Text = ds.IdEncomienda.ToString();

            string urlReporteEncomienda = "http://" + HttpContext.Current.Request.Url.Authority + ResolveUrl(string.Format("~/Reportes/ImprimirEncomiendaAnt.aspx?id_encomienda={0}", id_encomienda));
            hid_url_reporte.Value = urlReporteEncomienda;

        }

        protected void btnCambiarEstado_Click(object sender, EventArgs e)
        {
            lblmpeInfo.Text = "";
            if (ddlCambiarEstado.SelectedIndex <= 0)
            {
                lblMensajeError.Text = "Debe seleccionar el estado por el cual se desea realizar el cambio.";
                mvBotonesAccion.ActiveViewIndex = 2;
                ScriptManager.RegisterClientScriptBlock(updBotonesAccion, updBotonesAccion.GetType(), "mostrarError", "moverFinal();", true);
                return;
            }

            try
            {
                int id_encomienda = int.Parse(hid_id_encomienda.Value);
                int id_estado = int.Parse(ddlCambiarEstado.SelectedValue);
                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;

                ActualizarEncomiendaAnt_Estado(id_encomienda, id_estado, userid);

                CargarDatosEncomienda(int.Parse(hid_id_encomienda.Value));
                CargarEstadosPosibles(int.Parse(hid_id_encomienda.Value));
                CargarHistorialEstados(int.Parse(hid_id_encomienda.Value));
            }
            catch (Exception ex)
            {

                lblMensajeError.Text = ex.Message;
                mvBotonesAccion.ActiveViewIndex = 2;
            }

            if (lblmpeInfo.Text.Length == 0)
            {
                mvBotonesAccion.ActiveViewIndex = 1;
                lblMensajeInfo.Text = "La encomienda fue actualizada exitosamente.";
            }

            ScriptManager.RegisterClientScriptBlock(updBotonesAccion, updBotonesAccion.GetType(), "mostrarError", "moverFinal();", true);

        }

        protected void btnMensajeVerIndex0_Click(object sender, EventArgs e)
        {
            mvBotonesAccion.ActiveViewIndex = 0;
            ScriptManager.RegisterClientScriptBlock(updBotonesAccion, updBotonesAccion.GetType(), "mostrarError", "moverFinal();", true);
        }

        protected void lnkCertificado_Command(object sender, CommandEventArgs e)
        {
            LinkButton lnkEliminar = (LinkButton)sender;
            int id_certificado = Convert.ToInt32(lnkEliminar.CommandArgument);
            //Imprimir el certificado
        }

        private void ActualizarEncomiendaAnt_Estado(int id_encomienda, int id_estado, Guid userid)
        {
            EncomiendaBL encomiendaBL = new EncomiendaBL();
            encomiendaBL.ActualizarEncomiendaAnt_Estado(id_encomienda,id_estado, userid);
        }

        private string ConvertToBase64String(int value)
        {
            byte[] str1Byte = Encoding.ASCII.GetBytes(Convert.ToString(value));
            string base64 = Convert.ToBase64String(str1Byte);
            return base64;
        }

        private int ConvertFromBase64StringToInt32(string value)
        {
            byte[] bValue = Convert.FromBase64String(value);
            string res = Encoding.ASCII.GetString(bValue);
            int ret = int.Parse(res);

            return ret;

        }
    }
}