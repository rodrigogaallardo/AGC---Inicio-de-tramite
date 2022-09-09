using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class wsEscribanosPersonasJuridicasRepresentantesRepository : BaseRepository<wsEscribanos_PersonasJuridicas_Representantes> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public wsEscribanosPersonasJuridicasRepresentantesRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }

        public IEnumerable<wsEscribanosPersonasJuridicasRepresentantesEntity> GetByFKIdWsPersonasJuridica(int id_wsPersonaJuridica)
        {
            var domains = (from acta_rpj in _unitOfWork.Db.wsEscribanos_PersonasJuridicas_Representantes
                           join pj in _unitOfWork.Db.Encomienda_Firmantes_PersonasJuridicas on acta_rpj.id_firmante_pj equals pj.id_firmante_pj
                           join titpj in _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas on pj.id_personajuridica equals titpj.id_personajuridica
                           join tcl in _unitOfWork.Db.TiposDeCaracterLegal on pj.id_tipocaracter equals tcl.id_tipocaracter
                           join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on pj.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                           where acta_rpj.id_wsPersonaJuridica == id_wsPersonaJuridica
                           select new wsEscribanosPersonasJuridicasRepresentantesEntity
                           {
                               id_wsRepresentantePJ=acta_rpj.id_wsRepresentantePJ,
                               id_wsPersonaJuridica = acta_rpj.id_wsPersonaJuridica,
                               fecha_designacion=acta_rpj.fecha_designacion,
                               nro_escritura_designacion=acta_rpj.nro_escritura_designacion,
                               nro_matricula_escribano_designacion=acta_rpj.nro_matricula_escribano_designacion,
                               fecha_escritura_designacion=acta_rpj.fecha_escritura_designacion,
                               jurisdiccion_designacion=acta_rpj.jurisdiccion_designacion,
                               apellido=pj.Apellido,
                               nombres= pj.Nombres,
                               TipoPersona = "PJ",
                               Titular=titpj.Razon_Social,
                               DescTipoDocPersonal=tdoc.Nombre,
                               Nro_Documento=pj.Nro_Documento,
                               nom_tipocaracter=tcl.nom_tipocaracter,
                               id_firmante=pj.id_firmante_pj,
                               id_firmante_pj=acta_rpj.id_firmante_pj,
                               registro_designacion=acta_rpj.registro_designacion
                           });
            return domains;
        }
    }
}

