using System;
using System.Collections.Generic;
using DataAcess;
using DataTransferObject;
using BaseRepository;
using AutoMapper;
using System.Linq;
using UnitOfWork;
using IBusinessLayer;

namespace BusinesLayer.Implementation
{
    public class CallesBL : ICallesBL<CallesDTO>
    {
        private CallesRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;


        public CallesBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CallesDTO, Calles>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CallesDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new CallesRepository(this.uowF.GetUnitOfWork());

                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Calles>, IEnumerable<CallesDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CallesDTO> GetCalles()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new CallesRepository(this.uowF.GetUnitOfWork());
                var elementsAll = repo.GetAll().ToList();

                var elements = from c in elementsAll
                    group c by new
                    {
                        c.Codigo_calle,
                        c.NombreOficial_calle
                    } into g
                    select new CallesDTO
                    {
                        Codigo_calle = g.Key.Codigo_calle,
                        NombreOficial_calle=  g.Key.NombreOficial_calle
                    };              
                
                //var elementsDto = mapperBase.Map<IEnumerable<Calles>, IEnumerable<CallesDTO>>(elements);
                return elements.OrderBy(x => x.NombreOficial_calle); 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public IEnumerable<CallesDTO> Get(int Codigo)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new CallesRepository(this.uowF.GetUnitOfWork());

                var elements = repo.Get(Codigo);
                var elementsDto = mapperBase.Map<IEnumerable<Calles>, IEnumerable<CallesDTO>>(elements);

                return elementsDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GetNombre(int Codigo, int Nro)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new CallesRepository(this.uowF.GetUnitOfWork());
                int AlturaInicio=0;
                int AlturaFin=0;
                string Result = string.Empty;
                var elements = repo.Get(Codigo);
                foreach(var element in elements)
                {
                    AlturaInicio = element.AlturaDerechaInicio_calle.Value;
		            if (element.AlturaIzquierdaInicio_calle < element.AlturaDerechaInicio_calle)
			            AlturaInicio = element.AlturaIzquierdaInicio_calle.Value;
		
		            AlturaFin = element.AlturaDerechaFin_calle.Value;
		            if (element.AlturaIzquierdaFin_calle > element.AlturaDerechaFin_calle)
			            AlturaFin = element.AlturaIzquierdaFin_calle.Value;

                    if (Nro >= AlturaInicio && Nro <= AlturaFin)
                        Result = element.NombreOficial_calle;
                }
                
                return Result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}