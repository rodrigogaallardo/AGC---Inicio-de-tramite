using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using System.Data.Entity.Core.Objects;
using StaticClass;
using DataAcess.EntityCustom;
using System.Data.Entity;

namespace BaseRepository
{
    public class SSITSolicitudesRepository : BaseRepository<SSIT_Solicitudes>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SSITSolicitudesRepository(IUnitOfWork unit)
            : base(unit)
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
        public SSIT_Solicitudes GetByFKIdEncomienda(int IdEncomienda)
        {
            return (from sol in _unitOfWork.Db.SSIT_Solicitudes
                    join encSol in _unitOfWork.Db.Encomienda_SSIT_Solicitudes on sol.id_solicitud equals encSol.id_solicitud
                    join enc in _unitOfWork.Db.Encomienda on encSol.id_encomienda equals enc.id_encomienda
                    where enc.id_encomienda == IdEncomienda
                    select sol).FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTipoTramite"></param>
        /// <returns></returns>	
        public IEnumerable<SSIT_Solicitudes> GetByFKIdTipoTramite(int IdTipoTramite)
        {
            IEnumerable<SSIT_Solicitudes> domains = _unitOfWork.Db.SSIT_Solicitudes.Where(x =>
                                                        x.id_tipotramite == IdTipoTramite
                                                        );

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTipoExpediente"></param>
        /// <returns></returns>	
        public IEnumerable<SSIT_Solicitudes> GetByFKIdTipoExpediente(int IdTipoExpediente)
        {
            IEnumerable<SSIT_Solicitudes> domains = _unitOfWork.Db.SSIT_Solicitudes.Where(x =>
                                                        x.id_tipoexpediente == IdTipoExpediente
                                                        );

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSubtipoExpediente"></param>
        /// <returns></returns>	
        public IEnumerable<SSIT_Solicitudes> GetByFKIdSubtipoExpediente(int IdSubtipoExpediente)
        {
            IEnumerable<SSIT_Solicitudes> domains = _unitOfWork.Db.SSIT_Solicitudes.Where(x =>
                                                        x.id_subtipoexpediente == IdSubtipoExpediente
                                                        );

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEstado"></param>
        /// <returns></returns>	
        public IEnumerable<SSIT_Solicitudes> GetByFKIdEstado(int IdEstado)
        {
            IEnumerable<SSIT_Solicitudes> domains = _unitOfWork.Db.SSIT_Solicitudes.Where(x =>
                                                        x.id_estado == IdEstado
                                                        );

            return domains;
        }

        //TODO: COrregir estos metodos 
        public SSIT_Solicitudes GetAnteriorByFKIdEncomienda(int id_encomienda)
        {
            //var domains = (from sol in _unitOfWork.Db.SSIT_Solicitudes
            //               join encAnt in _unitOfWork.Db.Encomienda on sol.id_encomienda equals encAnt.id_encomienda
            //               join rel in _unitOfWork.Db.Rel_Encomienda_Rectificatoria on encAnt.id_encomienda equals rel.id_encomienda_anterior
            //               where rel.id_encomienda_nueva == id_encomienda
            //               && sol.id_estado != 20 //ANU 
            //               && sol.id_estado != 24 //VENCIDA
            //               select sol).FirstOrDefault();
            //return domains;

            return new SSIT_Solicitudes();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listIDSolicitud"></param>
        /// <returns></returns>
        public IEnumerable<SSIT_Solicitudes> GetListaIdSolicitud(List<int> listIDSolicitud)
        {
            return _unitOfWork.Db.SSIT_Solicitudes.Where(x => listIDSolicitud.Contains(x.id_solicitud)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_solicitudDesde"></param>
        /// <param name="id_solicitudHasta"></param>
        /// <returns></returns>
        public IEnumerable<SSIT_Solicitudes> GetRangoIdSolicitud(int id_solicitudDesde, int id_solicitudHasta)
        {
            return _unitOfWork.Db.SSIT_Solicitudes.Where(x => x.id_solicitud >= id_solicitudDesde && x.id_solicitud <= id_solicitudHasta).ToList();
        }

        public bool CompareTitularesWithEncomienda(int idSolicitud)
        {
            try
            {
                var encomienda = _unitOfWork.Db.Encomienda.Where(x => x.Encomienda_SSIT_Solicitudes.Select(y => y.id_solicitud).Contains(idSolicitud)
                        && x.id_estado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo
                        && x.tipo_anexo == Constantes.TipoAnexo_A).OrderByDescending(x => x.id_encomienda).FirstOrDefault();
                if (encomienda != null)
                {
                    #region Validacion PersonaFisica
                    if (!compareCantRegPersonasFisicas(idSolicitud, encomienda.id_encomienda))
                        return false;
                    var ssitSolicitudPersonaFisica = _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasFisicas.Where(x => x.id_solicitud == idSolicitud).ToList();
                    var encominedaSolicitudPersonaFisica = _unitOfWork.Db.Encomienda_Titulares_PersonasFisicas.Where(x => x.id_encomienda == encomienda.id_encomienda).ToList();

                    foreach (SSIT_Solicitudes_Titulares_PersonasFisicas pf in ssitSolicitudPersonaFisica)
                    {

                        var encomiendaPrsonaFisica = encominedaSolicitudPersonaFisica.Where(
                            x => x.id_encomienda == encomienda.id_encomienda
                                && (x.Apellido ?? string.Empty).Trim().ToUpper() == (pf.Apellido ?? string.Empty).Trim().ToUpper()
                                && (x.Nombres ?? string.Empty).Trim().ToUpper() == (pf.Nombres ?? string.Empty).Trim().ToUpper()
                                && x.id_tipodoc_personal == pf.id_tipodoc_personal
                                && (x.Nro_Documento ?? string.Empty).Trim() == (pf.Nro_Documento ?? string.Empty).Trim()
                                && (x.Cuit ?? string.Empty).Trim() == (pf.Cuit ?? string.Empty).Trim()
                                && x.id_tipoiibb == pf.id_tipoiibb
                                && (x.Ingresos_Brutos ?? string.Empty).Trim() == (pf.Ingresos_Brutos ?? string.Empty).Trim()
                        ).FirstOrDefault();

                        if (encomiendaPrsonaFisica == null)
                            return false;
                    }
                    #endregion Validacion PersonaFisica

                    #region Validacion PersonaJuridico
                    if (!compareCantRegPersonasJuridicas(idSolicitud, encomienda.id_encomienda))
                        return false;

                    var ssitSolicitudPersonaJuridica = _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasJuridicas.Where(x => x.id_solicitud == idSolicitud).ToList();
                    var encominedaSolicitudPersonaJuridica = _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas.Where(x => x.id_encomienda == encomienda.id_encomienda).ToList();

                    foreach (SSIT_Solicitudes_Titulares_PersonasJuridicas pj in ssitSolicitudPersonaJuridica)
                    {
                        var encomiendaPrsonaJuridica = encominedaSolicitudPersonaJuridica.Where(
                            x => x.id_encomienda == encomienda.id_encomienda
                                    && x.Id_TipoSociedad == pj.Id_TipoSociedad
                                    && (x.Razon_Social ?? string.Empty).Trim().ToUpper() == (pj.Razon_Social ?? string.Empty).Trim().ToUpper()
                                    && (x.CUIT ?? string.Empty).Trim() == (pj.CUIT ?? string.Empty).Trim()
                                    && (x.Nro_IIBB ?? string.Empty).Trim() == (pj.Nro_IIBB ?? string.Empty).Trim()
                                    && x.id_tipoiibb == pj.id_tipoiibb
                        ).FirstOrDefault();

                        if (encomiendaPrsonaJuridica == null)
                            return false;
                    }


                    var ssitSolicitudPersonaJuridicaPersonaFisica = _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas.Where(x => x.id_solicitud == idSolicitud).ToList();
                    var encominedaSolicitudPersonaJuridicaPersonaFisica = _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas.Where(x => x.id_encomienda == encomienda.id_encomienda).ToList();

                    foreach (SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas pjpf in ssitSolicitudPersonaJuridicaPersonaFisica)
                    {
                        var encomiendaPrsonaJuridicaPersonaFisica = encominedaSolicitudPersonaJuridicaPersonaFisica.Where(
                            x => x.id_encomienda == encomienda.id_encomienda
                                    && (x.Apellido ?? string.Empty).Trim().ToUpper() == (pjpf.Apellido ?? string.Empty).Trim().ToUpper()
                                    && (x.Nombres ?? string.Empty).Trim().ToUpper() == (pjpf.Nombres ?? string.Empty).Trim().ToUpper()
                                    && x.id_tipodoc_personal == pjpf.id_tipodoc_personal
                                    && (x.Nro_Documento ?? string.Empty).Trim() == (pjpf.Nro_Documento ?? string.Empty).Trim()
                            ).FirstOrDefault();

                        if (encomiendaPrsonaJuridicaPersonaFisica == null)
                            return false;
                    }
                    #endregion
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CompareWithEncomienda(int idSolicitud)
        {
            try
            {
                var encomienda = _unitOfWork.Db.Encomienda.Where(x => x.Encomienda_SSIT_Solicitudes.Select(y => y.id_solicitud).Contains(idSolicitud)
                        && (x.id_estado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo || x.id_estado == (int)Constantes.Encomienda_Estados.Vencida)
                        && x.tipo_anexo == Constantes.TipoAnexo_A).OrderByDescending(x => x.id_encomienda).FirstOrDefault();
                if (encomienda != null)
                {
                    #region Validacion PersonaFisica
                    if (!compareCantRegPersonasFisicas(idSolicitud, encomienda.id_encomienda))
                        return false;
                    var ssitSolicitudPersonaFisica = _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasFisicas.Where(x => x.id_solicitud == idSolicitud).ToList();
                    var encominedaSolicitudPersonaFisica = _unitOfWork.Db.Encomienda_Titulares_PersonasFisicas.Where(x => x.id_encomienda == encomienda.id_encomienda).ToList();

                    foreach (SSIT_Solicitudes_Titulares_PersonasFisicas pf in ssitSolicitudPersonaFisica)
                    {

                        var encomiendaPrsonaFisica = encominedaSolicitudPersonaFisica.Where(
                            x => x.id_encomienda == encomienda.id_encomienda
                                && (x.Apellido ?? string.Empty).Trim().ToUpper() == (pf.Apellido ?? string.Empty).Trim().ToUpper()
                                && (x.Nombres ?? string.Empty).Trim().ToUpper() == (pf.Nombres ?? string.Empty).Trim().ToUpper()
                                && x.id_tipodoc_personal == pf.id_tipodoc_personal
                                && (x.Nro_Documento ?? string.Empty).Trim() == (pf.Nro_Documento ?? string.Empty).Trim()
                                && (x.Cuit ?? string.Empty).Trim() == (pf.Cuit ?? string.Empty).Trim()
                                && x.id_tipoiibb == pf.id_tipoiibb
                                && (x.Ingresos_Brutos ?? string.Empty).Trim() == (pf.Ingresos_Brutos ?? string.Empty).Trim()
                        ).FirstOrDefault();

                        if (encomiendaPrsonaFisica == null)
                            return false;
                    }
                    #endregion Validacion PersonaFisica

                    #region Validacion PersonaJuridico
                    if (!compareCantRegPersonasJuridicas(idSolicitud, encomienda.id_encomienda))
                        return false;

                    var ssitSolicitudPersonaJuridica = _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasJuridicas.Where(x => x.id_solicitud == idSolicitud).ToList();
                    var encominedaSolicitudPersonaJuridica = _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas.Where(x => x.id_encomienda == encomienda.id_encomienda).ToList();

                    foreach (SSIT_Solicitudes_Titulares_PersonasJuridicas pj in ssitSolicitudPersonaJuridica)
                    {
                        var encomiendaPrsonaJuridica = encominedaSolicitudPersonaJuridica.Where(
                            x => x.id_encomienda == encomienda.id_encomienda
                                    && x.Id_TipoSociedad == pj.Id_TipoSociedad
                                    && (x.Razon_Social ?? string.Empty).Trim().ToUpper() == (pj.Razon_Social ?? string.Empty).Trim().ToUpper()
                                    && (x.CUIT ?? string.Empty).Trim() == (pj.CUIT ?? string.Empty).Trim()
                                    && (x.Nro_IIBB ?? string.Empty).Trim() == (pj.Nro_IIBB ?? string.Empty).Trim()
                                    && x.id_tipoiibb == pj.id_tipoiibb
                        ).FirstOrDefault();

                        if (encomiendaPrsonaJuridica == null)
                            return false;
                    }


                    var ssitSolicitudPersonaJuridicaPersonaFisica = _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas.Where(x => x.id_solicitud == idSolicitud).ToList();
                    var encominedaSolicitudPersonaJuridicaPersonaFisica = _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas.Where(x => x.id_encomienda == encomienda.id_encomienda).ToList();

                    foreach (SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas pjpf in ssitSolicitudPersonaJuridicaPersonaFisica)
                    {
                        var encomiendaPrsonaJuridicaPersonaFisica = encominedaSolicitudPersonaJuridicaPersonaFisica.Where(
                            x => x.id_encomienda == encomienda.id_encomienda
                                    && (x.Apellido ?? string.Empty).Trim().ToUpper() == (pjpf.Apellido ?? string.Empty).Trim().ToUpper()
                                    && (x.Nombres ?? string.Empty).Trim().ToUpper() == (pjpf.Nombres ?? string.Empty).Trim().ToUpper()
                                    && x.id_tipodoc_personal == pjpf.id_tipodoc_personal
                                    && (x.Nro_Documento ?? string.Empty).Trim() == (pjpf.Nro_Documento ?? string.Empty).Trim()
                            ).FirstOrDefault();

                        if (encomiendaPrsonaJuridicaPersonaFisica == null)
                            return false;
                    }
                    #endregion

                    #region Validacion Ubicaciones
                    if (!compareCantRegUbicaciones(idSolicitud, encomienda.id_encomienda))
                        return false;

                    var ssitSolicitudesUbicaciones = _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones.Where(x => x.id_solicitud == idSolicitud).ToList();
                    var encomineda_Ubicaciones = _unitOfWork.Db.Encomienda_Ubicaciones.Where(x => x.id_encomienda == encomienda.id_encomienda).ToList();

                    foreach (SSIT_Solicitudes_Ubicaciones ubic in ssitSolicitudesUbicaciones)
                    {
                        var encomiendaUbicaciones = encomineda_Ubicaciones.Where(
                            x => x.id_encomienda == encomienda.id_encomienda
                                && x.id_ubicacion == ubic.id_ubicacion
                                && x.id_subtipoubicacion == ubic.id_subtipoubicacion
                                && (x.local_subtipoubicacion ?? string.Empty).Trim() == (ubic.local_subtipoubicacion ?? string.Empty).Trim()
                                && (x.Depto ?? string.Empty).Trim() == (ubic.Depto ?? string.Empty).Trim()
                                && (x.Local ?? string.Empty).Trim() == (ubic.Local ?? string.Empty).Trim()
                                && (x.Torre ?? string.Empty).Trim() == (ubic.Torre ?? string.Empty).Trim()
                                && (x.deptoLocal_encomiendaubicacion ?? string.Empty).Trim() == (ubic.deptoLocal_ubicacion ?? string.Empty).Trim()
                                && x.id_zonaplaneamiento == ubic.id_zonaplaneamiento
                        ).FirstOrDefault();

                        if (encomiendaUbicaciones == null)
                            return false;

                        //busca las ubicaciones PUERTAS dentro de la coleecion de firmantes en el ssit
                        foreach (SSIT_Solicitudes_Ubicaciones_Puertas SSitUbicacionesPuertas in ubic.SSIT_Solicitudes_Ubicaciones_Puertas)
                        {
                            var encomiendaUbicacionesPuertas = encomiendaUbicaciones.Encomienda_Ubicaciones_Puertas.Where(
                                x => x.codigo_calle == SSitUbicacionesPuertas.codigo_calle
                                && x.NroPuerta == SSitUbicacionesPuertas.NroPuerta
                                ).FirstOrDefault();
                            if (encomiendaUbicacionesPuertas == null)
                                return false;
                        }


                        //busca las ubicaciones PUERTAS dentro de la coleecion de firmantes en el ssit
                        foreach (SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal SSitUbicacionesPropHorizontal in ubic.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal)
                        {
                            var encomiendaFirmantes = encomiendaUbicaciones.Encomienda_Ubicaciones_PropiedadHorizontal.Where(
                                x => x.id_propiedadhorizontal == SSitUbicacionesPropHorizontal.id_propiedadhorizontal
                                ).SingleOrDefault();
                            if (encomiendaFirmantes == null)
                                return false;
                        }

                    }


                    #endregion
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// valida que las cantidaddes de registros sean las mismas para SSIT y encomiendas
        /// </summary>
        /// <param name="idSolcitud"></param>
        /// <returns></returns>
        private bool compareCantRegPersonasFisicas(int idSolicitud, int idEncomienda)
        {
            try
            {
                var coutRecordSsitPersonasFisicas = _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasFisicas.Where(x => x.id_solicitud == idSolicitud).Count();
                var coutRecordEncomiendaPersonasFisicas = _unitOfWork.Db.Encomienda_Titulares_PersonasFisicas.Where(x => x.id_encomienda == idEncomienda).Count();
                if (coutRecordSsitPersonasFisicas != coutRecordEncomiendaPersonasFisicas)
                    return false;

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// valida que las cantidaddes de registros sean las mismas para SSIT y encomiendas
        /// </summary>
        /// <param name="idSolcitud"></param>
        /// <returns></returns>
        public bool compareCantRegPersonasJuridicas(int idSolicitud, int idEncomienda)
        {

            try
            {
                var coutRecordSsitPersonasJuridicas = _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasJuridicas.Where(x => x.id_solicitud == idSolicitud).Count();
                var coutRecordEncomiendaPersonasJuridicas = _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas.Where(x => x.id_encomienda == idEncomienda).Count();
                if (coutRecordSsitPersonasJuridicas != coutRecordEncomiendaPersonasJuridicas)
                    return false;

                var coutRecordSsitPersonasJuridicasPersonasFisicas = _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas.Where(x => x.id_solicitud == idSolicitud).Count();
                var coutRecordEncomiendaPersonasJuridicasPersonasFisicas = _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas.Where(x => x.id_encomienda == idEncomienda).Count();
                if (coutRecordSsitPersonasJuridicasPersonasFisicas != coutRecordEncomiendaPersonasJuridicasPersonasFisicas)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// valida que las cantidaddes de registros sean las mismas para SSIT y encomiendas
        /// </summary>
        /// <param name="idSolcitud"></param>
        /// <returns></returns>
        public bool compareCantRegUbicaciones(int idSolicitud, int idEncomienda)
        {
            try
            {
                var coutRecordSsitUbicaciones = _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones.Where(x => x.id_solicitud == idSolicitud).Count();
                var coutRecordEncomiendaUbicaciones = _unitOfWork.Db.Encomienda_Ubicaciones.Where(x => x.id_encomienda == idEncomienda).Count();
                if (coutRecordSsitUbicaciones != coutRecordEncomiendaUbicaciones)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IQueryable<SSIT_Solicitudes> GetSolicitudesAprobadasxExpediente(int anio_expediente, int nro_expediente)
        {


            string formato_SADE = string.Format("{0}-{1}", anio_expediente, nro_expediente.ToString().PadLeft(8, Convert.ToChar("0")));
            string formato_anterior = string.Format("{0}/{1}", nro_expediente, anio_expediente);


            IQueryable<SSIT_Solicitudes> res = (from sol in _unitOfWork.Db.SSIT_Solicitudes
                                                where (sol.NroExpediente.Contains(formato_anterior) ||
                                                sol.NroExpedienteSade.Contains(formato_SADE)) && sol.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.APRO
                                                select sol);

            return res;
        }

        public IQueryable<SSIT_Solicitudes> GetSolicitudesAprobadasxPartidaMatriz(int nro_partida_matriz)
        {
            IQueryable<SSIT_Solicitudes> res = (from sol in _unitOfWork.Db.SSIT_Solicitudes
                                                join solubic in _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones on sol.id_solicitud equals solubic.id_solicitud
                                                join ubic in _unitOfWork.Db.Ubicaciones on solubic.id_ubicacion equals ubic.id_ubicacion
                                                where ubic.NroPartidaMatriz == nro_partida_matriz && sol.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.APRO
                                                select sol).Distinct();


            return res;
        }



        public IQueryable<SSIT_Solicitudes> GetSolicitudesAprobadasxCUIT(string cuit)
        {
            IQueryable<SSIT_Solicitudes> res = (from sol in _unitOfWork.Db.SSIT_Solicitudes
                                                join titpf in _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasFisicas on sol.id_solicitud equals titpf.id_solicitud into pleft_titpf
                                                from titpf in pleft_titpf.DefaultIfEmpty()
                                                join titpj in _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasJuridicas on sol.id_solicitud equals titpj.id_solicitud into pleft_titpj
                                                from titpj in pleft_titpj.DefaultIfEmpty()
                                                where (titpf.Cuit == cuit || titpj.CUIT == cuit)
                                                   && sol.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.APRO
                                                select sol).Distinct();

            return res;
        }


        public IEnumerable<SSIT_Solicitudes> GetByFKIdSolicitud(int IdSolicitud)
        {
            IEnumerable<SSIT_Solicitudes> domains = _unitOfWork.Db.SSIT_Solicitudes.Where(x =>
                                                        x.id_solicitud == IdSolicitud
                                                        );

            return domains;
        }

        public bool GetEximir_CAA(int IdSolicitud, int tipoTramite)
        {
            var eximido = (from excaa in _unitOfWork.Db.Eximicion_CAA
                           where excaa.id_solicitud == IdSolicitud && excaa.id_tipo_tramite == tipoTramite
                           orderby excaa.id_eximicion_caa descending
                           select excaa.eximido).FirstOrDefault();

            return eximido;
        }

        public string GetMixDistritoZonaySubZonaBySolicitud(int idSolicitud)
        {
            var parameter = new System.Data.Entity.Core.Objects.ObjectParameter("result", "varchar(1000)");
            _unitOfWork.Db.GetMixDistritoZonaySubZonaBySolicitud(idSolicitud, parameter);
            return parameter.Value.ToString();
        }


        public bool ExisteTareaSolicitud(int Id_Solicitud, int Id_Tarea)
        {
            var q = (from s in _unitOfWork.Db.SSIT_Solicitudes
                           join tth in _unitOfWork.Db.SGI_Tramites_Tareas_HAB  on s.id_solicitud equals tth.id_solicitud
                           join tt in _unitOfWork.Db.SGI_Tramites_Tareas on tth.id_tramitetarea equals tt.id_tramitetarea
                           where 
                                s.id_solicitud == Id_Solicitud && tt.id_tarea == Id_Tarea
                           select s);

            return q.Any();
        }

        public IQueryable<SGI_Tramites_Tareas> getTareasSolicitud(int Id_Solicitud)
        {
            var q = (from s in _unitOfWork.Db.SSIT_Solicitudes
                     join tth in _unitOfWork.Db.SGI_Tramites_Tareas_HAB on s.id_solicitud equals tth.id_solicitud
                     join tt in _unitOfWork.Db.SGI_Tramites_Tareas on tth.id_tramitetarea equals tt.id_tramitetarea
                     where
                          s.id_solicitud == Id_Solicitud 
                     select tt);

            return q;
        }
        public bool isRubroCur(int idSolicitud)
        {
            int valor = Convert.ToInt32(_unitOfWork.Db.Parametros.FirstOrDefault(p => p.cod_param == "NroSolicitudReferencia").valorchar_param);
            return idSolicitud > valor;
        }

        public string ObtenerObservacionLibradoUsoOblea(int idSolicitud)
        {
            string observacion = string.Empty;

            var query = (from tth in _unitOfWork.Db.SGI_Tramites_Tareas_HAB
                         join calificacion in _unitOfWork.Db.SGI_Tarea_Calificar on tth.id_tramitetarea equals calificacion.id_tramitetarea
                         where tth.id_solicitud == idSolicitud && calificacion.Observaciones_LibradoUso != null
                         select new
                         {
                            calificacion.Observaciones_LibradoUso,
                            calificacion.CreateDate
                         }).Union(from tth in _unitOfWork.Db.SGI_Tramites_Tareas_HAB
                                  join calificacion in _unitOfWork.Db.SGI_Tarea_Revision_Gerente on tth.id_tramitetarea equals calificacion.id_tramitetarea
                                  where tth.id_solicitud == idSolicitud && calificacion.Observaciones_LibradoUso != null
                                  select new
                                  {
                                      calificacion.Observaciones_LibradoUso,
                                      calificacion.CreateDate
                                  }).Union(from tth in _unitOfWork.Db.SGI_Tramites_Tareas_HAB
                                           join calificacion in _unitOfWork.Db.SGI_Tarea_Revision_SubGerente on tth.id_tramitetarea equals calificacion.id_tramitetarea
                                           where tth.id_solicitud == idSolicitud && calificacion.Observaciones_LibradoUso != null
                                           select new
                                           {
                                               calificacion.Observaciones_LibradoUso,
                                               calificacion.CreateDate
                                           }).OrderByDescending(result => result.CreateDate).FirstOrDefault();

            if (query != null)
            {
                observacion = query.Observaciones_LibradoUso;
            }
            return observacion;
        }
    }
}

