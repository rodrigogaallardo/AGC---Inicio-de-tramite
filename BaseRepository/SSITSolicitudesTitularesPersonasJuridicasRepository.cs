using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using StaticClass;

namespace BaseRepository
{
    public class SSITSolicitudesTitularesPersonasJuridicasRepository : BaseRepository<SSIT_Solicitudes_Titulares_PersonasJuridicas>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SSITSolicitudesTitularesPersonasJuridicasRepository(IUnitOfWork unit)
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
        public IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas> GetByFKIdSolicitud(int IdSolicitud)
        {
            IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas> domains = _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasJuridicas.Where(x =>
                                                        x.id_solicitud == IdSolicitud
                                                        );

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTipoSociedad"></param>
        /// <returns></returns>	
        public IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas> GetByFKIdTipoSociedad(int IdTipoSociedad)
        {
            IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas> domains = _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasJuridicas.Where(x =>
                                                        x.Id_TipoSociedad == IdTipoSociedad
                                                        );

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTipoiibb"></param>
        /// <returns></returns>	
        public IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas> GetByFKIdTipoiibb(int IdTipoiibb)
        {
            IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas> domains = _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasJuridicas.Where(x =>
                                                        x.id_tipoiibb == IdTipoiibb
                                                        );

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdLocalidad"></param>
        /// <returns></returns>	
        public IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas> GetByFKIdLocalidad(int IdLocalidad)
        {
            IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas> domains = _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasJuridicas.Where(x =>
                                                        x.id_localidad == IdLocalidad
                                                        );

            return domains;
        }

        public IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas> GetByIdSolicitudCuitIdPersonaJuridica(int id_solicitud, string Cuit, int IdPersonaJuridica)
        {
            IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas> domains = _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasJuridicas.Where(x =>
                                                                                    x.id_solicitud == id_solicitud
                                                                                    && x.id_personajuridica != IdPersonaJuridica
                                                                                    && x.CUIT == Cuit);
            return domains;
        }
        public IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas> GetByIdSolicitudIdPersonaJuridica(int id_solicitud, int IdPersonaJuridica)
        {
            IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas> domains = _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasJuridicas.Where(x =>
                                                                                    x.id_solicitud == id_solicitud
                                                                                    && x.id_personajuridica == IdPersonaJuridica
                                                                                    );
            return domains;
        }

    }
}

