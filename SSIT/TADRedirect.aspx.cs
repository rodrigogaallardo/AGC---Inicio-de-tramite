using BusinesLayer.Implementation;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT
{
    public partial class TADRedirect : System.Web.UI.Page
    {
        public string TADAction { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Busco los valores de los redirec y Acction
            Guid UserID = (Guid)Membership.GetUser().ProviderUserKey;
            ParametrosBL blParam = new ParametrosBL();
            UsuarioBL Usuario = new UsuarioBL();
            UsuarioDTO User = Usuario.Single(UserID);

            string URLSipsa = blParam.GetParametroChar("SIPSA.Url");
            string Usersign = User.sign;
            string Token = User.token;

            this.TADAction = URLSipsa;
            sign.Value = Usersign;
            token.Value = Token;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Summit1", "document.getElementById('form1').action = '" + URLSipsa + "';", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Summit2", "try {self.document.forms[0].submit();} catch(e){alert(e);} ", true);
            Response.Flush();
        }
    }
}