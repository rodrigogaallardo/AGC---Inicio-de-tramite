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
    public class EncomiendaTiposSobrecargasBL : IEncomiendaTiposSobrecargasBL<EncomiendaTiposSobrecargasDTO>, IDisposable
    {
        private EncomiendaTiposSobrecargasRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public EncomiendaTiposSobrecargasBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EncomiendaTiposSobrecargasDTO, Encomienda_Tipos_Sobrecargas>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }

        public IEnumerable<EncomiendaTiposSobrecargasDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaTiposSobrecargasRepository(this.uowF.GetUnitOfWork());
                var entityTipoDocumentosReqs = repo.GetAll().ToList();
                var lstMenuesDto = mapperBase.Map<List<Encomienda_Tipos_Sobrecargas>, IEnumerable<EncomiendaTiposSobrecargasDTO>>(entityTipoDocumentosReqs);
                return lstMenuesDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
