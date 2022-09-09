using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBusinessLayer;
using DataTransferObject;
using AutoMapper;
using UnitOfWork;
using DataAcess;
using BaseRepository;
using Dal.UnitOfWork;

namespace BusinesLayer.Implementation
{
    public class SSITSolicitudesDatosLocalBL: ISSTISolicitudesDatosLocalBL<SSITSolicitudesDatosLocalDTO>
    {
        private SSITSolicitudesDatosLocalRepository repo = null;
        private IUnitOfWorkFactory uowF = null;

        IMapper mapperBase;

        public SSITSolicitudesDatosLocalBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SSITSolicitudesDatosLocalDTO, SSIT_Solicitudes_DatosLocal>()
                .ForMember(dest => dest.SSIT_Solicitudes, source => source.Ignore())
                .ForMember(dest => dest.aspnet_Users, source => source.Ignore())
                .ForMember(dest => dest.aspnet_Users1, source => source.Ignore())
                ;

                cfg.CreateMap<SSIT_Solicitudes_DatosLocal, SSITSolicitudesDatosLocalDTO>();

            });
            mapperBase = config.CreateMapper();

        }

        public IEnumerable<SSITSolicitudesDatosLocalDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesDatosLocalRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_DatosLocal>, IEnumerable<SSITSolicitudesDatosLocalDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SSITSolicitudesDatosLocalDTO Single(int IdSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesDatosLocalRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdSolicitud);

                SSITSolicitudesDatosLocalDTO entityDto = null;
                if(entity != null)
                    entityDto = mapperBase.Map<SSIT_Solicitudes_DatosLocal, SSITSolicitudesDatosLocalDTO>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region Métodos de actualizacion insert
        public bool Insert(SSITSolicitudesDatosLocalDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesDatosLocalRepository(unitOfWork);
                    var elementDto = mapperBase.Map<SSITSolicitudesDatosLocalDTO, SSIT_Solicitudes_DatosLocal>(objectDto);
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
        #region Métodos de actualizacion Update
        public void Update(SSITSolicitudesDatosLocalDTO objectDTO)
        {
            try
            {
                bool IsUpdate = false;
                IUnitOfWork unitOfWorkRead = this.uowF.GetUnitOfWork();
                var repoRead = new SSITSolicitudesDatosLocalRepository(unitOfWorkRead);

                if (repoRead.GetByFKIdSolicitud(objectDTO.IdSolicitud).Count() > 0)
                    IsUpdate = true;


                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesDatosLocalRepository(unitOfWork);

                    var elementDTO = mapperBase.Map<SSITSolicitudesDatosLocalDTO, SSIT_Solicitudes_DatosLocal>(objectDTO);
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
        #region Métodos de actualizacion Delete
        public void Delete(SSITSolicitudesDatosLocalDTO objectDTO)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesDatosLocalRepository(unitOfWork);
                    var elementDto = mapperBase.Map<SSITSolicitudesDatosLocalDTO, SSIT_Solicitudes_DatosLocal>(objectDTO);
                    repo.Delete(elementDto);

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
