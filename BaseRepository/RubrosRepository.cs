using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;
using StaticClass;

namespace BaseRepository
{
    /// <summary>
    /// Representa a la entidad rubros del Schema DTO
    /// </summary>
    public class RubrosRepository : BaseRepository<Rubros>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RubrosRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }

        /// <summary>
        /// Get all Filtrered 
        /// </summary>
        /// <param name="esAnterior">parametro Es anterior, es opcional</param>
        /// <returns>lista de Rubros que correspondan con el filtrado de los parametros pasados</returns>
        public IEnumerable<Rubros> GetAllByParam(bool esAnterior)
        {
            var lstRubrosTemp = _unitOfWork.Db.Rubros.Where(x => x.EsAnterior_Rubro == esAnterior);
            List<Rubros> lstRubros = new List<Rubros>();
            return lstRubrosTemp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdRubro"></param>
        /// <param name="Superficie"></param>
        /// <returns></returns>
        public int ImpactoAmbiental(int IdRubro, decimal Superficie)
        {
            var query = (from rub in _unitOfWork.Db.Rubros
                         join rel in _unitOfWork.Db.Rel_Rubros_ImpactoAmbiental on rub.id_rubro equals rel.id_rubro
                         where Superficie >= rel.DesdeM2 && Superficie <= rel.HastaM2
                         && rel.id_rubro == IdRubro
                         select rel.id_ImpactoAmbiental).FirstOrDefault();
            return query;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdRubros"></param>
        /// <returns></returns>
        public int TipoDocumentoRequeridoMayor(int[] IdRubros)
        {
            var query = (from rub in _unitOfWork.Db.Rubros
                         join rel in _unitOfWork.Db.Tipo_Documentacion_Req on rub.id_tipodocreq equals rel.Id
                         where IdRubros.Contains(rub.id_rubro)
                         select rel.Id).Max();
            return query;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RubrosEntity> GetRubros(string CodZona, decimal Superficie, string CodigoRubro, bool TieneNormativa, int IdTipoTramite)
        {
            var codZonaHab = CodZona;
            var zonaHab = _unitOfWork.Db.Rel_ZonasPlaneamiento_ZonasHabilitaciones.Where(x => x.CodZonaLey449 == CodZona).FirstOrDefault();
            if (zonaHab != null)
                codZonaHab = zonaHab.CodZonaHab;

            if (IdTipoTramite == (int)Constantes.TipoDeTramite.RedistribucionDeUso || IdTipoTramite == (int)Constantes.TipoDeTramite.Ampliacion)
            {
                var resolution = (from rub in _unitOfWork.Db.Rubros
                                  join act in _unitOfWork.Db.TipoActividad on rub.id_tipoactividad equals act.Id
                                  join tdoc in _unitOfWork.Db.Tipo_Documentacion_Req on rub.id_tipodocreq equals tdoc.Id
                                  // join rubzc in _unitOfWork.Db.RubrosZonasCondiciones on rub.cod_rubro equals rubzc.cod_rubro
                                  // into gj
                                  //  from subRub in gj.Where(x => x.cod_ZonaHab.Equals(codZonaHab)).DefaultIfEmpty()
                                  //  join rubCond in _unitOfWork.Db.RubrosCondiciones on subRub.cod_condicion equals rubCond.cod_condicion
                                  //  into sd
                                  // from subRubCond in sd.Where(x => x.SupMin_condicion <= Superficie && x.SupMax_condicion >= Superficie).DefaultIfEmpty()
                                  where
                                   (rub.cod_rubro.Contains(CodigoRubro) || rub.nom_rubro.Contains(CodigoRubro))
                                  && (!rub.VigenciaHasta_rubro.HasValue || rub.VigenciaHasta_rubro > DateTime.Now)
                                  //&& (IdTipoTramite == (int)StaticClass.Constantes.TipoTramite.REDISTRIBUCION_USO || !rub.EsAnterior_Rubro)
                                  orderby rub.cod_rubro
                                  select new RubrosEntity()
                                  {
                                      Codigo = rub.cod_rubro,
                                      Nombre = rub.nom_rubro,
                                      TipoActividadNombre = act.Descripcion,
                                      IdTipoActividad = act.Id,
                                      RestriccionZona = "tilde.png", //subRub.cod_ZonaHab.Equals(codZonaHab) ? "tilde.png" : "Prohibido.png",
                                      RestriccionSup = "pregunta.png",//(subRub.cod_ZonaHab == null) ? "pregunta.png" : (subRubCond.SupMax_condicion > 0 && subRubCond.SupMin_condicion >= 0) ? "tilde.png" : "Prohibido.png",
                                      IdTipoDocumentorRequerido = rub.id_tipodocreq,
                                      EsAnterior = rub.EsAnterior_Rubro,
                                      Superficie = Superficie,
                                      TieneNormativa = TieneNormativa,
                                      IdTipoTramite = IdTipoTramite,
                                      IdRubro = rub.id_rubro,
                                      Ley105 = rub.ley105,
                                      LocalVenta = rub.local_venta,
                                      PregAntenaEmisora = rub.PregAntenaEmisora,
                                      SoloAPRA = rub.SoloAPRA,
                                      Tooltip = rub.tooltip_rubro,
                                      VigenciaDesde = rub.VigenciaDesde_rubro,
                                      VigenciaHasta = rub.VigenciaHasta_rubro
                                  }).ToList();

                resolution.Where(x => x.Codigo == "801060" && x.TieneNormativa == false)
                .Select(rub => { rub.RestriccionZona = "Prohibido.png"; rub.RestriccionSup = "Prohibido.png"; return rub; })
                .ToList();
                return resolution;
            }
            else
            {
                var resolution = (from rub in _unitOfWork.Db.Rubros
                                  join act in _unitOfWork.Db.TipoActividad on rub.id_tipoactividad equals act.Id
                                  join tdoc in _unitOfWork.Db.Tipo_Documentacion_Req on rub.id_tipodocreq equals tdoc.Id
                                  join rubzc in _unitOfWork.Db.RubrosZonasCondiciones on rub.cod_rubro equals rubzc.cod_rubro
                                  into gj
                                  from subRub in gj.Where(x => x.cod_ZonaHab.Equals(codZonaHab)).DefaultIfEmpty()
                                  join rubCond in _unitOfWork.Db.RubrosCondiciones on subRub.cod_condicion equals rubCond.cod_condicion
                                  into sd
                                  from subRubCond in sd.Where(x => x.SupMin_condicion <= Superficie && x.SupMax_condicion >= Superficie).DefaultIfEmpty()
                                  where
                                   (rub.cod_rubro.Contains(CodigoRubro) || rub.nom_rubro.Contains(CodigoRubro))
                                  && (!rub.VigenciaHasta_rubro.HasValue || rub.VigenciaHasta_rubro > DateTime.Now)
                                  && (IdTipoTramite == (int)StaticClass.Constantes.TipoTramite.REDISTRIBUCION_USO || !rub.EsAnterior_Rubro)
                                  orderby rub.cod_rubro
                                  select new RubrosEntity()
                                  {
                                      Codigo = rub.cod_rubro,
                                      Nombre = rub.nom_rubro,
                                      TipoActividadNombre = act.Descripcion,
                                      IdTipoActividad = act.Id,
                                      RestriccionZona = subRub.cod_ZonaHab.Equals(codZonaHab) ? "tilde.png" : "Prohibido.png",
                                      RestriccionSup = (subRub.cod_ZonaHab == null) ? "pregunta.png" : (subRubCond.SupMax_condicion > 0 && subRubCond.SupMin_condicion >= 0) ? "tilde.png" : "Prohibido.png",
                                      IdTipoDocumentorRequerido = rub.id_tipodocreq,
                                      EsAnterior = rub.EsAnterior_Rubro,
                                      Superficie = Superficie,
                                      TieneNormativa = TieneNormativa,
                                      IdTipoTramite = IdTipoTramite,
                                      IdRubro = rub.id_rubro,
                                      Ley105 = rub.ley105,
                                      LocalVenta = rub.local_venta,
                                      PregAntenaEmisora = rub.PregAntenaEmisora,
                                      SoloAPRA = rub.SoloAPRA,
                                      Tooltip = rub.tooltip_rubro,
                                      VigenciaDesde = rub.VigenciaDesde_rubro,
                                      VigenciaHasta = rub.VigenciaHasta_rubro
                                  }).ToList();

                resolution.Where(x => x.Codigo == "801060" && x.TieneNormativa == false)
                .Select(rub => { rub.RestriccionZona = "Prohibido.png"; rub.RestriccionSup = "Prohibido.png"; return rub; })
                .ToList();
                return resolution;
            }
        }


        public IEnumerable<RubrosEntity> GetRubrosHistoricos(string CodZona, decimal Superficie, string CodigoRubro, bool TieneNormativa, int IdTipoTramite)
        {

            var codZonaHab = CodZona;
            var zonaHab = _unitOfWork.Db.Rel_ZonasPlaneamiento_ZonasHabilitaciones.Where(x => x.CodZonaLey449 == CodZona).FirstOrDefault();
            if (zonaHab != null)
                codZonaHab = zonaHab.CodZonaHab;

            var resolution = (from rub in _unitOfWork.Db.Rubros
                              join act in _unitOfWork.Db.TipoActividad on rub.id_tipoactividad equals act.Id
                              join tdoc in _unitOfWork.Db.Tipo_Documentacion_Req on rub.id_tipodocreq equals tdoc.Id
                              join rubzc in _unitOfWork.Db.RubrosZonasCondiciones on rub.cod_rubro equals rubzc.cod_rubro
                              into gj
                              from subRub in gj.Where(x => x.cod_ZonaHab.Equals(codZonaHab)).DefaultIfEmpty()
                              join rubCond in _unitOfWork.Db.RubrosCondiciones on subRub.cod_condicion equals rubCond.cod_condicion
                              into sd
                              from subRubCond in sd.Where(x => x.SupMin_condicion <= Superficie && x.SupMax_condicion >= Superficie).DefaultIfEmpty()
                              where
                               (rub.cod_rubro.Contains(CodigoRubro) || rub.nom_rubro.Contains(CodigoRubro))
                              orderby rub.cod_rubro
                              select new RubrosEntity()
                              {
                                  Codigo = rub.cod_rubro,
                                  Nombre = rub.nom_rubro,
                                  TipoActividadNombre = act.Descripcion,
                                  IdTipoActividad = act.Id,
                                  RestriccionZona = subRub.cod_ZonaHab.Equals(codZonaHab) ? "tilde.png" : "Prohibido.png",
                                  RestriccionSup = (subRub.cod_ZonaHab == null) ? "pregunta.png" : (subRubCond.SupMax_condicion > 0 && subRubCond.SupMin_condicion >= 0) ? "tilde.png" : "Prohibido.png",
                                  IdTipoDocumentorRequerido = rub.id_tipodocreq,
                                  EsAnterior = rub.EsAnterior_Rubro,
                                  Superficie = Superficie,
                                  TieneNormativa = TieneNormativa,
                                  IdTipoTramite = IdTipoTramite,
                                  IdRubro = rub.id_rubro,
                                  Ley105 = rub.ley105,
                                  LocalVenta = rub.local_venta,
                                  PregAntenaEmisora = rub.PregAntenaEmisora,
                                  SoloAPRA = rub.SoloAPRA,
                                  Tooltip = rub.tooltip_rubro,
                                  VigenciaDesde = rub.VigenciaDesde_rubro,
                                  VigenciaHasta = rub.VigenciaHasta_rubro
                              });
            return resolution;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public IEnumerable<Rubros> Get(string Codigo)
        {
            var resolution = (from rub in _unitOfWork.Db.Rubros
                              where rub.cod_rubro == Codigo
                              select rub);
            return resolution;

        }

        public IEnumerable<Rubros> GetByListCodigo(List<string> lstcod_rubro)
        {
            var domains = _unitOfWork.Db.Rubros.Where(x => lstcod_rubro.Contains(x.cod_rubro));

            return domains;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_encomienda"></param>
        /// <returns></returns>
        public IEnumerable<Rubros_Config_Incendio> getRubrosIncendioEncomienda(int id_encomienda)
        {
            IEnumerable<Rubros_Config_Incendio> domains = (from rubInc in _unitOfWork.Db.Rubros_Config_Incendio
                                                           join rubro in _unitOfWork.Db.Rubros on rubInc.id_rubro equals rubro.id_rubro
                                                           join encrub in _unitOfWork.Db.Encomienda_Rubros on rubro.cod_rubro equals encrub.cod_rubro
                                                           where encrub.id_encomienda == id_encomienda
                                                           select rubInc);
            return domains;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>
        public IEnumerable<Rubros> GetRubrosByIdEncomienda(int IdEncomienda)
        {
            var domains = (from EncRub in _unitOfWork.Db.Encomienda_Rubros
                           join Rub in _unitOfWork.Db.Rubros on EncRub.cod_rubro equals Rub.cod_rubro
                           where EncRub.id_encomienda == IdEncomienda
                           select Rub);
            return domains;
        }

        public List<string> GetInfoAdicionalRubros(List<string> lstCodRubros)
        {
            var domains = (from rub in _unitOfWork.Db.Rubros
                           join inf in _unitOfWork.Db.Rubros_InformacionRelevante on rub.id_rubro equals inf.id_rubro
                           where lstCodRubros.Contains(rub.cod_rubro)
                           select inf.descripcion_rubinf).Distinct().ToList();

            return domains;
        }

        public IEnumerable<Rubros> GetRubrosAnterioresByIdEncomienda(int IdEncomienda)
        {
            var domains = (from EncRub in _unitOfWork.Db.Encomienda_Rubros_AT_Anterior
                           join Rub in _unitOfWork.Db.Rubros on EncRub.cod_rubro equals Rub.cod_rubro
                           where EncRub.id_encomienda == IdEncomienda
                           select Rub);
            return domains;
        }

        public bool ValidarSuperficie(string CodigoRubro, string CodZonaPla, decimal Superficie)
        {

            bool ret = false;

            string codZonaHab = null;
            var zonaHab = _unitOfWork.Db.Rel_ZonasPlaneamiento_ZonasHabilitaciones.Where(x => x.CodZonaLey449 == CodZonaPla).FirstOrDefault();
            if (zonaHab != null)
                codZonaHab = zonaHab.CodZonaHab;

            ret = (from rub in _unitOfWork.Db.Rubros
                   join act in _unitOfWork.Db.TipoActividad on rub.id_tipoactividad equals act.Id
                   join tdoc in _unitOfWork.Db.Tipo_Documentacion_Req on rub.id_tipodocreq equals tdoc.Id
                   join rubzc in _unitOfWork.Db.RubrosZonasCondiciones on new { A = rub.cod_rubro, B = codZonaHab } equals new { A = rubzc.cod_rubro, B = rubzc.cod_ZonaHab }
                   join rubCond in _unitOfWork.Db.RubrosCondiciones on rubzc.cod_condicion equals rubCond.cod_condicion
                   where
                       rub.cod_rubro == CodigoRubro
                       && ((rubCond.SupMin_condicion <= Superficie && rubCond.SupMax_condicion >= Superficie)
                           || rub.EsAnterior_Rubro)
                   select rub).Count() > 0;

            return ret;
        }

        public bool CategorizaciónDeImpacto(int cod_rubro)
        {
            var domains = (from rel in _unitOfWork.Db.Rel_Rubros_ImpactoAmbiental
                           where rel.id_rubro == cod_rubro
                           select rel.id_rubro).ToList();

            return domains.Count() > 0;
        }

        public bool GetRubrosZonasCondiciones(string CodigoRubro, string zonaDeclarada)
        {
            return (from rubzc in _unitOfWork.Db.RubrosZonasCondiciones
                    where rubzc.cod_rubro == CodigoRubro && rubzc.cod_ZonaHab == zonaDeclarada
                    select rubzc).Count() > 0;
        }
    }
}
