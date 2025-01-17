﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalService
{
    using System.Xml.Serialization;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Diagnostics;


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "ws_pagosSoap", Namespace = "http://tempuri.org/")]
    public partial class ws_pagos : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback GetConceptoOperationCompleted;

        private System.Threading.SendOrPostCallback GetConceptosOperationCompleted;

        private System.Threading.SendOrPostCallback GetConceptosConfigOperationCompleted;

        private System.Threading.SendOrPostCallback GenerarBoletaUnicaOperationCompleted;

        private System.Threading.SendOrPostCallback GenerarPagoElectronicoOperationCompleted;

        private System.Threading.SendOrPostCallback GetBoletasOperationCompleted;

        private System.Threading.SendOrPostCallback GetBoletaUnicaOperationCompleted;

        private System.Threading.SendOrPostCallback GetPagoElectronicoOperationCompleted;

        private System.Threading.SendOrPostCallback GetEstadoPosteriorAlPagoOperationCompleted;

        private System.Threading.SendOrPostCallback GetEstadoPagoOperationCompleted;

        private System.Threading.SendOrPostCallback GetPDFBoletaUnicaOperationCompleted;

        private System.Threading.SendOrPostCallback GetQROperationCompleted;

        private System.Threading.SendOrPostCallback AvisoPagoOperationCompleted;

        /// <remarks/>
        public ws_pagos()
        {
            this.Url = "http://www.dghpsh.agcontrol.gob.ar/test/webservices.agcontrol.gob.ar/ws_pagos.asm" +
                "x";
        }

        /// <remarks/>
        public event GetConceptoCompletedEventHandler GetConceptoCompleted;

        /// <remarks/>
        public event GetConceptosCompletedEventHandler GetConceptosCompleted;

        /// <remarks/>
        public event GetConceptosConfigCompletedEventHandler GetConceptosConfigCompleted;

        /// <remarks/>
        public event GenerarBoletaUnicaCompletedEventHandler GenerarBoletaUnicaCompleted;

        /// <remarks/>
        public event GenerarPagoElectronicoCompletedEventHandler GenerarPagoElectronicoCompleted;

        /// <remarks/>
        public event GetBoletasCompletedEventHandler GetBoletasCompleted;

        /// <remarks/>
        public event GetBoletaUnicaCompletedEventHandler GetBoletaUnicaCompleted;

        /// <remarks/>
        public event GetPagoElectronicoCompletedEventHandler GetPagoElectronicoCompleted;

        /// <remarks/>
        public event GetEstadoPosteriorAlPagoCompletedEventHandler GetEstadoPosteriorAlPagoCompleted;

        /// <remarks/>
        public event GetEstadoPagoCompletedEventHandler GetEstadoPagoCompleted;

        /// <remarks/>
        public event GetPDFBoletaUnicaCompletedEventHandler GetPDFBoletaUnicaCompleted;

        /// <remarks/>
        public event GetQRCompletedEventHandler GetQRCompleted;

        /// <remarks/>
        public event AvisoPagoCompletedEventHandler AvisoPagoCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetConcepto", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public BUConcepto GetConcepto(string user, string pass, int CodigoConcepto1, int CodigoConcepto2, int CodigoConcepto3, ref wsResultado clsResultado)
        {
            object[] results = this.Invoke("GetConcepto", new object[] {
                        user,
                        pass,
                        CodigoConcepto1,
                        CodigoConcepto2,
                        CodigoConcepto3,
                        clsResultado});
            clsResultado = ((wsResultado)(results[1]));
            return ((BUConcepto)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetConcepto(string user, string pass, int CodigoConcepto1, int CodigoConcepto2, int CodigoConcepto3, wsResultado clsResultado, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetConcepto", new object[] {
                        user,
                        pass,
                        CodigoConcepto1,
                        CodigoConcepto2,
                        CodigoConcepto3,
                        clsResultado}, callback, asyncState);
        }

        /// <remarks/>
        public BUConcepto EndGetConcepto(System.IAsyncResult asyncResult, out wsResultado clsResultado)
        {
            object[] results = this.EndInvoke(asyncResult);
            clsResultado = ((wsResultado)(results[1]));
            return ((BUConcepto)(results[0]));
        }

        /// <remarks/>
        public void GetConceptoAsync(string user, string pass, int CodigoConcepto1, int CodigoConcepto2, int CodigoConcepto3, wsResultado clsResultado)
        {
            this.GetConceptoAsync(user, pass, CodigoConcepto1, CodigoConcepto2, CodigoConcepto3, clsResultado, null);
        }

        /// <remarks/>
        public void GetConceptoAsync(string user, string pass, int CodigoConcepto1, int CodigoConcepto2, int CodigoConcepto3, wsResultado clsResultado, object userState)
        {
            if ((this.GetConceptoOperationCompleted == null))
            {
                this.GetConceptoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetConceptoOperationCompleted);
            }
            this.InvokeAsync("GetConcepto", new object[] {
                        user,
                        pass,
                        CodigoConcepto1,
                        CodigoConcepto2,
                        CodigoConcepto3,
                        clsResultado}, this.GetConceptoOperationCompleted, userState);
        }

        private void OnGetConceptoOperationCompleted(object arg)
        {
            if ((this.GetConceptoCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetConceptoCompleted(this, new GetConceptoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetConceptos", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public BUConcepto[] GetConceptos(string user, string pass, ref wsResultado clsResultado)
        {
            object[] results = this.Invoke("GetConceptos", new object[] {
                        user,
                        pass,
                        clsResultado});
            clsResultado = ((wsResultado)(results[1]));
            return ((BUConcepto[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetConceptos(string user, string pass, wsResultado clsResultado, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetConceptos", new object[] {
                        user,
                        pass,
                        clsResultado}, callback, asyncState);
        }

        /// <remarks/>
        public BUConcepto[] EndGetConceptos(System.IAsyncResult asyncResult, out wsResultado clsResultado)
        {
            object[] results = this.EndInvoke(asyncResult);
            clsResultado = ((wsResultado)(results[1]));
            return ((BUConcepto[])(results[0]));
        }

        /// <remarks/>
        public void GetConceptosAsync(string user, string pass, wsResultado clsResultado)
        {
            this.GetConceptosAsync(user, pass, clsResultado, null);
        }

        /// <remarks/>
        public void GetConceptosAsync(string user, string pass, wsResultado clsResultado, object userState)
        {
            if ((this.GetConceptosOperationCompleted == null))
            {
                this.GetConceptosOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetConceptosOperationCompleted);
            }
            this.InvokeAsync("GetConceptos", new object[] {
                        user,
                        pass,
                        clsResultado}, this.GetConceptosOperationCompleted, userState);
        }

        private void OnGetConceptosOperationCompleted(object arg)
        {
            if ((this.GetConceptosCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetConceptosCompleted(this, new GetConceptosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetConceptosConfig", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public BUIConceptoConfig[] GetConceptosConfig(string user, string pass, ref wsResultado clsResultado)
        {
            object[] results = this.Invoke("GetConceptosConfig", new object[] {
                        user,
                        pass,
                        clsResultado});
            clsResultado = ((wsResultado)(results[1]));
            return ((BUIConceptoConfig[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetConceptosConfig(string user, string pass, wsResultado clsResultado, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetConceptosConfig", new object[] {
                        user,
                        pass,
                        clsResultado}, callback, asyncState);
        }

        /// <remarks/>
        public BUIConceptoConfig[] EndGetConceptosConfig(System.IAsyncResult asyncResult, out wsResultado clsResultado)
        {
            object[] results = this.EndInvoke(asyncResult);
            clsResultado = ((wsResultado)(results[1]));
            return ((BUIConceptoConfig[])(results[0]));
        }

        /// <remarks/>
        public void GetConceptosConfigAsync(string user, string pass, wsResultado clsResultado)
        {
            this.GetConceptosConfigAsync(user, pass, clsResultado, null);
        }

        /// <remarks/>
        public void GetConceptosConfigAsync(string user, string pass, wsResultado clsResultado, object userState)
        {
            if ((this.GetConceptosConfigOperationCompleted == null))
            {
                this.GetConceptosConfigOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetConceptosConfigOperationCompleted);
            }
            this.InvokeAsync("GetConceptosConfig", new object[] {
                        user,
                        pass,
                        clsResultado}, this.GetConceptosConfigOperationCompleted, userState);
        }

        private void OnGetConceptosConfigOperationCompleted(object arg)
        {
            if ((this.GetConceptosConfigCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetConceptosConfigCompleted(this, new GetConceptosConfigCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GenerarBoletaUnica", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public BUBoletaUnica GenerarBoletaUnica(string user, string pass, BUDatosContribuyente datosConstribuyente, BUConcepto[] listaConcepto, ref wsResultado clsResultado)
        {
            object[] results = this.Invoke("GenerarBoletaUnica", new object[] {
                        user,
                        pass,
                        datosConstribuyente,
                        listaConcepto,
                        clsResultado});
            clsResultado = ((wsResultado)(results[1]));
            return ((BUBoletaUnica)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGenerarBoletaUnica(string user, string pass, BUDatosContribuyente datosConstribuyente, BUConcepto[] listaConcepto, wsResultado clsResultado, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GenerarBoletaUnica", new object[] {
                        user,
                        pass,
                        datosConstribuyente,
                        listaConcepto,
                        clsResultado}, callback, asyncState);
        }

        /// <remarks/>
        public BUBoletaUnica EndGenerarBoletaUnica(System.IAsyncResult asyncResult, out wsResultado clsResultado)
        {
            object[] results = this.EndInvoke(asyncResult);
            clsResultado = ((wsResultado)(results[1]));
            return ((BUBoletaUnica)(results[0]));
        }

        /// <remarks/>
        public void GenerarBoletaUnicaAsync(string user, string pass, BUDatosContribuyente datosConstribuyente, BUConcepto[] listaConcepto, wsResultado clsResultado)
        {
            this.GenerarBoletaUnicaAsync(user, pass, datosConstribuyente, listaConcepto, clsResultado, null);
        }

        /// <remarks/>
        public void GenerarBoletaUnicaAsync(string user, string pass, BUDatosContribuyente datosConstribuyente, BUConcepto[] listaConcepto, wsResultado clsResultado, object userState)
        {
            if ((this.GenerarBoletaUnicaOperationCompleted == null))
            {
                this.GenerarBoletaUnicaOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGenerarBoletaUnicaOperationCompleted);
            }
            this.InvokeAsync("GenerarBoletaUnica", new object[] {
                        user,
                        pass,
                        datosConstribuyente,
                        listaConcepto,
                        clsResultado}, this.GenerarBoletaUnicaOperationCompleted, userState);
        }

        private void OnGenerarBoletaUnicaOperationCompleted(object arg)
        {
            if ((this.GenerarBoletaUnicaCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GenerarBoletaUnicaCompleted(this, new GenerarBoletaUnicaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GenerarPagoElectronico", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public BUPagoElectronico GenerarPagoElectronico(string user, string pass, BUDatosContribuyente datosConstribuyente, BUConcepto[] listaConcepto, ref wsResultado clsResultado)
        {
            object[] results = this.Invoke("GenerarPagoElectronico", new object[] {
                        user,
                        pass,
                        datosConstribuyente,
                        listaConcepto,
                        clsResultado});
            clsResultado = ((wsResultado)(results[1]));
            return ((BUPagoElectronico)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGenerarPagoElectronico(string user, string pass, BUDatosContribuyente datosConstribuyente, BUConcepto[] listaConcepto, wsResultado clsResultado, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GenerarPagoElectronico", new object[] {
                        user,
                        pass,
                        datosConstribuyente,
                        listaConcepto,
                        clsResultado}, callback, asyncState);
        }

        /// <remarks/>
        public BUPagoElectronico EndGenerarPagoElectronico(System.IAsyncResult asyncResult, out wsResultado clsResultado)
        {
            object[] results = this.EndInvoke(asyncResult);
            clsResultado = ((wsResultado)(results[1]));
            return ((BUPagoElectronico)(results[0]));
        }

        /// <remarks/>
        public void GenerarPagoElectronicoAsync(string user, string pass, BUDatosContribuyente datosConstribuyente, BUConcepto[] listaConcepto, wsResultado clsResultado)
        {
            this.GenerarPagoElectronicoAsync(user, pass, datosConstribuyente, listaConcepto, clsResultado, null);
        }

        /// <remarks/>
        public void GenerarPagoElectronicoAsync(string user, string pass, BUDatosContribuyente datosConstribuyente, BUConcepto[] listaConcepto, wsResultado clsResultado, object userState)
        {
            if ((this.GenerarPagoElectronicoOperationCompleted == null))
            {
                this.GenerarPagoElectronicoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGenerarPagoElectronicoOperationCompleted);
            }
            this.InvokeAsync("GenerarPagoElectronico", new object[] {
                        user,
                        pass,
                        datosConstribuyente,
                        listaConcepto,
                        clsResultado}, this.GenerarPagoElectronicoOperationCompleted, userState);
        }

        private void OnGenerarPagoElectronicoOperationCompleted(object arg)
        {
            if ((this.GenerarPagoElectronicoCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GenerarPagoElectronicoCompleted(this, new GenerarPagoElectronicoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetBoletas", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public BUBoletaUnica[] GetBoletas(string user, string pass, int[] IdPagos, ref wsResultado clsResultado)
        {
            object[] results = this.Invoke("GetBoletas", new object[] {
                        user,
                        pass,
                        IdPagos,
                        clsResultado});
            clsResultado = ((wsResultado)(results[1]));
            return ((BUBoletaUnica[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetBoletas(string user, string pass, int[] IdPagos, wsResultado clsResultado, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetBoletas", new object[] {
                        user,
                        pass,
                        IdPagos,
                        clsResultado}, callback, asyncState);
        }

        /// <remarks/>
        public BUBoletaUnica[] EndGetBoletas(System.IAsyncResult asyncResult, out wsResultado clsResultado)
        {
            object[] results = this.EndInvoke(asyncResult);
            clsResultado = ((wsResultado)(results[1]));
            return ((BUBoletaUnica[])(results[0]));
        }

        /// <remarks/>
        public void GetBoletasAsync(string user, string pass, int[] IdPagos, wsResultado clsResultado)
        {
            this.GetBoletasAsync(user, pass, IdPagos, clsResultado, null);
        }

        /// <remarks/>
        public void GetBoletasAsync(string user, string pass, int[] IdPagos, wsResultado clsResultado, object userState)
        {
            if ((this.GetBoletasOperationCompleted == null))
            {
                this.GetBoletasOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetBoletasOperationCompleted);
            }
            this.InvokeAsync("GetBoletas", new object[] {
                        user,
                        pass,
                        IdPagos,
                        clsResultado}, this.GetBoletasOperationCompleted, userState);
        }

        private void OnGetBoletasOperationCompleted(object arg)
        {
            if ((this.GetBoletasCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetBoletasCompleted(this, new GetBoletasCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetBoletaUnica", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public BUBoletaUnica GetBoletaUnica(string user, string pass, int IdPago, ref wsResultado clsResultado)
        {
            object[] results = this.Invoke("GetBoletaUnica", new object[] {
                        user,
                        pass,
                        IdPago,
                        clsResultado});
            clsResultado = ((wsResultado)(results[1]));
            return ((BUBoletaUnica)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetBoletaUnica(string user, string pass, int IdPago, wsResultado clsResultado, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetBoletaUnica", new object[] {
                        user,
                        pass,
                        IdPago,
                        clsResultado}, callback, asyncState);
        }

        /// <remarks/>
        public BUBoletaUnica EndGetBoletaUnica(System.IAsyncResult asyncResult, out wsResultado clsResultado)
        {
            object[] results = this.EndInvoke(asyncResult);
            clsResultado = ((wsResultado)(results[1]));
            return ((BUBoletaUnica)(results[0]));
        }

        /// <remarks/>
        public void GetBoletaUnicaAsync(string user, string pass, int IdPago, wsResultado clsResultado)
        {
            this.GetBoletaUnicaAsync(user, pass, IdPago, clsResultado, null);
        }

        /// <remarks/>
        public void GetBoletaUnicaAsync(string user, string pass, int IdPago, wsResultado clsResultado, object userState)
        {
            if ((this.GetBoletaUnicaOperationCompleted == null))
            {
                this.GetBoletaUnicaOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetBoletaUnicaOperationCompleted);
            }
            this.InvokeAsync("GetBoletaUnica", new object[] {
                        user,
                        pass,
                        IdPago,
                        clsResultado}, this.GetBoletaUnicaOperationCompleted, userState);
        }

        private void OnGetBoletaUnicaOperationCompleted(object arg)
        {
            if ((this.GetBoletaUnicaCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetBoletaUnicaCompleted(this, new GetBoletaUnicaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetPagoElectronico", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public BUPagoElectronico GetPagoElectronico(string user, string pass, int IdPago, ref wsResultado clsResultado)
        {
            object[] results = this.Invoke("GetPagoElectronico", new object[] {
                        user,
                        pass,
                        IdPago,
                        clsResultado});
            clsResultado = ((wsResultado)(results[1]));
            return ((BUPagoElectronico)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetPagoElectronico(string user, string pass, int IdPago, wsResultado clsResultado, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetPagoElectronico", new object[] {
                        user,
                        pass,
                        IdPago,
                        clsResultado}, callback, asyncState);
        }

        /// <remarks/>
        public BUPagoElectronico EndGetPagoElectronico(System.IAsyncResult asyncResult, out wsResultado clsResultado)
        {
            object[] results = this.EndInvoke(asyncResult);
            clsResultado = ((wsResultado)(results[1]));
            return ((BUPagoElectronico)(results[0]));
        }

        /// <remarks/>
        public void GetPagoElectronicoAsync(string user, string pass, int IdPago, wsResultado clsResultado)
        {
            this.GetPagoElectronicoAsync(user, pass, IdPago, clsResultado, null);
        }

        /// <remarks/>
        public void GetPagoElectronicoAsync(string user, string pass, int IdPago, wsResultado clsResultado, object userState)
        {
            if ((this.GetPagoElectronicoOperationCompleted == null))
            {
                this.GetPagoElectronicoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetPagoElectronicoOperationCompleted);
            }
            this.InvokeAsync("GetPagoElectronico", new object[] {
                        user,
                        pass,
                        IdPago,
                        clsResultado}, this.GetPagoElectronicoOperationCompleted, userState);
        }

        private void OnGetPagoElectronicoOperationCompleted(object arg)
        {
            if ((this.GetPagoElectronicoCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetPagoElectronicoCompleted(this, new GetPagoElectronicoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetEstadoPosteriorAlPago", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetEstadoPosteriorAlPago(string user, string pass, int IdPago, ref wsResultado clsResultado)
        {
            object[] results = this.Invoke("GetEstadoPosteriorAlPago", new object[] {
                        user,
                        pass,
                        IdPago,
                        clsResultado});
            clsResultado = ((wsResultado)(results[1]));
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetEstadoPosteriorAlPago(string user, string pass, int IdPago, wsResultado clsResultado, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetEstadoPosteriorAlPago", new object[] {
                        user,
                        pass,
                        IdPago,
                        clsResultado}, callback, asyncState);
        }

        /// <remarks/>
        public string EndGetEstadoPosteriorAlPago(System.IAsyncResult asyncResult, out wsResultado clsResultado)
        {
            object[] results = this.EndInvoke(asyncResult);
            clsResultado = ((wsResultado)(results[1]));
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void GetEstadoPosteriorAlPagoAsync(string user, string pass, int IdPago, wsResultado clsResultado)
        {
            this.GetEstadoPosteriorAlPagoAsync(user, pass, IdPago, clsResultado, null);
        }

        /// <remarks/>
        public void GetEstadoPosteriorAlPagoAsync(string user, string pass, int IdPago, wsResultado clsResultado, object userState)
        {
            if ((this.GetEstadoPosteriorAlPagoOperationCompleted == null))
            {
                this.GetEstadoPosteriorAlPagoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetEstadoPosteriorAlPagoOperationCompleted);
            }
            this.InvokeAsync("GetEstadoPosteriorAlPago", new object[] {
                        user,
                        pass,
                        IdPago,
                        clsResultado}, this.GetEstadoPosteriorAlPagoOperationCompleted, userState);
        }

        private void OnGetEstadoPosteriorAlPagoOperationCompleted(object arg)
        {
            if ((this.GetEstadoPosteriorAlPagoCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetEstadoPosteriorAlPagoCompleted(this, new GetEstadoPosteriorAlPagoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetEstadoPago", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetEstadoPago(string user, string pass, int IdPago, ref wsResultado clsResultado)
        {
            object[] results = this.Invoke("GetEstadoPago", new object[] {
                        user,
                        pass,
                        IdPago,
                        clsResultado});
            clsResultado = ((wsResultado)(results[1]));
            return ((string)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetEstadoPago(string user, string pass, int IdPago, wsResultado clsResultado, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetEstadoPago", new object[] {
                        user,
                        pass,
                        IdPago,
                        clsResultado}, callback, asyncState);
        }

        /// <remarks/>
        public string EndGetEstadoPago(System.IAsyncResult asyncResult, out wsResultado clsResultado)
        {
            object[] results = this.EndInvoke(asyncResult);
            clsResultado = ((wsResultado)(results[1]));
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void GetEstadoPagoAsync(string user, string pass, int IdPago, wsResultado clsResultado)
        {
            this.GetEstadoPagoAsync(user, pass, IdPago, clsResultado, null);
        }

        /// <remarks/>
        public void GetEstadoPagoAsync(string user, string pass, int IdPago, wsResultado clsResultado, object userState)
        {
            if ((this.GetEstadoPagoOperationCompleted == null))
            {
                this.GetEstadoPagoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetEstadoPagoOperationCompleted);
            }
            this.InvokeAsync("GetEstadoPago", new object[] {
                        user,
                        pass,
                        IdPago,
                        clsResultado}, this.GetEstadoPagoOperationCompleted, userState);
        }

        private void OnGetEstadoPagoOperationCompleted(object arg)
        {
            if ((this.GetEstadoPagoCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetEstadoPagoCompleted(this, new GetEstadoPagoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetPDFBoletaUnica", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] GetPDFBoletaUnica(string user, string pass, int IdPago, ref wsResultado clsResultado)
        {
            object[] results = this.Invoke("GetPDFBoletaUnica", new object[] {
                        user,
                        pass,
                        IdPago,
                        clsResultado});
            clsResultado = ((wsResultado)(results[1]));
            return ((byte[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetPDFBoletaUnica(string user, string pass, int IdPago, wsResultado clsResultado, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetPDFBoletaUnica", new object[] {
                        user,
                        pass,
                        IdPago,
                        clsResultado}, callback, asyncState);
        }

        /// <remarks/>
        public byte[] EndGetPDFBoletaUnica(System.IAsyncResult asyncResult, out wsResultado clsResultado)
        {
            object[] results = this.EndInvoke(asyncResult);
            clsResultado = ((wsResultado)(results[1]));
            return ((byte[])(results[0]));
        }

        /// <remarks/>
        public void GetPDFBoletaUnicaAsync(string user, string pass, int IdPago, wsResultado clsResultado)
        {
            this.GetPDFBoletaUnicaAsync(user, pass, IdPago, clsResultado, null);
        }

        /// <remarks/>
        public void GetPDFBoletaUnicaAsync(string user, string pass, int IdPago, wsResultado clsResultado, object userState)
        {
            if ((this.GetPDFBoletaUnicaOperationCompleted == null))
            {
                this.GetPDFBoletaUnicaOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetPDFBoletaUnicaOperationCompleted);
            }
            this.InvokeAsync("GetPDFBoletaUnica", new object[] {
                        user,
                        pass,
                        IdPago,
                        clsResultado}, this.GetPDFBoletaUnicaOperationCompleted, userState);
        }

        private void OnGetPDFBoletaUnicaOperationCompleted(object arg)
        {
            if ((this.GetPDFBoletaUnicaCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetPDFBoletaUnicaCompleted(this, new GetPDFBoletaUnicaCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetQR", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] GetQR(string user, string pass, int IdPago, ref wsResultado clsResultado)
        {
            object[] results = this.Invoke("GetQR", new object[] {
                        user,
                        pass,
                        IdPago,
                        clsResultado});
            clsResultado = ((wsResultado)(results[1]));
            return ((byte[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginGetQR(string user, string pass, int IdPago, wsResultado clsResultado, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("GetQR", new object[] {
                        user,
                        pass,
                        IdPago,
                        clsResultado}, callback, asyncState);
        }

        /// <remarks/>
        public byte[] EndGetQR(System.IAsyncResult asyncResult, out wsResultado clsResultado)
        {
            object[] results = this.EndInvoke(asyncResult);
            clsResultado = ((wsResultado)(results[1]));
            return ((byte[])(results[0]));
        }

        /// <remarks/>
        public void GetQRAsync(string user, string pass, int IdPago, wsResultado clsResultado)
        {
            this.GetQRAsync(user, pass, IdPago, clsResultado, null);
        }

        /// <remarks/>
        public void GetQRAsync(string user, string pass, int IdPago, wsResultado clsResultado, object userState)
        {
            if ((this.GetQROperationCompleted == null))
            {
                this.GetQROperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetQROperationCompleted);
            }
            this.InvokeAsync("GetQR", new object[] {
                        user,
                        pass,
                        IdPago,
                        clsResultado}, this.GetQROperationCompleted, userState);
        }

        private void OnGetQROperationCompleted(object arg)
        {
            if ((this.GetQRCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetQRCompleted(this, new GetQRCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/AvisoPago", RequestNamespace = "http://tempuri.org/", ResponseNamespace = "http://tempuri.org/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int AvisoPago(string username, string password, System.Guid BUI_ID, string estado, [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)] System.Nullable<System.DateTime> fecha_pago, [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)] System.Nullable<System.DateTime> fecha_anulado, [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)] System.Nullable<System.DateTime> fecha_cancelado, ref wsResultado clsResultado)
        {
            object[] results = this.Invoke("AvisoPago", new object[] {
                        username,
                        password,
                        BUI_ID,
                        estado,
                        fecha_pago,
                        fecha_anulado,
                        fecha_cancelado,
                        clsResultado});
            clsResultado = ((wsResultado)(results[1]));
            return ((int)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginAvisoPago(string username, string password, System.Guid BUI_ID, string estado, System.Nullable<System.DateTime> fecha_pago, System.Nullable<System.DateTime> fecha_anulado, System.Nullable<System.DateTime> fecha_cancelado, wsResultado clsResultado, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("AvisoPago", new object[] {
                        username,
                        password,
                        BUI_ID,
                        estado,
                        fecha_pago,
                        fecha_anulado,
                        fecha_cancelado,
                        clsResultado}, callback, asyncState);
        }

        /// <remarks/>
        public int EndAvisoPago(System.IAsyncResult asyncResult, out wsResultado clsResultado)
        {
            object[] results = this.EndInvoke(asyncResult);
            clsResultado = ((wsResultado)(results[1]));
            return ((int)(results[0]));
        }

        /// <remarks/>
        public void AvisoPagoAsync(string username, string password, System.Guid BUI_ID, string estado, System.Nullable<System.DateTime> fecha_pago, System.Nullable<System.DateTime> fecha_anulado, System.Nullable<System.DateTime> fecha_cancelado, wsResultado clsResultado)
        {
            this.AvisoPagoAsync(username, password, BUI_ID, estado, fecha_pago, fecha_anulado, fecha_cancelado, clsResultado, null);
        }

        /// <remarks/>
        public void AvisoPagoAsync(string username, string password, System.Guid BUI_ID, string estado, System.Nullable<System.DateTime> fecha_pago, System.Nullable<System.DateTime> fecha_anulado, System.Nullable<System.DateTime> fecha_cancelado, wsResultado clsResultado, object userState)
        {
            if ((this.AvisoPagoOperationCompleted == null))
            {
                this.AvisoPagoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAvisoPagoOperationCompleted);
            }
            this.InvokeAsync("AvisoPago", new object[] {
                        username,
                        password,
                        BUI_ID,
                        estado,
                        fecha_pago,
                        fecha_anulado,
                        fecha_cancelado,
                        clsResultado}, this.AvisoPagoOperationCompleted, userState);
        }

        private void OnAvisoPagoOperationCompleted(object arg)
        {
            if ((this.AvisoPagoCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AvisoPagoCompleted(this, new AvisoPagoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/")]
    public partial class wsResultado
    {

        private int errorCodeField;

        private string errorDescriptionField;

        /// <comentarios/>
        public int ErrorCode
        {
            get
            {
                return this.errorCodeField;
            }
            set
            {
                this.errorCodeField = value;
            }
        }

        /// <comentarios/>
        public string ErrorDescription
        {
            get
            {
                return this.errorDescriptionField;
            }
            set
            {
                this.errorDescriptionField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/")]
    public partial class BUPagoElectronico
    {

        private string urlPagoElectronicoField;

        private int idPagoField;

        private decimal montoTotalField;

        private BUDatosContribuyente contribuyenteField;

        private string estadoNombreField;

        /// <comentarios/>
        public string UrlPagoElectronico
        {
            get
            {
                return this.urlPagoElectronicoField;
            }
            set
            {
                this.urlPagoElectronicoField = value;
            }
        }

        /// <comentarios/>
        public int IdPago
        {
            get
            {
                return this.idPagoField;
            }
            set
            {
                this.idPagoField = value;
            }
        }

        /// <comentarios/>
        public decimal MontoTotal
        {
            get
            {
                return this.montoTotalField;
            }
            set
            {
                this.montoTotalField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public BUDatosContribuyente Contribuyente
        {
            get
            {
                return this.contribuyenteField;
            }
            set
            {
                this.contribuyenteField = value;
            }
        }

        /// <comentarios/>
        public string EstadoNombre
        {
            get
            {
                return this.estadoNombreField;
            }
            set
            {
                this.estadoNombreField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/")]
    public partial class BUDatosContribuyente
    {

        private BUTipoPersona tipoPersonaField;

        private string apellidoyNombreField;

        private System.Nullable<BUTipodocumento> tipoDocField;

        private string documentoField;

        private string direccionField;

        private string pisoField;

        private string departamentoField;

        private string localidadField;

        private string codPostField;

        private string emailField;

        /// <comentarios/>
        public BUTipoPersona TipoPersona
        {
            get
            {
                return this.tipoPersonaField;
            }
            set
            {
                this.tipoPersonaField = value;
            }
        }

        /// <comentarios/>
        public string ApellidoyNombre
        {
            get
            {
                return this.apellidoyNombreField;
            }
            set
            {
                this.apellidoyNombreField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public System.Nullable<BUTipodocumento> TipoDoc
        {
            get
            {
                return this.tipoDocField;
            }
            set
            {
                this.tipoDocField = value;
            }
        }

        /// <comentarios/>
        public string Documento
        {
            get
            {
                return this.documentoField;
            }
            set
            {
                this.documentoField = value;
            }
        }

        /// <comentarios/>
        public string Direccion
        {
            get
            {
                return this.direccionField;
            }
            set
            {
                this.direccionField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string Piso
        {
            get
            {
                return this.pisoField;
            }
            set
            {
                this.pisoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string Departamento
        {
            get
            {
                return this.departamentoField;
            }
            set
            {
                this.departamentoField = value;
            }
        }

        /// <comentarios/>
        public string Localidad
        {
            get
            {
                return this.localidadField;
            }
            set
            {
                this.localidadField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string CodPost
        {
            get
            {
                return this.codPostField;
            }
            set
            {
                this.codPostField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string Email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/")]
    public enum BUTipoPersona
    {

        /// <comentarios/>
        Fisica,

        /// <comentarios/>
        Juridica,
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/")]
    public enum BUTipodocumento
    {

        /// <comentarios/>
        DNI,

        /// <comentarios/>
        CUIT,

        /// <comentarios/>
        LC,

        /// <comentarios/>
        CI,

        /// <comentarios/>
        LE,

        /// <comentarios/>
        PAS,
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/")]
    public partial class BUBoletaUnica
    {

        private int idPagoField;

        private string codBarrasField;

        private long nroBoletaUnicaField;

        private int dependenciaField;

        private BUDatosContribuyente contribuyenteField;

        private decimal montoTotalField;

        private int estadoIdField;

        private string estadoNombreField;

        private System.Nullable<System.DateTime> fechaPagoField;

        private System.Nullable<System.DateTime> fechaAnuladaField;

        private System.Nullable<System.DateTime> fechaCanceladaField;

        private string trazaPagoField;

        private string codigoVerificadorField;

        private string nroBUIField;

        private System.Nullable<System.Guid> bUI_IDField;

        /// <comentarios/>
        public int IdPago
        {
            get
            {
                return this.idPagoField;
            }
            set
            {
                this.idPagoField = value;
            }
        }

        /// <comentarios/>
        public string CodBarras
        {
            get
            {
                return this.codBarrasField;
            }
            set
            {
                this.codBarrasField = value;
            }
        }

        /// <comentarios/>
        public long NroBoletaUnica
        {
            get
            {
                return this.nroBoletaUnicaField;
            }
            set
            {
                this.nroBoletaUnicaField = value;
            }
        }

        /// <comentarios/>
        public int Dependencia
        {
            get
            {
                return this.dependenciaField;
            }
            set
            {
                this.dependenciaField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public BUDatosContribuyente Contribuyente
        {
            get
            {
                return this.contribuyenteField;
            }
            set
            {
                this.contribuyenteField = value;
            }
        }

        /// <comentarios/>
        public decimal MontoTotal
        {
            get
            {
                return this.montoTotalField;
            }
            set
            {
                this.montoTotalField = value;
            }
        }

        /// <comentarios/>
        public int EstadoId
        {
            get
            {
                return this.estadoIdField;
            }
            set
            {
                this.estadoIdField = value;
            }
        }

        /// <comentarios/>
        public string EstadoNombre
        {
            get
            {
                return this.estadoNombreField;
            }
            set
            {
                this.estadoNombreField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> FechaPago
        {
            get
            {
                return this.fechaPagoField;
            }
            set
            {
                this.fechaPagoField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> FechaAnulada
        {
            get
            {
                return this.fechaAnuladaField;
            }
            set
            {
                this.fechaAnuladaField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> FechaCancelada
        {
            get
            {
                return this.fechaCanceladaField;
            }
            set
            {
                this.fechaCanceladaField = value;
            }
        }

        /// <comentarios/>
        public string TrazaPago
        {
            get
            {
                return this.trazaPagoField;
            }
            set
            {
                this.trazaPagoField = value;
            }
        }

        /// <comentarios/>
        public string CodigoVerificador
        {
            get
            {
                return this.codigoVerificadorField;
            }
            set
            {
                this.codigoVerificadorField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public string NroBUI
        {
            get
            {
                return this.nroBUIField;
            }
            set
            {
                this.nroBUIField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
        public System.Nullable<System.Guid> BUI_ID
        {
            get
            {
                return this.bUI_IDField;
            }
            set
            {
                this.bUI_IDField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/")]
    public partial class BUIDetalleConcepto
    {

        private string descripcionField;

        private System.Guid idField;

        private System.Guid itemIDField;

        private string nombreField;

        private decimal valorField;

        /// <comentarios/>
        public string Descripcion
        {
            get
            {
                return this.descripcionField;
            }
            set
            {
                this.descripcionField = value;
            }
        }

        /// <comentarios/>
        public System.Guid ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <comentarios/>
        public System.Guid ItemID
        {
            get
            {
                return this.itemIDField;
            }
            set
            {
                this.itemIDField = value;
            }
        }

        /// <comentarios/>
        public string Nombre
        {
            get
            {
                return this.nombreField;
            }
            set
            {
                this.nombreField = value;
            }
        }

        /// <comentarios/>
        public decimal Valor
        {
            get
            {
                return this.valorField;
            }
            set
            {
                this.valorField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/")]
    public partial class BUIConceptoConfig
    {

        private bool admiteReglasField;

        private string codigoField;

        private string descripcionField;

        private BUIDetalleConcepto[] detallesField;

        private System.Guid idField;

        private bool tieneCantidadFijaField;

        private bool tieneValorFijoField;

        private decimal valorField;

        private int vigenciaField;

        /// <comentarios/>
        public bool AdmiteReglas
        {
            get
            {
                return this.admiteReglasField;
            }
            set
            {
                this.admiteReglasField = value;
            }
        }

        /// <comentarios/>
        public string Codigo
        {
            get
            {
                return this.codigoField;
            }
            set
            {
                this.codigoField = value;
            }
        }

        /// <comentarios/>
        public string Descripcion
        {
            get
            {
                return this.descripcionField;
            }
            set
            {
                this.descripcionField = value;
            }
        }

        /// <comentarios/>
        public BUIDetalleConcepto[] Detalles
        {
            get
            {
                return this.detallesField;
            }
            set
            {
                this.detallesField = value;
            }
        }

        /// <comentarios/>
        public System.Guid ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <comentarios/>
        public bool TieneCantidadFija
        {
            get
            {
                return this.tieneCantidadFijaField;
            }
            set
            {
                this.tieneCantidadFijaField = value;
            }
        }

        /// <comentarios/>
        public bool TieneValorFijo
        {
            get
            {
                return this.tieneValorFijoField;
            }
            set
            {
                this.tieneValorFijoField = value;
            }
        }

        /// <comentarios/>
        public decimal Valor
        {
            get
            {
                return this.valorField;
            }
            set
            {
                this.valorField = value;
            }
        }

        /// <comentarios/>
        public int Vigencia
        {
            get
            {
                return this.vigenciaField;
            }
            set
            {
                this.vigenciaField = value;
            }
        }
    }

    /// <comentarios/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/")]
    public partial class BUConcepto
    {

        private int codConcepto1Field;

        private int codConcepto2Field;

        private int codConcepto3Field;

        private int cantidadField;

        private decimal importeField;

        private decimal valorDetalleField;

        /// <comentarios/>
        public int CodConcepto1
        {
            get
            {
                return this.codConcepto1Field;
            }
            set
            {
                this.codConcepto1Field = value;
            }
        }

        /// <comentarios/>
        public int CodConcepto2
        {
            get
            {
                return this.codConcepto2Field;
            }
            set
            {
                this.codConcepto2Field = value;
            }
        }

        /// <comentarios/>
        public int CodConcepto3
        {
            get
            {
                return this.codConcepto3Field;
            }
            set
            {
                this.codConcepto3Field = value;
            }
        }

        /// <comentarios/>
        public int Cantidad
        {
            get
            {
                return this.cantidadField;
            }
            set
            {
                this.cantidadField = value;
            }
        }

        /// <comentarios/>
        public decimal Importe
        {
            get
            {
                return this.importeField;
            }
            set
            {
                this.importeField = value;
            }
        }

        /// <comentarios/>
        public decimal ValorDetalle
        {
            get
            {
                return this.valorDetalleField;
            }
            set
            {
                this.valorDetalleField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    public delegate void GetConceptoCompletedEventHandler(object sender, GetConceptoCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetConceptoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetConceptoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public BUConcepto Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((BUConcepto)(this.results[0]));
            }
        }

        /// <remarks/>
        public wsResultado clsResultado
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((wsResultado)(this.results[1]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    public delegate void GetConceptosCompletedEventHandler(object sender, GetConceptosCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetConceptosCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetConceptosCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public BUConcepto[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((BUConcepto[])(this.results[0]));
            }
        }

        /// <remarks/>
        public wsResultado clsResultado
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((wsResultado)(this.results[1]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    public delegate void GetConceptosConfigCompletedEventHandler(object sender, GetConceptosConfigCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetConceptosConfigCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetConceptosConfigCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public BUIConceptoConfig[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((BUIConceptoConfig[])(this.results[0]));
            }
        }

        /// <remarks/>
        public wsResultado clsResultado
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((wsResultado)(this.results[1]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    public delegate void GenerarBoletaUnicaCompletedEventHandler(object sender, GenerarBoletaUnicaCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GenerarBoletaUnicaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GenerarBoletaUnicaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public BUBoletaUnica Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((BUBoletaUnica)(this.results[0]));
            }
        }

        /// <remarks/>
        public wsResultado clsResultado
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((wsResultado)(this.results[1]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    public delegate void GenerarPagoElectronicoCompletedEventHandler(object sender, GenerarPagoElectronicoCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GenerarPagoElectronicoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GenerarPagoElectronicoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public BUPagoElectronico Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((BUPagoElectronico)(this.results[0]));
            }
        }

        /// <remarks/>
        public wsResultado clsResultado
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((wsResultado)(this.results[1]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    public delegate void GetBoletasCompletedEventHandler(object sender, GetBoletasCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetBoletasCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetBoletasCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public BUBoletaUnica[] Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((BUBoletaUnica[])(this.results[0]));
            }
        }

        /// <remarks/>
        public wsResultado clsResultado
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((wsResultado)(this.results[1]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    public delegate void GetBoletaUnicaCompletedEventHandler(object sender, GetBoletaUnicaCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetBoletaUnicaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetBoletaUnicaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public BUBoletaUnica Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((BUBoletaUnica)(this.results[0]));
            }
        }

        /// <remarks/>
        public wsResultado clsResultado
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((wsResultado)(this.results[1]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    public delegate void GetPagoElectronicoCompletedEventHandler(object sender, GetPagoElectronicoCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetPagoElectronicoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetPagoElectronicoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public BUPagoElectronico Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((BUPagoElectronico)(this.results[0]));
            }
        }

        /// <remarks/>
        public wsResultado clsResultado
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((wsResultado)(this.results[1]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    public delegate void GetEstadoPosteriorAlPagoCompletedEventHandler(object sender, GetEstadoPosteriorAlPagoCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetEstadoPosteriorAlPagoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetEstadoPosteriorAlPagoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }

        /// <remarks/>
        public wsResultado clsResultado
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((wsResultado)(this.results[1]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    public delegate void GetEstadoPagoCompletedEventHandler(object sender, GetEstadoPagoCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetEstadoPagoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetEstadoPagoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public string Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }

        /// <remarks/>
        public wsResultado clsResultado
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((wsResultado)(this.results[1]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    public delegate void GetPDFBoletaUnicaCompletedEventHandler(object sender, GetPDFBoletaUnicaCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetPDFBoletaUnicaCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetPDFBoletaUnicaCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
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

        /// <remarks/>
        public wsResultado clsResultado
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((wsResultado)(this.results[1]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    public delegate void GetQRCompletedEventHandler(object sender, GetQRCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetQRCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetQRCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
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

        /// <remarks/>
        public wsResultado clsResultado
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((wsResultado)(this.results[1]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    public delegate void AvisoPagoCompletedEventHandler(object sender, AvisoPagoCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.3038")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AvisoPagoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal AvisoPagoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public int Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }

        /// <remarks/>
        public wsResultado clsResultado
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((wsResultado)(this.results[1]));
            }
        }
    }
}
