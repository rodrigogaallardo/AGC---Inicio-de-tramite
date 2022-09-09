using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;
using StaticClass;
using DataTransferObject;

namespace BaseRepository
{
    public class TitularesRepository : BaseRepository<TitularesEntity>
    {
		private readonly IUnitOfWork _unitOfWork;

        public TitularesRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }

        public IEnumerable<TitularesEntity> GetTitularesEncomienda(int id_encomienda)
        {

            var lstTitulares = (from pf in _unitOfWork.Db.Encomienda_Titulares_PersonasFisicas
                                where pf.id_encomienda == id_encomienda
                                select new TitularesEntity
                                {
                                    IdPersona = pf.id_personafisica,
                                    TipoPersona = Constantes.TipoPersonaFisica,
                                    TipoPersonaDesc = Constantes.TipoPersonaFisica_Desc,
                                    ApellidoNomRazon = pf.Apellido + " " + pf.Nombres,
                                    CUIT = pf.Cuit,
                                    Domicilio = pf.Calle + " " + pf.Nro_Puerta.ToString() +
                                                 (pf.Torre.Length > 0 ? " Torre: " + pf.Torre : "") +
                                                (pf.Piso.Length > 0 ? " Piso: " + pf.Piso : "") +
                                                (pf.Depto.Length > 0 ? " Depto/UF: " + pf.Depto : ""),
                                    tipo_doc = 0
                                }).Union(
                                    (from pj in _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas
                                     where pj.id_encomienda == id_encomienda
                                     select new TitularesEntity
                                     {
                                         IdPersona = pj.id_personajuridica,
                                         TipoPersona = Constantes.TipoPersonaJuridica,
                                         TipoPersonaDesc = Constantes.TipoPersonaJuridica_Desc,
                                         ApellidoNomRazon = pj.Razon_Social,
                                         CUIT = pj.CUIT,
                                         Domicilio = pj.Calle + " " + (pj.NroPuerta.HasValue ? pj.NroPuerta.Value.ToString() : "") +
                                                    (pj.Torre.Length > 0 ? " Torre: " + pj.Torre : "") +
                                                    (pj.Piso.Length > 0 ? " Piso: " + pj.Piso : "") +
                                                    (pj.Depto.Length > 0 ? "  Depto/UF: " + pj.Depto : ""),
                                         tipo_doc = 0
                                     }));
            return lstTitulares;
        }
        public IEnumerable<TitularesSHEntity> GetTitularesSHSolicitud(int IdPersonaJuridica)
        {
            var lstTitularesSH = (from titsh in _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas
                                  join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on titsh.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                                  join firsh in _unitOfWork.Db.SSIT_Solicitudes_Firmantes_PersonasJuridicas on titsh.id_firmante_pj equals firsh.id_firmante_pj
                                  where titsh.id_personajuridica == IdPersonaJuridica
                                  select new TitularesSHEntity
                                  {
                                      RowId = Guid.NewGuid(),
                                      IdPersonaJuridica = titsh.id_personajuridica,
                                      Apellidos = titsh.Apellido,
                                      Nombres = titsh.Nombres,
                                      TipoDoc = tdoc.Nombre,
                                      NroDoc = titsh.Nro_Documento,
                                      IdTipoDocPersonal = titsh.id_tipodoc_personal,
                                      Email = titsh.Email,
                                      IdFirmantePj = titsh.id_firmante_pj,
                                      Cuit = titsh.Cuit
                                  });
            return lstTitularesSH;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdPersonaJuridica"></param>
        /// <returns></returns>
        public IEnumerable<TitularesSHEntity> GetTitularesEncomiendaSH(int IdPersonaJuridica)
        {
            var lstTitularesSH = (from titsh in _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas
                                  join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on titsh.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                                  join firsh in _unitOfWork.Db.Encomienda_Firmantes_PersonasJuridicas on titsh.id_firmante_pj equals firsh.id_firmante_pj
                                  where titsh.id_personajuridica == IdPersonaJuridica
                                  select new TitularesSHEntity
                                  {
                                      RowId = Guid.NewGuid(),
                                      IdPersonaJuridica = titsh.id_personajuridica,
                                      Apellidos = titsh.Apellido,
                                      Nombres = titsh.Nombres,
                                      TipoDoc = tdoc.Nombre,
                                      NroDoc = titsh.Nro_Documento,
                                      IdTipoDocPersonal = titsh.id_tipodoc_personal,
                                      Email = titsh.Email,
                                      IdFirmantePj = titsh.id_firmante_pj
                                  });
            return lstTitularesSH;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdPersonaJuridica"></param>
        /// <returns></returns>
        public IEnumerable<TitularesSHEntity> GetTitularesTransferenciaSH(int IdPersonaJuridica)
        {
            var lstTitularesSH = (from titsh in _unitOfWork.Db.Transf_Titulares_PersonasJuridicas_PersonasFisicas
                                  join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on titsh.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                                  join firsh in _unitOfWork.Db.Transf_Firmantes_PersonasJuridicas on titsh.id_firmante_pj equals firsh.id_firmante_pj
                                  where titsh.id_personajuridica == IdPersonaJuridica
                                  select new TitularesSHEntity
                                  {
                                      RowId = Guid.NewGuid(),
                                      IdPersonaJuridica = titsh.id_personajuridica,
                                      Apellidos = titsh.Apellido,
                                      Nombres = titsh.Nombres,
                                      TipoDoc = tdoc.Nombre,
                                      NroDoc = titsh.Nro_Documento,
                                      IdTipoDocPersonal = titsh.id_tipodoc_personal,
                                      Email = titsh.Email,
                                      IdFirmantePj = titsh.id_firmante_pj
                                  });
            return lstTitularesSH;
        }

        public IEnumerable<TitularesSHEntity> GetTitularesTransferenciaSHANT(int IdPersonaJuridica)
        {
            var lstTitularesSH = (from titsh in _unitOfWork.Db.Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas
                                  join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on titsh.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                                  //join firsh in _unitOfWork.Db.Transf_Firmantes_Solicitud_PersonasJuridicas on titsh.id_personajuridica equals firsh.id_firmante_pj
                                  where titsh.id_personajuridica == IdPersonaJuridica
                                  select new TitularesSHEntity
                                  {
                                      RowId = Guid.NewGuid(),
                                      IdPersonaJuridica = titsh.id_personajuridica,
                                      Apellidos = titsh.Apellido,
                                      Nombres = titsh.Nombres,
                                      TipoDoc = tdoc.Nombre,
                                      NroDoc = titsh.Nro_Documento,
                                      IdTipoDocPersonal = titsh.id_tipodoc_personal,
                                      Email = titsh.Email,
                                      IdFirmantePj = titsh.id_personajuridica
                                  });
            return lstTitularesSH;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdPersonaJuridica"></param>
        /// <returns></returns>
        public IEnumerable<TitularesSHEntity> GetTitularesSHConsultaPadron(int IdPersonaJuridica)
        {
            var lstTitularesSH = (from titsh in _unitOfWork.Db.CPadron_Titulares_PersonasJuridicas_PersonasFisicas
                                  join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on titsh.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                                  //join firsh in _unitOfWork.Db.Encomienda_Firmantes_PersonasJuridicas on titsh.id_personajuridica equals firsh.id_firmante_pj
                                  where titsh.id_personajuridica == IdPersonaJuridica
                                  select new TitularesSHEntity
                                  {
                                      RowId = Guid.NewGuid(),
                                      IdPersonaJuridica = titsh.id_personajuridica,
                                      Apellidos = titsh.Apellido,
                                      Nombres = titsh.Nombres,
                                      TipoDoc = tdoc.Nombre,
                                      NroDoc = titsh.Nro_Documento,
                                      IdTipoDocPersonal = titsh.id_tipodoc_personal,
                                      Email = titsh.Email,
                                      IdFirmantePj = 0//titsh.id_firmante_pj
                                  });
            return lstTitularesSH;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdPersonaJuridica"></param>
        /// <returns></returns>
        public IEnumerable<TitularesSHEntity> GetTitularesSolicitudSHConsultaPadron(int IdPersonaJuridica)
        {
            var lstTitularesSH = (from titsh in _unitOfWork.Db.CPadron_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas
                                  join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on titsh.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId                                  
                                  where titsh.id_personajuridica == IdPersonaJuridica
                                  select new TitularesSHEntity
                                  {
                                      RowId = Guid.NewGuid(),
                                      IdPersonaJuridica = titsh.id_personajuridica,
                                      Apellidos = titsh.Apellido,
                                      Nombres = titsh.Nombres,
                                      TipoDoc = tdoc.Nombre,
                                      NroDoc = titsh.Nro_Documento,
                                      IdTipoDocPersonal = titsh.id_tipodoc_personal,
                                      Email = titsh.Email,
                                      IdFirmantePj = 0//titsh.id_firmante_pj
                                  });
            return lstTitularesSH;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public IEnumerable<TitularesEntity> GetTitularesConsultaPadron(int IdSolicitud)
        {

            var lstTitulares = (from pf in _unitOfWork.Db.CPadron_Titulares_PersonasFisicas
                                where pf.id_cpadron == IdSolicitud
                                select new TitularesEntity
                                {
                                    IdPersona = pf.id_personafisica,
                                    TipoPersona = Constantes.TipoPersonaFisica,
                                    TipoPersonaDesc = Constantes.TipoPersonaFisica_Desc,
                                    ApellidoNomRazon = pf.Apellido + " " + pf.Nombres,
                                    Nombre = pf.Nombres,
                                    Apellido = pf.Apellido,
                                    RazonSocial = "",
                                    CUIT = pf.Cuit,
                                    Domicilio = pf.Calle + " " + pf.Nro_Puerta.ToString() +
                                                (pf.Torre.Length > 0 ? " Torre: " + pf.Torre : "") +
                                                (pf.Piso.Length > 0 ? " Piso: " + pf.Piso : "") +
                                                (pf.Depto.Length > 0 ? "  Depto/UF: " + pf.Depto : ""),
                                    tipo_doc = 0
                                }).Union(
                                    (from pj in _unitOfWork.Db.CPadron_Titulares_PersonasJuridicas
                                     where pj.id_cpadron == IdSolicitud
                                     select new TitularesEntity
                                     {
                                         IdPersona = pj.id_personajuridica,
                                         TipoPersona = Constantes.TipoPersonaJuridica,
                                         TipoPersonaDesc = Constantes.TipoPersonaJuridica_Desc,
                                         ApellidoNomRazon = pj.Razon_Social,
                                         Nombre = "",
                                         Apellido = "",
                                         RazonSocial = pj.Razon_Social,
                                         CUIT = pj.CUIT,
                                         Domicilio = pj.Calle + " " + (pj.NroPuerta.HasValue ? pj.NroPuerta.Value.ToString() : "") +
                                                   (pj.Torre.Length > 0 ? " Torre: " + pj.Torre : "") +
                                                    (pj.Piso.Length > 0 ? " Piso: " + pj.Piso : "") +
                                                    (pj.Depto.Length > 0 ? "  Depto/UF: " + pj.Depto : ""),
                                         tipo_doc = 0
                                     }));
            return lstTitulares;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public IEnumerable<TitularesEntity> GetTitularesConsultaPadronSolicitud(int IdSolicitud)
        {

            var lstTitulares = (from pf in _unitOfWork.Db.CPadron_Titulares_Solicitud_PersonasFisicas
                                where pf.id_cpadron == IdSolicitud
                                select new TitularesEntity
                                {
                                    IdPersona = pf.id_personafisica,
                                    TipoPersona = Constantes.TipoPersonaFisica,
                                    TipoPersonaDesc = Constantes.TipoPersonaFisica_Desc,
                                    ApellidoNomRazon = pf.Apellido + " " + pf.Nombres,
                                    CUIT = pf.Cuit,
                                    Domicilio = pf.Calle + " " + pf.Nro_Puerta.ToString() +
                                                (pf.Torre.Length > 0 ? " Torre: " + pf.Torre : "") +
                                                (pf.Piso.Length > 0 ? " Piso: " + pf.Piso : "") +
                                                (pf.Depto.Length > 0 ? "  Depto/UF: " + pf.Depto : ""),
                                    tipo_doc = 0
                                }).Union(
                                    (from pj in _unitOfWork.Db.CPadron_Titulares_Solicitud_PersonasJuridicas
                                     where pj.id_cpadron == IdSolicitud
                                     select new TitularesEntity
                                     {
                                         IdPersona = pj.id_personajuridica,
                                         TipoPersona = Constantes.TipoPersonaJuridica,
                                         TipoPersonaDesc = Constantes.TipoPersonaJuridica_Desc,
                                         ApellidoNomRazon = pj.Razon_Social,
                                         CUIT = pj.CUIT,
                                         Domicilio = pj.Calle + " " + (pj.NroPuerta.HasValue ? pj.NroPuerta.Value.ToString() : "") +
                                                     (pj.Torre.Length > 0 ? " Torre: " + pj.Torre : "") +
                                                    (pj.Piso.Length > 0 ? " Piso: " + pj.Piso : "") +
                                                    (pj.Depto.Length > 0 ? "  Depto/UF: " + pj.Depto : ""),
                                         tipo_doc = 0
                                     }));
            return lstTitulares;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public IEnumerable<TitularesEntity> GetTitularesTransferencias(int IdSolicitud)
        {

            var lstTitulares = (from pf in _unitOfWork.Db.Transf_Titulares_PersonasFisicas
                                join loc in _unitOfWork.Db.Localidad on pf.id_Localidad equals loc.Id
                                join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on pf.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                                where pf.id_solicitud == IdSolicitud
                                select new TitularesEntity
                                {
                                    IdPersona = pf.id_personafisica,
                                    TipoPersona = Constantes.TipoPersonaFisica,
                                    TipoPersonaDesc = Constantes.TipoPersonaFisica_Desc,
                                    Apellido = pf.Apellido,
                                    Nombre = pf.Nombres,
                                    ApellidoNomRazon = pf.Apellido + " " + pf.Nombres,
                                    RazonSocial= "",
                                    CUIT = pf.Cuit,
                                    Domicilio = pf.Calle + " " + pf.Nro_Puerta.ToString() +
                                                (pf.Piso.Length > 0 ? " Piso: " + pf.Piso : "") +
                                                (pf.Depto.Length > 0 ? "  Depto/UF: " + pf.Depto : ""),
                                    Piso = pf.Piso,
                                    Depto = pf.Depto,
                                    Email = pf.Email,
                                    Codigo_Postal = (pf.Codigo_Postal.Length == 0 ? "-" : pf.Codigo_Postal),
                                    Localidad = loc.Depto,
                                    tipo_doc = pf.id_tipodoc_personal,
                                    desc_tipo_doc = (pf.id_tipodoc_personal == 0 ? "DNI" : tdoc.Nombre),
                                    nro_doc = pf.Nro_Documento.ToString()
                                }).Union(
                                    (from pj in _unitOfWork.Db.Transf_Titulares_PersonasJuridicas
                                     join loc in _unitOfWork.Db.Localidad on pj.id_localidad equals loc.Id
                                     where pj.id_solicitud == IdSolicitud
                                     select new TitularesEntity
                                     {
                                         IdPersona = pj.id_personajuridica,
                                         TipoPersona = Constantes.TipoPersonaJuridica,
                                         TipoPersonaDesc = Constantes.TipoPersonaJuridica_Desc,
                                         Apellido = "",
                                         Nombre = "",
                                         ApellidoNomRazon = pj.Razon_Social,
                                         RazonSocial = pj.Razon_Social,
                                         CUIT = pj.CUIT,
                                         Domicilio = pj.Calle + " " + (pj.NroPuerta.HasValue ? pj.NroPuerta.Value.ToString() : "") +
                                                    (pj.Piso.Length > 0 ? " Piso: " + pj.Piso : "") +
                                                    (pj.Depto.Length > 0 ? "  Depto/UF: " + pj.Depto : ""),
                                         Piso = pj.Piso,
                                         Depto = pj.Depto,
                                         Email = pj.Email,
                                         Codigo_Postal = pj.Codigo_Postal,
                                         Localidad = loc.Depto,
                                         tipo_doc = 0,
                                         desc_tipo_doc = "CUIT",
                                         nro_doc = pj.CUIT
                                     }));
            return lstTitulares;
        }

        public IEnumerable<TitularesEntity> GetTitularesTransferenciasANT(int IdSolicitud)
        {

            var lstTitulares = (from pf in _unitOfWork.Db.Transf_Titulares_Solicitud_PersonasFisicas
                                join loc in _unitOfWork.Db.Localidad on pf.Id_Localidad equals loc.Id
                                join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on pf.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                                where pf.id_solicitud == IdSolicitud
                                select new TitularesEntity
                                {
                                    IdPersona = pf.id_personafisica,
                                    TipoPersona = Constantes.TipoPersonaFisica,
                                    TipoPersonaDesc = Constantes.TipoPersonaFisica_Desc,
                                    Apellido = pf.Apellido,
                                    Nombre = pf.Nombres,
                                    ApellidoNomRazon = pf.Apellido + " " + pf.Nombres,
                                    RazonSocial = "",
                                    CUIT = pf.Cuit,
                                    Domicilio = pf.Calle + " " + pf.Nro_Puerta.ToString() +
                                                (pf.Piso.Length > 0 ? " Piso: " + pf.Piso : "") +
                                                (pf.Depto.Length > 0 ? "  Depto/UF: " + pf.Depto : ""),
                                    Piso = pf.Piso,
                                    Depto = pf.Depto,
                                    Email = pf.Email,
                                    Codigo_Postal = (pf.Codigo_Postal.Length == 0 ? "-" : pf.Codigo_Postal),
                                    Localidad = loc.Depto,
                                    tipo_doc = pf.id_tipodoc_personal,
                                    desc_tipo_doc = (pf.id_tipodoc_personal == 0 ? "DNI" : tdoc.Nombre),
                                    nro_doc = pf.Nro_Documento.ToString()
                                }).Union(
                                    (from pj in _unitOfWork.Db.Transf_Titulares_Solicitud_PersonasJuridicas
                                     join loc in _unitOfWork.Db.Localidad on pj.id_localidad equals loc.Id
                                     where pj.id_solicitud == IdSolicitud
                                     select new TitularesEntity
                                     {
                                         IdPersona = pj.id_personajuridica,
                                         TipoPersona = Constantes.TipoPersonaJuridica,
                                         TipoPersonaDesc = Constantes.TipoPersonaJuridica_Desc,
                                         Apellido = "",
                                         Nombre = "",
                                         ApellidoNomRazon = pj.Razon_Social,
                                         RazonSocial = pj.Razon_Social,
                                         CUIT = pj.CUIT,
                                         Domicilio = pj.Calle + " " + (pj.NroPuerta.HasValue ? pj.NroPuerta.Value.ToString() : "") +
                                                    (pj.Piso.Length > 0 ? " Piso: " + pj.Piso : "") +
                                                    (pj.Depto.Length > 0 ? "  Depto/UF: " + pj.Depto : ""),
                                         Piso = pj.Piso,
                                         Depto = pj.Depto,
                                         Email = pj.Email,
                                         Codigo_Postal = pj.Codigo_Postal,
                                         Localidad = loc.Depto,
                                         tipo_doc = 0,
                                         desc_tipo_doc = "CUIT",
                                         nro_doc = pj.CUIT
                                     }));
            return lstTitulares;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public IEnumerable<TitularesEntity> GetTitularesSolicitud(int IdSolicitud)
        {

            var lstTitulares = (from pf in _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasFisicas
                                join loc in _unitOfWork.Db.Localidad on pf.Id_Localidad equals loc.Id
                                join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on pf.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                                where pf.id_solicitud == IdSolicitud
                                select new TitularesEntity
                                {
                                    IdPersona = pf.id_personafisica,
                                    TipoPersona = Constantes.TipoPersonaFisica,
                                    TipoPersonaDesc = Constantes.TipoPersonaFisica_Desc,
                                    ApellidoNomRazon = pf.Apellido + " " + pf.Nombres,
                                    Nombre = pf.Nombres,
                                    Apellido = pf.Apellido,
                                    RazonSocial = "",
                                    CUIT = pf.Cuit,
                                    Domicilio = pf.Calle + " " + pf.Nro_Puerta.ToString() +
                                                (pf.Torre.Length > 0 ? " Torre: " + pf.Torre : "") +
                                                (pf.Piso.Length > 0 ? " Piso: " + pf.Piso : "") +
                                                (pf.Depto.Length > 0 ? "  Depto/UF: " + pf.Depto : ""),
                                    Piso = pf.Piso,
                                    Depto = pf.Depto,
                                    Email = pf.Email,
                                    Codigo_Postal = (pf.Codigo_Postal.Length == 0 ? "-" : pf.Codigo_Postal),
                                    Localidad = loc.Depto,
                                    tipo_doc = pf.id_tipodoc_personal,
                                    desc_tipo_doc = (pf.id_tipodoc_personal == 0 ? "DNI" : tdoc.Nombre),
                                    nro_doc = pf.Nro_Documento.ToString()
                                }).Union(
                                    (from pj in _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasJuridicas
                                     join loc in _unitOfWork.Db.Localidad on pj.id_localidad equals loc.Id
                                     where pj.id_solicitud == IdSolicitud
                                     select new TitularesEntity
                                     {
                                         IdPersona = pj.id_personajuridica,
                                         TipoPersona = Constantes.TipoPersonaJuridica,
                                         TipoPersonaDesc = Constantes.TipoPersonaJuridica_Desc,
                                         ApellidoNomRazon = pj.Razon_Social,
                                         Nombre = "",
                                         Apellido = "",
                                         RazonSocial = pj.Razon_Social,
                                         CUIT = pj.CUIT,
                                         Domicilio = pj.Calle + " " + (pj.NroPuerta.HasValue ? pj.NroPuerta.Value.ToString() : "") +
                                                   (pj.Torre.Length > 0 ? " Torre: " + pj.Torre : "") +
                                                    (pj.Piso.Length > 0 ? " Piso: " + pj.Piso : "") +
                                                    (pj.Depto.Length > 0 ? "  Depto/UF: " + pj.Depto : ""),
                                         Piso = pj.Piso,
                                         Depto = pj.Depto,
                                         Email = pj.Email,
                                         Codigo_Postal = pj.Codigo_Postal,
                                         Localidad = loc.Depto,
                                         tipo_doc = 0,
                                         desc_tipo_doc = "CUIT",
                                         nro_doc = pj.CUIT

                                     }));
            return lstTitulares;
        }

        public IEnumerable<TitularesEntity> GetTitularesTransmision(int IdSolicitud)
        {

            var lstTitulares = (from pf in _unitOfWork.Db.Transf_Titulares_PersonasFisicas
                                join loc in _unitOfWork.Db.Localidad on pf.id_Localidad equals loc.Id
                                join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on pf.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                                where pf.id_solicitud == IdSolicitud
                                select new TitularesEntity
                                {
                                    IdPersona = pf.id_personafisica,
                                    TipoPersona = Constantes.TipoPersonaFisica,
                                    TipoPersonaDesc = Constantes.TipoPersonaFisica_Desc,
                                    ApellidoNomRazon = pf.Apellido + " " + pf.Nombres,
                                    Nombre = pf.Nombres,
                                    Apellido = pf.Apellido,
                                    RazonSocial = "",
                                    CUIT = pf.Cuit,
                                    Domicilio = pf.Calle + " " + pf.Nro_Puerta.ToString() +
                                                //(pf.Length > 0 ? " Torre: " + pf.Torre : "") +
                                                (pf.Piso.Length > 0 ? " Piso: " + pf.Piso : "") +
                                                (pf.Depto.Length > 0 ? "  Depto/UF: " + pf.Depto : ""),
                                    Piso = pf.Piso,
                                    Depto = pf.Depto,
                                    Email = pf.Email,
                                    Codigo_Postal = (pf.Codigo_Postal.Length == 0 ? "-" : pf.Codigo_Postal),
                                    Localidad = loc.Depto,
                                    tipo_doc = pf.id_tipodoc_personal,
                                    desc_tipo_doc = (pf.id_tipodoc_personal == 0 ? "DNI" : tdoc.Nombre),
                                    nro_doc = pf.Nro_Documento.ToString()
                                }).Union(
                                    (from pj in _unitOfWork.Db.Transf_Titulares_PersonasJuridicas
                                     join loc in _unitOfWork.Db.Localidad on pj.id_localidad equals loc.Id
                                     where pj.id_solicitud == IdSolicitud
                                     select new TitularesEntity
                                     {
                                         IdPersona = pj.id_personajuridica,
                                         TipoPersona = Constantes.TipoPersonaJuridica,
                                         TipoPersonaDesc = Constantes.TipoPersonaJuridica_Desc,
                                         ApellidoNomRazon = pj.Razon_Social,
                                         Nombre = "",
                                         Apellido = "",
                                         RazonSocial = pj.Razon_Social,
                                         CUIT = pj.CUIT,
                                         Domicilio = pj.Calle + " " + (pj.NroPuerta.HasValue ? pj.NroPuerta.Value.ToString() : "") +
                                                   //(pj.Torre.Length > 0 ? " Torre: " + pj.Torre : "") +
                                                    (pj.Piso.Length > 0 ? " Piso: " + pj.Piso : "") +
                                                    (pj.Depto.Length > 0 ? "  Depto/UF: " + pj.Depto : ""),
                                         Piso = pj.Piso,
                                         Depto = pj.Depto,
                                         Email = pj.Email,
                                         Codigo_Postal = pj.Codigo_Postal,
                                         Localidad = loc.Depto,
                                         tipo_doc = 0,
                                         desc_tipo_doc = "CUIT",
                                         nro_doc = pj.CUIT

                                     }));
            return lstTitulares;
        }
    }
}
