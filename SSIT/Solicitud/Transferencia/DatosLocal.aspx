<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DatosLocal.aspx.cs" Inherits="SSIT.Solicitud.Transferencia.DatosLocal" %>


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
        <h2>Datos del Local</h2>
        <hr />

        <asp:HiddenField ID="hid_DecimalSeparator" runat="server" />
        <asp:HiddenField ID="hid_return_url" runat="server" />
        <asp:HiddenField ID="hid_id_encomienda" runat="server" />

        <p style="margin: auto; padding: 10px; line-height: 20px">
            En este paso podr&aacute; ver los mapas en donde se encuentra la ubicaci&oacute;n y especificar las superficies del lugar.
        </p>
        
        <%--Box de Mapas--%>
        <div id="box_datoslocal" class="box-panel" Style="background-color:#ffffff">


            <div style="margin: 20px; margin-top: -5px">
                <div style="color: #377bb5">
                    <h4><i class="imoon imoon-office" style="margin-right: 10px"></i>Datos de la Ubicacion</h4>
                    <hr />
                </div>
            </div>
            <%-- contenido de Mapas --%>
            <asp:UpdatePanel ID="updDatosLocal" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                       <div class="row" style="margin-left:20px">
                        <div class="form-horizontal" style="margin-left:-35px">
                        <div class="col-sm-6">
                            <div class="col-sm-12 text-center">
                                <strong>Entre calles</strong>
                            </div>

                            <div class="text-center ">
                                <asp:Image ID="imgMapa1" runat="server" CssClass="img-thumbnail" onError="noExisteFotoParcela(this);"/>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="col-sm-12 text-center">
                                <strong>Croquis</strong>
                            </div>

                            <div class="text-center ">
                                <asp:Image ID="imgMapa2" runat="server" CssClass="img-thumbnail" onError="noExisteFotoParcela(this);"/>
                            </div>

                        </div>
                        </div>
                           <div class="col-sm-12"><br /></div>

                        <div class="col-sm-6">

                            <div class="col-sm-12 text-center pbottom15">
                                <strong>Dimensiones de la Habilitación</strong>
                            </div>
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <asp:Label runat="server" class="col-sm-6 control-label">Superficie cubierta:</asp:Label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtSuperficieCubierta" runat="server" Text="0,00"  Width="100px" CssClass="form-control text-right"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:Label runat="server" class="col-sm-6 control-label">Superficie descubierta:</asp:Label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtSuperficieDescubierta" runat="server" Text="0,00" Width="100px" CssClass="form-control text-right"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:Label runat="server" class="col-sm-6 control-label">Superficie total:</asp:Label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtSuperficieTotal" runat="server" Text="0,00" Width="100px" CssClass="form-control text-right" ></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:Label runat="server" class="col-sm-6 control-label">Dimensión del Frente:</asp:Label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtDimensionFrente" runat="server" Text="0,00" Width="100px"  CssClass="form-control text-right"></asp:TextBox>
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
                                <asp:Label runat="server" class="control-label col-sm-6">Frente:</asp:Label>
                                <div class="col-sm-6">
                                   <asp:TextBox ID="txtFrente" runat="server" MaxLength="10" Width="100px" CssClass="form-control text-right"></asp:TextBox>
                             
                                </div>
                            </div>
                            <div class="form-group" style="text-align: center">
                                <asp:Label runat="server"  class="control-label col-sm-6">Fondo:</asp:Label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtFondo" runat="server" MaxLength="10" Width="100px" CssClass="form-control text-right"></asp:TextBox>  
                                </div>

                            </div>
                            <div class="form-group" style="text-align: center">
                                <asp:Label runat="server"  class="control-label col-sm-6">Lateral Izquierdo:</asp:Label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtLatIzq" runat="server" MaxLength="10" Width="100px" CssClass="form-control text-right"></asp:TextBox>

                                </div>

                            </div>
                            <div class="form-group" style="text-align: center">

                                <asp:Label runat="server"  class="control-label col-sm-6">Lateral Derecho:</asp:Label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtLatDer" runat="server" MaxLength="10" Width="100px" CssClass="form-control text-right"></asp:TextBox>
           
                                </div>
 
                            </div>
                           </div>
                          </div>
                        </div>                 
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
        <br />
        <div class="box-panel" Style="background-color:#ffffff">
            <div style="margin: 20px; margin-top: -5px">
                <div style="color: #377bb5">
                    <h4><i class="imoon imoon-office" style="margin-right: 10px"></i>Caracteristicas del Local</h4>
                    <hr />
                </div>
            </div>
            <asp:UpdatePanel ID="updCaracteristicas" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <%--Características del local--%>

                    <div class="titulo-5" style="margin-left:15px">
                        <b>Caracter&iacute;sticas Generales</b>
                    </div>
                    <div style="margin-top: 10px">

                        <div class="form-horizontal" style="margin-left:35px">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="control-label col-sm-4" Style="margin-left: -95px">Posee lugar de carga y descarga:</asp:Label>
                                <div class="col-sm-3" style="width: 100px; margin-left: 10px; margin-top:10px">
                                    <asp:RadioButton ID="opt1_si" runat="server" GroupName="LugarCargaDescarga" Text="Sí" />
                                    <asp:Label runat="server"></asp:Label>
                                    <asp:RadioButton ID="opt1_no" runat="server" GroupName="LugarCargaDescarga" Text="No" />
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="control-label col-sm-3">Posee estacionamiento:</asp:Label>
                                <div class="col-sm-3" style="width: 100px; margin-left: 10px; margin-top:10px"">
                                    <asp:RadioButton ID="opt2_si" runat="server" GroupName="Estacionamiento" Text="Sí" />
                                    <asp:Label runat="server"></asp:Label>
                                    <asp:RadioButton ID="opt2_no" runat="server" GroupName="Estacionamiento" Text="No" />
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="control-label col-sm-3">Red de tránsito pesado:</asp:Label>
                                <div class="col-sm-3" style="width: 100px; margin-left: 10px; margin-top:10px"">
                                    <asp:RadioButton ID="opt3_si" runat="server" GroupName="RedTansitoPesado" Text="Sí" />
                                    <asp:Label runat="server"></asp:Label>
                                    <asp:RadioButton ID="opt3_no" runat="server" GroupName="RedTansitoPesado" Text="No" />
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" CssClass="control-label col-sm-3">Sobre Avenida:</asp:Label>
                                <div class="col-sm-3" style="width: 100px; margin-left: 10px; margin-top:10px"">
                                    <asp:RadioButton ID="opt4_si" runat="server" GroupName="SobreAvenida" Text="Sí" />
                                    <asp:Label runat="server"></asp:Label>
                                    <asp:RadioButton ID="opt4_no" runat="server" GroupName="SobreAvenida" Text="No" />
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" CssClass="control-label col-sm-3"> Cantidad de operarios:</asp:Label>
                                <div class="col-sm-3" style="margin-left: -5px">
                                    <asp:TextBox ID="txtCantOperarios" CssClass="form-control" runat="server" Width="100px" MaxLength="5"></asp:TextBox>
                                </div>

                            </div>
                        </div>
                    </div>
                    <br />
                    <br />

                    <%--Servicios Sanitarios--%>
                    <div class="pull-right" style="margin-top: -340px; margin-right:20px">
                        <div class="titulo-5" style="margin-top: 10px">
                            <b>Servicios sanitarios</b>
                        </div>
                        <div style="margin-top: 10px">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div style="margin-left: -240px">
                                        <asp:Label ID="Label6" runat="server" CssClass="control-label col-sm-6"> Los mismos se encuentran:</asp:Label>
                                    </div>
                                    <div class="col-sm-2" style="width: 275px; margin-left: 10px; margin-top:10px"">
                                        <asp:RadioButton ID="opt5_dentro" Checked="true" runat="server" GroupName="Sanitarios" onclick="objVisibility('tblDistanciaSanitarios_dl','hide'); ocultarReq();" Text="Dentro del Local" />
                                        <asp:RadioButton ID="opt5_fuera" runat="server" GroupName="Sanitarios" onclick="objVisibility('tblDistanciaSanitarios_dl','show');" Text="Fuera del Local" />
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div id="tblDistanciaSanitarios_dl" style="display: none;">
                                        <asp:Label ID="lblDistanciaSanitarioa_dl" runat="server" CssClass="control-label col-sm-9">¿a que distancia? (metros):</asp:Label>
                                        <div class="col-sm-3" style="margin-left: -5px">
                                            <asp:TextBox ID="txtDistanciaSanitarios_dl" CssClass="form-control" runat="server" MaxLength="4" onfocus="this.select();" Width="70px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div id="ReqDistanciaSanitarios" class="field-validation-error" style="display: none">
                                    Al seleccionar Fuera del local debe indicar la distancia.
                                </div>

                                <td colspan="2">
                                    <%--       <asp:CustomValidator ID="CustomValidator3" runat="server" 
                                                            ClientValidationFunction="validarIngresoRespuestasSanitarios" 
                                                            CssClass="error-label" Display="Dynamic" 
                                                            ErrorMessage="Las respuestas son obligatorias y de selecciona 'Fuera del local', la distancia es obligatoria.." 
                                                            ValidationGroup="Continuar"></asp:CustomValidator>--%>
                                </td>

                                <div class="form-group">
                                    <asp:Label ID="Label7" runat="server" CssClass="control-label col-sm-9">Cantidad de artefactos sanitarios:</asp:Label>
                                    <div class="col-sm-3" style="margin-left: -5px">
                                        <asp:TextBox ID="txtCantidadArtefactosSanitarios" CssClass="form-control" runat="server" MaxLength="4" Width="70px"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="Label8" runat="server" CssClass="control-label col-sm-9">Superficie de Sanitarios:</asp:Label>
                                    <div class="col-sm-3" style="margin-left: -5px">
                                        <asp:TextBox ID="txtSuperficieSanitarios" CssClass="form-control" runat="server" MaxLength="8" Width="70px"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:Label ID="Label9" runat="server" CssClass="control-label col-sm-9">Superficie Salón de Ventas:</asp:Label>
                                    <div class="col-sm-3" style="margin-left: -5px">
                                        <asp:TextBox ID="txtLocalVenta" runat="server" CssClass="form-control" MaxLength="10" Width="70px"></asp:TextBox>
                                    </div>

                                    <div style="margin-left: 45px; margin-top: 40px">
                                        <asp:CompareValidator ID="cvtxtLocalVenta" runat="server" 
                                            ControlToValidate="txtLocalVenta" 
                                            Operator="LessThanEqual"
                                            ControlToCompare="txtSuperficieTotal"
                                            Type="Double"
                                            ErrorMessage="La cantidad debe ser menor o igual a la superficie total." 
                                            CssClass="field-validation-error"
                                            Display="Dynamic"
                                            
                                            ValidationGroup="Continuar">
                                        </asp:CompareValidator>
                                    </div>

                                </div>

                            </div>
                        </div>
                    </div>

                    <%--Materiales Empleados en:--%>
                    <div class="titulo-5" style="margin-left:20px">
                        <b>Materiales expresados en...</b>
                    </div>
                    <table style="margin-top: 10px; margin-left:40px">
                        <tr>
                            <td class="celda">
                                <asp:Label runat="server">
                                    Pisos:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPisos" CssClass="form-control" runat="server" TextMode="MultiLine" MaxLength="200" Height="63px"
                                    Width="750px" Style="margin-top: 5px"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td class="celda">
                                <asp:Label runat="server">
                                    Paredes:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtParedes" CssClass="form-control" runat="server" TextMode="MultiLine" MaxLength="200"
                                    Height="63px" Width="750px" Style="margin-top: 5px"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td class="celda">
                                <asp:Label runat="server">
                                    Techos:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTechos" CssClass="form-control" runat="server" TextMode="MultiLine" MaxLength="200" Height="63px"
                                    Width="750px" Style="margin-top: 5px"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td class="celda">
                                <asp:Label runat="server">
                                    Revestimientos:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRevestimientos" CssClass="form-control" runat="server" TextMode="MultiLine" MaxLength="200"
                                    Height="63px" Width="750px" Style="margin-top: 5px"></asp:TextBox>

                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <br />
                    <%--SobreCarga--%>
                    <div class="form-group">
                        <asp:Label ID="Label10" runat="server" CssClass="control-label col-sm-6" Font-Bold="true">Posee planta/s por debajo de la planta/s a habilitar?:</asp:Label>

                        <div class="col-sm-3" style="width: 100px; margin-left: -70px; ">
                            <asp:RadioButton ID="optsCertificadoSobrecarga_SI" runat="server" GroupName="Sobrecarga" Text="Sí" />
                            <asp:Label runat="server"></asp:Label>
                            <asp:RadioButton ID="optsCertificadoSobrecarga_NO" runat="server" Checked="true" GroupName="Sobrecarga" Text="No" />
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <br />
        <br />

        <%--Botones de Guardado--%>
        <asp:UpdatePanel ID="updBotonesGuardar" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <div class="form-inline text-right mtop20">

                    <div id="pnlBotonesGuardar" class="form-group">
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-lg btn-primary" OnClick="btnContinuar_Click" CausesValidation="true" ValidationGroup="Continuar" OnClientClick="return validarGuardar();">
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
                        <h4 class="modal-title" style="margin-top: -8px">Error</h4>
                    </div>
                    <div class="modal-body">
                        <table style="border-collapse: separate; border-spacing: 5px">
                            <tr>
                                <td style="text-align: center; vertical-align: text-top">
                                    <asp:Label runat="server" class="imoon imoon-remove-circle fs64" style="color: #f00"></asp:Label>
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

        function finalizarCarga() {
            $("#Loading").hide();
            $("#page_content").show();
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
                TotalizarSuperficie();
            });

            $('#<%: txtSuperficieDescubierta.ClientID %>').on("keyup", function () {
                TotalizarSuperficie();
            });

            $('#<%: txtDistanciaSanitarios_dl.ClientID %>').on("keyup", function () {
                $("#ReqDistanciaSanitarios").hide();
            });


            if ($('#<%: opt5_dentro.ClientID %>').attr("checked") == "checked")
                objVisibility('tblDistanciaSanitarios_dl', 'hide');
            else
                objVisibility('tblDistanciaSanitarios_dl', 'show');

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

        function ocultarBotonesGuardado() {

            $("#pnlBotonesGuardar").hide();

            return true;
        }

        function ocultarReq() {
            $("#ReqDistanciaSanitarios").hide();
            return false;
        }

        function validarGuardar() {

            var ret = true;
            $("#ReqDistanciaSanitarios").hide();

            var valorDistancia = $("#<%: txtDistanciaSanitarios_dl.ClientID %>").val();
            var totalDistancia = stringToFloat(valorDistancia);

            if ($("#<%: opt5_fuera.ClientID %>").is(":checked") && totalDistancia <= 0) {
                $("#ReqDistanciaSanitarios").show();
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
    </script>
</asp:Content>
