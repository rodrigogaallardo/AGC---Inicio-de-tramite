<%@ Page Title="" Language="C#" MasterPageFile="~/Mailer/Mail.Master" AutoEventWireup="true" CodeBehind="MailSolicitudNueva.aspx.cs" Inherits="SSIT.Mailer.MailSolicitudNueva" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <asp:FormView ID="FormView1" runat="server" ItemType="StaticClass.MailSolicitudNueva" SelectMethod="GetData">
        <ItemTemplate>

            <div style="margin-left: 20px; margin-top:-20px">

                <h3>Sr. Contribuyente,</h3>

                <p>Su solicitud de habilitación ha sido confirmada.</p>
                <p>La misma se identificará con los siguientes datos:</p>
                <br />
                <p>Número de trámite: <%# Item.id_solicitud %></p>
                <p>Código de seguridad: <%# Item.codigo_seguridad %></p>
                <br />
                <p>El número de tramite será la identificación única del mismo hasta tanto se finalice la gestión</p>
                <p>El código de seguridad es único para cada trámite y es la identificación que se le deberá otorgar al profesional que usted seleccione para la confección de la documentación a presentar, a través del cual el profesional interviniente tendrá acceso en el sistema de solicitudes para completar sus tareas y adicionar documentación (Anexo técnico).</p>
                <p>A partir de ahora, el profesional interviniente podrá confeccionar el Anexo Técnico.</p>
                <br />
            </div>

        </ItemTemplate>

    </asp:FormView>
</asp:Content>
