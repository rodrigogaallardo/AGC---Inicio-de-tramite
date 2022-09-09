using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSIT.App_Components;
using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System.Globalization;
using ExternalService;
using System.Web.UI.HtmlControls;

namespace SSIT.Account
{
    public partial class Register : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager sm = ScriptManager.GetCurrent(this);

            if (sm.IsInAsyncPostBack)
            {
                UpdatePanel updDatosPersonas = (UpdatePanel)CreateUserStep.ContentTemplateContainer.FindControl("updDatosPersonas");
                ScriptManager.RegisterStartupScript(updDatosPersonas, updDatosPersonas.GetType(), "init_JS_updDatosPersonas", "init_JS_updDatosPersonas();", true);

            }


            if (!Page.IsPostBack)
            {
                OnPageLoaded();
            }
            else
            {
                CheckBox chkAceptarTerminos = (CheckBox)CreateUserStep.ContentTemplateContainer.FindControl("chkAceptarTerminos");
                if (chkAceptarTerminos.Checked)
                {
                    Button CreateUserButton = (Button)CreateUserStep.ContentTemplateContainer.FindControl("CreateUserButton");
                    CreateUserButton.Enabled = true;
                }
            }

            var DivcontendorGral = (HtmlControl)Master.FindControl("contendorGral");


