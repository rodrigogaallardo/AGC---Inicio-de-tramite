<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VisorTramite2.aspx.cs" Inherits="SSIT.Solicitud.VisorTramite2" %>

<%@ Register Src="~/Solicitud/Habilitacion/Controls/Rubros2.ascx" TagPrefix="uc" TagName="Rubros" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%: Scripts.Render("~/bundles/select2") %>
    <%: Styles.Render("~/bundles/select2Css") %>

    <%: Scripts.Render("~/bundles/fileupload") %>
    <%: Styles.Render("~/bundles/fileuploadCss") %>

    <div id="page_content" style="display: inline">
        <asp:HiddenField ID="hid_id_solicitud" runat="server" />
        <asp:HiddenField ID="hid_id_estado" runat="server" />
        <asp:UpdatePanel ID="updEstadoSolicitud" runat="server" RenderMode="Inline" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-7" style="margin-left: -105px">
                        <div class="row">
                            <label class="col-sm-5 text-right">Número de trámite:</label>
                            <asp:Label ID="lblNroSolicitud" CssClass="col-sm-7" runat="server" Font-Bold="true" Font-Size="16px" Style="color: #337ab7">"A Completar"</asp:Label>

                        </div>

                        <div class="row">
                            <label class="col-sm-5 text-right">Tipo de trámite:</label>
                            <asp:Label ID="lblTipoTramite" CssClass="col-sm-7" runat="server" Font-Bold="true"> "A COMPLETAR"</asp:Label>

                        </div>
                        <div class="row">
                            <label class="col-sm-5 text-right">Estado del trámite:</label>
                            <asp:Label ID="lblEstadoSolicitud" CssClass="col-sm-7" runat="server" Font-Bold="true">Incompleto</asp:Label>

                        </div>
                    </div>
                    <div class="col-sm-6">

                        <div id="pnlProcesando" class="text-center" style="display: none">
                            <img src="<%: ResolveUrl("~/Content/img/app/Loading64x64.gif") %>" alt="" />
                            <span class="mleft10">Procesando...</span>
                        </div>

                        <div id="pnlShortcuts" class="view view-shortcuts view-id-shortcuts box-panel" style="display: inline-block; width: auto; float: right">
                            <div class="views-row col-sm-12 text-right" style="display: flex">

                                <asp:Panel ID="divbtnConfirmarTramite" runat="server" class=" display-inline-block ">

                                    <asp:LinkButton ID="btnConfirmarTramite" CssClass="shortcut shortcut-sm" Style="width: 7em; height: 10em" runat="server" title="Confirmar Solicitud de Tramite" OnClientClick="return validarGuardar();">
                                            <span class="bg-info-lt">
                                                <span class="glyphicon imoon-ok fs48"></span>
                                            </span>
                                            <p >Confirmar</p>
                                    </asp:LinkButton>

                                </asp:Panel>

                               <asp:Panel ID="divbtnImprimirSolicitud" runat="server" class="display-inline-block">

                                    <asp:HyperLink ID="btnImprimirSolicitud" CssClass="shortcut shortcut-sm" Style="width: 7em; height: 10em" title="Descargar Solicitud de Tramite" runat="server" Target="_blank">
                                        <span class="bg-success-lt">
                                            <span class="glyphicon imoon-download fs48"></span>
                                        </span>
                                        <p >Descargar Solicitud</p>
                                    </asp:HyperLink>

                                </asp:Panel>

                                <asp:Panel ID="pnlBandeja" runat="server" class=" display-inline-block ">

                                    <asp:LinkButton ID="btnBandeja" CssClass="shortcut shortcut-sm" Style="width: 7em; height: 10em" runat="server" title="Bandeja de Tramites" PostBackUrl="~/Solicitud/Bandeja2.aspx">
                                            <span class="bg-primary-lt">
                                                <span class="glyphicon imoon-inbox fs48"></span>
                                            </span>
                                            <p >Bandeja</p>
                                    </asp:LinkButton>

                                </asp:Panel>

                                <asp:Panel ID="divbtnAnularTramite" runat="server" class=" display-inline-block ">

                                    <asp:LinkButton ID="btnAnularTramite" CssClass="shortcut shortcut-sm" Style="width: 7em; height: 10em" runat="server" title="Anular Solicitud de Tramite" OnClientClick="return showfrmConfirmarAnulacion1();">
                                            <span class="bg-gray-3">
                                                <span class="glyphicon imoon-close fs48 "></span>
                                            </span>
                                            <p >Anular</p>
                                    </asp:LinkButton>

                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
                <table border="0" style="width: 100%">
                    <tr>
                        <td style="width: 45%">
                            <div class="row">
                                <div class="col-md-5 text-right">
                                </div>
                                <div class="col-sm-7">
                                </div>

                            </div>
                            <div class="row" id="divCodigoSeguridad" runat="server" style="display: none">
                                <div class="col-md-5 text-right">
                                </div>
                                <div class="col-md-7">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5 text-right">
                                </div>
                                <div class="col-sm-7">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5 text-right mtop5">
                                </div>
                                <div class="col-md-7 mtop5">
                                </div>
                            </div>
                        </td>
                        <td class="text-right"></td>
                    </tr>
                </table>

                <asp:Panel ID="pnlTramiteIncompleto" runat="server" CssClass="alert alert-success mtop10" Visible="false" Width="100%">
                    <asp:Label ID="lblTextoTramiteIncompleto" runat="server"></asp:Label>
                </asp:Panel>

            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="updCargarDatos" runat="server" RenderMode="Inline" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="accordion-body collapse in" id="collapse_titulares">
                    <asp:Panel ID="pnlDatos" runat="server" CssClass="form-inline" BackColor="#ffffff">
                        <div class="big-heading">
                            <div class="container" style="text-align: center;">
                                <h2>SOLICITUD DE DECLARACION RESPONSABLE</h2>
                            </div>
                        </div>
                        <br
                        <div style="text-align: justify">
                            <p>
                                Usted se encuentra tramitando una solicitud de Declaraci&oacute;n Responsable de acuerdo a la Ley Marco de Regulaci&oacute;n de Actividades Econ&oacute;micas de la Ciudad de Buenos Aires N° 6.101.
                            </p>
                            <p>
                                En los casos que la actividad econ&oacute;mica solicitada se encuentre dentro de las enumeradas en el art&iacute;culo 13 (Licencia de Actividades Econ&oacute;micas) de la citada ley, o que se trate de un tramite de transferencia, ampliación o disminución de superficie o redistribución de uso; deber&aacute; solicitar un turno en la Agencia Gubernamental de Control, sito en calle Juan Domingo Per&oacute;n 2941, de esta Ciudad.
                            </p>
                            <br />
                            <h3>DECLARACIÓN RESPONSABLE</h3>
                            <p>
                                A los efectos del presente tr&aacute;mite el Ciudadano Responsable declara bajo juramento de acuerdo a los Principios de Responsabilidad Ciudadana y Profesional y Buena Fe establecidos en la Ley N° 6.101, los datos siguientes:
                            </p>

                        </div>
                        <br />
                        <div class="row mleft10 mtop1">
                            <div class="span4">
                                <b>
                                    <asp:Label ID="lblTitulares" runat="server" Text="Titular/es:" class="control-label col-sm-3 mtop5 text-right"></asp:Label></b>
                                <asp:TextBox ID="txtTitulares" runat="server" Width="50%" CssClass="form-control" MaxLength="100"></asp:TextBox>

                                <div id="Req_Titu" class="field-validation-error" style="display: none;">
                                    Debe ingresar el/los titular/es.
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row mleft10 mtop1">
                            <div class="span4">
                                <b>
                                    <asp:Label runat="server" class="control-label col-sm-3 mtop5 text-right">C.U.I.T. (*):</asp:Label></b>

                                <asp:TextBox ID="txtCuit" runat="server" MaxLength="11" Width="150px" CssClass="form-control"></asp:TextBox>
                                <div id="Req_CuitPF" class="field-validation-error" style="display: none;">
                                    Debe ingresar el CUIT.
                                </div>
                                <div id="ValFormato_Cuit" class="field-validation-error" style="display: none;">
                                    El cuit tiene formato incorrecto.
                                </div>
                                <div id="ValCantidad_Cuit" class="field-validation-error" style="display: none;">
                                    El cuit debe contener 11 dígitos sin guiones.
                                </div>

                            </div>
                        </div>
                        <br />
                        <div class="row mleft10 mtop1">
                            <div class="span4">
                                <b>
                                    <asp:Label ID="lblProfesinal" runat="server" Text="Profesional:" class="control-label col-sm-3 mtop5 text-right"></asp:Label></b>
                                <asp:TextBox ID="txtProfesional" runat="server" Width="50%" CssClass="form-control" MaxLength="100"></asp:TextBox>

                                <div id="Req_Profe" class="field-validation-error" style="display: none;">
                                    Debe ingresar los datos del profesional.
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row mleft10 mtop1">
                            <div class="span4">
                                <b>
                                    <asp:Label ID="lblMatricula" runat="server" Text="Matricula:" class="control-label col-sm-3 mtop5 text-right"></asp:Label></b>
                                <asp:TextBox ID="txtMatricula" runat="server" Width="30%" CssClass="form-control" MaxLength="20"></asp:TextBox>

                                <div id="Req_Matri" class="field-validation-error" style="display: none;">
                                    Debe ingresar la matricula.
                                </div>
                            </div>
                        </div>
                        <br />

                        <div class="row  mleft10 mtop1">
                            <div class="span4">
                                <b>
                                    <asp:Label ID="lblPartidaH" runat="server" Text="Partida Horizontal:" class="control-label col-sm-3 mtop5 text-right"></asp:Label></b>

                                <asp:TextBox ID="txtPartidaH" runat="server" Width="80px" CssClass="form-control"></asp:TextBox>
                                <div id="Req_PartidaH" class="field-validation-error" style="display: none;">
                                    Debe ingresar la Partida Horizontal.
                                </div>
                                <div id="ValFormato_PH" class="field-validation-error" style="display: none;">
                                    La Partida Horizontal tiene formato incorrecto.
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row  mleft10 mtop1">
                            <div class="span4">
                                <b>
                                    <asp:Label ID="lblPrtidaM" runat="server" Text="Partida Matriz:" class="control-label col-sm-3 mtop5 text-right"></asp:Label></b>

                                <asp:TextBox ID="txtPartidaM" runat="server" Width="80px" CssClass="form-control"></asp:TextBox>
                                <div id="Req_PartidaM" class="field-validation-error" style="display: none;">
                                    Debe ingresar la Partida Matriz.
                                </div>
                                <div id="ValFormato_PM" class="field-validation-error" style="display: none;">
                                    La Partida Matriz tiene formato incorrecto.
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row  mleft10 mtop1">
                            <div class="span4">
                                <b>
                                    <asp:Label ID="lblCalle" runat="server" Text="Calle:" class="control-label col-sm-3 mtop5 text-right"></asp:Label></b>

                                <asp:TextBox ID="txtCalle" runat="server" Width="60%" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                <div id="Req_Calle" class="field-validation-error" style="display: none;">
                                    Debe ingresar la Calle.
                                </div>
                            </div>
                        </div>

                        <br />
                        <div class="row  mleft10 mtop1">
                            <div class="span4">
                                <b>
                                    <asp:Label ID="lblAltura" runat="server" Text="Altura:" class="control-label col-sm-3 mtop5 text-right"></asp:Label></b>

                                <asp:TextBox ID="txtAltura" runat="server" Width="80px" CssClass="form-control"></asp:TextBox>
                                <div id="Req_Altura" class="field-validation-error" style="display: none;">
                                    Debe ingresar la Altura.
                                </div>
                                <div id="ValFormato_Altura" class="field-validation-error" style="display: none;">
                                    La Altura tiene formato incorrecto.
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row  mleft10 mtop1">
                            <div class="span4">
                                <b>
                                    <asp:Label ID="lblPiso" runat="server" Text="Piso:" class="control-label col-sm-3 mtop5 text-right"></asp:Label></b>

                                <asp:TextBox ID="txtPiso" runat="server" Width="30%" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                <div id="Req_Piso" class="field-validation-error" style="display: none;">
                                    Debe ingresar el/los pisos.
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row  mleft10 mtop1">
                            <div class="span4">
                                <b>
                                    <asp:Label ID="lblUnidad" runat="server" Text="Unidad Funcional:" class="control-label col-sm-3 mtop5 text-right"></asp:Label></b>

                                <asp:TextBox ID="txtUnidad" runat="server" Width="80px" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                <div id="Req_UF" class="field-validation-error" style="display: none;">
                                    Debe ingresar la unidad funcional.
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row  mleft10 mtop1">
                            <div class="span4">
                                <b>
                                    <asp:Label ID="lblDescripcion" runat="server" Text="Descripción de la Actividad:" class="control-label col-sm-3 mtop5 text-right"></asp:Label></b>

                                <asp:TextBox ID="txtActividad" runat="server" Width="60%" CssClass="form-control" MaxLength="250"></asp:TextBox>
                                <div id="Req_Actividad" class="field-validation-error" style="display: none;">
                                    Debe ingresar la actividad.
                                </div>
                            </div>
                        </div>
                        <br />
                        <asp:UpdatePanel ID="updZonaMixtura" runat="server">
                            <ContentTemplate>
                                <div class="row mleft10 mtop1">
                                    <label class="control-label col-sm-3 mtop5 text-right"><b>Zona Mixtura:</b></label>
                                    <asp:DropDownList ID="ddlZonaMixtura" runat="server" Width="40%" CssClass="form-control"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div id="Req_Zona" class="field-validation-error" style="display: none;">
                                    Debe ingresar la Zona.
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        <div class="row mleft10 mtop1">
                            <b>
                                <asp:Label ID="lblSuperficie" runat="server" Text="Superficie total a Habilitar:" class="control-label col-sm-3 mtop5 text-right"></asp:Label></b>

                            <asp:TextBox ID="txtSuperficie" runat="server" Width="80px" CssClass="form-control"></asp:TextBox>
                            <div id="Req_Superficie2" class="field-validation-error" style="display: none;">
                                Debe ingresar la Superficie.
                            </div>
                            <div id="ValFormato_Superficie" class="field-validation-error" style="display: none;">
                                La Superficie tiene formato incorrecto.
                            </div>
                        </div>
                        <br />
                    </asp:Panel>
                    <div class="row  mleft10 mtop1">
                        <div class="span4">
                            <b>
                                <asp:Label ID="Label1" runat="server" Text="Rubros:" class="control-label col-sm-3 mtop5 text-right"></asp:Label></b>

                            <div id="tabRubros">
                                <uc:Rubros runat="server" ID="visRubros" />
                            </div>
                        </div>
                    </div>

                    <br />
                    <hr />
                    <br />
                    <div style="text-align: justify">
                        <p>
                            El Ciudadano Responsable declara bajo juramento que el profesional interviniente designado se encuentra habilitado a los efectos de la presente tramitaci&oacute;n y ha verificado las condiciones del establecimiento, cumpliendo la normativa vigente.
                        </p>
                        <p>
                            El Ciudadano Responsable declara bajo juramento que la totalidad de la informaci&oacute;n volcada es ver&iacute;dica.
                        </p>
                        <p>
                            El Ciudadano Responsable declara que el establecimiento re&uacute;ne las condiciones generales de higiene y seguridad y bajo juramento; y se compromete a mantenerlo en las condiciones exigidas por las disposiciones vigentes para su funcionamiento.
                        </p>
                        <p>
                            El Ciudadano Responsable declara que se encuentra en conocimiento de las Leyes Nros. 123, 6.014, 6.099, 6.100, y 6.101; y las condiciones del local objeto de la presente solicitud se encuentran en un todo de acuerdo con esta normativa y toda norma reglamentaria y complementaria.
                        </p>
                        <p>
                            Las notificaciones vinculadas al presente tr&aacute;mite ser&aacute;n v&aacute;lidas en el domicilio electr&oacute;nico declarado y utilizado por el solicitante en el presente tr&aacute;mite.
                        </p>
                        <p>
                            El Ciudadano Responsable declara que se encuentra en conocimiento que la presente solicitud de Declaraci&oacute;n Responsable se realiza en la primera fase de la Etapa de Implementaci&oacute;n de la Ley Marco de Regulaci&oacute;n de Actividades Econ&oacute;micas y sujeta al cumplimiento de la segunda fase a partir del 4 de marzo de 2019. Cumplido el plazo citado deber&aacute; presentar dentro de los treinta (30) d&iacute;as h&aacute;biles en el sistema inform&aacute;tico la documentaci&oacute;n correspondiente.
                        </p>
                        <p>
                            El falseamiento de la informaci&oacute;n presentada en la presente declaraci&oacute;n jurada implica la revocaci&oacute;n de la autorizaci&oacute;n de la actividad econ&oacute;mica, la configuraci&oacute;n de la falta prevista en el art&iacute;culo 4.1.1.4 del Cap&iacute;tulo I, Secci&oacute;n 4°, del Libro II del Anexo A del R&eacute;gimen de Faltas de la Ciudad de Buenos Aires (Ley N° 451) y dem&aacute;s acciones legales que pudieren corresponder.
                        </p>
                    </div>
                </div>
                </div> 
                        

   
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>

    <%--Modal Confirmar Solicitud--%>
    <div id="frmConfirmarSolicitud2" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top: -8px">Datos de la Solicitud</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="udpConfirmarSolcitud" runat="server">
                        <ContentTemplate>
                            <div class="form-horizontal pright10">
                                <div class="form-group">
                                    <label class="control-label col-sm-5" style="font-weight: Bold !important;">Solicitud de trámite nro:</label>
                                    <asp:Label ID="nroSolicitudModal" CssClass="col-sm-4" runat="server" Font-Bold="true" Style="color: #337ab7; margin-top: 5px; font-size: 18px"></asp:Label>
                                </div>
                            </div>
                            <div class="form-horizontal pright10">
                                <div class="form-group">
                                    <label class="control-label col-sm-5" style="font-weight: Bold !important;">Código de Seguridad:</label>
                                    <asp:Label ID="lblCodSeguridad" CssClass="col-sm-4" runat="server" Font-Bold="true" Style="color: #337ab7; margin-top: 5px; font-size: 18px"></asp:Label>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer mleft20 mright20">

                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>

                            <div class="form-inline">
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary btn-lg" OnClientClick="return hideModalConfirmarSolicitud(); ">
                                   <span class="text">Continuar</span> 
                                   <i class="imoon imoon-chevron-right"></i>                               
                                </asp:LinkButton>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <%--Modal Confirmar Solicitud--%>
    <div id="frmConfirmarSolicitud1" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top: -8px">Confirmar Solicitud</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="udpConfirmarSolcitud1" runat="server">
                        <ContentTemplate>

                            <div class="form-horizontal pright10">
                                <div class="form-group">
                                    <p class="col-sm-12">¿Está seguro de que desea confirmar la solicitud?</p>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer mleft20 mright20">

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updConfirmarAnular">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div class="form-group">
                                    <asp:LinkButton ID="Button2" runat="server" CssClass="btn btn-primary" Text="Sí" OnClientClick="return hidefrmConfirmarSolicitud();frmConfirmarSolicitud1" OnClick="btnConfirmarTramite_Click"></asp:LinkButton>
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

    <%--Modal Confirmar Anulación--%>
    <div id="frmConfirmarAnulacion1" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content" style="width: 725px; margin-left: -80px">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top: -8px">Anular tr&aacute;mite</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon-remove-circle fs64 color-blue"></i>
                            </td>
                            <td style="padding-left: 20px">
                                <asp:Label runat="server">Seleccionaste la opción de <strong>Anular</strong> la Solitud. De esta manera, <strong>toda la información ingresada perderá validez</strong></asp:Label>
                                <br />
                                <br />
                                <asp:Label runat="server"> ¿Estas seguro de que querés ANULAR esta solicitud?</asp:Label>

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
                                    <asp:Button ID="btnAnular_Si" runat="server" CssClass="btn btn-default" Text="Sí" OnClientClick="return hidefrmConfirmarAnulacion1(); showfrmConfirmarAnulacion2();" />
                                    <button type="button" class="btn btn-primary" data-dismiss="modal">No</button>
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

    <div id="frmConfirmarAnulacion2" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content" style="width: 780px; margin-left: -110px">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top: -8px">Anular tr&aacute;mite</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon-remove-circle fs64 color-blue"></i>
                            </td>
                            <td style="vertical-align: middle; padding-left: 20px">
                                <asp:Label runat="server">Estás a punto de <strong>Anular la Solicitud, haciendo que toda la información ingresada pierda validez.</strong> <br /> ¿Estás seguro?</asp:Label>
                            </td>
                        </tr>
                    </table>

                </div>
                <div class="modal-footer mleft20 mright20">

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>

                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-default" Text="Sí" OnClick="btnAnularTramite_Click" />
                                    <button type="button" class="btn btn-primary" data-dismiss="modal">No</button>
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

    <%--modal de Errores--%>
    <div id="frmError" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top: -8px">Error</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="udpError" runat="server">
                        <ContentTemplate>
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer mleft20 mright20">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- /.modal -->

    <script type="text/javascript">
        function showfrmConfirmarTramite() {
            $("#frmConfirmarSolicitud1").modal("show");

            return false;
        }

        function hidefrmConfirmarSolicitud() {
            $("#frmConfirmarSolicitud1").modal("hide");
            ocultarShortcuts();
        }
        function hidefrmConfirmarAnulacion1() {
            $("#frmConfirmarAnulacion1").modal("hide");
            $("#frmConfirmarAnulacion2").modal("show");

        }
        function showModalConfirmarSolicitud() {

            $("#pnlProcesando").hide();
            //$("#frmConfirmarSolicitud2").modal("show");
            return false;
        }

        function showfrmError() {
            $("#frmError").modal("show");
            $("#pnlProcesando").hide();
            $("#pnlShortcuts").show();
            return false;
        }

        function validarGuardar() {

            var ret = true;


            var formatoCUIT = /\b(20|23|24|27|30|33|34)(\D)?[0-9]{8}(\D)?[0-9]/;
            var formatoNum = /^\d+$/;
            var formatoSuper = /^[0-9]+(,[0-9]+)?$/;

            $("#Req_Titu").hide();
            $("#Req_CuitPF").hide();
            $("#ValCantidad_Cuit").hide();
            $("#ValFormato_Cuit").hide();
            $("#Req_Profe").hide();
            $("#Req_Matri").hide();
            $("#ValFormato_PH").hide();
            $("#Req_PartidaH").hide();
            $("#ValFormato_PM").hide();
            $("#Req_PartidaM").hide();
            $("#Req_Calle").hide();
            $("#Req_Altura").hide();
            $("#ValFormato_Altura").hide();
            $("#Req_Piso").hide();
            $("#Req_UF").hide();
            $("#Req_Actividad").hide();
            $("#Req_Zona").hide();
            $("#Req_Superficie2").hide();
            $("#ValFormato_Superficie").hide();



            if ($.trim($("#<%: txtTitulares.ClientID %>").val()).length == 0) {
                $("#Req_Titu").css("display", "inline");
                ret = false;
            }


            if ($.trim($("#<%: txtCuit .ClientID %>").val()).length == 0) {
                $("#Req_CuitPF").css("display", "inline-block");
                ret = false;
            }
            else if ($.trim($("#<%: txtCuit.ClientID %>").val()).length < 11) {
                $("#ValCantidad_Cuit").css("display", "inline-block");
                ret = false;

            }
            else {
                if (!formatoNum.test($.trim($("#<%: txtCuit.ClientID %>").val()))) {
                    $("#ValFormato_Cuit").css("display", "inline-block");
                    ret = false;
                }
            }

        if ($.trim($("#<%: txtProfesional.ClientID %>").val()).length == 0) {
                $("#Req_Profe").css("display", "inline-block");
                ret = false;
            }

            if ($.trim($("#<%: txtMatricula.ClientID %>").val()).length == 0) {
                $("#Req_Matri").css("display", "inline-block");
                ret = false;
            }

            if ($.trim($("#<%: txtPartidaH .ClientID %>").val()).length == 0) {
                $("#Req_PartidaH").css("display", "inline-block");
                ret = false;
            }
            else {
                if (!formatoNum.test($.trim($("#<%: txtPartidaH.ClientID %>").val()))) {
                    $("#ValFormato_PH").css("display", "inline-block");
                    ret = false;
                }
            }

            if ($.trim($("#<%: txtPartidaM .ClientID %>").val()).length == 0) {
                $("#Req_PartidaM").css("display", "inline-block");
                ret = false;
            }
            else {
                if (!formatoNum.test($.trim($("#<%: txtPartidaM.ClientID %>").val()))) {
                    $("#ValFormato_PM").css("display", "inline-block");
                    ret = false;
                }
            }

            if ($.trim($("#<%: txtCalle .ClientID %>").val()).length == 0) {
                $("#Req_Calle").css("display", "inline-block");
                ret = false;
            }

            if ($.trim($("#<%: txtAltura .ClientID %>").val()).length == 0 || $("#<%: txtAltura .ClientID %>").val() == 0) {
                $("#Req_Altura").css("display", "inline-block");
                ret = false;
            }
            else {
                if (!formatoNum.test($.trim($("#<%: txtAltura.ClientID %>").val()))) {
                    $("#ValFormato_Altura").css("display", "inline-block");
                    ret = false;
                }
            }

            if ($.trim($("#<%: txtPiso .ClientID %>").val()).length == 0) {
                $("#Req_Piso").css("display", "inline-block");
                ret = false;
            }

            if ($.trim($("#<%: txtUnidad .ClientID %>").val()).length == 0) {
                $("#Req_UF").css("display", "inline-block");
                ret = false;
            }



            if ($.trim($("#<%: txtActividad .ClientID %>").val()).length == 0) {
                $("#Req_Actividad").css("display", "inline-block");
                ret = false;
            }

            if ($.trim($("#<%: ddlZonaMixtura.ClientID %>").val()).length == 0 || $("#<%: ddlZonaMixtura.ClientID %>").val() == 0) {
                $("#Req_Zona").css("display", "inline-block");
                ret = false;
            }

            if ($.trim($("#<%: txtSuperficie.ClientID %>").val()).length == 0 || $("#<%: txtSuperficie.ClientID %>").val() == 0) {
                $("#Req_Superficie2").css("display", "inline-block");
                ret = false;
            }
            else {
                if (!formatoSuper.test($.trim($("#<%: txtSuperficie.ClientID %>").val()))) {
                    $("#ValFormato_Superficie").css("display", "inline-block");
                    ret = false;
                }
            }


            if (ret) {
                showfrmConfirmarTramite();
            }

            return ret;

        }

        function hideModalConfirmarSolicitud() {
            $("#frmConfirmarSolicitud2").modal("hide");
            return false;
        }

        function showfrmConfirmarAnulacion1() {
            $("#frmConfirmarAnulacion1").modal("show");
            return false;
        }

        function hidefrmConfirmarAnulacion2() {
            $("#frmConfirmarAnulacion2").modal("hide");
            return false;
        }
    </script>
</asp:Content>



