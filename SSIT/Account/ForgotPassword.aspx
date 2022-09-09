<%@ Page Title="Recupero de Contraseña" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="SSIT.Account.ForgotPassword" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlContent" runat="server" DefaultButton="SubmitButton" CssClass="container-fluid">
                <div class="form-horizontal">
                    <div class="form-group">
                        <div class="col-xs-9">
                            <h2>¿Olvido su contrase&ntilde;a?</h2>
                            <label class="col-md-9 col-xs-12">Ingrese el nombre de usuario, presione el bot&oacute;n y recibir&aacute; su contrase&ntilde;a por correo electr&oacute;nico.</label>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-xs-9">
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" CssClass="col-md-3 col-sm-3 col-xs-12 control-label">Nombre de usuario:</asp:Label>
                            <div class="col-md-9 col-sm-3 col-xs-12">
                                <asp:TextBox ID="UserName" runat="server" CssClass="form-control" placeholder="Usuario"></asp:TextBox>
                            </div>
                        </div>
                </div>
                <div class="form-group">
                        <div class="col-xs-9">
                            <asp:LinkButton ID="SubmitButton" runat="server" CssClass="btn btn-primary" CommandName="Submit" ValidationGroup="PasswordRecovery1"
                                Style="margin-left: 10px"
                                OnClick="PasswordRecovery_VerifyingUser">
                            <i class="imoon-envelope"></i>
                            <span class="text">Enviar correo</span>
                            </asp:LinkButton>
                            <div>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" CssClass="field-validation-error"
                                    ErrorMessage="El nombre de usuario es requerido." ValidationGroup="PasswordRecovery1"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                </div>
                </div>
                <p class="validation-summary-errors">
                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                </p>
                <p class="validation-summary-errors">
                    <asp:Label ID="lblError" runat="server"></asp:Label>
                </p>
            </asp:Panel>
            <asp:Panel runat="server" ID="SuccessEmail" Visible="false">
                <div style="padding: 15px;">
                    <div class="alert alert-info">
                        Se le ha enviado la contraseña al e-mail
                    <asp:Label ID="lblEmail" runat="server" ForeColor="Blue"></asp:Label>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="ErrorUsuario" Visible="false">
                <div style="padding: 15px;">
                    <div class="alert alert-danger">
                        No fue posible tener acceso a su información. Inténtelo nuevamente.
                    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
