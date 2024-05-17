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
	public class TransferenciasTitularesPersonasJuridicasBL : ITransferenciasTitularesPersonasJuridicasBL<TransferenciasTitularesPersonasJuridicasDTO>
    {               
		private TransferenciasTitularesPersonasJuridicasRepository repo = null;        
        TransferenciasTitularesPersonasJuridicasPersonasFisicasRepository repoTitPJPF = null;
        TransferenciasFirmantesPersonasJuridicasRepository repoFirPJ = null;

        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
        IMapper mapperFirPJ;
        IMapper mapperTitPJPF;
        
        public TransferenciasTitularesPersonasJuridicasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TransferenciasTitularesPersonasJuridicasDTO, Transf_Titulares_PersonasJuridicas>().ReverseMap()
                    .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdTipoSociedad, source => source.MapFrom(p => p.Id_TipoSociedad))
                    .ForMember(dest => dest.RazonSocial, source => source.MapFrom(p => p.Razon_Social))
                    .ForMember(dest => dest.Cuit, source => source.MapFrom(p => p.CUIT))
                    .ForMember(dest => dest.IdTipoiibb, source => source.MapFrom(p => p.id_tipoiibb))
                    .ForMember(dest => dest.NumeroIibb, source => source.MapFrom(p => p.Nro_IIBB))
                    .ForMember(dest => dest.NumeroPuerta, source => source.MapFrom(p => p.NroPuerta))
                    .ForMember(dest => dest.IdLocalidad, source => source.MapFrom(p => p.id_localidad))
                    .ForMember(dest => dest.CodigoPostal, source => source.MapFrom(p => p.Codigo_Postal));

                cfg.CreateMap<Transf_Titulares_PersonasJuridicas, TransferenciasTitularesPersonasJuridicasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.Id_TipoSociedad, source => source.MapFrom(p => p.IdTipoSociedad))
                    .ForMember(dest => dest.Razon_Social, source => source.MapFrom(p => p.RazonSocial))
                    .ForMember(dest => dest.CUIT, source => source.MapFrom(p => p.Cuit))
                    .ForMember(dest => dest.id_tipoiibb, source => source.MapFrom(p => p.IdTipoiibb))
                    .ForMember(dest => dest.Nro_IIBB, source => source.MapFrom(p => p.NumeroIibb))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NumeroPuerta))
                    .ForMember(dest => dest.id_localidad, source => source.MapFrom(p => p.IdLocalidad))
                    .ForMember(dest => dest.Codigo_Postal, source => source.MapFrom(p => p.CodigoPostal));

            });
            mapperBase = config.CreateMapper();

            var configFirPJ = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TransferenciasFirmantesPersonasJuridicasDTO, Transf_Firmantes_PersonasJuridicas>().ReverseMap()
                    .ForMember(dest => dest.IdFirmantePersonaJuridica, source => source.MapFrom(p => p.id_firmante_pj))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                    .ForMember(dest => dest.IdTipoDocumentoPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.NumeroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.IdTipoCaracter, source => source.MapFrom(p => p.id_tipocaracter))
                    .ForMember(dest => dest.CargoFirmantePersonaJuridica, source => source.MapFrom(p => p.cargo_firmante_pj))
                    .ForMember(dest => dest.Cuit, source => source.MapFrom(p => p.Cuit));

                cfg.CreateMap<Transf_Firmantes_PersonasJuridicas, TransferenciasFirmantesPersonasJuridicasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_firmante_pj, source => source.MapFrom(p => p.IdFirmantePersonaJuridica))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocumentoPersonal))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NumeroDocumento))
                    .ForMember(dest => dest.id_tipocaracter, source => source.MapFrom(p => p.IdTipoCaracter))
                    .ForMember(dest => dest.cargo_firmante_pj, source => source.MapFrom(p => p.CargoFirmantePersonaJuridica))
                    .ForMember(dest => dest.Cuit, source => source.MapFrom(p => p.Cuit));
            });
            mapperFirPJ = configFirPJ.CreateMapper();

            var configPJPF = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TransferenciasTitularesPersonasJuridicasPersonasFisicasDTO, Transf_Titulares_PersonasJuridicas_PersonasFisicas>().ReverseMap()
                    .ForMember(dest => dest.IdTitularPersonaJuridica, source => source.MapFrom(p => p.id_titular_pj))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                    .ForMember(dest => dest.IdTipoDocumentoPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.NumeroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.IdFirmantePersonaJuridica, source => source.MapFrom(p => p.id_firmante_pj))
                    .ForMember(dest => dest.FirmanteMismaPersona, source => source.MapFrom(p => p.firmante_misma_persona));

                cfg.CreateMap<Transf_Titulares_PersonasJuridicas_PersonasFisicas, TransferenciasTitularesPersonasJuridicasPersonasFisicasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_titular_pj, source => source.MapFrom(p => p.IdTitularPersonaJuridica))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocumentoPersonal))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NumeroDocumento))
                    .ForMember(dest => dest.id_firmante_pj, source => source.MapFrom(p => p.IdFirmantePersonaJuridica))
                    .ForMember(dest => dest.firmante_misma_persona, source => source.MapFrom(p => p.FirmanteMismaPersona));
            });
            mapperTitPJPF = configPJPF.CreateMapper();
        }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
        public IEnumerable<TransferenciasTitularesPersonasJuridicasDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_PersonasJuridicas>, IEnumerable<TransferenciasTitularesPersonasJuridicasDTO>>(elements);
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
        /// <param name="IdPersonaJuridica"></param>
        /// <returns></returns>
		public TransferenciasTitularesPersonasJuridicasDTO Single(int IdPersonaJuridica )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdPersonaJuridica);
                var entityDto = mapperBase.Map<Transf_Titulares_PersonasJuridicas, TransferenciasTitularesPersonasJuridicasDTO>(entity);
     
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
        public IEnumerable<TransferenciasTitularesPersonasJuridicasDTO> GetByIdSolicitudIdPersonaJuridica(int IdSolicitud, int IdPersonaJuridica)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.GetByIdSolicitudIdPersonaJuridica(IdSolicitud, IdPersonaJuridica);
                var entityDto = mapperBase.Map<IEnumerable<Transf_Titulares_PersonasJuridicas>,IEnumerable<TransferenciasTitularesPersonasJuridicasDTO>>(entity);

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
        /// <param name="Cuit"></param>
        /// <param name="IdPersonaJuridica"></param>
        /// <returns></returns>
        public IEnumerable<TransferenciasTitularesPersonasJuridicasDTO> GetByIdTransferenciasCuitIdPersonaJuridica(int IdSolicitud, string Cuit, int IdPersonaJuridica)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.GetByIdTransferenciasCuitIdPersonaJuridica(IdSolicitud, Cuit, IdPersonaJuridica);
                var entityDto = mapperBase.Map<IEnumerable<Transf_Titulares_PersonasJuridicas>,IEnumerable<TransferenciasTitularesPersonasJuridicasDTO>>(entity);

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
		public IEnumerable<TransferenciasTitularesPersonasJuridicasDTO> GetByFKIdSolicitud(int IdSolicitud)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdSolicitud(IdSolicitud);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_PersonasJuridicas>, IEnumerable<TransferenciasTitularesPersonasJuridicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoiibb"></param>
		/// <returns></returns>	
		public IEnumerable<TransferenciasTitularesPersonasJuridicasDTO> GetByFKIdTipoiibb(int IdTipoiibb)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTipoiibb(IdTipoiibb);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_PersonasJuridicas>, IEnumerable<TransferenciasTitularesPersonasJuridicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdLocalidad"></param>
		/// <returns></returns>	
		public IEnumerable<TransferenciasTitularesPersonasJuridicasDTO> GetByFKIdLocalidad(int IdLocalidad)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdLocalidad(IdLocalidad);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_PersonasJuridicas>, IEnumerable<TransferenciasTitularesPersonasJuridicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="CreateUser"></param>
		/// <returns></returns>	
		public IEnumerable<TransferenciasTitularesPersonasJuridicasDTO> GetByFKCreateUser(Guid CreateUser)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKCreateUser(CreateUser);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_PersonasJuridicas>, IEnumerable<TransferenciasTitularesPersonasJuridicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="LastUpdateUser"></param>
		/// <returns></returns>	
		public IEnumerable<TransferenciasTitularesPersonasJuridicasDTO> GetByFKLastUpdateUser(Guid LastUpdateUser)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKLastUpdateUser(LastUpdateUser);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_PersonasJuridicas>, IEnumerable<TransferenciasTitularesPersonasJuridicasDTO>>(elements);
            return elementsDto;				
		}
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(TransferenciasTitularesPersonasJuridicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {
                    //TransferenciasSolicitudesBL transferenciasSolicitudesBL = new TransferenciasSolicitudesBL();
                    //var consultaPadronDTO = transferenciasSolicitudesBL.Single(objectDto.IdSolicitud);
                    //if (consultaPadronDTO.IdEstado != (int)Constantes.ConsultaPadronEstados.COMP && 
                    //    consultaPadronDTO.IdEstado != (int)Constantes.ConsultaPadronEstados.INCOM &&
                    //    consultaPadronDTO.IdEstado != (int)Constantes.ConsultaPadronEstados.OBSERVADO &&
                    //    consultaPadronDTO.IdEstado != (int)Constantes.ConsultaPadronEstados.PING)
                    //    throw new Exception(Errors.SSIT_CPADRON_NO_ADMITE_CAMBIOS);

                    repo = new TransferenciasTitularesPersonasJuridicasRepository(unitOfWork);
                    repoTitPJPF = new TransferenciasTitularesPersonasJuridicasPersonasFisicasRepository(unitOfWork);
                    repoFirPJ = new TransferenciasFirmantesPersonasJuridicasRepository(unitOfWork);

                    if (objectDto.IdPersonaJuridica > 0)
                    {
                        
                        
                        var encTitPerJurPerFisDTO = repoTitPJPF.GetByIdSolicitudIdPersonaJuridica(objectDto.IdSolicitud, objectDto.IdPersonaJuridica);
                        foreach (var item in encTitPerJurPerFisDTO)
                        {
                            repoTitPJPF.Delete(item);                            
                        }
                        
                        var encFirPerJurDTO = repoFirPJ.GetByFKIdSolicitudIdPersonaJuridica(objectDto.IdSolicitud, objectDto.IdPersonaJuridica);
                        foreach (var item in encFirPerJurDTO)
                            repoFirPJ.Delete(item); 

                        var encTitPerJurDTO = repo.GetByIdSolicitudIdPersonaJuridica(objectDto.IdSolicitud, objectDto.IdPersonaJuridica);
                        foreach (var item in encTitPerJurDTO)
                            repo.Delete(item);
                                                      
                    }

                    var elementDto = mapperBase.Map<TransferenciasTitularesPersonasJuridicasDTO, Transf_Titulares_PersonasJuridicas>(objectDto);

                    var insertOk = repo.Insert(elementDto);

                    if (elementDto.Id_TipoSociedad == (int)Constantes.TipoSociedad.Sociedad_Hecho ||
                        elementDto.Id_TipoSociedad == (int)Constantes.TipoSociedad.Sociedad_no_constituidas_regularmente)
                    {
                        foreach (var itemFirPj in objectDto.firmantesSH)
                        {
                            TransferenciasFirmantesPersonasJuridicasDTO encFirPerJur = new TransferenciasFirmantesPersonasJuridicasDTO();
                            encFirPerJur.IdSolicitud = elementDto.id_solicitud;
                            encFirPerJur.IdPersonaJuridica = elementDto.id_personajuridica;
                            encFirPerJur.Apellido = itemFirPj.Apellidos;
                            encFirPerJur.Nombres = itemFirPj.Nombres;
                            encFirPerJur.IdTipoDocumentoPersonal = itemFirPj.id_tipodoc_personal;
                            encFirPerJur.NumeroDocumento = itemFirPj.NroDoc;
                            encFirPerJur.Email = itemFirPj.email;
                            encFirPerJur.IdTipoCaracter = itemFirPj.id_tipocaracter;
                            encFirPerJur.CargoFirmantePersonaJuridica = itemFirPj.cargo_firmante;
                            encFirPerJur.Cuit = itemFirPj.Cuit;
                            var elementDtoFirPerJurInsert = mapperFirPJ.Map<TransferenciasFirmantesPersonasJuridicasDTO, Transf_Firmantes_PersonasJuridicas>(encFirPerJur);
                            repoFirPJ.Insert(elementDtoFirPerJurInsert);

                            foreach (var itemTitPjSH in objectDto.titularesSH)
                            {
                                if ((Guid)itemTitPjSH.RowId == (Guid)itemFirPj.rowid_titular)
                                {
                                    TransferenciasTitularesPersonasJuridicasPersonasFisicasDTO encoTitPerJurPerFis = new TransferenciasTitularesPersonasJuridicasPersonasFisicasDTO();
                                    encoTitPerJurPerFis.IdSolicitud = elementDto.id_solicitud;
                                    encoTitPerJurPerFis.IdPersonaJuridica = elementDto.id_personajuridica;
                                    encoTitPerJurPerFis.IdFirmantePersonaJuridica = elementDtoFirPerJurInsert.id_firmante_pj;
                                    encoTitPerJurPerFis.Apellido = itemTitPjSH.Apellidos;
                                    encoTitPerJurPerFis.Nombres = itemTitPjSH.Nombres;
                                    encoTitPerJurPerFis.IdTipoDocumentoPersonal = itemTitPjSH.IdTipoDocPersonal;
                                    encoTitPerJurPerFis.NumeroDocumento = itemTitPjSH.NroDoc;
                                    encoTitPerJurPerFis.Email = itemTitPjSH.Email;
                                    encoTitPerJurPerFis.FirmanteMismaPersona = itemFirPj.misma_persona.Value;
                                    var elementDtoTitPerJurInsert = mapperTitPJPF.Map<TransferenciasTitularesPersonasJuridicasPersonasFisicasDTO, Transf_Titulares_PersonasJuridicas_PersonasFisicas>(encoTitPerJurPerFis);
                                    repoTitPJPF.Insert(elementDtoTitPerJurInsert);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (var item in objectDto.encFirDTO)
                        {
                            TransferenciasFirmantesPersonasJuridicasDTO encFirPerJurInsert = new TransferenciasFirmantesPersonasJuridicasDTO();
                            encFirPerJurInsert.IdSolicitud = elementDto.id_solicitud;
                            encFirPerJurInsert.IdPersonaJuridica = elementDto.id_personajuridica;
                            encFirPerJurInsert.Apellido = item.Apellido;
                            encFirPerJurInsert.Nombres = item.Nombres;
                            encFirPerJurInsert.IdTipoDocumentoPersonal = item.IdTipoDocumentoPersonal;
                            encFirPerJurInsert.NumeroDocumento = item.NumeroDocumento;
                            encFirPerJurInsert.Email = item.Email;
                            encFirPerJurInsert.IdTipoCaracter = item.IdTipoCaracter;
                            encFirPerJurInsert.CargoFirmantePersonaJuridica = item.CargoFirmantePersonaJuridica;
                            encFirPerJurInsert.Cuit = item.Cuit;
                            var elementDtoFirPerJurInsert = mapperFirPJ.Map<TransferenciasFirmantesPersonasJuridicasDTO, Transf_Firmantes_PersonasJuridicas>(encFirPerJurInsert);
                            repoFirPJ.Insert(elementDtoFirPerJurInsert);
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
		public void Update(TransferenciasTitularesPersonasJuridicasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TransferenciasTitularesPersonasJuridicasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<TransferenciasTitularesPersonasJuridicasDTO, Transf_Titulares_PersonasJuridicas>(objectDTO);                   
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
		public void Delete(TransferenciasTitularesPersonasJuridicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TransferenciasTitularesPersonasJuridicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<TransferenciasTitularesPersonasJuridicasDTO, Transf_Titulares_PersonasJuridicas>(objectDto);                   
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
					repo = new TransferenciasTitularesPersonasJuridicasRepository(unitOfWork);                    
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
		public void DeleteByFKIdTipoiibb(int IdTipoiibb)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new TransferenciasTitularesPersonasJuridicasRepository(unitOfWork);                    
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
					repo = new TransferenciasTitularesPersonasJuridicasRepository(unitOfWork);                    
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
					repo = new TransferenciasTitularesPersonasJuridicasRepository(unitOfWork);                    
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
					repo = new TransferenciasTitularesPersonasJuridicasRepository(unitOfWork);                    
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

