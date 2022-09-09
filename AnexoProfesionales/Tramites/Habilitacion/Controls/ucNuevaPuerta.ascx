<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucNuevaPuerta.ascx.cs" Inherits="SSIT.Controls.ucNuevaPuerta" %>
   <%: Scripts.Render("~/bundles/select2") %>

<%: Scripts.Render("~/bundles/autoNumeric") %>
    <script type="text/javascript">
        function showfrmEnvioMail() {

            $("#frmEnvioMail").modal("show");
            return true;

        }

        function validarNuevaPuerta() {
            var ret = true;

            if ($.trim($("#<%: ddlCallesUbic.ClientID %>").val()).length == 0) {
                $("#Req_calleNuevaPuerta").css("display", "inline-block");

                ret = false;
            }
            if ($.trim($("#<%: txtNroPuerta_pa.ClientID %>").val()).length == 0) {
                $("#Req_puertaNuevaPuerta").css("display", "inline-block");

                ret = false;

            }
            if (ret) {
                showfrmEnvioMail();
            }
        }

        function init_JS_select2() {
            $("#<%: txtNroPuerta_pa.ClientID %>").autoNumeric({ aSep: '', mDec: '0', vMax: '99999999' });
            $("#<%: ddlCallesUbic.ClientID %>").select2({
                placeholder: "Seleccionar",
                allowClear: true,
                minimumInputLength: 3
            });
        }
        </script>
            <asp:Panel ID="pnlPedidoAltaUbic" runat="server" CssClass="modalPopup" >
                  <asp:HiddenField ID="hid_id_ubicacionPuerta" runat="server" />
            <%--contenido con diferentes opciones de busqueda de ubicaciones--%> 
            <asp:UpdatePanel ID="updPnlIngresarPedidoAlta" runat="server"  >
            <ContentTemplate>
                <script type="text/javascript">
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandler);
                    function endRequestHandler() {
                        init_JS_select2();
                    }
            </script> 
                   <table>
                    <tr>
                        
                        <td style="width: auto; padding-left: 10px; vertical-align: text-top">
                         <div class="titulo-4" style="border-bottom: solid 1px #e1e1e1; padding-top: 10px; font-weight:bold">
                           Datos de la Ubicación</div>



                            <asp:Panel ID="pnlSMP_pa" runat="server" style="padding-top:3px">
                                <div >
                                    Número de Partida Matriz:
                                    <asp:Label ID="grd_NroPartidaMatriz_pa" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <div>
                                    Sección:
                                    <asp:Label ID="grd_seccion_pa" runat="server" Font-Bold="true"></asp:Label>
                                    Manzana:
                                    <asp:Label ID="grd_manzana_pa" runat="server" Font-Bold="true"></asp:Label>
                                    Parcela:
                                    <asp:Label ID="grd_parcela_pa" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                            </asp:Panel>
                            <br />
                            <asp:Panel ID="pnlPuertas_pa" runat="server">

                                <div class="titulo-4" style="border-bottom: solid 1px #e1e1e1; padding-top: 10px; font-weight:bold">
                                Puertas</div>
                                
                                <asp:GridView ID="grdSeleccionPuertas_pa" runat="server" AutoGenerateColumns="false" GridLines="None" 
                                    Width="100%" ShowHeader="false" Font-Size="9pt">
                                    <Columns>
                                        
                                        <asp:TemplateField ItemStyle-Width="100px" ControlStyle-BackColor="Black">
                                            <ItemTemplate>
                                               <%# (Eval("Nombre_calle")) %> <%# (Eval("NroPuertaUbic")) %> 
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>

                            <br />

                            <asp:Panel ID="pnlCalle_pa" runat="server">
                              <table> 
                                 <div class="titulo-4" style="border-bottom: solid 1px #e1e1e1; padding-top: 10px; font-weight:bold">
                                       Seleccione un domicilio</div>
                                    <br />
                                        <td><strong>Calle:</strong></td>
                                        <td style="padding-left: 25px">

                                            <asp:DropDownList ID="ddlCallesUbic" placeholder="Seleccionar" runat="server" Width="500px"> </asp:DropDownList>
                                            <br />
                                            <span>Debe ingresar un mínimo de 3 letras y el sistema le mostrará las calles posibles.</span>
                                            <br />
                                          <div id="Req_calleNuevaPuerta" class="field-validation-error" style="display: none;">
                                            Debe ingresar una calle.
                                        </div>
                                        </td>
                                        <td></td>
                                    
                            </table>

                                <%--<asp:RequiredFieldValidator ID="ReqCalle" runat="server" ErrorMessage="Debe seleccionar una de las calles de la lista desplegable."
                                Display="Dynamic" ControlToValidate="ddlCallesUbic" 
                                CssClass="field-validation-error" Style="margin-left: 66px"></asp:RequiredFieldValidator>--%>

                            <br />
                            <div class="mtop10"></div>
                            <table>
                                <tr>
                                    <td><strong>N&uacute;mero:</strong></td>
                                    <td style="padding-left: 6px">
                                        <asp:TextBox ID="txtNroPuerta_pa" runat="server" Width="100px" MaxLength="5" CssClass="form-control"></asp:TextBox>
                                        <span>Debe ingresar el número de puerta</span>
                                        <br />
                                  <div id="Req_puertaNuevaPuerta" class="field-validation-error" style="display: none;">
                                            Debe ingresar una calle.
                                        </div>
                                    </td>
                            </table>                            
                            </asp:Panel>

                        </td>                        
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br /> <br />
                            <asp:Label ID="lblMailRtaSolicitudAlta" runat="server" CssClass="titulo-2" Style="padding: 0px 0px 5px 5px;
                            width: 100%"></asp:Label>
                        </td>
                    </tr>
                </table>

            </ContentTemplate>
            </asp:UpdatePanel>

            <%--contenido con diferentes opciones de busqueda de ubicaciones--%> 
            <asp:UpdatePanel ID="updPnlEnviarPedido" runat="server"  >
                <ContentTemplate>
                    <div class="text-center">
                                <%--Botón enviar mail de solicitud nueva calle--%>
                                <asp:Button ID="btnEnviarPedidoAltaUbic" runat="server" CssClass="btn btn-primary" Text="Enviar"                                  
                                        Width="80px" OnClick="btnEnviarPedidoAltaUbic_Click" OnClientClick="validarNuevaPuerta();" />

                                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </asp:Panel>          

<div id="frmEnvioMail" class="modal fade" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title" style="margin-top: -8px">Solicitud de nueva calle en parcela</h4>
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
                                    <asp:Label ID="Label3" runat="server" style="margin-left:30px" Text="Se ha enviado el e-mail correctamente" ></asp:Label>
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