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
	public class SSITSolicitudesFirmantesPersonasFisicasBL : ISSITSolicitudesFirmantesPersonasFisicasBL<SSITSolicitudesFirmantesPersonasFisicasDTO>
    {               
		private SSITSolicitudesFirmantesPersonasFisicasRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public SSITSolicitudesFirmantesPersonasFisicasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SSITSolicitudesFirmantesPersonasFisicasDTO, SSIT_Solicitudes_Firmantes_PersonasFisicas>().ReverseMap()
                    .ForMember(dest => dest.IdFirmantePf, source => source.MapFrom(p => p.id_firmante_pf))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdPersonaFisica, source => source.MapFrom(p => p.id_personafisica))
                    .ForMember(dest => dest.IdTipoDocPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.NroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.IdTipoCaracter, source => source.MapFrom(p => p.id_tipocaracter));

                cfg.CreateMap<SSIT_Solicitudes_Firmantes_PersonasFisicas, SSITSolicitudesFirmantesPersonasFisicasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_firmante_pf, source => source.MapFrom(p => p.IdFirmantePf))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_personafisica, source => source.MapFrom(p => p.IdPersonaFisica))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocPersonal))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NroDocumento))
                    .ForMember(dest => dest.id_tipocaracter, source => source.MapFrom(p => p.IdTipoCaracter));
            });
            mapperBase = config.CreateMapper();
        }
		
	
		public SSITSolicitudesFirmantesPersonasFisicasDTO Single(int IdFirmantePf )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesFirmantesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdFirmantePf);
                var entityDto = mapperBase.Map<SSIT_Solicitudes_Firmantes_PersonasFisicas, SSITSolicitudesFirmantesPersonasFisicasDTO>(entity);
     
                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<SSITSolicitudesFirmantesPersonasFisicasDTO> GetByIdSolicitudIdPersonaFisica(int id_solicitud, int IdPersonaFisica)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesFirmantesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByIdSolicitudIdPersonaFisica(id_solicitud, IdPersonaFisica);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Firmantes_PersonasFisicas>, IEnumerable<SSITSolicitudesFirmantesPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdSolicitud"></param>
		/// <returns></returns>	
		public IEnumerable<SSITSolicitudesFirmantesPersonasFisicasDTO> GetByFKIdSolicitud(int IdSolicitud)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesFirmantesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdSolicitud(IdSolicitud);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Firmantes_PersonasFisicas>, IEnumerable<SSITSolicitudesFirmantesPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdPersonaFisica"></param>
        /// <returns></returns>	
        public IEnumerable<SSITSolicitudesFirmantesPersonasFisicasDTO> GetByFKIdPersonaFisica(int IdPersonaFisica)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesFirmantesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdPersonaFisica(IdPersonaFisica);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Firmantes_PersonasFisicas>, IEnumerable<SSITSolicitudesFirmantesPersonasFisicasDTO>>(elements);
            return elementsDto;
        }
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(SSITSolicitudesFirmantesPersonasFisicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SSITSolicitudesFirmantesPersonasFisicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<SSITSolicitudesFirmantesPersonasFisicasDTO, SSIT_Solicitudes_Firmantes_PersonasFisicas>(objectDto);                   
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
		public void Update(SSITSolicitudesFirmantesPersonasFisicasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SSITSolicitudesFirmantesPersonasFisicasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<SSITSolicitudesFirmantesPersonasFisicasDTO, SSIT_Solicitudes_Firmantes_PersonasFisicas>(objectDTO);                   
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
		public void Delete(SSITSolicitudesFirmantesPersonasFisicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SSITSolicitudesFirmantesPersonasFisicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<SSITSolicitudesFirmantesPersonasFisicasDTO, SSIT_Solicitudes_Firmantes_PersonasFisicas>(objectDto);                   
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

    }
}

