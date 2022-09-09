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
	public class WsEscribanosPersonasFisicasBL : IWsEscribanosPersonasFisicasBL<wsEscribanosPersonasFisicasDTO>
    {               
		private wsEscribanosPersonasFisicasRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
        IMapper mapperPF;         
        public WsEscribanosPersonasFisicasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<wsEscribanosPersonasFisicasDTO, wsEscribanos_PersonasFisicas>().ReverseMap();
            });
            mapperBase = config.CreateMapper();

            var configPF = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<wsEscribanosPersonasFisicasDTO, wsEscribanosPersonasFisicasEntity>().ReverseMap();
            });
            mapperPF = configPF.CreateMapper();
        }

        public wsEscribanosPersonasFisicasDTO Single(int id_wsPersonaFisica)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new wsEscribanosPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(id_wsPersonaFisica);
                var entityDto = mapperBase.Map<wsEscribanos_PersonasFisicas, wsEscribanosPersonasFisicasDTO>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<wsEscribanosPersonasFisicasDTO> GetByFKIdActanotarial(int id_actanotarial)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new wsEscribanosPersonasFisicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdActanotarial(id_actanotarial);
            var entityDto = mapperPF.Map<IEnumerable<wsEscribanosPersonasFisicasEntity>, IEnumerable<wsEscribanosPersonasFisicasDTO>>(elements);
            return entityDto;            
        }
    }
}

