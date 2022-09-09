<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AuthenticationAGIP.Default" MasterPageFile="~/Site.Master" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <div>
        <asp:Literal ID="lit" runat="server"></asp:Literal>
            
        <asp:TextBox ID="txtdatos" runat="server" Width="1000px" Height="500px" TextMode="MultiLine"></asp:TextBox>
    </div>

</asp:Content>