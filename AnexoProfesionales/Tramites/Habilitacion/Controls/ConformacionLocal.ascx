<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConformacionLocal.ascx.cs" Inherits="AnexoProfesionales.Controls.ConformacionLocal" %>
<link href="<%: ResolveUrl("~/Content/themes/base/jquery.ui.custom.css") %>" rel="stylesheet" />

<%: Scripts.Render("~/bundles/autoNumeric") %>
<%: Scripts.Render("~/bundles/select2") %>
<%: Styles.Render("~/bundles/Select2Css") %>

<asp:Panel ID="pnlSobrecarga" runat="server">
    <%--Certificado de SobreCarga--%>

    <div class="row mtop10" style="margin-left: 50px; color: #344882;">
        <strong>Información ingresada en el trámite</strong>
    </div>
    <div style="padding: 10px 20px 20px 50px; width: 100%">

        <asp:GridView ID="grdConformacionLocal"
            runat="server"
            AutoGenerateColumns="false"
            DataKeyNames="id_encomiendaconflocal"
            AllowPaging="false"
            ShowHeader="false"
            ItemType="DataTransferObject.EncomiendaConformacionLocalDTO"
            Style="border: none; margin-top: 10px" GridLines="None" Width="1100px"
            CellPadding="3">

            <HeaderStyle CssClass="grid-header" HorizontalAlign="Left" />
            <PagerStyle CssClass="grid-pager" HorizontalAlign="Center" />
            <FooterStyle CssClass="grid-footer" HorizontalAlign="Left" />
            <RowStyle CssClass="grid-row" HorizontalAlign="Left" />
            <AlternatingRowStyle CssClass="grid-alternating-row" HorizontalAlign="Left" />
            <EditRowStyle CssClass="grid-edit-row" HorizontalAlign="Left" />
            <SelectedRowStyle CssClass="grid-selected-row" HorizontalAlign="Left" />

            <Columns>

                <asp:TemplateField ItemStyle-Width="100%">
                    <ItemTemplate>

                        <div class="form-horizontal">

                            <div class="form-group form-group-sm">
                                <label class="col-sm-2 control-label">Destino:</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtGrillaDestino" CssClass="form-control" runat="server" Text='<%# Item.TipoDestinoDTO.Nombre != "Otros" ? Item.TipoDestinoDTO.Nombre : Item.Detalle_conflocal %>'
                                        Enabled="false" Width="100%"></asp:TextBox>
                                </div>

                                <label class="col-sm-2 control-label">Planta:</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtGrillaPlanta" CssClass="form-control" runat="server" Text='<%# Item.EncomiendaPlantasDTO == null ? "" : Item.EncomiendaPlantasDTO.TipoSectorDTO == null ? "" : (Item.EncomiendaPlantasDTO.IdTipoSector == (int)StaticClass.Constantes.TipoSector.Piso 
                                                                                                                                        || Item.EncomiendaPlantasDTO.IdTipoSector == (int)StaticClass.Constantes.TipoSector.Otro) 
                                                                                                                                        ? Item.EncomiendaPlantasDTO.TipoSectorDTO.Nombre + " " + Item.EncomiendaPlantasDTO.detalle_encomiendatiposector
                                                                                                                                        : Item.EncomiendaPlantasDTO.TipoSectorDTO.Nombre %>'
                                        Enabled="false" Width="100%"></asp:TextBox>
                                </div>                               
                            </div>

                            <div class="form-group form-group-sm">
                                <label class="col-sm-2 control-label">Largo (m):</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtGrillaLargo" CssClass="form-control" runat="server" Text='<%# Item.largo_conflocal %>'
                                        Enabled="false" Width="100%"></asp:TextBox>
                                </div>
                                 <label class="col-sm-2 control-label">Ventilación:</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server" Text='<%# Item.TipoVentilacionDTO != null ? Item.TipoVentilacionDTO.nom_ventilacion : "" %>'
                                        Enabled="false" Width="100%"></asp:TextBox>
                                </div>

