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
    public class EncomiendaCertificadoSobrecargaBL : IEncomiendaCertificadoSobrecargaBL<EncomiendaCertificadoSobrecargaDTO>
    {
        private EncomiendaCertificadoSobrecargaRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public EncomiendaCertificadoSobrecargaBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EncomiendaCertificadoSobrecargaDTO, Encomienda_Certificado_Sobrecarga>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }

        public IEnumerable<EncomiendaCertificadoSobrecargaDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaCertificadoSobrecargaRepository(this.uowF.GetUnitOfWork());
                var entityTipoDocumentosReqs = repo.GetAll().ToList();
                var lstMenuesDto = mapperBase.Map<List<Encomienda_Certificado_Sobrecarga>, IEnumerable<EncomiendaCertificadoSobrecargaDTO>>(entityTipoDocumentosReqs);
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
        /// <param name="IdEncomiendaDatosLocal"></param>
        /// <returns></returns>	
        public EncomiendaCertificadoSobrecargaDTO GetByFKIdEncomiendaDatosLocal(int IdEncomiendaDatosLocal)
        {
            uowF = new TransactionScopeUnitOfWorkFactory();
            repo = new EncomiendaCertificadoSobrecargaRepository(this.uowF.GetUnitOfWork());
            var elements = repo.GetByFKIdEncomiendaDatosLocal(IdEncomiendaDatosLocal);
            var elementsDto = mapperBase.Map<Encomienda_Certificado_Sobrecarga, EncomiendaCertificadoSobrecargaDTO>(elements);
            return elementsDto;
        }

        public int Insert(EncomiendaCertificadoSobrecargaDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaCertificadoSobrecargaRepository(unitOfWork);
                    var elementDto = mapperBase.Map<EncomiendaCertificadoSobrecargaDTO, Encomienda_Certificado_Sobrecarga>(objectDto);
                    repo.Insert(elementDto);

                    unitOfWork.Commit();

                    return elementDto.id_sobrecarga;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(EncomiendaCertificadoSobrecargaDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaCertificadoSobrecargaRepository(unitOfWork);
                    var elementDto = mapperBase.Map<EncomiendaCertificadoSobrecargaDTO, Encomienda_Certificado_Sobrecarga>(objectDto);
                    repo.Update(elementDto);

                    unitOfWork.Commit();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(EncomiendaCertificadoSobrecargaDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {
                    repo = new EncomiendaCertificadoSobrecargaRepository(unitOfWork);
                    var elementDto = mapperBase.Map<EncomiendaCertificadoSobrecargaDTO, Encomienda_Certificado_Sobrecarga>(objectDto);
                    var insertOk = repo.Delete(elementDto);
                    unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
