using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    //public class DocumentosAdjuntosRepository : BaseRepository<vw_DocumentosAdjuntos> 
    //{
    //    private readonly IUnitOfWork _unitOfWork;

    //    public DocumentosAdjuntosRepository(IUnitOfWork unit) : base(unit)
    //    {
    //        if (unit == null) 
    //            throw new ArgumentNullException("unitOfWork Exception");
			
    //        _unitOfWork = unit;
    //    }	   	
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="IdEncomienda"></param>
    //    /// <returns></returns>	
    //    public IEnumerable<DocumentosAdjuntosDTO> GetByFKIdSolicitudIdAgrupamiento(int id_solicitud, int id_agrupamiento)
    //    {
    //        IEnumerable<DocumentosAdjuntosDTO> domains = (from d in _unitOfWork.Db.vw_DocumentosAdjuntos
    //                                                      join tipo_doc in _unitOfWork.Db.TiposDeDocumentosRequeridos on d.id_tdocreq equals tipo_doc.id_tdocreq
    //                                                      where d.id_solicitud == id_solicitud && d.id_agrupamiento == id_agrupamiento
    //                                                      select new DocumentosAdjuntosDTO
    //                                                      {
    //                                                          id_docadjunto = d.id_docadjunto,
    //                                                          id_encomienda = d.id_encomienda,
    //                                                          id_file = 0,
    //                                                          id_tdocreq = d.id_tdocreq,
    //                                                          documento = d.documento,
    //                                                          id_agrupamiento = d.id_agrupamiento,
    //                                                          id_solicitud = d.id_solicitud,
    //                                                          origen = d.origen,
    //                                                          puede_eliminar = d.puede_eliminar,
    //                                                          tdocreq_detalle = d.tdocreq_detalle,
    //                                                          nombre_tdocreq = tipo_doc.nombre_tdocreq,
    //                                                          CreateDate = d.CreateDate,
    //                                                          CreateUser = d.CreateUser
    //                                                      }).ToList();
	
    //        return domains;
    //    }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <returns></returns>
    //    public int Max()
    //    {
    //        return (from entity in _unitOfWork.Db.vw_DocumentosAdjuntos
    //            select entity.id_docadjunto).Max();            
    //    }
    //}
}

