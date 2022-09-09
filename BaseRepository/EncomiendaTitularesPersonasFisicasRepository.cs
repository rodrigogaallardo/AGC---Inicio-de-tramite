using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class EncomiendaTitularesPersonasFisicasRepository : BaseRepository<Encomienda_Titulares_PersonasFisicas>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EncomiendaTitularesPersonasFisicasRepository(IUnitOfWork unit) : base(unit)
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
        public IEnumerable<Encomienda_Titulares_PersonasFisicas> GetByFKIdEncomienda(int IdEncomienda)
        {
            IEnumerable<Encomienda_Titulares_PersonasFisicas> domains = _unitOfWork.Db.Encomienda_Titulares_PersonasFisicas.Where(x =>
                                                        x.id_encomienda == IdEncomienda
                                                        );

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTipoDocPersonal"></param>
        /// <returns></returns>	
        public IEnumerable<Encomienda_Titulares_PersonasFisicas> GetByFKIdTipoDocPersonal(int IdTipoDocPersonal)
        {
            IEnumerable<Encomienda_Titulares_PersonasFisicas> domains = _unitOfWork.Db.Encomienda_Titulares_PersonasFisicas.Where(x =>
                                                        x.id_tipodoc_personal == IdTipoDocPersonal
                                                        );

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdTipoiibb"></param>
        /// <returns></returns>	
        public IEnumerable<Encomienda_Titulares_PersonasFisicas> GetByFKIdTipoiibb(int IdTipoiibb)
        {
            IEnumerable<Encomienda_Titulares_PersonasFisicas> domains = _unitOfWork.Db.Encomienda_Titulares_PersonasFisicas.Where(x =>
                                                        x.id_tipoiibb == IdTipoiibb
                                                        );

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdLocalidad"></param>
        /// <returns></returns>	
        public IEnumerable<Encomienda_Titulares_PersonasFisicas> GetByFKIdLocalidad(int IdLocalidad)
        {
            IEnumerable<Encomienda_Titulares_PersonasFisicas> domains = _unitOfWork.Db.Encomienda_Titulares_PersonasFisicas.Where(x =>
                                                        x.Id_Localidad == IdLocalidad
                                                        );

            return domains;
        }

        public IEnumerable<Encomienda_Titulares_PersonasFisicas> GetByIdEncomiendaCuitIdPersonaFisica(int id_encomienda, string Cuit, int IdPersonaFisica)
        {
            IEnumerable<Encomienda_Titulares_PersonasFisicas> domains = (from tpf in _unitOfWork.Db.Encomienda_Titulares_PersonasFisicas
                                                                         where tpf.id_encomienda == id_encomienda
                                                                         && tpf.id_personafisica == IdPersonaFisica
                                                                         && tpf.Cuit == Cuit
                                                                         select tpf);
            return domains;
        }
               

        public IEnumerable<Encomienda_Titulares_PersonasFisicas> GetByIdEncomiendaIdPersonaFisica(int id_encomienda, int IdPersonaFisica)
        {
            IEnumerable<Encomienda_Titulares_PersonasFisicas> lstTitularPersonafisica = (from tpf in _unitOfWork.Db.Encomienda_Titulares_PersonasFisicas
                                                                                         where tpf.id_encomienda == id_encomienda
                                                                                         && tpf.id_personafisica == IdPersonaFisica
                                                                                         select tpf);

            return lstTitularPersonafisica;
        }

        /// <summary>
        /// Compara Encomienda_Solicitudes_Titulares_PersonasFisicas con Encomienda_Solicitudes_Titulares_PersonasFisicas
        /// </summary>
        /// <param name="idEncomienda1"></param>
        /// <param name="idEncomienda2"></param>
        /// <returns>True Si son iguales , False si son distintos</returns>
        public bool CompareDataSaved(int idEncomienda1, int idEncomienda2)
        {
            try
            {
                if (!compareCantRegPersonasFisicas(idEncomienda1, idEncomienda2))
                {
                    return false;
                }
                var encominedaSolicitudPersonaFisica1 = _unitOfWork.Db.Encomienda_Titulares_PersonasFisicas.Where(x => x.id_encomienda == idEncomienda1).ToList();

                var encominedaSolicitudPersonaFisica2 = _unitOfWork.Db.Encomienda_Titulares_PersonasFisicas.Where(x => x.id_encomienda == idEncomienda2).ToList();

                foreach (Encomienda_Titulares_PersonasFisicas pf in encominedaSolicitudPersonaFisica1)
                {
                    #region Validacion PersonaFisica
                    var encomiendaPrsonaFisica = encominedaSolicitudPersonaFisica2.Where(
                        x => x.Apellido == pf.Apellido &&
                            x.Nombres == pf.Nombres &&
                            x.id_tipodoc_personal == pf.id_tipodoc_personal &&
                            x.Nro_Documento == pf.Nro_Documento &&
                            x.Cuit == pf.Cuit &&
                            x.id_tipoiibb == pf.id_tipoiibb &&
                            x.Ingresos_Brutos == pf.Ingresos_Brutos &&
                            x.Calle == pf.Calle &&
                            x.Nro_Puerta == pf.Nro_Puerta &&
                            x.Piso == pf.Piso &&
                            x.Depto == pf.Depto &&
                            x.Id_Localidad == pf.Id_Localidad &&
                            x.Codigo_Postal == pf.Codigo_Postal &&
                            x.Email == pf.Email &&
                            x.Sms == pf.Sms &&
                            x.Telefono == pf.Telefono
                    ).FirstOrDefault();

                    if (encomiendaPrsonaFisica == null)
                        return false;

                    //busca los firmantes dentro de la coleecion de firmantes en el ssit
                    foreach (Encomienda_Firmantes_PersonasFisicas EncFirmateFisica in pf.Encomienda_Firmantes_PersonasFisicas)
                    {
                        var encomiendaFirmantes = encomiendaPrsonaFisica.Encomienda_Firmantes_PersonasFisicas.Where(
                            x => x.Apellido == EncFirmateFisica.Apellido
                            && x.Email == EncFirmateFisica.Email
                            && x.Nombres == EncFirmateFisica.Nombres
                            && x.Nro_Documento == EncFirmateFisica.Nro_Documento
                            && x.TipoDocumentoPersonal == EncFirmateFisica.TipoDocumentoPersonal
                            && x.TiposDeCaracterLegal == EncFirmateFisica.TiposDeCaracterLegal
                            ).SingleOrDefault();

                        if (encomiendaFirmantes == null)
                            return false;
                    }
                    #endregion Validacion PersonaFisica
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// valida que las cantidaddes de registros sean las mismas para SSIT y encomiendas
        /// </summary>
        /// <param name="idEncomienda1"></param>
        /// <param name="idEncomienda2"></param>
        /// <returns></returns>
        public bool compareCantRegPersonasFisicas(int idEncomienda1, int idEncomienda2)
        {
            try
            {
                var coutRecordEncomiendaPersonasFisicas1 = _unitOfWork.Db.Encomienda_Titulares_PersonasFisicas.Where(x => x.id_encomienda == idEncomienda1).Count();
                var coutRecordEncomiendaPersonasFisicas2 = _unitOfWork.Db.Encomienda_Titulares_PersonasFisicas.Where(x => x.id_encomienda == idEncomienda2).Count();
                if (coutRecordEncomiendaPersonasFisicas1 != coutRecordEncomiendaPersonasFisicas2)
                {
                    return false;
                }

                var coutRecordEncomiendaFirmantesPersonasFisicas1 = _unitOfWork.Db.Encomienda_Firmantes_PersonasFisicas.Where(x => x.id_encomienda == idEncomienda1).Count();
                var coutRecordEncomiendaFirmantesPersonasFisicas2 = _unitOfWork.Db.Encomienda_Firmantes_PersonasFisicas.Where(x => x.id_encomienda == idEncomienda2).Count();
                if (coutRecordEncomiendaFirmantesPersonasFisicas1 != coutRecordEncomiendaFirmantesPersonasFisicas2)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}


