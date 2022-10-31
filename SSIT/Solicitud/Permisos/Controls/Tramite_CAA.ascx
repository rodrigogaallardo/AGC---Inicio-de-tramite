<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tramite_CAA.ascx.cs" Inherits="SSIT.Solicitud.Permisos.Controls.Tramite_CAA" %>

<link href="<%: ResolveUrl("~/Content/css/shortcuts.css") %>" rel="stylesheet" />

<%@ Import Namespace="SSIT.Common" %>

<div id="box_titulares" class="accordion-group widget-box" style="background-color: #ffffff">
    <div class="accordion-heading">
        <a id="titulares_btnUpDown" data-parent="#collapse-group" href="#collapse_caa"
            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">
            <div class="widget-title">
                <span class="icon"><i class="imoon imoon-leaf" style="color: #344882"></i></span>
                <h5>
                    <asp:Label ID="lbl_titulares_tituloControl" runat="server" Text="Certificados de Aptitud Ambiental"></asp:Label></h5>
                <span class="btn-right"><i class="imoon imoon-chevron-up" style="color: #344882"></i></span>
            </div>
        </a>
    </div>

    <div style="margin: 10px">
        <div class="accordion-body collapse in" id="collapse_caa">
            <asp:UpdatePanel ID="updTramiteCAA" runat="server">
                <ContentTemplate>

                    <asp:Panel ID="pnlDatosRAC" runat="server" Visible="false">

                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Nro. de CAA:</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtNroCAARelacionado" runat="server" CssClass="form-control" Enabled="false" Width="100px"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">ASAE Nº:</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtNroRACRelacionado" runat="server" CssClass="form-control" Enabled="false" Width="100px"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                    </asp:Panel>

                </ContentTemplate>
            </asp:UpdatePanel>


            <asp:UpdatePanel ID="updBuscarCAA" runat="server">
                <ContentTemplate>

                    <asp:Panel ID="pnlBuscarCAA" runat="server">

                        <div class="ptop10">
                            <strong>Ingrese los datos del Certificado de Aptitud Ambiental (CAA) Aprobado relacionado a la habilitaci&oacute;n:</strong>
                        </div>
                        <div class="form-horizontal ptop10">
                            <div class="form-group">
                                <label class="control-label col-sm-3">Ingrese el Nro. de solicitud de CAA:</label>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtNroCAA" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNroCAA" Display="Dynamic"
                                        ErrorMessage="El Nro. de Solicitud es requerido." CssClass="alert alert-small alert-danger mbottom0 mtop5" ValidationGroup="BuscarCAA"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="control-label col-sm-3">Ingrese el C&oacute;digo de Seguridad:</label>
                                <div class="col-sm-9">
                                    <div class="form-inline">
                                        <asp:TextBox ID="txtCodSeguridadCAA" runat="server" CssClass="form-control" Width="100px" Style="text-transform: uppercase" MaxLength="4"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </div>
</div>

<script type="text/javascript">
    function deshabilitar(boton) {
        document.getElementById(boton).style.visibility = 'hidden';
    }
</script>


