<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucVisorTitulares.ascx.cs" Inherits="AnexoProfesionales.Tramites.Habilitacion.Controls.VisorTitulares" %>


<asp:Panel ID="pnlTitularesPF" runat="server">
    <%--Grilla de Titulares PF--%>
    <h3 style="text-align:center">Titulares Personas Fisicas</h3>
    <br /> 
    <asp:GridView ID="grdTitularesPF" runat="server" AutoGenerateColumns="false" AllowPaging="false" Style="border: none;" GridLines="None" Width="100%"
        CssClass="table table-bordered mtop5" CellPadding="3" DataKeyNames="IdPersonaFisica" ItemType="DataTransferObject.EncomiendaTitularesPersonasFisicasDTO">
        <AlternatingRowStyle BackColor="#efefef" />
        <Columns>
            <asp:TemplateField HeaderText="Datos Personales" ItemStyle-Width="300px" HeaderStyle-CssClass="text-center" HeaderStyle-BorderColor="#ffffff">
                <ItemTemplate>
                    <Label Style="color: #337AB7; font-weight: bold;">Apellido/s: </Label>
                    <asp:Label ID="grdlblApellidoPF" runat="server" Text='<%# Item.Apellido %>' />
                    <br />
                    <label style="color: #337AB7; font-weight: bold;">Nombre/s: </label>
                    <asp:Label ID="grdlblNombrePF" runat="server" Text='<%# Item.Nombres %>' />
                    <br />
                    <label style="color: #337AB7; font-weight: bold;">Tipo de doc: </label>
                    <asp:Label ID="grdlblTipoDocPF" runat="server" Text='<%# Item.TipoDocumentoPersonalDTO.Nombre %>' />
                    <br />
                    <label style="color: #337AB7; font-weight: bold;">Nro de doc: </label>
                    <asp:Label ID="grdlblNroDocPF" runat="server" Text='<%# Item.NroDocumento %>' />
                    <br />
                    <label style="color: #337AB7; font-weight: bold;">C.U.I.T.: </label>
                    <asp:Label ID="grdlblCuitPF" runat="server" Text='<%# Item.Cuit %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ing. Brutos" ItemStyle-Width="250px" HeaderStyle-CssClass="text-center" HeaderStyle-BorderColor="#ffffff">
                <ItemTemplate>
                    <label style="color: #337AB7; font-weight: bold;">Tipo Ing. Brutos: </label>
                    <asp:Label ID="grdlblTipoIngBrutoPF" runat="server" Text='<%# Item.TiposDeIngresosBrutosDTO.NomTipoIb %>' />
                    <asp:Panel ID="pnlIngBrutosPF" runat="server" Visible='<%# Item.IngresosBrutos == null ? false: Item.IngresosBrutos.Length > 0 %>'>
                        <br />
                        <label style="color: #337AB7; font-weight: bold;">N° Ing. Brutos: </label>
                        <asp:Label ID="grdlblIngBrutoPF" runat="server" Text='<%# Item.IngresosBrutos %>' />
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Dirección" ItemStyle-Width="300px" HeaderStyle-CssClass="text-center" HeaderStyle-BorderColor="#ffffff">
                <ItemTemplate>
                    <label style="color: #337AB7; font-weight: bold;">Calle: </label>
                    <asp:Label ID="grdlblCallePF" runat="server" Text='<%# Item.Calle %>' />
                    <br />
                    <label style="color: #337AB7; font-weight: bold;">N° de Puerta: </label>
                    <asp:Label ID="grdlblPuertaPF" runat="server" Text='<%# Item.NroPuerta %>' />
                    <asp:Panel ID="pnlPisoPF" runat="server" Visible='<%# Item.Piso == null ? false: Item.Piso.Length > 0  %>'>
                        <label style="color: #337AB7; font-weight: bold;">Piso: </label>
                        <asp:Label ID="grdlblPisoPF" runat="server" Text='<%# Item.Piso %>' />
                    </asp:Panel>
                    <asp:Panel ID="pnlDeptoPF" runat="server" Visible='<%# Item.Depto == null ? false: Item.Depto.Length > 0  %>'>
                        <label style="color: #337AB7; font-weight: bold;">Depto/UF:</label>
                        <asp:Label ID="grdlblDeptoPF" runat="server" Text='<%# Item.Depto %>' />
                    </asp:Panel>
                    <label style="color: #337AB7; font-weight: bold;">Código Postal: </label>
                    <asp:Label ID="grdlblCodPostalPF" runat="server" Text='<%# Item.CodigoPostal %>' />
                    <br />
                    <label style="color: #337AB7; font-weight: bold;">Provincia: </label>
                    <asp:Label ID="grdlblProvinciaPF" runat="server" Text='<%# Item.LocalidadDTO.ProvinciaDTO.Nombre %>' />
                    <br />
                    <label style="color: #337AB7; font-weight: bold;">Localidad: </label>
                    <asp:Label ID="grdlblLocalidadPF" runat="server" Text='<%# Item.LocalidadDTO.Depto %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Contacto" ItemStyle-Width="300px" HeaderStyle-CssClass="text-center" HeaderStyle-BorderColor="#ffffff">
                <ItemTemplate>
                    <asp:Panel ID="pnlTlfMovilPF" runat="server" Visible='<%# Item.TelefonoMovil == null ? false: Item.TelefonoMovil.Length > 0 %>'>
                        <label style="color: #337AB7; font-weight: bold;">Teléfono Móvil: </label>
                        <asp:Label ID="grdlblTlfMovilPF" runat="server" Text='<%# Item.TelefonoMovil %>' />
                        <br />
                    </asp:Panel>
                    <asp:Panel ID="pnlTlfPF" runat="server" Visible='<%# Item.Telefono == null ? false: Item.Telefono.Length > 0 %>'>
                        <label style="color: #337AB7; font-weight: bold;">Teléfono: </label>
                        <asp:Label ID="grdlblTlfPF" runat="server" Text='<%# Item.Telefono %>' />
                    </asp:Panel>
                    <label style="color: #337AB7; font-weight: bold;">Email: </label>
                    <asp:Label ID="grdlblEmailPF" runat="server" Text='<%# Item.Email %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <div class="mtop10">
                <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png")%>' alt="" />
                No hay datos aún...
            </div>
        </EmptyDataTemplate>
    </asp:GridView>
    <br />
    <%--Firmantes--%>
    <h3 style="text-align:center">Firmantes Personas Fisicas</h3>
    <br /> 
    <%--Grilla de Firmantes PF--%>
    <asp:GridView ID="grdFirmantesPF" runat="server" AutoGenerateColumns="false" AllowPaging="false" Style="border: none; margin-top: 10px" GridLines="None"
        Width="100%" CssClass="table table-bordered mtop5" CellPadding="3" DataKeyNames="IdFirmantePf" ItemType="DataTransferObject.EncomiendaFirmantesPersonasFisicasDTO">
        <AlternatingRowStyle BackColor="#efefef" />
        <Columns>
            <asp:TemplateField HeaderText="Firmante de..." HeaderStyle-CssClass="text-center" HeaderStyle-BorderColor="#ffffff">
                <ItemTemplate><%# Item.EncomiendaTitularesPersonasFisicasDTO.Apellido %> <%# Item.EncomiendaTitularesPersonasFisicasDTO.Nombres %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Apellido y Nombre/s" HeaderStyle-CssClass="text-center" HeaderStyle-BorderColor="#ffffff">
                <ItemTemplate><%# Item.Apellido %> <%# Item.Nombres%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tipo Doc." HeaderStyle-CssClass="text-center" HeaderStyle-BorderColor="#ffffff">
                <ItemTemplate><%# Item.TipoDocumentoPersonalDTO.Nombre %> </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Documento" HeaderStyle-CssClass="text-center" HeaderStyle-BorderColor="#ffffff">
                <ItemTemplate><%# Item.NroDocumento %> </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Carácter Legal" HeaderStyle-CssClass="text-center" HeaderStyle-BorderColor="#ffffff">
                <ItemTemplate><%# Item.TiposDeCaracterLegalDTO.NomTipoCaracter %> </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <div class="mtop10">
                <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                No hay datos aún...
            </div>
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Panel>
<br /> <br />
<asp:Panel ID="pnlTitularesPJ" runat="server">
    <%--Grilla de Titulares PF--%>
    <h3 style="text-align:center">Titulares Personas Juridicas</h3>
    <br /> 
    <asp:GridView ID="grdTitularesPJ" runat="server" AutoGenerateColumns="false" AllowPaging="false" Style="border: none;" GridLines="None" Width="100%"
        CssClass="table table-bordered mtop5" CellPadding="3" DataKeyNames="IdPersonaJuridica" ItemType="DataTransferObject.EncomiendaTitularesPersonasJuridicasDTO">
        <AlternatingRowStyle BackColor="#efefef" />
        <Columns>
            <asp:TemplateField HeaderText="Datos Principales"   HeaderStyle-CssClass="text-center" ItemStyle-Width="550px" HeaderStyle-BorderColor="#ffffff">
                <ItemTemplate>
                    <label style="color: #337AB7; font-weight: bold;">Razon Social: </label>
                    <asp:Label ID="grdlblApellidoPJ" Width="100%" runat="server" style="text-align:left;word-wrap: normal; word-break: break-all;" Text='<%#  Item.RazonSocial %>' />
                    <br />
                    <label style="color: #337AB7; font-weight: bold;">C.U.I.T.: </label>
                    <asp:Label ID="grdlblNombrePJ" runat="server" Text='<%# Item.CUIT %>' />
                    <br />
                    <label style="color: #337AB7; font-weight: bold;">Tipo de Sociedad: </label>
                    <asp:Label ID="grdlblTipoDocPJ" runat="server" Text='<%# Item.TipoSociedadDTO.Descripcion %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ing. Brutos" ItemStyle-Width="150px" HeaderStyle-CssClass="text-center" HeaderStyle-BorderColor="#ffffff">
                <ItemTemplate>
                    <label style="color: #337AB7; font-weight: bold;">Tipo Ing. Brutos: </label>
                    <asp:Label ID="grdlblTipoIngBrutoPJ" runat="server" Text='<%#  Item.TiposDeIngresosBrutosDTO.NomTipoIb %>' />
                    <asp:Panel ID="pnlIngBrutosPJ" runat="server" Visible='<%# Item.NroIb == null ? false: Item.NroIb.Length > 0 %>'>
                        <br />
                        <label style="color: #337AB7; font-weight: bold;">N° Ing. Brutos: </label>
                        <asp:Label ID="grdlblIngBrutoPJ" runat="server" Text='<%# Item.NroIb %>' />
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Dirección" ItemStyle-Width="350px" HeaderStyle-CssClass="text-center" HeaderStyle-BorderColor="#ffffff">
                <ItemTemplate>
                    <label style="color: #337AB7; font-weight: bold;">Calle: </label>
                    <asp:Label ID="grdlblCallePF" runat="server" Text='<%#  Item.Calle %>' />
                    <br />
                    <label style="color: #337AB7; font-weight: bold;">N° de Puerta: </label>
                    <asp:Label ID="grdlblPuertaPF" runat="server" Text='<%# Item.NroPuerta %>' />
                    <asp:Panel ID="pnlPisoPJ" runat="server" Visible='<%# Item.Piso == null ? false: Item.Piso.Length > 0 %>'>
                        <label style="color: #337AB7; font-weight: bold;">Piso: </label>
                        <asp:Label ID="grdlblPisoPF" runat="server" Text='<%# Item.Piso %>' />
                    </asp:Panel>
                    <asp:Panel ID="pnlDeptoPJ" runat="server" Visible='<%# Item.Depto == null ? false: Item.Depto.Length > 0 %>'>
                        <label style="color: #337AB7; font-weight: bold;"> Depto/UF:</label>
                        <asp:Label ID="grdlblDeptoPF" runat="server" Text='<%# Item.Depto %>' />
                    </asp:Panel>
                    <label style="color: #337AB7; font-weight: bold;">Código Postal: </label>
                    <asp:Label ID="grdlblCodPostalPF" runat="server" Text='<%# Item.CodigoPostal %>' />
                    <br />
                    <label style="color: #337AB7; font-weight: bold;">Provincia: </label>
                    <asp:Label ID="grdlblProvinciaPF" runat="server" Text='<%# Item.LocalidadDTO.ProvinciaDTO.Nombre %>' />
                    <br />
                    <label style="color: #337AB7; font-weight: bold;">Localidad: </label>
                    <asp:Label ID="grdlblLocalidadPF" runat="server" Text='<%# Item.LocalidadDTO.Depto %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Contacto" ItemStyle-Width="150px" HeaderStyle-CssClass="text-center" HeaderStyle-BorderColor="#ffffff">
                <ItemTemplate>
                    <asp:Panel ID="pnlTlfPJ" runat="server" Visible='<%# Item.Telefono == null ? false: Item.Telefono.Length > 0 %>'>
                        <label style="color: #337AB7; font-weight: bold;">Teléfono: </label>
                        <asp:Label ID="grdlblTlfPF" runat="server" Text='<%# Item.Telefono %>' />
                        <br />
                    </asp:Panel>
                    <asp:Panel ID="pnlEmailPJ" runat="server" Visible='<%# Item.Email == null ? false: Item.Email.Length > 0 %>'>
                        <label style="color: #337AB7; font-weight: bold;">Email: </label>
                        <asp:Label ID="grdlblEmailPF" runat="server" Text='<%# Item.Email %>' />
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <div class="mtop10">
                <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                No hay datos aún...
            </div>
        </EmptyDataTemplate>
    </asp:GridView>
     <br /> 
    <%--Firmantes--%>
    <h3 style="text-align:center">Firmantes Personas Juridicas</h3>
    <br /> 
    <%--Grilla de Firmantes PJ--%>
    <asp:GridView ID="grdFirmantesPJ" runat="server" AutoGenerateColumns="false" AllowPaging="false" Style="border: none; margin-top: 10px" GridLines="None"
        Width="100%" CssClass="table table-bordered mtop5" CellPadding="3" ItemType="DataTransferObject.FirmantesDTO">
        <AlternatingRowStyle BackColor="#efefef" />
        <Columns>
            <asp:TemplateField HeaderText="Firmante de..." HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" HeaderStyle-BorderColor="#ffffff">
                <ItemTemplate>
                    <asp:Label ID="firtit" Width="100%" runat="server" style="text-align:center;word-wrap: normal; word-break: break-all;" Text='<%# Item.Titular %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Apellido y Nombre/s" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" HeaderStyle-BorderColor="#ffffff">
                <ItemTemplate><%# Item.ApellidoNombres %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Tipo Doc." HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" HeaderStyle-BorderColor="#ffffff">
                <ItemTemplate><%# Item.DescTipoDocPersonal %> </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Documento" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" HeaderStyle-BorderColor="#ffffff">
                <ItemTemplate><%# Item.Nro_Documento %> </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Carácter Legal" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" HeaderStyle-BorderColor="#ffffff">
                <ItemTemplate><%# Item.nom_tipocaracter %> </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Cargo" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" HeaderStyle-BorderColor="#ffffff">
                <ItemTemplate><%# Item.cargo_firmante_pj %> </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <div class="mtop10">
                <img src='<%: ResolveUrl("~/Content/img/app/NoRecords.png") %>' alt="" />
                No hay datos aún...
            </div>
        </EmptyDataTemplate>
    </asp:GridView>
</asp:Panel>
