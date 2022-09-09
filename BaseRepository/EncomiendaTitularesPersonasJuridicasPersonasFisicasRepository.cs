using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class EncomiendaTitularesPersonasJuridicasPersonasFisicasRepository : BaseRepository<Encomienda_Titulares_PersonasJuridicas_PersonasFisicas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public EncomiendaTitularesPersonasJuridicasPersonasFisicasRepository(IUnitOfWork unit) : base(unit)
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
		public IEnumerable<Encomienda_Titulares_PersonasJuridicas_PersonasFisicas> GetByFKIdEncomienda(int IdEncomienda)
		{
			IEnumerable<Encomienda_Titulares_PersonasJuridicas_PersonasFisicas> domains = _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas.Where(x => 													
														x.id_encomienda == IdEncomienda											
														);
	
			return domains;
		}

        //public IEnumerable<Encomienda_Titulares_PersonasJuridicas_PersonasFisicas> existeTitularPJPF(int id_encomienda, int IdPersonaJuridica)
        //{
        //    var lstTitPJPF = (from tpf in _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas
        //                                  where tpf.id_encomienda == id_encomienda
        //                                  && tpf.id_personajuridica != IdPersonaJuridica
        //                                  select tpf);

        //    return lstTitPJPF;
        //}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdPersonaJuridica"></param>
		/// <returns></returns>	
		public IEnumerable<Encomienda_Titulares_PersonasJuridicas_PersonasFisicas> GetByFKIdPersonaJuridica(int IdPersonaJuridica)
		{
			IEnumerable<Encomienda_Titulares_PersonasJuridicas_PersonasFisicas> domains = _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas.Where(x => 													
														x.id_personajuridica == IdPersonaJuridica											
														);
	
			return domains;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoDocPersonal"></param>
		/// <returns></returns>	
		public IEnumerable<Encomienda_Titulares_PersonasJuridicas_PersonasFisicas> GetByFKIdTipoDocPersonal(int IdTipoDocPersonal)
		{
			IEnumerable<Encomienda_Titulares_PersonasJuridicas_PersonasFisicas> domains = _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas.Where(x => 													
														x.id_tipodoc_personal == IdTipoDocPersonal											
														);
	
			return domains;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdFirmantePj"></param>
		/// <returns></returns>	
		public IEnumerable<Encomienda_Titulares_PersonasJuridicas_PersonasFisicas> GetByFKIdFirmantePj(int IdFirmantePj)
		{
			IEnumerable<Encomienda_Titulares_PersonasJuridicas_PersonasFisicas> domains = _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas.Where(x => 													
														x.id_firmante_pj == IdFirmantePj											
														);
	
			return domains;
		}


        public IEnumerable<Encomienda_Titulares_PersonasJuridicas_PersonasFisicas> GetByIdEncomiendaIdPersonaJuridica(int id_encomienda, int IdPersonaJuridica)
        {
            IEnumerable<Encomienda_Titulares_PersonasJuridicas_PersonasFisicas> domains = _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas.Where(x =>
                                                                                         x.id_encomienda == id_encomienda
                                                                                         && x.id_personajuridica == IdPersonaJuridica
                                                                                         );
            return domains;
        }
	}
}

