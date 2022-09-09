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
	public class RubrosImpactoAmbientalBL : IRubrosImpactoAmbientalBL<RubrosImpactoAmbientalDTO>
    {               
		private RubrosImpactoAmbientalRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public RubrosImpactoAmbientalBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RubrosImpactoAmbientalDTO, Rel_Rubros_ImpactoAmbiental>().ReverseMap()
                    .ForMember(dest => dest.IdRubroImpactoAmbiental, source => source.MapFrom(p => p.id_relrubImp))
                    .ForMember(dest => dest.IdRubro, source => source.MapFrom(p => p.id_rubro))
                    .ForMember(dest => dest.IdImpactoAmbiental, source => source.MapFrom(p => p.id_ImpactoAmbiental));

                cfg.CreateMap<Rel_Rubros_ImpactoAmbiental, RubrosImpactoAmbientalDTO>().ReverseMap()                
                    .ForMember(dest => dest.id_relrubImp, source => source.MapFrom(p => p.IdRubroImpactoAmbiental))
                    .ForMember(dest => dest.id_rubro, source => source.MapFrom(p => p.IdRubro))
                    .ForMember(dest => dest.id_ImpactoAmbiental, source => source.MapFrom(p => p.IdImpactoAmbiental));
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<RubrosImpactoAmbientalDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosImpactoAmbientalRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Rel_Rubros_ImpactoAmbiental>, IEnumerable<RubrosImpactoAmbientalDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public RubrosImpactoAmbientalDTO Single(int IdRubroImpactoAmbiental )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosImpactoAmbientalRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdRubroImpactoAmbiental);
                var entityDto = mapperBase.Map<Rel_Rubros_ImpactoAmbiental, RubrosImpactoAmbientalDTO>(entity);
     
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
		/// <param name="IdImpactoAmbiental"></param>
		/// <returns></returns>	
		public IEnumerable<RubrosImpactoAmbientalDTO> GetByFKIdImpactoAmbiental(int IdImpactoAmbiental)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new RubrosImpactoAmbientalRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdImpactoAmbiental(IdImpactoAmbiental);
            var elementsDto = mapperBase.Map<IEnumerable<Rel_Rubros_ImpactoAmbiental>, IEnumerable<RubrosImpactoAmbientalDTO>>(elements);
            return elementsDto;				
		}
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(RubrosImpactoAmbientalDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new RubrosImpactoAmbientalRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<RubrosImpactoAmbientalDTO, Rel_Rubros_ImpactoAmbiental>(objectDto);                   
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
		public void Update(RubrosImpactoAmbientalDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new RubrosImpactoAmbientalRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<RubrosImpactoAmbientalDTO, Rel_Rubros_ImpactoAmbiental>(objectDTO);                   
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
		public void Delete(RubrosImpactoAmbientalDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new RubrosImpactoAmbientalRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<RubrosImpactoAmbientalDTO, Rel_Rubros_ImpactoAmbiental>(objectDto);                   
		            var insertOk = repo.Delete(elementDto);
		            unitOfWork.Commit();
		        }
		    }
		    catch (Exception ex)
		    {
		        throw ex;
		    }
		}
		public void DeleteByFKIdImpactoAmbiental(int IdImpactoAmbiental)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new RubrosImpactoAmbientalRepository(unitOfWork);                    
					var elements = repo.GetByFKIdImpactoAmbiental(IdImpactoAmbiental);
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
        /// <param name="Superficie"></param>
        /// <param name="IdRubro"></param>
        /// <returns></returns>
        public  RubrosImpactoAmbientalDTO GetImpactoAmbiental(decimal Superficie, int IdRubro)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosImpactoAmbientalRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByFKIdRubro(IdRubro);                 
                var elementsDto = mapperBase.Map<IEnumerable<Rel_Rubros_ImpactoAmbiental>, IEnumerable<RubrosImpactoAmbientalDTO>>(elements);
                elementsDto = elementsDto.Where(p => p.DesdeM2 <= Superficie && p.HastaM2 >= Superficie);
                return elementsDto.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

