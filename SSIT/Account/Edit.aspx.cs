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


namespace SSIT.Account
{
        public partial class Edit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            ScriptManager sm = ScriptManager.GetCurrent(this);

            if (sm.IsInAsyncPostBack)
            {
                ScriptManager.RegisterStartupScript(updDatosPersonas, updDatosPersonas.GetType(), "init_JS_updDatosPersonas", "init_JS_updDatosPersonas();", true);

            }

            if (!Page.IsPostBack)
            {
                OnPageLoaded();
            }
        }


        //Private members
        private void OnPageLoaded()
        {
            try
            {
                InicializarControles();
                CargarDatos();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "mostrarError", "mostrarPopup('pnlError');", true);
            }
        }


        private void InicializarControles()
        {
            CargarProvincias();
            CargarLocalidades();

        }

        private void CargarProvincias()
        {

            ProvinciaBL provinciaBL = new ProvinciaBL();
            var lstProvincias = provinciaBL.GetProvincias();

            ProvinciaDropDown.DataValueField = "Id";
            ProvinciaDropDown.DataTextField = "Nombre";
            ProvinciaDropDown.DataSource = lstProvincias;
            ProvinciaDropDown.DataBind();
            ProvinciaDropDown.Items.Insert(0, string.Empty);

            updProvincias.Update();

        }

        private void CargarDatos()
        {
            MembershipUser usuario = Membership.GetUser();

            Guid userId = (Guid)usuario.ProviderUserKey;

            UsuarioBL usuarioBl = new UsuarioBL();
            List<UsuarioDTO> usuarioDto = new List<UsuarioDTO>();

            var clsUsuario = usuarioBl.Single(userId);

            UserName.Text = usuario.UserName;
            Email.Text = clsUsuario.Email;
            txtDni.Text = clsUsuario.UserDni.ToString();
            Apellido.Text = clsUsuario.Apellido;
            Nombre.Text = clsUsuario.Nombre;
            txtRazon.Text = clsUsuario.RazonSocial;
            txtCUIT.Text = clsUsuario.CUIT;
            if (clsUsuario.TipoPersona == 0)
            {
                tipoFisica.Checked = true;
                panelPF.Visible = true;
                panelPJ.Visible = false;
            }
            else
            {
                tipoJuridica.Checked = true;
                panelPF.Visible = false;
                panelPJ.Visible = true;
            }


            Calle.Text = clsUsuario.Calle;
            NroPuerta.Text = clsUsuario.NroPuerta.ToString();
            Piso.Text = clsUsuario.Piso;
            Depto.Text = clsUsuario.Depto;
            CodPostal.Text = clsUsuario.Depto;
            ProvinciaDropDown.SelectedValue = clsUsuario.IdProvincia.ToString();
            CargarLocalidades();
            LocalidadDropDown.SelectedValue = clsUsuario.IdLocalidad.ToString();
            Movil.Text = clsUsuario.Movil;
            TelefonoTextBox.Text = clsUsuario.Telefono;

        }

        //protected void showNuevoEmail(object sender, EventArgs e)
        //{
        //pnlEmail.Style["display"] = "block-inline";
        //btnNuevoEmailShow.Style["display"] = "none";
        //btnNuevoEmailHide.Style["display"] = "block-inline";
        // udpEmail.Update();
        //}

        //protected void hideNuevoEmail(object sender, EventArgs e)
        //{
        //btnNuevoEmailShow.Style["display"] =  "block-inline";
        //btnNuevoEmailHide.Style["display"] = "none";
        //pnlEmail.Style["display"] = "none";
        //     udpEmail.Update();
        //}
        protected void ActualizarUsuario(object sender, EventArgs e)
        {
            try
            {

                MembershipUser usuario = Membership.GetUser();
                if (usuario == null)
                {
                    throw new InvalidOperationException("Debe ingresar el nombre de usuario.");
                }

                UsuarioDTO usuarioDto = new UsuarioDTO();
                UsuarioBL usuarioBl = new UsuarioBL();


                if (ModelState.IsValid)
                {
                    Guid userid = (Guid)usuario.ProviderUserKey;
                    usuarioDto.UserId = userid;

                    if (tipoFisica.Checked)
                    {
                        usuarioDto.TipoPersona = 0;
                        usuarioDto.UserDni = Convert.ToInt32(txtDni.Text.Trim());
                        usuarioDto.Apellido = Apellido.Text.Trim();
                        usuarioDto.Nombre = Nombre.Text.Trim();
                    }
                    else
                    {
                        usuarioDto.TipoPersona = 1;
                        usuarioDto.CUIT = txtCUIT.Text.Trim();
                        usuarioDto.RazonSocial = txtRazon.Text.Trim();
                    }

                    usuarioDto.Calle = Calle.Text.Trim();
                    usuarioDto.NroPuerta = Convert.ToInt32(NroPuerta.Text, CultureInfo.CurrentCulture);
                    usuarioDto.Piso = Piso.Text.Trim();
                    usuarioDto.Depto = Depto.Text.Trim();
                    usuarioDto.CodigoPostal = CodPostal.Text.Trim();
                    usuarioDto.IdLocalidad = Convert.ToInt32(LocalidadDropDown.SelectedValue, CultureInfo.CurrentCulture);
                    usuarioDto.IdProvincia = Convert.ToInt32(ProvinciaDropDown.SelectedValue, CultureInfo.CurrentCulture);
                    usuarioDto.Movil = Movil.Text.Trim();
                    usuarioDto.Telefono = TelefonoTextBox.Text.Trim();

                    //if (btnNuevoEmailHide.Style["display"] == "none")
                    //    usuarioDto.Email = Email.Text;
                    //else
                    //    usuarioDto.Email = txtNuevoEmail.Text;

                    if (!(usuarioDto.Email == usuario.Email))
                    {
                        // Si se cambio el correo
                        usuario.IsApproved = false;
                        usuario.Email = usuarioDto.Email;
                        Membership.UpdateUser(usuario);
             
                    }

                    usuarioBl.Update(usuarioDto);

                    if (!usuario.IsApproved)
                    {
                        EmailServiceBL serviceMail = new EmailServiceBL();
                        MailMessages mailer = new MailMessages();

                        string url;
                        url = "http://" + HttpContext.Current.Request.Url.Authority + ResolveUrl("~/Account/ActivateUser") + string.Format("?userid={0}", userid);
                        url = IPtoDomain(url);

                        string htmlBody = mailer.MailWelcome(usuario.UserName, usuario.GetPassword() , url, usuarioDto.Nombre + " " +usuarioDto.Apellido);

                        EmailEntity emailEntity = new EmailEntity();
                        emailEntity.Asunto = "Activación de usuario - SSIT";
                        emailEntity.IdEstado = (int)ExternalService.TiposDeEstadosEmail.PendienteDeEnvio;
                        emailEntity.IdTipoEmail = (int)ExternalService.TiposDeMail.CreacionUsuario;
                        emailEntity.IdOrigen = (int)ExternalService.MailOrigenes.SSIT;
                        emailEntity.CantIntentos = 3;
                        emailEntity.CantMaxIntentos = 3;
                        emailEntity.FechaAlta = DateTime.Now;
                        emailEntity.Prioridad = 1;
                        emailEntity.Html = htmlBody;
                        emailEntity.Email = usuarioDto.Email;

                        serviceMail.SendMail(emailEntity);

                    }

                }
                ScriptManager.RegisterStartupScript(updDatosPersonas, updDatosPersonas.GetType(), "showConfirm", "showConfirm();", true);
            }
            catch 
            {
                throw;
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
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "mostrarError", "mostrarPopup('pnlError');", true);
            }

        }


        private void CargarLocalidades()
        {
           
            if (ProvinciaDropDown.SelectedIndex > 0)
            {
                int idProvincia = Convert.ToInt32(ProvinciaDropDown.SelectedValue, CultureInfo.CurrentCulture);

                LocalidadBL localidadBL = new LocalidadBL();

                var lstLocalidades = localidadBL.GetByFKIdProvinciaExcluir(idProvincia, false);

                LocalidadDropDown.DataValueField = "Id";
                LocalidadDropDown.DataTextField = "Depto";
                LocalidadDropDown.DataSource = lstLocalidades;
                LocalidadDropDown.DataBind();
            }
            else
            {
                LocalidadDropDown.Items.Clear();
            }

            LocalidadRequired.Validate();
            uodLocalidades.Update();
        }


        protected void tipo_CheckedChanged(object sender, EventArgs e)
        {
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
        protected void redirectHome(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("~/"));
        }
    }
}