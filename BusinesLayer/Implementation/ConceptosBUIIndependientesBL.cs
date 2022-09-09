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
	public class ConceptosBUIIndependientesBL : IConceptosBUIIndependientesBL<ConceptosBUIIndependientesDTO>
    {               
		private ConceptosBUIIndependientesRepository repo = null;        
        private IUnitOfWorkFactory uowF = null;
        IMapper mapperBase;
		         
        public ConceptosBUIIndependientesBL()
        {            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ConceptosBUIIndependientesDTO, Conceptos_BUI_Independientes>().ReverseMap();
            });
            mapperBase = config.CreateMapper();
        }
		
        public IEnumerable<ConceptosBUIIndependientesDTO> GetAll()
        {
			try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConceptosBUIIndependientesRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetAll();
                var elementsDto = mapperBase.Map<IEnumerable<Conceptos_BUI_Independientes>, IEnumerable<ConceptosBUIIndependientesDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }	

		public ConceptosBUIIndependientesDTO Single(int Matricula )
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConceptosBUIIndependientesRepository(this.uowF.GetUnitOfWork());
                var entity = repo.Single(Matricula);
                var entityDto = mapperBase.Map<Conceptos_BUI_Independientes, ConceptosBUIIndependientesDTO>(entity);
     
                return entityDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ConceptosBUIIndependientesDTO> GetList(string[] arrCodConceptosCobrar)
        {
            try
            {
                uowF = new TransactionScopeUnitOfWorkFactory();
                repo = new ConceptosBUIIndependientesRepository(this.uowF.GetUnitOfWork());
                var elements = repo.GetList(arrCodConceptosCobrar);
                var elementsDto = mapperBase.Map<IEnumerable<Conceptos_BUI_Independientes>, IEnumerable<ConceptosBUIIndependientesDTO>>(elements);
                return elementsDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

