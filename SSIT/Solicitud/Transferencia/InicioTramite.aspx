<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InicioTramite.aspx.cs"  MasterPageFile="~/Site.Master" Inherits="SSIT.Solicitud.Transferencia.InicioTramite" %>


   
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

           <%: Scripts.Render("~/bundles/autoNumeric") %>

       
        <div class="content_sitio">
            <div id="content_sitio">


                <asp:Panel ID="pnlTramite" runat="server" Width="100%" DefaultButton="lnkContinuar" >
                    <%-- collapsible ubicaciones--%>
                    <div id="box_ubicacion" class="accordion-group widget-box" Style="background:#ffffff">

                        <%-- titulo collapsible ubicaciones--%>
                        <div class="accordion-heading">
                            <a id="ubicacion_btnUpDown" data-parent="#collapse-group" href="#collapse_ubicacion" data-toggle="collapse">

                                <div class="widget-title">
                                    <span class="icon"><i class="imoon imoon-list"  style="color: #377bb5"></i></span>
                                    <h5>
                                        <asp:Label ID="lbl_tituloControl" runat="server" Text="Datos de la Consulta Padrón"></asp:Label></h5>
                                            
                                </div>
                            </a>
                        </div>
                 <div class="accordion-body collapse in" id="collapse_ubicacion">
                    <div class="widget-content">
                        <div >
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <asp:label ID="Label1" runat="server" Text="Nro. de trámite de la Consulta al Padrón:" CssClass="control-label col-sm-5"></asp:label>

                                    <asp:TextBox ID="txtNroEncomienda" CssClass="form-control" runat="server" MaxLength="7" Width="80px" TabIndex="1"></asp:TextBox>
                                    <div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNroEncomienda" Display="Dynamic"
                                            ErrorMessage="El Nro. de Consulta al Padrón es requerido." CssClass="alert alert-danger" ValidationGroup="Continuar"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                            </div>                      
                         
                             <div class="form-horizontal">
                                <div class="form-group">

                                    <asp:label runat="server" Text="Código de Seguridad de la Consulta al Padrón:" CssClass="control-label col-sm-5"></asp:label>

                                    <asp:TextBox ID="txtCodigoSeguridad" CssClass="form-control" runat="server" MaxLength="4" Width="80px" Style="text-transform: uppercase" TabIndex="2"></asp:TextBox>
                                    <div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCodigoSeguridad"
                                            Display="Dynamic" ErrorMessage="El Código de Seguridad es requerido." CssClass="error-label"
                                            ValidationGroup="Continuar"></asp:RequiredFieldValidator>
                                    </div>

                               </div>

                            </div>  
                            <div class="pull-right" style="margin-top:-80px; margin-right:80px">
                                <asp:UpdatePanel ID="updContinuar" runat="server" RenderMode="Inline">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="pnlBotonesGuardar">
                                    <div style="padding-left: 25px">
                                        <asp:LinkButton ID="lnkContinuar" runat="server" CssClass="btn btn-primary btn-lg" ValidationGroup="Continuar" OnClick="lnkContinuar_Click" TabIndex="3" OnClientClick="return validarGuardar();">                                                          
                                            <span class="text">Continuar</span>
                                                <i class="imoon imoon-chevron-right" ></i>
                                        </asp:LinkButton>
                                    </div>
                                   </asp:Panel>
                                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="updContinuar" runat="server"
                                        DisplayAfter="0">
                                        <ProgressTemplate>
                                            <table border="0">
                                                <tr>
                                                    <td>
                                                        <asp:Image ID="impProgress" runat="server" ImageUrl="~/Content/Img/app/Loading24x24.gif" /></td>
                                                    <td>Generando Solicitud...</td>
                                                </tr>
                                            </table>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                                </div>
                    </div>

                    </div>
                 </div>
                </div>
                </asp:Panel>


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

        camposAutonumericos();
    });

    function camposAutonumericos() {
        $('#<%: txtNroEncomienda.ClientID %> ').autoNumeric({ aSep: '', mDec: '0', vMax: '9999999' });
        return false;
    }

        function showfrmError() {

            $("#frmError").modal("show");
            return false;

        }

          

            function validarGuardar() {
        
                if (Page_ClientValidate("Continuar")) {

                    ocultarBotonesGuardado();
                }
            }
        function ocultarBotonesGuardado() {

            $("#<%: pnlBotonesGuardar.ClientID %>").css("display","none");

             return true;
         }
  </script>

</asp:Content>