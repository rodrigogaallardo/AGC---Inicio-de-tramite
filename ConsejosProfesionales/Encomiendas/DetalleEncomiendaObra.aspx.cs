using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ConsejosProfesionales.Encomiendas
{
    public partial class DetalleEncomiendaObra : SecurePage
    {
        EncomiendaBL _encomiendaBL = new EncomiendaBL();

        public EncomiendaBL encomiendaBL
        {
            get { 
                if (_encomiendaBL == null )
                    _encomiendaBL = new EncomiendaBL();
 
                return _encomiendaBL;
            }
        }
        
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
            ScriptManager sm = ScriptManager.GetCurrent(this.Page);

            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(updBotonesAccion, updBotonesAccion.GetType(), "init_JS_updBuscarUbicacion", "init_JS_updBuscarUbicacion();", true);

            }
            if (!IsPostBack)
            {
                CargarDatos(NroTramite); 
            }

        }

      
        private void CargarDatos(int IdEncomienda)
        {
            var encomiendaDTO = encomiendaBL.GetEncomiendaExterna(IdEncomienda);

            CargarDatosEncomienda(encomiendaDTO);
            CargarHistorialEstados(encomiendaDTO.IdEncomienda);
            CargarEstadosPosibles(encomiendaDTO);
        }

        private void CargarEstadosPosibles(EncomiendaExternaDTO dsEncomienda)
        {   

            if (dsEncomienda.IdEstado == 0)
            {

                ddlCambiarEstado.DataTextField = "nom_estado_consejo";
                ddlCambiarEstado.DataValueField = "id_estado";
                ddlCambiarEstado.DataBind();

                ListItem itm4 = new ListItem("Validada por el Consejo", "4");
                ListItem itm5 = new ListItem("Rechazada por el Consejo", "5");

                ddlCambiarEstado.Items.Insert(0, itm5);
                ddlCambiarEstado.Items.Insert(0, itm4);
                ddlCambiarEstado.Items.Insert(0, "");
                lblCambiarEstado.Visible = true;
                ddlCambiarEstado.Visible = true;
                btnCambiarEstado.Visible = true;
            }
            else
            {
                lblCambiarEstado.Visible = false;
                ddlCambiarEstado.Visible = false;
                btnCambiarEstado.Visible = false;
                lblMotivoRechazo.Visible = false;
                txtEncMotivoRechazo.Visible = false;
            }
        }

        private void CargarHistorialEstados(int id_encomienda)
        {
            grdHistorialEstados.DataSource = encomiendaBL.Traer_EncomiendaExt_HistorialEstados(id_encomienda);
            grdHistorialEstados.DataBind();
        }

        #endregion

        private void CargarDatosEncomienda(EncomiendaExternaDTO ds)
        {
            lblFechaEncomienda.Text = ds.FechaEncomienda.ToShortDateString();
            lblEstado.Text = ds.Estado.NomEstado;

            lblEncMotivoRechazo.Text = ds.MotivoRechazo;
            if (!string.IsNullOrEmpty(lblEncMotivoRechazo.Text))
                divEncMotivoRechazo.Visible = true;
            //Deshabilita o habilita los botones según su estado.
            if (ds.Bloqueada)
            {
                ddlCambiarEstado.Enabled = false;
                btnCambiarEstado.Visible = false;
            }

            lblEncNumero.Text = ds.nroTramite.ToString();
            lblEncDireccion.Text = ds.Direccion.direccion;

            var ubicacion = ds.EncomiendaExternaUbicaciones.FirstOrDefault();

            lblEncSMP.Text = ubicacion.Ubicacion.Seccion + " / " + ubicacion.Ubicacion.Manzana + " / " + ubicacion.Ubicacion.Parcela;
            lblEncFecha.Text = ds.FechaEncomienda.ToShortDateString();
            lblEncProfnya.Text = ds.ProfesionalDTO.Nombre + " " + ds.ProfesionalDTO.Apellido;
            lblEncProfMatricula.Text = ds.ProfesionalDTO.Matricula;
            lblEncDGROC.Text = ds.NroDGROC;
            lblEncCertificado.Text = !string.IsNullOrEmpty(ds.TipoTramiteDescripcion) ? ds.TipoTramiteDescripcion.ToUpper() : "";

            repeater_EncComitentes.DataSource = ds.Titulares;
            repeater_EncComitentes.DataBind();

            repeater_EncRLegal.DataSource = ds.EncomiendaExternaTitularesPersonasJuridicasPersonasFisicas;
            repeater_EncRLegal.DataBind();

            if (ds.EncomiendaExternaTitularesPersonasJuridicasPersonasFisicas.Count() < 1)
                divEncRLegal.Visible = false;

            lblTipoTramite.Text = Enum.GetName(typeof(Constantes.TipoCertificado), TipoCertificado);

            var certf  = TraerCertificado(ds.IdTipoTramite,  ds.nroTramite);

            if (certf.Any())
            {
                string url = string.Format(ResolveUrl("~/Reportes/ImprimirEncomiendaExt.aspx") + "?param={0}", certf.FirstOrDefault().item);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tipoTramite"></param>
        /// <param name="nroTramite"></param>
        /// <returns></returns>
        public IEnumerable<CertificadosDTO> TraerCertificado(int tipoTramite, string nroTramite)
        {
            CertificadosBL certificadosBL = new CertificadosBL();
            return certificadosBL.GetByFKNroTipo(nroTramite, tipoTramite);
        }

        protected void btnCambiarEstado_Click(object sender, EventArgs e)
        {
            if (ddlCambiarEstado.SelectedIndex <= 0)
            {
                lblError.Text = "Debe seleccionar el estado por el cual se desea realizar el cambio.";
                this.EjecutarScript(updBotonesAccion, "showfrmError();");
                return;
            }
            if (ddlCambiarEstado.SelectedItem.Value == "5" && (string.IsNullOrEmpty(txtEncMotivoRechazo.Text) || txtEncMotivoRechazo.Text.Length < 10))
            {
                if (string.IsNullOrEmpty(txtEncMotivoRechazo.Text))
                {
                    lblError.Text = "Por favor aclare el motivo por el cual la encomienda no puede ser validada. Tenga en cuenta que el Profesional recibirá el aviso del rechazo y el motivo del mismo.";

                    this.EjecutarScript(updBotonesAccion, "showfrmError();");

                    return;
                }
                if (txtEncMotivoRechazo.Text.Length < 10)
                {
                    lblError.Text = "Debe ingresar un mínimo de 10 caracteres.";
                    this.EjecutarScript(updBotonesAccion, "showfrmError();");
                    return;
                }
            }

            try
            {
                int id_estado = 0;

                int.TryParse(ddlCambiarEstado.SelectedValue, out id_estado);

                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;

                if (ddlCambiarEstado.SelectedItem.Value == "5" && txtEncMotivoRechazo.Text.Length >= 10)
                {
                    string encmotivorechazo = txtEncMotivoRechazo.Text;
                    encomiendaBL.ActualizarDirectorObraMotivoRechazo(NroTramite, encmotivorechazo);
                }
                encomiendaBL.ActualizarEncomiendaEx_Estado(NroTramite, id_estado, userid);

                CargarDatos(NroTramite);

                lblSuccess.Text = "La acción fue realizada correctamente.";
                ScriptManager.RegisterClientScriptBlock(updBotonesAccion, updBotonesAccion.GetType(), "mostrarError", string.Format("mostrarPopup('{0}');", pnlSuccess.ClientID), true);
            }
            catch (Exception ex)
            {

                LogError.Write(ex, ex.Message);
                lblError.Text = Funciones.GetErrorMessage(ex);
                this.EjecutarScript(updBotonesAccion, "showfrmError();");
            }
        }

        protected void lnkCertificado_Command(object sender, CommandEventArgs e)
        {
            LinkButton lnkEliminar = (LinkButton)sender;
            int id_certificado = Convert.ToInt32(lnkEliminar.CommandArgument);
            string url = string.Format(ResolveUrl("~/Reportes/ImprimirEncomiendaExt.aspx") + "?param={0}", id_certificado);
        }
    }
}