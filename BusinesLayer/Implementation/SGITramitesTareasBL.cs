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
	public class SGITramitesTareasBL : ISGITramitesTareasBL<SGITramitesTareasDTO>
    {               
		private SGITramitesTareasRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public SGITramitesTareasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {			
                cfg.CreateMap<SGITramitesTareasDTO, SGI_Tramites_Tareas>().ReverseMap()
                    .ForMember(dest => dest.IdTramiteTarea, source => source.MapFrom(p => p.id_tramitetarea))
                    .ForMember(dest => dest.IdTarea, source => source.MapFrom(p => p.id_tarea))
                    .ForMember(dest => dest.IdResultado, source => source.MapFrom(p => p.id_resultado))
                    .ForMember(dest => dest.FechaInicioTramiteTarea, source => source.MapFrom(p => p.FechaInicio_tramitetarea))
                    .ForMember(dest => dest.FechaCierreTramiteTarea, source => source.MapFrom(p => p.FechaCierre_tramitetarea))
                    .ForMember(dest => dest.UsuarioAsignadoTramiteTarea, source => source.MapFrom(p => p.UsuarioAsignado_tramitetarea))
                    .ForMember(dest => dest.FechaAsignacionTramiteTarea, source => source.MapFrom(p => p.FechaAsignacion_tramtietarea))
                    .ForMember(dest => dest.IdProximaTarea, source => source.MapFrom(p => p.id_proxima_tarea));

                cfg.CreateMap<SGI_Tramites_Tareas, SGITramitesTareasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_tramitetarea, source => source.MapFrom(p => p.IdTramiteTarea))
                    .ForMember(dest => dest.id_tarea, source => source.MapFrom(p => p.IdTarea))
                    .ForMember(dest => dest.id_resultado, source => source.MapFrom(p => p.IdResultado))
                    .ForMember(dest => dest.FechaInicio_tramitetarea, source => source.MapFrom(p => p.FechaInicioTramiteTarea))
                    .ForMember(dest => dest.FechaCierre_tramitetarea, source => source.MapFrom(p => p.FechaCierreTramiteTarea))
                    .ForMember(dest => dest.UsuarioAsignado_tramitetarea, source => source.MapFrom(p => p.UsuarioAsignadoTramiteTarea))
                    .ForMember(dest => dest.FechaAsignacion_tramtietarea, source => source.MapFrom(p => p.FechaAsignacionTramiteTarea))
                    .ForMember(dest => dest.id_proxima_tarea, source => source.MapFrom(p => p.IdProximaTarea));
                });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<SGITramitesTareasDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SGITramitesTareasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<SGI_Tramites_Tareas>, IEnumerable<SGITramitesTareasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public SGITramitesTareasDTO Single(int IdTramiteTarea )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SGITramitesTareasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdTramiteTarea);
                var entityDto = mapperBase.Map<SGI_Tramites_Tareas, SGITramitesTareasDTO>(entity);
     
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
		/// <param name="IdTarea"></param>
		/// <returns></returns>	
		public IEnumerable<SGITramitesTareasDTO> GetByFKIdTarea(int IdTarea)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SGITramitesTareasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTarea(IdTarea);
            var elementsDto = mapperBase.Map<IEnumerable<SGI_Tramites_Tareas>, IEnumerable<SGITramitesTareasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdResultado"></param>
		/// <returns></returns>	
		public IEnumerable<SGITramitesTareasDTO> GetByFKIdResultado(int IdResultado)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SGITramitesTareasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdResultado(IdResultado);
            var elementsDto = mapperBase.Map<IEnumerable<SGI_Tramites_Tareas>, IEnumerable<SGITramitesTareasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="UsuarioAsignadoTramiteTarea"></param>
		/// <returns></returns>	
		public IEnumerable<SGITramitesTareasDTO> GetByFKUsuarioAsignadoTramiteTarea(Guid UsuarioAsignadoTramiteTarea)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SGITramitesTareasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKUsuarioAsignadoTramiteTarea(UsuarioAsignadoTramiteTarea);
            var elementsDto = mapperBase.Map<IEnumerable<SGI_Tramites_Tareas>, IEnumerable<SGITramitesTareasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdProximaTarea"></param>
		/// <returns></returns>	
		public IEnumerable<SGITramitesTareasDTO> GetByFKIdProximaTarea(int IdProximaTarea)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SGITramitesTareasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdProximaTarea(IdProximaTarea);
            var elementsDto = mapperBase.Map<IEnumerable<SGI_Tramites_Tareas>, IEnumerable<SGITramitesTareasDTO>>(elements);
            return elementsDto;				
		}

		public string Buscar_ObservacionPlanchetaDispoFirmada(int id_solicitud, DateTime? fechaDispoFirmada)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
			repo = new SGITramitesTareasRepository(this.uowF.GetUnitOfWork());
			return repo.Buscar_ObservacionPlanchetaDispoFirmada(id_solicitud, fechaDispoFirmada);
		}

		public string Buscar_ObservacionPlancheta(int id_solicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SGITramitesTareasRepository(this.uowF.GetUnitOfWork());
            return repo.Buscar_ObservacionPlancheta(id_solicitud);
        }

        public string Buscar_ObservacionPlanchetaTransmision(int id_solicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SGITramitesTareasRepository(this.uowF.GetUnitOfWork());
            return repo.Buscar_ObservacionPlanchetaTransmision(id_solicitud);
        }
        #region Métodos de actualizacion e insert
        /// <summary>
        /// Inserta la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public bool Insert(SGITramitesTareasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SGITramitesTareasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<SGITramitesTareasDTO, SGI_Tramites_Tareas>(objectDto);                   
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
		public void Update(SGITramitesTareasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SGITramitesTareasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<SGITramitesTareasDTO, SGI_Tramites_Tareas>(objectDTO);                   
		            repo.Update(elementDTO);
		            unitOfWork.Commit();           
		        }
		    }
		    catch (Exception ex)
		    {
		        throw ex;
		    }
		}

        public string GetObservacionPlanchetaTransmision(int id_solicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SGITramitesTareasRepository(this.uowF.GetUnitOfWork());
            return repo.GetObservacionPlanchetaTransmision(id_solicitud);
        }


        #endregion
        #region Métodos de actualizacion e insert
        /// <summary>
        /// elimina la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>      
        public void Delete(SGITramitesTareasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SGITramitesTareasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<SGITramitesTareasDTO, SGI_Tramites_Tareas>(objectDto);                   
		            var insertOk = repo.Delete(elementDto);
		            unitOfWork.Commit();
		        }
		    }
		    catch (Exception ex)
		    {
		        throw ex;
		    }
		}
		public void DeleteByFKIdTarea(int IdTarea)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new SGITramitesTareasRepository(unitOfWork);                    
					var elements = repo.GetByFKIdTarea(IdTarea);
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
		public void DeleteByFKIdResultado(int IdResultado)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new SGITramitesTareasRepository(unitOfWork);                    
					var elements = repo.GetByFKIdResultado(IdResultado);
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
		public void DeleteByFKUsuarioAsignadoTramiteTarea(Guid UsuarioAsignadoTramiteTarea)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new SGITramitesTareasRepository(unitOfWork);                    
					var elements = repo.GetByFKUsuarioAsignadoTramiteTarea(UsuarioAsignadoTramiteTarea);
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
		public void DeleteByFKIdProximaTarea(int IdProximaTarea)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new SGITramitesTareasRepository(unitOfWork);                    
					var elements = repo.GetByFKIdProximaTarea(IdProximaTarea);
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

        public void DeleteTareaByIdTramiteTareaIdTipoTramite(int IdTramiteTarea, int IdTipoTramite)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SGITramitesTareasRepository(unitOfWork);
                    repo.DeleteTareaByIdTramiteTarea(IdTramiteTarea, IdTipoTramite);

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

