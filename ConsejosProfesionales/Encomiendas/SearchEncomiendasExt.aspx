<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchEncomiendasExt.aspx.cs"  MasterPageFile="~/Site.Master" Inherits="ConsejosProfesionales.Encomiendas.SearchEncomiendasExt" %>

 <asp:Content ContentPlaceHolderID="FeaturedContent" runat="server" ID="Featured">
                   
    </asp:Content> 

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

                 <asp:Panel ID="pnlPage" runat="server"  CssClass="PageContainer">

                   <h2 class="text-center"> <asp:Label ID="lbltituloBusq" runat="server" Text="Encomienda de Ley 257 - Búsqueda de Trámites" Font-Size="28px" ></asp:Label> </h2>
                    <hr />

                    <p class="mtop10 text-center" >
                            Desde aquí podrá consultar los trámites que un usuario ha iniciado<br />
                            Ver el estado en que se encuentran y trabajar con cada uno.
                    </p>
                     <br />
                <%--Contenido--%>
                <asp:Panel ID="pnlContenido" runat="server" CssClass="box-contenido" BackColor="White">
                
                    <asp:Panel ID="pnlInfoPaso" runat="server" Width="100%" DefaultButton="btnBuscar">
                        <div style="border-top: dotted 1px #e1e1e1;">
                        </div>

                        <div style="padding:10px" class="box-panel">

                          <div style="margin:20px; margin-top:-5px"> 
                            <div style="margin-top:5px; color:#377bb5">                                 
                                        <h4><i class="imoon imoon-search" style="margin-right:10px"></i>Panel de Búsqueda</h4> 
                                         <hr />             
                                    </div>
                                </div>
                                <table border="0" style="width:100%">
                                <tr>
                                    <td style="vertical-align:top;width:40%">

                                            <label class="mleft20">Filtros de búsqueda:</label>
                                            <div class="checkbox mleft20">
                                            <asp:RadioButton ID="optBusquedaSeleccion" runat="server" GroupName="Estados" Text="Estados" Checked="true" style="margin:5px !important"/>
                                            <asp:RadioButton ID="optBusquedaTodos" runat="server" GroupName="Estados" Text="Todos" style="margin:5px !important" /></div>

                                    
                                        <div id="selestados" class="checkbox" style="margin-left:50px" >
                                            <asp:CheckBoxList CssClass="table" CellSpacing="10" CellPadding="10" ID="chkEstados" runat="server" AssociatedControlID="chkEstados"></asp:CheckBoxList>
                                        </div>
                                    </td>
                                    <td style="width:60%;vertical-align:top">
                                       <div style="margin-top:55px">
                                          <table >
                                        <tr style="margin-top:5px !important; margin-top:10px !important">
                                        <td>
                                            <asp:Label ID="Label3" runat="server" Text="Nro. de Matrícula:" CssClass="control-label col-sm-11 text-right" style="margin:5px !important"></asp:Label>    
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNroMatricula" runat="server"  MaxLength="8" CssClass="form-control" style="margin:5px !important"></asp:TextBox>
                                        </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="Apellido y nombre:" CssClass="control-label col-sm-11 text-right" style="margin:5px !important"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtApeNom" runat="server"  MaxLength="50" CssClass="form-control" style="margin:5px !important"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>  
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="C.U.I.T.:" CssClass="control-label col-sm-11 text-right" style="margin:5px !important"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCuit" runat="server"  MaxLength="13" CssClass="form-control" style="margin:5px !important"></asp:TextBox>
                                            <div>
                                                <asp:RegularExpressionValidator ID="CuitRegEx" runat="server" ControlToValidate="txtCuit" style="padding-left:5px" Display="Dynamic"
                                                    ErrorMessage="El Nº de CUIT no tiene un formato válido. Ej: 20-25006281-9" ValidationExpression="\d{2}-\d{8}-\d{1}"
                                                    ValidationGroup="Buscar"></asp:RegularExpressionValidator>
                                            </div>
                                        </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text="Nro. de Trámite:" CssClass="control-label col-sm-11 text-right" style="margin:5px !important"></asp:Label>    
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNroTramite" runat="server"  MaxLength="8" CssClass="form-control" style="margin:5px !important"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text="Nro. de Encomienda en Consejo:" CssClass="control-label col-sm-11 text-right" style="margin:5px !important; "></asp:Label>    
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNroEncomienda" runat="server" MaxLength="8" CssClass="form-control" style="margin:5px !important"></asp:TextBox>
                                            </td>
                                        </tr>
                                        </table>
                                       </div>
                                    </td>
                                </tr>
                                </table>
                            <br />
                                <asp:UpdatePanel ID="pnlDatosFiltros" runat="server">
                                <ContentTemplate>
                                    <div style="width:100%; text-align:center">
                                    
                                        <div style="display:table; margin:auto">
                                        <div style="display:table-row">
                                            <div style="display:table-cell">
                                                
                                                <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-primary"  ValidationGroup="Buscar" 
                                                                    OnClick="btnBuscar_Click" >
                                                    <i class="imoon imoon-search"></i>
                                                    <span class="text">Buscar</span>
                                                </asp:LinkButton>
                                            </div>
                                            <div style="display:table-cell">

                                                <asp:LinkButton ID="btnBusquedaLimpiar" runat="server"  CssClass="btn btn-default" >
                                                    <i class="imoon imoon-eraser"></i>
                                                    <span class="text">Limpiar</span>
                                                </asp:LinkButton>
                                               
                                            </div>
                                            <div style="display:table-cell">

                                                <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="pnlDatosFiltros"
                                                    runat="server" DisplayAfter="0"  >
                                                    <ProgressTemplate>
                                                        <img src="../Common/Images/Controles/Loading24x24.gif" alt="" />
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>   
                            
                                            </div>
                                        </div>
                                        </div>
                                    </div>
                            
                            

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <br /><br />
                        <div class="titulo-6">
                            <asp:Label ID="lbltituloResult" runat="server" Text="Encomienda de Ley 257 - Listado de Trámites"></asp:Label>
                        </div>

                        <asp:UpdatePanel ID="pnlGrillaTramites" runat="server">
                            <ContentTemplate>

                            <div>
                                <asp:GridView 
                                    ID="grdTramites" 
                                    runat="server" 
                                    AutoGenerateColumns="false" 
                                    Width="100%" 
                                    GridLines="None" 
                                    CssClass="table table-bordered mtop5"
                                    ItemType="DataTransferObject.EncomiendaExternaDTO" 
                                    OnDataBound="grdTramites_DataBound" 
                                    AllowPaging="true"
                                    PageSize="20" 
                                    SelectMethod="GetTramites"
                                    OnPageIndexChanging="grdTramites_PageIndexChanging"
                                    DataKeyNames="IdEncomienda,IdEstado,nroTramite, IdTipoTramite">
                                    <HeaderStyle CssClass="grid-header"  /> 
                                    <RowStyle CssClass="grid-row"  />
                                    <AlternatingRowStyle BackColor="#efefef" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Nº Trámite" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="170px">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkHActualizar" NavigateUrl='<%# "~/" +  ConsejosProfesionales.RouteConfig.ACTUALIZA_ENCOMIENDA_EXTERNA + Item.IdEncomienda + "/"  + Item.IdTipoTramite %>' Text="<%# Item.nroTramite %>"  runat="server"></asp:HyperLink>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="NroEncomiendaConsejo" HeaderText="Nº Encomienda en Consejo" ItemStyle-Width="140px" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" />
                                        <asp:BoundField DataField="CreateDate" DataFormatString="{0:g}" HeaderText="Fecha de inicio"  ItemStyle-Width="200px" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-center" />
                                        <asp:BoundField DataField="Estado.NomEstado" HeaderText="Estado" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="130px" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-center"/>
                                        <asp:BoundField DataField="Direccion.direccion" HeaderText="Dirección" HeaderStyle-HorizontalAlign="Left" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-center"/>


                                    </Columns>
                                    <PagerTemplate >
                                        <asp:Panel ID="pnlpager" runat="server" Style="padding: 10px; text-align: center;border-top: solid 1px #e1e1e1">
                                            <asp:LinkButton ID="cmdAnterior" runat="server" Text="<<" OnClick="cmdAnterior_Click" CssClass="btn btn-default" Width="35px" />
                                            <asp:LinkButton ID="cmdPage1" runat="server" Text="1" OnClick="cmdPage" CssClass="btn btn-default" />
                                            <asp:LinkButton ID="cmdPage2" runat="server" Text="2" OnClick="cmdPage" CssClass="btn" />
                                            <asp:LinkButton ID="cmdPage3" runat="server" Text="3" OnClick="cmdPage" CssClass="btn" />
                                            <asp:LinkButton ID="cmdPage4" runat="server" Text="4" OnClick="cmdPage" CssClass="btn" />
                                            <asp:LinkButton ID="cmdPage5" runat="server" Text="5" OnClick="cmdPage" CssClass="btn" />
                                            <asp:LinkButton ID="cmdPage6" runat="server" Text="6" OnClick="cmdPage" CssClass="btn" />
                                            <asp:LinkButton ID="cmdPage7" runat="server" Text="7" OnClick="cmdPage" CssClass="btn" />
                                            <asp:LinkButton ID="cmdPage8" runat="server" Text="8" OnClick="cmdPage" CssClass="btn" />
                                            <asp:LinkButton ID="cmdPage9" runat="server" Text="9" OnClick="cmdPage" CssClass="btn" />
                                            <asp:LinkButton ID="cmdPage10" runat="server" Text="10" OnClick="cmdPage" CssClass="btn" />
                                            <asp:LinkButton ID="cmdPage11" runat="server" Text="11" OnClick="cmdPage" CssClass="btn" />
                                            <asp:LinkButton ID="cmdPage12" runat="server" Text="12" OnClick="cmdPage" CssClass="btn" />
                                            <asp:LinkButton ID="cmdPage13" runat="server" Text="13" OnClick="cmdPage" CssClass="btn" />
                                            <asp:LinkButton ID="cmdPage14" runat="server" Text="14" OnClick="cmdPage" CssClass="btn" />
                                            <asp:LinkButton ID="cmdPage15" runat="server" Text="15" OnClick="cmdPage" CssClass="btn" />
                                            <asp:LinkButton ID="cmdPage16" runat="server" Text="16" OnClick="cmdPage" CssClass="btn" />
                                            <asp:LinkButton ID="cmdPage17" runat="server" Text="17" OnClick="cmdPage" CssClass="btn" />
                                            <asp:LinkButton ID="cmdPage18" runat="server" Text="18" OnClick="cmdPage" CssClass="btn" />
                                            <asp:LinkButton ID="cmdPage19" runat="server" Text="19" OnClick="cmdPage" CssClass="btn" />
                                            <asp:LinkButton ID="cmdSiguiente" runat="server" Text=">>" OnClick="cmdSiguiente_Click"
                                                CssClass="btn btn-default" Width="35px" />
                                        </asp:Panel>
                                
                                    </PagerTemplate>      
                                    <EmptyDataTemplate>
                                
                                        <div>
                                        No se encontraron trámites con los filtros ingresados.<br />
                                        </div>
                                    </EmptyDataTemplate>

                                </asp:GridView>
                                </div>
                                <div style="width:100%; text-align:center">
                                    <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="pnlGrillaTramites"
                                        runat="server" DisplayAfter="0">
                                        <ProgressTemplate>
                                            <img src="../Common/Images/Controles/Loading24x24.gif" alt="" /> Cargando...</ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>
            
                    </asp:Panel>



                </asp:Panel>
    
                
            </asp:Panel>
      
            
            <asp:Panel ID="pnlInformacion" runat="server" CssClass="modalPopup" Style="display: none"
                Width="450px" DefaultButton="btnAceptarInformacion">
                <table cellpadding="7" style="width: 100%">
                    <tr>
                        <td style="width: 80px">
                            <asp:Image ID="imgmpeInfo" runat="server" ImageUrl="~/Common/Images/Controles/error64x64.png" />
                        </td>
                        <td>
                            <asp:UpdatePanel ID="updmpeInfo" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblmpeInfo" runat="server" Style="color: Black"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="btnAceptarInformacion" runat="server" CssClass="btnOK" Text="Aceptar"
                                Width="100px" OnClientClick="return ocultarPopup('pnlInformacion');" />
                                            
                                        
                        </td>
                    </tr>
                </table>
            </asp:Panel>

