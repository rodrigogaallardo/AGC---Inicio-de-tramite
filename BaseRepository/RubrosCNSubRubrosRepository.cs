using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;
using System.Data.Entity.Core.Objects;
using System.Data;
using System.Data.Entity;
using DataTransferObject;

namespace BaseRepository
{
    public class RubrosCNSubRubrosRepository : BaseRepository<RubrosCN_Subrubros>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RubrosCNSubRubrosRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }

        public IEnumerable<RubrosCN_Subrubros> GetSubRubros(int idRubro)
        {
            var domains = (from sRub in _unitOfWork.Db.RubrosCN_Subrubros                           
                           where sRub.Id_rubroCN == idRubro
                           select sRub).ToList();

            return domains;
        }

        public IEnumerable<ItemRubrosCNSubRubrosDTO> GetSubRubrosVigentes(int idRubro)
        {
            var domains = (from rs in _unitOfWork.Db.RubrosCN_Subrubros
                           where rs.Id_rubroCN == idRubro && rs.VigenciaDesde_subrubro < DateTime.Now
                           && (rs.VigenciaHasta_subrubro == null || rs.VigenciaHasta_subrubro > DateTime.Now)
                           select new ItemRubrosCNSubRubrosDTO
                           {
                               Id_rubroCNsubrubro = rs.Id_rubroCNsubrubro,
                               Id_rubroCN = rs.Id_rubroCN,
                               Nombre = rs.Nombre,
                               IdGrupoCircuito = rs.IdGrupoCircuito
                           }).ToList();

            return domains;
        }

        public IEnumerable<RubrosCN_Subrubros> GetSubRubrosCN(int IdEncomienda)
        {
            var resolution =
                            from ubicrub in _unitOfWork.Db.Encomienda_RubrosCN
                            join rub in _unitOfWork.Db.Encomienda_RubrosCN_Subrubros on ubicrub.id_encomiendarubro equals rub.Id_EncRubro
                            join srub in _unitOfWork.Db.RubrosCN_Subrubros on rub.Id_rubrosubrubro equals srub.Id_rubroCNsubrubro
                            where
                            ubicrub.id_encomienda == IdEncomienda
                            select srub;
            return resolution;
        }

    }
}
