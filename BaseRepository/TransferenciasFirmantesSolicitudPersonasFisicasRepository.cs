using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class TransferenciasFirmantesSolicitudPersonasFisicasRepository : BaseRepository<Transf_Firmantes_Solicitud_PersonasFisicas> 
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransferenciasFirmantesSolicitudPersonasFisicasRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>	
        public IEnumerable<Transf_Firmantes_Solicitud_PersonasFisicas> GetByFKIdSolicitud(int IdSolicitud)
        {
            IEnumerable<Transf_Firmantes_Solicitud_PersonasFisicas> domains = _unitOfWork.Db.Transf_Firmantes_Solicitud_PersonasFisicas.Where(x =>
                                                        x.id_solicitud == IdSolicitud
                                                        );

            return domains;
        }
        public IEnumerable<Transf_Firmantes_Solicitud_PersonasFisicas> GetByFKIdPersonaFisica(int IdPersonaFisica)
        {
            IEnumerable<Transf_Firmantes_Solicitud_PersonasFisicas> domains = _unitOfWork.Db.Transf_Firmantes_Solicitud_PersonasFisicas.Where(x =>
                                                        x.id_personafisica == IdPersonaFisica
                                                        );

            return domains;
        }
        public IEnumerable<Transf_Firmantes_Solicitud_PersonasFisicas> GetByFKIdSolicitudIdPersonaFisica(int IdSolicitud, int IdPersonaFisica)
        {
            IEnumerable<Transf_Firmantes_Solicitud_PersonasFisicas> domains = _unitOfWork.Db.Transf_Firmantes_Solicitud_PersonasFisicas.Where(x =>
                                                        x.id_solicitud == IdSolicitud
                                                        && x.id_personafisica == IdPersonaFisica
                                                        );

            return domains;
        }
    }
}
