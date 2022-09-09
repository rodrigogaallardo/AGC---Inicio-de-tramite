using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class TransferenciasPlantasRepository : BaseRepository<Transf_Plantas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public TransferenciasPlantasRepository(IUnitOfWork unit) : base(unit)
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
		public IEnumerable<Transf_Plantas> GetByFKIdSolicitud(int IdSolicitud)
		{
			IEnumerable<Transf_Plantas> domains = _unitOfWork.Db.Transf_Plantas.Where(x => 													
														x.id_solicitud == IdSolicitud
                                                        );
	
			return domains;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoSector"></param>
		/// <returns></returns>	
		public IEnumerable<Transf_Plantas> GetByFKIdTipoSector(int IdTipoSector)
		{
			IEnumerable<Transf_Plantas> domains = _unitOfWork.Db.Transf_Plantas.Where(x => 													
														x.id_tiposector == IdTipoSector											
														);
	
			return domains;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public IEnumerable<TransferenciasPlantasEntity> Get(int IdSolicitud)
        {
            var q = (from tipsec in _unitOfWork.Db.TipoSector
                     join tplan in _unitOfWork.Db.Transf_Plantas.Where(x => x.id_solicitud == IdSolicitud)
                     on tipsec.Id equals tplan.id_tiposector into res
                     from tplan_Empty in res.DefaultIfEmpty()
                     where
                        (
                          (tipsec.Ocultar == null || tipsec.Ocultar.Value == false) || (tipsec.Ocultar == true && tplan_Empty.id_tiposector > 0)
                        )
                     orderby tipsec.Id   //Muy importante este orden - no cambiar, alteraria la funcionalidad
                     select new TransferenciasPlantasEntity()
                     {
                         IdTransferenciaTipoSector = tplan_Empty.id_transftiposector,
                         IdTipoSector = tipsec.Id,
                         Seleccionado = (tplan_Empty.id_tiposector != null ? tplan_Empty.id_tiposector > 0 : false),
                         Descripcion = tipsec.Descripcion,
                         MuestraCampoAdicional = (tipsec.MuestraCampoAdicional.HasValue ? tipsec.MuestraCampoAdicional.Value : false),
                         Detalle = string.IsNullOrEmpty(tplan_Empty.detalle_transftiposector) ? "" : tplan_Empty.detalle_transftiposector,
                         TamanoCampoAdicional = (tipsec.TamanoCampoAdicional.HasValue ? tipsec.TamanoCampoAdicional.Value : 0),
                         Ocultar = tipsec.Ocultar
                     });
            return q;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTransferenciaTipoSector"></param>
        /// <returns></returns>
        public string Exists(int IdTransferenciaTipoSector)
        {         
            return (from local in _unitOfWork.Db.Transf_ConformacionLocal 
                        join tipo in _unitOfWork.Db.TipoDestino  on local.id_destino equals tipo.Id 
                        where local.id_transftiposector == IdTransferenciaTipoSector
                    select tipo.Nombre  
                        ).FirstOrDefault();
        }
	}
}

