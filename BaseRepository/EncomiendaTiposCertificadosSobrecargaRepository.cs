using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class EncomiendaTiposCertificadosSobrecargaRepository : BaseRepository<Encomienda_Tipos_Certificados_Sobrecarga> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public EncomiendaTiposCertificadosSobrecargaRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
    }
}

