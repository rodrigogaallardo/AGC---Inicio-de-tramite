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
	public class WsEscribanosPersonasJuridicasBL : IWsEscribanosPersonasJuridicasBL<wsEscribanosPersonasJuridicasDTO>
    {               
		private wsEscribanosPersonasJuridicasRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
        IMapper mapperPJ;
         
        public WsEscribanosPersonasJuridicasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<wsEscribanosPersonasJuridicasDTO, wsEscribanos_PersonasJuridicas>().ReverseMap();
            });
            mapperBase = config.CreateMapper();

            var configPJ = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<wsEscribanosPersonasJuridicasDTO, wsEscribanosPersonasJuridicasEntity>().ReverseMap();
            });
            mapperPJ = configPJ.CreateMapper();
        }
		
        public wsEscribanosPersonasJuridicasDTO Single(int id_wsPersonaJuridica)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new wsEscribanosPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(id_wsPersonaJuridica);
                var entityDto = mapperBase.Map<wsEscribanos_PersonasJuridicas, wsEscribanosPersonasJuridicasDTO>(entity);

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
        /// <param name="id_actanotarial"></param>
        /// <returns></returns>
        public IEnumerable<wsEscribanosPersonasJuridicasDTO> GetByFKIdActanotarial(int id_actanotarial)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new wsEscribanosPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdActanotarial(id_actanotarial);
            var elementsDto = mapperPJ.Map<IEnumerable<wsEscribanosPersonasJuridicasEntity>,IEnumerable<wsEscribanosPersonasJuridicasDTO>>(elements);
            return elementsDto;
        }
    }
}

