<%@ Page  Title="Conformación del Local" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConformacionLocal.aspx.cs" Inherits="AnexoProfesionales.ConformacionLocal" %>

<%@ Register Src="~/Tramites/Habilitacion/Controls/ConformacionLocalDatos.ascx" TagPrefix="uc1" TagName="ConformacionLocalDatos" %>
<%@ Register Src="~/Tramites/Habilitacion/Controls/Titulo.ascx" TagPrefix="uc1" TagName="Titulo" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .widget-title, .modal-header, .table th, div.dataTables_wrapper .ui-widget-header {
            background-color: #ffffff;
            background-image: none;
        }
    </style>
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
            <asp:Button ID="btnCargarDatos" runat="server"  Style="display: none" OnClick="btnCargarDatos_Click" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="page_content" Style="display:none">
        <asp:HiddenField ID="hid_DecimalSeparator" runat="server" />
        <asp:HiddenField ID="hid_return_url" runat="server" />
        <asp:HiddenField ID="hid_id_encomienda" runat="server" />
        <asp:HiddenField ID="hid_id_encomiendadatoslocal" runat="server" />
        <uc1:Titulo runat="server" ID="Titulo" />
        <hr />
        <p>
            En este paso deberá ingresar los datos correspondientes a la conformación del local.
        </p>
        <br />
        <ul style="margin: auto; padding-left: 40px">
                        <li>Si el tipo de trámite es sin planos, la conformación del local es obligatoria, presione el link 'Continuar' cuando finalice la carga.</li>
                    </ul>
         <br /><br />
        <asp:UpdatePanel ID="updConformacionLocal" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
        <asp:Panel ID="pnlSobrecarga" runat="server" CssClass="box-panel">
            <%--Certificado de SobreCarga--%>
           
                    <div style="margin:20px; margin-top:-5px"> 
                    <div style="margin-top:5px; color:#377bb5">                                 
                        <h4><i class="imoon imoon-office" style="margin-right:10px"></i>Información ingresada en el trámite</h4>   
                        <hr />                    
                    </div>
            </div>

             <asp:UpdatePanel ID="updConformacionIngresada" runat="server" UpdateMode="Conditional">
                
                        <ContentTemplate>
    
                        <div style="padding: 0px 20px 20px 20px; width: 100%">

                            <asp:GridView ID="grdConformacionLocal" 
                                runat="server" 
                                AutoGenerateColumns="false"
                                DataKeyNames="id_encomiendaconflocal" 
                                AllowPaging="false"  ShowHeader="false"
                                Style="border: none;margin-top: 10px" GridLines="None" Width="1100px"
                                CellPadding="3" 
                                ItemType="DataTransferObject.EncomiendaConformacionLocalDTO"
                                OnRowCommand="grdConformacionLocal_RowCommand" OnRowDataBound="grdConformacionLocal_RowDataBound">
     

                                <HeaderStyle CssClass="grid-header"  HorizontalAlign="Left" />
                                <PagerStyle CssClass="grid-pager" HorizontalAlign="Center" />
                                <FooterStyle CssClass="grid-footer"  HorizontalAlign="Left"/>
                                <RowStyle CssClass="grid-row" HorizontalAlign="Left" />
                                <AlternatingRowStyle CssClass="grid-alternating-row" HorizontalAlign="Left" />
                                <EditRowStyle CssClass="grid-edit-row" HorizontalAlign="Left" />
                                <SelectedRowStyle CssClass="grid-selected-row" HorizontalAlign="Left" />

                                <Columns>

                                    <asp:TemplateField ItemStyle-Width="100%">
                                        <ItemTemplate>
                                        <div id="box_resulConform" class="accordion-group widget-box"  style="background:#ffffff; margin-top:-5px">

                                           <div class="accordion-heading">
                                            <a id="A5" data-parent="#collapse-group" href="#<%# Item.id_encomiendaconflocal %>"
                                                data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                                                <div class="widget-title">
                                                    <span class="icon"><i class="imoon imoon-puzzle" style="color:#344882;"></i></span>
                                                    
                                                <div class="form-horizontal">
                                                 <div class="form-group ">
                                                    <label class="col-sm-2 control-label">Destino:</label>
                                                    <div class="col-sm-3 control-label text-left">
                                                        <asp:label ID="txtGrillaDestino"  runat="server" Text='<%# Item.TipoDestinoDTO.Nombre %> ' 
                                                            Enabled="false" Width="100%" ></asp:label>
                                                    </div>
                                                    
                                                    <label class="col-sm-2 control-label">Planta:</label>
                                                    <div class="col-sm-3 control-label text-left">
                                                        <asp:label ID="txtGrillaPlanta"  runat="server" Text='<%#  (Item.EncomiendaPlantasDTO != null) ? (Item.EncomiendaPlantasDTO.IdTipoSector == (int)StaticClass.Constantes.TipoSector.Piso 
                                                                                                                    || Item.EncomiendaPlantasDTO.IdTipoSector == (int)StaticClass.Constantes.TipoSector.Otro) 
                                                                                                                    ? Item.EncomiendaPlantasDTO.TipoSectorDTO.Nombre + " " + Item.EncomiendaPlantasDTO.detalle_encomiendatiposector
                                                                                                                    : Item.EncomiendaPlantasDTO.TipoSectorDTO.Nombre : "" %>' 
                                                            Enabled="false" Width="100%" ></asp:label>
                                                    </div>
                                                        <span class="btn-right mright15"><i class="imoon imoon-chevron-down" style="color:#344882;"></i></span>
                                                </div>
                                                </div>
                                                 
                                                </div>
                                            </a>
                                        </div>

                                        <div class="accordion-body collapse" id="<%# Item.id_encomiendaconflocal %>">
                                          <div class="pull-right  mtop10 mright20">
                                                <asp:LinkButton ID="btnEditarDetalleConformacion" runat="server" CssClass="btn btn-default" title="Editar"
                                                    CommandName="EditarDetalle" CommandArgument='<% #Eval("id_encomiendaconflocal") %>' >
                                                    <i class="imoon imoon-pencil" style="color:#377bb5"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnEliminarDetalleConformacion" runat="server" CssClass="btn btn-default" title="Eliminar"
                                                    CommandName="EliminarDetalle" CommandArgument='<%# Eval("id_encomiendaconflocal") %>'
                                                        OnClientClick="return confirm('¿Está seguro que desea eliminar este detalle de la conformación del local?');" >
                                                    <i class="imoon imoon-close" style="color:#377bb5"></i>                                               
                                                </asp:LinkButton>

                                            </div>
                                            <br />
                                                <div class="form-horizontal">
                    

                                            
                                                 <div class="form-group form-group-sm">
                                                    <label class="col-sm-2 control-label">Largo:</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtGrillaLargo" CssClass="form-control" runat="server" Text='<% #Eval("largo_conflocal") %>' 
                                                            Enabled="false" Width="100%" ></asp:TextBox>
                                                    </div>
                                                     
                                                    <label class="col-sm-2 control-label">Paredes:</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtlGrillaPared" CssClass="form-control" runat="server" Text='<% #Eval("Paredes_conflocal") %>' 
                                                            Enabled="false" Width="100%" ></asp:TextBox>
                                                    </div>
                                                     </div>

                                                    
                                                 <div class="form-group form-group-sm">
                                                    <label class="col-sm-2 control-label">Ancho:</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtGrillaAncho" CssClass="form-control" runat="server" Text='<% #Eval("ancho_conflocal") %>' 
                                                            Enabled="false" Width="100%" ></asp:TextBox>
                                                        </div>
                                                     
                                                    <label class="col-sm-2 control-label">Techos::</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtGrillaTecho" CssClass="form-control" runat="server" Text='<% #Eval("Techos_conflocal") %>' 
                                                            Enabled="false" Width="100%" ></asp:TextBox>
                                                        </div>
                                                     </div>

                                                    
                                                 <div class="form-group form-group-sm">
                                                    <label class="col-sm-2 control-label">Alto:</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtGrillaAlto" CssClass="form-control" runat="server" Text='<% #Eval("alto_conflocal") %>' 
                                                            Enabled="false" Width="100%" ></asp:TextBox>
                                                        </div>
                                                     
                                                    <label class="col-sm-2 control-label">Pisos:</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtGrillaPiso" CssClass="form-control" runat="server" Text='<% #Eval("Pisos_conflocal") %>' 
                                                            Enabled="false" Width="100%" ></asp:TextBox>
                                                        </div>
                                                     </div>

                                                    
                                                 <div class="form-group form-group-sm">
                                                    <label class="col-sm-2 control-label">Tipo Superficie:</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" 
                                                            Text='<%# Item.TipoSuperficieDTO.Descripcion %>' 
                                                            Enabled="false" Width="100%" ></asp:TextBox>
                                                        </div>
                                                     
                                                    <label class="col-sm-2 control-label">Sup. Estimada:</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtGrillaSupEstimada" CssClass="form-control" runat="server" 
                                                            Text='<% # decimal.Parse( Eval("superficie_conflocal").ToString() ).ToString("N2")  %>' 
                                                            Enabled="false" Width="100%" ></asp:TextBox>
                                                        </div>
                                                     </div>
                                                    
                                                 <div class="form-group form-group-sm">
                                                    <label class="col-sm-2 control-label">Ventilación:</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtGrillaVentilacion" CssClass="form-control" runat="server" Text='<%# Item.TipoVentilacionDTO != null ? Item.TipoVentilacionDTO.nom_ventilacion  :  "" %>' 
                                                            Enabled="false" Width="100%" ></asp:TextBox>
                                                        </div>
                                                     
                                                    <label class="col-sm-2 control-label">Iluminación:</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" Text='<%# Item.TipoIluminacionDTO != null ? Item.TipoIluminacionDTO.nom_iluminacion : "" %>' 
                                                            Enabled="false" Width="100%" ></asp:TextBox>
                                                        </div>
                                                     </div>
                                                    
                                                 <div class="form-group form-group-sm">
                                                     
                                                    <label class="col-sm-2 control-label">Frisos:</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtGrillaFriso" CssClass="form-control" runat="server" 
                                                            Text='<% # Eval("Frisos_conflocal") %>' 
                                                            Enabled="false" Width="100%" ></asp:TextBox>
                                                        </div>
                                                     
                                                    <label class="col-sm-2 control-label">Observaciones:</label>
                                                     <div class="col-sm-3">
                                                         
                                                        <asp:TextBox ID="txtlGrillaObserv" CssClass="form-control" runat="server" Text='<% #Eval("Observaciones_conflocal") %>' 
                                                            Enabled="false" Width="100%" Height="100px" 
                                                            TextMode="MultiLine" style="overflow:auto;"></asp:TextBox>
                                                     </div>

                                                     </div>
                                                </div>
               
                                           
                                         </div>  </div>  
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>

                                <EmptyDataTemplate>
                                    <div class="mtop10">
                                         <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                                        <span class="mleft10">No hay datos aún...</span>
                                    </div>
                                </EmptyDataTemplate>

                            </asp:GridView>

                        </div>
                            
                        <asp:panel ID="pnlSuperficie" runat="server" CssClass="form-horizontal" >                           
                            <div class="form-group form-group-sm">
                                                     
                               <label class="col-sm-3 control-label">Superficie Total Estimada:</label>
                               <div class="col-sm-3">
                                     <asp:TextBox ID="txtSupTotal"  CssClass="form-control" runat="server" Text='' 
                                     Enabled="false" Width="100%" ></asp:TextBox>
                              </div>
                              <label class="col-sm-2 control-label">Superficie Total</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtSupTotalLocal"  CssClass="form-control" runat="server" Text='' 
                                        Enabled="false" Width="100%" ></asp:TextBox>
                                    </div>
                              </div>
                        </asp:panel>
                        
                        </ContentTemplate>
                          
                    </asp:UpdatePanel>
                                           
        <%--Boton Agregar--%>
             <div id="reqSupIguales" runat="server" class="alert alert-small alert-danger mbottom0 mtop5" style="display: none;">
                  La superficie total estimada debe ser igual a la superficie total a habilitar.
             </div>
            <div class="pull-right mtop10">
                        <span class="btn btn-default " onclick="showfrmAgregarConformacionLocal();">
                         <i class="imoon imoon-plus"></i>
                            <span class="text">Ingresar Detalle</span>
                        </span>
             </div>
         </asp:Panel>
        </ContentTemplate>
       </asp:UpdatePanel>
        <%--Botones de Guardado--%>
              <br /><br /><br /><br /><br />
        <asp:UpdatePanel ID="updBotonesGuardar" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <div class="form-inline text-right" style="">
                     
                    <div id="pnlBotonesGuardar" class="form-group">

                      <asp:LinkButton ID="btnContinuar" runat="server" CssClass="btn btn-primary" OnClick="btnContinuar_Click" OnClientClick="return validarGuardar();" >
                                <i class="imoon imoon-disk"></i>
                                <span class="text">Guardar y Continuar</span>
                        </asp:LinkButton>

                    </div>
                    <div class="form-group">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server" DisplayAfter="200" AssociatedUpdatePanelID="updBotonesGuardar">
                            <ProgressTemplate>
                                <img src='<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>' style="margin-left: 10px" alt="loading" />Guardando...
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
     
       
    <%-- Modal Agregar Ubicación --%>
    <div id="frmAgregarConformacionLocal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" style="margin-top:-8px">Agregar detalle de conformación de local</h4>
                </div>
                <div class="modal-body">
                    <uc1:ConformacionLocalDatos runat="server" ID="ucConformacionLocalDatos" />
                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->
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

  <div id="frmSuperficiesNoIguales" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top:-8px">Error</h4>
                </div>
                <div class="modal-body">
                    <table >
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                             <i class="imoon imoon-remove-circle fs64 mleft10"  style="color:#344882; margin-top:-10px"></i>
                            </td>
                              <td style="vertical-align:middle; padding-left:10px">
                                <asp:label ID="Label2" runat="server">La superficie total estimada debe ser igual a la superficie total a habilitar.</asp:label>
                             </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="form-group">
                                    <ContentTemplate>
                                        <asp:Label ID="Label1" runat="server" class="pad10"></asp:Label>
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

    <script type="text/javascript">
        var vSeparadorDecimal;
        var prev_val;
        $(document).ready(function () {
            $("#page_content").hide();
            $("#Loading").show();
            $("#<%: btnCargarDatos.ClientID %>").click();
            validarSuperficie();
        });

        function finalizarCarga() {
            $("#Loading").hide();
            $("#page_content").show();
            validarSuperficie();
            return false;
        }
    
        function showfrmAgregarConformacionLocal() {
            $("#frmAgregarConformacionLocal").modal({
                "show": true,
                "backdrop": "static",
                "keyboard": false
            });
            return false;
        }
        function validarSuperficie() {

            var cantGrilla = $("[id*='grdConformacionLocal']").length;

            if (cantGrilla > 1) {
                $("#<%: pnlSuperficie.ClientID %>").show();
                $("#<%: reqSupIguales.ClientID %>").show();
                return true;
            }
            else {
                $("#<%: reqSupIguales.ClientID %>").hide();
                $("#<%: pnlSuperficie.ClientID %>").hide();
                return true;
            }
        }
        function ocultarBotonesGuardado() {

            $("#pnlBotonesGuardar").hide();

            return true;
        }
        function hidefrmAgregarConformacionLocal() {
            $("#frmAgregarConformacionLocal").modal("hide");
            return false;
        }

        function validarGuardar() {

            if ($("#<%: txtSupTotal.ClientID %>").val() == $("#<%: txtSupTotalLocal.ClientID %>").val()) {
                ocultarBotonesGuardado();
                return true;
            }
            else {
                $("#frmSuperficiesNoIguales").modal("show");
                return false;
            }
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
        function showfrmError() {
            $("#frmError").modal("show");
            return false;
        }
   </script>
 </asp:Content>
