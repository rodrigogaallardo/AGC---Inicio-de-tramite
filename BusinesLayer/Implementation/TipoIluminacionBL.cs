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
	public class TipoIluminacionBL : ITipoIluminacionBL<TipoIluminacionDTO>
    {               
		private TipoIluminacionRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public TipoIluminacionBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TipoIluminacionDTO, tipo_iluminacion>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<TipoIluminacionDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TipoIluminacionRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<tipo_iluminacion>, IEnumerable<TipoIluminacionDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public TipoIluminacionDTO Single(int Id )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TipoIluminacionRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(Id);
                var entityDto = mapperBase.Map<tipo_iluminacion, TipoIluminacionDTO>(entity);
     
                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

