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
	public class TransferenciaUbicacionesBL : ITransferenciaUbicacionesBL<TransferenciaUbicacionesDTO>
    {               
		private TransferenciaUbicacionesRepository repo = null;        
        private ZonasPlaneamientoRepository repoZonaPlaneamiento = null;
        private TransferenciasUbicacionesPuertasRepository repoConsultarubicacionesPuertas =null;
        private TransferenciasUbicacionPropiedadHorizontalRepository repoConsultarubicacionesPropHorizontal = null;
        private TransferenciasSolicitudesRepository repoTransferenciaSolicitudes = null;
        private UbicacionesRepository repoUbic = null;
        private TransferenciaUbicacionesDistritosRepository repoDistrito = null;
        private TransferenciaUbicacionesMixturasRepository repoMixtura = null;
        private IUnitOfWorkFactory uowF = null;

        IMapper mapperBase;

        public TransferenciaUbicacionesBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Transf_Ubicaciones, TransferenciaUbicacionesDTO>()
                    .ForMember(dest => dest.IdTransferenciaUbicacion, source => source.MapFrom(p => p.id_transfubicacion))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdUbicacion, source => source.MapFrom(p => p.id_ubicacion))
                    .ForMember(dest => dest.IdSubTipoUbicacion, source => source.MapFrom(p => p.id_subtipoubicacion))
                    .ForMember(dest => dest.LocalSubTipoUbicacion, source => source.MapFrom(p => p.local_subtipoubicacion))
                    .ForMember(dest => dest.DeptoLocalTransferenciaUbicacion, source => source.MapFrom(p => p.deptoLocal_transfubicacion))
                    .ForMember(dest => dest.IdZonaPlaneamiento, source => source.MapFrom(p => p.id_zonaplaneamiento))
                    .ForMember(dest => dest.PropiedadesHorizontales, source => source.MapFrom(p => p.Transf_Ubicaciones_PropiedadHorizontal))
                    .ForMember(dest => dest.Puertas, source => source.MapFrom(p => p.Transf_Ubicaciones_Puertas))
                    .ForMember(dest => dest.ZonaPlaneamiento, source => source.MapFrom(p => p.Zonas_Planeamiento))
                    .ForMember(dest => dest.Ubicacion, source => source.MapFrom(p => p.Ubicaciones))
                    .ForMember(dest => dest.SubTipoUbicacion, source => source.MapFrom(p => p.SubTiposDeUbicacion))
                    .ForMember(dest => dest.Torre, source => source.MapFrom(p => p.Torre))
                    .ForMember(dest => dest.Depto, source => source.MapFrom(p => p.Depto))
                    .ForMember(dest => dest.Local, source => source.MapFrom(p => p.Local))
                    .ForMember(dest => dest.TransferenciaUbicacionesDistritosDTO, source => source.MapFrom(p => p.Transf_Ubicaciones_Distritos))
                    .ForMember(dest => dest.TransferenciaUbicacionesMixturasDTO, source => source.MapFrom(p => p.Transf_Ubicaciones_Mixturas));

                cfg.CreateMap<TransferenciaUbicacionesDTO, Transf_Ubicaciones>()                
                    .ForMember(dest => dest.id_transfubicacion, source => source.MapFrom(p => p.IdTransferenciaUbicacion))
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_ubicacion, source => source.MapFrom(p => p.IdUbicacion))
                    .ForMember(dest => dest.id_subtipoubicacion, source => source.MapFrom(p => p.IdSubTipoUbicacion))
                    .ForMember(dest => dest.local_subtipoubicacion, source => source.MapFrom(p => p.LocalSubTipoUbicacion))
                    .ForMember(dest => dest.deptoLocal_transfubicacion, source => source.MapFrom(p => p.DeptoLocalTransferenciaUbicacion))
                    .ForMember(dest => dest.id_zonaplaneamiento, source => source.MapFrom(p => p.IdZonaPlaneamiento))
                    .ForMember(dest => dest.Transf_Ubicaciones_PropiedadHorizontal, source => source.MapFrom(p => p.PropiedadesHorizontales))
                    .ForMember(dest => dest.Transf_Ubicaciones_Puertas, source => source.MapFrom(p => p.Puertas))
                    .ForMember(dest => dest.Torre, source => source.MapFrom(p => p.Torre))
                    .ForMember(dest => dest.Depto, source => source.MapFrom(p => p.Depto))
                    .ForMember(dest => dest.Local, source => source.MapFrom(p => p.Local))
                    .ForMember(dest => dest.Transf_Ubicaciones_Distritos, source => source.MapFrom(p => p.TransferenciaUbicacionesDistritosDTO))
                    .ForMember(dest => dest.Transf_Ubicaciones_Mixturas, source => source.MapFrom(p => p.TransferenciaUbicacionesMixturasDTO));


                cfg.CreateMap<Transf_Ubicaciones_Puertas, TransferenciasUbicacionesPuertasDTO>()
                   .ForMember(dest => dest.IdTranferenciaPuerta, source => source.MapFrom(p => p.id_transfpuerta))
                   .ForMember(dest => dest.IdTranferenciaUbicacion, source => source.MapFrom(p => p.id_transfubicacion))
                   .ForMember(dest => dest.CodigoCalle, source => source.MapFrom(p => p.codigo_calle))
                   .ForMember(dest => dest.NombreCalle, source => source.MapFrom(p => p.nombre_calle))
                   .ForMember(dest => dest.NumeroPuerta, source => source.MapFrom(p => p.NroPuerta));

                cfg.CreateMap<TransferenciasUbicacionesPuertasDTO, Transf_Ubicaciones_Puertas>()
                  .ForMember(dest => dest.id_transfpuerta, source => source.MapFrom(p => p.IdTranferenciaPuerta))
                  .ForMember(dest => dest.id_transfubicacion, source => source.MapFrom(p => p.IdTranferenciaUbicacion))
                  .ForMember(dest => dest.codigo_calle, source => source.MapFrom(p => p.CodigoCalle))
                  .ForMember(dest => dest.nombre_calle, source => source.MapFrom(p => p.NombreCalle))
                  .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NumeroPuerta));

                cfg.CreateMap<Transf_Ubicaciones_PropiedadHorizontal, TransferenciasUbicacionPropiedadHorizontalDTO>()
                   .ForMember(dest => dest.IdTranferenciaPropiedadHorizontal, source => source.MapFrom(p => p.id_transfprophorizontal))
                   .ForMember(dest => dest.IdTranferenciaUbicacion, source => source.MapFrom(p => p.id_transfprophorizontal))
                   .ForMember(dest => dest.IdPropiedadHorizontal, source => source.MapFrom(p => p.id_propiedadhorizontal))           
                   .ForMember(dest => dest.UbicacionPropiedadaHorizontal, source => source.MapFrom(p => p.Ubicaciones_PropiedadHorizontal));
                   //.ForMember(dest => dest.UbicacionPropiedadaHorizontal, source => source.MapFrom(p => p.Ubicaciones_PropiedadHorizontal));

                cfg.CreateMap<TransferenciasUbicacionPropiedadHorizontalDTO, Transf_Ubicaciones_PropiedadHorizontal>()
                   .ForMember(dest => dest.id_transfprophorizontal, source => source.MapFrom(p => p.IdTranferenciaPropiedadHorizontal))
                   .ForMember(dest => dest.id_transfubicacion, source => source.MapFrom(p => p.IdTranferenciaUbicacion))
                   .ForMember(dest => dest.id_propiedadhorizontal, source => source.MapFrom(p => p.IdPropiedadHorizontal));

                #region mixturas
                cfg.CreateMap<Transf_Ubicaciones_Mixturas, TransferenciaUbicacionesMixturasDTO>();

                cfg.CreateMap<TransferenciaUbicacionesMixturasDTO, Transf_Ubicaciones_Mixturas>();
                #endregion

                #region distritos
                cfg.CreateMap<Transf_Ubicaciones_Distritos, TransferenciaUbicacionesDistritosDTO>();

                cfg.CreateMap<TransferenciaUbicacionesDistritosDTO, Transf_Ubicaciones_Distritos>();
                #endregion

                cfg.CreateMap<Zonas_Planeamiento, ZonasPlaneamientoDTO>()
                    .ForMember(dest => dest.IdZonaPlaneamiento, source => source.MapFrom(p => p.id_zonaplaneamiento));

                cfg.CreateMap<Ubicaciones, UbicacionesDTO>()
                    .ForMember(dest => dest.IdUbicacion, source => source.MapFrom(p => p.id_ubicacion))
                    .ForMember(dest => dest.IdSubtipoUbicacion, source => source.MapFrom(p => p.id_subtipoubicacion))
                    .ForMember(dest => dest.IdZonaPlaneamiento, source => source.MapFrom(p => p.id_zonaplaneamiento))
                    .ForMember(dest => dest.CoordenadaX, source => source.MapFrom(p => p.Coordenada_X))
                    .ForMember(dest => dest.CoordenadaY, source => source.MapFrom(p => p.Coordenada_Y))
                    .ForMember(dest => dest.InhibidaObservacion, source => source.MapFrom(p => p.Inhibida_Observacion))
                    .ForMember(dest => dest.BajaLogica, source => source.MapFrom(p => p.baja_logica))
                    .ForMember(dest => dest.PisosBajoRasante, source => source.MapFrom(p => p.pisos_bajo_rasante))
                    .ForMember(dest => dest.PisosSobreRasante, source => source.MapFrom(p => p.pisos_sobre_rasante))
                    .ForMember(dest => dest.Unidades, source => source.MapFrom(p => p.unidades))
                    .ForMember(dest => dest.Locales, source => source.MapFrom(p => p.locales))
                    .ForMember(dest => dest.CantPh, source => source.MapFrom(p => p.cant_ph))
                    .ForMember(dest => dest.Vuc, source => source.MapFrom(p => p.vuc))
                    .ForMember(dest => dest.IdComuna, source => source.MapFrom(p => p.id_comuna))
                    .ForMember(dest => dest.IdBarrio, source => source.MapFrom(p => p.id_barrio))
                    .ForMember(dest => dest.IdAreaHospitalaria, source => source.MapFrom(p => p.id_areahospitalaria))
                    .ForMember(dest => dest.IdComisaria, source => source.MapFrom(p => p.id_comisaria))
                    .ForMember(dest => dest.IdDistritoEscolar, source => source.MapFrom(p => p.id_distritoescolar))
                    .ForMember(dest => dest.FechaUltimaActualizacionUsig, source => source.MapFrom(p => p.FechaUltimaActualizacionUSIG))
                    .ForMember(dest => dest.CantiActualizacionesUsig, source => source.MapFrom(p => p.CantiActualizacionesUSIG))
                    .ForMember(dest => dest.TipoPersonaTitularAgip, source => source.MapFrom(p => p.TipoPersonaTitularAGIP))
                    .ForMember(dest => dest.TitularAgip, source => source.MapFrom(p => p.TitularAGIP))
                    .ForMember(dest => dest.FechaAltaAgip, source => source.MapFrom(p => p.FechaAltaAGIP))
                    .ForMember(dest => dest.ZonasPlaneamiento, source => source.MapFrom(p => p.Zonas_Planeamiento))
                    .ForMember(dest => dest.UbicacionesPuertas, source => source.MapFrom(p => p.Ubicaciones_Puertas));

                cfg.CreateMap<Ubicaciones_Puertas, UbicacionesPuertasDTO>()
                    .ForMember(dest => dest.IdUbicacion, source => source.MapFrom(p => p.id_ubicacion))
                    .ForMember(dest => dest.CodigoCalle, source => source.MapFrom(p => p.codigo_calle))
                    .ForMember(dest => dest.IdUbicacionPuerta, source => source.MapFrom(p => p.id_ubic_puerta))
                    .ForMember(dest => dest.NroPuertaUbic, source => source.MapFrom(p => p.NroPuerta_ubic))
                    .ForMember(dest => dest.TipoPuerta, source => source.MapFrom(p => p.tipo_puerta));


                cfg.CreateMap<SubTiposDeUbicacion, SubTipoUbicacionesDTO>()
                    .ForMember(dest => dest.TiposDeUbicacionDTO, source => source.MapFrom(p => p.TiposDeUbicacion));

                cfg.CreateMap<TiposDeUbicacion, TiposDeUbicacionDTO>()
                    .ForMember(dest => dest.IdTipoUbicacion, source => source.MapFrom(p => p.id_tipoubicacion))
                    .ForMember(dest => dest.DescripcionTipoUbicacion, source => source.MapFrom(p => p.descripcion_tipoubicacion));

                cfg.CreateMap<Ubicaciones_PropiedadHorizontal, UbicacionesPropiedadhorizontalDTO>()
                       .ForMember(dest => dest.IdPropiedadHorizontal, source => source.MapFrom(p => p.id_propiedadhorizontal))
                    .ForMember(dest => dest.IdUbicacion, source => source.MapFrom(p => p.id_ubicacion));

                cfg.CreateMap<UbicacionesPropiedadhorizontalDTO, Ubicaciones_PropiedadHorizontal>()
                   .ForMember(dest => dest.id_propiedadhorizontal, source => source.MapFrom(p => p.IdPropiedadHorizontal))
                   .ForMember(dest => dest.id_ubicacion, source => source.MapFrom(p => p.IdUbicacion));

                cfg.CreateMap<Transf_Solicitudes, TransferenciasSolicitudesDTO>()
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                    .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.id_tipotramite))
                    .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.id_tipoexpediente))
                    .ForMember(dest => dest.IdSubTipoExpediente, source => source.MapFrom(p => p.id_subtipoexpediente))
                    .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                    .ForMember(dest => dest.Observaciones, source => source.MapFrom(p => p.Transf_Solicitudes_Observaciones))                    
                    .ForMember(dest => dest.NumeroExpedienteSade, source => source.MapFrom(p => p.NroExpedienteSade))
                    .ForMember(dest => dest.CodigoSeguridad, source => source.MapFrom(p => p.CodigoSeguridad))
                    .ForMember(dest => dest.idTAD, source => source.MapFrom(p => p.idTAD))
                    .ForMember(dest => dest.TipoTramite, source => source.Ignore())
                    .ForMember(dest => dest.TipoExpediente, source => source.Ignore())
                    .ForMember(dest => dest.SubTipoExpediente, source => source.Ignore())
                    .ForMember(dest => dest.Estado, source => source.Ignore())
                    .ForMember(dest => dest.Direccion, source => source.Ignore())
                    .ForMember(dest => dest.Ubicaciones, source => source.MapFrom(p => p.Transf_Ubicaciones));


        cfg.CreateMap<TransferenciasSolicitudesDTO, Transf_Solicitudes>()
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_cpadron, source => source.MapFrom(p => p.IdConsultaPadron))
                    .ForMember(dest => dest.id_tipotramite, source => source.MapFrom(p => p.IdTipoTramite))
                    .ForMember(dest => dest.id_tipoexpediente, source => source.MapFrom(p => p.IdTipoExpediente))
                    .ForMember(dest => dest.id_subtipoexpediente, source => source.MapFrom(p => p.IdSubTipoExpediente))
                    .ForMember(dest => dest.id_estado, source => source.MapFrom(p => p.IdEstado))
                    .ForMember(dest => dest.Transf_Solicitudes_Observaciones, source => source.MapFrom(p => p.Observaciones))
                    .ForMember(dest => dest.NroExpedienteSade, source => source.MapFrom(p => p.NumeroExpedienteSade))
                    .ForMember(dest => dest.CodigoSeguridad, source => source.MapFrom(p => p.CodigoSeguridad))
                    .ForMember(dest => dest.idTAD, source => source.MapFrom(p => p.idTAD))
                    .ForMember(dest => dest.TipoTramite, source => source.Ignore())
                    .ForMember(dest => dest.TipoExpediente, source => source.Ignore())
                    .ForMember(dest => dest.SubtipoExpediente, source => source.Ignore())
                    .ForMember(dest => dest.Transf_Ubicaciones, source => source.MapFrom(p => p.Ubicaciones));

            });
            mapperBase = config.CreateMapper();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdConsultaPadronUbicacion"></param>
        /// <returns></returns>
		public TransferenciaUbicacionesDTO Single(int IdTransfUbicacion )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciaUbicacionesRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdTransfUbicacion);
                var entityDto = mapperBase.Map<Transf_Ubicaciones, TransferenciaUbicacionesDTO>(entity);
     
                return entityDto;
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
        public bool Insert(TransferenciaUbicacionesDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork();
 
                repoTransferenciaSolicitudes = new TransferenciasSolicitudesRepository(unitOfWork);
                repo = new TransferenciaUbicacionesRepository(unitOfWork);
                repoUbic = new UbicacionesRepository(unitOfWork);

                var transferenciaSolicitudesEntity = repoTransferenciaSolicitudes.Single(objectDto.IdSolicitud.Value);
                var ubicacionEntity = repoUbic.Single(objectDto.IdUbicacion.Value);

                //if (transferenciaSolicitudesEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && transferenciaSolicitudesEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
                //    throw new Exception(Errors.SSIT_CPADRON_NO_ADMITE_CAMBIOS);
                
                //if (ubicacionEntity.id_zonaplaneamiento == 0)
                //    throw new Exception();

                var inhibidasEntity = ubicacionEntity.Ubicaciones_Inhibiciones;

                if (inhibidasEntity.Any(p => !p.fecha_vencimiento.HasValue || (DateTime.Now >= p.fecha_inhibicion && DateTime.Now <= p.fecha_vencimiento.Value)))
                    throw new Exception(Errors.UBICACION_INHIBIDA);

                var transfUbicEntity = transferenciaSolicitudesEntity.Transf_Ubicaciones.Where(x => x.id_ubicacion == objectDto.IdUbicacion.Value);

                if (transfUbicEntity.Count() > 0)
                    throw new Exception(Errors.UBICACION_IGUAL);

                CallesBL callesBL = new CallesBL();
                foreach (var puerta in objectDto.Puertas)
                    puerta.NombreCalle = callesBL.GetNombre(puerta.CodigoCalle, puerta.NumeroPuerta);

                var transfEntityInsert = mapperBase.Map<TransferenciaUbicacionesDTO, Transf_Ubicaciones>(objectDto);
                transfEntityInsert.id_zonaplaneamiento = ubicacionEntity.id_zonaplaneamiento;
                var consultaPadronSolicitudesDTO = mapperBase.Map<Transf_Solicitudes, TransferenciasSolicitudesDTO>(transferenciaSolicitudesEntity);

                Transf_Solicitudes transfSolicitudesEntityUpdate = null;
               /* if (string.IsNullOrEmpty(consultaPadronSolicitudesEntity.ZonaDeclarada))
                {
                    var Zona = ubicacionEntity.Zonas_Planeamiento;
                    if (!string.IsNullOrEmpty(Zona.CodZonaPla))
                    {
                        consultaPadronSolicitudesDTO.ZonaDeclarada = Zona.CodZonaPla;
                        consultaPadronSolicitudesEntityUpdate = mapperBase.Map<ConsultaPadronSolicitudesDTO, CPadron_Solicitudes>(consultaPadronSolicitudesDTO);  
                    }
                }*/

                using (IUnitOfWork unitOfWorkTran = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new TransferenciaUbicacionesRepository(unitOfWorkTran);

                    repo.Insert(transfEntityInsert);
                    if (transfSolicitudesEntityUpdate != null)
                    {
                        repoTransferenciaSolicitudes = new TransferenciasSolicitudesRepository(unitOfWorkTran);
                        repoTransferenciaSolicitudes.Update(transfSolicitudesEntityUpdate);
                    }

                    unitOfWorkTran.Commit();
                }
                return true;
                
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
		public void Delete(TransferenciaUbicacionesDTO objectDto)
		{
		    try
		    {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new TransferenciaUbicacionesRepository(unitOfWork);
                    repoConsultarubicacionesPuertas = new TransferenciasUbicacionesPuertasRepository(unitOfWork);
                    repoTransferenciaSolicitudes = new TransferenciasSolicitudesRepository(unitOfWork);
                    repoDistrito = new TransferenciaUbicacionesDistritosRepository(unitOfWork);
                    repoMixtura = new TransferenciaUbicacionesMixturasRepository(unitOfWork);

                    var transferenciaUbicaciones = repo.Single(objectDto.IdTransferenciaUbicacion);

                    //if (transferenciaUbicaciones.Transf_Solicitudes.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && transferenciaUbicaciones.Transf_Solicitudes.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
                    //    throw new Exception(Errors.SSIT_CPADRON_NO_ADMITE_CAMBIOS);

                    repoConsultarubicacionesPropHorizontal = new TransferenciasUbicacionPropiedadHorizontalRepository(unitOfWork);                    
                    if(transferenciaUbicaciones.Transf_Ubicaciones_PropiedadHorizontal.Any())
                        repoConsultarubicacionesPropHorizontal.DeleteRange(transferenciaUbicaciones.Transf_Ubicaciones_PropiedadHorizontal);

                    repoConsultarubicacionesPuertas.DeleteRange(transferenciaUbicaciones.Transf_Ubicaciones_Puertas);
                    repoMixtura.RemoveRange(transferenciaUbicaciones.Transf_Ubicaciones_Mixturas);
                    repoDistrito.RemoveRange(transferenciaUbicaciones.Transf_Ubicaciones_Distritos);

                    var transferencia = transferenciaUbicaciones.Transf_Solicitudes;
                    repo.Delete(transferenciaUbicaciones);

                    var transferenciaUbicacionesEntity = transferencia.Transf_Ubicaciones;

                    if (!transferenciaUbicacionesEntity.Any())                    
                    {
                        
                        repoTransferenciaSolicitudes.Update(transferencia);
                    }
                    else
                    {                                                
                        repoZonaPlaneamiento = new ZonasPlaneamientoRepository(unitOfWork);
                        var zona1 = transferencia.Transf_Ubicaciones.Select(p => p.Ubicaciones).Select(p => p.Zonas_Planeamiento);
                        //var zona2 = repoZonaPlaneamiento.GetZonaComplementariaTransferencia(transferencia.id_solicitud);
                        //var zonas = zona1.Concat(zona2);

                       /* if (!string.IsNullOrEmpty(transferencia.ZonaDeclarada))
                        {
                            var zonaNueva = zonas.FirstOrDefault().CodZonaPla;
                            if (!zonas.Any(p => p.CodZonaPla.Equals(consultaPadron.ZonaDeclarada)))
                            {
                                consultaPadron.ZonaDeclarada = zonaNueva;
                                if (string.IsNullOrEmpty(zonaNueva))
                                    consultaPadron.ZonaDeclarada = null;
                                
                                repoConsultaPadronSolicitudes.Update(consultaPadron);
                            }
                        }*/
                    }                
		            unitOfWork.Commit();
		        }
		    }
		    catch (Exception ex)
		    {
		        throw ex;
		    }
		}

        public IEnumerable<TransferenciaUbicacionesDTO> GetByFKIdSolicitud(int IdSolicitud)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new TransferenciaUbicacionesRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdSolicitud(IdSolicitud);
            var elementsDto = mapperBase.Map<IEnumerable<Transf_Ubicaciones>, IEnumerable<TransferenciaUbicacionesDTO>>(elements);
            return elementsDto;
        }
        #endregion
        public void copiarUbicacionToTR(EncomiendaDTO encDTO, Guid userId)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new TransferenciaUbicacionesRepository(unitOfWork);
                    repoConsultarubicacionesPuertas = new TransferenciasUbicacionesPuertasRepository(unitOfWork);
                    repoConsultarubicacionesPropHorizontal = new TransferenciasUbicacionPropiedadHorizontalRepository(unitOfWork);
                    repoMixtura = new TransferenciaUbicacionesMixturasRepository(unitOfWork);
                    repoDistrito = new TransferenciaUbicacionesDistritosRepository(unitOfWork);

                    int id_solicitud = encDTO.IdSolicitud;//EncomiendaSSITSolicitudesDTO.Select(x => x.id_solicitud).FirstOrDefault();
                    var TRUbiEntity = repo.GetByFKIdSolicitud(id_solicitud).ToList();
                    var lstEncUbicDTO = encDTO.EncomiendaUbicacionesDTO;

                    foreach (var ubic in TRUbiEntity)
                    {
                        var lstPuertas = ubic.Transf_Ubicaciones_Puertas.ToList();
                        foreach (var puerta in lstPuertas)
                            repoConsultarubicacionesPuertas.Delete(puerta);

                        var lstPH = ubic.Transf_Ubicaciones_PropiedadHorizontal.ToList();
                        foreach (var ph in lstPH)
                            repoConsultarubicacionesPropHorizontal.Delete(ph);

                        var lstMixturas = ubic.Transf_Ubicaciones_Mixturas.ToList();
                        foreach (var mixturas in lstMixturas)
                            repoMixtura.Delete(mixturas);

                        var lstDistritos = ubic.Transf_Ubicaciones_Distritos.ToList();
                        foreach (var distritos in lstDistritos)
                            repoDistrito.Delete(distritos);

                        repo.Delete(ubic);
                    }

                    TransferenciaUbicacionesDTO TRUbicDTO = null;
                    TransferenciasUbicacionesPuertasDTO TRUbicPuertaDTO = null;
                    TransferenciasUbicacionPropiedadHorizontalDTO TRUbicPHDTO = null;
                    TransferenciaUbicacionesDistritosDTO TRUbicDistritoDTO = null;
                    TransferenciaUbicacionesMixturasDTO TRUbicMixturaDTO = null;

                    foreach (var encUbic in lstEncUbicDTO)
                    {
                        TRUbicDTO = new TransferenciaUbicacionesDTO();

                        TRUbicDTO.IdSolicitud = id_solicitud;
                        TRUbicDTO.IdUbicacion = encUbic.IdUbicacion;
                        TRUbicDTO.IdSubTipoUbicacion = encUbic.IdSubtipoUbicacion;
                        TRUbicDTO.LocalSubTipoUbicacion = encUbic.LocalSubtipoUbicacion;
                        TRUbicDTO.DeptoLocalTransferenciaUbicacion = encUbic.DeptoLocalEncomiendaUbicacion;
                        TRUbicDTO.CreateDate = DateTime.Now;
                        TRUbicDTO.CreateUser = userId;
                        TRUbicDTO.IdZonaPlaneamiento = encUbic.IdZonaPlaneamiento;
                        TRUbicDTO.Depto = encUbic.Depto;
                        TRUbicDTO.Local = encUbic.Local;
                        TRUbicDTO.Torre = encUbic.Torre;

                        var ubicEntity = mapperBase.Map<TransferenciaUbicacionesDTO, Transf_Ubicaciones>(TRUbicDTO);
                        repo.Insert(ubicEntity);

                        foreach (var encPuerta in encUbic.EncomiendaUbicacionesPuertasDTO)
                        {
                            TRUbicPuertaDTO = new TransferenciasUbicacionesPuertasDTO();

                            TRUbicPuertaDTO.IdTranferenciaUbicacion = ubicEntity.id_transfubicacion;
                            TRUbicPuertaDTO.CodigoCalle = encPuerta.CodigoCalle;
                            TRUbicPuertaDTO.NombreCalle = encPuerta.NombreCalle;
                            TRUbicPuertaDTO.NumeroPuerta = encPuerta.NroPuerta;

                            var puertaEntity = mapperBase.Map<TransferenciasUbicacionesPuertasDTO, Transf_Ubicaciones_Puertas>(TRUbicPuertaDTO);
                            repoConsultarubicacionesPuertas.Insert(puertaEntity);
                        }

                        foreach (var encPH in encUbic.EncomiendaUbicacionesPropiedadHorizontalDTO)
                        {
                            TRUbicPHDTO = new TransferenciasUbicacionPropiedadHorizontalDTO();

                            TRUbicPHDTO.IdTranferenciaUbicacion = ubicEntity.id_transfubicacion;
                            TRUbicPHDTO.IdPropiedadHorizontal = encPH.IdPropiedadHorizontal;

                            var puertaEntity = mapperBase.Map<TransferenciasUbicacionPropiedadHorizontalDTO, Transf_Ubicaciones_PropiedadHorizontal>(TRUbicPHDTO);
                            repoConsultarubicacionesPropHorizontal.Insert(puertaEntity);
                        }

                        foreach (var encMixtura in encUbic.EncomiendaUbicacionesMixturasDTO)
                        {
                            TRUbicMixturaDTO = new TransferenciaUbicacionesMixturasDTO();

                            TRUbicMixturaDTO.id_transfubicacion = ubicEntity.id_transfubicacion;
                            TRUbicMixturaDTO.IdZonaMixtura = encMixtura.IdZonaMixtura;

                            var mixturaEntity = mapperBase.Map<TransferenciaUbicacionesMixturasDTO, Transf_Ubicaciones_Mixturas>(TRUbicMixturaDTO);
                            repoMixtura.Insert(mixturaEntity);
                        }

                        foreach (var encDistrito in encUbic.EncomiendaUbicacionesDistritosDTO)
                        {
                            TRUbicDistritoDTO = new TransferenciaUbicacionesDistritosDTO();

                            TRUbicDistritoDTO.id_transfubicacion = ubicEntity.id_transfubicacion;
                            TRUbicDistritoDTO.IdDistrito = encDistrito.IdDistrito;
                            TRUbicDistritoDTO.IdZona = encDistrito.IdZona;
                            TRUbicDistritoDTO.IdSubZona = encDistrito.IdSubZona;

                            var distritoEntity = mapperBase.Map<TransferenciaUbicacionesDistritosDTO, Transf_Ubicaciones_Distritos>(TRUbicDistritoDTO);
                            repoDistrito.Insert(distritoEntity);
                        }
                    }
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

