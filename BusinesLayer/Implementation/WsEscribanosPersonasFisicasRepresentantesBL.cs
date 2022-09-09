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
	public class WsEscribanosPersonasFisicasRepresentantesBL : IWsEscribanosPersonasFisicasRepresentantesBL<wsEscribanosPersonasFisicasRepresentantesDTO>
    {               
		private wsEscribanosPersonasFisicasRepresentantesRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
        IMapper mapperPF;        
        public WsEscribanosPersonasFisicasRepresentantesBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<wsEscribanosPersonasFisicasRepresentantesDTO, wsEscribanos_PersonasFisicas_Representantes>().ReverseMap();
            });
            mapperBase = config.CreateMapper();

            var configPF = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<wsEscribanosPersonasFisicasRepresentantesDTO, wsEscribanosPersonasFisicasRepresentantesEntity>().ReverseMap();
            });
            mapperPF = configPF.CreateMapper();
        }

        public wsEscribanosPersonasFisicasRepresentantesDTO Single(int id_wsRepresentantePF)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new wsEscribanosPersonasFisicasRepresentantesRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(id_wsRepresentantePF);
                var entityDto = mapperBase.Map<wsEscribanos_PersonasFisicas_Representantes, wsEscribanosPersonasFisicasRepresentantesDTO>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<wsEscribanosPersonasFisicasRepresentantesDTO> GetByFKIdWsPersonasFisica(int id_wsPersonaFisica)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new wsEscribanosPersonasFisicasRepresentantesRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdWsPersonasFisica(id_wsPersonaFisica);
            var entityDto = mapperPF.Map<IEnumerable<wsEscribanosPersonasFisicasRepresentantesEntity>, IEnumerable<wsEscribanosPersonasFisicasRepresentantesDTO>>(elements);

            return entityDto;
 
        }
    }
}

