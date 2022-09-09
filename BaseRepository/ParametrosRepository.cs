using System;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class ParametrosRepository : BaseRepository<Parametros>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ParametrosRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }

        public Parametros SingleByIDParametro(int idCodigoImpactoAmbiental)
        {
            var parametro = from p in _unitOfWork.Db.Parametros
                            join impAmb in _unitOfWork.Db.ImpactoAmbiental
                            on p.cod_param.ToUpper() equals impAmb.cod_Impacto.ToUpper()
                            where impAmb.id_ImpactoAmbiental == idCodigoImpactoAmbiental
                            select p;

            return parametro.SingleOrDefault();
        }

        public string GetParametroChar(string CodParam)
        {
            var parametro = _unitOfWork.Db.Parametros.Where(x=>x.cod_param==CodParam).Select(x=>x.valorchar_param).FirstOrDefault();
            return parametro;
        }
        public decimal? GetParametroNum(string CodParam)
        {
            var parametro = _unitOfWork.Db.Parametros.Where(x => x.cod_param == CodParam).Select(x => x.valornum_param).FirstOrDefault();
            return parametro;
        }

        public Parametros GetParametros(string codParam)
        {
            var parametros = _unitOfWork.Db.Parametros.Where(x => x.cod_param == codParam);
            return parametros.FirstOrDefault();
        }
    }
}

