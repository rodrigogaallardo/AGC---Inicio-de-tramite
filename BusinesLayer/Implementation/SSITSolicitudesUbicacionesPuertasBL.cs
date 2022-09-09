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
	public class SSITSolicitudesUbicacionesPuertasBL : ISSITSolicitudesUbicacionesPuertasBL<SSITSolicitudesUbicacionesPuertasDTO>
    {               
		private SSITSolicitudesUbicacionesPuertasRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public SSITSolicitudesUbicacionesPuertasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SSITSolicitudesUbicacionesPuertasDTO, SSIT_Solicitudes_Ubicaciones_Puertas>().ReverseMap()
                    .ForMember(dest => dest.IdSolicitudPuerta, source => source.MapFrom(p => p.id_solicitudpuerta))
                    .ForMember(dest => dest.IdSolicitudUbicacion, source => source.MapFrom(p => p.id_solicitudubicacion))
                    .ForMember(dest => dest.CodigoCalle, source => source.MapFrom(p => p.codigo_calle))
                    .ForMember(dest => dest.NombreCalle, source => source.MapFrom(p => p.nombre_calle));

                cfg.CreateMap<SSIT_Solicitudes_Ubicaciones_Puertas, SSITSolicitudesUbicacionesPuertasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_solicitudpuerta, source => source.MapFrom(p => p.IdSolicitudPuerta))
                    .ForMember(dest => dest.id_solicitudubicacion, source => source.MapFrom(p => p.IdSolicitudUbicacion))
                    .ForMember(dest => dest.codigo_calle, source => source.MapFrom(p => p.CodigoCalle))
                    .ForMember(dest => dest.nombre_calle, source => source.MapFrom(p => p.NombreCalle)); 
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<SSITSolicitudesUbicacionesPuertasDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesUbicacionesPuertasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Ubicaciones_Puertas>, IEnumerable<SSITSolicitudesUbicacionesPuertasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public SSITSolicitudesUbicacionesPuertasDTO Single(int IdSolicitudPuerta )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesUbicacionesPuertasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdSolicitudPuerta);
                var entityDto = mapperBase.Map<SSIT_Solicitudes_Ubicaciones_Puertas, SSITSolicitudesUbicacionesPuertasDTO>(entity);
     
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
		/// <param name="IdSSITSolicitudesUbicacion"></param>
		/// <returns></returns>	
		public IEnumerable<SSITSolicitudesUbicacionesPuertasDTO> GetByFKIdSolicitudUbicacion(int IdSolicitudUbicacion)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesUbicacionesPuertasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdSolicitudUbicacion(IdSolicitudUbicacion);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Ubicaciones_Puertas>, IEnumerable<SSITSolicitudesUbicacionesPuertasDTO>>(elements);
            return elementsDto;				
		}
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(SSITSolicitudesUbicacionesPuertasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SSITSolicitudesUbicacionesPuertasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<SSITSolicitudesUbicacionesPuertasDTO, SSIT_Solicitudes_Ubicaciones_Puertas>(objectDto);                   
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
		public void Update(SSITSolicitudesUbicacionesPuertasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SSITSolicitudesUbicacionesPuertasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<SSITSolicitudesUbicacionesPuertasDTO, SSIT_Solicitudes_Ubicaciones_Puertas>(objectDTO);                   
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
		#region Métodos de actualizacion e insert
		/// <summary>
		/// elimina la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>      
		public void Delete(SSITSolicitudesUbicacionesPuertasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SSITSolicitudesUbicacionesPuertasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<SSITSolicitudesUbicacionesPuertasDTO, SSIT_Solicitudes_Ubicaciones_Puertas>(objectDto);                   
		            var insertOk = repo.Delete(elementDto);
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

