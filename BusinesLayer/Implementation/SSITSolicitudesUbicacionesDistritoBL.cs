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
    public class SSITSolicitudesUbicacionesDistritoBL : ISSITSolicitudesUbicacionesDistritoBL<SSITSolicitudesUbicacionesDistritoDTO>
    {
        private SSITSolicitudesUbicacionesDistritosRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public SSITSolicitudesUbicacionesDistritoBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SSIT_Solicitudes_Ubicaciones_Distritos, SSITSolicitudesUbicacionesDistritoDTO>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }

        /// <summary>
		/// 
		/// </summary>
		/// <param name="IdEncomiendaUbicacion"></param>
		/// <returns></returns>	
		public IEnumerable<SSITSolicitudesUbicacionesDistritoDTO> GetByFKIdSolicitudUbicacion(int IdSolicitudUbicacion)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesUbicacionesDistritosRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdSolicitudUbicacion(IdSolicitudUbicacion);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Ubicaciones_Distritos>, IEnumerable<SSITSolicitudesUbicacionesDistritoDTO>>(elements);
            return elementsDto;
        }
    }
}
