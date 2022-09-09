<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Rubros2.ascx.cs" Inherits="SSIT.Solicitud.Habilitacion.Controls.Rubros2" %>
<asp:HiddenField ID="hid_id_solicitud" runat="server" />
<%--Panel Rubros--%>
<asp:Panel ID="pnlRubros" Class="IbuttonControl" runat="server">
    <div>
        <asp:Panel ID="AgregarRubro" runat="server">
            <div class="row  mleft10 mtop1">
                <button id="btnAgregarRubros" class="btn btn-default pbottom5" onclick="return showfrmAgregarRubros();">
                    <i class="imoon imoon-plus"></i>
                    <span class="text">Agregar Rubro</span>
                </button>
            </div>
        </asp:Panel>
        <br />
        <asp:UpdatePanel ID="updRubros" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="hid_id_rubro_eliminar" runat="server" />
                <asp:HiddenField ID="hid_enabled" runat="server" />
                <asp:GridView ID="grdRubrosIngresados" runat="server" AutoGenerateColumns="false"
                    AllowPaging="false" Style="border: none;" CssClass="col-sm-3 table table-bordered mtop5"
                    OnRowDataBound="grdRubrosIngresados_RowDataBound"
                    GridLines="None" Width="80%">
                    <HeaderStyle CssClass="grid-header" />
                    <RowStyle CssClass="grid-row" />
                    <AlternatingRowStyle BackColor="#efefef" />
                    <Columns>
                        <asp:BoundField DataField="Codigo" HeaderText="Código" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" HeaderStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="Superficie" HeaderText="Superficie" Visible="true" />
                        <asp:TemplateField ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEliminarRubro" runat="server" data-id-rubro-eliminar='<%# Eval("idRelRubSol") %>' CssClass="link-local"
                                    OnClientClick="return showConfirmarEliminarRubro(this);">
                                        <i class="imoon imoon-close"></i>
                                        <span class="text">Eliminar</span>
                                </asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="titulo-4">
                            No se ingresaron datos
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
</asp:Panel>

<%--Modal Confirmar Eliminar Rubro--%>
<div id="frmConfirmarEliminarRubro" class="modal fade" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title" style="margin-top: -8px">Eliminar Rubro</h4>
            </div>
            <div class="modal-body">
                <table style="border-collapse: separate; border-spacing: 5px">
                    <tr>
                        <td style="text-align: center; vertical-align: text-top">
                            <i class="imoon imoon-remove-circle fs64 color-blue"></i>
                        </td>
                        <td style="vertical-align: middle">
                            <label class="mleft10">¿ Est&aacute; seguro de eliminar el Rubro ?</label>
                        </td>
                    </tr>
                </table>

            </div>
            <div class="modal-footer mleft20 mright20">

                <asp:UpdatePanel ID="updConfirmarEliminarRubro" runat="server">
                    <ContentTemplate>

                        <div class="form-inline">
                            <div class="form-group">
                                <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="updConfirmarEliminarRubro">
                                    <ProgressTemplate>
                                        <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                            <div id="pnlBotonesConfirmacionEliminarRubro" class="form-group">
                                <asp:Button ID="btnEliminarRubro"
                                    runat="server"
                                    CssClass="btn btn-primary"
                                    Text="Sí"
                                    OnClientClick="hidefrmConfirmarEliminarRubro();"
                                    OnClick="btnEliminarRubro_Click" />
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

