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
	public class TipoDocumentoPersonalBL : ITipoDocumentoPersonalBL<TipoDocumentoPersonalDTO>
    {               
		private TipoDocumentoPersonalRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public TipoDocumentoPersonalBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TipoDocumentoPersonalDTO, TipoDocumentoPersonal>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<TipoDocumentoPersonalDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TipoDocumentoPersonalRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<TipoDocumentoPersonal>, IEnumerable<TipoDocumentoPersonalDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TipoDocumentoPersonalDTO> GetDniPasaporte()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TipoDocumentoPersonalRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetDniPasaporte();
                var elementsDto = mapperBase.Map<IEnumerable<TipoDocumentoPersonal>, IEnumerable<TipoDocumentoPersonalDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public TipoDocumentoPersonalDTO Single(int TipoDocumentoPersonalId )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TipoDocumentoPersonalRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(TipoDocumentoPersonalId);
                var entityDto = mapperBase.Map<TipoDocumentoPersonal, TipoDocumentoPersonalDTO>(entity);
     
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
		public bool Insert(TipoDocumentoPersonalDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TipoDocumentoPersonalRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<TipoDocumentoPersonalDTO, TipoDocumentoPersonal>(objectDto);                   
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
		public void Update(TipoDocumentoPersonalDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TipoDocumentoPersonalRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<TipoDocumentoPersonalDTO, TipoDocumentoPersonal>(objectDTO);                   
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
		public void Delete(TipoDocumentoPersonalDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TipoDocumentoPersonalRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<TipoDocumentoPersonalDTO, TipoDocumentoPersonal>(objectDto);                   
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

