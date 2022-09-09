using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class SGITramitesTareasCpadronRepository : BaseRepository<SGI_Tramites_Tareas_CPADRON>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SGITramitesTareasCpadronRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }

        public IEnumerable<SGI_Tramites_Tareas_CPADRON> GetByFKIdTramiteTareasIdSolicitud(int id_tramitetarea, int id_cpadron)
        {
            var domains = _unitOfWork.Db.SGI_Tramites_Tareas_CPADRON.Where(x => x.id_tramitetarea == id_tramitetarea && x.id_cpadron == id_cpadron);
            return domains;
        }
            
    }
}

