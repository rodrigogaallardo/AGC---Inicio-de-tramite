<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatosLocal.ascx.cs" Inherits="AnexoProfesionales.Controls.DatosLocal" %>

<%--Datos Local--%>
<asp:Panel ID="pnlDatosLocal" runat="server">
    <br />
    <%--Grilla de Datos Local--%>
    <div class="row" style="margin-left: 20px">
        <div class="form-horizontal" style="margin-left: -35px">
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
                        <asp:TextBox ID="txtSuperficieCubierta" runat="server" Text="0,00" Enabled="false" Width="100px" CssClass="form-control text-right"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-sm-6 control-label">Superficie descubierta:</label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtSuperficieDescubierta" runat="server" Text="0,00" Enabled="false" Width="100px" CssClass="form-control text-right"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-sm-6 control-label">Superficie total:</label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtSuperficieTotal" runat="server" Text="0,00" Width="100px" Enabled="false" CssClass="form-control text-right"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-sm-6 control-label">Dimensión del Frente:</label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtDimensionFrente" runat="server" Text="0,00" Width="100px" Enabled="false" CssClass="form-control text-right"></asp:TextBox>
                    </div>
                </div>

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
                        <asp:TextBox ID="txtFrente" runat="server" MaxLength="10" Width="100px" Enabled="false" CssClass="form-control text-right"></asp:TextBox>

                    </div>
                </div>
                <div class="form-group" style="text-align: center">
                    <label class="control-label col-sm-6">Fondo:</label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtFondo" runat="server" MaxLength="10" Width="100px" Enabled="false" CssClass="form-control text-right"></asp:TextBox>
                    </div>

                </div>
                <div class="form-group" style="text-align: center">
                    <label class="control-label col-sm-6">Lateral Izquierdo:</label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtLatIzq" runat="server" MaxLength="10" Width="100px" Enabled="false" CssClass="form-control text-right"></asp:TextBox>

                    </div>

                </div>
                <div class="form-group" style="text-align: center">

                    <label class="control-label col-sm-6">Lateral Derecho:</label>
                    <div class="col-sm-6">
                        <asp:TextBox ID="txtLatDer" runat="server" MaxLength="10" Width="100px" Enabled="false" CssClass="form-control text-right"></asp:TextBox>

                    </div>

                </div>
            </div>
        </div>

    </div>

    <asp:Panel ID="pnlAmpliacionSuperficie" runat="server" BackColor="White" CssClass="ptop30" Visible="false">

        <div style="margin: 20px; margin-top: -5px">
            <div style="color: #377bb5">
                <h4>
                    <i class="imoon imoon-expand" style="margin-right: 10px"></i>¿Ud. desea tramitar una ampliación de superficie?
                        <div class="radio-inline mleft20">
                            <label class="color-black">
                                <asp:RadioButton ID="optAmpliacionSuperficie_SI" runat="server" GroupName="Ampliacion" Enabled="false" />S&iacute;
                            </label>
                            <label class="pleft30 color-black">
                                <asp:RadioButton ID="optAmpliacionSuperficie_NO" runat="server" GroupName="Ampliacion" Enabled="false" />No
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

        </div>

    </asp:Panel>


    <br />
    <br />
    <%--Grilla de caracteristicas de local--%>
    <div style="margin-left: 30px">
        <strong>Características del Local</strong>
    </div>

    <div class="form-horizontal" style="margin-left: 40px">
        <div class="form-group">
            <asp:Label ID="Label1" runat="server" CssClass="control-label col-sm-4" Style="margin-left: -97px">Posee lugar de carga y descarga:</asp:Label>
            <div class="col-sm-3" style="width: 100px; margin-left: 10px; margin-top: 10px">
                <asp:RadioButton ID="opt1_si" runat="server" GroupName="LugarCargaDescarga" Text="Sí" Enabled="false" />
                <label></label>
                <asp:RadioButton ID="opt1_no" runat="server" GroupName="LugarCargaDescarga" Text="No" Enabled="false" />
            </div>
        </div>

        <div class="form-group">
            <asp:Label ID="Label2" runat="server" CssClass="control-label col-sm-3">Posee estacionamiento:</asp:Label>
            <div class="col-sm-3" style="width: 100px; margin-left: 10px; margin-top: 10px">
                <asp:RadioButton ID="opt2_si" runat="server" GroupName="Estacionamiento" Text="Sí" Enabled="false" />
                <label></label>
                <asp:RadioButton ID="opt2_no" runat="server" GroupName="Estacionamiento" Text="No" Enabled="false" />
            </div>
        </div>

        <div class="form-group">
            <asp:Label ID="Label12" runat="server" CssClass="control-label col-sm-3">Posee estacionamiento de Bicicleta:</asp:Label>
            <div class="col-sm-3" style="width: 100px; margin-left: 10px; margin-top: 10px">
                <asp:RadioButton ID="opt5_si" runat="server" GroupName="EstacionamientoBicicleta" Text="Sí" Enabled="false" />
                <label></label>
                <asp:RadioButton ID="opt5_no" runat="server" GroupName="EstacionamientoBicicleta" Text="No" Enabled="false"/>
            </div>
        </div>

        <div class="form-group">
            <asp:Label ID="Label3" runat="server" CssClass="control-label col-sm-3">Red de tránsito pesado:</asp:Label>
            <div class="col-sm-3" style="width: 100px; margin-left: 10px; margin-top: 10px">
                <asp:RadioButton ID="opt3_si" runat="server" GroupName="RedTansitoPesado" Text="Sí" Enabled="false" />
                <label></label>
                <asp:RadioButton ID="opt3_no" runat="server" GroupName="RedTansitoPesado" Text="No" Enabled="false" />
            </div>
        </div>

        <div class="form-group">
            <asp:Label ID="Label4" runat="server" CssClass="control-label col-sm-3">Sobre Avenida:</asp:Label>
            <div class="col-sm-3" style="width: 100px; margin-left: 10px; margin-top: 10px">
                <asp:RadioButton ID="opt4_si" runat="server" GroupName="SobreAvenida" Text="Sí" Enabled="false" />
                <label></label>
                <asp:RadioButton ID="opt4_no" runat="server" GroupName="SobreAvenida" Text="No" Enabled="false" />
            </div>
        </div>

        <div class="form-group">
            <asp:Label ID="Label5" runat="server" CssClass="control-label col-sm-3"> Cantidad de operarios:</asp:Label>
            <div class="col-sm-3" style="margin-left: -5px">
                <asp:TextBox ID="txtCantOperarios" CssClass="form-control" runat="server" Width="100px" MaxLength="5" Enabled="false"></asp:TextBox>
            </div>

        </div>
    </div>
    <br />
    <br />


    <%--Servicios Sanitarios--%>
    <div class="pull-right" style="margin-top: -340px; margin-right: 20px">
        <div class="titulo-5" style="margin-top: 30px">
            <b>Servicios sanitarios</b>
        </div>
        <div style="margin-right: 20px; margin-top: 40px">
            <div class="form-horizontal">
                <div class="form-group">
                    <div style="margin-left: -240px">
                        <asp:Label ID="Label6" runat="server" CssClass="control-label col-sm-6"> Los mismos se encuentran:</asp:Label>
                    </div>
                    <div class="col-sm-2" style="width: 275px; margin-left: 10px; margin-top: 10px">
                        <asp:RadioButton ID="opt5_dentro" Checked="true" runat="server" GroupName="Sanitarios" onclick="objVisibility('tblDistanciaSanitarios_dl','hide'); ocultarReq();" Text="Dentro del Local" Enabled="false" />
                        <asp:RadioButton ID="opt5_fuera" runat="server" GroupName="Sanitarios" onclick="objVisibility('tblDistanciaSanitarios_dl','show');" Text="Fuera del Local" Enabled="false" />
                    </div>
                </div>


                <div class="form-group">
                    <div id="tblDistanciaSanitarios_dl" style="display: none;">
                        <asp:Label ID="lblDistanciaSanitarioa_dl" runat="server" CssClass="control-label col-sm-9">¿a que distancia? (metros):</asp:Label>
                        <div class="col-sm-3" style="margin-left: -5px">
                            <asp:TextBox ID="txtDistanciaSanitarios_dl" CssClass="form-control" runat="server" MaxLength="4" Width="70px" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">

                    <div style="margin-left: -240px">
                        <asp:Label ID="lblAccesibilidad" runat="server" CssClass="control-label col-sm-6">Accesibilidad del local a habilitar:</asp:Label>
                    </div>
                    <div class="col-sm-2" style="width: 275px; margin-left: 10px; margin-top: 10px">
                        <asp:RadioButton ID="rbtnCumpleLey962" runat="server" GroupName="Accesibilidad" Text="Cumple con la accesibilidad según Código de Edificación " Enabled="false" />
                        <br />
                        <asp:RadioButton ID="rbtnEximidoLey962" runat="server" GroupName="Accesibilidad" Text="Se exime de la accesibilidad conforme Resolución 345/AGC/2021" Enabled="false" />

                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="Label7" runat="server" CssClass="control-label col-sm-9">Cantidad de artefactos sanitarios:</asp:Label>
                    <div class="col-sm-3" style="margin-left: -5px">
                        <asp:TextBox ID="txtCantidadArtefactosSanitarios" CssClass="form-control" runat="server" MaxLength="4" Width="70px" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblPoseeSalubridad" runat="server" CssClass="control-label col-sm-9">Posee servicio de salubridad especial?:</asp:Label>
                    <div class="col-sm-3" style="margin-left: -5px">
                        <asp:CheckBox ID="chkSalubridad" runat="server" Enabled="false" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label8" runat="server" CssClass="control-label col-sm-9">Superficie de Sanitarios:</asp:Label>
                    <div class="col-sm-3" style="margin-left: -5px">
                        <asp:TextBox ID="txtSuperficieSanitarios" CssClass="form-control" runat="server" MaxLength="8" Width="70px" Enabled="false"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="Label9" runat="server" CssClass="control-label col-sm-9">Superficie Salón de Ventas:</asp:Label>
                    <div class="col-sm-3" style="margin-left: -5px">
                        <asp:TextBox ID="txtLocalVenta" runat="server" CssClass="form-control" MaxLength="10" Width="70px" Enabled="false"></asp:TextBox>
                    </div>

                    <td colspan="2">
                        <asp:RequiredFieldValidator ID="ReqtxtLocalVenta" runat="server" ControlToValidate="txtLocalVenta"
                            ErrorMessage="Debe ingresar la Superficie del Salón de Ventas." CssClass="error-label"
                            Display="Dynamic" SetFocusOnError="true" ValidationGroup="Continuar"></asp:RequiredFieldValidator>
                    </td>

                </div>

            </div>
        </div>
    </div>
    <br />
    <%--Materiales Usados--%>
    <div style="margin-left: 40px">
        <strong>Materiales Utilizados</strong>
    </div>
    <table style="margin-top: 10px; margin-left: 40px">
        <tr>
            <td class="celda">
                <label>
                    Pisos:</label>
            </td>
            <td>
                <asp:TextBox ID="txtPisos" CssClass="form-control" runat="server" TextMode="MultiLine" MaxLength="200" Height="63px"
                    Width="750px" Enable="false" disabled="true" Style="margin-top: 5px"></asp:TextBox>
                <div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPisos"
                        ErrorMessage="El campo es obligatorio." CssClass="alert alert-small alert-danger mbottom0 mtop5" Display="Dynamic"
                        SetFocusOnError="true" ValidationGroup="Continuar"></asp:RequiredFieldValidator>
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
                    Height="63px" Enable="false" disabled="true" Width="750px" Style="margin-top: 5px"></asp:TextBox>
                <div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtParedes"
                        ErrorMessage="El campo es obligatorio." CssClass="alert alert-small alert-danger mbottom0 mtop5" Display="Dynamic"
                        SetFocusOnError="true" ValidationGroup="Continuar"></asp:RequiredFieldValidator>
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
                    Width="750px" Enable="false" disabled="true" Style="margin-top: 5px"></asp:TextBox>
                <div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTechos"
                        ErrorMessage="El campo es obligatorio." CssClass="alert alert-small alert-danger mbottom0 mtop5" Display="Dynamic"
                        SetFocusOnError="true" ValidationGroup="Continuar"></asp:RequiredFieldValidator>
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
                    Height="63px" Enable="false" disabled="true" Width="750px" Style="margin-top: 5px"></asp:TextBox>
                <div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRevestimientos"
                        ErrorMessage="El campo es obligatorio." CssClass="alert alert-small alert-danger mbottom0 mtop5" Display="Dynamic"
                        SetFocusOnError="true" ValidationGroup="Continuar"></asp:RequiredFieldValidator>
                </div>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <div class="box-shadow" style="margin-left: 50px">
        <b>Posee mas de 350 asistentes?:</b>
        <asp:RadioButton ID="asistentes_SI" runat="server" GroupName="Asistentes" Text="Sí" Enabled="false" />
        <asp:RadioButton ID="asistentes_NO" runat="server" GroupName="Asistentes" Text="No" Enabled="false" />
    </div>
    <%--SobreCarga--%>
    <div class="box-shadow" style="margin-left: 50px">
        <b>Posee planta/s por debajo de la planta/s a habilitar?:</b>
        <asp:RadioButton ID="optsCertificadoSobrecarga_SI" runat="server" GroupName="Sobrecarga" Text="Sí" Enabled="false" />
        <asp:RadioButton ID="optsCertificadoSobrecarga_NO" runat="server" GroupName="Sobrecarga" Text="No" Enabled="false" />
    </div>

    <div class="box-shadow" style="margin-left: 50px">
        <b>Posee productos inflamables?:</b>
        <asp:RadioButton ID="productosInflamables_SI" runat="server" GroupName="productosInflamables" Text="Sí" Enabled="false" />
        <asp:RadioButton ID="productosInflamables_NO" runat="server" GroupName="productosInflamables" Text="No" Enabled="false" />
    </div>
</asp:Panel>
<script type="text/javascript">

    $(document).ready(function () {
        //debugger;
        if ($('#<%: opt5_dentro.ClientID %>').attr("checked") == "checked")
            $("#tblDistanciaSanitarios_dl").css("display", "none");
        else
            $("#tblDistanciaSanitarios_dl").css("display", "block");
    });

    function ocultarDistanciaSanitarios(accion) {
        //recibe el id del objeto a mostrar u ocultar y la accion 'show'-> mostrar 'hide' o nada->ocultar
        if (accion == 'show')
            $("#tblDistanciaSanitarios_dl").css("display", "block");
        else
            $("#tblDistanciaSanitarios_dl").css("display", "none");
        return false;
    }
    function noExisteFotoParcela(objimg) {

        $(objimg).attr("src", "/Content/img/app/ImageNotFound.png");

        return true;
    }
</script>
