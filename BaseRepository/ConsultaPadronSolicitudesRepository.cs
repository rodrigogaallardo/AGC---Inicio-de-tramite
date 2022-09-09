using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;
using StaticClass;

namespace BaseRepository
{
    public class ConsultaPadronSolicitudesRepository : BaseRepository<CPadron_Solicitudes> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public ConsultaPadronSolicitudesRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listIDCPadron"></param>
        /// <returns></returns>
        public List<CPadron_Solicitudes> GetListaIdCPadron(List<int> listIDCPadron)
        {
            return _unitOfWork.Db.CPadron_Solicitudes.Where(x => listIDCPadron.Contains(x.id_cpadron)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_cpadronDesde"></param>
        /// <param name="id_cpadronHasta"></param>
        /// <returns></returns>
        public List<CPadron_Solicitudes> GetRangoIdCpadron(int id_cpadronDesde, int id_cpadronHasta)
        {
            return _unitOfWork.Db.CPadron_Solicitudes.Where(x => x.id_cpadron >= id_cpadronDesde && x.id_cpadron <= id_cpadronHasta).ToList();
        }        
    }
}

