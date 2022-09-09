using BusinesLayer.Implementation;
using DataTransferObject;
//using SelectPdf;
using ExternalService;
using ExternalService.ws_interface_AGC;
using Reporting;
using SSIT.App_Components;
using SSIT.Common;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IronPdf;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SSIT.Solicitud
{
    public partial class VisorTramite2 : SecurePage
    {
        private int id_solicitud
        {
            get
            {
                int ret = 0;
                int.TryParse(Convert.ToString(Page.RouteData.Values["id_solicitud"]), out ret);
                return ret;
            }
            set
            {
                hid_id_solicitud.Value = value.ToString();
            }

        }
        private int id_estado
        {
            get
            {
                int ret = 0;
                int.TryParse(hid_id_estado.Value, out ret);
                return ret;
            }
            set
            {
                hid_id_estado.Value = value.ToString();
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarComboZonas();
                CargarDatos();
                visRubros.CargarDatos(id_solicitud);
            }
        }

        private void CargarDatos()
        {
            SSITSolicitudesNuevasBL solBL = new SSITSolicitudesNuevasBL();
            SSITSolicitudesNuevasDTO sol = solBL.Single(id_solicitud);

            CargarCabecera(sol);

            txtTitulares.Text = sol.Nombre_RazonSocial;
            txtCuit.Text = sol.Cuit != null ? sol.Cuit.ToString() : "";
            txtProfesional.Text = sol.Nombre_Profesional;
            txtMatricula.Text = sol.Matricula;
            txtCalle.Text = sol.Nombre_calle;
            txtAltura.Text = sol.Altura_calle != null ? sol.Altura_calle.ToString() : "";
            txtPiso.Text = sol.Piso;
            txtUnidad.Text = sol.UnidadFuncional;
            txtPartidaH.Text = sol.NroPartidaHorizontal != null ? sol.NroPartidaHorizontal.ToString() : "";
            txtPartidaM.Text = sol.NroPartidaMatriz != null? sol.NroPartidaMatriz.ToString() : "";
            txtActividad.Text = sol.Descripcion_Actividad;
            txtSuperficie.Text = sol.Superficie != null ? sol.Superficie.ToString() : "";
            ddlZonaMixtura.SelectedValue = sol.CodZonaHab;

            if(sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.ETRA)
            {
                pnlDatos.Enabled = false;
            }

        }

        private void CargarComboZonas()
        {
            ZonasPlaneamientoBL zpBL = new ZonasPlaneamientoBL();

            ddlZonaMixtura.Items.Add("1");
            ddlZonaMixtura.Items.Add("2");
            ddlZonaMixtura.Items.Add("3");
            ddlZonaMixtura.Items.Add("4");

            /*var lstZonasPlaneamiento = zpBL.GetAll();
            
            ddlZonaMixtura.DataSource = lstZonasPlaneamiento;
            ddlZonaMixtura.DataTextField = "CodZonaPla";
            ddlZonaMixtura.DataValueField = "IdZonaPlaneamiento";
            ddlZonaMixtura.DataBind();        */

        }

       
        private void CargarCabecera(SSITSolicitudesNuevasDTO sol)
        {
            id_estado = sol.IdEstado;
            lblNroSolicitud.Text = sol.IdSolicitud.ToString();
            lblTipoTramite.Text = sol.TipoTramiteDescripcion;

            EngineBL engBL = new EngineBL();


            lblEstadoSolicitud.Text = sol.TipoEstadoSolicitudDTO.Descripcion;
            

            updEstadoSolicitud.Update();

            #region botones
            btnBandeja.PostBackUrl = "~/" + RouteConfig.BANDEJA_DE_ENTRADA;
            divbtnConfirmarTramite.Visible = false;
            if (id_estado == (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
                divbtnConfirmarTramite.Visible = true;

            divbtnImprimirSolicitud.Visible = false;
            btnImprimirSolicitud.NavigateUrl = string.Format("~/" + RouteConfig.IMPRIMIR_SOLICITUD2 + "{0}", Functions.ConvertToBase64String(id_solicitud));
            if (id_estado == (int)Constantes.TipoEstadoSolicitudEnum.ETRA)
                divbtnImprimirSolicitud.Visible = true;

            divbtnAnularTramite.Visible = false;
            if (id_estado == (int)Constantes.TipoEstadoSolicitudEnum.INCOM ||
                id_estado == (int)Constantes.TipoEstadoSolicitudEnum.COMP ||
                id_estado == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO ||
                id_estado == (int)Constantes.TipoEstadoSolicitudEnum.SUSPEN ||
                id_estado == (int)Constantes.TipoEstadoSolicitudEnum.ETRA ||
                id_estado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF)
                divbtnAnularTramite.Visible = true;

            #endregion

            updEstadoSolicitud.Update();
        }
        protected void btnConfirmarTramite_Click(object sender, EventArgs e)
        {
            try
            {
                SSITSolicitudesNuevasBL blSol = new SSITSolicitudesNuevasBL();
                SSITSolicitudesNuevasDTO sol = blSol.Single(id_solicitud);

                visRubros.ValidarRubros();
                // if (blSol.confirmarSolicitud(id_solicitud, (Guid)usuario.ProviderUserKey))
                //{
                int altura = 0;
                    int ph = 0;
                    int pm = 0;
                    decimal super = 0;
                    sol = blSol.Single(id_solicitud);

                    int.TryParse(Convert.ToString(txtAltura.Text), out altura);
                    sol.Altura_calle = altura;
                    // sol.CodZonaHab = ddlZonaMixtura.SelectedIndex();
                    sol.Cuit = txtCuit.Text;
                    sol.Descripcion_Actividad = txtActividad.Text;
                    sol.Matricula = txtMatricula.Text;
                    sol.Nombre_calle = txtCalle.Text;
                    sol.Nombre_Profesional = txtProfesional.Text;
                    sol.Nombre_RazonSocial = txtTitulares.Text;

                    int.TryParse(Convert.ToString(txtPartidaH.Text), out ph);
                    sol.NroPartidaHorizontal = ph;

                    int.TryParse(Convert.ToString(txtPartidaM.Text), out pm);
                    sol.NroPartidaMatriz = pm;

                    sol.Piso = txtPiso.Text;

                    decimal.TryParse(Convert.ToString(txtSuperficie.Text), out super);
                    sol.Superficie = super;

                    sol.UnidadFuncional = txtUnidad.Text;
                    sol.IdEstado = (int)Constantes.TipoEstadoSolicitudEnum.ETRA;

                    sol.LastUpdateDate = DateTime.Now;
                    sol.LastUpdateUser = (Guid)Membership.GetUser().ProviderUserKey;
                    sol.CodZonaHab = ddlZonaMixtura.SelectedValue;

                    blSol.Update(sol);
                    sol = blSol.Single(id_solicitud);

                    MembershipUser usuario = Membership.GetUser();
                    if (sol.idTAD != null)
                    {
                        Functions.enviarParticipantes(sol);
                        Functions.enviarCambio(sol);
                    }

                

                    //lblCodigoDeSeguridad.Text = sol.CodigoSeguridad.ToString();
                    CargarCabecera(sol);
                    //visPresentacion.CargarDatos(sol, encomiendas);
                    if (id_solicitud > Constantes.SOLICITUDES_NUEVAS_MAYORES_A)
                    {                     

                        SSITSolicitudesNotificacionesBL notifBL = new SSITSolicitudesNotificacionesBL();
                        int idMotivoNotificacion = (int)Constantes.MotivosNotificaciones.SolicitudConfirmada;
                        string motivo = notifBL.getMotivoById(idMotivoNotificacion);

                        string asunto = "Sol: " + id_solicitud + " - " + motivo;


                        MailMessages mailer = new MailMessages();

                        string htmlBody = mailer.MailSolicitudNueva(sol.IdSolicitud, sol.CodigoSeguridad, sol.TipoTramiteDescripcion);
                        EmailServiceBL mailService = new EmailServiceBL();
                        EmailEntity emailEntity = new EmailEntity();
                        emailEntity.Email = usuario.Email;
                        emailEntity.Html = htmlBody;
                        emailEntity.Asunto = asunto;
                        emailEntity.IdEstado = (int)ExternalService.TiposDeEstadosEmail.PendienteDeEnvio;
                        emailEntity.IdTipoEmail = (int)ExternalService.TiposDeMail.Generico;
                        emailEntity.IdOrigen = (int)ExternalService.MailOrigenes.SSIT;
                        emailEntity.CantIntentos = 3;
                        emailEntity.CantMaxIntentos = 3;
                        emailEntity.FechaAlta = DateTime.Now;
                        emailEntity.Prioridad = 1;
                        emailEntity.IdEmail = mailService.SendMail(emailEntity);

                        

                        try
                        {
                            notifBL.InsertNotificacionByIdSolicitud(sol, emailEntity.IdEmail, idMotivoNotificacion);
                        }
                        catch (Exception) { }
                       
                    }
                //}
                nroSolicitudModal.Text = sol.IdSolicitud.ToString();
                lblCodSeguridad.Text = sol.CodigoSeguridad.ToString();
                pnlDatos.Enabled = false;
                updEstadoSolicitud.Update();
                ScriptManager.RegisterStartupScript(udpConfirmarSolcitud, udpConfirmarSolcitud.GetType(), "showModalConfirmarSolicitud", "showModalConfirmarSolicitud();", true);
            }
            catch (ValidationException ex)
            {
                lblError.Text = ex.Message;
                this.EjecutarScript(udpError, "showfrmError();");
            }
            catch (Exception ex)
            {
                LogError.Write(ex);
                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(udpError, udpError.GetType(), "showfrmError", "showfrmError();", true);
            }
            finally
            {
                updCargarDatos.Update();
            }


        }

        protected void btnAnularTramite_Click(object sender, EventArgs e)
        {
            try
            {
                SSITSolicitudesNuevasBL blSol = new SSITSolicitudesNuevasBL();
                var sol = blSol.Single(id_solicitud);
                Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
                if (blSol.anularSolicitud(id_solicitud, userid))
                {
                    sol = blSol.Single(id_solicitud);
                    CargarCabecera(sol);
                    CargarDatos();
                    ScriptManager.RegisterStartupScript(updEstadoSolicitud, updEstadoSolicitud.GetType(), "hidefrmConfirmarAnulacion2", "hidefrmConfirmarAnulacion2();", true);
                    updEstadoSolicitud.Update();
                }
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                lblError.Text = ex.Message;
                ScriptManager.RegisterStartupScript(updEstadoSolicitud, updEstadoSolicitud.GetType(), "showfrmError", "showfrmError();", true);
            }
        }

       
    }
}