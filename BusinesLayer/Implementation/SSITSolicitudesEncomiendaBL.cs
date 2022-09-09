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

namespace BusinesLayer.Implementation
{
	public class SSITSolicitudesEncomiendaBL : ISSITSolicitudesEncomiendaBL<SSITSolicitudesEncomiendaDTO>
    {               
		private SSITSolicitudesEncomiendaRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public SSITSolicitudesEncomiendaBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SSITSolicitudesEncomiendaDTO, SSIT_Solicitudes_Encomienda>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }
		
        #region Métodos de actualizacion e insert
        /// <summary>
        /// Inserta la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public bool Insert(SSITSolicitudesEncomiendaDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SSITSolicitudesEncomiendaRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<SSITSolicitudesEncomiendaDTO, SSIT_Solicitudes_Encomienda>(objectDto);
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

        public IEnumerable<SSITSolicitudesEncomiendaDTO> GetByFKIdSolicitud(int id_solicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesEncomiendaRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByFKIdSolicitud(id_solicitud);
                var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Encomienda>, IEnumerable<SSITSolicitudesEncomiendaDTO>>(elements);
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

