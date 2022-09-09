<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatosLocal.ascx.cs" Inherits="SSIT.Solicitud.Habilitacion.Controls.DatosLocal" %>


<div class="form-group pleft20 pright20">

    <asp:UpdatePanel ID="updDatosLocal" runat="server" >
        <ContentTemplate>

            <div class="row">
                <div class="col-sm-4">
                    <div class="col-sm-12 text-center">
                        <strong>Entre calles</strong>
                    </div>

                    <div class="text-center ">
                        <asp:Image ID="imgMapa1" runat="server" CssClass="img-thumbnail" />
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="col-sm-12 text-center">
                        <strong>Croquis</strong>
                    </div>

                    <div class="text-center ">
                        <asp:Image ID="imgMapa2" runat="server" CssClass="img-thumbnail" />
                    </div>

                </div>
                <div class="col-sm-4">

                    <div class="col-sm-12 text-center pbottom15">
                        <strong>Superficies</strong></label>
                    </div>
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-sm-6 control-label">Superficie cubierta:</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtSuperficieCubierta" runat="server" Text="0,00" Width="100px" CssClass="form-control text-right" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-6 control-label">Superficie descubierta:</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtSuperficieDescubierta" runat="server" Text="0,00" Width="100px" CssClass="form-control text-right" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-6 control-label">Superficie total:</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtSuperficieTotal" runat="server" Text="0,00" Width="100px" CssClass="form-control text-right" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


</div>
