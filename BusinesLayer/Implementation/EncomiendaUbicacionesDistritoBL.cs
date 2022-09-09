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
    public class EncomiendaUbicacionesDistritoBL : IEncomiendaUbicacionesDistritoBL<Encomienda_Ubicaciones_DistritosDTO>
    {
        private EncomiendaUbicacionesDistritosRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public EncomiendaUbicacionesDistritoBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Encomienda_Ubicaciones_Distritos, Encomienda_Ubicaciones_DistritosDTO>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }

        /// <summary>
		/// 
		/// </summary>
		/// <param name="IdEncomiendaUbicacion"></param>
		/// <returns></returns>	
		public IEnumerable<Encomienda_Ubicaciones_DistritosDTO> GetByFKIdEncomiendaUbicacion(int IdEncomiendaUbicacion)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaUbicacionesDistritosRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdEncomiendaUbicacion(IdEncomiendaUbicacion);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Ubicaciones_Distritos>, IEnumerable<Encomienda_Ubicaciones_DistritosDTO>>(elements);
            return elementsDto;
        }
    }
}
