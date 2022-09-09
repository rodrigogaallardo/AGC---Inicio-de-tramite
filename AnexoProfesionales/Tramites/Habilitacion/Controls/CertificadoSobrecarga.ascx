<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CertificadoSobreCarga.ascx.cs" Inherits="AnexoProfesionales.Controls.CertificadoSobreCarga" %>

<%--Datos Local--%>
<asp:Panel ID="pnlCargaPlanos" runat="server">

        <%--Grilla de certificado de sobrecarga--%>
                <br />
                  <ul>
                   
                    <li>Certificado de sobrecarga en base a:
                       
                        <asp:DropDownList ID="ddlTiposCertificado" runat="server" Width="400px" style="display:initial; margin-left:10px" height="30px" CssClass="form-control"
                            Enabled="false">
                        </asp:DropDownList>
                        
                    </li>
                </ul>
                    <ul>
                    <li>Requisitos:              
                        <asp:DropDownList ID="ddlTiposSobrecargas" runat="server" Width="400px" height="30px"  
                            style="display:initial; margin-left:175px" CssClass="form-control" AutoPostBack="true" Enabled="false">
                        </asp:DropDownList>
                     </li>
                </ul>
            <br />
               <div style="margin-left:20px">
                   <asp:UpdatePanel ID="updGrillaSobreCargas" runat="server">
                                            <ContentTemplate>
                                    <asp:GridView ID="grdSobrecargas"  CssClass="box-panel" runat="server" AutoGenerateColumns="false" GridLines="None" Width="1130px">                                                 
                                 <Columns>
                                   <asp:TemplateField ItemStyle-Width="100%" >
                                     <ItemTemplate>
                                            <div style="color:#377bb5; margin-left:10px; margin-top:-10px">                                 
                                                  <h4><i class="imoon imoon-stackexchange" style="margin-right:10px"></i>Listado de Sobrecargas</h4>
                                                                    
                                               </div>
                                             <asp:HiddenField ID="hid_id_sobrecarga_detalle1" runat="server" Value='<% #Eval("id_sobrecarga_detalle1") %>' />    
                                         <div style="margin:20px">                                                                                                                 
                                                <div class="divider"></div>
                                                <div class="form-horizontal" style="background:content-box">
                                                   <div class="form-group">
                                                                                   
                                                    <asp:Label ID="Label1" runat="server" CssClass="control-label col-sm-3 text-center" Font-Bold="true" Text='<%# tipoString() %>'></asp:Label>                                                                 
                                                    <asp:Label ID="lbl_tipo_destino" runat="server" CssClass="control-label col-sm-1 text-center" Text='<% #Eval("desc_tipo_destino") %>' Style="margin-left:-30px"></asp:Label>
                                                    <asp:HiddenField ID="hid_id_tipo_destino"  runat="server" Value='<% #Eval("id_tipo_destino") %>' />
                                                
                                                    <asp:Label ID="Label2" runat="server" CssClass="control-label col-sm-1 text-center" Font-Bold="true" Text="Planta:" Style="margin-left:50px"></asp:Label>                                                                                                                       
                                                    <asp:Label ID="lbl_planta" CssClass="control-label col-sm-1" runat="server" Text='<% #Eval("desc_planta") %>' Style="margin-left:-20px"></asp:Label>
                                                    <asp:HiddenField ID="hid_id_planta"  runat="server" Value='<% #Eval("id_planta") %>' />
                                                  
                                                         
                                                    <asp:Label ID="Label3" runat="server" CssClass="control-label col-sm-2 text-center" Font-Bold="true" Text="Losa Sobre" Style="margin-left:130px"></asp:Label>           
                                                    <asp:Label ID="lbl_losa_sobre" CssClass="control-label col-sm-1 text-center" runat="server"  Text='<% #Eval("losa_sobre") %>' Style="margin-left:70px"></asp:Label>
                                                                  
                                                   
                                                  </div>
                                                 </div>  
                                                <div class="divider"></div>   
                                                  <div class="form-horizontal">
                                                  <div class="form-group">     
                                                    <asp:Label ID="Label4" runat="server" CssClass="control-label col-sm-3 text-center" Font-Bold="true" Text="Uso"></asp:Label>                                                                          
                                                    <asp:Label ID="lbl_tipo_uso" runat="server" CssClass="control-label col-sm-4 text-center" Text='<% #Eval("desc_tipo_uso") %>' Style="margin-left:10px"></asp:Label>
                                                    <asp:HiddenField ID="hid_id_tipo_uso" runat="server" Value='<% #Eval("id_tipo_uso") %>' />
                                                         
                                                     
                                                    <asp:Label ID="lblSobrecarga" runat="server" Text='<% #Eval("texto_carga_uso") %>'  CssClass="control-label col-sm-4 text-center" Font-Bold="true" Style="margin-left:-30px"  ></asp:Label>
                                                                 
                                                    <asp:Label ID="lbl_valor" runat="server"  CssClass="control-label col-sm-1 text-center"  Text='<% #Eval("valor") %>' ></asp:Label>
                                                    <asp:Label ID="lbl_detalle" runat="server"   CssClass="control-label col-sm-1 text-center" Text='<% #Eval("detalle") %>'></asp:Label>
                                                  </div>
                                                 </div>
                                                <div class="divider"></div>       
                                                <div class="form-horizontal">
                                                  <div class="form-group">           
                                                    <asp:Label ID="lblUso1" runat="server" Text='<% #Eval("texto_uso_1") %>'  CssClass="control-label col-sm-3 text-center" Font-Bold="true" ></asp:Label>
                                                               
                                                    <asp:Label ID="lbl_tipo_uso_1" runat="server"  CssClass="control-label col-sm-4 text-center"  Text='<% #Eval("desc_tipo_uso_1") %>'  Style="margin-left:10px"></asp:Label>
                                                    <asp:HiddenField ID="hid_id_tipo_uso_1" runat="server"   Value='<% #Eval("id_tipo_uso_1") %>' />
                                              
                                                    <asp:Label ID="lblTxtUso1" runat="server"   CssClass="control-label col-sm-4 text-center" Text='<% #Eval("texto_carga_uso") %>' Font-Bold="true"  Style="margin-left:-30px"></asp:Label>            
                                                    <asp:Label ID="lbl_valor_1" runat="server"  CssClass="control-label col-sm-1 text-center"  Text='<% #!Eval("valor_1").ToString().Equals("0,00") ? Eval("valor_1") : "" %>' ></asp:Label>
                                                        </div>
                                                 </div>    
                                                <div class="divider"></div>                 
                                                <div class="form-horizontal">
                                                  <div class="form-group">                    
                                                    <asp:Label ID="lblUso2" runat="server"  CssClass="control-label col-sm-3 text-center"  Text='<% #Eval("texto_uso_2") %>' Font-Bold="true"></asp:Label>
                            
                                                    <asp:Label ID="lbl_tipo_uso_2" runat="server" CssClass="control-label col-sm-4 text-center" Text='<% #Eval("desc_tipo_uso_2") %>' Style="margin-left:10px"></asp:Label>
                                                    <asp:HiddenField ID="hid_id_tipo_uso_2" runat="server" Value='<% #Eval("id_tipo_uso_2") %>' />
                                               
                                                    <asp:Label ID="lblTxtUso2" runat="server" CssClass="control-label col-sm-4 text-center" Text='<% #Eval("texto_carga_uso") %>' Font-Bold="true" Style="margin-left:-30px"></asp:Label>
                                                              
                                                    <asp:Label ID="lbl_valor_2" runat="server" CssClass="control-label col-sm-1 text-center" Text='<% #!Eval("valor_2").ToString().Equals("0,00") ? Eval("valor_2") : "" %>'></asp:Label>
                                                 </div>
                                                 </div>    
                                                 <div class="divider"></div> 
                                        </div>   
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                           </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                   <br />
              </div>
</asp:Panel>


