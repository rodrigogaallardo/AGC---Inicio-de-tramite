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
    public class EncomiendaTiposCertificadosSobrecargaBL : IEncomiendaTiposCertificadosSobrecargaBL<EncomiendaTiposCertificadosSobrecargaDTO>, IDisposable
    {
        private EncomiendaTiposCertificadosSobrecargaRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public EncomiendaTiposCertificadosSobrecargaBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EncomiendaTiposCertificadosSobrecargaDTO, Encomienda_Tipos_Certificados_Sobrecarga>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }

        public IEnumerable<EncomiendaTiposCertificadosSobrecargaDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EncomiendaTiposCertificadosSobrecargaRepository(this.uowF.GetUnitOfWork());
                var entityTipoDocumentosReqs = repo.GetAll().ToList();
                var lstMenuesDto = mapperBase.Map<List<Encomienda_Tipos_Certificados_Sobrecarga>, IEnumerable<EncomiendaTiposCertificadosSobrecargaDTO>>(entityTipoDocumentosReqs);
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
