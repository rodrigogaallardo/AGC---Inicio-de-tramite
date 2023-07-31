using AutoMapper;
using BaseRepository;
using Dal.UnitOfWork;
using DataAcess;
using DataTransferObject;
using IBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitOfWork;

namespace BusinesLayer.Implementation
{
    public class EncomiendaSSITSolicitudesBL : IEncomiendaSSITSolicitudesBL<EncomiendaSSITSolicitudesDTO>
    {
        private EncomiendaSSITSolicitudesRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public EncomiendaSSITSolicitudesBL()
        {
            var config = new MapperConfiguration(cfg =>
            {

                #region "Encomienda_SSIT_Solicitudes"
                cfg.CreateMap<Encomienda_SSIT_Solicitudes, EncomiendaSSITSolicitudesDTO>()
                    .ForMember(dest => dest.SSITSolicitudesDTO, source => source.MapFrom(p => p.SSIT_Solicitudes));

                cfg.CreateMap<EncomiendaSSITSolicitudesDTO, Encomienda_SSIT_Solicitudes>()
                    .ForMember(dest => dest.SSIT_Solicitudes, source => source.MapFrom(p => p.SSITSolicitudesDTO));
                #endregion

                #region "Solicitudes"
                cfg.CreateMap<SSITSolicitudesDTO, SSIT_Solicitudes>();

                cfg.CreateMap<SSIT_Solicitudes, SSITSolicitudesDTO>();
                #endregion
            });
            mapperBase = config.CreateMapper();


        }


        public EncomiendaSSITSolicitudesDTO Single(int id_encomiendaSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaSSITSolicitudesRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(id_encomiendaSolicitud);
                var entityDto = mapperBase.Map<Encomienda_SSIT_Solicitudes, EncomiendaSSITSolicitudesDTO>(entity);

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
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>	
        public IEnumerable<EncomiendaSSITSolicitudesDTO> GetByFKIdEncomienda(int IdEncomienda)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaSSITSolicitudesRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdEncomienda(IdEncomienda);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_SSIT_Solicitudes>, IEnumerable<EncomiendaSSITSolicitudesDTO>>(elements);
            return elementsDto;
        }

        public IEnumerable<EncomiendaSSITSolicitudesDTO> GetByFKIdSolicitud(int IdSolicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaSSITSolicitudesRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdSolicitud(IdSolicitud);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_SSIT_Solicitudes>, IEnumerable<EncomiendaSSITSolicitudesDTO>>(elements);
            return elementsDto;
        }

        #region Métodos de inserccion
        /// <summary>
        /// Inserta la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public bool Insert(EncomiendaSSITSolicitudesDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaSSITSolicitudesRepository(unitOfWork);
                    var elementDto = mapperBase.Map<EncomiendaSSITSolicitudesDTO, Encomienda_SSIT_Solicitudes>(objectDto);
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


        #endregion
        #region Métodos de actualizacion
        /// <summary>
        /// Modifica la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public void Update(EncomiendaSSITSolicitudesDTO objectDTO)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaSSITSolicitudesRepository(unitOfWork);
                    var elementDTO = mapperBase.Map<EncomiendaSSITSolicitudesDTO, Encomienda_SSIT_Solicitudes>(objectDTO);
                    repo.Update(elementDTO);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
        #region Métodos de eliminacion
        /// <summary>
        /// elimina la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>      
        public void Delete(EncomiendaSSITSolicitudesDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaSSITSolicitudesRepository(unitOfWork);
                    var elementDto = mapperBase.Map<EncomiendaSSITSolicitudesDTO, Encomienda_SSIT_Solicitudes>(objectDto);
                    var insertOk = repo.Delete(elementDto);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool existe(int id_solicitud, int id_encomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaSSITSolicitudesRepository(this.uowF.GetUnitOfWork());
                return repo.existe(id_solicitud, id_encomienda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}

