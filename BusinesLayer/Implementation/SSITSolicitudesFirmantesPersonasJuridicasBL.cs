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
	public class SSITSolicitudesFirmantesPersonasJuridicasBL : ISSITSolicitudesFirmantesPersonasJuridicasBL<SSITSolicitudesFirmantesPersonasJuridicasDTO>
    {               
		private SSITSolicitudesFirmantesPersonasJuridicasRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public SSITSolicitudesFirmantesPersonasJuridicasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SSITSolicitudesFirmantesPersonasJuridicasDTO, SSIT_Solicitudes_Firmantes_PersonasJuridicas>().ReverseMap()
                    .ForMember(dest => dest.IdFirmantePj, source => source.MapFrom(p => p.id_firmante_pj))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                    .ForMember(dest => dest.IdTipoDocPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.NroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.IdTipoCaracter, source => source.MapFrom(p => p.id_tipocaracter))
                    .ForMember(dest => dest.CargoFirmantePj, source => source.MapFrom(p => p.cargo_firmante_pj));

                cfg.CreateMap<SSIT_Solicitudes_Firmantes_PersonasJuridicas, SSITSolicitudesFirmantesPersonasJuridicasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_firmante_pj, source => source.MapFrom(p => p.IdFirmantePj))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocPersonal))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NroDocumento))
                    .ForMember(dest => dest.id_tipocaracter, source => source.MapFrom(p => p.IdTipoCaracter))
                    .ForMember(dest => dest.cargo_firmante_pj, source => source.MapFrom(p => p.CargoFirmantePj));
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<SSITSolicitudesFirmantesPersonasJuridicasDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesFirmantesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Firmantes_PersonasJuridicas>, IEnumerable<SSITSolicitudesFirmantesPersonasJuridicasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public SSITSolicitudesFirmantesPersonasJuridicasDTO Single(int IdFirmantePj )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesFirmantesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdFirmantePj);
                var entityDto = mapperBase.Map<SSIT_Solicitudes_Firmantes_PersonasJuridicas, SSITSolicitudesFirmantesPersonasJuridicasDTO>(entity);
     
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
		public IEnumerable<SSITSolicitudesFirmantesPersonasJuridicasDTO> GetByFKIdSolicitud(int IdSolicitud)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesFirmantesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdSolicitud(IdSolicitud);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Firmantes_PersonasJuridicas>, IEnumerable<SSITSolicitudesFirmantesPersonasJuridicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdPersonaJuridica"></param>
		/// <returns></returns>	
		public IEnumerable<SSITSolicitudesFirmantesPersonasJuridicasDTO> GetByFKIdPersonaJuridica(int IdPersonaJuridica)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesFirmantesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdPersonaJuridica(IdPersonaJuridica);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Firmantes_PersonasJuridicas>, IEnumerable<SSITSolicitudesFirmantesPersonasJuridicasDTO>>(elements);
            return elementsDto;				
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <param name="IdPersonaJuridica"></param>
        /// <returns></returns>
        public IEnumerable<SSITSolicitudesFirmantesPersonasJuridicasDTO> GetByIdSolicitudIdPersonaJuridica(int IdSolicitud, int IdPersonaJuridica)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesFirmantesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByIdSolicitudIdPersonaJuridica(IdSolicitud, IdPersonaJuridica);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Firmantes_PersonasJuridicas>, IEnumerable<SSITSolicitudesFirmantesPersonasJuridicasDTO>>(elements);
            return elementsDto;
        }

        public string[] GetCargoFirmantesPersonasJuridicas()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesFirmantesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                return repo.GetCargoFirmantesPersonasJuridicas();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoDocPersonal"></param>
		/// <returns></returns>	
		public IEnumerable<SSITSolicitudesFirmantesPersonasJuridicasDTO> GetByFKIdTipoDocPersonal(int IdTipoDocPersonal)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesFirmantesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTipoDocPersonal(IdTipoDocPersonal);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Firmantes_PersonasJuridicas>, IEnumerable<SSITSolicitudesFirmantesPersonasJuridicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoCaracter"></param>
		/// <returns></returns>	
		public IEnumerable<SSITSolicitudesFirmantesPersonasJuridicasDTO> GetByFKIdTipoCaracter(int IdTipoCaracter)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesFirmantesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTipoCaracter(IdTipoCaracter);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Firmantes_PersonasJuridicas>, IEnumerable<SSITSolicitudesFirmantesPersonasJuridicasDTO>>(elements);
            return elementsDto;				
		}
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(SSITSolicitudesFirmantesPersonasJuridicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SSITSolicitudesFirmantesPersonasJuridicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<SSITSolicitudesFirmantesPersonasJuridicasDTO, SSIT_Solicitudes_Firmantes_PersonasJuridicas>(objectDto);                   
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
		public void Update(SSITSolicitudesFirmantesPersonasJuridicasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SSITSolicitudesFirmantesPersonasJuridicasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<SSITSolicitudesFirmantesPersonasJuridicasDTO, SSIT_Solicitudes_Firmantes_PersonasJuridicas>(objectDTO);                   
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
		public void Delete(SSITSolicitudesFirmantesPersonasJuridicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SSITSolicitudesFirmantesPersonasJuridicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<SSITSolicitudesFirmantesPersonasJuridicasDTO, SSIT_Solicitudes_Firmantes_PersonasJuridicas>(objectDto);                   
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
					repo = new SSITSolicitudesFirmantesPersonasJuridicasRepository(unitOfWork);                    
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
					repo = new SSITSolicitudesFirmantesPersonasJuridicasRepository(unitOfWork);                    
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
					repo = new SSITSolicitudesFirmantesPersonasJuridicasRepository(unitOfWork);                    
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
		public void DeleteByFKIdTipoCaracter(int IdTipoCaracter)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new SSITSolicitudesFirmantesPersonasJuridicasRepository(unitOfWork);                    
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

