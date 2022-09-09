using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;
using StaticClass;

namespace BaseRepository
{
    public class EncomiendaRubrosCNRepository : BaseRepository<Encomienda_RubrosCN>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EncomiendaRubrosCNRepository(IUnitOfWork unit) : base(unit)
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
        public IEnumerable<Encomienda_RubrosCN> GetByFKIdEncomienda(int IdEncomienda)
        {
            IEnumerable<Encomienda_RubrosCN> domains = _unitOfWork.Db.Encomienda_RubrosCN.Where(x =>
                                                        x.id_encomienda == IdEncomienda
                                                        );

            return domains;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>
        public IEnumerable<RubrosCN> GetRubrosByIdEncomienda(int IdEncomienda)
        {
            var domains = (from encRubros in _unitOfWork.Db.Encomienda_RubrosCN
                           join rub in _unitOfWork.Db.RubrosCN on encRubros.CodigoRubro equals rub.Codigo
                           where encRubros.id_encomienda == IdEncomienda
                           select rub);

            return domains;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdEncomienda"></param>
        /// <returns></returns>
        public IEnumerable<EncomiendaRubrosCNEntity> GetRubrosCN(int IdEncomienda)
        {
            var CodZona = "";
            //CodZona = (from encubic in _unitOfWork.Db.Encomienda_Ubicaciones
            //             join ubi in _unitOfWork.Db.Ubicaciones on encubic.id_ubicacion equals ubi.id_ubicacion
            //             join zona in _unitOfWork.Db.ZonasMixtura on ubi.IdMixtura equals zona.IdZona
            //             where encubic.id_encomienda == IdEncomienda
            //             select zona.Codigo).FirstOrDefault();

            var resolution =
                            from enc in _unitOfWork.Db.Encomienda
                            join ubicrub in _unitOfWork.Db.Encomienda_RubrosCN on enc.id_encomienda equals ubicrub.id_encomienda
                            join act in _unitOfWork.Db.TipoActividad on ubicrub.IdTipoActividad equals act.Id
                            join rub in _unitOfWork.Db.RubrosCN on ubicrub.CodigoRubro equals rub.Codigo
                            where
                            enc.id_encomienda == IdEncomienda
                            select new EncomiendaRubrosCNEntity()
                            {
                                IdEncomiendaRubro = ubicrub.id_encomiendarubro,
                                CodigoRubro = ubicrub.CodigoRubro,
                                DescripcionRubro = ubicrub.NombreRubro,
                                SuperficieHabilitar = ubicrub.SuperficieHabilitar,
                                TipoActividadNombre = act.Nombre,
                                IdTipoActividad = act.Id,
                                IdEncomienda = ubicrub.id_encomienda,
                                CreateDate = ubicrub.CreateDate,
                                EsAnterior = false,
                                RestriccionZona = "tilde.png",
                                RestriccionSup = "tilde.png",
                            };
            return resolution;
        }

        public int GetRubrosSubrubrosCN(int idEncomiendaRubro, int id_encomienda)
        {
            return (from sub in _unitOfWork.Db.Encomienda_RubrosCN_Subrubros
                    join rub in _unitOfWork.Db.Encomienda_RubrosCN on sub.Id_EncRubro equals rub.id_encomiendarubro
                    where (rub.id_encomiendarubro == idEncomiendaRubro) && (rub.id_encomienda == id_encomienda)
                    select sub).Count();
        }

        public IEnumerable<EncomiendaRubrosCNEntity> GetRubrosCNATAnterior(int IdEncomienda)
        {
            var resolution = (from enc in _unitOfWork.Db.Encomienda
                              join ubicrub in _unitOfWork.Db.Encomienda_RubrosCN_AT_Anterior on enc.id_encomienda equals ubicrub.id_encomienda
                              join act in _unitOfWork.Db.TipoActividad on ubicrub.IdTipoActividad equals act.Id
                              join rub in _unitOfWork.Db.RubrosCN on ubicrub.CodigoRubro equals rub.Codigo
                              where
                              enc.id_encomienda == IdEncomienda
                              select new EncomiendaRubrosCNEntity()
                              {
                                  IdEncomiendaRubro = ubicrub.id_encomiendarubro,
                                  CodigoRubro = ubicrub.CodigoRubro,
                                  DescripcionRubro = ubicrub.NombreRubro,
                                  SuperficieHabilitar = ubicrub.SuperficieHabilitar,
                                  TipoActividadNombre = act.Nombre,
                                  IdTipoActividad = act.Id,
                                  IdEncomienda = ubicrub.id_encomienda,
                                  CreateDate = ubicrub.CreateDate,
                                  EsAnterior = false,
                                  RestriccionZona = "tilde.png",
                                  RestriccionSup = "tilde.png",
                              });


            //var resolution = (from enc in _unitOfWork.Db.Encomienda
            // join ubicrub in _unitOfWork.Db.Encomienda_RubrosCN_AT_Anterior on enc.id_encomienda equals ubicrub.id_encomienda
            // join act in _unitOfWork.Db.TipoActividad on ubicrub.IdTipoActividad equals act.Id
            // join rub in _unitOfWork.Db.RubrosCN on ubicrub.CodigoRubro equals rub.Codigo
            // where
            // enc.id_encomienda == IdEncomienda
            // select new EncomiendaRubrosCNEntity()
            // {
            //     IdEncomiendaRubro = ubicrub.id_encomiendarubro,
            //     CodigoRubro = ubicrub.CodigoRubro,
            //     DescripcionRubro = ubicrub.NombreRubro,
            //     SuperficieHabilitar = ubicrub.SuperficieHabilitar,
            //     TipoActividadNombre = act.Nombre,
            //     IdTipoActividad = act.Id,
            //     IdEncomienda = ubicrub.id_encomienda,
            //     CreateDate = ubicrub.CreateDate,
            //     EsAnterior = false,
            //     RestriccionZona = "tilde.png",
            //     RestriccionSup = "tilde.png",
            // }).Union(from enc in _unitOfWork.Db.Encomienda
            //          join ubicrub in _unitOfWork.Db.Encomienda_Rubros_AT_Anterior on enc.id_encomienda equals ubicrub.id_encomienda
            //          join act in _unitOfWork.Db.TipoActividad on ubicrub.id_tipoactividad equals act.Id
            //          join rub in _unitOfWork.Db.Rubros on ubicrub.cod_rubro equals rub.cod_rubro
            //          where
            //          enc.id_encomienda == IdEncomienda
            //          select new EncomiendaRubrosCNEntity()
            //          {
            //              IdEncomiendaRubro = ubicrub.id_encomiendarubro,
            //              CodigoRubro = ubicrub.cod_rubro,
            //              DescripcionRubro = ubicrub.desc_rubro,
            //              SuperficieHabilitar = ubicrub.SuperficieHabilitar,
            //              TipoActividadNombre = act.Nombre,
            //              IdTipoActividad = act.Id,
            //              IdEncomienda = ubicrub.id_encomienda,
            //              CreateDate = ubicrub.CreateDate,
            //              EsAnterior = false,
            //              RestriccionZona = "tilde.png",
            //              RestriccionSup = "tilde.png",
            //          });

            return resolution;
        }
    }
}

