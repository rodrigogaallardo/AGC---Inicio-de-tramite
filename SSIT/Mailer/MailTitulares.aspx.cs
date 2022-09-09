using SSIT.App_Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSIT.Mailer
{
    public partial class MailTitulares : BasePage
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
            object pProf = Request.QueryString["Profesional"];

            if (pProf != null)
            {
                List<StaticClass.MailAnulacionAnexo> Listampr = new List<StaticClass.MailAnulacionAnexo>();

                StaticClass.MailAnulacionAnexo mpr = new StaticClass.MailAnulacionAnexo();

                mpr.Renglon1 = "Sr. Contribuyente,";
                mpr.Renglon2 = "Por medio del presente, se le informa que a su pedido, se ha procedido a realizar la desvinculación del profesional <b><em>" + pProf + "</em></b> para la gestión del ANEXO TECNICO de su Trámite.";
                mpr.Renglon3 = "Su próximo paso corresponde a la selección de un nuevo profesional de la construcción de los consejos habilitados para que genere la documentación de ANEXO TECNICO.";

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