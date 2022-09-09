using AutoMapper;
using BaseRepository;
using Dal.UnitOfWork;
using DataAcess;
using DataAcess.EntityCustom;
using DataTransferObject;
using IBusinessLayer;
using System;
using System.Collections.Generic;
using UnitOfWork;
namespace BusinesLayer.Implementation
{
    public class TransferenciasNotificacionesBL : ITransferenciasNotificacionesBL<TransferenciasNotificacionesDTO>
    {
        private IUnitOfWorkFactory uowF = null;
        private TransferenciasNotificacionesRepository repo = null;
        IMapper mapperBase;

        public TransferenciasNotificacionesBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AvisoNotificacionDTO, AvisoNotificacionEntity>().ReverseMap();
                cfg.CreateMap<TransferenciasNotificacionesDTO, Transf_Solicitudes_Notificaciones>()
                    .ForMember(dest => dest.fechaNotificacionSSIT, source => source.MapFrom(p => p.fechaNotificacionSSIT))
                    .ForMember(dest => dest.id_notificacion, source => source.MapFrom(p => p.id_notificacion))
                    .ForMember(dest => dest.id_email, source => source.MapFrom(p => p.id_email))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.id_solicitud));

                cfg.CreateMap<Transf_Solicitudes_Notificaciones, TransferenciasNotificacionesDTO>()
                    .ForMember(dest => dest.fechaNotificacionSSIT, source => source.MapFrom(p => p.fechaNotificacionSSIT))
                    .ForMember(dest => dest.id_notificacion, source => source.MapFrom(p => p.id_notificacion))
                    .ForMember(dest => dest.id_email, source => source.MapFrom(p => p.id_email))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.id_solicitud));
            });

            mapperBase = config.CreateMapper();
        }

        public IEnumerable<TransferenciasNotificacionesDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Insert(TransferenciasNotificacionesDTO objectDto)
        {
            throw new NotImplementedException();
        }

        public void Update(TransferenciasNotificacionesDTO objectDto)
        {
            throw new NotImplementedException();
        }

        public void Delete(TransferenciasNotificacionesDTO objectDto)
        {
            throw new NotImplementedException();
        }

        public TransferenciasNotificacionesDTO Single(int Id)
        {
            throw new NotImplementedException();
        }

        public void InsertNotificacionByIdTransferencia(TransferenciasSolicitudesDTO sol, int id_email, int idMotivoNotificacion)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new TransferenciasNotificacionesRepository(this.uowF.GetUnitOfWork());

                    var notif = new TransferenciasNotificacionesDTO();

                    notif.id_solicitud = sol.IdSolicitud;
                    notif.id_email = id_email;
                    notif.createDate = DateTime.Now;
                    notif.Id_NotificacionMotivo = idMotivoNotificacion;

                    var elementDto = mapperBase.Map<TransferenciasNotificacionesDTO, Transf_Solicitudes_Notificaciones>(notif);

                    repo.Insert(elementDto);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateNotificacionByIdNotificacion(int IdNotificacion)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new TransferenciasNotificacionesRepository(this.uowF.GetUnitOfWork());

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

        public void InsertNotificacionByIdSolicitud(int id_solicitud, int idMail, int idMotivoNotificacion)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new TransferenciasNotificacionesRepository(this.uowF.GetUnitOfWork());
                    var notif = new TransferenciasNotificacionesDTO();

                    notif.id_solicitud = id_solicitud;
                    notif.id_email = idMail;
                    notif.createDate = DateTime.Now;
                    notif.Id_NotificacionMotivo = idMotivoNotificacion;
                    var entity = mapperBase.Map<TransferenciasNotificacionesDTO, Transf_Solicitudes_Notificaciones>(notif);
                    repo.Insert(entity);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
