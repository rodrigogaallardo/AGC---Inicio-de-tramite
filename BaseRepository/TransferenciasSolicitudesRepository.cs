using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;
using StaticClass;

namespace BaseRepository
{
    public class TransferenciasSolicitudesRepository : BaseRepository<Transf_Solicitudes>
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransferenciasSolicitudesRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdConsultaPadron"></param>
        /// <returns></returns>	
        public IEnumerable<Transf_Solicitudes> GetByFKIdConsultaPadron(int IdConsultaPadron)
        {
            IEnumerable<Transf_Solicitudes> domains = _unitOfWork.Db.Transf_Solicitudes.Where(x =>
                                                        x.id_cpadron == IdConsultaPadron
                                                        );

            return domains;
        }

        /// <summary>
        /// Obtiene el ultimo id_tramitetarea para volver a regenerar una boleta vencida con ese mismo id
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        /// 
        public Transf_Solicitudes GetByFKIdEncomienda(int IdEncomienda)
        {
            Transf_Solicitudes domains = (from sol in _unitOfWork.Db.Transf_Solicitudes
                                          join encSol in _unitOfWork.Db.Encomienda_Transf_Solicitudes on sol.id_solicitud equals encSol.id_solicitud
                                          join enc in _unitOfWork.Db.Encomienda on encSol.id_encomienda equals enc.id_encomienda
                                          where enc.id_encomienda == IdEncomienda
                                          select sol).FirstOrDefault();

            return domains;
        }

        public int GetIdTramiteTareaPago(int IdSolicitud)
        {
            var domains = (from tt_tr in _unitOfWork.Db.SGI_Tramites_Tareas_TRANSF
                           join tt in _unitOfWork.Db.SGI_Tramites_Tareas on tt_tr.id_tramitetarea equals tt.id_tramitetarea
                           join ttpag in _unitOfWork.Db.SGI_Solicitudes_Pagos on tt.id_tramitetarea equals ttpag.id_tramitetarea
                           where tt_tr.id_solicitud == IdSolicitud
                           select ttpag)
                            .OrderByDescending(o => o.id_sol_pago)
                            .FirstOrDefault();

            int id_tramitetarea = 0;
            if (domains != null)
                id_tramitetarea = domains.id_tramitetarea;

            return id_tramitetarea;
        }

        public IQueryable<Transf_Solicitudes> GetSolicitudesAprobadasxExpediente(int anio_expediente, int nro_expediente)
        {

            string formato_SADE = string.Format("{0}-{1}", anio_expediente, nro_expediente.ToString().PadLeft(8, Convert.ToChar("0")));

            IQueryable<Transf_Solicitudes> res = (from sol in _unitOfWork.Db.Transf_Solicitudes
                                                  where sol.NroExpedienteSade.Contains(formato_SADE)
                                                  && sol.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.APRO
                                                  select sol);

            return res;
        }

        public IQueryable<Transf_Solicitudes> GetSolicitudesAprobadasxPartidaMatriz(int nro_partida_matriz)
        {
            IQueryable<Transf_Solicitudes> res = (from sol in _unitOfWork.Db.Transf_Solicitudes
                                                  join cpubic in _unitOfWork.Db.CPadron_Ubicaciones on sol.id_cpadron equals cpubic.id_cpadron
                                                  join ubic in _unitOfWork.Db.Ubicaciones on cpubic.id_ubicacion equals ubic.id_ubicacion
                                                  where ubic.NroPartidaMatriz == nro_partida_matriz && sol.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.APRO
                                                  select sol).Distinct();


            return res;
        }

        public IQueryable<Transf_Solicitudes> GetSolicitudesAprobadasxCUIT(string cuit)
        {
            IQueryable<Transf_Solicitudes> res = (from sol in _unitOfWork.Db.Transf_Solicitudes
                                                  join titpf in _unitOfWork.Db.Transf_Titulares_PersonasFisicas on sol.id_solicitud equals titpf.id_solicitud into pleft_titpf
                                                  from titpf in pleft_titpf.DefaultIfEmpty()
                                                  join titpj in _unitOfWork.Db.Transf_Titulares_PersonasJuridicas on sol.id_solicitud equals titpj.id_solicitud into pleft_titpj
                                                  from titpj in pleft_titpj.DefaultIfEmpty()
                                                  where (titpf.Cuit == cuit || titpj.CUIT == cuit)
                                                     && sol.id_estado == (int)Constantes.TipoEstadoSolicitudEnum.APRO
                                                  select sol).Distinct();

            return res;
        }

        public IEnumerable<Transf_Solicitudes> GetListaIdSolicitudTransf(List<int> listIDSolicitud)
        {
            return _unitOfWork.Db.Transf_Solicitudes.Where(x => listIDSolicitud.Contains(x.id_solicitud)).ToList();
        }

        public IEnumerable<Transf_Solicitudes> GetRangoIdSolicitud(int inicio, int fin)
        {
            return _unitOfWork.Db.Transf_Solicitudes.Where(x => x.id_solicitud >= inicio && x.id_solicitud <= fin).ToList();
        }

        public string GetMixDistritoZonaySubZonaByTr(int idSolicitud)
        {
            var parameter = new System.Data.Entity.Core.Objects.ObjectParameter("result", "varchar(1000)");
            _unitOfWork.Db.GetMixDistritoZonaySubZonaByTR(idSolicitud, parameter);
            return parameter.Value.ToString();
        }
    }
}

