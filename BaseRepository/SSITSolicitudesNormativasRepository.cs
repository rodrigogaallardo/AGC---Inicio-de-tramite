using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using Dal.UnitOfWork;


namespace BaseRepository
{
    public class SSITSolicitudesNormativasRepository : BaseRepository<SSIT_Solicitudes_Normativas>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SSITSolicitudesNormativasRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }

        public IEnumerable<SSIT_Solicitudes_Normativas> GetByFKIdSolicitud(int IdSolicitud)
        {
            IEnumerable<SSIT_Solicitudes_Normativas> domains = _unitOfWork.Db.SSIT_Solicitudes_Normativas.Where(x =>
                                                        x.IdSolicitud == IdSolicitud
                                                        );

            return domains;
        }

        public IEnumerable<SSIT_Solicitudes_Normativas> GetNormativas(int IdSolicitud)
        {
            var domains = (from query in _unitOfWork.Db.SSIT_Solicitudes_Normativas
                           join tn in _unitOfWork.Db.TipoNormativa on query.id_tiponormativa equals tn.Id
                           join en in _unitOfWork.Db.EntidadNormativa on query.id_entidadnormativa equals en.Id
                           where query.IdSolicitud == IdSolicitud
                           select query
                            );

            return domains;
        }

    }
}
