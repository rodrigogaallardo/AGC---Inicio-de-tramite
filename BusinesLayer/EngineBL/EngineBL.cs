using System;
using System.Collections.Generic;
using DataAcess;
using DataTransferObject;
using DataTransferObject.Engine;
using BaseRepository;
using AutoMapper;
using System.Linq;
using UnitOfWork;
using BaseRepository.Engine;
using StaticClass;
using Dal.UnitOfWork;
using System.Globalization;
using System.Configuration;
using DataAcess.EntityCustom;

namespace BusinesLayer.Implementation
{
    public class EngineBL
    {
        public static string engine_tarea_No_Puede_Tomarse = "";
        private EncomiendaRepository repoEnc = null;
        private SSITSolicitudesRepository repoSsit = null;
        private TransferenciasSolicitudesRepository repoTransf = null;
        private SGITramitesTareasepository repoTareas = null;
        private SGITramitesTareasRepository repoTramiteTarea = null;
        private SGITramitesTareasHABTareasepository repoTareasHabilitacion = null;
        private AspnetUsersRepository aspnetUsersRepository = null;
        /*ENgine Repositories*/
        EngTareasRepository repoEngTareas = null;

        private IUnitOfWorkFactory uowF = null;

        IMapper mapperBase;
        IMapper mapperBaseTarea;
        IMapper mapperSGI;
        IMapper mapperResultadoTarea;
        IMapper mapperBaseENG_Circuitos;

