using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;
using StaticClass;


namespace BaseRepository
{
    public class ENGGruposCircuitosTipoTramiteRepository : BaseRepository<ENG_Grupos_Circuitos_Tipo_Tramite>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ENGGruposCircuitosTipoTramiteRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }

        public ENG_Grupos_Circuitos_Tipo_Tramite GetByFKIdGrupo(string nomGrupo)
        {
            ENG_Grupos_Circuitos_Tipo_Tramite domains = _unitOfWork.Db.ENG_Grupos_Circuitos_Tipo_Tramite.Where(x =>
                                                        x.ENG_Grupos_Circuitos.cod_grupo_circuito == nomGrupo ).FirstOrDefault();

            return domains;
        }
    }
}
