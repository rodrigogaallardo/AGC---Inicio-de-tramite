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
	public class TiposDePlanosBL : ITiposDePlanosBL<TiposDePlanosDTO>
    {               
		private TiposDePlanosRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public TiposDePlanosBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TiposDePlanosDTO, TiposDePlanos>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<TiposDePlanosDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TiposDePlanosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<TiposDePlanos>, IEnumerable<TiposDePlanosDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public TiposDePlanosDTO Single(int Id )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TiposDePlanosRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(Id);
                var entityDto = mapperBase.Map<TiposDePlanos, TiposDePlanosDTO>(entity);
     
                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

