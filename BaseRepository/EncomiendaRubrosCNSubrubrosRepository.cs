using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class EncomiendaRubrosCNSubrubrosRepository : BaseRepository<Encomienda_RubrosCN_Subrubros>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EncomiendaRubrosCNSubrubrosRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }

        public IEnumerable<Encomienda_RubrosCN_Subrubros> GetSubRubrosByEncomienda(int id_encomienda_ant)
        {
            var domains = _unitOfWork.Db.Encomienda_RubrosCN
                                            .Where(x => x.id_encomienda == id_encomienda_ant)
                                            .Select(x => x.id_encomiendarubro);

            return _unitOfWork.Db.Encomienda_RubrosCN_Subrubros.Where(x => domains.Contains(x.Id_EncRubro));
        }

        public IEnumerable<Encomienda_RubrosCN_Subrubros> GetSubRubrosByEncomiendaRubro(int idEncomiendaRubro, int id_encomienda_ant)
        {
            //var domains = _unitOfWork.Db.Encomienda_RubrosCN
            //                    .Where(x => x.id_encomienda == id_encomienda_ant)
            //                    .Select(x => x.id_encomiendarubro);

            return _unitOfWork.Db.Encomienda_RubrosCN_Subrubros.Where(x => x.Id_EncRubro == idEncomiendaRubro);
        }

        public IEnumerable<Encomienda_RubrosCN_Subrubros> GetRubrosSubrubrosCNVigentes(int idEncomiendaRubro, int id_encomienda)
        {
            return (from sub in _unitOfWork.Db.Encomienda_RubrosCN_Subrubros
                    join rub in _unitOfWork.Db.Encomienda_RubrosCN on sub.Id_EncRubro equals rub.id_encomiendarubro
                    join subr in _unitOfWork.Db.RubrosCN_Subrubros on sub.Id_rubrosubrubro equals subr.Id_rubroCNsubrubro
                    where (rub.id_encomiendarubro == idEncomiendaRubro) && (rub.id_encomienda == id_encomienda)
                    && (subr.VigenciaHasta_subrubro == null || subr.VigenciaHasta_subrubro > DateTime.Now)
                    select sub).ToList();
        }

    }
}
