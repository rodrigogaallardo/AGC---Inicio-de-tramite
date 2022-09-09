using AutoMapper;
using BaseRepository;
using Dal.UnitOfWork;
using DataAcess;
using DataTransferObject;
using ExternalService;
using IBusinessLayer;
using StaticClass;
using System;
using System.Collections.Generic;
using UnitOfWork;

namespace BusinesLayer.Implementation
{
    public class SSITDocumentosAdjuntosBL : ISSITDocumentosAdjuntosBL<SSITDocumentosAdjuntosDTO>
    {
        public static string NoEsPosibleEliminarDocuementoEstado = "";
        public static string NoEsPosibleGenerarDocuementoEstado = "";
        private SSITDocumentosAdjuntosRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public SSITDocumentosAdjuntosBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SSIT_DocumentosAdjuntos, SSITDocumentosAdjuntosDTO>()
                    .ForMember(dest => dest.TiposDeDocumentosSistemaDTO, source => source.MapFrom(p => p.TiposDeDocumentosSistema))
                    .ForMember(dest => dest.TiposDeDocumentosRequeridosDTO, source => source.MapFrom(p => p.TiposDeDocumentosRequeridos));

                cfg.CreateMap<SSITDocumentosAdjuntosDTO, SSIT_DocumentosAdjuntos>();

