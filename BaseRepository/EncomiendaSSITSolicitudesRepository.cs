using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class EncomiendaSSITSolicitudesRepository : BaseRepository<Encomienda_SSIT_Solicitudes> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public EncomiendaSSITSolicitudesRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdEncomienda"></param>
		/// <returns></returns>	
        public IEnumerable<Encomienda_SSIT_Solicitudes> GetByFKIdEncomienda(int IdEncomienda)
		{
            IEnumerable<Encomienda_SSIT_Solicitudes> domains = (from ep in _unitOfWork.Db.Encomienda_SSIT_Solicitudes
                                                      where ep.id_encomienda == IdEncomienda
                                                      select ep
                                                    );
	
			return domains;
		}

        public IEnumerable<Encomienda_SSIT_Solicitudes> GetByFKIdSolicitud(int IdSolicitud)
        {
            IEnumerable<Encomienda_SSIT_Solicitudes> domains = (from ep in _unitOfWork.Db.Encomienda_SSIT_Solicitudes
                                                                where ep.id_solicitud == IdSolicitud
                                                                select ep
                                                    );

            return domains;
        }
        
        public bool existe(int id_solicitud, int id_encomienda)
        {
            IEnumerable<Encomienda_SSIT_Solicitudes> domains = _unitOfWork.Db.Encomienda_SSIT_Solicitudes.Where(x =>
                                                        x.id_encomienda == id_encomienda && x.id_solicitud== id_solicitud
                                                        );

            return domains.Count() > 0;
        }

    }
}

