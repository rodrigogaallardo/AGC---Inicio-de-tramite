<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BuscarUbicacion.ascx.cs" Inherits="SSIT.Solicitud.Habilitacion.Controls.BuscarUbicacion" %>
<%@ Register Src="~/Solicitud/Habilitacion/Controls/ZonaPlaneamiento.ascx" TagPrefix="uc" TagName="ZonaPlaneamiento" %>
<%@ Register Src="~/Solicitud/Controls/ucNuevaPuerta.ascx" TagPrefix="uc1" TagName="ucNuevaPuerta" %>


<%: Scripts.Render("~/bundles/autoNumeric") %>
<%: Scripts.Render("~/bundles/select2") %>


<div id="pnlBuscarUbicacion" style="width: 100%;">
    <asp:UpdatePanel ID="updBuscarUbicacion" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <asp:Panel ID="pnlContentBuscar" runat="server">

                <%--Tabs de Busqueda--%>
                <div id="tabs" class="mtop10">

                    <ul role="tablist" class="nav nav-tabs">
                        <li role="presentation" id="btnBuscarPorDom" class="active"><a data-toggle="tab" role="tab" aria-controls="profile" aria-expanded="true" href="#tabs-1"><strong style="color: #377bb5;">Domicilio</strong></a></li>
                        <li role="presentation" id="btnBuscarPorPartida"><a data-toggle="tab" role="tab" aria-controls="home" aria-expanded="false" href="#tabs-2"><strong style="color: #377bb5;">N&uacute;mero de Partida</strong></a></li>
                        <li role="presentation" id="btnBuscarPorSMP"><a data-toggle="tab" role="tab" aria-controls="messages" aria-expanded="false" href="#tabs-3"><strong style="color: #377bb5;">Secci&oacute;n / Manzana / Parcela</strong></a></li>
                        <li role="presentation" id="btnBuscarPorUbiEspecial"><a data-toggle="tab" role="tab" aria-controls="settings" aria-expanded="false" href="#tabs-4"><strong style="color: #377bb5;">Ubicaciones Especiales (Subte/Tren/etc)</strong></a></li>
                    </ul>

                    <asp:HiddenField ID="hid_tabselected" runat="server" />

                    <%--Tab Domicilio--%>
                    <div id="tabs-1" class="tab-pane box-panel-tab">

                        <asp:Panel ID="pnlDomicilio" runat="server" DefaultButton="btnBuscar2" ValidateRequestMode="Enabled">
                            <table>
                                <caption>
                                    <h4 style="margin-top: -3px; margin-bottom: -8px">Seleccione un domicilio:</h4>
                                    <hr></hr>
                                    <tr>
                                        <td><strong>Calle:</strong></td>
                                        <td style="padding-left: 25px">

                                            <asp:DropDownList ID="ddlCalles" placeholder="Seleccionar" runat="server" Width="500px">
                                            </asp:DropDownList>
                                            <br />
                                            <span>Debe ingresar un mínimo de 3 letras y el sistema le mostrará las calles posibles.</span>

                                        </td>
                                        <td></td>
                                    </tr>
                                </caption>
                            </table>

                            <asp:RequiredFieldValidator ID="ReqCalle" runat="server" ErrorMessage="Debe seleccionar una de las calles de la lista desplegable."
                                Display="Dynamic" ControlToValidate="ddlCalles" ValidationGroup="Buscar2"
                                CssClass="field-validation-error" Style="margin-left: 66px"></asp:RequiredFieldValidator>

                            <br />
                            <div class="mtop10"></div>
                            <table>
                                <tr>
                                    <td><strong>N&uacute;mero:</strong></td>
                                    <td style="padding-left: 6px">
                                        <asp:TextBox ID="txtNroPuerta" runat="server" Width="100px" MaxLength="5" CssClass="form-control"></asp:TextBox>
                                        <span>Debe ingresar el número de puerta</span>

                                    </td>
                            </table>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Debe ingresar el número de puerta"
                                Display="Dynamic" ControlToValidate="txtNroPuerta" ValidationGroup="Buscar2" CssClass="field-validation-error" Style="margin-left: 66px">
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="El número de puerta debe ser mayor a 0 (cero)."
                                Display="Dynamic" ControlToValidate="txtNroPuerta" ValidationGroup="Buscar2" CssClass="field-validation-error" Style="margin-left: 66px"
                                MinimumValue="1" MaximumValue="99999999"></asp:RangeValidator>

                            <hr />

                            <%--Contenedor del botón buscar--%>
                            <asp:UpdatePanel ID="updPanelBuscar2" runat="server" RenderMode="Inline">
                                <ContentTemplate>

                                    <div class="form-inline text-right">

                                        <div class="form-group">

                                            <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="updPanelBuscar2"
                                                runat="server" DisplayAfter="200" DynamicLayout="false">
                                                <ProgressTemplate>
                                                    <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" alt="" />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>

                                        </div>
                                        <div class="form-group">

                                            <asp:LinkButton ID="btnBuscar2" runat="server" CssClass="btn btn-primary" ValidationGroup="Buscar2" OnClick="btnBuscar2_Click">
                                                <i class="imoon imoon-search"></i>
                                                <span class="text">Buscar</span>
                                            </asp:LinkButton>

                                            <asp:LinkButton ID="btnCerrar2" runat="server" CssClass="btn btn-default" OnClick="btnCerrar_Click">
                                                <i class="imoon-close"></i>
                                                <span class="text">Cerrar</span>
                                            </asp:LinkButton>
                                        </div>

                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>

                    </div>

                    <%--Tab Partida--%>
                    <div id="tabs-2" class="tab-pane box-panel-tab">

                        <asp:Panel ID="pnlPartidas" runat="server" DefaultButton="btnBuscar1">
                            <div style="margin-top: -10px">
                                <h4>Seleccione el Tipo de Partida:
                                </h4>
                                <div class="divider" style="margin-left: 10px; margin-right: 10px"></div>
                                <table border="0" style="border-collapse: separate; border-spacing: 10px;">
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="optTipoPartidaHorizontal" runat="server" GroupName="TipoPartida" />
                                        </td>
                                        <td>
                                            <b>Horizontal:</b> Es aquella que figura en la boleta de ABL y corresponde a un
                                                                lote subdividido. Un ejemplo es un Edificio de departamentos.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="optTipoPartidaMatriz" runat="server" GroupName="TipoPartida"
                                                Checked="true" />
                                        </td>
                                        <td>
                                            <b>Matriz:</b> Es aquella que figura en la boleta de ABL y corresponde a un lote
                                                                sin subdividir. Un ejemplo es una casa.
                                        </td>
                                    </tr>
                                </table>
                                <div class="divider" style="margin-left: 10px; margin-right: 10px"></div>
                                <br />
                                <div class="form-inline">
                                    <div class="form-group">
                                        <strong>Ingrese el N&uacute;mero de Partida:</strong>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtNroPartida" runat="server" Width="90px" MaxLength="8" Style="padding-left: 5px" CssClass="form-control"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Debe ingresar el número de partida"
                                            Display="Dynamic" ControlToValidate="txtNroPartida" ValidationGroup="Buscar1"
                                            CssClass="field-validation-error"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="El número de partida debe ser mayor a 0 (cero)."
                                            Display="Dynamic" ControlToValidate="txtNroPartida" ValidationGroup="Buscar1"
                                            CssClass="field-validation-error" MinimumValue="1" MaximumValue="99999999"></asp:RangeValidator>
                                    </div>
                                </div>
                            </div>



                            <%--Contenedor del botón buscar--%>
                            <asp:UpdatePanel ID="updPanelBuscar1" runat="server" RenderMode="Inline">
                                <ContentTemplate>

                                    <div class="form-inline text-right">

                                        <div class="form-group">
                                            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="updPanelBuscar1"
                                                runat="server" DisplayAfter="200">
                                                <ProgressTemplate>
                                                    <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>

                                        <div class="form-group mbottom0">
                                            <asp:LinkButton ID="btnBuscar1" runat="server" CssClass="btn btn-primary" ValidationGroup="Buscar1" OnClick="btnBuscar1_Click">
                                                <i class="imoon imoon-search"></i>
                                                <span class="text">Buscar</span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnCerrar1" runat="server" CssClass="btn btn-default" OnClick="btnCerrar_Click" OnClientClick="return ">
                                                    <i class="imoon-close"></i>
                                                    <span class="text">Cerrar</span>
                                            </asp:LinkButton>
                                        </div>

                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </asp:Panel>


                    </div>

                    <%--Búsqueda por Nomenclatura Catastral--%>
                    <div id="tabs-3" class="tab-pane box-panel-tab">
                        <asp:Panel ID="pnlSMP_DFBTN" runat="server" DefaultButton="btnBuscar3">

                            <h4 style="margin-top: -5px">Ingrese los Datos Catastrales:</h4>
                            <div class="row" style="margin-top: -10px">
                                <div class="form-horizontal">
                                    <div class="form-inline">
                                        <hr style="margin-left: 20px; margin-right: 10px;"></hr>

                                        <asp:Label ID="lblSeccion" runat="server" Text="Secci&oacute;n:" class="col-sm-2 control-label" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtSeccion" runat="server" Width="90px" MaxLength="3" CssClass="col-sm-2 form-control"></asp:TextBox>

                                        <asp:Label ID="Label1" runat="server" Text="Manzana:" class="col-sm-2 control-label" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtManzana" runat="server" Width="90px" MaxLength="4" Style="text-transform: uppercase" CssClass="col-sm-2 form-control"></asp:TextBox>

                                        <asp:Label ID="Label2" runat="server" Text="Parcela:" class="col-sm-2 control-label" Font-Bold="true"></asp:Label>
                                        <asp:TextBox ID="txtParcela" runat="server" Width="90px" MaxLength="4" Style="text-transform: uppercase" CssClass="col-sm-2 form-control"></asp:TextBox>

                                    </div>

                                </div>
                            </div>
                            <div class="form-horizontal">
                                <div class="form-inline">

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Debe ingresar el número de Sección"
                                        Display="Dynamic" ControlToValidate="txtSeccion" ValidationGroup="Buscar3" CssClass="field-validation-error" Style="margin-left: 15px"></asp:RequiredFieldValidator>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Debe ingresar la Manzana."
                                        Display="Dynamic" ControlToValidate="txtManzana" ValidationGroup="Buscar3" CssClass="field-validation-error" Style="margin-left: 20px"></asp:RequiredFieldValidator>

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Debe ingresar la Parcela."
                                        Display="Dynamic" ControlToValidate="txtParcela" ValidationGroup="Buscar3" CssClass="field-validation-error" Style="margin-left: 55px"></asp:RequiredFieldValidator>

                                </div>
                            </div>
                            <hr />

                        </asp:Panel>
                        <%--Contenedor del botón buscar--%>
                        <asp:UpdatePanel ID="updPanelBuscar3" runat="server" RenderMode="Inline">
                            <ContentTemplate>

                                <div class="form-inline text-right">

                                    <div class="form-group">
                                        <asp:UpdateProgress ID="UpdateProgress3" AssociatedUpdatePanelID="updPanelBuscar3"
                                            runat="server" DisplayAfter="200">
                                            <ProgressTemplate>
                                                <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" alt="" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>

                                    </div>

                                    <div class="form-group">
                                        <asp:LinkButton ID="btnBuscar3" runat="server" CssClass="btn btn-primary" ValidationGroup="Buscar3" OnClick="btnBuscar3_Click">
                                                <i class="imoon imoon-search"></i>
                                                <span class="text">Buscar</span>
                                        </asp:LinkButton>

                                        <asp:LinkButton ID="btnCerrar3" runat="server" CssClass="btn btn-default" OnClick="btnCerrar_Click">
                                                <i class="imoon-close"></i>
                                                <span class="text">Cerrar</span>
                                        </asp:LinkButton>
                                    </div>

                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>

                    <%--Búsquedas Especiales (Subtes,Trnes,etc)--%>
                    <div id="tabs-4" class="tab-pane box-panel-tab">
                        <asp:Panel ID="pnlBusquedasEspeciales" runat="server" DefaultButton="btnBuscar4">

                            <asp:UpdatePanel ID="updBuscarUbicacioneEspeciales" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <h4 style="margin-top: -5px">Seleccione la ubicacion especial:</h4>
                                    <hr />
                                    <table style="border-collapse: separate; border-spacing: 5px">
                                        <tr>
                                            <td class="form-label"><strong>Tipo de Ubicaci&oacute;n:</strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlTipoDeUbicacion" runat="server" OnSelectedIndexChanged="ddlTipoDeUbicacion_SelectedIndexChanged"
                                                    AutoPostBack="true" Width="350px" CssClass="form-control">
                                                </asp:DropDownList>
                                                <div>
                                                    <asp:RequiredFieldValidator ID="ReqddlTipoDeUbicacion" runat="server" ControlToValidate="ddlTipoDeUbicacion" CssClass="field-validation-error"
                                                        Display="Dynamic" ErrorMessage="Debe seleccionar el tipo de ubicación." ValidationGroup="Buscar4"></asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form-label"><strong>Subtipo de Ubicaci&oacute;n:</strong>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSubTipoUbicacion" runat="server" Width="350px" CssClass="form-control">
                                                </asp:DropDownList>
                                                <div>

                                                    <asp:RequiredFieldValidator ID="ReqddlSubTipoUbicacion" runat="server" ControlToValidate="ddlSubTipoUbicacion" CssClass="field-validation-error"
                                                        Display="Dynamic" ErrorMessage="Debe seleccionar el sub tipo de ubicación." ValidationGroup="Buscar4"></asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="form-label">
                                                <asp:Label ID="lbldescUbicacion" Font-Bold="true" runat="server" Text="Local:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDescUbicacion" runat="server" MaxLength="25" Width="150px" CssClass="form-control"></asp:TextBox>
                                                <div>
                                                    <asp:RequiredFieldValidator ID="ReqtxtDescUbicacion" runat="server" ControlToValidate="txtDescUbicacion" CssClass="field-validation-error"
                                                        Display="Dynamic" ErrorMessage="Debe ingresar el Nº de local." ValidationGroup="Buscar4"></asp:RequiredFieldValidator>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>


                                </ContentTemplate>

                            </asp:UpdatePanel>

                            <%--Contenedor del botón buscar--%>
                            <asp:UpdatePanel ID="updPanelBuscar4" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <div class="form-inline text-right">

                                        <div class="form-group">

                                            <asp:UpdateProgress ID="UpdateProgress4" AssociatedUpdatePanelID="updPanelBuscar4"
                                                runat="server" DisplayAfter="200">
                                                <ProgressTemplate>
                                                    <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" alt="" />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>

                                        </div>
                                        <div class="form-group">

                                            <asp:LinkButton ID="btnbuscar4" runat="server" CssClass="btn btn-primary" ValidationGroup="Buscar4" OnClick="btnBuscar4_Click">
                                                <i class="imoon imoon-search"></i>
                                                <span class="text">Buscar</span>
                                            </asp:LinkButton>

                                            <asp:LinkButton ID="btnCerrar4" runat="server" CssClass="btn btn-default" OnClick="btnCerrar_Click">
                                                <i class="imoon-close"></i>
                                                <span class="text">Cerrar</span>
                                            </asp:LinkButton>
                                        </div>

                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </asp:Panel>
                    </div>

                </div>

            </asp:Panel>

            <asp:UpdatePanel ID="updpnlmessages" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel runat="server" ID="pnlAviso" Visible="false">
                        <div class="box-panel">
                            <div class="col-sm-12">
                                <div>
                                    <i class="imoon-black imoon-warning fs48 color-yellow"></i>
                                    <br />
                                </div>
                                <div class="mtop15 text-justify" id="divAtencion" runat="server">
                                    <b>ATENCIÓN:</b>&nbsp;<asp:Label ID="lblMsjAtencion" runat="server" Text=""></asp:Label>
                                </div>

                                <div class="margin-10-top">
                                    <asp:LinkButton runat="server" type="button" class="btn btn-primary" ID="btnRegistro" OnClick="btnRegistro_Click">¿Cómo registro mi puerta? </asp:LinkButton>
                                </div>
                                <div id="divRegistro" runat="server" style="display: none" class="margin-10-top">
                                    <b>Requisitos Del Trámite</b><br />
                                    <br />

                                    Solicitar el alta de nuevos números domiciliarios.<br>
                                    Quién puede realizar el trámite: profesionales habilitados o particulares.<br>
                                    Los interesados en iniciar este trámite ante la Dirección General de Registro de Obras y Catastro (DGROC) deberán completar los datos requeridos en el formulario y adjuntar la documentación que a continuación se detalla:<br>
                                    <ul>
                                        <li>Informe de dominio extendido por el registro de la propiedad inmueble</li>
                                        <li>Comprobante de pago<br>
                                        </li>
                                        <li>Adicionalmente, según el caso deberán presentar:<br>
                                            <ul>
                                                <li>En caso de edificios sometidos al régimen de la ley 13.512 deberá presentarse el consentimiento de la totalidad de los copropietarios.</li>
                                                <li>Otra documentación</li>
                                            </ul>
                                        </li>
                                    </ul>

                                    <br>
                                    A efectos del inicio del presente trámite, la persona deberá ser designada por el propietario como apoderado (en el sistema TAD) como requisito indispensable para la tramitación.
                                    <br>
                                    El pago se puede realizar a través de <a href="https://sir.buenosaires.gob.ar/" target="_blank">https://sir.buenosaires.gob.ar/<br>
                                    </a>
                                    Concepto: Certificado de Numeración Domiciliaria (Form:1200).<br>
                                    Para solicitar incorporar una presentación sobre el expediente o realizar una consulta, por favor hacerlo al siguiente mail: <a href="mailto:consultacatastro@buenosaires.gob.ar">consultacatastro@buenosaires.gob.ar</a><br>
                                    <div class="margin-10-top">
                                        <asp:LinkButton runat="server" type="button" class="btn btn-primary pull-right" ID="btnRegistroSalir" OnClick="btnRegistroSalir_Click"><i class="imoon imoon-arrow-left"></i><span class="text">Volver</span></asp:LinkButton>
                                    </div>

                                </div>
                            </div>
                        </div>
                        </div>


                            </div>
                        </div>   
                  <br />
                        <div class="modal-footer" id="footBtnContinuarAviso" runat="server" style="display: none;">
                            <div class="form-group">
                                <div class="form-inline text-right">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lnkCerrar" runat="server" CssClass="btn btn-default btn-primary" OnClick="btnContinuarAviso_Click"><i class="imoon imoon-arrow-left"></i>
                                         <span class="text">Volver</span></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

            <%--Resultados de la búsqueda--%>
            <asp:UpdatePanel ID="pnlResultados" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <asp:Panel ID="pnlGridResultados" runat="server" Visible="false" Class="tab-pane box-panel-tab" Style="display: block">


                        <legend>Resultados de la b&uacute;squeda
                            <asp:Label ID="lblCantResultados" runat="server" Style="padding-left: 10px; font-size: small; font-style: italic"></asp:Label>
                        </legend>

                        <asp:HiddenField ID="hid_id_selectedubicacion" runat="server" Value="" />
                        <div style="max-height: 50vh; overflow: auto">
                            <div class="form-inline" style="display: none">
                                <asp:Label ID="lblfoto" runat="server" CssClass="label-primary" Text="Foto" Width="10%"></asp:Label>
                                <asp:Label ID="Label3" runat="server" CssClass="label-primary" Text="direccion" Width="60%"></asp:Label>
                                <asp:Label ID="Label4" runat="server" CssClass="label-primary" Text="agregar ubicacion" Width="30%"></asp:Label>
                            </div>
                            <asp:GridView ID="grdResultados" runat="server" AutoGenerateColumns="false" DataKeyNames="IdUbicacion"
                                AllowPaging="true" PageSize="10" AllowSorting="false" GridLines="None" CssClass="table table-bordered mtop5"
                                ItemType="DataTransferObject.ItemDirectionDTO" OnRowDataBound="grdResultados_RowDataBound">

                                <Columns>


                                    <asp:TemplateField HeaderText="Foto" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center mtop40">
                                        <ItemTemplate>
                                            <%-- <asp:Image ID="imgCargando" runat="server" ImageUrl="~/Content/img/app/Loading64x64.gif" />--%>
                                            <asp:ImageButton ID="imgfoto" runat="server" onerror="noExisteFotoParcela(this);" Style="border: solid 2px #939393; border-radius: 12px; width: 170px;" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ubicación" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="200px">
                                        <ItemTemplate>
                                            <div style="margin-top: 50px">
                                                <asp:Label runat="server" CssClass="mtop40" Text='<%# Item.direccion %>' />

                                                <asp:Label runat="server" CssClass="mtop40" Text='<%# Item.Numero %>' />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Agregar Ubicación" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center mtop40">
                                        <ItemTemplate>
                                            <div style="margin-top: 50px">
                                                <asp:LinkButton ID="lnkubicacion" ToolTip="Agregar Ubicación" CssClass="btn btn-primary" Style="vertical-align: central !important" OnClick="lnkUbicacion_Click" runat="server" CommandArgument='<%# Item.idUbicacion %>'>
                                                    
                                                    <i class="imoon imoon-ok"></i>
                                                </asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="180px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>

                        <fieldset>

                            <asp:GridView ID="gridubicacion"
                                runat="server"
                                AutoGenerateColumns="false"
                                DataKeyNames="IdUbicacion"
                                OnRowDataBound="gridubicacion_OnRowDataBound"
                                ShowHeader="false"
                                Width="100%"
                                GridLines="None"
                                OnDataBound="gridubicacion_DataBound"
                                AllowPaging="true"
                                Style="margin-top: -10px"
                                PageSize="1"
                                OnPageIndexChanging="gridubicacion_PageIndexChanging"
                                ItemType="DataTransferObject.UbicacionesDTO">
                                <Columns>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hid_id_ubicacion" runat="server" Value="<%# Item.IdUbicacion %>" />
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 305px">

                                                        <strong>Datos de la Ubicaci&oacute;n</strong>
                                                        <asp:Panel ID="pnlSMP" runat="server">
                                                            <div>
                                                                Número de Partida Matriz: <b><%# Item.NroPartidaMatriz  %></b>
                                                                <span class="label-azul"></span>
                                                            </div>
                                                            <div>
                                                                Sección:
                                                                    <asp:Label Font-Bold="true" ID="grd_seccion" runat="server" Text="<%# Item.Seccion %>" CssClass="label-azul"></asp:Label>
                                                                Manzana: 
                                                                    <asp:Label Font-Bold="true" ID="grd_manzana" runat="server" Text="<%# Item.Manzana %>" CssClass="label-azul"></asp:Label>
                                                                Parcela:
                                                                    <asp:Label Font-Bold="true" ID="grd_parcela" runat="server" Text="<%# Item.Parcela %>" CssClass="label-azul"></asp:Label>
                                                                <div>
                                                                    Tipo Ubicación:
                                                                    <asp:Label Font-Bold="true" ID="lblTipoUbicacion1" runat="server" CssClass="label-azul"></asp:Label>
                                                                </div>
                                                                <div style="display: block;">
                                                                    Subtipo Ubicación:
                                                                    <asp:Label Font-Bold="true" ID="lblSubTipoUbicacion1" runat="server" CssClass="label-azul"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>

                                                        <asp:Panel ID="pnlTipoUbicacion" runat="server" Style="padding-top: 3px" Visible="false">
                                                            <%-- <div>
                                                                Ubicaci&oacute;n:
                                                                    <asp:Label ID="lblTipoUbicacion" runat="server" CssClass="label-azul"></asp:Label>
                                                            </div>--%>
                                                            <div>
                                                                Sección:
                                                                    <asp:Label Font-Bold="true" ID="grd_seccion1" runat="server" Text="<%# Item.Seccion %>" CssClass="label-azul"></asp:Label>
                                                                Manzana: 
                                                                    <asp:Label Font-Bold="true" ID="grd_manzana1" runat="server" Text="<%# Item.Manzana %>" CssClass="label-azul"></asp:Label>
                                                                Parcela:
                                                                    <asp:Label Font-Bold="true" ID="grd_parcela1" runat="server" Text="<%# Item.Parcela %>" CssClass="label-azul"></asp:Label>
                                                                <div>
                                                                    Tipo Ubicación:
                                                                    <asp:Label Font-Bold="true" ID="lblTipoUbicacion" runat="server" CssClass="label-azul"></asp:Label>
                                                                </div>
                                                                <div style="display: block;">
                                                                    Subtipo Ubicación:
                                                                    <asp:Label Font-Bold="true" ID="lblSubTipoUbicacion" runat="server" CssClass="label-azul"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <%--<div>
                                                                Detalle:
                                                                    <asp:Label ID="lblSubTipoUbicacion" runat="server" CssClass="label-azul"></asp:Label>
                                                            </div>--%>
                                                            <div>
                                                                Local:
                                                                    <asp:Label ID="lblLocal" runat="server" CssClass="label-azul"></asp:Label>
                                                            </div>
                                                        </asp:Panel>
                                                        <br />
                                                        <div style="min-height: 250px; min-width: 300px">
                                                            <img id="imgCargando" src='<%: ResolveUrl("~/Content/img/app/Loading128x128.gif") %>' alt="" style="margin-left: 60px; margin-top: 40px" />
                                                            <img id="imgFotoParcela" class="img-polaroid" src="<%# GetUrlFoto(300,250,Item.Seccion,Item.Manzana,Item.Parcela ) %>" onload="fotoCargada('imgCargando',this);" onerror="noExisteFotoParcela(this);" style="display: none; border: solid 2px #939393; border-radius: 12px; width: 290px" />
                                                            <br />
                                                            <img id="imgMapa1" src="<%# GetUrlMapa(Item.Seccion,Item.Manzana,Item.Parcela, Item.Direccion) %>" onload="fotoCargada('imgCargando',this);" onerror="noExisteFotoParcela(this);" style="display: none; border: solid 2px #939393; border-radius: 12px; width: 290px" />
                                                            <br />
                                                            <img id="imgMapa2" src="<%# GetUrlCroquis(Item.Seccion,Item.Manzana,Item.Parcela, Item.Direccion) %>" onload="fotoCargada('imgCargando',this);" onerror="noExisteFotoParcela(this);" style="display: none; border: solid 2px #939393; border-radius: 12px; width: 290px" />
                                                        </div>
                                                    </td>
                                                    <td style="width: 10px; border-right: solid 1px #eeeeee"></td>
                                                    <td style="width: auto; padding-left: 10px; vertical-align: text-top">
                                                        <asp:Panel ID="pnlPuertas" runat="server">
                                                            <div style="overflow: auto; max-height: 800px">
                                                                <div class="box-panel" style="margin-left: 5px; width: 700px; margin-top: 5px">
                                                                    <div id="lblSeleccionarPuerta" runat="server">
                                                                        <h5><i class="imoon imoon-office" style="margin-right: 4px"></i>Puertas -
                                                                <label style="font-weight: bold !important">Debe seleccionar con un Click la puerta correspondiente</label></h5>
                                                                    </div>
                                                                    <label id="lblPartidaInhibida" class="alert alert-warning" runat="server" style="background-color: #fcda59; display: none"><i class="imoon imoon-notification" style="margin-right: 4px"></i>Se pone en conocimiento que el domicilio declarado por usted presenta irregularidades. Por favor acerquese a nuestras oficinas ubicadas en TTE. GRAL. JUAN DOMINGO PERON 2941.</label>
                                                                    <hr style="margin-top: -5px"></hr>

                                                                    <div class="alert alert-small alert-warning">
                                                                        Si al intentar seleccionar las puertas, la calle no existe en el sistema. Haga click 
                                                                        <asp:LinkButton ID="btnNuevaPuerta" runat="server" Text="aquí" OnClick="btnNuevaPuerta_Click" Style="color: #377bb5;"
                                                                            CommandArgument="<%# Item.IdUbicacion %>"></asp:LinkButton>
                                                                    </div>
                                                                    <p>La parcela que usted ingresó tiene las siguientes puertas registradas: </p>
                                                                    <br />
                                                                    <asp:UpdatePanel ID="updPuertas" runat="server" style="margin-left: 5px; width: 700px;">
                                                                        <ContentTemplate>
                                                                            <asp:DataList ID="lstPuertas"
                                                                                runat="server"
                                                                                RepeatColumns="1"
                                                                                RepeatDirection="Vertical"
                                                                                RepeatLayout="Table"
                                                                                Width="100%"
                                                                                OnItemDataBound="lstPuertas_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                    <asp:HiddenField ID="hid_ubic_puerta" runat="server" Value='<%# Eval("IdUbicacionPuerta") %>' />
                                                                                    <asp:HiddenField ID="hid_codigo_calle" runat="server" Value='<%# Eval("CodigoCalle") %>' />
                                                                                    <asp:HiddenField ID="hid_NroPuerta_ubic" runat="server" Value='<%# Eval("NroPuertaUbic") %>' />

                                                                                    <div class="form-inline">
                                                                                        <div class="control-group">
                                                                                            <label class="checkbox">
                                                                                                <asp:CheckBox ID="chkPuerta" CssClass="chkPuertaClass" runat="server" />
                                                                                                <asp:Label ID="lblnombreCalle" runat="server" Text='<%# Eval("Nombre_calle") %>'></asp:Label>
                                                                                            </label>
                                                                                            <asp:TextBox ID="txtNroPuerta" runat="server" Text='<%# Eval("NroPuertaUbic") %>' Width="65px" CssClass="form-control grid-txtNroPuerta" Enabled="false" ReadOnly="true"></asp:TextBox>


                                                                                            <%--                                                                                            <asp:LinkButton ID="lnkAgregarOtraPuerta" runat="server" Text="Agregar otra puerta" Style="display: inline"
                                                                                                data-toggle="tooltip"
                                                                                                ToolTip="Agrega otra puerta en la misma calle y dentro de la misma cuadra." CssClass="AgregarOtraPuerta mleft5"
                                                                                                Font-Size="9pt" OnClick="lnkAgregarOtraPuerta_Click" CommandArgument='<%# Eval("IdUbicacionPuerta")  %>'>
                                                                                            </asp:LinkButton>--%>
                                                                                        </div>
                                                                                        <br />
                                                                                    </div>


                                                                                </ItemTemplate>

                                                                            </asp:DataList>

                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <br />
                                                                <br />

                                                                <%--Depto / Local / Otros--%>

                                                                <asp:Panel ID="pnlDeptoLocal" runat="server" Visible="true" CssClass="box-panel pbottom10" Style="margin-left: 5px; width: 700px;">

                                                                    <div class=" form-horizontal form-group">

                                                                        <asp:Label class="control-label col-sm-2" runat="server"><b>Otros:</b></asp:Label>
                                                                        <div class="col-sm-4">
                                                                            <asp:TextBox ID="txtOtros" runat="server" MaxLength="50" Width="250px" CssClass="form-control"></asp:TextBox>
                                                                            <div style="font-size: 8pt; font-weight: bold; color: #9a9a9a; width: 350px;">
                                                                                * Indicar los textos completos del sector deseado.
                                                                                        <br />
                                                                                Ej: "Oficina 23 y 24", "Sección 18", etc.
                                                                              
                                                                             
                                                                            </div>
                                                                        </div>


                                                                        <asp:Label ID="Label6" runat="server" CssClass="control-label col-sm-2"><b>Local:</b></asp:Label>
                                                                        <div class="col-sm-4">
                                                                            <asp:TextBox ID="txtLocal" runat="server" MaxLength="8" Width="80px" CssClass="form-control"></asp:TextBox>
                                                                            <div style="font-size: 8pt; font-weight: bold; width: 200px; color: #9a9a9a">
                                                                                * Indicar únicamente el nº o letra del local.
                                                                            </div>
                                                                        </div>

                                                                    </div>

                                                                    <br />
                                                                    <br />
                                                                    <br />
                                                                    <br />

                                                                    <div class="form-horizontal form-group">
                                                                        <asp:Label ID="Label5" runat="server" class="col-sm-2 control-label"><b>Depto:</b></asp:Label>
                                                                        <div class="col-sm-4">
                                                                            <asp:TextBox ID="txtDepto" runat="server" MaxLength="3" Width="80px" CssClass="form-control"></asp:TextBox>
                                                                            <div style="font-size: 8pt; font-weight: bold; width: 200px; color: #9a9a9a">
                                                                                * Indicar únicamente el nº o letra del departamento.
                                            
                                                                            </div>
                                                                        </div>



                                                                        <asp:Label runat="server" CssClass="control-label col-sm-2"><b>Torre:</b></asp:Label>
                                                                        <div class="col-sm-2">
                                                                            <asp:TextBox ID="txtTorre" runat="server" MaxLength="3" Width="80px" CssClass="form-control"></asp:TextBox>

                                                                        </div>
                                                                    </div>

                                                                </asp:Panel>

                                                                <br />
                                                                <br />
                                                                <%--Grilla para seleccionar partida horizontal--%>
                                                                <asp:UpdatePanel ID="updPartidasHorizontales" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:Panel ID="pnlPartidasHorizontales" runat="server" Style="border-bottom: solid 1px #e1e1e1; display: none; margin-left: 5px; width: 700px;" CssClass="BuscarUbicacion-pnlPartidasHorizontales box-panel">
                                                                            <div class="titulo-1" style="border-bottom: solid 1px #e1e1e1; padding-top: 5px; font-size: medium">
                                                                                <b>Partidas Horizontales o Subdivisiones:</b>
                                                                            </div>
                                                                            <asp:Panel ID="pnlChecksListPHorizontales" runat="server" Style="overflow: auto; max-height: 135px">
                                                                                <asp:CheckBoxList ID="CheckBoxListPHorizontales" Style="width: 660px" runat="server" RepeatDirection="Vertical"
                                                                                    RepeatLayout="Table" RepeatColumns="3" CellPadding="1" Font-Size="9pt" OnDataBound="CheckBoxListPHorizontales_DataBound">
                                                                                </asp:CheckBoxList>
                                                                            </asp:Panel>
                                                                        </asp:Panel>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>

                                                            </div>
                                                        </asp:Panel>

                                                    </td>
                                                </tr>

                                            </table>




                                            <div class="alert alert-info alert-block" style="margin-top: 10px; display: none">
                                                <strong>Info!</strong>
                                                <ul>
                                                    <li>Si la numeración de la puerta no es correcta, puede modificarla siempre que se encuentre
                                                                        dentro de la cuadra.</li>
                                                    <li>Si la cantidad de puertas en la calle es inferior a las que ud posee, puede utilizar
                                                                        el botón "Agregar otra puerta" y cambiar su numeración por la correcta.</li>
                                                </ul>
                                            </div>

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerTemplate>
                                    <asp:Panel ID="pnlpager" runat="server" Style="padding: 10px; text-align: center; border-top: solid 1px #e1e1e1">
                                        <asp:Button ID="cmdAnterior" runat="server" Text="<< Anterior" OnClick="cmdAnterior_Click"
                                            CssClass="btn btn-default" Width="100px" />
                                        <asp:Button ID="cmdPage1" runat="server" Text="1 " OnClick="cmdPage" CssClass="btn btn-default"
                                            Width="22px" />
                                        <asp:Button ID="cmdPage2" runat="server" Text="2" OnClick="cmdPage" CssClass="btn btn-default"
                                            Width="22px" />
                                        <asp:Button ID="cmdPage3" runat="server" Text="3" OnClick="cmdPage" CssClass="btn btn-default"
                                            Width="22px" />
                                        <asp:Button ID="cmdPage4" runat="server" Text="4" OnClick="cmdPage" CssClass="btn btn-default"
                                            Width="22px" />
                                        <asp:Button ID="cmdPage5" runat="server" Text="5" OnClick="cmdPage" CssClass="btn btn-default"
                                            Width="22px" />
                                        <asp:Button ID="cmdPage6" runat="server" Text="6" OnClick="cmdPage" CssClass="btn btn-default"
                                            Width="22px" />
                                        <asp:Button ID="cmdPage7" runat="server" Text="7" OnClick="cmdPage" CssClass="btn btn-default"
                                            Width="22px" />
                                        <asp:Button ID="cmdPage8" runat="server" Text="8" OnClick="cmdPage" CssClass="btn btn-default"
                                            Width="22px" />
                                        <asp:Button ID="cmdPage9" runat="server" Text="9" OnClick="cmdPage" CssClass="btn btn-default"
                                            Width="22px" />
                                        <asp:Button ID="cmdPage10" runat="server" Text="10" OnClick="cmdPage" CssClass="btn btn-default"
                                            Width="22px" />
                                        <asp:Button ID="cmdPage11" runat="server" Text="11" OnClick="cmdPage" CssClass="btn btn-default"
                                            Width="22px" />
                                        <asp:Button ID="cmdPage12" runat="server" Text="12" OnClick="cmdPage" CssClass="btn btn-default"
                                            Width="22px" />
                                        <asp:Button ID="cmdPage13" runat="server" Text="13" OnClick="cmdPage" CssClass="btn btn-default"
                                            Width="22px" />
                                        <asp:Button ID="cmdPage14" runat="server" Text="14" OnClick="cmdPage" CssClass="btn btn-default"
                                            Width="22px" />
                                        <asp:Button ID="cmdPage15" runat="server" Text="15" OnClick="cmdPage" CssClass="btn btn-default"
                                            Width="22px" />
                                        <asp:Button ID="cmdPage16" runat="server" Text="16" OnClick="cmdPage" CssClass="btn btn-default"
                                            Width="22px" />
                                        <asp:Button ID="cmdPage17" runat="server" Text="17" OnClick="cmdPage" CssClass="btn btn-default"
                                            Width="22px" />
                                        <asp:Button ID="cmdPage18" runat="server" Text="18" OnClick="cmdPage" CssClass="btn btn-default"
                                            Width="22px" />
                                        <asp:Button ID="cmdPage19" runat="server" Text="19" OnClick="cmdPage" CssClass="btn btn-default"
                                            Width="22px" />
                                        <asp:Button ID="cmdSiguiente" runat="server" Text="Siguiente >>" OnClick="cmdSiguiente_Click"
                                            CssClass="btn btn-default" Width="100px" />
                                    </asp:Panel>
                                </PagerTemplate>
                                <EmptyDataTemplate>
                                    <asp:Panel ID="pnlNotFound" runat="server" Style="padding: 10px;">
                                        <div class="form-inline">
                                            <div class="controls">
                                                <div class="mtop10">

                                                    <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                                                    <span class="mleft10">No se encontraron datos con los par&aacute;metros de b&uacute;squeda indicados.</span>

                                                </div>
                                            </div>
                                        </div>

                                    </asp:Panel>
                                </EmptyDataTemplate>
                            </asp:GridView>

                            <asp:Panel ID="pnlValidacionIngresoUbicacion" runat="server" CssClass="field-validation-error">

                                <asp:BulletedList ID="lstValidacionesUbicacion" runat="server">
                                </asp:BulletedList>
                            </asp:Panel>

                        </fieldset>
                        <%--Botones de Nuva Búsqueda y de Ingresar--%>

                        <asp:UpdatePanel ID="updPanelBotones" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="pnlbotonesingreso" runat="server">

                                    <div class="form-inline text-center mtop5" style="margin-left: 80px">

                                        <div class="form-group">
                                            <asp:LinkButton ID="btnNuevaBusqueda" runat="server" CssClass="btn btn-default"
                                                OnClick="btnNuevaBusquedar_Click">
                                                <i class="imoon-white imoon-search"></i>
                                                <span class="text">Nueva Busqueda</span>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="form-group">
                                            <asp:LinkButton ID="btnIngresarUbicacion" runat="server" CssClass="btn btn-primary"
                                                OnClick="btnIngresarUbicacion_Click" OnClientClick="return validarIngresarUbicacion();">
                                                <i class="imoon-white imoon-plus"></i>
                                                <span class="text">Aceptar</span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnEditarUbicacion" runat="server" CssClass="btn btn-primary"
                                                OnClick="btnEditarUbicacion_Click" OnClientClick="return validarIngresarUbicacion();" Visible="false">
                                                <i class="imoon-white imoon-plus"></i>
                                                <span class="text">Aceptar</span>
                                            </asp:LinkButton>
                                        </div>

                                        <div class="form-group">
                                            <asp:UpdateProgress ID="UpdateProgress5" AssociatedUpdatePanelID="updPanelBotones"
                                                runat="server" DisplayAfter="200">
                                                <ProgressTemplate>
                                                    <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" alt="" />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>

                                        <div class="form-group pull-right">
                                            <asp:LinkButton ID="btnCerrar5" runat="server" CssClass="btn btn-default" OnClick="btnCerrar_Click">
                                                    <i class="imoon-close"></i>
                                                    <span class="text">Cerrar</span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    </div>


                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </asp:Panel>



                </ContentTemplate>
            </asp:UpdatePanel>

            <div id="box_zonaPlaneamiento">
                <uc:ZonaPlaneamiento runat="server" ID="ZonaPlaneamiento" />
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>



    <%--Modal mensajes de error--%>
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
                                        <asp:Label ID="lblError" runat="server" class="pad10"></asp:Label>
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

    <%--Modal Confirmar Anulación--%>
    <asp:UpdatePanel ID="udpModalSinPartida" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="frmConfirmarNoPH" class="modal fade" role="dialog" style="margin-top: 510px">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" style="margin-top: -8px">Unidades Funcionales</h4>
                        </div>
                        <div class="modal-body">
                            <table style="border-collapse: separate; border-spacing: 5px">
                                <tr>
                                    <td style="text-align: center; vertical-align: text-top">
                                        <i class="imoon imoon-info fs64 color-blue"></i>
                                    </td>
                                    <td style="vertical-align: middle">
                                        <div>
                                            El domicilio ingresado tiene unidades funcionales (UF). 
                                        </div>
                                    </td>
                                </tr>
                            </table>

                        </div>
                        <div class="modal-footer mleft20 mright20">

                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:LinkButton ID="btnEditarSinPartida" runat="server" Text="Continuar sin UF" CssClass="btn btn-default" OnClick="btnEditarUbicacion_Click" Visible="false" OnClientClick="return hidefrmConfirmarNoPHEdit(true)"></asp:LinkButton>
                                    <button id="btnGuardarSinPartida" runat="server" type="button" class="btn btn-default" onclick="return hidefrmConfirmarNoPH(true);">Continuar sin UF</button>
                                    <button type="button" class="btn btn-default" onclick="return hidefrmConfirmarNoPH(false);">Seleccionar una UF</button>
                                </div>
                            </div>



                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- /.modal -->
    <asp:UpdatePanel ID="udpfrmSolicitarNuevaPuerta" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <%-- Modal Solicitar nueva puerta por mail --%>
            <div id="frmSolicitarNuevaPuerta" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" style="margin-top: -8px">Solicitar Nueva Puerta</h4>
                        </div>
                        <div class="modal-body">
                            <uc1:ucNuevaPuerta runat="server" ID="NuevaPuerta" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <!-- /.modal -->


    <script type="text/javascript">
        var vconfirm = false;

        $(document).ready(function () {

            init_JS_updBuscarUbicacion();
            init_JS_updPuertas();

        });

        function init_JS_updPuertas() {

            $(".grid-txtNroPuerta").autoNumeric("init", { aSep: "", mDec: 0, vMax: '99999' });
        }

        function showfrmError() {
            $("#pnlBotonesGuardar").show();
            $("#frmError").modal("show");
            return false;

        }

        function init_JS_updBuscarUbicacion() {

            $("#<%: txtNroPartida.ClientID %>").autoNumeric("init", { aSep: "", mDec: 0, vMax: '9999999' });
            $("#<%: txtSeccion.ClientID %>").autoNumeric("init", { aSep: "", mDec: 0, vMax: '999' });
            $("#<%: txtNroPuerta.ClientID %>").autoNumeric("init", { aSep: "", mDec: 0, vMax: '99999' });

            $("#tabs").tabs({
                activate: function (event, ui) {

                    var active = $("#tabs").tabs("option", "active");
                    $("#<%: hid_tabselected.ClientID %>").val(active);
                }
            });

            $("#<%: ddlCalles.ClientID %>").select2({
                placeholder: "Seleccionar",
                allowClear: true,
                minimumInputLength: 3
            });
            $("#<%: btnEditarUbicacion.ClientID %>").css("Display", "none");




        }

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


        function showfrmConfirmarNoPH() {

            vconfirm = false;
            $("#frmConfirmarNoPH").modal({
                "show": true,
                "backdrop": "static",
                "keyboard": false
            });

            return false;
        }

        function hidefrmConfirmarNoPH(value) {
            debugger;
            var ret = true;
            vconfirm = value;
            $("#frmConfirmarNoPH").modal('hide');

            if (value) {
                validarIngresarUbicacion();
                eval($("#<%: btnIngresarUbicacion.ClientID %>").prop("href"));
                ret = false;
            }

            if (vconfirm) {
                $("#<%: btnIngresarUbicacion.ClientID %>").hide();
                $("#<%: btnNuevaBusqueda.ClientID %>").hide();
                $("#<%: btnCerrar5.ClientID %>").hide();
                vconfirm = false;
            }
            else {
                $("#<%: btnIngresarUbicacion.ClientID %>").show();
                $("#<%: btnNuevaBusqueda.ClientID %>").show();
                $("#<%: btnCerrar5.ClientID %>").show();
            }

            return ret;
        }
        function hidefrmConfirmarNoPHEdit(value) {

            var ret = true;
            vconfirm = value;
            $("#frmConfirmarNoPH").modal('hide');

            if (value) {
                validarIngresarUbicacion();
                eval($("#<%: btnEditarUbicacion.ClientID %>").prop("href"));
                ret = false;
            }

            if (vconfirm) {
                $("#<%: btnEditarUbicacion.ClientID %>").hide();
                $("#<%: btnNuevaBusqueda.ClientID %>").hide();
                $("#<%: btnCerrar5.ClientID %>").hide();
                vconfirm = false;
            }
            else {
                $("#<%: btnEditarUbicacion.ClientID %>").show();
                $("#<%: btnNuevaBusqueda.ClientID %>").show();
                $("#<%: btnCerrar5.ClientID %>").show();
            }

            return ret;
        }

        function validarIngresarUbicacion() {

            var ret = true;
            var puerta = false;
            var contPuerta = 0;

            $('#<%=pnlValidacionIngresoUbicacion.ClientID%>').hide();

            $(".chkPuertaClass > input[type='checkbox']").each(function () {
                debugger;
                contPuerta = contPuerta + 1;
                if ($(this).is(":checked"))
                    puerta = true;
            });


            if (!puerta && contPuerta > 0) {
                $('#<%=pnlValidacionIngresoUbicacion.ClientID%>').html("<ul id='ulErr'><li>Debe tildar al menos 1 puerta.</li></ul>");
                $('#<%=pnlValidacionIngresoUbicacion.ClientID%>').show();
                ret = false;
            }
            else {
                // Si no confirmo se valida, si ya confirmo no se valida.
                if (!vconfirm) {
                    $(".BuscarUbicacion-pnlPartidasHorizontales").each(function (index, item) {

                        // Si el panel de partidas horizontales está visible
                        if ($(item).css("display") == "block") {
                            //Si la cantidad de partidas horizontales seleccionadas es (0 cero).
                            if ($(item).find("input:checked").length == 0) {
                                ret = false;
                                showfrmConfirmarNoPH();
                                ret = false;
                            }
                           
                        }
                    });
                }
            }


            if (ret)
            {
                $("#<%: btnIngresarUbicacion.ClientID %>").hide();
                $("#<%: btnEditarUbicacion.ClientID %>").hide();
                $("#<%: btnNuevaBusqueda.ClientID %>").hide();
                $("#<%: btnCerrar5.ClientID %>").hide();

            }
            return ret;
        }
        
        function showfrmSolicitarNuevaPuerta() {
            
            $("#frmSolicitarNuevaPuerta").modal("show");
            return false;
        }

        function hidefrmSolicitarNuevaPuerta() {
            $("#frmSolicitarNuevaPuerta").modal("hide");
            return false;
        }

        function deshabilitarPorInhibiciondeUbicacion() {
            $("#MainContent_BuscarUbicacion_gridubicacion_lblSeleccionarPuerta_0").hide();
            $("#MainContent_BuscarUbicacion_gridubicacion_lblPartidaInhibida_0").show();
        }

        function habilitarPorNoInhibiciondeUbicacion() {
            $("#MainContent_BuscarUbicacion_gridubicacion_lblSeleccionarPuerta_0").show();
            $("#MainContent_BuscarUbicacion_gridubicacion_lblPartidaInhibida_0").hide();
        }


        function hidefrmConfirmarNoPHFin() {
            $("#frmConfirmarNoPH").removeClass("modal-backdrop fade in");
        }

        function showModalAPH() {
            $("#MainContent_BuscarUbicacion_pnlGridResultados").hide();
            $("#MainContent_BuscarUbicacion_ZonaPlaneamiento_pnlZonaPlaneamiento").hide();
            $("#MainContent_BuscarUbicacion_ZonaPlaneamiento_pnlAPH").show();
            return false;
        }

        function hideModalAPH() {
            $("#MainContent_BuscarUbicacion_pnlGridResultados").show();
            $("#MainContent_BuscarUbicacion_ZonaPlaneamiento_pnlZonaPlaneamiento").show();
            $("#MainContent_BuscarUbicacion_ZonaPlaneamiento_pnlAPH").hide();
            return false;
        }

        function hideZonaPlaneamiento() {
            $("#MainContent_BuscarUbicacion_ZonaPlaneamiento_pnlZonaPlaneamiento").hide();
        }
    </script>

</div>


