using System;
using System.Collections.Generic;
using DataAcess;
using DataTransferObject;
using BaseRepository;
using AutoMapper;
using System.Linq;
using UnitOfWork;
using IBusinessLayer;
using DataAcess.EntityCustom;

namespace BusinesLayer.Implementation
{
    public class FirmantesBL : IFirmantesBL
    {
        public static string firmantesInexistentes ="";
        private FirmantesRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
        IMapper mapperBaseFirmantesPJEntity;

        public FirmantesBL()
        {
            var configFirmantesPJEntity = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FirmantesPJDTO, FirmantesPJEntity>().ReverseMap();
            });

            var config = new MapperConfiguration(cfg =>
            {            
                cfg.CreateMap<FirmantesDTO, FirmantesEntity>().ReverseMap()
                    .ForMember(dest => dest.id_firmante, source => source.MapFrom(p => p.IdFirmante))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NroDocumento))
                    .ForMember(dest => dest.nom_tipocaracter, source => source.MapFrom(p => p.NomTipoCaracter))
                    .ForMember(dest => dest.cargo_firmante_pj, source => source.MapFrom(p => p.CargoFirmante));

                cfg.CreateMap<FirmantesEntity, FirmantesDTO>().ReverseMap()
                    .ForMember(dest => dest.IdFirmante, source => source.MapFrom(p => p.id_firmante))
                    .ForMember(dest => dest.NroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.NomTipoCaracter, source => source.MapFrom(p => p.nom_tipocaracter))
                    .ForMember(dest => dest.CargoFirmante, source => source.MapFrom(p => p.cargo_firmante_pj));
            });
            mapperBase = config.CreateMapper();
            mapperBaseFirmantesPJEntity = configFirmantesPJEntity.CreateMapper();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_encomienda"></param>
        /// <returns></returns>
        public IEnumerable<FirmantesDTO> GetFirmantesEncomienda(int id_encomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new FirmantesRepository(this.uowF.GetUnitOfWork());

                var firmantes = repo.GetFirmantesEncomienda(id_encomienda);
                if (firmantes != null)
                {
                    var elementsDto = mapperBase.Map<IEnumerable<FirmantesEntity>, IEnumerable<FirmantesDTO>>(firmantes);
                    return elementsDto;
                }
                else
                {
                    firmantesInexistentes = string.Format("No existen firmantes para la encomienda: " + Convert.ToString(id_encomienda));
                    throw new Exception(firmantesInexistentes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public IEnumerable<FirmantesDTO> GetFirmantesPJEncomienda(int id_encomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new FirmantesRepository(this.uowF.GetUnitOfWork());

                var firmantes = repo.GetFirmantesPJEncomienda(id_encomienda);
                if (firmantes != null)
                {
                    var elementsDto = mapperBase.Map<IEnumerable<FirmantesEntity>, IEnumerable<FirmantesDTO>>(firmantes);
                    return elementsDto;
                }
                else
                {
                    firmantesInexistentes = string.Format("No existen firmantes para la encomienda: " + Convert.ToString(id_encomienda));
                    throw new Exception(firmantesInexistentes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_encomienda"></param>
        /// <returns></returns>
        public IEnumerable<FirmantesDTO> GetFirmantesSolicitud(int id_solicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new FirmantesRepository(this.uowF.GetUnitOfWork());

                var firmantes = repo.GetFirmantesSolicitud(id_solicitud);
                if (firmantes != null)
                {
                    var elementsDto = mapperBase.Map<IEnumerable<FirmantesEntity>, IEnumerable<FirmantesDTO>>(firmantes);
                    return elementsDto;
                }
                else
                {
                    firmantesInexistentes = string.Format("No existen firmantes para la encomienda: " + Convert.ToString(id_solicitud));
                    throw new Exception(firmantesInexistentes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_encomienda"></param>
        /// <returns></returns>
        public IEnumerable<FirmantesDTO> GetFirmantesTransferencias(int IdSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new FirmantesRepository(this.uowF.GetUnitOfWork());

                var firmantes = repo.GetFirmantesTransferencias(IdSolicitud);
                
                var elementsDto = mapperBase.Map<IEnumerable<FirmantesEntity>, IEnumerable<FirmantesDTO>>(firmantes);
                return elementsDto;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<FirmantesDTO> GetFirmantesTransferenciasANT(int IdSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new FirmantesRepository(this.uowF.GetUnitOfWork());

                var firmantes = repo.GetFirmantesTransferenciasANT(IdSolicitud);

                var elementsDto = mapperBase.Map<IEnumerable<FirmantesEntity>, IEnumerable<FirmantesDTO>>(firmantes);
                return elementsDto;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<FirmantesPJDTO> GetFirmantesPJPF(int id_firmante_pj)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new FirmantesRepository(this.uowF.GetUnitOfWork());

                var firmantes = repo.GetFirmantesPJPF(id_firmante_pj);
                if (firmantes != null)
                {
                    var elementsDto = mapperBaseFirmantesPJEntity.Map<IEnumerable<FirmantesPJEntity>, IEnumerable<FirmantesPJDTO>>(firmantes);
                    return elementsDto;
                }
                else
                {
                    firmantesInexistentes = string.Format("No existen firmantes con el ID: " + Convert.ToString(id_firmante_pj));
                    throw new Exception(firmantesInexistentes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public IEnumerable<FirmantesPJDTO> GetFirmantesPJPFSolicitud(int id_firmante_pj)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new FirmantesRepository(this.uowF.GetUnitOfWork());

                var firmantes = repo.GetFirmantesPJPFSolicitud(id_firmante_pj);
                if (firmantes != null)
                {
                    var elementsDto = mapperBaseFirmantesPJEntity.Map<IEnumerable<FirmantesPJEntity>, IEnumerable<FirmantesPJDTO>>(firmantes);
                    return elementsDto;
                }
                else
                {
                    firmantesInexistentes = string.Format("No existen firmantes con el ID: " + Convert.ToString(id_firmante_pj));
                    throw new Exception(firmantesInexistentes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_firmante_pj"></param>
        /// <returns></returns>
        public IEnumerable<FirmantesPJDTO> GetFirmantesTransferenciasPJ(int id_firmante_pj)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new FirmantesRepository(this.uowF.GetUnitOfWork());
                var firmantes = repo.GetFirmantesTransferenciasPJ(id_firmante_pj);                               
                var elementsDto = mapperBaseFirmantesPJEntity.Map<IEnumerable<FirmantesPJEntity>, IEnumerable<FirmantesPJDTO>>(firmantes);
                return elementsDto;
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<FirmantesPJDTO> GetTransfFirmantesPJPFSolicitudByIDSol(int idSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new FirmantesRepository(this.uowF.GetUnitOfWork());
                var firmantes = repo.GetTransfFirmantesPJPFSolicitudByIDSol(idSolicitud);
                var elementsDto = mapperBaseFirmantesPJEntity.Map<IEnumerable<FirmantesPJEntity>, IEnumerable<FirmantesPJDTO>>(firmantes);
                return elementsDto;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<FirmantesPJDTO> GetFirmantesTransferenciasPJANT(int id_firmante_pj)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new FirmantesRepository(this.uowF.GetUnitOfWork());
                var firmantes = repo.GetFirmantesTransferenciasPJANT(id_firmante_pj);
                var elementsDto = mapperBaseFirmantesPJEntity.Map<IEnumerable<FirmantesPJEntity>, IEnumerable<FirmantesPJDTO>>(firmantes);
                return elementsDto;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<FirmantesPJDTO> GetFirmantesPJ(int id_firmante_pj)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new FirmantesRepository(this.uowF.GetUnitOfWork());

                var firmantes = repo.GetFirmantesPJ(id_firmante_pj);
                if (firmantes != null)
                {
                    var elementsDto = mapperBaseFirmantesPJEntity.Map<IEnumerable<FirmantesPJEntity>, IEnumerable<FirmantesPJDTO>>(firmantes);
                    return elementsDto;
                }
                else
                {
                    firmantesInexistentes = string.Format("No existen firmantes con el ID: " + Convert.ToString(id_firmante_pj));
                    throw new Exception(firmantesInexistentes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<FirmantesPJDTO> GetFirmantesPJSolicitud(int id_firmante_pj)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new FirmantesRepository(this.uowF.GetUnitOfWork());

                var firmantes = repo.GetFirmantesPJSolicitud(id_firmante_pj);
                if (firmantes != null)
                {
                    var elementsDto = mapperBaseFirmantesPJEntity.Map<IEnumerable<FirmantesPJEntity>, IEnumerable<FirmantesPJDTO>>(firmantes);
                    return elementsDto;
                }
                else
                {
                    firmantesInexistentes = string.Format("No existen firmantes con el ID: " + Convert.ToString(id_firmante_pj));
                    throw new Exception(firmantesInexistentes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
