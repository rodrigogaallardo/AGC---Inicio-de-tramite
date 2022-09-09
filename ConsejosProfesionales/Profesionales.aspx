<%@ Page Title="Detalles del Usuario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profesionales.aspx.cs" Inherits="ConsejosProfesionales.Profesionales" %>


<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    
    <asp:LoginView ID="lgnView" runat="server">
        <AnonymousTemplate>

            <%--Usuario Anónimo (Single loguear)--%>


            <div class="row">

                <div class="col-sm-9 text-justify">

                    <h2 class="titulo1 text-center">Profesionales</h2>

                    <p class="text-center">
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
                             <br />
                    </p>
                </div>

                <div class="col-sm-3" style="border-left: 2px solid #e2e2e2; padding-left: 34px; vertical-align: top">
                    <h2 class="titulo1 text-center">Inicio de Sesión</h2>

                    <asp:Panel ID="pnlLogin" runat="server" Style="margin-top: 10px">
                        <asp:Login ID="LoginControl" runat="server" OnLoggedIn="LoginControl_LoggedIn" OnLoginError="LoginControl_LoginError" MembershipProvider="SqlMembershipProviderProfesionales" UserNameRequiredErrorMessage="El Nombre de usuario es requerido" PasswordRequiredErrorMessage="La contraseña es requerida."
                            FailureText="Nombre de usuario o contraseña incorrecta.<br />Por favor, intente nuevamente."
                            RememberMeText="Recordarme en este equipo." TitleText=""
                            PasswordRecoveryText="Recuperar contraseña"
                            PasswordRecoveryUrl="~/Account/ForgotPassword"
                            PasswordLabelText="Contraseña:"
                            UserNameLabelText="Nombre de Usuario:"
                            InstructionText="Escriba su nombre de usuario y contraseña." UserNameBlokedText="Este usuario ah cagado verdulis"
                            DestinationPageUrl="~/Profesionales.aspx">
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
          
        </LoggedInTemplate>
    </asp:LoginView>
    
            
