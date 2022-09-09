<%@ page title="Inicio" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" codebehind="Default.aspx.cs" inherits="ConsejosProfesionales._Default" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <asp:LoginView ID="lgnView" runat="server">
        <AnonymousTemplate>

            <%--Usuario Anónimo (Single loguear)--%>


            <div class="row">

                <div class="col-sm-9 text-justify">

                    <h2 class="titulo1 text-center">Consejos Profesionales</h2>

                    <p class="text-center">
                        Desde aquí podrás ingresar al portal de tu Consejo Profesional donde podrás validar todos aquellos trámites que lo requieran.
                             <br />
                    </p>
                </div>

                <div class="col-sm-3" style="border-left: 2px solid #e2e2e2; padding-left: 34px; vertical-align: top">
                    <h2 class="titulo1 text-center">Inicio de Sesión</h2>

                    <asp:Panel ID="pnlLogin" runat="server" Style="margin-top: 10px">
                        <asp:Login ID="LoginControl" runat="server" OnLoggedIn="LoginControl_LoggedIn" OnLoginError="LoginControl_LoginError" UserNameRequiredErrorMessage="El Nombre de usuario es requerido" PasswordRequiredErrorMessage="La contraseña es requerida."
                            FailureText="Nombre de usuario o contraseña incorrecta.<br />Por favor, intente nuevamente."
                            RememberMeText="Recordarme en este equipo." TitleText=""
                            PasswordRecoveryText="Recuperar contraseña"
                            PasswordRecoveryUrl="~/Account/ForgotPassword"
                            PasswordLabelText="Contraseña:"
                            UserNameLabelText="Nombre de Usuario:"
                            InstructionText="Escriba su nombre de usuario y contraseña." UserNameBlokedText="Este usuario ah cagado verdulis"
                            DestinationPageUrl="~/Default.aspx">
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
                                    <div class="form-group" style="margin-top: 20px">


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


        </AnonymousTemplate>
        <%--Usuario Logeado--%>
        <LoggedInTemplate>
            <div style="text-align: center;">

                <%--Logo--%>
                <div class="clearfix"></div>
                <br />
                <div class="pright20">
                    <nav role="navigation">
                        <div class="container-fluid">
                            <div class="text-center">
                                <div class="pbottom15">
                                    <h2>Administración</h2>
                                </div>
                            </div>
                        </div>
                    </nav>
                </div>

                <div class="view view-shortcuts view-id-shortcuts pright30" style="display: flex !important; justify-content: center !important;">

                    <asp:LinkButton ID="lnkABMProfesionales" CssClass="col-md-3 col-sm-6 shortcut" runat="server" PostBackUrl="~/ABM/BuscarProfesionales.aspx">
                                            <span class="bg-warning-lt">
                                                    <span class="glyphicon imoon-user4 fs48"></span>
                                                </span>
                                            <h4 style="text-transform:uppercase">ABM de Profesionales</h4>
                    </asp:LinkButton>

                    <%--<asp:LinkButton ID="lnkUsuariosProfesionales" CssClass="col-md-3 col-sm-6 shortcut" runat="server" PostBackUrl="~/ABM/UsuariosProf.aspx">
                                            <span class="bg-primary-lt">
                                                    <span class="glyphicon imoon-user4 fs48"></span>
                                                </span>
                                            <h4 style="text-transform:uppercase">Usuarios de Profesionales</h4>
                    </asp:LinkButton>--%>

                    <asp:LinkButton ID="lnkUsuariosConsejo" CssClass="col-md-3 col-sm-6 shortcut" runat="server" PostBackUrl="~/ABM/UsuariosCon.aspx">
                                            <span class="bg-success2-lt">
                                                    <span class="glyphicon imoon-user2 fs48"></span>
                                                </span>
                                            <h4 style="text-transform:uppercase">Usuarios del Consejo (internos)</h4>
                    </asp:LinkButton>


                </div>
                <hr />
                <div class="pright20">
                    <nav role="navigation">
                        <div class="container-fluid">
                            <div class="text-center">
                                <div>
                                    <h3>Certificación de Trámites</h3>
                                </div>
                            </div>
                        </div>
                    </nav>
                </div>


                <div class="view view-shortcuts view-id-shortcuts pright30" style="display: flex !important; justify-content: center !important;">

                    <asp:LinkButton ID="lnkSearchEncomiendas" CssClass="col-md-3 col-sm-6 shortcut" runat="server" PostBackUrl="~/Encomiendas/SearchEncomiendas.aspx">
                                            <span class="bg-warning-dk">
                                                    <span class="glyphicon imoon-suitcase fs48"></span>
                                                </span>
                                            <h4 style="text-transform:uppercase">Anexo de Habilitación</h4>
                    </asp:LinkButton>
                    <asp:LinkButton ID="lnkSearchEncomiendasExt" CssClass="col-md-3 col-sm-6 shortcut" runat="server" PostBackUrl="~/Encomiendas/SearchEncomiendasExt.aspx?tipo_certificado=4">
                                            <span class="bg-success2-dk">
                                                    <span class="glyphicon imoon-legal fs48"></span>
                                                </span>
                                            <h4 style="text-transform:uppercase">Encomienda de Ley 257</h4>
                    </asp:LinkButton>
                    <asp:LinkButton ID="lnkSearchEncomiendasAnt" CssClass="col-md-3 col-sm-6 shortcut" runat="server" PostBackUrl="~/Encomiendas/SearchEncomiendaAntenas.aspx">
                                            <span class="bg-pink-lt">
                                                    <span class="glyphicon imoon-rss fs48"></span>
                                                </span>
                                            <h4 style="text-transform:uppercase">Encomiendas de Antenas</h4>
                    </asp:LinkButton>
                    <asp:LinkButton ID="lnkSearchEncomiendasExtDe" CssClass="col-md-3 col-sm-6 shortcut" runat="server" PostBackUrl="~/Encomiendas/SearchEncomiendasExt.aspx?tipo_certificado=10">
                                            <span class="bg-violet-lt">
                                                    <span class="glyphicon imoon-road fs48"></span>
                                                </span>
                                            <h4 style="text-transform:uppercase">Encomienda de Demoledores y Excavadores</h4>
                    </asp:LinkButton>
                </div>

                <hr />

                <nav role="navigation">
                    <div class="container-fluid">
                        <div class="text-center">
                            <div>
                                <h4>Certificación de Datos <asp:Label ID="lblCountDirObraPend" Text="( 0 )" runat="server" style="background-color:red;color:white"></asp:Label> </h4>
                            </div>
                        </div>
                    </div>
                </nav>
            </div>


            <div class="view view-shortcuts view-id-shortcuts pright30" style="display: flex !important; justify-content: center !important;">

                <asp:LinkButton ID="lnkValidacionEncomiendaObra" CssClass="col-md-3 col-sm-6 shortcut" runat="server" PostBackUrl="~/Encomiendas/ValidacionEncomiendaObra.aspx">
                    <span class="bg-blue-dk">
                        <span class="glyphicon imoon-building fs48"></span>
                    </span>
                    <h4 style="text-transform:uppercase">Validación de Encomienda de Obra</h4>
                </asp:LinkButton>
            </div>

            </div>
        </LoggedInTemplate>
    </asp:LoginView>



</asp:Content>
