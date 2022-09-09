using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class GrupoConsejosRepository : BaseRepository<GrupoConsejos> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public GrupoConsejosRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<GrupoConsejos> Get(Guid userId)
        {
           return  (from rel in _unitOfWork.Db.Rel_Usuarios_GrupoConsejo
             join grup in _unitOfWork.Db.GrupoConsejos on rel.id_grupoconsejo equals grup.id_grupoconsejo
             where rel.userid == userId 
            select grup);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstIdGrupoExcluir"></param>
        /// <returns></returns>
        public IEnumerable<GrupoConsejos> GetExcluye(List<int> lstIdGrupoExcluir)
        {
            var q = (from grupo in _unitOfWork.Db.GrupoConsejos
                     where grupo.nombre_grupoconsejo != "SE"
                     && !lstIdGrupoExcluir.Contains(grupo.id_grupoconsejo)
                     select grupo).Distinct();

            return q;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GrupoConsejos> GetAll()
        {
            var q = (from grupo in _unitOfWork.Db.GrupoConsejos
                     where grupo.nombre_grupoconsejo != "SE"
                     select grupo).Distinct();

            return q;

        }
      
	}
}

