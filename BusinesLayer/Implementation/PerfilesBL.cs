using IBusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;
using BaseRepository;
using AutoMapper;
using UnitOfWork;
using DataAcess;

namespace BusinesLayer.Implementation
{
    //public class PerfilesBL :IBaseBusiness<PerfilesDTO>,IPerfilesBL<PerfilesDTO>
    //{

    //    //private PerfilesRepository repo = null;
    //    private IUnitOfWorkFactory uowF = null;
    //    IMapper mapperBase;


    //    public PerfilesBL()
    //    {
    //        var config = new MapperConfiguration(cfg =>
    //        {
    //            cfg.CreateMap<PerfilesDTO, BAFYCO_Perfiles>().ReverseMap();                    
                
    //        });
    //        mapperBase = config.CreateMapper();
        
    //    }
    //    public IEnumerable<PerfilesDTO> GetAll()
    //    {
    //        try
    //        {
    //            uowF = new TransactionScopeUnitOfWorkFactory();
    //            repo = new PerfilesRepository(this.uowF.GetUnitOfWork());
    //            var entityPerfiles = repo.GetAll().ToList();
    //            var lstPerfilesDto = mapperBase.Map<List<BAFYCO_Perfiles>, IEnumerable<PerfilesDTO>>(entityPerfiles);
    //            return lstPerfilesDto;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    public PerfilesDTO Single(int idPerfil)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public PerfilesDTO MyProperty
    //    {
    //        get
    //        {
    //            throw new NotImplementedException();
    //        }
    //        set
    //        {
    //            throw new NotImplementedException();
    //        }
    //    }
    //}
}
