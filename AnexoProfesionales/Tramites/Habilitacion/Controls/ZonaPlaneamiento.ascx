<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ZonaPlaneamiento.ascx.cs" Inherits="AnexoProfesionales.Tramites.Habilitacion.Controls.ZonaPlaneamiento" %>

<asp:UpdatePanel ID="updZonaPlaneamiento" runat="server" >
      <ContentTemplate>

        <asp:panel ID="pnlZonaPlaneamiento"  CssClass="box-panel mtop20" runat="server" Style="display:none">
                 <asp:Label ID="lblZonaSeleccionadaI" runat="server" style="margin-top: 10px; display: none">
            <strong>Distrito Industrial - I</strong><br />
                                                
            <br /> Son zonas destinadas al agrupamiento de las actividades manufactureras y de servicio cuya<br />
            área de mercado es predominantemente la Capital Federal y por sus características admiten ser localizadas <br />
            en el ejido urbano.<br />
            <br /><b>I1</b>Industrial Exclusivo;
            <br /><b>I2</b>Industrial compatible con el uso residencial en forma restringida.<br /><br />

                En estas zonas, solo podrá comenzar la explotación comercial quien posea la normativa específica del Ministerio de Desarrollo Urbano y Transporte que autorice su uso.
        </asp:Label>
                 <asp:Label ID="lblZonaSeleccionadaP" runat="server" style="margin-top: 10px; display: none">
            <strong>Distrito Portuario - P</strong><br />
                                                
            <br /> Área afectada a la actividad portuaria que requiere que requiere condiciones especiales para su desarrollo.<br /><br />

                En estas zonas, solo podrá comenzar la explotación comercial quien posea la normativa específica del Ministerio de Desarrollo Urbano y Transporte que autorice su uso.
        </asp:Label>
                 <asp:Label ID="lblZonaSeleccionadaU" runat="server" style="margin-top: 10px; display: none">
            <strong>Distritos Urbanizaciones Determinadas - U</strong><br />
                                                
            <br />Corresponde a distritos que, con la finalidad de establecer o preservar conjuntos urbanos de<br />
                características diferentes, son objeto de regulación integral en materia de uso, ocupación,<br />
                subdivisión del suelo y plástica urbana.<br /><br />

                En estas zonas, solo podrá comenzar la explotación comercial quien posea la normativa específica del Ministerio de Desarrollo Urbano y Transporte que autorice su uso.
        </asp:Label>
                 <asp:Label ID="lblZonaSeleccionadaAR" runat="server" style="margin-top: 10px; display: none">
            <strong>Distritos Arquitectra Especial - AE</strong><br />
                                                
            <br />Ámbitos o recorridos urbanos que poseen una identidad reconocible por sus característicasz <br /> 
            fisicas particulares, que son objeto de normas para obra nueva referidas a aspectos formales, <br />
            proporciones y relaciones de los edificios con su entorno.<br /><br />

                En estas zonas, solo podrá comenzar la explotación comercial quien posea la normativa específica del Ministerio de Desarrollo Urbano y Transporte que autorice su uso.
        </asp:Label>
                 <asp:Label ID="lblZonaSeleccionadaRU" runat="server" style="margin-top: 10px; display: none">
            <strong>Distritos Renovación Urbana - RU</strong><br />
                                                                                               
            <br />Corresponden a áreas en las que existe una necesidad de reestructuración integral:<br />
            -Por absolescencia de algunos de sus sectores o elementos;<br />
            -Por afectación a obras trascendetes de interés publico.<br />
            -Por sus particulares condiciones de deterioro físicos y económicos social.<br />
            -La afectación a Distrito RU implica que, por el término de 2 años a contar desde la adopción<br />
            de la medida, no se podrá modificar el estado actual de los usos y construcciones, pudiendo<br />
            solamente llevarse a cabo obras de conservación y mantenimiento.<br /><br />
                                                 
                En estas zonas, solo podrá comenzar la explotación comercial quien posea la normativa específica del Ministerio de Desarrollo Urbano y Transporte que autorice su uso.
        </asp:Label>  
                 <asp:Label ID="lblZonaSeleccionadaUF" runat="server" style="margin-top: 10px; display: none">
            <strong>Distrito Urbanización Futura - UF</strong><br />
                                                                                               
            <br />Corresponden a terrenos de propiedad pública, aún no urbanizados u ocupados por instalaciones<br />
            y usos pasibles de remoción futura, así como a las tierras destinadas a uso ferroviario, zona de vías,<br />
            playas de maniobras, estaciones y terrenos aledaños a esos usos.<br />
            Estos distritos están destinados a desarrollos urbanos integrales que exigen un plan de <br />
            conjunto previo, en base a normas y programas especiales.<br /><br />

                En estas zonas, solo podrá comenzar la explotación comercial quien posea la normativa específica del Ministerio de Desarrollo Urbano y Transporte que autorice su uso.
        </asp:Label>
                 <asp:Label ID="lblZonaSeleccionadaUP" runat="server" style="margin-top: 10px; display: none">
            <strong>Distrito Urbanización Parque - UP</strong><br />
                                                                                               
            <br />Corresponden a áreas destinadas a espacios verdes y parquización de uso público.<br /><br />

                En estas zonas, solo podrá comenzar la explotación comercial quien posea la normativa específica del Ministerio de Desarrollo Urbano y Transporte que autorice su uso.
        </asp:Label>
                 <asp:Label ID="lblZonaSeleccionadaARE" runat="server" style="margin-top: 10px; display: none">
            <strong>Distrito Área de Reserva Ecológica - ARE</strong><br />
                                                                                               
            <br />Áreas por su carácter ambiental, su configuración física y su dinámica evolutiva, dan lugar<br />
            a la conformación de ambientes naturales donde las distintas especies de su flora y fauna puedan<br />
                mantenerse a perpetuidad o incluso aumentar su densiadad, ya sea mediante el mantenimiento de <br />
            las condiciones naturales o con el aporte de un manejo científico.<br />

                En estas zonas, solo podrá comenzar la explotación comercial quien posea la normativa específica del Ministerio de Desarrollo Urbano y Transporte que autorice su uso.
        </asp:Label>
                 <asp:Label ID="lblZonaSeleccionadaADP" runat="server" style="margin-top: 10px; display: none">
            <strong>Distrito Área de Desarrollo Prioritario - ADP</strong><br /><br />
                                                                                               
            <br />Son aquellos polígonos que se delimitan para lograr los objetivos de Art. 8.1.2 por medio de la<br />
            realización de desarrollos públicos o privados superadores de la situación actual. La zonificación<br />
            preexistente a la delimitación de un área de desarrollo prioritario mantendrá plena vigencia en<br />
            todo lo que no sea objeto de un convenio urbanístico.<br /><br />

            En estas zonas, solo podrá comenzar la explotación comercial quien posea la normativa específica del Ministerio de Desarrollo Urbano y Transporte que autorice su uso.
        </asp:Label>
                 <asp:Label ID="lblZonaSeleccionadaAPH" runat="server" style="margin-top: 10px; display: none">
                 <strong>Área de Protección Historica - APH</strong> <br /><br />
                Las Áreas de Protección Histórica (APH) son zonas, espacios o conjuntos urbanos que por sus valores históricos, arquitectónicos, 
                singulares o ambientales constituyen ámbitos claramente identificables como referentes de nuestra cultura.
                En estas zonas, solo podrá comenzar la explotación comercial quien posea la normativa especifica del Ministerio de Desarrollo Urbano y Transporte que autorice su uso. 
                <div class="pull-right">
                         <input type="button" class="btn btn-primary mtop20" value="¿Cómo hago el trámite de APH?" onclick="showModalAPH();" />
             </div>
          </asp:Label>
        </asp:panel>

        <asp:panel ID="pnlAPH"  CssClass="box-panel" runat="server" Style="display: none">
                <strong> Requisitos Del Trámite</strong><br />
                Los interesados en iniciar este trámite ante la Dirección General de Registro de Obras y Catastro (DGROC) deberán completar los datos requeridos en el formulario y adjuntar la documentación que a continuación se detalla:
                <br /><br />
                <ul>
                    <li>Documentación que acredite la titularidad del Dominio (Título de Propiedad) autenticado ante Escribano</li>
                    <li>Constancia de Ingresos Brutos</li>
                    <li>Constancia de CUIT</li>
                    <li>Memoria descriptiva – técnica indicando tareas a realizar, métodos, materiales y colores a emplear Adicionalmente, según el caso deberá presentar:</li>
                    <li>Si el interesado fuera “Persona Jurídica” deberá presentar el correspondiente Estatuto Social autenticado ante Escribano</li>
                    <li>Relevamiento fotográfico del predio y de sus espacios linderos, así como del ámbito de la calle, indicando el predio en cuestión y la fecha en la que se realizaron las tomas</li>
                    <li>De realizarse alguna intervención sobre la fachada del inmueble deberá presentar Plano de Fachada con resolución arquitectónica que involucren sus linderos en escala 1:50</li>
                    <li>De tratarse de un inmueble subdividido por el Régimen de Propiedad Horizontal, deberá presentarse el correspondiente</li>
                    <li>Reglamento de Copropiedad autenticado ante Escribano</li>
                    <li>Certificado de Consulta de Registro Catastral</li>
                    <li>Certificado de Solicitud de Perímetro y ancho de calle</li>
                    <li>Relevamiento fotográfico del predio y de sus linderos que conforman el pulmón de la manzana indicando el predio en cuestión y la fecha en la que se realizaron las tomas</li>
                    <li>Plano de propuesta con plantas, vistas, cortes y balance de superficies</li>
                    <li>Siluetas y cómputos de las superficies a compensar</li>
                    <li>Axonométrica del proyecto desde el frente y contrafrente con los edificios linderos debidamente acotados</li>
                    <li>Relevamiento de los muros divisorios de predio con los patios que posean debidamente acotados a NPT (dicha documentación deberá ser verificada y suscripta por un profesional matriculado y especialista en el tema)</li>
                    <li>Plano de Obra registrado de lo existente otorgado por la Dirección General de Registro de Obras y Catastro y/o plano de Aguas Argentinas (AySA) o en su defecto certificado que acredite la falta de plano Trazado de LFI y LBI. En caso de ser una manzana atípica, se deberá entregar copia de LFI particularizada</li>
                    <li>Plano adicional (en formato DWF)</li>
                    <li>Otra documentación</li>
                </ul>
                <br /><br />
                La documentación deberá estar firmada por el propietario y el profesional actuante.
                Luego del inicio del trámite se procederá a efectuar el derecho de timbrado (Ley Tarifaria), el cual deberá ser abonado y posteriormente se requerirá el ingreso del comprobante de pago correspondiente.
                     <div class="pull-right mtop20">
                         <asp:LinkButton ID="LinkButton1" CssClass="btn btn-default" Text="Cerrar" OnClientClick="return hideModalAPH();" runat="server"></asp:LinkButton>
                     </div>
           </asp:panel>


     </ContentTemplate>
 </asp:UpdatePanel>