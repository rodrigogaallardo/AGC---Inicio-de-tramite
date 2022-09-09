using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using DataAcess.EntityCustom;
using Dal.UnitOfWork;
using static StaticClass.Constantes;

namespace BaseRepository
{
    public class TramitesRepository : BaseRepository<TramitesEntity>
    {
		private readonly IUnitOfWork _unitOfWork;

        public TramitesRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }

        public IEnumerable<TramitesEntity> GetTramites(Guid userid)
        {
            var lstTramites = (from ssit in _unitOfWork.Db.SSIT_Solicitudes
                               where ssit.CreateUser == userid
                               select new TramitesEntity
                               {
                                   IdTramite = ssit.id_solicitud,
                                   CodigoSeguridad = ssit.CodigoSeguridad,
                                   CreateDate = ssit.CreateDate,
                                   IdTipoTramite = ssit.id_tipotramite,
                                   TipoTramiteDescripcion = (
                                    !(bool)ssit.EsECI
                                    ?ssit.TipoTramite.descripcion_tipotramite
                                    :(ssit.id_tipotramite == (int)StaticClass.Constantes.TipoTramite.HabilitacionECIHabilitacion?TipoTramiteDescripcion.HabilitacionECI: TipoTramiteDescripcion.AdecuacionECI)
                                    ),
                                   TipoExpedienteDescripcion = ssit.TipoExpediente.descripcion_tipoexpediente,
                                   IdEstado = ssit.id_estado,
                                   EstadoDescripcion = ssit.TipoEstadoSolicitud.Descripcion,
                                   TipoExpediente = ssit.id_tipoexpediente,
                                   SubTipoExpediente = ssit.id_subtipoexpediente,
                                   SubTipoExpedienteDescripcion = ssit.SubtipoExpediente.descripcion_subtipoexpediente,
                                   Domicilio = "",
                                   Url = "",
                                   NroExpedienteSade = ssit.NroExpedienteSade
                               }).Concat(
                               (from transf in _unitOfWork.Db.Transf_Solicitudes
                                where transf.CreateUser == userid
                                select new TramitesEntity
                                {
                                    IdTramite = transf.id_solicitud,
                                    CodigoSeguridad = transf.CodigoSeguridad,
                                    CreateDate = transf.CreateDate,
                                    IdTipoTramite = transf.id_tipotramite,
                                    TipoTramiteDescripcion = transf.TipoTramite.descripcion_tipotramite,
                                    TipoExpedienteDescripcion = transf.TipoExpediente.descripcion_tipoexpediente,
                                    IdEstado = transf.id_estado,
                                    EstadoDescripcion = transf.TipoEstadoSolicitud.Descripcion,
                                    TipoExpediente = transf.id_tipoexpediente,
                                    SubTipoExpediente = transf.id_subtipoexpediente,
                                    SubTipoExpedienteDescripcion = transf.SubtipoExpediente.descripcion_subtipoexpediente,
                                    Domicilio = "",
                                    Url = "",
                                    NroExpedienteSade = transf.NroExpedienteSade
                                })).Concat(
                                (from cpadron in _unitOfWork.Db.CPadron_Solicitudes
                                 where cpadron.CreateUser == userid
                                 select new TramitesEntity
                                 {
                                     IdTramite = cpadron.id_cpadron,
                                     CodigoSeguridad = cpadron.CodigoSeguridad,
                                     CreateDate = cpadron.CreateDate,
                                     IdTipoTramite = cpadron.id_tipotramite,
                                     TipoTramiteDescripcion = cpadron.TipoTramite.descripcion_tipotramite,
                                     TipoExpedienteDescripcion = cpadron.TipoExpediente.descripcion_tipoexpediente,
                                     IdEstado = cpadron.id_estado,
                                     EstadoDescripcion = cpadron.CPadron_Estados.nom_estado_usuario,
                                     TipoExpediente = cpadron.id_tipoexpediente,
                                     SubTipoExpediente = cpadron.id_subtipoexpediente,
                                     SubTipoExpedienteDescripcion = cpadron.SubtipoExpediente.descripcion_subtipoexpediente,
                                     Domicilio = "",
                                     Url = "",
                                     NroExpedienteSade = ""
                                 }));

            return lstTramites;
        }

        public IEnumerable<TramitesEntity> GetTramitesNuevos(Guid userid)
        {
            var lstTramites = (from ssit in _unitOfWork.Db.SSIT_Solicitudes_Nuevas
                               where ssit.CreateUser == userid
                               select new TramitesEntity
                               {
                                   IdTramite = ssit.id_solicitud,
                                   CodigoSeguridad = ssit.CodigoSeguridad,
                                   CreateDate = ssit.CreateDate,
                                   IdTipoTramite = ssit.id_tipotramite,
                                   TipoTramiteDescripcion = ssit.TipoTramite.descripcion_tipotramite,
                                   TipoExpedienteDescripcion = "",
                                   IdEstado = ssit.id_estado,
                                   EstadoDescripcion = ssit.TipoEstadoSolicitud.Descripcion,
                                   TipoExpediente = 0,
                                   SubTipoExpediente = 0,
                                   SubTipoExpedienteDescripcion = "",
                                   Domicilio = ssit.Nombre_calle + " " + ssit.Altura_calle + " " + ssit.Piso + " " + ssit.UnidadFuncional,
                                   Url = "",
                                   NroExpedienteSade = ""
                               });

            return lstTramites;
        }

    }
}
