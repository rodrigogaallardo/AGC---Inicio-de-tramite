using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class SSITSolicitudesPagosRepository : BaseRepository<SSIT_Solicitudes_Pagos> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public SSITSolicitudesPagosRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }

        public IEnumerable<SSIT_Solicitudes_Pagos> GetByFKIdSolicitud(int id_solicitud)
        {
            IEnumerable<SSIT_Solicitudes_Pagos> domains = _unitOfWork.Db.SSIT_Solicitudes_Pagos.Where(x =>
                                                        x.id_solicitud == id_solicitud
                                                        );

            return domains;
        }

        public IEnumerable<clsItemGrillaPagosEntity> GetGrillaByFKIdSolicitud(int id_solicitud)
        {
            var lstPagos = (from solpag in _unitOfWork.Db.SSIT_Solicitudes_Pagos
                            join pagos in _unitOfWork.Db.wsPagos on solpag.id_pago equals pagos.id_pago
                            join bu in _unitOfWork.Db.wsPagos_BoletaUnica on pagos.id_pago equals bu.id_pago
                            join bue in _unitOfWork.Db.wsPagos_BoletaUnica_Estados on bu.EstadoPago_BU equals bue.id_estadopago
                            where solpag.id_solicitud == id_solicitud
                            select new clsItemGrillaPagosEntity
                        {
                            id_sol_pago = solpag.id_sol_pago,
                            id_solicitud = solpag.id_solicitud,
                            id_pago = solpag.id_pago,
                            id_medio_pago = 0,
                            monto_pago = solpag.monto_pago,
                            CreateDate = solpag.CreateDate,
                            desc_medio_pago = "Boleta única",
                            desc_estado_pago = bue.nom_estadopago,
                            id_estado_pago = bue.id_estadopago
                        });

            return lstPagos;
        }
    }
}

