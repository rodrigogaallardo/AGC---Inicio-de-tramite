<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AnexoNotarial.ascx.cs" Inherits="SSIT.Solicitud.Habilitacion.Controls.AnexoNotarial" %>
<div id="box_titulares" class="accordion-group widget-box" style="background-color:#ffffff">
    <div class="accordion-heading">
        <a id="titulares_btnUpDown" data-parent="#collapse-group" href="#collapse_AnexoNotarial"
            data-toggle="collapse" onclick="bt_btnUpDown_collapse_click(this)">

            <div class="widget-title">
                <span class="icon"><i class="imoon imoon-file" Style="color:#344882"></i></span>
                <h5>
                    <asp:Label ID="lbl_titulares_tituloControl" runat="server" Text="Anexo Notarial"></asp:Label></h5>
                <span class="btn-right"><i class="imoon imoon-chevron-up" Style="color:#344882"></i></span>
            </div>
        </a>
    </div>
  <div class="accordion-body collapse in" id="collapse_AnexoNotarial">
   <div style="margin:10px">
<asp:Panel ID="pnlAnexoNotarial" runat="server" >
    <div style="padding: 0px 10px 10px 10px; width: auto">
       
        <div class="mtop10" >
            <strong>Listado de Anexos Notarial</strong>
        </div>
        <asp:GridView 
            ID="gridAnexo_db" 
            runat="server" 
            AutoGenerateColumns="false"
            AllowPaging="false" 
            Style="border:none;" 
            CssClass="table table-bordered mtop5"
            GridLines="None" 
            Width="100%" 
            DataKeyNames="id_actanotarial">
            <HeaderStyle CssClass="grid-header" />
            <RowStyle CssClass="grid-row" />
            <AlternatingRowStyle BackColor="#efefef" />
            <Columns>
                <asp:BoundField DataField="id_actanotarial" HeaderText="Acta Notarial" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center " ItemStyle-Width="20%" />
                <asp:BoundField DataField="id_encomienda" HeaderText="Anexo" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center " ItemStyle-Width="20%"  />
                 <asp:BoundField DataField="Escribano.ApyNom" HeaderText="Escribano" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center " ItemStyle-Width="20%"  />
                <asp:BoundField DataField="CreateDate" DataFormatString="{0:d}" HeaderText="Fecha" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" ItemStyle-Width="20%" />              
                <asp:TemplateField ItemStyle-CssClass="text-center"  HeaderStyle-CssClass="text-center" HeaderText="Descargar" ItemStyle-Width="20%">
                    <ItemTemplate>
                        <asp:HyperLink ID="lnkImprimirAnexo" runat="server" CssClass="link-local" NavigateUrl='<%#  Eval("url") %>' title="Descargar Anexo" Target="_blank">
                             <span class="icon"><i class="imoon-download color-blue fs24"></i></span>
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <div class="mtop10">
                      <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                    No se relacionaron anexos.
                </div>
            </EmptyDataTemplate>
        </asp:GridView>
 
       
    </div>
</asp:Panel>
   </div>
 </div>
    </div>
