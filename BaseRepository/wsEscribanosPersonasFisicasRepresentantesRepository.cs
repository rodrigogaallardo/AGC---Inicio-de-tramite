using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class wsEscribanosPersonasFisicasRepresentantesRepository : BaseRepository<wsEscribanos_PersonasFisicas_Representantes>
    {
        private readonly IUnitOfWork _unitOfWork;

        public wsEscribanosPersonasFisicasRepresentantesRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }

        public IEnumerable<wsEscribanosPersonasFisicasRepresentantesEntity> GetByFKIdWsPersonasFisica(int id_wsPersonaFisica)
        {
            var domains = (from acta_rpf in _unitOfWork.Db.wsEscribanos_PersonasFisicas_Representantes
                           join pf in _unitOfWork.Db.Encomienda_Firmantes_PersonasFisicas on acta_rpf.id_firmante_pf equals pf.id_firmante_pf
                           join titpf in _unitOfWork.Db.Encomienda_Titulares_PersonasFisicas on pf.id_personafisica equals titpf.id_personafisica
                           join tcl in _unitOfWork.Db.TiposDeCaracterLegal on pf.id_tipocaracter equals tcl.id_tipocaracter
                           join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on pf.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                           where acta_rpf.id_wsPersonaFisica == id_wsPersonaFisica
                           select new wsEscribanosPersonasFisicasRepresentantesEntity
                           {
                               id_wsRepresentantePF = acta_rpf.id_wsRepresentantePF,
                               id_wsPersonaFisica = acta_rpf.id_wsPersonaFisica,
                               fecha_poder = acta_rpf.fecha_poder,
                               nro_escritura_poder = acta_rpf.nro_escritura_poder,
                               nro_matricula_escribano_poder = acta_rpf.nro_matricula_escribano_poder,
                               registro_poder = acta_rpf.registro_poder,
                               jurisdiccion_poder = acta_rpf.jurisdiccion_poder,
                               Apellido = pf.Apellido,
                               Nombres = pf.Nombres,
                               TipoPersona = "PF",
                               Titular = titpf.Apellido + ", " + titpf.Nombres,
                               DescTipoDocPersonal = tdoc.Nombre,
                               Nro_Documento = pf.Nro_Documento,
                               nom_tipocaracter = tcl.nom_tipocaracter,
                               id_firmante = pf.id_firmante_pf,
                               id_firmante_pf = acta_rpf.id_firmante_pf,
                           });
            return domains;

        }
    }
}

