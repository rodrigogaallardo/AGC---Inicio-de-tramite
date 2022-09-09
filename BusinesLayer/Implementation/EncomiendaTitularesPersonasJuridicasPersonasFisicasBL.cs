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
	public class EncomiendaTitularesPersonasJuridicasPersonasFisicasBL : IEncomiendaTitularesPersonasJuridicasPersonasFisicasBL<EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO>
    {               
		private EncomiendaTitularesPersonasJuridicasPersonasFisicasRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public EncomiendaTitularesPersonasJuridicasPersonasFisicasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO, Encomienda_Titulares_PersonasJuridicas_PersonasFisicas>().ReverseMap()
                    .ForMember(dest => dest.IdTitularPj, source => source.MapFrom(p => p.id_titular_pj))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                    .ForMember(dest => dest.IdTipoDocPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.NroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.IdFirmantePj, source => source.MapFrom(p => p.id_firmante_pj))
                    .ForMember(dest => dest.FirmanteMismaPersona, source => source.MapFrom(p => p.firmante_misma_persona));

                cfg.CreateMap<Encomienda_Titulares_PersonasJuridicas_PersonasFisicas, EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_titular_pj, source => source.MapFrom(p => p.IdTitularPj))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocPersonal))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NroDocumento))
                    .ForMember(dest => dest.id_firmante_pj, source => source.MapFrom(p => p.IdFirmantePj))
                    .ForMember(dest => dest.firmante_misma_persona, source => source.MapFrom(p => p.FirmanteMismaPersona));
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaTitularesPersonasJuridicasPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Titulares_PersonasJuridicas_PersonasFisicas>, IEnumerable<EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO Single(int IdTitularPj )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaTitularesPersonasJuridicasPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdTitularPj);
                var entityDto = mapperBase.Map<Encomienda_Titulares_PersonasJuridicas_PersonasFisicas, EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO>(entity);
     
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
		public IEnumerable<EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO> GetByFKIdEncomienda(int IdEncomienda)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaTitularesPersonasJuridicasPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdEncomienda(IdEncomienda);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Titulares_PersonasJuridicas_PersonasFisicas>, IEnumerable<EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdPersonaJuridica"></param>
		/// <returns></returns>	
		public IEnumerable<EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO> GetByFKIdPersonaJuridica(int IdPersonaJuridica)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaTitularesPersonasJuridicasPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdPersonaJuridica(IdPersonaJuridica);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Titulares_PersonasJuridicas_PersonasFisicas>, IEnumerable<EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoDocPersonal"></param>
		/// <returns></returns>	
		public IEnumerable<EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO> GetByFKIdTipoDocPersonal(int IdTipoDocPersonal)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaTitularesPersonasJuridicasPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTipoDocPersonal(IdTipoDocPersonal);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Titulares_PersonasJuridicas_PersonasFisicas>, IEnumerable<EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdFirmantePj"></param>
		/// <returns></returns>	
		public IEnumerable<EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO> GetByFKIdFirmantePj(int IdFirmantePj)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaTitularesPersonasJuridicasPersonasFisicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdFirmantePj(IdFirmantePj);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Titulares_PersonasJuridicas_PersonasFisicas>, IEnumerable<EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO>>(elements);
            return elementsDto;				
		}

        public IEnumerable<EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO> GetByIdEncomiendaIdPersonaJuridica(int id_encomienda, int IdPersonaJuridica)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaTitularesPersonasJuridicasPersonasFisicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByIdEncomiendaIdPersonaJuridica(id_encomienda, IdPersonaJuridica);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Titulares_PersonasJuridicas_PersonasFisicas>, IEnumerable<EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO>>(elements);
            return elementsDto;
        }

		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO objectDto)
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

		            repo = new EncomiendaTitularesPersonasJuridicasPersonasFisicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO, Encomienda_Titulares_PersonasJuridicas_PersonasFisicas>(objectDto);                   
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
		public void Update(EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new EncomiendaTitularesPersonasJuridicasPersonasFisicasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO, Encomienda_Titulares_PersonasJuridicas_PersonasFisicas>(objectDTO);                   
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
		public void Delete(EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new EncomiendaTitularesPersonasJuridicasPersonasFisicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO, Encomienda_Titulares_PersonasJuridicas_PersonasFisicas>(objectDto);                   
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

