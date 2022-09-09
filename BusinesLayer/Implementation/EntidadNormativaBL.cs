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
	public class EntidadNormativaBL : IEntidadNormativaBL<EntidadNormativaDTO>
    {               
		private EntidadNormativaRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public EntidadNormativaBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EntidadNormativaDTO, EntidadNormativa>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<EntidadNormativaDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EntidadNormativaRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<EntidadNormativa>, IEnumerable<EntidadNormativaDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public EntidadNormativaDTO Single(int Id )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EntidadNormativaRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(Id);
                var entityDto = mapperBase.Map<EntidadNormativa, EntidadNormativaDTO>(entity);
     
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
		public bool Insert(EntidadNormativaDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new EntidadNormativaRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<EntidadNormativaDTO, EntidadNormativa>(objectDto);                   
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
		public void Update(EntidadNormativaDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new EntidadNormativaRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<EntidadNormativaDTO, EntidadNormativa>(objectDTO);                   
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
		public void Delete(EntidadNormativaDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new EntidadNormativaRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<EntidadNormativaDTO, EntidadNormativa>(objectDto);                   
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

