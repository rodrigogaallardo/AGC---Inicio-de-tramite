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
using DataAcess.EntityCustom;

namespace BusinesLayer.Implementation
{
	public class ConsultaPadronUbicacionesPuertasBL : IConsultaPadronUbicacionesPuertasBL<ConsultaPadronUbicacionesPuertasDTO>
    {               
		private ConsultaPadronUbicacionesPuertasRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
    

        public ConsultaPadronUbicacionesPuertasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<ConsultaPadronUbicacionesPuertasDTO, CPadron_Ubicaciones_Puertas>().ReverseMap()
                    .ForMember(dest => dest.IdConsultaPadronPuerta, source => source.MapFrom(p => p.id_cpadronpuerta))
                    .ForMember(dest => dest.IdConsultaPadronUbicacion, source => source.MapFrom(p => p.id_cpadronubicacion))
                    .ForMember(dest => dest.CodigoCalle, source => source.MapFrom(p => p.codigo_calle))
                    .ForMember(dest => dest.NombreCalle, source => source.MapFrom(p => p.nombre_calle))
                    .ForMember(dest => dest.NumeroPuerta, source => source.MapFrom(p => p.NroPuerta));

                cfg.CreateMap<CPadron_Ubicaciones_Puertas, ConsultaPadronUbicacionesPuertasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_cpadronpuerta, source => source.MapFrom(p => p.IdConsultaPadronPuerta))
                    .ForMember(dest => dest.id_cpadronubicacion, source => source.MapFrom(p => p.IdConsultaPadronUbicacion))
                    .ForMember(dest => dest.codigo_calle, source => source.MapFrom(p => p.CodigoCalle))
                    .ForMember(dest => dest.nombre_calle, source => source.MapFrom(p => p.NombreCalle))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NumeroPuerta));
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<ConsultaPadronUbicacionesPuertasDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronUbicacionesPuertasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<CPadron_Ubicaciones_Puertas>, IEnumerable<ConsultaPadronUbicacionesPuertasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public ConsultaPadronUbicacionesPuertasDTO Single(int IdConsultaPadronPuerta )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronUbicacionesPuertasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdConsultaPadronPuerta);
                var entityDto = mapperBase.Map<CPadron_Ubicaciones_Puertas, ConsultaPadronUbicacionesPuertasDTO>(entity);
     
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
		/// <param name="IdConsultaPadronUbicacion"></param>
		/// <returns></returns>	
		public IEnumerable<ConsultaPadronUbicacionesPuertasDTO> GetByFKIdConsultaPadronUbicacion(int IdConsultaPadronUbicacion)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new ConsultaPadronUbicacionesPuertasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdConsultaPadronUbicacion(IdConsultaPadronUbicacion);
            var elementsDto = mapperBase.Map<IEnumerable<CPadron_Ubicaciones_Puertas>, IEnumerable<ConsultaPadronUbicacionesPuertasDTO>>(elements);
            return elementsDto;				
		}
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(ConsultaPadronUbicacionesPuertasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new ConsultaPadronUbicacionesPuertasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<ConsultaPadronUbicacionesPuertasDTO, CPadron_Ubicaciones_Puertas>(objectDto);                   
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
		public void Update(ConsultaPadronUbicacionesPuertasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new ConsultaPadronUbicacionesPuertasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<ConsultaPadronUbicacionesPuertasDTO, CPadron_Ubicaciones_Puertas>(objectDTO);                   
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
		public void Delete(ConsultaPadronUbicacionesPuertasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new ConsultaPadronUbicacionesPuertasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<ConsultaPadronUbicacionesPuertasDTO, CPadron_Ubicaciones_Puertas>(objectDto);                   
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

