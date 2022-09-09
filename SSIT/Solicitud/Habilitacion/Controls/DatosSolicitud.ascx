<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatosSolicitud.ascx.cs" Inherits="SSIT.Solicitud.Habilitacion.Controls.DatosSolicitud" %>

<%@ Register Src="~/Solicitud/Habilitacion/Controls/Ubicacion.ascx" TagPrefix="uc" TagName="Ubicacion" %>
<%@ Register Src="~/Solicitud/Habilitacion/Controls/Titulares.ascx" TagPrefix="uc" TagName="Titulares" %>
<%: Scripts.Render("~/bundles/autoNumeric") %>
<%-- collapsible titulares--%>
<div id="box_titulares" class="accordion-group widget-box" style="background-color: #ffffff">

    <%-- titulo collapsible ubicaciones--%>
    <div class="accordion-heading">
        <a id="titulares_btnUpDown" data-parent="#collapse-group" href="#collapse_titulares"
            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

            <div class="widget-title">
                <span class="icon"><i class="imoon-map-marker imoon-users" style="color: #344882"></i></span>
                <h5>
                    <asp:Label ID="lbl_titulares_tituloControl" runat="server" Text="Titulares"></asp:Label></h5>
                <span class="btn-right"><i class="imoon imoon-chevron-up" style="color: #344882"></i></span>
            </div>
        </a>
    </div>

    <%-- contenido del collapsible ubicaciones --%>
    <div class="accordion-body collapse in" id="collapse_titulares">
        <%--Botón de Modificación de Titulares --%>
        <asp:Panel ID="pnlModifTitulares" runat="server" CssClass="text-right mtop10 mright10" BackColor="#ffffff">
            <asp:LinkButton ID="btnModificarTitulares" runat="server" PostBackUrl="~/Titulares.aspx"
                CssClass="btn btn-primary">
                    <i class="imoon imoon-pencil"  style="color: #344882;"></i>
                <span class="text">Modificar Datos del/los titulares</span>
            </asp:LinkButton>
        </asp:Panel>
        <div style="margin: 10px">
            <uc:Titulares runat="server" ID="visTitulares" />
        </div>

    </div>

</div>

<%-- collapsible ubicaciones--%>
<div id="box_ubicacion" class="accordion-group widget-box" style="background-color: #ffffff">

    <%-- titulo collapsible ubicaciones--%>
    <div class="accordion-heading">
        <a id="ubicacion_btnUpDown" data-parent="#collapse-group" href="#collapse_ubicacion"
            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

            <div class="widget-title">
                <span class="icon"><i class="imoon-map-marker imoon-map-marker" style="color: #344882"></i></span>
                <h5>
                    <asp:Label ID="lbl_ubicacion_tituloControl" runat="server" Text="Ubicaci&oacute;n"></asp:Label></h5>
                <span class="btn-right"><i class="imoon imoon-chevron-up" style="color: #344882"></i></span>
            </div>
        </a>
    </div>

    <%-- contenido del collapsible ubicaciones --%>
    <div class="accordion-body collapse in" id="collapse_ubicacion">
        <%--Botón de Modificación de Ubicacion --%>
        <asp:Panel ID="pnlModifUbicacion" runat="server" CssClass="pull-right mbottom20 mright10" BackColor="#ffffff">
            <asp:LinkButton ID="btnModificarUbicacion" runat="server" PostBackUrl="~/Ubicacion.aspx"
                CssClass="btn btn-primary" Width="180px">
                    <i class="imoon imoon-pencil"></i>
                <span class="text">Modificar Ubicación</span>
            </asp:LinkButton>
        </asp:Panel>
        <div style="margin-left: 10px; margin-top: 10px; margin-right: 10px; margin-bottom: 20px">
            <uc:Ubicacion runat="server" ID="visUbicaciones" />
        </div>

    </div>

</div>


