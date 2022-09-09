<%@ Page Title="Recupero de contraseña" Language="C#" MasterPageFile="~/Mailer/Mail.Master" AutoEventWireup="true" CodeBehind="MailPassRecovery.aspx.cs" Inherits="SSIT.Mailer.MailPassRecovery" %>

<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <asp:FormView ID="FormView1" runat="server" ItemType="StaticClass.MailPassRecovery" SelectMethod="GetData">
        <ItemTemplate>


            <div style="margin: 20px">


                <h4><em><%# Item.Renglon1 %></em></h4>
                <h4><em><%# Item.Renglon2 %></em></h4>
                <h4><em><%# Item.Renglon3 %></em></h4>
                <h4><em style="color: green"><%# Item.UrlLogin %></em></h4>


                <h2>Datos de acceso</h2>
                <h3>Usuario: <%# Item.Username  %></h3>
                <h3>Password: <%# Item.Password %></h3>


                <a href="<%# Item.UrlLogin %>" target="_blank">Iniciar sesi&oacute;n</a>

            </div>

        </ItemTemplate>

    </asp:FormView>

</asp:content>

