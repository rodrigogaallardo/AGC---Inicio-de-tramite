<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="CargarPlano.aspx.cs" Inherits=" SSIT.Solicitud.Consulta_Padron.CargarPlano" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <%: Scripts.Render("~/bundles/fileUploadCss") %>
    <%: Scripts.Render("~/bundles/fileUpload") %>    
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
                <td style="font-size: 24px">Cargando tr&aacute;mites
                </td>
            </tr>
        </table>
    </div>

    <asp:UpdatePanel ID="updCargarDatos" runat="server">
        <ContentTemplate>          
            <asp:Button ID="btnCargarDatos" runat="server" OnClick="btnCargarDatos_Click" Style="display: none" />            
        </ContentTemplate>
    </asp:UpdatePanel>

   <div id="page_content" Style="display:none">
        <h2>Carga de Documentos </h2>
        <hr />
        
        <asp:HiddenField ID="hid_DecimalSeparator" runat="server" />
        <asp:HiddenField ID="hid_return_url" runat="server" />

        <p style="margin: auto; padding: 10px; line-height: 20px">
            En este paso deberá ingresar el/los documentos/s del trámite
        </p>
       <br />
        <%--Box de tipo de planos--%>
        <div class="box-panel">
            <div style="margin:10px; margin-top:-5px"> 
                    <div style="margin-top:5px; color:#377bb5">                                 
                        <h4><i class="imoon imoon-file2" style="margin-right:10px"></i>Documentos Cargados</h4>   
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

                        <div class="col-m-8">                                                          
                            <div class="row" >

                                <label style="margin-left:20px"><b>Presiona el botón "Seleccionar Archivo" y elegí el archivo que deseas agregar.</b></label>
                                <br /> <br /> <br />
                                <div class="controls col-sm-8">
                                    <asp:DropDownList ID="TipoDropDown" runat="server" Width="400px" height="30px" CssClass="form-control" >
                                    </asp:DropDownList>
                                </div>
                              
                                <div class="controls pull-right col-sm-4">
                                    <span class="btn btn-default fileinput-button">
                                        <i class="imoon imoon-white imoon-file-text"></i>
                                        <span>Seleccionar Archivo</span>
                                        <input id="fileupload" type="file" name="files[]" multiple></input>                                            
                                    </span>
                                </div>
                            </div>  
                              <br /> <br />                                  
                            <asp:HiddenField ID="hid_filename_documento" runat="server" />
                              <div id="pnlFiles" class="ptop20 hide">
                                  <asp:UpdatePanel ID="updFiles" runat="server">
                                        <ContentTemplate>
                                            <div id="files" class="files"></div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                  
                                <asp:Button ID="btnCargarPlano" runat="server" Text="Cambiar" CssClass="btn btn-inverse" OnClick="btnCargarPlano_Click" Style="display: none" OnClientClick="ocultarBoton()" />
                        
                                <!-- The global progress bar -->
                                <div id="progress" class="progress mtop5" style="display:none">
                                    <div class="bar bar-success"></div>
                                </div>
                             </div>
                        </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                   
                    <%--Grilla de planos--%>
                    <asp:UpdatePanel ID="updPnlGrillaPlanos" runat="server" >
                    <ContentTemplate>

                        <div style="padding-left:20px;" class="box-panel">
                            <asp:GridView ID="grdPlanos" 
                                runat="server" 
                                AutoGenerateColumns="false"
                                DataKeyNames="Id, IdConsultaPadron" 
                                AllowPaging="false" 
                                Style="border: none;" 
                                GridLines="None" 
                                Width="100%" 
                                CellPadding="3">
                                <HeaderStyle CssClass="grid-header"  /> 
                                <AlternatingRowStyle BackColor="#efefef" />

                                <Columns>

                                    <asp:BoundField DataField="NombreArchivo" HeaderText="Nombre del Archivo" />

                                    <asp:TemplateField HeaderText="Descargar">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkDescargarPlano" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" runat="server" Target="_blank" NavigateUrl='<%#"~/Download.ashx?Id=" + Eval("IdFile") %>'>
                                                <i class="imoon-download-alt fs24"></i>
                                           
                                            </asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="CreateDate" HeaderText="Subido el "  
                                            ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"  DataFormatString="{0:dd/MM/yyyy}" />

                                    <asp:TemplateField HeaderText="Eliminar" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEliminar" runat="server" 
                                                    CommandArgument='<%# Eval("Id") %>' 
                                                     OnClick="lnkEliminar_Command">
                                                <i class="imoon imoon-trash fs24"></i> 
                                                </a>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="mtop10">
                                         <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                                        No hay documentos aún...
                                    </div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>                        
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>

            
        </div>

        <%--Botones de Guardado--%>
        <asp:UpdatePanel ID="updBotonesGuardar" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <div class="form-inline text-right mtop20">
                    <div id="pnlBotonesGuardar" class="form-group">

                      <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-lg btn-primary" OnClick="btnContinuar_Click" OnClientClick="return validarGuardar();">
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


 <script type="text/javascript">

     uploadButton = $('<button/>')
              .addClass('btn btn-primary uploadbutton')
              .text('Subir Documento')
              .on('click', function () {
                    
                    $("[id*='progress']").show();

                    var $this = $(this),
                    data = $this.data();

                    data.submit().always(function (data, e) {

                        if (data.files != undefined) {
                            $("#<%: hid_filename_documento.ClientID %>").val(data.files[0].Name);                            
                            $("#<%: btnCargarPlano.ClientID %>").click();
                        }
                    });
                });

                cancelUploadButton = $('<button/>')
                .addClass('btn btn-danger mleft5 cancelbutton')
                .text('Cancelar')
                .on('click', function () {
                    $("#files table").remove();
                    $("#pnlFiles").hide();
                    return false;
                });


     $(document).ready(function () {

         $("#page_content").hide();
         $("#Loading").show();
         $("#<%= btnCargarDatos.ClientID %>").click();

       });
     

     function finalizarCarga() {

         $("#Loading").hide();

         $("#page_content").show();
         init_fileUpload();
         return false;
     }
 
     function showfrmError() {         
         $("#frmError").modal("show");
         return false;

     }

     function init_fileUpload() {
        'use strict';
        // https://github.com/blueimp/jQuery-File-Upload/wiki/Options
        var nrorandom = Math.floor(Math.random() * 1000000);        
        $("#<%: hid_filename_documento.ClientID %>").val(nrorandom);
        var url = '<%: ResolveUrl("~/Scripts/jquery-fileupload/Upload.ashx?nrorandom=") %>' + nrorandom.toString();
        $("[id*='fileupload']").fileupload({
            url: url,
            dataType: 'json',
            formData: { folder: 'c:\Temporal' },          
            add: function (e, data) {

                var goUpload = true;
                var uploadFile = data.files[0];
                
                if (!(/\.(pdf)$/i).test(uploadFile.name)) {
                    alert('Solo se permiten archivos de tipo .pdf');
                    goUpload = false;
                }

                if (!validar_input(uploadFile)) {
                    goUpload = false;
                }

                //genera el html con los datos del archivo y los botones para subirlo.
                $("#files table").remove();
                data.context = $('<div/>').appendTo('#files');
                $.each(data.files, function (index, file) {
                    var node = $('<table class="file-input-tabledata table"/>').append('<tr/>');
                    $(node).find("tr:last").append('<td class="col1"/>');

                    $(node).find('tr:last td.col1').append('<span class="btnpdf20x20"/>');
                    $(node).find('tr:last td.col1').append('<span class="text"/>');
                    $(node).find('tr:last td.col1 span.text').text(file.name);

                    $(node).find("tr:last").append('<td class="col2"/>');
                    $(node).find('tr:last td.col2').append('<p/>');
                    $(node).find('tr:last td.col2 p').text(parseInt(file.size / 1024).toString() + " kb.");

                    $(node).find("tr:last").append('<td class="col3"/>');
                    $(node).find('tr:last td.col3').append(uploadButton.clone(true).data(data))
                                                    .append(cancelUploadButton.clone(true).data(data));

                    node.appendTo(data.context);
                });

                if (goUpload == true) {
                    $("[id*='progress']").show();
                    $("#pnlFiles").removeClass("hide");
                    $("#pnlFiles").show();                    
                }
            },
            done: function (e, data) {
                
                $("#<%: hid_filename_documento.ClientID %>").val(data.files[0].name);                
                $("#files .uploadbutton").hide();
                $("#files .cancelbutton").hide();

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

        function validar_input(uploadFile) {

            var p = parseInt($('#<%=hid_tamanio_max.ClientID%>').val(), 10);
            if (uploadFile.size > p) { // 2mb
                alert('El tamaño máximo permitido es de ' + $('#<%=hid_tamanio_max.ClientID%>').val() + ' MB');
                return false;
            }
        
            return true;
        }

       
     function ocultarBoton() {

         $("#<%: btnCargarPlano.ClientID %>").css("display","none");

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
