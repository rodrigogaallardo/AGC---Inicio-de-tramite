<%@ Page Title="Manage Account" Language="C#" UICulture="es-MX" Culture="es-MX" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="SSIT.Account.Manage" %>
<%@ Import Namespace="System.Threading" %>
<%@ Import Namespace="System.Globalization" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">


    <section id="passwordForm">
        
        <div class="mtop15">
            <asp:PlaceHolder runat="server" ID="successMessage" Visible="false" ViewStateMode="Disabled" >
                <p class="message-success"><%: SuccessMessage %></p>
            </asp:PlaceHolder>

            <p>Inició sesión como <strong><%: User.Identity.Name %></strong>.</p>
           
        </div>
        
        <h2>Cambiar contraseña</h2>

        <asp:PlaceHolder runat="server" ID="changePassword" Visible="false">
            
            <asp:ChangePassword ID="changepwd" runat="server" CancelDestinationPageUrl="~/" ViewStateMode="Disabled"
                RenderOuterTable="false" SuccessPageUrl="Manage?m=ChangePwdSuccess" 
                ChangePasswordFailureText="Contraseña incorrecta o nueva contraseña no válida. Longitud mínima de la nueva contraseña es: {0}. Se requieren caracteres no alfanuméricos: {1}."  >
                <ChangePasswordTemplate>
                    <p class="validation-summary-errors mtop20">
                        <asp:Label runat="server" ID="FailureText" />
                    </p>
                    <fieldset class="changePassword">
                        <%--<legend>Cambiar detalles de contraseña</legend>--%>
                        <ol class="list-unstyled">
                            <li>
                                <asp:Label runat="server" ID="CurrentPasswordLabel" AssociatedControlID="CurrentPassword">Contraseña actual</asp:Label>
                                <asp:TextBox runat="server" ID="CurrentPassword" CssClass="passwordEntry form-control" TextMode="Password" Width="300px"  />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="CurrentPassword"
                                    CssClass="field-validation-error" ErrorMessage="El campo de contraseña actual es obligatorio."
                                    ValidationGroup="ChangePassword" />
                            </li>
                            <li>
                                <asp:Label runat="server" ID="NewPasswordLabel" AssociatedControlID="NewPassword">Nueva contraseña</asp:Label>
                                <asp:TextBox runat="server" ID="NewPassword" CssClass="passwordEntry form-control" TextMode="Password" Width="300px" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="NewPassword"
                                    CssClass="field-validation-error" ErrorMessage="La contraseña nueva es obligatoria."
                                    ValidationGroup="ChangePassword" />
                            </li>
                            <li>
                                <asp:Label runat="server" ID="ConfirmNewPasswordLabel" AssociatedControlID="ConfirmNewPassword">Confirmar la nueva contraseña</asp:Label>
                                <asp:TextBox runat="server" ID="ConfirmNewPassword" CssClass="passwordEntry form-control" TextMode="Password" Width="300px"  />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmNewPassword"
                                    CssClass="field-validation-error" Display="Dynamic" ErrorMessage="La confirmación de la nueva contraseña es obligatoria."
                                    ValidationGroup="ChangePassword" />
                                <asp:CompareValidator runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword"
                                    CssClass="field-validation-error" Display="Dynamic" ErrorMessage="La nueva contraseña y la contraseña de confirmación no coinciden."
                                    ValidationGroup="ChangePassword" />
                            </li>
                        </ol>
                        <asp:LinkButton runat="server" CommandName="ChangePassword"  ValidationGroup="ChangePassword" CssClass="btn btn-primary btn-lg mtop10" >
                            <i class="imoon imoon-ok"></i>
                            <span class="text">Cambiar contraseña</span>
                        </asp:LinkButton>
                    </fieldset>
                </ChangePasswordTemplate>
            </asp:ChangePassword>
        </asp:PlaceHolder>
    </section>

    
</asp:Content>