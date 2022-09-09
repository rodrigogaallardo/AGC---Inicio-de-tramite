using AutoMapper;
using BaseRepository;
using IBusinessLayer;
using Dal.UnitOfWork;
using DataAcess;
using DataAcess.EntityCustom;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitOfWork;
using StaticClass;
using ExternalService;

namespace BusinesLayer.Implementation
{
    public class UbicacionesBL : IUbicacionesBL<UbicacionesDTO>
    {
        private ItemDirectionRepository itemRepo = null;
        private UbicacionesRepository repo = null;
        private UsuarioRepository repoUsu = null;
        private IUnitOfWorkFactory uowF = null;

        IMapper mapperBase;
        IMapper mapperBaseUbicacionPuerta = null;

        public UbicacionesBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UbicacionesDTO, Ubicaciones>().ReverseMap()
                    .ForMember(dest => dest.IdUbicacion, source => source.MapFrom(p => p.id_ubicacion))
                    .ForMember(dest => dest.IdSubtipoUbicacion, source => source.MapFrom(p => p.id_subtipoubicacion))
                    .ForMember(dest => dest.IdZonaPlaneamiento, source => source.MapFrom(p => p.id_zonaplaneamiento))
                    .ForMember(dest => dest.CoordenadaX, source => source.MapFrom(p => p.Coordenada_X))
                    .ForMember(dest => dest.CoordenadaY, source => source.MapFrom(p => p.Coordenada_Y))
                    .ForMember(dest => dest.InhibidaObservacion, source => source.MapFrom(p => p.Inhibida_Observacion))
                    .ForMember(dest => dest.BajaLogica, source => source.MapFrom(p => p.baja_logica))
                    .ForMember(dest => dest.PisosBajoRasante, source => source.MapFrom(p => p.pisos_bajo_rasante))
                    .ForMember(dest => dest.PisosSobreRasante, source => source.MapFrom(p => p.pisos_sobre_rasante))
                    .ForMember(dest => dest.Unidades, source => source.MapFrom(p => p.unidades))
                    .ForMember(dest => dest.Locales, source => source.MapFrom(p => p.locales))
                    .ForMember(dest => dest.CantPh, source => source.MapFrom(p => p.cant_ph))
                    .ForMember(dest => dest.Vuc, source => source.MapFrom(p => p.vuc))
                    .ForMember(dest => dest.IdComuna, source => source.MapFrom(p => p.id_comuna))
                    .ForMember(dest => dest.IdBarrio, source => source.MapFrom(p => p.id_barrio))
                    .ForMember(dest => dest.IdAreaHospitalaria, source => source.MapFrom(p => p.id_areahospitalaria))
                    .ForMember(dest => dest.IdComisaria, source => source.MapFrom(p => p.id_comisaria))
                    .ForMember(dest => dest.IdDistritoEscolar, source => source.MapFrom(p => p.id_distritoescolar))
                    .ForMember(dest => dest.FechaUltimaActualizacionUsig, source => source.MapFrom(p => p.FechaUltimaActualizacionUSIG))
                    .ForMember(dest => dest.CantiActualizacionesUsig, source => source.MapFrom(p => p.CantiActualizacionesUSIG))
                    .ForMember(dest => dest.TipoPersonaTitularAgip, source => source.MapFrom(p => p.TipoPersonaTitularAGIP))
                    .ForMember(dest => dest.TitularAgip, source => source.MapFrom(p => p.TitularAGIP))
                    .ForMember(dest => dest.FechaAltaAgip, source => source.MapFrom(p => p.FechaAltaAGIP))
                    .ForMember(dest => dest.ZonasPlaneamiento, source => source.MapFrom(p => p.Zonas_Planeamiento))
                    .ForMember(dest => dest.UbicacionesPuertas, source => source.MapFrom(p => p.Ubicaciones_Puertas))
                    .ForMember(dest => dest.UbicacionesZonasMixturasDTO, source => source.MapFrom(p => p.Ubicaciones_ZonasMixtura))
                    .ForMember(dest => dest.UbicacionesDistritosDTO, source => source.MapFrom(p => p.Ubicaciones_Distritos));

