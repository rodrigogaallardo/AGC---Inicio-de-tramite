using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataTransferObject;
using BaseRepository.Engine;
using BusinesLayer.Implementation;
using System.Collections.Generic;
using System.Linq;
using UnitOfWork;

namespace BusinessLayerUnitTest
{

    [TestClass]
    public class EngineUnitTest
    {
        [TestMethod]
        public void ObtenerLosItemDirectionEncomiendaEntityDesdeRepository()
        {

            EngineBL engineBL = new EngineBL();
            var userAssigned = engineBL.GetUltimoUsuarioAsignado(301851,300);


            Assert.IsTrue( userAssigned != null);

        }

        [TestMethod]
        public void ControlarTareaConElPerfilDelUSUARIOLogueado()
        {
            EngineBL engineBL = new EngineBL();
            var checkTareaPerfil = engineBL.CheckRolTarea(206643, new Guid("388C1CA4-56AA-4422-8149-8B9C23B0B3F5"));
            Assert.IsTrue(checkTareaPerfil);

        }

        [TestMethod]
        public void ControlarTareaConEdicionDelUsuarioLogueado()
        {
            EngineBL engineBL = new EngineBL();
            var checkTareaPerfil = engineBL.CheckEditTarea(206643, new Guid("388C1CA4-56AA-4422-8149-8B9C23B0B3F5"));
            Assert.IsTrue(checkTareaPerfil);

        }

        [TestMethod]
        public void TomarTareasConUsuario()
        {
            EngineBL engineBL = new EngineBL();
            var checkTareaPerfil = engineBL.TomarTarea(206643, new Guid("388C1CA4-56AA-4422-8149-8B9C23B0B3F5"));
            Assert.IsTrue(checkTareaPerfil == "Tarea Asignada");

        }


        [TestMethod]
        public void AsignarTareasConUsuario()
        {
            EngineBL engineBL = new EngineBL();
            var assignedWork = engineBL.AsignarTarea(1, new Guid("3AE30684-3FE4-40C1-BB12-781B94B74252"), new Guid("3AE30684-3FE4-40C1-BB12-781B94B74252"));
            Assert.IsTrue(assignedWork == 1);

        }
        [TestMethod]
        public void ReAsignarTareasConUsuario()
        {
            EngineBL engineBL = new EngineBL();
            var assignedWork = engineBL.ReasignarTarea(330991, new Guid("6B5FC058-12A1-4305-BD28-91CE11BDBCCB"));
            Assert.IsTrue(assignedWork > 1);

        }

        [TestMethod]
        public void ObtenerTareaSiguiente()
        {
            EngineBL engineBL = new EngineBL();
            var assignedWork = engineBL.GetTareasSiguientes(55, 400, 363148);
            Assert.IsTrue(true);

        }


        [TestMethod]
        public void CrearTarea()
        {
            EngineBL engineBL = new EngineBL();
            var assignedWork = engineBL.CrearTarea(200929, 10, new Guid("6B5FC058-12A1-4305-BD28-91CE11BDBCCB"));
            Assert.IsTrue(assignedWork > 0);

        }


        [TestMethod]
        public void FinalizarTarea()
        {
            EngineBL engineBL = new EngineBL();
            var assignedWork = engineBL.FinalizarTarea(326754, 23, 105, new Guid("F52300C8-572C-4CE5-BA12-26FD33ED38DE"));
            Assert.IsTrue(assignedWork > 0);

        }
    }
}
