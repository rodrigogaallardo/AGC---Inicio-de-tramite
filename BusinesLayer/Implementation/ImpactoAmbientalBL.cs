using IBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;
using DataAcess;
using BaseRepository;
using Dal.UnitOfWork;
using UnitOfWork;
using AutoMapper;
using System.Data;
using System.Transactions;


namespace BusinesLayer.Implementation
{
    public class ImpactoAmbientalBL : IImpactoAmbientalBL<ImpactoAmbientalDTO>
    {
        private ImpactoAmbientalRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public ImpactoAmbientalBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ImpactoAmbientalDTO, ImpactoAmbiental>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }

        public IEnumerable<ImpactoAmbientalDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ImpactoAmbientalRepository(this.uowF.GetUnitOfWork());
                var entityTipoDocumentosReqs = repo.GetAll().ToList();
                var lstMenuesDto = mapperBase.Map<List<ImpactoAmbiental>, IEnumerable<ImpactoAmbientalDTO>>(entityTipoDocumentosReqs);
                return lstMenuesDto;
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
        public ImpactoAmbientalDTO GetByFKIdEncomienda(int IdEncomienda)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new ImpactoAmbientalRepository(this.uowF.GetUnitOfWork());
            var element = repo.GetByFKIdEncomienda(IdEncomienda);
            var elementDto = mapperBase.Map<ImpactoAmbiental, ImpactoAmbientalDTO>(element);
            return elementDto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdConsultaPadron"></param>
        /// <returns></returns>
        public ImpactoAmbientalDTO GetByFKIdConsultaPadron(int IdConsultaPadron)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new ImpactoAmbientalRepository(this.uowF.GetUnitOfWork());
            var element = repo.GetByFKIdConsultaPadron(IdConsultaPadron);
            var elementDto = mapperBase.Map<ImpactoAmbiental, ImpactoAmbientalDTO>(element);
            return elementDto;
        }
    }
}
