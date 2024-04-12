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
using StaticClass;

namespace BusinesLayer.Implementation
{
	public class TransferenciasTitularesPersonasFisicasBL : ITransferenciasTitularesPersonasFisicasBL<TransferenciasTitularesPersonasFisicasDTO>
    {               
		private TransferenciasTitularesPersonasFisicasRepository repo = null;
        private TransferenciasFirmantesPersonasFisicasRepository repoFir = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		IMapper mapperFirmantes;       

        public TransferenciasTitularesPersonasFisicasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TransferenciasTitularesPersonasFisicasDTO, Transf_Titulares_PersonasFisicas>().ReverseMap()
                    .ForMember(dest => dest.IdPersonaFisica, source => source.MapFrom(p => p.id_personafisica))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdTipodocPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.NumeroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.IdTipoiibb, source => source.MapFrom(p => p.id_tipoiibb))
                    .ForMember(dest => dest.IngresosBrutos, source => source.MapFrom(p => p.Ingresos_Brutos))
                    .ForMember(dest => dest.NumeroPuerta, source => source.MapFrom(p => p.Nro_Puerta))
                    .ForMember(dest => dest.IdLocalidad, source => source.MapFrom(p => p.id_Localidad))
                    .ForMember(dest => dest.DtoFirmantes, source => source.Ignore())
                    .ForMember(dest => dest.Firmantes, source => source.Ignore())
                    .ForMember(dest => dest.CodigoPostal, source => source.MapFrom(p => p.Codigo_Postal));

