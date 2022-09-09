using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using SSIT;
using Elmah;
using StaticClass;
using BusinesLayer.Implementation;
using BusinesLayer.MappingConfig;
using System.Text;
using System.IO;

namespace SSIT
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMapperConfig.RegisterMappingEncomienda();
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {

            //Exception ex = Server.GetLastError();
            //string path = "N/A";
            //if (sender is HttpApplication)
            //    path = ((HttpApplication)sender).Request.Url.PathAndQuery;


            //// Code that runs when an unhandled error occurs
            //using (StreamWriter w = File.AppendText(@"c:\temporal\LogErroresSSIT.txt"))
            //{
            //    string msg = Funciones.GetErrorMessage(ex);
            //    string error = string.Format("Path: {0}", path);
            //    error += Environment.NewLine;
            //    error += string.Format("Mensaje: {0}", msg);
            //    error += Environment.NewLine;
            //    error += "Stack: " + ex.StackTrace;

            //    Log(error, w);
                

            //}
        }

        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());

            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-------------------------------");
        }

        protected void ErrorLog_Filtering(object sender, ExceptionFilterEventArgs e)
        {
            if (e.Exception.Message == Errors.ENCOMIENDA_CANT_OPERARIOS || e.Exception.Message == Errors.ENCOMIENDA_PLANOS_EXISTENTE || e.Exception.Message == Errors.ENCOMIENDA_PROFESIONAL_INEXISTENTE_USUARIO
                || e.Exception.Message == Errors.ENCOMIENDA_PROFESIONAL_INHIBIDO || e.Exception.Message == Errors.ENCOMIENDA_REDISTRIBUION_USO_NO_SSP || e.Exception.Message == Errors.ENCOMIENDA_RUBROS_INVALIDOS
                || e.Exception.Message == Errors.ENCOMIENDA_SALON_VENTA_SIN_ESP || e.Exception.Message == Errors.ENCOMIENDA_SIN_CERTIFICADO_PRO_TEATRO || e.Exception.Message == Errors.ENCOMIENDA_SIN_PLANOS_CONTRA_INCENDIO
                || e.Exception.Message == Errors.ENCOMIENDA_SIN_PLANOS_HABILITACION || e.Exception.Message == Errors.ENCOMIENDA_SOLICITUD_DATOS_INVALIDOS || e.Exception.Message == EncomiendaBL.encomienda_solicitud_encomienda_en_curso
                || e.Exception.Message == Errors.ENCOMIENDA_SOLICITUD_ESTADO_ERRONEO || e.Exception.Message == Errors.ENCOMIENDA_SUM_SUP_RUBROS || e.Exception.Message == Errors.ENCOMIENDA_SUP_RUBROS || e.Exception.Message == Errors.ENCOMIENDA_SUPERFICIE_RUBRO
                || e.Exception.Message == Errors.SSIT_ENCOMIENDA_DATOS_INVALIDOS || e.Exception.Message == Errors.SSIT_ENCOMIENDA_ESTADO_INVALIDO || e.Exception.Message == Errors.SSIT_SOLICITUD_ANEXO_EN_CURSO
                || e.Exception.Message == Errors.SSIT_SOLICITUD_ANEXO_NOTARIAL_ANULADO || e.Exception.Message == Errors.SSIT_SOLICITUD_ANEXO_NOTARIAL_INEXISTENTE 
                || e.Exception.Message == Errors.SSIT_SOLICITUD_ANEXO_NOTARIAL_SIN_ARCHIVO || e.Exception.Message == Errors.SSIT_SOLICITUD_ANEXO_TECNICO_INEXISTENTE
                || e.Exception.Message == Errors.SSIT_SOLICITUD_CAA_INEXISTENTE || e.Exception.Message == Errors.SSIT_SOLICITUD_ENCOMIENDA_EXISTENTE || e.Exception.Message == Errors.SSIT_SOLICITUD_ENCOMIENDA_ORDEN_ERRONEO
                || e.Exception.Message == Errors.SSIT_SOLICITUD_ESTADO_INVALIDO_PRESENTAR || e.Exception.Message == Errors.SSIT_SOLICITUD_NORMATIVA_ANEXO_SIN_DOCUMENTO || e.Exception.Message == Errors.SSIT_SOLICITUD_OBSERVACIONES_SIN_PROCESAR
                || e.Exception.Message == Errors.SSIT_SOLICITUD_PAGO || e.Exception.Message == Errors.SSIT_SOLICITUD_PAGO_CAA || e.Exception.Message == Errors.SSIT_SOLICITUD_RELACIONADA_ESTADO_INVALIDO || e.Exception.Message == Errors.SSIT_SOLICITUD_TITULARES_UBICACIONES_DIFERENTES
                || e.Exception.Message == Errors.SSIT_SOLICITUD_UBICACIONES_CLAUSURAS || e.Exception.Message == Errors.SSIT_SOLICITUD_UBICACIONES_INHIBIDAS || e.Exception.Message == Errors.SSIT_TRANSFERENCIAS_SIN_ACTA_NOTARIAL 
                || e.Exception.Message == Errors.SSIT_TRANSFERENCIAS_SIN_EDICTOS || e.Exception.Message == Errors.SSIT_TRANSFERENCIAS_SIN_TITULARES
                || e.Exception.Message == TransferenciasSolicitudesBL.ssit_transferencia_consulta_padron_no_aprobada || e.Exception.Message == TransferenciasSolicitudesBL.ssit_transferencia_solicitud_iniciada
                || e.Exception.Message == EngineBL.engine_tarea_No_Puede_Tomarse || e.Exception.Message == EncomiendaBL.superficieRubroMayorASuperficieAHabilitar
                || e.Exception.Message == FirmantesBL.firmantesInexistentes || e.Exception.Message == Errors.ENCOMIENDA_SOLICITUD_INGRESAR_TITULARES || e.Exception.Message == Errors.ENCOMIENDA_CAMBIOS
                || e.Exception.Message == Errors.ENCOMIENDA_TRAMITE_BLOQUEADO|| e.Exception.Message == Errors.ENCOMIENDA_REDISTRIBUION_USO_NO_SSP
                || e.Exception.Message == Errors.ENCOMIENDA_NO_DATOS_LOCAL || e.Exception.Message == Errors.ENCOMIENDA_RUBRO  || e.Exception.Message == Errors.ENCOMIENDA_DATOS_LOCAL_OPERARIOS
                || e.Exception.Message == Errors.ENCOMIENDA_DATOS_LOCAL_SUPERFICIE_RUBRO || e.Exception.Message == Errors.ENCOMIENDA_DATOS_LOCAL_SUPERFICIE || e.Exception.Message == Errors.ENCOMIENDA_RUBRO_EXISTENTE
                || e.Exception.Message == Errors.UBICACION_IGUAL || e.Exception.Message == Errors.SSIT_SOLICITUD_SIN_TAREA_ABIERTA
                || e.Exception.Message == Errors.SSIT_SOLICITUD_INGRESAR_TITULARES|| e.Exception.Message == Errors.SSIT_TRANSFERENCIAS_SIN_TAREAS_REVISION
                || e.Exception.Message == Errors.SSIT_TRANSFERENCIAS_CPADRON_SIN_COINCIDENCIAS  || e.Exception.Message == Errors.SSIT_TRANSFERENCIAS_TAREA_NO_CREADA
                || e.Exception.Message == Errors.SSIT_CPADRON_NO_SE_PUEDE_CREAR|| e.Exception.Message == Errors.UBICACION_INHIBIDA || e.Exception.Message == Errors.SSIT_CPADRON_NONBRE_ARCHIVO
                || e.Exception.Message == Errors.SSIT_CPADRON_NO_ADMITE_CAMBIOS || e.Exception.Message == Errors.SSIT_CPADRON_SUPERFICIE_RUBRO_MAYOR
                || e.Exception.Message == Errors.SSIT_CPADRON_TIENE_RUBRO || e.Exception.Message == Errors.SSIT_CPADRON_RUBRO_NO_ENCONTRADO
                || e.Exception.Message == Errors.SSIT_CPADRON_RUBRO_NO_CATEGORIZADO_AMBIENTALMENTE 
                || e.Exception.Message == Errors.UBICACION_SIN_ZONIFICACION || e.Exception.Message == Errors.SSIT_CPADRON_RUBRO_ZONA || e.Exception.Message == Errors.SSIT_SOLICITUD_NO_CAMBIOS 
                || e.Exception.Message == SSITDocumentosAdjuntosBL.NoEsPosibleGenerarDocuementoEstado
                || e.Exception.Message == SSITDocumentosAdjuntosBL.NoEsPosibleEliminarDocuementoEstado || e.Exception.Message == TitularesBL.noExistenTitulares || e.Exception.Message == EncomiendaBL.encomienda_superficie_rubro
                || e.Exception.Message == SSITSolicitudesUbicacionesBL.solicitudConAnexoEnEstadoNoPermitido
                || e.Exception.Message == SSITSolicitudesBL.solicitud_Anexo_Notarial_Inexistente || e.Exception.Message == SSITSolicitudesBL.solicitud_certificado_caa_inexistente) e.Dismiss();
        }

        public void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            // Esto sirve para que cuando redirigen con el token autologuea
            if (Request.Form["token"] != null && !Request.Url.AbsoluteUri.Contains("AuthenticateAGIP"))
            {
                string returnUrl = Request.Url.AbsoluteUri;

                Account.AuthenticateAGIPProc auth = new Account.AuthenticateAGIPProc();
                auth.ReadData();
                Response.Redirect(returnUrl);

            }

        }

        private void RedireccionarAlSistema(string url, string token)
        {

            //HttpResponse response = HttpContext.Current.Response;
            Response.Clear();

            StringBuilder s = new StringBuilder();
            s.Append("<html>");
            s.AppendFormat("<body onload='document.forms[\"form\"].submit()'>");
            s.AppendFormat("<form name='form' action='{0}' method='post'>", url);
            s.AppendFormat("<input type='hidden' name='{0}' value='{1}' />", "token", token);
            s.Append("</form></body></html>");
            Response.Write(s.ToString());
            Response.End();

        }

    }
}
