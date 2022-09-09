using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class LocalidadRepository : BaseRepository<Localidad> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public LocalidadRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }	   	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdProvincia"></param>
		/// <returns></returns>	
		public IEnumerable<Localidad> GetByFKIdProvincia(int IdProvincia)
		{
			IEnumerable<Localidad> domains = _unitOfWork.Db.Localidad.Where(x => 													
														x.IdProvincia == IdProvincia											
														);
	
			return domains;
		}

        public IEnumerable<Localidad> GetByFKIdProvinciaExcluir(int IdProvincia, bool Excluir)
        {
            var localidades = (from l in _unitOfWork.Db.Localidad
                                              join p in _unitOfWork.Db.Provincia on l.IdProvincia equals p.Id
                                              where p.Id == IdProvincia && l.Excluir == Excluir
                                              orderby p.Nombre
                                              select l);
            return localidades;
        }
	}
}

