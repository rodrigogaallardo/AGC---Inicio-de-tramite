using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using System.Data.Entity.Core.Objects;
using StaticClass;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class SSITSolicitudesNuevasRepository : BaseRepository<SSIT_Solicitudes_Nuevas>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SSITSolicitudesNuevasRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }
      
        public IEnumerable<SSIT_Solicitudes_Nuevas> GetByFKIdEstado(int IdEstado)
        {
            IEnumerable<SSIT_Solicitudes_Nuevas> domains = _unitOfWork.Db.SSIT_Solicitudes_Nuevas.Where(x =>
                                                        x.id_estado == IdEstado
                                                        );

            return domains;
        }



        public IEnumerable<SSIT_Solicitudes_Nuevas> GetByFKIdSolicitud(int IdSolicitud)
        {
            IEnumerable<SSIT_Solicitudes_Nuevas> domains = _unitOfWork.Db.SSIT_Solicitudes_Nuevas.Where(x =>
                                                        x.id_solicitud == IdSolicitud
                                                        );

            return domains;
        }

        
    }
}

