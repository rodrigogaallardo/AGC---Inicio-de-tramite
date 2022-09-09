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
	public class TipoTramiteBL : ITipoTramiteBL<TipoTramiteDTO>
    {               
		private TipoTramiteRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public TipoTramiteBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TipoTramiteDTO, TipoTramite>().ReverseMap()
                    .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.id_tipotramite))
                    .ForMember(dest => dest.CodTipoTramite, source => source.MapFrom(p => p.cod_tipotramite))
                    .ForMember(dest => dest.DescripcionTipoTramite, source => source.MapFrom(p => p.descripcion_tipotramite))
                    .ForMember(dest => dest.CodTipoTramiteWs, source => source.MapFrom(p => p.cod_tipotramite_ws));

                cfg.CreateMap<TipoTramite, TipoTramiteDTO>().ReverseMap()
                    .ForMember(dest => dest.id_tipotramite, source => source.MapFrom(p => p.IdTipoTramite))
                    .ForMember(dest => dest.cod_tipotramite, source => source.MapFrom(p => p.CodTipoTramite))
                    .ForMember(dest => dest.descripcion_tipotramite, source => source.MapFrom(p => p.DescripcionTipoTramite))
                    .ForMember(dest => dest.cod_tipotramite_ws, source => source.MapFrom(p => p.CodTipoTramiteWs));
            });
            mapperBase = config.CreateMapper();
        }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
        public IEnumerable<TipoTramiteDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TipoTramiteRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<TipoTramite>, IEnumerable<TipoTramiteDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TipoTramiteDTO> getExcluirIdTipoTramite(List<int> lstIdTipoTramite)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TipoTramiteRepository(this.uowF.GetUnitOfWork());
                var elements = repo.getExcluirIdTipoTramite(lstIdTipoTramite);
                var elementsDto = mapperBase.Map<IEnumerable<TipoTramite>, IEnumerable<TipoTramiteDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	

		public TipoTramiteDTO Single(int IdTipoTramite )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TipoTramiteRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdTipoTramite);
                var entityDto = mapperBase.Map<TipoTramite, TipoTramiteDTO>(entity);
     
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
		public bool Insert(TipoTramiteDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TipoTramiteRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<TipoTramiteDTO, TipoTramite>(objectDto);                   
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
		public void Update(TipoTramiteDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TipoTramiteRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<TipoTramiteDTO, TipoTramite>(objectDTO);                   
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
		public void Delete(TipoTramiteDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TipoTramiteRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<TipoTramiteDTO, TipoTramite>(objectDto);                   
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

