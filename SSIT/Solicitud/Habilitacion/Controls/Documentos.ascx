<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Documentos.ascx.cs" Inherits="SSIT.Solicitud.Habilitacion.Controls.Documentos" %>

<%@ Register Src="~/Solicitud/Controls/ucCargaDocumentos.ascx" TagPrefix="uc" TagName="CargaDocumentos" %>


<%--Panel carga documento Centro Cultural --%>
<asp:Panel ID="pnlDocumentosCCultural" runat="server" Style="display: none">
    <div id="box_documentosCC" class="accordion-group widget-box" style="background-color: #ffffff">
        <div class="accordion-heading">
            <a id="titulares_btnUpDownCC" data-parent="#collapse-group" href="#collapse_documentosCC"
                data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                <div class="widget-title">
                    <span class="icon"><i class="imoon imoon-file2" style="color: #344882"></i></span>
                    <h5>
                        <asp:Label ID="lbl_titulares_tituloControlCC" runat="server" Text="Constancia de inicio de trámite en IGJ o INAES"></asp:Label></h5>
                    <span class="btn-right"><i class="imoon imoon-chevron-up" style="color: #344882"></i></span>
                </div>
            </a>
        </div>
        <div class="accordion-body collapse in" id="collapse_documentosCC">
            <asp:UpdatePanel ID="updAgregarDocumentosCC" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div style="margin: 10px">
                        <div class="pull-right">
                            <asp:LinkButton ID="btnMostrarAgregadoDocumentosCC" runat="server" CssClass="btn btn-primary" OnClientClick="return DatosDocumentoAgregarToggleCC();">
                                <i class="imoon-white imoon-chevron-down"></i>
                                <span class="text">Agregar Constancia de trámite en IGJ o INAES</span>
                            </asp:LinkButton>
                        </div>
                        <div class="clearfix"></div>
                        <div style="padding: 0px 10px 10px 10px; width: auto">
                            <asp:GridView ID="gridCCultural_db" runat="server" AutoGenerateColumns="false"
                                AllowPaging="false" Style="border: none;" CssClass="table table-bordered mtop5"
                                ItemType="DataTransferObject.SSITDocumentosAdjuntosEntityDTO"
                                GridLines="None" Width="100%" DataKeyNames="id_docadjunto">
                                <HeaderStyle CssClass="grid-header" />
                                <RowStyle CssClass="grid-row" />
                                <AlternatingRowStyle BackColor="#efefef" />
                                <Columns>
                                    <asp:BoundField DataField="CreateDate" DataFormatString="{0:d}" HeaderText="Subido el" HeaderStyle-CssClass="text-center" ItemStyle-Width="90px" />
                                    <asp:BoundField DataField="detalle" HeaderText="Tipo de Documento" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="400px" />
                                    <asp:BoundField DataField="nombre_archivo" HeaderText="Nombre del archivo" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                                    <asp:TemplateField ItemStyle-CssClass="text-center" HeaderText="Acciones" HeaderStyle-CssClass="text-center" ItemStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkImprimirCC" runat="server" NavigateUrl='<%#  Eval("url") %>' title="Descargar" Target="_blank">
                                                    <span class="icon"><i class="imoon-download fs24 color-blue"></i></span>
                                            </asp:HyperLink>
                                            <asp:LinkButton ID="lnkEliminarCC" title="Eliminar" runat="server" Visible='<%# ((Convert.ToInt32(Eval("id_Estado")) == (int)StaticClass.Constantes.TipoEstadoSolicitudEnum.DATOSCONF) || ((Convert.ToInt32(Eval("id_Estado")) == (int)StaticClass.Constantes.TipoEstadoSolicitudEnum.OBSERVADO) && ((Item.CreateDate.Date)  > Convert.ToDateTime(hid_FechaDeCambioDeEstado.Value)))) %>'
                                                data-idDocAdjunto='<%# Eval("id_docadjunto") %>'
                                                OnClientClick="return showfrmConfirmarEliminarDocumento(this);">
                                                    <span class="icon"><i class="imoon imoon-trash fs24"></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>
                        </div>
                        <asp:UpdatePanel ID="UpdSinRegistrosCC" runat="server" UpdateMode="Conditional" Visible="false">
                            <ContentTemplate>
                                <div class="mtop10 table table-bordered mtop5">
                                    <div class="mtop10 mbottom10 mleft10">
                                        <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                                        No tiene Documentos Ingresados.
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Panel>

