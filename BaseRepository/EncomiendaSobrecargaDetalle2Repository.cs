using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class EncomiendaSobrecargaDetalle2Repository : BaseRepository<Encomienda_Sobrecarga_Detalle2>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EncomiendaSobrecargaDetalle2Repository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }

        public IEnumerable<Encomienda_Sobrecarga_Detalle2> GetByFKIdSobrecargaDetalle1(int id_sobrecarga_detalle1)
        {
            IEnumerable<Encomienda_Sobrecarga_Detalle2> domains = _unitOfWork.Db.Encomienda_Sobrecarga_Detalle2.Where(x =>
                                                        x.id_sobrecarga_detalle1 == id_sobrecarga_detalle1
                                                        );

            return domains;
        }
    }
}

