<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pagos.ascx.cs" Inherits="SSIT.Solicitud.Habilitacion.Controls.Pagos" %>

<asp:UpdatePanel ID="upd_hid_Pagos" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="hid_id_solicitud" runat="server" />
        <asp:HiddenField ID="hid_id_pago" runat="server" Value="0" />
        <asp:HiddenField ID="hid_estado_pago" runat="server" Value="" />
        <asp:HiddenField ID="hid_habilitar_generacion_manual" runat="server" Value="" />
        <asp:HiddenField ID="hid_tipo_tramite" runat="server" />
        <asp:HiddenField ID="hid_error_message" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>

<asp:UpdatePanel ID="updPnlGenerarBoletaUnica" runat="server" RenderMode="Inline">
    <ContentTemplate>
        <asp:Panel ID="pnlGenerarBoletaUnica" runat="server" Visible="false">
            <div class="row">
            </div>
            <div class="row">
                <div class="col-sm-1">
                    <div class="ptop10">
                        <i class="imoon imoon-dollar fs48"></i>
                    </div>
                </div>
                <div class="col-sm-11 ptop10">
                    <div class="form-group">
                        Generar boleta de pago para ser abonada en las cajas de tesorer&iacute;a de la Ciudad.
                    </div>
                    <div class="form-group">
                        <asp:LinkButton ID="lnkGenerarBoletaUnica" runat="server" OnClick="lnkGenerarBoletaUnica_Click" 
                            OnClientClick="$(this).hide();"
                            CssClass="btn btn-primary"> 
                                <i></i>
                                <span class="text">Generar Boleta</span>
                        </asp:LinkButton>
                    </div>
                    <div class="form-group">
                        <asp:UpdateProgress ID="UpPrgssGenerarBoletaUnica" runat="server" AssociatedUpdatePanelID="updPnlGenerarBoletaUnica">
                            <ProgressTemplate>
                                <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif")  %>" class="pleft10" alt="" />Generando ...
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

<asp:UpdatePanel ID="updGridBoletas" runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnlPagosGeneradosBUI" runat="server">
            <asp:GridView ID="grdPagosGeneradosBUI" runat="server" AutoGenerateColumns="false"
                DataKeyNames="id_sol_pago,id_solicitud,id_pago" CssClass="table table-bordered mtop5"
                AllowPaging="false" Style="border: none;"
                GridLines="None" Width="100%" OnRowDataBound="grdPagosGeneradosBUI_RowDataBound">
                <HeaderStyle CssClass="grid-header" />
                <Columns>
                    <asp:BoundField DataField="CreateDate" HeaderText="Fecha" ItemStyle-Width="80px"
                        HeaderStyle-CssClass="text-center " ItemStyle-CssClass="text-center" HtmlEncode="false" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="id_solicitud" HeaderText="Nº Trámite" ItemStyle-Width="90px" HeaderStyle-CssClass="text-center " ItemStyle-CssClass="text-center" />
                    <asp:BoundField DataField="id_pago" HeaderText="Nº identificación" ItemStyle-Width="200px" HeaderStyle-CssClass="text-center " ItemStyle-CssClass="text-center" />
                    <asp:BoundField DataField="monto_pago" HeaderText="Monto Total" DataFormatString="{0:C}" HeaderStyle-CssClass="text-center " ItemStyle-CssClass="text-center"
                        ItemStyle-Width="200px" />
                    <asp:TemplateField ItemStyle-CssClass="text-center" HeaderText="Estado" ItemStyle-Width="200px" HeaderStyle-CssClass="text-center ">
                        <ItemTemplate>
                            <asp:Label ID="lblDescripicionEstadoPago" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="text-center" HeaderText="Descargar" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkImprimirBoletaUnica" title="Descargar boleta" runat="server" Target="_blank">
                                <span class="icon"><i class="imoon-download fs24"></i></span>
                            </asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <div class="mtop10">
                        <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                        <span class="mleft10">No existen boletas generadas.</span>
                    </div>
                </EmptyDataTemplate>
            </asp:GridView>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

