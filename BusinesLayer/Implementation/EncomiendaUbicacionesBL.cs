using AutoMapper;
using BaseRepository;
using IBusinessLayer;
using Dal.UnitOfWork;
using DataAcess;
using DataTransferObject;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnitOfWork;
using StaticClass;
using DataAcess.EntityCustom;

namespace BusinesLayer.Implementation
{

    public class EncomiendaUbicacionesBL : IEncomiendaUbicacionesBL<EncomiendaUbicacionesDTO>
    {
        private EncomiendaRepository repoEnc = null;
        private UbicacionesRepository repoUbic = null;
        private EncomiendaUbicacionesRepository repo = null;
        private UbicacionesInhibicionesRepository repoUbicInhi = null;
        private ZonasPlaneamientoRepository repoZonaPlaneamiento = null;
        private EncomiendaUbicacionesPuertasRepository repoPuertas = null;
        private EncomiendaUbicacionesPropiedadHorizontalRepository repoPH = null;
        private EncomiendaUbicacionesMixturasRepository repoMixturas = null;
        private EncomiendaUbicacionesDistritosRepository repoDistritos = null;
        private IUnitOfWorkFactory uowF = null;

        IMapper mapperBase;
        IMapper mapperPuerta;
        IMapper mapperPH;

        public EncomiendaUbicacionesBL()
        {
            var config = new MapperConfiguration(cfg =>
            {

                #region "Encomienda_Ubicaciones"
                cfg.CreateMap<Encomienda_Ubicaciones, EncomiendaUbicacionesDTO>()
                    .ForMember(dest => dest.IdEncomiendaUbicacion, source => source.MapFrom(p => p.id_encomiendaubicacion))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.IdUbicacion, source => source.MapFrom(p => p.id_ubicacion))
                    .ForMember(dest => dest.IdSubtipoUbicacion, source => source.MapFrom(p => p.id_subtipoubicacion))
                    .ForMember(dest => dest.LocalSubtipoUbicacion, source => source.MapFrom(p => p.local_subtipoubicacion))
                    .ForMember(dest => dest.DeptoLocalEncomiendaUbicacion, source => source.MapFrom(p => p.deptoLocal_encomiendaubicacion))
                    .ForMember(dest => dest.IdZonaPlaneamiento, source => source.MapFrom(p => p.id_zonaplaneamiento))
                    .ForMember(dest => dest.EncomiendaUbicacionesPropiedadHorizontalDTO, source => source.MapFrom(p => p.Encomienda_Ubicaciones_PropiedadHorizontal))
                    .ForMember(dest => dest.EncomiendaUbicacionesPuertasDTO, source => source.MapFrom(p => p.Encomienda_Ubicaciones_Puertas))
                    .ForMember(dest => dest.SubTipoUbicacionesDTO, source => source.MapFrom(p => p.SubTiposDeUbicacion))
                    .ForMember(dest => dest.ZonasPlaneamientoDTO, source => source.MapFrom(p => p.Zonas_Planeamiento))
                    .ForMember(dest => dest.Ubicacion, source => source.MapFrom(p => p.Ubicaciones))
                    .ForMember(dest => dest.EncomiendaUbicacionesDistritosDTO, source => source.MapFrom(p => p.Encomienda_Ubicaciones_Distritos))
                    .ForMember(dest => dest.EncomiendaUbicacionesMixturasDTO, source => source.MapFrom(p => p.Encomienda_Ubicaciones_Mixturas));

                cfg.CreateMap<EncomiendaUbicacionesDTO, Encomienda_Ubicaciones>()
                    .ForMember(dest => dest.id_encomiendaubicacion, source => source.MapFrom(p => p.IdEncomiendaUbicacion))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.id_ubicacion, source => source.MapFrom(p => p.IdUbicacion))
                    .ForMember(dest => dest.id_subtipoubicacion, source => source.MapFrom(p => p.IdSubtipoUbicacion))
                    .ForMember(dest => dest.local_subtipoubicacion, source => source.MapFrom(p => p.LocalSubtipoUbicacion))
                    .ForMember(dest => dest.deptoLocal_encomiendaubicacion, source => source.MapFrom(p => p.DeptoLocalEncomiendaUbicacion))
                    .ForMember(dest => dest.id_zonaplaneamiento, source => source.MapFrom(p => p.IdZonaPlaneamiento))
                    .ForMember(dest => dest.Encomienda_Ubicaciones_PropiedadHorizontal, source => source.MapFrom(p => p.EncomiendaUbicacionesPropiedadHorizontalDTO))
                    .ForMember(dest => dest.Encomienda_Ubicaciones_Puertas, source => source.MapFrom(p => p.EncomiendaUbicacionesPuertasDTO))
                    .ForMember(dest => dest.SubTiposDeUbicacion, source => source.MapFrom(p => p.SubTipoUbicacionesDTO))
                    .ForMember(dest => dest.Zonas_Planeamiento, source => source.MapFrom(p => p.ZonasPlaneamientoDTO))
                    .ForMember(dest => dest.Ubicaciones, source => source.MapFrom(p => p.Ubicacion))
                    .ForMember(dest => dest.Encomienda_Ubicaciones_Mixturas, source => source.MapFrom(p => p.EncomiendaUbicacionesMixturasDTO))
                    .ForMember(dest => dest.Encomienda_Ubicaciones_Distritos, source => source.MapFrom(p => p.EncomiendaUbicacionesDistritosDTO));
                #endregion

                #region "Encomienda_Ubicaciones_PropiedadHorizontal"
                cfg.CreateMap<Encomienda_Ubicaciones_PropiedadHorizontal, EncomiendaUbicacionesPropiedadHorizontalDTO>()
                    .ForMember(dest => dest.IdEncomiendaPropiedadHorizontal, source => source.MapFrom(p => p.id_encomiendaprophorizontal))
                    .ForMember(dest => dest.IdEncomiendaUbicacion, source => source.MapFrom(p => p.id_encomiendaubicacion))
                    .ForMember(dest => dest.IdPropiedadHorizontal, source => source.MapFrom(p => p.id_propiedadhorizontal));

                cfg.CreateMap<EncomiendaUbicacionesPropiedadHorizontalDTO, Encomienda_Ubicaciones_PropiedadHorizontal>()
                  .ForMember(dest => dest.id_encomiendaprophorizontal, source => source.MapFrom(p => p.IdEncomiendaPropiedadHorizontal))
                  .ForMember(dest => dest.id_encomiendaubicacion, source => source.MapFrom(p => p.IdEncomiendaUbicacion))
                  .ForMember(dest => dest.id_propiedadhorizontal, source => source.MapFrom(p => p.IdPropiedadHorizontal));
                #endregion
                #region "Encomienda_Ubicaciones_Puertas"
                cfg.CreateMap<Encomienda_Ubicaciones_Puertas, EncomiendaUbicacionesPuertasDTO>()
                    .ForMember(dest => dest.IdEncomiendaPuerta, source => source.MapFrom(p => p.id_encomiendapuerta))
                    .ForMember(dest => dest.IdEncomiendaUbicacion, source => source.MapFrom(p => p.id_encomiendaubicacion))
                    .ForMember(dest => dest.CodigoCalle, source => source.MapFrom(p => p.codigo_calle))
                    .ForMember(dest => dest.NombreCalle, source => source.MapFrom(p => p.nombre_calle))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NroPuerta));

