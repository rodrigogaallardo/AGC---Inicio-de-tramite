using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class TransferenciasRubrosRepository : BaseRepository<Transf_Rubros> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public TransferenciasRubrosRepository(IUnitOfWork unit) : base(unit)
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
		public IEnumerable<Transf_Rubros> GetByFKIdSolicitud(int IdSolicitud)
		{
			IEnumerable<Transf_Rubros> domains = _unitOfWork.Db.Transf_Rubros.Where(x => 													
														x.id_solicitud == IdSolicitud											
														);
	
			return domains;	
		}
		
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public IEnumerable<TransferenciaRubrosEntity> GetRubros(int IdSolicitud)
        {
            var resolution =
                            from sol in _unitOfWork.Db.Transf_Solicitudes
                            join trub in _unitOfWork.Db.Transf_Rubros on sol.id_solicitud equals trub.id_solicitud
                            join act in _unitOfWork.Db.TipoActividad on trub.id_tipoactividad equals act.Id
                            join tdoc in _unitOfWork.Db.Tipo_Documentacion_Req on trub.id_tipodocreq equals tdoc.Id
                            join rel in _unitOfWork.Db.Rel_ZonasPlaneamiento_ZonasHabilitaciones on sol.ZonaDeclarada equals rel.CodZonaLey449
                            into rz
                            from relzona in rz.DefaultIfEmpty()
                            join rubzc in _unitOfWork.Db.RubrosZonasCondiciones on trub.cod_rubro equals rubzc.cod_rubro
                            into gj
                            from subRub in gj.Where(x => x.cod_ZonaHab.Equals(relzona.CodZonaHab)).DefaultIfEmpty()
                            join rubCond in _unitOfWork.Db.RubrosCondiciones on subRub.cod_condicion equals rubCond.cod_condicion
                             into sd
                            from subRubCond in sd.Where(x => x.SupMin_condicion <= trub.SuperficieHabilitar && x.SupMax_condicion >= trub.SuperficieHabilitar).DefaultIfEmpty()
                            join rub in _unitOfWork.Db.Rubros on trub.cod_rubro equals rub.cod_rubro
                            into ruba
                            from subruba in ruba.Where(x => !x.VigenciaHasta_rubro.HasValue || x.VigenciaHasta_rubro > DateTime.Now).DefaultIfEmpty()
                            where
                            sol.id_cpadron == IdSolicitud
                            select new TransferenciaRubrosEntity()
                            {
                                IdTransferenciaRubro = trub.id_transfrubro,
                                CodidoRubro = trub.cod_rubro,
                                DescripcionRubro = trub.desc_rubro,
                                SuperficieHabilitar = trub.SuperficieHabilitar,
                                TipoActividadNombre = act.Descripcion,
                                IdTipoActividad = act.Id,
                                IdTipoDocumentoReq = trub.id_tipodocreq,
                                IdSolicitud = trub.id_solicitud,
                                IdImpactoAmbiental = trub.id_ImpactoAmbiental,
                                CreateDate = trub.CreateDate,
                                EsAnterior = trub.EsAnterior,
                                TipoDocumentoDescripcion = tdoc.Descripcion,
                                LocalVenta = subruba.local_venta,//Este campo no se puede mapear, es una asociasion por codigo de rubro
                                RestriccionZona = subruba.cod_rubro != null ? subRub.cod_ZonaHab.Equals(relzona.CodZonaHab) ? "tilde.png" : "Prohibido.png" : "",
                                RestriccionSup = subruba.cod_rubro != null ? (subRub.cod_ZonaHab == null) ? "pregunta.png" : (subRubCond.SupMax_condicion > 0 && subRubCond.SupMin_condicion >= 0) ? "tilde.png" : "Prohibido.png" : "",
                                DocRequerida = trub.Tipo_Documentacion_Req.Nomenclatura 
                            };
            return resolution;
        }
	}
}