        public EngineBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CallesDTO, Calles>().ReverseMap();
            });
            mapperBase = config.CreateMapper();


            var configTarea = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EngineTareaDTO, ENG_Tareas>().ReverseMap();
            });
            mapperBaseTarea = configTarea.CreateMapper();

            var configSGI = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SGITramitesTareasDTO, SGI_Tramites_Tareas>().ReverseMap()
                    .ForMember(dest => dest.IdTramiteTarea, source => source.MapFrom(p => p.id_tramitetarea))
                    .ForMember(dest => dest.IdTarea, source => source.MapFrom(p => p.id_tarea))
                    .ForMember(dest => dest.IdResultado, source => source.MapFrom(p => p.id_resultado))
                    .ForMember(dest => dest.FechaInicioTramiteTarea, source => source.MapFrom(p => p.FechaInicio_tramitetarea))
                    .ForMember(dest => dest.FechaCierreTramiteTarea, source => source.MapFrom(p => p.FechaCierre_tramitetarea))
                    .ForMember(dest => dest.UsuarioAsignadoTramiteTarea, source => source.MapFrom(p => p.UsuarioAsignado_tramitetarea))
                    .ForMember(dest => dest.FechaAsignacionTramiteTarea, source => source.MapFrom(p => p.FechaAsignacion_tramtietarea))
                    .ForMember(dest => dest.IdProximaTarea, source => source.MapFrom(p => p.id_proxima_tarea));
            });
            mapperSGI = configSGI.CreateMapper();

            var configResultTarea = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EngineResultadoTareaDTO, ENG_Rel_Resultados_Tareas>().ReverseMap();
            });

            mapperResultadoTarea = configResultTarea.CreateMapper();

            var configENG_Circuitos = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ENG_CircuitosDTO, ENG_Circuitos>().ReverseMap();
            });
            mapperBaseENG_Circuitos = configENG_Circuitos.CreateMapper();
        }


        public Guid GetUltimoUsuarioAsignado(int id_solicitud, int id_tarea)
        {

            uowF = new TransactionScopeUnitOfWorkFactory();
            repoTareas = new SGITramitesTareasepository(this.uowF.GetUnitOfWork());

            Guid? userId = repoTareas.GetUltimoUsuarioAsignado(id_solicitud, id_tarea);

            Guid ret = Guid.Empty;
            if (userId.HasValue)
                ret = (Guid)userId;

            return ret;

        }

        public bool CheckRolTarea(int idTramitetarea, Guid userid)
        {
            bool ret = false;

            uowF = new TransactionScopeUnitOfWorkFactory();
            repoTareas = new SGITramitesTareasepository(this.uowF.GetUnitOfWork());
            aspnetUsersRepository = new AspnetUsersRepository(this.uowF.GetUnitOfWork());
            //SGI_Tramites_Tareas tramite_tarea = repoTareas.Single(id_tramitetarea);
            var perfiles_permitidos_tarea = repoTareas.PerfilesPermitidosPorTarea(idTramitetarea);
            var perfiles_usuario = aspnetUsersRepository.Single(userid).SGI_Perfiles.Select(x => x.nombre_perfil).ToList();

            var roles_en_comun = (from perfil1 in perfiles_permitidos_tarea select perfil1).Intersect
                           (from perfil2 in perfiles_usuario select perfil2).ToList();
            // Si posee el rol para modificar la tarea 
            ret = (roles_en_comun.Count > 0);

            return ret;

        }

        public bool CheckEditTarea(int idTramitetarea, Guid userid)
        {
            bool ret = false;
            uowF = new TransactionScopeUnitOfWorkFactory();
            repoTareas = new SGITramitesTareasepository(this.uowF.GetUnitOfWork());
            SGI_Tramites_Tareas tramite_tarea = repoTareas.Single(idTramitetarea);

            // Si posee el rol para modificar la tarea se continua la validación
            ret = CheckRolTarea(idTramitetarea, userid);
            // Si la tarea no está cerrada y está asignada al usuario logueado
            if (ret && !tramite_tarea.FechaCierre_tramitetarea.HasValue && tramite_tarea.UsuarioAsignado_tramitetarea == userid)
                ret = true;
            else
                ret = false;
            return ret;
        }

        /// <summary>
        /// Tomar Tarea
        /// </summary>
        /// <param name="id_tramitetarea"></param>
        /// <param name="userid"></param>
        public string TomarTarea(int idTramitetarea, Guid userid)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());
            bool tareaAsignada = repoEngTareas.ControldeTareaAsignada(idTramitetarea);
            string respuesta = "";
            if (!tareaAsignada)
            {
                IUnitOfWorkFactory uowFTs = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = uowFTs.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repoTareas = new SGITramitesTareasepository(unitOfWork);
                    var tareasEntity = repoTareas.Single(idTramitetarea);
                    tareasEntity.FechaAsignacion_tramtietarea = DateTime.Now;
                    tareasEntity.UsuarioAsignado_tramitetarea = userid;
                    repoTareas.Update(tareasEntity);
                    respuesta = StaticClass.EngineStaticClass.EngTareaOK;
                    repoTareas.UnitOfWork.Commit();
                }
            }
            else
            {
                respuesta = StaticClass.EngineStaticClass.EngTareaNotAssign;
            }

            return respuesta;
        }
        /// <summary>
        /// asigna una tarea a un usuario 
        /// </summary>
        /// <param name="id_tramitetarea"></param>
        /// <returns></returns>
        public string GetByFKIdTramiteTareasIdSolicitud(int idTarea, int idSol)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            SGI_SadeProcesosRepository repo = new SGI_SadeProcesosRepository(this.uowF.GetUnitOfWork());

            string respuesta = repo.GetPlanoVisadoByFKIdTramiteTarea(idTarea, idSol);
            return respuesta;
        }
        /// <summary>
        /// asigna una tarea a un usuario 
        /// </summary>
        /// <param name="id_tramitetarea"></param>
        /// <param name="userid_a_asignar"></param>
        /// <param name="userid_asignador"></param>
        /// <returns></returns>
        public int AsignarTarea(int idTramiteTarea, Guid? userIdTOAsign, Guid? userIdAsignador)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            IUnitOfWorkFactory uowFTs = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
            using (IUnitOfWork unitOfWork = uowFTs.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
            {
                repoEngTareas = new EngTareasRepository(unitOfWork);
                var asignarTarea = repoEngTareas.AsignarTarea(idTramiteTarea, userIdTOAsign, userIdAsignador);
                unitOfWork.Commit();
                return asignarTarea;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idTramiteTarea"></param>
        /// <param name="userIdTOAsign"></param>
        /// <param name="unitOfWork"></param>
        public void AsignarTarea(int idTramiteTarea, Guid userIdTOAsign, IUnitOfWork unitOfWork)
        {

            repoEngTareas = new EngTareasRepository(unitOfWork);
            repoTareas = new SGITramitesTareasepository(unitOfWork);
            repoTramiteTarea = new SGITramitesTareasRepository(unitOfWork);

            var profile = repoTareas.GetProfile(idTramiteTarea).FirstOrDefault();

            if (profile == null)
            {
                var TramiteTarea = repoTramiteTarea.Single(idTramiteTarea);

                TramiteTarea.UsuarioAsignado_tramitetarea = userIdTOAsign;
                TramiteTarea.FechaAsignacion_tramtietarea = DateTime.Now;

                repoTramiteTarea.Update(TramiteTarea);
            }
            else
            {
                engine_tarea_No_Puede_Tomarse = string.Format("No es posible tomar la tarea, la misma ha sido tomada por el usuario " + profile.NomApe);

                throw new Exception(engine_tarea_No_Puede_Tomarse);
            }



        }

        /// <summary>
        /// reasigna una TAREA
        /// </summary>
        /// <param name="idTramiteTarea"></param>
        /// <param name="userIdTOAsign"></param>
        /// <returns></returns>
        public int ReasignarTarea(int idTramiteTarea, Guid userIdTOAsign)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            IUnitOfWorkFactory uowFTs = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
            using (IUnitOfWork unitOfWork = uowFTs.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
            {
                repoEngTareas = new EngTareasRepository(unitOfWork);
                var asignarTarea = repoEngTareas.ResignarTarea(idTramiteTarea, userIdTOAsign);
                unitOfWork.Commit();
                return asignarTarea;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idTramiteTarea"></param>
        /// <param name="userIdTOAsign"></param>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        public int ReasignarTarea(int idTramiteTarea, Guid userIdTOAsign, IUnitOfWork unitOfWork)
        {
            repoEngTareas = new EngTareasRepository(unitOfWork);
            var asignarTarea = repoEngTareas.ResignarTarea(idTramiteTarea, userIdTOAsign);
            return asignarTarea;
        }

        /// <summary>
        /// Obtiene la Siguiente TAREA
        /// </summary>
        /// <param name="idResultado"></param>
        /// <param name="idTarea_actual"></param>
        /// <param name="idTramitetarea"></param>
        /// <returns></returns>
        public List<EngineTareaDTO> GetTareasSiguientes(int idTarea_actual, int idResultado, int idTramitetarea)
        {

            uowF = new TransactionScopeUnitOfWorkFactory();
            repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());
            var ret = repoEngTareas.SelectAllNExtTareas(idTarea_actual, idResultado, idTramitetarea).ToList();
            var lstTareasDto = mapperBaseTarea.Map<List<ENG_Tareas>, List<EngineTareaDTO>>(ret);
            return lstTareasDto;
        }

        public int CrearTarea(int idTramite, int idTarea, Guid createUser)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());
            var ret = repoEngTareas.CreateTarea(idTramite, idTarea, createUser);

            return ret;
        }
        public int CrearTarea(int idTramite, int idTarea, Guid createUser, IUnitOfWork unitOfWork)
        {

            repoEngTareas = new EngTareasRepository(unitOfWork);
            var ret = repoEngTareas.CreateTarea(idTramite, idTarea, createUser);

            return ret;
        }

        public int getTareaSolicitudHabilitacion(int id_solicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());
            int id_circuito = getIdCircuito(id_solicitud);
            string cod_tarea_solicitud = id_circuito.ToString() + Engine.Sufijo_SolicitudHabilitacion;
            return repoEngTareas.GetIdTarea(Convert.ToInt32(cod_tarea_solicitud));
        }
        private static decimal CalcularSuperficieTotal(Encomienda_DatosLocal datosLocal)
        {
            decimal SuperficieTotal = 0;
            bool esAmpliacionSuperficie = (datosLocal.ampliacion_superficie.HasValue ? datosLocal.ampliacion_superficie.Value : false);

            if (esAmpliacionSuperficie)
                SuperficieTotal = datosLocal.superficie_cubierta_amp.Value + datosLocal.superficie_descubierta_amp.Value;
            else
                SuperficieTotal = datosLocal.superficie_cubierta_dl.Value + datosLocal.superficie_descubierta_dl.Value;
            return SuperficieTotal;
        }

        private int getIdCircuitoByIdEncomienda(int id_encomienda)
        {                                    

            int id_circuito = GetIdCircuitoByIdEncomienda(id_encomienda);

            if (id_circuito == 0)
                throw new Exception("No se ha encontrado ningún rubro con circuito configurado en la solicitud.");

            return id_circuito;
        }
        
        private int getIdCircuito(int id_solicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());
            int id_circuito = repoEngTareas.GetIdCircuitoByIdSolicitud(id_solicitud);

            if (id_circuito == 0)
                throw new Exception("No se ha encontrado ningún rubro con circuito configurado en la solicitud.");
            return id_circuito;
        }
        public int getTareaFinTramite(int id_solicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());
            int id_circuito = getIdCircuito(id_solicitud);
            string cod_tarea_solicitud = id_circuito.ToString() + Engine.Sufijo_FinTramite;
            return repoEngTareas.GetIdTarea(Convert.ToInt32(cod_tarea_solicitud));
        }
        public int getTareaAsignacionCalificador(int id_solicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());
            int id_circuito = getIdCircuito(id_solicitud);
            string cod_tarea_solicitud = id_circuito.ToString() + Engine.Sufijo_AsginacionCalificador;
            return repoEngTareas.GetIdTarea(Convert.ToInt32(cod_tarea_solicitud));
        }
        public int getTareaGenerarExpediente(int id_solicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());
            int id_circuito = getIdCircuito(id_solicitud);
            string cod_tarea_solicitud = id_circuito.ToString() + Engine.Sufijo_GenerarExpediente;
            return repoEngTareas.GetIdTarea(Convert.ToInt32(cod_tarea_solicitud));
        }
        public int FinalizarTarea(int idTramiteTarea, int idResultado, int idProximaTarea, Guid? userid)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());
                int idTarea = repoEngTareas.FinalizarTarea(idTramiteTarea, idResultado, idProximaTarea, userid);
                return idTarea;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public SGITramitesTareasDTO GetUltimaTareaHabilitacionAbierta(int IdSolicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());

            var ret = repoEngTareas.GetUltimaTareaHabilitacionAbierta(IdSolicitud);
            var lstTareasDto = mapperSGI.Map<SGI_Tramites_Tareas, SGITramitesTareasDTO>(ret);
            return lstTareasDto;
        }
        public SGITramitesTareasDTO GetUltimaTareaHabilitacion(int IdSolicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());

            var ret = repoEngTareas.GetUltimaTareaHabilitacion(IdSolicitud);
            var lstTareasDto = mapperSGI.Map<SGI_Tramites_Tareas, SGITramitesTareasDTO>(ret);
            return lstTareasDto;
        }
        public SGITramitesTareasDTO GetUltimaTareaConsultaPadron(int IdSolicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());

            var ret = repoEngTareas.GetUltimaTareaConsultaPadron(IdSolicitud);
            var lstTareasDto = mapperSGI.Map<SGI_Tramites_Tareas, SGITramitesTareasDTO>(ret);
            return lstTareasDto;
        }
        public SGITramitesTareasDTO GetUltimaTareaTransferencia(int IdSolicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());

            var ret = repoEngTareas.GetUltimaTareaTransferencia(IdSolicitud);
            var lstTareasDto = mapperSGI.Map<SGI_Tramites_Tareas, SGITramitesTareasDTO>(ret);
            return lstTareasDto;
        }
        public int GetIdTarea(int Codigo)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());
                return repoEngTareas.GetIdTarea(Codigo);

            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }
        public IEnumerable<EngineResultadoTareaDTO> GetResultadoTarea(int IdTarea)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());

            var ret = repoEngTareas.GetResultadoTarea(IdTarea);
            var lstTareasDto = mapperResultadoTarea.Map<IEnumerable<ENG_Rel_Resultados_Tareas>, IEnumerable<EngineResultadoTareaDTO>>(ret);
            return lstTareasDto;

        }
        public int GetIdTarea(int Codigo, IUnitOfWork unitOfWork)
        {
            repoEngTareas = new EngTareasRepository(unitOfWork);
            return repoEngTareas.GetIdTarea(Codigo);
        }
        public IEnumerable<SGITramitesTareasDTO> GetTareaTransferencia(int IdSolicitud, int IdTarea)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());

            var ret = repoEngTareas.GetTareaTransferencia(IdSolicitud, IdTarea);
            var lstTareasDto = mapperSGI.Map<IEnumerable<SGI_Tramites_Tareas>, IEnumerable<SGITramitesTareasDTO>>(ret);

            return lstTareasDto;
        }
        public IEnumerable<SGITramitesTareasDTO> GetTareaHabilitacion(int IdSolicitud, int IdTarea)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());

            var ret = repoEngTareas.GetTareaHabilitacion(IdSolicitud, IdTarea);
            var lstTareasDto = mapperSGI.Map<IEnumerable<SGI_Tramites_Tareas>, IEnumerable<SGITramitesTareasDTO>>(ret);

            return lstTareasDto;
        }
        public IEnumerable<SGITramitesTareasDTO> GetTareaHabilitacionCalificar(int IdSolicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());

            var ret = repoEngTareas.GetTareaHabilitacionCalificar(IdSolicitud);
            var lstTareasDto = mapperSGI.Map<IEnumerable<SGI_Tramites_Tareas>, IEnumerable<SGITramitesTareasDTO>>(ret);

            return lstTareasDto;
        }
        public bool isCalificar(int IdTarea)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());
            return repoEngTareas.isCalificar(IdTarea);
        }
        public SGITramitesTareasDTO GetTareaAnteriorHabilitacion(int IdSolicitud, int IdTramitetarea)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());

            var ret = repoEngTareas.GetTareaAnteriorHabilitacion(IdSolicitud, IdTramitetarea);
            var lstTareasDto = mapperSGI.Map<SGI_Tramites_Tareas, SGITramitesTareasDTO>(ret);
            return lstTareasDto;
        }
        public IEnumerable<SGITramitesTareasDTO> GetTareaConsultaPadron(int IdCPadron, int IdTarea)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());

            var ret = repoEngTareas.GetTareaConsultaPadron(IdCPadron, IdTarea);
            var lstTareasDto = mapperSGI.Map<IEnumerable<SGI_Tramites_Tareas>, IEnumerable<SGITramitesTareasDTO>>(ret);

            return lstTareasDto;
        }
        public int GetIdCircuitoBySolicitud(int id_solicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            return getIdCircuito(id_solicitud);
        }
        public int GetIdCircuitoByEncomienda(int idEncomienda)
        {
            return getIdCircuitoByIdEncomienda(idEncomienda);
        }
        public int GetIdCircuito(int id_tarea)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());
            return repoEngTareas.GetIdCircuito(id_tarea);
        }
        public string GetDescripcionCircuito(int id_solicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());
            var descripcionCircuito = repoEngTareas.GetDescripcionCircuito(id_solicitud);
            return descripcionCircuito;
        }
        public IEnumerable<ENG_CircuitosDTO> GetDescripcionCircuitos(List<int> lstIdCircuitoExcluir)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());
            var descripcionCircuito = repoEngTareas.GetDescripcionCircuitos(lstIdCircuitoExcluir);
            var elementsDto = mapperBaseENG_Circuitos.Map<IEnumerable<ENG_Circuitos>, IEnumerable<ENG_CircuitosDTO>>(descripcionCircuito);
            return elementsDto;
        }
        public ENG_CircuitosDTO GetCircuitobyId(int idCircuito)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());
            var descripcionCircuito = repoEngTareas.GetCircuitobyId(idCircuito);
            var elementsDto = mapperBaseENG_Circuitos.Map<ENG_Circuitos, ENG_CircuitosDTO>(descripcionCircuito);
            return elementsDto;
        }
        public DispoEntity GetDatosDispoTransmision(int id_solicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());
            var dispo = repoEngTareas.GetDatosDispoTransmision(id_solicitud);
            return dispo;
        }

        internal int GetIdCircuitoByIdEncomienda(int id_encomienda)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());
            var dispo = repoEngTareas.GetIdCircuitoByIdEncomienda(id_encomienda);
            return dispo;
        }

        public bool UsuarioTienePermisoTarea(int idTarea, Guid usuario)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repoEngTareas = new EngTareasRepository(this.uowF.GetUnitOfWork());
            bool tienePermiso = repoEngTareas.UsuarioTienePermisoTarea(idTarea, usuario);
            return tienePermiso;
        }
    }
}
