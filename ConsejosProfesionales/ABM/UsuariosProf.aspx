<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsuariosProf.aspx.cs" MasterPageFile="~/Site.Master" Inherits="ConsejosProfesionales.ABM.UsuariosProf" %>

<asp:Content ContentPlaceHolderID="FeaturedContent" runat="server" ID="Featured">
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <%: Scripts.Render("~/bundles/autoNumeric") %>
    <%: Scripts.Render("~/bundles/select2") %>
    <%: Styles.Render("~/bundles/select2Css") %>

    <div style="text-align: center;">
        <asp:Panel ID="pnlPage" runat="server" CssClass="PageContainer">
            <%--Contenido--%>
            <asp:Panel ID="pnlContenido" runat="server" CssClass="box-contenido" BackColor="White">
                <h2>Búsqueda de Usuarios
                </h2>
                <hr />
                <p class="mtop10">
                    Acá podrás crear tu usuario, chequear que estés correctamente registrado y, también, buscar si sabrás si estás activo o no.
                </p>
                <br />

                <asp:Panel ID="pnl01" runat="server" DefaultButton="btnBuscar" CssClass="box-panel">
                    <div style="margin: 20px; margin-top: -5px">
                        <div style="margin-top: 5px; color: #377bb5">
                            <h4><i class="imoon imoon-search" style="margin-right: 10px"></i>Carga de Busqueda</h4>
                            <hr />
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" Text="Nombre de Usuario:" CssClass="control-label col-sm-2"></asp:Label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtUsername" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                </div>
                                <asp:Label ID="Label2" runat="server" Text="Nro. de Matrícula" CssClass="control-label col-sm-2"></asp:Label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtNroMatricula" runat="server" MaxLength="8" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" Text="Apellido y nombre:" CssClass="control-label col-sm-2"></asp:Label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtApeNom" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                </div>
                                <asp:Label ID="Label6" runat="server" Text="Dado de Baja:" CssClass="control-label col-sm-2"></asp:Label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlProfBajaLogica" runat="server" CssClass="form-control">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Text="No" Value="false" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Si" Value="true"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="form-group">

                                <asp:Label ID="Label7" runat="server" Text="Perfil:" CssClass="control-label col-sm-2"></asp:Label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="ddlPerfil" runat="server" CssClass=" form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="updBuscar" runat="server">
                        <ContentTemplate>
                            <div style="width: 100%; text-align: center">
                                <div style="display: table; margin: auto">
                                    <div style="display: table-row">
                                        <div style="display: table-cell">
                                            <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-primary" ValidationGroup="Buscar"
                                                OnClick="btnBuscar_Click">
                                                    <i class="imoon imoon-search"></i>
                                                    <span class="text">Buscar</span>
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="display: table-cell">
                                <asp:UpdateProgress ID="UpdateProgress3" AssociatedUpdatePanelID="updBuscar"
                                    runat="server" DisplayAfter="0">
                                    <ProgressTemplate>
                                        <img src="../Common/Images/Controles/Loading24x24.gif" alt="" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
                <br />

                <asp:UpdatePanel ID="updGrillaResultados" runat="server">
                    <ContentTemplate>
                        <div class="titulo-6">
                            Listado de Usuarios
                            <asp:Label ID="lblCantResultados" runat="server" Style="padding-left: 10px; font-size: small; font-style: italic"></asp:Label>
                        </div>
                        <asp:Panel ID="pnlResultados" runat="server">

                            <asp:GridView
                                ID="grdUsuarios"
                                runat="server"
                                AutoGenerateColumns="false"
                                DataKeyNames="userid"
                                Width="100%"
                                GridLines="None"
                                OnDataBound="grdUsuarios_DataBound"
                                AllowPaging="true"
                                PageSize="50"
                                ItemType="DataTransferObject.ProfesionalDTO"
                                CellPadding="3"
                                CssClass="table table-bordered mtop5"
                                OnPageIndexChanging="grdUsuarios_PageIndexChanging"
                                OnRowDataBound="grdUsuarios_RowDataBound"
                                SelectMethod="grdUsuarios_GetData">
                                <HeaderStyle CssClass="grid-header" />
                                <RowStyle CssClass="grid-row" />
                                <AlternatingRowStyle BackColor="#efefef" />
                                <Columns>

                                    <asp:BoundField DataField="Matricula" HeaderText="Matrícula" ItemStyle-CssClass="text-center" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="Apenom" HeaderText="Apellido y Nombre" ItemStyle-CssClass="text-left" ItemStyle-Width="220px" />
                                    <asp:BoundField DataField="CUIT" HeaderText="CUIT/CUIL" ItemStyle-Width="80px" ItemStyle-CssClass="text-left" />
                                    <asp:BoundField DataField="UserAspNet.UserName" HeaderText="Usuario" ItemStyle-CssClass="text-left" ItemStyle-Width="80px" />

                                    <%--lista de perfiles--%>
                                    <asp:TemplateField HeaderText="Perfiles" ItemStyle-Width="200px" ItemStyle-CssClass="text-left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPerfil" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--Eliminar Usuarios--%>
                                    <asp:TemplateField ItemStyle-Width="85px" HeaderText="Acciones" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="btnEditarUsuario" runat="server" title="Editar Usuario" CommandArgument='<%# Item.UserId %>'
                                                OnClick="btnEditarUsuario_Click"><i class="imoon imoon-pencil2" style="color:#377bb5"></i></asp:LinkButton>


                                            <asp:LinkButton ID="btnEliminarUsuario" runat="server" title="Eliminar Usuario" CommandName="EliminarUsuarios" CommandArgument='<%# Eval("userid") %>'
                                                Visible='<%# Item.UserAspNet != null ? !Item.UserAspNet.IsLockedOut : false  %>'
                                                OnClientClick="return confirm('¿Esta seguro que desea eliminar el usuario?');" OnClick="btnEliminarUsuario_Click"
                                                Text="Eliminar"><i class="imoon imoon-close" style="color:#377bb5"></i></asp:LinkButton>

                                            <asp:LinkButton ID="btnHabilitarUsuario" runat="server" title="Habilitar Usuario"
                                                CommandArgument='<%# Item.UserId %>' Visible='<%# Item.UserAspNet != null ?  Item.UserAspNet.IsLockedOut : false %>' Text="Habilitar" OnClick="btnHabilitarUsuario_Click">
                                                        <i class="imoon imoon-pencil2" style="color:#377bb5"></i></asp:LinkButton>


                                            <asp:LinkButton ID="btnGenerarUsuario" runat="server" title="Generar Usuario" CommandArgument='<%# Item.Id %>'
                                                OnClick="btnGenerarUsuario_Click"> <i class="imoon imoon-plus" style="color:#377bb5"></i></asp:LinkButton>


                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <PagerTemplate>
                                    <asp:Panel ID="pnlpager" runat="server" Style="padding: 10px; text-align: center; border-top: solid 1px #e1e1e1">
                                        <asp:LinkButton ID="cmdAnterior" runat="server" Text="<<" title="Anterior" OnClick="cmdAnterior_Click"
                                            CssClass="btn btn-default" Width="35px" />
                                        <asp:LinkButton ID="cmdPage1" runat="server" Text="1" OnClick="cmdPage" CssClass="btn" />
                                        <asp:LinkButton ID="cmdPage2" runat="server" Text="2" OnClick="cmdPage" CssClass="btn" />
                                        <asp:LinkButton ID="cmdPage3" runat="server" Text="3" OnClick="cmdPage" CssClass="btn" />
                                        <asp:LinkButton ID="cmdPage4" runat="server" Text="4" OnClick="cmdPage" CssClass="btn" />
                                        <asp:LinkButton ID="cmdPage5" runat="server" Text="5" OnClick="cmdPage" CssClass="btn" />
                                        <asp:LinkButton ID="cmdPage6" runat="server" Text="6" OnClick="cmdPage" CssClass="btn" />
                                        <asp:LinkButton ID="cmdPage7" runat="server" Text="7" OnClick="cmdPage" CssClass="btn" />
                                        <asp:LinkButton ID="cmdPage8" runat="server" Text="8" OnClick="cmdPage" CssClass="btn" />
                                        <asp:LinkButton ID="cmdPage9" runat="server" Text="9" OnClick="cmdPage" CssClass="btn" />
                                        <asp:LinkButton ID="cmdPage10" runat="server" Text="10" OnClick="cmdPage" CssClass="btn" />
                                        <asp:LinkButton ID="cmdPage11" runat="server" Text="11" OnClick="cmdPage" CssClass="btn" />
                                        <asp:LinkButton ID="cmdPage12" runat="server" Text="12" OnClick="cmdPage" CssClass="btn" />
                                        <asp:LinkButton ID="cmdPage13" runat="server" Text="13" OnClick="cmdPage" CssClass="btn" />
                                        <asp:LinkButton ID="cmdPage14" runat="server" Text="14" OnClick="cmdPage" CssClass="btn" />
                                        <asp:LinkButton ID="cmdPage15" runat="server" Text="15" OnClick="cmdPage" CssClass="btn" />
                                        <asp:LinkButton ID="cmdPage16" runat="server" Text="16" OnClick="cmdPage" CssClass="btn" />
                                        <asp:LinkButton ID="cmdPage17" runat="server" Text="17" OnClick="cmdPage" CssClass="btn" />
                                        <asp:LinkButton ID="cmdPage18" runat="server" Text="18" OnClick="cmdPage" CssClass="btn" />
                                        <asp:LinkButton ID="cmdPage19" runat="server" Text="19" OnClick="cmdPage" CssClass="btn" />
                                        <asp:LinkButton ID="cmdSiguiente" runat="server" Text=">>" title="Siguiente" OnClick="cmdSiguiente_Click"
                                            CssClass="btn btn-default" Width="35px" />
                                        <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="updGrillaResultados"
                                            runat="server" DisplayAfter="0">
                                            <ProgressTemplate>
                                                <img src="../Common/Images/Controles/Loading24x24.gif" alt="" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        </ContentTemplate>                                                                                                                    
                                    </asp:Panel>
                                </PagerTemplate>
                                <EmptyDataTemplate>
                                    <div>
                                        No se encontraron usuarios con los filtros ingresados.<br />
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </asp:Panel>
        </asp:Panel>
    </div>

    <asp:Panel ID="pnlInformacion" runat="server" class="modal modal-transparent fade in">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                </div>
                <div class="modal-body">
                    <table style="width: 100%">
                        <tr>

                            <td>
                                <asp:UpdatePanel ID="updmpeInfo" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblmpeInfo" runat="server" Style="color: Black"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="btnAceptarInformacion" runat="server" CssClass="btnOK" Text="Aceptar" Width="100px" data-dismiss="modal" />

                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:UpdatePanel ID="UpdPnlSuccess" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlSuccess" runat="server" CssClass="modal fade">
                <div class="modal-dialog">

                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">X</span><span class="sr-only">Close</span></button>
                        </div>
                        <div class="modal-body">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSuccess" runat="server" Style="color: Black"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center">
                                        <input type="button" id="btnAceptarSuccess" class="btnOK" value="Aceptar"
                                            data-dismiss="modal" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
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

                                <asp:HiddenField ID="hid_userid" runat="server" />
                                <asp:HiddenField ID="hid_id_profesional" runat="server" />


                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label ID="Label5" runat="server" Text="Apellido y Nombres:" CssClass="control-label col-sm-4 "></asp:Label>

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

                                        <asp:TextBox ID="txtEmail_datos" runat="server" MaxLength="100" Width="250px" CssClass="form-control col-sm-5"></asp:TextBox>
                                    </div>
                                </div>
                                <div>
                                    <asp:RequiredFieldValidator ID="RepEmail" runat="server" ControlToValidate="txtEmail_datos" Display="Dynamic"
                                        ErrorMessage="Debe ingresar la dirección de correo." SetFocusOnError="True"
                                        ValidationGroup="DatosUsuario" CssClass="error-label"></asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ID="EmailRegEx" runat="server" ControlToValidate="txtEmail_datos" Display="Dynamic"
                                        ErrorMessage="E-mail no tiene un formato válido. Ej: nombre@servidor.com" SetFocusOnError="True" CssClass="error-label"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="DatosUsuario"></asp:RegularExpressionValidator>
                                </div>
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <asp:Label ID="lblUsuarioBloqueado" runat="server" Text="¿Usuario Bloqueado?:" CssClass="control-label col-sm-4"></asp:Label>

                                        <asp:CheckBox ID="chkUsuarioBloqueado_datos" runat="server" CssClass="control-label col-sm-1" Style="margin-left: -20px" />
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
                                                <asp:DropDownList ID="ddlCalificacion" runat="server">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                                <hr />


                                <asp:Panel ID="pnlReenvioClave" runat="server">
                                    <h5>Recupero de Contraseña
                                    </h5>

                                    <asp:LinkButton ID="reenvioclave" runat="server" Text="Reenvio Contraseña" OnClick="reenvioclave_Click"></asp:LinkButton>
                                </asp:Panel>

                                <asp:Panel ID="pnlNotaEmail" runat="server" CssClass="nota">
                                    <b>Nota:</b> Al finalizar el proceso y generar el usuario, <b>la contraseña será enviada por email a la dirección ingresada</b>. Por favor verifique que la misma se encuentre escrita de manera correcta."
                                </asp:Panel>
                                <hr />
                                <asp:Panel runat="server" ID="pnlBlanqueo">
                                    <h5>Blanquear Contraseña
                                    </h5>
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <asp:Label ID="Label8" runat="server" Text="Contraseña:" CssClass="control-label col-sm-4"></asp:Label>
                                            <asp:TextBox ID="txtPass1" runat="server" Width="150px" CssClass="form-control col-sm-4" ValidationGroup="blanqueo"></asp:TextBox>                                            
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"  ValidationGroup="blanqueo"  ErrorMessage="Mínimo ocho caracteres" ControlToValidate="txtPass1" ValidationExpression = "^[\s\S]{8,}$"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="blanqueo" ErrorMessage="Debe ingresar la contraseña" ControlToValidate="txtPass1"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <asp:Label ID="Label9" runat="server" Text="Repita Contraseña:" CssClass="control-label col-sm-4"></asp:Label>
                                            <asp:TextBox ID="txtPass2" runat="server" Width="150px" CssClass="form-control col-sm-4 "></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Debe repetir la contraseña" ValidationGroup="blanqueo" ControlToValidate="txtPass2"></asp:RequiredFieldValidator>
                                        </div>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Las contraseñas no coinciden" ControlToCompare="txtPass1" ControlToValidate="txtPass2" Operator="Equal" Type="String" ValidationGroup="blanqueo"></asp:CompareValidator>
                                    </div>
                                    <div class="form-horizontal">
                                        <asp:Button ID="btnBlanqueo" runat="server" Text="Blanquear Contraseña" OnClick="lnkBlanqueo_Click" ValidationGroup="blanqueo" />
                                    </div>
                                </asp:Panel>
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

                                                <asp:Button ID="btnAceptarDatosUsuario" runat="server" CssClass="btn btn-primary" Text="Aceptar"
                                                    Width="90px" ValidationGroup="DatosUsuario" OnClick="btnAceptarDatosUsuario_Click" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnCancelarDatosUsuario" CssClass="btn btn-default" runat="server" data-dismiss="modal"
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
    <script type="text/javascript">

        $(document).ready(function () {
            init_JS_updBuscarUbicacion();
        });

        function init_JS_updBuscarUbicacion() {

            $("#<%: ddlProfBajaLogica.ClientID %>").select2({
                placeholder: "Todos",
                allowClear: true,
            });

            $("#<%: ddlPerfil.ClientID %>").select2({
                placeholder: "Todos",
                allowClear: true,
            });

        }
    </script>
</asp:Content>
