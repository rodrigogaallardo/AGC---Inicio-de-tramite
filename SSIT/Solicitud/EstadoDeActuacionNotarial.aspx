<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EstadoDeActuacionNotarial.aspx.cs" Inherits="SSIT.Solicitud.EstadoDeActuacionNotarial" %>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
    <%: Scripts.Render("~/bundles/autoNumeric") %>
    <%: Scripts.Render("~/bundles/select2") %>
    <%: Styles.Render("~/bundles/select2Css") %>
    
        <div class="content_sitio">
            <div id="content_sitio" style="padding-bottom: 20px">
                <asp:UpdatePanel ID="UpdPnlConsultar" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>

                    
                        <asp:Panel ID="pnlInfoPaso" runat="server" DefaultButton="btnConsultar">
                            
    <h2 >
        Consultar Actuación Notarial
    </h2>
    <hr />
                            
    <p class="mtop10" >
                                <i class="imoon imoon-info " style="margin-right: 10px"></i>Desde aquí podrá consultar la Actuación Notarial<br />
    </p>

                    
                            <div class="form-horizontal">
                    <div class="row mleft10 mtop10">
                                    <label class="control-label col-sm-4">N&uacute;mero de Encomienda:</label>
                                    <div class="col-sm-2">
                                        <asp:TextBox ID="txtNumDeEncomienda" runat="server" CssClass="form-control"  ClientIDMode="Static"></asp:TextBox>
                                        <div id="Req_NumDeEncomienda" class="field-validation-error" style="display: none;">
                                            Debe ingresar el N&uacute;mero de Encomienda
                                        </div>
                                    </div>
                                </div>
                                
                    <div class="row mleft10 mtop10">
                                    <label class="control-label col-sm-4" >C&oacute;digo de Confirmaci&oacute;n de los Escribanos:</label>
                                    <div class="col-sm-2">
                                        <asp:TextBox ID="txtCodConfirmacionEscribanos" runat="server" CssClass="form-control"  ClientIDMode="Static"></asp:TextBox>
                                        <div id="Req_CodConfirmacionEscribanos" class="field-validation-error" style="display: none;">
                                            Debe ingresar el C&oacute;odigo de Confirmaci&oacute;n de los Escribanos
                                        </div>
                                    </div>
                                </div>

                    <div class="row mleft10 mtop10">
                        
                            
                        <label class="control-label col-sm-4"></label>
                                     
                           <div class="col-sm-4">
                               <div class="pull-left">
                                   
                                        <asp:LinkButton ID="btnConsultar" runat="server" CssClass="btn btn-primary" OnClick="btnConsultar_Click" OnClientClick="return validarConsultar();" >
                                            <i class="imoon imoon-search"></i>
                                            <span class="text">Consultar</span>
                                        </asp:LinkButton>
                               </div>
                                        <div class="pull-left">
                            <asp:UpdateProgress ID="UpdateProgress2" runat="server"  AssociatedUpdatePanelID="UpdPnlConsultar">
                                <ProgressTemplate>
                                    <img src="../Content/img/app/Loading24x24.gif" style="margin-left: 10px" alt="loading" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>

                                        </div>
                               </div>
                                    </div>
                            </div>            
                        </asp:Panel>
                        </ContentTemplate>
                </asp:UpdatePanel>
                
        <%--collapsible observaciones--%>    
        <div id="box_observacion" class="accordion-group widget-box">

        <%-- titulo collapsible observaciones--%> 
        <div class="accordion-heading">
            <a id="observacion_btnUpDown" data-parent="#collapse-group" href="#collapse_observacion" 
                data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                <asp:HiddenField ID="hid_observacion_collapse" runat="server" Value="true"/>
                <asp:HiddenField ID="hid_observacion_visible" runat="server" Value="false"/>

                <div class="widget-title">
                    <span class="icon"><i class="imoon imoon-list-alt"></i></span>
                    <h5><asp:Label ID="lbl_observacion_tituloControl" runat="server" Text="Observaciones"></asp:Label></h5>
                    <span class="btn-right"><i class="imoon imoon-chevron-up"></i></span>        
                </div>
            </a>
        </div>

        <%-- contenido del collapsible observaciones --%>   
        <div class="accordion-body collapse in" id="collapse_observacion" >
            <div class="widget-content">
                <asp:UpdatePanel ID="updPnlObservaciones" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                <ContentTemplate>
                        
                    <asp:GridView ID="gridObservaciones" runat="server" 
                        CssClass="table table-bordered table-striped table-hover with-check"
                        AutoGenerateColumns="false" DataKeyNames="id_solobs"
                        OnRowDataBound="gridObservaciones_OnRowDataBound">
                        <Columns>
                            <asp:BoundField DataField="CreateDate" HeaderText="Fecha" ItemStyle-Width="150px" DataFormatString="{0:dd/MM/yyyy HH:mm}"  />
                            <asp:BoundField DataField="userApeNom" HeaderText="Calificador" />
                                        
                            <asp:TemplateField  ItemStyle-Width="190px">
                                <ItemTemplate>
                                                
                                    <asp:LinkButton ID="lnkModal" runat="server"  CssClass="" data-toggle="modal" data-target="codebehind"  >
                                        
                                        <span class="text">Leer Observaci&oacute;n</span>
                                    </asp:LinkButton>

                                    <asp:Panel ID="pnlObservacionModal" runat="server" ClientIDMode="Static" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="display:none; max-height:90%;overflow:auto" >
                                        <div class="modal-dialog">
                                        <div class="modal-content">
                                            <div class="modal-header titulo-2">
                                                    
                                                <a class="close" data-dismiss="modal">×</a>
                                                <p>Causales de no recepci&oacute;n</p>
                                            </div>
                                                    
                                            <div class="modal-body" >
                                                <p>Solicitud: <b><%#Eval("id_solicitud")%></b></p>
                                                <p>Fecha: <b> <%#Eval("CreateDate", "{0:dd/MM/yyyy HH:mm}")%></b></p>
                                                <p><%#Eval("observaciones")%></p>
                                            </div>

                                            <div class="modal-footer">
                                                <a href="#" class="btn btn-default" data-dismiss="modal">Cerrar</a>
                                            </div>
                                        </div>
                                        </div>
                                    </asp:Panel>
        
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                             <div class="pad10">

                                <img src="<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>" />
                                <span class="mleft20">No se encontraron registros.</span>

                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>

                    <asp:GridView ID="gridObservacionesNew" runat="server" 
                        CssClass="table table-bordered table-striped table-hover with-check"
                        AutoGenerateColumns="false" DataKeyNames="id_ObsDocs" 
                        OnRowDataBound="gridObservacionesNew_OnRowDataBound">
                        <Columns>
                            <asp:BoundField DataField="CreateDate" HeaderText="Fecha" ItemStyle-Width="150px" DataFormatString="{0:dd/MM/yyyy HH:mm}"  />
                            <asp:BoundField DataField="nombre_tdocreq" HeaderText="Documento" />
                               
                            <asp:TemplateField  ItemStyle-Width="190px">
                                <ItemTemplate>
                                                
                                    <asp:LinkButton ID="lnkModal" runat="server"  CssClass="" data-toggle="modal" data-target="codebehind"  >
                                        
                                        <span class="text">Leer Observaci&oacute;n</span>
                                    </asp:LinkButton>

                                    <asp:Panel ID="pnlObservacionModal" runat="server" ClientIDMode="Static" class="modal fade" tabindex="-1" role="dialog" aria-hidden="true" style="display:none; max-height:90%;overflow:auto" >
                                        <div class="modal-dialog">
                                        <div class="modal-content">
                                            <div class="modal-header titulo-2">
                                                    
                                                <a class="close" data-dismiss="modal">×</a>
                                                <p>Causales de no recepci&oacute;n</p>
                                            </div>
                                                    
                                            <div class="modal-body" >
                                                <p>Solicitud: <b><%#Eval("id_ObsDocs")%></b></p>
                                                <p>Fecha: <b> <%#Eval("CreateDate", "{0:dd/MM/yyyy HH:mm}")%></b></p>
                                                <p><%#Eval("Observacion_ObsDocs")%></p><br />
                                                <p><%#Eval("Respaldo_ObsDocs")%></p>
                                            </div>

                                            <div class="modal-footer">
                                                <a href="#" class="btn btn-default" data-dismiss="modal">Cerrar</a>
                                            </div>
                                        </div>
                                        </div>
                                    </asp:Panel>
        
                                </ItemTemplate>
                            </asp:TemplateField>         
                        </Columns>
                        </asp:GridView>
                </ContentTemplate>
                </asp:UpdatePanel>        
            </div>
        </div>

        <asp:UpdatePanel ID="updRefresh" runat="server" RenderMode="Inline">
        <ContentTemplate>

            <asp:HiddenField ID="hid_panel_visible" runat="server" />
            <asp:HiddenField ID="hid_id_solicitud" runat="server" />
            <asp:HiddenField ID="hid_id_encomienda" runat="server" />
            <asp:HiddenField ID="hid_mostrar_observacion" runat="server" />
            <asp:Panel id="pnlAlertasSolicitud" runat="server" Visible="false"  CssClass="alert alert-danger" 
                style="padding-bottom:5px; padding-top:5px">
                <asp:Label ID="lblAlertasSolicitud" runat="server"></asp:Label>
            </asp:Panel>
                                                                    
        </ContentTemplate>
        </asp:UpdatePanel>

        
        </div>

        <%-- collapsible documentos--%>    
        <div id="box_documento" class="accordion-group widget-box">
                    
            <%-- titulo collapsible documentos--%>    
            <div class="accordion-heading">
                <a id="documento_btnUpDown" data-parent="#collapse-group" href="#collapse_documento" 
                    data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                    <asp:HiddenField ID="hid_documento_collapse" runat="server" Value="true"/>
                    <asp:HiddenField ID="hid_documento_visible" runat="server" Value="true"/>

                    <div class="widget-title">
                        <span class="icon"><i class="imoon imoon-list-alt"></i></span>
                        <h5><asp:Label ID="lbl_documento_tituloControl" runat="server" Text="Lista de documentos"></asp:Label></h5>
                        <span class="btn-right"><i class="imoon imoon-chevron-up"></i></span>        
                    </div>
                </a>
            </div>

            <%-- contenido del collapsible documentos --%>    
            <div class="accordion-body collapse in" id="collapse_documento" >
                <div class="widget-content">
                    <asp:UpdatePanel ID="updPnlListaDocumentos" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                        <ContentTemplate>

                        <asp:GridView ID="GrdvCertificados" runat="server" CssClass="table table-bordered table-striped table-hover with-check"
                        AutoGenerateColumns="false" >
                            <Columns>
                                <asp:BoundField DataField="CreateDate" HeaderText="Fecha" ItemStyle-Width="90px" DataFormatString="{0:dd/MM/yyyy HH:mm}"  />
                                                                        
                                <asp:TemplateField ItemStyle-Width="190px">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkCertificado" runat="server" NavigateUrl='<%#  "~/Reportes/GetActa.aspx?id_certificado=" + Eval("id_certificado") %>' Target="_blank" >
                                            <i class="imoon imoon-download color-blue"></i>
                                                   <span class="text">Certificado Acta Notarial <%# Eval("NroTramite").ToString() %></span>
                                                </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                                <EmptyDataTemplate>
                                    <div class="pad10">
                                        <img src="<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>" />
                                        <span class="mleft20">No se encontraron registros.</span>
                                    </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                            
                            <asp:Panel ID="pnlNoRecordsCertificados" runat="server" class="pad10" style="display:none">

                                <img src="<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>" />
                                <span class="mleft20">No se encontraron registros.</span>

                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
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
                    <h4 class="modal-title" style="margin-top:-8px">Error</h4>
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
    
    <script type="text/javascript">

        $(document).ready(function () {

            $("#mnuRegistrarseBandeja").attr("class", "current");

            if (($("#optBusquedaSeleccion").attr("checked"))) {
                $("#selestados").show();
            }
            else {
                $("#selestados").hide();
            }

            init_Js_UpdPnlConsultar();
        });

        function seleccionEstados(mostrar) {

            if (mostrar) {
                $("#selestados").show(1000, function () {
                    EjecutarCargaBandeja();
                });
            }
            else {
                $("#selestados").hide(1000, function () {
                    EjecutarCargaBandeja();
                });
            }

            return false;
        }

        function EjecutarCargaBandeja() {
            $("#btnBuscar").click();
            return false;
        };

        function hideSummary() {
            if ($("[id!='ValSummary'][class*='alert-danger']:visible").length == 0) {
                $("#ValSummary").hide();
            }
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

        function validarConsultar() {
            hideSummary();
            var ret = true;

            $("#Req_NumDeEncomienda").hide();
            $("#Req_CodConfirmacionEscribanos").hide();


            if ($.trim($("#<%: txtCodConfirmacionEscribanos.ClientID %>").val()).length == 0) {
                $("#Req_CodConfirmacionEscribanos").css("display", "inline-block");
                ret = false;
            }

            if ($.trim($("#<%: txtNumDeEncomienda.ClientID %>").val()).length == 0) {
                $("#Req_NumDeEncomienda").css("display", "inline-block");
                ret = false;
            }


            return ret;
        }

        function init_Js_UpdPnlConsultar() {

            $("#txtNumDeEncomienda").focus();

            $("#<%: txtCodConfirmacionEscribanos.ClientID %>").autoNumeric({ aSep: '', mDec: '0', vMax: '999999' });
            $("#<%: txtNumDeEncomienda.ClientID %>").autoNumeric({ aSep: '', mDec: '0', vMax: '999999' });

            $("#<%: txtCodConfirmacionEscribanos.ClientID %>").on("keyup", function () {
                $("#Req_CodConfirmacionEscribanos").hide();
                hideSummary();
            });

            $("#<%: txtNumDeEncomienda.ClientID %>").on("keyup", function () {
                $("#Req_NumDeEncomienda").hide();
                hideSummary();
            });

            $("#box_observacion").hide();
            $("#box_documento").hide();

            //Preguntar a Richard 97 a 105 y 48 a 57   ^\d
            $("#txtNumDeEncomienda").keyup(function (e) {
                if (e.keyCode == 8 ||
                    /^([0-9])*$/.test(e.keyCode)) {
                    $('#box_documento').hide();
                    $('#box_observacion').hide();
                }
            });

            $("#txtCodConfirmacionEscribanos").keyup(function (e) {
                if (e.keyCode == 8 ||
                    /^([0-9])*$/.test(e.keyCode)) {
                    $('#box_documento').hide();
                    $('#box_observacion').hide();
                }
            });


            return false;
        }
        function showResults() {

            $('#box_documento').show("slow");
            $('#box_observacion').show("slow");
            return false;
        }

        function showfrmError() {

            $("#frmError").modal("show");
            return false;

        }

    </script>

</asp:Content>
