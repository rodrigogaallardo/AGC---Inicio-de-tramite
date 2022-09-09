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
using DataAcess.EntityCustom;

namespace BusinesLayer.Implementation
{
	public class ConsultaPadronDocumentosAdjuntosBL : IConsultaPadronDocumentosAdjuntosBL<ConsultaPadronDocumentosAdjuntosDTO>
    {               
		private ConsultaPadronDocumentosAdjuntosRepository repo = null;
        private TiposDeDocumentosRequeridosRepository repoTipoDoc = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
        //IMapper mapperCertificado;
 
        public ConsultaPadronDocumentosAdjuntosBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CPadron_DocumentosAdjuntos, ConsultaPadronDocumentosAdjuntosDTO>()                
                    .ForMember(dest => dest.Id, source => source.MapFrom(p => p.id_docadjunto))
                    .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                    .ForMember(dest => dest.IdTipodocumentoRequerido, source => source.MapFrom(p => p.id_tdocreq))
                    .ForMember(dest => dest.TipodocumentoRequeridoDetalle, source => source.MapFrom(p => p.tdocreq_detalle))
                    .ForMember(dest => dest.IdTipoDocumentoSistema, source => source.MapFrom(p => p.id_tipodocsis))
                    .ForMember(dest => dest.IdFile, source => source.MapFrom(p => p.id_file))
                    .ForMember(dest => dest.GeneradoxSistema, source => source.MapFrom(p => p.generadoxSistema))
                    .ForMember(dest => dest.NombreArchivo, source => source.MapFrom(p => p.nombre_archivo))
                    .ForMember(dest => dest.TiposDeDocumentosRequeridos, source => source.MapFrom(p => p.TiposDeDocumentosRequeridos))
                    .ForMember(dest => dest.TiposDeDocumentosSistema, source => source.MapFrom(p => p.TiposDeDocumentosSistema));

                cfg.CreateMap<ConsultaPadronDocumentosAdjuntosDTO, CPadron_DocumentosAdjuntos>()                
                    .ForMember(dest => dest.id_docadjunto, source => source.MapFrom(p => p.Id))
                    .ForMember(dest => dest.id_cpadron, source => source.MapFrom(p => p.IdConsultaPadron))
                    .ForMember(dest => dest.id_tdocreq, source => source.MapFrom(p => p.IdTipodocumentoRequerido))
                    .ForMember(dest => dest.tdocreq_detalle, source => source.MapFrom(p => p.TipodocumentoRequeridoDetalle))
                    .ForMember(dest => dest.id_tipodocsis, source => source.MapFrom(p => p.IdTipoDocumentoSistema))
                    .ForMember(dest => dest.id_file, source => source.MapFrom(p => p.IdFile))
                    .ForMember(dest => dest.generadoxSistema, source => source.MapFrom(p => p.GeneradoxSistema))
                    .ForMember(dest => dest.nombre_archivo, source => source.MapFrom(p => p.NombreArchivo))
                    .ForMember(dest => dest.TiposDeDocumentosRequeridos, source => source.MapFrom(p => p.TiposDeDocumentosRequeridos))
                    .ForMember(dest => dest.TiposDeDocumentosSistema, source => source.MapFrom(p => p.TiposDeDocumentosSistema));

