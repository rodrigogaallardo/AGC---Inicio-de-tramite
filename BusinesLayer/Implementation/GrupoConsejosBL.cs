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
	public class GrupoConsejosBL : IGrupoConsejosBL<GrupoConsejosDTO>
    {               
		private GrupoConsejosRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public GrupoConsejosBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {				
                cfg.CreateMap<GrupoConsejosDTO, GrupoConsejos>().ReverseMap()
                    .ForMember(dest => dest.Id, source => source.MapFrom(p => p.id_grupoconsejo))
                    .ForMember(dest => dest.Nombre, source => source.MapFrom(p => p.nombre_grupoconsejo))
                    .ForMember(dest => dest.Descripcion, source => source.MapFrom(p => p.descripcion_grupoconsejo))
                    .ForMember(dest => dest.LogoImpresion, source => source.MapFrom(p => p.logo_impresion_grupoconsejo))
                    .ForMember(dest => dest.LogoPantalla, source => source.MapFrom(p => p.logo_pantalla_grupoconsejo));

                cfg.CreateMap<GrupoConsejos, GrupoConsejosDTO>().ReverseMap()
                    .ForMember(dest => dest.id_grupoconsejo, source => source.MapFrom(p => p.Id))
                    .ForMember(dest => dest.nombre_grupoconsejo, source => source.MapFrom(p => p.Nombre))
                    .ForMember(dest => dest.descripcion_grupoconsejo, source => source.MapFrom(p => p.Descripcion))
                    .ForMember(dest => dest.logo_impresion_grupoconsejo, source => source.MapFrom(p => p.LogoImpresion))
                    .ForMember(dest => dest.logo_pantalla_grupoconsejo, source => source.MapFrom(p => p.LogoPantalla));
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<GrupoConsejosDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new GrupoConsejosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<GrupoConsejos>, IEnumerable<GrupoConsejosDTO>>(elements);
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
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<GrupoConsejosDTO> Get(Guid userId)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new GrupoConsejosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.Get(userId);
                var elementsDto = mapperBase.Map<IEnumerable<GrupoConsejos>, IEnumerable<GrupoConsejosDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
		public GrupoConsejosDTO Single(int Id )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new GrupoConsejosRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(Id);
                var entityDto = mapperBase.Map<GrupoConsejos, GrupoConsejosDTO>(entity);
     
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
		public bool Insert(GrupoConsejosDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new GrupoConsejosRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<GrupoConsejosDTO, GrupoConsejos>(objectDto);                   
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
		public void Update(GrupoConsejosDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new GrupoConsejosRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<GrupoConsejosDTO, GrupoConsejos>(objectDTO);                   
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
		public void Delete(GrupoConsejosDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new GrupoConsejosRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<GrupoConsejosDTO, GrupoConsejos>(objectDto);                   
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
        /// <param name="lstIdGrupoExcluir"></param>
        /// <returns></returns>
        public IEnumerable<GrupoConsejosDTO> GetExcluye(List<int> lstIdGrupoExcluir)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new GrupoConsejosRepository(this.uowF.GetUnitOfWork());

                var elements = repo.GetExcluye(lstIdGrupoExcluir);
                var elementsDto = mapperBase.Map<IEnumerable<GrupoConsejos>, IEnumerable<GrupoConsejosDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       
    }
}

