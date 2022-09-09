using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class EncomiendaCertificadoSobrecargaRepository : BaseRepository<Encomienda_Certificado_Sobrecarga>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EncomiendaCertificadoSobrecargaRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomiendaDatosLocal"></param>
        /// <returns></returns>	
        public Encomienda_Certificado_Sobrecarga GetByFKIdEncomiendaDatosLocal(int IdEncomiendaDatosLocal)
        {
            Encomienda_Certificado_Sobrecarga domains = _unitOfWork.Db.Encomienda_Certificado_Sobrecarga.Where(x =>
                                                        x.id_encomienda_datoslocal == IdEncomiendaDatosLocal
                                                        ).FirstOrDefault();
            return domains;
        }
    }
}

