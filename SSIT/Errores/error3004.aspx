<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="error3004.aspx.cs" Inherits="SSIT.Errores.error3004" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">


    <div class="text-center">
        <h2>La solicitud ingresada no se encuentra en la base de datos.</h2>

        <div class="mtop20">
            <asp:LinkButton ID="btnHome" runat="server" CssClass="btn btn-default" PostBackUrl="~/">
                <i></i>
                <span class="text">Inicio</span>
            </asp:LinkButton>
        </div>
    </div>

</asp:Content>
