<%@ Page Title="Anexo Técnico" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AnexoProfesionales._Default" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    
          <%--Login--%>
  <asp:LoginView ID="LoginView1" runat="server">
        <AnonymousTemplate>
            
                <%--Usuario Anónimo (Single loguear)--%>
           
      <section>
                <div class="row">

                    <div class="col-sm-9 text-justify">
                                                   
        <h2 class="titulo1 text-center"  >Anexo Técnico</h2>
                            <p >
                        Desde aquí podrás confeccionar un Anexo Técnico para asociar a solicitudes de Autorización de Actividad Económica. 
                                        Para poder realizar cualquier trámite, es necesario poseer un usuario y su respectiva clave.
                                        Escribí tu nombre de usuario y contraseña para ingresar a tu cuenta.
                                        De no contar aún con usuario dirigite a tu Consejo Profesional  para que lo genere. <br />
                        </p>
                        
                        </div>
                    
                    <div class="col-sm-3" style="border-left: 2px solid #e2e2e2; padding-left: 34px; vertical-align: top">
                           <h2 class="titulo1 text-center" >Inicio de Sesión</h2>
                        
                        <asp:Panel ID="pnlLogin" runat="server" Style="margin-top: 10px">
                            <asp:Login ID="LoginControl" runat="server" OnLoggedIn="LoginControl_LoggedIn" OnLoginError="LoginControl_LoginError"  UserNameRequiredErrorMessage="El Nombre de usuario es requerido" PasswordRequiredErrorMessage="La contraseña es requerida."
                                FailureText="Nombre de usuario o contraseña incorrecta.<br />Por favor, intente nuevamente."
                                RememberMeText="Recordarme en este equipo." TitleText=""
                                 PasswordRecoveryText="Recuperar contraseña"
                                  PasswordRecoveryUrl="~/Account/ForgotPassword"  
                                 PasswordLabelText="Contraseña:"
                                 UserNameLabelText="Nombre de Usuario:"
                                InstructionText="Escriba su nombre de usuario y contraseña." UserNameBlokedText="Este usuario ah cagado verdulis" 
                                DestinationPageUrl="~/Default.aspx" OnAuthenticate="LoginControl_Authenticate" >
                 <LayoutTemplate>
            <asp:Panel ID="pnl" runat="server" DefaultButton="Login" CssClass="form-group">
              <div class="form-group">
              <label for="Nombre">Usuario:</label>
            
                    <asp:TextBox ID="UserName" runat="server" Width="100%" MaxLength="50" placeholder="Usuario" CssClass="form-control input-lg" name="UserName"></asp:TextBox>
                    <%--<input class="form-control input-lg" type="text" placeholder="Usuario" id="Nombre" required="">--%>
                    
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserName"
                                                            CssClass="field-validation-error" Display="Dynamic" ValidationGroup="LoginGroup"
                                                            SetFocusOnError="True">Debe ingresar el Nombre de Usuario.</asp:RequiredFieldValidator>
             
                  </div>
                  
              <div class="form-group">
              <label for="Nombre">Contraseña:</label>
                    <asp:TextBox ID="PassWord" runat="server" TextMode="Password" Width="100%" MaxLength="50" placeholder="Contraseña" CssClass="form-control input-lg"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="PassWord"
                                                            CssClass="field-validation-error" Display="Dynamic" ValidationGroup="LoginGroup"
                                                            SetFocusOnError="True">Debe ingresar la Contraseña.</asp:RequiredFieldValidator>
               
              </div>
                <div class="form-group" style="margin-top:20px">
               
                                                            <asp:HyperLink ID="lnkPasswordRecovery" runat="server" NavigateUrl="~/Account/ForgotPassword" >¿Olvidaste tu contraseña?</asp:HyperLink></strong>
                <div class="field-validation-error" style="text-align: center;">
                                                        <div class="field-validation-error">
                                                            <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                                                        </div>
                    </div>
                </div>
                     <div class="form-group">
                    
                                                        <asp:LinkButton ID="Login" runat="server" CommandName="Login" ValidationGroup="LoginGroup" CssClass="btn btn-primary btn-lg btn-block">
                                                        <i class="imoon imoon-user"></i>
                                                        <span class="text">Acceder</span>
                                                        </asp:LinkButton>
                    </div>
            
            </asp:Panel>
                                        
                                    </LayoutTemplate>
                            </asp:Login>
                        </asp:Panel>
                        </div>
                    </div>
          </section>
        </AnonymousTemplate>
        <%--Usuario Logeado--%>
     <LoggedInTemplate>
            <table style="width: 1140px; padding-top: 30px">
                <tr>
                    <td style="vertical-align: top">
        <h2 class="titulo1 text-center"  >Anexo Técnico</h2>
                        <p class="text-center" >
                            Desde aquí podrá iniciar trámites de solicitud para la Anexo Técnico.<br />
                            También podrá consultar el estado de los trámites en curso.<br />
                        </p>
                        
                        <br />

                        <h5 class="text-center" id="lblTexto" runat="server" style="font-weight: bold; color:red;">El presente Anexo Técnico debe ser certificado ante el Consejo Profesional correspondiente dentro de los 30 días hábiles de iniciado el mismo. Cumplido dicho plazo, operará su vencimiento y deberá confeccionar uno nuevo.</h5>

                        <div class="pright20" style="margin-bottom:20px; margin-top:-20px">
                            <nav class="navbar navbar-default" style="" role="navigation">
                            </nav>
                        </div>
                        <div class="view view-shortcuts view-id-shortcuts pright30" style="display:flex !important;justify-content:center !important;">
                                    
                                     <asp:LinkButton ID="linkDescargaInstHab" CssClass="col-md-3 col-sm-6 shortcut" runat="server" OnClick="linkDescargaInstAt_Click">
                                                <span class="bg-success-dk">
                                                    <span class="glyphicon imoon-download fs48"></span>
                                                </span>
                                                    <h4 style="text-transform:uppercase">Instructivo de Anexo Técnico de Autorización de Actividad Económica</h4>
                                        </asp:LinkButton>

                                     <asp:LinkButton ID="LinkButton2" CssClass="col-md-3 col-sm-6 shortcut" runat="server" OnClick="btnEncomienda_Click">
                                            <span class="bg-warning-dk">
                                                <span class="glyphicon imoon-file fs48"></span>
                                            </span>
                                            <h4 style="text-transform:uppercase">Creación de Anexo Técnico</h4>                                       
                                        </asp:LinkButton>
                         </div>

                    <td style="width: 100px; border-left: 1px solid #e2e2e2; padding-left: 34px; vertical-align: top">
                        <asp:Panel ID="pnlLogueado" runat="server" Width="200px">

                            <div class="view view-shortcuts view-id-shortcuts ptop30">
                                
                                <div class="row">
                                            <asp:LinkButton ID="LinkButton1" CssClass="shortcut" runat="server" PostBackUrl="~/Tramites/Bandeja">
                                               
                                                 <span class="bg-primary-lt">
                                                    <span class="glyphicon imoon-desktop fs48" ></span>
                                                </span>
                                                <h4>CONSULTA DE TRAMITES</h4>
                                                <p>Desde aquí podrá consultar cualquier trámite realizado en este sistema</p>
                                            </asp:LinkButton>
                                </div>
                            </div>
                            
                        </asp:Panel>
                    </td>
                </tr>
            </table>
    </LoggedInTemplate>
        </asp:LoginView>

    <%--Confirmar Nuevo AVUS--%>
    <div id="frmConfirmarNuevoAVUS" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content ">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top:-8px">Aviso</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon-remove-circle fs64 color-blue"></i>
                            </td>
                            <td style="vertical-align: middle; padding-left:10px">

                                <asp:HiddenField ID="hid_url_TramiteAVUS" runat="server" />
                                <label >Esta acci&oacute;n iniciará un nuevo trámite de Certificado de Aceites Vegetales Usados. ¿Está seguro de querer hacerlo?</label>

                                <ul class="mtop20">
                                    <li>
                                        Sr. Contribuyente: si su actividad es Sin Relevante Efecto y posee una habilitación anterior a la vigencia del Decreto 222/2012 y disposición 117/DGTALAPRA/2012, y por ese motivo no cuenta con Certificado
                                        de Aptitud Ambiental obtenida a través del trámite Web, solicite un turno para el Módulo de Atención Personalizada – Belgrano 1429 – Tel 4380-3800 en donde podrán digitalizar su certificado.
                                    </li>
                                    <li class="mtop10">
                                        Si posee Certificado de Aptitud Ambiental obtenida a través del trámite Web, y no recuerda el número de solicitud y código de seguridad del Certificado de Aptitud Ambiental (CAA) puede consultarlo en
                                        la “Bandeja de Trámites”, en la solicitud
                                        del CAA, o al pie del certificado de CAA, sino lo encuentra solicite un turno en el Módulo de Atención Personalizada, para que le indique cuál es su código de seguridad.
                                    </li>

                                        <li class="mtop10">
                                        Si usted NO posee un Certificado de CAA<br />  
                                        Por favor ingrese los datos para iniciar su CAA, mediante la opción <strong>CAA para destinos diferentes a una habilitación nueva</strong> De esta forma podrá tener obtener el número de solicitud y código de seguridad, y obtener los datos para su certificado
                                        de AVUS.

                                    </li>
                                </ul>

                                <div class="checkbox pull-right">
                                    <label>
                                        <asp:CheckBox ID="chkHeLeidoAVUS" runat="server" onclick="habilitarInicioTramiteAVUS();" />He le&iacute;do.
                                    </label>
                                </div>

                            </td>
                        </tr>
                    </table>

                </div>
                <div class="modal-footer mtop0 mleft20 mright20">

                    <asp:UpdatePanel ID="updConfirmarNuevoAVUS" runat="server">
                        <ContentTemplate>

                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updConfirmarNuevoAVUS">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />Procesando...
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div id="Div2" class="form-group">
                                    
                                    <asp:LinkButton ID="btnNuevoAVUS" runat="server" CssClass="btn btn-primary" Text="Sí" disabled="disabled"></asp:LinkButton>
                                    <button type="button" class="btn btn-default" data-dismiss="modal">No</button>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->


    <%--Confirmar Nuevo RPATO--%>
    <div id="frmConfirmarNuevoRPATO" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content ">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top:-8px">Aviso</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon-spam fs48 color-blue"></i>
                            </td>
                            <td style="vertical-align: middle; padding-left:10px">

                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                <label >Esta acci&oacute;n iniciará un nuevo trámite de Certificado de Residuos Patog&eacute;nicos. ¿Está seguro de querer hacerlo?</label>

                                <ul class="mtop20">
                                    <li>
                                        Sr. Contribuyente: si su actividad es Sin Relevante Efecto y posee una habilitaci&oacute;n anterior a la vigencia del Decreto 222/2012 y disposici&oacute;n 117/DGTALAPRA/2012, y por ese motivo no cuenta con Certificado
                                        de Aptitud Ambiental obtenida a trav&eacute;s del tr&aacute;mite Web, solicite un turno para el M&oacute;dulo de Atenci&oacute;n Personalizada – Belgrano 1429 – Tel 4380-3800 en donde podr&aacute;n digitalizar su certificado.
                                    </li>
                                    <li class="mtop10">
                                        Si posee Certificado de Aptitud Ambiental obtenida a trav&eacute;s del tr&aacute;mite Web, y no recuerda el n&uacute;mero de solicitud y c&oacute;digo de seguridad del Certificado de Aptitud Ambiental (CAA) puede consultarlo en
                                        la “Bandeja de Tr&aacute;mites”, en la solicitud
                                        del CAA, o al pie del certificado de CAA, sino lo encuentra solicite un turno en el M&oacute;dulo de Atenci&oacute;n Personalizada, para que le indique cu&aacute;l es su c&oacute;digo de seguridad.
                                    </li>

                                        <li class="mtop10">
                                        Si usted NO posee un Certificado de CAA<br />  
                                        Por favor ingrese los datos para iniciar su CAA, mediante la opci&oacute;n <strong>CAA para destinos diferentes a una habilitaci&oacute;n nueva</strong> De esta forma podr&aacute; tener obtener el n&uacute;mero de solicitud y c&oacute;digo de seguridad, y obtener los datos para su certificado
                                        de Residuos Patog&eacute;nicos.

                                    </li>
                                </ul>

                                <div class="checkbox pull-right">
                                    <label>
                                        <asp:CheckBox ID="chkHeLeidoRPATO" runat="server" onclick="habilitarInicioTramiteRPATO();" />He le&iacute;do.
                                    </label>
                                </div>


                            </td>
                        </tr>
                    </table>


                </div>
                <div class="modal-footer mtop0 mleft20 mright20">

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updConfirmarNuevoAVUS">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />Procesando...
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div id="Div3" class="form-group">
                                    
                                    <asp:LinkButton ID="btnNuevoRPATO" runat="server" CssClass="btn btn-primary" Text="Sí" disabled="disabled"></asp:LinkButton>
                                    <button type="button" class="btn btn-default" data-dismiss="modal">No</button>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->

    <div id="frmError" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top:-8px">Error</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon-remove-circle fs64" style="color: #f00"></i>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="updmpeInfo" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblError" runat="server" Style="color: Black"></asp:Label>
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
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <script type="text/ecmascript">

        function showfrmError() {

            $("#frmError").modal("show");
            return false;

        }

        function showfrmConfirmarNuevoAVUS(obj) {

            var postbackurl = "";

            if ($(obj).prop("id").indexOf("btnAVUSGenerador") >= 0) {
                postbackurl = "<%: ResolveUrl("~/Tramites/AVUS/NuevoTramite/1") %>";
            }
            else {
                postbackurl = "<%: ResolveUrl("~/Tramites/AVUS/NuevoTramite/2") %>";
            }


            $("#<%: chkHeLeidoAVUS.ClientID %>").prop("checked", false);
            $("#<%: btnNuevoAVUS.ClientID %>").prop("href", postbackurl);
            habilitarInicioTramiteAVUS();

            $("#frmConfirmarNuevoAVUS").modal("show");
            return false;
        }

        function showfrmConfirmarNuevoRPATO(obj) {

            var postbackurl = "";

            if ($(obj).prop("id").indexOf("btnRPATOGenerador") >= 0) {
                postbackurl = "<%: ResolveUrl("~/Tramites/RPATO/NuevoTramite/1") %>";
            }
            else {
                postbackurl = "<%: ResolveUrl("~/Tramites/RPATO/NuevoTramite/2") %>";
            }


            $("#<%: chkHeLeidoRPATO.ClientID %>").prop("checked", false);
            $("#<%: btnNuevoRPATO.ClientID %>").prop("href", postbackurl);
            habilitarInicioTramiteAVUS();

            $("#frmConfirmarNuevoRPATO").modal({
                backdrop: "static",
                show: true
            });
            return false;
        }

        function habilitarInicioTramite() {

            //if ($("#%: chkHeLeido.ClientID %>").prop("checked")) {
            //    $("#%: btnNuevoCAAOD.ClientID %>").prop("disabled", false)
            //}
            //else {
            //    $("#%: btnNuevoCAAOD.ClientID %>").prop("disabled", true)
            //}
        }

        function habilitarInicioTramiteAVUS() {

            if ($("#<%: chkHeLeidoAVUS.ClientID %>").prop("checked")) {
                $("#<%: btnNuevoAVUS.ClientID %>").removeAttr("disabled")
            }
            else {
                $("#<%: btnNuevoAVUS.ClientID %>").attr("disabled", "disabled")
            }

        }
        function habilitarInicioTramiteRPATO() {

            if ($("#<%: chkHeLeidoRPATO.ClientID %>").prop("checked")) {
                $("#<%: btnNuevoRPATO.ClientID %>").removeAttr("disabled")
            }
            else {
                $("#<%: btnNuevoRPATO.ClientID %>").attr("disabled", "disabled")
            }

        }
    </script>
    
</asp:Content>
