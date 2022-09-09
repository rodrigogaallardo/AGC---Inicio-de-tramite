using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataTransferObject;
using BaseRepository;
using StaticClass;
using BusinesLayer.Implementation;
using System.Collections.Generic;
using System.Linq;
using BusinesLayer.MappingConfig;

namespace BusinessLayerUnitTest
{
    [TestClass]
    public class BlUnitTestDataBase
    {
        [TestMethod]
        public void TestMapping()
        {
            AutoMapperConfig.RegisterMappingEncomienda();
            EncomiendaBL encBL = new EncomiendaBL();
            var encomiendaDTOFULL = encBL.Single(158373);

            //SSITSolicitudesBL solBL = new SSITSolicitudesBL();
            //var ssitSolDTO = solBL.Single(301520);


            //SSITSolicitudesTitularesPersonasFisicasBL soltitpfBL = new SSITSolicitudesTitularesPersonasFisicasBL();
            //var soltitPFDTO = soltitpfBL.Single(1);

            Assert.IsTrue(encomiendaDTOFULL != null);
        }

        [TestMethod]
        public void ObtenerRUBROSDesdeLabaseDeDATOS()
        {
            RubrosBL tipoDocumentacionRequeridaBL = new RubrosBL();
            var rubros = tipoDocumentacionRequeridaBL.GetAll().ToList();
            Assert.IsTrue(rubros.Count > 0);
        }
        
        [TestMethod]
        public void GetMenuesFromDatabase()
        {
            MenuesBL menuBL = new MenuesBL();
            var lstMenues = menuBL.GetMenuByUserId(new Guid("5674BEEF-AFEA-4ADE-AEC3-6774E251BAF2")).OrderByDescending(x => x.id_menu).ToList();          
            Assert.IsTrue(lstMenues.Count > 0);

        }

        [TestMethod]
        public void GetProfesionalFromDatabase()
        {
            ProfesionalesBL blProf = new ProfesionalesBL();
            ProfesionalDTO prof = blProf.Get(new Guid("33C830B8-07D4-418F-86C5-8F153DF2BF5A"));
            Assert.IsTrue(prof != null);
        }

        [TestMethod]
        public void TraerTitularesDesdeLaBaseDeDatos()
        {
            //TitularesBL titubl = new TitularesBL();
            //var titulares = titubl.GetTitulares(73);
            //Assert.IsTrue(titulares.Count() > 0);
        }

        [TestMethod]
        public void TraerFirmantesDesdeLaBaseDeDatos()
        {
            //FirmantesBL firmantesBL = new FirmantesBL ();
            //var firmantes = firmantesBL.GetFirmantes(10987);
            //Assert.IsTrue(firmantes.Count() > 0);
        }
        [TestMethod]
        public void GetRubros()
        {
            EncomiendaRubrosBL rubrosBL = new EncomiendaRubrosBL();
            var rubros = rubrosBL.GetRubros(100000, 30, "601040", "C3");
            Assert.IsTrue(rubros.Count() > 0);
        }
        [TestMethod]
        public void GetRubrosSolo()
        {
            RubrosBL rubrosBL = new RubrosBL();
            var rubros = rubrosBL.Get("Josedo");
            Assert.IsTrue(rubros.Any());
        }        

        [TestMethod]
        public void CompararSSITSolicitudes()
        {
            SSITSolicitudesBL ssit = new SSITSolicitudesBL();
            var areEquals = ssit.CompareWithEncomienda(302849);
            Assert.IsTrue(areEquals);
        }

        [TestMethod]
        public void CompareEncomiedaWITHotherEncomienda()
        {
            EncomiendaBL encomiendaBL = new EncomiendaBL();
            var areEquals = encomiendaBL.CompareBetween(94491, 94489);
            Assert.IsFalse(areEquals);
        }
        [TestMethod]
        public void SelectAllGetCallesDistinctFromDatabase()
        {
            CallesBL calles = new CallesBL();

            var callesDTO = calles.GetCalles().ToList();
            Assert.IsTrue(callesDTO.Count() > 0);
        }

        [TestMethod]
        public void GetTipoDeInstructivos()
        {
            InstructivosBL instructivosBL = new InstructivosBL();
            var areEquals = instructivosBL.getInstuctivo(Instructivos_tipos.DGHyP_Habilitaciones);
            Assert.IsTrue(areEquals != null);
        }

        //[TestMethod]
        //public void ObtenerUntitularFromDataBase()
        //{
        //    TitularesBL titularesBL = new TitularesBL();
        //    var lstTitulares = titularesBL.GetTitulares(id_encomienda).ToList();
        //    Assert.IsTrue(areEquals != null);
        //}

        [TestMethod]
        public void iNSERTssITsOLICITUDeNdb()
        {
            SSITSolicitudesBL ssitSolicitudesBL = new SSITSolicitudesBL();

            SSITSolicitudesDTO ssitSolicitudesDTO = new SSITSolicitudesDTO()
            {
                CodigoSeguridad = "AA44",
                CreateDate = DateTime.Now,
                CreateUser = new Guid("2B086828-682B-442A-B2C7-789F6E1717D7"),
                IdEstado = 0,
                IdSubTipoExpediente = 1,
                IdTipoExpediente = 1,
                IdTipoTramite = 1,
                NroExpediente = "1"
            };

            Assert.IsTrue( ssitSolicitudesBL.Insert(ssitSolicitudesDTO) > 0);

        
        }


        [TestMethod]
        public void getOneEnvcomiendaFormDatabase()
        {
            EncomiendaBL encomiendaBl = new EncomiendaBL();
            var encomienda = encomiendaBl.Single(10040);
            Assert.IsTrue(encomienda != null);
        }

        [TestMethod]
        public void DeleteUbicacionConsultaPadron()
        {
            ConsultaPadronUbicacionesBL consultaPadronUbicacionesBL = new ConsultaPadronUbicacionesBL();
            ConsultaPadronUbicacionesDTO dto = new ConsultaPadronUbicacionesDTO() 
            { 
                IdConsultaPadronUbicacion = 256 ,
                IdConsultaPadron = 512
            }; 
            consultaPadronUbicacionesBL.Delete(dto);
        
        }

        [TestMethod]
        public void ConfirmarAnexoTecnicoEnEncomiendaBL()
        {
            EncomiendaBL encomiendaBL = new EncomiendaBL();
            var confirmarAnexo = encomiendaBL.ConfirmarAnexoTecnico(94381, new Guid("DFC718C3-4661-46AF-BEEC-E17DFD10DD96"));
            Assert.IsTrue(confirmarAnexo);
        }
        [TestMethod]
        public void Traer_EncomiendaExt_HistorialEstados()
        {
            EncomiendaBL encomiendaBL = new EncomiendaBL();
            var confirmarAnexo = encomiendaBL.Traer_EncomiendaExt_HistorialEstados(10);
            
            Assert.IsTrue(confirmarAnexo.Any());
        }
        [TestMethod]
        public void TraerObservaciones()
        {
            TransferenciasSolicitudesObservacionesBL blObs = new TransferenciasSolicitudesObservacionesBL();
            var list = blObs.GetByFKIdSolicitud(7);

            Assert.IsTrue(list.Count() > 0 ); 
        }
    }
}
