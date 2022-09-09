<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ABMProfesionalesMasivo.aspx.cs" Inherits="ConsejosProfesionales.ABM.ABMProfesionalesMasivo" %>
         
                    <asp:Content ContentPlaceHolderID="FeaturedContent" runat="server" ID="Featured">

                    </asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    
 
        
        <div style="text-align: center;">
                        
        

                <%--Contenido--%>
                <asp:Panel ID="pnlContenido" runat="server" CssClass="box-contenido" BackColor="White">

                    <asp:HiddenField ID="hid_filename" runat="server" />
                    <asp:Panel ID="pnlUpload" runat="server" Width="100%">
                        <table style="width:100%" border="0">
                        <tr>
                        <td>
                            <p>
                                Seleccione el archivo excel que contiene los datos de los profesionales que desea actualizar o incorporar.<br />
                                Si no sabe como debe ser el formato del archivo, por favor presione el link a su derecha.
                            </p>
                            <div style="width:100%">
                                <asp:FileUpload ID="fupProfesionales" runat="server"   />
                            </div>

                        </td>
                        <td style="width:300px" >
                            
                            <asp:HyperLink ID="btnArchivoFormatoProfesionale" runat="server" NavigateUrl="~/Common/Archivos/FormatoArchivoProfesionales.xls?X=2" 
                                CssClass="btnexcel48" Text="Formato del Archivo de Importación" Height="48px" style="padding-top:30px"  ></asp:HyperLink>
                            
                        </td>
                        </tr>
                        </table>
                        
                    </asp:Panel>

                    
                    <asp:UpdatePanel ID="updResultado" runat="server">
                    <ContentTemplate>
                    
                        <asp:Panel ID="pnlValidacion" runat="server" style="display:none"  >
                    
                            <div style="font-family:Trebuchet MS; font-size:16pt; text-align:center;width:100%">
                                <div>
                                    <img src="../Common/Images/Controles/Loading64x64.gif" />
                                </div>
                                <div>
                                    <asp:UpdatePanel ID="updValidacion" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lblMensajeValidacion" runat="server">Importando Archivo de Profesionales</asp:Label>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div>
                                    <div class="timer"></div>
                                </div>
                                
                                <asp:Button ID="btnComenzarValidacion" runat="server" style="display:none" OnClick="btnComenzarValidacion_Click" OnClientClick="inicializarTimer();" />
                                <asp:Button ID="btnComenzarValidacion2" runat="server" Style="display: none" OnClick="btnComenzarValidacion2_Click"
                                    OnClientClick="inicializarTimer();" />

                        
                            </div>

                        </asp:Panel>
                    
                        <asp:Panel ID="pnlGrillaResultados" runat="server" Visible="false">
                            
                            <table border="0">
                            <tr>
                                <td>
                                    <img src="../Common/Images/Controles/error32x32.png" />
                                </td>
                                <td>
                                    No se pudo realizar la actualización debido a que hay errores en el archivo Excel (origen de los datos).
                                </td>
                            </tr>
                            </table>
                                
                            <asp:GridView ID="grdErrores" runat="server" AutoGenerateColumns="false" Width="100%">
                            <HeaderStyle CssClass="grid-header"  /> 
                            <RowStyle CssClass="grid-row"  />
                            <AlternatingRowStyle BackColor="#efefef" />
                            <Columns>
                                <asp:BoundField DataField="mensaje" HeaderText="Descripción del suceso" />
                            </Columns>
                            </asp:GridView>
                            
                            <div style="width:100%; text-align:center; padding-top:20px"">
                                <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/ABM/ABMProfesionalesMasivo.aspx" >Cargar otro archivo o intentar nuevamente</asp:LinkButton>
                            </div>

                        </asp:Panel>

                        <asp:Panel ID="pnlresultadoOK" runat="server" Visible="false" Width="100%">
                        
                            <div style="width:100%; text-align:center">
                                <table border="0">
                                <tr>
                                    <td>
                                        <img src="../Common/Images/Controles/ok32x32.png" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMensajeOK" runat="server">El Proceso de Importación de Profesionales concluyó de manera satisfactoria.</asp:Label>
                                    </td>
                                </tr>
                                </table>
                            </div>

                        </asp:Panel>

                        <asp:Panel ID="pnlErrores" runat="server" Visible="false" >
                            
                            <div style="width:100%; text-align:center">
                                <table border="0" class="error-box">
                                <tr>
                                    <td>
                                        <img src="../Common/Images/Controles/error32x32.png" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblErrores" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                </table>
                            </div>

                            <div style="width:100%; text-align:center; padding-top:20px"">
                                <asp:LinkButton ID="lnkIntentarNuevamente" runat="server"  PostBackUrl="~/ABM/ABMProfesionalesMasivo.aspx" >Cargar otro archivo o intentar nuevamente</asp:LinkButton>
                            </div>

                        </asp:Panel>

                    </ContentTemplate>
                    </asp:UpdatePanel>
                    
                        

                </asp:Panel>
              
         
                
        </div>
         
  

</asp:Content>