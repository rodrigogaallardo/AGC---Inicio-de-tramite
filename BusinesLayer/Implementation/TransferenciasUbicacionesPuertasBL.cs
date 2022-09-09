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
	public class TransferenciasUbicacionesPuertasBL : ITransferenciasUbicacionesPuertasBL<TransferenciasUbicacionesPuertasDTO>
    {               
		private TransferenciasUbicacionesPuertasRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
    

        public TransferenciasUbicacionesPuertasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<TransferenciasUbicacionesPuertasDTO, Transf_Ubicaciones_Puertas>().ReverseMap()
                    .ForMember(dest => dest.IdTranferenciaPuerta, source => source.MapFrom(p => p.id_transfpuerta))
                    .ForMember(dest => dest.IdTranferenciaUbicacion, source => source.MapFrom(p => p.id_transfubicacion))
                    .ForMember(dest => dest.CodigoCalle, source => source.MapFrom(p => p.codigo_calle))
                    .ForMember(dest => dest.NombreCalle, source => source.MapFrom(p => p.nombre_calle))
                    .ForMember(dest => dest.NumeroPuerta, source => source.MapFrom(p => p.NroPuerta));

                cfg.CreateMap<Transf_Ubicaciones_Puertas, TransferenciasUbicacionesPuertasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_transfpuerta, source => source.MapFrom(p => p.IdTranferenciaPuerta))
                    .ForMember(dest => dest.id_transfubicacion, source => source.MapFrom(p => p.IdTranferenciaUbicacion))
                    .ForMember(dest => dest.codigo_calle, source => source.MapFrom(p => p.CodigoCalle))
                    .ForMember(dest => dest.nombre_calle, source => source.MapFrom(p => p.NombreCalle))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NumeroPuerta));
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<TransferenciasUbicacionesPuertasDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasUbicacionesPuertasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Transf_Ubicaciones_Puertas>, IEnumerable<TransferenciasUbicacionesPuertasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public TransferenciasUbicacionesPuertasDTO Single(int IdTransferenciaPuerta )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasUbicacionesPuertasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdTransferenciaPuerta);
                var entityDto = mapperBase.Map<Transf_Ubicaciones_Puertas, TransferenciasUbicacionesPuertasDTO>(entity);
     
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
		/// <param name="IdTransferenciaUbicacion"></param>
		/// <returns></returns>	
		public IEnumerable<TransferenciasUbicacionesPuertasDTO> GetByFKIdTransferenciaUbicacion(int IdTransferenciaUbicacion)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasUbicacionesPuertasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTransferenciaUbicacion(IdTransferenciaUbicacion);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Ubicaciones_Puertas>, IEnumerable<TransferenciasUbicacionesPuertasDTO>>(elements);
            return elementsDto;				
		}
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(TransferenciasUbicacionesPuertasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TransferenciasUbicacionesPuertasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<TransferenciasUbicacionesPuertasDTO, Transf_Ubicaciones_Puertas>(objectDto);                   
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
		public void Update(TransferenciasUbicacionesPuertasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TransferenciasUbicacionesPuertasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<TransferenciasUbicacionesPuertasDTO, Transf_Ubicaciones_Puertas>(objectDTO);                   
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
		public void Delete(TransferenciasUbicacionesPuertasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TransferenciasUbicacionesPuertasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<TransferenciasUbicacionesPuertasDTO, Transf_Ubicaciones_Puertas>(objectDto);                   
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

