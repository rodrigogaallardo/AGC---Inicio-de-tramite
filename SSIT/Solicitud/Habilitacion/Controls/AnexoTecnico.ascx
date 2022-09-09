<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AnexoTecnico.ascx.cs" Inherits="SSIT.Solicitud.Habilitacion.Controls.AnexoTecnico" %>
<%@ Register Src="~/Solicitud/Habilitacion/Controls/Rubros.ascx" TagPrefix="uc1" TagName="Rubros" %>
<%@ Register Src="~/Solicitud/Habilitacion/Controls/RubrosCN.ascx" TagPrefix="uc1" TagName="RubrosCN" %>

<asp:HiddenField ID="hid_id_solicitudRef" runat="server" />
<div id="box_titulares" class="accordion-group widget-box" style="background-color: #ffffff">
    <div class="accordion-heading">
        <a id="titulares_btnUpDown" data-parent="#collapse-group" href="#collapse_AnexoTecnico"
            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

            <div class="widget-title">
                <span class="icon"><i class="imoon imoon-file" style="color: #344882"></i></span>
                <h5>
                    <asp:Label ID="lbl_titulares_tituloControl" runat="server" Text="Anexo Técnico"></asp:Label></h5>
                <span class="btn-right"><i class="imoon imoon-chevron-up" style="color: #344882"></i></span>
            </div>
        </a>
    </div>
    <div class="accordion-body collapse in" id="collapse_AnexoTecnico">
        <div style="margin: 10px">
            <asp:UpdatePanel ID="updEncomienda" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div style="padding: 0px 10px 10px 10px; width: auto">

                        <div class="mtop10">
                            <strong>Listado de Anexos Técnico</strong>
                        </div>
                        <asp:GridView ID="gridEncomienda_db"
                            runat="server"
                            AutoGenerateColumns="false"
                            AllowPaging="false"
                            Style="border: none;"
                            CssClass="table table-bordered mtop5"
                            GridLines="None" Width="100%"
                            DataKeyNames="IdEncomienda"
                            OnRowDataBound="gridEncomienda_db_RowDataBound"
                            OnDataBound="gridEncomienda_db_DataBound"
                            ItemType="DataTransferObject.EncomiendaDTO">
                            <HeaderStyle CssClass="grid-header" />
                            <RowStyle CssClass="grid-row" />
                            <AlternatingRowStyle BackColor="#efefef" />
                            <Columns>
                                <asp:BoundField DataField="IdEncomienda" HeaderText="Anexo" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="70px" />
                                <asp:BoundField DataField="tipoAnexo" HeaderText="Tipo" HeaderStyle-CssClass="text-center " ItemStyle-CssClass="text-center" />
                                <asp:BoundField DataField="CreateDate" DataFormatString="{0:d}" HeaderText="Fecha" HeaderStyle-CssClass="text-center" ItemStyle-Width="90px" />
                                <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" HeaderText="Rubros">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkRubro" CssClass="link-local" runat="server" data-id_encomienda='<%# Item.IdEncomienda %>' title="Ver rubros cargados" OnClientClick="return showModalRubros(this);">
                                <span class="icon"><i class="imoon-eye color-blue fs24"></i></span>
                                        </asp:LinkButton>
                                            <%--Modal Rubros --%>
                                            <div id='<%#"MostrarRubros" + Item.IdEncomienda %>'  class="modal fade" role="dialog" style="display:none;">
                                                <div class="modal-dialog">
                                                    <div class="modal-content" style="width:900px; margin-left:-150px">
                                                        <div class="modal-header">
                                                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                                            <h4 class="modal-title" style="margin-top: -8px">Datos de los Rubros</h4>
                                                        </div>
                                                        <div class="modal-body">
                                                            <asp:UpdatePanel ID="udpRubros" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                        <uc1:Rubros runat="server" ID="Rubros"/>
                                                                        <uc1:RubrosCN runat="server" ID="RubrosCN"/>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>                                                           
                                                        </div>
                                                    </div>
                                                    <!-- /.modal-content -->
                                                </div>
                                                <!-- /.modal-dialog -->
                                            </div>
                                            <!-- /.modal -->
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="TipoTramiteDescripcion" HeaderText="Trámite" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="80px" />
                                <asp:BoundField DataField="Estado.NomEstado" HeaderText="Estado" HeaderStyle-CssClass="text-center " ItemStyle-CssClass="text-center" />

                                <asp:TemplateField HeaderStyle-CssClass="text-center min-width-150" HeaderText="Profesional">
                                    <ItemTemplate>
                                        <%# Item.ProfesionalDTO.Apellido + ", " + Item.ProfesionalDTO.Nombre %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" HeaderText="Anexo">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkImprimirAnexo" title="Descargar Anexo" CssClass="link-local" runat="server" Target="_blank">
                                <span class="icon"><i class="imoon-download fs24 color-blue"></i></span>
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" HeaderText="Certificado"  >
                                    <ItemTemplate>
                                        <asp:Panel runat="server" Visible='<%# Item.IdEstado != (int)StaticClass.Constantes.Encomienda_Estados.Anulada %>' >
                                        <asp:HyperLink ID="lnkImprimirCertificado" title="Descargar Certificado" CssClass="link-local" runat="server" Target="_blank">
                                         <span class="icon"><i class="imoon-download fs24 color-blue"></i></span>
                                        </asp:HyperLink>
                                        </asp:Panel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-CssClass="text-center link-local" HeaderStyle-CssClass="text-center" HeaderText="Acción">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkAnular" runat="server" CssClass="link-local" data-id_encomienda='<%# Eval("IdEncomienda") %>' OnClientClick="return showfrmConfirmarAnularAnexo(this);">
                            <span class="icon"><i class="imoon imoon-close fs24 color-blue"></i></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="mtop10">
                                    <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                                    No se relacionaron anexos.
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>


                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
