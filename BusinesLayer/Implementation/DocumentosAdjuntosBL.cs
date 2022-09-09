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

namespace BusinesLayer.Implementation
{
    //public class DocumentosAdjuntosBL : IDocumentosAdjuntosBL<DocumentosAdjuntosDTO>
    //{               
    //    private DocumentosAdjuntosRepository repo = null;        
    //    private IUnitOfWorkFactory uowF = null;
    //    IMapper mapperBase;
		         
    //    public DocumentosAdjuntosBL()
    //    {            
    //        var config = new MapperConfiguration(cfg =>
    //        {
    //            cfg.CreateMap<DocumentosAdjuntosDTO, vw_DocumentosAdjuntos>().ReverseMap();
    //        });
    //        mapperBase = config.CreateMapper();
    //    }
		
    //    public IEnumerable<DocumentosAdjuntosDTO> GetAll()
    //    {
    //        try
    //        {
    //            uowF = new TransactionScopeUnitOfWorkFactory();
    //            repo = new DocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
    //            var elements = repo.GetAll();
    //            var elementsDto = mapperBase.Map<IEnumerable<vw_DocumentosAdjuntos>, IEnumerable<DocumentosAdjuntosDTO>>(elements);
    //            return elementsDto;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }	
    //    public DocumentosAdjuntosDTO Single(int id_docadjunto)
    //    {
    //        try
    //        {
    //            uowF = new TransactionScopeUnitOfWorkFactory();
    //            repo = new DocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
    //            var entity = repo.Single(id_docadjunto);
    //            var entityDto = mapperBase.Map<vw_DocumentosAdjuntos, DocumentosAdjuntosDTO>(entity);
     
    //            return entityDto;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //    #region Métodos de inserccion
    //    /// <summary>
    //    /// Inserta la entidad para por parametro
    //    /// </summary>
    //    /// <param name="objectDto"></param>
    //    public bool Insert(DocumentosAdjuntosDTO objectDto)
    //    {
    //        try
    //        {   
    //            uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
    //            using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
    //            {                    
    //                repo = new DocumentosAdjuntosRepository(unitOfWork);                    
    //                var elementDto = mapperBase.Map<DocumentosAdjuntosDTO, vw_DocumentosAdjuntos>(objectDto);
    //                elementDto.id_docadjunto = repo.Max() + 1;
    //                var insertOk = repo.Insert(elementDto);
    //                unitOfWork.Commit();
    //                return true;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
		

    //    #endregion
    //    #region Métodos de actualizacion
    //    /// <summary>
    //    /// Modifica la entidad para por parametro
    //    /// </summary>
    //    /// <param name="objectDto"></param>
    //    public void Update(DocumentosAdjuntosDTO objectDTO)
    //    {
    //        try
    //        {   
    //            uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
    //            using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
    //            {                    
    //                repo = new DocumentosAdjuntosRepository(unitOfWork);                    
    //                var elementDTO = mapperBase.Map<DocumentosAdjuntosDTO, vw_DocumentosAdjuntos>(objectDTO);                   
    //                repo.Update(elementDTO);
    //                unitOfWork.Commit();           
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
		
    //    #endregion
    //    #region Métodos de eliminacion
    //    /// <summary>
    //    /// elimina la entidad para por parametro
    //    /// </summary>
    //    /// <param name="objectDto"></param>      
    //    public void Delete(DocumentosAdjuntosDTO objectDto)
    //    {
    //        try
    //        {   
    //            uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
    //            using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
    //            {                    
    //                repo = new DocumentosAdjuntosRepository(unitOfWork);                    
    //                var elementDto = mapperBase.Map<DocumentosAdjuntosDTO, vw_DocumentosAdjuntos>(objectDto);                   
    //                var insertOk = repo.Delete(elementDto);
    //                unitOfWork.Commit();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //    #endregion
    //    public IEnumerable<DocumentosAdjuntosDTO> GetByFKIdSolicitudIdAgrupamiento(int id_solicitud, int id_agrupamiento)
    //    {
    //        try
    //        {
    //            uowF = new TransactionScopeUnitOfWorkFactory();
    //            repo = new DocumentosAdjuntosRepository(this.uowF.GetUnitOfWork());
    //            var elements = repo.GetByFKIdSolicitudIdAgrupamiento(id_solicitud, id_agrupamiento);
    //            return elements;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //}
}

