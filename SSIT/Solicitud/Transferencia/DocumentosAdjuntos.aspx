<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocumentosAdjuntos.aspx.cs" MasterPageFile="~/Site.Master" Inherits="SSIT.Solicitud.Transferencia.DocumentosAdjuntos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

       <div class="accordion-body collapse" id="collapse_documento">
                        <div class="widget-content">
                          
                            <asp:UpdatePanel ID="updpnlDocumentosAdjuntos" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <asp:Panel ID="pnlDocumentosGeneradosAgencia" runat="server" >

                                        <asp:Repeater ID="repeater_docsAgencia" runat="server">
                                            <HeaderTemplate>
                                                <strong>Lista de documentos generados por la Agencia</strong>
                                                <table border="0" class="table table-bordered table-hover mtop10" style="width: 100%">
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:HyperLink ID="lnkDocAdjunto" runat="server"
                                                            Target="_blank" Style="padding-right: 10px"
                                                            CssClass="btnpdf20x20" Text='<%# ( Eval("tdocreq_detalle").ToString().Length > 0 ?Eval("tdocreq_detalle"): Eval("nombre_tdocreq")) %>'
                                                            NavigateUrl='<%# "~/Reportes/ImprimirDocumentoAdjunto.aspx?id=" + Eval("id_docadjunto")%>'>
                                                        </asp:HyperLink>
                                                    </td>
                                                    <td style="width: 150px;">
                                                        <%#  ((DateTime) Eval("CreateDate")).ToString("dd/MM/yyyy HH:mm") %>
                                                    </td>
                                                  
                                                </tr>


                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>

                                        </asp:Repeater>

                                    </asp:Panel>

                                    <asp:Panel ID="pnlDocumentosAdjuntos" runat="server" >

                                        <asp:GridView ID="grdPlanos" 
                                            runat="server" 
                                            AutoGenerateColumns="false"
                                            DataKeyNames="Id, IdSolicitud" 
                                            AllowPaging="false" Style="border: none; margin-top: 10px"
                                        GridLines="None" Width="900px"
                                        CellPadding="3">
                                        <HeaderStyle CssClass="grid-header" />
                                        <AlternatingRowStyle BackColor="#efefef" />
                                        <Columns>
                                            <asp:BoundField DataField="Tipo" HeaderText="Tipo" ItemStyle-Width="200px" />

                                            <asp:TemplateField ItemStyle-Height="24px" HeaderText="Descargar">
                                                <ItemTemplate>
                                                    <a href='<%# ResolveClientUrl("~/Download.ashx?Id=") + Eval("IdFile") %>' ><%# Eval("NombreArchivo") %></a>                                                                                                           
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="CreateDate" HeaderText="Subido el " ItemStyle-Width="80px"
                                                HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:dd/MM/yyyy}" />

                                            <asp:TemplateField ItemStyle-Height="24px" ItemStyle-Width="80px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEliminar" runat="server"
                                                        CommandArgument='<%# Eval("Id") %>'
                                                        OnClientClick="javascript:return confirmar_eliminar();"
                                                        OnCommand="lnkEliminar_Command"
                                                        Width="70px">
                                                <i class="imoon imoon-trash"></i> 
                                                <span class="text">Eliminar</span></a>
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

                                        <asp:HiddenField ID="hid_id_docadjunto" runat="server" />


                                    </asp:Panel>

                                    <asp:Panel ID="pnlDocumentosAdjuntos_NoData" runat="server" Visible="false" CssClass="mbottom10 pad10 table-bordered">

                                        <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                                        <span class="mleft10">No se encontraron documentos.</span>

                                    </asp:Panel>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <%-- Panel para agregar documentos adjuntos--%>
                            <asp:UpdatePanel ID="updpnlAgregarDocumentos" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>

                                    <asp:Panel ID="pnlAgregarDocumentos" runat="server">


                                        <div class="pull-right">
                                            <asp:LinkButton ID="btnMostrarAgregadoDocumentos" runat="server" CssClass="btn btn-primary" OnClientClick="return DatosDocumentoAgregarToggle();">
                                                <i class="imoon-white imoon-chevron-down" ></i>
                                                <span class="text">Agregar Documentos</span>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="clearfix"></div>

                                        <asp:Panel ID="pnlDatosDocumento" runat="server" Style="display: none">


                                            <strong class="pleft10 mtop20">Carga de documentos</strong>
                                            
                                            <table border="0" style="border-collapse: separate; border-spacing: 5px; margin-top: 20px;" >

                                                <tr>
                                                    <td class="vtop">
                                                        <label class="fs48 imoon-number"></label>
                                                    </td>
                                                    <td>
                                                        <p>Selecciona el tipo de documento que deseas agregar </p>

                                                        <asp:DropDownList ID="ddlTiposDeDocumentosEscaneados" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="ptop20 vtop">
                                                        <label class="fs48 imoon-number2"></label>
                                                    </td>
                                                    <td class="ptop20">
                                                        <p>
                                                            Presiona el botón "Seleccionar Archivo" y elegí el archivo que deseas agregar.
                                                        </p>
                                                        <p>
                                                            <strong>Solo se admiten tipos de archivo con extensión pdf (Portable Document Format)</strong>
                                                        </p>


                                                        <span id="btnSeleccionarFiles" class="btn btn-default fileinput-button mtop20">
                                                            <i class="imoon imoon-folder-open"></i>
                                                            <span class="text">Seleccionar archivo</span>
                                                            <input id="fileupload" type="file" name="files[]" multiple accept="application/pdf">
                                                        </span>                                                                
                                                                &nbsp;<asp:HiddenField ID="hid_tamanio_max" runat="server" />                                
                                                                <asp:HiddenField ID="hid_extension" runat="server" />
                                                        <asp:Button ID="btnSubirDocumento" runat="server" Text="Subir Documento" CssClass="btn btn-inverse" OnClick="btnSubirDocumento_Click" Style="display: none" />
                                                        <asp:HiddenField ID="hid_filename_documento" runat="server" />

                                                    </td>
                                                </tr>
                                            </table>

                                               

                                                        <!-- The global progress bar -->
                                                        <div id="progress" class="progress mtop5" style="display: none">
                                                            <div class="progress-bar progress-bar-success">
                                                                <span id="progressvalue" class="sr-only"></span>
                                                            </div>
                                                        </div>

                                            <asp:Panel ID="pnlErrorFoto" runat="server" CssClass="field-validation-error" style="display:none">
                                                <button type="button" class="close" data-dismiss="alert">&times;</button>
                                                <asp:Label ID="lblErrorDocumento" runat="server">El detalle del documento es obligatorio.</asp:Label>
                                            </asp:Panel>



                                        </asp:Panel>
                                    </asp:Panel>

                                </ContentTemplate>
                            </asp:UpdatePanel>

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


                data.submit().always(function () {
                    
                    $("#<%: btnSubirDocumento.ClientID %>").click();

                });

          });

            cancelUploadButton = $('<button/>')
            .addClass('btn btn-warning mleft5 cancelbutton')
            .text('Cancelar')
            .on('click', function () {
                $("#files table").remove();              
                return false;
            });

            $(document).ready(function () {
                $("#page_content").hide();
                $("#Loading").show();
                //$("#: btnCargarDatostramite.ClientID  ").click();

                init_Js_updpnlAgregarDocumentos();

                init_fileUpload();
            });


          $(document).ready(function () {
              $("#page_content").hide();
              $("#Loading").show();
              //$("#: btnCargarDatostramite.ClientID  ").click();

                init_Js_updpnlAgregarDocumentos();

                init_fileUpload();
          });

            function DatosDocumentoAgregarToggle() {
            
                if ($("#<%: pnlDatosDocumento.ClientID %>").css("display") == "none") {
                     //$("#<%: ddlTiposDeDocumentosEscaneados.ClientID %>").select2("val", "");
                     $("#<%: pnlDatosDocumento.ClientID %>").show("slow");
                     $("#<%: btnMostrarAgregadoDocumentos.ClientID %> i").removeClass('icon-chevron-down');
                     $("#<%: btnMostrarAgregadoDocumentos.ClientID %> i").addClass('icon-chevron-up');
                 }
                 else {
                     $("#<%: pnlDatosDocumento.ClientID %>").hide("slow");
                     $("#<%: btnMostrarAgregadoDocumentos.ClientID %> i").removeClass('icon-chevron-up');
                     $("#<%: btnMostrarAgregadoDocumentos.ClientID %> i").addClass('icon-chevron-down');

                 }
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
                     //acceptFileTypes: /(\.|\/)(dwf|dwf|)$/i,
                     add: function (e, data) {
                     
                         var goUpload = true;
                         var uploadFile = data.files[0];

                         if (goUpload == true) {
                             $("[id*='progress']").show();
                             data.submit();
                         }
                     },
                     done: function (e, data) {                         
                        $("#<%: hid_filename_documento.ClientID %>").val(nrorandom.toString() + data.files[0].name);
                        $("#<%: btnSubirDocumento.ClientID %>").click();

                },
                progressall: function (e, data) {
                    var porc = parseInt(data.loaded / data.total * 100, 10);
                    progress(porc);
                },
                fail: function (e, data) {
                    alert(data.files[0].error);
                }

            }).prop('disabled', !$.support.fileInput).parent().addClass($.support.fileInput ? undefined : 'disabled');

        }

        function progress(value) {

            $(".progress-bar").css(
                'width',
                value + '%'
            );           
        }

        function init_Js_updpnlAgregarDocumentos() {

            $("#<%: ddlTiposDeDocumentosEscaneados.ClientID %>").select2({
                placeholder: 'Seleccione el tipo de documento.',
                allowClear: true

            });

        }
          </script>

</asp:Content>