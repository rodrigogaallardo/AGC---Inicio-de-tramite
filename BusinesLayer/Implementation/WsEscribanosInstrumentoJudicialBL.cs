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

namespace BusinesLayer.Implementation
{
	public class WsEscribanosInstrumentoJudicialBL : IWsEscribanosInstrumentoJudicialBL<wsEscribanosInstrumentoJudicialDTO>
    {               
		private wsEscribanosInstrumentoJudicialRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public WsEscribanosInstrumentoJudicialBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<wsEscribanosInstrumentoJudicialDTO, wsEscribanos_InstrumentoJudicial>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }
		
        public wsEscribanosInstrumentoJudicialDTO Single(int id_insjud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new wsEscribanosInstrumentoJudicialRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(id_insjud);
                var entityDto = mapperBase.Map<wsEscribanos_InstrumentoJudicial, wsEscribanosInstrumentoJudicialDTO>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<wsEscribanosInstrumentoJudicialDTO> GetByFKIdActanotarial(int id_actanotarial)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new wsEscribanosInstrumentoJudicialRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdActanotarial(id_actanotarial);
            var entityDto = mapperBase.Map<IEnumerable<wsEscribanos_InstrumentoJudicial>, IEnumerable<wsEscribanosInstrumentoJudicialDTO>>(elements);
            return entityDto;
        }
    }
}

