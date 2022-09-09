using BusinesLayer.Implementation;
using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using StaticClass;
using System.Globalization;
using System.Collections.Generic;

namespace ConsejosProfesionales
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
              if (!IsPostBack)
                SetearMenu();
 
        }
        private void SetearMenu()
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                LinkButton lnkUsuariosProfesionales = (LinkButton)lgnView.FindControl("lnkUsuariosProfesionales");
                LinkButton lnkUsuariosConsejo = (LinkButton)lgnView.FindControl("lnkUsuariosConsejo");
                LinkButton lnkSearchEncomiendas = (LinkButton)lgnView.FindControl("lnkSearchEncomiendas");
                LinkButton lnkSearchEncomiendasExtDe = (LinkButton)lgnView.FindControl("lnkSearchEncomiendasExtDe");
                LinkButton lnkValidacionEncomiendaObra = (LinkButton)lgnView.FindControl("lnkValidacionEncomiendaObra");

                //lnkUsuariosProfesionales.Visible = false;
                lnkUsuariosConsejo.Visible = false;
                lnkSearchEncomiendas.Visible = false;
                lnkSearchEncomiendasExtDe.Visible = false;
                lnkValidacionEncomiendaObra.Visible = false;

                MembershipUser user = Membership.GetUser();

                if (user != null)
                {
                    lnkValidacionEncomiendaObra.Visible = true;

                    string[] strRoles = Roles.GetRolesForUser(user.UserName);


                    if (ExisteRol(strRoles, "adm_usuarios") || ExisteRol(strRoles, "administrador"))
                    {
                        //lnkUsuariosProfesionales.Visible = true;
                        lnkUsuariosConsejo.Visible = true;
                    }

                    if (ExisteRol(strRoles, "Recepcion_Encomiendas") || ExisteRol(strRoles, "administrador") || ExisteRol(strRoles, "Aprobacion_Encomiendas"))
                    {
                        lnkSearchEncomiendas.Visible = true;
                    }

                    if (ExisteConsejo(user))
                        lnkSearchEncomiendasExtDe.Visible = true;

                    
                }
            }
        }

        private bool ExisteConsejo(MembershipUser user)
        {
            Guid userid = (Guid)user.ProviderUserKey;
            GrupoConsejosBL grupoConsejoBL = new GrupoConsejosBL();
            var dsGrupoConsejo = grupoConsejoBL.Get(userid);


            Label lblCountDirObraPend = (Label)lgnView.FindControl("lblCountDirObraPend");

            if(dsGrupoConsejo != null && dsGrupoConsejo.Count() > 0)
            {

                EncomiendaBL encomiendaBL = new EncomiendaBL();

                List<int> BusListEstados = new List<int>();
                BusListEstados.Add(0);

                int totalRowCount = 0;

                int cant = encomiendaBL.TraerEncomiendasDirectorObra(dsGrupoConsejo.FirstOrDefault().Id, "", "", "", BusListEstados, 0, 11,  0, 9999,  "", out totalRowCount).Count();
                lblCountDirObraPend.Text = cant == 0 ? "" : "( " + cant + " )";
            }
            

            return dsGrupoConsejo.Any(p => p.Nombre.Equals("CPAU") || p.Nombre.Equals("CPIC") || p.Nombre.Equals("CPII"));
        }

        private bool ExisteRol(string[] ListaRoles, string RolBuscado)
        {
            bool ret = false;

            foreach (string strRol in ListaRoles)
            {

                if (strRol.ToLower().Equals(RolBuscado.ToLower()))
                    ret = true;
            }

            return ret;
        }

        protected void LoginControl_LoginError(object sender, EventArgs e)
        {
            Login LoginControl = (Login)sender;
            LoginControl.FailureText = "";

            MembershipUser user = Membership.GetUser(LoginControl.UserName);
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

                MembershipUser usu = Membership.GetUser((sender as Login).UserName);
                UsuarioBL usuBL = new UsuarioBL();

                string ret = "";
                if (usu != null)
                {
                    var usuDTO = usuBL.Single((Guid)usu.ProviderUserKey);
                    TextInfo txtInfo = new CultureInfo("es-AR", false).TextInfo;

                    string Apellido = txtInfo.ToTitleCase((usuDTO.Apellido != null) ? usuDTO.Apellido.ToLower() : string.Empty);
                    string Nombre = txtInfo.ToTitleCase((usuDTO.Nombre != null) ? usuDTO.Nombre.ToLower() : string.Empty);

                    ret = usu.UserName + " (" + Apellido + " " + Nombre + ")";

                    var userNameCookie = new HttpCookie(Constantes.UserNameCookie)
                    {
                        Value = ret
                    };

                    Response.Cookies.Set(userNameCookie);
                }
            }
        }
        
    }
}