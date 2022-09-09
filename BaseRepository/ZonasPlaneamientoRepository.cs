using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class ZonasPlaneamientoRepository : BaseRepository<Zonas_Planeamiento> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public ZonasPlaneamientoRepository(IUnitOfWork unit) : base(unit)
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
        public IEnumerable<Zonas_Planeamiento> GetZonasEncomienda(int IdEncomienda)
        {
            var query = (from encubic in _unitOfWork.Db.Encomienda_Ubicaciones
                         join zon in _unitOfWork.Db.Zonas_Planeamiento on encubic.id_zonaplaneamiento equals zon.id_zonaplaneamiento
                         where encubic.id_encomienda == IdEncomienda
                         select zon);

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>
        public IEnumerable<Zonas_Planeamiento> GetZonaPlaneamientoByIdEncomienda(int IdEncomienda)
        {
            var domains = (from enc in _unitOfWork.Db.Encomienda
                           join zp in _unitOfWork.Db.Zonas_Planeamiento on enc.ZonaDeclarada equals zp.CodZonaPla
                           where enc.id_encomienda == IdEncomienda
                           select zp);
            return domains;
        }

        public IEnumerable<Rel_ZonasPlaneamiento_ZonasHabilitaciones> GetZonaHabByCodZonaPla(string CodZonaPla)
        {
            var domains = _unitOfWork.Db.Rel_ZonasPlaneamiento_ZonasHabilitaciones.Where(x => x.CodZonaLey449 == CodZonaPla);
            return domains;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>
        public IEnumerable<Zonas_Planeamiento> GetZonaComplementariaEncomienda(int IdEncomienda)
        {
            var lstZonasComp = (from encubic in _unitOfWork.Db.Encomienda_Ubicaciones
                                join ubic_zncomp in _unitOfWork.Db.Ubicaciones_ZonasComplementarias on encubic.id_ubicacion equals ubic_zncomp.id_ubicacion
                                join zonpla in _unitOfWork.Db.Zonas_Planeamiento on ubic_zncomp.id_zonaplaneamiento equals zonpla.id_zonaplaneamiento
                                where encubic.id_encomienda == IdEncomienda
                                select zonpla);
            return lstZonasComp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdConsultaPadron"></param>
        /// <returns></returns>
        public IEnumerable<Zonas_Planeamiento> GetZonasConsultaPadron(int IdConsultaPadron)
        {
            var query = (from cpaubic in _unitOfWork.Db.CPadron_Ubicaciones
                         join zon in _unitOfWork.Db.Zonas_Planeamiento on cpaubic.id_zonaplaneamiento equals zon.id_zonaplaneamiento
                         where cpaubic.id_cpadron == IdConsultaPadron
                         select zon);

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CodZonaPla"></param>
        /// <returns></returns>
        public IEnumerable<Zonas_Planeamiento> GetByCodZonaPlaneamiento(string CodZonaPla)
        {
            var query = _unitOfWork.Db.Zonas_Planeamiento.Where(x => x.CodZonaPla == CodZonaPla);

            return query;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdConsultaPadron"></param>
        /// <returns></returns>
        public IEnumerable<Zonas_Planeamiento> GetZonaComplementariaConsultaPadron(int IdConsultaPadron)
        {
            var lstZonasComp = (from cpaubic in _unitOfWork.Db.CPadron_Ubicaciones
                                join ubic_zncomp in _unitOfWork.Db.Ubicaciones_ZonasComplementarias on cpaubic.id_ubicacion equals ubic_zncomp.id_ubicacion
                                join zonpla in _unitOfWork.Db.Zonas_Planeamiento on ubic_zncomp.id_zonaplaneamiento equals zonpla.id_zonaplaneamiento
                                where cpaubic.id_cpadron == IdConsultaPadron
                                select zonpla);
            return lstZonasComp;
        }
    }
}

