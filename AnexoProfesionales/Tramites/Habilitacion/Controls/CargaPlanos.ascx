<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CargaPlanos.ascx.cs" Inherits="AnexoProfesionales.Controls.CargaPlanos" %>

<%--Datos Local--%>
<asp:Panel ID="pnlCargaPlanos" runat="server" Style="margin:20px">

        <%--Grilla de Datos Local--%>
        <div>
            <strong>Datos de Planos</strong>
        </div>
        <br />
          <%--Grilla de planos--%>
           <asp:UpdatePanel ID="updPnlGrillaPlanos" runat="server" >
                    <ContentTemplate>

                       <div style="padding-left:20px; width:1125px;" class="box-panel">
                            <asp:GridView ID="grdPlanos" runat="server" AutoGenerateColumns="false"
                                DataKeyNames="id_encomienda_plano, id_encomienda" AllowPaging="false" Style="border: none;" GridLines="None" Width="900px" 
                                CellPadding="3">
                                <HeaderStyle CssClass="grid-header"  /> 
                                <AlternatingRowStyle BackColor="#efefef" />

                                <Columns>

                                    <asp:BoundField DataField="DetalleExtra" HeaderText="Tipo de Plano" ItemStyle-Width="140px" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                                    <asp:BoundField DataField="detalle" HeaderText="Detalle del Plano" ItemStyle-Width="200px" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />

                                    <asp:TemplateField ItemStyle-Height="24px" ItemStyle-Width="350px" HeaderText="Descargar">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkDescargarPlano" runat="server" Target="_blank" style="padding-right:10px" NavigateUrl='<%# Eval("url") %>'>
                                                <i class="icon-download-alt"></i>
                                                <span class="text"><%# Eval("nombre_archivo")%></span>
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="CreateDate" HeaderText="Subido el "  ItemStyle-Width="100px"
                                            HeaderStyle-HorizontalAlign="Left"  DataFormatString="{0:dd/MM/yyyy}" />


                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="mtop10">
                                         <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                                        No hay planos aún...
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </ContentTemplate>
           </asp:UpdatePanel>

</asp:Panel>


