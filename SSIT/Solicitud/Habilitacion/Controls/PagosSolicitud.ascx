<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PagosSolicitud.ascx.cs" Inherits="SSIT.Solicitud.Habilitacion.Controls.PagosSolicitud" %>

<%@ Register Src="~/Solicitud/Habilitacion/Controls/Pagos.ascx" TagPrefix="uc" TagName="Pagos" %>

<%-- collapsible Pagos--%>
<div id="box_titulares" class="accordion-group widget-box" style="background-color: #ffffff">
    <div class="accordion-heading">
        <a id="titulares_btnUpDown" data-parent="#collapse-group" href="#collapse_pagos"
            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

            <div class="widget-title">
                <span class="icon"><i class="imoon imoon-dollar" style="color: #344882"></i></span>
                <h5>
                    <asp:Label ID="lbl_titulares_tituloControl" runat="server" Text="Pagos - Boletas"></asp:Label></h5>
                <span class="btn-right"><i class="imoon imoon-chevron-up" style="color: #344882"></i></span>
            </div>
        </a>
    </div>
    <div class="accordion-body collapse in" id="collapse_pagos">
        <div style="margin: 10px">

            <asp:UpdatePanel runat="server" ID="udpLabelProteatro" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Panel ID="pnlMsjProTeatro" runat="server" CssClass="alert alert-success" Style="display: none">
                        <asp:Label ID="lblMsjProTeatro" runat="server" Text="Si Usted cuenta con certificado Proteatro deberá adjuntar el mismo y no generar las BUI (Boleta Única Inteligente) para habilitaciones. El certificado Proteatro lo exime de los pagos de los timbrados."></asp:Label>
                        <br />
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="updBoxPagos" runat="server">
                <ContentTemplate>

                    <asp:Panel ID="pnlPagos" runat="server" CssClass="mleft10">
                        
                        <asp:Panel ID="pnlAGC" runat="server">
                            <strong>Boletas generadas para AGC</strong>
                            <uc:Pagos runat="server" ID="vis_Pagos_AGC" OnErrorClick="PagosError_Click"  />
                        </asp:Panel>

                        <strong>Boletas generadas para APRA</strong>
                        <uc:Pagos runat="server" ID="vis_Pagos_APRA" OnErrorClick="PagosError_Click" />

                    </asp:Panel>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</div>
<script type="text/javascript">

    function ocultarLabelProTeatro() {
        $("#<%: pnlMsjProTeatro.ClientID %>").css("display", "none");
    }
    function mostrarLabelProTeatro() {

        $("#<%: pnlMsjProTeatro.ClientID %>").css("display", "block");

    }
</script>
