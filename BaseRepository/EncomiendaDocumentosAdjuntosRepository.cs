using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class EncomiendaDocumentosAdjuntosRepository : BaseRepository<Encomienda_DocumentosAdjuntos> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public EncomiendaDocumentosAdjuntosRepository(IUnitOfWork unit) : base(unit)
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
        public IEnumerable<Encomienda_DocumentosAdjuntos> GetByFKIdEncomiendaTipoSis(int IdEncomienda, int idTipo)
		{
            IEnumerable<Encomienda_DocumentosAdjuntos> domains = (from ed in _unitOfWork.Db.Encomienda_DocumentosAdjuntos
                                                                    where ed.id_encomienda == IdEncomienda && ed.id_tipodocsis == idTipo
                                                                       select ed);
	
			return domains;
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public IEnumerable<Encomienda_DocumentosAdjuntos> GetByFKIdEncomiendaTipoSis(int IdEncomienda, string Codigo)
        {
            IEnumerable<Encomienda_DocumentosAdjuntos> domains = (from ed in _unitOfWork.Db.Encomienda_DocumentosAdjuntos
                                                                       join tipo in _unitOfWork.Db.TiposDeDocumentosSistema on ed.id_tipodocsis equals tipo.id_tipdocsis
                                                                       where ed.id_encomienda == IdEncomienda && tipo.cod_tipodocsis.Equals(Codigo)
                                                                       select ed);

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdsEncomienda"></param>
        /// <param name="idTipo"></param>
        /// <returns></returns>
        public IEnumerable<Encomienda_DocumentosAdjuntos> GetByFKListIdEncomiendaTipoSis(List<int> IdsEncomienda, int idTipo)
        {
            IEnumerable<Encomienda_DocumentosAdjuntos> domains = (from ed in _unitOfWork.Db.Encomienda_DocumentosAdjuntos
                                                                    where IdsEncomienda.Contains(ed.id_encomienda) 
                                                                    && ed.id_tipodocsis == idTipo
                                                                    select ed
                                                                    );

            return domains;
        }
        public IEnumerable<Encomienda_DocumentosAdjuntos> GetByFKIdEncomienda(int IdEncomienda)
        {
            IEnumerable<Encomienda_DocumentosAdjuntos> domains = (from ed in _unitOfWork.Db.Encomienda_DocumentosAdjuntos
                                                                       where ed.id_encomienda == IdEncomienda
                                                                       select ed);

            return domains;
        }
    }
}

