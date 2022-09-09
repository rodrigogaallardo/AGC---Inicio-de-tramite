<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="BuscarProfesionales.aspx.cs" Inherits="ConsejosProfesionales.ABM.BuscarProfesionales" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <h2 class="text-center">Búsqueda de Profesionales
    </h2>
    <hr />


    <p class="mtop10 text-center">
        Desde aqu&iacute; podr&aacute; consultar los profesionales.<br />
        Ver el estado en que se encuentran y trabajar con cada uno.
    </p>
    <br />
    <asp:Panel ID="pnl01" runat="server" DefaultButton="btnBuscar" CssClass="box-panel">
        <div style="margin: 20px; margin-top: -5px">
            <div style="margin-top: 5px; color: #377bb5">
                <h4><i class="imoon imoon-search" style="margin-right: 10px"></i>Carga de Busqueda</h4>
                <hr />
            </div>
        </div>
        <div class="form-horizontal ">
            <div class="form-group">
                <asp:Label ID="Label3" runat="server" Text="Nro. de Matrícula:" CssClass="control-label col-sm-2"></asp:Label>
                <div class="col-sm-3">
                    <asp:TextBox ID="txtNroMatricula" runat="server" Width="100px" MaxLength="8" CssClass="form-control"></asp:TextBox>
                </div>
                <asp:Label ID="Label5" runat="server" Text="Usuario:" CssClass="control-label col-sm-2"></asp:Label>
                <div class="col-sm-3">
                    <asp:TextBox ID="txtUserName" runat="server" Width="200px" MaxLength="50" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="form-horizontal">
            <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text="Apellido y nombre:" CssClass="control-label col-sm-2"></asp:Label>
                <div class="col-sm-3">
                    <asp:TextBox ID="txtApeNom" runat="server" Width="300px" MaxLength="50" CssClass="form-control"></asp:TextBox>
                </div>
                <asp:Label ID="Label4" runat="server" Text="Nro. de Documento:" CssClass="control-label col-sm-2"></asp:Label>
                <div class="col-sm-3">
                    <asp:TextBox ID="txtNroDNI" runat="server" Width="200px" MaxLength="50" CssClass="form-control"></asp:TextBox>                    
                </div>
            </div>
        </div>
        <div class="form-horizontal">
            <div class="form-group">
                <asp:Label ID="Label2" runat="server" Text="Bloqueado:" CssClass="control-label col-sm-2"></asp:Label>
                <div class="col-sm-2">
                    <asp:DropDownList ID="ddplBloqueado" runat="server" Width="110px" CssClass="form-control">
                        <asp:ListItem Text="No" Value="false"></asp:ListItem>
                        <asp:ListItem Text="Si" Value="true"></asp:ListItem>
                        <asp:ListItem Text="Todos" Value="" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <asp:Label ID="Label7" runat="server" Text="Inhibido:" CssClass="control-label col-sm-3"></asp:Label>
                <div class="col-sm-3">
                    <asp:DropDownList ID="ddlprofInhibido" runat="server" Width="110px" CssClass="form-control">
                        <asp:ListItem Text="No" Value="false" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Si" Value="true"></asp:ListItem>
                        <asp:ListItem Text="Todos" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="form-horizontal">
            <div class="form-group">
                <asp:Label ID="Label6" runat="server" Text="Dado de Baja:" CssClass="control-label col-sm-2"></asp:Label>
                <div class="col-sm-3">
                    <asp:DropDownList ID="ddlProfBajaLogica" runat="server" Width="110px" CssClass="form-control">
                        <asp:ListItem Text="No" Value="false" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Si" Value="true"></asp:ListItem>
                        <asp:ListItem Text="Todos" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <br />
        <asp:UpdatePanel ID="updBuscar" runat="server">
            <ContentTemplate>
                <div style="width: 100%; text-align: center">
                    <div style="display: table; margin: auto">
                        <div style="display: table-row">
                            <div style="display: table-cell">
                                <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-primary" ValidationGroup="Buscar"
                                    OnClick="btnBuscar_Click" UseSubmitBehavior="false">
                                    <i class="imoon imoon-search"></i>
                                    <span class="text">Buscar</span>
                                </asp:LinkButton>
                            </div>
                            <div style="display: table-cell">

                                <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="updBuscar"
                                    runat="server" DisplayAfter="0">
                                    <ProgressTemplate>
                                        <img src="../Common/Images/Controles/Loading24x24.gif" alt="" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <br />

    <div style="width: 100%; text-align: right;">
        <asp:LinkButton ID="btnExportar" runat="server" CssClass="btn btn-success" OnClick="btnExportar_Click" Title="Exportar Profesionales">
                                <i class="icon-white imoon-file-excel"></i>
                                <span class="text">Exportar Profesionales</span>
        </asp:LinkButton>
        <asp:LinkButton ID="btnAgregarProfesional" runat="server" CssClass="btn btn-default text-center" PostBackUrl="~/ABM/ABMProfesionales.aspx" Title="Ingresar Profesional">
                                <i class="imoon imoon-plus"></i>
                                <span class="text">Ingresar Profesional</span>
        </asp:LinkButton>
    </div>
    <br />
    <asp:UpdatePanel ID="pnlGrillaResultados" runat="server">

        <ContentTemplate>
            <div class="mtop30 row pleft10 pright10">
                <div class="col-sm-6">
                    <strong>Resultado de la b&uacute;squeda</strong>
                </div>
                <div class="col-sm-6 text-right">
                    <strong>Cantidad de registros:</strong>
                    <asp:Label ID="lblCantResultados" runat="server" CssClass="badge">0</asp:Label>
                </div>
            </div>
            <asp:Panel ID="pnlResultados" runat="server">
                <asp:GridView
                    ID="grdProfesionales"
                    runat="server"
                    AutoGenerateColumns="false"
                    OnRowCommand="grdProfesionales_RowCommand"
                    Width="100%"
                    GridLines="None"
                    CssClass="table table-bordered mtop5"
                    OnDataBound="grdProfesionales_DataBound"
                    AllowPaging="true"
                    SelectMethod="grdProfesionales_GetData"
                    PageSize="50"
                    OnPageIndexChanging="grdProfesionales_PageIndexChanging"
                    OnRowDataBound="grdProfesionales_RowDataBound">
                    <HeaderStyle CssClass="grid-header" />
                    <RowStyle CssClass="grid-row" />
                    <AlternatingRowStyle BackColor="#efefef" />
                    <Columns>
                        <asp:BoundField DataField="Matricula" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" HeaderText="Matrícula" HeaderStyle-HorizontalAlign="Left" />
                        <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" HeaderText="Usuario" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblUser" Text='<%# Eval("UsuarioCreado") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Apenom" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" HeaderText="Apellido y Nombre" HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="NroDocumento" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" HeaderText="Nro. de Documento" HeaderStyle-HorizontalAlign="Left" />
                        <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" HeaderText="Subdivisión" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <%# Eval("ConsejoProfesionalDTO.GrupoConsejosDTO.Nombre") %>
                                <asp:HyperLink ID="lnkConsejo" NavigateUrl="javascript:void(0);" runat="server" rel="popover" data-trigger="focus"
                                    data-content='<%# Eval("ConsejoProfesionalDTO.Nombre") + " - " + Eval("ConsejoProfesionalDTO.GrupoConsejosDTO.descripcion") %>' Style="margin-left: 10px"
                                    data-placement="left" data-original-title="Consejo Profesional">
                                                    <i class="imoon-eye"></i>
                                </asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Bloqueado" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" HeaderText="Bloqueado" HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="Inhibido" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" HeaderText="Inhibido" HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="DadoBaja" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" HeaderText="Dado de Baja" HeaderStyle-HorizontalAlign="Left" />
                        <asp:TemplateField ItemStyle-Width="100px" HeaderText="Acciones" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <%--Editar Profesionales--%>
                                <asp:LinkButton ID="btnEditarProfesionales"
                                    title="Editar Profesional" PostBackUrl='<%# Eval("Id","~/ABM/ABMProfesionales.aspx?id={0}") %>'
                                    runat="server"
                                    Style="margin-left: 10px" Visible='<%# !((bool)Convert.ToBoolean(Eval("BajaLogica"))) %>'
                                    CommandName="EditarProfesionales">
                                      <i class="imoon-pencil"></i>              
                                </asp:LinkButton>
                                <%--Historial Profesionales--%>
                                <asp:LinkButton ID="btnHistorial"
                                    title="Historial Profesional"
                                    runat="server" PostBackUrl='<%# Eval("Id","~/ABM/HistorialProfesionales.aspx?id={0}") %>'
                                    Style="margin-left: 10px"
                                    CommandName="HistorialProfesionales">
                                      <i class="imoon-history"></i>              
                                </asp:LinkButton>
                                <%--Eliminar Profesionales--%>
                                <asp:LinkButton ID="btnEliminarProfesionales"
                                    title="Dar de Baja"
                                    runat="server"
                                    Style="margin-left: 10px"
                                    CommandName="EliminarProfesionales" Visible='<%# !((bool)Convert.ToBoolean(Eval("BajaLogica"))) %>'
                                    OnClientClick='<%# "showfrmConfirmarEliminar(" + Eval("Id") + ");" %>'>
                                      <i class="imoon-trash"></i>              
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnReactivarBaja"
                                    title="Reactivar baja"
                                    runat="server" OnClientClick='<%# "showfrmConfirmarReactivar(" + Eval("Id") + ");" %>'
                                    Style="margin-left: 10px" Visible='<%# ((bool)Convert.ToBoolean(Eval("BajaLogica"))) %>'>
                                      <i class="imoon-switch"></i>              
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
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
                            <span class="mleft20">No se encontraron profesionales con los filtros ingresados.<br />
                            </span>
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </asp:Panel>
            <script>
                $(function () {
                    $('[rel=popover]').popover({
                        html: 'true',
                        placement: 'left'
                    });
                });
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>



    <asp:Panel ID="pnlInformacion" runat="server" class="modal modal-transparent fade in">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                </div>
                <div class="modal-body">

                    <table style="width: 100%">
                        <tr>
                            <td style="width: 80px">
                                <asp:Image ID="imgmpeInfo" runat="server" ImageUrl="~/Common/Images/Controles/error64x64.png" />
                            </td>
                            <td>
                                <asp:UpdatePanel ID="updmpeInfo" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblmpeInfo" runat="server" Style="color: Black"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <asp:Button ID="btnAceptarInformacion" runat="server" CssClass="btnOK" Text="Aceptar"
                                    Width="100px" OnClientClick="return ocultarPopup('pnlInformacion');" />


                            </td>
                        </tr>
                    </table>

                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlSuccess" runat="server" CssClass="modalPopup" Style="display: none"
        Width="450px" DefaultButton="btnAceptarSuccess">
        <table style="width: 100%">
            <tr>
                <td style="width: 80px">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Common/Images/Controles/info64x64.png" />
                </td>
                <td>
                    <asp:UpdatePanel ID="UpdpnlSuccess" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblSuccess" runat="server" Style="color: Black"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="btnAceptarSuccess" runat="server" CssClass="btnOK" Text="Aceptar"
                        Width="100px" OnClientClick="return ocultarPopup('pnlSuccess');" />

                </td>
            </tr>
        </table>
    </asp:Panel>

    <%--Modal Confirmar Anulación--%>
    <asp:UpdatePanel ID="udpConfirmarEliminacion" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="frmConfirmarEliminacion" class="modal fade" role="dialog">

                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" onclick="hidefrmConfirmar()" data-dismiss="modal"><span aria-hidden="true">X</span><span class="sr-only">Close</span></button>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel ID="UpdPnlSuccess_ok" runat="server">
                                <ContentTemplate>
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Common/Images/Controles/info64x64.png" />
                                                    </div>
                                                    <div class="col-sm-9" style="vertical-align: middle">
                                                        ¿Esta seguro que desea dar de baja al profesional?
                                                                <asp:HiddenField ID="hfIdProfesional" runat="server" />
                                                    </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Button ID="btnEliminar" runat="server" type="button" class="btn btn-default" Text="SI" OnClick="btnEliminar_Click" />
                                                <button type="button" class="btn btn-default" onclick="hidefrmConfirmar()" data-dismiss="modal">NO</button>
                                                <asp:UpdateProgress ID="UpdateProgress3" AssociatedUpdatePanelID="udpConfirmarEliminacion" runat="server" DisplayAfter="0">
                                                    <ProgressTemplate>
                                                        <div style="padding: 15px">
                                                            <img src="../Common/Images/Controles/Loading24x24.gif" alt="" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>









            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--Modal Confirmar Activación--%>
    <asp:UpdatePanel ID="udpConfirmarReactivar" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="frmConfirmarReactivar" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">X</span><span class="sr-only">Close</span></button>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Common/Images/Controles/info64x64.png" />
                                                    </div>
                                                    <div class="col-sm-9" style="vertical-align: middle">
                                                        ¿Esta seguro que desea reactivar el Profesional?
                                                                <asp:HiddenField ID="hfIdProfesionalReactivar" runat="server" />
                                                    </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <asp:Button ID="btnReactivar" runat="server" type="button" class="btn btn-default btnOK" Text="SI" OnClick="btnReactivar_Click" />
                                                <button type="button" class="btn btn-default btnOK" data-dismiss="modal">NO</button>
                                                <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="udpConfirmarReactivar" runat="server" DisplayAfter="0">
                                                    <ProgressTemplate>
                                                        <div style="padding: 15px">
                                                            <img src="../Common/Images/Controles/Loading24x24.gif" alt="" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>





            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--Exportación a Excel--%>
    <div id="frmExportarExcel" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="updExportaExcel" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title">Exportar a Excel</h4>
                        </div>
                        <div class="modal-body">
                            <asp:Timer ID="Timer1" OnTick="Timer1_Tick" runat="server" Interval="1000" Enabled="false">
                            </asp:Timer>
                            <asp:Panel ID="pnlExportandoExcel" runat="server">
                                <div class="row text-center">
                                    <img src="<%: ResolveUrl("~/Content/img/app/Loading128x128.gif") %>" alt="" />
                                </div>
                                <div class="row text-center">
                                    <h2>
                                        <asp:Label ID="lblRegistrosExportados" runat="server"></asp:Label></h2>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlDescargarExcel" runat="server" Style="display: normal">
                                <div class="row text-center">
                                    <asp:HyperLink ID="btnDescargarExcel" runat="server" Target="_blank" CssClass="btn btn-link">
                                        <i class="imoon imoon-file-excel color-green fs48"></i>
                                        <br />
                                        <span class="text">Descargar archivo</span>
                                    </asp:HyperLink>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnlExportacionError" runat="server" Style="display: none">
                                <div class="row text-center">
                                    <i class="imoon imoon-notification color-blue fs64"></i>
                                    <h3>
                                        <asp:Label ID="lblExportarError" runat="server" Text="Error exportando el contenido, por favor vuelva a intentar."></asp:Label></h3>
                                </div>
                            </asp:Panel>

                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnCerrarExportacion" runat="server" CssClass="btn btn-default" OnClick="btnCerrarExportacion_Click" Text="Cerrar" Visible="false" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


    <%--Modal Confirmar Activación--%>
    <asp:UpdatePanel ID="updConfirmarMensaje" runat="server">
        <ContentTemplate>
            <div id="frmConfirmarMensaje" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" onclick="hidefrmConfirmarBaja()" data-dismiss="modal"><span aria-hidden="true">X</span><span class="sr-only">Close</span></button>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <div class="row">
                                                    <div class="col-sm-3">
                                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/Common/Images/Controles/info64x64.png" />
                                                    </div>
                                                    <div class="col-sm-9" style="vertical-align: middle">
                                                        <asp:Label ID="lblConfirmarMensaje" runat="server" Text=""></asp:Label>
                                                    </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">
                                                <button type="button" onclick="hidefrmConfirmarBaja()" class="btn btn-default" data-dismiss="modal">Aceptar</button>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>





            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <script>
        //Re-Create for on page postbacks
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            $('[rel=popover]').popover({
                html: 'true',
                placement: 'left'
            });
        });
    </script>
    <script type="text/javascript">

        function tooltipsLoad() {

            $('[rel=popover]').popover({
                html: 'true',
                placement: 'left'
            });

        }

        $(function () {
            $('[rel=popover]').popover({
                html: 'true',
                placement: 'left'
            });
        });

        function showfrmConfirmarEliminar(Id) {

            $("#<%: hfIdProfesional.ClientID %>").val(Id);

            $("#frmConfirmarEliminacion").modal({
                "show": true,
                "backdrop": "static",
                "keyboard": false
            });

            return false;
        }
        function showfrmConfirmarReactivar(Id) {

            $("#<%: hfIdProfesionalReactivar.ClientID %>").val(Id);

            $("#frmConfirmarReactivar").modal({
                "show": true,
                "backdrop": "static",
                "keyboard": false
            });

            return false;
        }

        function hidefrmConfirmar() {
            $("#frmConfirmarEliminacion").modal("hide");
            $('.modal-backdrop').remove();
            $('body').removeClass('modal-open');
        }
        function hidefrmConfirmarBaja() {
            $("#frmConfirmarEliminacion").modal("hide");
            $("#frmConfirmarReactivar").modal("hide");
            $("#frmConfirmarEliminacion").modal("hide");
            $('.modal-backdrop').remove();
            $('body').removeClass('modal-open');
        }
        function hidefrmConfirmarReactivar() {
            $("#frmConfirmarReactivar").modal("hide");
            $('.modal-backdrop').remove();
            $('body').removeClass('modal-open');
        }
        function showfrmMensajeFinal() {
            $("#frmConfirmarMensaje").modal("show");
            return false;
        }
        function hidefrmConfirmar() {
            $("#frmConfirmarMensaje").modal("hide");
            $('.modal-backdrop').remove();
            $('body').removeClass('modal-open');
        }
        function showfrmExportarExcel() {
            $("#frmExportarExcel").modal({
                backdrop: "static",
                show: true
            });
            return true;
        }
        function hidefrmExportarExcel() {
            $("#frmExportarExcel").modal("hide");
            return false;
        }
    </script>
</asp:Content>
