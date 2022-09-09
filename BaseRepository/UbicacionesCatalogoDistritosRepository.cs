using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using Dal.UnitOfWork;
using StaticClass;

namespace BaseRepository
{
    public class UbicacionesCatalogoDistritosRepository : BaseRepository<Ubicaciones_CatalogoDistritos>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UbicacionesCatalogoDistritosRepository(IUnitOfWork unit) : base(unit)
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
        public IEnumerable<Ubicaciones_CatalogoDistritos> GetDistritosEncomienda(int IdEncomienda)
        {
            EncomiendaUbicacionesRepository ubicacion = new EncomiendaUbicacionesRepository(_unitOfWork);
            int idUbi = (int)ubicacion.GetByFKIdEncomienda(IdEncomienda).FirstOrDefault().id_ubicacion;

            var query = (from ubiDis in _unitOfWork.Db.Ubicaciones_Distritos
                         join ubiCat in _unitOfWork.Db.Ubicaciones_CatalogoDistritos on ubiDis.IdDistrito equals ubiCat.IdDistrito
                         where ubiDis.id_ubicacion == idUbi
                         select ubiCat).ToList();

            return query;
        }

        public IEnumerable<Ubicaciones_CatalogoDistritos> GetDistritosUbicacion(int IdUbicacion)
        {
            //var query = (from ubi in _unitOfWork.Db.Ubicaciones_CatalogoDistritos
            //             from ubiDis in ubi.Ubicaciones.Where(x => x.id_ubicacion == IdUbicacion)
            //             select ubi).ToList();
            var query = (from ubiDis in _unitOfWork.Db.Ubicaciones_Distritos
                         join ubiCat in _unitOfWork.Db.Ubicaciones_CatalogoDistritos on ubiDis.IdDistrito equals ubiCat.IdDistrito
                         where ubiDis.id_ubicacion == IdUbicacion
                         select ubiCat).ToList();

            return query;
        }

        public IEnumerable<Ubicaciones_CatalogoDistritos> GetDistritosUbicacion(List<int> lstUbi)
        {
            //var query = (from ubi in _unitOfWork.Db.Ubicaciones_CatalogoDistritos
            //             from ubiDis in ubi.Ubicaciones.Where(x => lstUbi.Contains(x.id_ubicacion))
            //             select ubi).Distinct().ToList();

            var query = (from ubiDis in _unitOfWork.Db.Ubicaciones_Distritos
                         join ubiCat in _unitOfWork.Db.Ubicaciones_CatalogoDistritos on ubiDis.IdDistrito equals ubiCat.IdDistrito
                         where lstUbi.Contains(ubiDis.id_ubicacion)
                         select ubiCat).ToList();

            return query;
        }

        public Ubicaciones_CatalogoDistritos GetDisrito(string codigo)
        {
            var query = (from ubi in _unitOfWork.Db.Ubicaciones_CatalogoDistritos
                         where ubi.Codigo == codigo
                         select ubi).FirstOrDefault();

            return query;
        }

        public int? GetIdZonaByUbicacion(int idUbicacion)
        {
            int? zona = (from u in _unitOfWork.Db.Ubicaciones_Distritos
                        where u.id_ubicacion == idUbicacion
                        select u.IdZona).FirstOrDefault();

            return zona;
        }

        public int? GetIdSubZonaByUbicacion(int idUbicacion)
        {
            int? subZona = (from u in _unitOfWork.Db.Ubicaciones_Distritos
                         where u.id_ubicacion == idUbicacion
                         select u.IdSubZona).FirstOrDefault();

            return subZona;
        }
    }
}
