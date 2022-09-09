using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class TransferenciasUbicacionesPuertasRepository : BaseRepository<Transf_Ubicaciones_Puertas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public TransferenciasUbicacionesPuertasRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTransferenciasUbicacion"></param>
        /// <returns></returns>	
        public IEnumerable<Transf_Ubicaciones_Puertas> GetByFKIdTransferenciaUbicacion(int IdTransferenciaUbicacion)
		{
			IEnumerable<Transf_Ubicaciones_Puertas> domains = _unitOfWork.Db.Transf_Ubicaciones_Puertas.Where(x => 													
														x.id_transfubicacion == IdTransferenciaUbicacion
                                                        );
	
			return domains;
		}
     
        public bool DeleteRange(IEnumerable<Transf_Ubicaciones_Puertas> lstUbicacionesPuertaEntity)
        {
            try
            {
                var deleted = _unitOfWork.Db.Transf_Ubicaciones_Puertas.RemoveRange(lstUbicacionesPuertaEntity);
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

