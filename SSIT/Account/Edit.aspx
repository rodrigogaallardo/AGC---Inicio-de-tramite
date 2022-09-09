<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="SSIT.Account.Edit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
    <%: Scripts.Render("~/bundles/autoNumeric") %>
<asp:UpdatePanel ID="UpdModificarDatos" runat="server" UpdateMode="Conditional">
<ContentTemplate>                
    <asp:Panel ID="pnlModificarDatos" runat="server" CssClass="form-horizontal">
        <h2>Datos del usuario</h2>


        <label class="mtop20">Desde aqu&iacute; podr&aacute; modificar los datos de usuario.</label>
        <label>Tenga en cuenta que si modifica el e-mail, el sistema le enviará un correo a la casilla indicada y deberá volver a activar el usuario para poder utilizar el sistema.</label>


        <div style="color: red; padding: 10px 0px 10px 0px">
            <div class="field-validation-error">
                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
            </div>
        </div>
        <fieldset>
            <div class="form-horizontal">
                <div class="form-group">
                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" CssClass="col-sm-3 col-sm-3 control-label">Nombre de Usuario (*):</asp:Label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="UserName" runat="server" MaxLength="50" Width="150px" CssClass="form-control" Enabled="false"></asp:TextBox>
                    </div>
                </div>


                <div class="form-group">
                    <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email" CssClass="col-sm-3 control-label">E-mail (*):</asp:Label>
                    <div class="col-sm-3">

                        <asp:TextBox ID="Email" runat="server" MaxLength="50" Width="300px" CssClass="form-control" Enabled="false"></asp:TextBox>

                    </div>
                     <%--<asp:LinkButton runat="server" Text="Modificar E-mail" CssClass="btn btn-default" ID="btnNuevoEmailShow" OnClick="showNuevoEmail" style="margin-left:30px"></asp:LinkButton>
                     <asp:LinkButton runat="server" Text="No Modificar E-mail" CssClass="btn btn-default" ID="btnNuevoEmailHide" OnClick="hideNuevoEmail" style="margin-left:30px; display:none"></asp:LinkButton>--%>
                    </div>
                     <asp:UpdatePanel ID="udpEmail" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>         
                              <asp:Panel ID="pnlEmail" runat="server" style="display:none">
                               <div class="form-group">
                               <asp:Label ID="Label5" runat="server" AssociatedControlID="txtNuevoEmail" CssClass="col-sm-3 control-label">Nuevo E-mail:</asp:Label>
                                <div class="col-sm-9">
                                  <asp:TextBox ID="txtNuevoEmail" runat="server" MaxLength="50"  Width="300px" CssClass="form-control"></asp:TextBox>
                                  <div class="req">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNuevoEmail"
                                            Display="Dynamic" ErrorMessage="El nuevo E-mail es requerido." ValidationGroup="CreateUserWizard"
                                            SetFocusOnError="True" CssClass="field-validation-error"></asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNuevoEmail" Display="Dynamic"
                                            CssClass="field-validation-error" ErrorMessage="E-mail no tiene un formato válido. Ej: nombre@servidor.com"
                                            SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ValidationGroup="CreateUserWizard">
                                        </asp:RegularExpressionValidator>

                                    </div>
                                </div>
                              </div>
                              <div class="form-group">
                               <asp:Label ID="Label6" runat="server" AssociatedControlID="txtNuevoEmailConfirmacion" CssClass="col-sm-3 control-label">Confirmar Nuevo E-mail:</asp:Label>
                               <div class="col-sm-9">
                                 <asp:TextBox ID="txtNuevoEmailConfirmacion" runat="server" MaxLength="50"  Width="300px" CssClass="form-control" ></asp:TextBox>
                                     <div class="req">
                                        <asp:RequiredFieldValidator ID="txtNuevoEmailConfirmacionRequered" runat="server" ControlToValidate="txtNuevoEmailConfirmacion"
                                            ErrorMessage="Confirmación del E-mail es requerido." ValidationGroup="CreateUserWizard" Display="Dynamic"
                                            SetFocusOnError="True" CssClass="field-validation-error"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="txtNuevoEmailConfirmacionRequered2" runat="server" EnableViewState="False"
                                            ControlToValidate="txtNuevoEmail" ErrorMessage="El nuevo e-mail y la Confirmación deben coincidir"
                                            ValidationGroup="CreateUserWizard" ControlToCompare="txtNuevoEmailConfirmacion" Display="Dynamic"
                                            CssClass="field-validation-error"></asp:CompareValidator>
                                    </div>
                               </div>
                            </div>
                              </asp:Panel>

                           </ContentTemplate>
                  </asp:UpdatePanel>
             

                <div class="form-group">
                    <asp:Label ID="Label3" runat="server" AssociatedControlID="tipoFisica" Text="Tipo (*):" CssClass="col-sm-3 control-label"></asp:Label>
                    <div class="col-sm-9">
                        <asp:UpdatePanel ID="updTipoPersona" runat="server" RenderMode="Inline">
                            <ContentTemplate>


                                <div class="radio">
                                    <label>
                                        <asp:RadioButton ID="tipoFisica" runat="server"
                                            GroupName="tipo" Checked="true" AutoPostBack="True" OnCheckedChanged="tipo_CheckedChanged" />
                                        Persona F&iacute;sica
                                                    
                                    </label>
                                </div>


                                <div class="form-inline">
                                    <div class="radio">
                                        <label>
                                            <asp:RadioButton ID="tipoJuridica" runat="server"
                                                GroupName="tipo" AutoPostBack="true" OnCheckedChanged="tipo_CheckedChanged" />
                                            Persona Jur&iacute;dica
                                        </label>

                                    </div>
                                    <div class="form-group">
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="200" AssociatedUpdatePanelID="updTipoPersona">
                                            <ProgressTemplate>
                                                <img src="../Content/img/app/Loading24x24.gif" style="margin-left: 20px" alt="loading" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <asp:UpdatePanel ID="updDatosPersonas" runat="server" RenderMode="Inline">
                    <ContentTemplate>
                        <!-- Persona fisica-->
                        <asp:Panel ID="panelPF" runat="server">
                            <div class="form-group" id="divDni">
                                <asp:Label ID="lblDni" runat="server" AssociatedControlID="txtDni" CssClass="col-sm-3 control-label">D.N.I (*):</asp:Label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtDni" runat="server" MaxLength="10" Width="100px" CssClass="form-control"></asp:TextBox>
                                    <div class="req">
                                        <asp:RequiredFieldValidator ID="rfv_txtDni" runat="server" ControlToValidate="txtDni"
                                            Display="Dynamic" ErrorMessage="DNI es requerido." ValidationGroup="Guardar"
                                            SetFocusOnError="True" CssClass="field-validation-error"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="rv_txtDni" runat="server" ControlToValidate="txtDni"
                                            Display="Dynamic" ErrorMessage="DNI debe estar entre 1 y 99999999"
                                            ValidationGroup="Guardar" SetFocusOnError="True" MaximumValue="99999999"
                                            MinimumValue="1" Type="Integer" CssClass="field-validation-error"></asp:RangeValidator>

                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="ApellidoLabel" runat="server" AssociatedControlID="Apellido" CssClass="col-sm-3 control-label">Apellido (*):</asp:Label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="Apellido" runat="server" MaxLength="50" Width="200px" CssClass="form-control"></asp:TextBox>

                                    <div class="req">
                                        <asp:RequiredFieldValidator ID="ApellidoRequired" runat="server" ControlToValidate="Apellido"
                                            Display="Dynamic" ErrorMessage="Apellido es requerido." ValidationGroup="Guardar"
                                            SetFocusOnError="True" CssClass="field-validation-error"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="NombreLabel" runat="server" AssociatedControlID="Nombre" CssClass="col-sm-3 control-label">Nombre/s (*):</asp:Label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="Nombre" runat="server" MaxLength="50" Width="350px" CssClass="form-control"></asp:TextBox>

                                    <div class="req">
                                        <asp:RequiredFieldValidator ID="NombreRequired" runat="server" ControlToValidate="Nombre"
                                            Display="Dynamic" ErrorMessage="Nombre es requerido." ValidationGroup="Guardar"
                                            SetFocusOnError="True" CssClass="field-validation-error"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <!-- Persona juridaca-->
                        <asp:Panel ID="panelPJ" runat="server" Visible="false">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" AssociatedControlID="txtCUIT" CssClass="col-sm-3 control-label">C.U.I.T. (*):</asp:Label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtCUIT" runat="server" MaxLength="11" Width="150px" CssClass="form-control"></asp:TextBox>
                                    <div>
                                        <asp:RequiredFieldValidator ID="ReqtxtCuitPF" runat="server" ControlToValidate="txtCUIT"
                                            Display="Dynamic" CssClass="field-validation-error" ErrorMessage="CUIT es requerido."
                                            SetFocusOnError="True" ValidationGroup="Guardar"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidatorCuit" runat="server" ControlToValidate="txtCUIT"
                                            Display="Dynamic" ErrorMessage="CUIT debe estar entre 1 y 99999999999"
                                            ValidationGroup="Guardar" SetFocusOnError="True" MaximumValue="99999999999"
                                            MinimumValue="1" Type="Double" CssClass="field-validation-error"></asp:RangeValidator>

                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" AssociatedControlID="txtRazon" CssClass="col-sm-3 control-label">Razon Social (*):</asp:Label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtRazon" runat="server" MaxLength="50" Width="250px" CssClass="form-control"></asp:TextBox>

                                    <div class="req">
                                        <asp:RequiredFieldValidator ID="rqRazon" runat="server" ControlToValidate="txtRazon"
                                            Display="Dynamic" ErrorMessage="Razon Social es requerido." ValidationGroup="Guardar"
                                            SetFocusOnError="True" CssClass="field-validation-error"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>


                <div class="form-group">
                    <label class="col-sm-3 control-label">Calle (*):</label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="Calle" runat="server" MaxLength="50" Width="250px" CssClass="form-control"></asp:TextBox>
                        <div class="req">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Calle"
                                Display="Dynamic" ErrorMessage="Calle es requerido." ValidationGroup="Guardar"
                                SetFocusOnError="True" CssClass="field-validation-error"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>


                <div class="form-group">
                    <label class="col-sm-3 control-label">N&uacute;mero (*):</label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="NroPuerta" runat="server" Width="100px" CssClass="form-control"></asp:TextBox>

                        <div class="req">
                            <asp:RequiredFieldValidator ID="NroPuertaRequired" runat="server" ControlToValidate="NroPuerta"
                                Display="Dynamic" ErrorMessage="Número de calle es requerido." ValidationGroup="Guardar"
                                SetFocusOnError="True" CssClass="field-validation-error"></asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="NroPuertaRange" runat="server" ControlToValidate="NroPuerta"
                                Display="Dynamic" ErrorMessage="Número de Calle debe estar entre 1 y 999999"
                                ValidationGroup="Guardar" SetFocusOnError="True" MaximumValue="999999"
                                MinimumValue="1" Type="Integer" CssClass="field-validation-error"></asp:RangeValidator>
                        </div>
                    </div>
                </div>


                <div class="form-group">
                    <label class="col-sm-3 control-label">Piso:</label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="Piso" runat="server" MaxLength="5" Width="150px" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>


                <div class="form-group">
                    <label class="col-sm-3 control-label">Depto/UF:</label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="Depto" runat="server" MaxLength="5" Width="150px" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-sm-3 control-label">C&oacute;digo Postal:</label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="CodPostal" runat="server" MaxLength="10" Width="150px" CssClass="form-control"></asp:TextBox>
                        <div class="req">
                            <asp:RegularExpressionValidator ID="CodPostalRegEx" runat="server" ControlToValidate="CodPostal"
                                Display="Dynamic" ErrorMessage="El Código Postal solo puede contener letras y números."
                                ValidationGroup="Guardar" SetFocusOnError="True" ValidationExpression="[a-zA-Z]*\d*[a-zA-Z]*"
                                CssClass="field-validation-error"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>


                <div class="form-group">
                    <label class="col-sm-3 control-label">Provincia (*):</label>
                    <div class="col-sm-9">
                        <asp:UpdatePanel ID="updProvincias" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                            <ContentTemplate>

                                <div class="form-inline">
                                    <div>
                                        <asp:DropDownList ID="ProvinciaDropDown" runat="server" Width="300px" AutoPostBack="True"
                                            CssClass="form-control" OnSelectedIndexChanged="ProvinciaDropDown_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="inline">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="updProvincias">
                                            <ProgressTemplate>
                                                <img src="../Content/img/app/Loading24x24.gif" style="margin-left: 10px" alt="loading" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="req">
                            <asp:RequiredFieldValidator ID="ProvinciaRequired" runat="server" ControlToValidate="ProvinciaDropDown"
                                ErrorMessage="Provincia es requerida" ValidationGroup="Guardar" Display="Dynamic"
                                CssClass="field-validation-error"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>


                <div class="form-group">
                    <label class="col-sm-3 control-label">Localidad (*):</label>
                    <div class="col-sm-9">
                        <asp:UpdatePanel ID="uodLocalidades" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="LocalidadDropDown" runat="server" CssClass="form-control" Width="300px">
                                </asp:DropDownList>

                                <div class="req">
                                    <asp:RequiredFieldValidator ID="LocalidadRequired" runat="server" ControlToValidate="LocalidadDropDown"
                                        Display="Dynamic" ErrorMessage="Localidad es requerida" ValidationGroup="Guardar"
                                        CssClass="field-validation-error"></asp:RequiredFieldValidator>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </div>


                <div class="form-group">
                    <label class="col-sm-3 control-label">Tel&eacute;fono:</label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="TelefonoTextBox" runat="server" MaxLength="50" Width="300px" CssClass="form-control"
                            ToolTip="Teléfono"></asp:TextBox>
                        <div class="req">
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TelefonoTextBox"
                                ErrorMessage="el teléfono solo puede contener números y guiones. Ej: 011-1234-5678"
                                Display="Dynamic" ValidationGroup="Guardar" SetFocusOnError="True" ValidationExpression="\d+(-|\d)*"
                                CssClass="field-validation-error"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>

                <div class="form-group">
                    <label class="col-sm-3 control-label">M&oacute;vil:</label>
                    <div class="col-sm-9">
                        <asp:TextBox ID="Movil" runat="server" MaxLength="50" Width="150px" CssClass="form-control"></asp:TextBox>
                        <div class="req">
                            <asp:RegularExpressionValidator ID="MovilRegEx" runat="server" ControlToValidate="Movil"
                                ErrorMessage="Movil solo puede contener números y guiones. Ej: 011-1234-5678"
                                Display="Dynamic" ValidationGroup="Guardar" SetFocusOnError="True" ValidationExpression="\d+(-|\d)*"
                                CssClass="field-validation-error"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>


                <div class="form-group">

                    <div class="col-sm-12 text-center">

                        
                            <asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-primary btn-lg" ValidationGroup="Guardar" OnClientClick="return ocultarBoton(this);" OnClick="ActualizarUsuario">
                                <i class="imoon imoon-ok"></i>
                                <span class="text">Guardar</span>

                            </asp:LinkButton>

                            <div id="pnlProcensandoGuardar" class="display-none">
                                <img src="../Content/img/app/Loading24x24.gif" style="margin-left: 10px" alt="loading" />
                                <label class="dinline mleft5">Procesando</label>
                            </div>


                    </div>
                </div>

            </div>
        </fieldset>
    </asp:Panel>
   </ContentTemplate>
  </asp:UpdatePanel>

    <div id="frmContrase" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" style="margin-top: -8px">Actualización de usuario</h4>
                </div>
                <div class="modal-body text-center">
                <asp:label ID="Label4" runat="server"  Font-Bold="true" Style="color:#377bb5">El usuario ha sido actualizado exitosamente</asp:label>
                </div>
               <div class="modal-footer mleft20 mright20">

                    <asp:UpdatePanel ID="updBotonesIngresarTitularesSH" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress10" runat="server" AssociatedUpdatePanelID="updBotonesIngresarTitularesSH">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div id="pnlBotonesIngresarTitularesSH" class="form-group">
                                    <asp:Button ID="btnAceptar" runat="server" CssClass="btn btn-primary" Text="Cerrar" OnClick="redirectHome"
                                        OnClientClick="return hideConfirm();"  />
                                 
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

      <div id="frmConfirmarNuevoEmail" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top:-8px">Modificación de E-mail</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon-remove-circle fs64 color-blue"></i>
                            </td>
                            <td style="vertical-align: middle">
                                <label class="mleft10"> <strong>Atenci&oacute;n!</strong> Esta a punto de modificar el e-mail, si presiona "Sí", para poder iniciar sesión nuevamente será necesario
                                que vuelva a activar el usuario. Para ello se le enviará un e-mail con las instrucciones necesarias a su nuevo correo.</label>
                            </td>
                        </tr>
                    </table>

                </div>
                <div class="modal-footer mleft20 mright20">

                    <asp:UpdatePanel ID="updConfirmarEliminar" runat="server">
                        <ContentTemplate>

                            <asp:HiddenField ID="hid_id_sobrecarga_eliminar" runat="server" />

                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updConfirmarEliminar">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div id="pnlBotonesConfirmacionEliminar" class="form-group">
                                    <asp:Button ID="btnConfirmarEliminarSobrecarga" runat="server" CssClass="btn btn-primary" Text="Sí" OnClick="ActualizarUsuario"
                                        OnClientClick="ocultarBotonesConfirmacion();" />
                                    <asp:Button runat="server" type="button" class="btn btn-default" data-dismiss="modal" Text="No" OnClientClick="return mostrarBotonGuardar();"></asp:Button>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>


    <%--Popup de Error--%>
    <asp:Panel ID="pnlError" runat="server" CssClass="modalPopup" Style="display: none"
        Width="450px" DefaultButton="btnAceptarError">
        <table style="width: 100%; border-collapse:separate; border-spacing:7px">
            <tr>
                <td style="width: 80px">
                    <asp:Image ID="imgmpeInfo" runat="server" ImageUrl="~/Common/Images/Controles/error64x64.png" />
                </td>
                <td>
                    <asp:UpdatePanel ID="updmpeInfo" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblError" runat="server" CssClass="error-label"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="btnAceptarError" runat="server" CssClass="btnOK" Text="Aceptar" Width="100px" />
                </td>
            </tr>
        </table>
    </asp:Panel>


    <script type="text/javascript">


        $(document).ready(function () {

            $("[id*='NroPuerta']").autoNumeric("init", { aSep: "", mDec: 0, vMax: '99999' });

        });

        function init_JS_updDatosPersonas() {

            $("[id*='txtCUIT']").autoNumeric("init", { aSep: "", mDec: 0, vMax: '9999999999999' });

        }

        function ocultarBoton(obj) {


            if (Page_ClientValidate("Guardar")) {
                $(obj).switchClass("display-inline", "display-none");
                $("[id*='pnlProcensandoGuardar']").switchClass("display-none", "display-block");

                <%-- if ($('#<%: btnNuevoEmailHide.ClientID %>').css("display") != "none") {
                    $("#frmConfirmarNuevoEmail").modal("show");
                    return false;
                }--%>

            }
        }

        function hideConfirm() {
            $("#frmContrase").modal("hide");
            return true;
        }

        function showConfirm() {
            $("#frmContrase").modal("show");
        }
        function ocultarBotonesConfirmacion()
        {
            $("#frmConfirmarNuevoEmail").modal("hide");
        }
        function mostrarBotonGuardar()
        {
            $("[id*='btnSave']").switchClass("display-none", "display-inline");
            $("#pnlProcensandoGuardar").switchClass("display-block", "display-none");
            return false;
        }

    </script>

</asp:Content>

