<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="GetObleaTransmision.aspx.cs" Inherits="SSIT.Mobile.GetObleaTransmision" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Tramite</title>
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <meta name="viewport" content="width=device-width" />
    <meta name="ROBOTS" content="NOINDEX, NOFOLLOW" />
    
    <link href="<%: ResolveClientUrl("~/Content/Site.css")  %>" rel="stylesheet" />

</head>
<%: Scripts.Render("~/bundles/modernizr") %>
<body style="margin: 0; background-color: white">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="180">
            <Scripts>
                <%--To learn more about bundling scripts in ScriptManager see http://go.microsoft.com/fwlink/?LinkID=272931&clcid=0x409 --%>
                <%--Framework Scripts--%>

                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="jquery.ui.combined" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />
                <%--Site Scripts--%>
            </Scripts>
        </asp:ScriptManager>

        <div style="padding: 13px 0px 13px 0px">
            <img src="<%: ResolveClientUrl("~/Common/Images/LOGO_AGC.png")  %>" />
        </div>
        <div style="padding-left: 10px; border-top: solid 1px gray;">
            <asp:HiddenField runat="server" ID="hdfid_solicitud" />
            <asp:HiddenField runat="server" ID="hdfid_tarea" />
            <table>
                <tr>
                    <td>Nº de Expediente:</td>
                    <td colspan="3">
                        <b>
                            <asp:Label ID="lblNroExpediente" runat="server"></asp:Label></b>
                    </td>
                </tr>
                <tr>
                    <td>Tipo de Tr&aacute;mite:</td>
                    <td colspan="3">
                        <b><asp:Label ID="lblTipoTramite" runat="server"></asp:Label></b>
                    </td>
                </tr>
                <tr>
                    <td>Nro. Solicitud:</td>
                    <td colspan="3">
                        <b>
                            <asp:Label ID="lblNroSolicitud" runat="server"></asp:Label></b>
                    </td>
                </tr>               
                <tr>
                    <td>
                        <asp:Label ID="lblNroDisposicionSADEText" Text="Nro. Disposicion GEDO:" runat="server" Visible="true" /></td>
                    <td colspan="3">
                        <b>
                            <asp:Label ID="lblNroDisposicionSADE" runat="server" Visible="true"></asp:Label></b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblFechaDisposicionText" Text="Fecha Disposicion:" runat="server" Visible="true" /></td>
                    <td colspan="3">
                        <b>
                            <asp:Label ID="lblFechaDisposicion" runat="server" Visible="true"></asp:Label></b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblObservacionesText" Text="Notas adicionales para la Disposición:" runat="server" Visible="true" /></td>
                    <td colspan="3">
                        <b>
                            <asp:Label ID="lblObservaciones" runat="server" Visible="true"></asp:Label></b>
                    </td>
                </tr>
                <tr>
                    <td>Estado:</td>
                    <td colspan="3">
                        <b>
                            <asp:Label ID="lblEstado" runat="server"></asp:Label></b>
                    </td>
                </tr>
            </table>
            <div>
                <table>
                    <tr>
                        <td style="vertical-align: top">Titular/es:
                        </td>
                        <td>
                            <table>
                                <asp:Repeater ID="repeater_titulares" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td style="padding: 0">
                                                <b><span class="text"><%# Eval("ApellidoNomRazon")%></span></b>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div>

                <div>
                    <label>Direcci&oacute;n:</label>
                    <asp:Label ID="lblDireccion" runat="server" Font-Bold="true"></asp:Label>
                </div>
                <div>
                    <label>Plantas a habilitar:</label>
                    <asp:Label ID="lblPlantashabilitar" runat="server" Font-Bold="true"></asp:Label>
                </div>
                <div>
                    <label id="ZonaMix" runat="server"> Mixtura:</label>
                    <asp:Label ID="lblZonaDeclarada" runat="server" Font-Bold="true"></asp:Label>
                </div>
                <div>
                    <label>Cantidad de operarios:</label>
                    <asp:Label ID="lblCantOperarios" runat="server" Font-Bold="true"></asp:Label>
                </div>
                <asp:Panel ID="pnlSuperficieTramiteAnterior" runat="server" Visible="false">
                    <label>Superficie habilitada seg&uacute;n expediente <asp:Label ID="lblNroExpedienteAnteriorSuperficie" runat="server"></asp:Label>:</label>
                    <asp:Label ID="lblSuperficieTotalAnterior" runat="server" Font-Bold="true"></asp:Label>
                </asp:Panel>
                <div>
                    <label>Superficie total a habilitar</label>
                    <asp:Label ID="lblSuperficieTotal" runat="server" Font-Bold="true"></asp:Label>
                </div>

               
            </div>
            <br />
            <div class="col-sm-12 text-center">
                <strong>Ubicaciones</strong>
            </div>
            <br />
            <div class="mleft10 mtop10 mbottom10">
                <div class="item ">
                    <asp:Image ID="imgMapa1" runat="server" CssClass="img-thumbnail" onError="noExisteFotoParcela(this);" Style="border: solid 2px #939393; border-radius: 12px; width: 250px" />

                </div>
                <br />
                <asp:GridView ID="gridubicacion_db"
                    runat="server"
                    AutoGenerateColumns="false"
                    DataKeyNames="IdTransferenciaUbicacion"
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

                                    <div class="form-horizontal" style="margin-left: 10px; margin-top: -10px">
                                        <div class="form-group">

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
                                                            <asp:Label ID="lblEmptyDataPartidasHorizontales_db" runat="server" Text="No posee"
                                                                Visible="false" CssClass="col-sm-2"></asp:Label>
                                                </asp:Panel>
                                                </li>   </ul>

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
                        <div class="mtop10table table-bordered mtop5">
                            <div class="mtop10 mbottom10 mleft10">
                                <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' />
                                No se encontraron registros.
                            </div>
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
            <br />

            <asp:Panel ID="pnlRubrosTramiteAnterior" runat="server" Visible="false">

                <div>
                    <label>Rubros Habilitados según Expediente <asp:Label ID="lblNroExpedienteAnteriorRubros" runat="server"></asp:Label>:</label>
                </div>
                <asp:GridView ID="grdRubrosTramiteAnterior" runat="server" AutoGenerateColumns="false"
                    AllowPaging="false" Style="padding-right: 20px">
                    <Columns>
                        <asp:BoundField DataField="CodigoRubro" HeaderText="Código" ItemStyle-CssClass="celda_center" HeaderStyle-CssClass="celda_center" />
                        <asp:BoundField DataField="DescripcionRubro" HeaderText="Descripción" ItemStyle-CssClass="celda_left" HeaderStyle-CssClass="celda_left" />
                        <asp:BoundField DataField="TipoDocumentacionRequeridaDTO.Nomenclatura" HeaderText="Tipo" ItemStyle-CssClass="celda_center" HeaderStyle-CssClass="celda_center" />
                        <asp:BoundField DataField="SuperficieHabilitar" HeaderText="Superficie" ItemStyle-CssClass="celda_center" HeaderStyle-CssClass="celda_center" />
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="titulo-4">
                            No se encontraron datos
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>                

            </asp:Panel>

            <div class="mtop20">
                <label>Rubros solicitados:</label>
            </div>
            <asp:GridView ID="grdRubros" runat="server" AutoGenerateColumns="false"
                AllowPaging="false" Style="padding-right: 20px">
                <Columns>
                    <asp:BoundField DataField="CodigoRubro" HeaderText="Código" ItemStyle-CssClass="celda_center" HeaderStyle-CssClass="celda_center" />
                    <asp:BoundField DataField="DescripcionRubro" HeaderText="Descripción" ItemStyle-CssClass="celda_left" HeaderStyle-CssClass="celda_left" />
                    <%--<asp:BoundField DataField="TipoDocumentacionRequeridaDTO.Nomenclatura" HeaderText="Tipo" ItemStyle-CssClass="celda_center" HeaderStyle-CssClass="celda_center" />--%>
                    <asp:BoundField DataField="SuperficieHabilitar" HeaderText="Superficie" ItemStyle-CssClass="celda_center" HeaderStyle-CssClass="celda_center" />
                </Columns>
                <EmptyDataTemplate>
                    <div class="titulo-4">
                        No se ingresaron datos
                    </div>
                </EmptyDataTemplate>
            </asp:GridView>
            <br />
                <asp:GridView ID="grdSubRubros" runat="server" AutoGenerateColumns="false"
                    AllowPaging="false" Style="padding-right: 20px" Visible =" false">
                    <Columns>
                        <asp:BoundField DataField="RubrosDTO.Codigo" HeaderText="Rubro" ItemStyle-CssClass="celda_center" HeaderStyle-CssClass="celda_center" />
                        <asp:BoundField DataField="Nombre" HeaderText="Detalle" ItemStyle-CssClass="celda_left" HeaderStyle-CssClass="celda_left" />
                        
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="titulo-4">
                            No se encontraron datos
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            <br />
            <div>
                <label>Observaciones:</label>
                <asp:Label ID="lblObservacionesRubros" runat="server" Font-Bold="true"></asp:Label>
                <asp:Label ID="lblPlanoVisado" runat="server" Font-Bold="true"></asp:Label>
                <br />
            </div>

            <div id="pnlInformacionAdicional" runat="server">
                <table>
                    <tr>
                        <td><strong>Información adicional sobre la actividad:</strong></td>
                    </tr>
                    <tr>
                        <td><asp:BulletedList ID="blInfoAdicional" runat="server"></asp:BulletedList></td>
                    </tr>
                </table>
            </div>
            <div>
                <table>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Label ID="lblTextCertificado" runat="server" Visible="false"
                                Text="Se concede la presente Autorización de Actividad Económica, conforme la Declaración Responsable presentada por el Ciudadano Responsable y Profesional Responsable interviniente, en virtud de lo dispuesto en la Ley 6.101, Decreto N°40/2019 y Resolución N°84/AGC/2019. Se hace saber que la presente Autorización deberá ser actualizada trascurrido los cinco (5) años, de conformidad con el artículo 16 de la Ley 6.101 y el Capítulo IV, del ANEXO I, de la Resolución N°84/AGC/2019." />
                        </td>
                    </tr>
                </table>
            </div>
        </div>

    </form>
</body>
<script type="text/javascript">
    function noExisteFotoParcela(objimg) {

        $(objimg).attr("src", "../Content/img/app/ImageNotFound.png");

        return true;
    }
</script>
</html>
