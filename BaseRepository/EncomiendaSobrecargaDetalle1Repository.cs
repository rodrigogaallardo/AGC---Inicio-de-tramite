using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class EncomiendaSobrecargaDetalle1Repository : BaseRepository<Encomienda_Sobrecarga_Detalle1>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EncomiendaSobrecargaDetalle1Repository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }

        public IEnumerable<Encomienda_Sobrecarga_Detalle1> GetByFKIdSobrecarga(int id_sobrecarga)
        {
            IEnumerable<Encomienda_Sobrecarga_Detalle1> domains = _unitOfWork.Db.Encomienda_Sobrecarga_Detalle1.Where(x =>
                                                        x.id_sobrecarga == id_sobrecarga
                                                        );

            return domains;
        }
    }
}

