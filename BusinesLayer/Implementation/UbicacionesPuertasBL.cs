using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBusinessLayer;
using DataTransferObject;
using BaseRepository;
using UnitOfWork;
using AutoMapper;
using DataAcess;
using Dal.UnitOfWork;

namespace BusinesLayer.Implementation
{
    public class UbicacionesPuertasBL : IUbicacionesPuertasBL<UbicacionesPuertasDTO>
    {
        private UbicacionesPuertasRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public UbicacionesPuertasBL()
        {
            var config = new MapperConfiguration(cfg =>
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
            mapperBase = config.CreateMapper();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UbicacionesPuertasDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesPuertasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Ubicaciones_Puertas>, IEnumerable<UbicacionesPuertasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UbicacionesPuertasDTO Single(int IdUbicacionPuerta)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new UbicacionesPuertasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdUbicacionPuerta);
                var entityDto = mapperBase.Map<Ubicaciones_Puertas, UbicacionesPuertasDTO>(entity);

                entityDto.Nombre_calle = Bus_NombreCalle(entityDto.CodigoCalle, entityDto.NroPuertaUbic);

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
        /// <param name="IdUbicacion"></param>
        /// <returns></returns>	
        public IEnumerable<UbicacionesPuertasDTO> GetByFKIdUbicacion(int IdUbicacion)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new UbicacionesPuertasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdUbicacion(IdUbicacion);
            var elementsDto = mapperBase.Map<IEnumerable<Ubicaciones_Puertas>, IEnumerable<UbicacionesPuertasDTO>>(elements);

            SSITSolicitudesUbicacionesBL ubicacionesBL = new SSITSolicitudesUbicacionesBL();

            foreach (var element in elementsDto)
            {
                //if (ubicacionesBL.esUbicacionEspecialConObjetoTerritorialByIdUbicacion(IdUbicacion))
                //{
                //    element.NuevaPuerta += 't';
                //}
                element.Nombre_calle = Bus_NombreCalle(element.CodigoCalle, element.NroPuertaUbic);
            }



            return elementsDto;
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

                if (item.AlturaIzquierdaFin_calle > item.AlturaDerechaFin_calle)
                    AlturaFin = (int)item.AlturaIzquierdaFin_calle;

                if (NroPuerta >= AlturaInicio && NroPuerta <= AlturaFin)
                    nombre_calle = item.NombreOficial_calle;
            }

            return nombre_calle;
        }

        #region Métodos de actualizacion e insert
        /// <summary>
        /// Inserta la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public bool Insert(UbicacionesPuertasDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new UbicacionesPuertasRepository(unitOfWork);
                    var elementDto = mapperBase.Map<UbicacionesPuertasDTO, Ubicaciones_Puertas>(objectDto);
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
        public void Update(UbicacionesPuertasDTO objectDTO)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new UbicacionesPuertasRepository(unitOfWork);
                    var elementDTO = mapperBase.Map<UbicacionesPuertasDTO, Ubicaciones_Puertas>(objectDTO);
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
        public void Delete(UbicacionesPuertasDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new UbicacionesPuertasRepository(unitOfWork);
                    var elementDto = mapperBase.Map<UbicacionesPuertasDTO, Ubicaciones_Puertas>(objectDto);
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
