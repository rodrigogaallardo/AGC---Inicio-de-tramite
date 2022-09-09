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
    public class TiposDeTransmisionBL : ITiposDeTransmisionBL<TiposDeTransmisionDTO>
    {
        private TiposDeTransmisionesRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public TiposDeTransmisionBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TiposDeTransmisionDTO, TiposdeTransmision>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }

        public IEnumerable<TiposDeTransmisionDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TiposDeTransmisionesRepository(this.uowF.GetUnitOfWork());
                var entityTipoDeTransmisiones = repo.GetAll().ToList();
                var lst = mapperBase.Map<List<TiposdeTransmision>, IEnumerable<TiposDeTransmisionDTO>>(entityTipoDeTransmisiones);
                return lst;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TiposDeTransmisionDTO Single(int Id)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TiposDeTransmisionesRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(Id);
                var entityDto = mapperBase.Map<TiposdeTransmision, TiposDeTransmisionDTO>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
