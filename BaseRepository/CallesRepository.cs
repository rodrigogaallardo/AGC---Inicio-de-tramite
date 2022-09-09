using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using IBaseRepository;
using Dal.UnitOfWork;


namespace BaseRepository
{
    public class CallesRepository : BaseRepository<Calles>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CallesRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }
        /// <summary>
        /// DEVOLVER UN ENTITY
        /// </summary>
        /// <returns></returns>
        //public IEnumerable<Calles> GetCalles()
        //{
        //    var q = (from calle in _unitOfWork.Db.Calles
        //             select calle).Distinct().OrderBy(x => x.NombreOficial_calle);


        //    //var q = from c in _unitOfWork.Db.Calles
        //    //        group c by new
        //    //        {
        //    //            c.Codigo_calle,
        //    //            c.NombreOficial_calle
        //    //        } into g
        //    //        select new Calles
        //    //        {
        //    //            Codigo_calle = g.Key.Codigo_calle,
        //    //            NombreOficial_calle=  g.Key.NombreOficial_calle

        //    //        };

        //    return q;
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public IEnumerable<Calles> Get(int Codigo)
        {
            var q = (from calle in _unitOfWork.Db.Calles
                     where calle.Codigo_calle == Codigo
                     select calle).OrderBy(x => x.NombreOficial_calle); 

            return q;
        }
    }

}

