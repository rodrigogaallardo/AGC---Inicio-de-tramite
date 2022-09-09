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
using StaticClass;
using DataAcess.EntityCustom;

namespace BusinesLayer.Implementation
{
	public class WsEscribanosActaNotarialBL : IWsEscribanosActaNotarialBL<wsEscribanosActaNotarialDTO>
    {               
		private wsEscribanosActaNotarialRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
        IMapper mapperBaseAN;
		         
        public WsEscribanosActaNotarialBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<wsEscribanosActaNotarialDTO, wsEscribanos_ActaNotarial>()
                    .ForMember(dest => dest.Escribano, source => source.MapFrom(p => p.Escribano));

                cfg.CreateMap<wsEscribanos_ActaNotarial, wsEscribanosActaNotarialDTO>()
                    .ForMember(dest => dest.Escribano, source => source.MapFrom(p => p.Escribano)); 

                cfg.CreateMap<Escribano, EscribanoDTO>();

                cfg.CreateMap<EscribanoDTO, Escribano>();

            });

            mapperBase = config.CreateMapper();
        }
		
		public wsEscribanosActaNotarialDTO Single(int id_actanotarial)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new wsEscribanosActaNotarialRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(id_actanotarial);
                var entityDto = mapperBase.Map<wsEscribanos_ActaNotarial, wsEscribanosActaNotarialDTO>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdEncomienda"></param>
		/// <returns></returns>	
        public IEnumerable<wsEscribanosActaNotarialDTO> GetByFKListIdEncomienda(List<int> lista)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new wsEscribanosActaNotarialRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByFKListIdEncomienda(lista);
                var elementsDto = mapperBase.Map<IEnumerable<wsEscribanos_ActaNotarial>, IEnumerable<wsEscribanosActaNotarialDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public wsEscribanosActaNotarialDTO GetByFKIdEncomienda(int IdEncomienda)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new wsEscribanosActaNotarialRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdEncomienda(IdEncomienda);
            var elementsDto = mapperBase.Map<wsEscribanos_ActaNotarial, wsEscribanosActaNotarialDTO>(elements);
            return elementsDto;
        }

        public bool CopiarDesdeAPRA(int IdEncomienda, int IdCaa)
        {
            bool ret = false;
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new wsEscribanosActaNotarialRepository(this.uowF.GetUnitOfWork());
            ret = repo.CopiarDesdeAPRA(IdEncomienda, IdCaa);
            return ret;
        }
        

    }
}

