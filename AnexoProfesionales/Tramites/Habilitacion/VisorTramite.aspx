<%@ Page Title="Visualizar Trámite" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VisorTramite.aspx.cs" Inherits="AnexoProfesionales.VisorTramite" %>

<%@ Register Src="~/Tramites/Habilitacion/Controls/Ubicacion.ascx" TagPrefix="uc" TagName="Ubicacion" %>
<%@ Register Src="~/Tramites/Habilitacion/Controls/Rubros.ascx" TagPrefix="uc" TagName="Rubros" %>
<%@ Register Src="~/Tramites/Habilitacion/Controls/Titulares.ascx" TagPrefix="uc" TagName="Titulares" %>
<%@ Register Src="~/Tramites/Habilitacion/Controls/ConformacionLocal.ascx" TagPrefix="uc" TagName="ConformacionLocal" %>
<%@ Register Src="~/Tramites/Habilitacion/Controls/DatosLocal.ascx" TagPrefix="uc" TagName="DatosLocal" %>
<%@ Register Src="~/Tramites/Habilitacion/Controls/CargaPlanos.ascx" TagPrefix="uc" TagName="CargaPlanos" %>
<%@ Register Src="~/Tramites/Habilitacion/Controls/RubrosCN.ascx" TagPrefix="uc" TagName="RubrosCN" %>
<%@ Register Src="~/Tramites/Habilitacion/Controls/SobreCargaDatos.ascx" TagPrefix="uc" TagName="CertificadoSobrecarga" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    


    <%: Scripts.Render("~/bundles/select2") %>
    <%: Styles.Render("~/bundles/select2Css") %>

    <%: Scripts.Render("~/bundles/fileupload") %>
    <%: Styles.Render("~/bundles/fileuploadCss") %>


    <%--ajax cargando ...--%>
    <div id="Loading" style="text-align: center; padding-bottom: 20px; margin-top:120px">
        <table border="0" style="border-collapse: separate; border-spacing: 5px; margin: auto" >
            <tr>
                <td>
                    <img src="<%: ResolveUrl("~/Content/img/app/Loading128x128.gif") %>"alt="" />
                </td>
            </tr>
            <tr>
                <td style="font-size: 24px">Cargando datos del trámite
                </td>
            </tr>
        </table>
    </div>
   
    <div id="page_content" style="display:none" >

        <%--extracto datos solicitud, acciones confirmar, imprimir, anular--%>
        <asp:UpdatePanel ID="updEstadoSolicitud" runat="server" RenderMode="Inline" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:HiddenField ID="hid_id_encomienda" runat="server" />
                <asp:HiddenField ID="hid_id_estado" runat="server" />
                
                <table border="0" style="width: 100%; ">
                    <tr>
                        <td style="width:45%">
                            <div class="row">
                                <div class="col-md-5 text-right">
                                    <label>Número de trámite:</label>
                                </div>
                                <div class="col-sm-7">
                                    <asp:Label ID="lblNroEncomienda" runat="server" Font-Bold="true" style="color:#344882;">"A Completar"</asp:Label>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-5 text-right">
                                    <label>Tipo de trámite:</label>
                                </div>
                                <div class="col-sm-7">
                                    <asp:Label ID="lblTipoTramite" runat="server" Font-Bold="true"> "A COMPLETAR"</asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5 text-right">
                                    <label>Fecha de inicio:</label>
                                </div>
                                <div class="col-sm-7">
                                    <asp:Label ID="lblFechaEncomienda" runat="server" Font-Bold="true"> "A COMPLETAR"</asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5 text-right">
                                    <label>Estado del Tramite:</label>
                                </div>
                                <div class="col-md-7">
                                    <asp:Label ID="lblEstadoSolicitud" runat="server" Font-Bold="true">Incompleto</asp:Label>
                                </div>
                            </div>
                            
                            <asp:Panel ID="pnlImpactoAmbiental" runat="server" CssClass="row">
                                <div class="col-md-5 text-right">
                                    <label>Tipo de Impacto Ambiental:</label>
                                </div>
                                <div class="col-md-7">
                                    <asp:Label ID="lblTipoImpactoAmbiental" runat="server" Font-Bold="true">A completar</asp:Label>
                                </div>
                            </asp:Panel>
                        </td>
                        <td class="text-right">
                            
                            <div id="pnlProcesando" class="text-center" style="display:none">
                                <img src="<%: ResolveUrl("~/Content/img/app/Loading64x64.gif") %>" alt=""/>
                                <span class="mleft10">Procesando...</span>
                            </div>
                             
                              <div id="pnlShortcuts" class="view view-shortcuts view-id-shortcuts box-panel" style="display:inline-block; width:auto;float:right">
                                <div class="views-row col-sm-12 text-right" style="display:flex">



                                     <asp:Panel ID="divbtnConfirmarTramite" runat="server" class="display-inline-block" >
                                            <asp:LinkButton ID="btnConfirmarTramite" title="Confirmar Anexo Técnico" CssClass="shortcut shortcut-sm" style="width:7em;height:10em" runat="server" OnClientClick="return ocultarShortcuts();" OnClick="btnConfirmarTramite_Click">
                                            <span class="bg-info-lt">
                                                <span class="glyphicon imoon-ok fs48"></span>
                                            </span>
                                            <p>Confirmar</p>
                                            </asp:LinkButton>

                                    </asp:Panel>

                                     <asp:Panel ID="divbtnImprimirSolicitud" runat="server" class="display-inline-block">

                                            <asp:HyperLink ID="btnImprimirSolicitud" CssClass="shortcut shortcut-sm" style="width:7em;height:10em" title="Descargar Anexo Técnico" runat="server" Target="_blank" >
                                         <span class="bg-success-lt">
                                            <span class="glyphicon imoon-download fs48"></span>
                                        </span>
                                           <p>Descargar Anexo</p>
                                            </asp:HyperLink>
                                       
                                    </asp:Panel>
                                    
                                     <asp:Panel ID="pnlBandeja" runat="server" class="display-inline-block">

                                            <asp:LinkButton ID="btnBandeja" title="Bandeja de Tramites" CssClass="shortcut shortcut-sm" style="width:7em;height:10em" runat="server" PostBackUrl="~/Solicitud/Bandeja.aspx">
                                            <span class="bg-primary-lt">
                                                <span class="glyphicon imoon-inbox fs48"></span>
                                            </span>
                                            <p>Bandeja</p>
                                            </asp:LinkButton>

                                    </asp:Panel>
                                    
                                     <asp:Panel ID="divbtnAnularTramite" runat="server" class="display-inline-block">
     
                                            <asp:LinkButton ID="btnAnularTramite" runat="server" CssClass="shortcut shortcut-sm" style="width:7em;height:10em" title="Anular Anexo Técnico" OnClientClick="return showfrmConfirmarAnulacion();">
                                            <span class="bg-gray-3">
                                                <span class="glyphicon imoon-close fs48 "></span>
                                            </span>
                                            <p>Anular</p>
                                            </asp:LinkButton>

                                    </asp:Panel>
                                </div>
                            </div>

                        </td>
                    </tr>
                </table>

                  <asp:Panel ID="pnlMsgPlanoContraIncendios" runat="server" CssClass="alert alert-success mtop10" Visible="false" Width="100%">
                    <asp:Label ID="lblMsgPlanoContraIncendios" Text="El trámite XXXXXX requiere la presentación de Plano Conforme a Obra de Instalación de Prevención contra Incendio registrado por la DGROC o Plano de Instalación de Prevención contra Incendio registrado por la DGROC, correspondiendo para este último una verificación in situ conforme lo establecido en la normativa vigente." ForeColor="Red" runat="server"></asp:Label>
                </asp:Panel>

                <asp:Panel ID="pnlTramiteIncompleto" runat="server" CssClass="alert alert-success mtop10" Visible="false" Width="100%">
                    <asp:Label ID="lblTextoTramiteIncompleto" runat="server"></asp:Label>
                </asp:Panel>

            </ContentTemplate>
        </asp:UpdatePanel>

        <%--Paneles con datos de la solicitud--%>
        <asp:UpdatePanel ID="updCargarDatos" runat="server" RenderMode="Inline" UpdateMode="Conditional">
            <ContentTemplate>

                <asp:HiddenField ID="hid_mostrar_conformacionLocal" runat="server" />
                <asp:HiddenField ID="hid_mostrar_certificadoSobrecarga" runat="server" />
                <asp:Button ID="btnCargarDatostramite" runat="server" Style="display: none" OnClick="btnCargarDatostramite_Click" />


                <%-- collapsible ubicaciones--%>
                <div id="box_ubicacion" class="accordion-group widget-box" style="background:#ffffff">

                    <%-- titulo collapsible ubicaciones--%>
                    <div class="accordion-heading">
                        <a id="ubicacion_btnUpDown" data-parent="#collapse-group" href="#collapse_ubicacion"
                            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                            <div class="widget-title">
                                <span class="icon"><i class="imoon-map-marker imoon-map-marker" style="color:#344882;"></i></span>
                                <h5>
                                    <asp:Label ID="lbl_ubicacion_tituloControl" runat="server" Text="Ubicaci&oacute;n"></asp:Label></h5>
                                <span class="btn-right"><i class="imoon imoon-chevron-up" style="color:#344882;"></i></span>
                            </div>
                        </a>
                    </div>

                    <%-- contenido del collapsible ubicaciones --%>
                    <div class="accordion-body collapse in" id="collapse_ubicacion" style="margin-left:20px; margin-right:20px">     
                          <%--Botón de Modificación de Ubicaciones --%>

                        <asp:Panel ID="pnlModifUbicacion" runat="server" CssClass="pull-right mbottom20 mright10" BackColor="#ffffff"  style="margin-top:10px; border-bottom:none;">
                            <asp:linkButton ID="btnModificarUbicacion" runat="server" PostBackUrl="~/Ubicacion.aspx"
                                CssClass="btn btn-primary" >
                                 <i class="imoon imoon-pencil"></i>
                                <span class="text">Modificar Ubicación</span>
                            </asp:linkButton>
                        </asp:Panel>
                            <uc:Ubicacion runat="server" ID="visUbicaciones"/>                    
                         </div>         
                    </div>     
 
                <%-- Datos del Local--%>
                <div id="box_datoslocal" class="accordion-group widget-box" style="background:#ffffff">
                    <div class="accordion-heading">
                        <a id="A1" data-parent="#collapse-group" href="#collapse_datoslocal"
                            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">
                            <div class="widget-title">
                                <span class="icon"><i class="imoon imoon-office" style="color:#344882;"></i></span>
                                <h5>
                                    <asp:Label ID="lblTituloDatosLocal" runat="server" Text="Datos del Local"></asp:Label></h5>
                                <span class="btn-right"><i class="imoon imoon-chevron-down" style="color:#344882;"></i></span>
                            </div>
                        </a>
                    </div>

                    <div class="accordion-body collapse" id="collapse_datoslocal">
                      <%--Botón de Modificación de Datos del Local  --%>
                        <asp:Panel ID="pnlModifDatosLocal" runat="server" CssClass="mbottom20 mtop10 col-sm-11 text-right" BackColor="#ffffff" style="margin-left:65px">
                            <asp:LinkButton ID="btnModificarDatosLocal" runat="server" PostBackUrl="~/DatosLocal.aspx"
                                 CssClass="btn btn-primary" >
                                 <i class="imoon imoon-pencil"></i>
                                <span class="text">Modificar Datos del Local</span>
                            </asp:LinkButton>
                        </asp:Panel>
                        <div class="form-horizontal">
                            <uc:DatosLocal ID="visDatoslocal" runat="server" />
                        </div>
                    </div>
                </div>
              
                <%-- Conformación del local--%>
                <div id="box_conformacionLocal" class="accordion-group widget-box"  style="background:#fff">

                    <div class="accordion-heading">
                        <a id="A4" data-parent="#collapse-group" href="#collapse_conformacionLocal"
                            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                            <div class="widget-title">
                                <span class="icon"><i class="imoon imoon-office" style="color:#344882;"></i></span>
                                <h5>
                                    <asp:Label ID="Label2" runat="server" Text="Conformación del Local"></asp:Label></h5>
                                <span class="btn-right"><i class="imoon imoon-chevron-down" style="color:#344882;"></i></span>
                            </div>
                        </a>
                    </div>

                    <div class="accordion-body collapse" id="collapse_conformacionLocal">
                       <%--Botón de Modificación de Datos del Local  --%>
                        <asp:Panel ID="pnlModConformacionLocal" runat="server" CssClass="text-right mbottom20" BackColor="#ffffff" Style="margin-top:20px; margin-right:25px">
                            <asp:LinkButton ID="btnModificarConformacionLocal" runat="server" PostBackUrl="~/ConformacionLocal.aspx"
                                 CssClass="btn btn-primary">
                                 <i class="imoon imoon-pencil"></i>
                                <span class="text">Modificar conformación de Local</span></asp:LinkButton>
                        </asp:Panel>
                            <uc:ConformacionLocal ID="visConformacionLocal" runat="server" />
                    </div>
                </div>

                    <%-- Certificado de Sobrecarga--%>
                <div id="box_certificadoSobrecarga" class="accordion-group widget-box"  style="background:#ffffff">

                    <div class="accordion-heading">
                        <a id="A6" data-parent="#collapse-group" href="#collapse_certificadoSobrecarga"
                            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                            <div class="widget-title">
                                <span class="icon"><i class="imoon imoon-office" style="color:#344882;"></i></span>
                                <h5>
                                    <asp:Label ID="LabelS" runat="server" Text="Certificado de Sobrecarga"></asp:Label></h5>
                                <span class="btn-right"><i class="imoon imoon-chevron-down" style="color:#344882;"></i></span>
                            </div>
                        </a>
                    </div>

                    <div class="accordion-body collapse" id="collapse_certificadoSobrecarga">
                        
                        <asp:Panel ID="pnlMod" runat="server" CssClass="text-right mbottom20 mtop10" BackColor="#ffffff" Style="margin-right:35px">
                            <asp:LinkButton ID="btnModificarCertificadoSobrecarga" runat="server" PostBackUrl="~/CertificadoSobrecarga.aspx"
                                 CssClass="btn btn-primary" >
                                 <i class="imoon imoon-pencil"></i>
                                <span class="text">Modificar Datos de/los certificados de sobrecarga</span>
                            </asp:LinkButton>
                        </asp:Panel>

                            <uc:CertificadoSobrecarga ID="visCertificadoSobrecarga" runat="server" />
           
                        <%--Botón de Modificación de Datos del Local  --%>

                    </div>
                </div>

                    <%--Carga de Planos--%>
                <div id="box_cargarPlano" class="accordion-group widget-box" style="background:#ffffff">

                    <div class="accordion-heading">
                        <a id="A7" data-parent="#collapse-group" href="#collapse_cargarPlano"
                            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                            <div class="widget-title">
                                <span class="icon"><i class="imoon imoon-map" style="color:#344882;"></i></span>
                                <h5>
                                    <asp:Label ID="Label5" runat="server" Text="Carga de Planos"></asp:Label></h5>
                                <span class="btn-right"><i class="imoon imoon-chevron-down" style="color:#344882;"></i></span>
                            </div>
                        </a>
                    </div>

                    <div class="accordion-body collapse" id="collapse_cargarPlano">
                         <%--Botón de Modificación de Datos del Local  --%> 
                           <asp:Panel ID="Panel3" runat="server" CssClass="text-right mbottom20 mtop10" BackColor="#ffffff" Style="margin-right:30px">
                            <asp:LinkButton ID="btnModificarCargarPlanos" runat="server" PostBackUrl="~/CargarPlano.aspx"
                            CssClass="btn btn-primary" >
                                 <i class="imoon imoon-pencil"></i>
                                <span class="text">Modificar los Planos Cargados</span>
                            </asp:LinkButton>
                          </asp:Panel>
                        <uc:CargaPlanos ID="visCargaPlanos" runat="server" />

                    </div>
                </div>

                <%-- Datos del Rubros--%>
                <div id="box_rubros" class="accordion-group widget-box" style="background:#ffffff">

                    <div class="accordion-heading">
                        <a id="A2" data-parent="#collapse-group" href="#collapse_rubros"
                            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                            <div class="widget-title">
                                <span class="icon"><i class="imoon imoon-hammer" style="color:#344882;"></i></span>
                                <h5>
                                    <asp:Label ID="lblTituloRubros" runat="server" Text="Rubros"></asp:Label></h5>
                                <span class="btn-right"><i class="imoon imoon-chevron-down" style="color:#344882;"></i></span>
                            </div>
                        </a>
                    </div>

                    <div class="accordion-body collapse" id="collapse_rubros">
                          <%--Botón de Modificación de Rubros --%>
                        <asp:Panel ID="pnlModifRubros" runat="server" CssClass="pull-right mbottom20" BackColor="#ffffff" Style="margin-right:30px">
                            <asp:LinkButton ID="btnModificarRubros" runat="server" 
                             CssClass="btn btn-primary" >
                                 <i class="imoon imoon-pencil"></i>
                                <span class="text">Modificar Datos del/los Rubros</span>
                            </asp:LinkButton>
                        </asp:Panel>

                            <uc:Rubros runat="server" id="visRubros" />
                            <uc:RubrosCN runat="server" id="visRubrosCN" />
                        <!-- Solo para ecis-->
                    <asp:Panel ID="pnlInfoAdicional" runat="server" Style="margin-top: 5px">
                        <div style="margin: 20px; margin-top: -5px">
                            <div style="color: #377bb5">
                                <h4><i class="imoon imoon-hammer" style="margin-right: 10px"></i>
                                    <asp:Label ID="lblInfoAdicional" runat="server" Text="Información adicional"></asp:Label></h4>
                                <hr />
                            </div>
                        </div>

                        <div class="form-group">
                            <div>
                                <asp:Label CssClass="control-label col-sm-4" ID="Label4" runat="server" Text="¿Realiza actividad de baile?"></asp:Label>
                                <asp:RadioButton CssClass="control-label text-left" ID="rbActBaileSI" runat="server" GroupName="ActBaile" Text="Si" Enabled="false"/>
                                &nbsp;
                                <asp:RadioButton CssClass="control-label text-left" ID="rbActBaileNo" runat="server" GroupName="ActBaile" Text="No" Enabled="false" />
                            </div>
                        </div>
                        <div class="form-group">
                            <div>
                                <asp:Label CssClass="control-label col-sm-4" ID="Label6" runat="server" Text="¿Posee estructura para luminaria?"></asp:Label>
                                <asp:RadioButton CssClass="control-label text-left" ID="rbLuminariaSi" runat="server" GroupName="Luminaria" Text="Si" Enabled="false"/>
                                &nbsp;
                                <asp:RadioButton CssClass="control-label text-left" ID="rbLuminariaNo" runat="server" GroupName="Luminaria" Text="No" Enabled="false"/>
                            </div>
                        </div>
                    </asp:Panel>
                        <!-- Solo para ecis-->
                    </div>
                </div>

                <%-- Datos de los Titulares--%>
                <div id="box_titulares" class="accordion-group widget-box"  style="background:#ffffff">

                    <div class="accordion-heading">
                        <a id="A3" data-parent="#collapse-group" href="#collapse_titulares"
                            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

                            <div class="widget-title">
                                <span class="icon"><i class="imoon imoon-users" style="color:#344882;"></i></span>
                                <h5>
                                    <asp:Label ID="Label1" runat="server" Text="Titulares"></asp:Label></h5>
                                <span class="btn-right"><i class="imoon imoon-chevron-down" style="color:#344882;"></i></span>
                            </div>
                        </a>
                    </div>

                    <div class="accordion-body collapse" id="collapse_titulares">
                        <%--Botón de Modificación de Titulares --%>
                         <asp:Panel ID="pnlModifTitulares" runat="server" CssClass="pull-right mbottom20 mright10" BackColor="#ffffff" Visible="true" Style="margin-left:-10px">
                            <asp:LinkButton ID="btnModificarTitulares" runat="server" PostBackUrl="~/VisorTitulares.aspx"
                                 CssClass="btn btn-primary" >
                                 <i class="imoon imoon-eye"></i>
                                <span class="text">Ver Datos del/los titulares</span>
                            </asp:LinkButton>  
                        </asp:Panel>
                            <uc:Titulares runat="server" id="visTitulares" />
                    </div>
                </div>

                <%-- Documentos--%>
                <div id="box_documentos" class="accordion-group widget-box"  style="background:#ffffff">

                    <div class="accordion-heading">
                        <a id="A5" data-parent="#collapse-group" href="#collapse_documentos"
                            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click2(this)">

                            <div class="widget-title">
                                <span class="icon"><i class="imoon imoon-file2" style="color:#344882;"></i></span>
                                <h5>
                                    <asp:Label ID="Label3" runat="server" Text="Documentos"></asp:Label></h5>
                                <span class="btn-right"><i class="imoon imoon-chevron-down" style="color:#344882;"></i></span>
                            </div>
                        </a>
                    </div>

                    <div class="accordion-body collapse" id="collapse_documentos" style="margin-left:10px; margin-right:10px">

                        <asp:UpdatePanel ID="upPnlDocumentos" runat="server" Width="900px" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div style="padding: 0px 10px 10px 10px; width: auto">
                                   
                                    <div class="pull-right mright10 mbottom10" >
                                        <asp:LinkButton ID="btnMostrarAgregadoDocumentos" runat="server" CssClass="btn btn-primary" OnClientClick="return DatosDocumentoAgregarToggle();">
                                            <i class="imoon-white imoon-chevron-down"></i>
                                            <span class="text">Agregar Documentos</span>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="mtop10" >
                                        <strong>Documentos Agregados</strong>
                                    </div>
                                    <asp:GridView ID="gridAgregados_db" runat="server" AutoGenerateColumns="false"
                                        AllowPaging="false" Style="border: none; " CssClass="table table-bordered mtop5"
                                        GridLines="None" Width="100%" DataKeyNames="id_docadjunto" OnRowDataBound="gridAgregados_db_RowDataBound">
                                        <HeaderStyle CssClass="grid-header" />
                                        <RowStyle CssClass="grid-row" />
                                        <AlternatingRowStyle BackColor="#efefef" />
                                        <Columns>
                                            <asp:BoundField DataField="CreateDate" DataFormatString="{0:d}" HeaderText="Fecha" HeaderStyle-CssClass="text-center" ItemStyle-Width="90px" />
                                            <asp:BoundField DataField="TiposDeDocumentosRequeridosDTO.nombre_tdocreq" HeaderText="Tipo de Documento" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"/>
                                            <asp:BoundField DataField="TiposDeDocumentosSistemaDTO.id_tipdocsis"  ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"/>
                                            <asp:BoundField DataField="nombre_archivo" HeaderText="Nombre del archivo" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"/>    
                                            <asp:TemplateField ItemStyle-CssClass="text-center" HeaderText="Descargar">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lnkImprimirAgregados" title="Descargar Archivo Subido" runat="server" NavigateUrl='<%#  Eval("url") %>'
                                                        Target="_blank">
                                                        <i class="imoon imoon-download fs24"></i>
                                                    </asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="text-center"  HeaderText="Eliminar">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEliminar" runat="server" title="Eliminar Archivo Subido"
                                                        CommandArgument='<%# Eval("id_docadjunto") %>'
                                                        OnClientClick="return showConfirmarEliminar(this);"
                                                        data-IdDocAdjunto='<%# Eval("id_docadjunto") %>'
                                                        Width="70px">
                                                        <i class="imoon imoon-trash fs24"></i> 

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
                                <asp:HiddenField ID="hid_id_docadjunto" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <%--Panel para agregar documentos adjuntos--%>
                        <asp:UpdatePanel ID="updpnlAgregarDocumentos" runat="server" UpdateMode="Conditional" style="margin-left:10px; margin-right:10px">
                            <ContentTemplate>

                                <asp:Panel ID="pnlAgregarDocumentos" runat="server" >

                                    <asp:HiddenField ID="hid_doc_id_solicitud" runat="server" />

                                    <div class="clearfix"></div>

                                    <asp:Panel ID="pnlDatosDocumento" runat="server" Style="display:none">

                                        <h5 class="text-left mleft10"><b>Carga de documentos</b></h5>

                                        <table >

                                            <tr>
                                                <td >
                                                   
                                                    <i class="imoon-number fs48"></i>
                                                </td>
                                                <td>
                                                    <p>Selecciona el tipo de documento que deseas agregar </p>

                                                    <asp:DropDownList ID="ddlTiposDeDocumentosEscaneados" runat="server"  Width="760px">
                                                    </asp:DropDownList>
               
                                                </td>
                                            </tr>
                                         
                                            <tr >
                                                <td style="vertical-align:initial">
                                                     <br /><br />
                                                    <i class="imoon-number2 fs48"></i>
                                                </td>
                                                <td >
                                                    <br /><br />
                                                    <p>
                                                        Presiona el botón "Seleccionar Archivo" y elija el archivo que deseas agregar.
                                                    </p>
                                                    <p>
                                                    <strong>Solo se admiten tipos de archivo con extensión pdf</strong>
                                                    </p>


                                                    <span id="btnSeleccionarFiles" class="btn btn-default fileinput-button">
                                                        <i class="imoon imoon-folder-open"></i>
                                                        <span class="text">Seleccionar archivo</span>
                                                        <input id="fileupload" type="file" name="files[]" multiple accept=".pdf">
                                                    </span>
                                                     &nbsp;<asp:Panel ID="ReqTipoDocEscaneado" runat="server" CssClass="alert alert-danger">
                                                        <p>
                                                            Para poder subir el documento debes seleccionar el tipo de documento en el paso 1.
                                                        </p>
                                                    </asp:Panel>

                                                    <asp:Button ID="btnSubirDocumento" runat="server" Text="Subir Documento" CssClass="btn btn-inverse" OnClick="btnSubirDocumento_Click" Style="display: none" />
                                                    <asp:HiddenField ID="hid_filename_documento" runat="server" />


                                                    <div id="pnlFiles" class="ptop20 hide">
                                                        <p>
                                                            Si el archivo que elegiste es el correcto presiona el bot&oacute;n "Subir documento" de lo contrario presiona el bot&oacute;n Cancelar y volv&eacute; a seleccionarlo.
                                                        </p>

                                                        <asp:UpdatePanel ID="updFiles" runat="server">
                                                            <ContentTemplate>
                                                                <div id="files" class="files"></div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                                        <!-- The global progress bar -->
                                                        <div id="progress" class="progress mtop5" style="display: none">
                                                            <div class="progress-bar progress-bar-success">
                                                                <span id="progressvalue" class="sr-only"></span>
                                                            </div>
                                                        </div>


                                       

                                    

                                        <asp:Panel ID="pnlErrorFoto" runat="server" CssClass="alert alert-error mtop10 hide">
                                            <button type="button" class="close" data-dismiss="alert">&times;</button>
                                            <asp:Label ID="lblErrorDocumento" runat="server">El detalle del documento es obligatorio.</asp:Label>
                                        </asp:Panel>

                                    </asp:Panel>
                                </asp:Panel>

                            </ContentTemplate>
                        </asp:UpdatePanel>
 
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


    <%--Modal Confirmar Anulación--%>
    <div id="frmConfirmarAnulacion" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top:-8px">Anular tr&aacute;mite</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon-remove-circle fs64 color-blue" ></i>
                            </td>
                            <td style="vertical-align:middle">
                                <label class="mleft10">¿ Est&aacute; seguro de anular el tr&aacute;mite ?</label>
                            </td>
                        </tr>
                    </table>

                </div>
                <div class="modal-footer mleft20 mright20">

                    <asp:UpdatePanel ID="updConfirmarAnular" runat="server">
                        <ContentTemplate>

                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updConfirmarAnular">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnAnular_Si" runat="server" CssClass="btn btn-primary" Text="Sí" OnClick="btnAnular_Si_Click" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">No</button>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <%--Modal Eliminar Documentos Adjuntos--%>
    <div id="frmEliminarDocumentoAdjunto" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top:-8px">Confirmaci&oacute;n</h4>
                </div>
                <div class="modal-body">
                    <div class="form-inline">

                        <div class="form-group">
                            <table>
                                <tr>
                                    <td>
                                        <i class="imoon imoon-remove-circle fs64 color-blue"></i>
                                    </td>
                                    <td>
                                        <label class="mleft10">¿Est&aacute; seguro que desea eliminar el documento adjunto?</label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="modal-footer mleft20 mright20">
                    <asp:UpdatePanel ID="updbtnConfirmarEliminarDocAdjunto" runat="server">
                        <ContentTemplate>

                            <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="updbtnConfirmarEliminarDocAdjunto" DynamicLayout="true" DisplayAfter="200" style="display: inline-block">
                                <ProgressTemplate>
                                    <span class="mleft10">Eliminando...</span>
                                    <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>

                            <div id="pnlBotonesConfirmarEliminarDocAdjunto">

                                <asp:LinkButton ID="btnEliminarDocumentoAdjunto" runat="server"  CssClass="btn btn-primary" OnClientClick="$('#pnlBotonesConfirmarEliminarDocAdjunto').hide();">
                                    <span class="text">S&iacute;</span>
                                </asp:LinkButton>
                                <button type="button" class="btn btn-default" data-dismiss="modal">No</button>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->

    
    <%--Confirmar Eliminar documento adjunto--%>
    <div id="frmConfirmarEliminar" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top:-8px">Eliminar</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon-remove-circle fs64 color-blue"></i>
                            </td>
                            <td style="vertical-align: middle">
                                <label class="mleft10">¿ Est&aacute; seguro de eliminar el documento adjunto ?</label>
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

    <script type="text/javascript">
        uploadButton = $('<button/>')
           .addClass('btn btn-primary uploadbutton')
           .text('Subir Documento')
           .on('click', function () {
               ($("#select2-drop").css("display", "none"));


               if ($("#<%: ddlTiposDeDocumentosEscaneados.ClientID  %>").select2("val").length == 0) {
                    $("#<%: ReqTipoDocEscaneado.ClientID %>").show();
                    return false;
                }
                else {
                    $("#<%: ReqTipoDocEscaneado.ClientID %>").hide();
                }
                $("[id*='progress']").show();

                var $this = $(this),
                data = $this.data();



                data.submit().always(function (data, e) {
                    if (data.files != undefined) {
                        $("#<%: hid_filename_documento.ClientID %>").val(data.files[0].Name);
                        $("#<%: btnSubirDocumento.ClientID %>").click();
                    }

                });

            });

            cancelUploadButton = $('<button/>')
            .addClass('btn btn-warning mleft5 cancelbutton')
            .text('Cancelar')
            .on('click', function () {
                $("#files table").remove();
                $("#<%: ReqTipoDocEscaneado.ClientID %>").hide();
                $("#pnlFiles").hide();
                return false;
            });

        $(document).ready(function () {
            $("#page_content").hide();
            $("#Loading").show();
            $("#<%: ReqTipoDocEscaneado.ClientID %>").hide();
            $("#<%: btnCargarDatostramite.ClientID %> ").click();
           

        });

        function showConfirmarEliminar(obj) {
            
            var id_docadjunto = $(obj).attr("data-IdDocAdjunto");

            $("#<%: hid_id_docadjunto.ClientID %>").val(id_docadjunto);

            $("#frmConfirmarEliminar").modal("show");
            return false;
        }

        function ocultarBotonesConfirmacion() {
            $("#pnlBotonesConfirmacionEliminar").hide();
            return false;
        }

        function hideConfirmarEliminar() {
            $("#frmConfirmarEliminar").modal("hide");
            return false;
        }

        function finalizarCarga() {
            $("#Loading").hide();
            $("#page_content").slideDown("slow");

            var mostrar_observacion = $("#<%: hid_mostrar_conformacionLocal.ClientID %>").val();

            if (mostrar_observacion == "true") {
                $("#box_conformacionLocal").show();
            } else {
                $("#box_conformacionLocal").hide();
            }

            var mostrar_CertificadoSobrecarga = $("#<%: hid_mostrar_certificadoSobrecarga.ClientID %>").val();

            if (mostrar_CertificadoSobrecarga == "true") {
                $("#box_certificadoSobrecarga").show();
            } else {
                $("#box_certificadoSobrecarga").hide();
            }
            DatosDocumentoAgregarToggle();
            return false;
        }

        function showfrmError() {
            $("#frmError").modal("show");
            return false;
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
        function bt_btnUpDown_collapse_click2(obj) {
            var href_collapse = $(obj).attr("href");

            if ($(href_collapse).attr("id") != undefined) {
                if ($(obj).find("i.imoon-chevron-down").length > 0) {
                    $(obj).find("i.imoon-chevron-down").switchClass("imoon-chevron-down", "imoon-chevron-up", 0);
                    ($("#s2id_MainContent_ddlTiposDeDocumentosEscaneados").css("display", "inline"));
                    $('#s2id_MainContent_ddlTiposDeDocumentosEscaneados').select2("close");
                    ($("#select2-drop").css("display", "none"));
                }
              
            else {
              
                    $(obj).find("i.imoon-chevron-up").switchClass("imoon-chevron-up", "imoon-chevron-down", 0);
                    ($("#s2id_MainContent_ddlTiposDeDocumentosEscaneados").css("display", "none"));
                    ($("#select2-drop").css("display", "none"));
                    $('#s2id_MainContent_ddlTiposDeDocumentosEscaneados').select2("close");
   

                }
            }

        }
        function noExisteFotoParcela(objimg) {

            $(objimg).attr("src",   "<%: ResolveUrl("~/Content/img/app/ImageNotFound.png") %>");

            return true;
        }


        function showfrmConfirmarAnulacion() {

            $("#frmConfirmarAnulacion").modal("show");
            return false;

        }

        function hidefrmConfirmarAnulacion() {

            $("#frmConfirmarAnulacion").modal("hide");
            return false;

        }

        function ocultarShortcuts() {
             $("#pnlShortcuts").hide();
             $("#pnlProcesando").show();
             return true;
        }
        ////////////////////////////////Documentos
       
        function tooltips() {
            $("[data-toggle='tooltip']").tooltip();
            return false;
        }

        function DatosDocumentoAgregarToggle() {
            if ($("#<%: pnlDatosDocumento.ClientID %>").css("display") == "none") {
                $("#<%: ddlTiposDeDocumentosEscaneados.ClientID %>").select2("val", "");
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

        function formatddlTipoDocumentoEscaneado_Selected(obj) {
            tooltips();

            var parts = obj.text.split('|');
            var html = "<b>" + parts[0] + "</b>";  //<div>" + parts[2] + "</div>";

            if (parts[0] == null)
                html = "";

            return html;
        }

        function formatddlTipoDocumentoEscaneado_Open(obj) {

            var parts = obj.text.split('|');

            var html = "<li class='select2-row'>";

            html += "<span class='select2-font-header1'>" + parts[0] + "</span>"
            if (parts[1] != undefined) {
                html += "<div class='select2-font-detalle1'>" + parts[1] + "</div>";
            }
            html += "<div class='select2-rowdivisor' /></li>";
            return html;
        }

        function init_Js_updpnlAgregarDocumentos() {

            $("#<%: ddlTiposDeDocumentosEscaneados.ClientID %>").select2({
                placeholder: 'Seleccione el tipo de documento.',
                allowClear: true,
                formatResult: formatddlTipoDocumentoEscaneado_Open,
                formatSelection: formatddlTipoDocumentoEscaneado_Selected,
            });

            $("#<%: ddlTiposDeDocumentosEscaneados.ClientID %>").on("change", function () {
                $("#<%: ReqTipoDocEscaneado.ClientID %>").hide();
            });
 

            'use strict';
            // Change this to the location of your server-side upload handler:
            var nrorandom = Math.floor(Math.random() * 1000000);
            $("#<%: hid_filename_documento.ClientID %>").val(nrorandom);
            

            var url = '<%: ResolveUrl("~/Scripts/jquery-fileupload/Upload.ashx?nrorandom=") %>' + nrorandom.toString();
            $("[id*='fileupload']").fileupload({
                url: url,
                dataType: 'json',
                formData: { folder: 'C:\\Temporal' },

                acceptFileTypes: /(\.|\/)(pdf)$/i,
                add: function (e, data) {

                    var goUpload = true;
                    var uploadFile = data.files[0];

                    if (!(/\.(pdf)$/i).test(uploadFile.name)) {
                        alert('Solo se permiten archivos de tipo .pdf');
                        goUpload = false;
                    }
                    if (uploadFile.size > 2097152) { // 2mb
                        alert('El tamaño máximo permitido para los documentos es de 2 MB');
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
                        //data.submit();
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

            //Select2CloseOnSelect2Open();
        }

        function confirmar_eliminar() {
            return confirm('¿Esta seguro que desea eliminar este Registro?');
        }
    </script>
</asp:Content>
