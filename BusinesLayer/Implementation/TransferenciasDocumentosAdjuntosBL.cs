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
using ExternalService;
using StaticClass;
using DataAcess.EntityCustom;

namespace BusinesLayer.Implementation
{
	public class TransferenciasDocumentosAdjuntosBL : ITransferenciasDocumentosAdjuntosBL<TransferenciasDocumentosAdjuntosDTO>
    {               
		private TransferenciasDocumentosAdjuntosRepository repo = null;
        private TiposDeDocumentosRequeridosRepository repoTipoDoc = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
         
        public TransferenciasDocumentosAdjuntosBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Transf_DocumentosAdjuntos, TransferenciasDocumentosAdjuntosDTO>()
                    .ForMember(dest => dest.Id, source => source.MapFrom(p => p.id_docadjunto))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdTipoDocumentoRequerido, source => source.MapFrom(p => p.id_tdocreq))
                    .ForMember(dest => dest.TipoDocumentoRequeridoDetalle, source => source.MapFrom(p => p.tdocreq_detalle))
                    .ForMember(dest => dest.IdTipoDocsis, source => source.MapFrom(p => p.id_tipodocsis))
                    .ForMember(dest => dest.IdFile, source => source.MapFrom(p => p.id_file))
                    .ForMember(dest => dest.GeneradoxSistema, source => source.MapFrom(p => p.generadoxSistema))
                    .ForMember(dest => dest.NombreArchivo, source => source.MapFrom(p => p.nombre_archivo))
                    .ForMember(dest => dest.IdAgrupamiento, source => source.MapFrom(p => p.id_agrupamiento))
                    .ForMember(dest => dest.TipoDocumentoRequerido, source => source.MapFrom(p => p.TiposDeDocumentosRequeridos));

