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
	public class ConsultaPadronPlantasBL : IConsultaPadronPlantasBL<ConsultaPadronPlantasDTO>
    {               
		private ConsultaPadronPlantasRepository repo = null;                        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
        IMapper mapperBaseEntity;
		         
        public ConsultaPadronPlantasBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ConsultaPadronPlantasDTO, CPadron_Plantas>().ReverseMap()
                    .ForMember(dest => dest.IdConsultaPadronTipoSector, source => source.MapFrom(p => p.id_cpadrontiposector))
                    .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                    .ForMember(dest => dest.IdTipoSector, source => source.MapFrom(p => p.id_tiposector))
                    .ForMember(dest => dest.DetalleConsultaPadronTipoSector, source => source.MapFrom(p => p.detalle_cpadrontiposector))
                    .ForMember(dest => dest.TipoSector, source => source.MapFrom(p => p.TipoSector));

                cfg.CreateMap<TipoSectorDTO, TipoSector>().ReverseMap();

                cfg.CreateMap<CPadron_Plantas, ConsultaPadronPlantasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_cpadrontiposector, source => source.MapFrom(p => p.IdConsultaPadronTipoSector))
                    .ForMember(dest => dest.id_cpadron, source => source.MapFrom(p => p.IdConsultaPadron))
                    .ForMember(dest => dest.id_tiposector, source => source.MapFrom(p => p.IdTipoSector))
                    .ForMember(dest => dest.detalle_cpadrontiposector, source => source.MapFrom(p => p.DetalleConsultaPadronTipoSector))
                    .ForMember(dest => dest.TipoSector, source => source.MapFrom(p => p.TipoSector));

            });
            mapperBase = config.CreateMapper();

            var config1 = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ConsultaPadronPlantasModelDTO, ConsultaPadronPlantasEntity>().ReverseMap();
            });
            mapperBaseEntity = config1.CreateMapper();
        }
		
        public IEnumerable<ConsultaPadronPlantasDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronPlantasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<CPadron_Plantas>, IEnumerable<ConsultaPadronPlantasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public ConsultaPadronPlantasDTO Single(int IdConsultaPadronTipoSector )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronPlantasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdConsultaPadronTipoSector);
                var entityDto = mapperBase.Map<CPadron_Plantas, ConsultaPadronPlantasDTO>(entity);
     
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
		public IEnumerable<ConsultaPadronPlantasDTO> GetByFKIdConsultaPadron(int IdConsultaPadron)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new ConsultaPadronPlantasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdConsultaPadron(IdConsultaPadron);
            var elementsDto = mapperBase.Map<IEnumerable<CPadron_Plantas>, IEnumerable<ConsultaPadronPlantasDTO>>(elements);
            return elementsDto;				
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoSector"></param>
		/// <returns></returns>	
		public IEnumerable<ConsultaPadronPlantasDTO> GetByFKIdTipoSector(int IdTipoSector)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new ConsultaPadronPlantasRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdTipoSector(IdTipoSector);
            var elementsDto = mapperBase.Map<IEnumerable<CPadron_Plantas>, IEnumerable<ConsultaPadronPlantasDTO>>(elements);
            return elementsDto;				
		}
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public bool Insert(ConsultaPadronPlantasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {              
                    repo = new ConsultaPadronPlantasRepository(unitOfWork);                    
                    var elementDto = mapperBase.Map<ConsultaPadronPlantasDTO, CPadron_Plantas>(objectDto);                   

                    if (objectDto.IdConsultaPadronTipoSector > 0){
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
		public void Update(ConsultaPadronPlantasDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new ConsultaPadronPlantasRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<ConsultaPadronPlantasDTO, CPadron_Plantas>(objectDTO);                   
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
		public void Delete(ConsultaPadronPlantasDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new ConsultaPadronPlantasRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<ConsultaPadronPlantasDTO, CPadron_Plantas>(objectDto);                   
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
        public IEnumerable<ConsultaPadronPlantasModelDTO> Get(int IdSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronPlantasRepository(this.uowF.GetUnitOfWork());
                var lstMenuesDto = repo.Get(IdSolicitud);
                var elementDto = mapperBaseEntity.Map<IEnumerable<ConsultaPadronPlantasEntity>, IEnumerable<ConsultaPadronPlantasModelDTO>>(lstMenuesDto);
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
        public void ActualizarPlantas(List<ConsultaPadronPlantasDTO> plantas)
        {
            try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {
                    repo = new ConsultaPadronPlantasRepository(unitOfWork); 
                    
                    foreach (var objectDto in plantas)
                    {
                        var elementDto = mapperBase.Map<ConsultaPadronPlantasDTO, CPadron_Plantas>(objectDto);

                        if (objectDto.Seleccionado)
                        {
                            if (objectDto.IdConsultaPadronTipoSector > 0)
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
                            if (objectDto.IdConsultaPadronTipoSector > 0)
                            {                                
                                string nombre = repo.Exists(objectDto.IdConsultaPadronTipoSector);
                                if (string.IsNullOrEmpty(nombre))
                                {
                                    repo.Delete(elementDto);
                                }
                                else 
                                {
                                    throw new Exception(
                                       string.Format("La planta {0} se está utilizando en destino {1}. Para eliminarla deberá quitarla de la conformación del local. ", objectDto.DetalleConsultaPadronTipoSector, nombre)
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

