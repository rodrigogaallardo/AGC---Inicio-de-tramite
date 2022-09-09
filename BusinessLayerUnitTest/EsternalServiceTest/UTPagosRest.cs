using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExternalService;
using StaticClass;

namespace BusinessLayerUnitTest.EsternalServiceTest
{
    [TestClass]
    public class UTPagosRest
    {
        [TestMethod]
        public void TestCancelarPago()
        {
            ExternalServicePagos externalService = new ExternalServicePagos();
            externalService.CancelarPago(3);
            Assert.IsTrue( true);
        }
    }
}