                cfg.CreateMap<TransferenciasDocumentosAdjuntosDTO, Transf_DocumentosAdjuntos>()
                    .ForMember(dest => dest.id_docadjunto, source => source.MapFrom(p => p.Id))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_tdocreq, source => source.MapFrom(p => p.IdTipoDocumentoRequerido))
                    .ForMember(dest => dest.tdocreq_detalle, source => source.MapFrom(p => p.TipoDocumentoRequeridoDetalle))
                    .ForMember(dest => dest.id_tipodocsis, source => source.MapFrom(p => p.IdTipoDocsis))
                    .ForMember(dest => dest.id_file, source => source.MapFrom(p => p.IdFile))
                    .ForMember(dest => dest.generadoxSistema, source => source.MapFrom(p => p.GeneradoxSistema))
                    .ForMember(dest => dest.nombre_archivo, source => source.MapFrom(p => p.NombreArchivo))
                    .ForMember(dest => dest.id_agrupamiento, source => source.MapFrom(p => p.IdAgrupamiento))
                    .ForMember(dest => dest.TiposDeDocumentosRequeridos, source => source.Ignore());

                cfg.CreateMap<TiposDeDocumentosRequeridos, TiposDeDocumentosRequeridosDTO>();

                cfg.CreateMap<TiposDeDocumentosRequeridosDTO, TiposDeDocumentosRequeridos>();
            });
            mapperBase = config.CreateMapper();

          
        }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
        public TransferenciasDocumentosAdjuntosDTO Single(int Id )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(Id);
                var entityDto = mapperBase.Map<Transf_DocumentosAdjuntos, TransferenciasDocumentosAdjuntosDTO>(entity);
     
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
		public IEnumerable<TransferenciasDocumentosAdjuntosDTO> GetByFKIdSolicitud(int IdSolicitud)
		{
            try{
			    uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByFKIdSolicitud(IdSolicitud);
                var elementsDto = mapperBase.Map<IEnumerable<Transf_DocumentosAdjuntos>, IEnumerable<TransferenciasDocumentosAdjuntosDTO>>(elements);
                return elementsDto;
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
		public bool Insert(TransferenciasDocumentosAdjuntosDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory();
                IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork();
		        
                PDFBL pdfBL = new PDFBL();
                if (objectDto.IdTipoDocumentoRequerido == (int)Constantes.TipoDocumentoRequerido.Acta_Notarial)
                    if (!pdfBL.isFirmadoPdf(objectDto.Documento))
                        throw new Exception("El archivo no esta firmado");

		        repo = new TransferenciasDocumentosAdjuntosRepository(unitOfWork);
                repoTipoDoc = new TiposDeDocumentosRequeridosRepository(unitOfWork);
 
                ExternalServiceFiles externalServices = new ExternalServiceFiles();
                int IdFile =  externalServices.addFile(objectDto.NombreArchivo, objectDto.Documento);

                objectDto.IdFile = IdFile;
                objectDto.CreateDate = DateTime.Now;
                objectDto.IdTipoDocsis = (int)Constantes.TiposDeDocumentosSistema.DOC_ADJUNTO_TRANSFERENCIA;
                    
                var tdorequerido = repoTipoDoc.Single(objectDto.IdTipoDocumentoRequerido); 

                if (tdorequerido != null )
                {
                    if (tdorequerido.nombre_tdocreq.ToLower().Equals("plancheta"))
                        objectDto.IdTipoDocsis = (int)Constantes.TiposDeDocumentosSistema.PLANCHETA_TRANSFERENCIA;
                }
                    
		        var elementDto = mapperBase.Map<TransferenciasDocumentosAdjuntosDTO, Transf_DocumentosAdjuntos>(objectDto);                   

                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWorkTran = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new TransferenciasDocumentosAdjuntosRepository(unitOfWorkTran);
                    var insertOk = repo.Insert(elementDto);
                    unitOfWorkTran.Commit();
                }
		           
		        return true;
		       
		    }
		    catch
		    {
		        throw;
		    }
		}

        public bool Insert(TransferenciasDocumentosAdjuntosDTO objectDto, bool ForzarGenerar)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new TransferenciasDocumentosAdjuntosRepository(unitOfWork);
                    var elementDto = mapperBase.Map<TransferenciasDocumentosAdjuntosDTO, Transf_DocumentosAdjuntos>(objectDto);

                    if (!ForzarGenerar)
                    {
                        List<int> lstEstadosNoPermitidos = new List<int>();
                        lstEstadosNoPermitidos.Add((int)Constantes.TipoEstadoSolicitudEnum.ANU);
                        lstEstadosNoPermitidos.Add((int)Constantes.TipoEstadoSolicitudEnum.APRO);
                        lstEstadosNoPermitidos.Add((int)Constantes.TipoEstadoSolicitudEnum.RECH);
                        lstEstadosNoPermitidos.Add((int)Constantes.TipoEstadoSolicitudEnum.VENCIDA);
                        TransferenciasSolicitudesBL solBL = new TransferenciasSolicitudesBL();
                        var sol = solBL.Single(objectDto.IdSolicitud);

                        if (sol != null)
                            if (lstEstadosNoPermitidos.Contains(sol.IdEstado))
                            {
                               string  NoEsPosibleGenerarDocuementoEstado = string.Format("No se puede generar el documento cuando el tramite se encuentra " + sol.Estado.Descripcion);
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
        public IEnumerable<TransferenciasDocumentosAdjuntosDTO> GetByFKIdSolicitudTipoDocSis(int id_solicitud, int id_tipodocsis)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByFKIdSolicitudTipoDocSis(id_solicitud, id_tipodocsis);
                var elementsDto = mapperBase.Map<IEnumerable<Transf_DocumentosAdjuntos>, IEnumerable<TransferenciasDocumentosAdjuntosDTO>>(elements);
                return elementsDto;
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
        public void Update(TransferenciasDocumentosAdjuntosDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TransferenciasDocumentosAdjuntosRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<TransferenciasDocumentosAdjuntosDTO, Transf_DocumentosAdjuntos>(objectDTO);                   
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
		public void Delete(TransferenciasDocumentosAdjuntosDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new TransferenciasDocumentosAdjuntosRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<TransferenciasDocumentosAdjuntosDTO, Transf_DocumentosAdjuntos>(objectDto);                   
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

