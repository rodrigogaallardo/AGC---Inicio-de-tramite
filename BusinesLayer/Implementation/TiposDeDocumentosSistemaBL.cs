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

namespace BusinesLayer.Implementation
{
	public class TiposDeDocumentosSistemaBL : ITiposDeDocumentosSistemaBL<TiposDeDocumentosSistemaDTO>
    {               
		private TiposDeDocumentosSistemaRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public TiposDeDocumentosSistemaBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TiposDeDocumentosSistemaDTO, TiposDeDocumentosSistema>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<TiposDeDocumentosSistemaDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TiposDeDocumentosSistemaRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<TiposDeDocumentosSistema>, IEnumerable<TiposDeDocumentosSistemaDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public TiposDeDocumentosSistemaDTO Single(int id_tipdocsis)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TiposDeDocumentosSistemaRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(id_tipdocsis);
                var entityDto = mapperBase.Map<TiposDeDocumentosSistema, TiposDeDocumentosSistemaDTO>(entity);
     
                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TiposDeDocumentosSistemaDTO GetByCodigo(string cod_tipodocsis)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TiposDeDocumentosSistemaRepository(this.uowF.GetUnitOfWork());
                var entity = repo.GetByCodigo(cod_tipodocsis);
                var entityDto = mapperBase.Map<TiposDeDocumentosSistema, TiposDeDocumentosSistemaDTO>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

