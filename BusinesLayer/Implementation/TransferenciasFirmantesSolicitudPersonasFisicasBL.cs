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
    
    public class TransferenciasFirmantesSolicitudPersonasFisicasBL : ITransferenciasFirmantesSolicitudPersonasFisicasBL<TransferenciasFirmantesSolicitudPersonasFisicasDTO>
    {
        private TransferenciasFirmantesSolicitudPersonasFisicasRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public TransferenciasFirmantesSolicitudPersonasFisicasBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TransferenciasFirmantesSolicitudPersonasFisicasDTO, Transf_Firmantes_Solicitud_PersonasFisicas>().ReverseMap()
                ;

                cfg.CreateMap<Transf_Firmantes_Solicitud_PersonasFisicas, TransferenciasFirmantesSolicitudPersonasFisicasDTO>().ReverseMap()
               ;

               
            });
            mapperBase = config.CreateMapper();
        }

        public IEnumerable<TransferenciasFirmantesSolicitudPersonasFisicasDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasFirmantesSolicitudPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Transf_Firmantes_Solicitud_PersonasFisicas>, IEnumerable<TransferenciasFirmantesSolicitudPersonasFisicasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<TransferenciasFirmantesSolicitudPersonasFisicasDTO> GetByFKIdPersonaFisica(int IdPersonaFisica)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasFirmantesSolicitudPersonasFisicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdPersonaFisica(IdPersonaFisica);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Firmantes_Solicitud_PersonasFisicas>, IEnumerable<TransferenciasFirmantesSolicitudPersonasFisicasDTO>>(elements);
            return elementsDto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdPersonaFisica"></param>
        /// <returns></returns>
        public TransferenciasFirmantesSolicitudPersonasFisicasDTO Single(int IdPersonaFisica)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasFirmantesSolicitudPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdPersonaFisica);
                var entityDto = mapperBase.Map<Transf_Firmantes_Solicitud_PersonasFisicas, TransferenciasFirmantesSolicitudPersonasFisicasDTO>(entity);

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
        /// <param name="IdConsultaPadron"></param>
        /// <returns></returns>	
        public IEnumerable<TransferenciasFirmantesSolicitudPersonasFisicasDTO> GetByFKIdSolicitud(int IdSolicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasFirmantesSolicitudPersonasFisicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdSolicitud(IdSolicitud);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Firmantes_Solicitud_PersonasFisicas>, IEnumerable<TransferenciasFirmantesSolicitudPersonasFisicasDTO>>(elements);
            return elementsDto;
        }

        public bool Insert(TransferenciasFirmantesSolicitudPersonasFisicasDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new TransferenciasFirmantesSolicitudPersonasFisicasRepository(unitOfWork);

                    if (objectDto.id_firmante_pf > 0)
                    {
                        var deleteObject = new TransferenciasFirmantesSolicitudPersonasFisicasDTO()
                        {
                            id_firmante_pf = objectDto.id_firmante_pf
                        };
                        var deleteObjectDTO = mapperBase.Map<TransferenciasFirmantesSolicitudPersonasFisicasDTO, Transf_Firmantes_Solicitud_PersonasFisicas>(deleteObject);
                        repo.Delete(deleteObjectDTO);
                    }

                    var elementDto = mapperBase.Map<TransferenciasFirmantesSolicitudPersonasFisicasDTO, Transf_Firmantes_Solicitud_PersonasFisicas>(objectDto);
                    var insertOk = repo.Insert(elementDto);
                    unitOfWork.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Delete(TransferenciasFirmantesSolicitudPersonasFisicasDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new TransferenciasFirmantesSolicitudPersonasFisicasRepository(unitOfWork);
                    var elementDto = mapperBase.Map<TransferenciasFirmantesSolicitudPersonasFisicasDTO, Transf_Firmantes_Solicitud_PersonasFisicas>(objectDto);
                    var insertOk = repo.Delete(elementDto);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<TransferenciasFirmantesSolicitudPersonasFisicasDTO> GetByFKIdSolicitudIdPersonaFisica(int IdSolicitud, int IdPersonaFisica)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasFirmantesSolicitudPersonasFisicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdSolicitudIdPersonaFisica(IdSolicitud, IdPersonaFisica);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Firmantes_Solicitud_PersonasFisicas>, IEnumerable<TransferenciasFirmantesSolicitudPersonasFisicasDTO>>(elements);
            return elementsDto;
        }
    }
}
