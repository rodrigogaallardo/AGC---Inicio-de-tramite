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
	public class WsEscribanosDerechoOcupacionBL : IWsEscribanosDerechoOcupacionBL<wsEscribanosDerechoOcupacionDTO>
    {               
		private wsEscribanosDerechoOcupacionRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public WsEscribanosDerechoOcupacionBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<wsEscribanosDerechoOcupacionDTO, wsEscribanos_DerechoOcupacion>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }
		
        public wsEscribanosDerechoOcupacionDTO Single(int id_wsPersonaJuridica)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new wsEscribanosDerechoOcupacionRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(id_wsPersonaJuridica);
                var entityDto = mapperBase.Map<wsEscribanos_DerechoOcupacion, wsEscribanosDerechoOcupacionDTO>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<wsEscribanosDerechoOcupacionDTO> GetByFKIdActanotarial(int id_actanotarial)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new wsEscribanosDerechoOcupacionRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdActanotarial(id_actanotarial);
            var entityDto = mapperBase.Map<IEnumerable<wsEscribanos_DerechoOcupacion>, IEnumerable<wsEscribanosDerechoOcupacionDTO>>(elements);
            return entityDto;
        }
    }
}

