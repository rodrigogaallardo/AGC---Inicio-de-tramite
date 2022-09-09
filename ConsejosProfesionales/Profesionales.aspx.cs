using BusinesLayer.Implementation;
using DataTransferObject;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace ConsejosProfesionales
{
    public partial class Profesionales : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MembershipProvider MemberProfesional = Membership.Providers["SqlMembershipProviderProfesionales"];
            RoleProvider roleProvider = System.Web.Security.Roles.Providers["SqlRoleProviderProfesionales"];
            ScriptManager sm = ScriptManager.GetCurrent(this.Page);
            
            UpdatePanel UpdModificarDatos = (UpdatePanel)lgnView.FindControl("UpdModificarDatos");

            if (sm.IsInAsyncPostBack)
            {
                //ScriptManager.RegisterStartupScript(updCargarDatos, updCargarDatos.GetType(), "init_JS_Inicializar", "init_JS_Inicializar();", true);
                if (UpdModificarDatos != null)
                    ScriptManager.RegisterStartupScript(UpdModificarDatos, UpdModificarDatos.GetType(), "init_JS_Inicializar", "init_JS_Inicializar();", true);

            }
            if (!IsPostBack)
            {
                CargarProfesional();

            }


        }

        protected void CargarProfesional()
        {

            if (HttpContext.Current.Request.IsAuthenticated)
            {

                MembershipProvider MemberProfesional = Membership.Providers["SqlMembershipProviderProfesionales"];
                MembershipUser usu = MemberProfesional.GetUser(HttpContext.Current.User.Identity.Name, true);
                UsuarioBL usuBL = new UsuarioBL();

                if (usu != null)
                {
                    ProfesionalesBL profBL = new ProfesionalesBL();
                    ProfesionalDTO prof = profBL.Get((Guid)usu.ProviderUserKey);

                    TipoDocumentoPersonalBL docBL = new TipoDocumentoPersonalBL();

                    List<RolesDTO> roles = new List<RolesDTO>();

                    txtApellidoReq.Text = prof.Apellido;
                    txtNombreReq.Text = prof.Nombre;
                    txtMatriculaMetrogas.Text = prof.MatriculaMetrogas+"";
                    txtEmailReq.Text = prof.Email;
                    txtCuitReq.Text = prof.Cuit;
                    txtCalleReq.Text = prof.Calle;
                    txtNroReq.Text = prof.NroPuerta;
                    txtNroMatriculaReq.Text = prof.Matricula;
                    txtLocalidadReq.Text = prof.Localidad;
                    txtProvinciaReq.Text = prof.Provincia;
                    txtConsejoReq.Text = prof.ConsejoProfesionalDTO != null ? prof.ConsejoProfesionalDTO.Descripcion : "";
                    txtTelefonoReq.Text = prof.Telefono;
                    txtSmsReq.Text = prof.Sms;
                    txtCategoriaMetrogas.Text = prof.CategoriaMetrogas + "";
                    txtPisoReq.Text = prof.Piso;
                    txtDeptoReq.Text = prof.Depto;
                    txtNroIngresosBrutosReq.Text = prof.IngresosBrutos+"";
                    txtTipoYNroDocReq.Text = prof.IdTipoDocumento != null ? (docBL.Single(prof.IdTipoDocumento ?? 0).Nombre+": " + prof.NroDocumento) : "";

                    List<EncomiendaURLxROLDTO> lsturl = profBL.GetEncURL((Guid)usu.ProviderUserKey);
                    //List<ApplicationsDTO> lstapp = profBL.GetAppByUser((Guid)usu.ProviderUserKey);

                    //if (lstapp.Count > 0)
                    //{
                    //    foreach (ApplicationsDTO item in lstapp)
                    //    {
                    //        List<RolesDTO> roldescripcion = profBL.GetRolxAppId(item.ApplicationId, (Guid)usu.ProviderUserKey);

                    //        if (roldescripcion.Count > 0)
                    //        {
                    //            roles.Add(new RolesDTO
                    //            {
                    //                RoleName = item.ApplicationName,
                    //                Description = string.Join(", ", roldescripcion.Select(y => y.Description)),
                    //            });
                    //        }
                    //    }
                    //}
                    if (lsturl.Count > 0)
                    {
                        foreach (EncomiendaURLxROLDTO item in lsturl)
                        {
                            List<RolesDTO> roldescripcion = profBL.GetRolxURL(item.URL);

                            if (roldescripcion.Count > 0)
                            {
                                roles.Add(new RolesDTO
                                {
                                    RoleName = item.URL,
                                    Description = string.Join(", ", roldescripcion.Select(y => y.Description)),
                                });
                            }
                        }
                    }
                    grdSistemas.DataSource = roles.Distinct().ToList();
                    grdSistemas.DataBind();

                    //List<UsuarioDTO> roles = usuBL.GetRolURL((Guid)usu.ProviderUserKey);

                    //UsuarioDTO usuario = usuBL.Single((Guid)usu.ProviderUserKey);


                    //if (usuario != null)
                    //{
                    //    grdSistemas.DataSource = usuario.Roles;
                    //    grdSistemas.DataBind();
                    //}


                }
            }
            else
                UpdModificarDatos.Visible = false;
        }

        protected void LoginControl_LoginError(object sender, EventArgs e)
        {
            Login LoginControl = (Login)sender;
            LoginControl.FailureText = "";

            MembershipProvider MemberProfesional = Membership.Providers["SqlMembershipProviderProfesionales"];
            MembershipUser user = MemberProfesional.GetUser(LoginControl.UserName,true);
            try
            {
                if (user == null)
                {
                    LoginControl.FailureText = "El nombre de usuario no existe.";
                }
                else
                {
                    if (!user.IsApproved)
                        LoginControl.FailureText = "El usuario no se encuentra habilitado. Por favor utilice el mail que se le ha enviado en la registración.";

                    if (user.IsLockedOut)
                        LoginControl.FailureText = "El usuario se encuentra bloqueado. Por favor, revise a su casilla de e-mail declarada para activarlo desbloquearlo.";

                    if (LoginControl.FailureText.Length == 0 && user.GetPassword() != LoginControl.Password)
                        LoginControl.FailureText = "La Contraseña ingresada es incorrecta.";

                }
            }
            catch (Exception ex)
            {
                LoginControl.FailureText = ex.Message;
            }

        }

        protected void LoginControl_LoggedIn(object sender, EventArgs e)
        {
            if (Request.Cookies[Constantes.UserNameCookie] != null)
            {

                MembershipProvider MemberProfesional = Membership.Providers["SqlMembershipProviderProfesionales"];
                MembershipUser usu = MemberProfesional.GetUser((sender as Login).UserName,true);
                UsuarioBL usuBL = new UsuarioBL();

                if (usu != null)
                {
                    var usuDTO = usuBL.Single((Guid)usu.ProviderUserKey);
                    TextInfo txtInfo = new CultureInfo("es-AR", false).TextInfo;
                    
                    var userNameCookie = new HttpCookie(Constantes.UserNameCookie);
                    userNameCookie.Value = usu.UserName;

                    Response.Cookies.Set(userNameCookie);
                }
            }
        }

        
    }
}