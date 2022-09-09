<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AvisosSolicitud.ascx.cs" Inherits="SSIT.Solicitud.Habilitacion.Controls.AvisosSolicitud" %>

<asp:UpdatePanel ID="var" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:HiddenField ID="hid_id_solicitud" runat="server" />
        <asp:HiddenField ID="hid_avisos_collapse" runat="server" Value="false" />
        <asp:HiddenField ID="hid_error_message" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>


<asp:Panel ID="pnlAvisos" runat="server">
    <div id="box_Avisos" class="accordion-group widget-box" style="background-color: #ffffff">
        <div class="accordion-heading">
            <a id="btnUpDownAvisos" data-parent="#collapse-group" href="#collapseAvisos"
                data-toggle="collapse" onclick="btnUpDownAvisos_click(this)">
                <div class="widget-title">
                    <span class="icon"><i class="imoon-bell" style="color: #344882"></i></span>
                    <h5>Avisos</h5>
                    <span class="btn-right"><i class="imoon imoon-chevron-up" style="color: #344882"></i></span>
                </div>
            </a>
        </div>
        <div class="accordion-body collapse in" id="collapseAvisos">
            <div class="widget-content">
                <asp:UpdatePanel ID="updPnlNotificaciones" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:HiddenField ID="hfMailID" runat="server" />
                        <asp:GridView ID="grdMails" runat="server" AutoGenerateColumns="false" GridLines="None" CssClass="table table-bordered table-striped table-hover with-check"
                            DataKeyNames="Mail_ID" ItemType="DataTransferObject.EMailDTO">
                            <Columns>
                                <asp:BoundField Visible="false" DataField="Mail_ID" HeaderText="ID" />
                                <asp:BoundField DataField="Mail_Asunto" HeaderText="Asunto" ItemStyle-Width="300px" />
                                <asp:BoundField DataField="Mail_Email" HeaderText="E-Mail" ItemStyle-Width="100px" />
                                <asp:BoundField DataField="Mail_Fecha" HeaderText="Fecha" DataFormatString="{0:d}" ItemStyle-Width="70px" />
                                <asp:TemplateField ItemStyle-Width="15px" HeaderText="Ver" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDetalles" runat="server" ToolTip="Ver Detalle" CssClass="link-local" data-target="#pnlDetalle"
                                            CommandArgument='<%#Eval("Mail_ID")%>' OnClick="lnkDetalles_Click"><i class="imoon-eye color-blue fs24"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Panel ID="pnlNotDataFound" runat="server" CssClass="GrayText-1 ptop10">
                                    <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />No se encontraron registros.
                                </asp:Panel>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Panel>

<%--Modal mensajes de error--%>
<div id="frmAvisoNotificacion" class="modal fade" role="dialog">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title" style="margin-top: -8px">Detalle del Aviso</h4>
            </div>
            <div class="modal-body">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:Table ID="Table1" runat="server" HorizontalAlign="Center" CssClass="table table-bordered mtop5">
                            <asp:TableHeaderRow VerticalAlign="Middle" HorizontalAlign="Center">
                                <asp:TableHeaderCell Visible="false">ID</asp:TableHeaderCell>
                                <asp:TableHeaderCell Font-Bold="true">Email</asp:TableHeaderCell>
                                <asp:TableHeaderCell Font-Bold="true">Asunto</asp:TableHeaderCell>
                                <%--<asp:TableHeaderCell Font-Bold="true">Proceso</asp:TableHeaderCell>--%>
                                <asp:TableHeaderCell Font-Bold="true">Fecha de Alta</asp:TableHeaderCell>
                                <asp:TableHeaderCell Font-Bold="true">Fecha de Envio</asp:TableHeaderCell>
                                <asp:TableHeaderCell Font-Bold="true">Prioridad</asp:TableHeaderCell>
                                <asp:TableHeaderCell Font-Bold="true">Cant. de Intentos</asp:TableHeaderCell>
                            </asp:TableHeaderRow>
                            <asp:TableRow>
                                <asp:TableCell Visible="false" ID="IDCorreo"></asp:TableCell>
                                <asp:TableCell ID="Email"></asp:TableCell>
                                <asp:TableCell ID="Asunto"></asp:TableCell>
                                <%--<asp:TableCell ID="Proceso"></asp:TableCell>--%>
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
        inicializar_avisos_btnUpDown_collapse();
    });


    function showfrmAvisoNotificacion() {

        //divHMLBody.innerHTML = obj;
        $("#frmAvisoNotificacion").modal("show");
        return false;

    }
    function inicializar_avisos_btnUpDown_collapse() {
        //cuando cargua por primera vez, se muestra expandido o no segun el seteo de los atributos del control
        var colapsar = $('#<%=hid_avisos_collapse.ClientID%>').attr("value");

        var obj = $("#btnUpDownAvisos")[0];
        var href_collapse = $(obj).attr("href");

        if ($(href_collapse).attr("id") != undefined) {
            if ($(href_collapse).css("height") == "0px") {
                if (colapsar == "true") {
                    $(href_collapse).collapse();
                    $(obj).find(".icon-chevron-down").switchClass("icon-chevron-down", "icon-chevron-up", 0);
                }
            }
            else {
                if (colapsar == "false") {
                    $(href_collapse).collapse();
                    $(obj).find(".icon-chevron-up").switchClass("icon-chevron-up", "icon-chevron-down", 0);
                }
            }
        }
    }

    function btnUpDownAvisos_click(obj) {
        var href_collapse = $(obj).attr("href");
        if ($(href_collapse).attr("id") != undefined) {
            if ($(href_collapse).css("height") == "0px") {
                $(obj).find(".icon-chevron-down").switchClass("icon-chevron-down", "icon-chevron-up", 0);
            }
            else {
                $(obj).find(".icon-chevron-up").switchClass("icon-chevron-up", "icon-chevron-down", 0);
            }
        }
    }
</script>