                cfg.CreateMap<TiposDeDocumentosRequeridos, TiposDeDocumentosRequeridosDTO>().ReverseMap();
                cfg.CreateMap<TiposDeDocumentosSistema, TiposDeDocumentosSistemaDTO>().ReverseMap();

            });
            mapperBase = config.CreateMapper();

            //var configCertificado = new MapperConfiguration(cfg =>
            //{
            //    cfg.CreateMap<ItemCertificadoDTO, ItemCertificadoEntity>().ReverseMap();
            //});
            //mapperCertificado = configCertificado.CreateMapper(); 
        }
		
        //public IEnumerable<ConsultaPadronDocumentosAdjuntosDTO> GetAll()
        //{
        //    try
        //    {
        //        uowF = new TransactionScopeUnitOfWorkFactory();
        //        repo = new ConsultaPadronDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
        //        var elements = repo.GetAll();
        //        var elementsDto = mapperBase.Map<IEnumerable<CPadron_DocumentosAdjuntos>, IEnumerable<ConsultaPadronDocumentosAdjuntosDTO>>(elements);
        //        return elementsDto;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}	
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
		public ConsultaPadronDocumentosAdjuntosDTO Single(int Id)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(Id);
                var entityDto = mapperBase.Map<CPadron_DocumentosAdjuntos, ConsultaPadronDocumentosAdjuntosDTO>(entity);
     
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
		/// <param name="IdConsultaPadron"></param>
		/// <returns></returns>	
		public IEnumerable<ConsultaPadronDocumentosAdjuntosDTO> GetByFKIdConsultaPadron(int IdConsultaPadron)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new ConsultaPadronDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdConsultaPadron(IdConsultaPadron);
            var elementsDto = mapperBase.Map<IEnumerable<CPadron_DocumentosAdjuntos>, IEnumerable<ConsultaPadronDocumentosAdjuntosDTO>>(elements);
            return elementsDto;				
		}

        public IEnumerable<ConsultaPadronDocumentosAdjuntosDTO> GetByFKIdConsultaPadronTipoSis(int IdConsultaPadron, int idTipo)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByFKIdConsultaPadronTipoSis(IdConsultaPadron, idTipo);
                var elementsDTO = mapperBase.Map<IEnumerable<CPadron_DocumentosAdjuntos>, IEnumerable<ConsultaPadronDocumentosAdjuntosDTO>>(elements);
                return elementsDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdConsultaPadron"></param>
        /// <returns></returns>
        //public IEnumerable<ItemCertificadoDTO> Get(int IdConsultaPadron)
        //{
        //    uowF = new TransactionScopeUnitOfWorkFactory();
        //    repo = new ConsultaPadronDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
        //    var elements = repo.Get(IdConsultaPadron);
        //    var elementsDto = mapperCertificado.Map<IEnumerable<ItemCertificadoEntity>, IEnumerable<ItemCertificadoDTO>>(elements);
        //    return elementsDto;				
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTipodocumentoRequerido"></param>
        /// <returns></returns>	
        //public IEnumerable<ConsultaPadronDocumentosAdjuntosDTO> GetByFKIdTipodocumentoRequerido(int IdTipodocumentoRequerido)
        //{
        //    uowF = new TransactionScopeUnitOfWorkFactory();
        //    repo = new ConsultaPadronDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
        //     var elements = repo.GetByFKIdTipodocumentoRequerido(IdTipodocumentoRequerido);
        //    var elementsDto = mapperBase.Map<IEnumerable<CPadron_DocumentosAdjuntos>, IEnumerable<ConsultaPadronDocumentosAdjuntosDTO>>(elements);
        //    return elementsDto;				
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="IdTipoDocumentoSistema"></param>
        ///// <returns></returns>	
        //public IEnumerable<ConsultaPadronDocumentosAdjuntosDTO> GetByFKIdTipoDocumentoSistema(int IdTipoDocumentoSistema)
        //{
        //    uowF = new TransactionScopeUnitOfWorkFactory();
        //    repo = new ConsultaPadronDocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
        //     var elements = repo.GetByFKIdTipoDocumentoSistema(IdTipoDocumentoSistema);
        //    var elementsDto = mapperBase.Map<IEnumerable<CPadron_DocumentosAdjuntos>, IEnumerable<ConsultaPadronDocumentosAdjuntosDTO>>(elements);
        //    return elementsDto;				
        //}
        #region Métodos de actualizacion e insert
        /// <summary>
        /// Inserta la entidad para por parametro
        /// </summary>
        /// <param name="objectDto"></param>
        public bool Insert(ConsultaPadronDocumentosAdjuntosDTO objectDto)
		{
		    try
		    {
                if (objectDto.NombreArchivo.Length > 50)
                    throw new Exception(Errors.SSIT_CPADRON_NONBRE_ARCHIVO); 

		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new ConsultaPadronDocumentosAdjuntosRepository(unitOfWork);
                    repoTipoDoc = new TiposDeDocumentosRequeridosRepository(unitOfWork); 

                    if (objectDto.IdTipoDocumentoSistema != (int)Constantes.TiposDeDocumentosSistema.SOLICITUD_CPADRON)
                        objectDto.IdTipoDocumentoSistema = (int)Constantes.TiposDeDocumentosSistema.DOC_ADJUNTO_CPADRON;

                    var tdorequerido = repoTipoDoc.Single(objectDto.IdTipodocumentoRequerido);

                    if (tdorequerido != null)
                    {
                        if (tdorequerido.nombre_tdocreq.ToLower().Equals(Constantes.PLANCHETA))
                            objectDto.IdTipoDocumentoSistema = (int)Constantes.TiposDeDocumentosSistema.PLANCHETA_CPADRON;
                    }
		            var elementDto = mapperBase.Map<ConsultaPadronDocumentosAdjuntosDTO, CPadron_DocumentosAdjuntos>(objectDto);                   
                    
		            var insertOk = repo.Insert(elementDto);
		            unitOfWork.Commit();
                    objectDto.Id = elementDto.id_docadjunto;
		            return true;
		        }
		    }
		    catch
		    {
		        throw;
		    }
		}		
		#endregion
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Modifica la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
		public void Update(ConsultaPadronDocumentosAdjuntosDTO objectDTO)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new ConsultaPadronDocumentosAdjuntosRepository(unitOfWork);                    
		            var elementDTO = mapperBase.Map<ConsultaPadronDocumentosAdjuntosDTO, CPadron_DocumentosAdjuntos>(objectDTO);                   
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
		public void Delete(ConsultaPadronDocumentosAdjuntosDTO objectDto)
		{
		    try
		    {   
		        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
		        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
		        {                    
		            repo = new ConsultaPadronDocumentosAdjuntosRepository(unitOfWork);                    
		            var elementDto = mapperBase.Map<ConsultaPadronDocumentosAdjuntosDTO, CPadron_DocumentosAdjuntos>(objectDto);                   
		            var insertOk = repo.Delete(elementDto);
		            unitOfWork.Commit();
		        }
		    }
		    catch (Exception ex)
		    {
		        throw ex;
		    }
		}
		public void DeleteByFKIdConsultaPadron(int IdConsultaPadron)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new ConsultaPadronDocumentosAdjuntosRepository(unitOfWork);                    
                    //var elements = repo.GetByFKIdConsultaPadron(IdConsultaPadron);
                    //foreach(var element in elements)				
                    //    repo.Delete(element);
		
					unitOfWork.Commit();		
				}
		    }		
			catch (Exception ex)
			{
				//throw ex;
			}
		}
		public void DeleteByFKIdTipodocumentoRequerido(int IdTipodocumentoRequerido)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new ConsultaPadronDocumentosAdjuntosRepository(unitOfWork);                    
					var elements = repo.GetByFKIdTipodocumentoRequerido(IdTipodocumentoRequerido);
					foreach(var element in elements)				
						repo.Delete(element);
		
					unitOfWork.Commit();		
				}
		    }		
			catch (Exception ex)
			{
				//throw ex;
			}
		}
		public void DeleteByFKIdTipoDocumentoSistema(int IdTipoDocumentoSistema)
		{
			try
			{   
				uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
				using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
				{                    
					repo = new ConsultaPadronDocumentosAdjuntosRepository(unitOfWork);                    
					var elements = repo.GetByFKIdTipoDocumentoSistema(IdTipoDocumentoSistema);
					foreach(var element in elements)				
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

