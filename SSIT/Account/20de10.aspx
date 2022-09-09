<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="20de10.aspx.cs" Inherits="SSIT.Account._20de10" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-wrapper main-content clear-fix ptop20">
            <div class="form-horizontal mtop20">
                <div class="form-group">
                    <label for="txt" class="col-sm-4 control-label">Usuario: </label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtUsername" runat="server" Width="120px" CssClass="form-control inline"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername"
                            CssClass="field-validation-error" Display="Dynamic"
                            SetFocusOnError="True">Debe ingresar el Nombre de Usuario.</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="form-group">
                    <label for="password" class="col-sm-4 control-label">Contraseña:</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="50" placeholder="Contraseña" CssClass="form-control inline" Width="120px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                            CssClass="field-validation-error" Display="Dynamic"
                            SetFocusOnError="True">Debe ingresar la Contraseña.</asp:RequiredFieldValidator>
                        
                        <br />
                        <br />
                        <asp:LinkButton ID="lnk" runat="server" OnClick="lnk_Click" CssClass="btn btn-default" Text="Ingresar"></asp:LinkButton>                
                    </div>

                </div>


            </div>






    </section>
</asp:Content>
