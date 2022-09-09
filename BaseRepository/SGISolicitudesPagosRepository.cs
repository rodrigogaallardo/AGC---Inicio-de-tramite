using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;
using StaticClass;

namespace BaseRepository
{
    public class SGISolicitudesPagosRepository : BaseRepository<SGI_Solicitudes_Pagos> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public SGISolicitudesPagosRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTramiteTarea"></param>
		/// <returns></returns>	
		public IEnumerable<SGI_Solicitudes_Pagos> GetByFKIdTramiteTarea(int IdTramiteTarea)
		{
			IEnumerable<SGI_Solicitudes_Pagos> domains = _unitOfWork.Db.SGI_Solicitudes_Pagos.Where(x => 													
														x.id_tramitetarea == IdTramiteTarea											
														);
	
			return domains;	
		}
        public IEnumerable<PagosEntity> GetTransf(int IdSolicitud)
        {
            var lstPagos = (from tt in _unitOfWork.Db.SGI_Tramites_Tareas
                            join tt_tr in _unitOfWork.Db.SGI_Tramites_Tareas_TRANSF on tt.id_tramitetarea equals tt_tr.id_tramitetarea
                            join sp in _unitOfWork.Db.SGI_Solicitudes_Pagos on tt.id_tramitetarea equals sp.id_tramitetarea
                            where tt_tr.id_solicitud == IdSolicitud 
                            select new PagosEntity
                            {
                                CreateDate = sp.CreateDate,
                                desc_medio_pago = Constantes.BOLETA_UNICA,
                                id_medio_pago = Constantes.ID_BOLETA_UNICA,
                                id_pago = sp.id_pago,
                                id_sol_pago = sp.id_sol_pago,
                                id_solicitud = sp.id_tramitetarea,
                                monto_pago = sp.monto_pago                                
                            });

            return lstPagos;
        }

        public IEnumerable<PagosEntity> GetTransmisiones(int IdSolicitud)
        {
            var lstPagos = (from transfPagos in _unitOfWork.Db.Transf_Solicitudes_Pagos
                            join pagos in _unitOfWork.Db.wsPagos on transfPagos.id_pago equals pagos.id_pago
                            join bu in _unitOfWork.Db.wsPagos_BoletaUnica on pagos.id_pago equals bu.id_pago
                            join bue in _unitOfWork.Db.wsPagos_BoletaUnica_Estados on bu.EstadoPago_BU equals bue.id_estadopago
                            where transfPagos.id_solicitud == IdSolicitud
                            select new PagosEntity
                            {
                                CreateDate = transfPagos.CreateDate,
                                desc_medio_pago = Constantes.BOLETA_UNICA,
                                id_medio_pago = Constantes.ID_BOLETA_UNICA,
                                id_pago = transfPagos.id_pago,
                                id_sol_pago = transfPagos.id_sol_pago,
                                id_solicitud = transfPagos.id_solicitud,
                                monto_pago = transfPagos.monto_pago,
                                id_estado_pago = bue.id_estadopago,
                            });

            return lstPagos;
        }

        public IEnumerable<PagosEntity> GetHab(int IdSolicitud)
        {
            var lstPagos = (from tt in _unitOfWork.Db.SGI_Tramites_Tareas
                            join tt_hab in _unitOfWork.Db.SGI_Tramites_Tareas_HAB on tt.id_tramitetarea equals tt_hab.id_tramitetarea                         
                            join sp in _unitOfWork.Db.SGI_Solicitudes_Pagos on tt.id_tramitetarea equals sp.id_tramitetarea          
                            join pagos in _unitOfWork.Db.wsPagos on sp.id_pago equals pagos.id_pago
                            join bu in _unitOfWork.Db.wsPagos_BoletaUnica on pagos.id_pago equals bu.id_pago
                            join bue in _unitOfWork.Db.wsPagos_BoletaUnica_Estados on bu.EstadoPago_BU equals bue.id_estadopago
                            where tt_hab.id_solicitud == IdSolicitud 
                            select new PagosEntity
                            {
                                CreateDate = sp.CreateDate,
                                desc_medio_pago = Constantes.BOLETA_UNICA,
                                id_medio_pago = Constantes.ID_BOLETA_UNICA,
                                id_pago = sp.id_pago,
                                id_sol_pago = sp.id_sol_pago,
                                id_solicitud = tt_hab.id_solicitud,
                                monto_pago = sp.monto_pago,
                                desc_estado_pago = bue.nom_estadopago,
                                id_estado_pago = bue.id_estadopago
                            });

            return lstPagos;
        }
        public PagosEntity GetEstadoPago(int IdPago)
        {
            var domains = (from bu in _unitOfWork.Db.wsPagos_BoletaUnica
                             join bue in _unitOfWork.Db.wsPagos_BoletaUnica_Estados on bu.EstadoPago_BU equals bue.id_estadopago
                             where bu.id_pago == IdPago
                             select new PagosEntity
                             {
                                 desc_estado_pago = bue.nom_estadopago,
                                 id_estado_pago = bu.EstadoPago_BU
                             }).FirstOrDefault();
            return domains;
        }

        public int GetMaxId()
        {
            return _unitOfWork.Db.SGI_Solicitudes_Pagos.OrderByDescending(o => o.id_sol_pago).Select(s => s.id_sol_pago + 1).FirstOrDefault();
        }
	}
}

