using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;
using System.Text;
using BusinesLayer.Implementation;
using System.Data.SqlClient;
using DataTransferObject;
using StaticClass;
using ExternalService;

namespace ConsejosProfesionales.ABM
{
    public partial class UsuariosCon : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {

            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                grdUsuarios.PageIndex = 0;
                BuscarUsuarios();
            }
            catch (Exception ex)
            {
                lblmpeInfo.Text = ex.Message;
                ScriptManager.RegisterClientScriptBlock(updBuscar, updBuscar.GetType(), "mostrarError", "mostrarPopup('pnlInformacion');", true);
            }
        }

        private int getGrupoConsejoLogeado()
        {
            GrupoConsejosBL grupoConsejoBL = new GrupoConsejosBL();
            // Obtiene el grupo de consejos al que pertenece el usuario logueado
            var dsGruCon = grupoConsejoBL.Get((Guid)Membership.GetUser().ProviderUserKey).FirstOrDefault();
            int id_grupoconsejo = 0;
            if (dsGruCon != null)
            {
                int.TryParse(dsGruCon.Id.ToString(), out id_grupoconsejo);
            }
            return id_grupoconsejo;
        }

        private void BuscarUsuarios()
        {
            string strApellido = txtApellido_buscar.Text.Trim();
            string strNombres = txtNombres_buscar.Text.Trim();
            string strUsername = txtUsername_buscar.Text.Trim();

            UsuarioBL usuarioBL = new UsuarioBL();
            var ds = usuarioBL.Get(strUsername, strApellido, strNombres, getGrupoConsejoLogeado(), Membership.Provider.ApplicationName.ToLower());

            int cantResultados = ds.Count();
            lblCantResultados.Text = "( Cantidad de resultados: " + cantResultados.ToString() + " )";
            lblCantResultados.Visible = (cantResultados > 0);

            grdUsuarios.DataSource = ds;
            grdUsuarios.DataBind();

        }

        protected void btnEliminarUsuario_Click(object sender, EventArgs e)
        {
            lblmpeInfo.Text = "";
            LinkButton btnEliminarUsuario = (LinkButton)sender;

            try
            {
                Guid userid = Guid.Parse(btnEliminarUsuario.CommandArgument.ToString());
                try
                {
                    UsuarioBL usuarioBL = new UsuarioBL();

                    usuarioBL.Delete(new UsuarioDTO() { UserId = userid }); 
                }
                catch (SqlException)
                {
                    throw new Exception("No se puede eliminar el usuario ya que el mismo actualizó Encomiendas, si desea que el usuario no pueda utilizar el sistema presione la opción 'Editar' e inactive el usuario. Esto hará que no pueda utilizar el sistema.");
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                txtUsername_buscar.Text = "";
                txtApellido_buscar.Text = "";
                txtNombres_buscar.Text = "";

                BuscarUsuarios();
             }
            catch (Exception ex)
            {
                lblmpeInfo.Text = ex.Message;
                ScriptManager.RegisterClientScriptBlock(updGrillaResultados, updGrillaResultados.GetType(), "mostrarError", string.Format("mostrarPopup('{0}');",pnlInformacion.ClientID), true);
            }
        }

        protected void grdUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            grdUsuarios.PageIndex = e.NewPageIndex;
            BuscarUsuarios();

        }
        protected void cmdPage(object sender, EventArgs e)
        {
            Button cmdPage = (Button)sender;
            grdUsuarios.PageIndex = int.Parse(cmdPage.Text) - 1;
            BuscarUsuarios();


        }
        protected void cmdAnterior_Click(object sender, EventArgs e)
        {
            grdUsuarios.PageIndex = grdUsuarios.PageIndex - 1;
            BuscarUsuarios();

        }
        protected void cmdSiguiente_Click(object sender, EventArgs e)
        {
            grdUsuarios.PageIndex = grdUsuarios.PageIndex + 1;
            BuscarUsuarios();
        }


        protected void grdUsuarios_DataBound(object sender, EventArgs e)
        {
            GridView grid = grdUsuarios;
            GridViewRow fila = (GridViewRow)grid.BottomPagerRow;

            if (fila != null)
            {

                Button btnAnterior = (Button)fila.Cells[0].FindControl("cmdAnterior");
                Button btnSiguiente = (Button)fila.Cells[0].FindControl("cmdSiguiente");

                if (grid.PageIndex == 0)
                    btnAnterior.Visible = false;
                else
                    btnAnterior.Visible = true;

                if (grid.PageIndex == grid.PageCount - 1)
                    btnSiguiente.Visible = false;
                else
                    btnSiguiente.Visible = true;


                // Ocultar todos los botones con Números de Página
                for (int i = 1; i <= 19; i++)
                {
                    Button btn = (Button)fila.Cells[0].FindControl("cmdPage" + i.ToString());
                    btn.Visible = false;
                }


                if (grid.PageIndex == 0 || grid.PageCount <= 10)
                {
                    // Mostrar 10 botones o el máximo de páginas

                    for (int i = 1; i <= 10; i++)
                    {
                        if (i <= grid.PageCount)
                        {
                            Button btn = (Button)fila.Cells[0].FindControl("cmdPage" + i.ToString());
                            btn.Text = i.ToString();
                            btn.Visible = true;
                            if (i + 1 < 100)     // Esto es para cuando el botón va de 1 a 9 inclusive no sea tan chico
                                btn.Width = Unit.Parse("35px");

                        }
                    }
                }
                else
                {
                    // Mostrar 9 botones hacia la izquierda y 9 hacia la derecha
                    // o bien los que sea posible en caso de no llegar a 9

                    int CantBucles = 0;

                    Button btnPage10 = (Button)fila.Cells[0].FindControl("cmdPage10");
                    btnPage10.Visible = true;
                    btnPage10.Text = Convert.ToString(grid.PageIndex + 1);

                    // Ubica los 9 botones hacia la izquierda
                    for (int i = grid.PageIndex - 1; i >= grid.PageIndex - 9; i--)
                    {
                        CantBucles++;
                        if (i >= 0)
                        {
                            Button btn = (Button)fila.Cells[0].FindControl("cmdPage" + Convert.ToString(10 - CantBucles));
                            btn.Visible = true;
                            btn.Text = Convert.ToString(i + 1);
                            if (i + 1 < 100)             // Esto es para cuando el botón va de 1 a 9 inclusive no sea tan chico
                                btn.Width = Unit.Parse("35px");
                        }

                    }

                    CantBucles = 0;
                    // Ubica los 9 botones hacia la derecha
                    for (int i = grid.PageIndex + 1; i <= grid.PageIndex + 9; i++)
                    {
                        CantBucles++;
                        if (i <= grid.PageCount - 1)
                        {
                            Button btn = (Button)fila.Cells[0].FindControl("cmdPage" + Convert.ToString(10 + CantBucles));
                            btn.Visible = true;
                            btn.Text = Convert.ToString(i + 1);
                            if (i + 1 < 100)     // Esto es para cuando el botón va de 1 a 9 inclusive no sea tan chico
                                btn.Width = Unit.Parse("35px");

                        }
                    }



                }
                Button cmdPage;
                string btnPage = "";
                for (int i = 1; i <= 19; i++)
                {
                    btnPage = "cmdPage" + i.ToString();
                    cmdPage = (Button)fila.Cells[0].FindControl(btnPage);
                    if (cmdPage != null)
                        cmdPage.CssClass = "btn btn-info";

                    if (cmdPage.Text == Convert.ToString(grid.PageIndex + 1))
                        cmdPage.CssClass = "btn btn-info";
                }
            }
        }

        protected void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            pnlNotaEmail.Visible = true;
            pnlReenvioClave.Visible = false;
            try
            {
                hid_userid.Value = "";
                txtusername.Text = "";
                txtApellido.Text = "";
                txtNombre.Text = "";
                txtEmail.Text = "";
                txtCelular.Text = "";

                CusValusername.Enabled = true;
                txtusername.Enabled = true;

                lblUsuarioBloqueado.Visible = false;
                chkUsuarioBloqueado_datos.Visible = false;
                chkUsuarioBloqueado_datos.Checked = false;

                chkPerfiles.ClearSelection();

                chkPerfiles.DataSource = Roles.Provider.GetAllRoles();
                chkPerfiles.DataBind();

                ScriptManager.RegisterClientScriptBlock(updGrillaResultados, updGrillaResultados.GetType(), "mostrarPopup", string.Format("mostrarPopup('{0}');", pnlDatosUsuario.ClientID), true);
            }
            catch (Exception ex)
            {
                //Oculto
                ScriptManager.RegisterClientScriptBlock(updDatosUsuario, updDatosUsuario.GetType(), "ocultarPopup", string.Format("ocultarPopup('{0}');", pnlDatosUsuario.ClientID), true);

                lblmpeInfo.Text = ex.Message;
                ScriptManager.RegisterClientScriptBlock(updDatosUsuario, updDatosUsuario.GetType(), "mostrarError", string.Format("mostrarPopup('{0}');", pnlInformacion.ClientID), true);
            }
        }

        protected void btnEditarUsuario_Click(object sender, EventArgs e)
        {
            pnlNotaEmail.Visible = false;
            pnlReenvioClave.Visible = true;
            try
            {
                CusValusername.Enabled = false;
                txtusername.Enabled = false;
                txtApellido.Text = "";
                txtNombre.Text = "";
                txtCelular.Text = "";
                chkPerfiles.ClearSelection();

                LinkButton btnEditarUsuario = (LinkButton)sender;
                hid_userid.Value = btnEditarUsuario.CommandArgument;
                Guid userid_registro = Guid.Parse(btnEditarUsuario.CommandArgument);

                MembershipUser user = Membership.GetUser(userid_registro);

                lblUsuarioBloqueado.Visible = true;
                chkUsuarioBloqueado_datos.Visible = true;
                chkUsuarioBloqueado_datos.Checked = (user.IsLockedOut || !user.IsApproved);

                chkPerfiles.DataSource = Roles.Provider.GetAllRoles();
                chkPerfiles.DataBind();
                string[] strRoles = Roles.GetRolesForUser(user.UserName);

                foreach (string Rol in strRoles)
                {
                    chkPerfiles.Items.FindByText(Rol).Selected = true;
                }
                UsuarioBL usuarioBL = new UsuarioBL();
                var dr = usuarioBL.Single(userid_registro);

                txtApellido.Text = dr.Apellido;
                txtNombre.Text = dr.Nombre;
                txtCelular.Text = dr.Telefono;
                txtEmail.Text = dr.Email;
                txtusername.Text = dr.UserName;       

                ScriptManager.RegisterClientScriptBlock(updGrillaResultados, updGrillaResultados.GetType(),"mostrarPopup", string.Format("mostrarPopup('{0}');",pnlDatosUsuario.ClientID), true);
            }
            catch (Exception ex)
            {
                //Oculto
                ScriptManager.RegisterClientScriptBlock(updDatosUsuario, updDatosUsuario.GetType(), "ocultarPopup", string.Format("ocultarPopup('{0}');", pnlDatosUsuario.ClientID), true);
                lblmpeInfo.Text = ex.Message;
                ScriptManager.RegisterClientScriptBlock(updDatosUsuario, updDatosUsuario.GetType(), "mostrarError", string.Format("mostrarPopup('{0}');",pnlInformacion.ClientID), true);
            }
        }

        protected void btnAceptarDatosUsuario_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string strUsername = txtusername.Text.Trim();
                Guid UserUpdate = (Guid)Membership.GetUser().ProviderUserKey;

                UsuarioDTO user = new UsuarioDTO();
                user.UserName = strUsername;
                user.Apellido = txtApellido.Text;
                user.Nombre = txtNombre.Text;
                user.Email = txtEmail.Text;
                user.Telefono = txtCelular.Text;
                user.IdLocalidad = (int)Constantes.Localidad.CABA;
                user.IdProvincia = (int)Constantes.Provincia.CABA;
                user.CreateUser = UserUpdate;
                user.Bloqueado = chkUsuarioBloqueado_datos.Checked;

                foreach (ListItem itm in chkPerfiles.Items)
                {
                    if (user.Roles == null)
                        user.Roles = new List<RolesDTO>();
 
                    if (itm.Selected)
                    {
                        user.Roles.Add(new RolesDTO() { RoleName = itm.Text });
                    }
                }
                
                try
                {
                    if (txtusername.Enabled)
                    {
                       
                        UsuarioBL usuarioBL = new UsuarioBL();
                        usuarioBL.InsertMembership(user); 
                        
                        SendMail(user.UserName);

                        lblSuccess.Text = "Usuario creado.";
                        ScriptManager.RegisterClientScriptBlock(updDatosUsuario, updDatosUsuario.GetType(), "mostrarError", string.Format("mostrarPopup('{0}');", pnlSuccess.ClientID), true);
                        //Oculto
                        ScriptManager.RegisterClientScriptBlock(updDatosUsuario, updDatosUsuario.GetType(), "ocultarPopup", string.Format("ocultarPopup('{0}');", pnlDatosUsuario.ClientID), true);

                        txtUsername_buscar.Text = user.UserName;
                        txtApellido_buscar.Text = "";
                        txtNombres_buscar.Text = "";
                        BuscarUsuarios();

                            
                    }
                    else
                    {

                        UsuarioBL usuarioBL = new UsuarioBL();
                        usuarioBL.UpdateMembership(user);

                        ScriptManager.RegisterClientScriptBlock(updDatosUsuario, updDatosUsuario.GetType(), "ocultarPopup", string.Format("ocultarPopup('{0}');",pnlDatosUsuario.ClientID), true);

                        txtUsername_buscar.Text = user.UserName;
                        txtApellido_buscar.Text = "";
                        txtNombres_buscar.Text = "";
                        BuscarUsuarios();
                    }
                }
                catch (Exception ex)
                {
                    //Oculto
                    ScriptManager.RegisterClientScriptBlock(updDatosUsuario, updDatosUsuario.GetType(), "ocultarPopup", string.Format("ocultarPopup('{0}');", pnlDatosUsuario.ClientID), true);

                    lblmpeInfo.Text = ex.Message;
                    ScriptManager.RegisterClientScriptBlock(updDatosUsuario, updDatosUsuario.GetType(), "mostrarError", string.Format("mostrarPopup('{0}');", pnlInformacion.ClientID), true);
                }
            }
        }
        protected void SendMail(string strUsername)
        {
            if (Page.IsValid)
            {
                try
                {
                    
                    MembershipUser user = Membership.GetUser(strUsername, false);

                    MailMessages mailer = new MailMessages();

                    string htmlBody;
                    string url;
                    string Asunto = "Recupero de contraseña - Consejo de Profesionales";
                    url = "http://" + HttpContext.Current.Request.Url.Authority + ResolveUrl("~/Account/Login");
                    url = IPtoDomain(url);

                    htmlBody = mailer.MailPassRecovery(user.UserName, user.GetPassword(), url);
                    EmailServiceBL serviceMail = new EmailServiceBL();

                    EmailEntity emailEntity = new EmailEntity();
                    emailEntity.Email = user.Email;
                    emailEntity.Html = htmlBody;
                    emailEntity.Asunto = Asunto;
                    emailEntity.IdEstado = (int)ExternalService.TiposDeEstadosEmail.PendienteDeEnvio;
                    emailEntity.IdTipoEmail = (int)ExternalService.TiposDeMail.RecuperoContrasena;
                    emailEntity.IdOrigen = (int)ExternalService.MailOrigenes.SSIT;
                    emailEntity.CantIntentos = 3;
                    emailEntity.CantMaxIntentos = 3;
                    emailEntity.FechaAlta = DateTime.Now;
                    emailEntity.Prioridad = 1;

                    serviceMail.SendMail(emailEntity);

                    //Oculto
                    ScriptManager.RegisterClientScriptBlock(updDatosUsuario, updDatosUsuario.GetType(), "ocultarPopup", string.Format("ocultarPopup('{0}');", pnlDatosUsuario.ClientID), true);

                    lblSuccess.Text = "Se le ha enviado la contraseña.";
                    ScriptManager.RegisterClientScriptBlock(updDatosUsuario, updDatosUsuario.GetType(), "mostrarError", string.Format("mostrarPopup('{0}');", pnlSuccess.ClientID), true);
                }
                catch (Exception ex)
                {
                    //Oculto
                    ScriptManager.RegisterClientScriptBlock(updDatosUsuario, updDatosUsuario.GetType(), "ocultarPopup", string.Format("ocultarPopup('{0}');", pnlDatosUsuario.ClientID), true);

                    lblmpeInfo.Text = ex.Message;
                    ScriptManager.RegisterClientScriptBlock(updDatosUsuario, updDatosUsuario.GetType(), "mostrarError", string.Format("mostrarPopup('{0}');", pnlInformacion.ClientID), true);
                }
            }
        }
        protected void CusValusername_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;
            if (Membership.GetUser(txtusername.Text.Trim(), false) != null)
            {
                CusValusername.ErrorMessage = "Ya existe un usuario con este mismo nombre, por favor elija otro o agregue algun caracter.";
                args.IsValid = false;
            }
        }

        private void ActualizarRoles(MembershipUser user, CheckBoxList chkList)
        {

            string[] strRoles = Roles.GetRolesForUser(user.UserName);
            if (strRoles.Length > 0)
                Roles.RemoveUserFromRoles(user.UserName, strRoles);

            foreach (ListItem itm in chkList.Items)
            {
                if (itm.Selected)
                {
                    Roles.AddUserToRole(user.UserName, itm.Text);
                }
            }

        }

        private void SendMail(MembershipUser user)
        {
            string textobody = GetActivationMessage(user);
            //MailMessages.SendMail(1, 1, user.Email, "Encomienda Digital (Consejos) - Datos de Usuario", textobody);
        }

        private string GetActivationMessage(MembershipUser user)
        {

            Guid userId = (Guid)user.ProviderUserKey;
            string userName = user.UserName;
            user.ResetPassword();
            string password = user.GetPassword();

            string url = ConfigurationManager.AppSettings["Url.Website.Consejos"].ToString() + "Usuarios/Login.aspx";
            StringBuilder s = new StringBuilder();

            s.AppendLine("<body style='font-family:Verdana;font-size:9pt'>");

            s.AppendLine("<p>");
            s.AppendLine("<b>Bienvenido al sistema de Encomienda Digital!</b>");
            s.AppendLine("</p>");

            s.Append("<p>");
            s.Append("Utilice las siguientes credenciales para iniciar sesi&oacute;n:<br />");
            s.AppendFormat("<b>Usuario:</b> {0}<br />", userName);
            s.AppendFormat("<b>Contrase&ntilde;a:</b> {0}<br />", password);
            s.Append("</p>");
            s.Append("Haga clic en el siguiente enlace para ingresar al sitio:<br />");
            s.Append(string.Format("<a href='{0}'>{0}</a>", url));

            s.Append("</body>");
            return s.ToString();
        }

        protected void reenvioclave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                string strUsername = txtusername.Text.Trim();
                try
                {
                    MembershipUser user = Membership.GetUser(strUsername, false);

                    //MailMessages.SendClave(user);
                    SendMail(user.UserName);

                    //lblSuccess.Text = "Se le ha enviado la contraseña.";
                    //ScriptManager.RegisterClientScriptBlock(updDatosUsuario, updDatosUsuario.GetType(), "mostrarError", "mostrarPopup('pnlSuccess');", true);
                }
                catch (Exception ex)
                {
                    lblmpeInfo.Text = ex.Message;
                    ScriptManager.RegisterClientScriptBlock(updDatosUsuario, updDatosUsuario.GetType(), "mostrarError", "mostrarPopup('pnlInformacion');", true);
                }
            }
        }
    }
}