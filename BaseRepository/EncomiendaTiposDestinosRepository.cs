using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class EncomiendaTiposDestinosRepository : BaseRepository<Encomienda_Tipos_Destinos>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EncomiendaTiposDestinosRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }

        public IEnumerable<Encomienda_Tipos_Destinos> GetByFKIdTipoSobrecarga(int id_tipo_sobrecarga)
        {
            IEnumerable<Encomienda_Tipos_Destinos> domains = (from td in _unitOfWork.Db.Encomienda_Tipos_Destinos
                                                               join rel in _unitOfWork.Db.Encomienda_Rel_TiposSobrecargas_TiposDestinos on td.id_tipo_destino equals rel.id_tipo_destino
                                                               where rel.id_tipo_sobrecarga == id_tipo_sobrecarga
                                                               select td
                                                        );

            return domains;
        }
    }
}

