using AutoMapper;
using BaseRepository;
using DataAcess;
using IBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;
using DataTransferObject;
using Dal.UnitOfWork;

namespace BusinesLayer.Implementation
{
    public class SSITSolicitudesAvisoCaducidadBL : ISSITSolicitudesAvisoCaducidadBL<SSIT_Solicitudes_AvisoCaducidadDTO>
    {
        private IUnitOfWorkFactory uowF = null;
        private SSIT_Solicitudes_AvisoCaducidadRepository repo = null;
        IMapper mapperBase;

        public SSITSolicitudesAvisoCaducidadBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SSIT_Solicitudes_AvisoCaducidad, SSIT_Solicitudes_AvisoCaducidadDTO>()
                    .ForMember(dest => dest.fechaNotificacionSSIT, source => source.MapFrom(p => p.fechaNotificacionSSIT))
                    .ForMember(dest => dest.id_aviso, source => source.MapFrom(p => p.id_aviso))
                    .ForMember(dest => dest.id_email, source => source.MapFrom(p => p.id_email))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.id_solicitud));

                cfg.CreateMap<SSIT_Solicitudes_AvisoCaducidadDTO, SSIT_Solicitudes_AvisoCaducidad>()
                    .ForMember(dest => dest.fechaNotificacionSSIT, source => source.MapFrom(p => p.fechaNotificacionSSIT))
                    .ForMember(dest => dest.id_aviso, source => source.MapFrom(p => p.id_aviso))
                    .ForMember(dest => dest.id_email, source => source.MapFrom(p => p.id_email))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.id_solicitud));
            });

            mapperBase = config.CreateMapper();
        }
        public IEnumerable<SSIT_Solicitudes_AvisoCaducidadDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Insert(SSIT_Solicitudes_AvisoCaducidadDTO objectDto)
        {
            throw new NotImplementedException();
        }

        public void Update(SSIT_Solicitudes_AvisoCaducidadDTO objectDto)
        {
            throw new NotImplementedException();
        }

        public void Delete(SSIT_Solicitudes_AvisoCaducidadDTO objectDto)
        {
            throw new NotImplementedException();
        }

        public SSIT_Solicitudes_AvisoCaducidadDTO Single(int Id)
        {
            throw new NotImplementedException();
        }

        public void UpdateNotificacionByUserId(Guid userid)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSIT_Solicitudes_AvisoCaducidadRepository(this.uowF.GetUnitOfWork());
                    var elements = repo.GetNotificacionByUserId(userid);
                    var elementDTO = mapperBase.Map<IEnumerable<SSIT_Solicitudes_AvisoCaducidad>, IEnumerable<SSIT_Solicitudes_AvisoCaducidadDTO>>(elements);
                    foreach (var item in elements)
                    {                        
                        item.fechaNotificacionSSIT = DateTime.Now;
                        repo.Update(item);
                    }
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
