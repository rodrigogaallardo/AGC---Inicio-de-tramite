using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class ConsultaPadronTitularesPersonasFisicasRepository : BaseRepository<CPadron_Titulares_PersonasFisicas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public ConsultaPadronTitularesPersonasFisicasRepository(IUnitOfWork unit) : base(unit)
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
        /// <param name="IdPersonaFisica"></param>
        /// <returns></returns>
        public IEnumerable<CPadron_Titulares_PersonasFisicas> GetByIdConsultaPadronCuitIdPersonaFisica(int IdSolicitud, string Cuit, int IdPersonaFisica)
        {
            var lstTitularPersonaJur = (from tpf in _unitOfWork.Db.CPadron_Titulares_PersonasFisicas
                                        where tpf.id_cpadron == IdSolicitud
                                        && tpf.id_personafisica != IdPersonaFisica
                                        && tpf.Cuit == Cuit
                                        select tpf);

            return lstTitularPersonaJur;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public IEnumerable<CPadron_Titulares_PersonasFisicas> GetByFKIdConsultaPadron(int IdSolicitud)
        {
            var lstTitularPersonaJur = (from tpf in _unitOfWork.Db.CPadron_Titulares_PersonasFisicas
                                        where tpf.id_cpadron == IdSolicitud                                                                                
                                        select tpf);
            return lstTitularPersonaJur;
        }
	}
}

