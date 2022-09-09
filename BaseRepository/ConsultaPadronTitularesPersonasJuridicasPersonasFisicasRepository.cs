using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class ConsultaPadronTitularesPersonasJuridicasPersonasFisicasRepository : BaseRepository<CPadron_Titulares_PersonasJuridicas_PersonasFisicas>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConsultaPadronTitularesPersonasJuridicasPersonasFisicasRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }
        public IEnumerable<CPadron_Titulares_PersonasJuridicas_PersonasFisicas> GetByFKIdConsultaPadron(int IdSolicitud)
        {
            return (from entity in _unitOfWork.Db.CPadron_Titulares_PersonasJuridicas_PersonasFisicas
                    where entity.id_cpadron == IdSolicitud
                    select entity);
        }
    }     
}

