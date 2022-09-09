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
    public class EncomiendaSobrecargaDetalle2BL : IEncomiendaSobrecargaDetalle2BL<EncomiendaSobrecargaDetalle2DTO>, IDisposable
    {
        private EncomiendaSobrecargaDetalle2Repository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public EncomiendaSobrecargaDetalle2BL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EncomiendaSobrecargaDetalle2DTO, Encomienda_Sobrecarga_Detalle2>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }

        public IEnumerable<EncomiendaSobrecargaDetalle2DTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaSobrecargaDetalle2Repository(this.uowF.GetUnitOfWork());
                var entityTipoDocumentosReqs = repo.GetAll().ToList();
                var lstMenuesDto = mapperBase.Map<List<Encomienda_Sobrecarga_Detalle2>, IEnumerable<EncomiendaSobrecargaDetalle2DTO>>(entityTipoDocumentosReqs);
                return lstMenuesDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<EncomiendaSobrecargaDetalle2DTO> GetByFKIdSobrecargaDetalle1(int id_sobrecarga_detalle1)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaSobrecargaDetalle2Repository(this.uowF.GetUnitOfWork());
                var entityTipoDocumentosReqs = repo.GetByFKIdSobrecargaDetalle1(id_sobrecarga_detalle1);
                var lstMenuesDto = mapperBase.Map<IEnumerable<Encomienda_Sobrecarga_Detalle2>, IEnumerable<EncomiendaSobrecargaDetalle2DTO>>(entityTipoDocumentosReqs);
                return lstMenuesDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(EncomiendaSobrecargaDetalle2DTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaSobrecargaDetalle2Repository(unitOfWork);
                    var elementDto = mapperBase.Map<EncomiendaSobrecargaDetalle2DTO, Encomienda_Sobrecarga_Detalle2>(objectDto);
                    repo.Insert(elementDto);

                    unitOfWork.Commit();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(EncomiendaSobrecargaDetalle2DTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaSobrecargaDetalle2Repository(unitOfWork);
                    var elementDto = mapperBase.Map<EncomiendaSobrecargaDetalle2DTO, Encomienda_Sobrecarga_Detalle2>(objectDto);
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
