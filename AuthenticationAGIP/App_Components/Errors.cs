using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthenticationAGIP.App_Components
{
    public static class Errors
    {
        
        public static string Log(Exception ex)
        {
            string id = null;

            var elmahCon = Elmah.ErrorLog.GetDefault(null);
            id = elmahCon.Log(new Elmah.Error(ex));

            return id;
        }

        public static string Get(string id)
        {
            string message = null;
            var elmahCon = Elmah.ErrorLog.GetDefault(null);
            var error = elmahCon.GetError(id);

            if (error != null)
                message = error.Error.Message;

            return message;
        }
    }
}