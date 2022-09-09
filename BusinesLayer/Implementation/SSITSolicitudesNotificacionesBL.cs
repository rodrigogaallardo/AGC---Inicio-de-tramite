using AutoMapper;
using BaseRepository;
using Dal.UnitOfWork;
using DataAcess;
using DataAcess.EntityCustom;
using DataTransferObject;
using ExternalService;
using IBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;
using System.Web.Security;
using StaticClass;

namespace BusinesLayer.Implementation
{
    public class SSITSolicitudesNotificacionesBL : ISSITSolicitudesNotificacionesBL<SSITSolicitudesNotificacionesDTO>
    {
        private IUnitOfWorkFactory uowF = null;
        private SSITSolicitudesNotificacionesRepository repo = null;
        IMapper mapperBase;

        public SSITSolicitudesNotificacionesBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SSIT_Solicitudes_Notificaciones, SSITSolicitudesNotificacionesDTO>()
                    .ForMember(dest => dest.fechaNotificacionSSIT, source => source.MapFrom(p => p.fechaNotificacionSSIT))
                    .ForMember(dest => dest.id_notificacion, source => source.MapFrom(p => p.id_notificacion))
                    .ForMember(dest => dest.id_email, source => source.MapFrom(p => p.id_email))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.id_solicitud));

                cfg.CreateMap<SSITSolicitudesNotificacionesDTO, SSIT_Solicitudes_Notificaciones>()
                    .ForMember(dest => dest.fechaNotificacionSSIT, source => source.MapFrom(p => p.fechaNotificacionSSIT))
                    .ForMember(dest => dest.id_notificacion, source => source.MapFrom(p => p.id_notificacion))
                    .ForMember(dest => dest.id_email, source => source.MapFrom(p => p.id_email))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.id_solicitud));

                cfg.CreateMap<SSIT_Solicitudes, SSITSolicitudesDTO>()
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.id_tipotramite))
                    .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.id_tipoexpediente))
                    .ForMember(dest => dest.IdSubTipoExpediente, source => source.MapFrom(p => p.id_subtipoexpediente))
                    .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                    .ForMember(dest => dest.Telefono, source => source.MapFrom(p => p.telefono))
                    .ForMember(dest => dest.TipoEstadoSolicitudDTO, source => source.MapFrom(p => p.TipoEstadoSolicitud))
                    //.ForMember(dest => dest.SubTipoExpedienteDTO, source => source.MapFrom(p => p.SubtipoExpediente))
                    //.ForMember(dest => dest.TipoExpedienteDTO, source => source.MapFrom(p => p.TipoExpediente))
                    //.ForMember(dest => dest.TipoTramiteDTO, source => source.MapFrom(p => p.TipoTramite))
                    //.ForMember(dest => dest.SSITSolicitudesPagosDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Pagos))
                    .ForMember(dest => dest.SSITDocumentosAdjuntosDTO, source => source.MapFrom(p => p.SSIT_DocumentosAdjuntos))
                    .ForMember(dest => dest.SSITSolicitudesUbicacionesDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Ubicaciones))
                    //.ForMember(dest => dest.SSITSolicitudesHistorialEstadosDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_HistorialEstados))
                    //.ForMember(dest => dest.SSITSolicitudesTitularesPersonasFisicasDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Titulares_PersonasFisicas))
                    //.ForMember(dest => dest.SSITSolicitudesOrigenDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Origen))
                    .ForMember(dest => dest.EncomiendaSSITSolicitudesDTO, source => source.MapFrom(p => p.Encomienda_SSIT_Solicitudes))
                    .ForMember(dest => dest.SSITSolicitudesOrigenDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Origen))
                    ;

                cfg.CreateMap<SSITSolicitudesDTO, SSIT_Solicitudes>()
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_tipotramite, source => source.MapFrom(p => p.IdTipoTramite))
                    .ForMember(dest => dest.id_tipoexpediente, source => source.MapFrom(p => p.IdTipoExpediente))
                    .ForMember(dest => dest.id_subtipoexpediente, source => source.MapFrom(p => p.IdSubTipoExpediente))
                    .ForMember(dest => dest.id_estado, source => source.MapFrom(p => p.IdEstado))
                    .ForMember(dest => dest.telefono, source => source.MapFrom(p => p.Telefono))
                    .ForMember(dest => dest.TipoEstadoSolicitud, source => source.Ignore())
                    .ForMember(dest => dest.SubtipoExpediente, source => source.Ignore())
                    .ForMember(dest => dest.SGI_Tramites_Tareas_HAB, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_AvisoCaducidad, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_Encomienda, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_Firmantes_PersonasFisicas, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_Firmantes_PersonasJuridicas, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_Observaciones, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_Pagos, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas, source => source.Ignore())
                    .ForMember(dest => dest.TipoExpediente, source => source.Ignore())
                    .ForMember(dest => dest.TipoTramite, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_Pagos, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_DocumentosAdjuntos, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_Ubicaciones, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_HistorialEstados, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_Titulares_PersonasFisicas, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_Titulares_PersonasJuridicas, source => source.Ignore())
                    .ForMember(dest => dest.Encomienda_SSIT_Solicitudes, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_Origen, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_Origen, source => source.MapFrom(p => p.SSITSolicitudesOrigenDTO))
                    ;

                cfg.CreateMap<SSIT_Solicitudes_Origen, SSITSolicitudesOrigenDTO>()
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.id_solicitud_origen, source => source.MapFrom(p => p.id_solicitud_origen))
                    .ForMember(dest => dest.id_transf_origen, source => source.MapFrom(p => p.id_transf_origen))
                    ;

                cfg.CreateMap<SSITSolicitudesOrigenDTO, SSIT_Solicitudes_Origen>()
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.id_solicitud_origen, source => source.MapFrom(p => p.id_solicitud_origen))
                    .ForMember(dest => dest.id_transf_origen, source => source.MapFrom(p => p.id_transf_origen))
                    ;

                cfg.CreateMap<AvisoNotificacionDTO, AvisoNotificacionEntity>().ReverseMap();
            });

            mapperBase = config.CreateMapper();
        }

        public IEnumerable<AvisoNotificacionDTO> GetAvisoNotificaciones(Guid userid, int id_solicitud, int id_motivo, int startRowIndex, int maximumRows, string sortByExpression, out int totalRowCount)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesNotificacionesRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetAvisoNotificaciones(userid);

            if (id_solicitud > 0)
                elements = elements.Where(idSol => idSol.IdTramite == id_solicitud);

            if (id_motivo > 0)

                elements = elements.Where(not => not.Id_NotificacionMotivo == id_motivo);

            totalRowCount = elements.Count();
            elements = elements.OrderByDescending(o => o.CreateDate).Skip(startRowIndex).Take(maximumRows);
            var elementsDTO = mapperBase.Map<IEnumerable<AvisoNotificacionEntity>, IEnumerable<AvisoNotificacionDTO>>(elements);

            return elementsDTO;
        }

        public int GetCantidadNotificacionesByUser(Guid userid)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesNotificacionesRepository(this.uowF.GetUnitOfWork());
            int elements = repo.GetCantidadNotificacionesByUser(userid);

            return elements;
        }

        public IEnumerable<AvisoNotificacionDTO> GetAvisoNotificaciones(Guid userid, int startRowIndex, int maximumRows, string sortByExpression, out int totalRowCount)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesNotificacionesRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetAvisoNotificaciones(userid);

            totalRowCount = elements.Count();
            elements = elements.OrderBy(o => o.FechaNotificacion).Skip(startRowIndex).Take(maximumRows);
            var elementsDTO = mapperBase.Map<IEnumerable<AvisoNotificacionEntity>, IEnumerable<AvisoNotificacionDTO>>(elements);

            return elementsDTO;
        }

        public List<SSITSolicitudesNotificacionesDTO> GetNotificacionesByIdSolicitud(int id_solicitud)
        {
            throw new NotImplementedException();
        }

        public void Delete(SSITSolicitudesNotificacionesDTO objectDto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SSITSolicitudesNotificacionesDTO> GetAll()
        {
            throw new NotImplementedException();
        }


        public SSITSolicitudesNotificacionesDTO Single(int Id)
        {
            throw new NotImplementedException();
        }

        public void Update(SSITSolicitudesNotificacionesDTO objectDto)
        {
            throw new NotImplementedException();
        }
        public bool Insert(SSITSolicitudesNotificacionesDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesNotificacionesRepository(this.uowF.GetUnitOfWork());
                    var notif = mapperBase.Map<SSITSolicitudesNotificacionesDTO, SSIT_Solicitudes_Notificaciones>(objectDto);

                    repo.Insert(notif);
                    unitOfWork.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void UpdateNotificacionByUserId(Guid userid)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesNotificacionesRepository(this.uowF.GetUnitOfWork());
                    var elements = repo.GetNotificacionByUserId(userid);
                    var elementDTO = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Notificaciones>, IEnumerable<SSITSolicitudesNotificacionesDTO>>(elements);
                    foreach (var item in elements)
                    {
                        //126517: JADHE 47310 - SSIT - Visualizacion de Notificaciones en SGI SSIT Modificacion
                        DateTime fechaMin = Convert.ToDateTime("02-08-2018").Date;
                        if ((item.fechaNotificacionSSIT >= fechaMin) || (item.fechaNotificacionSSIT == null))
                        {
                            item.fechaNotificacionSSIT = DateTime.Now;
                            repo.Update(item);
                        }
                    }
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertNotificacionByIdSolicitud(SSITSolicitudesDTO sol, int id_email, int idMotivoNotificacion)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesNotificacionesRepository(this.uowF.GetUnitOfWork());

                    var SSIT_sol = mapperBase.Map<SSIT_Solicitudes>(sol);

                    var notif = new SSITSolicitudesNotificacionesDTO();

                    notif.id_solicitud = sol.IdSolicitud;
                    notif.id_email = id_email;
                    notif.createDate = DateTime.Now;
                    notif.Id_NotificacionMotivo = idMotivoNotificacion;

                    var elementDto = mapperBase.Map<SSITSolicitudesNotificacionesDTO, SSIT_Solicitudes_Notificaciones>(notif);

                    repo.Insert(elementDto);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertNotificacionByIdSolicitud(SSITSolicitudesNuevasDTO sol, int id_email, int idMotivoNotificacion)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesNotificacionesRepository(this.uowF.GetUnitOfWork());

                    var SSIT_sol = mapperBase.Map<SSIT_Solicitudes_Nuevas>(sol);

                    var notif = new SSITSolicitudesNotificacionesDTO();

                    notif.id_solicitud = sol.IdSolicitud;
                    notif.id_email = id_email;
                    notif.createDate = DateTime.Now;
                    notif.Id_NotificacionMotivo = idMotivoNotificacion;

                    var elementDto = mapperBase.Map<SSITSolicitudesNotificacionesDTO, SSIT_Solicitudes_Notificaciones>(notif);

                    repo.Insert(elementDto);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //IdNotificacion
        public void UpdateNotificacionByIdNotificacion(int IdNotificacion)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesNotificacionesRepository(this.uowF.GetUnitOfWork());

                    var element = repo.GetNotificacionByIdNotificacion(IdNotificacion);
                    element.fechaNotificacionSSIT = DateTime.Now;
                    repo.Update(element);

                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string getMotivoById(int idMotivoNotificacion)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesNotificacionesRepository(this.uowF.GetUnitOfWork());
            return repo.getMotivoById(idMotivoNotificacion);
        }
    }
}
