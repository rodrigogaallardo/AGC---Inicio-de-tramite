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
    public class SSITSolicitudesTitularesPersonasJuridicasBL : ISSITSolicitudesTitularesPersonasJuridicasBL<SSITSolicitudesTitularesPersonasJuridicasDTO>
    {
        private SSITSolicitudesTitularesPersonasJuridicasRepository repo = null;
        private SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasRepository repoTitPJPF = null;
        private SSITSolicitudesFirmantesPersonasJuridicasRepository repoFirPJ = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
        IMapper mapperFirPJ;
        IMapper mapperTitPJPF;

        public SSITSolicitudesTitularesPersonasJuridicasBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SSITSolicitudesTitularesPersonasJuridicasDTO, SSIT_Solicitudes_Titulares_PersonasJuridicas>().ReverseMap()
                .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                .ForMember(dest => dest.IdTipoSociedad, source => source.MapFrom(p => p.Id_TipoSociedad))
                .ForMember(dest => dest.RazonSocial, source => source.MapFrom(p => p.Razon_Social))
                .ForMember(dest => dest.IdTipoiibb, source => source.MapFrom(p => p.id_tipoiibb))
                .ForMember(dest => dest.NroIibb, source => source.MapFrom(p => p.Nro_IIBB))
                .ForMember(dest => dest.IdLocalidad, source => source.MapFrom(p => p.id_localidad))
                .ForMember(dest => dest.CodigoPostal, source => source.MapFrom(p => p.Codigo_Postal));
            });
            var configFirPJ = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SSITSolicitudesFirmantesPersonasJuridicasDTO, SSIT_Solicitudes_Firmantes_PersonasJuridicas>().ReverseMap()
                .ForMember(dest => dest.IdFirmantePj, source => source.MapFrom(p => p.id_firmante_pj))
                .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                .ForMember(dest => dest.NroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                .ForMember(dest => dest.IdTipoCaracter, source => source.MapFrom(p => p.id_tipocaracter))
                .ForMember(dest => dest.CargoFirmantePj, source => source.MapFrom(p => p.cargo_firmante_pj));

            });
            var configTitPJPF = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO, SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas>().ReverseMap()
                    .ForMember(dest => dest.IdTitularPj, source => source.MapFrom(p => p.id_titular_pj))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                    .ForMember(dest => dest.IdTipoDocPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.NroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.IdFirmantePj, source => source.MapFrom(p => p.id_firmante_pj))
                    .ForMember(dest => dest.FirmanteMismaPersona, source => source.MapFrom(p => p.firmante_misma_persona));
            });

            mapperTitPJPF = configTitPJPF.CreateMapper();
            mapperFirPJ = configFirPJ.CreateMapper();
            mapperBase = config.CreateMapper();
        }

        public IEnumerable<SSITSolicitudesTitularesPersonasJuridicasDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas>, IEnumerable<SSITSolicitudesTitularesPersonasJuridicasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public SSITSolicitudesTitularesPersonasJuridicasDTO Single(int IdPersonaJuridica)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITSolicitudesTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdPersonaJuridica);
                var entityDto = mapperBase.Map<SSIT_Solicitudes_Titulares_PersonasJuridicas, SSITSolicitudesTitularesPersonasJuridicasDTO>(entity);

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
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>	
        public IEnumerable<SSITSolicitudesTitularesPersonasJuridicasDTO> GetByFKIdSolicitud(int IdSolicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdSolicitud(IdSolicitud);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas>, IEnumerable<SSITSolicitudesTitularesPersonasJuridicasDTO>>(elements);
            return elementsDto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTipoSociedad"></param>
        /// <returns></returns>	
        public IEnumerable<SSITSolicitudesTitularesPersonasJuridicasDTO> GetByFKIdTipoSociedad(int IdTipoSociedad)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdTipoSociedad(IdTipoSociedad);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas>, IEnumerable<SSITSolicitudesTitularesPersonasJuridicasDTO>>(elements);
            return elementsDto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTipoiibb"></param>
        /// <returns></returns>	
        public IEnumerable<SSITSolicitudesTitularesPersonasJuridicasDTO> GetByFKIdTipoiibb(int IdTipoiibb)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdTipoiibb(IdTipoiibb);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas>, IEnumerable<SSITSolicitudesTitularesPersonasJuridicasDTO>>(elements);
            return elementsDto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdLocalidad"></param>
        /// <returns></returns>	
        public IEnumerable<SSITSolicitudesTitularesPersonasJuridicasDTO> GetByFKIdLocalidad(int IdLocalidad)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdLocalidad(IdLocalidad);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas>, IEnumerable<SSITSolicitudesTitularesPersonasJuridicasDTO>>(elements);
            return elementsDto;
        }

        public IEnumerable<SSITSolicitudesTitularesPersonasJuridicasDTO> GetByIdSolicitudCuitIdPersonaJuridica(int id_solicitud, string Cuit, int IdPersonaJuridica)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByIdSolicitudCuitIdPersonaJuridica(id_solicitud, Cuit, IdPersonaJuridica);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas>, IEnumerable<SSITSolicitudesTitularesPersonasJuridicasDTO>>(elements);
            return elementsDto;
        }

        public IEnumerable<SSITSolicitudesTitularesPersonasJuridicasDTO> GetByIdSolicitudIdPersonaJuridica(int id_solicitud, int IdPersonaJuridica)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByIdSolicitudIdPersonaJuridica(id_solicitud, IdPersonaJuridica);
            var elementsDto = mapperBase.Map<IEnumerable<SSIT_Solicitudes_Titulares_PersonasJuridicas>, IEnumerable<SSITSolicitudesTitularesPersonasJuridicasDTO>>(elements);
            return elementsDto;
        }

        public string GetRazonSocial(int id_solicitud)
        {
            string result = "";

            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new SSITSolicitudesTitularesPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdSolicitud(id_solicitud);

            result = string.Join(" / ", elements.Select(s => s.Razon_Social).ToArray());

            return result;
        }


        #region Métodos de actualizacion e insert
        /// <summary>
        /// Inserta la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public bool Insert(SSITSolicitudesTitularesPersonasJuridicasDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    SSITSolicitudesBL solicitudesBL = new SSITSolicitudesBL();
                    var solicitudDTO = solicitudesBL.Single(objectDto.IdSolicitud);
                    if (solicitudDTO.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && solicitudDTO.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM
                        && solicitudDTO.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.DATOSCONF && solicitudDTO.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO)
                        throw new Exception(Errors.SSIT_SOLICITUD_NO_CAMBIOS);

                    repo = new SSITSolicitudesTitularesPersonasJuridicasRepository(unitOfWork);
                    repoTitPJPF = new SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasRepository(unitOfWork);
                    repoFirPJ = new SSITSolicitudesFirmantesPersonasJuridicasRepository(unitOfWork);


                    if (objectDto.IdPersonaJuridica > 0)
                    {
                        var titPJPF = repoTitPJPF.GetByIdSolicitudIdPersonaJuridica(objectDto.IdSolicitud, objectDto.IdPersonaJuridica);
                        foreach (var tit in titPJPF)
                            repoTitPJPF.Delete(tit);

                        var firPJ = repoFirPJ.GetByIdSolicitudIdPersonaJuridica(objectDto.IdSolicitud, objectDto.IdPersonaJuridica);
                        foreach (var fir in firPJ)
                            repoFirPJ.Delete(fir);

                        var titPJ = repo.GetByIdSolicitudIdPersonaJuridica(objectDto.IdSolicitud, objectDto.IdPersonaJuridica);
                        foreach (var tit in titPJ)
                            repo.Delete(tit);
                    }

                    var elementDto = mapperBase.Map<SSITSolicitudesTitularesPersonasJuridicasDTO, SSIT_Solicitudes_Titulares_PersonasJuridicas>(objectDto);

                    var insertOk = repo.Insert(elementDto);

                    if (elementDto.Id_TipoSociedad == (int)Constantes.TipoSociedad.Sociedad_Hecho ||
                        elementDto.Id_TipoSociedad == (int)Constantes.TipoSociedad.Sociedad_no_constituidas_regularmente)
                    {
                        foreach (var itemFirPj in objectDto.firmantesSH)
                        {
                            SSITSolicitudesFirmantesPersonasJuridicasDTO solFirPJDTO = new SSITSolicitudesFirmantesPersonasJuridicasDTO();
                            solFirPJDTO.IdSolicitud = elementDto.id_solicitud;
                            solFirPJDTO.IdPersonaJuridica = elementDto.id_personajuridica;
                            solFirPJDTO.Apellido = itemFirPj.Apellidos;
                            solFirPJDTO.Nombres = itemFirPj.Nombres;
                            solFirPJDTO.IdTipoDocPersonal = itemFirPj.id_tipodoc_personal;
                            solFirPJDTO.NroDocumento = itemFirPj.NroDoc;
                            solFirPJDTO.Email = itemFirPj.email;
                            solFirPJDTO.IdTipoCaracter = itemFirPj.id_tipocaracter;
                            solFirPJDTO.CargoFirmantePj = itemFirPj.cargo_firmante;
                            solFirPJDTO.Cuit = itemFirPj.Cuit;
                            var elementDtoFirPerJurInsert = mapperFirPJ.Map<SSITSolicitudesFirmantesPersonasJuridicasDTO, SSIT_Solicitudes_Firmantes_PersonasJuridicas>(solFirPJDTO);
                            repoFirPJ.Insert(elementDtoFirPerJurInsert);

                            foreach (var itemTitPjSH in objectDto.titularesSH)
                            {
                                if ((Guid)itemTitPjSH.RowId == (Guid)itemFirPj.rowid_titular)
                                {
                                    SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO solTitPJPFDTO = new SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO();
                                    solTitPJPFDTO.IdSolicitud = elementDto.id_solicitud;
                                    solTitPJPFDTO.IdPersonaJuridica = elementDto.id_personajuridica;
                                    solTitPJPFDTO.IdFirmantePj = elementDtoFirPerJurInsert.id_firmante_pj;
                                    solTitPJPFDTO.Apellido = itemTitPjSH.Apellidos;
                                    solTitPJPFDTO.Nombres = itemTitPjSH.Nombres;
                                    solTitPJPFDTO.IdTipoDocPersonal = itemTitPjSH.IdTipoDocPersonal;
                                    solTitPJPFDTO.NroDocumento = itemTitPjSH.NroDoc;
                                    solTitPJPFDTO.Email = itemTitPjSH.Email;
                                    solTitPJPFDTO.FirmanteMismaPersona = itemFirPj.misma_persona.Value;
                                    solTitPJPFDTO.Cuit = itemTitPjSH.Cuit;
                                    var elementDtoTitPerJurInsert = mapperTitPJPF.Map<SSITSolicitudesTitularesPersonasJuridicasPersonasFisicasDTO, SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas>(solTitPJPFDTO);
                                    repoTitPJPF.Insert(elementDtoTitPerJurInsert);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {

                        foreach (var item in objectDto.solFirDTO)
                        {
                            SSITSolicitudesFirmantesPersonasJuridicasDTO solFirPJInsertDTO = new SSITSolicitudesFirmantesPersonasJuridicasDTO();
                            solFirPJInsertDTO.IdSolicitud = elementDto.id_solicitud;
                            solFirPJInsertDTO.IdPersonaJuridica = elementDto.id_personajuridica;
                            solFirPJInsertDTO.Apellido = item.Apellido;
                            solFirPJInsertDTO.Nombres = item.Nombres;
                            solFirPJInsertDTO.IdTipoDocPersonal = item.IdTipoDocPersonal;
                            solFirPJInsertDTO.NroDocumento = item.NroDocumento;
                            solFirPJInsertDTO.Cuit = item.Cuit;
                            solFirPJInsertDTO.Email = item.Email;
                            solFirPJInsertDTO.IdTipoCaracter = item.IdTipoCaracter;
                            solFirPJInsertDTO.CargoFirmantePj = item.CargoFirmantePj;
                            var elementDtoFirPerJurInsert = mapperFirPJ.Map<SSITSolicitudesFirmantesPersonasJuridicasDTO, SSIT_Solicitudes_Firmantes_PersonasJuridicas>(solFirPJInsertDTO);
                            repoFirPJ.Insert(elementDtoFirPerJurInsert);
                        }
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

        //public bool Insert(SSITSolicitudesTitularesPersonasJuridicasDTO objectDto)
        //{
        //    try
        //    {
        //        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
        //        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
        //        {
        //            repo = new SSITSolicitudesTitularesPersonasJuridicasRepository(unitOfWork);
        //            var elementDto = mapperBase.Map<SSITSolicitudesTitularesPersonasJuridicasDTO, SSIT_Solicitudes_Titulares_PersonasJuridicas>(objectDto);
        //            var insertOk = repo.Insert(elementDto);
        //            unitOfWork.Commit();
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        #endregion
        #region Métodos de actualizacion e insert
        /// <summary>
        /// Modifica la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public void Update(SSITSolicitudesTitularesPersonasJuridicasDTO objectDTO)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesTitularesPersonasJuridicasRepository(unitOfWork);
                    var elementDTO = mapperBase.Map<SSITSolicitudesTitularesPersonasJuridicasDTO, SSIT_Solicitudes_Titulares_PersonasJuridicas>(objectDTO);
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
        public void Delete(SSITSolicitudesTitularesPersonasJuridicasDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesTitularesPersonasJuridicasRepository(unitOfWork);
                    var elementDto = mapperBase.Map<SSITSolicitudesTitularesPersonasJuridicasDTO, SSIT_Solicitudes_Titulares_PersonasJuridicas>(objectDto);
                    var insertOk = repo.Delete(elementDto);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteByFKIdSolicitud(int IdSolicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesTitularesPersonasJuridicasRepository(unitOfWork);
                    var elements = repo.GetByFKIdSolicitud(IdSolicitud);
                    foreach (var element in elements)
                        repo.Delete(element);

                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
        public void DeleteByFKIdTipoSociedad(int IdTipoSociedad)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesTitularesPersonasJuridicasRepository(unitOfWork);
                    var elements = repo.GetByFKIdTipoSociedad(IdTipoSociedad);
                    foreach (var element in elements)
                        repo.Delete(element);

                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
        public void DeleteByFKIdTipoiibb(int IdTipoiibb)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesTitularesPersonasJuridicasRepository(unitOfWork);
                    var elements = repo.GetByFKIdTipoiibb(IdTipoiibb);
                    foreach (var element in elements)
                        repo.Delete(element);

                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }
        public void DeleteByFKIdLocalidad(int IdLocalidad)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITSolicitudesTitularesPersonasJuridicasRepository(unitOfWork);
                    var elements = repo.GetByFKIdLocalidad(IdLocalidad);
                    foreach (var element in elements)
                        repo.Delete(element);

                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }



        #endregion
    }
}

