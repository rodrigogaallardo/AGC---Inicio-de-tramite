<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DatosSolicitud.aspx.cs" MasterPageFile="~/Site.Master" Inherits="SSIT.Solicitud.Consulta_Padron.DatosSolicitud" %>
<%@ Register Src="~/Solicitud/Consulta_Padron/Controls/ucDatosSolicitud.ascx" TagPrefix="uc" TagName="ucDatosSolicitud" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <uc:ucDatosSolicitud runat="server" ID="visDatosSolicitud"/>                    
</asp:Content>
