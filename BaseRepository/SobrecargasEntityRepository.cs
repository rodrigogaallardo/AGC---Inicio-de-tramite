using System;
using System.Collections.Generic;
using System.Linq;
using DataAcess;
using Dal.UnitOfWork;
using DataAcess.EntityCustom;

namespace BaseRepository
{
    public class SobrecargasEntityRepository : BaseRepository<SobrecargasEntity>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SobrecargasEntityRepository(IUnitOfWork unit) : base(unit)
        {
            if (unit == null)
                throw new ArgumentNullException("unitOfWork Exception");

            _unitOfWork = unit;
        }
        public IEnumerable<SobrecargasEntity> getSobrecargaDetallado(int id_sobrecarga)
        {
            var lstSobrecargasTemp = (from d1 in _unitOfWork.Db.Encomienda_Sobrecarga_Detalle1
                                      join d2 in _unitOfWork.Db.Encomienda_Sobrecarga_Detalle2 on d1.id_sobrecarga_detalle1 equals d2.id_sobrecarga_detalle1
                                      join td in _unitOfWork.Db.Encomienda_Tipos_Destinos on d1.id_tipo_destino equals td.id_tipo_destino
                                      join tu in _unitOfWork.Db.Encomienda_Tipos_Usos on d1.id_tipo_uso equals tu.id_tipo_uso
                                      join ep in _unitOfWork.Db.Encomienda_Plantas on d1.id_encomiendatiposector equals ep.id_encomiendatiposector
                                      join tip_sec in _unitOfWork.Db.TipoSector on ep.id_tiposector equals tip_sec.Id
                                      join tu1 in _unitOfWork.Db.Encomienda_Tipos_Usos on d2.id_tipo_uso_1 equals tu1.id_tipo_uso
                                      join tu2 in _unitOfWork.Db.Encomienda_Tipos_Usos on d2.id_tipo_uso_2 equals tu2.id_tipo_uso
                                      join ecs in _unitOfWork.Db.Encomienda_Certificado_Sobrecarga on d1.id_sobrecarga equals ecs.id_sobrecarga

                                      where ecs.id_sobrecarga == id_sobrecarga
                                      select new SobrecargasEntity
                                      {
                                          id_sobrecarga_detalle1 = d1.id_sobrecarga_detalle1,
                                          id_tipo_destino = d1.id_tipo_destino,
                                          desc_tipo_destino = td.descripcion,
                                          id_tipo_uso = d1.id_tipo_uso,
                                          desc_tipo_uso = tu.descripcion,
                                          valor = d1.valor,
                                          detalle = d1.detalle,
                                          id_planta = d1.id_encomiendatiposector,
                                          desc_planta = (tip_sec.Id == 11 ? ep.detalle_encomiendatiposector : tip_sec.Nombre + " " + ep.detalle_encomiendatiposector),
                                          losa_sobre = d1.losa_sobre,
                                          id_tipo_uso_1 = d2.id_tipo_uso_1,
                                          desc_tipo_uso_1 = tu1.descripcion,
                                          valor_1 = d2.valor_1,
                                          id_tipo_uso_2 = d2.id_tipo_uso_2,
                                          desc_tipo_uso_2 = tu2.descripcion,
                                          valor_2 = d2.valor_2,
                                          texto_carga_uso = (ecs.id_tipo_sobrecarga == 1 ? "Admite sobrecarga de [kg/m2]" : "Admite sobrecarga de [kN/m2]"),
                                          texto_uso_1 = (ecs.id_tipo_sobrecarga == 1 ? "Pasillos de acceso general, escaleras, balcones" : "Escaleras"),
                                          texto_uso_2 = (ecs.id_tipo_sobrecarga == 1 ? "Barandilla de balcones y escaleras, esfuerzo horizontal dirigido al interior y aplicado sobre el pasamanos" : "Barandas")
                                      });
            return lstSobrecargasTemp;

        }
    }
}

