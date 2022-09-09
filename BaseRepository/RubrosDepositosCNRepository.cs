using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;
using System.Data.Entity.Core.Objects;
using System.Data;
using System.Data.Entity;
using DataTransferObject;

namespace BaseRepository
{
    public class RubrosDepositosCNRepository : BaseRepository<RubrosDepositosCN>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RubrosDepositosCNRepository(IUnitOfWork unit) 
            : base(unit)
        {
            if (unit == null) throw new ArgumentNullException("unitOfWork Exception");
            _unitOfWork = unit;
        }



        public IEnumerable<clsItemGrillaSeleccionarDepositos> GetListaDepositos(int id_encomienda)
        {
            bool TieneNormativaLocalizacion = _unitOfWork.Db.Encomienda_Normativas.Count(x => x.id_encomienda == id_encomienda) > 0;

            IEnumerable<clsItemGrillaSeleccionarDepositos> domains = 
                (from cat in _unitOfWork.Db.RubrosDepositosCategoriasCN
                join dep in _unitOfWork.Db.RubrosDepositosCN on cat.IdCategoriaDeposito equals dep.IdCategoriaDeposito
                where dep.VigenciaDesde < DateTime.Now && (dep.VigenciaHasta == null || dep.VigenciaHasta > DateTime.Now)
                orderby cat.IdCategoriaDeposito, dep.Codigo
                select new clsItemGrillaSeleccionarDepositos
                {
                    Categoria = cat.Descripcion,
                    IdDeposito = dep.IdDeposito,
                    IdCategoriaDeposito = dep.IdCategoriaDeposito,
                    Codigo = dep.Codigo,
                    Descripcion = dep.Descripcion,
                    GradoMolestia = dep.GradoMolestia,
                    CondicionZonaMixtura1 = dep.ZonaMixtura1,
                    CondicionZonaMixtura2 = dep.ZonaMixtura2,
                    CondicionZonaMixtura3 = dep.ZonaMixtura3,
                    CondicionZonaMixtura4 = dep.ZonaMixtura4,
                    ZonasMixtura = "",
                    Resultado = false,
                    TieneNormativa = TieneNormativaLocalizacion,
                    ObservacionesCategorizacion = dep.ObservacionesCategorizacion
                    //CodigoTipoCertificado = ""
                }).ToList();

            return domains;
        }


        public List<RubrosDepositosCN_Evaluar_Result> RubrosDepositosCN_Evaluar(int id_tramite, int idDeposito, decimal superficieCubierta, int zonaMixtura, string sistema)
        {
            ObjectResult<RubrosDepositosCN_Evaluar_Result> listaPrf = _unitOfWork.Db.RubrosDepositosCN_Evaluar(id_tramite, idDeposito, superficieCubierta, zonaMixtura, sistema);
            return listaPrf.ToList();

        }        
    }
}
