using AutoMapper;
using BaseRepository;
using IBusinessLayer;
using Dal.UnitOfWork;
using DataAcess;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitOfWork;

namespace BusinesLayer.Implementation
{
	public class CargosBL : ICargosBL<CargosDTO>
    {               
		private CargosRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public CargosBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CargosDTO, Cargos>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<CargosDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new CargosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Provincia>, IEnumerable<CargosDTO>>(elements);
                return elementsDto; 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CargosDTO> GetCargos()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new CargosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetCargos();
                var elementsDto = mapperBase.Map<IEnumerable<Cargos>, IEnumerable<CargosDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

