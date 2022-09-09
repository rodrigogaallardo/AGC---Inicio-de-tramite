using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using StaticClass;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class SSITDocumentosAdjuntosEntityRepository : BaseRepository<SSITDocumentosAdjuntosEntity>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SSITDocumentosAdjuntosEntityRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }

        public IEnumerable<SSITDocumentosAdjuntosEntity> GetByFKIdSolicitud(int id_solicitud)
        {
            var domains = (from doc in _unitOfWork.Db.SSIT_DocumentosAdjuntos
                           join tipo in _unitOfWork.Db.TiposDeDocumentosRequeridos on doc.id_tdocreq equals tipo.id_tdocreq
                           join sol in _unitOfWork.Db.SSIT_Solicitudes on doc.id_solicitud equals sol.id_solicitud
                           join us in _unitOfWork.Db.Usuario on doc.CreateUser equals us.UserId
                           where doc.id_solicitud == id_solicitud
                           && doc.id_tdocreq != 0
                           select new SSITDocumentosAdjuntosEntity
                           {
                               id_estado = sol.id_estado,
                               id_docadjunto = doc.id_docadjunto,
                               id_file = doc.id_file,
                               CreateDate = doc.CreateDate,
                               nombre_archivo = doc.nombre_archivo,
                               detalle = tipo.nombre_tdocreq,
                               id_master = doc.id_solicitud,
                               usuario = us.Nombre + " " + us.Apellido,
                               id_tdocreq = tipo.id_tdocreq
                           });           
            return domains;
        }

        public IEnumerable<SSITDocumentosAdjuntosEntity> GetByFKIdSolicitudGeneradosIdDocReq(int id_solicitud, int id_docReq)
        {

            var domains = (from doc in _unitOfWork.Db.SSIT_DocumentosAdjuntos
                           join sol in _unitOfWork.Db.SSIT_Solicitudes on doc.id_solicitud equals sol.id_solicitud
                           join user in _unitOfWork.Db.Usuario on doc.CreateUser equals user.UserId into us
                           from u in us.DefaultIfEmpty()
                           join tiposis in _unitOfWork.Db.TiposDeDocumentosSistema on doc.id_tipodocsis equals tiposis.id_tipdocsis
                           where doc.id_solicitud == id_solicitud 
                           && doc.id_tdocreq == id_docReq
                           select new SSITDocumentosAdjuntosEntity
                           {
                               id_docadjunto = doc.id_docadjunto,
                               id_file = doc.id_file,
                               CreateDate = doc.CreateDate,
                               nombre_archivo = doc.nombre_archivo,
                               detalle = tiposis.nombre_tipodocsis,
                               id_master = doc.id_solicitud,
                               usuario = u != null ? u.Nombre + " " + u.Apellido : ""
                           });
            return domains;
        }
        public IEnumerable<SSITDocumentosAdjuntosEntity> GetByFKIdSolicitudGenerados(int id_solicitud)
        {

            var domains = (from doc in _unitOfWork.Db.SSIT_DocumentosAdjuntos
                           join sol in _unitOfWork.Db.SSIT_Solicitudes on doc.id_solicitud equals sol.id_solicitud
                           join user in _unitOfWork.Db.Usuario on doc.CreateUser equals user.UserId into us
                           from u in us.DefaultIfEmpty()
                           join tiposis in _unitOfWork.Db.TiposDeDocumentosSistema on doc.id_tipodocsis equals tiposis.id_tipdocsis
                           where doc.id_solicitud == id_solicitud && doc.id_tdocreq != (int)Constantes.TipoDocumentoRequerido.CertificadoProTeatro
                           && doc.id_tdocreq == 0
                           select new SSITDocumentosAdjuntosEntity
                           {
                               id_docadjunto = doc.id_docadjunto,
                               id_file = doc.id_file,
                               CreateDate = doc.CreateDate,
                               nombre_archivo = doc.nombre_archivo,
                               detalle = tiposis.nombre_tipodocsis,
                               id_master = doc.id_solicitud,
                               usuario = u != null ? u.Nombre + " " + u.Apellido : ""
                           });
            return domains;
        }

        public IEnumerable<SSITDocumentosAdjuntosEntity> GetByFKListIdEncomienda(List<int> lstIdEncomienda)
        {
            var domains = (from doc in _unitOfWork.Db.Encomienda_Planos
                           join tipo in _unitOfWork.Db.TiposDePlanos on doc.id_tipo_plano equals tipo.id_tipo_plano
                           join enc in  _unitOfWork.Db.Encomienda on doc.id_encomienda equals enc.id_encomienda
                           join pro in _unitOfWork.Db.Profesional on enc.CreateUser equals pro.UserId
                           where lstIdEncomienda.Contains(doc.id_encomienda)
                           select new SSITDocumentosAdjuntosEntity
                           {
                               id_docadjunto = doc.id_encomienda_plano,
                               id_master = doc.id_encomienda,
                               id_file = doc.id_file,
                               CreateDate = doc.CreateDate,
                               nombre_archivo = doc.nombre_archivo,
                               detalle = doc.id_tipo_plano == (int)Constantes.TiposDePlanos.Otro && !string.IsNullOrEmpty(doc.detalle) ? doc.detalle : tipo.nombre,
                               usuario = pro.Nombre + " " + pro.Apellido
                           }).Union(from ed in _unitOfWork.Db.Encomienda_DocumentosAdjuntos
                                    join usu in _unitOfWork.Db.aspnet_Users on ed.CreateUser equals usu.UserId
                                    join tipdocsis in _unitOfWork.Db.TiposDeDocumentosSistema on ed.id_tipodocsis equals tipdocsis.id_tipdocsis
                                    join usuSol in _unitOfWork.Db.Usuario on ed.CreateUser equals usuSol.UserId into pleft_usuSol
                                    from usuSol in pleft_usuSol.DefaultIfEmpty()
                                    join pro in _unitOfWork.Db.Profesional on ed.CreateUser equals pro.UserId into pleft_pro
                                    from pro in pleft_pro.DefaultIfEmpty()
                                    where lstIdEncomienda.Contains(ed.id_encomienda) && ed.id_tipodocsis != (int)Constantes.TiposDeDocumentosSistema.ENCOMIENDA_DIGITAL
                                    select new SSITDocumentosAdjuntosEntity
                                    {
                                        id_docadjunto = ed.id_docadjunto,
                                        id_master = ed.id_encomienda,
                                        id_file = ed.id_file,
                                        CreateDate = ed.CreateDate,
                                        nombre_archivo = ed.nombre_archivo,
                                        detalle = ( ed.id_tdocreq == 0 ? tipdocsis.nombre_tipodocsis :  ed.TiposDeDocumentosRequeridos.nombre_tdocreq),
                                        usuario = ( pro != null ? pro.Nombre + " " + pro.Apellido : ( usuSol != null ? usuSol.Apellido + " " + usuSol.Nombre : usu.UserName))
                                    }
                );

            return domains;
        }
    }
}

