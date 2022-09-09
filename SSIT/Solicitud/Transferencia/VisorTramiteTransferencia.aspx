<%@ Page Title="Visualizar Trámite" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VisorTramiteTransferencia.aspx.cs" Inherits="SSIT.VisorTramiteTransferencia" %>

<%@ Register Src="~/Solicitud/Consulta_Padron/Controls/Ubicacion.ascx" TagPrefix="uc" TagName="Ubicacion" %>
<%@ Register Src="~/Solicitud/Consulta_Padron/Controls/Rubros.ascx" TagPrefix="uc" TagName="Rubros" %>
<%@ Register Src="~/Solicitud/Consulta_Padron/Controls/DatosLocal.ascx" TagPrefix="uc" TagName="DatosLocal" %>
<%@ Register Src="~/Solicitud/Transferencia/Controls/Titulares.ascx" TagPrefix="uc" TagName="TitularesTRANS" %>
<%@ Register Src="~/Solicitud/Consulta_Padron/Controls/Titulares.ascx" TagPrefix="uc1" TagName="TitularesHAB" %>
<%@ Register Src="~/Solicitud/Consulta_Padron/Controls/TitularesCP.ascx" TagPrefix="uc2" TagName="TitularesCP" %>
<%@ Register Src="~/Solicitud/Transferencia/Controls/Pagos.ascx" TagPrefix="uc" TagName="Pagos" %>
<%@ Register Src="~/Solicitud/Controls/ucCargaDocumentos.ascx" TagPrefix="uc" TagName="CargaDocumentos" %>


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
                    <div class="col-sm-6">
                        <div class="row">
                            <label class="col-sm-5 text-right">Número de trámite:</label>
                            <asp:Label ID="lblNroSolicitud" CssClass="col-sm-7" runat="server" Font-Bold="true" Font-Size="16px" Style="color: #337ab7">"A Completar"</asp:Label>

                        </div>
                        <div class="row">
                            <label class="col-sm-5 text-right">Nº de Consulta al padrón:</label>
                            <asp:Label ID="lblNroEncomienda" CssClass="col-sm-7" runat="server" Font-Bold="true" Font-Size="16px" Style="color: #337ab7"> "A COMPLETAR"</asp:Label>

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
                    </div>

                    <div class="col-sm-6">

                        <div id="pnlProcesando" class="text-center" style="display: none">
                            <img src="<%: ResolveUrl("~/Content/img/app/Loading64x64.gif") %>" alt="" />
                            <span class="mleft10">Procesando...</span>
                        </div>

                        <div id="pnlShortcuts" class="view view-shortcuts view-id-shortcuts box-panel" style="display: inline-block; width: auto; float: right">
                            <div class="views-row col-sm-12 text-right" style="display: flex">



                                <asp:Panel ID="divbtnConfirmarTramite" runat="server" class=" display-inline-block" Visible="false">

                                    <asp:LinkButton ID="btnConfirmarTramite" CssClass="shortcut shortcut-sm" Style="width: 7em; height: 10em" runat="server" OnClientClick="return ocultarShortcuts();" OnClick="btnConfirmarTramite_Click">
                                            <span class="bg-info-lt">
                                                <span class="glyphicon imoon-ok fs48"></span>
                                            </span>
                                            <p>Confirmar</p>
                                    </asp:LinkButton>

                                </asp:Panel>


                                <asp:Panel ID="divbtnImprimirSolicitud" runat="server" class=" display-inline-block" Visible="false">

                                    <asp:HyperLink ID="btnImprimirSolicitud" CssClass="shortcut shortcut-sm" Style="width: 7em; height: 10em" title="Descargar la solicitud" runat="server" Target="_blank">
                                        <span class="bg-success-lt">
                                            <span class="glyphicon imoon-download fs48"></span>
                                        </span>
                                        <p >Descargar Solicitud</p>
                                    </asp:HyperLink>
                                </asp:Panel>

                                <asp:Panel ID="pnlBandeja" runat="server" class=" display-inline-block">

                                    <asp:LinkButton ID="btnBandeja" CssClass="shortcut shortcut-sm" Style="width: 7em; height: 10em" runat="server" PostBackUrl="~/Solicitud/Bandeja">
                                            <span class="bg-primary-lt">
                                                <span class="glyphicon imoon-inbox fs48"></span>
                                            </span>
                                            <p>Bandeja</p>
                                    </asp:LinkButton>

                                </asp:Panel>

                                <asp:Panel ID="divbtnAnularTramite" runat="server" class=" display-inline-block" Visible="false">

                                    <asp:LinkButton ID="btnAnularTramite" CssClass="shortcut shortcut-sm" Style="width: 7em; height: 10em" runat="server" OnClientClick="return showfrmConfirmarAnulacion();">
                                            <span class="bg-gray-3">
                                                <span class="glyphicon imoon-close fs48 "></span>
                                            </span>
                                            <p>Anular</p>
                                    </asp:LinkButton>
                                </asp:Panel>
                            </div>
                        </div>

                    </div>
                </div>

                <asp:Panel ID="pnlTramiteIncompleto" runat="server" CssClass="alert alert-danger mtop10" Visible="false" Width="100%">
                    <asp:Label ID="lblTextoTramiteIncompleto" runat="server"></asp:Label>
                </asp:Panel>

            </ContentTemplate>
        </asp:UpdatePanel>

        <%--Paneles con datos de la solicitud--%>
        <asp:UpdatePanel ID="updCargarDatos" runat="server" RenderMode="Inline" UpdateMode="Conditional">
            <ContentTemplate>

                <asp:Button ID="btnCargarDatostramite" runat="server" Style="display: none" OnClick="btnCargarDatostramite_Click" />

                <asp:HiddenField ID="hid_id_encomienda" runat="server" />
                <asp:HiddenField ID="hid_id_solicitud" runat="server" />
                <asp:HiddenField ID="hid_id_estado" runat="server" />
                <asp:HiddenField ID="hid_mostrar_escribano" runat="server" />
                <asp:HiddenField ID="hid_mostrar_observacion" runat="server" />
                <asp:HiddenField ID="hid_mostrar_certificado" runat="server" />
                <asp:HiddenField ID="hid_mostrar_datos" runat="server" />

                <asp:Panel ID="pnlAlertasSolicitud" runat="server" Visible="false" CssClass="alert alert-danger"
                    Style="padding-bottom: 5px; padding-top: 5px">
                    <asp:Label ID="lblAlertasSolicitud" runat="server"></asp:Label>
                </asp:Panel>

                <%-- collapsible Pagos--%>
                <asp:UpdatePanel ID="updBoxPagos" runat="server">
                    <ContentTemplate>

                        <asp:Panel ID="pnlPagos" runat="server" CssClass="accordion-group widget-box" Style="background-color: #ffffff">

                            <%-- titulo collapsible pagos--%>
                            <div class="accordion-heading">
                                <a id="A3" data-parent="#collapse-group" href="#collapse_pagos"
                                    data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                                    <div class="widget-title">
                                        <span class="icon"><i class="icon imoon-dollar" style="color: #344882;"></i></span>
                                        <h5>
                                            <asp:Label ID="Label1" runat="server" Text="Pagos"></asp:Label></h5>
                                        <span class="btn-right"><i class="imoon imoon-chevron-up" style="color: #344882;"></i></span>
                                    </div>
                                </a>
                            </div>

                            <%-- contenido del collapsible pagos --%>
                            <div class="accordion-body collapse in" id="collapse_pagos">
                                <div class="widget-content">
                                    <strong>Boletas generadas</strong>
                                    <uc:Pagos runat="server" ID="Pagos" />

                                </div>
                            </div>
                        </asp:Panel>

                    </ContentTemplate>
                </asp:UpdatePanel>
                <%-- collapsible ubicaciones--%>
                <div id="box_ubicacion" class="accordion-group widget-box" style="background-color: #ffffff">

                    <%-- titulo collapsible ubicaciones--%>
                    <div class="accordion-heading">
                        <a id="ubicacion_btnUpDown" data-parent="#collapse-group" href="#collapse_ubicacion"
                            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                            <div class="widget-title">
                                <span class="icon"><i class="imoon-map-marker imoon-map-marker" style="color: #344882;"></i></span>
                                <h5>
                                    <asp:Label ID="lbl_ubicacion_tituloControl" runat="server" Text="Ubicaci&oacute;n"></asp:Label></h5>
                                <span class="btn-right"><i class="imoon imoon-chevron-up" style="color: #344882;"></i></span>
                            </div>
                        </a>
                    </div>

                    <%-- contenido del collapsible ubicaciones --%>
                    <div class="accordion-body collapse in" id="collapse_ubicacion">
                        <div class="mtop20 mright20 mleft20">
                            <uc:Ubicacion runat="server" ID="visUbicaciones" />
                            <br />
                        </div>
                    </div>

                </div>

                <%-- Datos del Local--%>
                <div id="Div1" class="accordion-group widget-box" style="background-color: #ffffff">

                    <%-- titulo collapsible ubicaciones--%>
                    <div class="accordion-heading">
                        <a id="A1" data-parent="#collapse-group" href="#collapse_datoslocal"
                            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                            <div class="widget-title">
                                <span class="icon"><i class="imoon-map-marker imoon-map-marker" style="color: #344882;"></i></span>
                                <h5>
                                    <asp:Label ID="Label2" runat="server" Text="Datos del local"></asp:Label></h5>
                                <span class="btn-right"><i class="imoon imoon-chevron-down" style="color: #344882;"></i></span>
                            </div>
                        </a>
                    </div>

                    <%-- contenido del collapsible ubicaciones --%>
                    <div class="accordion-body collapse" id="collapse_datoslocal">
                        <uc:DatosLocal runat="server" ID="DatosLocal" />
                    </div>

                </div>

                <%-- Datos del Rubros--%>
                <div id="box_rubros" class="accordion-group widget-box" style="background-color: #ffffff">

                    <div class="accordion-heading">
                        <a id="A2" data-parent="#collapse-group" href="#collapse_rubros"
                            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                            <div class="widget-title">
                                <span class="icon"><i class="imoon imoon-hammer" style="color: #344882;"></i></span>
                                <h5>
                                    <asp:Label ID="lblTituloRubros" runat="server" Text="Rubros"></asp:Label></h5>
                                <span class="btn-right"><i class="imoon imoon-chevron-down" style="color: #344882;"></i></span>
                            </div>
                        </a>
                    </div>

                    <div class="accordion-body collapse" id="collapse_rubros">
                        <uc:Rubros runat="server" ID="visRubros" />
                    </div>
                </div>

                <%--Titulares de la habilitacion --%>
                <div id="Div4" class="accordion-group widget-box" style="background-color: #ffffff">

                    <div class="accordion-heading">
                        <a id="A6" data-parent="#collapse-group" href="#collapse_titulares_habilitacion"
                            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                            <div class="widget-title">
                                <span class="icon"><i class="imoon imoon-user" style="color: #344882;"></i></span>
                                <h5>
                                    <asp:Label ID="Label5" runat="server" Text="Titulares de la Habilitación"></asp:Label></h5>
                                <span class="btn-right"><i class="imoon imoon-chevron-down" style="color: #344882;"></i></span>
                            </div>
                        </a>
                    </div>

                    <div class="accordion-body collapse" id="collapse_titulares_habilitacion">
                        <uc2:TitularesCP runat="server" ID="TitularesHabilitacion" />


                    </div>
                </div>

                <%--Titulares de la solicitud --%>
                <div id="Div3" class="accordion-group widget-box" style="background-color: #ffffff">

                    <div class="accordion-heading">
                        <a id="A5" data-parent="#collapse-group" href="#collapse_titulares_solicitud"
                            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                            <div class="widget-title">
                                <span class="icon"><i class="imoon imoon-user" style="color: #344882;"></i></span>
                                <h5>
                                    <asp:Label ID="Label4" runat="server" Text="Titulares de la Solicitud"></asp:Label></h5>
                                <span class="btn-right"><i class="imoon imoon-chevron-down" style="color: #344882;"></i></span>
                            </div>
                        </a>
                    </div>

                    <div class="accordion-body collapse" id="collapse_titulares_solicitud">
                        <uc1:TitularesHAB runat="server" ID="TitularesSolicitud" />
                    </div>
                </div>

                <%-- Titulares de la transferencia--%>
                <div id="Div2" class="accordion-group widget-box" style="background-color: #ffffff">

                    <div class="accordion-heading">
                        <a id="A4" data-parent="#collapse-group" href="#collapse_titulares"
                            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                            <div class="widget-title">
                                <span class="icon"><i class="imoon imoon-user" style="color: #344882;"></i></span>
                                <h5>
                                    <asp:Label ID="Label3" runat="server" Text="Titulares de la Transferencia"></asp:Label></h5>
                                <span class="btn-right"><i class="imoon imoon-chevron-down" style="color: #344882;"></i></span>
                            </div>
                        </a>
                    </div>

                    <div class="accordion-body collapse" id="collapse_titulares">
                        <uc:TitularesTRANS runat="server" ID="Titulares" />

                        <%--Botón de Modificación de Titulares --%>
                        <asp:Panel ID="pnlModifTitulares" runat="server" CssClass="bar-modif" BackColor="#ffffff" Visible="false">
                            <asp:LinkButton ID="btnModificarTitulares" runat="server" PostBackUrl="~/Titulares.aspx"
                                CssClass="btn btn-primary">
                                 <i class="imoon imoon-pencil"></i>
                                <span class="text">Modificar Datos del/los titulares</span>
                            </asp:LinkButton>
                        </asp:Panel>
                    </div>
                </div>

                <%-- collapsible certificado--%>
                <div id="box_certificado" class="accordion-group widget-box">

                    <%-- titulo collapsible rubro--%>
                    <div class="accordion-heading">
                        <a id="certificado_btnUpDown" data-parent="#collapse-group" href="#collapse_certificado"
                            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                            <asp:HiddenField ID="hid_certificado_collapse" runat="server" Value="true" />
                            <asp:HiddenField ID="hid_certificado_visible" runat="server" Value="false" />

                            <div class="widget-title">
                                <span class="icon"><i class="imoon imoon-list-alt" style="color: #344882;"></i></span>
                                <h5>
                                    <asp:Label ID="lbl_certificado_tituloControl" runat="server" Text="Certificado Pro-Teatro"></asp:Label></h5>
                                <span class="btn-right"><i class="imoon imoon-chevron-down"></i></span>
                            </div>
                        </a>
                    </div>

                    <%-- contenido del collapsible rubro --%>
                    <div class="accordion-body collapse" id="collapse_certificado">
                        <div class="widget-content">
                            <asp:UpdatePanel ID="updPnlCertificados" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlProTeatro" runat="server" Style="margin-top: 5px;">

                                        <asp:UpdatePanel ID="uplProTeatro" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Panel ID="pnlDatosProTeatro" runat="server" Style="display: block">



                                                    <%--Grilla de certificados--%>
                                                    <asp:UpdatePanel ID="updPnlProTeatro" runat="server">
                                                        <ContentTemplate>

                                                            <div style="padding-left: 20px; width: 890px;">
                                                                <asp:GridView ID="grdProTeatro"
                                                                    runat="server"
                                                                    AutoGenerateColumns="false"
                                                                    DataKeyNames="id_docadjunto, id_encomienda"
                                                                    AllowPaging="false" Style="border: none; margin-top: 10px"
                                                                    GridLines="None" Width="890px"
                                                                    CellPadding="3">
                                                                    <HeaderStyle CssClass="grid-header" />
                                                                    <AlternatingRowStyle BackColor="#efefef" />
                                                                    <Columns>
                                                                        <asp:TemplateField ItemStyle-Height="24px" HeaderText="Descargar">
                                                                            <ItemTemplate>
                                                                                <asp:HyperLink ID="lnkDescargarPlano" runat="server" Target="_blank" Style="padding-right: 10px" NavigateUrl='<%#"~/Download.ashx?Id=" + Eval("Id") %>'>
                                                                                <i class="icon-download-alt"></i>
                                                                                <span class="text"><%# Eval("nombre_archivo")%></span>
                                                                                </asp:HyperLink>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--<asp:BoundField DataField="nombre_tipodocsis" HeaderText="Detalle" />--%>

                                                                        <asp:BoundField DataField="CreateDate" HeaderText="Subido el " ItemStyle-Width="80px"
                                                                            HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:dd/MM/yyyy}" />


                                                                    </Columns>
                                                                    <EmptyDataTemplate>
                                                                        <div class="titulo-4">
                                                                            No hay certificados aún...
                                                                        </div>
                                                                    </EmptyDataTemplate>
                                                                </asp:GridView>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>


                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>


                </div>

                <%--collapsible observaciones--%>
                <div id="box_observacion" class="accordion-group widget-box">

                    <%-- titulo collapsible observaciones--%>
                    <div class="accordion-heading">
                        <a id="observacion_btnUpDown" data-parent="#collapse-group" href="#collapse_observacion"
                            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                            <asp:HiddenField ID="hid_observacion_collapse" runat="server" Value="true" />
                            <asp:HiddenField ID="hid_observacion_visible" runat="server" Value="false" />

                            <div class="widget-title">
                                <span class="icon"><i class="imoon imoon-list-alt" style="color: #344882;"></i></span>
                                <h5>
                                    <asp:Label ID="lbl_observacion_tituloControl" runat="server" Text="Observaciones"></asp:Label></h5>
                                <span class="btn-right"><i class="imoon imoon-chevron-down"></i></span>
                            </div>
                        </a>
                    </div>

                    <%-- contenido del collapsible observaciones --%>
                    <div class="accordion-body collapse" id="collapse_observacion">
                        <div class="widget-content">
                            <asp:UpdatePanel ID="updPnlObservaciones" runat="server" RenderMode="Inline">
                                <ContentTemplate>

                                    <asp:GridView
                                        ID="gridObservaciones"
                                        runat="server"
                                        CssClass="table table-bordered table-striped table-hover with-check"
                                        ItemType="DataTransferObject.TransferenciasSolicitudesObservacionesDTO"
                                        AutoGenerateColumns="false"
                                        DataKeyNames="Id"
                                        OnRowDataBound="gridObservaciones_OnRowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="CreateDate" HeaderText="Fecha" ItemStyle-Width="150px" DataFormatString="{0:dd/MM/yyyy HH:mm}" />

                                            <asp:TemplateField HeaderText="Calificador">
                                                <ItemTemplate>
                                                    <%# Item.UsuarioSGI == null ? Item.Usuario.Apellido + ", " + Item.Usuario.Nombre : Item.UsuarioSGI.Apellido + ", " + Item.UsuarioSGI.Nombre %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-Width="190px" HeaderText="Leer Observaci&oacute;n" ItemStyle-CssClass="text-center link-local" HeaderStyle-CssClass="text-center">
                                                <ItemTemplate>

                                                    <asp:LinkButton ID="lnkModal" runat="server" CssClass="" data-toggle="modal" data-target="codebehind">
                                                    <span class="icon"><i class="imoon-eye color-blue fs24"></i></span>
                                                    </asp:LinkButton>

                                                    <asp:Panel ID="pnlObservacionModal" runat="server" class="modal fade in" TabIndex="-1" role="dialog" aria-hidden="true" HorizontalAlign="Left">
                                                        <div class="modal-dialog">
                                                            <div class="modal-content">
                                                                <div class="modal-header">
                                                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                                                    <h4 class="modal-title" style="margin-top: -8px">Causales de no recepci&oacute;n</h4>
                                                                </div>

                                                                <div class="modal-body">
                                                                    <p>Solicitud: <b><%#Eval("IdSolicitud")%></b></p>
                                                                    <p>Fecha: <b><%#Eval("CreateDate", "{0:dd/MM/yyyy HH:mm}")%></b></p>
                                                                    <p><%#Eval("Observaciones")%></p>
                                                                </div>

                                                                <div class="modal-footer">
                                                                    <a href="#" class="btn btn-default" data-dismiss="modal">Cerrar</a>
                                                                    <asp:Button ID="btnConfirmarObservacion" runat="server"
                                                                        Text="He leído" CssClass="btn btn-primary"
                                                                        OnClientClick="cerrarModalObservacion(this);"
                                                                        CommandArgument='<%# Eval("Id") %>'
                                                                        OnCommand="btnConfirmarObservacion_Command" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Notificado" ItemStyle-Width="20px" ItemStyle-CssClass="text-center link-local" HeaderStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <span class="icon">
                                                        <i class='<%# Eval("Leido").ToString() == "True" ? "imoon-ok fs24" : "" %>'></i>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>

                <%-- collapsible documentos--%>
                <div id="box_documentos" class="accordion-group widget-box" style="background-color: #ffffff">

                    <%-- titulo collapsible documentos--%>
                    <div class="accordion-heading">
                        <a id="documento_btnUpDown" data-parent="#collapse-group" href="#collapse_documento"
                            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                            <asp:HiddenField ID="hid_documento_collapse" runat="server" Value="true" />
                            <asp:HiddenField ID="hid_documento_visible" runat="server" Value="false" />

                            <div class="widget-title">
                                <span class="icon"><i class="imoon imoon-file2" style="color: #344882"></i></span>
                                <h5>
                                    <asp:Label ID="lbl_documento_tituloControl" runat="server" Text="Documentos"></asp:Label></h5>

                                <span class="btn-right"><i class="imoon imoon-chevron-down" style="color: #344882;"></i></span>
                            </div>
                        </a>
                    </div>

                    <%-- contenido del collapsible documentos --%>
                    <div class="accordion-body collapse" id="collapse_documento">
                        <div style="margin: 10px">
                            <div class="pull-right">
                                <asp:LinkButton ID="btnMostrarAgregadoDocumentos" runat="server" CssClass="btn btn-primary" OnClientClick="return DatosDocumentoAgregarToggle();" Visible="false">
                                                <i class="imoon-white imoon-chevron-down" ></i>
                                                <span class="text">Agregar Documentos</span>
                                </asp:LinkButton>
                            </div>

                            <div class="clearfix"></div>
                            <asp:UpdatePanel ID="updpnlDocumentosAdjuntos" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div style="padding: 0px 10px 10px 10px; width: auto">
                                        <asp:Panel ID="pnlDocumentosGeneradosAgencia" runat="server">
                                            <asp:Repeater ID="repeater_docsAgencia" runat="server">
                                                <HeaderTemplate>
                                                    <strong>Lista de documentos generados por la Agencia</strong>
                                                    <table border="0" class="table table-bordered table-hover" style="width: 100%">
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:HyperLink ID="lnkDocAdjunto" runat="server"
                                                                Target="_blank" Style="padding-right: 10px"
                                                                CssClass="btnpdf20x20" Text='<%# ( Eval("tdocreq_detalle").ToString().Length > 0 ?Eval("tdocreq_detalle"): Eval("nombre_tdocreq")) %>'
                                                                NavigateUrl='<%# "~/Reportes/ImprimirDocumentoAdjunto.aspx?id=" + Eval("id_docadjunto")%>'>
                                                            </asp:HyperLink>
                                                        </td>
                                                        <td style="width: 150px;">
                                                            <%#  ((DateTime) Eval("CreateDate")).ToString("dd/MM/yyyy HH:mm") %>
                                                        </td>

                                                    </tr>


                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>

                                            </asp:Repeater>

                                        </asp:Panel>

                                        <asp:Panel ID="pnlDocumentosAdjuntos" runat="server">

                                            <div class="mtop10" id="textDocUd" runat="server">
                                                <strong>Documentos.</strong>
                                            </div>
                                            <asp:GridView ID="grdPlanos"
                                                runat="server"
                                                AutoGenerateColumns="false"
                                                DataKeyNames="Id, IdSolicitud"
                                                AllowPaging="false" Style="border: none;" CssClass="table table-bordered mtop5"
                                                GridLines="None" Width="100%">
                                                <HeaderStyle CssClass="grid-header" />
                                                <AlternatingRowStyle BackColor="#efefef" />
                                                <Columns>
                                                    <asp:BoundField DataField="CreateDate" DataFormatString="{0:d}" HeaderText="Subido el" HeaderStyle-CssClass="text-center" ItemStyle-Width="90px" />
                                                    <asp:BoundField DataField="TipoDocumentoRequerido.nombre_tdocreq" HeaderText="Tipo de Documento" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="400px" />

                                                    <asp:BoundField DataField="NombreArchivo" HeaderText="Nombre del archivo" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                                                    <%--<asp:TemplateField ItemStyle-Height="24px" HeaderText="Descargar">
                                                <ItemTemplate>
                                                    <a href='<%# ResolveClientUrl("~/Download.ashx?Id=") + Eval("IdFile") %>' ><%# Eval("NombreArchivo") %></a>                                                                                                           
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                                    <asp:TemplateField ItemStyle-CssClass="text-center" HeaderText="Acciones" HeaderStyle-CssClass="text-center" ItemStyle-Width="120px">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkImprimirAgregados" runat="server" NavigateUrl='<%# string.Format("~/Download.ashx?Id=") + Eval("IdFile") %>' title="Descargar" Target="_blank">
                             <span class="icon"><i class="imoon-download fs24 color-blue"></i></span>
                                                            </asp:HyperLink>
                                                            <asp:LinkButton ID="lnkEliminar" runat="server" title="Eliminar"
                                                                data-id-docadjunto='<%# Eval("Id") %>'
                                                                OnClientClick="showfrmEliminarDocumentoAdjunto(this)"
                                                                Visible='<%# Eval("Id").ToString() !="0" %>'>
                                                <i class="imoon-trash fs24"></i></a>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <div class="mtop10">
                                                        <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                                                        No hay documentos aún...
                                                    </div>
                                                </EmptyDataTemplate>
                                            </asp:GridView>

                                            <asp:HiddenField ID="hid_id_docadjunto" runat="server" />


                                        </asp:Panel>

                                        <asp:Panel ID="pnlDocumentosAdjuntos_NoData" runat="server" Visible="false" CssClass="mbottom10 pad10 table-bordered">

                                            <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                                            <span class="mleft10">No se encontraron documentos.</span>

                                        </asp:Panel>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>

                </div>
                <%-- Panel para agregar documentos adjuntos--%>
                <asp:UpdatePanel ID="updpnlAgregarDocumentos" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlAgregarDocumentos" runat="server">
                            <asp:Panel ID="pnlDatosDocumento" runat="server" Style="display: none">
                                <div id="Div5" class="accordion-group widget-box" style="background-color: #ffffff">
                                    <div class="accordion-heading">
                                        <a id="A7" data-parent="#collapse-group" href="#collapse_documentosAdicionales"
                                            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                                            <div class="widget-title">
                                                <span class="icon"><i class="imoon imoon-file2" style="color: #344882"></i></span>
                                                <h5>
                                                    <asp:Label ID="Label6" runat="server" Text="Carga de documentos"></asp:Label></h5>
                                                <span class="btn-right"><i class="imoon imoon-chevron-up" style="color: #344882"></i></span>
                                            </div>
                                        </a>
                                    </div>
                                    <div class="accordion-body collapse in" id="collapse_documentosAdicionales">
                                        <uc:CargaDocumentos runat="server" ID="CargaDocumentos" OnErrorCargaDocumento="OnErrorCargaDocumentoClick" OnSubirDocumentoClick="CargaDocumentos_SubirDocumentoClick" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <%--modal de Errores--%>
    <div id="frmError" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                    <table>
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

                </div>
                <div class="modal-footer mleft20 mright20">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->

    <%--Modal Confirmar Anulación--%>
    <div id="frmConfirmarAnulacion" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
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
                            <td style="vertical-align: middle">
                                <label class="mleft10">¿ Est&aacute; seguro de anular el tr&aacute;mite ?</label>
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
                                    <asp:Button ID="btnAnular_Si" runat="server" CssClass="btn btn-primary" Text="Sí" OnClick="btnAnularTramite_Click" />
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

    <%--Modal Eliminar Documentos Adjuntos--%>
    <div id="frmEliminarDocumentoAdjunto" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top: -8px">Confirmaci&oacute;n</h4>
                </div>
                <div class="modal-body">
                    <div class="form-inline">

                        <div class="form-group">
                            <table>
                                <tr>
                                    <td>
                                        <i class="imoon imoon-remove-circle fs64 color-blue"></i>
                                    </td>
                                    <td>
                                        <label class="mleft10">¿Est&aacute; seguro que desea eliminar el documento adjunto?</label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer mleft20 mright20">
                    <asp:UpdatePanel ID="updbtnConfirmarEliminarDocAdjunto" runat="server">
                        <ContentTemplate>

                            <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="updbtnConfirmarEliminarDocAdjunto" DynamicLayout="true" DisplayAfter="200" style="display: inline-block">
                                <ProgressTemplate>
                                    <span class="mleft10">Eliminando...</span>
                                    <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>

                            <div id="pnlBotonesConfirmarEliminarDocAdjunto">

                                <asp:LinkButton ID="btnEliminarDocumentoAdjunto" OnClick="btnEliminarDocumentoAdjunto_Click" runat="server" CssClass="btn btn-primary" OnClientClick="$('#pnlBotonesConfirmarEliminarDocAdjunto').hide();">
                                    <span class="text">S&iacute;</span>
                                </asp:LinkButton>
                                <button type="button" class="btn btn-default" data-dismiss="modal">No</button>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->


    <script type="text/javascript">
        $(document).ready(function () {
            $("#page_content").hide();
            $("#Loading").show();
            $("#<%: btnCargarDatostramite.ClientID %> ").click();
        });

        function finalizarCarga() {
            $("#Loading").hide();
            $("#page_content").slideDown("slow");

            var mostrar_escribano = $("#<%: hid_mostrar_escribano.ClientID %> ").val();

            if (mostrar_escribano == "true") {
                $("#box_escribano").show();
            } else {
                $("#box_escribano").hide();
            }

            var mostrar_observacion = $("#<%: hid_mostrar_observacion.ClientID %> ").val();

            if (mostrar_observacion == "true") {
                $("#box_observacion").show();
            } else {
                $("#box_observacion").hide();
            }

            var mostrar_certificado = $("#<%: hid_mostrar_certificado.ClientID %> ").val();
            if (mostrar_certificado == "True") {
                $("#box_certificado").show();
            } else {
                $("#box_certificado").hide();
            }

            return false;
        }

        function showfrmError() {
            $("#frmError").modal("show");
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

        function showDetalleDocumentoCD() {
            $("#tr_detalle").show();
            return false;
        }

        function hideDetalleDocumentoCD() {
            $("#tr_detalle").hide();
            return false;
        }
        function noExisteFotoParcela(objimg) {
            $(objimg).attr("src", "<%: ResolveUrl("~/Content/img/app/ImageNotFound.png") %>");
            return true;
        }

        function showfrmImprimirCAA() {
            $("#frmImprimirCAA").modal("show");
            return false;
        }

        function hidefrmImprimirCAA() {
            $("#frmImprimirCAA").modal("hide");
            return false;
        }

        function ocultarShortcuts() {
            $("#pnlShortcuts").hide();
            $("#pnlProcesando").show();
            return true;
        }

        function showfrmConfirmarAnulacion() {
            $("#frmConfirmarAnulacion").modal("show");
            return false;
        }

        function hidefrmConfirmarAnulacion() {
            $("#frmConfirmarAnulacion").modal("hide");
            return false;
        }

        function DatosDocumentoAgregarToggle() {
            if ($("#<%: pnlDatosDocumento.ClientID %>").css("display") == "none") {
                $("#<%: pnlDatosDocumento.ClientID %>").show("slow");
                $("#<%: btnMostrarAgregadoDocumentos.ClientID %> i").removeClass('imoon-chevron-down');
                $("#<%: btnMostrarAgregadoDocumentos.ClientID %> i").addClass('imoon-chevron-up');
            }
            else {
                $("#<%: pnlDatosDocumento.ClientID %>").hide("slow");
                $("#<%: btnMostrarAgregadoDocumentos.ClientID %> i").removeClass('imoon-chevron-up');
                $("#<%: btnMostrarAgregadoDocumentos.ClientID %> i").addClass('imoon-chevron-down');

            }
            return false;
        }

        function showfrmEliminarDocumentoAdjunto(obj) {

            var id_docadjunto = $(obj).attr("data-id-docadjunto");
            $("#<%: hid_id_docadjunto.ClientID %>").val(id_docadjunto);

            $('#pnlBotonesConfirmarEliminarDocAdjunto').show();
            $("#frmEliminarDocumentoAdjunto").modal("show");

            return false;
        }
        function hidefrmEliminarDocumentoAdjunto() {
            $("#frmEliminarDocumentoAdjunto").modal("hide");
            return false;
        }
        function ocultarShortcuts() {

            $("#pnlShortcuts").hide();
            $("#pnlProcesando").show();
            return true;
        }

        function cerrarModalObservacion(btn_confirmar) {
            var buscar_boton = $(btn_confirmar)[0].parentElement.parentElement.parentElement.parentElement;
            $(buscar_boton).modal('hide');
        }

        function confirmar_eliminar() {
            return confirm('¿Esta seguro que desea eliminar este Registro?');
        }

        function collapseDocumento() {
            //bt_btnUpDown_collapse_click
            $("#collapse_documento").removeClass("out");
            $("#collapse_documento").addClass("in");
        }

    </script>
</asp:Content>
