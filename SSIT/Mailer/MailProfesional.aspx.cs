using SSIT.App_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Mailer
{
    public partial class MailProfesional : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((Mailer)this.Master).Titulo = "Anulación de Anexo Técnico.";
            }

        }
        public IEnumerable<StaticClass.MailAnulacionAnexo> GetData()
        {
            object pIdEnc = Request.QueryString["IdEncomienda"];

            if (pIdEnc != null)
            {
                List<StaticClass.MailAnulacionAnexo> Listampr = new List<StaticClass.MailAnulacionAnexo>();

                StaticClass.MailAnulacionAnexo mpr = new StaticClass.MailAnulacionAnexo();

                mpr.Renglon1 = "Sr. Profesional,";
                mpr.Renglon2 = "Por medio del presente, se le informa que el Anexo Técnico N: <b><em>" + pIdEnc + "</em></b> ha sido anulado, a pedido del solicitante.";

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