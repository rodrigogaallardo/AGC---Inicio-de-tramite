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
	public class SGISolicitudesPagosBL : ISGISolicitudesPagosBL<SGISolicitudesPagosDTO>
    {               
		private SGISolicitudesPagosRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
        IMapper mapperEntity;
		         
        public SGISolicitudesPagosBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SGISolicitudesPagosDTO, SGI_Solicitudes_Pagos>().ReverseMap()
                    .ForMember(dest => dest.IdSolicitudPago, source => source.MapFrom(p => p.id_sol_pago))				
                    .ForMember(dest => dest.IdTramiteTarea, source => source.MapFrom(p => p.id_tramitetarea))				
                    .ForMember(dest => dest.IdMedioPago, source => source.MapFrom(p => p.id_medio_pago))				
                    .ForMember(dest => dest.IdPago, source => source.MapFrom(p => p.id_pago))				
                    .ForMember(dest => dest.MontoPago, source => source.MapFrom(p => p.monto_pago))				
                    .ForMember(dest => dest.CodigoBarras, source => source.MapFrom(p => p.codigo_barras))				
                    .ForMember(dest => dest.NumeroBoletaUnica, source => source.MapFrom(p => p.nro_boleta_unica))				
                    .ForMember(dest => dest.NumeroDependencia, source => source.MapFrom(p => p.nro_dependencia))				
                    .ForMember(dest => dest.CodigoVerificador, source => source.MapFrom(p => p.codigo_verificador))				
                    .ForMember(dest => dest.UrlPago , source => source.MapFrom(p => p.url_pago));

                cfg.CreateMap<SGI_Solicitudes_Pagos, SGISolicitudesPagosDTO>().ReverseMap()
                    .ForMember(dest => dest.id_sol_pago, source => source.MapFrom(p => p.IdSolicitudPago))				
                    .ForMember(dest => dest.id_tramitetarea, source => source.MapFrom(p => p.IdTramiteTarea))				
                    .ForMember(dest => dest.id_medio_pago, source => source.MapFrom(p => p.IdMedioPago))				
                    .ForMember(dest => dest.id_pago, source => source.MapFrom(p => p.IdPago))				
                    .ForMember(dest => dest.monto_pago, source => source.MapFrom(p => p.MontoPago))				
                    .ForMember(dest => dest.codigo_barras, source => source.MapFrom(p => p.CodigoBarras))				
                    .ForMember(dest => dest.nro_boleta_unica, source => source.MapFrom(p => p.NumeroBoletaUnica))				
                    .ForMember(dest => dest.nro_dependencia, source => source.MapFrom(p => p.NumeroDependencia))				
                    .ForMember(dest => dest.codigo_verificador, source => source.MapFrom(p => p.CodigoVerificador))				
                    .ForMember(dest => dest.url_pago, source => source.MapFrom(p => p.UrlPago));

            });
            mapperBase = config.CreateMapper();

            var config1 = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<clsItemGrillaPagos, PagosEntity>().ReverseMap();
            });

            mapperEntity = config1.CreateMapper();
        }
		
        public IEnumerable<SGISolicitudesPagosDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SGISolicitudesPagosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<SGI_Solicitudes_Pagos>, IEnumerable<SGISolicitudesPagosDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public SGISolicitudesPagosDTO Single(int IdSolicitudPago )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SGISolicitudesPagosRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdSolicitudPago);
                var entityDto = mapperBase.Map<SGI_Solicitudes_Pagos, SGISolicitudesPagosDTO>(entity);
     
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
		/// <param name="IdTramiteTarea"></param>
		/// <returns></returns>	
		public IEnumerable<SGISolicitudesPagosDTO> GetByFKIdTramiteTarea(int IdTramiteTarea)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SGISolicitudesPagosRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTramiteTarea(IdTramiteTarea);
            var elementsDto = mapperBase.Map<IEnumerable<SGI_Solicitudes_Pagos>, IEnumerable<SGISolicitudesPagosDTO>>(elements);
            return elementsDto;				
		}
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(SGISolicitudesPagosDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SGISolicitudesPagosRepository(unitOfWork);
                    int ultimoID = repo.GetMaxId();
                    objectDto.IdSolicitudPago = ultimoID;
		            var elementDto = mapperBase.Map<SGISolicitudesPagosDTO, SGI_Solicitudes_Pagos>(objectDto);                   
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
		public void Update(SGISolicitudesPagosDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SGISolicitudesPagosRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<SGISolicitudesPagosDTO, SGI_Solicitudes_Pagos>(objectDTO);                   
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
		public void Delete(SGISolicitudesPagosDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SGISolicitudesPagosRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<SGISolicitudesPagosDTO, SGI_Solicitudes_Pagos>(objectDto);                   
		            var insertOk = repo.Delete(elementDto);
		            unitOfWork.Commit();
		        }
		    }
		    catch (Exception ex)
		    {
		        throw ex;
		    }
		}
		public void DeleteByFKIdTramiteTarea(int IdTramiteTarea)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new SGISolicitudesPagosRepository(unitOfWork);                    
					var elements = repo.GetByFKIdTramiteTarea(IdTramiteTarea);
					foreach(var element in elements)				
						repo.Delete(element);
		
					unitOfWork.Commit();		
				}
		    }		
			catch (Exception ex)
			{
				//throw ex;
			}
		}
		
		

		#endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTramiteTarea"></param>
        /// <returns></returns>
        public IEnumerable<clsItemGrillaPagos> GetTransf(int IdSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SGISolicitudesPagosRepository(this.uowF.GetUnitOfWork());
                var entity = repo.GetTransf(IdSolicitud);
                var entityDto = mapperEntity.Map<IEnumerable<PagosEntity>, IEnumerable<clsItemGrillaPagos>>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<clsItemGrillaPagos> GetTransmisiones(int IdSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SGISolicitudesPagosRepository(this.uowF.GetUnitOfWork());
                var entity = repo.GetTransmisiones(IdSolicitud);
                var entityDto = mapperEntity.Map<IEnumerable<PagosEntity>, IEnumerable<clsItemGrillaPagos>>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<clsItemGrillaPagos> GetHab(int IdSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SGISolicitudesPagosRepository(this.uowF.GetUnitOfWork());
                var entity = repo.GetHab(IdSolicitud);
                var entityDto = mapperEntity.Map<IEnumerable<PagosEntity>, IEnumerable<clsItemGrillaPagos>>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public clsItemGrillaPagos GetEstadoPago(int IdPago)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SGISolicitudesPagosRepository(this.uowF.GetUnitOfWork());
                var entity = repo.GetEstadoPago(IdPago);
                var entityDto = mapperEntity.Map<PagosEntity, clsItemGrillaPagos>(entity);
                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

