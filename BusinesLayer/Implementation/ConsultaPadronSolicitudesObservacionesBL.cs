using AutoMapper;
using BaseRepository;
using IBusinessLayer;
using Dal.UnitOfWork;
using DataAcess;
using DataAcess.EntityCustom;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitOfWork;

namespace BusinesLayer.Implementation
{
	public class ConsultaPadronSolicitudesObservacionesBL : IConsultaPadronSolicitudesObservacionesBL<ConsultaPadronSolicitudesObservacionesDTO>
    {               
		private ConsultaPadronSolicitudesObservacionesRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public ConsultaPadronSolicitudesObservacionesBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CPadron_Solicitudes_Observaciones, ConsultaPadronSolicitudesObservacionesDTO>()
                    .ForMember(dest => dest.IdConsultaPadronObservacion, source => source.MapFrom(p => p.id_cpadron_observacion))
                    .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                    .ForMember(dest => dest.Observaciones, source => source.MapFrom(p => p.observaciones))
                    .ForMember(dest => dest.Leido, source => source.MapFrom(p => p.leido))
                    .ForMember(dest => dest.User, source => source.MapFrom(p => p.aspnet_Users));

                cfg.CreateMap<aspnet_Users, AspnetUserDTO>()
                    .ForMember(dest => dest.SGIProfile, source => source.MapFrom(p => p.SGI_Profiles))
                    .ForMember(dest => dest.Usuario, source => source.MapFrom(p => p.Usuario));

                cfg.CreateMap<SGI_Profiles, SGIProfileDTO>();

                cfg.CreateMap<Usuario, UsuarioDTO>();

            });
            mapperBase = config.CreateMapper();
        }
		
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdConsultaPadronObservacion"></param>
        /// <returns></returns>
		public ConsultaPadronSolicitudesObservacionesDTO Single(int IdConsultaPadronObservacion )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronSolicitudesObservacionesRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdConsultaPadronObservacion);
                var entityDto = mapperBase.Map<CPadron_Solicitudes_Observaciones, ConsultaPadronSolicitudesObservacionesDTO>(entity);
     
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
		public IEnumerable<ConsultaPadronSolicitudesObservacionesDTO> GetByFKIdConsultaPadron(int IdConsultaPadron)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new ConsultaPadronSolicitudesObservacionesRepository(this.uowF.GetUnitOfWork());
             var elements = repo.GetByFKIdConsultaPadron(IdConsultaPadron);
            var elementsDto = mapperBase.Map<IEnumerable<CPadron_Solicitudes_Observaciones>, IEnumerable<ConsultaPadronSolicitudesObservacionesDTO>>(elements);
            return elementsDto;				
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdConsultaPadron"></param>
        /// <returns></returns>
        public IEnumerable<ConsultaPadronSolicitudesObservacionesDTO> Get(int IdConsultaPadron)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronSolicitudesObservacionesRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Get(IdConsultaPadron);
                var entityDto = mapperBase.Map<IEnumerable<CPadron_Solicitudes_Observaciones>, IEnumerable<ConsultaPadronSolicitudesObservacionesDTO>>(entity);

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
        /// <param name="IdConsultaPadronObservacion"></param>
        public void Leer(int IdConsultaPadronObservacion)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new ConsultaPadronSolicitudesObservacionesRepository(unitOfWork);
                    var objectDTO = repo.Single(IdConsultaPadronObservacion);
                    objectDTO.leido = true;
                    repo.Update(objectDTO);
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

