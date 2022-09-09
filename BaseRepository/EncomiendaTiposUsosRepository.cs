using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class EncomiendaTiposUsosRepository : BaseRepository<Encomienda_Tipos_Usos>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EncomiendaTiposUsosRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_tipo_destino"></param>
        /// <param name="nroGrupo"></param>
        /// <returns></returns>
        public IEnumerable<Encomienda_Tipos_Usos> GetByFKIdTipoDestinoGrupo(int id_tipo_destino, int nroGrupo)
        {
            IEnumerable<Encomienda_Tipos_Usos> domains = (from td in _unitOfWork.Db.Encomienda_Tipos_Usos
                                                               join rel in _unitOfWork.Db.Encomienda_Rel_TiposDestinos_TiposUsos on td.id_tipo_uso equals rel.id_tipo_uso
                                                               where rel.id_tipo_destino == id_tipo_destino && td.nro_grupo==nroGrupo
                                                                select td
                                                        );

            return domains;
        }

    }
}

