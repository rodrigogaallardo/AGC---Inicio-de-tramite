<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Presentacion.ascx.cs" Inherits="SSIT.Solicitud.Habilitacion.Controls.Presentacion" %>

<%: Scripts.Render("~/bundles/fileupload") %>
<%: Styles.Render("~/bundles/fileuploadCss") %>

<div id="box_titulares" class="accordion-group widget-box"   style="background-color:#ffffff">
    <div class="accordion-heading">
        <a id="titulares_btnUpDown" data-parent="#collapse-group" href="#collapse_historial"
            data-toggle="collapse"  onclick="bt_btnUpDown_collapse_click(this)">

            <div class="widget-title">
                <span class="icon"><i class="imoon-busy" Style="color:#344882"></i></span>
                <h5>
                    <asp:Label ID="lbl_titulares_tituloControl" runat="server" Text="Historial"></asp:Label></h5>
                <span class="btn-right"><i class="imoon imoon-chevron-up" Style="color:#344882"></i></span>
            </div>
        </a>
    </div>
  <div class="accordion-body collapse" id="collapse_historial">
   <div style="margin:10px">
       <asp:UpdatePanel ID="updHistorial" runat="server" UpdateMode="Conditional">
           <ContentTemplate>
               <asp:Panel ID="pnlHistorial" runat="server" >
                   <div style="padding: 0px 10px 10px 10px; width: auto">
                       <div class="mtop10">
                           <strong>Historial de estados del Tramite</strong>
                       </div>
                       <asp:GridView ID="gridHistorial_db" runat="server" AutoGenerateColumns="false"
                           AllowPaging="false" Style="border: none;" CssClass="table table-bordered mtop5"
                           GridLines="None" Width="100%">
                           <HeaderStyle CssClass="grid-header" />
                           <RowStyle CssClass="grid-row" />
                           <AlternatingRowStyle BackColor="#efefef" />
                           <Columns>
                               <asp:BoundField DataField="fecha" HeaderText="Fecha" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="350px" />
                               <asp:BoundField DataField="Estado" HeaderText="Estado del Tramite" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="350px" />
  <%--                          <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" HeaderText="Observaciones">
                            <ItemTemplate>
                               <asp:Panel ID="pnlVisualizarObservaciones" runat="server"  Visible='<%# (Convert.ToInt32(Eval("IdEstado")) == (int)StaticClass.Constantes.TipoEstadoSolicitudEnum.OBSERVADO) %>'>
                                <asp:LinkButton ID="lnkRubro" CssClass="link-local" runat="server" title="Ver Observaciones" OnClientClick="return showBox_observacion();">
                                    <span class="icon"><i class="imoon-eye color-blue fs24"></i></span>
                                </asp:LinkButton>
                               </asp:Panel>
                            </ItemTemplate>
                            </asp:TemplateField>--%>
                           </Columns>
                        <EmptyDataTemplate>
                            <div class="mtop10">
                                <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                                No se hicieron cambios de estados.
                            </div>
                        </EmptyDataTemplate>
                       </asp:GridView>
                    
                   </div>
               </asp:Panel>
           </ContentTemplate>
       </asp:UpdatePanel>
   </div>
 </div> 

</div>

