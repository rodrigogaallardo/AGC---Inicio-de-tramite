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
    public class EncomiendaTiposDestinosBL : IEncomiendaTiposDestinosBL<EncomiendaTiposDestinosDTO>, IDisposable
    {
        private EncomiendaTiposDestinosRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public EncomiendaTiposDestinosBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EncomiendaTiposDestinosDTO, Encomienda_Tipos_Destinos>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }

        public IEnumerable<EncomiendaTiposDestinosDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaTiposDestinosRepository(this.uowF.GetUnitOfWork());
                var entityTipoDocumentosReqs = repo.GetAll().ToList();
                var lstMenuesDto = mapperBase.Map<List<Encomienda_Tipos_Destinos>, IEnumerable<EncomiendaTiposDestinosDTO>>(entityTipoDocumentosReqs);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_tipo_sobrecarga"></param>
        /// <returns></returns>
        public IEnumerable<EncomiendaTiposDestinosDTO> GetByFKIdTipoSobrecarga(int id_tipo_sobrecarga)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaTiposDestinosRepository(this.uowF.GetUnitOfWork());
                var lst = repo.GetByFKIdTipoSobrecarga(id_tipo_sobrecarga);
                var entityDto = mapperBase.Map<IEnumerable<Encomienda_Tipos_Destinos>,IEnumerable<EncomiendaTiposDestinosDTO>>(lst);
                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EncomiendaTiposDestinosDTO Single(int id_tipo_destino)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaTiposDestinosRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(id_tipo_destino);
                var entityDto = mapperBase.Map<Encomienda_Tipos_Destinos, EncomiendaTiposDestinosDTO>(entity);

                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
