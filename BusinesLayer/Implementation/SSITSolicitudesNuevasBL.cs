using AutoMapper;
using BaseRepository;
using IBusinessLayer;
using Dal.UnitOfWork;
using DataAcess;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitOfWork;
using StaticClass;
using DataAcess.EntityCustom;
using ExternalService.ws_interface_AGC;
using ExternalService;

namespace BusinesLayer.Implementation
{
    public class SSITSolicitudesNuevasBL : ISSITSolicitudesNuevasBL<SSITSolicitudesNuevasDTO>
    {
        private SSITSolicitudesNuevasRepository repo = null;
        private ItemDirectionRepository itemRepo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public SSITSolicitudesNuevasBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                #region "SSIT_Solicitudes"
                cfg.CreateMap<SSIT_Solicitudes_Nuevas, SSITSolicitudesNuevasDTO>()
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.id_tipotramite))
                    .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                    .ForMember(dest => dest.TipoEstadoSolicitudDTO, source => source.MapFrom(p => p.TipoEstadoSolicitud))
                    .ForMember(dest => dest.Altura_calle, source => source.MapFrom(p => p.Altura_calle))
                    .ForMember(dest => dest.CodigoSeguridad, source => source.MapFrom(p => p.CodigoSeguridad))
                    .ForMember(dest => dest.CodZonaHab, source => source.MapFrom(p => p.CodZonaHab))
                    .ForMember(dest => dest.Cuit, source => source.MapFrom(p => p.Cuit))
                    .ForMember(dest => dest.Descripcion_Actividad, source => source.MapFrom(p => p.Descripcion_Actividad))
                    .ForMember(dest => dest.idTAD, source => source.MapFrom(p => p.id_Tad))
                    .ForMember(dest => dest.Matricula, source => source.MapFrom(p => p.Matricula))
                    .ForMember(dest => dest.Nombre_calle, source => source.MapFrom(p => p.Nombre_calle))
                    .ForMember(dest => dest.Nombre_Profesional, source => source.MapFrom(p => p.Nombre_Profesional))
                    .ForMember(dest => dest.Nombre_RazonSocial, source => source.MapFrom(p => p.Nombre_RazonSocial))
                    .ForMember(dest => dest.NroPartidaHorizontal, source => source.MapFrom(p => p.NroPartidaHorizontal))
                    .ForMember(dest => dest.NroPartidaMatriz, source => source.MapFrom(p => p.NroPartidaMatriz))
                    .ForMember(dest => dest.Piso, source => source.MapFrom(p => p.Piso))
                    .ForMember(dest => dest.Superficie, source => source.MapFrom(p => p.Superficie))
                    .ForMember(dest => dest.UnidadFuncional, source => source.MapFrom(p => p.UnidadFuncional))
                    .ForMember(dest => dest.RelRubrosSolicitudesNuevasDTO, source => source.MapFrom(p => p.Rel_Rubros_Solicitudes_Nuevas));

                //.ForMember(dest => dest.SubTipoExpedienteDTO, source => source.MapFrom(p => p.SubtipoExpediente))
                //.ForMember(dest => dest.TipoExpedienteDTO, source => source.MapFrom(p => p.TipoExpediente))
                //.ForMember(dest => dest.TipoTramiteDTO, source => source.MapFrom(p => p.TipoTramite))
                //.ForMember(dest => dest.SSITSolicitudesPagosDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Pagos))
                //.ForMember(dest => dest.SSITSolicitudesHistorialEstadosDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_HistorialEstados))
                //.ForMember(dest => dest.SSITSolicitudesTitularesPersonasFisicasDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Titulares_PersonasFisicas))
                //.ForMember(dest => dest.SSITSolicitudesOrigenDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Origen))
                ;

                cfg.CreateMap<SSITSolicitudesNuevasDTO, SSIT_Solicitudes_Nuevas>()
                     .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_tipotramite, source => source.MapFrom(p => p.IdTipoTramite))
                    .ForMember(dest => dest.id_estado, source => source.MapFrom(p => p.IdEstado))
                    .ForMember(dest => dest.TipoEstadoSolicitud, source => source.Ignore())
                    .ForMember(dest => dest.Altura_calle, source => source.MapFrom(p => p.Altura_calle))
                    .ForMember(dest => dest.CodigoSeguridad, source => source.MapFrom(p => p.CodigoSeguridad))
                    .ForMember(dest => dest.CodZonaHab, source => source.MapFrom(p => p.CodZonaHab))
                    .ForMember(dest => dest.Cuit, source => source.MapFrom(p => p.Cuit))
                    .ForMember(dest => dest.Descripcion_Actividad, source => source.MapFrom(p => p.Descripcion_Actividad))
                    .ForMember(dest => dest.id_Tad, source => source.MapFrom(p => p.idTAD))
                    .ForMember(dest => dest.Matricula, source => source.MapFrom(p => p.Matricula))
                    .ForMember(dest => dest.Nombre_calle, source => source.MapFrom(p => p.Nombre_calle))
                    .ForMember(dest => dest.Nombre_Profesional, source => source.MapFrom(p => p.Nombre_Profesional))
                    .ForMember(dest => dest.Nombre_RazonSocial, source => source.MapFrom(p => p.Nombre_RazonSocial))
                    .ForMember(dest => dest.NroPartidaHorizontal, source => source.MapFrom(p => p.NroPartidaHorizontal))
                    .ForMember(dest => dest.NroPartidaMatriz, source => source.MapFrom(p => p.NroPartidaMatriz))
                    .ForMember(dest => dest.Piso, source => source.MapFrom(p => p.Piso))
                    .ForMember(dest => dest.Superficie, source => source.MapFrom(p => p.Superficie))
                    .ForMember(dest => dest.UnidadFuncional, source => source.MapFrom(p => p.UnidadFuncional))
                    .ForMember(dest => dest.Rel_Rubros_Solicitudes_Nuevas, source => source.Ignore());
                ;


                #endregion

                #region "Rel_Rubros_Solicitudes_Nuevas"
                cfg.CreateMap<Rel_Rubros_Solicitudes_Nuevas, RelRubrosSolicitudesNuevasDTO>()
                .ForMember(dest => dest.idRelRubSol, source => source.MapFrom(p => p.id_relrubSol));

                cfg.CreateMap<RelRubrosSolicitudesNuevasDTO, Rel_Rubros_Solicitudes_Nuevas>();
                #endregion

                #region "TipoEstadoSolicitud"
                cfg.CreateMap<TipoEstadoSolicitud, TipoEstadoSolicitudDTO>();

                cfg.CreateMap<TipoEstadoSolicitudDTO, TipoEstadoSolicitud>()
                    .ForAllMembers(dest => dest.Ignore());
                #endregion

                #region "TipoTramite"
                cfg.CreateMap<TipoTramite, TipoTramiteDTO>()
                    .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.id_tipotramite))
                    .ForMember(dest => dest.CodTipoTramite, source => source.MapFrom(p => p.cod_tipotramite))
                    .ForMember(dest => dest.DescripcionTipoTramite, source => source.MapFrom(p => p.descripcion_tipotramite))
                    .ForMember(dest => dest.CodTipoTramiteWs, source => source.MapFrom(p => p.cod_tipotramite_ws));

                cfg.CreateMap<TipoTramiteDTO, TipoTramite>()
                    .ForMember(dest => dest.id_tipotramite, source => source.MapFrom(p => p.IdTipoTramite))
                    .ForMember(dest => dest.cod_tipotramite, source => source.MapFrom(p => p.CodTipoTramite))
                    .ForMember(dest => dest.descripcion_tipotramite, source => source.MapFrom(p => p.DescripcionTipoTramite))
                    .ForMember(dest => dest.cod_tipotramite_ws, source => source.MapFrom(p => p.CodTipoTramiteWs));
                #endregion

            });

            mapperBase = config.CreateMapper();
        }


        public SSITSolicitudesNuevasDTO Single(int IdSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesNuevasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdSolicitud);
                var entityDto = mapperBase.Map<SSIT_Solicitudes_Nuevas, SSITSolicitudesNuevasDTO>(entity);
                if (entityDto != null)
                {
                    switch (entityDto.IdTipoTramite)
                    {
                        case (int)Constantes.TipoDeTramite.Habilitacion:
                            entityDto.TipoTramiteDescripcion = "Habilitación";
                            break;
                        case (int)Constantes.TipoDeTramite.Transferencia:
                            entityDto.TipoTramiteDescripcion = "Transferencia";
                            break;
                        case (int)Constantes.TipoDeTramite.Ampliacion:
                            entityDto.TipoTramiteDescripcion = "Ampliación de rubro y/o superficie";
                            break;
                        case (int)Constantes.TipoDeTramite.RedistribucionDeUso:
                            entityDto.TipoTramiteDescripcion = "Redistribución de Uso";
                            break;

                        case (int)Constantes.TipoDeTramite.RectificatoriaHabilitacion:
                            entityDto.TipoTramiteDescripcion = "Rectificatoria de Habilitación";
                            break;
                    }

                }

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Métodos de actualizacion e insert
        /// <summary>
        /// Inserta la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public int Insert(SSITSolicitudesNuevasDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);


                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesNuevasRepository(unitOfWork);

                    var elementEntitySol = mapperBase.Map<SSITSolicitudesNuevasDTO, SSIT_Solicitudes_Nuevas>(objectDto);
                    var insertSolOk = repo.Insert(elementEntitySol);


                    unitOfWork.Commit();
                    objectDto.IdSolicitud = elementEntitySol.id_solicitud;
                    return elementEntitySol.id_solicitud;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public int InsertRubros(RelRubrosSolicitudesNuevasDTO objectDto)
        //{
        //    //try
        //    //{
        //    //    uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);


        //    //    using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
        //    //    {
        //    //        repo = new RelRubrosSolicitudesNuevasRepository(unitOfWork);

        //    //        var elementEntitySol = mapperBase.Map<RelRubrosSolicitudesNuevasDTO, Rel_Rubros_Solicitudes_Nuevas>(objectDto);
        //    //        var insertSolOk = repo.Insert(elementEntitySol);


        //    //        unitOfWork.Commit();
        //    //        objectDto.IdSolicitud = elementEntitySol.id_Solicitud;
        //    //        return elementEntitySol.id_Solicitud;
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    throw ex;
        //    //}
        //}
        /// <summary>
        /// Modifica la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public void Update(SSITSolicitudesNuevasDTO objectDTO)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesNuevasRepository(unitOfWork);
                    var elementDTO = mapperBase.Map<SSITSolicitudesNuevasDTO, SSIT_Solicitudes_Nuevas>(objectDTO);
                    repo.Update(elementDTO);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// elimina la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>      
        public void Delete(SSITSolicitudesNuevasDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesNuevasRepository(unitOfWork);
                    var elementDto = mapperBase.Map<SSITSolicitudesNuevasDTO, SSIT_Solicitudes_Nuevas>(objectDto);
                    var insertOk = repo.Delete(elementDto);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region acciones
        public bool anularSolicitud(int id_solicitud, Guid userid)
        {
            bool confirmar = false;
            SSITSolicitudesNuevasDTO sol = Single(id_solicitud);
            if (sol.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.ANU)
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    sol.IdEstado = (int)Constantes.TipoEstadoSolicitudEnum.ANU;
                    sol.LastUpdateUser = userid;
                    sol.LastUpdateDate = DateTime.Now;
                    Update(sol);

                    unitOfWork.Commit();
                }
                confirmar = true;
            }
            return confirmar;
        }
        public bool confirmarSolicitud(int id_solicitud, Guid userid)
        {
            bool confirmar = false;
            SSITSolicitudesNuevasDTO sol = Single(id_solicitud);
            if (sol.IdEstado == (int)Constantes.TipoEstadoSolicitudEnum.COMP)
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    sol.IdEstado = (int)Constantes.TipoEstadoSolicitudEnum.ETRA;
                    sol.LastUpdateUser = userid;
                    sol.LastUpdateDate = DateTime.Now;
                    Update(sol);
                    unitOfWork.Commit();
                }
                confirmar = true;

            }
            return confirmar;
        }

        

        #endregion
        #region Validaciones
        private bool ValidacionSolicitudes(int id_solicitud)
        {
            /*repo = new SSITSolicitudesRepository(this.uowF.GetUnitOfWork());
            var SSITentity = repo.Single(id_solicitud);
            if (SSITentity.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF
                && isEscuela(SSITentity.id_solicitud)
                && string.IsNullOrEmpty(SSITentity.NroExpedienteSadeRelacionado))
                throw new Exception(Errors.SSIT_SOLICITUD_ESCUELA_SIN_NUMERO_EXPEDIENTE_RELACIONADO_SADE);

            if (SSITentity.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF
                || SSITentity.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO
                || SSITentity.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.SUSPEN
                || SSITentity.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.PENPAG)
            {
                #region Validaciones comunes
                var listEnc = SSITentity.Encomienda.Where(x => x.id_estado == (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo).ToList();

                var lstEncA = listEnc.Where(x => x.tipo_anexo == Constantes.TipoAnexo_A).ToList();
                if (lstEncA.Count() == 0)
                    throw new Exception(Errors.SSIT_SOLICITUD_ANEXO_TECNICO_INEXISTENTE);

                if (ExisteAnexosEnCurso(id_solicitud))
                    throw new Exception(Errors.SSIT_SOLICITUD_ANEXO_EN_CURSO);

                var encomienda = lstEncA.OrderByDescending(x => x.id_encomienda).First();

                if (SSITentity.id_tipotramite != (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                {
                    if (!ExisteAnexosNotarialAprobada(listEnc))
                    {
                        solicitud_Anexo_Notarial_Inexistente = string.Format(Errors.SSIT_SOLICITUD_ANEXO_NOTARIAL_INEXISTENTE, encomienda.id_encomienda);
                        throw new Exception(solicitud_Anexo_Notarial_Inexistente);
                    }

                    var pdfActa = SSITentity.Encomienda.Any(enc => enc.wsEscribanos_ActaNotarial.Any(wsActa => wsActa.id_file.HasValue));

                    if (!pdfActa)
                        throw new Exception(Errors.SSIT_SOLICITUD_ANEXO_NOTARIAL_SIN_ARCHIVO);
                }


                var ubiClausuras = SSITentity.SSIT_Solicitudes_Ubicaciones
                                        .Any(x => x.Ubicaciones.Ubicaciones_Clausuras
                                                        .Any(w => w.fecha_alta_clausura < DateTime.Now
                                                                    && (w.fecha_baja_clausura > DateTime.Now || w.fecha_baja_clausura == null)));

                var horClausuras = SSITentity.SSIT_Solicitudes_Ubicaciones
                                    .Any(ubic => ubic.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal
                                                    .Any(ssitHor => ssitHor.Ubicaciones_PropiedadHorizontal.Ubicaciones_PropiedadHorizontal_Inhibiciones
                                                                                    .Any(inhi => inhi.fecha_vencimiento > DateTime.Now)));

                if (ubiClausuras || horClausuras)
                    throw new Exception(Errors.SSIT_SOLICITUD_UBICACIONES_CLAUSURAS);

                var ubiInhibiciones = SSITentity.SSIT_Solicitudes_Ubicaciones.Any(solUbi => solUbi.Ubicaciones.Ubicaciones_Inhibiciones
                                                                                                    .Any(uc => uc.fecha_vencimiento > DateTime.Now));

                var horInhibiciones = SSITentity.SSIT_Solicitudes_Ubicaciones
                                                .Any(solUbi => solUbi.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal
                                                            .Any(horUbi => horUbi.Ubicaciones_PropiedadHorizontal.Ubicaciones_PropiedadHorizontal_Inhibiciones
                                                                        .Any(hor => hor.fecha_vencimiento > DateTime.Now)));

                if (ubiInhibiciones || horInhibiciones)
                    throw new Exception(Errors.SSIT_SOLICITUD_UBICACIONES_INHIBIDAS);

                List<int> lstEncomiendasRelacionadas = SSITentity.Encomienda.Select(s => s.id_encomienda).ToList();



                int id_encomienda = listEnc.LastOrDefault().id_encomienda;
                EncomiendaBL encBL = new EncomiendaBL();
                EncomiendaDTO encDTO = encBL.Single(id_encomienda);
                RubrosBL rubBL = new RubrosBL();

                var lstRubros = rubBL.GetByListCodigo(encDTO.EncomiendaRubrosDTO.Select(s => s.CodigoRubro).ToList());

                bool tieneRubroEstadio = lstRubros.Where(x => x.EsEstadio).Any();

                DtoCAA solicitud_caa = new DtoCAA();
                bool EximirCAA = repo.GetEximir_CAA(SSITentity.id_solicitud, SSITentity.id_tipotramite);

                if (EximirCAA == false &&
                    SSITentity.id_tipotramite != (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                    solicitud_caa = ValidarCAA(lstEncomiendasRelacionadas, listEnc, encomienda, tieneRubroEstadio);

                if (!CompareWithEncomienda(id_solicitud))
                    throw new Exception(Errors.SSIT_SOLICITUD_TITULARES_UBICACIONES_DIFERENTES);
                #endregion
                #region pagos
                if (SSITentity.id_tipotramite != (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                {
                    SSITSolicitudesBL ssitBL = new SSITSolicitudesBL();
                    var ssitDTO = ssitBL.Single(encDTO.IdSolicitud);

                    int ExcepcionRubro = (int)Constantes.TieneRubroConExencionPago.SinExencion;

                    bool tieneRubroProTeatro = lstRubros.Where(x => x.EsProTeatro).Any();
                    if (tieneRubroProTeatro)
                        ExcepcionRubro = (int)Constantes.TieneRubroConExencionPago.ProTeatro;

                    if (tieneRubroEstadio)
                        ExcepcionRubro = (int)Constantes.TieneRubroConExencionPago.Estadio;

                    bool tieneRubroCCultural = lstRubros.Where(x => x.EsCentroCultural).Any();
                    if (tieneRubroCCultural)
                        ExcepcionRubro = (int)Constantes.TieneRubroConExencionPago.CentroCultural;

                    var repoDoc = new SSITDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
                    var listDocSsit = repoDoc.GetByFKIdSolicitud(id_solicitud);

                    switch (ExcepcionRubro)
                    {
                        case (int)Constantes.TieneRubroConExencionPago.ProTeatro:
                            bool tieneDocProTeatro = listDocSsit.Any(x => x.id_tdocreq == (int)Constantes.TipoDocumentoRequerido.ConstanciaInicioTramiteIGJoINAES ||
                                                      x.id_tdocreq == (int)Constantes.TipoDocumentoRequerido.CertificadoProTeatro);
                            if (!tieneDocProTeatro)
                            {
                                ValidarPagoSSIT(id_solicitud);
                                if (EximirCAA == false)
                                    ValidarPagoCAA(solicitud_caa);
                            }
                            break;
                        case (int)Constantes.TieneRubroConExencionPago.Estadio:
                            bool tieneChkEstadio = ssitDTO.ExencionPago;
                            if (!tieneChkEstadio)
                                ValidarPagoSSIT(id_solicitud);

                            if (solicitud_caa.id_solicitud > 0 && EximirCAA == false)
                                ValidarPagoCAA(solicitud_caa);
                            break;
                        case (int)Constantes.TieneRubroConExencionPago.CentroCultural:
                            bool tieneDocCCultural = listDocSsit.Where(x => x.id_tdocreq == (int)Constantes.TipoDocumentoRequerido.ConstanciaInicioTramiteIGJoINAES).Any();
                            if (!tieneDocCCultural)
                            {
                                ValidarPagoSSIT(id_solicitud);
                                if (EximirCAA == false)
                                    ValidarPagoCAA(solicitud_caa);
                            }
                            break;
                        default:
                            ValidarPagoSSIT(id_solicitud);
                            if (EximirCAA == false)
                                ValidarPagoCAA(solicitud_caa);
                            break;
                    }
                }
                #endregion

                if (id_solicitud > Constantes.SOLICITUDES_NUEVAS_MAYORES_A)
                    ValidacionSolicitudesNuevas(id_solicitud, id_encomienda);
                else
                    ValidacionSolicitudesViejas(SSITentity);
                return true;
            }*/
            return false;
        }


        #endregion
        public string ActualizarEstado(int IdSolicitud, Guid userid)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesNuevasRepository(this.uowF.GetUnitOfWork());
            var sol = repo.Single(IdSolicitud);

            if (sol.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
            {
                var hasTitulares = sol.Nombre_RazonSocial.Trim() == "" || sol.Cuit.Trim() == "";
                if (!hasTitulares)
                {
                    return "Falta declarar el/los Titular/es";
                }

                var hasProfesional = sol.Nombre_Profesional.Trim() == "" || sol.Matricula.Trim() == "";
                if (!hasProfesional)
                {
                    return "Falta declarar el profesional";
                }
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWorkTran = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesNuevasRepository(unitOfWorkTran);
                    sol = repo.Single(IdSolicitud);
                    sol.id_estado = (int)Constantes.TipoEstadoSolicitudEnum.COMP;
                    sol.LastUpdateUser = userid;
                    sol.LastUpdateDate = DateTime.Now;
                    repo.Update(sol);
                    unitOfWorkTran.Commit();
                }

                if (sol.id_Tad != null)
                {
                    ParametrosRepository parametrosRepo = new ParametrosRepository(this.uowF.GetUnitOfWork());

                    string _urlESB = parametrosRepo.GetParametroChar("Url.Service.ESB");
                    string trata = parametrosRepo.GetParametroChar("Trata.Habilitacion");
                    if (sol.id_tipotramite == (int)Constantes.TipoTramite.AMPLIACION)
                        trata = parametrosRepo.GetParametroChar("Trata.Ampliacion");
                    else if (sol.id_tipotramite == (int)Constantes.TipoTramite.REDISTRIBUCION_USO)
                        trata = parametrosRepo.GetParametroChar("Trata.RedistribucionDeUso");

                    itemRepo = new ItemDirectionRepository(this.uowF.GetUnitOfWork());
                    List<int> lisSol = new List<int>();
                    lisSol.Add(IdSolicitud);
                    List<ItemPuertaEntity> LstDoorsDirection = itemRepo.GetDireccionesSSIT(lisSol).ToList();
                    /*var listU = convertDirecciones(LstDoorsDirection);

                    string Direccion = listU.First().direccion;

                    enviarActualizacionTramite(_urlESB, sol, trata, Direccion);*/
                }
            }

            return string.Empty;
        }

        private void enviarActualizacionTramite(string _urlESB, SSIT_Solicitudes sol, string trata, string Direccion)
        {
            try
            {
                wsTAD.actualizarTramite(_urlESB, sol.idTAD.Value, sol.id_solicitud, sol.NroExpedienteSade, trata, Direccion);
            }
            catch (Exception)
            {
            }
        }

       
    }
}
