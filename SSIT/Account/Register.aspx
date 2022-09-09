<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="SSIT.Account.Register" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="HeadContent">
    <style>
        .table > thead > tr > th,
        .table > tbody > tr > th,
        .table > tfoot > tr > th,
        .table > thead > tr > td,
        .table > tbody > tr > td,
        .table > tfoot > tr > td {
            border-top: solid 0px #ccc !important;
        }
    </style>
</asp:Content>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <%: Scripts.Render("~/bundles/autoNumeric") %>
    <div>
        <asp:CreateUserWizard ID="CreateUserWizard" runat="server" OnCreatedUser="CreateUserWizard_CreatedUser"
            ContinueDestinationPageUrl="~/Account/Login" ContinueButtonText="Iniciar Sesión" DisableCreatedUser="true"
            InvalidAnswerErrorMessage="La Respuesta de Seguridad no es válida" InvalidEmailErrorMessage="Escriba una dirección de e-mail válida"
            InvalidPasswordErrorMessage="La contraseña debe tener al menos {0} caracteres alfanuméricos."
            InvalidQuestionErrorMessage="La Pregunta de Seguridad no es válida." UnknownErrorMessage="La cuenta no fue creada. Por favor, intente de nuevo."
            DuplicateEmailErrorMessage="La dirección de e-mail que ha ingresado ya está registrada. Por favor, ingrese una dirección de E-mail diferente."
            DuplicateUserNameErrorMessage="Por favor, ingrese un nombre de usuario diferente. El mismo se encuentra en uso."
            CancelDestinationPageUrl="~/Account/Login"
            LoginCreatedUser="False"
            RequireEmail="False"
            cssClass="table table-responsive">

            <StepStyle CssClass="form-horizontal" />
            <CreateUserButtonStyle CssClass="hide" />

            <WizardSteps>
                <asp:CreateUserWizardStep runat="server" ID="CreateUserStep">
                    <ContentTemplate>
                        <hgroup class="title">
                            <h2>Registración de Usuario.</h2>
                        </hgroup>

                        <div style="line-height: 18px">
                            El usuario le servirá para iniciar un trámite online y para seguir el estado del mismo.<br />
                            Para obtener un usuario, debe completar los campos que se muestran a continuación,
                       luego el sistema le enviará un email para comprobar la dirección de correo.
                        <br />
                            Siga las instrucciones del email para activar el usuario.<br />
                            Si ya posee un usuario en el sistema anterior, no es necesario que se vuelva a registrar, puede utilizar el mismo.<br />
                            (*) Campos Obligatorios.
                        </div>


                        <div style="color: red; padding: 10px 0px 10px 0px">
                            <div class="field-validation-error">
                                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                            </div>
                        </div>

                        <fieldset>
                            <div class="form-horizontal">
                                <div>
                                    <div class="form-group">
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" CssClass="col-sm-3 col-sm-3 control-label">Nombre de Usuario (*):</asp:Label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="UserName" runat="server" MaxLength="50" CssClass="form-control" placeholder="Nombre de Usuario"></asp:TextBox>
                                            <div>
                                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                    Display="Dynamic" ErrorMessage="Nombre de usuario es requerido." ValidationGroup="CreateUserWizard"
                                                    SetFocusOnError="True" CssClass="field-validation-error"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="field-validation-error"
                                                    runat="server" ErrorMessage="Debe ingresar solo letras o números." Display="Dynamic"
                                                    ControlToValidate="UserName" ValidationExpression="^[a-zA-Z0-9]*$"
                                                    ValidationGroup="CreateUserWizard"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" CssClass="col-sm-3 control-label">Clave (*):</asp:Label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="Password" runat="server" MaxLength="50" oncopy="return false" TextMode="Password" CssClass="form-control" placeholder="Clave (*)"></asp:TextBox>
                                            <div class="req">
                                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                    Display="Dynamic" ErrorMessage="Contraseña es requerida." ValidationGroup="CreateUserWizard"
                                                    SetFocusOnError="True" CssClass="field-validation-error"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword" CssClass="col-sm-3 control-label">Confirme Clave (*):</asp:Label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="ConfirmPassword" runat="server" MaxLength="50" TextMode="Password" CssClass="form-control" placeholder="Confirme Clave (*)"></asp:TextBox>
                                            <div class="req">
                                                <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword"
                                                    ErrorMessage="Confirmación de contraseña es requerida." ValidationGroup="CreateUserWizard" Display="Dynamic"
                                                    SetFocusOnError="True" CssClass="field-validation-error"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="PasswordCompare" runat="server" EnableViewState="False"
                                                    ControlToValidate="ConfirmPassword" ErrorMessage="La Contraseña y la Confirmación deben coincidir"
                                                    ValidationGroup="CreateUserWizard" ControlToCompare="Password" Display="Dynamic"
                                                    CssClass="field-validation-error"></asp:CompareValidator>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="form-group">
                                        <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email" CssClass="col-sm-3 control-label">E-mail (*):</asp:Label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="Email" runat="server" MaxLength="50" oncopy="return false" CssClass="form-control" placeholder="E-Mail"></asp:TextBox>
                                            <div class="req">
                                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email"
                                                    Display="Dynamic" ErrorMessage="E-mail es requerido." ValidationGroup="CreateUserWizard"
                                                    SetFocusOnError="True" CssClass="field-validation-error"></asp:RequiredFieldValidator>

                                                <asp:RegularExpressionValidator ID="EmailRegEx" runat="server" ControlToValidate="Email" Display="Dynamic"
                                                    CssClass="field-validation-error" ErrorMessage="E-mail no tiene un formato válido. Ej: nombre@servidor.com"
                                                    SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    ValidationGroup="CreateUserWizard">
                                                </asp:RegularExpressionValidator>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label ID="ConfirmEmailLabel" runat="server" AssociatedControlID="txtConfirmEmail" CssClass="col-sm-3 control-label">Confirme E-mail (*):</asp:Label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtConfirmEmail" runat="server" MaxLength="50" TextMode="Email" CssClass="form-control" placeholder="Confirme E-Mail (*)"></asp:TextBox>
                                            <div class="req">
                                                <asp:RequiredFieldValidator ID="txtConfirmEmailRequered" runat="server" ControlToValidate="txtConfirmEmail"
                                                    ErrorMessage="Confirmación del E-mail es requerido." ValidationGroup="CreateUserWizard" Display="Dynamic"
                                                    SetFocusOnError="True" CssClass="field-validation-error"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="ConfirmEmail" runat="server" EnableViewState="False"
                                                    ControlToValidate="txtConfirmEmail" ErrorMessage="El e-mail y la Confirmación deben coincidir"
                                                    ValidationGroup="CreateUserWizard" ControlToCompare="Email" Display="Dynamic"
                                                    CssClass="field-validation-error"></asp:CompareValidator>
                                            </div>
                                        </div>
                                    </div>


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
                                                        <asp:TextBox ID="txtDni" runat="server" MaxLength="10" CssClass="form-control" placeholder="D.N.I (*)"></asp:TextBox>
                                                        <div class="req">
                                                            <asp:RequiredFieldValidator ID="rfv_txtDni" runat="server" ControlToValidate="txtDni"
                                                                Display="Dynamic" ErrorMessage="DNI es requerido." ValidationGroup="CreateUserWizard"
                                                                SetFocusOnError="True" CssClass="field-validation-error"></asp:RequiredFieldValidator>
                                                            <asp:RangeValidator ID="rv_txtDni" runat="server" ControlToValidate="txtDni"
                                                                Display="Dynamic" ErrorMessage="DNI debe estar entre 1 y 99999999"
                                                                ValidationGroup="CreateUserWizard" SetFocusOnError="True" MaximumValue="99999999"
                                                                MinimumValue="1" Type="Integer" CssClass="field-validation-error"></asp:RangeValidator>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="ApellidoLabel" runat="server" AssociatedControlID="Apellido" CssClass="col-sm-3 control-label">Apellido (*):</asp:Label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox ID="Apellido" runat="server" MaxLength="50" CssClass="form-control" placeholder="Apellido (*)"></asp:TextBox>

                                                        <div class="req">
                                                            <asp:RequiredFieldValidator ID="ApellidoRequired" runat="server" ControlToValidate="Apellido"
                                                                Display="Dynamic" ErrorMessage="Apellido es requerido." ValidationGroup="CreateUserWizard"
                                                                SetFocusOnError="True" CssClass="field-validation-error"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="NombreLabel" runat="server" AssociatedControlID="Nombre" CssClass="col-sm-3 control-label">Nombre/s (*):</asp:Label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox ID="Nombre" runat="server" MaxLength="50" CssClass="form-control" placeholder="Nombre/s (*)"></asp:TextBox>

                                                        <div class="req">
                                                            <asp:RequiredFieldValidator ID="NombreRequired" runat="server" ControlToValidate="Nombre"
                                                                Display="Dynamic" ErrorMessage="Nombre es requerido." ValidationGroup="CreateUserWizard"
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
                                                        <asp:TextBox ID="txtCUIT" runat="server" MaxLength="11" CssClass="form-control" placeholder="C.U.I.T. (*)"></asp:TextBox>
                                                        <div>
                                                            <asp:RequiredFieldValidator ID="ReqtxtCuitPF" runat="server" ControlToValidate="txtCUIT"
                                                                Display="Dynamic" CssClass="field-validation-error" ErrorMessage="CUIT es requerido."
                                                                SetFocusOnError="True" ValidationGroup="CreateUserWizard"></asp:RequiredFieldValidator>
                                                            <asp:RangeValidator ID="RangeValidatorCuit" runat="server" ControlToValidate="txtCUIT"
                                                                Display="Dynamic" ErrorMessage="CUIT debe estar entre 1 y 99999999999"
                                                                ValidationGroup="CreateUserWizard" SetFocusOnError="True" MaximumValue="99999999999"
                                                                MinimumValue="1" Type="Double" CssClass="field-validation-error"></asp:RangeValidator>

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Label ID="Label2" runat="server" AssociatedControlID="txtRazon" CssClass="col-sm-3 control-label">Razón Social (*):</asp:Label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox ID="txtRazon" runat="server" MaxLength="50" CssClass="form-control" placeholder="Razón Social (*)"></asp:TextBox>

                                                        <div class="req">
                                                            <asp:RequiredFieldValidator ID="rqRazon" runat="server" ControlToValidate="txtRazon"
                                                                Display="Dynamic" ErrorMessage="Razon Social es requerido." ValidationGroup="CreateUserWizard"
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
                                            <asp:TextBox ID="Calle" runat="server" MaxLength="50" CssClass="form-control" placeholder="Calle (*)"></asp:TextBox>
                                            <div class="req">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Calle"
                                                    Display="Dynamic" ErrorMessage="Calle es requerido." ValidationGroup="CreateUserWizard"
                                                    SetFocusOnError="True" CssClass="field-validation-error"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Número (*):</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="NroPuerta" runat="server" CssClass="form-control" placeholder="Número (*)"></asp:TextBox>

                                            <div class="req">
                                                <asp:RequiredFieldValidator ID="NroPuertaRequired" runat="server" ControlToValidate="NroPuerta"
                                                    Display="Dynamic" ErrorMessage="Número de calle es requerido." ValidationGroup="CreateUserWizard"
                                                    SetFocusOnError="True" CssClass="field-validation-error"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="NroPuertaRange" runat="server" ControlToValidate="NroPuerta"
                                                    Display="Dynamic" ErrorMessage="Número de Calle debe estar entre 1 y 999999"
                                                    ValidationGroup="CreateUserWizard" SetFocusOnError="True" MaximumValue="999999"
                                                    MinimumValue="1" Type="Integer" CssClass="field-validation-error"></asp:RangeValidator>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Piso:</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="Piso" runat="server" MaxLength="5" CssClass="form-control" placeholder="Piso"></asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Depto/UF:</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="Depto" runat="server" MaxLength="5" CssClass="form-control" placeholder="Depto/UF"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Código Postal:</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="CodPostal" runat="server" MaxLength="10" CssClass="form-control" placeholder="Código Postal"></asp:TextBox>
                                            <div class="req">
                                                <asp:RegularExpressionValidator ID="CodPostalRegEx" runat="server" ControlToValidate="CodPostal"
                                                    Display="Dynamic" ErrorMessage="El Código Postal solo puede contener letras y números."
                                                    ValidationGroup="CreateUserWizard" SetFocusOnError="True" ValidationExpression="[a-zA-Z]*\d*[a-zA-Z]*"
                                                    CssClass="field-validation-error"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Provincia (*):</label>
                                        <div class="col-sm-9">
                                            <asp:UpdatePanel ID="updProvincias" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div>
                                                        <asp:DropDownList ID="ProvinciaDropDown" runat="server" AutoPostBack="True"
                                                            CssClass="form-control " OnSelectedIndexChanged="ProvinciaDropDown_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="inline">
                                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="updProvincias">
                                                            <ProgressTemplate>
                                                                <img src="../Content/img/app/Loading24x24.gif" style="margin-left: 10px" alt="loading" />
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="req">
                                                <asp:RequiredFieldValidator ID="ProvinciaRequired" runat="server" ControlToValidate="ProvinciaDropDown"
                                                    ErrorMessage="Provincia es requerida" ValidationGroup="CreateUserWizard" Display="Dynamic"
                                                    CssClass="field-validation-error"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Localidad (*):</label>
                                        <div class="col-sm-9">
                                            <asp:UpdatePanel ID="uodLocalidades" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="LocalidadDropDown" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                    <div class="req">
                                                        <asp:RequiredFieldValidator ID="LocalidadRequired" runat="server" ControlToValidate="LocalidadDropDown"
                                                            Display="Dynamic" ErrorMessage="Localidad es requerida" ValidationGroup="CreateUserWizard"
                                                            CssClass="field-validation-error"></asp:RequiredFieldValidator>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">M&oacute;vil:</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="Movil" runat="server" MaxLength="50" CssClass="form-control" placeholder="Móvil"></asp:TextBox>
                                            <div class="req">
                                                <asp:RegularExpressionValidator ID="MovilRegEx" runat="server" ControlToValidate="Movil"
                                                    ErrorMessage="Movil solo puede contener números y guiones. Ej: 011-1234-5678"
                                                    Display="Dynamic" ValidationGroup="CreateUserWizard" SetFocusOnError="True" ValidationExpression="\d+(-|\d)*"
                                                    CssClass="field-validation-error"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-3 control-label">Tel&eacute;fono:</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="TelefonoTextBox" runat="server" MaxLength="50" CssClass="form-control" placeholder="Teléfono"
                                                ToolTip="Teléfono"></asp:TextBox>
                                            <div class="req">
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TelefonoTextBox"
                                                    ErrorMessage="el teléfono solo puede contener números y guiones. Ej: 011-1234-5678"
                                                    Display="Dynamic" ValidationGroup="CreateUserWizard" SetFocusOnError="True" ValidationExpression="\d+(-|\d)*"
                                                    CssClass="field-validation-error"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-sm-1 control-label"></label>
                                        <label class="col-sm-9 control-label" style="text-align: center">
                                            El correo electrónico declarado en la presente será el medio de comunicación oficial para todos aquellos 
                                    actos que la administración deba notificar al solicitante, y cumplirá los efectos de notificación válida, 
                                    conforme art. 59 y 61 del Decreto 1510/GCABA/97.
                                        </label>
                                    </div>
                                    <div class="form-group" style="margin-left: 240px">
                                        <div class="col-sm-1">
                                            <asp:CheckBox ID="chkAceptarTerminos" runat="server" Display="Dynamic" onclick="checkClick(this);" MaxLength="50" Width="38px"
                                                ToolTip="Decreto" Checked="false" CssClass="form-control" EnableViewState="false"></asp:CheckBox>
                                        </div>
                                        <label class="control-label">
                                            <b>Aceptar términos y condiciones.</b>
                                        </label>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-12 text-center">
                                            <asp:Button ID="CreateUserButton" runat="server"
                                                CommandName="MoveNext" data-controlid="CreateUserButton" CssClass="btn btn-primary" Enabled="false"
                                                ValidationGroup="CreateUserWizard" OnClientClick="ocultarBoton(this);" Text="Registrarse"></asp:Button>
                                            <div id="pnlProcensadoRegistracion" class="display-none">
                                                <img src="../Content/img/app/Loading24x24.gif" style="margin-left: 10px" alt="loading" />
                                                <label class="dinline mleft5">Procesando</label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        </form>
                    </ContentTemplate>
                </asp:CreateUserWizardStep>
                <asp:CompleteWizardStep runat="server" ID="CompleteStep">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="updComplete" runat="server">
                            <ContentTemplate>

                                <div class="titulo-2">El usuario ha sido creado correctamente.</div>
                                <ul style="line-height: 25px">
                                    <li>Se ha enviado un e-mail a la cuenta de correo
                            <asp:Label ID="lblEmailSent" runat="server" ForeColor="Blue"></asp:Label>
                                        con las instrucciones para activar su cuenta de usuario.</li>
                                    <li>Debe activar su cuenta de usuario para poder operar en el sitio. </li>
                                </ul>
                                <div>
                                    <asp:Button ID="ContinueButton" runat="server" CausesValidation="False" CommandName="Continue" CssClass="btn btn-primary"
                                        Text="Continuar" OnClick="ContinueButton_Click" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:CompleteWizardStep>
            </WizardSteps>
        </asp:CreateUserWizard>


        <%--Popup de Error--%>
        <asp:Panel ID="pnlError" runat="server" CssClass="modalPopup" Style="display: none"
            Width="450px" DefaultButton="btnAceptarError">
            <table style="width: 100%; border-collapse: separate; border-spacing: 7px">
                <tr>
                    <td style="width: 80px">
                        <i class="imoon imoon-remove-circle fs64" style="color: #f00"></i>
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
    </div>

    <script type="text/javascript">


        $(document).ready(function () {

            $("[id*='NroPuerta']").autoNumeric("init", { aSep: "", mDec: 0, vMax: '99999' });
            //checkClick($("[id*='chkAceptarTerminos']"));   


        });

        function init_JS_updDatosPersonas() {

            $("[id*='txtCUIT']").autoNumeric("init", { aSep: "", mDec: 0, vMax: '9999999999999' });

        }

        function mostrarPopup() {
            $("#<%: pnlError.ClientID %>").css("display", "block");
        }

        function ocultarBoton(obj) {

            if (Page_ClientValidate("CreateUserWizard")) {
                $(obj).hide();
                $("[id*='pnlProcensadoRegistracion']").switchClass("display-none", "display-block");
            }
            return true;
        }
        function checkClick(c) {
            if (c.checked)
                $("[data-controlid='CreateUserButton']").removeAttr("disabled")
            else
                $("[data-controlid='CreateUserButton']").attr("disabled", "disabled")
        }

    </script>
</asp:Content>
