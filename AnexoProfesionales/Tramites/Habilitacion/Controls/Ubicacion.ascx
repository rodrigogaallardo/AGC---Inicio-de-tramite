<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Ubicacion.ascx.cs" Inherits="AnexoProfesionales.Controls.Ubicacion" %>

<%--Panel de Ubicacion--%>
<%--<asp:HiddenField ID="hid_id_solicitud" runat="server" />--%>


<asp:Panel ID="pnlUbicacion" runat="server">


    <asp:GridView ID="gridubicacion_db"
        runat="server"
        AutoGenerateColumns="false"
        DataKeyNames="IdEncomiendaUbicacion"
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
                        <div style="margin-left: 20px;">
                        </div>
                    </div>
                    <asp:Panel ID="pnlbtnEliminar" runat="server">
                        <asp:LinkButton ID="btnEliminar" runat="server" CssClass="pull-right btn btn-default" title="Quitar Ubicación" OnClick="btnEliminar_Click" CommandArgument='<%# Eval("IdEncomiendaUbicacion") %>'>
                                  <i class="imoon imoon-close" style="color:#377bb5"></i>                                
                        </asp:LinkButton>


                        <asp:Panel ID="pnlbtnEditar" runat="server">
                            <asp:LinkButton ID="btnEditar" CssClass="pull-right btn btn-default" runat="server" title="Editar Ubicación" OnClick="btnEditar_Click" CommandArgument='<%# Eval("IdEncomiendaUbicacion") %>'>
                                 <i class="imoon imoon-pencil"  style="color:#377bb5"></i>                                
                            </asp:LinkButton>
                        </asp:Panel>
                    </asp:Panel>

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
                                                <asp:Image ID="imgMapa1" runat="server" CssClass="img-thumbnail" />
                                                <div class="col-sm-12 text-center">
                                                    <strong>Entre calles</strong>
                                                </div>
                                            </div>
                                            <div class="item">
                                                <asp:Image ID="imgMapa2" runat="server" CssClass="img-thumbnail" />
                                                <div class="col-sm-12 text-center">
                                                    <strong>Croquis</strong>
                                                </div>
                                            </div>
                                        </div>
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
                                                <asp:Label ID="lblPartidahorizontal_db" runat="server" Text='<% #Bind("DescripcionCompleta") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:DataList>
                                                <asp:Label ID="lblEmptyDataPartidasHorizontales_db" runat="server" Text="No posee"
                                                    Visible="false"></asp:Label>
                                    </asp:Panel>
                                    </li> </ul>
                                    <ul id="zonificacion">
                                        <li>Area de Mixtura / Distrito de Zonificación:
                                        <asp:Label ID="lbl_zonificacion_db" runat="server" Style="width: 100%" Font-Bold="true"></asp:Label></li>
                                    </ul>
                                    <asp:Panel ID="pnlPuertasview" runat="server">

                                        <ul>
                                            <li>Puertas:
                                        
                                        <asp:DataList ID="dtlPuertas_db" runat="server" RepeatDirection="Horizontal" RepeatColumns="1"
                                            CellSpacing="10" Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPuertas_db" runat="server" Text='<% #Bind("NombreCalle") %>'></asp:Label>
                                                <asp:Label ID="lnkNroPuerta_db" runat="server" Text='<% #Bind("NroPuerta") %>' CssClass="pleft5"></asp:Label>
                                            </ItemTemplate>
                                        </asp:DataList></li>

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
                                                                <asp:Label ID="lblTextTorre" CssClass="mleft10 mright20" Font-Bold="true" runat="server"></asp:Label>
                                                            </li>
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
                        </div>
                      
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <div class="mtop10 mleft20 mright40 table table-bordered mtop5" style="width: 1100px">
                <div class="mtop10 mbottom10 mleft10">
                    <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' />
                    <span class="mleft10">No se encontraron registros.</span>
                </div>
            </div>
        </EmptyDataTemplate>
    </asp:GridView>
    <hr />

    <%--Plantas a habilitar--%>
    <asp:Panel ID="pnlPlantasHabilitar" runat="server" Style="padding-left: 10px">
        <strong>Plantas a habilitar:</strong>
        <asp:Label ID="lblPlantasHabilitar" runat="server"></asp:Label>
        <br />

    </asp:Panel>

</asp:Panel>

<br>
<asp:CheckBox ID="chkServidumbre" CssClass="mleft10" runat="server" Text=" Posee Servidumbre de paso" Style="display: none" ToolTip="Al haberse escogido más de una ubicación, la opción debe estar tildada." />
<asp:Label ID="lblServidumbre" runat="server" CssClass="alert alert-warning" Text="(En caso de seleccionar más de una ubicación, deberá declarar la existencia de Servidumbre de Paso entre las dos parcelas. Esta selección tiene carácter de Declaración Jurada)" Style="display: none"></asp:Label>
<asp:CheckBox ID="chkGaleria" Visible="false" runat="server" Text="&nbsp;&nbsp;El local a habilitar esta contenido por Galerías, centros comerciales, paseo de compras" />
<br />
<asp:CheckBox ID="chkPlantasConsecutivas" Visible="false" runat="server" Text="&nbsp;&nbsp;Las plantas declaradas son consecutivas y superan los 10 mts de altura" />
<br />

