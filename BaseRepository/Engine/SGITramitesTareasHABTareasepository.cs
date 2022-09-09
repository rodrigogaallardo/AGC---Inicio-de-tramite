using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using IBaseRepository;
using Dal.UnitOfWork;

namespace BaseRepository.Engine
{
    public class SGITramitesTareasHABTareasepository : BaseRepository<SGI_Tramites_Tareas_HAB>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SGITramitesTareasHABTareasepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }


       
    }

}

