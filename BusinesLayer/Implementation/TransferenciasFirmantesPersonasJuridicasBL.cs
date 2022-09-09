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
	public class TransferenciasFirmantesPersonasJuridicasBL : ITransferenciasFirmantesPersonasJuridicasBL<TransferenciasFirmantesPersonasJuridicasDTO>
    {               
		private TransferenciasFirmantesPersonasJuridicasRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public TransferenciasFirmantesPersonasJuridicasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TransferenciasFirmantesPersonasJuridicasDTO, Transf_Firmantes_PersonasJuridicas>().ReverseMap()
                    .ForMember(dest => dest.IdFirmantePersonaJuridica, source => source.MapFrom(p => p.id_firmante_pj))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                    .ForMember(dest => dest.IdTipoDocumentoPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.NumeroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.IdTipoCaracter, source => source.MapFrom(p => p.id_tipocaracter))
                    .ForMember(dest => dest.CargoFirmantePersonaJuridica, source => source.MapFrom(p => p.cargo_firmante_pj));

                cfg.CreateMap<Transf_Firmantes_PersonasJuridicas, TransferenciasFirmantesPersonasJuridicasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_firmante_pj, source => source.MapFrom(p => p.IdFirmantePersonaJuridica))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocumentoPersonal))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NumeroDocumento))
                    .ForMember(dest => dest.id_tipocaracter, source => source.MapFrom(p => p.IdTipoCaracter))
                    .ForMember(dest => dest.cargo_firmante_pj, source => source.MapFrom(p => p.CargoFirmantePersonaJuridica));
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<TransferenciasFirmantesPersonasJuridicasDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasFirmantesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Transf_Firmantes_PersonasJuridicas>, IEnumerable<TransferenciasFirmantesPersonasJuridicasDTO>>(elements);
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
        /// <returns></returns>
        public string[] GetCargoFirmantesPersonasJuridicas()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasFirmantesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());

                var elements = repo.GetAll();

                return elements.Where(p => !string.IsNullOrWhiteSpace(p.cargo_firmante_pj)).Select(p => p.cargo_firmante_pj).Distinct().ToArray();    

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

		public TransferenciasFirmantesPersonasJuridicasDTO Single(int IdFirmantePersonaJuridica )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasFirmantesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdFirmantePersonaJuridica);
                var entityDto = mapperBase.Map<Transf_Firmantes_PersonasJuridicas, TransferenciasFirmantesPersonasJuridicasDTO>(entity);
     
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
        /// <param name="IdPersonaJuridica"></param>
        /// <returns></returns>
        public IEnumerable<TransferenciasFirmantesPersonasJuridicasDTO> GetByFKIdSolicitudIdPersonaJuridica(int IdSolicitud, int IdPersonaJuridica)
        {   
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasFirmantesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByFKIdSolicitudIdPersonaJuridica(IdSolicitud,IdPersonaJuridica);
                var elementsDto = mapperBase.Map<IEnumerable<Transf_Firmantes_PersonasJuridicas>, IEnumerable<TransferenciasFirmantesPersonasJuridicasDTO>>(elements);
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
		/// <param name="IdSolicitud"></param>
		/// <returns></returns>	
		public IEnumerable<TransferenciasFirmantesPersonasJuridicasDTO> GetByFKIdSolicitud(int IdSolicitud)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasFirmantesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdSolicitud(IdSolicitud);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Firmantes_PersonasJuridicas>, IEnumerable<TransferenciasFirmantesPersonasJuridicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdPersonaJuridica"></param>
		/// <returns></returns>	
		public IEnumerable<TransferenciasFirmantesPersonasJuridicasDTO> GetByFKIdPersonaJuridica(int IdPersonaJuridica)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasFirmantesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdPersonaJuridica(IdPersonaJuridica);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Firmantes_PersonasJuridicas>, IEnumerable<TransferenciasFirmantesPersonasJuridicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoDocumentoPersonal"></param>
		/// <returns></returns>	
		public IEnumerable<TransferenciasFirmantesPersonasJuridicasDTO> GetByFKIdTipoDocumentoPersonal(int IdTipoDocumentoPersonal)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasFirmantesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTipoDocumentoPersonal(IdTipoDocumentoPersonal);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Firmantes_PersonasJuridicas>, IEnumerable<TransferenciasFirmantesPersonasJuridicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoCaracter"></param>
		/// <returns></returns>	
		public IEnumerable<TransferenciasFirmantesPersonasJuridicasDTO> GetByFKIdTipoCaracter(int IdTipoCaracter)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasFirmantesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTipoCaracter(IdTipoCaracter);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Firmantes_PersonasJuridicas>, IEnumerable<TransferenciasFirmantesPersonasJuridicasDTO>>(elements);
            return elementsDto;				
		}
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(TransferenciasFirmantesPersonasJuridicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TransferenciasFirmantesPersonasJuridicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<TransferenciasFirmantesPersonasJuridicasDTO, Transf_Firmantes_PersonasJuridicas>(objectDto);                   
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
		public void Update(TransferenciasFirmantesPersonasJuridicasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TransferenciasFirmantesPersonasJuridicasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<TransferenciasFirmantesPersonasJuridicasDTO, Transf_Firmantes_PersonasJuridicas>(objectDTO);                   
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
		public void Delete(TransferenciasFirmantesPersonasJuridicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TransferenciasFirmantesPersonasJuridicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<TransferenciasFirmantesPersonasJuridicasDTO, Transf_Firmantes_PersonasJuridicas>(objectDto);                   
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
					repo = new TransferenciasFirmantesPersonasJuridicasRepository(unitOfWork);                    
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
					repo = new TransferenciasFirmantesPersonasJuridicasRepository(unitOfWork);                    
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
					repo = new TransferenciasFirmantesPersonasJuridicasRepository(unitOfWork);                    
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
					repo = new TransferenciasFirmantesPersonasJuridicasRepository(unitOfWork);                    
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

