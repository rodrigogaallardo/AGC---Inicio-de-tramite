using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class EncomiendaFirmantesPersonasFisicasRepository : BaseRepository<Encomienda_Firmantes_PersonasFisicas>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EncomiendaFirmantesPersonasFisicasRepository(IUnitOfWork unit)
            : base(unit)
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
        public IEnumerable<Encomienda_Firmantes_PersonasFisicas> GetByFKIdEncomienda(int IdEncomienda)
        {
            IEnumerable<Encomienda_Firmantes_PersonasFisicas> domains = _unitOfWork.Db.Encomienda_Firmantes_PersonasFisicas.Where(x =>
                                                        x.id_encomienda == IdEncomienda
                                                        );

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdPersonaFisica"></param>
        /// <returns></returns>	
        public IEnumerable<Encomienda_Firmantes_PersonasFisicas> GetByFKIdPersonaFisica(int IdPersonaFisica)
        {
            IEnumerable<Encomienda_Firmantes_PersonasFisicas> domains = _unitOfWork.Db.Encomienda_Firmantes_PersonasFisicas.Where(x =>
                                                        x.id_personafisica == IdPersonaFisica
                                                        );

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTipodocPersonal"></param>
        /// <returns></returns>	
        public IEnumerable<Encomienda_Firmantes_PersonasFisicas> GetByFKIdTipodocPersonal(int IdTipodocPersonal)
        {
            IEnumerable<Encomienda_Firmantes_PersonasFisicas> domains = _unitOfWork.Db.Encomienda_Firmantes_PersonasFisicas.Where(x =>
                                                        x.id_tipodoc_personal == IdTipodocPersonal
                                                        );

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTipoCaracter"></param>
        /// <returns></returns>	
        public IEnumerable<Encomienda_Firmantes_PersonasFisicas> GetByFKIdTipoCaracter(int IdTipoCaracter)
        {
            IEnumerable<Encomienda_Firmantes_PersonasFisicas> domains = _unitOfWork.Db.Encomienda_Firmantes_PersonasFisicas.Where(x =>
                                                        x.id_tipocaracter == IdTipoCaracter
                                                        );

            return domains;
        }

        public IEnumerable<Encomienda_Firmantes_PersonasFisicas> GetByIdEncomiendaIdPersonaFisica(int id_encomienda, int IdPersonaFisica)
        {
            IEnumerable<Encomienda_Firmantes_PersonasFisicas> domains = _unitOfWork.Db.Encomienda_Firmantes_PersonasFisicas.Where(x =>
                                                                                          x.id_encomienda == id_encomienda
                                                                                          && x.id_personafisica == IdPersonaFisica
                                                                                          );
            return domains;
        }

        //public Encomienda_Firmantes_PersonasFisicas existeFirmantePF(int id_encomienda, int IdPersonaFisica)
        //{
        //    Encomienda_Firmantes_PersonasFisicas domains = _unitOfWork.Db.Encomienda_Firmantes_PersonasFisicas.Where(x =>
        //                                                                                  x.id_encomienda == id_encomienda
        //                                                                                  && x.id_personafisica != IdPersonaFisica
        //                                                                                  ).FirstOrDefault();
        //    return domains;
        //}
    }
}

