using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class SGITramitesTareasHabRepository : BaseRepository<SGI_Tramites_Tareas_HAB>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SGITramitesTareasHabRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }
        
        public IEnumerable<SGI_Tramites_Tareas_HAB> GetByFKIdTramiteTareasIdSolicitud(int id_tramitetarea, int id_solicitud)
        {
            var domains = _unitOfWork.Db.SGI_Tramites_Tareas_HAB.Where(x => x.id_tramitetarea == id_tramitetarea && x.id_solicitud == id_solicitud);
            return domains;
        }
            
    }
}

