<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" MaintainScrollPositionOnPostback="True" MasterPageFile="~/Site.Master" CodeBehind="ABMProfesionales.aspx.cs" Inherits="ConsejosProfesionales.ABM.ABMProfesionales" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <%: Scripts.Render("~/bundles/autoNumeric") %>
    <%: Scripts.Render("~/bundles/select2") %>
    <%: Styles.Render("~/bundles/select2Css") %>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                     <asp:HiddenField ID="hid_userid" runat="server" />
                                <asp:HiddenField ID="hid_id_profesional" runat="server" />
                                
    <asp:Panel ID="pnlContenido" runat="server" CssClass="box-contenido" BackColor="White">
        <div class="row text-center">
            <h2>Profesionales - ABM</h2>

        </div>

                
        <br />
        <br />
        <div id="Divprofesionales" class="box-panel">

            <div style="margin: 20px; margin-top: -5px">
                <div style="color: #377bb5">
                    <h4><i class="imoon imoon-user4" style="margin-right: 10px"></i>Carga de Profesionales</h4>
                    <hr />
                </div>
            </div>

            <div>
                <%-- Busqueda1 --%>
                <div class="row">
                    <div class="col-sm-12 col-md-12">
                        <asp:UpdatePanel ID="updpnlBuscar" runat="server" UpdateMode="Conditional">

                            <ContentTemplate>

                                <asp:Panel ID="Panel1" runat="server" CssClass="form-horizontal">

                                      <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Guardar" EnableClientScript="true"
                                            DisplayMode="BulletList" ForeColor="Red" ShowSummary="true" HeaderText="Debe ingresar el/los siguiente/s campo/s o bien corregir los errores indicados:" />

                                    <asp:HiddenField ID="id_profesional" runat="server" />
                                    <div class="form-group">
                                        <label class="control-label col-sm-2">Consejo (*):</label>
                                        <div class="col-sm-6">
                                            <asp:DropDownList ID="ddlConsejo" runat="server" CssClass="form-control"></asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Consejo"
                                                ControlToValidate="ddlConsejo" InitialValue="0" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                        </div>

                                        <%--Nro de Matrícula--%>
                                    </div>


                                    <%--Apellido--%>
                                    <div class="form-group">
                                        <label class="control-label col-sm-2">Apellido (*):</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtApellido" runat="server" Width="100%" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Apellido"
                                                ControlToValidate="txtApellido" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                        </div>

                                        <%--Nombres--%>
                                        <label class="control-label col-sm-2">Nombres (*):</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtNombre" runat="server" Width="100%" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Nombre/s"
                                                ControlToValidate="txtNombre" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <%--Tipo y Número de Documento--%>
                                    <div class="form-group">
                                        <label class="control-label col-sm-2">Tipo y Nº de Doc. (*):</label>
                                        <div class="col-sm-3">
                                            <div class="form-inline">
                                                <asp:DropDownList ID="ddlTipoDoc" runat="server" Width="125px" CssClass="form-control"></asp:DropDownList>

                                                <asp:TextBox ID="txtNroDoc" runat="server" Width="130px" MaxLength="9" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Tipo de Documento"
                                                    ControlToValidate="ddlTipoDoc" InitialValue="0" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Número de Documento"
                                                    ControlToValidate="txtNroDoc" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <%-- Matricula --%>
                                        <label class="control-label col-sm-2">Nro. Matrícula (*):</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtMatricula" runat="server" MaxLength="8" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Número de Matrícula"
                                                ControlToValidate="txtMatricula" ValidationGroup="Guardar" Display="Dynamic">*</asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CusValMatriculaUnique" runat="server" Display="Dynamic" ControlToValidate="txtMatricula"
                                                ValidationGroup="Guardar" OnServerValidate="CusValMatriculaUnique_ServerValidate">
                                            </asp:CustomValidator>
                                        </div>
                                    </div>

                                    <%--Calle--%>
                                    <div class="form-group">
                                        <label class="control-label col-sm-2">Calle:</label>
                                        <div class="col-sm-8">
                                            <div class=" form-inline">
                                                <asp:TextBox ID="txtCalle" runat="server" Width="250px" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                                <asp:Label ID="Label8" runat="server" CssClass=" mleft30">Nro:</asp:Label>
                                                <asp:TextBox ID="txtNroPuerta" runat="server" Width="60px" MaxLength="6" CssClass="form-control"></asp:TextBox>
                                                <asp:Label ID="Label9" runat="server" CssClass="  mleft30">Piso:</asp:Label>
                                                <asp:TextBox ID="txtPiso" runat="server" Width="50px" MaxLength="5" CssClass="form-control"></asp:TextBox>
                                                <asp:Label ID="Label10" runat="server" CssClass="  mleft30">Depto:</asp:Label>
                                                <asp:TextBox ID="txtDepto" runat="server" Width="50px" MaxLength="5" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <%--Provincia--%>

                                        <label class="control-label col-sm-2">Provincia (*):</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlProvincia" runat="server" CssClass="form-control"></asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Provincia"
                                                ControlToValidate="ddlProvincia" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                        </div>
                                        <%--Localidad--%>
                                        <label class="control-label col-sm-2">Localidad (*):</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtLocalidad" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Localidad"
                                                ControlToValidate="txtLocalidad" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                        </div>

                                    </div>


                                    <%--Email--%>
                                    <div class="form-group">
                                        <label class="control-label col-sm-2">E-mail:</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtEmail" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>

                                            <asp:RegularExpressionValidator ID="EmailRegEx" runat="server" ControlToValidate="txtEmail" Display="Dynamic"
                                                ErrorMessage="E-mail no tiene un formato válido. Ej: nombre@servidor.com" SetFocusOnError="True" CssClass="error-label"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Guardar"></asp:RegularExpressionValidator>
                                        </div>


                                        <%--Sms--%>
                                        <label class="control-label col-sm-2">Sms:</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtSms" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <%--Teléfono--%>
                                    <div class="form-group">
                                        <label class="control-label col-sm-2">Tel&eacute;fono:</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtTelefono" runat="server" MaxLength="100" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <%--CUIT--%>
                                        <label id="Label15" class="control-label col-sm-2">CUIL (*):</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtCuit" runat="server" MaxLength="13" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="C.U.I.T."
                                                ControlToValidate="txtCuit" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="CuitRegEx" runat="server" ControlToValidate="txtCuit"
                                                ErrorMessage="El Nº de CUIT/CUIL no tiene un formato válido. Ej: 20-25006281-9" ValidationExpression="\d{2}-\d{8}-\d{1}"
                                                ValidationGroup="Guardar">*</asp:RegularExpressionValidator>

                                            <asp:CustomValidator ID="CuitCustomValidator" runat="server" ClientValidationFunction="ValidarCuit"
                                                ControlToValidate="txtCuit" ErrorMessage="El CUIT/CUIL ingresado es inválido" ValidationGroup="Guardar">*</asp:CustomValidator>

                                            <asp:CustomValidator ID="CuitCustomValidator2" runat="server" ClientValidationFunction="ValidarDniCuit"
                                                ControlToValidate="txtCuit" ErrorMessage="El CUIT/CUIL ingresado no se corresponde con el DNI ingresado."
                                                ValidationGroup="Guardar">*</asp:CustomValidator>
                                        </div>

                                    </div>

                                    <%--Ingresos Brutos--%>
                                    <div class="form-group">

                                        <label id="Label16" runat="server" class="control-label col-sm-2">Nº Ingresos Brutos:</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtIngresosBrutos" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <label id="lblObservaciones" runat="server" class="control-label col-sm-2">Observaciones:</label>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtObservaciones" TextMode="MultiLine" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <%-- Matrícula y Categoría --%>

                                        <label id="lnlMatriculaMetrogas" runat="server" class="control-label col-sm-2">Matrícula gasista:</label>

                                        <div class="col-sm-6">
                                            <div class="form-inline">
                                                <asp:TextBox ID="txtMatriculaMetrogas" runat="server" Width="150px" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                                <asp:Label ID="lblCategoria" runat="server" CssClass="form-label">Categoría:</asp:Label>
                                                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value=""></asp:ListItem>
                                                    <asp:ListItem Value="1">1º</asp:ListItem>
                                                    <asp:ListItem Value="2">2º</asp:ListItem>
                                                    <asp:ListItem Value="3">3º</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <%--Inhibido--%>
                                    <div class="form-group">
                                        <asp:Label ID="lblInhibido" runat="server" CssClass="control-label col-sm-2">¿Está inhibido?:</asp:Label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlInhibido" runat="server" Width="80px" CssClass="form-control">
                                                <asp:ListItem Text="Sí" Value="1"></asp:ListItem>
                                                <asp:ListItem Selected="True" Text="No" Value="2"></asp:ListItem>
                                            </asp:DropDownList>

                                        </div>

                                        <%--Baja Logica--%>


                                        <asp:Panel ID="pnlBajaLogicaLabel" runat="server">
                                            <asp:Label ID="lblBajaLogica" runat="server" Text="¿Está dado de Baja?:" CssClass="control-label col-sm-2"></asp:Label>
                                        </asp:Panel>
                                        <div class="col-sm-3">

                                            <asp:Panel ID="pnlBajaLogicaDato" runat="server">
                                                <asp:DropDownList ID="ddlBajaLogica" runat="server" Width="80px" CssClass="form-control">
                                                    <asp:ListItem Text="No" Value="false"></asp:ListItem>
                                                    <asp:ListItem Text="Si" Value="true"></asp:ListItem>
                                                </asp:DropDownList>
                                            </asp:Panel>

                                        </div>
                                    </div>
                                    <%--Panel de Botones--%>

                                    <asp:UpdatePanel ID="updBotonesAccion" runat="server">
                                        <ContentTemplate>
                                            <asp:Panel ID="pnlBotones" runat="server">

                                                <asp:UpdatePanel ID="updBotonGuardar" runat="server">
                                                    <ContentTemplate>

                                                        <div class="row ptop10 pright15">
                                                            <div class="cols-sm-12 text-right">
                                                                
                                                                <asp:LinkButton ID="cmdGuardar" runat="server" OnClick="cmdGuardar_Click" ValidationGroup="Guardar" OnClientClick="$('#pnlError').hide();" CssClass="btn btn-primary ">
                                                                    <i class="imoon imoon-ok"></i>
                                                                    <span class="text">Guardar</span>
                                                                </asp:LinkButton>
                                                                <asp:LinkButton ID="cmdDatosUsuario"  runat="server" OnClick="cmdDatosUsuario_Click" Enabled="false" CssClass="btn btn-default disabled">
                                                                    <i class="imoon imoon-user"></i>
                                                                    <span class="text">Datos de usuario</span>
                                                                </asp:LinkButton>

                                                                <asp:LinkButton ID="cmdVolver" runat="server" OnClick="cmdVolver_Click" CssClass="btn btn-default">
                                                                    <i class="imoon imoon-refresh"></i>
                                                                    <span class="text">Nueva Búsqueda</span>
                                                                </asp:LinkButton>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>


                                    </div>
                       </div>

                                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="updBotonGuardar" runat="server" DisplayAfter="0">
                                        <ProgressTemplate>

                                            <div style="padding: 15px">
                                                <img src="../Common/Images/Controles/Loading24x24.gif" alt="" />
                                                Actualizando...
                                            </div>

                                        </ProgressTemplate>
                                    </asp:UpdateProgress>



                                    <div class="form-horizontal form-group">


                                      
                                        <asp:UpdatePanel ID="updMensajes" runat="server">
                                            <ContentTemplate>

                                                <%--Mensaje Error--%>
                                                <asp:Panel ID="pnlError" runat="server" Visible="false">
                                                    <asp:Table ID="Table1" runat="server" HorizontalAlign="Center" CssClass="box-borde-azul"
                                                        CellSpacing="5">
                                                        <asp:TableRow>
                                                            <asp:TableCell Style="background-color: Transparent">
                                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Common/Images/Controles/error32x32.png" />
                                                            </asp:TableCell>
                                                            <asp:TableCell Style="background-color: Transparent">
                                                                <asp:Label ID="lblError" runat="server" CssClass="error-label"></asp:Label>
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>
                                                </asp:Panel>

                                                <%--Mensaje OK--%>

                                                <asp:Panel ID="pnlInfo" runat="server" CssClass="modal fade">
                                                    <div class="modal-dialog">
                                                        <div class="modal-content">
                                                            <div class="modal-header">
                                                                <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Cerrar</span></button>
                                                            </div>
                                                            <div class="modal-body">
                                                                El registro ha sido guardado correctamente.
                                                            </div>
                                                            <div class="modal-footer mleft20 mright20">
                                                                <asp:HyperLink ID="btnCerrarAgregarProfesional"  onclick="return cerrarModal();"  runat="server" Text="Cerrar" CssClass="btn btn-default" ></asp:HyperLink>
                                                                <%--<button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>--%>
                                                            </div>
                                                </asp:Panel>
                                                </div>

                                                <%--Panel de Generación del Usuario--%>
                                                <asp:Panel ID="pnlGenerarUsuario" runat="server" Visible="false">
                                                    <div style="padding: 20px 5px 5px 5px; text-align: center">
                                                        <asp:HyperLink ID="btnGenerarUsuario1" runat="server" Text="Generar Usuario" CssClass="btnNew" Width="130px"></asp:HyperLink>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>
                                    </table>
                                </asp:Panel>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <%--Paneles Popups---------------------------------------------------------------------------------------------------------------%>
                    <%--modal--%>
                    <div class="modal fade" id="pnlMensajes">
                        <div class="modal-dialog">
                            <asp:Panel ID="pnlMsjError" runat="server" DefaultButton="btnBlanqueo">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                                        <h4 class="modal-title" style="margin-top: -8px">
                                            <asp:Label runat="server" ID="Label6">Error</asp:Label>
                                        </h4>
                                    </div>
                                    <div class="modal-body">
                                        <asp:UpdatePanel ID="updMsjError" runat="server">
                                            <ContentTemplate>
                                                <asp:Panel ID="pnlInformacion" runat="server" CssClass="modalPopup" DefaultButton="btnAceptarInformacion">
                                                    <table cellpadding="7" style="width: 100%">
                                                        <div class="form-horizontal form-group">
                                                            <tr>
                                                                <td style="width: 80px">
                                                                    <asp:Image ID="imgmpeInfo" runat="server" ImageUrl="~/Common/Images/Controles/error64x64.png" />
                                                                    <asp:UpdatePanel ID="updmpeInfo" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:Label ID="lblmpeInfo" runat="server" Style="color: Black"></asp:Label>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>

                                                        </div>
                                                        <div class="form-horizontal form-group">
                                                            <tr>
                                                                <td colspan="2" style="text-align: center">
                                                                    <asp:Button ID="btnAceptarInformacion" runat="server" CssClass="btnOK" OnClientClick="return hideMensajes();" Text="Aceptar" Width="100px" />
                                                                </td>
                                                            </tr>
                                                        </div>
                                                    </table>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <%--fin modal--%>

                    <%--modal--%>
                    <div class="modal fade" id="pnlMensajesSuccess">
                        <div class="modal-dialog">
                            <asp:Panel ID="Panel3" runat="server" DefaultButton="btnBlanqueo">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                                        <h4 class="modal-title" style="margin-top: -8px">
                                            <asp:Label runat="server" ID="Label7">Notificación</asp:Label>
                                        </h4>
                                    </div>
                                    <div class="modal-body">
                                        <asp:UpdatePanel ID="udpMensajesSucess" runat="server">
                                            <ContentTemplate>
                                                    <table cellpadding="7" style="width: 100%">
                                                        <div class="form-horizontal form-group">
                                                            <tr>
                                                                <td style="width: 80px">
                                                                    <div class="row">
                                                                        <div class="col-sm-3">
                                                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Common/Images/Controles/info64x64.png" />
                                                                        </div>
                                                                        <div class="col-sm-9" style="vertical-align:middle">
                                                                            <asp:UpdatePanel ID="UpdpnlSuccess" runat="server">
                                                                                <ContentTemplate>
                                                                                    <asp:Label ID="lblSuccess" runat="server" Style="color: Black"></asp:Label>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </div>

                                                                    </div>
                                                                    
                                                                    
                                                                </td>
                                                            </tr>

                                                        </div>
                                                        <div class="form-horizontal form-group">
                                                            <tr>
                                                                <td colspan="2" style="text-align: center">
                                                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-default" OnClientClick="hideMensajes()"  Text="Aceptar" Width="100px" />
                                                                </td>
                                                            </tr>
                                                        </div>
                                                    </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <%--fin modal--%>
    

    
    <%--modal--%> 
    <div class="modal fade" id="pnlDatosUsuarioClient">
        <div class="modal-dialog">
            <asp:Panel ID="pnlDatosUsuario" runat="server" DefaultButton="btnAceptarDatosUsuario">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title" style="margin-top: -8px">
                            <asp:Label runat="server" ID="lblUsuario">Generar Usuario</asp:Label>
                        </h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="updDatosUsuario" runat="server">
                            <ContentTemplate>

                                <script lang="javascript" type="text/javascript">

                                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

                                    function EndRequestHandler(sender, args) {
                                        mostrarToolTips();
                                    }

                                    function mostrarToolTips() {
                                        $("#cblPerfil tbody tr td span").each(function () {
                                            if ($(this).attr("title") != undefined) {
                                                $(this).tipTip({ defaultPosition: "left", maxWidth: "500px", delay: "0" });
                                            }
                                        })
                                    }

                                </script>
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label ID="txtApellidoYNombre" runat="server" Text="Apellido y Nombres:" CssClass="control-label col-sm-4 "></asp:Label>

                                        <asp:Label ID="lblProfesional_datos" runat="server" Font-Bold="true" CssClass="control-label col-sm-5 col-sm-pull-1"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label ID="lblNombreUsuario_datos" runat="server" Text="Nombre de Usuario:" CssClass="control-label col-sm-4"></asp:Label>
                                        <asp:TextBox ID="txtusername_datos" runat="server" MaxLength="25" Width="150px" CssClass="form-control col-sm-4 "></asp:TextBox>

                                    </div>
                                </div>
                                <div>
                                    <asp:RequiredFieldValidator ID="rfv_txtusername_datos" runat="server"
                                        ControlToValidate="txtusername_datos" Display="Dynamic"
                                        ErrorMessage="Debe ingresar el Nombre de Usuario." SetFocusOnError="True"
                                        ValidationGroup="DatosUsuario" CssClass="error-label"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label ID="Label4" runat="server" Text="E-Mail:" CssClass="control-label col-sm-4"></asp:Label>
                                        <asp:TextBox ID="txtEmail_datos" runat="server" MaxLength="100" Width="250px" CssClass="form-control col-sm-5" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div>
                                    <asp:RequiredFieldValidator ID="RepEmail" runat="server" ControlToValidate="txtEmail_datos" Display="Dynamic"
                                        ErrorMessage="Debe ingresar la dirección de correo." SetFocusOnError="True"
                                        ValidationGroup="DatosUsuario" CssClass="error-label"></asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail_datos" Display="Dynamic"
                                        ErrorMessage="E-mail no tiene un formato válido. Ej: nombre@servidor.com" SetFocusOnError="True" CssClass="error-label"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="DatosUsuario"></asp:RegularExpressionValidator>
                                </div>
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label ID="lblUsuarioBloqueado" runat="server" Text="¿Usuario Bloqueado?:" CssClass="control-label col-sm-4"></asp:Label>
                                        <style>
                                            .rbl input[type="radio"]
                                                {
                                                   margin-left: 70px;
                                                   margin-right: 1px;
                                                }
                                        </style>
                                        <asp:RadioButtonList ID="RadioUsuarioBloqueado_datos" CssClass="rbl" RepeatColumns="2" RepeatDirection="Horizontal"  runat="server">
                                            <asp:ListItem Text="Si"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="No"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <%--<asp:CheckBox ID="chkUsuarioBloqueado_datos" runat="server" CssClass="control-label col-sm-1" Style="margin-left: -20px" />--%>
                                    </div>
                                </div>
                                <br />

                                <h4 style="margin-bottom: -15px">Perfiles:</h4>
                                <hr />

                                <asp:GridView
                                    ID="grdPerfiles"
                                    runat="server"
                                    AutoGenerateColumns="false"
                                    GridLines="None"
                                    Width="100%"
                                    ShowHeader="false"
                                    ItemType="DataTransferObject.RolesDTO"
                                    OnRowDataBound="grdPerfiles_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hid_id_rol" runat="server" Value='<%# Item.RoleId %>' />
                                                <asp:HiddenField ID="hid_rolname" runat="server" Value='<%# Item.RoleName %>' />
                                                <div class="checkbox mleft40" style="margin: 5px">
                                                    <asp:CheckBox ID="chkPerfil" runat="server" Text='<%# Item.Description %>' Font-Size="15px" CssClass="control-label" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlCalificacion" runat="server" >
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                                <hr />
                                <% if (tieneUserName)
                                    { %>
                                <asp:Panel ID="pnlReenvioClave" runat="server">
                                    <h5>Recupero de Contraseña</h5>
                                    <br />
                                    <br />
                                    <div class="row">
                                        <div class="col-sm-5 col-sm-offset-1">
                                            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-success" runat="server" OnClientClick="return ShowBlanquearClave()">
                                                <i class="imoon imoon-key"></i>
                                                <span class="text">Blanquear Contraseña</span>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:LinkButton ID="reenvioclave" CssClass="btn btn-default" runat="server" OnClick="reenvioclave_Click">
                                                <i class="imoon imoon-key"></i>
                                                <span class="text">Reenviar Contraseña</span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                    <br />
                                    <br />

                                <asp:Panel ID="pnlNotaEmail" runat="server" CssClass="nota">
                                    <b>Nota:</b> Al finalizar el proceso y generar el usuario, <b>la contraseña será enviada por email a la dirección ingresada</b>. Por favor verifique que la misma se encuentre escrita de manera correcta."
                                </asp:Panel>
                                <hr />
                                </asp:Panel>
                                <% } %>
                                <asp:Panel ID="pnlPerfilInhibido" runat="server">
                                    <br />
                                    <center style="font-weight: bold">Inibiciones por perfil para el profesional</center>
                                    <hr />
                                    <asp:GridView ID="gvPerfilInibido" runat="server"
                                        AutoGenerateColumns="false" Width="100%" GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="RoleName" HeaderText="Perfil"
                                                HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="200px" />
                                            <asp:BoundField DataField="fecha_inhibicion" HeaderText="Desde"
                                                DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="fecha_vencimiento" HeaderText="Hasta"
                                                DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="motivo" HeaderText="Motivo" />
                                        </Columns>
                                    </asp:GridView>
                                    <br />
                                </asp:Panel>


                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <asp:UpdatePanel ID="updBotonesDatosUsuario" runat="server">
                            <ContentTemplate>
                                <div style="width: 100%; padding: 5px 25px 5px 0px; text-align: right">
                                    <table border="0">
                                        <tr>
                                            <td style="width: 100%">

                                                <asp:UpdateProgress ID="UpdateProgress6" AssociatedUpdatePanelID="updBotonesDatosUsuario" runat="server" DisplayAfter="0">
                                                    <ProgressTemplate>
                                                        <img src="../Common/Images/Controles/Loading24x24.gif" alt="" />
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>

                                            </td>
                                            <td>

                                                <asp:Button ID="btnAceptarDatosUsuario" runat="server" CssClass="btn btn-primary" Text="Guardar"
                                                    Width="90px" ValidationGroup="DatosUsuario" OnClick="btnAceptarDatosUsuario_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnCancelarDatosUsuario" OnClientClick="hideDatosUsuario()" CssClass="btn btn-default" runat="server" data-dismiss="modal"
                                                    Text="Cancelar" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
    <%--fin modal--%>

      
                    <div id="pnlVisualizador" class="modal fade">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">X</span><span class="sr-only">Close</span></button>
                                </div>
                                <div class="modal-body">
                                    <asp:UpdatePanel ID="UpdPnlSuccess_ok" runat="server">
                                        <ContentTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <div class="row">
                                                            <div class="col-sm-3">
                                                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Common/Images/Controles/info64x64.png" />
                                                            </div>
                                                            <div class="col-sm-9" style="vertical-align: middle">
                                                                <asp:Label ID="lblMensajeActualizacion" runat="server" Style="color: Black"></asp:Label>
                                                            </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center">
                                                        <asp:LinkButton ID="LinkButton2" PostBackUrl="~/ABM/BuscarProfesionales.aspx" CssClass="btn btn-default" runat="server">Aceptar</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
      <%--modal generar usuario--%>
                    <div id="pnlMSJGenerarUsuario" class="modal fade">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" onclick="ocultarMSJGenerarUsuario()" data-dismiss="modal"><span aria-hidden="true">X</span><span class="sr-only">Close</span></button>
                                </div>
                                <div class="modal-body">
                                    <asp:UpdatePanel ID="updMSJGenerarUsuario" runat="server">
                                        <ContentTemplate>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td>
                                                        <div class="row">
                                                            <div class="col-sm-3">
                                                                <asp:Image ID="Image4" runat="server" ImageUrl="~/Common/Images/Controles/info64x64.png" />
                                                            </div>
                                                            <div class="col-sm-9" style="vertical-align: middle">
                                                                <asp:Label ID="Label3" runat="server" Text="El profesional no tiene usuario generado, ¿ desea generarlo ahora?" Style="color: Black"></asp:Label>
                                                            </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center">
                                                        <asp:LinkButton ID="btnGenerarUserNuevo" OnClick="btnGenerarUserNuevo_Click" CssClass="btn btn-default" runat="server">Generar Usuario</asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton4" OnClientClick="ocultarMSJGenerarUsuario()" CssClass="btn btn-default" runat="server">Cerrar</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--fin modal generar usuario--%>  
