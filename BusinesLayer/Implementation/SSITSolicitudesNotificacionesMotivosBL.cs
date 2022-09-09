using AutoMapper;
using BaseRepository;
using DataAcess;
using DataTransferObject;
using IBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;


namespace BusinesLayer.Implementation
{
    public class SSITSolicitudesNotificacionesMotivosBL : SSITSolicitudesNotificacionesMotivosBL<MotivoNotificacionDTO>
    {
        private SSITSolicitudesNotificacionesMotivoRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public SSITSolicitudesNotificacionesMotivosBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SSIT_Solicitudes_Notificaciones_motivos, MotivoNotificacionDTO>()
                    .ForMember(dest => dest.IdNotificacionMotivo, source => source.MapFrom(p => p.IdNotificacionMotivo))
                    .ForMember(dest => dest.NotificacionMotivo, source => source.MapFrom(p => p.NotificacionMotivo));
            });

            mapperBase = config.CreateMapper();
        }
        public void Delete(MotivoNotificacionDTO objectDto)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<MotivoNotificacionDTO> GetAll()
        {
            throw new NotImplementedException();
        }
        public bool Insert(MotivoNotificacionDTO objectDto)
        {
            throw new NotImplementedException();
        }
        public MotivoNotificacionDTO Single(int Id)
        {
            throw new NotImplementedException();
        }
        public void Get(MotivoNotificacionDTO objectDto)
        {
            throw new NotImplementedException();
        }
        public void Update(MotivoNotificacionDTO objectDto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MotivoNotificacionDTO> getAllMotivos()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesNotificacionesMotivoRepository(this.uowF.GetUnitOfWork());
                var elements = repo.getAllMotivos();
                var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Notificaciones_motivos>, IEnumerable<MotivoNotificacionDTO>>(elements);
                return elementsDto.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
