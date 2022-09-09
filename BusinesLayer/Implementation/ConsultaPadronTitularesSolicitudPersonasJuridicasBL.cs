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
	public class ConsultaPadronTitularesSolicitudPersonasJuridicasBL : IConsultaPadronTitularesSolicitudPersonasJuridicasBL<ConsultaPadronTitularesSolicitudPersonasJuridicasDTO>
    {               
		private ConsultaPadronTitularesSolicitudPersonasJuridicasRepository repo = null;        
        private ConsultaPadronTitularesSolicitudPersonasJuridicasPersonasFisicasRepository repoPersonaFisicaJuridica = null;
        private IUnitOfWorkFactory uowF = null;
        
        IMapper mapperBase;
        IMapper mapperBasePersonaFisicaJuridica;
		         
        public ConsultaPadronTitularesSolicitudPersonasJuridicasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ConsultaPadronTitularesSolicitudPersonasJuridicasDTO, CPadron_Titulares_Solicitud_PersonasJuridicas>().ReverseMap()
                    .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                    .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                    .ForMember(dest => dest.IdTipoSociedad, source => source.MapFrom(p => p.Id_TipoSociedad))
                    .ForMember(dest => dest.RazonSocial, source => source.MapFrom(p => p.Razon_Social))
                    .ForMember(dest => dest.IdTipoiibb, source => source.MapFrom(p => p.id_tipoiibb))
                    .ForMember(dest => dest.Numeroiibb, source => source.MapFrom(p => p.Nro_IIBB))
                    .ForMember(dest => dest.NumeroPuerta, source => source.MapFrom(p => p.NroPuerta))
                    .ForMember(dest => dest.IdLocalidad, source => source.MapFrom(p => p.id_localidad))
                    .ForMember(dest => dest.CodigoPostal, source => source.MapFrom(p => p.Codigo_Postal));

                cfg.CreateMap<CPadron_Titulares_Solicitud_PersonasJuridicas, ConsultaPadronTitularesSolicitudPersonasJuridicasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))
                    .ForMember(dest => dest.id_cpadron, source => source.MapFrom(p => p.IdConsultaPadron))
                    .ForMember(dest => dest.Id_TipoSociedad, source => source.MapFrom(p => p.IdTipoSociedad))
                    .ForMember(dest => dest.Razon_Social, source => source.MapFrom(p => p.RazonSocial))
                    .ForMember(dest => dest.id_tipoiibb, source => source.MapFrom(p => p.IdTipoiibb))
                    .ForMember(dest => dest.Nro_IIBB, source => source.MapFrom(p => p.Numeroiibb))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NumeroPuerta))
                    .ForMember(dest => dest.id_localidad, source => source.MapFrom(p => p.IdLocalidad))
                    .ForMember(dest => dest.Codigo_Postal, source => source.MapFrom(p => p.CodigoPostal));
            });
            mapperBase = config.CreateMapper();

            var configPJ = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ConsultaPadronTitularesSolicitudPersonasJuridicasPersonasFisicasDTO, CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas>().ReverseMap()
                    .ForMember(dest => dest.IdTitularPersonaJuridica, source => source.MapFrom(p => p.id_titular_pj))		      
                    .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))		      
                    .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))		      
                    .ForMember(dest => dest.IdTipoDocumentoPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))		      
                    .ForMember(dest => dest.FirmanteMismaPersona, source => source.MapFrom(p => p.firmante_misma_persona))		      
                    .ForMember(dest => dest.NumeroDocumento, source => source.MapFrom(p => p.Nro_Documento));

                cfg.CreateMap<CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas, ConsultaPadronTitularesSolicitudPersonasJuridicasPersonasFisicasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_titular_pj, source => source.MapFrom(p => p.IdTitularPersonaJuridica))		      
                    .ForMember(dest => dest.id_cpadron, source => source.MapFrom(p => p.IdConsultaPadron))		      
                    .ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))		      
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocumentoPersonal))		      
                    .ForMember(dest => dest.firmante_misma_persona, source => source.MapFrom(p => p.FirmanteMismaPersona))		      
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NumeroDocumento));
            });
            mapperBasePersonaFisicaJuridica = configPJ.CreateMapper();
        }
		
        public IEnumerable<ConsultaPadronTitularesSolicitudPersonasJuridicasDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronTitularesSolicitudPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<CPadron_Titulares_Solicitud_PersonasJuridicas>, IEnumerable<ConsultaPadronTitularesSolicitudPersonasJuridicasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public ConsultaPadronTitularesSolicitudPersonasJuridicasDTO Single(int IdPersonaJuridica )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronTitularesSolicitudPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdPersonaJuridica);
                var entityDto = mapperBase.Map<CPadron_Titulares_Solicitud_PersonasJuridicas, ConsultaPadronTitularesSolicitudPersonasJuridicasDTO>(entity);
     
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
		public IEnumerable<ConsultaPadronTitularesSolicitudPersonasJuridicasDTO> GetByFKIdConsultaPadron(int IdConsultaPadron)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new ConsultaPadronTitularesSolicitudPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdConsultaPadron(IdConsultaPadron);
            var elementsDto = mapperBase.Map<IEnumerable<CPadron_Titulares_Solicitud_PersonasJuridicas>, IEnumerable<ConsultaPadronTitularesSolicitudPersonasJuridicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoSociedad"></param>
		/// <returns></returns>	
		public IEnumerable<ConsultaPadronTitularesSolicitudPersonasJuridicasDTO> GetByFKIdTipoSociedad(int IdTipoSociedad)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new ConsultaPadronTitularesSolicitudPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTipoSociedad(IdTipoSociedad);
            var elementsDto = mapperBase.Map<IEnumerable<CPadron_Titulares_Solicitud_PersonasJuridicas>, IEnumerable<ConsultaPadronTitularesSolicitudPersonasJuridicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoiibb"></param>
		/// <returns></returns>	
		public IEnumerable<ConsultaPadronTitularesSolicitudPersonasJuridicasDTO> GetByFKIdTipoiibb(int IdTipoiibb)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new ConsultaPadronTitularesSolicitudPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTipoiibb(IdTipoiibb);
            var elementsDto = mapperBase.Map<IEnumerable<CPadron_Titulares_Solicitud_PersonasJuridicas>, IEnumerable<ConsultaPadronTitularesSolicitudPersonasJuridicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdLocalidad"></param>
		/// <returns></returns>	
		public IEnumerable<ConsultaPadronTitularesSolicitudPersonasJuridicasDTO> GetByFKIdLocalidad(int IdLocalidad)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new ConsultaPadronTitularesSolicitudPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdLocalidad(IdLocalidad);
            var elementsDto = mapperBase.Map<IEnumerable<CPadron_Titulares_Solicitud_PersonasJuridicas>, IEnumerable<ConsultaPadronTitularesSolicitudPersonasJuridicasDTO>>(elements);
            return elementsDto;				
		}
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(ConsultaPadronTitularesSolicitudPersonasJuridicasDTO objectDto)
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
		            repo = new ConsultaPadronTitularesSolicitudPersonasJuridicasRepository(unitOfWork);

		            var elementDto = mapperBase.Map<ConsultaPadronTitularesSolicitudPersonasJuridicasDTO, CPadron_Titulares_Solicitud_PersonasJuridicas>(objectDto);
                    
                    repo.Insert(elementDto);

                    repoPersonaFisicaJuridica = new ConsultaPadronTitularesSolicitudPersonasJuridicasPersonasFisicasRepository(unitOfWork);

                    if (elementDto.Id_TipoSociedad == (int)Constantes.TipoSociedad.Sociedad_Hecho ||
                        elementDto.Id_TipoSociedad == (int)Constantes.TipoSociedad.Sociedad_no_constituidas_regularmente)
                    {
                        foreach (var item in objectDto.titularesSH)
                        {
                            ConsultaPadronTitularesSolicitudPersonasJuridicasPersonasFisicasDTO encoTitPerJurPerFis = new ConsultaPadronTitularesSolicitudPersonasJuridicasPersonasFisicasDTO();

                            encoTitPerJurPerFis.IdConsultaPadron = objectDto.IdConsultaPadron;
                            encoTitPerJurPerFis.IdPersonaJuridica = elementDto.id_personajuridica;
                            encoTitPerJurPerFis.Apellido = item.Apellidos;
                            encoTitPerJurPerFis.Nombres = item.Nombres;
                            encoTitPerJurPerFis.IdTipoDocumentoPersonal = item.IdTipoDocPersonal;
                            encoTitPerJurPerFis.NumeroDocumento = item.NroDoc;
                            encoTitPerJurPerFis.Email = item.Email;

                            var elementDtoTitPerJurInsert = mapperBasePersonaFisicaJuridica.Map<ConsultaPadronTitularesSolicitudPersonasJuridicasPersonasFisicasDTO, CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas>(encoTitPerJurPerFis);
                            repoPersonaFisicaJuridica.Insert(elementDtoTitPerJurInsert);
                        }
                    }
                    
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
		public void Update(ConsultaPadronTitularesSolicitudPersonasJuridicasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new ConsultaPadronTitularesSolicitudPersonasJuridicasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<ConsultaPadronTitularesSolicitudPersonasJuridicasDTO, CPadron_Titulares_Solicitud_PersonasJuridicas>(objectDTO);                   
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
		public void Delete(ConsultaPadronTitularesSolicitudPersonasJuridicasDTO objectDto)
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
                    repo = new ConsultaPadronTitularesSolicitudPersonasJuridicasRepository(unitOfWork);
                    var elementDto = mapperBase.Map<ConsultaPadronTitularesSolicitudPersonasJuridicasDTO, CPadron_Titulares_Solicitud_PersonasJuridicas>(objectDto);

                    repoPersonaFisicaJuridica = new ConsultaPadronTitularesSolicitudPersonasJuridicasPersonasFisicasRepository(unitOfWork);

                    var personafisicajuridica = repoPersonaFisicaJuridica.GetByFKIdConsultaPadron(objectDto.IdConsultaPadron);

                    foreach (var item in personafisicajuridica)
                        repoPersonaFisicaJuridica.Delete(item);

                    repo.Delete(elementDto);

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
					repo = new ConsultaPadronTitularesSolicitudPersonasJuridicasRepository(unitOfWork);                    
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
		public void DeleteByFKIdTipoSociedad(int IdTipoSociedad)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new ConsultaPadronTitularesSolicitudPersonasJuridicasRepository(unitOfWork);                    
					var elements = repo.GetByFKIdTipoSociedad(IdTipoSociedad);
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
					repo = new ConsultaPadronTitularesSolicitudPersonasJuridicasRepository(unitOfWork);                    
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
					repo = new ConsultaPadronTitularesSolicitudPersonasJuridicasRepository(unitOfWork);                    
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
        public IEnumerable<ConsultaPadronTitularesSolicitudPersonasJuridicasDTO> GetByIdConsultaPadronCuitIdPersonaJuridica(int IdSolicitud, string Cuit, int IdPersonaFisica)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronTitularesSolicitudPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.GetByIdConsultaPadronCuitIdPersonaJuridica(IdSolicitud, Cuit, IdPersonaFisica);
                var entityDto = mapperBase.Map<IEnumerable<CPadron_Titulares_Solicitud_PersonasJuridicas>, IEnumerable<ConsultaPadronTitularesSolicitudPersonasJuridicasDTO>>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

