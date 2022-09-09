using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class EncomiendaTitularesPersonasJuridicasRepository : BaseRepository<Encomienda_Titulares_PersonasJuridicas> 
    {
		private readonly IUnitOfWork _unitOfWork;

		public EncomiendaTitularesPersonasJuridicasRepository(IUnitOfWork unit) : base(unit)
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
		public IEnumerable<Encomienda_Titulares_PersonasJuridicas> GetByFKIdEncomienda(int IdEncomienda)
		{
			IEnumerable<Encomienda_Titulares_PersonasJuridicas> domains = _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas.Where(x => 													
														x.id_encomienda == IdEncomienda											
														);
	
			return domains;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoSociedad"></param>
		/// <returns></returns>	
		public IEnumerable<Encomienda_Titulares_PersonasJuridicas> GetByFKIdTipoSociedad(int IdTipoSociedad)
		{
			IEnumerable<Encomienda_Titulares_PersonasJuridicas> domains = _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas.Where(x => 													
														x.Id_TipoSociedad == IdTipoSociedad											
														);
	
			return domains;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdTipoIb"></param>
		/// <returns></returns>	
		public IEnumerable<Encomienda_Titulares_PersonasJuridicas> GetByFKIdTipoIb(int IdTipoIb)
		{
			IEnumerable<Encomienda_Titulares_PersonasJuridicas> domains = _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas.Where(x => 													
														x.id_tipoiibb == IdTipoIb											
														);
	
			return domains;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="IdLocalidad"></param>
		/// <returns></returns>	
		public IEnumerable<Encomienda_Titulares_PersonasJuridicas> GetByFKIdLocalidad(int IdLocalidad)
		{
			IEnumerable<Encomienda_Titulares_PersonasJuridicas> domains = _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas.Where(x => 													
														x.id_localidad == IdLocalidad											
														);
	
			return domains;
		}

        public IEnumerable<Encomienda_Titulares_PersonasJuridicas> GetByIdEncomiendaCuitIdPersonaJuridica(int id_encomienda, string Cuit, int IdPersonaJuridica)
        {
            var lstTitularPersonaJur = (from tpf in _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas
                                           where tpf.id_encomienda == id_encomienda
                                           && tpf.id_personajuridica == IdPersonaJuridica
                                           && tpf.CUIT == Cuit
                                           select tpf);

            return lstTitularPersonaJur;
        }

        public IEnumerable<Encomienda_Titulares_PersonasJuridicas> existeTitularPJ(int id_encomienda, string Cuit, int IdPersonaJuridica)
        {
            var lstTitularPersonaJur = (from tpf in _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas
                                           where tpf.id_encomienda == id_encomienda
                                           && tpf.id_personajuridica != IdPersonaJuridica
                                           && tpf.CUIT == Cuit
                                           select tpf);

            return lstTitularPersonaJur;
        }
        public IEnumerable<Encomienda_Titulares_PersonasJuridicas> existeTitularPJ(int id_encomienda, int IdPersonaJuridica)
        {
            var lstTitularPersonaJur = (from tpf in _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas
                                        where tpf.id_encomienda == id_encomienda
                                        && tpf.id_personajuridica != IdPersonaJuridica
                                        select tpf);

            return lstTitularPersonaJur;
        }

        public IEnumerable<Encomienda_Titulares_PersonasJuridicas> GetByIdEncomiendaIdPersonaJuridica(int id_encomienda, int IdPersonaJuridica)
        {
            IEnumerable<Encomienda_Titulares_PersonasJuridicas> domains = _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas.Where(x =>
                                                                            x.id_encomienda == id_encomienda
                                                                            && x.id_personajuridica == IdPersonaJuridica
                                                                            );
            return domains;
        }

        /// <summary>
        /// Compara Encomienda_Solicitudes_Titulares_PersonasJuridicass con Encomienda_Solicitudes_Titulares_PersonasJuridicass
        /// </summary>
        /// <param name="idEncomienda1"></param>
        /// <param name="idEncomienda2"></param>
        /// <returns>True Si son iguales , False si son distintos</returns>
        public bool CompareDataSaved(int idEncomienda1, int idEncomienda2)
        {
            try
            {
                if (!compareCantRegPersonasJuridicas(idEncomienda1, idEncomienda2))
                {
                    return false;
                }
                var encominedaSolicitudPersonaJuridicas1 = _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas.Where(x => x.id_encomienda == idEncomienda1).ToList();
                var encominedaSolicitudPersonaJuridicas2 = _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas.Where(x => x.id_encomienda == idEncomienda2).ToList();

                foreach (Encomienda_Titulares_PersonasJuridicas pf in encominedaSolicitudPersonaJuridicas1)
                {
                    #region Validacion PersonaFisica
                    var encomiendaPrsonaJuridica = encominedaSolicitudPersonaJuridicas2.Where(
                        x => x.Razon_Social == pf.Razon_Social &&
                            x.CUIT == pf.CUIT &&
                            x.id_tipoiibb == pf.id_tipoiibb &&
                            x.Id_TipoSociedad == pf.Id_TipoSociedad &&
                            x.Nro_IIBB == pf.Nro_IIBB &&
                            x.Calle == pf.Calle &&
                             x.NroPuerta == pf.NroPuerta &&
                            x.Piso == pf.Piso &&
                            x.Depto == pf.Depto &&
                            x.id_localidad == pf.id_localidad &&
                            x.Codigo_Postal == pf.Codigo_Postal &&
                           x.Email == pf.Email &&
                            x.Telefono == pf.Telefono
                    ).FirstOrDefault();

                    if (encomiendaPrsonaJuridica == null)
                        return false;

                    //busca los firmantes dentro de la coleecion de firmantes en el ssit
                    foreach (Encomienda_Firmantes_PersonasJuridicas EncFirmateJuridica in pf.Encomienda_Firmantes_PersonasJuridicas)
                    {
                        var encomiendaJuridicas = encomiendaPrsonaJuridica.Encomienda_Firmantes_PersonasJuridicas.Where(
                            x => x.Apellido == EncFirmateJuridica.Apellido
                            && x.Email == EncFirmateJuridica.Email
                            && x.Nombres == EncFirmateJuridica.Nombres
                            && x.Nro_Documento == EncFirmateJuridica.Nro_Documento
                            && x.id_tipodoc_personal == EncFirmateJuridica.id_tipodoc_personal
                            && x.id_tipocaracter == EncFirmateJuridica.id_tipocaracter
                            && x.cargo_firmante_pj == EncFirmateJuridica.cargo_firmante_pj
                            ).SingleOrDefault();

                        if (encomiendaJuridicas == null)
                            return false;
                    }

                    //busca los titulares personas dentro de la coleecion (SH)
                    foreach (Encomienda_Titulares_PersonasJuridicas_PersonasFisicas EncJuridica in pf.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas)
                    {
                        var encomiendaJuridicas = encomiendaPrsonaJuridica.Encomienda_Titulares_PersonasJuridicas_PersonasFisicas.Where(
                            x => x.Apellido == EncJuridica.Apellido
                            && x.Email == EncJuridica.Email
                            && x.Nombres == EncJuridica.Nombres
                            && x.Nro_Documento == EncJuridica.Nro_Documento
                            && x.id_tipodoc_personal == EncJuridica.id_tipodoc_personal
                            && x.firmante_misma_persona == EncJuridica.firmante_misma_persona
                            ).SingleOrDefault();

                        if (encomiendaJuridicas == null)
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
        public bool compareCantRegPersonasJuridicas(int idEncomienda1, int idEncomienda2)
        {
            try
            {
                var coutRecordEncomiendaPersonasJuridicas1 = _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas.Where(x => x.id_encomienda == idEncomienda1).Count();
                var coutRecordEncomiendaPersonasJuridicas2 = _unitOfWork.Db.Encomienda_Titulares_PersonasJuridicas.Where(x => x.id_encomienda == idEncomienda2).Count();
                if (coutRecordEncomiendaPersonasJuridicas1 != coutRecordEncomiendaPersonasJuridicas2)
                {
                    return false;
                }

                var coutRecordEncomiendaFirmantesPersonasJuridicas1 = _unitOfWork.Db.Encomienda_Firmantes_PersonasJuridicas.Where(x => x.id_encomienda == idEncomienda1).Count();
                var coutRecordEncomiendaFirmantesPersonasJuridicas2 = _unitOfWork.Db.Encomienda_Firmantes_PersonasJuridicas.Where(x => x.id_encomienda == idEncomienda2).Count();
                if (coutRecordEncomiendaFirmantesPersonasJuridicas1 != coutRecordEncomiendaFirmantesPersonasJuridicas2)
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

