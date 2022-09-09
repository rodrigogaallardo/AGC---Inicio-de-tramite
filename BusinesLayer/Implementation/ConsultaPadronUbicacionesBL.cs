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
	public class ConsultaPadronUbicacionesBL : IConsultaPadronUbicacionesBL<ConsultaPadronUbicacionesDTO>
    {               
		private ConsultaPadronUbicacionesRepository repo = null;        
        private ZonasPlaneamientoRepository repoZonaPlaneamiento = null;
        private ConsultaPadronUbicacionesPuertasRepository repoConsultarubicacionesPuertas=null;
        private ConsultaPadronUbicacionPropiedadHorizontalRepository repoConsultarubicacionesPropHorizontal = null;
        private ConsultaPadronSolicitudesRepository repoConsultaPadronSolicitudes = null;
        private UbicacionesRepository repoUbic = null;
        private IUnitOfWorkFactory uowF = null;

        IMapper mapperBase;

        public ConsultaPadronUbicacionesBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CPadron_Ubicaciones, ConsultaPadronUbicacionesDTO>()
                    .ForMember(dest => dest.IdConsultaPadronUbicacion, source => source.MapFrom(p => p.id_cpadronubicacion))
                    .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                    .ForMember(dest => dest.IdUbicacion, source => source.MapFrom(p => p.id_ubicacion))
                    .ForMember(dest => dest.IdSubTipoUbicacion, source => source.MapFrom(p => p.id_subtipoubicacion))
                    .ForMember(dest => dest.LocalSubTipoUbicacion, source => source.MapFrom(p => p.local_subtipoubicacion))
                    .ForMember(dest => dest.DeptoLocalConsultaPadronUbicacion, source => source.MapFrom(p => p.deptoLocal_cpadronubicacion))
                    .ForMember(dest => dest.IdZonaPlaneamiento, source => source.MapFrom(p => p.id_zonaplaneamiento))
                    .ForMember(dest => dest.PropiedadesHorizontales, source => source.MapFrom(p => p.CPadron_Ubicaciones_PropiedadHorizontal))
                    .ForMember(dest => dest.Puertas, source => source.MapFrom(p => p.CPadron_Ubicaciones_Puertas))
                    .ForMember(dest => dest.ZonaPlaneamiento, source => source.MapFrom(p => p.Zonas_Planeamiento))
                    .ForMember(dest => dest.Ubicacion, source => source.MapFrom(p => p.Ubicaciones))
                    .ForMember(dest => dest.SubTipoUbicacion, source => source.MapFrom(p => p.SubTiposDeUbicacion))
                    .ForMember(dest => dest.Torre, source => source.MapFrom(p => p.Torre))
                    .ForMember(dest => dest.Depto, source => source.MapFrom(p => p.Depto))
                    .ForMember(dest => dest.Local, source => source.MapFrom(p => p.Local));

                cfg.CreateMap<ConsultaPadronUbicacionesDTO, CPadron_Ubicaciones>()                
                    .ForMember(dest => dest.id_cpadronubicacion, source => source.MapFrom(p => p.IdConsultaPadronUbicacion))
                    .ForMember(dest => dest.id_cpadron, source => source.MapFrom(p => p.IdConsultaPadron))
                    .ForMember(dest => dest.id_ubicacion, source => source.MapFrom(p => p.IdUbicacion))
                    .ForMember(dest => dest.id_subtipoubicacion, source => source.MapFrom(p => p.IdSubTipoUbicacion))
                    .ForMember(dest => dest.local_subtipoubicacion, source => source.MapFrom(p => p.LocalSubTipoUbicacion))
                    .ForMember(dest => dest.deptoLocal_cpadronubicacion, source => source.MapFrom(p => p.DeptoLocalConsultaPadronUbicacion))
                    .ForMember(dest => dest.id_zonaplaneamiento, source => source.MapFrom(p => p.IdZonaPlaneamiento))
                    .ForMember(dest => dest.CPadron_Ubicaciones_PropiedadHorizontal, source => source.MapFrom(p => p.PropiedadesHorizontales))
                    .ForMember(dest => dest.CPadron_Ubicaciones_Puertas, source => source.MapFrom(p => p.Puertas))
                    .ForMember(dest => dest.Torre, source => source.MapFrom(p => p.Torre))
                    .ForMember(dest => dest.Depto, source => source.MapFrom(p => p.Depto))
                    .ForMember(dest => dest.Local, source => source.MapFrom(p => p.Local));


                cfg.CreateMap<CPadron_Ubicaciones_Puertas, ConsultaPadronUbicacionesPuertasDTO>()
                   .ForMember(dest => dest.IdConsultaPadronPuerta, source => source.MapFrom(p => p.id_cpadronpuerta))
                   .ForMember(dest => dest.IdConsultaPadronUbicacion, source => source.MapFrom(p => p.id_cpadronubicacion))
                   .ForMember(dest => dest.CodigoCalle, source => source.MapFrom(p => p.codigo_calle))
                   .ForMember(dest => dest.NombreCalle, source => source.MapFrom(p => p.nombre_calle))
                   .ForMember(dest => dest.NumeroPuerta, source => source.MapFrom(p => p.NroPuerta));

                cfg.CreateMap<ConsultaPadronUbicacionesPuertasDTO, CPadron_Ubicaciones_Puertas>()
                  .ForMember(dest => dest.id_cpadronpuerta, source => source.MapFrom(p => p.IdConsultaPadronPuerta))
                  .ForMember(dest => dest.id_cpadronubicacion, source => source.MapFrom(p => p.IdConsultaPadronUbicacion))
                  .ForMember(dest => dest.codigo_calle, source => source.MapFrom(p => p.CodigoCalle))
                  .ForMember(dest => dest.nombre_calle, source => source.MapFrom(p => p.NombreCalle))
                  .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NumeroPuerta));

                cfg.CreateMap<CPadron_Ubicaciones_PropiedadHorizontal, ConsultaPadronUbicacionPropiedadHorizontalDTO>()
                   .ForMember(dest => dest.IdConsultaPadronPropiedadHorizontal, source => source.MapFrom(p => p.id_cpadronprophorizontal))
                   .ForMember(dest => dest.IdConsultaPadronUbicacion, source => source.MapFrom(p => p.id_cpadronubicacion))
                   .ForMember(dest => dest.IdPropiedadHorizontal, source => source.MapFrom(p => p.id_propiedadhorizontal))
                   .ForMember(dest => dest.UbicacionPropiedadaHorizontal, source => source.MapFrom(p => p.Ubicaciones_PropiedadHorizontal));

                cfg.CreateMap<ConsultaPadronUbicacionPropiedadHorizontalDTO, CPadron_Ubicaciones_PropiedadHorizontal>()
                   .ForMember(dest => dest.id_cpadronprophorizontal, source => source.MapFrom(p => p.IdConsultaPadronPropiedadHorizontal))
                   .ForMember(dest => dest.id_cpadronubicacion, source => source.MapFrom(p => p.IdConsultaPadronUbicacion))
                   .ForMember(dest => dest.id_propiedadhorizontal, source => source.MapFrom(p => p.IdPropiedadHorizontal));

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

                cfg.CreateMap<CPadron_Solicitudes, ConsultaPadronSolicitudesDTO>()
                    .ForMember(dest => dest.IdConsultaPadron, source => source.MapFrom(p => p.id_cpadron))
                    .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.id_tipotramite))
                    .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.id_tipoexpediente))
                    .ForMember(dest => dest.IdSubTipoExpediente, source => source.MapFrom(p => p.id_subtipoexpediente))
                    .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                    .ForMember(dest => dest.ObservacionesInternas, source => source.MapFrom(p => p.observaciones_internas))
                    .ForMember(dest => dest.ObservacionesContribuyente, source => source.MapFrom(p => p.observaciones_contribuyente))
                    .ForMember(dest => dest.NroExpedienteAnterior, source => source.MapFrom(p => p.nro_expediente_anterior))
                    .ForMember(dest => dest.Observaciones, source => source.MapFrom(p => p.observaciones))
                    .ForMember(dest => dest.NombreApellidoEscribano, source => source.MapFrom(p => p.nombre_apellido_escribano))
                    .ForMember(dest => dest.NroMatriculaEscribano, source => source.MapFrom(p => p.nro_matricula_escribano))
                    .ForMember(dest => dest.TipoTramite, source => source.Ignore())
                    .ForMember(dest => dest.TipoExpediente, source => source.Ignore())
                    .ForMember(dest => dest.SubTipoExpediente, source => source.Ignore())
                    .ForMember(dest => dest.Estado, source => source.Ignore())
                    .ForMember(dest => dest.Normativa, source => source.Ignore())
                    .ForMember(dest => dest.Ubicaciones, source => source.MapFrom(p => p.CPadron_Ubicaciones));

                cfg.CreateMap<ConsultaPadronSolicitudesDTO, CPadron_Solicitudes>()
                    .ForMember(dest => dest.id_cpadron, source => source.MapFrom(p => p.IdConsultaPadron))
                    .ForMember(dest => dest.id_tipotramite, source => source.MapFrom(p => p.IdTipoTramite))
                    .ForMember(dest => dest.id_tipoexpediente, source => source.MapFrom(p => p.IdTipoExpediente))
                    .ForMember(dest => dest.id_subtipoexpediente, source => source.MapFrom(p => p.IdSubTipoExpediente))
                    .ForMember(dest => dest.id_estado, source => source.MapFrom(p => p.IdEstado))
                    .ForMember(dest => dest.observaciones_internas, source => source.MapFrom(p => p.ObservacionesInternas))
                    .ForMember(dest => dest.observaciones_contribuyente, source => source.MapFrom(p => p.ObservacionesContribuyente))
                    .ForMember(dest => dest.nro_expediente_anterior, source => source.MapFrom(p => p.NroExpedienteAnterior))
                    .ForMember(dest => dest.observaciones, source => source.MapFrom(p => p.Observaciones))
                    .ForMember(dest => dest.nombre_apellido_escribano, source => source.MapFrom(p => p.NombreApellidoEscribano))
                    .ForMember(dest => dest.nro_matricula_escribano, source => source.MapFrom(p => p.NroMatriculaEscribano))
                    .ForMember(dest => dest.TipoTramite, source => source.Ignore())
                    .ForMember(dest => dest.TipoExpediente, source => source.Ignore())
                    .ForMember(dest => dest.SubtipoExpediente, source => source.Ignore())
                    .ForMember(dest => dest.CPadron_Estados, source => source.Ignore())
                    .ForMember(dest => dest.CPadron_Ubicaciones, source => source.MapFrom(p => p.Ubicaciones));

            });
            mapperBase = config.CreateMapper();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdConsultaPadronUbicacion"></param>
        /// <returns></returns>
		public ConsultaPadronUbicacionesDTO Single(int IdConsultaPadronUbicacion )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConsultaPadronUbicacionesRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdConsultaPadronUbicacion);
                var entityDto = mapperBase.Map<CPadron_Ubicaciones, ConsultaPadronUbicacionesDTO>(entity);
     
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
		public IEnumerable<ConsultaPadronUbicacionesDTO> GetByFKIdConsultaPadron(int IdConsultaPadron)
		{
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new ConsultaPadronUbicacionesRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdConsultaPadron(IdConsultaPadron);
            var elementsDto = mapperBase.Map<IEnumerable<CPadron_Ubicaciones>, IEnumerable<ConsultaPadronUbicacionesDTO>>(elements);
            return elementsDto;				
		}
		#region Métodos de actualizacion e insert
		/// <summary>
		/// Inserta la entidad para por parametro
		/// </summary>
		/// <param name="objectDto"></param>
        public bool Insert(ConsultaPadronUbicacionesDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork();
 
                repoConsultaPadronSolicitudes = new ConsultaPadronSolicitudesRepository(unitOfWork);
                repo = new ConsultaPadronUbicacionesRepository(unitOfWork);
                repoUbic = new UbicacionesRepository(unitOfWork);

                var consultaPadronSolicitudesEntity = repoConsultaPadronSolicitudes.Single(objectDto.IdConsultaPadron.Value);
                var ubicacionEntity = repoUbic.Single(objectDto.IdUbicacion.Value);

                if (consultaPadronSolicitudesEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && consultaPadronSolicitudesEntity.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
                    throw new Exception(Errors.SSIT_CPADRON_NO_ADMITE_CAMBIOS);
                
                if (ubicacionEntity.id_zonaplaneamiento == 0)
                    throw new Exception();

                var inhibidasEntity = ubicacionEntity.Ubicaciones_Inhibiciones;

                if (inhibidasEntity.Any(p => !p.fecha_vencimiento.HasValue || (DateTime.Now >= p.fecha_inhibicion && DateTime.Now <= p.fecha_vencimiento.Value)))
                    throw new Exception(Errors.UBICACION_INHIBIDA);

                var cpadronUbicEntity = consultaPadronSolicitudesEntity.CPadron_Ubicaciones.Where(x => x.id_ubicacion == objectDto.IdUbicacion.Value);

                if (cpadronUbicEntity.Count() > 0)
                    throw new Exception(Errors.UBICACION_IGUAL);

                CallesBL callesBL = new CallesBL();
                foreach (var puerta in objectDto.Puertas)
                    puerta.NombreCalle = callesBL.GetNombre(puerta.CodigoCalle, puerta.NumeroPuerta);

                var consultaPadronEntityInsert = mapperBase.Map<ConsultaPadronUbicacionesDTO, CPadron_Ubicaciones>(objectDto);
                consultaPadronEntityInsert.id_zonaplaneamiento = ubicacionEntity.id_zonaplaneamiento;
                var consultaPadronSolicitudesDTO = mapperBase.Map<CPadron_Solicitudes, ConsultaPadronSolicitudesDTO>(consultaPadronSolicitudesEntity);

                CPadron_Solicitudes consultaPadronSolicitudesEntityUpdate = null;
                if (string.IsNullOrEmpty(consultaPadronSolicitudesEntity.ZonaDeclarada))
                {
                    var Zona = ubicacionEntity.Zonas_Planeamiento;
                    if (!string.IsNullOrEmpty(Zona.CodZonaPla))
                    {
                        consultaPadronSolicitudesDTO.ZonaDeclarada = Zona.CodZonaPla;
                        consultaPadronSolicitudesEntityUpdate = mapperBase.Map<ConsultaPadronSolicitudesDTO, CPadron_Solicitudes>(consultaPadronSolicitudesDTO);  
                    }
                }

                using (IUnitOfWork unitOfWorkTran = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new ConsultaPadronUbicacionesRepository(unitOfWorkTran);

                    repo.Insert(consultaPadronEntityInsert);
                    if (consultaPadronSolicitudesEntityUpdate != null)
                    {
                        repoConsultaPadronSolicitudes = new ConsultaPadronSolicitudesRepository(unitOfWorkTran);
                        repoConsultaPadronSolicitudes.Update(consultaPadronSolicitudesEntityUpdate);
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
		public void Delete(ConsultaPadronUbicacionesDTO objectDto)
		{
		    try
		    {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new ConsultaPadronUbicacionesRepository(unitOfWork);
                    repoConsultarubicacionesPuertas = new ConsultaPadronUbicacionesPuertasRepository(unitOfWork);
                    repoConsultaPadronSolicitudes = new ConsultaPadronSolicitudesRepository(unitOfWork);
                    var consultaPadronUbicaciones = repo.Single(objectDto.IdConsultaPadronUbicacion);

                    if (consultaPadronUbicaciones.CPadron_Solicitudes.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.COMP && consultaPadronUbicaciones.CPadron_Solicitudes.id_estado != (int)Constantes.TipoEstadoSolicitudEnum.INCOM)
                        throw new Exception(Errors.SSIT_CPADRON_NO_ADMITE_CAMBIOS);

                    repoConsultarubicacionesPropHorizontal = new ConsultaPadronUbicacionPropiedadHorizontalRepository(unitOfWork);                    
                    if(consultaPadronUbicaciones.CPadron_Ubicaciones_PropiedadHorizontal.Any())
                        repoConsultarubicacionesPropHorizontal.DeleteRange(consultaPadronUbicaciones.CPadron_Ubicaciones_PropiedadHorizontal);

                    repoConsultarubicacionesPuertas.DeleteRange(consultaPadronUbicaciones.CPadron_Ubicaciones_Puertas);

                    var consultaPadron = consultaPadronUbicaciones.CPadron_Solicitudes;
                    repo.Delete(consultaPadronUbicaciones);

                    var consultaPadronUbicacionesEntity = consultaPadron.CPadron_Ubicaciones;

                    if (!consultaPadronUbicacionesEntity.Any())                    
                    {
                        consultaPadron.ZonaDeclarada = null;
                        repoConsultaPadronSolicitudes.Update(consultaPadron);
                    }
                    else
                    {                                                
                        repoZonaPlaneamiento = new ZonasPlaneamientoRepository(unitOfWork);
                        var zona1 = consultaPadron.CPadron_Ubicaciones.Select(p => p.Ubicaciones).Select(p => p.Zonas_Planeamiento);
                        var zona2 = repoZonaPlaneamiento.GetZonaComplementariaConsultaPadron(consultaPadron.id_cpadron);
                        var zonas = zona1.Concat(zona2);

                        if (!string.IsNullOrEmpty(consultaPadron.ZonaDeclarada))
                        {
                            var zonaNueva = zonas.FirstOrDefault().CodZonaPla;
                            if (!zonas.Any(p => p.CodZonaPla.Equals(consultaPadron.ZonaDeclarada)))
                            {
                                consultaPadron.ZonaDeclarada = zonaNueva;
                                if (string.IsNullOrEmpty(zonaNueva))
                                    consultaPadron.ZonaDeclarada = null;
                                
                                repoConsultaPadronSolicitudes.Update(consultaPadron);
                            }
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
		#endregion
    
    }
}

