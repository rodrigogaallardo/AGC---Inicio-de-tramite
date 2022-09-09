<%@ Page Title="AT - Inicio Trámite" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InicioTramite.aspx.cs" Inherits="AnexoProfesionales.Tramites.Habilitacion.InicioTramite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
    <%: Scripts.Render("~/bundles/autoNumeric") %>


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
   
    <div id="page_content" Style="display:none">

        <%--Paneles con datos de la solicitud--%>
        <asp:UpdatePanel ID="updCargarDatos" runat="server" RenderMode="Inline" UpdateMode="Conditional">
            <ContentTemplate>

                <asp:Button ID="btnCargarDatostramite" runat="server" Style="display: none" OnClick="btnCargarDatostramite_Click" />

                <asp:Panel ID="pnlTramite" runat="server" >
                    
                        <h2>Nuevo Anexo T&eacute;cnico</h2>
                            <hr />
                        <p>
                            Para iniciar un Anexo T&eacute;cnico a partir de una Solicitud debe ingresar los siguientes datos,
                            
                            confirmados los Datos de la Solicitud en el SSIT, se obtendrá el Código de Seguridad correspondiente.
                            
                        </p>
                    <br />

                      <div class="box-panel" style="background:#FFFFFF">
                            <div style="margin:20px; margin-top:-5px"> 
                            <div style="color:#377bb5">                                 
                                        <h4><i class="imoon imoon-file" style="margin-right:10px"></i>Datos de la solicitud</h4>    
                                        <hr />                   
                                    </div>
                              </div>

                          <div class="form-horizontal pright10">
                           <div class="form-group">
                  
                                    <label class="control-label col-sm-5"><b>Nro. de trámite de la Solicitud:</b></label>
                                    <div class="col-sm-7">
                                        <asp:TextBox ID="txtNroSolicitud" runat="server" MaxLength="7" Width="80px" TabIndex="1" CssClass="form-control"></asp:TextBox>
                                    </div>
                                  <div style="margin-left:270px">
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNroSolicitud" Display="Dynamic"
                                        ErrorMessage="El Nro. de Solicitud es requerido." CssClass="alert alert-small alert-danger mbottom0 mtop5" ValidationGroup="Continuar"></asp:RequiredFieldValidator>
                              
                                    </div> 
                                   
                                    </div>     
                            
                           <div class="form-group">
                  
                              <label class="control-label col-sm-5"><b>Código de Seguridad de la Solicitud:</b></label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtCodigoSeguridad" runat="server" MaxLength="4" Width="80px" style="text-transform:uppercase" TabIndex="2" CssClass="form-control"></asp:TextBox>
                                    </div>  

                                    <asp:UpdatePanel ID="updContinuar" runat="server" RenderMode="Inline">
                                      <ContentTemplate>

                                          <div>
                                              <asp:LinkButton ID="lnkContinuar" runat="server" CssClass="btn btn-primary btn-lg" Text="Continuar"
                                                  ValidationGroup="Continuar" OnClick="lnkContinuar_Click" OnClientClick=" return validar();">
                                                     <span class="text">Continuar</span>
                                                     <i class="imoon imoon-chevron-right"></i>
                                                   
                                              </asp:LinkButton>
                                          </div>

                                          <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="updContinuar" runat="server"
                                              DisplayAfter="0">
                                              <ProgressTemplate>
                                                  <table border="0">
                                                      <tr>
                                                          <td>
                                                              <asp:Image ID="impProgress" runat="server" ImageUrl="~/Content/img/app/Loading24x24.gif" /></td>
                                                          <td>Generando Anexo Tecnico...</td>
                                                      </tr>
                                                  </table>
                                              </ProgressTemplate>
                                          </asp:UpdateProgress>

                                      </ContentTemplate>
                                  </asp:UpdatePanel>
                                  <div class="col-sm-7" style="margin-left:225px">
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCodigoSeguridad"
                                        Display="Dynamic" ErrorMessage="El Código de Seguridad es requerido." CssClass="alert alert-small alert-danger mbottom0 mtop5"
                                        ValidationGroup="Continuar"></asp:RequiredFieldValidator>
                                    </div>
                              </div>
                             

                          </div>
                 
                     </div>  
                        
                </asp:Panel>    

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
                                <i class="imoon imoon-info fs64 color-blue"></i>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="updmpeInfo" runat="server" >
                                    <ContentTemplate>
                                        <asp:Label ID="lblError" runat="server" Style="color: #000000"></asp:Label>
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
    
    <%--modal de Informacion--%>
    <div id="frmInfor" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title" style="margin-top:-8px">Información</h4>
                </div>
                <div class="modal-body">
                    <table style="border-collapse: separate; border-spacing: 5px">
                        <tr>
                            <td style="text-align: center; vertical-align: text-top">
                                <i class="imoon imoon-info fs64 color-blue"></i>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="updmpeInfor" runat="server" class="form-group">
                                    <ContentTemplate>
                                        <asp:Label ID="lblInfor" runat="server" Style="color: Black"></asp:Label>
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
            $("#<%: btnCargarDatostramite.ClientID %> ").click();

        });

        function finalizarCarga() {
            $("#Loading").hide();
            $("#page_content").slideDown("slow");

            $('#<%: txtNroSolicitud.ClientID %>').autoNumeric({ aSep: '', aDec: '.', mDec: '0',vMax: '9999999'});
            return false;
        }

        function showfrmError() {
            $("#frmError").modal("show");
            return false;
        }

        function showfrmInfor() {
            $("#frmInfor").modal("show");
            return false;
        }

        function validar() {
            var aux = Page_ClientValidate("Continuar");
            if (aux) {
                $('#<%: lnkContinuar.ClientID %>').hide();
                return true;
            }
        
        }
    </script>
</asp:Content>
