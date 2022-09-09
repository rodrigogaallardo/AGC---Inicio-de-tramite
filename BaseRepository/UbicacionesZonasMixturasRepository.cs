using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using System.Collections;

namespace BaseRepository
{
    public class UbicacionesZonasMixturasRepository : BaseRepository<Ubicaciones_ZonasMixtura> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public UbicacionesZonasMixturasRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>
        public IEnumerable<Ubicaciones_ZonasMixtura> GetZonasEncomienda(int IdEncomienda)
        {
            EncomiendaUbicacionesRepository ubicacion = new EncomiendaUbicacionesRepository(_unitOfWork);
            int idUbi = (int)ubicacion.GetByFKIdEncomienda(IdEncomienda).FirstOrDefault().id_ubicacion;

            var query = (from ubi in _unitOfWork.Db.Ubicaciones_ZonasMixtura
                         from ubiZon in ubi.Ubicaciones.Where(x => x.id_ubicacion == idUbi)
                         select ubi).ToList();

            return query;
        }

        public IEnumerable<Ubicaciones_ZonasMixtura> GetZonasUbicacion(int IdUbicacion)
        {
            var query = (from ubi in _unitOfWork.Db.Ubicaciones_ZonasMixtura
                         from ubiZon in ubi.Ubicaciones.Where(x => x.id_ubicacion == IdUbicacion)
                         select ubi).Distinct().ToList();

            return query;
        }

        public IEnumerable<Ubicaciones_ZonasMixtura> GetZonasUbicacion(List<int> lstUbi)
        {            
            var query = (from ubi in _unitOfWork.Db.Ubicaciones_ZonasMixtura
                         from ubiZon in ubi.Ubicaciones.Where(x => lstUbi.Contains(x.id_ubicacion))
                         select ubi).Distinct().ToList();

            return query;
        }

        public Ubicaciones_ZonasMixtura GetZona(string codigo)
        {
            var query = (from ubi in _unitOfWork.Db.Ubicaciones_ZonasMixtura
                         where ubi.Codigo == codigo
                         select ubi).FirstOrDefault();

            return query;
        }
    }
}

