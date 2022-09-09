using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace SSIT.App_Components
{
    public class SecurePage : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            if (!HttpContext.Current.Request.IsAuthenticated)
                Response.Redirect("~/Default.aspx?ReturnUrl=" + HttpContext.Current.Request.Url.LocalPath);
                    
            base.OnInit(e);
        }
    }
    public class BasePage : System.Web.UI.Page
    {
        private static Random random = new Random((int)DateTime.Now.Ticks);//thanks to McAden

        public BasePage()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }


        public string GenerarCodigoSeguridad()
        {
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            char ch;
            for (int i = 0; i < 6; i++)
            {
                if (i % 2 == 0)
                {
                    ch = Convert.ToChar(random.Next(0, 9).ToString());
                }
                else
                {
                    ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                }
                builder.Append(ch);
            }

            return builder.ToString();
        }



        public static string IPtoDomain(string url)
        {

            string ret = url.Replace("10.20.72.31", "www.dghpsh.agcontrol.gob.ar");
            ret = url.Replace("10.20.72.23", "www.dghpsh.agcontrol.gob.ar");
            ret = url.Replace("10.20.72.11", "www.dghpsh.agcontrol.gob.ar");
            ret = url.Replace("azufral.agc", "www.dghpsh.agcontrol.gob.ar");

            return ret;


        }

        public void EjecutarScript(UpdatePanel upd, string scriptName)
        {
            System.Random rndm = new Random();

            int nroRandom = rndm.Next(1, 999);

            ScriptManager.RegisterStartupScript(upd, upd.GetType(),
                "script" + nroRandom.ToString(), scriptName, true);

        }
        public void EjecutarScript(Page pag, string scriptName)
        {
            Page.ClientScript.RegisterStartupScript(pag.GetType(), "script", scriptName, true);

        }
    }
}