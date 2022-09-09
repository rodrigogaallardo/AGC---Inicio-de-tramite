<%@ Page Title="Rubros" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Rubros.aspx.cs" Inherits="SSIT.Solicitud.Consulta_Padron.Rubros" %>
<%@ Register Src="~/Solicitud/Consulta_Padron/Controls/Rubros.ascx" TagPrefix="uc1" TagName="Rubros" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <%: Scripts.Render("~/bundles/autoNumeric") %>
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
            <asp:HiddenField ID="hid_DecimalSeparator" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <%--fin ajax cargando--%>

    <div id="page_content" Style="display:none">

        <h2>Rubros o Actividades</h2>
        <hr />

        <div class="row">

            <div class="col-sm-1 mtop10" style="width: 25px">
                <i class="imoon imoon-info fs24 " style="color:#377bb5"></i>
            </div>
            <div class="col-sm-11">

                <p class="pad10">
                    En este paso deber&aacute; ingresar los datos correspondientes a los rubros o actividades a las que est&aacute; destinado el local.
                </p>
                <ul>
                    <li>Los rubros son evaluados en funci&oacute;n de la zona donde se encuentra el local, por ello es necesario ingresar la ubicaci&oacute;n del local con anterioridad.</li>
                    <li>Es necesario haber informado la superficie del local para poder ingresar rubros o actividades a la solicitud en curso.</li>
                </ul>
            </div>
        </div>

        <br />
        <div class="box-panel">
                           <div style="margin:20px; margin-top:-5px"> 
                            <div style="color:#377bb5">                                 
                                        <h4><i class="imoon imoon-pencil" style="margin-right:10px"></i>Información ingresada en el trámite</h4>    
                                        <hr />                   
                                    </div>
                            </div>
                    <%--Información ingresada en el trámite--%>
                    <asp:UpdatePanel ID="updInformacionTramite" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                    
                            <div class="form-horizontal">
                                <div class="form-group">
                        
                                    <label class="control-label col-sm-3" style="width:200px">Tipo de Tr&aacute;mite:</label>
                                    <asp:Label ID="lblTipoTramite" runat="server" Text="0" CssClass="control-label col-sm-9 text-left" Style="font-weight: bold !important"></asp:Label>
                        
                                </div>

                                <div class="form-group">
                                    <label class="control-label col-sm-3" style="width: 200px">Superficie total a habilitar:</label>
                                    <asp:Label ID="lblSuperficieLocal" runat="server" Text="0" CssClass="control-label col-sm-9 text-left" Style="font-weight: bold !important"></asp:Label>
                                    <asp:HiddenField id="hid_Superficie_Local" runat="server" />

                                </div>

                                <div class="form-group">
                                    <label class="control-label col-sm-3" style="width: 200px">Zonificaci&oacute;n Declarada:</label>
                                    <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlZonaDeclarada" runat="server" Width="400px" CssClass="form-control" OnSelectedIndexChanged="ddlZonaDeclarada_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                    </div>
                                </div>


                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>


                    <%--Panel de Normativa--%>
                    <asp:UpdatePanel ID="updNormativa" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="pnlNormativa" runat="server" CssClass="pad20">
                        <div style="margin:20px; margin-top:-5px"> 
                            <div style="color:#377bb5">                                 
                                        <h4><i class="imoon imoon-file2" style="margin-right:10px"></i>Normativa</h4>    
                                        <hr />                   
                                    </div>
                            </div>

                                <asp:Panel ID="viewNormativa1" runat="server">
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <a href="#" class="btn btn-default" onclick="return showfrmAgregarNormativa();" >
                                                <i class="imoon imoon-plus"></i>
                                                <span class="text">Aplicar Normativa</span>
                                            </a>
                                        </div>
                                        <div class="col-sm-9">
                                            <p class="pleft10">
                                                Si se especifica una normativa, se permitir&aacute; ingresar cualquier rubro que se encuentre
                                                                en el nomenclador de habilitaciones vigente, sin importar la Superficie o Zona.
                                            </p>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="viewNormativa2" runat="server" >
                                    
                                    <div class="row">
                                        <div class="form-horizontal">
                                            <div class="form-group">

                                                <label class="control-label col-sm-3" style="width: 200px">Tipo de Normativa:</label>
                                                <asp:Label ID="lblTipoNormativa" runat="server" Text="0" CssClass="control-label col-sm-9 text-left" Style="font-weight: bold !important"></asp:Label>

                                            </div>
                                            <div class="form-group">

                                                <label class="control-label col-sm-3" style="width: 200px">Entidad Normativa:</label>
                                                <asp:Label ID="lblEntidadNormativa" runat="server" Text="0" CssClass="control-label col-sm-9 text-left" Style="font-weight: bold !important"></asp:Label>

                                            </div>
                                            <div class="form-group">

                                                <label class="control-label col-sm-3" style="width: 200px">Nro de normativa:</label>
                                                <asp:Label ID="lblNroNormativa" runat="server" Text="0" CssClass="control-label col-sm-9 text-left" style="font-weight:bold !important"></asp:Label>

                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="row">
                                        <asp:HiddenField ID="hid_id_entidad_normativa" runat="server" />
                                        <div class="col-sm-2">
                                            <a href="#" class="btn btn-default" onclick="return showfrmConfirmarEliminarNormativa();">
                                                <i class="imoon imoon-close"></i>
                                                <span class="text">Eliminar Normativa</span>
                                            </a>
                                        </div>

                                        <div class="col-sm-10 ">
                                            <label class="pleft30">
                                                Al haber especificado una normativa, se permite ingresar cualquier rubro que se
                                                encuentre en el nomenclador de habilitaciones vigente, sin importar la Superficie
                                                o Zona.
                                            </label>
                                        </div>
                                    </div>
                                </asp:Panel>
                                
                                <asp:Panel ID="pnlAgregaRubroUsoNoContemplado" runat="server" >
                                    <br />
                                    <div class="row" style="margin-left:15px">
                                        <div class="form-horizontal">
                                            <div class="form-group">                    
                                                <a href="#" class="btn btn-default" onclick="return showfrmAgregarRubroUsoNoContemplado();" >
                                                <i class="imoon imoon-plus"></i>
                                                <span class="text">Agregar Rubros Uso No contemplado Normativa</span>
                                            </a>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <div style="width: 100%; text-align: center">
                                    <asp:UpdateProgress ID="UpdateProgress5" AssociatedUpdatePanelID="updNormativa" runat="server"
                                        DisplayAfter="0">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                            <label>
                                                Actualizando...</label>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                            
                            </asp:Panel>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
     

        <br />
        <div class="box-panel">
                    <div style="margin:20px; margin-top:-5px"> 
        <div style="color:#377bb5">                                 
                    <h4><i class="imoon imoon-hammer" style="margin-right:10px"></i>Rubros</h4>    
                    <hr />                   
                </div>
        </div>

            <%-- contenido del box Rubros --%>

                    <div class="row mbottom10">
                        <div class="col-sm-12 text-right pright15">
                            <button id="btnAgregarRubros" class="btn btn-default pbottom5" onclick="return showfrmAgregarRubros();">
                                <i class="imoon imoon-plus"></i>
                                <span class="text">Agregar Rubro</span>
                            </button>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="updRubros" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <asp:GridView ID="grdRubrosIngresados" runat="server" AutoGenerateColumns="false" 
                                AllowPaging="false" Style="border: none;" CssClass="table table-bordered mtop5"
                                GridLines="None" Width="100% ">
                                <HeaderStyle CssClass="grid-header" />
                                <RowStyle CssClass="grid-row" />
                                <AlternatingRowStyle BackColor="#efefef" />
                                <Columns>
                                    <asp:BoundField DataField="CodidoRubro" HeaderText="Código" ItemStyle-Width="50px" HeaderStyle-HorizontalAlign="Left" />
                                    <asp:BoundField DataField="DescripcionRubro" HeaderText="Descripción" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"  />
                                    <asp:BoundField DataField="TipoActividadNombre" HeaderText="Actividad" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="150px" />
                                    <asp:ImageField DataImageUrlField="RestriccionZona" DataImageUrlFormatString="~/Common/Images/Rubros/{0}"
                                            HeaderText="Zona" ItemStyle-Width="50px" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" >
                                        </asp:ImageField>
                                        <asp:ImageField DataImageUrlField="RestriccionSup" DataImageUrlFormatString="~/Common/Images/Rubros/{0}"
                                            HeaderText="Sup" ItemStyle-Width="50px" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" >
                                        </asp:ImageField>
                                        <asp:BoundField DataField="EsAnterior" HeaderText="Anterior" Visible="false" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                                        <asp:BoundField DataField="SuperficieHabilitar" HeaderText="Superficie" Visible="true" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />     
                                        <asp:TemplateField ItemStyle-Width="140px" HeaderText="Eliminar" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" >
                                        <ItemTemplate>
                                            
                                            <asp:LinkButton ID="btnEliminarRubro" runat="server"  ToolTip="Eliminar"  data-id-rubro-eliminar='<%# Eval("IdConsultaPadronRubro") %>' CssClass="link-local"
                                                OnClientClick="return showConfirmarEliminarRubro(this);">
                                                <i class="imoon imoon-close"></i>
                                                
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
                            <asp:HiddenField ID="hid_id_caarubro_eliminar" runat="server" />

                            <asp:Panel ID="pnlObservaciones" runat="server" Style="margin-top: 5px">
                                <label><b>
                                    Observaciones</b></label>
                                    <asp:TextBox ID="txtObservaciones" runat="server" MaxLength="300" TextMode="MultiLine" CssClass="form-control"
                                        Height="80px" Style="overflow: hidden; margin-top: 5px" onkeyup="maxlength('txtObservaciones',300);">
                            
                                    </asp:TextBox>

                            </asp:Panel>

                        </ContentTemplate>
                    </asp:UpdatePanel>


        </div>

        <%--Botones de Guardado--%>
        <asp:UpdatePanel ID="updBotonesGuardar" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <div class="form-inline text-right mtop20">
                    <div id="pnlBotonesGuardar" class="form-group">

                        <asp:LinkButton ID="btnContinuar" runat="server" CssClass="btn btn-lg btn-primary" OnClick="btnContinuar_Click" OnClientClick="return validarGuardar();">
                                <i class="imoon imoon-disk"></i>
                                <span class="text">Guardar y Continuar</span>
                        </asp:LinkButton>

                    </div>
                    <div class="form-group">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="200" AssociatedUpdatePanelID="updBotonesGuardar">
                            <ProgressTemplate>
                                <img src='<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>' style="margin-left: 10px" alt="loading" />Guardando...
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>

    </div>

    <%--Modal form Agregar normativa--%>
    <div id="frmAgregarNormativa" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top:-8px">Agregar Normativa</h4>
                </div>
                <div class="modal-body pbottom5" >
                    <asp:UpdatePanel ID="updAgregarNormativa" runat="server">
                        <ContentTemplate>

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <p class="control-label col-sm-3">Tipo de Normativa:</p>
                                    <div class="col-sm-9">
                                        <asp:DropDownList ID="ddlTipoNormativa" runat="server" Width="400px" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <p class="control-label col-sm-3">Entidad Normativa:</p>
                                    <div class="col-sm-9">
                                        <asp:DropDownList ID="ddlEntidadNormativa" runat="server" Width="400px" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <p class="control-label col-sm-4" style="margin-left:-50px">N&uacute;mero Normativa:</p>
                                    <div style="margin-left:165px">
                                        <asp:TextBox ID="txtNroNormativa" runat="server" MaxLength="15" Width="120px" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div id="ReqtxtNormativa" class="form-group mbottom5" style="display: none">
                                    <div class="col-sm-offset-3 col-sm-9">
                                        <div class="field-validation-error" >
                                            <span>El N&uacute;mero de normativa es obligatorio.</span>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <div class="modal-footer mtop5 mleft20 mright20"  style="margin-left:20px; margin-right:20px">

                    <asp:UpdatePanel ID="updBotonesIngresarNormativa" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <div class="form-inline text-right">
                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress_updBotonesIngresarNormativa" AssociatedUpdatePanelID="updBotonesIngresarNormativa"
                                        runat="server" DisplayAfter="200">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>

                                <div class="form-group">
                                    <asp:LinkButton ID="btnIngresarNormativa" runat="server" CssClass="btn btn-default" OnClientClick="return validarAgregarNormativa();" OnClick="btnIngresarNormativa_Click">
                                        <i class="imoon imoon-ok"></i>
                                        <span class="text">Aceptar</span>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnCerrarNormativa" runat="server" CssClass="btn btn-default" data-dismiss="modal">
                                       <i class="imoon imoon-close"></i>
                                        <span class="text">Cancelar</span>
                                    </asp:LinkButton>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->
      <div id="frmAgregarActividades" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top:-8px">Agregar Actividades no existentes en el nomenclador</h4>
                </div>
                <div class="modal-body pbottom5">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="control-label col-sm-3">Descripción de la actividad:</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtDesc_runc" runat="server" Width="400px" CssClass="form-control">
                                        </asp:TextBox>
                                    </div>
                                    <div id="Req_txtDesc_runc" class="form-group mbottom5" style="display: none">
                                    <div class="col-sm-offset-3 col-sm-9">
                                        <div class="field-validation-error" >
                                            <span>Debe ingresar la descripción de la Actividad que desea ingresar.</span>
                                        </div>
                                    </div>
                                </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-3">Documentación Requerida::</label>
                                    <div class="col-sm-9">
                                        <asp:DropDownList ID="ddlTipoDocReq_runc" runat="server" Width="400px" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-3">Tipo Actividad:</label>
                                    <div class="col-sm-9">
                                        <asp:DropDownList ID="ddlTipoActividad_runc" runat="server" Width="400px" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="control-label col-sm-3">Superficie</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtSuperficieRubro_runc" runat="server" MaxLength="15" Width="120px" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div id="Req_TxtSuperficie" class="form-group mbottom5" style="display: none">
                                    <div class="col-sm-offset-3 col-sm-9">
                                        <div class="field-validation-error" >
                                            <span>La superficie a habilitar debe ser un número entre 1 y la superficie total del local.</span>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
                <div class="modal-footer mtop5 mleft20 mright20" style="margin-left:20px; margin-right:20px">

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <div class="form-inline text-right">
                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress8" AssociatedUpdatePanelID="updBotonesIngresarNormativa"
                                        runat="server" DisplayAfter="200">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>

                                <div class="form-group">
                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-default" OnClientClick="return validarAgregarRubroNoContemplado();" OnClick="btnAgregarRubroUsoNoContemplado_Click">
                                        <i></i>
                                        <span class="text">Aceptar</span>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton4" runat="server" CssClass="btn btn-default" data-dismiss="modal">
                                        <i></i>
                                        <span class="text">Cancelar</span>
                                    </asp:LinkButton>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>
    <%--Modal Confirmar Eliminar Normativa--%>
    <div id="frmConfirmarEliminarNormativa" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top:-8px">Eliminar Normativa</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon-remove-circle fs64 color-blue"></i>
                            </td>
                            <td style="vertical-align: middle">
                                <label class="mleft10">¿ Est&aacute; seguro de eliminar la Normativa ?</label>
                            </td>
                        </tr>
                    </table>

                </div>
                <div class="modal-footer mleft20 mright20">

                    <asp:UpdatePanel ID="updConfirmarEliminarNormativa" runat="server">
                        <ContentTemplate>

                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updConfirmarEliminarNormativa">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div id="pnlBotonesConfirmacionEliminarnormativa" class="form-group">
                                   <asp:Button ID="btnEliminarNormativa" runat="server" CssClass="btn btn-default" Text="Sí" OnClick="btnEliminarNormativa_Click" OnClientClick="ocultarBotonesConfirmacionEliminarNormativa();" />
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
    
    <%--Modal Confirmar Eliminar Rubro--%>
    <div id="frmConfirmarEliminarRubro" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top:-8px">Eliminar Rubro</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon-remove-circle fs64 color-blue"></i>
                            </td>
                            <td style="vertical-align: middle">
                                <label class="mleft10">¿ Est&aacute; seguro de eliminar el Rubro ?</label>
                            </td>
                        </tr>
                    </table>

                </div>
                <div class="modal-footer mleft20 mright20">

                    <asp:UpdatePanel ID="updConfirmarEliminarRubro" runat="server">
                        <ContentTemplate>

                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="updConfirmarEliminarRubro">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div id="pnlBotonesConfirmacionEliminarRubro" class="form-group">
                                   <asp:Button ID="Button1" 
                                       runat="server" 
                                       CssClass="btn btn-default" 
                                       Text="Sí" 
                                       OnClick="btnEliminarRubro_Click"
                                       OnClientClick="ocultarBotonesConfirmacionEliminarRubro();" 
                                    />
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

    <%--Modal form Agregar Rubros--%>
    <div id="frmAgregarRubros" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top:-8px">Agregar Rubros</h4>
                </div>
                <div class="modal-body pbottom20">
                    <asp:UpdatePanel ID="updBuscarRubros" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlBuscarRubros" runat="server" CssClass="form-horizontal" DefaultButton="btnBuscar">

                                <div class="form-group">
                                    <h3 class="pleft20 col-sm-12">B&uacute;squeda de Rubros</h3>
                                </div>
                                <div class="form-group">
                                    <p class="control-label col-sm-3">Superficie del rubro:</p>
                                    <div class="col-sm-1">
                                        <asp:TextBox ID="txtSuperficie" runat="server" Width="100px" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <p class="control-label col-sm-3" style="margin-top:-15px">Ingrese el código o parte de la descipción del rubro a buscar:</p>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-offset-3 col-sm-9 ptop5">
                                        <div id="Req_txtBuscar" class="field-validation-error" style="display: none">
                                            Debe ingresar al menos 3 caracteres para iniciar la b&uacute;squeda.
                                        </div>
                                    </div>
                                </div>

                                <hr class="mbottom0 mtop0" />
                                

                                <asp:UpdatePanel ID="updBotonesBuscarRubros" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>

                                        <asp:Panel ID="pnlBotonesBuscarRubros" runat="server" CssClass="form-inline text-right">
                                            <div class="form-group">
                                                <asp:UpdateProgress ID="UpdateProgress3" AssociatedUpdatePanelID="updBotonesBuscarRubros"
                                                    runat="server" DisplayAfter="200">
                                                    <ProgressTemplate>
                                                        <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                                        Buscando...
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </div>

                                            <asp:Panel ID="BotonesBuscarRubros" runat="server" CssClas="form-group" DefaultButton="btnBuscar">
                                                <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-primary" OnClick="btnBuscar_Click" OnClientClick="return validarBuscar();">
                                                    <i class="imoon imoon-search"></i>
                                                    <span class="text">Buscar</span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-default" data-dismiss="modal">
                                                    <i class="imoon imoon-close"></i>
                                                    <span class="text">Cerrar</span>
                                                </asp:LinkButton>
                                            </asp:Panel>
                                        </asp:Panel>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                    
                            </asp:Panel>
                   
                            <asp:Panel ID="pnlResultadoBusquedaRubros" runat="server" CssClass="form-horizontal" style="display:none">

                                <div style="max-height:500px; overflow-y:auto">

                                    <asp:GridView ID="grdRubros" runat="server" 
                                        AutoGenerateColumns="false" 
                                        DataKeyNames="IdRubro,Codigo,Superficie"
                                        AllowPaging="true" 
                                        PageSize="10"
                                        Style="border: none;" 
                                        CssClass="table table-bordered mtop5"
                                        ItemType="DataTransferObject.RubrosDTO"
                                        GridLines="None" 
                                        Width="100%"
                                        OnPageIndexChanging="grdRubros_PageIndexChanging"
                                        OnDataBound="grdRubros_DataBound"                                        
                                        >
                                        <HeaderStyle CssClass="grid-header" />
                                        <RowStyle CssClass="grid-row" />
                                        <AlternatingRowStyle BackColor="#efefef" />
                                       <Columns>

                                <asp:BoundField DataField="Codigo" HeaderText="Código" />
                                <asp:BoundField DataField="Nombre" HeaderText="Descripción" />
                                <asp:BoundField DataField="TipoActividadNombre" HeaderText="Actividad" />
                                <asp:ImageField DataImageUrlField="RestriccionZona" DataImageUrlFormatString="~/Common/Images/Rubros/{0}"
                                    HeaderText="Zona">
                                </asp:ImageField>
                                <asp:ImageField DataImageUrlField="RestriccionSup" DataImageUrlFormatString="~/Common/Images/Rubros/{0}"
                                    HeaderText="Sup">
                                </asp:ImageField>                                
                                <asp:BoundField DataField="Superficie" HeaderText="Superficie" Visible="true" />
                                <asp:TemplateField HeaderText="Ingresar">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkRubroElegido" runat="server" />
                                </ItemTemplate>
                                </asp:TemplateField>
                            
                            </Columns>
                                        <PagerTemplate >
                                
                                <asp:UpdatePanel ID="updPnlpager" runat="server" >
                                    <ContentTemplate>
                                
                                    <asp:Panel ID="pnlpager" runat="server" Style="padding: 10px; text-align: center;border-top: solid 1px #e1e1e1">

                                    
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
                                    
                                    <div style="display:inline-table">

                                        <asp:UpdateProgress ID="UpdateProgress7" AssociatedUpdatePanelID="updPnlpager" runat="server"
                                            DisplayAfter="0">
                                            <ProgressTemplate>
                                                <img src="../Common/Images/Controles/Loading24x24.gif" alt="" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </div>
                                    
                                    </asp:Panel>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                
                            </PagerTemplate>  
                                        <EmptyDataTemplate>

                                            <div class="mtop10">

                                                <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                                                <span class="mleft10">No se encontraron registros.</span>

                                            </div>

                                        </EmptyDataTemplate>
                                    </asp:GridView>

                                </div>

                                <asp:UpdatePanel ID="updBotonesAgregarRubros" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>

                                        <asp:Panel ID="pnlGrupoAgregarRubros" runat="server" CssClass="row ptop10 pleft10 pright10" Style="display: none">
                                            <div class="col-sm-3">

                                            <asp:LinkButton ID="btnnuevaBusqueda" runat="server" CssClass="btn btn-default" OnClick="btnnuevaBusqueda_Click">
                                                    <i class="imoon imoon-search"></i>
                                                    <span class="text">Nueva B&uacute;squeda</span>
                                                </asp:LinkButton>
                                            </div>

                                            <div class="col-sm-6 pleft20">

                                                <asp:UpdatePanel ID="updValidadorAgregarRubros" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Panel ID="ValidadorAgregarRubros" runat="server" CssClass="field-validation-error" Style="display: none">
                                                            <asp:Label ID="lblValidadorAgregarRubros" runat="server"></asp:Label>
                                                        </asp:Panel>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>


                                            
                                            <asp:Panel ID="pnlBotonesAgregarRubros" runat="server" CssClass="col-sm-3 text-right">

                                                <asp:UpdateProgress ID="UpdateProgress4" AssociatedUpdatePanelID="updBotonesAgregarRubros"
                                                    runat="server" DisplayAfter="200">
                                                    <ProgressTemplate>
                                                        <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                                        Procesando...
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>


                                                <div id="BotonesAgregarRubros">
                                                    <asp:LinkButton ID="btnIngresarRubros" runat="server" CssClass="btn btn-primary" OnClick="btnIngresarRubros_Click" OnClientClick="ocultarBotonesAgregarRubros();">
                                                        <i class="imoon imoon-plus"></i>
                                                        <span class="text">Agregar</span>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-default" data-dismiss="modal">
                                                        <i class="imoon imoon-close"></i>
                                                        <span class="text">Cerrar</span>
                                                    </asp:LinkButton>
                                                </div>

                                            </asp:Panel>
                                        </asp:Panel>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </asp:Panel>
                            

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
               
            </div>
        </div>
    </div>
    <!-- /.modal -->

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
                                <i class="imoon imoon-remove-circle fs64" style="color: #f00"></i>
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

        $(document).ready(function () {

            $("#page_content").hide();
            $("#Loading").show();

            $("#<%= btnCargarDatos.ClientID %>").click();

            init_JS_updAgregarNormativa();

        });

        function finalizarCarga() {

            $("#Loading").hide();
            $("#page_content").show();

            return false;
        }

        function init_JS_updAgregarNormativa() {

            $("#<%: txtNroNormativa.ClientID %>").on("keyup", function () {
                $("#ReqtxtNormativa").hide();
            });


        }


        function showfrmError() {
            $("#pnlBotonesGuardar").show();
            $("#frmError").modal("show");
            return false;

        }

        function showfrmConfirmarEliminar() {

            $("#pnlBotonesConfirmacion").show();
            $("#frmConfirmarEliminar").modal("show");
            return false;
        }

        function hidefrmConfirmarEliminar() {

            $("#frmConfirmarEliminar").modal("hide");
            return false;
        }

        function ocultarBotonesConfirmacion() {

            $("#pnlBotonesConfirmacion").hide();
            return false;
        }

        function ocultarBotonesGuardado() {

            $("#pnlBotonesGuardar").hide();

            return true;
        }

        function showfrmAgregarNormativa() {

            $("#<%: txtNroNormativa.ClientID %>").val("");
            $("#<%: ddlTipoNormativa.ClientID %>")[0].selectedIndex = 0;
            $("#<%: ddlEntidadNormativa.ClientID %>")[0].selectedIndex = 0;
            $("#ReqtxtNormativa").hide();
            $("#frmAgregarNormativa").modal("show");
            return false;
        }
        function showfrmAgregarRubroUsoNoContemplado() {

            $("#frmAgregarActividades").modal("show");

            $("#<%: txtDesc_runc.ClientID %>").val("");
            $("#<%: txtSuperficieRubro_runc.ClientID %>").val("");
            $("#<%: ddlTipoActividad_runc.ClientID %>")[0].selectedIndex = 0;
            $("#<%: ddlTipoDocReq_runc.ClientID %>")[0].selectedIndex = 0;

            $("#Req_txtSuperficieRubro_runc").hide();
            $("#Req_txtDesc_runc").hide();

            return false;
        }

        function hidefrmAgregarNormativa() {

            $("#frmAgregarNormativa").modal("hide");
            return false;
        }
        function hidefrmAgregarRubroUsoNoContemplado() {
            $("#frmAgregarActividades").modal("hide");
            return false;
        }
        function ocultarBotonesConfirmacionEliminarNormativa() {
            $("#pnlBotonesConfirmacionEliminarnormativa").hide();
            return false;
        }

        function showfrmConfirmarEliminarNormativa() {

            $("#frmConfirmarEliminarNormativa").modal("show");
            return false;
        }

        function hidefrmConfirmarEliminarNormativa() {

            $("#frmConfirmarEliminarNormativa").modal("hide");
            return false;
        }

        function showfrmAgregarRubros() {

            $("#<%: txtSuperficie.ClientID %>").val($("#<%: hid_Superficie_Local.ClientID %>").val());
            $("#<%: txtBuscar.ClientID %>").val("");
            $("#<%: pnlBuscarRubros.ClientID %>").show();
            $("#<%: pnlResultadoBusquedaRubros.ClientID %>").hide();
            $("#<%: pnlBotonesAgregarRubros.ClientID %>").hide();
            $("#<%: pnlBotonesBuscarRubros.ClientID %>").show();

            $("#<%: BotonesBuscarRubros.ClientID %>").show();


            $("#frmAgregarRubros").on("shown.bs.modal", function (e) {
                $("#<%: txtBuscar.ClientID %>").focus();
            });

            $("#frmAgregarRubros").modal({
                "show": true,
                "backdrop": "static"

            });

            return false;
            }

            function hidefrmAgregarRubros() {

                $("#frmAgregarRubros").modal("hide");
                return false;
            }

            function ocultarBotonesBusquedaRubros() {
                $("#<%: BotonesBuscarRubros.ClientID %>").hide();
            return false;
        }

        function ocultarBotonesConfirmacionEliminarRubro() {
            $("#pnlBotonesConfirmacionEliminarRubro").hide();
            return false;
        }

        function ocultarValidadorAgregarRubros() {

            $("#<%: ValidadorAgregarRubros.ClientID %>").hide();
            return false;
        }

        function ocultarBotonesAgregarRubros() {

            $("#BotonesAgregarRubros").hide();
            return false;
        }

        function showConfirmarEliminarRubro(obj) {

            var id_caarubro_eliminar = $(obj).attr("data-id-rubro-eliminar");
            $("#<%: hid_id_caarubro_eliminar.ClientID %>").val(id_caarubro_eliminar);

            $("#frmConfirmarEliminarRubro").modal("show");
            return false;
        }

        function hidefrmConfirmarEliminarRubro() {

            $("#frmConfirmarEliminarRubro").modal("hide");
            return false;
        }

        function validarAgregarNormativa() {
            var ret = true;

            $("#ReqtxtNormativa").hide();
            if ($("#<%: txtNroNormativa.ClientID %>").val().length == 0) {
                $("#ReqtxtNormativa").show();
                ret = false;
            }

            return ret;

        }

        function validarAgregarRubroNoContemplado() {
            var ret = true;

            $("#Req_TxtSuperficie").hide();
            if ($("#<%: txtSuperficieRubro_runc.ClientID %>").val().length == 0) {
                $("#Req_TxtSuperficie").show();
                ret = false;
            }

            $("#Req_txtDesc_runc").hide();
            if ($("#<%: txtDesc_runc.ClientID %>").val().length == 0) {
                $("#Req_TxtReq_txtDesc_runcSuperficie").show();
                ret = false;
            }


            return ret;
        }

        function validarBuscar() {

            var ret = true;
            $("#Req_txtBuscar").hide();

            var value1 = $("#<%: txtSuperficie.ClientID %>").val();
            var value2 = $("#<%: hid_Superficie_Local.ClientID %>").val();
            var superficie = stringToFloat(value1);
            var superficieMaxima = stringToFloat(value2);

            if ($("#<%: txtBuscar.ClientID %>").val().length < 3) {
                $("#Req_txtBuscar").css("display", "inline-block");
                ret = false;
            }

            if (ret) {
                ocultarBotonesBusquedaRubros();
                $("#<%: pnlGrupoAgregarRubros.ClientID %>").css("display", "block");
            }

            return ret;
        }

        function validarGuardar() {
            var ret = true;

            if (ret)
                ocultarBotonesGuardado();

            return ret;
        }


    </script>
</asp:Content>
