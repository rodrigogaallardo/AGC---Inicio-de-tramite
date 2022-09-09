using AutoMapper;
using BaseRepository;
using IBusinessLayer;
using Dal.UnitOfWork;
using DataAcess;
using DataTransferObject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnitOfWork;

namespace BusinesLayer.Implementation
{
	public class UbicacionesZonasMixturasBL : IUbicacionesZonasMixturasBL<UbicacionesZonasMixturasDTO>
    {               
		private UbicacionesZonasMixturasRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public UbicacionesZonasMixturasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UbicacionesZonasMixturasDTO, Ubicaciones_ZonasMixtura>().ReverseMap()
                    .ForMember(dest => dest.IdZona, source => source.MapFrom(p => p.IdZonaMixtura));

                cfg.CreateMap<Ubicaciones_ZonasMixtura, UbicacionesZonasMixturasDTO>().ReverseMap()
                    .ForMember(dest => dest.IdZonaMixtura, source => source.MapFrom(p => p.IdZona));
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<UbicacionesZonasMixturasDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesZonasMixturasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Ubicaciones_ZonasMixtura>, IEnumerable<UbicacionesZonasMixturasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       

		public UbicacionesZonasMixturasDTO Single(int IdZona )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesZonasMixturasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdZona);
                var entityDto = mapperBase.Map<Ubicaciones_ZonasMixtura, UbicacionesZonasMixturasDTO>(entity);
     
                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(UbicacionesZonasMixturasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new UbicacionesZonasMixturasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<UbicacionesZonasMixturasDTO, Ubicaciones_ZonasMixtura>(objectDto);                   
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
		public void Update(UbicacionesZonasMixturasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new UbicacionesZonasMixturasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<UbicacionesZonasMixturasDTO, Ubicaciones_ZonasMixtura>(objectDTO);                   
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
		public void Delete(UbicacionesZonasMixturasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new UbicacionesZonasMixturasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<UbicacionesZonasMixturasDTO, Ubicaciones_ZonasMixtura>(objectDto);                   
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>
        public IEnumerable<UbicacionesZonasMixturasDTO> GetZonasEncomienda(int IdEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesZonasMixturasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetZonasEncomienda(IdEncomienda);
                var elementsDto = mapperBase.Map<IEnumerable<Ubicaciones_ZonasMixtura>, IEnumerable<UbicacionesZonasMixturasDTO>>(elements);

                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<UbicacionesZonasMixturasDTO> GetZonasUbicacion(int IdUbicacion)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesZonasMixturasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetZonasUbicacion(IdUbicacion);
                var elementsDto = mapperBase.Map<IEnumerable<Ubicaciones_ZonasMixtura>, IEnumerable<UbicacionesZonasMixturasDTO>>(elements);

                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<UbicacionesZonasMixturasDTO> GetZonasUbicacion(List<int> lstUbi)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesZonasMixturasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetZonasUbicacion(lstUbi);
                var elementsDto = mapperBase.Map<IEnumerable<Ubicaciones_ZonasMixtura>, IEnumerable<UbicacionesZonasMixturasDTO>>(elements);

                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

