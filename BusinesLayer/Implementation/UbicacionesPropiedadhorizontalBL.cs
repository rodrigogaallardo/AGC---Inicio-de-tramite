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
	public class UbicacionesPropiedadhorizontalBL : IUbicacionesPropiedadhorizontalBL<UbicacionesPropiedadhorizontalDTO>
    {               
		private UbicacionesPropiedadhorizontalRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public UbicacionesPropiedadhorizontalBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {				
                cfg.CreateMap<UbicacionesPropiedadhorizontalDTO, Ubicaciones_PropiedadHorizontal>().ReverseMap()
                    .ForMember(dest => dest.IdPropiedadHorizontal, source => source.MapFrom(p => p.id_propiedadhorizontal))
                    .ForMember(dest => dest.IdUbicacion, source => source.MapFrom(p => p.id_ubicacion));

                cfg.CreateMap<Ubicaciones_PropiedadHorizontal, UbicacionesPropiedadhorizontalDTO>().ReverseMap()
                    .ForMember(dest => dest.id_propiedadhorizontal, source => source.MapFrom(p => p.IdPropiedadHorizontal))
                    .ForMember(dest => dest.id_ubicacion, source => source.MapFrom(p => p.IdUbicacion));
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<UbicacionesPropiedadhorizontalDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesPropiedadhorizontalRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Ubicaciones_PropiedadHorizontal>, IEnumerable<UbicacionesPropiedadhorizontalDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public UbicacionesPropiedadhorizontalDTO Single(int IdPropiedadHorizontal )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesPropiedadhorizontalRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdPropiedadHorizontal);
                var entityDto = mapperBase.Map<Ubicaciones_PropiedadHorizontal, UbicacionesPropiedadhorizontalDTO>(entity);
     
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
		/// <param name="IdUbicacion"></param>
		/// <returns></returns>	
		public IEnumerable<UbicacionesPropiedadhorizontalDTO> GetByFKIdUbicacion(int IdUbicacion)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new UbicacionesPropiedadhorizontalRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdUbicacion(IdUbicacion);
            var elementsDto = mapperBase.Map<IEnumerable<Ubicaciones_PropiedadHorizontal>, IEnumerable<UbicacionesPropiedadhorizontalDTO>>(elements);
            return elementsDto;				
		}
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(UbicacionesPropiedadhorizontalDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new UbicacionesPropiedadhorizontalRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<UbicacionesPropiedadhorizontalDTO, Ubicaciones_PropiedadHorizontal>(objectDto);                   
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
		public void Update(UbicacionesPropiedadhorizontalDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new UbicacionesPropiedadhorizontalRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<UbicacionesPropiedadhorizontalDTO, Ubicaciones_PropiedadHorizontal>(objectDTO);                   
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
		public void Delete(UbicacionesPropiedadhorizontalDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new UbicacionesPropiedadhorizontalRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<UbicacionesPropiedadhorizontalDTO, Ubicaciones_PropiedadHorizontal>(objectDto);                   
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

