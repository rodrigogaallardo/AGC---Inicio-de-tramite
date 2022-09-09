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
    public class EncomiendaTiposUsosBL : IEncomiendaTiposUsosBL<EncomiendaTiposUsosDTO>
    {
        private EncomiendaTiposUsosRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public EncomiendaTiposUsosBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EncomiendaTiposUsosDTO, Encomienda_Tipos_Usos>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }

        public IEnumerable<EncomiendaTiposUsosDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaTiposUsosRepository(this.uowF.GetUnitOfWork());
                var entityTipoDocumentosReqs = repo.GetAll().ToList();
                var lstMenuesDto = mapperBase.Map<List<Encomienda_Tipos_Usos>, IEnumerable<EncomiendaTiposUsosDTO>>(entityTipoDocumentosReqs);
                return lstMenuesDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<EncomiendaTiposUsosDTO> GetByFKIdTipoDestinoGrupo(int id_tipo_destino, int nroGrupo)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaTiposUsosRepository(this.uowF.GetUnitOfWork());
                var lst = repo.GetByFKIdTipoDestinoGrupo(id_tipo_destino,nroGrupo);
                var lstMenuesDto = mapperBase.Map<IEnumerable<Encomienda_Tipos_Usos>, IEnumerable<EncomiendaTiposUsosDTO>>(lst);
                return lstMenuesDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
