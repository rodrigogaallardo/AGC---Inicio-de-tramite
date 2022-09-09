using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class ProfesionalHistorialRepository : BaseRepository<Profesional_historial>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProfesionalHistorialRepository(IUnitOfWork unit)
                : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }
        public IEnumerable<Profesional_historial> Get(int id_profesional)
        {
            return _unitOfWork.Db.Profesional_historial.Where(x => x.Id_profesional == id_profesional);            
        }
    }
}