                cfg.CreateMap<Ubicaciones, UbicacionesDTO>().ReverseMap()
                    .ForMember(dest => dest.id_ubicacion, source => source.MapFrom(p => p.IdUbicacion))
                    .ForMember(dest => dest.id_subtipoubicacion, source => source.MapFrom(p => p.IdSubtipoUbicacion))
                    .ForMember(dest => dest.id_zonaplaneamiento, source => source.MapFrom(p => p.IdZonaPlaneamiento))
                    .ForMember(dest => dest.Coordenada_X, source => source.MapFrom(p => p.CoordenadaX))
                    .ForMember(dest => dest.Coordenada_Y, source => source.MapFrom(p => p.CoordenadaY))
                    .ForMember(dest => dest.Inhibida_Observacion, source => source.MapFrom(p => p.InhibidaObservacion))
                    .ForMember(dest => dest.baja_logica, source => source.MapFrom(p => p.BajaLogica))
                    .ForMember(dest => dest.pisos_bajo_rasante, source => source.MapFrom(p => p.PisosBajoRasante))
                    .ForMember(dest => dest.pisos_sobre_rasante, source => source.MapFrom(p => p.PisosSobreRasante))
                    .ForMember(dest => dest.unidades, source => source.MapFrom(p => p.Unidades))
                    .ForMember(dest => dest.locales, source => source.MapFrom(p => p.Locales))
                    .ForMember(dest => dest.cant_ph, source => source.MapFrom(p => p.CantPh))
                    .ForMember(dest => dest.vuc, source => source.MapFrom(p => p.Vuc))
                    .ForMember(dest => dest.id_comuna, source => source.MapFrom(p => p.IdComuna))
                    .ForMember(dest => dest.id_barrio, source => source.MapFrom(p => p.IdBarrio))
                    .ForMember(dest => dest.id_areahospitalaria, source => source.MapFrom(p => p.IdAreaHospitalaria))
                    .ForMember(dest => dest.id_comisaria, source => source.MapFrom(p => p.IdComisaria))
                    .ForMember(dest => dest.id_distritoescolar, source => source.MapFrom(p => p.IdDistritoEscolar))
                    .ForMember(dest => dest.FechaUltimaActualizacionUSIG, source => source.MapFrom(p => p.FechaUltimaActualizacionUsig))
                    .ForMember(dest => dest.CantiActualizacionesUSIG, source => source.MapFrom(p => p.CantiActualizacionesUsig))
                    .ForMember(dest => dest.TipoPersonaTitularAGIP, source => source.MapFrom(p => p.TipoPersonaTitularAgip))
                    .ForMember(dest => dest.TitularAGIP, source => source.MapFrom(p => p.TitularAgip))
                    .ForMember(dest => dest.FechaAltaAGIP, source => source.MapFrom(p => p.FechaAltaAgip))
                    .ForMember(dest => dest.Ubicaciones_Puertas, source => source.MapFrom(p => p.UbicacionesPuertas))
                    .ForMember(dest => dest.Ubicaciones_ZonasMixtura, source => source.MapFrom(p => p.UbicacionesZonasMixturasDTO))
                    .ForMember(dest => dest.Ubicaciones_Distritos, source => source.MapFrom(p => p.UbicacionesDistritosDTO));

                cfg.CreateMap<Ubicaciones_Puertas, UbicacionesPuertasDTO>()
                    .ForMember(dest => dest.IdUbicacion, source => source.MapFrom(p => p.id_ubicacion))
                    .ForMember(dest => dest.CodigoCalle, source => source.MapFrom(p => p.codigo_calle))
                    .ForMember(dest => dest.IdUbicacionPuerta, source => source.MapFrom(p => p.id_ubic_puerta))
                    .ForMember(dest => dest.NroPuertaUbic, source => source.MapFrom(p => p.NroPuerta_ubic))
                    .ForMember(dest => dest.TipoPuerta, source => source.MapFrom(p => p.tipo_puerta));


