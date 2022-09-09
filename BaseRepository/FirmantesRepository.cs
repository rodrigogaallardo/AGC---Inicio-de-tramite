using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;
using StaticClass;

namespace BaseRepository
{
    public class FirmantesRepository : BaseRepository<FirmantesEntity>
    {
        private readonly IUnitOfWork _unitOfWork;

        public FirmantesRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }


        public IEnumerable<FirmantesEntity> GetFirmantesPJEncomienda(int id_encomienda)
        {

            List<int> lstTipoSH = new List<int>();
            lstTipoSH.Add((int)Constantes.TipoSociedad.Sociedad_no_constituidas_regularmente);
            lstTipoSH.Add((int)Constantes.TipoSociedad.Sociedad_Hecho);

            var firmantesPJ = (from pj in _unitOfWork.Db.Encomienda_Firmantes_PersonasJuridicas
                               join tcl in _unitOfWork.Db.TiposDeCaracterLegal on pj.id_tipocaracter equals tcl.id_tipocaracter
                               join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on pj.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                               where
                                 pj.id_encomienda == id_encomienda &&
                                 !lstTipoSH.Contains(pj.Encomienda_Titulares_PersonasJuridicas.Id_TipoSociedad)
                               select new FirmantesEntity
                               {
                                   TipoPersona = "PJ",
                                   IdFirmante = pj.id_firmante_pj,
                                   Titular = pj.Encomienda_Titulares_PersonasJuridicas.Razon_Social,
                                   ApellidoNombres = pj.Apellido + " " + pj.Nombres,
                                   DescTipoDocPersonal = tdoc.Nombre,
                                   NroDocumento = pj.Nro_Documento,
                                   NomTipoCaracter = tcl.nom_tipocaracter,
                                   CargoFirmante = pj.cargo_firmante_pj
                               });

            var firmantesPJPF = (from pj in _unitOfWork.Db.Encomienda_Firmantes_PersonasJuridicas
                                 join tcl in _unitOfWork.Db.TiposDeCaracterLegal on pj.id_tipocaracter equals tcl.id_tipocaracter
                                 join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on pj.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                                 join titsh in _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas on pj.id_firmante_pj equals titsh.id_firmante_pj
                                 where
                                   pj.id_encomienda == id_encomienda &&
                                   lstTipoSH.Contains(pj.Encomienda_Titulares_PersonasJuridicas.Id_TipoSociedad)
                                 select new FirmantesEntity
                                 {
                                     TipoPersona = "PJ",
                                     IdFirmante = pj.id_firmante_pj,
                                     Titular = titsh.Apellido + " " + titsh.Nombres,
                                     ApellidoNombres = pj.Apellido + " " + pj.Nombres,
                                     DescTipoDocPersonal = tdoc.Nombre,
                                     NroDocumento = pj.Nro_Documento,
                                     NomTipoCaracter = tcl.nom_tipocaracter,
                                     CargoFirmante = pj.cargo_firmante_pj
                                 });
            var lstFirmantes = firmantesPJ.Union(firmantesPJPF);

            return lstFirmantes;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_encomienda"></param>
        /// <returns></returns>
        public IEnumerable<FirmantesEntity> GetFirmantesEncomienda(int id_encomienda)
        {

            List<int> lstTipoSH = new List<int>();
            lstTipoSH.Add((int)Constantes.TipoSociedad.Sociedad_no_constituidas_regularmente);
            lstTipoSH.Add((int)Constantes.TipoSociedad.Sociedad_Hecho);

            var firmantesPJ = (from pj in _unitOfWork.Db.Encomienda_Firmantes_PersonasJuridicas
                               join tcl in _unitOfWork.Db.TiposDeCaracterLegal on pj.id_tipocaracter equals tcl.id_tipocaracter
                               join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on pj.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                               where
                                 pj.id_encomienda == id_encomienda &&
                                 !lstTipoSH.Contains(pj.Encomienda_Titulares_PersonasJuridicas.Id_TipoSociedad)
                               select new FirmantesEntity
                               {
                                   TipoPersona = "PJ",
                                   IdFirmante = pj.id_firmante_pj,
                                   Titular = pj.Encomienda_Titulares_PersonasJuridicas.Razon_Social,
                                   ApellidoNombres = pj.Apellido + " " + pj.Nombres,
                                   DescTipoDocPersonal = tdoc.Nombre,
                                   NroDocumento = pj.Nro_Documento,
                                   NomTipoCaracter = tcl.nom_tipocaracter,
                                   CargoFirmante = pj.cargo_firmante_pj
                               });

            var firmantesPJPF = (from pj in _unitOfWork.Db.Encomienda_Firmantes_PersonasJuridicas
                                 join tcl in _unitOfWork.Db.TiposDeCaracterLegal on pj.id_tipocaracter equals tcl.id_tipocaracter
                                 join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on pj.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                                 join titsh in _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas on pj.id_firmante_pj equals titsh.id_firmante_pj
                                 where
                                   pj.id_encomienda == id_encomienda &&
                                   lstTipoSH.Contains(pj.Encomienda_Titulares_PersonasJuridicas.Id_TipoSociedad)
                                 select new FirmantesEntity
                                 {
                                     TipoPersona = "PJ",
                                     IdFirmante = pj.id_firmante_pj,
                                     Titular = titsh.Apellido + " " + titsh.Nombres,
                                     ApellidoNombres = pj.Apellido + " " + pj.Nombres,
                                     DescTipoDocPersonal = tdoc.Nombre,
                                     NroDocumento = pj.Nro_Documento,
                                     NomTipoCaracter = tcl.nom_tipocaracter,
                                     CargoFirmante = pj.cargo_firmante_pj
                                 });

            var firmantesPF = (from pf in _unitOfWork.Db.Encomienda_Firmantes_PersonasFisicas
                               join tcl in _unitOfWork.Db.TiposDeCaracterLegal on pf.id_tipocaracter equals tcl.id_tipocaracter
                               join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on pf.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                               where
                                 pf.id_encomienda == id_encomienda
                               select new FirmantesEntity
                               {
                                   TipoPersona = "PF",
                                   IdFirmante = pf.id_firmante_pf,
                                   Titular = pf.Apellido + ", " + pf.Nombres,
                                   ApellidoNombres = pf.Apellido + " " + pf.Nombres,
                                   DescTipoDocPersonal = tdoc.Nombre,
                                   NroDocumento = pf.Nro_Documento,
                                   NomTipoCaracter = tcl.nom_tipocaracter,
                                   CargoFirmante = ""
                               });

            var lstFirmantes = firmantesPJ.Union(firmantesPJPF).Union(firmantesPF);

            return lstFirmantes;
        }

        public IEnumerable<FirmantesEntity> GetFirmantesSolicitud(int id_solicitud)
        {
            List<int> lstTipoSH = new List<int>();
            lstTipoSH.Add((int)Constantes.TipoSociedad.Sociedad_no_constituidas_regularmente);
            lstTipoSH.Add((int)Constantes.TipoSociedad.Sociedad_Hecho);

            var firmantesPJ = (from pj in _unitOfWork.Db.SSIT_Solicitudes_Firmantes_PersonasJuridicas
                               join tcl in _unitOfWork.Db.TiposDeCaracterLegal on pj.id_tipocaracter equals tcl.id_tipocaracter
                               join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on pj.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                               where
                                 pj.id_solicitud == id_solicitud &&
                                 !lstTipoSH.Contains(pj.SSIT_Solicitudes_Titulares_PersonasJuridicas.Id_TipoSociedad)
                               select new FirmantesEntity
                               {
                                   TipoPersona = "PJ",
                                   IdFirmante = pj.id_firmante_pj,
                                   Titular = pj.SSIT_Solicitudes_Titulares_PersonasJuridicas.Razon_Social,
                                   ApellidoNombres = pj.Apellido + " " + pj.Nombres,
                                   DescTipoDocPersonal = tdoc.Nombre,
                                   NroDocumento = pj.Nro_Documento,
                                   NomTipoCaracter = tcl.nom_tipocaracter,
                                   CargoFirmante = pj.cargo_firmante_pj,
                                   Email = pj.Email
                               });

            var firmantesPJPF = (from pj in _unitOfWork.Db.SSIT_Solicitudes_Firmantes_PersonasJuridicas
                                 join tcl in _unitOfWork.Db.TiposDeCaracterLegal on pj.id_tipocaracter equals tcl.id_tipocaracter
                                 join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on pj.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                                 join titsh in _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas on pj.id_firmante_pj equals titsh.id_firmante_pj
                                 where
                                   pj.id_solicitud == id_solicitud &&
                                   lstTipoSH.Contains(pj.SSIT_Solicitudes_Titulares_PersonasJuridicas.Id_TipoSociedad)
                                 select new FirmantesEntity
                                 {
                                     TipoPersona = "PJ",
                                     IdFirmante = pj.id_firmante_pj,
                                     Titular = titsh.Apellido + " " + titsh.Nombres,
                                     ApellidoNombres = pj.Apellido + " " + pj.Nombres,
                                     DescTipoDocPersonal = tdoc.Nombre,
                                     NroDocumento = pj.Nro_Documento,
                                     NomTipoCaracter = tcl.nom_tipocaracter,
                                     CargoFirmante = pj.cargo_firmante_pj,
                                     Email = pj.Email
                                 });

            var firmantesPF = (from titPF in _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasFisicas
                               join firPF in _unitOfWork.Db.SSIT_Solicitudes_Firmantes_PersonasFisicas on titPF.id_personafisica equals firPF.id_personafisica
                               join tcl in _unitOfWork.Db.TiposDeCaracterLegal on firPF.id_tipocaracter equals tcl.id_tipocaracter
                               join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on firPF.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                               where
                                 firPF.id_solicitud == id_solicitud
                               select new FirmantesEntity
                               {
                                   TipoPersona = "PF",
                                   IdFirmante = firPF.id_firmante_pf,
                                   Titular = titPF.Apellido + ", " + titPF.Nombres,
                                   ApellidoNombres = firPF.Apellido + " " + firPF.Nombres,
                                   DescTipoDocPersonal = tdoc.Nombre,
                                   NroDocumento = firPF.Nro_Documento,
                                   NomTipoCaracter = tcl.nom_tipocaracter,
                                   CargoFirmante = "",
                                   Email = titPF.Email
                               });

            var lstFirmantes = firmantesPJ.Union(firmantesPJPF).Union(firmantesPF);

            return lstFirmantes;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public IEnumerable<FirmantesEntity> GetFirmantesTransferencias(int IdSolicitud)
        {
            var lstFirmantes = (from pj in _unitOfWork.Db.Transf_Firmantes_PersonasJuridicas
                                join titpj in _unitOfWork.Db.Transf_Titulares_PersonasJuridicas on pj.id_personajuridica equals titpj.id_personajuridica
                                join tcl in _unitOfWork.Db.TiposDeCaracterLegal on pj.id_tipocaracter equals tcl.id_tipocaracter
                                join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on pj.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                                where pj.id_solicitud == IdSolicitud
                                select new FirmantesEntity
                                {
                                    TipoPersona = "PJ",
                                    IdFirmante = pj.id_firmante_pj,
                                    Titular = titpj.Razon_Social,
                                    ApellidoNombres = pj.Apellido + " " + pj.Nombres,
                                    DescTipoDocPersonal = tdoc.Nombre,
                                    NroDocumento = pj.Nro_Documento,
                                    NomTipoCaracter = tcl.nom_tipocaracter,
                                    CargoFirmante = pj.cargo_firmante_pj
                                }).Union(
                                    from pf in _unitOfWork.Db.Transf_Firmantes_PersonasFisicas
                                    join titpf in _unitOfWork.Db.Transf_Titulares_PersonasFisicas on pf.id_personafisica equals titpf.id_personafisica
                                    join tcl in _unitOfWork.Db.TiposDeCaracterLegal on pf.id_tipocaracter equals tcl.id_tipocaracter
                                    join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on pf.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                                    where pf.id_solicitud == IdSolicitud
                                    select new FirmantesEntity
                                    {
                                        TipoPersona = "PF",
                                        IdFirmante = pf.id_firmante_pf,
                                        Titular = titpf.Apellido + ", " + titpf.Nombres,
                                        ApellidoNombres = pf.Apellido + " " + pf.Nombres,
                                        DescTipoDocPersonal = tdoc.Nombre,
                                        NroDocumento = pf.Nro_Documento,
                                        NomTipoCaracter = tcl.nom_tipocaracter,
                                        CargoFirmante = ""
                                    });
            return lstFirmantes;
        }


        public IEnumerable<FirmantesEntity> GetFirmantesTransferenciasANT(int IdSolicitud)
        {
            var lstFirmantes = (from pj in _unitOfWork.Db.Transf_Firmantes_Solicitud_PersonasJuridicas
                                join titpj in _unitOfWork.Db.Transf_Titulares_Solicitud_PersonasJuridicas on pj.id_personajuridica equals titpj.id_personajuridica
                                join tcl in _unitOfWork.Db.TiposDeCaracterLegal on pj.id_tipocaracter equals tcl.id_tipocaracter
                                join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on pj.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                                where pj.id_solicitud == IdSolicitud
                                select new FirmantesEntity
                                {
                                    TipoPersona = "PJ",
                                    IdFirmante = pj.id_firmante_pj,
                                    Titular = titpj.Razon_Social,
                                    ApellidoNombres = pj.Apellido + " " + pj.Nombres,
                                    DescTipoDocPersonal = tdoc.Nombre,
                                    NroDocumento = pj.Nro_Documento,
                                    NomTipoCaracter = tcl.nom_tipocaracter,
                                    CargoFirmante = pj.cargo_firmante_pj
                                }).Union(
                                    from pf in _unitOfWork.Db.Transf_Firmantes_Solicitud_PersonasFisicas
                                    join titpf in _unitOfWork.Db.Transf_Titulares_Solicitud_PersonasFisicas on pf.id_personafisica equals titpf.id_personafisica
                                    join tcl in _unitOfWork.Db.TiposDeCaracterLegal on pf.id_tipocaracter equals tcl.id_tipocaracter
                                    join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on pf.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                                    where pf.id_solicitud == IdSolicitud
                                    select new FirmantesEntity
                                    {
                                        TipoPersona = "PF",
                                        IdFirmante = pf.id_firmante_pf,
                                        Titular = titpf.Apellido + ", " + titpf.Nombres,
                                        ApellidoNombres = pf.Apellido + " " + pf.Nombres,
                                        DescTipoDocPersonal = tdoc.Nombre,
                                        NroDocumento = pf.Nro_Documento,
                                        NomTipoCaracter = tcl.nom_tipocaracter,
                                        CargoFirmante = ""
                                    });
            return lstFirmantes;
        }
        public IEnumerable<FirmantesPJEntity> GetFirmantesPJPFSolicitud(int id_firmante_pj)
        {
            var lstFirmantesSH = (from firpj in _unitOfWork.Db.SSIT_Solicitudes_Firmantes_PersonasJuridicas
                                  join titsh in _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasJuridicas_PersonasFisicas on firpj.id_firmante_pj equals titsh.id_firmante_pj
                                  join tcl in _unitOfWork.Db.TiposDeCaracterLegal on firpj.id_tipocaracter equals tcl.id_tipocaracter
                                  join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on firpj.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                                  where firpj.id_firmante_pj == id_firmante_pj
                                  select new FirmantesPJEntity
                                  {
                                      IdPersonaJuridica = firpj.id_personajuridica,
                                      Apellidos = firpj.Apellido,
                                      Nombres = firpj.Nombres,
                                      TipoDoc = tdoc.Nombre,
                                      NroDoc = firpj.Nro_Documento,
                                      Cuit = firpj.Cuit,
                                      NomTipoCaracter = tcl.nom_tipocaracter,
                                      IdTipoDocPersonal = firpj.id_tipodoc_personal,
                                      IdTipoCaracter = firpj.id_tipocaracter,
                                      CargoFirmantePj = firpj.cargo_firmante_pj,
                                      Email = firpj.Email,
                                      FirmanteMismaPersona = titsh.firmante_misma_persona
                                  });
            return lstFirmantesSH;
        }

        public IEnumerable<FirmantesPJEntity> GetTransfFirmantesPJPFSolicitudByIDSol(int IdSolicitud)
        {
            var lstFirmantesSH = (from firpj in _unitOfWork.Db.Transf_Firmantes_Solicitud_PersonasJuridicas
                                  join titsh in _unitOfWork.Db.Transf_Titulares_Solicitud_PersonasJuridicas_PersonasFisicas on firpj.id_firmante_pj equals titsh.id_personajuridica
                                  join tcl in _unitOfWork.Db.TiposDeCaracterLegal on firpj.id_tipocaracter equals tcl.id_tipocaracter
                                  join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on firpj.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                                  where firpj.id_solicitud == IdSolicitud
                                  select new FirmantesPJEntity
                                  {
                                      IdPersonaJuridica = firpj.id_firmante_pj,
                                      Apellidos = firpj.Apellido,
                                      Nombres = firpj.Nombres,
                                      TipoDoc = tdoc.Nombre,
                                      NroDoc = firpj.Nro_Documento,
                                      Cuit = "",
                                      NomTipoCaracter = tcl.nom_tipocaracter,
                                      IdTipoDocPersonal = firpj.id_tipodoc_personal,
                                      IdTipoCaracter = firpj.id_tipocaracter,
                                      CargoFirmantePj = firpj.cargo_firmante_pj,
                                      Email = firpj.Email,
                                      FirmanteMismaPersona = titsh.firmante_misma_persona
                                  });
            return lstFirmantesSH;
        }

        public IEnumerable<FirmantesPJEntity> GetTransfFirmantesPJPFByIDSol(int IdSolicitud)
        {
            var lstFirmantesSH = (from firpj in _unitOfWork.Db.Transf_Firmantes_PersonasJuridicas
                                  join titsh in _unitOfWork.Db.Transf_Titulares_PersonasJuridicas_PersonasFisicas on firpj.id_firmante_pj equals titsh.id_firmante_pj
                                  join tcl in _unitOfWork.Db.TiposDeCaracterLegal on firpj.id_tipocaracter equals tcl.id_tipocaracter
                                  join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on firpj.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                                  where firpj.id_solicitud == IdSolicitud
                                  select new FirmantesPJEntity
                                  {
                                      IdPersonaJuridica = firpj.id_firmante_pj,
                                      Apellidos = firpj.Apellido,
                                      Nombres = firpj.Nombres,
                                      TipoDoc = tdoc.Nombre,
                                      NroDoc = firpj.Nro_Documento,
                                      Cuit = "",
                                      NomTipoCaracter = tcl.nom_tipocaracter,
                                      IdTipoDocPersonal = firpj.id_tipodoc_personal,
                                      IdTipoCaracter = firpj.id_tipocaracter,
                                      CargoFirmantePj = firpj.cargo_firmante_pj,
                                      Email = firpj.Email,
                                      FirmanteMismaPersona = titsh.firmante_misma_persona
                                  });
            return lstFirmantesSH;
        }


        public IEnumerable<FirmantesPJEntity> GetFirmantesPJPF(int id_firmante_pj)
        {
            var lstFirmantesSH = (from firpj in _unitOfWork.Db.Encomienda_Firmantes_PersonasJuridicas
                                  join titsh in _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas on firpj.id_firmante_pj equals titsh.id_firmante_pj
                                  join tcl in _unitOfWork.Db.TiposDeCaracterLegal on firpj.id_tipocaracter equals tcl.id_tipocaracter
                                  join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on firpj.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                                  where firpj.id_firmante_pj == id_firmante_pj
                                  select new FirmantesPJEntity
                                  {
                                      IdPersonaJuridica = firpj.id_personajuridica,
                                      Apellidos = firpj.Apellido,
                                      Nombres = firpj.Nombres,
                                      TipoDoc = tdoc.Nombre,
                                      NroDoc = firpj.Nro_Documento,
                                      NomTipoCaracter = tcl.nom_tipocaracter,
                                      IdTipoDocPersonal = firpj.id_tipodoc_personal,
                                      IdTipoCaracter = firpj.id_tipocaracter,
                                      CargoFirmantePj = firpj.cargo_firmante_pj,
                                      Email = firpj.Email,
                                      FirmanteMismaPersona = titsh.firmante_misma_persona
                                  });
            return lstFirmantesSH;
        }
        public IEnumerable<FirmantesPJEntity> GetFirmantesPJSolicitud(int id_firmante_pj)
        {
            var lstFirmantesPJ = (from fpj in _unitOfWork.Db.SSIT_Solicitudes_Firmantes_PersonasJuridicas
                                  join tpj in _unitOfWork.Db.SSIT_Solicitudes_Titulares_PersonasJuridicas on fpj.id_personajuridica equals tpj.id_personajuridica
                                  join tc in _unitOfWork.Db.TiposDeCaracterLegal on fpj.id_tipocaracter equals tc.id_tipocaracter
                                  join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on fpj.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                                  where fpj.id_personajuridica == id_firmante_pj
                                  select new FirmantesPJEntity
                                  {
                                      IdPersonaJuridica = fpj.id_personajuridica,
                                      Apellidos = fpj.Apellido,
                                      Nombres = fpj.Nombres,
                                      TipoDoc = tdoc.Nombre,
                                      NroDoc = fpj.Nro_Documento,
                                      Cuit = fpj.Cuit,
                                      NomTipoCaracter = tc.nom_tipocaracter,
                                      IdTipoDocPersonal = fpj.id_tipodoc_personal,
                                      Email = fpj.Email,
                                      IdTipoCaracter = fpj.id_tipocaracter,
                                      CargoFirmantePj = fpj.cargo_firmante_pj
                                  });

            return lstFirmantesPJ;
        }
        public IEnumerable<FirmantesPJEntity> GetFirmantesTransferenciasPJ(int id_firmante_pj)
        {
            var lstFirmantesPJ = (from fpj in _unitOfWork.Db.Transf_Firmantes_PersonasJuridicas
                                  join tpj in _unitOfWork.Db.Transf_Titulares_PersonasJuridicas on fpj.id_personajuridica equals tpj.id_personajuridica
                                  join tc in _unitOfWork.Db.TiposDeCaracterLegal on fpj.id_tipocaracter equals tc.id_tipocaracter
                                  join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on fpj.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                                  where fpj.id_personajuridica == id_firmante_pj
                                  select new FirmantesPJEntity
                                  {
                                      IdPersonaJuridica = fpj.id_personajuridica,
                                      Apellidos = fpj.Apellido,
                                      Nombres = fpj.Nombres,
                                      TipoDoc = tdoc.Nombre,
                                      NroDoc = fpj.Nro_Documento,
                                      NomTipoCaracter = tc.nom_tipocaracter,
                                      IdTipoDocPersonal = fpj.id_tipodoc_personal,
                                      Email = fpj.Email,
                                      IdTipoCaracter = fpj.id_tipocaracter,
                                      CargoFirmantePj = fpj.cargo_firmante_pj
                                  });

            return lstFirmantesPJ;
        }

    public IEnumerable<FirmantesPJEntity> GetFirmantesTransferenciasPJANT(int id_firmante_pj)
        {
            var lstFirmantesPJ = (from fpj in _unitOfWork.Db.Transf_Firmantes_Solicitud_PersonasJuridicas
                                  join tpj in _unitOfWork.Db.Transf_Titulares_Solicitud_PersonasJuridicas on fpj.id_personajuridica equals tpj.id_personajuridica
                                  join tc in _unitOfWork.Db.TiposDeCaracterLegal on fpj.id_tipocaracter equals tc.id_tipocaracter
                                  join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on fpj.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                                  where fpj.id_personajuridica == id_firmante_pj
                                  select new FirmantesPJEntity
                                  {
                                      IdPersonaJuridica = fpj.id_personajuridica,
                                      Apellidos = fpj.Apellido,
                                      Nombres = fpj.Nombres,
                                      TipoDoc = tdoc.Nombre,
                                      NroDoc = fpj.Nro_Documento,
                                      NomTipoCaracter = tc.nom_tipocaracter,
                                      IdTipoDocPersonal = fpj.id_tipodoc_personal,
                                      Email = fpj.Email,
                                      IdTipoCaracter = fpj.id_tipocaracter,
                                      CargoFirmantePj = fpj.cargo_firmante_pj,
                                      FirmanteMismaPersona = true
                                  });

            return lstFirmantesPJ;
        }
        public IEnumerable<FirmantesPJEntity> GetFirmantesPJ(int id_firmante_pj)
        {
            var lstFirmantesPJ = (from fpj in _unitOfWork.Db.Encomienda_Firmantes_PersonasJuridicas
                                  join tpj in _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas on fpj.id_personajuridica equals tpj.id_personajuridica
                                  join tc in _unitOfWork.Db.TiposDeCaracterLegal on fpj.id_tipocaracter equals tc.id_tipocaracter
                                  join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on fpj.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                                  where fpj.id_personajuridica == id_firmante_pj
                                  select new FirmantesPJEntity
                                  {
                                      IdPersonaJuridica = fpj.id_personajuridica,
                                      Apellidos = fpj.Apellido,
                                      Nombres = fpj.Nombres,
                                      TipoDoc = tdoc.Nombre,
                                      NroDoc = fpj.Nro_Documento,
                                      NomTipoCaracter = tc.nom_tipocaracter,
                                      IdTipoDocPersonal = fpj.id_tipodoc_personal,
                                      Email = fpj.Email,
                                      IdTipoCaracter = fpj.id_tipocaracter,
                                      CargoFirmantePj = fpj.cargo_firmante_pj
                                  });

            return lstFirmantesPJ;
        }
        //public IEnumerable<FirmantesEntity> GetFirmantesConsultaPadron(int IdSolicitud)
        //{
        //    var lstFirmantes = (from pj in _unitOfWork.Db.CPadron_Titulares_Solicitud_PersonasJuridicas
        //                        join titpj in _unitOfWork.Db.CPadron_Titulares_PersonasJuridicas on pj.id_personajuridica equals titpj.id_personajuridica
        //                        join tcl in _unitOfWork.Db.TiposDeCaracterLegal on pj.id_tipocaracter equals tcl.id_tipocaracter
        //                        join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on pj.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
        //                        where pj.id_encomienda == IdSolicitud
        //                        select new FirmantesEntity
        //                        {
        //                            TipoPersona = "PJ",
        //                            IdFirmante = pj.id_firmante_pj,
        //                            Titular = titpj.Razon_Social,
        //                            ApellidoNombres = pj.Apellido + " " + pj.Nombres,
        //                            DescTipoDocPersonal = tdoc.Nombre,
        //                            NroDocumento = pj.Nro_Documento,
        //                            NomTipoCaracter = tcl.nom_tipocaracter,
        //                            CargoFirmante = pj.cargo_firmante_pj
        //                        }).Union(
        //                            from pf in _unitOfWork.Db.Encomienda_Firmantes_PersonasFisicas
        //                            join titpf in _unitOfWork.Db.CPadron_Titulares_PersonasFisicas on pf.id_personafisica equals titpf.id_personafisica
        //                            join tcl in _unitOfWork.Db.TiposDeCaracterLegal on pf.id_tipocaracter equals tcl.id_tipocaracter
        //                            join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on pf.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
        //                            where pf.id_encomienda == IdSolicitud
        //                            select new FirmantesEntity
        //                            {
        //                                TipoPersona = "PF",
        //                                IdFirmante = pf.id_firmante_pf,
        //                                Titular = titpf.Apellido + ", " + titpf.Nombres,
        //                                ApellidoNombres = pf.Apellido + " " + pf.Nombres,
        //                                DescTipoDocPersonal = tdoc.Nombre,
        //                                NroDocumento = pf.Nro_Documento,
        //                                NomTipoCaracter = tcl.nom_tipocaracter,
        //                                CargoFirmante = ""
        //                            });
        //    return lstFirmantes;
        //}

        //public IEnumerable<FirmantesPJEntity> GetFirmantesPJPF(int id_firmante_pj)
        //{
        //    var lstFirmantesSH = (from firpj in _unitOfWork.Db.Encomienda_Firmantes_PersonasJuridicas
        //                          join titsh in _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas on firpj.id_firmante_pj equals titsh.id_firmante_pj
        //                          join tcl in _unitOfWork.Db.TiposDeCaracterLegal on firpj.id_tipocaracter equals tcl.id_tipocaracter
        //                          join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on firpj.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
        //                          where firpj.id_firmante_pj == id_firmante_pj
        //                          select new FirmantesPJEntity
        //                          {
        //                              IdPersonaJuridica = firpj.id_personajuridica,
        //                              Apellidos = firpj.Apellido,
        //                              Nombres = firpj.Nombres,
        //                              TipoDoc = tdoc.Nombre,
        //                              NroDoc = firpj.Nro_Documento,
        //                              NomTipoCaracter = tcl.nom_tipocaracter,
        //                              IdTipoDocPersonal = firpj.id_tipodoc_personal,
        //                              IdTipoCaracter = firpj.id_tipocaracter,
        //                              CargoFirmantePj = firpj.cargo_firmante_pj,
        //                              Email = firpj.Email,
        //                              FirmanteMismaPersona = titsh.firmante_misma_persona
        //                          });
        //    return lstFirmantesSH;
        //}


        //public IEnumerable<FirmantesPJEntity> GetFirmantesPJ(int id_firmante_pj)
        //{
        //    var lstFirmantesPJ = (from fpj in _unitOfWork.Db.Encomienda_Firmantes_PersonasJuridicas
        //                          join tpj in _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas on fpj.id_personajuridica equals tpj.id_personajuridica
        //                          join tc in _unitOfWork.Db.TiposDeCaracterLegal on fpj.id_tipocaracter equals tc.id_tipocaracter
        //                          join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on fpj.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
        //                          where fpj.id_personajuridica == id_firmante_pj
        //                          select new FirmantesPJEntity
        //                          {
        //                              IdPersonaJuridica = fpj.id_personajuridica,
        //                              Apellidos = fpj.Apellido,
        //                              Nombres = fpj.Nombres,
        //                              TipoDoc = tdoc.Nombre,
        //                              NroDoc = fpj.Nro_Documento,
        //                              NomTipoCaracter = tc.nom_tipocaracter,
        //                              IdTipoDocPersonal = fpj.id_tipodoc_personal,
        //                              Email = fpj.Email,
        //                              IdTipoCaracter = fpj.id_tipocaracter,
        //                              CargoFirmantePj = fpj.cargo_firmante_pj
        //                          });

        //    return lstFirmantesPJ;
        //}
    }
}