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
using DataAcess.EntityCustom;

namespace BusinesLayer.Implementation
{
	public class SSITSolicitudesObservacionesBL : ISSITSolicitudesObservacionesBL<SSITSolicitudesObservacionesDTO>
    {               
		private SSITSolicitudesObservacionesRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
        IMapper mapperObservaciones;         
        public SSITSolicitudesObservacionesBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SSITSolicitudesObservacionesDTO, SSIT_Solicitudes_Observaciones>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
            
            var configObservaciones = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SSITSolicitudesObservacionesDTO, SSITSolicitudesObservacionesEntity>().ReverseMap();
            });
            mapperObservaciones = configObservaciones.CreateMapper();
        }
		

        public IEnumerable<SSITSolicitudesObservacionesDTO> GetByFKIdSolicitud(int id_solicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesObservacionesRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByFKIdSolicitud(id_solicitud);
                var elementDto = mapperObservaciones.Map<IEnumerable<SSITSolicitudesObservacionesEntity>, IEnumerable<SSITSolicitudesObservacionesDTO>>(elements);
                return elementDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(SSITSolicitudesObservacionesDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesObservacionesRepository(unitOfWork);
                    var elementDto = mapperBase.Map<SSITSolicitudesObservacionesDTO, SSIT_Solicitudes_Observaciones>(objectDto);
                    repo.Update(elementDto);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SSITSolicitudesObservacionesDTO Single(int id_solobs)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesObservacionesRepository(unitOfWork);
                    var entity = repo.Single(id_solobs);
                    var entityDto = mapperBase.Map<SSIT_Solicitudes_Observaciones, SSITSolicitudesObservacionesDTO>(entity);
                    return entityDto;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