<%--Panel adjuntar Documento Centro Cultural --%>
<asp:UpdatePanel ID="updpnlAgregarDocumentosCC" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="pnlAgregarDocumentosCC" runat="server">
            <asp:Panel ID="pnlDatosDocumentoCC" runat="server" Style="display: none">
                <div id="DivaccordionCC" class="accordion-group widget-box" style="background-color: #ffffff">
                    <div class="accordion-heading">
                        <a id="A1accordionCC" data-parent="#collapse-group" href="#collapse_documentosAdicionalesCC"
                            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">
                            <div class="widget-title">
                                <span class="icon"><i class="imoon imoon-file2" style="color: #344882"></i></span>
                                <h5><asp:Label ID="Label2" runat="server" Text="Carga de documentos"></asp:Label></h5>
                                <span class="btn-right"><i class="imoon imoon-chevron-up" style="color: #344882"></i></span>
                            </div>
                        </a>
                    </div>
                    <div class="accordion-body collapse in" id="collapse_documentosAdicionalesCC">
                        <uc:CargaDocumentos runat="server" ID="CargaDocumentosCC" OnErrorCargaDocumento="OnErrorCargaDocumentoClick" OnSubirDocumentoClick="CargaDocumentos_SubirDocumentoClick" />
                    </div>
                </div>
            </asp:Panel>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

<%--Panel carga Certificado ProTeatro --%>
<asp:Panel ID="pnlDocumentosProTeatro" runat="server" Style="display: none">
    <div id="box_documentosPT" class="accordion-group widget-box" style="background-color: #ffffff">
        <div class="accordion-heading">
            <a id="titulares_btnUpDownPT" data-parent="#collapse-group" href="#collapse_documentosPT"
                data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                <div class="widget-title">
                    <span class="icon"><i class="imoon imoon-file2" style="color: #344882"></i></span>
                    <h5>
                        <asp:Label ID="lbl_titulares_tituloControlPT" runat="server" Text="Eximición de pago"></asp:Label></h5>
                    <span class="btn-right"><i class="imoon imoon-chevron-up" style="color: #344882"></i></span>
                </div>
            </a>
        </div>
        <div class="accordion-body collapse in" id="collapse_documentosPT">
            <asp:UpdatePanel ID="updAgregarDocumentosPT" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div style="margin: 10px">
                        <div class="pull-right">
                            <asp:LinkButton ID="btnMostrarAgregadoDocumentosPT" runat="server" CssClass="btn btn-primary" OnClientClick="return DatosDocumentoAgregarTogglePT();">
                                <i class="imoon-white imoon-chevron-down"></i>
                                <span class="text">Agregar</span>
                            </asp:LinkButton>
                        </div>
                        <div class="clearfix"></div>
                        <div style="padding: 0px 10px 10px 10px; width: auto">
                            <asp:GridView ID="gridProTeatro_db" runat="server" AutoGenerateColumns="false"
                                AllowPaging="false" Style="border: none;" CssClass="table table-bordered mtop5"
                                ItemType="DataTransferObject.SSITDocumentosAdjuntosEntityDTO"
                                GridLines="None" Width="100%" DataKeyNames="id_docadjunto">
                                <HeaderStyle CssClass="grid-header" />
                                <RowStyle CssClass="grid-row" />
                                <AlternatingRowStyle BackColor="#efefef" />
                                <Columns>
                                    <asp:BoundField DataField="CreateDate" DataFormatString="{0:d}" HeaderText="Subido el" HeaderStyle-CssClass="text-center" ItemStyle-Width="90px" />
                                    <asp:BoundField DataField="detalle" HeaderText="Tipo de Documento" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="400px" />
                                    <asp:BoundField DataField="nombre_archivo" HeaderText="Nombre del archivo" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                                    <asp:TemplateField ItemStyle-CssClass="text-center" HeaderText="Acciones" HeaderStyle-CssClass="text-center" ItemStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkImprimirPT" runat="server" NavigateUrl='<%#  Eval("url") %>' title="Descargar" Target="_blank">
                                                    <span class="icon"><i class="imoon-download fs24 color-blue"></i></span>
                                            </asp:HyperLink>
                                            <asp:LinkButton ID="lnkEliminarPT" title="Eliminar" runat="server" Visible='<%# ((Convert.ToInt32(Eval("id_Estado")) == (int)StaticClass.Constantes.TipoEstadoSolicitudEnum.DATOSCONF) || ((Convert.ToInt32(Eval("id_Estado")) == (int)StaticClass.Constantes.TipoEstadoSolicitudEnum.OBSERVADO) && ((Item.CreateDate.Date)  > Convert.ToDateTime(hid_FechaDeCambioDeEstado.Value)))) %>'
                                                data-idDocAdjunto='<%# Eval("id_docadjunto") %>'
                                                OnClientClick="return showfrmConfirmarEliminarDocumento(this);">
                                                    <span class="icon"><i class="imoon imoon-trash fs24"></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>
                        </div>
                        <asp:UpdatePanel ID="UpdSinRegistrosPT" runat="server" UpdateMode="Conditional" Visible="false">
                            <ContentTemplate>
                                <div class="mtop10 table table-bordered mtop5">
                                    <div class="mtop10 mbottom10 mleft10">
                                        <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                                        No tiene Documentos Ingresados.
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Panel>

