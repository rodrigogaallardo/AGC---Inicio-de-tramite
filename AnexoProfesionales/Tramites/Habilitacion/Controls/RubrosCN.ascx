<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RubrosCN.ascx.cs" Inherits="AnexoProfesionales.Controls.RubrosCN" %>

<%--Panel Rubros--%>
<asp:Panel ID="pnlRubros" Class="IbuttonControl" runat="server">
    <div style="margin: 20px">
        <asp:Panel ID="pnlNormativa" runat="server" Visible="false">
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
        <div class="form-group">
            <div id="pnlchkCumpleArticulo521" runat="server" style="display: none;" class="col-sm-10">
                <asp:CheckBox ID="chkCumpleArticulo521" runat="server" Text="¿Declara que cumple con el artículo 5.2.1 inc “c” o “d” del cpu?" Enabled="false" />
            </div>
        </div>
        <div class="form-group">
            <div id="pnlchkOficinaComercial" runat="server" style="display: none;" class="col-sm-8">
                <asp:CheckBox ID="chkOficinaComercial" runat="server" Enabled="false" />
            </div>
        </div>
        <div class="mtop10">
            <strong>Listado de Rubros</strong>
        </div>
        <asp:GridView ID="grdRubrosIngresados" runat="server" AutoGenerateColumns="false"
            AllowPaging="false" Style="border: none;" CssClass="table table-bordered mtop5"
            GridLines="None" Width="100%">
            <HeaderStyle CssClass="grid-header" />
            <RowStyle CssClass="grid-row" />
            <AlternatingRowStyle BackColor="#efefef" />
            <Columns>
                <asp:BoundField DataField="CodigoRubro" HeaderText="Código" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="DescripcionRubro" HeaderText="Descripción" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                <asp:BoundField DataField="TipoActividadNombre" HeaderText="Actividad" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="150px" />
                <asp:ImageField DataImageUrlField="RestriccionZona" DataImageUrlFormatString="~/Common/Images/Rubros/{0}"
                    HeaderText="Area Mixtura/Distrito Zonificación" ItemStyle-Width="50px" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                </asp:ImageField>
                <asp:BoundField DataField="EsAnterior" HeaderText="Anterior" Visible="false" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                <asp:BoundField DataField="SuperficieHabilitar" HeaderText="Superficie" Visible="true" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
            </Columns>
            <EmptyDataTemplate>
                <div class="mtop10table table-bordered mtop5">
                    <div class="mtop10 mbottom10 mleft10">
                        <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' />
                        <span class="mleft10">No se encontraron registros.</span>
                    </div>
                </div>
            </EmptyDataTemplate>
        </asp:GridView>

        <asp:Panel ID="pnlSubRubros" runat="server" Visible="false">
            <div class="mtop10">
                <strong>Listado de Subrubros</strong>
            </div>
            <asp:GridView ID="grdSubRubros" runat="server" AutoGenerateColumns="false"
                AllowPaging="false" Style="border: none;" CssClass="table table-bordered mtop5"
                GridLines="None" Width="100% ">
                <HeaderStyle CssClass="grid-header" />
                <RowStyle CssClass="grid-row" />
                <AlternatingRowStyle BackColor="#efefef" />
                <Columns>
                    <asp:BoundField DataField="RubrosDTO.Codigo" HeaderText="Rubro" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                    <asp:BoundField DataField="Nombre" HeaderText="Descripción" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                </Columns>
                <EmptyDataTemplate>

                    <div class="mtop10">

                        <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                        <span class="mleft10">No se encontraron registros.</span>

                    </div>

                </EmptyDataTemplate>
            </asp:GridView>

        </asp:Panel>

        <asp:Panel ID="pnlDepositos" runat="server" Visible="false">
            <div class="mtop10">
                <strong>Listado de Depósitos</strong>
            </div>
            <asp:GridView
                ID="grdDepositos"
                runat="server"
                AutoGenerateColumns="false"
                AllowPaging="false"
                Style="border: none;"
                CssClass="table table-bordered mtop5"
                GridLines="None"
                Width="100% ">
                <HeaderStyle CssClass="grid-header" />
                <RowStyle CssClass="grid-row" />
                <AlternatingRowStyle BackColor="#efefef" />
                <Columns>
                    <asp:BoundField DataField="RubrosCNDTO.Codigo" HeaderText="Rubro" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                    <asp:BoundField DataField="RubrosDepositosCNDTO.Codigo" HeaderText="Depósito" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                    <asp:BoundField DataField="RubrosDepositosCNDTO.Descripcion" HeaderText="Descripción" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                </Columns>
                <EmptyDataTemplate>

                    <div class="mtop10">

                        <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                        <span class="mleft10">No se encontraron registros.</span>

                    </div>

                </EmptyDataTemplate>
            </asp:GridView>
        </asp:Panel>

        <asp:Panel ID="pnlObservacionesRubros" runat="server" Visible="false" HorizontalAlign="Left">
            <div>
                <strong>Observaciones de rubro
                </strong>
            </div>
            <asp:Label ID="lblObservacionesRubros" runat="server" />
        </asp:Panel>

        <asp:Panel ID="pnlRubrosAnteriores" runat="server" Visible="false">
            <div class="mtop10" id="TituloRubrosAnterioresAT" runat="server">
                <strong>Listado de rubros de la habilitaci&oacute;n anterior</strong>
            </div>
            <asp:GridView ID="grdRubrosIngresadosATAnterior" runat="server" AutoGenerateColumns="false"
                AllowPaging="false" Style="border: none;" CssClass="table table-bordered mtop5"
                GridLines="None" Width="100% ">
                <HeaderStyle CssClass="grid-header" />
                <RowStyle CssClass="grid-row" />
                <AlternatingRowStyle BackColor="#efefef" />
                <Columns>
                    <asp:BoundField DataField="CodigoRubro" HeaderText="Código" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="DescripcionRubro" HeaderText="Descripción" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                    <asp:BoundField DataField="TipoActividadNombre" HeaderText="Actividad" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="150px" />
                    <asp:ImageField DataImageUrlField="RestriccionZona" DataImageUrlFormatString="~/Common/Images/Rubros/{0}"
                        HeaderText="Area Mixtura/Distrito Zonificación" ItemStyle-Width="50px" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    </asp:ImageField>
                    <asp:BoundField DataField="EsAnterior" HeaderText="Anterior" Visible="false" />
                    <asp:BoundField DataField="SuperficieHabilitar" HeaderText="Superficie" Visible="true" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />


                </Columns>
                <EmptyDataTemplate>

                    <div class="mtop10">

                        <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                        <span class="mleft10">No se encontraron registros.</span>

                    </div>

                </EmptyDataTemplate>
            </asp:GridView>

            <asp:Panel ID="pnlObservacionesRubrosATAnterior" runat="server" Visible="false" HorizontalAlign="Left">
                <div>
                    <strong>Observaciones de rubro
                    </strong>
                </div>
                <asp:Label ID="lblObservacionesRubrosATAnterior" runat="server" />
            </asp:Panel>

        </asp:Panel>

        <asp:Panel ID="pnlRubrosCNAnteriores" runat="server" Visible="false">
            <div class="mtop10" id="TituloRubrosAnterioresCN" runat="server">
                <strong>Listado de rubros de la habilitaci&oacute;n anterior</strong>
            </div>
            <asp:GridView ID="grdRubrosCNIngresadosATAnterior" runat="server" AutoGenerateColumns="false"
                AllowPaging="false" Style="border: none;" CssClass="table table-bordered mtop5"
                GridLines="None" Width="100% ">
                <HeaderStyle CssClass="grid-header" />
                <RowStyle CssClass="grid-row" />
                <AlternatingRowStyle BackColor="#efefef" />
                <Columns>
                    <asp:BoundField DataField="CodigoRubro" HeaderText="Código" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="DescripcionRubro" HeaderText="Descripción" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                    <asp:BoundField DataField="TipoActividadNombre" HeaderText="Actividad" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="150px" />
                    <asp:ImageField DataImageUrlField="RestriccionZona" DataImageUrlFormatString="~/Common/Images/Rubros/{0}"
                        HeaderText="Area Mixtura/Distrito Zonificación" ItemStyle-Width="50px" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                    </asp:ImageField>
                    <asp:BoundField DataField="EsAnterior" HeaderText="Anterior" Visible="false" />
                    <asp:BoundField DataField="SuperficieHabilitar" HeaderText="Superficie" Visible="true" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />


                </Columns>
                <EmptyDataTemplate>

                    <div class="mtop10">

                        <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                        <span class="mleft10">No se encontraron registros.</span>

                    </div>

                </EmptyDataTemplate>
            </asp:GridView>

            <asp:Panel ID="pnlObservacionesRubrosCNATAnterior" runat="server" Visible="false" HorizontalAlign="Left">
                <div>
                    <strong>Observaciones de rubro
                    </strong>
                </div>
                <asp:Label ID="lblObservacionesRubrosCNATAnterior" runat="server" />
            </asp:Panel>

        </asp:Panel>
    </div>
</asp:Panel>
