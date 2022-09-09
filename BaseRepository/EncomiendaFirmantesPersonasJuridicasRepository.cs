using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class EncomiendaFirmantesPersonasJuridicasRepository : BaseRepository<Encomienda_Firmantes_PersonasJuridicas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public EncomiendaFirmantesPersonasJuridicasRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdEncomienda"></param>
		/// <returns></returns>	
		public IEnumerable<Encomienda_Firmantes_PersonasJuridicas> GetByFKIdEncomienda(int IdEncomienda)
		{
			IEnumerable<Encomienda_Firmantes_PersonasJuridicas> domains = _unitOfWork.Db.Encomienda_Firmantes_PersonasJuridicas.Where(x => 													
														x.id_encomienda == IdEncomienda											
														);
	
			return domains;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdPersonaJuridica"></param>
		/// <returns></returns>	
		public IEnumerable<Encomienda_Firmantes_PersonasJuridicas> GetByFKIdPersonaJuridica(int IdPersonaJuridica)
		{
			IEnumerable<Encomienda_Firmantes_PersonasJuridicas> domains = _unitOfWork.Db.Encomienda_Firmantes_PersonasJuridicas.Where(x => 													
														x.id_personajuridica == IdPersonaJuridica											
														);
	
			return domains;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoDocPersonal"></param>
		/// <returns></returns>	
		public IEnumerable<Encomienda_Firmantes_PersonasJuridicas> GetByFKIdTipoDocPersonal(int IdTipoDocPersonal)
		{
			IEnumerable<Encomienda_Firmantes_PersonasJuridicas> domains = _unitOfWork.Db.Encomienda_Firmantes_PersonasJuridicas.Where(x => 													
														x.id_tipodoc_personal == IdTipoDocPersonal											
														);
	
			return domains;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoCaracter"></param>
		/// <returns></returns>	
		public IEnumerable<Encomienda_Firmantes_PersonasJuridicas> GetByFKIdTipoCaracter(int IdTipoCaracter)
		{
			IEnumerable<Encomienda_Firmantes_PersonasJuridicas> domains = _unitOfWork.Db.Encomienda_Firmantes_PersonasJuridicas.Where(x => 													
														x.id_tipocaracter == IdTipoCaracter											
														);
	
			return domains;
		}

        public string[] GetCargoFirmantesPersonasJuridicas()
        {
            var query = (from cfpj in _unitOfWork.Db.Encomienda_Firmantes_PersonasJuridicas
                         where cfpj.cargo_firmante_pj.Trim().Length > 0
                         select cfpj.cargo_firmante_pj).Distinct().OrderBy(x => x).ToArray();
            return query;
        }

        public IEnumerable<Encomienda_Firmantes_PersonasJuridicas> GetByIdEncomiendaIdPersonaJuridica(int id_encomienda, int IdPersonaJuridica)
        {
            IEnumerable<Encomienda_Firmantes_PersonasJuridicas> domains = _unitOfWork.Db.Encomienda_Firmantes_PersonasJuridicas.Where(x =>
                                                                            x.id_encomienda == id_encomienda
                                                                            && x.id_personajuridica == IdPersonaJuridica
                                                                            );
            return domains;
        }
	}
}