<%--Panel adjuntar Certificado ProTeatro --%>
<asp:UpdatePanel ID="updpnlAgregarDocumentosPT" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="pnlAgregarDocumentosPT" runat="server">
            <asp:Panel ID="pnlDatosDocumentoPT" runat="server" Style="display: none">
                <div id="DivaccordionPT" class="accordion-group widget-box" style="background-color: #ffffff">
                    <div class="accordion-heading">
                        <a id="A1accordionPT" data-parent="#collapse-group" href="#collapse_documentosAdicionalesPT"
                            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">
                            <div class="widget-title">
                                <span class="icon"><i class="imoon imoon-file2" style="color: #344882"></i></span>
                                <h5><asp:Label ID="Label3" runat="server" Text="Carga de documentos"></asp:Label></h5>
                                <span class="btn-right"><i class="imoon imoon-chevron-up" style="color: #344882"></i></span>
                            </div>
                        </a>
                    </div>
                    <div class="accordion-body collapse in" id="collapse_documentosAdicionalesPT">
                        <br />
                        <ul>
                            <li><b>Teatros Independientes:</b> Certificado Pro-Teatro</li>
                            <li><b>Centros Culturales:</b> Constancia de Inicio de Tramite en IGJ o INAES</li>
                        </ul>
                        <uc:CargaDocumentos runat="server" ID="CargaDocumentosPT" OnErrorCargaDocumento="OnErrorCargaDocumentoClick" OnSubirDocumentoClick="CargaDocumentos_SubirDocumentoClick" />
                    </div>
                </div>
            </asp:Panel>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

      
