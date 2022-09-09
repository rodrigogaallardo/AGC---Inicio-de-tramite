using AnexoProfesionales.App_Components;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;

namespace AnexoProfesionales.Mailer
{
    public partial class MailWelcome : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Mailer) this.Master).Titulo = "Activaci&oacute;n de usuario.";
            }
        }
        public IEnumerable<StaticClass.MailWelcome> GetData()
        {
            object puserid = Request.QueryString["userid"];

            if (puserid != null)
            {
                Guid userid = Guid.Parse(puserid.ToString());
                MembershipUser usu = Membership.GetUser(userid);
                
                string pass = usu.GetPassword();
                string url = "http://" + HttpContext.Current.Request.Url.Authority + ResolveUrl("~/Account/ActivateUser") + string.Format("?userid={0}", userid);
                url = IPtoDomain(url);

                List<StaticClass.MailWelcome> ListaMW = new List<StaticClass.MailWelcome>();

                StaticClass.MailWelcome MW = new StaticClass.MailWelcome();

                MW.Username = usu.UserName;
                MW.Email = usu.Email;
                MW.Urlactivacion = url;
                MW.Password = pass;
                MW.Renglon1 = "Se realizó con éxito la registración de su usuario en el aplicativo SSIT.";
                MW.Renglon2 = "Para poder utilizar dicha cuenta, deberá activar el usuario presionando el botón “activar usuario”. Si tiene algún inconveniente con la activación del usuario, copie la siguiente dirección en su navegador: URL.";

                ListaMW.Add(MW);

                return ListaMW;
            }
            else
            {
                return null;
            }
      
        }
    }
}