            DivcontendorGral.Attributes.Add("class", "container-fluid");
        }


        protected void CreateUserWizard_CreatedUser(object sender, EventArgs e)
        {
           
            CrearProfile();
        }

        //Private members
        private void OnPageLoaded()
        {
            try
            {
                InicializarControles();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "mostrarError", "mostrarPopup();", true);
            }
        }


        private void InicializarControles()
        {

            CargarProvincias();
            CargarLocalidades();
        }


        private void CrearProfile()
        {
            MembershipUser usuarioCreado = Membership.GetUser(CreateUserWizard.UserName);
            if (usuarioCreado == null)
            {
                throw new InvalidOperationException("Debe ingresar el nombre de usuario");
            }
            UsuarioDTO usuario = new UsuarioDTO();
            UsuarioBL usuarioBl = new UsuarioBL();

            if (ModelState.IsValid)
            {

                Control container = CreateUserStep.ContentTemplateContainer;
                Guid userid = (Guid)usuarioCreado.ProviderUserKey;
                string UserName = ControlHelper.GetValue(container, "UserName");
                string Password = ControlHelper.GetValue(container, "Password");
                string Email = ControlHelper.GetValue(container, "Email");

                Label lblEmailSent = (Label)CompleteStep.ContentTemplateContainer.FindControl("lblEmailSent");
                lblEmailSent.Text = Email;

                Control control = container.FindControl("tipoFisica");
                RadioButton tipoFisica = control as RadioButton;
                int? dni = null;;
                int tipoPersonaId = 0;
                long? cuit = null;
                string Apellido = "", Nombre = "", RazonSocial = "";

                if (tipoFisica.Checked)
                {
                    tipoPersonaId = 0;
                    dni = Convert.ToInt32(ControlHelper.GetValue(container, "txtDni"));
                    Apellido = ControlHelper.GetValue(container, "Apellido");
                    Nombre = ControlHelper.GetValue(container, "Nombre");
                }
                else
                {
                    tipoPersonaId = 1;
                    cuit = Convert.ToInt64(ControlHelper.GetValue(container, "txtCUIT"));
                    RazonSocial = ControlHelper.GetValue(container, "txtRazon");
                }


                string Calle = ControlHelper.GetValue(container, "Calle");
                int NroPuerta = Convert.ToInt32(ControlHelper.GetValue(container, "NroPuerta"), CultureInfo.CurrentCulture);
                string Piso = ControlHelper.GetValue(container, "Piso");
                string Depto = ControlHelper.GetValue(container, "Depto");
                string CodigoPostal = ControlHelper.GetValue(container, "CodigoPostal");
                int idLocalidad = Convert.ToInt32(ControlHelper.GetValue(container, "LocalidadDropDown"), CultureInfo.CurrentCulture);
                int idProvincia = Convert.ToInt32(ControlHelper.GetValue(container, "ProvinciaDropDown"), CultureInfo.CurrentCulture);
                string Celular = ControlHelper.GetValue(container, "Movil");
                string Telefono = ControlHelper.GetValue(container, "TelefonoTextBox");

                usuario.UserId = userid;
                usuario.UserName = UserName;
                usuario.TipoPersona = tipoPersonaId;
                usuario.UserDni = dni;
                usuario.RazonSocial = RazonSocial.Trim();
                usuario.CUIT = cuit.ToString();
                usuario.Apellido = Apellido.Trim();
                usuario.Nombre = Nombre.Trim();
                usuario.Calle = Calle.Trim();
                usuario.NroPuerta = NroPuerta;
                usuario.Piso = Piso;
                usuario.Depto = Depto;
                usuario.CodigoPostal = CodigoPostal;
                usuario.IdLocalidad = idLocalidad;
                usuario.IdProvincia = idProvincia;
                usuario.Movil = Celular;
                usuario.Telefono = Telefono;
                usuario.Email = Email;
                usuario.Password = Password;

                MailMessages mailer = new MailMessages();

                string url;
                url = "http://" + HttpContext.Current.Request.Url.Authority + ResolveUrl("~/Account/ActivateUser") + string.Format("?userid={0}", userid);
                url = IPtoDomain(url);
                string nombreApellido = usuario.Nombre + " " + usuario.Apellido;
                string htmlBody  =  mailer.MailWelcome(UserName, Password, url, nombreApellido);

                ExternalService.EmailEntity emailEntity = new ExternalService.EmailEntity();

                emailEntity.Asunto = "Información de acceso al sistema SSIT";
                emailEntity.IdEstado = (int)ExternalService.TiposDeEstadosEmail.PendienteDeEnvio;
                emailEntity.IdTipoEmail = (int)ExternalService.TiposDeMail.CreacionUsuario;
                emailEntity.IdOrigen = (int)ExternalService.MailOrigenes.SSIT;
                emailEntity.CantIntentos = 3;
                emailEntity.CantMaxIntentos = 3;
                emailEntity.Email = Email;
                emailEntity.FechaAlta = DateTime.Now;
                emailEntity.Prioridad = 1;
                emailEntity.Html = htmlBody;

                usuarioBl.Insert(usuario, emailEntity);

                
            }
        }


        protected void ProvinciaDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CargarLocalidades();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "mostrarError", "mostrarPopup();", true);
            }

        }


        private void CargarLocalidades()
        {
                DropDownList ProvinciaDropDown = (DropDownList)CreateUserStep.ContentTemplateContainer.FindControl("ProvinciaDropDown");
                DropDownList LocalidadDropDown = (DropDownList)CreateUserStep.ContentTemplateContainer.FindControl("LocalidadDropDown");

                RequiredFieldValidator LocalidadRequired = (RequiredFieldValidator)CreateUserStep.ContentTemplateContainer.FindControl("LocalidadRequired");
                UpdatePanel uodLocalidades = (UpdatePanel)CreateUserStep.ContentTemplateContainer.FindControl("uodLocalidades");

                if (ProvinciaDropDown.SelectedIndex > 0)
                {
                    LocalidadBL localidadBL = new LocalidadBL();

                    int idProvincia = Convert.ToInt32(ProvinciaDropDown.SelectedValue);
                    var lstLocalidades = localidadBL.GetByFKIdProvinciaExcluir(idProvincia, false);

                    LocalidadDropDown.DataValueField = "Id";
                    LocalidadDropDown.DataTextField = "Depto";
                    LocalidadDropDown.DataSource = lstLocalidades.OrderBy(x => x.Depto);
                    LocalidadDropDown.DataBind();
                }
                else
                {
                    LocalidadDropDown.Items.Clear();
                }

                LocalidadRequired.Validate();
                uodLocalidades.Update();
        }


        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "El nombre de usuario seleccionado ya existe. Por favor, seleccione uno diferente.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "El email ingresado para el usuario ya existe. Por favor, indique uno diferente.";

                case MembershipCreateStatus.InvalidPassword:
                    return "El password ingresado no es válido.";

                case MembershipCreateStatus.InvalidEmail:
                    return "El email ingresado no es válido.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "La respuesta ingresada no es válida.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "La pregunt ingresada no es válida.";

                case MembershipCreateStatus.InvalidUserName:
                    return "El nombre de usuario no es válido.";

                case MembershipCreateStatus.ProviderError:
                    return "Se ha encontrado un error. Contacte al administrador.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        protected void ContinueButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/");
        }

        protected void tipo_CheckedChanged(object sender, EventArgs e)
        {

            RadioButton tipoFisica = (RadioButton)CreateUserStep.ContentTemplateContainer.FindControl("tipoFisica");
            Panel panelPF = (Panel)CreateUserStep.ContentTemplateContainer.FindControl("panelPF");
            Panel panelPJ = (Panel)CreateUserStep.ContentTemplateContainer.FindControl("panelPJ");
            if (tipoFisica.Checked)
            {
                panelPF.Visible = true;
                panelPJ.Visible = false;
            }
            else
            {
                panelPF.Visible = false;
                panelPJ.Visible = true;

            }

        }

        private void CargarProvincias()
        {
            DropDownList ProvinciaDropDown = (DropDownList)CreateUserStep.ContentTemplateContainer.FindControl("ProvinciaDropDown");
            UpdatePanel updProvincias = (UpdatePanel)CreateUserStep.ContentTemplateContainer.FindControl("updProvincias");
            ProvinciaBL provinciaBL = new ProvinciaBL();
            var lstProvincias = provinciaBL.GetProvincias();

            ProvinciaDropDown.DataValueField = "Id";
            ProvinciaDropDown.DataTextField = "Nombre";
            ProvinciaDropDown.DataSource = lstProvincias;
            ProvinciaDropDown.DataBind();
            ProvinciaDropDown.Items.Insert(0, string.Empty);

            updProvincias.Update();

        }


    }
}