using Dal.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using StaticClass;

namespace BaseRepository
{
    public class AntenasDocumentosAdjuntosRepository : BaseRepository<ANT_DocumentosAdjuntos>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AntenasDocumentosAdjuntosRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>
        public IEnumerable<ANT_DocumentosAdjuntos> Get(int IdEncomienda)
        {
            return             
            ( from doc in _unitOfWork.Db.ANT_DocumentosAdjuntos 
                join tds in _unitOfWork.Db.APRA_TiposDeDocumentosSistema on doc.id_tipodocsis equals tds.id_tipdocsis
                where  (tds.cod_tipodocsis.Equals(Constantes.ENCOMIENDA_REG_ANT)  || tds.cod_tipodocsis.Equals(Constantes.ENCOMIENDA_RNI_ANT)) 
                && doc.id_encomienda == IdEncomienda
                  select doc);
            
        }
    }
}
