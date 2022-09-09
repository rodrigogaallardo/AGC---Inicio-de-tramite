<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistorialProfesionales.aspx.cs" Inherits="ConsejosProfesionales.ABM.HistorialProfesionales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hiddenIdProfesional" runat="server" />
    <asp:UpdatePanel ID="pnlGrillaResultados" runat="server">
        <ContentTemplate>
            <asp:LinkButton ID="cmdGuardar" runat="server" CssClass="btn btn-primary" OnClientClick="javascript:history.go(-1)">
              <i class="imoon imoon-arrow-left"></i>
              <span class="text">Volver</span>
            </asp:LinkButton>
            <div class="mtop30 row pleft10 pright10">
                <div class="col-sm-6">
                    <strong>Historial</strong>
                </div>
                <div class="col-sm-6 text-right">
                    <strong>Cantidad de registros:</strong>
                    <asp:Label ID="lblCantResultados" runat="server" CssClass="badge">0</asp:Label>
                </div>
            </div>
            <br>
            <div style="overflow: auto; width: 100%; height: 490px; overflow-y: scroll;">
                <asp:Panel ID="pnlResultados" runat="server">
                    <asp:GridView
                        ID="grdProfesionales"
                        runat="server"
                        AutoGenerateColumns="false"
                        Width="2000px"
                        GridLines="None"
                        CssClass="table table-bordered mtop5"
                        OnDataBound="grdProfesionales_DataBound"
                        AllowPaging="true"
                        SelectMethod="grdProfesionales_GetData"
                        PageSize="50"                        
                        OnPageIndexChanging="grdProfesionales_PageIndexChanging"
                        OnRowDataBound="grdProfesionales_RowDataBound">
                        <HeaderStyle CssClass="grid-header" />
                        <RowStyle CssClass="grid-row" Width="100%" />
                        <AlternatingRowStyle BackColor="#efefef" />
                        <Columns>
                            <asp:BoundField DataField="OperacionFull" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" HeaderText="Tipo de Operación" />
                            <asp:BoundField DataField="Fecha" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" HeaderText="Fecha Operación" ItemStyle-Wrap="false"  />
                            <asp:BoundField DataField="aspnet_Users1.UserName" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-center" HeaderText="Responsable" />
                            <asp:BoundField DataField="ConsejoProfesional.Nombre" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-center" HeaderText="Consejo" ItemStyle-Wrap="false"/>
                            <asp:BoundField DataField="Nombre" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-center" HeaderText="Nombre"  />
                            <asp:BoundField DataField="Apellido" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-center" HeaderText="Apellido" />
                            <asp:BoundField DataField="TipoDocuento" HeaderText="Tipo Documento" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                            <asp:BoundField DataField="NroDocumento" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-center" HeaderText="Documento" />
                            <asp:BoundField DataField="Matricula" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-center" HeaderText="Matricula" />
                            <asp:BoundField DataField="Perfiles" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-center" HeaderText="Perfil" ItemStyle-Wrap="false"/>
                            <asp:BoundField DataField="Calle" HeaderText="Calle" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-center" />
                            <asp:BoundField DataField="NroPuerta" HeaderText="NroPuerta" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-center"  />
                            <asp:BoundField DataField="Piso" HeaderText="Piso" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-center"  />
                            <asp:BoundField DataField="Depto" HeaderText="Depto" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-center"/>
                            <asp:BoundField DataField="Localidad" HeaderText="Localidad" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-center"  ItemStyle-Wrap="false"/>
                            <asp:BoundField DataField="Provincia" HeaderText="Provincia" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-center" ItemStyle-Wrap="false"  />
                            <asp:BoundField DataField="Email" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-center" HeaderText="E-mail"/>
                            <asp:BoundField DataField="SMS" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-center" HeaderText="SMS"  />
                            <asp:BoundField DataField="Telefono" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-center" HeaderText="Télefono" />
                            <asp:BoundField DataField="CUIT" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" HeaderText="CUIL"  ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="IngresosBrutos" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-center" HeaderText="Ingresos Brutos" />
                            <asp:BoundField DataField="MatriculaMetrogas" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-center" HeaderText="Matricula Gasista" />
                            <asp:BoundField DataField="CategoriaMetrogas" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-center" HeaderText="Categoría" />
                            <asp:BoundField DataField="Inhibido" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-center" HeaderText="Inhibido"/>
                            <asp:BoundField DataField="Baja" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-center" HeaderText="Baja" />
                            <asp:BoundField DataField="Observaciones" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-center" HeaderText="Observaciones" ItemStyle-Wrap="false" />
                        </Columns>
                        <PagerTemplate>
                            <asp:Panel ID="pnlpager" CssClass="form-inline" runat="server" Style="padding: 10px; text-align: center; border-top: solid 1px #e1e1e1">
                                <asp:LinkButton ID="cmdAnterior" runat="server" Text="<<" OnClick="cmdAnterior_Click"
                                    CssClass="btn btn-default" Width="35px" />
                                <asp:LinkButton ID="cmdPage1" runat="server" Text="1" OnClick="cmdPage" CssClass="btn btn-default" />
                                <asp:LinkButton ID="cmdPage2" runat="server" Text="2" OnClick="cmdPage" CssClass="btn" />
                                <asp:LinkButton ID="cmdPage3" runat="server" Text="3" OnClick="cmdPage" CssClass="btn" />
                                <asp:LinkButton ID="cmdPage4" runat="server" Text="4" OnClick="cmdPage" CssClass="btn" />
                                <asp:LinkButton ID="cmdPage5" runat="server" Text="5" OnClick="cmdPage" CssClass="btn" />
                                <asp:LinkButton ID="cmdPage6" runat="server" Text="6" OnClick="cmdPage" CssClass="btn" />
                                <asp:LinkButton ID="cmdPage7" runat="server" Text="7" OnClick="cmdPage" CssClass="btn" />
                                <asp:LinkButton ID="cmdPage8" runat="server" Text="8" OnClick="cmdPage" CssClass="btn" />
                                <asp:LinkButton ID="cmdPage9" runat="server" Text="9" OnClick="cmdPage" CssClass="btn" />
                                <asp:LinkButton ID="cmdPage10" runat="server" Text="10" OnClick="cmdPage" CssClass="btn" />
                                <asp:LinkButton ID="cmdPage11" runat="server" Text="11" OnClick="cmdPage" CssClass="btn" />
                                <asp:LinkButton ID="cmdPage12" runat="server" Text="12" OnClick="cmdPage" CssClass="btn" />
                                <asp:LinkButton ID="cmdPage13" runat="server" Text="13" OnClick="cmdPage" CssClass="btn" />
                                <asp:LinkButton ID="cmdPage14" runat="server" Text="14" OnClick="cmdPage" CssClass="btn" />
                                <asp:LinkButton ID="cmdPage15" runat="server" Text="15" OnClick="cmdPage" CssClass="btn" />
                                <asp:LinkButton ID="cmdPage16" runat="server" Text="16" OnClick="cmdPage" CssClass="btn" />
                                <asp:LinkButton ID="cmdPage17" runat="server" Text="17" OnClick="cmdPage" CssClass="btn" />
                                <asp:LinkButton ID="cmdPage18" runat="server" Text="18" OnClick="cmdPage" CssClass="btn" />
                                <asp:LinkButton ID="cmdPage19" runat="server" Text="19" OnClick="cmdPage" CssClass="btn" />
                                <asp:LinkButton ID="cmdSiguiente" runat="server" Text=">>" OnClick="cmdSiguiente_Click"
                                    CssClass="btn btn-default" Width="35px" />

                                <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="pnlGrillaResultados"
                                    runat="server" DisplayAfter="0">
                                    <ProgressTemplate>
                                        <img src="../Common/Images/Controles/Loading24x24.gif" alt="" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </asp:Panel>
                        </PagerTemplate>
                        <EmptyDataTemplate>
                            <div>
                                <img src="../Content/img/app/NoRecords.png" />
                                <span class="mleft20">No se encontró historial para el profesional seleccionado.<br />
                                </span>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