<asp:UpdatePanel ID="UpdModificarDatos" runat="server" >
<ContentTemplate>      
           
                <%-- collapsible ubicaciones--%>
                <div id="box_profesional" class="accordion-group widget-box" style="background:#ffffff;">

                    <%-- titulo collapsible ubicaciones--%>
                    <div class="accordion-heading">
                        <a id="profesional_btnUpDown" data-parent="#collapse-group" href="#collapse_profesional"
                            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                            <div class="widget-title">
                                <span class="icon"><i class="imoon imoon-user4 fs16" style="color:#344882;"></i></span>
                                <h5>
                                   <asp:Label ID="lbl_profesional_tituloControl" runat="server" Text="Informaci&oacute;n Personal"></asp:Label></h5>
                                   <span class="btn-right"><i class="imoon imoon-chevron-down" style="color:#344882;"></i></span>
                            </div>
                        </a>
                    </div>

                    <%-- contenido del collapsible ubicaciones --%>
                    <div class="accordion-body collapse" id="collapse_profesional">   
                        <div class="widget-content">           
   <h3>
       Datos del Profesional
   </h3>
                        <asp:Panel ID="pnlDatos" runat="server" CssClass="form-horizontal" >

                                    
                                        <div class="form-group form-group-sm">
                                            <label class="control-label col-sm-2">Consejo:</label>
                                            <div class="col-sm-8">
                                                   <asp:TextBox ID="txtConsejoReq" CssClass="form-control" runat="server" Enabled="false" ></asp:TextBox>  
                                            </div>
                                        </div>
                                    

                                        <div class="form-group form-group-sm">
                                            <label class="control-label col-sm-2">Nro. Matrícula:</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtNroMatriculaReq" CssClass="form-control" runat="server"  Enabled="false" ></asp:TextBox>
                                                
                                            </div>
                                            <label class="control-label col-sm-2">Tipo y Nº de Doc.:</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtTipoYNroDocReq" CssClass="form-control" runat="server"  Enabled="false" ></asp:TextBox>  
                                                
                                            </div>
                                        </div>
                                    
                                        <div class="form-group form-group-sm">
                                            <label class="control-label col-sm-2">Apellido:</label>
                                                            <div class="col-sm-3" >
                                                                <asp:TextBox ID="txtApellidoReq" CssClass="form-control"  runat="server"  Enabled="false" ></asp:TextBox>                         
                                                            </div>
                                            <label class="control-label col-sm-2">Nombres:</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtNombreReq" CssClass="form-control" runat="server"  Enabled="false" ></asp:TextBox>                         
                                                            </div>
                                        </div>
                                    
                                        <div class="form-group form-group-sm">
                                                            <label class="control-label col-sm-2">C.U.I.T.:</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtCuitReq" CssClass="form-control" runat="server" Enabled="false" ></asp:TextBox>                         
                                                            </div>
                                            
                                                            <label class="control-label col-sm-2">Nº Ingresos Brutos:</label>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtNroIngresosBrutosReq" CssClass="form-control" runat="server"  Enabled="false" ></asp:TextBox>                         
                                        </div>
                                    </div>
                                    
                                    
                                        <div class="form-group form-group-sm">
                                            <label class="control-label col-sm-2">Calle:</label>
                                            <div class="col-sm-3" >
                                                                <asp:TextBox ID="txtCalleReq"  CssClass="form-control" runat="server" Enabled="false" ></asp:TextBox>   
                                            </div>
                                            <label class="control-label col-sm-2">Nro de Puerta:</label>
                                            <div class="col-sm-4">
                                                <div class="form-inline" >
                                                <asp:TextBox ID="txtNroReq" runat="server" CssClass="form-control" Width="80px" MaxLength="30" Enabled="false" ></asp:TextBox>
                                                    <label class="pleft5 pright5" style="font-weight:bold;color:#555555" >Piso:</label>
                                                <asp:TextBox ID="txtPisoReq" runat="server" CssClass="form-control" Width="60px" MaxLength="30" Enabled="false" ></asp:TextBox>
                                                    <label class="pleft5 pright5" style="font-weight:bold;color:#555555" >Depto:</label>
                                                <asp:TextBox ID="txtDeptoReq" runat="server" CssClass="form-control" Width="60px" MaxLength="30" Enabled="false" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>


                                        <div class="form-group form-group-sm">
                                            <label class="control-label col-sm-2">Provincia:</label>
                                                            <div class="col-sm-3" >
                                                                <asp:TextBox ID="txtProvinciaReq" CssClass="form-control" runat="server" Enabled="false" ></asp:TextBox>  
                                                                </div>
                                            
                                            <label class="control-label col-sm-2">Localidad:</label>
                                                            <div class="col-sm-3" >
                                                                <asp:TextBox ID="txtLocalidadReq" CssClass="form-control" runat="server" Enabled="false" ></asp:TextBox>  
                                                                </div>

                                        </div>

                                        <div class="form-group form-group-sm">
                                            <label class="control-label col-sm-2">Email:</label>
                                                            <div class="col-sm-3" >
                                                                <asp:TextBox ID="txtEmailReq" CssClass="form-control" runat="server"  Enabled="false" ></asp:TextBox> 
                                                                </div>

                                        </div>

                                        <div class="form-group form-group-sm">
                                            <label class="control-label col-sm-2">Telefono:</label>
                                                            <div class="col-sm-3" >
                                                                <asp:TextBox ID="txtTelefonoReq" CssClass="form-control" runat="server"  Enabled="false" ></asp:TextBox>  
                                                                </div>
                                            <label class="control-label col-sm-2">SMS:</label>
                                                            <div class="col-sm-3" >
                                                                <asp:TextBox ID="txtSmsReq" CssClass="form-control" runat="server" Enabled="false" ></asp:TextBox>   
                                        </div>
                                    </div>
                                    
                                        <div class="form-group form-group-sm">
                                            <label class="control-label col-sm-2">Matricula gasista:</label>
                                                            <div class="col-sm-3" >
                                                                <asp:TextBox ID="txtMatriculaMetrogas" CssClass="form-control" runat="server" Enabled="false" ></asp:TextBox> 
                                                                </div>
                                            <label class="control-label col-sm-2">Categoria:</label>
                                                            <div class="col-sm-3" >
                                                                <asp:TextBox ID="txtCategoriaMetrogas" CssClass="form-control" runat="server" Enabled="false" ></asp:TextBox> 
                                                                </div>

                                        </div>

                                </asp:Panel>
                            </div>
                    </div>
                </div>
               
                <div id="box_sistema" class="accordion-group widget-box" style="background:#ffffff;">
                    
                    <%-- titulo collapsible ubicaciones--%>
                    <div class="accordion-heading">
                        <a id="A1" data-parent="#collapse-group" href="#collapse_sistemas"
                            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                            <div class="widget-title">
                                <span class="icon"><i class="imoon imoon-list fs16" style="color:#344882;"></i></span>
                                <h5>Sistemas</h5>
                                   <span class="btn-right"><i class="imoon imoon-chevron-up" style="color:#344882;"></i></span>
                                </div>
                            </a>
                        </div>
                    
                    <%-- contenido del collapsible ubicaciones --%>
                    <div class="accordion-body collapse in" id="collapse_sistemas">
                        <div class="widget-content"> 
    <h3>
        Accesos a los Sistemas
    </h3>
                            <asp:GridView ID="grdSistemas" runat="server"
                                ItemType="DataTransferObject.RolesDTO" AutoGenerateColumns="false"  CssClass="table table-bordered mtop5"
                                Width="100%" GridLines="None"  AllowPaging="true" PageSize="50"  >
                                <Columns>
                                    <asp:TemplateField HeaderText="Perfiles Asignados" HeaderStyle-CssClass="text-center" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblweb" runat="server" Text='<%# Item.Description %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ingreso al Sistema" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" ItemStyle-Width="150px">
                                        <ItemTemplate>
                                            <a href='<%# Item.RoleName %>' target="_blank" >
                                                <i class="imoon imoon-circle-arrow-right fs32"></i>
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>

   </ContentTemplate>
  </asp:UpdatePanel>

    <script type="text/javascript">

        $(document).ready(function () {
            init_JS_Inicializar();
            
        });

        function init_JS_Inicializar() {

        }

        function bt_btnUpDown_collapse_click(obj) {
            var href_collapse = $(obj).attr("href");

            if ($(href_collapse).attr("id") != undefined) {
                if ($(obj).find("i.imoon-chevron-down").length > 0) {
                    $(obj).find("i.imoon-chevron-down").switchClass("imoon-chevron-down", "imoon-chevron-up", 0);
                }
                else {
                    $(obj).find("i.imoon-chevron-up").switchClass("imoon-chevron-up", "imoon-chevron-down", 0);
                }
            }
        }
    </script>

</asp:Content>