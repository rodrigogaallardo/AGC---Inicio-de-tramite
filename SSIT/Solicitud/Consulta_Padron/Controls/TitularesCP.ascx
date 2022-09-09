<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TitularesCP.ascx.cs" Inherits="SSIT.Solicitud.Controls.Titulares" %>

<%--Datos Titulares--%>
<asp:Panel ID="pnlTitulares" runat="server">
    <div  style="margin:20px">
        <%--Grilla de Titulares--%>
        <div>
            <strong>Titulares</strong>
        </div>
        <div>
            <asp:GridView ID="grdTitularesHab" runat="server" AutoGenerateColumns="false" DataKeyNames="id_persona"
                AllowPaging="false" Style="border: none;" GridLines="None" Width="100%" CssClass="table table-bordered mtop5"
                CellPadding="3">
                <HeaderStyle CssClass="grid-header" />
                <AlternatingRowStyle BackColor="#efefef" />
                <Columns>
                    <asp:BoundField DataField="TipoPersonaDesc" HeaderText="Tipo" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                    <asp:TemplateField HeaderText="Apellido y Nombre / Razon Social" HeaderStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lblgrdTitName" runat="server" Width="100%" style="text-align:center;word-wrap: normal; word-break: break-all;" Text='<%# Eval("ApellidoNomRazon") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Cuit" HeaderText="CUIT" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                    <asp:BoundField DataField="Domicilio" HeaderText="Domicilio Legal" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"/>
                </Columns>
                <EmptyDataTemplate>
                    <div class="mtop10">
                         <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                        No hay datos aún...
                    </div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
        
     
    </div>
</asp:Panel>
