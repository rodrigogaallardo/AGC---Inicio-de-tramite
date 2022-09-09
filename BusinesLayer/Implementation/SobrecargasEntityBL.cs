using IBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;
using DataAcess;
using DataAcess.EntityCustom;
using BaseRepository;
using Dal.UnitOfWork;
using UnitOfWork;
using AutoMapper;
using System.Data;
using System.Transactions;



namespace BusinesLayer.Implementation
{
    public class SobrecargasEntityBL : ISobrecargasEntityBL<SobrecargasEntityDTO>, IDisposable
    {
        private SobrecargasEntityRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public SobrecargasEntityBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SobrecargasEntityDTO, SobrecargasEntity>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SobrecargasEntityDTO> getSobrecargaDetallado(int id_sobrecarga)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new SobrecargasEntityRepository(this.uowF.GetUnitOfWork());
                var sobrecargas = repo.getSobrecargaDetallado(id_sobrecarga);
                var lstsobrecargasDto = mapperBase.Map<IEnumerable<SobrecargasEntity>, IEnumerable<SobrecargasEntityDTO>>(sobrecargas);
                return lstsobrecargasDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
