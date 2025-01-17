﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace ExternalService.WSConsejo
{

    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Web.Services;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;

    // 
    // This source code was auto-generated by wsdl, Version=4.0.30319.33440.
    // 


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "WSConsejoSoap", Namespace = "http://tempuri.org/")]
    public partial class WSConsejo : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback GetPdfCertificadoEncomiendaExtOperationCompleted;

        /// <remarks/>
        public WSConsejo()
        {
            this.Url = "http://www.dghpsh.agcontrol.gob.ar/ConsejosProfesionales/WSConsejo.asmx";
        }

        /// <remarks/>
        public event GetPdfCertificadoEncomiendaExtCompletedEventHandler GetPdfCertificadoEncomiendaExtCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetPdfCertificadoEncomiendaExt", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] GetPdfCertificadoEncomiendaExt(string usuario, string clave, int tipo_tramite, int nro_tramite)
        {
            object[] results = this.Invoke("GetPdfCertificadoEncomiendaExt", new object[] {
                    usuario,
                    clave,
                    tipo_tramite,
                    nro_tramite});
            return ((byte[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetPdfCertificadoEncomiendaExt(string usuario, string clave, int tipo_tramite, int nro_tramite, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetPdfCertificadoEncomiendaExt", new object[] {
                    usuario,
                    clave,
                    tipo_tramite,
                    nro_tramite}, callback, asyncState);
        }

        /// <remarks/>
        public byte[] EndGetPdfCertificadoEncomiendaExt(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((byte[])(results[0]));
        }

        /// <remarks/>
        public void GetPdfCertificadoEncomiendaExtAsync(string usuario, string clave, int tipo_tramite, int nro_tramite)
        {
            this.GetPdfCertificadoEncomiendaExtAsync(usuario, clave, tipo_tramite, nro_tramite, null);
        }

        /// <remarks/>
        public void GetPdfCertificadoEncomiendaExtAsync(string usuario, string clave, int tipo_tramite, int nro_tramite, object userState)
        {
            if ((this.GetPdfCertificadoEncomiendaExtOperationCompleted == null))
            {
                this.GetPdfCertificadoEncomiendaExtOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetPdfCertificadoEncomiendaExtOperationCompleted);
            }
            this.InvokeAsync("GetPdfCertificadoEncomiendaExt", new object[] {
                    usuario,
                    clave,
                    tipo_tramite,
                    nro_tramite}, this.GetPdfCertificadoEncomiendaExtOperationCompleted, userState);
        }

        private void OnGetPdfCertificadoEncomiendaExtOperationCompleted(object arg)
        {
            if ((this.GetPdfCertificadoEncomiendaExtCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetPdfCertificadoEncomiendaExtCompleted(this, new GetPdfCertificadoEncomiendaExtCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
    public delegate void GetPdfCertificadoEncomiendaExtCompletedEventHandler(object sender, GetPdfCertificadoEncomiendaExtCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetPdfCertificadoEncomiendaExtCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetPdfCertificadoEncomiendaExtCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public byte[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((byte[])(this.results[0]));
            }
        }
    }
}