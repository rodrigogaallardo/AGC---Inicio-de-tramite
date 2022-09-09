<%@ Page Title=" Ubicación" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ubicacion.aspx.cs" Inherits="AnexoProfesionales.Ubicacion" %>

<%@ Register Src="~/Tramites/Habilitacion/Controls/ZonaPlaneamiento.ascx" TagPrefix="uc" TagName="ZonaPlaneamiento" %>
<%@ Register Src="~/Tramites/Habilitacion/Controls/Ubicacion.ascx" TagPrefix="uc1" TagName="Ubicacion" %>
<%@ Register Src="~/Tramites/Habilitacion/Controls/BuscarUbicacion.ascx" TagPrefix="uc1" TagName="BuscarUbicacion" %>
<%@ Register Src="~/Tramites/Habilitacion/Controls/Titulo.ascx" TagPrefix="uc1" TagName="Titulo" %>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <%: Scripts.Render("~/bundles/autoNumeric") %>
    <%: Scripts.Render("~/bundles/select2") %>
    <%: Styles.Render("~/bundles/Select2Css") %>
    <%--ajax cargando ...--%>
    <div id="Loading" style="text-align: center; padding-bottom: 20px; margin-top: 120px">
        <table border="0" style="border-collapse: separate; border-spacing: 5px; margin: auto">
            <tr>
                <td>
                    <img src="<%: ResolveUrl("~/Content/img/app/Loading128x128.gif") %>" alt="" />
                </td>
            </tr>
            <tr>
                <td style="font-size: 24px">Cargando datos...
                </td>
            </tr>
        </table>
    </div>

    <div id="page_content" style="display: none">
        <uc1:Titulo runat="server" ID="Titulo" />

        <hr />

        <div class="row">

            <div class="col-sm-1 mtop10" style="width: 25px">
                <i class="imoon imoon-info fs24" style="color: #377bb5"></i>
            </div>
            <div class="col-sm-11">

                <p class="pad10">
                    En este paso deberá ingresar la ubicación donde se encuentra el local.<br />
                    La ubicación se puede ingresar a través del:
                </p>

            </div>
        </div>
        <asp:UpdatePanel ID="updUbicaciones" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="hid_return_url" runat="server" />
                <div class="row mleft20 mright20">
                    <div class="col-sm-9">
                        <ul class="pleft40">
                            <li>Domicilio.</li>
                            <li>N&uacute;mero de Partida (matriz o horizontal).</li>
                            <li>Datos Catastrales (Secci&oacute;n, manzana y Parcela).</li>
                            <li>Ubicaciones especiales.</li>
                        </ul>
                        <br />
                    </div>
                    <div class="text-right">
                        <asp:UpdatePanel ID="updAgregarUbicacion" runat="server">
                            <ContentTemplate>

                                 <asp:LinkButton ID="btnAgregarUbicacion" runat="server" CssClass="btn btn-primary mtop40" OnClientClick="showfrmAgregarUbicacion();">
                                    <i class="imoon imoon-plus"></i>
                                    <span class="text">Agregar Ubicaci&oacute;n</span>
                                </asp:LinkButton>
                                
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <asp:Button ID="btnCargarDatos" runat="server" OnClick="btnCargarDatos_Click" Style="display: none" />
                    <asp:HiddenField ID="hid_id_solicitud" runat="server" />

                    <%-- Box ubicaciones--%>
                    <div id="box_ubicacion" class="box-panel">

                        <div style="margin: 20px; margin-top: -5px">
                            <div style="margin-top: 5px; color: #377bb5">
                                <h4><i class="imoon imoon-map-marker" style="margin-right: 10px"></i>Datos de la Ubicaci&oacute;n</h4>
                                <hr />
                            </div>
                        </div>
                        <uc1:Ubicacion runat="server" ID="visUbicaciones" OnEliminarClick="visUbicaciones_EliminarClick" OnEditarClick="visUbicaciones_EditarClick" />
                        <br />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />

        <asp:UpdatePanel ID="updGaleria" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <div class="box-panel">


                    <div style="color: #377bb5">
                        <h4><i class="imoon imoon-stackexchange" style="margin-right: 10px"></i>El local a habilitar esta contenido por Galerías, centros comerciales, paseo de compras:</h4>
                        <hr />
                    </div>


                    <div class=" pleft20 mtop10">
                        <asp:RadioButton ID="chkSI" GroupName="galeria" Text="SI" runat="server" />
                        <asp:RadioButton ID="chkNO" GroupName="galeria" Text="NO" runat="server" />
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%-- Contenido del panel de plantas a habilitar --%>
        <asp:UpdatePanel ID="updPlantas" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <div class="box-panel">

                    <asp:Panel ID="pnlPlantasHabilitar" runat="server" CssClass="mtop5">

                        <div style="color: #377bb5">
                            <h4><i class="imoon imoon-stackexchange" style="margin-right: 10px"></i>Seleccione las Plantas</h4>
                            <hr />
                        </div>

                        <div class=" pleft20 mtop10">

                            <div class="form-group col-md-8">

                                <asp:GridView ID="grdPlantasHabilitar" runat="server" AutoGenerateColumns="false"
                                    GridLines="None" CellPadding="3" ShowHeader="false" OnRowDataBound="grdPlantasHabilitar_OnRowDataBound">
                                    <Columns>

                                        <asp:TemplateField ItemStyle-Width="100px">
                                            <ItemTemplate>
                                                <div class="checkbox mtop1">
                                                    <label>
                                                        <asp:CheckBox ID="chkSeleccionado" runat="server" Checked='<% #Eval("Seleccionado")%>'
                                                            OnCheckedChanged="chkSeleccionado_CheckedChanged" AutoPostBack="true" />
                                                        <%# Eval("Descripcion") %>
                                                        <asp:HiddenField ID="hid_id_tiposector" runat="server" Value='<% #Eval("id_tiposector")%>' />
                                                        <asp:HiddenField ID="hid_descripcion" runat="server" Value='<% #Eval("Descripcion")%>' />
                                                    </label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="form-inline">
                                                    <div class="form-group">
                                                        <asp:TextBox ID="txtDetalle" runat="server" Enabled='<% #Eval("Seleccionado")%>' CssClass="form-control mtop5 mbottom5" MaxLength='<% #Eval("TamanoCampoAdicional")%>'
                                                            Visible='<% #Eval("MuestraCampoAdicional")%>' Width="100px" Text='<% #Eval("detalle")%>'></asp:TextBox>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Panel ID="ReqtxtDetalle" runat="server" CssClass="alert alert-danger pad5 mbottom5 mtop5 mleft5" Style="display: none">
                                                            Debe ingresar la aclaración del item.
                                                        </asp:Panel>
                                                        <asp:Panel ID="ReqtxtDetalleIgual" runat="server" CssClass="alert alert-danger pad5 mbottom5 mtop5 mleft5" Style="display: none">
                                                            No puede haber descrpciones iguales.
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                            </div>
                            <div class="form-group col-md-4">

                                <div>Opciones con información adicional:</div>
                                <div style="padding-top: 10px"><b>Piso:</b> En esta opción deberá indicar la aclaración del piso. Ej: Piso 2, Piso 3</div>
                                <div style="padding-top: 10px"><b>Otro:</b> En esta opción deberá indicar la descripción deseada, alguna no incluida en la lista ofrecida.</div>
                                <div style="padding-top: 10px">
                                    <b>Nota:</b> En los campos "Otro" <b>NO</b> deberá ingresar información de otra cosa que no sea una planta a habilitar. <b>NO</b> indicar unidades funcionales, departamentos, locales o cualquier referencia a la ubicación.
                                </div>

                            </div>
                            <div class="form-group col-md-12 text-rigth">
                                <asp:CheckBox runat="server" ID="chkConsecutivas" Text="Las plantas declaradas son consecutivas y superan los 10 mts de altura." />
                            </div>
                        </div>


                    </asp:Panel>

                    <asp:Panel ID="Req_Plantas" runat="server" CssClass="alert alert-danger mtop10" Style="display: none">
                        Debe ingresar la informaci&oacute;n referida a las plantas.
                    </asp:Panel>

                </div>

            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="pnlResultados" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlGridResultados" runat="server" Visible="false" Style="display: block">

                    <asp:Label ID="lblTituloResultados" runat="server" CssClass="titulo-2" Style="padding: 0px 0px 5px 5px; width: 100%">Resultados de la b&uacute;squeda</asp:Label>


                    <asp:Label ID="lblCantResultados" runat="server"
                        Style="padding-left: 10px; font-size: small; font-style: italic"></asp:Label>

                    <hr />


                    <asp:GridView ID="gridubicacion" runat="server" AutoGenerateColumns="false" DataKeyNames="id_ubicacion"
                        Style="border: none; border-bottom: solid 1px #e1e1e1; padding-top: 5px"
                        GridLines="None   "
                        AllowPaging="true" PageSize="1">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <table style="width: auto">
                                        <tr>
                                            <td>
                                                <asp:Image ID="imgFotoParcela" runat="server" Style="outline: solid 2px #939393" onError="noExisteFotoParcela(this);" />
                                            </td>
                                            <td style="width: auto; padding-left: 10px; vertical-align: text-top">
                                                <div class="titulo-4">
                                                    Datos de la Ubicaci&oacute;n
                                                </div>


                                                <asp:Panel ID="pnlSMP" runat="server" Style="padding-top: 3px">
                                                    <div>
                                                        Número de Partida Matriz:
                                                            <asp:Label ID="grd_NroPartidaMatriz" runat="server"></asp:Label>
                                                    </div>
                                                    <div>
                                                        Sección:
                                                            <asp:Label ID="grd_seccion" runat="server"></asp:Label>
                                                        Manzana:
                                                            <asp:Label ID="grd_manzana" runat="server"></asp:Label>
                                                        Parcela:
                                                            <asp:Label ID="grd_parcela" runat="server"></asp:Label>
                                                    </div>
                                                </asp:Panel>

                                                <asp:Panel ID="pnlTipoUbicacion" runat="server" Style="padding-top: 3px" Visible="false">
                                                    <div>
                                                        <label class="titulo-4">Tipo de Ubicaci&oacute;n:</label>
                                                        <asp:Label ID="lblTipoUbicacion" runat="server"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <label class="titulo-4">Subtipo de Ubicaci&oacute;n:</label>
                                                        <asp:Label ID="lblSubTipoUbicacion" runat="server"></asp:Label>
                                                    </div>
                                                    <div>
                                                        <label class="titulo-4">Local:</label>
                                                        <asp:Label ID="lblLocal" runat="server"></asp:Label>
                                                    </div>
                                                </asp:Panel>


                                                <div style="padding-top: 10px">
                                                    <div class="titulo-4">
                                                        Zonificación de la Parcela
                                                    </div>
                                                    <asp:Label ID="grd_lblZonaParcela" runat="server"></asp:Label>
                                                </div>

                                                <asp:Panel ID="pnlPuertas" runat="server">
                                                    <div class="nota" style="margin-top: 5px; border-bottom: none">

                                                        <asp:UpdatePanel ID="updPanelPedidoAltaUbic" runat="server">
                                                            <ContentTemplate>

                                                                <ul style="padding: 5px 10px 5px 20px; margin: 0px">
                                                                    <li>Si la numeración de la puerta no es correcta, puede modificarla siempre que se encuentre dentro de la cuadra.</li>
                                                                    <li>Si la cantidad de puertas en la calle es inferior a las que ud posee, puede utilizar el botón "Agregar otra puerta en esta calle" y cambiar su numeración por la correcta.</li>
                                                                </ul>


                                                                <asp:UpdateProgress ID="updPrgPedidoAltaUbic" AssociatedUpdatePanelID="updPanelPedidoAltaUbic"
                                                                    runat="server" DisplayAfter="0">
                                                                    <ProgressTemplate>
                                                                        <img src="../Common/Images/Controles/Loading24x24.gif" alt="" />
                                                                    </ProgressTemplate>
                                                                </asp:UpdateProgress>


                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>

                                                    </div>

                                                    <div class="error-box" style="font-size: 9pt; border-top: none">
                                                        <ul style="padding: 5px 10px 5px 20px; margin: 0px">
                                                            <li>Si al intentar seleccionar las puertas, la calle no existe en el sistema haga 
                                                            <asp:LinkButton ID="LinkButton1" runat="server" Text="click aqu&iacute"
                                                                Style="font-size: 9pt"></asp:LinkButton>
                                                            </li>
                                                    </div>

                                                    <div class="titulo-4" style="border-bottom: solid 1px #e1e1e1; padding-top: 10px">
                                                        Puertas
                                                    </div>


                                                    <%--Grid para la selección de puertas--%>
                                                    <div style="display: none">
                                                        <asp:GridView ID="grdSeleccionPuertasCopia" runat="server" AutoGenerateColumns="false" GridLines="None"
                                                            Width="100%" ShowHeader="false">
                                                            <Columns>
                                                                <asp:BoundField DataField="tipo_puerta" />
                                                                <asp:BoundField DataField="id_puerta" />
                                                                <asp:BoundField DataField="CodigoCalle" />
                                                                <asp:BoundField DataField="NombreCalle" />
                                                                <asp:BoundField DataField="NroPuerta" />
                                                                <asp:BoundField DataField="DescripcionCompleta" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>

                                                    <asp:GridView ID="grdSeleccionPuertas" runat="server" AutoGenerateColumns="false" GridLines="None"
                                                        Width="100%" ShowHeader="false" Font-Size="9pt">
                                                        <Columns>

                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hid_seleccionPuerta_codigo_calle" runat="server" Value='<%# Eval("id_puerta") %>' />
                                                                    <asp:HiddenField ID="hid_seleccionPuerta_NroPuertaOriginal" runat="server" Value='<%# Eval("NroPuerta") %>' />

                                                                    <asp:CheckBox ID="chkSeleccionPuerta" runat="server" Text='<%# Eval("NombreCalle") %>' Font-Size="9pt" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField ItemStyle-Width="60px">
                                                                <ItemTemplate>
                                                                    <asp:TextBox class="spin" ID="txtSeleccionNroPuerta" runat="server" Text='<%# Eval("NroPuerta") %>'
                                                                        MaxLength="5" onkeypress="camposAutonumericosPuertas(this);" Width="50px" Font-Size="9pt"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkAgregarOtraPuerta" runat="server" Text="Agregar otra puerta en esta calle" Font-Size="9pt"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>

                                            </td>
                                        </tr>
                                        <tr>
                                            <%--Depto / Local / Otros--%>
                                            <td colspan="2">
                                                <asp:Panel ID="pnlDeptoLocal" runat="server" Visible="false">
                                                    <table border="0" cellpadding="2" style="margin-top: 10px; width: 100%">
                                                        <tr>                                                            
                                                            <td>
                                                                <label>Otros:</label>
                                                                <asp:TextBox ID="txtOtros" runat="server" MaxLength="50" Width="250px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 190px">
                                                                <label>Depto:</label>
                                                                <asp:TextBox ID="txtDepto" runat="server" MaxLength="8" Width="80px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 190px">
                                                                <label>Local:</label>
                                                                <asp:TextBox ID="txtLocal" runat="server" MaxLength="8" Width="80px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            
                                                            <td style="vertical-align: top; width: 250px">
                                                                <div style="font-size: 8pt; font-weight: bold">
                                                                    * En el campo otros, indicar los textos completos del sector a habilitar. Ej: "Oficina 23 y 24", "Sección 18", etc.
                                                                </div>
                                                            </td>
                                                            <td style="vertical-align: top">
                                                                <div style="font-size: 8pt; font-weight: bold; width: 170px">
                                                                    * En el campo Depto, indicar únicamente el nº o letra del departamento.
                                                                </div>
                                                            </td>
                                                            <td style="vertical-align: top">
                                                                <div style="font-size: 8pt; font-weight: bold; width: 170px">
                                                                    * En el campo Local, indicar únicamente el nº o letra del local.
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>

                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="2">
                                                <%--Grilla para seleccionar partida horizontal--%>
                                                <asp:UpdatePanel ID="updPartidasHorizontales" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Panel ID="pnlPartidasHorizontales" runat="server">
                                                            <div class="titulo-1" style="border-bottom: solid 1px #e1e1e1; padding-top: 10px; font-size: medium">
                                                                Partidas Horizontales o Subdivisiones:
                                                            </div>
                                                            <asp:Panel ID="pnlChecksListPHorizontales" runat="server" Style="overflow: auto; max-height: 154px">
                                                                <asp:CheckBoxList ID="CheckBoxListPHorizontales" runat="server" Width="750px" RepeatDirection="Horizontal"
                                                                    RepeatLayout="Table" RepeatColumns="3" CellPadding="1" Font-Size="9pt" AutoPostBack="true">
                                                                </asp:CheckBoxList>
                                                            </asp:Panel>
                                                        </asp:Panel>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <PagerTemplate>
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
                            </asp:Panel>

                        </PagerTemplate>

                        <EmptyDataTemplate>

                            <asp:Panel ID="pnlNotFound" runat="server" Style="padding: 10px; text-align: center">

                                <table>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Image ID="imgNotFound" runat="server" ImageUrl="~/Common/Images/Controles/NotFound.png" AlternateText="" />
                                        </td>
                                        <td style="width: 100%">
                                            <asp:Label ID="lblNotFound" runat="server" CssClass="titulo-2">No se encontraron datos con los parámetros de búsqueda indicados.</asp:Label>
                                        </td>
                                    </tr>

                                </table>

                            </asp:Panel>

                        </EmptyDataTemplate>

                    </asp:GridView>


                </asp:Panel>

            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Label ID="reqUbicacion" runat="server" Text="Debe ingresar al menos una ubicacion para continuar" CssClass="alert alert-danger" Style="display: none"></asp:Label>
        <%--Botones de Guardado--%>
        <asp:UpdatePanel ID="updBotonesGuardar" runat="server" UpdateMode="Conditional">
            <ContentTemplate>


                <div class="form-group form-inline ">
                    <div id="pnlBotonesGuardar">

                        <div id="Div1" class="col-sm-6 mtop10">
                            <asp:LinkButton ID="btnVolver" runat="server" CssClass="btn btn-default btn-lg" OnClick="btnVolver_Click" Style="display: none">
                        <i class="imoon imoon-arrow-left"></i>
                        <span class="text">Volver</span>
                            </asp:LinkButton>

                        </div>
                        <div class="text-right mtop10">
                            <asp:LinkButton ID="btnContinuar" runat="server" CssClass="btn btn-primary  btn-lg" OnClick="btnContinuar_Click" OnClientClick="return validarGuardar();">
                                <i class="imoon imoon-disk"></i>
                                <span class="text">Guardar y Continuar</span>
                            </asp:LinkButton>

                        </div>
                        <div class="form-group">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="200" AssociatedUpdatePanelID="updBotonesGuardar">
                                <ProgressTemplate>
                                    <img src='<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>' style="margin-left: 10px" alt="loading" />Guardando...
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>



    </div>

    <%--Modal mensajes de error--%>
    <div id="frmError" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top: -8px">Advertencia</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon-info fs64" style="color: #377bb5"></i>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="updmpeInfo" runat="server" class="form-group">
                                    <ContentTemplate>
                                        <asp:Label ID="lblError" runat="server"></asp:Label>
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
    <!-- /.modal -->

    <%--Modal Confirmar Eliminación--%>
    <div id="frmConfirmarEliminar" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top: -8px">Eliminar ubicaci&oacute;n</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon-remove-circle fs64 color-blue"></i>
                            </td>
                            <td style="vertical-align: middle">
                                <label class="mleft10">¿ Est&aacute; seguro de eliminar esta ubicaci&oacute;n ?</label>
                            </td>
                        </tr>
                    </table>

                </div>
                <div class="modal-footer mleft20 mright20">

                    <asp:UpdatePanel ID="updConfirmarEliminar" runat="server">
                        <ContentTemplate>

                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updConfirmarEliminar">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div id="pnlBotonesConfirmacion" class="form-group">
                                    <asp:Button ID="btnEliminar_Si" runat="server" CssClass="btn btn-primary" Text="Sí" OnClick="btnEliminar_Si_Click" OnClientClick="ocultarBotonesConfirmacion();" />
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


    <%-- Modal Agregar Ubicación --%>
    <div id="frmAgregarUbicacion" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header" style="background: #FFFFFF">
                    <h4 class="modal-title" style="margin-top: -5px">Buscar Ubicaci&oacute;n</h4>
                </div>
                <div class="modal-body" style="background: #efefef">
                    <uc1:BuscarUbicacion runat="server" ID="BuscarUbicacion" />
                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->

    <script type="text/javascript">

        $(document).ready(function () {
            $("#page_content").hide();
            $("#Loading").show();
            $("#<%: btnCargarDatos.ClientID %>").click();

        });
        function finalizarCarga() {
            $("#Loading").hide();
            $("#page_content").show();

            return false;

        }

        function comprobarnumericos() {
            $('.classAutonumeric').autoNumeric("init", { aSep: '', mDec: '0', vMax: '99' });
        }

        function showfrmError() {
            $("#pnlBotonesGuardar").show();
            $("#frmError").modal("show");
            return false;

        }
        function showfrmConfirmarEliminar() {

            $("#pnlBotonesConfirmacion").show();
            $("#frmConfirmarEliminar").modal("show");
            return false;
        }

        function hidefrmConfirmarEliminar() {

            $("#frmConfirmarEliminar").modal("hide");
            return false;
        }

        function ocultarBotonesConfirmacion() {

            $("#pnlBotonesConfirmacion").hide();
            return false;
        }

        function showfrmAgregarUbicacion() {

            $("#frmAgregarUbicacion").modal({
                "show": true,
                "backdrop": "static",
                "keyboard": false
            });
            return false;
        }

        function hidefrmAgregarUbicacion() {

            $("#frmAgregarUbicacion").modal("hide");
            canUbi = $("[id *= 'gridubicacion_db']").length;
            if (canUbi > 1) {
                $("#<%: reqUbicacion.ClientID %>").css("display", "none");
            }
            else {
                $("#<%: reqUbicacion.ClientID %>").css("display", "block");

            }
            return false;
        }

        function ocultarBotonesGuardado() {

            $("#pnlBotonesGuardar").hide();

            return true;
        }

        function validarGuardar() {

            var ret = true;
            var plantasSeleccionadas = false;

            $("#<%: Req_Plantas.ClientID %>").hide();
            $("#<%: grdPlantasHabilitar.ClientID %> [id*='_ReqtxtDetalle']").hide();

            plantasSeleccionadas = ($("#<%: grdPlantasHabilitar.ClientID %> :checkbox[checked]").length > 0);

            $("#<%: grdPlantasHabilitar.ClientID %> [id*='_txtDetalle']").each(function (index, element) {

                if ($(element).val().length == 0) {
                    var txtDetalle_id = $(element).prop("id");
                    var chkSeleccionado_id = txtDetalle_id.replace("_txtDetalle", "_chkSeleccionado");
                    var ReqtxtDetalle_id = txtDetalle_id.replace("_txtDetalle", "_ReqtxtDetalle");

                    if ($("#" + chkSeleccionado_id).prop("checked")) {


                        $("#" + ReqtxtDetalle_id).show();
                        ret = false;

                    }
                }

            })

            if (!plantasSeleccionadas) {
                $("#<%: Req_Plantas.ClientID %>").css("display", "inline-block");
                ret = false;
            }
            canUbi = $("[id *= 'gridubicacion_db']").length;
            if (canUbi > 1) {
                $("#<%: reqUbicacion.ClientID %>").css("display", "none");
            }
            else {
                $("#<%: reqUbicacion.ClientID %>").css("display", "block");
                ret = false;

            }
            if (ret)
                ocultarBotonesGuardado();

            return ret;
        }
        function checkClick(c) {
            if (c.checked)
                $("[data-controlid='CreateUserButton']").removeAttr("disabled")
            else
                $("[data-controlid='CreateUserButton']").attr("disabled", "disabled")
        }

    </script>

</asp:Content>
