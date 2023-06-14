<%@ Page Title="Datos del local" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DatosLocal.aspx.cs" Inherits="AnexoProfesionales.DatosLocal" %>

<%@ Register Src="~/Tramites/Habilitacion/Controls/Titulo.ascx" TagPrefix="uc1" TagName="Titulo" %>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <%: Scripts.Render("~/bundles/autoNumeric") %>

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
        <asp:UpdatePanel ID="updCargarDatos" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnCargarDatos" runat="server" Style="display: none" OnClick="btnCargarDatos_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <uc1:Titulo runat="server" ID="Titulo" />
        <hr />

        <asp:HiddenField ID="hid_DecimalSeparator" runat="server" />
        <asp:HiddenField ID="hid_return_url" runat="server" />
        <asp:HiddenField ID="hid_id_encomienda" runat="server" />

        <p style="margin: auto; padding: 10px; line-height: 20px">
            En este paso podr&aacute; ver los mapas en donde se encuentra la ubicaci&oacute;n y especificar las superficies del lugar.
        </p>

        <%--Box de Mapas--%>
        <div id="box_datoslocal" class="box-panel" style="background-color: #ffffff">

            <div style="margin: 20px; margin-top: -5px">
                <div style="color: #377bb5">
                    <h4><i class="imoon imoon-office" style="margin-right: 10px"></i>Datos de la Ubicacion</h4>
                    <hr />
                </div>
            </div>
            <%-- contenido de Mapas --%>
            <asp:UpdatePanel ID="updDatosLocal" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <div class="row">
                        <div class="form-horizontal">
                            <div class="col-sm-6">
                                <div class="col-sm-12 text-center">
                                    <strong>Entre calles</strong>
                                </div>

                                <div class="text-center ">
                                    <asp:Image ID="imgMapa1" runat="server" CssClass="img-thumbnail" onError="noExisteFotoParcela(this);" />
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="col-sm-12 text-center">
                                    <strong>Croquis</strong>
                                </div>

                                <div class="text-center ">
                                    <asp:Image ID="imgMapa2" runat="server" CssClass="img-thumbnail" onError="noExisteFotoParcela(this);" />
                                </div>

                            </div>

                            <div class="col-sm-12">
                                <br />
                            </div>

                            <div class="col-sm-6">

                                <div class="col-sm-12 text-center pbottom15">
                                    <asp:Label ID="lblTituloSuperficieHabilitacion" runat="server" Font-Bold="true" Text="Dimensiones de la Habilitación"></asp:Label>
                                </div>
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <label class="col-sm-6 control-label">Superficie cubierta:</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtSuperficieCubierta" runat="server" Text="0,00" Width="100px" CssClass="form-control text-right"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-6 control-label">Superficie descubierta:</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtSuperficieDescubierta" runat="server" Text="0,00" Width="100px" CssClass="form-control text-right"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-6 control-label">Superficie total:</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtSuperficieTotal" runat="server" Text="0,00" Width="100px" CssClass="form-control text-right" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-6 control-label">Dimensión del Frente:</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtDimensionFrente" runat="server" Text="0,00" Width="100px" CssClass="form-control text-right"></asp:TextBox>
                                            <asp:CompareValidator ID="cv2_txtDimensionFrente" runat="server"
                                                ControlToValidate="txtDimensionFrente"
                                                Operator="LessThanEqual"
                                                Type="Double" ControlToCompare="txtFrente"
                                                ErrorMessage="La Dimensión del Frente debe ser igual o inferior al Frente de la Parcela." CssClass="alert alert-small alert-danger mbottom0 mtop5 col-sm-12" Display="Dynamic"
                                                ValidationGroup="Continuar">
                                            </asp:CompareValidator>
                                            <div id="ReqDF" class="alert alert-danger" style="display: none">
                                                Debe completar la Dimensión del Frente.
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-12 pleft20 pright20">

                                            <div id="ReqSuperficies" class="alert alert-danger" style="display: none">
                                                Los campos de superficie son obligatorios (al menos uno).
                                            </div>

                                        </div>
                                    </div>

                                    <asp:Panel ID="pnlAvisomodifSuperficie" runat="server" CssClass="alert alert-info mtop5" Visible="false">
                                        Para poder modificar la superficie, no debe tener ningún rubro cargado. Debe quitar los rubros si desea modificarla.
                                    </asp:Panel>

                                </div>

                            </div>
                            <div class="col-sm-6">
                                <div class="form-horizontal">
                                    <div class="col-sm-12 text-center pbottom15">
                                        <strong>Dimensiones de la Parcela</strong>
                                    </div>
                                    <div class="form-group" style="text-align: center">
                                        <label class="control-label col-sm-6">Frente:</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtFrente" runat="server" MaxLength="10" Width="100px" CssClass="form-control text-right"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv_txtFrente" runat="server"
                                                ControlToValidate="txtFrente"
                                                ErrorMessage="El campo es obligatorio." CssClass="alert alert-small alert-danger mbottom0 mtop5 col-sm-6" Display="Dynamic"
                                                ValidationGroup="Continuar">
                                            </asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cv_txtFrente" runat="server"
                                                ControlToValidate="txtFrente"
                                                Operator="GreaterThan"
                                                Type="Double" ValueToCompare="0"
                                                ErrorMessage="La cantidad debe ser mayor a cero." CssClass="alert alert-small alert-danger mbottom0 mtop5 col-sm-6" Display="Dynamic"
                                                ValidationGroup="Continuar">
                                            </asp:CompareValidator>

                                        </div>
                                    </div>
                                    <div class="form-group" style="text-align: center">
                                        <label class="control-label col-sm-6">Fondo:</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtFondo" runat="server" MaxLength="10" Width="100px" CssClass="form-control text-right"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                ControlToValidate="txtFondo"
                                                ErrorMessage="El campo es obligatorio." CssClass="alert alert-small alert-danger mbottom0 mtop5 col-sm-6" Display="Dynamic"
                                                ValidationGroup="Continuar">
                                            </asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cv_txtFondo" runat="server"
                                                ControlToValidate="txtFondo"
                                                Operator="GreaterThan"
                                                Type="Double" ValueToCompare="0"
                                                ErrorMessage="La cantidad debe ser mayor a cero." CssClass="alert alert-small alert-danger mbottom0 mtop5 col-sm-6" Display="Dynamic"
                                                ValidationGroup="Continuar">
                                            </asp:CompareValidator>
                                        </div>

                                    </div>
                                    <div class="form-group" style="text-align: center">
                                        <label class="control-label col-sm-6">Lateral Izquierdo:</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtLatIzq" runat="server" MaxLength="10" Width="100px" CssClass="form-control text-right"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv_txtLatIzq" runat="server"
                                                ControlToValidate="txtLatIzq"
                                                ErrorMessage="El campo es obligatorio." CssClass="alert alert-small alert-danger mbottom0 mtop5 col-sm-6" Display="Dynamic"
                                                ValidationGroup="Continuar">
                                            </asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cv_txtLatIzq" runat="server"
                                                ControlToValidate="txtLatIzq"
                                                Operator="GreaterThan"
                                                Type="Double" ValueToCompare="0"
                                                ErrorMessage="La cantidad debe ser mayor a cero." CssClass="alert alert-small alert-danger mbottom0 mtop5 col-sm-6" Display="Dynamic"
                                                ValidationGroup="Continuar">
                                            </asp:CompareValidator>

                                        </div>

                                    </div>
                                    <div class="form-group" style="text-align: center">

                                        <label class="control-label col-sm-6">Lateral Derecho:</label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtLatDer" runat="server" MaxLength="10" Width="100px" CssClass="form-control text-right"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv_txtLatDer" runat="server"
                                                ControlToValidate="txtLatDer"
                                                ErrorMessage="El campo es obligatorio." CssClass="alert alert-small alert-danger mbottom0 mtop5 col-sm-6" Display="Dynamic"
                                                ValidationGroup="Continuar">
                                            </asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cv_txtLatDer" runat="server"
                                                ControlToValidate="txtLatDer"
                                                Operator="GreaterThan"
                                                Type="Double" ValueToCompare="0"
                                                ErrorMessage="La cantidad debe ser mayor a cero." CssClass="alert alert-small alert-danger mbottom0 mtop5 col-sm-6" Display="Dynamic"
                                                ValidationGroup="Continuar">
                                            </asp:CompareValidator>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>

        <%--Ampliacion de Superficie--%>
        <asp:UpdatePanel ID="updAmpliacionSuperficie" runat="server">
            <ContentTemplate>

                <asp:Panel ID="pnlAmpliacionSuperficie" runat="server" BackColor="White" CssClass="ptop10">

                    <div style="margin: 20px; margin-top: -5px">
                        <div style="color: #377bb5">
                            <h4>
                                <i class="imoon imoon-expand" style="margin-right: 10px"></i>¿Ud. desea tramitar una ampliación de superficie?
                                <div class="radio-inline mleft20">
                                    <label class="color-black">
                                        <asp:RadioButton ID="optAmpliacionSuperficie_SI" runat="server" GroupName="Ampliacion" onclick="return stateControlsSuperficieAmp(true);" />S&iacute;
                                    </label>
                                    <label class="pleft30 color-black">
                                        <asp:RadioButton ID="optAmpliacionSuperficie_NO" runat="server" GroupName="Ampliacion" onclick="return stateControlsSuperficieAmp(false);" />No
                                    </label>

                                </div>
                            </h4>
                            <hr />
                        </div>
                    </div>


                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-sm-6 text-center pbottom15">
                                <strong>Superficie Total</strong>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">Superficie cubierta:</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="txtSuperficieCubiertaAmp" runat="server" Width="100px" CssClass="form-control text-right" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-3 control-label">Superficie descubierta:</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="txtSuperficieDescubiertaAmp" runat="server" Width="100px" CssClass="form-control text-right" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-3 control-label">Superficie total:</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="txtSuperficieTotalAmp" runat="server" Width="100px" CssClass="form-control text-right" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-sm-12 pleft20 pright20">

                                <div id="ReqSuperficiesAmp" class="alert alert-danger" style="display: none">
                                    Los campos de superficie son obligatorios (al menos uno).
                                </div>

                            </div>
                        </div>


                    </div>

                </asp:Panel>

            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Panel ID="pnlCarracteristicas" runat="server" BackColor="White" CssClass="ptop10">

            <div style="margin: 20px; margin-top: -5px">
                <div style="color: #377bb5">
                    <h4><i class="imoon imoon-office" style="margin-right: 10px"></i>Caracteristicas del Local</h4>
                    <hr />
                </div>
            </div>
            <asp:UpdatePanel ID="updCaracteristicas" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <%--Características del local--%>
                    <div class="pleft20">

                        <div class="row no-gutter">

                            <div class="col-sm-5">
                                <div class="titulo-5">
                                    <b>Caracter&iacute;sticas Generales</b>
                                </div>
                                <div>
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" CssClass="control-label col-sm-7">Posee lugar de carga y descarga:</asp:Label>
                                        <div class="col-sm-3">
                                            <asp:RadioButton ID="opt1_si" runat="server" GroupName="LugarCargaDescarga" Text="Sí" />
                                            <label></label>
                                            <asp:RadioButton ID="opt1_no" runat="server" GroupName="LugarCargaDescarga" Text="No" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" CssClass="control-label col-sm-7">Posee estacionamiento:</asp:Label>
                                        <div class="col-sm-3">
                                            <asp:RadioButton ID="opt2_si" runat="server" GroupName="Estacionamiento" Text="Sí" />
                                            <label></label>
                                            <asp:RadioButton ID="opt2_no" runat="server" GroupName="Estacionamiento" Text="No" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="Label12" runat="server" CssClass="control-label col-sm-7">Posee estacionamiento de Bicicleta:</asp:Label>
                                        <div class="col-sm-3" style="width: 100px; margin-left: 10px; margin-top: 10px">
                                            <asp:RadioButton ID="opt5_si" runat="server" GroupName="EstacionamientoBicicleta" Text="Sí" />
                                            <label></label>
                                            <asp:RadioButton ID="opt5_no" runat="server" GroupName="EstacionamientoBicicleta" Text="No" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" CssClass="control-label col-sm-7">Red de tránsito pesado:</asp:Label>
                                        <div class="col-sm-3">
                                            <asp:RadioButton ID="opt3_si" runat="server" GroupName="RedTansitoPesado" Text="Sí" />
                                            <label></label>
                                            <asp:RadioButton ID="opt3_no" runat="server" GroupName="RedTansitoPesado" Text="No" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="Label4" runat="server" CssClass="control-label col-sm-7">Sobre Avenida:</asp:Label>
                                        <div class="col-sm-3">
                                            <asp:RadioButton ID="opt4_si" runat="server" GroupName="SobreAvenida" Text="Sí" />
                                            <label></label>
                                            <asp:RadioButton ID="opt4_no" runat="server" GroupName="SobreAvenida" Text="No" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="Label5" runat="server" CssClass="control-label col-sm-7"> Cantidad de operarios:</asp:Label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtCantOperarios" CssClass="form-control" runat="server" Width="100px" MaxLength="5"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="rfv_txtCantOperarios" runat="server"
                                                ControlToValidate="txtCantOperarios"
                                                ErrorMessage="El campo es obligatorio." CssClass="alert alert-small alert-danger mbottom0 mtop5" Display="Dynamic"
                                                ValidationGroup="Continuar">
                                            </asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="cv_txtCantOperarios" runat="server"
                                                ControlToValidate="txtCantOperarios"
                                                Operator="GreaterThan"
                                                Type="Integer" ValueToCompare="0"
                                                ErrorMessage="La cantidad debe ser mayor a cero." CssClass="alert alert-small alert-danger mbottom0 mtop5" Display="Dynamic"
                                                ValidationGroup="Continuar">
                                            </asp:CompareValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--Servicios Sanitarios--%>
                            <div class="col-sm-7">
                                <div class="titulo-5">
                                    <b>Servicios sanitarios</b>
                                </div>
                                <div>
                                    <div class="form-group">

                                        <asp:Label ID="Label6" runat="server" CssClass="control-label col-sm-6"> Los mismos se encuentran:</asp:Label>
                                        <div class="col-sm-6">
                                            <asp:RadioButton ID="opt5_dentro" Checked="true" runat="server" GroupName="Sanitarios" onclick="objVisibility('tblDistanciaSanitarios_dl','hide'); ocultarReq();" Text="Dentro del Local" />
                                            <asp:RadioButton ID="opt5_fuera" runat="server" GroupName="Sanitarios" onclick="objVisibility('tblDistanciaSanitarios_dl','show');" Text="Fuera del Local" />
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div id="tblDistanciaSanitarios_dl" style="display: none">
                                            <asp:Label ID="lblDistanciaSanitarioa_dl" runat="server" CssClass="control-label col-sm-6">¿a que distancia? (metros):</asp:Label>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtDistanciaSanitarios_dl" CssClass="form-control" runat="server" MaxLength="4" onfocus="this.select();" Width="70px"></asp:TextBox>
                                                <div id="ReqDistanciaSanitarios" class="alert alert-small alert-danger" style="display: none">
                                                    Al seleccionar Fuera del local debe indicar la distancia.
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="Label7" runat="server" CssClass="control-label col-sm-6">Cantidad de artefactos sanitarios:</asp:Label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtCantidadArtefactosSanitarios" CssClass="form-control" runat="server" MaxLength="4" Width="70px" onchange="javascript: HabilitarSuperficiaSanitarios();"></asp:TextBox>
                                            <div id="ReqCantidadArtefactosSanitarios" class="alert alert-small alert-danger" style="display: none">
                                                Al seleccionar que cumple con la Ley 962 debe ingresar la cantidad de artefactos sanitarios.
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">

                                        <asp:Label ID="lblAccesibilidad" runat="server" CssClass="control-label col-sm-6">Accesibilidad del local a habilitar:</asp:Label>

                                        <div class="col-sm-6">

                                            <asp:RadioButton ID="rbtnCumpleLey962" runat="server" GroupName="Accesibilidad" Text="Cumple con la accesibilidad según Código de Edificación" onchange="javascript: HabilitarSuperficiaSanitarios();" /></u>
                                                    
                                            <asp:RadioButton ID="rbtnEximidoLey962" runat="server" GroupName="Accesibilidad" Text="Se exime de la accesibilidad conforme Resolución 345/AGC/2021" />
                                            <div id="divLey962" class="alert alert-small alert-danger" style="display: none">
                                                Debe seleccionar una opción
                                            </div>


                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblPoseeSalubridad" runat="server" CssClass="control-label col-sm-6">Posee servicio de salubridad especial?:</asp:Label>
                                        <div class="col-sm-6">
                                            <asp:CheckBox ID="chk" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label8" runat="server" CssClass="control-label col-sm-6">Superficie de Sanitarios:</asp:Label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtSuperficieSanitarios" Enabled="false" CssClass="form-control" runat="server" MaxLength="8" Width="70px"></asp:TextBox>
                                        </div>
                                        <div id="ValSuperficieSanitarios" class="alert alert-small alert-danger" style="display: none">
                                            La Superficie de sanitarios no puede ser mayor a la Superficie Total.
                                        </div>
                                        <div id="ValSuperficieSanitariosLey962" class="alert alert-small alert-danger" style="display: none">
                                            La Superficie de sanitarios no puede ser 0.
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label9" runat="server" CssClass="control-label col-sm-6">Superficie Salón de Ventas:</asp:Label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtLocalVenta" runat="server" CssClass="form-control" MaxLength="10" Width="70px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="ReqtxtLocalVenta" runat="server" ControlToValidate="txtLocalVenta"
                                                ErrorMessage="Debe ingresar la Superficie del Salón de Ventas." CssClass="alert alert-small alert-danger mbottom0 mtop5"
                                                Display="Dynamic" ValidationGroup="Continuar"></asp:RequiredFieldValidator>

                                            <asp:CompareValidator ID="cvtxtLocalVenta" runat="server"
                                                ControlToValidate="txtLocalVenta"
                                                Operator="LessThanEqual"
                                                ControlToCompare="txtSuperficieTotal"
                                                Type="Double"
                                                ErrorMessage="La cantidad debe ser menor o igual a la superficie total."
                                                CssClass="alert alert-small alert-danger mbottom0 mtop5"
                                                Display="Dynamic"
                                                ValidationGroup="Continuar">
                                            </asp:CompareValidator>
                                        </div>
                                    </div>
                                    <%-->div class="form-group">
                                        <asp:Label ID="Label12" runat="server" CssClass="control-label col-sm-6">Superficie Semicubierta:</asp:Label>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtSupSemiCubierta" Enabled="false" CssClass="form-control" runat="server" MaxLength="8" Width="70px"></asp:TextBox>                                            
                                        </div> 
                                    </--%>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <%--Materiales Empleados en:--%>
                            <div class="titulo-5" style="margin-left: 20px">
                                <b>Materiales expresados en...</b>
                            </div>
                            <table style="margin-top: 10px; margin-left: 40px">
                                <tr>
                                    <td class="celda">
                                        <label>
                                            Pisos:</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPisos" CssClass="form-control" runat="server" TextMode="MultiLine" MaxLength="200" Height="63px"
                                            Width="750px" Style="margin-top: 5px"></asp:TextBox>
                                        <div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPisos"
                                                ErrorMessage="El campo es obligatorio." CssClass="alert alert-small alert-danger mbottom0 mtop5" Display="Dynamic"
                                                ValidationGroup="Continuar"></asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="celda">
                                        <label>
                                            Paredes:</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtParedes" CssClass="form-control" runat="server" TextMode="MultiLine" MaxLength="200"
                                            Height="63px" Width="750px" Style="margin-top: 5px"></asp:TextBox>
                                        <div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtParedes"
                                                ErrorMessage="El campo es obligatorio." CssClass="alert alert-small alert-danger mbottom0 mtop5" Display="Dynamic"
                                                ValidationGroup="Continuar"></asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="celda">
                                        <label>
                                            Techos:</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTechos" CssClass="form-control" runat="server" TextMode="MultiLine" MaxLength="200" Height="63px"
                                            Width="750px" Style="margin-top: 5px"></asp:TextBox>
                                        <div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTechos"
                                                ErrorMessage="El campo es obligatorio." CssClass="alert alert-small alert-danger mbottom0 mtop5" Display="Dynamic"
                                                ValidationGroup="Continuar"></asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="celda">
                                        <label>
                                            Revestimientos:</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRevestimientos" CssClass="form-control" runat="server" TextMode="MultiLine" MaxLength="200"
                                            Height="63px" Width="750px" Style="margin-top: 5px"></asp:TextBox>
                                        <div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRevestimientos"
                                                ErrorMessage="El campo es obligatorio." CssClass="alert alert-small alert-danger mbottom0 mtop5" Display="Dynamic"
                                                ValidationGroup="Continuar"></asp:RequiredFieldValidator>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="row" style="margin-bottom: 15px; margin-left: -40px; margin-top: 20px;">
                            <div class="form-group">
                                <asp:Label ID="Label11" runat="server" CssClass="control-label col-sm-6" Font-Bold="true">Posee mas de 350 asistentes?:</asp:Label>
                                <div class="col-sm-3" style="width: 100px;">
                                    <asp:RadioButton ID="asistentes_SI" runat="server" GroupName="Asistentes" Text="Sí" AutoPostBack="true" />
                                    <asp:RadioButton ID="asistentes_NO" runat="server" Checked="true" GroupName="Asistentes" Text="No" AutoPostBack="true" />
                                    <div id="ValAsistentes" class="alert alert-small alert-danger" style="display: none">
                                        Debe seleccionar una opción
                                    </div>
                                </div>
                            </div>
                            <%--SobreCarga--%>
                            <div class="form-group">
                                <asp:Label ID="Label10" runat="server" CssClass="control-label col-sm-6" Font-Bold="true">Posee planta/s por debajo de la planta/s a habilitar?:</asp:Label>
                                <div class="col-sm-3" style="width: 100px;">
                                    <asp:RadioButton ID="optsCertificadoSobrecarga_SI" runat="server" GroupName="Sobrecarga" Text="Sí" AutoPostBack="true"
                                        OnCheckedChanged="optsCertificadoSobrecarga_SI_CheckedChanged" />
                                    <asp:RadioButton ID="optsCertificadoSobrecarga_NO" runat="server" Checked="true" GroupName="Sobrecarga" Text="No" AutoPostBack="true"
                                        OnCheckedChanged="optsCertificadoSobrecarga_NO_CheckedChanged" />
                                </div>
                            </div>

                            <div class="form-group">
                                <div runat="server" id="divcheckDeclaracion" class="col-md-offset-1 col-md-10" visible="true">
                                    <asp:CheckBox ID="checkBDJCertificado" runat="server" class="checkbox" Checked="false"
                                        AutoPostBack="true"
                                        Text="Declaro que todas las estructuras 
                                                          sometidas a cargas o esfuerzos de cualquier tipo,
                                                          incluidos pasos, escaleras, barandas, etc., soportan las sobrecargas previstas
                                                          para los destinos declarados conforme la reglamentación vigente." />
                                    <div id="ReqCertificadoDeSobrecarga" class="alert alert-small alert-danger" style="display: none">
                                        Si el local posee planta/s por debajo de la planta/s a habilitar, debe aceptar la declaración. Sino no podra avanzar con el tramite.
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="Label13" runat="server" CssClass="control-label col-sm-6" Font-Bold="true">Posee productos inflamables?:</asp:Label>
                                <div class="col-sm-3" style="width: 100px;">
                                    <asp:RadioButton ID="productosInflamables_SI" runat="server" GroupName="productosInflamables" Text="Sí" AutoPostBack="true" />
                                    <asp:RadioButton ID="productosInflamables_NO" runat="server" Checked="true" GroupName="productosInflamables" Text="No" AutoPostBack="true" />
                                    <div id="ValProductosInflamables" class="alert alert-small alert-danger" style="display: none">
                                        Debe seleccionar una opción
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <br />
        <br />

        <%--Botones de Guardado--%>
        <asp:UpdatePanel ID="updBotonesGuardar" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <div class="form-inline text-right mtop20">

                    <div id="pnlBotonesGuardar" class="form-group">
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary" OnClick="btnContinuar_Click"
                            CausesValidation="true" ValidationGroup="Continuar" OnClientClick="return validarGuardar();">
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
                    <h4 class="modal-title">Error</h4>
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


    <script type="text/javascript">

        var vSeparadorDecimal;

        $(document).ready(function () {
            $("#page_content").hide();
            $("#Loading").show();
            $("#<%: btnCargarDatos.ClientID %>").click();
        });

        function HabilitarSuperficiaSanitarios() {
            var value1 = $("#<%: txtCantidadArtefactosSanitarios.ClientID %>").val();
            if (value1 > 0 || $("#<%: rbtnCumpleLey962.ClientID %>").is(":checked"))
                document.getElementById('<%= txtSuperficieSanitarios.ClientID %>').disabled = false;
            else {
                document.getElementById('<%= txtSuperficieSanitarios.ClientID %>').disabled = true;
                document.getElementById('<%= txtSuperficieSanitarios.ClientID %>').value = "0";
            }
            return false;
        }

        function finalizarCarga() {
            $("#Loading").hide();
            $("#page_content").show();
            return false;
        }

        function init_Js_updAmpliacionSuperficie() {

            eval("$('#<%: txtSuperficieCubiertaAmp.ClientID %>').autoNumeric({ aSep: '', aDec: '" + vSeparadorDecimal + "', mDec: '2',vMax: '999999.99'})");
            eval("$('#<%: txtSuperficieDescubiertaAmp.ClientID %>').autoNumeric({ aSep: '', aDec: '" + vSeparadorDecimal + "', mDec: '2',vMax: '999999.99'})");

            $('#<%: txtSuperficieCubiertaAmp.ClientID %>').on("keyup", function () {
                $("#ReqSuperficiesAmp").hide();
                TotalizarSuperficieAmp();
            });

            $('#<%: txtSuperficieDescubiertaAmp.ClientID %>').on("keyup", function () {
                $("#DivCertificadoDeSobrecarga").hide();
                TotalizarSuperficieAmp();
            });

            TotalizarSuperficieAmp();

            return false;
        }

        function init_JS_updDatosLocal() {

            vSeparadorDecimal = $("#<%: hid_DecimalSeparator.ClientID %>").attr("value");
            eval("$('#<%: txtSuperficieCubierta.ClientID %>').autoNumeric({ aSep: '', aDec: '" + vSeparadorDecimal + "', mDec: '2',vMax: '999999.99'})");
            eval("$('#<%: txtSuperficieDescubierta.ClientID %>').autoNumeric({ aSep: '', aDec: '" + vSeparadorDecimal + "', mDec: '2',vMax: '999999.99'})");
            eval("$('#<%: txtFrente.ClientID %>').autoNumeric({ aSep: '', aDec: '" + vSeparadorDecimal + "', mDec: '2',vMax: '999999.99'})");
            eval("$('#<%: txtDimensionFrente.ClientID %>').autoNumeric({ aSep: '', aDec: '" + vSeparadorDecimal + "', mDec: '2',vMax: '999999.99'})");
            eval("$('#<%: txtFondo.ClientID %>').autoNumeric({ aSep: '', aDec: '" + vSeparadorDecimal + "', mDec: '2',vMax: '999999.99'})");
            eval("$('#<%: txtLatDer.ClientID %>').autoNumeric({ aSep: '', aDec: '" + vSeparadorDecimal + "', mDec: '2',vMax: '999999.99'})");
            eval("$('#<%: txtLatIzq.ClientID %>').autoNumeric({ aSep: '', aDec: '" + vSeparadorDecimal + "', mDec: '2',vMax: '999999.99'})");
            eval("$('#<%: txtCantidadArtefactosSanitarios.ClientID %>').autoNumeric({ aSep: '', aDec: '" + vSeparadorDecimal + "', mDec: '0',vMax: '999999'})");
            eval("$('#<%: txtSuperficieSanitarios.ClientID %>').autoNumeric({ aSep: '', aDec: '" + vSeparadorDecimal + "', mDec: '2',vMax: '999999.99'})");
            eval("$('#<%: txtCantOperarios.ClientID %>').autoNumeric({ aSep: '', aDec: '" + vSeparadorDecimal + "', mDec: '0',vMax: '999999'})");
            eval("$('#<%: txtDistanciaSanitarios_dl.ClientID %>').autoNumeric({ aSep: '', aDec: '" + vSeparadorDecimal + "', mDec: '0',vMax: '999999'})");
            eval("$('#<%: txtLocalVenta.ClientID %>').autoNumeric({ aSep: '', aDec: '" + vSeparadorDecimal + "', mDec: '2',vMax: '999999.99'})");


            $('#<%: txtSuperficieCubierta.ClientID %>').on("keyup", function () {
                $("#ReqSuperficies").hide();
                TotalizarSuperficie();
            });

            $('#<%: txtSuperficieDescubierta.ClientID %>').on("keyup", function () {
                $("#ReqSuperficies").hide();
                TotalizarSuperficie();
            });

            if ($('#<%: checkBDJCertificado.ClientID %>').attr("checked") == "checked")
                $("#ReqCertificadoDeSobrecarga").hide();
            else
                $("#ReqCertificadoDeSobrecarga").show();

            $('#<%: txtDistanciaSanitarios_dl.ClientID %>').on("keyup", function () {
                $("#ReqDistanciaSanitarios").hide();
            });


            $('#<%: txtSuperficieSanitarios.ClientID %>').on("keyup", function () {
                $("#ValSuperficieSanitarios").hide();
            });

            $('#<%: txtSuperficieSanitarios.ClientID %>').on("keyup", function () {
                $("#ValSuperficieSanitariosLey962").hide();
            });


            if ($('#<%: opt5_dentro.ClientID %>').checked)
                $("#tblDistanciaSanitarios_dl").hide;
            else
                $("#tblDistanciaSanitarios_dl").show;

        }

        function showfrmError() {
            $("#pnlBotonesGuardar").show();
            $("#frmError").modal("show");
            return false;
        }

        function TotalizarSuperficie() {

            var value1 = $("#<%: txtSuperficieCubierta.ClientID %>").val();
            var value2 = $("#<%: txtSuperficieDescubierta.ClientID %>").val();

            val1 = stringToFloat(value1, vSeparadorDecimal);
            val2 = stringToFloat(value2, vSeparadorDecimal);
            var total = val1 + val2;
            $("#<%: txtSuperficieTotal.ClientID %>").val(total.toFixed(2).toString().replace(".", vSeparadorDecimal));

            return false;
        }


        function TotalizarSuperficieAmp() {

            var value1 = $("#<%: txtSuperficieCubiertaAmp.ClientID %>").val();
            var value2 = $("#<%: txtSuperficieDescubiertaAmp.ClientID %>").val();

            val1 = stringToFloat(value1, vSeparadorDecimal);
            val2 = stringToFloat(value2, vSeparadorDecimal);
            var total = val1 + val2;
            $("#<%: txtSuperficieTotalAmp.ClientID %>").val(total.toFixed(2).toString().replace(".", vSeparadorDecimal));

            return false;
        }

        function toolTips() {
            $("[data-toggle='tooltip']").tooltip();
            return false;

        }
        function ocultarBotonesGuardado() {

            $("#pnlBotonesGuardar").hide();

            return true;
        }

        function ocultarReq() {
            $("#ReqDistanciaSanitarios").hide();
            $("#ValSuperficieSanitarios").hide();
            $("#ValSuperficieSanitariosLey962").hide();
            return false;
        }

        function validarGuardar() {

            var ret = true;
            $("#ReqDistanciaSanitarios").hide();
            $("#ReqSuperficies").hide();
            $("#ReqSuperficiesAmp").hide();
            $("#ValSuperficieSanitarios").hide();
            $("#divLey962").hide();
            $("#ReqCertificadoDeSobrecarga").hide();
            $("#ValAsistentes").hide();
            $("#ValProductosInflamables").hide();
            $("#ReqCantidadArtefactosSanitarios").hide();
            $("#ReqDF").hide();
            $("#ValSuperficieSanitariosLey962").hide();

            var value1 = $("#<%: txtSuperficieTotal.ClientID %>").val();
            var total = stringToFloat(value1);

            var valorDistancia = $("#<%: txtDistanciaSanitarios_dl.ClientID %>").val();
            var totalDistancia = stringToFloat(valorDistancia);

            var valorSuperficieSanitario = $("#<%: txtSuperficieSanitarios.ClientID %>").val();
            var totalSuperficieSanitario = stringToFloat(valorSuperficieSanitario);

            var valueDF = $("#<%: txtDimensionFrente.ClientID %>").val();
            var totalDF = stringToFloat(valueDF);

            if (total <= 0) {
                $("#ReqSuperficies").show();
                ret = false;
            }

            // Valida si es una ampliación de superficie, que se carguen los valores.
            if ($("#<%: optAmpliacionSuperficie_SI.ClientID %>").prop("checked")) {

                var value1Amp = $("#<%: txtSuperficieTotalAmp.ClientID %>").val();
                var totalAmp = stringToFloat(value1Amp);
                var cantSanitarios = stringToInt($("#<%: txtCantidadArtefactosSanitarios.ClientID %>").val())

                if (totalAmp <= 0) {
                    $("#ReqSuperficiesAmp").css("display", "inline-block");
                    ret = false;
                }
            }

            if ($("#<%: opt5_fuera.ClientID %>").is(":checked") && totalDistancia <= 0) {
                $("#ReqDistanciaSanitarios").show();
                ret = false;
            }


            if ($("#<%: opt5_dentro.ClientID %>").is(":checked") && (totalSuperficieSanitario > total)) {
                $("#ValSuperficieSanitarios").show();
                ret = false;
            }

            if (!$("#<%: asistentes_SI.ClientID %>").is(":checked") && !$("#<%: asistentes_NO.ClientID %>").is(":checked")) {
                $("#ValAsistentes").show();
                ret = false;
            }

            if (!$("#<%: productosInflamables_SI.ClientID %>").is(":checked") && !$("#<%: productosInflamables_NO.ClientID %>").is(":checked")) {
                $("#ValProductosInflamables").show();
                ret = false;
            }

            if (!$("#<%: rbtnCumpleLey962.ClientID %>").is(":checked") && !$("#<%: rbtnEximidoLey962.ClientID %>").is(":checked")) {
                $("#divLey962").show();
            }

            //valida Certificado de Sobrecarga mantis 124897
            if ($("#<%: optsCertificadoSobrecarga_SI.ClientID %>").is(":checked") && !($("#<%: checkBDJCertificado.ClientID %>").is(":checked"))) {
                $("#ReqCertificadoDeSobrecarga").show();
                return false;
            }

            //valida cantidad de sanitarios
            if ($("#<%: rbtnCumpleLey962.ClientID %>").is(":checked") && cantSanitarios <= 0) {
                $("#ReqCantidadArtefactosSanitarios").show();
                ret = false;
            }

            //valida superficie de sanitarios
            if ($("#<%: rbtnCumpleLey962.ClientID %>").is(":checked") && totalSuperficieSanitario <= 0) {
                $("#ValSuperficieSanitariosLey962").show();
                ret = false;
            }

            //valida dimension de frente
            if ($("#<%: rbtnCumpleLey962.ClientID %>").is(":checked") && totalDF <= 0) {
                $("#ReqDF").show();
                ret = false;
            }

            var flag = Page_ClientValidate('Continuar');
            if (flag && ret) {
                ocultarBotonesGuardado();
            }
            return ret;
        }
        function objVisibility(id, accion) {
            //recibe el id del objeto a mostrar u ocultar y la accion 'show'-> mostrar 'hide' o nada->ocultar
            if (accion == 'show')
                $("#" + id).css("display", "block");
            else
                $("#" + id).css("display", "none");
            return false;
        }
        function noExisteFotoParcela(objimg) {

            $(objimg).attr("src", "/Content/img/app/ImageNotFound.png");

            return true;
        }

        function stateControlsSuperficieAmp(enabled) {

            if ($("#<%: optAmpliacionSuperficie_SI.ClientID %>").is(":checked")) {
                $("#<%: txtSuperficieCubiertaAmp.ClientID %>").prop("disabled", false);
                $("#<%: txtSuperficieDescubiertaAmp.ClientID %>").prop("disabled", false);
            }
            else {
                $("#ReqSuperficiesAmp").hide();
                $("#<%: txtSuperficieCubiertaAmp.ClientID %>").prop("disabled", true);
                $("#<%: txtSuperficieDescubiertaAmp.ClientID %>").prop("disabled", true);
                $("#<%: txtSuperficieCubiertaAmp.ClientID %>").val(parseFloat("0").toFixed(2).toString());
                $("#<%: txtSuperficieDescubiertaAmp.ClientID %>").val(parseFloat("0").toFixed(2).toString());
                $("#<%: txtSuperficieTotalAmp.ClientID %>").val(parseFloat("0").toFixed(2).toString());
            }
            return true;
        }
    </script>
</asp:Content>
