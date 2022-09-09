using BusinesLayer.Implementation;
using DataTransferObject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayerUnitTest
{
    [TestClass]
    public class ConsultaPadronUnitTest
    {
        Guid CreateUser = Guid.Parse("81D7CA25-757E-4D2E-B77D-1DBC54900F3C");

        [TestMethod]
        public void Single()
        {
            ConsultaPadronSolicitudesBL consultaPadronBL = new ConsultaPadronSolicitudesBL();

            var consultaDTO = consultaPadronBL.Single(3304);

            Assert.IsTrue(consultaDTO != null);

        }

        public void CrearTransferencia()
        {
            TransferenciasSolicitudesBL bl = new TransferenciasSolicitudesBL();
            bl.CrearTransferencia(356, "6W6C", Guid.Parse("C17D3C7E-665C-4201-951E-0F1B6C5C514F"));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void SingleTransferencia()
        {

            TransferenciasSolicitudesBL bl = new TransferenciasSolicitudesBL();
            bl.Single(290);
        }

        //[TestMethod]
        //public void InstanceBL()
        //{
        //    EncomiendaBuscadorBL encomienda = new EncomiendaBuscadorBL();
        //    int totalRowCount=0;
        //    var dtos =   encomienda.GetByTramiteEstado(Guid.Parse("B747C4DD-019C-46FE-8020-39FC70B6D143"), 0, -1, -1, 0, 0, 100, out totalRowCount);
        //    Assert.IsTrue(dtos.Any()); 
        //}
    }
}
