using Dal.UnitOfWork;
using DataAcess;
using DataAcess.EntityCustom;
using StaticClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRepository
{
    public class MailsRepository : BaseRepository<Emails>
    {
        private readonly IUnitOfWork _unitOfWork;

        public MailsRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }

        public IEnumerable<clsItemGrillaMails> GetNotificacionesByIdSolicitud(int id_solicitud)
        {
            var q = (
                        from mail in _unitOfWork.Db.Emails
                        join tipo in _unitOfWork.Db.Emails_Tipos on mail.id_tipo_email equals tipo.id_tipo_email
                        join edo in _unitOfWork.Db.Email_Estados on mail.id_estado equals edo.id_estado
                        join ac in _unitOfWork.Db.SSIT_Solicitudes_Notificaciones on mail.id_email equals ac.id_email into pleft_ac
                        from ac in pleft_ac.DefaultIfEmpty()
                        where ac.id_solicitud == id_solicitud

                        orderby mail.id_email ascending
                        select new clsItemGrillaMails()
                        {
                            Mail_ID = mail.id_email.ToString(),
                            Mail_Estado = edo.descripcion,
                            Mail_Proceso = tipo.descripcion,
                            Mail_Asunto = mail.asunto,
                            Mail_Email = mail.email,
                            Mail_Fecha = (mail.fecha_envio == null) ? mail.fecha_alta : mail.fecha_envio,
                        }
                    ).ToList();

            var qac = (
                        from mail in _unitOfWork.Db.Emails
                        join tipo in _unitOfWork.Db.Emails_Tipos on mail.id_tipo_email equals tipo.id_tipo_email
                        join edo in _unitOfWork.Db.Email_Estados on mail.id_estado equals edo.id_estado
                        join ac in _unitOfWork.Db.SSIT_Solicitudes_AvisoCaducidad on mail.id_email equals ac.id_email into pleft_ac
                        from ac in pleft_ac.DefaultIfEmpty()
                        where mail.asunto.Contains(id_solicitud.ToString()) && ac.id_solicitud == id_solicitud || mail.html.Contains(id_solicitud.ToString()) && ac.id_solicitud == id_solicitud

                        orderby mail.id_email ascending
                        select new clsItemGrillaMails()
                        {
                            Mail_ID = mail.id_email.ToString(),
                            Mail_Estado = edo.descripcion,
                            Mail_Proceso = tipo.descripcion,
                            Mail_Asunto = mail.asunto,
                            Mail_Email = mail.email,
                            Mail_Fecha = (mail.fecha_envio == null) ? mail.fecha_alta : mail.fecha_envio,
                        }
                    ).ToList();

            var all = q.Union(qac);

            return all;
        }

        public IEnumerable<clsItemGrillaMails> GetNotificacionesByTransferencia(int id_solicitud)
        {
            var q = (
                        from mail in _unitOfWork.Db.Emails
                        join tipo in _unitOfWork.Db.Emails_Tipos on mail.id_tipo_email equals tipo.id_tipo_email
                        join edo in _unitOfWork.Db.Email_Estados on mail.id_estado equals edo.id_estado
                        join ac in _unitOfWork.Db.Transf_Solicitudes_Notificaciones on mail.id_email equals ac.id_email into pleft_ac
                        from ac in pleft_ac.DefaultIfEmpty()
                        where ac.id_solicitud == id_solicitud

                        orderby mail.id_email ascending
                        select new clsItemGrillaMails()
                        {
                            Mail_ID = mail.id_email.ToString(),
                            Mail_Estado = edo.descripcion,
                            Mail_Proceso = tipo.descripcion,
                            Mail_Asunto = mail.asunto,
                            Mail_Email = mail.email,
                            Mail_Fecha = (mail.fecha_envio == null) ? mail.fecha_alta : mail.fecha_envio,
                        }
                    ).ToList();

            var qac = (
                        from mail in _unitOfWork.Db.Emails
                        join tipo in _unitOfWork.Db.Emails_Tipos on mail.id_tipo_email equals tipo.id_tipo_email
                        join edo in _unitOfWork.Db.Email_Estados on mail.id_estado equals edo.id_estado
                        join ac in _unitOfWork.Db.Transf_Solicitudes_AvisoCaducidad on mail.id_email equals ac.id_email into pleft_ac
                        from ac in pleft_ac.DefaultIfEmpty()
                        where mail.asunto.Contains(id_solicitud.ToString()) && ac.id_solicitud == id_solicitud || mail.html.Contains(id_solicitud.ToString()) && ac.id_solicitud == id_solicitud

                        orderby mail.id_email ascending
                        select new clsItemGrillaMails()
                        {
                            Mail_ID = mail.id_email.ToString(),
                            Mail_Estado = edo.descripcion,
                            Mail_Proceso = tipo.descripcion,
                            Mail_Asunto = mail.asunto,
                            Mail_Email = mail.email,
                            Mail_Fecha = (mail.fecha_envio == null) ? mail.fecha_alta : mail.fecha_envio,
                        }
                    ).ToList();

            var all = q.Union(qac);

            return all;
        }
    }
}
