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
	public class TipoSociedadBL : ITipoSociedadBL<TipoSociedadDTO>
    {               
		private TipoSociedadRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public TipoSociedadBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TipoSociedadDTO, TipoSociedad>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<TipoSociedadDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TipoSociedadRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<TipoSociedad>, IEnumerable<TipoSociedadDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public TipoSociedadDTO Single(int Id )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TipoSociedadRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(Id);
                var entityDto = mapperBase.Map<TipoSociedad, TipoSociedadDTO>(entity);
     
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
		public bool Insert(TipoSociedadDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TipoSociedadRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<TipoSociedadDTO, TipoSociedad>(objectDto);                   
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
		public void Update(TipoSociedadDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TipoSociedadRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<TipoSociedadDTO, TipoSociedad>(objectDTO);                   
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
		public void Delete(TipoSociedadDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TipoSociedadRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<TipoSociedadDTO, TipoSociedad>(objectDto);                   
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

