<%@ Page Title="Visualizar Titulares" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VisorTitulares.aspx.cs" Inherits="AnexoProfesionales.Tramites.Habilitacion.VisorTitulares" %>

<%@ Register Src="~/Tramites/Habilitacion/Controls/ucVisorTitulares.ascx" TagPrefix="uc1" TagName="ucVisorTitulares" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<uc1:ucVisorTitulares runat="server" id="ucVisorTitulares" />
</asp:Content>
