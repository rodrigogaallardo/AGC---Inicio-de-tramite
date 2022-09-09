using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataTrasnferObject;
using BaseRepository;
using BusinesLayer.Implementation;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayerUnitTest
{
    [TestClass]
    public class BlUnitTestDataBase
    {
        [TestMethod]
        public void ObtenerMenuesDesdeLabaseDeDATOSCompletos()
        {
            MenuesBL menuBL = new MenuesBL();
            var lstMenues = menuBL.GetAll().ToList();
            Assert.IsTrue(lstMenues.Count > 0);
        }
        [TestMethod]
        public void InsertIntoMenuesWithTransactionUnitOfWork()
        {
            MenuesBL menuBL = new MenuesBL();
            PerfilesBL perfilesBL = new PerfilesBL();

            var lstMenues = menuBL.GetAll().OrderByDescending(x => x.id_menu).ToList();
            var lstPerfiles = perfilesBL.GetAll().OrderByDescending(x => x.id_perfil).ToList();
            
            
            bool menuInsert= false;
            if (lstMenues.Count() > 0)
            {

                PerfilesDTO perfilesDTO = new PerfilesDTO()
                {
                    descripcion_perfil = "Demian Pruebas Borrar",
                    id_perfil = lstPerfiles[0].id_perfil + 1,
                    nombre_perfil = "Demian Borrar",
                    CreateDate = DateTime.Now,
                    CreateUser = new Guid("617F06DF-1EDE-46F5-9A2E-832E1E144AB4"),                    
                    LastUpdateDate = DateTime.Now,
                    LastUpdateUser = new Guid("617F06DF-1EDE-46F5-9A2E-832E1E144AB4")
                    

                };
                List<PerfilesDTO> lstPerfilesDto = new List<PerfilesDTO>();
                lstPerfilesDto.Add(perfilesDTO);
                
                
                MenuesDTO menuDto = new MenuesDTO()
                {
                    id_menu = lstMenues[0].id_menu + 1 ,
                    descripcion_menu="Testing MenuDemian",
                    aclaracion_menu="mediante esta opcion BLA BLA BLA",
                    pagina_menu="pruebas",
                    nroOrden=0,
                    visible=false,
                    PerfilesDtoCollection = lstPerfilesDto,
                    CreateDate = DateTime.Now,
                    CreateUser = new Guid("617F06DF-1EDE-46F5-9A2E-832E1E144AB4"),
                    LastUpdateDate = DateTime.Now,
                    LastUpdateUser = new Guid("617F06DF-1EDE-46F5-9A2E-832E1E144AB4"),

                };

                menuInsert = menuBL.Insert(menuDto);
                
            }
            Assert.IsTrue(menuInsert);

        }
    }
}
