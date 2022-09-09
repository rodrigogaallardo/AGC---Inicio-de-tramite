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
	public class EncomiendaUbicacionesPropiedadHorizontalBL : IEncomiendaUbicacionesPropiedadHorizontalBL<EncomiendaUbicacionesPropiedadHorizontalDTO>
    {               
		private EncomiendaUbicacionesPropiedadHorizontalRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public EncomiendaUbicacionesPropiedadHorizontalBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EncomiendaUbicacionesPropiedadHorizontalDTO, Encomienda_Ubicaciones_PropiedadHorizontal>().ReverseMap()
                    .ForMember(dest => dest.IdEncomiendaPropiedadHorizontal, source => source.MapFrom(p => p.id_encomiendaprophorizontal))
                    .ForMember(dest => dest.IdEncomiendaUbicacion, source => source.MapFrom(p => p.id_encomiendaubicacion))
                    .ForMember(dest => dest.IdPropiedadHorizontal, source => source.MapFrom(p => p.id_propiedadhorizontal));

                cfg.CreateMap<Encomienda_Ubicaciones_PropiedadHorizontal, EncomiendaUbicacionesPropiedadHorizontalDTO>().ReverseMap()                
                    .ForMember(dest => dest.id_encomiendaprophorizontal, source => source.MapFrom(p => p.IdEncomiendaPropiedadHorizontal))
                    .ForMember(dest => dest.id_encomiendaubicacion, source => source.MapFrom(p => p.IdEncomiendaUbicacion))
                    .ForMember(dest => dest.id_propiedadhorizontal, source => source.MapFrom(p => p.IdPropiedadHorizontal));
                ;
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<EncomiendaUbicacionesPropiedadHorizontalDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaUbicacionesPropiedadHorizontalRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Ubicaciones_PropiedadHorizontal>, IEnumerable<EncomiendaUbicacionesPropiedadHorizontalDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public EncomiendaUbicacionesPropiedadHorizontalDTO Single(int IdEncomiendaPropiedadHorizontal )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaUbicacionesPropiedadHorizontalRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdEncomiendaPropiedadHorizontal);
                var entityDto = mapperBase.Map<Encomienda_Ubicaciones_PropiedadHorizontal, EncomiendaUbicacionesPropiedadHorizontalDTO>(entity);
     
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
		/// <param name="IdEncomiendaUbicacion"></param>
		/// <returns></returns>	
		public IEnumerable<EncomiendaUbicacionesPropiedadHorizontalDTO> GetByFKIdEncomiendaUbicacion(int IdEncomiendaUbicacion)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaUbicacionesPropiedadHorizontalRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdEncomiendaUbicacion(IdEncomiendaUbicacion);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Ubicaciones_PropiedadHorizontal>, IEnumerable<EncomiendaUbicacionesPropiedadHorizontalDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdPropiedadHorizontal"></param>
		/// <returns></returns>	
		public IEnumerable<EncomiendaUbicacionesPropiedadHorizontalDTO> GetByFKIdPropiedadHorizontal(int IdPropiedadHorizontal)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaUbicacionesPropiedadHorizontalRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdPropiedadHorizontal(IdPropiedadHorizontal);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Ubicaciones_PropiedadHorizontal>, IEnumerable<EncomiendaUbicacionesPropiedadHorizontalDTO>>(elements);
            return elementsDto;				
		}
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(EncomiendaUbicacionesPropiedadHorizontalDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new EncomiendaUbicacionesPropiedadHorizontalRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<EncomiendaUbicacionesPropiedadHorizontalDTO, Encomienda_Ubicaciones_PropiedadHorizontal>(objectDto);                   
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
		public void Update(EncomiendaUbicacionesPropiedadHorizontalDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new EncomiendaUbicacionesPropiedadHorizontalRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<EncomiendaUbicacionesPropiedadHorizontalDTO, Encomienda_Ubicaciones_PropiedadHorizontal>(objectDTO);                   
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
		public void Delete(EncomiendaUbicacionesPropiedadHorizontalDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new EncomiendaUbicacionesPropiedadHorizontalRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<EncomiendaUbicacionesPropiedadHorizontalDTO, Encomienda_Ubicaciones_PropiedadHorizontal>(objectDto);                   
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

