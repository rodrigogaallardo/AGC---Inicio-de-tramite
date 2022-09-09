<%@ Page Title="" Language="C#" MasterPageFile="~/Mailer/Mail.Master" AutoEventWireup="true" CodeBehind="MailTitulares.aspx.cs" Inherits="SSIT.Mailer.MailTitulares" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:FormView ID="FormView1" runat="server" ItemType="StaticClass.MailAnulacionAnexo" SelectMethod="GetData">
        <ItemTemplate>


            <div style="margin: 20px">
               
                <p><%# Item.Renglon1 %></p>

                <p><%# Item.Renglon2 %></p>
                <p><%# Item.Renglon3 %></p>
            </div>

        </ItemTemplate>

    </asp:FormView>
</asp:Content>
