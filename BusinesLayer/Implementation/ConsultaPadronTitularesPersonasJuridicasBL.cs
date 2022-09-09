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
	public class ConsultaPadronTitularesPersonasJuridicasBL : IConsultaPadronTitularesPersonasJuridicasBL<ConsultaPadronTitularesPersonasJuridicasDTO>
    {               
		private ConsultaPadronTitularesPersonasJuridicasRepository repo = null;
        private ConsultaPadronTitularesPersonasJuridicasPersonasFisicasRepository repoPersonaFisicaJuridica = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
        IMapper mapperBasePersonaFisicaJuridica; 
		         
        public ConsultaPadronTitularesPersonasJuridicasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ConsultaPadronTitularesPersonasJuridicasDTO, CPadron_Titulares_PersonasJuridicas>().ReverseMap()
                    .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                    .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                    .ForMember(dest => dest.IdTipoSociedad, source => source.MapFrom(p => p.Id_TipoSociedad))
                    .ForMember(dest => dest.RazonSocial, source => source.MapFrom(p => p.Razon_Social))
                    .ForMember(dest => dest.IdTipoiibb, source => source.MapFrom(p => p.id_tipoiibb))
                    .ForMember(dest => dest.NumeroroIibb, source => source.MapFrom(p => p.Nro_IIBB))
                    .ForMember(dest => dest.NumeroPuerta, source => source.MapFrom(p => p.NroPuerta))
                    .ForMember(dest => dest.IdLocalidad, source => source.MapFrom(p => p.id_localidad))
                    .ForMember(dest => dest.CodigoPostal, source => source.MapFrom(p => p.Codigo_Postal));

                cfg.CreateMap<CPadron_Titulares_PersonasJuridicas, ConsultaPadronTitularesPersonasJuridicasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))
                    .ForMember(dest => dest.id_cpadron, source => source.MapFrom(p => p.IdConsultaPadron))
                    .ForMember(dest => dest.Id_TipoSociedad, source => source.MapFrom(p => p.IdTipoSociedad))
                    .ForMember(dest => dest.Razon_Social, source => source.MapFrom(p => p.RazonSocial))
                    .ForMember(dest => dest.id_tipoiibb, source => source.MapFrom(p => p.IdTipoiibb))
                    .ForMember(dest => dest.Nro_IIBB, source => source.MapFrom(p => p.NumeroroIibb))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NumeroPuerta))
                    .ForMember(dest => dest.id_localidad, source => source.MapFrom(p => p.IdLocalidad))
                    .ForMember(dest => dest.Codigo_Postal, source => source.MapFrom(p => p.CodigoPostal));
            });
            mapperBase = config.CreateMapper();

            var configPFJ = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ConsultaPadronTitularesPersonasJuridicasPersonasFisicasDTO, CPadron_Titulares_PersonasJuridicas_PersonasFisicas>().ReverseMap()
                    .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                    .ForMember(dest => dest.IdTitularPersonaJuridica, source => source.MapFrom(p => p.id_titular_pj))
                    .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                    .ForMember(dest => dest.IdTipoDocumentoPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.FirmanteMismaPersona, source => source.MapFrom(p => p.firmante_misma_persona))
                    .ForMember(dest => dest.NumeroDocumento, source => source.MapFrom(p => p.Nro_Documento));

                cfg.CreateMap<CPadron_Titulares_PersonasJuridicas_PersonasFisicas, ConsultaPadronTitularesPersonasJuridicasPersonasFisicasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_cpadron, source => source.MapFrom(p => p.IdConsultaPadron))
                    .ForMember(dest => dest.id_titular_pj, source => source.MapFrom(p => p.IdTitularPersonaJuridica))
                    .ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocumentoPersonal))
                    .ForMember(dest => dest.firmante_misma_persona, source => source.MapFrom(p => p.FirmanteMismaPersona))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NumeroDocumento));
            });
            mapperBasePersonaFisicaJuridica = configPFJ.CreateMapper();
        }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
        public IEnumerable<ConsultaPadronTitularesPersonasJuridicasDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<CPadron_Titulares_PersonasJuridicas>, IEnumerable<ConsultaPadronTitularesPersonasJuridicasDTO>>(elements);
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
        /// <param name="IdPersonaJuridica"></param>
        /// <returns></returns>
        public IEnumerable<ConsultaPadronTitularesPersonasJuridicasDTO> GetByIdConsultaPadronCuitIdPersonaJuridica(int IdSolicitud, string Cuit, int IdPersonaJuridica)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByIdConsultaPadronCuitIdPersonaJuridica(IdSolicitud,Cuit,IdPersonaJuridica);
                var elementsDto = mapperBase.Map<IEnumerable<CPadron_Titulares_PersonasJuridicas>, IEnumerable<ConsultaPadronTitularesPersonasJuridicasDTO>>(elements);
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
		public ConsultaPadronTitularesPersonasJuridicasDTO Single(int IdPersonaJuridica )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdPersonaJuridica);
                var entityDto = mapperBase.Map<CPadron_Titulares_PersonasJuridicas, ConsultaPadronTitularesPersonasJuridicasDTO>(entity);
     
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
        public IEnumerable<ConsultaPadronTitularesPersonasJuridicasDTO> GetByFKIdConsultaPadron(int IdConsultaPadron)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.GetByFKIdConsultaPadron(IdConsultaPadron);
                var entityDto = mapperBase.Map<IEnumerable<CPadron_Titulares_PersonasJuridicas>, IEnumerable<ConsultaPadronTitularesPersonasJuridicasDTO>>(entity);
     
                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(ConsultaPadronTitularesPersonasJuridicasDTO objectDto)
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
		            repo = new ConsultaPadronTitularesPersonasJuridicasRepository(unitOfWork);
                    var elementDto = mapperBase.Map<ConsultaPadronTitularesPersonasJuridicasDTO, CPadron_Titulares_PersonasJuridicas>(objectDto);                   
		            repo.Insert(elementDto);
                    
                    repoPersonaFisicaJuridica = new ConsultaPadronTitularesPersonasJuridicasPersonasFisicasRepository(unitOfWork);

                    if (elementDto.Id_TipoSociedad == (int)Constantes.TipoSociedad.Sociedad_Hecho ||
                        elementDto.Id_TipoSociedad == (int)Constantes.TipoSociedad.Sociedad_no_constituidas_regularmente)
                    {
                        foreach (var item in objectDto.titularesSH)
                        {
                            ConsultaPadronTitularesPersonasJuridicasPersonasFisicasDTO encoTitPerJurPerFis = new ConsultaPadronTitularesPersonasJuridicasPersonasFisicasDTO();

                            encoTitPerJurPerFis.IdConsultaPadron = objectDto.IdConsultaPadron;
                            encoTitPerJurPerFis.IdPersonaJuridica = elementDto.id_personajuridica;
                            encoTitPerJurPerFis.Apellido = item.Apellidos;
                            encoTitPerJurPerFis.Nombres = item.Nombres;
                            encoTitPerJurPerFis.IdTipoDocumentoPersonal = item.IdTipoDocPersonal;
                            encoTitPerJurPerFis.NumeroDocumento = item.NroDoc;
                            encoTitPerJurPerFis.Email = item.Email;

                            var elementDtoTitPerJurInsert = mapperBasePersonaFisicaJuridica.Map<ConsultaPadronTitularesPersonasJuridicasPersonasFisicasDTO, CPadron_Titulares_PersonasJuridicas_PersonasFisicas>(encoTitPerJurPerFis);
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
		public void Update(ConsultaPadronTitularesPersonasJuridicasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new ConsultaPadronTitularesPersonasJuridicasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<ConsultaPadronTitularesPersonasJuridicasDTO, CPadron_Titulares_PersonasJuridicas>(objectDTO);                   
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
		public void Delete(ConsultaPadronTitularesPersonasJuridicasDTO objectDto)
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
		            repo = new ConsultaPadronTitularesPersonasJuridicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<ConsultaPadronTitularesPersonasJuridicasDTO, CPadron_Titulares_PersonasJuridicas>(objectDto);
                                        
                    repoPersonaFisicaJuridica = new ConsultaPadronTitularesPersonasJuridicasPersonasFisicasRepository(unitOfWork);
                    
                    var personafisicajuridica = repoPersonaFisicaJuridica.GetByFKIdConsultaPadron(objectDto.IdConsultaPadron); 
                    
                    foreach(var item in personafisicajuridica)                                            
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
		
		

		#endregion
    }
}

