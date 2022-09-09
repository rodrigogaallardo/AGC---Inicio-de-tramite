<%@ Page  Title="Certificado de Sobrecarga" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CertificadoSobrecarga.aspx.cs" Inherits="AnexoProfesionales.CertificadoSobrecarga" %>

<%@ Register Src="~/Tramites/Habilitacion/Controls/SobreCargaDatos.ascx" TagPrefix="uc1" TagName="SobreCargaDatos" %>
<%@ Register Src="~/Tramites/Habilitacion/Controls/Titulo.ascx" TagPrefix="uc1" TagName="Titulo" %>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

<%: Scripts.Render("~/bundles/autoNumeric") %>
<%: Scripts.Render("~/bundles/select2") %>
<%: Styles.Render("~/bundles/Select2Css") %>

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
            <asp:Button ID="btnCargarDatos" runat="server"  Style="display: none" OnClick="btnCargarDatos_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="page_content" Style="display:none">
        <asp:HiddenField ID="hid_DecimalSeparator" runat="server" />
        <asp:HiddenField ID="hid_return_url" runat="server" />
        <asp:HiddenField ID="hid_id_encomienda" runat="server" />
        <asp:HiddenField ID="hid_id_encomiendadatoslocal" runat="server" />
        <uc1:Titulo runat="server" ID="Titulo" />
        <hr />
        <p>
            En este paso podr&aacute; elegir y cargar los certificados de sobrecarga.
        </p>
        <br />
        <asp:Panel ID="pnlSobrecarga" runat="server">
            <%--Certificado de SobreCarga--%>

                <asp:UpdatePanel ID="updCertificadoSobrecarga" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>   
                    <ul>
                        <li>Certificado de sobrecarga en base a:
                            <asp:DropDownList ID="ddlTiposCertificado" runat="server" Width="400px" style="display:initial; margin-left:10px" height="34px" CssClass="form-control"></asp:DropDownList>
                        </li>
                        <br />
                        <li>Requisitos:
                            <asp:DropDownList ID="ddlTiposSobrecargas" runat="server" Width="400px" height="34px"  style="display:initial; margin-left:175px" CssClass="form-control" AutoPostBack="true" 
                                OnSelectedIndexChanged="ddlTiposSobrecargas_SelectedIndexChanged"></asp:DropDownList>
                         </li>

                    <div style="margin-left:250px">
                        <div id="Req_Requisito" class="alert alert-small alert-danger mbottom0 mtop5" style="display:none; ">
                                            Debe seleccionar un requisito.
                        </div>
                        </div>
                         </ul>
                    <div class="pull-right" style="margin-top:-70px; margin-right:50px">
                        <span class="btn btn-primary" onclick="showfrmAgregarSobrecarga();">
                                             <i class="imoon imoon-plus"></i>
                            <span class="text">Agregar Sobrecarga</span>
                        </span>
                     </div>        
                     <%--Boton Agregar--%>      
                     <div class="mtop40">       
                    <asp:UpdatePanel ID="updGrillaSobreCargas" runat="server">
                       <ContentTemplate>
                           <asp:GridView ID="grdSobrecargas" CssClass="box-panel" runat="server" AutoGenerateColumns="false" GridLines="None" Width="1130px">                                                 
                                 <Columns>
                                   <asp:TemplateField ItemStyle-Width="100%">
                                     <ItemTemplate>
                                         <tr>
                                          <td>
                                            <div style="color:#377bb5; margin-left:10px; margin-top:-10px">                                 
                                                  <h4><i class="imoon imoon-stackexchange" style="margin-right:10px"></i>Listado de Sobrecargas</h4>
                                                   <div style="margin-top:-35px; margin-right:20px; text-align: right">
                                                      <asp:LinkButton title="Editar" ID="btnEditarSobrecarga" runat="server" CssClass="btn btn-default" Width="40px" 
                                                                    CommandName="Editar" OnClick="btnEditarSobrecarga_Click"> 
                                                                    <i class="imoon imoon-pencil" style="color:#377bb5"></i>
                                                                </asp:LinkButton>

                                                      <asp:LinkButton title="Eliminar" ID="btnEliminarSobrecarga" runat="server" CssClass="btn btn-default" Width="40px"
                                                                    CommandName="Eliminar" OnClick="btnEliminarSobrecarga_Click">
                                                                    <i class="imoon imoon-close" style="color:#377bb5" ></i>
                                                                 </asp:LinkButton>

                                                   </div>                                                                        
                                               </div>
                                             <asp:HiddenField ID="hid_id_sobrecarga_detalle1" runat="server" Value='<% #Eval("id_sobrecarga_detalle1") %>' />    
                                         <div style="margin:20px">                                                                                                                 
                                                <div class="divider"></div>
                                                <div class="form-horizontal" style="background:content-box">
                                                   <div class="form-group">
                                                                                   
                                                    <asp:Label ID="Label1" runat="server" CssClass="control-label col-sm-3 text-center" Font-Bold="true" Text='<%# tipoString() %>'></asp:Label>                                                                 
                                                    <asp:Label ID="lbl_tipo_destino" runat="server" CssClass="control-label col-sm-1 text-center" Text='<% #Eval("desc_tipo_destino") %>' Style="margin-left:-30px"></asp:Label>
                                                    <asp:HiddenField ID="hid_id_tipo_destino"  runat="server" Value='<% #Eval("id_tipo_destino") %>' />
                                                
                                                    <asp:Label ID="Label2" runat="server" CssClass="control-label col-sm-1 text-center" Font-Bold="true" Text="Planta:" Style="margin-left:50px"></asp:Label>                                                                                                                       
                                                    <asp:Label ID="lbl_planta" CssClass="control-label col-sm-1" runat="server" Text='<% #Eval("desc_planta") %>' Style="margin-left:-20px"></asp:Label>
                                                    <asp:HiddenField ID="hid_id_planta"  runat="server" Value='<% #Eval("id_planta") %>' />
                                                  
                                                         
                                                    <asp:Label ID="Label3" runat="server" CssClass="control-label col-sm-2 text-center" Font-Bold="true" Text="Losa Sobre" Style="margin-left:130px"></asp:Label>           
                                                    <asp:Label ID="lbl_losa_sobre" CssClass="control-label col-sm-1 text-center" runat="server"  Text='<% #Eval("losa_sobre") %>' Style="margin-left:70px"></asp:Label>
                                                                  
                                                   
                                                  </div>
                                                 </div>  
                                                <div class="divider"></div>   
                                                <div class="form-horizontal">
                                                  <div class="form-group">     
                                                    <asp:Label ID="Label4" runat="server" CssClass="control-label col-sm-3 text-center" Font-Bold="true" Text="Uso"></asp:Label>                                                                          
                                                    <asp:Label ID="lbl_tipo_uso" runat="server" CssClass="control-label col-sm-4 text-center" Text='<% #Eval("desc_tipo_uso") %>' Style="margin-left:10px"></asp:Label>
                                                    <asp:HiddenField ID="hid_id_tipo_uso" runat="server" Value='<% #Eval("id_tipo_uso") %>' />
                                                         
                                                     
                                                    <asp:Label ID="lblSobrecarga" runat="server" Text='<% #Eval("texto_carga_uso") %>'  CssClass="control-label col-sm-4 text-center" Font-Bold="true" Style="margin-left:-30px"  ></asp:Label>
                                                                 
                                                    <asp:Label ID="lbl_valor" runat="server"  CssClass="control-label col-sm-1 text-center"  Text='<% #Eval("valor") %>' ></asp:Label>
                                                    <asp:Label ID="lbl_detalle" runat="server"   CssClass="control-label col-sm-1 text-center" Text='<% #Eval("detalle") %>'></asp:Label>
                                                  </div>
                                                 </div>  
                                                <div class="divider"></div>       
                                                <div class="form-horizontal">
                                                  <div class="form-group">           
                                                    <asp:Label ID="lblUso1" runat="server" Text='<% #Eval("texto_uso_1") %>'  CssClass="control-label col-sm-3 text-center" Font-Bold="true" ></asp:Label>
                                                               
                                                    <asp:Label ID="lbl_tipo_uso_1" runat="server"  CssClass="control-label col-sm-4 text-center"  Text='<% #Eval("desc_tipo_uso_1") %>'  Style="margin-left:10px"></asp:Label>
                                                    <asp:HiddenField ID="hid_id_tipo_uso_1" runat="server"   Value='<% #Eval("id_tipo_uso_1") %>' />
                                              
                                                    <asp:Label ID="lblTxtUso1" runat="server"   CssClass="control-label col-sm-4 text-center" Text='<% #Eval("texto_carga_uso") %>' Font-Bold="true"  Style="margin-left:-30px"></asp:Label>            
                                                    <asp:Label ID="lbl_valor_1" runat="server"  CssClass="control-label col-sm-1 text-center"  Text='<% #!Eval("valor_1").ToString().Equals("0,00") ? Eval("valor_1") : "" %>' ></asp:Label>
                                                        </div>
                                                 </div>  
                                                <div class="divider"></div>                 
                                                <div class="form-horizontal">
                                                  <div class="form-group">                    
                                                    <asp:Label ID="lblUso2" runat="server"  CssClass="control-label col-sm-3 text-center"  Text='<% #Eval("texto_uso_2") %>' Font-Bold="true"></asp:Label>
                            
                                                    <asp:Label ID="lbl_tipo_uso_2" runat="server" CssClass="control-label col-sm-4 text-center" Text='<% #Eval("desc_tipo_uso_2") %>' Style="margin-left:10px"></asp:Label>
                                                    <asp:HiddenField ID="hid_id_tipo_uso_2" runat="server" Value='<% #Eval("id_tipo_uso_2") %>' />
                                               
                                                    <asp:Label ID="lblTxtUso2" runat="server" CssClass="control-label col-sm-4 text-center" Text='<% #Eval("texto_carga_uso") %>' Font-Bold="true" Style="margin-left:-30px"></asp:Label>
                                                              
                                                    <asp:Label ID="lbl_valor_2" runat="server" CssClass="control-label col-sm-1 text-center" Text='<% #!Eval("valor_2").ToString().Equals("0,00") ? Eval("valor_2") : "" %>'></asp:Label>
                                                 </div>
                                                 </div>   
                                                 <div class="divider"></div> 
                                        </div>
                                        </td>   
                                         </tr>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                           </asp:GridView>
                           <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                                                    AssociatedUpdatePanelID="updGrillaSobreCargas" DisplayAfter="0">
                                                    <ProgressTemplate>
                                                        <img src='<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>' alt="" />
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                           <asp:CustomValidator ID="CustomValidator5" runat="server" 
                                                    ClientValidationFunction="validarIngresoSobrecargas" CssClass="error-label" 
                                                    Display="Dynamic" ErrorMessage="Debe ingresar las sobrecargas." 
                                                    ValidationGroup="Continuar"></asp:CustomValidator>
                       </ContentTemplate>
                     </asp:UpdatePanel>
                    </div>      
                    <br />
             
                    <%--Botones de Guardado--%>
                      <br /><br />
          <asp:UpdatePanel ID="updBotonesGuardar" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <div class="form-inline text-right">
                     
                    <div id="pnlBotonesGuardar" class="form-group" >

                      <asp:LinkButton ID="lnkGuardarYContinuar" runat="server" CssClass="btn btn-primary" OnClick="lnkGuardarYContinuar_Click" >
                                <i class="imoon imoon-disk"></i>
                                <span class="text">Guardar y Continuar</span>
                        </asp:LinkButton>

                    </div>
                    <div class="form-group">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="200" AssociatedUpdatePanelID="updBotonesGuardar">
                            <ProgressTemplate>
                                <img src='<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>' style="margin-left: 10px" alt="loading" />Guardando...
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    <%-- Modal Agregar Ubicación --%>
    <div id="frmAgregarSobrecarga" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" style="margin-top:-8px">Agregar Sobrecarga</h4>
                </div>
                <div class="modal-body">
                    <uc1:SobreCargaDatos runat="server" ID="ucSobreCargaDatos" />
                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->
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
    <!-- /.modal -->
  
    <%--Confirmar Eliminar Sobrecarga--%>
    <div id="frmConfirmarEliminar" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top:-8px">Eliminar sobrecarga</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon-remove-circle fs64 color-blue"></i>
                            </td>
                            <td style="vertical-align: middle">
                                <label class="mleft10">¿ Est&aacute; seguro de eliminar el registro ?</label>
                            </td>
                        </tr>
                    </table>

                </div>
                <div class="modal-footer mleft20 mright20">

                    <asp:UpdatePanel ID="updConfirmarEliminar" runat="server">
                        <ContentTemplate>

                            <asp:HiddenField ID="hid_id_sobrecarga_eliminar" runat="server" />

                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updConfirmarEliminar">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div id="pnlBotonesConfirmacionEliminar" class="form-group">
                                    <asp:Button ID="btnConfirmarEliminarSobrecarga" runat="server" CssClass="btn btn-primary" Text="Sí" OnClick="btnConfirmarEliminarSobrecarga_Click"
                                        OnClientClick="ocultarBotonesConfirmacion();" />
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

    <script type="text/javascript">

      
        var prev_val;
        var aux = 0;
        $(document).ready(function () {
            
            $("#page_content").hide();
            $("#Loading").show();
            
            $("#<%: btnCargarDatos.ClientID %>").click();
          

        });

        function showfrmConfirmarEliminar(rowIndex) {
            //debugger;
            $("#<%: hid_id_sobrecarga_eliminar.ClientID %>").val(rowIndex);

            $("#frmConfirmarEliminar").modal("show");
            return false;
        }

        function hidefrmConfirmarEliminar() {
            $("#frmConfirmarEliminar").modal("hide");
        }

        function ocultarBotonesConfirmacion() {

            $("#pnlBotonesConfirmacionEliminar").hide();
            return false;
        }

        function finalizarCarga() {
            $("#Loading").hide();
            $("#page_content").show();
        }

        function showfrmAgregarSobrecarga() {
           
            if ($(<%: ddlTiposSobrecargas.ClientID %>).val() == "0") {
                $("#Req_Requisito").css("display", "inline-block");
               
            }
            if ($(<%: ddlTiposSobrecargas.ClientID %>).val() != "0") {
            
                $("#frmAgregarSobrecarga").modal({
                    "show": true,
                    "backdrop": "static",
                    "keyboard": false
                });
            }
        }

        function hidefrmAgregarSobrecarga() {
            $("#frmAgregarSobrecarga").modal("hide");

        }

        function validar() {

            var cantGrilla = $("[id*='grdSobrecargas']").length;

            if (cantGrilla > 1) {
                $('#<%: lnkGuardarYContinuar.ClientID %>').hide();
                return true;
            }
        }     

        function showfrmError() {
            $("#frmError").modal("show");
            return false;
        }
   </script>

 </asp:Content>