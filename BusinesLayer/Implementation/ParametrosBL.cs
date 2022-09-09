using IBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using DataTransferObject;
using DataAcess;
using BaseRepository;
using UnitOfWork;
using AutoMapper;
using System.Configuration;

namespace BusinesLayer.Implementation
{
    public class ParametrosBL : IParametrosBL<ParametrosDTO>
    {
        private ParametrosRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public ParametrosBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Parametros, ParametrosDTO>()
                    .ForMember(dest => dest.IdParam, opt => opt.MapFrom(src => src.id_param))
                    .ForMember(dest => dest.CodParam, opt => opt.MapFrom(src => src.cod_param))
                    .ForMember(dest => dest.NomParam, opt => opt.MapFrom(src => src.nom_param))
                    .ForMember(dest => dest.ValorcharParam, opt => opt.MapFrom(src => src.valorchar_param))
                    .ForMember(dest => dest.ValornumParam, opt => opt.MapFrom(src => src.valornum_param))
                    .ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }

        public IEnumerable<ParametrosDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ParametrosRepository(this.uowF.GetUnitOfWork());
                var entityTipoDocumentosReqs = repo.GetAll().ToList();
                var lstMenuesDto = mapperBase.Map<List<Parametros>, IEnumerable<ParametrosDTO>>(entityTipoDocumentosReqs);
                return lstMenuesDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetParametroChar(string CodParam)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new ParametrosRepository(this.uowF.GetUnitOfWork());
            var element = repo.GetParametroChar(CodParam);
            return element;
        }
        public decimal? GetParametroNum(string CodParam)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new ParametrosRepository(this.uowF.GetUnitOfWork());
            var element = repo.GetParametroNum(CodParam);
            return element;
        }

        public ParametrosDTO GetParametros(string codParam)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new ParametrosRepository(this.uowF.GetUnitOfWork());
            var element = repo.GetParametros(codParam);
            var parametrosDTO = mapperBase.Map<ParametrosDTO>(element);
            
            if (parametrosDTO?.ValornumParam == null)
            {
                decimal.TryParse(ConfigurationManager.AppSettings["NroSolicitudReferencia"], out decimal nroSolReferencia);
                parametrosDTO = new ParametrosDTO
                {
                    ValornumParam = nroSolReferencia
                };
            }
            return parametrosDTO;
        }
    }
}
