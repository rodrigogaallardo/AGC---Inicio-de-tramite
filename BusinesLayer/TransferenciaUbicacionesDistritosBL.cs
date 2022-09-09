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

namespace BusinesLayer
{
    public class TransferenciaUbicacionesDistritosBL : ITransferenciaUbicacionesDistritosBL<TransferenciaUbicacionesDistritosDTO>
    {
        private TransferenciaUbicacionesDistritosRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public TransferenciaUbicacionesDistritosBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Transf_Ubicaciones_Distritos, TransferenciaUbicacionesDistritosDTO>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }

        /// <summary>
		/// 
		/// </summary>
		/// <param name="IdEncomiendaUbicacion"></param>
		/// <returns></returns>	
		public IEnumerable<TransferenciaUbicacionesDistritosDTO> GetByFKIdSolicitudUbicacion(int IdSolicitudUbicacion)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciaUbicacionesDistritosRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdSolicitudUbicacion(IdSolicitudUbicacion);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Ubicaciones_Distritos>, IEnumerable<TransferenciaUbicacionesDistritosDTO>>(elements);
            return elementsDto;
        }
    }
}
