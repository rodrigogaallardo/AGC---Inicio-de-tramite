using SSIT.App_Components;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;

namespace SSIT.Mailer
{
    public partial class MailPassRecovery : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Mailer)this.Master).Titulo = "Recupero de cuenta.";
            }

        }

        public IEnumerable<StaticClass.MailPassRecovery> GetData()
        {
            object puserid = Request.QueryString["userid"];

            if (puserid != null)
            {
                Guid userid = Guid.Parse(puserid.ToString());
                MembershipUser usu = Membership.GetUser(userid);
                
                string pass = usu.GetPassword();
                string url = "http://" + HttpContext.Current.Request.Url.Authority + ResolveUrl("~/Account/Login");
                url = IPtoDomain(url);

                List<StaticClass.MailPassRecovery> Listampr = new List<StaticClass.MailPassRecovery>();

                StaticClass.MailPassRecovery mpr = new StaticClass.MailPassRecovery();

                mpr.Username = usu.UserName;
                mpr.Email = usu.Email;
                mpr.Password = pass;
                mpr.UrlLogin = url;
                mpr.Renglon1 = "<b>Instrucciones:</b> Haga click en el botón Iniciar Sesi&oacute;n e ingrese los datos requeridos.";
                mpr.Renglon2 = "Si por alg&uacute;n motivo no pudiera hacer click en el bot&oacute;n, copie la siguiente direcci&oacute;n en su navegador:";
                
                Listampr.Add(mpr);

                return Listampr;
            }
            else
            {
                return null;
            }
        }
    }
}