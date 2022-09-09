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
using DataAcess.EntityCustom;

namespace BusinesLayer.Implementation
{
	public class SGITareaCalificarObsDocsBL : ISGITareaCalificarObsDocsBL<SGITareaCalificarObsDocsDTO>
    {               
		private SGITareaCalificarObsDocsRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
        IMapper mapperGrilla;

        public SGITareaCalificarObsDocsBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SGITareaCalificarObsDocsDTO, SGI_Tarea_Calificar_ObsDocs>().ReverseMap();
            });
            mapperBase = config.CreateMapper();

            var configGrilla = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SGITareaCalificarObsDocsGrillaDTO, SGITareaCalificarObsGrillaEntity>().ReverseMap();
            });
            mapperGrilla = configGrilla.CreateMapper();
        }
		
        public IEnumerable<SGITareaCalificarObsDocsDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SGITareaCalificarObsDocsRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<SGI_Tarea_Calificar_ObsDocs>, IEnumerable<SGITareaCalificarObsDocsDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<SGITareaCalificarObsDocsGrillaDTO> GetByFKIdSolicitud(int id_Solicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SGITareaCalificarObsDocsRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdSolicitud(id_Solicitud);
            var elementsDto = mapperGrilla.Map<IEnumerable<SGITareaCalificarObsGrillaEntity>, IEnumerable<SGITareaCalificarObsDocsGrillaDTO>>(elements);
            return elementsDto;
        }
		public SGITareaCalificarObsDocsDTO Single(int Id )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SGITareaCalificarObsDocsRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(Id);
                var entityDto = mapperBase.Map<SGI_Tarea_Calificar_ObsDocs, SGITareaCalificarObsDocsDTO>(entity);
     
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
        /// <param name="IdTramiteTarea"></param>
        /// <returns></returns>	
        public IEnumerable<SGITareaCalificarObsDocsGrillaDTO> GetByFKIdObsGrupo(int id_ObsGrupo)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SGITareaCalificarObsDocsRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdObsGrupo(id_ObsGrupo);
            var elementsDto = mapperGrilla.Map<IEnumerable<SGITareaCalificarObsGrillaEntity>, IEnumerable<SGITareaCalificarObsDocsGrillaDTO>>(elements);
            return elementsDto;            
        }

		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(SGITareaCalificarObsDocsDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SGITareaCalificarObsDocsRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<SGITareaCalificarObsDocsDTO, SGI_Tarea_Calificar_ObsDocs>(objectDto);                   
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
		public void Update(SGITareaCalificarObsDocsDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SGITareaCalificarObsDocsRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<SGITareaCalificarObsDocsDTO, SGI_Tarea_Calificar_ObsDocs>(objectDTO);                   
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
		public void Delete(SGITareaCalificarObsDocsDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SGITareaCalificarObsDocsRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<SGITareaCalificarObsDocsDTO, SGI_Tarea_Calificar_ObsDocs>(objectDto);                   
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

        public bool ExistenObservacionesdetalleSinProcesar(int id_solicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SGITareaCalificarObsDocsRepository(this.uowF.GetUnitOfWork());
            var elements = repo.ExistenObservacionesdetalleSinProcesar(id_solicitud);
            return elements;
        }
    }
}

