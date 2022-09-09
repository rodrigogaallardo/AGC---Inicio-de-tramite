using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using IBaseRepository;
using Dal.UnitOfWork;
using StaticClass;

namespace BaseRepository
{
    public class TiposDeDocumentosRequeridosRepository : BaseRepository<TiposDeDocumentosRequeridos>
    { 
        private readonly IUnitOfWork _unitOfWork;

        public TiposDeDocumentosRequeridosRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }

        public IEnumerable<TiposDeDocumentosRequeridos> GetVisibleSSIT()
        {
            var domains = _unitOfWork.Db.TiposDeDocumentosRequeridos.Where(x => x.visible_en_SSIT == true);

            return domains;
        }

        public IEnumerable<TiposDeDocumentosRequeridos> GetByListIdTdoReq(List<int> LstIdTdocReq)
        {
            var domains = _unitOfWork.Db.TiposDeDocumentosRequeridos.Where(x => LstIdTdocReq.Contains(x.id_tdocreq));
            return domains;
        }


        public IEnumerable<TiposDeDocumentosRequeridos> GetVisibleAnexoTecnico()
        {
            var domains = _unitOfWork.Db.TiposDeDocumentosRequeridos.Where(x => x.visible_en_AT);

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TiposDeDocumentosRequeridos> GetVisibleSSITXTipoTramite(int IdTipoTramite)
        {
            var domains = (from tdodocreq in  _unitOfWork.Db.TiposDeDocumentosRequeridos
                              join rel in _unitOfWork.Db.Rel_TipoTramite_TiposDeDocumentosRequeridos on tdodocreq.id_tdocreq equals rel.id_tdocreq
                                where tdodocreq.visible_en_SSIT 
                                && rel.id_tipotramite == IdTipoTramite
                              select tdodocreq
                              );
            return domains;        
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTipoTramite"></param>
        /// <returns></returns>
        public IEnumerable<TiposDeDocumentosRequeridos> GetVisibleConsultaPadron(int IdTipoTramite)
        {
            var domains = (from tdodocreq in _unitOfWork.Db.TiposDeDocumentosRequeridos
                           join rel in _unitOfWork.Db.Rel_TipoTramite_TiposDeDocumentosRequeridos on tdodocreq.id_tdocreq equals rel.id_tdocreq
                           where 
                           rel.id_tipotramite == IdTipoTramite && (
                           tdodocreq.nombre_tdocreq.ToLower().Equals(Constantes.PLANCHETA)
                           || 
                           tdodocreq.nombre_tdocreq.ToLower().Equals(Constantes.OTROS)
                           )
                           select tdodocreq
                              );
            return domains;
        }
        public IEnumerable<TiposDeDocumentosRequeridos> GetByFKIdTipoNormativa(int IdTipoNormativa)
        {
            IEnumerable<TiposDeDocumentosRequeridos> domains = (from td in _unitOfWork.Db.TiposDeDocumentosRequeridos
                                                  join rel in _unitOfWork.Db.Rel_TiposDeDocumentosRequeridos_TipoNormativa on td.id_tdocreq equals rel.id_tdocreq
                                                  where rel.id_tnor == IdTipoNormativa
                                                  select td);
            return domains;
        }
    }
}
