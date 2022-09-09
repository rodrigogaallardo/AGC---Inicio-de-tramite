using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class SSITSolicitudesEncomiendaRepository : BaseRepository<SSIT_Solicitudes_Encomienda> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public SSITSolicitudesEncomiendaRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }

        public IEnumerable<SSIT_Solicitudes_Encomienda> GetByFKIdSolicitud(int id_solicitud)
        {
            IEnumerable<SSIT_Solicitudes_Encomienda> domains = _unitOfWork.Db.SSIT_Solicitudes_Encomienda.Where(x =>
                                                        x.id_solicitud == id_solicitud
                                                        );

            return domains;
        }
	}
}

