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
	public class SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasBL : ISSITSolicitudesTitularesPersonasJuridicasPersonasFisicasBL<SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO>
    {               
		private SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO, SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas>().ReverseMap()
                    .ForMember(dest => dest.IdTitularPj, source => source.MapFrom(p => p.id_titular_pj))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                    .ForMember(dest => dest.IdTipoDocPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.NroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.IdFirmantePj, source => source.MapFrom(p => p.id_firmante_pj))
                    .ForMember(dest => dest.FirmanteMismaPersona, source => source.MapFrom(p => p.firmante_misma_persona));
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas>, IEnumerable<SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO Single(int IdTitularPj )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdTitularPj);
                var entityDto = mapperBase.Map<SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas, SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO>(entity);
     
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
		public IEnumerable<SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO> GetByFKIdSolicitud(int IdSolicitud)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdSolicitud(IdSolicitud);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas>, IEnumerable<SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdPersonaJuridica"></param>
		/// <returns></returns>	
		public IEnumerable<SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO> GetByFKIdPersonaJuridica(int IdPersonaJuridica)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdPersonaJuridica(IdPersonaJuridica);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas>, IEnumerable<SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
        public IEnumerable<SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO> GetByIdSolicitudIdPersonaJuridica(int IdSolicitud, int IdPersonaJuridica)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByIdSolicitudIdPersonaJuridica(IdSolicitud, IdPersonaJuridica);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas>, IEnumerable<SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO>>(elements);
            return elementsDto;
        }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoDocPersonal"></param>
		/// <returns></returns>	
		public IEnumerable<SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO> GetByFKIdTipoDocPersonal(int IdTipoDocPersonal)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTipoDocPersonal(IdTipoDocPersonal);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas>, IEnumerable<SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdFirmantePj"></param>
		/// <returns></returns>	
		public IEnumerable<SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO> GetByFKIdFirmantePj(int IdFirmantePj)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdFirmantePj(IdFirmantePj);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas>, IEnumerable<SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO, SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas>(objectDto);                   
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
		public void Update(SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO, SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas>(objectDTO);                   
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
		public void Delete(SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO, SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas>(objectDto);                   
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
					repo = new SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasRepository(unitOfWork);                    
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
					repo = new SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasRepository(unitOfWork);                    
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
		public void DeleteByFKIdTipoDocPersonal(int IdTipoDocPersonal)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasRepository(unitOfWork);                    
					var elements = repo.GetByFKIdTipoDocPersonal(IdTipoDocPersonal);
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
		public void DeleteByFKIdFirmantePj(int IdFirmantePj)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasRepository(unitOfWork);                    
					var elements = repo.GetByFKIdFirmantePj(IdFirmantePj);
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

