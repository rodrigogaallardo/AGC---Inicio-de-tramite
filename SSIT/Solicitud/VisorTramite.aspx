<%@ Page Async="true" Title="Visualizar Trámite" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VisorTramite.aspx.cs" Inherits="SSIT.VisorTramite" %>

<%@ Register Src="~/Solicitud/Habilitacion/Controls/DatosSolicitud.ascx" TagPrefix="uc" TagName="DatosSolicitud" %>
<%@ Register Src="~/Solicitud/Habilitacion/Controls/AnexoTecnico.ascx" TagPrefix="uc" TagName="AnexoTecnico" %>
<%@ Register Src="~/Solicitud/Habilitacion/Controls/AnexoNotarial.ascx" TagPrefix="uc" TagName="AnexoNotarial" %>
<%@ Register Src="~/Solicitud/Habilitacion/Controls/Tramite_CAA.ascx" TagPrefix="uc" TagName="Tramite_CAA" %>
<%@ Register Src="~/Solicitud/Habilitacion/Controls/PagosSolicitud.ascx" TagPrefix="uc" TagName="PagosSolicitud" %>
<%@ Register Src="~/Solicitud/Habilitacion/Controls/Documentos.ascx" TagPrefix="uc" TagName="Documentos" %>
<%@ Register Src="~/Solicitud/Habilitacion/Controls/Presentacion.ascx" TagPrefix="uc" TagName="Presentacion" %>
<%@ Register Src="~/Solicitud/Habilitacion/Controls/Rubros.ascx" TagPrefix="uc" TagName="Rubros" %>
<%@ Register Src="~/Solicitud/Habilitacion/Controls/AvisosSolicitud.ascx" TagPrefix="uc" TagName="Avisos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <%: Scripts.Render("~/bundles/select2") %>
    <%: Styles.Render("~/bundles/select2Css") %>
    <%: Scripts.Render("~/bundles/fileupload") %>
    <%: Styles.Render("~/bundles/fileuploadCss") %>


    <%--ajax cargando ...--%>
    <div id="Loading" style="text-align: center; padding-bottom: 20px; margin-top: 120px">
        <table border="0" style="border-collapse: separate; border-spacing: 5px; margin: auto">
            <tr>
                <td>
                    <img src="<%: ResolveUrl("~/Content/img/app/Loading128x128.gif") %>" alt="" />
                </td>
            </tr>
            <tr>
                <td style="font-size: 24px">Cargando datos del trámite
                </td>
            </tr>
        </table>
    </div>

    <div id="page_content" style="display: none">
        <%--extracto datos solicitud, acciones confirmar, imprimir, anular--%>
        <asp:UpdatePanel ID="updEstadoSolicitud" runat="server" RenderMode="Inline" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-7" style="margin-left: -105px">
                        <div class="row">
                            <label class="col-sm-5 text-right">Número de trámite:</label>
                            <asp:Label ID="lblNroSolicitud" CssClass="col-sm-7" runat="server" Font-Bold="true" Font-Size="16px" Style="color: #337ab7">"A Completar"</asp:Label>

                        </div>
                        <div class="row">
                            <label class="col-sm-5 text-right">Codigo de Seguridad:</label>
                            <asp:Label ID="lblCodigoDeSeguridad" CssClass="col-sm-7" runat="server" Font-Bold="true" Font-Size="16px" Style="color: #337ab7">"Debe confirmar la solicitud para verlo"</asp:Label>

                        </div>
                        <div class="row">
                            <label class="col-sm-5 text-right">Tipo de trámite:</label>
                            <asp:Label ID="lblTipoTramite" CssClass="col-sm-7" runat="server" Font-Bold="true"> "A COMPLETAR"</asp:Label>

                        </div>
                        <div class="row">
                            <label class="col-sm-5 text-right">Nro. Expediente:</label>
                            <asp:Label ID="lblNroExpediente" CssClass="col-sm-7" runat="server" Font-Bold="true"> </asp:Label>

                        </div>
                        <div class="row">
                            <label class="col-sm-5 text-right">Estado del trámite:</label>
                            <asp:Label ID="lblEstadoSolicitud" CssClass="col-sm-7" runat="server" Font-Bold="true">Incompleto</asp:Label>

                        </div>
                        <div class="row">
                            <label class="col-sm-5 text-right">Habilitación anterior:</label>
                            <asp:Label ID="lblHabAnterior" CssClass="col-sm-7" runat="server" Font-Bold="true" Font-Size="16px" Style="color: #337ab7"></asp:Label>

                        </div>
                    </div>
                    <div class="col-sm-6">

                        <div id="pnlProcesando" class="text-center" style="display: none">
                            <img src="<%: ResolveUrl("~/Content/img/app/Loading64x64.gif") %>" alt="" />
                            <span class="mleft10">Procesando...</span>
                        </div>

                        <div id="pnlShortcuts" class="view view-shortcuts view-id-shortcuts box-panel" style="display: inline-block; width: auto; float: right">
                            <div class="views-row col-sm-12 text-right" style="display: flex">

                                <asp:Panel ID="divbtnConfirmarTramite" runat="server" class=" display-inline-block ">

                                    <asp:LinkButton ID="btnConfirmarTramite" CssClass="shortcut shortcut-sm" Style="width: 7em; height: 10em" runat="server" title="Confirmar Solicitud de Tramite" OnClientClick="showfrmConfirmarTramite();">
                                            <span class="bg-info-lt">
                                                <span class="glyphicon imoon-ok fs48"></span>
                                            </span>
                                            <p >Confirmar</p>
                                    </asp:LinkButton>

                                </asp:Panel>

                                <asp:Panel ID="divbtnPresentarTramite" runat="server" class=" display-inline-block ">

                                    <asp:LinkButton ID="btnPresentarTramite" CssClass="shortcut shortcut-sm" Style="width: 7em; height: 10em" runat="server" title="Presentar Solicitud de Tramite" OnClientClick="return ocultarShortcuts();" OnClick="btnPresentarTramite_Click">
                                            <span class="bg-info-lt">
                                                <span class="glyphicon imoon-ok fs48"></span>
                                            </span>
                                            <p >Presentar</p>
                                    </asp:LinkButton>


                                </asp:Panel>


                                <asp:Panel ID="divbtnImprimirSolicitud" runat="server" class="display-inline-block">

                                    <asp:HyperLink ID="btnImprimirSolicitud" CssClass="shortcut shortcut-sm" Style="width: 7em; height: 10em" title="Descargar Declaración Responsable" runat="server" Target="_blank">
                                        <span class="bg-success-lt">
                                            <span class="glyphicon imoon-download fs48"></span>
                                        </span>
                                        <p >Descargar Declaración Responsable</p>
                                    </asp:HyperLink>

                                </asp:Panel>

                                <asp:Panel ID="divbtnOblea" runat="server" class=" display-inline-block ">

                                    <asp:HyperLink ID="btnOblea" CssClass="shortcut shortcut-sm" Style="width: 7em; height: 10em" title="Descargar Solicitud de Tramite" runat="server" Target="_blank">
                                        <span class="bg-success-lt">
                                            <span class="glyphicon imoon-qrcode fs48"></span>
                                        </span>
                                        <p >Descargar Oblea</p>
                                    </asp:HyperLink>

                                </asp:Panel>


                                <asp:Panel ID="pnlBandeja" runat="server" class=" display-inline-block ">

                                    <asp:LinkButton ID="btnBandeja" CssClass="shortcut shortcut-sm" Style="width: 7em; height: 10em" runat="server" title="Bandeja de Tramites" PostBackUrl="~/Solicitud/Bandeja.aspx">
                                            <span class="bg-primary-lt">
                                                <span class="glyphicon imoon-inbox fs48"></span>
                                            </span>
                                            <p >Bandeja</p>
                                    </asp:LinkButton>

                                </asp:Panel>

                                <asp:Panel ID="divbtnAnularTramite" runat="server" class=" display-inline-block ">

                                    <asp:LinkButton ID="btnAnularTramite" CssClass="shortcut shortcut-sm" Style="width: 7em; height: 10em" runat="server" title="Anular Solicitud de Tramite" OnClientClick="return showfrmConfirmarAnulacion1();">
                                            <span class="bg-gray-3">
                                                <span class="glyphicon imoon-close fs48 "></span>
                                            </span>
                                            <p >Anular</p>
                                    </asp:LinkButton>

                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
                <table border="0" style="width: 100%">
                    <tr>
                        <td style="width: 45%">
                            <div class="row">
                                <div class="col-md-5 text-right">
                                </div>
                                <div class="col-sm-7">
                                </div>

                            </div>
                            <div class="row" id="divCodigoSeguridad" runat="server" style="display: none">
                                <div class="col-md-5 text-right">
                                </div>
                                <div class="col-md-7">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5 text-right">
                                </div>
                                <div class="col-sm-7">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5 text-right mtop5">
                                </div>
                                <div class="col-md-7 mtop5">
                                </div>
                            </div>
                        </td>
                        <td class="text-right"></td>
                    </tr>
                </table>

                <asp:Panel ID="pnlTramiteIncompleto" runat="server" CssClass="alert alert-success mtop10" Visible="false" Width="100%">
                    <asp:Label ID="lblTextoTramiteIncompleto" runat="server"></asp:Label>
                </asp:Panel>

            </ContentTemplate>
        </asp:UpdatePanel>

        <%--Paneles con datos de la solicitud--%>
        <asp:UpdatePanel ID="updCargarDatos" runat="server" RenderMode="Inline" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btnCargarDatostramite" runat="server" Style="display: none" OnClick="btnCargarDatostramite_Click" />
                <asp:HiddenField ID="hid_id_solicitud" runat="server" />
                <asp:HiddenField ID="hid_id_estado" runat="server" />
                <asp:HiddenField ID="hid_tab_selected" runat="server" Value="0" />
                <asp:UpdatePanel ID="updAlertas" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlAlertasSolicitud" runat="server" Visible="false" CssClass="alert alert-danger"
                            Style="padding-bottom: 5px; padding-top: 5px">
                            <asp:Label ID="lblAlertasSolicitud" runat="server"></asp:Label>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <br />
                <div id="tabsVisorTramite">
                    <div class="box-panel">
                        <div style="margin: 20px; margin-top: -5px">
                            <div style="margin-top: 5px; color: #377bb5">
                                <h4></i>Panel de Trámite!</h4>
                                <hr />
                            </div>
                        </div>
                        <ul class="view view-shortcuts view-id-shortcuts pright30" style="display: flex !important; justify-content: center !important;">

                            <li id="liDatossolicitud" runat="server" style="display: inline-block;">
                                <a href="#tabDatosSolicitud" onclick="colorear(0)" class="shortcut shortcut-md" style="width: 10em; height: 14em" target="_blank">
                                    <span id="spnDatosSolicitud" runat="server">
                                        <span class="glyphicon imoon-file fs48"></span>
                                    </span>
                                    <p>Datos de la Solicitud</p>
                                </a>
                            </li>
                            <li id="liAnexoTecnico" runat="server" style="display: inline-block;">
                                <a id="HyperLink1" href="#tabAnexoTecnico" onclick="colorear(1)" class="shortcut shortcut-md" style="width: 10em; height: 14em" target="_blank">
                                    <span id="spnAnexoTecnico" runat="server">
                                        <span class="glyphicon imoon-file2 fs48"></span>
                                    </span>
                                    <p>Anexo Técnico</p>
                                </a>
                            </li>
                            <li id="liAnexoNotarial" runat="server" style="display: inline-block;">
                                <a id="A1" href="#tabAnexoNotarial" onclick="colorear(2)" class="shortcut shortcut-md" style="width: 10em; height: 14em" target="_blank">
                                    <span id="spnAnexoNotarial" runat="server">
                                        <span class="glyphicon imoon-check fs48"></span>
                                    </span>
                                    <p>Anexo Notarial</p>
                                </a>
                            </li>
                            <li id="liApra" runat="server" style="display: inline-block;">
                                <a id="A2" href="#tabApra" onclick="colorear(3)" class="shortcut shortcut-md" style="width: 10em; height: 14em" target="_blank">
                                    <span id="spntabApra" runat="server">
                                        <span class="glyphicon imoon-leaf fs48"></span>
                                    </span>
                                    <p>CAA</p>
                                </a>
                            </li>
                            <li id="liDocumentos" runat="server" style="display: inline-block;">
                                <a id="A6" href="#tabDocumentos" onclick="colorear(4)" class="shortcut shortcut-md" style="width: 10em; height: 14em" target="_blank">
                                    <span id="spntabDocumentos" runat="server">
                                        <span class="glyphicon imoon-file2 fs48"></span>
                                    </span>
                                    <p>Documentos Adjuntos</p>
                                </a>
                            </li>
                            <li id="liBui" runat="server" style="display: inline-block;">
                                <a id="A3" href="#tabBui" onclick="colorear(5)" class="shortcut shortcut-md" style="width: 10em; height: 14em" target="_blank">
                                    <span id="spntabBui" runat="server">
                                        <span class="glyphicon imoon-dollar fs48"></span>
                                    </span>
                                    <p>Pagos</p>
                                </a>
                            </li>
                            <li id="liPresentacion" runat="server" style="display: inline-block;">
                                <a id="A7" href="#tabPresentacion" onclick="colorear(6)" class="shortcut shortcut-md" style="width: 10em; height: 14em" target="_blank">
                                    <span id="spntabPresentacion" runat="server">
                                        <span class="glyphicon imoon-busy fs48"></span>
                                    </span>
                                    <p style="font-size: 15px">Observaciones e Historial</p>
                                </a>
                            </li>
                            <li id="liAvisos" runat="server" style="display: inline-block;">
                                <a id="A8" href="#tabAvisos" onclick="colorear(7)" class="shortcut shortcut-md" style="width: 10em; height: 14em" target="_blank">
                                    <span id="spantabAvisos" runat="server">
                                        <span class="glyphicon imoon-bell fs48"></span>
                                    </span>
                                    <p style="font-size: 15px">Avisos</p>
                                </a>
                            </li>

                            <a id="A9" href="https://librodigital.agcontrol.gob.ar/" onclick="colorear(8)" class="shortcut shortcut-md" style="width: 10em; height: 14em" target="_self">
                                <span id="spantabLibroDigital" runat="server">
                                    <span class="glyphicon imoon-file2 fs48"></span>
                                </span>
                                <p>Consulta de libro digital</p>
                            </a>

                        </ul>
                    </div>
                    <br />
                    <div id="tabDatosSolicitud">
                        <uc:DatosSolicitud runat="server" ID="visDatosSolicitud" />
                    </div>
                    <div id="tabAnexoTecnico">
                        <uc:AnexoTecnico runat="server" ID="visAnexoTecnico" />
                    </div>
                    <div id="tabAnexoNotarial">
                        <uc:AnexoNotarial runat="server" ID="visAnexoNotarial" />
                    </div>
                    <div id="tabApra">
                        <uc:Tramite_CAA runat="server" ID="visTramite_CAA" OnError="visTramite_CAA_Error" OnEventRecargarPagos="visTramite_CAA_EventRecargarPagos" />
                    </div>
                    <div id="tabDocumentos">
                        <uc:Documentos runat="server" ID="visDocumentos" OnEventRecargarPagos="visDocumentos_EventRecargarPagos" />
                    </div>
                    <div id="tabBui">
                        <uc:PagosSolicitud runat="server" ID="visPagosSolicitud" OnPSErrorClick="PagosError_Click" />
                    </div>
                    <div id="tabPresentacion">
                        <uc:Presentacion runat="server" ID="visPresentacion" />
                    </div>
                    <div id="tabAvisos">
                        <uc:Avisos runat="server" ID="visAviso" />
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <%--modal de Errores--%>
    <div id="frmError" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top: -8px">Aviso</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="udpError" runat="server">
                        <ContentTemplate>
                            <table style="border-collapse: separate; border-spacing: 5px">
                                <tr>
                                    <td style="text-align: center; vertical-align: text-top">
                                        <i class="imoon imoon-remove-circle fs64" style="color: #f00"></i>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="updmpeInfo" runat="server" class="form-group">
                                            <ContentTemplate>
                                                <asp:Label ID="lblError" runat="server" Style="color: Black"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer mleft20 mright20">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->

    <%--Modal Confirmar Anulación--%>
    <div id="frmConfirmarAnulacion1" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content" style="width: 725px; margin-left: -80px">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top: -8px">Anular tr&aacute;mite</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon-remove-circle fs64 color-blue"></i>
                            </td>
                            <td style="padding-left: 20px">
                                <asp:Label runat="server">Seleccionaste la opción de <strong>Anular</strong> la Solitud. De esta manera, <strong>toda la información ingresada perderá validez</strong></asp:Label>
                                <br />
                                <br />
                                <asp:Label runat="server">Recordá que si intentás realizar una nueva solicitud para el mismo trámite, ya <strong>NO PODRÁS REUTILIZAR</strong> el <u>Nro de Solicitud con su Código de Seguridad,</u> <u>el Anexo Técnico (Profesional),</u> <u>el
                                    Anexo Notarial (Escribano)</u> ni el <u>Certificado de Aptitud Ambiental</u>. Además, tendrás que generar las boletas del nuevo Trámite, y <strong>volver a pagar</strong>.</asp:Label>
                                <br />
                                <br />
                                <asp:Label runat="server"> ¿Estas seguro de que querés ANULAR esta solicitud?</asp:Label>

                            </td>
                        </tr>
                    </table>

                </div>
                <div class="modal-footer mleft20 mright20">

                    <asp:UpdatePanel ID="updConfirmarAnular" runat="server">
                        <ContentTemplate>

                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updConfirmarAnular">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnAnular_Si" runat="server" CssClass="btn btn-default" Text="Sí" OnClientClick="return hidefrmConfirmarAnulacion1(); showfrmConfirmarAnulacion2();" />
                                    <button type="button" class="btn btn-primary" data-dismiss="modal">No</button>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div id="frmConfirmarAnulacion2" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content" style="width: 780px; margin-left: -110px">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top: -8px">Anular tr&aacute;mite</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon-remove-circle fs64 color-blue"></i>
                            </td>
                            <td style="vertical-align: middle; padding-left: 20px">
                                <asp:Label runat="server">Estás a punto de <strong>Anular la Solicitud, haciendo que toda la información ingresada pierda validez.</strong> <br /> ¿Estás seguro?</asp:Label>
                            </td>
                        </tr>
                    </table>

                </div>
                <div class="modal-footer mleft20 mright20">

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>

                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-default" Text="Sí" OnClick="btnAnularTramite_Click" />
                                    <button type="button" class="btn btn-primary" data-dismiss="modal">No</button>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->
    <%--Modal Confirmar Solicitud--%>
    <div id="frmConfirmarSolicitud2" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top: -8px">Datos de la Solicitud</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="udpConfirmarSolcitud" runat="server">
                        <ContentTemplate>
                            <div class="form-horizontal pright10">
                                <div class="form-group">
                                    <label class="control-label col-sm-5" style="font-weight: Bold !important;">Solicitud de trámite nro:</label>
                                    <asp:Label ID="nroSolicitudModal" CssClass="col-sm-4" runat="server" Font-Bold="true" Style="color: #337ab7; margin-top: 5px; font-size: 18px"></asp:Label>
                                </div>
                            </div>
                            <div class="form-horizontal pright10">
                                <div class="form-group">
                                    <label class="control-label col-sm-5" style="font-weight: Bold !important;">Código de Seguridad:</label>
                                    <asp:Label ID="lblCodSeguridad" CssClass="col-sm-4" runat="server" Font-Bold="true" Style="color: #337ab7; margin-top: 5px; font-size: 18px"></asp:Label>
                                </div>
                            </div>
                            <div class="form-horizontal pright10 box-panel">
                                <div class="form-group">
                                    <p class="col-sm-12">
                                        <asp:Label ID="lblConfirmarModal" runat="server" Text='Conserve estos datos, ya que le serán requeridos para completar el Anexo Técnico de su trámite. También podrá descargarlos desde el botón "Descargar declaración responsable" sí lo desea.'></asp:Label>
                                    </p>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer mleft20 mright20">

                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>

                            <div class="form-inline">
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary btn-lg" OnClientClick="return hideModalConfirmarSolicitud(); ">
                                   <span class="text">Continuar</span> 
                                   <i class="imoon imoon-chevron-right"></i>                               
                                </asp:LinkButton>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->
    <%--Modal Confirmar Solicitud--%>
    <div id="frmConfirmarSolicitud1" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top: -8px">Confirmar Solicitud</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="udpConfirmarSolcitud1" runat="server">
                        <ContentTemplate>

                            <div class="form-horizontal pright10">
                                <div class="form-group">
                                    <p class="col-sm-12">¿Está seguro de que desea confirmar la solicitud?</p>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer mleft20 mright20">

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updConfirmarAnular">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div class="form-group">
                                    <asp:LinkButton ID="Button2" runat="server" CssClass="btn btn-primary" Text="Sí" OnClientClick="return hidefrmConfirmarSolicitud(); " OnClick="btnConfirmarTramite_Click"></asp:LinkButton>
                                    <button type="button" class="btn btn-default" data-dismiss="modal">No</button>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->



    <%--Modal DocumentosFaltantes--%>
    <div id="frmDocumentosFaltantes" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top: -8px">Documentos Requeridos</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updDocumentosFaltantes" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <div class="form-horizontal pright10 pleft10">
                                <div class="form-group">


                                    <asp:GridView ID="grdDocumentosFaltantes"
                                        runat="server"
                                        AutoGenerateColumns="false"
                                        AllowPaging="true" PageSize="30"
                                        ItemType="DataTransferObject.RubrosTipoDocReqDTO"
                                        Style="border: none;"
                                        CssClass="table table-bordered mtop5"
                                        GridLines="None" Width="100%">
                                        <HeaderStyle CssClass="grid-header" />
                                        <RowStyle CssClass="grid-row" />
                                        <AlternatingRowStyle BackColor="#efefef" />
                                        <Columns>
                                            <asp:BoundField DataField="CodRubro" DataFormatString="{0:d}" HeaderText="Código del Rubro que lo exige" HeaderStyle-CssClass="text-center" ItemStyle-Width="90px" />
                                            <asp:BoundField DataField="NombreDoc" DataFormatString="{0:d}" HeaderText="Documento requerido" HeaderStyle-CssClass="text-center" ItemStyle-Width="90px" />
                                        </Columns>

                                    </asp:GridView>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true"></asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer mleft20 mright20">
                    <div class="form-inline">
                        <div class="form-group">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->


    <script type="text/javascript">

        $(document).ready(function () {
            $("#page_content").hide();
            $("#Loading").show();

            $("[data-toggle='tooltip']").tooltip();

            init_Js_updCargarDatos();

            $("#<%: btnCargarDatostramite.ClientID %> ").click();

        });
        function finalizarCarga() {
            $("#Loading").hide();
            $("#page_content").show();
            return false;
        }

        function showfrmError() {
            $("#frmError").modal("show");
            $("#pnlProcesando").hide();
            $("#pnlShortcuts").show();
            return false;
        }

        function init_Js_updCargarDatos() {
            cargaTabs();
            return false;
        }

        function bt_btnUpDown_collapse_click(obj) {
            var href_collapse = $(obj).attr("href");

            if ($(href_collapse).attr("id") != undefined) {
                if ($(obj).find("i.imoon-chevron-down").length > 0) {
                    $(obj).find("i.imoon-chevron-down").switchClass("imoon-chevron-down", "imoon-chevron-up", 0);
                }
                else {
                    $(obj).find("i.imoon-chevron-up").switchClass("imoon-chevron-up", "imoon-chevron-down", 0);
                }
            }
        }

        function showfrmConfirmarAnulacion1() {
            $("#frmConfirmarAnulacion1").modal("show");
            return false;
        }

        function hidefrmConfirmarAnulacion1() {
            $("#frmConfirmarAnulacion1").modal("hide");
            $("#frmConfirmarAnulacion2").modal("show");

        }

        function hidefrmConfirmarAnulacion2() {
            $("#frmConfirmarAnulacion2").modal("hide");
            return true;

        }
        function ocultarShortcuts() {
            $("#pnlShortcuts").hide();
            $("#pnlProcesando").show();
            return true;
        }

        function showModalConfirmarSolicitud() {

            $("#pnlProcesando").hide();
            $("#frmConfirmarSolicitud2").modal("show");
            return false;
        }

        function hideModalConfirmarSolicitud() {
            $("#frmConfirmarSolicitud2").modal("hide");
            return false;
        }

        function cargaTabs() {
            $("[data-toggle='tooltip']").tooltip();

            var tabselected = parseInt($("#<%: hid_tab_selected.ClientID %>").val());

            $("#tabsVisorTramite").tabs({ active: tabselected });
            colorear(tabselected);

            $("#tabsVisorTramite").tabs({
                activate: function (event, ui) {

                    $("#<%: hid_tab_selected.ClientID %>").val(ui.newTab.index().toString());
                }
            });

            return false;
        }

        function colorear(indice) {

            switch (indice) {
                case 0:
                    $("#<%: spnDatosSolicitud.ClientID %> ").css("background", "#fdd306");
                    $("#<%: spnAnexoTecnico.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spnAnexoNotarial.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabApra.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabBui.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabDocumentos.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabPresentacion.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spantabAvisos.ClientID %> ").css("background", "#EEEEEE");
                    <%--$("#<%: spantabLibroDigital.ClientID %> ").css("background", "#EEEEEE");--%>
                    break;

                case 1:
                    $("#<%: spnDatosSolicitud.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spnAnexoTecnico.ClientID %> ").css("background", "#fdd306");
                    $("#<%: spnAnexoNotarial.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabApra.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabBui.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabDocumentos.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabPresentacion.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spantabAvisos.ClientID %> ").css("background", "#EEEEEE");
                    <%--$("#<%: spantabLibroDigital.ClientID %> ").css("background", "#EEEEEE");--%>
                    break;

                case 2:
                    $("#<%: spnDatosSolicitud.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spnAnexoTecnico.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spnAnexoNotarial.ClientID %> ").css("background", "#fdd306");
                    $("#<%: spntabApra.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabBui.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabDocumentos.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabPresentacion.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spantabAvisos.ClientID %> ").css("background", "#EEEEEE");
                    <%--$("#<%: spantabLibroDigital.ClientID %> ").css("background", "#EEEEEE");--%>
                    break;

                case 3:
                    $("#<%: spnDatosSolicitud.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spnAnexoTecnico.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spnAnexoNotarial.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabApra.ClientID %> ").css("background", "#fdd306");
                    $("#<%: spntabBui.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabDocumentos.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabPresentacion.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spantabAvisos.ClientID %> ").css("background", "#EEEEEE");
                    <%--$("#<%: spantabLibroDigital.ClientID %> ").css("background", "#EEEEEE");--%>
                    break;

                case 4:
                    $("#<%: spnDatosSolicitud.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spnAnexoTecnico.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spnAnexoNotarial.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabApra.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabBui.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabDocumentos.ClientID %> ").css("background", "#fdd306");
                    $("#<%: spntabPresentacion.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spantabAvisos.ClientID %> ").css("background", "#EEEEEE");
                    <%--$("#<%: spantabLibroDigital.ClientID %> ").css("background", "#EEEEEE");--%>
                    break;

                case 5:
                    $("#<%: spnDatosSolicitud.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spnAnexoTecnico.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spnAnexoNotarial.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabApra.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabBui.ClientID %> ").css("background", "#fdd306");
                    $("#<%: spntabDocumentos.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabPresentacion.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spantabAvisos.ClientID %> ").css("background", "#EEEEEE");
                    <%--$("#<%: spantabLibroDigital.ClientID %> ").css("background", "#EEEEEE");--%>
                    break;

                case 6:
                    $("#<%: spnDatosSolicitud.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spnAnexoTecnico.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spnAnexoNotarial.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabApra.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabBui.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabDocumentos.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabPresentacion.ClientID %> ").css("background", "#fdd306");
                    $("#<%: spantabAvisos.ClientID %> ").css("background", "#EEEEEE");
                    <%--$("#<%: spantabLibroDigital.ClientID %> ").css("background", "#EEEEEE");--%>
                    break;

                case 7:
                    $("#<%: spnDatosSolicitud.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spnAnexoTecnico.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spnAnexoNotarial.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabApra.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabBui.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabDocumentos.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabPresentacion.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spantabAvisos.ClientID %> ").css("background", "#fdd306");
                    <%--$("#<%: spantabLibroDigital.ClientID %> ").css("background", "#EEEEEE");--%>
                    break;

                case 8:
                    $("#<%: spnDatosSolicitud.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spnAnexoTecnico.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spnAnexoNotarial.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabApra.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabBui.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabDocumentos.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spntabPresentacion.ClientID %> ").css("background", "#EEEEEE");
                    $("#<%: spantabAvisos.ClientID %> ").css("background", "#EEEEEE");
                    <%--$("#<%: spantabLibroDigital.ClientID %> ").css("background", "#fdd306");--%>

                    break;
            }

            return false;
        }
        function showSolicitudNueva(visible) {
            if (visible) {
                //$("#tabsVisorTramite").find("[aria-controls='tabApra']").show();
                $("#tabsVisorTramite").find("[aria-controls='tabBui']").show();
            } else {
                //$("#tabsVisorTramite").find("[aria-controls='tabApra']").hide();
                $("#tabsVisorTramite").find("[aria-controls='tabBui']").hide();
            }

            return false;
        }
        function showfrmConfirmarTramite() {
            $("#frmConfirmarSolicitud1").modal("show");

            return false;
        }
        function hidefrmConfirmarSolicitud() {
            $("#frmConfirmarSolicitud1").modal("hide");
            ocultarShortcuts();
        }


        function showfrmDocumentosFaltantes() {
            $("#frmDocumentosFaltantes").modal("show");

        }
        function hidefrmDocumentosFaltantes() {
            $("#frmDocumentosFaltantes").modal("hide");
            ocultarShortcuts();
        }

    </script>
</asp:Content>