                cfg.CreateMap<Zonas_Planeamiento, ZonasPlaneamientoDTO>()
                    .ForMember(dest => dest.IdZonaPlaneamiento, source => source.MapFrom(p => p.id_zonaplaneamiento));

                cfg.CreateMap<UbicacionesZonasMixturasDTO, Ubicaciones_ZonasMixtura>().ReverseMap()
                    .ForMember(dest => dest.IdZona, source => source.MapFrom(p => p.IdZonaMixtura));

                cfg.CreateMap<Ubicaciones_ZonasMixtura, UbicacionesZonasMixturasDTO>().ReverseMap()
                     .ForMember(dest => dest.IdZonaMixtura, source => source.MapFrom(p => p.IdZona));

                cfg.CreateMap<Ubicaciones_Distritos, UbicacionesDistritosDTO>().ReverseMap();

                cfg.CreateMap<Ubicaciones_CatalogoDistritos, UbicacionesCatalogoDistritosDTO>().ReverseMap();
            });
            mapperBase = config.CreateMapper();

            var configUbicPuer = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UbicacionesPuertasDTO, Ubicaciones_Puertas>().ReverseMap()
                    .ForMember(dest => dest.IdUbicacionPuerta, source => source.MapFrom(p => p.id_ubic_puerta))
                    .ForMember(dest => dest.IdUbicacion, source => source.MapFrom(p => p.id_ubicacion))
                    .ForMember(dest => dest.TipoPuerta, source => source.MapFrom(p => p.tipo_puerta))
                    .ForMember(dest => dest.CodigoCalle, source => source.MapFrom(p => p.codigo_calle))
                    .ForMember(dest => dest.NroPuertaUbic, source => source.MapFrom(p => p.NroPuerta_ubic));

                cfg.CreateMap<Ubicaciones_Puertas, UbicacionesPuertasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_ubic_puerta, source => source.MapFrom(p => p.IdUbicacionPuerta))
                    .ForMember(dest => dest.id_ubicacion, source => source.MapFrom(p => p.IdUbicacion))
                    .ForMember(dest => dest.tipo_puerta, source => source.MapFrom(p => p.TipoPuerta))
                    .ForMember(dest => dest.codigo_calle, source => source.MapFrom(p => p.CodigoCalle))
                    .ForMember(dest => dest.NroPuerta_ubic, source => source.MapFrom(p => p.NroPuertaUbic));
            });
            mapperBaseUbicacionPuerta = configUbicPuer.CreateMapper();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UbicacionesDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Ubicaciones>, IEnumerable<UbicacionesDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdUbicacion"></param>
        /// <returns></returns>
        public UbicacionesDTO Single(int IdUbicacion)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdUbicacion);
                var entityDto = mapperBase.Map<Ubicaciones, UbicacionesDTO>(entity);

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
        /// <param name="NroPartida"></param>                                                                                            Ubicaciones
        /// <param name="FechaActual"></param>
        /// <returns></returns>
        public IEnumerable<UbicacionesDTO> Get(int NroPartida, DateTime FechaActual)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesRepository(this.uowF.GetUnitOfWork());

                var elements = repo.Get(NroPartida, FechaActual);
                var elementsDto = mapperBase.Map<IEnumerable<Ubicaciones>, IEnumerable<UbicacionesDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="NroPartida"></param>
        /// <param name="FechaActual"></param>
        /// <returns></returns>
        public IEnumerable<UbicacionesDTO> GetXPartidaHorizontal(int NroPartida, DateTime FechaActual)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesRepository(this.uowF.GetUnitOfWork());

                var elements = repo.GetXPartidaHorizontal(NroPartida, FechaActual);
                var elementsDto = mapperBase.Map<IEnumerable<Ubicaciones>, IEnumerable<UbicacionesDTO>>(elements);

                return elementsDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="NroPartida"></param>
        /// <param name="FechaActual"></param>
        /// <param name="CodigoCalle"></param>
        /// <returns></returns>
        public IEnumerable<UbicacionesDTO> GetXPartidaHorizontal(int NroPartida, DateTime FechaActual, int CodigoCalle)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesRepository(this.uowF.GetUnitOfWork());

                var elements = repo.GetXPuerta(NroPartida, FechaActual, CodigoCalle);
                var elementsDto = mapperBase.Map<IEnumerable<Ubicaciones>, IEnumerable<UbicacionesDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="minvaluePuerta"></param>
        /// <param name="maxvaluePuerta"></param>
        /// <param name="FechaActual"></param>
        /// <param name="CodigoCalle"></param>
        /// <param name="parimpar"></param>
        /// <returns></returns>
        public IEnumerable<UbicacionesDTO> Get(int minvaluePuerta, int maxvaluePuerta, DateTime FechaActual, int CodigoCalle, int parimpar)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesRepository(this.uowF.GetUnitOfWork());

                var elements = repo.Get(minvaluePuerta, maxvaluePuerta, FechaActual, CodigoCalle, parimpar);
                var elementsDto = mapperBase.Map<IEnumerable<Ubicaciones>, IEnumerable<UbicacionesDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Seccion"></param>
        /// <param name="Manzana"></param>
        /// <param name="Parcela"></param>
        /// <param name="FechaActual"></param>
        /// <returns></returns>
        public IEnumerable<UbicacionesDTO> Get(int Seccion, string Manzana, string Parcela, DateTime FechaActual)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesRepository(this.uowF.GetUnitOfWork());

                var elements = repo.Get(Seccion, Manzana, Parcela, FechaActual);
                var elementsDto = mapperBase.Map<IEnumerable<Ubicaciones>, IEnumerable<UbicacionesDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSubTipoUbicacion"></param>
        /// <param name="FechaActual"></param>
        /// <returns></returns>
        public IEnumerable<UbicacionesDTO> GetXTipo(int IdSubTipoUbicacion, DateTime FechaActual)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesRepository(this.uowF.GetUnitOfWork());

                var elements = repo.GetXTipo(IdSubTipoUbicacion, FechaActual);
                var elementsDto = mapperBase.Map<IEnumerable<Ubicaciones>, IEnumerable<UbicacionesDTO>>(elements);

                return elementsDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Numero"></param>
        /// <param name="Calle"></param>
        /// <param name="FechaActual"></param>
        /// <returns></returns>
        public List<UbicacionesDTO> Buscar(int Numero, int Calle, DateTime FechaActual)
        {
            return Buscar(0, 0, false, FechaActual, Numero, Calle, 0, string.Empty, string.Empty, 0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Seccion"></param>
        /// <param name="Manzana"></param>
        /// <param name="Parcela"></param>
        /// <param name="FechaActual"></param>
        /// <returns></returns>
        public List<UbicacionesDTO> Buscar(int Seccion, string Manzana, string Parcela, DateTime FechaActual)
        {
            return Buscar(2, 0, false, FechaActual, 0, 0, Seccion, Manzana, Parcela, 0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SubtipoUbicacion"></param>
        /// <param name="FechaActual"></param>
        /// <returns></returns>
        public List<UbicacionesDTO> Buscar(int SubtipoUbicacion, DateTime FechaActual)
        {
            return Buscar(3, 0, false, FechaActual, 0, 0, 0, string.Empty, string.Empty, SubtipoUbicacion);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="NroPartida"></param>
        /// <param name="TipoPartida"></param>
        /// <param name="FechaActual"></param>
        /// <returns></returns>
        public List<UbicacionesDTO> Buscar(int NroPartida, bool TipoPartida, DateTime FechaActual)
        {
            return Buscar(1, NroPartida, TipoPartida, FechaActual, 0, 0, 0, string.Empty, string.Empty, 0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TipoBusqueda"></param>
        /// <param name="NroPartida"></param>
        /// <param name="TipoPartida"></param>
        /// <param name="FechaActual"></param>
        /// <param name="Numero"></param>
        /// <param name="Calle"></param>
        /// <param name="Seccion"></param>
        /// <param name="Manzana"></param>
        /// <param name="Parcela"></param>
        /// <param name="SubtipoUbicacion"></param>
        /// <returns></returns>
        public List<UbicacionesDTO> Buscar(int TipoBusqueda, int NroPartida, bool TipoPartida, DateTime FechaActual, int Numero, int Calle, int Seccion, string Manzana, string Parcela, int SubtipoUbicacion)
        {
            List<UbicacionesDTO> result = null;

            switch (TipoBusqueda)
            {
                case 1:

                    if (TipoPartida)
                    {
                        var query1 = GetXPartidaHorizontal(NroPartida, FechaActual);

                        result = query1.ToList();
                    }
                    else
                    {
                        var query1 = Get(NroPartida, FechaActual);
                        UbicacionesPuertasBL numeroCalle1 = new UbicacionesPuertasBL();

                        result = query1.ToList();
                        foreach (var item in result)
                        {

                            ItemDirectionDTO calleN = new ItemDirectionDTO();


                            IEnumerable<UbicacionesPuertasDTO> numeroCalle = numeroCalle1.GetByFKIdUbicacion(item.IdUbicacion).ToList();
                            foreach (var item2 in numeroCalle)
                            {
                                string calleNombre1 = Bus_NombreCalle(item2.CodigoCalle, item2.NroPuertaUbic);
                                calleN.direccion = calleNombre1;
                                if (calleN.Numero == null)
                                {
                                    calleN.Numero = item2.NroPuertaUbic.ToString();
                                }
                                else
                                {
                                    calleN.Numero = calleN.Numero + "/" + item2.NroPuertaUbic.ToString();
                                }
                            }
                            calleN.idUbicacion = item.IdUbicacion;
                            calleN.Seccion = item.Seccion;
                            calleN.Manzana = item.Manzana;
                            calleN.Parcela = item.Parcela;
                            item.Direccion = calleN;
                        }
                    }
                    break;
                case 0:

                    int vNroPuerta = Numero;
                    int minvaluePuerta = 0;
                    int maxvaluePuerta = 0;
                    int parimpar = 0;   // 0 = par - 1 impar

                    int vcodigo_calle = Calle;

                    var query2 = Get(FechaActual, vcodigo_calle, Numero);

                    result = query2.ToList();

                    if (result.Count.Equals(0))
                    {

                        if (vNroPuerta % 100 == 0)
                        {
                            minvaluePuerta = vNroPuerta - 25;
                            maxvaluePuerta = vNroPuerta;
                        }
                        else
                        {
                            minvaluePuerta = Convert.ToInt32(Math.Floor(Convert.ToDecimal(vNroPuerta / 100)) * 100 + 1);
                            maxvaluePuerta = Convert.ToInt32(Math.Floor(Convert.ToDecimal(vNroPuerta / 100 + 1)) * 100);
                            if (minvaluePuerta < vNroPuerta - 34)
                                minvaluePuerta = vNroPuerta - 34;
                            if (maxvaluePuerta > vNroPuerta + 34)
                                maxvaluePuerta = vNroPuerta + 34;

                        }
                        parimpar = vNroPuerta % 2;

                        // se realiza la búsqueda en un rango de +30 y -30 

                        query2 = Get(minvaluePuerta, maxvaluePuerta, FechaActual, vcodigo_calle, parimpar);

                        result = query2.ToList();

                        if (result.Count > 0)
                        {
                            List<UbicacionesDTO> ubicacionesSinDuplicados = new List<UbicacionesDTO>();

                            foreach (UbicacionesDTO ubicacion in query2.ToList())
                            {

                                if (ubicacionesSinDuplicados.Find(x => x.IdUbicacion == ubicacion.IdUbicacion) == null)
                                    ubicacionesSinDuplicados.Add(ubicacion);

                            }

                            result = ubicacionesSinDuplicados;
                        }
                    }
                    UbicacionesPuertasBL numeroCalle0 = new UbicacionesPuertasBL();
                    foreach (var item in result)
                    {

                        ItemDirectionDTO calleN = new ItemDirectionDTO();


                        IEnumerable<UbicacionesPuertasDTO> numeroCalle = numeroCalle0.GetByFKIdUbicacion(item.IdUbicacion).ToList();
                        foreach (var item2 in numeroCalle)
                        {
                            string calleNombre2 = Bus_NombreCalle(item2.CodigoCalle, item2.NroPuertaUbic);
                            calleN.direccion = calleNombre2;
                            if (calleN.Numero == null)
                            {
                                calleN.Numero = item2.NroPuertaUbic.ToString();
                            }
                            else
                            {
                                calleN.Numero = calleN.Numero + "/" + item2.NroPuertaUbic.ToString();
                            }
                        }
                        calleN.idUbicacion = item.IdUbicacion;
                        calleN.Seccion = item.Seccion;
                        calleN.Manzana = item.Manzana;
                        calleN.Parcela = item.Parcela;
                        item.Direccion = calleN;
                    }
                    break;

                case 2:


                    var query3 = Get(Seccion, Manzana, Parcela, FechaActual);
                    UbicacionesPuertasBL numeroCalle2 = new UbicacionesPuertasBL();

                    result = query3.ToList();
                    foreach (var item in result)
                    {
                        IEnumerable<UbicacionesPuertasDTO> numeroCalle = numeroCalle2.GetByFKIdUbicacion(item.IdUbicacion).ToList();
                        ItemDirectionDTO calleN = new ItemDirectionDTO();

                        foreach (var item2 in numeroCalle)
                        {
                            string calleNombre = Bus_NombreCalle(item2.CodigoCalle, item2.NroPuertaUbic);
                            calleN.direccion = calleNombre;
                            if (calleN.Numero == null)
                            {
                                calleN.Numero = item2.NroPuertaUbic.ToString();
                            }
                            else
                            {
                                calleN.Numero = calleN.Numero + "/" + item2.NroPuertaUbic.ToString();
                            }
                        }
                        calleN.idUbicacion = item.IdUbicacion;
                        calleN.Seccion = item.Seccion;
                        calleN.Manzana = item.Manzana;
                        calleN.Parcela = item.Parcela;
                        item.Direccion = calleN;
                    }
                    break;
                case 3:
                    var query4 = GetXTipo(SubtipoUbicacion, FechaActual);
                    UbicacionesPuertasBL numeroCalle3 = new UbicacionesPuertasBL();
                    SSITSolicitudesUbicacionesBL ubicacionesBL = new SSITSolicitudesUbicacionesBL();
                    result = query4.ToList();
                    foreach (var item in result)
                    {
                        ItemDirectionDTO calleN = new ItemDirectionDTO();

                        IEnumerable<UbicacionesPuertasDTO> numeroCalle = numeroCalle3.GetByFKIdUbicacion(item.IdUbicacion).ToList();
                        foreach (var item2 in numeroCalle)
                        {
                            string calleNombre3 = Bus_NombreCalle(item2.CodigoCalle, item2.NroPuertaUbic);
                            calleN.direccion = calleNombre3;
                            if (calleN.Numero == null)
                            {

                                if (ubicacionesBL.esUbicacionEspecialConObjetoTerritorialByIdUbicacion(item2.IdUbicacion))
                                {
                                    calleN.Numero = item2.NroPuertaUbic.ToString() + 't';
                                }
                                else
                                {
                                    calleN.Numero = item2.NroPuertaUbic.ToString();
                                }
                            }
                            else
                            {
                                if (ubicacionesBL.esUbicacionEspecialConObjetoTerritorialByIdUbicacion(item2.IdUbicacion))
                                {
                                    calleN.Numero = calleN.Numero + "/" + item2.NroPuertaUbic.ToString()+'t';
                                }
                                else
                                {
                                calleN.Numero = calleN.Numero + "/" + item2.NroPuertaUbic.ToString();
                                }
                            }
                        }
                        calleN.idUbicacion = item.IdUbicacion;
                        calleN.Seccion = item.Seccion;
                        calleN.Manzana = item.Manzana;
                        calleN.Parcela = item.Parcela;
                        item.Direccion = calleN;
                    }
                    break;
            }

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FechaActual"></param>
        /// <param name="CodigoCalle"></param>
        /// <param name="Nro"></param>
        /// <returns></returns>
        public IEnumerable<UbicacionesDTO> Get(DateTime FechaActual, int CodigoCalle, int Nro)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesRepository(this.uowF.GetUnitOfWork());

                var elements = repo.Get(FechaActual, CodigoCalle, Nro);
                var elementsDto = mapperBase.Map<IEnumerable<Ubicaciones>, IEnumerable<UbicacionesDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FechaActual"></param>
        /// <param name="CodigoCalle"></param>
        /// <param name="Nro"></param>
        /// <returns></returns>
        public bool Exists(DateTime FechaActual, int CodigoCalle, int Nro)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesRepository(this.uowF.GetUnitOfWork());

                return repo.Exists(FechaActual, CodigoCalle, Nro);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool validarUbicacionClausuras(int IdUbicacion)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new UbicacionesRepository(this.uowF.GetUnitOfWork());
            return repo.validarUbicacionClausuras(IdUbicacion);
        }

        public bool validarUbicacionInhibiciones(int IdUbicacion)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new UbicacionesRepository(this.uowF.GetUnitOfWork());
            return repo.validarUbicacionInhibiciones(IdUbicacion);
        }

        private string Bus_NombreCalle(int codigo_calle, int NroPuerta)
        {
            string nombre_calle = "";

            CallesBL calles = new CallesBL();

            IEnumerable<CallesDTO> lstCalles = calles.Get(codigo_calle);

            int AlturaInicio = 0;
            int AlturaFin = 0;

            foreach (CallesDTO item in lstCalles)
            {
                AlturaInicio = (int)item.AlturaDerechaInicio_calle;

                if (item.AlturaIzquierdaInicio_calle < item.AlturaDerechaInicio_calle)
                    AlturaInicio = (int)item.AlturaIzquierdaInicio_calle;

                AlturaFin = (int)item.AlturaDerechaFin_calle;

                if (item.AlturaIzquierdaFin_calle < item.AlturaDerechaFin_calle)
                    AlturaFin = (int)item.AlturaIzquierdaFin_calle;

                if (NroPuerta >= AlturaInicio && NroPuerta <= AlturaFin)
                    nombre_calle = item.NombreOficial_calle;
            }

            return nombre_calle;
        }

        public void SendMailSolicitudNuevaPuerta(Guid userid, int id_ubicacion, int NroPuerta, string calle)
        {

            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork();

                repo = new UbicacionesRepository(unitOfWork);
                repoUsu = new UsuarioRepository(unitOfWork);

                MailMessages mailer = new MailMessages();
                string UrlMapa = "";

                var ubicEntity = repo.Single(id_ubicacion);
                var usuarioEntity = repoUsu.Single(userid);

                var lstPuertaEntity = mapperBaseUbicacionPuerta.Map<IEnumerable<Ubicaciones_Puertas>, IEnumerable<UbicacionesPuertasDTO>>(ubicEntity.Ubicaciones_Puertas);

                foreach (var element in lstPuertaEntity)
                    element.Nombre_calle = Bus_NombreCalle(element.CodigoCalle, element.NroPuertaUbic);

                var puerta = lstPuertaEntity.FirstOrDefault();
                if (puerta != null)
                {
                    string direccion = string.Format("{0} {1}", puerta.Nombre_calle, puerta.NroPuertaUbic);
                    UrlMapa = Funciones.GetUrlMapa(ubicEntity.Seccion.Value, ubicEntity.Manzana, ubicEntity.Parcela, direccion);
                }

                string urlFoto = Funciones.GetUrlFoto(ubicEntity.Seccion.Value, ubicEntity.Manzana, ubicEntity.Parcela, 350, 250);

                string htmlBody = mailer.MailSolicitudNuevaPuerta(usuarioEntity.UserName
                                                                , usuarioEntity.Apellido
                                                                , usuarioEntity.Nombre
                                                                , usuarioEntity.Email
                                                                , ubicEntity.NroPartidaMatriz.ToString()
                                                                , ubicEntity.Seccion.ToString()
                                                                , ubicEntity.Manzana
                                                                , ubicEntity.Parcela
                                                                , calle
                                                                , NroPuerta.ToString()
                                                                , urlFoto
                                                                , UrlMapa);

                EmailServiceBL mailService = new EmailServiceBL();
                EmailEntity emailEntity = new EmailEntity();
                emailEntity.Email = usuarioEntity.Email;
                emailEntity.Html = htmlBody;
                emailEntity.Asunto = "Solicitud de nueva calle en parcela";
                emailEntity.IdEstado = (int)ExternalService.TiposDeEstadosEmail.PendienteDeEnvio;
                emailEntity.IdTipoEmail = (int)ExternalService.TiposDeMail.Generico;
                emailEntity.IdOrigen = (int)ExternalService.MailOrigenes.SSIT;
                emailEntity.CantIntentos = 3;
                emailEntity.CantMaxIntentos = 3;
                emailEntity.FechaAlta = DateTime.Now;
                emailEntity.Prioridad = 1;

                mailService.SendMail(emailEntity);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ItemDirectionDTO> GetDirecciones(List<int> lstIDUbicaciones)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                itemRepo = new ItemDirectionRepository(this.uowF.GetUnitOfWork());
                List<ItemPuertaEntity> LstDoorsDirection = itemRepo.GetDireccionesUbic(lstIDUbicaciones).ToList();
                //List<ItemPuertaDTO> lstPuertas = mapperItemPuerta.Map<IEnumerable<ItemPuertaEntity>, IEnumerable<ItemPuertaDTO>>(LstDoorsDirection).ToList();
                List<ItemDirectionDTO> lstDirecciones = new List<ItemDirectionDTO>();

                int id_solicitud_ant = 0;
                string calle_ant = "";
                string Direccion_armada = "";

                if (LstDoorsDirection.Count() > 0)
                {
                    id_solicitud_ant = LstDoorsDirection[0].id_solicitud;
                    calle_ant = LstDoorsDirection[0].calle;
                }

                foreach (var puerta in LstDoorsDirection)
                {
                    if (id_solicitud_ant != puerta.id_solicitud)
                    {
                        ItemDirectionDTO itemDireccion = new ItemDirectionDTO();
                        itemDireccion.id_solicitud = id_solicitud_ant;
                        itemDireccion.direccion = Direccion_armada;
                        lstDirecciones.Add(itemDireccion);
                        Direccion_armada = "";
                        id_solicitud_ant = puerta.id_solicitud;
                    }

                    if (Direccion_armada.Length == 0 || puerta.calle != calle_ant)
                    {
                        if (Direccion_armada.Length > 0)
                            Direccion_armada += " -";

                        Direccion_armada += puerta.calle + " " + puerta.puerta;
                    }
                    else
                    {
                        Direccion_armada += " / " + puerta.puerta;
                    }

                    calle_ant = puerta.calle;


                }

                if (Direccion_armada.Length > 0)
                {
                    ItemDirectionDTO itemDireccion = new ItemDirectionDTO();
                    itemDireccion.id_solicitud = id_solicitud_ant;
                    itemDireccion.direccion = Direccion_armada;
                    lstDirecciones.Add(itemDireccion);
                    Direccion_armada = "";
                }

                return lstDirecciones;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Métodos de actualizacion e insert
        /// <summary>
        /// Inserta la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public bool Insert(UbicacionesDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new UbicacionesRepository(unitOfWork);
                    var elementDto = mapperBase.Map<UbicacionesDTO, Ubicaciones>(objectDto);
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


        #endregion
        #region Métodos de actualizacion e insert
        /// <summary>
        /// Modifica la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public void Update(UbicacionesDTO objectDTO)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new UbicacionesRepository(unitOfWork);
                    var elementDTO = mapperBase.Map<UbicacionesDTO, Ubicaciones>(objectDTO);
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
        public void Delete(UbicacionesDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new UbicacionesRepository(unitOfWork);
                    var elementDto = mapperBase.Map<UbicacionesDTO, Ubicaciones>(objectDto);
                    var insertOk = repo.Delete(elementDto);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion
    }
}

