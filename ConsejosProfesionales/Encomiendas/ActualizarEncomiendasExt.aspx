<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActualizarEncomiendasExt.aspx.cs" MasterPageFile="~/Site.Master" Inherits="ConsejosProfesionales.Encomiendas.ActualizarEncomiendasExt" %>

<asp:Content ContentPlaceHolderID="FeaturedContent" runat="server" ID="Featured">
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <asp:Panel ID="pnlPage" runat="server" CssClass="PageContainer">


        <%--Contenido--%>
        <asp:Panel ID="pnlContenido" runat="server" CssClass="box-contenido" BackColor="White">


            <asp:HiddenField ID="hid_id_encomienda" runat="server" />

            <asp:Panel ID="pnlInfoPaso" runat="server" Width="940px">
                <h3>Datos Completos del trámite</h3>
                <hr />
                <p style="margin: auto; padding: 10px; line-height: 20px">
                    En esta pantalla podrá visualizar todos los datos de la Encomienda al momento de ser confirmada por profesional.<br />
                </p>

                <div id="Div1" style="margin: auto; padding: 10px; line-height: 20px">
                    <asp:Repeater ID="repeater_certificados" runat="server">
                        <ItemTemplate>

                            <asp:LinkButton ID="lnkCertificado" runat="server" CommandArgument='<%# Eval("id_certificado") %>' OnCommand="lnkCertificado_Command">
                                <asp:Image ID="imagen1" runat="server" ImageUrl="~/Common/Images/Controles/pdf24x24.png" />
                                <span class="text">Certificado-<%# Eval("id_certificado").ToString() %></span>
                            </asp:LinkButton>

                        </ItemTemplate>
                    </asp:Repeater>
                </div>

                <asp:Panel ID="pnlVisorPDF" runat="server">
                    <asp:Label ID="lbl_url_reporte" runat="server" Width="800px" Visible="false"></asp:Label>
                    <asp:HiddenField ID="hid_url_reporte" runat="server" />
                    <%--iframe se genera dinamicamente porque en algunos browser no muestra pdf con refresh o load--%>
                    <iframe id="idFramePdf" width="100%" height="1100px"
                        frameborder="1" scrolling="auto" src=""></iframe>

                </asp:Panel>

                <br />
                <br />

                <asp:UpdatePanel ID="updPnlhistorial" runat="server">
                    <ContentTemplate>

                        <div style="width: 100%;">

                            <h3>Historial de Estados
                            </h3>
                            <hr />
                            <div style="padding: 10px; max-height: 250px; overflow: auto">
                                <asp:GridView ID="grdHistorialEstados"
                                    runat="server" AutoGenerateColumns="false"
                                    ItemType="DataTransferObject.EncomiendaExternaHistorialEstadosDTO"
                                    AllowPaging="false" Style="border: none; margin-top: 10px" GridLines="None" CellPadding="3" Width="100%">
                                    <HeaderStyle CssClass="grid-header" />
                                    <RowStyle CssClass="grid-row" />
                                    <AlternatingRowStyle BackColor="#efefef" />

                                    <Columns>

                                        <asp:BoundField HeaderText="Fecha de Act." DataField="fecha_modificacion" DataFormatString="{0:dd/MM/yyyy HH:mm}" ItemStyle-Width="120px" />
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
                <h3>Datos de la Encomienda
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
                            </div>
                        </div>


                    </ContentTemplate>
                </asp:UpdatePanel>


                <%--Acciones--%>
                <asp:UpdatePanel ID="updBotonesAccion" runat="server">
                    <ContentTemplate>
                        <h3>Acciones sobre la encomienda
                        </h3>
                        <hr />
                        <asp:MultiView ID="mvBotonesAccion" runat="server" ActiveViewIndex="0">

                            <asp:View ID="vwBotonesAccion" runat="server">

                                <br />
                                <div style="width: 100%; padding-top: 10px;">

                                    <table border="0" cellpadding="5">
                                        <tr>
                                            <td>Cambiar al estado:</td>
                                            <td>
                                                <asp:DropDownList ID="ddlCambiarEstado" runat="server" Width="300px"></asp:DropDownList></td>
                                            <td>
                                                <asp:LinkButton ID="btnCambiarEstado" runat="server" CssClass="btn btn-primary" OnClick="btnCambiarEstado_Click">
                            <i class="imoon imoon-refresh"></i>
                            <span class="text">Actualizar</span>
                                                </asp:LinkButton></td>
                                            <td>
                                                <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="updBotonesAccion" runat="server" DisplayAfter="0">
                                                    <ProgressTemplate>
                                                        <div style="width: 100%; text-align: center; overflow: hidden">
                                                            <img src="../Common/Images/Controles/Loading24x24.gif" alt="" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>

                                            </td>
                                            <td style="vertical-align: middle">
                                                <asp:LinkButton ID="btnNuevaBusqueda" runat="server" CssClass="btn btn-default" PostBackUrl="~/Encomiendas/SearchEncomiendasExt.aspx">
                            <i class="imoon imoon-search"></i>
                            <span class="text">Nueva búsqueda</span>
                                                </asp:LinkButton>
                                            </td>
                                            <td style="padding-left: 20px">
                                                <asp:HyperLink ID="btnImprimirComprobante" runat="server" Text="Imprimir Certificación" NavigateUrl="~/Reportes/ImprimirComprobante.aspx" CssClass="btnpdf24" Width="162px" Target="_blank"></asp:HyperLink>
                                            </td>

                                        </tr>
                                    </table>


                                </div>

                                <asp:Panel ID="pnlExportarXML" runat="server" Style="text-align: center; padding: 20px" Width="900px">
                                    <table border="0" style="margin: auto">
                                        <tr>
                                            <td>
                                                <img src="../Common/Images/Controles/xml64x64.png" alt="xml" />
                                            </td>
                                            <td>
                                                <asp:HyperLink ID="lnkExportarEncomiendaXML" runat="server" Text="Descargar Encomienda a XML"
                                                    Target="_blank" />
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>

                            </asp:View>

                            <asp:View ID="vwMensajeInfo" runat="server">
                                <br />
                                <center>
                                    <asp:Panel ID="pnlMensajeInfo" runat="server"
                                        BorderWidth="1px" BorderColor="LightGray" Width="450px" BackColor="#efefef">
                                        <table cellpadding="7" style="width: 100%; border: 10px; border-color: Red">
                                            <tr>
                                                <td style="width: 80px">
                                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/Common/Images/Controles/info64x64.png" />

                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMensajeInfo" runat="server" Style="color: Black"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="text-align: center">
                                                    <asp:Button ID="btnMensajeInfo" runat="server" CssClass="btnOK"
                                                        Text="Aceptar" Width="100px" OnClick="btnMensajeVerIndex0_Click" />
                                                </td>
                                            </tr>
                                        </table>

                                    </asp:Panel>
                                </center>
                                <br />
                            </asp:View>

                            <asp:View ID="vwMensajeError" runat="server">
                                <br />
                                <center>
                                    <asp:Panel ID="pnlMensajeError" runat="server"
                                        BorderWidth="2px" BorderColor="LightGray" Width="450px" BackColor="#efefef">
                                        <table cellpadding="7" style="width: 100%; border: 10px; border-color: Red">
                                            <tr>
                                                <td style="width: 80px">
                                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Common/Images/Controles/error64x64.png" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMensajeError" runat="server" Style="color: Black"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="text-align: center">
                                                    <asp:Button ID="btnMensajeError" runat="server" CssClass="btnOK"
                                                        Text="Aceptar" Width="100px" OnClick="btnMensajeVerIndex0_Click" />
                                                </td>
                                            </tr>
                                        </table>

                                    </asp:Panel>
                                </center>
                                <br />
                            </asp:View>

                        </asp:MultiView>

                        <asp:HiddenField ID="finalDocumento" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>

                <asp:Panel ID="pnlInformacion" runat="server" CssClass="modalPopup" Style="display: none"
                    Width="450px" DefaultButton="btnAceptarInformacion">
                    <table cellpadding="7" style="width: 100%">
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
                                    Width="100px" OnClientClick="return mostrarPanelError(false);" />


                            </td>
                        </tr>
                    </table>
                </asp:Panel>

                <asp:Panel ID="pnlSuccess" runat="server" CssClass="modalPopup" Style="display: none"
                    Width="450px" DefaultButton="btnAceptarSuccess">
                    <table cellpadding="7" style="width: 100%">
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
                                    Width="100px" OnClientClick="return mostrarPanelSuccess(false);" />


                            </td>
                        </tr>
                    </table>
                </asp:Panel>


            </asp:Panel>
        </asp:Panel>


    </asp:Panel>

    <script type="text/javascript">

        $(document).ready(function () {
            cargarFramePdf();
        });

        function cargarFramePdf() {
            var reporte = $('#<%: hid_url_reporte.ClientID %>').val();
            $("#idFramePdf").attr("src", reporte);
        }

        function moverFinal() {
            var altura = $(document).height();
            $("html,body").animate({ scrollTop: altura + "px" });

        }

    </script>
</asp:Content>
