using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;
using System.Data.Entity.Core.Objects;
using System.Data;
using System.Data.Entity;
using StaticClass;
using DataTransferObject;

namespace BaseRepository
{
    /// <summary>
    /// Representa a la entidad rubros del Schema DTO
    /// </summary>
    public class RubrosCNRepository : BaseRepository<RubrosCN>
    {
        private readonly IUnitOfWork _unitOfWork;

        private IEnumerable<RubrosCNEntity> GetRubrosxZonaDistrito(string Rubro, string codigo, int tipo, decimal Superficie, int IdEncomienda)
        {
            UbicacionesZonasMixturasRepository repoZona = new UbicacionesZonasMixturasRepository(UnitOfWork);
            UbicacionesCatalogoDistritosRepository repoDis = new UbicacionesCatalogoDistritosRepository(UnitOfWork);
            Ubicaciones_CatalogoDistritos distrito = new Ubicaciones_CatalogoDistritos();
            EncomiendaRepository repoEnc = new EncomiendaRepository(UnitOfWork);

            var enc = repoEnc.Single(IdEncomienda);
            int IdZona = 0;
            int IdDistrito = 0;
            var normativa = enc.Encomienda_Normativas.Any();

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
                              where (rub.Nombre.Contains(Rubro) || rub.Codigo.Contains(Rubro)) && !rub.SoloAPRA
                              select new RubrosCNEntity()
                              {
                                  Codigo = rub.Codigo,
                                  Nombre = rub.Nombre,
                                  TipoActividadNombre = act.Nombre,
                                  TieneNormativa = normativa,
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

            if (enc.id_tipotramite == (int)Constantes.TipoDeTramite.Transferencia ||
                enc.id_tipotramite == (int)Constantes.TipoDeTramite.RedistribucionDeUso)
                return resolution;
            else
                return GetRubrosCN_Evaluar(resolution.Where(p => !p.VigenciaHasta.HasValue || p.VigenciaHasta > DateTime.Now), IdEncomienda, tipo);
        }
        public RubrosCNRepository(IUnitOfWork unit)
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }
        public IEnumerable<RubrosCN> Get(string Codigo)
        {
            var resolution = (from rub in _unitOfWork.Db.RubrosCN
                              where rub.Codigo == Codigo
                              select rub);
            return resolution;

        }
        public IEnumerable<RubrosCN> GetRubros(string codigo, decimal superficie)
        {
            var resolution = (from rub in _unitOfWork.Db.RubrosCN
                              where (rub.Codigo.Contains(codigo) || rub.Nombre.Contains(codigo)) &&
                              (rub.VigenciaHasta_rubro > DateTime.Now || rub.VigenciaHasta_rubro == null) &&
                              !rub.SoloAPRA 
                              select rub);
            return resolution;

        }

        private void AddParam(IDbCommand cmd, DbType tipo, ParameterDirection direc, string nombre, ValueType valor, int idparam)
        {

            IDbDataParameter param = cmd.CreateParameter();
            param.DbType = tipo;
            if (idparam == 7)
                param.Value = "Encomienda";
            else
                param.Value = valor;
            param.ParameterName = nombre;
            param.Direction = direc;
            cmd.Parameters.Add(param);
        }
        public IEnumerable<RubrosCNEntity> GetRubrosCN_Evaluar(IEnumerable<RubrosCNEntity> listRub, int IdEncomienda, int tipo)
        {

            try
            {

                var cmd = _unitOfWork.Db.Database.Connection.CreateCommand();
                _unitOfWork.Db.Database.Connection.Open();

                foreach (var rubro in listRub)
                {
                    if (tipo == 1)
                        cmd.CommandText = string.Format("EXEC RubrosCN_Evaluar {0},{1},{2},{3},{4}, 0", IdEncomienda, rubro.IdRubro, rubro.Superficie.ToString().Replace(",", "."), rubro.ZonaMixtura, "Encomienda");
                    else
                    {
                        int idUbicacion = _unitOfWork.Db.Encomienda_Ubicaciones.Where(x => x.id_encomienda == IdEncomienda).Select(y => (int)y.id_ubicacion).FirstOrDefault();
                        var zonaSubZona = _unitOfWork.Db.Ubicaciones_Distritos.Where(x => x.id_ubicacion == idUbicacion).FirstOrDefault();

                        cmd.CommandText = "RubrosCN_Distritos_Evaluar";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();

                        IDbDataParameter[] param = new IDbDataParameter[8];

                        AddParam(cmd, DbType.Int32, ParameterDirection.Input, "@id_tramite", IdEncomienda, 1);

                        AddParam(cmd, DbType.Int32, ParameterDirection.Input, "@IdDistrito", rubro.ZonaMixtura, 2);

                        AddParam(cmd, DbType.Int32, ParameterDirection.Input, "@IdZona", zonaSubZona.IdZona == null ? 0 : zonaSubZona.IdZona, 3);

                        AddParam(cmd, DbType.Int32, ParameterDirection.Input, "@IdSubZona", zonaSubZona.IdSubZona == null ? 0 : zonaSubZona.IdSubZona, 4);

                        AddParam(cmd, DbType.Int32, ParameterDirection.Input, "@IdRubro", rubro.IdRubro, 5);

                        AddParam(cmd, DbType.Decimal, ParameterDirection.Input, "@SuperficieHabilitar", rubro.Superficie, 6);

                        AddParam(cmd, DbType.String, ParameterDirection.Input, "@Sistema", null, 7);

                        AddParam(cmd, DbType.Boolean, ParameterDirection.InputOutput, "@Resultado", true, 8);
                    }

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        if (tipo == 2)
                        {
                            rubro.Resultado = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                            if (Convert.ToBoolean(cmd.Parameters["@Resultado"].Value) == true)
                                rubro.RestriccionZona = "tilde.png";
                            else
                                rubro.RestriccionZona = "Prohibido.png";
                        }
                        //Cada vez que se lee el reader automaticamente pasa a la 2da tabla (se lee al hacer el load)

                        var dt1 = new DataTable();
                        dt1.Load(reader);

                        if (dt1.AsEnumerable().Count() > 0)
                        {
                            DataRow dr1 = dt1.AsEnumerable().FirstOrDefault();
                            if (tipo == 1)
                            {
                                rubro.Resultado = Convert.ToBoolean(dr1["Resultado"]);
                                rubro.RestriccionZona = rubro.Resultado == true ? "tilde.png" : "Prohibido.png";
                            }
                            var dt2 = new DataTable();
                            dt2.Load(reader);
                            List<DataRow> lstRows = dt2.AsEnumerable().ToList();

                            rubro.Resultadoscondiciones = new List<itemResultadoEvaluacionCondiciones>();
                            foreach (var row in lstRows)
                            {
                                rubro.Resultadoscondiciones.Add(new itemResultadoEvaluacionCondiciones
                                {
                                    IdCondicion = Convert.ToInt32(row["IdCondicion"]),
                                    Codigo = row["Codigo"].ToString(),
                                    Descripcion = row["Descripcion"].ToString(),
                                    resultado = Convert.ToBoolean(row["Resultado"]),
                                    mensaje = row["mensaje"].ToString(),
                                });
                            }

                            foreach (var item in rubro.Resultadoscondiciones)
                            {
                                if (!item.resultado)
                                {
                                    if (rubro.Mensaje.Length > 0)
                                        rubro.Mensaje += "<br/>";

                                    rubro.Mensaje += string.Format("{0} - {1} {2}", item.Codigo, item.Descripcion, (!string.IsNullOrWhiteSpace(item.mensaje) ? ", " + item.mensaje : ""));
                                }
                            }
                        }
                    }

                }
            }
            finally
            {
                _unitOfWork.Db.Database.Connection.Close();
            }

