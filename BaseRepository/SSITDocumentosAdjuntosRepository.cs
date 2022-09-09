using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using StaticClass;

namespace BaseRepository
{
    public class SSITDocumentosAdjuntosRepository : BaseRepository<SSIT_DocumentosAdjuntos>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SSITDocumentosAdjuntosRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>	
        public IEnumerable<SSIT_DocumentosAdjuntos> GetByFKIdSolicitud(int id_solicitud)
        {
            List<int> lstIdEstado = new List<int>();
            lstIdEstado.Add((int)Constantes.TipoEstadoSolicitudEnum.INCOM);
            lstIdEstado.Add((int)Constantes.TipoEstadoSolicitudEnum.COMP);
            lstIdEstado.Add((int)Constantes.TipoEstadoSolicitudEnum.PING);
            lstIdEstado.Add((int)Constantes.TipoEstadoSolicitudEnum.ANU);
            lstIdEstado.Add((int)Constantes.TipoEstadoSolicitudEnum.PENPAG);

            var domains = (from doc in _unitOfWork.Db.SSIT_DocumentosAdjuntos
                           join sol in _unitOfWork.Db.SSIT_Solicitudes on doc.id_solicitud equals sol.id_solicitud
                           where doc.id_solicitud == id_solicitud && 
                           (doc.id_tipodocsis != 13 || !lstIdEstado.Contains(sol.id_estado))
                           select doc );

            return domains;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_solicitud"></param>
        /// <param name="id_tipodocsis"></param>
        /// <returns></returns>
        public IEnumerable<SSIT_DocumentosAdjuntos> GetByFKIdSolicitudTipoDocSis(int id_solicitud, int id_tipodocsis)
        {
            return _unitOfWork.Db.SSIT_DocumentosAdjuntos
                         .Where(x => x.id_solicitud == id_solicitud
                           && x.id_tipodocsis == id_tipodocsis);
        }

        public IEnumerable<SSIT_DocumentosAdjuntos> GetByFKIdSolicitudTipoDocReq(int id_solicitud, int id_tdocreq)
        {
            return _unitOfWork.Db.SSIT_DocumentosAdjuntos
                         .Where(x => x.id_solicitud == id_solicitud
                           && x.id_tdocreq == id_tdocreq);
        }

        /// <summary>
        /// Se cambia el metodo que se usaba en el repository SSITDocumentosAdjuntosEntityRepository, se mantiene la misma logica que hay ahi.
        /// </summary>
        /// <param name="id_solicitud"></param>
        /// <returns></returns>
        public IEnumerable<SSIT_DocumentosAdjuntos> GetByFKIdSolicitudGenerados(int id_solicitud)
        {

            var domains = (from doc in _unitOfWork.Db.SSIT_DocumentosAdjuntos
                           where doc.id_solicitud == id_solicitud 
                                //&& doc.id_tipodocsis != (int)Constantes.TiposDeDocumentosSistema.CERTIFICADO_PRO_TEATRO
                                && doc.id_tdocreq == 0
                           select doc);
            return domains;
        }
    }
}

