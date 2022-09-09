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
	public class SSITSolicitudesUbicacionesPropiedadHorizontalBL : ISSITSolicitudesUbicacionesPropiedadHorizontalBL<SSITSolicitudesUbicacionesPropiedadHorizontalDTO>
    {               
		private SSITSolicitudesUbicacionesPropiedadHorizontalRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public SSITSolicitudesUbicacionesPropiedadHorizontalBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SSITSolicitudesUbicacionesPropiedadHorizontalDTO, SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal>().ReverseMap()
                    .ForMember(dest => dest.IdSolicitudPropiedadHorizontal, source => source.MapFrom(p => p.id_solicitudprophorizontal))
                    .ForMember(dest => dest.IdSolicitudUbicacion, source => source.MapFrom(p => p.id_solicitudubicacion))
                    .ForMember(dest => dest.IdPropiedadHorizontal, source => source.MapFrom(p => p.id_propiedadhorizontal));

                cfg.CreateMap<SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal, SSITSolicitudesUbicacionesPropiedadHorizontalDTO>().ReverseMap()
                    .ForMember(dest => dest.id_solicitudprophorizontal, source => source.MapFrom(p => p.IdSolicitudPropiedadHorizontal))
                    .ForMember(dest => dest.id_solicitudubicacion, source => source.MapFrom(p => p.IdSolicitudUbicacion))
                    .ForMember(dest => dest.id_propiedadhorizontal, source => source.MapFrom(p => p.IdPropiedadHorizontal));
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<SSITSolicitudesUbicacionesPropiedadHorizontalDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesUbicacionesPropiedadHorizontalRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal>, IEnumerable<SSITSolicitudesUbicacionesPropiedadHorizontalDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public SSITSolicitudesUbicacionesPropiedadHorizontalDTO Single(int IdSolicitudPropiedadHorizontal )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesUbicacionesPropiedadHorizontalRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdSolicitudPropiedadHorizontal);
                var entityDto = mapperBase.Map<SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal, SSITSolicitudesUbicacionesPropiedadHorizontalDTO>(entity);
     
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
		public IEnumerable<SSITSolicitudesUbicacionesPropiedadHorizontalDTO> GetByFKIdSolicitudUbicacion(int IdSolicitudUbicacion)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesUbicacionesPropiedadHorizontalRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdSolicitudUbicacion(IdSolicitudUbicacion);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal>, IEnumerable<SSITSolicitudesUbicacionesPropiedadHorizontalDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdPropiedadHorizontal"></param>
		/// <returns></returns>	
		public IEnumerable<SSITSolicitudesUbicacionesPropiedadHorizontalDTO> GetByFKIdPropiedadHorizontal(int IdPropiedadHorizontal)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesUbicacionesPropiedadHorizontalRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdPropiedadHorizontal(IdPropiedadHorizontal);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal>, IEnumerable<SSITSolicitudesUbicacionesPropiedadHorizontalDTO>>(elements);
            return elementsDto;				
		}
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(SSITSolicitudesUbicacionesPropiedadHorizontalDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SSITSolicitudesUbicacionesPropiedadHorizontalRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<SSITSolicitudesUbicacionesPropiedadHorizontalDTO, SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal>(objectDto);                   
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
		public void Update(SSITSolicitudesUbicacionesPropiedadHorizontalDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SSITSolicitudesUbicacionesPropiedadHorizontalRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<SSITSolicitudesUbicacionesPropiedadHorizontalDTO, SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal>(objectDTO);                   
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
		public void Delete(SSITSolicitudesUbicacionesPropiedadHorizontalDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SSITSolicitudesUbicacionesPropiedadHorizontalRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<SSITSolicitudesUbicacionesPropiedadHorizontalDTO, SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal>(objectDto);                   
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

