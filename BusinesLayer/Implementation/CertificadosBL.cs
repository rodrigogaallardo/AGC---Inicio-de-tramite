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
	public class CertificadosBL : ICertificadosBL<CertificadosDTO>
    {               
		private CertificadosRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public CertificadosBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CertificadosDTO, vis_Certificados>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }
		
		public CertificadosDTO Single(int id_certificado)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new CertificadosRepository(this.uowF.GetUnitOfWork());
                var entity = repo.GetById(id_certificado);
                var entityDto = mapperBase.Map<vis_Certificados, CertificadosDTO>(entity);

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
		/// <param name="IdEncomienda"></param>
		/// <returns></returns>	
		public IEnumerable<CertificadosDTO> GetByFKNroTipo(int NroTramite, int TipoTramite)
        {
			uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new CertificadosRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKNroTipo(NroTramite, TipoTramite);
            var elementsDto = mapperBase.Map<IEnumerable<vis_Certificados>, IEnumerable<CertificadosDTO>>(elements);
            return elementsDto;				
		}

        public IEnumerable<CertificadosDTO> GetByFKListNroTipo(List<int> list, int TipoTramite)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new CertificadosRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKListNroTipo(list, TipoTramite);
            var elementsDto = mapperBase.Map<IEnumerable<vis_Certificados>, IEnumerable<CertificadosDTO>>(elements);
            return elementsDto;
        }
    }
}

