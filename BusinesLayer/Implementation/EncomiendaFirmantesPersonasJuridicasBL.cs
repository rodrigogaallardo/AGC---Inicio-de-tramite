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
	public class EncomiendaFirmantesPersonasJuridicasBL : IEncomiendaFirmantesPersonasJuridicasBL<EncomiendaFirmantesPersonasJuridicasDTO>
    {               
		private EncomiendaFirmantesPersonasJuridicasRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public EncomiendaFirmantesPersonasJuridicasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EncomiendaFirmantesPersonasJuridicasDTO, Encomienda_Firmantes_PersonasJuridicas>().ReverseMap()
                    .ForMember(dest => dest.IdFirmantePj, source => source.MapFrom(p => p.id_firmante_pj))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                    .ForMember(dest => dest.IdTipoDocPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.NroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.IdTipoCaracter, source => source.MapFrom(p => p.id_tipocaracter))
                    .ForMember(dest => dest.CargoFirmantePj, source => source.MapFrom(p => p.cargo_firmante_pj));

                cfg.CreateMap<Encomienda_Firmantes_PersonasJuridicas, EncomiendaFirmantesPersonasJuridicasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_firmante_pj, source => source.MapFrom(p => p.IdFirmantePj))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocPersonal))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NroDocumento))
                    .ForMember(dest => dest.id_tipocaracter, source => source.MapFrom(p => p.IdTipoCaracter))
                    .ForMember(dest => dest.cargo_firmante_pj, source => source.MapFrom(p => p.CargoFirmantePj));
            });
            mapperBase = config.CreateMapper();
        }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
        public IEnumerable<EncomiendaFirmantesPersonasJuridicasDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaFirmantesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Firmantes_PersonasJuridicas>, IEnumerable<EncomiendaFirmantesPersonasJuridicasDTO>>(elements);
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
	    /// <param name="IdFirmantePj"></param>
	    /// <returns></returns>
		public EncomiendaFirmantesPersonasJuridicasDTO Single(int IdFirmantePj )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaFirmantesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdFirmantePj);
                var entityDto = mapperBase.Map<Encomienda_Firmantes_PersonasJuridicas, EncomiendaFirmantesPersonasJuridicasDTO>(entity);
     
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
		public IEnumerable<EncomiendaFirmantesPersonasJuridicasDTO> GetByFKIdEncomienda(int IdEncomienda)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaFirmantesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdEncomienda(IdEncomienda);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Firmantes_PersonasJuridicas>, IEnumerable<EncomiendaFirmantesPersonasJuridicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdPersonaJuridica"></param>
		/// <returns></returns>	
		public IEnumerable<EncomiendaFirmantesPersonasJuridicasDTO> GetByFKIdPersonaJuridica(int IdPersonaJuridica)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaFirmantesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdPersonaJuridica(IdPersonaJuridica);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Firmantes_PersonasJuridicas>, IEnumerable<EncomiendaFirmantesPersonasJuridicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoDocPersonal"></param>
		/// <returns></returns>	
		public IEnumerable<EncomiendaFirmantesPersonasJuridicasDTO> GetByFKIdTipoDocPersonal(int IdTipoDocPersonal)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaFirmantesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTipoDocPersonal(IdTipoDocPersonal);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Firmantes_PersonasJuridicas>, IEnumerable<EncomiendaFirmantesPersonasJuridicasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoCaracter"></param>
		/// <returns></returns>	
        public IEnumerable<EncomiendaFirmantesPersonasJuridicasDTO> GetByFKIdTipoCaracter(int IdTipoCaracter)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaFirmantesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdTipoCaracter(IdTipoCaracter);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Firmantes_PersonasJuridicas>, IEnumerable<EncomiendaFirmantesPersonasJuridicasDTO>>(elements);
            return elementsDto;
        }

        public string[] GetCargoFirmantesPersonasJuridicas()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaFirmantesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                return repo.GetCargoFirmantesPersonasJuridicas();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public IEnumerable<EncomiendaFirmantesPersonasJuridicasDTO> GetByIdEncomiendaIdPersonaJuridica(int id_encomienda, int IdPersonaJuridica)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaFirmantesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByIdEncomiendaIdPersonaJuridica(id_encomienda, IdPersonaJuridica);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Firmantes_PersonasJuridicas>, IEnumerable<EncomiendaFirmantesPersonasJuridicasDTO>>(elements);
            return elementsDto;
        }

		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(EncomiendaFirmantesPersonasJuridicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new EncomiendaFirmantesPersonasJuridicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<EncomiendaFirmantesPersonasJuridicasDTO, Encomienda_Firmantes_PersonasJuridicas>(objectDto);                   
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
		public void Update(EncomiendaFirmantesPersonasJuridicasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new EncomiendaFirmantesPersonasJuridicasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<EncomiendaFirmantesPersonasJuridicasDTO, Encomienda_Firmantes_PersonasJuridicas>(objectDTO);                   
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
		public void Delete(EncomiendaFirmantesPersonasJuridicasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
		            repo = new EncomiendaFirmantesPersonasJuridicasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<EncomiendaFirmantesPersonasJuridicasDTO, Encomienda_Firmantes_PersonasJuridicas>(objectDto);                   
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

