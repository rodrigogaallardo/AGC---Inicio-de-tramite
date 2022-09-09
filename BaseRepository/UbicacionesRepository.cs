using Dal.UnitOfWork;
using DataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class UbicacionesRepository : BaseRepository<Ubicaciones>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UbicacionesRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="NroPartida"></param>
        /// <param name="FechaActual"></param>
        /// <returns></returns>
        public IEnumerable<Ubicaciones> Get(int NroPartida, DateTime FechaActual)
        {
            var query = (from ubic in _unitOfWork.Db.Ubicaciones
                         where ubic.NroPartidaMatriz == NroPartida
              && (!ubic.VigenciaHasta.HasValue || ubic.VigenciaHasta > FechaActual)
              && ubic.baja_logica == false
                         select ubic);
            return query;

        }
          

        public IEnumerable<Ubicaciones> GetXPartidaHorizontal(int NroPartida, DateTime FechaActual)
        {
            var query = (from ubic in _unitOfWork.Db.Ubicaciones
                         join phor in _unitOfWork.Db.Ubicaciones_PropiedadHorizontal on ubic.id_ubicacion equals phor.id_ubicacion
                         where phor.NroPartidaHorizontal == NroPartida
                         && (!ubic.VigenciaHasta.HasValue || ubic.VigenciaHasta > FechaActual)
                         && ubic.baja_logica == false
                          select ubic );

            return query;
        }     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="NroPuerta"></param>
        /// <param name="FechaActual"></param>
        /// <param name="CodigoCalle"></param>
        /// <returns></returns>
        public IEnumerable<Ubicaciones> GetXPuerta(int NroPuerta, DateTime FechaActual, int CodigoCalle)
        {
            var query = (from ubic in _unitOfWork.Db.Ubicaciones
                         join puer in _unitOfWork.Db.Ubicaciones_Puertas on ubic.id_ubicacion equals puer.id_ubicacion
                         where puer.codigo_calle == CodigoCalle && puer.NroPuerta_ubic == NroPuerta
                          && (!ubic.VigenciaHasta.HasValue || ubic.VigenciaHasta > FechaActual)
                          && ubic.baja_logica == false
                         select ubic);
            return query;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="minvaluePuerta"></param>
        /// <param name="maxvaluePuerta"></param>
        /// <param name="FechaActual"></param>
        /// <param name="CodigoCalle"></param>
        /// <param name="parimpar"></param>
        /// <returns></returns>
        public IEnumerable<Ubicaciones> Get(int minvaluePuerta, int maxvaluePuerta, DateTime FechaActual, int CodigoCalle, int parimpar)
        {
            var query = (from ubic in _unitOfWork.Db.Ubicaciones
                         join puer in _unitOfWork.Db.Ubicaciones_Puertas on ubic.id_ubicacion equals puer.id_ubicacion
                         where puer.codigo_calle.Equals(CodigoCalle) && (puer.NroPuerta_ubic >= minvaluePuerta && puer.NroPuerta_ubic <= maxvaluePuerta)
                        && puer.NroPuerta_ubic % 2 == parimpar
                        && (!ubic.VigenciaHasta.HasValue || ubic.VigenciaHasta > FechaActual)
                        && ubic.baja_logica == false
                         orderby puer.NroPuerta_ubic
                         select  ubic);

            return query;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FechaActual"></param>
        /// <param name="CodigoCalle"></param>
        /// <param name="Nro"></param>
        /// <returns></returns>
        public bool Exists(DateTime FechaActual, int CodigoCalle, int Nro)
        {
            var query = (from ubic in _unitOfWork.Db.Ubicaciones
                         join puer in _unitOfWork.Db.Ubicaciones_Puertas on ubic.id_ubicacion equals puer.id_ubicacion
                         where puer.codigo_calle.Equals(CodigoCalle) && (puer.NroPuerta_ubic == Nro)
                        && (!ubic.VigenciaHasta.HasValue || ubic.VigenciaHasta > FechaActual)
                        && ubic.baja_logica == false
                         select ubic).Any();
            return query;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="FechaActual"></param>
        /// <param name="CodigoCalle"></param>
        /// <param name="Nro"></param>
        /// <returns></returns>
        public IEnumerable<Ubicaciones> Get(DateTime FechaActual, int CodigoCalle, int Nro)
        {
            var query = (from ubic in _unitOfWork.Db.Ubicaciones
                         join puer in _unitOfWork.Db.Ubicaciones_Puertas on ubic.id_ubicacion equals puer.id_ubicacion
                         where puer.codigo_calle.Equals(CodigoCalle) && (puer.NroPuerta_ubic == Nro)
                        && (!ubic.VigenciaHasta.HasValue || ubic.VigenciaHasta > FechaActual)
                        && ubic.baja_logica == false
                         orderby puer.NroPuerta_ubic
                         select ubic); 

            return query;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Seccion"></param>
        /// <param name="Manzana"></param>
        /// <param name="Parcela"></param>
        /// <param name="FechaActual"></param>
        /// <returns></returns>
        public IEnumerable<Ubicaciones> Get(int Seccion, string Manzana, string Parcela, DateTime FechaActual)
        {
            var query3 = (from ubic in _unitOfWork.Db.Ubicaciones
                          where ubic.Seccion == Seccion && ubic.Manzana == Manzana && ubic.Parcela == Parcela
                          && (!ubic.VigenciaHasta.HasValue || ubic.VigenciaHasta > FechaActual)
                          && ubic.baja_logica == false
                          select ubic);

            return query3;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSubTipoUbicacion"></param>
        /// <param name="FechaActual"></param>
        /// <returns></returns>
        public IEnumerable<Ubicaciones> GetXTipo(int IdSubTipoUbicacion, DateTime FechaActual)
        {
            var query4 = (from ubic in _unitOfWork.Db.Ubicaciones
                          where ubic.id_subtipoubicacion == IdSubTipoUbicacion
                          && (!ubic.VigenciaHasta.HasValue || ubic.VigenciaHasta > FechaActual)
                          && ubic.baja_logica == false
                          select ubic
                          );

            return query4;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdUbicacion"></param>
        /// <returns></returns>
        public string GetCodigoZona(int IdUbicacion)
        {

            var query = (from ubic in _unitOfWork.Db.Ubicaciones
                         join zona in _unitOfWork.Db.Zonas_Planeamiento on ubic.id_zonaplaneamiento equals zona.id_zonaplaneamiento
                         join rel in _unitOfWork.Db.Rel_ZonasPlaneamiento_ZonasHabilitaciones on zona.CodZonaPla equals rel.CodZonaLey449
                         into codigo
                         from sd in codigo.Where(x => x.CodZonaLey449 == zona.CodZonaPla).DefaultIfEmpty()
                         where ubic.id_ubicacion == IdUbicacion
                         select sd == null ? zona.CodZonaPla : codigo.FirstOrDefault().CodZonaHab).FirstOrDefault();


            return query;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>
        public string GetCodigoZonaEncomienda(int IdEncomienda)
        {

            var query = (from encubic in _unitOfWork.Db.Encomienda_Ubicaciones
                         join enc in _unitOfWork.Db.Encomienda on encubic.id_encomienda equals enc.id_encomienda
                         join zona in _unitOfWork.Db.Zonas_Planeamiento on encubic.id_zonaplaneamiento equals zona.id_zonaplaneamiento
                         join rel in _unitOfWork.Db.Rel_ZonasPlaneamiento_ZonasHabilitaciones on zona.CodZonaPla equals rel.CodZonaLey449
                         into codigo
                         from sd in codigo.Where(x => x.CodZonaLey449 == zona.CodZonaPla).DefaultIfEmpty()
                         where enc.id_encomienda == IdEncomienda
                         select sd == null ? zona.CodZonaPla : codigo.FirstOrDefault().CodZonaHab).FirstOrDefault();

            return query;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdConsulta"></param>
        /// <returns></returns>
        public string GetCodigoZonaConsultaPadron(int IdConsulta)
        {

            var query = (from cpaubic in _unitOfWork.Db.CPadron_Ubicaciones
                         join cpa in _unitOfWork.Db.CPadron_Solicitudes on cpaubic.id_cpadron equals cpa.id_cpadron
                         join zona in _unitOfWork.Db.Zonas_Planeamiento on cpaubic.id_zonaplaneamiento equals zona.id_zonaplaneamiento
                         join rel in _unitOfWork.Db.Rel_ZonasPlaneamiento_ZonasHabilitaciones on zona.CodZonaPla equals rel.CodZonaLey449
                         into codigo
                         from sd in codigo.Where(x => x.CodZonaLey449 == zona.CodZonaPla).DefaultIfEmpty()
                         where cpa.id_cpadron == IdConsulta
                         select sd == null ? zona.CodZonaPla : codigo.FirstOrDefault().CodZonaHab).FirstOrDefault();

            return query;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSubtipoUbicacion"></param>
        /// <returns></returns>
        public IEnumerable<Ubicaciones> GetByFKIdSubtipoUbicacion(int IdSubtipoUbicacion)
        {
            IEnumerable<Ubicaciones> domains = _unitOfWork.Db.Ubicaciones.Where(x =>
                                                        x.id_subtipoubicacion == IdSubtipoUbicacion
                                                        );

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdZonaplaneamiento"></param>
        /// <returns></returns>
        public IEnumerable<Ubicaciones> GetByFKIdZonaplaneamiento(int IdZonaplaneamiento)
        {
            IEnumerable<Ubicaciones> domains = _unitOfWork.Db.Ubicaciones.Where(x =>
                                                        x.id_zonaplaneamiento == IdZonaplaneamiento
                                                        );

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdComuna"></param>
        /// <returns></returns>
        public IEnumerable<Ubicaciones> GetByFKIdComuna(int IdComuna)
        {
            IEnumerable<Ubicaciones> domains = _unitOfWork.Db.Ubicaciones.Where(x =>
                                                        x.id_comuna == IdComuna
                                                        );

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdBarrio"></param>
        /// <returns></returns>
        public IEnumerable<Ubicaciones> GetByFKIdBarrio(int IdBarrio)
        {
            IEnumerable<Ubicaciones> domains = _unitOfWork.Db.Ubicaciones.Where(x =>
                                                        x.id_barrio == IdBarrio
                                                        );

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdAreaHospitalaria"></param>
        /// <returns></returns>
        public IEnumerable<Ubicaciones> GetByFKIdAreaHospitalaria(int IdAreaHospitalaria)
        {
            IEnumerable<Ubicaciones> domains = _unitOfWork.Db.Ubicaciones.Where(x =>
                                                        x.id_areahospitalaria == IdAreaHospitalaria
                                                        );

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdComisaria"></param>
        /// <returns></returns>
        public IEnumerable<Ubicaciones> GetByFKIdComisaria(int IdComisaria)
        {
            IEnumerable<Ubicaciones> domains = _unitOfWork.Db.Ubicaciones.Where(x =>
                                                        x.id_comisaria == IdComisaria
                                                        );

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdRegionSanitaria"></param>
        /// <returns></returns>
        public IEnumerable<Ubicaciones> GetByFKIdRegionSanitaria(int IdRegionSanitaria)
        {
            IEnumerable<Ubicaciones> domains = _unitOfWork.Db.Ubicaciones.Where(x =>
                                                        x.id_regionsanitaria == IdRegionSanitaria
                                                        );

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdDistritoEscolar"></param>
        /// <returns></returns>
        public IEnumerable<Ubicaciones> GetByFKIdDistritoEscolar(int IdDistritoEscolar)
        {
            IEnumerable<Ubicaciones> domains = _unitOfWork.Db.Ubicaciones.Where(x =>
                                                        x.id_distritoescolar == IdDistritoEscolar
                                                        );

            return domains;
        }

        public bool validarUbicacionClausuras(int IdUbicacion)
        {
            var ubiClausuras = (from solUbi in _unitOfWork.Db.Ubicaciones
                                join uc in _unitOfWork.Db.Ubicaciones_Clausuras on solUbi.id_ubicacion equals uc.id_ubicacion
                                where solUbi.id_ubicacion == IdUbicacion && uc.fecha_alta_clausura < DateTime.Now
                                && (uc.fecha_baja_clausura > DateTime.Now || uc.fecha_baja_clausura == null)
                                select uc.id_ubicclausura
                                ).ToList();
            if (ubiClausuras.Count() > 0)
                return true;

            //var horClausuras = (from solUbi in _unitOfWork.Db.Ubicaciones
            //                    join horUbi in _unitOfWork.Db.Ubicaciones_PropiedadHorizontal on solUbi.id_ubicacion equals horUbi.id_ubicacion
            //                    join hor in _unitOfWork.Db.Ubicaciones_PropiedadHorizontal_Clausuras on horUbi.id_propiedadhorizontal equals hor.id_propiedadhorizontal
            //                    where solUbi.id_ubicacion == IdUbicacion && hor.fecha_alta_clausura < DateTime.Now
            //                    && (hor.fecha_baja_clausura > DateTime.Now || hor.fecha_baja_clausura == null)
            //                    select hor.id_ubicphorclausura
            //                    ).ToList();
            //if (horClausuras.Count() > 0)
            //    return true;
            return false;
        }

        public bool validarUbicacionInhibiciones(int IdUbicacion)
        {
            var ubiInhibiciones = (from solUbi in _unitOfWork.Db.Ubicaciones_Inhibiciones
                                   where solUbi.id_ubicacion == IdUbicacion && solUbi.fecha_inhibicion < DateTime.Now
                                    && (solUbi.fecha_vencimiento > DateTime.Now || solUbi.fecha_vencimiento == null)
                                   select solUbi.id_ubicinhibi
                                ).ToList();
            if (ubiInhibiciones.Count() > 0)
                return true;

            //var horInhibiciones = (from solUbi in _unitOfWork.Db.Ubicaciones
            //                       join horUbi in _unitOfWork.Db.Ubicaciones_PropiedadHorizontal on solUbi.id_ubicacion equals horUbi.id_ubicacion
            //                       join hor in _unitOfWork.Db.Ubicaciones_PropiedadHorizontal_Inhibiciones on horUbi.id_propiedadhorizontal equals hor.id_propiedadhorizontal
            //                       where solUbi.id_ubicacion == IdUbicacion && hor.fecha_inhibicion < DateTime.Now
            //                        && (hor.fecha_vencimiento > DateTime.Now || hor.fecha_vencimiento == null)
            //                       select hor.id_ubicphorinhibi
            //                    ).ToList();
            //if (horInhibiciones.Count() > 0)
            //    return true;
            return false;
        }
    }
}
