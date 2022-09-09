<%@ Page Title="Manage Account" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Manage.aspx.cs" Inherits="AnexoProfesionales.Account.Manage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

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
                RenderOuterTable="false" SuccessPageUrl="Manage?m=ChangePwdSuccess" >
                <ChangePasswordTemplate>
                    <p class="validation-summary-errors mtop20">
                        <asp:Literal runat="server" ID="FailureText" />
                    </p>
                    <fieldset class="changePassword">
                        <%--<legend>Cambiar detalles de contraseña</legend>--%>
                        <ol class="list-unstyled">
                            <li>
                                <asp:Label runat="server" ID="CurrentPasswordLabel" AssociatedControlID="CurrentPassword">Contraseña actual</asp:Label>
                                <asp:TextBox runat="server" ID="CurrentPassword" CssClass="passwordEntry form-control" TextMode="Password" Width="300px"  />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="CurrentPassword"
                                    CssClass="alert alert-small alert-danger mbottom0 mtop5" ErrorMessage="El campo de contraseña actual es obligatorio."
                                    ValidationGroup="ChangePassword" />
                            </li>
                            <li>
                                <asp:Label runat="server" ID="NewPasswordLabel" AssociatedControlID="NewPassword">Nueva contraseña</asp:Label>
                                <asp:TextBox runat="server" ID="NewPassword" CssClass="passwordEntry form-control" TextMode="Password" Width="300px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="NewPassword"
                                    CssClass="alert alert-small alert-danger mbottom0 mtop5" ErrorMessage="La contraseña nueva es obligatoria."
                                    ValidationGroup="ChangePassword" />
                            </li>
                            <li>
                                <asp:Label runat="server" ID="ConfirmNewPasswordLabel" AssociatedControlID="ConfirmNewPassword">Confirmar la nueva contraseña</asp:Label>
                                <asp:TextBox runat="server" ID="ConfirmNewPassword" CssClass="passwordEntry form-control" TextMode="Password" Width="300px"  />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ConfirmNewPassword"
                                    CssClass="alert alert-small alert-danger mbottom0 mtop5" Display="Dynamic" ErrorMessage="La confirmación de la nueva contraseña es obligatoria."
                                    ValidationGroup="ChangePassword" />
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword"
                                    CssClass="alert alert-small alert-danger mbottom0 mtop5" Display="Dynamic" ErrorMessage="La nueva contraseña y la contraseña de confirmación no coinciden."
                                    ValidationGroup="ChangePassword" />
                            </li>
                        </ol>
                        <asp:LinkButton ID="Button1" runat="server" CommandName="ChangePassword"  ValidationGroup="ChangePassword" CssClass="btn btn-primary btn-lg mtop10" >
                               <i class="imoon imoon-ok"></i>
                            <span class="text">Cambiar contraseña</span>
                        </asp:LinkButton>
                    </fieldset>
                </ChangePasswordTemplate>
            </asp:ChangePassword>
        </asp:PlaceHolder>
    </section>

    
</asp:Content>
