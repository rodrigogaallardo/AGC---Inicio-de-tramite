<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConformacionLocalDatos.ascx.cs" Inherits="AnexoProfesionales.Controls.ConformacionLocalDatos" %>
<link href="<%: ResolveUrl("~/Content/themes/base/jquery.ui.custom.css") %>" rel="stylesheet" />

<%: Scripts.Render("~/bundles/autoNumeric") %>
<%: Scripts.Render("~/bundles/select2") %>
<%: Styles.Render("~/bundles/Select2Css") %>


<div id="pnlBuscarUbicacion" style="width: 100%;">
    <asp:UpdatePanel ID="updConformacionLocalDatos" runat="server" >
        <ContentTemplate>

            <asp:Panel ID="pnlContentBuscar" runat="server">
                <asp:HiddenField ID="hid_conflocal" runat="server" />
                <asp:HiddenField ID="hid_DecimalSeparatorCL" runat="server" />
                <%--Tabs de Busqueda--%>
                <div id="tabs" >            
                  <div class="row mleft10 mright10">                 
                        <asp:UpdatePanel ID="updpnlDatos" runat="server" UpdateMode="Conditional">
                           <ContentTemplate>
                            <div class="widget-content">
                              <div class="form-horizontal">               
                                    <div class="form-group form-group-sm">                                       
                                        <label  class="control-label col-sm-2" >Destino</label>
                                         <div class="col-sm-3">
                                           <asp:DropDownList ID="ddlDestino" runat="server" CssClass="form-control" Width="400px"
                                               OnSelectedIndexChanged="ddlDestino_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                         </div>
                                            
                                      </div>  

                                        <%--campo detalle--%>  
                                  <asp:Panel ID="pnlTxtDetalle" runat="server" Visible="false">
                                    <div class="form-group form-group-sm">                                       
                                        <label  class="control-label col-sm-2" >Detalle (*):</label>
                                         <div class="col-sm-3">
                                            <asp:TextBox ID="txtDetalle" runat="server" CssClass="form-control" MaxLength="200" Width="400px"></asp:TextBox>
                                            <div id="Req_Detalle" class="alert alert-small alert-danger mbottom0 mtop5" style="display: none;">
                                                Debe ingresar el Detalle.
                                            </div>
                                         </div>
                                            
                                      </div>
                                  </asp:Panel>                                     
                                      <div class="form-group form-group-sm"> 
                                          <label  class="control-label col-sm-2">Largo (*):</label>
                                           <div class="col-sm-2">   
                                              <asp:TextBox ID="txtLargo" runat="server" CssClass="form-control">
                                             </asp:TextBox>
                                                <div id="Req_Largo" class="alert alert-small alert-danger mbottom0 mtop5" style="display: none;">
                                                    Debe ingresar e Largo.
                                                </div>
                                           </div>
                                           
                                     
                                          <label  class="control-label col-sm-1">Ancho (*):</label>
                                          <div class="col-sm-2" >
                                             <asp:TextBox ID="txtAncho" runat="server" CssClass="form-control" >
                                             </asp:TextBox>
                                            <div id="Req_Ancho" class="alert alert-small alert-danger mbottom0 mtop5" style="display: none;">
                                                Debe ingresar el Ancho.
                                            </div>
                                          </div> 
                                    
                                       <label  class="control-label col-sm-1">Alto (*):</label>
                                       <div class="col-sm-2"  > 
                                           <asp:TextBox ID="txtAlto" runat="server" CssClass="form-control" Height="30px"></asp:TextBox>
                                         </div>
                                        <div id="Req_Alto" class="alert alert-small alert-danger mbottom0 mtop5" style="display: none;">
                                            Debe ingresar el Alto.
                                        </div>
                                       </div>  
                                                                  
                                    <div class="form-group ">
                                       
                                       <label  class="control-label col-sm-2" >Tipo de superficie (*):</label>
                                          <div class="col-sm-3"  >
                                      <asp:DropDownList ID="ddlTipoSuperficie" runat="server" CssClass="form-control" width="200px" Height="30px"></asp:DropDownList>
                                        <div id="Req_TipoSuperficie" class="alert alert-small alert-danger mbottom0 mtop5" style="display: none;">
                                            Debe ingresar el Tipo Superficie.
                                        </div>
                                   </div>
                                      
                                       <label  class="control-label col-sm-2" >Superficie (*):</label>
                                          <div class="col-sm-3"  >
                                          <asp:TextBox ID="txtSuperficie" runat="server" CssClass="form-control" width="200px" Height="30px"></asp:TextBox>
                                        <div id="Req_Superficie" class="alert alert-small alert-danger mbottom0 mtop5" style="display: none;">
                                            Debe ingresar la Superficie.
                                        </div>
                                        <div id="Val_Superficie" class="alert alert-small alert-danger mbottom0 mtop5" style="display: none;">
                                            Si selecciona destino playa carga / descarga la superficie debe ser mayor o igual a 30 mts.
                                        </div>
                                       </div>
                                  
                                  </div>

                                    <div class="form-group">
                                       
                                       <label  class="control-label col-sm-2" >Planta (*):</label>
                                          <div class="col-sm-3"  >
                                      <asp:DropDownList ID="ddlPlanta" runat="server" CssClass="form-control" width="200px" Height="30px"></asp:DropDownList>
                                        <div id="Req_Planta" class="alert alert-small alert-danger mbottom0 mtop5" style="display: none;">
                                            Debe ingresar la Planta.
                                        </div>
                                   </div>
                                      
                                      <label  class="control-label col-sm-2" >Paredes (*):</label>
                                          <div class="col-sm-3"  >
                                      <asp:TextBox ID="txtParedes" runat="server" CssClass="form-control" width="200px" Height="30px" MaxLength="50"></asp:TextBox>
                                        <div id="Req_Paredes" class="alert alert-small alert-danger mbottom0 mtop5" style="display: none;">
                                            Debe ingresar las Paredes.
                                        </div>
                                   </div>

                                  </div>

                                    <div class="form-group ">
                                       
                                       <label  class="control-label col-sm-2">Techos (*):</label>
                                          <div class="col-sm-3"  >
                                      <asp:TextBox ID="txtTechos" runat="server" CssClass="form-control" width="200px" Height="30px" MaxLength="50"></asp:TextBox>
                                        <div id="Req_Techos" class="alert alert-small alert-danger mbottom0 mtop5" style="display: none;">
                                            Debe ingresar los Techos.
                                        </div>
                                   </div>
                                      
                                         <label  class="control-label col-sm-2" >Pisos (*):</label>
                                          <div class="col-sm-3"  >
                                      <asp:TextBox ID="txtPisos" runat="server" CssClass="form-control" width="200px" Height="30px" MaxLength="50"></asp:TextBox>
                                        <div id="Req_Pisos" class="alert alert-small alert-danger mbottom0 mtop5" style="display: none;">
                                            Debe ingresar los Pisos.
                                        </div>
                                   </div>
                                  
                                  </div>

                                   <div class="form-group">
                                       <label  class="control-label col-sm-2">Ventilación (*):</label>
                                          <div class="col-sm-3"  >
                                    <asp:DropDownList ID="ddlVentilacion" runat="server" CssClass="form-control" width="200px" Height="30px"></asp:DropDownList>
                                        <div id="Req_Ventilacion" class="alert alert-small alert-danger mbottom0 mtop5" style="display: none;">
                                            Debe ingresar la Ventilación.
                                        </div>

                                   </div>
                                      
                                         <label  class="control-label col-sm-2" >Iluminación (*):</label>
                                          <div class="col-sm-3"  >
                                     <asp:DropDownList ID="ddlIluminacion" runat="server" CssClass="form-control" width="200px" Height="30px"></asp:DropDownList>
                                        <div id="Req_Iluminacion" class="alert alert-small alert-danger mbottom0 mtop5" style="display: none;">
                                            Debe ingresar la Iluminación.
                                        </div>

                                   </div>
                                  
                                  </div>
                                    
                                   <div class="form-group ">
                                       
                                       <label  class="control-label col-sm-2">Frisos (*):</label>
                                          <div class="col-sm-3"  >
                                      <asp:TextBox ID="txtFrisos" runat="server" CssClass="form-control" width="200px" Height="30px" MaxLength="50"></asp:TextBox>
                                        <div id="Req_Frisos" class="alert alert-small alert-danger mbottom0 mtop5" style="display: none;">
                                            Debe ingresar los Frisos.
                                        </div>
                                   </div>
                                     </div> 
                                   <div class="form-group ">  
                                   <label  class="control-label col-sm-2" >Observaciones:</label>
                                          <div class="col-sm-3"  >
                                      <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control" width="450px" Height="100px" TextMode="MultiLine" MaxLength="4000"></asp:TextBox>
                                   </div>
                                    </div>
                                  </div>
                            
                                </div>
                             <br />
                               <div class="pull-left mleft10">
                                   <label> Los campos marcados con "(*)" son obligatorios.</label>
                               </div>
                             <div class="pull-right mright10">
                                 <asp:LinkButton ID="btnCerrar" runat="server" CssClass="btn btn-default" OnClick="btnCerrar_Click">
                                      <i class="imoon-close"></i>
                                   <span class="text">Cerrar</span>
                             </asp:LinkButton>
                            </div>
                                <div class="pull-right mright10">
                                     <asp:LinkButton ID="btnAgregar" runat="server" CssClass="btn btn-primary" OnClick="btnAgregar_Click" OnClientClick="return validar();" >
                                                    <i class="imoon-white imoon-plus"></i>
                                                    <span class="text">Agregar</span>
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
            init_JS_updConformacionLocal();
        });

        function init_JS_updConformacionLocal() {
            vSeparadorDecimal = $("#<%: hid_DecimalSeparatorCL.ClientID %>").attr("value");

            eval("$('#<%: txtLargo.ClientID %>').autoNumeric({ aSep: '', aDec: '" + vSeparadorDecimal + "', mDec: '2',vMax: '99999.99'})");
            eval("$('#<%: txtAncho.ClientID %>').autoNumeric({ aSep: '', aDec: '" + vSeparadorDecimal + "', mDec: '2',vMax: '999.99'})");
            eval("$('#<%: txtAlto.ClientID %>').autoNumeric({ aSep: '', aDec: '" + vSeparadorDecimal + "', mDec: '2',vMax: '99999.99'})");
            eval("$('#<%: txtSuperficie.ClientID %>').autoNumeric({ aSep: '', aDec: '" + vSeparadorDecimal + "', mDec: '2',vMax: '99999999.99'})");

            $("#<%: txtLargo.ClientID %>").on("keyup", function (e) {
                $("#Req_Largo").hide();
                cambioTipoSuperficie();
            });
            $("#<%: txtAncho.ClientID %>").on("keyup", function (e) {
                $("#Req_Ancho").hide();
                cambioTipoSuperficie();
            });
            $("#<%: txtDetalle.ClientID %>").on("keyup", function (e) {
                $("#Req_Detalle").hide();
            });
            $("#<%: txtAlto.ClientID %>").on("keyup", function (e) {
                $("#Req_Alto").hide();
            });
            $("#<%: txtSuperficie.ClientID %>").on("keyup", function (e) {
                $("#Req_Superficie").hide();
                $("#Val_Superficie").hide();
            });
            $("#<%: txtParedes.ClientID %>").on("keyup", function (e) {
                $("#Req_Paredes").hide();
            });
            $("#<%: txtTechos.ClientID %>").on("keyup", function (e) {
                $("#Req_Techos").hide();
            });
            $("#<%: txtPisos.ClientID %>").on("keyup", function (e) {
                $("#Req_Pisos").hide();
            });
            $("#<%: txtFrisos.ClientID %>").on("keyup", function (e) {
                $("#Req_Frisos").hide();
            });

            $("#<%: ddlTipoSuperficie.ClientID %>").on("change", function (e) {
                $("#Req_TipoSuperficie").hide();
                $("#Req_Superficie").hide();
                $("#Val_Superficie").hide();
                cambioTipoSuperficie();
            });
            cambioTipoSuperficie();
            $("#<%: ddlPlanta.ClientID %>").on("change", function (e) {
                $("#Req_Planta").hide();
            });
            $("#<%: ddlVentilacion.ClientID %>").on("change", function (e) {
                $("#Req_Ventilacion").hide();
                cambioTipoSuperficie();
            });
            $("#<%: ddlIluminacion.ClientID %>").on("change", function (e) {
                $("#Req_Iluminacion").hide();
                cambioTipoSuperficie();
            });
        }

        function cambioTipoSuperficie() {
            var valor = $("#<%: ddlTipoSuperficie.ClientID %>").val();
            var vSeparadorDecimal= $("#<%: hid_DecimalSeparatorCL.ClientID %>").val();
            if (valor == 1) {//Regular
                var sup = stringToFloat($("#<%: txtAncho.ClientID %>").val(), vSeparadorDecimal) * stringToFloat($("#<%: txtLargo.ClientID %>").val(), vSeparadorDecimal);
                $("#<%: txtSuperficie.ClientID %>").val(sup.toFixed(2).toString().replace(".", vSeparadorDecimal));
                $("#<%: txtSuperficie.ClientID %>").prop("disabled", true);
            } else
                $("#<%: txtSuperficie.ClientID %>").prop("disabled", false);
            return false;
        }

        function validar() {
            var ret = true;
            $("#Req_Detalle").hide();
            $("#Req_Largo").hide();
            $("#Req_Ancho").hide();
            $("#Req_Alto").hide();
            $("#Req_TipoSuperficie").hide();
            $("#Req_Superficie").hide();
            $("#Val_Superficie").hide();
            $("#Req_Planta").hide();
            $("#Req_Paredes").hide();
            $("#Req_Techos").hide();
            $("#Req_Pisos").hide();
            $("#Req_Ventilacion").hide();
            $("#Req_Iluminacion").hide();
            $("#Req_Frisos").hide();
            //debugger;
            //if ($.trim($("#<%: txtDetalle.ClientID %>").val()).length == 0) {
            //    $("#Req_Detalle").css("display", "inline-block");
            //    ret = false;
            //}
            if ($.trim($("#<%: txtLargo.ClientID %>").val()).length == 0) {
                $("#Req_Largo").css("display", "inline-block");
                ret = false;
            }
            if ($.trim($("#<%: txtAncho.ClientID %>").val()).length == 0) {
                $("#Req_Ancho").css("display", "inline-block");
                ret = false;
            }
            if ($.trim($("#<%: txtAlto.ClientID %>").val()).length == 0) {
                $("#Req_Alto").css("display", "inline-block");
                ret = false;
            }
            if ($("#<%: ddlTipoSuperficie.ClientID %>").val() == 0) {
                $("#Req_TipoSuperficie").css("display", "inline-block");
                ret = false;
            }
            if ($.trim($("#<%: txtSuperficie.ClientID %>").val()).length == 0) {
                $("#Req_Superficie").css("display", "inline-block");
                ret = false;
            }
            if ($("#<%: ddlPlanta.ClientID %>").val() == 0) {
                $("#Req_Planta").css("display", "inline-block");
                ret = false;
            }
            if ($.trim($("#<%: txtParedes.ClientID %>").val()).length == 0) {
                $("#Req_Paredes").css("display", "inline-block");
                ret = false;
            }
            if ($.trim($("#<%: txtTechos.ClientID %>").val()).length == 0) {
                $("#Req_Techos").css("display", "inline-block");
                ret = false;
            }
            if ($.trim($("#<%: txtTechos.ClientID %>").val()).length == 0) {
                $("#Req_Pisos").css("display", "inline-block");
                ret = false;
            }

            if ($("#<%: ddlVentilacion.ClientID %>").val() == -1) {
                $("#Req_Ventilacion").css("display", "inline-block");
                ret = false;
            }
            if ($("#<%: ddlIluminacion.ClientID %>").val() == 0) {
                $("#Req_Iluminacion").css("display", "inline-block");
                ret = false;
            }
            if ($.trim($("#<%: txtFrisos.ClientID %>").val()).length == 0) {
                $("#Req_Frisos").css("display", "inline-block");
                ret = false;
            }

            var idDestinoCargaDescarga = '<%= (int)StaticClass.Constantes.TipoDestino.PlayaCargaDescarga %>';

            if (($("#<%: ddlDestino.ClientID %>").val() == idDestinoCargaDescarga) && parseFloat($("#<%: txtSuperficie.ClientID %>").val()) < 30) {
                $("#Val_Superficie").css("display", "inline-block");
                ret = false;
            }
            return ret;
        }

    </script>

</div>


