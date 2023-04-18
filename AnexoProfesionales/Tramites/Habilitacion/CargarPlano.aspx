<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="CargarPlano.aspx.cs" Inherits="AnexoProfesionales.CargarPlano" %>

<%@ Register Src="~/Tramites/Habilitacion/Controls/Titulo.ascx" TagPrefix="uc1" TagName="Titulo" %>



<asp:Content ID="Content3" Title="Carga de Planos" ContentPlaceHolderID="MainContent" runat="server">

     <%: Scripts.Render("~/bundles/fileUploadCss") %>
      <%: Scripts.Render("~/bundles/fileUpload") %>
    
    <%: Scripts.Render("~/bundles/autoNumeric") %>
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


   <div id="page_content" Style="display:none">
       <uc1:Titulo runat="server" id="Titulo" />
        <hr />
        
        <asp:HiddenField ID="hid_DecimalSeparator" runat="server" />
        <asp:HiddenField ID="hid_return_url" runat="server" />
        <asp:Panel ID="pnlMsgPlanoContraIncendios" runat="server" CssClass="alert alert-success mtop10" Visible="false" Width="100%">
                    <asp:Label ID="lblMsgPlanoContraIncendios" Text="El trámite XXXXXX requiere la presentación de Plano Conforme a Obra de Instalación de Prevención contra Incendio registrado por la DGROC o Plano de Instalación de Prevención contra Incendio registrado por la DGROC, correspondiendo para este último una verificación in situ conforme lo establecido en la normativa vigente." ForeColor="Red" runat="server"></asp:Label>
                </asp:Panel>

        <p style="margin: auto; padding: 10px; line-height: 20px">
            En este paso deberá ingresar el/los plano/s del trámite.
        </p>
       <br />
        <%--Box de tipo de planos--%>
        <div class="box-panel">
        <div style="margin:20px; margin-top:-5px"> 
                    <div style="margin-top:5px; color:#377bb5">                                 
                        <h4><i class="imoon imoon-file2" style="margin-right:10px"></i>Planos Cargados</h4>   
                        <hr />                    
                    </div>
            </div>
             <asp:Panel ID="pnlContenido" runat="server">


                    <asp:HiddenField ID="hid_id_encomienda" runat="server" />

                    <asp:UpdatePanel ID="updPnlCargarPlano" runat="server" >
                    <ContentTemplate>

                        <asp:HiddenField ID="hid_tamanio" runat="server" />
                        <asp:HiddenField ID="hid_tamanio_max" runat="server" />
                        <asp:HiddenField ID="hid_requierre_detalle" runat="server" />
                        <asp:HiddenField ID="hid_extension" runat="server" />
                        <script type="text/javascript">
                            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandler);
                            function endRequestHandler() {
                                init_fileUpload();
                            }
                        </script>  

                        <div style="padding:10px">
                          
                                <div class="col-sm-8">
                                    <label><b>Tipo de Plano:</b></label>
                                    <div class="controls">
                                        <asp:DropDownList ID="TipoDropDown" runat="server" Width="400px" height="30px" AutoPostBack="True"
                                            CssClass="form-control" OnSelectedIndexChanged="TipoDropDown_SelectedIndexChanged" >
                                        </asp:DropDownList>
                                </div>
                                </div>
                                <div class="col-sm-8 mtop20">
                                    <asp:Label ID="lblDetalle" runat="server"><b>Detalle Plano:</b></asp:Label>
                                      <div class="controls">
                                        <asp:TextBox ID="txtDetalle" runat="server" MaxLength="50" Width="400px" CssClass="form-control"></asp:TextBox>
                                          </div> 
                                     <br />
                                 </div>
                                <div class="controls mtop20">
                                        <span class="btn btn-primary fileinput-button">
                                            <i class="imoon imoon-white imoon-file-text"></i>
                                            <span>Cargar Plano</span>
                                            <input id="fileupload" type="file" name="files[]" multiple></input>
                                            <label id="val_upfile_txtDetalle" class="error-label" style="display:none">Debe ingresar el detalle.</label>
                                         </span>

                                       </div>

                            <asp:HiddenField ID="hid_filename_plano_random" runat="server" />
                            <asp:HiddenField ID="hid_filename_plano" runat="server" />

                            <asp:Button ID="btnCargarPlano" runat="server" Text="Cambiar" CssClass="btn btn-default" OnClick="btnCargarPlano_Click" Style="display: none" OnClientClick="ocultarBoton()" />
                        </div>

                        <!-- The global progress bar -->

                                 <div id="progress" class="progress mtop5" Style="display: none" >
                                    <div class="progress-bar progress-bar-success">
                                        <span id="progressvaluePT" class="sr-only"></span>
                                    </div>
                                </div>
  

                    </ContentTemplate>
                    </asp:UpdatePanel>
 
                    <%--Grilla de planos--%>
                    <asp:UpdatePanel ID="updPnlGrillaPlanos" runat="server" >
                    <ContentTemplate>

                        <div style="padding-left:20px; width:1100px;" >
                            <asp:GridView ID="grdPlanos" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered mtop5" ItemType="DataTransferObject.EncomiendaPlanosDTO"
                                DataKeyNames="id_encomienda_plano, id_encomienda" AllowPaging="false" Style="border: none;" GridLines="None" Width="1100px"
                                CellPadding="10">
                                <HeaderStyle CssClass="grid-header"  /> 
  
                                <Columns>

                                    <asp:BoundField DataField="DetalleExtra" HeaderText="Tipo de Plano" ItemStyle-Width="80px" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                                    <asp:BoundField DataField="detalle" HeaderText="Detalle del Plano" ItemStyle-Width="80px" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />

                                    <asp:TemplateField ItemStyle-Height="24px" ItemStyle-Width="80px" ItemStyle-CssClass="text-center" HeaderText="Descargar" HeaderStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkDescargarPlano" runat="server" Target="_blank" NavigateUrl='<%# Eval("url") %>'>
                                                <i class="imoon-download-alt"></i>
                                                <span class="text"><%# Eval("nombre_archivo")%></span>
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="CreateDate" HeaderText="Subido el "  ItemStyle-Width="80px" HeaderStyle-CssClass="text-center"
                                            HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="text-center"  DataFormatString="{0:dd/MM/yyyy}" />

                                    

                                    <asp:TemplateField ItemStyle-Height="24px" ItemStyle-Width="80px" HeaderText="Eliminar" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEliminar" runat="server" 
                                                    CommandArgument='<%# Eval("id_encomienda_plano") %>' 
                                                    Width="85px" OnClick="lnkEliminar_Command">
                                                <i class="imoon imoon-trash"></i> 
                                                <span class="text">Eliminar</span></a>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="mtop10">
                                         <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                                        No hay planos aún...
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                        <div>
                            <asp:Label id="alertPlanoIncendio" runat="server" CssClass="label-warning" style="margin-left:10px;margin-top:20px; display:none" >Se debe cargar el Plano Contra Incendios.</asp:Label>
                        </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
           
              
                <div style=" margin-top:20px; padding:10px";>
  
                     <p><b> Consideración importante en el plano de habilitación:</b></p>
                        <ul>
                            <li style="padding-left:40px;">
                                 Tiene que estar en formato "DWF" de AutoCAD
                            </li>
                        </ul>
                        <ul>
                            <li style="padding-left:40px;">
                                 Tiene que estar en 2D
                            </li>
                        </ul>
                        <ul>
                            <li style="padding-left:40px;">
                                 Tiene que tene fondo blanco
                            </li>
                        </ul>
                        <ul>
                            <li style="padding-left:40px;">
                                Tiene que tener lineas negras con espesor reglamentario
                            </li>
                        </ul>
                 
                </div>
                <div style=" margin-top:20px; padding:10px">
                   <p><b>Consideración importante en un plano que no es el de habilitación:</b></p>
                        <ul>
                            <li style="padding-left:40px;">
                                 Tiene que estar en formato "JPG"
                            </li>
                        </ul>
                        <ul>
                            <li style="padding-left:40px;">
                                 Tiene que estar escaneado en una sola pasada (no puede estar en partes)
                            </li>
                        </ul>
                        <ul>
                            <li style="padding-left:40px;">
                                El plano tiene que estar certificado ante escribano publico
                            </li>
                        </ul>
             
                </div>
            
                  </div>

        <%--Botones de Guardado--%>
        <asp:UpdatePanel ID="updBotonesGuardar" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <div class="form-inline text-right mtop20">
                    <div id="pnlBotonesGuardar" class="form-group">

                      <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary" OnClick="btnContinuar_Click" OnClientClick="return validarGuardar();">
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
        
        <%--modal de Errores--%>
    <div id="frmError" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top:-8px">Atención</h4>
                </div>
                <div class="modal-body">
                 <asp:UpdatePanel ID="udpError" runat="server">
                   <ContentTemplate>
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon-info fs64" style="color: #377bb5"></i>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="updmpeInfo" runat="server" class="form-group">
                                    <ContentTemplate>
                                        <asp:Label ID="lblError" runat="server" Style="color: Black"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
               </asp:UpdatePanel> 
                </div>
                <div class="modal-footer mleft20 mright20" >
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>


 <script type="text/javascript">


     //if ($("#" + id + ":visible").length > 0) {
     //    ValidatorEnable(objval, true);
     //    $(objval).hide();
     //}

     $(document).ready(function () {

         $("#page_content").hide();
         $("#Loading").show();
         finalizarCarga();
     });
     function finalizarCarga() {
         $("#Loading").hide();
         $("#page_content").show();

         return false;
     }
     function confirmar_eliminar() {
         return confirm('¿Esta seguro que desea eliminar este Registro?');
     }
     function showfrmError() {
         $("#frmError").modal("show");
         $("#pnlProcesando").hide();
         $("#pnlShortcuts").show();
         return false;
     }

     function tda_mostrar_mensaje(texto, titulo) {

         if (titulo == "") {
             titulo = "Documentos Adjuntos";
         }

         $.gritter.add({
             title: titulo,
             text: texto,
             image: '<%: ResolveUrl("~/Content/img/info32.png") %>',
             sticky: false
         });

     }

     function init_fileUpload() {
         'use strict';
         // https://github.com/blueimp/jQuery-File-Upload/wiki/Options
         var nrorandom = Math.floor(Math.random() * 1000000);
         $("#<%: hid_filename_plano_random.ClientID %>").val(nrorandom);

         var url = '<%: ResolveUrl("~/Scripts/jquery-fileupload/Upload.ashx?nrorandom=") %>' + nrorandom.toString();
         $("[id*='fileupload']").fileupload({
             url: url,
             dataType: 'json',
             formData: { folder: 'c:\Temporal' },
             //acceptFileTypes: /(\.|\/)(dwf|dwf|)$/i,
             add: function (e, data) {
                 var goUpload = true;
                 var uploadFile = data.files[0];

                 if (!validar_input(uploadFile)) {
                     goUpload = false;
                 }

                 if (goUpload == true) {
                     $("[id*='progress']").show();
                     data.submit();
                 }
             },
             done: function (e, data) {
                 //var filename = nrorandom.toString() + "." + fileObj.name;
                 $("#<%: hid_filename_plano.ClientID %>").val(data.files[0].name);
                $("#<%: btnCargarPlano.ClientID %>").click();

            },
            progressall: function (e, data) {
                var porc = parseInt(data.loaded / data.total * 100, 10);
                $('#progress .progress-bar').css(
                    'width',
                    porc + '%'
                );

                if (porc >= 100) {
                    $('#progress .progress-bar').text('Cargando archivo...espere');
                }
                else {
                    $('#progress .progress-bar').text(porc.toString() + '%');
                }
            },
            fail: function (e, data) {

                alert(data.files[0].error);
            }

        }).prop('disabled', !$.support.fileInput).parent().addClass($.support.fileInput ? undefined : 'disabled');
     }


     function progress(value) {

         $("#progress .bar").css(
             'width',
             value + '%'
         );
     }

     function validar_input(uploadFile) {
         if ($('#<%=hid_requierre_detalle.ClientID%>').val() == 'True') {
                if ($('#<%=txtDetalle.ClientID%>').val() == null ||
                    $('#<%=txtDetalle.ClientID%>').val().trim() == '') {
                    alert('El Detalle es requerido.');
                    return false;
                }
            }

            if ($('#<%=hid_extension.ClientID%>').val() == 'dwf') {
                if (!(/\.(dwf|dwf)$/i).test(uploadFile.name)) {
                    alert('Solo se permiten archivos de tipo plano (*.dwf)');
                    return false;
                }
            } else if ($('#<%=hid_extension.ClientID%>').val() == 'jpg') {
                if (!(/\.(jpg|jpg)$/i).test(uploadFile.name)) {
                    alert('Solo se permiten archivos de tipo plano (*.jpg)');
                    return false;
                }
            } else {
                alert('El tipo ' + $('#<%=hid_extension.ClientID%>').val() + ' no esta soportado');
                return false;
            }

            var p = parseInt($('#<%=hid_tamanio_max.ClientID%>').val(), 10);
            if (uploadFile.size > p) { // 2mb
                alert('El tamaño máximo permitido es de ' + $('#<%=hid_tamanio_max.ClientID%>').val() + ' MB');
             return false;
         }

         return true;
     }

     function finalizarCarga222() {

         $("#<%: txtDetalle.ClientID %>").prop("value", "");

         return false;

     }
     function ocultarBoton() {

         $("#<%: btnCargarPlano.ClientID %>").css("display", "none");

         return false;

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
