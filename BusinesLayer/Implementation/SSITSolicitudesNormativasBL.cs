using AutoMapper;
using BaseRepository;
using IBusinessLayer;
using Dal.UnitOfWork;
using DataAcess;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;
using StaticClass;


namespace BusinesLayer.Implementation
{
    public class SSITSolicitudesNormativasBL : ISSITSolicitudesNormativasBL<SSITSolicitudesNormativasDTO>
    {

        private SSITSolicitudesNormativasRepository repo = null;
        private SSITSolicitudesRepository repSolicitud = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public SSITSolicitudesNormativasBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SSITSolicitudesNormativasDTO, SSIT_Solicitudes_Normativas>().ReverseMap()
                    .ForMember(dest => dest.TipoNormativaDTO, source => source.MapFrom(p => p.TipoNormativa))
                    .ForMember(dest => dest.EntidadNormativaDTO, source => source.MapFrom(p => p.EntidadNormativa));

                cfg.CreateMap<SSIT_Solicitudes_Normativas, SSITSolicitudesNormativasDTO>().ReverseMap()
                    .ForMember(dest => dest.TipoNormativa, source => source.MapFrom(p => p.TipoNormativaDTO))
                    .ForMember(dest => dest.EntidadNormativa, source => source.MapFrom(p => p.EntidadNormativaDTO));


                cfg.CreateMap<EntidadNormativa, EntidadNormativaDTO>();
                cfg.CreateMap<EntidadNormativaDTO, EntidadNormativa>();

                cfg.CreateMap<TipoNormativa, TipoNormativaDTO>();
                cfg.CreateMap<TipoNormativaDTO, TipoNormativa>();
            });
            mapperBase = config.CreateMapper();

        }


        public SSITSolicitudesNormativasDTO Single(int IdSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesNormativasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdSolicitud);
                var entityDto = mapperBase.Map<SSIT_Solicitudes_Normativas, SSITSolicitudesNormativasDTO>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<SSITSolicitudesNormativasDTO> GetNormativas(int IdSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesNormativasRepository(this.uowF.GetUnitOfWork());

                var elements = repo.GetNormativas(IdSolicitud);
                var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Normativas>, IEnumerable<SSITSolicitudesNormativasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<SSITSolicitudesNormativasDTO> GetByFKIdSolicitud(int IdSolicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesNormativasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdSolicitud(IdSolicitud);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Normativas>, IEnumerable<SSITSolicitudesNormativasDTO>>(elements);
            return elementsDto;
        }

        #region Métodos de actualizacion e insert
        /// <summary>
        /// Inserta la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public bool Insert(SSITSolicitudesNormativasDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesNormativasRepository(unitOfWork);
                    var elementDto = mapperBase.Map<SSITSolicitudesNormativasDTO, SSIT_Solicitudes_Normativas>(objectDto);
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
        #region Métodos de actualizacion e insert
        /// <summary>
        /// Modifica la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public void Update(SSITSolicitudesNormativasDTO objectDTO)
        {
            try
            {
                bool IsUpdate = false;
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesNormativasRepository(uowF.GetUnitOfWork());
                repSolicitud = new SSITSolicitudesRepository(uowF.GetUnitOfWork());

                var solicitud = repSolicitud.Single(objectDTO.IdSolicitud);
                if (solicitud.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && solicitud.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
                    throw new Exception(Errors.ENCOMIENDA_CAMBIOS);

                if (repo.GetByFKIdSolicitud(objectDTO.IdSolicitud).Any())
                    IsUpdate = true;

                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesNormativasRepository(unitOfWork);

                    var elementDTO = mapperBase.Map<SSITSolicitudesNormativasDTO, SSIT_Solicitudes_Normativas>(objectDTO);
                    if (IsUpdate)
                    { repo.Update(elementDTO); }
                    else
                    {
                        elementDTO.CreateDate = DateTime.Now;
                        repo.Insert(elementDTO);
                    }

                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
        #region Métodos de actualizacion e insert
        /// <summary>
        /// elimina la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>      
        public void Delete(SSITSolicitudesNormativasDTO objectDTO)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesNormativasRepository(unitOfWork);
                    repSolicitud = new SSITSolicitudesRepository(unitOfWork);
                    var encomienda = repSolicitud.Single(objectDTO.IdSolicitud);
                    if (encomienda.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && encomienda.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
                        throw new Exception(Errors.ENCOMIENDA_CAMBIOS);

                    var elementDTO = mapperBase.Map<SSITSolicitudesNormativasDTO, SSIT_Solicitudes_Normativas>(objectDTO);
                    repo.Delete(elementDTO);

                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }
}
