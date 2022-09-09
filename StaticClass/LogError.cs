using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Elmah;
using System.Threading;

namespace StaticClass
{
    public static class LogError
    {

        public static void Write(Exception ex, string mensajeError)
        {
            try
            {
                if (!(ex is ThreadAbortException))
                {
                    // log error to Elmah
                    if (!string.IsNullOrEmpty(mensajeError))
                    {
                        var nuevaEx = new Exception(mensajeError, ex);
                        logElmah(nuevaEx);
                    }
                    else
                    {
                        logElmah(ex);
                    }

                    // send errors to ErrorWS (my own legacy service)
                    // using (ErrorWSSoapClient client = new ErrorWSSoapClient())
                    // {
                    //    client.LogErrors(...);
                    // }
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public static void Write(Exception ex)
        {
            try
            {
                logElmah(ex);
                // send errors to ErrorWS (my own legacy service)
                // using (ErrorWSSoapClient client = new ErrorWSSoapClient())
                // {
                //    client.LogErrors(...);
                // }
            }
            catch (Exception)
            {
                // uh oh! just keep going
            }
        }

        public static void Write(string ex)
        {
            try
            {
                if(StaticClass.Funciones.isLogs())
                    logElmah(new Exception(ex));
                // send errors to ErrorWS (my own legacy service)
                // using (ErrorWSSoapClient client = new ErrorWSSoapClient())
                // {
                //    client.LogErrors(...);
                // }
            }
            catch (Exception)
            {
                // uh oh! just keep going
            }
        }
        private static void logElmah(Exception ex)
        {
            if (HttpContext.Current != null) //website is logging the error
                ErrorSignal.FromCurrentContext().Raise(ex, HttpContext.Current);

            else //non website, probably an agent
            {
                var elmahCon = Elmah.ErrorLog.GetDefault(null);
                elmahCon.Log(new Elmah.Error(ex));
            }

        }
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