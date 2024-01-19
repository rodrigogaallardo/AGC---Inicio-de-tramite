using Dal.UnitOfWork;
using DataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRepository
{
    public class EncomiendaExternaRepository : BaseRepository<EncomiendaExt>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EncomiendaExternaRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="NroTramite"></param>
        /// <returns></returns>
        public EncomiendaExt Get(string NroTramite, int IdTipoTramite)
        {
            return (from enc in _unitOfWork.Db.EncomiendaExt
                    where enc.nroTramite == NroTramite
                    && enc.TipoTramite == IdTipoTramite
                    select enc).FirstOrDefault(); 
        }
    }
}
