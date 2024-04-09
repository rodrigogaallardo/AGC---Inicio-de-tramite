<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SSIT._Default" %>


<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
    <style>
        .table-borderless td, .table-borderless th {
            border: 0;
        }

        #MainContent_LoginView1_LoginControl {
            border: 0;
        }

        .container .jumbotron, .container-fluid .jumbotron {
            border-radius: 0 !important;
        }

        .table > thead > tr > th,
        .table > tbody > tr > th,
        .table > tfoot > tr > th,
        .table > thead > tr > td,
        .table > tbody > tr > td,
        .table > tfoot > tr > td {
            border-top: solid 0px #ccc !important;
        }

        @media screen and (min-width: 768px) {
            .container h1 {
                font-size: 63px;
            }

            .navbar-brand {
            }
        }

        @media screen and (max-width: 767px) {
            .container h1 {
                font-size: 28px;
                line-height: 35px;
            }

            .container h2 {
                font-size: 20px;
            }

            .navbar-brand {
                font-size: 13px;
                padding: 15px 0 15px 0;
            }
        }
    </style>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <%@ Import Namespace="SSIT.Common" %>

    <%--Login--%>
    <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
            <section>
                <%--Usuario Anónimo (Single loguear)--%>
                <div class="jumbotron jumbotron-misc jumbotron-main area-title" style="background-image: url('../content/img/general.jpg');" id="jumbotron">
                    <!-- NAVEGACIÓN PRINCIPAL -->
                    <nav class="navbar navbar-default" role="navigation">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="navbar-header">
                                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#main-nav">
                                        <span class="sr-only">Cambiar navegación</span>
                                        <span class="icon-bar"></span>
                                        <span class="icon-bar"></span>
                                        <span class="icon-bar"></span>
                                    </button>
                                    <a class="navbar-brand" href="http://buenosaires.gob.ar/agc" title="Gobierno Electrónico">Agencia Gubernamental de Control</a>
                                </div>
                                <div class="collapse navbar-collapse" id="main-nav">
                                    <ul class="nav navbar-nav navbar-right">
                                        <li><a href="<%=Page.ResolveUrl("~/")%>">Inicio</a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </nav>
                    <!-- FIN DE NAVEGACIÓN PRINCIPAL -->
                    <div class="big-heading">
                        <div class="container" style="text-align: left;">
                            <h1>Solicitudes de Inicio de Trámite de Autorización de Actividad Económica</h1>
                            <h2>Efectuá el Inicio online de Autorización de Actividad Económica que tienen encomienda digital o tu solicitud de Consulta al padrón que NO requiere encomienda profesional</h2>
                        </div>
                    </div>
                </div>
                <!-- CONTENIDO -->
                <main class="container-fluid" role="main">
                    <section>
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-offset-1 col-md-8 col-sm-8">
                                    <h3>También podes consultar el estado de los trámites en curso.</h3>
                                    <h4>Para poder iniciar trámites es necesario tener un usuario.</h4>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-offset-1 col-sm-8">
                                    <h2>Iniciar sesión </h2>
                                </div>
                            </div>
                        </div>
                        <div class="container-fluid">
                            <div class="form-group">
                                <asp:Panel ID="Panel1" runat="server" CssClass="container-fluid">
                                    <div class="form-horizontal" role="form">
                                        <div class="form-group">
                                            <asp:Label ID="labelAgip" runat="server" CssClass="col-sm-offset-2 col-sm-10">
                                            </asp:Label>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </section>
                    <%--<hr>--%>
                    <section>
                        <div class="container-fluid">
                            <%--<div class="row">
                                <asp:HyperLink ID="lnkConsultaProfesionales2" CssClass="col-md-offset-4 col-md-4 col-sm-12 shortcut" runat="server" NavigateUrl="~/Consulta_Profesionales2/ConsultaProfesionales2.aspx">
                               <span class="bg-danger-lt">
                               <span class="glyphicon imoon-user4 fs48 "></span>
                               </span>
                                   <h4>CONSULTA DE PROFESIONALES POR TIPO DE ACTIVIDAD </h4>
                               <p>Acced&eacute; al historial de los tr&aacute;mites presentados y eleg&iacute; un profesional acorde a lo que tu habilitaci&oacute;n requiere</p>
                                </asp:HyperLink>
                            </div>--%>
                            <div class="row">
                                <%--                                <a class="col-md-12 col-sm-12 shortcut" href="https://dghpsh.agcontrol.gob.ar/AsistenteOnline/Home/Index">
                                    <span class="bg-violet-lt">
                                        <span class="glyphicon glyphicon-phone"></span>
                                    </span>
                                    <h3>Asistente Online de<br />Habilitaciones</h3>
                                </a>--%>

                                <asp:LinkButton ID="lnkAsistenteOnline" CssClass="shortcut" runat="server">
                                                 <span class="bg-violet-lt">
                                                    <span class="glyphicon glyphicon-phone"></span>
                                                </span>
                                                <h3>Asistente Online de<br />Habilitaciones</h3>
                                </asp:LinkButton>

                            </div>


                            <div class="row">
                                <a class="col-md-4 col-sm-12 shortcut" href="http://www.buenosaires.gob.ar/agc/guia-de-tramites-de-la-agc">
                                    <span class="bg-blue-lt">
                                        <span class="glyphicon glyphicon-list-alt"></span>
                                    </span>
                                    <h3>Guía de trámites de la AGC</h3>
                                </a>
                                <a class="col-md-4 col-sm-12 shortcut" href="http://www.buenosaires.gob.ar/agc/consejos-profesionales">
                                    <span class="bg-violet-lt">
                                        <span class="glyphicon glyphicon-phone"></span>
                                    </span>
                                    <h3>Consejos profesionales</h3>
                                </a>
                                <a class="col-md-4 col-sm-12 shortcut" href="http://www.buenosaires.gob.ar/agc/direcciongeneralhabilitacionesypermisos">
                                    <span class="bg-warning-lt">
                                        <span class="glyphicon glyphicon-user"></span>
                                    </span>
                                    <h3>Preguntas frecuentes</h3>
                                </a>
                            </div>


                        </div>
                    </section>
                </main>
                <!-- FIN DE CONTENIDO -->
            </section>
        </AnonymousTemplate>
        <%--Usuario Logeado--%>
        <LoggedInTemplate>
            <table style="width: 98%; padding-top: 15px;">
                <tr>
                    <td style='vertical-align: top'>
                        <h2 class="titulo1 text-center">Solicitudes de Inicio de Trámites</h2>
                        <p style="padding-top: 10px; line-height: 20px; text-align: center">
                            Desde aquí, vas a poder efectuar el Inicio Online de los Trámites vinculados a tu Autorización de Actividad Económica.<br />
                        </p>
                        <p style="padding-top: 10px; line-height: 20px; text-align: center">
                            Podr&aacute; consultar su Libro Digital a trav&eacute;s de <a href="https://librodigital.agcontrol.gob.ar/">librodigital.agcontrol.gob.ar</a> para aquellas solicitudes que hayan sido libradas al uso a partir del 09/12/2020. Para poder ingresar al sistema deber&aacute; adherir el servicio de “Libro Digital de Inspecciones” en AGIP. Para mayor informaci&oacute;n sobre la funcionalidad de AGIP ingrese a <a href="https://www.agip.gob.ar/tutoriales">www.agip.gob.ar/tutoriales</a><br />
                        </p>
                        <div class="ptop10 pright20">
                            <nav class="navbar navbar-default" role="navigation">
                                <div class="container-fluid">
                                    <div class="text-center">
                                        <div class="ptop15 ">
                                            <strong>Tramites</strong>
                                        </div>
                                    </div>
                                </div>
                            </nav>
                        </div>
                        <div class="view view-shortcuts view-id-shortcuts pright30" style="display: flex !important; justify-content: center !important;">
                            <asp:LinkButton ID="LinkButton2" CssClass="col-md-3 col-sm-6 shortcut" runat="server" OnClientClick="return showfrmNuevaSolicitud();">
                                    <span class="bg-warning-dk">
                                            <span class="glyphicon imoon-file fs48"></span>
                                        </span>
                                    <h4 style="text-transform:uppercase">Nueva Autorización de Actividad Económica</h4>
                            </asp:LinkButton>

                            <asp:LinkButton ID="lnkCrearECI" CssClass="col-md-3 col-sm-6 shortcut" runat="server" PostBackUrl="~/Solicitud/HabilitacionECI/SelecInicioTramite.aspx">
                                    <span class="bg-blue-lt">
                                            <span class="glyphicon imoon-file fs48"></span>
                                        </span>
                                    <h4 style="text-transform:uppercase">Nuevo Espacio Cultural Independiente(ECI)</h4>
                            </asp:LinkButton>
                            <asp:LinkButton ID="lnkCrearTransferencia" CssClass="col-md-3 col-sm-6 shortcut" runat="server" OnClientClick="return showfrmNuevaTransferencia();">
                                    <span class="bg-violet-lt">
                                            <span class="glyphicon imoon-file fs48"></span>
                                        </span>
                                    <h4 style="text-transform:uppercase">Nueva Transmisión</h4>
                            </asp:LinkButton>

                            <asp:LinkButton ID="lnkCrearAmpliacion" CssClass="col-md-3 col-sm-6 shortcut" runat="server" PostBackUrl="~/Solicitud/Ampliacion/InicioTramite.aspx">
                                    <span class="bg-pink-lt">
                                            <span class="glyphicon imoon-file fs48"></span>
                                        </span>
                                    <h4 style="text-transform:uppercase">Nueva Ampliaci&oacute;n de Rubro y/o Superficie</h4>
                            </asp:LinkButton>

                            <asp:LinkButton ID="lnkCrearRedistribucionUso" CssClass="col-md-3 col-sm-6 shortcut" runat="server" PostBackUrl="~/Solicitud/RedistribucionUso/InicioTramite.aspx">
                                    <span class="bg-info-lt">
                                            <span class="glyphicon imoon-file fs48"></span>
                                        </span>
                                    <h4 style="text-transform:uppercase">Nueva Redistribuci&oacute;n de Usos</h4>
                            </asp:LinkButton>

                            <asp:LinkButton ID="lnkCrearPermisoMC" CssClass="col-md-3 col-sm-6 shortcut" runat="server" PostBackUrl="~/Solicitud/Permisos/Inicio">
                                    <span class="bg-success-dk">
                                            <span class="glyphicon imoon-file fs48"></span>
                                        </span>
                                    <h4 style="text-transform:uppercase">Nuevo Permiso Accesorio Actividad M&uacute;sica y Canto</h4>
                            </asp:LinkButton>

                        </div>

                        <div class="ptop10 pright20">
                            <nav class="navbar navbar-default" role="navigation">
                                <div class="container-fluid">
                                    <div class="text-center">
                                        <div class="ptop15 ">
                                            <strong>Instructivos</strong>
                                        </div>
                                    </div>
                                </div>
                            </nav>
                        </div>
                        <div class="view view-shortcuts view-id-shortcuts pright30" style="display: flex !important; justify-content: center !important;">
                            <asp:LinkButton ID="linkDescargaInstHab" CssClass="col-md-3 col-sm-6 shortcut" runat="server" OnClick="linkDescargaInstHab_Click">
                                                <span class="bg-warning-dk">
                                                    <span class="glyphicon imoon-download fs48"></span>
                                                </span>
                                                    <h4 style="text-transform:uppercase">Instructivo Autorización de Actividad Económica</h4>
                            </asp:LinkButton>
                            <asp:LinkButton ID="linkDescargaInstECI" CssClass="col-md-3 col-sm-6 shortcut" runat="server" OnClick="linkDescargaInstECI_Click">
                                            <span class="bg-blue-lt">
                                                <span class="glyphicon imoon-download fs48"></span>
                                            </span>
                                            <h4  style="text-transform:uppercase">Instructivo Espacio Cultural Independiente(ECI)</h4>
                            </asp:LinkButton>
                            <asp:LinkButton ID="linkDescargaInstTrans" CssClass="col-md-3 col-sm-6 shortcut" runat="server" OnClick="linkDescargaInstTrans_Click">
                                            <span class="bg-violet-lt">
                                                    <span class="glyphicon imoon-download fs48"></span>
                                                </span>
                                            <h4  style="text-transform:uppercase">Instructivo Transmisión</h4>
                            </asp:LinkButton>
                            <asp:LinkButton ID="linkDescargaInstAmpliaciones" CssClass="col-md-3 col-sm-6 shortcut" runat="server" OnClick="linkDescargaInstAmpliaciones_Click">
                                            <span class="bg-pink-lt">
                                                    <span class="glyphicon imoon-download fs48"></span>
                                                </span>
                                            <h4  style="text-transform:uppercase">Instructivo Ampliación de Rubro y/o Superficie</h4>
                            </asp:LinkButton>
                            <asp:LinkButton ID="linkDescargaInstRedistUso" CssClass="col-md-3 col-sm-6 shortcut" runat="server" OnClick="linkDescargaInstRedistUso_Click">
                                            <span class="bg-info-lt">
                                                <span class="glyphicon imoon-download fs48"></span>
                                            </span>
                                            <h4  style="text-transform:uppercase">Instructivo Redistribución de Usos</h4>
                            </asp:LinkButton>

                            <asp:LinkButton ID="linkDescargaInstPermisoMC" CssClass="col-md-3 col-sm-6 shortcut" runat="server" OnClick="linkDescargaInstPermisoMC_Click">
                                            <span class="bg-success-dk">
                                                <span class="glyphicon imoon-download fs48"></span>
                                            </span>
                                            <h4  style="text-transform:uppercase">Instructivo Permiso Accesorio Actividad M&uacute;sica y Canto</h4>
                            </asp:LinkButton>
                        </div>
                    </td>
                    <td style="width: 100px; border-left: 1px solid #e2e2e2; padding-left: 20px; vertical-align: top">
                        <asp:Panel ID="pnlLogueado" runat="server" Width="200px">
                            <br />

                            <br />
                            <br />
                            <div class="view view-shortcuts view-id-shortcuts ">
                                <div class="clearfix"></div>
                                <br />
                                <div class="ptop10">
                                    <nav class="navbar navbar-default" role="navigation">
                                        <div class="container-fluid">
                                            <div class="text-center">
                                                <div class="ptop15 ">
                                                    <strong>Consultas</strong>
                                                </div>
                                            </div>
                                        </div>
                                    </nav>
                                </div>
                                <div class="row">
                                    <asp:LinkButton ID="LinkButton1" CssClass="shortcut" runat="server" PostBackUrl="~/Solicitud/Bandeja">
                                                
                                                 <span class="bg-primary-lt">
                                                    <span class="glyphicon imoon-desktop fs48"></span>
                                                </span>
                                                <h4>MIS SOLICITUDES</h4>
                                    </asp:LinkButton>
                                </div>
                                <div class="row">
                                    <asp:LinkButton ID="lnkConsultaProfesionales" CssClass="shortcut" runat="server" PostBackUrl="~/Solicitud/ConsultaProfesionales.aspx">
                                               
                                                 <span class="bg-danger-lt">
                                                    <span class="glyphicon imoon-user4 fs48 "></span>
                                                </span>
                                                <h4>CONSULTA DE PROFESIONALES POR TIPO DE ACTIVIDAD </h4>
                                    </asp:LinkButton>
                                </div>
                                <div class="row">
                                    <asp:LinkButton ID="lnkConsultaEscribanos" CssClass=" shortcut" runat="server" PostBackUrl="~/Solicitud/ConsultaEscribanos.aspx">
                                                 <span class="bg-blue-dk">
                                                    <span class="glyphicon imoon-user4 fs48"></span>
                                                </span>
                                                <h4>CONSULTA DE ESCRIBANOS PARA ANEXO NOTARIAL</h4>
                                    </asp:LinkButton>
                                </div>
                            </div>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </LoggedInTemplate>
    </asp:LoginView>

    <%--Confirmar Nueva Solicitud--%>
    <div id="frmConfirmar_NuevaSolicitud" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top: -8px">Aviso</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon imoon-info fs64 color-blue"></i>
                            </td>
                            <td style="vertical-align: middle">
                                <p class="mleft10">
                                    Vas a solicitar el inicio de un trámite de <strong>NUEVA AUTORIZACION DE ACTIVIDAD ECONOMICA</strong>. A continuación, se te asignará un número identificador de tu Solicitud, 
                                    con el cual vas a poder continuar/consultar –en cualquier momento- a través la opción “Mis Solicitudes”.  
                                    <br />
                                    <br />

                                    <u>POR FAVOR, RECORDÁ NO GENERAR  MÁS DE UNA SOLICITUD PARA UN  MISMO LUGAR Y UN MISMO TITULAR, YA QUE LA ANTERIOR SERÁ ANULADA EN FORMA AUTOMÁTICA.</u>
                                </p>
                            </td>
                        </tr>
                    </table>

                    <asp:UpdatePanel ID="updExentoBUI" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlExentoBUI" runat="server" Visible="false">
                                <div class="col-sm-5 text-right">
                                    <i class="imoon imoon-dollar fs24 color-red"></i>
                                </div>
                                <div class="col-sm-7 ptop2">
                                    <asp:CheckBox ID="chkExentoBUI" runat="server" /><span class="mleft10">Tr&aacute;mite exento de Boleta &uacute;nica inteligente.</span>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer mleft20 mright20">
                    <asp:UpdatePanel ID="updConfirmarNuevaSolicitud" runat="server">
                        <ContentTemplate>
                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updConfirmarNuevaSolicitud">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />Procesando...
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div id="pnlBotonesConfirmarNuevaSolicitud" class="form-group">
                                    <asp:LinkButton ID="btnNuevaSolicitud" runat="server" CssClass="btn btn-primary" OnClick="btnNuevaSolicitud_Click"
                                        OnClientClick="ocultarBotonesConfirmarNuevaSolicitud();">
                                    <i class="imoon imoon-ok"></i>
                                        <span class="text">Aceptar</span>
                                    </asp:LinkButton>
                                    <button type="button" class="btn btn-default" data-dismiss="modal">
                                        <i class="imoon imoon-close"></i>
                                        <span class="text">Cancelar</span>
                                    </button>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->

    <%--Confirmar Nueva Solicitud--%>
    <div id="frmConfirmar_NuevaSolicitudCPadron" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top: -8px">Aviso</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon-info fs64 color-blue"></i>
                            </td>
                            <td style="vertical-align: middle">
                                <p class="mleft10">
                                    Vas a solicitar el inicio de un trámite de <strong>CONSULTA AL PADRÓN</strong>. A continuación, se te asignará un número identificador de tu Solicitud, 
                                    con el cual vas a poder continuar/consultar –en cualquier momento- a través la opción “Mis Solicitudes”.<br />
                                    <br />
                                    Una vez aprobada tu solicitud, se te informará -además- un Código de Seguridad asociado.<br />
                                    <br />
                                    <u>POR FAVOR, RECORDÁ NO GENERAR  MÁS DE UNA SOLICITUD PARA UNA  MISMA UBICACIÓN Y TITULAR, YA QUE LA ANTERIOR SERÁ ANULADA EN FORMA AUTOMÁTICA.</u>
                                </p>
                            </td>
                        </tr>
                    </table>


                </div>
                <div class="modal-footer mleft20 mright20">

                    <asp:UpdatePanel ID="updConfirmarNuevaSolicitudCPadron" runat="server">
                        <ContentTemplate>

                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="updConfirmarNuevaSolicitudCPadron">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />Procesando...
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div id="pnlBotonesConfirmarNuevaSolicitudCPadron" class="form-group">
                                    <asp:LinkButton ID="btnAceptar" runat="server" CssClass="btn btn-primary" OnClick="btnAceptar_Click"
                                        OnClientClick="ocultarBotonesConfirmarNuevaSolicitudCPadron();">
                                        <i class="imoon imoon-ok"></i>
                                        <span class="text">Aceptar</span>
                                    </asp:LinkButton>
                                    <button type="button" class="btn btn-default" data-dismiss="modal">
                                        <i class="imoon imoon-close"></i>
                                        <span class="text">Cancelar</span>
                                    </button>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->

    <div id="frmConfirmar_NuevaTransferencia" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top: -8px">Aviso</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon imoon-info fs64 color-blue"></i>
                            </td>
                            <td style="vertical-align: middle">
                                <p class="mleft10">
                                    Vas solicitar el inicio  un trámite de <strong>TRANSMISIÓN DE AUTORIZACION DE ACTIVIDAD ECONÓMICA</strong>, la misma podr&aacute; ser: 1) por Transferencia, 2) por Cambio de Denominaci&oacute;n o 3) por Oficio Judicial. Record&aacute; 
                                    que seg&uacute;n el art. 33 del Anexo I de la Resolución N° 150-AGC/2023, al producirse un cambio de titularidad de la Habilitaci&oacute;n/Autorizaci&oacute;n previamente otorgada, pod&eacute;s optar por iniciar un nuevo tr&aacute;mite de Autorizaci&oacute;n
                                     o solicitar la Transmisi&oacute;n del titular anterior.
                                </p>
                            </td>
                        </tr>
                    </table>

                </div>
                <div class="modal-footer mleft20 mright20">

                    <asp:UpdatePanel ID="updConfirmarNuevaTransferencia" runat="server">
                        <ContentTemplate>

                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="updConfirmarNuevaTransferencia">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />Procesando...
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div id="pnlBotonesConfirmarNuevaTransferencia" class="form-group">
                                    <asp:LinkButton ID="btnAceptarT" runat="server" CssClass="btn btn-primary" OnClick="lnkCrearTransferencia_Click"
                                        OnClientClick="ocultarBotonesConfirmarNuevaTransferencia();">
                                    <i class="imoon imoon-ok"></i>
                                        <span class="text">Aceptar</span>
                                    </asp:LinkButton>
                                    <button type="button" class="btn btn-default" data-dismiss="modal">
                                        <i class="imoon imoon-close"></i>
                                        <span class="text">Cancelar</span>
                                    </button>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->

    <%--Atencion implementacion--%>
    <div id="frmImplementacion" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top: -8px">Atención</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon-info fs64 color-blue"></i>
                            </td>
                            <td style="vertical-align: middle">
                                <p class="mleft10">
                                    Les informamos que estaremos realizando tareas de mantenimiento en el sistema desde el viernes 19 a las 18hs. El mismo volverá a estar disponible el lunes 22 a las 9hs.
                                    <br />
                                    Disculpen las molestias ocasionadas                                
                                </p>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->

    <div id="frmError" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top: -8px">Error</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon-remove-circle fs64" style="color: #f00"></i>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="updmpeInfo" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblError" runat="server" Style="color: Black"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>

                </div>
                <div class="modal-footer mleft20 mright20">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <%--modal de Avisos--%>
    <div id="ModalAvisoNotificacion" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top: -8px">Aviso importante</h4>
                </div>
                <div class="modal-body">
                    <label runat="server" id="lblModalNotificaciones"></label>
                </div>
                <div class="modal-footer mleft20 mright20">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <script type="text/ecmascript">

        function showfrmNuevaSolicitud() {
            $("#frmConfirmar_NuevaSolicitud").modal("show");
            return false;
        }

        function ocultarBotonesConfirmarNuevaSolicitudCPadron() {
            $("#pnlBotonesConfirmarNuevaSolicitudCPadron").hide();
            return false;
        }

        function ocultarBotonesConfirmarNuevaSolicitud() {
            $("#pnlBotonesConfirmarNuevaSolicitud").hide();
            return false;
        }
        function showfrmNuevaSolicitudCPadron() {
            $("#frmConfirmar_NuevaSolicitudCPadron").modal("show");
            return false;
        }
        function showfrmNuevaTransferencia() {
            $("#frmConfirmar_NuevaTransferencia").modal("show");
            return false;
        }


        function ocultarBotonesConfirmarNuevaTransferencia() {
            $("#pnlBotonesConfirmarNuevaTransferencia").hide();
            return false;
        }


        function showfrmError() {

            $("#frmError").modal("show");
            return false;
        }

        function hideModalAvisoNotificacion() {
            $("#ModalAvisoNotificacion").modal("hide");
            return false;
        }

        function showfrmImplementacion() {
            $("#frmImplementacion").modal("show");
            return false;
        }

        $(document).ready(function () {
            $("#MainContent_LoginView1_LoginControl").attr('border', 'none');
            //showfrmImplementacion();
        });

        function showModalAvisoNotificacion() {
            $("#ModalAvisoNotificacion").modal({ backdrop: 'static', keyboard: "false" });
            return false;
        }
    </script>
</asp:Content>
