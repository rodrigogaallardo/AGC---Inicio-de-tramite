using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class TransferenciasUbicacionPropiedadHorizontalRepository : BaseRepository<Transf_Ubicaciones_PropiedadHorizontal> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public TransferenciasUbicacionPropiedadHorizontalRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTransferenciaUbicacion"></param>
        /// <returns></returns>	
        public IEnumerable<Transf_Ubicaciones_PropiedadHorizontal> GetByFKIdTransferenciaUbicacion(int IdTransferenciaUbicacion)
		{
			IEnumerable<Transf_Ubicaciones_PropiedadHorizontal> domains = _unitOfWork.Db.Transf_Ubicaciones_PropiedadHorizontal.Where(x => 													
														x.id_transfubicacion == IdTransferenciaUbicacion
                                                        );
	
			return domains;
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public IEnumerable<Transf_Ubicaciones_PropiedadHorizontal> GetByFKIdSolicitud(int IdSolicitud)
        {
            IEnumerable<Transf_Ubicaciones_PropiedadHorizontal> domains =                
                (from  tubic in _unitOfWork.Db.Transf_Ubicaciones 
                    join tphor in _unitOfWork.Db.Transf_Ubicaciones_PropiedadHorizontal on tubic.id_transfubicacion equals tphor.id_transfubicacion
                    join phor in _unitOfWork.Db.Ubicaciones_PropiedadHorizontal on tphor.id_propiedadhorizontal equals phor.id_propiedadhorizontal
                 where tubic.id_solicitud == IdSolicitud
                 select tphor);

            return domains;
        }

        /// <summary>
        /// delete all collection OF CPadron_Ubicaciones_PropiedadHorizontal
        /// </summary>
        /// <param name="lstPropiedaHorizontalEntity"></param>
        /// <returns></returns>
        public bool DeleteRange(IEnumerable<Transf_Ubicaciones_PropiedadHorizontal> lstPropiedaHorizontalEntity)
        {
            try
            {
                var deleted = _unitOfWork.Db.Transf_Ubicaciones_PropiedadHorizontal.RemoveRange(lstPropiedaHorizontalEntity);
                _unitOfWork.Db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

	}
}

