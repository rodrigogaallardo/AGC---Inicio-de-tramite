using AutoMapper;
using BaseRepository;
using Dal.UnitOfWork;
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
    public class SGITramitesTareasHabBL : ISGITramitesTareasHabBL<SGITramitesTareasHabDTO>
    {
        private SGITramitesTareasHabRepository repo = null;
        private SGITramitesTareasRepository repoTT = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public SGITramitesTareasHabBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SGITramitesTareasHabDTO, SGI_Tramites_Tareas_HAB>().ReverseMap();

                cfg.CreateMap<SGITramitesTareasDTO, SGI_Tramites_Tareas>().ReverseMap()
                    .ForMember(dest => dest.IdTramiteTarea, source => source.MapFrom(p => p.id_tramitetarea))
                    .ForMember(dest => dest.IdTarea, source => source.MapFrom(p => p.id_tarea))
                    .ForMember(dest => dest.IdResultado, source => source.MapFrom(p => p.id_resultado))
                    .ForMember(dest => dest.FechaInicioTramiteTarea, source => source.MapFrom(p => p.FechaInicio_tramitetarea))
                    .ForMember(dest => dest.FechaCierreTramiteTarea, source => source.MapFrom(p => p.FechaCierre_tramitetarea))
                    .ForMember(dest => dest.UsuarioAsignadoTramiteTarea, source => source.MapFrom(p => p.UsuarioAsignado_tramitetarea))
                    .ForMember(dest => dest.FechaAsignacionTramiteTarea, source => source.MapFrom(p => p.FechaAsignacion_tramtietarea))
                    .ForMember(dest => dest.IdProximaTarea, source => source.MapFrom(p => p.id_proxima_tarea));

                cfg.CreateMap<SGI_Tramites_Tareas, SGITramitesTareasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_tramitetarea, source => source.MapFrom(p => p.IdTramiteTarea))
                    .ForMember(dest => dest.id_tarea, source => source.MapFrom(p => p.IdTarea))
                    .ForMember(dest => dest.id_resultado, source => source.MapFrom(p => p.IdResultado))
                    .ForMember(dest => dest.FechaInicio_tramitetarea, source => source.MapFrom(p => p.FechaInicioTramiteTarea))
                    .ForMember(dest => dest.FechaCierre_tramitetarea, source => source.MapFrom(p => p.FechaCierreTramiteTarea))
                    .ForMember(dest => dest.UsuarioAsignado_tramitetarea, source => source.MapFrom(p => p.UsuarioAsignadoTramiteTarea))
                    .ForMember(dest => dest.FechaAsignacion_tramtietarea, source => source.MapFrom(p => p.FechaAsignacionTramiteTarea))
                    .ForMember(dest => dest.id_proxima_tarea, source => source.MapFrom(p => p.IdProximaTarea));
            });
            mapperBase = config.CreateMapper();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectDto"></param>
        public void Delete(SGITramitesTareasHabDTO sgiTTHabDTO, SGITramitesTareasDTO sgiTTDTO)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SGITramitesTareasHabRepository(unitOfWork);
                    repoTT = new SGITramitesTareasRepository(unitOfWork);
                    var elementEntityTTHab = mapperBase.Map<SGITramitesTareasHabDTO, SGI_Tramites_Tareas_HAB>(sgiTTHabDTO);
                    var elementEntityTT = mapperBase.Map<SGITramitesTareasDTO, SGI_Tramites_Tareas>(sgiTTDTO);
                    repo.Delete(elementEntityTTHab);
                    repoTT.Delete(elementEntityTT);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SGITramitesTareasHabDTO GetByFKIdTramiteTareasIdSolicitud(int id_tramitetarea, int id_solicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SGITramitesTareasHabRepository(this.uowF.GetUnitOfWork());

                var elements = repo.GetByFKIdTramiteTareasIdSolicitud(id_tramitetarea, id_solicitud);
                var elementsDto = mapperBase.Map<IEnumerable<SGI_Tramites_Tareas_HAB>, IEnumerable<SGITramitesTareasHabDTO>>(elements);
                return elementsDto.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Insert(SGITramitesTareasHabDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SGITramitesTareasHabRepository(unitOfWork);
                    var elementDto = mapperBase.Map<SGITramitesTareasHabDTO, SGI_Tramites_Tareas_HAB>(objectDto);
                    var insertOk = repo.Insert(elementDto);
                    unitOfWork.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(SGITramitesTareasHabDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SGITramitesTareasHabRepository(unitOfWork);
                    var elementDto = mapperBase.Map<SGITramitesTareasHabDTO, SGI_Tramites_Tareas_HAB>(objectDto);
                    repo.Update(elementDto);
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
