<%@ Page Title="Consulta de Profesionales" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConsultaProfesionales2.aspx.cs" Inherits="SSIT.Consulta_Profesionales2.ConsultaProfesionales2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <%: Scripts.Render("~/bundles/autoNumeric") %>
    <%: Scripts.Render("~/bundles/select2") %>
    <%: Styles.Render("~/bundles/select2Css") %>

    <h2>Consulta de Profesionales
    </h2>
    <hr />
    <br />
    <%--ajax cargando ...--%>
    <div id="Loading" style="text-align: center; padding-bottom: 20px; margin-top: 120px">
        <table border="0" style="border-collapse: separate; border-spacing: 5px; margin: auto">
            <tr>
                <td>
                    <img src="<%: ResolveUrl("~/Content/img/app/Loading128x128.gif") %>" alt="" />
                </td>
            </tr>
            <tr>
                <td style="font-size: 24px">Cargando profesionales
                </td>
            </tr>
        </table>
    </div>



    <div id="page_content" style="display: none">

        <asp:UpdatePanel ID="updBuscar" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hid_id_tipotramite" runat="server" />
                <asp:HiddenField ID="hid_id_estado" runat="server" />
                <asp:HiddenField ID="hid_id_solicitud" runat="server" />
                <asp:HiddenField ID="hid_formulario_cargado" runat="server" />



                <asp:Panel ID="pnlBuscar" runat="server" DefaultButton="btnBuscar" CssClass="form-inline">

                    <asp:UpdatePanel ID="updpnlCircuito" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hid_profesional" runat="server" />
                            <div class="row mleft10 mtop10">
                                <label class="control-label col-sm-2 mtop5 text-right">Tipo de Actividad/Tr&aacute;mite:</label>
                                <div class="col-sm-8 mtop5">
                                    <asp:DropDownList ID="ddlCircuitos" AutoPostBack="true" runat="server" Width="60%"></asp:DropDownList>

                                </div>
                            </div>

                            <div class="row mleft10 mtop10">
                                <label class="control-label col-sm-2 mtop5 text-right">Profesional</label>
                                <div class="col-sm-4 mtop5">
                                    <asp:TextBox ID="txtprofesional" class="form-control form-control-sm" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="row mleft10 mtop10">

                        <label class="control-label col-sm-2 mtop5 text-right"></label>
                        <div class="col-sm-4 mtop5">

                            <asp:LinkButton ID="btnLimpiar" runat="server" CssClass="btn btn-default" OnClick="btnLimpiar_Click">
                            <i class="imoon imoon-eraser"></i>
                            <span class="text">Limpiar</span>
                            </asp:LinkButton>


                            <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-primary" OnClick="btnBuscar_Click">
                            <i class="imoon imoon-search"></i>
                            <span class="text">Buscar</span>
                            </asp:LinkButton>

                            <div class="form-group">
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="200" AssociatedUpdatePanelID="updBuscar">
                                    <ProgressTemplate>
                                        <img src="../Content/img/app/Loading24x24.gif" style="margin-left: 10px" alt="loading" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </div>
                    </div>
                </asp:Panel>

            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="updgrdBandeja" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnCargarDatos" runat="server" OnClick="btnCargarDatos_Click" Style="display: none" />

                <div class="mtop30 row pleft10 pright10">
                    <div class="col-sm-6">
                        <strong>Resultado de la b&uacute;squeda</strong>
                    </div>
                    <div class="col-sm-6 text-right">
                        <strong>Cantidad de registros:</strong>
                        <asp:Label ID="lblCantidadRegistros" runat="server" CssClass="badge">0</asp:Label>
                    </div>
                </div>

                <asp:GridView ID="grdBandeja"
                    runat="server"
                    AutoGenerateColumns="false"
                    AllowPaging="true"
                    PageSize="30"
                    ItemType="DataTransferObject.SSIT_Listado_ProfesionalesDTO"
                    SelectMethod="GetProfesionales"
                    Style="border: none;" CssClass="table table-bordered mtop5"
                    OnDataBound="grdBandeja_DataBound"
                    OnRowDataBound="grdBandeja_RowDataBound"
                    AllowSorting="true"
                    GridLines="None" Width="100%">
                    <SortedAscendingHeaderStyle CssClass="GridAscendingHeaderStyle" />
                    <SortedDescendingHeaderStyle CssClass="GridDescendingHeaderStyle" />
                    <Columns>
                        <asp:TemplateField HeaderText="Profesional" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <asp:Label ID="grdlblApeNom" runat="server" Text='<%# Item.Apellido + " " + Item.nombre %>' /><br />

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Consejo Profesional" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <asp:Label ID="grdlblConsejo" runat="server" Text='<%# Item.Consejo %>' Font-Size="12px" /><br />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total de Solicitudes" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" ItemStyle-Width="10%" SortExpression="total">
                            <ItemTemplate>
                                <asp:Label ID="grdlblTotalSolicitudes" runat="server" Text='<%#  Item.total %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Aprobadas" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" ItemStyle-Width="10%" SortExpression="Aprobadas">
                            <ItemTemplate>
                                <asp:Label ID="grdlblpAprobadas" runat="server" Text='<%# Item.Aprobadas %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="% Aprobadas" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" ItemStyle-Width="10%" SortExpression="porcentaje_aprob">
                            <ItemTemplate>
                                <asp:Label ID="grdlblporcentajeAprobadas" runat="server" Text='<%# Item.porcentaje_aprob %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Rechazadas" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" ItemStyle-Width="10%" SortExpression="Rechazadas">
                            <ItemTemplate>
                                <asp:Label ID="grdlblrechazadas" runat="server" Text='<%# Item.Rechazadas %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="% Rechazadas" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" ItemStyle-Width="10%" SortExpression="porcentaje_recha">
                            <ItemTemplate>
                                <asp:Label ID="grdlblporcentajerechazadas" runat="server" Text='<%# Item.porcentaje_recha %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Vencidas" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" ItemStyle-Width="10%" SortExpression="Vencidas">
                            <ItemTemplate>
                                <asp:Label ID="grdlblvencidas" runat="server" Text='<%# Item.Vencidas %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="% Vencidas" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" ItemStyle-Width="10%" SortExpression="porcentaje_venci">
                            <ItemTemplate>
                                <asp:Label ID="grdlblporcentajeVencidas" runat="server" Text='<%# Item.porcentaje_venci %>' />
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

    <script type="text/javascript">


        $(document).ready(function () {

            $("#page_content").hide();
            $("#Loading").show();
            //$("#: txtNroSolicitud.ClientID ").autoNumeric("init", { aSep: "", mDec: 0, vMax: "999999" });
            $("#<%: btnCargarDatos.ClientID %>").click();



        });

        function init_JS_updpnlCircuito() {

            //$("#: txtBusMatricula.ClientID ").autoNumeric("init", { aSep: "", mDec: 0, vMax: '9999999' });

            $("#<%: ddlCircuitos.ClientID %>").select2({
                placeholder: "Todos",
                allowClear: true,
            });


            $('[rel=popover]').popover({
                html: 'true',
                placement: 'right'
            })


        }

        function init_js_updgrdBandeja() {
            $(function () {
                $("#<%: txtprofesional.ClientID %>").keyup(function () {
                    var val = $(this).val().toUpperCase();
                    $('#<%: grdBandeja.ClientID %> > tbody > tr td:nth-child(1)').each(function (index, element) {
                        if ($(this).text().toUpperCase().indexOf(val) < 0)
                            $(this).parent().hide();
                        else
                            $(this).parent().show();
                    });
                });
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
    </script>
</asp:Content>