<%-- collapsible Rubro estadio--%>
<asp:Panel ID="box_RubroEstadio" runat="server" CssClass="accordion-group widget-box" style="background-color: #ffffff; display:block;" >
    <%-- titulo collapsible rubro estadio--%>
    <div class="accordion-heading">
        <a id="RubroEstadio_btnUpDown" data-parent="#collapse-group" href="#collapse_RubroEstadio"
            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

            <div class="widget-title">
                <span class="icon"><i class="imoon-map-marker imoon-map-marker" style="color: #344882"></i></span>
                <h5>
                    <asp:Label ID="lblTituloRubroEstadio" runat="server" Text="Exención de pago para rubros estadios de futbol"></asp:Label></h5>
                <span class="btn-right"><i class="imoon imoon-chevron-up" style="color: #344882"></i></span>
            </div>
        </a>
    </div>
    
    <%-- contenido del collapsible rubro estadio--%>
    <div class="accordion-body collapse in" id="collapse_RubroEstadio">
        <div style="margin-left: 10px; margin-top: 10px; margin-right: 10px; margin-bottom: 20px">
            <asp:UpdatePanel runat="server" ID="updRubroEstadio" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="pnlRubroEstadio" runat="server">

                        <div class="form-horizontal pright20">
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <div class="form-inline">
                                        <asp:Label ID="lblNroExpedienteCAA" runat="server" CssClass="control-label col-sm-3 text-center" Style="margin-left: 37px" AssociatedControlID="txtNroExpedienteCAA">Nro de Expediente de CAA:</asp:Label>
                                        <asp:TextBox ID="txtNroExpedienteCAA" runat="server" Width="200px" CssClass="form-control mleft15"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-horizontal pright20">
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <div class="form-inline">
                                        <asp:CheckBox ID="chkExencionPago" runat="server" Style="margin-left: 37px" />
                                        <asp:Label ID="lblchkExencionPago" runat="server">Solicito la exención del pago del timbrado de los derechos de habilitación por lo establecido por el artículo 14 de la Ley 2801, conforme texto del artículo 3º de la Ley 5290, BOCBA 4694.</asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-horizontal pright20">
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <asp:UpdatePanel ID="updGuardarRubroEstadio" runat="server" RenderMode="Inline">
                                        <ContentTemplate>
                                            <div class="form-inline">
                                                <div id="pnlBotonesGuardarRubroEstadio" runat="server">

                                                    <div class="text-right mtop10 mright20">
                                                        <asp:LinkButton ID="lnkGuardarRubroEstadio" runat="server" CssClass="btn btn-primary" OnClick="lnkGuardarRubroEstadio_Click">
                                                            <i class="imoon imoon-disk"></i>
                                                            <span class="text">Guardar</span>
                                                        </asp:LinkButton>

                                                    </div>
                                                </div>
                                                <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="updContinuar" runat="server"
                                                    DisplayAfter="0">
                                                    <ProgressTemplate>
                                                        <div class="pull-right">
                                                            <asp:Image ID="impProgress1" runat="server" ImageUrl="~/Content/img/app/Loading24x24.gif" /></td>
                                                          <td>Guardando...</td>
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Panel>

