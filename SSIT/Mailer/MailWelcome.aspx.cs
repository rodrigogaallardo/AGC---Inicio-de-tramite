using SSIT.App_Components;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using BusinesLayer.Implementation;
using IBusinessLayer;
using DataTransferObject;
using StaticClass;
using System.Globalization;


namespace SSIT.Mailer
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

                UsuarioBL usuarioBl = new UsuarioBL();
                var usuario = usuarioBl.Single(userid);
                string pass = usu.GetPassword();
                string url = "http://" + HttpContext.Current.Request.Url.Authority + ResolveUrl("~/Account/ActivateUser") + string.Format("?userid={0}", userid);
                url = IPtoDomain(url);

                List<StaticClass.MailWelcome> ListaMW = new List<StaticClass.MailWelcome>();

                StaticClass.MailWelcome MW = new StaticClass.MailWelcome();

                MW.NombreApellido = usuario.Nombre + " " + usuario.Apellido;
                MW.Username = usu.UserName;
                MW.Email = usu.Email;
                MW.Urlactivacion = url;
                MW.Password = pass;
                MW.Renglon1 = "Para poder utilizar dicha cuenta deber&aacute; activar el usuario presionando el bot&oacute;n 'Activar usuario'.";
                MW.Renglon2 = "Si tiene alg&uacute;n inconveniente con la activaci&oacute;n del usuario, copie la siguiente direcci&oacute;n en su navegador:";

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