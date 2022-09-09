using AutoMapper;
using BaseRepository;
using BusinesLayer.MappingConfig;
using Dal.UnitOfWork;
using DataAcess;
using DataAcess.EntityCustom;
using DataTransferObject;
using IBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork;

namespace BusinesLayer.Implementation
{
    public class Encomienda_RubrosCN_DepositoBL : IEncomienda_RubrosCN_DepositoBL<Encomienda_RubrosCN_DepositoDTO>
    {
        private Encomienda_RubrosCN_DepositoRepository repo = null;

        private IUnitOfWorkFactory uowF = null;

        IMapper mapperBase;

        public Encomienda_RubrosCN_DepositoBL()
        {
        }

        public List<Encomienda_RubrosCN_DepositoDTO> GetByEncomienda(int idEncomienda)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new Encomienda_RubrosCN_DepositoRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetRubrosDepositosByEncomienda(idEncomienda);
                var elementsDto = AutoMapperConfig.MapperBaseEncomienda.Map<IEnumerable<Encomienda_RubrosCN_Deposito>, IEnumerable<Encomienda_RubrosCN_DepositoDTO>>(elements);
                return elementsDto.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Tuple<string, int>> GetByEncToList(int idEncomienda)
        {
            try
            {
                List<Tuple<string, int>> listRubDep = new List<Tuple<string, int>>();
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new Encomienda_RubrosCN_DepositoRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetRubrosDepositosByEncomienda(idEncomienda);
                var elementsDto = AutoMapperConfig.MapperBaseEncomienda.Map<IEnumerable<Encomienda_RubrosCN_Deposito>, IEnumerable<Encomienda_RubrosCN_DepositoDTO>>(elements);
                foreach(var element in elementsDto)
                {
                    Tuple<string, int> dep = new Tuple<string, int>(element.RubrosCNDTO.Codigo, element.IdDeposito);
                    listRubDep.Add(dep);
                }
                return listRubDep;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertRubDeposito(Encomienda_RubrosCN_DepositoDTO RubDep)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    Encomienda_RubrosCN_DepositoRepository repo = new Encomienda_RubrosCN_DepositoRepository(unitOfWork);
                    //var element = mapperBase.Map<Encomienda_RubrosCN_DepositoDTO, Encomienda_RubrosCN_Deposito>(RubDep);
                    var element = new Encomienda_RubrosCN_Deposito();
                    element.id_encomienda = RubDep.id_encomienda;
                    element.IdRubro = RubDep.IdRubro;
                    element.IdDeposito = RubDep.IdDeposito;
                    repo.Insert(element);
                    unitOfWork.Commit();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool DeleteRubDeposito(int idEncomienda, int idRubroCN)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    Encomienda_RubrosCN_DepositoRepository repo = new Encomienda_RubrosCN_DepositoRepository(unitOfWork);

                    var elements = repo.Where(x => x.id_encomienda == idEncomienda && x.IdRubro == idRubroCN).ToList();
                    repo.DeleteRange(elements);
                    unitOfWork.Commit();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
