using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace SSIT
{
    public class BundleConfig
    {
        class AsIsBundleOrderer : IBundleOrderer
        {
            public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
            {
                return files;
            }
        }
        // Para obtener más información sobre la agrupación de archivos, visite http://go.microsoft.com/fwlink/?LinkId=254726
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/WebFormsJs").Include(
              "~/Scripts/bootstrap.js",
              "~/Scripts/Funciones.js",
              "~/Scripts/jquery.gritter.js",
              "~/Scripts/WebForms/WebForms.js",
              "~/Scripts/WebForms/WebUIValidation.js",
              "~/Scripts/WebForms/MenuStandards.js",
              "~/Scripts/WebForms/Focus.js",
              "~/Scripts/WebForms/GridView.js",
              "~/Scripts/WebForms/DetailsView.js",
              "~/Scripts/WebForms/TreeView.js",
              "~/Scripts/WebForms/WebParts.js",
              "~/Scripts/Datepicker_es.js",
              "~/Scripts/analitycs.js"
              ));

            bundles.Add(new ScriptBundle("~/bundles/MsAjaxJs").Include(
                "~/Scripts/WebForms/MsAjax/MicrosoftAjax.js",
                "~/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                "~/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                "~/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"));

            // Use la versión de desarrollo de Modernizr para desarrollar y aprender. Luego, cuando esté listo
            // para la producción, use la herramienta de creación en http://modernizr.com para elegir solo las pruebas que necesite
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/select2").Include(
            "~/Scripts/select2.full.js",
            "~/Scripts/Select2-locales/select2_locale_es.js"
            ));

            bundles.Add(new StyleBundle("~/bundles/Select2Css").Include(
                      "~/Content/select2.css"));

            bundles.Add(new ScriptBundle("~/bundles/fileUpload").Include(
                  "~/Scripts/jquery-fileupload/vendor/jquery.ui.widget.js",
                  "~/Scripts/jquery-fileupload/jquery.iframe-transport.js",
                  "~/Scripts/jquery-fileupload/jquery.fileupload.js"));

            bundles.Add(new StyleBundle("~/bundles/fileUploadCss").Include(
                     "~/Content/jQuery.FileUpload/css/jquery.fileupload.css",
                     "~/Content/jQuery.FileUpload/css/jquery.fileupload-ui.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",                   
                "~/Scripts/jquery-ui-{version}.js",
                "~/Scripts/jquery.gritter.js",
                "~/Scripts/jquery-ui.widget.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/flot").Include(
                      "~/Scripts/flot/jquery.flot.js",
                      "~/Scripts/flot/jquery.flot.pie.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/browser").Include(
                      "~/Scripts/browser.js"));

            bundles.Add(new ScriptBundle("~/bundles/autoNumeric").Include(
              "~/Scripts/autoNumeric/autoNumeric-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/timepicker").Include(
                "~/Scripts/jquery-ui-timepicker.js"));

            bundles.Add(new StyleBundle("~/bundles/timepickerCss").Include(
                      "~/Content/themes/base/timepicker.css"));
        }
    }
}