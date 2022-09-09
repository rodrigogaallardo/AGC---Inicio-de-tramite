<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="AnexoProfesionales.Account.Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
    <%: Scripts.Render("~/bundles/autoNumeric") %>
<asp:UpdatePanel ID="UpdModificarDatos" runat="server" UpdateMode="Conditional">
<ContentTemplate>                
    <asp:Panel ID="pnlModificarDatos" runat="server" CssClass="form-horizontal">
        <h2>Datos del usuario</h2>
        <label class="mtop20">Desde aqu&iacute; podr&aacute; ver los datos de usuario.</label>

        <div style="color: red; padding: 10px 0px 10px 0px">
            <div class="field-validation-error">
                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
            </div>
        </div>
        <fieldset>
            <div class="form-horizontal">
                <div class="form-group">
                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" CssClass="col-sm-3 col-sm-3 control-label">Nombre de Usuario (*):</asp:Label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="UserName" runat="server" MaxLength="50" Width="150px" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email" CssClass="col-sm-3 control-label">E-mail:</asp:Label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="Email" runat="server" MaxLength="50" Width="250px" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group" id="divDni">
                    <asp:Label ID="lblDni" runat="server" AssociatedControlID="txtDni" CssClass="col-sm-3 control-label">D.N.I:</asp:Label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="txtDni" runat="server" MaxLength="10" Width="100px" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group" id="divcUIT">
                    <asp:Label ID="Label1" runat="server" AssociatedControlID="txtDni" CssClass="col-sm-3 control-label">C.U.T.I.:</asp:Label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="Cuit" runat="server" MaxLength="10" Width="100px" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>                
                <div class="form-group">
                    <asp:Label ID="ApellidoLabel" runat="server" AssociatedControlID="Apellido" CssClass="col-sm-3 control-label">Apellido:</asp:Label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="Apellido" runat="server" MaxLength="50" Width="200px" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="NombreLabel" runat="server" AssociatedControlID="Nombre" CssClass="col-sm-3 control-label">Nombre/s:</asp:Label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="Nombre" runat="server" MaxLength="50" Width="350px" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                 <div class="form-group">
                    <asp:Label ID="Label2" runat="server" AssociatedControlID="Nombre" CssClass="col-sm-3 control-label">Matricula:</asp:Label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="Matricula" runat="server" MaxLength="50" Width="350px" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label">Calle:</label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="Calle" runat="server" MaxLength="50" Width="250px" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label">N&uacute;mero:</label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="NroPuerta" runat="server" Width="100px" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label">Piso:</label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="Piso" runat="server" MaxLength="5" Width="150px" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label"> Depto/UF:</label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="Depto" runat="server" MaxLength="5" Width="150px" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label">Provincia:</label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="Provincia" runat="server" MaxLength="10" Width="150px" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label">Localidad:</label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="Localidad" runat="server" MaxLength="10" Width="150px" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label">Tel&eacute;fono:</label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="Telefono" runat="server" MaxLength="50" Width="300px" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>
        </fieldset>
    </asp:Panel>
   </ContentTemplate>
  </asp:UpdatePanel>

        <%--Popup de Error--%>
    <asp:Panel ID="pnlError" runat="server" CssClass="modalPopup" Style="display: none"
        Width="450px" DefaultButton="btnAceptarError">
        <table style="width: 100%; border-collapse:separate; border-spacing:7px">
            <tr>
                <td style="width: 80px">
                    <asp:Image ID="imgmpeInfo" runat="server" ImageUrl="~/Common/Images/Controles/error64x64.png" />
                </td>
                <td>
                    <asp:UpdatePanel ID="updmpeInfo" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblError" runat="server" CssClass="error-label"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="btnAceptarError" runat="server" CssClass="btnOK" Text="Aceptar" Width="100px" />
                </td>
            </tr>
        </table>
    </asp:Panel>


   <script type="text/javascript">
    </script>

</asp:Content>

