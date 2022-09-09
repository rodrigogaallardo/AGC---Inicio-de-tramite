using AutoMapper;
using BaseRepository;
using IBusinessLayer;
using Dal.UnitOfWork;
using DataAcess;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitOfWork;
using DataAcess.EntityCustom;

namespace BusinesLayer.Implementation
{
    public class TransferenciasSolicitudesHistorialEstadosBL : ITransferenciasSolicitudesHistorialEstadosBL<TransferenciasSolicitudesHistorialEstadosDTO>
    {
        private TransferenciasSolicitudesHistorialEstadosRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public TransferenciasSolicitudesHistorialEstadosBL()
        {
            var configHistorial = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TransferenciasSolicitudesHistorialEstadosGrillaDTO, TransferenciasSolicitudesHistorialEstadosGrillaEntity>().ReverseMap();

                cfg.CreateMap<TransferenciasSolicitudesHistorialEstadosDTO, Transf_Solicitudes_HistorialEstados>().ReverseMap();

            });

            mapperBase = configHistorial.CreateMapper();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="solicitud"></param>
        /// <returns></returns>

        public List<TransferenciasSolicitudesHistorialEstadosGrillaDTO> GetByFKIdSolicitudGrilla(TransferenciasSolicitudesDTO solicitud)
        {
            try
            {
                List<TransferenciasSolicitudesHistorialEstadosGrillaDTO> lstHistDTO = new List<TransferenciasSolicitudesHistorialEstadosGrillaDTO>();
                TransferenciasSolicitudesHistorialEstadosGrillaDTO HistDTO = new TransferenciasSolicitudesHistorialEstadosGrillaDTO();

                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TransferenciasSolicitudesHistorialEstadosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByFKIdSolicitudGrilla(solicitud.IdSolicitud);
                var elementsDto = mapperBase.Map<IEnumerable<TransferenciasSolicitudesHistorialEstadosGrillaEntity>, IEnumerable<TransferenciasSolicitudesHistorialEstadosGrillaDTO>>(elements);
                HistDTO.Estado = StaticClass.Constantes.ESTADO_INCOMPLETO_DESCRIPCION;
                HistDTO.fecha = solicitud.CreateDate;
                lstHistDTO.Add(HistDTO);
                lstHistDTO.AddRange(elementsDto.ToList());

                return lstHistDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
