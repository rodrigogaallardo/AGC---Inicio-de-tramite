<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Tramite_CAA.ascx.cs" Inherits="SSIT.Solicitud.Habilitacion.Controls.Tramite_CAA" %>

<link href="<%: ResolveUrl("~/Content/css/shortcuts.css") %>" rel="stylesheet" />

<%@ Import Namespace="SSIT.Common" %>
<asp:HiddenField ID="hid_id_encomienda_in_caa" runat="server" />

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
                    <div class="text-left mtop10" visible="false" runat="server" id="DivBtnSIPSA">
                        <strong>Para tramitar su Certificado de Aptitud Ambiental inicie su solicitud por aquí:</strong>
                        <asp:LinkButton ID="btnGenerarCAA" runat="server" CssClass="btn btn-primary" PostBackUrl="~/TADRedirect.aspx" OnClientClick="window.document.forms[0].target='_blank';">
                            <i class="imoon imoon-file4"></i>
                            <span class="text">Ingresar a SIPSA</span>
                        </asp:LinkButton>
                    </div>
                    <asp:UpdatePanel ID="updBuscarCAA" runat="server" Visible="false">
                        <ContentTemplate>
                            <asp:Panel ID="pnlBuscarCAA" runat="server">
                                <div class="ptop10">
                                    <strong>Para finalizar ingrese su número de solicitud y código de seguridad:</strong>
                                </div>
                                <div class="form-horizontal ptop10">
                                    <div class="form-group">
                                        <label class="control-label col-sm-3">Ingrese el Nro. de solicitud de CAA:</label>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtNroCAA" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNroCAA" Display="Dynamic"
                                                ErrorMessage="El Nro. de Solicitud es requerido." CssClass="alert alert-small alert-danger mbottom0 mtop5" ValidationGroup="BuscarCAA"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="control-label col-sm-3">Inrese el C&oacute;digo de Seguridad:</label>
                                        <div class="col-sm-9">
                                            <div class="form-inline">
                                                <asp:TextBox ID="txtCodSeguridadCAA" runat="server" CssClass="form-control" Width="100px" Style="text-transform: uppercase" MaxLength="4"></asp:TextBox>
                                                <asp:LinkButton ID="btnBuscarCAA" runat="server" CssClass="btn btn-primary ptop5" OnClick="btnBuscarCAA_Click" ValidationGroup="BuscarCAA" OnClientClick="deshabilitar(this.id)">
                                                    <i class="imoon imoon-search"></i>
                                                    <span class="text">Buscar</span>
                                                </asp:LinkButton>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCodSeguridadCAA" Display="Dynamic"
                                                ErrorMessage="El código de seguridad es requerido." CssClass="alert alert-small alert-danger mbottom0 mtop5" ValidationGroup="BuscarCAA"></asp:RequiredFieldValidator>

                                            <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="updBuscarCAA" runat="server"
                                                DisplayAfter="0">
                                                <ProgressTemplate>
                                                    <asp:Image ID="impProgress" runat="server" ImageUrl="~/Content/img/app/Loading24x24.gif" CssClass="mtop5" />
                                                    <span>Buscando y Comparando datos...</span>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <asp:Panel ID="pnlErrorBuscarCAA" runat="server" CssClass="alert alert-danger mtop10" Visible="false">
                                                <asp:Label ID="lblErrorBuscarCAA" runat="server"></asp:Label>
                                                <asp:BulletedList ID="lstMensajesCAA" runat="server" CssClass="mtop10">
                                                </asp:BulletedList>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                      <div class="text-left mtop10" visible="false" runat="server" id="DivBtnSIPSAExpress">
                        <strong>Para tramitar su Certificado de Aptitud Ambiental inicie su solicitud por aquí:</strong>
                        <asp:LinkButton ID="linkBtnGenerarCAA" runat="server" CssClass="btn btn-primary" OnClick="linkBtnGenerarCAA_Click" 
                            OnClientClick="showLoadingGifAndDisableButton('loadingDiv'); deshabilitar(this.id);">
                            <i class="imoon imoon-file4"></i>
                            <span class="text">Generar CAA</span>
                        </asp:LinkButton>
                          <div id="loadingDiv" style="display: none;">
                              <img src="~/Content/img/app/Loading24x24.gif" alt="Loading..." CssClass="mtop5" ID="generandoCAAgif" runat="server"/>
                          </div>
                    </div>
                    <div style="padding: 0px 10px 10px 10px; width: auto">
                        <div class="mtop10">
                            <strong>Listado de Certificados de Aptitud Ambiental</strong>
                        </div>
                        <asp:GridView ID="grdArchivosCAA" runat="server" AutoGenerateColumns="false"
                            GridLines="none" CssClass="table table-bordered mtop5">
                            <Columns>
                                <asp:BoundField DataField="id_solicitud" HeaderText="Nº Solicitud" ItemStyle-Width="80px" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                                <asp:BoundField DataField="id_caa" HeaderText="Nº CAA" ItemStyle-Width="70px" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                                <asp:BoundField DataField="id_encomienda" HeaderText="Anexo" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="80px" />
                                <asp:BoundField DataField="nombre_tipocertificado" HeaderText="Tipo Cert." ItemStyle-Width="250px" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                                <asp:BoundField DataField="estado_caa" HeaderText="Estado" ItemStyle-Width="250px" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                                <asp:BoundField DataField="CreateDate" DataFormatString="{0:d}" HeaderText="Fecha" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="90px" />
                                <asp:TemplateField ItemStyle-CssClass="text-center" HeaderText="Archivo" HeaderStyle-CssClass="text-center" ItemStyle-Width="120px">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkArchivoPdf" runat="server" CssClass="btn-link" Target="_blank" NavigateUrl='<%#  Eval("url") %>' Visible='<%#  Eval("mostrarDoc") %>'>
                                             <i class="imoon imoon-download color-blue fs24"></i>
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="mtop10">
                                    <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                                    Este tr&aacute;mite no posee CAA/s.
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
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

<script type="text/javascript">
    function showLoadingGifAndDisableButton(loadingDivId) {
        var loadingDiv = document.getElementById(loadingDivId);
        loadingDiv.style.display = 'block';  
        var linkBtnGenerarCAA = document.getElementById('<%= linkBtnGenerarCAA.ClientID %>');
        linkBtnGenerarCAA.disabled = true;  
    }
</script>




