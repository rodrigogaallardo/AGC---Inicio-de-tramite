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
	public class ProvinciaBL : IProvinciaBL<ProvinciaDTO>
    {               
		private ProvinciaRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public ProvinciaBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProvinciaDTO, Provincia>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<ProvinciaDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ProvinciaRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Provincia>, IEnumerable<ProvinciaDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public ProvinciaDTO Single(int Id )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ProvinciaRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(Id);
                var entityDto = mapperBase.Map<Provincia, ProvinciaDTO>(entity);
     
                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<ProvinciaDTO> GetProvincias()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ProvinciaRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetProvincias();
                var elementsDto = mapperBase.Map<IEnumerable<Provincia>, IEnumerable<ProvinciaDTO>>(elements);
                return elementsDto;
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
		public bool Insert(ProvinciaDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new ProvinciaRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<ProvinciaDTO, Provincia>(objectDto);                   
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
		public void Update(ProvinciaDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new ProvinciaRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<ProvinciaDTO, Provincia>(objectDTO);                   
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
		public void Delete(ProvinciaDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new ProvinciaRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<ProvinciaDTO, Provincia>(objectDto);                   
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

