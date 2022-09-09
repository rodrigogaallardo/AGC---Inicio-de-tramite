<%@ Page  Title="Datos del local" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true CodeBehind="CertificadoSobrecarga.aspx.cs" Inherits="AnexoProfesionales.CertificadoSobrecarga" %>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <%: Scripts.Render("~/bundles/autoNumeric") %>

    <%--ajax cargando ...--%>
    <div id="Loading" style="text-align: center; padding-bottom: 20px; margin-top: 120px">
        <table border="0" style="border-collapse: separate; border-spacing: 5px; margin: auto">
            <tr>
                <td>
                    <img src="<%: ResolveUrl("~/Content/img/app/Loading128x128.gif") %>" alt="" />
                </td>
            </tr>
            <tr>
                <td style="font-size: 24px">Cargando datos...
                </td>
            </tr>
        </table>
    </div>

    <div id="page_content">

        <h2>Datos del Local</h2>
        <hr />
        
        <asp:HiddenField ID="hid_DecimalSeparator" runat="server" />
        <asp:HiddenField ID="hid_return_url" runat="server" />
        <asp:HiddenField ID="hid_id_solicitud" runat="server" />

        <p style="margin: auto; padding: 10px; line-height: 20px">
            En este paso podr&aacute; ver los mapas en donde se encuentra la ubicaci&oacute;n y especificar las superficies del lugar.
        </p>
    
        <asp:Panel ID="pnlSobrecarga" runat="server">
            <%--Certificado de SobreCarga--%>
            <div class="row mleft5 mtop10">
                <strong>Certificado de Sobrecarga</strong>
            </div>
            <div class="row mleft5">
                
                <div class="col-md-2" style="width:100px;line-height:40px">
                    <label>Corresponde:</label>
                </div>
                <div class="col-md-2" style="width: 45px;">
                    <div class="radio">
                        <label>
                            <asp:RadioButton ID="optsCertificadoSobrecarga_SI" runat="server" GroupName="Sobrecarga" />Sí
                        </label>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="radio">
                        <label>
                        <asp:RadioButton ID="optsCertificadoSobrecarga_NO" runat="server" GroupName="Sobrecarga" Checked="true"  />No
                        </label>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlCertificadoSobrecarga" runat="server" Style="display: none">
            <table border="0" cellpadding="5">
                <tr>
                    <td>Certificado de sobrecarga en base a:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTipoObservacionCertificado" runat="server" Width="400px"
                          >
                            <asp:ListItem Value="1" Text="Estudios analíticos (in situ)"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Planos aprobados conformes a obra de estructura"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Prueba de carga"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table border="0" cellspacing="0" cellpadding="5" style="padding: 0px 10px 10px 10px">
                <tr>
                    <td>Listado de sobrecargas
                    </td>
                    <td>
                        <div style="padding-left: 30px">
                            Requisitos establecidos en:
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="width: 450px; border: solid 1px #cccccc; vertical-align: top">
                        <asp:UpdatePanel ID="updGrillaSobreCargas" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grdSobrecargas" runat="server" AutoGenerateColumns="false" GridLines="None"
                                    Width="400px">
                                    <Columns>
                                        <asp:BoundField DataField="estructura_sobrecarga" HeaderText="Estructura" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="peso_sobrecarga" HeaderText="Peso (kN/m2)" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </asp:GridView>
                                <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="updGrillaSobreCargas"
                                    runat="server" DisplayAfter="0">
                                    <ProgressTemplate>
                                        <img src="../../../Content/img/app/Loading24x24.gif" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <table border="0" cellpadding="5" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:RadioButton ID="optParrafoSobrecarga1" runat="server" Text=" " GroupName="RequisitosSobrecarga"
                                         />
                                </td>
                                <td>1) Local preexistente a la vigencia de la Ley 521, B.O. N° 1101(10/01/01) Sobrecarga
                                    conforme Art. 8.1.3. inc
                                    <asp:Label ID="txtIncisoSobrecarga" runat="server" Width="30px" CssClass="form-control"></asp:Label>
                                    <span style="padding-left: 5px">item</span>
                                    <asp:Label ID="txtItemincisoSobrecarga" runat="server" Width="30px" CssClass="form-control"></asp:Label>
                                    .<br />
                                    Sobrecargas y cargas accidentales o útiles del Código de la Edificación vigente
                                    al momento.
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="optParrafoSobrecarga2" runat="server" Text=" " GroupName="RequisitosSobrecarga"
                                        />
                                </td>
                                <td>2) Sobrecarga conforme Art. 8.1.1 Código de la Edificación - CIRSOC 101.<br />
                                    Cargas y Sobrecargas Gravitatorias para el Cálculo de Estructuras de Edificios.
                                </td>
                            </tr>
                        </table>
                        <div style="padding: 5px 0px 0px 20px">
                            <asp:CustomValidator ID="CustomValidator6" runat="server" ErrorMessage="Debe ingresar una de las dos opciones."
                                ValidationGroup="Continuar" CssClass="error-label" Display="Dynamic" ClientValidationFunction="validarIngresoRequisitos"></asp:CustomValidator>
                            <asp:CustomValidator ID="CustomValidator7" runat="server" ErrorMessage="Debe ingresar el inciso y el item del artículo."
                                ValidationGroup="Continuar" CssClass="error-label" Display="Dynamic" ClientValidationFunction="validarIngresoArt813"></asp:CustomValidator>
                        </div>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
 
    <script type="text/javascript">

      

        $(document).ready(function () {
            $("#page_content").hide();
            $("#Loading").show();


            finalizarCarga(); //Lo puse para poder ver la pantalla. LUEGO QUITAR.
        });
   </script>
 </asp:Content>