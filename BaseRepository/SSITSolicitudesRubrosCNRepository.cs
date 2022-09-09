using Dal.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class SSITSolicitudesRubrosCNRepository : BaseRepository<SSIT_Solicitudes_RubrosCN>
    {

        private readonly IUnitOfWork _unitOfWork;

        public SSITSolicitudesRubrosCNRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }

        public IEnumerable<SSIT_Solicitudes_RubrosCN> GetByFKIdSolicitud(int IdSolicitud)
        {
            IEnumerable<SSIT_Solicitudes_RubrosCN> domains = _unitOfWork.Db.SSIT_Solicitudes_RubrosCN.Where(x =>
                                                        x.IdSolicitud == IdSolicitud
                                                        );

            return domains;
        }

        public IEnumerable<SSIT_Solicitudes_RubrosCN> GetRubrosCN(int IdSolicitud)
        {

            var resolution = (from sol in _unitOfWork.Db.SSIT_Solicitudes
                              join solrub in _unitOfWork.Db.SSIT_Solicitudes_RubrosCN on sol.id_solicitud equals solrub.IdSolicitud
                              join act in _unitOfWork.Db.TipoActividad on solrub.IdTipoActividad equals act.Id
                              join rub in _unitOfWork.Db.RubrosCN on solrub.IdRubro equals rub.IdRubro
                              where
                                  sol.id_solicitud == IdSolicitud
                              select solrub);

            return resolution;
        }

        private IEnumerable<RubrosCNEntity> GetRubrosxZonaDistrito(string Rubro, string codigo, int tipo, decimal Superficie,  bool TieneNormativa)
        {
            UbicacionesZonasMixturasRepository repoZona = new UbicacionesZonasMixturasRepository(UnitOfWork);
            UbicacionesCatalogoDistritosRepository repoDis = new UbicacionesCatalogoDistritosRepository(UnitOfWork);
            Ubicaciones_CatalogoDistritos distrito = new Ubicaciones_CatalogoDistritos();

            int IdZona = 0;
            int IdDistrito = 0;

            if (tipo == 1)
            {
                var Zonas = repoZona.GetZona(codigo);
                IdZona = Zonas.IdZonaMixtura;
            }
            else
            {
                distrito = repoDis.GetDisrito(codigo);
                IdDistrito = distrito.IdDistrito;
            }

            var resolution = (from rub in _unitOfWork.Db.RubrosCN
                              join act in _unitOfWork.Db.TipoActividad on rub.IdTipoActividad equals act.Id
                              where rub.Nombre.Contains(Rubro) || rub.Codigo.Contains(Rubro)
                              select new RubrosCNEntity()
                              {
                                  Codigo = rub.Codigo,
                                  Nombre = rub.Nombre,
                                  TipoActividadNombre = act.Nombre,
                                  TieneNormativa = TieneNormativa,
                                  IdTipoActividad = act.Id,
                                  EsAnterior = false,
                                  RestriccionZona = "",
                                  Superficie = Superficie,
                                  IdTipoExpediente = rub.IdTipoExpediente,
                                  IdRubro = rub.IdRubro,
                                  VigenciaDesde = rub.VigenciaDesde_rubro,
                                  VigenciaHasta = rub.VigenciaHasta_rubro,
                                  ZonaMixtura = tipo == 1 ? IdZona : IdDistrito,
                                  CondicionZonaMixtura = (IdZona == 1 ? rub.ZonaMixtura1 :
                                                         (IdZona == 2 ? rub.ZonaMixtura2 :
                                                         (IdZona == 3 ? rub.ZonaMixtura3 :
                                                         (IdZona == 4 ? rub.ZonaMixtura4 : "C")))),
                                  Mensaje = ""
                              }).ToList();

            return resolution;
        }


        public IEnumerable<RubrosCNEntity> GetRubros(List<string> listZona, List<string> listDistritos, decimal Superficie, string Rubro, bool TieneNormativa)
        {
            List<RubrosCNEntity> lista = new List<RubrosCNEntity>();
            IEnumerable<RubrosCNEntity> rubros = null;

            foreach (var item in listZona)
            {
                rubros = GetRubrosxZonaDistrito(Rubro, item, 1, Superficie,  TieneNormativa);
                lista.AddRange(rubros);
            }

            foreach (var item in listDistritos)
            {
                rubros = GetRubrosxZonaDistrito(Rubro, item, 2, Superficie,  TieneNormativa);
                lista.AddRange(rubros);
            }
            var list = lista.OrderByDescending(z => z.Resultado).GroupBy(x => x.Codigo).Select(y => y.First());

            return list.OrderBy(x => x.Codigo);
        }



    }
}
