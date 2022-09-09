<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TAD.aspx.cs" Inherits="SSIT.TAD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="text-center">
        <div class="form-inline">
            <div class="form-group">
                <label >CUIT:</label>
                <asp:TextBox ID="txt" runat="server" Width="120px" CssClass="form-control inline"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:LinkButton ID="lnk" runat="server" OnClick="lnk_Click" CssClass="btn btn-default" Text="Post"></asp:LinkButton>
            </div>
        </div>
        <div class="form-inline">
            <div class="form-group">
                <div id="Req_user" class="alert alert-small alert-danger mbottom0 mtop5" style="display: none;">
                    El usaurio no existe.
                </div>
            </div>
            <div class="form-group">
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            init_Js();
        });

        function init_Js() {
            $("#<%: txt.ClientID %>").on("keyup", function (e) {
                $("#Req_user").hide();
            });
            return false;
        }

        function showfrmError() {
            $("#Req_user").css("display", "inline-block");
            return false;
        }
    </script>
</asp:Content>

