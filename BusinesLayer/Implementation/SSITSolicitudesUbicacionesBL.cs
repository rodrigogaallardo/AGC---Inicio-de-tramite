using AutoMapper;
using BaseRepository;
using IBusinessLayer;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;
using DataAcess;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitOfWork;
using StaticClass;
using System.Web.Security;

namespace BusinesLayer.Implementation
{

    public class SSITSolicitudesUbicacionesBL : ISSITSolicitudesUbicacionesBL<SSITSolicitudesUbicacionesDTO>
    {
        public static string solicitudConAnexoEnEstadoNoPermitido = "";
        private SSITSolicitudesUbicacionesRepository repo = null;
        private SSITSolicitudesUbicacionesPuertasRepository repoPuertas = null;
        private SSITSolicitudesUbicacionesPropiedadHorizontalRepository repoPH = null;
        private SSITSolicitudesUbicacionesMixturasRepository repoMixturas = null;
        private SSITSolicitudesUbicacionesDistritosRepository repoDistritos = null;

        private SSITSolicitudesRepository repoSSIT = null;
        private UbicacionesRepository repoUbic = null;
        private EncomiendaRepository repoEnc = null;

        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
        IMapper mapperModel;

        public SSITSolicitudesUbicacionesBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SSIT_Solicitudes_Ubicaciones, SSITSolicitudesUbicacionesDTO>()
                    .ForMember(dest => dest.IdSolicitudUbicacion, source => source.MapFrom(p => p.id_solicitudubicacion))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdUbicacion, source => source.MapFrom(p => p.id_ubicacion))
                    .ForMember(dest => dest.IdSubtipoUbicacion, source => source.MapFrom(p => p.id_subtipoubicacion))
                    .ForMember(dest => dest.LocalSubtipoUbicacion, source => source.MapFrom(p => p.local_subtipoubicacion))
                    .ForMember(dest => dest.DeptoLocalUbicacion, source => source.MapFrom(p => p.deptoLocal_ubicacion))
                    .ForMember(dest => dest.IdZonaPlaneamiento, source => source.MapFrom(p => p.id_zonaplaneamiento))
                    .ForMember(dest => dest.UbicacionesDTO, source => source.MapFrom(p => p.Ubicaciones))
                    .ForMember(dest => dest.SubTipoUbicacionesDTO, source => source.MapFrom(p => p.SubTiposDeUbicacion))
                    .ForMember(dest => dest.ZonasPlaneamientoDTO, source => source.MapFrom(p => p.Zonas_Planeamiento))
                    .ForMember(dest => dest.SSITSolicitudesUbicacionesPuertasDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Ubicaciones_Puertas))
                    .ForMember(dest => dest.SSITSolicitudesUbicacionesPropiedadHorizontalDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal))
                    .ForMember(dest => dest.SSITSolicitudesUbicacionesMixturasDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Ubicaciones_Mixturas))
                    .ForMember(dest => dest.SSITSolicitudesUbicacionesDistritosDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Ubicaciones_Distritos));

                cfg.CreateMap<SSITSolicitudesUbicacionesDTO, SSIT_Solicitudes_Ubicaciones>()
                    .ForMember(dest => dest.id_solicitudubicacion, source => source.MapFrom(p => p.IdSolicitudUbicacion))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_ubicacion, source => source.MapFrom(p => p.IdUbicacion))
                    .ForMember(dest => dest.id_subtipoubicacion, source => source.MapFrom(p => p.IdSubtipoUbicacion))
                    .ForMember(dest => dest.local_subtipoubicacion, source => source.MapFrom(p => p.LocalSubtipoUbicacion))
                    .ForMember(dest => dest.deptoLocal_ubicacion, source => source.MapFrom(p => p.DeptoLocalUbicacion))
                    .ForMember(dest => dest.id_zonaplaneamiento, source => source.MapFrom(p => p.IdZonaPlaneamiento))
                    .ForMember(dest => dest.Ubicaciones, source => source.MapFrom(p => p.UbicacionesDTO))
                    .ForMember(dest => dest.SubTiposDeUbicacion, source => source.MapFrom(p => p.SubTipoUbicacionesDTO))
                    .ForMember(dest => dest.Zonas_Planeamiento, source => source.MapFrom(p => p.ZonasPlaneamientoDTO))
                    .ForMember(dest => dest.SSIT_Solicitudes_Ubicaciones_Puertas, source => source.MapFrom(p => p.SSITSolicitudesUbicacionesPuertasDTO))
                    .ForMember(dest => dest.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal, source => source.MapFrom(p => p.SSITSolicitudesUbicacionesPropiedadHorizontalDTO))
                    .ForMember(dest => dest.SSIT_Solicitudes_Ubicaciones_Mixturas, source => source.MapFrom(p => p.SSITSolicitudesUbicacionesMixturasDTO))
                    .ForMember(dest => dest.SSIT_Solicitudes_Ubicaciones_Distritos, source => source.MapFrom(p => p.SSITSolicitudesUbicacionesDistritosDTO));

                #region "SubTiposDeUbicacion"
                cfg.CreateMap<SubTiposDeUbicacion, SubTipoUbicacionesDTO>()
                    .ForMember(dest => dest.TiposDeUbicacionDTO, source => source.MapFrom(p => p.TiposDeUbicacion));

                cfg.CreateMap<SubTipoUbicacionesDTO, SubTiposDeUbicacion>()
                    .ForMember(dest => dest.TiposDeUbicacion, source => source.MapFrom(p => p.TiposDeUbicacionDTO));
                #endregion
                #region "Ubicaciones"
                cfg.CreateMap<Ubicaciones, UbicacionesDTO>()
                     .ForMember(dest => dest.IdUbicacion, source => source.MapFrom(p => p.id_ubicacion));

                cfg.CreateMap<UbicacionesDTO, Ubicaciones>()
                     .ForMember(dest => dest.id_ubicacion, source => source.MapFrom(p => p.IdUbicacion));
                #endregion
                #region "Zonas_Planeamiento"
                cfg.CreateMap<Zonas_Planeamiento, ZonasPlaneamientoDTO>()
                    .ForMember(dest => dest.IdZonaPlaneamiento, source => source.MapFrom(p => p.id_zonaplaneamiento));

                cfg.CreateMap<ZonasPlaneamientoDTO, Zonas_Planeamiento>()
                    .ForMember(dest => dest.id_zonaplaneamiento, source => source.MapFrom(p => p.IdZonaPlaneamiento));

                #endregion
                #region "TiposDeUbicacion"
                cfg.CreateMap<TiposDeUbicacion, TiposDeUbicacionDTO>()
                    .ForMember(dest => dest.IdTipoUbicacion, source => source.MapFrom(p => p.id_tipoubicacion))
                    .ForMember(dest => dest.DescripcionTipoUbicacion, source => source.MapFrom(p => p.descripcion_tipoubicacion));
                //.ForMember(dest => dest.SubTipoUbicacionesDTO, source => source.MapFrom(p => p.SubTiposDeUbicacion));

                cfg.CreateMap<TiposDeUbicacionDTO, TiposDeUbicacion>()
                    .ForMember(dest => dest.id_tipoubicacion, source => source.MapFrom(p => p.IdTipoUbicacion))
                    .ForMember(dest => dest.descripcion_tipoubicacion, source => source.MapFrom(p => p.DescripcionTipoUbicacion));
                //.ForMember(dest => dest.SubTiposDeUbicacion, source => source.MapFrom(p => p.SubTipoUbicacionesDTO));
                #endregion

                cfg.CreateMap<SSIT_Solicitudes_Ubicaciones_Puertas, SSITSolicitudesUbicacionesPuertasDTO>()
                  .ForMember(dest => dest.IdSolicitudPuerta, source => source.MapFrom(p => p.id_solicitudpuerta))
                  .ForMember(dest => dest.IdSolicitudUbicacion, source => source.MapFrom(p => p.id_solicitudubicacion))
                  .ForMember(dest => dest.CodigoCalle, source => source.MapFrom(p => p.codigo_calle))
                  .ForMember(dest => dest.NombreCalle, source => source.MapFrom(p => p.nombre_calle));

                cfg.CreateMap<SSITSolicitudesUbicacionesPuertasDTO, SSIT_Solicitudes_Ubicaciones_Puertas>()
                 .ForMember(dest => dest.id_solicitudpuerta, source => source.MapFrom(p => p.IdSolicitudPuerta))
                 .ForMember(dest => dest.id_solicitudubicacion, source => source.MapFrom(p => p.IdSolicitudUbicacion))
                 .ForMember(dest => dest.codigo_calle, source => source.MapFrom(p => p.CodigoCalle))
                 .ForMember(dest => dest.nombre_calle, source => source.MapFrom(p => p.NombreCalle));

                cfg.CreateMap<SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal, SSITSolicitudesUbicacionesPropiedadHorizontalDTO>()
                   .ForMember(dest => dest.IdSolicitudPropiedadHorizontal, source => source.MapFrom(p => p.id_solicitudprophorizontal))
                   .ForMember(dest => dest.IdSolicitudUbicacion, source => source.MapFrom(p => p.id_solicitudubicacion))
                   .ForMember(dest => dest.IdPropiedadHorizontal, source => source.MapFrom(p => p.id_propiedadhorizontal))
                   .ForMember(dest => dest.UbicacionesPropiedadhorizontalDTO, source => source.MapFrom(p => p.Ubicaciones_PropiedadHorizontal));

                cfg.CreateMap<SSITSolicitudesUbicacionesPropiedadHorizontalDTO, SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal>()
                   .ForMember(dest => dest.id_solicitudprophorizontal, source => source.MapFrom(p => p.IdSolicitudPropiedadHorizontal))
                   .ForMember(dest => dest.id_solicitudubicacion, source => source.MapFrom(p => p.IdSolicitudUbicacion))
                   .ForMember(dest => dest.id_propiedadhorizontal, source => source.MapFrom(p => p.IdPropiedadHorizontal))
                   .ForMember(dest => dest.Ubicaciones_PropiedadHorizontal, source => source.MapFrom(p => p.UbicacionesPropiedadhorizontalDTO));

                cfg.CreateMap<Ubicaciones_PropiedadHorizontal, UbicacionesPropiedadhorizontalDTO>()
                   .ForMember(dest => dest.IdPropiedadHorizontal, source => source.MapFrom(p => p.id_propiedadhorizontal))
                   .ForMember(dest => dest.IdUbicacion, source => source.MapFrom(p => p.id_ubicacion));

                cfg.CreateMap<UbicacionesPropiedadhorizontalDTO, Ubicaciones_PropiedadHorizontal>()
                    .ForMember(dest => dest.id_propiedadhorizontal, source => source.MapFrom(p => p.IdPropiedadHorizontal))
                    .ForMember(dest => dest.id_ubicacion, source => source.MapFrom(p => p.IdUbicacion));

                #region mixturas
                cfg.CreateMap<SSIT_Solicitudes_Ubicaciones_Mixturas, SSITSolicitudesUbicacionesMixturasDTO>();

                cfg.CreateMap<SSITSolicitudesUbicacionesMixturasDTO, SSIT_Solicitudes_Ubicaciones_Mixturas>();
                #endregion

                #region distritos
                cfg.CreateMap<SSIT_Solicitudes_Ubicaciones_Distritos, SSITSolicitudesUbicacionesDistritoDTO>();

                cfg.CreateMap<SSITSolicitudesUbicacionesDistritoDTO, SSIT_Solicitudes_Ubicaciones_Distritos>();
                #endregion
            });


            var configUbicacion = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SSITSolicitudesUbicacionModelDTO, SsitSolicitudesUbicacionesModel>().ReverseMap();
            });

            mapperBase = config.CreateMapper();

            mapperModel = configUbicacion.CreateMapper();
        }

        public string GetMixDistritoZonaySubZonaBySolicitudUbicacion(int idSolicitudUbicacion)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesUbicacionesRepository(this.uowF.GetUnitOfWork());
            return repo.GetMixDistritoZonaySubZonaBySolicitudUbicacion(idSolicitudUbicacion);
        }
        public string GetMixDistritoZonaySubZonaBySolicitud(int IdSolicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesUbicacionesRepository(this.uowF.GetUnitOfWork());
            return repo.GetMixDistritoZonaySubZonaBySolicitud(IdSolicitud);
        }
        public string GetZonasPlaneamiento(int IdSolicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesUbicacionesRepository(this.uowF.GetUnitOfWork());
            var lstZonas = repo.Where(x => x.id_solicitud == IdSolicitud).Select(s => s.Zonas_Planeamiento.CodZonaPla).ToList();

            return string.Join(" / ", lstZonas.ToArray());
        }

        public string GetZonificacion(int IdSolicitud)
        {
            string result = "";

            uowF = new TransactionScopeUnitOfWorkFactory();
            var uof = this.uowF.GetUnitOfWork();
            repo = new SSITSolicitudesUbicacionesRepository(uof);
            var repoParam = new ParametrosRepository(uof);

            var NroSolicitudReferencia = repoParam.GetParametroNum("NroSolicitudReferencia");

            if (IdSolicitud > NroSolicitudReferencia.Value)
            {
                result = GetMixDistritoZonaySubZonaBySolicitud(IdSolicitud);
            }
            else
            {
                result = GetZonasPlaneamiento(IdSolicitud);
            }

            return result;
        }
        public IEnumerable<SSITSolicitudesUbicacionesDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesUbicacionesRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Ubicaciones>, IEnumerable<SSITSolicitudesUbicacionesDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool esUbicacionEspecialConObjetoTerritorial(int? idSubtipoUbicacion)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesUbicacionesRepository(this.uowF.GetUnitOfWork());
            return repo.esUbicacionEspecialConObjetoTerritorial(idSubtipoUbicacion);
        }

        public SSITSolicitudesUbicacionesDTO Single(int IdSSITSolicitudesUbicacion)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesUbicacionesRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdSSITSolicitudesUbicacion);
                var entityDto = mapperBase.Map<SSIT_Solicitudes_Ubicaciones, SSITSolicitudesUbicacionesDTO>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSSITSolicitudes"></param>
        /// <returns></returns>	
        public IEnumerable<SSITSolicitudesUbicacionesDTO> GetByFKIdSolicitud(int IdSolicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesUbicacionesRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdSolicitud(IdSolicitud);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Ubicaciones>, IEnumerable<SSITSolicitudesUbicacionesDTO>>(elements);
            return elementsDto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdUbicacion"></param>
        /// <returns></returns>	
        public IEnumerable<SSITSolicitudesUbicacionesDTO> GetByFKIdUbicacion(int IdUbicacion)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesUbicacionesRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdUbicacion(IdUbicacion);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Ubicaciones>, IEnumerable<SSITSolicitudesUbicacionesDTO>>(elements);
            return elementsDto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSubtipoUbicacion"></param>
        /// <returns></returns>	
        public IEnumerable<SSITSolicitudesUbicacionesDTO> GetByFKIdSubtipoUbicacion(int IdSubtipoUbicacion)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesUbicacionesRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdSubtipoUbicacion(IdSubtipoUbicacion);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Ubicaciones>, IEnumerable<SSITSolicitudesUbicacionesDTO>>(elements);
            return elementsDto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdZonaPlaneamiento"></param>
        /// <returns></returns>	
        public IEnumerable<SSITSolicitudesUbicacionesDTO> GetByFKIdZonaPlaneamiento(int IdZonaPlaneamiento)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesUbicacionesRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdZonaPlaneamiento(IdZonaPlaneamiento);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Ubicaciones>, IEnumerable<SSITSolicitudesUbicacionesDTO>>(elements);
            return elementsDto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enc"></param>
        public void copiarUbicacion(int id_encomienda, IUnitOfWork unitOfWork, Guid userid)
        {
            try
            {
                repo = new SSITSolicitudesUbicacionesRepository(unitOfWork);
                repoEnc = new EncomiendaRepository(unitOfWork);
                repoPuertas = new SSITSolicitudesUbicacionesPuertasRepository(unitOfWork);
                var encomiendaEntity = repoEnc.Single(id_encomienda);

                var listEncUbi = encomiendaEntity.Encomienda_Ubicaciones;
                var listSolUbi = encomiendaEntity.Encomienda_SSIT_Solicitudes.Select(x => x.SSIT_Solicitudes.SSIT_Solicitudes_Ubicaciones).FirstOrDefault();

                foreach (var encUbi in listEncUbi)
                {
                    var solUbi = listSolUbi.Where(x => x.id_ubicacion == encUbi.id_ubicacion
                                                    && x.id_zonaplaneamiento == encUbi.id_zonaplaneamiento).FirstOrDefault();

                    //Si es null hay que agregarla sino hay q actualizarla
                    if (solUbi == null)
                    {
                        SSITSolicitudesUbicacionesDTO ssitSolUbicDTO = new SSITSolicitudesUbicacionesDTO();
                        ssitSolUbicDTO.DeptoLocalUbicacion = encUbi.deptoLocal_encomiendaubicacion;
                        ssitSolUbicDTO.IdSolicitud = encomiendaEntity.Encomienda_SSIT_Solicitudes.Select(x => x.id_solicitud).FirstOrDefault();
                        ssitSolUbicDTO.IdSubtipoUbicacion = encUbi.id_subtipoubicacion;
                        ssitSolUbicDTO.IdUbicacion = encUbi.id_ubicacion;
                        ssitSolUbicDTO.IdZonaPlaneamiento = encUbi.id_zonaplaneamiento;
                        ssitSolUbicDTO.LocalSubtipoUbicacion = encUbi.local_subtipoubicacion;
                        var lhor = encUbi.Encomienda_Ubicaciones_PropiedadHorizontal;
                        ssitSolUbicDTO.PropiedadesHorizontales = new List<UbicacionesPropiedadhorizontalDTO>();
                        foreach (var hor in lhor)
                        {
                            UbicacionesPropiedadhorizontalDTO h = new UbicacionesPropiedadhorizontalDTO();
                            h.IdPropiedadHorizontal = hor.id_propiedadhorizontal.Value;
                            ssitSolUbicDTO.PropiedadesHorizontales.Add(h);
                        }
                        var lpuer = encUbi.Encomienda_Ubicaciones_Puertas;
                        ssitSolUbicDTO.Puertas = new List<UbicacionesPuertasDTO>();
                        foreach (var puer in lpuer)
                        {
                            UbicacionesPuertasDTO p = new UbicacionesPuertasDTO();
                            p.CodigoCalle = puer.codigo_calle;
                            p.NroPuertaUbic = puer.NroPuerta;
                            ssitSolUbicDTO.Puertas.Add(p);
                        }
                        ssitSolUbicDTO.CreateDate = DateTime.Now;
                        ssitSolUbicDTO.CreateUser = userid;

                        var elements = mapperBase.Map<SSITSolicitudesUbicacionesDTO, SSIT_Solicitudes_Ubicaciones>(ssitSolUbicDTO);
                        repo.Insert(elements);
                    }
                    else
                    {
                        repoPH = new SSITSolicitudesUbicacionesPropiedadHorizontalRepository(unitOfWork);
                        var listEncHor = encUbi.Encomienda_Ubicaciones_PropiedadHorizontal;
                        var listSolHor = solUbi.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal;
                        foreach (var encHor in listEncHor)
                        {
                            var solHor = listSolHor.Where(x => x.id_propiedadhorizontal == encHor.id_propiedadhorizontal).FirstOrDefault();
                            if (solHor == null)
                            {
                                SSITSolicitudesUbicacionesPropiedadHorizontalDTO hor = new SSITSolicitudesUbicacionesPropiedadHorizontalDTO();
                                hor.IdPropiedadHorizontal = encHor.id_propiedadhorizontal;
                                hor.IdSolicitudUbicacion = solUbi.id_solicitudubicacion;

                                var elements = mapperBase.Map<SSITSolicitudesUbicacionesPropiedadHorizontalDTO, SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal>(hor);
                                repoPH.Insert(elements);
                            }
                        }
                        //Eliminos las que no existen
                        foreach (var solHor in listSolHor)
                        {
                            var encHor = listEncHor.Where(x => x.id_propiedadhorizontal == solHor.id_propiedadhorizontal).FirstOrDefault();
                            if (encHor == null)
                                repoPH.Delete(solHor);
                        }

                        var listEncPuer = encUbi.Encomienda_Ubicaciones_Puertas;
                        var listSolPuer = solUbi.SSIT_Solicitudes_Ubicaciones_Puertas;
                        foreach (var encPuer in listEncPuer)
                        {
                            var solPuer = listSolPuer.Where(x => x.codigo_calle == encPuer.codigo_calle &&
                                                                x.nombre_calle == encPuer.nombre_calle).FirstOrDefault();
                            if (solPuer == null)
                            {
                                SSITSolicitudesUbicacionesPuertasDTO puer = new SSITSolicitudesUbicacionesPuertasDTO();
                                puer.CodigoCalle = encPuer.codigo_calle;
                                puer.IdSolicitudUbicacion = solUbi.id_solicitudubicacion;
                                puer.NombreCalle = encPuer.nombre_calle;
                                puer.NroPuerta = encPuer.NroPuerta;

                                var elements = mapperBase.Map<SSITSolicitudesUbicacionesPuertasDTO, SSIT_Solicitudes_Ubicaciones_Puertas>(puer);
                                repoPuertas.Insert(elements);
                            }
                            else
                            {
                                solPuer.codigo_calle = encPuer.codigo_calle;
                                solPuer.nombre_calle = encPuer.nombre_calle;
                                solPuer.NroPuerta = encPuer.NroPuerta;

                                repoPuertas.Update(solPuer);
                            }
                        }
                        //Eliminos las que no existen
                        foreach (var solPuer in listSolPuer)
                        {
                            var encPuer = listEncPuer.Where(x => x.codigo_calle == solPuer.codigo_calle &&
                                                                x.nombre_calle == solPuer.nombre_calle).FirstOrDefault();
                            if (encPuer == null)
                            {
                                repoPuertas.Delete(solPuer);
                            }
                        }

                        solUbi.deptoLocal_ubicacion = encUbi.deptoLocal_encomiendaubicacion;
                        solUbi.id_subtipoubicacion = encUbi.id_subtipoubicacion;
                        solUbi.id_zonaplaneamiento = encUbi.id_zonaplaneamiento;
                        solUbi.local_subtipoubicacion = encUbi.local_subtipoubicacion;
                        repo.Update(solUbi);
                    }
                }
                //Eliminos las que no existen
                foreach (var solUbi in listSolUbi)
                {
                    var encUbi = listEncUbi.Where(x => x.id_ubicacion == solUbi.id_ubicacion
                                                    && x.id_zonaplaneamiento == solUbi.id_zonaplaneamiento).FirstOrDefault();
                    if (encUbi == null)
                    {
                        repo.Delete(solUbi);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool esUbicacionEspecialConObjetoTerritorialByIdUbicacion(int idUbicacion)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesUbicacionesRepository(this.uowF.GetUnitOfWork());
            return repo.esUbicacionEspecialConObjetoTerritorialByIdUbicacion(idUbicacion);
        }
        #region Métodos de actualizacion e insert

        public bool Insert(SSITSolicitudesUbicacionesDTO objectDto, bool validarEstado)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesUbicacionesRepository(unitOfWork);
                    repoSSIT = new SSITSolicitudesRepository(unitOfWork);
                    repoUbic = new UbicacionesRepository(unitOfWork);
                    var SSITSolicitudesEntity = repoSSIT.Single(objectDto.IdSolicitud.Value);

                    List<int> id_estados = SSITSolicitudesEntity.Encomienda_SSIT_Solicitudes.Select(x => x.Encomienda).Select(s => s.id_estado).ToList();
                    if (validarEstado)
                    {
                        foreach (int id in id_estados)
                        {
                            if (id != (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo && id != (int)Constantes.Encomienda_Estados.Anulada)
                            {
                                solicitudConAnexoEnEstadoNoPermitido = string.Format("Existe un anexo técnico asociado a la solicitud en un estado que no permite modificar las ubicaciones de la misma.");
                                throw new Exception(solicitudConAnexoEnEstadoNoPermitido);
                            }
                        }
                    }
                    Ubicaciones ubicacionEntity = repoUbic.Single(objectDto.IdUbicacion.Value);

                    if (ubicacionEntity.Ubicaciones_Distritos.Count == 0 &&
                        ubicacionEntity.Ubicaciones_ZonasMixtura.Count == 0)
                        throw new Exception(Errors.UBICACION_SIN_ZONIFICACION);

                    ICollection<Ubicaciones_Inhibiciones> inhibidasEntity = ubicacionEntity.Ubicaciones_Inhibiciones;

                    if (inhibidasEntity.Any(p => !p.fecha_vencimiento.HasValue || (DateTime.Now >= p.fecha_inhibicion && DateTime.Now >= p.fecha_vencimiento.Value)))
                        throw new Exception(Errors.UBICACION_INHIBIDA);

                    var ssitUbicEntity = SSITSolicitudesEntity.SSIT_Solicitudes_Ubicaciones.Where(x => x.id_ubicacion == objectDto.IdUbicacion.Value);

                    if (ssitUbicEntity.Count() > 0)
                        throw new Exception(Errors.UBICACION_IGUAL);

                    //if (SSITSolicitudesEntity.SSIT_Solicitudes_Ubicaciones.Count() > 1)
                    //{
                    //    bool mismaManzParc = SSITSolicitudesEntity.SSIT_Solicitudes_Ubicaciones.Where(x =>
                    //                                                                                    x.Ubicaciones.Seccion == ubicacionEntity.Seccion
                    //                                                                                    && x.Ubicaciones.Manzana == ubicacionEntity.Manzana).Count() > 0;

                    //    if (!mismaManzParc)
                    //        throw new Exception(Errors.UBICACION_IGUAL);
                    //}

                    var elementDto = mapperBase.Map<SSITSolicitudesUbicacionesDTO, SSIT_Solicitudes_Ubicaciones>(objectDto);
                    elementDto.id_zonaplaneamiento = ubicacionEntity.id_zonaplaneamiento;
                    repo.Insert(elementDto);

                    repoPuertas = new SSITSolicitudesUbicacionesPuertasRepository(unitOfWork);
                    CallesBL callesBL = new CallesBL();
                    foreach (UbicacionesPuertasDTO puerta in objectDto.Puertas)
                    {
                        SSITSolicitudesUbicacionesPuertasDTO puertaDTO = new SSITSolicitudesUbicacionesPuertasDTO();

                        puertaDTO.CodigoCalle = puerta.CodigoCalle;
                        puertaDTO.IdSolicitudUbicacion = elementDto.id_solicitudubicacion;
                        puertaDTO.NombreCalle = callesBL.GetNombre(puerta.CodigoCalle, puerta.NroPuertaUbic);
                        puertaDTO.NroPuerta = puerta.NroPuertaUbic;
                        var element = mapperBase.Map<SSITSolicitudesUbicacionesPuertasDTO, SSIT_Solicitudes_Ubicaciones_Puertas>(puertaDTO);

                        repoPuertas.Insert(element);
                    }
                    repoPH = new SSITSolicitudesUbicacionesPropiedadHorizontalRepository(unitOfWork);
                    foreach (UbicacionesPropiedadhorizontalDTO ph in objectDto.PropiedadesHorizontales)
                    {
                        SSITSolicitudesUbicacionesPropiedadHorizontalDTO phDTO = new SSITSolicitudesUbicacionesPropiedadHorizontalDTO();

                        phDTO.IdSolicitudUbicacion = elementDto.id_solicitudubicacion;
                        phDTO.IdPropiedadHorizontal = ph.IdPropiedadHorizontal;

                        var element = mapperBase.Map<SSITSolicitudesUbicacionesPropiedadHorizontalDTO, SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal>(phDTO);

                        repoPH.Insert(element);
                    }

                    unitOfWork.Commit();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Inserta la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public bool Insert(SSITSolicitudesUbicacionesDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesUbicacionesRepository(unitOfWork);
                    repoSSIT = new SSITSolicitudesRepository(unitOfWork);
                    repoUbic = new UbicacionesRepository(unitOfWork);
                    var SSITSolicitudesEntity = repoSSIT.Single(objectDto.IdSolicitud.Value);

                    List<int> id_estados = SSITSolicitudesEntity.Encomienda_SSIT_Solicitudes.Select(x => x.Encomienda).Select(s => s.id_estado).ToList();

                    foreach (int id in id_estados)
                    {
                        if (id != (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo && id != (int)Constantes.Encomienda_Estados.Anulada)
                        {
                            solicitudConAnexoEnEstadoNoPermitido = string.Format("Existe un anexo técnico asociado a la solicitud en un estado que no permite modificar las ubicaciones de la misma.");
                            throw new Exception(solicitudConAnexoEnEstadoNoPermitido);
                        }
                    }

                    Ubicaciones ubicacionEntity = repoUbic.Single(objectDto.IdUbicacion.Value);

                    //if (ubicacionEntity.id_zonaplaneamiento == 0) 
                    if (ubicacionEntity.Ubicaciones_Distritos.Count == 0 && ubicacionEntity.Ubicaciones_ZonasMixtura.Count == 0)
                        throw new Exception(Errors.UBICACION_SIN_ZONIFICACION);

                    ICollection<Ubicaciones_Inhibiciones> inhibidasEntity = ubicacionEntity.Ubicaciones_Inhibiciones;

                    List<int> lstPHSeleccionadas = objectDto.PropiedadesHorizontales.Select(s => s.IdPropiedadHorizontal).ToList();

                    var PHInhibidas = ubicacionEntity.Ubicaciones_PropiedadHorizontal
                                    .Any(s => s.Ubicaciones_PropiedadHorizontal_Inhibiciones
                                        .Any(hor => hor.fecha_inhibicion < DateTime.Now
                                            && (hor.fecha_vencimiento > DateTime.Now || hor.fecha_vencimiento == null) && lstPHSeleccionadas.Contains(hor.id_propiedadhorizontal)));

                    var PHClausura = ubicacionEntity.Ubicaciones_PropiedadHorizontal
                                    .Any(s => s.Ubicaciones_PropiedadHorizontal_Clausuras
                                        .Any(hor => hor.fecha_alta_clausura < DateTime.Now
                                            && (hor.fecha_baja_clausura > DateTime.Now || hor.fecha_baja_clausura == null) && lstPHSeleccionadas.Contains(hor.id_propiedadhorizontal)));

                    if (inhibidasEntity.Any(p => !p.fecha_vencimiento.HasValue
                                            || (DateTime.Now >= p.fecha_inhibicion && DateTime.Now <= p.fecha_vencimiento.Value))
                                            || PHInhibidas
                                            || PHClausura)
                        throw new Exception(Errors.UBICACION_INHIBIDA);


                    var ssitUbicEntity = SSITSolicitudesEntity.SSIT_Solicitudes_Ubicaciones.Where(x => x.id_ubicacion == objectDto.IdUbicacion.Value);

                    if (ssitUbicEntity.Count() > 0)
                        throw new Exception(Errors.UBICACION_IGUAL);

                    //if (SSITSolicitudesEntity.SSIT_Solicitudes_Ubicaciones.Count() > 0)
                    //{
                    //    bool mismaSeccionManz = SSITSolicitudesEntity.SSIT_Solicitudes_Ubicaciones.Where(x =>
                    //                                                                                    x.Ubicaciones.Seccion == ubicacionEntity.Seccion
                    //                                                                                    && x.Ubicaciones.Manzana == ubicacionEntity.Manzana).Count() > 0;

                    //    if (!mismaSeccionManz)
                    //        throw new Exception("La ubicación que esta intentando agregar debe pertenecer a la misma manzana de las anteriores cargadas.");
                    //}

                    var elementDto = mapperBase.Map<SSITSolicitudesUbicacionesDTO, SSIT_Solicitudes_Ubicaciones>(objectDto);
                    elementDto.id_zonaplaneamiento = ubicacionEntity.id_zonaplaneamiento;
                    repo.Insert(elementDto);

                    repoPuertas = new SSITSolicitudesUbicacionesPuertasRepository(unitOfWork);
                    CallesBL callesBL = new CallesBL();
                    foreach (UbicacionesPuertasDTO puerta in objectDto.Puertas)
                    {
                        SSITSolicitudesUbicacionesPuertasDTO puertaDTO = new SSITSolicitudesUbicacionesPuertasDTO();

                        puertaDTO.CodigoCalle = puerta.CodigoCalle;
                        puertaDTO.IdSolicitudUbicacion = elementDto.id_solicitudubicacion;
                        puertaDTO.NombreCalle = callesBL.GetNombre(puerta.CodigoCalle, puerta.NroPuertaUbic);
                        puertaDTO.NroPuerta = puerta.NroPuertaUbic;
                        var element = mapperBase.Map<SSITSolicitudesUbicacionesPuertasDTO, SSIT_Solicitudes_Ubicaciones_Puertas>(puertaDTO);

                        repoPuertas.Insert(element);
                    }
                    repoPH = new SSITSolicitudesUbicacionesPropiedadHorizontalRepository(unitOfWork);
                    foreach (UbicacionesPropiedadhorizontalDTO ph in objectDto.PropiedadesHorizontales)
                    {
                        SSITSolicitudesUbicacionesPropiedadHorizontalDTO phDTO = new SSITSolicitudesUbicacionesPropiedadHorizontalDTO();

                        phDTO.IdSolicitudUbicacion = elementDto.id_solicitudubicacion;
                        phDTO.IdPropiedadHorizontal = ph.IdPropiedadHorizontal;

                        var element = mapperBase.Map<SSITSolicitudesUbicacionesPropiedadHorizontalDTO, SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal>(phDTO);

                        repoPH.Insert(element);
                    }

                    unitOfWork.Commit();

                    objectDto.IdSolicitudUbicacion = elementDto.id_solicitudubicacion;
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
        #region Métodos de actualizacion e insert
        /// <summary>
        /// Modifica la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public void Update(SSITSolicitudesUbicacionesDTO objectDTO)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repoSSIT = new SSITSolicitudesRepository(unitOfWork);
                    var SSITSolicitudesEntity = repoSSIT.Single(objectDTO.IdSolicitud);
                    List<int> id_estados = SSITSolicitudesEntity.Encomienda_SSIT_Solicitudes.Select(x => x.Encomienda).Select(s => s.id_estado).ToList();

                    foreach (int id in id_estados)
                    {
                        if (id != (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo && id != (int)Constantes.Encomienda_Estados.Anulada)
                        { 
                            solicitudConAnexoEnEstadoNoPermitido = string.Format("Existe un anexo técnico asociado a la solicitud en un estado que no permite modificar las ubicaciones de la misma.");
                        throw new Exception(solicitudConAnexoEnEstadoNoPermitido);
                        }
                    }


                    repo = new SSITSolicitudesUbicacionesRepository(unitOfWork);
                    var elementDTO = mapperBase.Map<SSITSolicitudesUbicacionesDTO, SSIT_Solicitudes_Ubicaciones>(objectDTO);
                    repo.Update(elementDTO);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectDTO"></param>
        /// <param name="validar"></param>
        public void Update(SSITSolicitudesUbicacionesDTO objectDTO, bool validar)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repoSSIT = new SSITSolicitudesRepository(unitOfWork);
                    var SSITSolicitudesEntity = repoSSIT.Single(objectDTO.IdSolicitud);
                    List<int> id_estados = SSITSolicitudesEntity.Encomienda_SSIT_Solicitudes.Select(x => x.Encomienda).Select(s => s.id_estado).ToList();
                    if (validar)
                    {
                        foreach (int id in id_estados)
                        {
                            if (id != (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo && id != (int)Constantes.Encomienda_Estados.Anulada)
                            {
                                solicitudConAnexoEnEstadoNoPermitido = string.Format("Existe un anexo técnico asociado a la solicitud en un estado que no permite modificar las ubicaciones de la misma.");
                                throw new Exception(solicitudConAnexoEnEstadoNoPermitido);
                            }
                        }
                    }

                    repo = new SSITSolicitudesUbicacionesRepository(unitOfWork);
                    var elementDTO = mapperBase.Map<SSITSolicitudesUbicacionesDTO, SSIT_Solicitudes_Ubicaciones>(objectDTO);
                    repo.Update(elementDTO);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        #region Métodos de actualizacion e insert
        /// <summary>
        /// elimina la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>      
        public void Delete(SSITSolicitudesUbicacionesDTO objectDto)
        {
            try
            {

                uowF = new TransactionScopeUnitOfWorkFactory();
                IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork();

                repoSSIT = new SSITSolicitudesRepository(unitOfWork);
                var SSITSolicitudesEntity = repoSSIT.Single(objectDto.IdSolicitud.Value);
                if (SSITSolicitudesEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.COMP 
                    && SSITSolicitudesEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM
                    && SSITSolicitudesEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF
                    && SSITSolicitudesEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO
                    && SSITSolicitudesEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.SUSPEN)
                    throw new Exception(Errors.SSIT_SOLICITUD_NO_CAMBIOS);

                List<int> id_estados = SSITSolicitudesEntity.Encomienda_SSIT_Solicitudes.Select(x => x.Encomienda).Select(s => s.id_estado).ToList();

                foreach (int id in id_estados)
                {
                    if (id != (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo && id != (int)Constantes.Encomienda_Estados.Anulada)
                    { 
                        solicitudConAnexoEnEstadoNoPermitido = string.Format("Existe un anexo técnico asociado a la solicitud en un estado que no permite modificar las ubicaciones de la misma.");
                        throw new Exception(solicitudConAnexoEnEstadoNoPermitido);
                    }
                }


                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWorkTran = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {

                    repo = new SSITSolicitudesUbicacionesRepository(unitOfWorkTran);
                    repoPH = new SSITSolicitudesUbicacionesPropiedadHorizontalRepository(unitOfWorkTran);
                    repoPuertas = new SSITSolicitudesUbicacionesPuertasRepository(unitOfWorkTran);
                    repoMixturas = new SSITSolicitudesUbicacionesMixturasRepository(unitOfWorkTran);
                    repoDistritos = new SSITSolicitudesUbicacionesDistritosRepository(unitOfWorkTran);

                    var elements = repo.Single(objectDto.IdSolicitudUbicacion);
                    repoPH.RemoveRange(elements.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal);

                    repoPuertas.RemoveRange(elements.SSIT_Solicitudes_Ubicaciones_Puertas);

                    repoMixturas.RemoveRange(elements.SSIT_Solicitudes_Ubicaciones_Mixturas);

                    repoDistritos.RemoveRange(elements.SSIT_Solicitudes_Ubicaciones_Distritos);

                    repo.Delete(elements);

                    unitOfWorkTran.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int? getIdSubTipoUbicacionByIdUbicacion(int id_ubicacion)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesUbicacionesRepository(this.uowF.GetUnitOfWork());
            return repo.getIdSubTipoUbicacionByIdUbicacion(id_ubicacion);
        }

        public void Delete(SSITSolicitudesUbicacionesDTO objectDto, bool validar)
        {
            try
            {

                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repoSSIT = new SSITSolicitudesRepository(unitOfWork);
                    var SSITSolicitudesEntity = repoSSIT.Single(objectDto.IdSolicitud.Value);
                    if (SSITSolicitudesEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && SSITSolicitudesEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM
                        && SSITSolicitudesEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF
                        && SSITSolicitudesEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO
                        && SSITSolicitudesEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.SUSPEN)
                        throw new Exception(Errors.SSIT_SOLICITUD_NO_CAMBIOS);

                    List<int> id_estados = SSITSolicitudesEntity.Encomienda_SSIT_Solicitudes.Select(x => x.Encomienda).Select(s => s.id_estado).ToList();
                    if (validar)
                    {
                        foreach (int id in id_estados)
                        {
                            if (id != (int)Constantes.Encomienda_Estados.Aprobada_por_el_consejo && id != (int)Constantes.Encomienda_Estados.Anulada)
                            {
                                solicitudConAnexoEnEstadoNoPermitido = string.Format("Existe un anexo técnico asociado a la solicitud en un estado que no permite modificar las ubicaciones de la misma.");
                            throw new Exception(solicitudConAnexoEnEstadoNoPermitido);
                            }
                        }
                    }
                    repo = new SSITSolicitudesUbicacionesRepository(unitOfWork);
                    repoPH = new SSITSolicitudesUbicacionesPropiedadHorizontalRepository(unitOfWork);
                    repoPuertas = new SSITSolicitudesUbicacionesPuertasRepository(unitOfWork);

                    var elementDto = mapperBase.Map<SSITSolicitudesUbicacionesDTO, SSIT_Solicitudes_Ubicaciones>(objectDto);

                    var elements = repoPH.GetByFKIdSolicitudUbicacion(objectDto.IdSolicitudUbicacion);
                    foreach (var element in elements)
                        repoPH.Delete(element);

                    var elementsPuertas = repoPuertas.GetByFKIdSolicitudUbicacion(objectDto.IdSolicitudUbicacion);
                    foreach (var element in elementsPuertas)
                        repoPuertas.Delete(element);

                    repo.Delete(elementDto);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        //public IEnumerable<SSITSolicitudesUbicacionModelDTO> GetSSITSolicitudesUbicacion(int IdSSITSolicitudes)
        //{
        //    try
        //    {
        //        uowF = new TransactionScopeUnitOfWorkFactory();
        //        repo = new SSITSolicitudesUbicacionesRepository(this.uowF.GetUnitOfWork());
        //        var ssitUbicacionesSolicitudes = repo.Get(IdSSITSolicitudes);
        //        var elementsDto = mapperModel.Map<IEnumerable<SsitSolicitudesUbicacionesModel>, IEnumerable<SSITSolicitudesUbicacionModelDTO>>(ssitUbicacionesSolicitudes);
        //        return elementsDto;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSSITSolicitudesUbicacion"></param>
        /// <returns></returns>
        public IEnumerable<SSITSolicitudesUbicacionModelDTO> GetUbicacion(int IdSSITSolicitudesUbicacion)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesUbicacionesRepository(this.uowF.GetUnitOfWork());
                //return repo.GetUbicacion(IdSSITSolicitudesUbicacion);

                var ssitUbicacionesSolicitudes = repo.GetUbicacion(IdSSITSolicitudesUbicacion);
                var elementsDto = mapperModel.Map<IEnumerable<SsitSolicitudesUbicacionesModel>, IEnumerable<SSITSolicitudesUbicacionModelDTO>>(ssitUbicacionesSolicitudes);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool validarUbicacionClausuras(int id_solicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesUbicacionesRepository(this.uowF.GetUnitOfWork());
            return repo.validarUbicacionClausuras(id_solicitud);
        }

        public bool validarUbicacionInhibiciones(int id_solicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesUbicacionesRepository(this.uowF.GetUnitOfWork());
            return repo.validarUbicacionInhibiciones(id_solicitud);
        }

        public void copiarUbicacionToSSIT(EncomiendaDTO encDTO, Guid userId)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesUbicacionesRepository(unitOfWork);
                    repoPuertas = new SSITSolicitudesUbicacionesPuertasRepository(unitOfWork);
                    repoPH = new SSITSolicitudesUbicacionesPropiedadHorizontalRepository(unitOfWork);
                    repoMixturas = new SSITSolicitudesUbicacionesMixturasRepository(unitOfWork);
                    repoDistritos = new SSITSolicitudesUbicacionesDistritosRepository(unitOfWork);

                    int id_solicitud = encDTO.IdSolicitud; //EncomiendaSSITSolicitudesDTO.Select(x => x.id_solicitud).FirstOrDefault()
                    var SSITUbiEntity = repo.GetByFKIdSolicitud(id_solicitud).ToList();
                    var lstEncUbicDTO = encDTO.EncomiendaUbicacionesDTO;

                    foreach (var ubic in SSITUbiEntity)
                    {
                        var lstPuertas = ubic.SSIT_Solicitudes_Ubicaciones_Puertas.ToList();
                        foreach (var puerta in lstPuertas)
                            repoPuertas.Delete(puerta);

                        var lstPH = ubic.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal.ToList();
                        foreach (var ph in lstPH)
                            repoPH.Delete(ph);

                        var lstMixturas = ubic.SSIT_Solicitudes_Ubicaciones_Mixturas.ToList();
                        foreach (var mixturas in lstMixturas)
                            repoMixturas.Delete(mixturas);

                        var lstDistritos = ubic.SSIT_Solicitudes_Ubicaciones_Distritos.ToList();
                        foreach (var distritos in lstDistritos)
                            repoDistritos.Delete(distritos);

                        repo.Delete(ubic);
                    }

                    SSITSolicitudesUbicacionesDTO SSITUbicDTO = null;
                    SSITSolicitudesUbicacionesPuertasDTO SSITUbicPuertaDTO = null;
                    SSITSolicitudesUbicacionesPropiedadHorizontalDTO SSITUbicPHDTO = null;
                    SSITSolicitudesUbicacionesDistritoDTO SSITUbicDistritoDTO = null;
                    SSITSolicitudesUbicacionesMixturasDTO SSITUbicMixturaDTO = null;

                    foreach (var encUbic in lstEncUbicDTO)
                    {
                        SSITUbicDTO = new SSITSolicitudesUbicacionesDTO();

                        SSITUbicDTO.IdSolicitud = id_solicitud;
                        SSITUbicDTO.IdUbicacion = encUbic.IdUbicacion;
                        SSITUbicDTO.IdSubtipoUbicacion = encUbic.IdSubtipoUbicacion;
                        SSITUbicDTO.LocalSubtipoUbicacion = encUbic.LocalSubtipoUbicacion;
                        SSITUbicDTO.DeptoLocalUbicacion = encUbic.DeptoLocalEncomiendaUbicacion;
                        SSITUbicDTO.CreateDate = DateTime.Now;
                        SSITUbicDTO.CreateUser = userId;
                        SSITUbicDTO.IdZonaPlaneamiento = encUbic.IdZonaPlaneamiento;
                        SSITUbicDTO.Depto = encUbic.Depto;
                        SSITUbicDTO.Local = encUbic.Local;
                        SSITUbicDTO.Torre = encUbic.Torre;

                        var ubicEntity = mapperBase.Map<SSITSolicitudesUbicacionesDTO, SSIT_Solicitudes_Ubicaciones>(SSITUbicDTO);
                        repo.Insert(ubicEntity);

                        foreach (var encPuerta in encUbic.EncomiendaUbicacionesPuertasDTO)
                        {
                            SSITUbicPuertaDTO = new SSITSolicitudesUbicacionesPuertasDTO();

                            SSITUbicPuertaDTO.IdSolicitudUbicacion = ubicEntity.id_solicitudubicacion;
                            SSITUbicPuertaDTO.CodigoCalle = encPuerta.CodigoCalle;
                            SSITUbicPuertaDTO.NombreCalle = encPuerta.NombreCalle;
                            SSITUbicPuertaDTO.NroPuerta = encPuerta.NroPuerta;

                            var puertaEntity = mapperBase.Map<SSITSolicitudesUbicacionesPuertasDTO, SSIT_Solicitudes_Ubicaciones_Puertas>(SSITUbicPuertaDTO);
                            repoPuertas.Insert(puertaEntity);
                        }

                        foreach (var encPH in encUbic.EncomiendaUbicacionesPropiedadHorizontalDTO)
                        {
                            SSITUbicPHDTO = new SSITSolicitudesUbicacionesPropiedadHorizontalDTO();

                            SSITUbicPHDTO.IdSolicitudUbicacion = ubicEntity.id_solicitudubicacion;
                            SSITUbicPHDTO.IdPropiedadHorizontal = encPH.IdPropiedadHorizontal;

                            var puertaEntity = mapperBase.Map<SSITSolicitudesUbicacionesPropiedadHorizontalDTO, SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal>(SSITUbicPHDTO);
                            repoPH.Insert(puertaEntity);
                        }

                        foreach (var encMixtura in encUbic.EncomiendaUbicacionesMixturasDTO)
                        {
                            SSITUbicMixturaDTO = new SSITSolicitudesUbicacionesMixturasDTO();

                            SSITUbicMixturaDTO.id_solicitudubicacion = ubicEntity.id_solicitudubicacion;
                            SSITUbicMixturaDTO.IdZonaMixtura = encMixtura.IdZonaMixtura;

                            var mixturaEntity = mapperBase.Map<SSITSolicitudesUbicacionesMixturasDTO, SSIT_Solicitudes_Ubicaciones_Mixturas>(SSITUbicMixturaDTO);
                            repoMixturas.Insert(mixturaEntity);
                        }

                        foreach (var encDistrito in encUbic.EncomiendaUbicacionesDistritosDTO)
                        {
                            SSITUbicDistritoDTO = new SSITSolicitudesUbicacionesDistritoDTO();

                            SSITUbicDistritoDTO.id_solicitudubicacion = ubicEntity.id_solicitudubicacion;
                            SSITUbicDistritoDTO.IdDistrito = encDistrito.IdDistrito;
                            SSITUbicDistritoDTO.IdZona = encDistrito.IdZona;
                            SSITUbicDistritoDTO.IdSubZona = encDistrito.IdSubZona;

                            var distritoEntity = mapperBase.Map<SSITSolicitudesUbicacionesDistritoDTO, SSIT_Solicitudes_Ubicaciones_Distritos>(SSITUbicDistritoDTO);
                            repoDistritos.Insert(distritoEntity);
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
    }
}