                cfg.CreateMap<Transf_Titulares_PersonasFisicas, TransferenciasTitularesPersonasFisicasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_personafisica, source => source.MapFrom(p => p.IdPersonaFisica))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipodocPersonal))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NumeroDocumento))
                    .ForMember(dest => dest.id_tipoiibb, source => source.MapFrom(p => p.IdTipoiibb))
                    .ForMember(dest => dest.Ingresos_Brutos, source => source.MapFrom(p => p.IngresosBrutos))
                    .ForMember(dest => dest.Nro_Puerta, source => source.MapFrom(p => p.NumeroPuerta))
                    .ForMember(dest => dest.id_Localidad, source => source.MapFrom(p => p.IdLocalidad))
                    .ForMember(dest => dest.Transf_Firmantes_PersonasFisicas, source => source.Ignore())
                    .ForMember(dest => dest.Codigo_Postal, source => source.MapFrom(p => p.CodigoPostal));
            });
            mapperBase = config.CreateMapper();

             var configFirmantes = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TransferenciasFirmantesPersonasFisicasDTO, Transf_Firmantes_PersonasFisicas>().ReverseMap()
                    .ForMember(dest => dest.IdFirmantePersonaFisica, source => source.MapFrom(p => p.id_firmante_pf))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdPersonaFisica, source => source.MapFrom(p => p.id_personafisica))
                    .ForMember(dest => dest.IdTipoDocumentoPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.NumeroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.IdTipoCaracter, source => source.MapFrom(p => p.id_tipocaracter))
                    .ForMember(dest => dest.Cuit, source => source.MapFrom(p => p.Cuit));

                cfg.CreateMap<Transf_Firmantes_PersonasFisicas, TransferenciasFirmantesPersonasFisicasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_firmante_pf, source => source.MapFrom(p => p.IdFirmantePersonaFisica))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_personafisica, source => source.MapFrom(p => p.IdPersonaFisica))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocumentoPersonal))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NumeroDocumento))
                    .ForMember(dest => dest.id_tipocaracter, source => source.MapFrom(p => p.IdTipoCaracter))
                    .ForMember(dest => dest.Cuit, source => source.MapFrom(p => p.Cuit));
            });

             mapperFirmantes = configFirmantes.CreateMapper(); 

        }
		
        public IEnumerable<TransferenciasTitularesPersonasFisicasDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_PersonasFisicas>, IEnumerable<TransferenciasTitularesPersonasFisicasDTO>>(elements);
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
        /// <param name="Cuit"></param>
        /// <param name="IdPersonaFisica"></param>
        /// <returns></returns>
        public IEnumerable<TransferenciasTitularesPersonasFisicasDTO> GetByIdTransferenciasCuitIdPersonaFisica(int IdSolicitud, string Cuit, int IdPersonaFisica)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByIdTransferenciasCuitIdPersonaFisica(IdSolicitud, Cuit, IdPersonaFisica);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_PersonasFisicas>, IEnumerable<TransferenciasTitularesPersonasFisicasDTO>>(elements);
            return elementsDto;			
        }
		public TransferenciasTitularesPersonasFisicasDTO Single(int IdPersonaFisica )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdPersonaFisica);
                var entityDto = mapperBase.Map<Transf_Titulares_PersonasFisicas, TransferenciasTitularesPersonasFisicasDTO>(entity);
     
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
		public IEnumerable<TransferenciasTitularesPersonasFisicasDTO> GetByFKIdSolicitud(int IdSolicitud)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdSolicitud(IdSolicitud);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_PersonasFisicas>, IEnumerable<TransferenciasTitularesPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <param name="IdPersonaJuridica"></param>
        /// <returns></returns>
        public IEnumerable<TransferenciasTitularesPersonasFisicasDTO> GetByFKIdSolicitudIdPersonaFisica(int IdSolicitud, int IdPersonaJuridica)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdSolicitudIdPersonaFisica(IdSolicitud, IdPersonaJuridica);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_PersonasFisicas>, IEnumerable<TransferenciasTitularesPersonasFisicasDTO>>(elements);
            return elementsDto;
        }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipodocPersonal"></param>
		/// <returns></returns>	
		public IEnumerable<TransferenciasTitularesPersonasFisicasDTO> GetByFKIdTipodocPersonal(int IdTipodocPersonal)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTipodocPersonal(IdTipodocPersonal);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_PersonasFisicas>, IEnumerable<TransferenciasTitularesPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoiibb"></param>
		/// <returns></returns>	
		public IEnumerable<TransferenciasTitularesPersonasFisicasDTO> GetByFKIdTipoiibb(int IdTipoiibb)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTipoiibb(IdTipoiibb);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_PersonasFisicas>, IEnumerable<TransferenciasTitularesPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdLocalidad"></param>
		/// <returns></returns>	
		public IEnumerable<TransferenciasTitularesPersonasFisicasDTO> GetByFKIdLocalidad(int IdLocalidad)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdLocalidad(IdLocalidad);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_PersonasFisicas>, IEnumerable<TransferenciasTitularesPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="CreateUser"></param>
		/// <returns></returns>	
		public IEnumerable<TransferenciasTitularesPersonasFisicasDTO> GetByFKCreateUser(Guid CreateUser)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKCreateUser(CreateUser);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_PersonasFisicas>, IEnumerable<TransferenciasTitularesPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="LastUpdateUser"></param>
		/// <returns></returns>	
		public IEnumerable<TransferenciasTitularesPersonasFisicasDTO> GetByFKLastUpdateUser(Guid LastUpdateUser)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKLastUpdateUser(LastUpdateUser);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_PersonasFisicas>, IEnumerable<TransferenciasTitularesPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(TransferenciasTitularesPersonasFisicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {
                    TransferenciasSolicitudesBL transferenciasSolicitudesBL = new TransferenciasSolicitudesBL();
                    var consultaPadronDTO = transferenciasSolicitudesBL.Single(objectDto.IdSolicitud);
                    //if (consultaPadronDTO.IdEstado != (int)Constantes.ConsultaPadronEstados.COMP && consultaPadronDTO.IdEstado != (int)Constantes.ConsultaPadronEstados.INCOM && consultaPadronDTO.IdEstado != (int)Constantes.ConsultaPadronEstados.PING)
                    //    throw new Exception(Errors.SSIT_CPADRON_NO_ADMITE_CAMBIOS);
                       
                    repoFir = new TransferenciasFirmantesPersonasFisicasRepository(unitOfWork);                                                                           
		            repo = new TransferenciasTitularesPersonasFisicasRepository(unitOfWork);

                    if (objectDto.IdPersonaFisica > 0)
                    {                        
                        var firmantes = repoFir.GetByFKIdSolicitudIdPersonaFisica(objectDto.IdSolicitud, objectDto.IdPersonaFisica);
                        foreach (var firmante in firmantes)
                            repoFir.Delete(firmante);

                        var elementDtoDel = mapperBase.Map<TransferenciasTitularesPersonasFisicasDTO, Transf_Titulares_PersonasFisicas>(objectDto);
                        repo.Delete(elementDtoDel);

                    }                    
                 
                    var elementDto = mapperBase.Map<TransferenciasTitularesPersonasFisicasDTO, Transf_Titulares_PersonasFisicas>(objectDto);
                    var elementDtoFirmantes = mapperFirmantes.Map<TransferenciasFirmantesPersonasFisicasDTO, Transf_Firmantes_PersonasFisicas>(objectDto.DtoFirmantes);

                    repo.Insert(elementDto);
                    elementDtoFirmantes.id_personafisica = elementDto.id_personafisica;
                    repoFir.Insert(elementDtoFirmantes);


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
		public void Update(TransferenciasTitularesPersonasFisicasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TransferenciasTitularesPersonasFisicasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<TransferenciasTitularesPersonasFisicasDTO, Transf_Titulares_PersonasFisicas>(objectDTO);                   
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
		public void Delete(TransferenciasTitularesPersonasFisicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TransferenciasTitularesPersonasFisicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<TransferenciasTitularesPersonasFisicasDTO, Transf_Titulares_PersonasFisicas>(objectDto);                   
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
					repo = new TransferenciasTitularesPersonasFisicasRepository(unitOfWork);                    
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
		public void DeleteByFKIdTipodocPersonal(int IdTipodocPersonal)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new TransferenciasTitularesPersonasFisicasRepository(unitOfWork);                    
					var elements = repo.GetByFKIdTipodocPersonal(IdTipodocPersonal);
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
		public void DeleteByFKIdTipoiibb(int IdTipoiibb)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new TransferenciasTitularesPersonasFisicasRepository(unitOfWork);                    
					var elements = repo.GetByFKIdTipoiibb(IdTipoiibb);
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
		public void DeleteByFKIdLocalidad(int IdLocalidad)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new TransferenciasTitularesPersonasFisicasRepository(unitOfWork);                    
					var elements = repo.GetByFKIdLocalidad(IdLocalidad);
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
		public void DeleteByFKCreateUser(Guid CreateUser)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new TransferenciasTitularesPersonasFisicasRepository(unitOfWork);                    
					var elements = repo.GetByFKCreateUser(CreateUser);
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
		public void DeleteByFKLastUpdateUser(Guid LastUpdateUser)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new TransferenciasTitularesPersonasFisicasRepository(unitOfWork);                    
					var elements = repo.GetByFKLastUpdateUser(LastUpdateUser);
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

