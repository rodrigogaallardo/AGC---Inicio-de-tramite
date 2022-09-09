<%@ Page Title="" Culture="es-AR" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BandejaDeNotificaciones.aspx.cs" Inherits="SSIT.Solicitud.BandejaDeNotificaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%: Scripts.Render("~/bundles/autoNumeric") %>
    <%: Scripts.Render("~/bundles/select2") %>
    <%: Styles.Render("~/bundles/select2Css") %>
    <style>
        label[for=chkSelect] {
            color: transparent;
            display: none;
        }
    </style>
    <h2>Bandeja de avisos</h2>
    <hr />
    <p class="mtop10">
        Desde aqui podrá consultar los trámites que contienen avisos importantes.
    </p>
    <%--ajax cargando ...--%>
    <div id="Loading" style="text-align: center; padding-bottom: 20px; margin-top: 120px">
        <table border="0" style="border-collapse: separate; border-spacing: 5px; margin: auto">
            <tr>
                <td>
                    <img src="<%: ResolveUrl("~/Content/img/app/Loading128x128.gif") %>" alt="" />
                </td>
            </tr>
            <tr>
                <td style="font-size: 24px">Cargando avisos
                </td>
            </tr>
        </table>
    </div>

    <div id="page_content" style="display: none">
        <%-- Busqueda --%>
        <asp:UpdatePanel ID="updBuscar" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hid_id_motivo" runat="server" />
                <asp:HiddenField ID="hid_id_solicitud" runat="server" />
                <asp:HiddenField ID="hid_ubicacion" runat="server" />
                <asp:HiddenField ID="hid_NotificacionMotivo" runat="server" />
                <asp:HiddenField ID="hid_formulario_cargado" runat="server" />

                <div class="mtop30">
                    <h3>Búsqueda de avisos</h3>
                    <hr />
                </div>
                <asp:Panel ID="pnlBuscar" runat="server" DefaultButton="btnBuscar" CssClass="form-inline">
                    <div class="row mleft10 mtop10">
                        <label class="control-label col-sm-2 mtop5 text-right">N&uacute;mero de solicitud:</label>
                        <asp:TextBox ID="txtNroSolicitud" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                    </div>
                    <div class="row mleft10 mtop10">
                        <label class="control-label col-sm-2 mtop5 text-right">Domicilio:</label>
                        <asp:TextBox ID="txtUbicacion" runat="server" CssClass="form-control" Width="50%" Style='text-transform: uppercase'></asp:TextBox>
                    </div>
                    <asp:UpdatePanel ID="updMotivos" runat="server">
                        <ContentTemplate>
                            <div class="row mleft10 mtop10">
                                <label class="control-label col-sm-2 mtop5 text-right">Estado del aviso:</label>
                                <asp:DropDownList ID="ddlMotivos" runat="server" Width="50%" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row mleft10 mtop10">
                        <label class="control-label col-sm-2 mtop5 text-right"></label>
                        <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-primary" OnClick="btnBuscar_Click">
                            <i class="imoon imoon-search"></i>
                            <span class="text">Buscar</span>
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnLimpiar" runat="server" CssClass="btn btn-default" OnClick="btnLimpiar_Click">
                            <i class="imoon imoon-eraser"></i>
                            <span class="text">Limpiar</span>
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnSeleccionarTodos" runat="server" CssClass="btn btn-primary" OnClick="btnSeleccionarTodos_Click">
                            <i class="imoon imoon-search"></i>
                            <span class="text">Seleccionar todo</span>
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnMarcarComoLeido" runat="server" CssClass="btn btn-default" OnClick="btnMarcarComoLeido_Click">
                            <i class="imoon imoon-eraser"></i>
                            <span class="text">Marcar como leído</span>
                        </asp:LinkButton>
                        <div class="form-group">
                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="200" AssociatedUpdatePanelID="updBuscar">
                                <ProgressTemplate>
                                    <img src="../Content/img/app/Loading24x24.gif" style="margin-left: 10px" alt="loading" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%-- Fin Busqueda --%>

        <asp:UpdatePanel ID="updgrdBandeja" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnCargarDatos" runat="server" OnClick="btnCargarDatos_Click" Style="display: none" />
                <asp:HiddenField ID="hfMailID" runat="server" />
                <div class="mtop30 row pleft10 pright10">
                    <div class="col-sm-6">
                        <strong>Resultado de la búsqueda</strong>
                    </div>
                    <div class="col-sm-6 text-right">
                        <strong>Cantidad de registros:</strong>
                        <asp:Label ID="lblCantidadRegistros" runat="server" CssClass="badge">0</asp:Label>
                    </div>
                </div>
                <br />
                <br />
                <asp:GridView ID="grdBandeja"
                    runat="server"
                    AutoGenerateColumns="false"
                    AllowPaging="true" PageSize="30"
                    ItemType="DataTransferObject.AvisoNotificacionDTO"
                    SelectMethod="GetAvisoNotificaciones"
                    Style="border: none;"
                    CssClass="table table-bordered mtop5"
                    OnDataBound="grdBandeja_DataBound"
                    OnRowDataBound="grdBandeja_RowDataBound"
                    GridLines="None" Width="100%">
                    <HeaderStyle CssClass="grid-header" />
                    <RowStyle CssClass="grid-row" />
                    <AlternatingRowStyle BackColor="#efefef" />
                    <Columns>
                        <asp:HyperLinkField DataTextField="IdTramite" DataNavigateUrlFormatString="{0}" DataNavigateUrlFields="url" ControlStyle-CssClass="btn-link" HeaderText="Solicitud" ItemStyle-Width="70px" ItemStyle-CssClass="text-center"/>
                        <asp:BoundField DataField="NotificacionMotivo" HeaderText="Motivo" HeaderStyle-CssClass="text-center" ItemStyle-Width="80px" />
                        <asp:BoundField DataField="AsuntoMail" HeaderText="Asunto Mail" HeaderStyle-CssClass="text-center" ItemStyle-Width="500px" />
                        <asp:BoundField DataField="FechaAviso" DataFormatString="{0:d}" HeaderText="Fecha de Generación Aviso" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="70px" />
                        <asp:BoundField DataField="Domicilio" HeaderText="Domicilio" ItemStyle-CssClass="text-center min-width-80" HeaderStyle-CssClass="text-center" HtmlEncodeFormatString="true" />
                        <asp:BoundField DataField="FechaNotificacion" DataFormatString="{0:d}" HeaderText="Fecha de Notificación" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="70px" />
                        <asp:TemplateField ItemStyle-Width="15px" HeaderText="Visor" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDetalles" runat="server" ToolTip="Ver Mail" CssClass="link-local" data-target="#pnlDetalle"
                                    CommandArgument='<%#Eval("IdMail") + ","+ Eval("IdNotificacion") + ","+ Eval("IdTramite")%>' OnClick="lnkDetalles_Click"><i class="imoon-eye color-blue fs16"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Selector" ItemStyle-CssClass="text-center" ItemStyle-Width="40px">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkParent" runat="server" OnCheckedChanged="chkParent_CheckedChanged" AutoPostBack="true" Style="margin-left: 4px;" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" OnCheckedChanged="chkSelect_CheckedChanged" AutoPostBack="true" Text='<%#Eval("IdNotificacion")%>' CssClass="color: #ffffff00;" style="font-size:0"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerTemplate>
                        <asp:Panel ID="pnlpager" runat="server" Style="padding: 10px; text-align: center; border-top: solid 1px #e1e1e1">
                            <asp:LinkButton ID="cmdAnterior" runat="server" Text="<<" OnClick="cmdAnterior_Click"
                                CssClass="btn btn-sm btn-default" />
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
                                CssClass="btn btn-sm btn-default" />
                        </asp:Panel>
                    </PagerTemplate>
                    <EmptyDataTemplate>
                        <div>
                            <img src="../Content/img/app/NoRecords.png" />
                            <span class="mleft20">No se encontraron registros.</span>
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <%--modal de Errores--%>
    <div id="frmError" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top: -8px">Error</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon-remove-circle fs64" style="color: #f00"></i>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="updmpeInfo" runat="server" class="form-group">
                                    <ContentTemplate>
                                        <asp:Label ID="lblError" runat="server" Style="color: Black"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="modal-footer mleft20 mright20">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <%--Modal detalle mail--%>
    <div id="frmAvisoNotificacion" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" style="margin-top: -8px">Detalle del Aviso</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel runat="server" ID="visorMail">
                        <ContentTemplate>
                            <asp:Table ID="Table1" runat="server" HorizontalAlign="Center" CssClass="table table-bordered mtop5">
                                <asp:TableHeaderRow VerticalAlign="Middle" HorizontalAlign="Center">
                                    <asp:TableHeaderCell Visible="false">ID</asp:TableHeaderCell>
                                    <asp:TableHeaderCell Font-Bold="true">Email</asp:TableHeaderCell>
                                    <asp:TableHeaderCell Font-Bold="true">Asunto</asp:TableHeaderCell>
                                    <asp:TableHeaderCell Font-Bold="true">Fecha de Alta</asp:TableHeaderCell>
                                    <asp:TableHeaderCell Font-Bold="true">Fecha de Envio</asp:TableHeaderCell>
                                    <asp:TableHeaderCell Font-Bold="true">Prioridad</asp:TableHeaderCell>
                                    <asp:TableHeaderCell Font-Bold="true">Cant. de Intentos</asp:TableHeaderCell>
                                </asp:TableHeaderRow>
                                <asp:TableRow>
                                    <asp:TableCell Visible="false" ID="IDCorreo"></asp:TableCell>
                                    <asp:TableCell ID="Email"></asp:TableCell>
                                    <asp:TableCell ID="Asunto"></asp:TableCell>
                                    <asp:TableCell ID="FecAlta"></asp:TableCell>
                                    <asp:TableCell ID="FecEnvio"></asp:TableCell>
                                    <asp:TableCell ID="Prioridad"></asp:TableCell>
                                    <asp:TableCell ID="CantInt"></asp:TableCell>
                                </asp:TableRow>
                                <asp:TableHeaderRow VerticalAlign="Middle" HorizontalAlign="Center">
                                    <asp:TableHeaderCell ColumnSpan="8" Font-Bold="true">Mensaje</asp:TableHeaderCell>
                                </asp:TableHeaderRow>
                                <asp:TableRow>
                                    <asp:TableCell ID="CuerpoHTML" ColumnSpan="8" Width="500px">
                                        <iframe style="width: 100%; border-style: none" id="Message" runat="server"></iframe>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer mleft20 mright20">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->

    <script type="text/javascript">
        $(document).ready(function () {
            $("#page_content").hide();
            $("#Loading").show();
            $("#<%: txtNroSolicitud.ClientID %>").autoNumeric("init", { aSep: "", mDec: 0, vMax: "999999" });

            $("#<%: btnCargarDatos.ClientID %>").click();
        });

        function init_JS_updBuscar() {
            $("#<%: txtNroSolicitud.ClientID %>").autoNumeric("init", { aSep: "", mDec: 0, vMax: "999999" });
            $("#<%: ddlMotivos.ClientID %>").select2({
                placeholder: "(Todos)",
                allowClear: true,
            });
        }
        function finalizarCarga() {
            $("#Loading").hide();
            $("#page_content").show();
            return false;
        }

        function showfrmError() {
            $("#frmError").modal("show");
            return false;
        }

        function showfrmAvisoNotificacion() {
            $("#frmAvisoNotificacion").modal({ backdrop: 'static', keyboard: "false" });
            return false;
        }
    </script>
</asp:Content>
