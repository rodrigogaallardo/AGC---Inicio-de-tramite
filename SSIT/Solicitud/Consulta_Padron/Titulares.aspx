<%@ Page Title="Consulta al Padrón - Titulares" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Titulares.aspx.cs" Inherits="SSIT.Solicitud.Consulta_Padron.Titulares" %>

<%@ Register Src="~/Solicitud/Consulta_Padron/Controls/Titulares.ascx" TagPrefix="uc1" TagName="Titulares" %>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">


    <%: Scripts.Render("~/bundles/autoNumeric") %>
    <%: Scripts.Render("~/bundles/select2") %>
    <%: Styles.Render("~/bundles/Select2Css") %>

    <%--ajax cargando ...--%>

    <div id="Loading" style="text-align: center; padding-bottom: 20px; margin-top: 120px">
        <table border="0" style="border-collapse: separate; border-spacing: 5px; margin: auto">
            <tr>
                <td>
                    <img src="<%: ResolveUrl("~/Content/img/app/Loading128x128.gif") %>" alt="" />
                </td>
            </tr>
            <tr>
                <td style="font-size: 24px">Cargando datos...
                </td>
            </tr>
        </table>
    </div>

    <asp:UpdatePanel ID="updCargarDatos" runat="server">
        <ContentTemplate>

            <asp:Button ID="btnCargarDatos" runat="server" OnClick="btnCargarDatos_Click" Style="display: none" />
            <asp:HiddenField ID="hid_id_solicitud" runat="server" />
            <asp:HiddenField ID="hid_id_encomienda" runat="server" />
            <asp:HiddenField ID="hid_return_url" runat="server" />
            <asp:HiddenField ID="hid_CargosFirPJ" runat="server" />
            <asp:HiddenField ID="hid_CargosFirSH" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--fin ajax cargando--%>

    <div id="page_content" Style="display:none">

        <h2>Titulares</h2>
        <hr />

        <div class="row">

            <div class="col-sm-1 mtop10" style="width: 25px">
                <i class="imoon imoon-info fs24" style="color:#377bb5"></i>
            </div>
            <div class="col-sm-11">

                <p class="pad10">
                    En este paso deber&aacute; ingresar los datos correspondientes a los Titulares.                    
                </p>
              
            </div>
        </div>

        <div id="box_titulares" class="box-panel">
                           <div style="margin:20px; margin-top:-5px"> 
                            <div style="color:#377bb5">                                 
                                        <h4><i class="imoon imoon-users" style="margin-right:10px"></i>Datos de los titulares</h4>    
                                        <hr />                   
                                    </div>
                              </div>
                    <asp:UpdatePanel ID="updShowAgregarPersonas" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row ptop10 pright15">
                                <div class="cols-sm-12 text-right">
                                    <asp:LinkButton ID="btnShowAgregarPF" runat="server" CssClass="btn btn-default" OnClick="btnShowAgregarPF_Click" >
                                        <i class="imoon imoon-user" style="color:#377bb5"></i>
                                        <span class="text">Agregar Persona F&iacute;sica</span>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnShowAgregarPJ" runat="server" CssClass="btn btn-default" OnClick="btnShowAgregarPJ_Click">
                                        <i class="imoon imoon-office" style="color:#377bb5"></i>
                                        <span class="text">Agregar Persona Jur&iacute;dica</span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <asp:UpdatePanel ID="updGrillaTitulares" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <%--Grilla de Titulares--%>
                            <div>
                                <strong>Titulares</strong>
                            </div>
                            <div>
                                <asp:GridView ID="grdTitularesHab" runat="server" AutoGenerateColumns="false" DataKeyNames="id_persona"
                                    AllowPaging="false" Style="border: none;" GridLines="None" Width="100%" CssClass="table table-bordered mtop5 "
                                    CellPadding="3">
                                    <HeaderStyle CssClass="grid-header"  />
                                    <AlternatingRowStyle BackColor="#efefef" />
                                    <Columns>

                                        <asp:BoundField DataField="TipoPersonaDesc" HeaderText="Tipo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"/>
                                        <asp:BoundField DataField="ApellidoNomRazon" HeaderText="Apellido y Nombre / Razon Social"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"/>
                                        <asp:BoundField DataField="Cuit" HeaderText="CUIT" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"/>
                                        <asp:BoundField DataField="Domicilio" HeaderText="Domicilio Legal" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center"/>

                                        <asp:TemplateField ItemStyle-Width="30px" HeaderText="Acciones" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>

                                                 <asp:LinkButton ID="btnEditarTitular" runat="server" CommandName='<%# Eval("TipoPersona") %>' CommandArgument='<%# Eval("id_persona") %>'
                                                    data-toggle="tooltip" title="Editar" CssClass="link-local" OnClick="btnEditarTitular_Click">
                                                        <i class="imoon imoon-pencil" style="color:#377bb5"></i>
                                                </asp:LinkButton>

                                                <asp:LinkButton ID="btnEliminarTitular" runat="server" data-tipopersona-eliminar='<%# Eval("TipoPersona") %>' data-toggle="tooltip" title="Eliminar"
                                                    data-id-persona-eliminar='<%# Eval("id_persona") %>' CssClass="link-local" OnClientClick="return showConfirmarEliminar(this);">
                                                        <i class="imoon imoon-close" style="color:#377bb5"></i>
                                                </asp:LinkButton>

                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <div class="mtop10">

                                            <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                                            <span class="mleft10">No se encontraron registros.</span>

                                        </div>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                            
                            <asp:HiddenField ID="hid_tipopersona_eliminar" runat="server" />
                            <asp:HiddenField ID="hid_id_persona_eliminar" runat="server" />

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>

        <%--Botones de Guardado--%>
        <asp:UpdatePanel ID="updBotonesGuardar" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

              <div id="pnlBotonesGuardar">
                     <div id="Div1" class="col-sm-6 mtop10">
                        <asp:LinkButton ID="btnVolver" runat="server" CssClass="btn btn-default btn-lg" Onclick="btnVolver_Click" style="display:none">
                        <i class="imoon imoon-arrow-left"></i>
                        <span class="text">Volver</span>
                        </asp:LinkButton>

                    </div>
    
                 <div class="text-right mtop10">
                      

                       <asp:LinkButton ID="btnContinuar" runat="server" CssClass="btn btn-lg btn-primary" OnClick="btnContinuar_Click" OnClientClick="return validarGuardar();">
                        <i class="imoon imoon-disk" ></i>
                        <span class="text">Guardar y Continuar</span>
                        </asp:LinkButton>

                    </div>
                    <div class="form-group">
                        <asp:UpdateProgress ID="UpdateProgress9" runat="server" DisplayAfter="200" AssociatedUpdatePanelID="updBotonesGuardar">
                            <ProgressTemplate>
                                <img src='<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>' style="margin-left: 10px" alt="loading" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>

    </div>



    <%--Modal Agregar Persona Física--%>
    <div id="frmAgregarPersonaFisica" class="modal fade" role="dialog">
        <asp:UpdatePanel ID="updAgregarPersonaFisica" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                 <asp:Panel ID="Panel1" runat="server" DefaultButton="btnAceptarTitPF">
                <div class="modal-dialog modal-lg" style="min-width:1000px">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title" style="margin-top:-8px">Agregar Persona F&iacute;sica</h4>
                        </div>
                        <div class="modal-body pbottom0">
                            <asp:HiddenField ID="hid_id_titular_pf" runat="server" />
                            

                            <div class="form-horizontal pright10">
                                <div class="form-group">
                                    <asp:label runat="server" class="control-label col-sm-2">Apellido/s (*):</asp:label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtApellidosPF" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                        <div id="Req_ApellidoPF" class="field-validation-error" style="display:none; ">
                                            Debe ingresar el/los Apellido/s.
                                        </div>
                                    </div>
                                    <asp:label runat="server" class="control-label col-sm-2">Nombre/s (*):</asp:label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtNombresPF" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                        <div id="Req_NombresPF" class="field-validation-error" style="display: none;">
                                            Debe ingresar el/los Nombres/s.
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-horizontal pright10">
                                <div class="form-group">
                                    <asp:label runat="server" class="control-label col-sm-2">Tipo y Nro de doc.(*):</asp:label>
                                    <div class="col-sm-4">
                                        <div class="form-inline">
                                            <asp:DropDownList ID="ddlTipoDocumentoPF" runat="server" Width="140px" CssClass="form-control">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtNroDocumentoPF" runat="server" MaxLength="15" Width="140px" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div id="Req_TipoNroDocPF" class="field-validation-error" style="display: none;">
                                            Debe ingresar el Tipo y Nro. de doc.
                                        </div>
                                    </div>
                                    <asp:label runat="server" class="control-label col-sm-2">C.U.I.T. (*):</asp:label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtCuitPF" runat="server" MaxLength="11" Width="150px" CssClass="form-control"></asp:TextBox>
                                        <div id="Req_CuitPF" class="field-validation-error" style="display: none;">
                                            Debe ingresar el CUIT.
                                        </div>
                                        <div id="ValCantidad_CuitPF" class="field-validation-error" style="display: none;">
                                            El cuit debe contener 11 dígitos sin guiones.
                                        </div>
                                        <div id="ValFormato_CuitPF" class="field-validation-error" style="display: none;">
                                            El CUIT ingresado no corresponde a una Persona Física. Ej: 20012345673
                                        </div>
                                        <div id="ValDV_CuitPF" class="field-validation-error" style="display: none;">
                                            El CUIT ingresado es inv&aacute;lido.
                                        </div>
                                        <div id="ValDNI_CuitPF" class="field-validation-error" style="display: none;">
                                            El CUIT ingresado es distinto al DNI.
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-horizontal pright10">
                                <div class="form-group">
                                    <asp:label runat="server" class="control-label col-sm-2">Tipo Ing. Brutos (*):</asp:label>
                                    <div class="col-sm-4">
                                        <asp:UpdatePanel ID="upd_ddlTipoIngresosBrutosPF" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlTipoIngresosBrutosPF" runat="server" Width="200px" CssClass="form-control"
                                                    OnSelectedIndexChanged="ddlTipoIngresosBrutosPF_SelectedIndexChanged" AutoPostBack="true" >
                                                </asp:DropDownList>
                                        
                                                <asp:HiddenField ID="hid_IngresosBrutosPF_expresion" runat="server" />
                                                <asp:HiddenField ID="hid_IngresosBrutosPF_formato" runat="server" />

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div id="Req_TipoIngresosBrutosPF" class="field-validation-error" style="display: none;">
                                            Debe seleccionar el tipo de Ingresos Brutos.
                                        </div>
                                    </div>
                                    <asp:label runat="server" class="control-label col-sm-2">Nº Ing. Brutos:</asp:label>
                                    <div class="col-sm-4">
                                        <asp:UpdatePanel ID="upd_txtIngresosBrutosPF" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtIngresosBrutosPF" runat="server" MaxLength="11" Width="200px" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>  
                                        <div id="Req_IngresosBrutosPF" class="field-validation-error" style="display: none;">
                                            Debe ingresar el Nro de Ing. Brutos.
                                        </div>
                                        <div id="ValFormato_IngresosBrutosPF" class="field-validation-error" style="display: none;">
                                            El CUIT ingresado no corresponde a una Persona Física. Ej: 20012345673.
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-horizontal pright10">
                                <div class="form-group">
                                    <asp:label runat="server" class="control-label col-sm-2">Calle (*):</asp:label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtCallePF" runat="server" MaxLength="50" Width="250px" CssClass="form-control"></asp:TextBox>
                                        <div id="Req_CallePF" class="field-validation-error" style="display: none;">
                                            Debe ingresar la Calle.
                                        </div>
                                    </div>
                                    <asp:label runat="server" class="control-label col-sm-2">Nro de Puerta (*):</asp:label>
                                    <div class="col-sm-4">
                                        <div class="form-inline">
                                            <asp:TextBox ID="txtNroPuertaPF" runat="server" MaxLength="5" Width="70px" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div id="Req_NroPuertaPF" class="field-validation-error" style="display: none;">
                                           Debe ingresar el Nro de Puerta.
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-horizontal pright10">
                                 <div class="form-group">
                                  <div class="col-sm-6">
                                        <div class="form-inline">
                                            <asp:Label ID="lblTorrePF" runat="server" class="col-sm-3 control-label" Style="margin-left: 37px">Torre:</asp:Label>
                                            <asp:TextBox ID="txtTorrePF" runat="server" MaxLength="3" Width="50px" CssClass="form-control mleft15"></asp:TextBox>
                                            <asp:Label ID="lblPisoPF" runat="server" class="pleft5 pright5">Piso:</asp:Label>
                                            <asp:TextBox ID="txtPisoPF" runat="server" MaxLength="3" Width="50px" CssClass="form-control"></asp:TextBox>
                                            <asp:Label ID="lblDeptoPF" runat="server" class="pleft5 pright5">Depto/UF:</asp:Label>
                                            <asp:TextBox ID="txtDeptoPF" runat="server" MaxLength="3" Width="50px" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
 
                                    <div class="col-sm-5">
                                        <asp:Label ID="lblCPPF" runat="server" class="control-label col-sm-5 mright15" Style="margin-left: -10px">C&oacute;digo Postal (*):</asp:Label>
                                        <asp:TextBox ID="txtCPPF" runat="server" MaxLength="8" Style="text-transform: uppercase" CssClass="form-control" Width="100px"></asp:TextBox>

                                        <div id="Req_CPPF" class="field-validation-error" style="display: none; margin-left: 180px">
                                            Debe ingresar el c&oacute;digo postal.
                                        </div>
                                        <div id="Val_Formato_CPPF" class="field-validation-error" style="display: none;">
                                            El formato es inv&aacute;lido, el mismo debe se por ej: 1093 o C1093AAC.
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-horizontal pright10">
                                <div class="form-group">
                                    <asp:label runat="server" class="control-label col-sm-2">Provincia (*):</asp:label>
                                    <div class="col-sm-4">
                                        <asp:UpdatePanel ID="updProvinciasPF" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlProvinciaPF" runat="server" Width="250px" AutoPostBack="true" CssClass="form-control"
                                                    OnSelectedIndexChanged="ddlProvinciaPF_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                        <div id="Req_ProvinciaPF" class="field-validation-error" style="display: none;">
                                            Debe ingresar la Provincia.
                                        </div>

                                    </div>
                                    <asp:label runat="server" class="control-label col-sm-2">Localidad (*):</asp:label>
                                    <div class="col-sm-4">
                                        <asp:UpdatePanel ID="updLocalidadPF" runat="server">
                                            <ContentTemplate>
                                                <div class="form-inline">
                                            
                                                        <asp:DropDownList ID="ddlLocalidadPF" runat="server" Width="250px" CssClass="form-control">
                                                        </asp:DropDownList>
                                            
                                                    <div class="form-group">

                                                        <asp:UpdateProgress ID="UpdateProgress7" AssociatedUpdatePanelID="updProvinciasPF" runat="server" DisplayAfter="0">
                                                            <ProgressTemplate>
                                                                <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                
                                                    </div>
                                                    
                                                </div>
                                                
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div id="Req_LocalidadPF" class="field-validation-error" style="display: none;">
                                            Debe ingresar la Localidad.
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-horizontal pright10">
                                <div class="form-group">

                                    <asp:label runat="server" class="control-label col-sm-2">Tel&eacute;fono M&oacute;vil:</asp:label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtTelefonoMovilPF" runat="server" Width="160px" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                    </div>
                                    <asp:label runat="server" class="control-label col-sm-2">Tel&eacute;fono:</asp:label>
                                    <div class="col-sm-4">
                                        <div class="form-inline">
                                            <asp:TextBox ID="txtTelefonoPF" runat="server" Width="160px" CssClass="form-control" MaxLength="50" ></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-horizontal pright10">
                                <div class="form-group">
                                    <asp:label runat="server" class="control-label col-sm-2">E-mail (*):</asp:label>
                                    <div class="col-sm-4">
                                       
                                        <asp:TextBox ID="txtEmailPF" runat="server" Width="250px" CssClass="form-control"></asp:TextBox>
                                        Debe ser el mail del titular, donde recibirá las notificaciones
                                        <br />
                                        <div id="Req_EmailPF" class="field-validation-error" style="display: none;">
                                            Debe ingresar el E-mail.
                                           
                                        </div>
                                        <div id="ValFormato_EmailPF" class="field-validation-error" style="display: none;">
                                            formato inv&aacute;lido. Ej: nombre@servidor.com
                                        </div>

                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="modal-footer mtop0 mleft20 mright20">
                            
                            <asp:UpdatePanel ID="updBotonesAgregarPF" runat="server">
                                <ContentTemplate>

                                    <div class="form-inline">
                                        <div class="form-group pull-left">
                                            <asp:Panel ID="ValExiste_TitularPF" runat="server" CssClass="field-validation-error" Style="display: none;">
                                                Ya existe un titular con el mismo número de CUIT.
                                            </asp:Panel>
                                        </div>
                                        <div class="form-group">
                                            <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="updBotonesAgregarPF">
                                                <ProgressTemplate>
                                                    <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />Guardando...
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>
                                        <asp:label ID="Label2" runat="server" class="mbottom0 pull-left" >
                                                Los campos marcados con "(*)" son obligatorios.
                                        </asp:label>
                                        <div id="pnlBotonesAgregarPF" class="form-group">

                                            <asp:LinkButton ID="btnAceptarTitPF" runat="server" CssClass="btn btn-primary" OnClientClick="return validarAgregarPF();" OnClick="btnAceptarTitPF_Click">
                                                <i class="imoon imoon-ok" ></i>
                                                <span class="text">Aceptar</span>
                                            </asp:LinkButton>
                                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                                <i class="imoon imoon-close"></i>
                                                <span class="text">Cancelar</span>
                                            </button>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                 </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
    <!-- /.modal -->


    <%--Modal Agregar Persona Jurídica--%>
    <div id="frmAgregarPersonaJuridica" class="modal fade" style="overflow-y: auto;">
        <div class="modal-dialog modal-lg" style="min-width: 1000px">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" style="margin-top:-8px">Agregar Persona Jur&iacute;dica</h4>
                </div>
                <div class="modal-body pbottom0">
                    <asp:UpdatePanel ID="updAgregarPersonaJuridica" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                        <script type="text/javascript">
                                            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandler);
                                            function endRequestHandler() {
                                                inicializar_popover();
                                            }
                           </script> 
                             <asp:Panel ID="Panel2" runat="server" DefaultButton="btnAceptarTitPJ">
                            <asp:HiddenField ID="hid_id_titular_pj" runat="server" />

                            <div class="form-horizontal pright10">
                                <div class="form-group">                                
                               <asp:label runat="server" class="control-label col-sm-2">Tipo de Sociedad (*):</asp:label>
                                    <div class="col-sm-4">
                                        <asp:UpdatePanel ID="upd_ddlTipoSociedadPJ" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlTipoSociedadPJ" runat="server" Width="300px" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlTipoSociedadPJ_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div id="Req_TipoSociedadPJ" class="field-validation-error" style="display: none;">
                                            Debe ingresar el Tipo de Sociedad
                                        </div>
                                    </div>                               
                              
                                      <asp:label runat="server" class="control-label col-sm-2">C.U.I.T.(*):</asp:label>
                                      <div class="col-sm-4">                   
                                        <asp:TextBox ID="txtCuitPJ" runat="server" MaxLength="11" Width="140px" rel="popover" data-placement="top" data-trigger="focus" data-content="Debe ingresar los 11 dígitos sin guiones" AutoPostBack="true" CssClass="form-control" Style="display:inline"></asp:TextBox>
                                      
                                        <div id="Req_CuitPJ" class="field-validation-error" style="display: none;">
                                            Debe ingresar el CUIT.
                                        </div>
                                        <div id="ValCantidad_CuitPJ" class="field-validation-error" style="display: none;">
                                            El cuit debe contener 11 dígitos sin guiones
                                        </div>
                                        <div id="ValFormato_CuitPJ" class="field-validation-error" style="display: none;">
                                            El CUIT ingresado no corresponde a una Persona Jurídica. Ej: 30012345673
                                        </div>
                                        <div id="ValDV_CuitPJ" class="field-validation-error " style="display: none;">
                                            El CUIT ingresado es inv&aacute;lido.
                                        </div>
                                    </div>
                                      </div>
                               </div>
                            <div class="form-horizontal pright10">
                                <div class="form-group">
                                  <div id="rowRazonSocial">
                             
                                    <asp:Label ID="lblRazonSocialPJ" runat="server" CssClass="control-label col-sm-2 text-center" Text="Razon Social (*):"></asp:Label>
                                    <div class="col-sm-10">
                                        <asp:UpdatePanel ID="upd_txtRazonSocialPJ" runat="server">
                                            <ContentTemplate>   
                                                <asp:TextBox ID="txtRazonSocialPJ" rel="popover" data-placement="top" data-trigger="focus" data-content="La razón social debe coincidir exactamente con la de la escritura de constitución de la sociedad." Style="display:inline" runat="server" MaxLength="200" Width="635px"  CssClass="form-control"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                        <div id="Req_RazonSocialPJ" class="field-validation-error" style="display: none;">
                                            Debe ingresar la Raz&oacute;n Social
                                        </div>
                                    </div>
                                 </div>
                         
                             </div>
                         </div>
                            <div class="form-horizontal pright10">
                                <div class="form-group">

                                     <asp:label runat="server" class="control-label col-sm-2" >Tipo Ing. Brutos:</asp:label>
                                    
                                   
                                    <div class="col-sm-4">
                                        <asp:UpdatePanel ID="upd_ddlTipoIngresosBrutosPJ" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlTipoIngresosBrutosPJ" runat="server" Width="200px" AutoPostBack="true" CssClass="form-control"
                                                    OnSelectedIndexChanged="ddlTipoIngresosBrutosPJ_SelectedIndexChanged"
                                                    ></asp:DropDownList>

                                                <asp:HiddenField ID="hid_IngresosBrutosPJ_expresion" runat="server" />
                                                <asp:HiddenField ID="hid_IngresosBrutosPJ_formato" runat="server" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div id="Req_TipoIngresosBrutosPJ" class="field-validation-error" style="display: none;">
                                            Debe seleccionar el tipo de Ingresos Brutos.
                                        </div>
                                    </div>
                                        
                                   
                                     <asp:label runat="server" class="control-label col-sm-2">Nº Ing. Brutos:</asp:label>
                                     <div class="col-sm-4">
                                        <asp:UpdatePanel ID="upd_txtIngresosBrutosPJ" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtIngresosBrutosPJ" rel="popover" MaxLength="11" data-placement="top" data-trigger="focus" data-content="Debe ingresar los 11 dígitos sin guiones" runat="server"  Width="150px" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                            
                                        <div id="Req_IngresosBrutosPJ" class="field-validation-error" style="display: none;">
                                            Debe ingresar el Nro de Ing. Brutos.
                                        </div>
                                        <div id="ValFormato_IngresosBrutosPJ" class="field-validation-error" style="display: none;">
                                             El CUIT ingresado no corresponde a una Persona Física. Ej: 20012345673.
                                        </div>
                                    </div>

                                            
                                </div>
                            </div>
                                       
                            <div class="form-horizontal pright10">
                                <div class="form-group">
                                    <asp:label runat="server" class="control-label col-sm-2">Calle (*):</asp:label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtCallePJ" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        <div id="Req_CallePJ" class="field-validation-error" style="display: none;">
                                            Debe ingresar la Calle.
                                        </div>
                                    </div>
                                    <asp:label runat="server" class="control-label col-sm-2">Nro de Puerta (*):</asp:label>
                                    <div class="col-sm-4">
                                        <div class="form-inline">
                                            <asp:TextBox ID="txtNroPuertaPJ" runat="server" MaxLength="5" Width="70px" CssClass="form-control"></asp:TextBox>
                    
                                        </div>
                                        <div id="Req_NroPuertaPJ" class="field-validation-error" style="display: none;">
                                            Debe ingresar el Nro de Puerta.
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-horizontal pright10">
                                  <div class="form-group">
                                  <div class="col-sm-6">
                                  <div class="form-inline">
                                  <asp:label ID="lblTorrePj" runat="server" class="col-sm-4 control-label mleft10">Torre:</asp:label>
                                  <asp:TextBox ID="txtTorrePJ" runat="server" MaxLength="3" Width="50px" CssClass="form-control"></asp:TextBox>
                                  <asp:label ID="lblPisoPJ" runat="server" class="pleft5 pright5">Piso:</asp:label>
                                    <asp:TextBox ID="txtPisoPJ" runat="server" MaxLength="3" Width="50px" CssClass="form-control"></asp:TextBox>
                                    <asp:label ID="lblDeptoPJ" runat="server" class="pleft5 pright5">Depto/UF:</asp:label>
                                    <asp:TextBox ID="txtDeptoPJ" runat="server" MaxLength="3" Width="50px" CssClass="form-control"></asp:TextBox>
                                   </div>
                                  </div>
                                    <div class="col-sm-5">
                                       <asp:label ID="lblCPPJ" runat="server" class="control-label col-sm-5 mleft5" style="margin-left:-10px">C&oacute;digo Postal (*):</asp:label>
                                        <asp:TextBox ID="txtCPPJ" runat="server" MaxLength="8" Style="text-transform: uppercase" CssClass="form-control" Width="100px"></asp:TextBox>

                                        <div id="Req_CPPJ" class="field-validation-error" style="display: none; margin-left:180px" >
                                            Debe ingresar el c&oacute;digo postal.
                                        </div>
                                        <div id="Val_Formato_CPPJ" class="field-validation-error" style="display: none;">
                                            El formato es inv&aacute;lido, el mismo debe se por ej: 1093 o C1093AAC.
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-horizontal pright10">
                                <div class="form-group">
                                    <asp:label runat="server" class="control-label col-sm-2">Provincia (*):</asp:label>
                                    <div class="col-sm-4">
                                        <asp:UpdatePanel ID="updProvinciasPJ" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlProvinciaPJ" runat="server" Width="250px" AutoPostBack="true" CssClass="form-control"
                                                    OnSelectedIndexChanged="ddlProvinciaPJ_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div id="Req_ProvinciaPJ" class="field-validation-error" style="display: none;">
                                            Debe ingresar la Provincia.
                                        </div>
                                    </div>
                                    <asp:label runat="server" class="control-label col-sm-2">Localidad (*):</asp:label>
                                    <div class="col-sm-4">
                                        <asp:UpdatePanel ID="updLocalidadPJ" runat="server">
                                            <ContentTemplate>
                                                <div class="form-inline">

                                                    <asp:DropDownList ID="ddlLocalidadPJ" runat="server" Width="250px" CssClass="form-control">
                                                    </asp:DropDownList>

                                                    <div class="form-group">

                                                        <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="updProvinciasPJ" runat="server" DisplayAfter="0">
                                                            <ProgressTemplate>
                                                                <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>

                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div id="Req_LocalidadPJ" class="field-validation-error" style="display: none;">
                                            Debe ingresar la Localidad.
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div class="form-horizontal pright10">
                                <div class="form-group">
                                    <asp:label runat="server" class="control-label col-sm-2">Tel&eacute;fono:</asp:label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtTelefonoPJ" runat="server" Width="200px" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <asp:label runat="server" class="control-label col-sm-2">E-mail (*):</asp:label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtEmailPJ" runat="server" CssClass="form-control" ></asp:TextBox>
                                        Debe ser el mail del titular, donde recibirá las notificaciones
                                        <br />
                                        <div id="Req_EmailPJ" class="field-validation-error" style="display: none;">
                                            Debe ingresar el E-mail.
                                        </div>
                                        <div id="ValFormato_EmailPJ" class="field-validation-error" style="display: none;">
                                            formato inv&aacute;lido. Ej: nombre@servidor.com
                                        </div>
                                    </div>

                                </div>
                            </div>


                            <%--Grilla de los titulares de las Sociedades de hecho --%>
                            <asp:UpdatePanel ID="updgrillaTitularesSH"  runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <asp:Panel ID="pnlAgregarTitularSH" DefaultButton="btnAgregarTitularSH" runat="server" Style="display: none">

                                        <%--Botones de Agregar Titulares SH--%>
                                        <asp:UpdatePanel ID="updBotonesAgregarTitularSH" runat="server">
                                            <ContentTemplate>
                                                

                                                            <div style="width: 100%; text-align: right;">

                                                                <table border="0" style="float: right">
                                                                    <tr>
                                                                        <td style="width: 30px">

                                                                            <asp:UpdateProgress ID="UpdateProgress15" AssociatedUpdatePanelID="updBotonesAgregarTitularSH"
                                                                                runat="server" DisplayAfter="0">
                                                                                <ProgressTemplate>
                                                                                    <img src='<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>' alt="" />
                                                                                </ProgressTemplate>
                                                                            </asp:UpdateProgress>

                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="btnAgregarTitularSH" runat="server" CssClass="btn btn-default" OnClick="btnAgregarTitularSH_Click" >
                                                                                <i class="imoon imoon-plus"></i>
                                                                                <span class="text">Agregar Titular</span>
                                                                            </asp:LinkButton>
                                                                        </td>


                                                                    </tr>
                                                                </table>

                                                            </div>

                                                   

                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <br /><br />
                                         <div style="margin-left:55px">
                                            <b>Datos de los Titulares</b>
                                        </div>
                                              
                                        <%--Grilla de Titulares de Sociedad de Hecho--%>
                                        <asp:GridView ID="grdTitularesSH" runat="server" AutoGenerateColumns="false" AllowPaging="false" CssClass="table table-bordered mtop5"
                                            GridLines="None" Width="860px" CellPadding="3" Style="margin-left:55px" >
                                                

                                            <Columns>

                                                <asp:BoundField DataField="Apellidos" HeaderText="Apellido/s" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" />
                                                <asp:BoundField DataField="Nombres" HeaderText="Nombre/s" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"/>
                                                <asp:BoundField DataField="TipoDoc" HeaderText="Tipo Doc." HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"/>
                                                <asp:BoundField DataField="NroDoc" HeaderText="Nro de Doc." HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center"/>
                                                <asp:BoundField DataField="email" HeaderText="Email" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="center"/>

                                                <asp:TemplateField ItemStyle-Width="80px"  HeaderStyle-HorizontalAlign="center" HeaderText="Acciones" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>

                                                        <asp:LinkButton ID="btnEditarTitularSH" runat="server" OnClick="btnEditarTitularSH_Click"
                                                            CommandName="Editar" title="Editar" data-toggle="tooltip">
                                                            <i class="imoon imoon-pencil" style="color:#377bb5"></i>
                                                        </asp:LinkButton>

                                                        <asp:LinkButton ID="btnEliminarTitularSH" runat="server" OnClick="btnEliminarTitularSH_Click" title="Eliminar" data-toggle="tooltip"
                                                            CommandName="Eliminar" OnClientClick="return confirm('¿Está seguro que desea eliminar este titular?');">
                                                            <i class="imoon imoon-remove" style="color:#377bb5"></i>                                                            
                                                        </asp:LinkButton>
                                          
                                                        <asp:HiddenField ID="hid_rowid_grdTitularesSH" runat="server" Value='<% #Eval("rowid") %>' />
                                                        <asp:HiddenField ID="hid_id_tipodoc_grdTitularesSH" runat="server" Value='<% #Eval("IdTipoDocPersonal") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div class="mtop10">

                                                    <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                                                    <span class="mleft10">No se encontraron registros.</span>

                                                </div>
                                            </EmptyDataTemplate>
                                        </asp:GridView>                

                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                       </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer mtop10 mleft20 mright20">
                    <asp:UpdatePanel ID="updBotonesAgregarPJ" runat="server">
                        <ContentTemplate>
                            <asp:Panel runat="server"  defaultButton="btnAceptarTitPJ">
                            <div class="form-inline">

                                <div class="form-group pull-left">
                                    <asp:Panel ID="ValExiste_TitularPJ" runat="server" CssClass="field-validation-error" Style="display: none;">
                                        Ya existe un titular con el mismo número de CUIT.
                                    </asp:Panel>
                                </div>

                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress8" runat="server" AssociatedUpdatePanelID="updBotonesAgregarPJ">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />Guardando...
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <asp:label ID="Label1" runat="server" class="mbottom0 mtop5 pull-left" >
                                                Los campos marcados con "(*)" son obligatorios.
                                 </asp:label>
                                <div id="pnlBotonesAgregarPJ" class="form-group">

                                    <asp:LinkButton ID="btnAceptarTitPJ" runat="server" CssClass="btn btn-primary" OnClientClick="return validarAgregarPJ();" OnClick="btnAceptarTitPJ_Click">
                                                <i class="imoon imoon-ok"></i>
                                                <span class="text">Aceptar</span>
                                    </asp:LinkButton>
                                    <button type="button" class="btn btn-default" data-dismiss="modal">
                                        <i class="imoon imoon-close"></i>
                                        <span class="text">Cancelar</span>
                                    </button>
                                </div>
                            </div>
                                </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->


    <%--Confirmar Eliminar Persona--%>
    <div id="frmConfirmarEliminar" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top:-8px">Eliminar Persona</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <asp:label runat="server" class="imoon imoon-remove-circle fs64 color-blue"></asp:label>
                            </td>
                            <td style="vertical-align: middle">
                                <asp:label runat="server" class="mleft10">¿ Est&aacute; seguro de eliminar el registro ?</asp:label>
                            </td>
                        </tr>
                    </table>

                </div>
                <div class="modal-footer mleft20 mright20">

                    <asp:UpdatePanel ID="updConfirmarEliminar" runat="server">
                        <ContentTemplate>

                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updConfirmarEliminar">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div id="pnlBotonesConfirmacionEliminar" class="form-group">
                                    <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-primary" Text="Sí" OnClick="btnEliminar_Click"
                                        OnClientClick="ocultarBotonesConfirmacion();" />
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

    <%--Confirmar Eliminar Firmante Persona Juridica--%>
    <div id="frmConfirmarEliminarFirPJ" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top:-8px">Eliminar Firmante (Persona Jur&iacute;dica)</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <asp:label runat="server" class="imoon imoon-remove-circle fs64 color-blue"></asp:label>
                            </td>
                            <td style="vertical-align: middle">
                                <asp:label runat="server" class="mleft10">¿ Est&aacute; seguro de eliminar este firmante ?</asp:label>
                            </td>
                        </tr>
                    </table>

                </div>
                <div class="modal-footer mleft20 mright20">

                    <asp:UpdatePanel ID="updConfirmarEliminarFirPJ" runat="server">
                        <ContentTemplate>

                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="updConfirmarEliminarFirPJ">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div id="Div2" class="form-group">
                                    <%--<asp:Button ID="btnEliminarFirmantePJ" runat="server" CssClass="btn btn-primary" Text="Sí" OnClick="btnEliminarFirmantePJ_Click"/>--%>
                                    <button type="button" class="btn btn-default" data-dismiss="modal">No</button>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>


    <%-- Agregar Titular y Firmante (Sociedad de Hecho)--%>
    <div id="frmAgregarTitularesSH" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top:-8px">Agregar Titulares (Sociedad de Hecho)</h4>
                </div>
               
                <div class="modal-body">
                      <asp:Panel runat="server" DefaultButton="btnAceptarTitSH">
                    <asp:UpdatePanel ID="updABMTitularesSH" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                        
                           <asp:HiddenField ID="hid_rowindex_titSH" runat="server" />
                            <strong>Datos del Titular</strong>
                            <div class="form-horizontal pright10 mtop5">
                                
                                <div class="form-group">
                                    <asp:label runat="server" class="control-label col-sm-3">Apellido/s (*):</asp:label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtApellidosTitSH" runat="server" MaxLength="50" Width="300px" CssClass="form-control"></asp:TextBox>

                                        <div id="Req_ApellidosTitSH" class="field-validation-error" style="display: none;">
                                            Debe ingresar el/los Apellido/s.
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:label runat="server" class="control-label col-sm-3">Nombre/s (*):</asp:label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtNombresTitSH" runat="server" MaxLength="50" Width="400px" CssClass="form-control"></asp:TextBox>

                                        <div id="Req_NombresTitSH" class="field-validation-error" style="display: none;">
                                            Debe ingresar el/los Nombre/s.
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:label runat="server" class="control-label col-sm-3">Tipo y Nro. de doc.(*):</asp:label>
                                    <div class="col-sm-9">
                                        <div class="form-inline ">
                                            <asp:DropDownList ID="ddlTipoDocumentoTitSH" runat="server" Width="150px" CssClass="form-control">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtNroDocumentoTitSH" runat="server" MaxLength="15" Width="140px" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div id="Req_TipoNroDocumentoTitSH" class="field-validation-error" style="display: none;">
                                            Debe ingresar el Tipo y Nro. de doc.
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:label runat="server" class="control-label col-sm-3">E-mail(*):</asp:label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtEmailTitSH" runat="server" MaxLength="40" Width="400px" CssClass="form-control"></asp:TextBox>

                                        <div id="Req_EmailTitSH" class="field-validation-error" style="display: none;">
                                            Debe ingresar el e-mail.
                                        </div>
                                        <div id="Val_Formato_EmailTitSH" class="field-validation-error" style="display: none;">
                                            formato inv&aacute;lido. Ej: nombre@servidor.com
                                        </div>
                                    </div>
                                </div>
                            </div> 
                                       
                        </ContentTemplate>
                    </asp:UpdatePanel>
                          </asp:Panel>
                </div>
                <div class="modal-footer mleft20 mright20">

                    <asp:UpdatePanel ID="updBotonesIngresarTitularesSH" runat="server">
                        <ContentTemplate>
                             <asp:Panel ID="Panel3" runat="server" DefaultButton="btnAceptarTitSH"> 
                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress10" runat="server" AssociatedUpdatePanelID="updBotonesIngresarTitularesSH">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div id="pnlBotonesIngresarTitularesSH" class="form-group">
                                    <asp:LinkButton ID="btnAceptarTitSH" runat="server" CssClass="btn btn-primary" 
                                         OnClientClick="return validarAgregarTitSH();" OnClick="btnAceptarTitSH_Click" >
                                        <i class="imoon imoon-ok"></i>
                                        <span class="text">Aceptar</span>
                                    </asp:LinkButton>
                                    <button type="button" class="btn btn-default" data-dismiss="modal">
                                        <i class="imoon imoon-close"></i>
                                        <span class="text">Cancelar</span>
                                    </button>
                                </div>
                            </div>
                            </asp:Panel>  
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                  
            </div>
        </div>
    </div>


    <%--Modal mensajes de error--%>
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
                                <asp:label runat="server" class="imoon imoon-remove-circle fs64" style="color: #f00"></asp:label>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="updmpeInfo" runat="server" class="form-group">
                                    <ContentTemplate>
                                        <asp:Label ID="lblError" runat="server" class="pad10"></asp:Label>
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
        </div>
    </div>
    <!-- /.modal --> 

    <script type="text/javascript">

        
        var id_tipodoc_pasaporte = "<%: (int)StaticClass.Constantes.TipoDocumentoPersonal.PASAPORTE %>";
        var id_tipodoc_dni = "<%: (int)StaticClass.Constantes.TipoDocumentoPersonal.DNI %>";

        $(document).ready(function () {

            $("#page_content").hide();
            $("#Loading").show();
            toolTips();
            init_JS_updGrillaTitulares();
            init_JS_upd_ddlTipoIngresosBrutosPF();
            init_JS_upd_txtIngresosBrutosPF();
            init_JS_updLocalidadPF();
            init_JS_updProvinciasPF();
            init_JS_updAgregarPersonaJuridica();
            init_JS_upd_ddlTipoIngresosBrutosPJ();
            init_JS_upd_txtIngresosBrutosPJ();
            init_JS_updLocalidadPJ();
            init_JS_updProvinciasPJ();
            init_JS_upd_ddlTipoSociedadPJ();
            init_JS_upd_txtRazonSocialPJ();
            $("#<%: btnCargarDatos.ClientID %>").click();
    

        });

        function finalizarCarga() {

            $("#Loading").hide();
            $("#page_content").show();

            return false;

        }
        function toolTips() {
            $("[data-toggle='tooltip']").tooltip();
            return false;

        }

        function inicializar_popover() {

            $('[rel=popover]').popover({
                html: 'true',
            })

        }
        function showfrmError() {
            
            $("#frmError").modal("show");
            return false;

        }

        function init_JS_updProvinciasPF() {
            $("#<%: ddlProvinciaPF.ClientID %>").on("change", function (e) {
                $("#Req_ProvinciaPF").hide();
                $("#Req_LocalidadPF").hide();
            });
            return false;
        }

        function init_JS_updGrillaTitulares() {
            toolTips();
        }

        function init_Js_updABMTitularesSH() {

            toolTips();
        
            setNumeric();
        
            $("#<%: txtApellidosTitSH.ClientID %>").on("keyup", function (e) {
                $("#Req_ApellidosTitSH").hide();
            });

            $("#<%: txtNombresTitSH.ClientID %>").on("keyup", function (e) {
                $("#Req_NombresTitSH").hide();
            });
            $("#<%: ddlTipoDocumentoTitSH.ClientID %>").on("change", function (e) {
                $("#Req_TipoNroDocumentoTitSH").hide();
                setNumeric();
            
            });
            $("#<%: txtNroDocumentoTitSH.ClientID %>").on("keyup", function (e) {
                $("#Req_TipoNroDocumentoTitSH").hide();
            });
            $("#<%: txtEmailTitSH.ClientID %>").on("keyup", function (e) {
                $("#Req_EmailTitSH").hide();
                $("#Val_Formato_EmailTitSH").hide();
            });

            return false;
        }
        function setNumeric ()
        {
            
            if ($("#<%: ddlTipoDocumentoTitSH.ClientID %>").val() != id_tipodoc_pasaporte) {
                $("#<%: txtNroDocumentoTitSH.ClientID %>").autoNumeric({ aSep: '', mDec: '0', vMax: '99999999' }); 
                $("#<%: txtNroDocumentoTitSH.ClientID %>").autoNumeric("update");
            }
            else {
                $("#<%: txtNroDocumentoTitSH.ClientID %>").autoNumeric("destroy");
            }

        }

        function showfrmAgregarPersonaFisica() {

            $("#frmAgregarPersonaFisica").modal({
                "show": true,
                "backdrop": "static"

            });
            return false;
        }
        function hidefrmAgregarPersonaFisica() {

            $("#frmAgregarPersonaFisica").modal("hide");
            return false;
        }
        function showfrmAgregarPersonaJuridica() {

            $("#frmAgregarPersonaJuridica").modal({
                "show": true,
                "backdrop": "static"

            });
            return false;
        }
        function hidefrmAgregarPersonaJuridica() {

            $("#frmAgregarPersonaJuridica").modal("hide");
            return false;
        }
       
        function ValidarDniCuit() {
            
            var ret = true;
            var tipoDoc = $("#<%:  ddlTipoDocumentoPF.ClientID %>").val();
            
            
            // Solo cuando se eligio tipo de doc DNI
            if (tipoDoc == 1) {
                
                var dni = $("#<%: txtNroDocumentoPF.ClientID %>").val();
                var cuit = $("#<%: txtCuitPF.ClientID %>").val();

                ret = ((cuit.indexOf(dni) > -1) || (parseInt(dni) > 90000000));
            }

            return ret;
        }


        function showConfirmarEliminar(obj) {

            var tipopersona_eliminar = $(obj).attr("data-tipopersona-eliminar");
            var id_persona_eliminar = $(obj).attr("data-id-persona-eliminar");

            $("#<%: hid_tipopersona_eliminar.ClientID %>").val(tipopersona_eliminar);
            $("#<%: hid_id_persona_eliminar.ClientID %>").val(id_persona_eliminar);

            $("#frmConfirmarEliminar").modal("show");
            return false;
        }

        function hideConfirmarEliminar() {
            $("#frmConfirmarEliminar").modal("hide");
            return false;
        }

        function hidefrmConfirmarEliminarFirPJ() {

            $("#frmConfirmarEliminarFirPJ").modal("hide");
        }

        function ocultarBotonesConfirmacion() {
            $("#pnlBotonesConfirmacionEliminar").hide();
            return false;
        }

        function validarAgregarPF() {

            var ret = true;
            
            var strmsgFormatoIIBB = "formato incorrecto. Ej: " + $("#<%: hid_IngresosBrutosPF_formato.ClientID %>").val();
            var formatoIIBB = $("#<%: hid_IngresosBrutosPF_expresion.ClientID %>").val(); ///^([0-9]|-)*$/;
            
            
            if (formatoIIBB.length > 0) {
                formatoIIBB = eval("/^" + formatoIIBB + "$/");
            }
            
            var formatoCUIT = /[2]\d{10}$/;
            var formatoEmail = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([.]\w+)*$/;
            var formatoCP = /^(\d{4}|[a-zA-Z]\d{4}[a-zA-Z]{3})$/;


            $("#Req_ApellidoPF").hide();
            $("#Req_NombresPF").hide();
            $("#Req_TipoNroDocPF").hide();
            $("#Req_CuitPF").hide();
            $("#ValDV_CuitPF").hide();
            $("#ValCantidad_CuitPF").hide();
            $("#ValFormato_CuitPF").hide();
            $("#ValDNI_CuitPF").hide();
            $("#Req_TipoIngresosBrutosPF").hide();
            $("#Req_IngresosBrutosPF").hide();
            $("#ValFormato_IngresosBrutosPF").hide();
            $("#Req_CallePF").hide();
            $("#Req_NroPuertaPF").hide();
            $("#Req_CPPF").hide();
            $("#Req_ProvinciaPF").hide();
            $("#Req_LocalidadPF").hide();
            $("#Req_EmailPF").hide();
            $("#ValFormato_EmailPF").hide();
            $("#Req_ApellidoFirPF").hide();
            $("#Req_NombresFirPF").hide();
            $("#Req_TipoNroDocFirPF").hide();
            $("#Req_TipoCaracterLegalFirPF").hide();
            $("#<%: ValExiste_TitularPF.ClientID %>").hide();            
            $("#Val_Formato_CPPF").hide();


            if ($.trim($("#<%: txtApellidosPF.ClientID %>").val()).length == 0) {
                $("#Req_ApellidoPF").css("display", "inline-block");
                ret = false;
            }

            if ($.trim($("#<%: txtNombresPF.ClientID %>").val()).length == 0) {
                $("#Req_NombresPF").css("display", "inline-block");
                ret = false;
            }

            if ($.trim($("#<%: ddlTipoDocumentoPF.ClientID %>").val()).length == 0 || 
                $.trim($("#<%: txtNroDocumentoPF.ClientID %>").val()).length == 0 ) {
                $("#Req_TipoNroDocPF").css("display", "inline-block");
                ret = false;
            }
            
            if ($.trim($("#<%: txtCuitPF.ClientID %>").val()).length == 0) {
                $("#Req_CuitPF").css("display", "inline-block");
                ret = false;
            }
            else {
                if ($.trim($("#<%: txtCuitPF.ClientID %>").val()).length < 11) {
                        $("#ValCantidad_CuitPF").css("display", "inline-block");
                        ret = false;
                    }

                    else if (!formatoCUIT.test($.trim($("#<%: txtCuitPF.ClientID %>").val()))) {
                        $("#ValFormato_CuitPF").css("display", "inline-block");
                        ret = false;
                    }
                    else if (!ValidarCuitSinGuiones($("#<%: txtCuitPF.ClientID %>")[0])) {
                        $("#ValDV_CuitPF").css("display", "inline-block");
                        ret = false;
                    }
                    else if ($.trim($("#<%: ddlTipoDocumentoPF.ClientID %>").val()) == id_tipodoc_dni) {
                        if (!ValidarDniCuit()) {
                            $("#ValDNI_CuitPF").css("display", "inline-block");
                            ret = false;
                        }
                    }
             }

            //Código postal
            if ($.trim($("#<%: txtCPPF.ClientID %>").val()).length == 0) {
                $("#Req_CPPF").css("display", "inline-block");
                ret = false;
            }
            else {
                if (!formatoCP.test($.trim($("#<%: txtCPPF.ClientID %>").val()))) {
                    $("#Val_Formato_CPPF").css("display", "inline-block");
                    ret = false;
                }
            }

            
            if ($.trim($("#<%: ddlTipoIngresosBrutosPF.ClientID %>").val()).length == 0) {
                $("#Req_TipoIngresosBrutosPF").css("display", "inline-block");
                ret = false;
            }
            else {
                if (!$("#<%: txtIngresosBrutosPF.ClientID %>").prop("disabled")) {

                    if ($.trim($("#<%: txtIngresosBrutosPF.ClientID %>").val()).length == 0) {
                        $("#Req_IngresosBrutosPF").css("display", "inline-block");
                        ret = false;
                    }
                    else {
                        if (!formatoIIBB.test($.trim($("#<%: txtIngresosBrutosPF.ClientID %>").val()))) {
                            $("#ValFormato_IngresosBrutosPF").text(strmsgFormatoIIBB);
                            $("#ValFormato_IngresosBrutosPF").css("display", "inline-block");
                            ret = false;
                        }
                    }
                }
            }
               
            if ($.trim($("#<%: txtCallePF.ClientID %>").val()).length == 0) {
                $("#Req_CallePF").css("display", "inline-block");
                ret = false;
            }

            if ($.trim($("#<%: txtNroPuertaPF.ClientID %>").val()).length == 0) {
                $("#Req_NroPuertaPF").css("display", "inline-block");
                ret = false;
            }
            
            if ($.trim($("#<%: ddlLocalidadPF.ClientID %>").val()).length == 0) {
                $("#Req_LocalidadPF").css("display", "inline-block");
                ret = false;
            }

            if ($.trim($("#<%: txtEmailPF.ClientID %>").val()).length > 0) {
                if (!formatoEmail.test($.trim($("#<%: txtEmailPF.ClientID %>").val()))) {
                    $("#ValFormato_EmailPF").css("display", "inline-block");
                    ret = false;
                }
            }
            else {
                $("#Req_EmailPF").css("display", "inline-block");
                ret = false;
            }

            if (ret) {
                $("#pnlBotonesAgregarPF").hide();
            }

            return ret;

        }
   
        function validarAgregarPJ() {

            var ret = true;

            var strmsgFormatoIIBB = "formato incorrecto. Ej: " + $("#<%: hid_IngresosBrutosPJ_formato.ClientID %>").val();
            var formatoIIBB = $("#<%: hid_IngresosBrutosPJ_expresion.ClientID %>").val(); ///^([0-9]|-)*$/;

            if (formatoIIBB.length > 0) {
                formatoIIBB = eval("/^" + formatoIIBB + "$/");
            }

            var formatoCUIT = /[3]\d{10}$/;
            var formatoEmail = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([.]\w+)*$/;
            var formatoCP = /^(\d{4}|[a-zA-Z]\d{4}[a-zA-Z]{3})$/;

            $("#Req_TipoSociedadPJ").hide();
            $("#Req_RazonSocialPJ").hide();
            $("#Req_CuitPJ").hide();
            $("#ValDV_CuitPJ").hide();
            $("#ValCantidad_CuitPJ").hide();
            $("#ValFormato_CuitPJ").hide();
            $("#Req_TipoIngresosBrutosPJ").hide();
            $("#Req_IngresosBrutosPJ").hide();
            $("#ValFormato_IngresosBrutosPJ").hide();
            $("#Req_CallePJ").hide();
            $("#Req_NroPuertaPJ").hide();
            $("#Req_CPPJ").hide();
            $("#Req_ProvinciaPJ").hide();
            $("#Req_LocalidadPJ").hide();
            $("#Req_EmailPJ").hide();
            $("#ValFormato_EmailPJ").hide();
            $("#Req_FirmantesPJ").hide();            
            $("#<%: ValExiste_TitularPJ.ClientID %>").hide();
            $("#Req_CPPJ").hide();
            $("#Val_Formato_CPPJ").hide();
            
            var id_tiposociedad = $.trim($("#<%: ddlTipoSociedadPJ.ClientID %>").val());

            if ($.trim($("#<%: ddlTipoSociedadPJ.ClientID %>").val()).length == 0) {
                $("#Req_TipoSociedadPJ").css("display", "inline-block");
                ret = false;
            }
                    
            if ($.trim($("#<%: txtRazonSocialPJ.ClientID %>").val()).length == 0) {
                $("#Req_RazonSocialPJ").css("display", "inline-block");
                ret = false;
            }


            if ($.trim($("#<%: txtRazonSocialPJ.ClientID %>").val()).length == 0) {
                $("#Req_RazonSocialPJ").css("display", "inline-block");
                ret = false;
            }


            if ($.trim($("#<%: txtCuitPJ.ClientID %>").val()).length == 0) {
                $("#Req_CuitPJ").css("display", "inline-block");
                ret = false;
            }
            else {
                if ($.trim($("#<%: txtCuitPJ.ClientID %>").val()).length < 11) {
                        $("#ValCantidad_CuitPJ").css("display", "inline-block");
                        ret = false;
                    }

                    else if (!formatoCUIT.test($.trim($("#<%: txtCuitPJ.ClientID %>").val()))) {
                        $("#ValFormato_CuitPJ").css("display", "inline-block");
                        ret = false;
                    }
                    else if (!ValidarCuitSinGuiones($("#<%: txtCuitPJ.ClientID %>")[0])) {
                        $("#ValDV_CuitPJ").css("display", "inline-block");
                        ret = false;
                    }

            }


            if ($.trim($("#<%: ddlTipoIngresosBrutosPJ.ClientID %>").val()).length == 0) {
                $("#Req_TipoIngresosBrutosPJ").css("display", "inline-block");
                ret = false;
            }
            else {
                if (!$("#<%: txtIngresosBrutosPJ.ClientID %>").prop("disabled")) {

                    if ($.trim($("#<%: txtIngresosBrutosPJ.ClientID %>").val()).length == 0) {
                        $("#Req_IngresosBrutosPJ").css("display", "inline-block");
                        ret = false;
                    }
                    else {
                        if (!formatoIIBB.test($.trim($("#<%: txtIngresosBrutosPJ.ClientID %>").val()))) {
                            $("#ValFormato_IngresosBrutosPJ").text(strmsgFormatoIIBB);
                            $("#ValFormato_IngresosBrutosPJ").css("display", "inline-block");
                            ret = false;
                        }
                    }
                }
            }


            if ($.trim($("#<%: txtCallePJ.ClientID %>").val()).length == 0) {
                $("#Req_CallePJ").css("display", "inline-block");
                ret = false;
            }

            if ($.trim($("#<%: txtNroPuertaPJ.ClientID %>").val()).length == 0) {
                $("#Req_NroPuertaPJ").css("display", "inline-block");
                ret = false;
            }


            //Código postal
            if ($.trim($("#<%: txtCPPJ.ClientID %>").val()).length == 0) {
                $("#Req_CPPJ").css("display", "inline-block");
                ret = false;
            }
            else {
                if (!formatoCP.test($.trim($("#<%: txtCPPJ.ClientID %>").val()))) {
                    $("#Val_Formato_CPPJ").css("display", "inline-block");
                    ret = false;
                }
            }


            if ($.trim($("#<%: ddlProvinciaPJ.ClientID %>").val()).length == 0) {
                $("#Req_ProvinciaPJ").css("display", "inline-block");
                ret = false;
            }

            if ($.trim($("#<%: ddlLocalidadPJ.ClientID %>").val()).length == 0) {
                $("#Req_LocalidadPJ").css("display", "inline-block");
                ret = false;
            }

            if ($.trim($("#<%: txtEmailPJ.ClientID %>").val()).length > 0) {
                if (!formatoEmail.test($.trim($("#<%: txtEmailPJ.ClientID %>").val()))) {
                    $("#ValFormato_EmailPJ").css("display", "inline-block");
                    ret = false;
                }
            }
            else {
                $("#Req_EmailPJ").css("display", "inline-block");
                ret = false;
            }

            if (ret) {
                $("#pnlBotonesAgregarPJ").hide();
            }

            return ret;
        }

        function init_JS_updAgregarPersonaFisica() {

            toolTips();

            // Se realiza aqui y en el change para que ete correcto al editar y levanta por primera vez
            if ($("#<%: ddlTipoDocumentoPF.ClientID %>").val() != id_tipodoc_pasaporte) {
                $("#<%: txtNroDocumentoPF.ClientID %>").autoNumeric({ aSep: '', mDec: '0', vMax: '99999999' });
                 $("#<%: txtNroDocumentoPF.ClientID %>").autoNumeric("update");
             }
             else {
                 $("#<%: txtNroDocumentoPF.ClientID %>").autoNumeric("destroy");
             }

            $("#<%: txtCuitPJ.ClientID %>").autoNumeric({ aSep: '', mDec: '0', vMax: '99999999999' });
            $("#<%: txtCuitPF.ClientID %>").autoNumeric({ aSep: '', mDec: '0', vMax: '99999999999' });
            $("#<%: txtNroPuertaPF.ClientID %>").autoNumeric({ aSep: '', mDec: '0', vMax: '99999' });
            $("#<%: txtTelefonoPF.ClientID %>").autoNumeric({ aSep: '', mDec: '0', vMax: '9999999999999999999999' });
            
            

            $("#<%: txtApellidosPF.ClientID %>").on("keyup", function (e) {
                $("#Req_ApellidoPF").hide();
            });

            $("#<%: txtNombresPF.ClientID %>").on("keyup", function (e) {
                $("#Req_NombresPF").hide();
            });

            $("#<%: ddlTipoDocumentoPF.ClientID %>").on("change", function (e) {
                $("#Req_TipoNroDocPF").hide();


                if ($("#<%: ddlTipoDocumentoPF.ClientID %>").val() != id_tipodoc_pasaporte) {
                    $("#<%: txtNroDocumentoPF.ClientID %>").autoNumeric({ aSep: '', mDec: '0', vMax: '99999999' });
                    $("#<%: txtNroDocumentoPF.ClientID %>").autoNumeric("update");
                }
                else {
                    $("#<%: txtNroDocumentoPF.ClientID %>").autoNumeric("destroy");
                }

            });

            $("#<%: txtNroDocumentoPF.ClientID %>").on("keyup", function (e) {
                $("#Req_TipoNroDocPF").hide();
                $("#ValDNI_CuitPF").hide();
            });

            $("#<%: txtCuitPF.ClientID %>").on("keyup", function (e) {
                $("#Req_CuitPF").hide();
                $("#ValCantidad_CuitPF").hide();
                $("#ValFormato_CuitPF").hide();
                $("#ValDV_CuitPF").hide();
                $("#ValDNI_CuitPF").hide();
                $("#<%: ValExiste_TitularPF.ClientID %>").hide();
                
            });

            $("#<%: txtCallePF.ClientID %>").on("keyup", function (e) {
                $("#Req_CallePF").hide();
            });

            $("#<%: txtCPPF.ClientID %>").on("keyup", function (e) {
                $("#Req_CPPF").hide();
                $("#Val_Formato_CPPF").hide();
            });

            $("#<%: txtNroPuertaPF.ClientID %>").on("keyup", function (e) {
                $("#Req_NroPuertaPF").hide();
            });

            $("#<%: txtEmailPF.ClientID %>").on("keyup", function (e) {
                $("#Req_EmailPF").hide();
                $("#ValFormato_EmailPF").hide();
            });
           

            return false;
        }

        function init_JS_upd_ddlTipoIngresosBrutosPF() {

            $("#<%: ddlTipoIngresosBrutosPF.ClientID %>").on("change", function (e) {
                $("#Req_TipoIngresosBrutosPF").hide();
                $("#Req_IngresosBrutosPF").hide();
                $("#ValFormato_IngresosBrutosPF").hide();
            });

            return false;

        }

        function init_JS_upd_txtIngresosBrutosPF() {
            $("#<%: txtIngresosBrutosPF.ClientID %>").on("keyup", function (e) {
                $("#Req_IngresosBrutosPF").hide();
                $("#ValFormato_IngresosBrutosPF").hide();
            });

            return false;
        }
        function setFocus() {

            $("#<%: ddlTipoIngresosBrutosPJ.ClientID %>").focus();
                }
        function init_JS_updLocalidadPF() {

            $("#<%: ddlLocalidadPF.ClientID %>").on("change", function (e) {
                $("#Req_LocalidadPF").hide();
            });
            return false;
        }
  
        function init_JS_updAgregarPersonaJuridica() {

            toolTips();

            $("#<%: txtNroPuertaPJ.ClientID %>").autoNumeric({ aSep: '', mDec: '0', vMax: '99999' });


            $("#<%: txtCuitPJ.ClientID %>").on("keyup", function (e) {
                $("#Req_CuitPJ").hide();
                $("#ValCantidad_CuitPJ").hide();
                $("#ValFormato_CuitPJ").hide();
                $("#ValDV_CuitPJ").hide();
                $("#<%: ValExiste_TitularPJ.ClientID %>").hide();
            });

            $("#<%: txtCallePJ.ClientID %>").on("keyup", function (e) {
                $("#Req_CallePJ").hide();
            });

            $("#<%: txtEmailPJ.ClientID %>").on("keyup", function (e) {
                $("#Req_EmailPJ").hide();
                $("#ValFormato_EmailPJ").hide();
            });

            $("#<%: txtNroPuertaPJ.ClientID %>").on("keyup", function (e) {
                $("#Req_NroPuertaPJ").hide();
            });

            $("#<%: txtCPPJ.ClientID %>").on("keyup", function (e) {
                $("#Req_CPPJ").hide();
                $("#Val_Formato_CPPJ").hide();
            });


            return false;
        }

        function init_JS_upd_ddlTipoIngresosBrutosPJ() {

            $("#<%: ddlTipoIngresosBrutosPJ.ClientID %>").on("change", function (e) {
                $("#Req_TipoIngresosBrutosPJ").hide();
                $("#Req_IngresosBrutosPJ").hide();
                $("#ValFormato_IngresosBrutosPJ").hide();
            });

            return false;

        }

        function init_JS_upd_txtIngresosBrutosPJ() {
            $("#<%: txtIngresosBrutosPJ.ClientID %>").on("keyup", function (e) {
                $("#Req_IngresosBrutosPJ").hide();
                $("#ValFormato_IngresosBrutosPJ").hide();
            });

            return false;
        }

        function init_JS_updLocalidadPJ() {

            $("#<%: ddlLocalidadPJ.ClientID %>").on("change", function (e) {
                $("#Req_LocalidadPJ").hide();
            });
            return false;
        }

        function init_JS_updProvinciasPJ() {
            $("#<%: ddlProvinciaPJ.ClientID %>").on("change", function (e) {
                $("#Req_ProvinciaPJ").hide();
                $("#Req_LocalidadPJ").hide();
            });
            return false;
        }

        function init_JS_upd_ddlTipoSociedadPJ() {

             $("#<%: ddlTipoSociedadPJ.ClientID %>").on("change", function (e) {
                $("#Req_TipoSociedadPJ").hide();

            });

            $("#<%: ddlTipoSociedadPJ.ClientID %>").select2({
                placeholder: "Seleccionar",
                allowClear: true,
            });

            return false;
        }

        function init_JS_upd_txtRazonSocialPJ() {

            $("#<%: txtRazonSocialPJ.ClientID %>").on("keyup", function (e) {
                $("#Req_RazonSocialPJ").hide();
            });

            return false;
        }

        function init_Js_updgrillaTitularesSH() {
            toolTips();
            return false;
        }

        function showfrmAgregarTitularesSH() {

            $("#frmAgregarTitularesSH").modal({
                "show": true,
                "backdrop": "static"
            });
            return false;

        }
        function hidefrmAgregarTitularesSH() {

            $("#frmAgregarTitularesSH").modal("hide");
            return false;

        }
        function ocultarBotonesIngresarTitularesSH() {

            $("#pnlBotonesIngresarTitularesSH").hide();
            return false;

        }

        function validarAgregarTitSH() {

            var ret = true;

            var formatoEmail = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([.]\w+)*$/;

            $("#Req_ApellidosTitSH").hide();
            $("#Req_NombresTitSH").hide();
            $("#Req_TipoNroDocumentoTitSH").hide();
            $("#Req_EmailTitSH").hide();
            $("#Val_Formato_EmailTitSH").hide();

            $("#Req_ApellidosFirSH").hide();
            $("#Req_NombresFirSH").hide();
            $("#Req_TipoNroDocumentoFirSH").hide();
            $("#Req_EmailFirSH").hide();
            $("#Val_Formato_EmailFirSH").hide();
            $("#Req_TipoCaracterLegalFirSH").hide();
            $("#Req_CargoFirSH").hide();


            if ($.trim($("#<%: txtApellidosTitSH.ClientID %>").val()).length == 0) {
                $("#Req_ApellidosTitSH").css("display", "inline-block");
                ret = false;
            }

            if ($.trim($("#<%: txtNombresTitSH.ClientID %>").val()).length == 0) {
                $("#Req_NombresTitSH").css("display", "inline-block");
                ret = false;
            }

            if ($.trim($("#<%: ddlTipoDocumentoTitSH.ClientID %>").val()).length == 0 ||
                $.trim($("#<%: txtNroDocumentoTitSH.ClientID %>").val()).length == 0) {
                $("#Req_TipoNroDocumentoTitSH").css("display", "inline-block");
                ret = false;
            }
            if ($.trim($("#<%: txtEmailTitSH.ClientID %>").val()).length == 0 )
            {
                $("#Req_EmailTitSH").css("display", "inline-block");
                ret = false;
            }           

            if ($.trim($("#<%: txtEmailTitSH.ClientID %>").val()).length > 0) {
                if (!formatoEmail.test($.trim($("#<%: txtEmailTitSH.ClientID %>").val()))) {
                    $("#Val_Formato_EmailTitSH").css("display", "inline-block");
                    ret = false;
                }
            }
            else {
                $("#Req_EmailTitSH").css("display", "inline-block");
                ret = false;
            }

            if (ret) {
                ocultarBotonesIngresarTitularesSH();
            }
            return ret;
        }
        function validarGuardar() {
            ocultarBotonesGuardado();
        }
        function ocultarBotonesGuardado() {

            $("#pnlBotonesGuardar").hide();

            return true;
        }
  
    </script>

</asp:Content>
