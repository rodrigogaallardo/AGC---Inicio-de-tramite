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
    public class EncomiendaRelTiposDestinosTiposUsosBL : IEncomiendaRelTiposDestinosTiposUsosBL<EncomiendaRelTiposDestinosTiposUsosDTO>, IDisposable
    {
        private EncomiendaRelTiposDestinosTiposUsosRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public EncomiendaRelTiposDestinosTiposUsosBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EncomiendaRelTiposDestinosTiposUsosDTO, Encomienda_Rel_TiposDestinos_TiposUsos>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }

        public IEnumerable<EncomiendaRelTiposDestinosTiposUsosDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaRelTiposDestinosTiposUsosRepository(this.uowF.GetUnitOfWork());
                var entityTipoDocumentosReqs = repo.GetAll().ToList();
                var lstMenuesDto = mapperBase.Map<List<Encomienda_Rel_TiposDestinos_TiposUsos>, IEnumerable<EncomiendaRelTiposDestinosTiposUsosDTO>>(entityTipoDocumentosReqs);
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

        public IEnumerable<EncomiendaRelTiposDestinosTiposUsosDTO> GetByFKIdTipoDestinoTipoTipo(int id_tipo_uso, int id_tipo_destino)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaRelTiposDestinosTiposUsosRepository(this.uowF.GetUnitOfWork());
                var entityTipoDocumentosReqs = repo.GetByFKIdTipoDestinoTipoTipo(id_tipo_uso,id_tipo_destino);
                var lstMenuesDto = mapperBase.Map<IEnumerable<Encomienda_Rel_TiposDestinos_TiposUsos>, IEnumerable<EncomiendaRelTiposDestinosTiposUsosDTO>>(entityTipoDocumentosReqs);
                return lstMenuesDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
