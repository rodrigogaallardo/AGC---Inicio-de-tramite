using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class ConsultaPadronRubrosRepository : BaseRepository<CPadron_Rubros> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public ConsultaPadronRubrosRepository(IUnitOfWork unit) : base(unit)
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
		public IEnumerable<CPadron_Rubros> GetByFKIdConsultaPadron(int IdConsultaPadron)
		{
			IEnumerable<CPadron_Rubros> domains = _unitOfWork.Db.CPadron_Rubros.Where(x => 													
														x.id_cpadron == IdConsultaPadron											
														);
	
			return domains;	
		}
		
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public IEnumerable<ConsultaPadronRubrosEntity> GetRubros(int IdConsultaPadron)
        {
            var resolution =
                            from sol in _unitOfWork.Db.CPadron_Solicitudes
                            join crub in _unitOfWork.Db.CPadron_Rubros on sol.id_cpadron equals crub.id_cpadron
                            join act in _unitOfWork.Db.TipoActividad on crub.id_tipoactividad equals act.Id
                            join tdoc in _unitOfWork.Db.Tipo_Documentacion_Req on crub.id_tipodocreq equals tdoc.Id
                            join rel in _unitOfWork.Db.Rel_ZonasPlaneamiento_ZonasHabilitaciones on sol.ZonaDeclarada equals rel.CodZonaLey449
                            into rz
                            from relzona in rz.DefaultIfEmpty()
                            join rubzc in _unitOfWork.Db.RubrosZonasCondiciones on crub.cod_rubro equals rubzc.cod_rubro
                            into gj
                            from subRub in gj.Where(x => x.cod_ZonaHab.Equals(relzona.CodZonaHab)).DefaultIfEmpty()
                            join rubCond in _unitOfWork.Db.RubrosCondiciones on subRub.cod_condicion equals rubCond.cod_condicion
                             into sd
                            from subRubCond in sd.Where(x => x.SupMin_condicion <= crub.SuperficieHabilitar && x.SupMax_condicion >= crub.SuperficieHabilitar).DefaultIfEmpty()
                            join rub in _unitOfWork.Db.Rubros on crub.cod_rubro equals rub.cod_rubro
                            into ruba
                            from subruba in ruba.Where(x => !x.VigenciaHasta_rubro.HasValue || x.VigenciaHasta_rubro > DateTime.Now).DefaultIfEmpty()
                            where
                            sol.id_cpadron == IdConsultaPadron
                            select new ConsultaPadronRubrosEntity()
                            {
                                IdConsultaPadronRubro = crub.id_cpadronrubro,
                                CodidoRubro = crub.cod_rubro,
                                DescripcionRubro = crub.desc_rubro,
                                SuperficieHabilitar = crub.SuperficieHabilitar,
                                TipoActividadNombre = act.Descripcion,
                                IdTipoActividad = act.Id,
                                IdTipoDocumentoReq = crub.id_tipodocreq,
                                IdConsultaPadron = crub.id_cpadron,
                                IdImpactoAmbiental = crub.id_ImpactoAmbiental,
                                CreateDate = crub.CreateDate,
                                EsAnterior = crub.EsAnterior,
                                TipoDocumentoDescripcion = tdoc.Descripcion,
                                LocalVenta = subruba.local_venta,//Este campo no se puede mapear, es una asociasion por codigo de rubro
                                RestriccionZona = subruba.cod_rubro != null ? subRub.cod_ZonaHab.Equals(relzona.CodZonaHab) ? "tilde.png" : "Prohibido.png" : "",
                                RestriccionSup = subruba.cod_rubro != null ? (subRub.cod_ZonaHab == null) ? "pregunta.png" : (subRubCond.SupMax_condicion > 0 && subRubCond.SupMin_condicion >= 0) ? "tilde.png" : "Prohibido.png" : "",
                                DocRequerida = crub.Tipo_Documentacion_Req.Nomenclatura 
                            };
            return resolution;
        }
	}
}

