using AutoMapper;
using BaseRepository;
using IBusinessLayer;
using Dal.UnitOfWork;
using DataAcess;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitOfWork;
using StaticClass;

namespace BusinesLayer.Implementation
{
    public class InstructivosBL
    {
        private InstructivosRepository repo = null;
        private IUnitOfWorkFactory uowF = null;
      
        IMapper mapperBase;

        public InstructivosBL()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<InstructivosDTO, Instructivos>().ReverseMap();

            });
            mapperBase = config.CreateMapper();
        }

        public InstructivosDTO getInstuctivo(string nombreInstructivo)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new InstructivosRepository(this.uowF.GetUnitOfWork());

                var instEntity = repo.GetInstructivosByTipo(nombreInstructivo);
                var elementsDto = mapperBase.Map<Instructivos, InstructivosDTO>(instEntity);
                return elementsDto;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}

