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
    public class TransferenciasTitularesSolicitudPersonasJuridicasBL : ITransferenciasTitularesSolicitudPersonasJuridicasBL<TransferenciasTitularesSolicitudPersonasJuridicasDTO>
    {
        private TransferenciasTitularesSolicitudPersonasJuridicasRepository repo = null;
        private TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasRepository repoPersonaFisicaJuridica = null;
        private IUnitOfWorkFactory uowF = null;

        IMapper mapperBase;
        IMapper mapperBasePersonaFisicaJuridica;
        IMapper mapperFirPJ;

        public TransferenciasTitularesSolicitudPersonasJuridicasBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TransferenciasTitularesSolicitudPersonasJuridicasDTO, Transf_Titulares_Solicitud_PersonasJuridicas>().ReverseMap()
                    .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdTipoSociedad, source => source.MapFrom(p => p.Id_TipoSociedad))
                    .ForMember(dest => dest.RazonSocial, source => source.MapFrom(p => p.Razon_Social))
                    .ForMember(dest => dest.IdTipoiibb, source => source.MapFrom(p => p.id_tipoiibb))
                    .ForMember(dest => dest.Numeroiibb, source => source.MapFrom(p => p.Nro_IIBB))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NroPuerta))
                    .ForMember(dest => dest.IdLocalidad, source => source.MapFrom(p => p.id_localidad))
                    .ForMember(dest => dest.CodigoPostal, source => source.MapFrom(p => p.Codigo_Postal));

                //cfg.CreateMap<Transf_Titulares_Solicitud_PersonasJuridicas, TransferenciasTitularesSolicitudPersonasJuridicasDTO>().ReverseMap();
                //.ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))
                //.ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                //.ForMember(dest => dest.Id_TipoSociedad, source => source.MapFrom(p => p.IdTipoSociedad))
                //.ForMember(dest => dest.Razon_Social, source => source.MapFrom(p => p.RazonSocial))
                //.ForMember(dest => dest.id_tipoiibb, source => source.MapFrom(p => p.IdTipoiibb))
                //.ForMember(dest => dest.Nro_IIBB, source => source.MapFrom(p => p.Numeroiibb))
                //.ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NumeroPuerta))
                //.ForMember(dest => dest.id_localidad, source => source.MapFrom(p => p.IdLocalidad))
                //.ForMember(dest => dest.Codigo_Postal, source => source.MapFrom(p => p.CodigoPostal)) ;

            });
            mapperBase = config.CreateMapper();

            var configPJ = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasDTO, Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas>().ReverseMap()
                    .ForMember(dest => dest.IdTitularPersonaJuridica, source => source.MapFrom(p => p.id_titular_pj))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                    .ForMember(dest => dest.IdTipoDocumentoPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.FirmanteMismaPersona, source => source.MapFrom(p => p.firmante_misma_persona))
                    .ForMember(dest => dest.NumeroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.id_firmante_pj, source => source.MapFrom(p => p.id_firmante_pj));

                cfg.CreateMap<Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas, TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasDTO>().ReverseMap()
                    .ForMember(dest => dest.id_titular_pj, source => source.MapFrom(p => p.IdTitularPersonaJuridica))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocumentoPersonal))
                    .ForMember(dest => dest.firmante_misma_persona, source => source.MapFrom(p => p.FirmanteMismaPersona))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NumeroDocumento))
                    .ForMember(dest => dest.id_firmante_pj, source => source.MapFrom(p => p.id_firmante_pj));
            });
            mapperBasePersonaFisicaJuridica = configPJ.CreateMapper();

            var configFirPJ = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TransferenciasFirmantesSolicitudPersonasJuridicasDTO, Transf_Firmantes_Solicitud_PersonasJuridicas>();
                cfg.CreateMap<Transf_Firmantes_Solicitud_PersonasJuridicas, TransferenciasFirmantesSolicitudPersonasJuridicasDTO>();

            });
            mapperFirPJ = configFirPJ.CreateMapper();
        }

        public IEnumerable<TransferenciasTitularesSolicitudPersonasJuridicasDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasTitularesSolicitudPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas>, IEnumerable<TransferenciasTitularesSolicitudPersonasJuridicasDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TransferenciasTitularesSolicitudPersonasJuridicasDTO> GetByIdSolicitudIdPersonaJuridica(int IdSolicitud, int IdPersonaJuridica)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasTitularesSolicitudPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.GetByIdSolicitudIdPersonaJuridica(IdSolicitud, IdPersonaJuridica);
                var entityDto = mapperBase.Map<IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas>, IEnumerable<TransferenciasTitularesSolicitudPersonasJuridicasDTO>>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TransferenciasTitularesSolicitudPersonasJuridicasDTO Single(int IdPersonaJuridica)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasTitularesSolicitudPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdPersonaJuridica);
                var entityDto = mapperBase.Map<Transf_Titulares_Solicitud_PersonasJuridicas, TransferenciasTitularesSolicitudPersonasJuridicasDTO>(entity);

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
        public IEnumerable<TransferenciasTitularesSolicitudPersonasJuridicasDTO> GetByFKIdSolicitud(int IdSolicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasTitularesSolicitudPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdSolicitud(IdSolicitud);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas>, IEnumerable<TransferenciasTitularesSolicitudPersonasJuridicasDTO>>(elements);
            return elementsDto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTipoSociedad"></param>
        /// <returns></returns>	
        public IEnumerable<TransferenciasTitularesSolicitudPersonasJuridicasDTO> GetByFKIdTipoSociedad(int IdTipoSociedad)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasTitularesSolicitudPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdTipoSociedad(IdTipoSociedad);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas>, IEnumerable<TransferenciasTitularesSolicitudPersonasJuridicasDTO>>(elements);
            return elementsDto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTipoiibb"></param>
        /// <returns></returns>	
        public IEnumerable<TransferenciasTitularesSolicitudPersonasJuridicasDTO> GetByFKIdTipoiibb(int IdTipoiibb)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasTitularesSolicitudPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdTipoiibb(IdTipoiibb);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas>, IEnumerable<TransferenciasTitularesSolicitudPersonasJuridicasDTO>>(elements);
            return elementsDto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdLocalidad"></param>
        /// <returns></returns>	
        public IEnumerable<TransferenciasTitularesSolicitudPersonasJuridicasDTO> GetByFKIdLocalidad(int IdLocalidad)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciasTitularesSolicitudPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdLocalidad(IdLocalidad);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas>, IEnumerable<TransferenciasTitularesSolicitudPersonasJuridicasDTO>>(elements);
            return elementsDto;
        }
        #region Métodos de actualizacion e insert
        /// <summary>
        /// Inserta la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public bool Insert(TransferenciasTitularesSolicitudPersonasJuridicasDTO objectDto)
        {
            try
            {
                TransferenciasFirmantesSolicitudPersonasJuridicasBL firBL = new TransferenciasFirmantesSolicitudPersonasJuridicasBL();
                TransferenciasSolicitudesBL transferenciaSolicitudesBL = new TransferenciasSolicitudesBL();
                var transferenciaDTO = transferenciaSolicitudesBL.Single(objectDto.IdSolicitud);

                if (transferenciaDTO.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.COMP &&
                    transferenciaDTO.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM &&
                    transferenciaDTO.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.OBSERVADO &&
                    transferenciaDTO.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.PING)
                    throw new Exception(Errors.SSIT_TRANSFERENCIAS_NO_ADMITE_CAMBIOS);

                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new TransferenciasTitularesSolicitudPersonasJuridicasRepository(unitOfWork);
                    TransferenciasFirmantesSolicitudPersonasJuridicasRepository repoFirPJ = new TransferenciasFirmantesSolicitudPersonasJuridicasRepository(unitOfWork);
                    repoPersonaFisicaJuridica = new TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasRepository(unitOfWork);

                    //Elimino todos
                    if (objectDto.IdPersonaJuridica > 0)
                    {
                        var deleteObjectfirPFPJDTO = repoPersonaFisicaJuridica.GetByIdSolicitudIdPersonaJuridica(objectDto.IdSolicitud, objectDto.IdPersonaJuridica);
                        foreach (var item in deleteObjectfirPFPJDTO)
                        {
                            repoPersonaFisicaJuridica.Delete(item);
                        }

                        var deleteObjectfirDTO = firBL.GetByFKIdSolicitudIdPersonaJuridica(objectDto.IdSolicitud, objectDto.IdPersonaJuridica);
                        foreach (var item in deleteObjectfirDTO)
                        {
                            firBL.Delete(item);
                        }

                        var deleteObject = new TransferenciasTitularesSolicitudPersonasJuridicasDTO()
                        {
                            IdPersonaJuridica = objectDto.IdPersonaJuridica
                        };
                        var deleteObjectDTO = mapperBase.Map<TransferenciasTitularesSolicitudPersonasJuridicasDTO, Transf_Titulares_Solicitud_PersonasJuridicas>(deleteObject);
                        repo.Delete(deleteObjectDTO);
                    }

                    var elementDto = mapperBase.Map<TransferenciasTitularesSolicitudPersonasJuridicasDTO, Transf_Titulares_Solicitud_PersonasJuridicas>(objectDto);

                    repo.Insert(elementDto);
                    ///********************************************
                    if (elementDto.Id_TipoSociedad == (int)Constantes.TipoSociedad.Sociedad_Hecho ||
                        elementDto.Id_TipoSociedad == (int)Constantes.TipoSociedad.Sociedad_no_constituidas_regularmente)
                    {
                        foreach (var itemFirPj in objectDto.firmantesSH)
                        {
                            TransferenciasFirmantesSolicitudPersonasJuridicasDTO encFirPerJur = new TransferenciasFirmantesSolicitudPersonasJuridicasDTO();
                            encFirPerJur.id_solicitud = elementDto.id_solicitud;
                            encFirPerJur.id_personajuridica = elementDto.id_personajuridica;
                            encFirPerJur.Apellido = itemFirPj.Apellidos;
                            encFirPerJur.Nombres = itemFirPj.Nombres;
                            encFirPerJur.id_tipodoc_personal = itemFirPj.id_tipodoc_personal;
                            encFirPerJur.Nro_Documento = itemFirPj.NroDoc;
                            encFirPerJur.Cuit = itemFirPj.Cuit;
                            encFirPerJur.Email = itemFirPj.email;
                            encFirPerJur.id_tipocaracter = itemFirPj.id_tipocaracter;
                            encFirPerJur.cargo_firmante_pj = itemFirPj.cargo_firmante;
                            var elementDtoFirPerJurInsert = mapperFirPJ.Map<TransferenciasFirmantesSolicitudPersonasJuridicasDTO, Transf_Firmantes_Solicitud_PersonasJuridicas>(encFirPerJur);
                            int id_firmante_pj = repoFirPJ.insert(elementDtoFirPerJurInsert);

                            foreach (var itemTitPjSH in objectDto.titularesSH)
                            {
                                if ((Guid)itemTitPjSH.RowId == (Guid)itemFirPj.rowid_titular)
                                {
                                    TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasDTO encoTitPerJurPerFis = new TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasDTO();
                                    encoTitPerJurPerFis.IdSolicitud = elementDto.id_solicitud;
                                    encoTitPerJurPerFis.IdPersonaJuridica = elementDto.id_personajuridica;
                                    encoTitPerJurPerFis.id_firmante_pj = id_firmante_pj;
                                    encoTitPerJurPerFis.Apellido = itemTitPjSH.Apellidos;
                                    encoTitPerJurPerFis.Nombres = itemTitPjSH.Nombres;
                                    encoTitPerJurPerFis.IdTipoDocumentoPersonal = itemTitPjSH.IdTipoDocPersonal;
                                    encoTitPerJurPerFis.NumeroDocumento = itemTitPjSH.NroDoc;
                                    encoTitPerJurPerFis.Email = itemTitPjSH.Email;
                                    encoTitPerJurPerFis.FirmanteMismaPersona = itemFirPj.misma_persona.HasValue ? itemFirPj.misma_persona.Value : false;
                                    var elementDtoTitPerJurInsert = mapperBasePersonaFisicaJuridica.Map<TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasDTO, Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas>(encoTitPerJurPerFis);
                                    repoPersonaFisicaJuridica.Insert(elementDtoTitPerJurInsert);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (var item in objectDto.encFirDTO)
                        {
                            TransferenciasFirmantesSolicitudPersonasJuridicasDTO solFirPJInsertDTO = new TransferenciasFirmantesSolicitudPersonasJuridicasDTO();
                            solFirPJInsertDTO.id_solicitud = elementDto.id_solicitud;
                            solFirPJInsertDTO.id_personajuridica = elementDto.id_personajuridica;
                            solFirPJInsertDTO.Apellido = item.Apellido;
                            solFirPJInsertDTO.Nombres = item.Nombres;
                            solFirPJInsertDTO.id_tipodoc_personal = item.id_tipodoc_personal;
                            solFirPJInsertDTO.Nro_Documento = item.Nro_Documento;
                            solFirPJInsertDTO.Cuit = item.Cuit;
                            solFirPJInsertDTO.Email = item.Email;
                            solFirPJInsertDTO.id_tipocaracter = item.id_tipocaracter;
                            solFirPJInsertDTO.cargo_firmante_pj = item.cargo_firmante_pj;
                            var elementDtoFirPerJurInsert = mapperFirPJ.Map<TransferenciasFirmantesSolicitudPersonasJuridicasDTO, Transf_Firmantes_Solicitud_PersonasJuridicas>(solFirPJInsertDTO);
                            repoFirPJ.Insert(elementDtoFirPerJurInsert);
                        }
                    }

                    ///******************************************

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
        public void Update(TransferenciasTitularesSolicitudPersonasJuridicasDTO objectDTO)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new TransferenciasTitularesSolicitudPersonasJuridicasRepository(unitOfWork);
                    var elementDTO = mapperBase.Map<TransferenciasTitularesSolicitudPersonasJuridicasDTO, Transf_Titulares_Solicitud_PersonasJuridicas>(objectDTO);
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
        public void Delete(TransferenciasTitularesSolicitudPersonasJuridicasDTO objectDto)
        {
            try
            {
                TransferenciasSolicitudesBL transferenciaSolicitudesBL = new TransferenciasSolicitudesBL();
                var transferenciaDTO = transferenciaSolicitudesBL.Single(objectDto.IdSolicitud);

                if (transferenciaDTO.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && transferenciaDTO.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM && transferenciaDTO.IdEstado != (int)Constantes.TipoEstadoSolicitudEnum.PING)
                    throw new Exception(Errors.SSIT_TRANSFERENCIAS_NO_ADMITE_CAMBIOS);

                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new TransferenciasTitularesSolicitudPersonasJuridicasRepository(unitOfWork);
                    var elementDto = mapperBase.Map<TransferenciasTitularesSolicitudPersonasJuridicasDTO, Transf_Titulares_Solicitud_PersonasJuridicas>(objectDto);

                    repoPersonaFisicaJuridica = new TransferenciasTitularesSolicitudPersonasJuridicasPersonasFisicasRepository(unitOfWork);

                    var personafisicajuridica = repoPersonaFisicaJuridica.GetByFKIdSolicitud(objectDto.IdSolicitud);

                    foreach (var item in personafisicajuridica)
                        repoPersonaFisicaJuridica.Delete(item);

                    repo.Delete(elementDto);

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
                    repo = new TransferenciasTitularesSolicitudPersonasJuridicasRepository(unitOfWork);
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
                    repo = new TransferenciasTitularesSolicitudPersonasJuridicasRepository(unitOfWork);
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
                    repo = new TransferenciasTitularesSolicitudPersonasJuridicasRepository(unitOfWork);
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
                    repo = new TransferenciasTitularesSolicitudPersonasJuridicasRepository(unitOfWork);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <param name="Cuit"></param>
        /// <param name="IdPersonaFisica"></param>
        /// <returns></returns>
        public IEnumerable<TransferenciasTitularesSolicitudPersonasJuridicasDTO> GetByIdSolicitudCuitIdPersonaJuridica(int IdSolicitud, string Cuit, int IdPersonaFisica)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasTitularesSolicitudPersonasJuridicasRepository(this.uowF.GetUnitOfWork());
                var entity = repo.GetByIdSolicitudCuitIdPersonaJuridica(IdSolicitud, Cuit, IdPersonaFisica);
                var entityDto = mapperBase.Map<IEnumerable<Transf_Titulares_Solicitud_PersonasJuridicas>, IEnumerable<TransferenciasTitularesSolicitudPersonasJuridicasDTO>>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

