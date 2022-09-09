using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class ConsultaPadronPlantasRepository : BaseRepository<CPadron_Plantas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public ConsultaPadronPlantasRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdConsultaPadron"></param>
		/// <returns></returns>	
		public IEnumerable<CPadron_Plantas> GetByFKIdConsultaPadron(int IdConsultaPadron)
		{
			IEnumerable<CPadron_Plantas> domains = _unitOfWork.Db.CPadron_Plantas.Where(x => 													
														x.id_cpadron == IdConsultaPadron											
														);
	
			return domains;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoSector"></param>
		/// <returns></returns>	
		public IEnumerable<CPadron_Plantas> GetByFKIdTipoSector(int IdTipoSector)
		{
			IEnumerable<CPadron_Plantas> domains = _unitOfWork.Db.CPadron_Plantas.Where(x => 													
														x.id_tiposector == IdTipoSector											
														);
	
			return domains;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public IEnumerable<ConsultaPadronPlantasEntity> Get(int IdSolicitud)
        {
            var q = (from tipsec in _unitOfWork.Db.TipoSector
                     join caaplan in _unitOfWork.Db.CPadron_Plantas.Where(x => x.id_cpadron == IdSolicitud)
                     on tipsec.Id equals caaplan.id_tiposector into res
                     from caaplan_Empty in res.DefaultIfEmpty()
                     where
                        (
                          (tipsec.Ocultar == null || tipsec.Ocultar.Value == false) || (tipsec.Ocultar == true && caaplan_Empty.id_tiposector > 0)
                        )
                     orderby tipsec.Id   //Muy importante este orden - no cambiar, alteraria la funcionalidad
                     select new ConsultaPadronPlantasEntity()
                     {
                         IdConsultaPadronTipoSector = caaplan_Empty.id_cpadrontiposector,
                         IdTipoSector = tipsec.Id,
                         Seleccionado = (caaplan_Empty.id_tiposector != null ? caaplan_Empty.id_tiposector > 0 : false),
                         Descripcion = tipsec.Descripcion,
                         MuestraCampoAdicional = (tipsec.MuestraCampoAdicional.HasValue ? tipsec.MuestraCampoAdicional.Value : false),
                         Detalle = string.IsNullOrEmpty(caaplan_Empty.detalle_cpadrontiposector) ? "" : caaplan_Empty.detalle_cpadrontiposector,
                         TamanoCampoAdicional = (tipsec.TamanoCampoAdicional.HasValue ? tipsec.TamanoCampoAdicional.Value : 0),
                         Ocultar = tipsec.Ocultar
                     });
            return q;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdConsultaPadronTipoSector"></param>
        /// <returns></returns>
        public string Exists(int IdConsultaPadronTipoSector)
        {         
            return (from local in _unitOfWork.Db.CPadron_ConformacionLocal 
                        join tipo in _unitOfWork.Db.TipoDestino  on local.id_destino equals tipo.Id 
                        where local.id_cpadrontiposector == IdConsultaPadronTipoSector
                        select tipo.Nombre  
                        ).FirstOrDefault();
        }
	}
}

