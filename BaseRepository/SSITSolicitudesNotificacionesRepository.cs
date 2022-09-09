using Dal.UnitOfWork;
using DataAcess;
using DataAcess.EntityCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRepository
{
    public class SSITSolicitudesNotificacionesRepository : BaseRepository<SSIT_Solicitudes_Notificaciones>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SSITSolicitudesNotificacionesRepository(IUnitOfWork unit)
                : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }

        public IEnumerable<SSIT_Solicitudes_Notificaciones> GetNotificacionByUserId(Guid userid)
        {
            try
            {
                var ListaSol = (from not in _unitOfWork.Db.SSIT_Solicitudes_Notificaciones
                                where
                                    not.SSIT_Solicitudes.CreateUser == userid && not.fechaNotificacionSSIT == null
                                select not);
                return ListaSol;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<SSIT_Solicitudes_Notificaciones> GetNotificacionByIdSolicitud(int id_solicitud)
        {
            try
            {
                var ListaSol = (from not in _unitOfWork.Db.SSIT_Solicitudes_Notificaciones
                                where
                                  not.id_solicitud == id_solicitud
                                select not);
                return ListaSol;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<AvisoNotificacionEntity> GetAvisoNotificaciones(Guid userid)
        {
            var lstAvisos = (from not in _unitOfWork.Db.SSIT_Solicitudes_Notificaciones
                             join ssit in _unitOfWork.Db.SSIT_Solicitudes on not.id_solicitud equals ssit.id_solicitud
                             join mail in _unitOfWork.Db.Emails on not.id_email equals mail.id_email
                             join solu in _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones on ssit.id_solicitud equals solu.id_solicitud
                             join dirc in _unitOfWork.Db.Ubicaciones_DireccionesConformadas on solu.id_ubicacion equals dirc.id_ubicacion
                             join notMotiv in _unitOfWork.Db.SSIT_Solicitudes_Notificaciones_motivos on not.Id_NotificacionMotivo equals notMotiv.IdNotificacionMotivo into Notif
                             from nt in Notif.DefaultIfEmpty()
                             where ssit.CreateUser == userid
                             select new AvisoNotificacionEntity
                             {
                                 IdNotificacion = not.id_notificacion,
                                 AsuntoMail = mail.asunto,
                                 Domicilio = dirc.direccion,
                                 FechaAviso = mail.fecha_alta,
                                 IdMail = mail.id_email,
                                 IdTramite = ssit.id_solicitud,
                                 Id_NotificacionMotivo = nt.IdNotificacionMotivo != null ? nt.IdNotificacionMotivo : 0,
                                 NotificacionMotivo = nt.NotificacionMotivo,
                                 Url = "~/Solicitud/Habilitacion/Visualizar/"+ssit.id_solicitud,
                                 FechaNotificacion = not.fechaNotificacionSSIT == null ? null : not.fechaNotificacionSSIT,
                                 CreateDate = not.createDate
                             });

            var listAvisosTr = (from not in _unitOfWork.Db.Transf_Solicitudes_Notificaciones
                                join tssit in _unitOfWork.Db.Transf_Solicitudes on not.id_solicitud equals tssit.id_solicitud
                                join mail in _unitOfWork.Db.Emails on not.id_email equals mail.id_email
                                join soluleft in _unitOfWork.Db.Transf_Ubicaciones on tssit.id_solicitud equals soluleft.id_solicitud into TransfDirecciones
                                from dir in TransfDirecciones.DefaultIfEmpty()
                                join dirleft in _unitOfWork.Db.Ubicaciones_DireccionesConformadas on dir.id_ubicacion equals dirleft.id_ubicacion into DireccionConformadas
                                from dirConf in DireccionConformadas.DefaultIfEmpty()
                                join cpleft in _unitOfWork.Db.CPadron_Ubicaciones on tssit.id_cpadron equals cpleft.id_cpadron into TransfCPDirecciones
                                from dirCP in TransfCPDirecciones.DefaultIfEmpty()
                                join dirCPleft in _unitOfWork.Db.Ubicaciones_DireccionesConformadas on dirCP.id_ubicacion equals dirCPleft.id_ubicacion into DireccionConformadasCP
                                from dirCPConf in DireccionConformadasCP.DefaultIfEmpty()
                                join notMotiv in _unitOfWork.Db.SSIT_Solicitudes_Notificaciones_motivos on not.Id_NotificacionMotivo equals notMotiv.IdNotificacionMotivo into Notif
                                from nt in Notif.DefaultIfEmpty()
                                where tssit.CreateUser == userid
                                select new AvisoNotificacionEntity
                                {
                                    IdNotificacion = not.id_notificacion,
                                    AsuntoMail = mail.asunto,
                                    Domicilio = dirConf.direccion.Length > 1 ? dirConf.direccion : dirCPConf.direccion,
                                    FechaAviso = mail.fecha_alta,
                                    IdMail = mail.id_email,
                                    IdTramite = tssit.id_solicitud,
                                    Id_NotificacionMotivo = nt.IdNotificacionMotivo != null ? nt.IdNotificacionMotivo : 0,
                                    NotificacionMotivo = nt.NotificacionMotivo,
                                    Url = "~/Solicitud/Transferencias/Visualizar/"+ tssit.id_solicitud,
                                    FechaNotificacion = not.fechaNotificacionSSIT == null ? null : not.fechaNotificacionSSIT,
                                    CreateDate = not.createDate
                                });

            return lstAvisos.Union(listAvisosTr);
        }

        public int GetCantidadNotificacionesByUser(Guid userid)
        {
            var q = (from not in _unitOfWork.Db.SSIT_Solicitudes_Notificaciones
                     where not.SSIT_Solicitudes.CreateUser == userid && not.fechaNotificacionSSIT == null
                     select not)
                    .ToList();
            var qtr = (from transf in _unitOfWork.Db.Transf_Solicitudes
                       join not in _unitOfWork.Db.Transf_Solicitudes_Notificaciones on transf.id_solicitud equals not.id_solicitud
                       where transf.CreateUser == userid && not.fechaNotificacionSSIT == null
                       select not)
                    .ToList();
            return q.Count() + qtr.Count;
        }

        public SSIT_Solicitudes_Notificaciones GetNotificacionByIdNotificacion(int idNotificacion)
        {
            return (from not in _unitOfWork.Db.SSIT_Solicitudes_Notificaciones
                    where not.id_notificacion == idNotificacion
                    select not).FirstOrDefault();
        }

        public string getMotivoById(int idMotivoNotificacion)
        {
            return (from motiv in _unitOfWork.Db.SSIT_Solicitudes_Notificaciones_motivos
                    where motiv.IdNotificacionMotivo == idMotivoNotificacion
                    select motiv.NotificacionMotivo).FirstOrDefault();
        }

        public Transf_Solicitudes_Notificaciones GetNotificacionTRByIdNotificacion(int idNotificacion)
        {
            return (from not in _unitOfWork.Db.Transf_Solicitudes_Notificaciones
                    where not.id_notificacion == idNotificacion
                    select not).FirstOrDefault();
        }
    }
}