<div id="box_documentos" class="accordion-group widget-box" style="background-color: #ffffff">
    <div class="accordion-heading">
        <a id="titulares_btnUpDown" data-parent="#collapse-group" href="#collapse_documentos"
            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

            <div class="widget-title">
                <span class="icon"><i class="imoon imoon-file2" style="color: #344882"></i></span>
                <h5>
                    <asp:Label ID="lbl_titulares_tituloControl" runat="server" Text="Documentos"></asp:Label></h5>
                <span class="btn-right"><i class="imoon imoon-chevron-up" style="color: #344882"></i></span>
            </div>
        </a>
    </div>
    <div class="accordion-body collapse in" id="collapse_documentos">
     <asp:UpdatePanel ID="updAgregarDocumentos" runat="server" UpdateMode="Conditional">
     <ContentTemplate>
  
           <div style="margin: 10px">
            <div class="pull-right">
                <asp:LinkButton ID="btnMostrarAgregadoDocumentos" runat="server" CssClass="btn btn-primary" OnClientClick="return DatosDocumentoAgregarToggle();">
            <i class="imoon-white imoon-chevron-down"></i>
            <span class="text">Agregar Documentos</span>
                </asp:LinkButton>
            </div>
            <asp:UpdatePanel ID="upPnlDocumentos" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div style="padding: 0px 10px 10px 10px; width: auto">
                        <asp:HiddenField ID="hid_FechaDeCambioDeEstado" runat="server" />
                        <div class="mtop10" id="textDocProf" runat="server">
                            <strong>Documentos ingresados por el Profesional</strong>
                        </div>
                        <asp:GridView ID="gridRelacionados_db" runat="server" AutoGenerateColumns="false"
                            AllowPaging="false" Style="border: none;" CssClass="table table-bordered mtop5"
                            GridLines="None" Width="100%" DataKeyNames="id_docadjunto">
                            <HeaderStyle CssClass="grid-header" />
                            <RowStyle CssClass="grid-row" />
                            <AlternatingRowStyle BackColor="#efefef" />
                            <Columns>
                                <asp:BoundField DataField="CreateDate" DataFormatString="{0:d}" HeaderText="Subido el" HeaderStyle-CssClass="text-center" ItemStyle-Width="90px" />
                                <asp:BoundField DataField="id_master" HeaderText="Anexo" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="80px" />
                                <asp:BoundField DataField="usuario" HeaderText="Profesional" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="400px" />
                                <asp:BoundField DataField="detalle" HeaderText="Tipo de Documento" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                                <asp:BoundField DataField="nombre_archivo" HeaderText="Nombre del archivo" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                                <asp:TemplateField ItemStyle-CssClass="text-center" HeaderText="Acciones" HeaderStyle-CssClass="text-center" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkImprimirRelacionados" runat="server" NavigateUrl='<%#  Eval("url") %>' title="Descargar" Target="_blank">
                             <span class="icon"><i class="imoon-download fs24 color-blue"></i></span>
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <div class="mtop10" id="textDocAgencia" runat="server">
                            <strong>Documentos ingresados por la Agencia</strong>
                        </div>
                        <asp:GridView ID="gridIngresados_db" runat="server" AutoGenerateColumns="false"
                            AllowPaging="false" Style="border: none;" CssClass="table table-bordered mtop5"
                            ItemType="DataTransferObject.SSITDocumentosAdjuntosDTO"
                            GridLines="None" Width="100%" DataKeyNames="id_docadjunto">
                            <HeaderStyle CssClass="grid-header" />
                            <RowStyle CssClass="grid-row" />
                            <AlternatingRowStyle BackColor="#efefef" />
                            <Columns>
                                <asp:BoundField DataField="CreateDate" DataFormatString="{0:d}" HeaderText="Subido el" HeaderStyle-CssClass="text-center" ItemStyle-Width="90px"  />
                                <asp:BoundField DataField="id_solicitud" HeaderText="Solicitud" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="80px" />
                                <asp:TemplateField HeaderText="Tipo de Documento" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="400px">
                                    <ItemTemplate>
                                        <%#(Item.id_tipodocsis==(int)StaticClass.Constantes.TiposDeDocumentosSistema.DOC_ADJUNTO_SSIT?Item.tdocreq_detalle:Item.TiposDeDocumentosSistemaDTO.nombre_tipodocsis) %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="nombre_archivo" HeaderText="Nombre del archivo" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                                <asp:TemplateField ItemStyle-CssClass="text-center" HeaderText="Acciones" HeaderStyle-CssClass="text-center" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkImprimirRelacionados" runat="server" NavigateUrl='<%# Item.url %>' title="Descargar" Target="_blank">
                             <span class="icon"><i class="imoon-download fs24 color-blue"></i></span>
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>


                        <div class="mtop10" id="textDocUd" runat="server">
                            <strong>Documentos Ingresados por Ud.</strong>
                        </div>
                        <asp:GridView ID="gridAgregados_db" runat="server" AutoGenerateColumns="false"
                            AllowPaging="false" Style="border: none;" CssClass="table table-bordered mtop5"
                            ItemType="DataTransferObject.SSITDocumentosAdjuntosEntityDTO"
                            GridLines="None" Width="100%" DataKeyNames="id_docadjunto">
                            <HeaderStyle CssClass="grid-header" />
                            <RowStyle CssClass="grid-row" />
                            <AlternatingRowStyle BackColor="#efefef" />
                            <Columns>
                                <asp:BoundField DataField="CreateDate" DataFormatString="{0:d}" HeaderText="Subido el" HeaderStyle-CssClass="text-center" ItemStyle-Width="90px" />
                                <%--<asp:BoundField DataField="id_master" HeaderText="Solicitud" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="80px" />--%>
                                <asp:BoundField DataField="detalle" HeaderText="Tipo de Documento" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="400px"  />
                                <asp:BoundField DataField="nombre_archivo" HeaderText="Nombre del archivo" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                                <asp:TemplateField ItemStyle-CssClass="text-center" HeaderText="Acciones" HeaderStyle-CssClass="text-center" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkImprimirAgregados" runat="server" NavigateUrl='<%#  Eval("url") %>' title="Descargar" Target="_blank">
                             <span class="icon"><i class="imoon-download fs24 color-blue"></i></span>
                                        </asp:HyperLink>
                                        <asp:LinkButton ID="lnkEliminar" title="Eliminar" runat="server" Visible= '<%# ((Convert.ToInt32(Eval("id_Estado")) == (int)StaticClass.Constantes.TipoEstadoSolicitudEnum.INCOM) 
                                                                                                                        || (Convert.ToInt32(Eval("id_Estado")) == (int)StaticClass.Constantes.TipoEstadoSolicitudEnum.COMP) 
                                                                                                                        || (Convert.ToInt32(Eval("id_Estado")) == (int)StaticClass.Constantes.TipoEstadoSolicitudEnum.DATOSCONF) 
                                                                                                                        || ((Convert.ToInt32(Eval("id_Estado")) == (int)StaticClass.Constantes.TipoEstadoSolicitudEnum.OBSERVADO) && ((Item.CreateDate.Date)  > Convert.ToDateTime(hid_FechaDeCambioDeEstado.Value)))) %>' 
                                            data-idDocAdjunto='<%# Eval("id_docadjunto") %>'
                                            OnClientClick="return showfrmConfirmarEliminarDocumento(this);"
                                            >
                            <span class="icon"><i class="imoon imoon-trash fs24"></i></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>

                        <div class="mtop10" id="textDocObs" runat="server">
                            <strong>Documentos Relacionados a Observaciones</strong>
                        </div>
                        <asp:GridView ID="grdRelacionadosObservaciones" runat="server" AutoGenerateColumns="false"
                            AllowPaging="false" Style="border: none;" CssClass="table table-bordered mtop5"
                            GridLines="None" Width="100%">
                            <HeaderStyle CssClass="grid-header" />
                            <RowStyle CssClass="grid-row" />
                            <AlternatingRowStyle BackColor="#efefef" />
                            <Columns>
                                <asp:BoundField DataField="CreateDate" DataFormatString="{0:d}" HeaderText="Subido el" HeaderStyle-CssClass="text-center" ItemStyle-Width="90px" />
                                <asp:BoundField DataField="id_master" HeaderText="Solicitud" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="80px" />
                                <asp:BoundField DataField="nombre_tdocreq" HeaderText="Tipo de Documento" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="400px"  />
                                <asp:BoundField DataField="filename" HeaderText="Nombre del archivo" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                                <asp:TemplateField ItemStyle-CssClass="text-center" HeaderText="Acciones" HeaderStyle-CssClass="text-center" ItemStyle-Width="80px">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkImprimirAgregados" runat="server" NavigateUrl='<%#  Eval("url") %>' title="Descargar" Target="_blank">
                             <span class="icon"><i class="imoon-download fs24 color-blue"></i></span>
                                        </asp:HyperLink>

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdSinRegistros" runat="server" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                <div class="mtop10 table table-bordered mtop5">
                     <div class="mtop10 mbottom10 mleft10">
                        <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                        No tiene Documentos Ingresados.
                    </div>
              </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
          </ContentTemplate>
     </asp:UpdatePanel>
    </div>
