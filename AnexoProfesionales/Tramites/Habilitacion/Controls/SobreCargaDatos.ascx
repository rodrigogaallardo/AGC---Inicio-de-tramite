<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SobreCargaDatos.ascx.cs" Inherits="AnexoProfesionales.SobreCargaDatos" %>

<link href="<%: ResolveUrl("~/Content/themes/base/jquery.ui.custom.css") %>" rel="stylesheet" />

<%: Scripts.Render("~/bundles/autoNumeric") %>
<%: Scripts.Render("~/bundles/select2") %>
<%: Styles.Render("~/bundles/Select2Css") %>


<div id="pnlSobreCargaDatos" style="width: 100%;">
    <asp:UpdatePanel ID="updSobrecarga" runat="server" >
        <ContentTemplate>

            <asp:Panel ID="pnlContentBuscar" runat="server">
                <asp:HiddenField ID="hid_DecimalSeparatorS" runat="server" />
                <%--Tabs de Busqueda--%>
                <div id="tabs" >            
                  <div class="row mleft10 mright10">                 
                        <asp:UpdatePanel ID="updpnlBuscar" runat="server" UpdateMode="Conditional" >
                           <ContentTemplate>
                                <asp:HiddenField ID="hid_rowindex_fir" runat="server" />
                            <div class="widget-content">
                              <div class="form-horizontal">               
                                    <div class="form-group form-group-sm">                                       
                                        <asp:label ID="lblTipoDestino" runat="server" CssClass="control-label col-sm-2" >Destino:</asp:label>
                                         <div class="col-sm-3">
                                           <asp:DropDownList ID="ddlDestino" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDestino_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <div id="Req_Destino" class="alert alert-small alert-danger mbottom0 mtop5" style="display: none;">
                                                Debe ingresar el Destino.
                                            </div>
                                         </div>
                                     </div>  
                                    
                                    <div class="form-group form-group-sm">             
                                        <label  class="control-label col-sm-2">Planta:</label>
                                          <div class="col-sm-3">   
                                              <asp:DropDownList ID="ddlPlantas" runat="server" CssClass="form-control">
                                             </asp:DropDownList>
                                            <div id="Req_Plantas" class="alert alert-small alert-danger mbottom0 mtop5" style="display: none;">
                                                Debe ingresar la Planta. Este dato se carga en Ubicación
                                            </div>
                                       </div>
                                       <label  class="control-label col-sm-2">Losa Sobre:</label>
                                       <div class="col-sm-3"  > 
                                           <asp:TextBox ID="txtLosaSobre" runat="server" CssClass="form-control" width="200px" Height="30px"></asp:TextBox>
                                            <div id="Req_LosaSobre" class="alert alert-small alert-danger mbottom0 mtop5" style="display: none;">
                                                Debe ingresar la Losa Sobre.
                                            </div>
                                         </div>
                                     </div>
                                    <div class="form-group form-group-sm">             

                                        <label  class="control-label col-sm-2">Uso:</label>
                                        <div class="col-sm-3" >
                                        <asp:DropDownList ID="ddlUsos" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlUsos_SelectedIndexChanged" AutoPostBack="true" >
                                           </asp:DropDownList>
                                            <div id="Req_Usos" class="alert alert-small alert-danger mbottom0 mtop5" style="display: none;">
                                                Debe ingresar el Uso.
                                            </div>
                                        </div> 
                                       <asp:label ID="lblSobrecarga" runat="server" CssClass="control-label col-sm-2" >Sobrecarga:</asp:label>
                                       <div class="col-sm-3">
                                            <asp:TextBox ID="txtSobrecarga" CssClass="form-control" runat="server" MaxLength="6" Width="200px"></asp:TextBox>
                                            <asp:TextBox ID="txtDetalle" CssClass="form-control" runat="server" MaxLength="100" Width="150px" Visible="false"></asp:TextBox>
                                            <asp:HiddenField ID="hid_min_req" runat="server" />
                                            <div id="Req_Sobrecarga" class="alert alert-small alert-danger mbottom0 mtop5" style="display: none;">
                                                Debe ingresar el dato.
                                            </div>
                                            <div id="Req_SobrecargaMin" class="alert alert-small alert-danger mbottom0 mtop5" style="display: none;">
                                                Valor ingresado no alcanza el valor mínimo exigido por la norma.
                                            </div>

                                        </div>
                                    
                                    </div>  
                                                                  
                                    <div class="form-group form-group-sm">
                                       <asp:label ID="lblUso1" runat="server" CssClass="control-label col-sm-2" >Tipo uso 1:</asp:label>
                                       <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlUsos1" runat="server" CssClass="form-control" width="200px" Height="30px"></asp:DropDownList>
                                        </div>
                                       
                                      
                                       <asp:label ID="lblTxtUso1" runat="server" CssClass="control-label col-sm-2" >Uso mínimo 1:</asp:label>
                                       <div class="col-sm-3"  >
                                            <asp:TextBox ID="txtUso1" runat="server" CssClass="form-control" width="200px" Height="30px"></asp:TextBox>
                                           <asp:HiddenField ID="hid_min_uso1_req" runat="server" />
                                            <div id="Req_Uso1" class="alert alert-small alert-danger mbottom0 mtop5" style="display: none;">
                                                Debe ingresar el dato.
                                            </div>
                                            <div id="Req_Uso1Min" class="alert alert-small alert-danger mbottom0 mtop5" style="display: none;">
                                                Valor ingresado no alcanza el valor mínimo exigido por la norma.
                                            </div>

                                        </div>
                                  </div>

                                    <div class="form-group form-group-sm">
                                         <asp:label ID="lblUso2" runat="server" CssClass="control-label col-sm-2" >Tipo uso 2:</asp:label>
                                          <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlUsos2" runat="server" CssClass="form-control" width="200px" Height="30px"></asp:DropDownList>
                                          </div>

                                       
                                       <asp:label ID="lblTxtUso2" runat="server" CssClass="control-label col-sm-2" >Uso mínimo 2:</asp:label>
                                       <div class="col-sm-3"  >
                                            <asp:TextBox ID="txtUso2" runat="server" CssClass="form-control" width="200px" Height="30px"></asp:TextBox>
                                            <asp:HiddenField ID="hid_min_uso2_req" runat="server" />
                                            <asp:HiddenField ID="hid_requiere_detalle_2" runat="server" />    
                                            <div id="Req_Uso2" class="alert alert-small alert-danger mbottom0 mtop5" style="display: none;">
                                                Debe ingresar el dato.
                                            </div>
                                            <div id="Req_Uso2Min" class="alert alert-small alert-danger mbottom0 mtop5" style="display: none;">
                                                Valor ingresado no alcanza el valor mínimo exigido por la norma.
                                            </div>
                                       </div>
                                      
                                  
                                  </div>

                                    
                             </div> 
                                </div>
                             <br />
                             <div class="pull-right mright10">
                               <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-default" OnClick="btnCerrar_Click">
                                    <i class="imoon-close"></i>
                                    <span class="text">Cerrar</span>
                                </asp:LinkButton>
                            </div>
                                <div class="pull-right mright10">
                                     <asp:LinkButton ID="btnCerrar5" runat="server" CssClass="btn btn-default" OnClick="btnIngresarSobrecarga_Click" OnClientClick="return validar();" >
                                                    <i class="imoon-white imoon-plus"></i>
                                                    <span class="text">Aceptar</span>
                                            </asp:LinkButton>
                              </div>  
                            </ContentTemplate>
                        </asp:UpdatePanel>                  
                </div>

             </div>
            </asp:Panel>      

        </ContentTemplate>

    </asp:UpdatePanel>

    
    <script type="text/javascript">
        var vconfirm = false;
        $(document).ready(function () {
            init_JS_updSobrecarga();
        });

        function init_JS_updSobrecarga() {

            var vSeparadorDecimal= $("#<%: hid_DecimalSeparatorS.ClientID %>").val();
            $("#<%: txtSobrecarga.ClientID %>").autoNumeric("init", { aSep: '.', mDec: 2, vMax: '999999.99', aDec: ',' });
            $("#<%: txtUso1.ClientID %>").autoNumeric("init", { aSep: '.', mDec: 2, vMax: '999999.99',  aDec: ','  });
            $("#<%: txtUso2.ClientID %>").autoNumeric("init", { aSep: '.', mDec: 2, vMax: '999999.99', aDec: ',' });


            $("#<%: txtSobrecarga.ClientID %>").on("keyup", function (e) {
                $("#Req_Sobrecarga").hide();
                $("#Req_SobrecargaMin").hide();
            });
            $("#<%: txtUso1.ClientID %>").on("keyup", function (e) {
                $("#Req_Uso1").hide();
                $("#Req_Uso1Min").hide();
            });
            $("#<%: txtUso2.ClientID %>").on("keyup", function (e) {
                $("#Req_Uso2").hide();
                $("#Req_Uso2Min").hide();
            });
            $("#<%: txtLosaSobre.ClientID %>").on("keyup", function (e) {
                $("#Req_LosaSobre").hide();
            });

            $("#<%: ddlDestino.ClientID %>").on("change", function (e) {
                $("#Req_Destino").hide();
            });
            $("#<%: ddlPlantas.ClientID %>").on("change", function (e) {
                $("#Req_Plantas").hide();
            });
            $("#<%: ddlUsos.ClientID %>").on("change", function (e) {
                $("#Req_Usos").hide();
            });
        }

        function validar() {
            var ret = true;
            var vSeparadorDecimal= $("#<%: hid_DecimalSeparatorS.ClientID %>").val();
            $("#Req_Sobrecarga").hide();
            $("#Req_SobrecargaMin").hide();
            $("#Req_Uso1").hide();
            $("#Req_Uso1Min").hide();
            $("#Req_Uso2").hide();
            $("#Req_Uso2Min").hide();
            $("#Req_LosaSobre").hide();
            $("#Req_Destino").hide();
            $("#Req_Plantas").hide();
            $("#Req_Usos").hide();
            //debugger;
            if ($.trim($("#<%: txtSobrecarga.ClientID %>").val()).length == 0) {
                $("#Req_Sobrecarga").css("display", "inline-block");
                ret = false;
            }
            else {
                var val = $("#<%: txtSobrecarga.ClientID %>").val();
                var min = $("#<%: hid_min_req.ClientID %>").val();
                var val1 = stringToFloat(val, vSeparadorDecimal);
                var val2 = stringToFloat(min, vSeparadorDecimal);
                if (val1 < val2) {
                    $("#Req_SobrecargaMin").css("display", "inline-block");
                    ret = false;
                }
            }
            if ($.trim($("#<%: txtUso1.ClientID %>").val()).length == 0) {
                $("#Req_Uso1").css("display", "inline-block");
                ret = false;
            }
            else {
                var val = $("#<%: txtUso1.ClientID %>").val();
                var min = $("#<%: hid_min_uso1_req.ClientID %>").val();
                var val1 = stringToFloat(val, vSeparadorDecimal);
                var val2 = stringToFloat(min, vSeparadorDecimal);
                if (val1 < val2) {
                    $("#Req_Uso1Min").css("display", "inline-block");
                    ret = false;
                }
            }

            if ($.trim($("#<%: txtUso2.ClientID %>").val()).length == 0) {
                $("#Req_Uso2").css("display", "inline-block");
                ret = false;
            }
            else {
                var val = $("#<%: txtUso2.ClientID %>").val();
                var min = $("#<%: hid_min_uso2_req.ClientID %>").val();
                var val1 = stringToFloat(val, vSeparadorDecimal);
                var val2 = stringToFloat(min, vSeparadorDecimal);
                if (val1 < val2) {
                    $("#Req_Uso2Min").css("display", "inline-block");
                    ret = false;
                }
            }

            if ($.trim($("#<%: txtLosaSobre.ClientID %>").val()).length == 0) {
                $("#Req_LosaSobre").css("display", "inline-block");
                ret = false;
            }
            if ($("#<%: ddlDestino.ClientID %>").val() == 0) {
                $("#Req_Destino").css("display", "inline-block");
                ret = false;
            }
            if ($("#<%: ddlPlantas.ClientID %>").val() == 0) {
                $("#Req_Plantas").css("display", "inline-block");
                ret = false;
            }
            if ($("#<%: ddlUsos.ClientID %>").val() == 0) {
                $("#Req_Usos").css("display", "inline-block");
                ret = false;
            }
            return ret;
        }

      

    </script>

</div>


