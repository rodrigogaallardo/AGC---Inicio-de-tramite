﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="error3005.aspx.cs" Inherits="SSIT.Errores.error3005" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="text-center">
        <h2>No se ha enviado el par&aacute;metro necesario para esta p&aacute;gina.</h2>

        <div class="mtop20">
            <asp:LinkButton ID="btnHome" runat="server" CssClass="btn btn-default" PostBackUrl="~/">
                <i></i>
                <span class="text">Inicio</span>
            </asp:LinkButton>
        </div>
    </div>

</asp:Content>
