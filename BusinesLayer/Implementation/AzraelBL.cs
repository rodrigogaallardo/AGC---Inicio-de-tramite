using AutoMapper;
using BaseRepository;
using BaseRepository.Engine;
using Dal.UnitOfWork;
using DataAcess;
using DataAcess.EntityCustom;
using DataTransferObject;
using ExternalService;
using ExternalService.ws_interface_AGC;
using IBusinessLayer;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using UnitOfWork;

namespace BusinesLayer.Implementation
{
    /// <summary>
    /// REgenera los PDF en la base de datos 
    /// </summary>
    public class AzraelBL
    {
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
        private AzraelRepository repo = null;

        public AzraelBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TramitesDTO, TramitesEntity>().ReverseMap();

                cfg.CreateMap<TareasDTO, TareasEntity>().ReverseMap();

                cfg.CreateMap<AzraelBuscadorFileDTO, AzraelBuscadorFileEntity>().ReverseMap();

                cfg.CreateMap<AzraelBuscadorFileEntity, AzraelBuscadorFileDTO>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
            uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
            using (IUnitOfWork unitOfWork = uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
            {
                repo = new AzraelRepository(unitOfWork);
            }
        }

        public void GuardarPDFEncomienda(int id_encomienda, int id_file, string FileName, Guid UserId)
        {
            try
            {
                EncomiendaDocumentosAdjuntosBL encDocBL = new EncomiendaDocumentosAdjuntosBL();
                ExternalServiceFiles esf = new ExternalServiceFiles();
                EncomiendaDocumentosAdjuntosDTO encDocDTO;

                encDocDTO = encDocBL.GetByFKIdEncomiendaTipoSis(id_encomienda, (int)Constantes.TiposDeDocumentosSistema.ENCOMIENDA_DIGITAL).FirstOrDefault();

                if (encDocDTO != null)
                {
                    if (id_file != encDocDTO.id_file)
                        esf.deleteFile(encDocDTO.id_file);
                    encDocDTO.id_file = id_file;
                    encDocBL.Update(encDocDTO);
                }
                else
                {
                    encDocDTO = new EncomiendaDocumentosAdjuntosDTO();
                    encDocDTO.id_encomienda = id_encomienda;
                    encDocDTO.id_tipodocsis = (int)Constantes.TiposDeDocumentosSistema.ENCOMIENDA_DIGITAL;
                    encDocDTO.id_tdocreq = 0;
                    encDocDTO.generadoxSistema = true;
                    encDocDTO.CreateDate = DateTime.Now;
                    encDocDTO.CreateUser = UserId;
                    encDocDTO.nombre_archivo = FileName;
                    encDocDTO.id_file = id_file;
                    encDocBL.Insert(encDocDTO);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardarPDFCertificado(int id_encomienda, int id_file, string FileName, Guid UserId)
        {
            try
            {
                EncomiendaDocumentosAdjuntosBL encDocBL = new EncomiendaDocumentosAdjuntosBL();
                ExternalServiceFiles esf = new ExternalServiceFiles();
                EncomiendaDocumentosAdjuntosDTO encDocDTO;

                encDocDTO = encDocBL.GetByFKIdEncomiendaTipoSis(id_encomienda, (int)Constantes.TiposDeDocumentosSistema.CERTIF_CONSEJO_HABILITACION).FirstOrDefault();

                if (encDocDTO != null)
                {
                    if (id_file != encDocDTO.id_file)
                        esf.deleteFile(encDocDTO.id_file);
                    encDocDTO.id_file = id_file;
                    encDocBL.Update(encDocDTO);
                }
                else
                {
                    encDocDTO = new EncomiendaDocumentosAdjuntosDTO();
                    encDocDTO.id_encomienda = id_encomienda;
                    encDocDTO.id_tipodocsis = (int)Constantes.TiposDeDocumentosSistema.CERTIF_CONSEJO_HABILITACION;
                    encDocDTO.id_tdocreq = 0;
                    encDocDTO.generadoxSistema = true;
                    encDocDTO.CreateDate = DateTime.Now;
                    encDocDTO.CreateUser = UserId;
                    encDocDTO.nombre_archivo = FileName;
                    encDocDTO.id_file = id_file;
                    encDocBL.Insert(encDocDTO);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardarPDFOblea(int id_solicitud, int id_file, string FileName, Guid UserId)
        {
            try
            {
                SSITDocumentosAdjuntosBL solDocBL = new SSITDocumentosAdjuntosBL();
                ExternalServiceFiles esf = new ExternalServiceFiles();
                SSITDocumentosAdjuntosDTO solDocDTO;

                solDocDTO = solDocBL.GetByFKIdSolicitudTipoDocSis(id_solicitud, (int)Constantes.TiposDeDocumentosSistema.OBLEA_SOLICITUD).FirstOrDefault();

                if (solDocDTO != null)
                {
                    if (id_file != solDocDTO.id_file)
                        esf.deleteFile(solDocDTO.id_file);
                    solDocDTO.id_file = id_file;
                    solDocBL.Update(solDocDTO);
                }
                else
                {
                    solDocDTO = new SSITDocumentosAdjuntosDTO();
                    solDocDTO.id_solicitud = id_solicitud;
                    solDocDTO.id_tipodocsis = (int)Constantes.TiposDeDocumentosSistema.OBLEA_SOLICITUD;
                    solDocDTO.id_tdocreq = 0;
                    solDocDTO.generadoxSistema = true;
                    solDocDTO.CreateDate = DateTime.Now;
                    solDocDTO.CreateUser = UserId;
                    solDocDTO.nombre_archivo = FileName;
                    solDocDTO.id_file = id_file;
                    solDocBL.Insert(solDocDTO, true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetNewOblea(int id_solicitud, Guid userid, int id_file, string FileName)
        {
            SSITDocumentosAdjuntosBL solDocBL = new SSITDocumentosAdjuntosBL();
            ExternalServiceFiles esf = new ExternalServiceFiles();
            SSITDocumentosAdjuntosDTO solDocDTO;

            solDocDTO = solDocBL.GetByFKIdSolicitudTipoDocSis(id_solicitud, (int)Constantes.TiposDeDocumentosSistema.OBLEA_SOLICITUD).FirstOrDefault();

            if (solDocDTO != null)
            {
                solDocDTO = new SSITDocumentosAdjuntosDTO();
                solDocDTO.id_solicitud = id_solicitud;
                solDocDTO.id_tipodocsis = (int)Constantes.TiposDeDocumentosSistema.OBLEA_SOLICITUD;
                solDocDTO.id_tdocreq = 0;
                solDocDTO.generadoxSistema = true;
                solDocDTO.CreateDate = DateTime.Now;
                solDocDTO.CreateUser = userid;
                solDocDTO.nombre_archivo = FileName;
                solDocDTO.id_file = id_file;
                solDocBL.Insert(solDocDTO, true);
            }

        }

        public void SetNewObleaTF(int id_solicitud, Guid userid, int id_file, string FileName)
        {
            TransferenciasDocumentosAdjuntosBL transfDocBL = new TransferenciasDocumentosAdjuntosBL();
            ExternalServiceFiles esf = new ExternalServiceFiles();
            TransferenciasDocumentosAdjuntosDTO transfDTO;

            transfDTO = transfDocBL.GetByFKIdSolicitudTipoDocSis(id_solicitud, (int)Constantes.TiposDeDocumentosSistema.OBLEA_SOLICITUD).FirstOrDefault();

            if (transfDTO != null)
            {
                transfDTO = new TransferenciasDocumentosAdjuntosDTO();
                transfDTO.IdSolicitud = id_solicitud;
                transfDTO.IdTipoDocsis = (int)Constantes.TiposDeDocumentosSistema.OBLEA_SOLICITUD;
                transfDTO.IdTipoDocumentoRequerido = 0;
                transfDTO.GeneradoxSistema = true;
                transfDTO.CreateDate = DateTime.Now;
                transfDTO.CreateUser = userid;
                transfDTO.NombreArchivo = FileName;
                transfDTO.IdFile = id_file;
                transfDocBL.Insert(transfDTO, true);
            }

        }


        public void GuardarPDFSolicitud(int id_solicitud, int id_file, string FileName, Guid UserId)
        {
            try
            {
                SSITDocumentosAdjuntosBL solDocBL = new SSITDocumentosAdjuntosBL();
                ExternalServiceFiles esf = new ExternalServiceFiles();
                SSITDocumentosAdjuntosDTO solDocDTO;

                solDocDTO = solDocBL.GetByFKIdSolicitudTipoDocSis(id_solicitud, (int)Constantes.TiposDeDocumentosSistema.SOLICITUD_HABILITACION).FirstOrDefault();

                if (solDocDTO != null)
                {
                    if (id_file != solDocDTO.id_file)
                        esf.deleteFile(solDocDTO.id_file);
                    solDocDTO.id_file = id_file;
                    solDocBL.Update(solDocDTO);
                }
                else
                {
                    solDocDTO = new SSITDocumentosAdjuntosDTO();
                    solDocDTO.id_solicitud = id_solicitud;
                    solDocDTO.id_tipodocsis = (int)Constantes.TiposDeDocumentosSistema.SOLICITUD_HABILITACION;
                    solDocDTO.id_tdocreq = 0;
                    solDocDTO.generadoxSistema = true;
                    solDocDTO.CreateDate = DateTime.Now;
                    solDocDTO.CreateUser = UserId;
                    solDocDTO.nombre_archivo = FileName;
                    solDocDTO.id_file = id_file;
                    solDocBL.Insert(solDocDTO, true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardarPDFInformeCpadron(int id_cpadron, int id_file, string FileName, Guid UserId)
        {
            try
            {
                ConsultaPadronDocumentosAdjuntosBL cpDocBL = new ConsultaPadronDocumentosAdjuntosBL();
                ExternalServiceFiles esf = new ExternalServiceFiles();
                ConsultaPadronDocumentosAdjuntosDTO cpDocDTO;

                cpDocDTO = cpDocBL.GetByFKIdConsultaPadronTipoSis(id_cpadron, (int)Constantes.TiposDeDocumentosSistema.INFORMES_CPADRON).FirstOrDefault();

                if (cpDocDTO != null)
                {
                    if (id_file != cpDocDTO.IdFile)
                        esf.deleteFile(cpDocDTO.IdFile);
                    cpDocDTO.IdFile = id_file;
                    cpDocBL.Update(cpDocDTO);
                }
                else
                {
                    cpDocDTO = new ConsultaPadronDocumentosAdjuntosDTO();
                    cpDocDTO.IdConsultaPadron = id_cpadron;
                    cpDocDTO.IdTipoDocumentoSistema = (int)Constantes.TiposDeDocumentosSistema.INFORMES_CPADRON;
                    cpDocDTO.IdTipodocumentoRequerido = 0;
                    cpDocDTO.GeneradoxSistema = true;
                    cpDocDTO.CreateDate = DateTime.Now;
                    cpDocDTO.CreateUser = UserId;
                    cpDocDTO.NombreArchivo = FileName;
                    cpDocDTO.IdFile = id_file;
                    cpDocBL.Insert(cpDocDTO);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardarPDFSolicitudEconomica(int id_solicitud, int id_file, string fileName, Guid userId)
        {
            try
            {
                SSITDocumentosAdjuntosBL solDocBL = new SSITDocumentosAdjuntosBL();
                ExternalServiceFiles esf = new ExternalServiceFiles();
                SSITDocumentosAdjuntosDTO solDocDTO;

                solDocDTO = solDocBL.GetByFKIdSolicitudTipoDocSis(id_solicitud, (int)Constantes.TiposDeDocumentosSistema.DECLARACION_RESPONSABLE).FirstOrDefault();

                if (solDocDTO != null)
                {
                    if (id_file != solDocDTO.id_file)
                        esf.deleteFile(solDocDTO.id_file);
                    solDocDTO.id_file = id_file;
                    solDocBL.Update(solDocDTO);
                }
                else
                {
                    solDocDTO = new SSITDocumentosAdjuntosDTO();
                    solDocDTO.id_solicitud = id_solicitud;
                    solDocDTO.id_tipodocsis = (int)Constantes.TiposDeDocumentosSistema.DECLARACION_RESPONSABLE;
                    solDocDTO.id_tdocreq = 0;
                    solDocDTO.generadoxSistema = true;
                    solDocDTO.CreateDate = DateTime.Now;
                    solDocDTO.CreateUser = userId;
                    solDocDTO.nombre_archivo = fileName;
                    solDocDTO.id_file = id_file;
                    solDocBL.Insert(solDocDTO, true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardarPDFManifiestoTransmision(int id_solicitud, int id_file, string fileName, Guid userId)
        {
            try
            {
                TransferenciasDocumentosAdjuntosBL solDocBL = new TransferenciasDocumentosAdjuntosBL();
                ExternalServiceFiles esf = new ExternalServiceFiles();
                TransferenciasDocumentosAdjuntosDTO  solDocDTO;

                solDocDTO = solDocBL.GetByFKIdSolicitudTipoDocSis(id_solicitud, (int)Constantes.TiposDeDocumentosSistema.MANIFIESTO_TRANSMISION).FirstOrDefault();

                if (solDocDTO != null)
                {
                    if (id_file != solDocDTO.IdFile)
                        esf.deleteFile(solDocDTO.IdFile);
                    solDocDTO.IdFile = id_file;
                    solDocBL.Update(solDocDTO);
                }
                else
                {
                    solDocDTO = new TransferenciasDocumentosAdjuntosDTO();//new SSITDocumentosAdjuntosDTO();
                    solDocDTO.IdSolicitud = id_solicitud;
                    solDocDTO.IdTipoDocsis = (int)Constantes.TiposDeDocumentosSistema.MANIFIESTO_TRANSMISION;
                    solDocDTO.IdTipoDocumentoRequerido = 0;
                    solDocDTO.GeneradoxSistema = true;
                    solDocDTO.CreateDate = DateTime.Now;
                    solDocDTO.CreateUser = userId;
                    solDocDTO.NombreArchivo = fileName;
                    solDocDTO.IdFile = id_file;
                    solDocBL.Insert(solDocDTO, true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GuardarPDFCertificadoHabilitacion(int id_solicitud, int id_file, string FileName, Guid UserId)
        {
            try
            {
                SSITDocumentosAdjuntosBL solDocBL = new SSITDocumentosAdjuntosBL();
                ExternalServiceFiles esf = new ExternalServiceFiles();
                SSITDocumentosAdjuntosDTO solDocDTO;

                solDocDTO = solDocBL.GetByFKIdSolicitudTipoDocSis(id_solicitud, (int)Constantes.TiposDeDocumentosSistema.CERTIFICADO_HABILITACION).FirstOrDefault();

                if (solDocDTO != null)
                {
                    if (id_file != solDocDTO.id_file)
                        esf.deleteFile(solDocDTO.id_file);
                    solDocDTO.id_file = id_file;
                    solDocBL.Update(solDocDTO);
                }
                else
                {
                    solDocDTO = new SSITDocumentosAdjuntosDTO();
                    solDocDTO.id_solicitud = id_solicitud;
                    solDocDTO.id_tipodocsis = (int)Constantes.TiposDeDocumentosSistema.CERTIFICADO_HABILITACION;
                    solDocDTO.id_tdocreq = 0;
                    solDocDTO.generadoxSistema = true;
                    solDocDTO.CreateDate = DateTime.Now;
                    solDocDTO.CreateUser = UserId;
                    solDocDTO.nombre_archivo = FileName;
                    solDocDTO.id_file = id_file;
                    solDocBL.Insert(solDocDTO, true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TramitesDTO> GetTramitesSGI(int id_tramite)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new AzraelRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetTramitesSGI(id_tramite);
            var elementsDto = mapperBase.Map<IEnumerable<TramitesEntity>, IEnumerable<TramitesDTO>>(elements);
            return elementsDto;
        }

        public IEnumerable<TareasDTO> GetTareasSGI(int id_tramite, int id_tipotramite)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new AzraelRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetTareasSGI(id_tramite, id_tipotramite);
            var elementsDto = mapperBase.Map<IEnumerable<TareasEntity>, IEnumerable<TareasDTO>>(elements);
            return elementsDto;
        }

        public IEnumerable<AzraelBuscadorFileDTO> GetTramitesFiles(int IdBuscador)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new AzraelRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetTramitesFiles(IdBuscador);
            var elementsDto = mapperBase.Map<IEnumerable<AzraelBuscadorFileEntity>, IEnumerable<AzraelBuscadorFileDTO>>(elements);
            return elementsDto;

        }
        public string GetUserNameByGuid(Guid UserID )
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new AzraelRepository(this.uowF.GetUnitOfWork());
            return repo.GetUserNameByGuid(UserID);
        }
    }
}
