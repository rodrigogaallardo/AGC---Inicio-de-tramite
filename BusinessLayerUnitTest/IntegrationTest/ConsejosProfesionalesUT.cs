using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinesLayer.Implementation;
using DataTransferObject;
using System.Linq;
using Moq;
using BaseRepository;
using Dal.UnitOfWork;
using DataAcess;

namespace BusinessLayerUnitTest.IntegrationTest
{
    /// <summary>
    /// Summary description for ConsejosProfesionalesUT
    /// </summary>
    [TestClass]
    public class ConsejosProfesionalesUT
    {

        Mock<ConsejoProfesionalRepository> moqRepository;

        public ConsejosProfesionalesUT()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion


        [TestInitialize]
        public void testInitialize()
        {
            moqRepository = new Mock<ConsejoProfesionalRepository>();


        }


        [TestMethod]
        public void TraerPerfilesProfesionalXGrupoFromDB()
        {            
           var role = new aspnet_Roles() { RoleId = new Guid("196F129D-FE7E-435A-9546-DF067C8E0CCD") };
            List<aspnet_Roles> list = new List<aspnet_Roles>();
            list.Add(role);
  
            moqRepository.Setup(x => x.TraerPerfilesProfesionalXGrupo(1)).Returns(list);
            ConsejoProfesionalBL consejoProfesionalBL = new ConsejoProfesionalBL();
            //consejoProfesionalBL.ConsejoProfesionalRepository = moqRepository.Object;
            var rolesGrupos = consejoProfesionalBL.TraerPerfilesProfesionalXGrupo(1);
            Assert.IsTrue(rolesGrupos.Count() > 0);

        }

       
        [TestMethod]
        public void  TraerPerfilesProfesionalXGrupoSobreCargadoFromDB()
        {
            Guid userid = Guid.Parse("CCE6ABD7-B743-4032-A46D-BEC599405701");


            var reUserClas = new Rel_UsuariosProf_Roles_Clasificacion()
            {
                UserID = Guid.Parse("CCE6ABD7-B743-4032-A46D-BEC599405701")
            };
            List<Rel_UsuariosProf_Roles_Clasificacion> listRelUserRolClas = new List<Rel_UsuariosProf_Roles_Clasificacion>();
            listRelUserRolClas.Add(reUserClas);
            moqRepository.Setup(x => x.TraerCalificacionesSeleccionadas(3, userid)).Returns(listRelUserRolClas);

            var role = new aspnet_Roles() { RoleId = new Guid("196F129D-FE7E-435A-9546-DF067C8E0CCD") };
            List<aspnet_Roles> list = new List<aspnet_Roles>();
            list.Add(role);
            moqRepository.Setup(x => x.TraerPerfilesProfesionalXGrupo(3, userid)).Returns(list);
            
            
            ConsejoProfesionalBL consejoProfesionalBL = new ConsejoProfesionalBL();
            //consejoProfesionalBL.ConsejoProfesionalRepository = moqRepository.Object;
            var rolesGrupos = consejoProfesionalBL.TraerPerfilesProfesionalXGrupo(3, userid);
            Assert.IsTrue(rolesGrupos.Count() > 0);

        }

        [TestMethod]
        public void TraerPerfilesProfesionalXGrupoSobreCargadoFromDB2()
        {
            Guid userid = Guid.Parse("84B58843-19D4-47AC-B7F6-00BC83D6A154");


            var reUserClas = new Rel_UsuariosProf_Roles_Clasificacion()
            {
                UserID = Guid.Parse("84B58843-19D4-47AC-B7F6-00BC83D6A154"),
                RoleID = Guid.Parse("77C764A7-1EAD-40B5-BF51-67FDC7F1B507")
            };
            List<Rel_UsuariosProf_Roles_Clasificacion> listRelUserRolClas = new List<Rel_UsuariosProf_Roles_Clasificacion>();
            listRelUserRolClas.Add(reUserClas);
            moqRepository.Setup(x => x.TraerCalificacionesSeleccionadas(3, userid)).Returns(listRelUserRolClas);

            var role = new aspnet_Roles() { RoleId = new Guid("77C764A7-1EAD-40B5-BF51-67FDC7F1B507") };
            List<aspnet_Roles> list = new List<aspnet_Roles>();
            list.Add(role);
            moqRepository.Setup(x => x.TraerPerfilesProfesionalXGrupo(3, userid)).Returns(list);


            ConsejoProfesionalBL consejoProfesionalBL = new ConsejoProfesionalBL();
            //consejoProfesionalBL.ConsejoProfesionalRepository = moqRepository.Object;
            var rolesGrupos = consejoProfesionalBL.TraerPerfilesProfesionalXGrupo(3, userid);
            Assert.IsTrue(rolesGrupos.Count() > 0);

        }

        [TestMethod]
        public void GetConsejosxGrupoFromDB()
        {
            ConsejoProfesional conPro = new ConsejoProfesional()
            {
                id_grupoconsejo = 1
            };
            List<ConsejoProfesional> listConPro = new List<ConsejoProfesional>();
            listConPro.Add(conPro);
            moqRepository.Setup(x => x.GetConsejosxGrupo(1)).Returns(listConPro);

            ConsejoProfesionalBL consejoProfesionalBL = new ConsejoProfesionalBL();
            //consejoProfesionalBL.ConsejoProfesionalRepository = moqRepository.Object;
            IEnumerable<ConsejoProfesionalDTO> consejos = consejoProfesionalBL.GetConsejosxGrupo(1);
            Assert.IsTrue(consejos.Count() > 0);

        }
            
        [TestMethod]
        public void GetGrupoConsejoFromDB()
        {
            Guid userid = Guid.Parse("448572E1-A782-4FB0-8F3C-6195E490E5F0");
            ConsejoProfesional conPro = new ConsejoProfesional();
 
            List<ConsejoProfesional> listConPro = new List<ConsejoProfesional>();
            listConPro.Add(conPro);
            moqRepository.Setup(x => x.GetGrupoConsejo(userid)).Returns(listConPro);

            ConsejoProfesionalBL consejoProfesionalBL = new ConsejoProfesionalBL();
            //consejoProfesionalBL.ConsejoProfesionalRepository = moqRepository.Object;
            IEnumerable<ConsejoProfesionalDTO> consejos = consejoProfesionalBL.GetGrupoConsejo(userid);
            Assert.IsTrue(consejos.Count() > 0);
        }

    }
}
