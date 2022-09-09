using BusinesLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTrasnferObject;
using DataAcess;
using BaseRepository;
using Dal.UnitOfWork;
using UnitOfWork;
using AutoMapper;
using System.Data;
using System.Transactions;


namespace BusinesLayer.Implementation
{
    public class MenuesBL : IMenuesBL<MenuesDTO>, IDisposable
    {
        private MenuesRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;

        public MenuesBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MenuesDTO, BAFYCO_Menues>()
                    .ForMember(dest => dest.BAFYCO_Perfiles, 
                    opt => opt.MapFrom(src => src.PerfilesDtoCollection)).ReverseMap();
                cfg.CreateMap<PerfilesDTO, BAFYCO_Perfiles>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }

        public IEnumerable<MenuesDTO> GetAll()
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new MenuesRepository(this.uowF.GetUnitOfWork());
                var entityMenues = repo.GetAll().ToList();
                var lstMenuesDto = mapperBase.Map<List<BAFYCO_Menues>, IEnumerable<MenuesDTO>>(entityMenues);
                return lstMenuesDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MenuesDTO Single(int idPerfil)
        {
            throw new NotImplementedException();
        }


        public bool Insert(MenuesDTO objectDto)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory(System.Transactions.IsolationLevel.ReadUncommitted);
                
                using (IUnitOfWork unitOfWork = this.uowF.GetUnitOfWork(System.Transactions.IsolationLevel.ReadUncommitted))
                {                    
                    repo = new MenuesRepository(unitOfWork);                    
                    var bafycoEntyties = mapperBase.Map<MenuesDTO, BAFYCO_Menues>(objectDto);                   
                    var insertOk = repo.Insert(bafycoEntyties);
                    unitOfWork.Commit();
                    return true;
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
