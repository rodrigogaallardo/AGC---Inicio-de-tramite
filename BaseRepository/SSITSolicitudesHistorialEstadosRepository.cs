using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class SSITSolicitudesHistorialEstadosRepository : BaseRepository<SSIT_Solicitudes_HistorialEstados>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SSITSolicitudesHistorialEstadosRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_solicitud"></param>
        /// <returns></returns>
        public IEnumerable<SSITSolicitudesHistorialEstadosGrillaEntity> GetByFKIdSolicitudGrilla(int id_solicitud)
        {
            var historiales = (from hist in _unitOfWork.Db.SSIT_Solicitudes_HistorialEstados
                               join est in _unitOfWork.Db.TipoEstadoSolicitud on new { Cod_estado_nuevo = hist.cod_estado_nuevo } equals new { Cod_estado_nuevo = est.Nombre }
                               where
                                 hist.id_solicitud == id_solicitud
                               select new SSITSolicitudesHistorialEstadosGrillaEntity
                               {
                                   id_solhistest = hist.id_solhistest,
                                   fecha = hist.fecha_modificacion,
                                   Estado = est.Descripcion,
                                   IdEstado = est.Id
                               });
            return historiales;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public IEnumerable<SSIT_Solicitudes_HistorialEstados> GetByFKIdSolicitud(int IdSolicitud)
        {
            var historiales = (from hist in _unitOfWork.Db.SSIT_Solicitudes_HistorialEstados
                               where hist.id_solicitud == IdSolicitud
                               select hist);

            return historiales;
        }
    }
}
