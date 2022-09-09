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
    public class SGITareaDocumentosAdjuntosBL : ISGITareaDocumentosAdjuntosBL<SGITareaDocumentosAdjuntosDTO>
    {               
		private SGITareaDocumentosAdjuntosRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public SGITareaDocumentosAdjuntosBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SGITareaDocumentosAdjuntosDTO, SGI_Tarea_Documentos_Adjuntos>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }

        public IEnumerable<SGITareaDocumentosAdjuntosDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SGITareaDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<SGI_Tarea_Documentos_Adjuntos>, IEnumerable<SGITareaDocumentosAdjuntosDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SGITareaDocumentosAdjuntosDTO Single(int id_doc_adj)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SGITareaDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(id_doc_adj);
                var entityDto = mapperBase.Map<SGI_Tarea_Documentos_Adjuntos, SGITareaDocumentosAdjuntosDTO>(entity);
     
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
		/// <param name="id_tramitetarea"></param>
		/// <returns></returns>	
        public IEnumerable<SGITareaDocumentosAdjuntosDTO> GetByFKid_tramitetarea(int id_tramitetarea)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SGITareaDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKid_tramitetarea(id_tramitetarea);
             var elementsDto = mapperBase.Map<IEnumerable<SGI_Tarea_Documentos_Adjuntos>, IEnumerable<SGITareaDocumentosAdjuntosDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id_tdocreq"></param>
		/// <returns></returns>	
		public IEnumerable<SGITareaDocumentosAdjuntosDTO> GetByFKid_tdocreq(int id_tdocreq)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SGITareaDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKid_tdocreq(id_tdocreq);
            var elementsDto = mapperBase.Map<IEnumerable<SGI_Tarea_Documentos_Adjuntos>, IEnumerable<SGITareaDocumentosAdjuntosDTO>>(elements);
            return elementsDto;				
		}
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(SGITareaDocumentosAdjuntosDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SGITareaDocumentosAdjuntosRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<SGITareaDocumentosAdjuntosDTO, SGI_Tarea_Documentos_Adjuntos>(objectDto);                   
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
		public void Update(SGITareaDocumentosAdjuntosDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SGITareaDocumentosAdjuntosRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<SGITareaDocumentosAdjuntosDTO, SGI_Tarea_Documentos_Adjuntos>(objectDTO);                   
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
		public void Delete(SGITareaDocumentosAdjuntosDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SGITareaDocumentosAdjuntosRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<SGITareaDocumentosAdjuntosDTO, SGI_Tarea_Documentos_Adjuntos>(objectDto);                   
		            var insertOk = repo.Delete(elementDto);
		            unitOfWork.Commit();
		        }
		    }
		    catch (Exception ex)
		    {
		        throw ex;
		    }
		}
		public void DeleteByFKid_tramitetarea(int id_tramitetarea)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new SGITareaDocumentosAdjuntosRepository(unitOfWork);                    
					var elements = repo.GetByFKid_tramitetarea(id_tramitetarea);
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
		public void DeleteByFKid_tdocreq(int id_tdocreq)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new SGITareaDocumentosAdjuntosRepository(unitOfWork);                    
					var elements = repo.GetByFKid_tdocreq(id_tdocreq);
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
    }
}

