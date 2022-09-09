using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using DataAcess.EntityCustom;
using Dal.UnitOfWork;

namespace BaseRepository
{
    public class EncomiendaExternaHistorialEstadosRepository : BaseRepository<EncomiendaExt_HistorialEstados>
    {
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unit"></param>
        public EncomiendaExternaHistorialEstadosRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>
        public IQueryable<EncomiendaExternaHistorialEstadosEntity> GetHistorialEncomiendaExterna(int IdEncomienda)
        {
            var query =  (
                    from hist in _unitOfWork.Db.EncomiendaExt_HistorialEstados
                    join esta_nuevo in _unitOfWork.Db.EncomiendaExt_Estados on hist.cod_estado_nuevo equals esta_nuevo.cod_estado
                    into j from esta_nuevo in j.DefaultIfEmpty()
                    join esta_viejo in _unitOfWork.Db.EncomiendaExt_Estados on hist.cod_estado_ant equals esta_viejo.cod_estado
                    into h from esta_viejo in h.DefaultIfEmpty()
                    where hist.id_encomienda == IdEncomienda
                    select new EncomiendaExternaHistorialEstadosEntity()
                    {
                        fecha_modificacion = hist.fecha_modificacion,
                        id_enchistest = hist.id_enchistest,
                        id_encomienda = hist.id_encomienda,
                        usuario_modificacion = hist.usuario_modificacion,
                        UserId = hist.aspnet_Users.UserId,
                        UserName = hist.aspnet_Users.UserName,
                        cod_estado_ant = hist.cod_estado_ant,
                        cod_estado_nuevo = hist.cod_estado_nuevo,
                        cod_estado_viejo = h.Any() ?  h.FirstOrDefault().cod_estado : string.Empty,
                        id_estado_viejo = h.Any() ?  h.FirstOrDefault().id_estado : 0,
                        nom_estado_viejo = h.Any() ? h.FirstOrDefault().nom_estado : string.Empty,
                        nom_estado_consejo_viejo = h.Any() ? h.FirstOrDefault().nom_estado_consejo : string.Empty,
                        id_estado_nuevo = esta_nuevo.id_estado,
                        nom_estado_nuevo = esta_nuevo.nom_estado,
                        nom_estado_consejo_nuevo = esta_nuevo.nom_estado_consejo
                    });

            return query; 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>
        public IQueryable<EncomiendaExternaHistorialEstadosEntity> GetHistorial(int IdEncomienda)
        {
            var query = (
                    from hist in _unitOfWork.Db.Encomienda_HistorialEstados
                    join esta_nuevo in _unitOfWork.Db.Encomienda_Estados on hist.cod_estado_nuevo equals esta_nuevo.cod_estado
                    into j
                    from esta_nuevo in j.DefaultIfEmpty()
                    join esta_viejo in _unitOfWork.Db.Encomienda_Estados on hist.cod_estado_ant equals esta_viejo.cod_estado
                    into h
                    from esta_viejo in h.DefaultIfEmpty()
                    where hist.id_encomienda == IdEncomienda
                    select new EncomiendaExternaHistorialEstadosEntity()
                    {
                        fecha_modificacion = hist.fecha_modificacion,
                        id_enchistest = hist.id_enchistest,
                        id_encomienda = hist.id_encomienda,
                        usuario_modificacion = hist.usuario_modificacion,
                        UserId = hist.aspnet_Users.UserId,
                        UserName = hist.aspnet_Users.UserName,
                        cod_estado_ant = hist.cod_estado_ant,
                        cod_estado_nuevo = hist.cod_estado_nuevo,
                        cod_estado_viejo = h.Any() ? h.FirstOrDefault().cod_estado : string.Empty,
                        id_estado_viejo = h.Any() ? h.FirstOrDefault().id_estado : 0,
                        nom_estado_viejo = h.Any() ? h.FirstOrDefault().nom_estado : string.Empty,
                        nom_estado_consejo_viejo = h.Any() ? h.FirstOrDefault().nom_estado_consejo : string.Empty,
                        id_estado_nuevo = esta_nuevo.id_estado,
                        nom_estado_nuevo = esta_nuevo.nom_estado,
                        nom_estado_consejo_nuevo = esta_nuevo.nom_estado_consejo
                    });

            return query;
        }
    }
}