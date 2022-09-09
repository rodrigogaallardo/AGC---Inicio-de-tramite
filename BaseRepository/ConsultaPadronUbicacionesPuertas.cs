using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class ConsultaPadronUbicacionesPuertasRepository : BaseRepository<CPadron_Ubicaciones_Puertas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public ConsultaPadronUbicacionesPuertasRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdConsultaPadronUbicacion"></param>
		/// <returns></returns>	
		public IEnumerable<CPadron_Ubicaciones_Puertas> GetByFKIdConsultaPadronUbicacion(int IdConsultaPadronUbicacion)
		{
			IEnumerable<CPadron_Ubicaciones_Puertas> domains = _unitOfWork.Db.CPadron_Ubicaciones_Puertas.Where(x => 													
														x.id_cpadronubicacion == IdConsultaPadronUbicacion											
														);
	
			return domains;
		}
     
        public bool DeleteRange(IEnumerable<CPadron_Ubicaciones_Puertas> lstUbicacionesPuertaEntity)
        {
            try
            {
                var deleted = _unitOfWork.Db.CPadron_Ubicaciones_Puertas.RemoveRange(lstUbicacionesPuertaEntity);
                _unitOfWork.Db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }

	}
}

