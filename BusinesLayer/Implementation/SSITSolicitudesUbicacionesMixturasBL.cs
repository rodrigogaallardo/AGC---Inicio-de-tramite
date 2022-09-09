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
    public class SSITSolicitudesUbicacionesMixturasBL : ISSITSolicitudesUbicacionesMixturasBL<SSITSolicitudesUbicacionesMixturasDTO>
    {
        private SSITSolicitudesUbicacionesMixturasRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public SSITSolicitudesUbicacionesMixturasBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
               cfg.CreateMap<SSIT_Solicitudes_Ubicaciones_Mixturas, SSITSolicitudesUbicacionesMixturasDTO>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }

        /// <summary>
		/// 
		/// </summary>
		/// <param name="IdEncomiendaUbicacion"></param>
		/// <returns></returns>	
		public IEnumerable<SSITSolicitudesUbicacionesMixturasDTO> GetByFKIdSolicitudUbicacion(int IdSolicitudUbicacion)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesUbicacionesMixturasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdSolicitudUbicacion(IdSolicitudUbicacion);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Ubicaciones_Mixturas>, IEnumerable<SSITSolicitudesUbicacionesMixturasDTO>>(elements);
            return elementsDto;
        }
    }
}
