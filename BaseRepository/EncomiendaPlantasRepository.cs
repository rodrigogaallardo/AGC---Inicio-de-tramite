using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;
using StaticClass;

namespace BaseRepository
{
    public class EncomiendaPlantasRepository : BaseRepository<Encomienda_Plantas>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EncomiendaPlantasRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_encomienda"></param>
        /// <returns></returns>	
        public IEnumerable<Encomienda_Plantas> GetByFKIdEncomienda(int id_encomienda)
        {
            IEnumerable<Encomienda_Plantas> domains = _unitOfWork.Db.Encomienda_Plantas.Where(x => x.id_encomienda == id_encomienda);

            return domains;
        }


        public IEnumerable<EncomiendaPlantasEntity> Get(int IdEncomienda)
        {
            var q = (from tipsec in _unitOfWork.Db.TipoSector
                     join caaplan in _unitOfWork.Db.Encomienda_Plantas.Where(x => x.id_encomienda == IdEncomienda)
                     on tipsec.Id equals caaplan.id_tiposector into res
                     from caaplan_Empty in res.DefaultIfEmpty()
                     where
                        (
                          (tipsec.Ocultar == null || tipsec.Ocultar.Value == false) ||
                          (tipsec.Ocultar == true && caaplan_Empty.id_tiposector > 0)
                        )
                     orderby tipsec.Id   // Muy importante este orden - no cambiar , alteraria la funcionalidad
                     select new EncomiendaPlantasEntity()
                     {
                         IdTipoSector = tipsec.Id,
                         Seleccionado = (caaplan_Empty.id_tiposector != null ? caaplan_Empty.id_tiposector > 0 : false),
                         Descripcion = tipsec.Descripcion,
                         MuestraCampoAdicional = (tipsec.MuestraCampoAdicional.HasValue ? tipsec.MuestraCampoAdicional.Value : false),
                         Detalle = string.IsNullOrEmpty(caaplan_Empty.detalle_encomiendatiposector) ? "" : caaplan_Empty.detalle_encomiendatiposector,
                         TamanoCampoAdicional = (tipsec.TamanoCampoAdicional.HasValue ? tipsec.TamanoCampoAdicional.Value : 0),
                         Ocultar = tipsec.Ocultar,
                         TipoSector = tipsec
                     });
            return q;
        }

        public Encomienda_Plantas GetByFKIdEncomiendaIdEncomiendaTiposector(int IdEncomienda, int IdEncomiendaTiposector)
        {
            var p = _unitOfWork.Db.Encomienda_Plantas.Where(x => x.id_encomiendatiposector == IdEncomiendaTiposector).First();
            var q = _unitOfWork.Db.Encomienda_Plantas.Where(x => x.id_encomienda == IdEncomienda &&
                            x.id_tiposector == p.id_tiposector &&
                            x.detalle_encomiendatiposector == p.detalle_encomiendatiposector).First();
            return q;
        }
    }
}

