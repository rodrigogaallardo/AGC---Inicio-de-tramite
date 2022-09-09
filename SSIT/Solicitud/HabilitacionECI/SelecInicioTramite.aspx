<%@ Page Title="Inicio de Ampliación" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SelecInicioTramite.aspx.cs" Inherits="SSIT.Solicitud.HabilitacionECI.SelecInicioTramite" %>
<%@ Register Src="~/Solicitud/Controls/SeleccionTramitesAprobados.ascx" TagPrefix="uc1" TagName="SeleccionTramitesAprobados" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <%: Scripts.Render("~/bundles/autoNumeric") %>
    <%: Scripts.Render("~/bundles/select2") %>
    <%: Styles.Render("~/bundles/select2Css") %>

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
            <asp:HiddenField ID="hid_id_solicitud" runat="server" />
            <asp:HiddenField ID="hid_return_url" runat="server" />
            <asp:HiddenField ID="hid_CargosFirPJ" runat="server" />
            <asp:HiddenField ID="hid_CargosFirSH" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="page_content" Style="display:none">
        
        <div id="pnlBusquedaTramites">
            <div class="row">
                <div class="mleft10 col-md-8">
                    <h2><asp:Label ID="lblTitulo" runat="server" Text="Label">Seleccione el tipo de tr&aacute;mite</asp:Label></h2>
                </div>
            
            </div>
            <div class="row">
                <div class="view view-shortcuts view-id-shortcuts pright30" style="display: flex !important; justify-content: center !important;">
                    <asp:LinkButton ID="lnkNuevaECI" CssClass="col-md-3 col-sm-6 shortcut" runat="server" OnClick="lnkNuevaECI_Click">
                            <span class="bg-blue-lt">
                                    <span class="glyphicon imoon-file fs48"></span>
                                </span>
                            <h4 style="text-transform:uppercase">Tramite Nuevo (ECI)</h4>
                    </asp:LinkButton>

                    <asp:LinkButton ID="lnkCrearECI" CssClass="col-md-3 col-sm-6 shortcut" runat="server" OnClick="lnkCrearECI_Click">
                            <span class="bg-violet-lt">
                                    <span class="glyphicon imoon-file fs48"></span>
                                </span>
                            <h4 style="text-transform:uppercase">Adecuacion (ECI)</h4>
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>

    <%--Confirmacion de nueva ampliacion--%>
    <div id="frmConfirmarNuevaAmpliacion" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title"style="margin-top:-8px">Aviso</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon imoon-info fs64 color-blue"></i>
                            </td>
                            <td style="vertical-align: middle">
                                <p class="mleft10">
                            Vas solicitar el inicio  un trámite de <strong><%= HabilitacionECI %></strong>.A continuaci&oacute;n se te asignar&aacute; un n&uacute;mero de identificador
                            de tu solicitud, con el que vas a poder continuar/consultar - en cualquier momento - a trav&eacute;s de la opci&oacute;n "Mis Solicitudes".
                                    
                                </p>                                
                                <p style="text-decoration: underline">
                                    POR FAVOR, RECORDA NO GENERAR MAS DE UNA SOLICITUD PARA UN MISMO LUGAR Y UN MISMO TITULAR, YA QUE LA ANTERIOR SERA ANULADA EN FORMA AUTOMATICA.
                                </p>
                            </td>
                        </tr>
                    </table>

                </div>
                <div class="modal-footer mleft20 mright20">

                    <asp:UpdatePanel ID="updConfirmarNuevaAmpliacion" runat="server">
                        <ContentTemplate>

                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="updConfirmarNuevaAmpliacion">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />Procesando...
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div id="pnlBotonesConfirmarNuevaAmpliacion" class="form-group">
                                    <asp:LinkButton ID="btnNuevaAmpliacion" runat="server" CssClass="btn btn-primary" OnClick="btnNuevaAmpliacion_Click"
                                        OnClientClick="ocultarBotonesConfirmarNuevaAmpliacion();" >
                                    <i class="imoon imoon-ok"></i>
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
    </div>

    <div id="frmExpSeg" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title"style="margin-top:-8px">Aviso</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon imoon-info fs64 color-blue"></i>
                            </td>
                            <td style="vertical-align: middle">
                                <p class="mleft10">
                                Por favor seleccione si posee el número de expediente o el número de la solicitud y el código de seguridad de <strong><%= HabilitacionECI %></strong>
                                    
                                </p>                                
                                <p style="text-decoration: underline">
                                POR FAVOR, RECORDA NO GENERAR MAS DE UNA SOLICITUD PARA UN MISMO LUGAR Y UN MISMO TITULAR, YA QUE LA ANTERIOR SERA ANULADA EN FORMA AUTOMATICA.
                                </p>
                            </td>
                        </tr>
                    </table>

                </div>
                <div class="modal-footer mright20" style="align-content:center;">
                    <asp:UpdatePanel ID="updConExpSol" runat="server">
                        <ContentTemplate>

                            <div class="form-inline">
                                <div class="form-group">
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updConExpSol">
                                        <ProgressTemplate>
                                            <img src="<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>" />Procesando...
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <div id="pnlBotonesConfirmarExpSeg" class="form-group">
                                    <asp:LinkButton ID="lnkPoseeExp" runat="server" CssClass="btn btn-primary" PostBackUrl="~/Solicitud/HabilitacionECI/InicioTramite.aspx" OnClientClick="ocultarBotonesfrmExpSeg();" >
                                    <i class="imoon imoon-ok"></i>
                                        <span class="text">Solicitud Aprobada</span>
                                    </asp:LinkButton>

                                    <asp:LinkButton ID="lnkPoseeSolicitud" runat="server" CssClass="btn btn-primary" PostBackUrl="~/Solicitud/HabilitacionECI/InicioTramiteNroSol.aspx"  OnClientClick="ocultarBotonesfrmExpSeg();" >
                                    <i class="imoon imoon-ok"></i>
                                        <span class="text">Solicitud en tramite</span>
                                    </asp:LinkButton>

                                    <asp:LinkButton ID="lnkPoseePapel" runat="server" CssClass="btn btn-primary" PostBackUrl="~/Solicitud/HabilitacionECI/InicioTramitePapel.aspx"  OnClientClick="ocultarBotonesfrmExpSeg();" >
                                    <i class="imoon imoon-ok"></i>
                                        <span class="text">Solicitud en Papel</span>
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


    <%--Modal mensajes de error--%>
    <div id="frmError" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top:-8px">Atención</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <asp:label runat="server" class="imoon imoon-info fs64" style="color: #377bb5"></asp:label>
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

        $(document).ready(function () {

            $("#page_content").hide();
            $("#Loading").show();
            toolTips();
            
            init_Js_updDatos();
            //Carga de controles
            finalizarCarga();
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
        function showfrmError() {

            $("#frmError").modal("show");
            return false;
        }

        function init_Js_updDatos()
        {
           
            return true;
        }

        function showfrmConfirmarNuevaAmpliacion() {
            $("#frmConfirmarNuevaAmpliacion").modal("show");
            return false;
        }

        function ocultarBotonesConfirmarNuevaAmpliacion() {
            $("#pnlBotonesConfirmarNuevaAmpliacion").hide
            return false;
        }


        function showfrmExpSeg() {
            $("#frmExpSeg").modal("show");
            return false;
        }

        function ocultarBotonesfrmExpSeg() {
            $("#pnlBotonesConfirmarExpSeg").hide();
            return false;
        }
    </script>
</asp:Content>
