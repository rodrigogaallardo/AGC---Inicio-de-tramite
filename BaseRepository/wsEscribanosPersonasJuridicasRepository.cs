using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using StaticClass;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class wsEscribanosPersonasJuridicasRepository : BaseRepository<wsEscribanos_PersonasJuridicas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public wsEscribanosPersonasJuridicasRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }

        public IEnumerable<wsEscribanosPersonasJuridicasEntity> GetByFKIdActanotarial(int id_actanotarial)
        {
            var domains = (from acta in _unitOfWork.Db.wsEscribanos_PersonasJuridicas
                           join pf in _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas on acta.id_personajuridica equals pf.id_personajuridica
                           where acta.id_actanotarial == id_actanotarial
                           select new wsEscribanosPersonasJuridicasEntity
                           {
                               id_wsPersonaJuridica = acta.id_wsPersonaJuridica,
                               id_actanotarial = acta.id_actanotarial,
                               id_personajuridica = acta.id_personajuridica,
                               fecha_ultimo_pago_IIBB = acta.fecha_ultimo_pago_IIBB,
                               porcentaje_titularidad = acta.porcentaje_titularidad,
                               direccion_sede_social = acta.direccion_sede_social,
                               fecha_contrato_social = acta.fecha_contrato_social,
                               instrumento_constitucion = acta.instrumento_constitucion,
                               nro_matricula_escribano_constitucion = acta.nro_matricula_escribano_constitucion,
                               nro_escritura_constitucion = acta.nro_escritura_constitucion,
                               registro_constitucion = acta.registro_constitucion,
                               jurisdiccion_constitucion = acta.jurisdiccion_constitucion,
                               nom_organismo_inscripcion = acta.nom_organismo_inscripcion,
                               fecha_incripcion = acta.fecha_incripcion,
                               datos_incripcion = acta.datos_incripcion,
                               nom_tipo_IVA = acta.nom_tipo_IVA,
                               TipoPersona = Constantes.TipoPersonaJuridica,
                               TipoPersonaDesc = Constantes.TipoPersonaJuridica_Desc,
                               id_persona = pf.id_personajuridica,
                               ApellidoNomRazon = pf.Razon_Social,
                               cuit = pf.CUIT,
                               Domicilio = pf.Calle + " " + pf.NroPuerta.ToString() + (!string.IsNullOrEmpty(pf.Piso) ? " Piso: " + pf.Piso : "")
                               + (!string.IsNullOrEmpty(pf.Depto) ? "  Depto/UF: " + pf.Depto : "")
                           });
            return domains;
        }
    }
}

