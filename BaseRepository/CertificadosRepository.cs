using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class CertificadosRepository : BaseRepository<vis_Certificados> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public CertificadosRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null) 
				throw new ArgumentNullException("unitOfWork Exception");
			
			_unitOfWork = unit;
        }

        public IEnumerable<vis_Certificados> GetByFKNroTipo(string NroTramite, int TipoTramite)
        {
            IEnumerable<vis_Certificados> domains = _unitOfWork.Db.vis_Certificados.Where(x => x.NroTramite == NroTramite && x.TipoTramite == TipoTramite);
            return domains;
        }

        public IEnumerable<vis_Certificados> GetByFKListNroTipo(List<String> list, int TipoTramite)
        {
            IEnumerable<vis_Certificados> domains = _unitOfWork.Db.vis_Certificados.Where(x => list.Contains(x.NroTramite) && x.TipoTramite == TipoTramite);
            return domains;
        }

        public vis_Certificados GetById(int id_certificado)
        {
            return _unitOfWork.Db.vis_Certificados.Where(x => x.id_certificado == id_certificado).FirstOrDefault();
        }
    }
}

