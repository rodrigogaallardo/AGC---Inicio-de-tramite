<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="error3006.aspx.cs" Inherits="SSIT.Errores.error3006" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="text-center">
        <h2>No se ha podido obtener el PDF de la boleta en el servicio.</h2>

        <div class="mtop20">
            <asp:LinkButton ID="btnHome" runat="server" CssClass="btn btn-default" PostBackUrl="~/">
                <i></i>
                <span class="text">Inicio</span>
            </asp:LinkButton>
        </div>
    </div>
</asp:Content>
