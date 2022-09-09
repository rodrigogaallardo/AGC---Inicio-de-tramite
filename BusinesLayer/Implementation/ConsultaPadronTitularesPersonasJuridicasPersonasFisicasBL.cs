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

namespace BusinesLayer.Implementation
{
	public class ConsultaPadronTitularesPersonasJuridicasPersonasFisicasBL : IConsultaPadronTitularesPersonasJuridicasPersonasFisicasBL<ConsultaPadronTitularesPersonasJuridicasPersonasFisicasDTO>
    {               
		private ConsultaPadronTitularesPersonasJuridicasPersonasFisicasRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public ConsultaPadronTitularesPersonasJuridicasPersonasFisicasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {		
                cfg.CreateMap<ConsultaPadronTitularesPersonasJuridicasPersonasFisicasDTO, CPadron_Titulares_PersonasJuridicas_PersonasFisicas>().ReverseMap()
                    .ForMember(dest => dest.IdTitularPersonaJuridica, source => source.MapFrom(p => p.id_titular_pj))
                    .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                    .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                    .ForMember(dest => dest.IdTipoDocumentoPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.FirmanteMismaPersona, source => source.MapFrom(p => p.firmante_misma_persona))
                    .ForMember(dest => dest.NumeroDocumento, source => source.MapFrom(p => p.Nro_Documento));

                cfg.CreateMap<CPadron_Titulares_PersonasJuridicas_PersonasFisicas, ConsultaPadronTitularesPersonasJuridicasPersonasFisicasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_titular_pj, source => source.MapFrom(p => p.IdTitularPersonaJuridica))
                    .ForMember(dest => dest.id_cpadron, source => source.MapFrom(p => p.IdConsultaPadron))
                    .ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocumentoPersonal))
                    .ForMember(dest => dest.firmante_misma_persona, source => source.MapFrom(p => p.FirmanteMismaPersona))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NumeroDocumento));
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<ConsultaPadronTitularesPersonasJuridicasPersonasFisicasDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronTitularesPersonasJuridicasPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<CPadron_Titulares_PersonasJuridicas_PersonasFisicas>, IEnumerable<ConsultaPadronTitularesPersonasJuridicasPersonasFisicasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public ConsultaPadronTitularesPersonasJuridicasPersonasFisicasDTO Single(int IdTitularPersonaJuridica )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronTitularesPersonasJuridicasPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdTitularPersonaJuridica);
                var entityDto = mapperBase.Map<CPadron_Titulares_PersonasJuridicas_PersonasFisicas, ConsultaPadronTitularesPersonasJuridicasPersonasFisicasDTO>(entity);
     
                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ConsultaPadronTitularesPersonasJuridicasPersonasFisicasDTO> GetByFKIdConsultaPadron(int IdConsultaPadron)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronTitularesPersonasJuridicasPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.GetByFKIdConsultaPadron(IdConsultaPadron);
                var entityDto = mapperBase.Map < IEnumerable<CPadron_Titulares_PersonasJuridicas_PersonasFisicas>, IEnumerable<ConsultaPadronTitularesPersonasJuridicasPersonasFisicasDTO>>(entity);
     
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
		public bool Insert(ConsultaPadronTitularesPersonasJuridicasPersonasFisicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new ConsultaPadronTitularesPersonasJuridicasPersonasFisicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<ConsultaPadronTitularesPersonasJuridicasPersonasFisicasDTO, CPadron_Titulares_PersonasJuridicas_PersonasFisicas>(objectDto);                   
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
		public void Update(ConsultaPadronTitularesPersonasJuridicasPersonasFisicasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new ConsultaPadronTitularesPersonasJuridicasPersonasFisicasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<ConsultaPadronTitularesPersonasJuridicasPersonasFisicasDTO, CPadron_Titulares_PersonasJuridicas_PersonasFisicas>(objectDTO);                   
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
		public void Delete(ConsultaPadronTitularesPersonasJuridicasPersonasFisicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new ConsultaPadronTitularesPersonasJuridicasPersonasFisicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<ConsultaPadronTitularesPersonasJuridicasPersonasFisicasDTO, CPadron_Titulares_PersonasJuridicas_PersonasFisicas>(objectDto);                   
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

