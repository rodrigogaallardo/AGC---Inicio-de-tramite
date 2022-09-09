using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseRepository;
using UnitOfWork;
using AutoMapper;
using DataAcess;
using DataTransferObject;
using Dal.UnitOfWork;

namespace BusinesLayer.Implementation
{
    public class PermisosBL
    {
        private SSITSolicitudesRepository repoSol = null;
        private SSITPermisosDatosAdicionalesRepository repoDatAdi = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public PermisosBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                #region "SSIT_Solicitudes"
                cfg.CreateMap<SSIT_Solicitudes, SSITSolicitudesDTO>()
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.id_tipotramite))
                    .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.id_tipoexpediente))
                    .ForMember(dest => dest.IdSubTipoExpediente, source => source.MapFrom(p => p.id_subtipoexpediente))
                    .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                    .ForMember(dest => dest.Telefono, source => source.MapFrom(p => p.telefono))
                    .ForMember(dest => dest.TipoEstadoSolicitudDTO, source => source.MapFrom(p => p.TipoEstadoSolicitud))
                    .ForMember(dest => dest.SSITDocumentosAdjuntosDTO, source => source.MapFrom(p => p.SSIT_DocumentosAdjuntos))
                    .ForMember(dest => dest.SSITSolicitudesUbicacionesDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Ubicaciones))
                    //.ForMember(dest => dest.EncomiendaSSITSolicitudesDTO, source => source.MapFrom(p => p.Encomienda_SSIT_Solicitudes))
                    .ForMember(dest => dest.TipoTramiteDescripcion, source => source.MapFrom(p => p.TipoTramite.descripcion_tipotramite))
                    .ForMember(dest => dest.TipoExpedienteDescripcion, source => source.MapFrom(p => p.TipoExpediente.descripcion_tipoexpediente))
                    .ForMember(dest => dest.SubTipoExpedienteDescripcion, source => source.MapFrom(p => p.SubtipoExpediente.descripcion_subtipoexpediente))
                    .ForMember(dest => dest.SSITSolicitudesOrigenDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Origen))
                    .ForMember(dest => dest.SSITSolicitudesRubrosCNDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_RubrosCN))
                    .ForMember(dest => dest.SSITSolicitudesDatosLocalDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_DatosLocal))
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
                    // .ForMember(dest => dest.Encomienda_SSIT_Solicitudes, source => source.Ignore())
                    .ForMember(dest => dest.SSIT_Solicitudes_Origen, source => source.MapFrom(p => p.SSITSolicitudesOrigenDTO))
                   ;

                cfg.CreateMap<SSITPermisosDatosAdicionalesDTO, SSIT_Permisos_DatosAdicionales>().ReverseMap();


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

                #endregion
            });

            mapperBase = config.CreateMapper();
        }

        public void Update(SSITSolicitudesDTO objectDTO)
        {
            try
            {

                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repoSol = new SSITSolicitudesRepository(uowF.GetUnitOfWork());
                    repoDatAdi = new SSITPermisosDatosAdicionalesRepository(uowF.GetUnitOfWork());
                    

                    if(objectDTO .SSITPermisosDatosAdicionalesDTO != null)
                    {
                        var entityDatAdi = mapperBase.Map<SSITPermisosDatosAdicionalesDTO, SSIT_Permisos_DatosAdicionales>(objectDTO.SSITPermisosDatosAdicionalesDTO);
                        if (repoDatAdi.GetByFKIdSolicitud(objectDTO.IdSolicitud).Count() > 0)
                            repoDatAdi.Update(entityDatAdi);
                        else
                            repoDatAdi.Insert(entityDatAdi);
                    }
                    
                    var elementDTO = mapperBase.Map<SSITSolicitudesDTO, SSIT_Solicitudes>(objectDTO);

                    repoSol.Update(elementDTO);
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