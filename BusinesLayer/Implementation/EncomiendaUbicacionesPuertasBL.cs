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
	public class EncomiendaUbicacionesPuertasBL : IEncomiendaUbicacionesPuertasBL<EncomiendaUbicacionesPuertasDTO>
    {               
		private EncomiendaUbicacionesPuertasRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public EncomiendaUbicacionesPuertasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {				
                cfg.CreateMap<EncomiendaUbicacionesPuertasDTO, Encomienda_Ubicaciones_Puertas>().ReverseMap()
                    .ForMember(dest => dest.IdEncomiendaPuerta, source => source.MapFrom(p => p.id_encomiendapuerta))
                    .ForMember(dest => dest.IdEncomiendaUbicacion, source => source.MapFrom(p => p.id_encomiendaubicacion))
                    .ForMember(dest => dest.CodigoCalle, source => source.MapFrom(p => p.codigo_calle))
                    .ForMember(dest => dest.NombreCalle, source => source.MapFrom(p => p.nombre_calle))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NroPuerta));

                cfg.CreateMap<Encomienda_Ubicaciones_Puertas, EncomiendaUbicacionesPuertasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_encomiendapuerta, source => source.MapFrom(p => p.IdEncomiendaPuerta))
                    .ForMember(dest => dest.id_encomiendaubicacion, source => source.MapFrom(p => p.IdEncomiendaUbicacion))
                    .ForMember(dest => dest.codigo_calle, source => source.MapFrom(p => p.CodigoCalle))
                    .ForMember(dest => dest.nombre_calle, source => source.MapFrom(p => p.NombreCalle))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NroPuerta));
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<EncomiendaUbicacionesPuertasDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaUbicacionesPuertasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Ubicaciones_Puertas>, IEnumerable<EncomiendaUbicacionesPuertasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public EncomiendaUbicacionesPuertasDTO Single(int IdEncomiendaPuerta )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaUbicacionesPuertasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdEncomiendaPuerta);
                var entityDto = mapperBase.Map<Encomienda_Ubicaciones_Puertas, EncomiendaUbicacionesPuertasDTO>(entity);
     
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
		/// <param name="IdEncomiendaUbicacion"></param>
		/// <returns></returns>	
		public IEnumerable<EncomiendaUbicacionesPuertasDTO> GetByFKIdEncomiendaUbicacion(int IdEncomiendaUbicacion)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaUbicacionesPuertasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdEncomiendaUbicacion(IdEncomiendaUbicacion);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Ubicaciones_Puertas>, IEnumerable<EncomiendaUbicacionesPuertasDTO>>(elements);
            return elementsDto;				
		}
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(EncomiendaUbicacionesPuertasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new EncomiendaUbicacionesPuertasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<EncomiendaUbicacionesPuertasDTO, Encomienda_Ubicaciones_Puertas>(objectDto);                   
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
		public void Update(EncomiendaUbicacionesPuertasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new EncomiendaUbicacionesPuertasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<EncomiendaUbicacionesPuertasDTO, Encomienda_Ubicaciones_Puertas>(objectDTO);                   
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
		public void Delete(EncomiendaUbicacionesPuertasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new EncomiendaUbicacionesPuertasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<EncomiendaUbicacionesPuertasDTO, Encomienda_Ubicaciones_Puertas>(objectDto);                   
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

