using AutoMapper;
using BaseRepository;
using IBusinessLayer;
using DataAcess;
using DataTransferObject;
using System;
using System.Collections.Generic;
using UnitOfWork;
using DataAcess.EntityCustom;

namespace BusinesLayer.Implementation
{
	public class SSITDocumentosAdjuntosEntityBL : ISSITDocumentosAdjuntosEntityBL<SSITDocumentosAdjuntosEntityDTO>
    {               
		private SSITDocumentosAdjuntosEntityRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public SSITDocumentosAdjuntosEntityBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SSITDocumentosAdjuntosEntityDTO, SSITDocumentosAdjuntosEntity>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }
		
		public SSITDocumentosAdjuntosEntityDTO Single(int Id )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITDocumentosAdjuntosEntityRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(Id);
                var entityDto = mapperBase.Map<SSITDocumentosAdjuntosEntity, SSITDocumentosAdjuntosEntityDTO>(entity);
     
                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<SSITDocumentosAdjuntosEntityDTO> GetByFKIdSolicitud(int id_solicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITDocumentosAdjuntosEntityRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByFKIdSolicitud(id_solicitud);
                var elementsDto = mapperBase.Map<IEnumerable<SSITDocumentosAdjuntosEntity>, IEnumerable<SSITDocumentosAdjuntosEntityDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<SSITDocumentosAdjuntosEntityDTO> GetByFKIdSolicitudGeneradosIdDocReq(int id_solicitud, int id_docReq)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITDocumentosAdjuntosEntityRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByFKIdSolicitudGeneradosIdDocReq(id_solicitud, id_docReq);
                var elementsDto = mapperBase.Map<IEnumerable<SSITDocumentosAdjuntosEntity>, IEnumerable<SSITDocumentosAdjuntosEntityDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<SSITDocumentosAdjuntosEntityDTO> GetByFKIdSolicitudGenerados(int id_solicitud)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITDocumentosAdjuntosEntityRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByFKIdSolicitudGenerados(id_solicitud);
                var elementsDto = mapperBase.Map<IEnumerable<SSITDocumentosAdjuntosEntity>, IEnumerable<SSITDocumentosAdjuntosEntityDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<SSITDocumentosAdjuntosEntityDTO> GetByFKListIdEncomienda(List<int> lstIdEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SSITDocumentosAdjuntosEntityRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByFKListIdEncomienda(lstIdEncomienda);
                var elementsDto = mapperBase.Map<IEnumerable<SSITDocumentosAdjuntosEntity>, IEnumerable<SSITDocumentosAdjuntosEntityDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

