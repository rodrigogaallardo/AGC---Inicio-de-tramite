using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBusinessLayer;
using UnitOfWork;
using BaseRepository;
using DataAcess;
using AutoMapper;
using DataAcess.EntityCustom;
using Dal.UnitOfWork;

namespace BusinesLayer.Implementation
{
    public class MailsBL : IMailsBL<EMailDTO>
    {
        private IUnitOfWorkFactory uowF = null;
        private MailsRepository repo = null;
        IMapper mapperBase;

        public MailsBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Emails, EMailDTO>()
                    .ForMember(dest => dest.Mail_Asunto, source => source.MapFrom(p => p.asunto))
                    .ForMember(dest => dest.Mail_Email, source => source.MapFrom(p => p.email))
                    .ForMember(dest => dest.Mail_Estado, source => source.MapFrom(p => p.id_estado))
                    .ForMember(dest => dest.Mail_Fecha, source => source.MapFrom(p => p.fecha_lectura))
                    .ForMember(dest => dest.Mail_FechaAlta, source => source.MapFrom(p => p.fecha_alta))
                    .ForMember(dest => dest.Mail_FechaEnvio, source => source.MapFrom(p => p.fecha_envio))
                    .ForMember(dest => dest.Mail_Html, source => source.MapFrom(p => p.html))
                    .ForMember(dest => dest.Mail_ID, source => source.MapFrom(p => p.id_email))
                    .ForMember(dest => dest.Mail_Intentos, source => source.MapFrom(p => p.cant_intentos))
                    .ForMember(dest => dest.Mail_Prioridad, source => source.MapFrom(p => p.prioridad));

                cfg.CreateMap<EMailDTO, Emails>()
                    .ForMember(dest => dest.asunto, source => source.MapFrom(p => p.Mail_Asunto))
                    .ForMember(dest => dest.email, source => source.MapFrom(p => p.Mail_Email))
                    .ForMember(dest => dest.id_estado, source => source.MapFrom(p => p.Mail_Estado))
                    .ForMember(dest => dest.fecha_lectura, source => source.MapFrom(p => p.Mail_Fecha))
                    .ForMember(dest => dest.fecha_alta, source => source.MapFrom(p => p.Mail_FechaAlta))
                    .ForMember(dest => dest.fecha_envio, source => source.MapFrom(p => p.Mail_FechaEnvio))
                    .ForMember(dest => dest.html, source => source.MapFrom(p => p.Mail_Html))
                    .ForMember(dest => dest.id_email, source => source.MapFrom(p => p.Mail_ID))
                    .ForMember(dest => dest.cant_intentos, source => source.MapFrom(p => p.Mail_Intentos))
                    .ForMember(dest => dest.prioridad, source => source.MapFrom(p => p.Mail_Prioridad));

                cfg.CreateMap<EMailDTO, clsItemGrillaMails>()
                    .ForMember(dest => dest.Mail_Asunto, source => source.MapFrom(p => p.Mail_Email))
                    .ForMember(dest => dest.Mail_Email, source => source.MapFrom(p => p.Mail_Email))
                    .ForMember(dest => dest.Mail_Estado, source => source.MapFrom(p => p.Mail_Estado))
                    .ForMember(dest => dest.Mail_Fecha, source => source.MapFrom(p => p.Mail_Fecha))
                    .ForMember(dest => dest.Mail_FechaAlta, source => source.MapFrom(p => p.Mail_FechaAlta))
                    .ForMember(dest => dest.Mail_FechaEnvio, source => source.MapFrom(p => p.Mail_FechaEnvio))
                    .ForMember(dest => dest.Mail_Html, source => source.MapFrom(p => p.Mail_Html))
                    .ForMember(dest => dest.Mail_ID, source => source.MapFrom(p => p.Mail_ID))
                    .ForMember(dest => dest.Mail_Intentos, source => source.MapFrom(p => p.Mail_Intentos))
                    .ForMember(dest => dest.Mail_Prioridad, source => source.MapFrom(p => p.Mail_Prioridad));

                cfg.CreateMap<clsItemGrillaMails, EMailDTO>()
                    .ForMember(dest => dest.Mail_Asunto, source => source.MapFrom(p => p.Mail_Asunto))
                    .ForMember(dest => dest.Mail_Email, source => source.MapFrom(p => p.Mail_Email))
                    .ForMember(dest => dest.Mail_Estado, source => source.MapFrom(p => p.Mail_Estado))
                    .ForMember(dest => dest.Mail_Fecha, source => source.MapFrom(p => p.Mail_Fecha))
                    .ForMember(dest => dest.Mail_FechaAlta, source => source.MapFrom(p => p.Mail_FechaAlta))
                    .ForMember(dest => dest.Mail_FechaEnvio, source => source.MapFrom(p => p.Mail_FechaEnvio))
                    .ForMember(dest => dest.Mail_Html, source => source.MapFrom(p => p.Mail_Html))
                    .ForMember(dest => dest.Mail_ID, source => source.MapFrom(p => p.Mail_ID))
                    .ForMember(dest => dest.Mail_Intentos, source => source.MapFrom(p => p.Mail_Intentos))
                    .ForMember(dest => dest.Mail_Prioridad, source => source.MapFrom(p => p.Mail_Prioridad));

            });

            mapperBase = config.CreateMapper();
        }

        public object GetNotificacionesByTransferencia(int id_solicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new MailsRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetNotificacionesByTransferencia(id_solicitud);
                var elementsDto = mapperBase.Map<IEnumerable<clsItemGrillaMails>, IEnumerable<EMailDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<EMailDTO> GetNotificacionesByIdSolicitud(int id_solicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new MailsRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetNotificacionesByIdSolicitud(id_solicitud);
                var elementsDto = mapperBase.Map<IEnumerable<clsItemGrillaMails>, IEnumerable<EMailDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void Delete(EMailDTO objectDto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EMailDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Insert(EMailDTO objectDto)
        {
            throw new NotImplementedException();
        }

        public EMailDTO Single(int idMail)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new MailsRepository(this.uowF.GetUnitOfWork());
                var ele = repo.Single(idMail);
                var mailDTO = mapperBase.Map<EMailDTO>(ele);
                return mailDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(EMailDTO objectDto)
        {
            throw new NotImplementedException();
        }

    }
}