using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExternalService;
using StaticClass;
using BusinesLayer.Implementation;

namespace BusinessLayerUnitTest.EsternalServiceTest
{
    [TestClass]
    public class UTMail
    {
        [TestMethod]
        public void TestMail()
        {
            EmailServiceBL externalServiceMail = new EmailServiceBL();
            EmailEntity mailEntity = new EmailEntity()
            {
                Asunto = "TEST",
                CantIntentos = 3,
                CantMaxIntentos = 3,
                Email = "daniel.melgarejo@grupomost.com",
                FechaAlta = DateTime.Now,
                FechaEnvio = DateTime.Now,
                Guid = Guid.NewGuid(),
                Html = "Este es un email de prueba",
                IdEstado = 1,
                IdOrigen = 1,
                IdTipoEmail = 1,
                Prioridad = 1
            };
            
            Assert.IsTrue( externalServiceMail.SendMail(mailEntity) <= 0);
        }
    }
}
