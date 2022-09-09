<%@ Page Title="Inicio de Ampliación" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InicioTramiteNroSol.aspx.cs" Inherits="SSIT.Solicitud.HabilitacionECI.InicioTramiteNroSol" %>

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
            <asp:Button ID="btnCargarDatos" runat="server" OnClick="btnCargarDatos_Click" Style="display: none" />
            <asp:HiddenField ID="hid_id_solicitud" runat="server" />
            <asp:HiddenField ID="hid_return_url" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--fin ajax cargando--%>

    <div id="page_content" Style="display:none">
        

        <div id="pnlBusquedaTramites">
            <%--Titulo--%>
            <div class="row">
                <div class="mleft10 col-md-8">
                    <h2><asp:Label ID="lblTitulo" runat="server" Text="Label">Ingreso de datos del tr&aacute;mite</asp:Label></h2>
                </div>
            
            </div>

            <hr />

            <div class="row">

                <div class="col-sm-1 mtop10" style="width: 25px">
                    <i class="imoon imoon-info fs24" style="color: #377bb5"></i>
                </div>
                <div class="col-sm-11">

                    <p class="pad10">
                        En este primer paso, deber&aacute; ingresar el número de la solicitud y la clave que posea la habilitaci&oacute;n vigente que usted desea ampliar.
                    </p>
                    <ul style="line-height: 20px">
                        <li><b>Número de Solicitud:</b>
                            <div>
                               Deberá ingresar el número de la solicitud en tramite
                            </div>
                        </li>
                        <li><b>Código de seguridad:</b>
                            <div>
                                Debera ingresar el código de seguridad indicado en la solicitud en tramite
                            </div>
                        </li>
                    </ul>

                    <div class="alert alert-info">
                        Recuerde que solo se validarán los datos de aquellos trámites cuyos Anexos Técnicos / Encomiendas hayan sido confeccionados digitalmente.
                    </div>
                </div>
            </div>
        
            <div id="box_datos" class="box-panel">

                <div style="margin: 20px; margin-top: -5px">
                    <div style="color: #377bb5">
                        <h4><i class="imoon imoon-edit" style="margin-right: 10px"></i>Ingres&aacute; y valid&aacute; tus datos</h4>
                        <hr />
                    </div>
                </div>

                <asp:UpdatePanel ID="updDatos" runat="server" >
                    <ContentTemplate>
                    
                        <asp:Panel ID="pnlBuscarDatos" runat="server" DefaultButton="btnValidar">

                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Nro. Solicitud:</label>
                                    <div class="col-sm-10 form-inline">
                                        <asp:TextBox ID="txtSolicitud_Nro" runat="server" Width="100px" CssClass="mleft5 form-control" placeholder="Número"></asp:TextBox>
                                        <div id="Req_Solicitud_Nro" class="field-validation-error" style="display: none;">
                                            Debe ingresar el número de las solicitud. 
                                        </div>
                                    
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Código de seguridad:</label>
                                    <div class="col-sm-10 form-inline">
                                        <asp:TextBox ID="txtCodigoSeg" runat="server" Width="100px" CssClass="form-control" MaxLength="4"></asp:TextBox>
                                        <div id="Req_CodigoSeg" class="field-validation-error" style="display: none;">
                                            Debe ingresar el código de seguridad.
                                        </div>
                                    </div>
                                </div>

                            </div>
                     

                            <asp:UpdatePanel ID="updBotonesValidar" runat="server" >
                                <ContentTemplate>

                                    <div id="pnlBotonesValidar" class="form-inline text-right" >

                                        <div class="form-group">
                                            <asp:UpdateProgress ID="UpdateProgress9" runat="server" DisplayAfter="200" AssociatedUpdatePanelID="updBotonesValidar">
                                                <ProgressTemplate>
                                                    <img src='<%: ResolveUrl("~/Content/img/app/Loading24x24.gif") %>' style="margin-left: 10px" alt="loading" />Validando...
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>
                                        <div class="form-group">
                      
                                            <asp:LinkButton ID="btnValidar" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnValidar_Click" OnClientClick="return validarBuscarExp();">
                                                <i class="imoon imoon-check"></i>
                                                <span class="text">Validar</span>
                                            </asp:LinkButton>

                                        </div>
                        
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </asp:Panel>

                    </ContentTemplate>
                </asp:UpdatePanel>
            

            </div>

            
        </div>
    </div>

<%--    <div id="pnlResultadoBusqueda" style="display:none">
         <asp:UpdatePanel ID="updTramitesEncontrados" runat="server" >
            <ContentTemplate>

                <div class="row ptop20">
                    <div class="col-sm-6">
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-default btn-lg" OnClientClick="return showfrmBuscarTramites();">
                            <i class="imoon imoon-search"></i>
                            <span class="text">Volver a la b&uacute;queda</span>
                        </asp:LinkButton>
                    </div>
                    <div class="col-sm-6 text-right">
                        <asp:LinkButton ID="btnConfirmar" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnConfirmar_Click" >
                            <i class="imoon imoon-checkmark"></i>
                            <span class="text">Confirmar</span>
                        </asp:LinkButton>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>--%>

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
                                    Vas solicitar la adecuación de un trámite a <strong><%= HabilitacionECI %></strong>. A continuación, se te asignará un número de identificador de tu solicitud,
                                    con el que vas a poder continuar/consultar - en cualquier momento - a través de la opción "Mis Solicitudes". Si cuando esta adecuación sea presentada la solicitud 
                                    anterior aún se encuentra “en trámite” se dará de baja automáticamente.
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
    <!-- /.modal -->


    <script type="text/javascript">

        $(document).ready(function () {

            $("#page_content").hide();
            $("#Loading").show();
            toolTips();

            init_Js_updDatos();

            $("#<%: btnCargarDatos.ClientID %>").click();


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

        function showfrmTramitesEncontrados() {
            
            $("#pnlBusquedaTramites").hide("slow", function () {
                $("#pnlResultadoBusqueda").show("slow");
            });
            
            return false;
        }

        function showfrmBuscarTramites() {

            $("#pnlResultadoBusqueda").hide("slow", function () {
                $("#pnlBusquedaTramites").show("slow");
            });

            return false;
        }

        function showfrmConfirmarNuevaAmpliacion() {
            $("#frmConfirmarNuevaAmpliacion").modal("show");
            return false;
        }

        function ocultarBotonesConfirmarNuevaAmpliacion() {
            $("#pnlBotonesConfirmarNuevaAmpliacion").hide();
            return false;
        }

        function init_Js_updDatos() {
            
            $("#<%: txtSolicitud_Nro.ClientID %>").autoNumeric({ aSep: '', mDec: '0', vMax: '99999999' });
            return false;
        }

        function validarBuscarExp() {

            var ret = true;
            $("#Req_Solicitud_Nro").hide();
            $("#Req_CodigoSeg").hide();
            
            if ($.trim($("#<%: txtSolicitud_Nro.ClientID %>").val()).length == 0)
            {
                $("#Req_Solicitud_Nro").css("display", "inline-block");
                ret = false;
            }


            if ($.trim($("#<%: txtCodigoSeg.ClientID %>").val()).length == 0) {

                $("#Req_CodigoSeg").css("display", "inline-block");
                ret = false;
            }

            if (ret) {
                $("#<%: btnValidar.ClientID %>").hide();
            }
            return ret;
        }
        


    </script>
</asp:Content>
