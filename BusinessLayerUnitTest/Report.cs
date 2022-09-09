using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reporting;
using ExternalService;

namespace BusinessLayerUnitTest
{
    [TestClass]
    public class Report
    {
        [TestMethod]
        public void testvalidacion()
        {
            bool TieneRubroProTeatro = true;
            bool TieneDocProTeatro = true;

            bool TieneRubroCentroCultural = true;
            bool TieneDocCentroCultural = true;


            bool TieneRubroEstadio = true;


            bool rubroproteatro = TieneRubroProTeatro ? !(TieneRubroProTeatro && TieneDocProTeatro) : false;

            bool rubrocentrocult = TieneRubroCentroCultural ? !(TieneRubroCentroCultural && TieneDocCentroCultural) : false;

            if ((rubroproteatro|| rubrocentrocult) || !(TieneRubroProTeatro || TieneRubroCentroCultural))
            {

            }
        }

        [TestMethod]
        public void DatosReporte()
        {
            MailMessages asd = new MailMessages();
            asd.MailWelcome("ASD", "ASD", "ASD", "ASD");
        }

        [TestMethod]
        public void ReporteEncomiendaAntena()
        {
            CommonReport.ImprimirCertificadoEncomiendaAnt(50);
        }
    }
}
