<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConversionUsuarios.aspx.cs" Inherits="SSIT.Solicitud.ConversionUsuarios" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div id="page_content" >    

        <asp:UpdatePanel ID="updPagina" runat="server">
            <ContentTemplate>

                <asp:Panel ID="pnlLogin" runat="server" DefaultButton="btnLogin">

                    <div class="form-horizontal">

                        <div class="form-group">
                            <label class="control-label col-sm-3">Usuario:</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="txtUsuAnterior" runat="server" Width="150px" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="control-label col-sm-3">Contrase&ntilde;a:</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="txtPassAnterior" runat="server" Width="150px" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-sm-9 col-sm-offset-3">
                                <asp:LinkButton ID="btnLogin" runat="server" CssClass="btn btn-primary" OnClick="btnLogin_Click">
                                    <span class="text">Obtener tr&aacute;mites</span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-9 col-sm-offset-3">
                                <asp:HyperLink ID="lnkPasswordRecovery" runat="server" Style="color: #808080" NavigateUrl="~/Account/ForgotPassword">¿Olvidaste tu contraseña?</asp:HyperLink>
                                <div class="field-validation-error" style="text-align: center;">
                                    <div class="field-validation-error">
                                        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlError" runat="server" CssClass="alert alert-danger" Visible="false">
                    <asp:Label ID="lblError" runat="server"></asp:Label>
                </asp:Panel>

                <asp:Panel ID="pnlSuccess" runat="server" CssClass="alert alert-success" Visible="false">
                    <asp:Label ID="lblSucess" runat="server">
                        Se han transferido los tr&aacute;mites del usuario anterior al actual. El usuario anterior ha sido eliminado. <br />
                        Dirijase a la <b><a href="<%: this.ResolveUrl("~/Solicitud/Bandeja") %>">Bandeja de Tr&aacute;mites</a></b> para poder verlos.
                    </asp:Label>
                </asp:Panel>


            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


    <%--Modal Confirmar Asociar usuario--%>
    <div id="frmConfirmarAsociarUsuario" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">Obtener Tr&aacute;mites</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon-remove-circle fs64 color-blue"></i>
                            </td>
                            <td style="vertical-align: middle">
                                <label class="mleft10">¿Est&aacute; seguro de asociar los tr&aacute;mites del usuario anterior al actual?</label>
                            </td>
                        </tr>
                    </table>

                </div>
                <div class="modal-footer">

                    <asp:UpdatePanel ID="updConfirmar" runat="server">
                        <ContentTemplate>

                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updConfirmar">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div id="pnlBotonesConfirmacion" class="form-group">
                                    <asp:Button ID="btnConfirmacion_SI" runat="server" CssClass="btn btn-primary" Text="Sí" OnClick="btnConfirmacion_SI_Click" 
                                        OnClientClick="ocultarBotonesConfirmacion();"  />
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

        function showfrmConfirmarAsociarUsuario() {

            $("#frmConfirmarAsociarUsuario").modal("show");
            return false;

        };
        function hidefrmConfirmarAsociarUsuario() {

            $("#frmConfirmarAsociarUsuario").modal("hide");
            return false;

        };
        function ocultarBotonesConfirmacion() {

            $("#pnlBotonesConfirmacion").hide();
            return true;

        };


    </script>
    
</asp:Content>