<%--Observaciones--%>
  <div id="box_observacion" runat="server" class="accordion-group widget-box" style="background-color:#ffffff">
    <div class="accordion-heading">
        <a id="A1" data-parent="#collapse-group" href="#collapse_observaciones"
            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

            <div class="widget-title">
                <span class="icon"><i class="imoon-file" Style="color:#344882"></i></span>
                <h5>
                    <asp:Label ID="Label1" runat="server" Text="Observaciones"></asp:Label></h5>
                <span class="btn-right"><i class="imoon imoon-chevron-up" Style="color:#344882"></i></span>
            </div>
        </a>
    </div>
  <div class="accordion-body collapse in" id="collapse_observaciones">
            <asp:HiddenField ID="hid_filename_documento" runat="server" />
            <asp:UpdatePanel ID="updPnlObservaciones" runat="server" >
                <ContentTemplate>
                    <br />
                    <asp:GridView ID="datlstObservaciones" runat="server" AutoGenerateColumns="false"
                        GridLines="None" DataKeyNames="id_ObsGrupo" OnRowDataBound="datlstObservaciones_RowDataBound" 
                        >
                        
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="15px">
                            <ItemTemplate>
                                    
                                <%--Detalle de observaciones--%>
                                <div id="box_detalle_observacion" class="accordion-group widget-box"  style="width:1135px;margin-top:-15px; margin-left:15px">
      
                                    <div class="accordion-heading">
                                        <a id="<%# Eval("id_ObsGrupo").ToString() + Eval("id_ObsGrupo").ToString() %>" data-parent="#collapse-group"  href="<%# '#'+ Eval("id_ObsGrupo").ToString() %>" data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">
                                            <div class="widget-title" Style="background:#ffffff;background-image: none;">
                                                <span class="icon"><i class="imoon imoon-list-alt" Style="color:#337ab7"></i></span>
                                                <h5>
                                                    <label> <%# Convert.ToDateTime(Eval("CreateDate")).ToString("dd/MM/yyyy HH:mm") %></label>
                                                    <label> - Calificador: </label>
                                                    <label><%# Eval("userApeNom") %></label>
                                                </h5>
                                                <span class="btn-right"><i class="imoon imoon-chevron-up" Style="color:#344882"></i></span>
                                            </div>
                                        </a>
                                    </div>
                                    <div ID="<%# Eval("id_ObsGrupo") %>" class="accordion-body collapse" >
                          
                                        <div class="widget-content" style="border-bottom:none">

                                            <%--Grilla de observaciones--%>
                                            <asp:UpdatePanel ID="updgrdDetalleObs" runat="server">
                                                <ContentTemplate>

                                                    <asp:GridView ID="grdDetalleObs" runat="server" CssClass="table table-bordered table-striped table-hover"
                                                        AutoGenerateColumns="false" OnRowDataBound="grdDetalleObs_RowDataBound" DataKeyNames="id_ObsDocs" ShowHeader="false" >
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Documento" ItemStyle-BackColor="#FFFFFF" >
                                                                <ItemTemplate>


                                                                    <asp:HiddenField ID="hid_cod_tipodocsis" runat="server" Value='<%#Eval("cod_tipodocsis")%>' />
                                                                    <asp:HiddenField ID="id_ObsDocs" runat="server" Value='<%#Eval("id_ObsDocs")%>' />
                                                                    <asp:HiddenField ID="hid_id_file" runat="server" Value='<%#Eval("id_file")%>' />
                                                                    <asp:HiddenField ID="hid_id_certificado" runat="server" Value='<%#Eval("id_certificado")%>' />
                                                                    <asp:HiddenField ID="hid_actual" runat="server" Value='<%#Eval("actual")%>' />

                                                                    <div class="form-horizontal" style="background:#FFFFFF">
                                                                        <div class="form-group mbottom0">
                                                                            <label class="control-label col-sm-3" style="width: 180px">Documento requerido:</label>
                                                                            <div class="col-sm-9 ptop9 pleft0">
                                                                                <label class="label  label-default" style="font-weight: normal; font-size: 100%"><%# Eval("nombre_tdocreq")%></label>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group mbottom0">
                                                                            <label class="control-label col-sm-3" style="width: 180px">Observaci&oacute;n:</label>
                                                                            <div class="col-sm-9 ptop8 pleft0">
                                                                                <%# Eval("Observacion_ObsDocs")%>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group mbottom0">
                                                                            <label class="control-label col-sm-3" style="width: 180px">Respaldo Normativo:</label>
                                                                            <div class="col-sm-9 ptop8 pleft0">
                                                                                <%# Eval("Respaldo_ObsDocs")%>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group mbottom0">
                                                                            <label class="control-label col-sm-3" style="width: 180px">Decide NO subirlo:</label>
                                                                            <div class="col-sm-9 ptop5 pleft0">
                                                                                <asp:CheckBox ID="chk_Decido_no_subir" runat="server" Checked='<%#Eval("Decido_no_subir")%>' OnCheckedChanged="chk_Decido_no_subir_CheckedChanged" AutoPostBack="true" />
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group mbottom0">
                                                                            <label class="control-label col-sm-3" style="width: 180px">Documento:</label>
                                                                            <div class="col-sm-9 pleft0">

                                                                                <asp:UpdatePanel ID="updFilesObservaciones" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <asp:Panel ID="pnlFilesObservaciones" runat="server">
                                                                                            <asp:LinkButton ID="btnSeleccionarFilesObservaciones" runat="server" CssClass="btn btn-sm btn-default fileinput-button">
                                                                                                <i class="imoon imoon-folder-open" Style="color:#337ab7"></i>
                                                                                                <span class="text">Seleccionar archivo</span>
                                                                                                <input id="fileupload_observaciones_<%#Eval("id_ObsDocs")%>" type="file" name="files[]" accept=".dwf,application/pdf">
                                                                                            </asp:LinkButton>

                                                                                            <asp:Button ID="btnSubirDocumentoObservaciones" runat="server" Text="Subir Documento" CssClass="btn btn-inverse" OnClick="btnSubirDocumentoObservaciones_Click" Style="display: none" />
                                                                                            <asp:HiddenField ID="hid_filename_observaciones_numerico" runat="server" />
                                                                                            <asp:HiddenField ID="hid_filename_observaciones_original" runat="server" />
                                                                                            <!-- The global progress bar -->
                                                                                            <div id="progress_observaciones" class="progress mtop5" style="display: none">
                                                                                                <div class="progress-bar progress-bar-success">
                                                                                                    <span id="progressvalue" class="sr-only"></span>
                                                                                                </div>
                                                                                            </div>

                                                                                                            

                                                                                        </asp:Panel>
                                                                                                        
                                                                                        <asp:DropDownList ID="ddlArchivo" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlArchivo_SelectedIndexChanged" Width="300px" Visible="false" AutoPostBack="true"></asp:DropDownList>

                                                                                        <asp:Panel ID="pnlFilesObservaciones_SinArchivo" CssClass="mtop10" runat="server" Visible="false">
                                                                                            <i class="imoon imoon-cancel-circle" Style="color:#337ab7"></i>
                                                                                            <span>No disponible</span>
                                                                                        </asp:Panel>

                                                                                        <asp:Panel ID="pnlFilesObservaciones_ConArchivo" CssClass="mtop7" runat="server" Visible="false">
                                                                                                            
                                                                                            <div class="form-inline">
                                                                                                <i class="imoon imoon-download color-blue"></i>
                                                                                                <asp:HyperLink ID="lnkPDFObservaciones" runat="server" Target="_blank" Text='<% #Eval("Filename") %>'></asp:HyperLink>

                                                                                                <asp:LinkButton ID="btn_eliminar_files_observaciones" runat="server" OnClick="btn_eliminar_files_observaciones_Click" title="Eliminar archivo" data-toggle="tooltip">
                                                                                                    <i class="imoon imoon-remove color-dark mleft5" ></i>
                                                                                                </asp:LinkButton>
                                                                                            </div>
                                                                                                            
                                                                                        </asp:Panel>
                                                                                       
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                            <br />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                                   
                                                        </Columns>
                                                        
                                                        <EmptyDataTemplate>
                                                            <asp:Panel ID="pnlNotDataFound" runat="server" CssClass="GrayText-1">
                                                                <h3>No se encontraron registros.</h3>
                                                            </asp:Panel>
                                                        </EmptyDataTemplate>
                                                        
                                                    </asp:GridView>
                                                    
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                 
                                        </div>
                               
                                     </div>               
                                </div>
                                   <br />  
                            </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>

                    <asp:GridView ID="gridObservaciones" runat="server" style="width:1125px"
                        CssClass="table table-bordered table-striped table-hover mleft20 mright20"
                        AutoGenerateColumns="false" DataKeyNames="id_solobs"
                        OnRowDataBound="gridObservaciones_OnRowDataBound">
                        <Columns>
                            <asp:BoundField DataField="CreateDate" HeaderText="Fecha" ItemStyle-Width="150px" DataFormatString="{0:dd/MM/yyyy HH:mm}"  HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-Center"/>
                            <asp:BoundField DataField="userApeNom" HeaderText="Calificador" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-Center"/>
                                        
                                    <asp:TemplateField ItemStyle-Width="190px" HeaderText="Leer Observaci&oacute;n" ItemStyle-CssClass="text-center link-local" HeaderStyle-CssClass="text-center">
                                        <ItemTemplate>

                                            <asp:LinkButton ID="lnkModal" runat="server" CssClass="" data-toggle="modal" data-target="codebehind">
                                                <span class="icon"><i class="imoon-eye color-blue fs24"></i></span>
                                            </asp:LinkButton>

                                            <asp:Panel ID="pnlObservacionModal" runat="server" class="modal fade in" TabIndex="-1" role="dialog" aria-hidden="true" HorizontalAlign="Left">
                                                <div class="modal-dialog">
                                                    <div class="modal-content">
                                                    <div class="modal-header" >
                                                      <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                                        <h4 class="modal-title" style="margin-top:-8px">Causales de no recepci&oacute;n</h4>
                                                        </div>

                                                        <div class="modal-body" >
                                                            <p>Solicitud: <b><%#Eval("id_solicitud")%></b></p>
                                                            <p>Fecha: <b><%#Eval("CreateDate", "{0:dd/MM/yyyy HH:mm}")%></b></p>
                                                            <p><%#Eval("Observaciones")%></p>
                                                        </div>

                                                        <div class="modal-footer">
                                                            <a href="#" class="btn btn-default" data-dismiss="modal">Cerrar</a>
                                                            <asp:Button ID="btnConfirmarObservacion" runat="server"
                                                                Text="He leído" CssClass="btn btn-primary"
                                                                OnClientClick="cerrarModalObservacion(this);"
                                                                CommandArgument='<%# Eval("id_solobs") %>'
                                                                OnCommand="btnConfirmarObservacion_Command" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Notificado" ItemStyle-Width="20px" ItemStyle-CssClass="text-center link-local" HeaderStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <span class="icon">
                                                <i class='<%# Eval("Leido").ToString() == "True" ? "imoon-ok fs24" : "" %>'></i>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    <h5 runat="server" Font-Bold="true" ID="lblSolictarTurno" CssClass="text-center" Text="Señor Contribuyente, para tomar conocimiento de las observaciones deberá solicitar turno web y concurrir a la AGC para pedir vista del expediente." Visible ="false"></h5>
                  <br /><br />
                    <div class="text-center">
                      <asp:HyperLink ID="btnSolicitarTurno" NavigateUrl="http://www.buenosaires.gob.ar/agc/habilitacionesobservadas" Text="Solicitar Turno" Width="200PX"  runat="server" CssClass=" btn btn-primary" Target="_blank" Visible ="false"></asp:HyperLink> </div>
                    <br /><br />
                    <asp:Panel ID="pnlErrorFoto" runat="server" CssClass="alert alert-error mtop10 hide">
                        <button type="button" class="close" data-dismiss="alert">&times;</button>
                        <asp:Label ID="lblErrorDocumento" runat="server">El detalle del documento es obligatorio.</asp:Label>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
          </div>


      
    </div>


