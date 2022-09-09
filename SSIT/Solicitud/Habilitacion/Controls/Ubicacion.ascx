<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Ubicacion.ascx.cs" Inherits="SSIT.Solicitud.Habilitacion.Controls.Ubicacion" %>

<%--Panel de Ubicacion--%>
<%--<asp:HiddenField ID="hid_id_solicitud" runat="server" />--%>


<asp:Panel ID="pnlUbicacion" runat="server">

    <asp:GridView ID="gridubicacion_db"
        runat="server"
        AutoGenerateColumns="false"
        DataKeyNames="IdSolicitudUbicacion"
        Style="border: none;"
        GridLines="None"
        ShowHeader="false"
        AllowPaging="false"
        Width="100%"
        OnRowDataBound="gridubicacion_db_OnRowDataBound">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>

                    <div>
                        <div style="margin-left: 20px; margin-top: -34px">
                            <hr />
                        </div>
                    </div>
                    <asp:Panel ID="pnlbtnEliminar" runat="server">
                        <asp:LinkButton ID="btnEliminar" CssClass="pull-right btn btn-default" runat="server" title="Quitar Ubicación" OnClick="btnEliminar_Click" CommandArgument='<%# Eval("IdSolicitudUbicacion") %>'>
                                    <i class="imoon imoon-close"  style="color:#377bb5"></i>                                
                        </asp:LinkButton>
                    </asp:Panel>

                    <asp:Panel ID="pnlbtnEditar" runat="server">
                        <asp:LinkButton ID="btnEditar" CssClass="pull-right btn btn-default" runat="server" title="Editar Ubicación" OnClick="btnEditar_Click" CommandArgument='<%# Eval("IdSolicitudUbicacion") %>'>
                                  <i class="imoon imoon-pencil"  style="color:#377bb5"></i>                                
                        </asp:LinkButton>
                    </asp:Panel>

                    <div>
                        <div class="form-horizontal" style="margin-left: 10px; margin-top: -10px">
                            <div class="form-group">
                                <div class="form-inline">
                                    <div class="col-sm-4">
                                        <div id='<%# Eval("IdUbicacion") %>' class="carousel slide" style="height: auto; width: auto; overflow: hidden;">

                                            <div class="carousel-inner" style="width: auto; height: auto;">
                                                <div class="item active">
                                                    <asp:Image ID="imgFotoParcela_db" runat="server" onError="noExisteFotoParcela(this);" Style="border: solid 2px #939393; border-radius: 12px" />
                                                </div>
                                                <div class="item ">
                                                    <asp:Image ID="imgMapa1" runat="server" onError="noExisteFotoParcela(this);" Style="border: solid 2px #939393; border-radius: 12px" />
                                                    <div class="col-sm-12 text-center">
                                                        <strong>Entre calles</strong>
                                                    </div>
                                                </div>
                                                <div class="item">
                                                    <asp:Image ID="imgMapa2" runat="server" onError="noExisteFotoParcela(this);" Style="border: solid 2px #939393; border-radius: 12px" />
                                                    <div class="col-sm-12 text-center">
                                                        <strong>Croquis</strong>
                                                    </div>
                                                </div>
                                            </div>
                                            <!-- Carousel nav -->
                                            <a class="left carousel-control" style="margin-left: -10px" href="<%# '#'+ Eval("IdUbicacion").ToString() %>" data-slide="prev">
                                                <span class="glyphicon glyphicon-chevron-left"></span>
                                            </a>
                                            <a class="right carousel-control" style="margin-right: -5px" href="<%# '#'+ Eval("IdUbicacion").ToString() %>" data-slide="next">
                                                <span class="glyphicon glyphicon-chevron-right"></span>
                                            </a>

                                            <ol class="carousel-indicators text-left" style="margin-left: -15px; display: inline-block">
                                                <li data-target="<%# '#'+ Eval("IdUbicacion").ToString() %>" data-slide-to="0" class="active"></li>
                                                <li data-target="<%# '#'+ Eval("IdUbicacion").ToString() %>" data-slide-to="1"></li>
                                                <li data-target="<%# '#'+ Eval("IdUbicacion").ToString() %>" data-slide-to="2"></li>
                                            </ol>
                                        </div>

                                    </div>
                                    <div class="col-sm-7">
                                        <asp:Panel ID="pnlSMPview" runat="server" Style="padding-top: 3px">
                                            <ul>
                                                <li>Sección:
                                            <asp:Label ID="grd_seccion_db" runat="server" Font-Bold="true"></asp:Label>
                                                    Manzana:
                                            <asp:Label ID="grd_manzana_db" runat="server" Font-Bold="true"></asp:Label>
                                                    Parcela:
                                            <asp:Label ID="grd_parcela_db" runat="server" Font-Bold="true"></asp:Label></li>
                                            </ul>
                                            <ul>
                                                <li>Partida Matriz Nº:
                                            <asp:Label ID="grd_NroPartidaMatriz_db" runat="server" Font-Bold="true"></asp:Label></li>
                                            </ul>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlTipoUbicacionview" runat="server" Style="padding-top: 3px" Visible="false">
                                            <ul>
                                                <li>Tipo de Ubicaci&oacute;n:
                                            <asp:Label ID="lblTipoUbicacionview" runat="server" Font-Bold="true"></asp:Label></li>
                                            </ul>
                                            <ul>
                                                <li>Subtipo de Ubicaci&oacute;n:
                                            <asp:Label ID="lblSubTipoUbicacionview" runat="server" Font-Bold="true"></asp:Label></li>
                                            </ul>
                                            <div>
                                                <strong>Local:</strong>
                                                <asp:Label ID="lblLocalview" runat="server"></asp:Label>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="pnlPartidasHorizontalesview" runat="server">
                                            <ul>
                                                <li>Partida/s Horizontal/es:
                                     
                                                <asp:DataList ID="dtlPartidaHorizontales_db" runat="server" Font-Bold="true" RepeatDirection="Horizontal"
                                                    RepeatColumns="1" CellSpacing="10">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPartidahorizontal_db" runat="server" Text='<% #Bind("DescripcionCompleta")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                                    <asp:Label ID="lblEmptyDataPartidasHorizontales_db" runat="server" Text="No posee" Visible="false" CssClass="col-sm-2"></asp:Label>
                                                </li>
                                            </ul>
                                        </asp:Panel>

                                        <ul>
                                            <li>Area de Mixtura / Distrito de Zonificación:
                                                <asp:Label ID="lbl_zonificacion_db" runat="server" Style="width: 100%" Font-Bold="true"></asp:Label>
                                            </li>
                                        </ul>
                                        <asp:Panel ID="pnlPuertasview" runat="server">

                                            <ul>
                                                <li>Puertas:
                                        
                                                <asp:DataList ID="dtlPuertas_db" runat="server" RepeatDirection="Horizontal" RepeatColumns="1"
                                                    CellSpacing="10" Font-Bold="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPuertas_db" runat="server" Text='<% #Bind("NombreCalle")%>'></asp:Label>
                                                        <asp:Label ID="lnkNroPuerta_db" runat="server" Text='<% #Bind("NroPuerta")%>' CssClass="pleft5"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:DataList>

                                                </li>

                                            </ul>
                                        </asp:Panel>
                                        <asp:UpdatePanel ID="UpnDeptoLocalview" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Panel ID="pnlDeptoLocalview" runat="server">
                                                    <div class="form-group">
                                                        <div class="form-inline" style="margin-left: 15px">
                                                            <ul>
                                                                <li>
                                                                    <asp:Label ID="lblOtros" runat="server" Text="Otros:"></asp:Label>
                                                                    <asp:Label ID="lblTextOtros" CssClass="mleft10 mright20" Font-Bold="true" runat="server"></asp:Label>

                                                                    <asp:Label ID="lblLocal" runat="server" Text="Local:"></asp:Label>
                                                                    <asp:Label ID="lblTextLocal" CssClass="mleft10 mright20" Font-Bold="true" runat="server"></asp:Label>

                                                                    <asp:Label ID="lblDepto" runat="server" Text="Depto:"></asp:Label>
                                                                    <asp:Label ID="lblTextDepto" CssClass="mleft10 mright20" Font-Bold="true" runat="server"></asp:Label>

                                                                    <asp:Label ID="lblTorre" runat="server" Text="Torre:"></asp:Label>
                                                                    <asp:Label ID="lblTextTorre" CssClass="mleft10 mright20" Font-Bold="true" runat="server"></asp:Label></li>
                                                            </ul>

                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <div class="mtop10 table table-bordered mtop5">
                <div class="mtop10 mbottom10 mleft10">
                    <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' />
                    No se encontraron registros.
                </div>
            </div>
        </EmptyDataTemplate>
    </asp:GridView>
    <%--Plantas a habilitar--%>
    <asp:Panel ID="pnlPlantasHabilitar" runat="server" Style="padding-left: 10px">
        <strong>Plantas a habilitar:</strong>
        <asp:Label ID="lblPlantasHabilitar" runat="server"></asp:Label>
        <br />
    </asp:Panel>

    <script type="text/javascript">
        function noExisteFotoParcela(objimg) {
            $(objimg).attr("src", '<%: ResolveUrl("~/Content/img/app/ImageNotFound.png") %>');
            fotoCargada();
            return true;
        }

        function fotoCargada(objOcultar, objMostrar) {
            $("#" + objOcultar).css("display", "none");
            $(objMostrar).css("display", "inherit");

            return true;
        }

    </script>
</asp:Panel>

<asp:CheckBox ID="chkServidumbre" runat="server" Text=" Posee Servidumbre de paso" Style="display: none" ToolTip="Al haberse escogido más de una ubicación, la opción debe estar tildada." />
<asp:Label ID="lblServidumbre" runat="server" Text="(En caso de seleccionar más de una ubicación, deberá declarar la existencia de Servidumbre de Paso entre las dos parcelas. Esta selección tiene carácter de Declaración Jurada)" Style="display: none"></asp:Label>
