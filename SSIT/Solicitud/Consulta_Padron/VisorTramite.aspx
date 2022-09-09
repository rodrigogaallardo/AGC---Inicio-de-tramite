<%@ Page Title="Visualizar Trámite" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VisorTramite.aspx.cs" Inherits="SSIT.Solicitud.Consulta_Padron.VisorTramite" %>

<%@ Register Src="~/Solicitud/Consulta_Padron/Controls/Ubicacion.ascx" TagPrefix="uc" TagName="Ubicacion" %>
<%@ Register Src="~/Solicitud/Consulta_Padron/Controls/Rubros.ascx" TagPrefix="uc" TagName="Rubros" %>
<%@ Register Src="~/Solicitud/Consulta_Padron/Controls/TitularesCP.ascx" TagPrefix="uc" TagName="TitularesHabi" %>
<%@ Register Src="~/Solicitud/Consulta_Padron/Controls/Titulares.ascx" TagPrefix="uc" TagName="TitularesSolic" %>
<%@ Register Src="~/Solicitud/Consulta_Padron/Controls/DatosLocal.ascx" TagPrefix="uc" TagName="DatosLocal" %>
<%@ Register Src="~/Solicitud/Consulta_Padron/Controls/ucDatosSolicitud.ascx" TagPrefix="uc" TagName="ucDatosSolicitud" %>


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
                <asp:HiddenField ID="hid_id_encomienda" runat="server" />

                <div class="row">
                    <div class="col-sm-6">
                        <div class="row">
                            <label class="col-sm-5 text-right">Número de trámite:</label>
                            <asp:Label ID="lblNroEncomienda" CssClass="col-sm-7" runat="server" Font-Bold="true" Font-Size="16px" Style="color: #337ab7">"A Completar"</asp:Label>

                        </div>
                        <div class="row" runat="server" id="divExpediente">
                            <label class="col-sm-5 text-right">Numero de Expediente:</label>
                            <asp:Label ID="lblNroExpediente" CssClass="col-sm-7" runat="server" Font-Bold="true" Font-Size="16px" Style="color: #337ab7">"Debe ingresar el Nº de Expediente"</asp:Label>

                        </div>
                        <div class="row">
                            <label class="col-sm-5 text-right">Tipo de trámite:</label>
                            <asp:Label ID="lblTipoTramite" CssClass="col-sm-7" runat="server" Font-Bold="true"> "A COMPLETAR"</asp:Label>

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

                                <asp:Panel ID="divbtnConfirmarTramite" runat="server" class=" display-inline-block">

                                    <asp:LinkButton ID="btnConfirmarTramite" CssClass="shortcut shortcut-sm" Style="width: 7em; height: 10em" runat="server" OnClientClick="return ocultarShortcuts();" OnClick="btnConfirmarTramite_Click">
                                            
                                                 <span class="bg-info-lt">
                                                    <span class="glyphicon imoon-ok fs48"></span>
                                                </span>
                                            <p>Confirmar</p>
                                    </asp:LinkButton>

                                </asp:Panel>


                                <asp:Panel ID="pnlBandeja" runat="server" class="display-inline-block">

                                    <asp:LinkButton ID="btnBandeja" CssClass="shortcut shortcut-sm" Style="width: 7em; height: 10em" runat="server" PostBackUrl="~/Tramites/Bandeja.aspx">
                                           
                                                
                                                 <span class="bg-primary-lt">
                                                    <span class="glyphicon imoon-inbox fs48"></span>
                                                </span>
                                            <p>Bandeja </p>
                                    </asp:LinkButton>
                                </asp:Panel>

                                <asp:Panel ID="divbtnAnularTramite" runat="server" class=" display-inline-block">

                                    <asp:LinkButton ID="btnAnularTramite" CssClass="shortcut shortcut-sm" Style="width: 7em; height: 10em" runat="server" OnClientClick="return showfrmConfirmarAnulacion();">
                                           
                                                
                                                 <span class="bg-gray-3">
                                                    <span class="glyphicon imoon-close fs48"></span>
                                                </span>
                                            <p >Anular </p>
                                    </asp:LinkButton>
                                </asp:Panel>

                            </div>
                        </div>

                    </div>
                </div>
                <table border="0" style="width: 100%;">
                    <tr>
                        <td style="width: 40%"></td>
                        <td style="width: 60%"></td>
                    </tr>
                </table>
                <br />
                <asp:Panel ID="pnlTramiteIncompleto" runat="server" CssClass="alert alert-success mtop10" Visible="false" Width="100%">
                    <asp:Label ID="lblTextoTramiteIncompleto" runat="server"></asp:Label>
                </asp:Panel>

            </ContentTemplate>
        </asp:UpdatePanel>

        <%--Paneles con datos de la solicitud--%>
        <asp:UpdatePanel ID="updCargarDatos" runat="server" RenderMode="Inline" UpdateMode="Conditional">
            <ContentTemplate>

                <asp:HiddenField ID="hid_mostrar_observaciones" runat="server" />
                <asp:Button ID="btnCargarDatostramite" runat="server" Style="display: none" OnClick="btnCargarDatostramite_Click" />
                <asp:Panel ID="pnlAlertasSolicitud" runat="server" Visible="false" CssClass="alert alert-success"
                    Style="padding-bottom: 5px; padding-top: 5px">
                    <asp:Label ID="lblAlertasSolicitud" runat="server"></asp:Label>
                </asp:Panel>


                <uc:ucDatosSolicitud runat="server" ID="visDatosSolicitud" OnEventActualizarEstadoVisor="visDatosSolicitud_EventActualizarEstadoVisor" />


                <%-- collapsible ubicaciones--%>
                <div id="box_ubicacion" class="accordion-group widget-box" style="background: #ffffff;">

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
                    <div class="accordion-body collapse in" id="collapse_ubicacion" style="margin-top: 20px; margin-left: 20px; margin-right: 20px">
                        <asp:Panel ID="pnlModifUbicacion" runat="server" CssClass="text-right mtop10 mright10" BackColor="#ffffff">
                            <asp:LinkButton ID="btnModificarUbicacion" runat="server" PostBackUrl="~/Ubicacion.aspx"
                                CssClass="btn btn-primary" Width="180px">
                                 <i class="imoon imoon-pencil"></i>
                                <span class="text">Modificar Ubicación</span>
                            </asp:LinkButton>

                        </asp:Panel>
                        <br />
                        <br />
                        <uc:Ubicacion runat="server" ID="visUbicaciones" />
                        <%--Botón de Modificación de Ubicaciones --%>
                    </div>


                </div>

                <%-- Datos del Local--%>
                <div id="box_datoslocal" class="accordion-group widget-box" style="background: #ffffff">

                    <div class="accordion-heading">
                        <a id="A1" data-parent="#collapse-group" href="#collapse_datoslocal"
                            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                            <div class="widget-title">
                                <span class="icon"><i class="imoon imoon-office" style="color: #344882;"></i></span>
                                <h5>
                                    <asp:Label ID="lblTituloDatosLocal" runat="server" Text="Datos del Local"></asp:Label></h5>
                                <span class="btn-right"><i class="imoon imoon-chevron-down" style="color: #344882;"></i></span>
                            </div>
                        </a>
                    </div>

                    <div class="accordion-body collapse" id="collapse_datoslocal">
                        <asp:Panel ID="pnlModifDatosLocal" runat="server" CssClass="text-right mtop10 mright10" BackColor="#ffffff">
                            <asp:LinkButton ID="btnModificarDatosLocal" runat="server" PostBackUrl="~/DatosLocal.aspx"
                                CssClass="btn btn-primary" Width="220px">
                                 <i class="imoon imoon-pencil"></i>
                                <span class="text">Modificar datos del Local</span>
                            </asp:LinkButton>
                        </asp:Panel>
                        <uc:DatosLocal ID="visDatoslocal" runat="server" />

                        <%--Botón de Modificación de Datos del Local  --%>
                    </div>
                </div>


                <%-- Datos del Rubros--%>
                <div id="box_rubros" class="accordion-group widget-box" style="background: #ffffff">

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
                        <asp:Panel ID="pnlModifRubros" runat="server" CssClass="text-right mtop10 mright10" BackColor="#ffffff">
                            <asp:LinkButton ID="btnModificarRubros" runat="server" PostBackUrl="~/Rubros.aspx"
                                CssClass="btn btn-primary">
                                 <i class="imoon imoon-pencil"></i>
                                <span class="text">Modificar Datos del/los Rubros</span>
                            </asp:LinkButton>
                        </asp:Panel>
                        <uc:Rubros runat="server" ID="visRubros" />

                        <%--Botón de Modificación de Rubros --%>
                    </div>
                </div>

                <%-- Datos de los Titulares--%>
                <div id="box_titulares" class="accordion-group widget-box" style="background: #ffffff">

                    <div class="accordion-heading">
                        <a id="A3" data-parent="#collapse-group" href="#collapse_titulares"
                            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                            <div class="widget-title">
                                <span class="icon"><i class="imoon imoon-users" style="color: #344882;"></i></span>
                                <h5>
                                    <asp:Label ID="Label1" runat="server" Text="Titulares"></asp:Label></h5>
                                <span class="btn-right"><i class="imoon imoon-chevron-down" style="color: #344882;"></i></span>
                            </div>
                        </a>
                    </div>

                    <div class="accordion-body collapse" id="collapse_titulares">
                        <%--Botón de Modificación de Titulares --%>
                        <asp:Panel ID="pnlModifTitulares" runat="server" CssClass="text-right mtop10 mright10" BackColor="#ffffff">
                            <asp:LinkButton ID="btnModificarTitulares" runat="server" PostBackUrl="~/Titulares.aspx"
                                CssClass="btn btn-primary">
                                 <i class="imoon imoon-pencil"></i>
                                <span class="text">Modificar Datos del/los titulares</span>
                            </asp:LinkButton>
                        </asp:Panel>
                        <uc:TitularesHabi runat="server" ID="visTitularesHab" />


                    </div>
                </div>

                <%-- Datos de los Titulares--%>
                <div id="box_titulares_solicitud" class="accordion-group widget-box" style="background: #ffffff">

                    <div class="accordion-heading">
                        <a id="A4" data-parent="#collapse-group" href="#collapse_titulares_solicitud"
                            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                            <div class="widget-title">
                                <span class="icon"><i class="imoon imoon-users" style="color: #344882;"></i></span>
                                <h5>
                                    <asp:Label ID="Label2" runat="server" Text="Titulares Solicitud"></asp:Label></h5>
                                <span class="btn-right"><i class="imoon imoon-chevron-down" style="color: #344882;"></i></span>
                            </div>
                        </a>
                    </div>

                    <div class="accordion-body collapse" id="collapse_titulares_solicitud">
                        <%--Botón de Modificación de Titulares --%>
                        <asp:Panel ID="pnlModifTitularesSolicitud" runat="server" CssClass="text-right mtop10 mright10" BackColor="#ffffff">
                            <asp:LinkButton ID="btnModifTitularesSolicitud" runat="server" PostBackUrl="~/Solicitud/ConsultaPadron/EditarTitularesSolicitud/<% IdSolicitud %>"
                                CssClass="btn btn-primary">
                                 <i class="imoon imoon-pencil"></i>
                                <span class="text">Modificar Datos del/los titulares</span>
                            </asp:LinkButton>
                        </asp:Panel>
                        <uc:TitularesSolic runat="server" ID="TitularesSol" />


                    </div>
                </div>


                <%--collapsible observaciones--%>
                <div id="box_observacion" class="accordion-group widget-box" runat="server" style="background-color: #ffffff">

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
                        <div class="mleft10 mright10">

                            <asp:GridView
                                ID="gridObservaciones"
                                runat="server"
                                CssClass="table table-bordered table-striped table-hover with-check"
                                AutoGenerateColumns="false"
                                ItemType="DataTransferObject.ConsultaPadronSolicitudesObservacionesDTO"
                                DataKeyNames="IdConsultaPadronObservacion"
                                OnRowDataBound="gridObservaciones_OnRowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="CreateDate" HeaderText="Fecha" ItemStyle-Width="150px" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                                    <asp:BoundField DataField="userApeNom" HeaderText="Calificador" />

                                    <asp:TemplateField ItemStyle-Width="190px" HeaderText="Observaciones">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="lnkModal" runat="server" CssClass="" data-toggle="modal" data-target="codebehind">
                                            <i class='icon <%#Eval("clase_leido")%>'></i>
                                            <span class="text">Leer Observaci&oacute;n</span>
                                            </asp:LinkButton>

                                            <asp:Panel ID="pnlObservacionModal" runat="server" class="modal fade in" TabIndex="-1" role="dialog" aria-hidden="true">
                                                <div class="modal-dialog">
                                                    <div class="modal-content">
                                                        <div class="modal-header titulo-2">

                                                            <a class="close" data-dismiss="modal">X</a>
                                                            <div style="margin-top: -8px">
                                                                <b style="margin-top: -8px">Causales de no recepci&oacute;n</b>
                                                            </div>
                                                        </div>

                                                        <div class="modal-body" style="max-height: 250px">
                                                            <p>Solicitud: <b><%#Eval("IdConsultaPadron")%></b></p>
                                                            <p>Fecha: <b><%#Eval("CreateDate", "{0:dd/MM/yyyy HH:mm}")%></b></p>
                                                            <p><%#Eval("Observaciones")%></p>
                                                        </div>

                                                        <div class="modal-footer">
                                                            <a href="#" class="btn btn-default" data-dismiss="modal">Cancelar</a>
                                                            <asp:Button ID="btnConfirmarObservacion" runat="server"
                                                                Text="He leído" CssClass="btn btn-primary"
                                                                OnClientClick="cerrarModalObservacion(this);"
                                                                CommandArgument='<%# Eval("IdConsultaPadronObservacion") %>'
                                                                OnCommand="btnConfirmarObservacion_Command" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>
                </div>

                <%-- collapsible documentos--%>
                <div id="box_documento" class="accordion-group widget-box" style="background: #ffffff">

                    <%-- titulo collapsible documentos--%>
                    <div class="accordion-heading">
                        <a id="documento_btnUpDown" data-parent="#collapse-group" href="#collapse_documento"
                            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                            <asp:HiddenField ID="hid_documento_collapse" runat="server" Value="true" />
                            <asp:HiddenField ID="hid_documento_visible" runat="server" Value="false" />

                            <div class="widget-title">
                                <span class="icon"><i class="imoon-file2" style="color: #344882;"></i></span>
                                <h5>
                                    <asp:Label ID="lbl_documento_tituloControl" runat="server" Text="Lista de documentos"></asp:Label></h5>
                                <span class="btn-right"><i class="imoon imoon-chevron-down" style="color: #344882;"></i></span>
                            </div>
                        </a>
                    </div>


                    <%-- contenido del collapsible documentos --%>
                    <div class="accordion-body collapse" id="collapse_documento">
                        <div class=" mleft20 mright20">
                            <asp:HiddenField ID="hddEmptyDocumentos" runat="server" />
                            <%--Botón de Modificación --%>
                            <asp:Panel ID="pnlModifDocumentos" runat="server" CssClass="text-right mtop10 mright10" BackColor="#ffffff">
                                <asp:LinkButton ID="btnModifDocumentos" runat="server" CssClass="btn btn-primary">
                                              <i class="imoon imoon-pencil"></i>
                                            <span class="text">Modificar Documentos</span>

                                </asp:LinkButton>

                            </asp:Panel>
                            <div class="mtop10" id="textDocProf" runat="server">
                                <strong>Documentos Adjuntos</strong>
                            </div>

                            <asp:GridView ID="repeater_certificados"
                                runat="server"
                                AutoGenerateColumns="false"
                                AllowPaging="false"
                                Style="border: none;"
                                GridLines="None"
                                Width="100%"
                                CssClass="table table-bordered mtop5 "
                                CellPadding="3">
                                <HeaderStyle CssClass="grid-header" />
                                <AlternatingRowStyle BackColor="#efefef" />
                                <Columns>
                                    <asp:BoundField DataField="CreateDate" DataFormatString="{0:d}" HeaderText="Fecha" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="90px" />
                                    <asp:BoundField DataField="NombreArchivo" HeaderText="Nombre del archivo" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                                    <asp:TemplateField ItemStyle-Width="190px" HeaderText="Descargar" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkCertificado" runat="server" Target="_blank" Style="padding-right: 10px" CssClass="btnpdf20x20" NavigateUrl='<%#  "~/Download.ashx?Id=" + Eval("IdFile") %>'>
                                                  <span class="icon"><i class="imoon-download fs24 color-blue"></i></span>
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="mtop10">

                                        <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                                        <span class="mleft10">No se encontraron registros.</span>

                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>

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
                    <h4 class="modal-title">Error</h4>
                </div>
                <div class="modal-body">
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
                    <h4 class="modal-title">Anular tr&aacute;mite</h4>
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
                    <h4 class="modal-title">Confirmaci&oacute;n</h4>
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

                                <asp:LinkButton ID="btnEliminarDocumentoAdjunto" runat="server" CssClass="btn btn-primary" OnClientClick="$('#pnlBotonesConfirmarEliminarDocAdjunto').hide();">
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
        var cantGrillaH = 0;
        var cantGrillaS = 0;
        $(document).ready(function () {
            $("#page_content").hide();
            $("#Loading").show();
            $("#<%: btnCargarDatostramite.ClientID %> ").click();

        });

        function finalizarCarga() {
            $("#Loading").hide();
            $("#page_content").slideDown("slow");

            var mostrar_observacion = $("#<%: hid_mostrar_observaciones.ClientID %>").val();

            if (mostrar_observacion == "True") {
                $("#box_observacion").show();
            } else {
                $("#box_observacion").hide();
            }
            validarFirmantesS();
            validarFirmantesH();
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

        function showfrmConfirmarAnulacion() {

            $("#frmConfirmarAnulacion").modal("show");
            return false;

        }

        function hidefrmConfirmarAnulacion() {

            $("#frmConfirmarAnulacion").modal("hide");
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
            $("#page_content").hide();
            $("#Loading").show();
        }
        function validarFirmantesS() {



            cantGrillaS = $("[id*='panelGrillaS']").length;


            if (cantGrillaS <= 1) {
                $("#panelGrillaS").hide();

            }

        }
        function validarFirmantesH() {

            cantGrillaH = $("[id*='panelGrillaH']").length;
            if (cantGrillaH <= 1) {
                $("#panelGrillaH").hide();

            }
        }
    </script>
</asp:Content>
