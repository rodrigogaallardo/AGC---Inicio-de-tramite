using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Elmah;
using System.Text;
using System.Reflection;
using System.Globalization;
using System.IO;
using System.Web.Security;

namespace AnexoProfesionales.Common
{

    public class Functions
    {
        public const string DATOS_CONSULTA = "DATOS_CONSULTA";
        public static string GetUrlFoto(int seccion, string manzana, string parcela, int ancho, int alto)
        {
            string ret = "";
            string SMP = "";
            int tamaManzana = 3;
            int tamaParcela = 3;

            manzana = manzana.Trim();
            parcela = parcela.Trim();


            SMP += seccion.ToString().PadLeft(2, Convert.ToChar("0"));
            SMP += "-";

            if (manzana.Length > 0)
            {
                if (!Char.IsNumber(manzana, manzana.Length - 1))
                    tamaManzana = 4;
            }

            SMP += manzana.PadLeft(tamaManzana, Convert.ToChar("0"));
            SMP += "-";

            if (parcela.Length > 0)
            {
                if (!Char.IsNumber(parcela, parcela.Length - 1))
                    tamaParcela = 4;
            }

            SMP += parcela.PadLeft(tamaParcela, Convert.ToChar("0"));

            ret = string.Format("http://fotos.usig.buenosaires.gob.ar/getFoto?smp={0}&i=0&h={1}&w={2}", SMP, alto, ancho);

            return ret;

        }

        public static Guid GetUserid()
        {
            Guid userid = (Guid)Membership.GetUser().ProviderUserKey;
            return userid;
        }
        public static string GetUserName()
        {
            string ret = "";
            MembershipUser usu = Membership.GetUser();
            if (usu != null)
                ret = usu.UserName;

            return ret;
        }

     
        public static string Mail_Pruebas
        {
            get
            {
                string ret = "";
                string value = System.Configuration.ConfigurationManager.AppSettings["Mail.Pruebas"];
                if (!string.IsNullOrEmpty(value))
                {
                    ret = value.ToString();
                }

                return ret;
            }

        }
        public static string NVL(string value)
        {
            return (string.IsNullOrEmpty(value) ? "" : value);
        }

        public static int NVL(int? value)
        {
            return (value.HasValue ? value.Value : 0);
        }
        public static decimal NVL(decimal? value)
        {
            return (value.HasValue ? value.Value : 0);
        }

        public static string IPtoDomain(string url)
        {

            string ret = url.Replace("10.20.72.31", "www.dghpsh.agcontrol.gob.ar");
            ret = ret.Replace("10.20.72.23", "www.dghpsh.agcontrol.gob.ar");
            ret = ret.Replace("azufral.agc", "www.dghpsh.agcontrol.gob.ar");

            return ret;


        }
        public static string GetUrlMapa(int seccion, string manzana, string parcela, string Direccion)
        {

            string ret = "";
            string SMP = "";
            int tamaManzana = 3;
            int tamaParcela = 3;            
            manzana = manzana.Trim();
            parcela = parcela.Trim();
            Direccion = Direccion.Trim();

            SMP += seccion.ToString().PadLeft(2, Convert.ToChar("0"));
            SMP += "-";

            if (manzana.Length > 0)
            {
                if (!Char.IsNumber(manzana, manzana.Length - 1))
                    tamaManzana = 4;
            }

            SMP += manzana.PadLeft(tamaManzana, Convert.ToChar("0"));
            SMP += "-";

            if (parcela.Length > 0)
            {
                if (!Char.IsNumber(parcela, parcela.Length - 1))
                    tamaParcela = 4;
            }

            SMP += parcela.PadLeft(tamaParcela, Convert.ToChar("0"));

            ret = string.Format("http://servicios.usig.buenosaires.gob.ar/LocDir/mapa.phtml?dir={0}&desc={0}&w=400&h=300&punto=5&r=200&smp={1}",
                        Direccion, SMP);
            return ret;

        }
        public static string GetUrlCroquis(int seccion, string manzana, string parcela, string Direccion)
        {

            string ret = "";
            string SMP = "";
            int tamaManzana = 3;
            int tamaParcela = 3;
            manzana = manzana.Trim();
            parcela = parcela.Trim();
            Direccion = Direccion.Trim();

            SMP += seccion.ToString().PadLeft(2, Convert.ToChar("0"));
            SMP += "-";

            if (manzana.Length > 0)
            {
                if (!Char.IsNumber(manzana, manzana.Length - 1))
                    tamaManzana = 4;
            }

            SMP += manzana.PadLeft(tamaManzana, Convert.ToChar("0"));
            SMP += "-";

            if (parcela.Length > 0)
            {
                if (!Char.IsNumber(parcela, parcela.Length - 1))
                    tamaParcela = 4;
            }

            SMP += parcela.PadLeft(tamaParcela, Convert.ToChar("0"));

            ret = string.Format("http://servicios.usig.buenosaires.gob.ar/LocDir/mapa.phtml?dir={0}&w=400&h=300&punto=5&r=50&smp={1}",
                     Direccion, SMP);
            return ret;

        }


        public static string ConvertToBase64String(int value)
        {
            byte[] str1Byte = Encoding.ASCII.GetBytes(Convert.ToString(value));
            string base64 = Convert.ToBase64String(str1Byte);
            return base64;
        }

        public static string ImageNotFound(System.Web.UI.Page page)
        { 
            
            return page.ResolveClientUrl("~/Content/img/app/ImageNotFound.png");  
        }
        public static string GetErrorMessage(Exception ex)
        {
            string ret = ex.Message;


            if (ex.InnerException != null)
                ret = ex.InnerException.Message;

            return ret;
        }
    }
}