</div>

<%--Panel para agregar documentos adjuntos--%>
 <asp:UpdatePanel ID="updpnlAgregarDocumentos" runat="server" UpdateMode="Conditional">
    <ContentTemplate>

        <asp:Panel ID="pnlAgregarDocumentos" runat="server">
            
            <asp:HiddenField ID="hid_doc_id_solicitud" runat="server" />

            <asp:Panel ID="pnlDatosDocumento" runat="server" Style="display: none">
                <div id="Div1" class="accordion-group widget-box" style="background-color: #ffffff">
                    <div class="accordion-heading">
                        <a id="A1" data-parent="#collapse-group" href="#collapse_documentosAdicionales"
                            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                            <div class="widget-title">
                                <span class="icon"><i class="imoon imoon-file2" style="color: #344882"></i></span>
                                <h5>
                                    <asp:Label ID="Label1" runat="server" Text="Carga de documentos"></asp:Label></h5>
                                <span class="btn-right"><i class="imoon imoon-chevron-up" style="color: #344882"></i></span>
                            </div>
                        </a>
                    </div>
                    <div class="accordion-body collapse in" id="collapse_documentosAdicionales">
                        <uc:CargaDocumentos runat="server" ID="CargaDocumentos"  OnErrorCargaDocumento="OnErrorCargaDocumentoClick" OnSubirDocumentoClick="CargaDocumentos_SubirDocumentoClick"/>
                    </div>
                </div>

            </asp:Panel>
        </asp:Panel>

    </ContentTemplate>
