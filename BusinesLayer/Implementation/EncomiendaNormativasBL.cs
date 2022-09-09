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
	public class EncomiendaNormativasBL : IEncomiendaNormativasBL<EncomiendaNormativasDTO>
    {               
		private EncomiendaNormativasRepository repo = null;
        private EncomiendaRepository repoEncomienda = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
         
        public EncomiendaNormativasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EncomiendaNormativasDTO, Encomienda_Normativas>().ReverseMap()
                    .ForMember(dest => dest.IdEncomiendaNormativa, source => source.MapFrom(p => p.id_encomiendatiponormativa))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.IdTipoNormativa, source => source.MapFrom(p => p.id_tiponormativa))
                    .ForMember(dest => dest.IdEntidadNormativa, source => source.MapFrom(p => p.id_entidadnormativa))
                    .ForMember(dest => dest.NroNormativa, source => source.MapFrom(p => p.nro_normativa))
                    .ForMember(dest => dest.TipoNormativaDTO, source => source.MapFrom(p => p.TipoNormativa))
                    .ForMember(dest => dest.EntidadNormativaDTO, source => source.MapFrom(p => p.EntidadNormativa));

                cfg.CreateMap<Encomienda_Normativas, EncomiendaNormativasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_encomiendatiponormativa, source => source.MapFrom(p => p.IdEncomiendaNormativa))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.id_tiponormativa, source => source.MapFrom(p => p.IdTipoNormativa))
                    .ForMember(dest => dest.id_entidadnormativa, source => source.MapFrom(p => p.IdEntidadNormativa))
                    .ForMember(dest => dest.nro_normativa, source => source.MapFrom(p => p.NroNormativa))
                    .ForMember(dest => dest.TipoNormativa, source => source.MapFrom(p => p.TipoNormativaDTO))
                    .ForMember(dest => dest.EntidadNormativa, source => source.MapFrom(p => p.EntidadNormativaDTO));


                cfg.CreateMap<EntidadNormativa, EntidadNormativaDTO>();
                cfg.CreateMap<EntidadNormativaDTO, EntidadNormativa>();

                cfg.CreateMap<TipoNormativa, TipoNormativaDTO>();
                cfg.CreateMap<TipoNormativaDTO, TipoNormativa>();
            });
            mapperBase = config.CreateMapper();

        }
		
     
		public EncomiendaNormativasDTO Single(int IdEncomiendaNormativa )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaNormativasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdEncomiendaNormativa);
                var entityDto = mapperBase.Map<Encomienda_Normativas, EncomiendaNormativasDTO>(entity);
     
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
        public IEnumerable<EncomiendaNormativasDTO> GetNormativas(int IdEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaNormativasRepository(this.uowF.GetUnitOfWork());

                var elements = repo.GetNormativas(IdEncomienda);
                var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Normativas>, IEnumerable<EncomiendaNormativasDTO>>(elements);
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
		/// <param name="IdEncomienda"></param>
		/// <returns></returns>	
		public IEnumerable<EncomiendaNormativasDTO> GetByFKIdEncomienda(int IdEncomienda)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaNormativasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdEncomienda(IdEncomienda);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Normativas>, IEnumerable<EncomiendaNormativasDTO>>(elements);
            return elementsDto;				
		}
		
		#region Métodos de actualizacion insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(EncomiendaNormativasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new EncomiendaNormativasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<EncomiendaNormativasDTO, Encomienda_Normativas>(objectDto);                   
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
        #region Métodos de actualizacion Update
        /// <summary>
        /// Modifica la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public void Update(EncomiendaNormativasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new EncomiendaNormativasRepository(unitOfWork);
                    repoEncomienda = new EncomiendaRepository(unitOfWork);
                    var encomienda = repoEncomienda.Single(objectDTO.IdEncomienda);
                    if (encomienda.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && encomienda.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
                        throw new Exception(Errors.ENCOMIENDA_CAMBIOS);

                    var elementDTO = mapperBase.Map<EncomiendaNormativasDTO, Encomienda_Normativas>(objectDTO);
                    if (repo.GetByFKIdEncomienda(objectDTO.IdEncomienda).Any())
                    { repo.Update(elementDTO); }
                    else
                    {
                        elementDTO.CreateDate = DateTime.Now;
                        repo.Insert(elementDTO); 
                    }

                    
		            unitOfWork.Commit();           
		        }
		    }
		    catch (Exception ex)
		    {
		        throw ex;
		    }
		}
		

		#endregion
		#region Métodos de actualizacion Delete
		/// <summary>
		/// elimina la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>      
        public void Delete(EncomiendaNormativasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new EncomiendaNormativasRepository(unitOfWork);
                    repoEncomienda = new EncomiendaRepository(unitOfWork);
                    var encomienda = repoEncomienda.Single(objectDTO.IdEncomienda);
                    if (encomienda.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && encomienda.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
                        throw new Exception(Errors.ENCOMIENDA_CAMBIOS);

                    var elementDto = mapperBase.Map<EncomiendaNormativasDTO, Encomienda_Normativas>(objectDTO);                   
		            
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

