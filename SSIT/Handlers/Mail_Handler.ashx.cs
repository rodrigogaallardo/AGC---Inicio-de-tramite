using BusinesLayer.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSIT.Handlers
{
    /// <summary>
    /// Summary description for Mail_Handler
    /// </summary>
    public class Mail_Handler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int IDMail = Convert.ToInt32(context.Request.QueryString["HtmlID"]);
            MailsBL mailBL = new MailsBL();
            var mailDTO = mailBL.Single(IDMail);
            var q = mailDTO.Mail_Html;
            context.Response.ContentType = "text/html";
            context.Response.Write(q);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}