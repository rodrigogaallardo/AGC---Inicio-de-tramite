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
	public class SGITramitesTareasTransferenciasBL : ISGITramitesTareasTransferenciasBL<SGITramitesTareasTransferenciasDTO>
    {               
		private SGITramitesTareasTransferenciasRepository repo = null;
        private SGITramitesTareasRepository repoTT = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public SGITramitesTareasTransferenciasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
			    cfg.CreateMap<SGITramitesTareasTransferenciasDTO, SGI_Tramites_Tareas_TRANSF>().ReverseMap()
                    .ForMember(dest => dest.Id, source => source.MapFrom(p => p.id_rel_tt_TRANSF))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdTramiteTarea, source => source.MapFrom(p => p.id_tramitetarea));

                cfg.CreateMap<SGI_Tramites_Tareas_TRANSF, SGITramitesTareasTransferenciasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_rel_tt_TRANSF, source => source.MapFrom(p => p.Id))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_tramitetarea, source => source.MapFrom(p => p.IdTramiteTarea));


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
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
        public IEnumerable<SGITramitesTareasTransferenciasDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SGITramitesTareasTransferenciasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<SGI_Tramites_Tareas_TRANSF>, IEnumerable<SGITramitesTareasTransferenciasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
	    /// <summary>
	    /// 
	    /// </summary>
	    /// <param name="Id"></param>
	    /// <returns></returns>
		public SGITramitesTareasTransferenciasDTO Single(int Id )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SGITramitesTareasTransferenciasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(Id);
                var entityDto = mapperBase.Map<SGI_Tramites_Tareas_TRANSF, SGITramitesTareasTransferenciasDTO>(entity);
     
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
		public IEnumerable<SGITramitesTareasTransferenciasDTO> GetByFKIdTramiteTarea(int IdTramiteTarea)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SGITramitesTareasTransferenciasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTramiteTarea(IdTramiteTarea);
            var elementsDto = mapperBase.Map<IEnumerable<SGI_Tramites_Tareas_TRANSF>, IEnumerable<SGITramitesTareasTransferenciasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdSolicitud"></param>
		/// <returns></returns>	
		public IEnumerable<SGITramitesTareasTransferenciasDTO> GetByFKIdSolicitud(int IdSolicitud)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SGITramitesTareasTransferenciasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdSolicitud(IdSolicitud);
            var elementsDto = mapperBase.Map<IEnumerable<SGI_Tramites_Tareas_TRANSF>, IEnumerable<SGITramitesTareasTransferenciasDTO>>(elements);
            return elementsDto;				
		}


        public void Delete(SGITramitesTareasTransferenciasDTO sgiTTHabDTO, SGITramitesTareasDTO sgiTTDTO)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SGITramitesTareasTransferenciasRepository(unitOfWork);
                    repoTT = new SGITramitesTareasRepository(unitOfWork);
                    var elementEntityTTHab = mapperBase.Map<SGITramitesTareasTransferenciasDTO, SGI_Tramites_Tareas_TRANSF>(sgiTTHabDTO);
                    var elementEntityTT = mapperBase.Map<SGITramitesTareasDTO, SGI_Tramites_Tareas>(sgiTTDTO);
                    repo.Delete(elementEntityTTHab);
                    repoTT.Delete(elementEntityTT);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SGITramitesTareasTransferenciasDTO GetByFKIdTramiteTareasIdSolicitud(int id_tramitetarea, int id_solicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SGITramitesTareasTransferenciasRepository(this.uowF.GetUnitOfWork());

                var elements = repo.GetByFKIdTramiteTareasIdSolicitud(id_tramitetarea, id_solicitud);
                var elementsDto = mapperBase.Map<IEnumerable<SGI_Tramites_Tareas_TRANSF>, IEnumerable<SGITramitesTareasTransferenciasDTO>>(elements);
                return elementsDto.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(SGITramitesTareasTransferenciasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SGITramitesTareasTransferenciasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<SGITramitesTareasTransferenciasDTO, SGI_Tramites_Tareas_TRANSF>(objectDto);                   
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
		public void Update(SGITramitesTareasTransferenciasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SGITramitesTareasTransferenciasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<SGITramitesTareasTransferenciasDTO, SGI_Tramites_Tareas_TRANSF>(objectDTO);                   
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
		public void Delete(SGITramitesTareasTransferenciasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SGITramitesTareasTransferenciasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<SGITramitesTareasTransferenciasDTO, SGI_Tramites_Tareas_TRANSF>(objectDto);                   
		            var insertOk = repo.Delete(elementDto);
		            unitOfWork.Commit();
		        }
		    }
		    catch (Exception ex)
		    {
		        throw ex;
		    }
		}
		public void DeleteByFKIdTramiteTarea(int IdTramiteTarea)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new SGITramitesTareasTransferenciasRepository(unitOfWork);                    
					var elements = repo.GetByFKIdTramiteTarea(IdTramiteTarea);
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
		public void DeleteByFKIdSolicitud(int IdSolicitud)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new SGITramitesTareasTransferenciasRepository(unitOfWork);                    
					var elements = repo.GetByFKIdSolicitud(IdSolicitud);
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

