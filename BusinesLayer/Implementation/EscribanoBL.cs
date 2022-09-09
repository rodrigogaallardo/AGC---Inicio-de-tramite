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

namespace BusinesLayer.Implementation
{
	public class EscribanoBL : IEscribanoBL<EscribanoDTO>
    {               
		private EscribanoRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public EscribanoBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EscribanoDTO, Escribano>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<EscribanoDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EscribanoRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Escribano>, IEnumerable<EscribanoDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<EscribanoDTO> GetEscribanos(int BusMatricula, string BusApeNom,
            int startRowIndex, int maximumRows, string sortByExpression, out int totalRowCount)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EscribanoRepository(this.uowF.GetUnitOfWork());
                var q = repo.GetEscribanos(BusMatricula, BusApeNom);

                totalRowCount = q.Count();

                q = q.OrderBy(o => o.Matricula).Skip(startRowIndex).Take(maximumRows);

                var rq = q.ToList();


                var elementsDto = mapperBase.Map<List<Escribano>, List<EscribanoDTO>>(rq);

                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	
		public EscribanoDTO Single(int Matricula )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new EscribanoRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(Matricula);
                var entityDto = mapperBase.Map<Escribano, EscribanoDTO>(entity);
     
                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

