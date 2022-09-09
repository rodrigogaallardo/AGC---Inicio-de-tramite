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
	public class ConsultaPadronTitularesPersonasFisicasBL : IConsultaPadronTitularesPersonasFisicasBL<ConsultaPadronTitularesPersonasFisicasDTO>
    {               
		private ConsultaPadronTitularesPersonasFisicasRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public ConsultaPadronTitularesPersonasFisicasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ConsultaPadronTitularesPersonasFisicasDTO, CPadron_Titulares_PersonasFisicas>().ReverseMap()
                    .ForMember(dest => dest.IdPersonaFisica, source => source.MapFrom(p => p.id_personafisica))
                    .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                    .ForMember(dest => dest.IdTipoDocumentoPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.IdTipoiibb, source => source.MapFrom(p => p.id_tipoiibb))
                    .ForMember(dest => dest.IngresosBrutos, source => source.MapFrom(p => p.Ingresos_Brutos))
                    .ForMember(dest => dest.NumeroPuerta, source => source.MapFrom(p => p.Nro_Puerta))
                    .ForMember(dest => dest.IdLocalidad, source => source.MapFrom(p => p.Id_Localidad))
                    .ForMember(dest => dest.CodigoPostal, source => source.MapFrom(p => p.Codigo_Postal))
                    .ForMember(dest => dest.NumeroDocumento, source => source.MapFrom(p => p.Nro_Documento));

                cfg.CreateMap<CPadron_Titulares_PersonasFisicas, ConsultaPadronTitularesPersonasFisicasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_personafisica, source => source.MapFrom(p => p.IdPersonaFisica))
                    .ForMember(dest => dest.id_cpadron, source => source.MapFrom(p => p.IdConsultaPadron))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocumentoPersonal))
                    .ForMember(dest => dest.id_tipoiibb, source => source.MapFrom(p => p.IdTipoiibb))
                    .ForMember(dest => dest.Ingresos_Brutos, source => source.MapFrom(p => p.IngresosBrutos))
                    .ForMember(dest => dest.Nro_Puerta, source => source.MapFrom(p => p.NumeroPuerta))
                    .ForMember(dest => dest.Id_Localidad, source => source.MapFrom(p => p.IdLocalidad))
                    .ForMember(dest => dest.Codigo_Postal, source => source.MapFrom(p => p.CodigoPostal))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NumeroDocumento));
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<ConsultaPadronTitularesPersonasFisicasDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<CPadron_Titulares_PersonasFisicas>, IEnumerable<ConsultaPadronTitularesPersonasFisicasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public ConsultaPadronTitularesPersonasFisicasDTO Single(int IdPersonaFisica )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdPersonaFisica);
                var entityDto = mapperBase.Map<CPadron_Titulares_PersonasFisicas, ConsultaPadronTitularesPersonasFisicasDTO>(entity);
     
                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ConsultaPadronTitularesPersonasFisicasDTO> GetByFKIdConsultaPadron(int IdConsultaPadron)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.GetByFKIdConsultaPadron(IdConsultaPadron);
                var entityDto = mapperBase.Map<IEnumerable<CPadron_Titulares_PersonasFisicas>, IEnumerable<ConsultaPadronTitularesPersonasFisicasDTO>>(entity);

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
		public bool Insert(ConsultaPadronTitularesPersonasFisicasDTO objectDto)
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
		            repo = new ConsultaPadronTitularesPersonasFisicasRepository(unitOfWork);
                    if (objectDto.IdPersonaFisica > 0)
                    {
                        var deleteObject = new ConsultaPadronTitularesPersonasFisicasDTO()
                        {
                            IdPersonaFisica = objectDto.IdPersonaFisica
                        };
                        var deleteObjectDTO = mapperBase.Map<ConsultaPadronTitularesPersonasFisicasDTO, CPadron_Titulares_PersonasFisicas>(deleteObject);
                        repo.Delete(deleteObjectDTO); 

                    }

		            var elementDto = mapperBase.Map<ConsultaPadronTitularesPersonasFisicasDTO, CPadron_Titulares_PersonasFisicas>(objectDto);                   
		            repo.Insert(elementDto);
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
		public void Update(ConsultaPadronTitularesPersonasFisicasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {
                    ConsultaPadronSolicitudesBL consultaPadronSolicitudesBL = new ConsultaPadronSolicitudesBL();
                    var consultaPadronDTO = consultaPadronSolicitudesBL.Single(objectDTO.IdConsultaPadron);

                    if (consultaPadronDTO.IdEstado != (int)Constantes.ConsultaPadronEstados.COMP && consultaPadronDTO.IdEstado != (int)Constantes.ConsultaPadronEstados.INCOM && consultaPadronDTO.IdEstado != (int)Constantes.ConsultaPadronEstados.PING)
                        throw new Exception(Errors.SSIT_CPADRON_NO_ADMITE_CAMBIOS);

		            repo = new ConsultaPadronTitularesPersonasFisicasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<ConsultaPadronTitularesPersonasFisicasDTO, CPadron_Titulares_PersonasFisicas>(objectDTO);                   
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
		public void Delete(ConsultaPadronTitularesPersonasFisicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new ConsultaPadronTitularesPersonasFisicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<ConsultaPadronTitularesPersonasFisicasDTO, CPadron_Titulares_PersonasFisicas>(objectDto);                   
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <param name="Cuit"></param>
        /// <param name="IdPersonaFisica"></param>
        /// <returns></returns>
        public IEnumerable<ConsultaPadronTitularesPersonasFisicasDTO> GetByIdConsultaPadronCuitIdPersonaFisica(int IdSolicitud, string Cuit, int IdPersonaFisica)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByIdConsultaPadronCuitIdPersonaFisica(IdSolicitud, Cuit, IdPersonaFisica);
                var elementsDto = mapperBase.Map<IEnumerable<CPadron_Titulares_PersonasFisicas>, IEnumerable<ConsultaPadronTitularesPersonasFisicasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

