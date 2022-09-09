using BusinesLayer.Implementation;
using DataTransferObject;
using ExternalService;
using SSIT.App_Components;
using StaticClass;
using System.Linq;
using System;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Solicitud.Consulta_Padron
{
    public partial class CargarPlano : BasePage
    {
        public int IdSolicitud
        {
            get
            {
                return Convert.ToInt32(Page.RouteData.Values["id_solicitud"]);             
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                hid_return_url.Value = Request.Url.AbsoluteUri;
                hid_extension.Value = "*";
            }
        }
        protected void btnCargarDatos_Click(object sender, EventArgs e)
        {
            try
            {
                ComprobarEncomienda();
                CargarTiposDePlanos();
                CargarPlanos(IdSolicitud);

                this.EjecutarScript(updCargarDatos, "finalizarCarga();");

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                this.EjecutarScript(updCargarDatos, "showfrmError();finalizarCarga();");
            }

        }
        private void ComprobarEncomienda()
        {
            if ( Page.RouteData.Values.Count > 0 && !string.IsNullOrEmpty(Page.RouteData.Values["id_solicitud"].ToString()))
            {
                ConsultaPadronSolicitudesBL encomiendaBL = new ConsultaPadronSolicitudesBL();
                var enc = encomiendaBL.Single(IdSolicitud);
                if (enc != null)
                {
                    /*Falta el userID y hacer overload de getuserid con el tipo de tramite*/
                    Guid userid_solicitud = (Guid)Membership.GetUser().ProviderUserKey;

                    if (userid_solicitud != enc.CreateUser)
                        Server.Transfer("~/Errores/Error3002.aspx");
                }
                else
                    Server.Transfer("~/Errores/Error3004.aspx");

            }
            else
                Server.Transfer("~/Errores/Error3001.aspx");
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/" + RouteConfig.VISOR_CPADRON + "{0}", IdSolicitud));
        }

        private void CargarTiposDePlanos()
        {
            TiposDeDocumentosRequeridosBL blTipos = new TiposDeDocumentosRequeridosBL();

            TipoDropDown.DataValueField = "id_tdocreq";
            TipoDropDown.DataTextField = "nombre_tdocreq";
            TipoDropDown.DataSource = blTipos.GetVisibleSSITXTipoTramite((int)Constantes.TipoDeTramite.ConsultaPadron); 
            TipoDropDown.DataBind();
            TipoDropDown.Focus();            
        }

        private void CargarPlanos(int IdSolicitud)
        {
            ConsultaPadronDocumentosAdjuntosBL blPlanos = new ConsultaPadronDocumentosAdjuntosBL();
            var elements = blPlanos.GetByFKIdConsultaPadron(IdSolicitud);
            grdPlanos.DataSource = elements.ToList(); 
            grdPlanos.DataBind();
        }

        protected void lnkEliminar_Command(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkEliminar = (LinkButton)sender;
                int id_encomienda_plano = Convert.ToInt32(lnkEliminar.CommandArgument);
                ConsultaPadronDocumentosAdjuntosBL blPlanos = new ConsultaPadronDocumentosAdjuntosBL();
                var dto = blPlanos.Single(id_encomienda_plano);
                blPlanos.Delete(dto);
                CargarPlanos(IdSolicitud);
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;                
                this.EjecutarScript(updPnlCargarPlano, "showfrmError();");
            }
        }

        protected void btnCargarPlano_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultaPadronDocumentosAdjuntosBL blPlanos = new ConsultaPadronDocumentosAdjuntosBL();
                
                int idTipoDePlano = Convert.ToInt32(TipoDropDown.SelectedValue);
                string nombre = hid_filename_documento.Value;
                string savedFileName = Constantes.PathTemporal + hid_filename_documento.Value;
                
                if (!File.Exists(savedFileName))
                    throw new Exception(Errors.FILE_NO_TRANSFERIDO);

                byte[] documento = File.ReadAllBytes(savedFileName);

                File.Delete(savedFileName);

                //Elimina los planos con mas de 2 días para mantener el directorio limpio.
                string[] lstArchs = Directory.GetFiles(Constantes.PathTemporal);
                foreach (string arch in lstArchs)
                {
                    DateTime fechaCreacion = File.GetCreationTime(arch);
                    if (fechaCreacion < DateTime.Now.AddDays(-2))
                        File.Delete(arch);
                }

                int id_file=0;
                ExternalServiceFiles externalServiceFiles = new ExternalServiceFiles();
                id_file = externalServiceFiles.addFile(nombre, documento);
                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                ConsultaPadronDocumentosAdjuntosDTO dto = new ConsultaPadronDocumentosAdjuntosDTO();
                dto.IdConsultaPadron = IdSolicitud;
                dto.IdFile = id_file;
                dto.IdTipodocumentoRequerido = idTipoDePlano;
                dto.NombreArchivo = nombre;
                dto.CreateUser = userid;
                dto.CreateDate = DateTime.Now;
                blPlanos.Insert(dto);

                CargarPlanos(IdSolicitud);                
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                this.EjecutarScript(updPnlCargarPlano, "showfrmError();");
            }
        }

    }
}