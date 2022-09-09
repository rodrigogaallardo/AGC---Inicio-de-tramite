<%@ Page Title="Carga de Plancheta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CargaPlancheta.aspx.cs" Inherits="SSIT.Solicitud.HabilitacionECI.CargaPlancheta" %>

<%@ Register Src="~/Solicitud/Controls/ucCargaDocumentos.ascx" TagPrefix="uc1" TagName="ucCargaDocumentos" %>
<%@ Register Src="~/Solicitud/Habilitacion/Controls/Titulo.ascx" TagPrefix="uc1" TagName="Titulo" %>


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
                <td style="font-size: 24px">Cargando datos...
                </td>
            </tr>
        </table>
    </div>
    
    <asp:UpdatePanel ID="updCargarDatos" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnCargarDatos" runat="server" OnClick="btnCargarDatos_Click" Style="display: none" />
            <asp:HiddenField ID="hid_id_solicitud" runat="server" />
            <asp:HiddenField ID="hid_return_url" runat="server" />
            <asp:HiddenField ID="hid_CargosFirPJ" runat="server" />
            <asp:HiddenField ID="hid_CargosFirSH" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--fin ajax cargando--%>

    <div id="page_content" Style="display:none">
        

        <div id="pnlBusquedaTramites">
            <uc1:Titulo runat="server" ID="Titulo" />

            <hr />

            <div class="row">
                <div class="col-sm-12">
                    <p class="pad10">
                        Ac&aacute; deber&aacute;s adjuntar la plancheta o constancia de habilitaci&oacute;n del establecimiento 
                        donde quer&eacute;s ampliar la superficie y/o rubro.
                    </p>
                </div>
                <div class="col-sm-12">
                    
                          
                    <asp:UpdatePanel ID="upPnlDocumentos" runat="server" >
                        <ContentTemplate>

                            <div style="padding: 0px 10px 10px 10px; width: auto">

                                <asp:Panel id="pnlTituloGrilla" runat="server" CssClass="mtop10" Visible="false" >
                                    <strong>Documentos Ingresados:</strong>
                                </asp:Panel>
                                <asp:GridView ID="gridAgregados_db" runat="server" AutoGenerateColumns="false"
                                    AllowPaging="false" Style="border: none;" CssClass="table table-bordered mtop5"
                                    ItemType="DataTransferObject.SSITDocumentosAdjuntosDTO"
                                    GridLines="None" Width="100%" DataKeyNames="id_docadjunto">
                                    <HeaderStyle CssClass="grid-header" />
                                    <RowStyle CssClass="grid-row" />
                                    <AlternatingRowStyle BackColor="#efefef" />
                                    <Columns>
                                        <asp:BoundField DataField="CreateDate" DataFormatString="{0:d}" HeaderText="Subido el" HeaderStyle-CssClass="text-center" ItemStyle-Width="90px" />
                                        <asp:BoundField DataField="nombre_archivo" HeaderText="Nombre del archivo" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                                        <asp:TemplateField ItemStyle-CssClass="text-center" HeaderText="Acciones" HeaderStyle-CssClass="text-center" ItemStyle-Width="80px">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkImprimirAgregados" runat="server" NavigateUrl='<%# Item.url %>' title="Descargar" Target="_blank" data-toggle="tooltip">
                                                    <span class="icon"><i class="imoon-download fs24 color-blue"></i></span>
                                                </asp:HyperLink>
                                                <asp:LinkButton ID="lnkEliminar" title="Eliminar" runat="server"  data-toggle="tooltip"
                                                    data-idDocAdjunto='<%# Eval("id_docadjunto") %>'
                                                    OnClientClick="return showfrmConfirmarEliminarDocumento(this);">
                                                    <span class="icon"><i class="imoon imoon-trash fs24"></i></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                </asp:GridView>

                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                        
                    
                </div>

                <div class="col-sm-12">
                    <asp:UpdatePanel ID="updCargaDocumentos" runat="server">
                        <ContentTemplate>
                            <uc1:ucCargaDocumentos runat="server" ID="ucCargaDocumentos"   OnErrorCargaDocumento="OnErrorCargaDocumentoClick" OnSubirDocumentoClick="CargaDocumentos_SubirDocumentoClick"  />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div id="Req_Archivos" class="alert alert-danger" style="display: none;">
                    Debe ingresar al menos un archivo para poder continuar.
                </div>
                                    
                
                 <div class="col-sm-12 text-right">

                     <asp:UpdatePanel ID="updContinuar" runat="server">
                         <ContentTemplate>

                                <asp:LinkButton ID="btnContinuar" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnContinuar_Click" OnClientClick="return validarGuardar();" >
                                    <i class="imoon imoon-save"></i>
                                    <span class="text">Guardar y Continuar</span>
                                </asp:LinkButton>

                        </ContentTemplate>
                     </asp:UpdatePanel>

                </div>


            </div>
        </div>
    </div>

    
    <%--Modal Confirmar Eliminar--%>
    <div id="frmConfirmarEliminarDocumento" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top:-8px">Eliminar Archivo</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon-remove-circle fs64 color-blue"></i>
                            </td>
                            <td style="vertical-align: middle">
                                <label class="mleft10">¿ Est&aacute; seguro de eliminar el archivo ?</label>
                            </td>
                        </tr>
                    </table>

                </div>
                <div class="modal-footer mleft20 mright20">
                    <asp:HiddenField ID="hid_id_docadjuntoEliminar" runat="server" />
                    <asp:UpdatePanel ID="updConfirmarEliminarDocumento" runat="server">
                        <ContentTemplate>

                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updConfirmarEliminarDocumento">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div id="pnlBotonesConfirmacionEliminarDocumento" class="form-group">
                                    <asp:Button ID="btnEliminarDocumento" runat="server" CssClass="btn btn-primary" Text="Sí" OnClick="btnEliminarDocumento_Click" OnClientClick="ocultarBotonesConfirmacionEliminarDocumento();" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">No</button>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>

    <%--Modal mensajes de error--%>
    <div id="frmError" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top:-8px">Atención</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <asp:label runat="server" class="imoon imoon-info fs64" style="color: #377bb5"></asp:label>
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
    <!-- /.modal -->

    
    <script type="text/javascript">

        $(document).ready(function () {

            $("#page_content").hide();
            $("#Loading").show();
            toolTips();

            $("#<%: btnCargarDatos.ClientID %>").click();


        });
        
        function finalizarCarga() {

            $("#Loading").hide();
            $("#page_content").show();

            return false;

        }

        function toolTips() {
            $("[data-toggle='tooltip']").tooltip();
            return false;

        }

        function showfrmError() {

            $("#frmError").modal("show");
            return false;
        }
        function showfrmConfirmarEliminarDocumento(obj) {

            var idDocAdjunto = $(obj).attr("data-idDocAdjunto");
            $("#<%: hid_id_docadjuntoEliminar.ClientID %>").val(idDocAdjunto);
            $("#frmConfirmarEliminarDocumento").modal("show");

            return false;
        }

        function ocultarBotonesConfirmacionEliminarDocumento() {
            $("#pnlBotonesConfirmacionEliminarDocumento").hide();
            return false;
        }

        function hidefrmConfirmarEliminarDocumento() {
            $("#frmConfirmarEliminarDocumento").modal("hide");
            return false;
        }

        function init_Js_upPnlDocumentos() {
            toolTips();
            return false;
        }

        function validarGuardar() {

            var ret = true;
            $("#Req_Archivos").hide();

            
            if ($("#<%: gridAgregados_db.ClientID %>").find("tr").length == 0) {
                $("#Req_Archivos").css("display", "inline-block");
                setTimeout(function () {
                    $("#Req_Archivos").hide("slow");
                }, 3000);
                ret = false;
            }

            return ret;

        }
    </script>

</asp:Content>
