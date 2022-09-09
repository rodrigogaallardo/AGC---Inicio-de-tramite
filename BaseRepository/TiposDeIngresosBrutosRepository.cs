using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class TiposDeIngresosBrutosRepository : BaseRepository<TiposDeIngresosBrutos>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TiposDeIngresosBrutosRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }

        public IEnumerable<TiposDeIngresosBrutos> GetByIdTipoIb(int IdTipoIb)
        {
            var query = (from tdib in _unitOfWork.Db.TiposDeIngresosBrutos
                         where tdib.id_tipoiibb == IdTipoIb
                         select tdib);
            return query;
        }

        public IEnumerable<TiposDeIngresosBrutos> GetIngresosBrutos()
        {
            var query = (from tdib in _unitOfWork.Db.TiposDeIngresosBrutos
                         where tdib.id_tipoiibb > 0
                         select tdib);
            return query;
        }
    }
}

