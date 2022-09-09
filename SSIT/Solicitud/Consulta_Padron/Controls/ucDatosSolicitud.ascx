<%@ Control Language="C#" CodeBehind="ucDatosSolicitud.ascx.cs" Inherits="SSIT.Solicitud.Consulta_Padron.Controls.ucDatosSolicitud" %>


<asp:UpdatePanel runat="server" ID="updDatosSolicitud" UpdateMode="Conditional">
    <ContentTemplate>
  <script type="text/javascript">

      var vSeparadorDecimal;
      var prev_val;

      $(document).ready(function () {
          vSeparadorDecimal = $("#hid_DecimalSeparator").attr("value");
      });

      function validarNroExp(sender, args) {
          //var nro = $("#txtNroExpediente").attr("value");
          //var formatoExpediente = /^[0-9]{1,10}\/[0-9]{4}$/;
          args.IsValid = true;
          //if (nro.length==0 || !formatoExpediente.test($.trim(nro))) 
          //    args.IsValid = false;
          return args.IsValid;
      }



    </script>

                <div style="text-align: center;">

            <asp:HiddenField ID="hid_DecimalSeparator" runat="server" />

                <%--Contenido--%>
                <asp:Panel ID="pnlContenido" runat="server" >

                    <div class="content_sitio">

                        <div id="content_sitio">

                             <%-- collapsible ubicaciones--%>
                            <div id="box_ubicacion" class="accordion-group widget-box" style="background:#ffffff">

                                <%-- titulo collapsible ubicaciones--%>
                                <div class="accordion-heading">
                                    <a id="ubicacion_btnUpDown" data-parent="#collapse-group" href="#collapse_DatosSolicitud" data-toggle="collapse">

                                        <asp:HiddenField ID="hid_ubicacion_collapse" runat="server" Value="true" />
                                        <asp:HiddenField ID="hid_ubicacion_visible" runat="server" Value="false" />

                                        <div class="widget-title">
                                            <span class="icon"><i class="imoon imoon-list"  style="color: #377bb5"></i></span>
                                            <h5>
                                                <asp:Label ID="lbl_ubicacion_tituloControl" runat="server" Text="Datos de la Solicitud"></asp:Label></h5>
                                            
                                        </div>
                                    </a>
                                </div>

                                <%-- contenido del collapsible ubicaciones --%>
                                <div class="accordion-body collapse in" id="collapse_DatosSolicitud">
                                    <div class="widget-content">
                                        <asp:HiddenField ID="hid_return_url" runat="server" />

                                        <asp:Panel ID="pnlDatosLocal" runat="server">
                        
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label class="control-label col-sm-3">Nro. de Solicitud:</label>
                                                    <div class="col-sm-3 text-left ptop5">
                                                        <asp:Label ID="nro_solicitud" runat="server" Font-Bold="true" ></asp:Label>
                                                    </div>

                                                    <asp:LinkButton ID="btnGuardarVisor" runat="server" CssClass="btn btn-primary mright20 pull-right" OnClick="btnGuardar_Click" Style="display:none"
                                                    >
                                                        <i class="imoon imoon-disk"></i>
                                                        <span class="text">Guardar</span>
                                                </asp:LinkButton>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label col-sm-3">Nro. de Expediente(*):</label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox ID="txtNroExpediente" runat="server" MaxLength="20" Width="140px" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-sm-9">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNroExpediente"
                                                            ErrorMessage="El Nro. de Expediente es requerido." CssClass="field-validation-error" Display="Dynamic"
                                                            SetFocusOnError="true" ValidationGroup="Continuar"></asp:RequiredFieldValidator>

                                                        <asp:CustomValidator ID="CustomValidator3" runat="server" ControlToValidate="txtNroExpediente"
                                                            ClientValidationFunction="validarNroExp"
                                                            CssClass="field-validation-error" Display="Dynamic"
                                                            ErrorMessage="El formato es inválido. Ej: 1111111111/2015"
                                                            ValidationGroup="Continuar"></asp:CustomValidator>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="control-label col-sm-3">Observaciones(*):</label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox ID="txtObservaciones" runat="server" TextMode="multiline" Rows="4" Width="500px" CssClass="form-control"></asp:TextBox>
                                                        
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-sm-3"></div>
                                                    <small class="col-sm-9 text-left">(*) El campo es obligatorio.</small>
                                                         </div>
                                                <div class="col-sm-8">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="field-validation-error" Display="Dynamic" ErrorMessage="El campo es obligatorio." ControlToValidate="txtObservaciones" ValidationGroup="Continuar"></asp:RequiredFieldValidator>
                                                 </div>
    
                                                <div class="text-right">
                                                    <asp:UpdatePanel ID="updContinuar" runat="server">
                                                        <ContentTemplate>
                                                         
                                                            <div  style="padding-top: 10px;">
                                                                <div class="form-inline">
                                                                    <div class="col-sm-12">
                                                                   <asp:Panel ID="pnlBotonesGuardar" runat="server">
                                                                        <div class="form-inline">
                                                                            <asp:LinkButton ID="btnContinuar" runat="server" CssClass="btn btn-lg btn-primary" OnClick="btnContinuar_Click" OnClientClick="return validarGuardar();"
                                                                                ValidationGroup="Continuar">
                                                                                    <i class="imoon imoon-disk"></i>
                                                                                    <span class="text">Guardar y Continuar</span>
                                                                            </asp:LinkButton>
                                                                        </div>
                                                                          </asp:Panel>
                                                                        <div>
                                                                            <asp:UpdateProgress ID="UpdateProgress3" runat="server"
                                                                                AssociatedUpdatePanelID="updContinuar" DisplayAfter="0">
                                                                                <ProgressTemplate>
                                                                                    <label>Procesando</label>
                                                                                    <img src='<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>' alt="" />
                                                                                </ProgressTemplate>
                                                                            </asp:UpdateProgress>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                            
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="clearfix"></div>
                                            </div>


                        

                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            
                    

                            </div>
                        </div>
                </asp:Panel>                            
        </div>



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
         function validarGuardar() {

             if (Page_ClientValidate("Continuar")) {

                 ocultarBotonesGuardado();
             }
         }
         function ocultarBotonesGuardado() {

             $("#<%: pnlBotonesGuardar.ClientID %>").css("display", "none");

             return true;
         }
         </script>

    </ContentTemplate>
</asp:UpdatePanel>

