using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class ConceptosBUIIndependientesRepository : BaseRepository<Conceptos_BUI_Independientes>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConceptosBUIIndependientesRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }

        public IEnumerable<Conceptos_BUI_Independientes> GetList(string[] arrCodConceptosCobrar)
        {
            var resul= (from con_bui in _unitOfWork.Db.Conceptos_BUI_Independientes
                        where arrCodConceptosCobrar.Contains(con_bui.keycode)
                        select con_bui);
            return resul;
        }
    }
}
