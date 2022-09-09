using Dal.UnitOfWork;
using DataAcess;
using System;
using System.Linq;

namespace BaseRepository
{
    public class ApraTiposTramiteRepository : BaseRepository<APRA_TiposDeTramite>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApraTiposTramiteRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTipoTramite"></param>
        /// <returns></returns>
        public APRA_TiposDeTramite Get(int IdTipoTramite)
        {
            return (from tipo in _unitOfWork.Db.APRA_TiposDeTramite
                    where tipo.id_tipotramite == IdTipoTramite
                    select tipo).FirstOrDefault();
        }
    }
}
