<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Titulares.ascx.cs" Inherits="SSIT.Transferencia.Controls.Titulares" %>

<%--Datos Titulares--%>
<asp:Panel ID="pnlTitulares" runat="server">
    <div style="margin:20px">
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
                    <asp:BoundField DataField="TipoPersonaDesc" HeaderText="Tipo" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                    <asp:TemplateField HeaderText="Apellido y Nombre / Razon Social" HeaderStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lblgrdTitName" runat="server" Width="100%" style="text-align:center;word-wrap: normal; word-break: break-all;" Text='<%# Eval("ApellidoNomRazon") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Cuit" HeaderText="CUIT" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                    <asp:BoundField DataField="Domicilio" HeaderText="Domicilio Legal" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                </Columns>
                <EmptyDataTemplate>
                     <div class="mtop10">
                         <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                        No hay datos aún...
                    </div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
        
        
            
        <%--Firmantes--%>
       <div id="panelGrillaTransf" runat="server">
        <div>
            <strong>Firmantes</strong>
        </div>
        <%--Grilla de Firmantes--%>
  
            <asp:GridView ID="grdTitularesTra" runat="server" AutoGenerateColumns="false" DataKeyNames="id_firmante"
                AllowPaging="false" Style="border: none; margin-top: 10px" GridLines="None" Width="100%" CssClass="table table-bordered mtop5"
                CellPadding="3">
                <HeaderStyle CssClass="grid-header" />
                <AlternatingRowStyle BackColor="#efefef" />
                <Columns>
                    <asp:TemplateField HeaderText="Firmante de..." HeaderStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lblgrdTitName" runat="server" Width="100%" style="text-align:center;word-wrap: normal; word-break: break-all;" Text='<%# Eval("Titular") %>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ApellidoNombres" HeaderText="Apellido y Nombre/s" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                    <asp:BoundField DataField="DescTipoDocPersonal" HeaderText="Tipo" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                    <asp:BoundField DataField="Nro_Documento" HeaderText="Documento" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                    <asp:BoundField DataField="nom_tipocaracter" HeaderText="Carácter Legal" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                    <asp:BoundField DataField="cargo_firmante_pj" HeaderText="Cargo" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
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
