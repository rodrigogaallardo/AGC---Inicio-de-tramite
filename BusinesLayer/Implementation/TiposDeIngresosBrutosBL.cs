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
	public class TiposDeIngresosBrutosBL : ITiposDeIngresosBrutosBL<TiposDeIngresosBrutosDTO>
    {               
		private TiposDeIngresosBrutosRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public TiposDeIngresosBrutosBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TiposDeIngresosBrutosDTO, TiposDeIngresosBrutos>().ReverseMap()
                    .ForMember(dest => dest.IdTipoIb, source => source.MapFrom(p => p.id_tipoiibb))
                    .ForMember(dest => dest.CodTipoIb, source => source.MapFrom(p => p.cod_tipoibb))
                    .ForMember(dest => dest.NomTipoIb, source => source.MapFrom(p => p.nom_tipoiibb))
                    .ForMember(dest => dest.FormatoTipoIb, source => source.MapFrom(p => p.formato_tipoiibb));

                cfg.CreateMap<TiposDeIngresosBrutos, TiposDeIngresosBrutosDTO>().ReverseMap()
                    .ForMember(dest => dest.id_tipoiibb, source => source.MapFrom(p => p.IdTipoIb))
                    .ForMember(dest => dest.cod_tipoibb, source => source.MapFrom(p => p.CodTipoIb))
                    .ForMember(dest => dest.nom_tipoiibb, source => source.MapFrom(p => p.NomTipoIb))
                    .ForMember(dest => dest.formato_tipoiibb, source => source.MapFrom(p => p.FormatoTipoIb));
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<TiposDeIngresosBrutosDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TiposDeIngresosBrutosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<TiposDeIngresosBrutos>, IEnumerable<TiposDeIngresosBrutosDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public TiposDeIngresosBrutosDTO Single(int IdTipoIb )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TiposDeIngresosBrutosRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdTipoIb);
                var entityDto = mapperBase.Map<TiposDeIngresosBrutos, TiposDeIngresosBrutosDTO>(entity);
     
                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<TiposDeIngresosBrutosDTO> GetByIdTipoIb(int IdTipoIb)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TiposDeIngresosBrutosRepository(this.uowF.GetUnitOfWork());
                var entity = repo.GetByIdTipoIb(IdTipoIb);
                var entityDto = mapperBase.Map<IEnumerable<TiposDeIngresosBrutos>, IEnumerable<TiposDeIngresosBrutosDTO>>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TiposDeIngresosBrutosDTO> GetIngresosBrutos()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TiposDeIngresosBrutosRepository(this.uowF.GetUnitOfWork());
                var entity = repo.GetIngresosBrutos();
                var entityDto = mapperBase.Map<IEnumerable<TiposDeIngresosBrutos>, IEnumerable<TiposDeIngresosBrutosDTO>>(entity);

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
		public bool Insert(TiposDeIngresosBrutosDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TiposDeIngresosBrutosRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<TiposDeIngresosBrutosDTO, TiposDeIngresosBrutos>(objectDto);                   
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
		public void Update(TiposDeIngresosBrutosDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TiposDeIngresosBrutosRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<TiposDeIngresosBrutosDTO, TiposDeIngresosBrutos>(objectDTO);                   
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
		public void Delete(TiposDeIngresosBrutosDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TiposDeIngresosBrutosRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<TiposDeIngresosBrutosDTO, TiposDeIngresosBrutos>(objectDto);                   
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

