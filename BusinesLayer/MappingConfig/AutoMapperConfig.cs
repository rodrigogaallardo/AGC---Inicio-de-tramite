using AutoMapper;
using DataAcess;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLayer.MappingConfig
{
    public class AutoMapperConfig
    {

        public static IMapper MapperBaseEncomienda { get; set; }

        public static void RegisterMappingEncomienda()
        {

            var config = new MapperConfiguration(cfg =>
            {
                #region "Encomienda"
                cfg.CreateMap<EncomiendaDTO, Encomienda>()
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.nroEncomiendaconsejo, source => source.MapFrom(p => p.NroEncomiendaConsejo))
                    .ForMember(dest => dest.id_consejo, source => source.MapFrom(p => p.IdConsejo))
                    .ForMember(dest => dest.id_profesional, source => source.MapFrom(p => p.IdProfesional))
                    .ForMember(dest => dest.id_tipotramite, source => source.MapFrom(p => p.IdTipoTramite))
                    .ForMember(dest => dest.id_tipoexpediente, source => source.MapFrom(p => p.IdTipoExpediente))
                    .ForMember(dest => dest.id_subtipoexpediente, source => source.MapFrom(p => p.IdSubTipoExpediente))
                    .ForMember(dest => dest.id_estado, source => source.MapFrom(p => p.IdEstado))
                    .ForMember(dest => dest.Observaciones_plantas, source => source.MapFrom(p => p.ObservacionesPlantas))
                    .ForMember(dest => dest.Observaciones_rubros, source => source.MapFrom(p => p.ObservacionesRubros))
                    .ForMember(dest => dest.Observaciones_rubros_AT_anterior, source => source.MapFrom(p => p.ObservacionesRubrosATAnterior))
                    .ForMember(dest => dest.Pro_teatro, source => source.MapFrom(p => p.ProTeatro))
                    //.ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.TipoTramite, source => source.MapFrom(p => p.TipoTramite))
                    .ForMember(dest => dest.TipoExpediente, source => source.MapFrom(p => p.TipoExpediente))
                    .ForMember(dest => dest.tipo_anexo, source => source.MapFrom(p => p.tipoAnexo))
                    .ForMember(dest => dest.Encomienda_Titulares_PersonasJuridicas, source => source.MapFrom(p => p.EncomiendaTitularesPersonasJuridicasDTO))
                    .ForMember(dest => dest.Encomienda_Titulares_PersonasFisicas, source => source.MapFrom(p => p.EncomiendaTitularesPersonasFisicasDTO))
                    .ForMember(dest => dest.Profesional, source => source.MapFrom(p => p.ProfesionalDTO))
                    .ForMember(dest => dest.Encomienda_Rubros, source => source.MapFrom(p => p.EncomiendaRubrosDTO))
                    .ForMember(dest => dest.Encomienda_ConformacionLocal, source => source.MapFrom(p => p.EncomiendaConformacionLocalDTO))
                    .ForMember(dest => dest.Encomienda_DatosLocal, source => source.MapFrom(p => p.EncomiendaDatosLocalDTO))
                    .ForMember(dest => dest.Encomienda_DocumentosAdjuntos, source => source.MapFrom(p => p.EncomiendaDocumentosAdjuntosDTO))
                    .ForMember(dest => dest.Encomienda_Planos, source => source.MapFrom(p => p.EncomiendaPlanosDTO))
                    .ForMember(dest => dest.Encomienda_Plantas, source => source.MapFrom(p => p.EncomiendaPlantasDTO))
                    .ForMember(dest => dest.Encomienda_Rectificatoria, source => source.MapFrom(p => p.EncomiendaRectificatoriaDTO))
                    .ForMember(dest => dest.Encomienda_SSIT_Solicitudes, source => source.MapFrom(p => p.EncomiendaSSITSolicitudesDTO))
                    .ForMember(dest => dest.Encomienda_Transf_Solicitudes, source => source.MapFrom(p => p.EncomiendaTransfSolicitudesDTO))
                    .ForMember(dest => dest.Encomienda_Ubicaciones, source => source.MapFrom(p => p.EncomiendaUbicacionesDTO))
                    .ForMember(dest => dest.Encomienda_Normativas, source => source.MapFrom(p => p.EncomiendaNormativasDTO))
                    .ForMember(dest => dest.SubtipoExpediente, source => source.MapFrom(p => p.SubTipoExpediente))
                    .ForMember(dest => dest.Encomienda_Estados, source => source.Ignore())
                    .ForMember(dest => dest.wsEscribanos_ActaNotarial, source => source.Ignore())
                    .ForMember(dest => dest.Encomienda_RubrosCN, source => source.MapFrom(p => p.EncomiendaRubrosCNDTO))
                    .ForMember(dest => dest.Encomienda_RubrosCN_Deposito, source => source.MapFrom(p => p.Encomienda_RubrosCN_DepositoDTO))
                    ;

                cfg.CreateMap<Encomienda, EncomiendaDTO>()
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.NroEncomiendaConsejo, source => source.MapFrom(p => p.nroEncomiendaconsejo))
                    .ForMember(dest => dest.IdConsejo, source => source.MapFrom(p => p.id_consejo))
                    .ForMember(dest => dest.IdProfesional, source => source.MapFrom(p => p.id_profesional))
                    .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.id_tipotramite))
                    .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.id_tipoexpediente))
                    .ForMember(dest => dest.IdSubTipoExpediente, source => source.MapFrom(p => p.id_subtipoexpediente))
                    .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                    .ForMember(dest => dest.ObservacionesPlantas, source => source.MapFrom(p => p.Observaciones_plantas))
                    .ForMember(dest => dest.ObservacionesRubros, source => source.MapFrom(p => p.Observaciones_rubros))
                    .ForMember(dest => dest.ObservacionesRubrosATAnterior, source => source.MapFrom(p => p.Observaciones_rubros_AT_anterior))
                    .ForMember(dest => dest.ProTeatro, source => source.MapFrom(p => p.Pro_teatro))
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.Encomienda_Transf_Solicitudes.Count > 0 ? p.Encomienda_Transf_Solicitudes.Select(x => x.id_solicitud).FirstOrDefault() : p.Encomienda_SSIT_Solicitudes.Count > 0 ? p.Encomienda_SSIT_Solicitudes.Select(x => x.id_solicitud).FirstOrDefault() : 0))
                    .ForMember(dest => dest.TipoTramite, source => source.MapFrom(p => p.TipoTramite))
                    .ForMember(dest => dest.TipoExpediente, source => source.MapFrom(p => p.TipoExpediente))
                    .ForMember(dest => dest.Estado, source => source.MapFrom(p => p.Encomienda_Estados))
                    .ForMember(dest => dest.tipoAnexo, source => source.MapFrom(p => p.tipo_anexo))
                    .ForMember(dest => dest.EncomiendaTitularesPersonasJuridicasDTO, source => source.MapFrom(p => p.Encomienda_Titulares_PersonasJuridicas))
                    .ForMember(dest => dest.EncomiendaTitularesPersonasFisicasDTO, source => source.MapFrom(p => p.Encomienda_Titulares_PersonasFisicas))
                    .ForMember(dest => dest.ProfesionalDTO, source => source.MapFrom(p => p.Profesional))
                    .ForMember(dest => dest.EncomiendaRubrosDTO, source => source.MapFrom(p => p.Encomienda_Rubros))
                    .ForMember(dest => dest.EncomiendaConformacionLocalDTO, source => source.MapFrom(p => p.Encomienda_ConformacionLocal))
                    .ForMember(dest => dest.EncomiendaDatosLocalDTO, source => source.MapFrom(p => p.Encomienda_DatosLocal))
                    .ForMember(dest => dest.EncomiendaDocumentosAdjuntosDTO, source => source.MapFrom(p => p.Encomienda_DocumentosAdjuntos))
                    .ForMember(dest => dest.EncomiendaPlanosDTO, source => source.MapFrom(p => p.Encomienda_Planos))
                    .ForMember(dest => dest.EncomiendaPlantasDTO, source => source.MapFrom(p => p.Encomienda_Plantas))
                    .ForMember(dest => dest.EncomiendaRectificatoriaDTO, source => source.MapFrom(p => p.Encomienda_Rectificatoria))
                    .ForMember(dest => dest.EncomiendaSSITSolicitudesDTO, source => source.MapFrom(p => p.Encomienda_SSIT_Solicitudes))
                    .ForMember(dest => dest.EncomiendaTransfSolicitudesDTO, source => source.MapFrom(p => p.Encomienda_Transf_Solicitudes))
                    .ForMember(dest => dest.EncomiendaUbicacionesDTO, source => source.MapFrom(p => p.Encomienda_Ubicaciones))
                    .ForMember(dest => dest.EncomiendaNormativasDTO, source => source.MapFrom(p => p.Encomienda_Normativas))
                    .ForMember(dest => dest.SubTipoExpediente, source => source.MapFrom(p => p.SubtipoExpediente))
                    .ForMember(dest => dest.ActasNotariales, source => source.MapFrom(p => p.wsEscribanos_ActaNotarial))
                    .ForMember(dest => dest.EncomiendaRubrosCNDTO, source => source.MapFrom(p => p.Encomienda_RubrosCN))
                    .ForMember(dest => dest.Encomienda_RubrosCN_DepositoDTO, source => source.MapFrom(p => p.Encomienda_RubrosCN_Deposito));

                #endregion
                #region "Encomienda_Certificado_Sobrecarga"
                cfg.CreateMap<Encomienda_Certificado_Sobrecarga, EncomiendaCertificadoSobrecargaDTO>()
                    .ForMember(dest => dest.EncomiendaTiposSobrecargasDTO, source => source.MapFrom(p => p.Encomienda_Tipos_Sobrecargas))
                    .ForMember(dest => dest.EncomiendaTiposCertificadosSobrecargaDTO, source => source.MapFrom(p => p.Encomienda_Tipos_Certificados_Sobrecarga));

                cfg.CreateMap<EncomiendaCertificadoSobrecargaDTO, Encomienda_Certificado_Sobrecarga>()
                    .ForMember(dest => dest.Encomienda_Tipos_Sobrecargas, source => source.MapFrom(p => p.EncomiendaTiposSobrecargasDTO))
                    .ForMember(dest => dest.Encomienda_Tipos_Certificados_Sobrecarga, source => source.MapFrom(p => p.EncomiendaTiposCertificadosSobrecargaDTO));
                #endregion
                #region "Encomienda_Tipos_Certificados_Sobrecarga"
                cfg.CreateMap<EncomiendaTiposCertificadosSobrecargaDTO, Encomienda_Tipos_Certificados_Sobrecarga>();

                cfg.CreateMap<Encomienda_Tipos_Certificados_Sobrecarga, EncomiendaTiposCertificadosSobrecargaDTO>();
                #endregion
                #region "Encomienda_ConformacionLocal"
                cfg.CreateMap<Encomienda_ConformacionLocal, EncomiendaConformacionLocalDTO>()
                    .ForMember(dest => dest.EncomiendaPlantasDTO, source => source.MapFrom(p => p.Encomienda_Plantas))
                    .ForMember(dest => dest.TipoSuperficieDTO, source => source.MapFrom(p => p.TipoSuperficie))
                    .ForMember(dest => dest.TipoVentilacionDTO, source => source.MapFrom(p => p.tipo_ventilacion))
                    .ForMember(dest => dest.TipoDestinoDTO, source => source.MapFrom(p => p.TipoDestino))
                    .ForMember(dest => dest.TipoIluminacionDTO, source => source.MapFrom(p => p.tipo_iluminacion));

                cfg.CreateMap<EncomiendaConformacionLocalDTO, Encomienda_ConformacionLocal>()
                    .ForMember(dest => dest.Encomienda_Plantas, source => source.MapFrom(p => p.EncomiendaPlantasDTO))
                    .ForMember(dest => dest.TipoSuperficie, source => source.MapFrom(p => p.TipoSuperficieDTO))
                    .ForMember(dest => dest.tipo_ventilacion, source => source.MapFrom(p => p.TipoVentilacionDTO))
                    .ForMember(dest => dest.TipoDestino, source => source.MapFrom(p => p.TipoDestinoDTO))
                    .ForMember(dest => dest.tipo_iluminacion, source => source.MapFrom(p => p.TipoIluminacionDTO));
                #endregion
                #region "Encomienda_DatosLocal"
                cfg.CreateMap<Encomienda_DatosLocal, EncomiendaDatosLocalDTO>()
                    .ForMember(dest => dest.EncomiendaCertificadoSobrecargaDTO, source => source.MapFrom(p => p.Encomienda_Certificado_Sobrecarga));

                cfg.CreateMap<EncomiendaDatosLocalDTO, Encomienda_DatosLocal>()
                    .ForMember(dest => dest.Encomienda_Certificado_Sobrecarga, source => source.MapFrom(p => p.EncomiendaCertificadoSobrecargaDTO));
                #endregion
                #region "Encomienda_DocumentosAdjuntos"
                cfg.CreateMap<Encomienda_DocumentosAdjuntos, EncomiendaDocumentosAdjuntosDTO>()
                    .ForMember(dest => dest.TiposDeDocumentosRequeridosDTO, source => source.MapFrom(p => p.TiposDeDocumentosRequeridos))
                    .ForMember(dest => dest.TiposDeDocumentosSistemaDTO, source => source.MapFrom(p => p.TiposDeDocumentosSistema));

                cfg.CreateMap<EncomiendaDocumentosAdjuntosDTO, Encomienda_DocumentosAdjuntos>()
                    .ForMember(dest => dest.TiposDeDocumentosRequeridos, source => source.MapFrom(p => p.TiposDeDocumentosRequeridosDTO))
                    .ForMember(dest => dest.TiposDeDocumentosSistema, source => source.MapFrom(p => p.TiposDeDocumentosSistemaDTO));
                #endregion
                #region "Encomienda_Estados"
                cfg.CreateMap<Encomienda_Estados, EncomiendaEstadosDTO>()
                    .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                    .ForMember(dest => dest.CodEstado, source => source.MapFrom(p => p.cod_estado))
                    .ForMember(dest => dest.NomEstadoConsejo, source => source.MapFrom(p => p.nom_estado_consejo))
                    .ForMember(dest => dest.NomEstado, source => source.MapFrom(p => p.nom_estado));

                cfg.CreateMap<EncomiendaEstadosDTO, Encomienda_Estados>()
                  .ForMember(dest => dest.id_estado, source => source.MapFrom(p => p.IdEstado))
                  .ForMember(dest => dest.cod_estado, source => source.MapFrom(p => p.CodEstado))
                  .ForMember(dest => dest.nom_estado, source => source.MapFrom(p => p.NomEstado))
                  .ForMember(dest => dest.nom_estado_consejo, source => source.MapFrom(p => p.NomEstadoConsejo));
                #endregion
                #region "Encomienda_Titulares_PersonasFisicas"
                cfg.CreateMap<Encomienda_Titulares_PersonasFisicas, EncomiendaTitularesPersonasFisicasDTO>()
                    .ForMember(dest => dest.IdPersonaFisica, source => source.MapFrom(p => p.id_personafisica))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.IdTipoDocPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.NroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.IdTipoiibb, source => source.MapFrom(p => p.id_tipoiibb))
                    .ForMember(dest => dest.IngresosBrutos, source => source.MapFrom(p => p.Ingresos_Brutos))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.Nro_Puerta))
                    .ForMember(dest => dest.IdLocalidad, source => source.MapFrom(p => p.Id_Localidad))
                    .ForMember(dest => dest.CodigoPostal, source => source.MapFrom(p => p.Codigo_Postal))
                    .ForMember(dest => dest.EncomiendaFirmantesPersonasFisicasDTO, source => source.MapFrom(p => p.Encomienda_Firmantes_PersonasFisicas))
                    .ForMember(dest => dest.LocalidadDTO, source => source.MapFrom(p => p.Localidad))
                    .ForMember(dest => dest.TipoDocumentoPersonalDTO, source => source.MapFrom(p => p.TipoDocumentoPersonal))
                    .ForMember(dest => dest.TiposDeIngresosBrutosDTO, source => source.MapFrom(p => p.TiposDeIngresosBrutos));

                cfg.CreateMap<EncomiendaTitularesPersonasFisicasDTO, Encomienda_Titulares_PersonasFisicas>()
                    .ForMember(dest => dest.id_personafisica, source => source.MapFrom(p => p.IdPersonaFisica))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocPersonal))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NroDocumento))
                    .ForMember(dest => dest.id_tipoiibb, source => source.MapFrom(p => p.IdTipoiibb))
                    .ForMember(dest => dest.Ingresos_Brutos, source => source.MapFrom(p => p.IngresosBrutos))
                    .ForMember(dest => dest.Nro_Puerta, source => source.MapFrom(p => p.NroPuerta))
                    .ForMember(dest => dest.Id_Localidad, source => source.MapFrom(p => p.IdLocalidad))
                    .ForMember(dest => dest.Codigo_Postal, source => source.MapFrom(p => p.CodigoPostal))
                    .ForMember(dest => dest.Encomienda_Firmantes_PersonasFisicas, source => source.MapFrom(p => p.EncomiendaFirmantesPersonasFisicasDTO))
                    .ForMember(dest => dest.Localidad, source => source.MapFrom(p => p.LocalidadDTO))
                    .ForMember(dest => dest.TipoDocumentoPersonal, source => source.MapFrom(p => p.TipoDocumentoPersonalDTO))
                    .ForMember(dest => dest.TiposDeIngresosBrutos, source => source.MapFrom(p => p.TiposDeIngresosBrutosDTO));
                #endregion
                #region "Encomienda_Titulares_PersonasJuridicas"
                cfg.CreateMap<Encomienda_Titulares_PersonasJuridicas, EncomiendaTitularesPersonasJuridicasDTO>()
                    .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.IdTipoSociedad, source => source.MapFrom(p => p.Id_TipoSociedad))
                    .ForMember(dest => dest.RazonSocial, source => source.MapFrom(p => p.Razon_Social))
                    .ForMember(dest => dest.IdTipoIb, source => source.MapFrom(p => p.id_tipoiibb))
                    .ForMember(dest => dest.NroIb, source => source.MapFrom(p => p.Nro_IIBB))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NroPuerta))
                    .ForMember(dest => dest.IdLocalidad, source => source.MapFrom(p => p.id_localidad))
                    .ForMember(dest => dest.CodigoPostal, source => source.MapFrom(p => p.Codigo_Postal))
                    .ForMember(dest => dest.LocalidadDTO, source => source.MapFrom(p => p.Localidad))
                    .ForMember(dest => dest.TiposDeIngresosBrutosDTO, source => source.MapFrom(p => p.TiposDeIngresosBrutos))
                    .ForMember(dest => dest.TipoSociedadDTO, source => source.MapFrom(p => p.TipoSociedad))
                    .ForMember(dest => dest.EncomiendaFirmantesPersonasJuridicasDTO, source => source.MapFrom(p => p.Encomienda_Firmantes_PersonasJuridicas))
                    .ForMember(dest => dest.EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO, source => source.MapFrom(p => p.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas));

                cfg.CreateMap<EncomiendaTitularesPersonasJuridicasDTO, Encomienda_Titulares_PersonasJuridicas>()
                    .ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.Id_TipoSociedad, source => source.MapFrom(p => p.IdTipoSociedad))
                    .ForMember(dest => dest.Razon_Social, source => source.MapFrom(p => p.RazonSocial))
                    .ForMember(dest => dest.id_tipoiibb, source => source.MapFrom(p => p.IdTipoIb))
                    .ForMember(dest => dest.Nro_IIBB, source => source.MapFrom(p => p.NroIb))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NroPuerta))
                    .ForMember(dest => dest.id_localidad, source => source.MapFrom(p => p.IdLocalidad))
                    .ForMember(dest => dest.Codigo_Postal, source => source.MapFrom(p => p.CodigoPostal))
                    .ForMember(dest => dest.Localidad, source => source.MapFrom(p => p.LocalidadDTO))
                    .ForMember(dest => dest.TiposDeIngresosBrutos, source => source.MapFrom(p => p.TiposDeIngresosBrutosDTO))
                    .ForMember(dest => dest.TipoSociedad, source => source.MapFrom(p => p.TipoSociedadDTO))
                    .ForMember(dest => dest.Encomienda_Firmantes_PersonasJuridicas, source => source.MapFrom(p => p.EncomiendaFirmantesPersonasJuridicasDTO))
                    .ForMember(dest => dest.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas, source => source.MapFrom(p => p.EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO));

                #endregion
                #region "Encomienda_Titulares_PersonasJuridicas_PersonasFisicas"
                cfg.CreateMap<Encomienda_Titulares_PersonasJuridicas_PersonasFisicas, EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO>()
                    .ForMember(dest => dest.IdTitularPj, source => source.MapFrom(p => p.id_titular_pj))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                    .ForMember(dest => dest.IdTipoDocPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.NroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.IdFirmantePj, source => source.MapFrom(p => p.id_firmante_pj))
                    .ForMember(dest => dest.FirmanteMismaPersona, source => source.MapFrom(p => p.firmante_misma_persona))
                    .ForMember(dest => dest.EncomiendaFirmantesPersonasJuridicasDTO, source => source.MapFrom(p => p.Encomienda_Firmantes_PersonasJuridicas))
                    .ForMember(dest => dest.EncomiendaTitularesPersonasJuridicasDTO, source => source.MapFrom(p => p.Encomienda_Titulares_PersonasJuridicas))
                    .ForMember(dest => dest.TipoDocumentoPersonalDTO, source => source.MapFrom(p => p.TipoDocumentoPersonal));

                cfg.CreateMap<EncomiendaTitularesPersonasJuridicasPersonasFisicasDTO, Encomienda_Titulares_PersonasJuridicas_PersonasFisicas>()
                    .ForMember(dest => dest.id_titular_pj, source => source.MapFrom(p => p.IdTitularPj))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocPersonal))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NroDocumento))
                    .ForMember(dest => dest.id_firmante_pj, source => source.MapFrom(p => p.IdFirmantePj))
                    .ForMember(dest => dest.firmante_misma_persona, source => source.MapFrom(p => p.FirmanteMismaPersona))
                    .ForMember(dest => dest.Encomienda_Firmantes_PersonasJuridicas, source => source.MapFrom(p => p.EncomiendaFirmantesPersonasJuridicasDTO))
                    .ForMember(dest => dest.Encomienda_Titulares_PersonasJuridicas, source => source.MapFrom(p => p.EncomiendaTitularesPersonasJuridicasDTO))
                    .ForMember(dest => dest.TipoDocumentoPersonal, source => source.MapFrom(p => p.TipoDocumentoPersonalDTO));
                #endregion
                #region "Encomienda_Firmantes_PersonasJuridicas"
                cfg.CreateMap<Encomienda_Firmantes_PersonasJuridicas, EncomiendaFirmantesPersonasJuridicasDTO>()
                    .ForMember(dest => dest.IdFirmantePj, source => source.MapFrom(p => p.id_firmante_pj))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.IdPersonaJuridica, source => source.MapFrom(p => p.id_personajuridica))
                    .ForMember(dest => dest.IdTipoDocPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.NroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.IdTipoCaracter, source => source.MapFrom(p => p.id_tipocaracter))
                    .ForMember(dest => dest.CargoFirmantePj, source => source.MapFrom(p => p.cargo_firmante_pj))
                    .ForMember(dest => dest.TipoDocumentoPersonalDTO, source => source.MapFrom(p => p.TipoDocumentoPersonal))
                    .ForMember(dest => dest.TiposDeCaracterLegalDTO, source => source.MapFrom(p => p.TiposDeCaracterLegal))
                    .ForMember(dest => dest.EncomiendaTitularesPersonasJuridicasDTO, source => source.MapFrom(p => p.Encomienda_Titulares_PersonasJuridicas));

                cfg.CreateMap<EncomiendaFirmantesPersonasJuridicasDTO, Encomienda_Firmantes_PersonasJuridicas>()
                    .ForMember(dest => dest.id_firmante_pj, source => source.MapFrom(p => p.IdFirmantePj))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.id_personajuridica, source => source.MapFrom(p => p.IdPersonaJuridica))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipoDocPersonal))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NroDocumento))
                    .ForMember(dest => dest.id_tipocaracter, source => source.MapFrom(p => p.IdTipoCaracter))
                    .ForMember(dest => dest.cargo_firmante_pj, source => source.MapFrom(p => p.CargoFirmantePj))
                    .ForMember(dest => dest.TipoDocumentoPersonal, source => source.MapFrom(p => p.TipoDocumentoPersonalDTO))
                    .ForMember(dest => dest.TiposDeCaracterLegal, source => source.MapFrom(p => p.TiposDeCaracterLegalDTO));
                #endregion
                #region "Encomienda_Firmantes_PersonasFisicas"
                cfg.CreateMap<Encomienda_Firmantes_PersonasFisicas, EncomiendaFirmantesPersonasFisicasDTO>()
                    .ForMember(dest => dest.IdFirmantePf, source => source.MapFrom(p => p.id_firmante_pf))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.IdPersonaFisica, source => source.MapFrom(p => p.id_personafisica))
                    .ForMember(dest => dest.IdTipodocPersonal, source => source.MapFrom(p => p.id_tipodoc_personal))
                    .ForMember(dest => dest.NroDocumento, source => source.MapFrom(p => p.Nro_Documento))
                    .ForMember(dest => dest.IdTipoCaracter, source => source.MapFrom(p => p.id_tipocaracter))
                    .ForMember(dest => dest.TipoDocumentoPersonalDTO, source => source.MapFrom(p => p.TipoDocumentoPersonal))
                    .ForMember(dest => dest.TiposDeCaracterLegalDTO, source => source.MapFrom(p => p.TiposDeCaracterLegal))
                    .ForMember(dest => dest.EncomiendaTitularesPersonasFisicasDTO, source => source.MapFrom(p => p.Encomienda_Titulares_PersonasFisicas));

                cfg.CreateMap<EncomiendaFirmantesPersonasFisicasDTO, Encomienda_Firmantes_PersonasFisicas>()
                    .ForMember(dest => dest.id_firmante_pf, source => source.MapFrom(p => p.IdFirmantePf))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.id_personafisica, source => source.MapFrom(p => p.IdPersonaFisica))
                    .ForMember(dest => dest.id_tipodoc_personal, source => source.MapFrom(p => p.IdTipodocPersonal))
                    .ForMember(dest => dest.Nro_Documento, source => source.MapFrom(p => p.NroDocumento))
                    .ForMember(dest => dest.id_tipocaracter, source => source.MapFrom(p => p.IdTipoCaracter))
                    .ForMember(dest => dest.TipoDocumentoPersonal, source => source.MapFrom(p => p.TipoDocumentoPersonalDTO))
                    .ForMember(dest => dest.TiposDeCaracterLegal, source => source.MapFrom(p => p.TiposDeCaracterLegalDTO));
                #endregion
                #region "Encomienda_Normativas"
                cfg.CreateMap<Encomienda_Normativas, EncomiendaNormativasDTO>()
                    .ForMember(dest => dest.IdEncomiendaNormativa, source => source.MapFrom(p => p.id_encomiendatiponormativa))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.IdTipoNormativa, source => source.MapFrom(p => p.id_tiponormativa))
                    .ForMember(dest => dest.IdEntidadNormativa, source => source.MapFrom(p => p.id_entidadnormativa))
                    .ForMember(dest => dest.NroNormativa, source => source.MapFrom(p => p.nro_normativa))
                    .ForMember(dest => dest.TipoNormativaDTO, source => source.MapFrom(p => p.TipoNormativa))
                    .ForMember(dest => dest.EntidadNormativaDTO, source => source.MapFrom(p => p.EntidadNormativa));

                cfg.CreateMap<EncomiendaNormativasDTO, Encomienda_Normativas>()
                    .ForMember(dest => dest.id_encomiendatiponormativa, source => source.MapFrom(p => p.IdEncomiendaNormativa))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.id_tiponormativa, source => source.MapFrom(p => p.IdTipoNormativa))
                    .ForMember(dest => dest.id_entidadnormativa, source => source.MapFrom(p => p.IdEntidadNormativa))
                    .ForMember(dest => dest.nro_normativa, source => source.MapFrom(p => p.NroNormativa))
                    .ForMember(dest => dest.TipoNormativa, source => source.MapFrom(p => p.TipoNormativaDTO))
                    .ForMember(dest => dest.EntidadNormativa, source => source.MapFrom(p => p.EntidadNormativaDTO));
                #endregion
                #region "Encomienda_Planos"
                cfg.CreateMap<Encomienda_Planos, EncomiendaPlanosDTO>()
                    .ForMember(dest => dest.TiposDePlanosDTO, source => source.MapFrom(p => p.TiposDePlanos));

                cfg.CreateMap<EncomiendaPlanosDTO, Encomienda_Planos>()
                    .ForMember(dest => dest.TiposDePlanos, source => source.MapFrom(p => p.TiposDePlanosDTO));
                #endregion
                #region "Encomienda_Plantas"
                cfg.CreateMap<Encomienda_Plantas, EncomiendaPlantasDTO>()
                    .ForMember(dest => dest.id_encomiendatiposector, source => source.MapFrom(p => p.id_encomiendatiposector))
                    .ForMember(dest => dest.Descripcion, source => source.MapFrom(p => p.detalle_encomiendatiposector))
                    .ForMember(dest => dest.IdTipoSector, source => source.MapFrom(p => p.id_tiposector))
                    .ForMember(dest => dest.TipoSectorDTO, source => source.MapFrom(p => p.TipoSector));

                cfg.CreateMap<EncomiendaPlantasDTO, Encomienda_Plantas>()
                    .ForMember(dest => dest.id_encomiendatiposector, source => source.MapFrom(p => p.id_encomiendatiposector))
                    .ForMember(dest => dest.detalle_encomiendatiposector, source => source.MapFrom(p => p.Descripcion))
                    .ForMember(dest => dest.id_tiposector, source => source.MapFrom(p => p.IdTipoSector))
                    .ForMember(dest => dest.TipoSector, source => source.MapFrom(p => p.TipoSectorDTO));
                #endregion
                #region "Encomienda_Rectificatoria"
                cfg.CreateMap<EncomiendaRectificatoriaDTO, Encomienda_Rectificatoria>();

                cfg.CreateMap<Encomienda_Rectificatoria, EncomiendaRectificatoriaDTO>();
                #endregion
                #region "Encomienda_Rubros"
                cfg.CreateMap<Encomienda_Rubros, EncomiendaRubrosDTO>()
                    .ForMember(dest => dest.CodigoRubro, source => source.MapFrom(p => p.cod_rubro))
                    .ForMember(dest => dest.IdEncomiendaRubro, source => source.MapFrom(p => p.id_encomiendarubro))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.DescripcionRubro, source => source.MapFrom(p => p.desc_rubro))
                    .ForMember(dest => dest.IdTipoActividad, source => source.MapFrom(p => p.id_tipoactividad))
                    .ForMember(dest => dest.IdTipoDocumentoRequerido, source => source.MapFrom(p => p.id_tipodocreq))
                    .ForMember(dest => dest.IdImpactoAmbiental, source => source.MapFrom(p => p.id_ImpactoAmbiental))
                    .ForMember(dest => dest.ImpactoAmbientalDTO, source => source.MapFrom(p => p.ImpactoAmbiental))
                    .ForMember(dest => dest.TipoDocumentacionRequeridaDTO, source => source.MapFrom(p => p.Tipo_Documentacion_Req))
                    .ForMember(dest => dest.TipoActividadDTO, source => source.MapFrom(p => p.TipoActividad));

                cfg.CreateMap<EncomiendaRubrosDTO, Encomienda_Rubros>()
                    .ForMember(dest => dest.id_encomiendarubro, source => source.MapFrom(p => p.IdEncomiendaRubro))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.cod_rubro, source => source.MapFrom(p => p.CodigoRubro))
                    .ForMember(dest => dest.desc_rubro, source => source.MapFrom(p => p.DescripcionRubro))
                    .ForMember(dest => dest.id_tipoactividad, source => source.MapFrom(p => p.IdTipoActividad))
                    .ForMember(dest => dest.id_tipodocreq, source => source.MapFrom(p => p.IdTipoDocumentoRequerido))
                    .ForMember(dest => dest.id_ImpactoAmbiental, source => source.MapFrom(p => p.IdImpactoAmbiental))
                    .ForMember(dest => dest.ImpactoAmbiental, source => source.MapFrom(p => p.ImpactoAmbientalDTO))
                    .ForMember(dest => dest.Tipo_Documentacion_Req, source => source.MapFrom(p => p.TipoDocumentacionRequeridaDTO))
                    .ForMember(dest => dest.TipoActividad, source => source.MapFrom(p => p.TipoActividadDTO));
                #endregion
                #region "Encomienda_Tipos_Sobrecargas"
                cfg.CreateMap<EncomiendaTiposSobrecargasDTO, Encomienda_Tipos_Sobrecargas>();

                cfg.CreateMap<Encomienda_Tipos_Sobrecargas, EncomiendaTiposSobrecargasDTO>();
                #endregion
                #region "Encomienda_Ubicaciones"
                cfg.CreateMap<Encomienda_Ubicaciones, EncomiendaUbicacionesDTO>()
                    .ForMember(dest => dest.IdEncomiendaUbicacion, source => source.MapFrom(p => p.id_encomiendaubicacion))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.IdUbicacion, source => source.MapFrom(p => p.id_ubicacion))
                    .ForMember(dest => dest.IdSubtipoUbicacion, source => source.MapFrom(p => p.id_subtipoubicacion))
                    .ForMember(dest => dest.LocalSubtipoUbicacion, source => source.MapFrom(p => p.local_subtipoubicacion))
                    .ForMember(dest => dest.DeptoLocalEncomiendaUbicacion, source => source.MapFrom(p => p.deptoLocal_encomiendaubicacion))
                    .ForMember(dest => dest.IdZonaPlaneamiento, source => source.MapFrom(p => p.id_zonaplaneamiento))
                    .ForMember(dest => dest.EncomiendaUbicacionesPropiedadHorizontalDTO, source => source.MapFrom(p => p.Encomienda_Ubicaciones_PropiedadHorizontal))
                    .ForMember(dest => dest.EncomiendaUbicacionesPuertasDTO, source => source.MapFrom(p => p.Encomienda_Ubicaciones_Puertas))
                    .ForMember(dest => dest.SubTipoUbicacionesDTO, source => source.MapFrom(p => p.SubTiposDeUbicacion))
                    .ForMember(dest => dest.ZonasPlaneamientoDTO, source => source.MapFrom(p => p.Zonas_Planeamiento))
                    .ForMember(dest => dest.Ubicacion, source => source.MapFrom(p => p.Ubicaciones))
                    .ForMember(dest => dest.EncomiendaUbicacionesDistritosDTO, source => source.MapFrom(p => p.Encomienda_Ubicaciones_Distritos))
                    .ForMember(dest => dest.EncomiendaUbicacionesMixturasDTO, source => source.MapFrom(p => p.Encomienda_Ubicaciones_Mixturas));

                cfg.CreateMap<EncomiendaUbicacionesDTO, Encomienda_Ubicaciones>()
                    .ForMember(dest => dest.id_encomiendaubicacion, source => source.MapFrom(p => p.IdEncomiendaUbicacion))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.id_ubicacion, source => source.MapFrom(p => p.IdUbicacion))
                    .ForMember(dest => dest.id_subtipoubicacion, source => source.MapFrom(p => p.IdSubtipoUbicacion))
                    .ForMember(dest => dest.local_subtipoubicacion, source => source.MapFrom(p => p.LocalSubtipoUbicacion))
                    .ForMember(dest => dest.deptoLocal_encomiendaubicacion, source => source.MapFrom(p => p.DeptoLocalEncomiendaUbicacion))
                    .ForMember(dest => dest.id_zonaplaneamiento, source => source.MapFrom(p => p.IdZonaPlaneamiento))
                    .ForMember(dest => dest.Encomienda_Ubicaciones_PropiedadHorizontal, source => source.MapFrom(p => p.EncomiendaUbicacionesPropiedadHorizontalDTO))
                    .ForMember(dest => dest.Encomienda_Ubicaciones_Puertas, source => source.MapFrom(p => p.EncomiendaUbicacionesPuertasDTO))
                    .ForMember(dest => dest.SubTiposDeUbicacion, source => source.MapFrom(p => p.SubTipoUbicacionesDTO))
                    .ForMember(dest => dest.Zonas_Planeamiento, source => source.MapFrom(p => p.ZonasPlaneamientoDTO))
                    .ForMember(dest => dest.Ubicaciones, source => source.MapFrom(p => p.Ubicacion))
                    .ForMember(dest => dest.Encomienda_Ubicaciones_Distritos, source => source.MapFrom(p => p.EncomiendaUbicacionesDistritosDTO))
                    .ForMember(dest => dest.Encomienda_Ubicaciones_Mixturas, source => source.MapFrom(p => p.EncomiendaUbicacionesMixturasDTO));
                #endregion
                #region "Encomienda_Ubicaciones_PropiedadHorizontal"
                cfg.CreateMap<Encomienda_Ubicaciones_PropiedadHorizontal, EncomiendaUbicacionesPropiedadHorizontalDTO>()
                    .ForMember(dest => dest.IdEncomiendaPropiedadHorizontal, source => source.MapFrom(p => p.id_encomiendaprophorizontal))
                    .ForMember(dest => dest.IdEncomiendaUbicacion, source => source.MapFrom(p => p.id_encomiendaubicacion))
                    .ForMember(dest => dest.IdPropiedadHorizontal, source => source.MapFrom(p => p.id_propiedadhorizontal));

                cfg.CreateMap<EncomiendaUbicacionesPropiedadHorizontalDTO, Encomienda_Ubicaciones_PropiedadHorizontal>()
                  .ForMember(dest => dest.id_encomiendaprophorizontal, source => source.MapFrom(p => p.IdEncomiendaPropiedadHorizontal))
                  .ForMember(dest => dest.id_encomiendaubicacion, source => source.MapFrom(p => p.IdEncomiendaUbicacion))
                  .ForMember(dest => dest.id_propiedadhorizontal, source => source.MapFrom(p => p.IdPropiedadHorizontal));
                #endregion
                #region "Encomienda_Ubicaciones_Puertas"
                cfg.CreateMap<Encomienda_Ubicaciones_Puertas, EncomiendaUbicacionesPuertasDTO>()
                    .ForMember(dest => dest.IdEncomiendaPuerta, source => source.MapFrom(p => p.id_encomiendapuerta))
                    .ForMember(dest => dest.IdEncomiendaUbicacion, source => source.MapFrom(p => p.id_encomiendaubicacion))
                    .ForMember(dest => dest.CodigoCalle, source => source.MapFrom(p => p.codigo_calle))
                    .ForMember(dest => dest.NombreCalle, source => source.MapFrom(p => p.nombre_calle))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NroPuerta));

                cfg.CreateMap<EncomiendaUbicacionesPuertasDTO, Encomienda_Ubicaciones_Puertas>()
                    .ForMember(dest => dest.id_encomiendapuerta, source => source.MapFrom(p => p.IdEncomiendaPuerta))
                    .ForMember(dest => dest.id_encomiendaubicacion, source => source.MapFrom(p => p.IdEncomiendaUbicacion))
                    .ForMember(dest => dest.codigo_calle, source => source.MapFrom(p => p.CodigoCalle))
                    .ForMember(dest => dest.nombre_calle, source => source.MapFrom(p => p.NombreCalle))
                    .ForMember(dest => dest.NroPuerta, source => source.MapFrom(p => p.NroPuerta));
                #endregion
                #region mixturas
                cfg.CreateMap<Encomienda_Ubicaciones_Mixturas, Encomienda_Ubicaciones_MixturasDTO>()
                .ForMember(dest => dest.UbicacionesZonasMixturasDTO, source => source.MapFrom(p => p.Ubicaciones_ZonasMixtura)); ;

                cfg.CreateMap<Encomienda_Ubicaciones_MixturasDTO, Encomienda_Ubicaciones_Mixturas>()
                .ForMember(dest => dest.Ubicaciones_ZonasMixtura, source => source.MapFrom(p => p.UbicacionesZonasMixturasDTO)); ;
                #endregion

                #region distritos
                cfg.CreateMap<Encomienda_Ubicaciones_Distritos, Encomienda_Ubicaciones_DistritosDTO>()
                .ForMember(dest => dest.UbicacionesCatalogoDistritosDTO, source => source.MapFrom(p => p.Ubicaciones_CatalogoDistritos)); ;

                cfg.CreateMap<Encomienda_Ubicaciones_DistritosDTO, Encomienda_Ubicaciones_Distritos>()
                .ForMember(dest => dest.Ubicaciones_CatalogoDistritos, source => source.MapFrom(p => p.UbicacionesCatalogoDistritosDTO)); ;
                #endregion
                #region "SSIT_Solicitudes"
                cfg.CreateMap<SSIT_Solicitudes, SSITSolicitudesDTO>()
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.id_tipotramite))
                    .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.id_tipoexpediente))
                    .ForMember(dest => dest.IdSubTipoExpediente, source => source.MapFrom(p => p.id_subtipoexpediente))
                    .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado))
                    .ForMember(dest => dest.Telefono, source => source.MapFrom(p => p.telefono))

                    .ForMember(dest => dest.SSITSolicitudesOrigenDTO, source => source.MapFrom(p => p.SSIT_Solicitudes_Origen))
                    ;

                cfg.CreateMap<SSITSolicitudesDTO, SSIT_Solicitudes>()
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_tipotramite, source => source.MapFrom(p => p.IdTipoTramite))
                    .ForMember(dest => dest.id_tipoexpediente, source => source.MapFrom(p => p.IdTipoExpediente))
                    .ForMember(dest => dest.id_subtipoexpediente, source => source.MapFrom(p => p.IdSubTipoExpediente))
                    .ForMember(dest => dest.id_estado, source => source.MapFrom(p => p.IdEstado))
                    .ForMember(dest => dest.telefono, source => source.MapFrom(p => p.Telefono));

                cfg.CreateMap<SSIT_Solicitudes_Origen, SSITSolicitudesOrigenDTO>()
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.id_solicitud_origen, source => source.MapFrom(p => p.id_solicitud_origen))
                    .ForMember(dest => dest.id_transf_origen, source => source.MapFrom(p => p.id_transf_origen))
                    ;

                cfg.CreateMap<SSITSolicitudesOrigenDTO, SSIT_Solicitudes_Origen>()
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.id_solicitud_origen, source => source.MapFrom(p => p.id_solicitud_origen))
                    .ForMember(dest => dest.id_transf_origen, source => source.MapFrom(p => p.id_transf_origen))
                    ;
                #endregion
                #region "ConsejoProfesional"
                cfg.CreateMap<ConsejoProfesional, ConsejoProfesionalDTO>()
                    .ForMember(dest => dest.GrupoConsejosDTO, source => source.MapFrom(p => p.GrupoConsejos));

                cfg.CreateMap<ConsejoProfesionalDTO, ConsejoProfesional>()
                    .ForMember(dest => dest.GrupoConsejos, source => source.MapFrom(p => p.GrupoConsejosDTO));
                #endregion
                #region "EntidadNormativa"
                cfg.CreateMap<EntidadNormativaDTO, EntidadNormativa>();

                cfg.CreateMap<EntidadNormativa, EntidadNormativaDTO>();
                #endregion
                #region "ImpactoAmbiental"
                cfg.CreateMap<ImpactoAmbientalDTO, ImpactoAmbiental>();

                cfg.CreateMap<ImpactoAmbiental, ImpactoAmbientalDTO>();
                #endregion
                #region "Localidad"
                cfg.CreateMap<LocalidadDTO, Localidad>()
                    .ForMember(dest => dest.Provincia, source => source.MapFrom(p => p.ProvinciaDTO));

                cfg.CreateMap<Localidad, LocalidadDTO>()
                    .ForMember(dest => dest.ProvinciaDTO, source => source.MapFrom(p => p.Provincia));
                #endregion
                #region "Profesional"
                cfg.CreateMap<Profesional, ProfesionalDTO>()
                    .ForMember(dest => dest.ConsejoProfesionalDTO, source => source.MapFrom(p => p.ConsejoProfesional));

                cfg.CreateMap<ProfesionalDTO, Profesional>()
                    .ForMember(dest => dest.ConsejoProfesional, source => source.MapFrom(p => p.ConsejoProfesionalDTO));
                #endregion
                #region "ConsejoProfesional"
                cfg.CreateMap<ConsejoProfesional, ConsejoProfesionalDTO>()
                    .ForMember(dest => dest.GrupoConsejosDTO, source => source.MapFrom(p => p.GrupoConsejos));

                cfg.CreateMap<ConsejoProfesionalDTO, ConsejoProfesional>()
                    .ForMember(dest => dest.GrupoConsejos, source => source.MapFrom(p => p.GrupoConsejosDTO));
                #endregion
                #region "GrupoConsejos"
                cfg.CreateMap<GrupoConsejos, GrupoConsejosDTO>()
                    .ForMember(dest => dest.Descripcion, source => source.MapFrom(p => p.descripcion_grupoconsejo))
                    .ForMember(dest => dest.Id, source => source.MapFrom(p => p.id_grupoconsejo))
                    .ForMember(dest => dest.LogoImpresion, source => source.MapFrom(p => p.logo_impresion_grupoconsejo))
                    .ForMember(dest => dest.LogoPantalla, source => source.MapFrom(p => p.logo_pantalla_grupoconsejo))
                    .ForMember(dest => dest.Nombre, source => source.MapFrom(p => p.nombre_grupoconsejo));

                cfg.CreateMap<GrupoConsejosDTO, GrupoConsejos>()
                    .ForMember(dest => dest.descripcion_grupoconsejo, source => source.MapFrom(p => p.Descripcion))
                    .ForMember(dest => dest.id_grupoconsejo, source => source.MapFrom(p => p.Id))
                    .ForMember(dest => dest.logo_impresion_grupoconsejo, source => source.MapFrom(p => p.LogoImpresion))
                    .ForMember(dest => dest.logo_pantalla_grupoconsejo, source => source.MapFrom(p => p.LogoPantalla))
                    .ForMember(dest => dest.nombre_grupoconsejo, source => source.MapFrom(p => p.Nombre));
                #endregion
                #region "Provincia"
                cfg.CreateMap<ProvinciaDTO, Provincia>();

                cfg.CreateMap<Provincia, ProvinciaDTO>();
                #endregion
                #region "Tipo_Documentacion_Req"
                cfg.CreateMap<TipoDocumentacionRequeridaDTO, Tipo_Documentacion_Req>();

                cfg.CreateMap<Tipo_Documentacion_Req, TipoDocumentacionRequeridaDTO>();
                #endregion
                #region "tipo_ventilacion"
                cfg.CreateMap<TipoVentilacionDTO, tipo_ventilacion>();

                cfg.CreateMap<tipo_ventilacion, TipoVentilacionDTO>();
                #endregion
                #region "TipoActividad"
                cfg.CreateMap<TipoActividadDTO, TipoActividad>();

                cfg.CreateMap<TipoActividad, TipoActividadDTO>();
                #endregion
                #region "TipoDestino"
                cfg.CreateMap<TipoDestinoDTO, TipoDestino>();

                cfg.CreateMap<TipoDestino, TipoDestinoDTO>();
                #endregion
                #region "TipoDocumentoPersonal"
                cfg.CreateMap<TipoDocumentoPersonalDTO, TipoDocumentoPersonal>();

                cfg.CreateMap<TipoDocumentoPersonal, TipoDocumentoPersonalDTO>();
                #endregion
                #region "TipoIluminasion"
                cfg.CreateMap<TipoIluminacionDTO, tipo_iluminacion>();

                cfg.CreateMap<tipo_iluminacion, TipoIluminacionDTO>();
                #endregion
                #region "TipoNormativa"
                cfg.CreateMap<TipoNormativaDTO, TipoNormativa>();

                cfg.CreateMap<TipoNormativa, TipoNormativaDTO>();
                #endregion
                #region "TipoSector"
                cfg.CreateMap<TipoSectorDTO, TipoSector>();

                cfg.CreateMap<TipoSector, TipoSectorDTO>();
                #endregion
                #region "TiposDeCaracterLegal"
                cfg.CreateMap<TiposDeCaracterLegal, TiposDeCaracterLegalDTO>()
                    .ForMember(dest => dest.IdTipoCaracter, source => source.MapFrom(p => p.id_tipocaracter))
                    .ForMember(dest => dest.CodTipoCaracter, source => source.MapFrom(p => p.cod_tipocaracter))
                    .ForMember(dest => dest.NomTipoCaracter, source => source.MapFrom(p => p.nom_tipocaracter))
                    .ForMember(dest => dest.DisponibilidadTipoCaracter, source => source.MapFrom(p => p.disponibilidad_tipocaracter))
                    .ForMember(dest => dest.MuestraCargoTipoCaracter, source => source.MapFrom(p => p.muestracargo_tipocaracter));

                cfg.CreateMap<TiposDeCaracterLegalDTO, TiposDeCaracterLegal>()
                    .ForMember(dest => dest.id_tipocaracter, source => source.MapFrom(p => p.IdTipoCaracter))
                    .ForMember(dest => dest.cod_tipocaracter, source => source.MapFrom(p => p.CodTipoCaracter))
                    .ForMember(dest => dest.nom_tipocaracter, source => source.MapFrom(p => p.NomTipoCaracter))
                    .ForMember(dest => dest.disponibilidad_tipocaracter, source => source.MapFrom(p => p.DisponibilidadTipoCaracter))
                    .ForMember(dest => dest.muestracargo_tipocaracter, source => source.MapFrom(p => p.MuestraCargoTipoCaracter));
                #endregion
                #region "TiposDeDocumentosRequeridos"
                cfg.CreateMap<TiposDeDocumentosRequeridosDTO, TiposDeDocumentosRequeridos>();

                cfg.CreateMap<TiposDeDocumentosRequeridos, TiposDeDocumentosRequeridosDTO>();
                #endregion
                #region "TiposDeDocumentosSistema"
                cfg.CreateMap<TiposDeDocumentosSistemaDTO, TiposDeDocumentosSistema>();

                cfg.CreateMap<TiposDeDocumentosSistema, TiposDeDocumentosSistemaDTO>();
                #endregion
                #region "TiposDeIngresosBrutos"
                cfg.CreateMap<TiposDeIngresosBrutos, TiposDeIngresosBrutosDTO>()
                    .ForMember(dest => dest.IdTipoIb, source => source.MapFrom(p => p.id_tipoiibb))
                    .ForMember(dest => dest.CodTipoIb, source => source.MapFrom(p => p.cod_tipoibb))
                    .ForMember(dest => dest.NomTipoIb, source => source.MapFrom(p => p.nom_tipoiibb))
                    .ForMember(dest => dest.FormatoTipoIb, source => source.MapFrom(p => p.formato_tipoiibb));

                cfg.CreateMap<TiposDeIngresosBrutosDTO, TiposDeIngresosBrutos>()
                    .ForMember(dest => dest.id_tipoiibb, source => source.MapFrom(p => p.IdTipoIb))
                    .ForMember(dest => dest.cod_tipoibb, source => source.MapFrom(p => p.CodTipoIb))
                    .ForMember(dest => dest.nom_tipoiibb, source => source.MapFrom(p => p.NomTipoIb))
                    .ForMember(dest => dest.formato_tipoiibb, source => source.MapFrom(p => p.FormatoTipoIb));
                #endregion
                #region "TiposDePlanos"
                cfg.CreateMap<TiposDePlanosDTO, TiposDePlanos>();

                cfg.CreateMap<TiposDePlanos, TiposDePlanosDTO>();
                #endregion
                #region "SubTiposDeUbicacion"
                cfg.CreateMap<SubTiposDeUbicacion, SubTipoUbicacionesDTO>()
                    .ForMember(dest => dest.TiposDeUbicacionDTO, source => source.MapFrom(p => p.TiposDeUbicacion));

                cfg.CreateMap<SubTipoUbicacionesDTO, SubTiposDeUbicacion>()
                    .ForMember(dest => dest.TiposDeUbicacion, source => source.MapFrom(p => p.TiposDeUbicacionDTO));
                #endregion
                #region "TiposDeUbicacion"
                cfg.CreateMap<TiposDeUbicacion, TiposDeUbicacionDTO>()
                    .ForMember(dest => dest.IdTipoUbicacion, source => source.MapFrom(p => p.id_tipoubicacion))
                    .ForMember(dest => dest.DescripcionTipoUbicacion, source => source.MapFrom(p => p.descripcion_tipoubicacion))
                    .ForMember(dest => dest.SubTipoUbicacionesDTO, source => source.MapFrom(p => p.SubTiposDeUbicacion));

                cfg.CreateMap<TiposDeUbicacionDTO, TiposDeUbicacion>()
                    .ForMember(dest => dest.id_tipoubicacion, source => source.MapFrom(p => p.IdTipoUbicacion))
                    .ForMember(dest => dest.descripcion_tipoubicacion, source => source.MapFrom(p => p.DescripcionTipoUbicacion))
                    .ForMember(dest => dest.SubTiposDeUbicacion, source => source.MapFrom(p => p.SubTipoUbicacionesDTO));
                #endregion
                #region "TipoSociedad"
                cfg.CreateMap<TipoSociedadDTO, TipoSociedad>();

                cfg.CreateMap<TipoSociedad, TipoSociedadDTO>();
                #endregion
                #region "TipoSuperficie"
                cfg.CreateMap<TipoSuperficieDTO, TipoSuperficie>();

                cfg.CreateMap<TipoSuperficie, TipoSuperficieDTO>();
                #endregion
                #region "TipoTramite"
                cfg.CreateMap<TipoTramiteDTO, TipoTramite>()
                    .ForMember(dest => dest.id_tipotramite, source => source.MapFrom(p => p.IdTipoTramite))
                    .ForMember(dest => dest.cod_tipotramite, source => source.MapFrom(p => p.CodTipoTramite))
                    .ForMember(dest => dest.descripcion_tipotramite, source => source.MapFrom(p => p.DescripcionTipoTramite))
                    .ForMember(dest => dest.cod_tipotramite_ws, source => source.MapFrom(p => p.CodTipoTramiteWs));

                cfg.CreateMap<TipoTramite, TipoTramiteDTO>()
                    .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.id_tipotramite))
                    .ForMember(dest => dest.CodTipoTramite, source => source.MapFrom(p => p.cod_tipotramite))
                    .ForMember(dest => dest.DescripcionTipoTramite, source => source.MapFrom(p => p.descripcion_tipotramite))
                    .ForMember(dest => dest.CodTipoTramite, source => source.MapFrom(p => p.cod_tipotramite_ws));
                #endregion
                #region "TipoExpediente"
                cfg.CreateMap<TipoExpedienteDTO, TipoExpediente>()
                    .ForMember(dest => dest.id_tipoexpediente, source => source.MapFrom(p => p.IdTipoExpediente))
                    .ForMember(dest => dest.cod_tipoexpediente, source => source.MapFrom(p => p.CodTipoExpediente))
                    .ForMember(dest => dest.descripcion_tipoexpediente, source => source.MapFrom(p => p.DescripcionTipoExpediente))
                    .ForMember(dest => dest.cod_tipoexpediente_ws, source => source.MapFrom(p => p.CodTipoExpedienteWs));

                cfg.CreateMap<TipoExpediente, TipoExpedienteDTO>()
                    .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.id_tipoexpediente))
                    .ForMember(dest => dest.CodTipoExpediente, source => source.MapFrom(p => p.cod_tipoexpediente))
                    .ForMember(dest => dest.DescripcionTipoExpediente, source => source.MapFrom(p => p.descripcion_tipoexpediente))
                    .ForMember(dest => dest.CodTipoExpedienteWs, source => source.MapFrom(p => p.cod_tipoexpediente_ws));
                #endregion
                #region "Ubicaciones"
                cfg.CreateMap<Ubicaciones, UbicacionesDTO>()
                     .ForMember(dest => dest.IdUbicacion, source => source.MapFrom(p => p.id_ubicacion))
                     .ForMember(dest => dest.UbicacionesZonasMixturasDTO, source => source.MapFrom(p => p.Ubicaciones_ZonasMixtura))
                     .ForMember(dest => dest.UbicacionesDistritosDTO, source => source.MapFrom(p => p.Ubicaciones_Distritos));
                cfg.CreateMap<UbicacionesDTO, Ubicaciones>()
                     .ForMember(dest => dest.id_ubicacion, source => source.MapFrom(p => p.IdUbicacion))
                     .ForMember(dest => dest.Ubicaciones_ZonasMixtura, source => source.MapFrom(p => p.UbicacionesZonasMixturasDTO))
                     .ForMember(dest => dest.Ubicaciones_Distritos, source => source.MapFrom(p => p.UbicacionesDistritosDTO));
                #endregion
                #region "SubTipoExpediente"
                cfg.CreateMap<SubTipoExpedienteDTO, SubtipoExpediente>()
                    .ForMember(dest => dest.id_subtipoexpediente, source => source.MapFrom(p => p.IdSubTipoExpediente))
                    .ForMember(dest => dest.cod_subtipoexpediente, source => source.MapFrom(p => p.CodSubTipoExpediente))
                    .ForMember(dest => dest.descripcion_subtipoexpediente, source => source.MapFrom(p => p.DescripcionSubTipoExpediente))
                    .ForMember(dest => dest.cod_subtipoexpediente_ws, source => source.MapFrom(p => p.CodSubTipoExpedienteWs));

                cfg.CreateMap<SubtipoExpediente, SubTipoExpedienteDTO>()
                    .ForMember(dest => dest.IdSubTipoExpediente, source => source.MapFrom(p => p.id_subtipoexpediente))
                    .ForMember(dest => dest.CodSubTipoExpediente, source => source.MapFrom(p => p.cod_subtipoexpediente))
                    .ForMember(dest => dest.DescripcionSubTipoExpediente, source => source.MapFrom(p => p.descripcion_subtipoexpediente))
                    .ForMember(dest => dest.CodSubTipoExpedienteWs, source => source.MapFrom(p => p.cod_subtipoexpediente_ws));
                #endregion
                #region "Zonas_Planeamiento"
                cfg.CreateMap<Zonas_Planeamiento, ZonasPlaneamientoDTO>()
                    .ForMember(dest => dest.IdZonaPlaneamiento, source => source.MapFrom(p => p.id_zonaplaneamiento));

                cfg.CreateMap<ZonasPlaneamientoDTO, Zonas_Planeamiento>()
                    .ForMember(dest => dest.id_zonaplaneamiento, source => source.MapFrom(p => p.IdZonaPlaneamiento));

                #endregion
                #region "Ubicaciones_ZonasMixtura"
                cfg.CreateMap<Ubicaciones_ZonasMixtura, UbicacionesZonasMixturasDTO>()
                     .ForMember(dest => dest.IdZona, source => source.MapFrom(p => p.IdZonaMixtura));
                cfg.CreateMap<UbicacionesZonasMixturasDTO, Ubicaciones_ZonasMixtura>()
                     .ForMember(dest => dest.IdZonaMixtura, source => source.MapFrom(p => p.IdZona));
                #endregion
                #region "Ubicaciones_Distritos"
                cfg.CreateMap<Ubicaciones_Distritos, UbicacionesDistritosDTO>();
                cfg.CreateMap<UbicacionesDistritosDTO, Ubicaciones_Distritos>();
                #endregion
                #region "Ubicaciones_CatalogoDistritos"
                cfg.CreateMap<Ubicaciones_CatalogoDistritos, UbicacionesCatalogoDistritosDTO>(); 
                cfg.CreateMap<UbicacionesCatalogoDistritosDTO, Ubicaciones_CatalogoDistritos>();
                #endregion
                #region "Encomienda_RubrosCN"
                cfg.CreateMap<Encomienda_RubrosCN, EncomiendaRubrosCNDTO>()
                    .ForMember(dest => dest.CodigoRubro, source => source.MapFrom(p => p.CodigoRubro))
                    .ForMember(dest => dest.IdEncomiendaRubro, source => source.MapFrom(p => p.id_encomiendarubro))
                    .ForMember(dest => dest.IdEncomienda, source => source.MapFrom(p => p.id_encomienda))
                    .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.IdTipoExpediente))
                    .ForMember(dest => dest.DescripcionRubro, source => source.MapFrom(p => p.NombreRubro))
                    .ForMember(dest => dest.IdTipoActividad, source => source.MapFrom(p => p.IdTipoActividad))
                    .ForMember(dest => dest.ImpactoAmbientalDTO, source => source.MapFrom(p => p.ImpactoAmbiental))
                    .ForMember(dest => dest.RubrosDTO, source => source.MapFrom(p => p.RubrosCN))
                    ;


                cfg.CreateMap<EncomiendaRubrosCNDTO, Encomienda_RubrosCN>()
                    .ForMember(dest => dest.id_encomiendarubro, source => source.MapFrom(p => p.IdEncomiendaRubro))
                    .ForMember(dest => dest.id_encomienda, source => source.MapFrom(p => p.IdEncomienda))
                    .ForMember(dest => dest.CodigoRubro, source => source.MapFrom(p => p.CodigoRubro))
                    .ForMember(dest => dest.NombreRubro, source => source.MapFrom(p => p.DescripcionRubro))
                    .ForMember(dest => dest.IdTipoActividad, source => source.MapFrom(p => p.IdTipoActividad))
                    .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.IdTipoExpediente))
                    .ForMember(dest => dest.ImpactoAmbiental, source => source.MapFrom(p => p.ImpactoAmbientalDTO))
                    .ForMember(dest => dest.RubrosCN, source => source.MapFrom(p => p.RubrosDTO))
                    ;
                #endregion
                #region Escribanos
                cfg.CreateMap<wsEscribanosActaNotarialDTO, wsEscribanos_ActaNotarial>()
                    .ForMember(dest => dest.Escribano, source => source.MapFrom(p => p.Escribano));

                cfg.CreateMap<wsEscribanos_ActaNotarial, wsEscribanosActaNotarialDTO>()
                    .ForMember(dest => dest.Escribano, source => source.MapFrom(p => p.Escribano));


                cfg.CreateMap<Escribano, EscribanoDTO>();

                cfg.CreateMap<EscribanoDTO, Escribano>();
                #endregion 
                #region
                    cfg.CreateMap<ImpactoAmbientalDTO, ImpactoAmbiental>();
                    cfg.CreateMap<ImpactoAmbiental, ImpactoAmbientalDTO>();
                #endregion
                #region Encomienda_SSIT_Solicitudes
                cfg.CreateMap<Encomienda_SSIT_Solicitudes, EncomiendaSSITSolicitudesDTO>()
                .ForMember(dest => dest.SSITSolicitudesDTO, source => source.MapFrom(p => p.SSIT_Solicitudes)); ;

                cfg.CreateMap<EncomiendaSSITSolicitudesDTO, Encomienda_SSIT_Solicitudes>()
                .ForMember(dest => dest.SSIT_Solicitudes, source => source.MapFrom(p => p.SSITSolicitudesDTO)); ;
                #endregion
                #region Encomienda_Transf_Solicitudes
                cfg.CreateMap<Encomienda_Transf_Solicitudes, EncomiendaTransfSolicitudesDTO>()
                .ForMember(dest => dest.TransferenciasSolicitudesDTO, source => source.MapFrom(p => p.Transf_Solicitudes))
                .ReverseMap(); 

                //cfg.CreateMap<EncomiendaTransfSolicitudesDTO, Encomienda_Transf_Solicitudes>()
                //.ForMember(dest => dest.Transf_Solicitudes, source => source.MapFrom(p => p.TransferenciasSolicitudesDTO)); ;
                #endregion
                #region "Transf_Solicitudes"
                cfg.CreateMap<Transf_Solicitudes, TransferenciasSolicitudesDTO>()
                    .ForMember(dest => dest.IdSolicitud, source => source.MapFrom(p => p.id_solicitud))
                    .ForMember(dest => dest.IdTipoTramite, source => source.MapFrom(p => p.id_tipotramite))
                    .ForMember(dest => dest.IdTipoExpediente, source => source.MapFrom(p => p.id_tipoexpediente))
                    .ForMember(dest => dest.IdSubTipoExpediente, source => source.MapFrom(p => p.id_subtipoexpediente))
                    .ForMember(dest => dest.IdEstado, source => source.MapFrom(p => p.id_estado));

                cfg.CreateMap<TransferenciasSolicitudesDTO, Transf_Solicitudes>()
                    .ForMember(dest => dest.id_solicitud, source => source.MapFrom(p => p.IdSolicitud))
                    .ForMember(dest => dest.id_tipotramite, source => source.MapFrom(p => p.IdTipoTramite))
                    .ForMember(dest => dest.id_tipoexpediente, source => source.MapFrom(p => p.IdTipoExpediente))
                    .ForMember(dest => dest.id_subtipoexpediente, source => source.MapFrom(p => p.IdSubTipoExpediente))
                    .ForMember(dest => dest.id_estado, source => source.MapFrom(p => p.IdEstado));
                #endregion
                #region "UbicacionesGrupoDistrito"
                cfg.CreateMap<Ubicaciones_GruposDistritos, UbicacionesGruposDistritosDTO>();
                #endregion
                #region "SSIT_Permisos_DatosAdicionales"
                cfg.CreateMap<SSIT_Permisos_DatosAdicionales, SSITPermisosDatosAdicionalesDTO>().ReverseMap();
                #endregion

                cfg.CreateMap<Encomienda_RubrosCN_DepositoDTO, Encomienda_RubrosCN_Deposito>()
                .ForMember(dest => dest.RubrosDepositosCN, source => source.MapFrom(p => p.RubrosDepositosCNDTO))
                .ForMember(dest => dest.RubrosCN, source => source.MapFrom(p => p.RubrosCNDTO));

                cfg.CreateMap<Encomienda_RubrosCN_Deposito, Encomienda_RubrosCN_DepositoDTO>()
               .ForMember(dest => dest.RubrosDepositosCNDTO, source => source.MapFrom(p => p.RubrosDepositosCN))
               .ForMember(dest => dest.RubrosCNDTO, source => source.MapFrom(p => p.RubrosCN));

                cfg.CreateMap<RubrosDepositosCN, RubrosDepositosCNDTO>().ReverseMap();
                cfg.CreateMap<CondicionesIncendio, CondicionesIncendioDTO>().ReverseMap();
                cfg.CreateMap<RubrosDepositosCN_RangosSuperficie, RubrosDepositosCN_RangosSuperficieDTO>().ReverseMap();

                cfg.CreateMap<RubrosCN, RubrosCNDTO>()
                    .ForMember(dest => dest.CondicionesIncendio, source => source.MapFrom(p => p.CondicionesIncendio))
                    .ReverseMap();

                #region "CondicionesIncendio"
                cfg.CreateMap <CondicionesIncendio, CondicionesIncendioDTO> ()
                     //.ForMember(dest => dest.idCondicionIncendio, source => source.MapFrom(p => p.idCondicionIncendio))
                     //.ForMember(dest => dest.codigo, source => source.MapFrom(p => p.codigo))
                     //.ForMember(dest => dest.superficie, source => source.MapFrom(p => p.superficie))
                     //.ForMember(dest => dest.superficieSubsuelo, source => source.MapFrom(p => p.superficieSubsuelo))
                     .ReverseMap();
                #endregion
            });

            MapperBaseEncomienda = config.CreateMapper();
        }

    }
}