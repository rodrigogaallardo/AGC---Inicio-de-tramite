<%@ Page Title="Datos del Local" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DatosLocal.aspx.cs" Inherits="SSIT.Solicitud.DatosLocal" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">


    <%: Scripts.Render("~/bundles/autoNumeric") %>
    <%: Scripts.Render("~/bundles/autoNumeric") %>
    <%--ajax cargando ...--%>

    <div id="Loading" style="text-align: center; padding-bottom: 20px; margin-top: 120px">
        <img src="<%: ResolveUrl("~/Content/img/app/Loading128x128.gif") %>" alt="" />
        <h3>Cargando datos... </h3>
    </div>

    <asp:UpdatePanel ID="updCargarDatos" runat="server">
        <ContentTemplate>
            <asp:Button ID="btnCargarDatos" runat="server" OnClick="btnCargarDatos_Click" Style="display: none" />
            <asp:HiddenField ID="hid_id_solicitud" runat="server" />
            <asp:HiddenField ID="hid_id_tipo_tramite" runat="server" />
            <asp:HiddenField ID="hid_return_url" runat="server" />
            <asp:HiddenField ID="hid_DecimalSeparator" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <%--fin ajax cargando--%>

    <div id="page_content" style="display: none">

        <div class="mtop30" style="display: none">
            <h3>Datos del Local</h3>
            <hr />
        </div>

        <div class="row">


            <div>
                <h3>Datos del local</h3>
                <hr class="mtop10" />
            </div>


            <asp:UpdatePanel ID="updDatosLocal" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <div class="row">
                        <div class="col-sm-4">
                            <div class="col-sm-12 text-center">
                                <strong>Entre calles</strong>
                            </div>

                            <div class="text-center ">
                                <asp:Image ID="imgMapa1" runat="server" CssClass="img-thumbnail" />
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="col-sm-12 text-center">
                                <strong>Croquis</strong>
                            </div>

                            <div class="text-center ">
                                <asp:Image ID="imgMapa2" runat="server" CssClass="img-thumbnail" />
                            </div>

                        </div>
                        <div class="col-sm-4">

                            <div class="col-sm-12 text-center pbottom15">
                                <strong>Superficies</strong></label>
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
                                    <div class="col-sm-12 pleft20 pright20">

                                        <div id="ReqSuperficies" class="alert alert-danger" style="display: none">
                                            Los campos de superficie son obligatorios (al menos uno).
                                        </div>

                                    </div>
                                </div>

                                <asp:Panel ID="pnlAvisomodifSuperficie" runat="server" CssClass="alert alert-info mtop5" Visible="false">
                                    Para poder modificar la superficie, la solicitud no debe contener ningún rubro cargado. Debe quitar los rubros si desea modificarla.
                                </asp:Panel>

                            </div>

                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>



            <%--Botones de Guardado--%>
            <asp:UpdatePanel ID="updBotonesGuardar" runat="server" UpdateMode="Conditional">
                <ContentTemplate>

                    <div class="form-inline text-right mtop20">
                        <div id="pnlBotonesGuardar" class="form-group">

                            <asp:LinkButton ID="btnContinuar" runat="server" CssClass="btn btn-primary" OnClick="btnContinuar_Click" OnClientClick="return validarGuardar();">
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

    </div>

        <%--Modal mensajes de error--%>
    <div id="frmError" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top: -8px">Atención</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <asp:Label runat="server" class="imoon imoon-info fs64" Style="color: #377bb5"></asp:Label>
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

            $('#<%: txtSuperficieCubierta.ClientID %>').on("keyup", function () {
                $("#ReqSuperficies").hide();
                TotalizarSuperficie();
            });

            $('#<%: txtSuperficieDescubierta.ClientID %>').on("keyup", function () {
                $("#ReqSuperficies").hide();
                TotalizarSuperficie();
            });

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

         function validarGuardar() {

             var ret = true;

             $("#ReqSuperficies").hide();

             var value1 = $("#<%: txtSuperficieTotal.ClientID %>").val();
            var total = stringToFloat(value1);

            if (total <= 0) {
                $("#ReqSuperficies").show();
                ret = false;
            }

            if (ret)
                ocultarBotonesGuardado();

            return ret;

        }

     </script>

</asp:Content>
