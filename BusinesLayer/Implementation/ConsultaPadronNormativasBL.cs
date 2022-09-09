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
using DataAcess.EntityCustom;

namespace BusinesLayer.Implementation
{
	public class ConsultaPadronNormativasBL : IConsultaPadronNormativasBL<ConsultaPadronNormativasDTO>
    {               
		private ConsultaPadronNormativasRepository repo = null;
        private ConsultaPadronSolicitudesRepository repoConsultaPadron = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
           
        public ConsultaPadronNormativasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CPadron_Normativas, ConsultaPadronNormativasDTO>()
                    .ForMember(dest => dest.IdConsultaPadronTipoNormativa, source => source.MapFrom(p => p.id_cpadrontiponormativa))
                    .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                    .ForMember(dest => dest.IdTipoNormativa, source => source.MapFrom(p => p.id_tiponormativa))
                    .ForMember(dest => dest.IdEntidadNormativa, source => source.MapFrom(p => p.id_entidadnormativa))
                    .ForMember(dest => dest.NumeroNormativa, source => source.MapFrom(p => p.nro_normativa))
                    .ForMember(dest => dest.TipoNormativa, source => source.MapFrom(p => p.TipoNormativa))
                    .ForMember(dest => dest.EntidadNormativa, source => source.MapFrom(p => p.EntidadNormativa));

                cfg.CreateMap<EntidadNormativa, EntidadNormativaDTO>();
                cfg.CreateMap<EntidadNormativaDTO, EntidadNormativa>();

                cfg.CreateMap<TipoNormativa, TipoNormativaDTO>();
                cfg.CreateMap<TipoNormativaDTO, TipoNormativa>();

                cfg.CreateMap<ConsultaPadronNormativasDTO, CPadron_Normativas>()
                    .ForMember(dest => dest.id_cpadrontiponormativa, source => source.MapFrom(p => p.IdConsultaPadronTipoNormativa))
                    .ForMember(dest => dest.id_cpadron, source => source.MapFrom(p => p.IdConsultaPadron))
                    .ForMember(dest => dest.id_tiponormativa, source => source.MapFrom(p => p.IdTipoNormativa))
                    .ForMember(dest => dest.id_entidadnormativa, source => source.MapFrom(p => p.IdEntidadNormativa))
                    .ForMember(dest => dest.nro_normativa, source => source.MapFrom(p => p.NumeroNormativa))
                    .ForMember(dest => dest.TipoNormativa, source => source.MapFrom(p => p.TipoNormativa))
                    .ForMember(dest => dest.EntidadNormativa, source => source.MapFrom(p => p.EntidadNormativa));
            });
            mapperBase = config.CreateMapper();

        }
		public ConsultaPadronNormativasDTO Single(int IdConsultaPadronTipoNormativa )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronNormativasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdConsultaPadronTipoNormativa);
                var entityDto = mapperBase.Map<CPadron_Normativas, ConsultaPadronNormativasDTO>(entity);
     
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
		public IEnumerable<ConsultaPadronNormativasDTO> GetByFKIdConsultaPadron(int IdConsultaPadron)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new ConsultaPadronNormativasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdConsultaPadron(IdConsultaPadron);
            var elementsDto = mapperBase.Map<IEnumerable<CPadron_Normativas>, IEnumerable<ConsultaPadronNormativasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoNormativa"></param>
		/// <returns></returns>	
		public IEnumerable<ConsultaPadronNormativasDTO> GetByFKIdTipoNormativa(int IdTipoNormativa)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new ConsultaPadronNormativasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTipoNormativa(IdTipoNormativa);
            var elementsDto = mapperBase.Map<IEnumerable<CPadron_Normativas>, IEnumerable<ConsultaPadronNormativasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdEntidadNormativa"></param>
		/// <returns></returns>	
		public IEnumerable<ConsultaPadronNormativasDTO> GetByFKIdEntidadNormativa(int IdEntidadNormativa)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new ConsultaPadronNormativasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdEntidadNormativa(IdEntidadNormativa);
            var elementsDto = mapperBase.Map<IEnumerable<CPadron_Normativas>, IEnumerable<ConsultaPadronNormativasDTO>>(elements);
            return elementsDto;				
		}
		
		
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(ConsultaPadronNormativasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new ConsultaPadronNormativasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<ConsultaPadronNormativasDTO, CPadron_Normativas>(objectDto);                   
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
		public void Update(ConsultaPadronNormativasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {
                    repoConsultaPadron = new ConsultaPadronSolicitudesRepository(unitOfWork);
                    repo = new ConsultaPadronNormativasRepository(unitOfWork); 

                    var consultaPadron = repoConsultaPadron.Single(objectDTO.IdConsultaPadron);
                    if (consultaPadron.id_estado != (int)Constantes.ConsultaPadronEstados.COMP 
                        && consultaPadron.id_estado != (int)Constantes.ConsultaPadronEstados.INCOM 
                        && consultaPadron.id_estado != (int)Constantes.ConsultaPadronEstados.PING)
                        throw new Exception(Errors.SSIT_CPADRON_NO_ADMITE_CAMBIOS);

                    var elementDTO = mapperBase.Map<ConsultaPadronNormativasDTO, CPadron_Normativas>(objectDTO);
                    
                    if (repo.GetByFKIdConsultaPadron(objectDTO.IdConsultaPadron).Any()){ 
                        repo.Update(elementDTO); 
                    }
                    else{
                        elementDTO.CreateDate = DateTime.Now;
                        repo.Insert(elementDTO);
                    }
		            
		            unitOfWork.Commit();           
		        }
		    }
		    catch 
		    {
		        throw;
		    }
		}
		

		#endregion
		#region Métodos de actualizacion e insert
		/// <summary>
		/// elimina la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>      
		public void Delete(ConsultaPadronNormativasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {
                    repoConsultaPadron = new ConsultaPadronSolicitudesRepository(unitOfWork);

                    var consultaPadron = repoConsultaPadron.Single(objectDto.IdConsultaPadron);
                    if (consultaPadron.id_estado != (int)Constantes.ConsultaPadronEstados.COMP && consultaPadron.id_estado != (int)Constantes.ConsultaPadronEstados.INCOM && consultaPadron.id_estado != (int)Constantes.ConsultaPadronEstados.PING)
                        throw new Exception(Errors.SSIT_CPADRON_NO_ADMITE_CAMBIOS);

		            repo = new ConsultaPadronNormativasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<ConsultaPadronNormativasDTO, CPadron_Normativas>(objectDto);                   
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
        /// <returns></returns>
        public IEnumerable<ConsultaPadronNormativasDTO> GetNormativas(int IdSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronNormativasRepository(this.uowF.GetUnitOfWork());
                var objectDto =  repo.GetNormativas(IdSolicitud);
                var elementDto = mapperBase.Map<IEnumerable<CPadron_Normativas>, IEnumerable<ConsultaPadronNormativasDTO>>(objectDto);

                return elementDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

