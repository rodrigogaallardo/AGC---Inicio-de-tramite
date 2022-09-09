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
    public class EncomiendaUbicacionesMixturasBL : IEncomiendaUbicacionesMixturasBL<Encomienda_Ubicaciones_MixturasDTO>
    {
        private EncomiendaUbicacionesMixturasRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public EncomiendaUbicacionesMixturasBL()
        {
            var config = new MapperConfiguration(cfg =>
            {            
                cfg.CreateMap<Encomienda_Ubicaciones_Mixturas, Encomienda_Ubicaciones_MixturasDTO>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }

        /// <summary>
		/// 
		/// </summary>
		/// <param name="IdEncomiendaUbicacion"></param>
		/// <returns></returns>	
		public IEnumerable<Encomienda_Ubicaciones_MixturasDTO> GetByFKIdEncomiendaUbicacion(int IdEncomiendaUbicacion)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaUbicacionesMixturasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdEncomiendaUbicacion(IdEncomiendaUbicacion);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Ubicaciones_Mixturas>, IEnumerable<Encomienda_Ubicaciones_MixturasDTO>>(elements);
            return elementsDto;
        }
    }
}
