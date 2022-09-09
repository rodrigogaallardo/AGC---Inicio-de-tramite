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
	public class ConsultaPadronTitularesSolicitudPersonasFisicasBL : IConsultaPadronTitularesSolicitudPersonasFisicasBL<ConsultaPadronTitularesSolicitudPersonasFisicasDTO>
    {               
		private ConsultaPadronTitularesSolicitudPersonasFisicasRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public ConsultaPadronTitularesSolicitudPersonasFisicasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ConsultaPadronTitularesSolicitudPersonasFisicasDTO, CPadron_Titulares_Solicitud_PersonasFisicas>().ReverseMap()
                    .ForMember(dest => dest.IdPersonaFisica, source => source.MapFrom(p => p.id_personafisica))
                    .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                    .ForMember(dest => dest.IdTipoDocumentoPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.CUIT, source => source.MapFrom(p => p.Cuit))
                    .ForMember(dest => dest.IdTipoiibb, source => source.MapFrom(p => p.id_tipoiibb))
                    .ForMember(dest => dest.IngresosBrutos, source => source.MapFrom(p => p.Ingresos_Brutos))
                    .ForMember(dest => dest.NumeroPuerta, source => source.MapFrom(p => p.Nro_Puerta))
                    .ForMember(dest => dest.IdLocalidad, source => source.MapFrom(p => p.Id_Localidad))
                    .ForMember(dest => dest.CodigoPostal, source => source.MapFrom(p => p.Codigo_Postal))
                    .ForMember(dest => dest.NumeroDocumento, source => source.MapFrom(p => p.Nro_Documento));

                cfg.CreateMap<CPadron_Titulares_Solicitud_PersonasFisicas, ConsultaPadronTitularesSolicitudPersonasFisicasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_personafisica, source => source.MapFrom(p => p.IdPersonaFisica))
                    .ForMember(dest => dest.id_cpadron, source => source.MapFrom(p => p.IdConsultaPadron))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocumentoPersonal))
                    .ForMember(dest => dest.Cuit, source => source.MapFrom(p => p.CUIT))
                    .ForMember(dest => dest.id_tipoiibb, source => source.MapFrom(p => p.IdTipoiibb))
                    .ForMember(dest => dest.Ingresos_Brutos, source => source.MapFrom(p => p.IngresosBrutos))
                    .ForMember(dest => dest.Nro_Puerta, source => source.MapFrom(p => p.NumeroPuerta))
                    .ForMember(dest => dest.Id_Localidad, source => source.MapFrom(p => p.IdLocalidad))
                    .ForMember(dest => dest.Codigo_Postal, source => source.MapFrom(p => p.CodigoPostal))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NumeroDocumento));
            });
            mapperBase = config.CreateMapper();
        }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
        public IEnumerable<ConsultaPadronTitularesSolicitudPersonasFisicasDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronTitularesSolicitudPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<CPadron_Titulares_Solicitud_PersonasFisicas>, IEnumerable<ConsultaPadronTitularesSolicitudPersonasFisicasDTO>>(elements);
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
		public ConsultaPadronTitularesSolicitudPersonasFisicasDTO Single(int IdPersonaFisica )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronTitularesSolicitudPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdPersonaFisica);
                var entityDto = mapperBase.Map<CPadron_Titulares_Solicitud_PersonasFisicas, ConsultaPadronTitularesSolicitudPersonasFisicasDTO>(entity);
     
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
		public IEnumerable<ConsultaPadronTitularesSolicitudPersonasFisicasDTO> GetByFKIdConsultaPadron(int IdConsultaPadron)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new ConsultaPadronTitularesSolicitudPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdConsultaPadron(IdConsultaPadron);
            var elementsDto = mapperBase.Map<IEnumerable<CPadron_Titulares_Solicitud_PersonasFisicas>, IEnumerable<ConsultaPadronTitularesSolicitudPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoDocumentoPersonal"></param>
		/// <returns></returns>	
		public IEnumerable<ConsultaPadronTitularesSolicitudPersonasFisicasDTO> GetByFKIdTipoDocumentoPersonal(int IdTipoDocumentoPersonal)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new ConsultaPadronTitularesSolicitudPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTipoDocumentoPersonal(IdTipoDocumentoPersonal);
            var elementsDto = mapperBase.Map<IEnumerable<CPadron_Titulares_Solicitud_PersonasFisicas>, IEnumerable<ConsultaPadronTitularesSolicitudPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoiibb"></param>
		/// <returns></returns>	
		public IEnumerable<ConsultaPadronTitularesSolicitudPersonasFisicasDTO> GetByFKIdTipoiibb(int IdTipoiibb)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new ConsultaPadronTitularesSolicitudPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTipoiibb(IdTipoiibb);
            var elementsDto = mapperBase.Map<IEnumerable<CPadron_Titulares_Solicitud_PersonasFisicas>, IEnumerable<ConsultaPadronTitularesSolicitudPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdLocalidad"></param>
		/// <returns></returns>	
		public IEnumerable<ConsultaPadronTitularesSolicitudPersonasFisicasDTO> GetByFKIdLocalidad(int IdLocalidad)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new ConsultaPadronTitularesSolicitudPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdLocalidad(IdLocalidad);
            var elementsDto = mapperBase.Map<IEnumerable<CPadron_Titulares_Solicitud_PersonasFisicas>, IEnumerable<ConsultaPadronTitularesSolicitudPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(ConsultaPadronTitularesSolicitudPersonasFisicasDTO objectDto)
		{
		    try
		    {
                ConsultaPadronSolicitudesBL consultaPadronSolicitudesBL = new ConsultaPadronSolicitudesBL();
                var consultaPadronDTO = consultaPadronSolicitudesBL.Single(objectDto.IdConsultaPadron);

                if (consultaPadronDTO.IdEstado != (int)Constantes.ConsultaPadronEstados.COMP && consultaPadronDTO.IdEstado != (int)Constantes.ConsultaPadronEstados.INCOM && consultaPadronDTO.IdEstado != (int)Constantes.ConsultaPadronEstados.PING)
                    throw new Exception(Errors.SSIT_CPADRON_NO_ADMITE_CAMBIOS);

		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new ConsultaPadronTitularesSolicitudPersonasFisicasRepository(unitOfWork);

                    if (objectDto.IdPersonaFisica > 0)
                    {
                        var deleteObject = new ConsultaPadronTitularesSolicitudPersonasFisicasDTO()
                        {
                            IdPersonaFisica = objectDto.IdPersonaFisica
                        };
                        var deleteObjectDTO = mapperBase.Map<ConsultaPadronTitularesSolicitudPersonasFisicasDTO, CPadron_Titulares_Solicitud_PersonasFisicas>(deleteObject);
                        repo.Delete(deleteObjectDTO);
                    }

                    var elementDto = mapperBase.Map<ConsultaPadronTitularesSolicitudPersonasFisicasDTO, CPadron_Titulares_Solicitud_PersonasFisicas>(objectDto);                   
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
		public void Update(ConsultaPadronTitularesSolicitudPersonasFisicasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new ConsultaPadronTitularesSolicitudPersonasFisicasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<ConsultaPadronTitularesSolicitudPersonasFisicasDTO, CPadron_Titulares_Solicitud_PersonasFisicas>(objectDTO);                   
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
		public void Delete(ConsultaPadronTitularesSolicitudPersonasFisicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new ConsultaPadronTitularesSolicitudPersonasFisicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<ConsultaPadronTitularesSolicitudPersonasFisicasDTO, CPadron_Titulares_Solicitud_PersonasFisicas>(objectDto);                   
		            var insertOk = repo.Delete(elementDto);
		            unitOfWork.Commit();
		        }
		    }
		    catch (Exception ex)
		    {
		        throw ex;
		    }
		}
		public void DeleteByFKIdConsultaPadron(int IdConsultaPadron)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new ConsultaPadronTitularesSolicitudPersonasFisicasRepository(unitOfWork);                    
					var elements = repo.GetByFKIdConsultaPadron(IdConsultaPadron);
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
					repo = new ConsultaPadronTitularesSolicitudPersonasFisicasRepository(unitOfWork);                    
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
					repo = new ConsultaPadronTitularesSolicitudPersonasFisicasRepository(unitOfWork);                    
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
					repo = new ConsultaPadronTitularesSolicitudPersonasFisicasRepository(unitOfWork);                    
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
        public IEnumerable<ConsultaPadronTitularesSolicitudPersonasFisicasDTO> GetByIdConsultaPadronCuitIdPersonaFisica(int IdSolicitud, string Cuit, int IdPersonaFisica)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronTitularesSolicitudPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.GetByIdConsultaPadronCuitIdPersonaFisica(IdSolicitud,Cuit,IdPersonaFisica);
                var entityDto = mapperBase.Map<IEnumerable<CPadron_Titulares_Solicitud_PersonasFisicas>, IEnumerable<ConsultaPadronTitularesSolicitudPersonasFisicasDTO>>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

