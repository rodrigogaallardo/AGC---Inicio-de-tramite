using Dal.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;

namespace BaseRepository
{
    public class UsuarioConsejoRepository : BaseRepository<Rel_Usuarios_GrupoConsejo>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioConsejoRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
    }
}
