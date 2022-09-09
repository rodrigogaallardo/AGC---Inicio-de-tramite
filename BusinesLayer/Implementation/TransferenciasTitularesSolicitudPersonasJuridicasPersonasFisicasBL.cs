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
	public class TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasBL : ITransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasBL<TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasDTO>
    {               
		private TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasDTO, Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas>().ReverseMap()
                    .ForMember(dest => dest.IdTitularPersonaJuridica, source => source.MapFrom(p => p.id_titular_pj))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                    .ForMember(dest => dest.IdTipoDocumentoPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.FirmanteMismaPersona, source => source.MapFrom(p => p.firmante_misma_persona))
                    .ForMember(dest => dest.NumeroDocumento, source => source.MapFrom(p => p.Nro_Documento));

                cfg.CreateMap<Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas, TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_titular_pj, source => source.MapFrom(p => p.IdTitularPersonaJuridica))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocumentoPersonal))
                    .ForMember(dest => dest.firmante_misma_persona, source => source.MapFrom(p => p.FirmanteMismaPersona))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NumeroDocumento));
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas>, IEnumerable<TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasDTO> GetByIdSolicitudIdPersonaJuridica(int IdSolicitud, int IdPersonaJuridica)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByIdSolicitudIdPersonaJuridica(IdSolicitud, IdPersonaJuridica);
                var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas>, IEnumerable<TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasDTO Single(int IdTitularPersonaJuridica )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdTitularPersonaJuridica);
                var entityDto = mapperBase.Map<Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas, TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasDTO>(entity);
     
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
		/// <param name="IdConsultaPadron"></param>
		/// <returns></returns>	
		public IEnumerable<TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasDTO> GetByFKIdSolicitud(int IdSolicitud)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdSolicitud(IdSolicitud);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas>, IEnumerable<TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdPersonaJuridica"></param>
		/// <returns></returns>	
		public IEnumerable<TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasDTO> GetByFKIdPersonaJuridica(int IdPersonaJuridica)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdPersonaJuridica(IdPersonaJuridica);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas>, IEnumerable<TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoDocumentoPersonal"></param>
		/// <returns></returns>	
		public IEnumerable<TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasDTO> GetByFKIdTipoDocumentoPersonal(int IdTipoDocumentoPersonal)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTipoDocumentoPersonal(IdTipoDocumentoPersonal);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas>, IEnumerable<TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasDTO, Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas>(objectDto);                   
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
		public void Update(TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasDTO, Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas>(objectDTO);                   
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
		public void Delete(TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasDTO, Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas>(objectDto);                   
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
					repo = new TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasRepository(unitOfWork);                    
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
		public void DeleteByFKIdPersonaJuridica(int IdPersonaJuridica)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasRepository(unitOfWork);                    
					var elements = repo.GetByFKIdPersonaJuridica(IdPersonaJuridica);
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
		public void DeleteByFKIdTipoDocumentoPersonal(int IdTipoDocumentoPersonal)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasRepository(unitOfWork);                    
					var elements = repo.GetByFKIdTipoDocumentoPersonal(IdTipoDocumentoPersonal);
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

