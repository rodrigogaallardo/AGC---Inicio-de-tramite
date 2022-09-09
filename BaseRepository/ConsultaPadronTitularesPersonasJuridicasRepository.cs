using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class ConsultaPadronTitularesPersonasJuridicasRepository : BaseRepository<CPadron_Titulares_PersonasJuridicas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public ConsultaPadronTitularesPersonasJuridicasRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <param name="Cuit"></param>
        /// <param name="IdPersonaJuridica"></param>
        /// <returns></returns>
        public IEnumerable<CPadron_Titulares_PersonasJuridicas> GetByIdConsultaPadronCuitIdPersonaJuridica(int IdSolicitud, string Cuit, int IdPersonaJuridica)
        {
            var lstTitularPersonaJur = (from tpf in _unitOfWork.Db.CPadron_Titulares_PersonasJuridicas
                                        where tpf.id_cpadron == IdSolicitud
                                        && tpf.id_personajuridica != IdPersonaJuridica
                                        && tpf.CUIT == Cuit
                                        select tpf);

            return lstTitularPersonaJur;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public IEnumerable<CPadron_Titulares_PersonasJuridicas> GetByFKIdConsultaPadron(int IdSolicitud)
        {
            var lstTitularPersonaJur = (from tpf in _unitOfWork.Db.CPadron_Titulares_PersonasJuridicas
                                        where tpf.id_cpadron == IdSolicitud
                                        select tpf);
            return lstTitularPersonaJur;
        }
	}
}

