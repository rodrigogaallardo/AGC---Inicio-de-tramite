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
	public class TransferenciasFirmantesPersonasFisicasBL : ITransferenciasFirmantesPersonasFisicasBL<TransferenciasFirmantesPersonasFisicasDTO>
    {               
		private TransferenciasFirmantesPersonasFisicasRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public TransferenciasFirmantesPersonasFisicasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TransferenciasFirmantesPersonasFisicasDTO, Transf_Firmantes_PersonasFisicas>().ReverseMap()
                    .ForMember(dest => dest.IdFirmantePersonaFisica, source => source.MapFrom(p => p.id_firmante_pf))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdPersonaFisica, source => source.MapFrom(p => p.id_personafisica))
                    .ForMember(dest => dest.IdTipoDocumentoPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.NumeroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.IdTipoCaracter, source => source.MapFrom(p => p.id_tipocaracter));

                cfg.CreateMap<Transf_Firmantes_PersonasFisicas, TransferenciasFirmantesPersonasFisicasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_firmante_pf, source => source.MapFrom(p => p.IdFirmantePersonaFisica))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_personafisica, source => source.MapFrom(p => p.IdPersonaFisica))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocumentoPersonal))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NumeroDocumento))
                    .ForMember(dest => dest.id_tipocaracter, source => source.MapFrom(p => p.IdTipoCaracter));
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<TransferenciasFirmantesPersonasFisicasDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasFirmantesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Transf_Firmantes_PersonasFisicas>, IEnumerable<TransferenciasFirmantesPersonasFisicasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public TransferenciasFirmantesPersonasFisicasDTO Single(int IdFirmantePersonaFisica )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasFirmantesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdFirmantePersonaFisica);
                var entityDto = mapperBase.Map<Transf_Firmantes_PersonasFisicas, TransferenciasFirmantesPersonasFisicasDTO>(entity);
     
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
		public IEnumerable<TransferenciasFirmantesPersonasFisicasDTO> GetByFKIdSolicitud(int IdSolicitud)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasFirmantesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdSolicitud(IdSolicitud);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Firmantes_PersonasFisicas>, IEnumerable<TransferenciasFirmantesPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <param name="IdPersonaFisica"></param>
        /// <returns></returns>
        public IEnumerable<TransferenciasFirmantesPersonasFisicasDTO> GetByFKIdSolicitudIdPersonaFisica(int IdSolicitud, int IdPersonaFisica)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasFirmantesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdSolicitudIdPersonaFisica(IdSolicitud,IdPersonaFisica);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Firmantes_PersonasFisicas>, IEnumerable<TransferenciasFirmantesPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdPersonaFisica"></param>
		/// <returns></returns>	
		public IEnumerable<TransferenciasFirmantesPersonasFisicasDTO> GetByFKIdPersonaFisica(int IdPersonaFisica)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasFirmantesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdPersonaFisica(IdPersonaFisica);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Firmantes_PersonasFisicas>, IEnumerable<TransferenciasFirmantesPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoDocumentoPersonal"></param>
		/// <returns></returns>	
		public IEnumerable<TransferenciasFirmantesPersonasFisicasDTO> GetByFKIdTipoDocumentoPersonal(int IdTipoDocumentoPersonal)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasFirmantesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTipoDocumentoPersonal(IdTipoDocumentoPersonal);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Firmantes_PersonasFisicas>, IEnumerable<TransferenciasFirmantesPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoCaracter"></param>
		/// <returns></returns>	
		public IEnumerable<TransferenciasFirmantesPersonasFisicasDTO> GetByFKIdTipoCaracter(int IdTipoCaracter)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasFirmantesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTipoCaracter(IdTipoCaracter);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Firmantes_PersonasFisicas>, IEnumerable<TransferenciasFirmantesPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(TransferenciasFirmantesPersonasFisicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TransferenciasFirmantesPersonasFisicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<TransferenciasFirmantesPersonasFisicasDTO, Transf_Firmantes_PersonasFisicas>(objectDto);                   
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
		public void Update(TransferenciasFirmantesPersonasFisicasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TransferenciasFirmantesPersonasFisicasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<TransferenciasFirmantesPersonasFisicasDTO, Transf_Firmantes_PersonasFisicas>(objectDTO);                   
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
		public void Delete(TransferenciasFirmantesPersonasFisicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TransferenciasFirmantesPersonasFisicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<TransferenciasFirmantesPersonasFisicasDTO, Transf_Firmantes_PersonasFisicas>(objectDto);                   
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
					repo = new TransferenciasFirmantesPersonasFisicasRepository(unitOfWork);                    
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
		public void DeleteByFKIdPersonaFisica(int IdPersonaFisica)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new TransferenciasFirmantesPersonasFisicasRepository(unitOfWork);                    
					var elements = repo.GetByFKIdPersonaFisica(IdPersonaFisica);
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
					repo = new TransferenciasFirmantesPersonasFisicasRepository(unitOfWork);                    
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
		public void DeleteByFKIdTipoCaracter(int IdTipoCaracter)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new TransferenciasFirmantesPersonasFisicasRepository(unitOfWork);                    
					var elements = repo.GetByFKIdTipoCaracter(IdTipoCaracter);
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

