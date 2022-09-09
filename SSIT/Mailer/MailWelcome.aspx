<%@ Page Title="Información de acceso al sistema SSIT" Language="C#" MasterPageFile="~/Mailer/Mail.Master" AutoEventWireup="true" CodeBehind="MailWelcome.aspx.cs" Inherits="SSIT.Mailer.MailWelcome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:FormView ID="FormView1" runat="server" ItemType="StaticClass.MailWelcome" SelectMethod="GetData">
        <ItemTemplate>
            <p>Estimada/o <b><%# Item.NombreApellido  %></b></p>
            <br /><br /><br />
            
            <p>Bienvenida/o al <b>Sistema De Solicitud de Inicio de Trámite</b> de la Agencia Gubernamental de Control (AGC)</p>
            <div style="margin: 20px">
                <br /><br />
             <p>A continuación detallamos la información que Usted necesitará para utilizar el sistema:<p>
                  <br /><br />
             <b>Acceso al Sistema SSIT</b>
              <p>Dirección web del Sistema:</p>
                  <br /><br />
               <h3>Usuario: <%# Item.Username  %></h3>
                  <br />
               <h3>Password: <%# Item.Password %></h3>
                  <br /><br /><br />
                

                <b>Operatoria General</b><br />
                <p>Este mensaje, al igual que las demás notificaciones que Usted reciba de ahora en más desde el Sistema XXXX, serán enviadas
                automáticamente por el sistema. No responda esos mensajes.
                <p>Si quiere acceder al manual de usuario puede hacer clic en el siguiente link: http://XXXXXXX </p>
                    <br /> <br />   <br />
                
                
                 
                <h4><em><b><%# Item.Renglon1 %></b></em></h4>
                <h4><em><b><%# Item.Renglon2 %></b></em></h4>
                <h4><em><b><%# Item.Renglon3 %></b></em></h4>
                <a href="<%# Item.Urlactivacion %>" target="_blank" ><em>ACTIVAR USUARIO</em></a>
                <br /><br /><br /><br />


                <p> Cordialmente,</p> <br />
                 <b>Agencia Gubernamental de Control</b>
            </div>

        </ItemTemplate>

    </asp:FormView>

</asp:Content>