<%--                                <label class="col-sm-2 control-label">Paredes:</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtlGrillaPared" CssClass="form-control" runat="server" Text='<% #Eval("Paredes_conflocal") %>'
                                        Enabled="false" Width="100%"></asp:TextBox>
                                </div>--%>
                            </div>


                            <div class="form-group form-group-sm">
                                <label class="col-sm-2 control-label">Ancho (m):</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtGrillaAncho" CssClass="form-control" runat="server" Text='<% #Eval("ancho_conflocal") %>'
                                        Enabled="false" Width="100%"></asp:TextBox>
                                </div>

                                <label class="col-sm-2 control-label">Iluminación:</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" Text='<%# Item.TipoIluminacionDTO != null ? Item.TipoIluminacionDTO.nom_iluminacion : "" %>'
                                        Enabled="false" Width="100%"></asp:TextBox>
                                </div>

                               <%-- <label class="col-sm-2 control-label">Techos::</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtGrillaTecho" CssClass="form-control" runat="server" Text='<% #Eval("Techos_conflocal") %>'
                                        Enabled="false" Width="100%"></asp:TextBox>
                                </div>--%>
                            </div>


                            <div class="form-group form-group-sm">
                                <label class="col-sm-2 control-label">Alto (m):</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtGrillaAlto" CssClass="form-control" runat="server" Text='<% #Eval("alto_conflocal") %>'
                                        Enabled="false" Width="100%"></asp:TextBox>
                                </div>

                                <label class="col-sm-2 control-label">Sup. Estimada (m2):</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="TextBox5" CssClass="form-control" runat="server"
                                        Text='<% # decimal.Parse(Eval("superficie_conflocal").ToString()).ToString("N2")  %>'
                                        Enabled="false" Width="100%"></asp:TextBox>
                                </div>

                                <%--<label class="col-sm-2 control-label">Pisos:</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtGrillaPiso" CssClass="form-control" runat="server" Text='<% #Eval("Pisos_conflocal") %>'
                                        Enabled="false" Width="100%"></asp:TextBox>
                                </div>--%>
                            </div>


                            <div class="form-group form-group-sm">
                                <label class="col-sm-2 control-label">Tipo Superficie:</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"
                                        Text='<%# Item.TipoSuperficieDTO.Descripcion %>'
                                        Enabled="false" Width="100%"></asp:TextBox>
                                </div>
                                <label class="col-sm-2 control-label">Observaciones:</label>
                                <div class="col-sm-3">

                                    <asp:TextBox ID="TextBox6" CssClass="form-control" runat="server" Text='<% #Eval("Observaciones_conflocal") %>'
                                        Enabled="false" Width="100%" Height="100px"
                                        TextMode="MultiLine" Style="overflow: auto;"></asp:TextBox>
                                </div>

                               <%-- <div class="col-sm-2 control-label"">
                                    <asp:Label runat="server" Visible='<%# Item.TipoDestinoDTO.Nombre != "Otros" ? false : true %>'>Detalle (Otros):</asp:Label>
                                </div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server"
                                        Text='<%# Item.Detalle_conflocal %>'
                                        Enabled="false" Visible='<%# Item.TipoDestinoDTO.Nombre != "Otros" ? false : true %>' Width="100%"></asp:TextBox>
                                </div>--%>                                                               
                             </div>


                             <div class="form-group form-group-sm">
                                
                            </div>
                        </div>
                        <hr />

                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>

            <EmptyDataTemplate>
                <div class="mtop10">
                    <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                    No hay datos aún...
                </div>
            </EmptyDataTemplate>

        </asp:GridView>

    </div>

    <div class="form-horizontal">

        <div class="form-group form-group-sm">

            <label class="col-sm-2 control-label">Superficie Total Estimada:</label>
            <div class="col-sm-3">
                <asp:TextBox ID="txtSupTotal" CssClass="form-control" runat="server" Text=''
                    Enabled="false" Width="100%"></asp:TextBox>
            </div>
        </div>
    </div>
</asp:Panel>
