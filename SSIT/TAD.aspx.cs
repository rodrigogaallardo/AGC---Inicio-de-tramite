using SSIT.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT
{
    public partial class TAD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (!Functions.isDesarrollo())
                   Response.Redirect("~/");
            }
        }

        protected void lnk_Click(object sender, EventArgs e)
        {
            string username = txt.Text.Trim();
            MembershipUser usu = Membership.GetUser(username);

            if (usu != null)
            {

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddMinutes(20), true, "",
                                    FormsAuthentication.FormsCookiePath);

                string hashCookies = FormsAuthentication.Encrypt(ticket);

                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashCookies);

                Response.Cookies.Add(cookie);

                Response.Redirect("~/");

                //FormsAuthentication.SetAuthCookie(username, true);

            }
            else
            {
                //ScriptManager.RegisterStartupScript(upd, upd.GetType(), "script", "showfrmError();", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "showfrmError();", true);
            }
        }
    }
}