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
    public class TipoDocumentacionRequeridaBL : ITipoDocumentacionRequeridaBL<TipoDocumentacionRequeridaDTO>, IDisposable
    {
        private TipoDocumentacionReqRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public TipoDocumentacionRequeridaBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TipoDocumentacionRequeridaDTO, Tipo_Documentacion_Req>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }

        public IEnumerable<TipoDocumentacionRequeridaDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new TipoDocumentacionReqRepository(this.uowF.GetUnitOfWork());
                var entityTipoDocumentosReqs = repo.GetAll().ToList();
                var lstMenuesDto = mapperBase.Map<List<Tipo_Documentacion_Req>, IEnumerable<TipoDocumentacionRequeridaDTO>>(entityTipoDocumentosReqs);
                return lstMenuesDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public MenuesDTO Single(int idPerfil)
        //{
        //    throw new NotImplementedException();
        //}


        //public bool Insert(MenuesDTO objectDto)
        //{
        //    try
        //    {
        //        uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);

        //        using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
        //        {
        //            repo = new MenuesRepository(unitOfWork);
        //            var bafycoEntyties = mapperBase.Map<MenuesDTO, BAFYCO_Menues>(objectDto);
        //            var insertOk = repo.Insert(bafycoEntyties);
        //            unitOfWork.Commit();
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }


        //}

        public void Dispose()
        {
            throw new NotImplementedException();
        }


        public bool Insert(TipoDocumentacionRequeridaDTO objectDto)
        {
            throw new NotImplementedException();
        }
    }
}
