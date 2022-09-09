using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class EncomiendaEstadosRepository : BaseRepository<Encomienda_Estados> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public EncomiendaEstadosRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EncomiendaExt_Estados> GetAllExt()
        {
            return (from est in _unitOfWork.Db.EncomiendaExt_Estados
                    select est);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="id_estado_actual"></param>
        /// <param name="tipoTramite"></param>
        /// <returns></returns>
        public IEnumerable<EncomiendaExt_Estados> TraerEncomiendaExtEstadosSiguientes(Guid UserId, int id_estado_actual, int tipoTramite)
        {
            var query = 
                     (from est in _unitOfWork.Db.EncomiendaExt_Estados
                    join trans in _unitOfWork.Db.EncomiendaExt_TransicionEstados on est.id_estado equals trans.id_estado_siguiente
                    join roles in _unitOfWork.Db.aspnet_Roles on trans.Rol equals roles.RoleName
                    
                    where trans.id_estado_actual == id_estado_actual
                        && trans.tipoTramite == tipoTramite 
                        && roles.aspnet_Users.Any(p => p.UserId == UserId)  
                    select est);

            return query;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="id_estado_actual"></param>
        /// <returns></returns>
        public IEnumerable<Encomienda_Estados> TraerEncomiendaEstadosSiguientes(Guid UserId, int id_estado_actual)
        {
            return (from est in _unitOfWork.Db.Encomienda_Estados
                    join trans in _unitOfWork.Db.Encomienda_TransicionEstados on est.id_estado equals trans.id_estado_siguiente
                    join roles in _unitOfWork.Db.aspnet_Roles on trans.Rol equals roles.RoleName

                    where trans.id_estado_actual == id_estado_actual
                        && roles.aspnet_Users.Any(p => p.UserId == UserId)
                    select est).Distinct();
        }
	}
}

