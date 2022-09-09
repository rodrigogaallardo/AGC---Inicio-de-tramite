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
	public class WsEscribanosPersonasJuridicasRepresentantesBL : IWsEscribanosPersonasJuridicasRepresentantesBL<wsEscribanosPersonasJuridicasRepresentantesDTO>
    {               
		private wsEscribanosPersonasJuridicasRepresentantesRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
        IMapper mapperPJ;
        
        public WsEscribanosPersonasJuridicasRepresentantesBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<wsEscribanosPersonasJuridicasRepresentantesDTO, wsEscribanos_PersonasJuridicas_Representantes>().ReverseMap();
            });
            mapperBase = config.CreateMapper();

            var configPJ = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<wsEscribanosPersonasJuridicasRepresentantesDTO, wsEscribanosPersonasJuridicasRepresentantesEntity>().ReverseMap();
            });
            mapperPJ = configPJ.CreateMapper();        
        }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id_wsRepresentantePJ"></param>
		/// <returns></returns>
        public wsEscribanosPersonasJuridicasRepresentantesDTO Single(int id_wsRepresentantePJ)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new wsEscribanosPersonasJuridicasRepresentantesRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(id_wsRepresentantePJ);
                var entityDto = mapperBase.Map<wsEscribanos_PersonasJuridicas_Representantes, wsEscribanosPersonasJuridicasRepresentantesDTO>(entity);

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
        /// <param name="id_wsPersonaJuridica"></param>
        /// <returns></returns>
        public IEnumerable<wsEscribanosPersonasJuridicasRepresentantesDTO> GetByFKIdWsPersonasJuridica(int id_wsPersonaJuridica)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new wsEscribanosPersonasJuridicasRepresentantesRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByFKIdWsPersonasJuridica(id_wsPersonaJuridica);
                var elementsDto = mapperPJ.Map<IEnumerable<wsEscribanosPersonasJuridicasRepresentantesEntity>, IEnumerable<wsEscribanosPersonasJuridicasRepresentantesDTO>>(elements);

                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