<script type="text/javascript">

    $(document).ready(function () {

        camposAutonumericos();

        if (($("#<%: optBusquedaSeleccion.ClientID %>").attr("checked"))) {
            $("#selestados").show();
        }
        else {
            $("#selestados").hide();
        }
    });

    function camposAutonumericos() {

        //$('#txtNroMatricula').autoNumeric({ aSep: '', mDec: '0', vMax: '99999999' });
        //$('#txtNroTramite').autoNumeric({ aSep: '', mDec: '0', vMax: '99999999' });
        //$('#txtNroEncomienda').autoNumeric({ aSep: '', mDec: '0', vMax: '99999999' });

        return false;
    }
    function seleccionEstados(mostrar) {

        if (mostrar) {
            $("#selestados").show();
        }
        else {
            $("#selestados").hide();
        }
        return false;
    }
    

    function LimpiarCamposFiltro(estadoConfirmar) {
        //blanquear campos input
        $('#<%: txtNroMatricula.ClientID %>').val("");
        $('#<%: txtApeNom.ClientID %>').val("");
        $('#<%: txtCuit.ClientID %>').val("");
        $('#<%: txtNroEncomienda.ClientID %>').val("");
        $('#<%: txtNroTramite.ClientID %>').val("");
        $('#<%: txtNroEncomienda.ClientID %>').val("");

        return false;
    }

</script>

    
</asp:Content>