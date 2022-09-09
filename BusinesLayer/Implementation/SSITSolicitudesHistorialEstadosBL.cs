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
	public class SSITSolicitudesHistorialEstadosBL : ISSITSolicitudesHistorialEstadosBL<SSITSolicitudesHistorialEstadosDTO>
    {               
		private SSITSolicitudesHistorialEstadosRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public SSITSolicitudesHistorialEstadosBL()
        {
            var configHistorial = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SSITSolicitudesHistorialEstadosGrillaDTO, SSITSolicitudesHistorialEstadosGrillaEntity>().ReverseMap();

                cfg.CreateMap<SSITSolicitudesHistorialEstadosDTO, SSIT_Solicitudes_HistorialEstados>().ReverseMap(); 

            });

            mapperBase = configHistorial.CreateMapper();
        }		
        /// <summary>
        /// 
        /// </summary>
        /// <param name="solicitud"></param>
        /// <returns></returns>
        
        public List<SSITSolicitudesHistorialEstadosGrillaDTO> GetByFKIdSolicitudGrilla(SSITSolicitudesDTO solicitud)
        {
            try
            {
                List<SSITSolicitudesHistorialEstadosGrillaDTO> lstHistDTO = new List<SSITSolicitudesHistorialEstadosGrillaDTO>();
                SSITSolicitudesHistorialEstadosGrillaDTO HistDTO = new SSITSolicitudesHistorialEstadosGrillaDTO();

                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesHistorialEstadosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByFKIdSolicitudGrilla(solicitud.IdSolicitud);
                var elementsDto = mapperBase.Map<IEnumerable<SSITSolicitudesHistorialEstadosGrillaEntity>, IEnumerable<SSITSolicitudesHistorialEstadosGrillaDTO>>(elements);
                HistDTO.Estado = StaticClass.Constantes.ESTADO_INCOMPLETO_DESCRIPCION;
                HistDTO.fecha = solicitud.CreateDate;
                lstHistDTO.Add(HistDTO);
                lstHistDTO.AddRange(elementsDto.ToList());
                
                return lstHistDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="historialesDTO"></param>
        public void Delete(IEnumerable<SSITSolicitudesHistorialEstadosDTO> historialesDTO)
        {
            uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
            using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))            
            {
                repo = new SSITSolicitudesHistorialEstadosRepository(unitOfWork);    
            
                var elementsEntity = mapperBase.Map<IEnumerable<SSITSolicitudesHistorialEstadosDTO>,IEnumerable<SSIT_Solicitudes_HistorialEstados>>(historialesDTO);

                repo.RemoveRange(elementsEntity);

                unitOfWork.Commit();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public IEnumerable<SSITSolicitudesHistorialEstadosDTO> GetByFKIdSolicitud(int IdSolicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesHistorialEstadosRepository(this.uowF.GetUnitOfWork());
            var elementsEntity = repo.GetByFKIdSolicitud(IdSolicitud);
            var historialesDTO = mapperBase.Map<IEnumerable<SSIT_Solicitudes_HistorialEstados>, IEnumerable<SSITSolicitudesHistorialEstadosDTO>>(elementsEntity);

            return historialesDTO;
        }
    }
}

