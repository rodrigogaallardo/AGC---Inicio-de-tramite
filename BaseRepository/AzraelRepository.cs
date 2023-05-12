using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using System.Data.Entity.Core.Objects;
using StaticClass;
using DataAcess.EntityCustom;
using System.Text;
using System.Threading.Tasks;


namespace BaseRepository
{
    public class AzraelRepository : BaseRepository<SSIT_Solicitudes>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AzraelRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }

        public string GetUserNameByGuid(Guid UserID)
        {
            string UserName = "";
            var domains = _unitOfWork.Db.aspnet_Users.Where(x => x.UserId == UserID && x.aspnet_Applications.ApplicationName == "PortalHabilitaciones").FirstOrDefault();
            if (domains != null)
                UserName = domains.UserName;
            return UserName;
        }
        public IEnumerable<TramitesEntity> GetTramitesSGI(int id_tramite)
        {
            var lstTramites = (from ssit in _unitOfWork.Db.SSIT_Solicitudes
                               join edo in _unitOfWork.Db.TipoEstadoSolicitud on ssit.id_estado equals edo.Id
                               join tipo in _unitOfWork.Db.TipoTramite on ssit.id_tipotramite equals tipo.id_tipotramite
                               where ssit.id_solicitud == id_tramite
                               select new TramitesEntity
                               {
                                   IdTramite = ssit.id_solicitud,
                                   EstadoDescripcion = edo.Descripcion,
                                   IdTipoTramite = ssit.id_tipotramite,
                                   TipoTramiteDescripcion = tipo.descripcion_tipotramite
                               }).Union
                                (from transf in _unitOfWork.Db.Transf_Solicitudes
                                 join edo in _unitOfWork.Db.TipoEstadoSolicitud on transf.id_estado equals edo.Id
                                 join tipo in _unitOfWork.Db.TipoTramite on transf.id_tipotramite equals tipo.id_tipotramite
                                 where transf.id_solicitud == id_tramite
                                 select new TramitesEntity
                                 {
                                     IdTramite = transf.id_solicitud,
                                     EstadoDescripcion = edo.Descripcion,
                                     IdTipoTramite = transf.id_tipotramite,
                                     TipoTramiteDescripcion = tipo.descripcion_tipotramite
                                 }).Union
                                (from cpadron in _unitOfWork.Db.CPadron_Solicitudes
                                 join edo in _unitOfWork.Db.CPadron_Estados on cpadron.id_estado equals edo.id_estado
                                 join tipo in _unitOfWork.Db.TipoTramite on cpadron.id_tipotramite equals tipo.id_tipotramite
                                 where cpadron.id_cpadron == id_tramite
                                 select new TramitesEntity
                                 {
                                     IdTramite = cpadron.id_cpadron,
                                     EstadoDescripcion = edo.nom_estado_usuario,
                                     IdTipoTramite = cpadron.id_tipotramite,
                                     TipoTramiteDescripcion = tipo.descripcion_tipotramite
                                 });

            return lstTramites;
        }

        public IEnumerable<AzraelBuscadorFileEntity> GetTramitesFiles(int IdBuscador)
        {
            IEnumerable<AzraelBuscadorFileEntity> tramiteSSIT = (from sol in _unitOfWork.Db.SSIT_Solicitudes
                                                                 join doc in _unitOfWork.Db.SSIT_DocumentosAdjuntos on sol.id_solicitud equals doc.id_solicitud
                                                                 join fil in _unitOfWork.Db.Files on doc.id_file equals fil.id_file
                                                                 join tdr in _unitOfWork.Db.TiposDeDocumentosRequeridos on doc.id_tdocreq equals tdr.id_tdocreq
                                                                 join tds in _unitOfWork.Db.TiposDeDocumentosSistema on doc.id_tipodocsis equals tds.id_tipdocsis
                                                                 join est in _unitOfWork.Db.TipoEstadoSolicitud on sol.id_estado equals est.Id
                                                                 join ex in _unitOfWork.Db.TipoExpediente on sol.id_tipoexpediente equals ex.id_tipoexpediente
                                                                 join sex in _unitOfWork.Db.SubtipoExpediente on sol.id_subtipoexpediente equals sex.id_subtipoexpediente
                                                                 where sol.id_solicitud == IdBuscador || fil.id_file == IdBuscador
                                                                 select new AzraelBuscadorFileEntity
                                                                 {
                                                                     IdTramite = sol.id_solicitud,
                                                                     IdFile = fil.id_file,
                                                                     TipoTramite1 = "Habilitacion",
                                                                     TipoTramite2 = ex.descripcion_tipoexpediente + " " + sex.descripcion_subtipoexpediente,
                                                                     Estado = est.Descripcion,
                                                                     TipoDocReq = tdr.nombre_tdocreq,
                                                                     TipoDocSis = tds.nombre_tipodocsis,
                                                                     FileName = fil.FileName,
                                                                     idTipoTramite = (int)Constantes.AzraelTipoTramite.SSIT,
                                                                     id_docadjunto = doc.id_docadjunto
                                                                 });

            IEnumerable<AzraelBuscadorFileEntity> tramiteEnc = (from enc in _unitOfWork.Db.Encomienda
                                                                join doc in _unitOfWork.Db.Encomienda_DocumentosAdjuntos on enc.id_encomienda equals doc.id_encomienda
                                                                join fil in _unitOfWork.Db.Files on doc.id_file equals fil.id_file
                                                                join tdr in _unitOfWork.Db.TiposDeDocumentosRequeridos on doc.id_tdocreq equals tdr.id_tdocreq
                                                                join tds in _unitOfWork.Db.TiposDeDocumentosSistema on doc.id_tipodocsis equals tds.id_tipdocsis
                                                                join est in _unitOfWork.Db.Encomienda_Estados on enc.id_estado equals est.id_estado
                                                                join ex in _unitOfWork.Db.TipoExpediente on enc.id_tipoexpediente equals ex.id_tipoexpediente
                                                                join sex in _unitOfWork.Db.SubtipoExpediente on enc.id_subtipoexpediente equals sex.id_subtipoexpediente
                                                                where enc.id_encomienda == IdBuscador || fil.id_file == IdBuscador
                                                                select new AzraelBuscadorFileEntity
                                                                {
                                                                    IdTramite = enc.id_encomienda,
                                                                    IdFile = fil.id_file,
                                                                    TipoTramite1 = "Encomienda",
                                                                    TipoTramite2 = ex.descripcion_tipoexpediente + " " + sex.descripcion_subtipoexpediente,
                                                                    Estado = est.nom_estado,
                                                                    TipoDocReq = tdr.nombre_tdocreq,
                                                                    TipoDocSis = tds.nombre_tipodocsis,
                                                                    FileName = fil.FileName,
                                                                    idTipoTramite = (int)Constantes.AzraelTipoTramite.Encomienda,
                                                                    id_docadjunto = doc.id_docadjunto
                                                                });


            IEnumerable<AzraelBuscadorFileEntity> tramiteCPadron = (from cpadron in _unitOfWork.Db.CPadron_Solicitudes
                                                                    join doc in _unitOfWork.Db.CPadron_DocumentosAdjuntos on cpadron.id_cpadron equals doc.id_cpadron
                                                                    join fil in _unitOfWork.Db.Files on doc.id_file equals fil.id_file
                                                                    join tdr in _unitOfWork.Db.TiposDeDocumentosRequeridos on doc.id_tdocreq equals tdr.id_tdocreq
                                                                    join tds in _unitOfWork.Db.TiposDeDocumentosSistema on doc.id_tipodocsis equals tds.id_tipdocsis
                                                                    join est in _unitOfWork.Db.CPadron_Estados on cpadron.id_estado equals est.id_estado
                                                                    join ex in _unitOfWork.Db.TipoExpediente on cpadron.id_tipoexpediente equals ex.id_tipoexpediente
                                                                    join sex in _unitOfWork.Db.SubtipoExpediente on cpadron.id_subtipoexpediente equals sex.id_subtipoexpediente
                                                                    where cpadron.id_cpadron == IdBuscador || fil.id_file == IdBuscador
                                                                    select new AzraelBuscadorFileEntity
                                                                    {
                                                                        IdTramite = cpadron.id_cpadron,
                                                                        IdFile = fil.id_file,
                                                                        TipoTramite1 = "Consulta padron",
                                                                        TipoTramite2 = ex.descripcion_tipoexpediente + " " + sex.descripcion_subtipoexpediente,
                                                                        Estado = est.nom_estado_usuario,
                                                                        TipoDocReq = tdr.nombre_tdocreq,
                                                                        TipoDocSis = tds.nombre_tipodocsis,
                                                                        FileName = fil.FileName,
                                                                        idTipoTramite = (int)Constantes.AzraelTipoTramite.CPadron,
                                                                        id_docadjunto = doc.id_docadjunto
                                                                    });

            IEnumerable<AzraelBuscadorFileEntity> tramiteTransf = (from transf in _unitOfWork.Db.Transf_Solicitudes
                                                                   join doc in _unitOfWork.Db.Transf_DocumentosAdjuntos on transf.id_solicitud equals doc.id_solicitud
                                                                   join fil in _unitOfWork.Db.Files on doc.id_file equals fil.id_file
                                                                   join tdr in _unitOfWork.Db.TiposDeDocumentosRequeridos on doc.id_tdocreq equals tdr.id_tdocreq
                                                                   join tds in _unitOfWork.Db.TiposDeDocumentosSistema on doc.id_tipodocsis equals tds.id_tipdocsis
                                                                   join est in _unitOfWork.Db.TipoEstadoSolicitud on transf.id_estado equals est.Id
                                                                   join ex in _unitOfWork.Db.TipoExpediente on transf.id_tipoexpediente equals ex.id_tipoexpediente
                                                                   join sex in _unitOfWork.Db.SubtipoExpediente on transf.id_subtipoexpediente equals sex.id_subtipoexpediente
                                                                   where transf.id_solicitud == IdBuscador || fil.id_file == IdBuscador
                                                                   select new AzraelBuscadorFileEntity
                                                                   {
                                                                       IdTramite = transf.id_solicitud,
                                                                       IdFile = fil.id_file,
                                                                       TipoTramite1 = "Transferencia",
                                                                       TipoTramite2 = ex.descripcion_tipoexpediente + " " + sex.descripcion_subtipoexpediente,
                                                                       Estado = est.Descripcion,
                                                                       TipoDocReq = tdr.nombre_tdocreq,
                                                                       TipoDocSis = tds.nombre_tipodocsis,
                                                                       FileName = fil.FileName,
                                                                       idTipoTramite = (int)Constantes.AzraelTipoTramite.Transferencia,
                                                                       id_docadjunto = doc.id_docadjunto
                                                                   });


            IEnumerable<AzraelBuscadorFileEntity> tramiteSGIHAB = (from sol in _unitOfWork.Db.SSIT_Solicitudes
                                                                   join tth in _unitOfWork.Db.SGI_Tramites_Tareas_HAB on sol.id_solicitud equals tth.id_solicitud
                                                                   join doc in _unitOfWork.Db.SGI_Tarea_Documentos_Adjuntos on tth.id_tramitetarea equals doc.id_tramitetarea
                                                                   join fil in _unitOfWork.Db.Files on doc.id_file equals fil.id_file
                                                                   join tdr in _unitOfWork.Db.TiposDeDocumentosRequeridos on doc.id_tdocreq equals tdr.id_tdocreq
                                                                   join est in _unitOfWork.Db.TipoEstadoSolicitud on sol.id_estado equals est.Id
                                                                   join ex in _unitOfWork.Db.TipoExpediente on sol.id_tipoexpediente equals ex.id_tipoexpediente
                                                                   join sex in _unitOfWork.Db.SubtipoExpediente on sol.id_subtipoexpediente equals sex.id_subtipoexpediente
                                                                   where sol.id_solicitud == IdBuscador || fil.id_file == IdBuscador
                                                                   select new AzraelBuscadorFileEntity
                                                                   {
                                                                       IdTramite = sol.id_solicitud,
                                                                       IdFile = fil.id_file,
                                                                       TipoTramite1 = "Habilitacion",
                                                                       TipoTramite2 = ex.descripcion_tipoexpediente + " " + sex.descripcion_subtipoexpediente,
                                                                       Estado = est.Descripcion,
                                                                       TipoDocReq = tdr.nombre_tdocreq,
                                                                       TipoDocSis = "No definido",
                                                                       FileName = fil.FileName,
                                                                       idTipoTramite = (int)Constantes.AzraelTipoTramite.SGI_HAB,
                                                                       id_docadjunto = doc.id_doc_adj
                                                                   });

            IEnumerable<AzraelBuscadorFileEntity> lst = tramiteSSIT.Union(tramiteEnc).Union(tramiteTransf).Union(tramiteCPadron).Union(tramiteSGIHAB);//.Union(tramiteCAA);

            return lst;
        }

        public IEnumerable<TareasEntity> GetTareasSGI(int id_tramite, int id_tipotramite)
        {
            IEnumerable<TareasEntity> lstTareas = null;
            if ((int)Constantes.TipoTramite.TRANSFERENCIA == id_tipotramite)
            {
                lstTareas = (from tt in _unitOfWork.Db.SGI_Tramites_Tareas

                             join tt_hab in _unitOfWork.Db.SGI_Tramites_Tareas_TRANSF on tt.id_tramitetarea equals tt_hab.id_tramitetarea

                             join uC in _unitOfWork.Db.Usuario on tt_hab.Transf_Solicitudes.CreateUser equals uC.UserId into uCleftjoin
                             from userC in uCleftjoin.DefaultIfEmpty()

                             join aspu in _unitOfWork.Db.aspnet_Users on userC.UserId equals aspu.UserId into aspuleftoin
                             from asU in aspuleftoin.DefaultIfEmpty()

                             join p in _unitOfWork.Db.SGI_Profiles on tt.UsuarioAsignado_tramitetarea equals p.userid into pleftjoin
                             from prof in pleftjoin.DefaultIfEmpty()

                             where tt_hab.id_solicitud == id_tramite
                             orderby tt.FechaInicio_tramitetarea
                             select new TareasEntity
                             {
                                 Descripcion = tt.ENG_Tareas.nombre_tarea,
                                 FechaCreacion = tt.FechaInicio_tramitetarea,
                                 FechaAsignacion = tt.FechaAsignacion_tramtietarea,
                                 FechaFinalizacion = tt.FechaCierre_tramitetarea,
                                 UsuarioAsignado = tt.UsuarioAsignado_tramitetarea,
                                 UserName = (prof != null ? prof.aspnet_Users.UserName : (tt.ENG_Tareas.formulario_tarea == null ? asU.UserName : null)),
                                 ApenomUsuario = (prof != null ? prof.Nombres + " " + prof.Apellido : (tt.ENG_Tareas.formulario_tarea == null ? userC.Nombre + " " + userC.Apellido : null)),
                                 id_tramitetarea = tt.id_tramitetarea
                             });
            }
            if ((int)Constantes.TipoTramite.CONSULTA_PADRON == id_tipotramite)
            {
                lstTareas = (from tt in _unitOfWork.Db.SGI_Tramites_Tareas
                             
                             join tt_cp in _unitOfWork.Db.SGI_Tramites_Tareas_CPADRON on tt.id_tramitetarea equals tt_cp.id_tramitetarea

                             join uC in _unitOfWork.Db.Usuario on tt_cp.CPadron_Solicitudes.CreateUser equals uC.UserId into uCleftjoin
                             from userC in uCleftjoin.DefaultIfEmpty()

                             join aspu in _unitOfWork.Db.aspnet_Users on userC.UserId equals aspu.UserId into aspuleftoin
                             from asU in aspuleftoin.DefaultIfEmpty()

                             join p in _unitOfWork.Db.SGI_Profiles on tt.UsuarioAsignado_tramitetarea equals p.userid into pleftjoin
                             from prof in pleftjoin.DefaultIfEmpty()

                             where tt_cp.id_cpadron == id_tramite
                             orderby tt.FechaInicio_tramitetarea
                             select new TareasEntity
                             {
                                 Descripcion = tt.ENG_Tareas.nombre_tarea,
                                 FechaCreacion = tt.FechaInicio_tramitetarea,
                                 FechaAsignacion = tt.FechaAsignacion_tramtietarea,
                                 FechaFinalizacion = tt.FechaCierre_tramitetarea,
                                 UsuarioAsignado = tt.UsuarioAsignado_tramitetarea,
                                 UserName = (prof != null ? prof.aspnet_Users.UserName : (tt.ENG_Tareas.formulario_tarea == null ? asU.UserName : null)),
                                 ApenomUsuario = (prof != null ? prof.Nombres + " " + prof.Apellido : (tt.ENG_Tareas.formulario_tarea == null ? userC.Nombre + " " + userC.Apellido : null)),
                                 id_tramitetarea = tt.id_tramitetarea
                             });
            }
            if ((int)Constantes.TipoTramite.HABILITACION == id_tipotramite)
            {
                lstTareas = (from tt in _unitOfWork.Db.SGI_Tramites_Tareas

                             join tt_hab in _unitOfWork.Db.SGI_Tramites_Tareas_HAB on tt.id_tramitetarea equals tt_hab.id_tramitetarea

                             join uC in _unitOfWork.Db.Usuario on tt_hab.SSIT_Solicitudes.CreateUser equals uC.UserId into uCleftjoin
                             from userC in uCleftjoin.DefaultIfEmpty()

                             join aspu in _unitOfWork.Db.aspnet_Users on userC.UserId equals aspu.UserId into aspuleftoin
                             from asU in aspuleftoin.DefaultIfEmpty()

                             join p in _unitOfWork.Db.SGI_Profiles on tt.UsuarioAsignado_tramitetarea equals p.userid into pleftjoin
                             from prof in pleftjoin.DefaultIfEmpty()

                             where tt_hab.id_solicitud == id_tramite
                             orderby tt.FechaInicio_tramitetarea
                             select new TareasEntity
                             {
                                 Descripcion = tt.ENG_Tareas.nombre_tarea,
                                 FechaCreacion = tt.FechaInicio_tramitetarea,
                                 FechaAsignacion = tt.FechaAsignacion_tramtietarea,
                                 FechaFinalizacion = tt.FechaCierre_tramitetarea,
                                 UsuarioAsignado = tt.UsuarioAsignado_tramitetarea,
                                 UserName = (prof != null ? prof.aspnet_Users.UserName : (tt.ENG_Tareas.formulario_tarea == null ? asU.UserName : null)),
                                 ApenomUsuario = (prof != null ? prof.Nombres + " " + prof.Apellido : (tt.ENG_Tareas.formulario_tarea == null ? userC.Nombre + " " + userC.Apellido : null)),
                                 id_tramitetarea = tt.id_tramitetarea
                             });
            }
            return lstTareas;
        }

    }
}
