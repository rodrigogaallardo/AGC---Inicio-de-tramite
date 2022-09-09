using Dal.UnitOfWork;
using DataAcess;
using DataAcess.EntityCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data;
using System.Data.Entity.Core.Objects;

namespace BaseRepository
{
    public class SSIT_Listado_ProfesionalesRepository : BaseRepository<SSIT_Listado_Profesionales_Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SSIT_Listado_ProfesionalesRepository(IUnitOfWork unit)
                : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }


        public List<SSIT_Listado_Profesionales_Result> GetProfesionalesSolicitud(int BusCircuito)
        {
            ObjectResult<SSIT_Listado_Profesionales_Result> listaPrf = _unitOfWork.Db.SSIT_Listado_Profesionales(BusCircuito);
            return listaPrf.ToList();

        }
    }
}
