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
using StaticClass;

namespace BusinesLayer.Implementation
{
	public class TiposDeDocumentosRequeridosBL : ITiposDeDocumentosRequeridosBL<TiposDeDocumentosRequeridosDTO>
    {               
		private TiposDeDocumentosRequeridosRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public TiposDeDocumentosRequeridosBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TiposDeDocumentosRequeridosDTO, TiposDeDocumentosRequeridos>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<TiposDeDocumentosRequeridosDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TiposDeDocumentosRequeridosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<TiposDeDocumentosRequeridos>, IEnumerable<TiposDeDocumentosRequeridosDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TiposDeDocumentosRequeridosDTO Single(int id_tdocreq)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TiposDeDocumentosRequeridosRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(id_tdocreq);
                var entityDto = mapperBase.Map<TiposDeDocumentosRequeridos, TiposDeDocumentosRequeridosDTO>(entity);
     
                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TiposDeDocumentosRequeridosDTO> GetVisibleAnexoTecnico()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TiposDeDocumentosRequeridosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetVisibleAnexoTecnico();
                var elementsDto = mapperBase.Map<IEnumerable<TiposDeDocumentosRequeridos>, IEnumerable<TiposDeDocumentosRequeridosDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TiposDeDocumentosRequeridosDTO> GetVisibleSSIT()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TiposDeDocumentosRequeridosRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetVisibleSSIT();
                var elementsDto = mapperBase.Map<IEnumerable<TiposDeDocumentosRequeridos>, IEnumerable<TiposDeDocumentosRequeridosDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TiposDeDocumentosRequeridosDTO> GetVisibleSSITXTipoTramite(int IdTipoTramite)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TiposDeDocumentosRequeridosRepository(this.uowF.GetUnitOfWork());
                IEnumerable<TiposDeDocumentosRequeridos> elements = null;
                
                if (IdTipoTramite == (int)Constantes.TipoDeTramite.ConsultaPadron)
                    elements = repo.GetVisibleConsultaPadron(IdTipoTramite);
                else
                    elements = repo.GetVisibleSSITXTipoTramite(IdTipoTramite);

                var elementsDto = mapperBase.Map<IEnumerable<TiposDeDocumentosRequeridos>, IEnumerable<TiposDeDocumentosRequeridosDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TiposDeDocumentosRequeridosDTO> GetByListIdTdoReq(List<int> LstIdTdocReq)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TiposDeDocumentosRequeridosRepository(this.uowF.GetUnitOfWork());
                var entity = repo.GetByListIdTdoReq(LstIdTdocReq);
                var entityDto = mapperBase.Map<IEnumerable<TiposDeDocumentosRequeridos>, IEnumerable<TiposDeDocumentosRequeridosDTO>>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

