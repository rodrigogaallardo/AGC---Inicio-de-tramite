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
	public class EncomiendaRectificatoriaBL : IEncomiendaRectificatoriaBL<EncomiendaRectificatoriaDTO>
    {               
		private EncomiendaRectificatoriaRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public EncomiendaRectificatoriaBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EncomiendaRectificatoriaDTO, Encomienda_Rectificatoria>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<EncomiendaRectificatoriaDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaRectificatoriaRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Rectificatoria>, IEnumerable<EncomiendaRectificatoriaDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public EncomiendaRectificatoriaDTO Single(int id_encrec)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaRectificatoriaRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(id_encrec);
                var entityDto = mapperBase.Map<Encomienda_Rectificatoria, EncomiendaRectificatoriaDTO>(entity);
     
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
		/// <param name="IdEncomienda"></param>
		/// <returns></returns>	
		public EncomiendaRectificatoriaDTO GetByFKIdEncomienda(int IdEncomienda)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaRectificatoriaRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdEncomienda(IdEncomienda);
            var elementsDto = mapperBase.Map<Encomienda_Rectificatoria, EncomiendaRectificatoriaDTO>(elements);
            return elementsDto;				
		}

		#region Métodos de inserccion
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(EncomiendaRectificatoriaDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new EncomiendaRectificatoriaRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<EncomiendaRectificatoriaDTO, Encomienda_Rectificatoria>(objectDto);
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

		#region Métodos de eliminacion
		/// <summary>
		/// elimina la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>      
		public void Delete(EncomiendaRectificatoriaDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new EncomiendaRectificatoriaRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<EncomiendaRectificatoriaDTO, Encomienda_Rectificatoria>(objectDto);                   
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

