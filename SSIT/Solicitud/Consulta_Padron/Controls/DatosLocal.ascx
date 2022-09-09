<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatosLocal.ascx.cs" Inherits=" SSIT.Solicitud.Consulta_Padron.Controls.DatosLocal" %>

<%--Datos Local--%>
<asp:Panel ID="pnlDatosLocal" runat="server">
    <br />
    <%--Grilla de Datos Local--%>
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
                                        <asp:TextBox ID="txtSuperficieCubierta" runat="server" Text="0,00" Enabled="false"  Width="100px" CssClass="form-control text-right"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:Label runat="server" class="col-sm-6 control-label">Superficie descubierta:</asp:Label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtSuperficieDescubierta" runat="server" Text="0,00" Enabled="false" Width="100px" CssClass="form-control text-right"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:Label runat="server" class="col-sm-6 control-label">Superficie total:</asp:Label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtSuperficieTotal" runat="server" Text="0,00" Width="100px" Enabled="false" CssClass="form-control text-right" ></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:Label runat="server" class="col-sm-6 control-label">Dimensión del Frente:</asp:Label>
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
                                <asp:Label runat="server" class="control-label col-sm-6">Frente:</asp:Label>
                                <div class="col-sm-6">
                                   <asp:TextBox ID="txtFrente" runat="server" MaxLength="10" Width="100px" Enabled="false" CssClass="form-control text-right"></asp:TextBox>
                             
                                </div>
                            </div>
                            <div class="form-group" style="text-align: center">
                                <asp:Label runat="server"  class="control-label col-sm-6">Fondo:</asp:Label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtFondo" runat="server" MaxLength="10" Width="100px" Enabled="false" CssClass="form-control text-right"></asp:TextBox>  
                                </div>

                            </div>
                            <div class="form-group" style="text-align: center">
                                <asp:Label runat="server"  class="control-label col-sm-6">Lateral Izquierdo:</asp:Label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtLatIzq" runat="server" MaxLength="10" Width="100px" Enabled="false" CssClass="form-control text-right"></asp:TextBox>

                                </div>

                            </div>
                            <div class="form-group" style="text-align: center">

                                <asp:Label runat="server"  class="control-label col-sm-6">Lateral Derecho:</asp:Label>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtLatDer" runat="server" MaxLength="10" Width="100px" Enabled="false" CssClass="form-control text-right"></asp:TextBox>
           
                                </div>
 
                            </div>
                           </div>
                          </div>
                        </div>  
    <br />
    <br />
    <%--Grilla de caracteristicas de local--%>
    <div style="margin-left: 40px">
        <strong>Características del Local</strong>
    </div>
                      
           <div class="form-horizontal" style="margin-left:0px">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="control-label col-sm-4" Enabled="false" Style="margin-left: -99px">Posee lugar de carga y descarga:</asp:Label>
                                <div class="col-sm-3" style="width: 100px; margin-left: 10px; margin-top:10px">
                                    <asp:RadioButton ID="opt1_si" runat="server" GroupName="LugarCargaDescarga" Text="Sí" />
                                    <asp:Label runat="server"></asp:Label>
                                    <asp:RadioButton ID="opt1_no" runat="server" GroupName="LugarCargaDescarga" Text="No" />
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" Enabled="false" CssClass="control-label col-sm-3">Posee estacionamiento:</asp:Label>
                                <div class="col-sm-3" style="width: 100px; margin-left: 10px; margin-top:10px"">
                                    <asp:RadioButton ID="opt2_si" runat="server" GroupName="Estacionamiento" Text="Sí" />
                                    <asp:Label runat="server"></asp:Label>
                                    <asp:RadioButton ID="opt2_no" runat="server" GroupName="Estacionamiento" Text="No" />
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" Enabled="false" CssClass="control-label col-sm-3">Red de tránsito pesado:</asp:Label>
                                <div class="col-sm-3" style="width: 100px; margin-left: 10px; margin-top:10px"">
                                    <asp:RadioButton ID="opt3_si" runat="server" GroupName="RedTansitoPesado" Text="Sí" />
                                    <asp:Label runat="server"></asp:Label>
                                    <asp:RadioButton ID="opt3_no" runat="server" GroupName="RedTansitoPesado" Text="No" />
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" Enabled="false" CssClass="control-label col-sm-3">Sobre Avenida:</asp:Label>
                                <div class="col-sm-3" style="width: 100px; margin-left: 10px; margin-top:10px"">
                                    <asp:RadioButton ID="opt4_si" runat="server" GroupName="SobreAvenida" Text="Sí" />
                                    <asp:Label runat="server"></asp:Label>
                                    <asp:RadioButton ID="opt4_no" runat="server" GroupName="SobreAvenida" Text="No" />
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" Enabled="false" CssClass="control-label col-sm-3"> Cantidad de operarios:</asp:Label>
                                <div class="col-sm-3" style="margin-left: -5px">
                                    <asp:TextBox ID="txtCantOperarios" Enabled="false" CssClass="form-control" runat="server" Width="100px" MaxLength="5"></asp:TextBox>
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
        <div style="margin-right: 20px; margin-top: 10px">
            <div class="form-horizontal">
                <div class="form-group">
                    <div style="margin-left: -240px">
                        <asp:Label ID="Label6" runat="server" CssClass="control-label col-sm-6"> Los mismos se encuentran:</asp:Label>
                    </div>
                    <div class="col-sm-2" style="width: 275px;  margin-top:10px; " >
                        <asp:RadioButton ID="opt5_dentro" Checked="true" runat="server" GroupName="Sanitarios" onclick="objVisibility('tblDistanciaSanitarios_dl','hide'); ocultarReq();" Text="Dentro del Local" Style="font-family:'Open Sans'" />
                        <asp:RadioButton ID="opt5_fuera" runat="server" GroupName="Sanitarios" onclick="objVisibility('tblDistanciaSanitarios_dl','show');" Text="Fuera del Local" Font-Bold="false"/>
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
                    <asp:Label ID="Label7" runat="server" CssClass="control-label col-sm-9">Cantidad de artefactos sanitarios:</asp:Label>
                    <div class="col-sm-3" style="margin-left: -5px">
                        <asp:TextBox ID="txtCantidadArtefactosSanitarios" CssClass="form-control" runat="server" MaxLength="4" Width="70px" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="Label8" runat="server" CssClass="control-label col-sm-9">Superficie de Sanitarios:</asp:Label>
                    <div class="col-sm-3" style="margin-left: -5px">
                        <asp:TextBox ID="txtSuperficieSanitarios"  CssClass="form-control" runat="server" MaxLength="8" Width="70px" Enabled="false"></asp:TextBox>
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
                  
                    <%--Materiales Empleados en:--%>
                    <div class="titulo-5" style="margin-left:50px">
                        <b>Materiales expresados en...</b>
                    </div>
                    <table style="margin-top: 10px; margin-left:50px">
                        <tr>
                            <td class="celda">
                                <asp:Label runat="server">
                                    Pisos:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPisos" Enabled="false" CssClass="form-control" runat="server" TextMode="MultiLine" MaxLength="200" Height="63px"
                                    Width="750px" Style="margin-top: 5px"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td class="celda">
                                <asp:Label runat="server">
                                    Paredes:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtParedes" Enabled="false" CssClass="form-control" runat="server" TextMode="MultiLine" MaxLength="200"
                                    Height="63px" Width="750px" Style="margin-top: 5px"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td class="celda">
                                <asp:Label runat="server">
                                    Techos:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTechos" Enabled="false" CssClass="form-control" runat="server" TextMode="MultiLine" MaxLength="200" Height="63px"
                                    Width="750px" Style="margin-top: 5px"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td class="celda">
                                <asp:Label runat="server">
                                    Revestimientos:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRevestimientos" Enabled="false" CssClass="form-control" runat="server" TextMode="MultiLine" MaxLength="200"
                                    Height="63px" Width="750px" Style="margin-top: 5px"></asp:TextBox>

                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <br />
</asp:Panel>
<script type="text/javascript">

    $(document).ready(function () {
        
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
