using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using StaticClass;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class EncomiendaConformacionLocalRepository : BaseRepository<Encomienda_ConformacionLocal>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EncomiendaConformacionLocalRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>	
        public IEnumerable<Encomienda_ConformacionLocal> GetByFKIdEncomienda(int IdEncomienda)
        {
            IEnumerable<Encomienda_ConformacionLocal> domains = _unitOfWork.Db.Encomienda_ConformacionLocal.
                Where(x => x.id_encomienda == IdEncomienda);

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>
        public IEnumerable<Encomienda_ConformacionLocal> GetByFKIdEncomiendaGrilla(int IdEncomienda)
        {
            IEnumerable<Encomienda_ConformacionLocal> domains =
                (from confloc in _unitOfWork.Db.Encomienda_ConformacionLocal
                 join td in _unitOfWork.Db.TipoDestino on confloc.id_destino equals td.Id
                 join ilu in _unitOfWork.Db.tipo_iluminacion on confloc.id_iluminacion equals ilu.id_iluminacion
                 join vent in _unitOfWork.Db.tipo_ventilacion on confloc.id_ventilacion equals vent.id_ventilacion
                 join tipoSup in _unitOfWork.Db.TipoSuperficie on confloc.id_tiposuperficie equals tipoSup.Id
                 join enc_plan in _unitOfWork.Db.Encomienda_Plantas on confloc.id_encomiendatiposector equals enc_plan.id_encomiendatiposector
                 join tip_sec in _unitOfWork.Db.TipoSector on enc_plan.id_tiposector equals tip_sec.Id
                 where confloc.id_encomienda == IdEncomienda
                 select confloc
                 //select new EncomiendaConformacionLocalGrillaEntity
                 //{
                 //    id_encomienda = confloc.id_encomienda,
                 //    id_encomiendaconflocal = confloc.id_encomiendaconflocal,
                 //    alto_conflocal = confloc.alto_conflocal.Value,
                 //    ancho_conflocal = confloc.ancho_conflocal.Value,
                 //    Detalle_conflocal = confloc.Detalle_conflocal,
                 //    Frisos_conflocal = confloc.Frisos_conflocal,
                 //    largo_conflocal = confloc.largo_conflocal.Value,
                 //    Observaciones_conflocal = confloc.Observaciones_conflocal,
                 //    Paredes_conflocal = confloc.Paredes_conflocal,
                 //    Pisos_conflocal = confloc.Pisos_conflocal,
                 //    superficie_conflocal = confloc.superficie_conflocal.Value,
                 //    Techos_conflocal = confloc.Techos_conflocal,
                 //    desc_tipodestino = td.Nombre,
                 //    desc_planta = (tip_sec.Id == (int)Constantes.TipoSector.Otro ? enc_plan.detalle_encomiendatiposector : tip_sec.Nombre),
                 //    desc_ventilacion = vent.nom_ventilacion,
                 //    desc_iluminacion = ilu.nom_iluminacion,
                 //    desc_tiposuperficie = tipoSup.Nombre
                 //}
            );

            return domains;
        }
    }
}

