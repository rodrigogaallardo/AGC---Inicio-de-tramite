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
    public class RubrosImpactoAmbientalCNBL : IRubrosImpactoAmbientalCNBL<RubrosImpactoAmbientalCNDTO>
    {
        private RubrosImpactoAmbientalCNRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public RubrosImpactoAmbientalCNBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RubrosImpactoAmbientalCNDTO, RubrosImpactoAmbientalCN>().ReverseMap();

                
            });
            mapperBase = config.CreateMapper();
        }
        public RubrosImpactoAmbientalCNDTO Single(int IdRubroImpactoAmbiental)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosImpactoAmbientalCNRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(IdRubroImpactoAmbiental);
                var entityDto = mapperBase.Map<RubrosImpactoAmbientalCN, RubrosImpactoAmbientalCNDTO>(entity);

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
        /// <param name="IdImpactoAmbiental"></param>
        /// <returns></returns>	
        public IEnumerable<RubrosImpactoAmbientalCNDTO> GetByFKIdImpactoAmbiental(int IdImpactoAmbiental)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new RubrosImpactoAmbientalCNRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdImpactoAmbiental(IdImpactoAmbiental);
            var elementsDto = mapperBase.Map<IEnumerable<RubrosImpactoAmbientalCN>, IEnumerable<RubrosImpactoAmbientalCNDTO>>(elements);
            return elementsDto;
        }

        public RubrosImpactoAmbientalCNDTO GetImpactoAmbiental(decimal Superficie, int IdRubro)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new RubrosImpactoAmbientalCNRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetByFKIdRubro(IdRubro);
                var elementsDto = mapperBase.Map<IEnumerable<RubrosImpactoAmbientalCN>, IEnumerable<RubrosImpactoAmbientalCNDTO>>(elements);
                elementsDto = elementsDto.Where(p => p.DesdeM2 <= Superficie && p.HastaM2 >= Superficie);
                return elementsDto.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
