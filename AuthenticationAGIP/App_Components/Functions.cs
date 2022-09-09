using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AuthenticationAGIP.Entity;

namespace AuthenticationAGIP.App_Components
{
    public static class Functions
    {
        public static bool isDesarrollo()
        {
            bool ret = false;
            string value = System.Configuration.ConfigurationManager.AppSettings["isDesarrollo"];
            if (!string.IsNullOrEmpty(value))
            {
                ret = Convert.ToBoolean(value);
            }

            return ret;
        }
        public static string GetAppSetting(string name)
        {
            string value = System.Configuration.ConfigurationManager.AppSettings[name];
            return value;
        }
        public static string GetParametroChar(string CodParam)
        {
            string ret = "";

            DGHP_SolicitudesEntities db = new DGHP_SolicitudesEntities();

            var param = db.Parametros.FirstOrDefault(x => x.cod_param == CodParam);
            if (param != null)
                ret = param.valorchar_param;

            db.Dispose();

            return ret;

        }

        public static decimal GetParametroNum(string CodParam)
        {
            decimal ret = 0;
            DGHP_SolicitudesEntities db = new DGHP_SolicitudesEntities();

            var param = db.Parametros.FirstOrDefault(x => x.cod_param == CodParam);
            if (param != null)
                ret = param.valornum_param.Value;

            db.Dispose();

            return ret;

        }

    }
}