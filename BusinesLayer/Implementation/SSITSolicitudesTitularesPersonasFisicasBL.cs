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
	public class SSITSolicitudesTitularesPersonasFisicasBL : ISSITSolicitudesTitularesPersonasFisicasBL<SSITSolicitudesTitularesPersonasFisicasDTO>
    {
        private SSITSolicitudesTitularesPersonasFisicasRepository repo = null;
        private SSITSolicitudesFirmantesPersonasFisicasRepository repoFir = null;       
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
        IMapper mapperFirmantes;

        public SSITSolicitudesTitularesPersonasFisicasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SSITSolicitudesTitularesPersonasFisicasDTO, SSIT_Solicitudes_Titulares_PersonasFisicas>().ReverseMap()
                    .ForMember(dest => dest.IdPersonaFisica, source => source.MapFrom(p => p.id_personafisica))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdTipodocPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.NroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.IdTipoiibb, source => source.MapFrom(p => p.id_tipoiibb))
                    .ForMember(dest => dest.IngresosBrutos, source => source.MapFrom(p => p.Ingresos_Brutos))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.Nro_Puerta))
                    .ForMember(dest => dest.IdLocalidad, source => source.MapFrom(p => p.Id_Localidad))
                    .ForMember(dest => dest.CodigoPostal, source => source.MapFrom(p => p.Codigo_Postal));
            });
            mapperBase = config.CreateMapper();

            var configFir = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SSITSolicitudesFirmantesPersonasFisicasDTO, SSIT_Solicitudes_Firmantes_PersonasFisicas>().ReverseMap()
                    .ForMember(dest => dest.IdFirmantePf, source => source.MapFrom(p => p.id_firmante_pf))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdPersonaFisica, source => source.MapFrom(p => p.id_personafisica))
                    .ForMember(dest => dest.IdTipoDocPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.NroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.IdTipoCaracter, source => source.MapFrom(p => p.id_tipocaracter));

            });
            mapperFirmantes = configFir.CreateMapper();
        }
		
     
		public SSITSolicitudesTitularesPersonasFisicasDTO Single(int IdPersonaFisica )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdPersonaFisica);
                var entityDto = mapperBase.Map<SSIT_Solicitudes_Titulares_PersonasFisicas, SSITSolicitudesTitularesPersonasFisicasDTO>(entity);
     
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
		public IEnumerable<SSITSolicitudesTitularesPersonasFisicasDTO> GetByFKIdSolicitud(int IdSolicitud)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdSolicitud(IdSolicitud);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Titulares_PersonasFisicas>, IEnumerable<SSITSolicitudesTitularesPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_solicitud"></param>
        /// <param name="Cuit"></param>
        /// <param name="IdPersonaFisica"></param>
        /// <returns></returns>
        public IEnumerable<SSITSolicitudesTitularesPersonasFisicasDTO> GetByIdSolicitudCuitIdPersonaFisica(int id_solicitud, string Cuit, int IdPersonaFisica)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByIdSolicitudCuitIdPersonaFisica(id_solicitud, Cuit, IdPersonaFisica);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Titulares_PersonasFisicas>, IEnumerable<SSITSolicitudesTitularesPersonasFisicasDTO>>(elements);
            return elementsDto;
        }
        public IEnumerable<SSITSolicitudesTitularesPersonasFisicasDTO> GetByIdSolicitudIdPersonaFisica(int id_solicitud, int IdPersonaFisica)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByIdSolicitudIdPersonaFisica(id_solicitud, IdPersonaFisica);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Titulares_PersonasFisicas>, IEnumerable<SSITSolicitudesTitularesPersonasFisicasDTO>>(elements);
            return elementsDto;
        }

        public string GetApellidoyNombres(int id_solicitud)
        {
            string result = "";

            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdSolicitud(id_solicitud);

            result = string.Join(" / ", elements.Select(s => s.Apellido + "," + s.Nombres).ToArray());

            return result;
        }

		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
        public bool Insert(SSITSolicitudesTitularesPersonasFisicasDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    SSITSolicitudesBL solBL = new SSITSolicitudesBL();
                    var solicitudDTO = solBL.Single(objectDto.IdSolicitud);
                    if (solicitudDTO.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && solicitudDTO.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM
                        && solicitudDTO.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF && solicitudDTO.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO)
                        throw new Exception(Errors.SSIT_SOLICITUD_NO_CAMBIOS);

                    repo = new SSITSolicitudesTitularesPersonasFisicasRepository(unitOfWork);
                    repoFir = new SSITSolicitudesFirmantesPersonasFisicasRepository(unitOfWork);

                    var elementDto = mapperBase.Map<SSITSolicitudesTitularesPersonasFisicasDTO, SSIT_Solicitudes_Titulares_PersonasFisicas>(objectDto);
                    var elementDtoFirmantes = mapperFirmantes.Map<SSITSolicitudesFirmantesPersonasFisicasDTO, SSIT_Solicitudes_Firmantes_PersonasFisicas>(objectDto.DtoFirmantes);

                    var insertOk = repo.Insert(elementDto);
                    elementDtoFirmantes.id_personafisica = elementDto.id_personafisica;
                    var insertOkFir = repoFir.Insert(elementDtoFirmantes);

                    unitOfWork.Commit();
                    objectDto.IdPersonaFisica = elementDto.id_personafisica;
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
		public void Update(SSITSolicitudesTitularesPersonasFisicasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SSITSolicitudesTitularesPersonasFisicasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<SSITSolicitudesTitularesPersonasFisicasDTO, SSIT_Solicitudes_Titulares_PersonasFisicas>(objectDTO);                   
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
		public void Delete(SSITSolicitudesTitularesPersonasFisicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new SSITSolicitudesTitularesPersonasFisicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<SSITSolicitudesTitularesPersonasFisicasDTO, SSIT_Solicitudes_Titulares_PersonasFisicas>(objectDto);                   
		            var insertOk = repo.Delete(elementDto);
		            unitOfWork.Commit();
		        }
		    }
		    catch
		    {
		        throw;
		    }
		}
		#endregion
    }
}

