using AutoMapper;
using BaseRepository;
using Dal.UnitOfWork;
using DataAcess;
using DataTransferObject;
using IBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitOfWork;

namespace BusinesLayer.Implementation
{
    public class EncomiendaDocumentosAdjuntosBL : IEncomiendaDocumentosAdjuntosBL<EncomiendaDocumentosAdjuntosDTO>
    {
        private EncomiendaDocumentosAdjuntosRepository repo = null;
        private SSITSolicitudesRepository repoSol = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public EncomiendaDocumentosAdjuntosBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                #region "Encomienda_DocumentosAdjuntos"
                cfg.CreateMap<Encomienda_DocumentosAdjuntos, EncomiendaDocumentosAdjuntosDTO>()
                    .ForMember(dest => dest.TiposDeDocumentosRequeridosDTO, source => source.MapFrom(p => p.TiposDeDocumentosRequeridos))
                    .ForMember(dest => dest.TiposDeDocumentosSistemaDTO, source => source.MapFrom(p => p.TiposDeDocumentosSistema));

                cfg.CreateMap<EncomiendaDocumentosAdjuntosDTO, Encomienda_DocumentosAdjuntos>()
                    .ForMember(dest => dest.TiposDeDocumentosRequeridos, source => source.MapFrom(p => p.TiposDeDocumentosRequeridosDTO))
                    .ForMember(dest => dest.TiposDeDocumentosSistema, source => source.MapFrom(p => p.TiposDeDocumentosSistemaDTO));
                #endregion
                #region "TiposDeDocumentosRequeridos"
                cfg.CreateMap<TiposDeDocumentosRequeridosDTO, TiposDeDocumentosRequeridos>();

                cfg.CreateMap<TiposDeDocumentosRequeridos, TiposDeDocumentosRequeridosDTO>();
                #endregion
                #region "TiposDeDocumentosSistema"
                cfg.CreateMap<TiposDeDocumentosSistemaDTO, TiposDeDocumentosSistema>();

                cfg.CreateMap<TiposDeDocumentosSistema, TiposDeDocumentosSistemaDTO>();
                #endregion
            });
            mapperBase = config.CreateMapper();
        }

        public IEnumerable<EncomiendaDocumentosAdjuntosDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Encomienda_DocumentosAdjuntos>, IEnumerable<EncomiendaDocumentosAdjuntosDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public EncomiendaDocumentosAdjuntosDTO Single(int id_docadjunto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(id_docadjunto);
                var entityDto = mapperBase.Map<Encomienda_DocumentosAdjuntos, EncomiendaDocumentosAdjuntosDTO>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Métodos de inserccion
        /// <summary>
        /// Inserta la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public bool Insert(EncomiendaDocumentosAdjuntosDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaDocumentosAdjuntosRepository(unitOfWork);
                    var element = mapperBase.Map<EncomiendaDocumentosAdjuntosDTO, Encomienda_DocumentosAdjuntos>(objectDto);
                    var insertOk = repo.Insert(element);
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
        #region Métodos de actualizacion
        /// <summary>
        /// Modifica la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public void Update(EncomiendaDocumentosAdjuntosDTO objectDTO)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaDocumentosAdjuntosRepository(unitOfWork);
                    var elementDTO = mapperBase.Map<EncomiendaDocumentosAdjuntosDTO, Encomienda_DocumentosAdjuntos>(objectDTO);
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
        public void Delete(EncomiendaDocumentosAdjuntosDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaDocumentosAdjuntosRepository(unitOfWork);
                    var elementDto = mapperBase.Map<EncomiendaDocumentosAdjuntosDTO, Encomienda_DocumentosAdjuntos>(objectDto);
                    var insertOk = repo.Delete(elementDto);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<EncomiendaDocumentosAdjuntosDTO> GetByFKIdEncomiendaTipoSis(int IdEncomienda, string Codigo)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByFKIdEncomiendaTipoSis(IdEncomienda, Codigo);
                var elementsDTO = mapperBase.Map<IEnumerable<Encomienda_DocumentosAdjuntos>, IEnumerable<EncomiendaDocumentosAdjuntosDTO>>(elements);
                return elementsDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<EncomiendaDocumentosAdjuntosDTO> GetByFKIdEncomiendaTipoSis(int IdEncomienda, int idTipo)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByFKIdEncomiendaTipoSis(IdEncomienda, idTipo);
                var elementsDTO = mapperBase.Map<IEnumerable<Encomienda_DocumentosAdjuntos>, IEnumerable<EncomiendaDocumentosAdjuntosDTO>>(elements);
                return elementsDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        public IEnumerable<EncomiendaDocumentosAdjuntosDTO> GetByFKListIdEncomiendaTipoSis(List<int> IdsEncomienda, int idTipo)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByFKListIdEncomiendaTipoSis(IdsEncomienda, idTipo);
                var elementsDTO = mapperBase.Map<IEnumerable<Encomienda_DocumentosAdjuntos>, IEnumerable<EncomiendaDocumentosAdjuntosDTO>>(elements);
                return elementsDTO;
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

                    repo = new EncomiendaDocumentosAdjuntosRepository(unitOfWork);

                    var ListencomiendasEntity = solicitudEntity.Encomienda_SSIT_Solicitudes.Select(x => x.Encomienda);

                    foreach (var e in ListencomiendasEntity)
                    {
                        var ListAdjEntity = repo.GetByFKIdEncomienda(e.id_encomienda);

                        foreach (var adj in ListAdjEntity)
                        {
                            if (adj.fechaPresentado == null)
                            {
                                adj.fechaPresentado = DateTime.Now;
                                repo.Update(adj);
                            }
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