            return listRub;
        }

        public string GetDescripcionDocumentosFaltantesByRubros(int id_solicitud, List<int> lstRubrosCN)
        {
            var list = _unitOfWork.Db.SSIT_DocumentosAdjuntos
              .Where(x => x.id_solicitud == id_solicitud)
              .Where(x => x.id_tdocreq != 0)
              .Select(x => x.id_tdocreq).ToList();

          var domains = (from rctddr in _unitOfWork.Db.RubrosCN_TiposDeDocumentosRequeridos
                           join rc in _unitOfWork.Db.RubrosCN on new { Id_rubro = rctddr.id_rubro } equals new { Id_rubro = rc.IdRubro }                           
                           where
                             lstRubrosCN.Contains(rctddr.id_rubro) && rctddr.obligatorio_rubtdocreq == true
                           select new
                          {
                              rctddr.id_tdocreq,
                              rctddr.TiposDeDocumentosRequeridos.nombre_tdocreq,
                          }).ToList();


            domains = domains.Where(d => !(list.Contains(d.id_tdocreq))).ToList();
                        
            string result = "";
            foreach (var item in domains)
            {
                result += "- " + item.nombre_tdocreq+ ". ";
            }
            return result;
        }


        public IEnumerable<RubrosCNEntity> GetRubros(List<string> listZona, List<string> listDistritos, decimal Superficie, string Rubro, int IdEncomienda)
        {
            List<RubrosCNEntity> lista = new List<RubrosCNEntity>();
            IEnumerable<RubrosCNEntity> rubros = null;

            foreach (var item in listZona)
            {
                rubros = GetRubrosxZonaDistrito(Rubro, item, 1, Superficie, IdEncomienda);
                lista.AddRange(rubros);
            }

            foreach (var item in listDistritos)
            {
                rubros = GetRubrosxZonaDistrito(Rubro, item, 2, Superficie, IdEncomienda);
                lista.AddRange(rubros);
            }
            var list = lista.OrderByDescending(z => z.Resultado).GroupBy(x => x.Codigo).Select(y => y.First());

            return list.OrderBy(x => x.Codigo);
        }

        public IEnumerable<RubrosCN> GetByListCodigo(List<string> lstcod_rubro)
        {
            var domains = _unitOfWork.Db.RubrosCN.Where(x => lstcod_rubro.Contains(x.Codigo) && !x.SoloAPRA);
            return domains;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdSolicitud"></param>
        /// <returns></returns>
        public IEnumerable<RubrosCN> GetRubrosByIdSolicitud(int IdSolicitud)
        {
            var domains = (from SolRub in _unitOfWork.Db.Rel_Rubros_Solicitudes_Nuevas
                           join Rub in _unitOfWork.Db.RubrosCN on SolRub.Codigo equals Rub.Codigo
                           where SolRub.id_Solicitud == IdSolicitud
                           select Rub);
            return domains;
        }

        public IEnumerable<RubrosCN> GetRubrosByIdEncomienda(int IdEncomienda)
        {
            var domains = (from EncRub in _unitOfWork.Db.Encomienda_RubrosCN
                           join Rub in _unitOfWork.Db.RubrosCN on EncRub.IdRubro equals Rub.IdRubro
                           where EncRub.id_encomienda == IdEncomienda
                           select Rub);
            return domains;
        }

        public bool CategorizaciónDeImpacto(string cod_rubro)
        {
            var domains = (from rel in _unitOfWork.Db.RubrosImpactoAmbientalCN
                           join Rub in _unitOfWork.Db.RubrosCN on rel.IdRubro equals Rub.IdRubro
                           where Rub.Codigo == cod_rubro
                           select rel.IdRubro).ToList();

            return domains.Count() > 0;
        }

    }
}
