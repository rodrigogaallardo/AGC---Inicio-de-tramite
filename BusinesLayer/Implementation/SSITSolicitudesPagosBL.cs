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
using StaticClass;

namespace BusinesLayer.Implementation
{
	public class SSITSolicitudesPagosBL : ISSITSolicitudesPagosBL<SSITSolicitudesPagosDTO>
    {               
		private SSITSolicitudesPagosRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
        IMapper mapperPagos;
        
        public SSITSolicitudesPagosBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SSITSolicitudesPagosDTO, SSIT_Solicitudes_Pagos>().ReverseMap();
            });
            mapperBase = config.CreateMapper();

            var configPagos = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<clsItemGrillaPagos, clsItemGrillaPagosEntity>().ReverseMap();
            });

            mapperPagos = configPagos.CreateMapper();
        }
		
        #region Métodos de actualizacion e insert
        /// <summary>
        /// Inserta la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public bool Insert(SSITSolicitudesPagosDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SSITSolicitudesPagosRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<SSITSolicitudesPagosDTO, SSIT_Solicitudes_Pagos>(objectDto);
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

        public IEnumerable<SSITSolicitudesPagosDTO> GetByFKIdSolicitud(int id_solicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesPagosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByFKIdSolicitud(id_solicitud);
                var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Pagos>, IEnumerable<SSITSolicitudesPagosDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<clsItemGrillaPagos> GetGrillaByFKIdSolicitud(int id_solicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesPagosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetGrillaByFKIdSolicitud(id_solicitud);
                var elementsDto = mapperPagos.Map<IEnumerable<clsItemGrillaPagosEntity>, IEnumerable<clsItemGrillaPagos>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}

