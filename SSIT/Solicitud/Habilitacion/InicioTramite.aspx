<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InicioTramite.aspx.cs" Inherits="SSIT.Solicitud.Habilitacion.InicioTramite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

        <asp:UpdatePanel ID="updMsj" runat="server" class="form-group" UpdateMode="Conditional">
            <ContentTemplate>
              <div class="box-panel text-center"  >
               <div style="margin: 20px; margin-top: -5px">
                <div class="text-center" >
                    <h2>Nueva Solicitud de Trámite</h2>
                    <hr />
                </div>
            </div>

                <asp:HiddenField ID="hid_id_solicitud" runat="server" />

                <div class="text-center" style="margin-left:100px">
                    <div class="col-sm-5">
                Su numero de Solicitud es: <b><asp:Label ID="lbl_ID" runat="server" Font-Size="28px" Style="margin-left:20px; color: #377bb5"></asp:Label></b></div>
                    <div class="col-sm-6">
                            <asp:LinkButton ID="btnAceptar" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnAceptar_Click">
                        <span class="text">Continuar</span>
                                <i class="imoon imoon-chevron-right" ></i>
                        </asp:LinkButton>
                    </div>
                </div>
              </div>
            </ContentTemplate>
        </asp:UpdatePanel>


</asp:Content>
