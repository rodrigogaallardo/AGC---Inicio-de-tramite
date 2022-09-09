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
	public class TransferenciasSolicitudesObservacionesBL : ITransferenciasSolicitudesObservacionesBL<TransferenciasSolicitudesObservacionesDTO>
    {               
		private TransferenciasSolicitudesObservacionesRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public TransferenciasSolicitudesObservacionesBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TransferenciasSolicitudesObservacionesDTO, Transf_Solicitudes_Observaciones>().ReverseMap()
                    .ForMember(dest => dest.Id, source => source.MapFrom(p => p.id_solobs))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.Observaciones, source => source.MapFrom(p => p.observaciones))
                    .ForMember(dest => dest.Leido, source => source.MapFrom(p => p.leido))
                    .ForMember(dest => dest.Usuario, source => source.MapFrom(p => p.aspnet_Users.Usuario))
                    .ForMember(dest => dest.UsuarioSGI, source => source.MapFrom(p => p.aspnet_Users.SGI_Profiles));

                cfg.CreateMap<Transf_Solicitudes_Observaciones, TransferenciasSolicitudesObservacionesDTO>().ReverseMap()
                    .ForMember(dest => dest.id_solobs, source => source.MapFrom(p => p.Id))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.observaciones, source => source.MapFrom(p => p.Observaciones))
                    .ForMember(dest => dest.leido, source => source.MapFrom(p => p.Leido));

                cfg.CreateMap<Usuario, UsuarioDTO>();

                cfg.CreateMap<SGI_Profiles, UsuarioDTO>()
                    .ForMember(dest => dest.Nombre, source => source.MapFrom(p => p.Nombres))
                    .ForMember(dest => dest.Apellido, source => source.MapFrom(p => p.Apellido));

            });
            mapperBase = config.CreateMapper();
        }
     
		public TransferenciasSolicitudesObservacionesDTO Single(int Id )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasSolicitudesObservacionesRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(Id);
                var entityDto = mapperBase.Map<Transf_Solicitudes_Observaciones, TransferenciasSolicitudesObservacionesDTO>(entity);
     
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
		/// <param name="IdSolicitud"></param>
		/// <returns></returns>	
		public IEnumerable<TransferenciasSolicitudesObservacionesDTO> GetByFKIdSolicitud(int IdSolicitud)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasSolicitudesObservacionesRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdSolicitud(IdSolicitud);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Solicitudes_Observaciones>, IEnumerable<TransferenciasSolicitudesObservacionesDTO>>(elements);
            return elementsDto;				
		}

        public IEnumerable<TransferenciasSolicitudesObservacionesDTO> GetByFKIdSolicitud2(int IdSolicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasSolicitudesObservacionesRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdSolicitud2(IdSolicitud);

            var elementsDto = (from e in elements
                               select new TransferenciasSolicitudesObservacionesDTO()
                               {
                                   Id = e.id_solobs,
                                   IdSolicitud = e.id_solicitud,
                                   Leido = e.leido,
                                   Observaciones = e.observaciones,
                                   CreateDate = e.CreateDate,
                                   CreateUser = e.CreateUser,
                                   NombreCompleto = e.userApeNom
                               }
                   ).ToList();

            return elementsDto;
        }

        #region Métodos de actualizacion e insert
        /// <summary>
        /// Inserta la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public bool Insert(TransferenciasSolicitudesObservacionesDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TransferenciasSolicitudesObservacionesRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<TransferenciasSolicitudesObservacionesDTO, Transf_Solicitudes_Observaciones>(objectDto);                   
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
		public void Update(TransferenciasSolicitudesObservacionesDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TransferenciasSolicitudesObservacionesRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<TransferenciasSolicitudesObservacionesDTO, Transf_Solicitudes_Observaciones>(objectDTO);                   
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
		public void Delete(TransferenciasSolicitudesObservacionesDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TransferenciasSolicitudesObservacionesRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<TransferenciasSolicitudesObservacionesDTO, Transf_Solicitudes_Observaciones>(objectDto);                   
		            var insertOk = repo.Delete(elementDto);
		            unitOfWork.Commit();
		        }
		    }
		    catch (Exception ex)
		    {
		        throw ex;
		    }
		}
		public void DeleteByFKIdSolicitud(int IdSolicitud)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new TransferenciasSolicitudesObservacionesRepository(unitOfWork);                    
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

