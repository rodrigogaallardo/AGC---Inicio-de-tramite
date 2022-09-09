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
    public class EncomiendaSobrecargaDetalle1BL : IEncomiendaSobrecargaDetalle1BL<EncomiendaSobrecargaDetalle1DTO>, IDisposable
    {
        private EncomiendaSobrecargaDetalle1Repository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public EncomiendaSobrecargaDetalle1BL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EncomiendaSobrecargaDetalle1DTO, Encomienda_Sobrecarga_Detalle1>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }

        public IEnumerable<EncomiendaSobrecargaDetalle1DTO> GetByFKIdSobrecarga(int id_sobrecarga)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaSobrecargaDetalle1Repository(this.uowF.GetUnitOfWork());
                var entityTipoDocumentosReqs = repo.GetByFKIdSobrecarga(id_sobrecarga);
                var lstMenuesDto = mapperBase.Map<IEnumerable<Encomienda_Sobrecarga_Detalle1>, IEnumerable<EncomiendaSobrecargaDetalle1DTO>>(entityTipoDocumentosReqs);
                return lstMenuesDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Insert(EncomiendaSobrecargaDetalle1DTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaSobrecargaDetalle1Repository(unitOfWork);
                    var elementDto = mapperBase.Map<EncomiendaSobrecargaDetalle1DTO, Encomienda_Sobrecarga_Detalle1>(objectDto);
                    repo.Insert(elementDto);

                    unitOfWork.Commit();

                    return elementDto.id_sobrecarga_detalle1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(EncomiendaSobrecargaDetalle1DTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaSobrecargaDetalle1Repository(unitOfWork);
                    var elementDto = mapperBase.Map<EncomiendaSobrecargaDetalle1DTO, Encomienda_Sobrecarga_Detalle1>(objectDto);
                    var insertOk = repo.Delete(elementDto);
                    unitOfWork.Commit();
                }
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
