using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using System.Reflection;
using StaticClass;

namespace BaseRepository
{
    public class SSITSolicitudesTitularesPersonasFisicasRepository : BaseRepository<SSIT_Solicitudes_Titulares_PersonasFisicas>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SSITSolicitudesTitularesPersonasFisicasRepository(IUnitOfWork unit)
            : base(unit)
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
        public IEnumerable<SSIT_Solicitudes_Titulares_PersonasFisicas> GetByFKIdSolicitud(int IdSolicitud)
        {
            IEnumerable<SSIT_Solicitudes_Titulares_PersonasFisicas> domains = _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasFisicas.Where(x =>
                                                        x.id_solicitud == IdSolicitud
                                                        );

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_solicitud"></param>
        /// <param name="Cuit"></param>
        /// <param name="IdPersonaFisica"></param>
        /// <returns></returns>
        public IEnumerable<SSIT_Solicitudes_Titulares_PersonasFisicas> GetByIdSolicitudCuitIdPersonaFisica(int id_solicitud, string Cuit, int IdPersonaFisica)
        {
            IEnumerable<SSIT_Solicitudes_Titulares_PersonasFisicas> domains = (from tpf in _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasFisicas
                                                                               where tpf.id_solicitud == id_solicitud
                                                                               && tpf.id_personafisica != IdPersonaFisica
                                                                               && tpf.Cuit == Cuit
                                                                               select tpf);
            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_solicitud"></param>
        /// <param name="IdPersonaFisica"></param>
        /// <returns></returns>
        public IEnumerable<SSIT_Solicitudes_Titulares_PersonasFisicas> GetByIdSolicitudIdPersonaFisica(int id_solicitud, int IdPersonaFisica)
        {
            IEnumerable<SSIT_Solicitudes_Titulares_PersonasFisicas> domains = (from tpf in _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasFisicas
                                                                               where tpf.id_solicitud == id_solicitud
                                                                               && tpf.id_personafisica == IdPersonaFisica
                                                                               select tpf);
            return domains;
        }


       

    }
}

