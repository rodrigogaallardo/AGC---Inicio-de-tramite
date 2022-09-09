<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="DetalleEncomiendaObra.aspx.cs" Inherits="ConsejosProfesionales.Encomiendas.DetalleEncomiendaObra" %>


<asp:Content ContentPlaceHolderID="FeaturedContent" runat="server" ID="Featured">
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <%: Scripts.Render("~/bundles/autoNumeric") %>
    <%: Scripts.Render("~/bundles/select2") %>
    <%: Styles.Render("~/bundles/select2Css") %>
    <h2>
        <asp:Label ID="lblEncCertificado" runat="server" CssClass="h2"></asp:Label>
    </h2>
    <hr />
    <div class="form-horizontal form-group" style="margin-top: -10px">
        <div class="col-sm-10">
            <p class="mtop10">
                En esta pantalla podrá visualizar todos los datos de la Encomienda al momento de ser confirmada por profesional.<br />
            </p>
        </div>
    </div>
    <br />
    <%--Contenido--%>
    <div id="page_content">
        <h3>Datos Completos del trámite</h3>
        <hr />
        <div style="width: 100%">
            <div>
                <br />
                <div style="padding: 10px; padding-left: 16px; font-weight: bolder">
                    De la Obra:
                </div>
                <div style="padding: 1px; padding-left: 20px;">
                    N&uacute;mero de Trámite en Consejo:
                                    <asp:Label ID="lblEncNumero" runat="server" Text="0" Font-Bold="true"></asp:Label>
                </div>
                <div style="padding: 1px; padding-left: 20px;">
                    Fecha de Certificacion de la Encomienda:
                                    <asp:Label ID="lblEncFecha" runat="server" Text="0" Font-Bold="true"></asp:Label>
                </div>
                <div style="padding: 1px; padding-left: 20px;">
                    Seccion/Manzana/Parcela:
                                    <asp:Label ID="lblEncSMP" runat="server" Font-Bold="true"></asp:Label>
                </div>
                <div style="padding: 1px; padding-left: 20px;">
                    Calle y Puerta:
                                    <asp:Label ID="lblEncDireccion" runat="server" Font-Bold="true"></asp:Label>
                </div>
                <div style="padding: 1px; padding-left: 20px;">
                    N&uacute;mero de tr&aacute;mite de DGROC:
                                    <asp:Label ID="lblEncDGROC" runat="server" Font-Bold="true"></asp:Label>
                </div>
                <br />

                <div style="padding: 10px; padding-left: 16px; font-weight: bolder">
                    Del Profesional:
                </div>
                <div style="padding: 1px; padding-left: 20px;">
                    Nombre y Apellido:
                                    <asp:Label ID="lblEncProfnya" runat="server" Font-Bold="true"></asp:Label>
                </div>
                <div style="padding: 1px; padding-left: 20px;">
                    NºMatricula:
                                    <asp:Label ID="lblEncProfMatricula" runat="server" Font-Bold="true"></asp:Label>
                </div>
                <br />

                <div style="padding: 10px; padding-left: 16px; font-weight: bolder">
                    Del/los Comitente/s:
                </div>

                <asp:Repeater ID="repeater_EncComitentes" runat="server">
                    <ItemTemplate>
                        <div style="padding: 1px; padding-left: 20px;">
                            <asp:Label ID="lblEncTitName_type" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TipoPersona").ToString() == StaticClass.Constantes.TipoPersonaJuridica  ? "Razon Social: ": "Nombre/s y Apellido: " %>'></asp:Label><asp:Label ID="lblEncTitName" Text='<%# DataBinder.Eval(Container.DataItem, "ApellidoNomRazon") %>' runat="server" Font-Bold="true"></asp:Label>
                        </div>
                        <div style="padding: 1px; padding-left: 20px;">
                            <asp:Label ID="lblEncTitCUIT_type" runat="server" Text="CUIT: "></asp:Label><asp:Label ID="lblEncTitCUIT" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "cuit") %>' Font-Bold="true"></asp:Label>
                        </div>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
                
                        <div id="divEncRLegal" style="padding: 10px; padding-left: 16px; font-weight: bolder" runat="server" >
                            Representante Legal/Apoderado:
                        </div>
                <asp:Repeater ID="repeater_EncRLegal" runat="server" >
                    <ItemTemplate>

                        <div id="divEncRLegal_NombreyApellido" style="padding: 1px; padding-left: 20px;" runat="server" visible='<%# DataBinder.Eval(Container.DataItem, "ApellidoNombres").ToString() != null %>'>
                            Nombre y Apellido:
                                            <asp:Label ID="lblEncPJTitularesNA" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem, "ApellidoNombres") %>' runat="server"></asp:Label>
                        </div>
                        <div id="divEncRLegal_CUIT" style="padding: 1px; padding-left: 20px;" runat="server" visible='<%# DataBinder.Eval(Container.DataItem, "ApellidoNombres").ToString() != null %>'>
                            C.U.I.L. / C.U.I.T. :
                                            <asp:Label ID="lblEncPJTitularesCUIT" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem, "CUIT") %>' runat="server"></asp:Label>
                        </div>
                                                <%--<div id="divEncRLegal_Representante" style="padding: 1px; padding-left: 20px; font-weight:600" runat="server" visible='<%# DataBinder.Eval(Container.DataItem, "Razon_Social").ToString() != null %>' >
                                                    Representante de: <asp:Label ID="lblEncPJTitularesREP" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem, "Razon_Social") %>' runat="server"></asp:Label>
                                                    </div>--%>

                        <br />
                    </ItemTemplate>
                </asp:Repeater>
                <br />
            </div>
        </div>
        <asp:UpdatePanel ID="updPnlhistorial" runat="server">
            <ContentTemplate>
                <div style="width: 100%;">

                    <h3>Historial de Estados
                    </h3>
                    <hr />
                    <div style="padding: 10px; max-height: 250px; overflow: auto">
                        <asp:GridView
                            ID="grdHistorialEstados"
                            ItemType="DataTransferObject.EncomiendaExternaHistorialEstadosDTO"
                            runat="server"
                            AutoGenerateColumns="false"
                            AllowPaging="false"
                            Style="border: none; margin-top: 10px"
                            GridLines="None"
                            CssClass="table table-bordered mtop5"
                            CellPadding="3"
                            Width="100%">
                            <HeaderStyle CssClass="grid-header" />
                            <RowStyle CssClass="grid-row" />
                            <AlternatingRowStyle BackColor="#efefef" />

                            <Columns>

                                <asp:BoundField HeaderText="Fecha de Actualizacion" HeaderStyle-CssClass="text-center" DataField="fecha_modificacion" DataFormatString="{0:dd/MM/yyyy HH:mm}" ItemStyle-Width="150px" />

                                <asp:TemplateField HeaderText="Estado origen" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lnknom_estado_ant" runat="server" Text='<%# Item.nom_estado_viejo == "Aprobada por el consejo" ? "Validada por el Consejo" : Item.nom_estado_viejo %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Estado destino" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lnknom_estado_nuevo" runat="server" Text='<%# Item.nom_estado_nuevo == "Aprobada por el consejo" ? "Validada por el Consejo" : Item.nom_estado_nuevo %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField HeaderText="Usuario" DataField="UserName" HeaderStyle-HorizontalAlign="Left" />

                            </Columns>

                        </asp:GridView>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--Datos de la Encomienda--%>
        <h3>Datos de la Validaci&oacute;n</h3>
        <hr />
        <asp:UpdatePanel ID="updDatosEncomienda" runat="server">
            <ContentTemplate>
                <div style="width: 100%;">
                    <div style="padding: 10px;">
                        <div style="padding: 1px">
                            Tipo de Tr&aacute;mite:<asp:Label ID="lblTipoTramite" runat="server" Text="0" Font-Bold="true" Style="padding-left: 5px"></asp:Label>
                        </div>
                        <div style="padding: 1px">
                            Fecha de inicio:<asp:Label ID="lblFechaEncomienda" runat="server" Font-Bold="true" Style="padding-left: 5px"></asp:Label>
                        </div>
                        <div style="padding: 1px">
                            Estado Actual:<asp:Label ID="lblEstado" runat="server" Font-Bold="true" Style="padding-left: 5px"></asp:Label>
                        </div>

                        <div style="padding: 1px" id="divEncMotivoRechazo" runat="server" visible="false">
                            Motivo del Rechazo:<asp:Label ID="lblEncMotivoRechazo" runat="server" Font-Bold="true" Style="padding-left: 5px"></asp:Label>
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
                <br />
                <div class="form-inline">
                    <div class="row mleft10 mtop10">
                        <asp:Label ID="lblCambiarEstado" runat="server" CssClass="control-label col-sm-2 mtop5 text-right" Text="Cambiar al estado:"></asp:Label>
                        <div class="col-sm-4 mtop5">
                            <asp:DropDownList ID="ddlCambiarEstado" runat="server" Width="100%"></asp:DropDownList>

                        </div>
                    </div>

                    <div class="row mleft10 mtop10">

                        <label class="control-label col-sm-2 mtop5 text-right"></label>
                        <div class="col-sm-8 mtop5">

                            <asp:LinkButton ID="btnNuevaBusqueda" runat="server" CssClass="btn btn-default" PostBackUrl="~/Encomiendas/ValidacionEncomiendaObra.aspx">
                            <i class="imoon imoon-search"></i>
                            <span class="text">Nueva búsqueda</span>
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnCambiarEstado" runat="server" CssClass="btn btn-primary" OnClick="btnCambiarEstado_Click">
                            <i class="imoon imoon-refresh"></i>
                            <span class="text">Actualizar</span>
                            </asp:LinkButton>



                            <div class="form-group">
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="200" AssociatedUpdatePanelID="updBotonesAccion">
                                    <ProgressTemplate>
                                        <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" style="margin-left: 10px" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </div>
                    </div>


                    <div id="DivTxtEncMotivoRechazo" class="row mleft10 mtop10">
                        <asp:Label ID="lblMotivoRechazo" CssClass="control-label col-sm-2 mtop5 text-right" Text="Motivo" runat="server"></asp:Label>
                        <div class="col-sm-8 mtop5">
                            <asp:TextBox ID="txtEncMotivoRechazo" Text="" Style="overflow: auto; max-width: 90%; max-height: 100px" TextMode="MultiLine" Rows="10" Width="90%" runat="server" MaxLength="1000" Height="100"></asp:TextBox>
                        </div>
                    </div>
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
        </div>
    </div>
    <asp:Panel ID="pnlSuccess" runat="server" CssClass="modal fade" DefaultButton="btnAceptarSuccess">
        <div class="modal-dialog">

            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">X</span><span class="sr-only">Close</span></button>
                </div>
                <div class="modal-body">

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

                </div>
            </div>
        </div>
    </asp:Panel>

    <script type="text/javascript">

        $(document).ready(function () {
            $("#DivTxtEncMotivoRechazo").hide();
            init_JS_updBuscarUbicacion();
            //ddlCambiarEstado
            $('#<%: ddlCambiarEstado.ClientID %>').on("change", function () {
                var motivoRechazar = ($("#<%: ddlCambiarEstado.ClientID %>").val());
                if (motivoRechazar == 5) {
                    $("#DivTxtEncMotivoRechazo").show();
                }
                else {
                    $("#DivTxtEncMotivoRechazo").hide();
                }
                return false;
            })
        });


        function init_JS_updBuscarUbicacion() {

            $("#<%: ddlCambiarEstado.ClientID %>").select2({
                placeholder: "(Seleccione el estado)",
                allowClear: true,
            });


        }

        function showfrmError() {

            $("#frmError").modal("show");
            return false;

        }
    </script>


</asp:Content>


