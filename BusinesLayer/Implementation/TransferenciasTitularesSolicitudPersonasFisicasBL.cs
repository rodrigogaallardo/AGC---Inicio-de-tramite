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
	public class TransferenciasTitularesSolicitudPersonasFisicasBL : ITransferenciasTitularesSolicitudPersonasFisicasBL<TransferenciasTitularesSolicitudPersonasFisicasDTO>
    {               
		private TransferenciasTitularesSolicitudPersonasFisicasRepository repo = null;
        private TransferenciasFirmantesSolicitudPersonasFisicasRepository repoFir = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
        IMapper mapperBaseFir;

        public TransferenciasTitularesSolicitudPersonasFisicasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TransferenciasTitularesSolicitudPersonasFisicasDTO, Transf_Titulares_Solicitud_PersonasFisicas>().ReverseMap()
                    .ForMember(dest => dest.IdPersonaFisica, source => source.MapFrom(p => p.id_personafisica))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdTipoDocumentoPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.CUIT, source => source.MapFrom(p => p.Cuit))
                    .ForMember(dest => dest.IdTipoiibb, source => source.MapFrom(p => p.id_tipoiibb))
                    .ForMember(dest => dest.IngresosBrutos, source => source.MapFrom(p => p.Ingresos_Brutos))
                    .ForMember(dest => dest.NumeroPuerta, source => source.MapFrom(p => p.Nro_Puerta))
                    .ForMember(dest => dest.IdLocalidad, source => source.MapFrom(p => p.Id_Localidad))
                    .ForMember(dest => dest.CodigoPostal, source => source.MapFrom(p => p.Codigo_Postal))
                    .ForMember(dest => dest.NumeroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.NumeroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.DtoFirmantes, source => source.Ignore())
                    .ForMember(dest => dest.Firmantes, source => source.Ignore());

                cfg.CreateMap<Transf_Titulares_Solicitud_PersonasFisicas, TransferenciasTitularesSolicitudPersonasFisicasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_personafisica, source => source.MapFrom(p => p.IdPersonaFisica))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocumentoPersonal))
                    .ForMember(dest => dest.Cuit, source => source.MapFrom(p => p.CUIT))
                    .ForMember(dest => dest.id_tipoiibb, source => source.MapFrom(p => p.IdTipoiibb))
                    .ForMember(dest => dest.Ingresos_Brutos, source => source.MapFrom(p => p.IngresosBrutos))
                    .ForMember(dest => dest.Nro_Puerta, source => source.MapFrom(p => p.NumeroPuerta))
                    .ForMember(dest => dest.Id_Localidad, source => source.MapFrom(p => p.IdLocalidad))
                    .ForMember(dest => dest.Codigo_Postal, source => source.MapFrom(p => p.CodigoPostal))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NumeroDocumento))
                    .ForMember(dest => dest.Transf_Firmantes_Solicitud_PersonasFisicas, source => source.Ignore());



            });
            mapperBase = config.CreateMapper();

            var configFirPF = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TransferenciasFirmantesSolicitudPersonasFisicasDTO, Transf_Firmantes_Solicitud_PersonasFisicas>().ReverseMap();

            });
            mapperBaseFir = configFirPF.CreateMapper();
        }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
        public IEnumerable<TransferenciasTitularesSolicitudPersonasFisicasDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasTitularesSolicitudPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_Solicitud_PersonasFisicas>, IEnumerable<TransferenciasTitularesSolicitudPersonasFisicasDTO>>(elements);
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
        /// <param name="IdPersonaFisica"></param>
        /// <returns></returns>
		public TransferenciasTitularesSolicitudPersonasFisicasDTO Single(int IdPersonaFisica )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasTitularesSolicitudPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdPersonaFisica);
                var entityDto = mapperBase.Map<Transf_Titulares_Solicitud_PersonasFisicas, TransferenciasTitularesSolicitudPersonasFisicasDTO>(entity);
     
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
		public IEnumerable<TransferenciasTitularesSolicitudPersonasFisicasDTO> GetByFKIdSolicitud(int IdSolicitud)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasTitularesSolicitudPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdSolicitud(IdSolicitud);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_Solicitud_PersonasFisicas>, IEnumerable<TransferenciasTitularesSolicitudPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}

        public IEnumerable<TransferenciasTitularesSolicitudPersonasFisicasDTO> GetByFKIdSolicitudIdPersonaFisica(int IdSolicitud, int IdPersonaJuridica)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasTitularesSolicitudPersonasFisicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdSolicitudIdPersonaFisica(IdSolicitud, IdPersonaJuridica);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_Solicitud_PersonasFisicas>, IEnumerable<TransferenciasTitularesSolicitudPersonasFisicasDTO>>(elements);
            return elementsDto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTipoDocumentoPersonal"></param>
        /// <returns></returns>	
        public IEnumerable<TransferenciasTitularesSolicitudPersonasFisicasDTO> GetByFKIdTipoDocumentoPersonal(int IdTipoDocumentoPersonal)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasTitularesSolicitudPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTipoDocumentoPersonal(IdTipoDocumentoPersonal);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_Solicitud_PersonasFisicas>, IEnumerable<TransferenciasTitularesSolicitudPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoiibb"></param>
		/// <returns></returns>	
		public IEnumerable<TransferenciasTitularesSolicitudPersonasFisicasDTO> GetByFKIdTipoiibb(int IdTipoiibb)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasTitularesSolicitudPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTipoiibb(IdTipoiibb);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_Solicitud_PersonasFisicas>, IEnumerable<TransferenciasTitularesSolicitudPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdLocalidad"></param>
		/// <returns></returns>	
		public IEnumerable<TransferenciasTitularesSolicitudPersonasFisicasDTO> GetByFKIdLocalidad(int IdLocalidad)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasTitularesSolicitudPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdLocalidad(IdLocalidad);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_Solicitud_PersonasFisicas>, IEnumerable<TransferenciasTitularesSolicitudPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(TransferenciasTitularesSolicitudPersonasFisicasDTO objectDto)
		{
		    try
		    {
                TransferenciasSolicitudesBL transferenciaSolicitudesBL = new TransferenciasSolicitudesBL();
                TransferenciasFirmantesSolicitudPersonasFisicasBL firBL = new TransferenciasFirmantesSolicitudPersonasFisicasBL();
                var transferenciaDTO = transferenciaSolicitudesBL.Single(objectDto.IdSolicitud);

                //if (transferenciaDTO.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && transferenciaDTO.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM && transferenciaDTO.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.PING)
                //    throw new Exception(Errors.SSIT_TRANSFERENCIAS_NO_ADMITE_CAMBIOS);

		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {
                    repoFir = new TransferenciasFirmantesSolicitudPersonasFisicasRepository(unitOfWork);
                    repo = new TransferenciasTitularesSolicitudPersonasFisicasRepository(unitOfWork);
                    TransferenciasFirmantesSolicitudPersonasFisicasRepository repofir = new TransferenciasFirmantesSolicitudPersonasFisicasRepository(unitOfWork);

                    if (objectDto.IdPersonaFisica > 0)
                    {
                        var deleteObjectfirDTO = firBL.GetByFKIdPersonaFisica(objectDto.IdPersonaFisica);
                        foreach (var item in deleteObjectfirDTO)
                        {
                            var dlFir = mapperBaseFir.Map<TransferenciasFirmantesSolicitudPersonasFisicasDTO,Transf_Firmantes_Solicitud_PersonasFisicas>(item);
                            repofir.Delete(dlFir);
                        }
                        var deleteObject = new TransferenciasTitularesSolicitudPersonasFisicasDTO()
                        {
                            IdPersonaFisica = objectDto.IdPersonaFisica
                        };
                        var deleteObjectDTO = mapperBase.Map<TransferenciasTitularesSolicitudPersonasFisicasDTO, Transf_Titulares_Solicitud_PersonasFisicas>(deleteObject);
                        repo.Delete(deleteObjectDTO);
                    }

                    var elementDto = mapperBase.Map<TransferenciasTitularesSolicitudPersonasFisicasDTO, Transf_Titulares_Solicitud_PersonasFisicas>(objectDto);
                    var elementDtoFirmantes = mapperBaseFir.Map<TransferenciasFirmantesSolicitudPersonasFisicasDTO, Transf_Firmantes_Solicitud_PersonasFisicas>(objectDto.DtoFirmantes);

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
		public void Update(TransferenciasTitularesSolicitudPersonasFisicasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TransferenciasTitularesSolicitudPersonasFisicasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<TransferenciasTitularesSolicitudPersonasFisicasDTO, Transf_Titulares_Solicitud_PersonasFisicas>(objectDTO);                   
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
		public void Delete(TransferenciasTitularesSolicitudPersonasFisicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TransferenciasTitularesSolicitudPersonasFisicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<TransferenciasTitularesSolicitudPersonasFisicasDTO, Transf_Titulares_Solicitud_PersonasFisicas>(objectDto);                   
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
					repo = new TransferenciasTitularesSolicitudPersonasFisicasRepository(unitOfWork);                    
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
		public void DeleteByFKIdTipoDocumentoPersonal(int IdTipoDocumentoPersonal)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new TransferenciasTitularesSolicitudPersonasFisicasRepository(unitOfWork);                    
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
		public void DeleteByFKIdTipoiibb(int IdTipoiibb)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new TransferenciasTitularesSolicitudPersonasFisicasRepository(unitOfWork);                    
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
					repo = new TransferenciasTitularesSolicitudPersonasFisicasRepository(unitOfWork);                    
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
		
		

		#endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <param name="Cuit"></param>
        /// <param name="IdPersonaFisica"></param>
        /// <returns></returns>
        public IEnumerable<TransferenciasTitularesSolicitudPersonasFisicasDTO> GetByIdSolicitudCuitIdPersonaFisica(int IdSolicitud, string Cuit, int IdPersonaFisica)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasTitularesSolicitudPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.GetByIdSolicitudCuitIdPersonaFisica(IdSolicitud,Cuit,IdPersonaFisica);
                var entityDto = mapperBase.Map<IEnumerable<Transf_Titulares_Solicitud_PersonasFisicas>, IEnumerable<TransferenciasTitularesSolicitudPersonasFisicasDTO>>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

