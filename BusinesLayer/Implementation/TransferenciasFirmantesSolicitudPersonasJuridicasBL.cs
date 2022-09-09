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
    public class TransferenciasFirmantesSolicitudPersonasJuridicasBL : ITransferenciasFirmantesSolicitudPersonasJuridicasBL<TransferenciasFirmantesSolicitudPersonasJuridicasDTO>
    {
        private TransferenciasFirmantesSolicitudPersonasJuridicasRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;

        IMapper mapperBase;

        public TransferenciasFirmantesSolicitudPersonasJuridicasBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TransferenciasFirmantesSolicitudPersonasJuridicasDTO, Transf_Firmantes_Solicitud_PersonasJuridicas>().ReverseMap()
                ;

                cfg.CreateMap<Transf_Firmantes_Solicitud_PersonasJuridicas, TransferenciasFirmantesSolicitudPersonasJuridicasDTO>().ReverseMap()
                ;


            });
            mapperBase = config.CreateMapper();
        }

        public IEnumerable<TransferenciasFirmantesSolicitudPersonasJuridicasDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasFirmantesSolicitudPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Transf_Firmantes_Solicitud_PersonasJuridicas>, IEnumerable<TransferenciasFirmantesSolicitudPersonasJuridicasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TransferenciasFirmantesSolicitudPersonasJuridicasDTO Single(int IdPersonaJuridica)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasFirmantesSolicitudPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdPersonaJuridica);
                var entityDto = mapperBase.Map<Transf_Firmantes_Solicitud_PersonasJuridicas, TransferenciasFirmantesSolicitudPersonasJuridicasDTO>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TransferenciasFirmantesSolicitudPersonasJuridicasDTO> GetByFKIdSolicitudIdPersonaJuridica(int IdSolicitud, int IdPersonaJuridica)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasFirmantesSolicitudPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByFKIdSolicitudIdPersonaJuridica(IdSolicitud, IdPersonaJuridica);
                var elementsDto = mapperBase.Map<IEnumerable<Transf_Firmantes_Solicitud_PersonasJuridicas>, IEnumerable<TransferenciasFirmantesSolicitudPersonasJuridicasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>	
        public IEnumerable<TransferenciasFirmantesSolicitudPersonasJuridicasDTO> GetByFKIdSolicitud(int IdSolicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasFirmantesSolicitudPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdSolicitud(IdSolicitud);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Firmantes_Solicitud_PersonasJuridicas>, IEnumerable<TransferenciasFirmantesSolicitudPersonasJuridicasDTO>>(elements);
            return elementsDto;
        }

        public bool Insert(TransferenciasFirmantesSolicitudPersonasJuridicasDTO objectDto)
        {
            try
            {                

                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new TransferenciasFirmantesSolicitudPersonasJuridicasRepository(unitOfWork);

                    var elementDto = mapperBase.Map<TransferenciasFirmantesSolicitudPersonasJuridicasDTO, Transf_Firmantes_Solicitud_PersonasJuridicas>(objectDto);

                    repo.Insert(elementDto);                

                    unitOfWork.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(TransferenciasFirmantesSolicitudPersonasJuridicasDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new TransferenciasFirmantesSolicitudPersonasJuridicasRepository(unitOfWork);
                    var elementDto = mapperBase.Map<TransferenciasFirmantesSolicitudPersonasJuridicasDTO, Transf_Firmantes_Solicitud_PersonasJuridicas>(objectDto);
                    var insertOk = repo.Delete(elementDto);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
