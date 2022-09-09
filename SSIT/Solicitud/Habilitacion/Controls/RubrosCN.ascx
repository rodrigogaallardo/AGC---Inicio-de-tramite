<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RubrosCN.ascx.cs" Inherits="SSIT.Solicitud.Habilitacion.Controls.RubrosCN" %>

<%--Panel Rubros--%>
<asp:Panel ID="pnlRubros" Class="IbuttonControl" runat="server">
    <div>
        <asp:Panel ID="pnlNormativa" runat="server" Visible="false" HorizontalAlign="Left">
            <div class="mleft5">
                <strong>Normativa aplicada al trámite
                </strong>
            </div>

            <div style="padding-top: 10px; padding-left: 5px">
                <label>Tipo de Normativa:</label>
                <asp:Label ID="lblTipoNormativa" runat="server" Font-Bold="true"></asp:Label>
            </div>
            <div style="padding-top: 5px; padding-left: 5px">
                <label>
                    Entidad Normativa:</label>
                <asp:Label ID="lblEntidadNormativa" runat="server" Font-Bold="true"></asp:Label>
            </div>
            <div style="padding-top: 5px; padding-left: 5px">
                <label>Nro de normativa:</label>
                <asp:Label ID="lblNroNormativa" runat="server" Font-Bold="true"></asp:Label>
            </div>
        </asp:Panel>
        <div class="mtop10 text-left">
            <strong>Listado de rubros</strong>
        </div>

        <asp:GridView ID="grdRubrosIngresados" runat="server" AutoGenerateColumns="false"
            AllowPaging="false" Style="border: none;" CssClass="table table-bordered mtop5"
            GridLines="None" Width="100%">
            <HeaderStyle CssClass="grid-header" />
            <RowStyle CssClass="grid-row" />
            <AlternatingRowStyle BackColor="#efefef" />
            <Columns>
                <asp:BoundField DataField="CodigoRubro" HeaderText="Código" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="DescripcionRubro" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="TipoActividadNombre" HeaderText="Actividad" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="150px" />
                <asp:ImageField DataImageUrlField="RestriccionZona" DataImageUrlFormatString="~/Common/Images/Rubros/{0}"
                    HeaderText="Area Mixtura/Distrit Zonificación" ItemStyle-Width="50px" Visible="false">
                </asp:ImageField>                
                <asp:BoundField DataField="EsAnterior" HeaderText="Anterior" Visible="false" />
                <asp:BoundField DataField="SuperficieHabilitar" HeaderText="Superficie" Visible="true" />
            </Columns>
            <EmptyDataTemplate>
                <div class="titulo-4">
                    No se ingresaron datos
                </div>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:Panel ID="pnlObservacionesRubros" runat="server" Visible="false" HorizontalAlign="Left">
            <div>
                <strong>Observaciones de rubro
                </strong>
            </div>
            <asp:Label ID="lblObservacionesRubros" runat="server" />
        </asp:Panel>
    </div>
</asp:Panel>

