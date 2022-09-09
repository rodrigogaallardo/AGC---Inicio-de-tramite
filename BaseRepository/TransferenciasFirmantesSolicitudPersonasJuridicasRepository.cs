using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class TransferenciasFirmantesSolicitudPersonasJuridicasRepository : BaseRepository<Transf_Firmantes_Solicitud_PersonasJuridicas>
    {
        private readonly IUnitOfWork _unitOfWork;

        public int insert(Transf_Firmantes_Solicitud_PersonasJuridicas entity)
        {
            var obj = dbSet.Add(entity);
            this._unitOfWork.Db.SaveChanges();
            return entity.id_firmante_pj;
        }
        public TransferenciasFirmantesSolicitudPersonasJuridicasRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }

        public IEnumerable<Transf_Firmantes_Solicitud_PersonasJuridicas> GetByFKIdSolicitud(int IdSolicitud)
        {
            IEnumerable<Transf_Firmantes_Solicitud_PersonasJuridicas> domains = _unitOfWork.Db.Transf_Firmantes_Solicitud_PersonasJuridicas.Where(x =>
                                                        x.id_solicitud == IdSolicitud
                                                        );

            return domains;
        }

        public IEnumerable<Transf_Firmantes_Solicitud_PersonasJuridicas> GetByFKIdSolicitudIdPersonaJuridica(int IdSolicitud, int IdPersonaJuridica)
        {
            IEnumerable<Transf_Firmantes_Solicitud_PersonasJuridicas> domains = _unitOfWork.Db.Transf_Firmantes_Solicitud_PersonasJuridicas.Where(x =>
                                                        x.id_solicitud == IdSolicitud
                                                        && x.id_personajuridica == IdPersonaJuridica);

            return domains;
        }
    }
}
