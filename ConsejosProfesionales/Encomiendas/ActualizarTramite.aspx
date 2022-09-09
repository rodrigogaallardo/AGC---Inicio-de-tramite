<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActualizarTramite.aspx.cs" MasterPageFile="~/Site.Master" Inherits="ConsejosProfesionales.Encomiendas.ActualizarTramite" %>

<asp:Content ContentPlaceHolderID="FeaturedContent" runat="server" ID="Featured">
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <asp:Panel ID="pnlPage" runat="server" CssClass="PageContainer">

        <asp:HiddenField ID="hid_id_encomienda" runat="server" />

        <h3>Datos Completos del trámite</h3>
        <hr />
        <p style="margin: auto; padding: 10px; line-height: 20px">
            En esta pantalla podrá visualizar todos los datos del Anexo Técnico al momento de ser confirmada por profesional.<br />
        </p>

        <asp:Panel ID="pnlVisorPDF" runat="server">
            <asp:Label ID="lbl_url_reporte" runat="server" Width="800px" Visible="false"></asp:Label>
            <asp:HiddenField ID="hid_url_reporte" runat="server" />
            <%--iframe se genera dinamicamente porque en algunos browser no muestra pdf con refresh o load--%>
            <iframe id="idFramePdf" width="100%" height="600px" frameborder="1" scrolling="auto" src=""></iframe>

        </asp:Panel>

        <br />

        <asp:UpdatePanel ID="updPnlhistorial" runat="server">
            <ContentTemplate>

                <div style="width: 100%;">

                    <h3>Historial de Estados
                    </h3>
                    <hr />
                    <div style="padding: 10px; max-height: 250px; overflow: auto">
                        <asp:GridView ID="grdHistorialEstados"
                            runat="server"
                            AutoGenerateColumns="false"
                            AllowPaging="false"
                            Style="border: none; margin-top: 10px"
                            GridLines="None"
                            CssClass="table table-bordered mtop5"
                            CellPadding="3"
                            Width="100%"
                            ItemType="DataTransferObject.EncomiendaExternaHistorialEstadosDTO">
                            <HeaderStyle CssClass="grid-header" />
                            <AlternatingRowStyle BackColor="#efefef" />

                            <Columns>

                                <asp:BoundField HeaderText="Fecha de Act." DataField="fecha_modificacion" HeaderStyle-CssClass="text-center" DataFormatString="{0:dd/MM/yyyy HH:mm}" ItemStyle-Width="150px" />
                                <asp:BoundField HeaderText="Estado origen" DataField="nom_estado_viejo" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Estado destino" DataField="nom_estado_nuevo" HeaderStyle-HorizontalAlign="Left" />
                                <asp:BoundField HeaderText="Usuario" DataField="UserName" HeaderStyle-HorizontalAlign="Left" />

                            </Columns>

                        </asp:GridView>
                    </div>
                </div>
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>

        <%--Datos de la Encomienda--%>
        <h3>Datos del Anexo Técnico
        </h3>
        <hr />
        <asp:UpdatePanel ID="updDatosEncomienda" runat="server">
            <ContentTemplate>
                <div style="width: 100%;">

                    <div style="padding: 10px;">
                        <div style="padding: 1px">
                            N&uacute;mero de tr&aacute;mite:<asp:Label ID="lblNroEncomienda" runat="server" Text="0" Font-Bold="true" Style="padding-left: 5px"></asp:Label>
                        </div>
                        <div style="padding: 1px">
                            Tipo de Tr&aacute;mite:<asp:Label ID="lblTipoTramite" runat="server" Text="0" Font-Bold="true" Style="padding-left: 5px"></asp:Label>
                        </div>
                        <div style="padding: 1px">
                            Fecha de inicio:<asp:Label ID="lblFechaEncomienda" runat="server" Font-Bold="true" Style="padding-left: 5px"></asp:Label>
                        </div>
                        <div style="padding: 1px">
                            Estado Actual:<asp:Label ID="lblEstado" runat="server" Font-Bold="true" Style="padding-left: 5px"></asp:Label>
                        </div>
                        <div style="padding: 1px">
                            Usuario de Creaci&oacute;n:<asp:Label ID="lblUsuarioCreacion" runat="server" Font-Bold="true" Style="padding-left: 5px"></asp:Label>
                        </div>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>


        <%--Acciones--%>
        <asp:UpdatePanel ID="updBotonesAccion" runat="server" >
            <ContentTemplate>
                <h3>Acciones sobre la encomienda</h3>
                <hr />
                <asp:Panel ID="pnlBotonesAccion" runat="server">

                    <div style="width: 100%; padding-top: 10px;">

                        <table border="0">
                            <tr>
                                <td>Cambiar al estado:</td>
                                <td class="pleft10">
                                    <asp:DropDownList ID="ddlCambiarEstado" runat="server" Width="300px" CssClass="form-control"></asp:DropDownList>

                                </td>
                                <td class="pleft10">
                                    <asp:LinkButton ID="btnCambiarEstado" runat="server" CssClass="btn btn-primary" OnClick="btnCambiarEstado_Click"
                                        OnClientClick="return ocultarActualizar();">
                                        <i class="imoon imoon-refresh"></i>
                                        <span class="text">Actualizar</span>
                                    </asp:LinkButton>
                                </td>
                                <td class="pleft10">
                                    
                                    <asp:LinkButton ID="btnNuevaBusqueda" runat="server"  CssClass="btn btn-default" PostBackUrl="~/Encomiendas/SearchEncomiendas.aspx">
                                                                                <i class="imoon imoon-search"></i>
                                        <span class="text">Nueva b&uacute;squeda</span>
                                    </asp:LinkButton>
                                </td>
                                <td class="pleft10">
                                    <asp:HyperLink ID="btnImprimirComprobante" runat="server"  CssClass="btn btn-success" Target="_blank">
                                        <i class="imoon imoon-file-pdf"></i>
                                        <span class="text">Imprimir Certificaci&oacute;n</span>
                                    </asp:HyperLink>
                                </td>

                            </tr>
                            <tr>
                                <td colspan="5">
                                    <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="updBotonesAccion" runat="server" DisplayAfter="0">
                                        <ProgressTemplate>
                                            <div style="width: 100%; text-align: center; overflow: hidden">
                                                <img src='<%: ResolveClientUrl("~/Common/Images/Controles/Loading24x24.gif") %>' alt="" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                        </table>

                    </div>

                    <asp:Panel ID="pnlExportarXML" runat="server" Style="text-align: center; padding: 20px" Width="900px">
                        <table border="0" style="margin: auto">
                            <tr>
                                <td>
                                    <img src='<%: ResolveClientUrl("~/Common/Images/Controles/xml64x64.png") %>' alt="xml" />
                                </td>
                                <td>
                                    <asp:HyperLink ID="lnkExportarEncomiendaXML" runat="server" Text="Descargar Anexo a XML"
                                        Target="_blank" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>


        <asp:Panel ID="pnlInformacion" runat="server" CssClass="modal fade">
            <div class="modal-dialog">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Cerrar</span></button>
                    </div>
                    <div class="modal-body">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 80px">
                                    <asp:Image ID="imgmpeInfo" runat="server" ImageUrl="~/Common/Images/Controles/info64x64.png" />
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
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                                </td>
                            </tr>
                        </table>

                    </div>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pnlSuccess" runat="server" CssClass="modal fade">
            <div class="modal-dialog">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">X</span><span class="sr-only">Cerrar</span></button>
                    </div>
                    <div class="modal-body">

                        <table style="width: 100%">
                            <tr>
                                <td style="width: 80px">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Common/Images/Controles/ok32x32.png" />
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
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                                </td>
                            </tr>
                        </table>

                    </div>
                </div>
            </div>

        </asp:Panel>

        <asp:Panel ID="pnlMensajeError" runat="server" CssClass="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Cerrar</span></button>
                    </div>
                    <div class="modal-body">

                        <table>
                            <tr>
                                <td>
                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Common/Images/Controles/error32x32.png" />
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lblMensajeError" runat="server"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <script type="text/javascript">
            $(document).ready(function () {
                cargarFramePdf();
            });

            function cargarFramePdf() {
                var name = '<%: hid_url_reporte.ClientID %>';
                var reporte = $("#" + name).val();
                $("#idFramePdf").attr("src", reporte);
            }

            function moverFinal() {
                var altura = $(document).height();
                $("html,body").animate({ scrollTop: altura + "px" });
                return false;
            }

            function ocultarActualizar() {
                $("#<%: btnCambiarEstado.ClientID %>").hide();
                return true;
            }
        </script>


    </asp:Panel>

</asp:Content>
