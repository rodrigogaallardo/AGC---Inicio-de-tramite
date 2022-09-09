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
    /// <summary>
    /// Representa a la entidad rubros del Schema DTO
    /// </summary>
    public class RubrosTiposDeDocReqRepository : BaseRepository<Rubros_TiposDeDocumentosRequeridos>
    {

        private readonly IUnitOfWork _unitOfWork;

        public RubrosTiposDeDocReqRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }

        /// <summary>
        /// devuelve los documentos requeridos para un numero de rubro 
        /// </summary>
        /// <param name="idRubro"></param>
        /// <returns></returns>
        public IEnumerable<Rubros_TiposDeDocumentosRequeridos> GetTiposDocumentosRequeridosByIDRubro(int idRubro)
        {
            var lstDocReq = _unitOfWork.Db.Rubros_TiposDeDocumentosRequeridos.Where(x => x.id_rubro == idRubro);
            return lstDocReq;
        }

        public IEnumerable<Rubros_TiposDeDocumentosRequeridos> GetTipoDocumentosRequeridosByCodigoRubro(string CodigoRubro)
        {
            var res = (from rtdr in _unitOfWork.Db.Rubros_TiposDeDocumentosRequeridos
                       join r in _unitOfWork.Db.RubrosCN on rtdr.id_rubro equals r.IdRubro
                       where r.Codigo == CodigoRubro
                       select rtdr
                      );
            return res;
        }
    }
}

