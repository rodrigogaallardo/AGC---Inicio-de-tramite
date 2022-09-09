<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ubicacion.aspx.cs" Inherits="SSIT.Solicitud.Transferencia.Ubicacion" %>

<%@ Register Src="~/Solicitud/Habilitacion/Controls/ZonaPlaneamiento.ascx" TagPrefix="uc" TagName="ZonaPlaneamiento" %>
<%@ Register Src="~/Solicitud/Transferencia/Controls/Ubicacion.ascx" TagPrefix="uc1" TagName="Ubicacion" %>
<%@ Register Src="~/Solicitud/Transferencia/Controls/BuscarUbicacion.ascx" TagPrefix="uc1" TagName="BuscarUbicacion" %>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <%: Scripts.Render("~/bundles/autoNumeric") %>

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

    <div id="page_content" Style="display:none">

        <h2>Ubicaci&oacute;n</h2>
        <hr />

        <div class="row">

            <div class="col-sm-1 mtop10" style="width:25px">
                <i class="imoon imoon-info fs24" style="color:#377bb5"></i>
            </div>
            <div class="col-sm-11">
                
                <p class="pad10" >
                    En este paso deberá ingresar la ubicación donde se encuentra el establecimiento a habilitar.<br /> 
                    La ubicación se puede ingresar a través del:
                </p>

            </div>
        </div>
        <asp:UpdatePanel ID="updUbicaciones" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="hid_return_url" runat="server" />
                <div class="row mleft20 mright20">
                    <div class="col-sm-9">
                        <ul class="pleft40">
                            <li>Domicilio.</li>
                            <li>N&uacute;mero de Partida (matriz o horizontal).</li>
                            <li>Datos Catastrales (Secci&oacute;n, manzana y Parcela).</li>
                            <li>Ubicaciones especiales.</li>
                        </ul>
                    </div>
                    <div class="text-right">
                       <asp:UpdatePanel ID="updAgregarUbicacion" runat="server">
                            <ContentTemplate>
                        <span class="btn btn-primary " onclick="showfrmAgregarUbicacion();">
                             <i class="imoon imoon-plus"></i>
                            <span class="text">Agregar Ubicaci&oacute;n</span>
                        </span>

                    </div>
                    </ContentTemplate>
                 </asp:UpdatePanel>
                </div>
        
                <asp:Button ID="btnCargarDatos" runat="server" OnClick="btnCargarDatos_Click"  Style="display: none" />
                <asp:HiddenField ID="hid_id_solicitud" runat="server" />

                <div id="box_ubicacion"  class="box-panel">
                     <div style="margin:20px; margin-top:-5px"> 
                            <div style="margin-top:5px; color:#377bb5">                                 
                                        <h4><i class="imoon imoon-map-marker" style="margin-right:10px"></i>Datos de la Ubicaci&oacute;n</h4> 
                                                      
                                    </div>
                              </div>
                     <uc1:Ubicacion runat="server" ID="visUbicaciones" OnEliminarClick="visUbicaciones_EliminarClick" OnEditarClick="visUbicaciones_EditarClick"/>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
        <br />

        <asp:UpdatePanel ID="updPlantas" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                    <%-- Contenido del panel de plantas a habilitar --%>
                      <div class="box-panel">


                            <asp:Panel ID="pnlPlantasHabilitar" runat="server" CssClass="mtop5" >
                                        
                                      
                                <div style="color:#377bb5">                                 
                                        <h4><i class="imoon imoon-stackexchange" style="margin-right:10px"></i>Seleccione las Plantas</h4> 
                                        <hr />                      
                                </div>
                                
                                           
                                <div class="row pleft20 mtop10">
                        
                                    <div class="form-group col-md-8">
                                        
                                            <asp:GridView 
                                                ID="grdPlantasHabilitar" 
                                                runat="server" 
                                                AutoGenerateColumns="false" 
                                                GridLines="None" 
                                                CellPadding="3" 
                                                ShowHeader="false"        
                                                DataKeyNames="IdTransferenciaTipoSector"                                         
                                                OnRowDataBound="grdPlantasHabilitar_OnRowDataBound"
                                                >
                                                <Columns>
                                                            
                                                    <asp:TemplateField ItemStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <div class="checkbox mtop1">
                                                                <label>
                                                                    <asp:CheckBox ID="chkSeleccionado" runat="server" Checked='<% #Eval("Seleccionado") %>'
                                                                        OnCheckedChanged="chkSeleccionado_CheckedChanged" AutoPostBack="true" /> <%# Eval("Descripcion") %>
                                                                    <asp:HiddenField ID="hid_id_tiposector" runat="server" Value='<% #Eval("IdTipoSector") %>' />
                                                                    <asp:HiddenField ID="hid_descripcion" runat="server" Value='<% #Eval("Descripcion") %>' />
                                                                </label>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField >
                                                        <ItemTemplate>
                                                                <div class="form-inline">
                                                                    <div class="form-group">
                                                                        <asp:TextBox ID="txtDetalle" runat="server" CssClass="form-control mtop5 mbottom5" MaxLength='<% #Eval("TamanoCampoAdicional") %>'
                                                                            Visible='<% #Eval("Ocultar") %>' Width="100px" Text='<% #Eval("detalleDES") %>'></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <asp:Panel ID="ReqtxtDetalle" runat="server" CssClass="field-validation-error" Style="display: none">
                                                                            Debe ingresar la aclaración del item.
                                                                        </asp:Panel>
                                                                    </div>
                                                                </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        
                                    </div>
                                    <div class="form-group col-md-4">
                                        
                                        <div>Opciones con información adicional:</div>
                                        <div style="padding-top:10px"><b>Piso:</b> En esta opción deberá indicar la aclaración del piso. Ej: Piso 2, Piso 3</div>
                                        <div style="padding-top: 10px"><b>Otro:</b> En esta opción deberá indicar la descripcón deaseada, alguna no incluída en la lista ofrecida.</div>
                                        <div style="padding-top:10px">
                                            <b>Nota:</b> En los campos "Otro" <b>NO</b> deberá ingresar información de otra cosa que no sea una planta a habilitar. <b>NO</b> indicar unidades funcionales, departamentos, locales o cualquier referencia a la ubicación.
                                        </div>
                                        
                                    </div>
                        
                                </div>

                               
                            </asp:Panel>

                            
                            <asp:Panel ID="Req_Plantas" runat="server" CssClass="field-validation-error" Style="display: none">
                                Debe ingresar la informaci&oacute;n referida a las plantas.
                            </asp:Panel>
                           
                        </div>
                 
                
            </ContentTemplate>
        </asp:UpdatePanel>


        <%--Botones de Guardado--%>
        <asp:UpdatePanel ID="updBotonesGuardar" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <div class="form-inline text-right mtop20">
                    <div id="pnlBotonesGuardar" class="form-group">

                        <asp:LinkButton ID="btnContinuar" runat="server" CssClass="btn btn-lg btn-primary" OnClientClick="return validarGuardar();"
                            OnClick="btnContinuar_Click">
                                <i class="imoon imoon-disk"></i>
                                <span class="text">Guardar y Continuar</span>
                        </asp:LinkButton>

                    </div>
                    <div class="form-group">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="200" AssociatedUpdatePanelID="updBotonesGuardar">
                            <ProgressTemplate>
                                <img src='<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>' style="margin-left: 10px" alt="loading" />Guardando...
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>

        

    </div>

     <%--Modal mensajes de error--%>
    <div id="frmError" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top:-8px">Advertencia</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px" >
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon-info fs64" style="color:#377bb5"></i>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="updmpeInfo" runat="server" class="form-group">
                                    <ContentTemplate>
                                        <asp:Label ID="lblError" runat="server" ></asp:Label>
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

    <%--Modal Confirmar Eliminación--%>
    <div id="frmConfirmarEliminar" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top:-8px">Eliminar ubicaci&oacute;n</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon-remove-circle fs64 color-blue"></i>
                            </td>
                            <td style="vertical-align: middle">
                                <label class="mleft10">¿ Est&aacute; seguro de eliminar esta ubicaci&oacute;n ?</label>
                            </td>
                        </tr>
                    </table>

                </div>
                <div class="modal-footer mleft20 mright20">

                    <asp:UpdatePanel ID="updConfirmarEliminar" runat="server">
                        <ContentTemplate>

                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updConfirmarEliminar">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div id="pnlBotonesConfirmacion" class="form-group">
                                    <asp:Button ID="btnEliminar_Si" runat="server" CssClass="btn btn-primary" Text="Sí"  OnClick="btnEliminar_Si_Click" OnClientClick="ocultarBotonesConfirmacion();" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">No</button>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->


    <%-- Modal Agregar Ubicación --%>
    <div id="frmAgregarUbicacion" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                  <div class="modal-header" style="background:#FFFFFF">
                   <h4 class="modal-title" style="margin-top:-8px">Buscar Ubicaci&oacute;n</h4>
                </div>
                <div class="modal-body" style="background:#efefef">
                    <uc1:BuscarUbicacion runat="server" ID="BuscarUbicacion" />
                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->

    <script type="text/javascript">

        $(document).ready(function () {
            $("#page_content").hide();
            $("#Loading").show();
            $("#<%: btnCargarDatos.ClientID %>").click();
                    

        });
        function finalizarCarga() {

            $("#Loading").hide();
            $("#page_content").show();

            return false;

        }

        function showfrmError() {
            $("#pnlBotonesGuardar").show();
            $("#frmError").modal("show");
            return false;

        }
        function showfrmConfirmarEliminar() {

            $("#pnlBotonesConfirmacion").show();
            $("#frmConfirmarEliminar").modal("show");
            return false;
        }

        function hidefrmConfirmarEliminar() {
            
            $("#frmConfirmarEliminar").modal("hide");
            return false;
        }

        function ocultarBotonesConfirmacion() {

            $("#pnlBotonesConfirmacion").hide();
            return false;
        }

        function showfrmAgregarUbicacion() {

            $("#frmAgregarUbicacion").modal({
                "show": true,
                "backdrop": "static",
                "keyboard": false
                });
            return false;
        }

        function hidefrmAgregarUbicacion() {
            
            $("#frmAgregarUbicacion").modal("hide");
            //return false;
        }

        function ocultarBotonesGuardado() {

            $("#pnlBotonesGuardar").hide();

            return true;
        }

        function validarGuardar() {

            var ret = true;
            var plantasSeleccionadas = false;

            $("#<%: Req_Plantas.ClientID %>").hide();
            $("#<%: grdPlantasHabilitar.ClientID %> [id*='_ReqtxtDetalle']").hide();
            
            plantasSeleccionadas = ($("#<%: grdPlantasHabilitar.ClientID %> :checkbox[checked]").length > 0);

            $("#<%: grdPlantasHabilitar.ClientID %> [id*='_txtDetalle']").each(function (index, element) {

                if ($(element).val().length == 0) {
                    var txtDetalle_id = $(element).prop("id");
                    var chkSeleccionado_id = txtDetalle_id.replace("_txtDetalle", "_chkSeleccionado");
                    var ReqtxtDetalle_id = txtDetalle_id.replace("_txtDetalle", "_ReqtxtDetalle");

                    if ($("#" + chkSeleccionado_id).prop("checked")) {
                        
                        
                        $("#" + ReqtxtDetalle_id).show();
                        ret = false;

                    }
                }

            })

            if (!plantasSeleccionadas) {
                $("#<%: Req_Plantas.ClientID %>").css("display", "inline-block");
                ret = false;
            }

            if (ret)
                ocultarBotonesGuardado();

            return ret;
        }
        
    </script>
</asp:Content>
