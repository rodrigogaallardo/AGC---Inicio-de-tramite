<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Rubros.ascx.cs" Inherits="SSIT.Solicitud.Consulta_Padron.Controls.Rubros" %>
<%--Panel Rubros--%>
<asp:Panel ID="pnlRubros" Class="IbuttonControl" runat="server">
    <div style="margin:20px">
        <asp:Panel ID="pnlNormativa" runat="server" Visible="false">
            <div class="mleft5">
                <strong>Normativa aplicada al trámite</strong>
            </div>
            <div style="padding-top: 10px; padding-left: 5px">
                <label>Tipo de Normativa:</label>
                <asp:Label ID="lblTipoNormativa" runat="server" Font-Bold="true"></asp:Label>
            </div>
            <div style="padding-top: 5px; padding-left: 5px">
                <label>Entidad Normativa:</label>
                <asp:Label ID="lblEntidadNormativa" runat="server" Font-Bold="true"></asp:Label>
            </div>
            <div style="padding-top: 5px; padding-left: 5px">
                <label>Nro de normativa:</label>
                <asp:Label ID="lblNroNormativa" runat="server" Font-Bold="true"></asp:Label>
            </div>
        </asp:Panel>
        <div class="mtop10" >
            <strong>Listado de rubros</strong>
        </div>
        <asp:GridView 
            ID="grdRubrosIngresados" 
            runat="server" 
            AutoGenerateColumns="false"
            AllowPaging="false" 
            Style="border: none;" 
            CssClass="table table-bordered mtop5"
            GridLines="None" 
            Width="100%"
            OnDataBound="grdRubrosIngresados_DataBound"
            >
            <HeaderStyle CssClass="grid-header" />
            <RowStyle CssClass="grid-row" />
            <AlternatingRowStyle BackColor="#efefef" />
            <Columns>
                <asp:BoundField DataField="CodidoRubro" HeaderText="Código" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="DescripcionRubro" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="TipoActividadNombre" HeaderText="Actividad" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="150px" />
                <asp:ImageField DataImageUrlField="RestriccionZona" DataImageUrlFormatString="~/Common/Images/Rubros/{0}" HeaderText="Zona" ItemStyle-Width="50px"></asp:ImageField>
                <asp:ImageField DataImageUrlField="RestriccionSup" DataImageUrlFormatString="~/Common/Images/Rubros/{0}" HeaderText="Sup" ItemStyle-Width="50px"></asp:ImageField>
                <asp:BoundField DataField="EsAnterior" HeaderText="Anterior" Visible="false" />
                <asp:BoundField DataField="SuperficieHabilitar" HeaderText="Superficie" Visible="true" />     
            </Columns>
            <EmptyDataTemplate>
 
                     <div class="mtop10">
                    <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' />
                    <span class="mleft10">No se encontraron registros.</span>
                 </div>
             
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Panel>