                #region "TiposDeDocumentosSistema"
                cfg.CreateMap<TiposDeDocumentosSistema, TiposDeDocumentosSistemaDTO>().ReverseMap();
                #endregion
                #region "TiposDeDocumentosRequeridos"
                cfg.CreateMap<TiposDeDocumentosRequeridos, TiposDeDocumentosRequeridosDTO>().ReverseMap();
                #endregion

            });
            mapperBase = config.CreateMapper();
        }

        public SSITDocumentosAdjuntosDTO Single(int Id)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(Id);
                var entityDto = mapperBase.Map<SSIT_DocumentosAdjuntos, SSITDocumentosAdjuntosDTO>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RegenerarPDFSolicitud(byte[] pdfsolicitud, int id_docadjunto, Guid userid)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITDocumentosAdjuntosRepository(unitOfWork);
                    var entity = repo.Single(id_docadjunto);

                    ExternalServiceFiles esf = new ExternalServiceFiles();
                    int id_file = esf.addFile(string.Format("Solicitud{0}.pdf", entity.id_solicitud), pdfsolicitud);

                    if (id_file > 0)
                    {
                        if (id_file != entity.id_file)
                            esf.deleteFile(entity.id_file);

                        entity.id_file = id_file;
                        entity.UpdateDate = DateTime.Now;
                        entity.UpdateUser = userid;
                        repo.Update(entity);
                        unitOfWork.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                LogError.Write(ex, ex.Message);
                throw ex;
            }
        }


        public IEnumerable<SSITDocumentosAdjuntosDTO> GetByFKIdSolicitudGenerados(int id_solicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByFKIdSolicitudGenerados(id_solicitud);
                var elementsDto = mapperBase.Map<IEnumerable<SSIT_DocumentosAdjuntos>, IEnumerable<SSITDocumentosAdjuntosDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region Métodos de insert
        /// <summary>
        /// Inserta la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public bool Insert(SSITDocumentosAdjuntosDTO objectDto, bool ForzarGenerar)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITDocumentosAdjuntosRepository(unitOfWork);
                    var elementDto = mapperBase.Map<SSITDocumentosAdjuntosDTO, SSIT_DocumentosAdjuntos>(objectDto);

                    if (!ForzarGenerar)
                    {
                        List<int> lstEstadosNoPermitidos = new List<int>();
                        lstEstadosNoPermitidos.Add((int)Constantes.TipoEstadoSolicitudEnum.ANU);
                        lstEstadosNoPermitidos.Add((int)Constantes.TipoEstadoSolicitudEnum.APRO);
                        lstEstadosNoPermitidos.Add((int)Constantes.TipoEstadoSolicitudEnum.RECH);
                        lstEstadosNoPermitidos.Add((int)Constantes.TipoEstadoSolicitudEnum.VENCIDA);
                        SSITSolicitudesBL solBL = new SSITSolicitudesBL();
                        var sol = solBL.Single(objectDto.id_solicitud);

                        if (sol != null)
                            if (lstEstadosNoPermitidos.Contains(sol.IdEstado))
                            {
                                NoEsPosibleGenerarDocuementoEstado = string.Format("No se puede generar el documento cuando el tramite se encuentra " + sol.TipoEstadoSolicitudDTO.Descripcion);
                                throw new Exception(NoEsPosibleGenerarDocuementoEstado);
                            }

                    }
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_solicitud"></param>
        /// <param name="id_tipodocsis"></param>
        /// <returns></returns>
        public IEnumerable<SSITDocumentosAdjuntosDTO> GetByFKIdSolicitudTipoDocSis(int id_solicitud, int id_tipodocsis)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByFKIdSolicitudTipoDocSis(id_solicitud, id_tipodocsis);
                var elementsDto = mapperBase.Map<IEnumerable<SSIT_DocumentosAdjuntos>, IEnumerable<SSITDocumentosAdjuntosDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public IEnumerable<SSITDocumentosAdjuntosDTO> GetByFKIdSolicitudTipoDocReq(int id_solicitud, int id_tdocreq)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByFKIdSolicitudTipoDocReq(id_solicitud, id_tdocreq);
                var elementsDto = mapperBase.Map<IEnumerable<SSIT_DocumentosAdjuntos>, IEnumerable<SSITDocumentosAdjuntosDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion
        #region Métodos de actualizacion
        /// <summary>
        /// Modifica la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public void Update(SSITDocumentosAdjuntosDTO objectDTO)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITDocumentosAdjuntosRepository(unitOfWork);
                    var elementDTO = mapperBase.Map<SSITDocumentosAdjuntosDTO, SSIT_DocumentosAdjuntos>(objectDTO);
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
        #region Métodos de eliminacion
        /// <summary>
        /// elimina la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>      
        public void Delete(SSITDocumentosAdjuntosDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    ExternalServiceFiles files = new ExternalServiceFiles();
                    List<int> lstEstadosNoPermitidos = new List<int>();
                    lstEstadosNoPermitidos.Add((int)Constantes.TipoEstadoSolicitudEnum.ANU);
                    lstEstadosNoPermitidos.Add((int)Constantes.TipoEstadoSolicitudEnum.APRO);
                    lstEstadosNoPermitidos.Add((int)Constantes.TipoEstadoSolicitudEnum.RECH);
                    SSITSolicitudesBL solBL = new SSITSolicitudesBL();
                    var sol = solBL.Single(objectDto.id_solicitud);

                    if (sol != null)
                        if (lstEstadosNoPermitidos.Contains(sol.IdEstado))
                        {
                            NoEsPosibleEliminarDocuementoEstado = string.Format("No se puede eliminar el documento cuando el tramite se encuentra " + sol.TipoEstadoSolicitudDTO.Descripcion);

                            throw new Exception(NoEsPosibleEliminarDocuementoEstado);

                        }
                    repo = new SSITDocumentosAdjuntosRepository(unitOfWork);
                    var elementDto = mapperBase.Map<SSITDocumentosAdjuntosDTO, SSIT_DocumentosAdjuntos>(objectDto);
                    int id_file = elementDto.id_file;
                    var insertOk = repo.Delete(elementDto);
                    unitOfWork.Commit();

                    if (id_file > 0)
                        files.deleteFile(elementDto.id_file);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public IEnumerable<SSITDocumentosAdjuntosDTO> GetByFKIdSolicitud(int id_solicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByFKIdSolicitud(id_solicitud);
                var elementsDto = mapperBase.Map<IEnumerable<SSIT_DocumentosAdjuntos>, IEnumerable<SSITDocumentosAdjuntosDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void SetFechaPresentado(SSIT_Solicitudes solicitudEntity)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new SSITDocumentosAdjuntosRepository(unitOfWork);

                    var ListEntity = repo.GetByFKIdSolicitud(solicitudEntity.id_solicitud);

                    foreach (var adj in ListEntity)
                    {
                        if (adj.fechaPresentado == null)
                        {
                            adj.fechaPresentado = DateTime.Now;
                            repo.Update(adj);
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