</asp:UpdatePanel>
   

<%--modal de Errores--%>
<div id="frmErrorDocumentos" class="modal fade" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title">Error</h4>
            </div>
            <div class="modal-body">
                <table style="border-collapse: separate; border-spacing: 5px">
                    <tr>
                        <td style="text-align: center; vertical-align: text-top">
                            <i class="imoon imoon-remove-circle fs64" style="color: #f00"></i>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="form-group">
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
    </div>
</div>
<!-- /.modal -->


<%--Modal Confirmar Eliminar--%>
<div id="frmConfirmarEliminarDocumento" class="modal fade" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                <h4 class="modal-title" style="margin-top:-8px">Eliminar Documento</h4>
            </div>
            <div class="modal-body">
                <table style="border-collapse: separate; border-spacing: 5px">
                    <tr>
                        <td style="text-align: center; vertical-align: text-top">
                            <i class="imoon imoon-remove-circle fs64 color-blue"></i>
                        </td>
                        <td style="vertical-align: middle">
                            <label class="mleft10">¿ Est&aacute; seguro de eliminar el Documento ?</label>
                        </td>
                    </tr>
                </table>

            </div>
            <div class="modal-footer mleft20 mright20">
                <asp:HiddenField ID="hid_id_docadjuntoEliminar" runat="server" />
                <asp:UpdatePanel ID="updConfirmarEliminarDocumento" runat="server">
                    <ContentTemplate>

                        <div class="form-inline">
                            <div class="form-group">
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updConfirmarEliminarDocumento">
                                    <ProgressTemplate>
                                        <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                            <div id="pnlBotonesConfirmacionEliminarDocumento" class="form-group">
                                <asp:Button ID="btnEliminarDocumento" runat="server" CssClass="btn btn-primary" Text="Sí" OnClick="btnEliminarDocumento_Click" OnClientClick="ocultarBotonesConfirmacionEliminarDocumento();" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">No</button>
                            </div>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
    </div>
