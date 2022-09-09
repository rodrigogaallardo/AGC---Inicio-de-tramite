using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;
using StaticClass;

namespace BaseRepository
{
    public class EncomiendaRubrosRepository : BaseRepository<Encomienda_Rubros> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public EncomiendaRubrosRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdEncomienda"></param>
		/// <returns></returns>	
		public IEnumerable<Encomienda_Rubros> GetByFKIdEncomienda(int IdEncomienda)
		{
			IEnumerable<Encomienda_Rubros> domains = _unitOfWork.Db.Encomienda_Rubros.Where(x => 													
														x.id_encomienda == IdEncomienda											
														);
	
			return domains;
		}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>
        public IEnumerable<RubrosTipoDocReqEntity> GetRubrosTdoReqByIdEncomienda(int IdEncomienda)
        {
            try
            {
                int? id_solicitud = (from enc in _unitOfWork.Db.Encomienda
                                     join encSol in _unitOfWork.Db.Encomienda_SSIT_Solicitudes on enc.id_encomienda equals encSol.id_encomienda
                                    where enc.id_encomienda == IdEncomienda
                                    select encSol.id_solicitud).FirstOrDefault();
                
                List<int> lstDocEnc = (from enc in _unitOfWork.Db.Encomienda
                                       join encDocAdj in _unitOfWork.Db.Encomienda_DocumentosAdjuntos on enc.id_encomienda equals encDocAdj.id_encomienda
                                       join encSol in _unitOfWork.Db.Encomienda_SSIT_Solicitudes on enc.id_encomienda equals encSol.id_encomienda
                                       where encSol.id_solicitud == id_solicitud.Value
                                            && enc.id_estado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo
                                       select encDocAdj.id_tdocreq).ToList();

                List<int> lstDocSol = _unitOfWork.Db.SSIT_DocumentosAdjuntos.Where(x => x.id_solicitud == id_solicitud).Select(s => s.id_tdocreq).ToList();

                List<int> lstDocObs = (from tt in _unitOfWork.Db.SGI_Tramites_Tareas
                                       join tth in _unitOfWork.Db.SGI_Tramites_Tareas_HAB on tt.id_tramitetarea equals tth.id_tramitetarea
                                       join tcg in _unitOfWork.Db.SGI_Tarea_Calificar_ObsGrupo on tt.id_tramitetarea equals tcg.id_tramitetarea
                                       join tco in _unitOfWork.Db.SGI_Tarea_Calificar_ObsDocs on tcg.id_ObsGrupo equals tco.id_ObsGrupo
                                       where tth.id_solicitud == id_solicitud && tco.id_file != null
                                       select tco.id_tdocreq).ToList();

                List<int> lstTdoReq = new List<int>();
                lstTdoReq.AddRange(lstDocEnc);
                lstTdoReq.AddRange(lstDocSol);
                lstTdoReq.AddRange(lstDocObs);

                var domains = (from rub in _unitOfWork.Db.Rubros
                               join encrub in _unitOfWork.Db.Encomienda_Rubros on rub.cod_rubro equals encrub.cod_rubro
                               join rubtdr in _unitOfWork.Db.Rubros_TiposDeDocumentosRequeridos on rub.id_rubro equals rubtdr.id_rubro
                               join tdr in _unitOfWork.Db.TiposDeDocumentosRequeridos on rubtdr.id_tdocreq equals tdr.id_tdocreq
                               where encrub.id_encomienda == IdEncomienda 
                                        && !lstTdoReq.Contains(tdr.id_tdocreq)
                                        && rubtdr.obligatorio_rubtdocreq == true
                               select new RubrosTipoDocReqEntity
                               {
                                   Id = tdr.id_tdocreq,
                                   NombreDoc = tdr.nombre_tdocreq,
                                   CodRubro = ""
                               })
                            .Distinct()
                            .ToList();

                foreach (var item in domains)
                {
                    item.CodRubro = string.Join(", ",
                                                    (from encRub in _unitOfWork.Db.Encomienda_Rubros
                                                     join rub in _unitOfWork.Db.Rubros on encRub.cod_rubro equals rub.cod_rubro
                                                     join rubTdr in _unitOfWork.Db.Rubros_TiposDeDocumentosRequeridos on rub.id_rubro equals rubTdr.id_rubro
                                                     join tdr in _unitOfWork.Db.TiposDeDocumentosRequeridos on rubTdr.id_tdocreq equals tdr.id_tdocreq
                                                     where encRub.id_encomienda == IdEncomienda && tdr.id_tdocreq == item.Id
                                                     select encRub.cod_rubro).ToArray().Distinct()
                                                       );
                }

                return domains;
            }
            catch
            {
                throw;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>
        public IEnumerable<Rubros> GetRubrosByIdEncomienda(int IdEncomienda)
        {
            var domains = (from encRubros in _unitOfWork.Db.Encomienda_Rubros
                           join rub in _unitOfWork.Db.Rubros on encRubros.cod_rubro equals rub.cod_rubro
                           where encRubros.id_encomienda == IdEncomienda 
                           select rub);

            return domains;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <param name="CodigoRubro"></param>
        /// <returns></returns>
        public double? GetByFKIdEncomiendaCodigo(int IdEncomienda)
        {
            return (from encrub in _unitOfWork.Db.Encomienda_Rubros
                    join rub in _unitOfWork.Db.Rubros on encrub.cod_rubro equals rub.cod_rubro
                    where
                    encrub.id_encomienda == IdEncomienda
                    select rub.local_venta).Min();            
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="IdTipoActividad"></param>
        ///// <returns></returns>	
        //public IEnumerable<Encomienda_Rubros> GetByFKIdTipoActividad(int IdTipoActividad)
        //{
        //    IEnumerable<Encomienda_Rubros> domains = _unitOfWork.Db.Encomienda_Rubros.Where(x => 													
        //                                                x.id_tipoactividad == IdTipoActividad											
        //                                                );
	
        //    return domains;
        //}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoDocumentoRequerido"></param>
		/// <returns></returns>	
        //public IEnumerable<Encomienda_Rubros> GetByFKIdTipoDocumentoRequerido(int IdTipoDocumentoRequerido)
        //{
        //    IEnumerable<Encomienda_Rubros> domains = _unitOfWork.Db.Encomienda_Rubros.Where(x =>
        //                                                x.id_tipodocreq == IdTipoDocumentoRequerido
        //                                                );

        //    return domains;
        //}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdImpactoAmbiental"></param>
		/// <returns></returns>	
        //public IEnumerable<Encomienda_Rubros> GetByFKIdImpactoAmbiental(int IdImpactoAmbiental)
        //{
        //    IEnumerable<Encomienda_Rubros> domains = _unitOfWork.Db.Encomienda_Rubros.Where(x => 													
        //                                                x.id_ImpactoAmbiental == IdImpactoAmbiental											
        //                                                );
	
        //    return domains;
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>
        public IEnumerable<EncomiendaRubrosEntity> GetRubros(int IdEncomienda)
        {
            var CodZona = "";
            var zonaPla = _unitOfWork.Db.Encomienda.Where(x => x.id_encomienda == IdEncomienda).FirstOrDefault().ZonaDeclarada;
            if (!string.IsNullOrEmpty(zonaPla))
            {
                CodZona = (from rel in _unitOfWork.Db.Rel_ZonasPlaneamiento_ZonasHabilitaciones
                           where rel.CodZonaLey449 == zonaPla
                           select rel.CodZonaHab).FirstOrDefault();
            }
            else
                CodZona = (from encubic in _unitOfWork.Db.Encomienda_Ubicaciones
                         join enc in _unitOfWork.Db.Encomienda on encubic.id_encomienda equals enc.id_encomienda
                         join zona in _unitOfWork.Db.Zonas_Planeamiento on encubic.id_zonaplaneamiento equals zona.id_zonaplaneamiento
                         join rel in _unitOfWork.Db.Rel_ZonasPlaneamiento_ZonasHabilitaciones on zona.CodZonaPla equals rel.CodZonaLey449
                         into codigo
                         from sd in codigo.Where(x => x.CodZonaLey449 == zona.CodZonaPla).DefaultIfEmpty()
                         where enc.id_encomienda == IdEncomienda
                         select sd == null ? zona.CodZonaPla : codigo.FirstOrDefault().CodZonaHab).FirstOrDefault();

            var resolution =
                            from enc in _unitOfWork.Db.Encomienda
                            join ubicrub in _unitOfWork.Db.Encomienda_Rubros on enc.id_encomienda equals ubicrub.id_encomienda
                            join act in _unitOfWork.Db.TipoActividad on ubicrub.id_tipoactividad equals act.Id
                            join tdoc in _unitOfWork.Db.Tipo_Documentacion_Req on ubicrub.id_tipodocreq equals tdoc.Id
                            join rubzc in _unitOfWork.Db.RubrosZonasCondiciones on ubicrub.cod_rubro equals rubzc.cod_rubro
                            into gj
                            from subRub in gj.Where(x => x.cod_ZonaHab.Equals(CodZona)).DefaultIfEmpty()
                            join rubCond in _unitOfWork.Db.RubrosCondiciones on subRub.cod_condicion equals rubCond.cod_condicion
                             into sd
                            from subRubCond in sd.Where(x => x.SupMin_condicion <= ubicrub.SuperficieHabilitar && x.SupMax_condicion >= ubicrub.SuperficieHabilitar).DefaultIfEmpty()
                            join rub in _unitOfWork.Db.Rubros on ubicrub.cod_rubro equals rub.cod_rubro
                            into ruba from subruba in ruba.Where(x =>  !x.VigenciaHasta_rubro.HasValue || x.VigenciaHasta_rubro > DateTime.Now).DefaultIfEmpty()
                            where
                            enc.id_encomienda == IdEncomienda
                            select new EncomiendaRubrosEntity()
                            {
                                IdEncomiendaRubro = ubicrub.id_encomiendarubro,
                                CodigoRubro = ubicrub.cod_rubro,
                                DescripcionRubro = ubicrub.desc_rubro,
                                SuperficieHabilitar = ubicrub.SuperficieHabilitar,
                                TipoActividadNombre = act.Descripcion,
                                IdTipoActividad = act.Id,
                                IdTipoDocumentoRequerido = ubicrub.id_tipodocreq,
                                IdEncomienda = ubicrub.id_encomienda,
                                IdImpactoAmbiental = ubicrub.id_ImpactoAmbiental,
                                CreateDate = ubicrub.CreateDate,
                                EsAnterior = ubicrub.EsAnterior,
                                TipoDocumentoDescripcion = tdoc.Descripcion,
                                LocalVenta = subruba.local_venta,
                                RestriccionZona = subRub.cod_ZonaHab.Contains(CodZona) ? "tilde.png" : "Prohibido.png",
                                RestriccionSup = subruba.cod_rubro != null ? (subRub.cod_ZonaHab == null) ? "pregunta.png" : (subRubCond.SupMax_condicion > 0 && subRubCond.SupMin_condicion >= 0) ? "tilde.png" : "Prohibido.png" : "",
                                TieneDeposito = subruba.TieneDeposito,
                                SupMinCargaDescarga = subruba.SupMinCargaDescarga,
                                SupMinCargaDescargaRefII = subruba.SupMinCargaDescargaRefII,
                                SupMinCargaDescargaRefV = subruba.SupMinCargaDescargaRefV,
                                OficinaComercial = subruba.OficinaComercial
                             };
            return resolution;
        }

        public IEnumerable<EncomiendaRubrosATAnteriorEntity> GetRubrosATAnterior(int IdEncomienda)
        {
            var CodZona = "";
            var zonaPla = _unitOfWork.Db.Encomienda.Where(x => x.id_encomienda == IdEncomienda).FirstOrDefault().ZonaDeclarada;
            if (!string.IsNullOrEmpty(zonaPla))
            {
                CodZona = (from rel in _unitOfWork.Db.Rel_ZonasPlaneamiento_ZonasHabilitaciones
                           where rel.CodZonaLey449 == zonaPla
                           select rel.CodZonaHab).FirstOrDefault();
            }
            else
                CodZona = (from encubic in _unitOfWork.Db.Encomienda_Ubicaciones
                           join enc in _unitOfWork.Db.Encomienda on encubic.id_encomienda equals enc.id_encomienda
                           join zona in _unitOfWork.Db.Zonas_Planeamiento on encubic.id_zonaplaneamiento equals zona.id_zonaplaneamiento
                           join rel in _unitOfWork.Db.Rel_ZonasPlaneamiento_ZonasHabilitaciones on zona.CodZonaPla equals rel.CodZonaLey449
                           into codigo
                           from sd in codigo.Where(x => x.CodZonaLey449 == zona.CodZonaPla).DefaultIfEmpty()
                           where enc.id_encomienda == IdEncomienda
                           select sd == null ? zona.CodZonaPla : codigo.FirstOrDefault().CodZonaHab).FirstOrDefault();

            var resolution =
                            from enc in _unitOfWork.Db.Encomienda
                            join ubicrub in _unitOfWork.Db.Encomienda_Rubros_AT_Anterior on enc.id_encomienda equals ubicrub.id_encomienda
                            join act in _unitOfWork.Db.TipoActividad on ubicrub.id_tipoactividad equals act.Id
                            join tdoc in _unitOfWork.Db.Tipo_Documentacion_Req on ubicrub.id_tipodocreq equals tdoc.Id
                            join rubzc in _unitOfWork.Db.RubrosZonasCondiciones on ubicrub.cod_rubro equals rubzc.cod_rubro
                            into gj
                            from subRub in gj.Where(x => x.cod_ZonaHab.Equals(CodZona)).DefaultIfEmpty()
                            join rubCond in _unitOfWork.Db.RubrosCondiciones on subRub.cod_condicion equals rubCond.cod_condicion
                             into sd
                            from subRubCond in sd.Where(x => x.SupMin_condicion <= ubicrub.SuperficieHabilitar && x.SupMax_condicion >= ubicrub.SuperficieHabilitar).DefaultIfEmpty()
                            join rub in _unitOfWork.Db.Rubros on ubicrub.cod_rubro equals rub.cod_rubro
                            into ruba
                            from subruba in ruba.Where(x => !x.VigenciaHasta_rubro.HasValue || x.VigenciaHasta_rubro > DateTime.Now).DefaultIfEmpty()
                            where
                            enc.id_encomienda == IdEncomienda
                            select new EncomiendaRubrosATAnteriorEntity()
                            {
                                IdEncomiendaRubro = ubicrub.id_encomiendarubro,
                                CodigoRubro = ubicrub.cod_rubro,
                                DescripcionRubro = ubicrub.desc_rubro,
                                SuperficieHabilitar = ubicrub.SuperficieHabilitar,
                                TipoActividadNombre = act.Descripcion,
                                IdTipoActividad = act.Id,
                                IdTipoDocumentoRequerido = ubicrub.id_tipodocreq,
                                IdEncomienda = ubicrub.id_encomienda,
                                IdImpactoAmbiental = ubicrub.id_ImpactoAmbiental,
                                CreateDate = ubicrub.CreateDate,
                                EsAnterior = ubicrub.EsAnterior,
                                TipoDocumentoDescripcion = tdoc.Descripcion,
                                LocalVenta = subruba.local_venta,
                                RestriccionZona = subRub.cod_ZonaHab.Contains(CodZona) ? "tilde.png" : "Prohibido.png",
                                RestriccionSup = subruba.cod_rubro != null ? (subRub.cod_ZonaHab == null) ? "pregunta.png" : (subRubCond.SupMax_condicion > 0 && subRubCond.SupMin_condicion >= 0) ? "tilde.png" : "Prohibido.png" : "",
                                TieneDeposito = subruba.TieneDeposito,
                                SupMinCargaDescarga = subruba.SupMinCargaDescarga,
                                SupMinCargaDescargaRefII = subruba.SupMinCargaDescargaRefII,
                                SupMinCargaDescargaRefV = subruba.SupMinCargaDescargaRefV,
                                OficinaComercial = subruba.OficinaComercial
                            };
            return resolution;
        }

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>
        public IEnumerable<EncomiendaRubrosEntity> GetRubros(List<int> IdEncomienda)
        {
            var CodZona = (from encubic in _unitOfWork.Db.Encomienda_Ubicaciones
                           join enc in _unitOfWork.Db.Encomienda on encubic.id_encomienda equals enc.id_encomienda
                           join zona in _unitOfWork.Db.Zonas_Planeamiento on encubic.id_zonaplaneamiento equals zona.id_zonaplaneamiento
                           join rel in _unitOfWork.Db.Rel_ZonasPlaneamiento_ZonasHabilitaciones on zona.CodZonaPla equals rel.CodZonaLey449
                           into codigo
                           from sd in codigo.Where(x => x.CodZonaLey449 == zona.CodZonaPla).DefaultIfEmpty()
                           where IdEncomienda.Contains(enc.id_encomienda)
                           select sd == null ? zona.CodZonaPla : codigo.FirstOrDefault().CodZonaHab).FirstOrDefault();

            var resolution =
                            from enc in _unitOfWork.Db.Encomienda
                            join ubicrub in _unitOfWork.Db.Encomienda_Rubros on enc.id_encomienda equals ubicrub.id_encomienda
                            join act in _unitOfWork.Db.TipoActividad on ubicrub.id_tipoactividad equals act.Id
                            join tdoc in _unitOfWork.Db.Tipo_Documentacion_Req on ubicrub.id_tipodocreq equals tdoc.Id
                            join rubzc in _unitOfWork.Db.RubrosZonasCondiciones on ubicrub.cod_rubro equals rubzc.cod_rubro
                            into gj
                            from subRub in gj.Where(x => x.cod_ZonaHab.Equals(CodZona)).DefaultIfEmpty()
                            join rubCond in _unitOfWork.Db.RubrosCondiciones on subRub.cod_condicion equals rubCond.cod_condicion
                             into sd
                            from subRubCond in sd.Where(x => x.SupMin_condicion <= ubicrub.SuperficieHabilitar && x.SupMax_condicion >= ubicrub.SuperficieHabilitar).DefaultIfEmpty()
                            join rub in _unitOfWork.Db.Rubros on ubicrub.cod_rubro equals rub.cod_rubro
                            into ruba
                            from subruba in ruba.Where(x => !x.VigenciaHasta_rubro.HasValue || x.VigenciaHasta_rubro > DateTime.Now).DefaultIfEmpty()
                            where
                            IdEncomienda.Contains(enc.id_encomienda)
                            && !ubicrub.EsAnterior
                            select new EncomiendaRubrosEntity()
                            {
                                IdEncomiendaRubro = ubicrub.id_encomiendarubro,
                                CodigoRubro = ubicrub.cod_rubro,
                                DescripcionRubro = ubicrub.desc_rubro,
                                SuperficieHabilitar = ubicrub.SuperficieHabilitar,
                                TipoActividadNombre = act.Descripcion,
                                IdTipoActividad = act.Id,
                                IdTipoDocumentoRequerido = ubicrub.id_tipodocreq,
                                IdEncomienda = ubicrub.id_encomienda,
                                IdImpactoAmbiental = ubicrub.id_ImpactoAmbiental,
                                CreateDate = ubicrub.CreateDate,
                                EsAnterior = ubicrub.EsAnterior,
                                TipoDocumentoDescripcion = tdoc.Descripcion,
                                LocalVenta = subruba.local_venta,
                                RestriccionZona = subRub.cod_ZonaHab.Contains(CodZona) ? "tilde.png" : "Prohibido.png",
                                RestriccionSup = subruba.cod_rubro != null ? (subRub.cod_ZonaHab == null) ? "pregunta.png" : (subRubCond.SupMax_condicion > 0 && subRubCond.SupMin_condicion >= 0) ? "tilde.png" : "Prohibido.png" : "",
                            };
            return resolution;
        }
	}
}

