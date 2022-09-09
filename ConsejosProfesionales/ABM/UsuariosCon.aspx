<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsuariosCon.aspx.cs" MasterPageFile="~/Site.Master" Inherits="ConsejosProfesionales.ABM.UsuariosCon" %>

<asp:Content ContentPlaceHolderID="FeaturedContent" runat="server" ID="Featured">
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">


    <div style="text-align: center;">

        <asp:Panel ID="pnlPage" runat="server" CssClass="PageContainer">
            <h2>Búsqueda de Usuarios del Consejo
            </h2>
            <hr />

            <%--Contenido--%>
            <asp:Panel ID="pnlContenido" runat="server" CssClass="box-contenido" BackColor="White">

                <p class="mtop10">
                    Desde aqu&iacute; podr&aacute; consultar los profesionales.<br />
                    Ver el estado en que se encuentran y trabajar con cada uno.
                </p>
                <br />
                <asp:UpdatePanel ID="updBuscar" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnl01" runat="server" DefaultButton="btnBuscar" CssClass="box-panel">
                            <div style="margin: 20px; margin-top: -5px">
                                <div style="margin-top: 5px; color: #377bb5">
                                    <h4><i class="imoon imoon-search" style="margin-right: 10px"></i>Carga de Busqueda</h4>
                                    <hr />
                                </div>
                            </div>
                            <div style="margin-left: 18%">
                                <div class="form-horizontal ">
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" Text="Nombre de Usuario:" CssClass="form-label  col-sm-4"></asp:Label>

                                        <asp:TextBox ID="txtUsername_buscar" runat="server" Width="200px" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-horizontal ">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" Text="Apellido/s:" CssClass="form-label col-sm-4"></asp:Label>

                                        <asp:TextBox ID="txtApellido_buscar" runat="server" Width="300px" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-horizontal ">
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" Text="Nombre/s:" CssClass="form-label col-sm-4"></asp:Label>

                                        <asp:TextBox ID="txtNombres_buscar" runat="server" Width="300px" MaxLength="70" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <br />
                            <div class="form-horizontal">
                                <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-primary pull-left" ValidationGroup="Buscar" OnClick="btnBuscar_Click">
                                                    <i class="imoon imoon-search"></i>
                                                    <span class="text">Buscar</span>
                                </asp:LinkButton>
                                <div class="pull-left">

                                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="updBuscar"
                                        runat="server" DisplayAfter="0">
                                        <ProgressTemplate>
                                            <img src="../Common/Images/Controles/Loading24x24.gif" alt="" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>

                            </div>

                        </asp:Panel>
                        <br />

                        <div style="width: 100%; text-align: right;">
                            <asp:LinkButton ID="btnAgregarUsuario" runat="server" CssClass="btn btn-default text-center" OnClick="btnAgregarUsuario_Click" Title="Ingresar Profesional">
                                <i class="imoon imoon-plus"></i>
                                <span class="text">Ingresar Usuario</span>
                            </asp:LinkButton>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

                <asp:UpdatePanel ID="updGrillaResultados" runat="server">
                    <ContentTemplate>

                        <table class="titulo-6b" style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td>Listado de Usuarios
                            <asp:Label ID="lblCantResultados" runat="server" Style="padding-left: 10px; font-size: small; font-style: italic"></asp:Label>

                                </td>

                            </tr>
                        </table>

                        <asp:Panel ID="pnlResultados" runat="server">

                            <asp:GridView ID="grdUsuarios" runat="server" AutoGenerateColumns="false" DataKeyNames="userid" CssClass="table table-bordered mtop5"
                                Width="100%" GridLines="None" AllowPaging="true" PageSize="50" CellPadding="3"
                                OnPageIndexChanging="grdUsuarios_PageIndexChanging">
                                <HeaderStyle CssClass="grid-header" />
                                <RowStyle CssClass="grid-row" />
                                <AlternatingRowStyle BackColor="#efefef" />
                                <Columns>


                                    <asp:BoundField DataField="username" HeaderText="Usuario" ItemStyle-CssClass="text-left" ItemStyle-Width="100px" />
                                    <asp:BoundField DataField="Apellido" HeaderText="Apellido/s" ItemStyle-CssClass="text-left" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre/s" ItemStyle-CssClass="text-left" ItemStyle-Width="150px" />
                                    <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-CssClass="text-left" ItemStyle-Width="150px" />

                                    <%--Eliminar Usuarios--%>

                                    <asp:TemplateField ItemStyle-Width="80px" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-left" HeaderText="Acciones">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditarUsuario" runat="server" title="Editar Usuario" CssClass="btnEdit" Width="25px" CommandArgument='<%# Eval("userid") %>' OnClick="btnEditarUsuario_Click">
                                                 <i class="imoon imoon-pencil2" style="color:#377bb5"></i></asp:LinkButton>

                                            <asp:LinkButton ID="btnEliminarUsuario" title="Eliminar" runat="server" Width="25px"
                                                CommandName="EliminarUsuarios" CommandArgument='<%# Eval("userid") %>'
                                                OnClientClick="return confirm('¿Esta seguro que desea eliminar el usuario?');"
                                                OnClick="btnEliminarUsuario_Click"
                                                Text="Eliminar">   <i class="imoon imoon-close" style="color:#377bb5"></i></asp:LinkButton>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <PagerTemplate>
                                    <asp:Panel ID="pnlpager" runat="server" Style="padding: 10px; text-align: center; border-top: solid 1px #e1e1e1">
                                        <asp:Button ID="cmdAnterior" runat="server" Text="<< Anterior" OnClick="cmdAnterior_Click"
                                            CssClass="btnPagerGrid" Width="100px" />
                                        <asp:Button ID="cmdPage1" runat="server" Text="1" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                        <asp:Button ID="cmdPage2" runat="server" Text="2" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                        <asp:Button ID="cmdPage3" runat="server" Text="3" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                        <asp:Button ID="cmdPage4" runat="server" Text="4" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                        <asp:Button ID="cmdPage5" runat="server" Text="5" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                        <asp:Button ID="cmdPage6" runat="server" Text="6" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                        <asp:Button ID="cmdPage7" runat="server" Text="7" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                        <asp:Button ID="cmdPage8" runat="server" Text="8" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                        <asp:Button ID="cmdPage9" runat="server" Text="9" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                        <asp:Button ID="cmdPage10" runat="server" Text="10" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                        <asp:Button ID="cmdPage11" runat="server" Text="11" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                        <asp:Button ID="cmdPage12" runat="server" Text="12" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                        <asp:Button ID="cmdPage13" runat="server" Text="13" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                        <asp:Button ID="cmdPage14" runat="server" Text="14" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                        <asp:Button ID="cmdPage15" runat="server" Text="15" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                        <asp:Button ID="cmdPage16" runat="server" Text="16" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                        <asp:Button ID="cmdPage17" runat="server" Text="17" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                        <asp:Button ID="cmdPage18" runat="server" Text="18" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                        <asp:Button ID="cmdPage19" runat="server" Text="19" OnClick="cmdPage" CssClass="btnPagerGrid" Width="22px" />
                                        <asp:Button ID="cmdSiguiente" runat="server" Text="Siguiente >>" OnClick="cmdSiguiente_Click"
                                            CssClass="btnPagerGrid" Width="100px" />
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


    <asp:Panel ID="pnlInformacion" runat="server" CssClass="modal modal-transparent fade in" DefaultButton="btnAceptarInformacion">
        <div class="modal-dialog">

            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                </div>
                <div class="modal-body">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 80px">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Common/Images/Controles/info64x64.png" />
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblmpeInfo" runat="server" Style="color: Black"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <asp:Button ID="btnAceptarInformacion" runat="server" CssClass="btnOK" Text="Aceptar" data-dismis="modal" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlSuccess" runat="server" CssClass="modal fade" DefaultButton="btnAceptarSuccess">
        <div class="modal-dialog">

            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">X</span><span class="sr-only">Close</span></button>
                </div>
                <div class="modal-body">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 80px">
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Common/Images/Controles/info64x64.png" />
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdpnlSuccess" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblSuccess" runat="server" Style="color: Black"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <asp:Button ID="btnAceptarSuccess" runat="server" CssClass="btnOK" Text="Aceptar"
                                    Width="100px" OnClientClick="return ocultarPopup('pnlSuccess');" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlDatosUsuario" runat="server" CssClass="modal modal-transparent fade in" DefaultButton="btnAceptarDatosUsuario">

        <div class="modal-dialog">

            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                    <h4 class="modal-title" style="margin-top: -8px">Agregar Usuario</h4>
                </div>
                <div class="modal-body mleft40">
                    <asp:UpdatePanel ID="updDatosUsuario" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hid_userid" runat="server" />
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <asp:Label ID="lblusername" runat="server" Text="Nombre de Usuario:" CssClass="col-sm-4 control-label"></asp:Label>
                                    <asp:TextBox ID="txtusername" runat="server" MaxLength="30" Width="150px" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div>
                                <asp:RegularExpressionValidator ID="ReqValidarExptxtUsername" runat="server" ControlToValidate="txtusername" Display="Dynamic"
                                    ErrorMessage="El Nombre de Usuario solo puede contener letras y números y una longitud minima de 6 caracteres" SetFocusOnError="True"
                                    CssClass="error-label"
                                    ValidationExpression="^[a-zA-Z0-9\s]{6,}$" ValidationGroup="DatosUsuario"></asp:RegularExpressionValidator>
                                <asp:CustomValidator ID="CusValusername" runat="server" CssClass="error-label" Display="Dynamic"
                                    ValidationGroup="DatosUsuario" OnServerValidate="CusValusername_ServerValidate">
                                </asp:CustomValidator>
                            </div>
                            <div class="form-horizontal">
                                <div class="form-group">

                                    <asp:Label ID="Label5" runat="server" Text="Apellido/s:" CssClass="col-sm-4 control-label"></asp:Label>
                                    <asp:TextBox ID="txtApellido" runat="server" MaxLength="50" Width="250px" CssClass="form-control"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="El Apellido solo puede contener letras"
                                        ControlToValidate="txtApellido" ValidationExpression="^[A-Za-z\s]*$"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div>
                                <asp:RequiredFieldValidator ID="ReqtxtApellido" runat="server" ControlToValidate="txtApellido" Display="Dynamic"
                                    ErrorMessage="Debe ingresar el/los Apellido/s." SetFocusOnError="True"
                                    ValidationGroup="DatosUsuario" CssClass="error-label"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <asp:Label ID="Label6" runat="server" Text="Nombre/s:" CssClass="col-sm-4 control-label"></asp:Label>
                                    <asp:TextBox ID="txtNombre" runat="server" MaxLength="50" Width="250px" CssClass="form-control"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="El Nombre solo puede contener letras"
                                        ControlToValidate="txtNombre" ValidationExpression="^[A-Za-z\s]*$"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div>
                                <asp:RequiredFieldValidator ID="ReqtxtNombre" runat="server" ControlToValidate="txtNombre" Display="Dynamic"
                                    ErrorMessage="Debe ingresar el/los Nombre/s." SetFocusOnError="True"
                                    ValidationGroup="DatosUsuario" CssClass="error-label"></asp:RequiredFieldValidator>

                            </div>
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <asp:Label ID="Label4" runat="server" Text="E-Mail:" CssClass="col-sm-4 control-label"></asp:Label>

                                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="50" Width="250px" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div>
                                <asp:RequiredFieldValidator ID="RepEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic"
                                    ErrorMessage="Debe ingresar la dirección de correo." SetFocusOnError="True"
                                    ValidationGroup="DatosUsuario" CssClass="error-label"></asp:RequiredFieldValidator>

                                <asp:RegularExpressionValidator ID="EmailRegEx" runat="server" ControlToValidate="txtEmail" Display="Dynamic"
                                    ErrorMessage="E-mail no tiene un formato válido. Ej: nombre@servidor.com" SetFocusOnError="True" CssClass="error-label"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="DatosUsuario"></asp:RegularExpressionValidator>
                            </div>
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <asp:Label ID="Label7" runat="server" Text="Teléfono:" CssClass="col-sm-4 control-label"></asp:Label>

                                    <asp:TextBox ID="txtCelular" runat="server" MaxLength="50" Width="150px" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <asp:Label ID="lblUsuarioBloqueado" runat="server" Text="¿Usuario Inactivo?:" CssClass="col-sm-4 control-label"> </asp:Label>

                                    <asp:CheckBox ID="chkUsuarioBloqueado_datos" runat="server" CssClass="col-sm-2" Style="margin-left: -15px; margin-top: 10px" />
                                </div>
                            </div>
                            <br />
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <asp:Label ID="Label8" runat="server" Text="Perfiles asignados:" CssClass="col-sm-4 control-label col-sm-pull-1" Font-Bold="true"></asp:Label>
                                    <br />
                                    <hr style="margin-right: 30px" />
                                    <div class="checkbox mleft40">
                                        <asp:CheckBoxList ID="chkPerfiles" runat="server" Width="100%" RepeatDirection="Vertical"
                                            RepeatLayout="Table" RepeatColumns="1" CellPadding="3" CellSpacing="0" CssClass="col-sm-2" Font-Size="15px" Style="margin-left: -15px;">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            <hr style="margin-right: 10px" />

                            <asp:Panel ID="pnlReenvioClave" runat="server">
                                <asp:LinkButton ID="reenvioclave" runat="server" Text="Reenvio Contraseña" OnClick="reenvioclave_Click"></asp:LinkButton>
                            </asp:Panel>
                            <asp:Panel ID="pnlNotaEmail" runat="server" CssClass="nota">
                                <b>Nota:</b> Al finalizar el proceso y generar el usuario, <b>la contraseña será enviada por email a la dirección ingresada</b>. Por favor verifique que la misma se encuentre escrita de manera correcta."
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
        </div>
    </asp:Panel>

</asp:Content>
