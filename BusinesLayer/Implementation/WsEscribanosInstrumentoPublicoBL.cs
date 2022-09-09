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
	public class WsEscribanosInstrumentoPublicoBL : IWsEscribanosInstrumentoPublicoBL<wsEscribanosInstrumentoPublicoDTO>
    {               
		private wsEscribanosInstrumentoPublicoRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public WsEscribanosInstrumentoPublicoBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<wsEscribanosInstrumentoPublicoDTO, wsEscribanos_InstrumentoPublico>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }
		
        public wsEscribanosInstrumentoPublicoDTO Single(int id_inspub)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new wsEscribanosInstrumentoPublicoRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(id_inspub);
                var entityDto = mapperBase.Map<wsEscribanos_InstrumentoPublico, wsEscribanosInstrumentoPublicoDTO>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<wsEscribanosInstrumentoPublicoDTO> GetByFKIdActanotarial(int id_actanotarial)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new wsEscribanosInstrumentoPublicoRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdActanotarial(id_actanotarial);
            var entityDto = mapperBase.Map<IEnumerable<wsEscribanos_InstrumentoPublico>, IEnumerable<wsEscribanosInstrumentoPublicoDTO>>(elements);
            return entityDto;
        }
    }
}

