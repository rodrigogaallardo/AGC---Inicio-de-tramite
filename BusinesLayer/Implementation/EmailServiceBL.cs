using ExternalService;
using ExternalService.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinesLayer.Implementation
{
    public class EmailServiceBL
    {
        protected ExternalServiceMail _mailService;
     
        public EmailServiceBL()
        {
            _mailService = new ExternalServiceMail(getParametros());

        }

        public int SendMail(EmailEntity mailEntity)
        {
            EmailServicePOST Datosmail = new EmailServicePOST
            {
                Prioridad = mailEntity.Prioridad,
                Email = mailEntity.Email,
                Cc = mailEntity.Cc,
                Cco = mailEntity.Cco,
                Asunto = mailEntity.Asunto,
                IdTipoEmail = mailEntity.IdTipoEmail,
                Html = mailEntity.Html
            };

            return _mailService.SendMail(Datosmail);
        }

        public int SendMail(EmailServicePOST Datosmail)
        {
            return _mailService.SendMail(Datosmail);
        }

        public ParametrosService getParametros()
        {
            try
            {
                ParametrosService param = new ParametrosService();
                ParametrosBL paramBL = new ParametrosBL();
                param.hostUri = paramBL.GetParametroChar("rest.api.host.uri");
                param.autenticateNameHost = paramBL.GetParametroChar("rest.api.Param.Host.Autorizacion");
                param.serviceNameHost = paramBL.GetParametroChar("rest.api.Name.Host.Email");
                param.userName = paramBL.GetParametroChar("SGI.Username.WebService.Rest.Email");
                param.password = paramBL.GetParametroChar("SGI.Password.WebService.Rest.Email");
                return param;
            }
            catch
            {
                throw;
            }
        }

    }
}
