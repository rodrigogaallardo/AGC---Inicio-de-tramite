<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SeleccionTramitesAprobados.ascx.cs" Inherits="SSIT.Solicitud.Controls.SeleccionTramitesAprobados" %>

<asp:UpdatePanel ID="updSeleccionTramitesAprobados" runat="server">
    <ContentTemplate>

        <div id="box_datos" class="box-panel">

    <div style="margin: 20px; margin-top: -5px">
        <div style="color: #377bb5">
            <h4><i class="imoon imoon-edit" style="margin-right: 10px"></i>Tr&aacute;mites encontrados</h4>
            <hr />
        </div>
    </div>
        
    <asp:GridView ID="grdTramites" runat="server" AutoGenerateColumns="false" ShowHeader="false" DataKeyNames="IdSolicitud,IdTipoTramite"
        AllowPaging="false" Style="border: none;" GridLines="None" Width="100%" CssClass="table table-bordered mtop5"
        OnRowDataBound="grdTramites_RowDataBound" ItemType="DataTransferObject.SolicitudesAprobadasDTO" OnSelectedIndexChanged="grdTramites_SelectedIndexChanged">
        <SelectedRowStyle BackColor="#fcda59" />
        <Columns>
            
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnSeleccionar" runat="server" OnClick="btnSeleccionar_Click" CssClass="btn btn-default" Text="Seleccionar" />
                </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:ButtonField Text="Seleccionar" CommandName="Select" ItemStyle-CssClass="btn btn-default"/>--%>
            
            <asp:TemplateField ItemStyle-CssClass="col-sm-10">
                <ItemTemplate>
                        

                        <div class="row ptop10">
                            <div class="col-sm-6">
                                <ul class="cabecera">
                                    <li>Solicitud:<strong><asp:Label ID="lblSolicitud" runat="server"></asp:Label></strong>
                                    </li>
                                    <li>Tipo de Tr&aacute;mite:<strong><asp:Label ID="lblTipoTramite" runat="server"></asp:Label></strong>
                                    </li>
                                    <li>Estado:<strong><asp:Label ID="lblEstado" runat="server"></asp:Label></strong>
                                    </li>
                                    <li>Anexo T&eacute;cnico:<strong><asp:Label ID="lblEncomienda" runat="server"></asp:Label></strong>
                                    </li>
                                </ul>

                            </div>
                            <div class="col-sm-6">
                                <ul class="cabecera" style="padding-left:10px">
                                    <li>
                                        <div class="row">
                                        <div class="col-sm-3 " style="width:70px;padding-left:10px !important">Ubicación:</div>
                                            <div class="col-sm-9" style="padding-left:10px !important">
                                                <strong><asp:Label ID="lblUbicacion" runat="server" ></asp:Label></strong>
                                            </div>
                                        </div>
                                    </li>
                                    <li>Superficie total:<strong><asp:Label ID="lblSuperficieTotal" runat="server"></asp:Label></strong>
                                    </li>
                                    <li>Expediente:<strong><asp:Label ID="lblExpediente" runat="server"></asp:Label></strong>
                                    </li>
                                    <li>Titular/es:<strong><asp:Label ID="lblTitulares" runat="server"></asp:Label></strong>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>

                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <div class="col-sm-1">
                <i class="imoon imoon-info2 fs64 color-gray"></i>
            </div>
            <div class="col-sm-11">
                <span>
                    Con los datos ingresados no encontramos registros de una habilitaci&oacute;n comercial vigente en nuestras bases. <br />
                    Por favor, verifique si los datos cargados son los correctos. Para modificar los datos ingresados, presione el botón “Volver a la b&uacute;squeda”. <br />
                    Si presiona “Continuar” podrá agregar más informaci&oacute;n acerca de la habilitaci&oacute;n. 
                </span>
            </div>
        </EmptyDataTemplate>
    </asp:GridView>
    
    <asp:Panel ID="pnlAvisoVariosTramites" runat="server" CssClass="alert alert-info" Visible="false">
        Debe seleccionar seg&uacute;n su criterio el trámite de Habilitación previa adecuado.
    </asp:Panel>

</div>

    </ContentTemplate>
</asp:UpdatePanel>