<%--modal--%> 
    <div class="modal fade" id="pnlBlanquearClave">
        <div class="modal-dialog">
            <asp:Panel ID="pnlBlanquear" runat="server" DefaultButton="btnBlanqueo">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title" style="margin-top: -8px">
                            <asp:Label runat="server" ID="Label5">Blanquear Contraseña</asp:Label>
                        </h4>
                    </div>
                    <div class="modal-body">
                                <asp:Panel runat="server" ID="pnlBlanqueo">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" Text="Contraseña:" CssClass="control-label col-sm-4"></asp:Label>
                                            <div class=" col-sm-8">
                                                <asp:TextBox ID="txtPass1" runat="server" Width="150px" TextMode="Password" CssClass="form-control" ValidationGroup="blanqueo"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" CssClass="text-danger" runat="server"  ValidationGroup="blanqueo"  ErrorMessage="Mínimo ocho caracteres" ControlToValidate="txtPass1" ValidationExpression = "^[\s\S]{8,}$"></asp:RegularExpressionValidator>
                                                <br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" CssClass="text-danger"  runat="server" ValidationGroup="blanqueo" ErrorMessage="Debe ingresar la contraseña" ControlToValidate="txtPass1"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <asp:Label ID="Label2" runat="server" Text="Repita Contraseña:" CssClass="control-label col-sm-4"></asp:Label>
                                            <div class=" col-sm-8">
                                                <asp:TextBox ID="txtPass2" runat="server" Width="150px" TextMode="Password" CssClass="form-control"></asp:TextBox>
                                                <br />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" CssClass="text-danger"  runat="server" ErrorMessage="Debe repetir la contraseña" ValidationGroup="blanqueo" ControlToValidate="txtPass2"></asp:RequiredFieldValidator>
                                                <br />
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="text-danger"  ErrorMessage="Las contraseñas no coinciden" ControlToCompare="txtPass1" ControlToValidate="txtPass2" Operator="Equal" Type="String" ValidationGroup="blanqueo"></asp:CompareValidator>
                                            </div>
                                        </div>
                                    </div>
                                    
                                </asp:Panel>
                        <hr />
                         <asp:UpdatePanel ID="updBlanquearClave" runat="server">
                            <ContentTemplate>
                                <div style="width: 100%; padding: 5px 25px 5px 0px; text-align: right">
                                    <table border="0">
                                        <tr>
                                            <td style="width: 50%">

                                                <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="updBlanquearClave" runat="server" DisplayAfter="0">
                                                    <ProgressTemplate>
                                                        <img src="../Common/Images/Controles/Loading24x24.gif" alt="" />
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>

                                            </td>
                                            <td>

                                        <asp:Button ID="btnBlanqueo" runat="server" CssClass="btn btn-primary" Text="Blanquear Contraseña" OnClick="lnkBlanqueo_Click" ValidationGroup="blanqueo" />
                                                
                                            </td>
                                            <td>
                                                <asp:Button ID="btnCancelarBlanqueo" CssClass="btn btn-default" runat="server" data-dismiss="modal"
                                                    Text="Cancelar" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                                    
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
    <%--fin modal--%>
    <script type="text/javascript">

        $(document).ready(function () {
            camposAutonumericosABM();
            init_JS_updBuscarUbicacion();
        });
        function init_JS_updBuscarUbicacion() {

            $("#<%: ddlConsejo.ClientID %>").select2({
                placeholder: "(Seleccione el Consejo)",
                allowClear: true,
            });

        }
        function hideDatosUsuario()
        {
            debugger;
            $('#pnlDatosUsuarioClient').modal("hide");
            $('.modal-backdrop').remove();
            $('body').removeClass('modal-open');
        }
        function ocultarFondoModal()
        {
            $('.modal-backdrop').remove();
            $('body').removeClass('modal-open');
        }
        function ocultarMensajes()
        {
            $("#pnlMensajes").modal("hide");
            $('.modal-backdrop').remove();
            $('body').removeClass('modal-open');
        }
        function camposAutonumericosABM() {



            $("#<%: txtTelefono.ClientID %>").autoNumeric({ aSep: '', mDec: '0', vMax: '9999999999999999999999' });
            $("#<%: txtNroDoc.ClientID %>").autoNumeric({ aSep: '', mDec: '0', vMax: '99999999' });
            $("#<%: txtIngresosBrutos.ClientID %>").autoNumeric({ aSep: '', mDec: '0', vMax: '999999999999999' });
            $("#<%: txtNroPuerta.ClientID %>").autoNumeric({ aSep: '', mDec: '0', vMax: '99999' });
            $("#<%: txtMatriculaMetrogas.ClientID %>").autoNumeric({ aSep: '', mDec: '0', vMax: '99999999' });

            return false;
        }

        function ValidarDniCuit(sender, e) {

            var tipoDoc = $("#<%: ddlTipoDoc.ClientID %>").val();
            var isValid = true;

            if (tipoDoc == 1) {
                var dni = $("#<%: txtNroDoc.ClientID %>").val();
                var cuit = e.Value.split("-")[1];

                dni = "00000000" + dni;
                dni = dni.substr(dni.length - 8, 8);

                cuit = "00000000" + cuit;
                cuit = cuit.substr(cuit.length - 8, 8);
                if (dni.substr(0, 2) > 90) {
                    isValid = true;
                }
                else {
                    isValid = (dni == cuit);
                }
            }
            e.IsValid = isValid;
        }

        function ShowModalCargadeUsuario()
        {
            $("#pnlDatosUsuarioClient").modal("show");
            return false;
        }
        function ShowBlanquearClave()
        {
            $("#pnlBlanquearClave").modal("show");
            return false;
        }
        function ShowVisualizadorActualizacion() {
            $("#pnlVisualizador").modal("show");
            return false;
        }
        function ShowMSJGenerarUsuario() {
            $("#pnlMSJGenerarUsuario").modal("show");
            return false;
        }
        function ocultarMSJGenerarUsuario() {
            $('.modal-backdrop').remove();
            $('body').removeClass('modal-open');
        } 
        function hideBlanquearClave() {
            $("#pnlBlanquearClave").modal("hide");
            $('body').removeClass('modal-open');//eliminamos la clase del body para poder hacer scroll
            $('.modal-backdrop').remove();//eliminamos el backdrop del modal
            return false;
        }
        function hideMensajes() {
            $("#pnlMensajes").modal("hide");
            $('body').removeClass('modal-open');//eliminamos la clase del body para poder hacer scroll
            $('.modal-backdrop').remove();//eliminamos el backdrop del modal
            return false;
        }
        function ShowError()
        {
            $("#pnlMensajes").modal("show");
            return false;
        }
        function ShowSucess()
        {
            $("#pnlMensajesSuccess").modal("show");
            return false;
        }
        function cerrarModal() {
            window.location.href = 'BuscarProfesionales.aspx';
        };
    </script>

                </ContentTemplate>
            </asp:UpdatePanel>
</asp:Content>

