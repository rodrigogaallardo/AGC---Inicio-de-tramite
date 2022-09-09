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
	public class EncomiendaFirmantesPersonasFisicasBL : IEncomiendaFirmantesPersonasFisicasBL<EncomiendaFirmantesPersonasFisicasDTO>
    {               
		private EncomiendaFirmantesPersonasFisicasRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public EncomiendaFirmantesPersonasFisicasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EncomiendaFirmantesPersonasFisicasDTO, Encomienda_Firmantes_PersonasFisicas>().ReverseMap()
                    .ForMember(dest => dest.IdFirmantePf, source => source.MapFrom(p => p.id_firmante_pf))                  
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))                  
                    .ForMember(dest => dest.IdPersonaFisica, source => source.MapFrom(p => p.id_personafisica))                  
                    .ForMember(dest => dest.IdTipodocPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))                  
                    .ForMember(dest => dest.NroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.IdTipoCaracter, source => source.MapFrom(p => p.id_tipocaracter))
                    .ForMember(dest => dest.TipoDocumentoPersonalDTO, source => source.MapFrom(p => p.TipoDocumentoPersonal))
                    .ForMember(dest => dest.TiposDeCaracterLegalDTO, source => source.MapFrom(p => p.TiposDeCaracterLegal));

                cfg.CreateMap<Encomienda_Firmantes_PersonasFisicas, EncomiendaFirmantesPersonasFisicasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_firmante_pf, source => source.MapFrom(p => p.IdFirmantePf))                  
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))                  
                    .ForMember(dest => dest.id_personafisica, source => source.MapFrom(p => p.IdPersonaFisica))                  
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipodocPersonal))                  
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NroDocumento))                  
                    .ForMember(dest => dest.id_tipocaracter, source => source.MapFrom(p => p.IdTipoCaracter));

                cfg.CreateMap<TipoDocumentoPersonalDTO, TipoDocumentoPersonal>().ReverseMap();

                cfg.CreateMap<TiposDeCaracterLegalDTO, TiposDeCaracterLegal>().ReverseMap()
                    .ForMember(dest => dest.IdTipoCaracter, source => source.MapFrom(p => p.id_tipocaracter))
                    .ForMember(dest => dest.CodTipoCaracter, source => source.MapFrom(p => p.cod_tipocaracter))
                    .ForMember(dest => dest.NomTipoCaracter, source => source.MapFrom(p => p.nom_tipocaracter))
                    .ForMember(dest => dest.DisponibilidadTipoCaracter, source => source.MapFrom(p => p.disponibilidad_tipocaracter))
                    .ForMember(dest => dest.MuestraCargoTipoCaracter, source => source.MapFrom(p => p.muestracargo_tipocaracter));
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<EncomiendaFirmantesPersonasFisicasDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaFirmantesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Firmantes_PersonasFisicas>, IEnumerable<EncomiendaFirmantesPersonasFisicasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public EncomiendaFirmantesPersonasFisicasDTO Single(int IdFirmantePf )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaFirmantesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdFirmantePf);
                var entityDto = mapperBase.Map<Encomienda_Firmantes_PersonasFisicas, EncomiendaFirmantesPersonasFisicasDTO>(entity);
     
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
		/// <param name="IdEncomienda"></param>
		/// <returns></returns>	
		public IEnumerable<EncomiendaFirmantesPersonasFisicasDTO> GetByFKIdEncomienda(int IdEncomienda)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaFirmantesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdEncomienda(IdEncomienda);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Firmantes_PersonasFisicas>, IEnumerable<EncomiendaFirmantesPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdPersonaFisica"></param>
		/// <returns></returns>	
		public IEnumerable<EncomiendaFirmantesPersonasFisicasDTO> GetByFKIdPersonaFisica(int IdPersonaFisica)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaFirmantesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdPersonaFisica(IdPersonaFisica);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Firmantes_PersonasFisicas>, IEnumerable<EncomiendaFirmantesPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipodocPersonal"></param>
		/// <returns></returns>	
		public IEnumerable<EncomiendaFirmantesPersonasFisicasDTO> GetByFKIdTipodocPersonal(int IdTipodocPersonal)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaFirmantesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTipodocPersonal(IdTipodocPersonal);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Firmantes_PersonasFisicas>, IEnumerable<EncomiendaFirmantesPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoCaracter"></param>
		/// <returns></returns>	
		public IEnumerable<EncomiendaFirmantesPersonasFisicasDTO> GetByFKIdTipoCaracter(int IdTipoCaracter)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaFirmantesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTipoCaracter(IdTipoCaracter);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Firmantes_PersonasFisicas>, IEnumerable<EncomiendaFirmantesPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}

        public IEnumerable<EncomiendaFirmantesPersonasFisicasDTO> GetByIdEncomiendaIdPersonaFisica(int id_encomienda, int IdPersonaFisica)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaFirmantesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByIdEncomiendaIdPersonaFisica(id_encomienda, IdPersonaFisica);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Firmantes_PersonasFisicas>, IEnumerable<EncomiendaFirmantesPersonasFisicasDTO>>(elements);
            return elementsDto;
        }

		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(EncomiendaFirmantesPersonasFisicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    EncomiendaBL encomiendaBL = new EncomiendaBL();
                    var encomiendaDTO = encomiendaBL.Single(objectDto.IdEncomienda);
                    if (encomiendaDTO.IdEstado != (int)Constantes.Encomienda_Estados.Completa && encomiendaDTO.IdEstado != (int)Constantes.Encomienda_Estados.Incompleta
                        && encomiendaDTO.IdEstado != (int)Constantes.Encomienda_Estados.Confirmada)
                        throw new Exception(Errors.ENCOMIENDA_CAMBIOS);
     
		            repo = new EncomiendaFirmantesPersonasFisicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<EncomiendaFirmantesPersonasFisicasDTO, Encomienda_Firmantes_PersonasFisicas>(objectDto);                   
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
		public void Update(EncomiendaFirmantesPersonasFisicasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new EncomiendaFirmantesPersonasFisicasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<EncomiendaFirmantesPersonasFisicasDTO, Encomienda_Firmantes_PersonasFisicas>(objectDTO);                   
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
		public void Delete(EncomiendaFirmantesPersonasFisicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new EncomiendaFirmantesPersonasFisicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<EncomiendaFirmantesPersonasFisicasDTO, Encomienda_Firmantes_PersonasFisicas>(objectDto);                   
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

