using Dal.UnitOfWork;
using DataAcess.EntityCustom;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BaseRepository
{
    public class ItemDirectionRepository : BaseRepository<ItemDirectionEntity>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ItemDirectionRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }

        /// <summary>
        /// Obtiene las direcciones para Encomiendas
        /// </summary>
        /// <param name="lstIDSolicitudes"></param>
        /// <returns></returns>
        public IEnumerable<ItemPuertaEntity> GetDirecciones(List<int> lstIDSolicitudes)
        {
            try
            {
                List<ItemDirectionEntity> lstItemDirection = new List<ItemDirectionEntity>();

                int idOT = (_unitOfWork.Db.TiposDeUbicacion.Where(x => x.descripcion_tipoubicacion == "Objeto territorial").Select(x => x.id_tipoubicacion)).FirstOrDefault();

                var LstItemPuerta = (from encubic in _unitOfWork.Db.Encomienda_Ubicaciones
                                     join encpuer in _unitOfWork.Db.Encomienda_Ubicaciones_Puertas on encubic.id_encomiendaubicacion equals encpuer.id_encomiendaubicacion into encpuer_join
                                     from encpuer in encpuer_join.DefaultIfEmpty()
                                     join encphor in _unitOfWork.Db.Encomienda_Ubicaciones_PropiedadHorizontal on new { Id_Encomiendabicacion = encubic.id_encomiendaubicacion } equals new { Id_Encomiendabicacion = encphor.id_encomiendaubicacion.Value } into encphor_join
                                     from encphor in encphor_join.DefaultIfEmpty()
                                     join phor in _unitOfWork.Db.Ubicaciones_PropiedadHorizontal on new { Id_propiedadhorizontal = encphor.id_propiedadhorizontal.Value } equals new { Id_propiedadhorizontal = phor.id_propiedadhorizontal } into phor_join
                                     from phor in phor_join.DefaultIfEmpty()
                                     where
                                       (lstIDSolicitudes).Contains(encubic.id_encomienda.Value)
                                     select new ItemPuertaEntity
                                     {
                                         idUbicacion = encubic.id_ubicacion,
                                         id_solicitud = encubic.Encomienda.id_encomienda,
                                         calle = encubic.SubTiposDeUbicacion.TiposDeUbicacion.id_tipoubicacion == 0 || encubic.SubTiposDeUbicacion.TiposDeUbicacion.id_tipoubicacion == idOT
                                         ? (encpuer.nombre_calle ?? "") : (encubic.SubTiposDeUbicacion.TiposDeUbicacion.descripcion_tipoubicacion + " " + encubic.SubTiposDeUbicacion.descripcion_subtipoubicacion),
                                         puerta = encubic.SubTiposDeUbicacion.TiposDeUbicacion.id_tipoubicacion == 0 || encubic.SubTiposDeUbicacion.TiposDeUbicacion.id_tipoubicacion == idOT
                                         ? encpuer.NroPuerta.ToString() ?? "" : ("Local " + encubic.local_subtipoubicacion ?? "")
                                     }).Distinct();
                return LstItemPuerta;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ItemPuertaEntity> GetDireccionesExt(List<int> lstIDSolicitudes)
        {
            try
            {
                List<ItemDirectionEntity> lstItemDirection = new List<ItemDirectionEntity>();

                var LstItemPuerta = (from encubic in _unitOfWork.Db.EncomiendaExt_Ubicaciones
                                     join encpuer in _unitOfWork.Db.EncomiendaExt_Ubicaciones_Puertas on encubic.id_encomiendaubicacion equals encpuer.id_encomiendaubicacion into encpuer_join
                                     from encpuer in encpuer_join.DefaultIfEmpty()
                                     join encphor in _unitOfWork.Db.EncomiendaExt_Ubicaciones_PropiedadHorizontal on new { Id_Encomiendabicacion = encubic.id_encomiendaubicacion } equals new { Id_Encomiendabicacion = encphor.id_encomiendaubicacion } into encphor_join
                                     from encphor in encphor_join.DefaultIfEmpty()
                                     join phor in _unitOfWork.Db.Ubicaciones_PropiedadHorizontal on new { Id_propiedadhorizontal = encphor.id_propiedadhorizontal } equals new { Id_propiedadhorizontal = phor.id_propiedadhorizontal } into phor_join
                                     from phor in phor_join.DefaultIfEmpty()
                                     where
                                       (lstIDSolicitudes).Contains(encubic.id_encomienda)
                                     select new ItemPuertaEntity
                                     {
                                         id_solicitud = encubic.EncomiendaExt.id_encomienda,
                                         calle = encubic.SubTiposDeUbicacion.TiposDeUbicacion.id_tipoubicacion == 0 ? (encpuer.nombre_calle ?? "") : (encubic.SubTiposDeUbicacion.TiposDeUbicacion.descripcion_tipoubicacion + " " + encubic.SubTiposDeUbicacion.descripcion_subtipoubicacion),
                                         puerta = encubic.SubTiposDeUbicacion.TiposDeUbicacion.id_tipoubicacion == 0 ? encpuer.NroPuerta.ToString() ?? "" : ("Local " + encubic.local_subtipoubicacion ?? "")
                                     }).Distinct();
                return LstItemPuerta;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ItemPuertaEntity> GetDireccionesAntenas(List<int> lstIDSolicitudes)
        {
            try
            {
                List<ItemDirectionEntity> lstItemDirection = new List<ItemDirectionEntity>();

                var LstItemPuerta = (from encubic in _unitOfWork.Db.ANT_Encomiendas
                                     join ubic in _unitOfWork.Db.ANT_Ubicaciones on encubic.id_encomienda equals ubic.id_encomienda
                                     join encubic_ubic in _unitOfWork.Db.ANT_Ubicaciones_Ubicacion on ubic.id_antubicacion equals encubic_ubic.id_antubicacion
                                     join stubic in _unitOfWork.Db.SubTiposDeUbicacion on encubic_ubic.id_subtipoubicacion equals stubic.id_subtipoubicacion
                                     into temp1
                                     from stubic_left in temp1.DefaultIfEmpty()
                                     join tubic in _unitOfWork.Db.TiposDeUbicacion on stubic_left.id_tipoubicacion equals tubic.id_tipoubicacion
                                     into temp2
                                     from tubic_left in temp2.DefaultIfEmpty()
                                     join encpuer in _unitOfWork.Db.ANT_Ubicaciones_Puertas on ubic.id_antubicacion equals encpuer.id_antubicacion into encpuer_join
                                     from encpuer in encpuer_join.DefaultIfEmpty()
                                     join encphor in _unitOfWork.Db.ANT_Ubicaciones_PropiedadHorizontal on new { Id_Encomiendabicacion = encubic_ubic.id_antubicacion } equals new { Id_Encomiendabicacion = encphor.id_antubicacion } into encphor_join
                                     from encphor in encphor_join.DefaultIfEmpty()
                                     join phor in _unitOfWork.Db.Ubicaciones_PropiedadHorizontal on new { Id_propiedadhorizontal = encphor.id_propiedadhorizontal } equals new { Id_propiedadhorizontal = phor.id_propiedadhorizontal } into phor_join
                                     from phor in phor_join.DefaultIfEmpty()
                                     join viapub in _unitOfWork.Db.ANT_Ubicaciones_ViaPublica on encubic_ubic.id_antubicacion equals viapub.id_antubicacion
                                     into temp6
                                     from viapub_left in temp6.DefaultIfEmpty()
                                     where
                                       (lstIDSolicitudes).Contains(encubic.id_encomienda)
                                     select new ItemPuertaEntity
                                     {
                                         id_solicitud = encubic.id_encomienda,
                                         calle = tubic_left.id_tipoubicacion == 0 ? (encpuer.Nombre_calle ?? "") : (tubic_left.descripcion_tipoubicacion + " " + stubic_left.descripcion_subtipoubicacion),
                                         puerta = tubic_left.id_tipoubicacion == 0 ? encpuer.NroPuerta.ToString() ?? "" : ("Local " + encubic_ubic.local_subtipoubicacion ?? "")
                                     }).Distinct();
                return LstItemPuerta;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ItemPuertaEntity> GetDireccionesCpadron(List<int> lstIDSolicitudes)
        {
            try
            {
                List<ItemDirectionEntity> lstItemDirection = new List<ItemDirectionEntity>();

                int idOT = (_unitOfWork.Db.TiposDeUbicacion.Where(x => x.descripcion_tipoubicacion == "Objeto territorial").Select(x => x.id_tipoubicacion)).FirstOrDefault();


                var LstItemPuerta = (from encubic in _unitOfWork.Db.CPadron_Ubicaciones
                                     join encpuer in _unitOfWork.Db.CPadron_Ubicaciones_Puertas on encubic.id_cpadronubicacion equals encpuer.id_cpadronubicacion into encpuer_join
                                     from encpuer in encpuer_join.DefaultIfEmpty()
                                     join encphor in _unitOfWork.Db.CPadron_Ubicaciones_PropiedadHorizontal on new { Id_cpadronubicacion = encubic.id_cpadronubicacion } equals new { Id_cpadronubicacion = encphor.id_cpadronubicacion.Value } into encphor_join
                                     from encphor in encphor_join.DefaultIfEmpty()
                                     join phor in _unitOfWork.Db.Ubicaciones_PropiedadHorizontal on new { Id_propiedadhorizontal = encphor.id_propiedadhorizontal.Value } equals new { Id_propiedadhorizontal = phor.id_propiedadhorizontal } into phor_join
                                     from phor in phor_join.DefaultIfEmpty()
                                     where
                                       (lstIDSolicitudes).Contains(encubic.id_cpadron.Value)
                                     select new ItemPuertaEntity
                                     {
                                         id_solicitud = encubic.CPadron_Solicitudes.id_cpadron,
                                         calle =
                                         encubic.SubTiposDeUbicacion.TiposDeUbicacion.id_tipoubicacion == 0 || encubic.SubTiposDeUbicacion.TiposDeUbicacion.id_tipoubicacion == idOT
                                         ? (encpuer.nombre_calle ?? "") : (encubic.SubTiposDeUbicacion.TiposDeUbicacion.descripcion_tipoubicacion + " " + encubic.SubTiposDeUbicacion.descripcion_subtipoubicacion),
                                         puerta =
                                         encubic.SubTiposDeUbicacion.TiposDeUbicacion.id_tipoubicacion == 0 || encubic.SubTiposDeUbicacion.TiposDeUbicacion.id_tipoubicacion == idOT
                                         ? encpuer.NroPuerta.ToString() ?? "" : ("Local " + encubic.local_subtipoubicacion ?? "")
                                     }).Distinct();


                return LstItemPuerta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<ItemPuertaEntity> GetDireccionesSSIT(List<int> lstIDSolicitudes)
        {
            try
            {
                List<ItemDirectionEntity> lstItemDirection = new List<ItemDirectionEntity>();

                int idOT = (_unitOfWork.Db.TiposDeUbicacion.Where(x => x.descripcion_tipoubicacion == "Objeto territorial").Select(x => x.id_tipoubicacion)).FirstOrDefault();

                var LstItemPuerta = (from solubic in _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones
                                     join encpuer in _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones_Puertas on solubic.id_solicitudubicacion equals encpuer.id_solicitudubicacion into encpuer_join
                                     from encpuer in encpuer_join.DefaultIfEmpty()
                                     join encphor in _unitOfWork.Db.SSIT_Solicitudes_Ubicaciones_PropiedadHorizontal on new { id_solicitudubicacion = solubic.id_solicitudubicacion } equals new { id_solicitudubicacion = encphor.id_solicitudubicacion.Value } into encphor_join
                                     from encphor in encphor_join.DefaultIfEmpty()
                                     join phor in _unitOfWork.Db.Ubicaciones_PropiedadHorizontal on new { Id_propiedadhorizontal = encphor.id_propiedadhorizontal.Value } equals new { Id_propiedadhorizontal = phor.id_propiedadhorizontal } into phor_join
                                     from phor in phor_join.DefaultIfEmpty()
                                     where
                                       (lstIDSolicitudes).Contains(solubic.id_solicitud.Value)
                                     select new ItemPuertaEntity
                                     {
                                         id_solicitud = solubic.SSIT_Solicitudes.id_solicitud,
                                         idUbicacion = solubic.id_ubicacion,
                                         calle = solubic.SubTiposDeUbicacion.TiposDeUbicacion.id_tipoubicacion == 0 || solubic.SubTiposDeUbicacion.TiposDeUbicacion.id_tipoubicacion == idOT ?
                                            (encpuer.nombre_calle ?? "") :
                                            (solubic.SubTiposDeUbicacion.TiposDeUbicacion.descripcion_tipoubicacion + " " + solubic.SubTiposDeUbicacion.descripcion_subtipoubicacion),
                                         puerta = solubic.SubTiposDeUbicacion.TiposDeUbicacion.id_tipoubicacion == 0 ? encpuer.NroPuerta.ToString() ?? "" :
                                         solubic.SubTiposDeUbicacion.TiposDeUbicacion.id_tipoubicacion == idOT ? encpuer.NroPuerta.ToString() + "t" :
                                        ("Local " + solubic.local_subtipoubicacion ?? ""),
                                         torre = solubic.Torre,
                                         local = solubic.Local,
                                         depto = solubic.Depto,
                                         otros = solubic.deptoLocal_ubicacion

                                     }).Distinct();

                return LstItemPuerta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ItemPuertaEntity> GetDireccionesTransf(List<int> lstIDSolicitudes)
        {
            try
            {
                List<ItemDirectionEntity> lstItemDirection = new List<ItemDirectionEntity>();
                ParametrosRepository repoparam = new ParametrosRepository(_unitOfWork);
                int nroTrReferencia = 0;
                int.TryParse(repoparam.GetParametroChar("NroTransmisionReferencia"), out nroTrReferencia);

                int idOT = (_unitOfWork.Db.TiposDeUbicacion.Where(x => x.descripcion_tipoubicacion == "Objeto territorial").Select(x => x.id_tipoubicacion)).FirstOrDefault();


                var LstItemPuerta = (from encubic in _unitOfWork.Db.CPadron_Ubicaciones
                                     join transf in _unitOfWork.Db.Transf_Solicitudes on encubic.id_cpadron equals transf.id_cpadron
                                     join encpuer in _unitOfWork.Db.CPadron_Ubicaciones_Puertas on encubic.id_cpadronubicacion equals encpuer.id_cpadronubicacion into encpuer_join
                                     from encpuer in encpuer_join.DefaultIfEmpty()
                                     join encphor in _unitOfWork.Db.CPadron_Ubicaciones_PropiedadHorizontal on new { Id_cpadronubicacion = encubic.id_cpadronubicacion } equals new { Id_cpadronubicacion = encphor.id_cpadronubicacion.Value } into encphor_join
                                     from encphor in encphor_join.DefaultIfEmpty()
                                     join phor in _unitOfWork.Db.Ubicaciones_PropiedadHorizontal on new { Id_propiedadhorizontal = encphor.id_propiedadhorizontal.Value } equals new { Id_propiedadhorizontal = phor.id_propiedadhorizontal } into phor_join
                                     from phor in phor_join.DefaultIfEmpty()
                                     where
                                       (lstIDSolicitudes.Where(x => x <= nroTrReferencia)).Contains(transf.id_solicitud)
                                     select new ItemPuertaEntity
                                     {
                                         id_solicitud = transf.id_solicitud,
                                         calle = encubic.SubTiposDeUbicacion.TiposDeUbicacion.id_tipoubicacion == 0 || encubic.SubTiposDeUbicacion.TiposDeUbicacion.id_tipoubicacion == idOT
                                         ? (encpuer.nombre_calle ?? "") : (encubic.SubTiposDeUbicacion.TiposDeUbicacion.descripcion_tipoubicacion + " " + encubic.SubTiposDeUbicacion.descripcion_subtipoubicacion),
                                         puerta = encubic.SubTiposDeUbicacion.TiposDeUbicacion.id_tipoubicacion == 0 || encubic.SubTiposDeUbicacion.TiposDeUbicacion.id_tipoubicacion == idOT
                                         ? encpuer.NroPuerta.ToString() ?? "" : ("Local " + encubic.local_subtipoubicacion ?? "")
                                     }).Distinct();

                var LstItemPuertaTR = (from encubic in _unitOfWork.Db.Transf_Ubicaciones
                                       join transf in _unitOfWork.Db.Transf_Solicitudes on encubic.id_solicitud equals transf.id_solicitud
                                       join encpuer in _unitOfWork.Db.Transf_Ubicaciones_Puertas on encubic.id_transfubicacion equals encpuer.id_transfubicacion into encpuer_join
                                       from encpuer in encpuer_join.DefaultIfEmpty()
                                       join encphor in _unitOfWork.Db.Transf_Ubicaciones_PropiedadHorizontal on new { id_transfubicacion = encubic.id_transfubicacion } equals new { id_transfubicacion = encphor.id_transfubicacion.Value } into encphor_join
                                       from encphor in encphor_join.DefaultIfEmpty()
                                       join phor in _unitOfWork.Db.Ubicaciones_PropiedadHorizontal on new { Id_propiedadhorizontal = encphor.id_propiedadhorizontal.Value } equals new { Id_propiedadhorizontal = phor.id_propiedadhorizontal } into phor_join
                                       from phor in phor_join.DefaultIfEmpty()
                                       where
                                         (lstIDSolicitudes.Where(x => x > nroTrReferencia)).Contains(transf.id_solicitud)
                                       select new ItemPuertaEntity
                                       {
                                           id_solicitud = transf.id_solicitud,
                                           calle = encubic.SubTiposDeUbicacion.TiposDeUbicacion.id_tipoubicacion == 0 ? (encpuer.nombre_calle ?? "") : (encubic.SubTiposDeUbicacion.TiposDeUbicacion.descripcion_tipoubicacion + " " + encubic.SubTiposDeUbicacion.descripcion_subtipoubicacion),
                                           puerta = encubic.SubTiposDeUbicacion.TiposDeUbicacion.id_tipoubicacion == 0 ? encpuer.NroPuerta.ToString() ?? "" : ("Local " + encubic.local_subtipoubicacion ?? "")
                                       }).Distinct();

                return LstItemPuerta.Union(LstItemPuertaTR);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ItemPuertaEntity> GetDireccionesUbic(List<int> lstIDUbicaciones)
        {
            try
            {
                List<ItemDirectionEntity> lstItemDirection = new List<ItemDirectionEntity>();

                int idOT = (_unitOfWork.Db.TiposDeUbicacion.Where(x => x.descripcion_tipoubicacion == "Objeto territorial").Select(x => x.id_tipoubicacion)).FirstOrDefault();

                var LstItemPuerta = (from ubic in _unitOfWork.Db.Ubicaciones
                                     join cpuer in _unitOfWork.Db.Ubicaciones_Puertas on ubic.id_ubicacion equals cpuer.id_ubicacion into cpuer_join
                                     from cpuer in cpuer_join.DefaultIfEmpty()
                                     join calles in _unitOfWork.Db.Calles on cpuer.codigo_calle equals calles.Codigo_calle
                                     where
                                         (lstIDUbicaciones).Contains(ubic.id_ubicacion)
                                     select new ItemPuertaEntity
                                     {
                                         seccion = ubic.Seccion,
                                         manzana = ubic.Manzana,
                                         parcela = ubic.Parcela,
                                         idUbicacion = ubic.id_ubicacion,
                                         calle = ubic.SubTiposDeUbicacion.TiposDeUbicacion.id_tipoubicacion == 0 || ubic.SubTiposDeUbicacion.TiposDeUbicacion.id_tipoubicacion == idOT
                                         ? (calles.NombreOficial_calle ?? "") : (ubic.SubTiposDeUbicacion.TiposDeUbicacion.descripcion_tipoubicacion + " " + ubic.SubTiposDeUbicacion.descripcion_subtipoubicacion),
                                         puerta = ubic.SubTiposDeUbicacion.TiposDeUbicacion.id_tipoubicacion == 0 || ubic.SubTiposDeUbicacion.TiposDeUbicacion.id_tipoubicacion == idOT
                                         ? cpuer.NroPuerta_ubic.ToString() ?? "" : ""
                                     }).Distinct();

                return LstItemPuerta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