<%-- collapsible Datos del contacto--%>
<div id="box_Contacto" class="accordion-group widget-box" style="background-color: #ffffff">

    <%-- titulo collapsible Datos del contacto--%>
    <div class="accordion-heading">
        <a id="contacto_btnUpDown" data-parent="#collapse-group" href="#collapse_contacto"
            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

            <div class="widget-title">
                <span class="icon"><i class="imoon-map-marker imoon-map-marker" style="color: #344882"></i></span>
                <h5>
                    <asp:Label ID="lblDatosContacto" runat="server" Text="Datos del contacto"></asp:Label></h5>
                <span class="btn-right"><i class="imoon imoon-chevron-up" style="color: #344882"></i></span>
            </div>
        </a>
    </div>

    <%-- contenido del collapsible Datos del contacto--%>
    <div class="accordion-body collapse in" id="collapse_contacto">
        <div style="margin-left: 10px; margin-top: 10px; margin-right: 10px; margin-bottom: 20px">
            <asp:UpdatePanel runat="server" ID="udpcontacto" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="pnlDatosContacto" runat="server">

                        <h5 class="text-center" id="lblTexto" runat="server" style="font-weight: bold">Este teléfono sera utilizado para los casos en que se requiera coordinar una inspección previa al funcionamiento o habilitación en el establecimiento.</h5>


                        <div id="pnlDatosAnteriores" style="display: none" runat="server">
                            <div class="form-horizontal pright10">
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" class="control-label col-sm-2">Teléfono de contacto actual:</asp:Label>
                                    <div class="col-sm-4">
                                        <asp:Label ID="lblTlfAnterior" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />

                        <div class="form-horizontal pright20">
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <div class="form-inline">
                                        <asp:Label ID="lblCodArea" runat="server" class="col-sm-3 control-label" Style="margin-left: 37px">Cod. Area:</asp:Label>
                                        <asp:TextBox ID="txtCodArea" runat="server" MaxLength="3" Width="100px" CssClass="form-control mleft15"></asp:TextBox>
                                        <div id="Req_CodArea" class="field-validation-error" style="display: none;">
                                            Debe ingresar el Código de Area.
                                        </div>
                                        <asp:Label ID="lblPrefijo" runat="server" class="pleft5 pright5">Prefijo:</asp:Label>
                                        <asp:TextBox ID="txtPrefijo" runat="server" MaxLength="4" Width="100px" CssClass="form-control"></asp:TextBox>
                                        <div id="Req_Prefijo" class="field-validation-error" style="display: none;">
                                            Debe ingresar el Prefijo.
                                        </div>
                                        <asp:Label ID="lblSufijo" runat="server" class="pleft5 pright5">Sufijo:</asp:Label>
                                        <asp:TextBox ID="txtSufijo" runat="server" MaxLength="4" Width="100px" CssClass="form-control"></asp:TextBox>
                                        <div id="Req_Sufijo" class="field-validation-error" style="display: none;">
                                            Debe ingresar el Sufijo.
                                        </div>
                                    </div>
                                    <asp:UpdatePanel ID="updContinuar" runat="server" RenderMode="Inline">
                                        <ContentTemplate>
                                            <div class="form-group form-inline ">
                                                <div id="pnlBotonesGuardar" runat="server">

                                                    <div class="text-right mtop10 mright20">
                                                        <asp:LinkButton ID="btnGuardarContacto" runat="server" CssClass="btn btn-primary" OnClick="btnGuardarContacto_Click" OnClientClick="return validarGuardar();">
                                                            <i class="imoon imoon-disk"></i>
                                                            <span class="text">Guardar</span>
                                                        </asp:LinkButton>

                                                    </div>
                                                </div>
                                                <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="updContinuar" runat="server"
                                                    DisplayAfter="0">
                                                    <ProgressTemplate>
                                                        <div class="pull-right">
                                                            <asp:Image ID="impProgress" runat="server" ImageUrl="~/Content/img/app/Loading24x24.gif" /></td>
                                                          <td>Guardando...</td>
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>
</div>



<%--<div id="box_ExpRel" class="accordion-group widget-box" style="background-color: #ffffff" runat="server">
    <div class="accordion-heading">
        <a id="exprel_btnUpDown" data-parent="#collapse-group" href="#collapse_exprel"
            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

            <div class="widget-title">
                <span class="icon"><i class="imoon-map-marker imoon-map-marker" style="color: #344882"></i></span>
                <h5>
                <asp:Label ID="Label4" runat="server" Text="Expediente Relacionado"></asp:Label></h5>
                <span class="btn-right"><i class="imoon imoon-chevron-up" style="color: #344882"></i></span>
            </div>
        </a>
  </div>
  contenido del collapsible Expediente Relacionado
    <div class="accordion-body collapse in" id="collapse_exprel">
        <div style="margin-left: 10px; margin-top: 10px; margin-right: 10px; margin-bottom: 20px">
            <asp:UpdatePanel runat="server" ID="udpexpRel" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="pnlExpRel" runat="server">

                        <div class="form-horizontal pright20">
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <div class="form-inline">
                                        <asp:Label ID="Label7" runat="server" class="col-sm-3 control-label" Style="margin-left: 37px">Número Expediente SADE:</asp:Label>
                                        <asp:TextBox ID="txtNumeroExpSade" runat="server" MaxLength="50" Width="300px" CssClass="form-control mleft15"></asp:TextBox>
                                        <div id="Req_NumeroExpSade" class="field-validation-error" style="display: none;">
                                            Debe ingresar el Número Expediente SADE.
                                        </div>
                                    </div>
                                    <asp:UpdatePanel ID="updContinuarNumero" runat="server" RenderMode="Inline">
                                        <ContentTemplate>
                                            <div class="form-group form-inline ">
                                                <div id="pnlBotonesGuardarNumero" runat="server">

                                                    <div class="text-right mtop10 mright20">
                                                        <asp:LinkButton ID="btnGuardarNumeroExpSade" runat="server" CssClass="btn btn-primary" OnClick="btnGuardarNumeroExpSade_Click" OnClientClick="return validarGuardarNumero();">
                                                            <i class="imoon imoon-disk"></i>
                                                            <span class="text">Guardar</span>
                                                        </asp:LinkButton>

                                                    </div>
                                                </div>
                                                <asp:UpdateProgress ID="UpdateProgress3" AssociatedUpdatePanelID="updContinuarNumero" runat="server"
                                                    DisplayAfter="0">
                                                    <ProgressTemplate>
                                                        <div class="pull-right">
                                                            <asp:Image ID="impProgress3" runat="server" ImageUrl="~/Content/img/app/Loading24x24.gif" /></td>
                                                          <td>Guardando...</td>
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>
</div>--%>

