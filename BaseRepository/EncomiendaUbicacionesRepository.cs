using Dal.UnitOfWork;
using DataAcess;
using DataAcess.EntityCustom;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BaseRepository
{
    public class EncomiendaUbicacionesRepository : BaseRepository<Encomienda_Ubicaciones>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EncomiendaUbicacionesRepository(IUnitOfWork unit)
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
        public IEnumerable<Encomienda_Ubicaciones> GetByFKIdEncomienda(int IdEncomienda)
        {
            IEnumerable<Encomienda_Ubicaciones> domains = _unitOfWork.Db.Encomienda_Ubicaciones.Where(x =>
                                                        x.id_encomienda == IdEncomienda
                                                        );

            return domains;
        }

        public IEnumerable<Zonas_Planeamiento> esZonaResidencial(List<int> lstZonaPlaneamiento)
        {
            IEnumerable<Zonas_Planeamiento> domains = (from zh in _unitOfWork.Db.Zonas_Habilitaciones
                                                       join rhp in _unitOfWork.Db.Rel_ZonasPlaneamiento_ZonasHabilitaciones on zh.CodZonaHab equals rhp.CodZonaHab
                                                       join zp in _unitOfWork.Db.Zonas_Planeamiento on rhp.CodZonaLey449 equals zp.CodZonaPla
                                                       where new List<string>() { "R1A", "R1B1", "R1BII", "R2A", "R2B", "R2BIII" }.Contains(zh.CodZonaHab)
                                                       && lstZonaPlaneamiento.Contains(zp.id_zonaplaneamiento)
                                                       select zp);
            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdUbicacion"></param>
        /// <returns></returns>	
        public IEnumerable<Encomienda_Ubicaciones> GetByFKIdUbicacion(int IdUbicacion)
        {
            IEnumerable<Encomienda_Ubicaciones> domains = _unitOfWork.Db.Encomienda_Ubicaciones.Where(x =>
                                                        x.id_ubicacion == IdUbicacion
                                                        );

            return domains;
        }
        /// <summary>
        /// Compara Encomienda_Ubicaciones con Encomienda_Ubicaciones
        /// </summary>
        /// <param name="idEncomienda1"></param>
        /// <param name="idEncomienda2"></param>
        /// <returns>True Si son iguales , False si son distintos</returns>
        public bool CompareDataSaved(int idEncomienda1, int idEncomienda2)
        {
            try
            {
                if (!CompareCant(idEncomienda1, idEncomienda2))
                    return false;

                var encominedaUbicaciones1 = _unitOfWork.Db.Encomienda_Ubicaciones.Where(x => x.id_encomienda == idEncomienda1).ToList();
                var encominedaUbicaciones2 = _unitOfWork.Db.Encomienda_Ubicaciones.Where(x => x.id_encomienda == idEncomienda2).ToList();

                foreach (Encomienda_Ubicaciones ubi in encominedaUbicaciones1)
                {
                    var encomiendaUbicacion = encominedaUbicaciones2.Where(x =>
                            (x.deptoLocal_encomiendaubicacion ?? string.Empty).Trim().ToUpper() == (ubi.deptoLocal_encomiendaubicacion ?? string.Empty).Trim().ToUpper()
                            && (x.Depto ?? string.Empty).Trim().ToUpper() == (ubi.Depto ?? string.Empty).Trim().ToUpper()
                            && (x.Local ?? string.Empty).Trim().ToUpper() == (ubi.Local ?? string.Empty).Trim().ToUpper()
                            && (x.Torre ?? string.Empty).Trim().ToUpper() == (ubi.Torre ?? string.Empty).Trim().ToUpper()
                            && x.id_ubicacion == ubi.id_ubicacion
                            && x.id_zonaplaneamiento == ubi.id_zonaplaneamiento
                    ).FirstOrDefault();

                    if (encomiendaUbicacion == null)
                        return false;

                    //busca las partidas horizontales
                    int countPartidasHorizontales1 = ubi.Encomienda_Ubicaciones_PropiedadHorizontal.Count();
                    int countPartidasHorizontales2 = encomiendaUbicacion.Encomienda_Ubicaciones_PropiedadHorizontal.Count();
                    if (countPartidasHorizontales1 != countPartidasHorizontales2)
                        return false;

                    foreach (Encomienda_Ubicaciones_PropiedadHorizontal ph in ubi.Encomienda_Ubicaciones_PropiedadHorizontal)
                    {
                        var partHor = encomiendaUbicacion.Encomienda_Ubicaciones_PropiedadHorizontal.Where(
                            x => x.id_propiedadhorizontal == ph.id_propiedadhorizontal).FirstOrDefault();

                        if (partHor == null)
                            return false;
                    }

                    //busca las puertas
                    int countPuertas1 = ubi.Encomienda_Ubicaciones_Puertas.Count();
                    int countPuertas2 = encomiendaUbicacion.Encomienda_Ubicaciones_Puertas.Count();
                    if (countPuertas1 != countPuertas2)
                        return false;

                    foreach (Encomienda_Ubicaciones_Puertas pu in ubi.Encomienda_Ubicaciones_Puertas)
                    {
                        var puerta = encomiendaUbicacion.Encomienda_Ubicaciones_Puertas.Where(
                            x => x.codigo_calle == pu.codigo_calle
                            && x.NroPuerta == pu.NroPuerta).FirstOrDefault();

                        if (puerta == null)
                            return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// valida que las cantidaddes de registros sean las mismas para encomiendas
        /// </summary>
        /// <param name="idEncomienda1"></param>
        /// <param name="idEncomienda2"></param>
        /// <returns></returns>
        public bool CompareCant(int idEncomienda1, int idEncomienda2)
        {
            try
            {
                var coutRecordEncomiendaUbicacion1 = _unitOfWork.Db.Encomienda_Ubicaciones.Where(x => x.id_encomienda == idEncomienda1).Count();
                var coutRecordEncomiendaUbicacion2 = _unitOfWork.Db.Encomienda_Ubicaciones.Where(x => x.id_encomienda == idEncomienda2).Count();
                if (coutRecordEncomiendaUbicacion1 != coutRecordEncomiendaUbicacion2)
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

        public string GetMixDistritoZonaySubZonaByEncomienda(int idEncomienda)
        {
            var parameter = new System.Data.Entity.Core.Objects.ObjectParameter("result", "varchar(1000)");
            _unitOfWork.Db.GetMixDistritoZonaySubZonaByEncomienda(idEncomienda, parameter);
            return parameter.Value.ToString();
        }

        public string GetMixDistritoZonaySubZonaByEncomiendaUbicacion(int idEncomiendaUbicacion)
        {
            var mix = (from eud in _unitOfWork.Db.Encomienda_Ubicaciones_Mixturas
                              join umix in _unitOfWork.Db.Ubicaciones_ZonasMixtura on eud.IdZonaMixtura equals umix.IdZonaMixtura
                              where eud.id_encomiendaubicacion == idEncomiendaUbicacion
                              select umix.Codigo).ToList();


            var dis = (from eud in _unitOfWork.Db.Encomienda_Ubicaciones_Distritos
                                      join ucdz in _unitOfWork.Db.Ubicaciones_CatalogoDistritos_Zonas on (int)eud.IdZona equals ucdz.IdZona into lzona
                                      from lz in lzona.DefaultIfEmpty()
                                      join ucdsz in _unitOfWork.Db.Ubicaciones_CatalogoDistritos_Subzonas on (int)eud.IdSubZona equals ucdsz.IdSubZona into lszona
                                      from lsz in lszona.DefaultIfEmpty()
                                      where eud.id_encomiendaubicacion == idEncomiendaUbicacion
                                      select eud.Ubicaciones_CatalogoDistritos.Codigo +" "+ lz.CodigoZona + " " + lsz.CodigoSubZona).ToList();

            string Mixtura = String.Join(" - ", mix.ToArray());

            string DistZonaSubZona = String.Join(" - ", dis.ToArray());

            if (!String.IsNullOrEmpty(Mixtura) && !String.IsNullOrEmpty(DistZonaSubZona))
            {
                return Mixtura + " / " + DistZonaSubZona;
            }
            else if (!String.IsNullOrEmpty(Mixtura))
            {
                return Mixtura;
            }
            return DistZonaSubZona;
        }
    }
}

