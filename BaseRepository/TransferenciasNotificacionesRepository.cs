using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class TransferenciasNotificacionesRepository : BaseRepository<Transf_Solicitudes_Notificaciones> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public TransferenciasNotificacionesRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }

        public Transf_Solicitudes_Notificaciones GetNotificacionByIdNotificacion(int idNotificacion)
        {
            return (from not in _unitOfWork.Db.Transf_Solicitudes_Notificaciones
                    where not.id_notificacion == idNotificacion
                    select not).FirstOrDefault();
        }
    }
}

