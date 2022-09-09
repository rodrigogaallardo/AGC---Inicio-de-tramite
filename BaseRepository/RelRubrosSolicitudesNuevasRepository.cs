using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    /// <summary>
    /// Representa a la entidad rubros del Schema DTO
    /// </summary>
    public class RelRubrosSolicitudesNuevasRepository : BaseRepository<Rel_Rubros_Solicitudes_Nuevas>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RelRubrosSolicitudesNuevasRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }

        public IEnumerable<Rel_Rubros_Solicitudes_Nuevas> GetRubrosByFKIdSolicitud(int idsol)
        {
            IEnumerable<Rel_Rubros_Solicitudes_Nuevas> domains = _unitOfWork.Db.Rel_Rubros_Solicitudes_Nuevas.Where(x =>
                                                        x.id_Solicitud == idsol
                                                        );

            return domains;
        }

    }
}
