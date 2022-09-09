using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class UsuariosProfesionalesRolesClasificacionRepository : BaseRepository<Rel_UsuariosProf_Roles_Clasificacion> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public UsuariosProfesionalesRolesClasificacionRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstUsuariosRolesClasificacionEntity"></param>
        /// <returns></returns>
        public bool DeleteRange(ICollection<Rel_UsuariosProf_Roles_Clasificacion> lstUsuariosRolesClasificacionEntity)
        {
            try
            {
                var deleted = _unitOfWork.Db.Rel_UsuariosProf_Roles_Clasificacion.RemoveRange(lstUsuariosRolesClasificacionEntity);
                _unitOfWork.Db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Max()
        {
            var query = (from roles in _unitOfWork.Db.Rel_UsuariosProf_Roles_Clasificacion
                         select roles.id_rel_prof_clasificacion).Max();

            return query;
        }
	}
}