<%--Modal mensajes de error--%>
<div id="frmError_DatosSolicitud" class="modal fade" role="dialog">
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
                            <asp:Label runat="server" class="imoon imoon-info fs64" Style="color: #377bb5"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="updmpeInfo" runat="server" class="form-group">
                                <ContentTemplate>
                                    <asp:Label ID="lblError" runat="server" class="pad10"></asp:Label>
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


<div id="frmDatosGuardados" class="modal fade" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title" style="margin-top: -8px">Datos adicionales</h4>
            </div>
            <div class="modal-body">
                <table style="border-collapse: separate; border-spacing: 5px">
                    <tr>
                        <td style="text-align: center; vertical-align: text-top">
                            <asp:Label ID="Label2" runat="server" class="imoon imoon-check fs64" Style="color: #377bb5"></asp:Label>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="form-group">
                                <ContentTemplate>
                                    <asp:Label ID="Label3" runat="server" Style="margin-left: 30px" Text="Los datos se han guardado correctamente"></asp:Label>
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

<script type="text/javascript">

    function showfrmError_DatosSolicitud() {

        $("#frmError_DatosSolicitud").modal("show");
        return false;

    }

    function showfrmDatosGuardados() {

        $("#frmDatosGuardados").modal("show");
        return false;

    }

    function init_JS_updDatosContacto() {
        $("#<%: txtCodArea.ClientID %>").autoNumeric({ aSep: '', mDec: '0', vMax: '999' });
        $("#<%: txtPrefijo.ClientID %>").autoNumeric({ aSep: '', mDec: '0', vMax: '9999' });
        $("#<%: txtCodArea.ClientID %>").autoNumeric({ aSep: '', mDec: '0', vMax: '9999' });
        $("#<%: txtSufijo.ClientID %>").on("keyup", function (e) {
            $("#Req_CodArea").hide();
        });

        $("#<%: txtPrefijo.ClientID %>").on("keyup", function (e) {
            $("#Req_Prefijo").hide();
        });

        $("#<%: txtSufijo.ClientID %>").on("keyup", function (e) {
            $("#Req_Sufijo").hide();
        });
    }
    function validarGuardar() {

        var ret = true;

        $("#Req_CodArea").hide();
        $("#Req_Prefijo").hide();
        $("#Req_Sufijo").hide();

        if ($.trim($("#<%: txtCodArea.ClientID %>").val()).length == 0) {
            $("#Req_CodArea").css("display", "inline-block");
            ret = false;
        }

        if ($.trim($("#<%: txtPrefijo.ClientID %>").val()).length == 0) {
            $("#Req_Prefijo").css("display", "inline-block");
            ret = false;
        }

        if ($.trim($("#<%: txtSufijo.ClientID %>").val()).length == 0) {
            $("#Req_Sufijo").css("display", "inline-block");
            ret = false;
        }
        showfrmDatosGuardados();
        return ret;
    }
<%--    function validarGuardarNumero() {
        var ret = true;

        $("#Req_NumeroExpSade").hide();

        if ($.trim($("#<%: txtNumeroExpSade.ClientID %>").val()).length == 0) {
            $("#Req_NumeroExpSade").css("display", "inline-block");
            ret = false;
        }

        return ret;
    }--%>
</script>