                cfg.CreateMap<EncomiendaUbicacionesPuertasDTO, Encomienda_Ubicaciones_Puertas>()
                    .ForMember(dest => dest.id_encomiendapuerta, source => source.MapFrom(p => p.IdEncomiendaPuerta))
                    .ForMember(dest => dest.id_encomiendaubicacion, source => source.MapFrom(p => p.IdEncomiendaUbicacion))
                    .ForMember(dest => dest.codigo_calle, source => source.MapFrom(p => p.CodigoCalle))
                    .ForMember(dest => dest.nombre_calle, source => source.MapFrom(p => p.NombreCalle))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NroPuerta));
                #endregion
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
                    .ForMember(dest => dest.DescripcionTipoUbicacion, source => source.MapFrom(p => p.descripcion_tipoubicacion))
                    .ForMember(dest => dest.SubTipoUbicacionesDTO, source => source.MapFrom(p => p.SubTiposDeUbicacion));

                cfg.CreateMap<TiposDeUbicacionDTO, TiposDeUbicacion>()
                    .ForMember(dest => dest.id_tipoubicacion, source => source.MapFrom(p => p.IdTipoUbicacion))
                    .ForMember(dest => dest.descripcion_tipoubicacion, source => source.MapFrom(p => p.DescripcionTipoUbicacion))
                    .ForMember(dest => dest.SubTiposDeUbicacion, source => source.MapFrom(p => p.SubTipoUbicacionesDTO));
                #endregion

                #region mixturas
                cfg.CreateMap<Encomienda_Ubicaciones_Mixturas, Encomienda_Ubicaciones_MixturasDTO>();
                //.ForMember(dest => dest.UbicacionesZonasMixturasDTO, source => source.MapFrom(p => p.Ubicaciones_ZonasMixtura));

                cfg.CreateMap<Encomienda_Ubicaciones_MixturasDTO, Encomienda_Ubicaciones_Mixturas>();
                //.ForMember(dest => dest.Ubicaciones_ZonasMixtura, source => source.MapFrom(p => p.UbicacionesZonasMixturasDTO));
                #endregion

                #region distritos
                cfg.CreateMap<Encomienda_Ubicaciones_Distritos, Encomienda_Ubicaciones_DistritosDTO>();

                cfg.CreateMap<Encomienda_Ubicaciones_DistritosDTO, Encomienda_Ubicaciones_Distritos>();
                #endregion
            });

            var configPuerta = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EncomiendaUbicacionesPuertasDTO, Encomienda_Ubicaciones_Puertas>().ReverseMap()
                    .ForMember(dest => dest.IdEncomiendaPuerta, source => source.MapFrom(p => p.id_encomiendapuerta))
                    .ForMember(dest => dest.IdEncomiendaUbicacion, source => source.MapFrom(p => p.id_encomiendaubicacion))
                    .ForMember(dest => dest.CodigoCalle, source => source.MapFrom(p => p.codigo_calle))
                    .ForMember(dest => dest.NombreCalle, source => source.MapFrom(p => p.nombre_calle))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NroPuerta));

                cfg.CreateMap<Encomienda_Ubicaciones_Puertas, EncomiendaUbicacionesPuertasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_encomiendapuerta, source => source.MapFrom(p => p.IdEncomiendaPuerta))
                    .ForMember(dest => dest.id_encomiendaubicacion, source => source.MapFrom(p => p.IdEncomiendaUbicacion))
                    .ForMember(dest => dest.codigo_calle, source => source.MapFrom(p => p.CodigoCalle))
                    .ForMember(dest => dest.nombre_calle, source => source.MapFrom(p => p.NombreCalle))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NroPuerta));

            });
            var configPH = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EncomiendaUbicacionesPropiedadHorizontalDTO, Encomienda_Ubicaciones_PropiedadHorizontal>().ReverseMap()
                    .ForMember(dest => dest.IdEncomiendaPropiedadHorizontal, source => source.MapFrom(p => p.id_encomiendaprophorizontal))
                    .ForMember(dest => dest.IdEncomiendaUbicacion, source => source.MapFrom(p => p.id_encomiendaubicacion))
                    .ForMember(dest => dest.IdPropiedadHorizontal, source => source.MapFrom(p => p.id_propiedadhorizontal));

                cfg.CreateMap<Encomienda_Ubicaciones_PropiedadHorizontal, EncomiendaUbicacionesPropiedadHorizontalDTO>().ReverseMap()
                  .ForMember(dest => dest.id_encomiendaprophorizontal, source => source.MapFrom(p => p.IdEncomiendaPropiedadHorizontal))
                  .ForMember(dest => dest.id_encomiendaubicacion, source => source.MapFrom(p => p.IdEncomiendaUbicacion))
                  .ForMember(dest => dest.id_propiedadhorizontal, source => source.MapFrom(p => p.IdPropiedadHorizontal));

            });

            mapperBase = config.CreateMapper();
            mapperPuerta = configPuerta.CreateMapper();
            mapperPH = configPH.CreateMapper();
        }

        public string GetMixDistritoZonaySubZonaByEncomiendaUbicacion(int idEncomiendaUbicacion)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaUbicacionesRepository(this.uowF.GetUnitOfWork());
                return repo.GetMixDistritoZonaySubZonaByEncomiendaUbicacion(idEncomiendaUbicacion);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetMixDistritoZonaySubZonaByEncomienda(int idEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaUbicacionesRepository(this.uowF.GetUnitOfWork());
                return repo.GetMixDistritoZonaySubZonaByEncomienda(idEncomienda);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetZonasPlaneamiento(int IdEncomienda)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaUbicacionesRepository(this.uowF.GetUnitOfWork());
            var lstZonas = repo.Where(x => x.id_encomienda == IdEncomienda).Select(s=> s.Zonas_Planeamiento.CodZonaPla).ToList();

            return string.Join(" / ", lstZonas.ToArray());
        }

        public string GetZonificacion(int IdEncomienda)
        {
            string result = "";

            uowF = new TransactionScopeUnitOfWorkFactory();
            var uof = this.uowF.GetUnitOfWork();
            repo = new EncomiendaUbicacionesRepository(uof);
            var repoEnc = new EncomiendaRepository(uof);
            var repoParam = new ParametrosRepository(uof);

            var encomiendaEntity = repoEnc.Single(IdEncomienda);
            var solicitud = encomiendaEntity.Encomienda_SSIT_Solicitudes.FirstOrDefault();
            var NroSolicitudReferencia = repoParam.GetParametroNum("NroSolicitudReferencia");

            if(solicitud == null || NroSolicitudReferencia.HasValue && solicitud.id_solicitud > NroSolicitudReferencia.Value  )
            {
                result = GetMixDistritoZonaySubZonaByEncomienda(IdEncomienda);
            }
            else
            {
                result = GetZonasPlaneamiento(IdEncomienda);
            }

            return result;
        }



        public EncomiendaUbicacionesDTO Single(int IdEncomiendaUbicacion)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaUbicacionesRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdEncomiendaUbicacion);
                var entityDto = mapperBase.Map<Encomienda_Ubicaciones, EncomiendaUbicacionesDTO>(entity);

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
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>	
        public IEnumerable<EncomiendaUbicacionesDTO> GetByFKIdEncomienda(int IdEncomienda)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaUbicacionesRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdEncomienda(IdEncomienda);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Ubicaciones>, IEnumerable<EncomiendaUbicacionesDTO>>(elements);
            return elementsDto;
        }

        public bool esZonaResidencial(List<int> lstZonaPlaneamiento)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaUbicacionesRepository(this.uowF.GetUnitOfWork());
            lstZonaPlaneamiento = lstZonaPlaneamiento.Distinct().ToList();
            var elements = repo.esZonaResidencial(lstZonaPlaneamiento);
            if (elements.Count() == lstZonaPlaneamiento.Count())
                return true;
            else
                return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdUbicacion"></param>
        /// <returns></returns>	
        public IEnumerable<EncomiendaUbicacionesDTO> GetByFKIdUbicacion(int IdUbicacion)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaUbicacionesRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdUbicacion(IdUbicacion);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Ubicaciones>, IEnumerable<EncomiendaUbicacionesDTO>>(elements);
            return elementsDto;
        }

        #region Métodos de actualizacion e insert
        /// <summary>
        /// Inserta la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public bool Insert(EncomiendaUbicacionesDTO objectDto)
        {
            uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
            using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
            {
                try
                {
                    repo = new EncomiendaUbicacionesRepository(unitOfWork);
                    repoEnc = new EncomiendaRepository(unitOfWork);
                    repoUbic = new UbicacionesRepository(unitOfWork);
                    repoUbicInhi = new UbicacionesInhibicionesRepository(unitOfWork);
                    var param = new ParametrosBL();
                    var encomiendaEntity = repoEnc.Single(objectDto.IdEncomienda.Value);
                    
                    int nroSolReferencia = encomiendaEntity.id_tipotramite == (int)Constantes.TipoDeTramite.Transferencia ?
                        (int)param.GetParametros("NroTransmisionReferencia").ValornumParam :
                        (int)param.GetParametros("NroSolicitudReferencia").ValornumParam;

                    int idSol = encomiendaEntity.id_tipotramite == (int)Constantes.TipoDeTramite.Transferencia ?
                        encomiendaEntity.Encomienda_Transf_Solicitudes.FirstOrDefault().id_solicitud :
                        encomiendaEntity.Encomienda_SSIT_Solicitudes.FirstOrDefault().id_solicitud;

                    if (encomiendaEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && encomiendaEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
                        throw new Exception(Errors.ENCOMIENDA_CAMBIOS);

                    var ubicacionEntity = repoUbic.Single(objectDto.IdUbicacion.Value);
                    
                    if (idSol > nroSolReferencia && ubicacionEntity.Ubicaciones_Distritos.Count == 0 && ubicacionEntity.Ubicaciones_ZonasMixtura.Count == 0)
                            throw new Exception(Errors.UBICACION_SIN_ZONIFICACION); 

                    var inhibidas = repoUbicInhi.GetByFKIdUbicacion(ubicacionEntity.id_ubicacion);

                    if (inhibidas.Any(p => !p.fecha_vencimiento.HasValue || (DateTime.Now >= p.fecha_inhibicion && DateTime.Now <= p.fecha_vencimiento.Value)))
                        throw new Exception(Errors.UBICACION_INHIBIDA);

                    var encomiendaUbicEntity = repo.GetByFKIdEncomienda(objectDto.IdEncomienda.Value).Where(x => x.id_ubicacion == objectDto.IdUbicacion.Value);

                    if (encomiendaUbicEntity.Count() > 0)
                        throw new Exception(Errors.UBICACION_IGUAL);

                    var elementDto = mapperBase.Map<EncomiendaUbicacionesDTO, Encomienda_Ubicaciones>(objectDto);
                    elementDto.id_zonaplaneamiento = ubicacionEntity.id_zonaplaneamiento;
                    repo.Insert(elementDto);

                    repoPuertas = new EncomiendaUbicacionesPuertasRepository(unitOfWork);
                    CallesBL callesBL = new CallesBL();
                    foreach (UbicacionesPuertasDTO puerta in objectDto.Puertas)
                    {
                        EncomiendaUbicacionesPuertasDTO puertaDTO = new EncomiendaUbicacionesPuertasDTO();

                        puertaDTO.CodigoCalle = puerta.CodigoCalle;
                        puertaDTO.IdEncomiendaUbicacion = elementDto.id_encomiendaubicacion;
                        puertaDTO.NombreCalle = callesBL.GetNombre(puerta.CodigoCalle, puerta.NroPuertaUbic);
                        puertaDTO.NroPuerta = puerta.NroPuertaUbic;
                        var element = mapperPuerta.Map<EncomiendaUbicacionesPuertasDTO, Encomienda_Ubicaciones_Puertas>(puertaDTO);

                        repoPuertas.Insert(element);
                    }
                    repoPH = new EncomiendaUbicacionesPropiedadHorizontalRepository(unitOfWork);
                    foreach (UbicacionesPropiedadhorizontalDTO ph in objectDto.PropiedadesHorizontales)
                    {
                        EncomiendaUbicacionesPropiedadHorizontalDTO phDTO = new EncomiendaUbicacionesPropiedadHorizontalDTO();

                        phDTO.IdEncomiendaUbicacion = elementDto.id_encomiendaubicacion;
                        phDTO.IdPropiedadHorizontal = ph.IdPropiedadHorizontal;

                        var element = mapperPH.Map<EncomiendaUbicacionesPropiedadHorizontalDTO, Encomienda_Ubicaciones_PropiedadHorizontal>(phDTO);

                        repoPH.Insert(element);
                    }

                    if (string.IsNullOrEmpty(encomiendaEntity.ZonaDeclarada))
                    {
                        ZonasPlaneamientoBL zonaPlaneamientoBL = new ZonasPlaneamientoBL();
                        var Zona = zonaPlaneamientoBL.Single(ubicacionEntity.id_zonaplaneamiento);
                        if (!string.IsNullOrEmpty(Zona.CodZonaPla))
                        {
                            encomiendaEntity.ZonaDeclarada = Zona.CodZonaPla;
                            repoEnc.Update(encomiendaEntity);
                        }
                    }

                    unitOfWork.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region Métodos de actualizacion e insert
        /// <summary>
        /// elimina la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>      
        public void Delete(EncomiendaUbicacionesDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork();
                repoEnc = new EncomiendaRepository(unitOfWork);

                var encomiendaEntity = repoEnc.Single(objectDto.IdEncomienda.Value);
                if (encomiendaEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && encomiendaEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
                    throw new Exception(Errors.ENCOMIENDA_CAMBIOS);

                var encomiendaUbicaciones = encomiendaEntity.Encomienda_Ubicaciones.Select(p => p).Where(p => p.id_encomiendaubicacion != objectDto.IdEncomiendaUbicacion);
                string ZonaDeclarada = null;

                if (encomiendaUbicaciones.Any())
                {
                    ZonaDeclarada = encomiendaEntity.ZonaDeclarada;
                    repoZonaPlaneamiento = new ZonasPlaneamientoRepository(unitOfWork);
                    IEnumerable<Zonas_Planeamiento> zonas = encomiendaUbicaciones.Select(p => p.Ubicaciones).Select(p => p.Zonas_Planeamiento);
                    foreach (var complementaria in encomiendaUbicaciones.Select(p => p.Ubicaciones).Select(p => p.Ubicaciones_ZonasComplementarias))
                        zonas.Concat(complementaria.Select(p => p.Zonas_Planeamiento));

                    if (!string.IsNullOrEmpty(ZonaDeclarada))
                    {
                        string zonaNueva = null;
                        Zonas_Planeamiento zonaNuevaQ = zonas.FirstOrDefault();

                        if (zonaNuevaQ != null)
                            zonaNueva = zonaNuevaQ.CodZonaPla;

                        if (!zonas.Any(p => p.CodZonaPla.Equals(ZonaDeclarada)))
                            ZonaDeclarada = zonaNueva;
                    }
                }
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWorkTran = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaUbicacionesRepository(unitOfWorkTran);
                    var encomiendaUbicacionEntity = repo.Single(objectDto.IdEncomiendaUbicacion);

                    repoPH = new EncomiendaUbicacionesPropiedadHorizontalRepository(unitOfWorkTran);
                    repoPH.RemoveRange(encomiendaUbicacionEntity.Encomienda_Ubicaciones_PropiedadHorizontal);

                    repoPuertas = new EncomiendaUbicacionesPuertasRepository(unitOfWorkTran);
                    repoPuertas.RemoveRange(encomiendaUbicacionEntity.Encomienda_Ubicaciones_Puertas);

                    repoMixturas = new EncomiendaUbicacionesMixturasRepository(unitOfWorkTran);
                    repoMixturas.RemoveRange(encomiendaUbicacionEntity.Encomienda_Ubicaciones_Mixturas);

                    repoDistritos = new EncomiendaUbicacionesDistritosRepository(unitOfWorkTran);
                    repoDistritos.RemoveRange(encomiendaUbicacionEntity.Encomienda_Ubicaciones_Distritos);

                    repo.Delete(encomiendaUbicacionEntity);

                    repoEnc = new EncomiendaRepository(unitOfWorkTran);
                    var encomiendaEntityUpdate = repoEnc.Single(objectDto.IdEncomienda);
                    encomiendaEntityUpdate.ZonaDeclarada = ZonaDeclarada;

                    repoEnc.Update(encomiendaEntityUpdate);

                    unitOfWorkTran.Commit();
                }
            }
            catch
            {
                throw;
            }
        }


        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idEncomienda1"></param>
        /// <param name="idEncomienda2"></param>
        /// <returns></returns>
        public bool CompareEntreEncomienda(int idEncomienda1, int idEncomienda2)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaUbicacionesRepository(this.uowF.GetUnitOfWork());
            var compare = repo.CompareDataSaved(idEncomienda1, idEncomienda2);
            return compare;
        }

        public bool PoseeDistritosU(int idEncomienda)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            UbicacionesGruposDistritosRepository repoGrupoDistritos = new UbicacionesGruposDistritosRepository(this.uowF.GetUnitOfWork());
            return repoGrupoDistritos.PoseeDistritosU(idEncomienda);
        }

    }
}

