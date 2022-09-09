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
	public class TiposDeUbicacionBL : ITiposDeUbicacionBL<TiposDeUbicacionDTO>
    {               
		private TiposDeUbicacionRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public TiposDeUbicacionBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {				
                cfg.CreateMap<TiposDeUbicacionDTO, TiposDeUbicacion>().ReverseMap()
                    .ForMember(dest => dest.IdTipoUbicacion, source => source.MapFrom(p => p.id_tipoubicacion))
                    .ForMember(dest => dest.DescripcionTipoUbicacion, source => source.MapFrom(p => p.descripcion_tipoubicacion));

                cfg.CreateMap<TiposDeUbicacion, TiposDeUbicacionDTO>().ReverseMap()
                    .ForMember(dest => dest.id_tipoubicacion, source => source.MapFrom(p => p.IdTipoUbicacion))
                    .ForMember(dest => dest.descripcion_tipoubicacion, source => source.MapFrom(p => p.DescripcionTipoUbicacion));
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<TiposDeUbicacionDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TiposDeUbicacionRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<TiposDeUbicacion>, IEnumerable<TiposDeUbicacionDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TiposDeUbicacionDTO> GetTiposDeUbicacionExcluir(int IdTipoUbicacion)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TiposDeUbicacionRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetTiposDeUbicacionExcluir(IdTipoUbicacion);
                var elementsDto = mapperBase.Map<IEnumerable<TiposDeUbicacion>, IEnumerable<TiposDeUbicacionDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	

		public TiposDeUbicacionDTO Single(int IdTipoUbicacion )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TiposDeUbicacionRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdTipoUbicacion);
                var entityDto = mapperBase.Map<TiposDeUbicacion, TiposDeUbicacionDTO>(entity);
     
                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TiposDeUbicacionDTO Get(int IdTipoUbicacion)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TiposDeUbicacionRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Get(IdTipoUbicacion);
                var entityDto = mapperBase.Map<TiposDeUbicacion, TiposDeUbicacionDTO>(entity);

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
        /// <returns></returns>
        public IEnumerable<TiposDeUbicacionDTO> Get()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TiposDeUbicacionRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Get();
                var entityDto = mapperBase.Map<IEnumerable<TiposDeUbicacion>, IEnumerable<TiposDeUbicacionDTO>>(entity);

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
		public bool Insert(TiposDeUbicacionDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TiposDeUbicacionRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<TiposDeUbicacionDTO, TiposDeUbicacion>(objectDto);                   
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
		public void Update(TiposDeUbicacionDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TiposDeUbicacionRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<TiposDeUbicacionDTO, TiposDeUbicacion>(objectDTO);                   
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
		public void Delete(TiposDeUbicacionDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TiposDeUbicacionRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<TiposDeUbicacionDTO, TiposDeUbicacion>(objectDto);                   
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



