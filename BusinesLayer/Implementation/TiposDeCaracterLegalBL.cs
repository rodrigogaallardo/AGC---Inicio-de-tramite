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
	public class TiposDeCaracterLegalBL : ITiposDeCaracterLegalBL<TiposDeCaracterLegalDTO>
    {               
		private TiposDeCaracterLegalRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public TiposDeCaracterLegalBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TiposDeCaracterLegalDTO, TiposDeCaracterLegal>().ReverseMap()
                    .ForMember(dest => dest.IdTipoCaracter, source => source.MapFrom(p => p.id_tipocaracter))
                    .ForMember(dest => dest.CodTipoCaracter, source => source.MapFrom(p => p.cod_tipocaracter))
                    .ForMember(dest => dest.NomTipoCaracter, source => source.MapFrom(p => p.nom_tipocaracter))
                    .ForMember(dest => dest.DisponibilidadTipoCaracter, source => source.MapFrom(p => p.disponibilidad_tipocaracter))
                    .ForMember(dest => dest.MuestraCargoTipoCaracter, source => source.MapFrom(p => p.muestracargo_tipocaracter));
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<TiposDeCaracterLegalDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TiposDeCaracterLegalRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<TiposDeCaracterLegal>, IEnumerable<TiposDeCaracterLegalDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<TiposDeCaracterLegalDTO> GetByDisponibilidad(int[] disponibilidad)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TiposDeCaracterLegalRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByDisponibilidad(disponibilidad);
                var elementsDto = mapperBase.Map<IEnumerable<TiposDeCaracterLegal>, IEnumerable<TiposDeCaracterLegalDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public TiposDeCaracterLegalDTO Single(int IdTipoCaracter )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TiposDeCaracterLegalRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdTipoCaracter);
                var entityDto = mapperBase.Map<TiposDeCaracterLegal, TiposDeCaracterLegalDTO>(entity);
     
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
		public bool Insert(TiposDeCaracterLegalDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TiposDeCaracterLegalRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<TiposDeCaracterLegalDTO, TiposDeCaracterLegal>(objectDto);                   
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
		public void Update(TiposDeCaracterLegalDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TiposDeCaracterLegalRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<TiposDeCaracterLegalDTO, TiposDeCaracterLegal>(objectDTO);                   
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
		public void Delete(TiposDeCaracterLegalDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TiposDeCaracterLegalRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<TiposDeCaracterLegalDTO, TiposDeCaracterLegal>(objectDto);                   
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

