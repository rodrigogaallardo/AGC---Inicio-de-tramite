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
	public class WsEscribanosInstrumentoPrivadoBL : IWsEscribanosInstrumentoPrivadoBL<wsEscribanosInstrumentoPrivadoDTO>
    {               
		private wsEscribanosInstrumentoPrivadoRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public WsEscribanosInstrumentoPrivadoBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<wsEscribanosInstrumentoPrivadoDTO, wsEscribanos_InstrumentoPrivado>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }
		
        public wsEscribanosInstrumentoPrivadoDTO Single(int id_inspriv)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new wsEscribanosInstrumentoPrivadoRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(id_inspriv);
                var entityDto = mapperBase.Map<wsEscribanos_InstrumentoPrivado, wsEscribanosInstrumentoPrivadoDTO>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<wsEscribanosInstrumentoPrivadoDTO> GetByFKIdActanotarial(int id_actanotarial)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new wsEscribanosInstrumentoPrivadoRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdActanotarial(id_actanotarial);
            var entityDto = mapperBase.Map<IEnumerable<wsEscribanos_InstrumentoPrivado>, IEnumerable<wsEscribanosInstrumentoPrivadoDTO>>(elements);
            return entityDto;
        }
    }
}