</div>


<script type="text/javascript">
  
    function hidefrmConfirmarEliminarDocumento() {
        $("#frmConfirmarEliminarDocumento").modal("hide");
        return false;
    }

    function showfrmConfirmarEliminarDocumento(obj) {
        var  idDocAdjunto = $(obj).attr("data-idDocAdjunto");
        $("#<%: hid_id_docadjuntoEliminar.ClientID %>").val(idDocAdjunto);
        $("#frmConfirmarEliminarDocumento").modal("show");
        return false;
    }

    function ocultarBotonesConfirmacionEliminarDocumento() {
        $("#pnlBotonesConfirmacionEliminarDocumento").hide();
        return false;
    }

    function tooltips() {
        $("[data-toggle='tooltip']").tooltip();
        return false;
    }

    function showfrmErrorDocumentos() {
        $("#frmErrorDocumentos").modal("show");
        return false;
    }

    function DatosDocumentoAgregarToggle() {
        if ($("#<%: pnlDatosDocumento.ClientID %>").css("display") == "none") {
            $("#<%: pnlDatosDocumento.ClientID %>").show("slow");
            $("#<%: btnMostrarAgregadoDocumentos.ClientID %> i").removeClass('imoon-chevron-down');
            $("#<%: btnMostrarAgregadoDocumentos.ClientID %> i").addClass('imoon-chevron-up');
        }
        else {
            $("#<%: pnlDatosDocumento.ClientID %>").hide("slow");
            $("#<%: btnMostrarAgregadoDocumentos.ClientID %> i").removeClass('imoon-chevron-up');
            $("#<%: btnMostrarAgregadoDocumentos.ClientID %> i").addClass('imoon-chevron-down');

        }
        return false;
    }
    function DatosDocumentoAgregarTogglePT() {
        if ($("#<%: pnlDatosDocumentoPT.ClientID %>").css("display") == "none") {
            $("#<%: pnlDatosDocumentoPT.ClientID %>").show("slow");
            $("#<%: btnMostrarAgregadoDocumentosPT.ClientID %> i").removeClass('imoon-chevron-down');
            $("#<%: btnMostrarAgregadoDocumentosPT.ClientID %> i").addClass('imoon-chevron-up');
        }
        else {
            $("#<%: pnlDatosDocumentoPT.ClientID %>").hide("slow");
            $("#<%: btnMostrarAgregadoDocumentosPT.ClientID %> i").removeClass('imoon-chevron-up');
            $("#<%: btnMostrarAgregadoDocumentosPT.ClientID %> i").addClass('imoon-chevron-down');

        }
        return false;
    }
    function DatosDocumentoAgregarToggleCC() {
        if ($("#<%: pnlDatosDocumentoCC.ClientID %>").css("display") == "none") {
            $("#<%: pnlDatosDocumentoCC.ClientID %>").show("slow");
            $("#<%: btnMostrarAgregadoDocumentosCC.ClientID %> i").removeClass('imoon-chevron-down');
            $("#<%: btnMostrarAgregadoDocumentosCC.ClientID %> i").addClass('imoon-chevron-up');
        }
        else {
            $("#<%: pnlDatosDocumentoCC.ClientID %>").hide("slow");
            $("#<%: btnMostrarAgregadoDocumentosCC.ClientID %> i").removeClass('imoon-chevron-up');
            $("#<%: btnMostrarAgregadoDocumentosCC.ClientID %> i").addClass('imoon-chevron-down');

        }
        return false;
    }

        function confirmar_eliminar() {
            return confirm('¿Esta seguro que desea eliminar este Registro?');
        }
</script>
