using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class ConsultaPadronUbicacionPropiedadHorizontalRepository : BaseRepository<CPadron_Ubicaciones_PropiedadHorizontal> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public ConsultaPadronUbicacionPropiedadHorizontalRepository(IUnitOfWork unit) : base(unit)
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
		public IEnumerable<CPadron_Ubicaciones_PropiedadHorizontal> GetByFKIdConsultaPadronUbicacion(int IdConsultaPadronUbicacion)
		{
			IEnumerable<CPadron_Ubicaciones_PropiedadHorizontal> domains = _unitOfWork.Db.CPadron_Ubicaciones_PropiedadHorizontal.Where(x => 													
														x.id_cpadronubicacion == IdConsultaPadronUbicacion											
														);
	
			return domains;
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdConsultaPadron"></param>
        /// <returns></returns>
        public IEnumerable<CPadron_Ubicaciones_PropiedadHorizontal> GetByFKIdConsultaPadron(int IdConsultaPadron)
        {
            IEnumerable<CPadron_Ubicaciones_PropiedadHorizontal> domains =                
                (from  cpubic in _unitOfWork.Db.CPadron_Ubicaciones 
                    join cpphor in _unitOfWork.Db.CPadron_Ubicaciones_PropiedadHorizontal on cpubic.id_cpadronubicacion equals cpphor.id_cpadronubicacion
                    join phor in _unitOfWork.Db.Ubicaciones_PropiedadHorizontal on cpphor.id_propiedadhorizontal equals phor.id_propiedadhorizontal
                 where cpubic.id_cpadron == IdConsultaPadron   
                 select cpphor);

            return domains;
        }

        /// <summary>
        /// delete all collection OF CPadron_Ubicaciones_PropiedadHorizontal
        /// </summary>
        /// <param name="lstPropiedaHorizontalEntity"></param>
        /// <returns></returns>
        public bool DeleteRange(IEnumerable<CPadron_Ubicaciones_PropiedadHorizontal> lstPropiedaHorizontalEntity)
        {
            try
            {
                var deleted = _unitOfWork.Db.CPadron_Ubicaciones_PropiedadHorizontal.RemoveRange(lstPropiedaHorizontalEntity);
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

