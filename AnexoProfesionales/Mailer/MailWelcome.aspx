<%@ Page Title="Activación de usuario" Language="C#" MasterPageFile="~/Mailer/Mail.Master" AutoEventWireup="true" CodeBehind="MailWelcome.aspx.cs" Inherits="AnexoProfesionales.Mailer.MailWelcome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:FormView ID="FormView1" runat="server" ItemType="StaticClass.MailWelcome" SelectMethod="GetData">
        <ItemTemplate>

            <div style="padding:20px">
            
                
                <h4><em><%# Item.Renglon1 %></em></h4>
                <h4><em><%# Item.Renglon2 %></em></h4>
                <h4><em><%# Item.Renglon3 %></em></h4>
                <h4><em style="color: green"><%# Item.Urlactivacion %></em></h4>


                <h2>Datos de acceso</h2>
                <h3>Usuario: <%# Item.Username  %></h3>
                <h3>Password: <%# Item.Password %></h3>
                
                
                <a href="<%# Item.Urlactivacion %>" target="_blank" ><em>Activar usuario</em></a>
                

            </div>

        </ItemTemplate>

    </asp:FormView>

</asp:Content>

