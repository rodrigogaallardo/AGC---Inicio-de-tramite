using IBusinessLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnitOfWork;
using DataTransferObject;
using AutoMapper;
using BaseRepository;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;
using StaticClass;

namespace BusinesLayer.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class RelRubrosCNSolicitudesNuevasBL : IRelRubrosCNSolicitudesNuevasBL<RelRubrosSolicitudesNuevasDTO>
    {
        private RelRubrosSolicitudesNuevasRepository repo = null;

        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public RelRubrosCNSolicitudesNuevasBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RelRubrosSolicitudesNuevasDTO, Rel_Rubros_Solicitudes_Nuevas>().ReverseMap()
                    .ForMember(dest => dest.idRelRubSol, source => source.MapFrom(p => p.id_relrubSol))
                    .ForMember(dest => dest.codigo, source => source.MapFrom(p => p.Codigo))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_Solicitud))
                    .ForMember(dest => dest.Superficie, source => source.MapFrom(p => p.Superficie));

                cfg.CreateMap<Rel_Rubros_Solicitudes_Nuevas, RelRubrosSolicitudesNuevasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_relrubSol, source => source.MapFrom(p => p.idRelRubSol))
                    .ForMember(dest => dest.Codigo, source => source.MapFrom(p => p.codigo))
                    .ForMember(dest => dest.id_Solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.Superficie, source => source.MapFrom(p => p.Superficie));

                

            });
            mapperBase = config.CreateMapper();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RelRubrosSolicitudesNuevasDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RelRubrosSolicitudesNuevasRepository(this.uowF.GetUnitOfWork());
                var entityRubros = repo.GetAll().ToList();
                var lstMenuesDto = mapperBase.Map<List<Rel_Rubros_Solicitudes_Nuevas>, IEnumerable<RelRubrosSolicitudesNuevasDTO>>(entityRubros);
                return lstMenuesDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<RelRubrosSolicitudesNuevasDTO> GetRByIdSolicitud(int IdSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RelRubrosSolicitudesNuevasRepository(this.uowF.GetUnitOfWork());
                var entityRubros = repo.GetRubrosByFKIdSolicitud(IdSolicitud).ToList();
                var lstMenuesDto = mapperBase.Map<List<Rel_Rubros_Solicitudes_Nuevas>, IEnumerable<RelRubrosSolicitudesNuevasDTO>>(entityRubros);
                return lstMenuesDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Insert(RelRubrosSolicitudesNuevasDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);


                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new RelRubrosSolicitudesNuevasRepository(unitOfWork);

                    var elementEntitySol = mapperBase.Map<RelRubrosSolicitudesNuevasDTO, Rel_Rubros_Solicitudes_Nuevas>(objectDto);
                    var insertSolOk = repo.Insert(elementEntitySol);


                    unitOfWork.Commit();
                    objectDto.IdSolicitud = elementEntitySol.id_relrubSol;
                    return elementEntitySol.id_relrubSol;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(RelRubrosSolicitudesNuevasDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new RelRubrosSolicitudesNuevasRepository(unitOfWork);
                    var elementDto = mapperBase.Map<RelRubrosSolicitudesNuevasDTO, Rel_Rubros_Solicitudes_Nuevas>(objectDto);
                    var insertOk = repo.Delete(elementDto);
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
