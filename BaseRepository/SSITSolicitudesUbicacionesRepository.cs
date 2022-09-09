using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using StaticClass;
using DataAcess.EntityCustom;
using DataTransferObject;

namespace BaseRepository
{
    public class SSITSolicitudesUbicacionesRepository : BaseRepository<SSIT_Solicitudes_Ubicaciones>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SSITSolicitudesUbicacionesRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSSITSolicitudes"></param>
        /// <returns></returns>	
        public IEnumerable<SSIT_Solicitudes_Ubicaciones> GetByFKIdSolicitud(int IdSolicitud)
        {
            IEnumerable<SSIT_Solicitudes_Ubicaciones> domains = _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones.Where(x =>
                                                        x.id_solicitud == IdSolicitud
                                                        );

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdUbicacion"></param>
        /// <returns></returns>	
        public IEnumerable<SSIT_Solicitudes_Ubicaciones> GetByFKIdUbicacion(int IdUbicacion)
        {
            IEnumerable<SSIT_Solicitudes_Ubicaciones> domains = _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones.Where(x =>
                                                        x.id_ubicacion == IdUbicacion
                                                        );

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSubtipoUbicacion"></param>
        /// <returns></returns>	
        public IEnumerable<SSIT_Solicitudes_Ubicaciones> GetByFKIdSubtipoUbicacion(int IdSubtipoUbicacion)
        {
            IEnumerable<SSIT_Solicitudes_Ubicaciones> domains = _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones.Where(x =>
                                                        x.id_subtipoubicacion == IdSubtipoUbicacion
                                                        );

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdZonaPlaneamiento"></param>
        /// <returns></returns>	
        public IEnumerable<SSIT_Solicitudes_Ubicaciones> GetByFKIdZonaPlaneamiento(int IdZonaPlaneamiento)
        {
            IEnumerable<SSIT_Solicitudes_Ubicaciones> domains = _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones.Where(x =>
                                                        x.id_zonaplaneamiento == IdZonaPlaneamiento
                                                        );

            return domains;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSSITSolicitudes"></param>
        /// <returns></returns>
        public IEnumerable<SsitSolicitudesUbicacionesModel> Get(int IdSSITSolicitudes)
        {
            var query = (from eu in _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones
                         join ubic in _unitOfWork.Db.Ubicaciones on eu.id_ubicacion equals ubic.id_ubicacion
                         join stubic in _unitOfWork.Db.SubTiposDeUbicacion on eu.id_subtipoubicacion equals stubic.id_subtipoubicacion
                         join tubic in _unitOfWork.Db.TiposDeUbicacion on stubic.id_tipoubicacion equals tubic.id_tipoubicacion
                         join zonpla in _unitOfWork.Db.Zonas_Planeamiento on eu.id_zonaplaneamiento equals zonpla.id_zonaplaneamiento
                         where eu.id_solicitud == IdSSITSolicitudes
                         select new SsitSolicitudesUbicacionesModel()
                         {
                             IdSolicitudUbicacion = eu.id_solicitudubicacion,
                             IdTipoUbicacion = tubic.id_tipoubicacion,
                             RequiereSMP = tubic.RequiereSMP,
                             DescripcionTipoUbicacion = tubic.descripcion_tipoubicacion,
                             DescripcionSubtipoUbicacion = stubic.descripcion_subtipoubicacion,
                             NroPartidaMatriz = ubic.NroPartidaMatriz,
                             Seccion = ubic.Seccion,
                             Manzana = ubic.Manzana,
                             Parcela = ubic.Parcela,
                             deptoLocal_ubicacion = eu.deptoLocal_ubicacion,
                             CodZonaPla = zonpla.CodZonaPla,
                             DescripcionZonaPla = zonpla.DescripcionZonaPla,
                             LocalSubTipoUbicacion = eu.local_subtipoubicacion
                         });

            return query;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSSITSolicitudesUbicacion"></param>
        /// <returns></returns>
        public IEnumerable<SsitSolicitudesUbicacionesModel> GetUbicacion(int IdSolicitudUbicacion)
        {
            var query = (from eu in _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones
                         join ubic in _unitOfWork.Db.Ubicaciones on eu.id_ubicacion equals ubic.id_ubicacion
                         join stubic in _unitOfWork.Db.SubTiposDeUbicacion on eu.id_subtipoubicacion equals stubic.id_subtipoubicacion
                         join tubic in _unitOfWork.Db.TiposDeUbicacion on stubic.id_tipoubicacion equals tubic.id_tipoubicacion
                         join zonpla in _unitOfWork.Db.Zonas_Planeamiento on eu.id_zonaplaneamiento equals zonpla.id_zonaplaneamiento
                         where eu.id_solicitudubicacion == IdSolicitudUbicacion
                         select new SsitSolicitudesUbicacionesModel()
                         {
                             IdSolicitudUbicacion = eu.id_solicitudubicacion,
                             IdTipoUbicacion = tubic.id_tipoubicacion,
                             RequiereSMP = tubic.RequiereSMP,
                             DescripcionTipoUbicacion = tubic.descripcion_tipoubicacion,
                             DescripcionSubtipoUbicacion = stubic.descripcion_subtipoubicacion,
                             NroPartidaMatriz = ubic.NroPartidaMatriz,
                             Seccion = ubic.Seccion,
                             Manzana = ubic.Manzana,
                             Parcela = ubic.Parcela,
                             deptoLocal_ubicacion = eu.deptoLocal_ubicacion,
                             CodZonaPla = zonpla.CodZonaPla,
                             DescripcionZonaPla = zonpla.DescripcionZonaPla,
                             LocalSubTipoUbicacion = eu.local_subtipoubicacion,
                             Torre = eu.Torre,
                             Local = eu.Local,
                             Depto = eu.Depto
                         });

            return query;
        }

        public bool validarUbicacionClausuras(int idSolicitud)
        {
            var ubiClausuras = (from solUbi in _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones
                                join uc in _unitOfWork.Db.Ubicaciones_Clausuras on solUbi.id_ubicacion equals uc.id_ubicacion
                                where solUbi.id_solicitud == idSolicitud && uc.fecha_alta_clausura < DateTime.Now
                                && (uc.fecha_baja_clausura > DateTime.Now || uc.fecha_baja_clausura == null)
                                select uc.id_ubicclausura
                                ).ToList();
            if (ubiClausuras.Count() > 0)
                return false;

            var horClausuras = (from solUbi in _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones
                                join horUbi in _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal on solUbi.id_solicitudubicacion equals horUbi.id_solicitudubicacion
                                join hor in _unitOfWork.Db.Ubicaciones_PropiedadHorizontal_Clausuras on horUbi.id_propiedadhorizontal equals hor.id_propiedadhorizontal
                                where solUbi.id_solicitud == idSolicitud && hor.fecha_alta_clausura < DateTime.Now
                                && (hor.fecha_baja_clausura > DateTime.Now || hor.fecha_baja_clausura == null)
                                select hor.id_ubicphorclausura
                                ).ToList();
            if (horClausuras.Count() > 0)
                return false;
            return true;
        }
        public bool validarUbicacionInhibiciones(int idSolicitud)
        {
            var ubiInhibiciones = (from solUbi in _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones
                                   join uc in _unitOfWork.Db.Ubicaciones_Inhibiciones on solUbi.id_ubicacion equals uc.id_ubicacion
                                   where solUbi.id_solicitud == idSolicitud && uc.fecha_inhibicion < DateTime.Now
                                        && (uc.fecha_vencimiento > DateTime.Now || uc.fecha_vencimiento == null)
                                   select uc.id_ubicinhibi
                                ).ToList();
            if (ubiInhibiciones.Count() > 0)
                return false;

            var horInhibiciones = (from solUbi in _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones
                                   join horUbi in _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal on solUbi.id_solicitudubicacion equals horUbi.id_solicitudubicacion
                                   join hor in _unitOfWork.Db.Ubicaciones_PropiedadHorizontal_Inhibiciones on horUbi.id_propiedadhorizontal equals hor.id_propiedadhorizontal
                                   where solUbi.id_solicitud == idSolicitud && hor.fecha_inhibicion < DateTime.Now
                                        && (hor.fecha_vencimiento > DateTime.Now || hor.fecha_vencimiento == null)
                                   select hor.id_ubicphorinhibi
                                ).ToList();
            if (horInhibiciones.Count() > 0)
                return false;
            return true;
        }

        public string GetMixDistritoZonaySubZonaBySolicitudUbicacion(int idSolicitudUbicacion)
        {
            var mix = (from ubi in _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones_Mixturas
                       join umix in _unitOfWork.Db.Ubicaciones_ZonasMixtura on ubi.IdZonaMixtura equals umix.IdZonaMixtura
                       where ubi.id_solicitudubicacion == idSolicitudUbicacion
                       select umix.Codigo).ToList();

            var dis = (from ud in _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones_Distritos
                       join ucdz in _unitOfWork.Db.Ubicaciones_CatalogoDistritos_Zonas on (int)ud.IdZona equals ucdz.IdZona into lzona
                       from lz in lzona.DefaultIfEmpty()
                       join ucdsz in _unitOfWork.Db.Ubicaciones_CatalogoDistritos_Subzonas on (int)ud.IdSubZona equals ucdsz.IdSubZona into lszona
                       from lsz in lszona.DefaultIfEmpty()
                       where ud.id_solicitudubicacion == idSolicitudUbicacion
                       select ud.Ubicaciones_CatalogoDistritos.Codigo + " " + lz.CodigoZona + " " + lsz.CodigoSubZona);

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


        public string GetMixDistritoZonaySubZonaBySolicitud(int idSolicitud)
        {
            var mix = (from ubic in _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones
                       join ubimix in _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones_Mixturas on ubic.id_solicitudubicacion equals ubimix.id_solicitudubicacion
                       join mixtura in _unitOfWork.Db.Ubicaciones_ZonasMixtura on ubimix.IdZonaMixtura equals mixtura.IdZonaMixtura
                       where ubic.id_solicitud == idSolicitud
                       select mixtura.Codigo).ToList();

            var dis = (from ubic in _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones
                       join ud in _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones_Distritos on ubic.id_solicitudubicacion equals ud.id_solicitudubicacion
                       join ucdz in _unitOfWork.Db.Ubicaciones_CatalogoDistritos_Zonas on (int)ud.IdZona equals ucdz.IdZona into lzona
                       from lz in lzona.DefaultIfEmpty()
                       join ucdsz in _unitOfWork.Db.Ubicaciones_CatalogoDistritos_Subzonas on (int)ud.IdSubZona equals ucdsz.IdSubZona into lszona
                       from lsz in lszona.DefaultIfEmpty()
                       where ubic.id_solicitud == idSolicitud
                       select ud.Ubicaciones_CatalogoDistritos.Codigo + " " + lz.CodigoZona + " " + lsz.CodigoSubZona);

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


        public bool esUbicacionEspecialConObjetoTerritorial(int? idSubtipoUbicacion)
        {
            var ot = _unitOfWork.Db.SubTiposDeUbicacion
                    .Where(x => x.id_tipoubicacion == (int)Constantes.TiposDeUbicacion.ObjetoTerritorial)
                     .Select(x => x.id_subtipoubicacion).ToList();

            return ot.Contains(idSubtipoUbicacion ?? default(int));
        }

        public bool esUbicacionEspecialConObjetoTerritorialByIdUbicacion(int idUbicacion)
        {
            var sbUbicacionesOT = _unitOfWork.Db.SubTiposDeUbicacion
                .Where(x => x.id_tipoubicacion == (int)Constantes.TiposDeUbicacion.ObjetoTerritorial)
                .Select(x => x.id_subtipoubicacion).ToList();

            var result = from ubi in _unitOfWork.Db.Ubicaciones
                         join sub in _unitOfWork.Db.SubTiposDeUbicacion on ubi.id_subtipoubicacion equals sub.id_subtipoubicacion
                         where (sbUbicacionesOT.Contains(sub.id_subtipoubicacion) && (ubi.id_ubicacion == idUbicacion))
                         select new
                         {
                             ubi.id_ubicacion
                         };

            return result.Count() > 0;
        }

        public int? getIdSubTipoUbicacionByIdUbicacion(int id_ubicacion)
        {
            return _unitOfWork.Db.Ubicaciones.Where(x => x.id_ubicacion == id_ubicacion).Select(x => x.id_subtipoubicacion).FirstOrDefault();
        }
    }
}

