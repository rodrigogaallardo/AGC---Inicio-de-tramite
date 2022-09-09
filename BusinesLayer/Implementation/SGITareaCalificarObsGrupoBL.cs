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
	public class SGITareaCalificarObsGrupoBL : ISGITareaCalificarObsGrupoBL<SGITareaCalificarObsGrupoDTO>
    {               
		private SGITareaCalificarObsGrupoRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
        IMapper mapperGrilla;
         
        public SGITareaCalificarObsGrupoBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SGITareaCalificarObsGrupoDTO, SGI_Tarea_Calificar_ObsGrupo>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
            
            var configGrilla = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SGITareaCalificarObsGrupoGrillaEntity, SGITareaCalificarObsGrupoGrillaDTO>()
                   .ForMember(dest => dest.SGITareaCalificarObsGrupo, source => source.MapFrom(p => p.Observaciones));

                cfg.CreateMap<SGITareaCalificarObsGrillaEntity, SGITareaCalificarObsDocsGrillaDTO>();
            });
            mapperGrilla = configGrilla.CreateMapper();
        }
		
		public SGITareaCalificarObsGrupoDTO Single(int Id )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SGITareaCalificarObsGrupoRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(Id);
                var entityDto = mapperBase.Map<SGI_Tarea_Calificar_ObsGrupo, SGITareaCalificarObsGrupoDTO>(entity);
     
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
		public IEnumerable<SGITareaCalificarObsGrupoGrillaDTO> GetByFKIdSolicitud(int IdSolicitud)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SGITareaCalificarObsGrupoRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdSolicitud(IdSolicitud);
            var elementsDto = mapperGrilla.Map<IEnumerable<SGITareaCalificarObsGrupoGrillaEntity>, IEnumerable<SGITareaCalificarObsGrupoGrillaDTO>>(elements);
            return elementsDto;				
		}

        /// <summary>
		/// 
		/// </summary>
		/// <param name="IdSolicitud"></param>
		/// <returns></returns>	
		public IEnumerable<SGITareaCalificarObsGrupoGrillaDTO> GetByFKIdSolicitudtr(int IdSolicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SGITareaCalificarObsGrupoRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdSolicitudtr(IdSolicitud);
            var elementsDto = mapperGrilla.Map<IEnumerable<SGITareaCalificarObsGrupoGrillaEntity>, IEnumerable<SGITareaCalificarObsGrupoGrillaDTO>>(elements);
            return elementsDto;
        }

    }
}

