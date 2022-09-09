using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class EscribanoRepository : BaseRepository<Escribano>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EscribanoRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }
        public IQueryable<Escribano> GetEscribanos(int BusMatricula, string BusApeNom)
        {
            var lstEscribanos = (from esc in _unitOfWork.Db.Escribano
                                 select esc
                                );

            if (BusMatricula > 0)
                lstEscribanos = lstEscribanos.Where(x => x.Matricula == BusMatricula);

            if (BusApeNom.Length > 1)
            {
                string[] lstApeNom = BusApeNom.Split(new char[0]);

                List<int> matriculas = new List<int>();

                foreach (string str in lstApeNom)
                {
                    var lst = lstEscribanos.Where(x => x.ApyNom.Contains(str)).Select(s => s.Matricula).ToList();
                    matriculas.AddRange(lst);
                }

                lstEscribanos = lstEscribanos.Where(x => matriculas.Contains(x.Matricula));
            }

            return lstEscribanos;
        }
    }
}