<%--Modal form Agregar Rubros--%>
<div id="frmAgregarRubros" class="modal fade" role="dialog">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title" style="margin-top: -8px">Agregar Rubros</h4>
            </div>
            <div class="modal-body pbottom20">
                <asp:UpdatePanel ID="updBuscarRubros" runat="server">
                    <ContentTemplate>

                        <asp:Panel ID="pnlBuscarRubros" runat="server" CssClass="form-horizontal" DefaultButton="btnBuscar">
                            <div class="row">
                                <div class="row">
                                    <div class="pleft20">
                                        <h3 class="col-sm-12">B&uacute;squeda de Rubros</h3>
                                    </div>
                                </div>
                                <div class="row">
                                    <label class="control-label col-sm-3">Superficie del rubro:</label>
                                    <div class="col-sm-1">
                                        <asp:TextBox ID="txtSuperficie" runat="server" Width="100px" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-8 pleft40">
                                        <div id="Req_Superficie" class="alert alert-danger mbottom0" style="display: none">
                                            La superficie a habilitar debe ser un número entre 1 y la superficie total del local.
                                        </div>
                                    </div>

                                </div>
                                <br />
                                <div class="row">
                                    <label class="control-label col-sm-3" style="margin-top: -15px">Ingrese el código o parte de la descipción del rubro a buscar:</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-offset-3 col-sm-9 ptop5">
                                        <div id="Req_txtBuscar" class="alert alert-danger mbottom0" style="display: none">
                                            Debe ingresar al menos 3 caracteres para iniciar la b&uacute;squeda.
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <hr class="mbottom0 mtop0" />


                            <asp:UpdatePanel ID="updBotonesBuscarRubros" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <asp:Panel ID="pnlBotonesBuscarRubros" runat="server" CssClass="form-inline text-right">
                                        <div class="form-group">
                                            <asp:UpdateProgress ID="UpdateProgress3" AssociatedUpdatePanelID="updBotonesBuscarRubros"
                                                runat="server" DisplayAfter="200">
                                                <ProgressTemplate>
                                                    <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                                    Buscando...
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>

                                        <asp:Panel ID="BotonesBuscarRubros" runat="server" CssClas="form-group" DefaultButton="btnBuscar">
                                            <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-primary" OnClick="btnBuscar_Click" OnClientClick="return validarBuscar();">
                                                    <i class="imoon imoon-search"></i>
                                                    <span class="text">Buscar</span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-default" data-dismiss="modal">
                                                    <i class="imoon imoon-close"></i>
                                                    <span class="text">Cerrar</span>
                                            </asp:LinkButton>
                                        </asp:Panel>
                                    </asp:Panel>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </asp:Panel>

                        <asp:Panel ID="pnlResultadoBusquedaRubros" runat="server" CssClass="form-horizontal" Style="display: none">

                            <div style="max-height: 500px; overflow-y: auto">

                                <asp:GridView ID="grdRubros" runat="server"
                                    AutoGenerateColumns="false"
                                    DataKeyNames="IdRubro,Codigo,Nombre,Superficie"
                                    AllowPaging="true"
                                    PageSize="10"
                                    Style="border: none;"
                                    CssClass="table table-bordered mtop5"
                                    ItemType="DataTransferObject.RubrosCNDTO"
                                    GridLines="None"
                                    Width="100%"
                                    OnDataBound="grdRubros_DataBound"
                                    OnRowDataBound="grdRubros_RowDataBound">
                                    <HeaderStyle CssClass="grid-header" />
                                    <RowStyle CssClass="grid-row" />
                                    <AlternatingRowStyle BackColor="#efefef" />
                                    <Columns>

                                        <asp:BoundField DataField="Codigo" HeaderText="Código" />
                                        <asp:BoundField DataField="Nombre" HeaderText="Descripción" />
                                        <asp:BoundField DataField="TipoActividadNombre" HeaderText="Actividad" />
                                        <asp:BoundField DataField="Superficie" HeaderText="Superficie" Visible="true" />
                                        <asp:TemplateField HeaderText="Ingresar">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkRubroElegido" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerTemplate>

                                        <asp:UpdatePanel ID="updPnlpager" runat="server">
                                            <ContentTemplate>

                                                <asp:Panel ID="pnlpager" runat="server" Style="padding: 10px; text-align: center; border-top: solid 1px #e1e1e1">

                                                    <asp:Button ID="cmdAnterior" runat="server" Text="<< Anterior" OnClick="cmdAnterior_Click"
                                                        CssClass="btnPagerGrid" Width="100px" />
                                                    <asp:Button ID="cmdPage1" runat="server" Text="1" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                                    <asp:Button ID="cmdPage2" runat="server" Text="2" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                                    <asp:Button ID="cmdPage3" runat="server" Text="3" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                                    <asp:Button ID="cmdPage4" runat="server" Text="4" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                                    <asp:Button ID="cmdPage5" runat="server" Text="5" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                                    <asp:Button ID="cmdPage6" runat="server" Text="6" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                                    <asp:Button ID="cmdPage7" runat="server" Text="7" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                                    <asp:Button ID="cmdPage8" runat="server" Text="8" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                                    <asp:Button ID="cmdPage9" runat="server" Text="9" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                                    <asp:Button ID="cmdPage10" runat="server" Text="10" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                                    <asp:Button ID="cmdPage11" runat="server" Text="11" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                                    <asp:Button ID="cmdPage12" runat="server" Text="12" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                                    <asp:Button ID="cmdPage13" runat="server" Text="13" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                                    <asp:Button ID="cmdPage14" runat="server" Text="14" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                                    <asp:Button ID="cmdPage15" runat="server" Text="15" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                                    <asp:Button ID="cmdPage16" runat="server" Text="16" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                                    <asp:Button ID="cmdPage17" runat="server" Text="17" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                                    <asp:Button ID="cmdPage18" runat="server" Text="18" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                                    <asp:Button ID="cmdPage19" runat="server" Text="19" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                                    <asp:Button ID="cmdSiguiente" runat="server" Text="Siguiente >>" OnClick="cmdSiguiente_Click"
                                                        CssClass="btnPagerGrid" Width="100px" />


                                                    <div style="display: inline-table">

                                                        <asp:UpdateProgress ID="UpdateProgress7" AssociatedUpdatePanelID="updPnlpager" runat="server"
                                                            DisplayAfter="0">
                                                            <ProgressTemplate>
                                                                <img src="../Common/Images/Controles/Loading24x24.gif" alt="" />
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </div>

                                                </asp:Panel>

                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </PagerTemplate>
                                    <EmptyDataTemplate>

                                        <div class="mtop10">

                                            <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                                            <span class="mleft10">No se encontraron registros.</span>

                                        </div>

                                    </EmptyDataTemplate>
                                </asp:GridView>

                            </div>

                            <asp:UpdatePanel ID="updBotonesAgregarRubros" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <asp:Panel ID="pnlGrupoAgregarRubros" runat="server" CssClass="row ptop10 pleft10 pright10" Style="display: none">


                                        <div class="col-sm-7 pleft20">

                                            <asp:UpdatePanel ID="updValidadorAgregarRubros" runat="server">
                                                <ContentTemplate>
                                                    <asp:Panel ID="ValidadorAgregarRubros" runat="server" CssClass="alert alert-danger mbottom0" Style="display: none">
                                                        <asp:Label ID="lblValidadorAgregarRubros" runat="server"></asp:Label>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>

                                        </div>



                                        <asp:Panel ID="pnlBotonesAgregarRubros" runat="server" CssClass="col-sm-3 text-right">

                                            <asp:UpdateProgress ID="UpdateProgress4" AssociatedUpdatePanelID="updBotonesAgregarRubros"
                                                runat="server" DisplayAfter="200">
                                                <ProgressTemplate>
                                                    <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                                    Procesando...
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>


                                            <div id="BotonesAgregarRubros" class="form-group">
                                                <asp:LinkButton ID="btnIngresarRubros" runat="server" CssClass="btn btn-primary" OnClick="btnIngresarRubros_Click" OnClientClick="ocultarBotonesAgregarRubros();">
                                                        <i class="imoon imoon-plus"></i>
                                                        <span class="text">Agregar</span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-default" data-dismiss="modal">
                                                        <i class="imoon imoon-close"></i>
                                                        <span class="text">Cerrar</span>
                                                </asp:LinkButton>
                                            </div>

                                        </asp:Panel>
                                    </asp:Panel>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </asp:Panel>

                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>

        </div>
    </div>
