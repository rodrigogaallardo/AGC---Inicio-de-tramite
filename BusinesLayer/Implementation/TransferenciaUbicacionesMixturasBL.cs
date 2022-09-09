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
    public class TransferenciaUbicacionesMixturasBL : ITransferenciaUbicacionesMixturasBL<TransferenciaUbicacionesMixturasDTO>
    {
        private TransferenciaUbicacionesMixturasRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public TransferenciaUbicacionesMixturasBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Transf_Ubicaciones_Mixturas, TransferenciaUbicacionesMixturasDTO>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }

        /// <summary>
		/// 
		/// </summary>
		/// <param name="IdEncomiendaUbicacion"></param>
		/// <returns></returns>	
		public IEnumerable<TransferenciaUbicacionesMixturasDTO> GetByFKIdSolicitudUbicacion(int IdSolicitudUbicacion)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciaUbicacionesMixturasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdSolicitudUbicacion(IdSolicitudUbicacion);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Ubicaciones_Mixturas>, IEnumerable<TransferenciaUbicacionesMixturasDTO>>(elements);
            return elementsDto;
        }
    }
}
