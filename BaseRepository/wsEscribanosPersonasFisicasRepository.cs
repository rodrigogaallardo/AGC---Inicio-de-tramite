using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using StaticClass;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class wsEscribanosPersonasFisicasRepository : BaseRepository<wsEscribanos_PersonasFisicas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public wsEscribanosPersonasFisicasRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }

        public IEnumerable<wsEscribanosPersonasFisicasEntity> GetByFKIdActanotarial(int id_actanotarial)
        {
            var domains = (from acta in _unitOfWork.Db.wsEscribanos_PersonasFisicas
                           join pf in _unitOfWork.Db.Encomienda_Titulares_PersonasFisicas on acta.id_personafisica equals pf.id_personafisica
                           join tdoc in _unitOfWork.Db.TipoDocumentoPersonal on pf.id_tipodoc_personal equals tdoc.TipoDocumentoPersonalId
                           where acta.id_actanotarial == id_actanotarial
                           select new wsEscribanosPersonasFisicasEntity
                           {
                               id_wsPersonasFisicas = acta.id_wsPersonasFisicas,
                               id_actanotarial = acta.id_actanotarial,
                               id_personafisica = acta.id_personafisica,
                               fecha_ultimo_pago_IIBB = acta.fecha_ultimo_pago_IIBB,
                               porcentaje_titularidad = acta.porcentaje_titularidad,
                               apellido = pf.Apellido,
                               nombres = pf.Nombres,
                               Nro_Documento = pf.Nro_Documento,
                               DescTipoDocPersonal = tdoc.Nombre,
                               TipoPersona = Constantes.TipoPersonaFisica,
                               TipoPersonaDesc = Constantes.TipoPersonaFisica_Desc,
                               id_persona = pf.id_personafisica,
                               ApellidoNomRazon = pf.Apellido + " " + pf.Nombres,
                               cuit = pf.Cuit,
                               Domicilio = pf.Calle + " " + pf.Nro_Puerta.ToString() + (!string.IsNullOrEmpty(pf.Piso) ? " Piso: " + pf.Piso : "")
                               + (!string.IsNullOrEmpty(pf.Depto) ? "  Depto/UF: " + pf.Depto : "")
                           });
            return domains;
        }

    }
}

