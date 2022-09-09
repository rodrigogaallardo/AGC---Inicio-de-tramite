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
	public class LocalidadBL : ILocalidadBL<LocalidadDTO>
    {               
		private LocalidadRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public LocalidadBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LocalidadDTO, Localidad>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<LocalidadDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new LocalidadRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Localidad>, IEnumerable<LocalidadDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public LocalidadDTO Single(int Id )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new LocalidadRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(Id);
                var entityDto = mapperBase.Map<Localidad, LocalidadDTO>(entity);
     
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
		/// <param name="IdProvincia"></param>
		/// <returns></returns>	
		public IEnumerable<LocalidadDTO> GetByFKIdProvincia(int IdProvincia)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new LocalidadRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdProvincia(IdProvincia);
            var elementsDto = mapperBase.Map<IEnumerable<Localidad>, IEnumerable<LocalidadDTO>>(elements);
            return elementsDto;				
		}

        public IEnumerable<LocalidadDTO> GetByFKIdProvinciaExcluir(int IdProvincia, bool Excluir)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new LocalidadRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdProvinciaExcluir(IdProvincia, Excluir);
            var elementsDto = mapperBase.Map<IEnumerable<Localidad>, IEnumerable<LocalidadDTO>>(elements);
            return elementsDto;				
		}

		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(LocalidadDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new LocalidadRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<LocalidadDTO, Localidad>(objectDto);                   
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
		public void Update(LocalidadDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new LocalidadRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<LocalidadDTO, Localidad>(objectDTO);                   
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
		public void Delete(LocalidadDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new LocalidadRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<LocalidadDTO, Localidad>(objectDto);                   
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

