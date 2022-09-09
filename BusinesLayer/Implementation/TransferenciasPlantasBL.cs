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
	public class TransferenciasPlantasBL : ITransferenciasPlantasBL<TransferenciasPlantasDTO>
    {               
		private TransferenciasPlantasRepository repo = null;                        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
        IMapper mapperBaseEntity;
		         
        public TransferenciasPlantasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TransferenciasPlantasDTO, Transf_Plantas>().ReverseMap()
                    .ForMember(dest => dest.IdTransferenciaTipoSector, source => source.MapFrom(p => p.id_transftiposector))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdTipoSector, source => source.MapFrom(p => p.id_tiposector))
                    .ForMember(dest => dest.DetalleTransferenciaTipoSector, source => source.MapFrom(p => p.detalle_transftiposector))
                    .ForMember(dest => dest.TipoSector, source => source.MapFrom(p => p.TipoSector));

                cfg.CreateMap<TipoSectorDTO, TipoSector>().ReverseMap();

                cfg.CreateMap<Transf_Plantas, TransferenciasPlantasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_transftiposector, source => source.MapFrom(p => p.IdTransferenciaTipoSector))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_tiposector, source => source.MapFrom(p => p.IdTipoSector))
                    .ForMember(dest => dest.detalle_transftiposector, source => source.MapFrom(p => p.DetalleTransferenciaTipoSector))
                    .ForMember(dest => dest.TipoSector, source => source.MapFrom(p => p.TipoSector));

            });
            mapperBase = config.CreateMapper();

            var config1 = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TransferenciasPlantasModelDTO, TransferenciasPlantasEntity>().ReverseMap();
            });
            mapperBaseEntity = config1.CreateMapper();
        }
		
        public IEnumerable<TransferenciasPlantasDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasPlantasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Transf_Plantas>, IEnumerable<TransferenciasPlantasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public TransferenciasPlantasDTO Single(int IdTransferenciaTipoSector )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasPlantasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdTransferenciaTipoSector);
                var entityDto = mapperBase.Map<Transf_Plantas, TransferenciasPlantasDTO>(entity);
     
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
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>	
        public IEnumerable<TransferenciasPlantasDTO> GetByFKIdSolicitud(int IdSolicitud)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasPlantasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdSolicitud(IdSolicitud);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Plantas>, IEnumerable<TransferenciasPlantasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoSector"></param>
		/// <returns></returns>	
		public IEnumerable<TransferenciasPlantasDTO> GetByFKIdTipoSector(int IdTipoSector)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasPlantasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTipoSector(IdTipoSector);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Plantas>, IEnumerable<TransferenciasPlantasDTO>>(elements);
            return elementsDto;				
		}
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(TransferenciasPlantasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {              
                    repo = new TransferenciasPlantasRepository(unitOfWork);                    
                    var elementDto = mapperBase.Map<TransferenciasPlantasDTO, Transf_Plantas>(objectDto);                   

                    if (objectDto.IdTransferenciaTipoSector > 0){
                        repo.Update(elementDto);
                    } else {
                        repo.Insert(elementDto);
                    }
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
		public void Update(TransferenciasPlantasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TransferenciasPlantasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<TransferenciasPlantasDTO, Transf_Plantas>(objectDTO);                   
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
		public void Delete(TransferenciasPlantasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TransferenciasPlantasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<TransferenciasPlantasDTO, Transf_Plantas>(objectDto);                   
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
        public IEnumerable<TransferenciasPlantasModelDTO> Get(int IdSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasPlantasRepository(this.uowF.GetUnitOfWork());
                var lstMenuesDto = repo.Get(IdSolicitud);
                var elementDto = mapperBaseEntity.Map<IEnumerable<TransferenciasPlantasEntity>, IEnumerable<TransferenciasPlantasModelDTO>>(lstMenuesDto);
                return elementDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="plantas"></param>
        public void ActualizarPlantas(List<TransferenciasPlantasDTO> plantas)
        {
            try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {
                    repo = new TransferenciasPlantasRepository(unitOfWork); 
                    
                    foreach (var objectDto in plantas)
                    {
                        var elementDto = mapperBase.Map<TransferenciasPlantasDTO, Transf_Plantas>(objectDto);

                        if (objectDto.Seleccionado)
                        {
                            if (objectDto.IdTransferenciaTipoSector > 0)
                            {
                                repo.Update(elementDto);
                            }
                            else
                            {
                                repo.Insert(elementDto);
                            }
                        }
                        else
                        {
                            if (objectDto.IdTransferenciaTipoSector > 0)
                            {                                
                                string nombre = repo.Exists(objectDto.IdTransferenciaTipoSector);
                                if (string.IsNullOrEmpty(nombre))
                                {
                                    repo.Delete(elementDto);
                                }
                                else 
                                {
                                    throw new Exception(
                                       string.Format("La planta {0} se está utilizando en destino {1}. Para eliminarla deberá quitarla de la conformación del local. ", objectDto.DetalleTransferenciaTipoSector, nombre)
                                       );
                                }
                            
                            }
                        }
                    }
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

