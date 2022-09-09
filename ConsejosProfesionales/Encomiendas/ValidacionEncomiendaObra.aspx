<%@ Page Language="C#" Title="Encomienda de Director de Obra" AutoEventWireup="true" CodeBehind="ValidacionEncomiendaObra.aspx.cs" MasterPageFile="~/Site.Master" Inherits="ConsejosProfesionales.Encomiendas.ValidacionEncomiendaObra" %>

<asp:Content ContentPlaceHolderID="FeaturedContent" runat="server" ID="Featured">
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <%: Scripts.Render("~/bundles/autoNumeric") %>
    <%: Scripts.Render("~/bundles/select2") %>
    <%: Styles.Render("~/bundles/select2Css") %>

    <h2 class="text-center">Validaci&oacute;n de Encomienda de Director de Obra
    </h2>
    <hr />


    <p class="mtop10 text-center">
        Desde aquí podrá consultar los trámites que un usuario ha iniciado.
        <br />
        Ver el estado en que se encuentran y trabajar con cada uno.<br />
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
                <td style="font-size: 24px">Cargando...
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div id="page_content" style="display: none">

        <asp:UpdatePanel ID="updBuscar" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hid_formulario_cargado" runat="server" />

                <asp:Panel ID="pnlBuscar" runat="server" DefaultButton="btnBuscar" CssClass="form-inline box-panel" Style="border-top: dotted 1px #e1e1e1;">
                    <div style="margin-top: 5px; color: #377bb5">
                        <h4><i class="imoon imoon-search" style="margin-right: 10px"></i>Panel de Búsqueda</h4>
                        <hr />
                    </div>
                    <div class="form-horizontal" style="float: left; width: 30%;">

                        <label class="mleft20">Filtros de búsqueda:</label><br />
                        <div class="checkbox mleft20">
                            <asp:RadioButton ID="optBusquedaSeleccion" runat="server" GroupName="Estados" Text="Estados" Checked="true" Style="margin: 5px !important" />
                            <asp:RadioButton ID="optBusquedaTodos" runat="server" GroupName="Estados" Text="Todos" Style="margin: 5px !important" />

                        </div>


                        <div id="selestados" class="checkbox" style="margin-left: 50px">
                            <asp:CheckBoxList ID="chkEstados" runat="server" CssClass="table" CellSpacing="10" CellPadding="10" Style="margin: 5px !important">
                                <asp:ListItem Text="Pendiente de validación" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Validada por el Consejo" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Rechazada por el Consejo" Value="5"></asp:ListItem>
                                <asp:ListItem Text="Anulada" Value="20"></asp:ListItem>
                            </asp:CheckBoxList>
                        </div>

                    </div>
                    <div class="row" style="float: left; width: 70%">

                        <br />
                        <br />
                        <br />
                        <div class="row mleft10 mtop10">
                            <asp:Label ID="Label3" runat="server" Text="Nº de Matrícula del Profesional:" CssClass="control-label col-sm-5 mtop5 text-right"></asp:Label>

                            <div class="col-sm-4 form-group-sm mtop5">
                                <asp:TextBox ID="txtNroMatricula" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row mleft10 mtop10">
                            <asp:Label ID="Label1" runat="server" Text="Apellido y nombre:" CssClass="control-label col-sm-5 mtop5 text-right"></asp:Label>
                            <div class="col-sm-4 form-group-sm mtop5">
                                <asp:TextBox ID="txtApeNom" runat="server" Width="100%" CssClass="form-control" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row mleft10 mtop10">
                            <asp:Label ID="Label5" runat="server" Text="Nº de Trámite en Consejo:" CssClass="control-label col-sm-5 mtop5 text-right"></asp:Label>
                            <div class="col-sm-4 form-group-sm mtop5">
                                <asp:TextBox ID="txtNroEncomienda" runat="server" Width="100%" CssClass="form-control" MaxLength="12"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row mleft10 mtop10">
                            <asp:Label ID="Label2" runat="server" Text="C.U.I.T. del Comitente:" CssClass="control-label col-sm-5 mtop5 text-right"></asp:Label>
                            <div class="col-sm-4 form-group-sm mtop5">
                                <asp:TextBox ID="txtCuit" runat="server" Width="100%" CssClass="form-control" MaxLength="13"></asp:TextBox>
                                <div>
                                    <asp:RegularExpressionValidator ID="CuitRegEx" runat="server" ControlToValidate="txtCuit" Style="padding-left: 5px" Display="Dynamic"
                                        ErrorMessage="El Nº de CUIT no tiene un formato válido. Ej: 20-25006281-9" ValidationExpression="\d{2}-\d{8}-\d{1}"
                                        ValidationGroup="Buscar"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row " style="float: left; width: 100%; margin-top: 30px">

                        <label class="control-label col-sm-4"></label>
                        <div class="col-sm-6">

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
                                        <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </div>
                    </div>
                    <br />
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
                        <asp:Label ID="lblCantidadRegistros" runat="server" CssClass="badge" Text="0"></asp:Label>
                    </div>
                </div>

                <asp:GridView ID="grdBandeja"
                    runat="server"
                    AutoGenerateColumns="false"
                    AllowPaging="true"
                    PageSize="30"
                    Style="border: none;" CssClass="table table-bordered mtop5"
                    ItemType="DataTransferObject.EncomiendaExternaDTO"
                    OnDataBound="grdBandeja_DataBound"
                    DataKeyNames="IdEncomienda,IdEstado, IdTipoTramite"
                    SelectMethod="GetTramites"
                    GridLines="None" Width="100%">
                    <HeaderStyle CssClass="grid-header" />
                    <RowStyle CssClass="grid-row" />
                    <AlternatingRowStyle BackColor="#efefef" />
                    <Columns>
                        <asp:TemplateField HeaderText="Nº Trámite en Consejo" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkNroEncomienda" runat="server" Text='<%# Item.nroTramite %>' OnClick="lnkNroEncomienda_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FechaEncomienda" DataFormatString="{0:g}" HeaderText="Fecha de pedido de Validaci&oacute;n" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px" />
                        <asp:TemplateField HeaderText="Estado" HeaderStyle-CssClass="text-left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="120px">
                            <ItemTemplate>
                                <asp:Label ID="lnkestado" runat="server" Text='<%# Item.Estado.NomEstado == "Aprobada por el consejo" ? "Validada por el Consejo" : Item.Estado.NomEstado %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Profesional" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px">
                            <ItemTemplate>
                                <asp:Label ID="lblProfesional" runat="server" Text='<%# Item.ProfesionalDTO.Apellido + ", " + Item.ProfesionalDTO.Nombre  %>'></asp:Label>
                                <asp:Label ID="lblProfMatricula" runat="server" Text='<%# Item.ProfesionalDTO.Matricula != null || Item.ProfesionalDTO.Matricula.Length > 0 ? ("<br />Matricula Nº: " + Item.ProfesionalDTO.Matricula) :"" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Direccion.direccion" HeaderText="Dirección" HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="150px" />
                        <asp:BoundField DataField="TipoTramiteDescripcion" HeaderText="Tipo de tramite" HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="80px" />


                    </Columns>
                    <PagerTemplate>
                        <asp:Panel ID="pnlpager" runat="server" Style="padding: 10px; text-align: center; border-top: solid 1px #e1e1e1">

                            <asp:LinkButton ID="cmdAnterior" runat="server" Text="<<" OnClick="cmdAnterior_Click"
                                CssClass="btn btn-default" />

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
                                CssClass="btn btn-default" />
                        </asp:Panel>
                    </PagerTemplate>

                    <EmptyDataTemplate>

                        <div>

                            <img src="../Content/img/app/NoRecords.png" />
                            <span class="mleft20">No se encontraron trámites con los filtros ingresados.</span>

                        </div>

                    </EmptyDataTemplate>

                </asp:GridView>
                <div style="width: 100%; text-align: center">
                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="updgrdBandeja"
                        runat="server" DisplayAfter="0">
                        <ProgressTemplate>
                            <img src="../Common/Images/Controles/Loading24x24.gif" alt="" />
                            Cargando...
                        </ProgressTemplate>
                    </asp:UpdateProgress>
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
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <script type="text/javascript">

        $(document).ready(function () {

            $("#page_content").hide();
            $("#Loading").show();
            $("#<%: txtNroEncomienda.ClientID%>").autoNumeric("init", { aSep: "", mDec: 0, vMax: "999999999999" });
            $("#<%: btnCargarDatos.ClientID %>").click();
        });

        function init_JS_updBuscarTramites() {

            $("#<%: txtNroEncomienda.ClientID%>").autoNumeric("init", { aSep: "", mDec: 0, vMax: "999999999999" });

            $("#<%: optBusquedaSeleccion.ClientID %>").on("click", function () {
                $("#selestados").show();
            });

            $("#<%: optBusquedaTodos.ClientID %>").on("click", function () {
                $("#selestados").hide();
            });

            if ($("#<%: optBusquedaSeleccion.ClientID %>").is(':checked')) {
                $("#selestados").show();
            }

            if ($("#<%: optBusquedaTodos.ClientID %>").is(':checked')) {
                $("#selestados").hide();
            }

            $('[rel=popover]').popover({
                html: 'true',
                placement: 'right'
            })


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
