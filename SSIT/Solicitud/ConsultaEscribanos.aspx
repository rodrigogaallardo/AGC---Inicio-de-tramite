<%@ Page Title="Consulta de Escribanos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConsultaEscribanos.aspx.cs" Inherits="SSIT.Solicitud.ConsultaEscribanos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <%: Scripts.Render("~/bundles/autoNumeric") %>
    <%: Scripts.Render("~/bundles/select2") %>
    <%: Styles.Render("~/bundles/select2Css") %>
    
    <h2 >
        Consulta de Escribanos
    </h2>
    <hr />
    
    <div class="form-horizontal form-group" style="margin-top:-10px">
        <div class="col-sm-10">
            <p class="mtop10">
               <b>Escribano Público</b> es el profesional, matriculado en Capital Federal, que certifica la validez de la documentación que hay que presentar y 
                acredita el título por el cual posee la propiedad (contrato de locación, titular del inmueble, cesión, otros).<br />
            </p>
        </div>
       <div class="col-sm-2 text-right mtop30">
            <asp:HyperLink ID="lnkImprimirLstEscr" runat="server" CssClass="btn btn-default" Target="_blank" >
                            <i class="imoon imoon-download color-blue"></i>
                            <span class="text">Descargar Todos</span>
            </asp:HyperLink>
        </div>
    </div>
    <hr />
    <%--ajax cargando ...--%>
    <div id="Loading" style="text-align: center; padding-bottom: 20px; margin-top: 120px">
        <table border="0" style="border-collapse: separate; border-spacing: 5px; margin: auto">
            <tr>
                <td>
                    <img src="<%: ResolveUrl("~/Content/img/app/Loading128x128.gif") %>" alt="" />
                </td>
            </tr>
            <tr>
                <td style="font-size: 24px">Cargando Escribanos
                </td>
            </tr>
        </table>
    </div>


    
    <div id="page_content" Style="display:none">

        <asp:UpdatePanel ID="updBuscar" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hid_id_tipotramite" runat="server" />
                <asp:HiddenField ID="hid_id_estado" runat="server" />
                <asp:HiddenField ID="hid_id_solicitud" runat="server" />
                <asp:HiddenField ID="hid_formulario_cargado" runat="server" />
                

        
                <asp:Panel ID="pnlBuscar" runat="server" DefaultButton="btnBuscar" CssClass="form-inline">
        
                    <div class="row mleft10 mtop10">
                        <label class="control-label col-sm-2 mtop5 text-right">Nº de matricula:</label>
                                                            <div class="col-sm-4 form-group-sm mtop5" >
                        <asp:TextBox ID="txtBusMatricula" runat="server" CssClass="form-control"  Width="100%" ></asp:TextBox>
                                                                </div>                      
                    </div>
                    
                    <div class="row mleft10 mtop10">
                        <label class="control-label col-sm-2 mtop5 text-right">Apellido y Nombre:</label>
                        <div class="col-sm-4 form-group-sm mtop5" >
                        <asp:TextBox ID="txtBusApeNom" runat="server" CssClass="form-control"   Width="100%" ></asp:TextBox>
                                                                </div>
                        </div>
        
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
                        <strong>Cantidad de registros:</strong> <asp:Label ID="lblCantidadRegistros" runat="server" CssClass="badge">0</asp:Label>
                    </div>
                </div>

                <asp:GridView ID="grdBandeja" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="30"
                    ItemType="DataTransferObject.EscribanoDTO" SelectMethod="GetEscribanosList"
                    Style="border: none;" CssClass="table table-bordered mtop5" OnDataBound="grdBandeja_DataBound"
                    OnRowDataBound="grdBandeja_RowDataBound"
                    GridLines="None" Width="100%">
                                <Columns>
                                     <asp:TemplateField  HeaderText="Profesional" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" ItemStyle-Width="30%" >
                                        <ItemTemplate>
                                            <asp:Label ID="grdlblApeNom" runat="server" Text='<%# Item.ApyNom %>'  /><br />
                                     
                                            <asp:Label ID="grdlblMatricula" runat="server" Text='<%# Item.Matricula != null ? ("Matricula Nº: "+Item.Matricula) : "" %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Telefono" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" HeaderText="Telefono" ItemStyle-Width="30%" />
                                    <asp:BoundField DataField="Email" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" HeaderText="Email" ItemStyle-Width="30%" />
                                 
                                </Columns>
                    <PagerTemplate>
                        <asp:Panel ID="pnlpager" runat="server" Style="padding: 10px; text-align: center; border-top: solid 1px #e1e1e1">

                            <asp:LinkButton ID="cmdAnterior" runat="server" Text="<<" OnClick="cmdAnterior_Click"
                                CssClass="btn btn-sm btn-default" />

                            <asp:LinkButton ID="cmdPage1" runat="server" Text="1" OnClick="cmdPage" CssClass="btn btn-default" />
                            <asp:LinkButton ID="cmdPage2" runat="server" Text="2" OnClick="cmdPage" CssClass="btn"  />
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
                    <h4 class="modal-title" style="margin-top:-8px">Error</h4>
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

        function init_JS_updBuscarUbicacion() {

            $("#<%: txtBusMatricula.ClientID %>").autoNumeric("init", { aSep: "", mDec: 0, vMax: '9999999' });


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
