using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class TiposDeUbicacionRepository : BaseRepository<TiposDeUbicacion> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public TiposDeUbicacionRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdUbicacion"></param>
        /// <returns></returns>
        public TiposDeUbicacion Get(int IdUbicacion)
        {
            var dataEsp = (from ubic in _unitOfWork.Db.Ubicaciones
                           join stu in _unitOfWork.Db.SubTiposDeUbicacion on ubic.id_subtipoubicacion equals stu.id_subtipoubicacion
                           join tu in _unitOfWork.Db.TiposDeUbicacion on stu.id_tipoubicacion equals tu.id_tipoubicacion
                           where ubic.id_ubicacion == IdUbicacion
                           select tu 
                           ).FirstOrDefault();

            return dataEsp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TiposDeUbicacion> Get()
        {
            return (from tipoubic in _unitOfWork.Db.TiposDeUbicacion
                    where tipoubic.id_tipoubicacion != 0
                    select tipoubic);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdUbicacion"></param>
        /// <returns></returns>
        public IEnumerable<TiposDeUbicacion> GetTiposDeUbicacionExcluir(int IdTipoUbicacion)
        {
            var dataEsp = (from tu in _unitOfWork.Db.TiposDeUbicacion
                           where tu.id_tipoubicacion != IdTipoUbicacion
                           select tu
                           );

            return dataEsp;
        }
	}
}