<%--Confirmar Anular Encomienda--%>
<div id="frmConfirmarAnularEncomienda1" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title" style="margin-top: -8px">Anular Anexo</h4>
            </div>
            <div class="modal-body">
                <table style="border-collapse: separate; border-spacing: 5px">
                    <tr>
                        <td style="text-align: center; vertical-align: text-top">
                            <i class="imoon imoon-remove-circle fs64 color-blue"></i>
                        </td>
                        <td style="vertical-align: middle; margin-left: 10px">
                            <asp:Label runat="server">
                                Seleccionaste la opción de <strong>Anular</strong> el Anexo. Una vez confirmada la anulación, el mismo perderá validez y <strong>ya no podrá ser modificado</strong>, <u><em>desvinculándolo</em></u> de la Solicitud que le dio orgien.<br />
                                <br />
                                ¿ Est&aacute; seguro de anular el Anexo Técnico iniciado por el profesional  ?</asp:Label>
                        </td>
                    </tr>
                </table>

            </div>
            <div class="modal-footer mleft20 mright20">

                <asp:UpdatePanel ID="updConfirmarAnularAnexo1" runat="server">
                    <ContentTemplate>
                        <asp:HiddenField ID="hid_encomienda_anular1" runat="server" />


                        <div class="form-inline">
                            <div class="form-group">
                                <asp:UpdateProgress ID="UpdateProgress11" AssociatedUpdatePanelID="updConfirmarAnularAnexo1"
                                    runat="server" DisplayAfter="200" DynamicLayout="false">
                                    <ProgressTemplate>
                                        <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" alt="" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>

                            <div id="pnlBotonesConfirmacionAnularAnexo1" class="form-group">
                                <asp:Button ID="btnConfirmarAnularAnexo" runat="server" CssClass="btn btn-primary" Text="Sí"
                                    OnClientClick="hidefrmConfirmarAnularEncomienda1(this);" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">No</button>
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>
</div>

<div id="frmConfirmarAnularEncomienda2" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title" style="margin-top: -8px">Anular Anexo</h4>
            </div>
            <div class="modal-body">
                <table style="border-collapse: separate; border-spacing: 5px">
                    <tr>
                        <td style="text-align: center; vertical-align: text-top">
                            <i class="imoon imoon-remove-circle fs64 color-blue"></i>
                        </td>
                        <td style="vertical-align: middle">
                            <label class="mleft10">
                                ¿ Est&aacute; seguro que querés <strong>ANULAR</strong> este Anexo ?</label>
                        </td>
                    </tr>
                </table>

            </div>
            <div class="modal-footer mleft20 mright20">

                <asp:UpdatePanel ID="updConfirmarAnularAnexo" runat="server">
                    <ContentTemplate>

                        <asp:HiddenField ID="hid_encomienda_anular2" runat="server" />

                        <div class="form-inline">
                            <div class="form-group">
                                <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="updConfirmarAnularAnexo"
                                    runat="server" DisplayAfter="200" DynamicLayout="false">
                                    <ProgressTemplate>
                                        <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" alt="" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>


                            <div id="pnlBotonesConfirmacionAnularAnexo2" class="form-group">
                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Sí" OnClick="btnConfirmarAnularAnexo_Click"
                                    OnClientClick="ocultarBotonesConfirmacionAnularAnexo();" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">No</button>
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>
</div>
<!-- /.modal -->


<script type="text/javascript">
    var idEncomiendaAnula1;
    var idEncomiendaAnular2;

    function ocultarBotonesConfirmacionAnularAnexo() {
        $("#pnlBotonesConfirmacionAnularAnexo1").hide();
        $("#pnlBotonesConfirmacionAnularAnexo2").hide();
        //$('body').removeClass('modal-open');
        $("#frmConfirmarAnularEncomienda2").modal("hide");
        //$('.modal-backdrop').remove();
        return false;
    }


    function showModalRubros(obj) {
        idEncomienda = $(obj).attr("data-id_encomienda");
        $("#MostrarRubros" + idEncomienda).modal("show");
        return false;
    }


    function hidefrmConfirmarAnularEncomienda1(obj) {
        $("#frmConfirmarAnularEncomienda1").modal("hide");
        //$('body').removeClass('modal-open');
        //$('.modal-backdrop').remove();

        idEncomiendaAnular2 = idEncomiendaAnula1;

        $("#<%: hid_encomienda_anular2.ClientID %>").val(idEncomiendaAnular2);

         $("#frmConfirmarAnularEncomienda2").modal("show");

         return false;
     }

     function hidefrmConfirmarAnularEncomienda2() {
         $("#frmConfirmarAnularEncomienda2").modal("hide");
         //$('body').removeClass('modal-open');
         //$('.modal-backdrop').remove();
         return false;
     }

     function showfrmConfirmarAnularAnexo(obj) {
         //debugger;

         idEncomiendaAnula1 = $(obj).attr("data-id_encomienda");

         $("#<%: hid_encomienda_anular1.ClientID %>").val(idEncomiendaAnula1);
         $("#frmConfirmarAnularEncomienda1").modal("show");

     }

</script>