</div>
<!-- /.modal -->

<script>

    function ocultarBotonesConfirmacionEliminarRubro() {
        $("#pnlBotonesConfirmacionEliminarRubro").hide();
        return false;
    }

    function showfrmAgregarRubros() {



        $("#<%: txtBuscar.ClientID %>").val("");
        $("#<%: pnlBuscarRubros.ClientID %>").show();
        $("#<%: pnlResultadoBusquedaRubros.ClientID %>").hide();
        $("#<%: pnlBotonesAgregarRubros.ClientID %>").hide();
        $("#<%: pnlBotonesBuscarRubros.ClientID %>").show();

        $("#<%: BotonesBuscarRubros.ClientID %>").show();


        $("#frmAgregarRubros").on("shown.bs.modal", function (e) {
            $("#<%: txtBuscar.ClientID %>").focus();
        });

        $("#frmAgregarRubros").modal({
            "show": true,
            "backdrop": "static"

        });

        return false;
        }

        function validarBuscar() {

            var ret = true;
            $("#Req_Superficie").hide();
            $("#Req_txtBuscar").hide();



            var value1 = $("#<%: txtSuperficie.ClientID %>").val();
            var value2 = $("#MainContent_txtSuperficie").val();






            var ContarComas = (value1.split(",").length - 1);
            var ContarPuntos = (value1.split(".").length - 1);

            value1 = value1.replace(".", ",");

            if (ContarComas + ContarPuntos > 1) {
                ret = false;
            }


            var ContarComas2 = (value2.split(",").length - 1);
            var ContarPuntos2 = (value2.split(".").length - 1);

            value2 = value2.replace(".", ",");

            if (ContarComas2 + ContarPuntos2 > 1) {
                ret = false;
            }

            var superficie = stringToFloat(value1);
            var superficieMaxima = stringToFloat(value2);

            if (superficie <= 0 || superficie > superficieMaxima) {
                $("#Req_Superficie").css("display", "inline-block");
                ret = false;
            }

            if ($("#<%: txtBuscar.ClientID %>").val().length < 3) {
                $("#Req_txtBuscar").css("display", "inline-block");
                ret = false;
            }

            if (ret) {

                $("#<%: pnlGrupoAgregarRubros.ClientID %>").css("display", "block");
                    }

                    return ret;
                }

                function ocultarBotonesAgregarRubros() {

                    $("#BotonesAgregarRubros").hide();
                    return false;
                }
                function hidefrmAgregarRubros() {

                    $("#frmAgregarRubros").modal("hide");
                    return false;
                }

                function showConfirmarEliminarRubro(obj) {
                    var id_rubro_eliminar = $(obj).attr("data-id-rubro-eliminar");
                    $("#<%: hid_id_rubro_eliminar.ClientID %>").val(id_rubro_eliminar);
            $("#frmConfirmarEliminarRubro").modal("show");
            return false;
        }

        function hidefrmConfirmarEliminarRubro() {

            $("#frmConfirmarEliminarRubro").modal("hide");
            return false;
        }

</script>
