using AutoMapper;
using BaseRepository;
using IBusinessLayer;
using Dal.UnitOfWork;
using DataAcess;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitOfWork;
using StaticClass;

namespace BusinesLayer.Implementation
{
    public class EncomiendaTitularesPersonasFisicasBL : IEncomiendaTitularesPersonasfisicasBL<EncomiendaTitularesPersonasFisicasDTO>
    {
        private EncomiendaTitularesPersonasFisicasRepository repo = null;
        private EncomiendaFirmantesPersonasFisicasRepository repoFir = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public EncomiendaTitularesPersonasFisicasBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EncomiendaTitularesPersonasFisicasDTO, Encomienda_Titulares_PersonasFisicas>().ReverseMap()
                    .ForMember(dest => dest.IdPersonaFisica, source => source.MapFrom(p => p.id_personafisica))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.IdTipoDocPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.NroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.IdTipoiibb, source => source.MapFrom(p => p.id_tipoiibb))
                    .ForMember(dest => dest.IngresosBrutos, source => source.MapFrom(p => p.Ingresos_Brutos))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.Nro_Puerta))
                    .ForMember(dest => dest.IdLocalidad, source => source.MapFrom(p => p.Id_Localidad))
                    .ForMember(dest => dest.CodigoPostal, source => source.MapFrom(p => p.Codigo_Postal))
                    .ForMember(dest => dest.EncomiendaFirmantesPersonasFisicasDTO, source => source.MapFrom(p => p.Encomienda_Firmantes_PersonasFisicas))
                    .ForMember(dest => dest.LocalidadDTO, source => source.MapFrom(p => p.Localidad))
                    .ForMember(dest => dest.TiposDeIngresosBrutosDTO, source => source.MapFrom(p => p.TiposDeIngresosBrutos))
                    .ForMember(dest => dest.TipoDocumentoPersonalDTO, source => source.MapFrom(p => p.TipoDocumentoPersonal));

                cfg.CreateMap<Encomienda_Titulares_PersonasFisicas, EncomiendaTitularesPersonasFisicasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_personafisica, source => source.MapFrom(p => p.IdPersonaFisica))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocPersonal))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NroDocumento))
                    .ForMember(dest => dest.id_tipoiibb, source => source.MapFrom(p => p.IdTipoiibb))
                    .ForMember(dest => dest.Ingresos_Brutos, source => source.MapFrom(p => p.IngresosBrutos))
                    .ForMember(dest => dest.Nro_Puerta, source => source.MapFrom(p => p.NroPuerta))
                    .ForMember(dest => dest.Id_Localidad, source => source.MapFrom(p => p.IdLocalidad))
                    .ForMember(dest => dest.Codigo_Postal, source => source.MapFrom(p => p.CodigoPostal));


                cfg.CreateMap<EncomiendaFirmantesPersonasFisicasDTO, Encomienda_Firmantes_PersonasFisicas>().ReverseMap()
                    .ForMember(dest => dest.IdFirmantePf, source => source.MapFrom(p => p.id_firmante_pf))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.IdPersonaFisica, source => source.MapFrom(p => p.id_personafisica))
                    .ForMember(dest => dest.IdTipodocPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.NroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.IdTipoCaracter, source => source.MapFrom(p => p.id_tipocaracter))
                    .ForMember(dest => dest.TipoDocumentoPersonalDTO, source => source.MapFrom(p => p.TipoDocumentoPersonal))
                    .ForMember(dest => dest.TiposDeCaracterLegalDTO, source => source.MapFrom(p => p.TiposDeCaracterLegal));

                cfg.CreateMap<TiposDeIngresosBrutosDTO, TiposDeIngresosBrutos>().ReverseMap()
                    .ForMember(dest => dest.IdTipoIb, source => source.MapFrom(p => p.id_tipoiibb))
                    .ForMember(dest => dest.CodTipoIb, source => source.MapFrom(p => p.cod_tipoibb))
                    .ForMember(dest => dest.NomTipoIb, source => source.MapFrom(p => p.nom_tipoiibb))
                    .ForMember(dest => dest.FormatoTipoIb, source => source.MapFrom(p => p.formato_tipoiibb));

                cfg.CreateMap<ProvinciaDTO, Provincia>().ReverseMap();

                cfg.CreateMap<LocalidadDTO, Localidad>().ReverseMap()
                    .ForMember(dest => dest.ProvinciaDTO, source => source.MapFrom(p => p.Provincia));

                cfg.CreateMap<TipoDocumentoPersonalDTO, TipoDocumentoPersonal>();

                cfg.CreateMap<TipoDocumentoPersonal, TipoDocumentoPersonalDTO>();

                cfg.CreateMap<TiposDeCaracterLegal, TiposDeCaracterLegalDTO>()
                    .ForMember(dest => dest.IdTipoCaracter, source => source.MapFrom(p => p.id_tipocaracter))
                    .ForMember(dest => dest.CodTipoCaracter, source => source.MapFrom(p => p.cod_tipocaracter))
                    .ForMember(dest => dest.NomTipoCaracter, source => source.MapFrom(p => p.nom_tipocaracter))
                    .ForMember(dest => dest.DisponibilidadTipoCaracter, source => source.MapFrom(p => p.disponibilidad_tipocaracter))
                    .ForMember(dest => dest.MuestraCargoTipoCaracter, source => source.MapFrom(p => p.muestracargo_tipocaracter));

                cfg.CreateMap<TiposDeCaracterLegalDTO, TiposDeCaracterLegal>()
                    .ForMember(dest => dest.id_tipocaracter, source => source.MapFrom(p => p.IdTipoCaracter))
                    .ForMember(dest => dest.cod_tipocaracter, source => source.MapFrom(p => p.CodTipoCaracter))
                    .ForMember(dest => dest.nom_tipocaracter, source => source.MapFrom(p => p.NomTipoCaracter))
                    .ForMember(dest => dest.disponibilidad_tipocaracter, source => source.MapFrom(p => p.DisponibilidadTipoCaracter))
                    .ForMember(dest => dest.muestracargo_tipocaracter, source => source.MapFrom(p => p.MuestraCargoTipoCaracter));

                cfg.CreateMap<Encomienda_Firmantes_PersonasFisicas, EncomiendaFirmantesPersonasFisicasDTO>()                 
                        .ForMember(dest => dest.IdFirmantePf, source => source.MapFrom(p => p.id_firmante_pf))
                        .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                        .ForMember(dest => dest.IdPersonaFisica, source => source.MapFrom(p => p.id_personafisica))
                        .ForMember(dest => dest.IdTipodocPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                        .ForMember(dest => dest.NroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                        .ForMember(dest => dest.IdTipoCaracter, source => source.MapFrom(p => p.id_tipocaracter));

                cfg.CreateMap<EncomiendaFirmantesPersonasFisicasDTO, Encomienda_Firmantes_PersonasFisicas>()   
                        .ForMember(dest => dest.id_firmante_pf, source => source.MapFrom(p => p.IdFirmantePf))
                        .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                        .ForMember(dest => dest.id_personafisica, source => source.MapFrom(p => p.IdPersonaFisica))
                        .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipodocPersonal))
                        .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NroDocumento))
                        .ForMember(dest => dest.id_tipocaracter, source => source.MapFrom(p => p.IdTipoCaracter));                
            });
            mapperBase = config.CreateMapper();
        }

        public IEnumerable<EncomiendaTitularesPersonasFisicasDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Titulares_PersonasFisicas>, IEnumerable<EncomiendaTitularesPersonasFisicasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public EncomiendaTitularesPersonasFisicasDTO Single(int IdPersonaFisica)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdPersonaFisica);
                var entityDto = mapperBase.Map<Encomienda_Titulares_PersonasFisicas, EncomiendaTitularesPersonasFisicasDTO>(entity);

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
        public IEnumerable<EncomiendaTitularesPersonasFisicasDTO> GetByFKIdEncomienda(int IdEncomienda)
        {
            try
            {

                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByFKIdEncomienda(IdEncomienda);
                var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Titulares_PersonasFisicas>, IEnumerable<EncomiendaTitularesPersonasFisicasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            { throw ex; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTipoDocPersonal"></param>
        /// <returns></returns>	
        public IEnumerable<EncomiendaTitularesPersonasFisicasDTO> GetByFKIdTipoDocPersonal(int IdTipoDocPersonal)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdTipoDocPersonal(IdTipoDocPersonal);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Titulares_PersonasFisicas>, IEnumerable<EncomiendaTitularesPersonasFisicasDTO>>(elements);
            return elementsDto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTipoiibb"></param>
        /// <returns></returns>	
        public IEnumerable<EncomiendaTitularesPersonasFisicasDTO> GetByFKIdTipoiibb(int IdTipoiibb)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdTipoiibb(IdTipoiibb);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Titulares_PersonasFisicas>, IEnumerable<EncomiendaTitularesPersonasFisicasDTO>>(elements);
            return elementsDto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdLocalidad"></param>
        /// <returns></returns>	
        public IEnumerable<EncomiendaTitularesPersonasFisicasDTO> GetByFKIdLocalidad(int IdLocalidad)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdLocalidad(IdLocalidad);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Titulares_PersonasFisicas>, IEnumerable<EncomiendaTitularesPersonasFisicasDTO>>(elements);
            return elementsDto;
        }
        public IEnumerable<EncomiendaTitularesPersonasFisicasDTO> GetByIdEncomiendaCuitIdPersonaFisica(int id_encomienda, string Cuit, int IdPersonaFisica)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByIdEncomiendaCuitIdPersonaFisica(id_encomienda, Cuit, IdPersonaFisica);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Titulares_PersonasFisicas>, IEnumerable<EncomiendaTitularesPersonasFisicasDTO>>(elements);
            return elementsDto;
        }

        public IEnumerable<EncomiendaTitularesPersonasFisicasDTO> GetByIdEncomiendaIdPersonaFisica(int id_encomienda, int IdPersonaFisica)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByIdEncomiendaIdPersonaFisica(id_encomienda, IdPersonaFisica);
            var elementsDto = mapperBase.Map<IEnumerable<Encomienda_Titulares_PersonasFisicas>, IEnumerable<EncomiendaTitularesPersonasFisicasDTO>>(elements);
            return elementsDto;
        }

        #region Métodos de actualizacion e insert
        /// <summary>
        /// Inserta la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public bool Insert(EncomiendaTitularesPersonasFisicasDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    EncomiendaBL encomiendaBL = new EncomiendaBL();
                    var encomiendaDTO = encomiendaBL.Single(objectDto.IdEncomienda);
                    if (encomiendaDTO.IdEstado != (int)Constantes.Encomienda_Estados.Completa && encomiendaDTO.IdEstado != (int)Constantes.Encomienda_Estados.Incompleta
                        && encomiendaDTO.IdEstado != (int)Constantes.Encomienda_Estados.Confirmada)
                        throw new Exception(Errors.ENCOMIENDA_CAMBIOS);

                    repo = new EncomiendaTitularesPersonasFisicasRepository(unitOfWork);
                    repoFir = new EncomiendaFirmantesPersonasFisicasRepository(unitOfWork);

                    var elementDto = mapperBase.Map<EncomiendaTitularesPersonasFisicasDTO, Encomienda_Titulares_PersonasFisicas>(objectDto);
                    var elementDtoFirmantes = mapperBase.Map<IEnumerable<EncomiendaFirmantesPersonasFisicasDTO>, IEnumerable<Encomienda_Firmantes_PersonasFisicas>>(objectDto.EncomiendaFirmantesPersonasFisicasDTO);

                    var insertOk = repo.Insert(elementDto);
                    foreach (var forPF in elementDtoFirmantes)
                    {
                        forPF.id_personafisica = elementDto.id_personafisica;
                        repoFir.Insert(forPF);
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


        #endregion
        #region Métodos de actualizacion e insert
        /// <summary>
        /// Modifica la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public void Update(EncomiendaTitularesPersonasFisicasDTO objectDTO)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaTitularesPersonasFisicasRepository(unitOfWork);
                    var elementDTO = mapperBase.Map<EncomiendaTitularesPersonasFisicasDTO, Encomienda_Titulares_PersonasFisicas>(objectDTO);
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
        public void Delete(EncomiendaTitularesPersonasFisicasDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaTitularesPersonasFisicasRepository(unitOfWork);
                    repoFir = new EncomiendaFirmantesPersonasFisicasRepository(unitOfWork);

                    var firmantesEntity = mapperBase.Map<IEnumerable<EncomiendaFirmantesPersonasFisicasDTO>,IEnumerable<Encomienda_Firmantes_PersonasFisicas>>(objectDto.EncomiendaFirmantesPersonasFisicasDTO);
                    repoFir.RemoveRange(firmantesEntity); 
                    
                    var titularesEntity = mapperBase.Map<EncomiendaTitularesPersonasFisicasDTO, Encomienda_Titulares_PersonasFisicas>(objectDto);

                    repo.Delete(titularesEntity);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public bool CompareEntreEncomienda(int idEncomienda1, int idEncomienda2)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaTitularesPersonasFisicasRepository(this.uowF.GetUnitOfWork());
            var compare = repo.CompareDataSaved(idEncomienda1, idEncomienda2);
            return compare;
        }
    }
}

