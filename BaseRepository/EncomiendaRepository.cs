using Dal.UnitOfWork;
using DataAcess;
using DataAcess.EntityCustom;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using static StaticClass.Constantes;

namespace BaseRepository
{
    public class EncomiendaRepository : BaseRepository<Encomienda>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EncomiendaRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }

        public List<int> GetEstadosByUserId(Guid userid)
        {
            var domains = _unitOfWork.Db.Encomienda.Where(x => x.CreateUser == userid).Select(s => s.id_estado).ToList();
            return domains;
        }
        public IEnumerable<TramitesEntity> GetByTramiteEstado(Guid UserId)
        {
            var ret = (from enc in _unitOfWork.Db.Encomienda
                       join est in _unitOfWork.Db.Encomienda_Estados on enc.id_estado equals est.id_estado
                       join tcer in _unitOfWork.Db.TipoExpediente on enc.id_tipoexpediente equals tcer.id_tipoexpediente
                       join ttra in _unitOfWork.Db.TipoTramite on enc.id_tipotramite equals ttra.id_tipotramite
                       join encsol in _unitOfWork.Db.Encomienda_SSIT_Solicitudes on enc.id_encomienda equals encsol.id_encomienda
                       join sol in _unitOfWork.Db.SSIT_Solicitudes on encsol.id_solicitud equals sol.id_solicitud
                       where enc.CreateUser == UserId
                       select new TramitesEntity
                       {
                           IdTramite = sol.id_solicitud,
                           CodigoSeguridad = sol.CodigoSeguridad,
                           CreateDate = enc.CreateDate,
                           IdTipoTramite = enc.id_tipotramite,
                           TipoTramiteDescripcion = (
                                    !(bool)sol.EsECI
                                    ? sol.TipoTramite.descripcion_tipotramite
                                    : (sol.id_tipotramite == (int)StaticClass.Constantes.TipoTramite.HabilitacionECIHabilitacion ? TipoTramiteDescripcion.HabilitacionECI : TipoTramiteDescripcion.AdecuacionECI)
                                    ),
                           TipoExpedienteDescripcion = enc.TipoExpediente.descripcion_tipoexpediente,
                           IdEstado = enc.id_estado,
                           EstadoDescripcion = enc.Encomienda_Estados.nom_estado,
                           TipoExpediente = enc.id_tipoexpediente,
                           SubTipoExpediente = enc.id_subtipoexpediente,
                           SubTipoExpedienteDescripcion = enc.SubtipoExpediente.descripcion_subtipoexpediente,
                           Domicilio = "",
                           Url = "",
                           NroExpedienteSade = sol.NroExpedienteSade,
                           id_encomienda = enc.id_encomienda
                       }
           ).Union(from enc in _unitOfWork.Db.Encomienda
                   join est in _unitOfWork.Db.Encomienda_Estados on enc.id_estado equals est.id_estado
                   join tcer in _unitOfWork.Db.TipoExpediente on enc.id_tipoexpediente equals tcer.id_tipoexpediente
                   join ttra in _unitOfWork.Db.TipoTramite on enc.id_tipotramite equals ttra.id_tipotramite
                   join encsol in _unitOfWork.Db.Encomienda_Transf_Solicitudes on enc.id_encomienda equals encsol.id_encomienda
                   join tramite in _unitOfWork.Db.Transf_Solicitudes on encsol.id_solicitud equals tramite.id_solicitud
                   where enc.CreateUser == UserId
                   select new TramitesEntity
                   {
                       IdTramite = tramite.id_solicitud,
                       CodigoSeguridad = tramite.CodigoSeguridad,
                       CreateDate = enc.CreateDate,
                       IdTipoTramite = enc.id_tipotramite,
                       TipoTramiteDescripcion = tramite.TipoTramite.descripcion_tipotramite,
                       TipoExpedienteDescripcion = enc.TipoExpediente.descripcion_tipoexpediente,
                       IdEstado = enc.id_estado,
                       EstadoDescripcion = enc.Encomienda_Estados.nom_estado,
                       TipoExpediente = enc.id_tipoexpediente,
                       SubTipoExpediente = enc.id_subtipoexpediente,
                       SubTipoExpedienteDescripcion = enc.SubtipoExpediente.descripcion_subtipoexpediente,
                       Domicilio = "",
                       Url = "",
                       NroExpedienteSade = tramite.NroExpedienteSade,
                       id_encomienda = enc.id_encomienda
                   }
           );
            return ret;
        }

        public int GetMaxNumeroEncomiendaConsejo(int id_consejo)
        {
            var domains = (from enc in _unitOfWork.Db.Encomienda
                           where enc.id_consejo == id_consejo
                           orderby enc.nroEncomiendaconsejo descending
                           select enc.nroEncomiendaconsejo)
                           .FirstOrDefault();

            int ret = 1;

            if (domains == 0)
                return ret;
            else
                return domains + 1;
        }

        public IEnumerable<Encomienda> GetByFKListIdEncomienda(List<int> list)
        {
            IEnumerable<Encomienda> domains = _unitOfWork.Db.Encomienda.Where(x => list.Contains(x.id_encomienda));
            return domains;
        }

        public DateTime GetFechaCertificacion(int id_encomienda)
        {
            DateTime max = (from hist in _unitOfWork.Db.Encomienda_HistorialEstados
                            join est in _unitOfWork.Db.Encomienda_Estados on hist.cod_estado_nuevo equals est.cod_estado
                            where hist.id_encomienda == id_encomienda && est.id_estado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo
                            orderby hist.id_enchistest descending
                            select hist.fecha_modificacion).FirstOrDefault();
            return max;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public IEnumerable<Encomienda> GetByFKIdSolicitud(int IdSolicitud)
        {
            var ret = (from enc in _unitOfWork.Db.Encomienda
                       join encSol in _unitOfWork.Db.Encomienda_SSIT_Solicitudes on enc.id_encomienda equals encSol.id_encomienda
                       where encSol.id_solicitud == IdSolicitud
                       select enc);
            return ret;
        }

        public IEnumerable<Encomienda> GetByFKIdSolicitudTransf(int IdSolicitud)
        {
            var ret = (from enc in _unitOfWork.Db.Encomienda
                       join encSol in _unitOfWork.Db.Encomienda_Transf_Solicitudes on enc.id_encomienda equals encSol.id_encomienda
                       where encSol.id_solicitud == IdSolicitud
                       select enc);
            return ret;
        }

        public IQueryable<Encomienda> GetUltimaEncomiendaAprobada(int IdSolicitud)
        {
            var ret = (from enc in _unitOfWork.Db.Encomienda
                       join encSol in _unitOfWork.Db.Encomienda_SSIT_Solicitudes on enc.id_encomienda equals encSol.id_encomienda
                       where encSol.id_solicitud == IdSolicitud && enc.id_estado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo
                       orderby enc.id_encomienda descending
                       select enc);
            return ret;
        }

        public IQueryable<Encomienda> GetUltimaEncomiendaAprobadaTR(int IdSolicitud)
        {
            var ret = (from enc in _unitOfWork.Db.Encomienda
                       join encSol in _unitOfWork.Db.Encomienda_Transf_Solicitudes on enc.id_encomienda equals encSol.id_encomienda
                       where encSol.id_solicitud == IdSolicitud && enc.id_estado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo
                       orderby enc.id_encomienda descending
                       select enc);
            return ret;
        }


        /// <summary>
        /// Compare between 2 encomiendas
        /// </summary>
        /// <param name="idEncomienda1"></param>
        /// <param name="idEncomienda2"></param>
        /// <returns></returns>
        public bool Compare(int idEncomienda1, int idEncomienda2)
        {
            try
            {

                var encomienda1 = _unitOfWork.Db.Encomienda.Where(x => x.id_encomienda == idEncomienda1).SingleOrDefault();
                if (encomienda1 == null)
                    return false;

                var encomienda2 = _unitOfWork.Db.Encomienda.Where(
                    x => x.id_encomienda == idEncomienda2
                    && x.ZonaDeclarada == encomienda1.ZonaDeclarada
                    && x.id_tipoexpediente == encomienda1.id_tipoexpediente
                    && x.id_subtipoexpediente == encomienda1.id_subtipoexpediente
                    && x.Observaciones_rubros == encomienda1.Observaciones_rubros
                    ).SingleOrDefault();

                if (encomienda2 == null)
                    return false;


                if (!CompareCantRegPersonasFisicas(encomienda1, encomienda2))
                {
                    return false;
                }

                if (!CompareCantRegPersonasJuridicas(encomienda1, encomienda2))
                {
                    return false;
                }

                if (!CompareCantRubros(encomienda1, encomienda2))
                    return false;

                //Valida personas Fisicas
                foreach (Encomienda_Titulares_PersonasFisicas pf in encomienda1.Encomienda_Titulares_PersonasFisicas)
                {
                    #region Validacion PersonaFisica
                    var encomiendaPrsonaFisica = encomienda2.Encomienda_Titulares_PersonasFisicas.Where(
                        x => (x.Apellido ?? string.Empty).Trim().ToUpper() == (pf.Apellido ?? string.Empty).Trim().ToUpper()
                            && (x.Nombres ?? string.Empty).Trim().ToUpper() == (pf.Nombres ?? string.Empty).Trim().ToUpper()
                            && x.id_tipodoc_personal == pf.id_tipodoc_personal
                            && (x.Nro_Documento ?? string.Empty).Trim() == (pf.Nro_Documento ?? string.Empty).Trim()
                            && (x.Cuit ?? string.Empty).Trim() == (pf.Cuit ?? string.Empty).Trim()
                            && x.id_tipoiibb == pf.id_tipoiibb
                            && (x.Ingresos_Brutos ?? string.Empty).Trim() == (pf.Ingresos_Brutos ?? string.Empty).Trim()
                    ).FirstOrDefault();

                    if (encomiendaPrsonaFisica == null)
                        return false;
                    #endregion Validacion PersonaFisica
                }

                //Validacion personasJuridicas
                foreach (Encomienda_Titulares_PersonasJuridicas pj in encomienda1.Encomienda_Titulares_PersonasJuridicas)
                {

                    #region Validacion PersonaJuridica
                    var encomiendaPrsonaJuridica = encomienda2.Encomienda_Titulares_PersonasJuridicas.Where(
                        x => x.id_encomienda == encomienda2.id_encomienda
                            && x.Id_TipoSociedad == pj.Id_TipoSociedad
                            && (x.Razon_Social ?? string.Empty).Trim().ToUpper() == (pj.Razon_Social ?? string.Empty).Trim().ToUpper()
                            && (x.CUIT ?? string.Empty).Trim() == (pj.CUIT ?? string.Empty).Trim()
                            && (x.Nro_IIBB ?? string.Empty).Trim() == (pj.Nro_IIBB ?? string.Empty).Trim()
                            && x.id_tipoiibb == pj.id_tipoiibb
                    ).FirstOrDefault();

                    if (encomiendaPrsonaJuridica == null)
                        return false;

                    foreach (Encomienda_Titulares_PersonasJuridicas_PersonasFisicas encomiendaJuridicaFisica in pj.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas)
                    {

                        var encomiendaTitularesFisicasJuridicas = encomiendaPrsonaJuridica.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas.Where(
                            x => (x.Apellido ?? string.Empty).Trim().ToUpper() == (encomiendaJuridicaFisica.Apellido ?? string.Empty).Trim().ToUpper()
                            && (x.Nombres ?? string.Empty).Trim().ToUpper() == (encomiendaJuridicaFisica.Nombres ?? string.Empty).Trim().ToUpper()
                            && (x.Nro_Documento ?? string.Empty).Trim() == (encomiendaJuridicaFisica.Nro_Documento ?? string.Empty).Trim()
                            && x.TipoDocumentoPersonal == encomiendaJuridicaFisica.TipoDocumentoPersonal
                            && x.firmante_misma_persona == encomiendaJuridicaFisica.firmante_misma_persona
                            ).FirstOrDefault();

                        if (encomiendaTitularesFisicasJuridicas == null)
                            return false;
                    }


                    #endregion Validacion PersonaJuridica
                }

                //control de rubros 
                foreach (Encomienda_RubrosCN rubros in encomienda1.Encomienda_RubrosCN)
                {
                    #region Validacion Rubros
                    var rubrosencomiendas = encomienda2.Encomienda_RubrosCN.Where(
                        x => x.CodigoRubro == rubros.CodigoRubro
                        //&& x.EsAnterior == rubros.EsAnterior
                        && x.NombreRubro == rubros.NombreRubro
                        && x.IdTipoActividad == rubros.IdTipoActividad
                        && x.idImpactoAmbiental == rubros.idImpactoAmbiental
                        && x.SuperficieHabilitar == rubros.SuperficieHabilitar
                        ).FirstOrDefault();

                    if (rubrosencomiendas == null)
                        return false;

                    #endregion
                }

                if (encomienda1.Encomienda_Plantas.Count != encomienda2.Encomienda_Plantas.Count)
                    return false;

                foreach (Encomienda_Plantas plantas in encomienda1.Encomienda_Plantas)
                {
                    var plantasencomiendas = encomienda2.Encomienda_Plantas.Where(
                        x => x.detalle_encomiendatiposector == plantas.detalle_encomiendatiposector
                        && x.id_tiposector == plantas.id_tiposector
                        ).FirstOrDefault();

                    if (plantasencomiendas == null)
                        return false;
                }

                var sumTotDatosLocalEncomienda1 = encomienda1.Encomienda_DatosLocal.Sum(x => x.superficie_cubierta_dl + x.superficie_descubierta_dl);
                var sumTotDatosLocalEncomienda2 = encomienda2.Encomienda_DatosLocal.Sum(x => x.superficie_cubierta_dl + x.superficie_descubierta_dl);

                if (sumTotDatosLocalEncomienda1 != sumTotDatosLocalEncomienda2)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// return compare cant of reg between encomienda
        /// </summary>
        /// <param name="idEncomienda1"></param>
        /// <param name="idEncomienda1"></param>
        /// <returns></returns>
        public bool CompareCantRegPersonasFisicas(Encomienda encomienda1, Encomienda encomienda2)
        {
            bool arEquals = true;
            try
            {
                if (encomienda1.Encomienda_Titulares_PersonasFisicas.Count != encomienda2.Encomienda_Titulares_PersonasFisicas.Count)
                    arEquals = false;

                return arEquals;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Compare cant Rubros
        /// </summary>
        /// <param name="encomienda1"></param>
        /// <param name="encomienda2"></param>
        public bool CompareCantRubros(Encomienda encomienda1, Encomienda encomienda2)
        {
            try
            {

                if (encomienda1.Encomienda_RubrosCN.Count != encomienda2.Encomienda_RubrosCN.Count)
                    return false;


                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CompareCantUbicaciones(Encomienda encomienda1, Encomienda encomienda2)
        {
            try
            {
                if (encomienda1.Encomienda_Ubicaciones.Count != encomienda2.Encomienda_Ubicaciones.Count)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ANT_Encomiendas_Estados> GetAntEstados()
        {
            return (from enc_and in _unitOfWork.Db.ANT_Encomiendas_Estados
                    select enc_and);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Encomienda> TraerEncomiendasConsejos(int id_grupoconsejo, string matricula, string Apenom, string cuit, string estados, int pIdEncomienda, int pIdEncomiendaConsejo)
        {
            var encomiendas = (from enc in _unitOfWork.Db.Encomienda
                               join con in _unitOfWork.Db.ConsejoProfesional on enc.id_consejo equals con.Id
                               join prof in _unitOfWork.Db.Profesional on enc.id_profesional equals prof.Id
                               where con.id_grupoconsejo == id_grupoconsejo
                               select enc);

            if (!string.IsNullOrEmpty(matricula))
                encomiendas = encomiendas.Where(p => p.Profesional.Matricula.Equals(matricula));
            if (!string.IsNullOrEmpty(Apenom))
                encomiendas = encomiendas.Where(p => p.Profesional.Apellido.Contains(Apenom));
            if (!string.IsNullOrEmpty(cuit))
                encomiendas = encomiendas.Where(p => p.Profesional.Cuit.Contains(cuit));
            if (pIdEncomienda > 0)
                encomiendas = encomiendas.Where(p => p.id_encomienda == pIdEncomienda);
            if (pIdEncomiendaConsejo > 0)
                encomiendas = encomiendas.Where(p => p.nroEncomiendaconsejo == pIdEncomiendaConsejo);
            if (!string.IsNullOrEmpty(estados))
            {
                int[] estadosChar = Array.ConvertAll(estados.Split(','), int.Parse);
                encomiendas = encomiendas.Where(p => estadosChar.Contains(p.id_estado));
            }

            return encomiendas.OrderByDescending(p => p.CreateDate);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_grupoconsejo"></param>
        /// <param name="matricula"></param>
        /// <param name="Apenom"></param>
        /// <param name="cuit"></param>
        /// <param name="estados"></param>
        /// <param name="tipoTramite"></param>
        /// <param name="nroTramite"></param>
        /// <param name="pIdEncomiendaConsejo"></param>
        /// <returns></returns>
        public IEnumerable<EncomiendaExt> TraerEncomiendasExConsejos(int id_grupoconsejo, string matricula, string Apenom, string cuit, string estados, int tipoTramite, int nroTramite, int pIdEncomiendaConsejo)
        {
            var encomiendas = (from enc in _unitOfWork.Db.EncomiendaExt
                               join con in _unitOfWork.Db.ConsejoProfesional on enc.id_consejo equals con.Id
                               join prof in _unitOfWork.Db.Profesional on enc.id_profesional equals prof.Id
                               join ubic in _unitOfWork.Db.EncomiendaExt_Ubicaciones on enc.id_encomienda equals ubic.id_encomienda
                               join stubic in _unitOfWork.Db.SubTiposDeUbicacion on ubic.id_subtipoubicacion equals stubic.id_subtipoubicacion
                               where con.id_grupoconsejo == id_grupoconsejo
                               && enc.TipoTramite == tipoTramite
                               select enc);

            if (!string.IsNullOrEmpty(matricula))
                encomiendas = encomiendas.Where(p => p.Profesional.Matricula.Equals(matricula));
            if (!string.IsNullOrEmpty(Apenom))
                encomiendas = encomiendas.Where(p => p.Profesional.Apellido.Contains(Apenom));
            if (!string.IsNullOrEmpty(cuit))
                encomiendas = encomiendas.Where(p => p.Profesional.Cuit.Contains(cuit));
            if (nroTramite > 0)
                encomiendas = encomiendas.Where(p => p.nroTramite == nroTramite);
            if (pIdEncomiendaConsejo > 0)
                encomiendas = encomiendas.Where(p => p.nroEncomiendaconsejo == pIdEncomiendaConsejo);

            if (!string.IsNullOrEmpty(estados))
            {
                int[] estadosChar = Array.ConvertAll(estados.Split(','), int.Parse);
                encomiendas = encomiendas.Where(p => estadosChar.Contains(p.id_estado));
            }

            return encomiendas.OrderByDescending(p => p.CreateDate);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_grupoconsejo"></param>
        /// <param name="matricula"></param>
        /// <param name="Apenom"></param>
        /// <param name="cuit"></param>
        /// <param name="id_estado"></param>
        /// <param name="nroTramite"></param>
        /// <param name="id_tipocertificado"></param>
        /// <returns></returns>
        public IEnumerable<EncomiendaExt> TraerEncomiendasDirectorObra(int BusIdGrupoConsejo, string BusMatricula, string BusApenom, string BusCuit, List<int> BusListEstados, int BusNroTramite, int BusTipoTramite)
        {

            var encomiendas = (from enc in _unitOfWork.Db.EncomiendaExt
                               join con in _unitOfWork.Db.ConsejoProfesional on enc.id_consejo equals con.Id
                               join prof in _unitOfWork.Db.Profesional on enc.id_profesional equals prof.Id
                               join ubic in _unitOfWork.Db.EncomiendaExt_Ubicaciones on enc.id_encomienda equals ubic.id_encomienda
                               where con.id_grupoconsejo == BusIdGrupoConsejo
                               && enc.TipoTramite == (int)Constantes.TipoCertificado.Ligue || enc.TipoTramite == (int)Constantes.TipoCertificado.Desligue

                               select enc);

            if (!string.IsNullOrEmpty(BusMatricula))
                encomiendas = encomiendas.Where(p => p.Profesional.Matricula.Equals(BusMatricula));

            if (!string.IsNullOrEmpty(BusApenom) && BusApenom.Length > 0)
            {
                string[] valores = BusApenom.Trim().Split(' ');
                for (int i = 0; i <= valores.Length - 1; i++)
                {
                    string valor = valores[i];
                    encomiendas = encomiendas.Where(x => x.Profesional.Apellido.Contains(valor.Trim()) || x.Profesional.Nombre.Contains(valor.Trim()));
                }
            }

            if (!string.IsNullOrEmpty(BusCuit))
                encomiendas = encomiendas.Where(p => p.Profesional.Cuit.Contains(BusCuit));

            if (BusNroTramite > 0)
                encomiendas = encomiendas.Where(p => p.nroTramite == BusNroTramite);

            if (BusListEstados.Count > 0)
                encomiendas = encomiendas.Where(p => BusListEstados.Contains(p.id_estado));

            if (BusTipoTramite > 0)
                encomiendas = encomiendas.Where(p => p.TipoTramite == BusTipoTramite);

            return encomiendas;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_encomiendaDesde"></param>
        /// <param name="id_encomiendaHasta"></param>
        /// <returns></returns>
        public List<Encomienda> GetRangoIdEncomienda(int id_encomiendaDesde, int id_encomiendaHasta)
        {
            var listIdEncomienda = _unitOfWork.Db.Encomienda_SSIT_Solicitudes.Where(x => x.id_encomienda >= id_encomiendaDesde && x.id_encomienda <= id_encomiendaHasta).Select(e => e.id_encomienda).ToList();
            return GetListaIdEncomienda(listIdEncomienda);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listIDEncomienda"></param>
        /// <returns></returns>
        public List<Encomienda> GetListaIdEncomienda(List<int> listIDEncomienda)
        {
            return _unitOfWork.Db.Encomienda.Where(x => listIDEncomienda.Contains(x.id_encomienda)).ToList();
        }

        /// <summary>
        /// compare between personasjuridicas
        /// </summary>
        /// <param name="encomienda1"></param>
        /// <param name="encomienda2"></param>
        /// <returns></returns>
        public bool CompareCantRegPersonasJuridicas(Encomienda encomienda1, Encomienda encomienda2)
        {
            bool arEquals = true;
            try
            {
                if (encomienda1.Encomienda_Titulares_PersonasJuridicas.Count != encomienda2.Encomienda_Titulares_PersonasJuridicas.Count)
                    arEquals = false;

                if (encomienda1.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas.Count != encomienda2.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas.Count)
                    arEquals = false;

                return arEquals;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Encomienda> GetByFechaCambioEstado(List<int> lstEncomiendas)
        {
            var domains = (from enc in _unitOfWork.Db.Encomienda
                           join hist in _unitOfWork.Db.Encomienda_HistorialEstados on enc.id_encomienda equals hist.id_encomienda
                           where lstEncomiendas.Contains(hist.id_encomienda)
                            && hist.cod_estado_nuevo == "APROC"
                            && hist.fecha_modificacion >= Constantes.fechaImplementacionSSPAutomaticas
                           orderby enc.id_encomienda descending
                           select enc);

            return domains;
        }

        public bool ValidarRequerimientosDocumentosRubros(int id_solicitud, List<int> listRubros)
        {
            var AdjuntosEntity = _unitOfWork.Db.RubrosCN_TiposDeDocumentosRequeridos.Where(x => listRubros.Contains(x.id_rubro) && x.obligatorio_rubtdocreq == true).Select(x => x.id_tdocreq).ToList();

            var ret = _unitOfWork.Db.SSIT_DocumentosAdjuntos.
                        Where(doc => doc.id_solicitud == id_solicitud && AdjuntosEntity.Contains(doc.id_tdocreq)).Count();
            return AdjuntosEntity.Count() == ret;
        }

        public bool esUbicacionEspecialConObjetoTerritorialByIdUbicacion(int idUbicacion)
        {

            var sbUbicacionesOT = _unitOfWork.Db.SubTiposDeUbicacion
              .Where(x => x.id_tipoubicacion == (int)Constantes.TiposDeUbicacion.ObjetoTerritorial)
              .Select(x => x.id_subtipoubicacion).ToList();

            var result = from ubi in _unitOfWork.Db.Ubicaciones
                         join sub in _unitOfWork.Db.SubTiposDeUbicacion on ubi.id_subtipoubicacion equals sub.id_subtipoubicacion
                         where (sbUbicacionesOT.Contains(sub.id_subtipoubicacion) && (ubi.id_ubicacion == idUbicacion))
                         select new
                         {
                             ubi.id_ubicacion
                         };

            return result.Count() > 0;
        }
    }
}

