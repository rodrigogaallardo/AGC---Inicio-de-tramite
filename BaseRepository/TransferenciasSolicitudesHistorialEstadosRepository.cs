using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class TransferenciasSolicitudesHistorialEstadosRepository : BaseRepository<Transf_Solicitudes_HistorialEstados>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransferenciasSolicitudesHistorialEstadosRepository(IUnitOfWork unit) : base(unit)
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
        public IEnumerable<TransferenciasSolicitudesHistorialEstadosGrillaEntity> GetByFKIdSolicitudGrilla(int id_solicitud)
        {
            var historiales = (from hist in _unitOfWork.Db.Transf_Solicitudes_HistorialEstados
                               join est in _unitOfWork.Db.TipoEstadoSolicitud on new { Cod_estado_nuevo = hist.cod_estado_nuevo } equals new { Cod_estado_nuevo = est.Nombre }
                               where
                                 hist.id_solicitud == id_solicitud
                               select new TransferenciasSolicitudesHistorialEstadosGrillaEntity
                               {
                                   id_solhistest = hist.id_solhistest,
                                   fecha = hist.fecha_modificacion,
                                   Estado = est.Descripcion,
                                   IdEstado = est.Id
                               });
            return historiales;
        }
    }
}