<asp:Panel ID="pnlErrorPresentacion" runat="server" CssClass="modalPopup" Style="display: none"
    Width="450px" DefaultButton="btnAceptarError">
    <table cellpadding="7" style="width: 100%">
        <tr>
            <td style="text-align: center; vertical-align: text-top">
                <i class="imoon imoon-remove-circle fs64" style="color: #f00"></i>
            </td>
            <td>
                <asp:UpdatePanel ID="updmpeInfo" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblmpeInfo" runat="server" Style="color: Black"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Button ID="btnAceptarError" runat="server" CssClass="btnOK" Text="Aceptar" Width="100px"
                    OnClientClick="__doPostBack('');" />
                                            
                                        
            </td>
        </tr>
    </table>
</asp:Panel> 

<div id="frmErrorPresentacion" class="modal fade" role="dialog">
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
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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


<script type="text/javascript">

    function tooltips() {
        $("[data-toggle='tooltip']").tooltip();
        return false;
    }

    function showBox_observacion() {
        $("#box_observacion").modal("show");
        return false;
    }

    function hideBox_observacion() {
        $("#box_observacion").modal("hide");
        return false;
    }

    function init_Js_updFilesObservaciones() {
        tooltips();

        'use strict';
        var nrorandom = Math.floor(Math.random() * 1000000);
        $("#<%: hid_filename_documento.ClientID %>").val(nrorandom);
   
        var url = '<%: ResolveUrl("~/Scripts/jquery-fileupload/Upload.ashx?nrorandom=") %>' + nrorandom.toString();
        <%--var url = '<%: ResolveUrl("~/Common/Controls/Upload.ashx") %>';--%>
       
        $("[id*='fileupload_observaciones']").fileupload({
            url: url,
            dataType: 'json',
            formData: { folder: 'C:\\Temporal' },
            acceptFileTypes: /(\.|\/)(pdf)$/i | /\.(dwf|dwf)$/i,
        
            add: function (e, data) {
                var goUpload = true;
                var uploadFile = data.files[0];
                if (uploadFile.size < 1) { // 2mb
                    alert('El archivo esta vacio.');
                    goUpload = false;
                }
                /*if (uploadFile.size > 2097152) { // 2mb
                    alert('El tamaño máximo permitido para los documentos es de 2 MB');
                    goUpload = false;
                }*/

                if (goUpload == true) {
                 
                    var pnlObservaciones = $("#" + $(data.fileInput).prop("id")).parent().parent();
                    pnlObservaciones.find("#progress_observaciones").show();
                   data.submit();
                }
            },
            done: function (e, data) {

                var pnlObservaciones = $("#" + $(data.fileInput).prop("id")).parent().parent();
                var hid_filename_observaciones_numerico = pnlObservaciones.find("[id*='hid_filename_observaciones_numerico']");
                var hid_filename_observaciones_original = pnlObservaciones.find("[id*='hid_filename_observaciones_original']");
                //var nrorandom = $("#<%: hid_filename_documento.ClientID %>").val();

                hid_filename_observaciones_numerico.val(nrorandom + data.files[0].name);
                hid_filename_observaciones_original.val(data.files[0].name);
                pnlObservaciones.find("#progress_observaciones").hide();
                pnlObservaciones.find("[id*='btnSubirDocumentoObservaciones']").click();
            },

            progressall: function (e, data) {

                var pnlObservaciones = $("#" + $(data.fileInput).prop("id")).parent().parent();
                var progress_observaciones =  pnlObservaciones.find("#progress_observaciones");

                var porc = parseInt(data.loaded / data.total * 100, 10);
                progress_observaciones.css(
                        'width',
                        porc + '%'
                    );

                if (porc <= 1) {
                    progress_observaciones.text('Cargando archivo...espere');
                }
                else {
                    progress_observaciones.text(porc.toString() + '%');
                }

            },
            fail: function (e, data) {

                alert(data.files[0].error);
            }

        }).prop('disabled', !$.support.fileInput).parent().addClass($.support.fileInput ? undefined : 'disabled');
    }

    function cerrarModalObservacion(btn_confirmar) {
        var buscar_boton = $(btn_confirmar)[0].parentElement.parentElement.parentElement.parentElement;
        $(buscar_boton).modal('hide');
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

    function abrirAcordionAlDecidir(obj) {
        var href_collapse = "#" + obj;
        var href_collapse2 = "#" + obj + obj;
       
        ($(href_collapse).switchClass("accordion-body collapse", "accordion-body collapse in"));
        ($(href_collapse).css("height", ""));
        ($(href_collapse).css("display", "inline-block"));
        ($(href_collapse2).switchClass("collapsed",""));
    }

    function showfrmErrorPresentacion() {

        $("#frmErrorPresentacion").modal("show");
        return false;

    }

</script>